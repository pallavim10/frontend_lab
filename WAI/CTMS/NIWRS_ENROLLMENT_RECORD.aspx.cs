using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;

namespace CTMS
{
    public partial class NIWRS_ENROLLMENT_RECORD : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_DATA();
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
                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_DASHBOARD_ENROLL", ENTEREDBY: Session["User_ID"].ToString());
                gvVisits.DataSource = ds;
                gvVisits.DataBind();

                AddNewRow();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
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

        string[] gridHeaders;
        protected void gvVisits_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    string Headers = null;
                    foreach (TableCell RowCell in e.Row.Cells)
                    {
                        if (Headers == null)
                        {
                            Headers = RowCell.Text;
                        }
                        else
                        {
                            Headers = Headers + "," + RowCell.Text;
                        }
                    }

                    gridHeaders = Headers.Split(',');
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        if (gridHeaders[i].ToString() != "Country" && gridHeaders[i].ToString() != "Site Id" && gridHeaders[i].ToString() != "Site Name" && drv["Country"].ToString() != "Total")
                        {
                            DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_DASHBOARD_STATUS_ENROLL_DATA", FormHeader: gridHeaders[i].ToString(), SITEID: drv["Site Id"].ToString());
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                e.Row.Cells[i].Text = ds.Tables[0].Rows[0]["COUNT"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void AddNewRow()
        {
            DataTable dt = new DataTable();

            dt = GetDataTable(gvVisits);

            DataRow dr1 = dt.NewRow();
            dr1["Country"] = "Total";
            dr1["Site ID"] = "";
            dr1["Site Name"] = "";

            for (int i = 3; i < dt.Columns.Count; i++)
            {
                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    count += Convert.ToInt32(row[dt.Columns[i].ColumnName]);
                }
                dr1[dt.Columns[i].ColumnName] = count;
            }

            dt.Rows.Add(dr1);

            gvVisits.DataSource = dt;
            gvVisits.DataBind();

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
                DataTable dt = GetDataTable(gvVisits);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = GetDataTable(gvVisits);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        
    }
}