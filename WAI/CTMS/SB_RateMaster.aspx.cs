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
    public partial class SB_RateMaster : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_GV();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_GV()
        {
            try
            {
                DataSet ds = dal.SiteBudget_SP(Action: "get_Task_Rate", Project_Id: Session["PROJECTID"].ToString());
                gvRates.DataSource = ds.Tables[0];
                gvRates.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvRates_PreRender(object sender, EventArgs e)
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

        protected void gvRates_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvRates_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvRates.Rows[e.RowIndex];
            string Task_ID = gvRates.DataKeys[e.RowIndex].Values[1].ToString();
            string Sub_Task_ID = gvRates.DataKeys[e.RowIndex].Values[2].ToString();
            string name = (row.Cells[2].Controls[0] as TextBox).Text;
            string country = (row.Cells[3].Controls[0] as TextBox).Text;
            dal.SiteBudget_SP(Action: "insert_Rate");
        }

        protected void gvRates_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRates.EditIndex = -1;
            this.bind_GV();
        }

        protected void gvRates_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    TextBox tbox = new TextBox();
                    tbox.ID = "TextBox1";
                    cell.Controls.Add(tbox);
                }
            }            

            Label lbl = new Label();
            e.Row.Cells[2].Controls.Add(lbl);
        }
    }
}