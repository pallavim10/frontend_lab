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
    public partial class ReportCTMS_RolesResponsilbility : System.Web.UI.Page
    {
        string ProjectID = "", Action = "", Role = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Request.QueryString["ProjectId"];
            Action = Request.QueryString["Action"];
            Role = Request.QueryString["Role"];

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
                    dsMain = dal.Budget_SP(Action: Action, Project_Id: ProjectID, Role_ID: Role);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportCTMS_RolesResponsilbility.rdlc");

                    //dsBudgetTOM = new DataSet();
                    //dsBudgetTOM = dal.Budget_SP(Action: "get_Group_SubTask_Logs_Data", Project_Id: ProjectID,ID:Group_ID);
                    ReportDataSource datasourceMain = new ReportDataSource("DataSet1", dsMain.Tables[0]);
                    //ReportDataSource datasourceSL = new ReportDataSource("DataSet2", dsBudgetTOM.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceMain);
                    //ReportViewer1.LocalReport.DataSources.Add(datasourceSL);
                    ReportViewer1.LocalReport.Refresh();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}