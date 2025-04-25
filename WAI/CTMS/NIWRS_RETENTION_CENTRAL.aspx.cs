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
    public partial class NIWRS_RETENTION_CENTRAL : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtRetentionComments.Attributes.Add("MaxLength", "200");
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
                DataSet ds = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_RETENTION_CENTRAL", COUNTRYID: ddlCountry.SelectedValue);
                gvKits.DataSource = ds;
                gvKits.DataBind();
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
                DataSet ds = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_RETENTION_CENTRAL_EXPORT", COUNTRYID: ddlCountry.SelectedValue);
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
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

        protected void Retenion_Click(object sender, EventArgs e)
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

        protected void btnSubmitRetention_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string Location = ((Label)gvKits.Rows[i].FindControl("Location")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;

                    CheckBox Chek_UNRetention = (CheckBox)gvKits.Rows[i].FindControl("Chek_UNRetention");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_UNRetention.Checked = true;
                    }


                    if (Chek_UNRetention.Checked)
                    {
                        dal_IWRS.IWRS_KITS_CENTRAL_SP(
                                ACTION: "UNRETENTION_CENTRAL_KIT",
                                KITNO: KITNO,
                                UNRETENTIONCOMM: txtRetentionComments.Text,
                                TABLENAME: TABLENAME
                            );

                        SEND_MAIL_UNRETENTION(KITNO, Location);
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

        private void SEND_MAIL_UNRETENTION(string KITNO ,string Location)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_CENTRAL_RETENTION");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_RETENTION_CENTRAL");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    Location = Location.Replace("[Location]", Location);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[KITNO]", KITNO);
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
                            ACTION: "CENTRAL_RETENTION_EXPIRE",
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