using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class MM_LISTING_DASHBOARD : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_MM = new DAL_MM();
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
                DataSet ds = dal_MM.MM_DASH_SP(ACTION: "BINDLIST");
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
                DataSet ds = dal_MM.MM_DASH_SP(ACTION: "GETLISTDATA_COUNTS", LISTING_ID: ddlList.SelectedValue);

                if (ds.Tables.Count > 0)
                {
                    grdStatus.DataSource = ds;
                    grdStatus.DataBind();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int TOTAL = 0,
                        OPEN = 0,
                        PRIM_Reviewed = 0,
                        SECOND_Reviewed = 0,
                        ReviewedFromAnotherListings = 0,
                        TotalQuery = 0,
                        OpenQuery = 0,
                        CloseQuery = 0;

                        foreach (GridViewRow row in grdStatus.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                LinkButton lblTOTAL = row.FindControl("lblTOTAL") as LinkButton;
                                LinkButton lblOPEN = row.FindControl("lblOPEN") as LinkButton;

                                LinkButton lblPrimaryReviewed = row.FindControl("lblPrimaryReviewed") as LinkButton;
                                LinkButton lblSecondaryReviewed = row.FindControl("lblSecondaryReviewed") as LinkButton;
                                LinkButton lblReviewedFromAnotherListings = row.FindControl("lblReviewedFromAnotherListings") as LinkButton;

                                LinkButton lblTotalQuery = row.FindControl("lblTotalQuery") as LinkButton;
                                LinkButton lblOpenQuery = row.FindControl("lblOpenQuery") as LinkButton;
                                LinkButton lblCloseQuery = row.FindControl("lblCloseQuery") as LinkButton;

                                TOTAL += Convert.ToInt32(lblTOTAL.Text);
                                OPEN += Convert.ToInt32(lblOPEN.Text);

                                PRIM_Reviewed += Convert.ToInt32(lblPrimaryReviewed.Text);
                                SECOND_Reviewed += Convert.ToInt32(lblSecondaryReviewed.Text);
                                ReviewedFromAnotherListings += Convert.ToInt32(lblReviewedFromAnotherListings.Text);

                                TotalQuery += Convert.ToInt32(lblTotalQuery.Text);
                                OpenQuery += Convert.ToInt32(lblOpenQuery.Text);
                                CloseQuery += Convert.ToInt32(lblCloseQuery.Text);

                            }
                        }

                        foreach (TableCell headerCell in grdStatus.HeaderRow.Cells)
                        {
                            Label TOTALTotal = headerCell.FindControl("TOTALTotal") as Label;
                            Label lblOPENTotal = headerCell.FindControl("lblOPENTotal") as Label;

                            Label lblPrimaryReviewedTotal = headerCell.FindControl("lblPrimaryReviewedTotal") as Label;
                            Label lblSecondaryReviewedTotal = headerCell.FindControl("lblSecondaryReviewedTotal") as Label;
                            Label lblReviewedFromAnotherListingsTotal = headerCell.FindControl("lblReviewedFromAnotherListingsTotal") as Label;

                            Label lblTotalQueryTotal = headerCell.FindControl("lblTotalQueryTotal") as Label;
                            Label lblOpenQueryTotal = headerCell.FindControl("lblOpenQueryTotal") as Label;
                            Label lblCloseQueryTotal = headerCell.FindControl("lblCloseQueryTotal") as Label;

                            TOTALTotal.Text = "( " + TOTAL.ToString() + " )";
                            lblOPENTotal.Text = "( " + OPEN.ToString() + " )";

                            lblPrimaryReviewedTotal.Text = "( " + PRIM_Reviewed.ToString() + " )";
                            lblSecondaryReviewedTotal.Text = "( " + SECOND_Reviewed.ToString() + " )";
                            lblReviewedFromAnotherListingsTotal.Text = "( " + ReviewedFromAnotherListings.ToString() + " )";

                            lblTotalQueryTotal.Text = "( " + TotalQuery.ToString() + " )";
                            lblOpenQueryTotal.Text = "( " + OpenQuery.ToString() + " )";
                            lblCloseQueryTotal.Text = "( " + CloseQuery.ToString() + " )";
                        }
                    }
                }
                else
                {
                    grdStatus.DataSource = null;
                    grdStatus.DataBind();
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

                DataSet ds = dal_MM.MM_DASH_SP(ACTION: "GETLISTDATA_TILE", LISTING_ID: ID);

                if (ds.Tables.Count > 0)
                {
                    lblCount.Text = ds.Tables[0].Rows[0]["COUNTS"].ToString();
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


                DataSet ds = dal_MM.MM_DASH_SP(ACTION: "GETLISTDATA_GRAPH", LISTING_ID: ID);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    barinfo += "{'INVID': '" + ds.Tables[0].Rows[i]["INVID"].ToString() + "', 'Count': " + ds.Tables[0].Rows[i]["COUNTS"].ToString() + " },";
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
                DataSet ds = dal_MM.MM_DASH_SP(ACTION: "GET_LISTING_TILES");
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
                DataSet ds = dal_MM.MM_DASH_SP(ACTION: "GET_LISTING_GRAPHS");
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