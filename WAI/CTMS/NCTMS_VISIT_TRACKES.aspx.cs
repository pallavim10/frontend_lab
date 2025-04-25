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
    public partial class NCTMS_VISIT_TRACKES : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_TRACKERS_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_TRACKERS_DATA()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_TRACKERS_MODULES",
                SVID: Request.QueryString["SVID"].ToString(),
                VISITID: Request.QueryString["VISITID"].ToString()
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeatData.DataSource = ds;
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
                    DataRowView row = (DataRowView)e.Item.DataItem;

                    GridView grd_Records = (GridView)e.Item.FindControl("grd_Records");

                    DataSet ds = dal.CTMS_DATA_SP(
                        ACTION: "GET_TRACKER_DATA",
                        SVID: Request.QueryString["SVID"].ToString(),
                        VISITID: Request.QueryString["VISITID"].ToString(),
                        MODULEID: row["MODULEID"].ToString(),
                        TABLENAME: row["TABLENAME"].ToString()
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_Records.DataSource = ds;
                        grd_Records.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvEmp_PreRender(object sender, EventArgs e)
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

        protected void grd_Records_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lnkPAGENUM = (LinkButton)e.Row.FindControl("lnkPAGENUM");

                GridView grd_Records = (GridView)sender;

                string DELETE = dr["DELETE"].ToString();

                if (DELETE == "True")
                {
                    e.Row.Attributes.Add("class", "strikeThrough");
                }

                grd_Records.HeaderRow.Cells[0].Visible = false;
                e.Row.Cells[0].Visible = false;
            }

        }
    }
}