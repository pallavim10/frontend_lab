using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class Login : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                ClearCacheItems();
            }

            if (hdn.Value != "1")
            {
                Session.Clear();
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

                    switch (RESULT)
                    {
                        case "Security question locked":
                            Response.Write("<script> alert('Your security question has been locked. Please contact administrator.');</script>");

                            break;

                        case "Account Locked":
                            Response.Write("<script> alert('Your account has been locked');</script>");

                            break;

                        case "Invalid Credentials, Account Locked":
                            Response.Write("<script> alert('Invalid credentials, Your account has been locked');</script>");

                            break;

                        case "Invalid Credentials":
                            Response.Write("<script> alert('Invalid credentials');</script>");

                            break;

                        case "Invalid User ID":
                            Response.Write("<script> alert('Invalid User ID');</script>");

                            break;

                        case "Account is Inactive":
                            Response.Write("<script> alert('Your account has been deactivated');</script>");

                            break;

                        case "First Login":
                            Session["User_ID"] = txt_UserName.Text;
                            Response.Write("<script> alert('Login successful, please change the password and set security question');window.location='Change_Password.aspx'; </script>");

                            break;

                        case "Change Password":
                            Session["User_ID"] = txt_UserName.Text;
                            Response.Write("<script> alert('Please change your password');window.location='Change_Password.aspx'; </script>");

                            break;

                        case "Set Security Question":
                            Session["User_ID"] = txt_UserName.Text;
                            Response.Write("<script> alert('Please set your security question');window.location='SET_SecurityQue.aspx'; </script>");

                            break;

                        case "Password Expired":
                            Session["User_ID"] = txt_UserName.Text;
                            Response.Write("<script> alert('Your password has been expired, please change the password');window.location='Change_Password.aspx'; </script>");

                            break;

                        default:
                            Session["User_ID"] = txt_UserName.Text;
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
                DataSet ds = dal_UMT.UMT_USER_DETAILS_SP(USERID: txt_UserName.Text);

                DataRow dr = ds.Tables[0].Rows[0];

                Session["PROJECTID"] = dr["PROJECTID"].ToString();
                Session["PROJECTIDTEXT"] = dr["PROJECTIDTEXT"].ToString();
                Session["SPONSORNAME"] = dr["SPONSORNAME"].ToString().Trim();

                Session["PWDExpire"] = dr["PWDExpire"].ToString();
                Session["NoofDays"] = dr["NoofDays"].ToString();
                Session["UserGroup_ID"] = dr["UserGroup_ID"].ToString().Trim();
                Session["UserGroupID"] = dr["UserGroupID"].ToString().Trim();
                Session["TimeZone_Value"] = dr["TimeZone_Value"].ToString().Trim();
                Session["TimeZone_Standard"] = dr["TimeZone_Standard"].ToString().Trim();
                Session["MEDAUTH_FORM"] = dr["MEDAUTH_FORM"].ToString().Trim();
                Session["MEDAUTH_FIELD"] = dr["MEDAUTH_FIELD"].ToString().Trim();
                Session["Unblind"] = dr["Unblind"].ToString().Trim();

                Session["Authentication_Title"] = dr["User_Name"].ToString().Trim();
                Session["Authentication_ID"] = dr["Email_ID"].ToString().Trim();

                Session["User_Name"] = dr["User_Name"].ToString().Trim();
                Session["USER_ID"] = dr["UserID"].ToString().Trim();

                Session["SignOff_eSource"] = dr["SignOff_eSource"].ToString().Trim();
                Session["SignOff_Safety"] = dr["SignOff_Safety"].ToString().Trim();
                Session["SignOff_eCRF"] = dr["SignOff_eCRF"].ToString().Trim();

                Session["UserType"] = dr["UserType"].ToString().Trim();

                Session["StudyRole"] = dr["StudyRole"].ToString().Trim();

                Session["UMT"] = "YES";

                HttpCookie usernameCookie = new HttpCookie("WAI_Name");

                usernameCookie.Values["WAI_Name"] = Session["User_Name"].ToString();
                HttpContext.Current.Response.Cookies.Add(usernameCookie);

                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    Session["DB_STATUS_LOGS_LAST_DAT"] = ds.Tables[1].Rows[0]["ENTEREDDAT"].ToString();
                }
                else
                {
                    Session.Remove("DB_STATUS_LOGS_LAST_DAT");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtforgetPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("Forget_Password.aspx");
        }
    }
}