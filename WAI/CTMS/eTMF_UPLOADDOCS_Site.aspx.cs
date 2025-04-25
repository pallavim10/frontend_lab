using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_UPLOADDOCS_Site : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        CommonFunction.CommonFunction com = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    if (!this.IsPostBack)
                    {
                        BINDDEPARTMENT();
                        BIND_DOCUMENTTYPE();
                        GetCountry();
                        GetSite();
                        GetUsers();
                        BIND_Functions();
                        GET_INDIVIDUAL();
                        CHECK_VERSION_TYPE();

                        divstatus.Visible = true;
                        divDefaultView.Visible = true;
                        divCountry.Visible = true;
                        divINVID.Visible = true;
                        divIndividual.Visible = true;
                        divfortask.Visible = false;
                        divforeTMF.Visible = false;
                        divdocument.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_Zone();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_Sections();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_Artifacts();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlArtifacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_DOCUMENTS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_DOCUMENTTYPE()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "get_DocType");

                drpDocType.DataSource = ds.Tables[0];
                drpDocType.DataValueField = "ID";
                drpDocType.DataTextField = "DocType";
                drpDocType.DataBind();
                drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_Zone()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "get_Folder", DOCTYPEID: drpDocType.SelectedValue);

                ddlZone.DataSource = ds.Tables[0];
                ddlZone.DataValueField = "ID";
                ddlZone.DataTextField = "Folder";
                ddlZone.DataBind();
                ddlZone.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_Sections()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "get_Sub_Folder", FOLDERID: ddlZone.SelectedValue);

                ddlSections.DataSource = ds.Tables[0];
                ddlSections.DataValueField = "ID";
                ddlSections.DataTextField = "Sub_Folder_Name";
                ddlSections.DataBind();
                ddlSections.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_Artifacts()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "get_ARTIFACTS", SUBFOLDERID: ddlSections.SelectedValue);

                ddlArtifacts.DataSource = ds.Tables[0];
                ddlArtifacts.DataValueField = "ID";
                ddlArtifacts.DataTextField = "Artifact_Name";
                ddlArtifacts.DataBind();
                ddlArtifacts.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BIND_SPEC()
        {
            try
            {
                txtSPEC.Text = "";
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GET_eTMF_SPEC", DocID: drpDocument.SelectedValue);
                string Values = "";
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Values += "" + ds.Tables[0].Rows[i]["VALUE"].ToString() + ",";
                    }
                }
                hfSPECS.Value = Values.TrimEnd(',');

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindSpecs();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAction.SelectedValue == "1")
                {
                    divstatus.Visible = true;
                    divDefaultView.Visible = true;
                    divCountry.Visible = true;
                    divINVID.Visible = true;
                    divIndividual.Visible = true;
                    divfortask.Visible = false;
                    divforeTMF.Visible = false;
                    divdocument.Visible = false;
                }
                else if (ddlAction.SelectedValue == "2")
                {
                    divstatus.Visible = true;
                    divDefaultView.Visible = true;
                    divCountry.Visible = true;
                    divINVID.Visible = true;
                    divIndividual.Visible = true;
                    divfortask.Visible = false;
                    divforeTMF.Visible = true;
                    divdocument.Visible = true;
                }
                else if (ddlAction.SelectedValue == "3")
                {
                    divstatus.Visible = true;
                    divDefaultView.Visible = true;
                    divCountry.Visible = true;
                    divINVID.Visible = true;
                    divIndividual.Visible = true;
                    divfortask.Visible = true;
                    divforeTMF.Visible = false;
                    divdocument.Visible = true;
                }
                else if (ddlAction.SelectedValue == "0")
                {
                    divstatus.Visible = false;
                    divDefaultView.Visible = false;
                    divCountry.Visible = false;
                    divINVID.Visible = false;
                    divIndividual.Visible = false;
                    divfortask.Visible = false;
                    divforeTMF.Visible = false;
                    divdocument.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BINDTASK();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_SUBTASK();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSubTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_DOCUMENTS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BINDDEPARTMENT()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "GET_DEPARTMENT");

                ddldepartment.DataSource = ds;
                ddldepartment.DataValueField = "Dept_ID";
                ddldepartment.DataTextField = "Dept_Name";
                ddldepartment.DataBind();
                ddldepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BINDTASK()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "GET_TASK", ID: ddldepartment.SelectedValue);

                ddlTask.DataSource = ds;
                ddlTask.DataValueField = "Task_ID";
                ddlTask.DataTextField = "Task_Name";
                ddlTask.DataBind();
                ddlTask.Items.Insert(0, new ListItem("--Select Task--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BIND_SUBTASK()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "GET_SUB_TASK", UploadTaskId: ddlTask.SelectedValue);

                ddlSubTask.DataSource = ds;
                ddlSubTask.DataValueField = "Sub_Task_ID";
                ddlSubTask.DataTextField = "Sub_Task_Name";
                ddlSubTask.DataBind();
                ddlSubTask.Items.Insert(0, new ListItem("--Select Sub-Task--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BIND_DOCUMENTS()
        {
            try
            {
                DataSet ds = new DataSet();

                if (divfortask.Visible == true)
                {
                    ds = dal.eTMF_SP(ACTION: "GetDocument_List", UploadTaskId: ddlTask.SelectedValue, UploadSubTaskId: ddlSubTask.SelectedValue);
                }
                else if (divforeTMF.Visible == true)
                {
                    ds = dal.eTMF_SP(ACTION: "GetDocument_List_eTMF",
                    UploadZoneId: ddlZone.SelectedValue,
                    UploadSectionId: ddlSections.SelectedValue,
                    UploadArtifactId: ddlArtifacts.SelectedValue
                    );
                }

                drpDocument.DataSource = ds;
                drpDocument.DataValueField = "ID";
                drpDocument.DataTextField = "DocName";
                drpDocument.DataBind();
                drpDocument.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetCountry()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP(USERID: Session["User_ID"].ToString());
                drpCountry.DataSource = ds.Tables[0];
                drpCountry.DataTextField = "COUNTRYNAME";
                drpCountry.DataValueField = "COUNTRYID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetSite()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP();
                drpSites.DataSource = ds.Tables[1];
                drpSites.DataTextField = "INVID";
                drpSites.DataValueField = "INVID";
                drpSites.DataBind();
                drpSites.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetSite();
                CHECK_DIVS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_INDIVIDUAL();
                CHECK_DIVS();
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
                if (FileUpload1.HasFile)
                {
                    try
                    {
                        Upload();
                        // CLAER();
                        Response.Write("<script> alert('Document Uploaded successfully.');window.location='eTMF_UPLOADDOCS_Site.aspx';</script>");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script> alert('Please select file.')</script>");
                    }
                }
                else
                {
                    Response.Write("<script> alert('Please select file.')</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Upload()
        {
            try
            {
                string folderPath = Server.MapPath("~/eTMF_Docs/");

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }

                string Size = FileUpload1.PostedFile.ContentLength.ToString();
                string extension = FileUpload1.PostedFile.ContentType;

                string UploadFileName = Path.GetFileName(FileUpload1.FileName);

                string folderPath_Editable = Server.MapPath("~/eTMF_Editable/");

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath_Editable))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath_Editable);
                }

                string UploadFileName_Editable = Path.GetFileName(FileUpload2.FileName);

                string Size_Editable = FileUpload2.PostedFile.ContentLength.ToString();
                string extension_Editable = FileUpload2.PostedFile.ContentType;

                string DepartmentId = "", TaskId = "", SubTaskId = "", DocTypeId = "", ZoneId = "", SectionId = "", ArtifactId = "", FUNCTIONS = "";

                if (ddldepartment.SelectedValue == "")
                {
                    DepartmentId = "0";
                }
                else
                {
                    DepartmentId = ddldepartment.SelectedValue;
                }

                if (ddlTask.SelectedValue == "")
                {
                    TaskId = "0";
                }
                else
                {
                    TaskId = ddlTask.SelectedValue;
                }

                if (ddlSubTask.SelectedValue == "")
                {
                    SubTaskId = "0";
                }
                else
                {
                    SubTaskId = ddlSubTask.SelectedValue;
                }

                if (drpDocType.SelectedValue == "")
                {
                    DocTypeId = "0";
                }
                else
                {
                    DocTypeId = drpDocType.SelectedValue;
                }

                if (ddlZone.SelectedValue == "")
                {
                    ZoneId = "0";
                }
                else
                {
                    ZoneId = ddlZone.SelectedValue;
                }

                if (ddlSections.SelectedValue == "")
                {
                    SectionId = "0";
                }
                else
                {
                    SectionId = ddlSections.SelectedValue;
                }

                if (ddlArtifacts.SelectedValue == "")
                {
                    ArtifactId = "0";
                }
                else
                {
                    ArtifactId = ddlArtifacts.SelectedValue;
                }

                if (ddlFunction.SelectedValue == "")
                {
                    FUNCTIONS = "0";
                }
                else
                {
                    FUNCTIONS = ddlFunction.SelectedValue;
                }

                if (drpDocument.SelectedValue == "")
                {
                    Upload_Documents_WITHOUTDOC(
                            UploadFileName,
                            Size,
                            extension,
                            DepartmentId,
                            TaskId,
                            SubTaskId,
                            DocTypeId,
                            ZoneId,
                            SectionId,
                            ArtifactId,
                            FUNCTIONS,
                            folderPath,
                            UploadFileName_Editable,
                            Size_Editable,
                            extension_Editable,
                            folderPath_Editable
                            );
                }
                else
                {
                    if (hfVerDATE.Value == "True" && hfVerSPEC.Value == "True")
                    {
                        if (drpAction.SelectedValue == "Draft")
                        {
                            Upload_Documents_withDOC_ByDate_BySPEC_Draft(
                                UploadFileName,
                                Size,
                                extension,
                                "0",
                                "0",
                                "0",
                                DocTypeId,
                                ZoneId,
                                SectionId,
                                ArtifactId,
                                FUNCTIONS,
                                folderPath,
                                UploadFileName_Editable,
                                Size_Editable,
                                extension_Editable,
                                folderPath_Editable
                                );

                        }
                        else
                        {

                            Upload_Documents_withDOC_ByDate_BySPEC(
                                UploadFileName,
                                Size,
                                extension,
                                "0",
                                "0",
                                "0",
                                DocTypeId,
                                ZoneId,
                                SectionId,
                                ArtifactId,
                                FUNCTIONS,
                                folderPath,
                                UploadFileName_Editable,
                                Size_Editable,
                                extension_Editable,
                                folderPath_Editable
                                );
                        }
                    }
                    else if (hfVerDATE.Value == "True")
                    {
                        if (drpAction.SelectedValue == "Draft")
                        {
                            Upload_Documents_withDOC_ByDate_Draft(
                                UploadFileName,
                                Size,
                                extension,
                                DepartmentId,
                                TaskId,
                                SubTaskId,
                                DocTypeId,
                                ZoneId,
                                SectionId,
                                ArtifactId,
                                FUNCTIONS,
                                folderPath,
                                UploadFileName_Editable,
                                Size_Editable,
                                extension_Editable,
                                folderPath_Editable
                                );

                        }
                        else
                        {

                            Upload_Documents_withDOC_ByDate(
                                UploadFileName,
                                Size,
                                extension,
                                DepartmentId,
                                TaskId,
                                SubTaskId,
                                DocTypeId,
                                ZoneId,
                                SectionId,
                                ArtifactId,
                                FUNCTIONS,
                                folderPath,
                                UploadFileName_Editable,
                                Size_Editable,
                                extension_Editable,
                                folderPath_Editable
                                );
                        }
                    }
                    else if (hfVerSPEC.Value == "True")
                    {
                        if (drpAction.SelectedValue == "Draft")
                        {
                            Upload_Documents_withDOC_BySPEC_Draft(
                                UploadFileName,
                                Size,
                                extension,
                                "0",
                                "0",
                                "0",
                                DocTypeId,
                                ZoneId,
                                SectionId,
                                ArtifactId,
                                FUNCTIONS,
                                folderPath,
                                UploadFileName_Editable,
                                Size_Editable,
                                extension_Editable,
                                folderPath_Editable
                                );
                        }
                        else
                        {
                            Upload_Documents_withDOC_BySPEC(
                                UploadFileName,
                                Size,
                                extension,
                                "0",
                                "0",
                                "0",
                                DocTypeId,
                                ZoneId,
                                SectionId,
                                ArtifactId,
                                FUNCTIONS,
                                folderPath,
                                UploadFileName_Editable,
                                Size_Editable,
                                extension_Editable,
                                folderPath_Editable
                                );
                        }
                    }
                    else
                    {
                        if (hfVerDATE.Value == "False" && hfVerSPEC.Value == "False" && hfVerTYPE.Value == "0" && (drpAction.SelectedValue == "Final" || drpAction.SelectedValue == "Draft") && ddlStatus3.SelectedValue == "Replace")
                        {
                            bool isFileSelected = false;
                            string ReplaceFile = "";

                            for (int i = 0; i < gvFiles.Rows.Count; i++)
                            {
                                if (!isFileSelected)
                                {
                                    CheckBox chkSelect = (CheckBox)gvFiles.Rows[i].FindControl("chkSelect");
                                    if (chkSelect.Checked)
                                    {
                                        isFileSelected = true;

                                        Label ID = (Label)gvFiles.Rows[i].FindControl("ID");
                                        ReplaceFile = ID.Text;
                                    }
                                }
                            }

                            if (isFileSelected)
                            {
                                if (drpAction.SelectedValue == "Draft")
                                {
                                    Upload_Documents_REPLACE_Draft(
                                    UploadFileName,
                                    Size,
                                    extension,
                                    DepartmentId,
                                    TaskId,
                                    SubTaskId,
                                    DocTypeId,
                                    ZoneId,
                                    SectionId,
                                    ArtifactId,
                                    FUNCTIONS,
                                    folderPath,
                                    ReplaceFile,
                                    UploadFileName_Editable,
                                    Size_Editable,
                                    extension_Editable,
                                    folderPath_Editable
                                    );
                                }
                                else
                                {
                                    Upload_Documents_REPLACE(
                                        UploadFileName,
                                        Size,
                                        extension,
                                        DepartmentId,
                                        TaskId,
                                        SubTaskId,
                                        DocTypeId,
                                        ZoneId,
                                        SectionId,
                                        ArtifactId,
                                        FUNCTIONS,
                                        folderPath,
                                        ReplaceFile,
                                        UploadFileName_Editable,
                                        Size_Editable,
                                        extension_Editable,
                                        folderPath_Editable
                                        );
                                }
                            }
                            else
                            {
                                Response.Write("<script> alert('Please select file to Replace.')</script>");
                            }

                        }
                        else if (hfVerDATE.Value == "False" && hfVerTYPE.Value == "0" && (drpAction.SelectedValue == "Final" || drpAction.SelectedValue == "Draft") && ddlStatus3.SelectedValue == "Current")
                        {
                            if (drpAction.SelectedValue == "Draft")
                            {
                                Upload_Documents_Current_Draft(
                                UploadFileName,
                                Size,
                                extension,
                                DepartmentId,
                                TaskId,
                                SubTaskId,
                                DocTypeId,
                                ZoneId,
                                SectionId,
                                ArtifactId,
                                FUNCTIONS,
                                folderPath,
                                UploadFileName_Editable,
                                Size_Editable,
                                extension_Editable,
                                folderPath_Editable
                                );
                            }
                            else
                            {
                                Upload_Documents_Current(
                                    UploadFileName,
                                    Size,
                                    extension,
                                    DepartmentId,
                                    TaskId,
                                    SubTaskId,
                                    DocTypeId,
                                    ZoneId,
                                    SectionId,
                                    ArtifactId,
                                    FUNCTIONS,
                                    folderPath,
                                    UploadFileName_Editable,
                                    Size_Editable,
                                    extension_Editable,
                                    folderPath_Editable
                                    );
                            }
                        }
                        else
                        {
                            if (drpAction.SelectedValue == "Draft")
                            {
                                Upload_Documents_withDOC_Draft(
                                UploadFileName,
                                Size,
                                extension,
                                DepartmentId,
                                TaskId,
                                SubTaskId,
                                DocTypeId,
                                ZoneId,
                                SectionId,
                                ArtifactId,
                                FUNCTIONS,
                                folderPath,
                                UploadFileName_Editable,
                                Size_Editable,
                                extension_Editable,
                                folderPath_Editable
                                );
                            }
                            else
                            {
                                Upload_Documents_withDOC(
                                UploadFileName,
                                Size,
                                extension,
                                DepartmentId,
                                TaskId,
                                SubTaskId,
                                DocTypeId,
                                ZoneId,
                                SectionId,
                                ArtifactId,
                                FUNCTIONS,
                                folderPath,
                                UploadFileName_Editable,
                                Size_Editable,
                                extension_Editable,
                                folderPath_Editable
                                );
                            }
                        }
                    }
                }

                CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();
                string SUBJECT = "Protocol ID- " + Session["PROJECTIDTEXT"].ToString() + " :- eTMF Document Upload Alert.";
                string BODY = "Dear Team, <br/> " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " on " + commFun.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm") + ".";
                com.Email_Users(txtToEmailIds.Text + "," + hfOwnerEmailId.Value, txtCcEmailIds.Text, SUBJECT, BODY, "it@diagnosearch.com");

                CLAER();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void CLAER()
        {
            ddlAction.SelectedIndex = 0;
            divforeTMF.Visible = false;
            divfortask.Visible = false;
            divDefaultView.Visible = false;
            divstatus.Visible = false;
            divdocument.Visible = false;
            drpDocType.SelectedIndex = 0;
            ddlZone.Items.Clear();
            ddlSections.Items.Clear();
            ddlArtifacts.Items.Clear();

            ddldepartment.SelectedIndex = 0;
            ddlTask.Items.Clear();
            ddlSubTask.Items.Clear();
            drpDocument.Items.Clear();

            txtDeadline.Text = "";
            drpCountry.SelectedIndex = 0;
            drpSites.SelectedIndex = 0;
            txtExpiryDate.Text = "";
            ddlFunction.SelectedIndex = 0;
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
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

        protected void drpAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CHECK_DIVS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CHECK_DIVS()
        {
            try
            {
                if (drpAction.SelectedValue == "Draft")
                {
                    divStatus2.Visible = true;
                    divStatus3.Visible = true;
                    ddlFinalStatus.CssClass += " required";
                    ddlStatus3.CssClass += " required";

                    if (ddlStatus3.SelectedValue == "Replace")
                    {
                        divFileReplace.Visible = true;
                    }
                    else
                    {
                        divFileReplace.Visible = false;
                    }
                }
                else if (drpAction.SelectedValue == "Final" && hfVerDATE.Value == "False" && hfVerTYPE.Value == "0")
                {
                    divStatus2.Visible = false;
                    divStatus3.Visible = true;
                    ddlFinalStatus.CssClass = ddlFinalStatus.CssClass.Replace("required", "");
                    ddlStatus3.CssClass += " required";

                    if (ddlStatus3.SelectedValue == "Replace")
                    {
                        divFileReplace.Visible = true;
                    }
                    else
                    {
                        divFileReplace.Visible = false;
                    }
                }
                else
                {
                    divStatus2.Visible = false;
                    divStatus3.Visible = false;
                    ddlFinalStatus.CssClass = ddlFinalStatus.CssClass.Replace("required", "");
                    ddlStatus3.CssClass = ddlFinalStatus.CssClass.Replace("required", "");

                    divFileReplace.Visible = false;
                }

                DataSet ds = dal.eTMF_SP(ACTION: "GET_FILES_REPLACE",
                DocID: drpDocument.SelectedValue,
                Status: drpAction.SelectedValue,
                SubStatus: ddlFinalStatus.SelectedValue,
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue,
                INDIVIDUAL: ddlIndividual.SelectedValue,
                SUBJID: txtSubject.Text
                );

                gvFiles.DataSource = ds;
                gvFiles.DataBind();

                if (gvFiles.Rows.Count == 0)
                {
                    for (int i = 0; i < ddlStatus3.Items.Count; i++)
                    {
                        if (ddlStatus3.Items[i].Value == "Replace")
                        {
                            ddlStatus3.Items[i].Attributes.Add("class", "disp-none");
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < ddlStatus3.Items.Count; i++)
                    {
                        if (ddlStatus3.Items[i].Value == "Replace")
                        {
                            ddlStatus3.Items[i].Attributes.Remove("class");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetUsers()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(
                ACTION: "GetUsers",
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue
                );
                grd_Users.DataSource = ds;
                grd_Users.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_Functions()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "GET_eTMF_FUNCTIONS");

                ddlFunction.DataSource = ds.Tables[0];
                ddlFunction.DataValueField = "ID";
                ddlFunction.DataTextField = "Functions";
                ddlFunction.DataBind();
                ddlFunction.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlFunction.Items.Insert(1, new ListItem("None", "1"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUploadAgainDoc_Click(object sender, EventArgs e)
        {
            try
            {
                FileUpload1 = (FileUpload)Session["FileUpload1"];

                if (FileUpload1.HasFile)
                {
                    try
                    {
                        Upload();

                        Response.Write("<script> alert('Document Uploaded successfully.');window.location='eTMF_UPLOADDOCS_Site.aspx';</script>");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script> alert('Please select file.')</script>");
                    }
                }
                else
                {
                    Response.Write("<script> alert('Please select file.')</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void drpDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetFiles();
                CHECK_VERSION_TYPE();
                CHECK_DIVS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void CHECK_VERSION_TYPE()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "GET_VERSION_TYPE", DocID: drpDocument.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hfVerDATE.Value = ds.Tables[0].Rows[0]["VerDATE"].ToString();
                    hfVerSPEC.Value = ds.Tables[0].Rows[0]["VerSPEC"].ToString();
                    hfVerTYPE.Value = ds.Tables[0].Rows[0]["VerTYPE"].ToString();
                    hfOwnerEmailId.Value = ds.Tables[0].Rows[0]["Email_ID"].ToString();

                    lblSPECtitle.Text = ds.Tables[0].Rows[0]["SPECtitle"].ToString();

                    hdnEmailEnable.Value = ds.Tables[0].Rows[0]["SetEmail"].ToString();

                    if (ds.Tables[0].Rows[0]["Datetitle"].ToString() != "")
                    {
                        lblDateTitle.Text = ds.Tables[0].Rows[0]["Datetitle"].ToString();
                    }
                    else
                    {
                        lblDateTitle.Text = "Document Date";
                    }

                    if (ds.Tables[0].Rows[0]["Info"].ToString() != "")
                    {
                        lblInstruction.Text = ds.Tables[0].Rows[0]["Info"].ToString();
                        divInstruction.Visible = true;
                    }
                    else
                    {
                        divInstruction.Visible = false;
                    }
                }
                else
                {
                    lblSPECtitle.Text = "";

                    divInstruction.Visible = false;

                    hfVerDATE.Value = "";
                    hfVerSPEC.Value = "";
                    hfVerTYPE.Value = "";
                    hfOwnerEmailId.Value = "";
                }

                if (hfVerDATE.Value == "True")
                {
                    lblRequiredDocDate.Visible = true;
                    txtDocDateTime.CssClass += " required";
                }
                else
                {
                    lblRequiredDocDate.Visible = false;
                    txtDocDateTime.CssClass = txtDocDateTime.CssClass.Replace("required", "");
                }

                BIND_SPEC();
                if (hfVerSPEC.Value == "True")
                {
                    divSPEC.Visible = true;
                    txtSPEC.CssClass += " required";
                }
                else
                {
                    divSPEC.Visible = false;
                    txtSPEC.CssClass = txtSPEC.CssClass.Replace("required", "");
                }

                divCountry.Visible = true;
                divINVID.Visible = true;
                divIndividual.Visible = true;

                drpCountry.CssClass = drpCountry.CssClass.Replace("required", "");
                drpSites.CssClass = drpSites.CssClass.Replace("required", "");
                ddlIndividual.CssClass = ddlIndividual.CssClass.Replace("required", "");
                txtSubject.CssClass = txtSubject.CssClass.Replace("required", "");

                drpCountry.SelectedIndex = 0;
                drpSites.SelectedIndex = 0;
                ddlIndividual.SelectedIndex = 0;
                txtSubject.Text = "";

                if (hfVerTYPE.Value == "Study")
                {
                    divCountry.Visible = false;
                    divINVID.Visible = false;
                    divIndividual.Visible = false;
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Country")
                {
                    drpCountry.CssClass += " required";
                    divINVID.Visible = false;
                    divIndividual.Visible = false;
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Site")
                {
                    drpCountry.CssClass += " required";
                    drpSites.CssClass += " required";
                    divIndividual.Visible = false;
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Individual")
                {
                    ddlIndividual.CssClass += " required";
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Subject")
                {
                    divIndividual.Visible = false;
                    divSubject.Visible = true;
                    txtSubject.CssClass += " required";
                }
                else
                {
                    if (ddlAction.SelectedIndex == 0)
                    {
                        divCountry.Visible = false;
                        divINVID.Visible = false;
                        divIndividual.Visible = false;
                        divSubject.Visible = false;
                    }
                }

                drpAction.SelectedIndex = 0;
                ddlFinalStatus.SelectedIndex = 0;
                ddlStatus3.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void GET_INDIVIDUAL()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(
                ACTION: "GET_INDIVIDUAL",
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue
                );
                ddlIndividual.DataSource = ds.Tables[0];
                ddlIndividual.DataTextField = "User_Name";
                ddlIndividual.DataValueField = "User_ID";
                ddlIndividual.DataBind();
                ddlIndividual.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlFinalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    divDeadline.Visible = true;
                    txtDeadline.Text = "";
                }
                else
                {
                    divDeadline.Visible = false;
                    txtDeadline.Text = "";
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    divUsers.Visible = true;
                }
                else
                {
                    divUsers.Visible = false;
                }

                CHECK_DIVS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlStatus3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CHECK_DIVS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_WITHOUTDOC(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {
                bool SendEmail = false;

                if (divEmail.Attributes["class"].ToString().Contains("disp-none"))
                {
                    SendEmail = false;
                }
                else
                {
                    SendEmail = true;
                }

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_WITHOUTDOC",
                        CountryID: drpCountry.SelectedValue,
                        SiteID: drpSites.SelectedValue,
                        DocID: drpDocument.SelectedValue,
                        UploadFileName: UploadFileName,
                        Size: Size,
                        FileType: extension,
                        Status: drpAction.SelectedValue,
                        UploadBy: Session["User_ID"].ToString(),
                        ExpiryDate: txtExpiryDate.Text,
                        UploadDepartmentId: DepartmentId,
                        UploadTaskId: TaskId,
                        UploadSubTaskId: SubTaskId,
                        DeadlineDate: txtDeadline.Text,
                        UploadDocTypeid: DocTypeId,
                        UploadZoneId: ZoneId,
                        UploadSectionId: SectionId,
                        UploadArtifactId: ArtifactId,
                        DOC_VERSIONNO: txtDovVersionNo.Text,
                        DOC_DATETIME: txtDocDateTime.Text,
                        NOTE: txtNote.Text,
                        Functions: FUNCTIONS,
                        INDIVIDUAL: ddlIndividual.SelectedValue,
                        SUBJID: txtSubject.Text,
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        ToEmailIDs: txtToEmailIds.Text,
                        CCEmailIDs: txtCcEmailIds.Text,
                        SendEmail: SendEmail,
                        DOC_REMINDERDAT: txtReminderDat.Text
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);


                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_withDOC_ByDate(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {
                bool SendEmail = false;

                if (divEmail.Attributes["class"].ToString().Contains("disp-none"))
                {
                    SendEmail = false;
                }
                else
                {
                    SendEmail = true;
                }

                DataSet ds = dal.eTMF_SP
                    (
                    ACTION: "Upload_Documents_withDOC_ByDate",
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue,
                    DocID: drpDocument.SelectedValue,
                    DocName: drpDocument.SelectedItem.Text,
                    UploadFileName: UploadFileName,
                    Size: Size,
                    FileType: extension,
                    Status: drpAction.SelectedValue,
                    UploadBy: Session["User_ID"].ToString(),
                    ExpiryDate: txtExpiryDate.Text,
                    UploadDepartmentId: DepartmentId,
                    UploadTaskId: TaskId,
                    UploadSubTaskId: SubTaskId,
                    DeadlineDate: txtDeadline.Text,
                    UploadDocTypeid: DocTypeId,
                    UploadZoneId: ZoneId,
                    UploadSectionId: SectionId,
                    UploadArtifactId: ArtifactId,
                    DOC_VERSIONNO: txtDovVersionNo.Text,
                    DOC_DATETIME: txtDocDateTime.Text,
                    NOTE: txtNote.Text,
                    Functions: FUNCTIONS,
                    INDIVIDUAL: ddlIndividual.SelectedValue,
                    SUBJID: txtSubject.Text,
                    UploadFileName_Editable: UploadFileName_Editable,
                    Size_Editable: Size_Editable,
                    FileType_Editable: extension_Editable,
                    ToEmailIDs: txtToEmailIds.Text,
                    CCEmailIDs: txtCcEmailIds.Text,
                    SendEmail: SendEmail,
                    DOC_REMINDERDAT: txtReminderDat.Text
                    );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: drpAction.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + drpAction.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_withDOC_ByDate_Draft(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {
                bool SendEmail = false;

                if (divEmail.Attributes["class"].ToString().Contains("disp-none"))
                {
                    SendEmail = false;
                }
                else
                {
                    SendEmail = true;
                }

                DataSet ds = dal.eTMF_SP
                    (
                    ACTION: "Upload_Documents_withDOC_ByDate_Draft",
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue,
                    DocID: drpDocument.SelectedValue,
                    DocName: drpDocument.SelectedItem.Text,
                    UploadFileName: UploadFileName,
                    Size: Size,
                    FileType: extension,
                    Status: drpAction.SelectedValue,
                    UploadBy: Session["User_ID"].ToString(),
                    ExpiryDate: txtExpiryDate.Text,
                    UploadDepartmentId: DepartmentId,
                    UploadTaskId: TaskId,
                    UploadSubTaskId: SubTaskId,
                    DeadlineDate: txtDeadline.Text,
                    UploadDocTypeid: DocTypeId,
                    UploadZoneId: ZoneId,
                    UploadSectionId: SectionId,
                    UploadArtifactId: ArtifactId,
                    DOC_VERSIONNO: txtDovVersionNo.Text,
                    DOC_DATETIME: txtDocDateTime.Text,
                    NOTE: txtNote.Text,
                    Functions: FUNCTIONS,
                    INDIVIDUAL: ddlIndividual.SelectedValue,
                    SUBJID: txtSubject.Text,
                    UploadFileName_Editable: UploadFileName_Editable,
                    Size_Editable: Size_Editable,
                    FileType_Editable: extension_Editable,
                    SubStatus: ddlFinalStatus.SelectedValue,
                    ToEmailIDs: txtToEmailIds.Text,
                    CCEmailIDs: txtCcEmailIds.Text,
                    SendEmail: SendEmail,
                    DOC_REMINDERDAT: txtReminderDat.Text
                    );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: drpAction.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + ddlFinalStatus.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_REPLACE(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string ReplaceFile, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {
                bool SendEmail = false;

                if (divEmail.Attributes["class"].ToString().Contains("disp-none"))
                {
                    SendEmail = false;
                }
                else
                {
                    SendEmail = true;
                }

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_REPLACE",
                        CountryID: drpCountry.SelectedValue,
                        SiteID: drpSites.SelectedValue,
                        DocID: drpDocument.SelectedValue,
                        DocName: drpDocument.SelectedItem.Text,
                        UploadFileName: UploadFileName,
                        Size: Size,
                        FileType: extension,
                        Status: drpAction.SelectedValue,
                        UploadBy: Session["User_ID"].ToString(),
                        ExpiryDate: txtExpiryDate.Text,
                        UploadDepartmentId: DepartmentId,
                        UploadTaskId: TaskId,
                        UploadSubTaskId: SubTaskId,
                        DeadlineDate: txtDeadline.Text,
                        UploadDocTypeid: DocTypeId,
                        UploadZoneId: ZoneId,
                        UploadSectionId: SectionId,
                        UploadArtifactId: ArtifactId,
                        DOC_VERSIONNO: txtDovVersionNo.Text,
                        DOC_DATETIME: txtDocDateTime.Text,
                        NOTE: txtNote.Text,
                        Functions: FUNCTIONS,
                        INDIVIDUAL: ddlIndividual.SelectedValue,
                        SUBJID: txtSubject.Text,
                        ID: ReplaceFile,
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        ToEmailIDs: txtToEmailIds.Text,
                        CCEmailIDs: txtCcEmailIds.Text,
                        SendEmail: SendEmail,
                        DOC_REMINDERDAT: txtReminderDat.Text
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: drpAction.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + drpAction.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_REPLACE_Draft(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string ReplaceFile, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {
                bool SendEmail = false;

                if (divEmail.Attributes["class"].ToString().Contains("disp-none"))
                {
                    SendEmail = false;
                }
                else
                {
                    SendEmail = true;
                }

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_REPLACE_Draft",
                        CountryID: drpCountry.SelectedValue,
                        SiteID: drpSites.SelectedValue,
                        DocID: drpDocument.SelectedValue,
                        DocName: drpDocument.SelectedItem.Text,
                        UploadFileName: UploadFileName,
                        Size: Size,
                        FileType: extension,
                        Status: drpAction.SelectedValue,
                        UploadBy: Session["User_ID"].ToString(),
                        ExpiryDate: txtExpiryDate.Text,
                        UploadDepartmentId: DepartmentId,
                        UploadTaskId: TaskId,
                        UploadSubTaskId: SubTaskId,
                        DeadlineDate: txtDeadline.Text,
                        UploadDocTypeid: DocTypeId,
                        UploadZoneId: ZoneId,
                        UploadSectionId: SectionId,
                        UploadArtifactId: ArtifactId,
                        DOC_VERSIONNO: txtDovVersionNo.Text,
                        DOC_DATETIME: txtDocDateTime.Text,
                        NOTE: txtNote.Text,
                        Functions: FUNCTIONS,
                        INDIVIDUAL: ddlIndividual.SelectedValue,
                        SUBJID: txtSubject.Text,
                        ID: ReplaceFile,
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        SubStatus: ddlFinalStatus.SelectedValue,
                        ToEmailIDs: txtToEmailIds.Text,
                        CCEmailIDs: txtCcEmailIds.Text,
                        SendEmail: SendEmail,
                        DOC_REMINDERDAT: txtReminderDat.Text
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: drpAction.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + ddlFinalStatus.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_Current(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {
                bool SendEmail = false;

                if (divEmail.Attributes["class"].ToString().Contains("disp-none"))
                {
                    SendEmail = false;
                }
                else
                {
                    SendEmail = true;
                }

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_Current",
                        CountryID: drpCountry.SelectedValue,
                        SiteID: drpSites.SelectedValue,
                        DocID: drpDocument.SelectedValue,
                        DocName: drpDocument.SelectedItem.Text,
                        UploadFileName: UploadFileName,
                        Size: Size,
                        FileType: extension,
                        Status: drpAction.SelectedValue,
                        UploadBy: Session["User_ID"].ToString(),
                        ExpiryDate: txtExpiryDate.Text,
                        UploadDepartmentId: DepartmentId,
                        UploadTaskId: TaskId,
                        UploadSubTaskId: SubTaskId,
                        DeadlineDate: txtDeadline.Text,
                        UploadDocTypeid: DocTypeId,
                        UploadZoneId: ZoneId,
                        UploadSectionId: SectionId,
                        UploadArtifactId: ArtifactId,
                        DOC_VERSIONNO: txtDovVersionNo.Text,
                        DOC_DATETIME: txtDocDateTime.Text,
                        NOTE: txtNote.Text,
                        Functions: FUNCTIONS,
                        INDIVIDUAL: ddlIndividual.SelectedValue,
                        SUBJID: txtSubject.Text,
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        ToEmailIDs: txtToEmailIds.Text,
                        CCEmailIDs: txtCcEmailIds.Text,
                        SendEmail: SendEmail,
                        DOC_REMINDERDAT: txtReminderDat.Text
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: drpAction.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + drpAction.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_Current_Draft(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {
                bool SendEmail = false;

                if (divEmail.Attributes["class"].ToString().Contains("disp-none"))
                {
                    SendEmail = false;
                }
                else
                {
                    SendEmail = true;
                }

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_Current_Draft",
                        CountryID: drpCountry.SelectedValue,
                        SiteID: drpSites.SelectedValue,
                        DocID: drpDocument.SelectedValue,
                        DocName: drpDocument.SelectedItem.Text,
                        UploadFileName: UploadFileName,
                        Size: Size,
                        FileType: extension,
                        Status: drpAction.SelectedValue,
                        UploadBy: Session["User_ID"].ToString(),
                        ExpiryDate: txtExpiryDate.Text,
                        UploadDepartmentId: DepartmentId,
                        UploadTaskId: TaskId,
                        UploadSubTaskId: SubTaskId,
                        DeadlineDate: txtDeadline.Text,
                        UploadDocTypeid: DocTypeId,
                        UploadZoneId: ZoneId,
                        UploadSectionId: SectionId,
                        UploadArtifactId: ArtifactId,
                        DOC_VERSIONNO: txtDovVersionNo.Text,
                        DOC_DATETIME: txtDocDateTime.Text,
                        NOTE: txtNote.Text,
                        Functions: FUNCTIONS,
                        INDIVIDUAL: ddlIndividual.SelectedValue,
                        SUBJID: txtSubject.Text,
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        SubStatus: ddlFinalStatus.SelectedValue,
                        ToEmailIDs: txtToEmailIds.Text,
                        CCEmailIDs: txtCcEmailIds.Text,
                        SendEmail: SendEmail,
                        DOC_REMINDERDAT: txtReminderDat.Text
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: drpAction.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + ddlFinalStatus.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_withDOC(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {
                bool SendEmail = false;

                if (divEmail.Attributes["class"].ToString().Contains("disp-none"))
                {
                    SendEmail = false;
                }
                else
                {
                    SendEmail = true;
                }

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_withDOC",
                        CountryID: drpCountry.SelectedValue,
                        SiteID: drpSites.SelectedValue,
                        DocID: drpDocument.SelectedValue,
                        DocName: drpDocument.SelectedItem.Text,
                        UploadFileName: UploadFileName,
                        Size: Size,
                        FileType: extension,
                        Status: drpAction.SelectedValue,
                        UploadBy: Session["User_ID"].ToString(),
                        ExpiryDate: txtExpiryDate.Text,
                        UploadDepartmentId: DepartmentId,
                        UploadTaskId: TaskId,
                        UploadSubTaskId: SubTaskId,
                        DeadlineDate: txtDeadline.Text,
                        UploadDocTypeid: DocTypeId,
                        UploadZoneId: ZoneId,
                        UploadSectionId: SectionId,
                        UploadArtifactId: ArtifactId,
                        DOC_VERSIONNO: txtDovVersionNo.Text,
                        DOC_DATETIME: txtDocDateTime.Text,
                        NOTE: txtNote.Text,
                        Functions: FUNCTIONS,
                        INDIVIDUAL: ddlIndividual.SelectedValue,
                        SUBJID: txtSubject.Text,
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        ToEmailIDs: txtToEmailIds.Text,
                        CCEmailIDs: txtCcEmailIds.Text,
                        SendEmail: SendEmail,
                        DOC_REMINDERDAT: txtReminderDat.Text
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: drpAction.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + drpAction.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_withDOC_Draft(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {
                bool SendEmail = false;

                if (divEmail.Attributes["class"].ToString().Contains("disp-none"))
                {
                    SendEmail = false;
                }
                else
                {
                    SendEmail = true;
                }

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_withDOC_Draft",
                        CountryID: drpCountry.SelectedValue,
                        SiteID: drpSites.SelectedValue,
                        DocID: drpDocument.SelectedValue,
                        DocName: drpDocument.SelectedItem.Text,
                        UploadFileName: UploadFileName,
                        Size: Size,
                        FileType: extension,
                        Status: drpAction.SelectedValue,
                        UploadBy: Session["User_ID"].ToString(),
                        ExpiryDate: txtExpiryDate.Text,
                        UploadDepartmentId: DepartmentId,
                        UploadTaskId: TaskId,
                        UploadSubTaskId: SubTaskId,
                        DeadlineDate: txtDeadline.Text,
                        UploadDocTypeid: DocTypeId,
                        UploadZoneId: ZoneId,
                        UploadSectionId: SectionId,
                        UploadArtifactId: ArtifactId,
                        DOC_VERSIONNO: txtDovVersionNo.Text,
                        DOC_DATETIME: txtDocDateTime.Text,
                        NOTE: txtNote.Text,
                        Functions: FUNCTIONS,
                        INDIVIDUAL: ddlIndividual.SelectedValue,
                        SUBJID: txtSubject.Text,
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        SubStatus: ddlFinalStatus.SelectedValue,
                        ToEmailIDs: txtToEmailIds.Text,
                        CCEmailIDs: txtCcEmailIds.Text,
                        SendEmail: SendEmail,
                        DOC_REMINDERDAT: txtReminderDat.Text
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                System.IO.File.Delete(folderPath + SysFileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: ddlFinalStatus.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + ddlFinalStatus.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }







        private void Upload_Documents_withDOC_BySPEC(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {

                DataSet ds = dal.eTMF_SP
                    (
                    ACTION: "Upload_Documents_withDOC_BySPEC",
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue,
                    DocID: drpDocument.SelectedValue,
                    DocName: drpDocument.SelectedItem.Text,
                    UploadFileName: UploadFileName,
                    Size: Size,
                    FileType: extension,
                    Status: drpAction.SelectedValue,
                    UploadBy: Session["User_ID"].ToString(),
                    ExpiryDate: txtExpiryDate.Text,
                    UploadDepartmentId: DepartmentId,
                    UploadTaskId: TaskId,
                    UploadSubTaskId: SubTaskId,
                    DeadlineDate: txtDeadline.Text,
                    UploadDocTypeid: DocTypeId,
                    UploadZoneId: ZoneId,
                    UploadSectionId: SectionId,
                    UploadArtifactId: ArtifactId,
                    DOC_VERSIONNO: txtDovVersionNo.Text,
                    DOC_DATETIME: txtDocDateTime.Text,
                    NOTE: txtNote.Text,
                    Functions: FUNCTIONS,
                    INDIVIDUAL: ddlIndividual.SelectedValue,
                    SUBJID: txtSubject.Text,
                    UploadFileName_Editable: UploadFileName_Editable,
                    Size_Editable: Size_Editable,
                    FileType_Editable: extension_Editable,
                    SPEC: txtSPEC.Text
                    );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: ddlFinalStatus.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + ddlFinalStatus.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_withDOC_BySPEC_Draft(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {

                DataSet ds = dal.eTMF_SP
                    (
                    ACTION: "Upload_Documents_withDOC_BySPEC_Draft",
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue,
                    DocID: drpDocument.SelectedValue,
                    DocName: drpDocument.SelectedItem.Text,
                    UploadFileName: UploadFileName,
                    Size: Size,
                    FileType: extension,
                    Status: drpAction.SelectedValue,
                    UploadBy: Session["User_ID"].ToString(),
                    ExpiryDate: txtExpiryDate.Text,
                    UploadDepartmentId: DepartmentId,
                    UploadTaskId: TaskId,
                    UploadSubTaskId: SubTaskId,
                    DeadlineDate: txtDeadline.Text,
                    UploadDocTypeid: DocTypeId,
                    UploadZoneId: ZoneId,
                    UploadSectionId: SectionId,
                    UploadArtifactId: ArtifactId,
                    DOC_VERSIONNO: txtDovVersionNo.Text,
                    DOC_DATETIME: txtDocDateTime.Text,
                    NOTE: txtNote.Text,
                    Functions: FUNCTIONS,
                    INDIVIDUAL: ddlIndividual.SelectedValue,
                    SUBJID: txtSubject.Text,
                    UploadFileName_Editable: UploadFileName_Editable,
                    Size_Editable: Size_Editable,
                    FileType_Editable: extension_Editable,
                    SubStatus: ddlFinalStatus.SelectedValue,
                    SPEC: txtSPEC.Text
                    );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: ddlFinalStatus.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + ddlFinalStatus.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_withDOC_ByDate_BySPEC_Draft(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {

                DataSet ds = dal.eTMF_SP
                    (
                    ACTION: "Upload_Documents_withDOC_ByDate_BySPEC_Draft",
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue,
                    DocID: drpDocument.SelectedValue,
                    DocName: drpDocument.SelectedItem.Text,
                    UploadFileName: UploadFileName,
                    Size: Size,
                    FileType: extension,
                    Status: drpAction.SelectedValue,
                    UploadBy: Session["User_ID"].ToString(),
                    ExpiryDate: txtExpiryDate.Text,
                    UploadDepartmentId: DepartmentId,
                    UploadTaskId: TaskId,
                    UploadSubTaskId: SubTaskId,
                    DeadlineDate: txtDeadline.Text,
                    UploadDocTypeid: DocTypeId,
                    UploadZoneId: ZoneId,
                    UploadSectionId: SectionId,
                    UploadArtifactId: ArtifactId,
                    DOC_VERSIONNO: txtDovVersionNo.Text,
                    DOC_DATETIME: txtDocDateTime.Text,
                    NOTE: txtNote.Text,
                    Functions: FUNCTIONS,
                    INDIVIDUAL: ddlIndividual.SelectedValue,
                    SUBJID: txtSubject.Text,
                    UploadFileName_Editable: UploadFileName_Editable,
                    Size_Editable: Size_Editable,
                    FileType_Editable: extension_Editable,
                    SubStatus: ddlFinalStatus.SelectedValue,
                    SPEC: txtSPEC.Text
                    );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: ddlFinalStatus.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + ddlFinalStatus.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Upload_Documents_withDOC_ByDate_BySPEC(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {

                DataSet ds = dal.eTMF_SP
                    (
                    ACTION: "Upload_Documents_withDOC_ByDate_BySPEC",
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue,
                    DocID: drpDocument.SelectedValue,
                    DocName: drpDocument.SelectedItem.Text,
                    UploadFileName: UploadFileName,
                    Size: Size,
                    FileType: extension,
                    Status: drpAction.SelectedValue,
                    UploadBy: Session["User_ID"].ToString(),
                    ExpiryDate: txtExpiryDate.Text,
                    UploadDepartmentId: DepartmentId,
                    UploadTaskId: TaskId,
                    UploadSubTaskId: SubTaskId,
                    DeadlineDate: txtDeadline.Text,
                    UploadDocTypeid: DocTypeId,
                    UploadZoneId: ZoneId,
                    UploadSectionId: SectionId,
                    UploadArtifactId: ArtifactId,
                    DOC_VERSIONNO: txtDovVersionNo.Text,
                    DOC_DATETIME: txtDocDateTime.Text,
                    NOTE: txtNote.Text,
                    Functions: FUNCTIONS,
                    INDIVIDUAL: ddlIndividual.SelectedValue,
                    SUBJID: txtSubject.Text,
                    UploadFileName_Editable: UploadFileName_Editable,
                    Size_Editable: Size_Editable,
                    FileType_Editable: extension_Editable,
                    SPEC: txtSPEC.Text
                    );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (FileUpload2.FileName != "")
                {
                    string SysFileName_Editable = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload2.FileName);

                    FileUpload2.SaveAs(folderPath_Editable + SysFileName_Editable);

                    dal.eTMF_SP(ACTION: "UpdateSysFileName_Editable", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName_Editable);
                }

                if (ddlFinalStatus.SelectedValue == "Collaborate" || ddlFinalStatus.SelectedValue == "Review" || ddlFinalStatus.SelectedValue == "QC" || ddlFinalStatus.SelectedValue == "QA Review" || ddlFinalStatus.SelectedValue == "Internal Approval" || ddlFinalStatus.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;
                        string EMAILID = ((Label)grd_Users.Rows[j].FindControl("EMAILID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: ddlFinalStatus.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + ddlFinalStatus.SelectedValue + " mode and assigned to you.";
                            com.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetFiles()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "Get_Files", DocID: drpDocument.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvFilesUploaded.DataSource = ds;
                    gvFilesUploaded.DataBind();
                    divUpoadedDocs.Visible = true;
                    divUploaded.Visible = true;
                    lblUploaded.Text = "Total " + gvFilesUploaded.Rows.Count + " Document(s) are already uploaded.";
                }
                else
                {
                    gvFilesUploaded.DataSource = null;
                    gvFilesUploaded.DataBind();
                    divUpoadedDocs.Visible = false;
                    divUploaded.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvFilesUploaded_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string FileType = drv["FileType"].ToString();
                    Label lbtnFileType = (Label)e.Row.FindControl("lbtnFileType");
                    HtmlControl ICON = (HtmlControl)e.Row.FindControl("ICONCLASS");
                    LinkButton lbtnDownloadDoc = (LinkButton)e.Row.FindControl("lbtnDownloadDoc");
                    string UnblindDoc = drv["Unblind"].ToString();

                    if (UnblindDoc == "Unblinded")
                    {
                        if (Session["Unblind"].ToString() == "Unblinded")
                        {
                            lbtnDownloadDoc.Visible = true;
                        }
                        else
                        {
                            lbtnDownloadDoc.Visible = false;
                        }
                    }
                    else
                    {
                        lbtnDownloadDoc.Visible = true;
                    }

                    if (FileType == "application/vnd.ms-excel" || FileType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        ICON.Attributes.Add("class", "fa fa-file-excel");
                        lbtnFileType.ToolTip = "Excel File";
                    }
                    else if (FileType == "application/msword" || FileType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    {
                        ICON.Attributes.Add("class", "fa fa-file-word");
                        lbtnFileType.ToolTip = "Word File";
                    }
                    else if (FileType == "application/pdf")
                    {
                        ICON.Attributes.Add("class", "fa fa-file-pdf");
                        lbtnFileType.ToolTip = "PDF File";
                    }
                    else if (FileType.Contains("image/"))
                    {
                        ICON.Attributes.Add("class", "fa fa-file-image");
                        lbtnFileType.ToolTip = "Image File";
                    }
                    else if (FileType == "application/vnd.ms-powerpoint" || FileType == "application/vnd.openxmlformats-officedocument.presentationml.presentation")
                    {
                        ICON.Attributes.Add("class", "fa fa-file-powerpoint");
                        lbtnFileType.ToolTip = "PPT File";
                    }
                    else
                    {
                        ICON.Attributes.Add("class", "fa fa-file-text");
                        lbtnFileType.ToolTip = "TEXT File";
                    }

                    HtmlControl iconhistory = (HtmlControl)e.Row.FindControl("iconhistory");

                    string ID = drv["ID"].ToString();
                    DataSet ds = dal.eTMF_SP(ACTION: "GET_VERSION_HISTORY_COUNT", ID: ID);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["COUNTS"].ToString() != "0")
                        {
                            iconhistory.Attributes.Add("style", "color: red;");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvFilesUploaded_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();

                if (e.CommandName == "Download")
                {
                    DataSet ds = dal.eTMF_SP(ACTION: "Download_File", ID: ID);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //File to be downloaded.
                        string fileName = ds.Tables[0].Rows[0]["SysFileName"].ToString();

                        //Set the New File name.
                        string newFileName = ds.Tables[0].Rows[0]["UploadFileName"].ToString();


                        //Path of the File to be downloaded.
                        string filePath = Server.MapPath(string.Format("~/eTMF_Docs/{0}", fileName));

                        //Setting the Content Type, Header and the new File name.
                        Response.ContentType = ds.Tables[0].Rows[0]["FileType"].ToString();
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + newFileName);

                        // Append cookie
                        HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                        cookie.Value = "Flag";
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);
                        // end

                        //Writing the File to Response Stream.
                        Response.WriteFile(filePath);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}