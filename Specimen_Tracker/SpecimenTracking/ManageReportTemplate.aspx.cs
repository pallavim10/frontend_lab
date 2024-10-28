using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class ManageReportTemplate : System.Web.UI.Page
    {
        DAL_SETUP Dal_Setup = new DAL_SETUP();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_SHIPMENT_MANIFEST();
                    GET_EXCEL_MANIFEST();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void GET_SHIPMENT_MANIFEST()
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_SHIPMENT_MANIFEST_SP(
                        ACTION: "GET_SHIPMENT_MANIFEST"
                        );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GrdShipment.DataSource = ds.Tables[0];
                    GrdShipment.DataBind();
                }
                else
                {
                    GrdShipment.DataSource = null;
                    GrdShipment.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        protected void lbtnUpload_Click(object sender, EventArgs e)
        {
            string fileName = Manifestfilename.FileName;
            if (fileName.Trim() != "")
            {
                UPLOAD_SHIPMENT_MANIFEST();
                GET_SHIPMENT_MANIFEST();
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

        private string GET_MANIFEST_COLUMNS(WordprocessingDocument copyDoc)
        {
            string RESULT = "";

            string targetTagName = "SHIPMENT_DATA";

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

        private void UPLOAD_SHIPMENT_MANIFEST()
        {
            try
            {
                string folderPath = Server.MapPath("~/Shipment Manifest/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(Server.MapPath("~/Shipment Manifest/"), Manifestfilename.FileName);

                Manifestfilename.SaveAs(filePath);

                string fileName = Manifestfilename.FileName;
                string contentType = Manifestfilename.PostedFile.ContentType;
                string fileExtension = Path.GetExtension(fileName).ToLower();
                int fileSize = Manifestfilename.PostedFile.ContentLength;

                byte[] fileData;
                using (Stream stream = Manifestfilename.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();

                    }
                }
                if (fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".docm")
                {
                    string SHIPMENT_COLUMNS = "";

                    using (WordprocessingDocument copyDoc = WordprocessingDocument.Open(filePath, false))
                    {
                        SHIPMENT_COLUMNS = GET_MANIFEST_COLUMNS(copyDoc);
                    }

                    DataSet ds = Dal_Setup.SETUP_SHIPMENT_MANIFEST_SP(ACTION: "UPLOAD_SHIPMENT_MANIFEST",
                        FILENAME: fileName,
                        CONTENT_TYPE: contentType,
                        DATA_TYPE: fileData,
                        FILE_EXTENSION: fileExtension,
                        SIZE: fileSize.ToString(),
                        SHIPMENT_COLUMNS: SHIPMENT_COLUMNS
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
                        text: 'Please Select Word File Only.',
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
            Manifestfilename.Attributes.Clear();
        }

        protected void GrdShipment_RowCommand(object sender, GridViewCommandEventArgs e)
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
                DataSet ds = Dal_Setup.SETUP_SHIPMENT_MANIFEST_SP(ACTION: "DOWNLOAD", ID: ID);

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

        protected void btnSubexcel_Click(object sender, EventArgs e)
        {
            string fileName = FileUploadExcel.FileName;
            if (fileName.Trim() != "")
            {
                UPLOAD_EXCEL_MANIFEST();
                GET_EXCEL_MANIFEST();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Warning!',
                        text: 'Please Select Excel File.',
                        icon: 'warning',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            }
        }
        private void GET_EXCEL_MANIFEST()
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_SHIPMENT_MANIFEST_SP(
                        ACTION: "GET_EXCEL_MANIFEST"
                        );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grd_data.DataSource = ds.Tables[0];
                    grd_data.DataBind();
                }
                else
                {
                    grd_data.DataSource = null;
                    grd_data.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void UPLOAD_EXCEL_MANIFEST()
        {
            try
            {

                string fileName = FileUploadExcel.FileName;
                string contentType = FileUploadExcel.PostedFile.ContentType;
                string fileExtension = Path.GetExtension(fileName).ToLower();
                int fileSize = FileUploadExcel.PostedFile.ContentLength;

                byte[] fileData;
                using (Stream stream = FileUploadExcel.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();

                    }
                }
                if (fileExtension == ".xlx" || fileExtension == ".xlsx" || fileExtension == ".csv")
                {
                    DataSet ds = Dal_Setup.SETUP_SHIPMENT_MANIFEST_SP(ACTION: "UPLOAD_EXCEL_MANIFEST",
                        FILENAME: fileName,
                        CONTENT_TYPE: contentType,
                        DATA_TYPE: fileData,
                        FILE_EXTENSION: fileExtension,
                        SIZE: fileSize.ToString()
                        );

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Excel file Uploaded Successfully.',
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
                        text: 'Please Select Excel File Only.',
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
        protected void btnCalexcel_Click(object sender, EventArgs e)
        {
            FileUploadExcel.Attributes.Clear();
        }

        protected void grd_data_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            if (e.CommandName.ToString() == "Download")
            {
                DOWNLOAD(ID);
            }
        }
    }
}