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
            //if (!this.IsPostBack)
            //{

            //    //if (Session["UserName"] == null)
            //    //{
            //       // Response.Redirect("~/SessionExpired.aspx");

            //    //}
            //    ////Added by Neeraj
            //    //HttpCookie cookie = new HttpCookie("Username");
            //    //cookie.Value = Session["UserName"].ToString();
            //    //// Optionally, set expiration and other properties
            //    //cookie.Expires = DateTime.Now.AddDays(30); // Example: cookie expires in 1 day
            //    //Response.Cookies.Add(cookie);

            //    //HttpCookie CookkieFullName = new HttpCookie("FullName");
            //    //CookkieFullName.Value = Session["FullName"].ToString();
            //    //// Optionally, set expiration and other properties
            //    //CookkieFullName.Expires = DateTime.Now.AddDays(30); // Example: cookie expires in 1 day
            //    //Response.Cookies.Add(CookkieFullName);

            //}
            try
            {
                if (!this.IsPostBack)
                {

                    // PopulateMenuControl();
                    if (Session["User_ID"] == null)
                    {
                        Response.Redirect("~/SessionExpired.aspx", false);
                    }

                    //if (Session["PWDExpire"].ToString() != "0")
                    //{
                    //    if (Request.QueryString["menu"] == null)
                    //    {
                    //        if (Session["NoofDays"].ToString() == "1")
                    //        {
                    //            lblPwdExpDays.Text = "Your password will expire today.";
                    //            modalPwdExpiry.Show();
                    //        }
                    //        else if (Session["NoofDays"].ToString() == "2")
                    //        {
                    //            lblPwdExpDays.Text = "Your password will expire tomorrow.";
                    //            modalPwdExpiry.Show();
                    //        }
                    //        else
                    //        {
                    //            lblPwdExpDays.Text = "Your password will expire in " + Session["NoofDays"].ToString() + " day/s.";
                    //            modalPwdExpiry.Show();
                    //        }
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        //private void PopulateMenuControl()
        //{
        //    try
        //    {
        //        if (Session["User_ID"] == null)
        //        {
        //            Response.Redirect("~/SessionExpired.aspx", false);
        //        }
        //        else
        //        {
        //            if (Session["UMT"] != null && Session["UMT"].ToString() == "YES")
        //            {
        //                DataSet ds = dal_UMT.SYS_FUNCTIONS_SP(
        //                ACTION: "GET_SYSTEMS"
        //                );

        //                lstm.DataSource = ds;
        //                lstm.DataBind();
        //            }
        //            else
        //            {
        //                DataSet ds = dal_UMT.SYSTEM_SP(
        //                ACTION: "GET_SYSTEMS",
        //                PROJECTID: Session["PROJECTID"].ToString()
        //                );
        //    lstm.DataSource = ds;
        //    lstm.DataBind();
        //}
        //else
        //{
        //    DataSet ds = dal_UMT.SYSTEM_SP(
        //    ACTION: "GET_SYSTEMS",
        //    PROJECTID: "111"//Session["PROJECTID"].ToString()
        //    ) ;

        //                lstm.DataSource = ds;
        //                lstm.DataBind();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        // lblErrorMsg.Text = ex.ToString();
        //    }


        //}

        protected void btnPwdExpiryYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Change_Password.aspx");
        }
    }
}