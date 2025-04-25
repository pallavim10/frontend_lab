using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using CTMS.CommonFunction;
using CTMS;

namespace CTMS
{
    public partial class MM_Delete_Query : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_MM = new DAL_MM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.IsPostBack)
                {
                    DataSet ds = dal_MM.MM_QUERY_SP(ACTION: "GET_QUERY_DETAILS", ID: Request.QueryString["ID"].ToString());
                    lblSITEID.Text = ds.Tables[0].Rows[0]["SITEID"].ToString();
                    lblSUBJID.Text = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                    lblVISIT.Text = ds.Tables[0].Rows[0]["VISIT"].ToString();
                    lblMODULENAME.Text = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                    lblOLDQUERYTEXT.Text = ds.Tables[0].Rows[0]["QUERYTEXT"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_MM.MM_QUERY_SP(ACTION: "DELETE_QUERY",
                ID: Request.QueryString["ID"].ToString(),
                QUERYTEXT: TxtReason.Text
                );

                Response.Write("<script> alert('Query Deleted Successfully.');window.location='MM_QueryList.aspx'; </script>");

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("MM_QueryList.aspx");

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}