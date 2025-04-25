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
    public partial class LabDataDashboard : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    BIND_CAT_DASHBOARD();
                    BIND_GRADE_DASHBOARD();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BIND_CAT_DASHBOARD()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal.MM_LISTINGS_SP(ACTION: "BIND_LAB_CAT_DASHBOARD_DATA", CATEGORYID: ddlCategory.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdCATEGORYDASH.DataSource = ds;
                    grdCATEGORYDASH.DataBind();
                }
                else
                {
                    grdCATEGORYDASH.DataSource = null;
                    grdCATEGORYDASH.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
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

        protected void BIND_GRADE_DASHBOARD()
        {
            try
            {
                DataSet ds = dal.MM_LISTINGS_SP(ACTION: "BIND_LAB_GRADE_DASHBOARD_DATA", Grade: ddlGrade.SelectedValue);

                if (ds.Tables.Count > 0)
                {
                    grdGRADEDASHBOARD.DataSource = ds;
                    grdGRADEDASHBOARD.DataBind();
                }
                else
                {
                    grdGRADEDASHBOARD.DataSource = null;
                    grdGRADEDASHBOARD.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdGRADEDASHBOARD_PreRender(object sender, EventArgs e)
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

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_CAT_DASHBOARD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_GRADE_DASHBOARD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}