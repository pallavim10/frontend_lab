using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class MM_LISTING_DATA_SUBJECT : System.Web.UI.Page
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
                    DataSet ds = dal_MM.MM_LIST_SP(ACTION: "GET_LIST_DETAILS", ID: Request.QueryString["LISTING_ID"].ToString());

                    hdnlistid.Value = Request.QueryString["LISTING_ID"].ToString();
                    hdnSUBJID.Value = Request.QueryString["SUBJID"].ToString();

                    lblHeader.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                    hdnPrimMODULENAME.Value = ds.Tables[0].Rows[0]["PrimaryMODULENAME"].ToString();
                    hdnPrimMODULEID.Value = ds.Tables[0].Rows[0]["PrimaryMODULE"].ToString();
                    hdnAutoQueryText.Value = ds.Tables[0].Rows[0]["QUERYTEXT"].ToString();

                    if (ds.Tables[0].Rows[0]["TRANSPOSE"].ToString() == "True")
                    {
                        hdntranspose.Value = "VisitNameVise";
                    }
                    else
                    {
                        hdntranspose.Value = "FieldNameVise";
                    }

                    lblHeader.Text = ds.Tables[0].Rows[0]["NAME"].ToString();


                    GET_OnClick();
                    GET_Other_Listings();

                    GET_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GET_Other_Listings()
        {
            try
            {
                DataSet ds = dal_MM.MM_LIST_SP(
                ACTION: "GET_Other_Listings",
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                FIELDNAME: "Subject"
                );

                repeatOtherListings.DataSource = ds;
                repeatOtherListings.DataBind();
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
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString()
                );

                ViewState["OnClickDT"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_DATA()
        {
            try
            {
                DataSet ds = new DataSet();
                if (hdntranspose.Value == "VisitNameVise")
                {
                    ds = ds = dal_MM.MM_LIST_SP(
                    ACTION: "GETLISTDATA_TRANSPOSE_DETAILS",
                    LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString()
                    );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gridData_Tran.DataSource = ds;
                        gridData_Tran.DataBind();
                    }
                    else
                    {
                        gridData_Tran.DataSource = null;
                        gridData_Tran.DataBind();
                    }

                    gridData.Visible = false;
                    gridData_Tran.Visible = true;
                }
                else
                {
                    ds = dal_MM.MM_LIST_SP(
                    ACTION: "GETLISTDATA_DETAILS",
                    LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                    OnClickFilter: Request.QueryString["SUBJID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                    FIELDNAME: "Subject"
                    );

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

                    gridData.Visible = true;
                    gridData_Tran.Visible = false;
                }

                if (Request.QueryString["PVID"] != null)
                {
                    GET_FORM_DETAILS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_FORM_DETAILS()
        {
            try
            {
                hdnPVID.Value = Request.QueryString["PVID"].ToString();
                hdnRECID.Value = Request.QueryString["RECID"].ToString();

                DataSet ds = dal_MM.MM_LIST_SP(
                    ACTION: "GET_FORM_DETAILS",
                    PVID: Request.QueryString["PVID"].ToString(),
                    RECID: Request.QueryString["RECID"].ToString(),
                    LISTING_ID: Request.QueryString["PREV_LISTID"].ToString()
                    );

                DataTable outputTable = new DataTable();

                if (ds.Tables.Count > 0)
                {
                    outputTable.Columns.Add("FIELDNAME");
                    outputTable.Columns.Add("DATA");
                    outputTable.Columns.Add("Condition1");
                    outputTable.Columns.Add("Ans1");
                    outputTable.Columns.Add("Or");
                    outputTable.Columns.Add("Condition2");
                    outputTable.Columns.Add("Ans2");
                    outputTable.Columns.Add("Color");

                    DataColumnCollection columns = outputTable.Columns;

                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        DataSet dsColor = dal_MM.MM_LIST_SP(
                            ACTION: "GET_COLOR_CODE",
                            LISTING_ID: Request.QueryString["PREV_LISTID"].ToString(),
                            FIELDNAME: dc.ColumnName.ToString()
                            );

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow drNew = outputTable.NewRow();
                            drNew["FIELDNAME"] = dc.ColumnName;
                            drNew["DATA"] = ds.Tables[0].Rows[i][dc.ColumnName];

                            if (dsColor.Tables[0].Rows.Count > 0)
                            {
                                drNew["Condition1"] = dsColor.Tables[0].Rows[0]["Condition1"];
                                drNew["Ans1"] = dsColor.Tables[0].Rows[0]["Ans1"];
                                drNew["Or"] = dsColor.Tables[0].Rows[0]["Or"];
                                drNew["Condition2"] = dsColor.Tables[0].Rows[0]["Condition2"];
                                drNew["Ans2"] = dsColor.Tables[0].Rows[0]["Ans2"];
                                drNew["Color"] = dsColor.Tables[0].Rows[0]["Color"];
                            }
                            else
                            {
                                drNew["Condition1"] = "";
                                drNew["Ans1"] = "";
                                drNew["Or"] = "";
                                drNew["Condition2"] = "";
                                drNew["Ans2"] = "";
                                drNew["Color"] = "";
                            }

                            outputTable.Rows.Add(drNew);
                        }
                    }

                    repeatData.DataSource = outputTable;
                    repeatData.DataBind();
                }


                if (repeatData.Items.Count > 0)
                {
                    divDetails.Visible = true;
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

        string[] gridDataOnClick;
        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (hdntranspose.Value == "FieldNameVise")
                {
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                }
                else
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
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

                if (hdntranspose.Value == "FieldNameVise")
                {
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;

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
                else
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                }
            }
        }

        protected void btngetTransposData_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (hdntranspose.Value == "FieldNameVise")
                {
                    hdntranspose.Value = "VisitNameVise";
                    ds = ds = dal_MM.MM_LIST_SP(
                        ACTION: "GETLISTDATA_TRANSPOSE_DETAILS",
                        LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                        SUBJID: Request.QueryString["SUBJID"].ToString()
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gridData_Tran.DataSource = ds;
                        gridData_Tran.DataBind();
                    }
                    else
                    {
                        gridData_Tran.DataSource = null;
                        gridData_Tran.DataBind();
                    }

                    gridData.Visible = false;
                    gridData_Tran.Visible = true;
                }
                else
                {
                    hdntranspose.Value = "FieldNameVise";

                    ds = dal_MM.MM_LIST_SP(
                    ACTION: "GETLISTDATA_DETAILS",
                    LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                    OnClickFilter: Request.QueryString["SUBJID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                    FIELDNAME: "Subject"
                    );

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

                    gridData.Visible = true;
                    gridData_Tran.Visible = false;
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
                if (hdntranspose.Value == "FieldNameVise")
                {
                    ds = dal_MM.MM_LIST_SP(
                    ACTION: "GETLISTDATA_DETAILS",
                    LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                    OnClickFilter: Request.QueryString["SUBJID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                    FIELDNAME: "Subject"
                    );
                }
                else
                {
                    ds = dal_MM.MM_LIST_SP(
                    ACTION: "GETLISTDATA_TRANSPOSE_DETAILS",
                    LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString()
                    );
                }

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
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
                if (hdntranspose.Value == "FieldNameVise")
                {
                    ds = dal_MM.MM_LIST_SP(
                    ACTION: "GETLISTDATA_DETAILS",
                    LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                    OnClickFilter: Request.QueryString["SUBJID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                    FIELDNAME: "Subject"
                    );
                }
                else
                {
                    ds = dal_MM.MM_LIST_SP(
                    ACTION: "GETLISTDATA_TRANSPOSE_DETAILS",
                    LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString()
                    );
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
                if (hdntranspose.Value == "FieldNameVise")
                {
                    ds = dal_MM.MM_LIST_SP(
                    ACTION: "GETLISTDATA_DETAILS",
                    LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                    OnClickFilter: Request.QueryString["SUBJID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                    FIELDNAME: "Subject"
                    );
                }
                else
                {
                    ds = dal_MM.MM_LIST_SP(
                    ACTION: "GETLISTDATA_TRANSPOSE_DETAILS",
                    LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString()
                    );
                }

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToRTF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("PVID");
            outputTable.Columns.Add("RECID");
            outputTable.Columns.Add("Subject");
            outputTable.Columns.Add("FIELDNAME");

            DataColumnCollection columns = outputTable.Columns;

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                if (!columns.Contains(inputTable.Rows[i]["VISIT"].ToString()))
                {
                    outputTable.Columns.Add(inputTable.Rows[i]["VISIT"].ToString());
                }
            }

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                foreach (DataColumn dc in inputTable.Columns)
                {
                    if (dc.ColumnName != "Subject" && dc.ColumnName != "VISIT" && dc.ColumnName != "PVID" && dc.ColumnName != "RECID" && dc.ColumnName != "QueryCount" && dc.ColumnName != "ListStatus" && dc.ColumnName != "AnotherListStatus")
                    {
                        if (columns.Contains(inputTable.Rows[i]["VISIT"].ToString()))
                        {
                            DataRow drNew = outputTable.NewRow();
                            drNew["PVID"] = inputTable.Rows[i]["PVID"];
                            drNew["Subject"] = inputTable.Rows[i]["Subject"];
                            drNew["FIELDNAME"] = dc.ColumnName;
                            drNew[inputTable.Rows[i]["VISIT"].ToString()] = inputTable.Rows[i][dc.ColumnName];
                            outputTable.Rows.Add(drNew);
                        }
                    }
                }
            }

            return outputTable;
        }

        protected void repeatData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;

                    string Condition1 = row["Condition1"].ToString();
                    string Ans1 = row["Ans1"].ToString();
                    string Or = row["Or"].ToString();
                    string Condition2 = row["Condition2"].ToString();
                    string Ans2 = row["Ans2"].ToString();
                    string Color = row["Color"].ToString();
                    string DATA = row["DATA"].ToString();

                    Label lblData = (Label)e.Item.FindControl("lblData");

                    bool Result = false;

                    if (Condition1 != "")
                    {

                        if (Condition1 == "IS NULL" && DATA == "")
                        {
                            Result = true;
                        }
                        else if (Condition1 == "IS NOT NULL" && DATA != "")
                        {
                            Result = true;
                        }
                        else if (Condition1 == "=" && DATA == Ans1)
                        {
                            Result = true;
                        }
                        else if (Condition1 == "!=" && DATA != Ans1)
                        {
                            Result = true;
                        }
                        else if (Condition1 == ">")
                        {
                            if (IsNumeric(DATA) && IsNumeric(Ans1))
                            {
                                if (Convert.ToInt32(DATA) > Convert.ToInt32(Ans1))
                                {
                                    Result = true;
                                }
                            }
                        }
                        else if (Condition1 == "=>")
                        {
                            if (IsNumeric(DATA) && IsNumeric(Ans1))
                            {
                                if (Convert.ToInt32(DATA) >= Convert.ToInt32(Ans1))
                                {
                                    Result = true;
                                }
                            }
                        }
                        else if (Condition1 == "<")
                        {
                            if (IsNumeric(DATA) && IsNumeric(Ans1))
                            {
                                if (Convert.ToInt32(DATA) < Convert.ToInt32(Ans1))
                                {
                                    Result = true;
                                }
                            }
                        }
                        else if (Condition1 == "=<")
                        {
                            if (IsNumeric(DATA) && IsNumeric(Ans1))
                            {
                                if (Convert.ToInt32(DATA) <= Convert.ToInt32(Ans1))
                                {
                                    Result = true;
                                }
                            }
                        }
                        else if (Condition1 == "[_]%")
                        {
                            if (DATA.StartsWith(Ans1))
                            {
                                Result = true;
                            }
                        }
                        else if (Condition1 == "![_]%")
                        {
                            if (!DATA.StartsWith(Ans1))
                            {
                                Result = true;
                            }
                        }
                        else if (Condition1 == "%_%")
                        {
                            if (DATA.Contains(Ans1))
                            {
                                Result = true;
                            }
                        }
                        else if (Condition1 == "!%_%")
                        {
                            if (!DATA.Contains(Ans1))
                            {
                                Result = true;
                            }
                        }

                        if (Or != "" && Result == false)
                        {
                            if (Condition2 == "IS NULL" && DATA == "")
                            {
                                Result = true;
                            }
                            else if (Condition2 == "IS NOT NULL" && DATA != "")
                            {
                                Result = true;
                            }
                            else if (Condition2 == "=" && DATA == Ans2)
                            {
                                Result = true;
                            }
                            else if (Condition2 == "!=" && DATA != Ans2)
                            {
                                Result = true;
                            }
                            else if (Condition2 == ">")
                            {
                                if (IsNumeric(DATA) && IsNumeric(Ans2))
                                {
                                    if (Convert.ToInt32(DATA) > Convert.ToInt32(Ans2))
                                    {
                                        Result = true;
                                    }
                                }
                            }
                            else if (Condition2 == "=>")
                            {
                                if (IsNumeric(DATA) && IsNumeric(Ans2))
                                {
                                    if (Convert.ToInt32(DATA) >= Convert.ToInt32(Ans2))
                                    {
                                        Result = true;
                                    }
                                }
                            }
                            else if (Condition2 == "<")
                            {
                                if (IsNumeric(DATA) && IsNumeric(Ans2))
                                {
                                    if (Convert.ToInt32(DATA) < Convert.ToInt32(Ans2))
                                    {
                                        Result = true;
                                    }
                                }
                            }
                            else if (Condition2 == "=<")
                            {
                                if (IsNumeric(DATA) && IsNumeric(Ans2))
                                {
                                    if (Convert.ToInt32(DATA) <= Convert.ToInt32(Ans2))
                                    {
                                        Result = true;
                                    }
                                }
                            }
                            else if (Condition2 == "[_]%")
                            {
                                if (DATA.StartsWith(Ans2))
                                {
                                    Result = true;
                                }
                            }
                            else if (Condition2 == "![_]%")
                            {
                                if (!DATA.StartsWith(Ans2))
                                {
                                    Result = true;
                                }
                            }
                            else if (Condition2 == "%_%")
                            {
                                if (DATA.Contains(Ans2))
                                {
                                    Result = true;
                                }
                            }
                            else if (Condition2 == "!%_%")
                            {
                                if (!DATA.Contains(Ans2))
                                {
                                    Result = true;
                                }
                            }
                        }
                    }

                    if (Result == true && Color != "")
                    {
                        lblData.Style.Add("color", Color);
                        lblData.Font.Bold = true;
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public bool IsNumeric(string value)
        {
            try
            {
                int number;
                bool result = int.TryParse(value, out number);
                return result;
            }
            catch (Exception ex) { return false; }
        }
    }
}