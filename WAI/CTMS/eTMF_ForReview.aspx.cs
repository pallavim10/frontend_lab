using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class eTMF_ForReview : System.Web.UI.Page
    {

        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        DataSet dsUsers = new DataSet();

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
                        GET_ZONE();
                        //GET_LOGS_REVIEW();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_Artifacts()
        {
            DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_ProjectArtifacts", SubFolder_ID: ddlSections.SelectedValue, Folder_ID: ddlZone.SelectedValue, User: Session["User_ID"].ToString());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvArtifact.DataSource = ds.Tables[0];
                    gvArtifact.DataBind();
                    MianContain.Visible = true;
                }
                else
                {
                    gvArtifact.DataSource = null;
                    gvArtifact.DataBind();
                    MianContain.Visible = false;
                }
            }
        }

        protected void gvArtifact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();

                    Control anchor = e.Row.FindControl("anchor") as Control;

                    Repeater Repeater1 = e.Row.FindControl("Repeater1") as Repeater;

                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "get_BD_Docs_Review", ID: ID, REVIEW_STATUS: ddlReviewstatus.SelectedValue);
                    Repeater1.DataSource = ds.Tables[0];
                    Repeater1.DataBind();

                    if (ds.Tables[0].Rows.Count > 0)
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

        protected void btnReview_Click(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                Boolean Review = false;
                //GridView gvFolder = (GridView)button.FindControl("gvFolder");

                //foreach (GridViewRow gvFolderRow in gvFolder.Rows)
                //{
                //    GridView gvSubFolder = (GridView)gvFolderRow.FindControl("gvSubFolder");

                //    foreach (GridViewRow gvSubFolderRow in gvSubFolder.Rows)
                //    {
                GridView gvArtifact = (GridView)button.FindControl("gvArtifact");

                foreach (GridViewRow gvArtifactRow in gvArtifact.Rows)
                {

                    Repeater Repeater1 = gvArtifactRow.FindControl("Repeater1") as Repeater;

                    foreach (RepeaterItem Repeater1Item in Repeater1.Items)
                    {
                        Label lblID = (Label)Repeater1Item.FindControl("lblID");
                        CheckBox Chk = Repeater1Item.FindControl("CheckReView") as CheckBox;
                        if (Chk.Checked && ddlReviewstatus.SelectedValue == "0")
                        {
                            dal_eTMF.eTMF_SET_SP(ACTION: "eTMF_Review_Check", ID: lblID.Text);

                            Review = true;
                        }
                        else if (!Chk.Checked && ddlReviewstatus.SelectedValue == "1")
                        {
                            dal_eTMF.eTMF_SET_SP(ACTION: "eTMF_Review_UnCheck", ID: lblID.Text);

                        }
                    }
                }
                //    }
                //}

                if (ddlReviewstatus.SelectedValue == "1")
                {
                    Response.Write("<script> alert('Document Unreviewed successfully.');window.location.href='eTMF_ForReview.aspx';</script>");
                }
                else
                {
                    Response.Write("<script> alert('Document Reviewed successfully.');window.location.href='eTMF_ForReview.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                HiddenField lblReview = (HiddenField)(e.Item.FindControl("lblReview"));
                CheckBox chk = (CheckBox)(e.Item.FindControl("CheckReView"));
                if (lblReview.Value == "True")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_LOGS_REVIEW()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GET_LOGS_REVIEW", Folder_ID: ddlZone.SelectedValue, SubFolder_ID: ddlSections.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grd_Review_Log.DataSource = ds;
                    Grd_Review_Log.DataBind();
                }
                else
                {
                    Grd_Review_Log.DataSource = null;
                    Grd_Review_Log.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Grd_Review_Log_PreRender(object sender, EventArgs e)
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

        protected void lbnsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string lblHeader = "eTMF-Review-History";
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GET_LOGS_REVIEW_EXPORT");

                Multiple_Export_Excel.ToExcel(ds, lblHeader + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SECTION();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_ZONE()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_Folder");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlZone.DataSource = ds.Tables[0];
                    ddlZone.DataValueField = "ID";
                    ddlZone.DataTextField = "Folder";
                    ddlZone.DataBind();
                    ddlZone.Items.Insert(0, new ListItem("--Select--", "-1"));
                }
                else
                {
                    ddlZone.DataSource = null;
                    ddlZone.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        private void GET_SECTION()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_SubFolder",
                     ID: ddlZone.SelectedValue
                     );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSections.DataSource = ds.Tables[0];
                    ddlSections.DataValueField = "SubFolder_ID";
                    ddlSections.DataTextField = "Sub_Folder_Name";
                    ddlSections.DataBind();
                    ddlSections.Items.Insert(0, new ListItem("--Select--", "-1"));
                }
                else
                {
                    ddlSections.DataSource = null;
                    ddlSections.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                GET_Artifacts();
                GET_LOGS_REVIEW();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}