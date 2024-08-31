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
                    //FileUploadEXL.Attributes.Add("onchange", "UploadFile(this)");
                    //UploadSubjectfile.Attributes.Add("onchange", "UploadFile(this)");
                    fileSubject.Attributes["onchange"] = "UploadFile(this)";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
                    }
                    else if (Section == "Subject")
                    {
                        BIND_DRP_COLS(ddlSubjectID, dtExcelSheet);
                        BIND_DRP_COLS(ddlSiteID, dtExcelSheet);


                        ViewState["Subject"] = excelData;
                    }

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
                ex.Message.ToString();
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
                ex.Message.ToString();
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
                ex.Message.ToString();
            }
        }

        protected void lbtnExportSpecimen_Click(object sender, EventArgs e)
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
                ex.Message.ToString();
            }
        }

        protected void lbnexportSubjectIds_Click(object sender, EventArgs e)
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
                ex.Message.ToString();
            }
        }

        private void UPLOAD_SPECIMEN()
        {
            try
            {
                bool Specimen = false;
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

                        Specimen = true;

                    }
                    if(Specimen == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Specimen IDs Upload Successfully.', 'success');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                UPLOAD_SPECIMEN();
                CLEAR_Specimen();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
                        Subject = true;
                    }

                    if(Subject == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Subject IDs Upload Successfully.', 'success');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnSubSubject_Click(object sender, EventArgs e)
        {
            try
            {
                UPLOAD_SUBJECT();
                CLEAR_Subject();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
                ex.Message.ToString();
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
                ex.Message.ToString();
            }
        }

    }
}