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
    public partial class Budget_OtherDocuments : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Dept();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Dept()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Dept");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDept.DataSource = ds.Tables[0];
                    gvDept.DataBind();
                }
                else
                {
                    gvDept.DataSource = null;
                    gvDept.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvDept_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Dept_ID = drv["Dept_ID"].ToString();

                    GridView gvMainTask = e.Row.FindControl("gvMainTask") as GridView;
                    DataSet ds = dal.Budget_SP(Action: "get_Task", Dept_Id: Dept_ID);
                    gvMainTask.DataSource = ds.Tables[0];
                    gvMainTask.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvMainTask.Rows.Count > 0)
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

        protected void gvMainTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Task_ID = drv["Task_ID"].ToString();

                    GridView gvTasks = e.Row.FindControl("gvTasks") as GridView;
                    DataSet ds = dal.Budget_SP(Action: "get_Doc_Tasks", Project_Id: Session["PROJECTID"].ToString(), Task_ID: Task_ID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvTasks.DataSource = ds.Tables[0];
                        gvTasks.DataBind();
                    }
                    else
                    {
                        gvTasks.DataSource = null;
                        gvTasks.DataBind();
                    }

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvTasks.Rows.Count > 0)
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
    }
}