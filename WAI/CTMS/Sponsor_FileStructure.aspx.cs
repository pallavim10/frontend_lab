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
    public partial class Sponsor_FileStructure : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["prevURL"] = Request.Url.PathAndQuery.ToString();
                    Get_Structure();
                    bind_Folder();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Structure()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_DocType");

                drpDocType.DataSource = ds.Tables[0];
                drpDocType.DataValueField = "ID";
                drpDocType.DataTextField = "DocType";
                drpDocType.DataBind();
                drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));

                if (ds.Tables[0].Rows.Count == 1)
                {
                    drpDocType.SelectedIndex = 1;
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

        private void bind_Folder()
        {
            try
            {
                if (drpDocType.SelectedIndex != 0)
                {
                    DataSet ds = new DataSet();

                    ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_Folder", Project_ID: Session["PROJECTID"].ToString(), DocType: drpDocType.SelectedValue);

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
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_SubFolder", ID: Folder_ID, Project_ID: Session["PROJECTID"].ToString());
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
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_ProjectArtifacts", SubFolder_ID: SubFolder_ID, Folder_ID: Folder_ID, Project_ID: Session["PROJECTID"].ToString(), User: Session["User_ID"].ToString());
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
                    string MainRefNo = drv["MainRefNo"].ToString();
                    string DocTypeId = drv["DocTypeId"].ToString();

                    GridView gvDocs = e.Row.FindControl("gvDocs") as GridView;
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_BD_Docs_FileStructure", RefNo: MainRefNo, DocTypeId: DocTypeId);
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
    }
}