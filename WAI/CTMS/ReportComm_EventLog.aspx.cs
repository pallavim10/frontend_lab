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
    public partial class ReportComm_EventLog : System.Web.UI.Page
    {
        string ORIGINS = "", Type = "", Nature = "", Department = "", Reference = "", Event = "";
        protected void Page_Load(object sender, EventArgs e)
        {           
            ORIGINS = Request.QueryString["ORIGINS"];
            Type = Request.QueryString["Type"];
            Nature = Request.QueryString["Nature"];
            Reference = Request.QueryString["Reference"];
            Department = Request.QueryString["Department"];
            Event = Request.QueryString["Event"];

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
                    if (ORIGINS != "0" || Type != "0" || Nature != "0" || Reference != "0" || Department != "0" || Event.ToString() != "")
                    {
                        dsMain = dal.Communication_SP(Action: "Get_EventLog_Filter_Data", PROJECTID: Session["PROJECTID"].ToString(), UserID: Session["USER_ID"].ToString(), ORIGINS: ORIGINS, Type: Type,
                        Nature: Nature, Reference: Reference, Department: Department, Event: Event);
                    }
                    else
                    {
                        dsMain = dal.Communication_SP(Action: "Get_EventLog", PROJECTID: Session["PROJECTID"].ToString(), UserID: Session["USER_ID"].ToString());
                    }                 
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportComm_EventLog.rdlc");

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