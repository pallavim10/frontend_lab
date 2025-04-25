using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class SPONSOR_LISTING_DATA_SUBJECT : System.Web.UI.Page
    {
        DAL dal = new DAL();
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
                    DataSet ds = dal.DM_LISTINGS_SP(Action: "GETLISTINGBY_ID", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString(), ID: Request.QueryString["LISTING_ID"].ToString());

                    hdnValue.Value = Request.QueryString["VALUE"].ToString();
                    hdnlistid.Value = Request.QueryString["LISTING_ID"].ToString();
                    hdnSUBJID.Value = Request.QueryString["SUBJID"].ToString();
                    hdnPREV_LISTID.Value = Request.QueryString["PREV_LISTID"].ToString();
                    hdnFIELDID.Value = "Subject";
                    hdnPrimMODULENAME.Value = ds.Tables[0].Rows[0]["PrimaryMODULENAME"].ToString();
                    hdnUNEXP.Value = ds.Tables[0].Rows[0]["UNEXP"].ToString();
                    hdnPrimMODULEID.Value = ds.Tables[0].Rows[0]["PrimaryMODULE"].ToString();

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
                    GET_DATA();
                    GET_Other_Listings();
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
                DataSet ds = dal.DM_LISTINGS_SP(
                Action: "GET_Other_Listings",
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
                DataSet ds = dal.DM_LISTINGS_SP(
                Action: "GET_OnClick",
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
                lnktranspose.Visible = true;
                DataSet ds = new DataSet();
                if (hdntranspose.Value == "VisitNameVise")
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA_TRANSPOSE_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
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
                    ds = dal.DM_LISTINGS_SP(
                Action: "MM_GETLISTDATA_SUBJECT",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                OnClickFilter: Request.QueryString["VALUE"].ToString(),
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
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

                DataSet ds = dal.DM_LISTINGS_SP(Action: "GET_FORM_DETAILS", PVID: Request.QueryString["PVID"].ToString(), RECID: Request.QueryString["RECID"].ToString(), LISTING_ID: Request.QueryString["PREV_LISTID"].ToString());

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
                        DataSet dsColor = dal.DM_LISTINGS_SP(Action: "GET_COLOR_CODE", LISTING_ID: Request.QueryString["PREV_LISTID"].ToString(), FIELDNAME: dc.ColumnName.ToString());

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
                    if (Request.QueryString["TYPE"] != null)
                    {
                        if (Request.QueryString["TYPE"].ToString() != "")
                        {
                            e.Row.Cells[0].CssClass = e.Row.Cells[0].CssClass + " disp-none";
                        }
                    }
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[11].Visible = false;
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
                //string PVID = drv["PVID"].ToString();

                if (hdntranspose.Value == "FieldNameVise")
                {
                    if (Request.QueryString["TYPE"] != null)
                    {
                        if (Request.QueryString["TYPE"].ToString() != "")
                        {
                            e.Row.Cells[0].CssClass = e.Row.Cells[0].CssClass + " disp-none";
                        }
                    }
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[11].Visible = false;

                    DataTable OnClickDT = (DataTable)ViewState["OnClickDT"];

                    foreach (DataRow dr in OnClickDT.Rows)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            if (gridDataOnClick[i].ToString() == dr["FIELDNAME"].ToString())
                            {
                                e.Row.Cells[i].Attributes.Add("onclick", dr["OnClick"].ToString());
                                e.Row.Cells[i].CssClass = e.Row.Cells[i].CssClass + " fontBlue";
                            }
                        }
                    }

                    HtmlControl iconComments = (HtmlControl)e.Row.FindControl("iconComments");
                    LinkButton lbtnAnotherReviewed = (LinkButton)e.Row.FindControl("lbtnAnotherReviewed");
                    LinkButton lbtnReviewPatientRev = (LinkButton)e.Row.FindControl("lbtnReviewPatientRev");
                    Label lbtnReviewDone = (Label)e.Row.FindControl("lbtnReviewDone");
                    Label lbtnPeerReview = (Label)e.Row.FindControl("lbtnPeerReview");
                    Label lbtnReviewQuery = (Label)e.Row.FindControl("lbtnReviewQuery");
                    HtmlGenericControl divQueryCount = e.Row.FindControl("divQueryCount") as HtmlGenericControl;
                    LinkButton lbtnQueryCount = (LinkButton)e.Row.FindControl("lbtnQueryCount");

                    int QueryCount = Convert.ToInt32(drv["QueryCount"]);
                    int OpenQueryCount = Convert.ToInt32(drv["OpenQueryCount"]);
                    string ListStatus = drv["ListStatus"].ToString();
                    string AnotherListStatus = drv["AnotherListStatus"].ToString();

                    if (drv["COMMENTSCOUNT"].ToString() != "0")
                    {
                        //iconComments.Attributes.Add("color", "Red");
                        iconComments.Style.Add("color", "Red");
                        iconComments.Attributes.Add("title", "Comments (" + drv["COMMENTSCOUNT"].ToString() + ")");
                    }

                    lbtnAnotherReviewed.Visible = false;
                    lbtnReviewDone.Visible = false;
                    lbtnReviewQuery.Visible = false;
                    lbtnPeerReview.Visible = false;
                    lbtnReviewPatientRev.Visible = false;

                    if (ListStatus == "" && AnotherListStatus != "")
                    {
                        lbtnAnotherReviewed.Visible = true;
                    }
                    else
                    {
                        if (ListStatus == "For Peer-Review")
                        {
                            // lbtnReview.Visible = false;
                            lbtnAnotherReviewed.Visible = false;
                            //lbtnForPeerReview.Visible = true;
                        }
                        else if (ListStatus == "Reviewed")
                        {
                            lbtnReviewDone.Visible = true;
                        }
                        else if (ListStatus == "Reviewed with Peer View")
                        {
                            lbtnPeerReview.Visible = true;
                        }
                        else if (ListStatus == "Query and Reviewed" || ListStatus == "Query and Reviewed from Patient Review")
                        {
                            lbtnReviewQuery.Visible = true;
                        }
                        else if (ListStatus == "Reviewed from Patient Review")
                        {
                            lbtnReviewPatientRev.Visible = true;
                        }
                    }

                    if (QueryCount > 0)
                    {
                        divQueryCount.Visible = true;
                        lbtnQueryCount.Text = QueryCount.ToString();

                        if (OpenQueryCount > 0)
                        {
                            divQueryCount.Attributes["class"] = "circleQueryCountRed";
                            //lbtnReview.Visible = false;
                            lbtnAnotherReviewed.Visible = false;
                        }
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

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gridData.Rows[rowIndex];

                string PVID = row.Cells[3].Text;
                string RECID = row.Cells[4].Text;
                string SUBJID = row.Cells[11].Text;
                //string SOURCE = "";
                string SOURCE = hdnPrimMODULENAME.Value;

                if (e.CommandName == "Review")
                {
                    dal.MM_LISTINGS_SP(
                    ACTION: "REVEIW",
                    PROJECT: Session["PROJECTID"].ToString(),
                    SOURCE: SOURCE,
                    PVID: PVID,
                    RECID: RECID,
                    LISTING_ID: hdnlistid.Value,
                    SUBJID: SUBJID,
                    DEPT: "Medical",
                    USERID: Session["USER_ID"].ToString()
                    );
                }
                else if (e.CommandName == "AutoQuery")
                {
                    dal.MM_LISTINGS_SP(
                    ACTION: "RAISE_AUTO_QUERY",
                    PROJECT: Session["PROJECTID"].ToString(),
                    SOURCE: SOURCE,
                    PVID: PVID,
                    RECID: RECID,
                    LISTING_ID: hdnlistid.Value,
                    SUBJID: SUBJID,
                    QUERYTEXT: "Auto Raised Query",
                    DEPT: "Medical",
                    USERID: Session["USER_ID"].ToString()
                       );
                }
                else if (e.CommandName == "UNEXP")
                {
                    dal.MM_LISTINGS_SP(
                    ACTION: "UNEXPECTED_EVENT",
                    PROJECT: Session["PROJECTID"].ToString(),
                    SOURCE: SOURCE,
                    PVID: PVID,
                    RECID: RECID,
                    LISTING_ID: hdnlistid.Value,
                    SUBJID: SUBJID,
                    USERID: Session["USER_ID"].ToString()
                       );
                }
                else if (e.CommandName == "EXP")
                {
                    dal.MM_LISTINGS_SP(
                    ACTION: "EXPECTED_EVENT",
                    PROJECT: Session["PROJECTID"].ToString(),
                    SOURCE: SOURCE,
                    PVID: PVID,
                    RECID: RECID,
                    LISTING_ID: hdnlistid.Value,
                    SUBJID: SUBJID,
                    USERID: Session["USER_ID"].ToString()
                       );
                }

                GET_DATA();

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
                if (hdntranspose.Value == "FieldNameVise")
                {
                    hdntranspose.Value = "VisitNameVise";
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA_TRANSPOSE_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
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
                    ds = dal.DM_LISTINGS_SP(
                Action: "MM_GETLISTDATA_SUBJECT",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                OnClickFilter: Request.QueryString["VALUE"].ToString(),
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
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
                dal.DM_LISTINGS_SP(Action: "INSERT_USER_REPORT_DOWNLOAD_LOGS", USERID: Session["User_Id"].ToString(), LISTING_NAME: lblHeader.Text);

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
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                OnClickFilter: Request.QueryString["VALUE"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString(),
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                FIELDID: Request.QueryString["FIELDID"].ToString()
                );
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA_TRANSPOSE_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
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
                dal.DM_LISTINGS_SP(Action: "INSERT_USER_REPORT_DOWNLOAD_LOGS", USERID: Session["User_Id"].ToString(), LISTING_NAME: lblHeader.Text);

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
                dal.DM_LISTINGS_SP(Action: "INSERT_USER_REPORT_DOWNLOAD_LOGS", USERID: Session["User_Id"].ToString(), LISTING_NAME: lblHeader.Text);

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
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                OnClickFilter: Request.QueryString["VALUE"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString(),
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                FIELDID: Request.QueryString["FIELDID"].ToString()
                );
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA_TRANSPOSE_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
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
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                OnClickFilter: Request.QueryString["VALUE"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString(),
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                FIELDID: Request.QueryString["FIELDID"].ToString()
                );
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA_TRANSPOSE_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
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