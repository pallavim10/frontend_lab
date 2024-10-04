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
                ex.ToString();
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

                ex.ToString();
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
                ex.ToString();
                throw;
            }
        }

        private void GET_SHIPMENT_DETAILS()
        {
            try
            {
                DataSet ds = Dal_MF.SPECIMEN_REPORT_SP(
                   ACTION: "GET_SHIPMENT_DETAILS",
                   SITEID: drpSite.SelectedValue);

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

                ex.Message.ToString();
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
                ex.Message.ToString();
            }
        }

        private void GET_SHIPMENT_DETAILS_EXPORT()
        {
            try
            {
                string xlname = "Shipment Details Report";

                DataSet ds = Dal_MF.SPECIMEN_REPORT_SP(
                   ACTION: "GET_SHIPMENT_DETAILS_EXPORT");

                DataSet dsExport = new DataSet();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
                ex.ToString();
                throw;
            }

        }

        protected void lbtnGETDATA_Click(object sender, EventArgs e)
        {
            GET_SHIPMENT_DETAILS();
        }
    }
}