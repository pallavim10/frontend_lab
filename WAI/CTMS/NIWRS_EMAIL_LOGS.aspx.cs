using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class NIWRS_EMAIL_LOGS : System.Web.UI.Page
    {
        DAL dal = new DAL();

        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.ToString();
            }
        }

        protected void btnGetdata_Click(object sender, EventArgs e)
        {
            GETDATA();
        }
        protected void GETDATA()
        {
            try
            {
              
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_EMAIL_LOGS",
                                                       FROMDATE: txtDateFrom.Text,
                                                       TODATE: txtDateTo.Text);

              
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    grdData.DataSource = ds.Tables[0];
                    grdData.DataBind();
                }
                else
                {
                    grdData.DataSource = null;
                    grdData.DataBind();
                }
            }
            catch (Exception ex)
            {               
                lblErrorMsg.Text = ex.Message;
            }
        }


        protected void grdUserDetails_PreRender(object sender, EventArgs e)
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_EMAIL_LOGS");

                DataTable newDT = new DataTable();

                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    DataRow[] rows = ds.Tables[0].Select(" [" + dc.ColumnName.ToString() + "] IS NOT NULL ");
                    double num;
                    if (ISDATE(rows[0][dc.ColumnName].ToString()))
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(DateTime));
                    }
                    else
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    newDT.ImportRow(row);
                }

                ds = new DataSet();

                ds.Tables.Add(newDT);

                ds.Tables[0].TableName = "Email Logs";
                Multiple_Export_Excel.ToExcel(ds, "Email Logs" + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public bool ISDATE(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportPDF();
        }

        private void ExportPDF()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_EMAIL_LOGS");
                //DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_EMAIL_LOGS");
                ds.Tables[0].TableName = "Email Logs";

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Multiple_Export_Excel.ExportToPDF(ds.Tables[0], "Email Logs", Page.Response);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}