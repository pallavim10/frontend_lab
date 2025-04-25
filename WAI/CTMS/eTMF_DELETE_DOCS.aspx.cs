using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_DELETE_DOCS : System.Web.UI.Page
    {
        DAL_eTMF dal_eTMF = new DAL_eTMF();
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
                        GETDATA();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "GET_eTMF_DOC_DATA", ID: Request.QueryString["ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblZone.Text = ds.Tables[0].Rows[0]["Zone"].ToString();
                    lblSection.Text = ds.Tables[0].Rows[0]["Section"].ToString();
                    lblArtifact.Text = ds.Tables[0].Rows[0]["Artifact"].ToString();
                    lblSubArtifact.Text = ds.Tables[0].Rows[0]["SubArtifact"].ToString();
                    lblFilename.Text = ds.Tables[0].Rows[0]["UploadFileName"].ToString();
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
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "DELETE_DOCUMENT",
                ID: Request.QueryString["ID"].ToString(),
                NOTE: txtReason.Text
                );

                Response.Write("<script> alert('Document Deleted successfully.');window.location.href='ETMF_MODI_DEL_DOCS.aspx';</script>");
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
                Response.Redirect("ETMF_MODI_DEL_DOCS.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }
    }
}