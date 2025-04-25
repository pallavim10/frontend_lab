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
    public partial class CODE_DISAPPROVED_MEDDRA : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GETDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GETDATA()
        {
            try
            {
                DataSet dt = dal.CODE_REPORTS_SP(
                            ACTION: "GET_CODE_DISAPPROVED_MEDDRA"
                            );

                if (dt.Tables[0].Rows.Count > 0)
                {
                    grdData.DataSource = dt;
                    grdData.DataBind();
                }
                else
                {
                    grdData.DataSource = null;
                    grdData.DataBind();
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

        protected void lblDisAppCodeMeddraExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "MedDRA Code DisApproved";

                DataSet dt = dal.CODE_REPORTS_SP(
                            ACTION: "GET_CODE_DISAPPROVED_MEDDRA"
                            );
                
                Multiple_Export_Excel.ToExcel(dt, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}