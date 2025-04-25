using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Security.Cryptography;
using System.Text;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class UMT_AssignRoles : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_ASSIGN_ROLES_DATA();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GET_ASSIGN_ROLES_DATA()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_ASSIGN_ROLES_DATA"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdAssignRoles.DataSource = ds.Tables[0];
                    grdAssignRoles.DataBind();
                }
                else
                {
                    grdAssignRoles.DataSource = null;
                    grdAssignRoles.DataBind();
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

        protected void grdAssignRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string UserID = e.CommandArgument.ToString();
                ViewState["UserID"] = UserID;

                //EncryptUserId(Convert.ToInt32(UserID));

                if (e.CommandName == "EIDITUSER")
                {
                    Response.Redirect("UMT_AssignRoleAct.aspx?UserID=" + ViewState["UserID"].ToString() + "&Type=Assign");

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lblAssignRoleExport_Click(object sender, EventArgs e)
        {
            try
            {
                string header = "Assign Role Details";

                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_ASSIGN_ROLES_DATA_EXPORT"
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
