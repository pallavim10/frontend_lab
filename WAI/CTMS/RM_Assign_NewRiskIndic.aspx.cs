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
    public partial class RM_Assign_NewRiskIndic : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GET_Category();
                GET_RISKINDI_Threshold();
                GET_RISK_Category();
                GET_RELATIVE_VALUE();
                GET_CORE_SPECIFIC();
                GET_LEVEL_SECURITY();
                GET_FREQUENCY();
                GET_Impacts_Types();
            }
        }

        protected void GET_Category()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_CATEGORY");
                ds.Tables[0].Rows.RemoveAt(0);
                string Values = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Values += "" + ds.Tables[0].Rows[i]["Category"].ToString() + "^";
                }
                hfCats.Value = Values.TrimEnd(',');
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindCats();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_SubCategory()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_SUB_CATEGORY", CATEGORY: txtCategory.Text);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.RemoveAt(0);
                    string Values = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Values += "" + ds.Tables[0].Rows[i]["Sub_Category"].ToString() + "^";
                    }
                    hfSubCats.Value = Values.TrimEnd(',');
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindSubCats();", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_RISKINDI_Threshold()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_Threshold");
                lstThreshold.Items.Clear();
                lstThreshold.DataSource = ds;
                lstThreshold.DataTextField = "Threshold_Basis";
                lstThreshold.DataValueField = "Threshold_Basis";
                lstThreshold.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_Impacts_Types()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "RISKTYPE");

                lstRiskType.Items.Clear();
                lstRiskType.DataSource = ds;
                lstRiskType.DataTextField = "TEXT";
                lstRiskType.DataValueField = "VALUE";
                lstRiskType.DataBind();

                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Impact");
                lstRiskImpact.Items.Clear();
                lstRiskImpact.DataSource = ds;
                lstRiskImpact.DataTextField = "TEXT";
                lstRiskImpact.DataValueField = "VALUE";
                lstRiskImpact.DataBind();
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
                    ds.Tables[0].Rows.RemoveAt(0);
                    drpRelativeValue.DataSource = ds;
                    drpRelativeValue.DataTextField = "Relative_Value";
                    drpRelativeValue.DataValueField = "Relative_Value";
                    drpRelativeValue.DataBind();

                    drpRelativeValue.Items.Insert(0, new ListItem("--Select--", "0"));
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
                    ds.Tables[0].Rows.RemoveAt(0);
                    drpCoreSpecific.DataSource = ds;
                    drpCoreSpecific.DataTextField = "Core_or_Specific";
                    drpCoreSpecific.DataValueField = "Core_or_Specific";
                    drpCoreSpecific.DataBind();

                    drpCoreSpecific.Items.Insert(0, new ListItem("--Select--", "0"));
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
                    ds.Tables[0].Rows.RemoveAt(0);
                    drpLevelSec.DataSource = ds;
                    drpLevelSec.DataTextField = "Level_of_Scrutiny";
                    drpLevelSec.DataValueField = "Level_of_Scrutiny";
                    drpLevelSec.DataBind();

                    drpLevelSec.Items.Insert(0, new ListItem("--Select--", "0"));
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
                    ds.Tables[0].Rows.RemoveAt(0);
                    drpFreq.DataSource = ds;
                    drpFreq.DataTextField = "Frequency";
                    drpFreq.DataValueField = "Frequency";
                    drpFreq.DataBind();

                    drpFreq.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string Type = "";
                foreach (ListItem item in lstRiskType.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Type.ToString() == "")
                        {
                            Type = item.Value;
                        }
                        else
                        {
                            Type += "," + item.Value;
                        }
                    }
                }

                string Impacts = "";
                foreach (ListItem item in lstRiskImpact.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Impacts.ToString() == "")
                        {
                            Impacts = item.Value;
                        }
                        else
                        {
                            Impacts += "," + item.Value;
                        }
                    }
                }

                string Threshold = "";
                foreach (ListItem item in lstThreshold.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Threshold.ToString() == "")
                        {
                            Threshold = item.Value;
                        }
                        else
                        {
                            Threshold += "," + item.Value;
                        }
                    }
                }

                DataSet ds = dal.RESOURCES_DATA(
                Action: "INSERT_RISK_INDIC",
                CATEGORY: txtCategory.Text,
                SUBCATE: txtSubCategory.Text,
                Risk_Indicator: txtRiskIndi.Text,
                Discussion_Details: txtDetails.Text,
                EXPERIANCE: drpExp.Text,
                RELATIVE_VALUE: drpRelativeValue.SelectedValue,
                CORE: drpCoreSpecific.SelectedValue,
                LEVEL_SECU: drpLevelSec.SelectedValue,
                FREQUENCY: drpFreq.SelectedValue,
                Threshold_Basis: Threshold,
                Scorecard: ddlScorecard.SelectedValue,
                Weighting: ddlWeighting.SelectedValue,
                Mitigation_Actions: ddlmitigation.SelectedValue,
                RACT_Traceability: txtract.Text,
                PRIMARYPI: txtprimarypi.Text,
                SECONDARYPI: txtsecpi.Text,
                Risk_Cat: drpRiskCategory.SelectedValue,
                Risk_SubCat: drpRiskSubCategory.SelectedValue,
                Risk_Fact: drpRiskFactor.SelectedValue,
                Risk_Impacts: Impacts,
                Risk_Type: Type,
                Risk_Description: txtRiskDescription.Text,
                Risk_Description_C: txtRiskDescriptionC.Text,
                Risk_Description_Inv: txtRiskDescriptionInv.Text
                    );


                dal.Risk_Indicator_SP(Action: "Assign_Risk_Indic", ID: Request.QueryString["TILEID"].ToString(), Result: ds.Tables[0].Rows[0][0].ToString());

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('New Risk Indicator Created and Assigned Successfully'); OpenRiskTrigger(" + Request.QueryString["TILEID"].ToString() + ") ", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpRiskCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_RISK_Subcategory();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpRiskSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BGET_RISK_Factors();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_RISK_Category()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Category", ProjectId: Session["PROJECTID"].ToString());
                drpRiskCategory.DataSource = dt;
                drpRiskCategory.DataTextField = "Description";
                drpRiskCategory.DataValueField = "id";
                drpRiskCategory.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_RISK_Subcategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "SubCategory", Categoryvalue: drpRiskCategory.SelectedValue, ProjectId: Session["PROJECTID"].ToString());
                drpRiskSubCategory.DataSource = dt;
                drpRiskSubCategory.DataTextField = "Description";
                drpRiskSubCategory.DataValueField = "id";
                drpRiskSubCategory.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BGET_RISK_Factors()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Factor", SubCategoryvalue: drpRiskSubCategory.SelectedValue, ProjectId: Session["PROJECTID"].ToString());
                drpRiskFactor.DataSource = dt;
                drpRiskFactor.DataTextField = "Description";
                drpRiskFactor.DataValueField = "id";
                drpRiskFactor.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void txtCategory_TextChanged(object sender, EventArgs e)
        {
            try
            {
               GET_SubCategory();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}