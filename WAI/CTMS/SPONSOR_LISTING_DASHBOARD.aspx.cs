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
    public partial class SPONSOR_LISTING_DASHBOARD : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    BINDLISTING();
                    GET_TILES();
                    GET_GRAPHS();
                }

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Change", "SetStatusTotals();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BINDLISTING()
        {
            try
            {
                DataSet ds = dal.MM_LISTINGS_SP(ACTION: "BINDLIST_SPONSOR");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlList.DataSource = ds;
                    ddlList.DataTextField = "NAME";
                    ddlList.DataValueField = "ID";
                    ddlList.DataBind();
                    ddlList.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.MM_LISTINGS_SP(ACTION: "BINDLISTDATAITEMS_New", LISTING_ID: ddlList.SelectedValue, USERID: Session["User_Id"].ToString());

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataSet dsMainStatus = dal.MM_LISTINGS_SP(ACTION: "GET_LIST_MainListStatus", SITE: dr["INVID"].ToString(), LISTING_ID: ddlList.SelectedValue);

                    DataRow drMainStatus = dsMainStatus.Tables[0].Rows[0];

                    //dr["OPEN"] = drMainStatus["OPEN"].ToString();
                    dr["REVIEWED"] = drMainStatus["REVIEWED"].ToString();
                    dr["PR"] = drMainStatus["PR"].ToString();
                    dr["QNR"] = drMainStatus["QNR"].ToString();
                    dr["PNR"] = drMainStatus["PNR"].ToString();
                    dr["TOTAL_QUERY"] = drMainStatus["TOTAL_QUERY"].ToString();
                    dr["OPEN_QUERY"] = drMainStatus["OPEN_QUERY"].ToString();
                    dr["CLOSE_QUERY"] = drMainStatus["CLOSE_QUERY"].ToString();
                    dr["ISSUE"] = drMainStatus["ISSUE"].ToString();
                    dr["UNEXP"] = drMainStatus["UNEXP"].ToString();
                }

                ds.Tables[0].AcceptChanges();

                if (ds.Tables.Count > 0)
                {
                    grdAdverseEvent.DataSource = ds;
                    grdAdverseEvent.DataBind();
                }
                else
                {
                    grdAdverseEvent.DataSource = null;
                    grdAdverseEvent.DataBind();
                }

                if (ddlList.SelectedIndex != 0 && ds.Tables.Count > 1)
                {
                    if (ds.Tables[1].Rows[0][0].ToString() == "True")
                    {
                        grdAdverseEvent.Columns[12].Visible = true;
                    }
                    else
                    {
                        grdAdverseEvent.Columns[12].Visible = false;
                    }
                }

                int TOTAL = 0,
                    OPEN = 0,
                    ForPeerReview = 0,
                    Reviewed = 0,
                    QueryAndReviewed = 0,
                    ReviewedwithPeerView = 0,
                    ReviewedFromAnotherListings = 0,
                    TotalQuery = 0,
                    OpenQuery = 0,
                    CloseQuery = 0,
                    Issue = 0,
                    UNEXP = 0;

                foreach (GridViewRow row in grdAdverseEvent.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblTOTAL = row.FindControl("TOTAL") as Label;
                        Label lblOPEN = row.FindControl("lblOPEN") as Label;
                        Label lblForPeerReview = row.FindControl("lblForPeerReview") as Label;
                        Label lblReviewed = row.FindControl("lblReviewed") as Label;
                        Label lblQueryAndReviewed = row.FindControl("lblQueryAndReviewed") as Label;
                        Label lblReviewedwithPeerView = row.FindControl("lblReviewedwithPeerView") as Label;
                        Label lblReviewedFromAnotherListings = row.FindControl("lblReviewedFromAnotherListings") as Label;
                        Label lblTotalQuery = row.FindControl("lblTotalQuery") as Label;
                        Label lblOpenQuery = row.FindControl("lblOpenQuery") as Label;
                        Label lblCloseQuery = row.FindControl("lblCloseQuery") as Label;
                        Label lblIssue = row.FindControl("lblIssue") as Label;
                        Label lblUNEXP = row.FindControl("lblUNEXP") as Label;

                        TOTAL += Convert.ToInt32(lblTOTAL.Text);
                        OPEN += Convert.ToInt32(lblOPEN.Text);
                        ForPeerReview += Convert.ToInt32(lblForPeerReview.Text);
                        Reviewed += Convert.ToInt32(lblReviewed.Text);
                        QueryAndReviewed += Convert.ToInt32(lblQueryAndReviewed.Text);
                        ReviewedwithPeerView += Convert.ToInt32(lblReviewedwithPeerView.Text);
                        ReviewedFromAnotherListings += Convert.ToInt32(lblReviewedFromAnotherListings.Text);
                        TotalQuery += Convert.ToInt32(lblTotalQuery.Text);
                        OpenQuery += Convert.ToInt32(lblOpenQuery.Text);
                        CloseQuery += Convert.ToInt32(lblCloseQuery.Text);
                        Issue += Convert.ToInt32(lblIssue.Text);
                        UNEXP += Convert.ToInt32(lblUNEXP.Text);

                    }
                }

                foreach (TableCell headerCell in grdAdverseEvent.HeaderRow.Cells)
                {
                    Label TOTALTotal = headerCell.FindControl("TOTALTotal") as Label;
                    Label lblOPENTotal = headerCell.FindControl("lblOPENTotal") as Label;
                    Label lblForPeerReviewTotal = headerCell.FindControl("lblForPeerReviewTotal") as Label;
                    Label lblReviewedTotal = headerCell.FindControl("lblReviewedTotal") as Label;
                    Label lblQueryAndReviewedTotal = headerCell.FindControl("lblQueryAndReviewedTotal") as Label;
                    Label lblReviewedwithPeerViewTotal = headerCell.FindControl("lblReviewedwithPeerViewTotal") as Label;
                    Label lblReviewedFromAnotherListingsTotal = headerCell.FindControl("lblReviewedFromAnotherListingsTotal") as Label;
                    Label lblTotalQueryTotal = headerCell.FindControl("lblTotalQueryTotal") as Label;
                    Label lblOpenQueryTotal = headerCell.FindControl("lblOpenQueryTotal") as Label;
                    Label lblCloseQueryTotal = headerCell.FindControl("lblCloseQueryTotal") as Label;
                    Label lblIssueTotal = headerCell.FindControl("lblIssueTotal") as Label;
                    Label lblUNEXPTotal = headerCell.FindControl("lblUNEXPTotal") as Label;

                    TOTALTotal.Text = "( " + TOTAL.ToString() + " )";
                    lblOPENTotal.Text = "( " + OPEN.ToString() + " )";
                    lblForPeerReviewTotal.Text = "( " + ForPeerReview.ToString() + " )";
                    lblReviewedTotal.Text = "( " + Reviewed.ToString() + " )";
                    lblQueryAndReviewedTotal.Text = "( " + QueryAndReviewed.ToString() + " )";
                    lblReviewedwithPeerViewTotal.Text = "( " + ReviewedwithPeerView.ToString() + " )";
                    lblReviewedFromAnotherListingsTotal.Text = "( " + ReviewedFromAnotherListings.ToString() + " )";
                    lblTotalQueryTotal.Text = "( " + TotalQuery.ToString() + " )";
                    lblOpenQueryTotal.Text = "( " + OpenQuery.ToString() + " )";
                    lblCloseQueryTotal.Text = "( " + CloseQuery.ToString() + " )";
                    lblIssueTotal.Text = "( " + Issue.ToString() + " )";
                    lblUNEXPTotal.Text = "( " + UNEXP.ToString() + " )";
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

        public int col = 0;
        protected void lstTile_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");
                Label lblCount = (Label)e.Item.FindControl("lblCount");
                int itemIndex = e.Item.DataItemIndex;

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string ID = drv["ID"].ToString();

                DataSet ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TILE", LISTING_ID: ID);

                if (ds.Tables.Count > 0)
                {
                    lblCount.Text = ds.Tables[0].Rows[0]["Count"].ToString();
                }
                else
                {
                    lblCount.Text = "0";
                }

                string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstGraph_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                int itemIndex = e.Item.DataItemIndex;
                string barinfo = "";

                HiddenField hfData = (HiddenField)e.Item.FindControl("hfData");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string ID = drv["ID"].ToString();


                DataSet ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: ID);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    barinfo += "{'INVID': '" + ds.Tables[0].Rows[i]["INVID"].ToString() + "', 'Count': " + ds.Tables[0].Rows[i]["Count"].ToString() + " },";
                }

                hfData.Value = "[" + barinfo.TrimEnd(',') + "]";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_TILES()
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(Action: "GET_LISTING_TILES_SPONSOR");
                lstTile.DataSource = ds;
                lstTile.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_GRAPHS()
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(Action: "GET_LISTING_GRAPHS_SPONSOR");
                lstGraph.DataSource = ds;
                lstGraph.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}