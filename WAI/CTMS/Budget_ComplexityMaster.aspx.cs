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
    public partial class Budget_ComplexityMaster : System.Web.UI.Page
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

        public void bind_GV()
        {
            try
            {
                DataSet ds = dal.Budget_SP(Action: "pivot_Complexity");
                gvComplexity.DataSource = ds.Tables[0];
                gvComplexity.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvComplexity.Rows.Count; i++)
                {
                    string Role_Id = ((Label)gvComplexity.Rows[i].FindControl("lblRole")).Text;
                    string Rate1 = ((TextBox)gvComplexity.Rows[i].FindControl("txtRate1")).Text;
                    string Rate2 = ((TextBox)gvComplexity.Rows[i].FindControl("txtRate2")).Text;
                    string Rate3 = ((TextBox)gvComplexity.Rows[i].FindControl("txtRate3")).Text;
                    string Rate4 = ((TextBox)gvComplexity.Rows[i].FindControl("txtRate4")).Text;
                    string Rate5 = ((TextBox)gvComplexity.Rows[i].FindControl("txtRate5")).Text;
                    string Rate6 = ((TextBox)gvComplexity.Rows[i].FindControl("txtRate6")).Text;
                    string Rate7 = ((TextBox)gvComplexity.Rows[i].FindControl("txtRate7")).Text;
                    string Rate8 = ((TextBox)gvComplexity.Rows[i].FindControl("txtRate8")).Text;
                    string Rate9 = ((TextBox)gvComplexity.Rows[i].FindControl("txtRate9")).Text;
                    string Rate10 = ((TextBox)gvComplexity.Rows[i].FindControl("txtRate10")).Text;
                    dal.Budget_SP(Action: "update_Complex",
                    Role_ID: Role_Id,
                    Rate1: Rate1, 
                    Rate2: Rate2, 
                    Rate3: Rate3, 
                    Rate4: Rate4, 
                    Rate5: Rate5, 
                    Rate6: Rate6, 
                    Rate7: Rate7, 
                    Rate8: Rate8, 
                    Rate9: Rate9, 
                    Rate10: Rate10
                    );
                }
                Response.Write("<script> alert('Record Updated successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvComplexity_PreRender(object sender, EventArgs e)
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