using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Data.SqlClient;

namespace CTMS
{
    public partial class GROUP_WISE_DASHBOARD : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    fill_Proj_Name();
                    MODULENAME();
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    fill_drpdwn_User_Group_ID();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Proj_Name()
        {
            try
            {
                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Specific_Project",
                Project_ID: Convert.ToInt32(Session["PROJECTID"]),
                ENTEREDBY: Session["User_ID"].ToString()
                );
                Drp_Project_Name.DataSource = ds;
                Drp_Project_Name.DataTextField = "PROJNAME";
                Drp_Project_Name.DataValueField = "Project_ID";
                Drp_Project_Name.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Drp_Project_Name.Items.Insert(0, new ListItem("--Select Project--", "0"));
                    Drp_Project_Name.SelectedValue = ds.Tables[0].Rows[0]["Project_ID"].ToString();
                }
                else if (ds.Tables[0].Rows.Count > 1)
                {
                    Drp_Project_Name.Items.Insert(0, new ListItem("--Select Project--", "0"));
                }
               
                Drp_User_Group.Items.Insert(0, new ListItem("--Select User Group--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void MODULENAME()
        {
            try
            {
                DataSet ds = dal.DASHBOARD_ASSIGNING(
                Action: "GETMASTER_FUNCTION"
                );
                ddlModulName.DataSource = ds;
                ddlModulName.DataTextField = "FunctionName";
                ddlModulName.DataValueField = "FunctionID";
                ddlModulName.DataBind();

                ddlModulName.Items.Insert(0, new ListItem("--Select Module Name--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Drp_Project_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_drpdwn_User_Group_ID();
        }

        private void fill_drpdwn_User_Group_ID()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con = new SqlConnection(dal.getconstr());
                SqlCommand cmd = new SqlCommand("Get_User_Group_ID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_ID", Drp_Project_Name.SelectedValue);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                Drp_User_Group.DataSource = ds.Tables[0];
                Drp_User_Group.DataTextField = "UserGroup_Name";
                Drp_User_Group.DataValueField = "UserGroup_ID";
                Drp_User_Group.DataBind();
                con.Close();
                Drp_User_Group.Items.Insert(0, new ListItem("--Select User Group--", "0"));
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Drp_User_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAddedDashboard.DataSource = null;
            grdAddedDashboard.DataBind();
            grdDashboard.DataSource = null;
            grdDashboard.DataBind();
            ddltype.SelectedIndex = 0;
            ddlModulName.SelectedIndex = 0;

            btnSubmit.Visible = false;
            btnCancel.Visible = false;
        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAddedDashboard.DataSource = null;
            grdAddedDashboard.DataBind();
            grdDashboard.DataSource = null;
            grdDashboard.DataBind();
            ddlModulName.SelectedIndex = 0;

            btnSubmit.Visible = false;
            btnCancel.Visible = false;
        }

        protected void GET_DASHBOARD_AGAINST_GROUPID()
        {
            try
            {
                DataSet ds = dal.DASHBOARD_ASSIGNING(Action: "GET_DASHBOARD_AGAINST_GROUPID",
               PROJECTID: Drp_Project_Name.SelectedValue,
               USERGROUPID: Drp_User_Group.SelectedValue,
               TYPE: ddltype.SelectedValue,
               FUNCTIONID: ddlModulName.SelectedValue
               );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdDashboard.DataSource = ds;
                    grdDashboard.DataBind();
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {
                    grdDashboard.DataSource = null;
                    grdDashboard.DataBind();
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_ADDED_DASHBOARD_AGAINST_GROUPID()
        {
            try
            {
                DataSet ds = dal.DASHBOARD_ASSIGNING(Action: "GET_ADDED_DASHBOARD_AGAINST_GROUPID",
               PROJECTID: Drp_Project_Name.SelectedValue,
               USERGROUPID: Drp_User_Group.SelectedValue,
               TYPE: ddltype.SelectedValue,
               FUNCTIONID: ddlModulName.SelectedValue
               );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdAddedDashboard.DataSource = ds;
                    grdAddedDashboard.DataBind();
                }
                else
                {
                    grdAddedDashboard.DataSource = null;
                    grdAddedDashboard.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModulName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GET_DASHBOARD_AGAINST_GROUPID();
            GET_ADDED_DASHBOARD_AGAINST_GROUPID();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdDashboard.Rows.Count; i++)
                {
                    CheckBox chkName = (CheckBox)grdDashboard.Rows[i].FindControl("chkName");

                    if (chkName.Checked == true)
                    {
                        string DASHBOARDTYPEID = ((Label)grdDashboard.Rows[i].FindControl("ID")).Text;

                        DataSet ds = dal.DASHBOARD_ASSIGNING(Action: "INSERT_GROUP_WISE_DASHBOARD",
                        USERGROUPID: Drp_User_Group.SelectedValue,
                        PROJECTID: Drp_Project_Name.SelectedValue,
                        TYPE: ddltype.SelectedValue,
                        TYPEID: DASHBOARDTYPEID,
                        FUNCTIONID: ddlModulName.SelectedValue,
                        ENTEREDBY: Session["User_ID"].ToString()
                        );

                      
                    }
                }

                GET_DASHBOARD_AGAINST_GROUPID();
                GET_ADDED_DASHBOARD_AGAINST_GROUPID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            grdAddedDashboard.DataSource = null;
            grdAddedDashboard.DataBind();
            grdDashboard.DataSource = null;
            grdDashboard.DataBind();
            Drp_Project_Name.SelectedIndex = 0;
            Drp_User_Group.Items.Clear();
            ddlModulName.Items.Clear();
            ddltype.SelectedIndex = 0;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
        }

        protected void lbtndeleteSection_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdAddedDashboard.Rows.Count; i++)
                {
                    CheckBox chkAddedDashboard = (CheckBox)grdAddedDashboard.Rows[i].FindControl("chkAddedDashboard");

                    if (chkAddedDashboard.Checked == true)
                    {
                        string DASHBOARDTYPEID = ((Label)grdAddedDashboard.Rows[i].FindControl("ID")).Text;

                       

                        DataSet ds = dal.DASHBOARD_ASSIGNING(Action: "DELETE_GROUP_WISE_DASHBOARD",
                        ID: DASHBOARDTYPEID, ENTEREDBY: Session["User_ID"].ToString()
                        );
                    }
                }

                GET_DASHBOARD_AGAINST_GROUPID();
                GET_ADDED_DASHBOARD_AGAINST_GROUPID();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}