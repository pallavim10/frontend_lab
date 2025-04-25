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
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class NIWRS_GEN_UNBLIND_REQ : System.Web.UI.Page
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

                    GetSites();
                    GET_SUBSITE();
                    GetSubject();
                    GET_REASON();
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SelectOther();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetSites()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        drpSites.DataSource = ds.Tables[0];
                        drpSites.DataValueField = "INVNAME";
                        drpSites.DataBind();
                    }
                    else
                    {
                        drpSites.DataSource = ds.Tables[0];
                        drpSites.DataValueField = "INVNAME";
                        drpSites.DataBind();
                        drpSites.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_REASON()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_OPTION", STRATA: "UNBLIND_REASON");
                drpReason.DataSource = ds.Tables[0];
                drpReason.DataValueField = "TEXT";
                drpReason.DataBind();
                drpReason.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBSITE()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_SUBSITE", SITEID: drpSites.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        drpSubSites.DataSource = ds.Tables[0];
                        drpSubSites.DataValueField = "SubSiteID";
                        drpSubSites.DataTextField = "SubSiteID";
                        drpSubSites.DataBind();
                    }
                    else
                    {
                        drpSubSites.DataSource = ds.Tables[0];
                        drpSubSites.DataValueField = "SubSiteID";
                        drpSubSites.DataTextField = "SubSiteID";
                        drpSubSites.DataBind();
                        drpSubSites.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
                else
                {
                    drpSubSites.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetSubject()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UNBLIND_SP(ACTION: "GET_BLIND_SUBJECTS", SITEID: drpSites.SelectedValue, SUBSITEID: drpSubSites.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        drpSUBJID.DataSource = ds.Tables[0];
                        drpSUBJID.DataTextField = "SUB_RAND";
                        drpSUBJID.DataValueField = "SUBJID";
                        drpSUBJID.DataBind();
                    }
                    else
                    {
                        drpSUBJID.DataSource = ds.Tables[0];
                        drpSUBJID.DataTextField = "SUB_RAND";
                        drpSUBJID.DataValueField = "SUBJID";
                        drpSUBJID.DataBind();
                        drpSUBJID.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
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
                if (drpReason.SelectedValue == "Others" && txtReason.Text == "")
                {
                    Response.Write("<script> alert('Please Specify Others. '); </script>");
                }
                else
                {
                    GEN_REQ();

                    SEND_MAIL();

                    Response.Write("<script> alert('Unblinding Request Generated Successfully. '); window.location.href = '" + Request.RawUrl.ToString() + "'; </script>");
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

        protected void drpSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBSITE();
                GetSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSubSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GEN_REQ()
        {
            try
            {
                DAL_UMT dal_UMT = new DAL_UMT();

                string EMAILID = "";
                DataSet ds = dal_UMT.UMT_USER_DETAILS_SP(USERID: Session["User_ID"].ToString());

                EMAILID = ds.Tables[0].Rows[0]["Email_ID"].ToString().Trim();

                if (drpReason.SelectedValue == "Others")
                {
                    dal_IWRS.IWRS_UNBLIND_SP(
                    ACTION: "GEN_UNBLIND_REQ",
                    CONDITION1: txtReason.Text,
                    ENTEREDBY: Session["User_ID"].ToString(),
                    FIELDNAME: EMAILID,
                    SUBJID: drpSUBJID.SelectedValue
                        );
                }
                else
                {
                    dal_IWRS.IWRS_UNBLIND_SP(
                    ACTION: "GEN_UNBLIND_REQ",
                    CONDITION1: drpReason.SelectedValue,
                    ENTEREDBY: Session["User_ID"].ToString(),
                    FIELDNAME: EMAILID,
                    SUBJID: drpSUBJID.SelectedValue
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL()
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();


                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS_SITE", STRATA: "UNBLIND", SITEID: drpSites.SelectedValue);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "Unblinding_Request_Generated");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[SITEID]", drpSites.SelectedValue);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[SITEID]", drpSites.SelectedValue);
                    BODY = BODY.Replace("[SCREENINGID]", drpSUBJID.SelectedValue);
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