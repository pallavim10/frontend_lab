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
    public partial class ReportCTMS_IssueList : System.Web.UI.Page
    {
        string ProjectID = "", Action = "", INVID = "", Status = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Request.QueryString["ProjectId"];
            //Action = Request.QueryString["Action"];
            INVID = Request.QueryString["INVID"];
            Status = Request.QueryString["Status"];

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

                    if (ProjectID == "0")
                    {                        
                        dsMain = dal.getsetISSUES(
                        Action: "ISSUES_LIST_CTMS"
                        );
                        
                        
                    }
                    if (ProjectID != "0" && INVID == "0" && Status == "0")
                    {                        
                        dsMain = dal.getsetISSUES(
                        Action: "ISSUES_LIST_Project_CTMS",
                        Project_ID: ProjectID
                        );                                                
                    }
                    if (ProjectID != "0" && INVID != "0" && Status == "0")
                    {                        
                        dsMain = dal.getsetISSUES(
                        Action: "ISSUES_LIST_INVID_CTMS",
                        Project_ID: ProjectID,
                        INVID: INVID
                        );                                                
                    }

                    if (ProjectID == "0" && INVID == "0" && Status != "0")
                    {                        
                        dsMain = dal.getsetISSUES(
                        Action: "ISSUES_LIST_Status_CTMS",
                        Status: Status
                        );                                                
                    }
                    if (ProjectID != "0" && Status != "0" && INVID == "0")
                    {                        
                        dsMain = dal.getsetISSUES(
                        Action: "ISSUES_LIST_Status_Project_CTMS",
                        Project_ID: ProjectID,
                        Status: Status
                        );                                                
                    }
                    if (ProjectID != "0" && INVID != "0" && Status != "0")
                    {                        
                        dsMain = dal.getsetISSUES(
                        Action: "ISSUES_LIST_Status_Project_INVID_CTMS",
                        Project_ID: ProjectID,
                        INVID: INVID,
                         Status: Status
                        );                                               
                    }

                    //dsMain = dal.getsetISSUES(Action: Action, Project_ID: ProjectID, INVID: INVID, Status: Status);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportCTMS_IssueList.rdlc");

                    //dsMainBudgetTOM = new DataSet();
                    //dsMainBudgetTOM = dal.Budget_SP(Action: "get_Group_SubTask_Logs_Data", Project_Id: ProjectID,ID:Group_ID);
                    ReportDataSource datasourceMain = new ReportDataSource("DataSet1", dsMain.Tables[0]);
                    //ReportDataSource datasourceSL = new ReportDataSource("DataSet2", dsMainBudgetTOM.Tables[0]);

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