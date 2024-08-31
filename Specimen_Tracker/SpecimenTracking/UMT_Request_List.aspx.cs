using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpecimenTracking.App_Code;

namespace SpecimenTracking
{
    public partial class UMT_Request_List : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        DAL dal = new DAL();
        CommonFunction cf = new CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    GET_USERS_REQUESTS();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_USERS_REQUESTS()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                     ACTION: "GET_USERS_REQUESTS"
                     );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    gvUserRequests.DataSource = ds.Tables[0];
                    gvUserRequests.DataBind();
                }
                else
                {
                    gvUserRequests.DataSource = null;
                    gvUserRequests.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvUserRequesr_PreRender(object sender, EventArgs e)
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

        protected void lbUsersRequestsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string header = "Request Users Details";

                DataSet ds = dal_UMT.UMT_USERS_SP(
                    ACTION: "GET_USERS_REQUESTS"
                    );


                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvUserRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField HiddenStatus = (HiddenField)e.Row.FindControl("HiddenStatus");

                    Label lblStatus = (e.Row.FindControl("lblStatus") as Label);
                    LinkButton lbtnReqforActi = (e.Row.FindControl("lbtnReqforActi") as LinkButton);
                    LinkButton lbtnReqForDactive = (e.Row.FindControl("lbtnReqForDactive") as LinkButton);
                    LinkButton lbtnGenReqPass = (e.Row.FindControl("lbtnGenReqPass") as LinkButton);
                    LinkButton lbtnGenReqSecQues = (e.Row.FindControl("lbtnGenReqSecQues") as LinkButton);
                    LinkButton lbtUnlock = (e.Row.FindControl("lbtUnlock") as LinkButton);
                    Label lblRequesttype = (e.Row.FindControl("lblRequesttype") as Label);

                    if (Convert.ToInt32(HiddenStatus.Value) == 0)
                    {
                        lblStatus.Text = "Pending";
                    }
                    if (Convert.ToInt32(HiddenStatus.Value) == 1)
                    {
                        lblStatus.Text = "Complete";
                    }
                    if (lblRequesttype.Text == "ACTIVATION")
                    {
                        lbtnReqforActi.Visible = true;
                        lbtnReqForDactive.Visible = false;
                        lbtnGenReqPass.Visible = false;
                        lbtnGenReqSecQues.Visible = false;
                        lbtUnlock.Visible = false;
                    }
                    if (lblRequesttype.Text == "DEACTIVATION")
                    {
                        lbtnReqforActi.Visible = false;
                        lbtnReqForDactive.Visible = true;
                        lbtnGenReqPass.Visible = false;
                        lbtnGenReqSecQues.Visible = false;
                        lbtUnlock.Visible = false;
                    }
                    if (lblRequesttype.Text == "UNLOCK")
                    {
                        lbtnReqforActi.Visible = false;
                        lbtnReqForDactive.Visible = false;
                        lbtnGenReqPass.Visible = false;
                        lbtnGenReqSecQues.Visible = false;
                        lbtUnlock.Visible = true;
                    }
                    if (lblRequesttype.Text == "RESET PASSWORD")
                    {
                        lbtnReqforActi.Visible = false;
                        lbtnReqForDactive.Visible = false;
                        lbtnGenReqPass.Visible = true;
                        lbtnGenReqSecQues.Visible = false;
                        lbtUnlock.Visible = false;
                    }
                    if (lblRequesttype.Text == "SECURITY QUESTION")
                    {
                        lbtnReqforActi.Visible = false;
                        lbtnReqForDactive.Visible = false;
                        lbtnGenReqPass.Visible = false;
                        lbtnGenReqSecQues.Visible = true;
                        lbtUnlock.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvUserRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                string PWD = "";
                Label USERNAME = (Label)gvUserRequests.Rows[index].FindControl("lblUserName");
                Label USERID = (Label)gvUserRequests.Rows[index].FindControl("lblSiteId");
                Label STUDYROLE = (Label)gvUserRequests.Rows[index].FindControl("lblStudyRole");
                Label User_EmailID = (Label)gvUserRequests.Rows[index].FindControl("lblEmail");

                if (e.CommandName == "ACTIVATION")
                {
                    ACTIVATION(ID);
                    USER_ACTIVATION_MAIL_FIRST(User_EmailID.Text, USERNAME.Text, STUDYROLE.Text, USERID.Text);
                    USER_ACTIVATION_MAIL_SECOND(USERID.Text, User_EmailID.Text);
                    USER_ACTIVATION_MAIL_THIRD(USERID.Text, User_EmailID.Text);
                    GET_USERS_REQUESTS();
                }
                else if (e.CommandName == "DEACTIVATION")
                {
                    DEACTIVATION(ID);
                    SEND_USER_DEACTIVATIONMAIL(USERNAME.Text, USERID.Text, STUDYROLE.Text, User_EmailID.Text);
                    GET_USERS_REQUESTS();
                }
                else if (e.CommandName == "UNLOCK")
                {
                    User_LOCK(ID);
                    SEND_MAIL_UNLOCK(USERNAME.Text, USERID.Text, STUDYROLE.Text, User_EmailID.Text);
                    GET_USERS_REQUESTS();
                }
                else if (e.CommandName == "REQUEST_PASSWORD")
                {
                    Resend_Password(ID);
                    USER_RESEND_PWD_MAIL_SECOND(USERID.Text, User_EmailID.Text);
                    GET_USERS_REQUESTS();
                }
                else if (e.CommandName == "REQUEST_QUESTION")
                {
                    ReSet_Question(ID);
                    SEND_MAIL_RESETQUESTION(USERNAME.Text, STUDYROLE.Text, User_EmailID.Text);
                    GET_USERS_REQUESTS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void USER_RESEND_PWD_MAIL_SECOND(string USERID, string User_EmailID)
        {
            try
            {
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "", PWD = "";

                DataSet ds1 = dal_UMT.UMT_USER_DETAILS_SP(USERID);
                if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables.Count > 0)
                {
                    PWD = ds1.Tables[0].Rows[0]["PWD"].ToString();
                }

                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                EMAILIDS = User_EmailID;
                SUBJECT = "Resend Password";
                dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "RESEND_PASSWORD_SECOND");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[PWD]", PWD);
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
        private void USER_ACTIVATION_MAIL_FIRST(string User_EmailID, string USERNAME, string STUDYROLE, string USERID)
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "", ACTIONS = "", SYSTEM_LIST = ""; ;
                ACTIONS = "User Activation / Deactivation";
                ds = dal.UMT_EMAIL_SP(ACTION: "GET_USER_ACTIVATION_DEACTIVATION", ACTIONS: ACTIONS);
                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }

                EMAILIDS += "," + User_EmailID;

                DataSet ds1 = dal_UMT.UMT_USERS_SP(ACTION: "Show_UserRoles", User_ID: USERID);

                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    SYSTEM_LIST += "<li>" + dr["System Name"].ToString() + "</li>";
                }

                SUBJECT = "User Activation";
                dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "USER_ACTIVATION_MAIL_FIRST");

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
                    BODY = BODY.Replace("[URL]", HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace(Request.RawUrl.ToString(), "/Auth.aspx"));
                    BODY = BODY.Replace("[SYSTEM_LIST]", SYSTEM_LIST);
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
        private void USER_ACTIVATION_MAIL_SECOND(string USERID, string User_EmailID)
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                SUBJECT = "User Activation";
                EMAILIDS = User_EmailID;
                dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "USER_ACTIVATION_MAIL_SECOND");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USERID]", USERID);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[USERID]", USERID);
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

        private void USER_ACTIVATION_MAIL_THIRD(string USERID, string User_EmailID)
        {
            try
            {
                string PWD = "";
                DataSet ds1 = dal_UMT.UMT_USER_DETAILS_SP(USERID);
                if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables.Count > 0)
                {
                    PWD = ds1.Tables[0].Rows[0]["PWD"].ToString();
                }

                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                EMAILIDS = User_EmailID;
                SUBJECT = "User Activation";
                dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "USER_ACTIVATION_MAIL_THIRD");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[PWD]", PWD);
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

        private void SEND_USER_DEACTIVATIONMAIL(string USERNAME, string USERID, string STUDYROLE, string User_EmailID)
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "", ACTIONS = "";
                ACTIONS = "User Activation / Deactivation";
                ds = dal.UMT_EMAIL_SP(ACTION: "GET_USER_ACTIVATION_DEACTIVATION", ACTIONS: ACTIONS);
                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }

                SUBJECT = "User Deactivation";
                EMAILIDS = User_EmailID + "," + EMAILIDS;
                dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "USER_DEACTIVATION");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USERNAME]", USERNAME);
                    SUBJECT = SUBJECT.Replace("[USERID]", USERID);
                    SUBJECT = SUBJECT.Replace("[STUDYROLE]", STUDYROLE);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[USERNAME]", USERNAME);
                    BODY = BODY.Replace("[USERID]", USERID);
                    BODY = BODY.Replace("[STUDYROLE]", STUDYROLE);
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

        private void SEND_MAIL_RESETQUESTION(string USERNAME, string STUDYROLE, string User_EmailID)
        {
            try
            {
                CommonFunction cf = new CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal.UMT_EMAIL_SP(ACTION: "GET_SECURITY_QUESTION", ACTIONS: "Reset Security Question");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }
                EMAILIDS = User_EmailID + "," + EMAILIDS;
                DataSet dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "RESET_QUESTION");

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
                    BODY = BODY.Replace("[USERNAME]", Session["User_Name"].ToString());
                    BODY = BODY.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    BODY = BODY.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());
                }
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
        private void SEND_MAIL_UNLOCK(string USERNAME, string USERID, string STUDYROLE, string User_EmailID)
        {
            try
            {
                CommonFunction cf = new CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal.UMT_EMAIL_SP(ACTION: "GET_USER_UNLOCK", ACTIONS: "User Unlock");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }
                EMAILIDS = User_EmailID + "," + EMAILIDS;
                DataSet dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "USER_UNLOCK");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USERNAME]", USERNAME);
                    SUBJECT = SUBJECT.Replace("[USERID]", USERID);
                    SUBJECT = SUBJECT.Replace("[STUDYROLE]", STUDYROLE);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[USERNAME]", USERNAME);
                    BODY = BODY.Replace("[USERID]", USERID);
                    BODY = BODY.Replace("[STUDYROLE]", STUDYROLE);
                    BODY = BODY.Replace("[USERNAME]", Session["User_Name"].ToString());
                    BODY = BODY.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    BODY = BODY.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());
                }
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
        private void ACTIVATION(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "ACTIVATE", ID: ID
                   );
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Activated Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DEACTIVATION(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "DEACTIVATE", ID: ID
                   );
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Deactivated Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void User_LOCK(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "LOCK", ID: ID
                   );
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Locked Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void ReSet_Question(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "QUESTION", ID: ID
                   );
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Reset Security Question Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Resend_Password(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "PASSWORD", ID: ID
                   );
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Password Send Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}