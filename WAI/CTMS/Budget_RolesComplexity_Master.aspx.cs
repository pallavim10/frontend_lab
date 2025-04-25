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
    public partial class Budget_RolesComplexity_Master : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnupdateRole.Visible = false;
                    btnupdateCompl.Visible = false;
                    bind_Roles();
                    bind_DDLComplex();
                    bind_Complex();
                    bind_DDLRoles("");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_DDLComplex()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Complex", Role_ID: ddlRole.SelectedValue);
                ddlCompl.DataSource = ds.Tables[0];
                ddlCompl.DataValueField = "Complexity";
                ddlCompl.DataTextField = "Complexity";
                ddlCompl.DataBind();
                ddlCompl.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Complex()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_ProjComplex", Project_Id: Session["PROJECTID"].ToString());
                gvCompl.DataSource = ds.Tables[0];
                gvCompl.DataBind();
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
                gvRole.DataSource = ds.Tables[0];
                gvRole.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }        
        }

        private void bind_DDLRoles(string Role_ID)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_DDLRole", Project_Id: Session["PROJECTID"].ToString(), Role_ID: Role_ID);
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

        private void bind_RolesGV()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Role");
                ddlRole.DataSource = ds.Tables[0];

                gvRole.DataSource = ds.Tables[0];
                gvRole.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Roles(string ID)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "single_Role", Role_ID: ID);
                txtRole.Text = ds.Tables[0].Rows[0]["Role"].ToString();
                btnsubmitRole.Visible = false;
                btnupdateRole.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Roles()
        {
            try
            {
                dal.Budget_SP(Action: "insert_Role", Role: txtRole.Text);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }         
        }

        private void update_Roles()
        {
            try
            {
                dal.Budget_SP(Action: "update_Role", Role: txtRole.Text, Role_ID: Session["RoleID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Roles(string ID)
        {
            try
            {
                dal.Budget_SP(Action: "delete_Role", Role_ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Complex(string ID)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "single_ProjComplex", ID: ID);
                bind_DDLRoles(ID);
                ddlRole.SelectedValue = ds.Tables[0].Rows[0]["Role_ID"].ToString();
                bind_DDLComplex();
                ddlCompl.SelectedValue = ds.Tables[0].Rows[0]["Complexity"].ToString();
                ddlRole.Enabled = false;
                btnsubmitCompl.Visible = false;
                btnupdateCompl.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Complex()
        {
            try
            {
                dal.Budget_SP(Action: "insert_ProjComplex", Project_Id: Session["PROJECTID"].ToString(), Role_ID: ddlRole.SelectedValue, Complexity: ddlCompl.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Complex()
        {
            try
            {
                dal.Budget_SP(Action: "update_ProjComplex", Complexity: ddlCompl.SelectedItem.Text, ID: Session["ComplexID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }        
        }

        private void delete_Complex(string ID)
        {
            try
            {
                dal.Budget_SP(Action: "delete_ProjComplex", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitRole_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Roles();
                txtRole.Text = "";
                bind_Roles();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateRole_Click(object sender, EventArgs e)
        {
            try
            {
                update_Roles();
                txtRole.Text = "";
                btnupdateRole.Visible = false;
                btnsubmitRole.Visible = true;
                bind_Roles();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelRole_Click(object sender, EventArgs e)
        {
            try
            {
                txtRole.Text = "";
                btnupdateRole.Visible = false;
                btnsubmitRole.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["RoleID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_Roles(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Roles(id);
                    bind_Roles();
                }
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
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteRole = (e.Row.FindControl("lbtndeleteRole") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteRole.Visible = false;
                    }
                    else
                    {
                        lbtndeleteRole.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitCompl_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Complex();
                bind_DDLRoles("");
                ddlRole.SelectedIndex = 0;
                ddlCompl.SelectedIndex = 0;
                bind_RolesGV();
                bind_Complex();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateCompl_Click(object sender, EventArgs e)
        {
            try
            {
                update_Complex();
                ddlRole.SelectedIndex = 0;
                ddlCompl.SelectedIndex = 0;
                btnupdateCompl.Visible = false;
                btnsubmitCompl.Visible = true;
                bind_Complex();
                ddlRole.Enabled = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelCompl_Click(object sender, EventArgs e)
        {
            try
            {
                ddlRole.SelectedIndex = 0;
                ddlCompl.SelectedIndex = 0;
                btnupdateCompl.Visible = false;
                btnsubmitCompl.Visible = true;
                ddlRole.Enabled = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvCompl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["ComplexID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_Complex(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Complex(id);
                    bind_Complex();
                    bind_RolesGV();
                    bind_DDLRoles("");
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
                bind_DDLComplex();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}