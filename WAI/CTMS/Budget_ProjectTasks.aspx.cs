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
    public partial class Budget_ProjectTasks : System.Web.UI.Page
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        public void bind_Dept()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Dept");
                ddl_Dept.DataSource = ds.Tables[0];
                ddl_Dept.DataValueField = "Dept_Id";
                ddl_Dept.DataTextField = "Dept_Name";
                ddl_Dept.DataBind();
                ddl_Dept.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_Task()
        {
            try
            {
                DataSet ds1 = dal.Budget_SP(Action: "get_Task", Dept_Id: ddl_Dept.SelectedValue.ToString());
                ddl_Task.DataSource = ds1.Tables[0];
                ddl_Task.DataValueField = "Task_ID";
                ddl_Task.DataTextField = "Task_Name";
                ddl_Task.DataBind();
                ddl_Task.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_SubTask()
        {
            try
            {
                string SectionID = null;
                string SubSectionID = null;
                if (ddl_Dept.SelectedValue != "0")
                {
                    SectionID = ddl_Dept.SelectedValue;
                }
                if (ddl_Task.SelectedValue != "0")
                {
                    SubSectionID = ddl_Task.SelectedValue;
                }

                DataSet ds = dal.Budget_SP(Action: "get_SubTask", Task_ID: ddl_Task.SelectedValue.ToString());
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
                DAL dal;
                dal = new DAL();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    CheckBox Chk_Sel_Fun = (e.Row.FindControl("Chk_Sel_Fun") as CheckBox);
                    CheckBox Chk_Sel_Remove_Fun = (e.Row.FindControl("Chk_Sel_Remove_Fun") as CheckBox);
                    if (Convert.ToInt32(id) > 0)
                    {
                        Chk_Sel_Fun.Visible = false;
                        Chk_Sel_Remove_Fun.Visible = true;
                    }
                    else
                    {
                        Chk_Sel_Fun.Visible = true;
                        Chk_Sel_Remove_Fun.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Btn_Add_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvTasks.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvTasks.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        string lbl_TaskId = ((Label)gvTasks.Rows[i].FindControl("lbl_TaskId")).Text;
                        string lbl_SubTaskId = ((Label)gvTasks.Rows[i].FindControl("lbl_SubTaskId")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Budget_SP(Action: "add_Project_Task", Project_Id: ProjectID, Task_ID: lbl_TaskId, Sub_Task_ID:lbl_SubTaskId);
                    }
                }
                bind_SubTask();
                //Response.Write("<script> alert('Record Added successfully.');window.location='Checklist_Master.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Btn_Remove_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvTasks.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvTasks.Rows[i].FindControl("Chk_Sel_Remove_Fun");

                    if (ChAction.Checked)
                    {
                        string lbl_TaskId = ((Label)gvTasks.Rows[i].FindControl("lbl_TaskId")).Text;
                        string lbl_SubTaskId = ((Label)gvTasks.Rows[i].FindControl("lbl_SubTaskId")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Budget_SP(Action: "remove_Project_Task", Project_Id: ProjectID, Task_ID: lbl_TaskId, Sub_Task_ID: lbl_SubTaskId);
                    }
                }
                bind_SubTask();
                //Response.Write("<script> alert('Record Removed successfully.');window.location='Checklist_Master.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void ddl_Dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Task();
                bind_SubTask();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void ddl_Task_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_SubTask();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void gvTasks_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }
    }
}