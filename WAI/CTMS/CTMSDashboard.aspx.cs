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
    public partial class CTMSDashboard : System.Web.UI.Page
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
                //DataSet ds1 = dal.Dashboard_SP(Action: "GET_Dashboard", User_ID: Session["User_ID"].ToString(), Project_ID: Session["PROJECTID"].ToString(), Section: "CTMS");
                DataSet ds1 = dal.DASHBOARD_ASSIGNING(Action: "GET_Dashboard",
                   USERID: Session["User_ID"].ToString(),
                   PROJECTID: Session["PROJECTID"].ToString(),
                   TYPENAME: "CTMS"
                   );

                repeatDashboard.DataSource = ds1.Tables[0];
                repeatDashboard.DataBind();

                repeatTiles.DataSource = ds1.Tables[1];
                repeatTiles.DataBind();
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

        public int col = 0;
        protected void repeatTiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                    HtmlGenericControl divBox = (HtmlGenericControl)e.Item.FindControl("divBox");
                    Label lblVal = e.Item.FindControl("lblVal") as Label;
                    Label lblName = e.Item.FindControl("lblName") as Label;
                    Label lblScore = e.Item.FindControl("lblScore") as Label;
                    HiddenField hfIndicID = e.Item.FindControl("hfIndicID") as HiddenField;
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

                    if (uName != "")
                    {
                        string usercontrolName = "~/Dashboard_Master/" + uName;
                        UserControl uc = (UserControl)Page.LoadControl(usercontrolName);
                        placeHolder.Controls.Add(uc);
                        divBox.Visible = false;
                    }
                    else
                    {
                        placeHolder.Visible = false;
                        string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                        if (col == 13)
                        {
                            col = 0;
                        }
                        divBox.Attributes.Add("class", color[col]);
                        col++;

                        if (Method != "")
                        {
                            ds = dal.Dashboard_SP(Action: Method, Project_ID: Session["PROJECTID"].ToString(), INVID: Session["DASHBOARD_SITE"].ToString(),
                               COUNTRYID: Session["DASHBOARD_COUNTRYID"].ToString(), User_ID: Session["User_ID"].ToString());


                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0][0].ToString() != "")
                                {
                                    lblVal.Text = dt.Rows[0][0].ToString();
                                }
                                else
                                {
                                    lblVal.Text = "0";
                                }

                                lblName.Text = row["Chart_Name"].ToString();
                            }
                            else
                            {
                                lblVal.Text = "0";
                                lblName.Text = row["Chart_Name"].ToString();
                            }
                        }
                        else
                        {
                            lblVal.Text = "0";
                            lblName.Text = row["Chart_Name"].ToString();
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

                    string usercontrolName = "~/Dashboard_Master/" + uName;
                    UserControl uc = (UserControl)Page.LoadControl(usercontrolName);
                    placeHolder.Controls.Add(uc);
                }
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
    }
}