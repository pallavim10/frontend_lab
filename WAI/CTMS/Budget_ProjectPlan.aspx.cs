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
    public partial class Budget_ProjectPlan : System.Web.UI.Page
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

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                for (int b = 0; b < gvMainTask.Rows.Count; b++)
                {
                    GridView gvTasks = gvMainTask.Rows[b].FindControl("gvTasks") as GridView;

                    for (int i = 0; i < gvTasks.Rows.Count; i++)
                    {
                        string Task_Id = ((Label)gvTasks.Rows[i].FindControl("lbl_TaskId")).Text;
                        string Sub_Task_Id = ((Label)gvTasks.Rows[i].FindControl("lbl_SubTaskId")).Text;
                        string DtPlan = string.Empty;
                        string MaxID = ((HiddenField)gvTasks.Rows[i].FindControl("hf_MaxID")).Value;

                        if (((TextBox)gvTasks.Rows[i].FindControl("txtDtPlan")).Text != "")
                        {
                            DtPlan = ((TextBox)gvTasks.Rows[i].FindControl("txtDtPlan")).Text;
                        }

                        string DtActual = string.Empty;
                        if (((TextBox)gvTasks.Rows[i].FindControl("txtDtActual")).Text != "")
                        {
                            DtActual = ((TextBox)gvTasks.Rows[i].FindControl("txtDtActual")).Text;
                        }


                        if (DtPlan.ToString() != "")
                        {
                            dal.Budget_SP(Action: "update_Plan_Dates",
                            Project_Id: Session["PROJECTID"].ToString(),
                            Task_ID: Task_Id,
                            Sub_Task_ID: Sub_Task_Id,
                            DtPlan: DtPlan,
                            DtActual: DtActual,
                            ID: MaxID,
                            INVID: "0"
                            );
                        }
                    }
                }

                GETDATA();
                Response.Write("<script> alert('Records Updated successfully.')</script>");
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
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string Task_Id = dr["Task_Id"].ToString();
                    string Sub_Task_Id = dr["Sub_Task_Id"].ToString();
                    string Doc = dr["Doc"].ToString();
                    string DtPlanCount = dr["DtPlanCount"].ToString();
                    string DtActualCount = dr["DtActualCount"].ToString();
                    string DocReq = dr["DocReq"].ToString();
                    string DocCount = dr["DocCount"].ToString();
                    string Difference1 = dr["Difference1"].ToString();
                    string Difference2 = dr["Difference2"].ToString();
                    string Recurring = dr["Recurring"].ToString();
                    string MaxID = dr["MaxID"].ToString();

                    Label txtDifference = (Label)e.Row.FindControl("txtDifference");
                    Label txtDifference2 = (Label)e.Row.FindControl("txtDifference2");
                    Label lblDtPlan = (Label)e.Row.FindControl("lblDtPlan");
                    TextBox txtDtPlan = (TextBox)e.Row.FindControl("txtDtPlan");
                    Label lblDtActual = (Label)e.Row.FindControl("lblDtActual");
                    TextBox txtDtActual = (TextBox)e.Row.FindControl("txtDtActual");
                    LinkButton lbtnDownloadDoc = (LinkButton)e.Row.FindControl("lbtnDownloadDoc");
                    LinkButton lbtnAddComment = (LinkButton)e.Row.FindControl("lbtnAddComment");
                    LinkButton lbtnAddNew = (LinkButton)e.Row.FindControl("lbtnAddNew");

                    if (Difference1.ToString() != "")
                    {
                        if (Convert.ToInt32(Difference1) > 0)
                        {
                            txtDifference.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            txtDifference.ForeColor = System.Drawing.Color.Green;
                        }
                    }

                    if (Difference2.ToString() != "")
                    {
                        if (Convert.ToInt32(Difference2) > 0)
                        {
                            txtDifference2.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            txtDifference2.ForeColor = System.Drawing.Color.Green;
                        }
                    }

                    if (Convert.ToInt32(Doc) > 0)
                    {
                        lbtnDownloadDoc.Visible = true;
                    }
                    else
                    {
                        lbtnDownloadDoc.Visible = false;
                    }

                    LinkButton lbtnUploadDoc = (LinkButton)e.Row.FindControl("lbtnUploadDoc");
                    Label UploadCount = (Label)e.Row.FindControl("UploadCount");

                    if (DocReq == "1")
                    {
                        if (Convert.ToInt32(DocCount) == 0)
                        {
                            lbtnUploadDoc.Visible = false;
                        }
                        else if (Convert.ToInt32(DocCount) > 1)
                        {
                            lbtnUploadDoc.ForeColor = System.Drawing.Color.Blue;
                            lbtnUploadDoc.Visible = true;
                        }
                        else
                        {
                            lbtnUploadDoc.ForeColor = System.Drawing.Color.Green;
                            lbtnUploadDoc.Visible = true;
                        }

                        DataSet ds2 = dal.eTMF_SP(ACTION: "COUNT_OF_DOCS", UploadTaskId: Task_Id, UploadSubTaskId: Sub_Task_Id);

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            if (ds2.Tables[0].Rows[0]["TOTALDOC"].ToString() != "0")
                            {
                                UploadCount.Visible = true;
                                UploadCount.Text = ds2.Tables[0].Rows[0]["UPLOADEDDOC"].ToString() + " (" + ds2.Tables[0].Rows[0]["TOTALDOC"].ToString() + ")";

                                if (ds2.Tables[0].Rows[0]["UPLOADEDDOC"].ToString() == ds2.Tables[0].Rows[0]["TOTALDOC"].ToString())
                                {
                                    UploadCount.ForeColor = System.Drawing.Color.Green;
                                }
                                else
                                {
                                    UploadCount.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                UploadCount.Visible = false;
                            }
                        }
                        else
                        {
                            UploadCount.Visible = false;
                        }
                    }
                    else
                    {
                        lbtnUploadDoc.Visible = false;
                        UploadCount.Visible = false;
                    }

                    if (dr["Multiple"].ToString() == "1")
                    {
                        lblDtPlan.Visible = true;
                        txtDtPlan.Visible = false;
                        lblDtActual.Visible = true;
                        txtDtActual.Visible = false;
                        lbtnAddComment.Enabled = false;

                        if (DtPlanCount != "0")
                        {
                            lblDtPlan.ForeColor = System.Drawing.Color.Red;
                            lblDtPlan.ToolTip = DtPlanCount;
                        }

                        if (DtActualCount != "0")
                        {
                            lblDtActual.ForeColor = System.Drawing.Color.Red;
                            lblDtActual.ToolTip = DtActualCount;
                        }
                    }

                    DataSet ds = dal.Budget_SP(Action: "Audit_TrailExist", Project_Id: Session["PROJECTID"].ToString(), Task_ID: Task_Id, Sub_Task_ID: Sub_Task_Id);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Image imgPlandate = (Image)e.Row.FindControl("AuditTrailPlanDate");
                        Image imgAcualDate = (Image)e.Row.FindControl("AuditTrailAcualDate");

                        if (ds.Tables[0].Rows[0]["PlanDate"].ToString() == "1")
                        {
                            imgPlandate.Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["ActualDate"].ToString() == "1")
                        {
                            imgAcualDate.Visible = true;
                        }
                    }

                    if (MaxID == "" || MaxID == "0")
                    {
                        lbtnUploadDoc.Visible = false;
                        lbtnAddComment.Visible = false;
                        UploadCount.Visible = false;
                    }

                    GridView gvSites = e.Row.FindControl("gvSites") as GridView;
                    DataSet ds1 = dal.Budget_SP(Action: "get_RECURRING_SubTask_Logs", Project_Id: Session["PROJECTID"].ToString(), Task_ID: Task_Id, Sub_Task_ID: Sub_Task_Id, INVID: "0");
                    gvSites.DataSource = ds1.Tables[0];
                    gvSites.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvSites.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                        lbtnAddNew.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                        lbtnAddNew.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
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

        private void bind_Dept()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_PROJECT_Dept_TOM", Project_Id: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepartment.DataSource = ds.Tables[0];
                    ddlDepartment.DataTextField = "Dept_Name";
                    ddlDepartment.DataValueField = "Dept_ID";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    ddlDepartment.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GETDATA()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_PROJECT_Task", Dept_Id: ddlDepartment.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvMainTask.DataSource = ds.Tables[0];
                    gvMainTask.DataBind();
                    btnsubmit.Visible = true;
                }
                else
                {
                    gvMainTask.DataSource = null;
                    gvMainTask.DataBind();
                    btnsubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvMainTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Task_ID = drv["Task_ID"].ToString();

                    GridView gvTasks = e.Row.FindControl("gvTasks") as GridView;
                    DataSet ds = dal.Budget_SP(Action: "get_project_Plans", Project_Id: Session["PROJECTID"].ToString(), Task_ID: Task_ID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvTasks.DataSource = ds.Tables[0];
                        gvTasks.DataBind();
                    }
                    else
                    {
                        gvTasks.DataSource = null;
                        gvTasks.DataBind();
                    }

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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnGetdata_Click(object sender, EventArgs e)
        {
            try
            {
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvSites_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string Task_Id = dr["Task_Id"].ToString();
                    string Sub_Task_Id = dr["Sub_Task_Id"].ToString();

                    string DocReq = dr["DocReq"].ToString();
                    string DocCount = dr["DocCount"].ToString();

                    LinkButton lbtnUploadDoc = (LinkButton)e.Row.FindControl("lbtnUploadDoc");
                    Label UploadCount = (Label)e.Row.FindControl("UploadCount");

                    if (DocReq == "1")
                    {
                        if (Convert.ToInt32(DocCount) == 0)
                        {
                            lbtnUploadDoc.Visible = false;
                        }
                        else if (Convert.ToInt32(DocCount) > 1)
                        {
                            lbtnUploadDoc.ForeColor = System.Drawing.Color.Blue;
                            lbtnUploadDoc.Visible = true;
                        }
                        else
                        {
                            lbtnUploadDoc.ForeColor = System.Drawing.Color.Green;
                            lbtnUploadDoc.Visible = true;
                        }

                        DataSet ds2 = dal.eTMF_SP(ACTION: "COUNT_OF_DOCS", UploadTaskId: Task_Id, UploadSubTaskId: Sub_Task_Id);

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            if (ds2.Tables[0].Rows[0]["TOTALDOC"].ToString() != "0")
                            {
                                UploadCount.Visible = true;
                                UploadCount.Text = ds2.Tables[0].Rows[0]["UPLOADEDDOC"].ToString() + " (" + ds2.Tables[0].Rows[0]["TOTALDOC"].ToString() + ")";

                                if (ds2.Tables[0].Rows[0]["UPLOADEDDOC"].ToString() == ds2.Tables[0].Rows[0]["TOTALDOC"].ToString())
                                {
                                    UploadCount.ForeColor = System.Drawing.Color.Green;
                                }
                                else
                                {
                                    UploadCount.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                UploadCount.Visible = false;
                            }
                        }
                        else
                        {
                            UploadCount.Visible = false;
                        }
                    }
                    else
                    {
                        lbtnUploadDoc.Visible = false;
                        UploadCount.Visible = false;
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