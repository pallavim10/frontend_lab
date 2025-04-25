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
    public partial class NIWRS_KIT_EXPR_COUNTRY : System.Web.UI.Page
    {
        DAL dal = new DAL();

        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    GET_KITS();
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
                DataSet ds = dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "GET_KIT_EXPIRED_COUNTRY");
                gvKits.DataSource = ds;
                gvKits.DataBind();
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
                DataSet ds = dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "GET_KIT_EXPIRED_COUNTRY_EXPORT");
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
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

        protected void btnSubmitDestroy_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvKits.Rows.Count; i++)
                {
                    string KITNO = ((Label)gvKits.Rows[i].FindControl("KITNO")).Text;
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRY")).Text;
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
                            ACTION: "COUNTRY_EXPIRED_DESTROY",
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

        private void SEND_MAIL_RETURNED(string KITNO, string COUNTRY)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS", STRATA: "KIT_COUNTRY_RETURN", COUNTRY: COUNTRY);

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
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRY")).Text;
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
                            ACTION: "COUNTRY_EXPIRED_RETURN",
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

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_COUNTRY_EXPIRED-REVERT");

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
                    BODY = BODY.Replace("[Revert_AT]", "Country Depot");
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
                    string COUNTRYNAME = ((Label)gvKits.Rows[i].FindControl("COUNTRY")).Text;
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
                            ACTION: "COUNTRY_EXPIRED_REVERTTOUSE",
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

    }
}