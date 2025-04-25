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
    public partial class eTMF_Change_Status : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        GetCurrentStatus();
                        GetUsersStatus();
                        GetUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetCurrentStatus()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "GetCurrentStatus", DocID: Request.QueryString["DocID"].ToString());
                lblCurrentStatus.Text = ds.Tables[0].Rows[0]["Status"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetUsersStatus()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "GetUserDocStatus", DocID: Request.QueryString["DocID"].ToString(), Status: lblCurrentStatus.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    divaddedusers.Visible = true;
                    gvUsers.DataSource = ds;
                    gvUsers.DataBind();
                }
                else
                {
                    divaddedusers.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Done = drv["Done"].ToString();

                    Label lblCheck = e.Row.FindControl("lblCheck") as Label;
                    Label lblUnCheck = e.Row.FindControl("lblUnCheck") as Label;

                    if (Done == "1" || Done == "True")
                    {
                        lblCheck.Visible = true;
                        lblUnCheck.Visible = false;
                    }
                    else
                    {
                        lblCheck.Visible = false;
                        lblUnCheck.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
                {
                    divDeadline.Visible = true;
                    txtDeadline.Text = "";
                }
                else
                {
                    divDeadline.Visible = false;
                    txtDeadline.Text = "";
                }

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
                {
                    divUsers.Visible = true;
                }
                else
                {
                    divUsers.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetUsers()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "GetUsers");
                grd_Users.DataSource = ds;
                grd_Users.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dal.eTMF_SP(ACTION: "ChnageStatus", ID: Request.QueryString["DocID"].ToString(), Status: drpAction.SelectedValue, UploadBy: Session["User_ID"].ToString());

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: drpAction.SelectedValue, DocID: Request.QueryString["DocID"].ToString(), UploadBy: User_ID, DocName: User_Name, DeadlineDate: txtDeadline.Text);
                        }
                    }
                }

                //Response.Write("<script> alert('Status Changed successfully.') window.location.href = '" + Session["prevURL"].ToString().Substring(1) + "' </script>");

                Response.Redirect(Session["prevURL"].ToString(), false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            string PAGENAME = Session["prevURL"].ToString();
            Response.Redirect(PAGENAME);
        }
    }
}