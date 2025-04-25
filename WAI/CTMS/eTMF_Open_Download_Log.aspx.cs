using CTMS.CommonFunction;
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
    public partial class eTMF_Open_Download_Log : System.Web.UI.Page
    {
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        DAL dal_Common = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }
        private void GET_LOG_FILE()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_ACTION_LOG_SP(ACTION: "GET_LOG_FILE", FROMDAT: TxtFromdate.Text, TODAT: TxtToDate.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdGETLOGFILE.DataSource = ds;
                    grdGETLOGFILE.DataBind();
                }
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            

        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                GET_LOG_FILE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdGETLOGFILE_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToPDF();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportToPDF()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_ACTION_LOG_SP(ACTION: "GET_LOG_FILE", FROMDAT: TxtFromdate.Text, TODAT: TxtToDate.Text);
                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
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
                DataSet ds = dal_eTMF.eTMF_ACTION_LOG_SP(ACTION: "GET_LOG_FILE", FROMDAT: TxtFromdate.Text, TODAT: TxtToDate.Text);
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        
    }
}