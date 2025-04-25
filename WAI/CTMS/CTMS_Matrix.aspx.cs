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
    public partial class CTMS_Matrix : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    bind_Grp();
                    bind_Task();
                    btnUpdateGrp.Visible = false;
                    lbtnUpdateGrp.Visible = false;
                    lbtnDeleteGrp.Visible = false;
                    btnUpdateMat.Visible = false;
                    btnUpdateComp.Visible = false;
                }
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
                DataSet ds1 = dal.CTMS_Matrix_SP(Action: "get_Task", ID: Session["PROJECTID"].ToString());
                ddlTask.DataSource = ds1.Tables[0];
                ddlTask.DataValueField = "Task_ID";
                ddlTask.DataTextField = "Task_Name";
                ddlTask.DataBind();
                ddlTask.Items.Insert(0, new ListItem("--Select--", "0"));
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

                    showHide_Icons();
                }

                get_Tasks();
                get_Matrix();
                get_Compare();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void showHide_Icons()
        {
            if (gvAddedTasks.Rows.Count > 0)
            {
                lbtnDeleteGrp.Visible = false;
            }
            else
            {
                lbtnDeleteGrp.Visible = true;
            }
        }

        protected void lbtnAddToGrp_Click(object sender, EventArgs e)
        {
            try
            {
                add_to_Grp();
                get_Tasks();
                showHide_Icons();
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
                get_Tasks();
                showHide_Icons();
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
                dal.Budget_SP(
                Action: "create_Group",
                GroupName: txtGrp.Text,
                Project_Id: Session["PROJECTID"].ToString(),
                MASTERDB: Session["InitialCatalog"].ToString());
                txtGrp.Text = "";
                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("CTMS");
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
                dal.Budget_SP(Action: "delete_Group", Project_Id: Session["PROJECTID"].ToString(), ID: ddlGroup.SelectedValue, MASTERDB: Session["InitialCatalog"].ToString());
                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("CTMS");
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

        private void update_Grp()
        {
            try
            {
                dal.Budget_SP(Action: "update_Group", GroupName: txtGrp.Text, ID: ddlGroup.SelectedValue, MASTERDB: Session["InitialCatalog"].ToString(), Project_Id: Session["PROJECTID"].ToString());
                txtGrp.Text = "";
                btnUpdateGrp.Visible = false;
                btnAddGrp.Visible = true;
                ddlGroup.Enabled = true;
                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("CTMS");
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

        protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_grpTasks();
                bind_nongrpTasks();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Tasks()
        {
            try
            {
                DataSet ds1 = dal.CTMS_Matrix_SP(Action: "get_Group_Tasks", Group_ID: ddlGroup.SelectedValue);

                ddl_Mat_PTask.DataSource = ds1.Tables[0];
                ddl_Mat_PTask.DataValueField = "Task_ID";
                ddl_Mat_PTask.DataTextField = "Task_Name";
                ddl_Mat_PTask.DataBind();
                ddl_Mat_PTask.Items.Insert(0, new ListItem("--Select--", "0"));

                ddl_Comp_CTask.DataSource = ds1.Tables[0];
                ddl_Comp_CTask.DataValueField = "Task_ID";
                ddl_Comp_CTask.DataTextField = "Task_Name";
                ddl_Comp_CTask.DataBind();
                ddl_Comp_CTask.Items.Insert(0, new ListItem("--Select--", "0"));

                ddl_Comp_PTask.DataSource = ds1.Tables[0];
                ddl_Comp_PTask.DataValueField = "Task_ID";
                ddl_Comp_PTask.DataTextField = "Task_Name";
                ddl_Comp_PTask.DataBind();
                ddl_Comp_PTask.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Mat_PSubTask()
        {
            try
            {
                DataSet ds1 = dal.CTMS_Matrix_SP(Action: "get_Group_SubTasks", P_Task_ID: ddl_Mat_PTask.SelectedValue, Group_ID: ddlGroup.SelectedValue);
                ddl_Mat_PSubTask.DataSource = ds1.Tables[0];
                ddl_Mat_PSubTask.DataValueField = "Sub_Task_ID";
                ddl_Mat_PSubTask.DataTextField = "Sub_Task_Name";
                ddl_Mat_PSubTask.DataBind();
                ddl_Mat_PSubTask.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Comp_PSubTask()
        {
            try
            {
                DataSet ds1 = dal.CTMS_Matrix_SP(Action: "get_Group_SubTasks", P_Task_ID: ddl_Comp_PTask.SelectedValue, Group_ID: ddlGroup.SelectedValue);
                ddl_Comp_PSubTask.DataSource = ds1.Tables[0];
                ddl_Comp_PSubTask.DataValueField = "Sub_Task_ID";
                ddl_Comp_PSubTask.DataTextField = "Sub_Task_Name";
                ddl_Comp_PSubTask.DataBind();
                ddl_Comp_PSubTask.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Comp_CSubTask()
        {
            try
            {
                DataSet ds1 = dal.CTMS_Matrix_SP(Action: "get_Group_SubTasks", P_Task_ID: ddl_Comp_CTask.SelectedValue, Group_ID: ddlGroup.SelectedValue);
                ddl_Comp_CSubTask.DataSource = ds1.Tables[0];
                ddl_Comp_CSubTask.DataValueField = "Sub_Task_ID";
                ddl_Comp_CSubTask.DataTextField = "Sub_Task_Name";
                ddl_Comp_CSubTask.DataBind();
                ddl_Comp_CSubTask.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Matrix()
        {
            try
            {
                dal.CTMS_Matrix_SP(Action: "insert_Matrix", Group_ID: ddlGroup.SelectedValue, P_Task_ID: ddl_Mat_PTask.SelectedValue, P_SubTask_ID: ddl_Mat_PSubTask.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Matrix()
        {
            try
            {
                dal.CTMS_Matrix_SP(Action: "update_Matrix", ID: Session["MATID"].ToString(), P_Task_ID: ddl_Mat_PTask.SelectedValue, P_SubTask_ID: ddl_Mat_PSubTask.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Matrix(string ID)
        {
            try
            {
                dal.CTMS_Matrix_SP(Action: "delete_Matrix", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Matrix()
        {
            try
            {
                DataSet ds = dal.CTMS_Matrix_SP(Action: "get_Matrix", Group_ID: ddlGroup.SelectedValue);
                gvMat.DataSource = ds.Tables[0];
                gvMat.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Matrix(string id)
        {
            try
            {
                DataSet ds = dal.CTMS_Matrix_SP(Action: "select_Matrix", ID: id);
                ddl_Mat_PTask.SelectedValue = ds.Tables[0].Rows[0]["Task_ID"].ToString();
                get_Mat_PSubTask();
                ddl_Mat_PSubTask.SelectedValue = ds.Tables[0].Rows[0]["SubTask_ID"].ToString();
                btnUpdateMat.Visible = true;
                btnSubmitMat.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Compare()
        {
            try
            {
                dal.CTMS_Matrix_SP(Action: "insert_Compare", Group_ID: ddlGroup.SelectedValue, P_Task_ID: ddl_Comp_PTask.SelectedValue, P_SubTask_ID: ddl_Comp_PSubTask.SelectedValue, C_Task_ID: ddl_Comp_CTask.SelectedValue, C_SubTask_ID: ddl_Comp_CSubTask.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Compare()
        {
            try
            {
                dal.CTMS_Matrix_SP(Action: "update_Compare", ID: Session["COMPID"].ToString(), P_Task_ID: ddl_Comp_PTask.SelectedValue, P_SubTask_ID: ddl_Comp_PSubTask.SelectedValue, C_Task_ID: ddl_Comp_CTask.SelectedValue, C_SubTask_ID: ddl_Comp_CSubTask.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Compare(string id)
        {
            try
            {
                dal.CTMS_Matrix_SP(Action: "delete_Compare", ID: id);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Compare()
        {
            try
            {
                DataSet ds = dal.CTMS_Matrix_SP(Action: "get_Compare", Group_ID: ddlGroup.SelectedValue);
                gvComp.DataSource = ds.Tables[0];
                gvComp.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Compare(string id)
        {
            try
            {
                DataSet ds = dal.CTMS_Matrix_SP(Action: "select_Compare", ID: id);
                ddl_Comp_PTask.SelectedValue = ds.Tables[0].Rows[0]["P_Task_ID"].ToString();
                get_Comp_PSubTask();
                ddl_Comp_PSubTask.SelectedValue = ds.Tables[0].Rows[0]["P_SubTask_ID"].ToString();
                ddl_Comp_CTask.SelectedValue = ds.Tables[0].Rows[0]["C_Task_ID"].ToString();
                get_Comp_CSubTask();
                ddl_Comp_CSubTask.SelectedValue = ds.Tables[0].Rows[0]["C_SubTask_ID"].ToString();
                btnSubmitComp.Visible = false;
                btnUpdateComp.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddl_Mat_PTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                get_Mat_PSubTask();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitMat_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Matrix();
                get_Matrix();
                ddl_Mat_PTask.SelectedIndex = 0;
                get_Mat_PSubTask();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateMat_Click(object sender, EventArgs e)
        {
            try
            {
                update_Matrix();
                ddl_Mat_PTask.SelectedIndex = 0;
                get_Mat_PSubTask();
                get_Matrix();
                btnUpdateMat.Visible = false;
                btnSubmitMat.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnancelMat_Click(object sender, EventArgs e)
        {
            try
            {
                ddl_Mat_PTask.SelectedIndex = 0;
                get_Mat_PSubTask();
                btnUpdateMat.Visible = false;
                btnSubmitMat.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvMat_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMat_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["MATID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_Matrix(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Matrix(id);
                    get_Matrix();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddl_Comp_PTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                get_Comp_PSubTask();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddl_Comp_CTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                get_Comp_CSubTask();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitComp_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Compare();
                get_Compare();
                ddl_Comp_PTask.SelectedIndex = 0;
                get_Comp_PSubTask();
                ddl_Comp_CTask.SelectedIndex = 0;
                get_Comp_CSubTask();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateComp_Click(object sender, EventArgs e)
        {
            try
            {
                update_Compare();
                get_Compare();
                ddl_Comp_PTask.SelectedIndex = 0;
                get_Comp_PSubTask();
                ddl_Comp_CTask.SelectedIndex = 0;
                get_Comp_CSubTask();
                btnUpdateComp.Visible = false;
                btnSubmitComp.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelComp_Click(object sender, EventArgs e)
        {
            try
            {

                ddl_Comp_PTask.SelectedIndex = 0;
                get_Comp_PSubTask();
                ddl_Comp_CTask.SelectedIndex = 0;
                get_Comp_CSubTask();
                btnUpdateComp.Visible = false;
                btnSubmitComp.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvComp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvComp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["COMPID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_Compare(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Compare(id);
                    get_Compare();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}