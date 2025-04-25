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
    public partial class NIWRS_KITS_SITE_REC : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtQuarantineComments.Attributes.Add("Maxlength", "200");
            txtACCEPTComments.Attributes.Add("Maxlength", "200");
            txtBlockComments.Attributes.Add("Maxlength", "200");
            txtDamageComments.Attributes.Add("Maxlength", "200");
            txtACCEPTComments.Attributes.Add("Maxlength", "200");

            try
            {
                if (!IsPostBack)
                {
                    GET_COUNTRY();
                    GET_SITE();
                    GET_SITE_ORDERS();
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
                DataSet ds = dal.GET_COUNTRY_SP();

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
                DataSet ds = dal.GET_INVID_SP(COUNTRYID: ddlCountry.SelectedValue);

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

        private void GET_SITE_ORDERS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_SITE_ORDERS", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue);
                gvOrders.DataSource = ds;
                gvOrders.DataBind();
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
                GET_SITE_ORDERS();
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
                GET_SITE_ORDERS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string ORDERID = dr["ORDERID"].ToString();

                    DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_SITE_ORDERS_KITS", ORDERID: ORDERID);
                    GridView gvKits = (GridView)e.Row.FindControl("gvKits");
                    gvKits.DataSource = ds;
                    gvKits.DataBind();

                    Label TOTALKITS = (Label)e.Row.FindControl("TOTALKITS");
                    TOTALKITS.Text = gvKits.Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "")
                {

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;

                    Label SHIPMENTID = (Label)gvOrders.Rows[index].FindControl("SHIPMENTID");
                    Label SHIPMENTDATE = (Label)gvOrders.Rows[index].FindControl("SHIPMENTDATE");
                    Label COUNTRYNAME = (Label)gvOrders.Rows[index].FindControl("COUNTRYNAME");
                    Label SITEID = (Label)gvOrders.Rows[index].FindControl("SITEID");
                    Label TABLENAME = (Label)gvOrders.Rows[index].FindControl("TABLENAME");
                    Label ORDERID = (Label)gvOrders.Rows[index].FindControl("ORDERID");
                    TextBox RECEIPTDATE = (TextBox)gvOrders.Rows[index].FindControl("RECEIPTDATE");

                    if (e.CommandName == "Accept")
                    {
                        hdnACCEPT_SHIPMENTID.Value = SHIPMENTID.Text;
                        hdnACCEPT_SHIPMENTDATE.Value = SHIPMENTDATE.Text;
                        hdnACCEPT_COUNTRYNAME.Value = COUNTRYNAME.Text;
                        hdnACCEPT_SITEID.Value = SITEID.Text;
                        hdnACCEPT_TABLENAME.Value = TABLENAME.Text;
                        hdnACCEPT_RECEIPTDATE.Value = RECEIPTDATE.Text;
                        hdnACCEPT_ORDERID.Value = e.CommandArgument.ToString();
                        hdnAccept_COMMAND.Value = e.CommandName.ToString();

                        modalpopupACCEPT.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL_ACCEPT(string ORDERID, string COUNTRY, string SITE, string SHIPMENTID, string SHIPMENTDATE)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_SITE", SITEID: SITE);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_REC-Accept");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[ORDERID]", ORDERID);
                    SUBJECT = SUBJECT.Replace("[SHIPMENTID]", SHIPMENTID);
                    SUBJECT = SUBJECT.Replace("[SHIPMENTDAT]", SHIPMENTDATE);
                    SUBJECT = SUBJECT.Replace("[SITEID]", SITE);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[ORDERID]", ORDERID);
                    BODY = BODY.Replace("[SHIPMENTID]", SHIPMENTID);
                    BODY = BODY.Replace("[SHIPMENTDAT]", SHIPMENTDATE);
                    BODY = BODY.Replace("[SITEID]", SITE);
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

        private void SEND_MAIL_KITACCEPT(string KITNO, string COUNTRY, string SITE)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_SITE", SITEID: SITE);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_REC-KitAccept");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[SITEID]", SITE);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[SITEID]", SITE);
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

        private void SEND_MAIL_QUARANTINE(string KITNO, string COUNTRY, string SITE)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_SITE_QUARATINE", SITEID: SITE);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_REC-Quaratine");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[SITE]", SITE);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[SITE]", SITE);
                    BODY = BODY.Replace("[QUARANTINED_AT]", "Site Order");
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

        private void SEND_MAIL_BLOCK(string KITNO, string COUNTRY, string SITE)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_SITE_BLOCK", SITEID: SITE);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_REC-Block");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[SITE]", SITE);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[SITE]", SITE);
                    BODY = BODY.Replace("[Blocked_AT]", "Site Order");
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

        private void SEND_MAIL_DAMAGE(string KITNO, string COUNTRY, string SITE)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_SITE_DAMAGE", SITEID: SITE);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_REC-Damage");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[SITE]", SITE);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[SITE]", SITE);
                    BODY = BODY.Replace("[Damaged_AT]", "Site Order");
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

        protected void btn_Quarantine(object sender, EventArgs e)
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
        protected void btnSubmitQuarantine_Click(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < gvOrders.Rows.Count; j++)
                {
                    string COUNTRYNAME = ((Label)gvOrders.Rows[j].FindControl("COUNTRYNAME")).Text;
                    string ORDERID = ((Label)gvOrders.Rows[j].FindControl("ORDERID")).Text;
                    string RECEIPTDATE = ((TextBox)gvOrders.Rows[j].FindControl("RECEIPTDATE")).Text;
                    string TABLENAME = ((Label)gvOrders.Rows[j].FindControl("TABLENAME")).Text;
                    string SITEID = ((Label)gvOrders.Rows[j].FindControl("SITEID")).Text;

                    GridView gvKits = (GridView)gvOrders.Rows[j].FindControl("gvKits");

                    for (int i = 0; i < gvKits.Rows.Count; i++)
                    {
                        string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;

                        CheckBox Chek_Quarantine = (CheckBox)gvKits.Rows[i].FindControl("Chek_Quarantine");

                        string selectedKitsCsv = hfKITS.Value;
                        if (selectedKitsCsv.Contains(KITNO))
                        {
                            Chek_Quarantine.Checked = true;
                        }

                        if (Chek_Quarantine.Checked)
                        {
                            dal_IWRS.IWRS_KITS_SITE_SP(
                                    ACTION: "QUARANTINE_SITE_KIT",
                                    KITNO: KITNO,
                                    RECEIPTDATE: RECEIPTDATE,
                                    ORDERID: ORDERID,
                                    QUARANTINECOMM: txtQuarantineComments.Text,
                                    TABLENAME: TABLENAME
                                  );

                            SEND_MAIL_QUARANTINE(KITNO, COUNTRYNAME, SITEID);

                        }
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Quarantined Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                    hfKITS.Value = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCancelQuarantine_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void btn_Block(object sender, EventArgs e)
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
        protected void btnSubmitBlock_Click(object sender, EventArgs e)
        {
            try
            {

                for (int j = 0; j < gvOrders.Rows.Count; j++)
                {
                    string COUNTRYNAME = ((Label)gvOrders.Rows[j].FindControl("COUNTRYNAME")).Text;
                    string ORDERID = ((Label)gvOrders.Rows[j].FindControl("ORDERID")).Text;
                    string RECEIPTDATE = ((TextBox)gvOrders.Rows[j].FindControl("RECEIPTDATE")).Text;
                    string TABLENAME = ((Label)gvOrders.Rows[j].FindControl("TABLENAME")).Text;
                    string SITEID = ((Label)gvOrders.Rows[j].FindControl("SITEID")).Text;

                    GridView gvKits = (GridView)gvOrders.Rows[j].FindControl("gvKits");

                    for (int i = 0; i < gvKits.Rows.Count; i++)
                    {
                        string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;

                        CheckBox Chek_Block = (CheckBox)gvKits.Rows[i].FindControl("Chek_Block");

                        string selectedKitsCsv = hfKITS.Value;
                        if (selectedKitsCsv.Contains(KITNO))
                        {
                            Chek_Block.Checked = true;
                        }


                        if (Chek_Block.Checked)
                        {
                            dal_IWRS.IWRS_KITS_SITE_SP(
                                    ACTION: "BLOCK_SITE_KIT",
                                    KITNO: KITNO,
                                    RECEIPTDATE: RECEIPTDATE,
                                    ORDERID: ORDERID,
                                    BLOCKEDCOMM: txtBlockComments.Text,
                                    TABLENAME: TABLENAME
                                );

                            SEND_MAIL_BLOCK(KITNO, COUNTRYNAME, SITEID);
                        }
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Blocked Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                    hfKITS.Value = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCancelBlock_Click(object sender, EventArgs e)
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

        protected void btn_Damage(object sender, EventArgs e)
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
                    ModalPopupExtender3.Show();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitDamage_Click(object sender, EventArgs e)
        {
            try
            {
                
                for (int j = 0; j < gvOrders.Rows.Count; j++)
                {

                    string COUNTRYNAME = ((Label)gvOrders.Rows[j].FindControl("COUNTRYNAME")).Text;
                    string ORDERID = ((Label)gvOrders.Rows[j].FindControl("ORDERID")).Text;
                    string RECEIPTDATE = ((TextBox)gvOrders.Rows[j].FindControl("RECEIPTDATE")).Text;
                    string TABLENAME = ((Label)gvOrders.Rows[j].FindControl("TABLENAME")).Text;
                    string SITEID = ((Label)gvOrders.Rows[j].FindControl("SITEID")).Text;

                    GridView gvKits = (GridView)gvOrders.Rows[j].FindControl("gvKits");

                    for (int i = 0; i < gvKits.Rows.Count; i++)
                    {
                        string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;

                        CheckBox Chek_Damage = (CheckBox)gvKits.Rows[i].FindControl("Chek_Damage");

                        string selectedKitsCsv = hfKITS.Value;
                        if (selectedKitsCsv.Contains(KITNO))
                        {
                            Chek_Damage.Checked = true;
                        }

                        if (Chek_Damage.Checked)
                        {
                            dal_IWRS.IWRS_KITS_SITE_SP(
                                ACTION: "DAMAGE_SITE_KIT",
                                KITNO: KITNO,
                                RECEIPTDATE: RECEIPTDATE,
                                ORDERID: ORDERID,
                                DAMAGEDCOMM: txtDamageComments.Text,
                                TABLENAME: TABLENAME
                                );

                            SEND_MAIL_DAMAGE(KITNO, COUNTRYNAME, SITEID);
                        }
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit marked as Damaged.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                    hfKITS.Value = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCancelDamage_Click(object sender, EventArgs e)
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


        protected void btn_Accept(object sender, EventArgs e)
        {
            try
            {
                bool kitSelected = false;
                GridViewRow gvRows = (sender as Button).NamingContainer as GridViewRow;
                GridView gvKits = (GridView)(gvRows.Parent.Parent);

                GridViewRow gvOrderRow = (GridViewRow)(gvKits.NamingContainer);

                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    CheckBox Chek_Accept = (CheckBox)gvKits.Rows[i].FindControl("Chek_Accept");
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string COUNTRYNAME = ((Label)gvOrderRow.FindControl("COUNTRYNAME")).Text;
                    string ORDERID = ((Label)gvOrderRow.FindControl("ORDERID")).Text;
                    string RECEIPTDATE = ((TextBox)gvOrderRow.FindControl("RECEIPTDATE")).Text;
                    string SITEID = ((Label)gvOrderRow.FindControl("SITEID")).Text;

                    if (Chek_Accept.Checked == true)
                    {
                        dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "ACCEPT_SITE_KIT", KITNO: KITNO, RECEIPTDATE: RECEIPTDATE, ORDERID: ORDERID);
                        SEND_MAIL_KITACCEPT(KITNO, COUNTRYNAME, SITEID);
                        kitSelected = true;
                    }
                }

                if (!kitSelected)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Accepted.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
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
                DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_SITE_ORDERS_EXPORT", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue);
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvKits_PreRender(object sender, EventArgs e)
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

        protected void btnSubmitACCEPT_Click(object sender, EventArgs e)
        {
            try
            {
                bool Orderlist = false;
                string ACCEPTCOMM = ddlACCEPTOptions.SelectedValue + " " + txtACCEPTComments.Text;
                if (hdnAccept_COMMAND.Value == "Accept")
                {
                    dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "ACCEPT_SITE_ORDER", ORDERID: hdnACCEPT_ORDERID.Value, SHIPMENTID: hdnACCEPT_SHIPMENTID.Value, SHIPMENTDATE: hdnACCEPT_SHIPMENTDATE.Value, RECEIPTDATE: hdnACCEPT_RECEIPTDATE.Value, TABLENAME: hdnACCEPT_TABLENAME.Value, ACCEPTCOMM: ACCEPTCOMM);
                    Orderlist = true;

                }
                else
                {
                    bool kitSelected = false;
                    for (int j = 0; j < gvOrders.Rows.Count; j++)
                    {
                        string COUNTRYNAME = ((Label)gvOrders.Rows[j].FindControl("COUNTRYNAME")).Text;
                        string ORDERID = ((Label)gvOrders.Rows[j].FindControl("ORDERID")).Text;
                        string RECEIPTDATE = ((TextBox)gvOrders.Rows[j].FindControl("RECEIPTDATE")).Text;
                        string SHIPMENTID = ((Label)gvOrders.Rows[j].FindControl("SHIPMENTID")).Text;
                        string TABLENAME = ((Label)gvOrders.Rows[j].FindControl("TABLENAME")).Text;
                        string SITEID = ((Label)gvOrders.Rows[j].FindControl("SITEID")).Text;

                        GridView gvKits = (GridView)gvOrders.Rows[j].FindControl("gvKits");


                        for (int i = 0; i < gvKits.Rows.Count; i++)
                        {
                            string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;

                            CheckBox Chek_Accept = (CheckBox)gvKits.Rows[i].FindControl("Chek_Accept");


                            if (Chek_Accept.Checked == true)
                            {
                                dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "SINGLE_ACCEPT_SITE_KIT", KITNO: KITNO, RECEIPTDATE: RECEIPTDATE, ORDERID: ORDERID,SHIPMENTID: SHIPMENTID, ACCEPTCOMM: ACCEPTCOMM);
                                SEND_MAIL_KITACCEPT(KITNO, COUNTRYNAME, SITEID);
                                kitSelected = true;

                            }
                        }
                    }

                    if (!kitSelected)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Accepted.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                    }

                }

                if (Orderlist == true)
                    {
                        SEND_MAIL_ACCEPT(hdnACCEPT_ORDERID.Value, hdnACCEPT_COUNTRYNAME.Value, hdnACCEPT_SITEID.Value, hdnACCEPT_SHIPMENTID.Value, hdnACCEPT_SHIPMENTDATE.Value);

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Order Accepted Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                    }

                }

            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnCancelACCEPT_Click(object sender, EventArgs e)
        {
            try
            {
                modalpopupACCEPT.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                bool kitSelected = true;

                GridViewRow gvRows = (sender as Button).NamingContainer as GridViewRow;
                GridView gvKits = (GridView)(gvRows.Parent.Parent);

                GridViewRow gvOrderRow = (GridViewRow)(gvKits.NamingContainer);


                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    CheckBox Chek_Accept = (CheckBox)gvKits.Rows[i].FindControl("Chek_Accept");

                    if (Chek_Accept.Checked && kitSelected)
                    {
                        kitSelected = false;
                    }
                }
                if (kitSelected)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.'); ", true);
                }
                else
                {
                    hdnAccept_COMMAND.Value = "";

                    modalpopupACCEPT.Show();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            
        }
    }
}