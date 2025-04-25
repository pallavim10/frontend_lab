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
    public partial class LabData_Default_Data : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillINV();
                    GET_TestName();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.GetSiteID(
            Action: "INVID",
            PROJECTID: Session["PROJECTID"].ToString(),
            User_ID: Session["User_ID"].ToString()
            );
            drpInvid.DataSource = ds.Tables[0];
            drpInvid.DataValueField = "INVNAME";
            drpInvid.DataBind();
            drpInvid.Items.Insert(0, new ListItem("--Select INVID--", "0"));
        }

        public void GET_Lab()
        {
            try
            {
                DataSet ds = dal.LAB_MASTER_SP(Action: "GET_Lab", INVID: drpInvid.SelectedValue);
                drpLabID.DataSource = ds.Tables[0];
                drpLabID.DataValueField = "Lab_ID";
                drpLabID.DataTextField = "Lab_Name";
                drpLabID.DataBind();
                drpLabID.Items.Insert(0, new ListItem("--Select Lab--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_TestName()
        {
            try
            {
                DataSet ds1 = dal.Lab_Data_SP(Action: "GET_TestName");
                drpTests.DataSource = ds1.Tables[0];
                drpTests.DataValueField = "ID";
                drpTests.DataTextField = "TestName";
                drpTests.DataBind();
                drpTests.Items.Insert(0, new ListItem("--Select Test--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_Items()
        {
            try
            {
                DataSet ds = dal.Lab_Data_SP(Action: "TestItems_WithoutData", TestID: drpTests.SelectedValue);
                gvItems.DataSource = ds.Tables[0];
                gvItems.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_Items_Data(string ID)
        {
            try
            {
                Session["LAB_RECID"] = ID;
                DataSet ds = dal.Lab_Data_SP(Action: "TestItems_WithData", TestID: drpTests.SelectedValue, INVID: drpInvid.SelectedValue, LabID: drpLabID.SelectedValue, RECID: ID);
                gvItems.DataSource = ds.Tables[0];
                gvItems.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void INSERT_Data()
        {
            try
            {
                DataSet ds = dal.Lab_Data_SP(Action: "GET_NEW_RECID", INVID: drpInvid.SelectedValue, LabID: drpLabID.SelectedValue, TestID: drpTests.SelectedValue);
                string RECID = ds.Tables[0].Rows[0]["NEW_RECID"].ToString();

                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    TextBox txtData = (TextBox)gvItems.Rows[i].FindControl("txtData");
                    Label lblItemID = (Label)gvItems.Rows[i].FindControl("lblItemID");

                    dal.Lab_Data_SP(Action: "INSERT_Data", RECID: RECID, DATA: txtData.Text, ItemID: lblItemID.Text, LabID: drpLabID.SelectedValue, INVID: drpInvid.SelectedValue, TestID: drpTests.SelectedValue);
                }

                GET_Items();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void UPDATE_Data()
        {
            try
            {
                string RECID = Session["LAB_RECID"].ToString();

                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    TextBox txtData = (TextBox)gvItems.Rows[i].FindControl("txtData");
                    Label lblItemID = (Label)gvItems.Rows[i].FindControl("lblItemID");

                    dal.Lab_Data_SP(Action: "UPDATE_Data", RECID: RECID, DATA: txtData.Text, ItemID: lblItemID.Text, LabID: drpLabID.SelectedValue, INVID: drpInvid.SelectedValue, TestID: drpTests.SelectedValue);
                }

                Clear();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DELETE_DATA(string ID)
        {
            try
            {
                dal.Lab_Data_SP(Action: "DELETE_DATA", RECID: ID, INVID: drpInvid.SelectedValue, LabID: drpLabID.SelectedValue, TestID: drpTests.SelectedValue);
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_DATA()
        {
            try
            {
                DataSet ds = dal.Lab_Data_SP(Action: "GET_DATA", TestID: drpTests.SelectedValue, INVID: drpInvid.SelectedValue, LabID: drpLabID.SelectedValue);
                if (ds.Tables.Count > 0)
                {
                    grdLabData.DataSource = ds.Tables[0];
                    grdLabData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Clear()
        {
            try
            {
                btnsubmitLabData.Visible = true;
                btnupdateLabData.Visible = false;
                GET_Items();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }




        protected void lbtnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label REC_ID = (Label)row.FindControl("lblRECID");

                GET_Items_Data(REC_ID.Text);

                btnupdateLabData.Visible = true;
                btnsubmitLabData.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void lbtndelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label REC_ID = (Label)row.FindControl("lblRECID");

                DELETE_DATA(REC_ID.Text);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void drpInvid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_Lab();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLabID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_Items();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitLabData_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_Data();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateLabData_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_Data();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelLabData_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdLabData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grdLabData.HeaderRow.Cells[2].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
        }
    }
}