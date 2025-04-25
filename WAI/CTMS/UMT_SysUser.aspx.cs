using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class UMT_SysUser : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SYSTEM_ROLES_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SYSTEM_ROLES_DATA()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SYSTEM_MASTER_SP(
                    ACTION: "GET_SYSTEMS"
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    grdSysUser.DataSource = ds.Tables[0];
                    grdSysUser.DataBind();
                }
                else
                {
                    grdSysUser.DataSource = null;
                    grdSysUser.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSysUser_PreRender(object sender, EventArgs e)
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

        protected void grdSysUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string SystemID = dr["SystemID"].ToString();

                    DataSet ds = dal_UMT.UMT_SYSTEM_MASTER_SP(
                      ACTION: "GET_INTERNAL_USERS"
                      );

                    DropDownList drpInternalUser = (DropDownList)e.Row.FindControl("drpInternalUser");


                    drpInternalUser.DataSource = ds.Tables[0];
                    drpInternalUser.DataValueField = "UserID";
                    drpInternalUser.DataTextField = "User_Name";
                    drpInternalUser.DataBind();
                    drpInternalUser.Items.Insert(0, new ListItem("-Select-", "0"));

                    if (dr["UserID"].ToString() != "")
                    {
                        drpInternalUser.SelectedValue = dr["UserID"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdSysUser.Rows.Count; i++)
                {
                    DropDownList drpInternalUser = (DropDownList)grdSysUser.Rows[i].FindControl("drpInternalUser");

                    Label lblSystem = (Label)grdSysUser.Rows[i].FindControl("lblSystem");

                    Label lblSystemID = (Label)grdSysUser.Rows[i].FindControl("lblSystemID");

                    dal_UMT.UMT_SYSTEM_MASTER_SP(
                        ACTION: "INSERT_MASTER_USERS",
                        SystemID: lblSystemID.Text,
                        SystemName: lblSystem.Text,
                        System_UserID: drpInternalUser.SelectedValue
                    );
                }

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Master User Updated Successfully');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lblMasterUsersExport_Click(object sender, EventArgs e)
        {
            try
            {
                string header = "Master Users Details";

                DataSet ds = dal_UMT.UMT_LOG_SP(
                    ACTION: "GET_ASSIGNED_MASTERS"
                    );

                DataSet dsExport = new DataSet();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                Multiple_Export_Excel.ToExcel(dsExport, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UMT_SysUser.aspx");
        }
    }
}