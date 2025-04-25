using CTMS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class UMT_USER_REQUEST_LOG : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
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
                DataSet ds = dal_UMT.UMT_LOG_SP(ACTION: "GET_USER_REQUEST_LOGS",
                    USERNAME: txtUserName.Text,
                    FROMDATE: txtDateFrom.Text,
                    TODATE: txtDateTo.Text
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

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            ExportToPDF();
        }

        private void ExportToPDF()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_LOG_SP(ACTION: "GET_USER_REQUEST_LOGS",
                    USERNAME: txtUserName.Text,
                    FROMDATE: txtDateFrom.Text,
                    TODATE: txtDateTo.Text);

                ds.Tables[0].TableName = "User Request Report";
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], "User Request Report", Page.Response);
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_LOG_SP(ACTION: "GET_USER_REQUEST_LOGS",
                    USERNAME: txtUserName.Text,
                    FROMDATE: txtDateFrom.Text,
                    TODATE: txtDateTo.Text);

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}