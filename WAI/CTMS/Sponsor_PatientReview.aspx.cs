using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class Sponsor_PatientReview : System.Web.UI.Page
    {
        DAL dal = new DAL();

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
                    GetIndication();
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
                DataSet ds = dal.DM_LISTINGS_SP(Action: "SPONSOR_GETLISTING_PAT_REV", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString(), SUBJECTID: drpSubID.SelectedValue);

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
                DataSet ds = dal.getSetDM(Action: "GET_PATIENT_REG", INVID: drpInvID.SelectedValue, VERSIONID: drpIndication.SelectedValue);
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

        public void GetIndication()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_INDICATION", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count == 1)
                {
                    drpIndication.DataSource = ds.Tables[0];
                    drpIndication.DataValueField = "ID";
                    drpIndication.DataTextField = "INDICATION";
                    drpIndication.DataBind();
                }
                else if (ds.Tables[0].Rows.Count > 1)
                {
                    drpIndication.DataSource = ds.Tables[0];
                    drpIndication.DataValueField = "ID";
                    drpIndication.DataTextField = "INDICATION";
                    drpIndication.DataBind();
                    drpIndication.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillINV()
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.GetSiteID(
            Action: "INVID",
            PROJECTID: Session["PROJECTID"].ToString(),
            User_ID: Session["User_ID"].ToString()
            );
            drpInvID.DataSource = ds.Tables[0];
            drpInvID.DataValueField = "INVNAME";
            drpInvID.DataBind();

            FillSubject();
            if (Session["UserGroup_ID"].ToString() == "Investigator" || Session["UserGroup_ID"].ToString() == "Co_Investigator")
            {
                string Userid = Session["User_ID"].ToString();
                DivINV.Visible = false;
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
                    string ITTCOUNT = row["ITTCOUNT"].ToString();
                    string PPPCOUNT = row["PPPCOUNT"].ToString();
                    string ITT = row["ITT"].ToString();
                    string PPP = row["PPP"].ToString();
                    GridView gridData = (GridView)e.Item.FindControl("gridData");
                    GridView gridData_Tran = (GridView)e.Item.FindControl("gridData_Tran");

                    ViewState["TRANSPOSE"] = row["TRANSPOSE"].ToString();
                    ViewState["LISTID"] = LISTID;

                    LinkButton lbtnITTChecked = (LinkButton)e.Item.FindControl("lbtnITTChecked");
                    LinkButton lbtnITTUncheck = (LinkButton)e.Item.FindControl("lbtnITTUncheck");
                    Label lblITT = (Label)e.Item.FindControl("lblITT");

                    LinkButton lbtnPPPChecked = (LinkButton)e.Item.FindControl("lbtnPPPChecked");
                    LinkButton lbtnPPPUncheck = (LinkButton)e.Item.FindControl("lbtnPPPUncheck");
                    Label lblPPP = (Label)e.Item.FindControl("lblPPP");

                    if (ITT == "True")
                    {
                        if (ITTCOUNT == "1")
                        {
                            lbtnITTChecked.Visible = true;
                            lblITT.Visible = true;
                        }
                        else
                        {
                            lbtnITTUncheck.Visible = true;
                            lblITT.Visible = true;
                        }
                    }
                    else
                    {
                        lbtnITTUncheck.Visible = false;
                        lblITT.Visible = false;
                    }

                    if (PPP == "True")
                    {
                        if (PPPCOUNT == "1")
                        {
                            lbtnPPPChecked.Visible = true;
                            lblPPP.Visible = true;
                        }
                        else
                        {
                            lbtnPPPUncheck.Visible = true;
                            lblPPP.Visible = true;
                        }
                    }
                    else
                    {
                        lbtnPPPUncheck.Visible = false;
                        lblPPP.Visible = false;
                    }

                    DataSet ds1 = dal.DM_LISTINGS_SP(
                    Action: "GET_OnClick",
                    LISTING_ID: LISTID
                    );

                    ViewState["OnClickDT"] = ds1.Tables[0];

                    DataSet ds = new DataSet();
                    if (ViewState["TRANSPOSE"].ToString() == "True")
                    {
                        ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TRANSPOSE", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: LISTID, SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);

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
                        ds = dal.DM_LISTINGS_SP(Action: "MM_GETLISTDATA_PatRev", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: LISTID, SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);

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
                //string PVID = drv["PVID"].ToString();

                if (ViewState["TRANSPOSE"].ToString() != "True")
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

                    DataSet dsStatusDetails = dal.DM_LISTINGS_SP(Action: "GET_LIST_StatusDetails", PVID: drv["PVID"].ToString(), RECID: drv["RECID"].ToString(), LISTING_ID: ViewState["LISTID"].ToString());

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

        protected void repeatData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string PRIMARY_MODULEID = e.CommandArgument.ToString();

                string LISTID = (e.Item.FindControl("hdnListID") as HiddenField).Value;

                if (e.CommandName == "AnalytITT")
                {
                    dal.ANALYT_SUBJECTS(ACTION: "INSERT_ANALYT_SUBJECT_ITT",
                    SUBJID: drpSubID.SelectedValue,
                    MODULEID: PRIMARY_MODULEID,
                    LISTINGID: LISTID,
                    USERID: Session["User_ID"].ToString()
                    );
                }
                else if (e.CommandName == "AnalytPPP")
                {
                    dal.ANALYT_SUBJECTS(ACTION: "INSERT_ANALYT_SUBJECT_PPP",
                    SUBJID: drpSubID.SelectedValue,
                    MODULEID: PRIMARY_MODULEID,
                    LISTINGID: LISTID,
                    USERID: Session["User_ID"].ToString()
                    );
                }

                GET_PAT_REV();
            }
            catch (Exception ex)
            {

            }
        }
    }
}