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
    public partial class NIWRS_EVENT_HISTORY : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    lbSubject.Text = Session["SUBJECTTEXT"].ToString();
                    GET_SITE();
                    GET_SUBJECT();
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
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                    }
                    else
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                        ddlSite.Items.Insert(0, new ListItem("--Select--", "0"));
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

        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBJECT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBJECT()
        {
            try
            {
                DataSet ds = dal.GET_SUBJECT_SP(INVID: ddlSite.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlSubject.DataSource = ds.Tables[0];
                        ddlSubject.DataValueField = "SUBJID";
                        ddlSubject.DataTextField = "SUBJID";
                        ddlSubject.DataBind();
                    }
                    else
                    {
                        ddlSubject.DataSource = ds.Tables[0];
                        ddlSubject.DataValueField = "SUBJID";
                        ddlSubject.DataTextField = "SUBJID";
                        ddlSubject.DataBind();
                        ddlSubject.Items.Insert(0, new ListItem("All", "0"));
                    }
                }
                else
                {
                    ddlSubject.Items.Clear();
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
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_EVENT_HISTORY",
                SITEID: ddlSite.SelectedValue,
                SUBJID: ddlSubject.SelectedValue,
                Value1: txtDateFrom.Text,
                Value3: txtDateTo.Text
                );

                ds.Tables[0].Columns["Subject Id"].ColumnName = Session["SUBJECTTEXT"].ToString();

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
                DataSet ds = ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_EVENT_HISTORY",
                SITEID: ddlSite.SelectedValue,
                SUBJID: ddlSubject.SelectedValue,
                Value1: txtDateFrom.Text,
                Value3: txtDateTo.Text
                );

                ds.Tables[0].Columns["Subject Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
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
                DataSet ds = ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_EVENT_HISTORY",
                SITEID: ddlSite.SelectedValue,
                SUBJID: ddlSubject.SelectedValue,
                Value1: txtDateFrom.Text,
                Value3: txtDateTo.Text
                );

                ds.Tables[0].Columns["Subject Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}