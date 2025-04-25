using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_UploadDocument_Doc : System.Web.UI.Page
    {
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();
        DAL dal_Common = new DAL();

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
                    if (!IsPostBack)
                    {
                        GetDocuments();
                        GetFiles();
                        GetCountry();
                        GetSite();
                        GetUsers();
                        GET_INDIVIDUAL();
                        CHECK_VERSION_TYPE();
                        CHECK_DIVS();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetDocuments()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "SelectDocument", ID: Request.QueryString["DocID"].ToString());

                //if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)

                lblDocumentName.Text = ds.Tables[0].Rows[0]["DocName"].ToString();
                hfDocumentID.Value = Request.QueryString["DocID"].ToString();

                hfDocTypeId.Value = ds.Tables[0].Rows[0]["DocTypeId"].ToString();
                hfZoneId.Value = ds.Tables[0].Rows[0]["ZoneId"].ToString();
                hfSectionId.Value = ds.Tables[0].Rows[0]["SectionId"].ToString();
                hfArtifactId.Value = ds.Tables[0].Rows[0]["ArtifactID"].ToString();

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

        protected void BIND_SPEC()
        {
            try
            {
                txtSPEC.Text = "";
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_eTMF_SPEC", DocID: Request.QueryString["DocID"].ToString());
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
                    if (CheckDocument_Exists() == "Not exist")
                    {
                        Upload();
                        // CLAER();
                        Response.Write("<script> alert('Document Uploaded successfully.')</script>");
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                    }
                    else
                    {
                        Response.Write("<script> alert('" + CheckDocument_Exists() + "')</script>");
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

                string
                    DocTypeId = hfDocTypeId.Value,
                    ZoneId = hfZoneId.Value,
                    SectionId = hfSectionId.Value,
                    ArtifactId = hfArtifactId.Value,
                    FUNCTIONS = "";

                if (hfDocumentID.Value == "")
                {
                    Upload_Documents_WITHOUTDOC(
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
                else
                {
                    if ((hfVerTYPE.Value == "0" || hfVerTYPE.Value == "" || hfVerTYPE.Value == "None") && hfVerDATE.Value.ToUpper() == "FALSE" && hfVerSPEC.Value.ToUpper() == "FALSE")
                    {
                        if (ddlStatus3.SelectedValue == "Replace")
                        {
                            bool isFileSelected = false;
                            string ReplaceFile = "";

                            for (int i = 0; i < gvFilesUploaded.Rows.Count; i++)
                            {
                                if (!isFileSelected)
                                {
                                    CheckBox chkSelect = (CheckBox)gvFilesUploaded.Rows[i].FindControl("chkSelect");
                                    if (chkSelect.Checked)
                                    {
                                        isFileSelected = true;

                                        Label ID = (Label)gvFilesUploaded.Rows[i].FindControl("ID");
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

                CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();
                string SUBJECT = "Protocol ID- " + Session["PROJECTIDTEXT"].ToString() + " :- eTMF Document Upload Alert.";
                string BODY = "Dear Team, <br/> " + FileUpload1.FileName + " is uploaded by " + Session["User_Name"].ToString() + " on " + commFun.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm") + ".";
                comm.Email_Users(txtToEmailIds.Text + "," + hfOwnerEmailId.Value, txtCcEmailIds.Text, SUBJECT, BODY, "it@diagnosearch.com");

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

        private void GetFiles()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "Get_Files", DocID: hfDocumentID.Value);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvFilesUploaded.DataSource = ds;
                    gvFilesUploaded.DataBind();

                    divUploaded.Visible = true;
                    lblUploaded.Text = "Total " + gvFilesUploaded.Rows.Count + " Document(s) are already uploaded.";
                }
                else
                {
                    divUploaded.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                lblErrorMsg.Text = ex.Message.ToString();
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
                else if (drpAction.SelectedValue == "Current" && hfVerDATE.Value == "False" && hfVerTYPE.Value == "0")
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

                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_FILES_REPLACE",
                DocID: hfDocumentID.Value,
                Status: drpAction.SelectedValue,
                SubStatus: ddlFinalStatus.SelectedValue,
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue,
                INDIVIDUAL: txtIndividual.Text
                );

                gvFilesReplace.DataSource = ds;
                gvFilesReplace.DataBind();

                if (gvFilesReplace.Rows.Count == 0)
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

        protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
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
                }
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
                FileUpload1 = (FileUpload)Session["FileUpload1_Upload_Doc"];

                if (FileUpload1.HasFile)
                {
                    try
                    {
                        Upload();

                        Response.Write("<script> alert('Document Uploaded successfully.')</script>");
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
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

        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
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

        private void CHECK_VERSION_TYPE()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "GET_VERSION_TYPE", DocID: hfDocumentID.Value);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
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

                    hfVerDATE.Value = ds.Tables[0].Rows[0]["VerDATE"].ToString();
                    hfVerSPEC.Value = ds.Tables[0].Rows[0]["VerSPEC"].ToString();
                    hfVerTYPE.Value = ds.Tables[0].Rows[0]["VerTYPE"].ToString();
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

                drpCountry.SelectedIndex = 0;
                drpSites.SelectedIndex = 0;
                txtIndividual.Text = "";
                txtSubject.Text = "";

                drpCountry.CssClass = drpCountry.CssClass.Replace("required", "");
                drpSites.CssClass = drpSites.CssClass.Replace("required", "");
                txtIndividual.CssClass = txtIndividual.CssClass.Replace("required", "");
                txtSubject.CssClass = txtSubject.CssClass.Replace("required", "");

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
                    txtIndividual.CssClass += " required";
                    divIndividual.Visible = true;
                    divSubject.Visible = false;
                }
                else if (hfVerTYPE.Value == "Subject")
                {
                    divIndividual.Visible = false;
                    divSubject.Visible = true;
                    txtSubject.CssClass += " required";
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
            string DocTypeId, string ZoneId, string SectionId,
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
                        DocID: hfDocumentID.Value,
                        DocName: lblDocumentName.Text,
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
            string DocTypeId, string ZoneId, string SectionId,
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
                            DocID: hfDocumentID.Value,
                            DocName: lblDocumentName.Text,
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
                            DocID: hfDocumentID.Value,
                            DocName: lblDocumentName.Text,
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
            string DocTypeId, string ZoneId, string SectionId,
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
                        DocID: hfDocumentID.Value,
                        DocName: lblDocumentName.Text,
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