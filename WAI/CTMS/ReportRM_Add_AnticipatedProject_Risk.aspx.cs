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
    public partial class ReportRM_Add_AnticipatedProject_Risk : System.Web.UI.Page
    {
        string ProjectID = "", Action = "", INVID = "", Subject = "";
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    dsMain = dal.getRiskList(Action: "ExportExcel");
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportRM_Add_AnticipatedProject_Risk.rdlc");

                    ReportDataSource datasourceMain = new ReportDataSource("DataSet1", dsMain.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceMain);
                    ReportViewer1.LocalReport.Refresh();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}