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
    public partial class NIWRS_UNBLIND_REPORT : System.Web.UI.Page
    {
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            string SUBJID = Request.QueryString["SUBJID"].ToString();
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

                    dsMain = new DataSet();
                    dsMain = dal_IWRS.IWRS_UNBLIND_SP(ACTION: "UNBLINDING_REPORT_PRINT", SUBJID: SUBJID);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/IWRS_Report_Unblinding Details.rdlc");

                    ReportDataSource DataSet1 = new ReportDataSource("DataSet4", dsMain.Tables[0]);
                    ReportDataSource DataSet2 = new ReportDataSource("DataSet2", dsMain.Tables[1]);
                    ReportDataSource DataSet3 = new ReportDataSource("DataSet3", dsMain.Tables[2]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(DataSet1);
                    ReportViewer1.LocalReport.DataSources.Add(DataSet2);
                    ReportViewer1.LocalReport.DataSources.Add(DataSet3);



                    ReportViewer1.LocalReport.Refresh();

                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
        }
    }
}