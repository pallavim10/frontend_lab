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
    public partial class NIWRS_QURANTINE_CENTRAL : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUnQuarantineComments.Attributes.Add("MaxLength", "200");
            txtRejectComments.Attributes.Add("MaxLength", "200");
            try
            {
                if (!IsPostBack)
                {
                    GET_COUNTRY();
                    GET_SITE();
                    GET_KITS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GET_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP(USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlCountry.DataSource = ds.Tables[0];
                        ddlCountry.DataTextField = "COUNTRYNAME";
                        ddlCountry.DataValueField = "COUNTRYID";
                        ddlCountry.DataBind();
                    }
                    else
                    {
                        ddlCountry.DataSource = ds.Tables[0];
                        ddlCountry.DataTextField = "COUNTRYNAME";
                        ddlCountry.DataValueField = "COUNTRYID";
                        ddlCountry.DataBind();
                        ddlCountry.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSite.DataSource = ds.Tables[0];
                    ddlSite.DataValueField = "INVID";
                    ddlSite.DataBind();
                }
                ddlSite.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GET_KITS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_QUARANTINE_CENTRAL", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue);
                gvKits.DataSource = ds;
                gvKits.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL_UNQUARANTINE(string Location, string ORDERID, string KITNO)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_CENTRAL_QUARATINE");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_QURANTINE_CENTRAL - UnQuarantine");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[ORDERID]", ORDERID);
                    SUBJECT = SUBJECT.Replace("[Location]", Location);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[ORDERID]", ORDERID);
                    BODY = BODY.Replace("[Location]", Location);
                    BODY = BODY.Replace("[REVERTTOUSE]", "Released");
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

        private void SEND_MAIL_REJECT(string KITNO, string Location, string ORDERID)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_CENTRAL_REJECT");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_QURANTINE_CENTRAL - Reject");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[ORDERID]", ORDERID);
                    SUBJECT = SUBJECT.Replace("[Location]", Location);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[ORDERID]", ORDERID);
                    BODY = BODY.Replace("[Location]", Location);
                    BODY = BODY.Replace("[REJECTED_AT]", Location);
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
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SITE();
                GET_KITS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_KITS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void gvKits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;

                Label Location = (Label)gvKits.Rows[index].FindControl("Location");

                if (e.CommandName == "UnQuarantine")
                {
                    dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "UNQUARANTINE_CENTRAL_KIT", KITNO: e.CommandArgument.ToString(), TREAT_GRP: Location.Text);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Reverted to Use Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                else if (e.CommandName == "Reject")
                {
                    dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "REJECT_CENTRAL_KIT", KITNO: e.CommandArgument.ToString(), TREAT_GRP: Location.Text);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Rejected Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                else if (e.CommandName == "Expire")
                {
                    dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "CENTRAL_QURANTINE_EXPIRE", KITNO: e.CommandArgument.ToString(), TREAT_GRP: Location.Text);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Expired Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                GET_KITS();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void grd_data_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }
        protected void btn_UnQuarantine(object sender, EventArgs e)
        {
            try
            {
                string selectedKitsCsv = hfKITS.Value;
                var selectedKits = selectedKitsCsv.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (selectedKits.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.');", true);
                    return;
                }
                else
                {
                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitUnQuarantine_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string Location = ((Label)gvKits.Rows[i].FindControl("Location")).Text;
                    string ORDERID = ((Label)gvKits.Rows[i].FindControl("ORDERID")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;


                    CheckBox Chek_UnQuarantine = (CheckBox)gvKits.Rows[i].FindControl("Chek_UnQuarantine");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_UnQuarantine.Checked = true;
                    }


                    if (Chek_UnQuarantine.Checked)
                    {
                        dal_IWRS.IWRS_KITS_CENTRAL_SP(
                                ACTION: "UNQUARANTINE_CENTRAL_KIT",
                                KITNO: KITNO,
                                TREAT_GRP: Location,
                                UNQUARANTINECOMM: txtUnQuarantineComments.Text,
                                ORDERID: ORDERID,
                                TABLENAME: TABLENAME
                            );

                        SEND_MAIL_UNQUARANTINE(Location, ORDERID, KITNO);
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Reverted to Use Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);

                hfKITS.Value = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCancelUnQuarantine_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btn_Reject(object sender, EventArgs e)
        {
            try
            {
                string selectedKitsCsv = hfKITS.Value;
                var selectedKits = selectedKitsCsv.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (selectedKits.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.');", true);
                    return;
                }
                else
                {
                    ModalPopupExtender2.Show();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitReject_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string Location = ((Label)gvKits.Rows[i].FindControl("Location")).Text;
                    string ORDERID = ((Label)gvKits.Rows[i].FindControl("ORDERID")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;

                    CheckBox Chek_Reject = (CheckBox)gvKits.Rows[i].FindControl("Chek_Reject");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_Reject.Checked = true;
                    }


                    if (Chek_Reject.Checked)
                    {
                        dal_IWRS.IWRS_KITS_CENTRAL_SP(
                            ACTION: "REJECT_CENTRAL_KIT",
                            KITNO: KITNO,
                            TREAT_GRP: Location,
                            ORDERID: ORDERID,
                            TABLENAME: TABLENAME,
                            ACCEPTCOMM:txtRejectComments.Text
                            );

                        SEND_MAIL_REJECT(KITNO, Location, ORDERID);
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Rejected Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);

                hfKITS.Value = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCancelReject_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
                lblHeader.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_QUARANTINE_CENTRAL_EXPORT", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue);
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Expire_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedKitsCsv = hfKITS.Value;
                var selectedKits = selectedKitsCsv.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (selectedKits.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.');", true);
                    return;
                }
                else
                {
                    ModalPopupExtender7.Show();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL_EXPIRE(string KITNO)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_CENTRAL_EXPIRED");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KIT_INVENTORY_CENTRAL - Expire");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[Kit_NUMBER]", KITNO);
                    BODY = BODY.Replace("[Expire_AT]", "Central Depot");
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

        protected void btnSubmitExpire_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;
                    
                    CheckBox Chek_Expire = (CheckBox)gvKits.Rows[i].FindControl("Chek_Expire");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_Expire.Checked = true;
                    }

                    if (Chek_Expire.Checked)
                    {
                        dal_IWRS.IWRS_KITS_CENTRAL_SP(
                            ACTION: "CENTRAL_QURANTINE_EXPIRE",
                            KITNO: KITNO,
                            EXPIRECOMM: txtExpireComments.Text,
                            TABLENAME: TABLENAME
                            );

                        SEND_MAIL_EXPIRE(KITNO);
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Expire Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);

                hfKITS.Value = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelExpire_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        
    }
}