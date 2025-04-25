using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;
using Word = Microsoft.Office.Interop.Word;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;
using ListItem = System.Web.UI.WebControls.ListItem;


namespace CTMS
{
    public partial class NIWRS_UNBLIND_LIST : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    grdUNBLINDED.Columns[2].HeaderText = Session["SUBJECTTEXT"].ToString();
                    GetSites();
                    GET_SUBSITE();
                    GET_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetSites()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        drpSite.DataSource = ds.Tables[0];
                        drpSite.DataValueField = "INVNAME";
                        drpSite.DataBind();
                    }
                    else
                    {
                        drpSite.DataSource = ds.Tables[0];
                        drpSite.DataValueField = "INVNAME";
                        drpSite.DataBind();
                        drpSite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBSITE()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_SUBSITE", SITEID: drpSite.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        drpSubSite.DataSource = ds.Tables[0];
                        drpSubSite.DataValueField = "SubSiteID";
                        drpSubSite.DataTextField = "SubSiteID";
                        drpSubSite.DataBind();
                    }
                    else
                    {
                        drpSubSite.DataSource = ds.Tables[0];
                        drpSubSite.DataValueField = "SubSiteID";
                        drpSubSite.DataTextField = "SubSiteID";
                        drpSubSite.DataBind();
                        drpSubSite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
                else
                {
                    drpSubSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DATA()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UNBLIND_SP(ACTION: "GET_UNBLINDED_LIST", SITEID: drpSite.SelectedValue, SUBSITEID: drpSubSite.SelectedValue);
                grdUNBLINDED.DataSource = ds;
                grdUNBLINDED.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBSITE();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSubSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUNBLINDED_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string REPORT = dr["REPORT"].ToString();

                    LinkButton lbtnDownload = (LinkButton)e.Row.FindControl("lbtnDownload");

                    if (REPORT == "True")
                    {
                        lbtnDownload.Visible = true;
                    }
                    else
                    {
                        lbtnDownload.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GrdData_PreRender(object sender, EventArgs e)
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

        protected void grdUNBLINDED_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string SUBJID = e.CommandArgument.ToString();

            if (e.CommandName == "Download")
            {
                DataSet ds = dal_IWRS.IWRS_UNBLIND_REPORT_SP(SUBJID: SUBJID);

                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    GENERATE_REPORT(ds);
                }

            }

        }

        private string Save_Report()
        {
            string FilePath = "";

            try
            {
                DataSet dsReport = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "DOWNLOAD_REPORT_TEMPLATE");

                if (dsReport.Tables.Count > 0 && dsReport.Tables[0].Rows.Count > 0)
                {
                    string FileEXT = dsReport.Tables[0].Rows[0]["FILENAME"].ToString().Split('.').Last();

                    byte[] FileData = (byte[])dsReport.Tables[0].Rows[0]["DATA"];

                    string FILENAME = dsReport.Tables[0].Rows[0]["FILENAME"].ToString().Replace("." + FileEXT, "") + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + "." + FileEXT;

                    FilePath = Path.Combine(Server.MapPath(@"UNBL_RPT_TEMPLATE"), FILENAME);

                    File.WriteAllBytes(FilePath, FileData);

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

            return FilePath;

        }

        private void SET_SITE_INV_DETAILS(WordprocessingDocument copyDoc, DataTable dt)
        {
            try
            {
                string uncheckValue = "☐";
                //string checkValue = "☒";
                string checkValue = "\u2611";

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
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void SET_PROJECT_SPONSOR_DETAILS(WordprocessingDocument copyDoc, DataTable dt)
        {
            try
            {
                string uncheckValue = "☐";
                //string checkValue = "☒";
                string checkValue = "\u2611";

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
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
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


        private void SET_UNBLINDING_DETAILS(WordprocessingDocument copyDoc, DataTable dt)
        {
            string targetTagName = "UNBLIND_DATA";

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
                foreach (DataRow drDATA in dt.Rows)
                {
                    var clonedSDT = (SdtElement)sdt.CloneNode(true);

                    if (clonedSDT != null)
                    {
                        List<Text> textElements = clonedSDT.Descendants<Text>().ToList();

                        foreach (Text element in textElements)
                        {
                            if (element != null)
                            {
                                foreach (DataColumn dcDATA in dt.Columns)
                                {
                                    element.Text = element.Text.Replace(dcDATA.ColumnName, drDATA[dcDATA.ColumnName].ToString());
                                }
                            }
                        }

                        sdt.Parent.InsertAfter(clonedSDT, sdt);
                    }
                }

                sdt.Remove();
            }
        }

        private void SET_SUBJECT_DATA(WordprocessingDocument copyDoc, DataTable dt)
        {
            string targetTagName = "SUBJECT_DATA";

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
                foreach (DataRow drDATA in dt.Rows)
                {
                    var clonedSDT = (SdtElement)sdt.CloneNode(true);

                    if (clonedSDT != null)
                    {
                        List<Text> textElements = clonedSDT.Descendants<Text>().ToList();

                        foreach (Text element in textElements)
                        {
                            if (element != null)
                            {
                                foreach (DataColumn dcDATA in dt.Columns)
                                {
                                    if (element.Text == dcDATA.ColumnName)
                                    {
                                        element.Text = element.Text.Replace(dcDATA.ColumnName, drDATA[dcDATA.ColumnName].ToString());
                                        
                                    }
                                }

                            }
                        }

                        sdt.Parent.InsertAfter(clonedSDT, sdt);
                    }
                }

                sdt.Remove();
            }
        }

        private void SET_KIT_DATA(WordprocessingDocument copyDoc, DataTable dt)
        {
            string targetTagName = "KIT_DATA";

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
                foreach (DataRow drDATA in dt.Rows)
                {
                    var clonedSDT = (SdtElement)sdt.CloneNode(true);

                    if (clonedSDT != null)
                    {
                        List<Text> textElements = clonedSDT.Descendants<Text>().ToList();

                        foreach (Text element in textElements)
                        {
                            if (element != null)
                            {
                                foreach (DataColumn dcDATA in dt.Columns)
                                {
                                    element.Text = element.Text.Replace(dcDATA.ColumnName, drDATA[dcDATA.ColumnName].ToString());
                                }
                            }
                        }

                        sdt.Parent.InsertAfter(clonedSDT, sdt);
                    }
                }

                sdt.Remove();
            }
        }

        private void GENERATE_REPORT(DataSet ds)
        {
            try
            {
                string TemplatePath = Save_Report();

                using (WordprocessingDocument copyDoc = WordprocessingDocument.Open(TemplatePath, true))
                {
                    if (ds.Tables.Count > 0)
                    {
                        SET_PROJECT_SPONSOR_DETAILS(copyDoc, ds.Tables[0]);

                        SET_SITE_INV_DETAILS(copyDoc, ds.Tables[1]);

                        SET_UNBLINDING_DETAILS(copyDoc, ds.Tables[2]);

                        SET_KIT_DATA(copyDoc, ds.Tables[3]);

                        SET_SUBJECT_DATA(copyDoc, ds.Tables[4]);
                    }
                }

                DOWNLOAD_REPORT(TemplatePath);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void DOWNLOAD_REPORT(string filePath)
        {
            string PdfFilePath = "";
            string filename = Path.GetFileName(filePath);

            string FileEXT = filename.Split('.').Last();

            string PDFFILENAME = filename.Replace("." + FileEXT + "", ".pdf");

            PdfFilePath = Path.Combine(Server.MapPath(@"UNBL_RPT_TEMPLATE"), PDFFILENAME);

            try
            {
                ConvertTOPDF(filePath, PdfFilePath, Word.WdExportFormat.wdExportFormatPDF);

                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + PDFFILENAME + "");
                Response.TransmitFile(PdfFilePath);
                Response.End();

                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("NIWRS_UNBLIND_LIST.aspx", "") + "UNBL_RPT_TEMPLATE/" + filename, false);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Microsoft.Office.Interop.Word") || ex.Message.Contains("CLSID"))
                {
                    Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("NIWRS_UNBLIND_LIST.aspx", "") + "UNBL_RPT_TEMPLATE/" + filename, false);
                }
                else
                {
                    lblErrorMsg.Text = ex.ToString();
                }
            }
        }

        private bool ConvertTOPDF(string sourcePath, string targetPath, Word.WdExportFormat exportFormat)
        {
            bool result;
            object paramMissing = Type.Missing;
            Microsoft.Office.Interop.Word.Application wordApplication = new Microsoft.Office.Interop.Word.Application();
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
                Word.WdExportItem paramExportItem = Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent;
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


    }
}