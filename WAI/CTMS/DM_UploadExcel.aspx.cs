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

namespace CTMS
{
    public partial class DM_UploadExcel : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                fileModuleField.Attributes["onchange"] = "UploadFile(this)";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUploadModuleField_Click(object sender, EventArgs e)
        {
            try
            {
                UploadModuleField();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnancelModuleField_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DM_UploadExcel.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void UploadModuleField()
        {
            DataTable excelData = new DataTable();
            try
            {
                excelData = (DataTable)ViewState["SCRexcelData"];
                if (excelData == null)
                {
                    Response.Write("<script> alert('Please Select File For Upload.');</script>");
                }
                else
                {
                    foreach (DataRow dr in excelData.Rows)
                    {
                        dal.DM_UPLOAD_SP(
                        Action: "UPLOAD_MODULE_FIELD",
                        PROJECTID: Session["PROJECTID"].ToString(),
                        USERID: Session["User_ID"].ToString(),
                        Cur_SEQNO: dr[drpSEQNO.SelectedValue].ToString(),
                        Cur_MODULENAME: dr[drpModulename.SelectedValue].ToString(),
                        Cur_VARIABLENAME: dr[drpVarialbleName.SelectedValue].ToString(),
                        Cur_FIELDNAME: dr[drpFieldname.SelectedValue].ToString(),
                        Cur_CONTROLTYPE: dr[drpControlType.SelectedValue].ToString(),
                        Cur_ANSWERS: dr[drpAnswer.SelectedValue].ToString(),
                        Cur_LENGTH: dr[drpLenght.SelectedValue].ToString(),
                        Cur_Description: dr[drpDescription.SelectedValue].ToString()
                        );
                    }

                    Response.Write("<script> alert('Records Uploaded successfully.');</script>");
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //public void UPLOAD_SITE_SUBJID()
        //{
        //    DataTable excelData = new DataTable();
        //    try
        //    {
        //        string filename = fileSubject.FileName;
        //        if (filename != "")
        //        {
        //            excelData = ConvertExcelToDataTable_Subject(filename);

        //            foreach (DataRow dr in excelData.Rows)
        //            {

        //                dal.InsertUpdateSubjectRegistration(
        //                Action: "UPLOAD_INV",
        //                Project_ID: Session["PROJECTID"].ToString(),
        //                INVID: dr["INVID"].ToString(),
        //                SUBJID: dr["SUBJID"].ToString(),
        //                ENTEREDBY: Session["USER_ID"].ToString(),
        //                Comments: dr["INITIAL"].ToString(),
        //                DOB: dr["DOB"].ToString(),
        //                GENDER: dr["GENDER"].ToString(),
        //                Age_Group: dr["INDIC"].ToString()
        //                );
        //            }

        //            Response.Write("<script> alert('Records Uploaded successfully.');</script>");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        public DataTable ConvertExcelToDataTable(string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            fileModuleField.SaveAs(savepath + @"\" + FileName);
            string filePath = savepath + @"\" + FileName;
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
                /*foreach (DataRow schemaRow in schemaTable.Rows)
                {
                }*/
                //Looping a first Sheet of Xl File
                DataRow schemaRow = schemaTable.Rows[0];
                string sheet = schemaRow["TABLE_NAME"].ToString();
                if (!sheet.EndsWith("_"))
                {
                    string query = "SELECT  * FROM [" + sheet + "]";
                    OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                    dtexcel.Locale = CultureInfo.CurrentCulture;
                    daexcel.Fill(dtexcel);
                    dtResult = dtexcel;
                }
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
                                dtResult = dtexcel;
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
            return dtResult; //Returning Dattable  
        }

        public DataTable ConvertExcelToDataTable_Subject(string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            //fileSubject.SaveAs(savepath + @"\" + FileName);
            string filePath = savepath + @"\" + FileName;
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
                /*foreach (DataRow schemaRow in schemaTable.Rows)
                {
                }*/
                //Looping a first Sheet of Xl File
                DataRow schemaRow = schemaTable.Rows[0];
                string sheet = schemaRow["TABLE_NAME"].ToString();
                if (!sheet.EndsWith("_"))
                {
                    string query = "SELECT  * FROM [" + sheet + "]";
                    OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                    dtexcel.Locale = CultureInfo.CurrentCulture;
                    daexcel.Fill(dtexcel);
                    dtResult = dtexcel;
                }
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
                                dtResult = dtexcel;
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
            return dtResult; //Returning Dattable  
        }

        //protected void btnSubject_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //if (fileSubject.HasFile)
        //        //{
        //        //    //UPLOAD_SITE_SUBJID();
        //        //}
        //        //else
        //        //{
        //        //    Response.Write("<script> alert('Please select file...');</script>");
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        protected void btnSubjectData_Click(object sender, EventArgs e)
        {
            try
            {
                if (filePatData.HasFile)
                {
                    UPLOAD_SITE_SUBJID_DATA();
                }
                else
                {
                    Response.Write("<script> alert('Please select file...');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public DataTable ConvertExcelToDataTable_SubjectDATA(string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
            if (!Directory.Exists(tempPath))
            {
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            //filePatData.SaveAs(savepath + @"\" + FileName);
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

        public void UPLOAD_SITE_SUBJID_DATA()
        {
            DataTable excelData = new DataTable();

            try
            {
                for (int f = 0; f < Request.Files.Count; f++)
                {
                    HttpPostedFile uploadedFile = Request.Files[f];

                    string filename = uploadedFile.FileName;
                    if (filename != "")
                    {
                        DataSet dsCols = dal.DM_UPLOAD_SP(Action: "GET_DATA_MAPPINGS", SHEET_NAME: filename);

                        if (dsCols.Tables[0].Rows.Count > 0)
                        {
                            excelData = ConvertExcelToDataTable_SubjectDATA(filename);

                            DataColumnCollection columns = SingleRowToCol(dsCols.Tables[0]).Columns;

                            for (int i = 0; i < excelData.Rows.Count; i++)
                            {
                                string COLUMN = "";
                                string DATA = "";
                                string colDATA = "";

                                string INSERTQUERY = "";
                                string UPDATEQUERY = "";

                                string PVID = "", PROJECTID = "", INVID = "", SUBJID = "", VISITID = "", MODULEID = "",
                                    VISCOUNT = "", SUBJID_DATA = "", VISIT = "", SITEID = "";

                                PROJECTID = Session["PROJECTID"].ToString();
                                INVID = SITEID;
                                SUBJID = SUBJID_DATA;

                                DataSet ds = new DataSet();

                                VISCOUNT = "1";

                                string TABLENAME = dsCols.Tables[0].Rows[0]["TABLENAME"].ToString();

                                foreach (DataColumn dc in excelData.Columns)
                                {
                                    if (columns.Contains(dc.ColumnName))
                                    {
                                        DataRow[] results = dsCols.Tables[0].Select("SHEETCOL = '" + dc.ColumnName + "'  AND SHEETCOL NOT LIKE '%,%'  ");

                                        for (int r = 0; r < results.Length; r++)
                                        {
                                            if (results[r]["FIELD"].ToString() == "SUBJID_DATA")
                                            {
                                                SUBJID_DATA = excelData.Rows[i][dc.ColumnName].ToString();
                                                SUBJID = SUBJID_DATA;

                                                if (COLUMN != "")
                                                {
                                                    COLUMN = COLUMN + " @ni$h SUBJID_DATA";
                                                }
                                                else
                                                {
                                                    COLUMN = "SUBJID_DATA";
                                                }


                                                if (DATA != "")
                                                {
                                                    DATA = DATA + " @ni$h '" + excelData.Rows[i][dc.ColumnName] + "'";
                                                }
                                                else
                                                {
                                                    DATA = "'" + excelData.Rows[i][dc.ColumnName] + "'";
                                                }
                                            }
                                            else if (results[r]["FIELD"].ToString() == "VISIT")
                                            {
                                                VISIT = excelData.Rows[i][dc.ColumnName].ToString();

                                                if (COLUMN != "")
                                                {
                                                    COLUMN = COLUMN + " @ni$h VISITNUM";
                                                }
                                                else
                                                {
                                                    COLUMN = "VISITNUM";
                                                }

                                                ds = dal.DM_ADD_UPDATE(
                                                ACTION: "GET_IDs_VISIT_MODULE",
                                                VISIT: VISIT,
                                                SUBJECTID: SUBJID,
                                                MODULENAME: dsCols.Tables[0].Rows[0]["MODULENAME"].ToString()
                                                    );

                                                VISITID = ds.Tables[0].Rows[0]["VISITID"].ToString();

                                                if (DATA != "")
                                                {
                                                    DATA = DATA + " @ni$h '" + VISITID + "'";
                                                }
                                                else
                                                {
                                                    DATA = "'" + VISITID + "'";
                                                }
                                            }
                                            else if (results[r]["FIELD"].ToString() == "SITEID")
                                            {
                                                SITEID = excelData.Rows[i][dc.ColumnName].ToString();
                                                INVID = SITEID;
                                            }
                                            else
                                            {
                                                if (COLUMN != "")
                                                {
                                                    COLUMN = COLUMN + " @ni$h " + results[r]["FIELD"].ToString() + "";
                                                }
                                                else
                                                {
                                                    COLUMN = results[r]["FIELD"].ToString();
                                                }


                                                if (DATA != "")
                                                {
                                                    if (excelData.Rows[i][dc.ColumnName].ToString() != "")
                                                    {
                                                        DATA = DATA + " @ni$h '" + excelData.Rows[i][dc.ColumnName] + "'";
                                                    }
                                                    else
                                                    {
                                                        DATA = DATA + " @ni$h NULL";
                                                    }
                                                }
                                                else
                                                {
                                                    if (excelData.Rows[i][dc.ColumnName].ToString() != "")
                                                    {
                                                        DATA = "'" + excelData.Rows[i][dc.ColumnName] + "'";
                                                    }
                                                    else
                                                    {
                                                        DATA = "NULL";
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }

                                DataRow[] resultsVAL = dsCols.Tables[0].Select("SHEETCOL = ''  AND SHEETCOL NOT LIKE '%,%'  ");

                                for (int r = 0; r < resultsVAL.Length; r++)
                                {
                                    if (COLUMN != "")
                                    {
                                        COLUMN = COLUMN + " @ni$h " + resultsVAL[r]["FIELD"].ToString() + "";
                                    }
                                    else
                                    {
                                        COLUMN = resultsVAL[r]["FIELD"].ToString();
                                    }


                                    if (DATA != "")
                                    {
                                        if (resultsVAL[r]["VAL"].ToString() != "")
                                        {
                                            DATA = DATA + " @ni$h '" + resultsVAL[r]["VAL"].ToString() + "'";
                                        }
                                        else
                                        {
                                            DATA = DATA + " @ni$h NULL";
                                        }
                                    }
                                    else
                                    {
                                        if (resultsVAL[r]["VAL"].ToString() != "")
                                        {
                                            DATA = "'" + resultsVAL[r]["VAL"].ToString() + "'";
                                        }
                                        else
                                        {
                                            DATA = "NULL";
                                        }
                                    }
                                }

                                DataRow[] colROWS = dsCols.Tables[0].Select("SHEETCOL LIKE '%,%'  ");

                                for (int r = 0; r < colROWS.Length; r++)
                                {
                                    colDATA = "";
                                    string[] COLUMNarr = colROWS[r]["SHEETCOL"].ToString().Split(',');

                                    if (COLUMN != "")
                                    {
                                        COLUMN = COLUMN + " @ni$h " + colROWS[r]["FIELD"].ToString() + "";
                                    }
                                    else
                                    {
                                        COLUMN = colROWS[r]["FIELD"].ToString();
                                    }

                                    for (int arr = 0; arr < COLUMNarr.Length; arr++)
                                    {
                                        if (excelData.Columns.Contains(COLUMNarr[arr]))
                                        {

                                            if (COLUMNarr[arr] != "")
                                            {
                                                if (excelData.Rows[i][COLUMNarr[arr]].ToString() != "")
                                                {
                                                    colDATA += " " + excelData.Rows[i][COLUMNarr[arr]] + ", ";
                                                }
                                            }
                                        }
                                    }

                                    if (colDATA != "")
                                    {
                                        colDATA = colDATA.TrimEnd(',', ' ');
                                        DATA = DATA + " @ni$h '" + colDATA + "'";
                                    }
                                    else
                                    {
                                        DATA = DATA + " @ni$h NULL";
                                    }
                                }


                                DataRow[] colPRIMROWS = dsCols.Tables[0].Select("PRIM = 'True'  ");
                                string PRIM_COLUMN = "";
                                string PRIM_DATA = "";

                                for (int r = 0; r < colPRIMROWS.Length; r++)
                                {
                                    if (PRIM_COLUMN != "")
                                    {
                                        PRIM_COLUMN = PRIM_COLUMN + " @ni$h " + colPRIMROWS[r]["FIELD"].ToString() + "";
                                    }
                                    else
                                    {
                                        PRIM_COLUMN = colPRIMROWS[r]["FIELD"].ToString();
                                    }

                                    if (PRIM_DATA != "")
                                    {
                                        if (excelData.Rows[i][colPRIMROWS[r]["SHEETCOL"].ToString()].ToString() != "")
                                        {
                                            PRIM_DATA = PRIM_DATA + " @ni$h '" + excelData.Rows[i][colPRIMROWS[r]["SHEETCOL"].ToString()] + "'";
                                        }
                                        else
                                        {
                                            PRIM_DATA = PRIM_DATA + " @ni$h NULL";
                                        }
                                    }
                                    else
                                    {
                                        if (excelData.Rows[i][colPRIMROWS[r]["SHEETCOL"].ToString()].ToString() != "")
                                        {
                                            PRIM_DATA = "'" + excelData.Rows[i][colPRIMROWS[r]["SHEETCOL"].ToString()] + "'";
                                        }
                                        else
                                        {
                                            PRIM_DATA = "NULL";
                                        }
                                    }
                                }

                                string[] colArr = COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                                string[] dataArr = DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);

                                MODULEID = dsCols.Tables[0].Rows[0]["MODULEID"].ToString();

                                PVID = PROJECTID + "-" + INVID + "-" + SUBJID + "-" + VISITID + "-" + MODULEID + "-" + VISCOUNT;

                                string RECID = GET_RECID(dsCols.Tables[0].Rows[0]["MODULENAME"].ToString(), MODULEID, PVID, TABLENAME, SUBJID);

                                int newRECID = Convert.ToInt32(RECID);

                                INSERTQUERY = "INSERT INTO [" + TABLENAME + "] ([PVID], [RECID], [VISITCOUNT], [ENTEREDBY], [ENTEREDDAT], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + PVID + "', '" + newRECID + "', '1', '" + Session["USER_ID"].ToString() + "', GETDATE(), " + DATA.Replace("@ni$h", ",") + " )";

                                for (int j = 0; j < colArr.Length; j++)
                                {
                                    if (UPDATEQUERY == "")
                                    {
                                        UPDATEQUERY = "UPDATE [" + TABLENAME + "] SET UPDATEDDAT = GETDATE(), UPDATEDBY = '" + Session["USER_ID"].ToString() + "' ";
                                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[j] + " = " + dataArr[j] + " ";
                                    }
                                    else
                                    {
                                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[j] + " = " + dataArr[j] + " ";
                                    }
                                }

                                UPDATEQUERY = UPDATEQUERY + " WHERE PVID = '" + PVID + "' AND SUBJID_DATA = '" + SUBJID + "' ";


                                string[] PRIM_colArr = PRIM_COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                                string[] PRIM_dataArr = PRIM_DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);

                                string PRIMQUERY = "";

                                for (int j = 0; j < PRIM_colArr.Length; j++)
                                {
                                    PRIMQUERY += " AND " + PRIM_colArr[j] + " = " + PRIM_dataArr[j] + " ";
                                }

                                UPDATEQUERY += PRIMQUERY;

                                dal.DM_UPLOAD_SP(
                                 Action: "INSERT_MODULE_DATA",
                                 TABLENAME: TABLENAME,
                                 PVID: PVID,
                                 SUBJID: SUBJID,
                                 INSERTQUERY: INSERTQUERY,
                                 UPDATEQUERY: UPDATEQUERY,
                                 IMPORTQUERY: PRIMQUERY
                                 );

                                dal.GetSetPV(
                                PVID: PVID,
                                INVID: INVID,
                                SUBJID: SUBJID,
                                PAGENUM: MODULEID,
                                VISITNUM: VISITID,
                                PAGESTATUS: "1",
                                ENTEREDBY: Session["USER_ID"].ToString(),
                                VISITCOUNT: "1");

                                //dal.GetSet_DM_ProjectData(
                                //Action: "AUTOCODE_Multiple",
                                //PROJECTID: Session["PROJECTID"].ToString(),
                                //PVID: PVID,
                                //RECID: newRECID.ToString(),
                                //MODULEID: MODULEID,
                                //SUBJID: SUBJID,
                                //ENTEREDBY: Session["USER_ID"].ToString()
                                //);

                                //if (i == (excelData.Rows.Count - 1))
                                //{
                                //    dal.DM_UPLOAD_SP(
                                //    Action: "UPDATE_ALL_ANS",
                                //    MODULENAME: dsCols.Tables[0].Rows[0]["MODULENAME"].ToString()
                                //    );

                                //    dal.DM_UPLOAD_SP(
                                //    Action: "UPDATE_ALL_DATES",
                                //    MODULENAME: dsCols.Tables[0].Rows[0]["MODULENAME"].ToString()
                                //    );

                                //    Run_Rules(dsCols.Tables[0].Rows[0]["MODULENAME"].ToString(), TABLENAME);

                                //    ExportToAnotherModule(MODULEID);

                                //    dal.DM_UPLOAD_SP(Action: "UPDATE_ALL_COUNTRY");
                                //}
                            }
                            Response.Write("<script> alert('Records Uploaded successfully.');</script>");
                        }
                        else
                        {
                            Response.Write("<script> alert('No Mappings Found for this Excel Sheet');</script>");
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string GET_RECID(string MODULENAME, string MODULEID, string PVID, string TABLENAME, string SUBJID)
        {
            string RECID = "0";
            try
            {
                DataSet ds;

                DataSet dsMOD = dal.DM_ADD_UPDATE(ACTION: "GET_MODULENAME_BYID", ID: MODULEID);

                if (dsMOD.Tables[0].Rows[0]["ALLVISYN"].ToString() == "True")
                {
                    ds = dal.GetSet_DM_ProjectData(
                     Action: "MAX_REC_ID_ALL",
                     PVID: PVID,
                     MODULENAME: MODULENAME,
                     SUBJID: SUBJID,
                     PROJECTID: Session["PROJECTID"].ToString(),
                     TABLENAME: TABLENAME
                     );
                }
                else
                {
                    ds = dal.GetSet_DM_ProjectData(
                    Action: "MAX_REC_ID",
                    PVID: PVID,
                    MODULENAME: MODULENAME,
                    SUBJID: SUBJID,
                    PROJECTID: Session["PROJECTID"].ToString(),
                    TABLENAME: TABLENAME
                    );
                }

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        RECID = (Convert.ToInt32(ds.Tables[0].Rows[0]["RECID"]) + 1).ToString();
                    }
                    else
                    {
                        RECID = "0";
                    }
                }
                else
                {
                    RECID = "0";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return RECID;
        }

        private DataTable SingleRowToCol(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                if (!outputTable.Columns.Contains(inputTable.Rows[i]["SHEETCOL"].ToString()))
                {
                    outputTable.Columns.Add(inputTable.Rows[i]["SHEETCOL"].ToString());
                }
            }

            return outputTable;
        }


        private DataTable SingleRowToCol_VARIABLENAME(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                if (!outputTable.Columns.Contains(inputTable.Rows[i]["VARIABLENAME"].ToString()))
                {
                    outputTable.Columns.Add(inputTable.Rows[i]["VARIABLENAME"].ToString());
                }
            }

            return outputTable;
        }

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("SUBJID_DATA");
            outputTable.Columns.Add("FIRSTDATATIME");
            outputTable.Columns.Add("LASTDATATIME");
            outputTable.Columns.Add("SITEID");
            outputTable.Columns.Add("VISIT");
            outputTable.Columns.Add("FORMMNEMONIC");
            outputTable.Columns.Add("VARIABLENAME");
            outputTable.Columns.Add("DATA");

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                foreach (DataColumn dc in inputTable.Columns)
                {
                    if (dc.ColumnName != "SUBJID_DATA" && dc.ColumnName != "FIRSTDATATIME" && dc.ColumnName != "LASTDATATIME" && dc.ColumnName != "SITEID" && dc.ColumnName != "VISIT" && dc.ColumnName != "FORMMNEMONIC")
                    {
                        DataRow drNew = outputTable.NewRow();
                        drNew["SUBJID_DATA"] = inputTable.Rows[i]["SUBJID_DATA"];
                        drNew["FIRSTDATATIME"] = inputTable.Rows[i]["FIRSTDATATIME"];
                        drNew["LASTDATATIME"] = inputTable.Rows[i]["LASTDATATIME"];
                        drNew["SITEID"] = inputTable.Rows[i]["SITEID"];
                        drNew["VISIT"] = inputTable.Rows[i]["VISIT"];
                        drNew["FORMMNEMONIC"] = inputTable.Rows[i]["FORMMNEMONIC"];
                        drNew["VARIABLENAME"] = dc.ColumnName.ToString();
                        drNew["DATA"] = inputTable.Rows[i][dc.ColumnName];
                        outputTable.Rows.Add(drNew);
                    }
                }
            }

            return outputTable;
        }

        private void ExportToAnotherModule(string MODULEID)
        {
            try
            {
                string TargetTable = "", SourceTable = "", TargetColumns = "", SourceColumns = "",
                    InsertQuery = "", SelectQuery = "", NewMODULEID = "";

                DataSet ds = dal.DM_UPLOAD_SP(Action: "GET_IMPORTS", MODULEID: MODULEID);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        NewMODULEID = ds.Tables[0].Rows[0]["ID"].ToString();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            TargetTable = ds.Tables[0].Rows[0]["TABLENAME"].ToString();

                            if (ds.Tables.Count > 1)
                            {
                                DataTable dt = SingleRowToCol_VARIABLENAME(ds.Tables[1]);

                                if (ds.Tables.Count > 2)
                                {
                                    foreach (DataRow dr in ds.Tables[2].Rows)
                                    {
                                        TargetColumns = "";
                                        SourceColumns = "";

                                        foreach (DataColumn dc in ds.Tables[2].Columns)
                                        {
                                            if (dc.ColumnName != "ID" && dc.ColumnName != "MODULEID")
                                            {
                                                if (TargetColumns != "")
                                                {
                                                    TargetColumns = TargetColumns + " @ni$h " + dc.ColumnName.ToString() + " ";
                                                }
                                                else
                                                {
                                                    TargetColumns = dc.ColumnName.ToString();
                                                }

                                                if (SourceColumns != "")
                                                {
                                                    if (dt.Columns.Contains(dr[dc.ColumnName].ToString()))
                                                    {
                                                        SourceColumns = SourceColumns + " @ni$h " + dr[dc.ColumnName].ToString() + " ";
                                                    }
                                                    else
                                                    {
                                                        SourceColumns = SourceColumns + " @ni$h '" + dr[dc.ColumnName].ToString() + "' ";
                                                    }
                                                }
                                                else
                                                {
                                                    if (dt.Columns.Contains(dr[dc.ColumnName].ToString()))
                                                    {
                                                        SourceColumns = dr[dc.ColumnName].ToString();
                                                    }
                                                    else
                                                    {
                                                        SourceColumns = "'" + dr[dc.ColumnName].ToString() + "'";
                                                    }
                                                }
                                            }
                                            else if (dc.ColumnName == "MODULEID")
                                            {
                                                MODULEID = dr["MODULEID"].ToString();
                                            }
                                            else if (dc.ColumnName == "ID")
                                            {
                                                DataSet dsTable = dal.DM_UPLOAD_SP(Action: "GET_IMPORT_TABLENAME", MODULEID: dr["MODULEID"].ToString());
                                                SourceTable = dsTable.Tables[0].Rows[0]["TABLENAME"].ToString();
                                            }
                                        }

                                        InsertQuery = "INSERT INTO [" + TargetTable + "] (PVID, RECID, SUBJID_DATA, VISITNUM, VISITCOUNT, COUNTRYID, COUNTRYNAME, ENTEREDBY, ENTEREDDAT, UPDATEDBY, UPDATEDDAT, [DELETE], " + TargetColumns.Replace("@ni$h", ",") + " )";
                                        SelectQuery = "SELECT REPLACE(PVID, '-" + MODULEID + "-', '-" + NewMODULEID + "-'), RECID, SUBJID_DATA, VISITNUM, VISITCOUNT, COUNTRYID, COUNTRYNAME, ENTEREDBY, ENTEREDDAT, UPDATEDBY, UPDATEDDAT, [DELETE], " + SourceColumns.Replace("@ni$h", ",") + " FROM [" + SourceTable + "] ";

                                        string IMPORTQUERY = InsertQuery + " " + SelectQuery;

                                        dal.DM_UPLOAD_SP(Action: "IMPORT_DATA", IMPORTQUERY: IMPORTQUERY);

                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }






        //Firing Rules

        protected void Run_Rules(string MODULENAME, string TABLENAME)
        {
            try
            {
                DataSet ds = dal.DM_RUN_RULE(Action: "GET_RULES_UPLOADDATA", Para_ModuleName: MODULENAME, TABLE: TABLENAME);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Run_Rule(Project_ID: Session["PROJECTID"].ToString(), SUBJID: dr["SUBJID_DATA"].ToString(), Para_Indication_ID: dr["INDICATION"].ToString(), Para_Visit_ID: dr["VISITNUM"].ToString(), Para_ModuleName: MODULENAME, Para_VariableName: dr["VARIABLENAME"].ToString(), RECID: dr["RECID"].ToString(), TABLENAME: TABLENAME, PVID: dr["PVID"].ToString());
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Run_Rule(string Project_ID, string SUBJID, string Para_Indication_ID, string Para_Visit_ID, string Para_VariableName, string Para_ModuleName, string RECID, string TABLENAME, string PVID)
        {
            try
            {
                DataSet dsRulesData = dal.DM_RUN_RULE(Action: "GET_RULES_Data", Project_ID: Project_ID, SUBJID: SUBJID, Para_Indication_ID: Para_Indication_ID, Para_Visit_ID: Para_Visit_ID, Para_VariableName: Para_VariableName, Para_ModuleName: Para_ModuleName, RECID: RECID);
                if (dsRulesData.Tables.Count > 1)
                {
                    if (dsRulesData.Tables[0].Rows.Count > 0 && dsRulesData.Tables[1].Rows.Count > 0)
                    {
                        Compare_Data(dsRulesData.Tables[0], dsRulesData.Tables[1], Project_ID, SUBJID, RECID: RECID, TABLENAME: TABLENAME, PVID: PVID, VISITID: Para_Visit_ID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Compare_Data(DataTable dtRules, DataTable dtData, string Project_ID, string SUBJID, string RECID, string TABLENAME, string PVID, string VISITID)
        {
            try
            {
                foreach (DataRow drRules in dtRules.Rows)
                {
                    foreach (DataRow drData in dtData.Rows)
                    {
                        bool success = false, success1 = false, success2 = false, success3 = false, success4 = false, success5 = false, Query = false;
                        string DATA = null;

                        //if (drRules["Field_ID"].ToString() == drData["ID"].ToString())
                        //{
                        //For Codition
                        if (drRules["Condition"].ToString() == "Is Blank")
                        {
                            if (drRules["Data"].ToString() == "")
                            {
                                success = true;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Is Not Blank")
                        {
                            if (drRules["Data"].ToString() != "")
                            {
                                success = true;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Is Equals To")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                if (drRules["Data"].ToString() == drRules["Value"].ToString())
                                {
                                    success = true;
                                }
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID: RECID).Trim();
                                if (drRules["Data"].ToString() == value)
                                {
                                    success = true;
                                }
                                DATA = value;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Is Not Equals To")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                if (drRules["Data"].ToString() != drRules["Value"].ToString())
                                {
                                    success = true;
                                }
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                if (drRules["Data"].ToString() != value)
                                {
                                    success = true;
                                }
                                DATA = value;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Is Greater Than")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                if (ISDATE(drRules["Value"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                {
                                    if (drRules["Data"].ToString() != "" && drRules["Value"].ToString() != "")
                                    {
                                        if (Convert.ToDateTime(drRules["Data"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(drRules["Value"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (drRules["Data"].ToString() != "" && drRules["Value"].ToString() != "")
                                    {
                                        if (Convert.ToInt32(drRules["Data"]) > Convert.ToInt32(drRules["Value"]))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                {
                                    if (drRules["Data"].ToString() != "" && value.ToString() != "")
                                    {
                                        if (Convert.ToDateTime(drRules["Data"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (drRules["Data"].ToString() != "" && value.ToString() != "")
                                    {
                                        if (Convert.ToInt32(drRules["Data"]) > Convert.ToInt32(value))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                DATA = value;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Is Greater Than OR Equals To")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                if (ISDATE(drRules["Value"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                {
                                    if (drRules["Data"].ToString() != "" && drRules["Value"].ToString() != "")
                                    {
                                        if (Convert.ToDateTime(drRules["Data"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(drRules["Value"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (drRules["Data"].ToString() != "" && drRules["Value"].ToString() != "")
                                    {
                                        if (Convert.ToInt32(drRules["Data"]) >= Convert.ToInt32(drRules["Value"]))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                {
                                    if (drRules["Data"].ToString() != "" && value.ToString() != "")
                                    {
                                        if (Convert.ToDateTime(drRules["Data"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (drRules["Data"].ToString() != "" && value.ToString() != "")
                                    {
                                        if (Convert.ToInt32(drRules["Data"]) >= Convert.ToInt32(value))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                DATA = value;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Is Lesser Than")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                if (ISDATE(drRules["Value"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                {
                                    if (drRules["Data"].ToString() != "" && drRules["Value"].ToString() != "")
                                    {
                                        if (Convert.ToInt32(drRules["Data"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToInt32(drRules["Value"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (drRules["Data"].ToString() != "" && drRules["Value"].ToString() != "")
                                    {
                                        if (Convert.ToInt32(drRules["Data"]) < Convert.ToInt32(drRules["Value"]))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                {
                                    if (drRules["Data"] != "" && value.ToString() != "")
                                    {
                                        if (Convert.ToDateTime(drRules["Data"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (drRules["Data"] != "" && value.ToString() != "")
                                    {
                                        if (Convert.ToInt32(drRules["Data"]) < Convert.ToInt32(value))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                DATA = value;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Is Lesser Than OR Equals To")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                if (ISDATE(drRules["Value"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                {
                                    if (drRules["Data"] != "" && drRules["Value"].ToString() != "")
                                    {
                                        if (Convert.ToDateTime(drRules["Data"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(drRules["Value"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (drRules["Data"] != "" && drRules["Value"].ToString() != "")
                                    {
                                        if (Convert.ToInt32(drRules["Data"]) <= Convert.ToInt32(drRules["Value"]))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                {
                                    if (drRules["Data"] != "" && value.ToString() != "")
                                    {
                                        if (Convert.ToDateTime(drRules["Data"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (drRules["Data"] != "" && value.ToString() != "")
                                    {
                                        if (Convert.ToInt32(drRules["Data"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToInt32(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                        {
                                            success = true;
                                        }
                                    }
                                }
                                DATA = value;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Begins With")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                if (drRules["Data"].ToString().StartsWith(drRules["Value"].ToString()))
                                {
                                    success = true;
                                }
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                if (drRules["Data"].ToString().StartsWith(value.ToString()))
                                {
                                    success = true;
                                }
                                DATA = value;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Does Not Begins With")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                if (!drRules["Data"].ToString().StartsWith(drRules["Value"].ToString()))
                                {
                                    success = true;
                                }
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                if (!drRules["Data"].ToString().StartsWith(value.ToString()))
                                {
                                    success = true;
                                }
                                DATA = value;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Contains")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                if (drRules["Data"].ToString().Contains(drRules["Value"].ToString()))
                                {
                                    success = true;
                                }
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();

                                if (value.Contains(','))
                                {
                                    string[] valueArray = value.Split(',');
                                    {
                                        foreach (string item in valueArray)
                                        {
                                            if (drRules["Data"].ToString() == item.ToString() && success != true)
                                            {
                                                success = true;
                                            }
                                        }
                                    }
                                }
                                else if (drRules["Data"].ToString().Contains(value.ToString()))
                                {
                                    success = true;
                                }
                                DATA = value;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Does Not Contains")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                if (!drRules["Data"].ToString().Contains(drRules["Value"].ToString()))
                                {
                                    success = true;
                                }
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                if (value.Contains(','))
                                {
                                    string[] valueArray = value.Split(',');
                                    {
                                        foreach (string item in valueArray)
                                        {
                                            if (drRules["Data"].ToString() != item.ToString() && success != true)
                                            {
                                                success = true;
                                            }
                                        }
                                    }
                                }
                                else if (!drRules["Data"].ToString().Contains(value.ToString()))
                                {
                                    success = true;
                                }
                                DATA = value;
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Is Between")
                        {
                            if (drRules["Value"].ToString().ToUpperInvariant().Contains("AND".ToUpperInvariant()))
                            {
                                string[] split = drRules["Value"].ToString().Trim().Split(new string[] { "AND" }, StringSplitOptions.None);
                                int From = Convert.ToInt32(split[0]);
                                int To = Convert.ToInt32(split[1]);

                                if (drRules["Data"].ToString() != "" && From.ToString() != "" && To.ToString() != "")
                                {
                                    if (IsBetween(Convert.ToInt32(drRules["Data"]), From, To))
                                    {
                                        success = true;
                                    }
                                }
                            }
                            else if (drRules["Formula"].ToString().ToUpperInvariant().Contains("AND".ToUpperInvariant()))
                            {
                                string[] split = drRules["Formula"].ToString().Trim().Split(new string[] { "AND" }, StringSplitOptions.None);
                                int From = Convert.ToInt32(split[0]);
                                int To = Convert.ToInt32(split[1]);

                                if (drRules["Data"].ToString() != "" && From.ToString() != "" && To.ToString() != "")
                                {
                                    if (IsBetween(Convert.ToInt32(drRules["Data"]), From, To))
                                    {
                                        success = true;
                                    }
                                }
                            }
                            else if (drRules["Value"].ToString() != "" && drRules["Value1"].ToString() != "")
                            {
                                if (drRules["Data"].ToString() != "" && drData["Value"].ToString() != "" && drData["Value1"].ToString() != "")
                                {
                                    if (IsBetween(Convert.ToInt32(drRules["Data"]), Convert.ToInt32(drData["Value"]), Convert.ToInt32(drData["Value1"])))
                                    {
                                        success = true;
                                    }
                                }
                            }
                            else if (drRules["Value"].ToString() != "" && drRules["Formula1"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();

                                if (drRules["Data"].ToString() != "" && drData["Value"].ToString() != "" && value.ToString() != "")
                                {
                                    if (IsBetween(Convert.ToInt32(drRules["Data"]), Convert.ToInt32(drData["Value"]), Convert.ToInt32(value)))
                                    {
                                        success = true;
                                    }
                                }
                            }
                            else if (drRules["Formula"].ToString() != "" && drRules["Value1"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();

                                if (drRules["Data"].ToString() != "" && value.ToString() != "" && drRules["Value1"].ToString() != "")
                                {
                                    if (IsBetween(Convert.ToInt32(drRules["Data"]), Convert.ToInt32(value), Convert.ToInt32(drRules["Value1"])))
                                    {
                                        success = true;
                                    }
                                }
                            }
                            else if (drRules["Formula"].ToString() != "" && drRules["Formula1"].ToString() != "")
                            {
                                string From = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                string To = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();

                                if (drRules["Data"].ToString() != "" && From.ToString() != "" && To.ToString() != "")
                                {
                                    if (IsBetween(Convert.ToInt32(drRules["Data"]), Convert.ToInt32(From), Convert.ToInt32(To)))
                                    {
                                        success = true;
                                    }
                                }
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Is Not Between")
                        {
                            if (drRules["Value"].ToString().ToUpperInvariant().Contains("AND".ToUpperInvariant()))
                            {
                                string[] split = drRules["Value"].ToString().Trim().Split(new string[] { "AND" }, StringSplitOptions.None);
                                int From = Convert.ToInt32(split[0]);
                                int To = Convert.ToInt32(split[1]);

                                if (drRules["Data"].ToString() != "" && From.ToString() != "" && To.ToString() != "")
                                {
                                    if (!IsBetween(Convert.ToInt32(drRules["Data"]), From, To))
                                    {
                                        success = true;
                                    }
                                }
                            }
                            else if (drRules["Formula"].ToString().ToUpperInvariant().Contains("AND".ToUpperInvariant()))
                            {
                                string[] split = drRules["Formula"].ToString().Trim().Split(new string[] { "AND" }, StringSplitOptions.None);
                                int From = Convert.ToInt32(split[0]);
                                int To = Convert.ToInt32(split[1]);

                                if (drRules["Data"].ToString() != "" && From.ToString() != "" && To.ToString() != "")
                                {
                                    if (!IsBetween(Convert.ToInt32(drRules["Data"]), From, To))
                                    {
                                        success = true;
                                    }
                                }
                            }
                            else if (drRules["Value"].ToString() != "" && drRules["Value1"].ToString() != "")
                            {
                                if (drRules["Data"].ToString() != "" && drData["Value"].ToString() != "" && drData["Value1"].ToString() != "")
                                {
                                    if (!IsBetween(Convert.ToInt32(drRules["Data"]), Convert.ToInt32(drData["Value"]), Convert.ToInt32(drData["Value1"])))
                                    {
                                        success = true;
                                    }
                                }
                            }
                            else if (drRules["Value"].ToString() != "" && drRules["Formula1"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();

                                if (drRules["Data"].ToString() != "" && drData["Value"].ToString() != "" && value.ToString() != "")
                                {
                                    if (!IsBetween(Convert.ToInt32(drRules["Data"]), Convert.ToInt32(drData["Value"]), Convert.ToInt32(value)))
                                    {
                                        success = true;
                                    }
                                }
                            }
                            else if (drRules["Formula"].ToString() != "" && drRules["Value1"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();

                                if (drRules["Data"].ToString() != "" && value.ToString() != "" && drRules["Value1"].ToString() != "")
                                {
                                    if (!IsBetween(Convert.ToInt32(drRules["Data"]), Convert.ToInt32(value), Convert.ToInt32(drRules["Value1"])))
                                    {
                                        success = true;
                                    }
                                }
                            }
                            else if (drRules["Formula"].ToString() != "" && drRules["Formula1"].ToString() != "")
                            {
                                string From = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                string To = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();

                                if (drRules["Data"].ToString() != "" && From.ToString() != "" && To.ToString() != "")
                                {
                                    if (!IsBetween(Convert.ToInt32(drRules["Data"]), Convert.ToInt32(From), Convert.ToInt32(To)))
                                    {
                                        success = true;
                                    }
                                }
                            }
                        }
                        else if (drRules["Condition"].ToString() == "Not Applicable")
                        {
                            if (drRules["Value"].ToString() != "")
                            {
                                DATA = drRules["Value"].ToString();
                            }
                            else if (drRules["Formula"].ToString() != "")
                            {
                                string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                DATA = value;
                            }
                            success = true;
                        }

                        if (success)
                        {
                            Query = true;
                        }
                        else
                        {
                            Query = false;
                        }

                        //For Condition1
                        if (drRules["Math_Symbol1"].ToString() != "")
                        {
                            if (drRules["Condition1"].ToString() == "Is Blank")
                            {
                                if (drRules["Data1"].ToString() == "")
                                {
                                    success1 = true;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Is Not Blank")
                            {
                                if (drRules["Data1"].ToString() != "")
                                {
                                    success1 = true;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Is Equals To")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    if (drRules["Data1"].ToString() == drRules["Value1"].ToString())
                                    {
                                        success1 = true;
                                    }
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data1"].ToString() == value)
                                    {
                                        success1 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Is Not Equals To")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    if (drRules["Data1"].ToString() != drRules["Value1"].ToString())
                                    {
                                        success1 = true;
                                    }
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data1"].ToString() != value)
                                    {
                                        success1 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Is Greater Than")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data1"].ToString() != "" && drRules["Value1"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(drRules["Value1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data1"].ToString() != "" && drRules["Value1"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data1"]) > Convert.ToInt32(drRules["Value1"]))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data1"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data1"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data1"]) > Convert.ToInt32(value))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Is Greater Than OR Equals To")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data1"].ToString() != "" && drRules["Value1"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(drRules["Value1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data1"].ToString() != "" && drRules["Value1"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data1"]) >= Convert.ToInt32(drRules["Value1"]))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data1"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data1"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data1"]) >= Convert.ToInt32(value))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Is Lesser Than")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data1"].ToString() != "" && drRules["Value1"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(drRules["Value1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data1"].ToString() != "" && drRules["Value1"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data1"]) < Convert.ToInt32(drRules["Value1"]))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data1"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data1"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data1"]) < Convert.ToInt32(value))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Is Lesser Than OR Equals To")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data1"].ToString() != "" && drRules["Value1"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToInt32(drRules["Value1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data1"].ToString() != "" && drRules["Value1"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data1"]) <= Convert.ToInt32(drRules["Value1"]))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data1"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data1"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data1"]) <= Convert.ToInt32(value))
                                            {
                                                success1 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Begins With")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    if (drRules["Data1"].ToString().StartsWith(drRules["Value1"].ToString()))
                                    {
                                        success1 = true;
                                    }
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data1"].ToString().StartsWith(value.ToString()))
                                    {
                                        success1 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Does Not Begins With")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    if (!drRules["Data1"].ToString().StartsWith(drRules["Value1"].ToString()))
                                    {
                                        success1 = true;
                                    }
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (!drRules["Data1"].ToString().StartsWith(value.ToString()))
                                    {
                                        success1 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Contains")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    if (drRules["Data1"].ToString().Contains(drRules["Value1"].ToString()))
                                    {
                                        success1 = true;
                                    }
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (value.Contains(','))
                                    {
                                        string[] valueArray = value.Split(',');
                                        {
                                            foreach (string item in valueArray)
                                            {
                                                if (drRules["Data1"].ToString() == item.ToString() && success1 != true)
                                                {
                                                    success1 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (drRules["Data1"].ToString().Contains(value.ToString()))
                                    {
                                        success1 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Does Not Contains")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    if (!drRules["Data1"].ToString().Contains(drRules["Value1"].ToString()))
                                    {
                                        success1 = true;
                                    }
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (value.Contains(','))
                                    {
                                        string[] valueArray = value.Split(',');
                                        {
                                            foreach (string item in valueArray)
                                            {
                                                if (drRules["Data1"].ToString() != item.ToString() && success1 != true)
                                                {
                                                    success1 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (!drRules["Data1"].ToString().Contains(value.ToString()))
                                    {
                                        success1 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition1"].ToString() == "Not Applicable")
                            {
                                if (drRules["Value1"].ToString() != "")
                                {
                                    DATA = drRules["Value1"].ToString();
                                }
                                else if (drRules["Formula1"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data1"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    DATA = value;
                                }
                            }

                            if (success == true && drRules["Math_Symbol1"].ToString() == "AND" && success1 == false)
                            {
                                Query = false;
                            }
                            else if (success == true && drRules["Math_Symbol1"].ToString() == "OR" && success1 == false)
                            {
                                Query = true;
                            }
                        }

                        //For Condition2
                        if (drRules["Math_Symbol2"].ToString() != "")
                        {
                            if (drRules["Condition2"].ToString() == "Is Blank")
                            {
                                if (drRules["Data2"].ToString() == "")
                                {
                                    success2 = true;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Is Not Blank")
                            {
                                if (drRules["Data2"].ToString() != "")
                                {
                                    success2 = true;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Is Equals To")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    if (drRules["Data2"].ToString() == drRules["Value2"].ToString())
                                    {
                                        success2 = true;
                                    }
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data2"].ToString() == value)
                                    {
                                        success2 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Is Not Equals To")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    if (drRules["Data2"].ToString() != drRules["Value2"].ToString())
                                    {
                                        success2 = true;
                                    }
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data2"].ToString() != value)
                                    {
                                        success2 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Is Greater Than")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data2"].ToString() != "" && drRules["Value2"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(drRules["Value2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data2"].ToString() != "" && drRules["Value2"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data2"]) > Convert.ToInt32(drRules["Value2"]))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data2"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data2"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data2"]) > Convert.ToInt32(value))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Is Greater Than OR Equals To")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data2"].ToString() != "" && drRules["Value2"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(drRules["Value2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data2"].ToString() != "" && drRules["Value2"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data2"]) >= Convert.ToInt32(drRules["Value2"]))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data2"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data2"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data2"]) >= Convert.ToInt32(value))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Is Lesser Than")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data2"].ToString() != "" && drRules["Value2"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(drRules["Value2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data2"].ToString() != "" && drRules["Value2"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data2"]) < Convert.ToInt32(drRules["Value2"]))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data2"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data2"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data2"]) < Convert.ToInt32(value))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Is Lesser Than OR Equals To")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data2"].ToString() != "" && drRules["Value2"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(drRules["Value2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data2"].ToString() != "" && drRules["Value2"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data2"]) <= Convert.ToInt32(drRules["Value2"]))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value))
                                    {
                                        if (drRules["Data2"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data2"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data2"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data2"]) <= Convert.ToInt32(value))
                                            {
                                                success2 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Begins With")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    if (drRules["Data2"].ToString().StartsWith(drRules["Value2"].ToString()))
                                    {
                                        success2 = true;
                                    }
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data2"].ToString().StartsWith(value.ToString()))
                                    {
                                        success2 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Does Not Begins With")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    if (!drRules["Data2"].ToString().StartsWith(drRules["Value2"].ToString()))
                                    {
                                        success2 = true;
                                    }
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (!drRules["Data2"].ToString().StartsWith(value.ToString()))
                                    {
                                        success2 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Contains")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    if (drRules["Data2"].ToString().Contains(drRules["Value2"].ToString()))
                                    {
                                        success2 = true;
                                    }
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (value.Contains(','))
                                    {
                                        string[] valueArray = value.Split(',');
                                        {
                                            foreach (string item in valueArray)
                                            {
                                                if (drRules["Data2"].ToString() == item.ToString() && success2 != true)
                                                {
                                                    success2 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (drRules["Data2"].ToString().Contains(value.ToString()))
                                    {
                                        success2 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Does Not Contains")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    if (!drRules["Data2"].ToString().Contains(drRules["Value2"].ToString()))
                                    {
                                        success2 = true;
                                    }
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (value.Contains(','))
                                    {
                                        string[] valueArray = value.Split(',');
                                        {
                                            foreach (string item in valueArray)
                                            {
                                                if (drRules["Data2"].ToString() != item.ToString() && success2 != true)
                                                {
                                                    success2 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (!drRules["Data2"].ToString().Contains(value.ToString()))
                                    {
                                        success2 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition2"].ToString() == "Not Applicable")
                            {
                                if (drRules["Value2"].ToString() != "")
                                {
                                    DATA = drRules["Value2"].ToString();
                                }
                                else if (drRules["Formula2"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data2"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    DATA = value;
                                }
                            }

                            if (success1 == true && drRules["Math_Symbol2"].ToString() == "AND" && success2 == false)
                            {
                                Query = false;
                            }
                            else if (success1 == true && drRules["Math_Symbol2"].ToString() == "OR" && success2 == false)
                            {
                                Query = true;
                            }
                        }

                        //For Condition3
                        if (drRules["Math_Symbol3"].ToString() != "")
                        {
                            if (drRules["Condition3"].ToString() == "Is Blank")
                            {
                                if (drRules["Data3"].ToString() == "")
                                {
                                    success3 = true;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Is Not Blank")
                            {
                                if (drRules["Data3"].ToString() != "")
                                {
                                    success3 = true;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Is Equals To")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    if (drRules["Data3"].ToString() == drRules["Value3"].ToString())
                                    {
                                        success3 = true;
                                    }
                                    DATA = drRules["Value3"].ToString();
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data3"].ToString() == value)
                                    {
                                        success3 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Is Not Equals To")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    if (drRules["Data3"].ToString() != drRules["Value3"].ToString())
                                    {
                                        success3 = true;
                                    }
                                    DATA = drRules["Value3"].ToString();
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data3"].ToString() != value)
                                    {
                                        success3 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Is Greater Than")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data3"].ToString() != "" && drRules["Value3"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(drRules["Value3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data3"].ToString() != "" && drRules["Value3"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data3"]) > Convert.ToInt32(drRules["Value3"]))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value3"].ToString();
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data3"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data3"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data3"]) > Convert.ToInt32(value))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Is Greater Than OR Equals To")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data3"].ToString() != "" && drRules["Value3"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(drRules["Value3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data3"].ToString() != "" && drRules["Value3"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data3"]) >= Convert.ToInt32(drRules["Value3"]))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value3"].ToString();
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data3"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data3"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data3"]) >= Convert.ToInt32(value))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Is Lesser Than")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data3"].ToString() != "" && drRules["Value3"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(drRules["Value3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data3"].ToString() != "" && drRules["Value3"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data3"]) < Convert.ToInt32(drRules["Value3"]))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value3"].ToString();
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data3"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data3"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data3"]) < Convert.ToInt32(value))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Is Lesser Than OR Equals To")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data3"].ToString() != "" && drRules["Value3"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(drRules["Value3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data3"].ToString() != "" && drRules["Value3"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data3"]) <= Convert.ToInt32(drRules["Value3"]))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value3"].ToString();
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data3"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data3"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data3"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data3"]) <= Convert.ToInt32(value))
                                            {
                                                success3 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Begins With")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    if (drRules["Data3"].ToString().StartsWith(drRules["Value3"].ToString()))
                                    {
                                        success3 = true;
                                    }
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data3"].ToString().StartsWith(value.ToString()))
                                    {
                                        success3 = true;
                                    }
                                }
                                DATA = drRules["Value3"].ToString();
                            }
                            else if (drRules["Condition3"].ToString() == "Does Not Begins With")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    if (!drRules["Data3"].ToString().StartsWith(drRules["Value3"].ToString()))
                                    {
                                        success3 = true;
                                    }
                                    DATA = drRules["Value3"].ToString();
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (!drRules["Data3"].ToString().StartsWith(value.ToString()))
                                    {
                                        success3 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Contains")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    if (drRules["Data3"].ToString().Contains(drRules["Value3"].ToString()))
                                    {
                                        success3 = true;
                                    }
                                    DATA = drRules["Value3"].ToString();
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (value.Contains(','))
                                    {
                                        string[] valueArray = value.Split(',');
                                        {
                                            foreach (string item in valueArray)
                                            {
                                                if (drRules["Data3"].ToString() == item.ToString() && success3 != true)
                                                {
                                                    success3 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (drRules["Data3"].ToString().Contains(value.ToString()))
                                    {
                                        success3 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Does Not Contains")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    if (!drRules["Data3"].ToString().Contains(drRules["Value3"].ToString()))
                                    {
                                        success3 = true;
                                    }
                                    DATA = drRules["Value3"].ToString();
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (value.Contains(','))
                                    {
                                        string[] valueArray = value.Split(',');
                                        {
                                            foreach (string item in valueArray)
                                            {
                                                if (drRules["Data3"].ToString() != item.ToString() && success3 != true)
                                                {
                                                    success3 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (!drRules["Data3"].ToString().Contains(value.ToString()))
                                    {
                                        success3 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition3"].ToString() == "Not Applicable")
                            {
                                if (drRules["Value3"].ToString() != "")
                                {
                                    DATA = drRules["Value3"].ToString();
                                }
                                else if (drRules["Formula3"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data3"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    DATA = value;
                                }
                            }

                            if (success2 == true && drRules["Math_Symbol3"].ToString() == "AND" && success3 == false)
                            {
                                Query = false;
                            }
                            else if (success2 == true && drRules["Math_Symbol3"].ToString() == "OR" && success3 == false)
                            {
                                Query = true;
                            }
                        }

                        //For Condition4
                        if (drRules["Math_Symbol4"].ToString() != "")
                        {
                            if (drRules["Condition4"].ToString() == "Is Blank")
                            {
                                if (drRules["Data4"].ToString() == "")
                                {
                                    success4 = true;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Is Not Blank")
                            {
                                if (drRules["Data4"].ToString() != "")
                                {
                                    success4 = true;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Is Equals To")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    if (drRules["Data4"].ToString() == drRules["Value4"].ToString())
                                    {
                                        success4 = true;
                                    }
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data4"].ToString() == value)
                                    {
                                        success4 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Is Not Equals To")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    if (drRules["Data4"].ToString() != drRules["Value4"].ToString())
                                    {
                                        success4 = true;
                                    }
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data4"].ToString() != value)
                                    {
                                        success4 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Is Greater Than")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data4"].ToString() != "" && drRules["Value4"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(drRules["Value4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data4"].ToString() != "" && drRules["Value4"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data4"]) > Convert.ToInt32(drRules["Value4"]))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data4"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data4"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data4"]) > Convert.ToInt32(value))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Is Greater Than OR Equals To")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data4"].ToString() != "" && drRules["Value4"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(drRules["Value4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data4"].ToString() != "" && drRules["Value4"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data4"]) >= Convert.ToInt32(drRules["Value4"]))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data4"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data4"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data4"]) >= Convert.ToInt32(value))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Is Lesser Than")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data4"].ToString() != "" && drRules["Value4"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(drRules["Value4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data4"].ToString() != "" && drRules["Value4"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data4"]) < Convert.ToInt32(drRules["Value4"]))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data4"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data4"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data4"]) < Convert.ToInt32(value))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Is Lesser Than OR Equals To")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value4"].ToString()))
                                    {
                                        if (drRules["Data4"].ToString() != "" && drRules["Value4"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(drRules["Value4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data4"].ToString() != "" && drRules["Value4"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data4"]) <= Convert.ToInt32(drRules["Value4"]))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data4"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data4"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data4"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data4"]) <= Convert.ToInt32(value))
                                            {
                                                success4 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Begins With")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    if (drRules["Data4"].ToString().StartsWith(drRules["Value4"].ToString()))
                                    {
                                        success4 = true;
                                    }
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data4"].ToString().StartsWith(value.ToString()))
                                    {
                                        success4 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Does Not Begins With")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    if (!drRules["Data4"].ToString().StartsWith(drRules["Value4"].ToString()))
                                    {
                                        success4 = true;
                                    }
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (!drRules["Data4"].ToString().StartsWith(value.ToString()))
                                    {
                                        success4 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Contains")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    if (drRules["Data4"].ToString().Contains(drRules["Value4"].ToString()))
                                    {
                                        success4 = true;
                                    }
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (value.Contains(','))
                                    {
                                        string[] valueArray = value.Split(',');
                                        {
                                            foreach (string item in valueArray)
                                            {
                                                if (drRules["Data4"].ToString() == item.ToString() && success4 != true)
                                                {
                                                    success4 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (drRules["Data4"].ToString().Contains(value.ToString()))
                                    {
                                        success4 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Does Not Contains")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    if (!drRules["Data4"].ToString().Contains(drRules["Value4"].ToString()))
                                    {
                                        success4 = true;
                                    }
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (value.Contains(','))
                                    {
                                        string[] valueArray = value.Split(',');
                                        {
                                            foreach (string item in valueArray)
                                            {
                                                if (drRules["Data4"].ToString() != item.ToString() && success4 != true)
                                                {
                                                    success4 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (!drRules["Data4"].ToString().Contains(value.ToString()))
                                    {
                                        success4 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition4"].ToString() == "Not Applicable")
                            {
                                if (drRules["Value4"].ToString() != "")
                                {
                                    DATA = drRules["Value4"].ToString();
                                }
                                else if (drRules["Formula4"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data4"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    DATA = value;
                                }
                            }

                            if (success3 == true && drRules["Math_Symbol4"].ToString() == "AND" && success4 == false)
                            {
                                Query = false;
                            }
                            else if (success3 == true && drRules["Math_Symbol4"].ToString() == "OR" && success4 == false)
                            {
                                Query = true;
                            }
                        }

                        //For Condition5
                        if (drRules["Math_Symbol5"].ToString() != "")
                        {
                            if (drRules["Condition5"].ToString() == "Is Blank")
                            {
                                if (drRules["Data5"].ToString() == "")
                                {
                                    success5 = true;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Is Not Blank")
                            {
                                if (drRules["Data5"].ToString() != "")
                                {
                                    success5 = true;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Is Equals To")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    if (drRules["Data5"].ToString() == drRules["Value5"].ToString())
                                    {
                                        success5 = true;
                                    }
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data5"].ToString() == value)
                                    {
                                        success5 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Is Not Equals To")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    if (drRules["Data5"].ToString() != drRules["Value5"].ToString())
                                    {
                                        success5 = true;
                                    }
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data5"].ToString() != value)
                                    {
                                        success5 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Is Greater Than")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data5"].ToString() != "" && drRules["Value5"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(drRules["Value5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data5"].ToString() != "" && drRules["Value5"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data5"]) > Convert.ToInt32(drRules["Value5"]))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data5"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) > Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data5"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data5"]) > Convert.ToInt32(value))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Is Greater Than OR Equals To")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data5"].ToString() != "" && drRules["Value5"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(drRules["Value5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data5"].ToString() != "" && drRules["Value5"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data5"]) >= Convert.ToInt32(drRules["Value5"]))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data5"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) >= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data5"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data5"]) >= Convert.ToInt32(value))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Is Lesser Than")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data5"].ToString() != "" && drRules["Value5"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(drRules["Value5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data5"].ToString() != "" && drRules["Value5"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data5"]) < Convert.ToInt32(drRules["Value5"]))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data5"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) < Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data5"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data5"]) < Convert.ToInt32(value))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Is Lesser Than OR Equals To")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    if (ISDATE(drRules["Value5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data5"].ToString() != "" && drRules["Value5"].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(drRules["Value5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data5"].ToString() != "" && drRules["Value5"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data5"]) <= Convert.ToInt32(drRules["Value5"]))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (ISDATE(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                    {
                                        if (drRules["Data5"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToDateTime(drRules["Data5"].ToString().Replace("dd/", "01/").Replace("mm/", "01/")) <= Convert.ToDateTime(value.Replace("dd/", "01/").Replace("mm/", "01/")))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (drRules["Data5"].ToString() != "" && value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(drRules["Data5"]) <= Convert.ToInt32(value))
                                            {
                                                success5 = true;
                                            }
                                        }
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Begins With")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    if (drRules["Data5"].ToString().StartsWith(drRules["Value5"].ToString()))
                                    {
                                        success5 = true;
                                    }
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (drRules["Data5"].ToString().StartsWith(value.ToString()))
                                    {
                                        success5 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Does Not Begins With")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    if (!drRules["Data5"].ToString().StartsWith(drRules["Value5"].ToString()))
                                    {
                                        success5 = true;
                                    }
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (!drRules["Data5"].ToString().StartsWith(value.ToString()))
                                    {
                                        success5 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Contains")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    if (drRules["Data5"].ToString().Contains(drRules["Value5"].ToString()))
                                    {
                                        success5 = true;
                                    }
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (value.Contains(','))
                                    {
                                        string[] valueArray = value.Split(',');
                                        {
                                            foreach (string item in valueArray)
                                            {
                                                if (drRules["Data5"].ToString() == item.ToString() && success5 != true)
                                                {
                                                    success5 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (drRules["Data5"].ToString().Contains(value.ToString()))
                                    {
                                        success5 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Does Not Contains")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    if (!drRules["Data5"].ToString().Contains(drRules["Value5"].ToString()))
                                    {
                                        success5 = true;
                                    }
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    if (value.Contains(','))
                                    {
                                        string[] valueArray = value.Split(',');
                                        {
                                            foreach (string item in valueArray)
                                            {
                                                if (drRules["Data5"].ToString() != item.ToString() && success5 != true)
                                                {
                                                    success5 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (!drRules["Data5"].ToString().Contains(value.ToString()))
                                    {
                                        success5 = true;
                                    }
                                    DATA = value;
                                }
                            }
                            else if (drRules["Condition5"].ToString() == "Not Applicable")
                            {
                                if (drRules["Value5"].ToString() != "")
                                {
                                    DATA = drRules["Value5"].ToString();
                                }
                                else if (drRules["Formula5"].ToString() != "")
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data5"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    DATA = value;
                                }
                            }

                            if (success4 == true && drRules["Math_Symbol5"].ToString() == "AND" && success5 == false)
                            {
                                Query = false;
                            }
                            else if (success4 == true && drRules["Math_Symbol5"].ToString() == "OR" && success5 == false)
                            {
                                Query = true;
                            }
                        }

                        if (Query)
                        {
                            if (drRules["Action"].ToString() == "Generate Query")
                            {
                                //Resolve_Query(
                                //    drRules["RULE_ID"].ToString(),
                                //    SUBJID,
                                //    drRules["VARIABLENAME"].ToString()
                                //    );

                                string QueryText = drRules["QUERYTEXT"].ToString();

                                if (QueryText.Contains("@DATA"))
                                {
                                    QueryText = QueryText.Replace("@DATA", drRules["Data"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@VALUE".ToUpperInvariant()))
                                {
                                    QueryText = QueryText.Replace("@VALUE", drRules["Value"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@FORMULA".ToUpperInvariant()))
                                {
                                    string value = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    QueryText = QueryText.Replace("@FORMULA", value);
                                }

                                if (QueryText.Contains("@DATA1"))
                                {
                                    QueryText = QueryText.Replace("@DATA1", drRules["Data1"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@VALUE1".ToUpperInvariant()))
                                {
                                    QueryText = QueryText.Replace("@VALUE1", drRules["Value1"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@FORMULA1".ToUpperInvariant()))
                                {
                                    string value = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    QueryText = QueryText.Replace("@FORMULA1", value);
                                }

                                if (QueryText.Contains("@DATA2"))
                                {
                                    QueryText = QueryText.Replace("@DATA2", drRules["Data2"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@VALUE2".ToUpperInvariant()))
                                {
                                    QueryText = QueryText.Replace("@VALUE2", drRules["Value2"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@FORMULA2".ToUpperInvariant()))
                                {
                                    string value = GetValue_Formula(drRules["Formula2"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    QueryText = QueryText.Replace("@FORMULA2", value);
                                }

                                if (QueryText.Contains("@DATA3"))
                                {
                                    QueryText = QueryText.Replace("@DATA3", drRules["Data3"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@VALUE3".ToUpperInvariant()))
                                {
                                    QueryText = QueryText.Replace("@VALUE3", drRules["Value3"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@FORMULA3".ToUpperInvariant()))
                                {
                                    string value = GetValue_Formula(drRules["Formula3"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    QueryText = QueryText.Replace("@FORMULA3", value);
                                }

                                if (QueryText.Contains("@DATA4"))
                                {
                                    QueryText = QueryText.Replace("@DATA4", drRules["Data4"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@VALUE4".ToUpperInvariant()))
                                {
                                    QueryText = QueryText.Replace("@VALUE4", drRules["Value4"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@FORMULA4".ToUpperInvariant()))
                                {
                                    string value = GetValue_Formula(drRules["Formula4"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    QueryText = QueryText.Replace("@FORMULA4", value);
                                }

                                if (QueryText.Contains("@DATA5"))
                                {
                                    QueryText = QueryText.Replace("@DATA5", drRules["Data5"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@VALUE5".ToUpperInvariant()))
                                {
                                    QueryText = QueryText.Replace("@VALUE5", drRules["Value5"].ToString());
                                }

                                if (QueryText.ToUpperInvariant().Contains("@FORMULA5".ToUpperInvariant()))
                                {
                                    string value = GetValue_Formula(drRules["Formula5"].ToString(), drRules["Data"].ToString(), Project_ID, SUBJID, RECID).Trim();
                                    QueryText = QueryText.Replace("@FORMULA5", value);
                                }

                                Generate_Query(
                                    drRules["RULE_ID"].ToString(),
                                    drRules["Nature"].ToString(),
                                    drData["PVID"].ToString(),
                                    "",
                                    drData["RECID"].ToString(),
                                    SUBJID,
                                    drRules["Data"].ToString(),
                                    QueryText,
                                    drRules["Module_ID"].ToString(),
                                    drRules["Field_ID"].ToString(),
                                    drRules["VARIABLENAME"].ToString(),
                                    drRules["Field_ID1"].ToString(),
                                    drRules["VARIABLENAME1"].ToString(),
                                    drRules["Field_ID2"].ToString(),
                                    drRules["VARIABLENAME2"].ToString(),
                                    drRules["Field_ID3"].ToString(),
                                    drRules["VARIABLENAME3"].ToString(),
                                    drRules["Field_ID4"].ToString(),
                                    drRules["VARIABLENAME4"].ToString(),
                                    drRules["Field_ID5"].ToString(),
                                    drRules["VARIABLENAME5"].ToString(),
                                    drRules["Informational"].ToString()
                                    );
                            }
                            else if (drRules["Action"].ToString() == "Set Field Value")
                            {
                                if (drRules["Value"].ToString() != "")
                                {
                                    DATA = drRules["Value"].ToString();
                                }
                                else if (drRules["Data1"].ToString() != "")
                                {
                                    DATA = drRules["Data1"].ToString().Replace("dd/", "01/").Replace("mm/", "01/");
                                }
                                else
                                {
                                    DATA = GetValue_Formula(drRules["Formula"].ToString(), drRules["Data"].ToString(), Session["PROJECTID"].ToString(), SUBJID, RECID).Replace("dd/", "01/").Replace("mm/", "01/");
                                }

                                string FORMULA1 = "";
                                if (drRules["Formula1"].ToString() != "")
                                {
                                    FORMULA1 = GetValue_Formula(drRules["Formula1"].ToString(), drRules["Data"].ToString(), Session["PROJECTID"].ToString(), SUBJID, RECID).Replace("dd/", "01/").Replace("mm/", "01/");
                                }

                                if (drRules["VARIABLENAME"].ToString().ToUpperInvariant().Contains("age".ToUpperInvariant()))
                                {
                                    dal.DM_RUN_RULE(Action: "Set_Age",
                                    Project_ID: Session["PROJECTID"].ToString(),
                                    PVID: PVID,
                                    SUBJID: SUBJID,
                                    ContID: "",
                                    Para_Visit_ID: VISITID,
                                    Para_ModuleName: drRules["MODULENAME"].ToString(),
                                    VARIABLENAME: drRules["VARIABLENAME"].ToString(),
                                    Data: DATA,
                                    FORMULA1: FORMULA1
                                    );
                                }
                                else
                                {
                                    Set_Value(drRules["VARIABLENAME"].ToString(),
                                         "",
                                         DATA,
                                         TABLENAME,
                                         PVID,
                                         RECID,
                                         SUBJID,
                                         VISITID
                                         );
                                }
                            }
                        }
                        else
                        {
                            Resolve_Query(
                                drRules["RULE_ID"].ToString(),
                                SUBJID,
                                drRules["VARIABLENAME"].ToString()
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double Calculate(string FormulaValue)
        {
            try
            {
                double result = Convert.ToDouble(new DataTable().Compute(FormulaValue, null));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Generate_Query(string RULE_ID, string Nature, string PVID, string ContID, string RECID, string SUBJID, string Data, string QUERYTEXT, string Module_ID,
            string Field_ID, string VARIABLENAME, string Field_ID1, string VARIABLENAME1,
            string Field_ID2, string VARIABLENAME2, string Field_ID3, string VARIABLENAME3,
            string Field_ID4, string VARIABLENAME4, string Field_ID5, string VARIABLENAME5
            , string Informational)
        {
            try
            {
                dal.DM_RUN_RULE(Action: "Generate_Query",
                RULE_ID: RULE_ID,
                Nature: Nature,
                PVID: PVID,
                ContID: ContID,
                RECID: RECID,
                SUBJID: SUBJID,
                Data: Data,
                QUERYTEXT: QUERYTEXT,
                Module_ID: Module_ID,
                Field_ID: Field_ID,
                VARIABLENAME: VARIABLENAME,
                Informational: Informational
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Resolve_Query(string RULE_ID, string SUBJID, string VARIABLENAME)
        {
            try
            {
                dal.DM_RUN_RULE(Action: "Resolve_Query",
                RULE_ID: RULE_ID,
                SUBJID: SUBJID,
                VARIABLENAME: VARIABLENAME
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Set_Value(string VariableName, string ContID, string Data, string TABLENAME, string PVID, string RECID, string SUBJID, string VISITID)
        {
            try
            {
                dal.GetSet_DM_ProjectData(
                        Action: "SET_FIELD_VALUE_Multiple",
                        PROJECTID: Session["PROJECTID"].ToString(),
                        PVID: PVID,
                        RECID: RECID,
                        SUBJID: SUBJID,
                        VISITNUM: VISITID,
                        VARIABLENAME: VariableName,
                        TABLENAME: TABLENAME,
                        DATA: Data,
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public string GetValue_Formula(string Formula, string Data, string Project_ID, string SUBJID, string RECID)
        {
            try
            {
                string Result = null;
                string[] FormulaArray = Formula.Split('︹');

                foreach (var item in FormulaArray)
                {
                    if (item.Contains("DATEADD"))
                    {
                        string Days = Between(item, "(", ")");
                        if (Days.Contains(","))
                        {
                            string[] daysArray = Days.Split(',');
                            string date = "";
                            string daysToAdd = "";
                            for (int i = 0; i <= daysArray.Length; i++)
                            {
                                if (i == 0)
                                {
                                    date = GetValue_Formula(daysArray[i].ToString(), Data, Project_ID, SUBJID, RECID);
                                }
                                if (i == 1)
                                {
                                    daysToAdd = daysArray[i].ToString();
                                }
                            }

                            Result = Result + " " + (Convert.ToDateTime(date).AddDays(Convert.ToDouble(daysToAdd))).ToString();

                        }
                        else
                        {
                            Result = Result + " " + (Convert.ToDateTime(Data).AddDays(Convert.ToDouble(Days))).ToString();
                        }

                    }
                    else if (item.Contains("DaysDiff"))
                    {
                        string Days = Between(item, "(", ")");
                        if (Days.Contains(","))
                        {
                            string[] daysArray = Days.Split(',');
                            string from = "";
                            string to = "";
                            for (int i = 0; i <= daysArray.Length; i++)
                            {
                                if (i == 0)
                                {
                                    from = GetValue_Formula(daysArray[i].ToString(), Data, Project_ID, SUBJID, RECID);
                                }
                                if (i == 1)
                                {
                                    to = GetValue_Formula(daysArray[i].ToString(), Data, Project_ID, SUBJID, RECID);
                                }
                            }

                            Result = Result + " " + Convert.ToDateTime(from).Subtract(Convert.ToDateTime(to)).TotalDays;

                        }

                    }
                    else if (item.Contains("Today"))
                    {
                        Result = Result + " " + DateTime.Now.ToShortDateString();
                    }
                    else if (item.Contains("Now"))
                    {
                        Result = Result + " " + DateTime.Now.ToShortTimeString();
                    }
                    else if (item.Contains("Max"))
                    {
                        if (item.Contains("@"))
                        {
                            string V = Between(item, "@V-", "@M-");
                            string M = Between(item, "@M-", "@F-");
                            string F = Between(item, "@F-", "@");
                            DataSet ds = dal.DM_RUN_RULE(Action: "MAX", VISIT: V, TABLE: M, COLUMN: F, Project_ID: Project_ID, SUBJID: SUBJID, RECID: RECID);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Result = Result + " " + ds.Tables[0].Rows[0]["Max"].ToString();
                            }
                            Result = new DataTable().Compute(Result, null).ToString();
                        }
                    }
                    else if (item.Contains("Min"))
                    {
                        if (item.Contains("@"))
                        {
                            string V = Between(item, "@V-", "@M-");
                            string M = Between(item, "@M-", "@F-");
                            string F = Between(item, "@F-", "@");
                            DataSet ds = dal.DM_RUN_RULE(Action: "MIN", VISIT: V, TABLE: M, COLUMN: F, Project_ID: Project_ID, SUBJID: SUBJID, RECID: RECID);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Result = Result + " " + ds.Tables[0].Rows[0]["Min"].ToString();
                            }
                            Result = new DataTable().Compute(Result, null).ToString();
                        }
                    }
                    else if (item.Contains("Sum"))
                    {
                        if (item.Contains("@"))
                        {
                            string V = Between(item, "@V-", "@M-");
                            string M = Between(item, "@M-", "@F-");
                            string F = Between(item, "@F-", "@");
                            DataSet ds = dal.DM_RUN_RULE(Action: "SUM", VISIT: V, TABLE: M, COLUMN: F, Project_ID: Project_ID, SUBJID: SUBJID, RECID: RECID);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Result = Result + " " + ds.Tables[0].Rows[0]["Sum"].ToString();
                            }
                            Result = new DataTable().Compute(Result, null).ToString();
                        }
                    }
                    else if (item.Contains("AVG"))
                    {
                        if (item.Contains("@"))
                        {
                            string V = Between(item, "@V-", "@M-");
                            string M = Between(item, "@M-", "@F-");
                            string F = Between(item, "@F-", "@");
                            DataSet ds = dal.DM_RUN_RULE(Action: "AVG", VISIT: V, TABLE: M, COLUMN: F, Project_ID: Project_ID, SUBJID: SUBJID, RECID: RECID);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Result = Result + " " + ds.Tables[0].Rows[0]["Avg"].ToString();
                            }
                            Result = new DataTable().Compute(Result, null).ToString();
                        }
                    }
                    else if (item.Contains("COUNT"))
                    {
                        if (item.Contains("@"))
                        {
                            string V = Between(item, "@V-", "@M-");
                            string M = Between(item, "@M-", "@F-");
                            string F = Between(item, "@F-", "@");
                            DataSet ds = dal.DM_RUN_RULE(Action: "COUNT", VISIT: V, TABLE: M, COLUMN: F, Project_ID: Project_ID, SUBJID: SUBJID, RECID: RECID);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Result = Result + " " + ds.Tables[0].Rows[0]["Count"].ToString();
                            }
                            Result = new DataTable().Compute(Result, null).ToString();
                        }
                    }
                    else if (item.Contains("CONCAT"))
                    {
                        string concatValue = Between(item, "(", ")");
                        string ResultItem = null;
                        string[] items = item.Split(',');
                        foreach (var itemList in items)
                        {
                            if (itemList.Contains("@"))
                            {
                                string V = Between(itemList, "@V-", "@M-");
                                string M = Between(itemList, "@M-", "@F-");
                                string F = Between(itemList, "@F-", "@");
                                DataSet ds = dal.DM_RUN_RULE(Action: "DATA", VISIT: V, TABLE: M, COLUMN: F, Project_ID: Project_ID, SUBJID: SUBJID, RECID: RECID);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    ResultItem = ResultItem + ds.Tables[0].Rows[0]["DATA"].ToString();
                                }
                            }
                            else
                            {
                                ResultItem = ResultItem + itemList;
                            }
                        }
                        Result = Result + " " + ResultItem;
                    }
                    else if (item.Contains("@"))
                    {
                        string V = Between(item, "@V-", "@M-");
                        string M = Between(item, "@M-", "@F-");
                        string F = Between(item, "@F-", "@");
                        DataSet ds = dal.DM_RUN_RULE(Action: "DATA", VISIT: V, TABLE: M, COLUMN: F, Project_ID: Project_ID, SUBJID: SUBJID, RECID: RECID);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Result = Result + " " + ds.Tables[0].Rows[0]["DATA"].ToString();
                        }
                        if (!ISDATE(Result))
                        {
                            Result = new DataTable().Compute(Result, null).ToString();
                        }
                    }
                    else
                    {
                        Result = Result + " " + item.ToString();
                    }
                }

                Result = Result.Replace("dd", "01/");
                Result = Result.Replace("mm", "01/");

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Between(string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        public bool IsBetween(int num, int lower, int upper, bool inclusive = false)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }

        public bool ISDATE(string date)
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

        protected void btnMODULE_FIELD_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(fileModuleField);
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

                    DataTable dtExcelSheet = new DataTable();
                    dtExcelSheet.Columns.Add("Column", typeof(String));
                    int cols = excelData.Columns.Count;
                    for (int i = 0; i < cols; i++)
                    {
                        dtExcelSheet.Rows.Add(excelData.Columns[i]);
                    }

                    BIND_DRP_COLS(drpModulename, dtExcelSheet);
                    BIND_DRP_COLS(drpFieldname, dtExcelSheet);
                    BIND_DRP_COLS(drpVarialbleName, dtExcelSheet);
                    BIND_DRP_COLS(drpControlType, dtExcelSheet);
                    BIND_DRP_COLS(drpAnswer, dtExcelSheet);
                    BIND_DRP_COLS(drpSEQNO, dtExcelSheet);
                    BIND_DRP_COLS(drpLenght, dtExcelSheet);
                    BIND_DRP_COLS(drpDescription, dtExcelSheet);

                    ViewState["SCRexcelData"] = excelData;
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
    }
}