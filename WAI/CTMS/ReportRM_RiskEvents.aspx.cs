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
    public partial class ReportRM_RiskEvents : System.Web.UI.Page
    {
        string ProjectID = "", Type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Request.QueryString["ProjectName"];
            Type = Request.QueryString["Type"];            

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
                    DataTable dsMain;

                    dsMain = new DataTable();

                    if (Type == "1")
                    {
                        dsMain = dal.getprojectevents(Action: "Analyzed", Id: ProjectID);
                    }
                    else if (Type == "2")
                    {
                        dsMain = dal.getprojectevents(Action: "Unanalyzed", Id: ProjectID);
                    }
                    else
                    {
                        dsMain = dal.getprojectevents(Action: "Select", Id: ProjectID);
                    }                  
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportRM_RiskEvents.rdlc");

                    ReportDataSource datasourceMain = new ReportDataSource("DataSet1", dsMain);

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