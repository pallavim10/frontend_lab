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
    public partial class CTMS_CreateMonitSchedule : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["PROJECTID"] != null)
                    {
                        Drp_Project.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTID"].ToString()));
                        fill_Inv();
                        fill_Section();
                    }
                    else
                    {
                        fill_Project();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void fill_Project()
        {
            try
            {
                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Specific_Project",
                Project_ID: Convert.ToInt32(Session["PROJECTID"]),
                ENTEREDBY: Session["User_ID"].ToString()
                );
                Drp_Project.DataSource = ds.Tables[0];
                Drp_Project.DataValueField = "Project_ID";
                Drp_Project.DataTextField = "PROJNAME";
                Drp_Project.DataBind();
                Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void fill_Inv()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSiteID(
                Action: "INVID",
                PROJECTID: Session["PROJECTID"].ToString(),
                User_ID: Session["User_ID"].ToString()
                );
                drp_InvID.DataSource = ds.Tables[0];
                drp_InvID.DataValueField = "INVNAME";
                drp_InvID.DataBind();
                drp_InvID.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }


        private void fill_Section()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_VISITTYPE_MASTER"
                );

                drpVisitType.DataSource = ds.Tables[0];
                drpVisitType.DataValueField = "ID";
                drpVisitType.DataTextField = "VISIT_NAME";
                drpVisitType.DataBind();
                drpVisitType.Items.Insert(0, new ListItem("--Select Visit Type--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string NewvisitNo = "";
                if (chkUnSchedualVisit.Checked == true)
                {
                    NewvisitNo = "UNS";
                }
                //else
                //{
                //    NewvisitNo = Convert.ToString(Convert.ToInt32(Session["visitno"]) + 1);
                //}

                DAL dal;
                dal = new DAL();
                dal.GetSetMonitoringVisit(
                Action: "CREATE_MV_SCHEDULE",
                PROJECTID: Drp_Project.SelectedItem.Value,
                INVID: drp_InvID.SelectedValue,
                VISITTYPE: drpVisitType.SelectedItem.Value,
                Frequency: txtFrequency.Text,
                VSTDAT: txtStartdate.Text.Trim(),
                ENTEREDBY: Session["User_ID"].ToString(),
                VISITNO: NewvisitNo
                );
                GetRecords();
                //Response.Write("<script> alert('Record Updated successfully.');window.location='CTMS_CreateMonitSchedule.aspx'; </script>");

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Drp_Project_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_Inv();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GetRecords()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetMonitoringVisit(
                Action: "GET_MV_SCHEDULE",
                PROJECTID: Drp_Project.SelectedItem.Value,
                INVID: drp_InvID.SelectedItem.Value,
                VISITTYPE: drpVisitType.SelectedItem.Value
                );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["visitno"] = ds.Tables[0].Rows.Count;
                    grdData.DataSource = ds;
                    grdData.DataBind();
                    divaddvisit.Visible = true;
                }
                else
                {
                    grdData.DataSource = null;
                    grdData.DataBind();
                    divaddvisit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drp_InvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetRecords();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpVisitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetRecords();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnaddvisit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFrequency.Text != "0")
                {
                    string NewvisitNo = "";
                    if (chkUnSchedualVisit.Checked == true)
                    {
                        NewvisitNo = "UNS";
                    }

                    dal.GetSetMonitoringVisit(
                    Action: "CREATE_NEW_MV_SCHEDULE",
                    PROJECTID: Drp_Project.SelectedItem.Value,
                    INVID: drp_InvID.SelectedValue,
                    VISITTYPE: drpVisitType.SelectedItem.Value,
                    Frequency: txtFrequency.Text,
                    VSTDAT: txtStartdate.Text.Trim(),
                    ENTEREDBY: Session["User_ID"].ToString(),
                    VISITNO: NewvisitNo
                    );
                    GetRecords();
                    txtFrequency.Text = "";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter more then 0 days')", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        int visitno=Convert.ToInt32(e.CommandArgument);
        //        string Newvisitno=Convert.ToString(visitno+1);
        //        if (e.CommandName == "AddVisit")
        //        {
        //            try
        //            {
        //                DAL dal;
        //                dal = new DAL();
        //                dal.GetSetMonitoringVisit(
        //                Action: "CREATE_NEW_MV_SCHEDULE",
        //                PROJECTID: Drp_Project.SelectedItem.Value,
        //                INVID: drp_InvID.SelectedValue,
        //                VISITTYPE: drpVisitType.SelectedItem.Value,
        //                Frequency: txtFrequency.Text,
        //                NoofVisit: txtNoofVisit.Text,
        //                ENTEREDBY: Session["User_ID"].ToString(),
        //                VISITNO:Newvisitno
        //                );
        //                GetRecords();
        //            }
        //            catch (Exception ex)
        //            {
        //                lblErrorMsg.Text = ex.Message.ToString();
        //            }
        //        }
        //        else if (e.CommandName == "insertnewvisit")
        //        {
        //            GetRecords();
        //        }
        //    }
        //    catch (Exception ex)
        //    { 

        //    }
        //}

        //protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            DataRowView dr = e.Row.DataItem as DataRowView;
        //            TextBox txtvisitdays = (TextBox)e.Row.FindControl("txtvisitdays");
        //            LinkButton lbtnaddvisitdays = (LinkButton)e.Row.FindControl("lbtnaddvisitdays");
        //            GridView grd=(GridView)sender;
        //            int visitcount = grd.Rows.Count;
        //            if (e.Row.RowIndex == (Convert.ToInt32(Session["visitcount"]) - 1))
        //            {
        //                //last row
        //                txtvisitdays.Visible = true;
        //                lbtnaddvisitdays.Visible = false;
        //            }
        //            else
        //            {
        //                txtvisitdays.Visible = false;
        //                lbtnaddvisitdays.Visible = false;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    { 

        //    }
        //}

    }
}