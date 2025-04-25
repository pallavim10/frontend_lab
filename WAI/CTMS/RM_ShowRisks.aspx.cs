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
    public partial class RM_ShowRisks : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }


                Session["From"] = Request.QueryString["From"]; //HttpUtility.HtmlDecode(Request.QueryString["TYPE"]);
                Session["To"] = Request.QueryString["To"];// HttpUtility.HtmlDecode(Request.QueryString["RiskId"]);

                if (!this.IsPostBack)
                {
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
                DataSet ds = dal.getBucketBetween(ProjectID: Session["PROJECTID"].ToString(), From: Session["From"].ToString(), To: Session["To"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}