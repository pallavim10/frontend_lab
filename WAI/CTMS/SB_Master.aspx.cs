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
    public partial class SB_Master : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Dept();
                    get_Visit();
                    //btnupdateAct.Visible = false;
                    btnUpdateVisit.Visible = false;
                    lbtnDeleteVisit.Visible = false;
                    lbtnUpdateVisit.Visible = false;
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
                DataSet ds = dal.SiteBudget_SP(Action: "get_Dept");
                ddlDept.DataSource = ds.Tables[0];
                ddlDept.DataValueField = "Dept_Id";
                ddlDept.DataTextField = "Dept_Name";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, new ListItem("--Select--", "0"));
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
                DataSet ds1 = dal.SiteBudget_SP(Action: "get_Task", Dept_Id: ddlDept.SelectedValue.ToString());
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

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Task();
                bind_NewActs();
                bind_AddedActs();
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
                bind_NewActs();
                bind_AddedActs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_NewActs()
        {
            try
            {
                DataSet ds = dal.SiteBudget_SP(Action: "get_NewActs", Id: ddlVisit.SelectedValue, Task_Id: ddlTask.SelectedValue, Dept_Id: ddlDept.SelectedValue, Project_Id: Session["PROJECTID"].ToString());
                gvNewActs.DataSource = ds.Tables[0];
                gvNewActs.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_AddedActs()
        {
            try
            {
                DataSet ds = dal.SiteBudget_SP(Action: "get_AddedActs", Visit_Id: ddlVisit.SelectedValue);
                gvAddedActs.DataSource = ds.Tables[0];
                gvAddedActs.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Visit()
        {
            try
            {
                dal.SiteBudget_SP(Action: "insert_Visit", Visit: txtGrp.Text, Project_Id: Session["PROJECTID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Visit()
        {
            try
            {
                dal.SiteBudget_SP(Action: "update_Visit", Visit: txtGrp.Text, Id: ddlVisit.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Visit()
        {
            try
            {
                Session["VISITID"] = ID;
                DataSet ds = dal.SiteBudget_SP(Action: "single_Visit", Id: ddlVisit.SelectedValue);
                btnAddVisit.Visible = false;
                btnUpdateVisit.Visible = true;
                txtGrp.Text = ds.Tables[0].Rows[0]["Visit"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Visit()
        {
            try
            {
                dal.SiteBudget_SP(Action: "delete_Visit", Id: ddlVisit.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void addToVisit()
        {
            try
            {
                for (int i = 0; i < gvNewActs.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvNewActs.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblSubTaskID = (Label)gvNewActs.Rows[i].FindControl("lblActID");
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.SiteBudget_SP(Action: "addToVisit", Project_Id: ProjectID, Task_Id: ddlTask.SelectedValue, Sub_Task_ID: lblSubTaskID.Text, Visit_Id: ddlVisit.SelectedValue);
                    }
                }
                bind_NewActs();
                bind_AddedActs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void removeFromVisit()
        {
            try
            {
                for (int i = 0; i < gvAddedActs.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAddedActs.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblActID = (Label)gvAddedActs.Rows[i].FindControl("lblActID");
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.SiteBudget_SP(Action: "removeFromVisit", Id: lblActID.Text);
                    }
                }
                bind_NewActs();
                bind_AddedActs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnAddVisit_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Visit();
                txtGrp.Text = "";
                get_Visit();
                bind_AddedActs();
                bind_NewActs();
                visitICONS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateVisit_Click(object sender, EventArgs e)
        {
            try
            {
                update_Visit();
                get_Visit();
                bind_NewActs();
                bind_AddedActs();
                visitICONS();
                ddlVisit.Enabled = true;
                txtGrp.Text = "";
                btnUpdateVisit.Visible = false;
                btnAddVisit.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddToVisit_Click(object sender, EventArgs e)
        {
            try
            {
                addToVisit();
                visitICONS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemoveFromVisit_Click(object sender, EventArgs e)
        {
            try
            {
                removeFromVisit();
                visitICONS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUpdateVisit_Click(object sender, EventArgs e)
        {
            try
            {
                edit_Visit();
                ddlVisit.Enabled = false;
                visitICONS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnDeleteVisit_Click(object sender, EventArgs e)
        {
            try
            {
                delete_Visit();
                get_Visit();
                bind_NewActs();
                bind_AddedActs();
                visitICONS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Visit()
        {
            try
            {
                DataSet ds = dal.SiteBudget_SP(Action: "get_Visit", Project_Id: Session["PROJECTID"].ToString());
                ddlVisit.DataSource = ds.Tables[0];
                ddlVisit.DataValueField = "ID";
                ddlVisit.DataTextField = "Visit";
                ddlVisit.DataBind();
                ddlVisit.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_AddedActs();
                bind_NewActs();
                visitICONS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void visitICONS()
        {
            try
            {
                if (ddlVisit.SelectedIndex == 0)
                {
                    lbtnUpdateVisit.Visible = false;
                    lbtnDeleteVisit.Visible = false;
                }
                else
                {
                    lbtnUpdateVisit.Visible = true;
                    if (gvAddedActs.Rows.Count > 0)
                    {
                        lbtnDeleteVisit.Visible = false;
                    }
                    else
                    {
                        lbtnDeleteVisit.Visible = true;
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