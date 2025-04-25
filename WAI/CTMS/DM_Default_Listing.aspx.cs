using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Ionic.Zip;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace CTMS
{
    public partial class DM_Default_Listing : System.Web.UI.Page
    {

        DAL dal = new DAL();
        DAL_DM dal_DM = new DAL_DM();
        CommonFunction.CommonFunction CF = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["UserGroup_ID"] == null)
                    {
                        Response.Redirect("SessionExpired.aspx", true);
                        return;
                    }

                    GET_MODULE();
                    GET_COUNTRY();
                    GET_INVID();
                    GET_SUBJECT();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GET_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP();
                drpCountry.DataSource = ds.Tables[0];
                drpCountry.DataTextField = "COUNTRYNAME";
                drpCountry.DataValueField = "COUNTRYID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_INVID()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(COUNTRYID: drpCountry.SelectedValue);
                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataTextField = "INVID";
                drpInvID.DataValueField = "INVID";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_MODULE()
        {
            try
            {
                DataSet ds = dal_DM.DM_LIST_SP(ACTION: "GET_MODULES");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddMODULE.DataSource = ds.Tables[0];
                    ddMODULE.DataValueField = "MODULEID";
                    ddMODULE.DataTextField = "MODULENAME";
                    ddMODULE.DataBind();
                    ddMODULE.Items.Insert(0, new ListItem("--Select Module--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_INVID();

                gridData.DataSource = null;
                gridData.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBJECT()
        {
            try
            {
                DataSet ds = dal.GET_SUBJECT_SP(INVID: drpInvID.SelectedValue);
                drpSubID.DataSource = ds.Tables[0];
                drpSubID.DataValueField = "SUBJID";
                drpSubID.DataBind();
                //drpSubID.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DM.DM_LIST_SP(
                    ACTION: "GET_DEFAULT_LIST",
                    MODULEID: ddMODULE.SelectedValue,
                    COUNTRYID: drpCountry.SelectedValue,
                    INVID: drpInvID.SelectedValue,
                    SUBJID: drpSubID.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gridData.DataSource = ds.Tables[0];
                    gridData.DataBind();
                }
                else
                {
                    gridData.DataSource = null;
                    gridData.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                gridData.DataSource = null;
                gridData.DataBind();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal_DM.DM_LIST_SP(
                    ACTION: "GET_DEFAULT_LIST",
                    MODULEID: ddMODULE.SelectedValue,
                    COUNTRYID: drpCountry.SelectedValue,
                    INVID: drpInvID.SelectedValue,
                    SUBJID: drpSubID.SelectedValue
                    );
                ds.Tables[0].TableName = ddMODULE.SelectedItem.Text;
                Multiple_Export_Excel.ToExcel(ds, ddMODULE.SelectedItem.Text + ".xls", Page.Response);
                Page.Response.End();
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
                GET_SUBJECT();

                gridData.DataSource = null;
                gridData.DataBind();
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

        protected void ddMODULE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DM.DM_LIST_SP(
                    ACTION: "GET_MODULE_DETAILS",
                    MODULEID: ddMODULE.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["EXT"].ToString() == "True")
                    {
                        drpCountry.Enabled = false;
                        drpInvID.Enabled = false;
                        drpSubID.Enabled = false;
                    }
                    else
                    {
                        drpCountry.Enabled = true;
                        drpInvID.Enabled = true;
                        drpSubID.Enabled = true;
                    }
                }
                else
                {
                    drpCountry.Enabled = true;
                    drpInvID.Enabled = true;
                    drpSubID.Enabled = true;
                }

                gridData.DataSource = null;
                gridData.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnArchival_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DM.DM_LIST_SP(ACTION: "GET_MODULES");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    List<DataSet> allDataSets = new List<DataSet>();
                    List<DataSet> moduleDataSets = new List<DataSet>();
                    DataTable dataTable = ds.Tables[0];
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string MODULENAME = dataTable.Rows[i]["MODULENAME"].ToString();
                        string MODULEID = dataTable.Rows[i]["MODULEID"].ToString();

                        moduleDataSets = GetDataSets(MODULENAME, MODULEID);

                        allDataSets.AddRange(moduleDataSets);
                    }

                    if (allDataSets.Count > 0)
                    {
                        string zipFilePath = ExportToZip(allDataSets);
                        DownloadZip(zipFilePath);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        static List<DataSet> GetDataSets(string MODULENAME, string MODULEID)
        {
            List<DataSet> dataSets = new List<DataSet>();

            try
            {
                DAL_DM dal_DM = new DAL_DM();
                DataSet ds = dal_DM.DM_LIST_SP(
                    ACTION: "GET_DEFAULT_LIST",
                    MODULEID: MODULEID
                );

                if (ds.Tables.Count > 0)
                {
                    string trimmedModuleName = MODULENAME.Length > 30 ? MODULENAME.Substring(0, 30) : MODULENAME;

                    ds.Tables[0].TableName = trimmedModuleName;
                    dataSets.Add(ds);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching dataset: " + ex.Message);
            }

            return dataSets;
        }
        public static string ExportToZip(List<DataSet> dataSets)
        {
            string zipFolderPath = HttpContext.Current.Server.MapPath("~/DM_Archival_Data");
            string zipFilePath = Path.Combine(zipFolderPath, "ExportedData.zip"); // Define ZIP file name

            
            if (!Directory.Exists(zipFolderPath))
            {
                Directory.CreateDirectory(zipFolderPath);
            }

            List<string> tempFiles = new List<string>();

            try
            {
                
                foreach (DataSet ds in dataSets)
                {
                    foreach (DataTable table in ds.Tables)
                    {
                        string excelFilePath = Path.Combine(zipFolderPath, $"{table.TableName}.xlsx");
                        tempFiles.Add(excelFilePath);

                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add(table.TableName);
                            int rowNumber = 1;
                            int colNumber = 1;

                            // Write headers
                            foreach (DataColumn column in table.Columns)
                            {
                                worksheet.Cell(rowNumber, colNumber).Value = column.ColumnName;
                                colNumber++;
                            }

                            rowNumber++;

                            // Write data
                            foreach (DataRow row in table.Rows)
                            {
                                colNumber = 1;
                                foreach (DataColumn column in table.Columns)
                                {
                                    if (column.DataType == typeof(string))
                                    {
                                        string cellValue = row[column]?.ToString() ?? "";

                                        if (cellValue.Length > 32767) // Excel limit
                                        {
                                            string textFilePath = Path.Combine(zipFolderPath, $"{table.TableName}_{column.ColumnName}_{rowNumber}.txt");
                                            //File.WriteAllText(textFilePath, cellValue); // Save full text in .txt file
                                            //tempFiles.Add(textFilePath);

                                            // Insert reference in Excel
                                            worksheet.Cell(rowNumber, colNumber).Value = $"Text in {Path.GetFileName(textFilePath)}";
                                        }
                                        else
                                        {
                                            worksheet.Cell(rowNumber, colNumber).Value = cellValue;
                                        }
                                    }
                                    else
                                    {
                                        worksheet.Cell(rowNumber, colNumber).Value = row[column].ToString();
                                    }

                                    colNumber++;
                                }

                                rowNumber++;
                            }
                            
                            workbook.SaveAs(excelFilePath);
                        }
                    }
                }

                // Create ZIP using Ionic.Zip
                using (ZipFile zip = new ZipFile())
                {
                    foreach (string filePath in tempFiles)
                    {
                        zip.AddFile(filePath, ""); // Add files to ZIP root
                    }

                    zip.Save(zipFilePath); // Save ZIP file
                }

                // Cleanup temporary Excel files
                foreach (string file in tempFiles)
                {
                    File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exporting ZIP: " + ex.Message);
            }

            return zipFilePath;
        }

        //private static DataTable TruncateLongText(DataTable table)
        //{
        //    DataTable newTable = table.Copy(); // Clone the original table

        //    foreach (DataRow row in newTable.Rows)
        //    {
        //        foreach (DataColumn column in newTable.Columns)
        //        {
        //            if (column.DataType == typeof(string)) // Only check string columns
        //            {
        //                string cellValue = row[column]?.ToString() ?? "";

        //                // Truncate if length > 32,767 characters
        //                if (cellValue.Length > 32767)
        //                {
        //                    row[column] = cellValue.Substring(0, 32767); // Trim to max limit
        //                }
        //            }
        //        }
        //    }

        //    return newTable;
        //}

        static void DownloadZip(string zipFilePath)
        {
            HttpContext context = HttpContext.Current;

            context.Response.Clear();
            context.Response.ContentType = "application/zip";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=Modules_Export.zip");
            context.Response.TransmitFile(zipFilePath);
            context.Response.End();
        }

        //protected void btnSASD_Click(object sender, EventArgs e)
        //{
        //    // Set up response
        //    Response.Clear();
        //    Response.ContentType = "application/x-sas-data";
        //    Response.AddHeader("Content-Disposition", "attachment; filename=yourdata.sas7bdat");

        //    // Create a MemoryStream to write the SASD data
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        // Write the DataTable to SASD format
        //        using (XportWriter writer = new XportWriter(memoryStream))
        //        {
        //            writer.Write(myDataTable);
        //        }

        //        // Write the MemoryStream to the response stream
        //        Response.BinaryWrite(memoryStream.ToArray());
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
    }
}