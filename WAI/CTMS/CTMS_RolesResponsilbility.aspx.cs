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
    public partial class CTMS_RolesResponsilbility : System.Web.UI.Page
    {
        DAL dal=  new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    bind_Roles();
                }
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
                DataSet ds = dal.Budget_SP(Action: "GET_BUDGET_ROLE");
                drpRole.DataSource = ds.Tables[0];
                drpRole.DataValueField = "Role_ID";
                drpRole.DataTextField = "Role";             
                drpRole.DataBind();
                drpRole.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void drpRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "GET_ROLE_TASK_Main", Project_Id: Session["PROJECTID"].ToString(), Role_ID: drpRole.SelectedValue);
                gvMain.DataSource = ds.Tables[0];
                gvMain.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void GridView1_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Task_ID = drv["Task_ID"].ToString();

                    GridView grdTask = e.Row.FindControl("grdTask") as GridView;
                    DataSet ds = dal.Budget_SP(Action: "GET_ROLE_TASK", Project_Id: Session["PROJECTID"].ToString(), Role_ID: drpRole.SelectedValue, Task_ID: Task_ID);
                    grdTask.DataSource = ds.Tables[0];
                    grdTask.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (grdTask.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }     
    }
}