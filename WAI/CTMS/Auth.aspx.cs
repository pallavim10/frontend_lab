using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace PPT
{
    public partial class Auth : System.Web.UI.Page
    {
        DAL constr = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            //this.Rotator.Text = "<FONT SIZE='2' FACE='Arial' COLOR=Black><MARQUEE SCROLLAMOUNT=6 WIDTH=75% BEHAVIOR=SCROLL BGColor=White><b>" + "DiagnoSearch Life Sciences Pvt Ltd. has Operations In India,South Korea, Thailand, Taiwan, Malaysia, Mexico & US" + "</b></MARQUEE></FONT>";
            if (hdn.Value != "1")
            {
                Session.Clear();
            }
            //if (Session["User_ID"] != null)
            //{
            //    SqlConnection con = new SqlConnection(constr.getconstr());
            //    SqlCommand cmd3 = new SqlCommand();
            //    cmd3.CommandType = CommandType.StoredProcedure;
            //    cmd3.Connection = con;
            //    cmd3.CommandText = "Update_Alrdy_Log_IN";
            //    con.Open();
            //    cmd3.Parameters.AddWithValue("@Action", "LogOut");
            //    cmd3.Parameters.AddWithValue("@User_ID", Session["User_ID"].ToString());
            //    cmd3.ExecuteNonQuery();
            //    con.Close();
            //    Session["User_ID"] = null;
            //}

        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {

                System.Web.HttpBrowserCapabilities browser = Request.Browser;
                string BrowserName = browser.Browser.ToString();
                float BrowserVersion = float.Parse(browser.Version);

                if (Request.Browser.Browser == "InternetExplorer")
                {
                    //   if (BrowserVersion < 9)
                    // {
                    throw new Exception("You are using Internet Explorer, This Application will work on Google Chrome");
                    // }
                }
                ViewState["USERNAME"] = this.txt_UserName.Text;
                ViewState["Password"] = this.txt_Pwd.Text;
                //string Host_IP = Request.UserHostAddress.ToString();
                string Host_IP = GetMACAddress();
                SqlConnection con = new SqlConnection();
                //if (Session["CHILD_CONN"] != null)
                //{
                //    con = new SqlConnection(constr.getconstrCHILD());
                //}
                //else
                //{
                //    con = new SqlConnection(constr.getconstr());
                //}
                con = new SqlConnection(constr.getconstr());
                Session["MASTERDBNAME"] = con.Database.ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "User_Auth";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", this.txt_UserName.Text);
                cmd.Parameters.AddWithValue("@Pwd", this.txt_Pwd.Text);
                cmd.Parameters.AddWithValue("@Host_IP", Host_IP);

                var returnParam1 = new SqlParameter
                {
                    ParameterName = "@Result",
                    Direction = ParameterDirection.Output,
                    Size = 1
                };
                cmd.Parameters.Add(returnParam1);
                cmd.ExecuteNonQuery();
                string retunvalue = (string)cmd.Parameters["@Result"].Value;
                con.Close();

                if (retunvalue == "1")
                {

                    throw new Exception("Invalid Credentials");
                }
                else if (retunvalue == "2")
                {

                    throw new Exception("User Account has been Locked.");
                }
                else if (retunvalue == "3")
                {
                    Session["User_ID"] = this.txt_UserName.Text;
                    Response.Redirect("Change_Password.aspx");
                }
                else if (retunvalue == "4")
                {
                    Session["User_ID"] = this.txt_UserName.Text;
                    Response.Redirect("Change_Password.aspx");
                }

                else if (retunvalue == "5")
                {
                    throw new Exception("Invalid Credentials");
                    //divLogin.Visible = false;
                    //divProject.Visible = true;
                    //fill_Project();
                    //lblErrorMsg.Text = "";
                }

                else if (retunvalue == "6")
                {
                    throw new Exception("User Has Already been Logged IN");
                }

                else if (retunvalue == "7")
                {
                    throw new Exception("User is Deactivated");
                }
                else if (retunvalue == "0")
                {
                    Session["User_ID"] = this.txt_UserName.Text;

                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Connection = con;
                    cmd3.CommandText = "Update_Alrdy_Log_IN";
                    con.Open();
                    cmd3.Parameters.AddWithValue("@Action", "LogIn");
                    cmd3.Parameters.AddWithValue("@User_ID", Session["User_ID"].ToString());
                    cmd3.ExecuteNonQuery();
                    con.Close();


                    con.Open();
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "PWD_EXP_Reminder";
                    cmd2.Connection = con;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@User_ID", this.txt_UserName.Text);
                    SqlDataReader myReader1;
                    myReader1 = cmd2.ExecuteReader();

                    while (myReader1.Read())
                    {
                        Session["PWDExpire"] = myReader1["Result"].ToString();
                        Session["NoofDays"] = myReader1["NoofDays"].ToString();
                        Session["UserGroup_ID"] = myReader1["UserGroup_ID"].ToString().Trim();
                        Session["UserGroupID"] = myReader1["UserGroupID"].ToString().Trim();
                        Session["TimeZone_Value"] = myReader1["TimeZone_Value"].ToString().Trim();
                        Session["TimeZone_Standard"] = myReader1["TimeZone_Standard"].ToString().Trim();
                        Session["MEDAUTH_FORM"] = myReader1["MEDAUTH_FORM"].ToString().Trim();
                        Session["MEDAUTH_FIELD"] = myReader1["MEDAUTH_FIELD"].ToString().Trim();
                        Session["Unblind"] = myReader1["Unblind"].ToString().Trim();

                        Session["Authentication_Title"] = myReader1["User_Name"].ToString().Trim();
                        Session["Authentication_ID"] = myReader1["Email_ID"].ToString().Trim();

                        Session["SignOff_eSource"] = myReader1["SignOff_eSource"].ToString().Trim();
                        Session["SignOff_Safety"] = myReader1["SignOff_Safety"].ToString().Trim();
                        Session["SignOff_eCRF"] = myReader1["SignOff_eCRF"].ToString().Trim();

                        Session["UserType"] = myReader1["UserType"].ToString().Trim();

                        Session["User_Name"] = myReader1["User_Name"].ToString().Trim();
                    }
                    con.Close();

                    if (txt_UserName.Text == "100")
                    {
                        Session["MasterAdminLogin"] = 1;
                        Response.Redirect("Master_HomePage.aspx", false);
                    }
                    else
                    {
                        divLogin.Visible = false;
                        divProject.Visible = true;
                        fill_Project();
                        lblErrorMsg.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {


                //Session["PROJECTID"] = null;

                //for checklist session
                Session["MonitoringINVID"] = null;
                Session["MonitoringMVID"] = null;
                Session["InitiationINVID"] = null;
                Session["InitiationMVID"] = null;
                Session["QualificationINVID"] = null;
                Session["QualificationMVID"] = null;
                Session["CloseOutINVID"] = null;
                Session["CloseOutMVID"] = null;
                //for checklist session


                Session["PROJECTID"] = drp_Project.SelectedItem.Value;
                Session["PROJECTIDTEXT"] = drp_Project.SelectedItem.Text;
                Session.Remove("MasterAdminLogin");



                string con1 = constr.getconstr();
                string[] parts = con1.Split(';');
                for (int i = 0; i < parts.Length; i++)
                {
                    string part = parts[i].Trim();
                    if (part.StartsWith("Data Source="))
                    {
                        Session["dataSource"] = part.Replace("Data Source=", "");

                    }
                    if (part.StartsWith("Initial Catalog="))
                    {
                        Session["InitialCatalog"] = part.Replace("Initial Catalog=", "");
                    }
                    if (part.StartsWith("Integrated Security="))
                    {
                        Session["IntegratedSecurity"] = part.Replace("Integrated Security=", "");

                    }
                    if (part.StartsWith("User ID="))
                    {
                        Session["UserID"] = part.Replace("User ID=", "");

                    }
                    if (part.StartsWith("Password="))
                    {
                        Session["Password"] = part.Replace("Password=", "");

                    }
                }

                DataSet ds1 = constr.ManageUserGroups(ACTION: "GET_SUBJECT_FROM_USER", User_ID: txt_UserName.Text);
                if (ds1.Tables[0].Rows[0]["ePRO_SUBJECTS"].ToString() != "")
                {
                    Session["ePRO_SUBJECTS"] = ds1.Tables[0].Rows[0]["ePRO_SUBJECTS"].ToString();
                }

                DataSet ds = constr.ManageUserGroups(ACTION: "CHECKUSERAUTH", User_ID: txt_UserName.Text, PROJECTID: drp_Project.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataSet dsProj = constr.GetSetPROJECTDETAILS(Action: "Select_Proj", Project_ID: Convert.ToInt32(drp_Project.SelectedItem.Value.ToString()));

                    if (dsProj.Tables.Count > 0)
                    {
                        if (dsProj.Tables[0].Rows.Count > 0)
                        {
                            Session["SPONSORNAME"] = dsProj.Tables[0].Rows[0]["SPONSOR"].ToString();
                        }
                    }

                    if (Request.QueryString["Type"] != null)
                    {
                        if (Request.QueryString["Type"] == "UNFREEZE")
                        {
                            Response.Redirect("DM_Approve_UnFreeze.aspx");
                        }
                        else if (Request.QueryString["Type"] == "UNLOCK")
                        {
                            Response.Redirect("DM_Approve_UnLock.aspx");
                        }
                        else
                        {
                            Response.Redirect("HomePage.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("HomePage.aspx");
                    }
                }
                else
                {
                    Response.Write("<script> alert('Your Login Permission has Expired,Contact Administrator to give Permission.');window.location='Auth.aspx'; </script>");
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
                DataSet ds = dal.ManageUserGroups(
                ACTION: "BINDPROJECTBYUSERLOGINDDL",
                User_ID: Session["User_ID"].ToString()
                );
                drp_Project.DataSource = ds.Tables[0];
                drp_Project.DataValueField = "Project_ID";
                drp_Project.DataTextField = "PROJNAME";
                drp_Project.DataBind();
                drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
                hdn.Value = "1";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void ddlproject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DAL dal = new DAL();
                DataTable dt = dal.getconstring(ACTION: "GETCONN", PROJECTID: drp_Project.SelectedValue);
                Session["CHILD_CONN"] = dt.Rows[0]["ConnectionString"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}
