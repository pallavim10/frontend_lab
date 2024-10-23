using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SpecimenTracking.App_Code;

namespace SpecimenTracking
{
    public partial class UserManagementDashboard : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        public class IconData
        {
            public string BgColor { get; set; }
            public string infoBoxIcon { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    this.Get_Dashboard();
                    
                }
                        
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        private void Get_Dashboard()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_DASH_SP(ACTION: "GET_TILES");
                lstTile.DataSource = ds;
                lstTile.DataBind();
            }
            catch (Exception ex)
            {
                 ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void GET_ICONS(ListViewItem Item,string FunctionName) 
        {
            try
            {
                string iconinfo = "";
                HiddenField hfIconData = (HiddenField)Item.FindControl("hfIconData");
                DataSet ds = dal_UMT.UMT_DASH_SP(ACTION: "GET_ICONS", NAME: FunctionName);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        iconinfo += "{'BgColor': '" + row["Color"].ToString() + "', 'infoBoxIcon': " + row["Icon"].ToString() + " },";
                    }
                    hfIconData.Value = "[" + iconinfo.TrimEnd(',') + "]";
                  
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }

        }
        public int col = 0;
        protected void lstTile_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");
                int itemIndex = e.Item.DataItemIndex;

                
                
                string barinfo = "";

                HiddenField hfBarData = (HiddenField)e.Item.FindControl("hfBarData");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string NAME = drv["NAME"].ToString();

                DataSet ds = dal_UMT.UMT_DASH_SP(ACTION: "GET_TILE_DETAILS", NAME: NAME);
                
                if (ds.Tables.Count > 0)
                {
                    
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            barinfo += "{'COLUMN': '" + ds.Tables[0].Rows[i]["NAME"].ToString() + "', 'VALUE': " + ds.Tables[0].Rows[i]["COUNTS"].ToString() + " },";
                        }

                        hfBarData.Value = "[" + barinfo.TrimEnd(',') + "]";
                        GET_ICONS(e.Item, NAME);
                    }
                }
            }
            catch (Exception ex)
            {
                //lblErrorMsg.Text =
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        
    }
}