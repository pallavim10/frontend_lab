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

namespace CTMS
{
    public partial class eTMF_UploadDocument : System.Web.UI.Page
    {
        DAL dal = new DAL();

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
                    if (!IsPostBack)
                    {
                        GetCountry();
                        GetSite();
                        BIND_Functions();
                        GetUsers();

                        if (Request.QueryString["INVID"].ToString() == "0")
                        {
                            divINVID.Visible = false;
                        }
                        else
                        {
                            divINVID.Visible = true;

                            DataSet ds = dal.eTMF_SP(ACTION: "GetCountryForSite", SiteID: Request.QueryString["INVID"].ToString());

                            drpCountry.SelectedValue = ds.Tables[0].Rows[0]["COUNTRYID"].ToString();

                            GetSite();

                            drpSites.SelectedValue = Request.QueryString["INVID"].ToString();
                        }

                        GetTaskSubtask();
                        GetDocuments();

                        if (Request.QueryString["ID"] != null)
                        {
                            hfID.Value = Request.QueryString["ID"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetTaskSubtask()
        {
            try
            {
                lbl1.Text = "Task";
                lbl2.Text = "Sub-Task";

                lbl_Task_ID.Text = Request.QueryString["TaskID"].ToString();
                lbl_Sub_Task_ID.Text = Request.QueryString["SubTaskID"].ToString();

                DataSet ds = dal.Budget_SP(Action: "single_Task", Task_ID: lbl_Task_ID.Text);
                lbl_Task.Text = ds.Tables[0].Rows[0]["Task_Name"].ToString();

                DataSet ds1 = dal.Budget_SP(Action: "single_SubTask", Task_ID: lbl_Task_ID.Text, Sub_Task_ID: lbl_Sub_Task_ID.Text);
                lbl_Sub_Task.Text = ds1.Tables[0].Rows[0]["Sub_Task_Name"].ToString();
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
                DataSet ds = dal.eTMF_SP(ACTION: "GetDocument_List", UploadTaskId: lbl_Task_ID.Text, UploadSubTaskId: lbl_Sub_Task_ID.Text);

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

                if (drpCountry.SelectedIndex != 0)
                {
                    divINVID.Visible = true;
                }
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
                        Session["FileUpload1_Upload_Doc_CTMS"] = FileUpload1;

                        string UploadFileName = Path.GetFileName(FileUpload1.FileName);

                        DataSet ds = dal.eTMF_SP(ACTION: "CHECK_FILE_EXISTS_OR_NOT", UploadFileName: UploadFileName);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmMsg();", true);
                        }
                        else
                        {
                            Upload();
                            Response.Write("<script> alert('Document Uploaded successfully.')</script>");
                            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
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

                System.IO.FileInfo fi = new System.IO.FileInfo(FileUpload1.PostedFile.FileName);
                DateTime createtime = fi.CreationTime;
                DateTime modifytime = fi.LastWriteTime;
                DateTime accesstime = fi.LastAccessTime;

                DataSet ds = dal.eTMF_SP
                    (
                    ACTION: "Upload_Documents_Task",
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
                    UploadTaskId: lbl_Task_ID.Text,
                    UploadSubTaskId: lbl_Sub_Task_ID.Text,
                    DeadlineDate: txtDeadline.Text,
                    DOC_VERSIONNO: txtDovVersionNo.Text,
                    DOC_DATETIME: txtDocDateTime.Text,
                    NOTE: txtNote.Text,
                    ID: hfID.Value,
                    Functions: ddlFunction.SelectedValue
                    );

                string SysFileName = ds.Tables[0].Rows[0][0].ToString() + Path.GetExtension(FileUpload1.FileName);

                FileUpload1.SaveAs(folderPath + SysFileName);

                dal.eTMF_SP(ACTION: "UpdateSysFileName", ID: ds.Tables[0].Rows[0][0].ToString(), SysFileName: SysFileName);

                if (drpAction.SelectedValue == "Collaborate" || drpAction.SelectedValue == "Review" || drpAction.SelectedValue == "QC" || drpAction.SelectedValue == "QA Review" || drpAction.SelectedValue == "Internal Approval" || drpAction.SelectedValue == "Sponsor Approval")
                {
                    for (int j = 0; j < grd_Users.Rows.Count; j++)
                    {
                        CheckBox chkSelect = (CheckBox)grd_Users.Rows[j].FindControl("chkSelect");
                        string User_Name = ((Label)grd_Users.Rows[j].FindControl("User_Name")).Text;
                        string User_ID = ((Label)grd_Users.Rows[j].FindControl("User_ID")).Text;

                        if (chkSelect.Checked)
                        {
                            dal.eTMF_SP(ACTION: "Insert_Status_Users", Status: drpAction.SelectedValue, DocID: ds.Tables[0].Rows[0][0].ToString(), UploadBy: User_ID, DocName: User_Name, DeadlineDate: txtDeadline.Text);
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

        private void GetFiles()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "Get_Files", DocID: drpDocument.SelectedValue);
                gvFiles.DataSource = ds;
                gvFiles.DataBind();

                if (gvFiles.Rows.Count > 0)
                {
                    divUploaded.Visible = true;
                    lblUploaded.Text = "Total " + gvFiles.Rows.Count + " Document(s) are already uploaded.";
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
                DataSet ds = dal.eTMF_SP(ACTION: "GetUsers");
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
                }
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
                ddlFunction.Items.Insert(0, new ListItem("None", "0"));
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
                FileUpload1 = (FileUpload)Session["FileUpload1_Upload_Doc_CTMS"];

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