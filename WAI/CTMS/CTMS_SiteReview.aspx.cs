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

namespace CTMS
{
    public partial class CTMS_SiteReview : System.Web.UI.Page
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
                    GETCATEGORY();
                    Get_Dashboard();
                }
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
                DataSet ds1 = dal.Risk_Indicator_SP(Action: "GET_Risk_Indicator_Tiles_Site_Review", Cat1: drpCategory.SelectedItem.Text);

                repeatTiles.DataSource = ds1.Tables[0];
                repeatTiles.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETCATEGORY()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDI_CATEGORY");

                if (ds.Tables[1].Rows.Count > 0)
                {
                    drpCategory.DataSource = ds.Tables[1];
                    drpCategory.DataTextField = "Category";
                    drpCategory.DataValueField = "Category";
                    drpCategory.DataBind();
                    drpCategory.Items.Insert(1, new ListItem("Medical Review", "Medical Review"));
                }
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
                    string ShortTilename = row["ShortTileName"].ToString();
                    string Method = row["Method"].ToString();
                    string X = row["X"].ToString();
                    string Y = row["Y"].ToString();
                    string Width = row["Width"].ToString();
                    string Height = row["Height"].ToString();
                    string ID = row["ID"].ToString();
                    string LISTID = row["LISTID"].ToString();

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

                    //if (X != null && X.ToString() != "")
                    //{
                    //    divMain.Attributes.Remove("data-gs-x");
                    //    divMain.Attributes.Add("data-gs-x", X);
                    //}
                    //if (Y != null && Y.ToString() != "")
                    //{
                    //    divMain.Attributes.Remove("data-gs-y");
                    //    divMain.Attributes.Add("data-gs-y", Y);
                    //}
                    //if (Width != null && Width.ToString() != "")
                    //{
                    //    divMain.Attributes.Remove("data-gs-width");
                    //    divMain.Attributes.Add("data-gs-width", Width);
                    //}
                    //if (Height != null && Height.ToString() != "")
                    //{
                    //    divMain.Attributes.Remove("data-gs-height");
                    //    divMain.Attributes.Add("data-gs-height", Height);
                    //}

                    hfIndicID.Value = ID;

                    lblName.Text = ShortTilename;
                    lblName.ToolTip = TileName;

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    if (LISTID != "")
                    {
                        ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TILE", LISTING_ID: LISTID, INVID: Session["DASHBOARD_SITE"].ToString(), COUNTRYID: Session["DASHBOARD_COUNTRYID"].ToString());
                    }
                    else
                    {
                        ds = dal.Dashboard_SP(Action: Method, Project_ID: Session["PROJECTID"].ToString(), INVID: Session["DASHBOARD_SITE"].ToString(),
                        COUNTRYID: Session["DASHBOARD_COUNTRYID"].ToString(), User_ID: Session["User_ID"].ToString());
                    }

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

                    if (lblVal.Text != "")
                    {
                        decimal value = Convert.ToDecimal(lblVal.Text);

                        if (Session["DASHBOARD_SITE"].ToString() != "All")
                        {
                            if (row["InvL1"].ToString() != "" && row["InvL2"].ToString() != "")
                            {
                                int InvL1 = Convert.ToInt32(row["InvL1"]), InvL2 = Convert.ToInt32(row["InvL2"]);

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
                                int InvL1 = Convert.ToInt32(row["InvL1"]);

                                if (value <= InvL1)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " greenblink";
                                    lblName.CssClass = lblVal.CssClass + " greenblink";

                                    if (row["InvLV0"].ToString() != "")
                                    {
                                        lblScore.Text = "(" + row["InvLV0"].ToString() + ")";
                                    }
                                }
                            }
                            else if (row["InvL2"].ToString() != "")
                            {
                                int InvL2 = Convert.ToInt32(row["InvL2"]);

                                if (value >= InvL2)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " redblink";
                                    lblName.CssClass = lblVal.CssClass + " redblink";

                                    if (row["InvLV2"].ToString() != "")
                                    {
                                        lblScore.Text = "(" + row["InvLV2"].ToString() + ")";
                                    }
                                }
                            }
                        }
                        else if (Session["DASHBOARD_COUNTRYID"].ToString() != "All")
                        {
                            if (row["CL1"].ToString() != "" && row["CL2"].ToString() != "")
                            {
                                int CL1 = Convert.ToInt32(row["CL1"]), CL2 = Convert.ToInt32(row["CL2"]);
                                if (CL1 > CL2)
                                {
                                    if (value >= CL1)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " greenblink";
                                        lblName.CssClass = lblVal.CssClass + " greenblink";

                                        if (row["CLV0"].ToString() != "")
                                        {
                                            lblScore.Text = "(" + row["CLV0"].ToString() + ")";
                                        }
                                    }
                                    else if (value <= CL1 && value >= CL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " yellowblink";
                                        lblName.CssClass = lblVal.CssClass + " yellowblink";

                                        if (row["CLV1"].ToString() != "")
                                        {
                                            lblScore.Text = "(" + row["CLV1"].ToString() + ")";
                                        }
                                    }
                                    else if (value <= CL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " redblink";
                                        lblName.CssClass = lblVal.CssClass + " redblink";

                                        if (row["CLV2"].ToString() != "")
                                        {
                                            lblScore.Text = "(" + row["CLV2"].ToString() + ")";
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
                                            lblScore.Text = "(" + row["CLV0"].ToString() + ")";
                                        }
                                    }
                                    else if (value >= CL1 && value <= CL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " yellowblink";
                                        lblName.CssClass = lblVal.CssClass + " yellowblink";

                                        if (row["CLV1"].ToString() != "")
                                        {
                                            lblScore.Text = "(" + row["CLV1"].ToString() + ")";
                                        }
                                    }
                                    else if (value >= CL2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " redblink";
                                        lblName.CssClass = lblVal.CssClass + " redblink";

                                        if (row["CLV2"].ToString() != "")
                                        {
                                            lblScore.Text = "(" + row["CLV2"].ToString() + ")";
                                        }
                                    }
                                }
                            }
                            else if (row["CL1"].ToString() != "")
                            {
                                int CL1 = Convert.ToInt32(row["CL1"]);

                                if (value <= CL1)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " greenblink";
                                    lblName.CssClass = lblVal.CssClass + " greenblink";

                                    if (row["CVL0"].ToString() != "")
                                    {
                                        lblScore.Text = "(" + row["CLV0"].ToString() + ")";
                                    }
                                }
                            }
                            else if (row["CL2"].ToString() != "")
                            {
                                int CL2 = Convert.ToInt32(row["CL2"]);

                                if (value >= CL2)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " redblink";
                                    lblName.CssClass = lblVal.CssClass + " redblink";

                                    if (row["CLV2"].ToString() != "")
                                    {
                                        lblScore.Text = "(" + row["CLV2"].ToString() + ")";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row["L1"].ToString() != "" && row["L2"].ToString() != "")
                            {
                                int L1 = Convert.ToInt32(row["L1"]), L2 = Convert.ToInt32(row["L2"]);
                                if (L1 > L2)
                                {
                                    if (value >= L1)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " greenblink";
                                        lblName.CssClass = lblVal.CssClass + " greenblink";

                                        if (row["LV0"].ToString() != "")
                                        {
                                            lblScore.Text = "(" + row["LV0"].ToString() + ")";
                                        }
                                    }
                                    else if (value <= L1 && value >= L2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " yellowblink";
                                        lblName.CssClass = lblVal.CssClass + " yellowblink";

                                        if (row["LV1"].ToString() != "")
                                        {
                                            lblScore.Text = "(" + row["LV1"].ToString() + ")";
                                        }
                                    }
                                    else if (value <= L2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " redblink";
                                        lblName.CssClass = lblVal.CssClass + " redblink";

                                        if (row["LV2"].ToString() != "")
                                        {
                                            lblScore.Text = "(" + row["LV2"].ToString() + ")";
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
                                            lblScore.Text = "(" + row["LV0"].ToString() + ")";
                                        }
                                    }
                                    else if (value >= L1 && value <= L2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " yellowblink";
                                        lblName.CssClass = lblVal.CssClass + " yellowblink";

                                        if (row["LV1"].ToString() != "")
                                        {
                                            lblScore.Text = "(" + row["LV1"].ToString() + ")";
                                        }
                                    }
                                    else if (value >= L2)
                                    {
                                        lblVal.CssClass = lblVal.CssClass + " redblink";
                                        lblName.CssClass = lblVal.CssClass + " redblink";

                                        if (row["LV2"].ToString() != "")
                                        {
                                            lblScore.Text = "(" + row["LV2"].ToString() + ")";
                                        }
                                    }
                                }
                            }
                            else if (row["L1"].ToString() != "")
                            {
                                int L1 = Convert.ToInt32(row["L1"]);

                                if (value <= L1)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " greenblink";
                                    lblName.CssClass = lblVal.CssClass + " greenblink";

                                    if (row["LV0"].ToString() != "")
                                    {
                                        lblScore.Text = "(" + row["LV0"].ToString() + ")";
                                    }
                                }
                            }
                            else if (row["L2"].ToString() != "")
                            {
                                int L2 = Convert.ToInt32(row["L2"]);

                                if (value >= L2)
                                {
                                    lblVal.CssClass = lblVal.CssClass + " redblink";
                                    lblName.CssClass = lblVal.CssClass + " redblink";

                                    if (row["LV2"].ToString() != "")
                                    {
                                        lblScore.Text = "(" + row["LV2"].ToString() + ")";
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

        protected void repeatDashboard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string uName = row["Usercontrol_Name"].ToString();
                    string Method = row["Method"].ToString();
                    string X = row["X"].ToString();
                    string Y = row["Y"].ToString();
                    string Width = row["Width"].ToString();
                    string Height = row["Height"].ToString();

                    HtmlGenericControl divMain = e.Item.FindControl("divMain") as HtmlGenericControl;
                    PlaceHolder placeHolder = e.Item.FindControl("placeHolder") as PlaceHolder;

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

                    string usercontrolName = "~/Dashboard Charts/" + uName;
                    UserControl uc = (UserControl)Page.LoadControl(usercontrolName);
                    placeHolder.Controls.Add(uc);
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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Get_Dashboard();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}