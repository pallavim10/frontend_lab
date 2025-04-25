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
    public partial class Manage_User_Groups : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["User_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx");
                }
                if (!this.IsPostBack)
                {
                    Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
                    drpCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
                    BINDGROUPNAME();
                    BINDASSIGNGROUPNAME();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        private void fill_drpdwn()
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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void fill_Country()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "GET_COUNTRY"
                );
                drpCountry.DataSource = ds.Tables[0];
                drpCountry.DataValueField = "CNTRYID";
                drpCountry.DataTextField = "COUNTRY";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.ManageUserGroups(ACTION: "INSERT_GROUPS", ENTEREDBY: Session["User_ID"].ToString(), GroupName: txtGroupsname.Text);
                BINDGROUPNAME();
                txtGroupsname.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.ManageUserGroups(ACTION: "Update_GROUPS", ENTEREDBY: Session["User_ID"].ToString(), ID: Session["ID"].ToString(), GroupName: txtGroupsname.Text);
                BINDGROUPNAME();
                txtGroupsname.Text = "";
                btnupdate.Visible = false;
                btnsubmit.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtGroupsname.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnAssigngroup_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.ManageUserGroups(ACTION: "INSERT_USERASSIGNGROUP", ENTEREDBY: Session["User_ID"].ToString(),
                ID: ddlGroups.SelectedValue,
                PROJECTID: Drp_Project.SelectedValue,
                UserGroup_Name: ddlGroups.SelectedItem.Text,
                countryID: drpCountry.SelectedValue
                );
                BINDASSIGNGROUPNAME();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateassigngroup_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.ManageUserGroups(ACTION: "UPDATE_USERASSIGNGROUP", ENTEREDBY: Session["User_ID"].ToString(),
                ID: Session["ASSIGNGROUPID"].ToString(),
                PROJECTID: Drp_Project.SelectedValue,
                UserGroup_Name: ddlGroups.SelectedItem.Text,
                countryID: drpCountry.SelectedValue
                );
                BINDASSIGNGROUPNAME();
                drpCountry.SelectedIndex = 0;
                Drp_Project.SelectedIndex = 0;
                btnupdateassigngroup.Visible = false;
                btnAssigngroup.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelassigngroup_Click(object sender, EventArgs e)
        {
            try
            {
                drpCountry.SelectedIndex = 0;
                Drp_Project.SelectedIndex = 0;
                ddlGroups.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUserGroups_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = Convert.ToString(e.CommandArgument);
                Session["ID"] = ID;
                if (e.CommandName == "EditGROUP")
                {
                    BINDGROUPNAMEBYID();
                }
                else if (e.CommandName == "DeleteGROUP")
                {
                    DELETEGROUPNAME(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUserGroups_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lbtndelete = (LinkButton)e.Row.FindControl("lbtndelete");
                string COUNTS = dr["COUNTS"].ToString();

                if (COUNTS == "0")
                {
                    lbtndelete.Visible = true;
                }
                else
                {
                    lbtndelete.Visible = false;
                }
            }
        }

        public void BINDGROUPNAMEBYID()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.ManageUserGroups(ACTION: "BINDGROUPNAMEBYID", ID: Session["ID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtGroupsname.Text = ds.Tables[0].Rows[0]["GroupName"].ToString();
                    btnsubmit.Visible = false;
                    btnupdate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETEGROUPNAME(string ID)
        {
            try
            {
                DataSet ds = dal.ManageUserGroups(ACTION: "DELETEUSERGROUP", ID: ID, ENTEREDBY: Session["User_ID"].ToString());
                BINDGROUPNAME();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BINDGROUPNAME()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.ManageUserGroups(ACTION: "BINDGROUPNAME");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlGroups.DataSource = ds;
                    ddlGroups.DataTextField = "GroupName";
                    ddlGroups.DataValueField = "ID";
                    ddlGroups.DataBind();

                    grdUserGroups.DataSource = ds;
                    grdUserGroups.DataBind();
                }
                ddlGroups.Items.Insert(0, new ListItem("--Select Group--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdAssignGroups_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = Convert.ToString(e.CommandArgument);
                Session["ASSIGNGROUPID"] = ID;
                if (e.CommandName == "EditASSIGNGROUP")
                {
                    BINDASSIGNGROUPNAMEBYID();
                }
                else if (e.CommandName == "Delete1")
                {
                    DELETEASSIGNGROUPNAMEBYID(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DELETEASSIGNGROUPNAMEBYID(string ID)
        {
            try
            {
                DataSet ds = dal.ManageUserGroups(ACTION: "DeleteAssignGroup", ID: ID, ENTEREDBY: Session["User_ID"].ToString());
                BINDASSIGNGROUPNAME();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BINDASSIGNGROUPNAMEBYID()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.ManageUserGroups(ACTION: "BINDASSIGNGROUPNAMEBYID", ID: Session["ASSIGNGROUPID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlGroups.SelectedValue = ds.Tables[0].Rows[0]["UserGroup_ID"].ToString();
                    fill_Country();
                    drpCountry.SelectedValue = ds.Tables[0].Rows[0]["CNTRYID"].ToString();
                    fill_drpdwn();
                    Drp_Project.SelectedValue = ds.Tables[0].Rows[0]["PROJECTID"].ToString();
                    btnAssigngroup.Visible = false;
                    btnupdateassigngroup.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BINDASSIGNGROUPNAME()
        {
            try
            {
                if (Session["PROJECTID"] == null)
                {
                    DataSet ds = new DataSet();
                    ds = dal.ManageUserGroups(ACTION: "BINDASSIGNGROUPNAME", ID: ddlGroups.SelectedValue, countryID: drpCountry.SelectedValue, PROJECTID: Drp_Project.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdAssignGroups.DataSource = ds;
                        grdAssignGroups.DataBind();
                    }
                    else
                    {
                        grdAssignGroups.DataSource = null;
                        grdAssignGroups.DataBind();
                    }
                }
                else
                {
                    DataSet ds = new DataSet();
                    ds = dal.ManageUserGroups(ACTION: "BINDASSIGNGROUPNAME", ID: ddlGroups.SelectedValue, countryID: drpCountry.SelectedValue, PROJECTID: Session["PROJECTID"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdAssignGroups.DataSource = ds;
                        grdAssignGroups.DataBind();
                    }
                    else
                    {
                        grdAssignGroups.DataSource = null;
                        grdAssignGroups.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BINDASSIGNGROUPNAME();
                fill_drpdwn();
                fill_Country();
                //drpCountry.SelectedIndex = 0;
                //Drp_Project.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Drp_Project_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.ManageUserGroups(ACTION: "BINDASSIGNGROUPNAME", ID: ddlGroups.SelectedValue, countryID: drpCountry.SelectedValue, PROJECTID: Drp_Project.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdAssignGroups.DataSource = ds;
                    grdAssignGroups.DataBind();
                }
                //drpCountry.SelectedIndex = 0;
                //Drp_Project.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

 

        protected void lbAddGroupExport_Click(object sender, EventArgs e)
        {
            try
            {
                Group_Master(header: "User Group Master", Action: "Get_Group");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbAssignProjGroupsExport_Click(object sender, EventArgs e)
        {
            try
            {
                Group_Master(header: "User Project Groups", Action: "Assign_Group_Project");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Group_Master(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
        {
            try
            {
                DataSet ds = new DataSet();
                DAL dal;
                dal = new DAL();
                ds = dal.Export_Data_SP(Action: Action, PRODUCTID: PRODUCT_ID, SUBCLASSID: SUBCLASS_ID);
                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



    }
}