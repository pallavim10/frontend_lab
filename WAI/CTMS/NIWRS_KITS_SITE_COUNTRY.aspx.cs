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
    public partial class NIWRS_KITS_SITE_COUNTRY : System.Web.UI.Page
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
                if (ddlSite.SelectedItem.Value != "0")
                {
                    DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_TREATMENTS_SITE",
                   SITEID: ddlSite.SelectedValue
                   );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        repeatTreat.DataSource = ds.Tables[0];
                        repeatTreat.DataBind();
                    }
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
                        DataSet dsKits = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_KITS_SITE_COUNTRY", TREAT_GRP: lblTreatment.Text, KITNO: txtNoKits.Text, SITEID: ddlSite.SelectedValue);

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
                    ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_KITS_SITE_COUNTRY", TREAT_GRP: "0", KITNO: "", SITEID: ddlSite.SelectedValue);
                }

                if (ds.Tables.Count > 0)
                {
                    gvKITS.DataSource = ds;
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
                if (ddlSite.SelectedItem.Value != "0")
                {
                    GET_KITS_SITE_ORDER();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert(' Please Select Site ID.'); ", true);
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

                GET_NEW_ORDERID_SITE();
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
                GET_TREATMENT();
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
                    DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_NEW_COUNTRY_ORDERID_SITE", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue);

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

                        Label KITNO = (Label)gvKITS.Rows[i].FindControl("KITNO");

                        dal_IWRS.IWRS_KITS_SITE_SP
                           (
                           ACTION: "GENERATE_COUNTRY_ORDER_SITE",
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

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_SITE_COUNTRY");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[COUNTRY]", ddlCountry.SelectedItem.Text);
                    SUBJECT = SUBJECT.Replace("[FROMSITE]", ddlSite.SelectedItem.Text);
                    SUBJECT = SUBJECT.Replace("[ORDERID]", txtOrderID.Text);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[COUNTRY]", ddlCountry.SelectedItem.Text);
                    BODY = BODY.Replace("[FROMSITE]", ddlSite.SelectedItem.Text);
                    BODY = BODY.Replace("[ORDERID]", txtOrderID.Text);
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