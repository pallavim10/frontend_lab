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
    public partial class SB_Rate_Master : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Bind_InvID();
                    btnsubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_GV()
        {
            try
            {
                DataSet ds = dal.SiteBudget_SP(Action: "get_Task_Rate_New", Project_Id: Session["PROJECTID"].ToString(), Site_ID: ddl_INVID.SelectedValue );
                gvRates.DataSource = ds.Tables[0];
                gvRates.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Bind_InvID()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSiteID(
                Action: "INVID",
                Project_Name: Session["PROJECTIDTEXT"].ToString(), User_ID: Session["User_ID"].ToString()
                );
                ddl_INVID.DataSource = ds.Tables[0];
                ddl_INVID.DataValueField = "INVNAME";
                ddl_INVID.DataBind();
                ddl_INVID.Items.Insert(0, new ListItem("--Select--", "99"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddl_INVID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_INVID.SelectedIndex != 0)
                {
                    bind_GV();
                    if (gvRates.Rows.Count > 0)
                    {
                        btnsubmit.Visible = true;
                    }
                }
                else
                {
                    btnsubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Rates()
        {
            try
            {
                for (int i = 0; i < gvRates.Rows.Count; i++)
                {
                    string lbl_TaskId = ((Label)gvRates.Rows[i].FindControl("lbl_TaskId")).Text;
                    string lbl_SubTaskId = ((Label)gvRates.Rows[i].FindControl("lbl_SubTaskId")).Text;
                    string txtRate = ((TextBox)gvRates.Rows[i].FindControl("txtRate")).Text;
                    dal.SiteBudget_SP(Action: "insert_Rate", Task_Id: lbl_TaskId, Sub_Task_ID: lbl_SubTaskId, Rate: txtRate, Site_ID: ddl_INVID.SelectedValue, Project_Id: Session["PROJECTID"].ToString());
                }
                Response.Write("<script> alert('Record Updated successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Rates();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}