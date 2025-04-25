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
    public partial class NIWRS_KITS_EXP_REPORT : System.Web.UI.Page
    {
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_EXPIRY_REPORT();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_EXPIRY_REPORT()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "GET_EXPIRY_REPORT");
                gvREUQESTS.DataSource = ds;
                gvREUQESTS.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvREUQESTS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    Label lbl_NIWRS_KITS_POOL = (Label)e.Row.FindControl("lbl_NIWRS_KITS_POOL");
                    Label lbl_NIWRS_KITS_COUNTRY_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_COUNTRY_ORDERS");
                    Label lbl_NIWRS_KITS_COUNTRY = (Label)e.Row.FindControl("lbl_NIWRS_KITS_COUNTRY");
                    Label lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS");
                    Label lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS");
                    Label lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS");
                    Label lbl_NIWRS_KITS_SITE_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_SITE_ORDERS");
                    Label lbl_NIWRS_KITS_SITE = (Label)e.Row.FindControl("lbl_NIWRS_KITS_SITE");
                    Label lbl_NIWRS_KITS_SITE_TRANSF_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_SITE_TRANSF_ORDERS");

                    GridView grd_NIWRS_KITS_POOL = (GridView)e.Row.FindControl("grd_NIWRS_KITS_POOL");
                    GridView grd_NIWRS_KITS_COUNTRY_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_COUNTRY_ORDERS");
                    GridView grd_NIWRS_KITS_COUNTRY = (GridView)e.Row.FindControl("grd_NIWRS_KITS_COUNTRY");
                    GridView grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS");
                    GridView grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS");
                    GridView grd_NIWRS_KITS_SITE_COUNTRY_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_SITE_COUNTRY_ORDERS");
                    GridView grd_NIWRS_KITS_SITE_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_SITE_ORDERS");
                    GridView grd_NIWRS_KITS_SITE = (GridView)e.Row.FindControl("grd_NIWRS_KITS_SITE");
                    GridView grd_NIWRS_KITS_SITE_TRANSF_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_SITE_TRANSF_ORDERS");

                    DataSet ds = dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "GET_EXPIRY_REPORT_DETAILS", ID: drv["ID"].ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_POOL.DataSource = ds.Tables[0];
                        grd_NIWRS_KITS_POOL.DataBind();
                        lbl_NIWRS_KITS_POOL.Text = grd_NIWRS_KITS_POOL.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_POOL.DataSource = null;
                        grd_NIWRS_KITS_POOL.DataBind();
                        lbl_NIWRS_KITS_POOL.Text = "0";
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataSource = ds.Tables[1];
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_ORDERS.Text = grd_NIWRS_KITS_COUNTRY_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_ORDERS.Text = "0";
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY.DataSource = ds.Tables[2];
                        grd_NIWRS_KITS_COUNTRY.DataBind();
                        lbl_NIWRS_KITS_COUNTRY.Text = grd_NIWRS_KITS_COUNTRY.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY.DataBind();
                        lbl_NIWRS_KITS_COUNTRY.Text = "0";
                    }

                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataSource = ds.Tables[3];
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.Text = grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.Text = "0";
                    }

                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataSource = ds.Tables[4];
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.Text = grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.Text = "0";

                    }

                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataSource = ds.Tables[5];
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS.Text = grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS.Text = "0";
                    }

                    if (ds.Tables[6].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE_ORDERS.DataSource = ds.Tables[6];
                        grd_NIWRS_KITS_SITE_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_ORDERS.Text = grd_NIWRS_KITS_SITE_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_SITE_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_ORDERS.Text = "0";
                    }

                    if (ds.Tables[7].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE.DataSource = ds.Tables[7];
                        grd_NIWRS_KITS_SITE.DataBind();
                        lbl_NIWRS_KITS_SITE.Text = grd_NIWRS_KITS_SITE.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE.DataSource = null;
                        grd_NIWRS_KITS_SITE.DataBind();
                        lbl_NIWRS_KITS_SITE.Text = "0";
                    }

                    if (ds.Tables[8].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataSource = ds.Tables[8];
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_TRANSF_ORDERS.Text = grd_NIWRS_KITS_SITE_TRANSF_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_TRANSF_ORDERS.Text = "0";
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "GET_EXPIRY_REPORT_EXPORT");

                string xlname = "IWRS_Kit Expiry Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);

                Response.Write("<script> alert('Report Exported Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}