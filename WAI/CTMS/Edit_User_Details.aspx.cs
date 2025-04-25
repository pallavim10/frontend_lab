using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Net;

namespace PPT
{
    public partial class Edit_User_Details : System.Web.UI.Page
    {
        DAL constr = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                fill_drpdwn_User_ID();
                fill_drpdwn_User_Group_ID();
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

                //Drp_User_Group1.DataSource = ds.Tables[0];
                //Drp_User_Group1.DataValueField = "UserGroup_ID";
                //Drp_User_Group1.DataTextField = "UserGroup_Name";
                //Drp_User_Group1.DataBind();
                //Drp_User_Group1.Items.Insert(0, new ListItem("--Select User Group--", "0"));

                lstUser_Group.DataSource = ds.Tables[0];
                lstUser_Group.DataTextField = "UserGroup_Name";
                lstUser_Group.DataValueField = "UserGroup_ID";
                lstUser_Group.DataBind();
                lstUser_Group.Items.Insert(0, new ListItem("--Select User Group--", "0"));
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void fill_drpdwn_User_ID()
        {
            try
            {
                DataSet ds = constr.ManageUserGroups(ACTION: "GETUSERASPERPROJECT");
                Drp_User.DataSource = ds.Tables[0];
                Drp_User.DataTextField = "User_Name";
                Drp_User.DataValueField = "User_ID";
                Drp_User.DataBind();
                Drp_User.Items.Insert(0, new ListItem("--Select User--", "0"));
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void lstUser_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_Proj_Name();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void Drp_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                lblErrorMsg.Text = "";

                if (Drp_User.SelectedValue != "0")
                {

                    SqlConnection con = new SqlConnection(constr.getconstr());
                    SqlCommand cmd = new SqlCommand("Get_User_Details");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@USERID", Drp_User.SelectedValue);


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        txt_User_Name.Text = sdr["User_Name"].ToString();
                        txt_User_Dis_Name.Text = sdr["User_Dis_Name"].ToString();
                        txt_EmailID.Text = sdr["Email_ID"].ToString();
                        Session["USERTYPE"] = sdr["User_Type"].ToString();

                        if (sdr["TIMEZONE"].ToString() != "")
                        {
                            ddlTimeZone.SelectedValue = sdr["TIMEZONE"].ToString();
                        }


                        if (sdr["SignOff_eSource"].ToString() == "True")
                        {
                            Check_eSource.Checked = true;
                        }
                        else
                        {
                            Check_eSource.Checked = false;
                        }



                        if (sdr["SignOff_Safety"].ToString() == "True")
                        {
                            Check_Safety.Checked = true;
                        }
                        else
                        {
                            Check_Safety.Checked = false;
                        }



                        if (sdr["SignOff_eCRF"].ToString() == "True")
                        {
                            Check_eCRF.Checked = true;
                        }
                        else
                        {
                            Check_eCRF.Checked = false;
                        }
                        if (sdr["MEDAUTH_FORM"].ToString() == "True")
                        {
                            chkForm.Checked = true;
                        }
                        else
                        {
                            chkForm.Checked = false;
                        }

                        if (sdr["MEDAUTH_FIELD"].ToString() == "True")
                        {
                            chkfield.Checked = true;
                        }
                        else
                        {
                            chkfield.Checked = false;
                        }

                        if (sdr["Unblind"].ToString() != "0" && sdr["Unblind"].ToString() != "")
                        {
                            ddlUnblind.SelectedValue = sdr["Unblind"].ToString();
                        }
                        else
                        {
                            ddlUnblind.SelectedValue = "0";
                        }

                        if (sdr["eSource_ReadOnly"].ToString() == "True")
                        {
                            Check_eSourceReadOnly.Checked = true;
                        }
                        else
                        {
                            Check_eSourceReadOnly.Checked = false;
                        }
                    }
                    con.Close();

                    DataSet ds2 = constr.ManageUserGroups(
                  ACTION: "BINDGROUPBYUSER",
                  User_ID: Drp_User.SelectedValue
                  );

                    lstUser_Group.ClearSelection();
                    if (ds2.Tables[0] != null && ds2.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                        {
                            if (ds2.Tables[0].Rows.Count != 0)
                            {
                                ListItem itm = lstUser_Group.Items.FindByValue(ds2.Tables[0].Rows[i]["UserGroup_ID"].ToString());
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }

                    fill_Proj_Name();

                    DataSet ds1 = constr.ManageUserGroups(
                    ACTION: "BINDPROJECTBYUSER",
                    User_ID: Drp_User.SelectedValue
                    );

                    lstProjects.ClearSelection();
                    if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            if (ds1.Tables[0].Rows.Count != 0)
                            {
                                ListItem itm = lstProjects.Items.FindByValue(ds1.Tables[0].Rows[i]["Project_ID"].ToString());
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    txt_EmailID.Text = "";
                    txt_User_Dis_Name.Text = "";
                    txt_User_Name.Text = "";
                    lstUser_Group.Items.Clear();
                    lstProjects.Items.Clear();
                    ddlTimeZone.SelectedIndex = 0;

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(constr.getconstr());
                SqlCommand cmd = new SqlCommand("Update_User_Details");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "Update");
                cmd.Parameters.AddWithValue("@User_Name", txt_User_Name.Text);
                cmd.Parameters.AddWithValue("@User_Dis_Name", txt_User_Dis_Name.Text);
                cmd.Parameters.AddWithValue("@Email_ID", txt_EmailID.Text);
                cmd.Parameters.AddWithValue("@USERID", Drp_User.SelectedValue);
                cmd.Parameters.AddWithValue("@TIMEZONE", ddlTimeZone.SelectedValue);
                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", chkForm.Checked);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", chkfield.Checked);
                cmd.Parameters.AddWithValue("@Unblind", ddlUnblind.SelectedValue);
               // cmd.Parameters.AddWithValue("@CHANGEBY", Session["User_ID"].ToString());
                cmd.Parameters.AddWithValue("@SignOff_eSource", Check_eSource.Checked);
                cmd.Parameters.AddWithValue("@SignOff_Safety", Check_Safety.Checked);
                cmd.Parameters.AddWithValue("@SignOff_eCRF", Check_eCRF.Checked);
                cmd.Parameters.AddWithValue("@eSource_ReadOnly", Check_eSourceReadOnly.Checked);
                cmd.ExecuteNonQuery();
                con.Close();

                string PROJECTID = null;
                if (Session["PROJECTID"] != null)
                {
                    PROJECTID = Session["PROJECTID"].ToString();
                }
                else
                {
                    if (divProject.Visible == true)
                    {
                        con = new SqlConnection(constr.getconstr());
                        cmd = new SqlCommand("Add_User_Profile");
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@Action", "DELETEUSERPROJECTS");
                        cmd.Parameters.AddWithValue("@USERID", Drp_User.SelectedValue);
                        cmd.ExecuteNonQuery();
                        con.Close();
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
                                        USERID: Drp_User.SelectedValue,
                                        ENTEREDBY: Session["User_ID"].ToString(),
                                        Email: txt_EmailID.Text,
                                        PROJECTID: item.Value,
                                        IPADDRESS: GetIpAddress()
                                        );

                                        DataSet ds4 = constr.AddUserProfile(Action: "AddUserProject",
                                        USERID: Drp_User.SelectedValue,
                                        ENTEREDBY: Session["User_ID"].ToString(),
                                        PROJECTID: item.Value,
                                        UserType: Session["USERTYPE"].ToString(),
                                         IPADDRESS: GetIpAddress()

                                            );

                                        DataSet ds2 = constr.AddUserProfile(Action: "AddGroupFunctionToUser",
                                       USERGROUPID: item1.Value,
                                       PROJECTID: item.Value,
                                       USERID: Drp_User.SelectedValue,
                                       ENTEREDBY: Session["User_ID"].ToString(),
                                       IPADDRESS : GetIpAddress()
                                      );

                                        DAL dal = new DAL();

                                        DataSet ds3 = dal.DASHBOARD_ASSIGNING(Action: "Update_Dashboard_Data",
                                        USERGROUPID: item1.Value,
                                        PROJECTID: item.Value,
                                        USERID: Drp_User.SelectedValue,
                                        ENTEREDBY: Session["User_ID"].ToString()
                                        );
                                    }
                                }
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Updated successfully'); window.location='Edit_User_Details.aspx';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Drp_Project_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //fill_drpdwn_User_ID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        //protected void Chk_Select_All_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Int16 i;
        //        if (Chk_Select_All.Checked)
        //        {
        //            for (i = 0; i < grdINV1.Rows.Count; i++)
        //            {
        //                CheckBox ChAction;
        //                ChAction = (CheckBox)grdINV1.Rows[i].FindControl("Chk_Sel_Fun");
        //                ChAction.Checked = true;
        //            }
        //        }
        //        else
        //        {
        //            for (i = 0; i < grdINV1.Rows.Count; i++)
        //            {
        //                CheckBox ChAction;
        //                ChAction = (CheckBox)grdINV1.Rows[i].FindControl("Chk_Sel_Fun");
        //                ChAction.Checked = false;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = "";
        //        lblErrorMsg.Text = ex.Message;
        //    }
        //}

        protected void grdINV1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                string Present = dr["Present"].ToString();
                if (Present != "0")
                {
                    CheckBox btnEdit = (CheckBox)e.Row.FindControl("Chk_Sel_Fun");
                    btnEdit.Checked = true;
                }
            }
        }

        //protected void lstProjects_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        fill_drpdwn_Site_ID();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //        throw;
        //    }
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            Drp_User.SelectedIndex = 0;
            lstProjects.Items.Clear();
            lstUser_Group.Items.Clear();
            lstProjects.Items.Clear();
            txt_EmailID.Text = "";
            txt_User_Dis_Name.Text = "";
            txt_User_Name.Text = "";
            ddlTimeZone.SelectedIndex = 0;
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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string ip_add = "";
            foreach (var ipp in host.AddressList)
            {
                if (ipp.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip_add = ipp.ToString();
                }
            }
            return ip_add;
        }


    }
}