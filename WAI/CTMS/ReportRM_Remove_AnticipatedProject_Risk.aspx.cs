using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace CTMS
{
    public partial class ReportRM_Remove_AnticipatedProject_Risk : System.Web.UI.Page
    {
        string ProjectName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectName = Request.QueryString["ProjectName"];

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
                    dsMain = dal.getRiskRemoveList(ProjectId: ProjectName, Action: "FACTORDATA");
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportRM_Remove_AnticipatedProject_Risk.rdlc");

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