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
    public partial class NIWRS_KITS_SITE_ORDER : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_COUNTRY();
                    GET_SITE();
                    GET_TREATMENT();
                    GET_NEW_ORDERID_SITE();
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
                        ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
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
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVID";
                        ddlSite.DataBind();
                    }
                    else
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVID";
                        ddlSite.DataBind();
                        ddlSite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_TREATMENT()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "GET_TREATMENTS_COUNTRY",
                    COUNTRYID: ddlCountry.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeatTreat.DataSource = ds.Tables[0];
                    repeatTreat.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_KITS_SITE_ORDER()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                bool kits = true;

                for (int i = 0; i < repeatTreat.Items.Count; i++)
                {
                    Label lblTreatment = (Label)repeatTreat.Items[i].FindControl("lblTreatment");

                    TextBox txtNoKits = (TextBox)repeatTreat.Items[i].FindControl("txtNoKits");

                    if (txtNoKits.Text != "")
                    {
                        DataSet dsKits = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_KITS_SITE_ORDER", TREAT_GRP: lblTreatment.Text, KITNO: txtNoKits.Text, COUNTRYID: ddlCountry.SelectedValue);

                        if (dsKits.Tables[0].Rows.Count.ToString() == txtNoKits.Text)
                        {
                            if (kits)
                            {
                                foreach (DataColumn colKit in dsKits.Tables[0].Columns)
                                {
                                    dt.Columns.Add(colKit.ColumnName.ToString());
                                }

                                ds.Tables.Add(dt);
                            }

                            ds.Tables[0].Merge(dsKits.Tables[0], true, MissingSchemaAction.Ignore);

                            kits = false;
                        }
                        else
                        {
                            kits = false;
                            btnGenOrder.Visible = false;

                            gvKITS.DataSource = null;
                            gvKITS.DataBind();

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insufficient Kits')", true);
                        }
                    }
                }

                if (kits)
                {
                    ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_KITS_SITE_ORDER", TREAT_GRP: "0", KITNO: "", COUNTRYID: ddlCountry.SelectedValue);
                }

                if (ds.Tables.Count > 0)
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.Sort = "KITNO ASC";

                    gvKITS.DataSource = dv;
                    gvKITS.DataBind();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        btnGenOrder.Visible = true;
                    }
                    else
                    {
                        btnGenOrder.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnGetKits_Click(object sender, EventArgs e)
        {
            try
            {
                GET_KITS_SITE_ORDER();
                int rowCount = gvKITS.Rows.Count;
                if (rowCount > 0)
                {
                    lblKitcount.Text = $"{rowCount}";
                    KitCount.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnGenOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (GENERATE_ORDER_SITE())
                {
                    SEND_MAIL();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Order Generated Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
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
                GET_NEW_ORDERID_SITE();
                GET_TREATMENT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlToSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_NEW_ORDERID_SITE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_NEW_ORDERID_SITE()
        {
            try
            {
                if (ddlCountry.SelectedValue != "0" && ddlSite.SelectedValue != "0")
                {
                    DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_NEW_ORDERID_SITE", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtOrderID.Text = ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        txtOrderID.Text = "";
                    }
                }
                else
                {
                    txtOrderID.Text = "";
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool GENERATE_ORDER_SITE()
        {
            bool Checked = false;

            try
            {
                for (int i = 0; i < gvKITS.Rows.Count; i++)
                {
                    CheckBox Chk_Sel = (CheckBox)gvKITS.Rows[i].FindControl("Chk_Sel");

                    if (Chk_Sel.Checked)
                    {
                        Checked = true;
                        HiddenField ID = (HiddenField)gvKITS.Rows[i].FindControl("ID");

                        dal_IWRS.IWRS_KITS_SITE_SP
                           (
                           ACTION: "GENERATE_ORDER_SITE",
                           ID: ID.Value,
                           COUNTRYID: ddlCountry.SelectedValue,
                           SITEID: ddlSite.SelectedValue,
                           ORDERID: txtOrderID.Text,
                           SHIPMENTID: txtShipmentID.Text
                           );
                    }
                }

                if (!Checked)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please Select Kits.'); ", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return Checked;
        }

        private void SEND_MAIL()
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_COUNTRY", SITEID: ddlSite.SelectedValue);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_ORDER");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[ORDERID]", txtOrderID.Text);
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", ddlCountry.SelectedItem.Text);
                    SUBJECT = SUBJECT.Replace("[SITEID]", ddlSite.SelectedValue);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[ORDERID]", txtOrderID.Text);
                    BODY = BODY.Replace("[COUNTRY]", ddlCountry.SelectedItem.Text);
                    BODY = BODY.Replace("[SITEID]", ddlSite.SelectedValue);
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

        protected void gvKITS_PreRender(object sender, EventArgs e)
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