using System;
using System.Collections.Generic;
using System.Linq;
using PPT;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CTMS
{
    public partial class IWRS_AUDITTRAIL_REPORT : System.Web.UI.Page
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
                    GetSite();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetSite()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );


                if (ds.Tables[0].Rows.Count > 0)
                {

                    drpSiteID.DataSource = ds.Tables[0];
                    drpSiteID.DataValueField = "INVID";
                    drpSiteID.DataBind();
                    drpSiteID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSiteID.DataSource = null;
                    drpSiteID.DataBind();
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
                DataSet ds = dal_IWRS.IWRS_AUDITTRAIL_SP(ACTION: "GET_AUDITTRAIL_REPORT",
                    SITEID: drpSiteID.SelectedValue,
                    SUBJECT_ID: drpSubID.SelectedValue,
                    DCFID: TxtDCFID.Text,
                    FROMDATE: txtDateFrom.Text,
                    TODATE: txtDateTo.Text
                    );

                ds.Tables[0].Columns["Subject Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
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

        protected void drpSiteID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSubjectID();
        }

        private void GetSubjectID()
        {
            try
            {
                DataSet ds = dal.GET_SUBJECT_SP(INVID: drpSiteID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--Select--", "0"));

                }
                else
                {
                    drpSubID.DataSource = null;
                    drpSubID.DataBind();

                    GrdData.DataSource = null;
                    GrdData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSubID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GrdData.DataSource = null;
            GrdData.DataBind();
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            ExportToPDF();
        }

        private void ExportToPDF()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_AUDITTRAIL_SP(ACTION: "GET_AUDITTRAIL_REPORT",
                    SITEID: drpSiteID.SelectedValue,
                    SUBJECT_ID: drpSubID.SelectedValue,
                    FROMDATE: txtDateFrom.Text,
                    TODATE: txtDateTo.Text);

                ds.Tables[0].TableName = "IWRS Audit Report";
                ds.Tables[0].Columns["Subject Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], "IWRS Audit Report", Page.Response);
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
            ExportSingleSheet();
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_AUDITTRAIL_SP(ACTION: "GET_AUDITTRAIL_REPORT",
                    SITEID: drpSiteID.SelectedValue,
                    SUBJECT_ID: drpSubID.SelectedValue,
                    FROMDATE: txtDateFrom.Text,
                    TODATE: txtDateTo.Text);

                ds.Tables[0].TableName = "IWRS Audit Report";
                ds.Tables[0].Columns["Subject Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
                Multiple_Export_Excel.ToExcel(ds.Tables[0], "IWRS Audit Report", Page.Response);
                //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    Multiple_Export_Excel.ToExcel(ds.Tables[0], "IWRS Audit Report", Page.Response);
                //}

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}