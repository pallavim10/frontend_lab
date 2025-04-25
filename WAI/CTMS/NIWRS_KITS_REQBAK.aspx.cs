using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;

namespace CTMS
{
    public partial class NIWRS_KITS_REQBAK : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblSubject.Text = Request.QueryString["SUBJID"].ToString();
                    lblVisit.Text = Request.QueryString["VISIT"].ToString();
                    lblOldKitNo.Text = Request.QueryString["KITNO"].ToString();
                    hfDispenseIDX.Value = Request.QueryString["DISPENSE_IDX"].ToString();
                    lblNewKitNo.Text = GET_NEW_KIT_BAK(lblSubject.Text, Request.QueryString["KITNO"].ToString());
                    GET_REASON();

                    if (Request.QueryString["Spons"] != null)
                    {
                        if (Request.QueryString["Spons"].ToString() == "Yes")
                        {
                            divSponsorApprov.Visible = true;
                        }
                        else
                        {
                            divSponsorApprov.Visible = false;
                        }
                    }
                }

                Response.Write("<script> SelectOther(); </script>");
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
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_OPTION", STRATA: "BakKit_REASON");
                ddlReason.DataSource = ds.Tables[0];
                ddlReason.DataValueField = "TEXT";
                ddlReason.DataBind();
                ddlReason.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string GET_NEW_KIT_BAK(string SUBJID, string KITNO)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_SP(ACTION: "GET_NEW_KIT_BAK", SUBJID: SUBJID, OLD_KITNO: KITNO);

                if (ds.Tables[0].Rows.Count < 1)
                {
                    Response.Write("<script> alert('Kits Not Available'); window.location.href = 'NIWRS_KITS.aspx?VISIT=" + lblVisit.Text + "&SUBJID=" + lblSubject.Text + "'; </script>");
                }
                else
                {
                    KITNO = ds.Tables[0].Rows[0]["KITNO"].ToString();
                    lblRandNo.Text = ds.Tables[0].Rows[0]["RANDNO"].ToString();
                }

                return KITNO;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

                return KITNO;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Spons"] != null)
                {
                    if (Request.QueryString["Spons"].ToString() == "Yes" && drpSponsorApproval.SelectedValue == "Approved")
                    {
                        if (ddlReason.SelectedValue == "Others")
                        {
                            if (txtReason.Text == "")
                            {
                                Response.Write("<script> alert('Please Specify Others. '); </script>");
                            }
                            else
                            {
                                dal_IWRS.IWRS_KITS_SP
                                    (
                                    ACTION: "INSERT_SPONSOR_DECISION",
                                    SUBJID: lblSubject.Text,
                                    OLD_KITNO: lblOldKitNo.Text,
                                    NEW_KITNO: lblNewKitNo.Text,
                                    ENTEREDBY: Session["User_ID"].ToString(),
                                    REASON: txtReason.Text,
                                    STATUSNAME: drpSponsorApproval.SelectedValue
                                    );

                                dal_IWRS.IWRS_KITS_SP
                                    (
                                    ACTION: "ALLOCATE_BAK_KIT",
                                    SUBJID: lblSubject.Text,
                                    OLD_KITNO: lblOldKitNo.Text,
                                    NEW_KITNO: lblNewKitNo.Text,
                                    ENTEREDBY: Session["User_ID"].ToString(),
                                    REASON: txtReason.Text,
                                    DISPENSE_IDX:hfDispenseIDX.Value
                                    );

                                SEND_MAIL();

                                Response.Write("<script> alert('Kit No. " + lblNewKitNo.Text + " Allocated. '); window.location.href = 'NIWRS_KITS.aspx?VISIT=" + lblVisit.Text + "&SUBJID=" + lblSubject.Text + "'; </script>");
                            }
                        }
                        else
                        {
                            dal_IWRS.IWRS_KITS_SP
                                (
                                ACTION: "INSERT_SPONSOR_DECISION",
                                SUBJID: lblSubject.Text,
                                OLD_KITNO: lblOldKitNo.Text,
                                NEW_KITNO: lblNewKitNo.Text,
                                ENTEREDBY: Session["User_ID"].ToString(),
                                REASON: ddlReason.SelectedItem.Text,
                                STATUSNAME: drpSponsorApproval.SelectedValue
                                );

                            dal_IWRS.IWRS_KITS_SP
                                (
                                ACTION: "ALLOCATE_BAK_KIT",
                                SUBJID: lblSubject.Text,
                                OLD_KITNO: lblOldKitNo.Text,
                                NEW_KITNO: lblNewKitNo.Text,
                                ENTEREDBY: Session["User_ID"].ToString(),
                                REASON: ddlReason.SelectedItem.Text,
                                DISPENSE_IDX: hfDispenseIDX.Value
                                );

                            SEND_MAIL();

                            Response.Write("<script> alert('Kit No. " + lblNewKitNo.Text + " Allocated. '); window.location.href = 'NIWRS_KITS.aspx?VISIT=" + lblVisit.Text + "&SUBJID=" + lblSubject.Text + "'; </script>");
                        }
                    }
                    else if (Request.QueryString["Spons"].ToString() == "Yes" && drpSponsorApproval.SelectedValue == "Disapproved")
                    {
                        if (ddlReason.SelectedValue == "Others")
                        {
                            if (txtReason.Text == "")
                            {
                                Response.Write("<script> alert('Please Specify Others. '); </script>");
                            }
                            else
                            {
                                dal_IWRS.IWRS_KITS_SP
                                    (
                                    ACTION: "INSERT_SPONSOR_DECISION",
                                    SUBJID: lblSubject.Text,
                                    OLD_KITNO: lblOldKitNo.Text,
                                    NEW_KITNO: lblNewKitNo.Text,
                                    ENTEREDBY: Session["User_ID"].ToString(),
                                    REASON: txtReason.Text,
                                    STATUSNAME: drpSponsorApproval.SelectedValue
                                    );

                                Response.Write("<script> alert('No Kit Allocated. '); window.location.href = 'NIWRS_KITS.aspx?VISIT=" + lblVisit.Text + "&SUBJID=" + lblSubject.Text + "'; </script>");
                            }
                        }
                        else
                        {
                            dal_IWRS.IWRS_KITS_SP
                                (
                                ACTION: "INSERT_SPONSOR_DECISION",
                                SUBJID: lblSubject.Text,
                                OLD_KITNO: lblOldKitNo.Text,
                                NEW_KITNO: lblNewKitNo.Text,
                                ENTEREDBY: Session["User_ID"].ToString(),
                                REASON: ddlReason.SelectedValue,
                                STATUSNAME: drpSponsorApproval.SelectedValue
                                );


                            Response.Write("<script> alert('No Kit Allocated. '); window.location.href = 'NIWRS_KITS.aspx?VISIT=" + lblVisit.Text + "&SUBJID=" + lblSubject.Text + "'; </script>");
                        }
                    }
                    else if (Request.QueryString["Spons"].ToString() == "Yes" && drpSponsorApproval.SelectedValue == "0")
                    {
                        if (ddlReason.SelectedValue == "Others")
                        {
                            if (txtReason.Text == "")
                            {
                                Response.Write("<script> alert('Please Specify Others. '); </script>");
                            }
                            else
                            {
                                dal_IWRS.IWRS_KITS_SP
                                    (
                                    ACTION: "INSERT_SPONSOR_APPROVAL",
                                    SUBJID: lblSubject.Text,
                                    OLD_KITNO: lblOldKitNo.Text,
                                    NEW_KITNO: lblNewKitNo.Text,
                                    ENTEREDBY: Session["User_ID"].ToString(),
                                    REASON: txtReason.Text
                                    );

                                Response.Write("<script> alert('Kit Sent for Approval. '); window.location.href = 'NIWRS_KITS.aspx?VISIT=" + lblVisit.Text + "&SUBJID=" + lblSubject.Text + "'; </script>");
                            }
                        }
                        else
                        {
                            dal_IWRS.IWRS_KITS_SP
                                (
                                ACTION: "INSERT_SPONSOR_APPROVAL",
                                SUBJID: lblSubject.Text,
                                OLD_KITNO: lblOldKitNo.Text,
                                NEW_KITNO: lblNewKitNo.Text,
                                ENTEREDBY: Session["User_ID"].ToString(),
                                REASON: ddlReason.SelectedValue
                                );

                            Response.Write("<script> alert('Kit Sent for Approval. '); window.location.href = 'NIWRS_KITS.aspx?VISIT=" + lblVisit.Text + "&SUBJID=" + lblSubject.Text + "'; </script>");
                        }
                    }
                    else if (Request.QueryString["Spons"].ToString() == "No")
                    {
                        if (ddlReason.SelectedValue == "Others")
                        {
                            if (txtReason.Text == "")
                            {
                                Response.Write("<script> alert('Please Specify Others. '); </script>");
                            }
                            else
                            {
                                dal_IWRS.IWRS_KITS_SP
                                    (
                                    ACTION: "ALLOCATE_BAK_KIT",
                                    SUBJID: lblSubject.Text,
                                    OLD_KITNO: lblOldKitNo.Text,
                                    NEW_KITNO: lblNewKitNo.Text,
                                    ENTEREDBY: Session["User_ID"].ToString(),
                                    REASON: txtReason.Text,
                                    DISPENSE_IDX: hfDispenseIDX.Value
                                    );

                                SEND_MAIL();

                                Response.Write("<script> alert('Kit No. " + lblNewKitNo.Text + " Allocated. '); window.location.href = 'NIWRS_KITS.aspx?VISIT=" + lblVisit.Text + "&SUBJID=" + lblSubject.Text + "'; </script>");
                            }
                        }
                        else
                        {
                            dal_IWRS.IWRS_KITS_SP
                                (
                                ACTION: "ALLOCATE_BAK_KIT",
                                SUBJID: lblSubject.Text,
                                OLD_KITNO: lblOldKitNo.Text,
                                NEW_KITNO: lblNewKitNo.Text,
                                ENTEREDBY: Session["User_ID"].ToString(),
                                REASON: ddlReason.SelectedValue,
                                DISPENSE_IDX: hfDispenseIDX.Value
                                );

                            SEND_MAIL();

                            Response.Write("<script> alert('Kit No. " + lblNewKitNo.Text + " Allocated. '); window.location.href = 'NIWRS_KITS.aspx?VISIT=" + lblVisit.Text + "&SUBJID=" + lblSubject.Text + "'; </script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NIWRS_KITS.aspx?VISIT=" + lblVisit.Text + "&SUBJID=" + lblSubject.Text);
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

                DataSet dsSUBJECT = dal_IWRS.IWRS_GET_SUBJECT_DETAILS_SP(ACTION: "GET_SUBJECT_DETAILS", SUBJID: lblSubject.Text);

                DataSet ds = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS_SITE", STRATA: "BakKit", SITEID: dsSUBJECT.Tables[0].Rows[0]["SITEID"].ToString());

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                SUBJECT = "Protocol ID- " + Session["PROJECTIDTEXT"].ToString() + " : Backup Kit Dispense Alert. " + lblVisit.Text + ", Site Id: " + dsSUBJECT.Tables[0].Rows[0]["SITEID"].ToString() + " ,Screening Id: " + lblSubject.Text + ".";

                BODY = "Screening ID : " + lblSubject.Text + " has successfully completed " + lblVisit.Text + " on " + cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt") + ". Randomization Number is " + lblRandNo.Text + " and Backup Kit number dispensed is " + lblNewKitNo.Text + ". User: " + Session["User_Name"].ToString() + "";

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