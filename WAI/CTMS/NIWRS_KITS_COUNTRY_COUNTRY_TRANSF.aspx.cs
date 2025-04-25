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
    public partial class NIWRS_KITS_COUNTRY_COUNTRY_TRANSF : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                COUNTRY();
                GET_COUNTRY();
                GET_NEW_ORDERID_COUNTRY_TRANSF();
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

                        ddlToCountry.DataSource = ds.Tables[0];
                        ddlToCountry.DataTextField = "COUNTRYNAME";
                        ddlToCountry.DataValueField = "COUNTRYID";
                        ddlToCountry.DataBind();

                    }
                    else
                    {

                        ddlToCountry.DataSource = ds.Tables[0];
                        ddlToCountry.DataTextField = "COUNTRYNAME";
                        ddlToCountry.DataValueField = "COUNTRYID";
                        ddlToCountry.DataBind();
                        ddlToCountry.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlfrmCountry.DataSource = ds.Tables[0];
                        ddlfrmCountry.DataTextField = "COUNTRYNAME";
                        ddlfrmCountry.DataValueField = "COUNTRYID";
                        ddlfrmCountry.DataBind();
                    }
                    else
                    {
                        ddlfrmCountry.DataSource = ds.Tables[0];
                        ddlfrmCountry.DataTextField = "COUNTRYNAME";
                        ddlfrmCountry.DataValueField = "COUNTRYID";
                        ddlfrmCountry.DataBind();

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
                GET_KITS_COUNTRY_TRANSF();
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
                if (GENERATE_ORDER_SITE_TRANSF())
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

        private void GET_KITS_COUNTRY_TRANSF()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "GET_KITS_COUNTRY_TRANSF", COUNTRYID: ddlfrmCountry.SelectedValue);
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

        protected void ddlfrmCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_COUNTRY();
                GET_NEW_ORDERID_COUNTRY_TRANSF();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlToCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_NEW_ORDERID_COUNTRY_TRANSF();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool GENERATE_ORDER_SITE_TRANSF()
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

                        dal_IWRS.IWRS_KITS_COUNTRY_SP
                           (
                           ACTION: "GENERATE_ORDER_COUNTRY_TRANSF",
                           ID: ID.Value,
                           COUNTRYID: ddlToCountry.SelectedValue,
                           KITNO: ddlfrmCountry.SelectedValue,
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

        private void GET_NEW_ORDERID_COUNTRY_TRANSF()
        {
            try
            {
                if (ddlfrmCountry.SelectedValue != "0" && ddlToCountry.SelectedValue != "0")
                {
                    DataSet ds = dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "GET_NEW_ORDERID_COUNTRY_TRANSF", COUNTRYID: ddlToCountry.SelectedValue, ID: ddlfrmCountry.SelectedValue);

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

        private void SEND_MAIL()
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "KIT_COUNTRY");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "NIWRS_KITS_COUNTRY_COUNTRY_TRANSF");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[FROMCOUNTRY]", ddlfrmCountry.SelectedItem.Text);
                    SUBJECT = SUBJECT.Replace("[TOCOUNTRY]", ddlToCountry.SelectedItem.Text);
                    SUBJECT = SUBJECT.Replace("[ORDERID]", txtOrderID.Text);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[FROMCOUNTRY]", ddlfrmCountry.SelectedItem.Text);
                    BODY = BODY.Replace("[TOCOUNTRY]", ddlToCountry.SelectedItem.Text);
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