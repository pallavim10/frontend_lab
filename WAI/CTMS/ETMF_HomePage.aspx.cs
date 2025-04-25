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
    public partial class ETMF_HomePage : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    if (!this.Page.IsPostBack)
                    {
                        COUNTRY();
                        SITE_AGAINST_COUNTRY();
                        GET_OVERALL_TILES();
                        GET_ZONEWISE_COUNTS();
                        BIND_TYPE();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_TYPE()
        {
            try
            {
                ddlViewby.Items.Add(new ListItem("--Select--", "0"));

                DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "TYPE_EVENT");
                string Event = ds.Tables[0].Rows[0]["Event"].ToString();
                if (Event != "0")
                {
                    ddlViewby.Items.Add(new ListItem("Event", "1"));
                }

                DataSet ds1 = dal_eTMF.eTMF_DATA_SP(ACTION: "TYPE_MILESTONE");
                string Milestone = ds1.Tables[0].Rows[0]["Milestone"].ToString();
                if (Milestone != "0")
                {
                    ddlViewby.Items.Add(new ListItem("Milestone", "2"));
                }

                if (Event != "0" || Milestone != "0")
                {
                    divGroupMetrics.Visible = true;
                }
                else
                {
                    divGroupMetrics.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlViewby_SelectedIndexChanged(object sender, EventArgs e)
        {
            BIND_DOCUMENTTYPE();
        }

        private void BIND_DOCUMENTTYPE()
        {
            try
            {
                if (ddlViewby.SelectedItem.Text == "Event")
                {
                    lblViewType.Text = "Select" + " " + ddlViewby.SelectedItem.Text + " :";
                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_DocType_Event");

                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (ddlViewby.SelectedItem.Text == "Milestone")
                {
                    lblViewType.Text = "Select" + " " + ddlViewby.SelectedItem.Text + " :";
                    DataSet ds = dal_eTMF.eTMF_DATA_SP(ACTION: "get_DocType_Milestone");

                    drpDocType.DataSource = ds.Tables[0];
                    drpDocType.DataValueField = "ID";
                    drpDocType.DataTextField = "DocType";
                    drpDocType.DataBind();
                    drpDocType.Items.Insert(0, new ListItem("--Select--", "0"));
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

        private void GET_ZONEWISE_COUNTS()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_REPORT_SP(ACTION: "GET_ZONEWISE_COUNTS",
                    CountryID: drpCountry.SelectedValue,
                    SiteID: drpSites.SelectedValue
                    );
                lstTile.DataSource = ds;
                lstTile.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_OVERALL_TILES()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_REPORT_SP(ACTION: "GET_OVERALL_TILES", CountryID: drpCountry.SelectedValue, SiteID: drpSites.SelectedValue);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    switch (dr["TILE"].ToString())
                    {
                        case "TotalUploadCount":
                            lblTotalUploadCount.Text = dr["Count"].ToString();
                            break;

                        case "Current":
                            lblCurrentCount.Text = dr["Count"].ToString();
                            break;

                        case "Old":
                            lblOldCount.Text = dr["Count"].ToString();
                            break;

                        case "PendingQC":
                            lblPendingQCCount.Text = dr["Count"].ToString();
                            break;

                        case "Upload1Month":
                            lblUpload1MonthCount.Text = dr["Count"].ToString();
                            break;

                        case "Upload15":
                            lblUpload15Count.Text = dr["Count"].ToString();
                            break;

                        case "Upload7":
                            lblUpload7Count.Text = dr["Count"].ToString();
                            break;

                        case "Upload24":
                            lblUpload24Count.Text = dr["Count"].ToString();
                            break;

                        case "Exp3Month":
                            lblExp3MonthCount.Text = dr["Count"].ToString();
                            break;

                        case "Exp1Month":
                            lblExp1MonthCount.Text = dr["Count"].ToString();
                            break;

                        case "Exp15Days":
                            lblExp15DaysCount.Text = dr["Count"].ToString();
                            break;

                        case "Exp7Days":
                            lblExp7DaysCount.Text = dr["Count"].ToString();
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public int col = 0;

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SITE_AGAINST_COUNTRY();
                Session["DASHBOARD_COUNTRYID"] = drpCountry.SelectedValue;

                GET_ZONEWISE_COUNTS();
                GET_OVERALL_TILES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["DASHBOARD_SITE"] = drpSites.SelectedValue;
            GET_ZONEWISE_COUNTS();
            GET_OVERALL_TILES();
        }

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

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_REPORT_SP(ACTION: "GET_GROUPIWSE_COUNTS", GroupID: drpDocType.SelectedValue);
                lstGroups.DataSource = ds;
                lstGroups.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}