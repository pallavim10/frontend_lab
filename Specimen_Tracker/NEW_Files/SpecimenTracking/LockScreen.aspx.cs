using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class LockScreen : System.Web.UI.Page
    {

        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CHECK_USER_EXIST();
                if (Request.Cookies["Username"] != null)
                {
                    lblUserName.Text = Server.HtmlEncode(Request.Cookies["Username"].Value);
                    lblFullName.Text = Server.HtmlEncode(Request.Cookies["FullName"].Value);

                }
                if (Request.Cookies["Password"] != null)
                {
                    txtPassword.Attributes["value"] = Request.Cookies["Password"].Value;
                }
            }
        }

        private void CHECK_USER_EXIST()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "CHECK_USER_EXIST");
                if (ds.Tables[0].Rows[0]["Count"].ToString() == "0")
                {
                    string script = @"
                         swal({
                         title: 'Success!',
                         text: 'Click OK to proceed with Super User registration.',
                         icon: 'success',
                         button: 'OK'
                        }).then((value) => {
                         window.location.href = 'RegisterSuperUser.aspx'; 
                    });";

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                string script = "";
                DataSet dsAuth = dal_UMT.UMT_Auth(UserID: lblUserName.Text, Pwd: txtPassword.Text);

                if (dsAuth.Tables.Count > 0 && dsAuth.Tables[0].Rows.Count > 0)
                {
                    string RESULT = dsAuth.Tables[0].Rows[0][0].ToString();
                    switch (RESULT)
                    {
                        case "Security question locked":
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Your security question has been locked. Please contact administrator.', 'warning');", true);
                            break;

                        case "Account Locked":
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Your account has been locked.', 'warning');", true);
                            break;

                        case "Invalid Credentials, Account Locked":
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid credentials, Your account has been locked.', 'warning');", true);
                            break;

                        case "Invalid Credentials":
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid credentials.', 'warning');", true);
                            break;

                        case "Invalid User ID":
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid User ID.', 'warning');", true);
                            break;

                        case "Account is Inactive":
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Your account has been deactivated.', 'warning');", true);
                            break;

                        case "First Login":
                            Session["User_ID"] = lblUserName.Text;
                            script = @"
                                    swal({
                                        title: 'Success!',
                                        text: 'Login successful, please change the password and set security question.',
                                        icon: 'success',
                                        button: 'OK'
                                    }).then((value) => {
                                        window.location.href = 'Change_Password.aspx'; 
                                    });";

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                            break;

                        case "Change Password":
                            Session["User_ID"] = lblUserName.Text;
                            script = @"
                                    swal({
                                        title: 'Warning!',
                                        text: 'Please change your password.',
                                        icon: 'warning',
                                        button: 'OK'
                                    }).then((value) => {
                                        window.location.href = 'Change_Password.aspx'; 
                                    });";

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);


                            break;

                        case "Set Security Question":
                            Session["User_ID"] = lblUserName.Text;
                            script = @"
                                    swal({
                                        title: 'Warning!',
                                        text: 'Please set your security question.',
                                        icon: 'warning',
                                        button: 'OK'
                                    }).then((value) => {
                                        window.location.href = 'SET_SecurityQue.aspx'; 
                                    });";

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);

                            break;

                        case "Password Expired":
                            Session["User_ID"] = lblUserName.Text;
                            script = @"
                                    swal({
                                        title: 'Warning!',
                                        text: 'Your password has been expired, please change the password.',
                                        icon: 'warning',
                                        button: 'OK'
                                    }).then((value) => {
                                        window.location.href = 'Change_Password.aspx'; 
                                    });";

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);

                            break;

                        default:
                            Session["User_ID"] = lblUserName.Text;
                            Get_UserDetails();
                            Response.Redirect("HomePage.aspx");

                            break;
                    }
                }
            }
          
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Get_UserDetails()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USER_DETAILS_SP(USERID: Session["User_ID"].ToString());

                DataRow dr = ds.Tables[0].Rows[0];

                Session["PROJECTID"] = dr["PROJECTID"].ToString();
                Session["PROJECTIDTEXT"] = dr["PROJECTIDTEXT"].ToString();

                Session["PWDExpire"] = dr["PWDExpire"].ToString();
                Session["NoofDays"] = dr["NoofDays"].ToString();
                Session["UserGroup_ID"] = dr["UserGroup_ID"].ToString().Trim();
                Session["UserGroupID"] = dr["UserGroupID"].ToString().Trim();
                Session["TimeZone_Value"] = dr["TimeZone_Value"].ToString().Trim();
                Session["TimeZone_Standard"] = dr["TimeZone_Standard"].ToString().Trim();
                Session["Unblind"] = dr["Unblind"].ToString().Trim();
                Session["Authentication_Title"] = dr["User_Name"].ToString().Trim();
                Session["Authentication_ID"] = dr["Email_ID"].ToString().Trim();
                Session["User_Name"] = dr["User_Name"].ToString().Trim();
                Session["USER_ID"] = dr["UserID"].ToString().Trim();
                Session["UserType"] = dr["UserType"].ToString().Trim();
                Session["StudyRole"] = dr["StudyRole"].ToString().Trim();


                Session["UMT"] = "YES";

                HttpCookie usernameCookie = new HttpCookie("WAI_Name");

                usernameCookie.Values["WAI_Name"] = Session["User_Name"].ToString();
                HttpContext.Current.Response.Cookies.Add(usernameCookie);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void lbtnLoginAnuser_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["Username"] != null)
            {
                Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-30);
                Response.Redirect("LoginPage.aspx");

            }
        }
    }
}