using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office2010.Word;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using IronPdf;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace CTMS
{
    public partial class NSAE_REPORTS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_SAE dal_SAE = new DAL_SAE();


        Word.WdExportFormat wd = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["UserGroup_ID"] == null)
                    {
                        Response.Redirect("SessionExpired.aspx", true);
                        return;
                    }

                    FillINV();
                    FillSubject();
                    GET_REPORTS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string RemoveInvalidFileNameChars(string filename)
        {
            // Get invalid characters from Path class
            char[] invalidChars = Path.GetInvalidFileNameChars();

            // Remove all invalid characters
            return new string(filename.Where(c => !invalidChars.Contains(c)).ToArray());
        }

        private string Save_Report(string SAEID)
        {
            string FilePath = "";


            try
            {
                DataSet dsReport = dal_SAE.SAE_REPORT_SP(ACTION: "GET_REPORT_FORMAT", ID: drpReportType.SelectedValue);

                if (dsReport.Tables.Count > 0 && dsReport.Tables[0].Rows.Count > 0)
                {
                    string FileEXT = dsReport.Tables[0].Rows[0]["FILENAME"].ToString().Split('.').Last();

                    byte[] FileData = (byte[])dsReport.Tables[0].Rows[0]["DATA_TYPE"];

                    string FILENAME = RemoveInvalidFileNameChars(Session["PROJECTIDTEXT"].ToString()) + "-" + RemoveInvalidFileNameChars(drpReportType.SelectedItem.Text) + "-" + SAEID + "-" + DateTime.Now.ToString("yyyyMMddHHmm") + "." + FileEXT + "";

                    FilePath = Path.Combine(Server.MapPath(@"SAE Report Templates"), FILENAME);

                    File.WriteAllBytes(FilePath, FileData);

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            return FilePath;

        }

        private bool ConvertTOPDF(string sourcePath, string targetPath, Word.WdExportFormat exportFormat)
        {
            bool result;
            object paramMissing = Type.Missing;
            Word.ApplicationClass wordApplication = new Word.ApplicationClass();
            Word.Document wordDocument = null;
            try
            {
                object paramSourceDocPath = sourcePath;
                string paramExportFilePath = targetPath;
                Word.WdExportFormat paramExportFormat = exportFormat;
                bool paramOpenAfterExport = false;
                Word.WdExportOptimizeFor paramExportOptimizeFor =
                Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Word.WdExportRange paramExportRange = Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                Word.WdExportItem paramExportItem = Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Word.WdExportCreateBookmarks paramCreateBookmarks =
                Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;
                wordDocument = wordApplication.Documents.Open(
                                       ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                                       ref paramMissing, ref paramMissing, ref paramMissing,
                                       ref paramMissing, ref paramMissing, ref paramMissing,
                                       ref paramMissing, ref paramMissing, ref paramMissing,
                                       ref paramMissing, ref paramMissing, ref paramMissing,
                                       ref paramMissing);

                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                    paramExportFormat, paramOpenAfterExport,
                    paramExportOptimizeFor, paramExportRange, paramStartPage,
                    paramEndPage, paramExportItem, paramIncludeDocProps,
                    paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                    paramBitmapMissingFonts, paramUseISO19005_1,
                    ref paramMissing);
                result = true;
            }
            finally
            {
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        private void GENERATE_REPORT(string SAEID, string SAE_STATUS)
        {
            try
            {
                string uncheckValue = "☐";
                //string checkValue = "☒";
                string checkValue = "\u2611";

                string TemplatePath = Save_Report(SAEID);

                //string newTemplatePath_PDF = Server.MapPath(@"SAE Report Templates\App 5-SAE Report form-Template_Updated.pdf");

                using (WordprocessingDocument copyDoc = WordprocessingDocument.Open(TemplatePath, true))
                {
                    SET_DEFAULT_VARIABLES(copyDoc, SAEID, SAE_STATUS);

                    DataSet dsVARIABLES = dal_SAE.SAE_REPORT_SP(ACTION: "GET_REPORT_VARIABLES", REPORT_ID: drpReportType.SelectedValue);

                    foreach (DataRow drVARIABLES in dsVARIABLES.Tables[0].Rows)
                    {
                        DataSet dsVARIABLESDATA = dal_SAE.SAE_REPORT_SP(
                            ACTION: "GET_REPORT_VARIABLES_DATA",
                            SAEID: SAEID,
                            STATUS: SAE_STATUS,
                            TABLENAME: drVARIABLES["TABLENAME"].ToString(),
                            VARIABLENAMES: drVARIABLES["VARIABLENAMES"].ToString()
                            );

                        string targetTagName = "";

                        if (drVARIABLES["CONTROL_TYPE"].ToString() == "Repeating Section Content Control")
                        {
                            targetTagName = drVARIABLES["DOMAIN"].ToString();
                        }
                        else
                        {
                            targetTagName = drVARIABLES["VARIABLENAMES"].ToString();
                        }

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
                            SdtProperties sdtProperties = sdt.SdtProperties;
                            SdtAlias titleElement = sdtProperties.GetFirstChild<SdtAlias>();
                            string title = titleElement != null ? titleElement.Val.Value : string.Empty;

                            switch (drVARIABLES["CONTROL_TYPE"].ToString())
                            {
                                case "Plain Text Content Control":

                                    foreach (DataRow drDATA in dsVARIABLESDATA.Tables[0].Rows)
                                    {
                                        foreach (DataColumn dcDATA in dsVARIABLESDATA.Tables[0].Columns)
                                        {
                                            var sdtContentRun = sdt.Descendants<SdtContentRun>().FirstOrDefault();
                                            if (sdtContentRun != null)
                                            {
                                                var textElement = sdt.Descendants<Text>().FirstOrDefault();

                                                if (textElement != null)
                                                {
                                                    if (title != "")
                                                    {
                                                        if (title.Contains("VISIBLE_"))
                                                        {
                                                            if (drDATA[dcDATA.ColumnName].ToString() != title.Replace("VISIBLE_", "").ToString())
                                                            {
                                                                sdtContentRun.Descendants<Text>().ToList().ForEach(t => t.Remove());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            textElement.Text = GET_VALUE_ByTitle(drDATA[dcDATA.ColumnName].ToString(), title);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        textElement.Text = drDATA[dcDATA.ColumnName].ToString();
                                                    }

                                                    sdtContentRun.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
                                                }
                                            }
                                            else
                                            {
                                                var textElement = sdt.Descendants<Text>().FirstOrDefault();

                                                if (textElement != null)
                                                {
                                                    if (title != "")
                                                    {
                                                        if (title.Contains("VISIBLE_"))
                                                        {
                                                            if (drDATA[dcDATA.ColumnName].ToString() != title.Replace("VISIBLE_", "").ToString())
                                                            {
                                                                sdt.Descendants<Text>().ToList().ForEach(t => t.Remove());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            textElement.Text = GET_VALUE_ByTitle(drDATA[dcDATA.ColumnName].ToString(), title);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        textElement.Text = drDATA[dcDATA.ColumnName].ToString();
                                                    }
                                                }

                                                sdt.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
                                            }
                                        }
                                    }

                                    break;

                                case "Check Box Content Control":

                                    List<SdtContentCheckBox> checkBoxes = sdt.Descendants<SdtContentCheckBox>().ToList();

                                    foreach (DataRow drDATA in dsVARIABLESDATA.Tables[0].Rows)
                                    {
                                        foreach (DataColumn dcDATA in dsVARIABLESDATA.Tables[0].Columns)
                                        {
                                            foreach (SdtContentCheckBox checkBox in checkBoxes)
                                            {
                                                var enclosingSdtElement = checkBox.Ancestors<SdtElement>().FirstOrDefault();

                                                if (enclosingSdtElement != null)
                                                {
                                                    var tagName = enclosingSdtElement.SdtProperties.GetFirstChild<Tag>().Val;

                                                    var VALUES = "";
                                                    if (title != "")
                                                    {
                                                        VALUES = GET_VALUE_ByTitle(drDATA[dcDATA.ColumnName].ToString(), title);
                                                    }
                                                    else
                                                    {
                                                        VALUES = drDATA[dcDATA.ColumnName].ToString();
                                                    }

                                                    if (checkBox.Parent.Parent.Descendants<SdtContentRun>().ToList().Count > 0)
                                                    {
                                                        SdtContentRun text = (SdtContentRun)checkBox.Parent.Parent.Descendants<SdtContentRun>().ToList()[0];

                                                        if (tagName == VALUES)
                                                        {
                                                            text.InnerXml = text.InnerXml.Replace(uncheckValue, checkValue);
                                                        }
                                                        else
                                                        {
                                                            text.InnerXml = text.InnerXml.Replace(checkValue, uncheckValue);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    break;

                                case "Repeating Section Content Control":

                                    DataTable dsVALUES = new DataTable();

                                    if (title != "")
                                    {
                                        dsVALUES = GET_DATASET_ByTitle(dsVARIABLESDATA.Tables[0], title);
                                    }
                                    else
                                    {
                                        dsVALUES = dsVARIABLESDATA.Tables[0];
                                    }

                                    foreach (DataRow drDATA in dsVARIABLESDATA.Tables[0].Rows)
                                    {
                                        var clonedSDT = (SdtElement)sdt.CloneNode(true);

                                        if (clonedSDT != null)
                                        {
                                            List<Text> textElements = clonedSDT.Descendants<Text>().ToList();

                                            foreach (Text element in textElements)
                                            {
                                                if (element != null)
                                                {
                                                    if (dsVARIABLESDATA.Tables[0].Columns.Contains(element.InnerText.Trim()))
                                                    {
                                                        element.Text = element.Text.Replace(element.InnerText.Trim(), drDATA[element.InnerText.Trim()].ToString());
                                                    }
                                                }
                                            }

                                            sdt.Parent.InsertAfter(clonedSDT, sdt);
                                        }
                                    }

                                    sdt.Remove();

                                    break;
                            }
                        }

                        copyDoc.MainDocumentPart.Document.Save();
                    }
                }

                DOWNLOAD_REPORT(TemplatePath);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private DataTable GET_DATASET_ByTitle(DataTable dtDATA, string TagName)
        {
            DataTable dtRESULT = new DataTable();

            try
            {
                string Position = TagName.Split('_')[0].ToString();
                string Numbers = TagName.Split('_')[1].ToString();

                dtRESULT = dtDATA.Clone();

                if (Position == "Top")
                {
                    for (int i = 0; i < Math.Min(Convert.ToInt32(Numbers), dtDATA.Rows.Count); i++)
                    {
                        dtRESULT.ImportRow(dtDATA.Rows[i]);
                    }
                }
                else if (Position == "Other")
                {
                    if (dtDATA.Rows.Count > Convert.ToInt32(Numbers))
                    {
                        for (int i = Convert.ToInt32(Numbers); i < dtDATA.Rows.Count; i++)
                        {
                            dtRESULT.ImportRow(dtDATA.Rows[i]);
                        }
                    }
                }
                else if (Position == "Bottom")
                {
                    if (dtDATA.Rows.Count > Convert.ToInt32(Numbers))
                    {
                        int rowCount = dtDATA.Rows.Count;

                        for (int i = Math.Max(0, rowCount - Convert.ToInt32(Numbers)); i < rowCount; i++)
                        {
                            dtRESULT.ImportRow(dtDATA.Rows[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            return dtRESULT;
        }

        private string GET_VALUE_ByTitle(string DATA, string TagName)
        {
            string result = "";

            try
            {
                if (TagName == "DD")
                {
                    if (DATA.Contains("/"))
                    {
                        result = DATA.Split('/')[0].ToString();
                    }
                    else
                    {
                        result = DATA.Split('-')[0].ToString();
                    }
                }
                else if (TagName == "MMM")
                {
                    if (DATA.Contains("/"))
                    {
                        result = DATA.Split('/')[1].ToString();
                    }
                    else
                    {
                        result = DATA.Split('-')[1].ToString();
                    }
                }
                else if (TagName == "YYYY")
                {
                    if (DATA.Contains("/"))
                    {
                        result = DATA.Split('/')[2].ToString();
                    }
                    else
                    {
                        result = DATA.Split('-')[2].ToString();
                    }
                }
                else if (TagName.Contains("Sentence"))
                {
                    string Position = TagName.Split('_')[1].ToString();
                    string Numbers = TagName.Split('_')[2].ToString();

                    if (Position == "Top")
                    {
                        string pattern = @"(?<=[.!?])\s+";
                        string[] sentences = Regex.Split(DATA, pattern);
                        result = string.Join(" ", sentences.Take(Convert.ToInt32(Numbers)));
                    }
                    else if (Position == "Other")
                    {
                        string pattern = @"(?<=[.!?])\s+";
                        string[] sentences = Regex.Split(DATA, pattern);

                        if (sentences.Length > Convert.ToInt32(Numbers))
                        {
                            result = string.Join(" ", sentences.Skip(Convert.ToInt32(Numbers)));
                        }
                    }
                    else if (Position == "Bottom")
                    {
                        string pattern = @"(?<=[.!?])\s+";
                        string[] sentences = Regex.Split(DATA, pattern);
                        result = string.Join(" ", sentences.Skip(Math.Max(0, sentences.Length - Convert.ToInt32(Numbers))));
                    }
                }
                else if (TagName.Contains("Character"))
                {
                    string Position = TagName.Split('_')[1].ToString();
                    string Numbers = TagName.Split('_')[2].ToString();

                    if (Position == "Top")
                    {
                        result = DATA.Substring(0, Convert.ToInt32(Numbers));
                    }
                    else if (Position == "Other")
                    {
                        if (DATA.Length > Convert.ToInt32(Numbers))
                        {
                            result = DATA.Substring(Convert.ToInt32(Numbers));
                        }
                        else
                        {
                            result = DATA;
                        }
                    }
                    else if (Position == "Bottom")
                    {
                        if (DATA.Length >= 2)
                        {
                            string lastTwoCharacters = DATA.Substring(DATA.Length - Convert.ToInt32(Numbers));
                        }
                    }
                }
                else
                {
                    result = "";
                }
            }
            catch (Exception ex)
            { }
            return result;
        }

        private void SET_DEFAULT_VARIABLES(WordprocessingDocument copyDoc, string SAEID, string STATUS)
        {
            try
            {
                string uncheckValue = "☐";
                //string checkValue = "☒";
                string checkValue = "\u2611";

                DataSet ds = dal_SAE.SAE_REPORT_SP(ACTION: "GET_DEFAULT_VARIABLES", SAEID: SAEID, STATUS: STATUS);

                if (ds.Tables.Count > 0)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string VARIABLENAME = dc.ColumnName;
                                string DATA = dt.Rows[0][dc.ColumnName].ToString();

                                List<SdtElement> sdtElements = new List<SdtElement>();

                                List<SdtElement> sdtElements_Main = copyDoc.MainDocumentPart.Document
                                                       .Descendants<SdtElement>()
                                                       .Where(sdt => sdt.SdtProperties != null &&
                                                                     sdt.SdtProperties.GetFirstChild<Tag>()?.Val?.Value == VARIABLENAME).ToList();

                                sdtElements.AddRange(sdtElements_Main);

                                foreach (var headerPart in copyDoc.MainDocumentPart.HeaderParts)
                                {
                                    List<SdtElement> sdtElements_Header = headerPart.Header.Descendants<SdtElement>().Where(sdt => sdt.SdtProperties != null &&
                                                                     sdt.SdtProperties.GetFirstChild<Tag>()?.Val?.Value == VARIABLENAME).ToList();

                                    sdtElements.AddRange(sdtElements_Header);
                                }

                                foreach (var footerPart in copyDoc.MainDocumentPart.FooterParts)
                                {
                                    List<SdtElement> sdtElements_Footer = footerPart.Footer.Descendants<SdtElement>().Where(sdt => sdt.SdtProperties != null &&
                                                                     sdt.SdtProperties.GetFirstChild<Tag>()?.Val?.Value == VARIABLENAME).ToList();

                                    sdtElements.AddRange(sdtElements_Footer);
                                }

                                if (VARIABLENAME == "REPORTTYPE")
                                {
                                    foreach (SdtElement sdt in sdtElements)
                                    {
                                        SdtProperties sdtProperties = sdt.SdtProperties;
                                        SdtAlias titleElement = sdtProperties.GetFirstChild<SdtAlias>();
                                        string title = titleElement != null ? titleElement.Val.Value : string.Empty;

                                        List<SdtContentCheckBox> checkBoxes = sdt.Descendants<SdtContentCheckBox>().ToList();

                                        foreach (SdtContentCheckBox checkBox in checkBoxes)
                                        {
                                            var enclosingSdtElement = checkBox.Ancestors<SdtElement>().FirstOrDefault();

                                            if (enclosingSdtElement != null)
                                            {
                                                var tagName = enclosingSdtElement.SdtProperties.GetFirstChild<Tag>().Val;

                                                var VALUES = "";
                                                if (title != "")
                                                {
                                                    VALUES = GET_VALUE_ByTitle(DATA.ToString(), title);
                                                }
                                                else
                                                {
                                                    VALUES = DATA.ToString();
                                                }

                                                if (checkBox.Parent.Parent.Descendants<SdtContentRun>().ToList().Count > 0)
                                                {
                                                    SdtContentRun text = (SdtContentRun)checkBox.Parent.Parent.Descendants<SdtContentRun>().ToList()[0];

                                                    if (tagName == VALUES)
                                                    {
                                                        text.InnerXml = text.InnerXml.Replace(uncheckValue, checkValue);
                                                    }
                                                    else
                                                    {
                                                        text.InnerXml = text.InnerXml.Replace(checkValue, uncheckValue);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (SdtElement sdt in sdtElements)
                                    {
                                        SdtProperties sdtProperties = sdt.SdtProperties;
                                        SdtAlias titleElement = sdtProperties.GetFirstChild<SdtAlias>();
                                        string title = titleElement != null ? titleElement.Val.Value : string.Empty;

                                        var sdtContentRun = sdt.Descendants<Text>().FirstOrDefault();
                                        if (sdtContentRun != null)
                                        {
                                            var textElement = sdt.Descendants<Text>().FirstOrDefault();

                                            if (textElement != null)
                                            {
                                                if (title != "")
                                                {
                                                    textElement.Text = GET_VALUE_ByTitle(DATA, title);
                                                }
                                                else
                                                {
                                                    textElement.Text = DATA;
                                                }

                                                sdtContentRun.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
                                            }
                                        }
                                        else
                                        {
                                            var textElement = sdt.Descendants<Text>().FirstOrDefault();

                                            if (textElement != null)
                                            {
                                                if (title != "")
                                                {
                                                    textElement.Text = GET_VALUE_ByTitle(DATA, title);
                                                }
                                                else
                                                {
                                                    textElement.Text = DATA;
                                                }
                                            }

                                            sdt.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DOWNLOAD_REPORT(string filePath)
        {
            string PdfFilePath = "";
            string filename = Path.GetFileName(filePath);

            string FileEXT = filename.Split('.').Last();

            string PDFFILENAME = filename.Replace("." + FileEXT + "", ".pdf");

            PdfFilePath = Path.Combine(Server.MapPath(@"SAE Report Templates"), PDFFILENAME);

            try
            {

                ConvertTOPDF(filePath, PdfFilePath, Word.WdExportFormat.wdExportFormatPDF);

                //Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("NSAE_REPORTS.aspx", "") + "SAE Report Templates/" + PDFFILENAME);

                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + PDFFILENAME + "");
                Response.TransmitFile(PdfFilePath);
                Response.End();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Microsoft.Office.Interop.Word") || ex.Message.Contains("CLSID"))
                {
                    lblErrorMsg.Text = ex.Message.ToString();

                    Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("NSAE_REPORTS.aspx", "") + "SAE Report Templates/" + filename, false);
                }
                else
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
        }

        protected void GET_REPORTS()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_REPORT_SP(ACTION: "GET_ALL_REPORTS", TYPE: Session["UserType"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpReportType.DataSource = ds.Tables[0];
                    drpReportType.DataValueField = "ID";
                    drpReportType.DataTextField = "REPORT_NAME";
                    drpReportType.DataBind();
                }
                else
                {
                    drpReportType.Items.Clear();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(USERID: Session["User_ID"].ToString());
                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataValueField = "INVID";
                drpInvID.DataBind();

                FillSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_SAE_SUBJECTS",
                    INVID: drpInvID.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
                else
                {
                    drpSubID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_SAEID()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_REPORT_SP(ACTION: "GET_SAEIDS", SUBJID: drpSubID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSAEID.DataSource = ds.Tables[0];
                    drpSAEID.DataValueField = "SAEID";
                    drpSAEID.DataTextField = "SAEID";
                    drpSAEID.DataBind();
                    drpSAEID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
                else
                {
                    drpSAEID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GET_SAE_STATUS()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_REPORT_SP(ACTION: "GET_SAE_STATUS", SAEID: drpSAEID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlStatus.DataSource = ds;
                    ddlStatus.DataTextField = "STATUS";
                    ddlStatus.DataBind();
                }
                else
                {
                    ddlStatus.DataSource = null;
                    ddlStatus.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetData()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_SAE.SAE_REPORT_SP(ACTION: "GET_SAE_REPORT_DATA", SAEID: drpSAEID.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdReports.DataSource = ds;
                    grdReports.DataBind();
                }
                else
                {
                    grdReports.DataSource = null;
                    grdReports.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSubID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SAEID();

                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSAEID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetData();

                if (ddlStatus.Visible == true)
                {
                    GET_SAE_STATUS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpReportType.SelectedItem.Text == "CIOMS")
                {
                    divStatus.Visible = true;
                    GET_SAE_STATUS();
                }
                else
                {
                    divStatus.Visible = false;
                }

                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdReports_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                string SAE = Convert.ToString(e.CommandArgument);

                if (e.CommandName == "DonwloadReport")
                {
                    string SAEID = ((Label)grdReports.Rows[RowIndex].FindControl("lblSAEID")).Text;
                    string STATUS = ((Label)grdReports.Rows[RowIndex].FindControl("lblStatus")).Text;

                    GENERATE_REPORT(SAEID, STATUS);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
                {
                    //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gv.ShowFooter == true && gv.Rows.Count > 0)
                {
                    //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                    gv.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void grd_data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                string HIGHLIGHTROW = dr["HIGHLIGHTROW"].ToString();

                if (HIGHLIGHTROW == "1")
                {
                    e.Row.Attributes.Add("style", "color:Red;font-weight:bold;");
                }
            }
        }
    }
}