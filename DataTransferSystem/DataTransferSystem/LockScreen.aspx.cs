using DataTransferSystem.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataTransferSystem
{
    public partial class LockScreen : System.Web.UI.Page
    {
        ActiveDirectory activeDirectory = new ActiveDirectory();
        DATA_DAL Data_Dal = new DATA_DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                string UserName = "";

                if (lblUserName.Text.Contains(@"\"))
                {
                    UserName = lblUserName.Text.Replace(GetDomain(lblUserName.Text), "");
                }
                else
                {
                    UserName = lblUserName.Text;
                }

                string Pwd = txtPassword.Text;

                string GUID = "", SID = "";

                string[] data = activeDirectory.ValidateUser(UserName);
                GUID = data[0];
                SID = data[1];

                if (GUID != "" || SID != "")
                {

                    if (activeDirectory.AuthenticateUser(UserName, Pwd))
                    {
                        string[] Details = new string[7];

                        Details = activeDirectory.GetUserDetails(UserName, Pwd);

                        Session["UserName"] = lblUserName.Text.ToString();
                        Session["FullName"] = Details[0].ToString();
                        Session["ContactNo"] = Details[1].ToString();
                        Session["EmailID"] = Details[2].ToString();
                        Session["Department"] = Details[3].ToString();
                        Session["Designation"] = Details[4].ToString();
                        Session["FirstName"] = Details[5].ToString();
                        Session["LastName"] = Details[6].ToString();

                        Session["USERID"] = Session["FullName"];

                        Data_Dal.LOGIN_LOGS_SP(
                            ACTION: "INSERT_LOGIN_LOG",
                            UserName: lblUserName.Text.ToString(),
                            FirstName: Session["FirstName"].ToString(),
                            LastName: Session["LastName"].ToString(),
                            Result: "Login Successfully.",
                            HostIP: Data_Dal.GetMACAddress(),
                            IPADDRESS: Data_Dal.GetIpAddress()
                            );

                        Response.Redirect("HomePage.aspx", false);

                    }
                    else
                    {
                        Data_Dal.LOGIN_LOGS_SP(
                            ACTION: "INSERT_LOGIN_LOG",
                            UserName: lblUserName.Text.ToString(),
                            Result: "Login Failed.Invalid Credentials.",
                            HostIP: Data_Dal.GetMACAddress(),
                            IPADDRESS: Data_Dal.GetIpAddress()
                            );

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error!', 'Login Failed.Invalid Credentials', 'error');", true);
                    }
                }
                else
                {
                    Data_Dal.LOGIN_LOGS_SP(
                            ACTION: "INSERT_LOGIN_LOG",
                            UserName: lblUserName.Text.ToString(),
                            Result: "User not exist.",
                            HostIP: Data_Dal.GetMACAddress(),
                            IPADDRESS: Data_Dal.GetIpAddress()
                            );
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'User not exist', 'error');", true);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string GetDomain(string text, string stopAt = @"\")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation) + @"\";
                }
            }

            return "";
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