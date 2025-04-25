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
    public partial class NSAE_REGULATORY_SUBMITED_REPORT : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtComment.Attributes.Add("MaxLength", "500");

                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.IsPostBack)
                {
                    FillSITEID();
                    GETDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillSITEID()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(USERID: Session["User_ID"].ToString());
                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataValueField = "INVID";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--Select--", "0"));

                FillSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_SAE_SUBJECTS",
                    INVID: drpInvID.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    drpSubID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSubID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SAEID();
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SAEID()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_REPORT_SP(ACTION: "GET_SAEIDS", SUBJID: drpSubID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSAEID.DataSource = ds.Tables[0];
                    drpSAEID.DataValueField = "SAEID";
                    drpSAEID.DataTextField = "SAEID";
                    drpSAEID.DataBind();
                    drpSAEID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSAEID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSAEID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SAE_STATUS();
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_SAE_STATUS()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_REPORT_SP(ACTION: "GET_SAE_STATUS",
                    SAEID: drpSAEID.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpStatus.DataSource = ds;
                    drpStatus.DataTextField = "STATUS";
                    drpStatus.DataValueField = "STATUS";
                    drpStatus.DataBind();
                }
                else
                {
                    drpStatus.Items.Clear();
                }
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
                        string fileName = FileUpload1.FileName;
                        string contentType = FileUpload1.PostedFile.ContentType;
                        string FileExtn = Path.GetExtension(fileName).ToLower();
                        if (FileExtn == ".pdf" || FileExtn == ".docx" || FileExtn == ".doc" || FileExtn == ".PDF")
                        {
                            Upload();
                            GETDATA();
                            Clear();

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Document Uploaded successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "' ", true);
                        }
                        else
                        {
                            Response.Write("<script> alert('Please Upload PDF and Word file only.')</script>");
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
                string fileName = FileUpload1.FileName;
                string contentType = FileUpload1.PostedFile.ContentType;
                byte[] fileData;
                using (Stream stream = FileUpload1.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();
                    }
                }
                dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "INSERT_REGULATRY_DOC",
                INVID: drpInvID.SelectedValue,
                SUBJID: drpSubID.SelectedValue,
                SAEID: drpSAEID.SelectedValue,
                STATUS: drpStatus.SelectedValue,
                FileName: fileName,
                ContentType: contentType,
                fileData: fileData,
                Notes: txtComment.Text
                );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "GET_REGULATRY_DOC",
                SAEID: drpSAEID.SelectedValue,
                INVID: drpInvID.SelectedValue,
                SUBJID: drpSubID.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvFiles.DataSource = ds;
                    gvFiles.DataBind();
                }
                else
                {
                    gvFiles.DataSource = null;
                    gvFiles.DataBind();
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
                    string FileType = drv["CONTENTTYPE"].ToString();

                    Label lbtnFileType = (Label)e.Row.FindControl("lbtnFileType");
                    HtmlControl ICON = (HtmlControl)e.Row.FindControl("ICONCLASS");
                    LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lblDeleteSupportDoc");
                    LinkButton DownloadSupportDoc = (LinkButton)e.Row.FindControl("lblDownloadSupportDoc");

                    if (drv["DELETED"].ToString() == "True")
                    {
                        lbtnDelete.Visible = false;
                        DownloadSupportDoc.Visible = false;
                    }
                    else
                    {
                        lbtnDelete.Visible = true;
                        DownloadSupportDoc.Visible = true;
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

        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "DeleteSupportDoc")
                {
                    dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "DELETE_REGULATRY_DOC",
                       ID: id
                       );

                    Response.Write("<script> alert('Document Deleted successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
                }
                else if (e.CommandName == "DownloadSupportDoc")
                {
                    string FileName, ContentType;
                    byte[] fileData;

                    DataSet ds = dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "GET_SAE_REGULATORY_DOCS_DETAILS",
                        ID: id
                        );

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            FileName = ds.Tables[0].Rows[0]["FILENAME"].ToString();
                            ContentType = ds.Tables[0].Rows[0]["CONTENTTYPE"].ToString();
                            fileData = (byte[])ds.Tables[0].Rows[0]["DATA"];
                            Response.Clear();

                            Response.ContentType = ContentType;
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

                            // Append cookie
                            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                            cookie.Value = "Flag";
                            cookie.Expires = DateTime.Now.AddDays(1);
                            Response.AppendCookie(cookie);
                            // end

                            Response.BinaryWrite(fileData);

                            Response.Flush();
                            Response.SuppressContent = true;
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.Url.PathAndQuery, false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void Clear()
        {
            try
            {
                drpInvID.SelectedIndex = 0;
                drpSubID.Items.Clear();
                drpSAEID.Items.Clear();
                drpStatus.Items.Clear();
                txtComment.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }
    }
}