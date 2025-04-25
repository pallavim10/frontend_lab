using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CTMS;
using CTMS.CommonFunction;

namespace PPT
{
    public partial class Add_User_Profile : System.Web.UI.Page
    {

        DAL constr = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                fill_drpdwn_User_Group_ID();
                GETUSERDATA();
                GET_TIMEZONE();


            }
            Session["PVID"] = null;
        }

        private void fill_Proj_Name()
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.GetSetPROJECTDETAILS(
            Action: "Get_Specific_Project",
            Project_ID: Convert.ToInt32(Session["PROJECTID"]),
            ENTEREDBY: Session["User_ID"].ToString()
            );
            Drp_Project_Name.DataSource = ds;
            Drp_Project_Name.DataTextField = "PROJNAME";
            Drp_Project_Name.DataValueField = "Project_ID";
            Drp_Project_Name.DataBind();

            string USERGROUPID = "";
            foreach (ListItem item in lstUser_Group.Items)
            {
                if (item.Selected == true)
                {
                    if (USERGROUPID != "")
                    {
                        USERGROUPID += "," + item.Value.ToString();
                    }
                    else
                    {
                        USERGROUPID += item.Value.ToString();
                    }
                }
            }

            DataSet ds1 = dal.ManageUserGroups(
            ACTION: "BINDPROJECTBYGROUP",
            UserGroupID: USERGROUPID
            );

            lstProjects.DataSource = ds1;
            lstProjects.DataValueField = "Project_ID";
            lstProjects.DataTextField = "PROJNAME";
            lstProjects.DataBind();

            Drp_Project_Name.Items.Insert(0, new ListItem("--Select Project Name--", "0"));
            // Drp_Site_ID.Items.Insert(0, new ListItem("--Select Site ID--", "0"));
        }

        private void fill_drpdwn_User_Group_ID()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.ManageUserGroups(
                ACTION: "BindGroups"
                );
                //Drp_User_Group.DataSource = ds.Tables[0];
                //Drp_User_Group.DataTextField = "UserGroup_Name";
                //Drp_User_Group.DataValueField = "UserGroup_ID";
                //Drp_User_Group.DataBind();
                //Drp_User_Group.Items.Insert(0, new ListItem("--Select User Group--", "0"));

                lstUser_Group.DataSource = ds.Tables[0];
                lstUser_Group.DataTextField = "UserGroup_Name";
                lstUser_Group.DataValueField = "UserGroup_ID";
                lstUser_Group.DataBind();
                lstUser_Group.Items.Insert(0, new ListItem("--Select User Group--", "0"));

            }

            catch (Exception ex)
            {

                lblErrorMsg.Text = ex.Message;
            }
        }

        private void GetUserType()
        {
            try
            {
                DataSet ds = constr.AddUserProfile(Action: "GETEMPLOYEE");
                if (ds.Tables.Count > 0)
                {
                    drpUserType.DataSource = ds.Tables[0];
                    drpUserType.DataValueField = "UserType";
                    drpUserType.DataTextField = "UserType";
                    drpUserType.DataBind();

                    drpUserType.Items.Insert(0, new ListItem("--Select UserType--", "0"));
                }
            }
            catch (Exception ex)
            {

                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                string EmailAdd = "abc@abc.com";
                string CCEmailAddress = "HI";
                string E_Sub = "HI";
                string E_Body = "HI", PROJECTNAME = "";
                SqlConnection con = new SqlConnection();
                con = new SqlConnection(constr.getconstr());
                con.Open();

                if (Session["PROJECTIDTEXT"] != null)
                {
                    PROJECTNAME = Session["PROJECTIDTEXT"].ToString();
                }

                if (drpUserType.SelectedValue == "Internal")
                {
                    if (drpEmployee.SelectedValue == "0")
                    {
                        throw new Exception("Select Internal Employee");
                    }
                }

                DataSet ds = new DataSet();

                if (drpUserType.SelectedValue == "Investigator Team Member")
                {
                    ds = constr.AddUserProfile(Action: "INSERT",
                    User_Name: ddlInvestigatorTeamMem.SelectedItem.Text,
                    ENTEREDBY: Session["User_ID"].ToString(),
                    Email: txt_EmailID.Text,
                    User_Dis_Name: txt_User_Dis_Name.Text,
                    UserType: drpUserType.SelectedValue,
                    TIMEZONE: ddlTimeZone.SelectedValue,
                    MEDAUTH_FORM: chkForm.Checked,
                    MEDAUTH_FIELD: chkfield.Checked,
                    Unblind: ddlUnblind.SelectedValue,
                    Esource: Check_eSource.Checked,
                    safety: Check_Safety.Checked,
                    eCrf: Check_eCRF.Checked,
                    eSource_ReadOnly: Check_eSourceReadOnly.Checked
                    );

                }
                else
                {

                    ds = constr.AddUserProfile(Action: "INSERT",
                        User_Name: drpEmployee.SelectedItem.Text,
                        ENTEREDBY: Session["User_ID"].ToString(),
                        Email: txt_EmailID.Text,
                        User_Dis_Name: txt_User_Dis_Name.Text,
                        UserType: drpUserType.SelectedValue,
                        TIMEZONE: ddlTimeZone.SelectedValue,
                        MEDAUTH_FORM: chkForm.Checked,
                        MEDAUTH_FIELD: chkfield.Checked,
                        Unblind: ddlUnblind.SelectedValue,
                        Esource: Check_eSource.Checked,
                        safety: Check_Safety.Checked,
                        eCrf: Check_eCRF.Checked,
                        eSource_ReadOnly: Check_eSourceReadOnly.Checked
                        ) ;
                }

                string UserID = ds.Tables[0].Rows[0][0].ToString();

                if (UserID != "")
                {
                    if (Session["PROJECTID"] == null)
                    {
                        foreach (ListItem item in lstProjects.Items)
                        {
                            if (item.Selected == true)
                            {
                                foreach (ListItem item1 in lstUser_Group.Items)
                                {
                                    if (item1.Selected == true)
                                    {
                                        DataSet ds1 = constr.AddUserProfile(Action: "AddUserGroupWise_UserID",
                                        USERGROUPID: item1.Value,
                                        CNTRYID: drpCountry.SelectedValue,
                                        USERID: UserID,
                                        ENTEREDBY: Session["User_ID"].ToString(),
                                        Email: txt_EmailID.Text
                                        );

                                        DataSet ds4 = constr.AddUserProfile(Action: "AddUserProject",
                                           USERID: UserID,
                                           ENTEREDBY: Session["User_ID"].ToString(),
                                           UserType: drpUserType.SelectedValue,
                                           PROJECTID: item.Value
                                              );

                                        DataSet ds2 = constr.AddUserProfile(Action: "AddGroupFunctionToUser",
                                        USERGROUPID: item1.Value,
                                        PROJECTID: item.Value,
                                        USERID: UserID,
                                        ENTEREDBY: Session["User_ID"].ToString()
                                       );

                                        DAL dal = new DAL();

                                        DataSet ds3 = dal.DASHBOARD_ASSIGNING(Action: "Add_Dashboard_Data",
                                        USERGROUPID: item1.Value,
                                        PROJECTID: item.Value,
                                        USERID: UserID,
                                        ENTEREDBY: Session["User_ID"].ToString()
                                        );
                                    }
                                }
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Added successfully'); window.location='Add_User_Profile.aspx';", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void SendEmail(string User_Name, string Email_ID)
        {
            try
            {
                string EmailAdd = Email_ID;
                string CCEmailAddress = "";
                string E_Sub = "";
                string E_Body = "";
                CommonFunction commFun = new CommonFunction();
                SqlConnection con = new SqlConnection();
                con = new SqlConnection(constr.getconstr());

                string UID = "";
                string PWD = "";

                SqlCommand cmd3 = new SqlCommand();
                SqlDataReader myReader;

                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Connection = con;
                con.Open();

                cmd3.CommandText = "GetPassword";
                cmd3.Parameters.AddWithValue("@User_Name", User_Name);

                myReader = cmd3.ExecuteReader();

                while (myReader.Read())
                {
                    UID = myReader["User_ID"].ToString();
                    PWD = myReader["PWD"].ToString();
                }
                con.Close();

                E_Sub = "Newly Created IWRS Credentials";

                E_Body = "Hi " + User_Name + ", Your User ID : " + UID + " AND Password : " + PWD + " for IWRS.";

                commFun.Email_Users(EmailAdd, CCEmailAddress, E_Sub, E_Body);

                //Response.Write("<script> alert('Record Updated successfully.'); </script>");
            }
            catch (Exception ex)
            {

                lblErrorMsg.Text = ex.Message;
                throw;
            }
        }

        protected void Drp_Project_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BINDCOUNTRYBYGROUP();
                GetUserType();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        public void BINDCOUNTRYBYGROUP()
        {
            try
            {
                string PROJECTID = null;
                if (Session["PROJECTID"] != null)
                {
                    PROJECTID = Session["PROJECTID"].ToString();
                }
                else
                {
                    foreach (ListItem item in lstProjects.Items)
                    {
                        if (item.Selected == true)
                        {
                            if (PROJECTID != null)
                            {
                                PROJECTID += "," + item.Value.ToString();
                            }
                            else
                            {
                                PROJECTID += item.Value.ToString();
                            }
                        }
                    }
                }

                string USERGROUPID = "";
                foreach (ListItem item in lstUser_Group.Items)
                {
                    if (item.Selected == true)
                    {
                        if (USERGROUPID != null)
                        {
                            USERGROUPID += "," + item.Value.ToString();
                        }
                        else
                        {
                            USERGROUPID += item.Value.ToString();
                        }
                    }
                }

                DAL dal;
                dal = new DAL();
                DataSet ds = dal.ManageUserGroups(
                ACTION: "GET_COUNTRYBYGROUP",
                PROJECTID: PROJECTID,
                ENTEREDBY: Session["User_ID"].ToString(),
                UserGroupID: USERGROUPID
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
            }
        }

        protected void drpEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpEmployee.SelectedValue != "0")
                {
                    if (drpUserType.SelectedValue == "Internal Member")
                    {
                        DAL dal;
                        dal = new DAL();
                        DataSet ds = constr.AddUserProfile(Action: "GETDETAILS", EmpCode: drpEmployee.SelectedValue);
                        if (ds.Tables.Count > 0)
                        {
                            //txt_User_Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txt_EmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                            txt_User_Dis_Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                        }
                    }
                    else if (drpUserType.SelectedValue == "Sponser Team Member")
                    {
                        DAL dal;
                        dal = new DAL();
                        DataSet ds = constr.AddUserProfile(Action: "GETSponsorTeamDETAILS", EmpCode: drpEmployee.SelectedValue);
                        if (ds.Tables.Count > 0)
                        {
                            //txt_User_Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txt_EmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                            txt_User_Dis_Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                        }
                    }
                    else if (drpUserType.SelectedValue == "Investigator Team Member")
                    {
                        DataSet ds = constr.AddUserProfile(Action: "GETEMPLOYEE", INVID: drpEmployee.SelectedValue);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlInvestigatorTeamMem.DataSource = ds.Tables[4];
                            ddlInvestigatorTeamMem.DataTextField = "Name";
                            ddlInvestigatorTeamMem.DataValueField = "ID";
                            ddlInvestigatorTeamMem.DataBind();
                            ddlInvestigatorTeamMem.Items.Insert(0, new ListItem("--Select Investigator Team Member--", "0"));

                            lstINVID.Items.Clear();
                            ds = constr.AddUserProfile(Action: "GETINVIDDETAILS", INVID: drpEmployee.SelectedValue);
                            lstINVID.DataSource = ds.Tables[0];
                            lstINVID.DataValueField = "INVID";
                            lstINVID.DataTextField = "INVID";
                            lstINVID.DataBind();

                            string[] INVID = ds.Tables[0].Rows[0]["INVID"].ToString().Split(',').ToArray();
                            lstINVID.ClearSelection();
                            if (INVID != null && INVID.Length > 0)
                            {
                                for (int i = 0; i < INVID.Length; i++)
                                {
                                    if (INVID[i] != "")
                                    {
                                        ListItem itm1 = lstINVID.Items.FindByText(INVID[i]);
                                        if (itm1 != null)
                                            itm1.Selected = true;
                                    }
                                }
                            }
                        }
                    }
                    else if (drpUserType.SelectedValue == "Investigator")
                    {
                        DataSet ds = constr.AddUserProfile(Action: "GETINVESTIGATORDETAILS", INVID: drpEmployee.SelectedValue);
                        if (ds.Tables.Count > 0)
                        {
                            //txt_User_Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txt_EmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                            txt_User_Dis_Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();

                            lstINVID.Items.Clear();
                            ds = constr.AddUserProfile(Action: "GETINVIDDETAILS", INVID: drpEmployee.SelectedValue);
                            lstINVID.DataSource = ds.Tables[0];
                            lstINVID.DataValueField = "INVID";
                            lstINVID.DataTextField = "INVID";
                            lstINVID.DataBind();

                            string[] INVID = ds.Tables[0].Rows[0]["INVID"].ToString().Split(',').ToArray();
                            lstINVID.ClearSelection();
                            if (INVID != null && INVID.Length > 0)
                            {
                                for (int i = 0; i < INVID.Length; i++)
                                {
                                    if (INVID[i] != "")
                                    {
                                        ListItem itm1 = lstINVID.Items.FindByText(INVID[i]);
                                        if (itm1 != null)
                                            itm1.Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ddlInvestigatorTeamMem.Items.Clear();
                    txt_EmailID.Text = "";
                    txt_User_Dis_Name.Text = "";
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void drpUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string PROJECTID = null;
                if (Session["PROJECTID"] != null)
                {
                    PROJECTID = Session["PROJECTID"].ToString();
                }
                else
                {
                    foreach (ListItem item in lstProjects.Items)
                    {
                        if (item.Selected == true)
                        {
                            if (PROJECTID != null)
                            {
                                PROJECTID += "," + item.Value.ToString();
                            }
                            else
                            {
                                PROJECTID += item.Value.ToString();
                            }
                        }
                    }
                }
                DataSet ds = new DataSet();
                div1.Visible = false;
                div2.Visible = false;
                if (drpUserType.SelectedValue == "Internal Member")
                {
                    ds = constr.AddUserProfile(Action: "GETEMPLOYEE");
                    if (ds.Tables.Count > 1)
                    {
                        drpEmployee.DataSource = ds.Tables[1];
                        drpEmployee.DataValueField = "EmpCode";
                        drpEmployee.DataTextField = "Name";
                        drpEmployee.DataBind();
                        drpEmployee.Items.Insert(0, new ListItem("--Select Employee--", "0"));
                        divinternalemp.InnerText = "Select Internal Employee:";
                        txt_User_Dis_Name.Text = "";
                        txt_EmailID.Text = "";
                    }
                }
                else if (drpUserType.SelectedValue == "Sponser Team Member")
                {
                    ds = constr.AddUserProfile(Action: "GETEMPLOYEE");
                    if (ds.Tables.Count > 1)
                    {
                        drpEmployee.Items.Clear();
                        drpEmployee.DataSource = ds.Tables[2];
                        drpEmployee.DataValueField = "ID";
                        drpEmployee.DataTextField = "Name";
                        drpEmployee.DataBind();
                        drpEmployee.Items.Insert(0, new ListItem("--Select Sponser Team Member--", "0"));
                        divinternalemp.InnerText = "Select Sponser Team Member:";
                        txt_User_Dis_Name.Text = "";
                        txt_EmailID.Text = "";
                    }
                }
                else if (drpUserType.SelectedValue == "Investigator Team Member")
                {
                    ds = constr.AddUserProfile(Action: "GETEMPLOYEE", PROJECTID: PROJECTID);
                    if (ds.Tables.Count > 1)
                    {
                        drpEmployee.Items.Clear();
                        drpEmployee.DataSource = ds.Tables[3];
                        drpEmployee.DataValueField = "ID";
                        drpEmployee.DataTextField = "Name";
                        drpEmployee.DataBind();
                        drpEmployee.Items.Insert(0, new ListItem("--Select Investigator Team Member--", "0"));
                        divinternalemp.InnerText = "Select Investigator Name:";
                        div1.Visible = true;
                        div2.Visible = true;
                        txt_User_Dis_Name.Text = "";
                        txt_EmailID.Text = "";
                    }
                }
                else if (drpUserType.SelectedValue == "Investigator")
                {
                    ds = constr.AddUserProfile(Action: "GETEMPLOYEE", PROJECTID: PROJECTID);
                    if (ds.Tables.Count > 1)
                    {
                        drpEmployee.Items.Clear();
                        drpEmployee.DataSource = ds.Tables[3];
                        drpEmployee.DataValueField = "ID";
                        drpEmployee.DataTextField = "Name";
                        drpEmployee.DataBind();
                        drpEmployee.Items.Insert(0, new ListItem("--Select Investigator Name--", "0"));
                        divinternalemp.InnerText = "Select Investigator Name:";
                        txt_User_Dis_Name.Text = "";
                        txt_EmailID.Text = "";
                    }
                }
                else
                {
                    drpEmployee.Items.Clear();
                    ddlInvestigatorTeamMem.Items.Clear();
                    txt_User_Dis_Name.Text = "";
                    txt_EmailID.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstUser_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_Proj_Name();
                drpCountry.Items.Clear();
                drpUserType.Items.Clear();
                drpEmployee.Items.Clear();
                txt_EmailID.Text = "";
                txt_User_Dis_Name.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void lstProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BINDCOUNTRYBYGROUP();
                GetUserType();
                txt_EmailID.Text = "";
                txt_User_Dis_Name.Text = "";
                drpEmployee.Items.Clear();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void ddlInvestigatorTeamMem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlInvestigatorTeamMem.SelectedValue != "0")
                {
                    DataSet ds = constr.AddUserProfile(Action: "GETInvTeamDETAILS", EmpCode: ddlInvestigatorTeamMem.SelectedValue);
                    if (ds.Tables.Count > 0)
                    {
                        txt_EmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                        txt_User_Dis_Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    }
                }
                else
                {
                    txt_User_Dis_Name.Text = "";
                    txt_EmailID.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            lstUser_Group.Items.Clear();
            lstProjects.Items.Clear();
            drpCountry.Items.Clear();
            drpUserType.Items.Clear();
            drpEmployee.Items.Clear();
            ddlInvestigatorTeamMem.Items.Clear();
            txt_EmailID.Text = "";
            txt_User_Dis_Name.Text = "";
            ddlTimeZone.SelectedIndex = 0;
        }

        private void GETUSERDATA()
        {
            try
            {
                DataSet ds = constr.AddUserProfile(Action: "GETUSERDATA");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdUserDetails.DataSource = ds;
                    grdUserDetails.DataBind();
                }
                else
                {
                    grdUserDetails.DataSource = null;
                    grdUserDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUserDetails_PreRender(object sender, EventArgs e)
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

        protected void GET_TIMEZONE()
        {
            try
            {
                DataSet ds = constr.AddUserProfile(Action: "GETTIMEZONE");

                ddlTimeZone.DataSource = ds;
                ddlTimeZone.DataValueField = "ID";
                ddlTimeZone.DataTextField = "TimeZone";
                ddlTimeZone.DataBind();
                ddlTimeZone.Items.Insert(0, new ListItem("--Select TimeZone--", "0"));
                ddlTimeZone.SelectedValue = "87";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbUserDetailsExport_Click(object sender, EventArgs e)
        {
            try
            {
                USER_DETAILS(header: "User Profile", Action: "GET_USER_DETAILS");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void USER_DETAILS(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
        {
            try
            {
                DataSet ds = new DataSet();
                DAL dal = new DAL();
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