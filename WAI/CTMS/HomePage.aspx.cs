using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Configuration;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class HomePage : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_UMT dal_UMT = new DAL_UMT();

        public int col = 0;
        int test = 0;
        CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["USER_ID"] == null)
                    {
                        Response.Redirect("SessionExpired.aspx", true);
                        return;
                    }

                    Session["PVID"] = null;
                    if (Session["IWRS_CurrentDate"] == null)
                    {
                        Session["IWRS_CurrentDate"] = commFun.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy");
                    }

                    PopulateMenuControl();

                    if (Session["PWDExpire"].ToString() != "0")
                    {
                        if (Request.QueryString["menu"] == null)
                        {
                            if (Session["NoofDays"].ToString() == "1")
                            {
                                lblPwdExpDays.Text = "Your password will expire today.";
                                modalPwdExpiry.Show();
                            }
                            else if (Session["NoofDays"].ToString() == "2")
                            {
                                lblPwdExpDays.Text = "Your password will expire tomorrow.";
                                modalPwdExpiry.Show();
                            }
                            else
                            {
                                lblPwdExpDays.Text = "Your password will expire in " + Session["NoofDays"].ToString() + " day/s.";
                                modalPwdExpiry.Show();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void PopulateMenuControl()
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    if (Session["UMT"] != null && Session["UMT"].ToString() == "YES")
                    {
                        DataSet ds = dal_UMT.SYS_FUNCTIONS_SP(
                        ACTION: "GET_SYSTEMS"
                        );

                        lstm.DataSource = ds;
                        lstm.DataBind();
                    }
                    else
                    {
                        DataSet ds = dal.SYSTEM_SP(
                        ACTION: "GET_SYSTEMS",
                        PROJECTID: Session["PROJECTID"].ToString()
                        );

                        lstm.DataSource = ds;
                        lstm.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // lblErrorMsg.Text = ex.ToString();
            }


        }

        protected void btnPwdExpiryYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Change_Password.aspx");
        }
    }
}