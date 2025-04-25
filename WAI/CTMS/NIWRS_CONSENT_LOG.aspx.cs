using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;

namespace CTMS
{
    public partial class NIWRS_CONSENT_LOG : System.Web.UI.Page
    {
        DAL dal = new DAL(); 
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["PREV_URL"] = Request.RawUrl.ToString();
                    GET_LIST();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LIST()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_CONSENT_LOG");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblHeader.Text = ds.Tables[0].Rows[0]["LISTNAME"].ToString();

                    GET_LIST_DETAILS(ds.Tables[0].Rows[0]["ID"].ToString());
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LIST_DETAILS(string SOURCE_ID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_CONSENT_DETAILS", LISTID: SOURCE_ID);

                ds.Tables[0].Columns["Screening Id"].ColumnName = Session["SUBJECTTEXT"].ToString();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridData.DataSource = ds;
                    gridData.DataBind();
                }
                else
                {
                    gridData.DataSource = null;
                    gridData.DataBind();
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
    }
}