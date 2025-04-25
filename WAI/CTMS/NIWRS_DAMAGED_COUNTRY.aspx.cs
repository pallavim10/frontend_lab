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
    public partial class NIWRS_DAMAGED_COUNTRY : System.Web.UI.Page
    {
        DAL dal = new DAL();

        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUnQuarantineComments.Attributes.Add("Maxlength", "200");
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
                DataSet ds = dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "GET_DAMAGED_COUNTRY", COUNTRYID: ddlCountry.SelectedValue);
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

        protected void gvKits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;

                Label ORDERID = (Label)gvKits.Rows[index].FindControl("ORDERID");
                Label COUNTRYNAME = (Label)gvKits.Rows[index].FindControl("COUNTRYNAME");
                Label KITNO = (Label)gvKits.Rows[index].FindControl("KITNO");

                if (e.CommandName == "UnQuarantine")
                {
                    dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "UNQUARANTINE_COUNTRY_KIT", KITNO: KITNO.Text);

                    SEND_MAIL_UNQUARANTINE(ORDERID.Text, COUNTRYNAME.Text, KITNO.Text);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Reverted to Use Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                GET_KITS();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL_UNQUARANTINE(string ORDERID, string COUNTRY, string KITNO)
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

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_DAMAGED_COUNTRY -UnQuarantine");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", COUNTRY);
                    SUBJECT = SUBJECT.Replace("[ORDERID]", ORDERID);
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", COUNTRY);
                    BODY = BODY.Replace("[ORDERID]", ORDERID);
                    BODY = BODY.Replace("[KITNO]", KITNO);
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
                    string ORDERID = ((Label)gvKits.Rows[i].FindControl("ORDERID")).Text;
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRYNAME")).Text;
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string TABLENAME = ((Label)gvKits.Rows[i].FindControl("TABLENAME")).Text;

                    CheckBox Chek_UnQuarantine = (CheckBox)gvKits.Rows[i].FindControl("Chek_UnQuarantine");

                    string selectedKitsCsv = hfKITS.Value;
                    if (selectedKitsCsv.Contains(KITNO))
                    {
                        Chek_UnQuarantine.Checked = true;
                    }

                    if (Chek_UnQuarantine.Checked)
                    {
                        dal_IWRS.IWRS_KITS_COUNTRY_SP(
                            ACTION: "UNQUARANTINE_COUNTRY_KIT",
                            KITNO: KITNO,
                            UNQUARANTINECOMM: txtUnQuarantineComments.Text,
                            TABLENAME: TABLENAME
                            );

                        SEND_MAIL_UNQUARANTINE(ORDERID, COUNTRYNAME, KITNO);
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
                DataSet ds = dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "GET_DAMAGED_COUNTRY_EXPORT", COUNTRYID: ddlCountry.SelectedValue);

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

        private void SEND_MAIL_EXPIRE(string KITNO, string COUNTRY)
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
                    BODY = BODY.Replace("[KITNO]", KITNO);
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
                            ACTION: "EXPIRE_DAMAGE_COUNTRY",
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

    }
}