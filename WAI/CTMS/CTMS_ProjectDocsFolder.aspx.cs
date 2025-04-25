using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class CTMS_ProjectDocsFolder : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    bind_Folders();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Folders()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_Folder");
                ddlFolder.DataSource = ds.Tables[0];
                ddlFolder.DataValueField = "ID";
                ddlFolder.DataTextField = "Folder";
                ddlFolder.DataBind();
                ddlFolder.Items.Insert(0, new ListItem("--Select--", "0"));
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

                for (int i = 0; i < gvSubFolder.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvSubFolder.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        string ID = ((Label)gvSubFolder.Rows[i].FindControl("lbl_ID")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal_eTMF.eTMF_SET_SP(ACTION: "add_Project", Folder_ID: ddlFolder.SelectedValue, SubFolder_ID: ID, Project_ID: ProjectID);
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

        private void GetData()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_SubFolder", ID: ddlFolder.SelectedValue, Project_ID: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvSubFolder.DataSource = ds.Tables[0];
                    gvSubFolder.DataBind();
                }
                else
                {
                    gvSubFolder.DataSource = null;
                    gvSubFolder.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Btn_Remove_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvSubFolder.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvSubFolder.Rows[i].FindControl("Chk_Sel_Remove_Fun");

                    if (ChAction.Checked)
                    {
                        string ID = ((Label)gvSubFolder.Rows[i].FindControl("lbl_ID")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal_eTMF.eTMF_SET_SP(ACTION: "remove_Project", Folder_ID: ddlFolder.SelectedValue, SubFolder_ID: ID, Project_ID: ProjectID);
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

        protected void ddlFolder_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void gvSubFolder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string count = drv["Count"].ToString();
                    CheckBox Chk_Sel_Fun = (e.Row.FindControl("Chk_Sel_Fun") as CheckBox);
                    CheckBox Chk_Sel_Remove_Fun = (e.Row.FindControl("Chk_Sel_Remove_Fun") as CheckBox);

                    if (Convert.ToInt32(count) > 0)
                    {
                        Chk_Sel_Fun.Visible = false;
                        Chk_Sel_Remove_Fun.Visible = true;
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