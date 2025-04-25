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
    public partial class ReportBMBudget_TOM : System.Web.UI.Page
    {
        string ProjectID = "", Action = "", VersionID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Request.QueryString["ProjectId"];
            Action = Request.QueryString["Action"];
            VersionID = Request.QueryString["VersionID"];

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
                    DataSet dsMain, dsBudgetTOM;

                    if (Action == "GET_Project_Budget_Data")
                    {
                        dsMain = new DataSet();
                        dsMain = dal.Budget_SP(Action: "GET_Project_Budget_Data", Project_Id: ProjectID, Version_ID: VersionID);
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportBMBudget_TOM.rdlc");
                    }
                    else
                    {
                        dsMain = new DataSet();
                        dsMain = dal.Budget_SP(Action: "get_PT_TOM_Data", Project_Id: ProjectID, Version_ID: VersionID);
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportPassthrough Budget.rdlc");
                    }
                    

                    //dsBudgetTOM = new DataSet();
                    //dsBudgetTOM = dal.Budget_SP(Action: "get_SubTask_Resources_Data", Project_Id: ProjectID, Version_ID:VersionID);
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