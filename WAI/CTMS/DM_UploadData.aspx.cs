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

namespace CTMS
{
    public partial class DM_UploadData : System.Web.UI.Page
    {

        DataTable dt_AuditTrail = new DataTable("DM_AUDITTRAIL");

        DAL_DB dal_DB = new DAL_DB();
        DAL_DM dal_DM = new DAL_DM();
        CommonFunction.CommonFunction_DM commFun = new CommonFunction.CommonFunction_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                filePatData.Attributes["multiple"] = "multiple";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

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
                        DataSet dsModule = dal_DB.DB_MAP_SP(ACTION: "GET_SHEEETNAME_DETAILS", SHEET_NAME: filename);

                        DataSet dsCols = dal_DB.DB_MAP_SP(ACTION: "GET_DATA_MAPPINGS", SHEET_NAME: filename);

                        if (dsCols.Tables[0].Rows.Count > 0 && dsModule.Tables[0].Rows.Count > 0)
                        {
                            string MODULENAME = dsModule.Tables[0].Rows[0]["MODULENAME"].ToString();
                            string MODULEID = dsModule.Tables[0].Rows[0]["ID"].ToString();
                            string TABLENAME = dsModule.Tables[0].Rows[0]["TABLENAME"].ToString();
                            string EXT = dsModule.Tables[0].Rows[0]["EXT"].ToString();
                            string EXT_TABLENAME = dsModule.Tables[0].Rows[0]["EXT_TABLENAME"].ToString();
                            string MULTIPLEYN = dsModule.Tables[0].Rows[0]["MULTIPLEYN"].ToString();

                            excelData = ConvertExcelToDataTable_SubjectDATA(filename);

                            DataColumnCollection columns = SingleRowToCol(dsCols.Tables[0]).Columns;

                            for (int i = 0; i < excelData.Rows.Count; i++)
                            {
                                dt_AuditTrail = new DataTable();

                                string COLUMN = "";
                                string DATA = "";
                                string colDATA = "";

                                string INSERTQUERY = "";
                                string UPDATEQUERY = "";

                                string PVID = "", PROJECTID = "", INVID = "", SUBJID = "", VISITID = "",
                                    VISCOUNT = "", SUBJID_DATA = "", VISIT = "", SITEID = "";

                                PROJECTID = Session["PROJECTID"].ToString();
                                INVID = SITEID;
                                SUBJID = SUBJID_DATA;

                                DataSet ds = new DataSet();

                                VISCOUNT = "1";

                                foreach (DataColumn dc in excelData.Columns)
                                {
                                    if (columns.Contains(dc.ColumnName))
                                    {
                                        DataRow[] results = dsCols.Tables[0].Select("SHEETCOL = '" + dc.ColumnName + "'  ");

                                        for (int r = 0; r < results.Length; r++)
                                        {
                                            if (EXT == "True")
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
                                            else
                                            {
                                                if (results[r]["FIELD"].ToString() == "SUBJID_DATA")
                                                {
                                                    SUBJID_DATA = excelData.Rows[i][dc.ColumnName].ToString();
                                                    SUBJID = SUBJID_DATA;

                                                    //if (COLUMN != "")
                                                    //{
                                                    //    COLUMN = COLUMN + " @ni$h SUBJID_DATA";
                                                    //}
                                                    //else
                                                    //{
                                                    //    COLUMN = "SUBJID_DATA";
                                                    //}

                                                    //if (DATA != "")
                                                    //{
                                                    //    DATA = DATA + " @ni$h '" + excelData.Rows[i][dc.ColumnName] + "'";
                                                    //}
                                                    //else
                                                    //{
                                                    //    DATA = "'" + excelData.Rows[i][dc.ColumnName] + "'";
                                                    //}
                                                }
                                                else if (results[r]["FIELD"].ToString() == "VISIT")
                                                {
                                                    VISIT = excelData.Rows[i][dc.ColumnName].ToString();

                                                    ds = dal_DB.DB_MAP_SP(
                                                        ACTION: "GET_IDs_VISIT_MODULE",
                                                        VISIT: VISIT,
                                                        SUBJID: SUBJID,
                                                        MODULENAME: dsCols.Tables[0].Rows[0]["MODULENAME"].ToString()
                                                        );

                                                    VISITID = ds.Tables[0].Rows[0]["VISITID"].ToString();

                                                    //if (COLUMN != "")
                                                    //{
                                                    //    COLUMN = COLUMN + " @ni$h VISITNUM";
                                                    //}
                                                    //else
                                                    //{
                                                    //    COLUMN = "VISITNUM";
                                                    //}

                                                    //if (DATA != "")
                                                    //{
                                                    //    DATA = DATA + " @ni$h '" + VISITID + "'";
                                                    //}
                                                    //else
                                                    //{
                                                    //    DATA = "'" + VISITID + "'";
                                                    //}

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
                                }

                                DataRow[] resultsVAL = dsCols.Tables[0].Select("SHEETCOL = ''  ");

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

                                    if (colROWS[r]["FIELD"].ToString().Replace(" ", "").Split(new string[] { "@ni$h" }, StringSplitOptions.None).Contains(colROWS[r]["SHEETCOL"].ToString()))
                                    {
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

                                        if (DATA != "")
                                        {
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
                                        else
                                        {
                                            if (colDATA != "")
                                            {
                                                colDATA = colDATA.TrimEnd(',', ' ');
                                                DATA = "'" + colDATA + "'";
                                            }
                                            else
                                            {
                                                DATA = " @ni$h NULL";
                                            }
                                        }

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

                                PVID = PROJECTID + "-" + INVID + "-" + SUBJID + "-" + VISITID + "-" + MODULEID + "-" + VISCOUNT;

                                string Last_RECID = GET_RECID(PVID, TABLENAME, SUBJID);

                                int newRECID = Convert.ToInt32(Last_RECID);

                                if (MULTIPLEYN == "False")
                                {
                                    newRECID = 0;
                                }

                                if (EXT == "True")
                                {
                                    INSERTQUERY = "INSERT INTO [" + EXT_TABLENAME + "] ([ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + Session["User_ID"].ToString() + "','" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + ")";
                                }
                                else
                                {
                                    INSERTQUERY = "INSERT INTO [" + TABLENAME + "] ([PVID], [RECID], [SUBJID_DATA], [VISITNUM], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + PVID + "', '" + newRECID + "', '" + SUBJID + "', '" + VISITID + "', '" + Session["User_ID"].ToString() + "','" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + ")";
                                }

                                for (int j = 0; j < colArr.Length; j++)
                                {
                                    if (UPDATEQUERY == "")
                                    {
                                        if (EXT == "True")
                                        {
                                            UPDATEQUERY = "UPDATE [" + EXT_TABLENAME + "] SET UPDATEDDAT = GETDATE(), UPDATEDBYNAME = '" + Session["User_Name"].ToString() + "', UPDATED_TZVAL = '" + Session["TimeZone_Value"].ToString() + "', UPDATEDBY = '" + Session["USER_ID"].ToString() + "' ";
                                        }
                                        else
                                        {
                                            UPDATEQUERY = "UPDATE [" + TABLENAME + "] SET UPDATEDDAT = GETDATE(), UPDATEDBYNAME = '" + Session["User_Name"].ToString() + "', UPDATED_TZVAL = '" + Session["TimeZone_Value"].ToString() + "', UPDATEDBY = '" + Session["USER_ID"].ToString() + "' ";
                                        }

                                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[j] + " = " + dataArr[j] + " ";
                                    }
                                    else
                                    {
                                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[j] + " = " + dataArr[j] + " ";
                                    }

                                    if (EXT != "True")
                                    {
                                        ADD_NEW_ROW_DATA(PVID, SUBJID, TABLENAME, VISITID, MODULENAME, "", colArr[j], "", dataArr[j].Replace("N'", "").Replace("'", ""), "External data uploaded", "", newRECID.ToString());
                                    }
                                }


                                string[] PRIM_colArr = PRIM_COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                                string[] PRIM_dataArr = PRIM_DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);

                                string PRIMQUERY = "";

                                for (int j = 0; j < PRIM_colArr.Length; j++)
                                {
                                    if (PRIM_colArr[j] != "" && PRIM_dataArr[j].Trim() != "")
                                    {
                                        PRIMQUERY += " AND [" + PRIM_colArr[j].Trim() + "] = " + PRIM_dataArr[j] + " ";
                                    }
                                }

                                if (EXT == "True")
                                {
                                    UPDATEQUERY = UPDATEQUERY + " WHERE ID IS NOT NULL ";
                                }
                                else
                                {
                                    if (PRIMQUERY != "")
                                    {
                                        UPDATEQUERY = UPDATEQUERY + " WHERE PVID = '" + PVID + "' " + PRIMQUERY + " ";
                                    }
                                    else
                                    {
                                        UPDATEQUERY = UPDATEQUERY + " WHERE PVID = '" + PVID + "' AND RECID = '" + newRECID.ToString() + "' ";
                                    }
                                }

                                DataSet dsResults = new DataSet();

                                if (EXT == "True")
                                {
                                    dsResults = dal_DB.DB_MAP_SP(
                                        ACTION: "INSERT_MODULE_DATA_EXT",
                                        INSERTQUERY: INSERTQUERY,
                                        UPDATEQUERY: UPDATEQUERY,
                                        TABLENAME: EXT_TABLENAME,
                                        PRIMQUERY: PRIMQUERY
                                        );

                                    excelData.Rows[i]["Upload result"] = dsResults.Tables[1].Rows[0][0].ToString();
                                }
                                else
                                {
                                    dsResults = dal_DB.DB_MAP_SP(
                                        ACTION: "INSERT_MODULE_DATA",
                                        INSERTQUERY: INSERTQUERY,
                                        UPDATEQUERY: UPDATEQUERY,
                                        TABLENAME: TABLENAME,
                                        PVID: PVID,
                                        RECID: newRECID.ToString(),
                                        SUBJID: SUBJID,
                                        MODULEID: MODULEID,
                                        VISITNUM: VISITID,
                                        INVID: INVID,
                                        VISIT: VISIT,
                                        PRIMQUERY: PRIMQUERY
                                        );

                                    dal_DM.DM_GetSetPV(
                                        PVID: PVID,
                                        INVID: INVID,
                                        SUBJID: SUBJID,
                                        MODULEID: MODULEID,
                                        VISITNUM: VISITID,
                                        PAGESTATUS: "1"
                                        );

                                    string UploadResult = dsResults.Tables[2].Rows[0][0].ToString();

                                    if (dsResults.Tables.Count > 3)
                                    {
                                        UploadResult = dsResults.Tables[3].Rows[0][0].ToString();
                                    }

                                    excelData.Rows[i]["Upload result"] = UploadResult;

                                    if (UploadResult.ToString().Contains("Data added") || UploadResult.ToString().Contains("Data updated"))
                                    {
                                        //Insert Bulk Audit Trail dataset
                                        if (dt_AuditTrail.Rows.Count > 0)
                                        {
                                            SqlConnection con = new SqlConnection(dal_DM.getconstr());

                                            using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                                            {
                                                if (con.State != ConnectionState.Open) { con.Open(); }

                                                sqlbc.DestinationTableName = "DM_AUDITTRAIL";

                                                sqlbc.ColumnMappings.Add("SOURCE", "SOURCE");
                                                sqlbc.ColumnMappings.Add("PVID", "PVID");
                                                sqlbc.ColumnMappings.Add("RECID", "RECID");
                                                sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                                                sqlbc.ColumnMappings.Add("VISITNUM", "VISITNUM");
                                                sqlbc.ColumnMappings.Add("MODULENAME", "MODULENAME");
                                                sqlbc.ColumnMappings.Add("FIELDNAME", "FIELDNAME");
                                                sqlbc.ColumnMappings.Add("TABLENAME", "TABLENAME");
                                                sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                                                sqlbc.ColumnMappings.Add("OLDVALUE", "OLDVALUE");
                                                sqlbc.ColumnMappings.Add("NEWVALUE", "NEWVALUE");
                                                sqlbc.ColumnMappings.Add("REASON", "REASON");
                                                sqlbc.ColumnMappings.Add("COMMENTS", "COMMENTS");
                                                sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                                                sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                                                sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                                                sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");
                                                sqlbc.ColumnMappings.Add("DM", "DM");

                                                sqlbc.WriteToServer(dt_AuditTrail);

                                                dt_AuditTrail.Clear();
                                            }
                                        }
                                    }

                                    commFun.AutoSet_ESOURCE_PVs(PVID, newRECID.ToString());
                                }

                            }

                            Response.Write("<script> alert('Data uploaded successfully. Please find downloaded Upload summary.');</script>");

                            Multiple_Export_Excel.ToExcel(excelData, filename + ".xls", Page.Response);
                            Response.End();
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

        protected void ADD_NEW_ROW_DATA(string PVID, string SUBJID, string TABLENAME, string VISITID, string MODULENAME, string FieldName, string VariableName, string OldValue, string NewValue, string Reason, string Comment, string RECID)
        {
            try
            {
                CREATE_DM_AUDITTRAIL_DT();

                DataRow myDataRow;
                myDataRow = dt_AuditTrail.NewRow();
                myDataRow["SOURCE"] = "External";
                myDataRow["PVID"] = PVID;
                myDataRow["RECID"] = RECID;
                myDataRow["SUBJID"] = SUBJID;
                myDataRow["VISITNUM"] = VISITID;
                myDataRow["MODULENAME"] = MODULENAME;
                myDataRow["FIELDNAME"] = FieldName.Trim();
                myDataRow["TABLENAME"] = TABLENAME;
                myDataRow["VARIABLENAME"] = VariableName.Trim();
                myDataRow["REASON"] = Reason;
                myDataRow["COMMENTS"] = Comment;
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                myDataRow["OLDVALUE"] = REMOVEHTML(OldValue).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["NEWVALUE"] = REMOVEHTML(NewValue).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["DM"] = "True";
                dt_AuditTrail.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CREATE_DM_AUDITTRAIL_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (dt_AuditTrail.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    dt_AuditTrail.Columns.Add("SOURCE");
                    dt_AuditTrail.Columns.Add("PVID");
                    dt_AuditTrail.Columns.Add("RECID");
                    dt_AuditTrail.Columns.Add("SUBJID");
                    dt_AuditTrail.Columns.Add("VISITNUM");
                    dt_AuditTrail.Columns.Add("MODULENAME");
                    dt_AuditTrail.Columns.Add("FIELDNAME");
                    dt_AuditTrail.Columns.Add("TABLENAME");
                    dt_AuditTrail.Columns.Add("VARIABLENAME");
                    dt_AuditTrail.Columns.Add("REASON");
                    dt_AuditTrail.Columns.Add("COMMENTS");
                    dt_AuditTrail.Columns.Add("ENTEREDBY");
                    dt_AuditTrail.Columns.Add("ENTEREDBYNAME");
                    dt_AuditTrail.Columns.Add("ENTEREDDAT");
                    dt_AuditTrail.Columns.Add("ENTERED_TZVAL");
                    dt_AuditTrail.Columns.Add("OLDVALUE");
                    dt_AuditTrail.Columns.Add("NEWVALUE");
                    dt_AuditTrail.Columns.Add("DM");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected static string REMOVEHTML(string str)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");

            return str;
        }

        protected void btnancelModuleField_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DM_UploadData.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string GET_RECID(string PVID, string TABLENAME, string SUBJID)
        {
            string RECID = "0";
            try
            {
                DataSet ds = dal_DM.DM_RECID_SP(
                    PVID: PVID,
                    TABLENAME: TABLENAME,
                    SUBJID: SUBJID
                    );

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
            filePatData.SaveAs(savepath + @"\" + FileName);
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
    }
}