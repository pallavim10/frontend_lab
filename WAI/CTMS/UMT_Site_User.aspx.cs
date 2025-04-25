using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;


namespace CTMS
{
    public partial class UMT_Site_User : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SITE_USER();
                    BIND_STUDYROLE();
                    GET_SITEID();
                    GET_SYSTEM("");
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GET_SYSTEM(string UserID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_SYSTEM",
                    User_ID: UserID
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    repeatSystem.DataSource = ds.Tables[0];
                    repeatSystem.DataBind();
                }
                else
                {
                    repeatSystem.DataSource = null;
                    repeatSystem.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void INSERT_SYSTEM()
        {
            try
            {
                for (int i = 0; i < repeatSystem.Items.Count; i++)
                {
                    CheckBox ChkSelect = (CheckBox)repeatSystem.Items[i].FindControl("ChkSelect");

                    Label lblSystemID = (Label)repeatSystem.Items[i].FindControl("lblSystemID");

                    Label lblSystemName = (Label)repeatSystem.Items[i].FindControl("lblSystemName");

                    if (ChkSelect.Checked)
                    {
                        DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                                        ACTION: "INSERT_SYSTEM",
                                        User_ID: hdnID.Value,
                                        SystemID: lblSystemID.Text,
                                        SystemName: lblSystemName.Text
                                        );

                    }
                    else
                    {
                        DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                                           ACTION: "DELETE_SYSTEM",
                                           SystemID: lblSystemID.Text,
                                           SystemName: lblSystemName.Text,
                                           User_ID: hdnID.Value
                                           );
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void BIND_STUDYROLE()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                          ACTION: "GET_STUDYROLE"
                          );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpStudyRole.DataSource = ds.Tables[0];
                    drpStudyRole.DataTextField = "StudyRole";
                    drpStudyRole.DataValueField = "StudyRole";
                    drpStudyRole.DataBind();
                    drpStudyRole.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void GET_SITEID()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_SITE_ID"
                    );

                drpSiteID.DataSource = ds.Tables[0];
                drpSiteID.DataValueField = "SiteID";
                drpSiteID.DataTextField = "SiteID";
                drpSiteID.DataBind();
                drpSiteID.Items.Insert(0, new ListItem("--Select Site Id--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }
        private void GET_SITE_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_SITE_USER"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdSiteUser.DataSource = ds.Tables[0];
                    grdSiteUser.DataBind();
                }
                else
                {
                    grdSiteUser.DataSource = null;
                    grdSiteUser.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void INSERT_SITE_USER_DATA()
        {
            if (txtFirstName.Text.Trim() == "")
            {
                Response.Write("<script language=javascript>alert('Please Enter First Name');</script>");
                return;
            }

            if (txtLastName.Text.Trim() == "")
            {
                Response.Write("<script language=javascript>alert('Please Enter Last Name');</script>");
                return;
            }

            if (txtContactNo.Text.Trim() == "")
            {
                Response.Write("<script language=javascript>alert('Please Enter Contact No');</script>");
                return;
            }

            DataSet da = dal_UMT.UMT_SITE_USER_SP(
                                        ACTION: "INSERT_SITE_USER",
                                        Fname: txtFirstName.Text,
                                        Lname: txtLastName.Text,
                                        EmailID: txtEmailid.Text,
                                        ContactNo: txtContactNo.Text,
                                        Blind: drpUnblind.SelectedValue,
                                        StudyRole: drpStudyRole.Text,
                                        SiteID: drpSiteID.SelectedValue
                                    );

            hdnID.Value = da.Tables[2].Rows[0]["SITE_USER_ID"].ToString();

            Response.Write("<script> alert('Create Site User Successfully'); window.location.href = 'UMT_Site_User.aspx';</script>");

            GET_SITE_USER();
            CLEAR();

        }
        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_SITE_USER_DATA();
                INSERT_SYSTEM();
                GET_SITE_USER();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void UPDATE_SITE_USER()
        {
            try
            {
                if (txtFirstName.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter First Name');</script>");
                    return;
                }

                if (txtLastName.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Last Name');</script>");
                    return;
                }

                if (txtContactNo.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Contact No');</script>");
                    return;
                }

                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                             ACTION: "UPDATE_SITE_USER",
                             ID: ViewState["ID"].ToString(),
                             Fname: txtFirstName.Text,
                             Lname: txtLastName.Text,
                             EmailID: txtEmailid.Text,
                             ContactNo: txtContactNo.Text,
                             Blind: drpUnblind.SelectedValue,
                             StudyRole: drpStudyRole.Text,
                             SiteID: drpSiteID.SelectedValue
                            );

                hdnID.Value = ViewState["SiteUserID"].ToString();


                Response.Write("<script> alert('User Updated Successfully'); window.location.href = 'UMT_Site_User.aspx';</script>");

                INSERT_SYSTEM();
                GET_SITE_USER();
                CLEAR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_SITE_USER();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void grdUserDetails_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
                {
                    //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gv.ShowFooter == true && gv.Rows.Count > 0)
                {
                    //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                    gv.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }
        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;


                Label lblUserID = row.FindControl("lblUserID") as Label;

                string SiteUserID = lblUserID.Text.ToString();

                ViewState["SiteUserID"] = SiteUserID;

                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;
                if (e.CommandName == "EIDIT")
                {
                    EDIT_SITE_USER(ID);
                    GET_SYSTEM(SiteUserID);
                    lbtnSubmit.Visible = false;
                    lbnUpdate.Visible = true;
                }
                else if (e.CommandName == "DELETED")
                {
                    DELETE_SITE_USER(ID);
                    GET_SITE_USER();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void DELETE_SITE_USER(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                  ACTION: "DELETE_SITE_USER",
                  ID: ID
                  );

                DataSet dsSystem = dal_UMT.UMT_USERS_SP(
                                         ACTION: "DELETE_USERID_AGIANS_SYSTEM",
                                         User_ID: ViewState["SiteUserID"].ToString()
                                         );


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User deleted Successfully')", true);
                GET_SITE_USER();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void EDIT_SITE_USER(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                               ACTION: "EDIT_SITE_USER",
                               ID: ID
                           );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtFirstName.Text = ds.Tables[0].Rows[0]["Fname"].ToString();
                    txtLastName.Text = ds.Tables[0].Rows[0]["Lname"].ToString();
                    txtEmailid.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                    txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    drpUnblind.SelectedValue = ds.Tables[0].Rows[0]["Blind"].ToString();
                    drpStudyRole.SelectedValue = ds.Tables[0].Rows[0]["StudyRole"].ToString();
                    drpSiteID.SelectedValue = ds.Tables[0].Rows[0]["SiteID"].ToString();
                }
                else
                {
                    grdSiteUser.DataSource = null;
                    grdSiteUser.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                lbtnSubmit.Visible = true;
                lbnUpdate.Visible = false;
                CLEAR();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CLEAR()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmailid.Text = "";
            txtContactNo.Text = "";
            drpStudyRole.SelectedIndex = 0;
            drpUnblind.SelectedIndex = 0;
            drpSiteID.SelectedIndex = 0;

        }
        protected void repeatSystem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DAL dal;
                    dal = new DAL();

                    DataRowView row = (DataRowView)e.Item.DataItem;

                    CheckBox ChkSelect = (CheckBox)e.Item.FindControl("ChkSelect");
                    //string IsSelected = dr["IsSelected"].ToString();

                    RepeaterItem item = e.Item;

                    ////Reference the Controls.
                    //string IsSelected = (item.FindControl("IsSelected") as Label).Text;

                    //string IsSelected = e.Item.DataItem as string;

                    DataRowView rowView = e.Item.DataItem as DataRowView;
                    if (rowView != null)
                    {
                        string IsSelected = rowView["IsSelected"].ToString();

                        if (IsSelected == "True")
                        {
                            ChkSelect.Checked = true;
                        }
                        else
                        {
                            ChkSelect.Checked = false;
                        }
                    }


                    //if (IsSelected == "True")
                    //{
                    //    ChkSelect.Checked = true;
                    //}
                    //else
                    //{
                    //    ChkSelect.Checked = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}