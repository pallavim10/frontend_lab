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
    public partial class eTMF_FileStructure : System.Web.UI.Page
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
                        BIND_TYPE();
                        Session["prevURL"] = Request.Url.PathAndQuery.ToString();

                        bind_Folder();
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
            DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "TYPE_EVENT");
            string Event = ds.Tables[0].Rows[0]["Event"].ToString();
            if (Event != "0")
            {
                DrpUploadUsing.Items.Add(new ListItem("Event", "2"));
            }

            DataSet ds1 = dal_eTMF.eTMF_DATA_SP(ACTION: "TYPE_MILESTONE");
            string Milestone = ds1.Tables[0].Rows[0]["Milestone"].ToString();
            if (Milestone != "0")
            {
                DrpUploadUsing.Items.Add(new ListItem("Milestone", "3"));
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

        private void bind_Folder()
        {
            try
            {
                DataSet ds = new DataSet();

                if (DrpUploadUsing.SelectedItem.Text != "eTMF")
                {
                    ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_Folder_Group", DOCTYPEID: drpDocType.SelectedValue);
                }
                else
                {
                    ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_Folder", DOCTYPEID: drpDocType.SelectedValue);
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

        protected void gvFolder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Folder_ID = drv["Folder_ID"].ToString();

                    GridView gvSubFolder = e.Row.FindControl("gvSubFolder") as GridView;
                    DataSet ds = new DataSet();

                    if (DrpUploadUsing.SelectedItem.Text != "eTMF")
                    {
                        ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_Sub_Folder_GROUP", FOLDERID: Folder_ID, DOCTYPEID: drpDocType.SelectedValue);
                    }
                    else
                    {
                        ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_Sub_Folder", FOLDERID: Folder_ID);
                    }

                    gvSubFolder.DataSource = ds.Tables[0];
                    gvSubFolder.DataBind();

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

                    GridView gvArtifact = e.Row.FindControl("gvArtifact") as GridView;
                    DataSet ds = new DataSet();

                    if (DrpUploadUsing.SelectedItem.Text != "eTMF")
                    {
                        ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_ARTIFACTS_GROUP", SUBFOLDERID: SubFolder_ID, FOLDERID: Folder_ID, DOCTYPEID: drpDocType.SelectedValue);
                    }
                    else
                    {
                        ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_ARTIFACTS", SUBFOLDERID: SubFolder_ID, FOLDERID: Folder_ID);
                    }


                    gvArtifact.DataSource = ds.Tables[0];
                    gvArtifact.DataBind();

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
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ArtifactID = drv["ID"].ToString();

                    GridView gvDocs = e.Row.FindControl("gvDocs") as GridView;
                    DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_BD_Docs_FileStructure", ARTIFACTS: ArtifactID);
                    gvDocs.DataSource = ds.Tables[0];
                    gvDocs.DataBind();

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
                    string Comment = drv["Comment"].ToString();
                    HtmlGenericControl divcomment = (HtmlGenericControl)e.Row.FindControl("divcomment");
                    LinkButton lbtnUploadDoc = (LinkButton)e.Row.FindControl("lbtnUploadDoc");
                    Label lblComment = (Label)e.Row.FindControl("lblComment");

                    if (Comment != "")
                    {
                        lblComment.Text = "[ " + Comment + " ]";
                        lblComment.ToolTip = Comment;

                    }


                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Folder();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DrpUploadUsing_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DrpUploadUsing.SelectedItem.Text == "eTMF")
                {
                    lblViewType.Text = "Select TMF Model :";
                    DataSet ds = dal.eTMF_QC_SP(ACTION: "get_DocType");

                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (DrpUploadUsing.SelectedItem.Text == "Event")
                {
                    lblViewType.Text = "Select" + " " + DrpUploadUsing.SelectedItem.Text + " :";
                    DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_DocType_Event");

                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (DrpUploadUsing.SelectedItem.Text == "Milestone")
                {
                    lblViewType.Text = "Select" + " " + DrpUploadUsing.SelectedItem.Text + " :";
                    DataSet ds = dal_eTMF.eTMF_UPLOAD_SP(ACTION: "get_DocType_Milestone");

                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
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
    }
}