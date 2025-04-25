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
    public partial class NIWRS_REPORT_RAND_TR_SUM : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["PREV_URL"] = Request.RawUrl.ToString();

                    GET_COUNTRY();
                    GET_SITE();
                    GET_SUBSITE();
                  //  GET_LIST();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnGetdata_Click(object sender, EventArgs e)
        {
            try
            {
                GET_LIST();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }
        private void GET_LIST()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "REPORT_UNBLIND_GET_RANDOMIZATION_AND_TRT_SUMMARY", SITEID: ddlSite.SelectedValue, COUNTRYID: ddlCountry.SelectedValue, SUBSITEID: ddlSubSite.SelectedValue, FROMDATE: txtDateFrom.Text, TODATE: txtDateTo.Text);

                ds.Tables[0].Columns["Participant Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridData.DataSource = ds;
                    gridData.DataBind();
                }
                else
                {
                    gridData.DataSource = null;
                    gridData.DataBind();
                }
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

        private void GET_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP(USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        ddlCountry.DataSource = ds.Tables[0];
                        ddlCountry.DataValueField = "COUNTRYID";
                        ddlCountry.DataTextField = "COUNTRYNAME";
                        ddlCountry.DataBind();
                        ddlCountry.Items.Insert(0, new ListItem("--All--", "-1"));
                    }
                    else
                    {
                        ddlCountry.DataSource = ds.Tables[0];
                        ddlCountry.DataValueField = "COUNTRYID";
                        ddlCountry.DataTextField = "COUNTRYNAME";
                        ddlCountry.DataBind();
                    }
                }
                else
                {
                    ddlSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                        ddlSite.Items.Insert(0, new ListItem("--All--", "-1"));
                    }
                    else
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                    }
                }
                else
                {
                    ddlSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBSITE()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_SUBSITE", SITEID: ddlSite.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        ddlSubSite.DataSource = ds.Tables[0];
                        ddlSubSite.DataValueField = "SubSiteID";
                        ddlSubSite.DataTextField = "SubSiteID";
                        ddlSubSite.DataBind();
                        ddlSubSite.Items.Insert(0, new ListItem("--All--", "-1"));
                    }
                    else
                    {
                        ddlSubSite.DataSource = ds.Tables[0];
                        ddlSubSite.DataValueField = "SubSiteID";
                        ddlSubSite.DataTextField = "SubSiteID";
                        ddlSubSite.DataBind();
                    }
                }
                else
                {
                    ddlSubSite.Items.Clear();
                }
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
                GET_SUBSITE();
              //  GET_LIST();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //protected void ddlSubSite_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GET_LIST();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SITE();
                GET_LIST();
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
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "REPORT_UNBLIND_GET_RANDOMIZATION_AND_TRT_SUMMARY", SITEID: ddlSite.SelectedValue, COUNTRYID: ddlCountry.SelectedValue);
                ds.Tables[0].Columns["Participant Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
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
                //DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "REPORT_UNBLIND_GET_RANDOMIZATION_AND_KIT_SUMMARY", SITEID: ddlSite.SelectedValue, COUNTRYID: ddlCountry.SelectedValue);
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "REPORT_UNBLIND_GET_RANDOMIZATION_AND_TRT_SUMMARY", SITEID: ddlSite.SelectedValue, COUNTRYID: ddlCountry.SelectedValue);

                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}