using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_Deleted_Docs : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
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
                        Session["prevURL"] = Request.Url.PathAndQuery.ToString();

                        if (Request.QueryString["Status"] != null)
                        {
                            drpStatus.SelectedValue = Request.QueryString["Status"].ToString();
                        }
                        BIND_TYPE();
                        BIND_DOCUMENTTYPE();
                        BIND_Zone();
                        BIND_Sections();
                        BIND_Artifacts();
                        BIND_DOCUMENTS();
                        GET_COUNTRY();
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
                ddlAction.Items.Add(new ListItem("Event", "2"));
            }

            DataSet ds1 = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "TYPE_MILESTONE");
            string Milestone = ds1.Tables[0].Rows[0]["Milestone"].ToString();
            if (Milestone != "0")
            {
                ddlAction.Items.Add(new ListItem("Milestone", "3"));
            }
        }

        private void BIND_DOCUMENTTYPE()
        {
            try
            {
                if (ddlAction.SelectedItem.Text == "eTMF")
                {
                    lblViewType.Text = "Select TMF Model :";
                    DataSet ds = dal.eTMF_UPLOAD_SP(ACTION: "get_DocType");
                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (ddlAction.SelectedItem.Text == "Event")
                {
                    lblViewType.Text = "Select" + " " + ddlAction.SelectedItem.Text + " :";
                    DataSet ds = dal.eTMF_UPLOAD_SP(ACTION: "get_DocType_Event");

                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (ddlAction.SelectedItem.Text == "Milestone")
                {
                    lblViewType.Text = "Select" + " " + ddlAction.SelectedItem.Text + " :";
                    DataSet ds = dal.eTMF_UPLOAD_SP(ACTION: "get_DocType_Milestone");

                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    lblViewType.Text = "Select :";
                    drpDocType.DataSource = null;
                    drpDocType.DataBind();
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
                if (ddlAction.SelectedItem.Text == "eTMF")
                {
                    DataSet ds = dal.eTMF_UPLOAD_SP(ACTION: "get_Folder", DOCTYPEID: drpDocType.SelectedValue);

                    ddlZone.DataSource = ds.Tables[0];
                    ddlZone.DataValueField = "ID";
                    ddlZone.DataTextField = "Folder";
                    ddlZone.DataBind();
                    ddlZone.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DataSet ds = dal.eTMF_UPLOAD_SP(ACTION: "get_Folder_Group", DOCTYPEID: drpDocType.SelectedValue);

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
                if (ddlAction.SelectedItem.Text == "eTMF")
                {
                    DataSet ds = dal.eTMF_UPLOAD_SP(ACTION: "get_Sub_Folder", FOLDERID: ddlZone.SelectedValue);

                    ddlSections.DataSource = ds.Tables[0];
                    ddlSections.DataValueField = "ID";
                    ddlSections.DataTextField = "Sub_Folder_Name";
                    ddlSections.DataBind();
                    ddlSections.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DataSet ds = dal.eTMF_UPLOAD_SP(ACTION: "get_Sub_Folder_GROUP", DOCTYPEID: drpDocType.SelectedValue, FOLDERID: ddlZone.SelectedValue);

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
                if (ddlAction.SelectedItem.Text == "eTMF")
                {
                    DataSet ds = dal.eTMF_UPLOAD_SP(ACTION: "get_ARTIFACTS", SUBFOLDERID: ddlSections.SelectedValue);

                    ddlArtifacts.DataSource = ds.Tables[0];
                    ddlArtifacts.DataValueField = "ID";
                    ddlArtifacts.DataTextField = "Artifact_Name";
                    ddlArtifacts.DataBind();
                    ddlArtifacts.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DataSet ds = dal.eTMF_UPLOAD_SP(ACTION: "get_ARTIFACTS_GROUP", DOCTYPEID: drpDocType.SelectedValue, FOLDERID: ddlZone.SelectedValue, SUBFOLDERID: ddlSections.SelectedValue);

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

        protected void BIND_DOCUMENTS()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal.eTMF_UPLOAD_SP(ACTION: "GetDocument_List_eTMF",
                UploadZoneId: ddlZone.SelectedValue,
                UploadSectionId: ddlSections.SelectedValue,
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

        protected void drpDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_Zone();
                BIND_Sections();
                BIND_Artifacts();
                BIND_DOCUMENTS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_Sections();
                BIND_Artifacts();
                BIND_DOCUMENTS();
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
                BIND_DOCUMENTS();
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

        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "Download_File", ID: e.CommandArgument.ToString());

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
                else
                {

                }
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
                    string ID = drv["ID"].ToString();
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

                    HtmlControl iconhistory = (HtmlControl)e.Row.FindControl("iconhistory");

                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "GET_VERSION_HISTORY_COUNT", ID: ID);

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

                    string UnblindDoc = drv["Unblind"].ToString();
                    LinkButton lbtnDownloadDoc = (LinkButton)e.Row.FindControl("lbtnDownloadDoc");

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
                }
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
                BIND_DOCUMENTTYPE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnShowFiles_Click(object sender, EventArgs e)
        {
            try
            {
                Get_Files();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Files()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_REPORT_SP(
                    ACTION: "GET_DELETED_DOCS",
                    ZONEID: ddlZone.SelectedValue,
                    SECTIONID: ddlSections.SelectedValue,
                    ARTIFACTID: ddlArtifacts.SelectedValue,
                    DocID: drpDocument.SelectedValue,
                    Status: drpStatus.SelectedValue,
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpInvID.SelectedValue,
                    DocType: drpDocType.SelectedValue,
                    GroupID: ddlAction.SelectedItem.Text
                    );

                gvFiles.DataSource = ds;
                gvFiles.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_REPORT_SP(
                    ACTION: "GET_DELETED_DOCS_Export",
                    ZONEID: ddlZone.SelectedValue,
                    SECTIONID: ddlSections.SelectedValue,
                    ARTIFACTID: ddlArtifacts.SelectedValue,
                    DocID: drpDocument.SelectedValue,
                    Status: drpStatus.SelectedValue,
                    DocType: drpDocType.SelectedValue,
                    GroupID: ddlAction.SelectedItem.Text
                    );

                ds.Tables[0].TableName = lblHeader.Text;

                var LinkValuePairs = new (string Value, string Link)[]
                {
                ("File Name", "FILENAME_Link")
                };

                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_REPORT_SP(
                    ACTION: "GET_DELETED_DOCS_Export",
                    ZONEID: ddlZone.SelectedValue,
                    SECTIONID: ddlSections.SelectedValue,
                    ARTIFACTID: ddlArtifacts.SelectedValue,
                    DocID: drpDocument.SelectedValue,
                    Status: drpStatus.SelectedValue,
                    DocType: drpDocType.SelectedValue,
                    GroupID: ddlAction.SelectedItem.Text
                    );

                ds.Tables[0].TableName = lblHeader.Text;

                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP();
                drpCountry.DataSource = ds.Tables[0];
                drpCountry.DataTextField = "COUNTRYNAME";
                drpCountry.DataValueField = "COUNTRYID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_INVID()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(COUNTRYID: drpCountry.SelectedValue);
                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataTextField = "INVID";
                drpInvID.DataValueField = "INVID";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            GET_INVID();
        }
    }
}