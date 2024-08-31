using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class SETUPDashboard : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GET_SETUP_SYSTEM();

            }

        }

        private void GET_SETUP_SYSTEM()
        {
            DataSet ds = dal_UMT.SYS_FUNCTIONS_SP(
                        ACTION: "GET_SETUP_SYSTEM", PARENT: "Set-Ups"
                        );

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lstm.DataSource = ds;
                lstm.DataBind();
            }
            else
            {
                lstm.DataSource = null;
                lstm.DataBind();
            }

        }

        protected void lstm_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                // Find the Label control inside the current ListView item
                Label lbl = (Label)e.Item.FindControl("lbltotal");
                HtmlGenericControl span = (HtmlGenericControl)e.Item.FindControl("FunctionName");
                

                switch(span.InnerText)
                {
                    case "Master Upload":

                        lbl.Text = "10";
                        break;

                    case "Manage Fields":
                        break;

                    case "Manage Criteria":
                        break;

                    case "Manage Visit":
                        break;

                    case "Manage Aliquots":
                        break;

                   
                }
            }
        }
    }
}