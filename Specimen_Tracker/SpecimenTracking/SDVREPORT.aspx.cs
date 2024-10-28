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
    public partial class SDVREPORT : System.Web.UI.Page
    {
        DAL_DE Dal_DE = new DAL_DE();
        DAL_MF Dal_MF = new DAL_MF();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["SID_ACTIVE"].ToString() == "True")
                    {
                        divSID.Visible = true;
                    }
                    else
                    {
                        divSID.Visible = false;
                    }
                    lblSUBSCRID.Text = Session["Subject ID"].ToString();
                    GET_SITE();
                    GET_SUBJECT();
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

        private void GET_SUBJECT()
        {
            try
            {
                DataSet ds = Dal_DE.GET_SUBJID_SP(drpSite.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubject.DataSource = ds.Tables[0];
                    drpSubject.DataValueField = "SUBJID";
                    drpSubject.DataBind();
                }
                drpSubject.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
            }
        }
        protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            GET_SUBJECT();
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

        private void GET_SDV_DETAILS()
        {
            try
            {
                DataSet ds = Dal_MF.SDV_SP(
                   ACTION: "GET_SDV_DETAILS",
                   SITEID: drpSite.SelectedValue,
                   SUBJID: drpSubject.SelectedValue,
                   SID: txtSpecimenID.Text);

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
                EXPORT_SDV();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void EXPORT_SDV()
        {
            try
            {
                string xlname = "SDV Report";

                DataSet ds = Dal_MF.SDV_SP(
                   ACTION: "GET_SDV_DETAILS_EXPORT",
                   SITEID: drpSite.SelectedValue,
                   SUBJID: drpSubject.SelectedValue,
                   SID: txtSpecimenID.Text);

                DataSet dsExport = new DataSet();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    DataTable copiedTable = ds.Tables[i].Copy();
                    if(copiedTable.Columns.Contains("Subject ID"))
                    {
                        copiedTable.Columns["Subject ID"].ColumnName = Session["Subject ID"].ToString();
                    }
                    if (Session["SID_ACTIVE"].ToString() == "False")
                    {
                        if (copiedTable.Columns.Contains("Specimen ID"))
                        {
                            copiedTable.Columns.Remove("Specimen ID");
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

        protected void grd_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                GridView gv = (sender as GridView);

                if (Session["SID_ACTIVE"].ToString() == "False")
                {
                    gv.HeaderRow.Cells[13].Visible = false;
                    e.Row.Cells[13].Visible = false;
                }
                gv.HeaderRow.Cells[12].Text = Session["Subject ID"].ToString();

                gv.HeaderRow.Cells[2].Visible = false;
                e.Row.Cells[2].Visible = false;

                gv.HeaderRow.Cells[3].Visible = false;
                e.Row.Cells[3].Visible = false;

                gv.HeaderRow.Cells[4].Visible = false;
                e.Row.Cells[4].Visible = false;

                gv.HeaderRow.Cells[5].Visible = false;
                e.Row.Cells[5].Visible = false;

                gv.HeaderRow.Cells[6].Visible = false;
                e.Row.Cells[6].Visible = false;

                gv.HeaderRow.Cells[7].Visible = false;
                e.Row.Cells[7].Visible = false;

                gv.HeaderRow.Cells[8].Visible = false;
                e.Row.Cells[8].Visible = false;

                gv.HeaderRow.Cells[9].Visible = false;
                e.Row.Cells[9].Visible = false;

                gv.HeaderRow.Cells[10].Visible = false;
                e.Row.Cells[10].Visible = false;



            }
        }

        protected void lbtnGETDATA_Click(object sender, EventArgs e)
        {
            GET_SDV_DETAILS();
        }
    }
}