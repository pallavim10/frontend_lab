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
    public partial class NIWRS_DCF_AUDIT_REPORT : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_SITE();
                    GETDATA();
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
                        drpInvID.DataSource = ds.Tables[0];
                        drpInvID.DataValueField = "INVNAME";
                        drpInvID.DataBind();
                        drpInvID.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                    else
                    {
                        drpInvID.DataSource = ds.Tables[0];
                        drpInvID.DataValueField = "INVNAME";
                        drpInvID.DataBind();
                    }
                }
                else
                {
                    drpInvID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal.getSetDM(Action: "GET_PATIENT_REG", INVID: drpInvID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    drpSubID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSubID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "DCF_AUDIT_REPORT", SITEID: drpInvID.SelectedValue, SUBJID: drpSubID.SelectedValue);

                ds.Tables[0].Columns["Subject Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
                if (ds.Tables.Count > 0)
                {
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
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "DCF_AUDIT_REPORT", SITEID: drpInvID.SelectedValue, SUBJID: drpSubID.SelectedValue);

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

                ds.Tables[0].TableName = "DCF Audit Report";
                ds.Tables[0].Columns["Subject Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
                Multiple_Export_Excel.ToExcel(ds, "DCF Audit Report" + ".xls", Page.Response);
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
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "DCF_AUDIT_REPORT", SITEID: drpInvID.SelectedValue, SUBJID: drpSubID.SelectedValue);

                ds.Tables[0].TableName = "DCF Audit Report";
                ds.Tables[0].Columns["Subject Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], "DCF Audit Report", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}