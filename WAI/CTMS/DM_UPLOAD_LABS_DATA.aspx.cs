using ClosedXML.Excel;
using CTMS.CommonFunction;
using ExcelDataReader;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DM_UPLOAD_LABS_DATA : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        DataTable DM_LAB_DEFAULTS = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    filLabData.Attributes["onchange"] = "UploadFile(this)";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnLabData_Click(object sender, EventArgs e)
        {
            try
            {
                lblfilLabData.Text = filLabData.FileName;

                lblfilLabData.ForeColor = System.Drawing.Color.Blue;

                lblfilLabData.Font.Underline = true;

                GET_DRP_COLS(filLabData);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DRP_COLS(FileUpload fileUpload)
        {
            try
            {
                DataTable excelData = new DataTable();
                string filename = fileUpload.FileName;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable(fileUpload, filename);

                    excelData.TableName = filename;
                    excelData.Columns.Add("Upload result");

                    DataTable dtExcelSheet = new DataTable();
                    dtExcelSheet.Columns.Add("Column", typeof(String));

                    int cols = excelData.Columns.Count;
                    for (int i = 0; i < cols; i++)
                    {
                        dtExcelSheet.Rows.Add(excelData.Columns[i]);
                    }

                    BIND_DRP_COLS(drpSiteId, dtExcelSheet);
                    BIND_DRP_COLS(drpLabId, dtExcelSheet);
                    BIND_DRP_COLS(drpLabName, dtExcelSheet);
                    BIND_DRP_COLS(drpTestName, dtExcelSheet);
                    BIND_DRP_COLS(drpGender, dtExcelSheet);
                    BIND_DRP_COLS(drpAgeLowerLimit, dtExcelSheet);
                    BIND_DRP_COLS(drpAgeUpperLimit, dtExcelSheet);
                    BIND_DRP_COLS(drpRefLowerLimit, dtExcelSheet);
                    BIND_DRP_COLS(drpRefUpperLimit, dtExcelSheet);
                    BIND_DRP_COLS(drpUnit, dtExcelSheet);
                    BIND_DRP_COLS(drpFromDate, dtExcelSheet);
                    BIND_DRP_COLS(drpEndDate, dtExcelSheet);

                    ViewState["LabexcelData"] = excelData;
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

        public DataTable ConvertExcelToDataTable(FileUpload fileUpload, string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
            if (!Directory.Exists(tempPath))
            {
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            fileUpload.SaveAs(savepath + @"\" + FileName);
            string filePath = savepath + @"\" + FileName;

            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
            {
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                DataSet result = excelReader.AsDataSet();
                excelReader.Close();
                dtResult = result.Tables[0];
            }
            else if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xls")
            {
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                DataSet result = excelReader.AsDataSet();
                excelReader.Close();
                dtResult = result.Tables[0];
            }
            else
            {
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateCsvReader(stream);
                DataSet result = excelReader.AsDataSet();
                excelReader.Close();
                dtResult = result.Tables[0];
            }

            for (int c = 0; c < dtResult.Columns.Count; c++)
            {
                dtResult.Columns[c].ColumnName = dtResult.Rows[0][c].ToString();
            }

            dtResult.Rows[0].Delete();
            dtResult.AcceptChanges();

            return dtResult; //Returning Dattable  
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UPLOAD_LAB_REF_RANGE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPLOAD_LAB_REF_RANGE()
        {
            DataTable excelData = new DataTable();
            try
            {
                excelData = (DataTable)ViewState["LabexcelData"];

                if (excelData == null)
                {
                    Response.Write("<script> alert('Please Select File For Upload.');</script>");
                }
                else
                {
                    string ExistsLabRefRange = "", TestNameNotAvailable = "";
                    int ROWSCOUNT = 0;
                    DataSet dsLabTestName = dal_DB.DB_UPLOAD_SP(ACTION: "GET_TestName_UPLOAD");
                    DataSet dsLabRefRange = dal_DB.DB_UPLOAD_SP(ACTION: "GET_LAB_REF_RANGE_UPLOAD");

                    DataView view = new DataView(excelData);
                    DataTable distinctLabs = view.ToTable(true, drpSiteId.SelectedValue, drpLabId.SelectedValue, drpLabName.SelectedValue);

                    for (int i = 0; i < distinctLabs.Rows.Count; i++)
                    {
                        DataSet dsLab = dal_DB.DB_UPLOAD_SP(ACTION: "ADD_UPDATE_LAB",
                            SITEID: distinctLabs.Rows[i][drpSiteId.SelectedValue].ToString().Trim(),
                            LABID: distinctLabs.Rows[i][drpLabId.SelectedValue].ToString().Trim(),
                            LABNAME: distinctLabs.Rows[i][drpLabName.SelectedValue].ToString().Trim()
                            );

                        if (dsLab.Tables.Count > 0 && dsLab.Tables[0].Rows.Count > 0)
                        {
                            DataRow[] dr = excelData.Select("[Site Id] = '" + distinctLabs.Rows[i][drpSiteId.SelectedValue].ToString().Trim() + "' AND [Lab Id] = '" + distinctLabs.Rows[i][drpLabId.SelectedValue].ToString().Trim() + "' AND [Lab Name] = '" + distinctLabs.Rows[i][drpLabName.SelectedValue].ToString().Trim() + "' ");

                            for (int j = 0; j < dr.Length; j++)
                            {
                                DataRow[] drTestName = dsLabTestName.Tables[0].Select("[TEXT] = '" + dr[j][drpTestName.SelectedValue].ToString().Trim() + "' ");

                                if (drTestName.Length == 0)
                                {
                                    dr[j]["Upload result"] = "Test name is not available in the database.";

                                    excelData.AcceptChanges();
                                }
                                else
                                {
                                    if (dsLabRefRange.Tables.Count > 0 && dsLabRefRange.Tables[0].Rows.Count > 0)
                                    {
                                        DataRow[] dr1 = dsLabRefRange.Tables[0].Select("[SITEID] = '" + dr[j][drpSiteId.SelectedValue].ToString().Trim() + "' AND [LABID] = '" + dr[j][drpLabId.SelectedValue].ToString().Trim() + "' AND [LBTEST] = '" + dr[j][drpTestName.SelectedValue].ToString().Trim() + "' AND [SEX] = '" + dr[j][drpGender.SelectedValue].ToString().Trim() + "' AND [AGELO] = '" + dr[j][drpAgeLowerLimit.SelectedValue].ToString().Trim() + "' AND [AGEHI] = '" + dr[j][drpAgeUpperLimit.SelectedValue].ToString().Trim() + "' ");

                                        if (dr1.Length > 0)
                                        {
                                            dr[j]["Upload result"] = "The lab reference ranges with combination of Site Id, Lab Name, Testname, Gender, Age Lower Limit and Age Upper Limit are already present in the database.";

                                            excelData.AcceptChanges();
                                        }
                                        else
                                        {
                                            ROWSCOUNT += 1;

                                            ADD_NEW_ROW_DATA(
                                                dr[j][drpSiteId.SelectedValue].ToString().Trim(),
                                                dr[j][drpLabId.SelectedValue].ToString().Trim(),
                                                dr[j][drpTestName.SelectedValue].ToString().Trim(),
                                                dr[j][drpGender.SelectedValue].ToString().Trim(),
                                                dr[j][drpAgeLowerLimit.SelectedValue].ToString().Trim(),
                                                dr[j][drpAgeUpperLimit.SelectedValue].ToString().Trim(),
                                                dr[j][drpRefLowerLimit.SelectedValue].ToString().Trim(),
                                                dr[j][drpRefUpperLimit.SelectedValue].ToString().Trim(),
                                                dr[j][drpUnit.SelectedValue].ToString().Trim(),
                                                dr[j][drpFromDate.SelectedValue].ToString().Trim(),
                                                dr[j][drpEndDate.SelectedValue].ToString().Trim()
                                                );

                                            dr[j]["Upload result"] = "The data uploaded.";

                                            excelData.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        ROWSCOUNT += 1;

                                        ADD_NEW_ROW_DATA(
                                            dr[j][drpSiteId.SelectedValue].ToString().Trim(),
                                            dr[j][drpLabId.SelectedValue].ToString().Trim(),
                                            dr[j][drpTestName.SelectedValue].ToString().Trim(),
                                            dr[j][drpGender.SelectedValue].ToString().Trim(),
                                            dr[j][drpAgeLowerLimit.SelectedValue].ToString().Trim(),
                                            dr[j][drpAgeUpperLimit.SelectedValue].ToString().Trim(),
                                            dr[j][drpRefLowerLimit.SelectedValue].ToString().Trim(),
                                            dr[j][drpRefUpperLimit.SelectedValue].ToString().Trim(),
                                            dr[j][drpUnit.SelectedValue].ToString().Trim(),
                                            dr[j][drpFromDate.SelectedValue].ToString().Trim(),
                                            dr[j][drpEndDate.SelectedValue].ToString().Trim()
                                            );

                                        dr[j]["Upload result"] = "The data uploaded.";

                                        excelData.AcceptChanges();
                                    }
                                }
                            }
                        }
                    }

                    if (DM_LAB_DEFAULTS.Rows.Count > 0)
                    {
                        blukpublish();
                    }

                    SAVE_UPLOAD_FILE(excelData);

                    string MSG = "Total " + ROWSCOUNT + " lab reference ranges uploaded successfully.";

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmMsg('" + MSG + "');", true);

                    Multiple_Export_Excel.ToExcel(excelData, excelData.TableName.ToString() + ".xls", Page.Response);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SAVE_UPLOAD_FILE(DataTable excelData)
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
                   NEV_MENU_NAME: lblHeader.InnerText
                   );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ADD_NEW_ROW_DATA(string SITEID, string LABID, string LBTEST, string SEX, string AGELO, string AGEHI, string LBORNRLO, string LBORNRHI, string LBORRESU, string EFFDATEFROM, string EFFDATETO)
        {
            try
            {
                CREATE_DM_LAB_DEFAULTS_DT();

                DataRow myDataRow;
                myDataRow = DM_LAB_DEFAULTS.NewRow();
                myDataRow["SITEID"] = SITEID;
                myDataRow["LABID"] = LABID;
                myDataRow["LBTEST"] = LBTEST;
                myDataRow["SEX"] = SEX;
                myDataRow["AGELO"] = AGELO;
                myDataRow["AGEHI"] = AGEHI;
                myDataRow["LBORNRLO"] = LBORNRLO;
                myDataRow["LBORNRHI"] = LBORNRHI;
                myDataRow["LBORRESU"] = LBORRESU;
                myDataRow["EFFDATEFROM"] = Convert.ToDateTime(EFFDATEFROM).ToString("dd-MMM-yyyy");
                myDataRow["EFFDATETO"] = Convert.ToDateTime(EFFDATETO).ToString("dd-MMM-yyyy");
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                DM_LAB_DEFAULTS.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CREATE_DM_LAB_DEFAULTS_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (DM_LAB_DEFAULTS.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    DM_LAB_DEFAULTS.Columns.Add("SITEID");
                    DM_LAB_DEFAULTS.Columns.Add("LABID");
                    DM_LAB_DEFAULTS.Columns.Add("LBTEST");
                    DM_LAB_DEFAULTS.Columns.Add("SEX");
                    DM_LAB_DEFAULTS.Columns.Add("AGELO");
                    DM_LAB_DEFAULTS.Columns.Add("AGEHI");
                    DM_LAB_DEFAULTS.Columns.Add("LBORNRLO");
                    DM_LAB_DEFAULTS.Columns.Add("LBORNRHI");
                    DM_LAB_DEFAULTS.Columns.Add("LBORRESU");
                    DM_LAB_DEFAULTS.Columns.Add("EFFDATEFROM");
                    DM_LAB_DEFAULTS.Columns.Add("EFFDATETO");
                    DM_LAB_DEFAULTS.Columns.Add("ENTEREDBY");
                    DM_LAB_DEFAULTS.Columns.Add("ENTEREDBYNAME");
                    DM_LAB_DEFAULTS.Columns.Add("ENTEREDDAT");
                    DM_LAB_DEFAULTS.Columns.Add("ENTERED_TZVAL");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void blukpublish()
        {
            DAL dal = new DAL();
            SqlConnection con = new SqlConnection(dal.getconstr());

            var options = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.KeepIdentity;

            using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), options))
            {
                if (con.State != ConnectionState.Open) { con.Open(); }

                sqlbc.DestinationTableName = "DM_LAB_DEFAULTS";

                sqlbc.ColumnMappings.Add("SITEID", "SITEID");
                sqlbc.ColumnMappings.Add("LABID", "LABID");
                sqlbc.ColumnMappings.Add("LBTEST", "LBTEST");
                sqlbc.ColumnMappings.Add("SEX", "SEX");
                sqlbc.ColumnMappings.Add("AGELO", "AGELO");
                sqlbc.ColumnMappings.Add("AGEHI", "AGEHI");
                sqlbc.ColumnMappings.Add("LBORNRLO", "LBORNRLO");
                sqlbc.ColumnMappings.Add("LBORNRHI", "LBORNRHI");
                sqlbc.ColumnMappings.Add("LBORRESU", "LBORRESU");
                sqlbc.ColumnMappings.Add("EFFDATEFROM", "EFFDATEFROM");
                sqlbc.ColumnMappings.Add("EFFDATETO", "EFFDATETO");
                sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");

                sqlbc.WriteToServer(DM_LAB_DEFAULTS);
                DM_LAB_DEFAULTS.Clear();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl.ToString());
        }

        protected void lbtnExportSampleFile_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_UPLOAD_SP(ACTION: "EXPORT_Lab_Reference_Ranges_SAMPLE_FILE");

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                dal_DB.DB_DOWNLOAD_LOGS_SP(ACTION: "INSERT_DOWNLOAD_LOGS",
                    FIELNAME: "Lab Reference Ranges Sample File.xls",
                    FUNCTIONNAME: "Lab Reference Ranges",
                    PAGENAME: Session["menu"].ToString()
                    );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Lab Reference Ranges Sample File_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel_Restricted(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}