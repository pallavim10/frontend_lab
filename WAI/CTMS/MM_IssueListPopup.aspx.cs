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
    public partial class MM_IssueListPopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    Session["INVID"] = Request.QueryString["INVID"];
                    Session["SUBJID"] = Request.QueryString["Subject"];
                    getData();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
       
        private void getData()
        {
            try
            {

                DAL dal;
                dal = new DAL();
                DataSet ds = new DataSet();
                ds = dal.getsetISSUES(
                Action: "GET_MEDICAL_ISSUE",
                Project_ID: Session["ProjectId"].ToString(),
                INVID:  Session["INVID"].ToString(),
                SUBJID:  Session["SUBJID"].ToString()
                );
                grdISSUES.DataSource = ds;
                grdISSUES.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

     
        protected void grdISSUES_PreRender(object sender, EventArgs e)
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
    }
}