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
    public partial class eTMF_Snapshot_SITE : System.Web.UI.Page
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

                        lblHeader.Text = Request.QueryString["SNAPNAME"].ToString().Replace("'", "");

                        BIND_ZONES();

                        GET_FILES_LIST();

                        ShowAs();
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

        private void BIND_ZONES()
        {
            try
            {
                DataSet ds = new DataSet();


                if (Request.QueryString["SNAPID"] != null)
                {
                    ds = dal.eTMF_Snapshot_SP(ACTION: "GET_ZONES", SnapId: Request.QueryString["SNAPID"].ToString());
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvFolder.DataSource = ds.Tables[0];
                    gvFolder.DataBind();
                }
                else
                {
                    gvFolder.DataSource = null;
                    gvFolder.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DownloadFile(string ID)
        {
            try
            {
                byte[] bytes;
                string fileName, contentType;

                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "Download", ID: ID);

                bytes = (byte[])ds.Tables[0].Rows[0]["Data"];
                contentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                fileName = ds.Tables[0].Rows[0]["FileName"].ToString();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "Inline; filename=" + fileName);

                // Append cookie
                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);
                // end

                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
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

                    GridView gvSubFolder = e.Row.FindControl("gvSubFolder") as GridView;
                    DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_SECTIONS", ZoneID: Folder_ID, SnapId: Request.QueryString["SNAPID"].ToString());
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
                    DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_ARTIFACTS", SectionId: SubFolder_ID, ZoneID: Folder_ID, SnapId: Request.QueryString["SNAPID"].ToString());
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
                    string MainRefNo = drv["MainRefNo"].ToString();
                    string DocTypeId = drv["DocTypeId"].ToString();

                    GridView gvDocs = e.Row.FindControl("gvDocs") as GridView;
                    DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "get_BD_Docs_FileStructure",
                    RefNo: MainRefNo,
                    DocTypeId: DocTypeId,
                    SnapId: Request.QueryString["SNAPID"].ToString()
                    );

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
                    string RefNo = drv["RefNo"].ToString();
                    string UniqueRefNo = drv["UniqueRefNo"].ToString();

                    GridView gvFiles = e.Row.FindControl("gvFiles") as GridView;
                    DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_FILES", ID: ID, SnapId: Request.QueryString["SNAPID"].ToString());
                    gvFiles.DataSource = ds.Tables[0];
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

        protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();
                    string FileType = drv["FileType"].ToString();
                    string OWNERNAME = drv["OWNERNAME"].ToString();
                    string UploadFileName_Editable = drv["UploadFileName_Editable"].ToString();
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

                    LinkButton lbtnDownloadDocEditable = (LinkButton)e.Row.FindControl("lbtnDownloadDocEditable");

                    if (OWNERNAME.ToString() == Session["User_Id"].ToString() && (UploadFileName_Editable.ToString() != "" && UploadFileName_Editable != null))
                    {
                        lbtnDownloadDocEditable.Visible = true;
                    }
                    else
                    {
                        lbtnDownloadDocEditable.Visible = false;
                    }

                    HtmlControl iconhistory = (HtmlControl)e.Row.FindControl("iconhistory");

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
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "Download_File", ID: e.CommandArgument.ToString());

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
                    DataSet ds = dal.eTMF_SP(ACTION: "Download_File", ID: e.CommandArgument.ToString());

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
                    DataSet ds = dal.eTMF_SP(ACTION: "Download_OtherType_File", ID: e.CommandArgument.ToString());

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

                dal.eTMF_SP(ACTION: "ChnageStatus", ID: ID.Text, Status: drpAction.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            BIND_ZONES();
        }

        protected void drpShowAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["ShowAs"] = drpShowAs.SelectedValue;
                ShowAs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ShowAs()
        {
            try
            {
                if (drpShowAs.SelectedValue == "List View")
                {
                    gvFolder.Visible = false;
                    gvFiles_List.Visible = true;
                }
                else
                {
                    gvFolder.Visible = true;
                    gvFiles_List.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_FILES_LIST()
        {
            try
            {
                DataSet ds = dal.eTMF_Snapshot_SP(ACTION: "GET_FILES", SnapId: Request.QueryString["SNAPID"].ToString());
                gvFiles_List.DataSource = ds.Tables[0];
                gvFiles_List.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}