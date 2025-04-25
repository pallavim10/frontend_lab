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
    public partial class MM_Advance_Metrics : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    COUNTRY();
                    SITE_AGAINST_COUNTRY();
                    GETINVIDS();
                    GET_GRAPHS();
                    GET_TILES();
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
                decimal percen = 0;

                HiddenField hfData = (HiddenField)e.Item.FindControl("hfData");
                HiddenField hdnNumListId = (HiddenField)e.Item.FindControl("hdnNumListId");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string Num_List_ID = drv["Num_List_ID"].ToString();
                string Denom_List_ID = drv["Denom_List_ID"].ToString();
                hdnNumListId.Value = drv["Num_List_ID"].ToString();

                string INVID = "", COUNTRYID = "";
                if (drpSites.SelectedValue == "All")
                {
                    INVID = "0";
                }
                else
                {
                    INVID = drpSites.SelectedValue;
                }

                if (drpCountry.SelectedValue == "All")
                {
                    COUNTRYID = "0";
                }
                else
                {
                    COUNTRYID = drpCountry.SelectedValue;
                }

                DataSet dsNum = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: Num_List_ID, INVID: INVID, COUNTRYID: COUNTRYID);
                DataSet dsDenom = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: Denom_List_ID, INVID: INVID, COUNTRYID: COUNTRYID);

                decimal NumeratorTotal = 0;
                string NumeratorVal = "", DenominatorVal = "";
                if (drpSites.SelectedValue == "All")
                {
                    DataSet dtINVID = (DataSet)ViewState["USER_INVIDS"];

                    for (int i = 0; i < dsNum.Tables[0].Rows.Count; i++)
                    {
                        if (NumeratorTotal == 0)
                        {
                            NumeratorTotal = Convert.ToDecimal(dsNum.Tables[0].Rows[i]["Count"]);
                        }
                        else
                        {
                            NumeratorTotal += Convert.ToDecimal(dsNum.Tables[0].Rows[i]["Count"]);
                        }
                    }

                    for (int k = 0; k < dtINVID.Tables[0].Rows.Count; k++)
                    {
                        for (int i = 0; i < dsNum.Tables[0].Rows.Count; i++)
                        {
                            if (dtINVID.Tables[0].Rows[k]["INVID"].ToString() == dsNum.Tables[0].Rows[i]["INVID"].ToString())
                            {
                                NumeratorVal = dsNum.Tables[0].Rows[i]["Count"].ToString();

                                for (int j = 0; j < dsDenom.Tables[0].Rows.Count; j++)
                                {
                                    if (dsNum.Tables[0].Rows[i]["INVID"].ToString() == dsDenom.Tables[0].Rows[j]["INVID"].ToString())
                                    {
                                        DenominatorVal = dsDenom.Tables[0].Rows[j]["Count"].ToString();
                                        percen = ((Convert.ToDecimal(dsNum.Tables[0].Rows[i]["Count"]) / Convert.ToDecimal(dsDenom.Tables[0].Rows[j]["Count"])) * 100);
                                    }
                                }
                            }
                        }

                        decimal Mean = (NumeratorTotal / Convert.ToDecimal(dtINVID.Tables[0].Rows.Count));
                        string COUNTVALUE = "(" + NumeratorVal + "/" + DenominatorVal + ")";
                        barinfo += "{'INVID': '" + dtINVID.Tables[0].Rows[k]["INVID"].ToString() + "', 'Count': " + String.Format("{0:0.00}", percen) + ",'Counts':'" + COUNTVALUE + "','Mean':+'" + String.Format("{0:0.00}", Mean) + "'},";
                        percen = 0;
                        NumeratorVal = "0";
                        DenominatorVal = "0";
                        Mean = 0;
                    }
                }
                else
                {
                    if (dsNum.Tables[0].Rows.Count > 0)
                    {
                        if (dsDenom.Tables[0].Rows.Count > 0)
                        {
                            percen = ((Convert.ToDecimal(dsNum.Tables[0].Rows[0]["Count"]) / Convert.ToDecimal(dsDenom.Tables[0].Rows[0]["Count"])) * 100);
                        }
                        else
                        {
                            percen = 0;
                        }
                    }
                    else
                    {
                        percen = 0;
                    }

                    decimal Mean = (Convert.ToDecimal(dsNum.Tables[0].Rows[0]["Count"]) / Convert.ToDecimal(1));
                    string COUNTVALUE = "(" + dsNum.Tables[0].Rows[0]["Count"].ToString() + "/" + dsDenom.Tables[0].Rows[0]["Count"].ToString() + ")";

                    barinfo += "{'INVID': '" + drpSites.SelectedValue + "', 'Count': " + String.Format("{0:0.00}", percen) + ",'Counts': '" + COUNTVALUE + "','Mean':'" + String.Format("{0:0.00}", Mean) + "'},";
                    percen = 0;
                }


                hfData.Value = "[" + barinfo.TrimEnd(',') + "]";

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
                DataSet ds = dal.DM_LISTINGS_SP(Action: "GETADVANCEMETRICS_Graph");
                lstGraph.DataSource = ds;
                lstGraph.DataBind();
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
                DataSet ds = dal.DM_LISTINGS_SP(Action: "GETADVANCEMETRICS_Tiles");
                lstTile.DataSource = ds;
                lstTile.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GETINVIDS()
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(Action: "GETINVIDS", USERID: Session["USER_ID"].ToString(), COUNTRYID: drpCountry.SelectedValue);
                ViewState["USER_INVIDS"] = ds;
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
                GETINVIDS();
                GET_GRAPHS();
                GET_TILES();
                Session["DASHBOARD_COUNTRYID"] = drpCountry.SelectedValue;

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
                GET_GRAPHS();
                GET_TILES();
                Session["DASHBOARD_SITE"] = drpSites.SelectedValue;
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
                drpCountry.Items.Insert(0, new ListItem("--All Countries--", "All"));
                Session["DASHBOARD_COUNTRYID"] = drpCountry.SelectedValue;
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
                DataSet ds = dal.GET_INVID_SP();
                drpSites.DataSource = ds.Tables[1];
                drpSites.DataTextField = "INVID";
                drpSites.DataValueField = "INVID";
                drpSites.DataBind();
                drpSites.Items.Insert(0, new ListItem("--All Sites--", "All"));

                Session["DASHBOARD_SITE"] = drpSites.SelectedValue;
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
                Label lblCount = (Label)e.Item.FindControl("lblCount");
                int itemIndex = e.Item.DataItemIndex;
                decimal AveragePer = 0;
                DataRowView drv = (DataRowView)e.Item.DataItem;

                string Num_List_ID = drv["Num_List_ID"].ToString();
                string Denom_List_ID = drv["Denom_List_ID"].ToString();

                string INVID = "", COUNTRYID = "";
                if (drpSites.SelectedValue == "All")
                {
                    INVID = "0";
                }
                else
                {
                    INVID = drpSites.SelectedValue;
                }

                if (drpCountry.SelectedValue == "All")
                {
                    COUNTRYID = "0";
                }
                else
                {
                    COUNTRYID = drpCountry.SelectedValue;
                }

                DataSet dsNum = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: Num_List_ID, INVID: INVID, COUNTRYID: COUNTRYID);
                DataSet dsDenom = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: Denom_List_ID, INVID: INVID, COUNTRYID: COUNTRYID);

                decimal SumOfNumerator = 0;
                decimal SumOfDenominator = 0;

                if (drpSites.SelectedValue == "All")
                {
                    if (dsNum.Tables[0].Rows.Count > 0)
                    {
                        if (dsDenom.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsNum.Tables[0].Rows.Count; i++)
                            {
                                if (SumOfNumerator == 0)
                                {
                                    SumOfNumerator = Convert.ToDecimal(dsNum.Tables[0].Rows[i]["Count"]);
                                }
                                else
                                {
                                    SumOfNumerator += Convert.ToDecimal(dsNum.Tables[0].Rows[i]["Count"]);
                                }
                            }

                            for (int j = 0; j < dsDenom.Tables[0].Rows.Count; j++)
                            {
                                if (SumOfDenominator == 0)
                                {
                                    SumOfDenominator = Convert.ToDecimal(dsDenom.Tables[0].Rows[j]["Count"]);
                                }
                                else
                                {
                                    SumOfDenominator += Convert.ToDecimal(dsDenom.Tables[0].Rows[j]["Count"]);
                                }
                            }

                            AveragePer = (SumOfNumerator / SumOfDenominator) * 100;
                        }
                        else
                        {
                            AveragePer = 0;
                        }
                    }
                    else
                    {
                        AveragePer = 0;
                    }
                }
                else
                {
                    if (dsNum.Tables[0].Rows.Count > 0)
                    {
                        if (dsDenom.Tables[0].Rows.Count > 0)
                        {
                            SumOfNumerator = Convert.ToDecimal(dsNum.Tables[0].Rows[0]["Count"]);
                            SumOfDenominator = Convert.ToDecimal(dsDenom.Tables[0].Rows[0]["Count"]);

                            AveragePer = (SumOfNumerator / SumOfDenominator) * 100;
                        }
                        else
                        {
                            AveragePer = 0;
                        }
                    }
                    else
                    {
                        AveragePer = 0;
                    }
                }

                if (AveragePer != 0)
                {
                    lblCount.Text = String.Format("{0:0.00}", AveragePer) + "%";
                }
                else
                {
                    lblCount.Text = "0%";
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

        protected void btngetdata_Click(object sender, EventArgs e)
        {

        }
    }

}