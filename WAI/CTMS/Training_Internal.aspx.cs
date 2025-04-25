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
    public partial class Training_Internal : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    get_Plan();
                    get_Trainee();
                    btnsubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Plan()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_TrainPlan_CTMS", Project_ID: Session["PROJECTID"].ToString());
                ddlPlan.DataSource = ds.Tables[0];
                ddlPlan.DataValueField = "ID";
                ddlPlan.DataTextField = "TrainingPlan";
                ddlPlan.DataBind();
                ddlPlan.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Trainee()
        {
            try
            {
                DataSet ds = dal.Train_Verification_SP(Action: "get_Trainees", Project_ID: Session["PROJECTID"].ToString());
                ddlTrainee.DataSource = ds.Tables[0];
                ddlTrainee.DataValueField = "Emp_ID";
                ddlTrainee.DataTextField = "Name";
                ddlTrainee.DataBind();
                ddlTrainee.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_Sections()
        {
            try
            {
                if (ddlPlan.SelectedIndex != 0 && ddlTrainee.SelectedIndex != 0)
                {
                    DataSet ds = dal.Train_Verification_SP(Action: "get_Plan_Section", Project_ID: Session["PROJECTID"].ToString(), Plan_ID: ddlPlan.SelectedValue);
                    gvSections.DataSource = ds.Tables[0];
                    gvSections.DataBind();
                    if (gvSections.Rows.Count > 0)
                    {
                        btnsubmit.Visible = true;
                    }
                }
                else
                {
                    gvSections.DataSource = null;
                    gvSections.DataBind();
                    btnsubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSections_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Section_ID = drv["Section_ID"].ToString();
                    string Doc = drv["Doc"].ToString();

                    GridView gvSubSection = e.Row.FindControl("gvSubSection") as GridView;
                    DataSet ds = dal.Train_Verification_SP(Action: "get_Plan_SubSection", Plan_ID: ddlPlan.SelectedValue, Project_ID: Session["PROJECTID"].ToString(), Section_ID: Section_ID, Emp_ID: ddlTrainee.SelectedValue);
                    gvSubSection.DataSource = ds.Tables[0];
                    gvSubSection.DataBind();

                    LinkButton lbtnDownloadDoc = e.Row.FindControl("lbtnDownloadDoc") as LinkButton;
                    if (Doc != "0")
                    {
                        lbtnDownloadDoc.Visible = true;
                    }
                    else
                    {
                        lbtnDownloadDoc.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlTrainee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                get_Sections();
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
                get_Sections();
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
                string SubSectionIDs = "";
                for (int a = 0; a < gvSections.Rows.Count; a++)
                {
                    GridView gvSubSection = gvSections.Rows[a].FindControl("gvSubSection") as GridView;

                    for (int i = 0; i < gvSubSection.Rows.Count; i++)
                    {
                        CheckBox ChAction;
                        ChAction = (CheckBox)gvSubSection.Rows[i].FindControl("Chk_Sel");

                        if (ChAction.Checked == true && ChAction.Enabled == true)
                        {
                            string lbl_Sub_Section_ID = ((Label)gvSubSection.Rows[i].FindControl("lbl_Sub_Section_ID")).Text;
                            string ProjectID = Session["PROJECTID"].ToString();
                            if (SubSectionIDs == "")
                            {
                                SubSectionIDs = lbl_Sub_Section_ID;
                            }
                            else
                            {
                                SubSectionIDs += "," + lbl_Sub_Section_ID;
                            }
                        }
                    }
                }
                if (SubSectionIDs != "")
                {
                    Response.Redirect("Train_Verification.aspx?SubSec=" + SubSectionIDs + "&Plan_ID=" + ddlPlan.SelectedValue + "&Emp_ID=" + ddlTrainee.SelectedValue + "");
                }                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSubSection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Read = drv["Read"].ToString();

                    CheckBox Chk_Sel = e.Row.FindControl("Chk_Sel") as CheckBox;
                    if (Read != "0")
                    {
                        Chk_Sel.Checked = true;
                        Chk_Sel.Enabled = false;
                    }
                    else
                    {
                        Chk_Sel.Checked = false;
                        Chk_Sel.Enabled = true;                    
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