using ClosedXML.Excel;
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
    public partial class DownloadExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ds = new DataSet();
            DataTable dtInput = Session["excelData"] as DataTable;
            ds.Tables.Add(dtInput.Copy());
            ToExcel(ds, Request.QueryString["file"],Page.Response);
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
    }
}