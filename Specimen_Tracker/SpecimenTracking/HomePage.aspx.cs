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
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (lstm.Items.Count == 1)
            {
                div4.Attributes.Add("class", "row single-tile"); // Apply single-tile class if only 1 item
              
            }
            else
            {
                div4.Attributes.Add("class", "row"); // Apply regular class if more than 1 item
              
            }
        }
        //protected void lstmenu_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    string menu = e.CommandArgument.ToString();
        //    if (e.CommandName == "menu")
        //    {
        //        if (menu == "Home")
        //        {
        //            mainmenu.Visible = true;
        //            sub.Visible = false;
        //            PopulateMenuControl();
        //            Session["menu"] = menu;
        //        }
        //        else
        //        {
        //            mainmenu.Visible = false;
        //            sub.Visible = true;
        //            Session["menu"] = menu;
        //            PopulateMenuControlChildItem(menu);

        //        }

        //    }
        //}

        protected void lstm_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                // Find the Label control inside the current ListView item
                Label lbl = (Label)e.Item.FindControl("lbltotal");
                //HtmlGenericControl span = (HtmlGenericControl)e.Item.FindControl("FunctionName");
                HtmlGenericControl itemTemplateDiv = (HtmlGenericControl)e.Item.FindControl("itemTemplateDiv");

                // Modify the class of itemTemplateDiv based on condition
                if (itemTemplateDiv != null)
                {
                    // Check the number of items in the ListView
                    if (lstm.Items.Count == 1)
                    {
                        // If there is only one tile, make it full width
                        itemTemplateDiv.Attributes["class"] = "col-lg-12"; // Full width for one tile
                        
                    }
                    else
                    {
                        // If there are multiple tiles, apply the original classes
                        itemTemplateDiv.Attributes["class"] = "col-lg-3 col-xs-6"; // Regular classes
                    }
                }
                
            }
        }


    }
}