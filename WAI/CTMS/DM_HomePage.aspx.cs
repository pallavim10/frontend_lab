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
    public partial class DM_HomePage : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
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
                  
                    GET_BOUND_DATA();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "bindGraph", "bindGraph();", true);
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
                GET_BOUND_DATA();
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
                GET_BOUND_DATA();
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
                DataRowView drv = (DataRowView)e.Item.DataItem;
                if (drv.Row.Table.Columns.Contains("TYPE"))
                {
                    string type = drv["TYPE"].ToString();

                    if (type == "Listing Tile")
                    {
                        lstListingTile_ItemDataBound(sender, e);
                        return;
                    }
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

        private void GET_SUBJECT_STATUS_DETAILS_TILES()
        {
            try
            {
                DataSet ds = dal_DM.DM_DASHBORAD_SP(ACTION: "GET_SUBJECT_STATUS_DETAILS_TILES",
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue
                    );

               //lstSubjectStatus.DataSource = ds.Tables[0];
             //  lstSubjectStatus.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstSubjectStatus_ItemDataBound(object sender, ListViewItemEventArgs e)
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
          

        protected void lstListingTile_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");
                Label lblCount = (Label)e.Item.FindControl("lblCount");
                int itemIndex = e.Item.DataItemIndex;

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string ID = drv["ID"].ToString();

                DataSet ds = dal_DM.DM_DASHBORAD_SP(ACTION: "GETLISTDATA_TILE",
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

        protected void lstListingGraph_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                int itemIndex = e.Item.DataItemIndex;
                string barinfo = "";

                HiddenField hfData = (HiddenField)e.Item.FindControl("hfData");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string ID = drv["ID"].ToString();


                DataSet ds = dal_DM.DM_DASHBORAD_SP(ACTION: "GETLISTDATA_GRAPH",
                    LISTING_ID: ID,
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue
                    );

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

        private void GET_BOUND_DATA()
        {
            try 
            {

                DataSet ds = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "GET_DM_DASHBOARD");
             
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                 
                    repeatData.DataSource = ds.Tables[0];
                    repeatData.DataBind();
                    // Delay the graph rendering until after repeater binding
                    //ScriptManager.RegisterStartupScript(this, GetType(), "bindGraph()", "setTimeout(bindGraph(), 500);", true);
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

        protected void repeatData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    var dataItem = (DataRowView)e.Item.DataItem;

                    ListView lstTile = (ListView)e.Item.FindControl("lstTile");
                    ListView lstGraph = (ListView)e.Item.FindControl("lstListingGraph");
                    Control ListingTileDiv = e.Item.FindControl("ListingTileDiv");

                    string type = dataItem["TYPE"].ToString(); // Use the current repeater item's data

                    DataSet dts = null;                    

                    if (type == "Query Details")
                    {
                        dts = dal_DM.DM_DASHBORAD_SP(ACTION: "GET_QUERY_DETAILS_TILES",
                            CountryID: drpCountry.SelectedValue,
                            SiteID: drpSites.SelectedValue);

                        if (lstTile != null && dts != null && dts.Tables.Count > 0 && dts.Tables[0].Rows.Count > 0)
                        {
                            lstTile.DataSource = dts.Tables[0];
                            lstTile.ItemDataBound -= lstListingTile_ItemDataBound; // Remove if previously assigned
                            lstTile.ItemDataBound += lstTile_ItemDataBound; // Assign default handler
                            lstTile.DataBind();
                            lstTile.Visible = true;
                            ListingTileDiv.Visible = true;
                            lstGraph.Visible = false;
                        }
                    }
                    else if (type == "Listing Tile")
                    {
                      dts = dal_DM.DM_DASHBORAD_SP(ACTION: "GET_LISTING_TILES");

                        if (dts != null && dts.Tables.Count > 0 && dts.Tables[0].Rows.Count > 0)
                        {
                            //  If "Count" column does NOT exist, add it manually
                            if (!dts.Tables[0].Columns.Contains("Count"))
                            {
                                dts.Tables[0].Columns.Add("Count", typeof(int));  // Add column
                                foreach (DataRow row in dts.Tables[0].Rows)
                                {
                                    row["Count"] = 0;  // Set default value to 0
                                }
                            }

                            if (!dts.Tables[0].Columns.Contains("TILE"))
                            {
                                dts.Tables[0].Columns.Add("TILE", typeof(string)); // Add TILE column
                                foreach (DataRow row in dts.Tables[0].Rows)
                                {
                                    row["TILE"] = row["NAME"].ToString(); // Set TILE to empty for Listing Tile
                                }
                            }

                            lstTile.DataSource = dts.Tables[0];
                            lstTile.ItemDataBound -= lstTile_ItemDataBound; // Remove default handler
                            lstTile.ItemDataBound += lstListingTile_ItemDataBound; // Add new handler
                            lstTile.DataBind();
                            lstTile.Visible = true;
                            ListingTileDiv.Visible = true;
                            lstGraph.Visible = false;
                        }
                    }
                    else if (type == "Subject Module Status Details")
                    {
                        dts = dal_DM.DM_DASHBORAD_SP(ACTION: "GET_SUBJECT_STATUS_DETAILS_TILES",
                            CountryID: drpCountry.SelectedValue,
                            SiteID: drpSites.SelectedValue);

                        if (lstTile != null && dts != null && dts.Tables.Count > 0 && dts.Tables[0].Rows.Count > 0)
                        {
                            lstTile.DataSource = dts.Tables[0];
                            lstTile.ItemDataBound -= lstListingTile_ItemDataBound; // Remove if previously assigned
                            lstTile.ItemDataBound += lstTile_ItemDataBound; // Assign default handler
                            lstTile.DataBind();
                            lstTile.Visible = true;
                            ListingTileDiv.Visible = true;
                            lstGraph.Visible = false;
                        }
                    }
                    else if (type == "Listing Graph")
                    {
                      DataSet dtsgrp = dal_DM.DM_DASHBORAD_SP(ACTION: "GET_LISTING_GRAPHS");
                        if (lstGraph != null)
                        {
                            lstGraph.DataSource = dtsgrp.Tables[0];
                            lstGraph.DataBind();
                            lstTile.Visible = false;
                            ListingTileDiv.Visible = false;
                            lstGraph.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}