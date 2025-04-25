using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class IWRS_KITS_COUNTRY : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtQuarantineComments.Attributes.Add("MaxLength", "200");
            txtBlockComments.Attributes.Add("MaxLength", "200");
            txtDamageComments.Attributes.Add("MaxLength", "200");
            txtDestroyComments.Attributes.Add("MaxLength", "200");
            txtRetentionComments.Attributes.Add("MaxLength", "200");
            txtReturnComments.Attributes.Add("MaxLength", "200");

            try
            {
                if (!IsPostBack)
                {
                    GET_COUNTRY();

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
        private void GET_KITS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "GET_KITS_COUNTRY", COUNTRYID: ddlCountry.SelectedValue);
                gvKits.DataSource = ds;
                gvKits.DataBind();
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
                GET_KITS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL_QUARANTINE(string KITNO, string COUNTRY)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS", STRATA: "KIT_COUNTRY_QUARATINE", COUNTRY: COUNTRY);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "IWRS_KITS_COUNTRY -Quaratine");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[Quaratined_AT]", "Country Depot");
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

        private void SEND_MAIL_BLOCK(string KITNO, string COUNTRY)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS", STRATA: "KIT_COUNTRY_BLOCK", COUNTRY: COUNTRY);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "IWRS_KITS_COUNTRY -Block");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[Blocked_AT]", "Country Depot");
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

        private void SEND_MAIL_DAMAGE(string KITNO, string COUNTRY)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS", STRATA: "KIT_COUNTRY_DAMAGE", COUNTRY: COUNTRY);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "IWRS_KITS_COUNTRY -Damage");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[Damaged_AT]", "Country Depot");
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

        private void SEND_MAIL_DESTROY(string KITNO, string COUNTRY)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS", STRATA: "KIT_COUNTRY_DESTROY", COUNTRY: COUNTRY);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "IWRS_KITS_COUNTRY -Destroy");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[Destroyed_AT]", "Country Depot");
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

        private void SEND_MAIL_RETURNED(string KITNO, string COUNTRY)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS", STRATA: "KIT_COUNTRY_RETURN",COUNTRY:COUNTRY);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "IWRS_KITS_COUNTRY -Return");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[Returned_AT]", "Country Depot");
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

        private void SEND_MAIL_RETENTION(string KITNO, string COUNTRY)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS", STRATA: "KIT_COUNTRY", COUNTRY: COUNTRY);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "IWRS_KITS_COUNTRY -Retention");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[Retention_AT]", "Country Depot");
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
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRYNAME")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;

                    CheckBox Chek_Quarantine = (CheckBox)gvKits.Rows[i].FindControl("Chek_Quarantine");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_Quarantine.Checked = true;
                    }

                    if (Chek_Quarantine.Checked)
                    {
                        dal_IWRS.IWRS_KITS_COUNTRY_SP(
                            ACTION: "QUARANTINE_COUNTRY_INVENTORY",
                            KITNO: KITNO,
                            QUARANTINECOMM: txtQuarantineComments.Text,
                            TABLENAME: TABLENAME
                            );

                        SEND_MAIL_QUARANTINE(KITNO, COUNTRYNAME);
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Quarantined Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);

                hfKITS.Value = "";
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
                //bool kitSelected = true;
                //for (int i = 0; i < gvKits.Rows.Count; i++)
                //{
                //    CheckBox Chek_Block = (CheckBox)gvKits.Rows[i].FindControl("Chek_Block");

                //    if (Chek_Block.Checked && kitSelected)
                //    {
                //        kitSelected = false;
                //    }
                //}
                //if (kitSelected)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                //}
                //else
                //{
                //    ModalPopupExtender2.Show();
                //}

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
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRYNAME")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;

                    CheckBox Chek_Block = (CheckBox)gvKits.Rows[i].FindControl("Chek_Block");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_Block.Checked = true;
                    }
                    

                    if (Chek_Block.Checked)
                    {
                        dal_IWRS.IWRS_KITS_COUNTRY_SP(
                            ACTION: "BLOCK_COUNTRY_INVENTORY",
                            KITNO: KITNO,
                            BLOCKEDCOMM: txtBlockComments.Text,
                            TABLENAME: TABLENAME
                            );

                        SEND_MAIL_BLOCK(KITNO, COUNTRYNAME);
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit Blocked Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);

                hfKITS.Value = "";
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
                //bool kitSelected = true;

                //for (int i = 0; i < gvKits.Rows.Count; i++)
                //{
                //    CheckBox Chek_Damage = (CheckBox)gvKits.Rows[i].FindControl("Chek_Damage");

                //    if (Chek_Damage.Checked && kitSelected)
                //    {
                //        kitSelected = false;
                //    }
                //}
                //if (kitSelected)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                //}
                //else
                //{
                //    ModalPopupExtender3.Show();
                //}

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
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRYNAME")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;

                    CheckBox Chek_Damage = (CheckBox)gvKits.Rows[i].FindControl("Chek_Damage");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_Damage.Checked = true;
                    }

                    if (Chek_Damage.Checked)
                    {
                        dal_IWRS.IWRS_KITS_COUNTRY_SP(
                            ACTION: "DAMAGE_COUNTRY_INVENTORY",
                            KITNO: KITNO,
                            DAMAGEDCOMM: txtDamageComments.Text,
                            TABLENAME: TABLENAME
                            );

                        SEND_MAIL_DAMAGE(KITNO, COUNTRYNAME);
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit marked as Damaged.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);

                hfKITS.Value = "";
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

        protected void btn_Destroy(object sender, EventArgs e)
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
                    ModalPopupExtender4.Show();
                }
                //bool kitSelected = true;

                //for (int i = 0; i < gvKits.Rows.Count; i++)
                //{
                //    CheckBox Chek_Destroy = (CheckBox)gvKits.Rows[i].FindControl("Chek_Destroy");

                //    if (Chek_Destroy.Checked && kitSelected)
                //    {
                //        kitSelected = false;
                //    }
                //}
                //if (kitSelected)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                //}
                //else
                //{
                //    ModalPopupExtender4.Show();
                //}

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitDestroy_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRYNAME")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;

                    CheckBox Chek_Destroy = (CheckBox)gvKits.Rows[i].FindControl("Chek_Destroy");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_Destroy.Checked = true;
                    }


                    if (Chek_Destroy.Checked)
                    {
                        dal_IWRS.IWRS_KITS_COUNTRY_SP(
                            ACTION: "DESTROY_COUNTRY_INVENTORY",
                            KITNO: KITNO,
                            DESTROYCOMM: txtDestroyComments.Text,
                            TABLENAME: TABLENAME
                            );

                        SEND_MAIL_DESTROY(KITNO, COUNTRYNAME);
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit marked as Destroy.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);

                hfKITS.Value = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCancelDestroy_Click(object sender, EventArgs e)
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

        protected void btn_Return(object sender, EventArgs e)
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
                    ModalPopupExtender5.Show();
                }
                //bool kitSelected = true;

                //for (int i = 0; i < gvKits.Rows.Count; i++)
                //{
                //    CheckBox Chek_Return = (CheckBox)gvKits.Rows[i].FindControl("Chek_Return");

                //    if (Chek_Return.Checked && kitSelected)
                //    {
                //        kitSelected = false;
                //    }
                //}

                //if (kitSelected)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                //}
                //else
                //{
                //    ModalPopupExtender5.Show();
                //}
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitReturn_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRYNAME")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;

                    CheckBox Chek_Return = (CheckBox)gvKits.Rows[i].FindControl("Chek_Return");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_Return.Checked = true;
                    }

                    if (Chek_Return.Checked)
                    {
                        dal_IWRS.IWRS_KITS_COUNTRY_SP(
                            ACTION: "RETURN_COUNTRY_INVENTORY",
                            KITNO: KITNO,
                            RETURNEDCOMM: txtReturnComments.Text,
                            TABLENAME: TABLENAME
                            );

                        SEND_MAIL_RETURNED(KITNO, COUNTRYNAME);
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit marked as Return.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);

                hfKITS.Value = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCancelReturn_Click(object sender, EventArgs e)
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
                DataSet ds = dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "GET_KITS_COUNTRY_EXPORT", COUNTRYID: ddlCountry.SelectedValue);
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btn_Retention(object sender, EventArgs e)
        {

            try
            {
                //bool kitSelected = true;

                //for (int i = 0; i < gvKits.Rows.Count; i++)
                //{
                //    CheckBox Chek_Retention = (CheckBox)gvKits.Rows[i].FindControl("Chek_Retention");

                //    if (Chek_Retention.Checked && kitSelected)
                //    {
                //        kitSelected = false;
                //    }
                //}
                //if (kitSelected)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                //}
                //else
                //{
                //    ModalPopupExtender6.Show();
                //}
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

        protected void btnSubmitRetention_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRYNAME")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;

                    CheckBox Chek_Retention = (CheckBox)gvKits.Rows[i].FindControl("Chek_Retention");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_Retention.Checked = true;
                    }

                    if (Chek_Retention.Checked)
                    {
                        dal_IWRS.IWRS_KITS_COUNTRY_SP(
                            ACTION: "RETENTION_COUNTRY_INVENTORY",
                            KITNO: KITNO,
                            RETENTIONCOMM: txtRetentionComments.Text,
                            TABLENAME: TABLENAME
                            );

                        SEND_MAIL_RETENTION(KITNO, COUNTRYNAME);
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kit marked as Retention.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);

                hfKITS.Value = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelRetention_Click(object sender, EventArgs e)
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


        protected void Expire_Click(object sender, EventArgs e)
        {
            try
            {
                //bool kitSelected = true;
                //for (int i = 0; i < gvKits.Rows.Count; i++)
                //{
                //    CheckBox Chek_Expire = (CheckBox)gvKits.Rows[i].FindControl("Chek_Expire");

                //    if (Chek_Expire.Checked && kitSelected)
                //    {
                //        kitSelected = false;
                //    }
                //}
                //if (kitSelected)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one kit.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                //}
                //else
                //{
                //    ModalPopupExtender7.Show();
                //}
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

        protected void btnSubmitExpire_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRYNAME")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;

                    CheckBox Chek_Expire = (CheckBox)gvKits.Rows[i].FindControl("Chek_Expire");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_Expire.Checked = true;
                    }


                    if (Chek_Expire.Checked)
                    {
                        dal_IWRS.IWRS_KITS_COUNTRY_SP(
                            ACTION: "EXPIRED_COUNTRY_INVENTORY",
                            KITNO: KITNO,
                            EXPIRECOMM: txtExpireComments.Text,
                            TABLENAME: TABLENAME
                            );

                        SEND_MAIL_EXPIRE(KITNO, COUNTRYNAME);
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

        private void SEND_MAIL_EXPIRE(string KITNO,string  COUNTRY)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_COUNTRY_EXPIRED");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KIT_INVENTORY_COUNTRY - Expire");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[Kit_NUMBER]", KITNO);
                    BODY = BODY.Replace("[Expire_AT]", "Country Depot");
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