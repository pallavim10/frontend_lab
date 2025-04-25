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
    public partial class RM_AddRiskMitigation : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_ResponsiblePerson();
                    bind_Causes();

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

                        txtDate.Text = "";
                        ddlPrimaryPerson.SelectedIndex = 0;
                        ddlSecondaryPerson.SelectedIndex = 0;
                        txtMitigation.Text = "";
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
                string Secondary_RES, Secondary_Type, Primary_RES, Primary_Type;

                Primary_RES = Before(ddlPrimaryPerson.SelectedValue, "(");
                Secondary_RES = Before(ddlSecondaryPerson.SelectedValue, "(");

                Primary_Type = Between(ddlPrimaryPerson.SelectedValue, "(", ")");
                Secondary_Type = Between(ddlSecondaryPerson.SelectedValue, "(", ")");

                dal.Risk_Mitigation_SP(
                Action: "INSERT",
                Event_ID: Request.QueryString["EventID"].ToString(),
                Project_ID: Session["PROJECTID"].ToString(),
                Cause_ID: ddlCause.SelectedValue,
                Date: txtDate.Text,
                Secondary_RES: Secondary_RES,
                Secondary_Type: Secondary_Type,
                Primary_RES: Primary_RES,
                Primary_Type: Primary_Type,
                Mitigation: txtMitigation.Text
                    );

                txtDate.Text = "";
                ddlPrimaryPerson.SelectedIndex = 0;
                ddlSecondaryPerson.SelectedIndex = 0;
                ddlCause.SelectedIndex = 0;
                txtMitigation.Text = "";
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
                string Secondary_RES, Secondary_Type, Primary_RES, Primary_Type;

                Primary_RES = Before(ddlPrimaryPerson.SelectedValue, "(");
                Secondary_RES = Before(ddlSecondaryPerson.SelectedValue, "(");

                Primary_Type = Between(ddlPrimaryPerson.SelectedValue, "(", ")");
                Secondary_Type = Between(ddlSecondaryPerson.SelectedValue, "(", ")");

                dal.Risk_Mitigation_SP(
                Action: "UPDATE",
                ID: Request.QueryString["EventID"].ToString(),
                Cause_ID: ddlCause.SelectedValue,
                Date: txtDate.Text,
                Secondary_RES: Secondary_RES,
                Secondary_Type: Secondary_Type,
                Primary_RES: Primary_RES,
                Primary_Type: Primary_Type,
                Mitigation: txtMitigation.Text
                    );

                btnUpdate.Visible = false;
                btnAdd.Visible = true;

                txtDate.Text = "";
                ddlPrimaryPerson.SelectedIndex = 0;
                ddlSecondaryPerson.SelectedIndex = 0;
                ddlCause.SelectedIndex = 0;
                txtMitigation.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT(string id)
        {
            try
            {
                DataSet ds = dal.Risk_Mitigation_SP(Action: "SELECT", ID: id);
                ddlCause.SelectedValue = ds.Tables[0].Rows[0]["Cause_ID"].ToString();
                txtMitigation.Text = ds.Tables[0].Rows[0]["Mitigation"].ToString();
                ddlPrimaryPerson.SelectedValue = ds.Tables[0].Rows[0]["Primary"].ToString();
                ddlSecondaryPerson.SelectedValue = ds.Tables[0].Rows[0]["Secondary"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();

                btnUpdate.Visible = true;
                btnAdd.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public static string Before(string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(0, posA);
        }

        public static string Between(string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        protected void ddlPrimaryPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPrimaryPerson.SelectedItem.Text = Before(ddlPrimaryPerson.SelectedItem.Text, "(");
        }

        protected void ddlSecondaryPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSecondaryPerson.SelectedItem.Text = Before(ddlSecondaryPerson.SelectedItem.Text, "(");
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

        private void bind_ResponsiblePerson()
        {
            try
            {
                DataSet ds = dal.Risk_Mitigation_SP(Action: "GET_Responsible", Project_ID: Session["PROJECTID"].ToString());
                ddlPrimaryPerson.DataSource = ds.Tables[0];
                ddlPrimaryPerson.DataTextField = "Name";
                ddlPrimaryPerson.DataValueField = "ID";
                ddlPrimaryPerson.DataBind();
                ddlPrimaryPerson.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlSecondaryPerson.DataSource = ds.Tables[0];
                ddlSecondaryPerson.DataTextField = "Name";
                ddlSecondaryPerson.DataValueField = "ID";
                ddlSecondaryPerson.DataBind();
                ddlSecondaryPerson.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Causes()
        {
            try
            {
                DataSet ds;
                if (Request.QueryString["TYPE"] == "UPDATE")
                {
                    ds = dal.Risk_Mitigation_SP(Action: "DDL_Cause", Project_ID: Session["PROJECTID"].ToString(), Cause_ID: Request.QueryString["EventID"].ToString());
                }
                else
                {
                    ds = dal.Risk_Mitigation_SP(Action: "DDL_Cause", Project_ID: Session["PROJECTID"].ToString(), Event_ID: Request.QueryString["EventID"].ToString());
                }
                ddlCause.DataSource = ds.Tables[0];
                ddlCause.DataTextField = "Cause";
                ddlCause.DataValueField = "ID";
                ddlCause.DataBind();
                ddlCause.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}