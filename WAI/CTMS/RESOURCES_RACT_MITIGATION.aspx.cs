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
    public partial class RESOURCES_RACT_MITIGATION : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnPAGINATION.Value = "1";
                GETCATEGORY();
            }
        }

        protected void GETCATEGORY()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISK_MITIGATION_CATEGORY");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpCategory.DataSource = ds;
                    drpCategory.DataTextField = "Category";
                    drpCategory.DataValueField = "Category";
                    drpCategory.DataBind();
                }

                GETDATA();
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
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISK_MITIGATION_DATA", CATEGORY: drpCategory.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblrefId.Text = ds.Tables[0].Rows[0]["RefID"].ToString();
                    lblObjectie.Text = ds.Tables[0].Rows[0]["Objective"].ToString();
                    lblRACTQUE.Text = ds.Tables[0].Rows[0]["RACT_Ques_for_Discus"].ToString();
                    lblDisc.Text = ds.Tables[0].Rows[0]["Mitigation_Ques_for_Discus"].ToString();
                    lblConsiderations.Text = ds.Tables[0].Rows[0]["Considerations"].ToString();
                    lblHightRisk.Text = ds.Tables[0].Rows[0]["Ex_for_Consider_High_Risk"].ToString();
                    lblMediumRisk.Text = ds.Tables[0].Rows[0]["Ex_for_Consider_Medium_Risk"].ToString();
                    lblLowRisk.Text = ds.Tables[0].Rows[0]["Ex_for_Consider_Low_Risk"].ToString();
                    lblpotential.Text = ds.Tables[0].Rows[0]["Potential_controls_mitigation_actions"].ToString();
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

        protected void PAGINATION(string TOTAL)
        {
            try
            {
                lblpagination.Text = "Page: " + hdnPAGINATION.Value + " out of " + TOTAL;
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
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_NEXT_PREV_ID_RISK_MITIGATION", CATEGORY: drpCategory.SelectedValue, ID: hdnCurrentID.Value);

                if (ds.Tables.Count > 0)
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
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISK_MITIGATION_DATA", CATEGORY: drpCategory.SelectedValue, ID: hdnPREVID.Value);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblrefId.Text = ds.Tables[0].Rows[0]["RefID"].ToString();
                    lblObjectie.Text = ds.Tables[0].Rows[0]["Objective"].ToString();
                    lblRACTQUE.Text = ds.Tables[0].Rows[0]["RACT_Ques_for_Discus"].ToString();
                    lblDisc.Text = ds.Tables[0].Rows[0]["Mitigation_Ques_for_Discus"].ToString();
                    lblConsiderations.Text = ds.Tables[0].Rows[0]["Considerations"].ToString();
                    lblHightRisk.Text = ds.Tables[0].Rows[0]["Ex_for_Consider_High_Risk"].ToString();
                    lblMediumRisk.Text = ds.Tables[0].Rows[0]["Ex_for_Consider_Medium_Risk"].ToString();
                    lblLowRisk.Text = ds.Tables[0].Rows[0]["Ex_for_Consider_Low_Risk"].ToString();
                    lblpotential.Text = ds.Tables[0].Rows[0]["Potential_controls_mitigation_actions"].ToString();
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
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISK_MITIGATION_DATA", CATEGORY: drpCategory.SelectedValue, ID: hdnNEXTID.Value);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblrefId.Text = ds.Tables[0].Rows[0]["RefID"].ToString();
                    lblObjectie.Text = ds.Tables[0].Rows[0]["Objective"].ToString();
                    lblRACTQUE.Text = ds.Tables[0].Rows[0]["RACT_Ques_for_Discus"].ToString();
                    lblDisc.Text = ds.Tables[0].Rows[0]["Mitigation_Ques_for_Discus"].ToString();
                    lblConsiderations.Text = ds.Tables[0].Rows[0]["Considerations"].ToString();
                    lblHightRisk.Text = ds.Tables[0].Rows[0]["Ex_for_Consider_High_Risk"].ToString();
                    lblMediumRisk.Text = ds.Tables[0].Rows[0]["Ex_for_Consider_Medium_Risk"].ToString();
                    lblLowRisk.Text = ds.Tables[0].Rows[0]["Ex_for_Consider_Low_Risk"].ToString();
                    lblpotential.Text = ds.Tables[0].Rows[0]["Potential_controls_mitigation_actions"].ToString();
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
    }
}