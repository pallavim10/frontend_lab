using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;

namespace CTMS
{
    public partial class eTMF_PROC_UPLOAD : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction com = new CommonFunction.CommonFunction();

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
                    DataSet ds = dal.eTMF_Process_SP(ACTION: "GET_FILE_DETAILS", ID: Request.QueryString["ID"].ToString());

                    lblfilename.Text = ds.Tables[0].Rows[0]["UploadFileName"].ToString();

                    hfMainDocId.Value = Request.QueryString["ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    Upload();
                }
                
                Comment();

                string PAGENAME = Session["prevURL"].ToString();

                Response.Write("<script> alert('Thank You');window.location='" + PAGENAME + "';</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Upload()
        {
            try
            {
                string folderPath = Server.MapPath("~/eTMF_Docs/");

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }

                string Size = FileUpload1.PostedFile.ContentLength.ToString();
                string extension = FileUpload1.PostedFile.ContentType;

                string UploadFileName = Path.GetFileName(FileUpload1.FileName);

                System.IO.FileInfo fi = new System.IO.FileInfo(FileUpload1.PostedFile.FileName);
                DateTime createtime = fi.CreationTime;
                DateTime modifytime = fi.LastWriteTime;
                DateTime accesstime = fi.LastAccessTime;

                DataSet ds = dal.eTMF_Process_SP
                        (
                        ACTION: "Upload_Proc_Doc",
                        UploadFileName: UploadFileName,
                        Size: Size,
                        FileType: extension,
                        UploadBy: Session["User_ID"].ToString(),
                        NOTE: txtNote.Text,
                        DocID: hfMainDocId.Value
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Comment()
        {
            try
            {
                dal.eTMF_Process_SP(
                    ACTION: "Add_Proc_Comments",
                    NOTE: txtNote.Text,
                    UploadBy: Session["User_ID"].ToString(),
                    DocID: hfMainDocId.Value
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}