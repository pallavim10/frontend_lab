using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public class Multiple_Export_Excel
    {
        const int rowLimit = 65000;

        public static string PDFfileName;

        private static string getWorkbookTemplate()
        {
            var sb = new StringBuilder(818);
            sb.AppendFormat(@"<?xml version=""1.0""?>{0}", Environment.NewLine);
            sb.AppendFormat(@"<?mso-application progid=""Excel.Sheet""?>{0}", Environment.NewLine);
            sb.AppendFormat(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:o=""urn:schemas-microsoft-com:office:office""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:x=""urn:schemas-microsoft-com:office:excel""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:html=""http://www.w3.org/TR/REC-html40"">{0}", Environment.NewLine);
            sb.AppendFormat(@" <Styles>{0}", Environment.NewLine);
            sb.AppendFormat(@"  <Style ss:ID=""Default"" ss:Name=""Normal"">{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Alignment ss:Vertical=""Bottom""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Borders/>{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Interior/>{0}", Environment.NewLine);
            sb.AppendFormat(@"   <NumberFormat/>{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Protection/>{0}", Environment.NewLine);
            sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
            sb.AppendFormat(@"  <Style ss:ID=""s62"">{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""{0}", Environment.NewLine);
            sb.AppendFormat(@"    ss:Bold=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
            sb.AppendFormat(@"  <Style ss:ID=""s63"">{0}", Environment.NewLine);
            sb.AppendFormat(@"   <NumberFormat ss:Format=""Short Date""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
            sb.AppendFormat(@" </Styles>{0}", Environment.NewLine);
            sb.Append(@"{0}\r\n</Workbook>");
            return sb.ToString();
        }

        private static string replaceXmlChar(string input)
        {
            //input = input.Replace("&", "&amp");
            //input = input.Replace("<", "&lt;");
            //input = input.Replace(">", "&gt;");
            //input = input.Replace("\"", "&quot;");
            //input = input.Replace("'", "&apos;");
            //input = input.Replace("_", " ");
            return input;
        }

        private static string getCell(Type type, object cellData)
        {
            var data = (cellData is DBNull) ? "" : cellData;
            if (type.Name.Contains("Int") || type.Name.Contains("Double") || type.Name.Contains("Decimal")) return string.Format("<Cell><Data ss:Type=\"Number\">{0}</Data></Cell>", data);
            if (type.Name.Contains("Date") && data.ToString() != string.Empty)
            {
                return string.Format("<Cell ss:StyleID=\"s63\"><Data ss:Type=\"DateTime\">{0}</Data></Cell>", Convert.ToDateTime(data).ToString("yyyy-MM-dd"));
            }
            return string.Format("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(data.ToString()));
        }

        private static string getWorksheets(DataSet source)
        {
            var sw = new StringWriter();
            if (source == null || source.Tables.Count == 0)
            {
                sw.Write("<Worksheet ss:Name=\"Sheet1\">\r\n<Table>\r\n<Row><Cell><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                return sw.ToString();
            }
            foreach (DataTable dt in source.Tables)
            {
                if (dt.Rows.Count == 0)
                    sw.Write("<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) + "\">\r\n<Table>\r\n<Row><Cell  ss:StyleID=\"s62\"><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                else
                {
                    //write each row data                
                    var sheetCount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((i % rowLimit) == 0)
                        {
                            //add close tags for previous sheet of the same data table
                            if ((i / rowLimit) > sheetCount)
                            {
                                sw.Write("\r\n</Table>\r\n</Worksheet>");
                                sheetCount = (i / rowLimit);
                            }
                            sw.Write("\r\n<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) +
                                     (((i / rowLimit) == 0) ? "" : Convert.ToString(i / rowLimit)) + "\">\r\n<Table>");
                            //write column name row
                            sw.Write("\r\n<Row>");
                            foreach (DataColumn dc in dt.Columns)
                                sw.Write(string.Format("<Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(dc.ColumnName)));
                            sw.Write("</Row>");
                        }
                        sw.Write("\r\n<Row>");
                        foreach (DataColumn dc in dt.Columns)
                            sw.Write(getCell(dc.DataType, dt.Rows[i][dc.ColumnName]));
                        sw.Write("</Row>");
                    }
                    sw.Write("\r\n</Table>\r\n</Worksheet>");
                }
            }

            return sw.ToString();
        }



        public static string GetExcelXml(DataTable dtInput, string filename)
        {
            var excelTemplate = getWorkbookTemplate();
            var ds = new DataSet();
            ds.Tables.Add(dtInput.Copy());
            var worksheets = getWorksheets(ds);
            var excelXml = string.Format(excelTemplate, worksheets);
            return excelXml;
        }

        public static string GetExcelXml(DataSet dsInput, string filename)
        {
            var excelTemplate = getWorkbookTemplate();
            var worksheets = getWorksheets(dsInput);
            var excelXml = string.Format(excelTemplate, worksheets);
            return excelXml;
        }

        public static bool IsDecimalCol(object o)
        {
            decimal result_ignored;
            return o != null &&
              !(o is DBNull) &&
              decimal.TryParse(Convert.ToString(o), out result_ignored);
        }

        public static bool IsIntCol(object o)
        {
            int result_ignored;
            return o != null &&
              !(o is DBNull) &&
              int.TryParse(Convert.ToString(o), out result_ignored);
        }

        public static bool IsDateCol(object o)
        {
            DateTime result_ignored;
            return o != null &&
              !(o is DBNull) &&
              DateTime.TryParse(Convert.ToString(o), out result_ignored);
        }

        public static bool IsTimeCol(object o)
        {
            TimeSpan result_ignored;
            return o != null &&
              !(o is DBNull) &&
              TimeSpan.TryParse(Convert.ToString(o), out result_ignored);
        }

        public static void ToExcel(DataSet dsInput, string filename, HttpResponse response)
        {
            if (dsInput.Tables.Count == 1)
            {
                DataTable newDT = new DataTable();

                foreach (DataColumn dc in dsInput.Tables[0].Columns)
                {
                    DataRow[] rows = dsInput.Tables[0].Select(" [" + dc.ColumnName.ToString() + "] IS NOT NULL ");

                    var filteredDecimal = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsDecimalCol(r[dc.ColumnName]));
                    var filteredNumbers = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsIntCol(r[dc.ColumnName]));
                    var filteredDates = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsDateCol(r[dc.ColumnName]));
                    var filteredTimes = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsTimeCol(r[dc.ColumnName]));

                    if (rows.Length == 0)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                    else if (filteredNumbers.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                    else if (filteredDecimal.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                    else if (filteredTimes.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(TimeSpan));
                    }
                    else if (filteredDates.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(DateTime));
                    }
                    else
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                }

                foreach (DataRow row in dsInput.Tables[0].Rows)
                {
                    newDT.ImportRow(row);
                }

                newDT.TableName = dsInput.Tables[0].TableName;

                dsInput = new DataSet();

                dsInput.Tables.Add(newDT);
            }

            if (!filename.Contains(HttpContext.Current.Session["PROJECTIDTEXT"].ToString()))
            {
                filename = HttpContext.Current.Session["PROJECTIDTEXT"].ToString() + "_" + filename.Replace(".xls", "") + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            }

            filename = filename.Replace(".xls", "");

            foreach (DataTable dtInput in dsInput.Tables)
            {
                dtInput.TableName = dtInput.TableName.Replace("[", "");
                dtInput.TableName = dtInput.TableName.Replace("]", "");
                dtInput.TableName = dtInput.TableName.Replace("\\", "");
                dtInput.TableName = dtInput.TableName.Replace("/", "");
                dtInput.TableName = dtInput.TableName.Replace("*", "");
                dtInput.TableName = dtInput.TableName.Replace("?", "");
                dtInput.TableName = dtInput.TableName.Replace(":", ".");
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dsInput);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                response.Clear();
                response.Buffer = true;
                response.Charset = "";
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", "attachment;filename= " + filename + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);

                    // Append cookie
                    HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                    cookie.Value = "Flag";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    response.AppendCookie(cookie);
                    // end

                    MyMemoryStream.WriteTo(response.OutputStream);
                    response.Flush();
                    response.SuppressContent = true;
                    response.End();
                }
            }
        }

        public static void ToExcel_Restricted(DataSet dsInput, string filename, HttpResponse response)
        {
            if (dsInput.Tables.Count == 1)
            {
                DataTable newDT = new DataTable();

                foreach (DataColumn dc in dsInput.Tables[0].Columns)
                {
                    DataRow[] rows = dsInput.Tables[0].Select(" [" + dc.ColumnName.ToString() + "] IS NOT NULL ");

                    var filteredDecimal = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsDecimalCol(r[dc.ColumnName]));
                    var filteredNumbers = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsIntCol(r[dc.ColumnName]));
                    var filteredDates = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsDateCol(r[dc.ColumnName]));
                    var filteredTimes = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsTimeCol(r[dc.ColumnName]));

                    if (rows.Length == 0)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                    else if (filteredNumbers.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                    else if (filteredDecimal.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                    else if (filteredTimes.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(TimeSpan));
                    }
                    else if (filteredDates.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(DateTime));
                    }
                    else
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                }

                foreach (DataRow row in dsInput.Tables[0].Rows)
                {
                    newDT.ImportRow(row);
                }

                dsInput = new DataSet();

                dsInput.Tables.Add(newDT);
            }

            if (!filename.Contains(HttpContext.Current.Session["PROJECTIDTEXT"].ToString()))
            {
                filename = HttpContext.Current.Session["PROJECTIDTEXT"].ToString() + "_" + filename.Replace(".xls", "") + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            }

            filename = filename.Replace(".xls", "");

            foreach (DataTable dtInput in dsInput.Tables)
            {
                dtInput.TableName = dtInput.TableName.Replace("[", "");
                dtInput.TableName = dtInput.TableName.Replace("]", "");
                dtInput.TableName = dtInput.TableName.Replace("\\", "");
                dtInput.TableName = dtInput.TableName.Replace("/", "");
                dtInput.TableName = dtInput.TableName.Replace("*", "");
                dtInput.TableName = dtInput.TableName.Replace("?", "");
                dtInput.TableName = dtInput.TableName.Replace(":", ".");
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dsInput);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                foreach (IXLWorksheet worksheet in wb.Worksheets)
                {
                    for (int i = 0; i < worksheet.ColumnCount(); i++)
                    {
                        string CellValue = worksheet.Cell(2, i + 1).Value.ToString();

                        if (CellValue != "")
                        {
                            string range = "";
                            if (dsInput.Tables.Contains(CellValue))
                            {
                                for (int j = 0; j < dsInput.Tables.Count; j++)
                                {
                                    if (dsInput.Tables[j].TableName.ToString() == CellValue.ToString())
                                    {
                                        range = "a2:a" + (dsInput.Tables[j].Rows.Count + 1);
                                        //wb.Worksheet(j + 1).Delete();

                                        worksheet.Cell(2, i + 1).DataValidation.List(wb.Worksheet(j + 1).Range(range), true);

                                        worksheet.Cell(2, i + 1).Value = "";

                                        worksheet.ColumnWidth = 20;

                                        wb.Worksheet(j + 1).Hide();

                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (CellValue.Contains(","))
                                {
                                    var validOptions = $"\"{String.Join(",", CellValue.Split(',').ToList())}\"";

                                    worksheet.Cell(2, i + 1).DataValidation.List(validOptions, true);

                                    worksheet.Cell(2, i + 1).Value = "";

                                    worksheet.ColumnWidth = 20;
                                }
                            }
                        }
                    }
                }

                response.Clear();
                response.Buffer = true;
                response.Charset = "";
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", "attachment;filename= " + filename + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);

                    // Append cookie
                    HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                    cookie.Value = "Flag";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    response.AppendCookie(cookie);
                    // end

                    MyMemoryStream.WriteTo(response.OutputStream);
                    response.Flush();
                    response.SuppressContent = true;
                    response.End();
                }
            }
        }

        static string GetCellAddress(IXLWorksheet worksheet, int rowIndex, int columnIndex)
        {
            // Get the corresponding cell
            IXLCell cell = worksheet.Cell(rowIndex, columnIndex);

            // Get the cell address
            string cellAddress = cell.Address.ToString();

            return cellAddress;
        }

        public static void SaveExcel(DataSet dsInput, string filename, HttpResponse response, string DestPath)
        {
            if (dsInput.Tables.Count == 1)
            {
                DataTable newDT = new DataTable();

                foreach (DataColumn dc in dsInput.Tables[0].Columns)
                {
                    DataRow[] rows = dsInput.Tables[0].Select(" [" + dc.ColumnName.ToString() + "] IS NOT NULL ");

                    var filteredDecimal = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsDecimalCol(r[dc.ColumnName]));
                    var filteredNumbers = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsIntCol(r[dc.ColumnName]));
                    var filteredDates = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsDateCol(r[dc.ColumnName]));
                    var filteredTimes = dsInput.Tables[0].Rows.Cast<DataRow>().Where(r => IsTimeCol(r[dc.ColumnName]));

                    if (rows.Length == 0)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                    else if (filteredNumbers.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                    else if (filteredDecimal.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                    else if (filteredTimes.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(TimeSpan));
                    }
                    else if (filteredDates.ToArray().Length == rows.Length)
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(DateTime));
                    }
                    else
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                }

                foreach (DataRow row in dsInput.Tables[0].Rows)
                {
                    newDT.ImportRow(row);
                }

                dsInput = new DataSet();

                dsInput.Tables.Add(newDT);
            }

            filename = filename.Replace(".xls", "");

            foreach (DataTable dtInput in dsInput.Tables)
            {
                dtInput.TableName = dtInput.TableName.Replace("[", "");
                dtInput.TableName = dtInput.TableName.Replace("]", "");
                dtInput.TableName = dtInput.TableName.Replace("\\", "");
                dtInput.TableName = dtInput.TableName.Replace("/", "");
                dtInput.TableName = dtInput.TableName.Replace("*", "");
                dtInput.TableName = dtInput.TableName.Replace("?", "");
                dtInput.TableName = dtInput.TableName.Replace(":", ".");
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                //string WritePassword = "Neeraj";
                wb.Worksheets.Add(dsInput);
                // wb.Worksheets.Add(WritePassword.ToString());
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                //response.Clear();
                //response.Buffer = true;
                //response.Charset = "";
                //response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //response.AddHeader("content-disposition", "attachment;filename= " + filename + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {

                    wb.SaveAs(DestPath + "\\" + filename + ".xlsx");

                    //MyMemoryStream.WriteTo(response.OutputStream);
                    //response.Flush();
                    //response.End();
                }
            }
        }

        public static bool ISDATE(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumeric(string value)
        {
            try
            {
                int number;
                bool result = int.TryParse(value, out number);
                return result;
            }
            catch (Exception ex) { return false; }
        }

        public static void ToExcel(DataTable dtInput, string filename, HttpResponse response)
        {
            var ds = new DataSet();
            ds.Tables.Add(dtInput.Copy());
            ToExcel(ds, filename, response);
        }

        public static void ExportToPDF(DataTable data, string filename, HttpResponse response)
        {
            //PDFfileName = replaceXmlChar(filename);
            //Document document = new Document(PageSize.A4.Rotate(), 10f, 10f, 60f, 60f);
            //PdfWriter writer = PdfWriter.GetInstance(document, response.OutputStream);


            //writer.PageEvent = new PDFFooter();

            //document.Open();
            //iTextSharp.text.Font headerFont = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_BOLD, 10, Font.UNDERLINE);
            //iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 7);
            //iTextSharp.text.Font myFont = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);

            //PdfPTable table = new PdfPTable(data.Columns.Count);

            //table.WidthPercentage = 100;
            //PdfPCell cell = new PdfPCell();

            //cell.Colspan = data.Columns.Count;

            //foreach (DataColumn c in data.Columns)
            //{
            //    table.AddCell(new Phrase(replaceXmlChar(c.ColumnName), myFont));
            //}

            //int i = data.Columns.Count;
            //foreach (DataRow r in data.Rows)
            //{
            //    if (data.Rows.Count > 0)
            //    {
            //        for (int j = 0; j < i; j++)
            //        {
            //            table.AddCell(new Phrase(r[j].ToString(), font5));
            //        }
            //    }
            //}

            //document.Add(table);
            //document.Add(new Phrase());
            //document.Add(new Phrase());
            //document.Close();

            //response.ContentType = "application/pdf";
            //response.AddHeader("content-disposition", "attachment; filename= " + filename + ".pdf");

            //// Append cookie
            //HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            //cookie.Value = "Flag";
            //cookie.Expires = DateTime.Now.AddDays(1);
            //response.AppendCookie(cookie);
            //// end

            //response.End();
        }

        public static void ExportToWord(DataTable data, string filename, HttpResponse response)
        {
            StringBuilder sbDocBody = new StringBuilder(); ;
            try
            {
                GridView GridView1 = new GridView();
                GridView1.Attributes.Add("class", "table table-bordered table-striped");
                GridView1.AllowPaging = false;
                GridView1.DataSource = data;
                GridView1.DataBind();

                response.Clear();
                response.Buffer = true;
                response.AddHeader("content-disposition", "attachment;filename=" + filename + ".doc");
                response.Charset = "";
                response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView1.RenderControl(hw);

                // Append cookie
                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                response.AppendCookie(cookie);
                // end

                response.Output.Write(sw.ToString());
                response.Flush();
                response.End();
            }
            catch (Exception ex)
            {
                // Ignore this error as this is caused due to termination of the Response Stream.
            }
        }

        public static void ExportToRTF(DataTable data, string filename, HttpResponse response)
        {
            StringBuilder sbDocBody = new StringBuilder(); ;
            try
            {
                GridView GridView1 = new GridView();
                GridView1.Attributes.Add("class", "table table-bordered table-striped");
                GridView1.AllowPaging = false;
                GridView1.DataSource = data;
                GridView1.DataBind();

                response.Clear();
                response.Buffer = true;
                response.AddHeader("content-disposition", "attachment;filename=" + filename + ".rtf");
                response.Charset = "";
                response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView1.RenderControl(hw);

                // Append cookie
                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                response.AppendCookie(cookie);
                // end

                response.Output.Write(sw.ToString());
                response.Flush();
                response.End();
            }
            catch (Exception ex)
            {
                // Ignore this error as this is caused due to termination of the Response Stream.
            }
        }

        public static string GET_PROJECT_NAME()
        {
            if (HttpContext.Current.Session["PROJECTIDTEXT"] == null)
            {
                return "";
            }
            else
            {
                return HttpContext.Current.Session["PROJECTIDTEXT"].ToString();
            }
        }

        public static string GET_SPONSOR_NAME()
        {
            if (HttpContext.Current.Session["SPONSORNAME"] == null)
            {
                return "";
            }
            else
            {
                return HttpContext.Current.Session["SPONSORNAME"].ToString();
            }
        }
    }
}