using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CTMS
{
    public partial class NIWRS_REVIEW : System.Web.UI.Page
    {

        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_CONFIGURATION_REVIEW", QUECODE: "CONFIGREVIEW");

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["ANS"].ToString() == "Review")
                        {
                            lbtnReview.Visible = false;
                            lbtnUnReview.Visible = true;
                        }
                        else
                        {
                            lbtnReview.Visible = true;
                            lbtnUnReview.Visible = false;
                        }
                    }
                    else
                    {
                        lbtnReview.Visible = true;
                        lbtnUnReview.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnReview_Click(object sender, EventArgs e)
        {
            try
            {
                dal_IWRS.IWRS_SET_OPTION_SP(
                    ACTION: "CONFIGURATION_REVIEW",
                    ANS: "Review",
                    QUECODE: "CONFIGREVIEW",
                    QUESTION: "Configuration Review");

                string MSG = "Configuration has been Reviewed and will not be available for Editing.";
                string ACTIONTYPE = "Reviewed";
                string STATUS = "Freeze";
                SEND_MAIL(ACTIONTYPE, STATUS, "SETUP_REVIEW_FREEZE","SETUP_REVIEW", MSG);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Configuration has been Reviewed and will not be available for Editing.');window.location='NIWRS_REVIEW.aspx'", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUnReview_Click(object sender, EventArgs e)
        {
            try
            {
                dal_IWRS.IWRS_SET_OPTION_SP(
                    ACTION: "CONFIGURATION_REVIEW",
                    ANS: "Unreview",
                    QUECODE: "CONFIGREVIEW",
                    QUESTION: "Configuration Review");

                string MSG = "Configuration has been opened for Editing.";
                string ACTIONTYPE = "Unreviewed";
                string STATUS = "Unfreeze";
                SEND_MAIL(ACTIONTYPE, STATUS, "SETUP_UNREVIEW_FREEZE", "SETUP_UNREVIEW", MSG);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Configuration has been opened for Editing.');window.location='NIWRS_REVIEW.aspx'", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL(string ACTIONTYPE, string STATUS, string EMAIL_CODE,string EMAIL_TYPE,string MSG)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: EMAIL_TYPE);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: EMAIL_CODE);

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COMMENT]", MSG);
                    SUBJECT = SUBJECT.Replace("[ACTION]", ACTIONTYPE);
                    SUBJECT = SUBJECT.Replace("[STATUS]", STATUS);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COMMENT]", MSG);
                    BODY = BODY.Replace("[ACTION]", ACTIONTYPE);
                    BODY = BODY.Replace("[STATUS]", STATUS);
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
    }
}