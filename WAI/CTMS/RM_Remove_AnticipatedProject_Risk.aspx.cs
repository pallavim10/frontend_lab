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
    public partial class RM_Remove_AnticipatedProject_Risk : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["PROJECTID"] != null)
                {
                    Drp_Project_Name.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTID"].ToString()));
                }
                else
                {
                    fill_drpdwn();
                }
            }
        }

        private void fill_drpdwn()
        {
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
                DataSet ds = dal.getRiskRemoveList(Session["PROJECTID"].ToString(), Action: "Cat");
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
                                string ID = ((Label)GridView1.Rows[i].FindControl("lbl_RISK_ID")).Text;
                                string msg = dal.DeleteRiskBucket(Id: ID);

                            }
                        }
                    }
                }
                Response.Write("<script> alert('Record Updated successfully.');window.location='RM_Remove_AnticipatedProject_Risk.aspx'; </script>");
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
                    DataSet ds = dal.getRiskRemoveList(Action: "SubCat", CatID: ID, ProjectId: Session["PROJECTID"].ToString());
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
                    DataSet ds = dal.getRiskRemoveList(Action: "Factors", CatID: ParentID, SubCatID: ID, ProjectId: Session["PROJECTID"].ToString());
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
                DataSet ds = dal.getRiskRemoveList(Action: "ExportExcel", ProjectId: Session["PROJECTID"].ToString());
                ds.Tables[0].TableName = "Anticipated Risks";
                Multiple_Export_Excel.ToExcel(ds, "Anticipated_Risks.xls", Page.Response);
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
                DataSet ds = dal.getRiskRemoveList(Action: "ExportExcel", ProjectId: Session["PROJECTID"].ToString());
                ds.Tables[0].TableName = "Anticipated Risks";
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], "Anticipated_Risks", Page.Response);
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
                DataSet ds = dal.getRiskRemoveList(Action: "ExportExcel", ProjectId: Session["PROJECTID"].ToString());
                ds.Tables[0].TableName = "Anticipated Risks";
                Multiple_Export_Excel.ExportToWord(ds.Tables[0], "Anticipated_Risks", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}