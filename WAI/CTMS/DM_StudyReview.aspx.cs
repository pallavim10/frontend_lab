using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_StudyReview : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_MM = new DAL_MM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", false);
                }
                else if (!IsPostBack)
                {
                    FillINV();
                    FillSubject();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void GET_PAT_REV()
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(Action: "GETLISTING_STUDY_REV", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeatData.DataSource = ds;
                    repeatData.DataBind();
                }
                else
                {
                    repeatData.DataSource = null;
                    repeatData.DataBind();
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
                DataSet ds = dal.GET_SUBJECT_SP(INVID: drpInvID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--Select--", "Select"));
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

        public void FillINV()
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.GET_INVID_SP();
            drpInvID.DataSource = ds.Tables[0];
            drpInvID.DataValueField = "INVID";
            drpInvID.DataBind();

            FillSubject();
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpIndication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void repeatData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string LISTID = row["ID"].ToString();
                    GridView gridData = (GridView)e.Item.FindControl("gridData");
                    GridView gridData_Tran = (GridView)e.Item.FindControl("gridData_Tran");

                    ViewState["TRANSPOSE"] = row["TRANSPOSE"].ToString();
                    ViewState["LISTID"] = LISTID;

                    DataSet ds1 = dal.DM_LISTINGS_SP(
                    Action: "GET_OnClick",
                    LISTING_ID: LISTID
                    );

                    ViewState["OnClickDT"] = ds1.Tables[0];

                    DataSet ds = new DataSet();
                    if (ViewState["TRANSPOSE"].ToString() == "True")
                    {
                        ds = dal_MM.MM_LIST_SP(ACTION: "GETLISTDATA_TRANSPOSE", LISTING_ID: LISTID, SUBJID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue);

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
                        ds = dal.DM_LISTINGS_SP(Action: "MM_GETLISTDATA_PatRev", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: LISTID, SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue);

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
                if (ViewState["TRANSPOSE"].ToString() != "True")
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
                //string PVID = drv["PVID"].ToString();

                if (ViewState["TRANSPOSE"].ToString() != "True")
                {
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;

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

                    DataSet dsStatusDetails = dal.DM_LISTINGS_SP(Action: "GET_LIST_StatusDetails", PVID: drv["PVID"].ToString(), RECID: drv["RECID"].ToString(), LISTING_ID: ViewState["LISTID"].ToString());

                    DataRow drStatusDetails = dsStatusDetails.Tables[0].Rows[0];

                    LinkButton lbtnForPeerReview = (LinkButton)e.Row.FindControl("lbtnForPeerReview");
                    LinkButton lbtnReview = (LinkButton)e.Row.FindControl("lbtnReview");
                    LinkButton lbtnAnotherReviewed = (LinkButton)e.Row.FindControl("lbtnAnotherReviewed");
                    Label lbtnReviewDone = (Label)e.Row.FindControl("lbtnReviewDone");
                    Label lbtnPeerReview = (Label)e.Row.FindControl("lbtnPeerReview");
                    Label lbtnReviewQuery = (Label)e.Row.FindControl("lbtnReviewQuery");
                    HtmlGenericControl divQueryCount = e.Row.FindControl("divQueryCount") as HtmlGenericControl;
                    LinkButton lbtnQueryCount = (LinkButton)e.Row.FindControl("lbtnQueryCount");
                    LinkButton lbtnUNEXP = (LinkButton)e.Row.FindControl("lbtnUNEXP");
                    HtmlControl iconComments = (HtmlControl)e.Row.FindControl("iconComments");

                    int QueryCount = Convert.ToInt32(drStatusDetails["QueryCount"]);
                    int OpenQueryCount = Convert.ToInt32(drStatusDetails["OpenQueryCount"]);
                    string ListStatus = drStatusDetails["ListStatus"].ToString();
                    string AnotherListStatus = drStatusDetails["AnotherListStatus"].ToString();
                    string UNEXP = drStatusDetails["UNEXP"].ToString();

                    if (drStatusDetails["COMMENTSCOUNT"].ToString() != "0")
                    {
                        //iconComments.Attributes.Add("color", "Red");
                        iconComments.Style.Add("color", "Red");
                        iconComments.Attributes.Add("title", "Comments (" + drStatusDetails["COMMENTSCOUNT"].ToString() + ")");
                    }

                    lbtnReview.CssClass = "disp-none";
                    lbtnAnotherReviewed.CssClass = "disp-none";
                    lbtnReviewDone.CssClass = "disp-none";
                    lbtnReviewQuery.CssClass = "disp-none";
                    lbtnPeerReview.CssClass = "disp-none";

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
                        if (ListStatus == "For Peer-Review")
                        {
                            lbtnReview.CssClass = "disp-none";
                            lbtnAnotherReviewed.CssClass = "disp-none";
                            lbtnForPeerReview.Visible = true;
                        }
                        else if (ListStatus == "Reviewed")
                        {
                            lbtnReviewDone.CssClass = "";
                        }
                        else if (ListStatus == "Reviewed with Peer View")
                        {
                            lbtnPeerReview.CssClass = "";
                        }
                        else if (ListStatus == "Query and Reviewed")
                        {
                            lbtnReviewQuery.CssClass = "";
                        }
                    }

                    if (QueryCount > 0)
                    {
                        divQueryCount.Visible = true;
                        lbtnQueryCount.Text = QueryCount.ToString();

                        if (OpenQueryCount > 0)
                        {
                            divQueryCount.Attributes["class"] = "circleQueryCountRed";
                            lbtnReview.Visible = false;
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

        protected void grd_data_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                GET_PAT_REV();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}