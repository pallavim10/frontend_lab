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
    public partial class ETMF_View_Docs : System.Web.UI.Page
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
                        BIND_TYPE();
                        GET_COUNTRY();
                        //BIND_ZONES();
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
            try
            {
                DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "TYPE_EVENT");
                string Event = ds.Tables[0].Rows[0]["Event"].ToString();
                if (Event != "0")
                {
                    ddlViewby.Items.Add(new ListItem("Event", "2"));
                }

                DataSet ds1 = dal_eTMF.eTMF_DATA_SP(ACTION: "TYPE_MILESTONE");
                string Milestone = ds1.Tables[0].Rows[0]["Milestone"].ToString();
                if (Milestone != "0")
                {
                    ddlViewby.Items.Add(new ListItem("Milestone", "3"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_ZONES()
        {
            try
            {
                DataSet ds = new DataSet();

                if (ddlViewby.SelectedItem.Text == "eTMF")
                {
                    ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_Folder", DocTypeId: drpDocType.SelectedValue);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlZone.DataSource = ds.Tables[0];
                        ddlZone.DataValueField = "ID";
                        ddlZone.DataTextField = "Folder";
                        ddlZone.DataBind();
                        ddlZone.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                    else
                    {
                        ddlZone.DataSource = null;
                        ddlZone.DataBind();
                    }
                }
                else
                {
                    ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_Folder_Group", DocTypeId: drpDocType.SelectedValue);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlZone.DataSource = ds.Tables[0];
                        ddlZone.DataValueField = "ID";
                        ddlZone.DataTextField = "Folder";
                        ddlZone.DataBind();
                        ddlZone.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                    else
                    {
                        ddlZone.DataSource = null;
                        ddlZone.DataBind();
                    }
                }


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
                BIND_SECTIONS();

                //BIND_ARTIFACT(DocID;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_SECTIONS()
        {
            try
            {
                DataSet ds = new DataSet();
                if (ddlViewby.SelectedItem.Text == "eTMF")
                {
                    ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_SubFolder",
                    ID: ddlZone.SelectedValue
                    );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSections.DataSource = ds.Tables[0];
                        ddlSections.DataValueField = "SubFolder_ID";
                        ddlSections.DataTextField = "Sub_Folder_Name";
                        ddlSections.DataBind();
                        ddlSections.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                    else
                    {
                        ddlSections.DataSource = null;
                        ddlSections.DataBind();
                    }
                }
                else
                {
                    ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_SubFolder_Group",
                    ID: ddlZone.SelectedValue,
                    DocTypeId: drpDocType.SelectedValue
                    );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSections.DataSource = ds.Tables[0];
                        ddlSections.DataValueField = "SubFolder_ID";
                        ddlSections.DataTextField = "Sub_Folder_Name";
                        ddlSections.DataBind();
                        ddlSections.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                    else
                    {
                        ddlSections.DataSource = null;
                        ddlSections.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_ARTIFACT(string UploadFileName)
        {
            try
            {
                DataSet ds = new DataSet();

                if (ddlViewby.SelectedItem.Text == "eTMF")
                {
                    ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_ProjectArtifacts",
                        SubFolder_ID: ddlSections.SelectedValue,
                        Folder_ID: ddlZone.SelectedValue,
                        User: Session["User_ID"].ToString(),
                        UploadFileName: UploadFileName,
                        CountryID: drpCountry.SelectedValue,
                        SiteID: drpInvID.SelectedValue
                    );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvArtifact.DataSource = ds.Tables[0];
                        gvArtifact.DataBind();

                        lblCount.Text = SubFolderDocs.ToString();

                        if (ddlZone.SelectedValue != "0" && ddlZone.SelectedValue != "")
                        {
                            gvArtifact.Columns[4].Visible = false;
                        }
                        else
                        {
                            gvArtifact.Columns[4].Visible = true;
                        }

                        if (ddlSections.SelectedValue != "0" && ddlSections.SelectedValue != "")
                        {
                            gvArtifact.Columns[5].Visible = false;
                        }
                        else
                        {
                            gvArtifact.Columns[5].Visible = true;
                        }
                    }
                    else
                    {
                        gvArtifact.DataSource = null;
                        gvArtifact.DataBind();

                        lblCount.Text = "0";
                    }
                }
                else
                {
                    ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_ProjectArtifacts_Group",
                        SubFolder_ID: ddlSections.SelectedValue,
                        Folder_ID: ddlZone.SelectedValue,
                        DocTypeId: drpDocType.SelectedValue,
                        User: Session["User_ID"].ToString()
                    );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvArtifact.DataSource = ds.Tables[0];
                        gvArtifact.DataBind();

                        lblCount.Text = SubFolderDocs.ToString();

                        if (ddlZone.SelectedValue != "0" && ddlZone.SelectedValue != "")
                        {
                            gvArtifact.Columns[4].Visible = false;
                        }
                        else
                        {
                            gvArtifact.Columns[4].Visible = true;
                        }

                        if (ddlSections.SelectedValue != "0" && ddlSections.SelectedValue != "")
                        {
                            gvArtifact.Columns[5].Visible = false;
                        }
                        else
                        {
                            gvArtifact.Columns[5].Visible = true;
                        }
                    }
                    else
                    {
                        gvArtifact.DataSource = null;
                        gvArtifact.DataBind();

                        lblCount.Text = "0";
                    }
                }
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

        int FolderDocs = 0;
        int SubFolderDocs = 0;
        int ArtifactDocs = 0;

        protected void gvFolder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    FolderDocs = 0;

                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Folder_ID = drv["Folder_ID"].ToString();
                    DataSet ds = new DataSet();
                    GridView gvSubFolder = e.Row.FindControl("gvSubFolder") as GridView;
                    if (ddlViewby.SelectedItem.Text == "eTMF")
                    {
                        ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_SubFolder",
                        ID: ddlZone.SelectedValue,
                        Project_ID: Session["PROJECTID"].ToString()
                        );
                    }
                    else
                    {
                        ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_SubFolder_Group",
                                            ID: ddlZone.SelectedValue,
                                            DocTypeId: drpDocType.SelectedValue
                                            );
                    }


                    gvSubFolder.DataSource = ds.Tables[0];
                    gvSubFolder.DataBind();

                    Label lblCount = e.Row.FindControl("lblCount") as Label;
                    lblCount.Text = FolderDocs.ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvSubFolder.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvSubFolder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    SubFolderDocs = 0;

                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Folder_ID = drv["Folder_ID"].ToString();
                    string SubFolder_ID = drv["SubFolder_ID"].ToString();

                    GridView gvArtifact = e.Row.FindControl("gvArtifact") as GridView;
                    DataSet ds = new DataSet();
                    if (ddlViewby.SelectedItem.Text == "eTMF")
                    {
                        ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_ProjectArtifacts", SubFolder_ID: SubFolder_ID, Folder_ID: Folder_ID, Project_ID: Session["PROJECTID"].ToString(), User: Session["User_ID"].ToString());
                    }
                    else
                    {
                        ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_ProjectArtifacts_Group",
                            SubFolder_ID: ddlSections.SelectedValue,
                            Folder_ID: ddlZone.SelectedValue,
                            DocTypeId: drpDocType.SelectedValue,
                            User: Session["User_ID"].ToString()
                    );
                    }
                    gvArtifact.DataSource = ds.Tables[0];
                    gvArtifact.DataBind();

                    Label lblCount = e.Row.FindControl("lblCount") as Label;
                    lblCount.Text = SubFolderDocs.ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvArtifact.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvArtifact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ArtifactDocs = 0;

                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ArtifactID = drv["ID"].ToString();
                    string DocID = drv["DocTypeId"].ToString();

                    GridView gvDocs = e.Row.FindControl("gvDocs") as GridView;
                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_BD_Docs_FileStructure", ARTIFACTS: ArtifactID, UploadFileName: txtDocumentName.Text.Trim(), CountryID: drpCountry.SelectedValue,
                        SiteID: drpInvID.SelectedValue);
                    gvDocs.DataSource = ds.Tables[0];
                    gvDocs.DataBind();

                    Label lblCount = e.Row.FindControl("lblCount") as Label;
                    lblCount.Text = ArtifactDocs.ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvDocs.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();

                    GridView gvFiles = e.Row.FindControl("gvFiles") as GridView;
                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "Get_Files", DocID: ID, UploadFileName: txtDocumentName.Text.Trim(), CountryID: drpCountry.SelectedValue,
                        SiteID: drpInvID.SelectedValue);
                    gvFiles.DataSource = ds;
                    gvFiles.DataBind();

                    Label lblCount = e.Row.FindControl("lblCount") as Label;
                    lblCount.Text = gvFiles.Rows.Count.ToString();

                    FolderDocs += gvFiles.Rows.Count;
                    SubFolderDocs += gvFiles.Rows.Count;
                    ArtifactDocs += gvFiles.Rows.Count;

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvFiles.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Download")
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
                }
                else if (e.CommandName == "Download_OtherType_File")
                {
                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "Download_OtherType_File", ID: e.CommandArgument.ToString());

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //File to be downloaded.
                        string fileName = ds.Tables[0].Rows[0]["SysFileName_Editable"].ToString();

                        //Set the New File name.
                        string newFileName = ds.Tables[0].Rows[0]["UploadFileName_Editable"].ToString();


                        //Path of the File to be downloaded.
                        string filePath = Server.MapPath(string.Format("~/eTMF_Editable/{0}", fileName));

                        //Setting the Content Type, Header and the new File name.
                        Response.ContentType = ds.Tables[0].Rows[0]["FileType_Editable"].ToString();
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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.Parent.Parent;
                int idx = row.RowIndex;

                DropDownList drpAction = (DropDownList)row.FindControl("drpAction");

                Label ID = (Label)row.FindControl("ID");

                dal_eTMF.eTMF_DATA_SP(ACTION: "ChnageStatus", ID: ID.Text, Status: drpAction.SelectedValue);
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
                    string ID = drv["ID"].ToString();
                    Label lbtnFileType = (Label)e.Row.FindControl("lbtnFileType");
                    HtmlControl ICON = (HtmlControl)e.Row.FindControl("ICONCLASS");
                    Label lbl_UploadFileName = (Label)e.Row.FindControl("lbl_UploadFileName");
                    LinkButton lbtn_UploadFileName = (LinkButton)e.Row.FindControl("lbtn_UploadFileName");
                    LinkButton lbtnDownloadDoc = (LinkButton)e.Row.FindControl("lbtnDownloadDoc");

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
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            BIND_ZONES();
        }

        protected void drpDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_ZONES();

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

                if (ddlViewby.SelectedItem.Text == "eTMF")
                {
                    lblViewType.Text = "Select TMF Model :";
                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_DocType");
                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else if (ddlViewby.SelectedItem.Text == "Event")
                {
                    lblViewType.Text = "Select" + " " + ddlViewby.SelectedItem.Text + " :";
                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_DocType_Event");

                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else if (ddlViewby.SelectedItem.Text == "Milestone")
                {
                    lblViewType.Text = "Select" + " " + ddlViewby.SelectedItem.Text + " :";
                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_DocType_Milestone");

                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    lblViewType.Text = "Select :";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void ddlViewby_SelectedIndexChanged(object sender, EventArgs e)
        {
            BIND_DOCUMENTTYPE();
        }

        protected void btnShowData_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtDocumentName.Text.Trim() != "") || (txtDocumentName.Text.Trim() == ""))
                {
                    string UploadedFileName = txtDocumentName.Text.Trim();
                    BIND_ARTIFACT(UploadedFileName);
                    divTCount.Visible = true;

                }
                //else
                //{
                //    Response.Write("<script>alert('Please enter document name.')</script>");
                //}
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
