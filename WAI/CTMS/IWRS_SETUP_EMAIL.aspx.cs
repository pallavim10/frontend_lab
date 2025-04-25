using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class IWRS_SETUP_EMAIL : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //GET_SITE();

                    GET_EMAILIDS("UNBLIND");
                    GET_EMAILIDS("UNBLINDTREAT");
                    GET_EMAILIDS("DCF");

                    GET_EMAILIDS("BakKit");
                    GET_EMAILIDS("KIT_CENTRAL");
                    GET_EMAILIDS("KIT_CENTRAL_QUARATINE");
                    GET_EMAILIDS("KIT_CENTRAL_REJECT");
                    GET_EMAILIDS("KIT_CENTRAL_DESTROY");
                    GET_EMAILIDS("KIT_CENTRAL_RETURN");
                    GET_EMAILIDS("KIT_CENTRAL_DAMAGE");
                    GET_EMAILIDS("KIT_CENTRAL_BLOCK");
                    GET_EMAILIDS("KIT_CENTRAL_RETENTION");
                    GET_EMAILIDS("KIT_CENTRAL_EXPIRED");
                    GET_EMAILIDS("KIT_COUNTRY");
                    GET_EMAILIDS("KIT_COUNTRY_QUARATINE");
                    GET_EMAILIDS("KIT_COUNTRY_REJECT");
                    GET_EMAILIDS("KIT_COUNTRY_DESTROY");
                    GET_EMAILIDS("KIT_COUNTRY_RETURN");
                    GET_EMAILIDS("KIT_COUNTRY_DAMAGE");
                    GET_EMAILIDS("KIT_COUNTRY_BLOCK");
                    GET_EMAILIDS("KIT_COUNTRY_RETENTION");
                    GET_EMAILIDS("KIT_COUNTRY_EXPIRED");
                    GET_EMAILIDS("KIT_SITE");
                    GET_EMAILIDS("KIT_SITE_QUARATINE");
                    GET_EMAILIDS("KIT_SITE_REJECT");
                    GET_EMAILIDS("KIT_SITE_DESTROY");
                    GET_EMAILIDS("KIT_SITE_RETURN");
                    GET_EMAILIDS("KIT_SITE_DAMAGE");
                    GET_EMAILIDS("KIT_SITE_BLOCK");
                    GET_EMAILIDS("KIT_SITE_RETENTION");
                    GET_EMAILIDS("KIT_SITE_EXPIRED");

                    GET_EMAILIDS("SETUP_REVIEW");
                    GET_EMAILIDS("SETUP_UNREVIEW");
                    GET_EMAILIDS("GENERATE_EXPIRY_UPDATE");
                    GET_EMAILIDS("REQUEST_UPDATE_EXPIRY_STATUS");


                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //private void GET_SITE()
        //{
        //    try
        //    {
        //        DataSet ds = dal.GET_INVID_SP();
        //        gvSites.DataSource = ds;
        //        gvSites.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        //Get Email ID 

        private void GET_EMAILIDS(string TYPE)
        {
            try
            {
                switch (TYPE)
                {
                    case "UNBLIND":
                        DataSet ds = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS", STRATA: "UNBLIND");
                        gvUnblindEmailds.DataSource = ds;
                        gvUnblindEmailds.DataBind();
                        break;

                    case "DCF":
                        DataSet ds1 = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS", STRATA: "DCF");
                        gvDCFEmailds.DataSource = ds1;
                        gvDCFEmailds.DataBind();
                        break;

                    case "UNBLINDTREAT":
                        DataSet ds2 = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAILS", STRATA: "UNBLINDTREAT");
                        gvUnblindTreatEmailds.DataSource = ds2;
                        gvUnblindTreatEmailds.DataBind();
                        break;

                    case "KIT_CENTRAL":
                        DataSet ds3 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCentral.DataSource = ds3;
                        gvEmaildsCentral.DataBind();
                        break;

                    case "KIT_CENTRAL_QUARATINE":
                        DataSet ds12 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCentralQuaratine.DataSource = ds12;
                        gvEmaildsCentralQuaratine.DataBind();
                        break;

                    case "KIT_CENTRAL_BLOCK":
                        DataSet ds13 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCentralBlocked.DataSource = ds13;
                        gvEmaildsCentralBlocked.DataBind();
                        break;

                    case "KIT_CENTRAL_DAMAGE":
                        DataSet ds14 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCentralDamaged.DataSource = ds14;
                        gvEmaildsCentralDamaged.DataBind();
                        break;

                    case "KIT_CENTRAL_RETURN":
                        DataSet ds15 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCentralReturned.DataSource = ds15;
                        gvEmaildsCentralReturned.DataBind();
                        break;

                    case "KIT_CENTRAL_DESTROY":
                        DataSet ds16 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCentralDestroyed.DataSource = ds16;
                        gvEmaildsCentralDestroyed.DataBind();
                        break;

                    case "KIT_CENTRAL_REJECT":
                        DataSet ds17 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCentralRejected.DataSource = ds17;
                        gvEmaildsCentralRejected.DataBind();
                        break;

                    case "KIT_CENTRAL_RETENTION":
                        DataSet ds26 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCentralRetention.DataSource = ds26;
                        gvEmaildsCentralRetention.DataBind();
                        break;

                    case "KIT_CENTRAL_EXPIRED":
                        DataSet ds27 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCentralExpired.DataSource = ds27;
                        gvEmaildsCentralExpired.DataBind();
                        break;

                    case "KIT_COUNTRY":
                        DataSet ds4 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCountry.DataSource = ds4;
                        gvEmaildsCountry.DataBind();
                        break;

                    case "KIT_COUNTRY_QUARATINE":
                        DataSet ds18 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCountryQuaratine.DataSource = ds18;
                        gvEmaildsCountryQuaratine.DataBind();
                        break;

                    case "KIT_COUNTRY_BLOCK":
                        DataSet ds19 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsCountryBlock.DataSource = ds19;
                        gvEmaildsCountryBlock.DataBind();
                        break;

                    case "KIT_COUNTRY_DAMAGE":
                        DataSet ds20 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvCountryDamage.DataSource = ds20;
                        gvCountryDamage.DataBind();
                        break;

                    case "KIT_COUNTRY_RETURN":
                        DataSet ds21 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvCountryReturn.DataSource = ds21;
                        gvCountryReturn.DataBind();
                        break;

                    case "KIT_COUNTRY_DESTROY":
                        DataSet ds22 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvCountryDestroy.DataSource = ds22;
                        gvCountryDestroy.DataBind();
                        break;

                    case "KIT_COUNTRY_REJECT":
                        DataSet ds23 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvCountryReject.DataSource = ds23;
                        gvCountryReject.DataBind();
                        break;

                    case "KIT_COUNTRY_RETENTION":
                        DataSet ds25 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvCountryRetention.DataSource = ds25;
                        gvCountryRetention.DataBind();
                        break;

                    case "KIT_COUNTRY_EXPIRED":
                        DataSet ds28 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvCountryExpired.DataSource = ds28;
                        gvCountryExpired.DataBind();
                        break;

                    case "KIT_SITE":
                        DataSet ds5 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsSite.DataSource = ds5;
                        gvEmaildsSite.DataBind();
                        break;

                    case "KIT_SITE_QUARATINE":
                        DataSet ds6 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsSITEQuaratine.DataSource = ds6;
                        gvEmaildsSITEQuaratine.DataBind();
                        break;

                    case "KIT_SITE_BLOCK":
                        DataSet ds7 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsSITEBlocked.DataSource = ds7;
                        gvEmaildsSITEBlocked.DataBind();
                        break;

                    case "KIT_SITE_DAMAGE":
                        DataSet ds8 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsSITEDamaged.DataSource = ds8;
                        gvEmaildsSITEDamaged.DataBind();
                        break;

                    case "KIT_SITE_RETURN":
                        DataSet ds9 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsSITEReturned.DataSource = ds9;
                        gvEmaildsSITEReturned.DataBind();
                        break;

                    case "KIT_SITE_DESTROY":
                        DataSet ds10 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsSITEDestroyed.DataSource = ds10;
                        gvEmaildsSITEDestroyed.DataBind();
                        break;

                    case "KIT_SITE_REJECT":
                        DataSet ds11 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsSITERejected.DataSource = ds11;
                        gvEmaildsSITERejected.DataBind();
                        break;

                    case "KIT_SITE_RETENTION":
                        DataSet ds24 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsSiteRetention.DataSource = ds24;
                        gvEmaildsSiteRetention.DataBind();
                        break;

                    case "KIT_SITE_EXPIRED":
                        DataSet ds29 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmaildsSiteExpired.DataSource = ds29;
                        gvEmaildsSiteExpired.DataBind();
                        break;

                    case "SETUP_REVIEW":
                        DataSet ds31 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmailSetupReview.DataSource = ds31;
                        gvEmailSetupReview.DataBind();
                        break;

                    case "SETUP_UNREVIEW":
                        DataSet ds32 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmailSetupUnreview.DataSource = ds32;
                        gvEmailSetupUnreview.DataBind();
                        break;

                    case "GENERATE_EXPIRY_UPDATE":
                        DataSet ds33 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmailUpdateExpiry.DataSource = ds33;
                        gvEmailUpdateExpiry.DataBind();
                        break;



                    case "REQUEST_UPDATE_EXPIRY_STATUS":
                        DataSet ds34 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvEmailAppRejExpiry.DataSource = ds34;
                        gvEmailAppRejExpiry.DataBind();
                        break;

                    default:
                        DataSet ds30 = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: TYPE);
                        gvBakEmailds.DataSource = ds30;
                        gvBakEmailds.DataBind();
                        break;


                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_BakKit_EMAILIDS()
        {
            try
            {
                for (int a = 0; a < gvBakEmailds.Rows.Count; a++)
                {
                    Label lblSiteID = gvBakEmailds.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvBakEmailds.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvBakEmailds.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvBakEmailds.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_BakKit_EMAILIDS",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitEmail_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_BakKit_EMAILIDS();
                GET_EMAILIDS("BakKit");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Back-Up Kit Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        


        //Insert Unblinded Email ID method
        private void INSERT_UNBLIND_EMAILIDS()
        {
            try
            {
                for (int a = 0; a < gvUnblindEmailds.Rows.Count; a++)
                {
                    Label lblSiteID = gvUnblindEmailds.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvUnblindEmailds.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvUnblindEmailds.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvUnblindEmailds.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_UNBLIND_EMAILIDS",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString(),
                        SITEID: lblSiteID.Text

                        );
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitUnblindEmails_Click(object sender, EventArgs e)
        {
            try
            {
                bool EMAILIDs = true;
                bool CCEMAILIDs = true;
                bool BCCEMAILIDs = true;
                for (int a = 0; a < gvUnblindEmailds.Rows.Count; a++)
                {
                    Label lblSiteID = gvUnblindEmailds.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvUnblindEmailds.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvUnblindEmailds.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvUnblindEmailds.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;
                    Regex regex = new Regex(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*(\+[a-z0-9-]+)?@[a-z0-9-]+(\.[a-z0-9-]+)*$");
                    Match match = regex.Match(txtEMAILIDs.Text);
                    List<string> emailArray = new List<string>();
                    if (txtEMAILIDs.Text != null && txtEMAILIDs.Text != "")
                    {
                        if (txtEMAILIDs.Text.IndexOf(",") != -1)
                        {
                            emailArray = txtEMAILIDs.Text.Split(',').ToList();
                        }
                        else
                        {
                            emailArray.Add(txtEMAILIDs.Text);
                        }
                        foreach (string email in emailArray)
                        {
                            if (!regex.IsMatch(email ?? ""))
                            {
                                EMAILIDs = false;
                            }
                        }
                    }
                    Match match1 = regex.Match(txtCCEMAILIDs.Text);
                    List<string> emailArray1 = new List<string>();
                    if (txtCCEMAILIDs.Text != null && txtCCEMAILIDs.Text != "")
                    {
                        if (txtCCEMAILIDs.Text.IndexOf(",") != -1)
                        {
                            emailArray1 = txtCCEMAILIDs.Text.Split(',').ToList();
                        }
                        else
                        {
                            emailArray1.Add(txtCCEMAILIDs.Text);
                        }
                        foreach (string email in emailArray1)
                        {
                            if (!regex.IsMatch(email ?? ""))
                            {
                                CCEMAILIDs = false;
                            }
                        }
                    }
                    Match match2 = regex.Match(txtBCCEMAILIDs.Text);
                    List<string> emailArray2 = new List<string>();
                    if (txtBCCEMAILIDs.Text != null && txtBCCEMAILIDs.Text != "")
                    {
                        if (txtBCCEMAILIDs.Text.IndexOf(",") != -1)
                        {
                            emailArray2 = txtBCCEMAILIDs.Text.Split(',').ToList();
                        }
                        else
                        {
                            emailArray2.Add(txtBCCEMAILIDs.Text);
                        }
                        foreach (string email in emailArray2)
                        {
                            if (!regex.IsMatch(email ?? ""))
                            {
                                BCCEMAILIDs = false;
                            }
                        }
                    }
                    if (EMAILIDs == true && CCEMAILIDs == true && BCCEMAILIDs == true)
                    {
                        INSERT_UNBLIND_EMAILIDS();
                        GET_EMAILIDS("UNBLIND");

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Unblinding(Without Treatment Arm) Email IDs Defined Successfully.'); ", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Unblinding (Without Treatment Arm) Please Enter Valid Mail ID.'); ", true);
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        


        //Insert Unblinded Treat Email ID method
        private void INSERT_UNBLINDTREAT_EMAILIDS()
        {
            try
            {
                for (int a = 0; a < gvUnblindTreatEmailds.Rows.Count; a++)
                {
                    Label lblSiteID = gvUnblindTreatEmailds.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvUnblindTreatEmailds.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvUnblindTreatEmailds.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvUnblindTreatEmailds.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_UNBLINDTREAT_EMAILIDS",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString(),
                        SITEID: lblSiteID.Text
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitTreatUnblindEmails_Click(object sender, EventArgs e)
        {
            try
            {
                bool EMAILIDs = true;
                bool CCEMAILIDs = true;
                bool BCCEMAILIDs = true;
                for (int a = 0; a < gvUnblindTreatEmailds.Rows.Count; a++)
                {
                    Label lblSiteID = gvUnblindTreatEmailds.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvUnblindTreatEmailds.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvUnblindTreatEmailds.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvUnblindTreatEmailds.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;
                    Regex regex = new Regex(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*(\+[a-z0-9-]+)?@[a-z0-9-]+(\.[a-z0-9-]+)*$");
                    Match match = regex.Match(txtEMAILIDs.Text);
                    List<string> emailArray = new List<string>();
                    if (txtEMAILIDs.Text != null && txtEMAILIDs.Text != "")
                    {
                        if (txtEMAILIDs.Text.IndexOf(",") != -1)
                        {
                            emailArray = txtEMAILIDs.Text.Split(',').ToList();
                        }
                        else
                        {
                            emailArray.Add(txtEMAILIDs.Text);
                        }
                        foreach (string email in emailArray)
                        {
                            if (!regex.IsMatch(email ?? ""))
                            {
                                EMAILIDs = false;
                            }
                        }
                    }
                    Match match1 = regex.Match(txtCCEMAILIDs.Text);
                    List<string> emailArray1 = new List<string>();
                    if (txtCCEMAILIDs.Text != null && txtCCEMAILIDs.Text != "")
                    {
                        if (txtCCEMAILIDs.Text.IndexOf(",") != -1)
                        {
                            emailArray1 = txtCCEMAILIDs.Text.Split(',').ToList();
                        }
                        else
                        {
                            emailArray1.Add(txtCCEMAILIDs.Text);
                        }
                        foreach (string email in emailArray1)
                        {
                            if (!regex.IsMatch(email ?? ""))
                            {
                                CCEMAILIDs = false;
                            }
                        }
                    }
                    Match match2 = regex.Match(txtBCCEMAILIDs.Text);
                    List<string> emailArray2 = new List<string>();
                    if (txtBCCEMAILIDs.Text != null && txtBCCEMAILIDs.Text != "")
                    {
                        if (txtBCCEMAILIDs.Text.IndexOf(",") != -1)
                        {
                            emailArray2 = txtBCCEMAILIDs.Text.Split(',').ToList();
                        }
                        else
                        {
                            emailArray2.Add(txtBCCEMAILIDs.Text);
                        }
                        foreach (string email in emailArray2)
                        {
                            if (!regex.IsMatch(email ?? ""))
                            {
                                BCCEMAILIDs = false;
                            }
                        }
                    }
                    if (EMAILIDs == true && CCEMAILIDs == true && BCCEMAILIDs == true)
                    {
                        INSERT_UNBLINDTREAT_EMAILIDS();
                        GET_EMAILIDS("UNBLINDTREAT");

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert(' Unblinding(With Treatment Arm) Email IDs Defined Successfully.'); ", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Unblinding (With Treatment Arm) Please Enter Valid Mail ID.'); ", true);
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        


        //Insert DCF Email ID method
        private void INSERT_DCF_EMAILIDS()
        {
            try
            {
                for (int a = 0; a < gvDCFEmailds.Rows.Count; a++)
                {
                    Label lblSiteID = gvDCFEmailds.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvDCFEmailds.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvDCFEmailds.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvDCFEmailds.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_DCF_EMAILIDS",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString(),
                        SITEID: lblSiteID.Text
                        );
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitDCFEmails_Click(object sender, EventArgs e)
        {
            try
            {
                bool EMAILIDs = true;
                bool CCEMAILIDs = true;
                bool BCCEMAILIDs = true;
                for (int a = 0; a < gvDCFEmailds.Rows.Count; a++)
                {
                    Label lblSiteID = gvDCFEmailds.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvDCFEmailds.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvDCFEmailds.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvDCFEmailds.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;
                    Regex regex = new Regex(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*(\+[a-z0-9-]+)?@[a-z0-9-]+(\.[a-z0-9-]+)*$");
                    Match match = regex.Match(txtEMAILIDs.Text);
                    List<string> emailArray = new List<string>();
                    if (txtEMAILIDs.Text != null && txtEMAILIDs.Text != "")
                    {
                        if (txtEMAILIDs.Text.IndexOf(",") != -1)
                        {
                            emailArray = txtEMAILIDs.Text.Split(',').ToList();
                        }
                        else
                        {
                            emailArray.Add(txtEMAILIDs.Text);
                        }
                        foreach (string email in emailArray)
                        {
                            if (!regex.IsMatch(email ?? ""))
                            {
                                EMAILIDs = false;
                            }
                        }
                    }
                    Match match1 = regex.Match(txtCCEMAILIDs.Text);
                    List<string> emailArray1 = new List<string>();
                    if (txtCCEMAILIDs.Text != null && txtCCEMAILIDs.Text != "")
                    {
                        if (txtCCEMAILIDs.Text.IndexOf(",") != -1)
                        {
                            emailArray1 = txtCCEMAILIDs.Text.Split(',').ToList();
                        }
                        else
                        {
                            emailArray1.Add(txtCCEMAILIDs.Text);
                        }
                        foreach (string email in emailArray1)
                        {
                            if (!regex.IsMatch(email ?? ""))
                            {
                                CCEMAILIDs = false;
                            }
                        }
                    }
                    Match match2 = regex.Match(txtBCCEMAILIDs.Text);
                    List<string> emailArray2 = new List<string>();
                    if (txtBCCEMAILIDs.Text != null && txtBCCEMAILIDs.Text != "")
                    {
                        if (txtBCCEMAILIDs.Text.IndexOf(",") != -1)
                        {
                            emailArray2 = txtBCCEMAILIDs.Text.Split(',').ToList();
                        }
                        else
                        {
                            emailArray2.Add(txtBCCEMAILIDs.Text);
                        }
                        foreach (string email in emailArray2)
                        {
                            if (!regex.IsMatch(email ?? ""))
                            {
                                BCCEMAILIDs = false;
                            }
                        }
                    }
                    if (EMAILIDs == true && CCEMAILIDs == true && BCCEMAILIDs == true)
                    {
                        INSERT_DCF_EMAILIDS();
                        GET_EMAILIDS("DCF");

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('DCF Email IDs Defined Successfully.'); ", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('DCF Email IDs Please Enter Valid Mail Id.'); ", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        //Define Reason for Back-Up Kit Ends

        private void INSERT_EMAILIDS_CENTRAL()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCentral.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmaildsCentral.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCentral.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCentral.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_EMAILIDS_COUNTRY()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCountry.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsCountry.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsCountry.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCountry.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCountry.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        private void INSERT_EMAILIDS_SITE()
        {
            try
            {
                for (int a = 0; a < gvEmaildsSite.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsSite.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsSite.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsSite.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsSite.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitEmailCentral_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_CENTRAL();
                GET_EMAILIDS("KIT_CENTRAL");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Central Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        protected void btnSubmitEmailCountry_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_COUNTRY();
                GET_EMAILIDS("KIT_COUNTRY");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Country Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        protected void btnSubmitEmailSite_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_SITE();
                GET_EMAILIDS("KIT_SITE");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Site Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        


        protected void btnSubmitCentralQuaratine_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_CENTRAL_QUARATINE();
                GET_EMAILIDS("KIT_CENTRAL_QUARATINE");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Central Quaratine Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_EMAILIDS_CENTRAL_QUARATINE()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCentralQuaratine.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmaildsCentralQuaratine.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCentralQuaratine.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCentralQuaratine.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_QUARATINE",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_CENTRAL_BLOCKED()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCentralBlocked.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmaildsCentralBlocked.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCentralBlocked.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCentralBlocked.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_BLOCK",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitCentralBlocked_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_CENTRAL_BLOCKED();
                GET_EMAILIDS("KIT_CENTRAL_BLOCK");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Central Block Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCencelCentralBlocked_Click(object sender, EventArgs e)
        {
            try
            {
                GET_EMAILIDS("KIT_CENTRAL_BLOCK");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_EMAILIDS_CENTRAL_DAMAGED()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCentralDamaged.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmaildsCentralDamaged.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCentralDamaged.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCentralDamaged.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_DAMAGE",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitCentralDamaged_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_CENTRAL_DAMAGED();
                GET_EMAILIDS("KIT_CENTRAL_DAMAGE");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Central Damage Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_CENTRAL_RETURNED()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCentralReturned.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmaildsCentralReturned.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCentralReturned.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCentralReturned.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_RETURN",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitCentralReturned_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_CENTRAL_RETURNED();
                GET_EMAILIDS("KIT_CENTRAL_RETURN");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Central Return Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_CENTRAL_DESTROYED()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCentralDestroyed.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmaildsCentralDestroyed.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCentralDestroyed.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCentralDestroyed.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_DESTROY",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitCentralDestroyed_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_CENTRAL_DESTROYED();
                GET_EMAILIDS("KIT_CENTRAL_DESTROY");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Central Destroy Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_CENTRAL_REJECTED()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCentralRejected.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmaildsCentralRejected.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCentralRejected.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCentralRejected.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_REJECT",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitCentralRejected_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_CENTRAL_REJECTED();
                GET_EMAILIDS("KIT_CENTRAL_REJECT");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Central Reject Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_COUNTRY_QUARATINE()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCountryQuaratine.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsCountryQuaratine.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsCountryQuaratine.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCountryQuaratine.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCountryQuaratine.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_QUARATINE",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitEmailCountryQuaratine_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_COUNTRY_QUARATINE();
                GET_EMAILIDS("KIT_COUNTRY_QUARATINE");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Country Quaratine Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_COUNTRY_BLOCK()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCountryBlock.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsCountryBlock.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsCountryBlock.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCountryBlock.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCountryBlock.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_BLOCK",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitEmailCountryBlock_Click(object sender, EventArgs e)
        {

            try
            {
                INSERT_EMAILIDS_COUNTRY_BLOCK();
                GET_EMAILIDS("KIT_COUNTRY_BLOCK");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Country Block Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_COUNTRY_DAMAGE()
        {
            try
            {
                for (int a = 0; a < gvCountryDamage.Rows.Count; a++)
                {
                    Label lblSiteID = gvCountryDamage.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvCountryDamage.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvCountryDamage.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvCountryDamage.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_DAMAGE",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitCountryDamage_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_COUNTRY_DAMAGE();
                GET_EMAILIDS("KIT_COUNTRY_DAMAGE");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Country Damage Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_COUNTRY_RETURN()
        {
            try
            {
                for (int a = 0; a < gvCountryReturn.Rows.Count; a++)
                {
                    Label lblSiteID = gvCountryReturn.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvCountryReturn.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvCountryReturn.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvCountryReturn.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_RETURN",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitCountryReturn_Click(object sender, EventArgs e)
        {

            try
            {
                INSERT_EMAILIDS_COUNTRY_RETURN();
                GET_EMAILIDS("KIT_COUNTRY_RETURN");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Country Return Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

       

        private void INSERT_EMAILIDS_COUNTRY_DESTROY()
        {
            try
            {
                for (int a = 0; a < gvCountryDestroy.Rows.Count; a++)
                {
                    Label lblSiteID = gvCountryDestroy.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvCountryDestroy.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvCountryDestroy.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvCountryDestroy.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_DESTROY",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
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

                INSERT_EMAILIDS_COUNTRY_DESTROY();
                GET_EMAILIDS("KIT_COUNTRY_DESTROY");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Country Destroy Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_COUNTRY_REJECT()
        {
            try
            {
                for (int a = 0; a < gvCountryReject.Rows.Count; a++)
                {
                    Label lblSiteID = gvCountryReject.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvCountryReject.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvCountryReject.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvCountryReject.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_REJECT",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitReject_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_COUNTRY_REJECT();
                GET_EMAILIDS("KIT_COUNTRY_REJECT");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Country Reject Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        

        private void INSERT_EMAILIDS_SITE_QUARATINE()
        {
            try
            {
                for (int a = 0; a < gvEmaildsSITEQuaratine.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsSITEQuaratine.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsSITEQuaratine.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsSITEQuaratine.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsSITEQuaratine.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_QUARATINE",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitSiteQuaratine_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_SITE_QUARATINE();
                GET_EMAILIDS("KIT_SITE_QUARATINE");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Site Quaratine Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_SITE_BLOCK()
        {
            try
            {
                for (int a = 0; a < gvEmaildsSITEBlocked.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsSITEBlocked.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsSITEBlocked.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsSITEBlocked.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsSITEBlocked.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_BLOCK",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmutSiteBlock_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_SITE_BLOCK();
                GET_EMAILIDS("KIT_SITE_BLOCK");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Site Block Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_SITE_DAMAGE()
        {
            try
            {
                for (int a = 0; a < gvEmaildsSITEDamaged.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsSITEDamaged.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsSITEDamaged.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsSITEDamaged.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsSITEDamaged.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_DAMAGE",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void BtnSubmitSiteDamage_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_SITE_DAMAGE();
                GET_EMAILIDS("KIT_SITE_DAMAGE");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Site Damage Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_SITE_RETURN()
        {
            try
            {
                for (int a = 0; a < gvEmaildsSITEReturned.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsSITEReturned.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsSITEReturned.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsSITEReturned.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsSITEReturned.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_RETURN",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void BtnSubmitSiteReturn_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_SITE_RETURN();
                GET_EMAILIDS("KIT_SITE_RETURN");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Site Return Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_SITE_DESTROY()
        {
            try
            {
                for (int a = 0; a < gvEmaildsSITEDestroyed.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsSITEDestroyed.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsSITEDestroyed.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsSITEDestroyed.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsSITEDestroyed.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_DESTROY",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void BtnSubmitSiteDestroy_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_SITE_DESTROY();
                GET_EMAILIDS("KIT_SITE_DESTROY");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Site Destroy Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_SITE_REJECT()
        {
            try
            {
                for (int a = 0; a < gvEmaildsSITERejected.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsSITERejected.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsSITERejected.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsSITERejected.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsSITERejected.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_REJECT",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void BtnSubmitSiteReject_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_SITE_REJECT();
                GET_EMAILIDS("KIT_SITE_REJECT");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Site Reject Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        protected void btncentralRetentionSubmit_Click(object sender, EventArgs e)
        {
            INSERT_EMAILIDS_CENTRAL_RETENTION();
            GET_EMAILIDS("KIT_CENTRAL_RETENTION");

        }
        private void INSERT_EMAILIDS_CENTRAL_RETENTION()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCentralRetention.Rows.Count; a++)
                {

                    TextBox txtEMAILIDs = gvEmaildsCentralRetention.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCentralRetention.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCentralRetention.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_RETENTION",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: "0",
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Central Retenation Level Email IDs Updated Successfully'); ", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncentralRetentionCancel_Click(object sender, EventArgs e)
        {
            GET_EMAILIDS("KIT_CENTRAL_RETENTION");
        }

        

        protected void BtnSubmitCountryRetention_Click(object sender, EventArgs e)
        {
            INSERT_EMAILIDS_COUNTRY_RETENTION();
            GET_EMAILIDS("KIT_COUNTRY_RETENTION");

        }
        private void INSERT_EMAILIDS_COUNTRY_RETENTION()
        {
            try
            {
                for (int a = 0; a < gvCountryRetention.Rows.Count; a++)
                {
                    Label lblSiteID = gvCountryRetention.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvCountryRetention.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvCountryRetention.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvCountryRetention.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_RETENTION",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Country Retenation Level Email IDs Updated Successfully'); ", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitEmaildsSiteRetention_Click(object sender, EventArgs e)
        {
            INSERT_EMAILIDS_SITE_RETENTION();
            GET_EMAILIDS("KIT_SITE_RETENTION");
        }
        private void INSERT_EMAILIDS_SITE_RETENTION()
        {
            try
            {
                for (int a = 0; a < gvEmaildsSiteRetention.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsSiteRetention.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsSiteRetention.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsSiteRetention.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsSiteRetention.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_RETENTION",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Site Retenation Level Email IDs Updated Successfully'); ", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        


        //Insert Email ID Central Expired method
        private void INSERT_EMAILIDS_CENTRAL_EXPIRED()
        {
            try
            {
                for (int a = 0; a < gvEmaildsCentralExpired.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmaildsCentralExpired.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsCentralExpired.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsCentralExpired.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_EXPIRED",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitCentralExpired_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_CENTRAL_EXPIRED();
                GET_EMAILIDS("KIT_CENTRAL_EXPIRED");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Central Expired Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        


        //Insert Email ID Country Expired method
        private void INSERT_EMAILIDS_COUNTRY_EXPIRED()
        {
            try
            {
                for (int a = 0; a < gvCountryExpired.Rows.Count; a++)
                {
                    Label lblSiteID = gvCountryExpired.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvCountryExpired.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvCountryExpired.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvCountryExpired.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_EXPIRED",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Country Expired Level Email IDs Updated Successfully'); ", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BtnSubmitCountryExpired_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_COUNTRY_EXPIRED();
                GET_EMAILIDS("KIT_COUNTRY_EXPIRED");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Country Quaratine Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        //Insert Email ID Site Expired method
        private void INSERT_EMAILIDS_SITE_EXPIRED()
        {
            try
            {
                for (int a = 0; a < gvEmaildsSiteExpired.Rows.Count; a++)
                {
                    Label lblSiteID = gvEmaildsSiteExpired.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvEmaildsSiteExpired.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmaildsSiteExpired.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmaildsSiteExpired.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_EXPIRED",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitEmaildsSiteExpired_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_SITE_EXPIRED();
                GET_EMAILIDS("KIT_SITE_EXPIRED");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Kit Management At Site Expired Level Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_SETUP_REVIEW()
        {
            try
            {
                for (int a = 0; a < gvEmailSetupReview.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmailSetupReview.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmailSetupReview.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmailSetupReview.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SETUP_REVIEW",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitEmailSetupReview_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_SETUP_REVIEW();
                GET_EMAILIDS("SETUP_REVIEW");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Setup review email ids Define Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }


        


        private void INSERT_EMAILIDS_SETUP_UNREVIEW()
        {
            try
            {
                for (int a = 0; a < gvEmailSetupUnreview.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmailSetupUnreview.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmailSetupUnreview.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmailSetupUnreview.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SETUP_UNREVIEW",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitEmailSetupUnreview_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_SETUP_UNREVIEW();
                GET_EMAILIDS("SETUP_UNREVIEW");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Setup unreview email ids Define Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_EMAILIDS_GENERATE_EXPIRY_UPDATE()
        {
            try
            {
                for (int a = 0; a < gvEmailUpdateExpiry.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvEmailUpdateExpiry.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvEmailUpdateExpiry.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvEmailUpdateExpiry.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_GENERATE_EXPIRY_UPDATE",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitEmailUpdateExpiry_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_GENERATE_EXPIRY_UPDATE();
                GET_EMAILIDS("GENERATE_EXPIRY_UPDATE");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Email IDS for Expiry Updated Generated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        private void INSERT_EMAILIDS_REQUEST_UPDATE_EXPIRY_STATUS()
        {
            {
                try
                {
                    for (int a = 0; a < gvEmailAppRejExpiry.Rows.Count; a++)
                    {
                        TextBox txtEMAILIDs = gvEmailAppRejExpiry.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                        TextBox txtCCEMAILIDs = gvEmailAppRejExpiry.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                        TextBox txtBCCEMAILIDs = gvEmailAppRejExpiry.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                        dal_IWRS.IWRS_SET_EMAIL_SP
                            (
                            ACTION: "INSERT_EMAILIDS_REQUEST_UPDATE_EXPIRY_STATUS",
                            EMAIL_IDS: txtEMAILIDs.Text,
                            CCEMAIL_IDS: txtCCEMAILIDs.Text,
                            BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                            ENTEREDBY: Session["USER_ID"].ToString()
                            );
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
        }

        protected void btnSubmitEmailAppRejExpiry_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_EMAILIDS_REQUEST_UPDATE_EXPIRY_STATUS();
                GET_EMAILIDS("REQUEST_UPDATE_EXPIRY_STATUS");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Email IDS for Approval and Rejection Kit Expiry Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        

        protected void btnSubEmailFormate_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    string EMAILIDS = txtEmailFormateIDS.Text;
                    string CCEEMAILIDS = txtCCEMAILFormateIDs.Text;
                    string BCCEMAILIDS = txtBCCEMAILFormateIDs.Text;
                    string TYPE = "";
                    string SITEID = "";

                    string[] EMAILIDSList = txtEmailFormateIDS.Text.Split(',');

                    foreach (var email in EMAILIDSList)
                    {
                        string trimmedEmail1 = email.Trim();
                        SITEID = ExtractNumbers(trimmedEmail1);
                        
                    }

                    string[] CCEEMAILIDSList = txtCCEMAILFormateIDs.Text.Split(',');

                    foreach (var email in CCEEMAILIDSList)
                    {
                        string trimmedEmail2 = email.Trim();
                        SITEID = ExtractNumbers(trimmedEmail2);

                    }

                    string[] BCCEMAILIDSList = txtBCCEMAILFormateIDs.Text.Split(',');

                    foreach (var email in BCCEMAILIDSList)
                    {
                        string trimmedEmail3 = email.Trim();
                        SITEID = ExtractNumbers(trimmedEmail3);

                    }



                    List<string> selectedValues = new List<string>();

                    foreach (ListItem item in lstEmailFormate.Items)
                    {
                        if (item.Selected)
                        {
                            TYPE = item.Value;
                            UPDATE_EMAILIDS(TYPE, EMAILIDS, CCEEMAILIDS, BCCEMAILIDS, SITEID);
                        }
                    }
                    GET_CLEAR();

                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        static string ExtractNumbers(string input)
        {
            return Regex.Replace(input, @"\D", ""); // Replace non-digits with an empty string
        }

        private void GET_CLEAR()
        {
            txtEmailFormateIDS.Text ="";
            txtCCEMAILFormateIDs.Text = "";
            txtBCCEMAILFormateIDs.Text = "";
            lstEmailFormate.ClearSelection();
        }

        protected void btnCalEmailFormate_Click(object sender, EventArgs e)
        {
            GET_CLEAR();

        }

        private void UPDATE_EMAILIDS(string TYPE, string EMAILIDS, string CCEEMAILIDS,string BCCEMAILIDS,string SITEID)
        {
            try
            {
                switch (TYPE)
                {
                    case "UNBLIND":
                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_UNBLIND_EMAILIDS",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString(),
                        SITEID: SITEID

                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "DCF":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_DCF_EMAILIDS",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString(),
                        SITEID: SITEID
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "UNBLINDTREAT":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_UNBLINDTREAT_EMAILIDS",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString(),
                        SITEID: SITEID
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_CENTRAL":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_CENTRAL_QUARATINE":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_QUARATINE",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_CENTRAL_BLOCK":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_BLOCK",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );


                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_CENTRAL_DAMAGE":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_DAMAGE",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_CENTRAL_RETURN":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_RETURN",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_CENTRAL_DESTROY":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_DESTROY",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_CENTRAL_REJECT":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_REJECT",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_CENTRAL_RETENTION":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_RETENTION",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: "0",
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_CENTRAL_EXPIRED":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_CENTRAL_EXPIRED",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_COUNTRY":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_COUNTRY_QUARATINE":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_QUARATINE",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_COUNTRY_BLOCK":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_BLOCK",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_COUNTRY_DAMAGE":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_DAMAGE",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_COUNTRY_RETURN":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_RETURN",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_COUNTRY_DESTROY":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                       (
                       ACTION: "INSERT_EMAILIDS_COUNTRY_DESTROY",
                       EMAIL_IDS: EMAILIDS,
                       CCEMAIL_IDS: CCEEMAILIDS,
                       BCCEMAIL_IDS: BCCEMAILIDS,
                       SITEID: SITEID,
                       ENTEREDBY: Session["USER_ID"].ToString()
                       );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_COUNTRY_REJECT":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_REJECT",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_COUNTRY_RETENTION":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_RETENTION",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_COUNTRY_EXPIRED":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_COUNTRY_EXPIRED",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_SITE":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_SITE_QUARATINE":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_QUARATINE",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_SITE_BLOCK":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_BLOCK",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_SITE_DAMAGE":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_DAMAGE",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_SITE_RETURN":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_RETURN",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_SITE_DESTROY":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_DESTROY",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_SITE_REJECT":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_REJECT",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_SITE_RETENTION":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_RETENTION",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "KIT_SITE_EXPIRED":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SITE_EXPIRED",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "SETUP_REVIEW":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SETUP_REVIEW",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "SETUP_UNREVIEW":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_SETUP_UNREVIEW",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "GENERATE_EXPIRY_UPDATE":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_EMAILIDS_GENERATE_EXPIRY_UPDATE",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);

                        break;


                    case "REQUEST_UPDATE_EXPIRY_STATUS":

                        dal_IWRS.IWRS_SET_EMAIL_SP
                            (
                            ACTION: "INSERT_EMAILIDS_REQUEST_UPDATE_EXPIRY_STATUS",
                            EMAIL_IDS: EMAILIDS,
                            CCEMAIL_IDS: CCEEMAILIDS,
                            BCCEMAIL_IDS: BCCEMAILIDS,
                            ENTEREDBY: Session["USER_ID"].ToString()
                            );

                        GET_EMAILIDS(TYPE);

                        break;

                    case "BakKit":
                        dal_IWRS.IWRS_SET_EMAIL_SP
                        (
                        ACTION: "INSERT_BakKit_EMAILIDS",
                        EMAIL_IDS: EMAILIDS,
                        CCEMAIL_IDS: CCEEMAILIDS,
                        BCCEMAIL_IDS: BCCEMAILIDS,
                        SITEID: SITEID,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );

                        GET_EMAILIDS(TYPE);
                        break;

                    default:

                        GET_EMAILIDS(TYPE);

                        break;


                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}