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
    public partial class MM_LINKED_QUERY : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_DM_QUERY();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_DM_QUERY()
        {
            try
            {
                DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "GET_DM_QUERY_PVID_RECID", QUERYID: Request.QueryString["QUERYID"].ToString());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdQueryDetailReports.DataSource = ds;
                    grdQueryDetailReports.DataBind();
                }
                else
                {
                    grdQueryDetailReports.DataSource = null;
                    grdQueryDetailReports.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdQueryDetailReports_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Link")
                {
                    string rowIndex = e.CommandArgument.ToString();
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                    DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "LINK_MM_QUERY_TO_DM_QUERY",
                        DM_QUERYID: rowIndex,
                        QUERYID: Request.QueryString["QUERYID"].ToString()
                        );

                    Session["BACKTO_MM_QUERY_REPORT"] = "1";

                    Response.Write("<script> alert('MM query linked to DM successfully.'); window.location.href='MM_QUERY_REPORTS.aspx' </script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Session["BACKTO_MM_QUERY_REPORT"] = "1";
                Response.Redirect("MM_QUERY_REPORTS.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdQueryDetailReports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lblComment = (LinkButton)e.Row.FindControl("lblComment");
                if (dr["Query_Comment_Count"].ToString() != "0")
                {
                    lblComment.Visible = true;
                }
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
    }
}