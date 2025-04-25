using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_LISTING_DATA : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["UserGroup_ID"] == null)
                    {
                        Response.Redirect("SessionExpired.aspx", true);
                        return;
                    }

                    DataSet ds = dal_DM.DM_LIST_SP(ACTION: "GET_LIST_DETAILS", ID: Request.QueryString["LISTID"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hdnlistid.Value = Request.QueryString["LISTID"].ToString();
                        lblHeader.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                    }

                    COUNTRY();
                    GET_INVID();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP();
                drpCountry.DataSource = ds.Tables[0];
                drpCountry.DataTextField = "COUNTRYNAME";
                drpCountry.DataValueField = "COUNTRYID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_INVID()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GET_INVID_SP();
                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataValueField = "INVID";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--All--", "0"));

                FillSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_INVID();
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
                DataSet ds = dal.GET_SUBJECT_SP(INVID: drpInvID.SelectedValue);
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

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal_DM.DM_LIST_SP(ACTION: "GETLISTDATA", LISTING_ID: hdnlistid.Value, SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue);
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

        protected void btngetTransposData_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal_DM.DM_LIST_SP(ACTION: "GETLISTDATA", LISTING_ID: hdnlistid.Value, SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue);
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
                ds = dal_DM.DM_LIST_SP(ACTION: "GETLISTDATA", LISTING_ID: hdnlistid.Value, SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue);

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
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

        protected void btnRTF_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToRTF();
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

                ds = dal_DM.DM_LIST_SP(ACTION: "GETLISTDATA", LISTING_ID: hdnlistid.Value, SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue);

                ds.Tables[0].TableName = lblHeader.Text;

                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportToRTF()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_DM.DM_LIST_SP(ACTION: "GETLISTDATA", LISTING_ID: hdnlistid.Value, SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue);

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToRTF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();

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

    }
}