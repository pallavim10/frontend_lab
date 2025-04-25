using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using PPT;

namespace CTMS
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        DAL constr = new DAL();

        CommonFunction.CommonFunction CF = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                fill_Proj_Name();
                fill_drpdwn_User_Group_ID();
            }
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

        private void fill_drpdwn_User_Group_ID()
        {
            try
            {
                SqlConnection con = new SqlConnection(constr.getconstr());
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
                Drp_User_Group.Items.Insert(0, new ListItem("--Select User Group--", "0"));
                con.Close();
                lblErrorMsg.Text = "";
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
                lblErrorMsg.Text = "";

                SqlConnection con = new SqlConnection(constr.getconstr());
                SqlCommand cmd = new SqlCommand("Get_User_ID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_ID", Drp_Project_Name.SelectedValue);
                cmd.Parameters.AddWithValue("@UserGroup_ID", Drp_User_Group.SelectedValue);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                Drp_User.DataSource = ds.Tables[0];
                Drp_User.DataTextField = "User_Name";
                Drp_User.DataValueField = "User_ID";
                Drp_User.DataBind();
                con.Close();
                Drp_User.Items.Insert(0, new ListItem("--Select User--", "0"));
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Drp_User_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_drpdwn_User_ID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                    cmd.Parameters.AddWithValue("@User_Name", Drp_User.SelectedItem.Text);


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        txt_User_Name.Text = sdr["User_Name"].ToString();
                        //txt_User_Dis_Name.Text = sdr["User_Dis_Name"].ToString();
                        txt_EmailID.Text = sdr["Email_ID"].ToString();
                        //  Drp_User_Group1.SelectedValue = sdr["UserGroup_Name"].ToString();
                        hnfUserName.Value = sdr["User_Name"].ToString();
                        hnfEmailID.Value = sdr["Email_ID"].ToString();
                        hnfUserType.Value = sdr["User_Type"].ToString();
                        hnfUserDispName.Value = sdr["User_Dis_Name"].ToString();
                        hnfUserID.Value = sdr["User_ID"].ToString();
                        hnfSiteID.Value = sdr["INVID"].ToString();
                    }


                    con.Close();
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

                string UserName, EmailID;
                UserName = hnfUserName.Value;
                EmailID = hnfEmailID.Value;

                DAL dal = new DAL();
                DataSet ds = new DataSet();
                ds = dal.User_Activation_Deactivation(
                Action: "ResetPassword",
                User_Name: hnfUserName.Value,
                User_ID: hnfUserID.Value, 
                ENTEREDBY: Session["User_ID"].ToString());

                SendEmail(UserName, EmailID);

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Password Resend Successfully'); window.location='ResetPassword.aspx';", true);
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
                fill_drpdwn_User_Group_ID();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void SendEmail(string User_Name, string Email_ID)
        {
            try
            {
                string EmailAdd = "helpdesk@diagnosearch.com";
                string CCEmailAddress = "";
                string E_Sub = "";
                string E_Body = "";
                CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();

                SqlConnection con = new SqlConnection(constr.getconstr());

                string UID = "";
                string PWD = "";

                SqlCommand cmd3 = new SqlCommand();
                con = new SqlConnection(constr.getconstr());

                //cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Connection = con;
                con.Open();



                DataSet ds = new DataSet();

                SqlDataAdapter adp;

                //Get Data
                cmd3 = new SqlCommand("Get_UID_PWD", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@User_Name", User_Name);

                adp = new SqlDataAdapter(cmd3);
                adp.Fill(ds);
                cmd3.Dispose();
                DataRow dr = ds.Tables[1].Rows[0];
                UID = dr["User_ID"].ToString();
                PWD = dr["PWD"].ToString();
                con.Close();

                EmailAdd = Email_ID;
                E_Sub = "Protocol Id - " + Drp_Project_Name.SelectedItem.Text.ToString() + " : User ID Resend";
                E_Body = "Hi " + Drp_User.SelectedItem.Text + ", Your User ID: " + UID + " User Role : " + Drp_User_Group.SelectedItem.Text + "";
                commFun.Email_Users(EmailAdd, CCEmailAddress, E_Sub, E_Body);

                //Send PWD in Email
                EmailAdd = Email_ID;
                E_Sub = "Protocol Id - " + Drp_Project_Name.SelectedItem.Text.ToString() + " : Password Resend";
                E_Body = "Hi, Your Login Password: " + PWD + " ";
                commFun.Email_Users(EmailAdd, CCEmailAddress, E_Sub, E_Body);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;

            }
        }
    }
}