using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace CTMS
{
    public partial class RM_ShowEvents : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["User_ID"] == null)
                    {
                        Response.Redirect("~/SessionExpired.aspx", false);
                    }

                    Session["From"] = Request.QueryString["From"]; //HttpUtility.HtmlDecode(Request.QueryString["TYPE"]);
                    Session["To"] = Request.QueryString["To"];// HttpUtility.HtmlDecode(Request.QueryString["RiskId"]);

                    getData();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        private void getData()
        {
            try
            {
                DataTable ds = dal.getprojectevents(Action: "Between", From: Session["From"].ToString(), To: Session["To"].ToString(), ProjectId: Session["PROJECTID"].ToString());
                if (ds.Rows.Count > 0)
                {
                    gridprojevents.DataSource = ds;
                    gridprojevents.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}