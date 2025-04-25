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
    public partial class Train_Study_Team : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Roles();
                    bind_EmpsDDL(null);
                    bind_View();
                    btnupdateTeam.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_View()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_Role_Team_View", Project_ID: Session["PROJECTID"].ToString());
                gvRole.DataSource = ds.Tables[0];
                gvRole.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Roles()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Role");
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

        private void bind_EmpsDDL(string ID)
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "getEmps", Project_ID: Session["PROJECTID"].ToString(), ID: ID, MASTERDBNAME: Session["MASTERDBNAME"].ToString());
                ddlEmp.DataSource = ds.Tables[0];
                ddlEmp.DataValueField = "ID";
                ddlEmp.DataTextField = "Name";
                ddlEmp.DataBind();
                ddlEmp.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Emps()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "getTeam", Project_ID: Session["PROJECTID"].ToString(), Role_ID: ddlRole.SelectedValue);
                gvEmp.DataSource = ds.Tables[0];
                gvEmp.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["TEAMID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_Team(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Team(id);
                    bind_Emps();
                    bind_EmpsDDL("");
                    bind_View();
                }
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
                bind_Emps();
                bind_EmpsDDL("");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Team()
        {
            try
            {
                dal.Training_SP(Action: "insert_Team", Role_ID: ddlRole.SelectedValue, Emp_ID: ddlEmp.SelectedValue, StartDate: txtSTDate.Text, EndDate: txtENDate.Text, Project_ID: Session["PROJECTID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Team(string ID)
        {
            try
            {
                dal.Training_SP(Action: "delete_Team", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Team()
        {
            try
            {
                dal.Training_SP(Action: "update_Team", ID: Session["TEAMID"].ToString(), Role_ID: ddlRole.SelectedValue, Emp_ID: ddlEmp.SelectedValue, StartDate: txtSTDate.Text, EndDate: txtENDate.Text);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Team(string ID)
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "single_Team", ID: ID);
                ddlRole.SelectedValue = ds.Tables[0].Rows[0]["Role_ID"].ToString();
                bind_EmpsDDL(ds.Tables[0].Rows[0]["Emp_ID"].ToString());
                ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                txtSTDate.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                txtENDate.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                btnsubmitTeam.Visible = false;
                btnupdateTeam.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitTeam_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Team();
                bind_Emps();
                bind_EmpsDDL("");
                bind_View();
                ddlEmp.SelectedIndex = 0;
                txtSTDate.Text = "";
                txtENDate.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateTeam_Click(object sender, EventArgs e)
        {
            try
            {
                update_Team();
                bind_Emps();
                bind_EmpsDDL("");
                bind_View();
                ddlEmp.SelectedIndex = 0;
                txtSTDate.Text = "";
                txtENDate.Text = "";
                btnupdateTeam.Visible = false;
                btnsubmitTeam.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelTeam_Click(object sender, EventArgs e)
        {
            try
            {
                ddlRole.SelectedIndex = 0;
                ddlEmp.SelectedIndex = 0;
                txtSTDate.Text = "";
                txtENDate.Text = "";
                btnupdateTeam.Visible = false;
                btnsubmitTeam.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Role_ID = drv["Role_ID"].ToString();

                    GridView gvEmpView = e.Row.FindControl("gvEmpView") as GridView;
                    DataSet ds = dal.Training_SP(Action: "getTeam", Role_ID: Role_ID, Project_ID: Session["PROJECTID"].ToString());
                    gvEmpView.DataSource = ds.Tables[0];
                    gvEmpView.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}