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
    public partial class CTMS_BudgetiTOM : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Dept();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Dept()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Dept_TOM", Project_Id: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDept.DataSource = ds.Tables[0];
                    gvDept.DataBind();
                }
                else
                {
                    gvDept.DataSource = null;
                    gvDept.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvDept_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Dept_ID = drv["Dept_ID"].ToString();

                    GridView gvMain = e.Row.FindControl("gvMain") as GridView;
                    DataSet ds = dal.Budget_SP(Action: "get_TOM_Project_Task", Project_Id: Session["PROJECTID"].ToString(), Dept_Id: Dept_ID);
                    gvMain.DataSource = ds.Tables[0];
                    gvMain.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvMain.Rows.Count > 0)
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

        protected void gvTasks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Task_ID = drv["Task_ID"].ToString();
                    string Sub_Task_ID = drv["Sub_Task_ID"].ToString();
                    string Sponsor = drv["Sponsor"].ToString();
                    string Self = drv["DS"].ToString();
                    string Site = drv["Site"].ToString();
                    string Others = drv["Others"].ToString();
                    string Training = drv["Training"].ToString();

                    Label lblSponsorCheck = (e.Row.FindControl("lblSponsorCheck") as Label);
                    Label lblSponsorUnCheck = (e.Row.FindControl("lblSponsorUnCheck") as Label);
                    if (Sponsor == "True")
                    {
                        lblSponsorCheck.Visible = true;
                        lblSponsorUnCheck.Visible = false;
                    }
                    else
                    {
                        lblSponsorCheck.Visible = false;
                        lblSponsorUnCheck.Visible = true;
                    }

                    Label lblDSCheck = (e.Row.FindControl("lblDSCheck") as Label);
                    Label lblDSUnCheck = (e.Row.FindControl("lblDSUnCheck") as Label);
                    if (Self == "True")
                    {
                        lblDSCheck.Visible = true;
                        lblDSUnCheck.Visible = false;
                    }
                    else
                    {
                        lblDSCheck.Visible = false;
                        lblDSUnCheck.Visible = true;
                    }

                    Label lblSiteCheck = (e.Row.FindControl("lblSiteCheck") as Label);
                    Label lblSiteUnCheck = (e.Row.FindControl("lblSiteUnCheck") as Label);
                    if (Site == "True")
                    {
                        lblSiteCheck.Visible = true;
                        lblSiteUnCheck.Visible = false;
                    }
                    else
                    {
                        lblSiteCheck.Visible = false;
                        lblSiteUnCheck.Visible = true;
                    }

                    Label lblOthersCheck = (e.Row.FindControl("lblOthersCheck") as Label);
                    Label lblOthersUnCheck = (e.Row.FindControl("lblOthersUnCheck") as Label);
                    if (Others == "True")
                    {
                        lblOthersCheck.Visible = true;
                        lblOthersUnCheck.Visible = false;
                    }
                    else
                    {
                        lblOthersCheck.Visible = false;
                        lblOthersUnCheck.Visible = true;
                    }

                    CheckBox chkTrain = (e.Row.FindControl("chkTrain") as CheckBox);
                    if (Training == "True")
                    {
                        chkTrain.Checked = true;
                    }
                    else
                    {
                        chkTrain.Checked = false;
                    }

                    Label lbl_Amt = (e.Row.FindControl("lbl_Amt") as Label);
                    GridView gvRoles = e.Row.FindControl("gvRoles") as GridView;
                    DataSet ds = dal.Budget_SP(Action: "get_SubTask_Resources", Project_Id: Session["PROJECTID"].ToString(), Task_ID: Task_ID, Sub_Task_ID: Sub_Task_ID);
                    gvRoles.DataSource = ds.Tables[0];
                    gvRoles.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvRoles.Rows.Count > 0)
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

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Task_ID = drv["Task_ID"].ToString();

                    GridView gvTasks = e.Row.FindControl("gvTasks") as GridView;
                    DataSet ds = dal.Budget_SP(Action: "get_CTMS_iTOM", Project_Id: Session["PROJECTID"].ToString(), Task_ID: Task_ID);
                    gvTasks.DataSource = ds.Tables[0];
                    gvTasks.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvTasks.Rows.Count > 0)
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
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                String ID = e.CommandArgument.ToString();

                if (e.CommandName == "EnableTrain")
                {
                    dal.Budget_SP(Action: "enable_Training_Project_Task", ID: ID);
                }
                if (e.CommandName == "DisableTrain")
                {
                    dal.Budget_SP(Action: "disable_Training_Project_Task", ID: ID);
                }
                bind_Dept();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EnableDisable_Training()
        {
            try
            {
                for (int a = 0; a < gvDept.Rows.Count; a++)
                {
                    GridView gvMain = gvDept.Rows[a].FindControl("gvMain") as GridView;

                    for (int b = 0; b < gvMain.Rows.Count; b++)
                    {
                        GridView gvTasks = gvMain.Rows[b].FindControl("gvTasks") as GridView;

                        for (int i = 0; i < gvTasks.Rows.Count; i++)
                        {
                            string lbl_Id = ((Label)gvTasks.Rows[i].FindControl("lbl_Id")).Text;
                            CheckBox chkTrain = (gvTasks.Rows[i].FindControl("chkTrain") as CheckBox);

                            if (chkTrain.Checked == true)
                            {
                                dal.Budget_SP(Action: "enable_Training_Project_Task", ID: lbl_Id, Project_Id: Session["PROJECTID"].ToString());
                            }
                            else
                            {
                                dal.Budget_SP(Action: "disable_Training_Project_Task", ID: lbl_Id, Project_Id: Session["PROJECTID"].ToString());
                            }
                        }
                    }
                }
                bind_Dept();
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
                EnableDisable_Training();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //protected void Print_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Response.Redirect("ReportBudgetTOM.aspx?Project_ID=" + Session["PROJECTID"].ToString() + "&Action=" + "get_TOM_ProjectTaks_Data");
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}
    }
}