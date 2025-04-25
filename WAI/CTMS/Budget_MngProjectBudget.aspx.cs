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
    public partial class Budget_MngProjectBudget : System.Web.UI.Page
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

        private void bind_Resources()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_SubTask_Resources", Project_Id: Session["PROJECTID"].ToString(), Task_ID: ddl_Tasks.SelectedValue, Sub_Task_ID: ddl_SubTask.SelectedValue);
                gvTasks.DataSource = ds.Tables[0];
                gvTasks.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Resources(string Role_ID)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "single_SubTask_Resources", Project_Id: Session["PROJECTID"].ToString(), Task_ID: ddl_Tasks.SelectedValue, Sub_Task_ID: ddl_SubTask.SelectedValue, Role_ID: Role_ID);
                bind_Role(Role_ID);
                ddlRole.SelectedValue = ds.Tables[0].Rows[0]["Role_ID"].ToString();
                txtRate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                txtHrs.Text = ds.Tables[0].Rows[0]["Hrs"].ToString();
                txtAmt.Text = ds.Tables[0].Rows[0]["Amt"].ToString();
                lbtnUpdate.Visible = true;
                lbtnSubmit.Visible = false;
                ddl_Tasks.Enabled = false;
                ddl_SubTask.Enabled = false;
                ddlRole.Enabled = false;
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
                DataSet ds = dal.Budget_SP(Action: "get_DS_Project_Task", Project_Id: Session["PROJECTID"].ToString(), Dept_Id: ddl_Dept.SelectedValue);
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
                DataSet ds = dal.Budget_SP(Action: "get_DS_Project_Dept", Project_Id: Session["PROJECTID"].ToString());
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

        private void bind_Role(string Role_ID)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_DS_Project_Task_DDL", Project_Id: Session["PROJECTID"].ToString(), Task_ID: ddl_Tasks.SelectedValue, Sub_Task_ID: ddl_SubTask.SelectedValue, Role_ID: Role_ID);
                ddlRole.DataSource = ds.Tables[0];
                ddlRole.DataValueField = "Role_ID";
                ddlRole.DataTextField = "Role";
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, new ListItem("--Select--", "0"));
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
                DataSet ds = dal.Budget_SP(Action: "get_DS_Project_Task", Project_Id: Session["PROJECTID"].ToString(), Task_ID: ddl_Tasks.SelectedValue);
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

        private void bind_Rate()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Role_Amt", Project_Id: Session["PROJECTID"].ToString(), Role_ID: ddlRole.SelectedValue);
                txtRate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
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
                Action: "insert_SubTask_Resources",
                Project_Id: Session["PROJECTID"].ToString(),
                Task_ID: ddl_Tasks.SelectedValue,
                Sub_Task_ID: ddl_SubTask.SelectedValue,
                Role_ID: ddlRole.SelectedValue,
                Hrs: txtHrs.Text
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Resource()
        {
            try
            {
                dal.Budget_SP(
                Action: "update_SubTask_Resources",
                Project_Id: Session["PROJECTID"].ToString(),
                Task_ID: ddl_Tasks.SelectedValue,
                Sub_Task_ID: ddl_SubTask.SelectedValue,
                Role_ID: ddlRole.SelectedValue,
                Hrs: txtHrs.Text
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Resource(string Role_Id)
        {
            try
            {
                dal.Budget_SP(
                Action: "delete_SubTask_Resources",
                Project_Id: Session["PROJECTID"].ToString(),
                Task_ID: ddl_Tasks.SelectedValue,
                Sub_Task_ID: ddl_SubTask.SelectedValue,
                Role_ID: Role_Id
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
                int Rate, Hrs, Amt;
                Rate = Convert.ToInt32(txtRate.Text);
                if (txtHrs.Text == "")
                {
                    Hrs = 0;
                }
                else
                {
                    Hrs = Convert.ToInt32(txtHrs.Text);
                }
                Amt = Rate * Hrs;

                txtAmt.Text = Amt.ToString();
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
                bind_SubTasks();
                bind_Resources();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlRole.SelectedValue.ToString() != "0")
                {
                    bind_Rate();
                    calc_Amt();
                }
                else
                {
                    txtRate.Text = "";
                    txtHrs.Text = "";
                    txtAmt.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void txtHrs_TextChanged(object sender, EventArgs e)
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

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Resource();
                bind_Role("");
                txtRate.Text = "";
                txtHrs.Text = "";
                txtAmt.Text = "";
                bind_Resources();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddl_SubTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Role("");
                bind_Resources();
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
                ddlRole.Enabled = true;
                bind_Role("");
                txtRate.Text = "";
                txtHrs.Text = "";
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
                update_Resource();
                bind_Resources();
                ddl_Tasks.Enabled = true;
                ddl_SubTask.Enabled = true;
                ddlRole.Enabled = true;
                bind_Role("");
                txtRate.Text = "";
                txtHrs.Text = "";
                txtAmt.Text = "";
                lbtnUpdate.Visible = false;
                lbtnSubmit.Visible = true;
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
                    edit_Resources(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Resource(id);
                    bind_Resources();
                    bind_Role("");
                }
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
                bind_SubTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}