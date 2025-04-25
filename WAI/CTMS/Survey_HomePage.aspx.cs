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
    public partial class Survey_HomePage : System.Web.UI.Page
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
                }
                Get_Dashboard();
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

        private void Get_Dashboard()
        {
            try
            {
                DataSet ds1 = dal.Dashboard_SP(Action: "GET_Dashboard", User_ID: Session["User_ID"].ToString(), Project_ID: Session["PROJECTID"].ToString(), Section: "Survey");
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

        protected void repeatDashboard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string uName = row["Usercontrol_Name"].ToString();
                    string X = row["X"].ToString();
                    string Y = row["Y"].ToString();
                    string Width = row["Width"].ToString();
                    string Height = row["Height"].ToString();

                    HtmlGenericControl divMain = e.Item.FindControl("divMain") as HtmlGenericControl;
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
                    PlaceHolder placeHolder = e.Item.FindControl("placeHolder") as PlaceHolder;
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
            Session["DASHBOARD_SITE"] = drpSites.SelectedValue;
            Get_Dashboard();
        }
    }
}