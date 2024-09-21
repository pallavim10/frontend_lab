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
    public partial class HomePage : System.Web.UI.Page
    {

        DAL_UMT dal_UMT = new DAL_UMT();

        public int col = 0;
        int test = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }

                if (!this.IsPostBack)
                {
                    //Added by Neeraj
                    HttpCookie cookie = new HttpCookie("Username");
                    cookie.Value = Session["USER_ID"].ToString();
                    // Optionally, set expiration and other properties
                    cookie.Expires = DateTime.Now.AddDays(30); // Example: cookie expires in 1 day
                    Response.Cookies.Add(cookie);


                    HttpCookie CookkieFullName = new HttpCookie("FullName");
                    CookkieFullName.Value = Session["User_Name"].ToString();
                    // Optionally, set expiration and other properties
                    CookkieFullName.Expires = DateTime.Now.AddDays(30); // Example: cookie expires in 1 day
                    Response.Cookies.Add(CookkieFullName);

                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        protected void btnPwdExpiryYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Change_Password.aspx");
        }
    }
}