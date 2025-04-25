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
    public partial class eTMF_ShowMyView : System.Web.UI.Page
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
                        
                        Get_Views();

                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Views()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "Get_Views");

                drpView.DataSource = ds;
                drpView.DataValueField = "ID";
                drpView.DataTextField = "ViewName";
                drpView.DataBind();
                drpView.Items.Insert(0, new ListItem("-Select-", "0"));
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
                DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "Get_Show_ViewFiles",
                ID: drpView.SelectedValue,
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue
                );

                gvFiles.DataSource = ds;
                gvFiles.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Files_Count_Tiles()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "Get_Show_ViewFiles_Count_Tile",
                ID: drpView.SelectedValue,
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue
                );

                lblVal.Text = "0";
                lblVal1.Text = "0";
                lblVal2.Text = "0";
                lblVal3.Text = "0";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "Estimated")
                        {
                            lblVal.Text = ds.Tables[0].Rows[i]["COUNTS"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "Old / Replaced")
                        {
                            lblVal1.Text = ds.Tables[0].Rows[i]["COUNTS"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "Draft")
                        {
                            lblVal2.Text = ds.Tables[0].Rows[i]["COUNTS"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "Final")
                        {
                            lblVal3.Text = ds.Tables[0].Rows[i]["COUNTS"].ToString();
                        }
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

                dal_eTMF.eTMF_View_SP(ACTION: "ChnageStatus", ID: ID.Text, Status: drpAction.SelectedValue);
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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void COUNTRY()
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

        protected void SITE_AGAINST_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(COUNTRYID: drpCountry.SelectedValue, USERID: Session["User_ID"].ToString());
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
                SITE_AGAINST_COUNTRY();
                Get_Files();
                Get_Files_Count_Tiles();

                bind_Tree();
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
                Get_Files();
                Get_Files_Count_Tiles();
                bind_Tree();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
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
                    gvTreeZone.Visible = false;
                    gvFiles.Visible = true;
                }
                else
                {
                    gvTreeZone.Visible = true;
                    gvFiles.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Tree()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_View_SP(
                ACTION: "GET_VIEW_Zones",
                ID: drpView.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvTreeZone.DataSource = ds.Tables[0];
                    gvTreeZone.DataBind();
                }
                else
                {
                    gvTreeZone.DataSource = null;
                    gvTreeZone.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        int FolderDocs = 0;
        int SubFolderDocs = 0;
        int ArtifactDocs = 0;

        protected void gvTreeZone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    FolderDocs = 0;

                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Folder_ID = drv["Folder_ID"].ToString();

                    GridView gvTreeSection = e.Row.FindControl("gvTreeSection") as GridView;
                    DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "GET_VIEW_Sections", UploadZoneId: Folder_ID, ID: drpView.SelectedValue);
                    gvTreeSection.DataSource = ds.Tables[0];
                    gvTreeSection.DataBind();

                    Label lblCount = e.Row.FindControl("lblCount") as Label;
                    lblCount.Text = FolderDocs.ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvTreeSection.Rows.Count > 0)
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

        protected void gvTreeSection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    SubFolderDocs = 0;

                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Folder_ID = drv["Folder_ID"].ToString();
                    string SubFolder_ID = drv["SubFolder_ID"].ToString();

                    GridView gvTreeArtifact = e.Row.FindControl("gvTreeArtifact") as GridView;
                    DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "GET_VIEW_Artifacts", UploadSectionId: SubFolder_ID, UploadZoneId: Folder_ID, ID: drpView.SelectedValue);
                    gvTreeArtifact.DataSource = ds.Tables[0];
                    gvTreeArtifact.DataBind();

                    Label lblCount = e.Row.FindControl("lblCount") as Label;
                    lblCount.Text = SubFolderDocs.ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvTreeArtifact.Rows.Count > 0)
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

        protected void gvTreeArtifact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ArtifactDocs = 0;

                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();

                    GridView gvTreeDocs = e.Row.FindControl("gvTreeDocs") as GridView;
                    DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "GET_VIEW_DOCS", ARTIFACTS: ID, ID: drpView.SelectedValue);
                    gvTreeDocs.DataSource = ds.Tables[0];
                    gvTreeDocs.DataBind();

                    Label lblCount = e.Row.FindControl("lblCount") as Label;
                    lblCount.Text = ArtifactDocs.ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvTreeDocs.Rows.Count > 0)
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

        protected void gvTreeDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();

                    GridView gvTreeFiles = e.Row.FindControl("gvTreeFiles") as GridView;
                    DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "GET_VIEW_Files", DocID: ID, CountryID: drpCountry.SelectedValue, SiteID: drpSites.SelectedValue);
                    gvTreeFiles.DataSource = ds;
                    gvTreeFiles.DataBind();

                    Label lblCount = e.Row.FindControl("lblCount") as Label;
                    lblCount.Text = gvTreeFiles.Rows.Count.ToString();

                    FolderDocs += gvTreeFiles.Rows.Count;
                    SubFolderDocs += gvTreeFiles.Rows.Count;
                    ArtifactDocs += gvTreeFiles.Rows.Count;

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvTreeFiles.Rows.Count > 0)
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

        protected void gvTreeDocs_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvTreeFiles_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvTreeFiles_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void btnShowFiles_Click(object sender, EventArgs e)
        {
            try
            {
                lblHeader.Text = drpView.SelectedItem.Text;

                DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "Select_View", ID: drpView.SelectedValue);

                COUNTRY();
                drpCountry.SelectedValue = ds.Tables[0].Rows[0]["CountryId"].ToString();
                SITE_AGAINST_COUNTRY();
                drpSites.SelectedValue = ds.Tables[0].Rows[0]["SiteId"].ToString();

                if (ViewState["ShowAs"] != null)
                {
                    drpShowAs.SelectedValue = ViewState["ShowAs"].ToString();
                }

                ShowAs();

                Get_Files();
                //Get_Files_Count_Tiles();

                bind_Tree();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}