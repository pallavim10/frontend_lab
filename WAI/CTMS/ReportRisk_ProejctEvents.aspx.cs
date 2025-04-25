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
    public partial class ReportRisk_ProejctEvents : System.Web.UI.Page
    {
        string RISKID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            RISKID = Request.QueryString["RISKID"];

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
                    DataTable dsMain = new DataTable();
                    DataSet dsrootcause,dsmitigation;
                   
                    dsMain = dal.getprojectevents(Action: "Update_DATA", Id: RISKID);

                    dsrootcause = new DataSet();
                    dsrootcause = dal.Risk_Cause_SP(Action: "GET", Event_ID: RISKID);

                    dsmitigation = new DataSet();
                    dsmitigation = dal.Risk_Mitigation_SP(Action: "GET", Event_ID: RISKID);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportRisk_ProejctEvents.rdlc");

                    ReportDataSource datasourceMain = new ReportDataSource("DataSet1", dsMain);
                    ReportDataSource datasourceMain1 = new ReportDataSource("DataSet2", dsrootcause.Tables[0]);
                    ReportDataSource datasourceMain2 = new ReportDataSource("DataSet3", dsmitigation.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceMain);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceMain1);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceMain2);
                    ReportViewer1.LocalReport.Refresh();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}