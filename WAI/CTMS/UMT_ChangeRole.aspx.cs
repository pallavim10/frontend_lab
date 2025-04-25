using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class UMT_ChangeRole : System.Web.UI.Page
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
                    ACTION: "GET_ASSIGN_ROLES_ALL_DATA"
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


                //GridViewRow headerRow = grdAssignRoles.HeaderRow;
                //TableCell headerCell = new TableCell();
                //if (headerRow != null)
                //{
                //    // Set the colspan attribute for the desired header cell
                //    headerRow.Cells[9].Attributes.Add("colspan", "9");
                //    grdAssignRoles.HeaderRow.Cells.Clear();
                //    grdAssignRoles.HeaderRow.Cells.Add(headerCell);
                //}


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
        protected void grdAssignRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[2].CssClass = "disp-none";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void grdAssignRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string UserID = e.CommandArgument.ToString();
                ViewState["UserID"] = UserID;


                if (e.CommandName == "EIDITUSER")
                {
                    Response.Redirect("UMT_AssignRoleAct.aspx?UserID=" + UserID + "&Type=Change");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        public static string EncryptId(string UserID)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                System.Text.Encoder enc = System.Text.Encoding.Unicode.GetEncoder();
                byte[] unicodeText = new byte[UserID.Length * 2];
                enc.GetBytes(UserID.ToCharArray(), 0, UserID.Length, unicodeText, 0, true);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] result = md5.ComputeHash(unicodeText);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        protected void lblUserRoleExport_Click(object sender, EventArgs e)
        {
            try
            {
                string header = "Users Role Details";

                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_ASSIGN_ROLES_ALL_DATA"
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