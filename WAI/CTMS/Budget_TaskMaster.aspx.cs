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
    public partial class Budget_TaskMaster : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_ID"] == null)
            {
                Response.Redirect("SessionExpired.aspx", true);
                return;
            }

            try
            {
                if (!Page.IsPostBack)
                {
                    bind_Dept();
                    bind_Grp();
                    btnupdateDept.Visible = false;
                    btnupdateTask.Visible = false;
                    btnupdateSubTask.Visible = false;
                    btnUpdateGrp.Visible = false;
                    lbtnUpdateGrp.Visible = false;
                    lbtnDeleteGrp.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void delete_SubTask(string Id)
        {
            try
            {
                dal.Budget_SP(Action: "delete_SubTask", Sub_Task_ID: Session["SubTaskId"].ToString(), Task_ID: ddlTask.SelectedValue.ToString());
                Session.Remove("SubTaskId");
                bind_TaskGV();
                bind_SubTask();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void edit_SubTask(string Id)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "single_SubTask", Task_ID: ddlTask.SelectedValue.ToString(), Sub_Task_ID: Id);
                txtSubTask.Text = ds.Tables[0].Rows[0]["Sub_Task_Name"].ToString();
                txtSeq.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                btnupdateSubTask.Visible = true;
                btnsubmitSubTask.Visible = false;
                if (ds.Tables[0].Rows[0]["Milestone"].ToString() == "1")
                {
                    chkMilestone.Checked = true;
                }
                else
                {
                    chkMilestone.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["Budget"].ToString() == "1")
                {
                    chkBudget.Checked = true;
                }
                else
                {
                    chkBudget.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["PassThrough"].ToString() == "1")
                {
                    chkPassthrough.Checked = true;
                }
                else
                {
                    chkPassthrough.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["Multiple"].ToString() == "1")
                {
                    chkMultiple.Checked = true;
                }
                else
                {
                    chkMultiple.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["TOM"].ToString() == "1")
                {
                    chkTOM.Checked = true;
                }
                else
                {
                    chkTOM.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["iTOM"].ToString() == "1")
                {
                    chkiTOM.Checked = true;
                }
                else
                {
                    chkiTOM.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["Event"].ToString() == "1")
                {
                    chkEvent.Checked = true;
                }
                else
                {
                    chkEvent.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["Download"].ToString() == "1")
                {
                    chkDownloadable.Checked = true;
                }
                else
                {
                    chkDownloadable.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["Document"].ToString() == "1")
                {
                    chkDocument.Checked = true;
                }
                else
                {
                    chkDocument.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["Recurring"].ToString() == "1")
                {
                    chkrecurring.Checked = true;
                }
                else
                {
                    chkrecurring.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["Timeline"].ToString() == "1")
                {
                    chkTimeline.Checked = true;
                }
                else
                {
                    chkTimeline.Checked = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Insert_SubTask()
        {
            try
            {
                string Dept, Task, SubTask;
                Dept = ddlDept.SelectedValue;
                Task = ddlTask.SelectedValue;
                SubTask = txtSubTask.Text;
                string Milestone = "";
                if (chkMilestone.Checked == true)
                {
                    Milestone = "1";
                }

                string Budget = "";
                if (chkBudget.Checked == true)
                {
                    Budget = "1";
                }

                string PassThrough = "";
                if (chkPassthrough.Checked == true)
                {
                    PassThrough = "1";
                }

                string Multiple = "";
                if (chkMultiple.Checked == true)
                {
                    Multiple = "1";
                }

                string Document = "";
                if (chkDocument.Checked == true)
                {
                    Document = "1";
                }

                string TOM = "";
                if (chkTOM.Checked == true)
                {
                    TOM = "1";
                }

                string iTOM = "";
                if (chkiTOM.Checked == true)
                {
                    iTOM = "1";
                }

                string Event = "";
                if (chkEvent.Checked == true)
                {
                    Event = "1";
                }

                string Download = "";
                if (chkDownloadable.Checked == true)
                {
                    Download = "1";
                }

                string Recurring = "";
                if (chkrecurring.Checked == true)
                {
                    Recurring = "1";
                }

                string Timeline = "";
                if (chkTimeline.Checked == true)
                {
                    Timeline = "1";
                }

                dal.Budget_SP(Action: "insert_SubTask", Task_ID: ddlTask.SelectedValue.ToString(), Sub_Task_Name: txtSubTask.Text, Milestone: Milestone, Budget: Budget, PassThrough: PassThrough, SEQNO: txtSeq.Text, Multiple: Multiple, Document: Document, TOM: TOM, iTOM: iTOM, Event: Event, Download: Download, Recurring: Recurring, Timeline: Timeline);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        public void update_SubTask()
        {
            try
            {
                string Section, SubSection, Checklist;
                Section = ddlDept.SelectedValue;
                SubSection = ddlTask.SelectedValue;
                Checklist = txtSubTask.Text;
                string Milestone = "";
                if (chkMilestone.Checked == true)
                {
                    Milestone = "1";
                }

                string Budget = "";
                if (chkBudget.Checked == true)
                {
                    Budget = "1";
                }

                string PassThrough = "";
                if (chkPassthrough.Checked == true)
                {
                    PassThrough = "1";
                }

                string Multiple = "";
                if (chkMultiple.Checked == true)
                {
                    Multiple = "1";
                }

                string Document = "";
                if (chkDocument.Checked == true)
                {
                    Document = "1";
                }

                string TOM = "";
                if (chkTOM.Checked == true)
                {
                    TOM = "1";
                }

                string iTOM = "";
                if (chkiTOM.Checked == true)
                {
                    iTOM = "1";
                }

                string Event = "";
                if (chkEvent.Checked == true)
                {
                    Event = "1";
                }

                string Download = "";
                if (chkDownloadable.Checked == true)
                {
                    Download = "1";
                }

                string Recurring = "";
                if (chkrecurring.Checked == true)
                {
                    Recurring = "1";
                }

                string Timeline = "";
                if (chkTimeline.Checked == true)
                {
                    Timeline = "1";
                }

                dal.Budget_SP(Action: "update_SubTask", Task_ID: ddlTask.SelectedValue.ToString(), Sub_Task_ID: Session["SubTaskId"].ToString(), Sub_Task_Name: txtSubTask.Text, Milestone: Milestone, Budget: Budget, PassThrough: PassThrough, SEQNO: txtSeq.Text, Multiple: Multiple, Document: Document, TOM: TOM, iTOM: iTOM, Event: Event, Download: Download, Recurring: Recurring, Timeline: Timeline);
                bind_TaskGV();
                chkMilestone.Checked = false;
                chkBudget.Checked = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        public void Edit_Dept(string id)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "single_Dept", Dept_Id: id);
                txtDept.Text = ds.Tables[0].Rows[0]["Dept_Name"].ToString();

                if (ds.Tables[0].Rows[0]["Site"].ToString() == "1")
                {
                    chkSite.Checked = true;
                }
                else
                {
                    chkSite.Checked = false;
                }

                btnupdateDept.Visible = true;
                btnsubmitDept.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Edit_Task(string id)
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "single_Task", Task_ID: id);
                txtTask.Text = ds.Tables[0].Rows[0]["Task_Name"].ToString();
                btnupdateTask.Visible = true;
                btnsubmitTask.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_Dept()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Dept");
                ddlDept.DataSource = ds.Tables[0];
                ddlDept.DataValueField = "Dept_Id";
                ddlDept.DataTextField = "Dept_Name";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, new ListItem("--Select--", "0"));

                gvDept.DataSource = ds.Tables[0];
                gvDept.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_DeptGV()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Dept");
                ddlDept.DataSource = ds.Tables[0];
                gvDept.DataSource = ds.Tables[0];
                gvDept.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_Task()
        {
            try
            {
                DataSet ds1 = dal.Budget_SP(Action: "get_Task", Dept_Id: ddlDept.SelectedValue.ToString());
                ddlTask.DataSource = ds1.Tables[0];
                ddlTask.DataValueField = "Task_ID";
                ddlTask.DataTextField = "Task_Name";
                ddlTask.DataBind();
                ddlTask.Items.Insert(0, new ListItem("--Select--", "0"));

                gvTask.DataSource = ds1.Tables[0];
                gvTask.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_TaskGV()
        {
            try
            {
                DataSet ds1 = dal.Budget_SP(Action: "get_Task", Dept_Id: ddlDept.SelectedValue.ToString());

                gvTask.DataSource = ds1.Tables[0];
                gvTask.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void delete_Dept(string id)
        {
            try
            {
                dal.Budget_SP(Action: "delete_Dept", Dept_Id: id);
                bind_Dept();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void delete_Task(string id)
        {
            try
            {
                dal.Budget_SP(Action: "delete_Task", Task_ID: id);
                bind_DeptGV();
                bind_Task();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void add_Dept()
        {
            try
            {
                string Site = "";
                if (chkSite.Checked == true)
                {
                    Site = "1";
                }
                dal.Budget_SP(Action: "insert_Dept", Dept_Name: txtDept.Text, Site: Site);
                bind_Dept();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void add_Task()
        {
            try
            {
                dal.Budget_SP(Action: "insert_Task", Dept_Id: ddlDept.SelectedValue.ToString(), Task_Name: txtTask.Text);
                bind_Task();
                bind_DeptGV();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void update_Dept()
        {
            try
            {
                string Site = "";
                if (chkSite.Checked == true)
                {
                    Site = "1";
                }
                dal.Budget_SP(Action: "update_Dept", Dept_Name: txtDept.Text, Dept_Id: Session["DeptId"].ToString(), Site: Site);
                bind_Dept();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void update_Task()
        {
            try
            {
                dal.Budget_SP(Action: "update_Task", Dept_Id: ddlDept.SelectedValue.ToString(), Task_Name: txtTask.Text, Task_ID: Session["TaskId"].ToString());
                bind_Task();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_SubTask()
        {
            try
            {
                string SectionID = null;
                string SubSectionID = null;
                //string ProjectID = "356";
                if (ddlDept.SelectedValue != "0")
                {
                    SectionID = ddlDept.SelectedValue;
                }
                if (ddlTask.SelectedValue != "0")
                {
                    SubSectionID = ddlTask.SelectedValue;
                }

                DataSet ds = dal.Budget_SP(Action: "get_SubTask", Task_ID: ddlTask.SelectedValue.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvSubTask.DataSource = ds.Tables[0];
                    gvSubTask.DataBind();
                }
                else
                {
                    gvSubTask.DataSource = null;
                    gvSubTask.DataBind();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitDept_Click(object sender, EventArgs e)
        {
            try
            {
                add_Dept();
                txtDept.Text = "";
                chkSite.Checked = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateDept_Click(object sender, EventArgs e)
        {
            try
            {
                update_Dept();
                txtDept.Text = "";
                chkSite.Checked = false;
                btnupdateDept.Visible = false;
                btnsubmitDept.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelDept_Click(object sender, EventArgs e)
        {
            try
            {
                btnsubmitDept.Visible = true;
                btnupdateDept.Visible = false;
                txtDept.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvDept_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["DeptID"] = id;
                if (e.CommandName == "Edit1")
                {
                    Edit_Dept(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Dept(id);
                    bind_Dept();
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
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteDept = (e.Row.FindControl("lbtndeleteDept") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteDept.Visible = false;
                    }
                    else
                    {
                        lbtndeleteDept.Visible = true;
                    }

                    Label lblSiteCheck = (e.Row.FindControl("lblSiteCheck") as Label);
                    Label lblSiteUnCheck = (e.Row.FindControl("lblSiteUnCheck") as Label);
                    string Site = drv["Site"].ToString();
                    if (Site == "1")
                    {
                        lblSiteCheck.Visible = true;
                        lblSiteUnCheck.Visible = false;
                    }
                    else
                    {
                        lblSiteCheck.Visible = false;
                        lblSiteUnCheck.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Task();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitTask_Click(object sender, EventArgs e)
        {
            try
            {
                add_Task();
                txtTask.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateTask_Click(object sender, EventArgs e)
        {
            try
            {
                update_Task();
                btnupdateTask.Visible = false;
                btnsubmitTask.Visible = true;
                txtTask.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelTask_Click(object sender, EventArgs e)
        {
            try
            {
                btnupdateTask.Visible = false;
                btnsubmitTask.Visible = true;
                txtTask.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvTask_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["TaskID"] = id;
                if (e.CommandName == "Edit1")
                {
                    Edit_Task(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Task(id);
                    bind_Task();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteTask = (e.Row.FindControl("lbtndeleteTask") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteTask.Visible = false;
                    }
                    else
                    {
                        lbtndeleteTask.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_SubTask();
                bind_grpTasks();
                bind_nongrpTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitSubTask_Click(object sender, EventArgs e)
        {
            try
            {
                Insert_SubTask();
                txtSubTask.Text = "";
                txtSeq.Text = "";
                bind_TaskGV();
                bind_SubTask();
                chkMultiple.Checked = false;
                chkMilestone.Checked = false;
                chkBudget.Checked = false;
                chkPassthrough.Checked = false;
                chkDocument.Checked = false;
                chkTOM.Checked = false;
                chkiTOM.Checked = false;
                chkEvent.Checked = false;
                chkDownloadable.Checked = false;
                chkrecurring.Checked = false;
                chkTimeline.Checked = false;
                bind_grpTasks();
                bind_nongrpTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateSubTask_Click(object sender, EventArgs e)
        {
            try
            {
                update_SubTask();
                txtSubTask.Text = "";
                txtSeq.Text = "";
                chkMultiple.Checked = false;
                chkPassthrough.Checked = false;
                btnupdateSubTask.Visible = false;
                btnsubmitSubTask.Visible = true;
                chkDocument.Checked = false;
                chkTOM.Checked = false;
                chkiTOM.Checked = false;
                chkEvent.Checked = false;
                chkDownloadable.Checked = false;
                chkrecurring.Checked = false;
                chkTimeline.Checked = false;
                bind_SubTask();
                bind_grpTasks();
                bind_nongrpTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelSubTask_Click(object sender, EventArgs e)
        {
            try
            {
                txtSubTask.Text = "";
                txtSeq.Text = "";
                btnsubmitSubTask.Visible = true;
                btnupdateSubTask.Visible = false;
                chkMultiple.Checked = false;
                chkMilestone.Checked = false;
                chkBudget.Checked = false;
                chkPassthrough.Checked = false;
                chkDocument.Checked = false;
                chkTOM.Checked = false;
                chkiTOM.Checked = false;
                chkEvent.Checked = false;
                chkDownloadable.Checked = false;
                chkrecurring.Checked = false;
                chkTimeline.Checked = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSubTask_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["SubTaskId"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_SubTask(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_SubTask(id);
                    bind_grpTasks();
                    bind_nongrpTasks();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSubTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    LinkButton lbtndeleteCheckList = (e.Row.FindControl("lbtndeleteCheckList") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtndeleteCheckList.Visible = false;
                    }
                    else
                    {
                        lbtndeleteCheckList.Visible = true;
                    }

                    Label lblMilestoneCheck = (e.Row.FindControl("lblMilestoneCheck") as Label);
                    Label lblMilestoneUnCheck = (e.Row.FindControl("lblMilestoneUnCheck") as Label);
                    string milesStone = drv["Milestone"].ToString();
                    if (milesStone == "1")
                    {
                        lblMilestoneCheck.Visible = true;
                        lblMilestoneUnCheck.Visible = false;
                    }
                    else
                    {
                        lblMilestoneCheck.Visible = false;
                        lblMilestoneUnCheck.Visible = true;
                    }

                    Label lblBudgetCheck = (e.Row.FindControl("lblBudgetCheck") as Label);
                    Label lblBudgetUnCheck = (e.Row.FindControl("lblBudgetUnCheck") as Label);
                    string budget = drv["Budget"].ToString();
                    if (budget == "1")
                    {
                        lblBudgetCheck.Visible = true;
                        lblBudgetUnCheck.Visible = false;
                    }
                    else
                    {
                        lblBudgetCheck.Visible = false;
                        lblBudgetUnCheck.Visible = true;
                    }

                    Label lblPassThroughCheck = (e.Row.FindControl("lblPassThroughCheck") as Label);
                    Label lblPassThroughUnCheck = (e.Row.FindControl("lblPassThroughUnCheck") as Label);
                    string PassThrough = drv["PassThrough"].ToString();
                    if (PassThrough == "1")
                    {
                        lblPassThroughCheck.Visible = true;
                        lblPassThroughUnCheck.Visible = false;
                    }
                    else
                    {
                        lblPassThroughCheck.Visible = false;
                        lblPassThroughUnCheck.Visible = true;
                    }

                    Label lblMultipleCheck = (e.Row.FindControl("lblMultipleCheck") as Label);
                    Label lblMultipleUnCheck = (e.Row.FindControl("lblMultipleUnCheck") as Label);
                    string Multiple = drv["Multiple"].ToString();
                    if (Multiple == "1")
                    {
                        lblMultipleCheck.Visible = true;
                        lblMultipleUnCheck.Visible = false;
                    }
                    else
                    {
                        lblMultipleCheck.Visible = false;
                        lblMultipleUnCheck.Visible = true;
                    }

                    Label lblDocumentCheck = (e.Row.FindControl("lblDocumentCheck") as Label);
                    Label lblDocumentUnCheck = (e.Row.FindControl("lblDocumentUnCheck") as Label);
                    string Document = drv["Document"].ToString();
                    if (Document == "1")
                    {
                        lblDocumentCheck.Visible = true;
                        lblDocumentUnCheck.Visible = false;
                    }
                    else
                    {
                        lblDocumentCheck.Visible = false;
                        lblDocumentUnCheck.Visible = true;
                    }

                    Label lblTOMCheck = (e.Row.FindControl("lblTOMCheck") as Label);
                    Label lblTOMUnCheck = (e.Row.FindControl("lblTOMUnCheck") as Label);
                    string TOM = drv["TOM"].ToString();
                    if (TOM == "1")
                    {
                        lblTOMCheck.Visible = true;
                        lblTOMUnCheck.Visible = false;
                    }
                    else
                    {
                        lblTOMCheck.Visible = false;
                        lblTOMUnCheck.Visible = true;
                    }

                    Label lbliTOMCheck = (e.Row.FindControl("lbliTOMCheck") as Label);
                    Label lbliTOMUnCheck = (e.Row.FindControl("lbliTOMUnCheck") as Label);
                    string iTOM = drv["iTOM"].ToString();
                    if (iTOM == "1")
                    {
                        lbliTOMCheck.Visible = true;
                        lbliTOMUnCheck.Visible = false;
                    }
                    else
                    {
                        lbliTOMCheck.Visible = false;
                        lbliTOMUnCheck.Visible = true;
                    }

                    Label lblEventCheck = (e.Row.FindControl("lblEventCheck") as Label);
                    Label lblEventUnCheck = (e.Row.FindControl("lblEventUnCheck") as Label);
                    string Event = drv["Event"].ToString();
                    if (Event == "1")
                    {
                        lblEventCheck.Visible = true;
                        lblEventUnCheck.Visible = false;
                    }
                    else
                    {
                        lblEventCheck.Visible = false;
                        lblEventUnCheck.Visible = true;
                    }

                    Label lblDownloadCheck = (e.Row.FindControl("lblDownloadCheck") as Label);
                    Label lblDownloadUnCheck = (e.Row.FindControl("lblDownloadUnCheck") as Label);
                    string Download = drv["Download"].ToString();
                    if (Download == "1")
                    {
                        lblDownloadCheck.Visible = true;
                        lblDownloadUnCheck.Visible = false;
                    }
                    else
                    {
                        lblDownloadCheck.Visible = false;
                        lblDownloadUnCheck.Visible = true;
                    }

                    Label lblRecurringCheck = (e.Row.FindControl("lblRecurringCheck") as Label);
                    Label lblRecurringUncheck = (e.Row.FindControl("lblRecurringUncheck") as Label);
                    string Recurring = drv["Recurring"].ToString();
                    if (Recurring == "1")
                    {
                        lblRecurringCheck.Visible = true;
                        lblRecurringUncheck.Visible = false;
                    }
                    else
                    {
                        lblRecurringCheck.Visible = false;
                        lblRecurringUncheck.Visible = true;
                    }

                    Label lblTimelineCheck = (e.Row.FindControl("lblTimelineCheck") as Label);
                    Label lblTimelineUnCheck = (e.Row.FindControl("lblTimelineUnCheck") as Label);
                    string Timeline = drv["Timeline"].ToString();
                    if (Timeline == "1")
                    {
                        lblTimelineCheck.Visible = true;
                        lblTimelineUnCheck.Visible = false;
                    }
                    else
                    {
                        lblTimelineCheck.Visible = false;
                        lblTimelineUnCheck.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnAddGrp_Click(object sender, EventArgs e)
        {
            try
            {
                create_Grp();
                lbtnUpdateGrp.Visible = false;
                lbtnDeleteGrp.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_grpTasks();
                bind_nongrpTasks();
                if (ddlGroup.SelectedValue == "0")
                {
                    lbtnUpdateGrp.Visible = false;
                    lbtnDeleteGrp.Visible = false;
                }
                else
                {
                    lbtnUpdateGrp.Visible = true;

                    if (gvAddedTasks.Rows.Count > 0)
                    {
                        lbtnDeleteGrp.Visible = false;
                    }
                    else
                    {
                        lbtnDeleteGrp.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddToGrp_Click(object sender, EventArgs e)
        {
            try
            {
                add_to_Grp();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemoveFromGrp_Click(object sender, EventArgs e)
        {
            try
            {
                remove_from_Grp();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUpdateGrp_Click(object sender, EventArgs e)
        {
            try
            {
                edit_Grp();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnDeleteGrp_Click(object sender, EventArgs e)
        {
            try
            {
                delete_Grp();
                lbtnUpdateGrp.Visible = false;
                lbtnDeleteGrp.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateGrp_Click(object sender, EventArgs e)
        {
            try
            {
                update_Grp();
                lbtnUpdateGrp.Visible = false;
                lbtnDeleteGrp.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void create_Grp()
        {
            try
            {
                dal.Budget_SP(Action: "create_Group", GroupName: txtGrp.Text, Project_Id: Session["PROJECTID"].ToString());
                txtGrp.Text = "";
                bind_Grp();
                bind_grpTasks();
                bind_nongrpTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Grp()
        {
            try
            {
                DataSet ds1 = dal.Budget_SP(Action: "get_Group", Project_Id: Session["PROJECTID"].ToString());
                ddlGroup.DataSource = ds1.Tables[0];
                ddlGroup.DataValueField = "ID";
                ddlGroup.DataTextField = "Group_Name";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Grp()
        {
            try
            {
                dal.Budget_SP(Action: "delete_Group", Project_Id: Session["PROJECTID"].ToString(), ID: ddlGroup.SelectedValue);
                bind_Grp();
                bind_grpTasks();
                bind_nongrpTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Grp()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "edit_Group", Project_Id: Session["PROJECTID"].ToString(), ID: ddlGroup.SelectedValue);
                txtGrp.Text = ds.Tables[0].Rows[0]["Group_Name"].ToString();
                btnUpdateGrp.Visible = true;
                btnAddGrp.Visible = false;
                ddlGroup.Enabled = false;
                bind_grpTasks();
                bind_nongrpTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //private void update_Grp()
        //{
        //    try
        //    {
        //        dal.Budget_SP(Action: "update_Group", GroupName: txtGrp.Text, ID: ddlGroup.SelectedValue);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        private void update_Grp()
        {
            try
            {
                dal.Budget_SP(Action: "update_Group", GroupName: txtGrp.Text, ID: ddlGroup.SelectedValue);
                txtGrp.Text = "";
                btnUpdateGrp.Visible = false;
                btnAddGrp.Visible = true;
                ddlGroup.Enabled = true;
                bind_Grp();
                bind_grpTasks();
                bind_nongrpTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void add_to_Grp()
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvNewSubTask.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvNewSubTask.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblSubTaskID = (Label)gvNewSubTask.Rows[i].FindControl("lblSubTaskID");
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Budget_SP(Action: "add_to_Grp", Project_Id: ProjectID, Task_ID: ddlTask.SelectedValue, Sub_Task_ID: lblSubTaskID.Text, ID: ddlGroup.SelectedValue);
                    }
                }
                bind_grpTasks();
                bind_nongrpTasks();
                //Response.Write("<script> alert('Record Added successfully.');window.location='Checklist_Master.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void remove_from_Grp()
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvAddedTasks.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAddedTasks.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label ID = (Label)gvAddedTasks.Rows[i].FindControl("lblSubTaskID");
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Budget_SP(Action: "remove_from_Grp", ID: ID.Text);
                    }
                }
                bind_grpTasks();
                bind_nongrpTasks();
                //Response.Write("<script> alert('Record Added successfully.');window.location='Checklist_Master.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void bind_grpTasks()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "grpTasks", Project_Id: Session["PROJECTID"].ToString(), ID: ddlGroup.SelectedValue);
                gvAddedTasks.DataSource = ds.Tables[0];
                gvAddedTasks.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_nongrpTasks()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "nongrpTasks", ID: ddlGroup.SelectedValue, Task_ID: ddlTask.SelectedValue, Project_Id: Session["PROJECTID"].ToString());
                gvNewSubTask.DataSource = ds.Tables[0];
                gvNewSubTask.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


    }
}