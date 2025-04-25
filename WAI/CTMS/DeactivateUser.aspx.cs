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
    public partial class DeactivateUser : System.Web.UI.Page
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
            fill_drpdwn_User_ID();
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

                        //hidden field values//
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
                DAL dal = new DAL();
                DataSet ds = new DataSet();
                ds = dal.User_Activation_Deactivation(
                Action: "Deactivation",
                User_Name: hnfUserName.Value,
                User_ID: hnfUserID.Value,
                ENTEREDBY: Session["User_ID"].ToString()
                );

                SendEmail();


                //Response.Write("<script> alert('User Deactivated successfully.');window.location='DeactivateUser.aspx'; </script>");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('User Deactivated successfully'); window.location='DeactivateUser.aspx';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Drp_Project_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_drpdwn_User_Group_ID();
        }


        protected void SendEmail()
        {
            try
            {

                string EmailAdd = "helpdesk@diagnosearch.com";
                string CCEmailAddress = "";
                string E_Sub = "";
                string E_Body = "";
                CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();

                SqlConnection con = new SqlConnection();
                con = new SqlConnection(constr.getconstr());
                DataSet ds = new DataSet();

                SqlDataAdapter adp;
                SqlCommand cmd5 = new SqlCommand();
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.Connection = con;
                con.Open();
                cmd5.CommandText = "GET_PROJECT_EMAILS";
                cmd5.Parameters.AddWithValue("@Project_ID", Drp_Project_Name.SelectedValue);
                cmd5.Parameters.AddWithValue("@Email_Type", "UserAct");
                adp = new SqlDataAdapter(cmd5);
                adp.Fill(ds);
                cmd5.Dispose();
                CCEmailAddress = ds.Tables[0].Rows[0]["E_CC"].ToString();
                con.Close();

                EmailAdd = txt_EmailID.Text;
                E_Sub = "Protocol Id - " + Drp_Project_Name.SelectedItem.Text.ToString() + " : User Deactivation";
                E_Body = "User: " + hnfUserDispName.Value + " is Deactivated.";
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