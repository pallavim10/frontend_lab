using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class CTMS_CreateAgendaMomUpload : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMeetingID.Text = Request.QueryString["AgendaID"];
            get_Doc();
        }

        protected void btnsubmitSec_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile uploadedFile = Request.Files[i];

                    string filename = Path.GetFileName(uploadedFile.FileName);
                    if (filename != "")
                    {
                        string contentType = uploadedFile.ContentType;

                        using (Stream fs = uploadedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                dal.CTMS_AGENDA(Action: "Upload_MoM", Project_ID: Session["PROJECTID"].ToString(), AgendaID: txtMeetingID.Text, FileName: filename, ContentType: contentType, Data: bytes);
                            }
                        }
                    }
                }

                get_Doc();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelSec_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        private void get_Doc()
        {
            try
            {              
                    DataSet ds = dal.CTMS_AGENDA(Action: "Get_Doc", Project_ID: Session["PROJECTID"].ToString(), AgendaID: txtMeetingID.Text);
                    gvMaterial.DataSource = ds.Tables[0];
                    gvMaterial.DataBind();           
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                DownloadFile(ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DownloadFile(string ID)
        {
            try
            {
                byte[] bytes;
                string fileName, contentType;

                DataSet ds = dal.CTMS_AGENDA(Action: "Download_Doc", ID: ID);

                bytes = (byte[])ds.Tables[0].Rows[0]["Data"];
                contentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                fileName = ds.Tables[0].Rows[0]["FileName"].ToString();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "Inline; filename=" + fileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

            }

        }
    }
}