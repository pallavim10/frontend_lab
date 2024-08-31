using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SpecimenTracking.App_Code;

namespace SpecimenTracking
{
    public partial class UMT_SiteUsersList : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SITE_USER();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void GET_SITE_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_SITE_USER_APPROVE"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdSiteUser.DataSource = ds.Tables[0];
                    grdSiteUser.DataBind();
                }
                else
                {
                    grdSiteUser.DataSource = null;
                    grdSiteUser.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUserDetails_PreRender(object sender, EventArgs e)
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

        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;

                if (e.CommandName == "EditUSER")
                {
                    Response.Redirect("UMT_SiteUsersApproval.aspx?ID=" + ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lblSiteUserListExport_Click(object sender, EventArgs e)
        {
            try
            {
                string header = "Site Users List Details";

                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_SITE_USER_APPROVE"
                    );

                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}