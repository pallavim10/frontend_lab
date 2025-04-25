using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.SqlClient;

namespace CTMS
{
    public partial class Train_Study_Team_Site : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    get_INVs();
                    bind_Team();
                    lbtnUpdate.Visible = false;
                }
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
                insert_Team();
                txtName.Text = "";
                txtRole.Text = "";
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                txtEmailID.Text = "";
                bind_Team();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ddlINV.SelectedIndex = 0;
                txtName.Text = "";
                txtRole.Text = "";
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                txtEmailID.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_INVs()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_INV", Project_ID: Session["PROJECTID"].ToString());
                ddlINV.DataSource = ds.Tables[0];
                ddlINV.DataValueField = "INVID";
                ddlINV.DataTextField = "INVNAM";
                ddlINV.DataBind();
                ddlINV.Items.Insert(0, new ListItem("--Select--", "0"));
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
                dal.Training_SP(Action: "insert_Team_Site", INVID: ddlINV.SelectedValue, Name: txtName.Text, Role: txtRole.Text, StartDate: txtStartDate.Text, EndDate: txtEndDate.Text, Project_ID: Session["PROJECTID"].ToString(), EmailID: txtEmailID.Text);
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
                dal.Training_SP(Action: "update_Team_Site", ID: Session["TEAMID"].ToString(), Name: txtName.Text, Role: txtRole.Text, StartDate: txtStartDate.Text, EndDate: txtEndDate.Text, EmailID: txtEmailID.Text);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Team(string id)
        {
            try
            {
                dal.Training_SP(Action: "delete_Team_Site", ID: id);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Team(string id)
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "select_team_Site", ID: id);
                get_INVs();
                ddlINV.SelectedValue = ds.Tables[0].Rows[0]["INVID"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtRole.Text = ds.Tables[0].Rows[0]["Role"].ToString();
                txtStartDate.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                txtEndDate.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                txtEmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                lbtnSubmit.Visible = false;
                lbtnUpdate.Visible = true;
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
                update_Team();
                txtName.Text = "";
                txtRole.Text = "";
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                txtEmailID.Text = "";
                lbtnSubmit.Visible = true;
                lbtnUpdate.Visible = false;
                bind_Team();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvTeam_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    bind_Team();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Team()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_Team_Site", Project_ID: Session["PROJECTID"].ToString());
                gvTeam.DataSource = ds.Tables[0];
                gvTeam.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}