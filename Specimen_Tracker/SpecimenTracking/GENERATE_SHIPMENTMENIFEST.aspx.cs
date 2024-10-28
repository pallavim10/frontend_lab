using DocumentFormat.OpenXml.Office2010.Word;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace SpecimenTracking
{
    public partial class GENERATE_SHIPMENTMENIFEST : System.Web.UI.Page
    {
        DAL_MF Dal_MF = new DAL_MF();
        DAL_DE Dal_DE = new DAL_DE();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SITE();
                    GET_SPECIMENTYPE();
                    GET_VISIT();
                    GET_ALIQUOT();
                    GET_LABS();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_SITE()
        {
            try
            {
                drpsite.Items.Clear();
                DataSet ds = Dal_DE.GET_SITEID_SP();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpsite.DataSource = ds.Tables[0];
                    drpsite.DataValueField = "SiteID";
                    drpsite.DataBind();

                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        drpsite.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_LABS()
        {
            try
            {
                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_LABS");
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpLab.DataSource = ds.Tables[0];
                    drpLab.DataValueField = "ID";
                    drpLab.DataTextField = "NAME";
                    drpLab.DataBind();

                    drpLab.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_SPECIMENTYPE()
        {
            try
            {
                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_SPECIMENTYPE");
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {

                    DivSpeciType.Visible = true;
                    drpspecimentype.DataSource = ds.Tables[0];
                    drpspecimentype.DataValueField = "OPTION_VALUE";
                    drpspecimentype.DataTextField = "OPTION_VALUE";
                    drpspecimentype.DataBind();

                    drpspecimentype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
                else
                {
                    DivSpeciType.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_VISIT()
        {
            try
            {
                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_VISIT");

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {

                    drpvisit.DataSource = ds.Tables[0];
                    drpvisit.DataValueField = "VISITNUM";
                    drpvisit.DataTextField = "VISITNAME";
                    drpvisit.DataBind();

                    drpvisit.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_ALIQUOT()
        {
            try
            {
                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_ALIQUOT");

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpAliquottype.DataSource = ds.Tables[0];
                    drpAliquottype.DataValueField = "ALIQUOTTYPE";
                    drpAliquottype.DataTextField = "ALIQUOTTYPE";
                    drpAliquottype.DataBind();

                    drpAliquottype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string ALIQUOTID = CHECK_ALIQUOTID();

                if (ALIQUOTID != "")
                {
                    DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(
                        ACTION: "GET_SHIPMENT_DATA",
                        SITEID: drpsite.SelectedValue,
                        SPECTYP: drpspecimentype.SelectedValue,
                        VISITNUM: drpvisit.SelectedValue,
                        VISIT: drpvisit.SelectedItem.Text,
                        ALIQUOTTYPE: drpAliquottype.SelectedValue,
                        ALIQUOTID: ALIQUOTID,
                        FROMDAT: txtFormDate.Text,
                        TODAT: txtToDate.Text,
                        LABID: drpLab.SelectedValue,
                        LABNAME: drpLab.SelectedItem.Text
                        );

                    if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        GENERATE_REPORT(ds);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Specimen ID not available', 'warning');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please select atleast one Aliquot ID.', 'warning');", true);
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private string Save_Report()
        {
            string FilePath = "";


            try
            {
                DataSet dsReport = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_REPORT_FORMAT");

                if (dsReport.Tables.Count > 0 && dsReport.Tables[0].Rows.Count > 0)
                {
                    string FileEXT = dsReport.Tables[0].Rows[0]["FILENAME"].ToString().Split('.').Last();

                    byte[] FileData = (byte[])dsReport.Tables[0].Rows[0]["DATA_TYPE"];

                    string FILENAME = dsReport.Tables[0].Rows[0]["FILENAME"].ToString().Replace("." + FileEXT, "") + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + "." + FileEXT;

                    FilePath = Path.Combine(Server.MapPath(@"Shipment Manifest"), FILENAME);

                    File.WriteAllBytes(FilePath, FileData);

                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }

            return FilePath;

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

        private void GENERATE_REPORT(DataSet ds)
        {
            try
            {
                string uncheckValue = "☐";
                //string checkValue = "☒";
                string checkValue = "\u2611";

                string TemplatePath = Save_Report();

                using (WordprocessingDocument copyDoc = WordprocessingDocument.Open(TemplatePath, true))
                {
                    if (ds.Tables.Count > 0)
                    {
                        SET_PROJECT_SITE_DETAILS(copyDoc, ds.Tables[0]);

                        SET_SHIPMENT_DATA(copyDoc, ds.Tables[1]);
                    }
                }

                DOWNLOAD_REPORT(TemplatePath);

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void SET_PROJECT_SITE_DETAILS(WordprocessingDocument copyDoc, DataTable dt)
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
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void SET_SHIPMENT_DATA(WordprocessingDocument copyDoc, DataTable dt)
        {
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

        private void DOWNLOAD_REPORT(string filePath)
        {
            string PdfFilePath = "";
            string filename = Path.GetFileName(filePath);

            string FileEXT = filename.Split('.').Last();

            string PDFFILENAME = filename.Replace("." + FileEXT + "", ".pdf");

            PdfFilePath = Path.Combine(Server.MapPath(@"Shipment Manifest"), PDFFILENAME);

            try
            {
                ConvertTOPDF(filePath, PdfFilePath, Word.WdExportFormat.wdExportFormatPDF);

                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + PDFFILENAME + "");
                Response.TransmitFile(PdfFilePath);
                Response.End();

                Dal_MF.SHIPMENT_MENIFEST_SP(
                        ACTION: "INSERT_MANIFEST",
                        SITEID: drpsite.SelectedValue,
                        SPECTYP: drpspecimentype.SelectedValue,
                        VISITNUM: drpvisit.SelectedValue,
                        VISIT: drpvisit.SelectedItem.Text,
                        ALIQUOTTYPE: drpAliquottype.SelectedValue,
                        ALIQUOTID: CHECK_ALIQUOTID(),
                        FROMDAT: txtFormDate.Text,
                        TODAT: txtToDate.Text,
                        LABID: drpLab.SelectedValue,
                        LABNAME: drpLab.SelectedItem.Text
                        );

                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("GENERATE_SHIPMENTMENIFEST.aspx", "") + "Shipment Manifest/" + filename, false);

                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Shipment Manifest generated successfully',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });", true);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Microsoft.Office.Interop.Word") || ex.Message.Contains("CLSID"))
                {
                    Dal_MF.SHIPMENT_MENIFEST_SP(
                            ACTION: "INSERT_MANIFEST",
                            SITEID: drpsite.SelectedValue,
                            SPECTYP: drpspecimentype.SelectedValue,
                            VISITNUM: drpvisit.SelectedValue,
                            VISIT: drpvisit.SelectedItem.Text,
                            ALIQUOTTYPE: drpAliquottype.SelectedValue,
                            ALIQUOTID: CHECK_ALIQUOTID(),
                            FROMDAT: txtFormDate.Text,
                            TODAT: txtToDate.Text,
                            LABID: drpLab.SelectedValue,
                            LABNAME: drpLab.SelectedItem.Text
                            );

                    Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("GENERATE_SHIPMENTMENIFEST.aspx", "") + "Shipment Manifest/" + filename, false);

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Shipment Manifest generated successfully',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });", true);
                }
                else
                {
                    ExceptionLogging.SendErrorToText(ex);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Shipmen Manifest not generated. Error " + ex.Message.ToString() + "', 'warning');", true);
                }

                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Shipmen Manifest not generated. Error " + ex.Message.ToString() + "', 'warning');", true);
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

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("GENERATE_SHIPMENTMENIFEST.aspx");
        }

        private void CLEAR()
        {
            drpspecimentype.ClearSelection();
            drpAliquottype.ClearSelection();
            drpvisit.ClearSelection();
        }

        private string CHECK_ALIQUOTID()
        {
            string ALIQUOTID = "";
            try
            {
                foreach (ListItem item in lstAliquotID.Items)
                {
                    if (item.Selected)
                    {
                        if (ALIQUOTID == "")
                        {
                            ALIQUOTID = item.Text;
                        }
                        else
                        {
                            ALIQUOTID = ALIQUOTID + "," + item.Text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }
            return ALIQUOTID;
        }

        protected void lbtnShowData_Click(object sender, EventArgs e)
        {
            try
            {
                string ALIQUOTID = CHECK_ALIQUOTID();

                if (ALIQUOTID != "")
                {
                    DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(
                                ACTION: "SHOW_SHIPMENT_DATA",
                                SITEID: drpsite.SelectedValue,
                                SPECTYP: drpspecimentype.SelectedValue,
                                VISITNUM: drpvisit.SelectedValue,
                                VISIT: drpvisit.SelectedItem.Text,
                                ALIQUOTTYPE: drpAliquottype.SelectedValue,
                                ALIQUOTID: ALIQUOTID,
                                FROMDAT: txtFormDate.Text,
                                TODAT: txtToDate.Text,
                                LABID: drpLab.SelectedValue
                                );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Grid_Data.DataSource = ds;
                        Grid_Data.DataBind();
                        divRecord.Visible = true;
                    }
                    else
                    {
                        Grid_Data.DataSource = null;
                        Grid_Data.DataBind();
                        divRecord.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please select atleast one Aliquot ID.', 'warning');", true);
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }
        }

        protected void Grid_Data_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
                {

                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gv.ShowFooter == true && gv.Rows.Count > 0)
                {

                    gv.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }
        }

        protected void Grid_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = (sender as GridView);

                if (Session["SID_ACTIVE"].ToString() == "False")
                {
                    gv.HeaderRow.Cells[2].Visible = false;
                    e.Row.Cells[2].Visible = false;
                }
                gv.HeaderRow.Cells[3].Text = Session["Subject ID"].ToString();
            }
        }

        protected void drpAliquottype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lstAliquotID.Items.Clear();

                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_ALIQUOTID", ALIQUOTTYPE: drpAliquottype.SelectedValue);

                lstAliquotID.DataSource = ds.Tables[0];
                lstAliquotID.DataValueField = "ALIQUOTID";
                lstAliquotID.DataTextField = "ALIQUOTID";
                lstAliquotID.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }
        }
    }
}