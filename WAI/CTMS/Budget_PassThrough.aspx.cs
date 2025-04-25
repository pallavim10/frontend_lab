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
    public partial class Budget_PassThrough : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Dept();
                    lbtnUpdate.Visible = false;
                }
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
                DataSet ds = dal.Budget_SP(Action: "get_PT_Project_Task", Project_Id: Session["PROJECTID"].ToString(), Dept_Id: ddl_Dept.SelectedValue);
                ddl_Tasks.DataSource = ds.Tables[0];
                ddl_Tasks.DataValueField = "Task_ID";
                ddl_Tasks.DataTextField = "Task_Name";
                ddl_Tasks.DataBind();
                ddl_Tasks.Items.Insert(0, new ListItem("--Select--", "0"));
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
                DataSet ds = dal.Budget_SP(Action: "get_PT_Project_Dept", Project_Id: Session["PROJECTID"].ToString());
                ddl_Dept.DataSource = ds.Tables[0];
                ddl_Dept.DataValueField = "Dept_ID";
                ddl_Dept.DataTextField = "Dept_Name";
                ddl_Dept.DataBind();
                ddl_Dept.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_SubTasks_DLL()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_PT_Project_Task_DDL", Project_Id: Session["PROJECTID"].ToString(), Task_ID: ddl_Tasks.SelectedValue);
                ddl_SubTask.DataSource = ds.Tables[0];
                ddl_SubTask.DataValueField = "Sub_Task_ID";
                ddl_SubTask.DataTextField = "Sub_Task_Name";
                ddl_SubTask.DataBind();
                ddl_SubTask.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_DDL_SubTasks(string Task_id)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_DDL_PT_Project_Task", Project_Id: Session["PROJECTID"].ToString(), Task_ID: ddl_Tasks.SelectedValue, Sub_Task_ID: Task_id);
                ddl_SubTask.DataSource = ds.Tables[0];
                ddl_SubTask.DataValueField = "Sub_Task_ID";
                ddl_SubTask.DataTextField = "Sub_Task_Name";
                ddl_SubTask.DataBind();
                ddl_SubTask.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_SubTasks()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_PT_Project_Task", Project_Id: Session["PROJECTID"].ToString(), Task_ID: ddl_Tasks.SelectedValue);
                gvTasks.DataSource = ds.Tables[0];
                gvTasks.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Resource()
        {
            try
            {
                dal.Budget_SP(
                Action: "passThrough_Budget",
                Project_Id: Session["PROJECTID"].ToString(),
                Task_ID: ddl_Tasks.SelectedValue,
                Sub_Task_ID: ddl_SubTask.SelectedValue,
                Unit: txtUnits.Text,
                Cost_Per_Unit: txtRate.Text
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void calc_Amt()
        {
            try
            {
                int Rate, Units, Amt;
                if (txtRate.Text != "")
                {
                    Rate = Convert.ToInt32(txtRate.Text);
                }
                else
                {
                    Rate = 0;
                }

                if (txtUnits.Text != "")
                {
                    Units = Convert.ToInt32(txtUnits.Text);
                }
                else
                {
                    Units = 0;
                }                

                Amt = Rate * Units;

                txtAmt.Text = Amt.ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_SubTask(string Sub_Task_id)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "edit_passThrough_Budget", Task_ID: ddl_Tasks.SelectedValue.ToString(), Sub_Task_ID: Sub_Task_id, Project_Id: Session["PROJECTID"].ToString());
                bind_DDL_SubTasks(Sub_Task_id);
                ddl_Tasks.Enabled = false;
                ddl_SubTask.Enabled = false;
                txtUnits.Text = ds.Tables[0].Rows[0]["Unit"].ToString();
                txtRate.Text = ds.Tables[0].Rows[0]["Cost_per_unit"].ToString();
                ddl_SubTask.SelectedValue = ds.Tables[0].Rows[0]["Sub_Task_Id"].ToString();
                calc_Amt();
                lbtnSubmit.Visible = false;
                lbtnUpdate.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_SubTask(string Sub_Task_id)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "delete_passThrough_Budget", Task_ID: ddl_Tasks.SelectedValue.ToString(), Sub_Task_ID: Sub_Task_id, Project_Id: Session["PROJECTID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddl_Tasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_SubTasks_DLL();
                bind_SubTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Resource();
                ddl_Tasks.Enabled = true;
                ddl_SubTask.Enabled = true;
                lbtnSubmit.Visible = true;
                lbtnUpdate.Visible = false;
                txtRate.Text = "";
                txtUnits.Text = "";
                txtAmt.Text = "";
                bind_SubTasks_DLL();
                bind_SubTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ddl_Tasks.Enabled = true;
                ddl_SubTask.Enabled = true;
                txtRate.Text = "";
                txtUnits.Text = "";
                txtAmt.Text = "";
                lbtnUpdate.Visible = false;
                lbtnSubmit.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Resource();
                ddl_Tasks.Enabled = true;
                ddl_SubTask.Enabled = true;
                txtRate.Text = "";
                txtUnits.Text = "";
                txtAmt.Text = "";
                lbtnUpdate.Visible = false;
                lbtnSubmit.Visible = true;
                bind_SubTasks_DLL();
                bind_SubTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["RoleID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_SubTask(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_SubTask(id);
                    bind_SubTasks_DLL();
                    bind_SubTasks();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void txtUnits_TextChanged(object sender, EventArgs e)
        {
            try
            {
                calc_Amt();
                txtRate.Focus();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                calc_Amt();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddl_Dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Tasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}