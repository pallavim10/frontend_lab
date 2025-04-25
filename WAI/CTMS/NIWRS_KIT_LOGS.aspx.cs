using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class NIWRS_KIT_LOGS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnGetdata_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_AUDITTRAIL_SP(ACTION: "GET_KIT_NO_LOG",
                    FROMDATE: txtDateFrom.Text,
                    TODATE: txtDateTo.Text,
                    ID: Txtkitnumber.Text
                    );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GrdData.DataSource = ds.Tables[0];
                    GrdData.DataBind();

                }
                else
                {
                    GrdData.DataSource = null;
                    GrdData.DataBind();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GrdData_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
                {
                    //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gv.ShowFooter == true && gv.Rows.Count > 0)
                {
                    //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                    gv.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            DataSet ds = dal_IWRS.IWRS_AUDITTRAIL_SP(ACTION: "GET_KIT_NO_LOG",
                FROMDATE: txtDateFrom.Text,
                TODATE: txtDateTo.Text);

            ds.Tables[0].TableName = "Kit No. Logs";
            Multiple_Export_Excel.ToExcel(ds.Tables[0], "Kit No. Logs", Page.Response);
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportPDF();
        }

        private void ExportPDF()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_AUDITTRAIL_SP(ACTION: "GET_KIT_NO_LOG",
                FROMDATE: txtDateFrom.Text,
                TODATE: txtDateTo.Text);

                ds.Tables[0].TableName = "Kit No. Logs";
                

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Multiple_Export_Excel.ExportToPDF(ds.Tables[0], "Kit No. Logs", Page.Response);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}