using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class RPT_SHIPMENT_DETAILS : System.Web.UI.Page
    {
        DAL_DE Dal_DE = new DAL_DE();
        DAL_MF Dal_MF = new DAL_MF();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SITE();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_SITE()
        {
            try
            {
                DataSet ds = Dal_DE.GET_SITEID_SP();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSite.DataSource = ds.Tables[0];
                    drpSite.DataValueField = "SiteID";
                    drpSite.DataBind();

                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        drpSite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void GridData_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
                {

                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gv.ShowFooter == true && gv.Rows.Count > 0)
                {

                    gv.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }
        }

        private void GET_SHIPMENT_DETAILS()
        {
            try
            {
                DataSet ds = Dal_MF.SPECIMEN_REPORT_SP(
                   ACTION: "GET_SHIPMENT_DETAILS",
                   SITEID: drpSite.SelectedValue,
                   FromDate: txtDateFrom.Text,
                   ToDate: txtDateTo.Text);

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grd_Data.DataSource = ds.Tables[0];
                    grd_Data.DataBind();
                    divRecord.Visible = true;
                }
                else
                {
                    grd_Data.DataSource = null;
                    grd_Data.DataBind();
                    divRecord.Visible = false;
                }
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                GET_SHIPMENT_DETAILS_EXPORT();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_SHIPMENT_DETAILS_EXPORT()
        {
            try
            {
                string xlname = "Shipment Details Report";

                DataSet ds = Dal_MF.SPECIMEN_REPORT_SP(
                   ACTION: "GET_SHIPMENT_DETAILS_EXPORT",
                   SITEID: drpSite.SelectedValue,
                   FromDate: txtDateFrom.Text,
                   ToDate: txtDateTo.Text);

                DataSet dsExport = new DataSet();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    DataTable copiedTable = ds.Tables[i].Copy();
                    if (Session["SID_ACTIVE"].ToString() == "False")
                    {
                        if (copiedTable.Columns.Contains("Total Specimen ID"))
                        {
                            copiedTable.Columns.Remove("Total Specimen ID");
                        }
                    }

                    dsExport.Tables.Add(copiedTable);
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void grd_Data_PreRender(object sender, EventArgs e)
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
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }

        }

        protected void lbtnGETDATA_Click(object sender, EventArgs e)
        {
            GET_SHIPMENT_DETAILS();
        }

        protected void grd_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                GridView gv = (sender as GridView);

                if (Session["SID_ACTIVE"].ToString() == "False")
                {
                    gv.HeaderRow.Cells[7].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }

            }
        }
    }
}