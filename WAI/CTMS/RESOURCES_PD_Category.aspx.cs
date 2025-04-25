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
    public partial class RESOURCES_PD_Category : System.Web.UI.Page
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
                GET_Classification();
                GET_Impact();
                GETDATA();
                GET_TabDATA();
            }
        }

        protected void GETCATEGORY()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_PD_CAT_Category");

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

        protected void GET_Classification()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_PD_CAT_Classification");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpClassification.DataSource = ds;
                    drpClassification.DataTextField = "Classification";
                    drpClassification.DataValueField = "Classification";
                    drpClassification.DataBind();

                    drpTabClassfication.DataSource = ds;
                    drpTabClassfication.DataTextField = "Classification";
                    drpTabClassfication.DataValueField = "Classification";
                    drpTabClassfication.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void GET_Impact()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_PD_CAT_Impact");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpImpact.DataSource = ds;
                    drpImpact.DataTextField = "Impact";
                    drpImpact.DataValueField = "Impact";
                    drpImpact.DataBind();

                    drpTabImpact.DataSource = ds;
                    drpTabImpact.DataTextField = "Impact";
                    drpTabImpact.DataValueField = "Impact";
                    drpTabImpact.DataBind();
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
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_PD_CAT_SubCategory", CATEGORY: drpCategory.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubCategory.DataSource = ds;
                    drpSubCategory.DataTextField = "Subcategory";
                    drpSubCategory.DataValueField = "Subcategory";
                    drpSubCategory.DataBind();
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
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_PD_CAT_SubCategory", CATEGORY: drpTabCategory.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpTabSubCategory.DataSource = ds;
                    drpTabSubCategory.DataTextField = "Subcategory";
                    drpTabSubCategory.DataValueField = "Subcategory";
                    drpTabSubCategory.DataBind();
                }
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
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_PD_CAT",
                CATEGORY: drpCategory.SelectedValue,
                SUBCATE: drpSubCategory.SelectedValue,
                CORE: drpClassification.SelectedValue,
                FREQUENCY: drpImpact.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblFactor.Text = ds.Tables[0].Rows[0]["Factor"].ToString();
                    lblrationalExample.Text = ds.Tables[0].Rows[0]["Rationale_Case_Examples"].ToString();
                    lblDataAnalysis.Text = ds.Tables[0].Rows[0]["Data_to_be_Use"].ToString();

                    drpRiskCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_Cat"].ToString();
                    GET_RISK_Subcategory();
                    drpRiskSubCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_SubCat"].ToString();
                    BGET_RISK_Factors();
                    drpRiskFactor.SelectedValue = ds.Tables[0].Rows[0]["Risk_Fact"].ToString();
                }
                else
                {
                    hdnCurrentID.Value = "0";
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
            lblFactor.Text = "";
            lblrationalExample.Text = "";
            lblDataAnalysis.Text = "";
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
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_NEXT_PREV_ID_PD_CAT",
                CATEGORY: drpCategory.SelectedValue,
                SUBCATE: drpSubCategory.SelectedValue,
                ID: hdnCurrentID.Value,
                CORE: drpClassification.SelectedValue,
                FREQUENCY: drpImpact.SelectedValue
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

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_PD_CAT",
                CATEGORY: drpCategory.SelectedValue,
                SUBCATE: drpSubCategory.SelectedValue,
                ID: hdnPREVID.Value,
                CORE: drpClassification.SelectedValue,
                FREQUENCY: drpImpact.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblFactor.Text = ds.Tables[0].Rows[0]["Factor"].ToString();
                    lblrationalExample.Text = ds.Tables[0].Rows[0]["Rationale_Case_Examples"].ToString();
                    lblDataAnalysis.Text = ds.Tables[0].Rows[0]["Data_to_be_Use"].ToString();
                    
                    drpRiskCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_Cat"].ToString();
                    GET_RISK_Subcategory();
                    drpRiskSubCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_SubCat"].ToString();
                    BGET_RISK_Factors();
                    drpRiskFactor.SelectedValue = ds.Tables[0].Rows[0]["Risk_Fact"].ToString();
                }
                else
                {
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
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_PD_CAT",
                CATEGORY: drpCategory.SelectedValue,
                SUBCATE: drpSubCategory.SelectedValue,
                ID: hdnNEXTID.Value,
                CORE: drpClassification.SelectedValue,
                FREQUENCY: drpImpact.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblFactor.Text = ds.Tables[0].Rows[0]["Factor"].ToString();
                    lblrationalExample.Text = ds.Tables[0].Rows[0]["Rationale_Case_Examples"].ToString();
                    lblDataAnalysis.Text = ds.Tables[0].Rows[0]["Data_to_be_Use"].ToString();

                    drpRiskCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_Cat"].ToString();
                    GET_RISK_Subcategory();
                    drpRiskSubCategory.SelectedValue = ds.Tables[0].Rows[0]["Risk_SubCat"].ToString();
                    BGET_RISK_Factors();
                    drpRiskFactor.SelectedValue = ds.Tables[0].Rows[0]["Risk_Fact"].ToString();
                }
                else
                {
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

        protected void GET_TabDATA()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_PD_CAT",
               CATEGORY: drpTabCategory.SelectedValue,
               SUBCATE: drpTabSubCategory.SelectedValue,
               CORE: drpTabClassfication.SelectedValue,
               FREQUENCY: drpTabImpact.SelectedValue
               );

                grdPDCAT.DataSource = ds;
                grdPDCAT.DataBind();

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

        protected void drpClassification_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void drpImpact_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void drpTabClassfication_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void drpTabImpact_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "UPDATE_PD_CAT",
                ID: hdnCurrentID.Value,
                CATEGORY: drpRiskCategory.SelectedValue,
                SUBCATE: drpRiskSubCategory.SelectedValue,
                FIELDNAME: drpRiskFactor.SelectedValue
                );

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Updated successfully'); window.location='RESOURCES_PD_Category.aspx';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}