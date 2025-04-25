using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class IWRS_STUDY_DOCS : System.Web.UI.Page
    {
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GET_DOC();

            }
        }

        private void GET_DOC()
        {
            try
            {

                DataSet ds = dal_IWRS.IWRS_DOCS_SP(ACTION: "GET_DOCUMENT", ID: Request.QueryString["ID"].ToString());
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblHeader.Text = ds.Tables[0].Rows[0]["DOC_NAME"].ToString();
                    string FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                    string ContentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                    byte[] FileData = (byte[])ds.Tables[0].Rows[0]["DATA"];

                    if (!Directory.Exists(Server.MapPath("~/IWRS_DOCS/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/IWRS_DOCS/"));
                    }

                    string FilePath = Server.MapPath("~/IWRS_DOCS/") + FileName;

                    using (System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
                    {
                        using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
                        {
                            bw.Write(FileData);
                            bw.Close();
                        }
                    }

                    string newPdfUrl = ("IWRS_DOCS/") + FileName;
                    ScriptManager.RegisterStartupScript(this, GetType(), "ChangePDFSource", "changePDFSource('" + newPdfUrl + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}