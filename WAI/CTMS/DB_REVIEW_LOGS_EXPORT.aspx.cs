using CTMS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DB_REVIEW_LOGS_EXPORT : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                {
                    GET_MODULES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GET_MODULES()
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULENAME");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds;
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataValueField = "ID";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnShowReview_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_REVIEW_SP(
                    ACTION: "GET_REVIEW_LOGS",
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdReviewLogs.DataSource = ds;
                    grdReviewLogs.DataBind();

                    btnExportExcel.Visible = true;
                }
                else
                {
                    grdReviewLogs.DataSource = null;
                    grdReviewLogs.DataBind();

                    btnExportExcel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdReviewLogs.DataSource = null;
                grdReviewLogs.DataBind();
                btnExportExcel.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdField_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_REVIEW_LOGS_EXPORT", MODULEID: drpModule.SelectedValue);

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Module Reviews Logs_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    
    }
}