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
    public partial class ReportBudget_ManageProjectTasks : System.Web.UI.Page
    {
        string ProjectID = "", Deept_Id = "", Task_Id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Request.QueryString["ProjectId"];
            Task_Id = Request.QueryString["Task_Id"];

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
                    dsMain = dal.Budget_SP(Action: "get_Project_Task_TOM_Data", Project_Id: ProjectID,Task_ID:Task_Id);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportBudget_ManageProjectTasks.rdlc");

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