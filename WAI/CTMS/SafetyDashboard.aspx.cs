using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class SafetyDashboard : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.Page.IsPostBack)
                {
                    COUNTRY();
                    SITE_AGAINST_COUNTRY();
                    GET_TILES();
                    GET_LISTING_PENDING_TILES();
                    GET_LISTING_TOTAL_TILES();
                }
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
                DataSet ds = dal.GET_COUNTRY_SP(USERID: Session["User_ID"].ToString());
                drpCountry.DataSource = ds.Tables[0];
                drpCountry.DataTextField = "COUNTRYNAME";
                drpCountry.DataValueField = "COUNTRYID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("--All Countries--", "0"));
                Session["DASHBOARD_COUNTRYID"] = drpCountry.SelectedValue;
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
                GET_TILES();
                GET_LISTING_PENDING_TILES();
                GET_LISTING_TOTAL_TILES();
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
                DataSet ds = dal.GET_INVID_SP(COUNTRYID: drpCountry.SelectedValue, USERID: Session["User_ID"].ToString());
                drpSites.DataSource = ds.Tables[0];
                drpSites.DataTextField = "INVID";
                drpSites.DataValueField = "INVID";
                drpSites.DataBind();
                drpSites.Items.Insert(0, new ListItem("--All Sites--", "0"));

                Session["DASHBOARD_SITE"] = drpSites.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_TILES();
                GET_LISTING_PENDING_TILES();
                GET_LISTING_TOTAL_TILES();
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
                DataSet ds = dal_SAE.SAE_DASHBORAD_SP(ACTION: "GET_TILES",
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue
                    );

                lstTile.DataSource = ds.Tables[0];
                lstTile.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public int col = 0;

        protected void lstTile_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");

                int itemIndex = e.Item.DataItemIndex;

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

        private void GET_LISTING_PENDING_TILES()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_DASHBORAD_SP(ACTION: "GET_LISTING_PENDING_TILES");

                lstListingTile_Pending.DataSource = ds;
                lstListingTile_Pending.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstListingTile_Pending_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");
                Label lblCount = (Label)e.Item.FindControl("lblCount");
                int itemIndex = e.Item.DataItemIndex;

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string ID = drv["ID"].ToString();

                DataSet ds = dal_SAE.SAE_DASHBORAD_SP(ACTION: "GET_LISTING_DATA_PENDING_TILES",
                    LISTING_ID: ID,
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue
                    );

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

        private void GET_LISTING_TOTAL_TILES()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_DASHBORAD_SP(ACTION: "GET_LISTING_TOTAL_TILES");

                lstListingTile_Total.DataSource = ds;
                lstListingTile_Total.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstListingTile_Total_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");
                Label lblCount = (Label)e.Item.FindControl("lblCount");
                int itemIndex = e.Item.DataItemIndex;

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string ID = drv["ID"].ToString();

                DataSet ds = dal_SAE.SAE_DASHBORAD_SP(ACTION: "GET_LISTING_DATA_TOTAL_TILES",
                    LISTING_ID: ID,
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue
                    );

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
    }
}