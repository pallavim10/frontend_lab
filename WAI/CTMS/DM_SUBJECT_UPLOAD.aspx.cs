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
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_SUBJECT_UPLOAD : System.Web.UI.Page
    {
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        DAL_DB dal_DB = new DAL_DB();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    fileScreen.Attributes["onchange"] = "UploadFile(this)";
                }
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

        private void GET_DRP_COLS(FileUpload fileUpload, string Section)
        {
            try
            {
                DataTable excelData = new DataTable();
                string filename = fileUpload.FileName;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable(fileUpload, filename);

                    DataTable dtExcelSheet = new DataTable();
                    dtExcelSheet.Columns.Add("Column", typeof(String));
                    int cols = excelData.Columns.Count;
                    for (int i = 0; i < cols; i++)
                    {
                        dtExcelSheet.Rows.Add(excelData.Columns[i]);
                    }

                    if (Section == "Screen")
                    {
                        BIND_DRP_COLS(drpScrCountryID, dtExcelSheet);
                        BIND_DRP_COLS(drpScrSiteID, dtExcelSheet);
                        BIND_DRP_COLS(drpScrSubSiteID, dtExcelSheet);
                        BIND_DRP_COLS(drpScrID, dtExcelSheet);
                        BIND_DRP_COLS(ddlScrIDENT, dtExcelSheet);

                        excelData.TableName = filename;
                        excelData.Columns.Add("Upload result");

                        ViewState["SCRexcelData"] = excelData;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Upload Screening IDs Starts

        protected void btnScrCols_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(fileScreen, "Screen");
                drpScrSiteID.Focus();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPLOAD_SCR()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["SCRexcelData"];

                //foreach (DataColumn dc in dt.Columns)
                //{
                //    if (dc.ColumnName != drpScrCountryID.SelectedValue &&
                //        dc.ColumnName != drpScrSiteID.SelectedValue &&
                //        dc.ColumnName != drpScrSubSiteID.SelectedValue &&
                //        dc.ColumnName != drpScrID.SelectedValue)
                //    {
                //        dal_IWRS.IWRS_UPLOAD_SP(ACTION: "CHECK_SCR", COLUMNNAME: dc.ColumnName);
                //    }
                //}

                DataSet dscountry = dal_DB.DB_UPLOAD_SP(ACTION: "GET_COUNTRY_IDS");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string COLUMN = "";
                    string DATA = "";
                    string WHERE = "";
                    string STATUS = "";
                    string INSERTQUERY = "";
                    string UPDATEQUERY = "";

                    if (drpScrCountryID.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h COUNTRYID";
                        }
                        else
                        {
                            COLUMN = "COUNTRYID";
                        }

                        string COUNTRYID = "";
                        if (dt.Rows[i][drpScrCountryID.SelectedValue].ToString() != "")
                        {
                            if (dscountry.Tables.Count > 0 && dscountry.Tables[0].Rows.Count > 0)
                            {
                                DataRow[] dr = dscountry.Tables[0].Select("[COUNTRY] = '" + dt.Rows[i][drpScrCountryID.SelectedValue].ToString().Trim() + "'");

                                if (dr.Length > 0)
                                {
                                    COUNTRYID = dr[0]["CNTRYID"].ToString();
                                }
                                else
                                {
                                    COUNTRYID = "111";
                                }
                            }
                            else
                            {
                                COUNTRYID = "111";
                            }
                        }

                        if (DATA != "")
                        {
                            if (COUNTRYID != "")
                            {
                                DATA = DATA + " @ni$h '" + COUNTRYID + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (COUNTRYID != "")
                            {
                                DATA = "'" + COUNTRYID + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpScrSiteID.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h SITEID";
                        }
                        else
                        {
                            COLUMN = "SITEID";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpScrSiteID.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpScrSiteID.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpScrSiteID.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpScrSiteID.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpScrSubSiteID.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h SUBSITEID";
                        }
                        else
                        {
                            COLUMN = "SUBSITEID";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpScrSubSiteID.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpScrSubSiteID.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpScrSubSiteID.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpScrSubSiteID.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (ddlScrIDENT.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h IDENT";
                        }
                        else
                        {
                            COLUMN = "IDENT";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][ddlScrIDENT.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][ddlScrIDENT.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][ddlScrIDENT.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][ddlScrIDENT.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpScrID.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h SUBJID";
                        }
                        else
                        {
                            COLUMN = "SUBJID";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpScrID.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpScrID.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpScrID.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpScrID.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }

                        WHERE = " SUBJID = CAST('" + dt.Rows[i][drpScrID.SelectedValue].ToString() + "' AS nvarchar(20))";
                    }

                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName != drpScrCountryID.SelectedValue &&
                            dc.ColumnName != drpScrSiteID.SelectedValue &&
                        dc.ColumnName != drpScrSubSiteID.SelectedValue &&
                        dc.ColumnName != drpScrID.SelectedValue)
                        {
                            COLUMN = COLUMN + " @ni$h " + dc.ColumnName.Replace(' ', '_') + "";

                            if (dt.Rows[i][dc.ColumnName].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][dc.ColumnName] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                    }

                    string[] colArr = COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                    string[] dataArr = DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                    if (ddlScrSystem.SelectedIndex != 0)
                    {
                        if (ddlScrSystem.SelectedValue == "DDC")
                        {
                            STATUS = "0";
                        }
                        else if (ddlScrSystem.SelectedValue == "DDM")
                        {
                            STATUS = "10";
                        }
                        else
                        {
                            STATUS = ddlScrSystem.SelectedValue.Replace("0", "");
                        }
                    }
                    INSERTQUERY = "INSERT INTO NIWRS_SUBJECT_MASTER ([STATUS], " + COLUMN.Replace("@ni$h", ",").TrimStart(',') + ") VALUES ("+ STATUS + ", " + DATA.Replace("@ni$h", ",").TrimStart(',') + " )";
                    //INSERTQUERY = "INSERT INTO NIWRS_SUBJECT_MASTER ([STATUS], " + COLUMN.Replace("@ni$h", ",").TrimStart(',') + ") VALUES (10, " + DATA.Replace("@ni$h", ",").TrimStart(',') + " )";
                    for (int j = 0; j < colArr.Length; j++)
                    {
                        if (colArr[j] != "" && dataArr[j] != "")
                        {
                            if (UPDATEQUERY == "")
                            {
                                UPDATEQUERY = colArr[j] + " = " + dataArr[j] + " ";
                            }
                            else
                            {
                                UPDATEQUERY += " AND " + colArr[j] + " = " + dataArr[j] + " ";
                            }
                        }
                    }

                    DataSet ds = dal_IWRS.IWRS_UPLOAD_SP(ACTION: "INSERT_SCR", WHERE: WHERE, INSERTQUERY: INSERTQUERY);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["ID"].ToString() == "0")
                        {
                            dt.Rows[i]["Upload result"] = "Not Uploaded, as already available";
                        }
                        else
                        {
                            dt.Rows[i]["Upload result"] = "Uploaded";
                        }
                    }
                }

                Multiple_Export_Excel.ToExcel(dt, dt.TableName.ToString() + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_SCR()
        {
            try
            {
                drpScrCountryID.Items.Clear();
                drpScrSiteID.Items.Clear();
                drpScrSubSiteID.Items.Clear();
                drpScrID.Items.Clear();
                ddlScrIDENT.Items.Clear();
                ViewState["SCRexcelData"] = null;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnScrUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UPLOAD_SCR();
                Response.Write("<script> alert('Screening IDs Uploaded successfully.');</script>");
                CLEAR_SCR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnScrCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_SCR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Upload Screening IDs Ends

        protected void lbtnExportUploadScreening_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dt = dal_DB.DB_UPLOAD_SP(
                    ACTION: "EXPORT_SCREENING_ID"
                    );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Exsting Screening IDs_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel(dt, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnExportSampleFile_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_UPLOAD_SP(
                    ACTION: "EXPORT_SCREENING_ID_SAMPLE_FILE"
                    );

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Screening IDs Sample File_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel_Restricted(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}