using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpecimenTracking.App_Code;
namespace SpecimenTracking
{
    public partial class UMT_Active_Deactive : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    GET_SITE();

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
                DataSet ds = dal_UMT.UMT_SITE_SP(
                    ACTION: "GET_SITE"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdSite.DataSource = ds.Tables[0];
                    grdSite.DataBind();
                }
                else
                {
                    grdSite.DataSource = null;
                    grdSite.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUserDetails_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void grdSite_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    Label lblID = (e.Row.FindControl("lblID") as Label);
                    Label lblActive = (e.Row.FindControl("lblActive") as Label);
                    LinkButton lbtActive = (e.Row.FindControl("lbtActive") as LinkButton);
                    LinkButton lbtDeactive = (e.Row.FindControl("lbtDeactive") as LinkButton);
                    HiddenField Active = (HiddenField)e.Row.FindControl("HiddenActive");
                    string hiddenFieldValue = Active.Value;

                    if (hiddenFieldValue == "True")
                    {
                        if (drv["ACTIVE_COUNTS"].ToString() != "0")
                        {
                            lblActive.Visible = true;
                            lbtDeactive.Visible = false;
                            lbtActive.Visible = false;
                        }
                        else
                        {
                            lblActive.Visible = false;
                            lbtDeactive.Visible = false;
                            lbtActive.Visible = true;
                        }
                    }
                    if (hiddenFieldValue == "False")
                    {
                        lbtDeactive.Visible = true;
                        lbtActive.Visible = false;
                        lblActive.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSite_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                ViewState["ID"] = id;

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;

                Label SITENAME = (Label)grdSite.Rows[index].FindControl("lblSiteName");
                Label SITEID = (Label)grdSite.Rows[index].FindControl("lblSiteId");


                if (e.CommandName == "ACTIVATION")
                {
                    ACTIVATION();
                    SEND_MAIL_ACTIVATION(SITEID.Text, SITENAME.Text);
                    GET_SITE();
                }
                else if (e.CommandName == "DEACTIVATION")
                {
                    DEACTIVATION();
                    SEND_MAIL_DEACTIVATION(SITEID.Text, SITENAME.Text);
                    GET_SITE();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DEACTIVATION()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(ACTION: "SITE_DEACTIVATE", ID: ViewState["ID"].ToString()
                    );
                string script = @"
                     swal({
                     title: 'Success!',
                     text: 'Site Deactivated Successfully.',
                     icon: 'success',
                     button: 'OK'
                     
                     });";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void SEND_MAIL_DEACTIVATION(string SITEID, string SITENAME)
        {
            try
            {
                CommonFunction cf = new CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal.UMT_EMAIL_SP(ACTION: "GET_SITE_ACTIVATION", ACTIONS: "Site Activation / Deactivation", SITEID: SITEID);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }
                DataSet dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "SITE_DEACTIVATION");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[SITEID]", SITEID);
                    SUBJECT = SUBJECT.Replace("[SITENAME]", SITENAME);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[SITEID]", SITEID);
                    BODY = BODY.Replace("[SITENAME]", SITENAME);
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

        private void SEND_MAIL_ACTIVATION(string SITEID, string SITENAME)
        {
            try
            {
                CommonFunction cf = new CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal.UMT_EMAIL_SP(ACTION: "GET_SITE_ACTIVATION", ACTIONS: "Site Activation / Deactivation");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }
                DataSet dsEmail = dal.UMT_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "SITE_ACTIVATION");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[SITEID]", SITEID);
                    SUBJECT = SUBJECT.Replace("[SITENAME]", SITENAME);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[SITEID]", SITEID);
                    BODY = BODY.Replace("[SITENAME]", SITENAME);
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
        private void ACTIVATION()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(ACTION: "SITE_ACTIVATE", ID: ViewState["ID"].ToString()
                   );

                string script = @"
                     swal({
                     title: 'Success!',
                     text: 'Site Activated Successfully.',
                     icon: 'success',
                     button: 'OK'                     
                     });";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbSiteDetailsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Site Details";

                DataSet ds = dal_UMT.UMT_LOG_SP(
                    ACTION: "GET_SITE"
                    );
                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}