using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DesignBench_HomePage : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }

                if (!this.Page.IsPostBack)
                {
                    DataSet ds = dal_DB.DB_STATUS_SP(ACTION: "GET_DB_STATUS");
                    if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        GET_SYSTEM_MODULE_COUNT_TILES();
                        GET_MODULE_DETAILS_COUNT_TILES();
                        GET_FIELD_DETAILS_COUNT_TILES();
                        GET_MODULE_LISTING_COUNT_TILES();
                        GET_RULES_COUNT_TILES();
                    }
                    else
                    {
                        ModalPopupExtender1.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



        private void GET_SYSTEM_MODULE_COUNT_TILES()
        {
            try
            {
                DataSet ds = dal_DB.DB_DASHBORAD_SP(ACTION: "GET_SYSTEM_MODULE_COUNT_TILES");

                lstTile_MODULES_COUNT.DataSource = ds;
                lstTile_MODULES_COUNT.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public int col = 0;
        protected void lstTile_MODULES_COUNT_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");

                int itemIndex = e.Item.DataItemIndex;

                string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MODULE_DETAILS_COUNT_TILES()
        {
            try
            {
                DataSet ds = dal_DB.DB_DASHBORAD_SP(ACTION: "GET_MODULE_DETAILS_COUNT_TILES");

                lst_BU_TILES_COUNT.DataSource = ds;
                lst_BU_TILES_COUNT.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lst_BU_TILES_COUNT_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");

                int itemIndex = e.Item.DataItemIndex;

                string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_FIELD_DETAILS_COUNT_TILES()
        {
            try
            {
                DataSet ds = dal_DB.DB_DASHBORAD_SP(ACTION: "GET_FIELD_DETAILS_COUNT_TILES");

                lstFieldDetails.DataSource = ds;
                lstFieldDetails.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstFieldDetails_COUNT_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");

                int itemIndex = e.Item.DataItemIndex;

                string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MODULE_LISTING_COUNT_TILES()
        {
            try
            {
                DataSet ds = dal_DB.DB_DASHBORAD_SP(ACTION: "GET_MODULE_LISTING_COUNT_TILES");

                lstModuleListing.DataSource = ds;
                lstModuleListing.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstModuleListing_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");

                int itemIndex = e.Item.DataItemIndex;

                string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_RULES_COUNT_TILES()
        {
            try
            {
                DataSet ds = dal_DB.DB_DASHBORAD_SP(ACTION: "GET_RULES_COUNT_TILES");

                lst_Rules.DataSource = ds;
                lst_Rules.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lst_Rules_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");

                int itemIndex = e.Item.DataItemIndex;

                string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dal_DB.DB_STATUS_SP(
                    ACTION: "INSERT_DB_VERSION",
                    DB_VERSION: txtVesion.Text.Trim()
                    );

                Response.Write("<script> alert('DB Version defined successfully.');window.location='DesignBench_HomePage.aspx'; </script>");
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }
    }
}