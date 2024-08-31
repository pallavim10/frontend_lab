using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SpecimenTracking.App_Code;

namespace SpecimenTracking
{
    public partial class UMT_UserSites : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BIND_USER();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(
                        ACTION: "GET_USERS"
                        );

                DrpUser.DataSource = ds.Tables[0];
                DrpUser.DataTextField = "FirstName";
                DrpUser.DataValueField = "UserID";
                DrpUser.DataBind();
                DrpUser.Items.Insert(0, new ListItem("--Select User--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITEID_SITES()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(
                    ACTION: "GET_SITEID_SITES",
                    User_ID: DrpUser.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    grdUsers.DataSource = ds.Tables[0];
                    grdUsers.DataBind();


                }
                else
                {
                    grdUsers.DataSource = null;
                    grdUsers.DataBind();


                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ADDED_USERS_SITES()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(
                    ACTION: "GET_ADDED_USERS_SITES",
                    User_ID: DrpUser.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    grdAddedUsers.DataSource = ds.Tables[0];
                    grdAddedUsers.DataBind();
                }
                else
                {
                    grdAddedUsers.DataSource = null;
                    grdAddedUsers.DataBind();
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DrpUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DrpUser.SelectedValue != "0")
                {
                    GET_SITEID_SITES();
                    GET_ADDED_USERS_SITES();
                }
                else
                {
                    grdUsers.DataSource = null;
                    grdUsers.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddFunctionName_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < grdUsers.Rows.Count; i++)
                {
                    CheckBox ChkSITEID = (CheckBox)grdUsers.Rows[i].FindControl("ChkSITEID");

                    if (ChkSITEID.Checked == true)
                    {
                        Label lblSiteID = (Label)grdUsers.Rows[i].FindControl("lblSiteID");

                        DataSet ds = dal_UMT.UMT_SITE_SP(
                             ACTION: "INSERT_UMT_USERS_SITE",
                             User_ID: DrpUser.SelectedValue,
                             SiteID: lblSiteID.Text
                          );

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Site Assigned Successfully');", true);

                    }
                }
                GET_SITEID_SITES();
                GET_ADDED_USERS_SITES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemoveFunctionName_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < grdAddedUsers.Rows.Count; i++)
                {
                    CheckBox chkAddedSiteID = (CheckBox)grdAddedUsers.Rows[i].FindControl("chkAddedSiteID");

                    if (chkAddedSiteID.Checked == true)
                    {
                        Label lblSiteID = (Label)grdAddedUsers.Rows[i].FindControl("lblSiteID");

                        DataSet ds = dal_UMT.UMT_SITE_SP(
                             ACTION: "DELETED_ADDED_USERS_SITES",
                             SiteID: lblSiteID.Text
                          );

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Site Removed Successfully');", true);

                    }
                }
                GET_SITEID_SITES();
                GET_ADDED_USERS_SITES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Assigned Sites";

                DataSet ds = dal_UMT.UMT_REPORT_SP(ACTION: "GET_USER_SITES");
                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}