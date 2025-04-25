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
    public partial class SPONSOR_LISTING_DATA : System.Web.UI.Page
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
                    DataSet ds = dal.DM_LISTINGS_SP(Action: "GETLISTINGBY_ID", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString(), ID: Request.QueryString["LISTID"].ToString());
                    hdnlistid.Value = Request.QueryString["LISTID"].ToString();
                    lblHeader.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                    hdnPrimMODULENAME.Value = ds.Tables[0].Rows[0]["PrimaryMODULENAME"].ToString();

                    if (ds.Tables[0].Rows[0]["TRANSPOSE"].ToString() == "True")
                    {
                        hdntranspose.Value = "VisitNameVise";
                    }
                    else
                    {
                        hdntranspose.Value = "FieldNameVise";
                    }

                    GET_OnClick();
                    COUNTRY();
                    SITE_AGAINST_COUNTRY();
                    GetIndication();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GET_OnClick()
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(
                Action: "GET_OnClick",
                LISTING_ID: Request.QueryString["LISTID"].ToString()
                );

                ViewState["OnClickDT"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void COUNTRY()
        {
            try
            {
                DataSet ds = dal.GetSiteID(Action: "GET_COUNTRYID_AND_INVID", User_ID: Session["User_ID"].ToString());
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

        protected void SITE_AGAINST_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GetSiteID(Action: "GET_COUNTRYID_AND_INVID", Country_ID: drpCountry.SelectedValue, User_ID: Session["User_ID"].ToString());
                drpInvID.DataSource = ds.Tables[1];
                drpInvID.DataTextField = "INVID";
                drpInvID.DataValueField = "INVID";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetIndication()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_INDICATION", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpIndication.DataSource = ds.Tables[0];
                    drpIndication.DataValueField = "ID";
                    drpIndication.DataTextField = "INDICATION";
                    drpIndication.DataBind();
                    drpIndication.Items.Insert(0, new ListItem("--ALL--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void drpIndication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                lnktranspose.Visible = false;
                //BINDSTATUSDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal.getSetDM(Action: "GET_PATIENT_REG", INVID: drpInvID.SelectedValue, VERSIONID: drpIndication.SelectedValue);
                drpSubID.DataSource = ds.Tables[0];
                drpSubID.DataValueField = "SUBJID";
                drpSubID.DataBind();
                drpSubID.Items.Insert(0, new ListItem("--All--", "0"));

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
                lnktranspose.Visible = true;
                DataSet ds = new DataSet();
                if (hdntranspose.Value == "VisitNameVise")
                {
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TRANSPOSE", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, STATUS: "0", COUNTRYID: drpCountry.SelectedValue);

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

                    gridData_Tran.Visible = true;
                    gridData.Visible = false;
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(Action: "SPONSOR_GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, COUNTRYID: drpCountry.SelectedValue);

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

                    gridData_Tran.Visible = false;
                    gridData.Visible = true;
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
                if (hdntranspose.Value == "FieldNameVise")
                {
                    hdntranspose.Value = "VisitNameVise";
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TRANSPOSE", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, STATUS: "0");

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

                    gridData_Tran.Visible = true;
                    gridData.Visible = false;
                }
                else
                {
                    hdntranspose.Value = "FieldNameVise";
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);

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

                    gridData_Tran.Visible = false;
                    gridData.Visible = true;
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
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, STATUS: "0");
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TRANSPOSE", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, STATUS: "0");
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
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, STATUS: "0");
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TRANSPOSE", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, STATUS: "0");
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
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, STATUS: "0");
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TRANSPOSE", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, STATUS: "0");
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

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

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
                    if (dc.ColumnName != "Subject" && dc.ColumnName != "VISIT")
                    {
                        if (columns.Contains(inputTable.Rows[i]["VISIT"].ToString()))
                        {
                            DataRow drNew = outputTable.NewRow();
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
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;

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
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;

                DataRowView drv = e.Row.DataItem as DataRowView;

                if (hdntranspose.Value == "FieldNameVise")
                {
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

                    DataSet dsStatusDetails = dal.DM_LISTINGS_SP(Action: "GET_LIST_StatusDetails_Sponsor", PVID: drv["PVID"].ToString(), RECID: drv["RECID"].ToString(), LISTING_ID: hdnlistid.Value);

                    DataRow drStatusDetails = dsStatusDetails.Tables[0].Rows[0];

                    HtmlControl iconComments = (HtmlControl)e.Row.FindControl("iconComments");
                    LinkButton lbtnAnotherReviewed = (LinkButton)e.Row.FindControl("lbtnAnotherReviewed");
                    LinkButton lbtnReviewPatientRev = (LinkButton)e.Row.FindControl("lbtnReviewPatientRev");
                    Label lbtnReviewDone = (Label)e.Row.FindControl("lbtnReviewDone");
                    Label lbtnPeerReview = (Label)e.Row.FindControl("lbtnPeerReview");
                    Label lbtnReviewQuery = (Label)e.Row.FindControl("lbtnReviewQuery");
                    HtmlGenericControl divQueryCount = e.Row.FindControl("divQueryCount") as HtmlGenericControl;
                    LinkButton lbtnQueryCount = (LinkButton)e.Row.FindControl("lbtnQueryCount");

                    int QueryCount = Convert.ToInt32(drStatusDetails["QueryCount"]);
                    int OpenQueryCount = Convert.ToInt32(drStatusDetails["OpenQueryCount"]);
                    string ListStatus = drStatusDetails["ListStatus"].ToString();
                    string AnotherListStatus = drStatusDetails["AnotherListStatus"].ToString();

                    if (drStatusDetails["COMMENTSCOUNT"].ToString() != "0")
                    {
                        //iconComments.Attributes.Add("color", "Red");
                        iconComments.Style.Add("color", "Red");
                        iconComments.Attributes.Add("title", "Comments (" + drStatusDetails["COMMENTSCOUNT"].ToString() + ")");
                    }

                    lbtnAnotherReviewed.CssClass = "disp-none";
                    lbtnReviewDone.CssClass = "disp-none";
                    lbtnReviewQuery.CssClass = "disp-none";
                    lbtnPeerReview.CssClass = "disp-none";
                    lbtnReviewPatientRev.CssClass = "disp-none";

                    if (ListStatus == "" && AnotherListStatus != "")
                    {
                        lbtnAnotherReviewed.CssClass = "";
                    }
                    else
                    {
                        if (ListStatus == "For Peer-Review")
                        {
                            lbtnAnotherReviewed.CssClass = "disp-none";
                            //   lbtnForPeerReview.Visible = true;
                        }
                        else if (ListStatus == "Reviewed")
                        {
                            lbtnReviewDone.CssClass = "";
                        }
                        else if (ListStatus == "Reviewed with Peer View")
                        {
                            lbtnPeerReview.CssClass = "";
                        }
                        else if (ListStatus == "Query and Reviewed" || ListStatus == "Query and Reviewed from Patient Review")
                        {
                            lbtnReviewQuery.CssClass = "";
                        }
                        else if (ListStatus == "Reviewed from Patient Review")
                        {
                            lbtnReviewPatientRev.CssClass = "";
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
                    }
                }
            }
        }
    }
}