using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class ETMF_Download_Docs : System.Web.UI.Page
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
                    bind_Folder();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Folder()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_Folder", Project_ID: Session["PROJECTID"].ToString());
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

                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "Download", ID: ID, User: Session["User_ID"].ToString());

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
                Response.End();
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }

        }

        protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                DownloadFile(ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        int FolderDocs = 0;

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
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_SubFolder", ID: Folder_ID, Project_ID: Session["PROJECTID"].ToString());
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
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Folder_ID = drv["Folder_ID"].ToString();
                    string SubFolder_ID = drv["SubFolder_ID"].ToString();

                    GridView gvDocs = e.Row.FindControl("gvDocs") as GridView;
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_ProjectDownloads", SubFolder_ID: SubFolder_ID, Folder_ID: Folder_ID, Project_ID: Session["PROJECTID"].ToString(), User: Session["User_ID"].ToString());
                    gvDocs.DataSource = ds.Tables[0];
                    gvDocs.DataBind();

                    Label lblCount = e.Row.FindControl("lblCount") as Label;
                    lblCount.Text = gvDocs.Rows.Count.ToString();

                    FolderDocs += gvDocs.Rows.Count;

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
    }
}