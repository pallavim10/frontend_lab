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
    public partial class Train_Site_Plan : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    get_Plan();
                    get_Section();
                    btnUpdatePlan.Visible = false;
                    lbtnDeletePlan.Visible = false;
                    lbtnUpdatePlan.Visible = false;
                    btnUpdateQue.Visible = false;
                }
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
                DataSet ds = dal.Training_SP(Action: "get_New_Site", ID: ddlPlan.SelectedValue, Project_ID: Session["PROJECTID"].ToString(), Section_ID: ddlSec.SelectedValue);
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
                DataSet ds = dal.Training_SP(Action: "get_Added_Site", ID: ddlPlan.SelectedValue, Project_ID: Session["PROJECTID"].ToString(), Section_ID: ddlSec.SelectedValue);
                gvAddedActs.DataSource = ds.Tables[0];
                gvAddedActs.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Plan()
        {
            try
            {
                dal.Training_SP(Action: "insert_TrainPlan_Site", TrainingPlan: txtGrp.Text, Project_ID: Session["PROJECTID"].ToString());
                txtGrp.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Plan()
        {
            try
            {
                dal.Training_SP(Action: "update_TrainPlan_Site", TrainingPlan: txtGrp.Text, ID: ddlPlan.SelectedValue);
                txtGrp.Text = "";
                ddlPlan.Enabled = true;
                btnAddPlan.Visible = true;
                btnUpdatePlan.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Plan()
        {
            try
            {
                Session["PLANID"] = ID;
                DataSet ds = dal.Training_SP(Action: "select_TrainPlan_Site", ID: ddlPlan.SelectedValue);
                btnAddPlan.Visible = false;
                btnUpdatePlan.Visible = true;
                ddlPlan.Enabled = false;
                txtGrp.Text = ds.Tables[0].Rows[0]["TrainingPlan"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Plan()
        {
            try
            {
                dal.Training_SP(Action: "delete_TrainPlan_Site", ID: ddlPlan.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void addToPlan()
        {
            try
            {
                for (int i = 0; i < gvNewActs.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvNewActs.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblSubSectionID = (Label)gvNewActs.Rows[i].FindControl("lblSubSectionID");
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Training_SP(Action: "addToPlan_Site", Project_ID: ProjectID, Section_ID: ddlSec.SelectedValue, SubSection_ID: lblSubSectionID.Text, ID: ddlPlan.SelectedValue);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void removeFromPlan()
        {
            try
            {
                for (int i = 0; i < gvAddedActs.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAddedActs.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblSubSectionID = (Label)gvAddedActs.Rows[i].FindControl("lblSubSectionID");
                        dal.Training_SP(Action: "removeFromPlan_Site", ID: lblSubSectionID.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void get_Plan()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_TrainPlan_Site", Project_ID: Session["PROJECTID"].ToString());
                ddlPlan.DataSource = ds.Tables[0];
                ddlPlan.DataValueField = "ID";
                ddlPlan.DataTextField = "TrainingPlan";
                ddlPlan.DataBind();
                ddlPlan.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlQuePlan.DataSource = ds.Tables[0];
                ddlQuePlan.DataValueField = "ID";
                ddlQuePlan.DataTextField = "TrainingPlan";
                ddlQuePlan.DataBind();
                ddlQuePlan.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Section()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_Sec_Site", Project_ID: Session["PROJECTID"].ToString());
                ddlSec.DataSource = ds.Tables[0];
                ddlSec.DataValueField = "Section_ID";
                ddlSec.DataTextField = "Section";
                ddlSec.DataBind();
                ddlSec.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Plan_ICONS()
        {
            try
            {
                if (ddlPlan.SelectedIndex == 0)
                {
                    lbtnUpdatePlan.Visible = false;
                    lbtnDeletePlan.Visible = false;
                }
                else
                {
                    lbtnUpdatePlan.Visible = true;
                    if (gvAddedActs.Rows.Count > 0)
                    {
                        lbtnDeletePlan.Visible = false;
                    }
                    else
                    {
                        lbtnDeletePlan.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_AddedActs();
                bind_NewActs();
                Plan_ICONS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_AddedActs();
                bind_NewActs();
                Plan_ICONS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddToPlan_Click(object sender, EventArgs e)
        {
            try
            {
                addToPlan();
                bind_AddedActs();
                get_SubSec();
                bind_NewActs();
                Plan_ICONS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemoveFromPlan_Click(object sender, EventArgs e)
        {
            try
            {
                removeFromPlan();
                bind_AddedActs();
                bind_NewActs();
                Plan_ICONS();
                get_SubSec();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnAddPlan_Click(object sender, EventArgs e)
        {
            try
            {
                insert_Plan();
                get_Plan();
                bind_AddedActs();
                bind_NewActs();
                Plan_ICONS();
                get_SubSec();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdatePlan_Click(object sender, EventArgs e)
        {
            try
            {
                update_Plan();
                get_Plan();
                get_SubSec();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUpdatePlan_Click(object sender, EventArgs e)
        {
            try
            {
                edit_Plan();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnDeletePlan_Click(object sender, EventArgs e)
        {
            try
            {
                delete_Plan();
                get_Plan();
                Plan_ICONS();
                get_SubSec();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitQue_Click(object sender, EventArgs e)
        {
            try
            {
                insert_ques();
                txtQue.Text = "";
                ddlChkList.SelectedIndex = 0;
                chkDate_Display();
                chkDateType.Checked = false;
                txtANS.Text = "";
                txtChkSeq.Text = "";
                get_Que();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateQue_Click(object sender, EventArgs e)
        {
            try
            {
                update_Que();
                txtQue.Text = "";
                ddlChkList.SelectedIndex = 0;
                chkDate_Display();
                chkDateType.Checked = false;
                txtChkSeq.Text = "";
                txtANS.Text = "";
                btnUpdateQue.Visible = false;
                btnSubmitQue.Visible = true;
                get_Que();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelQue_Click(object sender, EventArgs e)
        {
            try
            {
                txtQue.Text = "";
                ddlChkList.SelectedIndex = 0;
                chkDate_Display();
                chkDateType.Checked = false;
                txtChkSeq.Text = "";
                btnUpdateQue.Visible = false;
                btnSubmitQue.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlQuePlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                get_SubSec();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        private void get_SubSec()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_Added_Site", ID: ddlQuePlan.SelectedValue, Project_ID: Session["PROJECTID"].ToString(), Section_ID: ddlSec.SelectedValue);
                ddlSubSec.DataSource = ds.Tables[0];
                ddlSubSec.DataValueField = "Sub_Section_ID";
                ddlSubSec.DataTextField = "SubSection";
                ddlSubSec.DataBind();
                ddlSubSec.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlChkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                chkDate_Display();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_ques()
        {
            try
            {
                string date = "";
                if (chkDateType.Checked == true)
                {
                    date = "1";
                }
                dal.Train_Verification_SP(Action: "insert_Que_Site", Plan_ID: ddlQuePlan.SelectedValue, SEQNO: txtChkSeq.Text, FIELDNAME: txtQue.Text, CONTROLTYPE: ddlChkList.SelectedValue, SubSec_ID: ddlSubSec.SelectedValue, ANS: txtANS.Text, Date: date);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Que()
        {
            try
            {
                string date = "";
                if (chkDateType.Checked == true)
                {
                    date = "1";
                }
                dal.Train_Verification_SP(Action: "update_Que_Site", ID: Session["QUEID"].ToString(), Plan_ID: ddlQuePlan.SelectedValue, SEQNO: txtChkSeq.Text, FIELDNAME: txtQue.Text, CONTROLTYPE: ddlChkList.SelectedValue, ANS: txtANS.Text, Date: date);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_Que(string id)
        {
            try
            {
                dal.Train_Verification_SP(Action: "delete_Que_Site", ID: id);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Que()
        {
            try
            {
                DataSet ds = dal.Train_Verification_SP(Action: "get_Que_Site", Plan_ID: ddlQuePlan.SelectedValue, SubSec_ID: ddlSubSec.SelectedValue);
                gvQues.DataSource = ds.Tables[0];
                gvQues.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void edit_Que(string id)
        {
            try
            {
                DataSet ds = dal.Train_Verification_SP(Action: "select_Que_Site", ID: id);

                ddlQuePlan.SelectedValue = ds.Tables[0].Rows[0]["Plan_ID"].ToString();
                txtQue.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                ddlChkList.SelectedValue = ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString();
                chkDate_Display();
                ddlSubSec.SelectedValue = ds.Tables[0].Rows[0]["SubSection_ID"].ToString();
                if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("txtDate"))
                {
                    chkDateType.Checked = true;
                }
                else
                {
                    chkDateType.Checked = false;
                }
                txtANS.Text = ds.Tables[0].Rows[0]["ANS"].ToString();
                txtChkSeq.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

                btnUpdateQue.Visible = true;
                btnSubmitQue.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void chkDate_Display()
        {
            try
            {
                if (ddlChkList.SelectedValue == "TEXTBOX")
                {
                    tr_datetype.Visible = true;
                }
                else
                {
                    tr_datetype.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvQues_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string CONTROLTYPE = drv["CONTROLTYPE"].ToString();
                    LinkButton lbtnItems = (e.Row.FindControl("lbtnItems") as LinkButton);
                    if (CONTROLTYPE == "DROPDOWN" || CONTROLTYPE == "CHECKBOX" || CONTROLTYPE == "RADIOBUTTON")
                    {
                        lbtnItems.Visible = true;
                    }
                    else
                    {
                        lbtnItems.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvQues_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();
                Session["QUEID"] = id;
                if (e.CommandName == "Edit1")
                {
                    edit_Que(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    delete_Que(id);
                }
                get_Que();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSubSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                get_Que();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}