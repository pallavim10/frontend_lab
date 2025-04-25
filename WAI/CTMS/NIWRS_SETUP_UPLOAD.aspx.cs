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

namespace CTMS
{
    public partial class NIWRS_SETUP_UPLOAD : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    fileScreen.Attributes["onchange"] = "UploadFile(this)";
                    fileRandom.Attributes["onchange"] = "UploadFile(this)";
                    fileKit.Attributes["onchange"] = "UploadFile(this)";

                    
                    VIEW__SCREENING_IDS();
                    VIEW_RAND_LIST();
                    VIEW_KITS_LIST();
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

            dtResult.Columns.Add("Upload result");

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
                        if (excelData.Columns[i].ColumnName != "Upload result")
                        {
                            dtExcelSheet.Rows.Add(excelData.Columns[i]);
                        }
                    }


                    if (Section == "Screen")
                    {
                        BIND_DRP_COLS(drpScrCountryID, dtExcelSheet);
                        BIND_DRP_COLS(drpScrSiteID, dtExcelSheet);
                        BIND_DRP_COLS(drpScrSubSiteID, dtExcelSheet);
                        BIND_DRP_COLS(drpScrID, dtExcelSheet);
                        BIND_DRP_COLS(ddlScrIDENT, dtExcelSheet);

                        ViewState["SCRexcelData"] = excelData;
                    }
                    else if (Section == "Random")
                    {
                        BIND_DRP_COLS(drpRandSEQNO, dtExcelSheet);
                        BIND_DRP_COLS(drpRandBlock, dtExcelSheet);
                        BIND_DRP_COLS(drpRandTrtGrp, dtExcelSheet);
                        BIND_DRP_COLS(drpRandTrtGrpNm, dtExcelSheet);
                        BIND_DRP_COLS(drpRandStrata, dtExcelSheet);
                        BIND_DRP_COLS(drpRandStrataL, dtExcelSheet);
                        BIND_DRP_COLS(drpRandNo, dtExcelSheet);

                        ViewState["RANDOMexcelData"] = excelData;
                    }
                    else if (Section == "Kit")
                    {
                        BIND_DRP_COLS(drpKitSeq, dtExcelSheet);
                        BIND_DRP_COLS(drpKitNo, dtExcelSheet);
                        BIND_DRP_COLS(drpKitTrtGrp, dtExcelSheet);
                        BIND_DRP_COLS(drpKitTrtGrpNm, dtExcelSheet);
                        BIND_DRP_COLS(drpKitTrtStr, dtExcelSheet);
                        BIND_DRP_COLS(drpKitExpDt, dtExcelSheet);
                        BIND_DRP_COLS(drpKitLotNo, dtExcelSheet);
                        BIND_DRP_COLS(drpblExpDt, dtExcelSheet);

                        ViewState["KITexcelData"] = excelData;
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

        private DataTable UPDATE_COUNTRYID(DataTable dtCOUNTRY, DataTable MainDT, string COUNTRY_COLUMN)
        {
            foreach (DataRow drCountry in dtCOUNTRY.Rows)
            {
                IEnumerable<DataRow> rows = MainDT.Rows.Cast<DataRow>().Where(r => r[COUNTRY_COLUMN].ToString() == drCountry["COUNTRYNAME"].ToString());

                rows.ToList().ForEach(r => r.SetField(COUNTRY_COLUMN, drCountry["COUNTRYID"].ToString()));
            }

            return MainDT;
        }

        private void UPLOAD_SCR()
        {
            try
            {

                DataSet dtCOUNTRY = dal_IWRS.IWRS_UPLOAD_SP(ACTION: "GET_COUNTRYIDS");

                DataTable dt = UPDATE_COUNTRYID(dtCOUNTRY.Tables[0], (DataTable)ViewState["SCRexcelData"], drpScrCountryID.SelectedValue);

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName != "Upload result")
                    {
                        if (dc.ColumnName != drpScrCountryID.SelectedValue &&
                        dc.ColumnName != drpScrSiteID.SelectedValue &&
                        dc.ColumnName != drpScrSubSiteID.SelectedValue &&
                        dc.ColumnName != drpScrID.SelectedValue)
                        {
                            dal_IWRS.IWRS_UPLOAD_SP(ACTION: "CHECK_SCR", COLUMNNAME: dc.ColumnName);
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string COLUMN = "";
                    string DATA = "";
                    string WHERE = "";

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

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpScrCountryID.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpScrCountryID.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpScrCountryID.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpScrCountryID.SelectedValue] + "'";
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
                        if (dc.ColumnName != "Upload result")
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
                    }

                    string[] colArr = COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                    string[] dataArr = DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);

                    INSERTQUERY = "INSERT INTO NIWRS_SUBJECT_MASTER (" + COLUMN.Replace("@ni$h", ",").TrimStart(',') + " , ENTEREDBY, ENTEREDBYNAME, ENTEREDDAT, ENTERED_TZVAL) VALUES (" + DATA.Replace("@ni$h", ",").TrimStart(',') + " , '" + Session["USER_ID"].ToString() + "', '" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "')";

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

                    DataSet dsResults = dal_IWRS.IWRS_UPLOAD_SP(ACTION: "INSERT_SCR", WHERE: WHERE, INSERTQUERY: INSERTQUERY);

                    dt.Rows[i]["Upload result"] = dsResults.Tables[1].Rows[0][0].ToString();
                }


                Response.Write("<script> alert('Screening IDs Uploaded successfully. Please find downloaded Upload summary.');</script>");

                Multiple_Export_Excel.ToExcel(dt, "Uploaded Screening ID List.xls", Page.Response);
                Response.End();


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



        //Upload Random List Starts

        protected void btnRandCols_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(fileRandom, "Random");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPLOAD_RAND()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["RANDOMexcelData"];

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName != "Upload result")
                    {
                        if (dc.ColumnName != drpRandSEQNO.SelectedValue &&
                        dc.ColumnName != drpRandBlock.SelectedValue &&
                        dc.ColumnName != drpRandTrtGrp.SelectedValue &&
                        dc.ColumnName != drpRandTrtGrpNm.SelectedValue &&
                        dc.ColumnName != drpRandStrata.SelectedValue &&
                        dc.ColumnName != drpRandStrataL.SelectedValue &&
                        dc.ColumnName != drpRandNo.SelectedValue)
                        {
                            dal_IWRS.IWRS_UPLOAD_SP(ACTION: "CHECK_RAND", COLUMNNAME: dc.ColumnName);
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string COLUMN = "";
                    string DATA = "";
                    string WHERE = "";

                    string INSERTQUERY = "";
                    string UPDATEQUERY = "";

                    if (drpRandSEQNO.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h [SEQNO]";
                        }
                        else
                        {
                            COLUMN = "[SEQNO]";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpRandSEQNO.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpRandSEQNO.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpRandSEQNO.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpRandSEQNO.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpRandBlock.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h [BLOCK]";
                        }
                        else
                        {
                            COLUMN = "[BLOCK]";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpRandBlock.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpRandBlock.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpRandBlock.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpRandBlock.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpRandTrtGrp.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h [TREAT_GRP]";
                        }
                        else
                        {
                            COLUMN = "[TREAT_GRP]";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpRandTrtGrp.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpRandTrtGrp.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpRandTrtGrp.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpRandTrtGrp.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpRandTrtGrpNm.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h [TREAT_GRP_NAME]";
                        }
                        else
                        {
                            COLUMN = "[TREAT_GRP_NAME]";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpRandTrtGrpNm.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpRandTrtGrpNm.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpRandTrtGrpNm.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpRandTrtGrpNm.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpRandStrata.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h [STRATA]";
                        }
                        else
                        {
                            COLUMN = "[STRATA]";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpRandStrata.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpRandStrata.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpRandStrata.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpRandStrata.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpRandStrataL.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h [STRATAL]";
                        }
                        else
                        {
                            COLUMN = "[STRATAL]";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpRandStrataL.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpRandStrataL.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpRandStrataL.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpRandStrataL.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpRandNo.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h [RANDNO]";
                        }
                        else
                        {
                            COLUMN = "[RANDNO]";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpRandNo.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpRandNo.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpRandNo.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpRandNo.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }

                        WHERE = " [RANDNO] = '" + dt.Rows[i][drpRandNo.SelectedValue].ToString() + "'";
                    }

                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName != "Upload result")
                        {
                            if (dc.ColumnName != drpRandSEQNO.SelectedValue &&
                        dc.ColumnName != drpRandBlock.SelectedValue &&
                        dc.ColumnName != drpRandTrtGrp.SelectedValue &&
                        dc.ColumnName != drpRandTrtGrpNm.SelectedValue &&
                        dc.ColumnName != drpRandStrata.SelectedValue &&
                        dc.ColumnName != drpRandStrataL.SelectedValue &&
                        dc.ColumnName != drpRandNo.SelectedValue)
                            {
                                COLUMN = COLUMN + " @ni$h [" + dc.ColumnName.Replace(' ', '_') + "]";

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
                    }

                    string[] colArr = COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                    string[] dataArr = DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);

                    INSERTQUERY = "INSERT INTO NIWRS_RANDOMIZATION_POOL (" + COLUMN.Replace("@ni$h", ",").TrimStart(',') + " , ENTEREDBY, ENTEREDBYNAME, ENTEREDDAT, ENTERED_TZVAL) VALUES (" + DATA.Replace("@ni$h", ",").TrimStart(',') + " , '" + Session["USER_ID"].ToString() + "', '" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "')";

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

                    string TREAT_GRP = dt.Rows[i][drpRandTrtGrp.SelectedValue].ToString();
                    string TREAT_GRP_NAME = dt.Rows[i][drpRandTrtGrpNm.SelectedValue].ToString();

                    DataSet dsResults = dal_IWRS.IWRS_UPLOAD_SP(ACTION: "INSERT_RAND", TREAT_GRP: TREAT_GRP, TREAT_GRP_NAME: TREAT_GRP_NAME, WHERE: WHERE, INSERTQUERY: INSERTQUERY);

                    dt.Rows[i]["Upload result"] = dsResults.Tables[1].Rows[0][0].ToString();
                }

                Response.Write("<script> alert('Randomization List Uploaded successfully. Please find downloaded Upload summary.');</script>");

                Multiple_Export_Excel.ToExcel(dt, "Uploaded Randomization List.xls", Page.Response);
                Response.End();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_RAND()
        {
            try
            {
                drpRandSEQNO.Items.Clear();
                drpRandBlock.Items.Clear();
                drpRandTrtGrp.Items.Clear();
                drpRandTrtGrpNm.Items.Clear();
                drpRandStrata.Items.Clear();
                drpRandStrataL.Items.Clear();
                drpRandNo.Items.Clear();

                ViewState["RANDOMexcelData"] = null;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnRandUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UPLOAD_RAND();
                CLEAR_RAND();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnRandCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_RAND();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Upload Random List Starts


        //Upload Kit List Starts

        protected void btnKitCols_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(fileKit, "Kit");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        private void UPLOAD_KIT()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["KITexcelData"];

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName != "Upload result")
                    {
                        if (dc.ColumnName != drpKitSeq.SelectedValue &&
                        dc.ColumnName != drpKitNo.SelectedValue &&
                        dc.ColumnName != drpKitTrtGrp.SelectedValue &&
                        dc.ColumnName != drpKitTrtGrpNm.SelectedValue &&
                        dc.ColumnName != drpKitTrtStr.SelectedValue &&
                        dc.ColumnName != drpKitExpDt.SelectedValue &&
                        dc.ColumnName != drpKitLotNo.SelectedValue &&
                        dc.ColumnName != drpblExpDt.SelectedValue)
                        {
                            dal_IWRS.IWRS_UPLOAD_SP(ACTION: "CHECK_KIT", COLUMNNAME: dc.ColumnName);
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string COLUMN = "";
                    string DATA = "";
                    string WHERE = "";

                    string INSERTQUERY = "";
                    string UPDATEQUERY = "";

                    if (drpKitSeq.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h SEQNO";
                        }
                        else
                        {
                            COLUMN = "SEQNO";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpKitSeq.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpKitSeq.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpKitSeq.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpKitSeq.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpKitNo.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h KITNO";
                        }
                        else
                        {
                            COLUMN = "KITNO";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpKitNo.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpKitNo.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpKitNo.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpKitNo.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }

                        WHERE = " KITNO = '" + dt.Rows[i][drpKitNo.SelectedValue].ToString() + "'";
                    }

                    if (drpKitTrtGrp.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h TREAT_GRP";
                        }
                        else
                        {
                            COLUMN = "TREAT_GRP";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpKitTrtGrp.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpKitTrtGrp.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpKitTrtGrp.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpKitTrtGrp.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpKitTrtGrpNm.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h TREAT_GRP_NAME";
                        }
                        else
                        {
                            COLUMN = "TREAT_GRP_NAME";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpKitTrtGrpNm.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpKitTrtGrpNm.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpKitTrtGrpNm.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpKitTrtGrpNm.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpKitTrtStr.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h TREAT_STRENGTH";
                        }
                        else
                        {
                            COLUMN = "TREAT_STRENGTH";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpKitTrtStr.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpKitTrtStr.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpKitTrtStr.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpKitTrtStr.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpKitExpDt.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h EXPIRY_DATE";
                        }
                        else
                        {
                            COLUMN = "EXPIRY_DATE";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpKitExpDt.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpKitExpDt.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpKitExpDt.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpKitExpDt.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpKitLotNo.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h LOTNO";
                        }
                        else
                        {
                            COLUMN = "LOTNO";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpKitLotNo.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpKitLotNo.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpKitLotNo.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpKitLotNo.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    if (drpblExpDt.SelectedIndex != 0)
                    {
                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h BL_EXPIRY_DATE";
                        }
                        else
                        {
                            COLUMN = "BL_EXPIRY_DATE";
                        }

                        if (DATA != "")
                        {
                            if (dt.Rows[i][drpblExpDt.SelectedValue].ToString() != "")
                            {
                                DATA = DATA + " @ni$h '" + dt.Rows[i][drpblExpDt.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][drpblExpDt.SelectedValue].ToString() != "")
                            {
                                DATA = "'" + dt.Rows[i][drpblExpDt.SelectedValue] + "'";
                            }
                            else
                            {
                                DATA = "NULL";
                            }
                        }
                    }

                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName != "Upload result")
                        {
                            if (dc.ColumnName != drpKitSeq.SelectedValue &&
                            dc.ColumnName != drpKitNo.SelectedValue &&
                            dc.ColumnName != drpKitTrtGrp.SelectedValue &&
                            dc.ColumnName != drpKitTrtGrpNm.SelectedValue &&
                            dc.ColumnName != drpKitTrtStr.SelectedValue &&
                            dc.ColumnName != drpKitExpDt.SelectedValue &&
                            dc.ColumnName != drpKitLotNo.SelectedValue &&
                            dc.ColumnName != drpblExpDt.SelectedValue)
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
                    }

                    string[] colArr = COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                    string[] dataArr = DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);

                    INSERTQUERY = "INSERT INTO NIWRS_KITS_POOL (" + COLUMN.Replace("@ni$h", ",").TrimStart(',') + ", ENTEREDBY, ENTEREDBYNAME, ENTEREDDT, ENTERED_TZVAL) VALUES (" + DATA.Replace("@ni$h", ",").TrimStart(',') + " , '" + Session["USER_ID"].ToString() + "', '" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "')";

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

                    string TREAT_GRP = dt.Rows[i][drpKitTrtGrp.SelectedValue].ToString();
                    string TREAT_GRP_NAME = dt.Rows[i][drpKitTrtGrpNm.SelectedValue].ToString();

                    DataSet dsResults = dal_IWRS.IWRS_UPLOAD_SP(ACTION: "INSERT_KIT", TREAT_GRP: TREAT_GRP, TREAT_GRP_NAME: TREAT_GRP_NAME, WHERE: WHERE, INSERTQUERY: INSERTQUERY);

                    dt.Rows[i]["Upload result"] = dsResults.Tables[1].Rows[0][0].ToString();
                }

                Response.Write("<script> alert('Kit List Uploaded successfully. Please find downloaded Upload summary.');</script>");

                Multiple_Export_Excel.ToExcel(dt, "Uploaded Kit List.xls", Page.Response);
                Response.End();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_KIT()
        {
            try
            {
                drpKitSeq.Items.Clear();
                drpKitNo.Items.Clear();
                drpKitTrtGrp.Items.Clear();
                drpKitTrtGrpNm.Items.Clear();
                drpKitTrtStr.Items.Clear();
                drpKitExpDt.Items.Clear();
                drpKitLotNo.Items.Clear();
                drpblExpDt.Items.Clear();

                ViewState["KITexcelData"] = null;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnKitUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UPLOAD_KIT();
                CLEAR_KIT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnKitCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_KIT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnExportUploadScreening_Click(object sender, EventArgs e)
        {
            try
            {

                DAL dal = new DAL();

                DataTable dt = dal_IWRS.NIWRS_UPLOAD_SP(
                    ACTION: "EXPORT_SCREENING_ID"
                    );

                string xlname = "SCREENING_ID.xls";

                Multiple_Export_Excel.ToExcel(dt, xlname, Page.Response);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lnkRandomizationList_Click(object sender, EventArgs e)
        {
            try
            {

                DAL dal = new DAL();

                DataTable dt = dal_IWRS.NIWRS_UPLOAD_SP(
                    ACTION: "EXPORT_RANDOMIZATION_LIST"
                    );

                string xlname = "Randomization List.xls";

                Multiple_Export_Excel.ToExcel(dt, xlname, Page.Response);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lnkKitList_Click(object sender, EventArgs e)
        {
            try
            {

                DAL dal = new DAL();

                DataTable dt = dal_IWRS.NIWRS_UPLOAD_SP(
                    ACTION: "EXPORT_KIT_LIST"
                    );

                string xlname = "Kit List.xls";

                Multiple_Export_Excel.ToExcel(dt, xlname, Page.Response);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Data_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        private void VIEW__SCREENING_IDS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UPLOAD_SP(
                    ACTION: "VIEW__SCREENING_IDS"
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grd_Data.DataSource = ds;
                    grd_Data.DataBind();
                }
                else
                {
                    grd_Data.DataSource = null;
                    grd_Data.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();

            }
        }

        private void VIEW_RAND_LIST()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UPLOAD_SP(
                    ACTION: "VIEW_RAND_LIST"
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grd_data_rand.DataSource = ds;
                    grd_data_rand.DataBind();
                }
                else
                {
                    grd_data_rand.DataSource = null;
                    grd_data_rand.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();

            }
        }

        private void VIEW_KITS_LIST()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UPLOAD_SP(
                    ACTION: "VIEW_KITS_LIST"
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grd_data_kits.DataSource = ds;
                    grd_data_kits.DataBind();
                }
                else
                {
                    grd_data_kits.DataSource = null;
                    grd_data_kits.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void lblDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool Delete = false;
                for (int i = 0; i < grd_Data.Rows.Count; i++)
                {
                    string ID = ((Label)grd_Data.Rows[i].FindControl("lblID")).Text;

                    CheckBox Chek_Delete = (CheckBox)grd_Data.Rows[i].FindControl("Chek_SCR_Delete");

                    if (Chek_Delete.Checked)
                    {
                        dal_IWRS.IWRS_UPLOAD_SP(
                            ACTION: "DELETE_SCREENING_IDS",
                            ID: ID
                            );

                        Delete = true;
                    }
                }
                if (Delete == true)
                {
                    Response.Write("<script>alert('Screening IDs deleted Successfully.')</script>");

                    VIEW__SCREENING_IDS();

                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lblRandDelete_Click(object sender, EventArgs e)
        {
            bool Delete = false;
            for (int i = 0; i < grd_data_rand.Rows.Count; i++)
            {
                string ID = ((Label)grd_data_rand.Rows[i].FindControl("lblID")).Text;

                CheckBox Chek_Delete = (CheckBox)grd_data_rand.Rows[i].FindControl("Chek_RAND_Delete");

                if (Chek_Delete.Checked)
                {
                    dal_IWRS.IWRS_UPLOAD_SP(
                        ACTION: "DELETE_RAND_IDS",
                        ID: ID
                        );

                    Delete = true;
                }
            }
            if (Delete == true)
            {
                Response.Write("<script>alert('Randomization list deleted Successfully.')</script>");

                VIEW_RAND_LIST();

            }
        }

        protected void lblKITDelete_Click(object sender, EventArgs e)
        {
            bool Delete = false;
            for (int i = 0; i < grd_data_kits.Rows.Count; i++)
            {
                string ID = ((Label)grd_data_kits.Rows[i].FindControl("lblID")).Text;

                CheckBox Chek_Delete = (CheckBox)grd_data_kits.Rows[i].FindControl("Chek_KIT_Delete");

                if (Chek_Delete.Checked)
                {
                    dal_IWRS.IWRS_UPLOAD_SP(
                        ACTION: "DELETE_KITS_LITS",
                        ID: ID
                        );

                    Delete = true;
                }
            }
            if (Delete == true)
            {
                Response.Write("<script>alert('Kit list deleted Successfully.')</script>");

                VIEW_KITS_LIST();

            }
        }

    }
}