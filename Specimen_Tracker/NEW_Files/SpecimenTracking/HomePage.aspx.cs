using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class HomePage : System.Web.UI.Page
    {

        DAL_UMT dal_UMT = new DAL_UMT();

        public int col = 0;
        int test = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }

                if (!this.IsPostBack)
                {
                    //Added by Neeraj
                    HttpCookie cookie = new HttpCookie("Username");
                    cookie.Value = Session["USER_ID"].ToString();
                    // Optionally, set expiration and other properties
                    cookie.Expires = DateTime.Now.AddDays(30); // Example: cookie expires in 1 day
                    Response.Cookies.Add(cookie);


                    HttpCookie CookkieFullName = new HttpCookie("FullName");
                    CookkieFullName.Value = Session["User_Name"].ToString();
                    // Optionally, set expiration and other properties
                    CookkieFullName.Expires = DateTime.Now.AddDays(30); // Example: cookie expires in 1 day
                    Response.Cookies.Add(CookkieFullName);

                    this.GET_SYSTEM();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        private void PopulateMenuControl()
        {
            string CURRENT_SYSTEM = "";

            if (Session["UMT"] != null && Session["UMT"].ToString() == "YES")
            {
                if (Session["menu"] != null && Session["menu"].ToString() != "")
                {
                    CURRENT_SYSTEM = Session["menu"].ToString();
                }
                else
                {
                    CURRENT_SYSTEM = "Home";
                }

                DataSet ds = dal_UMT.SYS_FUNCTIONS_SP(
                        ACTION: "GET_FUNCTIONS",
                        SYSTEM: CURRENT_SYSTEM,
                        PARENT: CURRENT_SYSTEM
                        );

              //  lstmenu.DataSource = ds;
              //  lstmenu.DataBind();
              //  mainmenu.Visible = true;
            }
            else
            {
                DataSet ds = new DataSet();
                SqlConnection Sqlcon = new SqlConnection();
                Sqlcon = new SqlConnection(dal_UMT.getconstr());
                Sqlcon.Open();
                SqlDataAdapter da = new SqlDataAdapter("spQry_01_UserFunction", Sqlcon);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (Session["PROJECTID"] != null)
                {
                    da.SelectCommand.Parameters.AddWithValue("@ProjectID", Session["PROJECTID"].ToString());
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@ProjectID", "0");
                }
                da.SelectCommand.Parameters.AddWithValue("@UserId", Session["User_ID"].ToString());
                da.SelectCommand.Parameters.AddWithValue("@LevelID", "1");

                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                //lstmenu.DataSource = dt;
                //lstmenu.DataBind();
                Sqlcon.Close();
                //mainmenu.Visible = true;
            }

        }

        public void PopulateMenuControlChildItem(string strParentItem)
        {
            string CURRENT_SYSTEM = "";

            if (Session["UMT"] != null && Session["UMT"].ToString() == "YES")
            {
                if (Session["menu"] != null && Session["menu"].ToString() != "")
                {
                    CURRENT_SYSTEM = Session["menu"].ToString();
                }
                else
                {
                    CURRENT_SYSTEM = "Home";
                }

                DataSet ds = dal_UMT.SYS_FUNCTIONS_SP(
                        ACTION: "GET_FUNCTIONS",
                        SYSTEM: CURRENT_SYSTEM,
                        PARENT: strParentItem
                        );
               // lstsubmenu.DataSource = ds;
               // lstsubmenu.DataBind();


            }
            else
            {

                string MyValue = Session["User_ID"] as string;
                DataSet ds = new DataSet();
                SqlConnection Sqlcon = new SqlConnection();
                Sqlcon = new SqlConnection(dal_UMT.getconstr());
                Sqlcon.Open();
                SqlDataAdapter da = new SqlDataAdapter("spQry_01_UserFunction", Sqlcon);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (Session["PROJECTID"] != null)
                {
                    da.SelectCommand.Parameters.AddWithValue("@ProjectID", Session["PROJECTID"].ToString());
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@ProjectID", "0");
                }
                da.SelectCommand.Parameters.AddWithValue("@UserId", Session["User_ID"].ToString());
                da.SelectCommand.Parameters.AddWithValue("@Parent", strParentItem);

                da.Fill(ds);
                if (Session["PROJECTID"] == null)
                {
                    DataTable dt = ds.Tables[0];

                    DataRow[] rows;
                    DataRow[] rows1;
                    DataRow[] rows2;
                    DataRow[] rows3;
                    DataRow[] rows4;
                    DataRow[] rows5;
                    DataRow[] rows6;
                    DataRow[] rows7;
                    DataRow[] rows8;
                    DataRow[] rows9;
                    rows = dt.Select("FunctionName = 'Country Details'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    rows3 = dt.Select("FunctionName = 'Lab Details'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows3)
                        dt.Rows.Remove(row);
                    rows5 = dt.Select("FunctionName = 'Feasibility'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows5)
                        dt.Rows.Remove(row);
                    rows6 = dt.Select("FunctionName = 'Visit Details'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows6)
                        dt.Rows.Remove(row);
                    rows7 = dt.Select("FunctionName = 'Page Details'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows7)
                        dt.Rows.Remove(row);
                    rows8 = dt.Select("FunctionName = 'Subject Details'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows8)
                        dt.Rows.Remove(row);
                    rows9 = dt.Select("FunctionName = 'Inclusion Exclusion Criteria'");  //'UserName' is ColumnName
                    foreach (DataRow row in rows9)
                        dt.Rows.Remove(row);
                }
                //lstsubmenu.DataSource = ds;
                //lstsubmenu.DataBind();
                Sqlcon.Close();
            }

        }


        protected void btnPwdExpiryYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Change_Password.aspx");
        }
        private void GET_SYSTEM()
        {
            DataSet ds = dal_UMT.SYS_FUNCTIONS_SP(
                        ACTION: "GET_SYSTEMS"
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


                switch (span.InnerText)
                {
                    case "Shipment Manifest":

                        //lbl.Text = "10";
                        break;

                    case "Reports And Listings":
                        break;

                    case "Analyzing Laboratory":
                        break;

                    case "User Management":
                        break;

                    case "Data Entry":
                        break;


                }
            }
        }
    }
}