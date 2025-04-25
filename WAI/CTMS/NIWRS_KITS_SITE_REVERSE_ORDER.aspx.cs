using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class NIWRS_KITS_SITE_REVERSE_ORDER : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GET_COUNTRY();
                GET_SITE();
                GET_SITE_ORDERS_LIST();
                GET_SITE_ORDERS();
            }
        }

        private void GET_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP(USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlCountry.DataSource = ds.Tables[0];
                        ddlCountry.DataTextField = "COUNTRYNAME";
                        ddlCountry.DataValueField = "COUNTRYID";
                        ddlCountry.DataBind();
                    }
                    else
                    {
                        ddlCountry.DataSource = ds.Tables[0];
                        ddlCountry.DataTextField = "COUNTRYNAME";
                        ddlCountry.DataValueField = "COUNTRYID";
                        ddlCountry.DataBind();
                        ddlCountry.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSite.DataSource = ds.Tables[0];
                    ddlSite.DataValueField = "INVID";
                    ddlSite.DataBind();
                }
                ddlSite.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITE_ORDERS_LIST()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_COUNTRY_SP(ACTION: "GET_ORDER_DETAILS_COUNTRY", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlOrder.DataSource = ds.Tables[0];
                    ddlOrder.DataValueField = "ORDERID";
                    ddlOrder.DataBind();
                }
                ddlOrder.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITE_ORDERS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_ORDER_DETAILS_SITE", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue, ORDERID: ddlOrder.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvOrders.DataSource = ds;
                    gvOrders.DataBind();
                }
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SITE();
                GET_SITE_ORDERS_LIST();
                GET_SITE_ORDERS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SITE_ORDERS_LIST();
                GET_SITE_ORDERS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SITE_ORDERS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string ORDERID = dr["ORDERID"].ToString();
                    var anchorDiv = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("anchor");
                    DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_ORDER_DETAILS_SITE_KITS", ORDERID: ORDERID);
                    string KITCOUT = ds.Tables[0].Rows[0]["KITNO"].ToString();

                    GridView gvKits = (GridView)e.Row.FindControl("gvKits");

                    gvKits.DataSource = ds;
                    gvKits.DataBind();

                    Label TOTALKITS = (Label)e.Row.FindControl("TOTALKITS");
                    LinkButton lbtnCancelOrder = (LinkButton)e.Row.FindControl("lbtnCancelOrder");

                    Label CANCELEDBYNAME = (Label)e.Row.FindControl("CANCELEDBYNAME");
                    Label CANCELED_CAL_DAT = (Label)e.Row.FindControl("CANCELED_CAL_DAT");
                    Label CANCELED_CAL_TZDAT = (Label)e.Row.FindControl("CANCELED_CAL_TZDAT");
                    Label SHIPPED_CAL_DAT = (Label)e.Row.FindControl("SHIPPED_CAL_DAT");

                    if (KITCOUT.Trim() != "" && KITCOUT != "")
                    {
                        TOTALKITS.Text = gvKits.Rows.Count.ToString();
                        lbtnCancelOrder.Visible = true;
                    }
                    else
                    {
                        TOTALKITS.Text = "0";
                        CANCELEDBYNAME.Visible = true;
                        CANCELED_CAL_DAT.Visible = true;
                        CANCELED_CAL_TZDAT.Visible = true;
                        anchorDiv.Visible = false;
                    }
                    if (SHIPPED_CAL_DAT.Text.Trim() != "")
                    {
                        CANCELEDBYNAME.Visible = false;
                        CANCELED_CAL_DAT.Visible = false;
                        CANCELED_CAL_TZDAT.Visible = false;
                        lbtnCancelOrder.Visible = false;

                    }
                    else if (CANCELED_CAL_DAT.Text.Trim() == "" && SHIPPED_CAL_DAT.Text.Trim() == "")
                    {
                        lbtnCancelOrder.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string OrderID = e.CommandArgument.ToString();
                string Email_Code = "";

                string FROM = "";
                string TO = "";
                if (e.CommandName == "CancelOrder")
                {
                    int rowIndex = Convert.ToInt32(((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex);

                    GridViewRow row = gvOrders.Rows[rowIndex];
                    Label lblCountry = (Label)row.FindControl("COUNTRYNAME");
                    Label lblSiteID = (Label)row.FindControl("SITEID");

                    GET_SITE_REVERSE_ORDER(OrderID);

                    DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_SITE_COUNTRY", ORDERID: OrderID);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        FROM = ds.Tables[0].Rows[0]["FROM"].ToString();
                        TO = ds.Tables[0].Rows[0]["TO"].ToString();
                    }

                    //Response.Write("<script> alert('Order Cancelled Successfully.')</script>");
                    //Response.Redirect("NIWRS_ORDER_DETAILS_COUNTRY.aspx");

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Cancelled Successfully.'); window.location='NIWRS_KITS_SITE_REVERSE_ORDER.aspx';", true);

                    GET_SITE_ORDERS();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            
        }

        protected void gvKits_PreRender(object sender, EventArgs e)
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

        private void GET_SITE_REVERSE_ORDER(string OrderID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "IWRS_KIT_SITE_REVERSE_ORDER", ORDERID: OrderID);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
                lblHeader.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_SITE_SP(ACTION: "GET_ORDER_DETAILS_SITE_EXPORT", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue, ORDERID: ddlOrder.SelectedValue);
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}