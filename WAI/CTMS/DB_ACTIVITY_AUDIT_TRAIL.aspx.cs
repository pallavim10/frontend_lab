using CTMS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DB_ACTIVITY_AUDIT_TRAIL : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                {
                    GET_MODULES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlActivity.SelectedValue == "Master Library" || ddlActivity.SelectedValue == "Master Library Options")
                {
                    GET_MASTER_MODULES();
                    GET_MASTER_FIELDS();

                    divModules.Visible = true;
                    divFields.Visible = true;
                    divLab.Visible = false;
                    divRules.Visible = false;
                    divListing.Visible = false;
                    divListingCrit.Visible = false;
                    btnShow.Visible = true;
                }
                else if (ddlActivity.SelectedValue == "Modules" || ddlActivity.SelectedValue == "Conditional Visibility" || ddlActivity.SelectedValue == "OnSubmit OR OnLoad Criterias" || ddlActivity.SelectedValue == "OnSubmit OR OnLoad Criterias (Variables)" || ddlActivity.SelectedValue == "Code Mapping" || ddlActivity.SelectedValue == "Lab Reference Range Mapping")
                {
                    GET_MODULES();

                    divModules.Visible = true;
                    divFields.Visible = false;
                    divLab.Visible = false;
                    divRules.Visible = false;
                    divListing.Visible = false;
                    divListingCrit.Visible = false;
                    btnShow.Visible = true;
                }
                else if (ddlActivity.SelectedValue == "Fields" || ddlActivity.SelectedValue == "Field Options" || ddlActivity.SelectedValue == "OnChange Criterias")
                {
                    GET_MODULES();
                    GET_FIELDS();

                    divModules.Visible = true;
                    divFields.Visible = true;
                    divLab.Visible = false;
                    divRules.Visible = false;
                    divListing.Visible = false;
                    divListingCrit.Visible = false;
                    btnShow.Visible = true;
                }
                else if (ddlActivity.SelectedValue == "Labs" || ddlActivity.SelectedValue == "Lab Reference Range")
                {
                    GET_LABID();
                    divLab.Visible = true;
                    divRules.Visible = false;
                    divListing.Visible = false;
                    divListingCrit.Visible = false;
                    divModules.Visible = false;
                    divFields.Visible = false;
                    btnShow.Visible = true;
                }
                else if (ddlActivity.SelectedValue == "Rules")
                {
                    GET_RULEID();
                    divRules.Visible = true;
                    divLab.Visible = false;
                    divListing.Visible = false;
                    divListingCrit.Visible = false;
                    divModules.Visible = false;
                    divFields.Visible = false;
                    btnShow.Visible = true;
                }
                else if (ddlActivity.SelectedValue == "Listings")
                {
                    GET_LISTINGID();
                    divListing.Visible = true;
                    divListingCrit.Visible = false;
                    divLab.Visible = false;
                    divRules.Visible = false;
                    divModules.Visible = false;
                    divFields.Visible = false;
                    btnShow.Visible = true;
                }
                else if (ddlActivity.SelectedValue == "Listings Criterias")
                {
                    GET_LISTINGID();
                    divListing.Visible = true;
                    divListingCrit.Visible = true;
                    divLab.Visible = false;
                    divRules.Visible = false;
                    divModules.Visible = false;
                    divFields.Visible = false;
                    btnShow.Visible = true;
                }
                else
                {
                    divModules.Visible = false;
                    divFields.Visible = false;
                    divLab.Visible = false;
                    divRules.Visible = false;
                    divListing.Visible = false;
                    btnShow.Visible = false;
                    divListingCrit.Visible = false;
                }

                grdRecords.DataSource = null;
                grdRecords.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LABID()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(
                    ACTION: "GET_LAB_AUDITRAIL"
                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpLab.DataSource = ds.Tables[0];
                    drpLab.DataValueField = "LABID";
                    drpLab.DataTextField = "LABNAME";
                    drpLab.DataBind();
                    drpLab.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    drpLab.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_RULEID()
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(
                    ACTION: "GET_RULEID"
                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpRule.DataSource = ds.Tables[0];
                    drpRule.DataValueField = "RULEID";
                    drpRule.DataTextField = "RULEID";
                    drpRule.DataBind();
                    drpRule.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    drpRule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LISTINGID()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(
                    Action: "GET_LISTINGID_AUDITRAIL"
                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpListing.DataSource = ds.Tables[0];
                    drpListing.DataValueField = "ID";
                    drpListing.DataTextField = "NAME";
                    drpListing.DataBind();
                    drpListing.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    drpListing.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_MASTER_MODULES()
        {
            try
            {
                DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "GET_MASTER_MODULES");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds;
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataValueField = "DOMAIN";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_MODULES()
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULENAME");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds;
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataValueField = "ID";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlActivity.SelectedValue == "Master Library" || ddlActivity.SelectedValue == "Master Library Options")
                {
                    GET_MASTER_FIELDS();

                    divModules.Visible = true;
                    divFields.Visible = true;
                }
                else if (ddlActivity.SelectedValue == "Fields" || ddlActivity.SelectedValue == "Field Options" || ddlActivity.SelectedValue == "OnChange Criterias")
                {
                    GET_FIELDS();

                    divModules.Visible = true;
                    divFields.Visible = true;
                }

                grdRecords.DataSource = null;
                grdRecords.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_MASTER_FIELDS()
        {
            try
            {
                DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "GET_MASTERFIELD",
                    DOMAIN: drpModule.SelectedValue);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlFields.DataSource = ds;
                    ddlFields.DataTextField = "FIELDNAME";
                    ddlFields.DataValueField = "VARIABLENAME";
                    ddlFields.DataBind();
                    ddlFields.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlFields.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_FIELDS()
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULEFIELD_BYMODULEID",
                    MODULEID: drpModule.SelectedValue);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlFields.DataSource = ds;
                    ddlFields.DataTextField = "FIELDNAME";
                    ddlFields.DataValueField = "ID";
                    ddlFields.DataBind();
                    ddlFields.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlFields.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdRecords.DataSource = null;
                grdRecords.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdField_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void drpLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdRecords.DataSource = null;
                grdRecords.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdRecords.DataSource = null;
                grdRecords.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpListing_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdRecords.DataSource = null;
                grdRecords.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlListingCrit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdRecords.DataSource = null;
                grdRecords.DataBind();

                btnExportExcel.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_ACTIVITY_AUDIT_TRAIL_SP(ACTION: ddlActivity.SelectedValue,
                    MODULEID: drpModule.SelectedValue,
                    DOMAIN: drpModule.SelectedValue,
                    FIELDID: ddlFields.SelectedValue,
                    VARIABLENAME: ddlFields.SelectedValue,
                    LABID: drpLab.SelectedValue,
                    RULEID: drpRule.SelectedValue,
                    LISTINGID: drpListing.SelectedValue,
                    Listing_Crit: ddlListingCrit.SelectedValue
                   );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdRecords.DataSource = ds.Tables[0];
                    grdRecords.DataBind();

                    btnExportExcel.Visible = true;
                }
                else
                {
                    grdRecords.DataSource = null;
                    grdRecords.DataBind();

                    btnExportExcel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_ACTIVITY_AUDIT_TRAIL_SP(ACTION: ddlActivity.SelectedValue,
                    MODULEID: drpModule.SelectedValue,
                    DOMAIN: drpModule.SelectedValue,
                    FIELDID: ddlFields.SelectedValue,
                    VARIABLENAME: ddlFields.SelectedValue,
                    LABID: drpLab.SelectedValue,
                    RULEID: drpRule.SelectedValue,
                    LISTINGID: drpListing.SelectedValue,
                    Listing_Crit: ddlListingCrit.SelectedValue
                   );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_" + ddlActivity.SelectedValue + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel(ds, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}