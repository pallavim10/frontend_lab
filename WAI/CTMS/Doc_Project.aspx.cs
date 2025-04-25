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
    public partial class Doc_Project : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    bind_Plans();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Plans()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_DOC");
                ddlPlan.DataSource = ds.Tables[0];
                ddlPlan.DataValueField = "ID";
                ddlPlan.DataTextField = "DocName";
                ddlPlan.DataBind();
                ddlPlan.Items.Insert(0, new ListItem("--Select--", "0"));
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

                for (int i = 0; i < gvSection.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvSection.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        string ID = ((Label)gvSection.Rows[i].FindControl("lbl_ID")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Documents_SP(Action: "AddToProject", DocID: ddlPlan.SelectedValue, SecID: ID, ProjectID: ProjectID);
                    }
                }
                GetData();
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
                DataSet ds = dal.Documents_SP(Action: "GetProjectMaster", DocID: ddlPlan.SelectedValue, ProjectID: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvSection.DataSource = ds.Tables[0];
                    gvSection.DataBind();
                }
                else
                {
                    gvSection.DataSource = null;
                    gvSection.DataBind();
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

                for (int i = 0; i < gvSection.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvSection.Rows[i].FindControl("Chk_Sel_Remove_Fun");

                    if (ChAction.Checked)
                    {
                        string ID = ((Label)gvSection.Rows[i].FindControl("lbl_ID")).Text;
                        dal.Documents_SP(Action: "RemoveFromProject", DocID: ddlPlan.SelectedValue, SecID: ID, ProjectID: Session["PROJECTID"].ToString());
                    }
                }
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void gvSection_RowDataBound(object sender, GridViewRowEventArgs e)
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