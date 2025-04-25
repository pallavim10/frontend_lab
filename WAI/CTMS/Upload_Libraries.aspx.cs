using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using ExcelDataReader;
using System.Data.SqlClient;
using CTMS.CommonFunction;
using ClosedXML.Excel;

namespace CTMS
{
    public partial class Upload_Libraries : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    filMeddra.Attributes["onchange"] = "UploadFile(this)";
                    filWHODData.Attributes["onchange"] = "UploadFile(this)";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnMeddraCol_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(filMeddra, "Meddra");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DRP_COLS(FileUpload fileUpload, string Section)
        {
            try
            {
                DataTable excelData = new DataTable();
                string filename = fileUpload.FileName;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable(fileUpload, filename);
                    excelData.TableName = filename;
                    DataTable dtExcelSheet = new DataTable();
                    dtExcelSheet.Columns.Add("Column", typeof(String));
                    int cols = excelData.Columns.Count;
                    for (int i = 0; i < cols; i++)
                    {
                        dtExcelSheet.Rows.Add(excelData.Columns[i]);
                    }

                    if (Section == "Meddra")
                    {
                        BIND_DRP_COLS(drpSystemOrganClass, dtExcelSheet);
                        BIND_DRP_COLS(drpSystemOrganClassCode, dtExcelSheet);
                        BIND_DRP_COLS(drpHighLevelGrpTerm, dtExcelSheet);
                        BIND_DRP_COLS(drpHighLevelGrpCode, dtExcelSheet);
                        BIND_DRP_COLS(drpHighlevelterm, dtExcelSheet);
                        BIND_DRP_COLS(drpHighleveltermCode, dtExcelSheet);
                        BIND_DRP_COLS(drpPererredTerm, dtExcelSheet);
                        BIND_DRP_COLS(drpPererredTermCode, dtExcelSheet);
                        BIND_DRP_COLS(drpLowestLevelTerm, dtExcelSheet);
                        BIND_DRP_COLS(drpLowestLevelTermCode, dtExcelSheet);
                        BIND_DRP_COLS(drpPrimary, dtExcelSheet);

                        ViewState["MEDDRAexcelData"] = excelData;
                    }
                    else if (Section == "WHODData")
                    {
                        BIND_DRP_COLS(drpATCLEVEL1, dtExcelSheet);
                        BIND_DRP_COLS(drpATCLEVEL1Code, dtExcelSheet);
                        BIND_DRP_COLS(drpATCLEVEL2, dtExcelSheet);
                        BIND_DRP_COLS(drpATCLEVEL2Code, dtExcelSheet);
                        BIND_DRP_COLS(drpATCLEVEL3, dtExcelSheet);
                        BIND_DRP_COLS(drpATCLEVEL3Code, dtExcelSheet);
                        BIND_DRP_COLS(drpATCLEVEL4, dtExcelSheet);
                        BIND_DRP_COLS(drpATCLEVEL4Code, dtExcelSheet);
                        BIND_DRP_COLS(drpATCLEVEL5, dtExcelSheet);
                        BIND_DRP_COLS(drpATCLEVEL5Code, dtExcelSheet);
                        BIND_DRP_COLS(drpModifyTerm, dtExcelSheet);

                        ViewState["WHODexcelData"] = excelData;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_MEDDRA()
        {
            try
            {
                drpSystemOrganClass.Items.Clear();
                drpSystemOrganClass.ClearSelection();
                drpSystemOrganClassCode.Items.Clear();
                drpSystemOrganClassCode.ClearSelection();
                drpHighLevelGrpTerm.Items.Clear();
                drpHighLevelGrpTerm.ClearSelection();
                drpHighLevelGrpCode.Items.Clear();
                drpHighLevelGrpCode.ClearSelection();
                drpHighlevelterm.Items.Clear();
                drpHighlevelterm.ClearSelection();
                drpHighleveltermCode.Items.Clear();
                drpHighleveltermCode.ClearSelection();
                drpPererredTerm.Items.Clear();
                drpPererredTerm.ClearSelection();
                drpPererredTermCode.Items.Clear();
                drpPererredTermCode.ClearSelection();
                drpLowestLevelTerm.Items.Clear();
                drpLowestLevelTerm.ClearSelection();
                drpLowestLevelTermCode.Items.Clear();
                drpLowestLevelTermCode.ClearSelection();
                drpPrimary.Items.Clear();
                drpPrimary.ClearSelection();
                txtMedraVersionNum.Text = "";
                txtMedraVersionNum.Text = string.Empty;

                ViewState["MEDDRAexcelData"] = null;
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

        private void BIND_TEXTBOX_COLS(TextBox TEXT, DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        TEXT.Text = dt.Rows[i]["MEDRAVersionNo"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnMeddra_Click(object sender, EventArgs e)
        {
            try
            {
                UPLOAD_MEDDRA();
                Response.Write("<script> alert('MedDRA Libraries Uploaded successfully.');</script>");
                CLEAR_MEDDRA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPLOAD_MEDDRA()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["MEDDRAexcelData"];

                string conStr = dal_DB.getconstr();

                SqlConnection con = new SqlConnection(conStr);

                SqlBulkCopy sqlBulk = new SqlBulkCopy(con);

                dal_DB.MEDDDRA_UPLOAD_SP(ACTION: "TRUNCATE_MEDDRA");

                sqlBulk.DestinationTableName = "MedDRAData";

                if (drpSystemOrganClass.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpSystemOrganClass.SelectedItem.Text, "soc_name");
                }

                if (drpSystemOrganClassCode.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpSystemOrganClassCode.SelectedItem.Text, "soc_code");
                }

                if (drpHighLevelGrpTerm.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpHighLevelGrpTerm.SelectedItem.Text, "hlgt_name");
                }

                if (drpHighLevelGrpCode.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpHighLevelGrpCode.SelectedItem.Text, "hlgt_code");
                }

                if (drpHighlevelterm.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpHighlevelterm.SelectedItem.Text, "hlt_name");
                }

                if (drpHighleveltermCode.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpHighleveltermCode.SelectedItem.Text, "hlt_code");
                }

                if (drpPererredTerm.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpPererredTerm.SelectedItem.Text, "pt_name");
                }

                if (drpPererredTermCode.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpPererredTermCode.SelectedItem.Text, "pt_code");
                }

                if (drpLowestLevelTerm.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpLowestLevelTerm.SelectedItem.Text, "llt_name");
                }

                if (drpLowestLevelTermCode.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpLowestLevelTermCode.SelectedItem.Text, "llt_code");
                }

                if (drpPrimary.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpPrimary.SelectedItem.Text, "primary_soc_fg");
                }

                con.Open();
                sqlBulk.WriteToServer(dt);
                con.Close();

                dal_DB.MEDDDRA_UPLOAD_SP(
                    ACTION: "UPDATE_MEDRA_VERSION_NO",
                    MEDRAVersionNo: txtMedraVersionNum.Text
                    );

                SAVE_UPLOAD_FILE(dt, "Upload MedDRA Libraries");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmMsg('MedDRA Libraries uploaded successfully');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnMeddraCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_MEDDRA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnWHODDataCol_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(filWHODData, "WHODData");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnWHODData_Click(object sender, EventArgs e)
        {
            try
            {
                UPLOAD_WHODData();
                Response.Write("<script> alert('WHODD Libraries Uploaded successfully.');</script>");
                CLEAR_WHODData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPLOAD_WHODData()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["WHODexcelData"];

                string conStr = dal_DB.getconstr();

                SqlConnection con = new SqlConnection(conStr);

                SqlBulkCopy sqlBulk = new SqlBulkCopy(con);

                dal_DB.MEDDDRA_UPLOAD_SP(ACTION: "TRUNCATE_WHODData");

                sqlBulk.DestinationTableName = "WHODData";

                if (drpATCLEVEL1.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpATCLEVEL1.SelectedItem.Text, "CMATC1C");
                }

                if (drpATCLEVEL1Code.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpATCLEVEL1Code.SelectedItem.Text, "CMATC1CD");
                }

                if (drpATCLEVEL2.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpATCLEVEL2.SelectedItem.Text, "CMATC2C");
                }

                if (drpATCLEVEL2Code.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpATCLEVEL2Code.SelectedItem.Text, "CMATC2CD");
                }

                if (drpATCLEVEL3.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpATCLEVEL3.SelectedItem.Text, "CMATC3C");
                }

                if (drpATCLEVEL3Code.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpATCLEVEL3Code.SelectedItem.Text, "CMATC3CD");
                }

                if (drpATCLEVEL4.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpATCLEVEL4.SelectedItem.Text, "CMATC4C");
                }

                if (drpATCLEVEL4Code.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpATCLEVEL4Code.SelectedItem.Text, "CMATC4CD");
                }
                if (drpATCLEVEL5.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpATCLEVEL5.SelectedItem.Text, "CMATC5C");
                }
                if (drpATCLEVEL5Code.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpATCLEVEL5Code.SelectedItem.Text, "CMATC5CD");
                }
                if (drpModifyTerm.SelectedIndex != 0)
                {
                    sqlBulk.ColumnMappings.Add(drpModifyTerm.SelectedItem.Text, "CMGEN");
                }

                con.Open();
                sqlBulk.WriteToServer(dt);
                con.Close();

                dal_DB.MEDDDRA_UPLOAD_SP(
                    ACTION: "UPDATE_WHODD_VERSION_NO",
                    WHODVersionNo: txtWHODVersionNo.Text
                    );

                SAVE_UPLOAD_FILE(dt, "Upload WHODD Libraries");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmMsg('WHODD Libraries uploaded successfully');", true);
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

        private void CLEAR_WHODData()
        {
            try
            {
                drpATCLEVEL1.Items.Clear();
                drpATCLEVEL1.ClearSelection();
                drpATCLEVEL1Code.Items.Clear();
                drpATCLEVEL1Code.ClearSelection();
                drpATCLEVEL2.Items.Clear();
                drpATCLEVEL2.ClearSelection();
                drpATCLEVEL2Code.Items.Clear();
                drpATCLEVEL2Code.ClearSelection();
                drpATCLEVEL3.Items.Clear();
                drpATCLEVEL3.ClearSelection();
                drpATCLEVEL3Code.Items.Clear();
                drpATCLEVEL3Code.ClearSelection();
                drpATCLEVEL4.Items.Clear();
                drpATCLEVEL4.ClearSelection();
                drpATCLEVEL4Code.Items.Clear();
                drpATCLEVEL4Code.ClearSelection();
                drpATCLEVEL5.Items.Clear();
                drpATCLEVEL5.ClearSelection();
                drpATCLEVEL5Code.Items.Clear();
                drpATCLEVEL5Code.ClearSelection();
                drpModifyTerm.Items.Clear();
                drpModifyTerm.ClearSelection();
                txtWHODVersionNo.Text = "";
                txtWHODVersionNo.Text = string.Empty;
                ViewState["WHODexcelData"] = null;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnWHODDataCancel_Click(object sender, EventArgs e)
        {
            CLEAR_WHODData();
        }
    }
}