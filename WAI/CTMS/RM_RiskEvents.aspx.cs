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
    public partial class RM_RiskEvents : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    if (Session["PROJECTID"] != null)
                    {
                        Drp_Project_Name.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTID"].ToString()));
                    }
                    else
                    {
                        fill_drpdwn();
                    }

                    ddl_Analysed.Items.Clear();
                    ddl_Analysed.Items.Insert(0, new ListItem("--All--", "0"));
                    ddl_Analysed.Items.Insert(1, new ListItem("Analyzed", "1"));
                    ddl_Analysed.Items.Insert(2, new ListItem("New", "2"));
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
                getData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }

        }

        private void getData()
        {
            try
            {
                DataTable dt = new DataTable();
                if (ddl_Analysed.SelectedValue == "1")
                {
                    dt = dal.getprojectevents(Action: "Analyzed", Id: Session["PROJECTID"].ToString());
                }
                else if (ddl_Analysed.SelectedValue == "2")
                {
                    dt = dal.getprojectevents(Action: "Unanalyzed", Id: Session["PROJECTID"].ToString());
                }
                else
                {
                    dt = dal.getprojectevents(Action: "Select", Id: Session["PROJECTID"].ToString());
                }

                gridprojevents.DataSource = dt;
                gridprojevents.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gridprojevents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gridprojevents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                dal.getprojectevents(Action: "Delete", Id: id);

                getData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


    }
}