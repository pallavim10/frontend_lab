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
    public partial class NIWRS_KITS_SITE_PENDING : System.Web.UI.Page
    {

        DAL dal = new DAL();

        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtQuarantineComments.Attributes.Add("MaxLength", "200");
            txtBlockComments.Attributes.Add("MaxLength", "200");
            txtDamageComments.Attributes.Add("MaxLength", "200");
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
                DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_SITE_SHIPMENTS", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue);
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

                    DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_SITE_SHIPMENT_KITS", ORDERID: ORDERID);
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

                    TextBox SHIPMENTID = (TextBox)gvOrders.Rows[index].FindControl("SHIPMENTID");
                    TextBox SHIPMENTDATE = (TextBox)gvOrders.Rows[index].FindControl("SHIPMENTDATE");
                    Label COUNTRYNAME = (Label)gvOrders.Rows[index].FindControl("COUNTRYNAME");
                    Label SITEID = (Label)gvOrders.Rows[index].FindControl("SITEID");

                    if (e.CommandName == "Accept")
                    {
                        dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "UPDATE_SITE_SHIPMENT", ORDERID: e.CommandArgument.ToString(), SHIPMENTID: SHIPMENTID.Text, SHIPMENTDATE: SHIPMENTDATE.Text);

                        SEND_MAIL(e.CommandArgument.ToString(), COUNTRYNAME.Text, SITEID.Text, SHIPMENTID.Text, SHIPMENTDATE.Text);

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Shipment Updated Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                    }
                }
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

                GridViewRow parentRow = (sender as GridView).NamingContainer as GridViewRow;
                int parentindex = parentRow.RowIndex;

                Label ORDERID = (Label)gvOrders.Rows[parentindex].FindControl("ORDERID");
                Label COUNTRYNAME = (Label)gvOrders.Rows[parentindex].FindControl("COUNTRYNAME");
                Label SITEID = (Label)gvOrders.Rows[parentindex].FindControl("SITEID");
                Label TREAT_GRP_NAME = (Label)row.FindControl("TREAT_GRP_NAME");
                Label KITNO = (Label)row.FindControl("KITNO");

                if (e.CommandName == "Quarantine")
                {
                    dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "QUARANTINE_SITE_KIT", KITNO: e.CommandArgument.ToString(), ORDERID: ORDERID.Text);

                    SEND_MAIL_QUARANTINE(KITNO.Text, COUNTRYNAME.Text, SITEID.Text);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Quarantined Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                else if (e.CommandName == "Block")
                {
                    dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "BLOCK_SITE_KIT", KITNO: e.CommandArgument.ToString(), ORDERID: ORDERID.Text);

                    SEND_MAIL_BLOCK(KITNO.Text, COUNTRYNAME.Text, SITEID.Text);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Blocked Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                else if (e.CommandName == "Damage")
                {
                    dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "DAMAGE_SITE_KIT", KITNO: e.CommandArgument.ToString(), ORDERID: ORDERID.Text);

                    SEND_MAIL_DAMAGE(KITNO.Text, COUNTRYNAME.Text, SITEID.Text);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit marked as Damaged.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL(string ORDERID, string COUNTRY, string SITEID, string SHIPMENTID, string SHIPMENTDATE)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_SITE", SITEID: SITEID);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_PENDING");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[ORDERID]", ORDERID);
                    SUBJECT = SUBJECT.Replace("[SITEID]", SITEID);
                    SUBJECT = SUBJECT.Replace("[SHIPMENTDAT]", SHIPMENTDATE);
                    SUBJECT = SUBJECT.Replace("[SHIPMENTID]", SHIPMENTID);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[ORDERID]", ORDERID);
                    BODY = BODY.Replace("[SITEID]", SITEID);
                    BODY = BODY.Replace("[SHIPMENTDAT]", SHIPMENTDATE);
                    BODY = BODY.Replace("[SHIPMENTID]", SHIPMENTID);
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

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_PENDING-Quaratine");

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
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_PENDING-Block");

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

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_PENDING-Damage");

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
                    string SITEID = ((Label)gvOrders.Rows[j].FindControl("SITEID")).Text;
                    string TABLENAME = ((Label)gvOrders.Rows[j].FindControl("TABLENAME")).Text;

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
                    string SITEID = ((Label)gvOrders.Rows[j].FindControl("SITEID")).Text;
                    string TABLENAME = ((Label)gvOrders.Rows[j].FindControl("TABLENAME")).Text;

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
                    string SITEID = ((Label)gvOrders.Rows[j].FindControl("SITEID")).Text;
                    string TABLENAME = ((Label)gvOrders.Rows[j].FindControl("TABLENAME")).Text;

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
                DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_SITE_SHIPMENTS_EXPORT", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue);
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
    }
}