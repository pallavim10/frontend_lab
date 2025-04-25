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
    public partial class Sponsor_Project : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetData();
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

                for (int i = 0; i < gvSponsor.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvSponsor.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        string ID = ((Label)gvSponsor.Rows[i].FindControl("lbl_SponsorID")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Sponsor_SP(Action: "Add_To_Project", Project_ID: Session["PROJECTID"].ToString(), Sponsor_ID: ID);
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
                DataSet ds = dal.Sponsor_SP(Action: "Sposnor_Project", Project_ID: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvSponsor.DataSource = ds.Tables[0];
                    gvSponsor.DataBind();
                }
                else
                {
                    gvSponsor.DataSource = null;
                    gvSponsor.DataBind();
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

                for (int i = 0; i < gvSponsor.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvSponsor.Rows[i].FindControl("Chk_Sel_Remove_Fun");

                    if (ChAction.Checked)
                    {
                        string ID = ((Label)gvSponsor.Rows[i].FindControl("lbl_SponsorID")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Sponsor_SP(Action: "Remove_From_Project", Project_ID: Session["PROJECTID"].ToString(), Sponsor_ID: ID);
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

        protected void gvSponsor_RowDataBound(object sender, GridViewRowEventArgs e)
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