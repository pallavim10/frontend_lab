using System;
using System.Data;
using Microsoft.Reporting.WebForms;
using PPT;

namespace CTMS
{
    public partial class NCTMS_FU_LETTER : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    ReportData();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void ReportData()
        {
            ReportViewer1.ShowExportControls = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.Reset();
            ReportViewer1.ShowPrintButton = true;
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet dsMain = new DataSet();

                if (Request.QueryString["REPORTTYPE"].ToString() == "FU_REPORT")
                {
                    dsMain = dal.CTMS_REPORTS(ACTION: "GET_FUP_LETTER",
                        SVID: Request.QueryString["SVID"].ToString()
                        );

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CTMS_FUP_Letter.rdlc");

                    ReportDataSource rpt1 = new ReportDataSource("CTMS_FU", dsMain.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rpt1);
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Request.QueryString["REPORTTYPE"].ToString() == "CL_REPORT")
                {
                    dsMain = dal.CTMS_REPORTS(ACTION: "GET_CL_LETTER",
                        SVID: Request.QueryString["SVID"].ToString()
                        );

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CTMS_CL_Letter.rdlc");

                    ReportDataSource rpt1 = new ReportDataSource("CTMS_CL", dsMain.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rpt1);
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Request.QueryString["REPORTTYPE"].ToString() == "COL_REPORT")
                {
                    dsMain = dal.CTMS_REPORTS(ACTION: "GET_COL_LETTER",
                        SVID: Request.QueryString["SVID"].ToString()
                        );

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CTMS_COL_LETTER.rdlc");

                    ReportDataSource rpt1 = new ReportDataSource("CTMS_COL", dsMain.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rpt1);
                    ReportViewer1.LocalReport.Refresh();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}