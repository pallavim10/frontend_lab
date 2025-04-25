using CTMS.CommonFunction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class eTMF_Doc_Login : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                ClearCacheItems();

            }

        }

        public void ClearCacheItems()
        {
            List<string> keys = new List<string>();
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();

            while (enumerator.MoveNext())
                keys.Add(enumerator.Key.ToString());

            for (int i = 0; i < keys.Count; i++)
                Cache.Remove(keys[i]);
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

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsAuth = dal_UMT.UMT_Auth(UserID: txt_UserName.Text, Pwd: txt_Pwd.Text);

                if (dsAuth.Tables.Count > 0 && dsAuth.Tables[0].Rows.Count > 0)
                {
                    string RESULT = dsAuth.Tables[0].Rows[0][0].ToString();
                    Boolean Login = false;
                    string URL = "";
                    switch (RESULT)
                    {
                        case "Security question locked":
                            Response.Write("<script> alert('Your security question has been locked. Please contact administrator.'); window.close();</script>");
                            Login = true;
                            
                            break;

                        case "Account Locked":
                            Response.Write("<script> alert('Your account has been locked'); window.close();</script>");
                            Login = true;
                            
                            break;

                        case "Invalid Credentials, Account Locked":
                            Response.Write("<script> alert('Invalid credentials, Your account has been locked'); window.close();</script>");
                            Login = true;
                            
                            break;

                        case "Invalid Credentials":
                            Response.Write("<script> alert('Invalid credentials');window.location='eTMF_Doc_Login.aspx';</script>");
                            Login = true;
                            //Response.Redirect("about:blank");
                            break;

                        case "Invalid User ID":
                            Response.Write("<script> alert('Invalid User ID');</script>");
                            Login = true;
                            
                            break;

                        case "Account is Inactive":
                            Response.Write("<script> alert('Your account has been deactivated');</script>");
                            Login = true;
                            
                            break;

                        case "First Login":
                            Session["User_ID"] = txt_UserName.Text;
                            Response.Write("<script> alert('Login successful, please change the password and set security question');window.location='Change_Password.aspx'; </script>");
                            Login = true;
                            break;

                        case "Change Password":
                            Session["User_ID"] = txt_UserName.Text;
                            Response.Write("<script> alert('Please change your password');window.location='Change_Password.aspx'; </script>");
                            Login = true;
                            break;

                        case "Set Security Question":
                            Session["User_ID"] = txt_UserName.Text;
                            Response.Write("<script> alert('Please set your security question');window.location='SET_SecurityQue.aspx'; </script>");
                            Login = true;
                            break;

                        case "Password Expired":
                            Session["User_ID"] = txt_UserName.Text;
                            Response.Write("<script> alert('Your password has been expired, please change the password');window.location='Change_Password.aspx'; </script>");
                            Login = true;
                            break;

                        default:
                            Session["User_ID"] = txt_UserName.Text;
                            if(Login==false)
                            {
                                URL = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace(Request.RawUrl.ToString(), Request.QueryString["filePath"].ToString());

                                Response.Redirect(URL);
                            }
                            else
                            {
                                string script = "window.open('about:blank', '_self');";

                                ClientScript.RegisterStartupScript(this.GetType(), "OpenBlankWindow", script, true);
                            }
                            break;
                    }
                    if(Login == false)
                    {
                        URL = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace(Request.RawUrl.ToString(), Request.QueryString["filePath"].ToString());

                        Response.Redirect(URL);
                    }
                    else
                    {
                        

                        string script = "window.open('about:blank', '_self');";

                        ClientScript.RegisterStartupScript(this.GetType(), "OpenBlankWindow", script, true);
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



    }
}