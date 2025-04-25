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
    public partial class Remove_AnticipatedProject_Risk : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["PROJECTID"] != null)
                {
                    Drp_Project_Name.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTID"].ToString()));
                }
                else
                {
                    fill_drpdwn();
                }
            }
        }

        private void fill_drpdwn()
        {        
            DataSet ds = dal.GetSetPROJECTDETAILS(
            Action: "Get_Specific_Project",
            Project_ID: Convert.ToInt32(Session["PROJECTID"]),
            ENTEREDBY: Session["User_ID"].ToString()
            );
            Drp_Project_Name.DataSource = ds;
            Drp_Project_Name.DataValueField = "PROJNAME";
            Drp_Project_Name.DataBind();

            Drp_Project_Name.Items.Insert(0, new ListItem("--Select Project--", "0"));

        }

        protected void Btn_Get_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.getsetRisk_SP(Action: "Remove_Anticipated_Risk_New", Project_ID: Drp_Project_Name.SelectedValue);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }


        }
        protected void Btn_Add_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)GridView1.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        string Risk_ID = ((Label)GridView1.Rows[i].FindControl("lbl_RISK_ID")).Text;
                        DataSet ds = dal.getsetRisk_SP(Action: "Update_ProjectRisk", Project_ID: Drp_Project_Name.SelectedValue, RISK_ID: Risk_ID, ENTEREDBY: Session["User_ID"].ToString());
                    }
                }
                Response.Write("<script> alert('Record Updated successfully.');window.location='Remove_AnticipatedProject_Risk.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }
    }
}