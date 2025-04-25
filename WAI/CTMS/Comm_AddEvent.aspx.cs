using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

namespace CTMS
{
    public partial class Comm_AddEvent : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction comfunc = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    fill_Project();
                    fill_Dept();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Project()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Specific_Project",
                Project_ID: Convert.ToInt32(Session["PROJECTID"]),
                ENTEREDBY: Session["User_ID"].ToString()
                );
                Drp_Project.DataSource = ds.Tables[0];
                Drp_Project.DataValueField = "Project_ID";
                Drp_Project.DataTextField = "PROJNAME";
                Drp_Project.DataBind();
                Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
                if (Session["PROJECTID"] != null)
                {
                    Drp_Project.Items.FindByValue(Session["PROJECTID"].ToString()).Selected = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Dept()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "get_Dept");

                list_Dept.Items.Clear();
                list_Dept.DataSource = ds;
                list_Dept.DataTextField = "Dept_Name";
                list_Dept.DataValueField = "Dept_ID";
                list_Dept.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Event()
        {
            try
            {
                string Department = null;
                foreach (ListItem item in list_Dept.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Department == null)
                        {
                            Department = item.Value;
                        }
                        else
                        {
                            Department += "," + item.Value;
                        }
                    }
                }

                DataSet ds = dal.Budget_SP(Action: "Get_Event", Dept_Name: Department);

                ddlEvent.DataSource = ds;
                ddlEvent.DataValueField = "Event";
                ddlEvent.DataBind();

                ddlEvent.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlEvent.Items.Insert(1, new ListItem("Not In List", "Not In List"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void list_Dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_Event();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Insert_EventLogs();
                Response.Redirect(this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Insert_EventLogs()
        {
            DataTable ReturnValue = new DataTable();
            try
            {
                string UserID = null, PROJECTID = null, Type = null, Nature = null, Notes = null, Department = null, Reference = null, Event = null;

                UserID = Session["User_ID"].ToString();
                PROJECTID = Session["PROJECTID"].ToString();
                Nature = Drp_Nature.SelectedValue;
                Notes = txtNote.Text;
                Type = Drp_Type.SelectedValue;
                foreach (ListItem item in list_Dept.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Department == null)
                        {
                            Department = item.Text;
                        }
                        else
                        {
                            Department += "," + item.Text;
                        }
                    }
                }
                Reference = Drp_Refer.SelectedValue;
                Event = ddlEvent.SelectedValue;
                DataSet ds;

                string DateTimeSent = txtEventDate.Text;

                ds = dal.Communication_SP(Action: "Insert_EventLog",
                UserID: UserID,
                PROJECTID: PROJECTID,
                Type: Type,
                Nature: Nature,
                Notes: Notes,
                Department: Department,
                Reference: Reference,
                Event: Event,
                DateTimeSent: DateTimeSent
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}