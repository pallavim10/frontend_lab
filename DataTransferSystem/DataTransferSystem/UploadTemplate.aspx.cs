using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DataTransferSystem.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataTransferSystem
{
    public partial class UploadTemplate : System.Web.UI.Page
    {
        DAL_UPLOAD_TEMPLATE DAL_UPLOAD = new DAL_UPLOAD_TEMPLATE();
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                if (!this.IsPostBack) 
                {
                    GET_DATA_TRANSFER_DETAILS();
                    GET_DATA_TRANSFER_SUMMARY();
                }
            }
            catch (Exception ex) 
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }



        private void GET_DATA_TRANSFER_DETAILS() 
        {
            try
            {

                DataSet ds = DAL_UPLOAD.UPLOAD_TEMPLATE_SP(ACTION: "GET_DATA_TRANSFER_DETAILS",TRANSFER:"DETAILS");
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GrdDataTransferDetails.DataSource = ds.Tables[0];
                    GrdDataTransferDetails.DataBind();
                }
                else
                {
                    GrdDataTransferDetails.DataSource = ds.Tables[0];
                    GrdDataTransferDetails.DataBind();
                }
            }
            catch (Exception ex) 
            {
                ex.StackTrace.ToString();
            }
        
        }

        private void GET_DATA_TRANSFER_SUMMARY() 
        {
            try
            {

                DataSet ds = DAL_UPLOAD.UPLOAD_TEMPLATE_SP(ACTION: "GET_DATA_TRANSFER_SUMMARY",TRANSFER:"SUMMARY");
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grd_datatransfersummary.DataSource = ds.Tables[0];
                    grd_datatransfersummary.DataBind();
                }
                else
                {
                    grd_datatransfersummary.DataSource = ds.Tables[0];
                    grd_datatransfersummary.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }

        protected void lbtnUpload_Click(object sender, EventArgs e)
        {
            string fileName = transfer_detailfilename.FileName;
            if (fileName.Trim() != "")
            {
                UPLOAD_DATA_TRANSFER_DETAILS();
                GET_DATA_TRANSFER_DETAILS();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Warning!',
                        text: 'Please Select Word File.',
                        icon: 'warning',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            }
        }

        private string GET_DATATRANSFER_COLUMNS(WordprocessingDocument copyDoc)
        {
            string RESULT = "";

            string targetTagName = "DATA_TRANSFER_COLUMNS";

            List<SdtElement> sdtElements = new List<SdtElement>();

            List<SdtElement> sdtElements_Main = copyDoc.MainDocumentPart.Document
                .Descendants<SdtElement>()
                .Where(sdt => sdt.SdtProperties != null &&
                sdt.SdtProperties.GetFirstChild<Tag>()?.Val?.Value == targetTagName).ToList();

            sdtElements.AddRange(sdtElements_Main);

            foreach (var headerPart in copyDoc.MainDocumentPart.HeaderParts)
            {
                List<SdtElement> sdtElements_Header = headerPart.Header
                    .Descendants<SdtElement>()
                    .Where(sdt => sdt.SdtProperties != null &&
                    sdt.SdtProperties.GetFirstChild<Tag>()?.Val?.Value == targetTagName).ToList();

                sdtElements.AddRange(sdtElements_Header);
            }

            foreach (var footerPart in copyDoc.MainDocumentPart.FooterParts)
            {
                List<SdtElement> sdtElements_Footer = footerPart.Footer
                    .Descendants<SdtElement>()
                    .Where(sdt => sdt.SdtProperties != null &&
                    sdt.SdtProperties.GetFirstChild<Tag>()?.Val?.Value == targetTagName).ToList();

                sdtElements.AddRange(sdtElements_Footer);
            }

            foreach (SdtElement sdt in sdtElements)
            {
                var clonedSDT = (SdtElement)sdt.CloneNode(true);

                if (clonedSDT != null)
                {
                    List<Text> textElements = clonedSDT.Descendants<Text>().ToList();

                    foreach (Text element in textElements)
                    {
                        if (element != null)
                        {
                            if (RESULT != "")
                            {
                                RESULT = RESULT + "," + element.Text;
                            }
                            else
                            {
                                RESULT = element.Text;
                            }
                        }
                    }
                }
            }

            return RESULT;
        }

        private void UPLOAD_DATA_TRANSFER_DETAILS()
        {
            try
            {
                string folderPath = Server.MapPath("~/Data Transfer Details/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(Server.MapPath("~/Data Transfer Details/"), transfer_detailfilename.FileName);

                transfer_detailfilename.SaveAs(filePath);

                string fileName = transfer_detailfilename.FileName;
                string contentType = transfer_detailfilename.PostedFile.ContentType;
                string fileExtension = Path.GetExtension(fileName).ToLower();
                int fileSize = transfer_detailfilename.PostedFile.ContentLength;

                byte[] fileData;
                using (Stream stream = transfer_detailfilename.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();

                    }
                }
                if (fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".docm")
                {
                    string DETAILS_COLUMNS = "";

                    using (WordprocessingDocument copyDoc = WordprocessingDocument.Open(filePath, false))
                    {
                        DETAILS_COLUMNS = GET_DATATRANSFER_COLUMNS(copyDoc);
                    }

                    DataSet ds = DAL_UPLOAD.UPLOAD_TEMPLATE_SP(ACTION: "UPLOAD_TRANSFER_DETAILS",
                        FILENAME: fileName,
                        CONTENT_TYPE: contentType,
                        DATA_TYPE: fileData,
                        FILE_EXTENSION: fileExtension,
                        SIZE: fileSize.ToString(),
                        DATA_TRANSFER_COLUMNS: DETAILS_COLUMNS,
                        TRANSFER: "DETAILS"
                        );

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Word file Uploaded Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Warning!',
                        text: 'Please Select a Word File Only.',
                        icon: 'warning',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);

                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            transfer_detailfilename.Attributes.Clear();
        }

        protected void GrdDataTransferDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            if (e.CommandName.ToString() == "Download")
            {
                DOWNLOAD(ID);
            }
        }

        private void DOWNLOAD(string ID)
        {
            try
            {
                string FileName, ContentType;
                byte[] fileData;
                DataSet ds = DAL_UPLOAD.UPLOAD_TEMPLATE_SP(ACTION: "DOWNLOAD", ID: ID);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                        ContentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                        fileData = (byte[])ds.Tables[0].Rows[0]["DATA"];
                        Response.Clear();
                        Response.ContentType = ContentType;
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                        Response.BinaryWrite(fileData);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        protected void btnSubmitSummary_Click(object sender, EventArgs e)
        {
            string fileName = transfer_summaryfilename.FileName;
            if (fileName.Trim() != "")
            {
                UPLOAD_DATA_TRANSFER_SUMMARY();
                GET_DATA_TRANSFER_SUMMARY();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Warning!',
                        text: 'Please Select a Word File.',
                        icon: 'warning',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            }
        }
        private void UPLOAD_DATA_TRANSFER_SUMMARY()
        {
            try
            {
                string folderPath = Server.MapPath("~/Data Transfer Summary/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(Server.MapPath("~/Data Transfer Summary/"), transfer_summaryfilename.FileName);

                transfer_summaryfilename.SaveAs(filePath);

                string fileName = transfer_summaryfilename.FileName;
                string contentType = transfer_summaryfilename.PostedFile.ContentType;
                string fileExtension = Path.GetExtension(fileName).ToLower();
                int fileSize = transfer_summaryfilename.PostedFile.ContentLength;

                byte[] fileData;
                using (Stream stream = transfer_summaryfilename.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();

                    }
                }
                if (fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".docm")
                {
                    string SUMMARY_COLUMNS = "";

                    using (WordprocessingDocument copyDoc = WordprocessingDocument.Open(filePath, false))
                    {
                        SUMMARY_COLUMNS = GET_DATATRANSFER_COLUMNS(copyDoc);
                    }

                    DataSet ds = DAL_UPLOAD.UPLOAD_TEMPLATE_SP(ACTION: "UPLOAD_TRANSFER_SUMMARY",
                        FILENAME: fileName,
                        CONTENT_TYPE: contentType,
                        DATA_TYPE: fileData,
                        FILE_EXTENSION: fileExtension,
                        SIZE: fileSize.ToString(),
                        DATA_TRANSFER_COLUMNS: SUMMARY_COLUMNS,
                        TRANSFER: "SUMMARY"
                        );

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Word file Uploaded Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Warning!',
                        text: 'Please Select a Word File Only.',
                        icon: 'warning',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);

                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        protected void btnCancelSummary_Click(object sender, EventArgs e)
        {
            transfer_summaryfilename.Attributes.Clear();
        }

        protected void grd_datatransfersummary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            if (e.CommandName.ToString() == "Download")
            {
                DOWNLOAD(ID);
            }
        }
    }
}