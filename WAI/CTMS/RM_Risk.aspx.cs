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
    public partial class RM_Risk : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }

                BindCategory();
                BindSubcategory();
                BindFactors();
            }
        }

        public void BindCategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Category", ProjectId: Session["PROJECTID"].ToString());
                if (dt.Rows.Count > 1)
                {
                    ddlcategory.DataSource = dt;
                    ddlcategory.DataTextField = "Description";
                    ddlcategory.DataValueField = "id";
                    ddlcategory.DataBind();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindSubcategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue, ProjectId: Session["PROJECTID"].ToString());
                if (dt.Rows.Count > 1)
                {
                    ddlsubcategory.DataSource = dt;
                    ddlsubcategory.DataTextField = "Description";
                    ddlsubcategory.DataValueField = "id";
                    ddlsubcategory.DataBind();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindFactors()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Factor", SubCategoryvalue: ddlsubcategory.SelectedValue, ProjectId: Session["PROJECTID"].ToString());
                if (dt.Rows.Count > 1)
                {
                    ddlfactor.DataSource = dt;
                    ddlfactor.DataTextField = "Description";
                    ddlfactor.DataValueField = "id";
                    ddlfactor.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string keyvalue = "", NATUREVALUE = "";
                if (radno.Checked == true)
                {
                    keyvalue = "No";
                }
                else
                {
                    keyvalue = "Yes";
                }

                if (radio1.Checked == true)
                {
                    NATUREVALUE = "Static";
                }
                else
                {
                    NATUREVALUE = "Dynamic";
                }
                //string RiskActualId = ddlcategory.SelectedValue + ddlsubcategory.SelectedValue + ddlfactor.SelectedValue;
                string msg = dal.insertriskdata(Action: "Insert", CategoryValue: ddlcategory.SelectedValue,
                SubCategoryvalue: ddlsubcategory.SelectedValue,
                Factorsvalue: ddlfactor.SelectedValue,
                Risk_Consideration: txtRiskCons.Text,
                Suggested_Mitigation: txtSugMitig.Text,
                Suggested_Riskcategory: txtSugRiskCat.Text,
                Risk_Nature: NATUREVALUE,
                Transcelerate_category: txtTransCat.Text,
                Identifiedby: Session["User_ID"].ToString(),
                keyvalue: keyvalue);
                if (msg != null)
                {
                    ddlcategory.SelectedIndex = 0;
                    ddlsubcategory.SelectedIndex = 0;
                    ddlfactor.SelectedIndex = 0;
                    txtRiskCons.Text = "";
                    txtSugMitig.Text = "";
                    txtSugRiskCat.Text = "";
                    radio1.Checked = false;
                    radio2.Checked = false;
                    txtTransCat.Text = "";
                    Response.Write("<script> alert('Record Inserted successfully.')</script>");
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSubcategory();
                BindFactors();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                BindFactors();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}