using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class Forgot_Password : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        DAL dal = new DAL();
        CommonFunction cf = new CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string script = "";
                if (hdnActivity.Value == "EMAILID")
                {
                    string UsersResult = GET_USERS();

                    switch (UsersResult)
                    {
                        case "No user found":
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'No user found.', 'warning');", true);
                            divEmailID.Visible = true;
                            divUsers.Visible = false;
                            divSecurity.Visible = false;

                            break;

                        case "Multiple users found":
                            hdnActivity.Value = "USERS";
                            divEmailID.Visible = false;
                            divUsers.Visible = true;
                            divSecurity.Visible = false;

                            break;

                        case "Single user found":
                            hdnActivity.Value = "SECURITY";
                            divEmailID.Visible = false;
                            divUsers.Visible = false;
                            divSecurity.Visible = true;
                            GET_SECURITY();

                            break;
                    }
                }
                else if (hdnActivity.Value == "USERS")
                {
                    hdnActivity.Value = "SECURITY";
                    divEmailID.Visible = false;
                    divUsers.Visible = false;
                    divSecurity.Visible = true;
                    GET_SECURITY();
                }
                else if (hdnActivity.Value == "SECURITY")
                {
                    string securityResult = CHECK_SECURITY();
                    if (securityResult == "PASS")
                    {
                        DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "Forgot_Password", User_ID: ddlUsers.SelectedValue);
                        USER_RESEND_PWD_MAIL_FIRST(ddlUsers.SelectedValue, txtEmailID.Text);
                        USER_RESEND_PWD_MAIL_SECOND(ddlUsers.SelectedValue, txtEmailID.Text);
                         script = @"
                                swal({
                                title: 'Success!',
                                text: 'Password reset successfully.',
                                icon: 'success',
                                button: 'OK'
                                 }).then((value) => {
                                        window.location.href = 'LoginPage.aspx'; 
                            });";

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    }
                    else if (securityResult == "SECURITY QUESTION LOCKED")
                    {
                         script = @"
                                swal({
                                title: 'Warning!',
                                text: 'Invalid answer, Your security question has been locked. Please contact administrator.',
                                icon: 'warning',
                                button: 'OK'
                                 }).then((value) => {
                                        window.location.href = 'LoginPage.aspx'; 
                            });";

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    }
                    else
                    {
                        txtSECURITY_ANS.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid Answer.', 'warning');", true);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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

                    DataSet dsEmail = new DataSet();
                    EMAILIDS = User_EmailID;
                    SUBJECT = "Resend Password";
                    dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "RESEND_PASSWORD_FIRST");

                    if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                    {
                        SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                        if (ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString() != "")
                        {
                            SUBJECT = SUBJECT.Replace("[PROJECTID]", ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString());
                        }
                        if (ds1.Tables[0].Rows[0]["User_Name"].ToString() != "")
                        {
                            SUBJECT = SUBJECT.Replace("[USERNAME]", ds1.Tables[0].Rows[0]["User_Name"].ToString());
                        }
                        SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                        if (ds1.Tables[0].Rows[0]["TimeZone_Value"] != null)
                        {
                            SUBJECT = SUBJECT.Replace("[TIMEZONE]", ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString());
                        }

                        BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                        if (ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString() != "")
                        {
                            BODY = BODY.Replace("[PROJECTID]", ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString());
                        }
                        BODY = BODY.Replace("[USERID]", USERID);
                        if (ds1.Tables[0].Rows[0]["User_Name"].ToString() != "")
                        {
                            BODY = BODY.Replace("[USERNAME]", ds1.Tables[0].Rows[0]["User_Name"].ToString());
                        }
                        BODY = BODY.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                        if (ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString() != "")
                        {
                            BODY = BODY.Replace("[TIMEZONE]", ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString());
                        }
                    }
                }

                MAIL_SEND(EMAILIDS, CCEMAILIDS, BCCEMAILIDS, SUBJECT, BODY);

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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

                    DataSet ds = new DataSet();
                    DataSet dsEmail = new DataSet();
                    EMAILIDS = User_EmailID;
                    SUBJECT = "Resend Password";
                    dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "RESEND_PASSWORD_SECOND");

                    if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                    {
                        SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                        if (ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString() != "")
                        {
                            SUBJECT = SUBJECT.Replace("[PROJECTID]", ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString());
                        }
                        if (ds1.Tables[0].Rows[0]["User_Name"].ToString() != "")
                        {
                            SUBJECT = SUBJECT.Replace("[USERNAME]", ds1.Tables[0].Rows[0]["User_Name"].ToString());
                        }
                        SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                        if (ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString() != "")
                        {
                            SUBJECT = SUBJECT.Replace("[TIMEZONE]", ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString());
                        }

                        BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                        BODY = BODY.Replace("[PWD]", PWD);
                        BODY = BODY.Replace("[USERID]", USERID);
                        if (ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString() != "")
                        {
                            BODY = BODY.Replace("[PROJECTID]", ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString());
                        }
                        if (ds1.Tables[0].Rows[0]["User_Name"].ToString() != "")
                        {
                            BODY = BODY.Replace("[USERNAME]", ds1.Tables[0].Rows[0]["User_Name"].ToString());
                        }
                        BODY = BODY.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                        if (ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString() != "")
                        {
                            BODY = BODY.Replace("[TIMEZONE]", ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString());
                        }
                    }
                }

                MAIL_SEND(EMAILIDS, CCEMAILIDS, BCCEMAILIDS, SUBJECT, BODY);

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private string GET_USERS()
        {
            string Result = "";
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                        ACTION: "GET_USER_ByEmail",
                        EmailID: txtEmailID.Text
                        );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlUsers.Items.Clear();
                    ddlUsers.DataSource = ds;
                    ddlUsers.DataTextField = "FullName";
                    ddlUsers.DataValueField = "UserID";
                    ddlUsers.DataBind();

                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        Result = "Multiple users found";
                        ddlUsers.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                    else
                    {
                        Result = "Single user found";
                    }
                }
                else
                {
                    Result = "No user found";
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
            return Result;
        }

        private void GET_SECURITY()
        {
            try
            {
                DataSet dsQue = dal_UMT.UMT_SECURITY_QUES_SP(ACTION: "GET_SECURITY_QUE", UserID: ddlUsers.SelectedValue);

                if (dsQue.Tables.Count > 0 && dsQue.Tables[0].Rows.Count > 0)
                {
                    lblSECURITY_QUE.Text = dsQue.Tables[0].Rows[0]["SECURITY_QUE_decrypt"].ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private string CHECK_SECURITY()
        {
            string Result = "";
            try
            {
                DataSet dsRes = dal_UMT.UMT_SECURITY_QUES_SP(ACTION: "CHECK_SECURITY", UserID: ddlUsers.SelectedValue, SECURITY_ANS: txtSECURITY_ANS.Text);

                if (dsRes.Tables.Count > 0 && dsRes.Tables[0].Rows.Count > 0)
                {
                    Result = dsRes.Tables[0].Rows[0]["RESULT"].ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
            return Result;
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            txtSECURITY_ANS.Text = "";
            Response.Redirect("LoginPage.aspx");
        }
    }
}