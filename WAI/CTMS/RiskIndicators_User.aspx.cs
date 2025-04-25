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
    public partial class RiskIndicators_User : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    COUNTRY();
                    SITE_AGAINST_COUNTRY();
                    Get_Dashboard();
                    Get_Alerts();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Alerts()
        {
            try
            {
                DataSet dsAlerts = dal.Risk_Indicator_SP(Action: "Get_Alerts");
                hfGood.Value = dsAlerts.Tables[0].Rows[0]["Alert"].ToString();
                hfBad.Value = dsAlerts.Tables[0].Rows[1]["Alert"].ToString();
                hfWorst.Value = dsAlerts.Tables[0].Rows[2]["Alert"].ToString();

                DataSet dsProject = dal.Dashboard_SP(
                Action: "Triggered_Indicators_User",
                User_ID: Session["User_ID"].ToString()
                );
                gvRiskIndic.DataSource = dsProject;
                gvRiskIndic.DataBind();

                DataSet dsCountry = dal.Dashboard_SP(
                Action: "Get_Alerts_Country",
                User_ID: Session["User_ID"].ToString(),
                COUNTRYID: drpCountry.SelectedValue
                );
                gvCountryAlerts.DataSource = dsCountry;
                gvCountryAlerts.DataBind();

                DataSet dsSite = dal.Dashboard_SP(
                Action: "Get_Alerts_Sites",
                User_ID: Session["User_ID"].ToString(),
                INVID: drpSites.SelectedValue,
                COUNTRYID: drpCountry.SelectedValue
                );
                gvInvAlerts.DataSource = dsSite;
                gvInvAlerts.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Dashboard()
        {
            try
            {
                DataSet ds1 = dal.Risk_Indicator_SP(Action: "GET_Risk_Indicator_Tiles_User", Result: Session["User_ID"].ToString());

                repeatTiles.DataSource = ds1.Tables[0];
                repeatTiles.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public int col = 0;
        protected void repeatTiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string TileName = row["TileName"].ToString();
                    string Method = row["Method"].ToString();
                    string LISTID = row["LISTID"].ToString();
                    string X = row["X"].ToString();
                    string Y = row["Y"].ToString();
                    string Width = row["Width"].ToString();
                    string Height = row["Height"].ToString();
                    string ID = row["ID"].ToString();

                    decimal AveragePer = 0;
                    decimal SumOfNumerator = 0;
                    decimal SumOfDenominator = 0;

                    HtmlGenericControl divBox = (HtmlGenericControl)e.Item.FindControl("divBox");
                    HtmlGenericControl divMain = e.Item.FindControl("divMain") as HtmlGenericControl;
                    Label lblVal = e.Item.FindControl("lblVal") as Label;
                    Label lblName = e.Item.FindControl("lblName") as Label;
                    Label lblScore = e.Item.FindControl("lblScore") as Label;
                    HiddenField hfIndicID = e.Item.FindControl("hfIndicID") as HiddenField;


                    int itemIndex = e.Item.ItemIndex;
                    string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;

                    if (X != null && X.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-x");
                        divMain.Attributes.Add("data-gs-x", X);
                    }
                    if (Y != null && Y.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-y");
                        divMain.Attributes.Add("data-gs-y", Y);
                    }
                    if (Width != null && Width.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-width");
                        divMain.Attributes.Add("data-gs-width", Width);
                    }
                    if (Height != null && Height.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-height");
                        divMain.Attributes.Add("data-gs-height", Height);
                    }

                    hfIndicID.Value = ID;

                    lblName.Text = TileName;

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    if (LISTID != "")
                    {
                        if (Method == "Advance Metrics")
                        {
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

                            DataSet dsIDS = dal.DM_LISTINGS_SP(Action: "GETADVANCEMETRICS", ID: LISTID);

                            if (dsIDS.Tables[0].Rows.Count > 0)
                            {
                                DataSet dsNum = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: dsIDS.Tables[0].Rows[0]["Num_List_ID"].ToString(), INVID: INVID, COUNTRYID: COUNTRYID);
                                DataSet dsDenom = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: dsIDS.Tables[0].Rows[0]["Denom_List_ID"].ToString(), INVID: INVID, COUNTRYID: COUNTRYID);

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
                            }
                        }
                        else
                        {
                            ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TILE", LISTING_ID: LISTID, INVID: Session["DASHBOARD_SITE"].ToString(), COUNTRYID: Session["DASHBOARD_COUNTRYID"].ToString());
                        }
                    }
                    else
                    {
                        ds = dal.Dashboard_SP(Action: Method, Project_ID: Session["PROJECTID"].ToString(), INVID: Session["DASHBOARD_SITE"].ToString(),
                    COUNTRYID: Session["DASHBOARD_COUNTRYID"].ToString(), User_ID: Session["User_ID"].ToString());
                    }

                    if (Method != "Advance Metrics")
                    {
                        if (ds.Tables.Count != 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                lblVal.Text = dt.Rows[0][0].ToString();
                            }
                            else
                            {
                                lblVal.Text = "0";
                            }
                        }
                        else
                        {
                            lblVal.Text = "0";
                        }
                    }
                    else
                    {
                        lblVal.Text = String.Format("{0:0.00}", AveragePer);
                    }

                    if (lblVal.Text != "")
                    {
                        decimal value = Convert.ToDecimal(lblVal.Text);

                        if (Session["DASHBOARD_SITE"].ToString() != "All")
                        {
                            if (row["InvL1"].ToString() != "" && row["InvL2"].ToString() != "")
                            {
                                decimal InvL1 = Convert.ToDecimal(row["InvL1"]), InvL2 = Convert.ToDecimal(row["InvL2"]);

                                if (InvL1 > InvL2)
                                {
                                    if (value >= InvL1)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " greenblink";
                                        lblName.CssClass = lblVal.CssClass + " greenblink";

                                        if (row["InvLV0"].ToString() != "")
                                        {
                                            lblScore.Text = row["InvLV0"].ToString();
                                        }
                                    }
                                    else if (value <= InvL1 && value >= InvL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " yellowblink";
                                        lblName.CssClass = lblVal.CssClass + " yellowblink";

                                        if (row["InvLV1"].ToString() != "")
                                        {
                                            lblScore.Text = row["InvLV1"].ToString();
                                        }
                                    }
                                    else if (value <= InvL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " redblink";
                                        lblName.CssClass = lblVal.CssClass + " redblink";

                                        if (row["InvLV2"].ToString() != "")
                                        {
                                            lblScore.Text = row["InvLV2"].ToString();
                                        }

                                    }
                                }
                                else
                                {
                                    if (value <= InvL1)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " greenblink";
                                        lblName.CssClass = lblVal.CssClass + " greenblink";

                                        if (row["InvLV0"].ToString() != "")
                                        {
                                            lblScore.Text = "Score : " + row["InvLV0"].ToString();
                                        }
                                    }
                                    else if (value >= InvL1 && value <= InvL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " yellowblink";
                                        lblName.CssClass = lblVal.CssClass + " yellowblink";

                                        if (row["InvLV1"].ToString() != "")
                                        {
                                            lblScore.Text = "Score : " + row["InvLV1"].ToString();
                                        }
                                    }
                                    else if (value >= InvL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " redblink";
                                        lblName.CssClass = lblVal.CssClass + " redblink";

                                        if (row["InvLV2"].ToString() != "")
                                        {
                                            lblScore.Text = "Score : " + row["InvLV2"].ToString();
                                        }
                                    }
                                }

                            }
                            else if (row["InvL1"].ToString() != "")
                            {
                                decimal InvL1 = Convert.ToDecimal(row["InvL1"]);

                                if (value <= InvL1)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " greenblink";
                                    lblName.CssClass = lblVal.CssClass + " greenblink";

                                    if (row["InvLV0"].ToString() != "")
                                    {
                                        lblScore.Text = "Score : " + row["InvLV0"].ToString();
                                    }
                                }
                            }
                            else if (row["InvL2"].ToString() != "")
                            {
                                decimal InvL2 = Convert.ToDecimal(row["InvL2"]);

                                if (value >= InvL2)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " redblink";
                                    lblName.CssClass = lblVal.CssClass + " redblink";

                                    if (row["InvLV2"].ToString() != "")
                                    {
                                        lblScore.Text = "Score : " + row["InvLV2"].ToString();
                                    }
                                }
                            }
                        }
                        else if (Session["DASHBOARD_COUNTRYID"].ToString() != "All")
                        {
                            if (row["CL1"].ToString() != "" && row["CL2"].ToString() != "")
                            {
                                decimal CL1 = Convert.ToDecimal(row["CL1"]), CL2 = Convert.ToDecimal(row["CL2"]);
                                if (CL1 > CL2)
                                {
                                    if (value >= CL1)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " greenblink";
                                        lblName.CssClass = lblVal.CssClass + " greenblink";

                                        if (row["CLV0"].ToString() != "")
                                        {
                                            lblScore.Text = row["CLV0"].ToString();
                                        }
                                    }
                                    else if (value <= CL1 && value >= CL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " yellowblink";
                                        lblName.CssClass = lblVal.CssClass + " yellowblink";

                                        if (row["CLV1"].ToString() != "")
                                        {
                                            lblScore.Text = row["CLV1"].ToString();
                                        }
                                    }
                                    else if (value <= CL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " redblink";
                                        lblName.CssClass = lblVal.CssClass + " redblink";

                                        if (row["CLV2"].ToString() != "")
                                        {
                                            lblScore.Text = row["CLV2"].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    if (value <= CL1)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " greenblink";
                                        lblName.CssClass = lblVal.CssClass + " greenblink";

                                        if (row["CLV0"].ToString() != "")
                                        {
                                            lblScore.Text = "Score : " + row["CLV0"].ToString();
                                        }
                                    }
                                    else if (value >= CL1 && value <= CL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " yellowblink";
                                        lblName.CssClass = lblVal.CssClass + " yellowblink";

                                        if (row["CLV1"].ToString() != "")
                                        {
                                            lblScore.Text = "Score : " + row["CLV1"].ToString();
                                        }
                                    }
                                    else if (value >= CL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " redblink";
                                        lblName.CssClass = lblVal.CssClass + " redblink";

                                        if (row["CLV2"].ToString() != "")
                                        {
                                            lblScore.Text = "Score : " + row["CLV2"].ToString();
                                        }
                                    }
                                }
                            }
                            else if (row["CL1"].ToString() != "")
                            {
                                decimal CL1 = Convert.ToDecimal(row["CL1"]);

                                if (value <= CL1)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " greenblink";
                                    lblName.CssClass = lblVal.CssClass + " greenblink";

                                    if (row["CVL0"].ToString() != "")
                                    {
                                        lblScore.Text = "Score : " + row["CLV0"].ToString();
                                    }
                                }
                            }
                            else if (row["CL2"].ToString() != "")
                            {
                                decimal CL2 = Convert.ToDecimal(row["CL2"]);

                                if (value >= CL2)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " redblink";
                                    lblName.CssClass = lblVal.CssClass + " redblink";

                                    if (row["CLV2"].ToString() != "")
                                    {
                                        lblScore.Text = "Score : " + row["CLV2"].ToString();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row["L1"].ToString() != "" && row["L2"].ToString() != "")
                            {
                                decimal L1 = Convert.ToDecimal(row["L1"]), L2 = Convert.ToDecimal(row["L2"]);
                                if (L1 > L2)
                                {
                                    if (value >= L1)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " greenblink";
                                        lblName.CssClass = lblVal.CssClass + " greenblink";

                                        if (row["LV0"].ToString() != "")
                                        {
                                            lblScore.Text = row["LV0"].ToString();
                                        }
                                    }
                                    else if (value <= L1 && value >= L2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " yellowblink";
                                        lblName.CssClass = lblVal.CssClass + " yellowblink";

                                        if (row["LV1"].ToString() != "")
                                        {
                                            lblScore.Text = row["LV1"].ToString();
                                        }
                                    }
                                    else if (value <= L2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " redblink";
                                        lblName.CssClass = lblVal.CssClass + " redblink";

                                        if (row["LV2"].ToString() != "")
                                        {
                                            lblScore.Text = row["LV2"].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    if (value <= L1)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " greenblink";
                                        lblName.CssClass = lblVal.CssClass + " greenblink";

                                        if (row["LV0"].ToString() != "")
                                        {
                                            lblScore.Text = "Score : " + row["LV0"].ToString();
                                        }
                                    }
                                    else if (value >= L1 && value <= L2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " yellowblink";
                                        lblName.CssClass = lblVal.CssClass + " yellowblink";

                                        if (row["LV1"].ToString() != "")
                                        {
                                            lblScore.Text = "Score : " + row["LV1"].ToString();
                                        }
                                    }
                                    else if (value >= L2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " redblink";
                                        lblName.CssClass = lblVal.CssClass + " redblink";

                                        if (row["LV2"].ToString() != "")
                                        {
                                            lblScore.Text = "Score : " + row["LV2"].ToString();
                                        }
                                    }
                                }
                            }
                            else if (row["L1"].ToString() != "")
                            {
                                decimal L1 = Convert.ToDecimal(row["L1"]);

                                if (value <= L1)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " greenblink";
                                    lblName.CssClass = lblVal.CssClass + " greenblink";

                                    if (row["LV0"].ToString() != "")
                                    {
                                        lblScore.Text = "Score : " + row["LV0"].ToString();
                                    }
                                }
                            }
                            else if (row["L2"].ToString() != "")
                            {
                                decimal L2 = Convert.ToDecimal(row["L2"]);

                                if (value >= L2)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " redblink";
                                    lblName.CssClass = lblVal.CssClass + " redblink";

                                    if (row["LV2"].ToString() != "")
                                    {
                                        lblScore.Text = "Score : " + row["LV2"].ToString();
                                    }
                                }
                            }
                        }
                    }

                    if (Method == "Advance Metrics")
                    {
                        lblVal.Text += "%";
                    }
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
                DataSet ds = dal.GetSiteID(Action: "GET_COUNTRYID_AND_INVID", User_ID: Session["User_ID"].ToString());
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
                DataSet ds = dal.GetSiteID(Action: "GET_COUNTRYID_AND_INVID", Country_ID: drpCountry.SelectedValue, User_ID: Session["User_ID"].ToString());
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

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SITE_AGAINST_COUNTRY();
                Session["DASHBOARD_COUNTRYID"] = drpCountry.SelectedValue;
                Get_Dashboard();
                Get_Alerts();
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
                Session["DASHBOARD_SITE"] = drpSites.SelectedValue;
                Get_Dashboard();
                Get_Alerts();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void POST_ISSUE_PROJECT(string IndicId, string Score)
        {
            try
            {
                dal.Risk_Indicator_SP(Action: "POST_ISSUE_PROJECT", Score: Score, ID: IndicId, Result: Session["User_ID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void POST_ISSUE_COUNTRY(string IndicId, string Score, string Country)
        {
            try
            {
                dal.Risk_Indicator_SP(Action: "POST_ISSUE_COUNTRY", Score: Score, Actionable: Country, ID: IndicId, Result: Session["User_ID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void POST_ISSUE_SITE(string IndicId, string Score, string Site)
        {
            try
            {
                dal.Risk_Indicator_SP(Action: "POST_ISSUE_SITE", Score: Score, Actionable: Site, ID: IndicId, Result: Session["User_ID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void POST_RISK_PROJECT(string IndicId, string Score)
        {
            try
            {
                dal.Risk_Indicator_SP(Action: "POST_RISK_PROJECT", Score: Score, ID: IndicId, Result: Session["User_ID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void POST_RISK_COUNTRY(string IndicId, string Score, string Country)
        {
            try
            {
                dal.Risk_Indicator_SP(Action: "POST_RISK_COUNTRY", Score: Score, Actionable: Country, ID: IndicId, Result: Session["User_ID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void POST_RISK_SITE(string IndicId, string Score, string Site)
        {
            try
            {
                dal.Risk_Indicator_SP(Action: "POST_RISK_SITE", Score: Score, Actionable: Site, ID: IndicId, Result: Session["User_ID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_NON_POSTED(string IndicId, string Site, string Type, string At)
        {
            try
            {
                dal.Risk_Indicator_SP(Action: "UPDATE_NON_POSTED", ID: IndicId, Actionable: Site, Result: Type, Condition: At);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        int Green = 0;
        int Yellow = 0;
        int Red = 0;
        decimal TotalScore = 0;
        int X = 0;
        int Y = 0;
        int Z = 0;
        int A = 0;
        int B = 0;
        int C = 0;
        int D = 0;
        int E = 0;

        protected void gvRiskIndic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Method = drv["Method"].ToString();
                    string ID = drv["ID"].ToString();

                    string LISTID = drv["LISTID"].ToString();

                    string LAct1 = drv["LAct1"].ToString();
                    string LAct2 = drv["LAct2"].ToString();

                    decimal AveragePer = 0;
                    decimal SumOfNumerator = 0;
                    decimal SumOfDenominator = 0;

                    Label lblResult = (Label)e.Row.FindControl("lblResult");
                    Label lblScore = (Label)e.Row.FindControl("lblScore");
                    Label lblAction = (Label)e.Row.FindControl("lblAction");

                    HtmlGenericControl Igreen = (HtmlGenericControl)e.Row.FindControl("Igreen");
                    HtmlGenericControl Iyellow = (HtmlGenericControl)e.Row.FindControl("Iyellow");
                    HtmlGenericControl Ired = (HtmlGenericControl)e.Row.FindControl("Ired");

                    DataSet ds = new DataSet();
                    if (LISTID != "")
                    {
                        if (Method == "Advance Metrics")
                        {
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

                            DataSet dsIDS = dal.DM_LISTINGS_SP(Action: "GETADVANCEMETRICS", ID: LISTID);

                            if (dsIDS.Tables[0].Rows.Count > 0)
                            {
                                DataSet dsNum = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: dsIDS.Tables[0].Rows[0]["Num_List_ID"].ToString(), INVID: INVID, COUNTRYID: COUNTRYID);
                                DataSet dsDenom = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: dsIDS.Tables[0].Rows[0]["Denom_List_ID"].ToString(), INVID: INVID, COUNTRYID: COUNTRYID);

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
                            }

                            DataTable dt = new DataTable("MyTable");
                            dt.Columns.Add(new DataColumn("Count", typeof(double)));
                            DataRow dr = dt.NewRow();
                            dr["Count"] = String.Format("{0:0.00}", AveragePer);
                            dt.Rows.Add(dr);
                            ds.Tables.Add(dt);

                        }
                        else
                        {
                            ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TILE", LISTING_ID: LISTID);
                        }
                    }
                    else
                    {
                        ds = dal.Dashboard_SP(Action: Method, Project_ID: Session["PROJECTID"].ToString(),
                        User_ID: Session["User_ID"].ToString());
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() != "")
                        {
                            lblResult.Text = ds.Tables[0].Rows[0][0].ToString();

                            DataSet dsTrig = dal.Risk_Indicator_SP(Action: "GET_RM_INDIC_TRIGGER_User", ID: ID, Result: Session["User_ID"].ToString());
                            DataRow dr = dsTrig.Tables[0].Rows[0];

                            decimal value = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());

                            if (dr["L1"].ToString() != "" && dr["L2"].ToString() != "")
                            {
                                decimal L1 = Convert.ToDecimal(dr["L1"]), L2 = Convert.ToDecimal(dr["L2"]);

                                if (L1 > L2)
                                {
                                    if (value >= L1)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " greenblink";

                                        Igreen.Visible = true;

                                        if (dr["LV0"].ToString() != "")
                                        {
                                            lblScore.Text = dr["LV0"].ToString();
                                        }

                                        Green += 1;

                                        UPDATE_NON_POSTED(ID, null, "RISK", "PROJECT");

                                        UPDATE_NON_POSTED(ID, null, "ISSUE", "PROJECT");
                                    }
                                    else if (value <= L1 && value >= L2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " yellowblink";

                                        Iyellow.Visible = true;

                                        if (dr["LV1"].ToString() != "")
                                        {
                                            lblScore.Text = dr["LV1"].ToString();
                                        }

                                        lblAction.Text = LAct1;

                                        Yellow += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["LPost1"].ToString() == "Risk")
                                            {
                                                POST_RISK_PROJECT(ID, lblScore.Text);
                                            }
                                            else if (dr["LPost1"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_PROJECT(ID, lblScore.Text);
                                            }
                                        }
                                    }
                                    else if (value <= L2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " redblink";

                                        Ired.Visible = true;

                                        if (dr["LV2"].ToString() != "")
                                        {
                                            lblScore.Text = dr["LV2"].ToString();
                                        }

                                        lblAction.Text = LAct2;

                                        Red += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["LPost2"].ToString() == "Risk")
                                            {
                                                POST_RISK_PROJECT(ID, lblScore.Text);
                                            }
                                            else if (dr["LPost2"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_PROJECT(ID, lblScore.Text);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (value <= L1)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " greenblink";

                                        Igreen.Visible = true;

                                        if (dr["LV0"].ToString() != "")
                                        {
                                            lblScore.Text = dr["LV0"].ToString();
                                        }

                                        Green += 1;

                                        UPDATE_NON_POSTED(ID, null, "RISK", "PROJECT");

                                        UPDATE_NON_POSTED(ID, null, "RISK", "PROJECT");
                                    }
                                    else if (value >= L1 && value <= L2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " yellowblink";

                                        Iyellow.Visible = true;

                                        if (dr["LV1"].ToString() != "")
                                        {
                                            lblScore.Text = dr["LV1"].ToString();
                                        }

                                        lblAction.Text = LAct1;

                                        Yellow += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["LPost1"].ToString() == "Risk")
                                            {
                                                POST_RISK_PROJECT(ID, lblScore.Text);
                                            }
                                            else if (dr["LPost1"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_PROJECT(ID, lblScore.Text);
                                            }
                                        }
                                    }
                                    else if (value >= L2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " redblink";

                                        Ired.Visible = true;

                                        if (dr["LV2"].ToString() != "")
                                        {
                                            lblScore.Text = dr["LV2"].ToString();
                                        }

                                        lblAction.Text = LAct2;

                                        Red += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["LPost2"].ToString() == "Risk")
                                            {
                                                POST_RISK_PROJECT(ID, lblScore.Text);
                                            }
                                            else if (dr["LPost2"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_PROJECT(ID, lblScore.Text);
                                            }
                                        }
                                    }
                                }


                            }
                            else if (dr["L1"].ToString() != "")
                            {
                                decimal L1 = Convert.ToDecimal(dr["L1"]);

                                if (value <= L1)
                                {
                                    lblResult.CssClass = lblResult.CssClass + " greenblink";

                                    Igreen.Visible = true;

                                    if (dr["LV0"].ToString() != "")
                                    {
                                        lblScore.Text = dr["LV0"].ToString();
                                    }

                                    Green += 1;

                                    UPDATE_NON_POSTED(ID, null, "RISK", "PROJECT");
                                    UPDATE_NON_POSTED(ID, null, "ISSUE", "PROJECT");
                                }
                            }
                            else if (dr["L2"].ToString() != "")
                            {
                                decimal L2 = Convert.ToDecimal(dr["L2"]);

                                if (value >= L2)
                                {
                                    lblResult.CssClass = lblResult.CssClass + " redblink";

                                    Ired.Visible = true;

                                    if (dr["LV2"].ToString() != "")
                                    {
                                        lblScore.Text = dr["LV2"].ToString();
                                    }

                                    lblAction.Text = LAct2;

                                    Red += 1;

                                    if (lblScore.Text != "")
                                    {
                                        if (dr["LPost2"].ToString() == "Risk")
                                        {
                                            POST_RISK_PROJECT(ID, lblScore.Text);
                                        }
                                        else if (dr["LPost2"].ToString() == "Issue")
                                        {
                                            POST_ISSUE_PROJECT(ID, lblScore.Text);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvCountryAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[3].Text = hfGood.Value;
                    e.Row.Cells[4].Text = hfBad.Value;
                    e.Row.Cells[5].Text = hfWorst.Value;
                }
                else if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    Session["RiskIndicCOUNTRYID"] = drv["COUNTRYID"].ToString();

                    Label lblActionableGood = (Label)e.Row.FindControl("lblActionableGood");
                    Label lblActionableBad = (Label)e.Row.FindControl("lblActionableBad");
                    Label lblActionableWorst = (Label)e.Row.FindControl("lblActionableWorst");
                    Label lblTotalScore = (Label)e.Row.FindControl("lblTotalScore");

                    ViewState["Green"] = "0";
                    ViewState["Yellow"] = "0";
                    ViewState["Red"] = "0";
                    ViewState["TotalScore"] = "0";

                    Green = 0;
                    Yellow = 0;
                    Red = 0;
                    TotalScore = 0;

                    GridView gvCountryRiskIndic = e.Row.FindControl("gvCountryRiskIndic") as GridView;
                    DataSet ds = dal.Risk_Indicator_SP(Action: "Triggered_Indicators_Country_User", Result: Session["User_ID"].ToString());
                    gvCountryRiskIndic.DataSource = ds.Tables[0];
                    gvCountryRiskIndic.DataBind();

                    lblActionableGood.Text = ViewState["Green"].ToString();
                    lblActionableBad.Text = ViewState["Yellow"].ToString();
                    lblActionableWorst.Text = ViewState["Red"].ToString();
                    lblTotalScore.Text = ViewState["TotalScore"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvCountryRiskIndic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Method = drv["Method"].ToString();
                    string ID = drv["ID"].ToString();
                    string LISTID = drv["LISTID"].ToString();

                    string CLAct1 = drv["CLAct1"].ToString();
                    string CLAct2 = drv["CLAct2"].ToString();

                    Label lblResult = (Label)e.Row.FindControl("lblResult");
                    Label lblScore = (Label)e.Row.FindControl("lblScore");
                    Label lblAction = (Label)e.Row.FindControl("lblAction");

                    decimal AveragePer = 0;
                    decimal SumOfNumerator = 0;
                    decimal SumOfDenominator = 0;

                    HtmlGenericControl Igreen = (HtmlGenericControl)e.Row.FindControl("Igreen");
                    HtmlGenericControl Iyellow = (HtmlGenericControl)e.Row.FindControl("Iyellow");
                    HtmlGenericControl Ired = (HtmlGenericControl)e.Row.FindControl("Ired");

                    GridView gvRiskIndic = e.Row.FindControl("gvRiskIndic") as GridView;
                    DataSet ds = new DataSet();
                    if (LISTID != "")
                    {
                        if (Method == "Advance Metrics")
                        {
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

                            DataSet dsIDS = dal.DM_LISTINGS_SP(Action: "GETADVANCEMETRICS", ID: LISTID);

                            if (dsIDS.Tables[0].Rows.Count > 0)
                            {
                                DataSet dsNum = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: dsIDS.Tables[0].Rows[0]["Num_List_ID"].ToString(), INVID: INVID, COUNTRYID: COUNTRYID);
                                DataSet dsDenom = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: dsIDS.Tables[0].Rows[0]["Denom_List_ID"].ToString(), INVID: INVID, COUNTRYID: COUNTRYID);

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
                            }

                            DataTable dt = new DataTable("MyTable");
                            dt.Columns.Add(new DataColumn("Count", typeof(double)));
                            DataRow dr = dt.NewRow();
                            dr["Count"] = String.Format("{0:0.00}", AveragePer);
                            dt.Rows.Add(dr);
                            ds.Tables.Add(dt);

                        }
                        else
                        {
                            ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TILE", LISTING_ID: LISTID, INVID: Session["DASHBOARD_SITE"].ToString(), COUNTRYID: Session["RiskIndicCOUNTRYID"].ToString());
                        }
                    }
                    else
                    {
                        ds = dal.Dashboard_SP(Action: Method, Project_ID: Session["PROJECTID"].ToString(),
                        COUNTRYID: Session["RiskIndicCOUNTRYID"].ToString(), INVID: "All", User_ID: Session["User_ID"].ToString());
                    }


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() != "")
                        {
                            lblResult.Text = ds.Tables[0].Rows[0][0].ToString();

                            DataSet dsTrig = dal.Risk_Indicator_SP(Action: "GET_RM_INDIC_TRIGGER_User", ID: ID, Result: Session["User_ID"].ToString());
                            DataRow dr = dsTrig.Tables[0].Rows[0];

                            decimal value = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());

                            if (dr["CL1"].ToString() != "" && dr["CL2"].ToString() != "")
                            {
                                decimal CL1 = Convert.ToDecimal(dr["CL1"]), CL2 = Convert.ToDecimal(dr["CL2"]);

                                if (CL1 > CL2)
                                {
                                    if (value >= CL1)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " greenblink";

                                        Igreen.Visible = true;

                                        if (dr["CLV0"].ToString() != "")
                                        {
                                            lblScore.Text = dr["CLV0"].ToString();
                                        }

                                        Green += 1;

                                        UPDATE_NON_POSTED(ID, Session["RiskIndicCOUNTRYID"].ToString(), "RISK", "COUNTRY");
                                        UPDATE_NON_POSTED(ID, Session["RiskIndicCOUNTRYID"].ToString(), "ISSUE", "COUNTRY");
                                    }
                                    else if (value <= CL1 && value >= CL2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " yellowblink";

                                        Iyellow.Visible = true;

                                        if (dr["CLV1"].ToString() != "")
                                        {
                                            lblScore.Text = dr["CLV1"].ToString();
                                        }

                                        lblAction.Text = CLAct1;

                                        Yellow += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["CLPost1"].ToString() == "Risk")
                                            {
                                                POST_RISK_COUNTRY(ID, lblScore.Text, Session["RiskIndicCOUNTRYID"].ToString());
                                            }
                                            else if (dr["CLPost1"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_COUNTRY(ID, lblScore.Text, Session["RiskIndicCOUNTRYID"].ToString());
                                            }
                                        }
                                    }
                                    else if (value <= CL2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " redblink";

                                        Ired.Visible = true;

                                        if (dr["CLV2"].ToString() != "")
                                        {
                                            lblScore.Text = dr["CLV2"].ToString();
                                        }

                                        lblAction.Text = CLAct2;

                                        Red += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["CLPost2"].ToString() == "Risk")
                                            {
                                                POST_RISK_COUNTRY(ID, lblScore.Text, Session["RiskIndicCOUNTRYID"].ToString());
                                            }
                                            else if (dr["CLPost2"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_COUNTRY(ID, lblScore.Text, Session["RiskIndicCOUNTRYID"].ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (value <= CL1)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " greenblink";

                                        Igreen.Visible = true;

                                        if (dr["CLV0"].ToString() != "")
                                        {
                                            lblScore.Text = dr["CLV0"].ToString();
                                        }

                                        Green += 1;

                                        UPDATE_NON_POSTED(ID, Session["RiskIndicCOUNTRYID"].ToString(), "RISK", "COUNTRY");
                                        UPDATE_NON_POSTED(ID, Session["RiskIndicCOUNTRYID"].ToString(), "ISSUE", "COUNTRY");
                                    }
                                    else if (value >= CL1 && value <= CL2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " yellowblink";

                                        Iyellow.Visible = true;

                                        if (dr["CLV1"].ToString() != "")
                                        {
                                            lblScore.Text = dr["CLV1"].ToString();
                                        }

                                        lblAction.Text = CLAct1;

                                        Yellow += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["CLPost1"].ToString() == "Risk")
                                            {
                                                POST_RISK_COUNTRY(ID, lblScore.Text, Session["RiskIndicCOUNTRYID"].ToString());
                                            }
                                            else if (dr["CLPost1"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_COUNTRY(ID, lblScore.Text, Session["RiskIndicCOUNTRYID"].ToString());
                                            }
                                        }
                                    }
                                    else if (value >= CL2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " redblink";

                                        Ired.Visible = true;

                                        if (dr["CLV2"].ToString() != "")
                                        {
                                            lblScore.Text = dr["CLV2"].ToString();
                                        }

                                        lblAction.Text = CLAct2;

                                        Red += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["CLPost2"].ToString() == "Risk")
                                            {
                                                POST_RISK_COUNTRY(ID, lblScore.Text, Session["RiskIndicCOUNTRYID"].ToString());
                                            }
                                            else if (dr["CLPost2"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_COUNTRY(ID, lblScore.Text, Session["RiskIndicCOUNTRYID"].ToString());
                                            }
                                        }
                                    }
                                }


                            }
                            else if (dr["CL1"].ToString() != "")
                            {
                                decimal CL1 = Convert.ToDecimal(dr["CL1"]);

                                if (value <= CL1)
                                {
                                    lblResult.CssClass = lblResult.CssClass + " greenblink";

                                    Igreen.Visible = true;

                                    if (dr["CLV0"].ToString() != "")
                                    {
                                        lblScore.Text = dr["CLV0"].ToString();
                                    }

                                    Green += 1;

                                    UPDATE_NON_POSTED(ID, Session["RiskIndicCOUNTRYID"].ToString(), "RISK", "COUNTRY");
                                    UPDATE_NON_POSTED(ID, Session["RiskIndicCOUNTRYID"].ToString(), "ISSUE", "COUNTRY");
                                }
                            }
                            else if (dr["CL2"].ToString() != "")
                            {
                                decimal CL2 = Convert.ToDecimal(dr["CL2"]);

                                if (value >= CL2)
                                {
                                    lblResult.CssClass = lblResult.CssClass + " redblink";

                                    Ired.Visible = true;

                                    if (dr["CLV2"].ToString() != "")
                                    {
                                        lblScore.Text = dr["CLV2"].ToString();
                                    }

                                    lblAction.Text = CLAct2;

                                    Red += 1;

                                    if (lblScore.Text != "")
                                    {
                                        if (dr["CLPost2"].ToString() == "Risk")
                                        {
                                            POST_RISK_COUNTRY(ID, lblScore.Text, Session["RiskIndicCOUNTRYID"].ToString());
                                        }
                                        else if (dr["CLPost2"].ToString() == "Issue")
                                        {
                                            POST_ISSUE_COUNTRY(ID, lblScore.Text, Session["RiskIndicCOUNTRYID"].ToString());
                                        }
                                    }
                                }
                            }
                        }

                        if (lblScore.Text != "")
                        {
                            TotalScore += Convert.ToDecimal(lblScore.Text);
                        }

                        ViewState["Green"] = Green;
                        ViewState["Yellow"] = Yellow;
                        ViewState["Red"] = Red;
                        ViewState["TotalScore"] = TotalScore;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvInvAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    Session["RiskIndicINVID"] = drv["INVID"].ToString();

                    Label lblTotalScore = (Label)e.Row.FindControl("lblTotalScore");
                    Label lblActionable = (Label)e.Row.FindControl("lblActionable");

                    ViewState["Green"] = "0";
                    ViewState["Yellow"] = "0";
                    ViewState["Red"] = "0";
                    ViewState["TotalScore"] = "0";
                    ViewState["X"] = "0";
                    ViewState["Y"] = "0";
                    ViewState["Z"] = "0";
                    ViewState["A"] = "0";
                    ViewState["B"] = "0";
                    ViewState["C"] = "0";
                    ViewState["D"] = "0";
                    ViewState["E"] = "0";

                    Green = 0;
                    Yellow = 0;
                    Red = 0;
                    TotalScore = 0;
                    X = 0;
                    Y = 0;
                    Z = 0;
                    A = 0;
                    B = 0;
                    C = 0;
                    D = 0;
                    E = 0;

                    GridView gvInvRiskCat = e.Row.FindControl("gvInvRiskCat") as GridView;
                    DataSet ds = dal.Risk_Indicator_SP(Action: "Triggered_Indicators_Inv_Cat", InvL1: drv["INVID"].ToString());
                    gvInvRiskCat.DataSource = ds.Tables[0];
                    gvInvRiskCat.DataBind();

                    lblTotalScore.Text = ViewState["TotalScore"].ToString();

                    DataSet dsActionable = dal.Risk_Indicator_SP(
                    Action: "GET_ACTIONABLE",
                    Score: TotalScore.ToString(),
                    Green: Green.ToString(),
                    Yellow: Yellow.ToString(),
                    Red: Red.ToString(),
                    X: X.ToString(),
                    Y: Y.ToString(),
                    Z: Z.ToString(),
                    A: A.ToString(),
                    B: B.ToString(),
                    C: C.ToString(),
                    D: D.ToString(),
                    E: E.ToString()
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblActionable.Text = dsActionable.Tables[0].Rows[0]["Actionable"].ToString();
                    }

                    dal.Risk_Indicator_SP(Action: "TRUNCATE_CAT_ALGO_VALUES");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        int CatGreen = 0;
        int CatYellow = 0;
        int CatRed = 0;
        int CatScore = 0;
        int CatX = 0;
        int CatY = 0;
        int CatZ = 0;
        int CatA = 0;
        int CatB = 0;
        int CatC = 0;
        int CatD = 0;
        int CatE = 0;

        protected void gvInvRiskCat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    CatGreen = 0;
                    CatYellow = 0;
                    CatRed = 0;
                    CatScore = 0;
                    CatX = 0;
                    CatY = 0;
                    CatZ = 0;
                    CatA = 0;
                    CatB = 0;
                    CatC = 0;
                    CatD = 0;
                    CatE = 0;

                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Category = drv["Category"].ToString();

                    GridView gvInvRiskIndic = e.Row.FindControl("gvInvRiskIndic") as GridView;
                    DataSet ds = dal.Risk_Indicator_SP(Action: "Triggered_Indicators_Inv", Result: Category);
                    gvInvRiskIndic.DataSource = ds.Tables[0];
                    gvInvRiskIndic.DataBind();

                    dal.Risk_Indicator_SP(
                    Action: "INSERT_CAT_ALGO_VALUES",
                    Cat1: Category,
                    Score: CatScore.ToString(),
                    Green: CatGreen.ToString(),
                    Yellow: CatYellow.ToString(),
                    Red: CatRed.ToString(),
                    X: CatX.ToString(),
                    Y: CatY.ToString(),
                    Z: CatZ.ToString(),
                    A: CatA.ToString(),
                    B: CatB.ToString(),
                    C: CatC.ToString(),
                    D: CatD.ToString(),
                    E: CatE.ToString()
                        );

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvInvRiskIndic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Method = drv["Method"].ToString();
                    string ID = drv["ID"].ToString();

                    string InvLAct1 = drv["InvLAct1"].ToString();
                    string InvLAct2 = drv["InvLAct2"].ToString();

                    Label lblResult = (Label)e.Row.FindControl("lblResult");
                    Label lblScore = (Label)e.Row.FindControl("lblScore");
                    Label lblAction = (Label)e.Row.FindControl("lblAction");

                    HtmlGenericControl Igreen = (HtmlGenericControl)e.Row.FindControl("Igreen");
                    HtmlGenericControl Iyellow = (HtmlGenericControl)e.Row.FindControl("Iyellow");
                    HtmlGenericControl Ired = (HtmlGenericControl)e.Row.FindControl("Ired");

                    string LISTID = drv["LISTID"].ToString();

                    decimal AveragePer = 0;
                    decimal SumOfNumerator = 0;
                    decimal SumOfDenominator = 0;

                    GridView gvRiskIndic = e.Row.FindControl("gvRiskIndic") as GridView;
                    DataSet ds = new DataSet();

                    //DataSet ds = dal.Dashboard_SP(Action: Method, Project_ID: Session["PROJECTID"].ToString(),
                    //INVID: Session["RiskIndicINVID"].ToString(), User_ID: Session["User_ID"].ToString());

                    if (LISTID != "")
                    {
                        if (Method == "Advance Metrics")
                        {
                            string INVID = Session["RiskIndicINVID"].ToString();

                            DataSet dsIDS = dal.DM_LISTINGS_SP(Action: "GETADVANCEMETRICS", ID: LISTID);

                            if (dsIDS.Tables[0].Rows.Count > 0)
                            {
                                DataSet dsNum = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: dsIDS.Tables[0].Rows[0]["Num_List_ID"].ToString(), INVID: INVID, COUNTRYID: "0");
                                DataSet dsDenom = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: dsIDS.Tables[0].Rows[0]["Denom_List_ID"].ToString(), INVID: INVID, COUNTRYID: "0");

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
                            }

                            DataTable dt = new DataTable("MyTable");
                            dt.Columns.Add(new DataColumn("Count", typeof(double)));
                            DataRow dr = dt.NewRow();
                            dr["Count"] = String.Format("{0:0.00}", AveragePer);
                            dt.Rows.Add(dr);
                            ds.Tables.Add(dt);

                        }
                        else
                        {
                            ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TILE", LISTING_ID: LISTID, INVID: Session["RiskIndicINVID"].ToString());
                        }
                    }
                    else
                    {
                        ds = dal.Dashboard_SP(
                        Action: Method,
                        INVID: Session["RiskIndicINVID"].ToString(),
                        Project_ID: Session["PROJECTID"].ToString(),
                        User_ID: Session["User_ID"].ToString()
                        );
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() != "")
                        {
                            lblResult.Text = ds.Tables[0].Rows[0][0].ToString();

                            DataSet dsTrig = dal.Risk_Indicator_SP(Action: "GET_RM_INDIC_TRIGGER", ID: ID);
                            DataRow dr = dsTrig.Tables[0].Rows[0];

                            decimal value = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());

                            if (dr["InvL1"].ToString() != "" && dr["InvL2"].ToString() != "")
                            {
                                decimal InvL1 = Convert.ToDecimal(dr["InvL1"]), InvL2 = Convert.ToDecimal(dr["InvL2"]);

                                if (InvL1 > InvL2)
                                {
                                    if (value >= InvL1)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " greenblink";

                                        Igreen.Visible = true;

                                        if (dr["InvLV0"].ToString() != "")
                                        {
                                            lblScore.Text = dr["InvLV0"].ToString();

                                            if (dr["InvLV0"].ToString() == "5")
                                            {
                                                X += 1;
                                                CatX += 1;
                                            }
                                            else if (dr["InvLV0"].ToString() == "10")
                                            {
                                                Y += 1;
                                                CatY += 1;
                                            }
                                            else if (dr["InvLV0"].ToString() == "15")
                                            {
                                                Z += 1;
                                                CatZ += 1;
                                            }
                                        }

                                        Green += 1;
                                        CatGreen += 1;

                                        UPDATE_NON_POSTED(ID, Session["RiskIndicINVID"].ToString(), "RISK", "SITE");
                                        UPDATE_NON_POSTED(ID, Session["RiskIndicINVID"].ToString(), "ISSUE", "SITE");

                                    }
                                    else if (value <= InvL1 && value >= InvL2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " yellowblink";

                                        Iyellow.Visible = true;

                                        if (dr["InvLV1"].ToString() != "")
                                        {
                                            lblScore.Text = dr["InvLV1"].ToString();

                                            if (dr["InvLV1"].ToString() == "-5")
                                            {
                                                A += 1;
                                                CatA += 1;
                                            }
                                            else if (dr["InvLV1"].ToString() == "-10")
                                            {
                                                B += 1;
                                                CatB += 1;
                                            }
                                            else if (dr["InvLV1"].ToString() == "-15")
                                            {
                                                C += 1;
                                                CatC += 1;
                                            }
                                            else if (dr["InvLV1"].ToString() == "-20")
                                            {
                                                D += 1;
                                                CatD += 1;
                                            }
                                            else if (dr["InvLV1"].ToString() == "-25")
                                            {
                                                E += 1;
                                                CatE += 1;
                                            }
                                        }

                                        Yellow += 1;
                                        CatYellow += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["InvLPost1"].ToString() == "Risk")
                                            {
                                                POST_RISK_SITE(ID, lblScore.Text, Session["RiskIndicINVID"].ToString());
                                            }
                                            else if (dr["InvLPost1"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_SITE(ID, lblScore.Text, Session["RiskIndicINVID"].ToString());
                                            }
                                        }
                                    }
                                    else if (value <= InvL2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " redblink";

                                        Ired.Visible = true;

                                        if (dr["InvLV2"].ToString() != "")
                                        {
                                            lblScore.Text = dr["InvLV2"].ToString();

                                            if (dr["InvLV2"].ToString() == "-5")
                                            {
                                                A += 1;
                                                CatA += 1;
                                            }
                                            else if (dr["InvLV2"].ToString() == "-10")
                                            {
                                                B += 1;
                                                CatB += 1;
                                            }
                                            else if (dr["InvLV2"].ToString() == "-15")
                                            {
                                                C += 1;
                                                CatC += 1;
                                            }
                                            else if (dr["InvLV2"].ToString() == "-20")
                                            {
                                                D += 1;
                                                CatD += 1;
                                            }
                                            else if (dr["InvLV2"].ToString() == "-25")
                                            {
                                                E += 1;
                                                CatE += 1;
                                            }
                                        }

                                        Red += 1;
                                        CatRed += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["InvLPost2"].ToString() == "Risk")
                                            {
                                                POST_RISK_SITE(ID, lblScore.Text, Session["RiskIndicINVID"].ToString());
                                            }
                                            else if (dr["InvLPost2"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_SITE(ID, lblScore.Text, Session["RiskIndicINVID"].ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (value <= InvL1)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " greenblink";

                                        Igreen.Visible = true;

                                        if (dr["InvLV0"].ToString() != "")
                                        {
                                            lblScore.Text = dr["InvLV0"].ToString();

                                            if (dr["InvLV0"].ToString() == "5")
                                            {
                                                X += 1;
                                                CatX += 1;
                                            }
                                            else if (dr["InvLV0"].ToString() == "10")
                                            {
                                                Y += 1;
                                                CatY += 1;
                                            }
                                            else if (dr["InvLV0"].ToString() == "15")
                                            {
                                                Z += 1;
                                                CatZ += 1;
                                            }
                                        }

                                        Green += 1;
                                        CatGreen += 1;

                                        UPDATE_NON_POSTED(ID, Session["RiskIndicINVID"].ToString(), "RISK", "SITE");
                                        UPDATE_NON_POSTED(ID, Session["RiskIndicINVID"].ToString(), "ISSUE", "SITE");
                                    }
                                    else if (value >= InvL1 && value <= InvL2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " yellowblink";

                                        Iyellow.Visible = true;

                                        if (dr["InvLV1"].ToString() != "")
                                        {
                                            lblScore.Text = dr["InvLV1"].ToString();

                                            if (dr["InvLV1"].ToString() == "-5")
                                            {
                                                A += 1;
                                                CatA += 1;
                                            }
                                            else if (dr["InvLV1"].ToString() == "-10")
                                            {
                                                B += 1;
                                                CatB += 1;
                                            }
                                            else if (dr["InvLV1"].ToString() == "-15")
                                            {
                                                C += 1;
                                                CatC += 1;
                                            }
                                            else if (dr["InvLV1"].ToString() == "-20")
                                            {
                                                D += 1;
                                                CatD += 1;
                                            }
                                            else if (dr["InvLV1"].ToString() == "-25")
                                            {
                                                E += 1;
                                                CatE += 1;
                                            }
                                        }

                                        Yellow += 1;
                                        CatYellow += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["InvLPost1"].ToString() == "Risk")
                                            {
                                                POST_RISK_SITE(ID, lblScore.Text, Session["RiskIndicINVID"].ToString());
                                            }
                                            else if (dr["InvLPost1"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_SITE(ID, lblScore.Text, Session["RiskIndicINVID"].ToString());
                                            }
                                        }
                                    }
                                    else if (value >= InvL2)
                                    {
                                        lblResult.CssClass = lblResult.CssClass + " redblink";

                                        Ired.Visible = true;

                                        if (dr["InvLV2"].ToString() != "")
                                        {
                                            lblScore.Text = dr["InvLV2"].ToString();

                                            if (dr["InvLV2"].ToString() == "-5")
                                            {
                                                A += 1;
                                                CatA += 1;
                                            }
                                            else if (dr["InvLV2"].ToString() == "-10")
                                            {
                                                B += 1;
                                                CatB += 1;
                                            }
                                            else if (dr["InvLV2"].ToString() == "-15")
                                            {
                                                C += 1;
                                                CatC += 1;
                                            }
                                            else if (dr["InvLV2"].ToString() == "-20")
                                            {
                                                D += 1;
                                                CatD += 1;
                                            }
                                            else if (dr["InvLV2"].ToString() == "-25")
                                            {
                                                E += 1;
                                                CatE += 1;
                                            }
                                        }

                                        Red += 1;
                                        CatRed += 1;

                                        if (lblScore.Text != "")
                                        {
                                            if (dr["InvLPost2"].ToString() == "Risk")
                                            {
                                                POST_RISK_SITE(ID, lblScore.Text, Session["RiskIndicINVID"].ToString());
                                            }
                                            else if (dr["InvLPost2"].ToString() == "Issue")
                                            {
                                                POST_ISSUE_SITE(ID, lblScore.Text, Session["RiskIndicINVID"].ToString());
                                            }
                                        }
                                    }
                                }


                            }
                            else if (dr["InvL1"].ToString() != "")
                            {
                                decimal InvL1 = Convert.ToDecimal(dr["InvL1"]);

                                if (value <= InvL1)
                                {
                                    lblResult.CssClass = lblResult.CssClass + " greenblink";

                                    Igreen.Visible = true;

                                    if (dr["InvLV0"].ToString() != "")
                                    {
                                        lblScore.Text = dr["InvLV0"].ToString();

                                        if (dr["InvLV0"].ToString() == "5")
                                        {
                                            X += 1;
                                            CatX += 1;
                                        }
                                        else if (dr["InvLV0"].ToString() == "10")
                                        {
                                            Y += 1;
                                            CatY += 1;
                                        }
                                        else if (dr["InvLV0"].ToString() == "15")
                                        {
                                            Z += 1;
                                            CatZ += 1;
                                        }
                                    }

                                    Green += 1;
                                    CatGreen += 1;

                                    UPDATE_NON_POSTED(ID, Session["RiskIndicINVID"].ToString(), "RISK", "SITE");
                                    UPDATE_NON_POSTED(ID, Session["RiskIndicINVID"].ToString(), "ISSUE", "SITE");
                                }
                            }
                            else if (dr["InvL2"].ToString() != "")
                            {
                                decimal InvL2 = Convert.ToDecimal(dr["InvL2"]);

                                if (value >= InvL2)
                                {
                                    lblResult.CssClass = lblResult.CssClass + " redblink";

                                    Ired.Visible = true;

                                    if (dr["InvLV2"].ToString() != "")
                                    {
                                        lblScore.Text = dr["InvLV2"].ToString();

                                        if (dr["InvLV2"].ToString() == "-5")
                                        {
                                            A += 1;
                                            CatA += 1;
                                        }
                                        else if (dr["InvLV2"].ToString() == "-10")
                                        {
                                            B += 1;
                                            CatB += 1;
                                        }
                                        else if (dr["InvLV2"].ToString() == "-15")
                                        {
                                            C += 1;
                                            CatC += 1;
                                        }
                                        else if (dr["InvLV2"].ToString() == "-20")
                                        {
                                            D += 1;
                                            CatD += 1;
                                        }
                                        else if (dr["InvLV2"].ToString() == "-25")
                                        {
                                            E += 1;
                                            CatE += 1;
                                        }
                                    }

                                    Red += 1;
                                    CatRed += 1;

                                    if (lblScore.Text != "")
                                    {
                                        if (dr["InvLPost2"].ToString() == "Risk")
                                        {
                                            POST_RISK_SITE(ID, lblScore.Text, Session["RiskIndicINVID"].ToString());
                                        }
                                        else if (dr["InvLPost2"].ToString() == "Issue")
                                        {
                                            POST_ISSUE_SITE(ID, lblScore.Text, Session["RiskIndicINVID"].ToString());
                                        }
                                    }
                                }
                            }
                        }

                        if (lblScore.Text != "")
                        {
                            TotalScore += Convert.ToDecimal(lblScore.Text);
                        }

                        ViewState["Green"] = Green;
                        ViewState["Yellow"] = Yellow;
                        ViewState["Red"] = Red;
                        ViewState["TotalScore"] = TotalScore;
                        ViewState["X"] = X;
                        ViewState["Y"] = Y;
                        ViewState["Z"] = Z;
                        ViewState["A"] = A;
                        ViewState["B"] = B;
                        ViewState["C"] = C;
                        ViewState["D"] = D;
                        ViewState["E"] = E;
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