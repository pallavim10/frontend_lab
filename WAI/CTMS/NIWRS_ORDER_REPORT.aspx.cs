using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace CTMS
{
    public partial class NIWRS_ORDER_REPORT : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            string ORDERID = Request.QueryString["ORDERID"].ToString();
            if (!this.IsPostBack)
            {

                ReportViewer1.ShowExportControls = true;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.Reset();
                ReportViewer1.ShowPrintButton = true;
                try
                {
                    DAL dal;
                    dal = new DAL();
                    DataSet dsMain;

                    if (ORDERID.Contains("TLD-"))
                    {

                        dsMain = new DataSet();
                        dsMain = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_ORDER_DETAILS_CENTRAL_PRINT", ORDERID: ORDERID);
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/IWRS_Report_OrderDetails_Country.rdlc");

                        ReportDataSource datasourceMain = new ReportDataSource("DataSet2", dsMain.Tables[0]);

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasourceMain);
                        ReportViewer1.LocalReport.Refresh();

                    }
                    else
                    {

                        dsMain = new DataSet();
                        dsMain = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_ORDER_DETAILS_CENTRAL_PRINT", ORDERID: ORDERID);
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/IWRS_Report_OrderDetails.rdlc");

                        ReportDataSource datasourceMain = new ReportDataSource("DataSet2", dsMain.Tables[0]);

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasourceMain);
                        ReportViewer1.LocalReport.Refresh();

                    }
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
        }
    }
}