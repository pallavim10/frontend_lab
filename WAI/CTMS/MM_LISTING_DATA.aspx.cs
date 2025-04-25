using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class MM_LISTING_DATA : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_MM = new DAL_MM();
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

                    Session["prevURL"] = Request.Url.PathAndQuery.ToString();

                    DataSet ds = dal_MM.MM_LIST_SP(ACTION: "GET_LIST_DETAILS", ID: Request.QueryString["LISTID"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hdnlistid.Value = Request.QueryString["LISTID"].ToString();
                        lblHeader.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                        hdnPrimMODULENAME.Value = ds.Tables[0].Rows[0]["PrimaryMODULENAME"].ToString();
                        hdnPrimMODULEID.Value = ds.Tables[0].Rows[0]["PrimaryMODULE"].ToString();
                        hdnAutoQueryText.Value = ds.Tables[0].Rows[0]["QUERYTEXT"].ToString();

                        if (ds.Tables[0].Rows[0]["Query_Report"].ToString() == "True")
                        {
                            lbtExportQueries.Visible = true;
                        }
                        else
                        {
                            lbtExportQueries.Visible = false;
                        }

                        if (ds.Tables[0].Rows[0]["COMMENT_REPORT"].ToString() == "True")
                        {
                            lbtExportComments.Visible = true;
                        }
                        else
                        {
                            lbtExportComments.Visible = false;
                        }

                        if (ds.Tables[0].Rows[0]["TRANSPOSE"].ToString() == "True")
                        {
                            hdntranspose.Value = "Yes";
                            lbtnPivot.Visible = true;
                        }
                        else
                        {
                            hdntranspose.Value = "No";
                            lbtnPivot.Visible = false;
                        }
                    }

                    COUNTRY();
                    SITE_AGAINST_COUNTRY();
                    FillSubject();
                    GET_OnClick();
                    GET_DASHBOARD_DATA();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void BIND_PivotOptions(DataTable dt)
        {
            try
            {
                if (ddlRowField.Items.Count < 2)
                {
                    ddlRowField.DataSource = dt.Columns;
                    ddlRowField.DataBind();
                    ddlRowField.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlRowField.Items.Remove("PVID");
                    ddlRowField.Items.Remove("RECID");
                }

                if (ddlColField.Items.Count < 2)
                {
                    ddlColField.DataSource = dt.Columns;
                    ddlColField.DataBind();
                    ddlColField.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlColField.Items.Remove("PVID");
                    ddlColField.Items.Remove("RECID");
                }

                if (ddlDataField.Items.Count < 2)
                {
                    ddlDataField.DataSource = dt.Columns;
                    ddlDataField.DataBind();
                    ddlDataField.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlDataField.Items.Remove("PVID");
                    ddlDataField.Items.Remove("RECID");
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

        private void GET_DASHBOARD_DATA()
        {
            try
            {
                if (Request.QueryString["INVID"] != null)
                {
                    drpInvID.SelectedValue = Request.QueryString["INVID"].ToString();
                    if (Request.QueryString["STATUS"].ToString() != "")
                    {
                        switch (Request.QueryString["STATUS"].ToString())
                        {
                            case "Total":
                                drpStatus.SelectedValue = "0";
                                break;

                            case "Open":
                                drpStatus.SelectedValue = "Unreviewed Records";
                                break;

                            case "Primary Reviewed":
                                drpStatus.SelectedValue = "Records Reviewed at least once by one reviewer";
                                break;

                            case "Secondary Reviewed":
                                drpStatus.SelectedValue = "Records Reviewed at least once by one reviewer";
                                break;

                            case "All Query":
                                drpStatus.SelectedValue = "Queried Records";
                                break;

                            case "Open Query":
                                drpStatus.SelectedValue = "Records with Unresolved Queries";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_OnClick()
        {
            try
            {
                DataSet ds = dal_MM.MM_LIST_SP(
                ACTION: "GET_OnClick",
                LISTING_ID: Request.QueryString["LISTID"].ToString()
                );

                if (ds.Tables.Count > 0)
                {
                    ViewState["OnClickDT"] = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SITE_AGAINST_COUNTRY()
        {
            try
            {
                if (Request.QueryString["INVID"] != null)
                {
                    DataSet dsCountry = dal.GET_COUNTRY_SP();
                    if (dsCountry.Tables.Count > 0)
                    {
                        if (dsCountry.Tables[0].Rows.Count > 0)
                        {
                            drpCountry.SelectedValue = dsCountry.Tables[0].Rows[0]["COUNTRYID"].ToString();
                        }
                    }
                }

                DataSet ds = dal.GET_INVID_SP(COUNTRYID: drpCountry.SelectedValue, USERID: Session["User_ID"].ToString());
                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataTextField = "INVID";
                drpInvID.DataValueField = "INVID";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--All--", "0"));

                if (Request.QueryString["INVID"] != null)
                {
                    drpInvID.SelectedValue = Request.QueryString["INVID"].ToString();
                }
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
                SITE_AGAINST_COUNTRY();
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
                drpSubID.DataSource = ds.Tables[0];
                drpSubID.DataValueField = "SUBJID";
                drpSubID.DataBind();
                drpSubID.Items.Insert(0, new ListItem("--All--", "0"));

                if (Request.QueryString["SUBJID"] != null)
                {
                    if (Request.QueryString["SUBJID"].ToString() != "")
                    {
                        drpSubID.SelectedValue = Request.QueryString["SUBJID"].ToString();
                    }
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

                ds = dal_MM.MM_LIST_SP(ACTION: "GETLISTDATA", LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, STATUS: drpStatus.SelectedValue, COUNTRYID: drpCountry.SelectedValue);

                if (hdntranspose.Value == "Yes")
                {
                    ViewState["dsPivot"] = ds;

                    BIND_PivotOptions(ds.Tables[0]);

                    if (ddlRowField.SelectedValue == "0" || ddlRowField.SelectedValue == "")
                    {
                        modalPivot.Show();
                    }
                    else
                    {
                        BIND_PIVOT(ds);
                    }

                    gridData_Tran.Visible = true;
                    gridData.Visible = false;
                }
                else
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        gridData.DataSource = ds;
                        gridData.DataBind();
                    }
                    else
                    {
                        gridData.DataSource = null;
                        gridData.DataBind();
                    }

                    gridData_Tran.Visible = false;
                    gridData.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_PIVOT(DataSet ds)
        {
            try
            {
                Pivot pvt = new Pivot(ds.Tables[0]);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    switch (ddlSummarize.SelectedValue)
                    {
                        case "Average":
                            gridData_Tran.DataSource = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Average, ddlColField.SelectedValue);
                            ViewState["dsPivoted"] = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Average, ddlColField.SelectedValue);
                            break;

                        case "Count":
                            gridData_Tran.DataSource = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Count, ddlColField.SelectedValue);
                            ViewState["dsPivoted"] = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Count, ddlColField.SelectedValue);
                            break;

                        case "First":
                            gridData_Tran.DataSource = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.First, ddlColField.SelectedValue);
                            ViewState["dsPivoted"] = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.First, ddlColField.SelectedValue);
                            break;

                        case "Last":
                            gridData_Tran.DataSource = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Last, ddlColField.SelectedValue);
                            ViewState["dsPivoted"] = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Last, ddlColField.SelectedValue);
                            break;

                        case "Min":
                            gridData_Tran.DataSource = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Min, ddlColField.SelectedValue);
                            ViewState["dsPivoted"] = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Min, ddlColField.SelectedValue);
                            break;

                        case "Max":
                            gridData_Tran.DataSource = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Max, ddlColField.SelectedValue);
                            ViewState["dsPivoted"] = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Max, ddlColField.SelectedValue);
                            break;

                        case "Sum":
                            gridData_Tran.DataSource = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Sum, ddlColField.SelectedValue);
                            ViewState["dsPivoted"] = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Sum, ddlColField.SelectedValue);
                            break;

                        default:
                            gridData_Tran.DataSource = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Var, ddlColField.SelectedValue);
                            ViewState["dsPivoted"] = pvt.PivotData(ddlRowField.SelectedValue, ddlDataField.SelectedValue, AggregateFunction.Var, ddlColField.SelectedValue);
                            break;
                    }

                    gridData_Tran.DataBind();


                }
                else
                {
                    gridData_Tran.DataSource = null;
                    gridData_Tran.DataBind();
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
                if (hdntranspose.Value == "Yes")
                {
                    ds = (DataSet)ViewState["dsPivot"];
                    ds.Tables[0].TableName = lblHeader.Text;
                    ds.Tables.Add((DataTable)ViewState["dsPivoted"]);
                    ds.Tables[1].TableName = "Pivot";
                }
                else
                {
                    ds = dal_MM.MM_LIST_SP(ACTION: "GETLISTDATA", LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, STATUS: drpStatus.SelectedValue, COUNTRYID: drpCountry.SelectedValue);

                    ds.Tables[0].Columns.RemoveAt(0);
                    ds.Tables[0].Columns.RemoveAt(0);
                }



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
                if (hdntranspose.Value == "Yes")
                {
                    ds.Tables.Add((DataTable)ViewState["dsPivoted"]);
                    ds.Tables[0].TableName = "Pivot";
                }
                else
                {
                    ds = dal_MM.MM_LIST_SP(ACTION: "GETLISTDATA", LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, STATUS: drpStatus.SelectedValue, COUNTRYID: drpCountry.SelectedValue);
                }
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
                if (hdntranspose.Value == "Yes")
                {
                    ds.Tables.Add((DataTable)ViewState["dsPivoted"]);
                    ds.Tables[0].TableName = "Pivot";
                }
                else
                {
                    ds = dal_MM.MM_LIST_SP(ACTION: "GETLISTDATA", LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, STATUS: drpStatus.SelectedValue, COUNTRYID: drpCountry.SelectedValue);
                }
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

        private void GET_DATA()
        {
            try
            {
                DataSet ds = dal_MM.MM_LIST_SP(ACTION: "GETLISTDATA", LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, STATUS: drpStatus.SelectedValue, COUNTRYID: drpCountry.SelectedValue);

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

        string[] gridDataOnClick;
        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (hdntranspose.Value == "Yes")
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                }
                else
                {
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                }

                string Headers = null;
                foreach (TableCell RowCell in e.Row.Cells)
                {
                    if (Headers == null)
                    {
                        Headers = RowCell.Text;
                    }
                    else
                    {
                        Headers = Headers + "ª" + RowCell.Text;
                    }
                }

                gridDataOnClick = Headers.Split('ª');
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                if (hdntranspose.Value == "Yes")
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                }
                else
                {
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;

                    if (ViewState["OnClickDT"] != null)
                    {
                        DataTable OnClickDT = (DataTable)ViewState["OnClickDT"];

                        foreach (DataRow dr in OnClickDT.Rows)
                        {
                            for (int i = 0; i < e.Row.Cells.Count; i++)
                            {
                                if (gridDataOnClick[i].ToString().Replace("&#39;", "'") == dr["FIELDNAME"].ToString())
                                {
                                    e.Row.Cells[i].Attributes.Add("onclick", dr["OnClick"].ToString());
                                    e.Row.Cells[i].CssClass = e.Row.Cells[i].CssClass + " fontBlue";
                                }
                            }
                        }
                    }

                    DataSet dsStatusDetails = dal_MM.MM_LIST_SP(ACTION: "GET_LIST_StatusDetails", PVID: drv["PVID"].ToString(), RECID: drv["RECID"].ToString(), LISTING_ID: hdnlistid.Value);

                    DataRow drStatusDetails = dsStatusDetails.Tables[0].Rows[0];

                    LinkButton lbtnReview = (LinkButton)e.Row.FindControl("lbtnReview");
                    LinkButton lbtnAnotherReviewed = (LinkButton)e.Row.FindControl("lbtnAnotherReviewed");
                    LinkButton lbtnReviewDone_PRIM = (LinkButton)e.Row.FindControl("lbtnReviewDone_PRIM");
                    Label lbtnReviewDone_SECOND = (Label)e.Row.FindControl("lbtnReviewDone_SECOND");
                    HtmlGenericControl divQueryCount = e.Row.FindControl("divQueryCount") as HtmlGenericControl;
                    LinkButton lbtnQueryCount = (LinkButton)e.Row.FindControl("lbtnQueryCount");

                    HtmlControl iconComments = (HtmlControl)e.Row.FindControl("iconComments");

                    int QueryCount = Convert.ToInt32(drStatusDetails["QueryCount"]);
                    int OpenQueryCount = Convert.ToInt32(drStatusDetails["OpenQueryCount"]);
                    int OpenAutoQueryCount = Convert.ToInt32(drStatusDetails["OpenAutoQueryCount"]);
                    string ListStatus = drStatusDetails["ListStatus"].ToString();
                    string ListStatus_ROLEID = drStatusDetails["ListStatus_ROLEID"].ToString();
                    string AnotherListStatus = drStatusDetails["AnotherListStatus"].ToString();

                    if (drStatusDetails["COMMENTSCOUNT"].ToString() != "0")
                    {
                        iconComments.Style.Add("color", "Red");
                        iconComments.Attributes.Add("title", "Comments (" + drStatusDetails["COMMENTSCOUNT"].ToString() + ")");
                    }

                    lbtnReview.CssClass = "disp-none";
                    lbtnAnotherReviewed.CssClass = "disp-none";
                    lbtnReviewDone_PRIM.CssClass = "disp-none";
                    lbtnReviewDone_SECOND.CssClass = "disp-none";

                    if (ListStatus == "" && AnotherListStatus == "")
                    {
                        lbtnReview.CssClass = "";
                    }
                    else if (ListStatus == "" && AnotherListStatus != "")
                    {
                        lbtnAnotherReviewed.CssClass = "";
                    }
                    else
                    {
                        if (ListStatus == "Primary Reviewed")
                        {
                            lbtnReviewDone_PRIM.CssClass = "";

                            if (ListStatus_ROLEID == Session["UserGroup_ID"].ToString())
                            {
                                lbtnReviewDone_PRIM.OnClientClick = "";
                                lbtnReviewDone_PRIM.Enabled = false;
                            }
                        }
                        else if (ListStatus == "Secondary Reviewed")
                        {
                            lbtnReviewDone_SECOND.CssClass = "";
                        }
                        else if (AnotherListStatus.Contains("Reviewed"))
                        {
                            lbtnAnotherReviewed.CssClass = "";
                        }
                    }

                    if (QueryCount > 0)
                    {
                        divQueryCount.Visible = true;
                        lbtnQueryCount.Text = QueryCount.ToString();

                        if (OpenQueryCount > 0)
                        {
                            divQueryCount.Attributes["class"] = "circleQueryCountRed";
                        }
                        else
                        {
                            divQueryCount.Attributes["class"] = "circleQueryCountGreen";
                        }
                    }

                    LinkButton lbtnAutoQuery = (LinkButton)e.Row.FindControl("lbtnAutoQuery");
                    if (hdnAutoQueryText.Value != "" && OpenAutoQueryCount == 0)
                    {
                        lbtnAutoQuery.Visible = true;
                    }
                    else
                    {
                        lbtnAutoQuery.Visible = false;
                    }
                }
            }
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gridData.Rows[rowIndex];

                string PVID = row.Cells[3].Text;
                string RECID = row.Cells[4].Text;
                string SUBJID = row.Cells[11].Text;
                string SOURCE = lblHeader.Text;

                if (e.CommandName == "AutoQuery")
                {
                    dal_MM.MM_QUERY_SP(
                    ACTION: "RAISE_AUTO_QUERY",
                    SOURCE: SOURCE,
                    PVID: PVID,
                    RECID: RECID,
                    LISTING_ID: hdnlistid.Value,
                    SUBJID: SUBJID,
                    QUERYTEXT: "Auto Raised Query"
                       );
                }

                GET_DATA();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void lbtExportQueries_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet_QUERY();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet_QUERY()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_MM.MM_QUERY_SP(ACTION: "GET_QUERY_REPORT", LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJID: drpSubID.SelectedValue, SITEID: drpInvID.SelectedValue);

                ds.Tables[0].TableName = lblHeader.Text + "_QueryReport";
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + "_QueryReport" + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtExportComments_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet_COMMENTS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet_COMMENTS()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_MM.MM_COMMENT_SP(ACTION: "GET_COMMENT_REPORT", LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue);

                ds.Tables[0].TableName = lblHeader.Text + "_Comments";
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + "_Comments" + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnPivotSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["dsPivot"];

                BIND_PIVOT(ds);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}