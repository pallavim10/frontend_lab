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
    public partial class ReportCTMS_Group_Logs : System.Web.UI.Page
    {
        string ProjectID = "", Action = "", Group_ID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Request.QueryString["ProjectId"];
            Action = Request.QueryString["Action"];
            Group_ID = Request.QueryString["GroupID"];

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
                    DataSet dsMain, dsGroup;

                    dsMain = new DataSet();
                    dsMain = dal.Budget_SP(Action: Action, Project_Id: ProjectID, ID: Group_ID);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportCTMS_Group_Logs.rdlc");

                    dsGroup = new DataSet();
                    dsGroup = dal.Budget_SP(Action: "edit_Group", ID: Group_ID);

                    ReportDataSource datasourceMain = new ReportDataSource("DataSet1", dsMain.Tables[0]);
                    ReportDataSource datasourceSL = new ReportDataSource("DataSet2", dsGroup.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceMain);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceSL);
                    ReportViewer1.LocalReport.Refresh();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}