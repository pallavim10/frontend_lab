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
    public partial class Budget_ManageProjectTasks : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_DeptDrp();
                    btnSubmit.Visible = false;
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
                DataSet ds = dal.Budget_SP(Action: "get_Project_Task_TOM", Project_Id: Session["PROJECTID"].ToString(), Task_ID: ddl_Task.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnSubmit.Visible = true;
                    gvTasks.DataSource = ds.Tables[0];
                    gvTasks.DataBind();
                }
                else
                {
                    gvTasks.DataSource = null;
                    gvTasks.DataBind();
                    btnSubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_TaskDrp()
        {
            try
            {
                DataSet ds1 = dal.Budget_SP(Action: "get_Task_Master", Project_Id: Session["PROJECTID"].ToString(), Dept_Id: ddl_Dept.SelectedValue);
                ddl_Task.DataSource = ds1.Tables[0];
                ddl_Task.DataValueField = "Task_ID";
                ddl_Task.DataTextField = "Task_Name";
                ddl_Task.DataBind();
                ddl_Task.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_DeptDrp()
        {
            try
            {
                DataSet ds1 = dal.Budget_SP(Action: "get_Dept_Master", Project_Id: Session["PROJECTID"].ToString());
                ddl_Dept.DataSource = ds1.Tables[0];
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

        protected void gvTasks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Sponsor = drv["Sponsor"].ToString();
                    string Self = drv["DS"].ToString();
                    string Site = drv["Site"].ToString();
                    string Others = drv["Others"].ToString();
                    CheckBox chkSponsor = (e.Row.FindControl("chkSponsor") as CheckBox);
                    if (Sponsor == "True")
                    {
                        chkSponsor.Checked = true;
                    }
                    else
                    {
                        chkSponsor.Checked = false;
                    }
                    CheckBox chkSelf = (e.Row.FindControl("chkSelf") as CheckBox);
                    if (Self == "True")
                    {
                        chkSelf.Checked = true;
                    }
                    else
                    {
                        chkSelf.Checked = false;
                    }
                    CheckBox chkSite = (e.Row.FindControl("chkSite") as CheckBox);
                    if (Site == "True")
                    {
                        chkSite.Checked = true;
                    }
                    else
                    {
                        chkSite.Checked = false;
                    }
                    CheckBox chkOthers = (e.Row.FindControl("chkOthers") as CheckBox);
                    if (Others == "True")
                    {
                        chkOthers.Checked = true;
                    }
                    else
                    {
                        chkOthers.Checked = false;
                    }
                }
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
                string Sponsor = "";
                string Self = "";
                string Site = "";
                string Others = "";
                for (int i = 0; i < gvTasks.Rows.Count; i++)
                {
                    Label lbl_TaskId = (Label)gvTasks.Rows[i].FindControl("lbl_TaskId");
                    Label lbl_SubTaskId = (Label)gvTasks.Rows[i].FindControl("lbl_SubTaskId");
                    CheckBox chkSponsor = (CheckBox)gvTasks.Rows[i].FindControl("chkSponsor");
                    CheckBox chkSelf = (CheckBox)gvTasks.Rows[i].FindControl("chkSelf");
                    CheckBox chkSite = (CheckBox)gvTasks.Rows[i].FindControl("chkSite");
                    CheckBox chkOthers = (CheckBox)gvTasks.Rows[i].FindControl("chkOthers");

                    if (chkSponsor.Checked)
                    {
                        Sponsor = "1";
                    }
                    else
                    {
                        Sponsor = "";
                    }

                    if (chkSelf.Checked)
                    {
                        Self = "1";
                    }
                    else
                    {
                        Self = "";
                    }

                    if (chkSite.Checked)
                    {
                        Site = "1";
                    }
                    else
                    {
                        Site = "";
                    }

                    if (chkOthers.Checked)
                    {
                        Others = "1";
                    }
                    else
                    {
                        Others = "";
                    }

                    dal.Budget_SP(
                    Action: "update_Project_Task",
                    Project_Id: Session["PROJECTID"].ToString(),
                    Task_ID: lbl_TaskId.Text,
                    Sub_Task_ID: lbl_SubTaskId.Text,
                    Sponsor: Sponsor,
                    DS: Self,
                    Site: Site,
                    Others: Others
                        );
                }

                Response.Write("<script> alert('Record Updated successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddl_Task_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddl_Dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_TaskDrp();
                bind_Tasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}