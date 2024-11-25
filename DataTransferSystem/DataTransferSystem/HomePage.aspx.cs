using DataTransferSystem.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataTransferSystem
{
    public partial class HomePage : System.Web.UI.Page
    {
        DATA_DAL Data_Dal = new DATA_DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FullName"] == null || Session["UserName"] == null)
            {
                Response.Redirect("~/SessionExpired.aspx");
            }

            if (!this.IsPostBack)
            {
                //Added by Neeraj
                HttpCookie cookie = new HttpCookie("Username");
                cookie.Value = Session["UserName"].ToString();
                // Optionally, set expiration and other properties
                cookie.Expires = DateTime.Now.AddDays(30); // Example: cookie expires in 1 day
                Response.Cookies.Add(cookie);



                HttpCookie CookkieFullName = new HttpCookie("FullName");
                CookkieFullName.Value = Session["FullName"].ToString();
                // Optionally, set expiration and other properties
                CookkieFullName.Expires = DateTime.Now.AddDays(30); // Example: cookie expires in 1 day
                Response.Cookies.Add(CookkieFullName);


                Get_DashboardData(Session["FullName"].ToString());
            }
        }


        public void Get_DashboardData(string username)
        {
            DataSet ds = new DataSet();
            //ds = Data_Dal.Get_DashboardData(username);

            //if (ds.Tables.Count > 0)
            //{
            //    DataTable dt_total = ds.Tables[0];
            //    if (dt_total.Rows.Count > 0)
            //    {
            //        lbltotal.Text = dt_total.Rows[0]["Total"].ToString();
            //    }
            //}
        }
    }
}