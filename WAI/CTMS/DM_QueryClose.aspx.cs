using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class DM_QueryClose : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction cmfn = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PVID"] = null;
            if (!IsPostBack)
            {
                FillDropDown();
            }
        }

        private void FillDropDown()
        {

            DataSet dsSite = dal.GetQueryReports(ACTION: "1", User_ID: Session["User_ID"].ToString());
            if (dsSite.Tables[0] != null)
            {
                drpSite.DataSource = dsSite.Tables[0];
                drpSite.DataTextField = "Text";
                drpSite.DataValueField = "Value";
                drpSite.DataBind();
                drpSite.Items.Insert(0, new ListItem("--Select--", ""));
                drpSite.Items.Insert(1, new ListItem("--All--", "0"));
                //cmfn.BindDropDown(drpSite, dsSite.Tables[0]);           
            }
            if (dsSite.Tables[1] != null)
            {
                cmfn.BindDropDown(drpVisit, dsSite.Tables[1]);
            }
            if (dsSite.Tables[2] != null)
            {
                cmfn.BindDropDown(drpModule, dsSite.Tables[2]);
            }
            //if (dsSite.Tables[3] != null)
            //{
            //    cmfn.BindDropDown(drpModule, dsSite.Tables[3]);
            //}
            if (dsSite.Tables[4] != null)
            {
                cmfn.BindDropDown(drpField, dsSite.Tables[4]);
            }
            if (dsSite.Tables[5] != null)
            {
                drpQueryStatus.Items.Clear();
                drpQueryStatus.DataSource = dsSite.Tables[5];
                drpQueryStatus.DataTextField = "Text";
                drpQueryStatus.DataValueField = "Value";
                drpQueryStatus.DataBind();
                drpQueryStatus.Items.Insert(0, new ListItem("--All--", ""));
            }

        }
        protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSite.SelectedItem.Value != "")
            {
                if (drpSite.SelectedItem.Value == "0")
                {
                    drpPatient.Items.Clear();
                    drpPatient.Items.Insert(0, new ListItem("--All--", "0"));
                    DataSet ds = dal.GetQueryReports(ACTION: "1");
                    if (ds.Tables[1] != null)
                    {
                        cmfn.BindDropDown(drpVisit, ds.Tables[1]);
                    }
                    if (ds.Tables[2] != null)
                    {
                        cmfn.BindDropDown(drpModule, ds.Tables[2]);
                    }
                    //if (ds.Tables[3] != null)
                    //{
                    //    cmfn.BindDropDown(drpModule, ds.Tables[3]);
                    //}
                    if (ds.Tables[4] != null)
                    {
                        cmfn.BindDropDown(drpField, ds.Tables[4]);
                    }
                }
                else
                {
                    DataSet ds = dal.GetQueryReports(ACTION:"2", InvId: drpSite.SelectedItem.Value);
                    if (ds.Tables[0] != null)
                    {
                        cmfn.BindDropDown(drpPatient, ds.Tables[0]);
                    }
                    if (ds.Tables[1] != null)
                    {
                        cmfn.BindDropDown(drpVisit, ds.Tables[1]);
                    }
                    if (ds.Tables[2] != null)
                    {
                        cmfn.BindDropDown(drpModule, ds.Tables[2]);
                    }
                    //if (ds.Tables[3] != null)
                    //{
                    //    cmfn.BindDropDown(drpModule, ds.Tables[3]);
                    //}
                    if (ds.Tables[4] != null)
                    {
                        cmfn.BindDropDown(drpField, ds.Tables[4]);
                    }
                }
            }
        }
        protected void drpPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = dal.GetQueryReports(ACTION: "3", InvId: drpSite.SelectedItem.Value, SubjId: drpPatient.SelectedItem.Value);
            if (ds.Tables[0] != null)
            {
                cmfn.BindDropDown(drpVisit, ds.Tables[0]);
            }
            if (ds.Tables[1] != null)
            {
                cmfn.BindDropDown(drpModule, ds.Tables[1]);
            }
            //if (ds.Tables[2] != null)
            //{
            //    cmfn.BindDropDown(drpModule, ds.Tables[2]);
            //}
            if (ds.Tables[3] != null)
            {
                cmfn.BindDropDown(drpField, ds.Tables[3]);
            }
        }
        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet ds = dal.GetQueryReports(ACTION: "4", InvId: drpSite.SelectedItem.Value, SubjId: drpPatient.SelectedItem.Value, VisitNumber: drpVisit.SelectedItem.Value);
            if (ds.Tables[0] != null)
            {
                cmfn.BindDropDown(drpModule, ds.Tables[0]);
            }
            //if (ds.Tables[1] != null)
            //{
            //    cmfn.BindDropDown(drpModule, ds.Tables[1]);
            //}
            if (ds.Tables[2] != null)
            {
                cmfn.BindDropDown(drpField, ds.Tables[2]);
            }
        }

        //protected void drpPage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet ds = dal.GetQueryReports("5", drpSite.SelectedItem.Value, drpPatient.SelectedItem.Value, drpVisit.SelectedItem.Value, drpModule.SelectedItem.Value, "", "", "");
        //    if (ds.Tables[0] != null)
        //    {
        //        cmfn.BindDropDown(drpModule, ds.Tables[0]);
        //    }
        //    if (ds.Tables[1] != null)
        //    {
        //        cmfn.BindDropDown(drpField, ds.Tables[1]);
        //    }
        //}
        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = dal.GetQueryReports(ACTION: "6", InvId: drpSite.SelectedItem.Value, SubjId: drpPatient.SelectedItem.Value, VisitNumber: drpVisit.SelectedItem.Value, Page: drpModule.SelectedItem.Value, Module:  drpModule.SelectedItem.Value);
            //if (ds.Tables[0] != null)
            //{
            //    cmfn.BindDropDown(drpField, ds.Tables[0]);
            //}

             ds = dal.GetQueryReports("5", drpSite.SelectedItem.Value, drpPatient.SelectedItem.Value, drpVisit.SelectedItem.Value, drpModule.SelectedItem.Value, "", "", "");
             if (ds.Tables[1] != null)
             {
                 cmfn.BindDropDown(drpField, ds.Tables[1]);
             }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            DataSet ds = dal.GetQueryReports(ACTION: "8", InvId: drpSite.SelectedItem.Value, SubjId: drpPatient.SelectedItem.Value, VisitNumber: drpVisit.SelectedItem.Value, Module: drpModule.SelectedItem.Value, Field: drpField.SelectedItem.Value, User_ID: Session["User_ID"].ToString());
            if (ds.Tables[0] != null)
            {
                grdQueryDetailReports.Visible = true;
                grdQueryDetailReports.DataSource = ds.Tables[0];
                grdQueryDetailReports.DataBind();           }


        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            drpSite.ClearSelection();
            drpVisit.ClearSelection();
            //drpPage.ClearSelection();
            drpPatient.ClearSelection();
            drpModule.ClearSelection();
            drpQueryStatus.ClearSelection();
            drpField.ClearSelection();

            grdQueryDetailReports.Visible = false;

        }

        protected void Chk_Select_All_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Int16 i;
                if (Chk_Select_All.Checked)
                {
                    for (i = 0; i < grdQueryDetailReports.Rows.Count; i++)
                    {
                        CheckBox ChAction;
                        ChAction = (CheckBox)grdQueryDetailReports.Rows[i].FindControl("Chk_CloseYN");

                        ChAction.Checked = true;
                    }
                }
                else
                {

                    for (i = 0; i < grdQueryDetailReports.Rows.Count; i++)
                    {
                        CheckBox ChAction;
                        ChAction = (CheckBox)grdQueryDetailReports.Rows[i].FindControl("Chk_CloseYN");

                        ChAction.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }
        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 i;
                for (i = 0; i < grdQueryDetailReports.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)grdQueryDetailReports.Rows[i].FindControl("Chk_CloseYN");

                    if (ChAction.Checked)
                    {
                        string ID = ((TextBox)grdQueryDetailReports.Rows[i].FindControl("Txt_ID")).Text;
                        DataSet ds = dal.GetQueryReports(ACTION: "9", QUERYID: ID, QRYRESBY: Session["User_ID"].ToString());
                    }
                }
                Response.Write("<script> alert('Record Updated successfully.');window.location='DM_QueryClose.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }
    }
}