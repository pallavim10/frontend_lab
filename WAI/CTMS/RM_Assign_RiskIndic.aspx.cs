using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class RM_Assign_RiskIndic : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GETCATEGORY();
                GET_Tab_SUB_CATEGORY();
                GET_RELATIVE_VALUE();
                GET_CORE_SPECIFIC();
                GET_LEVEL_SECURITY();
                GET_FREQUENCY();
                GET_TabDATA();
            }
        }

        protected void GETCATEGORY()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_CATEGORY");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpTabCategory.DataSource = ds;
                    drpTabCategory.DataTextField = "Category";
                    drpTabCategory.DataValueField = "Category";
                    drpTabCategory.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void GET_RELATIVE_VALUE()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_Relative_Value");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpTabRelativeValue.DataSource = ds;
                    drpTabRelativeValue.DataTextField = "Relative_Value";
                    drpTabRelativeValue.DataValueField = "Relative_Value";
                    drpTabRelativeValue.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void GET_CORE_SPECIFIC()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_Core_Specific");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpTabCoreSpecific.DataSource = ds;
                    drpTabCoreSpecific.DataTextField = "Core_or_Specific";
                    drpTabCoreSpecific.DataValueField = "Core_or_Specific";
                    drpTabCoreSpecific.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void GET_LEVEL_SECURITY()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_Level_Scrutiny");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpTabLevelSec.DataSource = ds;
                    drpTabLevelSec.DataTextField = "Level_of_Scrutiny";
                    drpTabLevelSec.DataValueField = "Level_of_Scrutiny";
                    drpTabLevelSec.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void GET_FREQUENCY()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_Frequency");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpTabFreq.DataSource = ds;
                    drpTabFreq.DataTextField = "Frequency";
                    drpTabFreq.DataValueField = "Frequency";
                    drpTabFreq.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void GET_Tab_SUB_CATEGORY()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_SUB_CATEGORY", CATEGORY: drpTabCategory.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpTabSubCategory.DataSource = ds;
                    drpTabSubCategory.DataTextField = "Sub_Category";
                    drpTabSubCategory.DataValueField = "Sub_Category";
                    drpTabSubCategory.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void GET_TabDATA()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISK_INDICATOR_DATA",
                CATEGORY: drpTabCategory.SelectedValue,
                SUBCATE: drpTabSubCategory.SelectedValue,
                EXPERIANCE: "ALL",
                RELATIVE_VALUE: drpTabRelativeValue.SelectedValue,
                CORE: drpTabCoreSpecific.SelectedValue,
                LEVEL_SECU: drpTabLevelSec.SelectedValue,
                FREQUENCY: drpTabFreq.SelectedValue
                );

                grdRiskIndicators.DataSource = ds;
                grdRiskIndicators.DataBind();

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

        protected void drpTabCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_Tab_SUB_CATEGORY();
                GET_TabDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpTabSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_TabDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpTabRelativeValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_TabDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpTabFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_TabDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpTabLevelSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_TabDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpTabCoreSpecific_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_TabDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdRiskIndicators_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Assign")
                {
                    dal.Risk_Indicator_SP(Action: "Assign_Risk_Indic", ID: Request.QueryString["TILEID"].ToString(), Result: e.CommandArgument.ToString());

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Risk Indicator Assigned Successfully'); OpenRiskTrigger(" + Request.QueryString["TILEID"].ToString() + ") ", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}