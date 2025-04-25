using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class eTMF_RejectDocs : System.Web.UI.Page
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
                else
                {
                    if (!this.IsPostBack)
                    {
                        //GETDATA();
                    }
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
                DataSet ds = dal.eTMF_SP(ACTION: "UPDATE_REJECT_DOC",
                ID: Request.QueryString["ID"].ToString(),
                UploadBy: Session["User_Id"].ToString(),
                NOTE: txtReason.Text
                );

                Response.Write("<script> alert('Document Rejected successfully.');window.location.href='eTMF_NonClassified_Docs.aspx';</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("eTMF_NonClassified_Docs.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }
    }
}