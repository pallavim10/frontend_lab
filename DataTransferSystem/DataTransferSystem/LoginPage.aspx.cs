using DataTransferSystem.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataTransferSystem
{
    public partial class LoginPage : System.Web.UI.Page
    {
        ActiveDirectory activeDirectory = new ActiveDirectory();
        DATA_DAL Data_Dal = new DATA_DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session.Clear();
                FillCapctha();
                // Assuming 'username' is the username you want to store

                if (Request.Cookies["Username"] != null)
                {
                    txtUserName.Text = Server.HtmlEncode(Request.Cookies["Username"].Value);
                    // Use the username as needed
                    Response.Redirect("LockScreen.aspx");
                }

                if (Request.Cookies["Username"] != null)
                {
                    Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-30);
                    Response.Cookies["FullName"].Expires = DateTime.Now.AddDays(-30);
                }
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userCaptchaCode = txtCaptcha.Text.Trim();
                string sessionCaptchaCode = Session["captcha"].ToString();
                if (txtUserName.Text.Trim() == "")
                {
                    Data_Dal.LOGIN_LOGS_SP(
                    ACTION: "INSERT_LOGIN_LOG",
                    UserName: txtUserName.Text,
                    Result: "Please Enter UserName.",
                    HostIP: Data_Dal.GetMACAddress(),
                    IPADDRESS: Data_Dal.GetIpAddress()
                    );

                    FillCapctha();
                    txtCaptcha.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter UserName.', 'error');", true);
                }
                else if (txtPassword.Text.Trim() == "")
                {
                    Data_Dal.LOGIN_LOGS_SP(
                   ACTION: "INSERT_LOGIN_LOG",
                   UserName: txtUserName.Text,
                   Result: "Please enter password.",
                   HostIP: Data_Dal.GetMACAddress(),
                   IPADDRESS: Data_Dal.GetIpAddress()
                   );

                    FillCapctha();
                    txtCaptcha.Text = "";

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please enter password.', 'error');", true);
                }
                else if (userCaptchaCode == sessionCaptchaCode)
                {
                    string UserName = "";

                    if (txtUserName.Text.Contains(@"\"))
                    {
                        UserName = txtUserName.Text.Replace(GetDomain(txtUserName.Text), "");
                    }
                    else
                    {
                        UserName = txtUserName.Text;
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
                            DataSet dsUser = Data_Dal.LOGIN_LOGS_SP(ACTION: "CHECK_EXIST", UserName: UserName);
                            if (dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                            {
                                if (Chkremember.Checked)
                                {
                                    // Create cookies for UserID and Password
                                    HttpCookie userCookie = new HttpCookie("UserID", UserName);
                                    HttpCookie passCookie = new HttpCookie("Password", Pwd);

                                    // Set cookie expiration
                                    userCookie.Expires = DateTime.Now.AddDays(30);
                                    passCookie.Expires = DateTime.Now.AddDays(30);

                                    // Add cookies to the response
                                    Response.Cookies.Add(userCookie);
                                    Response.Cookies.Add(passCookie);
                                }
                                else
                                {
                                    // Remove cookies if "Remember Me" is not checked
                                    if (Request.Cookies["UserID"] != null)
                                    {
                                        HttpCookie userCookie = new HttpCookie("UserID");
                                        userCookie.Expires = DateTime.Now.AddDays(-1);
                                        Response.Cookies.Add(userCookie);
                                    }
                                    if (Request.Cookies["Password"] != null)
                                    {
                                        HttpCookie passCookie = new HttpCookie("Password");
                                        passCookie.Expires = DateTime.Now.AddDays(-1);
                                        Response.Cookies.Add(passCookie);
                                    }
                                }
                                string[] Details = new string[7];

                                Details = activeDirectory.GetUserDetails(UserName, Pwd);

                                Session["UserName"] = txtUserName.Text.ToString();
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
                                UserName: txtUserName.Text,
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
                                Session["Username"] = UserName;
                                Session["Pwd"] = Pwd;
                                string script = @"
                                    swal({
                                        title: 'Error',
                                        text: 'New User detected.Please proceed with Registration.',
                                        icon: 'error',
                                        button: 'OK'
                                    }).then((value) => {
                                        window.location.href = 'Registrations.aspx'; 
                                    });";

                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);

                                Data_Dal.LOGIN_LOGS_SP(
                                      ACTION: "INSERT_LOGIN_LOG",
                                      UserName: txtUserName.Text,
                                      Result: "New User",
                                      HostIP: Data_Dal.GetMACAddress(),
                                      IPADDRESS: Data_Dal.GetIpAddress()
                                      );


                            }
                        }
                        else
                        {
                            Data_Dal.LOGIN_LOGS_SP(
                            ACTION: "INSERT_LOGIN_LOG",
                            UserName: txtUserName.Text,
                            Result: "Login Failed.Invalid Credentials.",
                            HostIP: Data_Dal.GetMACAddress(),
                            IPADDRESS: Data_Dal.GetIpAddress()
                            );

                            FillCapctha();
                            txtCaptcha.Text = "";

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error!', 'Login Failed.Invalid Credentials', 'error');", true);
                        }
                    }
                    else
                    {
                        Data_Dal.LOGIN_LOGS_SP(
                            ACTION: "INSERT_LOGIN_LOG",
                            UserName: txtUserName.Text,
                            Result: "User not exist.",
                            HostIP: Data_Dal.GetMACAddress(),
                            IPADDRESS: Data_Dal.GetIpAddress()
                            );

                        FillCapctha();
                        txtCaptcha.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'User not exist', 'error');", true);
                    }
                }
                else
                {

                    Data_Dal.LOGIN_LOGS_SP(
                            ACTION: "INSERT_LOGIN_LOG",
                            UserName: txtUserName.Text,
                            Result: "Captcha validation failed.Please try again.",
                            HostIP: Data_Dal.GetMACAddress(),
                            IPADDRESS: Data_Dal.GetIpAddress()
                            );

                    FillCapctha();
                    txtCaptcha.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Captcha validation failed.Please try again..', 'warning');", true);


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

        private void FillCapctha()
        {
            try
            {
                Random random = new Random();

                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                {
                    captcha.Append(combination[random.Next(combination.Length)]);
                    Session["captcha"] = captcha.ToString();

                }

                using (Bitmap bitmap = new Bitmap(200, 60))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                        graphics.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);

                        // Add distortion to the text (example: slight rotation or random placement)
                        System.Drawing.Image backgroundImage = System.Drawing.Image.FromFile(Server.MapPath("~/dist/img/captcha-b.png"));
                        graphics.DrawImage(backgroundImage, 0, 0, bitmap.Width, bitmap.Height);
                        using (Font font = new Font("Arial", 24, FontStyle.Bold))
                        using (Brush brush = new SolidBrush(Color.Black))
                        {
                            // Example: Apply slight rotation to characters
                            GraphicsPath path = new GraphicsPath();
                            path.AddString(Session["captcha"].ToString(), font.FontFamily, (int)font.Style, font.Size, new Point(10, 10), StringFormat.GenericDefault);
                            Matrix matrix = new Matrix();
                            matrix.Rotate(5); // Rotate characters slightly
                            path.Transform(matrix);
                            graphics.DrawPath(new Pen(Color.Black, 0), path);
                        }
                    }

                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        bitmap.Save(memoryStream, ImageFormat.Png);
                        byte[] bytes = memoryStream.ToArray();
                        string base64String = Convert.ToBase64String(bytes);
                        imgCaptcha.ImageUrl = "data:image/png;base64," + base64String;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnrefcaptcha_Click(object sender, EventArgs e)
        {
            try
            {
                FillCapctha();
                txtCaptcha.Text = "";
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}