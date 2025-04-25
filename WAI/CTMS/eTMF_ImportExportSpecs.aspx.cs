using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using Ionic.Zip;
using ExcelDataReader;
using System.Security.Policy;
using CTMS.CommonFunction;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace CTMS
{
    public partial class eTMF_ImportExportSpecs : System.Web.UI.Page
    {

        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();

        DataSet dsUsers = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    fileSPECS.Attributes["onchange"] = "UploadFile(this)";
                    GET_LOGS_SPECS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void LbtnExportSpecs_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SPECS_SP(ACTION: "GET_SPECS_EXPORT");

                string xlname = "eTMF_Specs.xls";

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);

                Response.Write("<script> alert('Specification Exported Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void LbtUploadExportSpecs_Click(object sender, EventArgs e)
        {
            string filename = fileSPECS.FileName;
            if (filename != "")
            {
                try
                {
                    GET_TABLE_DATA(fileSPECS);
                    Response.Write("<script> alert('Specification Imported Successfully.')</script>");
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            else
            {
                Response.Write("<script> alert('PLEASE SELECT EXCEL DOCUMENT TO UPLOAD.')</script>");
            }
        }

        private void UPLOAD_TABLE_DATA(FileUpload fileUpload)
        {
            try
            {
                DataSet excelData = (DataSet)ViewState["excelData"];

                if (excelData.Tables.Count > 0)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {
                        con.Open();
                        bulkCopy.DestinationTableName = "eTMF_SPEC_TBL";

                        if (drpZoneRef.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpZoneRef.SelectedValue].ColumnName, "Zone_RefNo"); }
                        if (drpZone.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpZone.SelectedValue].ColumnName, "Zones"); }
                        if (drpSectionRef.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpSectionRef.SelectedValue].ColumnName, "Section_RefNo"); }
                        if (drpSection.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpSection.SelectedValue].ColumnName, "Sections"); }
                        if (drpArtifact.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpArtifact.SelectedValue].ColumnName, "Artifacts"); }
                        if (drpArtifactRef.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpArtifactRef.SelectedValue].ColumnName, "Artifact_RefNo"); }
                        if (drpArtifactDefin.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpArtifactDefin.SelectedValue].ColumnName, "ArtifactDefin"); }

                        if (drpDocRef.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpDocRef.SelectedValue].ColumnName, "Document_RefNo"); }
                        if (drpDocName.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpDocName.SelectedValue].ColumnName, "Document"); }

                        if (drpAutoReplace.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpAutoReplace.SelectedValue].ColumnName, "AutoReplace"); }
                        if (drpVerType.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpVerType.SelectedValue].ColumnName, "VerType"); }
                        if (drpVerDate.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpVerDate.SelectedValue].ColumnName, "VerDate"); }
                        if (drpVerSPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpVerSPEC.SelectedValue].ColumnName, "VerSPEC"); }

                        if (drpUnblind.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpUnblind.SelectedValue].ColumnName, "Unblind"); }

                        if (drpInstruction.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpInstruction.SelectedValue].ColumnName, "Instruction"); }

                        if (drpcomments.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpcomments.SelectedValue].ColumnName, "Comment"); }

                        if (drpDateTitle.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpDateTitle.SelectedValue].ColumnName, "DateTitle"); }
                        if (drpSPECtitle.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[0].Columns[drpSPECtitle.SelectedValue].ColumnName, "SPECtitle"); }

                        bulkCopy.WriteToServer(excelData.Tables[0]);
                        con.Close();
                    }

                    if (excelData.Tables.Count > 1)
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                        {
                            con.Open();
                            bulkCopy.DestinationTableName = "eTMF_OPTION_TBL";

                            if (drpZoneRef_SPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[1].Columns[drpZoneRef_SPEC.SelectedValue].ColumnName, "Zone_RefNo"); }
                            if (drpZone_SPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[1].Columns[drpZone_SPEC.SelectedValue].ColumnName, "Zones"); }

                            if (drpSectionRef_SPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[1].Columns[drpSectionRef_SPEC.SelectedValue].ColumnName, "Section_RefNo"); }
                            if (drpSection_SPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[1].Columns[drpSection_SPEC.SelectedValue].ColumnName, "Sections"); }

                            if (drpArtifactRef_SPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[1].Columns[drpArtifactRef_SPEC.SelectedValue].ColumnName, "Artifact_RefNo"); }
                            if (drpArtifact_SPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[1].Columns[drpArtifact_SPEC.SelectedValue].ColumnName, "Artifacts"); }

                            if (drpDocRef_SPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[1].Columns[drpDocRef_SPEC.SelectedValue].ColumnName, "Document_RefNo"); }
                            if (drpDocName_SPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[1].Columns[drpDocName_SPEC.SelectedValue].ColumnName, "Document"); }

                            if (drpSEQNO_SPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[1].Columns[drpSEQNO_SPEC.SelectedValue].ColumnName, "SPEC_SEQNO"); }
                            if (drpTEXT_SPEC.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[1].Columns[drpTEXT_SPEC.SelectedValue].ColumnName, "SPEC_TEXT"); }

                            bulkCopy.WriteToServer(excelData.Tables[1]);
                            con.Close();
                        }
                    }

                    if (excelData.Tables.Count > 2)
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                        {
                            con.Open();
                            bulkCopy.DestinationTableName = "eTMF_GROUP_TBL";

                            if (drpZoneRef_GRP.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[2].Columns[drpZoneRef_GRP.SelectedValue].ColumnName, "Zone_RefNo"); }
                            if (drpZone_GRP.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[2].Columns[drpZone_GRP.SelectedValue].ColumnName, "Zones"); }

                            if (drpSectionRef_GRP.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[2].Columns[drpSectionRef_GRP.SelectedValue].ColumnName, "Section_RefNo"); }
                            if (drpSection_GRP.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[2].Columns[drpSection_GRP.SelectedValue].ColumnName, "Sections"); }

                            if (drpArtifactRef_GRP.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[2].Columns[drpArtifactRef_GRP.SelectedValue].ColumnName, "Artifact_RefNo"); }
                            if (drpArtifact_GRP.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[2].Columns[drpArtifact_GRP.SelectedValue].ColumnName, "Artifacts"); }

                            if (drpGroupName_GRP.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[2].Columns[drpGroupName_GRP.SelectedValue].ColumnName, "GroupName"); }
                            if (drpEvent_GRP.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[2].Columns[drpEvent_GRP.SelectedValue].ColumnName, "Event"); }
                            if (drpMilestone_GRP.SelectedValue != "") { bulkCopy.ColumnMappings.Add(excelData.Tables[2].Columns[drpMilestone_GRP.SelectedValue].ColumnName, "Milestone"); }

                            bulkCopy.WriteToServer(excelData.Tables[2]);
                            con.Close();
                        }
                    }
                }

                DataSet ds = dal_eTMF.eTMF_SPECS_SP(ACTION: "IMPORT_SPECS");

                string xlname = "eTMF_Specs-Import Summary.xls";

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
                if (ex.Message.ToString().Contains("Thread being aborted"))
                {
                    Response.Write("<script> alert('Specification Imported Successfully.') window.location.href = 'eTMF_ImportExportSpecs.aspx';</script>");
                }
                else
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
        }

        private void GET_TABLE_DATA(FileUpload fileUpload)
        {
            string filename = fileUpload.FileName;
            if (filename != "")
            {
                bool VerDate = false;
                bool Internal_Value = false;
                bool SPONSOR = false;
                bool SITE = false;
                bool VerSPEC = false;
                bool EmailEnable = false;
                DataTable excelData = (DataTable)ViewState["excelData"];

                DataTable selected = new DataView(excelData).ToTable(false, "EmployeeID", "FirstName", "LastName");

                for (int c = 0; c < excelData.Rows.Count; c++)
                {
                    string Zone_RefNo = excelData.Rows[c][drpZoneRef.SelectedValue].ToString();
                    string Zones = excelData.Rows[c][drpZone.SelectedValue].ToString();
                    string Section_RefNo = excelData.Rows[c][drpSectionRef.SelectedValue].ToString();
                    string Sections = excelData.Rows[c][drpSection.SelectedValue].ToString();
                    string Artifact_RefNo = excelData.Rows[c][drpArtifactRef.SelectedValue].ToString();
                    string Artifacts = excelData.Rows[c][drpArtifact.SelectedValue].ToString();

                    string Document_Ref_No = excelData.Rows[c][drpDocRef.SelectedValue].ToString();
                    string Document = excelData.Rows[c][drpDocName.SelectedValue].ToString();

                    string AutoReplace = excelData.Rows[c][drpAutoReplace.SelectedValue].ToString();
                    string Ver_Type = excelData.Rows[c][drpVerType.SelectedValue].ToString();
                    string Ver_Date = excelData.Rows[c][drpVerDate.SelectedValue].ToString();
                    if (Ver_Date == "Yes")
                    {
                        VerDate = true;
                    }
                    string Ver_SPEC = excelData.Rows[c]["Ver. SPEC"].ToString();

                    string Internal = excelData.Rows[c]["Internal"].ToString();
                    if (Internal == "Yes")
                    {
                        Internal_Value = true;
                    }
                    string Site = excelData.Rows[c]["Site"].ToString();
                    if (Site == "Yes")
                    {
                        SITE = true;
                    }
                    string Sponsor = excelData.Rows[c]["Sponsor"].ToString();
                    if (Sponsor == "Yes")
                    {
                        SPONSOR = true;
                    }
                    string Unblind = excelData.Rows[c]["Access Control"].ToString();
                    if (Ver_SPEC == "Yes")
                    {
                        VerSPEC = true;
                    }

                    string Comment = excelData.Rows[c]["Comment"].ToString();

                    string Instruction = excelData.Rows[c]["Instruction"].ToString();

                    string DateTitle = excelData.Rows[c]["DateTitle"].ToString();

                    string SPECtitle = excelData.Rows[c]["SPECtitle"].ToString();

                    DataSet ds = dal_eTMF.eTMF_SPECS_SP(ACTION: "GET_EXPORTED_DATA");
                }
            }
        }

        private void GET_DRP_COLS(FileUpload fileUpload)
        {
            try
            {
                DataSet excelData = new DataSet();
                string filename = fileUpload.FileName;
                lblFileName.Text = filename;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable(fileUpload, filename);

                    if (excelData.Tables.Count > 0)
                    {
                        divImport.Visible = true;

                        lblDocMasterSheetName.Text = excelData.Tables[0].TableName;
                        lblExpDocSheetName.Text = excelData.Tables[0].TableName;
                        lblSettingSheetName.Text = excelData.Tables[0].TableName;

                        if (excelData.Tables.Count > 1)
                        {
                            divOptions.Visible = true;
                            lblOptionSheetName.Text = excelData.Tables[1].TableName;
                        }
                        else
                        {
                            divOptions.Visible = false;
                        }

                        if (excelData.Tables.Count > 2)
                        {
                            divGroup.Visible = true;
                            lblGroupSheetName.Text = excelData.Tables[2].TableName;
                        }
                        else
                        {
                            divGroup.Visible = false;
                        }

                        for (int t = 0; t < excelData.Tables.Count; t++)
                        {
                            DataTable dtExcelSheet = new DataTable();
                            dtExcelSheet.Columns.Add("Column", typeof(String));
                            int cols = excelData.Tables[t].Columns.Count;
                            for (int i = 0; i < cols; i++)
                            {
                                dtExcelSheet.Rows.Add(excelData.Tables[t].Columns[i]);
                            }

                            if (t == 0)
                            {
                                BIND_DRP_COLS(drpZoneRef, dtExcelSheet);
                                BIND_DRP_COLS(drpZone, dtExcelSheet);
                                BIND_DRP_COLS(drpSectionRef, dtExcelSheet);
                                BIND_DRP_COLS(drpSection, dtExcelSheet);
                                BIND_DRP_COLS(drpArtifact, dtExcelSheet);
                                BIND_DRP_COLS(drpArtifactRef, dtExcelSheet);
                                BIND_DRP_COLS(drpArtifactDefin, dtExcelSheet);

                                BIND_DRP_COLS(drpDocRef, dtExcelSheet);
                                BIND_DRP_COLS(drpDocName, dtExcelSheet);

                                BIND_DRP_COLS(drpAutoReplace, dtExcelSheet);
                                BIND_DRP_COLS(drpVerType, dtExcelSheet);
                                BIND_DRP_COLS(drpVerDate, dtExcelSheet);
                                BIND_DRP_COLS(drpVerSPEC, dtExcelSheet);
                                BIND_DRP_COLS(drpUnblind, dtExcelSheet);

                                BIND_DRP_COLS(drpInstruction, dtExcelSheet);
                                BIND_DRP_COLS(drpDateTitle, dtExcelSheet);
                                BIND_DRP_COLS(drpSPECtitle, dtExcelSheet);
                                BIND_DRP_COLS(drpcomments, dtExcelSheet);


                            }
                            else if (t == 1)
                            {
                                BIND_DRP_COLS(drpZoneRef_SPEC, dtExcelSheet);
                                BIND_DRP_COLS(drpZone_SPEC, dtExcelSheet);
                                BIND_DRP_COLS(drpSectionRef_SPEC, dtExcelSheet);
                                BIND_DRP_COLS(drpSection_SPEC, dtExcelSheet);
                                BIND_DRP_COLS(drpArtifactRef_SPEC, dtExcelSheet);
                                BIND_DRP_COLS(drpArtifact_SPEC, dtExcelSheet);
                                BIND_DRP_COLS(drpDocRef_SPEC, dtExcelSheet);
                                BIND_DRP_COLS(drpDocName_SPEC, dtExcelSheet);
                                BIND_DRP_COLS(drpSEQNO_SPEC, dtExcelSheet);
                                BIND_DRP_COLS(drpTEXT_SPEC, dtExcelSheet);
                            }
                            else if (t == 2)
                            {
                                BIND_DRP_COLS(drpZoneRef_GRP, dtExcelSheet);
                                BIND_DRP_COLS(drpZone_GRP, dtExcelSheet);
                                BIND_DRP_COLS(drpSectionRef_GRP, dtExcelSheet);
                                BIND_DRP_COLS(drpSection_GRP, dtExcelSheet);
                                BIND_DRP_COLS(drpArtifactRef_GRP, dtExcelSheet);
                                BIND_DRP_COLS(drpArtifact_GRP, dtExcelSheet);
                                BIND_DRP_COLS(drpGroupName_GRP, dtExcelSheet);
                                BIND_DRP_COLS(drpEvent_GRP, dtExcelSheet);
                                BIND_DRP_COLS(drpMilestone_GRP, dtExcelSheet);
                            }
                        }

                        ViewState["excelData"] = excelData;
                    }
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

        public DataSet ConvertExcelToDataTable(FileUpload fileUpload, string FileName)
        {
            DataSet dsResult = null;
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
                dsResult = result;
            }
            else if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xls")
            {
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                DataSet result = excelReader.AsDataSet();
                excelReader.Close();
                dsResult = result;
            }
            else
            {
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateCsvReader(stream);
                DataSet result = excelReader.AsDataSet();
                excelReader.Close();
                dsResult = result;
            }

            foreach (DataTable dt in dsResult.Tables)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    dt.Columns[c].ColumnName = dt.Rows[0][c].ToString();
                }

                dt.Rows[0].Delete();
                dt.AcceptChanges();
            }

            return dsResult; //Returning Dattable  
        }

        protected void btnSPECS_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(fileSPECS);
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
                UPLOAD_TABLE_DATA(fileSPECS);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnRandCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("eTMF_ImportExportSpecs.aspx");
        }

        protected void btnexportsmaple_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
                lblHeader.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SPECS_SP(ACTION: "GET_SAMPLE_SPECS");

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                Multiple_Export_Excel.ToExcel_Restricted(dsExport, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LOGS_SPECS()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_ACTION_LOG_SP(ACTION: "GET_LOGS_SPECS");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grd_spec_Log.DataSource = ds;
                    Grd_spec_Log.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Grd_spec_Log_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void lbnsExport_Click(object sender, EventArgs e)
        {
            try
            {
                lblHeader.Text = "eTMF-Specs-History";
                DataSet ds = dal_eTMF.eTMF_ACTION_LOG_SP(ACTION: "GET_LOGS_SPECS");
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}