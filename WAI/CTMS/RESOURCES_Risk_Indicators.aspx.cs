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
    public partial class RESOURCES_Risk_Indicators : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnPAGINATION.Value = "1";
                GET_RISK_Category();
                GETCATEGORY();
                GET_SUB_CATEGORY();
                GET_Tab_SUB_CATEGORY();
                GET_RELATIVE_VALUE();
                GET_CORE_SPECIFIC();
                GET_LEVEL_SECURITY();
                GET_FREQUENCY();
                GET_Impacts_Types();
                GETDATA();
                GET_TabDATA();
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

        protected void GETCATEGORY()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_CATEGORY");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpCategory.DataSource = ds;
                    drpCategory.DataTextField = "Category";
                    drpCategory.DataValueField = "Category";
                    drpCategory.DataBind();

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

        protected void GET_SUB_CATEGORY()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_SUB_CATEGORY", CATEGORY: drpCategory.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubCategory.DataSource = ds;
                    drpSubCategory.DataTextField = "Sub_Category";
                    drpSubCategory.DataValueField = "Sub_Category";
                    drpSubCategory.DataBind();
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

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnPAGINATION.Value = "1";
                GET_SUB_CATEGORY();
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnPAGINATION.Value = "1";
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpExp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnPAGINATION.Value = "1";
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpRelativeValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnPAGINATION.Value = "1";
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpCoreSpecific_TextChanged(object sender, EventArgs e)
        {
            try
            {
                hdnPAGINATION.Value = "1";
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLevelSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnPAGINATION.Value = "1";
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnPAGINATION.Value = "1";
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISK_INDICATOR_DATA",
                CATEGORY: drpCategory.SelectedValue,
                SUBCATE: drpSubCategory.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblRiskIndi.Text = ds.Tables[0].Rows[0]["Risk_Indicator"].ToString();
                    lblExp.Text = ds.Tables[0].Rows[0]["Experience"].ToString();
                    lblRelativeValue.Text = ds.Tables[0].Rows[0]["Relative_Value"].ToString();
                    lblCoreSpecific.Text = ds.Tables[0].Rows[0]["Core_or_Specific"].ToString();
                    lblLevelSec.Text = ds.Tables[0].Rows[0]["Level_of_Scrutiny"].ToString();
                    lblFreq.Text = ds.Tables[0].Rows[0]["Frequency"].ToString();
                    lblDetails.Text = ds.Tables[0].Rows[0]["Discussion_Details"].ToString();
                    lblThreshold.Text = ds.Tables[0].Rows[0]["Threshold_Basis"].ToString();
                    lblScorecard.Text = ds.Tables[0].Rows[0]["Scorecard"].ToString();
                    lblWeighting.Text = ds.Tables[0].Rows[0]["Weighting"].ToString();
                    lblmitigation.Text = ds.Tables[0].Rows[0]["Mitigation_Actions"].ToString();
                    txtract.Text = ds.Tables[0].Rows[0]["RACT_Traceability"].ToString();
                    txtprimarypi.Text = ds.Tables[0].Rows[0]["Primary_PI"].ToString();
                    txtsecpi.Text = ds.Tables[0].Rows[0]["Secondary_PI"].ToString();



                    drpRiskCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_Cat"].ToString();
                    GET_RISK_Subcategory();
                    drpRiskSubCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_SubCat"].ToString();
                    BGET_RISK_Factors();
                    drpRiskFactor.SelectedValue = ds.Tables[0].Rows[0]["Risk_Fact"].ToString();

                    txtRiskDescription.Text = ds.Tables[0].Rows[0]["Risk_Description"].ToString();
                    txtRiskDescriptionC.Text = ds.Tables[0].Rows[0]["Risk_Description_C"].ToString();
                    txtRiskDescriptionInv.Text = ds.Tables[0].Rows[0]["Risk_Description_Inv"].ToString();

                    string[] Impacts = ds.Tables[0].Rows[0]["Risk_Impacts"].ToString().Split(',').ToArray();

                    if (ds.Tables[0].Rows[0]["Risk_Impacts"].ToString() != "")
                    {
                        for (int i = 0; i < Impacts.Length; i++)
                        {
                            string temp = Impacts[i].ToString();
                            if (temp != "")
                            {
                                ListItem itm = lstRiskImpact.Items.FindByValue(temp);
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        lstRiskImpact.ClearSelection();
                    }


                    string[] Type = ds.Tables[0].Rows[0]["Risk_Type"].ToString().Split(',').ToArray();

                    if (ds.Tables[0].Rows[0]["Risk_Type"].ToString() != "")
                    {
                        for (int i = 0; i < Type.Length; i++)
                        {
                            string temp = Type[i].ToString();
                            if (temp != "")
                            {
                                ListItem itm = lstRiskType.Items.FindByValue(temp);
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        lstRiskType.ClearSelection();
                    }

                    btnSubmit.Visible = true;
                }
                else
                {
                    hdnCurrentID.Value = "0";
                    btnSubmit.Visible = false;
                    hdnPAGINATION.Value = "0";
                    CLEAR();
                }

                hdnTotal.Value = ds.Tables[0].Rows.Count.ToString();
                GET_NEXT_PREV_ID();
                PAGINATION(hdnTotal.Value);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CLEAR()
        {
            lblRiskIndi.Text = "";
            lblExp.Text = "";
            lblRelativeValue.Text = "";
            lblCoreSpecific.Text = "";
            lblLevelSec.Text = "";
            lblFreq.Text = "";
            lblDetails.Text = "";
            lblThreshold.Text = "";
            lblScorecard.Text = "";
            lblWeighting.Text = "";
            lblmitigation.Text = "";
            txtract.Text = "";
            txtprimarypi.Text = "";
            txtsecpi.Text = "";
        }

        protected void PAGINATION(string TOTAL)
        {
            try
            {
                lblpagination.Text = "Page : " + hdnPAGINATION.Value + " out of " + TOTAL;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_NEXT_PREV_ID()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_NEXT_PREV_ID_RISK_INDI",
                CATEGORY: drpCategory.SelectedValue,
                SUBCATE: drpSubCategory.SelectedValue,
                ID: hdnCurrentID.Value
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["PreviousValue"].ToString() != "")
                    {
                        btnPrevious.Visible = true;
                        hdnPREVID.Value = ds.Tables[0].Rows[0]["PreviousValue"].ToString();
                    }
                    else
                    {
                        btnPrevious.Visible = false;
                    }
                    if (ds.Tables[0].Rows[0]["NextValue"].ToString() != "")
                    {
                        btnNext.Visible = true;
                        hdnNEXTID.Value = ds.Tables[0].Rows[0]["NextValue"].ToString();
                    }
                    else
                    {
                        btnNext.Visible = false;
                    }
                }
                else
                {
                    btnPrevious.Visible = false;
                    btnNext.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISK_INDICATOR_DATA",
                CATEGORY: drpCategory.SelectedValue,
                SUBCATE: drpSubCategory.SelectedValue,
                ID: hdnPREVID.Value
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblRiskIndi.Text = ds.Tables[0].Rows[0]["Risk_Indicator"].ToString();
                    lblExp.Text = ds.Tables[0].Rows[0]["Experience"].ToString();
                    lblRelativeValue.Text = ds.Tables[0].Rows[0]["Relative_Value"].ToString();
                    lblCoreSpecific.Text = ds.Tables[0].Rows[0]["Core_or_Specific"].ToString();
                    lblLevelSec.Text = ds.Tables[0].Rows[0]["Level_of_Scrutiny"].ToString();
                    lblFreq.Text = ds.Tables[0].Rows[0]["Frequency"].ToString();
                    lblDetails.Text = ds.Tables[0].Rows[0]["Discussion_Details"].ToString();
                    lblThreshold.Text = ds.Tables[0].Rows[0]["Threshold_Basis"].ToString();
                    lblScorecard.Text = ds.Tables[0].Rows[0]["Scorecard"].ToString();
                    lblWeighting.Text = ds.Tables[0].Rows[0]["Weighting"].ToString();
                    lblmitigation.Text = ds.Tables[0].Rows[0]["Mitigation_Actions"].ToString();
                    txtract.Text = ds.Tables[0].Rows[0]["RACT_Traceability"].ToString();
                    txtprimarypi.Text = ds.Tables[0].Rows[0]["Primary_PI"].ToString();
                    txtsecpi.Text = ds.Tables[0].Rows[0]["Secondary_PI"].ToString();

                    drpRiskCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_Cat"].ToString();
                    GET_RISK_Subcategory();
                    drpRiskSubCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_SubCat"].ToString();
                    BGET_RISK_Factors();
                    drpRiskFactor.SelectedValue = ds.Tables[0].Rows[0]["Risk_Fact"].ToString();

                    txtRiskDescription.Text = ds.Tables[0].Rows[0]["Risk_Description"].ToString();
                    txtRiskDescriptionC.Text = ds.Tables[0].Rows[0]["Risk_Description_C"].ToString();
                    txtRiskDescriptionInv.Text = ds.Tables[0].Rows[0]["Risk_Description_Inv"].ToString();

                    string[] Impacts = ds.Tables[0].Rows[0]["Risk_Impacts"].ToString().Split(',').ToArray();

                    if (ds.Tables[0].Rows[0]["Risk_Impacts"].ToString() != "")
                    {
                        for (int i = 0; i < Impacts.Length; i++)
                        {
                            string temp = Impacts[i].ToString();
                            if (temp != "")
                            {
                                ListItem itm = lstRiskImpact.Items.FindByValue(temp);
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        lstRiskImpact.ClearSelection();
                    }


                    string[] Type = ds.Tables[0].Rows[0]["Risk_Type"].ToString().Split(',').ToArray();

                    if (ds.Tables[0].Rows[0]["Risk_Type"].ToString() != "")
                    {
                        for (int i = 0; i < Type.Length; i++)
                        {
                            string temp = Type[i].ToString();
                            if (temp != "")
                            {
                                ListItem itm = lstRiskType.Items.FindByValue(temp);
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        lstRiskType.ClearSelection();
                    }

                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                    CLEAR();
                }

                hdnPAGINATION.Value = (Convert.ToInt32(hdnPAGINATION.Value) - 1).ToString();
                PAGINATION(hdnTotal.Value);
                GET_NEXT_PREV_ID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISK_INDICATOR_DATA",
                CATEGORY: drpCategory.SelectedValue,
                SUBCATE: drpSubCategory.SelectedValue,
                ID: hdnNEXTID.Value
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblRiskIndi.Text = ds.Tables[0].Rows[0]["Risk_Indicator"].ToString();
                    lblExp.Text = ds.Tables[0].Rows[0]["Experience"].ToString();
                    lblRelativeValue.Text = ds.Tables[0].Rows[0]["Relative_Value"].ToString();
                    lblCoreSpecific.Text = ds.Tables[0].Rows[0]["Core_or_Specific"].ToString();
                    lblLevelSec.Text = ds.Tables[0].Rows[0]["Level_of_Scrutiny"].ToString();
                    lblFreq.Text = ds.Tables[0].Rows[0]["Frequency"].ToString();
                    lblDetails.Text = ds.Tables[0].Rows[0]["Discussion_Details"].ToString();
                    lblThreshold.Text = ds.Tables[0].Rows[0]["Threshold_Basis"].ToString();
                    lblScorecard.Text = ds.Tables[0].Rows[0]["Scorecard"].ToString();
                    lblWeighting.Text = ds.Tables[0].Rows[0]["Weighting"].ToString();
                    lblmitigation.Text = ds.Tables[0].Rows[0]["Mitigation_Actions"].ToString();
                    txtract.Text = ds.Tables[0].Rows[0]["RACT_Traceability"].ToString();
                    txtprimarypi.Text = ds.Tables[0].Rows[0]["Primary_PI"].ToString();
                    txtsecpi.Text = ds.Tables[0].Rows[0]["Secondary_PI"].ToString();

                    drpRiskCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_Cat"].ToString();
                    GET_RISK_Subcategory();
                    drpRiskSubCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_SubCat"].ToString();
                    BGET_RISK_Factors();
                    drpRiskFactor.SelectedValue = ds.Tables[0].Rows[0]["Risk_Fact"].ToString();

                    txtRiskDescription.Text = ds.Tables[0].Rows[0]["Risk_Description"].ToString();
                    txtRiskDescriptionC.Text = ds.Tables[0].Rows[0]["Risk_Description_C"].ToString();
                    txtRiskDescriptionInv.Text = ds.Tables[0].Rows[0]["Risk_Description_Inv"].ToString();

                    string[] Impacts = ds.Tables[0].Rows[0]["Risk_Impacts"].ToString().Split(',').ToArray();

                    if (ds.Tables[0].Rows[0]["Risk_Impacts"].ToString() != "")
                    {
                        for (int i = 0; i < Impacts.Length; i++)
                        {
                            string temp = Impacts[i].ToString();
                            if (temp != "")
                            {
                                ListItem itm = lstRiskImpact.Items.FindByValue(temp);
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        lstRiskImpact.ClearSelection();
                    }


                    string[] Type = ds.Tables[0].Rows[0]["Risk_Type"].ToString().Split(',').ToArray();

                    if (ds.Tables[0].Rows[0]["Risk_Type"].ToString() != "")
                    {
                        for (int i = 0; i < Type.Length; i++)
                        {
                            string temp = Type[i].ToString();
                            if (temp != "")
                            {
                                ListItem itm = lstRiskType.Items.FindByValue(temp);
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        lstRiskType.ClearSelection();
                    }

                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                    CLEAR();
                }

                hdnPAGINATION.Value = (Convert.ToInt32(hdnPAGINATION.Value) + 1).ToString();
                PAGINATION(hdnTotal.Value);
                GET_NEXT_PREV_ID();
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

                DataSet ds = dal.RESOURCES_DATA(Action: "UPDATE_RISK_INDI",
                ID: hdnCurrentID.Value,
                PRIMARYPI: txtprimarypi.Text,
                SECONDARYPI: txtsecpi.Text,
                VARIABLENAME: txtract.Text,
                CATEGORY: drpRiskCategory.SelectedValue,
                SUBCATE: drpRiskSubCategory.SelectedValue,
                FIELDNAME: drpRiskFactor.SelectedValue,
                FREQUENCY: txtRiskDescription.Text,
                CDESH_MODULE: txtRiskDescriptionC.Text,
                RELATIVE_VALUE: txtRiskDescriptionInv.Text,
                EXPERIANCE: Impacts,
                LEVEL_SECU: Type
                );

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Updated successfully');", true);
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

        //Tabular View

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

        protected void lbtnli2_Click(object sender, EventArgs e)
        {
            tab1.Visible = false;
            tab2.Visible = true;
        }

        protected void lbtnli1_Click(object sender, EventArgs e)
        {
            tab1.Visible = true;
            tab2.Visible = false;
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
    }
}