using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class Checklist_Master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    bind_DDL_Section();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_DDL_Section()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.drp_SectionsMaster();
                Drp_Section.DataSource = ds.Tables[0];
                Drp_Section.DataValueField = "SECTIONID";
                Drp_Section.DataTextField = "MODULENAME";
                Drp_Section.DataBind();
                Drp_Section.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bindMore_DDL_Section()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.drp_SectionsMaster();
                ddlSection.DataSource = ds.Tables[0];
                ddlSection.DataValueField = "SECTIONID";
                ddlSection.DataTextField = "MODULENAME";
                ddlSection.DataBind();
                ddlSection.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_DDL_SubSection()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds1 = dal.drp_SectionsMaster(Drp_Section.SelectedValue);
                Drp_SubSection.DataSource = ds1.Tables[0];
                Drp_SubSection.DataValueField = "SUBSECTIONID";
                Drp_SubSection.DataTextField = "SUBMODULENAME";
                Drp_SubSection.DataBind();
                Drp_SubSection.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Drp_Section_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_DDL_SubSection();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Drp_SubSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetData()
        {
            try
            {
                string SectionID=null;
                string SubSectionID=null;
                string ProjectID = Session["PROJECTID"].ToString();
                if (Drp_Section.SelectedValue!="0")
                {
                    SectionID = Drp_Section.SelectedValue;
                }
                if (Drp_SubSection.SelectedValue != "0")
                {
                    SubSectionID = Drp_SubSection.SelectedValue;
                }
                
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.get_ChecklistMaster(SectionID: SectionID, SubSectionID: SubSectionID, ProjectID:ProjectID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvChecklists.DataSource = ds.Tables[0];
                    gvChecklists.DataBind();
                }
                else
                {
                    gvChecklists.DataSource = null;
                    gvChecklists.DataBind();
                
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Btn_Add_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvChecklists.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvChecklists.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        string ID = ((Label)gvChecklists.Rows[i].FindControl("lbl_ID")).Text;
                        string fieldName = ((Label)gvChecklists.Rows[i].FindControl("lbl_fieldName")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Insert_Checklists(Id: ID, FieldName: fieldName, ProjectID: ProjectID, Action: "Add");
                    }
                }
                GetData();
                //Response.Write("<script> alert('Record Added successfully.');window.location='Checklist_Master.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Btn_Remove_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvChecklists.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvChecklists.Rows[i].FindControl("Chk_Sel_Remove_Fun");

                    if (ChAction.Checked)
                    {
                        string ID = ((Label)gvChecklists.Rows[i].FindControl("lbl_ID")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Insert_Checklists(Id: ID, ProjectID: ProjectID, Action: "remove");
                    }
                }
                GetData();
                //Response.Write("<script> alert('Record Removed successfully.');window.location='Checklist_Master.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void chk_moreChecklist_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_moreChecklist.Checked == true)
                {
                    bindMore_DDL_Section();
                    divMoreChecks.Visible = true;
                }
                else
                {
                    divMoreChecks.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

                string Section, SubSection, Checklist, ControlType, Date, VariableName, SubSectionYN;
                Section = ddlSection.SelectedValue;
                SubSection = ddlSubSection.SelectedValue;
                Checklist = txtChkList.Text;
                ControlType = ddlChkList.SelectedValue;
                if (ddlChkList.SelectedValue == "TEXTBOX")
                {
                    if (chkDateType.Checked == true)
                    {
                        Date = "1";
                    }
                    else
                    {
                        Date = null;
                    }
                }
                else
                {
                    Date = null;
                }
                VariableName = txtVariable.Text;
                if (chkSubSection.Checked == true)
                {
                    SubSectionYN = "1";
                }
                else
                {
                    SubSectionYN = "0";
                }
                string ProjectID = Session["PROJECTID"].ToString();

                string msg = dal.Insert_Checklists(ProjectID:ProjectID,SectionId:Section,SubSectionId:SubSection,FieldName:Checklist,controlType:ControlType,Date:Date, variablename:VariableName, Action: "AddMore", SubSectionYN:SubSectionYN);

                if (msg == "Done")
                {
                    ddlSection.SelectedIndex = 0;
                    ddlSubSection.Items.Clear();
                    txtChkList.Text = "";
                    ddlChkList.SelectedIndex = 0;
                    txtVariable.Text = "";
                    tr_datetype.Visible = false;
                    tr_datetype_BR.Visible = false;
                    chkDateType.Checked = false;
                    chkSubSection.Checked = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds1 = dal.drp_SectionsMaster(ddlSection.SelectedValue);
                ddlSubSection.DataSource = ds1.Tables[0];
                ddlSubSection.DataValueField = "SUBSECTIONID";
                ddlSubSection.DataTextField = "SUBMODULENAME";
                ddlSubSection.DataBind();
                ddlSubSection.Items.Insert(0, new ListItem("--Select--", "0"));
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
                if (ddlChkList.SelectedValue == "TEXTBOX")
                {
                    tr_datetype.Visible = true;
                    tr_datetype_BR.Visible = true;
                }
                else
                {
                    tr_datetype.Visible = false;
                    tr_datetype_BR.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvChecklists_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Exist"].ToString();
                    string count = drv["Count"].ToString();
                    CheckBox Chk_Sel_Fun = (e.Row.FindControl("Chk_Sel_Fun") as CheckBox);
                    CheckBox Chk_Sel_Remove_Fun = (e.Row.FindControl("Chk_Sel_Remove_Fun") as CheckBox);
                    if (Convert.ToInt32(id) > 0)
                    {
                        Chk_Sel_Fun.Visible = false;
                        if (Convert.ToInt32(count) > 0)
                        {
                            Chk_Sel_Remove_Fun.Visible = false;
                        }
                        else
                        {
                            Chk_Sel_Remove_Fun.Visible = true;
                        }                        
                    }
                    else
                    {
                        Chk_Sel_Fun.Visible = true;
                        Chk_Sel_Remove_Fun.Visible = false;
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