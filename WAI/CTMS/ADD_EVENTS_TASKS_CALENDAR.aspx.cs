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
    public partial class ADD_EVENTS_TASKS_CALENDAR : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GETTASKDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETTASKDATA()
        {
            try
            {
                DataSet ds = dal.Dashboard_SP(Action: "GET_TASK_CAL", Project_ID: Session["PROJECTID"].ToString(), User_ID: Session["User_ID"].ToString());

                grdEvnetData.DataSource = ds.Tables[0];
                grdEvnetData.DataBind();
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
                DataSet ds = dal.Dashboard_SP(Action: "INSERT_TASK_CAL",
                User_ID: Session["User_ID"].ToString(),
                Project_ID: Session["PROJECTID"].ToString(),
                Type: txttaskname.Text,
                X: txtstartdate.Text,
                Y: txtenddate.Text,
                Height: txtstarttime.Text,
                Width: txtendtime.Text
                );

                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                txtstartdate.Text = "";
                txtenddate.Text = "";
                txtstarttime.Text = "";
                txtendtime.Text = "";
                GETTASKDATA();
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
                DataSet ds = dal.Dashboard_SP(Action: "UPDATE_TASK_CAL",
               User_ID: Session["User_ID"].ToString(),
               Project_ID: Session["PROJECTID"].ToString(),
               Type: txttaskname.Text,
               X: txtstartdate.Text,
               Y: txtenddate.Text,
               ID: ViewState["ID"].ToString(),
                Height: txtstarttime.Text,
                Width: txtendtime.Text
               );

                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                txtstartdate.Text = "";
                txtenddate.Text = "";
                txtstarttime.Text = "";
                txtendtime.Text = "";
                GETTASKDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            txtstartdate.Text = "";
            txtenddate.Text = "";
            txtstarttime.Text = "";
            txtendtime.Text = "";
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void grdEvnetData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                ViewState["ID"] = id;

                if (e.CommandName == "EditField")
                {
                    DataSet ds = dal.Dashboard_SP(Action: "GET_TASK_CAL_BYID", ID: id);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txttaskname.Text = ds.Tables[0].Rows[0]["TASKNAME"].ToString();
                        txtstartdate.Text = ds.Tables[0].Rows[0]["TASKSTARTDATE"].ToString();
                        txtenddate.Text = ds.Tables[0].Rows[0]["TASKENDDATE"].ToString();
                        txtstarttime.Text = ds.Tables[0].Rows[0]["TASKSTARTTIME"].ToString();
                        txtendtime.Text = ds.Tables[0].Rows[0]["TASKENDTIME"].ToString();
                    }

                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (e.CommandName == "DeleteField")
                {
                    DataSet ds = dal.Dashboard_SP(Action: "DELETE_TASK_CAL", ID: id);
                    GETTASKDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}