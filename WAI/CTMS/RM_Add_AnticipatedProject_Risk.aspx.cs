using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class RM_Add_AnticipatedProject_Risk : System.Web.UI.Page
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

                if (Session["PROJECTID"] != null)
                {
                    Drp_Project_Name.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTID"].ToString()));
                }
                else
                {
                    fill_drpdwn();
                }
                BindCategory();
            }
        }

        private void fill_drpdwn()
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.GetSetPROJECTDETAILS(
            Action: "Get_Specific_Project",
            Project_ID: Convert.ToInt32(Session["PROJECTID"]),
            ENTEREDBY: Session["User_ID"].ToString()
            );
            Drp_Project_Name.DataSource = ds;
            Drp_Project_Name.DataValueField = "PROJNAME";
            Drp_Project_Name.DataBind();

            Drp_Project_Name.Items.Insert(0, new ListItem("--Select Project--", "0"));

        }

        protected void Btn_Get_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.getRiskList(Action: "Bank_Cat");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gvCategory.DataSource = ds.Tables[0];
                    gvCategory.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Btn_Add_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                for (int a = 0; a < gvCategory.Rows.Count; a++)
                {
                    GridView gvSubCategory = gvCategory.Rows[a].FindControl("gvSubCategory") as GridView;

                    for (int b = 0; b < gvSubCategory.Rows.Count; b++)
                    {
                        GridView GridView1 = gvSubCategory.Rows[b].FindControl("GridView1") as GridView;

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            CheckBox ChAction;
                            ChAction = (CheckBox)GridView1.Rows[i].FindControl("Chk_Sel_Fun");

                            if (ChAction.Checked)
                            {
                                string Risk_ID = ((Label)GridView1.Rows[i].FindControl("lbl_RISK_ID")).Text;
                                string RiskActualID = ((LinkButton)GridView1.Rows[i].FindControl("lnk_RISK_ID")).Text;
                                string CatId = ((Label)GridView1.Rows[i].FindControl("lblCatId")).Text;
                                string SubCatId = ((Label)GridView1.Rows[i].FindControl("lblSubCatId")).Text;
                                string FactId = ((Label)GridView1.Rows[i].FindControl("lblFactorId")).Text;

                                string msg = dal.insertRiskBucket(
                                ProjectId: Drp_Project_Name.SelectedValue,
                                RiskId: Risk_ID,
                                RCat: CatId,
                                RSubCat: SubCatId,
                                RFactor: FactId,
                                RManager: Session["User_ID"].ToString(),
                                RiskActualID: RiskActualID
                                    );
                            }
                        }
                    }
                }
                Response.Write("<script> alert('Record Updated successfully.');window.location='RM_Add_AnticipatedProject_Risk.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSubcategory();
                DataSet ds = dal.getRiskList(Action: "Category", CategoryId: ddlCategory.SelectedValue);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gvCategory.DataSource = ds.Tables[0];
                    gvCategory.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    gvCategory.DataSource = dt;
                    gvCategory.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.getRiskList(Action: "SubCategory", CategoryId: ddlCategory.SelectedValue, SubCategoryId: ddlSubCategory.SelectedValue);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gvCategory.DataSource = ds.Tables[0];
                    gvCategory.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        public void BindCategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Category");
                if (dt.Rows.Count > 1)
                {
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataTextField = "Description";
                    ddlCategory.DataValueField = "id";
                    ddlCategory.DataBind();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BindSubcategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "SubCategory", Categoryvalue: ddlCategory.SelectedValue);
                if (dt.Rows.Count > 1)
                {
                    ddlSubCategory.DataSource = dt;
                    ddlSubCategory.DataTextField = "Description";
                    ddlSubCategory.DataValueField = "id";
                    ddlSubCategory.DataBind();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    CheckBox Chk_Sel_Fun = (CheckBox)e.Row.FindControl("Chk_Sel_Fun");
                    CheckBox Chk_Del = (CheckBox)e.Row.FindControl("Chk_Del");

                    if (dr["Count"].ToString() != "0")
                    {
                        Chk_Sel_Fun.Visible = false;
                        Chk_Del.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void Btn_Del_Click(object sender, EventArgs e)
        {
            try
            {
                for (int a = 0; a < gvCategory.Rows.Count; a++)
                {
                    GridView gvSubCategory = gvCategory.Rows[a].FindControl("gvSubCategory") as GridView;

                    for (int b = 0; b < gvSubCategory.Rows.Count; b++)
                    {
                        GridView GridView1 = gvSubCategory.Rows[b].FindControl("GridView1") as GridView;

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            CheckBox Chk_Del;
                            Chk_Del = (CheckBox)GridView1.Rows[i].FindControl("Chk_Del");

                            if (Chk_Del.Checked)
                            {
                                string Risk_ID = ((Label)GridView1.Rows[i].FindControl("lbl_RISK_ID")).Text;

                                string msg = dal.deleteRiskBank(
                                Id: Risk_ID
                                    );
                            }
                        }
                    }
                }
                Response.Write("<script> alert('Record Deleted successfully.');window.location='RM_Add_AnticipatedProject_Risk.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();

                    GridView gvSubCategory = e.Row.FindControl("gvSubCategory") as GridView;
                    DataSet ds = dal.getRiskList(Action: "Bank_SubCat", CategoryId: ID);
                    gvSubCategory.DataSource = ds.Tables[0];
                    gvSubCategory.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvSubCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();
                    string ParentID = drv["ParentID"].ToString();

                    GridView GridView1 = e.Row.FindControl("GridView1") as GridView;
                    DataSet ds = dal.getRiskList(Action: "SubCategory", CategoryId: ParentID, SubCategoryId: ID);
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal.getRiskList(Action: "ExportExcel");
                ds.Tables[0].TableName = "Risk Bank";
                Multiple_Export_Excel.ToExcel(ds, "Risk_Bank.xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToPDF();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnWord_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToWord();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportToPDF()
        {
            try
            {
                DataSet ds = dal.getRiskList(Action: "ExportExcel");
                ds.Tables[0].TableName = "Risk Bank";
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], "Risk_Bank", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportToWord()
        {
            try
            {
                DataSet ds = dal.getRiskList(Action: "ExportExcel");
                ds.Tables[0].TableName = "Risk Bank";
                Multiple_Export_Excel.ExportToWord(ds.Tables[0], "Risk_Bank", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}