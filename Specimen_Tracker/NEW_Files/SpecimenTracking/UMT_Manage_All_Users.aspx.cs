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
    public partial class UMT_Manage_All_Users : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        DAL dal = new DAL();
        CommonFunction cf = new CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GET_USERTYPE();
                GET_STUDYROLE();
            }
        }

        private void GET_USERTYPE()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                        ACTION: "GET_USERTYPE"
                        );

                DrpUsertype.DataSource = ds.Tables[0];
                DrpUsertype.DataTextField = "UserType";
                DrpUsertype.DataBind();
                DrpUsertype.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_STUDYROLE()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                        ACTION: "GET_ROLE"
                        );

                DrpStudyROLE.DataSource = ds.Tables[0];
                DrpStudyROLE.DataTextField = "StudyRole";
                DrpStudyROLE.DataBind();
                DrpStudyROLE.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUsers_PreRender(object sender, EventArgs e)
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

        protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    Label ACTIVEBYNAME = (e.Row.FindControl("ACTIVEBYNAME") as Label);
                    Label ACTIVE_CAL_DAT = (e.Row.FindControl("ACTIVE_CAL_DAT") as Label);
                    Label ACTIVE_CAL_TZDAT = (e.Row.FindControl("ACTIVE_CAL_TZDAT") as Label);

                    Label DEACTIVEBYNAME = (e.Row.FindControl("DEACTIVEBYNAME") as Label);
                    Label DEACTIVE_CAL_DAT = (e.Row.FindControl("DEACTIVE_CAL_DAT") as Label);
                    Label DEACTIVE_CAL_TZDAT = (e.Row.FindControl("DEACTIVE_CAL_TZDAT") as Label);

                    Label lblID = (e.Row.FindControl("lblID") as Label);

                    LinkButton lbtActive = (e.Row.FindControl("lbtActive") as LinkButton);
                    LinkButton lbtDeactive = (e.Row.FindControl("lbtDeactive") as LinkButton);

                    LinkButton lbtLock = (e.Row.FindControl("lbtLock") as LinkButton);
                    LinkButton lbtUnlock = (e.Row.FindControl("lbtUnlock") as LinkButton);

                    LinkButton lbtnReqforActi = (e.Row.FindControl("lbtnReqforActi") as LinkButton);
                    LinkButton lbtnReqForDactive = (e.Row.FindControl("lbtnReqForDactive") as LinkButton);

                    LinkButton lbresendPassword = (e.Row.FindControl("lbresendPassword") as LinkButton);
                    LinkButton lblResetQuestion = (e.Row.FindControl("lblResetQuestion") as LinkButton);

                    LinkButton lbtnGenReqPass = (e.Row.FindControl("lbtnGenReqPass") as LinkButton);
                    LinkButton lbtnGenReqSecQues = (e.Row.FindControl("lbtnGenReqSecQues") as LinkButton);
                    LinkButton LbtnUNLOCK = (e.Row.FindControl("LbtnUNLOCK") as LinkButton);

                    HiddenField Active = (HiddenField)e.Row.FindControl("HiddenActive");
                    HiddenField Lock = (HiddenField)e.Row.FindControl("HiddenLOCK");
                    HiddenField SECURITY = (HiddenField)e.Row.FindControl("HiddenSECURITY");

                    HiddenField ACTIVATION_Pending = (HiddenField)e.Row.FindControl("ACTIVATION_Pending");
                    HiddenField DEACTIVATION_Pending = (HiddenField)e.Row.FindControl("DEACTIVATION_Pending");
                    HiddenField UNLOCK_Pending = (HiddenField)e.Row.FindControl("UNLOCK_Pending");
                    HiddenField RESET_PASSWORD_Pending = (HiddenField)e.Row.FindControl("RESET_PASSWORD_Pending");
                    HiddenField SECURITY_QUESTION_Pending = (HiddenField)e.Row.FindControl("SECURITY_QUESTION_Pending");

                    if (dr["SYSTEM_COUNTS"].ToString() == "0")
                    {
                        lbtActive.Visible = false;
                        lbtDeactive.Visible = false;

                        lbtLock.Visible = false;
                        lbtUnlock.Visible = false;

                        lbtnReqforActi.Visible = false;
                        lbtnReqForDactive.Visible = false;

                        lbresendPassword.Visible = false;
                        lblResetQuestion.Visible = false;

                        lbtnGenReqPass.Visible = false;
                        lbtnGenReqSecQues.Visible = false;
                        LbtnUNLOCK.Visible = false;
                    }
                    else
                    {
                        if (Active.Value == "True")
                        {
                            if (Convert.ToInt32(DEACTIVATION_Pending.Value) > 0)
                            {
                                lbtnReqforActi.Visible = false;
                                lbtnReqForDactive.Visible = true;
                                lbtDeactive.Visible = false;
                                lbtActive.Visible = false;
                            }
                            else
                            {
                                lbtDeactive.Visible = false;
                                lbtActive.Visible = true;
                                lbtnReqforActi.Visible = false;
                                lbtnReqForDactive.Visible = false;
                            }

                            ACTIVEBYNAME.Visible = true;
                            ACTIVE_CAL_DAT.Visible = true;
                            ACTIVE_CAL_TZDAT.Visible = true;
                            DEACTIVEBYNAME.Visible = false;
                            DEACTIVE_CAL_DAT.Visible = false;
                            DEACTIVE_CAL_TZDAT.Visible = false;
                        }
                        else
                        {
                            if (Convert.ToInt32(ACTIVATION_Pending.Value) > 0)
                            {
                                lbtDeactive.Visible = false;
                                lbtActive.Visible = false;
                                lbtnReqforActi.Visible = true;
                                lbtnReqForDactive.Visible = false;
                            }
                            else
                            {
                                lbtDeactive.Visible = true;
                                lbtActive.Visible = false;
                                lbtnReqforActi.Visible = false;
                                lbtnReqForDactive.Visible = false;
                            }

                            ACTIVEBYNAME.Visible = false;
                            ACTIVE_CAL_DAT.Visible = false;
                            ACTIVE_CAL_TZDAT.Visible = false;
                            DEACTIVEBYNAME.Visible = true;
                            DEACTIVE_CAL_DAT.Visible = true;
                            DEACTIVE_CAL_TZDAT.Visible = true;
                        }

                        if (Lock.Value == "True")
                        {
                            lbtLock.Visible = true;
                            lbtUnlock.Visible = false;
                            LbtnUNLOCK.Visible = false;
                        }
                        else
                        {
                            if (Convert.ToInt32(UNLOCK_Pending.Value) > 0)
                            {
                                lbtLock.Visible = false;
                                lbtUnlock.Visible = false;
                                LbtnUNLOCK.Visible = true;
                            }
                            else
                            {
                                lbtLock.Visible = false;
                                lbtUnlock.Visible = true;
                                LbtnUNLOCK.Visible = false;
                            }
                        }

                        if (Convert.ToInt32(RESET_PASSWORD_Pending.Value) > 0)
                        {
                            lbresendPassword.Visible = false;
                            lbtnGenReqPass.Visible = true;
                        }
                        else
                        {
                            lbresendPassword.Visible = true;
                            lbtnGenReqPass.Visible = false; ;
                        }

                        if (Convert.ToInt32(SECURITY_QUESTION_Pending.Value) > 0)
                        {
                            lblResetQuestion.Visible = false;
                            lbtnGenReqSecQues.Visible = true;
                        }
                        else
                        {
                            lblResetQuestion.Visible = true;
                            lbtnGenReqSecQues.Visible = false; ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string User_EmailID = "";
                string id = e.CommandArgument.ToString();
                ViewState["ID"] = id;
                string CommandName = e.CommandName.ToString();

                LinkButton lnk = (e.CommandSource) as LinkButton;
                GridViewRow clickedrow = lnk.NamingContainer as GridViewRow;
                User_EmailID = (clickedrow.FindControl("lblEmail") as Label).Text;
                string USERNAME = (clickedrow.FindControl("lblName") as Label).Text;
                string STUDYROLE = (clickedrow.FindControl("lblRole") as Label).Text;
                string USERID = (clickedrow.FindControl("lblUSerId") as Label).Text;

                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                switch (CommandName)
                {
                    case ("ACTIVATION"):
                        ACTIVATION(id);
                        GET_ALL_USERS();
                        USER_ACTIVATION_MAIL_FIRST(User_EmailID, USERNAME, STUDYROLE, USERID);
                        USER_ACTIVATION_MAIL_SECOND(USERID, User_EmailID);
                        USER_ACTIVATION_MAIL_THIRD(USERID, User_EmailID);
                        break;

                    case ("DEACTIVATION"):
                        DEACTIVATION(id);
                        GET_ALL_USERS();
                        SEND_USER_DEACTIVATIONMAIL(USERNAME, USERID, STUDYROLE, User_EmailID);
                        break;

                    case ("LOCK"):
                        User_LOCK(id);
                        GET_ALL_USERS();
                        SEND_UNLOCKMAIL(USERNAME, USERID, STUDYROLE, User_EmailID);
                        USER_RESEND_PWD_MAIL_SECOND(USERID, User_EmailID);
                        break;

                    case ("ReSet_Question"):
                        ReSet_Question(id);
                        GET_ALL_USERS();
                        SEND_RESET_QUESTIONMAIL(USERNAME, STUDYROLE, User_EmailID);
                        break;

                    case ("Resend_Password"):
                        Resend_Password(id);
                        USER_RESEND_PWD_MAIL_FIRST(USERID, User_EmailID);
                        USER_RESEND_PWD_MAIL_SECOND(USERID, User_EmailID);
                        GET_ALL_USERS();

                        break;

                    case ("ShowRoles"):
                        ShowRoles(e.CommandArgument.ToString());
                        modalRoles.Show();
                        break;

                    case ("ChangeRoles"):
                        Response.Redirect("UMT_AssignRoleAct.aspx?UserID=" + e.CommandArgument.ToString() + "&Type=Mng");
                        break;

                    default:

                        break;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void USER_RESEND_PWD_MAIL_FIRST(string USERID, string User_EmailID)
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
                dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "RESEND_PASSWORD_FIRST");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
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

        private void USER_ACTIVATION_MAIL_FIRST(string User_EmailID, string USERNAME, string STUDYROLE, string USERID)
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "", ACTIONS = "", SYSTEM_LIST = "", URL = "";
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
                URL = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace(Request.RawUrl.ToString(), "/Login.aspx");
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
                    BODY = BODY.Replace("[URL]", URL);
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

                DataSet ds = new DataSet();
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

        private void SEND_RESET_QUESTIONMAIL(string USERNAME, string STUDYROLE, string User_EmailID)
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "", ACTIONS = "";
                ACTIONS = "Reset Security Question";
                ds = dal.UMT_EMAIL_SP(ACTION: "GET_USER_UNLOCK", ACTIONS: ACTIONS);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }
                SUBJECT = "Reset Security Question";
                EMAILIDS = EMAILIDS + "," + User_EmailID;
                dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "RESET_QUESTION");

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

                MAIL_SEND(EMAILIDS, CCEMAILIDS, BCCEMAILIDS, SUBJECT, BODY);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_UNLOCKMAIL(string USERNAME, string USERID, string STUDYROLE, string User_EmailID)
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "", ACTIONS = "";
                ACTIONS = "User Unlock";
                ds = dal.UMT_EMAIL_SP(ACTION: "GET_USER_UNLOCK", ACTIONS: ACTIONS);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }
                EMAILIDS = EMAILIDS + "," + User_EmailID;
                SUBJECT = "User Unlock";
                dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "USER_UNLOCK");

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

        private void ShowRoles(string UserID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "Show_UserRoles", User_ID: UserID);
                grdUserRoles.DataSource = ds.Tables[0];
                grdUserRoles.DataBind();
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

        private void ACTIVATION(string id)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "User_ACTIVATE", ID: id);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Activated Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DEACTIVATION(string id)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "User_DEACTIVATE", ID: id);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Deactivated Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void User_LOCK(string id)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "User_LOCK", ID: id);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Unlocked Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ReSet_Question(string id)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "ReSet_Question", ID: id);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Reset Security Question Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Resend_Password(string id)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "Resend_Password", ID: id);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Password Send Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lblUserDetailsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string header = "All Users Details";

                DataSet ds = dal_UMT.UMT_USERS_SP(
                    ACTION: "GET_ALL_USERS"
                    );

                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnGetDetails_Click(object sender, EventArgs e)
        {
            try
            {
                GET_ALL_USERS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ALL_USERS()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                    ACTION: "GET_ALL_USERS", UserType: DrpUsertype.SelectedValue, StudyRole: DrpStudyROLE.SelectedValue, Fname: txtName.Text, EmailID: TxtEmail.Text, ContactNo: txtContact.Text, RequestStatus: DrpStatus.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
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

    }
}