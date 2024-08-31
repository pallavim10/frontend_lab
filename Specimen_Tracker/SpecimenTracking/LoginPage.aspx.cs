using System;
using SpecimenTracking.App_Code;
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

namespace SpecimenTracking
{
    public partial class LoginPage : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session.Clear();
                FillCapctha();
                // Assuming 'username' is the username you want to store

                //if (Request.Cookies["Username"] != null)
                //{
                //    txtUserName.Text = Server.HtmlEncode(Request.Cookies["Username"].Value);
                //    // Use the username as needed
                //    Response.Redirect("LockScreen.aspx");
                //}

                //if (Request.Cookies["Username"] != null)
                //{
                //    Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-30);
                //    Response.Cookies["FullName"].Expires = DateTime.Now.AddDays(-30);
                //}
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string script = "";
                DataSet dsAuth = dal_UMT.UMT_Auth(UserID: txtUserName.Text, Pwd: txtPassword.Text);

                if (dsAuth.Tables.Count > 0 && dsAuth.Tables[0].Rows.Count > 0)
                {
                    string RESULT = dsAuth.Tables[0].Rows[0][0].ToString();
                    string userCaptchaCode = txtCaptcha.Text.Trim();
                    string sessionCaptchaCode = Session["captcha"].ToString();
                    if (userCaptchaCode == sessionCaptchaCode)
                    {
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
                                Session["User_ID"] = txtUserName.Text;
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
                                Session["User_ID"] = txtUserName.Text;
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
                                Session["User_ID"] = txtUserName.Text;
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
                                Session["User_ID"] = txtUserName.Text;
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
                                Session["User_ID"] = txtUserName.Text;
                                Get_UserDetails();

                                Response.Redirect("HomePage.aspx");

                                break;
                        }
                    }
                    else
                    {
                        FillCapctha();
                        txtCaptcha.Text = "";
                        txtPassword.Text = "";
                        txtUserName.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Captcha validation failed.Please try again..', 'warning');", true);
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