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
    public partial class Code_Disapprove : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gridData_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[3].CssClass = "disp-none";
                e.Row.Cells[4].CssClass = "disp-none";
                e.Row.Cells[5].CssClass = "disp-none";
                e.Row.Cells[6].CssClass = "disp-none";
                e.Row.Cells[7].CssClass = "disp-none";
                e.Row.Cells[8].CssClass = "disp-none";
                e.Row.Cells[9].CssClass = "disp-none";
                e.Row.Cells[10].CssClass = "disp-none";
                e.Row.Cells[11].CssClass = "disp-none";
                e.Row.Cells[12].CssClass = "disp-none";
                e.Row.Cells[13].CssClass = "disp-none";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {

            try
            {
                DataSet dt = dal.DB_CODE_SP(ACTION: "GET_Disapproved", AUTOCODELIB: drpdictionary.SelectedValue);

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

        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ManualCode")
                {
                    Session["CODE_DICNM"] = drpdictionary.SelectedValue;

                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                    string AUTO_ID = e.CommandArgument.ToString();

                    Response.Redirect("Code_ManualCoding.aspx?AUTO_ID=" + AUTO_ID + "&AUTOCODELEB=" + drpdictionary.SelectedValue);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}