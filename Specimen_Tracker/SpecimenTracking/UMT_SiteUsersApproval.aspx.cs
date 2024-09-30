using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SpecimenTracking.App_Code;
using System.Web.UI.HtmlControls;

namespace SpecimenTracking
{
    public partial class UMT_SiteUsersApproval : System.Web.UI.Page
    {
        DAL_UMT dAL_UMT = new DAL_UMT();
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                if (!IsPostBack)
                {
                    GET_SITE_USER_DATA();
                }

            }
            catch (Exception ex) 
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITE_USER_DATA()
        {
            try
            {
                DataSet ds = dAL_UMT.UMT_SITE_USER_SP(
                  ACTION: "GET_SITE_USER_DATA",
                  ID: Request.QueryString["ID"].ToString()
                  );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblSiteID.Text = ds.Tables[0].Rows[0]["SiteID"].ToString();
                    lblStudyRole.Text = ds.Tables[0].Rows[0]["StudyRole"].ToString();
                    lblFirstName.Text = ds.Tables[0].Rows[0]["Fname"].ToString();
                    lblLastName.Text = ds.Tables[0].Rows[0]["Lname"].ToString();
                    lblEmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                    lblContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    lblUnblined.Text = ds.Tables[0].Rows[0]["Blind"].ToString();
                    hdnUserID.Value = ds.Tables[0].Rows[0]["UserID"].ToString();
                    GET_SYSTEM_DETAILS();
                }
                else
                {
                    lblSiteID.Text = "";
                    lblStudyRole.Text = "";
                    lblFirstName.Text = "";
                    lblLastName.Text = "";
                    lblEmailID.Text = "";
                    lblContactNo.Text = "";
                    lblUnblined.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SYSTEM_DETAILS()
        {
            try
            {
                DAL dal = new DAL();

                DataSet ds = dAL_UMT.UMT_SITE_USER_SP(
                  ACTION: "GET_SYSTEM",
                  User_ID: hdnUserID.Value
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

                    RepeaterItem item = e.Item;

                    Label lblSystemName = (Label)e.Item.FindControl("lblSystemName");

                    HtmlGenericControl divSubsysIWRS = e.Item.FindControl("divSubsysIWRS") as HtmlGenericControl;
                    HtmlGenericControl divSubSysPharma = e.Item.FindControl("divSubSysPharma") as HtmlGenericControl;
                    HtmlGenericControl divsubsysDM = e.Item.FindControl("divsubsysDM") as HtmlGenericControl;
                    HtmlGenericControl divsubsyseSource = e.Item.FindControl("divsubsyseSource") as HtmlGenericControl;

                    CheckBox ChkSubsysIWRS = (CheckBox)e.Item.FindControl("ChkSubsysIWRS");
                    CheckBox ChkSubSysPharma = (CheckBox)e.Item.FindControl("ChkSubSysPharma");
                    CheckBox chksubsysDM = (CheckBox)e.Item.FindControl("chksubsysDM");
                    CheckBox ChksubsyseSource = (CheckBox)e.Item.FindControl("ChksubsyseSource");
                    CheckBox chlReadOnlyeSource = (CheckBox)e.Item.FindControl("chlReadOnlyeSource");
                    HiddenField HiddenSubSytem = (HiddenField)e.Item.FindControl("HiddenSubSytem");

                    DataRowView rowView = e.Item.DataItem as DataRowView;
                    if (rowView != null)
                    {
                        string IsSelected = rowView["IsSelected"].ToString();

                        if (IsSelected == "True")
                        {
                            ChkSelect.Checked = true;
                            if (lblSystemName.Text == "IWRS")
                            {
                                divSubsysIWRS.Visible = true;
                                if (HiddenSubSytem.Value == "Unblinding")
                                {
                                    ChkSubsysIWRS.Checked = true;
                                }
                                else
                                {
                                    divSubsysIWRS.Visible = false;
                                }
                            }                  
                        }
                        else
                        {
                            ChkSelect.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void UPDATE_SITE_USER_APPROVAL()
        {
            try
            {
                string USERNAME = lblFirstName.Text + " " + lblLastName.Text;
                string STUDYROLE = lblStudyRole.Text;
                string APPROVAL_ACTION = drpAction.SelectedValue;
                string APPROVAL_COMMENT = txtComment.Text;
                DataSet ds = dAL_UMT.UMT_SITE_USER_SP(
                                   ACTION: "UPDATE_SITE_USER_APPROVAL",
                                   ID: Request.QueryString["ID"].ToString(),
                                   ReviewAct: APPROVAL_ACTION,
                                   ReviewComm: APPROVAL_COMMENT
                                  );
                if (drpAction.SelectedValue == "Approve")
                {
                    SEND_MAIL_APPROVAL_DISAPPROVAL(USERNAME, STUDYROLE, APPROVAL_ACTION, APPROVAL_COMMENT);

                    Response.Write("<script> alert('Site User Details Approved Successfully.'); window.location.href = 'UMT_SiteUsersList.aspx';</script>");
                }
                if (drpAction.SelectedValue == "Disapprove")
                {
                    SEND_MAIL_APPROVAL_DISAPPROVAL(USERNAME, STUDYROLE, APPROVAL_ACTION, APPROVAL_COMMENT);
                    Response.Write("<script> alert('Site User Details Disapproved Successfully.'); window.location.href = 'UMT_SiteUsersList.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL_APPROVAL_DISAPPROVAL(string USERNAME, string STUDYROLE, string APPROVAL_ACTION, string APPROVAL_COMMENT)
        {
            try
            {
                CommonFunction cf = new CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal.UMT_EMAIL_SP(ACTION: "GET_SITE_USER_APPROVAL", ACTIONS: "Site User Approval");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }
                DataSet dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "SITE_USER_APPROVAL");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USER_NAME]", USERNAME);
                    SUBJECT = SUBJECT.Replace("[STUDYROLE]", STUDYROLE);
                    SUBJECT = SUBJECT.Replace("[APPROVAL_ACTION]", APPROVAL_ACTION);
                    SUBJECT = SUBJECT.Replace("[APPROVAL_COMMENT]", APPROVAL_COMMENT);
                    SUBJECT = SUBJECT.Replace("[APPROVED_BY]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[USER_NAME]", USERNAME);
                    BODY = BODY.Replace("[STUDYROLE]", STUDYROLE);
                    BODY = BODY.Replace("[APPROVAL_ACTION]", APPROVAL_ACTION);
                    BODY = BODY.Replace("[APPROVAL_COMMENT]", APPROVAL_COMMENT);
                    BODY = BODY.Replace("[APPROVED_BY]", Session["User_Name"].ToString());
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

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_SITE_USER_APPROVAL();
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
                drpAction.SelectedIndex = 0;
                txtComment.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}