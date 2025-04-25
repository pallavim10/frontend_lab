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
    public partial class IWRS_DATA_TRANSFER : System.Web.UI.Page
    {
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!this.IsPostBack)
                {
                    GET_MODULE();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MODULE()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_MODULE");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    ddlModule.DataSource = ds.Tables[0];
                    ddlModule.DataValueField = "SOURCE_ID";
                    ddlModule.DataTextField = "HEADER";
                    ddlModule.DataBind();
                    ddlModule.Items.Insert(0, new ListItem("Patient Details", "000"));
                }
                else
                {
                    ddlModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DATA()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_DATA_TRANF_HISTORY", LISTID: ddlModule.SelectedValue);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdData.DataSource = ds;
                    grdData.DataBind();
                }
                else
                {
                    grdData.DataSource = null;
                    grdData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
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
                string FileName = "Data Transfer History";
                DataSet ds = ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_DATA_TRANF_HISTORY"
                );

                Multiple_Export_Excel.ToExcel(ds, FileName + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportPDF();
        }

        private void ExportPDF()
        {
            try
            {
                string FileName = "Data Transfer History";
                DataSet ds = ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_DATA_TRANF_HISTORY");

                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], FileName, Page.Response);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}