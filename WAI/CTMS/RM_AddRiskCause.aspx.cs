using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace CTMS
{
    public partial class RM_AddRiskCause : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Bind_Cause();

                    if (Request.QueryString["TYPE"] == "UPDATE")
                    {
                        btnUpdate.Visible = true;
                        btnAdd.Visible = false;
                        EDIT(Request.QueryString["EventID"].ToString());
                    }
                    else
                    {
                        btnUpdate.Visible = false;
                        btnAdd.Visible = true;
                        txtComment.Text = "";
                        ddlCause.SelectedIndex = 0;
                        Bind_SubCause();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT()
        {
            try
            {
                dal.Risk_Cause_SP(
                Action: "INSERT",
                Event_ID: Request.QueryString["EventID"].ToString(),
                Project_ID: Session["PROJECTID"].ToString(),
                Cause: ddlCause.SelectedValue,
                SubCause: ddlSubCause.SelectedValue,
                Comment: txtComment.Text
                    );

                ddlCause.SelectedIndex = 0;
                ddlSubCause.SelectedIndex = 0;
                txtComment.Text = "";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE()
        {
            try
            {
                dal.Risk_Cause_SP(
                Action: "UPDATE",
                ID: Request.QueryString["EventID"].ToString(),
                Project_ID: Session["PROJECTID"].ToString(),
                Cause: ddlCause.SelectedValue,
                SubCause: ddlSubCause.SelectedValue,
                Comment: txtComment.Text
                    );

                ddlCause.SelectedIndex = 0;
                Bind_SubCause();
                txtComment.Text = "";
                btnAdd.Visible = true;
                btnUpdate.Visible = false;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT(string ID)
        {
            try
            {
                DataSet ds = dal.Risk_Cause_SP(Action: "SELECT", ID: ID);
                ddlCause.SelectedValue = ds.Tables[0].Rows[0]["Cause"].ToString();
                Bind_SubCause();
                ddlSubCause.SelectedValue = ds.Tables[0].Rows[0]["SubCause"].ToString();
                txtComment.Text = ds.Tables[0].Rows[0]["Comment"].ToString();
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Bind_SubCause()
        {
            try
            {
                DataSet ds = dal.getsetIssueRootCause(Action: "SubCause", RootCause: ddlCause.SelectedValue);
                ddlSubCause.DataSource = ds.Tables[0];
                ddlSubCause.DataValueField = "SubCause";
                ddlSubCause.DataTextField = "SubCause";
                ddlSubCause.DataBind(); 
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Bind_Cause()
        {
            try
            {
                DataSet ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "RootCause");
                ddlCause.DataSource = ds;
                ddlCause.DataTextField = "TEXT";
                ddlCause.DataValueField = "VALUE";
                ddlCause.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlCause_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_SubCause();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}