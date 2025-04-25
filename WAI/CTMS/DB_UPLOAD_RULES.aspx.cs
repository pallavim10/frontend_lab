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
    public partial class DB_UPLOAD_RULES : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        DataTable DM_RULE = new DataTable();
        DataTable DM_RULE_VAR = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Uploadfile.Attributes["onchange"] = "UploadFile(this)";
                    FileUploadRuleVariable.Attributes["onchange"] = "UploadFile1(this)";
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
                excelData = (DataTable)ViewState["RuleRexcelData"];

                if (excelData == null)
                {
                    Response.Write("<script> alert('Please Select File For Upload.');</script>");
                }
                else
                {
                    string ExistsRuleID = "", Generate_Query = "", Set_Field_Value = "";
                    int ROWSCOUNT = 0;
                    DataSet dsRules = dal_DB.DB_UPLOAD_SP(ACTION: "GET_RULES_FOR_UPLOAD");
                    DataSet dsModuleFields = dal_DB.DB_UPLOAD_SP(ACTION: "GET_MODULE_FIELDS");


                    for (int i = 0; i < excelData.Rows.Count; i++)
                    {
                        DataRow[] dr = dsRules.Tables[0].Select("[RULEID] = '" + excelData.Rows[i][drpRuleRuleid.SelectedValue].ToString().Trim() + "'");

                        if (dr.Length > 0)
                        {
                            excelData.Rows[i]["Upload result"] = "Rule Id already present in the database. \\nPlease check your data and upload again with a unique Rule Id.";

                            //if (ExistsRuleID == "")
                            //{


                            //    ExistsRuleID = excelData.Rows[i][drpRuleRuleid.SelectedValue].ToString().Trim();
                            //}
                            //else
                            //{
                            //    ExistsRuleID += ", " + excelData.Rows[i][drpRuleRuleid.SelectedValue].ToString().Trim();
                            //}
                        }
                        else
                        {
                            DataRow[] dr1 = dsModuleFields.Tables[0].Select("DOMAIN = '" + excelData.Rows[i][drpRuleDomain.SelectedValue].ToString().Trim() + "'  AND VARIABLENAME = '" + excelData.Rows[i][drpRuleVariable.SelectedValue].ToString().Trim() + "'");

                            DataRow[] dr2 = dsModuleFields.Tables[0].Select("VISIT = '" + excelData.Rows[i][drpRuleVisit.SelectedValue].ToString().Trim() + "' ");

                            if (excelData.Rows[i][drpRuleAction.SelectedValue].ToString().Trim() == "Generate Query")
                            {
                                Generate_Query = "1";
                                Set_Field_Value = "0";
                            }
                            else if (excelData.Rows[i][drpRuleAction.SelectedValue].ToString().Trim() == "Set Field Value")
                            {
                                Generate_Query = "0";
                                Set_Field_Value = "1";
                            }

                            if (dr1.Length > 0)
                            {
                                if (dr2.Length > 0)
                                {
                                    ROWSCOUNT += 1;

                                    ADD_NEW_ROW_RULE_DATA(
                                    excelData.Rows[i][drpRuleRuleid.SelectedValue].ToString().Trim(),
                                    excelData.Rows[i][drpRuleSeqno.SelectedValue].ToString().Trim(),
                                    dr1[0]["MODULEID"].ToString(),
                                    dr2[0]["VISITNUM"].ToString(),
                                    dr1[0]["FIELD_ID"].ToString(),
                                    dr1[0]["TABLENAME"].ToString(),
                                    excelData.Rows[i][drpRuleVariable.SelectedValue].ToString().Trim(),
                                    excelData.Rows[i][drpRuleDomain.SelectedValue].ToString().Trim(),
                                    excelData.Rows[i][drpRuleNature.SelectedValue].ToString().Trim(),
                                    Generate_Query,
                                    Set_Field_Value,
                                    excelData.Rows[i][drpRuleDescripation.SelectedValue].ToString().Trim(),
                                    excelData.Rows[i][drpRuleQuerytext.SelectedValue].ToString().Trim(),
                                    excelData.Rows[i][drpRuleCondition.SelectedValue].ToString().Trim(),
                                    excelData.Rows[i][drpRuleFormulaSetValue.SelectedValue].ToString().Trim()
                                    );

                                    excelData.Rows[i]["Upload result"] = "Rule Uploaded.";
                                }
                                else
                                {
                                    excelData.Rows[i]["Upload result"] = "Visit not available in database.";
                                }
                            }
                            else
                            {
                                excelData.Rows[i]["Upload result"] = "Module Domain and Field Variable not available in database.";
                            }
                        }
                    }

                    if (DM_RULE.Rows.Count > 0)
                    {
                        blukRulepublish();
                    }

                    SAVE_UPLOAD_FILE(excelData, lblHeader.InnerText);

                    string MSG = "Total " + ROWSCOUNT + " records uploaded successfully. \\n\\nPlease find donwloaded Upload summary.";

                    Session["excelData"] = excelData;
                    //Multiple_Export_Excel.ToExcel(excelData, excelData.TableName.ToString() + ".xls", Page.Response);

                    string outputFileName = excelData.TableName.ToString();

                    string script = $@"window.location.href = 'DownloadExcel.aspx?file={outputFileName}';setTimeout(function() {{location.href = 'DM_UPLOAD_MODULE_FIELDS.aspx';}}, 1000);";

                    ScriptManager.RegisterStartupScript(this, GetType(), "downloadAndRefresh", script, true);
                    Session["Downloaded"] = true;
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

        protected void ADD_NEW_ROW_RULE_DATA(string RULEID, string SEQNO, string MODULEID, string VISITNUM, string FIELDID, string TABLENAME, string VARIABLENAME, string DOMAIN, string Nature, string GEN_QUERY, string SET_VALUE, string Description, string QueryText, string Condition, string FORMULA_VALUE)
        {
            try
            {
                CREATE_DM_RULE_DT();

                DataRow myDataRow;
                myDataRow = DM_RULE.NewRow();
                myDataRow["RULEID"] = RULEID;
                myDataRow["SEQNO"] = SEQNO;
                myDataRow["MODULEID"] = MODULEID;
                myDataRow["VISITNUM"] = VISITNUM;
                myDataRow["FIELDID"] = FIELDID;
                myDataRow["TABLENAME"] = TABLENAME;
                myDataRow["VARIABLENAME"] = VARIABLENAME;
                myDataRow["DOMAIN"] = DOMAIN;
                myDataRow["Nature"] = Nature;
                myDataRow["GEN_QUERY"] = Convert.ToBoolean(checkboolstring(GEN_QUERY));
                myDataRow["SET_VALUE"] = Convert.ToBoolean(checkboolstring(SET_VALUE));
                myDataRow["Description"] = Description;
                myDataRow["QueryText"] = QueryText;
                myDataRow["Condition"] = Condition;
                myDataRow["FORMULA_VALUE"] = FORMULA_VALUE;
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                DM_RULE.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CREATE_DM_RULE_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (DM_RULE.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    DM_RULE.Columns.Add("RULEID");
                    DM_RULE.Columns.Add("SEQNO");
                    DM_RULE.Columns.Add("MODULEID");
                    DM_RULE.Columns.Add("VISITNUM");
                    DM_RULE.Columns.Add("FIELDID");
                    DM_RULE.Columns.Add("TABLENAME");
                    DM_RULE.Columns.Add("VARIABLENAME");
                    DM_RULE.Columns.Add("DOMAIN");
                    DM_RULE.Columns.Add("Nature");
                    DM_RULE.Columns.Add("GEN_QUERY");
                    DM_RULE.Columns.Add("SET_VALUE");
                    DM_RULE.Columns.Add("Description");
                    DM_RULE.Columns.Add("QueryText");
                    DM_RULE.Columns.Add("Condition");
                    DM_RULE.Columns.Add("FORMULA_VALUE");
                    DM_RULE.Columns.Add("ENTEREDBY");
                    DM_RULE.Columns.Add("ENTEREDBYNAME");
                    DM_RULE.Columns.Add("ENTEREDDAT");
                    DM_RULE.Columns.Add("ENTERED_TZVAL");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public static bool checkboolstring(string s)
        {
            return (s == null || s == String.Empty || s == "0" || s == "No" || s == "NO") ? false : true;
        }

        protected void blukRulepublish()
        {
            DAL dal = new DAL();
            SqlConnection con = new SqlConnection(dal.getconstr());

            var options = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.KeepIdentity;

            using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), options))
            {
                if (con.State != ConnectionState.Open) { con.Open(); }

                sqlbc.DestinationTableName = "DM_RULE";

                sqlbc.ColumnMappings.Add("RULEID", "RULEID");
                sqlbc.ColumnMappings.Add("SEQNO", "SEQNO");
                sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                sqlbc.ColumnMappings.Add("VISITNUM", "VISITNUM");
                sqlbc.ColumnMappings.Add("FIELDID", "FIELDID");
                sqlbc.ColumnMappings.Add("TABLENAME", "TABLENAME");
                sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                sqlbc.ColumnMappings.Add("DOMAIN", "DOMAIN");
                sqlbc.ColumnMappings.Add("Nature", "Nature");
                sqlbc.ColumnMappings.Add("GEN_QUERY", "GEN_QUERY");
                sqlbc.ColumnMappings.Add("SET_VALUE", "SET_VALUE");
                sqlbc.ColumnMappings.Add("Description", "Description");
                sqlbc.ColumnMappings.Add("QueryText", "QueryText");
                sqlbc.ColumnMappings.Add("Condition", "Condition");
                sqlbc.ColumnMappings.Add("FORMULA_VALUE", "FORMULA_VALUE");
                sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");

                sqlbc.WriteToServer(DM_RULE);
                DM_RULE.Clear();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl.ToString());
        }

        protected void lbtnExportRuleSampleFile_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "EXPORT_RULES_SAMPLE_FILE");

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                dal_DB.DB_DOWNLOAD_LOGS_SP(ACTION: "INSERT_DOWNLOAD_LOGS",
                    FIELNAME: "Rule Sample File.xls",
                    FUNCTIONNAME: "Rule Sample File",
                    PAGENAME: Session["menu"].ToString()
                    );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Rule Sample File_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel_Restricted(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnExportRuleVariableSample_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "EXPORT_RULE_VAR_SAMPLE_FILE");

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Rule Variable Sample File_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel_Restricted(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnMappedRulesColumns_Click(object sender, EventArgs e)
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
                        //if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                        //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                        //else
                        //    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                            ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                        OleDbConnection conn = new OleDbConnection(strConn);
                        conn.Open();
                        DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        //Looping Total Sheet of Xl File



                        if (schemaTable != null && schemaTable.Rows.Count > 0)
                        {
                            string targetSheet = null;

                            // Loop through all sheets and select the first visible one
                            foreach (DataRow row in schemaTable.Rows)
                            {
                                string sheet = row["TABLE_NAME"].ToString();

                                // Check for the "Rules" sheet (ensure correct format with $ or brackets)
                                if (sheet.Contains("Rules") || sheet.Contains("Rules$"))
                                {
                                    targetSheet = sheet;
                                    break;  // Select the "Rules" sheet
                                }
                            }

                            if (string.IsNullOrEmpty(targetSheet))
                            {
                                string script = "alert('The Rules sheet was not found.'); window.location.href = '" + Request.RawUrl.ToString() + "';";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", script, true);
                                return;
                            }
                            // Query only the first visible sheet
                            string query = $"SELECT * FROM [{targetSheet}]";

                            OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                            dtexcel.Locale = CultureInfo.CurrentCulture;
                            daexcel.Fill(dtexcel);
                            excelData = dtexcel;

                            // ✅ Check if the sheet contains data
                            if (excelData.Rows.Count == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' No Data Found!'); window.location.href = '" + Request.RawUrl.ToString() + "';", true);
                                return;
                            }
                        }

                        //Looping a first Sheet of Xl File
                        //    DataRow schemaRow = schemaTable.Rows[0];
                        //string sheet = schemaRow["TABLE_NAME"].ToString();
                        //if (!sheet.EndsWith("_"))
                        //{
                        //    string query = "SELECT  * FROM [" + sheet + "]";
                        //    OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                        //    dtexcel.Locale = CultureInfo.CurrentCulture;
                        //    daexcel.Fill(dtexcel);
                        //    excelData = dtexcel;
                        //}
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

                    BIND_DRP_COLS(drpRuleVisit, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleDomain, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleVariable, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleRuleid, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleSeqno, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleNature, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleAction, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleDescripation, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleQuerytext, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleFormulaSetValue, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleCondition, dtExcelSheet);

                    ViewState["RuleRexcelData"] = excelData;
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

        protected void btnMappedRuleVariables_Click(object sender, EventArgs e)
        {
            try
            {
                GET_RULE_VAR_DRP_COLS(FileUploadRuleVariable);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_RULE_VAR_DRP_COLS(FileUpload fileUpload)
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
                        //if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                        //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                        //else
                        //    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                          ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";

                        OleDbConnection conn = new OleDbConnection(strConn);
                        conn.Open();
                        DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        //Looping Total Sheet of Xl File



                        if (schemaTable != null && schemaTable.Rows.Count > 0)
                        {
                            string targetSheet = null;

                            // Loop through all sheets and select the first visible one
                            foreach (DataRow row in schemaTable.Rows)
                            {
                                string sheet = row["TABLE_NAME"].ToString();

                                // Check for the "Rules" sheet (ensure correct format with $ or brackets)
                                if (sheet.Contains("Rule Variables") || sheet.Contains("Rule Variables$"))
                                {
                                    targetSheet = sheet;
                                    break;  // Select the "Rules" sheet
                                }
                            }

                            if (!string.IsNullOrEmpty(targetSheet))
                            {
                                // Query only the first visible sheet
                                string query = $"SELECT * FROM [{targetSheet}]";

                                OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                                dtexcel.Locale = CultureInfo.CurrentCulture;
                                daexcel.Fill(dtexcel);
                                excelData = dtexcel;

                                // ✅ Check if the sheet contains data
                                if (excelData.Rows.Count == 0)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' No Data Found.'); window.location.href = '" + Request.RawUrl.ToString() + "';", true);
                                    return;
                                }
                            }
                            else
                            {
                                Console.WriteLine("The 'Rules' sheet was not found.");
                            }

                        }

                        ////Looping a first Sheet of Xl File
                        //DataRow schemaRow = schemaTable.Rows[0];
                        //string sheet = schemaRow["TABLE_NAME"].ToString();
                        //if (!sheet.EndsWith("_"))
                        //{
                        //    string query = "SELECT  * FROM [" + sheet + "]";
                        //    OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                        //    dtexcel.Locale = CultureInfo.CurrentCulture;
                        //    daexcel.Fill(dtexcel);
                        //    excelData = dtexcel;
                        //}
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

                    BIND_DRP_COLS(drpRuleVarRuleId, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleVarColumnName, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleVarSeqNo, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleVarIsDerived, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleVarIsderivedformula, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleVarVisit, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleVarDomain, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleVarFieldVariable, dtExcelSheet);
                    BIND_DRP_COLS(drpRuleVarCondition, dtExcelSheet);

                    ViewState["RuleVariableRexcelData"] = excelData;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnuploadruleVariable_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["Downloaded"] == null)
                {
                    UploadRulesVariables();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void UploadRulesVariables()
        {
            DataTable excelData = new DataTable();
            try
            {
                excelData = (DataTable)ViewState["RuleVariableRexcelData"];

                if (excelData == null)
                {
                    Response.Write("<script> alert('Please Select File For Upload.');</script>");
                }
                else
                {
                    string ExistsRuleVar = "", RuleIdNotAvailable = "";
                    int ROWSCOUNT = 0;
                    DataSet dsRules = dal_DB.DB_UPLOAD_SP(ACTION: "GET_RULES_FOR_UPLOAD");
                    DataSet dsRulesVar = dal_DB.DB_UPLOAD_SP(ACTION: "GET_RULES_VAR_FOR_UPLOAD");
                    DataSet dsModuleFields = dal_DB.DB_UPLOAD_SP(ACTION: "GET_MODULE_FIELDS");

                    for (int i = 0; i < excelData.Rows.Count; i++)
                    {
                        DataRow[] drRule = dsRules.Tables[0].Select("[RULEID] = '" + excelData.Rows[i][drpRuleVarRuleId.SelectedValue].ToString().Trim() + "'");


                        if (drRule.Length > 0)
                        {
                            DataRow[] dr1 = dsModuleFields.Tables[0].Select("DOMAIN = '" + excelData.Rows[i][drpRuleVarDomain.SelectedValue].ToString().Trim() + "'  AND VARIABLENAME = '" + excelData.Rows[i][drpRuleVarFieldVariable.SelectedValue].ToString().Trim() + "'");

                            DataRow[] dr2 = dsModuleFields.Tables[0].Select("VISIT = '" + excelData.Rows[i][drpRuleVarVisit.SelectedValue].ToString().Trim() + "' ");




                            if (dr1.Length > 0 && dr2.Length > 0 && drRule.Length > 0)
                            {

                                // ✅ Check for column existence before accessing them
                                if (dr1[0].Table.Columns.Contains("MODULEID") &&
                                    dr1[0].Table.Columns.Contains("FIELD_ID") &&
                                    dr2[0].Table.Columns.Contains("VISITNUM") &&
                                    drRule[0].Table.Columns.Contains("ID"))
                                {
                                    // ✅ Trimmed values to prevent whitespace issues
                                    string visitNum = dr2[0]["VISITNUM"].ToString().Trim();
                                    string moduleId = dr1[0]["MODULEID"].ToString().Trim();
                                    string fieldId = dr1[0]["FIELD_ID"].ToString().Trim();
                                    string ruleId = drRule[0]["ID"].ToString().Trim();

                                    // Select RuleVar with safe access
                                    DataRow[] drRuleVar = dsRulesVar.Tables[0].Select(
                                        $"VISITNUM = '{visitNum}' AND MODULEID = '{moduleId}' AND FIELDID = '{fieldId}' AND RULEID = '{ruleId}'"
                                    );

                                    //DataRow[] drRuleVar = dsRulesVar.Tables[0].Select("VISITNUM = '" + dr2[0]["VISITNUM"].ToString() + "'  AND MODULEID = '" + dr1[0]["MODULEID"].ToString() + "' AND FIELDID = '" + dr1[0]["FIELD_ID"].ToString() + "' AND RULEID = '" + drRule[0]["ID"].ToString() + "' ");




                                    if (drRuleVar.Length > 0)
                                    {
                                        excelData.Rows[i]["Upload result"] = "Variable Name is already present in the database.";
                                    }
                                    else
                                    {
                                        ROWSCOUNT += 1;

                                        ADD_NEW_ROW_RULE_VAR_DATA(
                                            drRule[0]["ID"].ToString(),
                                            excelData.Rows[i][drpRuleVarSeqNo.SelectedValue].ToString().Trim(),
                                            excelData.Rows[i][drpRuleVarColumnName.SelectedValue].ToString().Trim(),
                                            dr1[0]["MODULEID"].ToString(),
                                            dr2[0]["VISITNUM"].ToString(),
                                            dr1[0]["FIELD_ID"].ToString(),
                                            dr1[0]["TABLENAME"].ToString(),
                                            excelData.Rows[i][drpRuleVarFieldVariable.SelectedValue].ToString().Trim(),
                                            excelData.Rows[i][drpRuleVarIsDerived.SelectedValue].ToString().Trim(),
                                            excelData.Rows[i][drpRuleVarIsderivedformula.SelectedValue].ToString().Trim(),
                                            excelData.Rows[i][drpRuleVarCondition.SelectedValue].ToString().Trim()
                                            );

                                        excelData.Rows[i]["Upload result"] = "Rule Variable Uploaded.";
                                    }


                                }

                            }



                        }
                        else
                        {
                            excelData.Rows[i]["Upload result"] = "Rule Id is not available in the database. \\n\\nPlease check your data and upload again with a unique Rule Id.";
                        }
                    }

                    if (DM_RULE_VAR.Rows.Count > 0)
                    {
                        blukRuleVarpublish();
                    }

                    SAVE_UPLOAD_FILE(excelData, lblHeaderVariable.InnerText);

                    string MSG = "Total " + ROWSCOUNT + " records uploaded successfully";

                    Session["excelData"] = excelData;
                    //Multiple_Export_Excel.ToExcel(excelData, excelData.TableName.ToString() + ".xls", Page.Response);

                    string outputFileName = excelData.TableName.ToString();

                    string script = $@"window.location.href = 'DownloadExcel.aspx?file={outputFileName}';setTimeout(function() {{location.href = 'DM_UPLOAD_MODULE_FIELDS.aspx';}}, 1000);";

                    ScriptManager.RegisterStartupScript(this, GetType(), "downloadAndRefresh", script, true);

                    Session["Downloaded"] = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ADD_NEW_ROW_RULE_VAR_DATA(string RULEID, string SEQNO, string COLUMN_NAME, string MODULEID, string VISITNUM, string FIELDID, string TABLENAME, string VARIABLENAME, string Derived, string Formula, string Condition)
        {
            try
            {
                CREATE_DM_RULE_VAR_DT();

                DataRow myDataRow;
                myDataRow = DM_RULE_VAR.NewRow();
                myDataRow["RULEID"] = RULEID;
                myDataRow["SEQNO"] = SEQNO;
                myDataRow["VARIABLENAME_DEF"] = COLUMN_NAME;
                myDataRow["MODULEID"] = MODULEID;
                myDataRow["VISITNUM"] = VISITNUM;
                myDataRow["FIELDID"] = FIELDID;
                myDataRow["TABLENAME"] = TABLENAME;
                myDataRow["VARIABLENAME"] = VARIABLENAME;
                myDataRow["Derived"] = Convert.ToBoolean(checkboolstring(Derived));
                myDataRow["Formula"] = Formula;
                myDataRow["Condition"] = Condition;
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                DM_RULE_VAR.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CREATE_DM_RULE_VAR_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (DM_RULE_VAR.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    DM_RULE_VAR.Columns.Add("RULEID");
                    DM_RULE_VAR.Columns.Add("SEQNO");
                    DM_RULE_VAR.Columns.Add("VARIABLENAME_DEF");
                    DM_RULE_VAR.Columns.Add("MODULEID");
                    DM_RULE_VAR.Columns.Add("VISITNUM");
                    DM_RULE_VAR.Columns.Add("FIELDID");
                    DM_RULE_VAR.Columns.Add("TABLENAME");
                    DM_RULE_VAR.Columns.Add("VARIABLENAME");
                    DM_RULE_VAR.Columns.Add("Derived");
                    DM_RULE_VAR.Columns.Add("Formula");
                    DM_RULE_VAR.Columns.Add("Condition");
                    DM_RULE_VAR.Columns.Add("ENTEREDBY");
                    DM_RULE_VAR.Columns.Add("ENTEREDBYNAME");
                    DM_RULE_VAR.Columns.Add("ENTEREDDAT");
                    DM_RULE_VAR.Columns.Add("ENTERED_TZVAL");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void blukRuleVarpublish()
        {
            DAL dal = new DAL();
            SqlConnection con = new SqlConnection(dal.getconstr());

            var options = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.KeepIdentity;

            using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), options))
            {
                if (con.State != ConnectionState.Open) { con.Open(); }

                sqlbc.DestinationTableName = "DM_Rule_Variables";

                sqlbc.ColumnMappings.Add("RULEID", "RULEID");
                sqlbc.ColumnMappings.Add("SEQNO", "SEQNO");
                sqlbc.ColumnMappings.Add("VARIABLENAME_DEF", "VARIABLENAME_DEF");
                sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                sqlbc.ColumnMappings.Add("VISITNUM", "VISITNUM");
                sqlbc.ColumnMappings.Add("FIELDID", "FIELDID");
                sqlbc.ColumnMappings.Add("TABLENAME", "TABLENAME");
                sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                sqlbc.ColumnMappings.Add("Derived", "Derived");
                sqlbc.ColumnMappings.Add("Formula", "Formula");
                sqlbc.ColumnMappings.Add("Condition", "Condition");
                sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");

                sqlbc.WriteToServer(DM_RULE_VAR);
                DM_RULE_VAR.Clear();
            }
        }

        protected void btncancelRule_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl.ToString());
        }
    }
}