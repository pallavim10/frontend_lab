using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class User_Logs_Export : System.Web.UI.Page
    {
        DAL dal = new DAL();
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

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal.EXPORT_DATA_SP(ACTION: ddlList.SelectedValue
                    );

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdUserDetails.DataSource = ds;
                        grdUserDetails.DataBind();
                    }
                    else
                    {
                        grdUserDetails.DataSource = null;
                        grdUserDetails.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                DataSet ds = new DataSet();

                ds = dal.EXPORT_DATA_SP(ACTION: ddlList.SelectedValue);

                DataTable newDT = new DataTable();

                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    DataRow[] rows = ds.Tables[0].Select(" [" + dc.ColumnName.ToString() + "] IS NOT NULL ");
                    double num;
                    if (ISDATE(rows[0][dc.ColumnName].ToString()))
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(DateTime));
                    }
                    else
                    {
                        newDT.Columns.Add(dc.ColumnName, typeof(string));
                    }
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    newDT.ImportRow(row);
                }

                ds = new DataSet();

                ds.Tables.Add(newDT);

                ds.Tables[0].TableName = ddlList.SelectedValue;
                Multiple_Export_Excel.ToExcel(ds, ddlList.SelectedValue + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public bool ISDATE(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
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
                DataSet ds = new DataSet();

                ds = dal.EXPORT_DATA_SP(ACTION: ddlList.SelectedValue);

                ds.Tables[0].TableName = ddlList.SelectedValue;
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], ddlList.SelectedItem.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUserDetails_PreRender(object sender, EventArgs e)
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
    }
}