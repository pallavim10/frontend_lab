using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using ExcelDataReader;
using CTMS.CommonFunction;
using ClosedXML.Excel;

namespace CTMS
{
    public partial class DB_UPLOAD_HELP_INSTRUCTION : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        DataTable DB_Instruction = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Uploadfile.Attributes["onchange"] = "UploadFile(this)";
                    Session.Remove("Downloaded");
                    Session.Remove("excelData");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["Downloaded"] == null)
                {
                    UploadRules();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void UploadRules()
        {
            DataTable excelData = new DataTable();
            try
            {
                excelData = (DataTable)ViewState["HelpInstruction_ExcelData"];

                if (excelData == null)
                {
                    Response.Write("<script> alert('Please Select File For Upload.');</script>");
                }
                else
                {
                    int ROWSCOUNT = 0;
                    //DataSet dsInstructions = dal_DB.DB_MODULE_SP(ACTION: "GET_DATA_INSTRUCTIONS");
                    DataSet dsModuleFields = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULE_FIELDS");

                    for (int i = 0; i < excelData.Rows.Count; i++)
                    {

                        DataRow[] dr = dsModuleFields.Tables[0].Select("[Module Name] = '" + excelData.Rows[i][drpModule.SelectedValue].ToString().Trim() + "' ");
                        //DataRow[] dr = dsModuleFields.Tables[0].Select("Module = '" + excelData.Rows[i][drpModule.SelectedValue].ToString().Trim() + "' ");

                        ROWSCOUNT += 1;

                        if (dr.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(dr[0]["ID"].ToString().Trim()))
                            {
                                dal_DB.DB_MODULE_SP(ACTION: "Insert_HelpData",
                                MODULEID: dr[0]["ID"].ToString().Trim(),
                                HelpDesc: excelData.Rows[i][drpCRF_Help_Instruction.SelectedValue].ToString().Trim(),
                                SAE_HelpDesc: excelData.Rows[i][drpPharma_Help_Instruction.SelectedValue].ToString().Trim()
                                );
                            }

                            excelData.Rows[i]["Upload result"] = "Help Instructions Uploaded.";
                        }
                        else
                        {
                            excelData.Rows[i]["Upload result"] = "This module is not available. Please check and upload again.";
                        }
                    }

                    SAVE_UPLOAD_FILE(excelData, lblHeader.InnerText);

                    // Export the data in a more efficient manner
                    if (ROWSCOUNT > 0)
                    {
                        Session["excelData"] = excelData;
                        //Multiple_Export_Excel.ToExcel(excelData, excelData.TableName.ToString() + ".xls", Page.Response);

                        string outputFileName = excelData.TableName.ToString();

                        string script = $@"window.location.href = 'DownloadExcel.aspx?file={outputFileName}';setTimeout(function() {{location.href = 'DB_UPLOAD_HELP_INSTRUCTION.aspx';}}, 1000);";

                        ScriptManager.RegisterStartupScript(this, GetType(), "downloadAndRefresh", script, true);
                        Session["Downloaded"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SAVE_UPLOAD_FILE(DataTable excelData, string HEADERNAME)
        {
            try
            {
                string FileName = excelData.TableName.ToString();

                if (excelData.TableName.Length > 30)
                {
                    excelData.TableName = excelData.TableName.Substring(0, 30);
                }

                //Saved Excel Data with upload status
                byte[] fileData;

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(excelData);

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        fileData = MyMemoryStream.ToArray();
                    }
                }

                dal_DB.DB_UPLOAD_SP(ACTION: "INSERT_UPLOAD_FILE_LOGS",
                   FileName: FileName,
                   ContentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                   fileData: fileData,
                   NEV_MENU_NAME: HEADERNAME
                   );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BulkInstructionpublish()
        {
            DAL dal = new DAL();
            SqlConnection con = new SqlConnection(dal.getconstr());

            var options = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.KeepIdentity;

            using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), options))
            {
                if (con.State != ConnectionState.Open) { con.Open(); }

                sqlbc.DestinationTableName = "HELP_SECTION_DATA";

                sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                sqlbc.ColumnMappings.Add("DATA", "DATA");
                sqlbc.ColumnMappings.Add("SAE_DATA", "SAE_DATA");
                sqlbc.WriteToServer(DB_Instruction);
                DB_Instruction.Clear();
            }
        }

        protected void ADD_NEW_ROW_RULE_DATA(string MODULE, string DATA, string SAE_DATA)
        {
            try
            {
                CREATE_DB_INSTURCTION_DT();

                DataRow myDataRow;
                myDataRow = DB_Instruction.NewRow();
                myDataRow["MODULEID"] = MODULE;
                myDataRow["DATA"] = DATA;
                myDataRow["SAE_DATA"] = SAE_DATA;
                DB_Instruction.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CREATE_DB_INSTURCTION_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (DB_Instruction.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    DB_Instruction.Columns.Add("MODULEID");
                    DB_Instruction.Columns.Add("DATA");
                    DB_Instruction.Columns.Add("SAE_DATA");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl.ToString());
        }

        protected void btnMappedInstructionColumns_Click(object sender, EventArgs e)
        {
            try
            {
                GET_RULES_DRP_COLS(Uploadfile);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void GET_RULES_DRP_COLS(FileUpload fileUpload)
        {
            try
            {
                DataTable excelData = new DataTable();
                string filename = fileUpload.FileName;

                if (filename != "")
                {
                    string tempPath = "ExcelData";
                    if (!Directory.Exists(tempPath))
                    {
                        DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                        info.Create();
                    }
                    string savepath = Server.MapPath(tempPath);
                    fileUpload.SaveAs(savepath + @"\" + filename);
                    string filePath = savepath + @"\" + filename;
                    //fileUpload.SaveAs(filePath);
                    DataTable dtexcel = new DataTable();
                    bool hasHeaders = false;
                    string HDR = hasHeaders ? "Yes" : "No";
                    string strConn;
                    if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() != ".csv")
                    {
                        if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                        else
                            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
                        OleDbConnection conn = new OleDbConnection(strConn);
                        conn.Open();
                        DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        //Looping Total Sheet of Xl File

                        //Looping a first Sheet of Xl File
                        DataRow schemaRow = schemaTable.Rows[0];
                        string sheet = schemaRow["TABLE_NAME"].ToString();
                        if (!sheet.EndsWith("_"))
                        {
                            string query = "SELECT  * FROM [" + sheet + "]";
                            OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                            dtexcel.Locale = CultureInfo.CurrentCulture;
                            daexcel.Fill(dtexcel);
                            excelData = dtexcel;
                        }
                        conn.Close();
                    }
                    else
                    {
                        try
                        {
                            string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\\;Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";

                            connection = String.Format(connection, Path.GetDirectoryName(filePath));

                            OleDbDataAdapter csvAdapter;
                            csvAdapter = new OleDbDataAdapter("SELECT * FROM [" + Path.GetFileName(filePath) + "]", connection);

                            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                            {
                                try
                                {
                                    csvAdapter.Fill(dtexcel);
                                    if ((dtexcel != null) && (dtexcel.Rows.Count > 0))
                                    {
                                        excelData = dtexcel;
                                    }
                                    else
                                    {
                                        String msg = "No records found";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(String.Format("Error reading Table {0}.\n{1}", Path.GetFileName(filePath), ex.Message));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblErrorMsg.Text = ex.Message.ToString();
                        }
                    }

                    excelData.TableName = filename;
                    excelData.Columns.Add("Upload result");

                    DataTable dtExcelSheet = new DataTable();
                    dtExcelSheet.Columns.Add("Column", typeof(String));
                    int cols = excelData.Columns.Count;
                    for (int i = 0; i < cols; i++)
                    {
                        dtExcelSheet.Rows.Add(excelData.Columns[i]);
                    }

                    BIND_DRP_COLS(drpModule, dtExcelSheet);
                    BIND_DRP_COLS(drpCRF_Help_Instruction, dtExcelSheet);
                    BIND_DRP_COLS(drpPharma_Help_Instruction, dtExcelSheet);



                    ViewState["HelpInstruction_ExcelData"] = excelData;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_DRP_COLS(DropDownList drp, DataTable dt)
        {
            try
            {
                drp.DataSource = dt;
                drp.DataValueField = "Column";
                drp.DataTextField = "Column";
                drp.DataBind();
                drp.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnExportInstructionSampleFile_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "EXPORT_INSTRUCTION_SAMPLE_FILE");

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                dal_DB.DB_DOWNLOAD_LOGS_SP(ACTION: "INSERT_DOWNLOAD_LOGS",
                    FIELNAME: "Help_Instruction Sample File.xls",
                    FUNCTIONNAME: "Help Instruction Sample File",
                    PAGENAME: Session["menu"].ToString()
                    );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Help_Instruction Sample File_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel_Restricted(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}