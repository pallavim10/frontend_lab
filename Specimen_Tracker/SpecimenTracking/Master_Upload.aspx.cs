using ExcelDataReader;
using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class Master_Upload : System.Web.UI.Page
    {
        DAL_SETUP Dal_Setup = new DAL_SETUP();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    fileSpecimen.Attributes["onchange"] = "UploadFile(this)";
                    fileSubject.Attributes["onchange"] = "UploadFile(this)";
                    FileBoxList.Attributes["onchange"] = "UploadFile(this)";
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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


                    if (Section == "Specimen")
                    {
                        BIND_DRP_COLS(ddlcolumnSID, dtExcelSheet);
                        BIND_DRP_COLS(ddlcolumnSite, dtExcelSheet);

                        ViewState["Specimen"] = excelData;
                        ddlcolumnSite.Focus();
                    }
                    else if (Section == "Subject")
                    {
                        BIND_DRP_COLS(ddlSubjectID, dtExcelSheet);
                        BIND_DRP_COLS(ddlSiteID, dtExcelSheet);

                        ViewState["Subject"] = excelData;
                        ddlSubjectID.Focus();
                    }
                    else if (Section == "BoxList")
                    {
                        BIND_DRP_COLS(drpsiteId, dtExcelSheet);
                        BIND_DRP_COLS(drpSequenceNo, dtExcelSheet);
                        BIND_DRP_COLS(drpslotno, dtExcelSheet);
                        BIND_DRP_COLS(drpBoxno, dtExcelSheet);


                        ViewState["BoxList"] = excelData;
                        drpBoxno.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void CLEAR_Specimen()
        {
            try
            {
                ddlcolumnSID.Items.Clear();
                ddlcolumnSite.Items.Clear();

                ViewState["Specimen"] = null;
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }

        }

        private void CLEAR_Subject()
        {
            try
            {
                ddlSubjectID.Items.Clear();
                ddlSiteID.Items.Clear();

                ViewState["Subject"] = null;
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnExportSpecimen_Click(object sender, EventArgs e)
        {
            Export_SpecimenID();
        }

        private void Export_SpecimenID()
        {
            try
            {
                string xlname = "SPECIMEN_ID";

                DataSet ds = Dal_Setup.SETUP_UPLOADMASTER_SP(
                   ACTION: "EXPORT_SPECIMEN_ID"
                   );
                DataSet dsExport = new DataSet();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void Export_SubjectID()
        {
            try
            {
                string xlname = "SubjectIDs";

                DataSet ds = Dal_Setup.SETUP_UPLOADMASTER_SP(
                   ACTION: "EXPORT_SUBJECT_ID"
                   );
                DataSet dsExport = new DataSet();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbnexportSubjectIds_Click(object sender, EventArgs e)
        {
            Export_SubjectID();
        }

        private void UPLOAD_SPECIMEN()
        {
            try
            {
                if (ViewState["Specimen"] != null)
                {
                    DataTable dataTable = (DataTable)ViewState["Specimen"];

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        string SID = dr[ddlcolumnSID.SelectedValue].ToString();
                        string SITEID = dr[ddlcolumnSite.SelectedValue].ToString();

                        DataSet ds = Dal_Setup.SETUP_UPLOADMASTER_SP(
                                    ACTION: "UPLOAD_SPECIMENID",
                                    SID: SID,
                                    SITEID: SITEID);

                        dr["Upload result"] = ds.Tables[0].Rows[0]["Results"].ToString();

                    }
                    CLEAR_Specimen();

                    Multiple_Export_Excel.ToExcel(dataTable, "Uploaded Specimen IDs.xlsx", Page.Response);

                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                UPLOAD_SPECIMEN();
               
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            CLEAR_Specimen();
        }

        private void UPLOAD_SUBJECT()
        {
            try
            {
                bool Subject = false;
                if (ViewState["Subject"] != null)
                {
                    DataTable dataTable = (DataTable)ViewState["Subject"];

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        string SITEID = dr[ddlSiteID.SelectedValue].ToString();
                        string SUBJID = dr[ddlSubjectID.SelectedValue].ToString();

                        DataSet ds = Dal_Setup.SETUP_UPLOADMASTER_SP(
                                    ACTION: "UPLOAD_SUBJECTID",
                                    SITEID: SITEID,
                                    SUBJECTID: SUBJID);

                        dr["Upload result"] = ds.Tables[0].Rows[0]["Results"].ToString();

                    }
                    CLEAR_Subject();

                    Multiple_Export_Excel.ToExcel(dataTable, "Uploaded Subject IDs.xlsx", Page.Response);


                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnSubSubject_Click(object sender, EventArgs e)
        {
            try
            {
                UPLOAD_SUBJECT();
                
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnCalSubject_Click(object sender, EventArgs e)
        {
            CLEAR_Subject();
        }

        protected void btnSubcols_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(fileSubject, "Subject");

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnSIDCols_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(fileSpecimen, "Specimen");

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnBoxUpload_Click(object sender, EventArgs e)
        {
            UPLOAD_BOXLIST();
            
        }

        private void UPLOAD_BOXLIST()
        {
            try
            {
                bool boxlist = false;
                if (ViewState["BoxList"] != null)
                {
                    DataTable dataTable = (DataTable)ViewState["BoxList"];

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        string SEQNO = dr[drpSequenceNo.SelectedValue].ToString();
                        string SLOTNO = dr[drpslotno.SelectedValue].ToString();
                        string BOXNO = dr[drpBoxno.SelectedValue].ToString();
                        string SITEID = dr[drpsiteId.SelectedValue].ToString();

                        DataSet ds = Dal_Setup.SETUP_UPLOADMASTER_SP(
                                    ACTION: "UPLOAD_BOXLIST",
                                    SEQNO: SEQNO,
                                    SLOTNO: SLOTNO,
                                    BOXNO: BOXNO,
                                    SITEID: SITEID);

                        dr["Upload result"] = ds.Tables[0].Rows[0]["Results"].ToString();

                    }
                    CLEAR_BOXLIST();

                    Multiple_Export_Excel.ToExcel(dataTable, "Uploaded Box Lists.xlsx", Page.Response);

                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnBoxCancel_Click(object sender, EventArgs e)
        {
            CLEAR_BOXLIST();
        }

        protected void lbtnExportBoxlist_Click(object sender, EventArgs e)
        {
            Export_BOXLIST();
        }

        private void Export_BOXLIST()
        {
            try
            {
                string xlname = "Box List";

                DataSet ds = Dal_Setup.SETUP_UPLOADMASTER_SP(
                   ACTION: "EXPORT_BOXLIST"
                   );
                DataSet dsExport = new DataSet();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void CLEAR_BOXLIST()
        {
            try
            {
                drpSequenceNo.Items.Clear();
                drpBoxno.Items.Clear();
                drpslotno.Items.Clear();

                ViewState["BoxList"] = null;
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnBoxList_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(FileBoxList, "BoxList");

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
    }
}