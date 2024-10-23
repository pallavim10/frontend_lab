using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpecimenTracking.App_Code;
using System.Data;

namespace SpecimenTracking
{
    public partial class UMT_RPT_Logins : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        private void GET_DATA()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_REPORT_SP(ACTION: "GET_LOGIN_LOGS",
                FromDate: txtDateFrom.Text,
                ToDate: txtDateTo.Text
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdData.DataSource = ds;
                    grdData.DataBind();
                    DivRecord.Visible = true;
                }
                else
                {
                    grdData.DataSource = null;
                    grdData.DataBind();
                    DivRecord.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DATA();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                ExceptionLogging.SendErrorToText(ex);

            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_REPORT_SP(ACTION: "GET_LOGIN_LOGS",
                FromDate: txtDateFrom.Text,
                ToDate: txtDateTo.Text
                );

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToPDF();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void ExportToPDF()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_REPORT_SP(ACTION: "GET_LOGIN_LOGS",
                FromDate: txtDateFrom.Text,
                ToDate: txtDateTo.Text
                );

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
    }
}