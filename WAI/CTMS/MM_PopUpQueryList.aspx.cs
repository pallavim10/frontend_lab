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
    public partial class MM_PopUpQueryList : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_MM = new DAL_MM();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_ID"] == null)
            {
                Response.Redirect("SessionExpired.aspx");
            }
            try
            {
                if (!IsPostBack)
                {
                    GetData();
                    GETDATA_DM();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        private void GETDATA_DM()
        {
            try
            {
                DataSet ds = dal_MM.MM_QUERY_SP(ACTION: "GET_MM_DM_QUERY", PVID: Request.QueryString["PVID"].ToString(), RECID: Request.QueryString["RECID"].ToString());
                grdDM.DataSource = ds;
                grdDM.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetData()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_MM.MM_QUERY_SP(ACTION: "GET_QUERY", PVID: Request.QueryString["PVID"].ToString(), RECID: Request.QueryString["RECID"].ToString());
                grd.DataSource = ds.Tables[0];
                grd.DataBind();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void GridView_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (Request.QueryString["SOURCE"] != null)
                {
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                string ID = drv["DM_QUERYID"].ToString();
                GridView grdComments = (GridView)e.Row.FindControl("grdComments");

                DataSet dsComments = dal_MM.MM_QUERY_SP(ACTION: "GET_QUERY_COMMENTS", ID: ID);
                grdComments.DataSource = dsComments;
                grdComments.DataBind();

                Control anchor = e.Row.FindControl("anchor") as Control;
                if (grdComments.Rows.Count > 0)
                {
                    anchor.Visible = true;
                }
                else
                {
                    anchor.Visible = false;
                }
            }
        }

        protected void grdDM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                string ID = drv["ID"].ToString();
                GridView grdDMComments = (GridView)e.Row.FindControl("grdDMComments");

                DataSet dsComments = dal_MM.MM_QUERY_SP(ACTION: "GET_QUERY_COMMENTS", ID: ID);
                grdDMComments.DataSource = dsComments;
                grdDMComments.DataBind();

                Control anchor = e.Row.FindControl("anchor") as Control;
                if (grdDMComments.Rows.Count > 0)
                {
                    anchor.Visible = true;
                }
                else
                {
                    anchor.Visible = false;
                }

            }
        }

    }
}