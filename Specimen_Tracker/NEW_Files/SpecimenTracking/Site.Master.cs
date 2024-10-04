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
    public partial class Site : System.Web.UI.MasterPage
    {

        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    //UserName.InnerText = Session["User_Name"].ToString();
                    lblUserName.Text = Session["User_Name"].ToString();
                    Lbl_User_Dept.Text = Session["StudyRole"].ToString();
                    if (!IsPostBack)
                    {

                        if (Session["PROJECTIDTEXT"] != null)
                        {
                            lblPROJECT.Text = Session["PROJECTIDTEXT"].ToString();
                            anchorproj.InnerText = Session["PROJECTIDTEXT"].ToString();
                            anchorproj.HRef = "HomePage.aspx?menu=Home";
                            anchorproj.Style.Add("font-family", "Arial Rounded MT");
                        }
                        else
                        {
                            anchorproj.HRef = "Master_HomePage.aspx?menu=Home";
                            anchorproj.InnerText = "Home";
                            anchorproj.Style.Add("font-family", "Arial Rounded MT");
                        }
                        

                        if (Request["val"] == "home")
                        {
                            Session["menu"] = "Home";
                        }
                        else if (Request["menu"] != null)
                        {
                            if (Request["menu"].ToString() == "Home")
                            {
                                Session["menu"] = Request["menu"].ToString();
                                PopulateMenuControl();
                            }
                            else
                            {
                                Session["menu"] = Request["menu"].ToString();
                            }

                        }

                        
                        HttpCookie NavigationPath_Cookies = Request.Cookies["NavigationPath"];
                        string NavigationPath = NavigationPath_Cookies != null ? NavigationPath_Cookies.Value.Split('=')[1] : "undefined";

                        if (Session["menu"] != null && Session["menu"].ToString() != "Home")
                        {
                            PopulateMenuControlChildItem(Session["menu"].ToString());
                            // lblnavmenu.Text = Session["menu"].ToString();

                            if (NavigationPath != "" && NavigationPath != "undefined")
                            {
                                //lblnavmenuuName.Text = NavigationPath;
                            }
                        }
                        else
                        {
                            HttpCookie nameCookie = new HttpCookie("NavigationPath");

                            nameCookie.Values["NavigationPath"] = "";

                            HttpContext.Current.Response.Cookies.Add(nameCookie);

                            PopulateMenuControl();
                        }

                        // Get_DashboardData();
                    }
                }
            }
            catch (Exception ex)
            {
                //Lbl_User_Dept.Text = "";
                //Lbl_User_Dept.Text = ex.Message;
                ex.StackTrace.ToString();
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

                lstmenu.DataSource = ds;
                lstmenu.DataBind();
                mainmenu.Visible = true;
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

                lstmenu.DataSource = dt;
                lstmenu.DataBind();
                Sqlcon.Close();
                mainmenu.Visible = true;
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
                lstsubmenu.DataSource = ds;
                lstsubmenu.DataBind();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Session["UserGroup_ID"] = dr["RoleID"].ToString().Trim();
                    Session["UserGroupID"] = dr["RoleID"].ToString().Trim();

                    lblUserRole.Text = dr["RoleName"].ToString().Trim();
                }

                if (CURRENT_SYSTEM == "Home")
                {
                    lblUserRole.Text = "";
                }

                if (lblUserRole.Text != "")
                {
                    divUserRole.Visible = true;
                }

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
                lstsubmenu.DataSource = ds;
                lstsubmenu.DataBind();
                Sqlcon.Close();
            }

        }

        protected void lstmenu_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRowView dr = (DataRowView)DataBinder.GetDataItem(e.Item);

            LinkButton lbtnmenu = (LinkButton)e.Item.FindControl("lbtnmenu");
            Label lblmenu = (Label)e.Item.FindControl("lblmenu");
            ListView lstchild = (ListView)e.Item.FindControl("lstchild");
            LinkButton child = (LinkButton)e.Item.FindControl("child");
            ListView lstsubchild = (ListView)e.Item.FindControl("lstsubchild");
            HtmlAnchor amain = (HtmlAnchor)e.Item.FindControl("amain");
            HtmlControl ICON = (HtmlControl)e.Item.FindControl("ICONCLASS");

            string title = amain.Title.ToString();
            if (title == "Masters")
            {
                if (Session["PROJECTID"] != null)
                {
                    amain.HRef = "HomePage.aspx?menu=" + title + "";
                }
                else
                {
                    amain.HRef = "Master_HomePage.aspx?menu=" + title + "";
                }
            }


            if (Session["UMT"] != null && Session["UMT"].ToString() == "YES")
            {
                amain.HRef = dr["NavigationURL"].ToString();
                ICON.Attributes.Add("class", dr["Icon"].ToString());
            }
        }

        protected void lstmenu_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string menu = e.CommandArgument.ToString();
            if (e.CommandName == "menu")
            {
                if (menu == "Home")
                {
                    mainmenu.Visible = true;
                    sub.Visible = false;
                    PopulateMenuControl();
                    Session["menu"] = menu;
                }
                else
                {
                    mainmenu.Visible = false;
                    sub.Visible = true;
                    Session["menu"] = menu;
                    PopulateMenuControlChildItem(menu);

                }

            }
        }

        string NavPath_L1 = "", NavPath_L2 = "", NavPath_L3 = "", NavPath_L4 = "";

        protected void lstsubmenu_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DataRowView dr = (DataRowView)DataBinder.GetDataItem(e.Item);
                LinkButton lbtnmenu = (LinkButton)e.Item.FindControl("lbtnmenu");
                ListView lstsubmenu1 = (ListView)e.Item.FindControl("lstsubmenu1");
                HtmlGenericControl ul1 = (HtmlGenericControl)e.Item.FindControl("ul1");
                HtmlGenericControl lisub = (HtmlGenericControl)e.Item.FindControl("lisub");
                HtmlControl i1 = (HtmlControl)e.Item.FindControl("i1");
                HiddenField hdnPath = (HiddenField)e.Item.FindControl("hdnPath");
                HtmlControl ICONSubCLASS = (HtmlControl)e.Item.FindControl("ICONSubCLASS");

                NavPath_L1 = lbtnmenu.Text.Trim();
                hdnPath.Value = NavPath_L1;

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
                            PARENT: lbtnmenu.Text
                            );
                    lstsubmenu1.DataSource = ds;
                    lstsubmenu1.DataBind();
                    ICONSubCLASS.Attributes.Add("class", dr["Icon"].ToString());
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lisub.Attributes.Add("class", "nav-item has-treeview");
                        i1.Attributes.Add("class", "fas fa-angle-left right pull-right");
                    }
                    else
                    {
                        ul1.Attributes.Add("class", "d-none");
                    }
                }
                else
                {
                    string MyValue1 = Session["User_ID"] as string;
                    DataSet ds1 = new DataSet();
                    SqlConnection Sqlcon = new SqlConnection();
                    Sqlcon = new SqlConnection(dal_UMT.getconstr());
                    Sqlcon.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter("spQry_01_UserFunction", Sqlcon);
                    da1.SelectCommand.CommandType = CommandType.StoredProcedure;
                    if (Session["PROJECTID"] != null)
                    {
                        da1.SelectCommand.Parameters.AddWithValue("@ProjectID", Session["PROJECTID"].ToString());
                    }
                    else
                    {
                        da1.SelectCommand.Parameters.AddWithValue("@ProjectID", "0");
                    }
                    da1.SelectCommand.Parameters.AddWithValue("@UserId", Session["User_ID"].ToString());
                    da1.SelectCommand.Parameters.AddWithValue("@Parent", lbtnmenu.Text);
                    da1.Fill(ds1);
                    DataTable dt = ds1.Tables[0];



                    if (Session["PROJECTID"] == null)
                    {
                        DataRow[] rows;
                        DataRow[] rows1;
                        DataRow[] rows2;
                        rows = dt.Select("FunctionName = 'Activate Site'");  //'UserName' is ColumnName
                        foreach (DataRow row in rows)
                            dt.Rows.Remove(row);
                        rows1 = dt.Select("FunctionName = 'Add User DashBoard'");  //'UserName' is ColumnName
                        foreach (DataRow row in rows1)
                            dt.Rows.Remove(row);
                        rows2 = dt.Select("FunctionName = 'Remove User Assign DashBoard'");  //'UserName' is ColumnName
                        foreach (DataRow row in rows2)
                            dt.Rows.Remove(row);
                    }
                    lstsubmenu1.DataSource = dt;
                    lstsubmenu1.DataBind();



                    if (dt.Rows.Count > 0)
                    {
                        lisub.Attributes.Add("class", "nav-item has-treeview");
                        i1.Attributes.Add("class", "fas fa-angle-left right pull-right");
                    }
                    Sqlcon.Close();
                }
            }
        }

        protected void lstsubmenu1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lbtnmenu1 = (LinkButton)e.Item.FindControl("lbtnmenu1");
            ListView lstsubmenu2 = (ListView)e.Item.FindControl("lstsubmenu2");
            HtmlGenericControl ul2 = (HtmlGenericControl)e.Item.FindControl("ul2");
            HtmlGenericControl li2 = (HtmlGenericControl)e.Item.FindControl("li2");
            HtmlControl i2 = (HtmlControl)e.Item.FindControl("i2");
            HiddenField hdnPath1 = (HiddenField)e.Item.FindControl("hdnPath1");

            NavPath_L2 = lbtnmenu1.Text.Trim();
            hdnPath1.Value = NavPath_L1 + " > " + NavPath_L2;

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
                        PARENT: lbtnmenu1.Text
                        );
                lstsubmenu2.DataSource = ds;
                lstsubmenu2.DataBind();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    li2.Attributes.Add("class", "nav-item has-treeview");
                    i2.Attributes.Add("class", "fas fa-angle-left right pull-right");
                }
                else
                {
                    ul2.Attributes.Add("class", "d-none");
                }
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
                da.SelectCommand.Parameters.AddWithValue("@Parent", lbtnmenu1.Text);

                da.Fill(ds);

                DataTable dt = ds.Tables[0];
                lstsubmenu2.DataSource = dt;
                lstsubmenu2.DataBind();
                if (dt.Rows.Count > 0)
                {
                    li2.Attributes.Add("class", "nav-item nav-treeview has-treeview");
                    i2.Attributes.Add("class", "fas fa-angle-left right pull-right");
                }
                Sqlcon.Close();
            }
        }

        protected void lstsubmenu2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lbtnmenu2 = (LinkButton)e.Item.FindControl("lbtnmenu2");
            ListView lstsubmenu3 = (ListView)e.Item.FindControl("lstsubmenu3");
            HtmlGenericControl ul3 = (HtmlGenericControl)e.Item.FindControl("ul3");
            HtmlGenericControl li3 = (HtmlGenericControl)e.Item.FindControl("li3");
            HtmlControl i3 = (HtmlControl)e.Item.FindControl("i3");
            HiddenField hdnPath2 = (HiddenField)e.Item.FindControl("hdnPath2");

            NavPath_L3 = lbtnmenu2.Text.Trim();
            hdnPath2.Value = NavPath_L1 + " > " + NavPath_L2 + " > " + NavPath_L3;

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
                        PARENT: lbtnmenu2.Text
                        );
                lstsubmenu3.DataSource = ds;
                lstsubmenu3.DataBind();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    li3.Attributes.Add("class", "nav-item  has-treeview");
                    i3.Attributes.Add("class", "fas fa-angle-left right pull-right");
                }
                else
                {
                    ul3.Attributes.Add("class", "d-none");
                }
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
                da.SelectCommand.Parameters.AddWithValue("@Parent", lbtnmenu2.Text);

                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                lstsubmenu3.DataSource = dt;
                lstsubmenu3.DataBind();
                if (dt.Rows.Count > 0)
                {
                    li3.Attributes.Add("class", "nav-item nav-treeview has-treeview");
                    i3.Attributes.Add("class", "fas fa-angle-left right pull-right");
                }
                Sqlcon.Close();
            }
        }

        protected void lstsubmenu3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lbtnmenu3 = (LinkButton)e.Item.FindControl("lbtnmenu3");
            HiddenField hdnPath3 = (HiddenField)e.Item.FindControl("hdnPath3");

            NavPath_L4 = lbtnmenu3.Text.Trim();
            hdnPath3.Value = NavPath_L1 + " > " + NavPath_L2 + " > " + NavPath_L3 + " > " + NavPath_L4;
        }

    }
}