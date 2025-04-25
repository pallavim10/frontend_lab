using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CTMS.CommonFunction;
using PPT;
using System.Text;

namespace CTMS
{
    public partial class UMT_AssignRoleAct : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        DAL dal = new DAL();
        CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_ASSIGNROLE_ACT_USER_DATA();
                    GET_SYSTEM_ROLES_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ASSIGNROLE_ACT_USER_DATA()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                  ACTION: "GET_ASSIGNROLE_ACT_USER_DATA",
                  ID: Request.QueryString["UserID"].ToString()
                  );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblSiteID.Text = ds.Tables[0].Rows[0]["SiteID"].ToString();
                    lblCompany.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                    lblStudyRole.Text = ds.Tables[0].Rows[0]["StudyRole"].ToString();
                    lblFirstName.Text = ds.Tables[0].Rows[0]["Fname"].ToString();
                    lblLastName.Text = ds.Tables[0].Rows[0]["Lname"].ToString();
                    lblEmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                    lblContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    lblUnblined.Text = ds.Tables[0].Rows[0]["Blind"].ToString();
                    lblUserType.Text = ds.Tables[0].Rows[0]["UserType"].ToString();
                    lblNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
                    hdnACTIVE.Value = ds.Tables[0].Rows[0]["ACTIVE"].ToString();
                    hdnUserName.Value = ds.Tables[0].Rows[0]["User_Name"].ToString();

                    if (lblUserType.Text == "Site")
                    {
                        divSite.Visible = true;
                    }
                    else if (lblUserType.Text == "External")
                    {
                        divExternal.Visible = true;
                        lblDiv.Text = "Company Name";
                    }
                    else if (lblUserType.Text == "Sponsor")
                    {
                        divExternal.Visible = true;
                        lblDiv.Text = "Sponsor Company Name";
                    }
                }
                else
                {
                    lblSiteID.Text = "";
                    lblCompany.Text = "";
                    lblStudyRole.Text = "";
                    lblFirstName.Text = "";
                    lblLastName.Text = "";
                    lblEmailID.Text = "";
                    lblContactNo.Text = "";
                    lblUnblined.Text = "";
                    lblUserType.Text = "";
                }
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

        private void GET_SYSTEM_ROLES_DATA()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_BIND_SYSTEM",
                    User_ID: Request.QueryString["UserID"].ToString()
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdAssignRolesAct.DataSource = ds.Tables[0];
                    grdAssignRolesAct.DataBind();
                }
                else
                {
                    grdAssignRolesAct.DataSource = null;
                    grdAssignRolesAct.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdAssignRolesAct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string SystemID = dr["SystemID"].ToString();
                    string Exist = dr["Exist"].ToString();

                    DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                      ACTION: "GET_SYSTEM_ROLES",
                      SystemID: SystemID,
                      Blind: lblUnblined.Text
                      );

                    DropDownList drpRoles = (DropDownList)e.Row.FindControl("drpRoles");

                    drpRoles.DataSource = ds.Tables[0];
                    drpRoles.DataValueField = "ID";
                    drpRoles.DataTextField = "RoleName";
                    drpRoles.DataBind();
                    drpRoles.Items.Insert(0, new ListItem("-Select-", "0"));

                    if (dr["RoleID"].ToString() != "")
                    {
                        drpRoles.SelectedValue = dr["RoleID"].ToString();
                    }

                    if (Exist == "" || Exist == "0")
                    {
                        drpRoles.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool sendEmail = false;
                bool revoke_sendemail = false;

                for (int i = 0; i < grdAssignRolesAct.Rows.Count; i++)
                {
                    DropDownList drpRoles = (DropDownList)grdAssignRolesAct.Rows[i].FindControl("drpRoles");

                    Label lblSystem = (Label)grdAssignRolesAct.Rows[i].FindControl("lblSystem");

                    Label lblRoleID = (Label)grdAssignRolesAct.Rows[i].FindControl("lblRoleID");

                    DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                               ACTION: "UPDATE_UMT_USERS_ACTBY",
                               RoleID: drpRoles.SelectedValue,
                               User_ID: Request.QueryString["UserID"].ToString(),
                               SystemName: lblSystem.Text
                            );

                    if (hdnACTIVE.Value == "True" && drpRoles.SelectedValue == "0")
                    {
                        if (drpRoles.SelectedValue != lblRoleID.Text)
                        {
                            revoke_sendemail = true;
                        }

                    }
                    if (hdnACTIVE.Value == "True" && drpRoles.SelectedValue != lblRoleID.Text)
                    {
                        sendEmail = true;
                    }
                }

                if (revoke_sendemail)
                {
                    SEND_SYSTEMS_REVOKE();
                }
                else
                {
                    if (sendEmail)
                    {
                        SEND_ROLE_CHANGE_EMAIL(lblFirstName.Text + " " + lblLastName.Text, lblStudyRole.Text, Request.QueryString["UserID"].ToString());
                    }
                }



                if (Request.QueryString["Type"] == "Assign")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('User Role Assigned Successfully.'); window.location.href = 'UMT_AssignRoles.aspx' ", true);

                }
                else if (Request.QueryString["Type"] == "Change")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('User Role Updated Successfully.');  window.location.href = 'UMT_ChangeRole.aspx' ", true);

                }
                else if (Request.QueryString["Type"] == "Mng")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('User Role Updated Successfully.');  window.location.href = 'UMT_Manage_All_Users.aspx' ", true);

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void SEND_SYSTEMS_REVOKE()
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string USERNAME = hdnUserName.Value;
                string STUDYROLE = lblStudyRole.Text;
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "", ACTIONS = "", LIST_SYSTEM = "";

                ACTIONS = "User Role Change";

                ds = dal.UMT_EMAIL_SP(ACTION: "GET_USER_ROLE_CHANGE", ACTIONS: ACTIONS);
                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }

                DataSet ds1 = dal_UMT.UMT_USERS_SP(ACTION: "Show_UserRoles", User_ID: Request.QueryString["UserID"].ToString());

                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    LIST_SYSTEM += "<li>" + dr["System Name"].ToString() + "</li>";
                }

                dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "SYSTEM_REVOKE");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USERNAME]", USERNAME);
                    SUBJECT = SUBJECT.Replace("[STUDYROLE]", STUDYROLE);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[USER_NAME]", USERNAME);
                    BODY = BODY.Replace("[STUDYROLE]", STUDYROLE);
                    BODY = BODY.Replace("[SYSTEM_LIST]", LIST_SYSTEM);
                    BODY = BODY.Replace("[USERNAME]", Session["User_Name"].ToString());
                    BODY = BODY.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    BODY = BODY.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());
                }

                MAIL_SEND(EMAILIDS, CCEMAILIDS, BCCEMAILIDS, SUBJECT, BODY);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_ROLE_CHANGE_EMAIL(string USERNAME, string STUDYROLE, string USERID)
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "", ACTIONS = "", LIST_SYSTEM = "", htmlTableBody = "", URL = "";

                ACTIONS = "User Role Change";

                ds = dal.UMT_EMAIL_SP(ACTION: "GET_USER_ROLE_CHANGE", ACTIONS: ACTIONS);
                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }

                DataSet ds1 = dal_UMT.UMT_USERS_SP(ACTION: "Show_User_SYSTEM_ROLE", User_ID: USERID);

                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    htmlTableBody = ConvertDataTableToHtmlBody(ds1.Tables[0]);
                }

                dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "USER_ROLE_ASSIGNED");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USERNAME]", USERNAME);
                    SUBJECT = SUBJECT.Replace("[STUDYROLE]", STUDYROLE);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[USERNAME]", USERNAME);
                    BODY = BODY.Replace("[STUDYROLE]", STUDYROLE);
                    BODY = BODY.Replace("[LIST_SYSTEM]", htmlTableBody);
                    BODY = BODY.Replace("[USERNAME]", Session["User_Name"].ToString());
                    BODY = BODY.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    BODY = BODY.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());
                }

                MAIL_SEND(EMAILIDS, CCEMAILIDS, BCCEMAILIDS, SUBJECT, BODY);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        static string ConvertDataTableToHtmlBody(DataTable dataTable)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<table style='border-collapse: collapse; width: 100%; border: 1px solid black; margin: 5px;'>");

            html.Append("<thead><tr style='border: 1px solid black;'>");

            foreach (DataColumn column in dataTable.Columns)
            {
                html.AppendFormat("<th style='border: 1px solid black; padding: 3px; background-color: #f2f2f2;'>{0}</th>", column.ColumnName);
            }
            html.Append("</tr>");
            html.Append("</thead>");


            html.Append("<tbody>");
            foreach (DataRow row in dataTable.Rows)
            {
                html.Append("<tr style='border: 1px solid black;'>");
                foreach (var cell in row.ItemArray)
                {
                    html.AppendFormat("<td style='border: 1px solid black; padding: 3px;'>{0}</td>", cell);
                }
                html.Append("</tr>");
            }
            html.Append("</tbody>");


            html.Append("</table>");

            return html.ToString();
        }

        private void MAIL_SEND(string EMAILIDS, string CCEMAILIDS, string BCCEMAILIDS, string SUBJECT, string BODY)
        {
            try
            {
                cf.Email_Users(
                    EmailAddress: EMAILIDS,
                    CCEmailAddress: CCEMAILIDS,
                    BCCEmailAddress: BCCEMAILIDS,
                    subject: SUBJECT,
                    body: BODY
                  );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Type"] == "Assign")
            {
                Response.Redirect("UMT_AssignRoles.aspx", false);
            }
            else if (Request.QueryString["Type"] == "Change")
            {
                Response.Redirect("UMT_ChangeRole.aspx", false);
            }
            else if (Request.QueryString["Type"] == "Mng")
            {
                Response.Redirect("UMT_Manage_All_Users.aspx", false);
            }
        }
    }
}