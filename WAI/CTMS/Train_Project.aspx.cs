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
    public partial class Train_Project : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    get_Section();
                }                
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
                DataSet ds = dal.Training_SP(Action: "get_Section");

                ddlSec.DataSource = ds.Tables[0];
                ddlSec.DataValueField = "ID";
                ddlSec.DataTextField = "Section";
                ddlSec.DataBind();
                ddlSec.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void get_SubSection()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "get_SubSection", Section_ID: ddlSec.SelectedValue);
                gvSubSec.DataSource = ds.Tables[0];
                gvSubSec.DataBind();
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
                get_SubSection();
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

                for (int i = 0; i < gvSubSec.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvSubSec.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        string lbl_SectionID = ((Label)gvSubSec.Rows[i].FindControl("lbl_SectionID")).Text;
                        string lbl_SubSectionID = ((Label)gvSubSec.Rows[i].FindControl("lbl_SubSectionID")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Training_SP(Action: "add_Project", Project_ID: ProjectID, Section_ID: lbl_SectionID, SubSection_ID: lbl_SubSectionID);
                    }
                }
                get_SubSection();
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

                for (int i = 0; i < gvSubSec.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvSubSec.Rows[i].FindControl("Chk_Sel_Remove_Fun");

                    if (ChAction.Checked)
                    {
                        string lbl_SectionID = ((Label)gvSubSec.Rows[i].FindControl("lbl_SectionID")).Text;
                        string lbl_SubSectionID = ((Label)gvSubSec.Rows[i].FindControl("lbl_SubSectionID")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Training_SP(Action: "remove_Project", Project_ID: ProjectID, Section_ID: lbl_SectionID, SubSection_ID: lbl_SubSectionID);
                    }
                }
                get_SubSection();
                //Response.Write("<script> alert('Record Removed successfully.');window.location='Checklist_Master.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void gvSubSec_PreRender(object sender, EventArgs e)
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

        protected void gvSubSec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["Count"].ToString();
                    CheckBox Chk_Sel_Fun = (e.Row.FindControl("Chk_Sel_Fun") as CheckBox);
                    CheckBox Chk_Sel_Remove_Fun = (e.Row.FindControl("Chk_Sel_Remove_Fun") as CheckBox);
                    if (Convert.ToInt32(id) > 0)
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