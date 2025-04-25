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
    public partial class NIWRS_KITS_CENTRAL_REC : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtACCEPTComments.Attributes.Add("MaxLength", "200");
            txtQuarantineComments.Attributes.Add("MaxLength", "200");
            try
            {
                if (!IsPostBack)
                {
                    GET_COUNTRY_ORDERS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_COUNTRY_ORDERS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_CETNTRL_KIT_RECIEVE");
                gvOrders.DataSource = ds;
                gvOrders.DataBind();
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

                    DataSet ds = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_CETNTRL_ORDERS_KITS", ORDERID: ORDERID);
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
        private void SEND_MAIL_ACCEPT(string ORDERID, string RECEIPTDATE, string SHIPMENTID)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_CENTRAL");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_CENTRAL_REC");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[ORDERID]", ORDERID);
                    SUBJECT = SUBJECT.Replace("[RECEIPTDAT]", RECEIPTDATE);
                    SUBJECT = SUBJECT.Replace("[SHIPMENTID]", SHIPMENTID);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[ORDERID]", ORDERID);
                    BODY = BODY.Replace("[RECEIPTDAT]", RECEIPTDATE);
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
                    TextBox RECEIPTDATE = (TextBox)gvOrders.Rows[index].FindControl("RECEIPTDATE");

                    if (e.CommandName == "Accept")
                    {
                        hdnACCEPT_SHIPMENTID.Value = SHIPMENTID.Text;
                        hdnACCEPT_SHIPMENTDATE.Value = SHIPMENTDATE.Text;
                        hdnACCEPT_RECEIPTDATE.Value = RECEIPTDATE.Text;
                        hdnACCEPT_ORDERID.Value = e.CommandArgument.ToString();
                        
                        modalpopupACCEPT.Show();
                    }
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
                DataSet ds = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_CETNTRL_KIT_RECIEVE_EXPORT");
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvOrders_PreRender(object sender, EventArgs e)
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
                dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "ACCEPT_CENTRAL_ORDER", ORDERID: hdnACCEPT_ORDERID.Value, SHIPMENTID: hdnACCEPT_SHIPMENTID.Value, SHIPMENTDATE: hdnACCEPT_SHIPMENTDATE.Value, RECEIPTDATE: hdnACCEPT_RECEIPTDATE.Value, ACCEPTCOMM: txtACCEPTComments.Text);

                SEND_MAIL_ACCEPT(hdnACCEPT_ORDERID.Value, hdnACCEPT_RECEIPTDATE.Value, hdnACCEPT_SHIPMENTID.Value);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Order Accepted Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
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

        private void SEND_MAIL_QUARANTINE(string KITNO, string ORDERID)
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

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_COUNTRY_PENDING - Quarantine");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    
                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[ORDERID]", ORDERID);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    
                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[ORDERID]", ORDERID);
                    BODY = BODY.Replace("[QUARANTINED_AT]", "Central Order");
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


        protected void btnSubmitQuarantine_Click(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < gvOrders.Rows.Count; j++)
                {
                    
                    GridView gvKits = (GridView)gvOrders.Rows[j].FindControl("gvKits");
                    string ORDERID = ((Label)gvOrders.Rows[j].FindControl("ORDERID")).Text;
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
                            dal_IWRS.IWRS_KITS_CENTRAL_SP(
                                ACTION: "QUARANTINE_CENTRAL_KIT",
                                KITNO: KITNO,
                                QUARANTINECOMM: txtQuarantineComments.Text,
                                ORDERID: ORDERID
                                
                             );

                            SEND_MAIL_QUARANTINE(KITNO, ORDERID);
                        }
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

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL_BLOCK(string KITNO, string ORDERID)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_CENTRAL_BLOCK");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_COUNTRY_PENDING - Block");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());

                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[ORDERID]", ORDERID);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());

                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[ORDERID]", ORDERID);
                    BODY = BODY.Replace("[BLOCK_AT]", "Central Order");
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

        protected void btnSubmitBlock_Click(object sender, EventArgs e)
        {
            try
            {

                for (int j = 0; j < gvOrders.Rows.Count; j++)
                {
                    GridView gvKits = (GridView)gvOrders.Rows[j].FindControl("gvKits");
                    string ORDERID = ((Label)gvOrders.Rows[j].FindControl("ORDERID")).Text;
                    
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
                            dal_IWRS.IWRS_KITS_CENTRAL_SP(
                                ACTION: "BLOCK_CENTRAL_KIT",
                                KITNO: KITNO,
                                BLOCKEDCOMM: txtBlockComments.Text,
                                ORDERID: ORDERID
                                );

                            SEND_MAIL_BLOCK(KITNO, ORDERID);
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
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        private void SEND_MAIL_DAMAGE(string KITNO, string ORDERID)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_CENTRAL_BLOCK");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_COUNTRY_PENDING - Damage");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());

                    SUBJECT = SUBJECT.Replace("[KITNO]", KITNO);
                    SUBJECT = SUBJECT.Replace("[ORDERID]", ORDERID);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());

                    BODY = BODY.Replace("[KITNO]", KITNO);
                    BODY = BODY.Replace("[ORDERID]", ORDERID);
                    BODY = BODY.Replace("[DAMAGE_AT]", "Central Order");
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
                    GridView gvKits = (GridView)gvOrders.Rows[j].FindControl("gvKits");
                    string ORDERID = ((Label)gvOrders.Rows[j].FindControl("ORDERID")).Text;
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
                            dal_IWRS.IWRS_KITS_CENTRAL_SP(
                                ACTION: "DAMAGE_CENTRAL_KIT",
                                KITNO: KITNO,
                                DAMAGEDCOMM: txtDamageComments.Text,
                                ORDERID: ORDERID
                                );

                            SEND_MAIL_DAMAGE(KITNO, ORDERID);
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
    }
}