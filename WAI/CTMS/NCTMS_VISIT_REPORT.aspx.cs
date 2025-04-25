using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using Microsoft.Reporting.WebForms;

namespace CTMS
{
    public partial class NCTMS_VISIT_REPORT : System.Web.UI.Page
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
                DataSet dsMain;

                dsMain = new DataSet();

                dsMain = dal.CTMS_REPORTS(ACTION: "GET_CTMS_REPORT_DATA",
                   SVID: Request.QueryString["SVID"].ToString(),
                   INVID: Request.QueryString["SITEID"].ToString(),
                   VISITID: Request.QueryString["VISITID"].ToString(),
                   VISITNAME: Request.QueryString["VISIT"].ToString()
                   );

                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                if (Request.QueryString["VISIT"].ToString() == "Monitoring Visit")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CTMS_MONITORING_VISIT_REPORT.rdlc");
                }
                else if (Request.QueryString["VISIT"].ToString() == "Site Evaluation Visit")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CTMS_SITE_EVALUATION_REPORT.rdlc");
                }
                else if (Request.QueryString["VISIT"].ToString() == "Site Initiation Visit")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CTMS_SITE_INITIATION_REPORT.rdlc");
                }
                else if (Request.QueryString["VISIT"].ToString() == "Site Initiation Visit")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CTMS_SITE_INITIATION_REPORT.rdlc");
                }
                else if (Request.QueryString["VISIT"].ToString() == "Unblinded Interim Monitoring Visit")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CTMS_UNBLINDED_INTERIM_VISIT_REPORT.rdlc");
                }

                ReportDataSource rpt1 = new ReportDataSource("CTMS_VISIT_REPORT", dsMain.Tables[0]);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rpt1);
                ReportViewer1.LocalReport.Refresh();

                DataSet dsMain1 = dal.CTMS_REPORTS(ACTION: "GET_INV_CRA_DETAILS",
                    SVID: Request.QueryString["SVID"].ToString(),
                    INVID: Request.QueryString["SITEID"].ToString(),
                    VISITID: Request.QueryString["VISITID"].ToString(),
                    VISITNAME: Request.QueryString["VISIT"].ToString()
                    );

                ReportDataSource rpt2 = new ReportDataSource("CTMS_INV_CRA_DATA", dsMain1.Tables[0]);

                ReportViewer1.LocalReport.DataSources.Add(rpt2);
                ReportViewer1.LocalReport.Refresh();

                DataSet dsMain2 = dal.CTMS_REPORTS(ACTION: "GET_STATUS_DATA",
                   SVID: Request.QueryString["SVID"].ToString(),
                   INVID: Request.QueryString["SITEID"].ToString(),
                   VISITID: Request.QueryString["VISITID"].ToString(),
                   VISITNAME: Request.QueryString["VISIT"].ToString()
                   );

                ReportDataSource rpt3 = new ReportDataSource("CTMS_STATUS_DATA", dsMain2.Tables[0]);

                ReportViewer1.LocalReport.DataSources.Add(rpt3);
                ReportViewer1.LocalReport.Refresh();

                DataSet dsMain3 = dal.CTMS_REPORTS(ACTION: "GET_ATTENDANCE_DATA",
                  SVID: Request.QueryString["SVID"].ToString(),
                  INVID: Request.QueryString["SITEID"].ToString(),
                  VISITID: Request.QueryString["VISITID"].ToString(),
                  VISITNAME: Request.QueryString["VISIT"].ToString()
                  );

                ReportDataSource rpt4 = new ReportDataSource("CTMS_ATTENDANCE_RECORD", dsMain3.Tables[0]);

                ReportViewer1.LocalReport.DataSources.Add(rpt4);
                ReportViewer1.LocalReport.Refresh();

                if (Request.QueryString["VISIT"].ToString() == "Monitoring Visit")
                {
                    DataSet DS1 = dal.CTMS_REPORTS(ACTION: "GET_MONITORING_VISIT_TRACKER_DATA",
                      SVID: Request.QueryString["SVID"].ToString(),
                      INVID: Request.QueryString["SITEID"].ToString(),
                      VISITID: Request.QueryString["VISITID"].ToString(),
                      VISITNAME: Request.QueryString["VISIT"].ToString()
                      );

                    ReportDataSource rpt5 = new ReportDataSource("CTMS_ICF_TABLE", DS1.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt5);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt6 = new ReportDataSource("CTMS_RECRUITMENT_STATUS", DS1.Tables[1]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt6);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt7 = new ReportDataSource("CTMS_SAFETY_REPORT_CIOMS", DS1.Tables[2]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt7);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt8 = new ReportDataSource("CTMS_SDV_REPORT", DS1.Tables[3]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt8);
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Request.QueryString["VISIT"].ToString() == "Site Evaluation Visit")
                {
                    DataSet DS2 = dal.CTMS_REPORTS(ACTION: "GET_SITE_EVELUTION_TRACKER_DATA",
                          SVID: Request.QueryString["SVID"].ToString(),
                          INVID: Request.QueryString["SITEID"].ToString(),
                          VISITID: Request.QueryString["VISITID"].ToString(),
                          VISITNAME: Request.QueryString["VISIT"].ToString()
                          );

                    ReportDataSource rpt9 = new ReportDataSource("CTMS_Agreements_required", DS2.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt9);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt10 = new ReportDataSource("CTMS_Document_provided_investigator", DS2.Tables[1]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt10);
                    ReportViewer1.LocalReport.Refresh();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}