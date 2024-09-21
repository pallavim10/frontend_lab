using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SpecimenTracking.App_Code;
using Newtonsoft.Json;

namespace SpecimenTracking
{
    public partial class UserManagementDashboard : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    Get_Dashboard();
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
                 ex.Message.ToString();
            }
        }
        public int col = 0;
        //protected void lstTile_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    try
        //    {
        //        System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");
        //        int itemIndex = e.Item.DataItemIndex;

        //        string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };
        //        if (e.Item.ItemType == ListViewItemType.DataItem)
        //        {
        //            if (col == 13)
        //            {
        //                col = 0;
        //            }
        //            divBox.Attributes.Add("class", color[col]);
        //            col++;
        //        }

        //        string barinfo = "";

        //        HiddenField hfBarData = (HiddenField)e.Item.FindControl("hfBarData");

        //        DataRowView drv = (DataRowView)e.Item.DataItem;
        //        string NAME = drv["NAME"].ToString();

        //        DataSet ds = dal_UMT.UMT_DASH_SP(ACTION: "GET_TILE_DETAILS", NAME: NAME);

        //        if (ds.Tables.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[0];
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                barinfo += "{'COLUMN': '" + row["NAME"].ToString() + "', 'VALUE': " + row["COUNTS"].ToString() + " },";
        //            }
        //            hfBarData.Value = "[" + barinfo.TrimEnd(',') + "]";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblErrorMsg.Text =
        //        ex.Message.ToString();
        //    }
        //}

        protected void lstTile_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                HiddenField hfBarData = (HiddenField)e.Item.FindControl("hfBarData");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string NAME = drv["NAME"].ToString();

                DataSet ds = dal_UMT.UMT_DASH_SP(ACTION: "GET_TILE_DETAILS", NAME: NAME);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    List<Dictionary<string, object>> barInfo = new List<Dictionary<string, object>>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        barInfo.Add(new Dictionary<string, object>
                {
                    { "COLUMN", row["NAME"].ToString() },
                    { "VALUE", row["COUNTS"].ToString() }
                });
                    }

                    // Serialize the list to JSON and assign it to the hidden field
                    hfBarData.Value = JsonConvert.SerializeObject(barInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}