using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using PPT;
using System.Data.SqlClient;

namespace CTMS
{
    public partial class Master_HomePage : System.Web.UI.Page
    {
        DAL dal = new DAL();
        public int col = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["PVID"] = null;
                PopulateMenuControl();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void PopulateMenuControl()
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    string MyValue = Session["User_ID"] as string;
                    DataSet ds = new DataSet();
                    SqlConnection Sqlcon = new SqlConnection();
                    Sqlcon = new SqlConnection(dal.getconstr());
                    Sqlcon.Open();
                    SqlDataAdapter da = new SqlDataAdapter("spQry_01_UserFunction", Sqlcon);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProjectID", "0");
                    da.SelectCommand.Parameters.AddWithValue("@UserId", Session["User_ID"].ToString());
                    da.SelectCommand.Parameters.AddWithValue("@LevelID", "1");
                    //da.SelectCommand.Parameters.AddWithValue("@Parent", "2");
                    da.Fill(ds);
                    DataTable dt = ds.Tables[0];


                    DataRow[] rows;
                    DataRow[] rows1;
                    DataRow[] rows2;
                    DataRow[] rows3;
                    DataRow[] rows4;
                    rows = dt.Select("FunctionName = 'Home'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    rows1 = dt.Select("FunctionName = 'Masters'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows1)
                        dt.Rows.Remove(row);
                    rows2 = dt.Select("FunctionName = 'Dashboard'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows2)
                        dt.Rows.Remove(row);


                    //rows3 = dt.Select("FunctionName = 'Regulatory'");  //'UserName' is ColumnName
                    //foreach (DataRow row in rows3)
                    // dt.Rows.Remove(row);

                    rows4 = dt.Select("FunctionName = 'Customized AE Filters'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows4)
                        dt.Rows.Remove(row);

                    lstm.DataSource = null;
                    lstm.DataBind();
                    Sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // lblErrorMsg.Text = ex.ToString();
            }


        }

        protected void lstm_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("a1");
                Session["menu"] = a1.Title;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void lstm_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                HtmlAnchor main = (HtmlAnchor)e.Item.FindControl("main");
                System.Web.UI.HtmlControls.HtmlGenericControl divcol = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divcol");
                int itemIndex = e.Item.DataItemIndex;

                string[] color = { "small-box bg-aqua", "small-box bg-green", "small-box bg-yellow", "small-box bg-red", "small-box bg-blue", "small-box bg-maroon", "small-box bg-aqua", "small-box bg-green", "small-box bg-red", "small-box bg-aqua", "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-green", "small-box bg-yellow", "small-box bg-red", "small-box bg-yellow", };
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    divcol.Attributes.Add("class", color[col]);
                    col++;
                }
                string title = main.Title.ToString();
                if (title == "Manage Products")
                {
                    main.HRef = "Therapetic_Master.aspx?menu=" + title + "";
                }
                else if (title == "Manage Projects")
                {
                    main.HRef = "ADD_PROJECT_MASTER.aspx?menu=" + title + "";
                }
                else if (title == "User Group Management")
                {
                    main.HRef = "ADD_PROJECT_MASTER.aspx?menu=" + title + "";
                }
                else if (title == "User Management")
                {
                    main.HRef = "ADD_PROJECT_MASTER.aspx?menu=" + title + "";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}