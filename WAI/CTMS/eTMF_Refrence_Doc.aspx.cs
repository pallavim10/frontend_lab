using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;

namespace CTMS
{
    public partial class eTMF_Refrence_Doc : System.Web.UI.Page
    {
        DAL dal = new DAL();
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
                        DataSet ds = dal.eTMF_SP(ACTION: "GET_STRUCTURE_DATA", ID: Request.QueryString["ID"].ToString());

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblZones.Text = ds.Tables[0].Rows[0]["Folder"].ToString();
                            lblSections.Text = ds.Tables[0].Rows[0]["Sub_Folder_Name"].ToString();

                            hdnZoneId.Value = ds.Tables[0].Rows[0]["ZONEID"].ToString();
                            hdnSectionId.Value = ds.Tables[0].Rows[0]["SECTIONID"].ToString();

                            BIND_Artifacts();

                            ddlArtifacts.SelectedValue = ds.Tables[1].Rows[0]["UploadArtifactId"].ToString();

                            BIND_DOCUMENTS();

                            drpDocument.SelectedValue = ds.Tables[1].Rows[0]["DocID"].ToString();
                        }

                        GetCountry();
                        GetSite();
                        GetUsers();
                        BIND_Functions();
                        GET_INDIVIDUAL();
                        CHECK_VERSION_TYPE();
                    }
                }
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

        private void BIND_Artifacts()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "get_ARTIFACTS", SUBFOLDERID: hdnSectionId.Value);

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

        protected void BIND_DOCUMENTS()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal.eTMF_SP(ACTION: "GetDocument_List_eTMF",
                    UploadZoneId: hdnZoneId.Value,
                    UploadSectionId: hdnSectionId.Value,
                    UploadArtifactId: ddlArtifacts.SelectedValue
                    );

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
                        string URL = Session["prevURL"].ToString();
                        Response.Write("<script> alert('Document Uploaded successfully.');window.location='" + URL + "';</script>");
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
                    if (hfVerDATE.Value == "True")
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
                    else
                    {
                        if (hfVerDATE.Value == "False" && hfVerTYPE.Value == "0" && (drpAction.SelectedValue == "Final" || drpAction.SelectedValue == "Draft") && ddlStatus3.SelectedValue == "Replace")
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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
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
                if (drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
                {
                    divDeadline.Visible = true;
                    txtDeadline.Text = "";
                }
                else
                {
                    divDeadline.Visible = false;
                    txtDeadline.Text = "";
                }

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
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

        private void CHECK_DIVS()
        {
            try
            {
                if (drpAction.SelectedValue == "Draft" && hfVerDATE.Value == "False" && hfVerTYPE.Value == "0")
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
                INDIVIDUAL: ddlIndividual.SelectedValue
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
                        string URL = Session["prevURL"].ToString();
                        Response.Write("<script> alert('Document Uploaded successfully.');window.location='" + URL + "';</script>");
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
                    hfVerTYPE.Value = ds.Tables[0].Rows[0]["VerTYPE"].ToString();
                }
                else
                {
                    hfVerDATE.Value = "";
                    hfVerTYPE.Value = "";
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

                divCountry.Visible = true;
                divINVID.Visible = true;
                divIndividual.Visible = true;

                drpCountry.CssClass = drpCountry.CssClass.Replace("required", "");
                drpSites.CssClass = drpSites.CssClass.Replace("required", "");
                ddlIndividual.CssClass = ddlIndividual.CssClass.Replace("required", "");

                drpCountry.SelectedIndex = 0;
                drpSites.SelectedIndex = 0;
                ddlIndividual.SelectedIndex = 0;

                if (hfVerTYPE.Value == "Study")
                {
                    divCountry.Visible = false;
                    divINVID.Visible = false;
                    divIndividual.Visible = false;
                }
                else if (hfVerTYPE.Value == "Country")
                {
                    drpCountry.CssClass += " required";
                    divINVID.Visible = false;
                    divIndividual.Visible = false;
                }
                else if (hfVerTYPE.Value == "Site")
                {
                    drpCountry.CssClass += " required";
                    drpSites.CssClass += " required";
                    divIndividual.Visible = false;
                }
                else if (hfVerTYPE.Value == "Individual")
                {
                    ddlIndividual.CssClass += " required";
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
                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_WITHOUTDOC_Refrence",
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
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        REFRENCE_DOC: Request.QueryString["ID"].ToString()
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

                DataSet ds = dal.eTMF_SP
                    (
                    ACTION: "Upload_Documents_withDOC_ByDate_Refrence",
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
                    UploadFileName_Editable: UploadFileName_Editable,
                    Size_Editable: Size_Editable,
                    FileType_Editable: extension_Editable,
                    REFRENCE_DOC: Request.QueryString["ID"].ToString()
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

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
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

                DataSet ds = dal.eTMF_SP
                    (
                    ACTION: "Upload_Documents_withDOC_ByDate_Draft_Refrence",
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
                    UploadFileName_Editable: UploadFileName_Editable,
                    Size_Editable: Size_Editable,
                    FileType_Editable: extension_Editable,
                    REFRENCE_DOC: Request.QueryString["ID"].ToString()
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

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
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

        private void Upload_Documents_REPLACE(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string ReplaceFile, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_REPLACE_Refrence",
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
                        ID: ReplaceFile,
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        REFRENCE_DOC: Request.QueryString["ID"].ToString()
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

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
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

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_REPLACE_Draft_Refrence",
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
                        ID: ReplaceFile,
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        REFRENCE_DOC: Request.QueryString["ID"].ToString()
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

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
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

        private void Upload_Documents_Current(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_Current_Refrence",
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
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        REFRENCE_DOC: Request.QueryString["ID"].ToString()
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

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
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

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_Current_Draft_Refrence",
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
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        REFRENCE_DOC: Request.QueryString["ID"].ToString()
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

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
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

        private void Upload_Documents_withDOC(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string UploadFileName_Editable, string Size_Editable,
            string extension_Editable, string folderPath_Editable)
        {
            try
            {

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_withDOC_Refrence",
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
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        REFRENCE_DOC: Request.QueryString["ID"].ToString()
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

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
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

                DataSet ds = dal.eTMF_SP
                        (
                        ACTION: "Upload_Documents_withDOC_Draft_Refrence",
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
                        UploadFileName_Editable: UploadFileName_Editable,
                        Size_Editable: Size_Editable,
                        FileType_Editable: extension_Editable,
                        REFRENCE_DOC: Request.QueryString["ID"].ToString()
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

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
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
    }
}