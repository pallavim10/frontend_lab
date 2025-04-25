using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class CTMS_Group_Logs : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfv_lblGrpID.Value = Request.QueryString["GroupID"].ToString();
                    bind_GroupName();
                    bind_Tasks();
                    bind_Matrix();
                    bind_Compare();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_GroupName()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "edit_Group", ID: hfv_lblGrpID.Value);
                lblGrpName.Text = ds.Tables[0].Rows[0]["Group_Name"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Tasks()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Group_Task_Logs", ID: hfv_lblGrpID.Value, Project_Id: Session["PROJECTID"].ToString());
                gvTasks.DataSource = ds.Tables[0];
                gvTasks.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvTasks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Task_ID = drv["Task_ID"].ToString();
                    string Sub_Task_ID = drv["Sub_Task_ID"].ToString();
                    string Multiple = drv["Multiple"].ToString();
                    string DtPlanCount = drv["DtPlanCount"].ToString();
                    string DtActualCount = drv["DtActualCount"].ToString();

                    GridView gvSites = e.Row.FindControl("gvSites") as GridView;
                    DataSet ds = dal.Budget_SP(Action: "get_Group_SubTask_Logs", Project_Id: Session["PROJECTID"].ToString(), Task_ID: Task_ID, Sub_Task_ID: Sub_Task_ID);
                    gvSites.DataSource = ds.Tables[0];
                    gvSites.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvSites.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Matrix()
        {
            try
            {
                DataSet ds = dal.CTMS_Matrix_SP(Action: "Matrix_View", Group_ID: hfv_lblGrpID.Value, ID: Session["PROJECTID"].ToString());
                lstm.DataSource = ds.Tables[0];
                lstm.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public int col = 0;
        protected void lstm_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                HtmlAnchor main = (HtmlAnchor)e.Item.FindControl("main");
                System.Web.UI.HtmlControls.HtmlGenericControl divcol = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divcol");
                int itemIndex = e.Item.DataItemIndex;

                string[] color = { "small-box bg-aqua", "small-box bg-green", "small-box bg-yellow", "small-box bg-red", "small-box bg-blue", "small-box bg-maroon" };
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 5)
                    {
                        col = 0;
                    }
                    divcol.Attributes.Add("class", color[col]);
                    col++;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Compare()
        {
            try
            {
                DataSet ds = dal.CTMS_Matrix_SP(Action: "get_Compare", Group_ID: hfv_lblGrpID.Value, ID: Session["PROJECTID"].ToString());
                gvCompTasks.DataSource = ds.Tables[0];
                gvCompTasks.DataBind();

                lstComapre.DataSource = ds.Tables[0];
                lstComapre.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvCompTasks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string P_Task_ID = drv["P_Task_ID"].ToString();
                    string P_SubTask_ID = drv["P_SubTask_ID"].ToString();
                    string C_Task_ID = drv["C_Task_ID"].ToString();
                    string C_SubTask_ID = drv["C_SubTask_ID"].ToString();
                    string C_Multiple = drv["C_Multiple"].ToString();
                    string P_Multiple = drv["P_Multiple"].ToString();
                    string ActualDateDiff = drv["Actual"].ToString();

                    Label lbl_Min = e.Row.FindControl("lbl_Min") as Label;
                    Label lbl_Avg = e.Row.FindControl("lbl_Avg") as Label;
                    Label lbl_Max = e.Row.FindControl("lbl_Max") as Label;
                    
                    GridView gvSites = e.Row.FindControl("gvSites") as GridView;
                    DataSet ds = dal.CTMS_Matrix_SP(Action: "Compare_View", ID: Session["PROJECTID"].ToString(), P_Task_ID: P_Task_ID, P_SubTask_ID: P_SubTask_ID, C_Task_ID: C_Task_ID, C_SubTask_ID: C_SubTask_ID);
                    gvSites.DataSource = ds.Tables[0];
                    gvSites.DataBind();

                    List<int> Out = new List<int>();

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int Actual = dr.Field<int>("Actual");
                        if (Actual != 0)
                        {
                            Out.Add(Actual);
                        }
                    }

                    lbl_Min.Text = Out.Min().ToString();
                    lbl_Max.Text = Out.Max().ToString();
                    lbl_Avg.Text = Out.Average().ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;

                    if (gvSites.Rows.Count > 0)
                    {
                        anchor.Visible = false;
                    }
                    else
                    {
                        anchor.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstComapre_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                HtmlAnchor main = (HtmlAnchor)e.Item.FindControl("main");
                System.Web.UI.HtmlControls.HtmlGenericControl divcol = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divcol");
                int itemIndex = e.Item.DataItemIndex;

                string[] color = { "small-box bg-aqua", "small-box bg-green", "small-box bg-yellow", "small-box bg-red", "small-box bg-blue", "small-box bg-maroon" };
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 5)
                    {
                        col = 0;
                    }
                    divcol.Attributes.Add("class", color[col]);
                    col++;
                }

                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRowView drv = (DataRowView)dataItem.DataItem;
                string P_Task_ID = drv["P_Task_ID"].ToString();
                string P_SubTask_ID = drv["P_SubTask_ID"].ToString();
                string C_Task_ID = drv["C_Task_ID"].ToString();
                string C_SubTask_ID = drv["C_SubTask_ID"].ToString();
                string C_Multiple = drv["C_Multiple"].ToString();
                string P_Multiple = drv["P_Multiple"].ToString();
                string ActualDateDiff = drv["Actual"].ToString();

                GridView gvSites = e.Item.FindControl("gvSites") as GridView;
                DataSet ds = dal.CTMS_Matrix_SP(Action: "Compare_View", ID: Session["PROJECTID"].ToString(), P_Task_ID: P_Task_ID, P_SubTask_ID: P_SubTask_ID, C_Task_ID: C_Task_ID, C_SubTask_ID: C_SubTask_ID);

                Label lbl_Min = e.Item.FindControl("lbl_Min") as Label;
                Label lbl_Avg = e.Item.FindControl("lbl_Avg") as Label;
                Label lbl_Max = e.Item.FindControl("lbl_Max") as Label;

                int minActual = 0;
                int maxActual = 0;
                int avgActual = 0;
                int totalActual = 0;
                int totalCount = 0;


                if (C_Multiple != "1" & P_Multiple != "1")
                {
                    lbl_Min.Text = ActualDateDiff.ToString();
                    lbl_Max.Text = ActualDateDiff.ToString();
                    lbl_Avg.Text = ActualDateDiff.ToString();
                }
                else
                {
                    List<int> Out = new List<int>();

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int Actual = dr.Field<int>("Actual");
                        if (Actual != 0)
                        {
                            Out.Add(Actual);
                        }
                    }

                    lbl_Min.Text = Out.Min().ToString();
                    lbl_Max.Text = Out.Max().ToString();
                    lbl_Avg.Text = Out.Average().ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Print_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("ReportCTMS_Group_Logs.aspx?Project_ID=" + Session["PROJECTID"].ToString() + "&Action=" + "get_Group_SubTask_Logs_Data" + "&GroupId=" + hfv_lblGrpID.Value);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}