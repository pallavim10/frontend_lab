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
    public partial class NSAE_REPORT_VIEW : System.Web.UI.Page
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

                if (Request.QueryString["REPORT_TYPE"].ToString() == "SAE FORM")
                {
                    dsMain = dal.SAE_REPORT(ACTION: "SAE_FORMS",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    STATUS: Request.QueryString["STATUS"].ToString()
                    );

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/NSAE_Report.rdlc");

                    ReportParameter UserName = new ReportParameter("UserName", Session["User_Name"].ToString(), false);
                    ReportViewer1.LocalReport.SetParameters(UserName);

                    ReportDataSource rpt1 = new ReportDataSource("NSAE_REPORTDATA", dsMain.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rpt1);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt2 = new ReportDataSource("NSAE_SUBJECT_DATA", dsMain.Tables[1]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt2);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt3 = new ReportDataSource("NSAE_SUSPECT_PRODUCT", dsMain.Tables[2]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt3);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt4 = new ReportDataSource("NSAE_EVENT_DETAILS", dsMain.Tables[3]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt4);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt5 = new ReportDataSource("NSAE_ADDITIONALDATA", dsMain.Tables[4]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt5);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt6 = new ReportDataSource("NSAE_EVENT_DESC", dsMain.Tables[5]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt6);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt7 = new ReportDataSource("NSAE_MEDICATION_TREAT", dsMain.Tables[6]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt7);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt8 = new ReportDataSource("NSE_LAB_DATA", dsMain.Tables[7]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt8);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt9 = new ReportDataSource("NSAE_CONC_MEDIC", dsMain.Tables[8]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt9);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt10 = new ReportDataSource("NSAE_MEDICAL_HISTORY", dsMain.Tables[9]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt10);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt11 = new ReportDataSource("NSAE_INVDETAILS", dsMain.Tables[10]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt11);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt12 = new ReportDataSource("NSAE_EVENT_NERRATIVE", dsMain.Tables[11]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt12);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt13 = new ReportDataSource("NSAE_REPORTER_DETAILS", dsMain.Tables[12]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt13);
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (Request.QueryString["REPORT_TYPE"].ToString() == "CIOMS")
                {
                    dsMain = dal.SAE_REPORT(ACTION: "CIOMS",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    STATUS: Request.QueryString["STATUS"].ToString(),
                    REPORTSTATUS: Request.QueryString["REPORTSTATUS"].ToString()
                    );

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/NSAE_CIOMS_REPORT.rdlc");

                    ReportParameter UserName = new ReportParameter("UserName", Session["User_Name"].ToString(), false);
                    ReportViewer1.LocalReport.SetParameters(UserName);

                    ReportDataSource rpt1 = new ReportDataSource("C_SUBJECT_DETAILS", dsMain.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rpt1);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt2 = new ReportDataSource("C_EVENT_DESC", dsMain.Tables[1]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt2);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt3 = new ReportDataSource("C_REPORT_INFO", dsMain.Tables[2]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt3);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt4 = new ReportDataSource("C_VACCIN_INFO", dsMain.Tables[3]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt4);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt5 = new ReportDataSource("C_EVENT_DETAILS", dsMain.Tables[4]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt5);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt6 = new ReportDataSource("C_ADDITION_EVENT_INFO", dsMain.Tables[5]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt6);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt7 = new ReportDataSource("C_MEDICAL_TREATE_EVENT", dsMain.Tables[6]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt7);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt8 = new ReportDataSource("C_LAB_INVESTIGATION", dsMain.Tables[7]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt8);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt9 = new ReportDataSource("C_CONC_MEDICATION", dsMain.Tables[8]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt9);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt10 = new ReportDataSource("C_MEDICAL_HISTORY", dsMain.Tables[9]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt10);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt11 = new ReportDataSource("C_INV_DETAILS", dsMain.Tables[10]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt11);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt12 = new ReportDataSource("C_ETHICS_DETAILS", dsMain.Tables[11]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt12);
                    ReportViewer1.LocalReport.Refresh();

                    ReportDataSource rpt13 = new ReportDataSource("C_EVENTNERRATIVE", dsMain.Tables[12]);

                    ReportViewer1.LocalReport.DataSources.Add(rpt13);
                    ReportViewer1.LocalReport.Refresh();
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}