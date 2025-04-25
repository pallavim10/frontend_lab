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
    public partial class Budget_TOM : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Version();
                }                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void create_NewVersion()
        {
            try
            {
                dal.Budget_SP(Action: "create_Version", Project_Id: Session["PROJECTID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Version()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Version", Project_Id: Session["PROJECTID"].ToString());
                ddl_Version.DataSource = ds.Tables[0];
                ddl_Version.DataValueField = "Version_ID";
                ddl_Version.DataTextField = "Version_ID";
                ddl_Version.DataBind();
                ddl_Version.Items.Insert(0, new ListItem("--Select--", "0"));
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

                    LinkButton lbtnTrainCheck = (e.Row.FindControl("lbtnTrainCheck") as LinkButton);
                    LinkButton lbtnTrainUnCheck = (e.Row.FindControl("lbtnTrainUnCheck") as LinkButton);
                    if (Training == "True")
                    {
                        lbtnTrainCheck.Visible = true;
                        lbtnTrainUnCheck.Visible = false;
                    }
                    else
                    {
                        lbtnTrainCheck.Visible = false;
                        lbtnTrainUnCheck.Visible = true;
                    }

                    Label lbl_Amt = (e.Row.FindControl("lbl_Amt") as Label);
                    GridView gvRoles = e.Row.FindControl("gvRoles") as GridView;
                    DataSet ds = dal.Budget_SP(Action: "get_SubTask_Resources", Project_Id: Session["PROJECTID"].ToString(), Task_ID: Task_ID, Sub_Task_ID: Sub_Task_ID, Version_ID: ddl_Version.SelectedValue);
                    gvRoles.DataSource = ds.Tables[0];
                    gvRoles.DataBind();
                    int Total = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Total = Convert.ToInt32(ds.Tables[0].Compute("SUM(Amt)", string.Empty));
                    }
                    lbl_Amt.Text = Total.ToString();

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
                    DataSet ds = dal.Budget_SP(Action: "get_CTMS_TOM", Project_Id: Session["PROJECTID"].ToString(), Task_ID: Task_ID, Version_ID: ddl_Version.SelectedValue);
                    gvTasks.DataSource = ds.Tables[0];
                    gvTasks.DataBind();
                                        
                    Label lbl_MainAmt = (e.Row.FindControl("lbl_MainAmt") as Label);
                    int Amt = 0;
                    foreach (GridViewRow row in gvTasks.Rows)
                    {
                        Label lbl_Amt = (row.FindControl("lbl_Amt") as Label);
                        Amt += Convert.ToInt32(lbl_Amt.Text);
                    }
                    lbl_MainAmt.Text = Amt.ToString();

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

        protected void ddl_Version_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Version.SelectedIndex != 0)
                {
                    bind_Dept();
                    gvDept.Visible = true;
                    gvDept2.Visible = true;
                }
                else
                {
                    gvDept.Visible = false;
                    gvDept2.Visible = false; 
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
                create_NewVersion();
                bind_Version();
                Response.Write("<script> alert('New Version Created successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvMain2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Task_ID = drv["Task_ID"].ToString();

                    GridView gvTasks2 = e.Row.FindControl("gvTasks2") as GridView;
                    DataSet ds = dal.Budget_SP(Action: "get_PT_TOM", Project_Id: Session["PROJECTID"].ToString(), Task_ID: Task_ID, Version_ID: ddl_Version.SelectedValue);
                    gvTasks2.DataSource = ds.Tables[0];
                    gvTasks2.DataBind();

                    Label lbl_MainAmt2 = (e.Row.FindControl("lbl_MainAmt2") as Label);
                    int Amt = 0;
                    foreach (GridViewRow row in gvTasks2.Rows)
                    {
                        Label lbl_Amt = (row.FindControl("lbl_Amt") as Label);
                        Amt += Convert.ToInt32(lbl_Amt.Text);
                    }
                    lbl_MainAmt2.Text = Amt.ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvTasks2.Rows.Count > 0)
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


        private void bind_Dept()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Dept_TOM", Project_Id: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDept.DataSource = ds.Tables[0];
                    gvDept.DataBind();

                    gvDept2.DataSource = ds.Tables[0];
                    gvDept2.DataBind();
                }
                else
                {
                    gvDept.DataSource = null;
                    gvDept.DataBind();

                    gvDept2.DataSource = null;
                    gvDept2.DataBind();
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
                    DataSet ds = dal.Budget_SP(Action: "get_TOM_Project_Task", Project_Id: Session["PROJECTID"].ToString(), Version_ID: ddl_Version.SelectedValue, Dept_Id: Dept_ID);
                    gvMain.DataSource = ds.Tables[0];
                    gvMain.DataBind();

                    Label lbl_DeptAmt2 = (e.Row.FindControl("lbl_DeptAmt") as Label);
                    int Amt = 0;
                    foreach (GridViewRow row in gvMain.Rows)
                    {
                        Label lbl_Amt = (row.FindControl("lbl_MainAmt") as Label);
                        Amt += Convert.ToInt32(lbl_Amt.Text);
                    }
                    lbl_DeptAmt2.Text = Amt.ToString();

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

        protected void gvDept2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Dept_ID = drv["Dept_ID"].ToString();

                    GridView gvMain2 = e.Row.FindControl("gvMain2") as GridView;
                    DataSet ds1 = dal.Budget_SP(Action: "get_PT_Project_Task", Project_Id: Session["PROJECTID"].ToString(), Version_ID: ddl_Version.SelectedValue, Dept_Id: Dept_ID);
                    gvMain2.DataSource = ds1.Tables[0];
                    gvMain2.DataBind();

                    Label lbl_DeptAmt2 = (e.Row.FindControl("lbl_DeptAmt2") as Label);
                    int Amt = 0;
                    foreach (GridViewRow row in gvMain2.Rows)
                    {
                        Label lbl_Amt = (row.FindControl("lbl_MainAmt2") as Label);
                        Amt += Convert.ToInt32(lbl_Amt.Text);
                    }
                    lbl_DeptAmt2.Text = Amt.ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvMain2.Rows.Count > 0)
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