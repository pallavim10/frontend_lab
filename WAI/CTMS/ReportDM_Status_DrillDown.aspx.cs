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
    public partial class ReportDM_Status_DrillDown : System.Web.UI.Page
    {
        string ProjectID = "", INVID = "", INDICATIONID = "", SUBJID = "", STATUSID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Request.QueryString["ProjectId"];           
            INVID = Request.QueryString["INVID"];
            INDICATIONID = Request.QueryString["INDICATIONID"];
            SUBJID = Request.QueryString["SUBJID"];
            STATUSID = Request.QueryString["STATUSID"];

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
                    dsMain = dal.DM_ADD_UPDATE(ACTION: "GET_VISIT_PAGE_DATA", PROJECTID: Session["PROJECTID"].ToString(), INVID: INVID, SUBJECTID: SUBJID, INDICATION: INDICATIONID, DATATYPE: STATUSID);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportDM_Status_DrillDown.rdlc");

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