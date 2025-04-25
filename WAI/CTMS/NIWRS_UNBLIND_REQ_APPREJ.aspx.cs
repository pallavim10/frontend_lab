using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class NIWRS_UNBLIND_REQ_APPREJ : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtReason.Attributes.Add("MaxLength", "200");
            try
            {
                if (!IsPostBack)
                {
                    SUBJECTTEXT.Text = Session["SUBJECTTEXT"].ToString();
                    GETDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GETDATA()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UNBLIND_SP(ACTION: "GET_UNBLIND_REQ_LIST_DATA", SUBJID: Request.QueryString["ID"].ToString());
                DataRow dr = ds.Tables[0].Rows[0];

                lblSite.Text = dr["SITEID"].ToString();
                lblSubSite.Text = dr["SUBSITEID"].ToString();
                lblSUB_RAND.Text = dr["SUB_RAND"].ToString();
                lblReason.Text = dr["UNBLIND_REQ_REASN"].ToString();
                lblREQDT.Text = dr["UNLIND_REQ_DT"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpDecision.SelectedValue == "Approved")
                {
                    ModalPopupExtender2.Show();
                }
                else
                {
                    Approved_Unblinded_Req();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            string str = Convert.ToString(txt_UserName.Text);
            string pass = Convert.ToString(txt_Pwd.Text);
            string compid = Session["User_ID"].ToString();
            ViewState["pass"] = pass;

            if (chkINVSIGN.Checked == true)
            {
                if (str == compid)
                {
                    ViewState["User_IDP"] = str;
                    Check_AUTH();
                }
                else
                {
                    txt_UserName.Text = "";
                    txt_Pwd.Text = "";
                    chkINVSIGN.Checked = false;
                    ModalPopupExtender2.Show();
                    Response.Write("<script>alert('Please Enter Valid User ID')</script>");
                }
            }
            else
            {
                txt_UserName.Text = "";
                txt_Pwd.Text = "";
                chkINVSIGN.Checked = false;
                ModalPopupExtender2.Show();
                Response.Write("<script>alert('Please Sign on Checkbox')</script>");
                txt_Pwd.Text = ViewState["pass"].ToString();
            }

        }

        protected void Approved_Unblinded_Req()
        {
            try
            {
                dal_IWRS.IWRS_UNBLIND_SP(
                ACTION: "STATUS_UNBLIND_REQ",
                CONDITION1: txtReason.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                SUBJID: Request.QueryString["ID"].ToString(),
                STATUSNAME: drpDecision.SelectedValue
                );


                if (drpDecision.SelectedValue == "Approved")
                {
                    SEND_MAIL_APPROVE();

                    Response.Write("<script> alert('Unblinding Request Approved Successfully. '); window.location.href = 'NIWRS_UNBLIND_REQ_LIST.aspx'; </script>");
                }
                else
                {
                    SEND_MAIL_DISAPPROVE();

                    Response.Write("<script> alert('Unblinding Request Disapproved Successfully. '); window.location.href = 'NIWRS_UNBLIND_REQ_LIST.aspx'; </script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        private void Check_AUTH()
        {
            try
            {
                DAL_UMT dal_UMT = new DAL_UMT();

                DataSet dsAuth = dal_UMT.UMT_Auth(UserID: txt_UserName.Text, Pwd: txt_Pwd.Text);

                if (dsAuth.Tables.Count > 0 && dsAuth.Tables[0].Rows.Count > 0)
                {
                    string RESULT = dsAuth.Tables[0].Rows[0][0].ToString();

                    switch (RESULT)
                    {
                        case "Account Locked":
                            Response.Write("<script> alert('Your account has been locked'); window.location='Login.aspx';  </script>");

                            break;

                        case "Invalid Credentials, Account Locked":
                            Response.Write("<script> alert('Invalid credentials, Your account has been locked'); window.location='Login.aspx';  </script>");

                            break;

                        case "Invalid Credentials":
                            Response.Write("<script> alert('Invalid credentials');</script>");
                            ModalPopupExtender2.Show();

                            break;

                        case "Invalid User ID":
                            Response.Write("<script> alert('Invalid User ID');</script>");
                            ModalPopupExtender2.Show();

                            break;

                        default:
                            Approved_Unblinded_Req();

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelREQ_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NIWRS_GEN_UNBLIND_REQ.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL_APPROVE()
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS_SITE", STRATA: "UNBLIND", SITEID: lblSite.Text);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "Unblinding_Request_Approved");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[SITEID]", lblSite.Text);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[SITEID]", lblSite.Text);
                    BODY = BODY.Replace("[SCREENINGID]", lblSUB_RAND.Text);
                    BODY = BODY.Replace("[RANDNO]", lblSUB_RAND.Text);
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

        private void SEND_MAIL_DISAPPROVE()
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS_SITE", STRATA: "UNBLIND", SITEID: lblSite.Text);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "Unblinding_Request_Disapproved");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[SITEID]", lblSite.Text);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[SITEID]", lblSite.Text);
                    BODY = BODY.Replace("[SCREENINGID]", lblSUB_RAND.Text);
                    BODY = BODY.Replace("[RANDNO]", lblSUB_RAND.Text);
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