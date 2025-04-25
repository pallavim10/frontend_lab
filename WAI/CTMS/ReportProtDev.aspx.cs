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
    public partial class ReportProtDev : System.Web.UI.Page
    {
        string PROTVOIL_ID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            PROTVOIL_ID = Request.QueryString["PROTVOIL_ID"];            

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
                    DataSet dsMain,dscomment, dsimpact, dscapa;

                    dsMain = new DataSet();
                    dsMain = dal.ProtocolVoilation_SP(Action: "GET_DATA_DATA", Project_ID: Session["PROJECTID"].ToString(), PROTVOIL_ID: PROTVOIL_ID);

                    dscomment = new DataSet();
                    dscomment = dal.getsetPDComments(Action: "GET_DATA_DATA", PROTVOIL_ID: PROTVOIL_ID);

                    dsimpact = new DataSet();
                    dsimpact = dal.getsetPDImpact(Action: "GET_DATA_DATA", PROTVOIL_ID: PROTVOIL_ID);

                    dscapa = new DataSet();
                    dscapa = dal.getsetPDCAPA(Action: "GET_DATA_DATA", PROTVOIL_ID: PROTVOIL_ID);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportProtDev.rdlc");

                    ReportDataSource datasourceMain = new ReportDataSource("DataSet1", dsMain.Tables[0]);
                    ReportDataSource datasourceMain1 = new ReportDataSource("DataSet2", dscomment.Tables[0]);
                    ReportDataSource datasourceMain2 = new ReportDataSource("DataSet3", dsimpact.Tables[0]);
                    ReportDataSource datasourceMain3 = new ReportDataSource("DataSet4", dscapa.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceMain);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceMain1);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceMain2);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceMain3);
                    ReportViewer1.LocalReport.Refresh();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}