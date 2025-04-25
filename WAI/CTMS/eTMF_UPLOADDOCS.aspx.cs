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
    public partial class eTMF_UPLOADDOCS : System.Web.UI.Page
    {
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        DAL dal_Common = new DAL();

        CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNote.Attributes.Add("MaxLength", "200");

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
                        BIND_TYPE();
                        BINDDEPARTMENT();
                        BIND_DOCUMENTTYPE();
                        GetCountry();
                        GetSite();
                        GetUsers();
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

        private void BIND_TYPE()
        {
            DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "TYPE_EVENT");
            string Event = ds.Tables[0].Rows[0]["Event"].ToString();
            if (Event != "0")
            {
                ddlAction.Items.Add(new ListItem("Event", "3"));
            }

            DataSet ds1 = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "TYPE_MILESTONE");
            string Milestone = ds1.Tables[0].Rows[0]["Milestone"].ToString();
            if (Milestone != "0")
            {
                ddlAction.Items.Add(new ListItem("Milestone", "4"));
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
                DataSet ds = new DataSet();

                if (ddlAction.SelectedItem.Text == "eTMF")
                {
                    ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_DocType");
                }
                else if (ddlAction.SelectedItem.Text == "Event")
                {
                    ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_DocType_Event");
                }
                else if (ddlAction.SelectedItem.Text == "Milestone")
                {
                    ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_DocType_Milestone");
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
                }

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
                if (ddlAction.SelectedValue == "2")
                {
                    DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_Folder", DOCTYPEID: drpDocType.SelectedValue);

                    ddlZone.DataSource = ds.Tables[0];
                    ddlZone.DataValueField = "ID";
                    ddlZone.DataTextField = "Folder";
                    ddlZone.DataBind();
                    ddlZone.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_Folder_Group", DOCTYPEID: drpDocType.SelectedValue);

                    ddlZone.DataSource = ds.Tables[0];
                    ddlZone.DataValueField = "ID";
                    ddlZone.DataTextField = "Folder";
                    ddlZone.DataBind();
                    ddlZone.Items.Insert(0, new ListItem("--Select--", "0"));
                }

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
                if (ddlAction.SelectedValue == "2")
                {

                    DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_Sub_Folder", FOLDERID: ddlZone.SelectedValue);

                    ddlSections.DataSource = ds.Tables[0];
                    ddlSections.DataValueField = "ID";
                    ddlSections.DataTextField = "Sub_Folder_Name";
                    ddlSections.DataBind();
                    ddlSections.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_Sub_Folder_GROUP", DOCTYPEID: drpDocType.SelectedValue, FOLDERID: ddlZone.SelectedValue);

                    ddlSections.DataSource = ds.Tables[0];
                    ddlSections.DataValueField = "ID";
                    ddlSections.DataTextField = "Sub_Folder_Name";
                    ddlSections.DataBind();
                    ddlSections.Items.Insert(0, new ListItem("--Select--", "0"));
                }

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
                if (ddlAction.SelectedValue == "2")
                {
                    DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_ARTIFACTS", SUBFOLDERID: ddlSections.SelectedValue);

                    ddlArtifacts.DataSource = ds.Tables[0];
                    ddlArtifacts.DataValueField = "ID";
                    ddlArtifacts.DataTextField = "Artifact_Name";
                    ddlArtifacts.DataBind();
                    ddlArtifacts.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_ARTIFACTS_GROUP", DOCTYPEID: drpDocType.SelectedValue, FOLDERID: ddlZone.SelectedValue, SUBFOLDERID: ddlSections.SelectedValue);

                    ddlArtifacts.DataSource = ds.Tables[0];
                    ddlArtifacts.DataValueField = "ID";
                    ddlArtifacts.DataTextField = "Artifact_Name";
                    ddlArtifacts.DataBind();
                    ddlArtifacts.Items.Insert(0, new ListItem("--Select--", "0"));
                }
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
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_eTMF_SPEC", DocID: drpDocument.SelectedValue);
                string Values = "";
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Values += "" + ds.Tables[0].Rows[i]["TEXT"].ToString() + ",";
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

                if (ddlAction.SelectedValue == "2" || ddlAction.SelectedValue == "3" || ddlAction.SelectedValue == "4")
                {
                    if (ddlAction.SelectedItem.Text == "eTMF")
                    {
                        lblViewType.Text = "Select TMF Model :";
                    }
                    else if (ddlAction.SelectedItem.Text == "Event")
                    {
                        lblViewType.Text = "Select" + " " + ddlAction.SelectedItem.Text + " :";
                    }
                    else if (ddlAction.SelectedItem.Text == "Milestone")
                    {
                        lblViewType.Text = "Select" + " " + ddlAction.SelectedItem.Text + " :";
                    }

                    divstatus.Visible = true;
                    divDefaultView.Visible = true;
                    divCountry.Visible = true;
                    divINVID.Visible = true;
                    divIndividual.Visible = true;
                    divfortask.Visible = false;
                    divforeTMF.Visible = true;
                    divdocument.Visible = true;
                    BIND_DOCUMENTTYPE();
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
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_DEPARTMENT");

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
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_TASK", ID: ddldepartment.SelectedValue);

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
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_SUB_TASK", UploadTaskId: ddlTask.SelectedValue);

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
                    ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GetDocument_List", UploadTaskId: ddlTask.SelectedValue, UploadSubTaskId: ddlSubTask.SelectedValue);
                }
                else if (divforeTMF.Visible == true)
                {
                    ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GetDocument_List_eTMF",
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
                DataSet ds = dal_Common.GET_COUNTRY_SP(USERID: Session["User_ID"].ToString());
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
                DataSet ds = dal_Common.GET_INVID_SP(COUNTRYID: drpCountry.SelectedValue, USERID: Session["User_ID"].ToString());
                drpSites.DataSource = ds.Tables[0];
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
                        if (CheckDocument_Exists() == "Not exist")
                        {
                            Upload();
                            // CLAER();
                            Response.Write("<script> alert('Document Uploaded successfully.');window.location='eTMF_UPLOADDOCS.aspx';</script>");
                        }
                        else
                        {
                            Response.Write("<script> alert('" + CheckDocument_Exists() + "')</script>");
                        }
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

        private string CheckDocument_Exists()
        {
            string result = "";
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(
                    ACTION: "CheckDocument_Exists",
                    UploadFileName: Path.GetFileName(FileUpload1.FileName).ToString(),
                    DOC_VERSIONNO: txtDovVersionNo.Text,
                    DOC_DATETIME: txtDocDateTime.Text
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "Unknown")
                    {
                        result = "Same document already uploaded as Unkonwn";
                    }
                    else
                    {
                        result = "Same document already exist in " + ds.Tables[0].Rows[0]["MSG"].ToString();
                    }
                }
                else
                {
                    result = "Not exist";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
            return result;
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
                            folderPath
                            );
                }
                else
                {
                    if ((hfVerTYPE.Value == "0" || hfVerTYPE.Value == "" || hfVerTYPE.Value == "None") && hfVerDATE.Value.ToUpper() == "FALSE" && hfVerSPEC.Value.ToUpper() == "FALSE")
                    {
                        if (ddlStatus3.SelectedValue == "Replace")
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
                                UPLOAD_DOCUMENT_REPLACE(
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
                                        ReplaceFile
                                    );
                            }
                            else
                            {
                                Response.Write("<script> alert('Please select file to Replace.')</script>");
                            }
                        }
                        else
                        {
                            UPLOAD_DOCUMENT_CURRENT(
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
                                    folderPath
                                );
                        }
                    }
                    else
                    {
                        UPLOAD_DOCUMENT(
                                UploadFileName,
                                Size,
                                extension,
                                DocTypeId,
                                ZoneId,
                                SectionId,
                                ArtifactId,
                                FUNCTIONS,
                                folderPath
                            );
                    }
                }

                string SUBJECT = "Protocol ID- " + Session["PROJECTIDTEXT"].ToString() + " :- eTMF Document Upload Alert.";
                string BODY = "Dear Team, <br/> " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " on " + comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm") + ".";
                comm.Email_Users(txtToEmailIds.Text + "," + hfOwnerEmailId.Value, txtCcEmailIds.Text, SUBJECT, BODY, "it@diagnosearch.com");

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
                if (drpAction.SelectedValue == "Current" && hfVerDATE.Value.ToUpper() == "FALSE" && hfVerSPEC.Value.ToUpper() == "FALSE" && hfVerTYPE.Value == "None")
                {
                    divStatus2.Visible = false;
                    divStatus3.Visible = true;
                    ddlFinalStatus.CssClass = ddlFinalStatus.CssClass.Replace("required", "");
                    ddlStatus3.CssClass += " required";
                    lblStatusAction.Visible = true;

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

                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_FILES_REPLACE",
                DocID: drpDocument.SelectedValue,
                Status: drpAction.SelectedValue,
                SubStatus: ddlFinalStatus.SelectedValue,
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue,
                INDIVIDUAL: txtIndividual.Text,
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
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(
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

                        Response.Write("<script> alert('Document Uploaded successfully.');window.location='eTMF_UPLOADDOCS.aspx';</script>");
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
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_VERSION_TYPE", DocID: drpDocument.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hfVerDATE.Value = ds.Tables[0].Rows[0]["VerDATE"].ToString();
                    hfVerSPEC.Value = ds.Tables[0].Rows[0]["VerSPEC"].ToString();
                    hfVerTYPE.Value = ds.Tables[0].Rows[0]["VerTYPE"].ToString();

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
                    lblRequiredDocDate.Visible = true;
                }
                else
                {
                    lblRequiredDocDate.Visible = false;
                    txtDocDateTime.CssClass = txtDocDateTime.CssClass.Replace("required", "");
                    lblRequiredDocDate.Visible = false;
                }

                BIND_SPEC();
                if (hfVerSPEC.Value == "True")
                {
                    divSPEC.Visible = true;
                    txtSPEC.CssClass += " required";
                    lblSpec.Visible = true;
                }
                else
                {
                    divSPEC.Visible = false;
                    txtSPEC.CssClass = txtSPEC.CssClass.Replace("required", "");
                    lblSpec.Visible = false;

                }

                divCountry.Visible = true;
                divINVID.Visible = true;
                divIndividual.Visible = true;

                drpCountry.CssClass = drpCountry.CssClass.Replace("required", "");
                lblCountry.Visible = false;

                drpSites.CssClass = drpSites.CssClass.Replace("required", "");
                lblSite.Visible = false;

                txtIndividual.CssClass = txtIndividual.CssClass.Replace("required", "");
                lblIndividual.Visible = false;

                txtSubject.CssClass = txtSubject.CssClass.Replace("required", "");
                lblSubject.Visible = false;

                drpCountry.SelectedIndex = 0;
                drpSites.SelectedIndex = 0;
                txtIndividual.Text = "";
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
                    lblCountry.Visible = true;
                    divINVID.Visible = false;
                    divIndividual.Visible = false;
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Site")
                {
                    drpCountry.CssClass += " required";
                    drpSites.CssClass += " required";
                    lblCountry.Visible = true;
                    lblSite.Visible = true;
                    divIndividual.Visible = false;
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Individual")
                {
                    txtIndividual.CssClass += " required";
                    lblIndividual.Visible = true;
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Subject")
                {
                    divIndividual.Visible = false;
                    divSubject.Visible = true;
                    txtSubject.CssClass += " required";
                    lblSubject.Visible = true;
                }
                else if (hfVerTYPE.Value == "None")
                {
                    if (gvFilesUploaded.Rows.Count > 0)
                    {
                        divstatus.Visible = true;
                        drpAction.SelectedValue = "Current";
                        drpAction.Enabled = false;
                        divStatus3.Visible = true;
                        drpAction_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                    else
                    {
                        drpAction.SelectedIndex = 0;
                        ddlFinalStatus.SelectedIndex = 0;
                        ddlStatus3.SelectedIndex = 0;
                        drpAction.Enabled = true;
                        divStatus3.Visible = false;
                    }


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

                if (hfVerTYPE.Value != "None")
                {
                    drpAction.SelectedIndex = 0;
                    ddlFinalStatus.SelectedIndex = 0;
                    ddlStatus3.SelectedIndex = 0;
                    drpAction.Enabled = true;
                }

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
                txtIndividual.Text = "";
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(
                ACTION: "GET_INDIVIDUAL",
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue
                );

                string Values = "";
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Values += "" + ds.Tables[0].Rows[i]["User_Name"].ToString() + ",";
                    }
                }
                hfIndividual.Value = Values.TrimEnd(',');

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindIndividual();", true);

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
            string ArtifactId, string FUNCTIONS, string folderPath)
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

                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP
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
                        RECEIPTDAT: txtReceiptdate.Text,
                        Functions: FUNCTIONS,
                        INDIVIDUAL: txtIndividual.Text,
                        SUBJID: txtSubject.Text,
                        ToEmailIDs: txtToEmailIds.Text,
                        CCEmailIDs: txtCcEmailIds.Text,
                        SendEmail: SendEmail
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal_eTMF.eTMF_UPLOAD_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPLOAD_DOCUMENT_REPLACE(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath, string ReplaceFile)
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

                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP
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
                            RECEIPTDAT: txtReceiptdate.Text,
                            Functions: FUNCTIONS,
                            INDIVIDUAL: txtIndividual.Text,
                            SUBJID: txtSubject.Text,
                            ID: ReplaceFile,
                            ToEmailIDs: txtToEmailIds.Text,
                            CCEmailIDs: txtCcEmailIds.Text,
                            SendEmail: SendEmail
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal_eTMF.eTMF_UPLOAD_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPLOAD_DOCUMENT(string UploadFileName, string Size, string extension, string DocTypeId, string ZoneId, string SectionId, string ArtifactId, string FUNCTIONS, string folderPath)
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

                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP
                        (
                            ACTION: "UPLOAD_DOCUMENT",
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
                            DeadlineDate: txtDeadline.Text,
                            UploadDocTypeid: DocTypeId,
                            UploadZoneId: ZoneId,
                            UploadSectionId: SectionId,
                            UploadArtifactId: ArtifactId,
                            DOC_VERSIONNO: txtDovVersionNo.Text,
                            DOC_DATETIME: txtDocDateTime.Text,
                            NOTE: txtNote.Text,
                            RECEIPTDAT: txtReceiptdate.Text,
                            Functions: FUNCTIONS,
                            INDIVIDUAL: txtIndividual.Text,
                            SUBJID: txtSubject.Text,
                            ToEmailIDs: txtToEmailIds.Text,
                            CCEmailIDs: txtCcEmailIds.Text,
                            SendEmail: SendEmail,
                            SPEC: txtSPEC.Text
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal_eTMF.eTMF_UPLOAD_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPLOAD_DOCUMENT_CURRENT(string UploadFileName, string Size, string extension,
            string DepartmentId, string TaskId, string SubTaskId, string DocTypeId, string ZoneId, string SectionId,
            string ArtifactId, string FUNCTIONS, string folderPath)
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

                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP
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
                        RECEIPTDAT: txtReceiptdate.Text,
                        Functions: FUNCTIONS,
                        INDIVIDUAL: txtIndividual.Text,
                        SUBJID: txtSubject.Text,
                        ToEmailIDs: txtToEmailIds.Text,
                        CCEmailIDs: txtCcEmailIds.Text,
                        SendEmail: SendEmail
                        );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal_eTMF.eTMF_UPLOAD_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

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
                            dal_eTMF.eTMF_UPLOAD_SP(ACTION: "Insert_Status_Users", Status: drpAction.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name);

                            string BODY = "Dear " + User_Name + ", " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " with " + drpAction.SelectedValue + " mode and assigned to you.";
                            comm.Email_Users(EMAILID, null, "eTMF: Document Upload Alert", BODY, "it@diagnosearch.com");
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
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "Get_Files", DocID: drpDocument.SelectedValue);

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
                    Label lbl_UploadFileName = (Label)e.Row.FindControl("lbl_UploadFileName");
                    LinkButton lbtn_UploadFileName = (LinkButton)e.Row.FindControl("lbtn_UploadFileName");
                    string UnblindDoc = drv["Unblind"].ToString();

                    if (UnblindDoc == "Unblinded")
                    {
                        if (Session["Unblind"].ToString() == "Unblinded")
                        {
                            lbl_UploadFileName.Visible = false;
                            lbtn_UploadFileName.Visible = true;
                            lbtnDownloadDoc.Visible = true;
                        }
                        else
                        {
                            lbtn_UploadFileName.Visible = false;
                            lbl_UploadFileName.Visible = true;
                            lbtnDownloadDoc.Visible = false;
                        }
                    }
                    else
                    {
                        lbl_UploadFileName.Visible = false;
                        lbtn_UploadFileName.Visible = true;
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
                    DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_VERSION_HISTORY_COUNT", ID: ID);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["COUNTS"].ToString() != "0")
                        {
                            iconhistory.Attributes.Add("style", "color: red;");
                        }
                    }

                    HtmlControl iconQC = (HtmlControl)e.Row.FindControl("iconQC");

                    if (drv["QC"].ToString() == "True")
                    {
                        iconQC.Attributes.Add("class", "fa fa-check");
                        iconQC.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconQC.Attributes.Add("class", "fa fa-times");
                        iconQC.Attributes.Add("style", "color: red;");
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
                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "Download_File", ID: ID);

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