using CTMS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DB_UPOAD_DATA_LOGS : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_ACTIVITY_AUDIT_TRAIL_SP(ACTION: "GET_UPLOAD_DATA_LOGS",
                    LISTINGID: ddlActivity.SelectedValue);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdRecords.DataSource = ds.Tables[0];
                    grdRecords.DataBind();

                    btnExportExcel.Visible = true;
                }
                else
                {
                    grdRecords.DataSource = null;
                    grdRecords.DataBind();

                    btnExportExcel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_ACTIVITY_AUDIT_TRAIL_SP(ACTION: "GET_UPLOAD_DATA_LOGS_EXPORT",
                    LISTINGID: ddlActivity.SelectedValue);

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_" + ddlActivity.SelectedValue + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel(ds, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdField_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdRecords_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                if (e.CommandName == "DownloadSupportDoc")
                {
                    string FileName, ContentType;
                    byte[] fileData;

                    DataSet ds = dal_DB.DB_ACTIVITY_AUDIT_TRAIL_SP(ACTION: "GET_UPLOAD_DATA_LOGS_BYID",
                        LISTINGID: ID);

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            FileName = ds.Tables[0].Rows[0]["FILENAME"].ToString();
                            ContentType = ds.Tables[0].Rows[0]["CONTENTTYPE"].ToString();
                            fileData = (byte[])ds.Tables[0].Rows[0]["DATA"];
                            Response.Clear();
                            Response.ContentType = ContentType;
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

                            // Append cookie
                            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                            cookie.Value = "Flag";
                            cookie.Expires = DateTime.Now.AddDays(1);
                            Response.AppendCookie(cookie);
                            // end

                            Response.BinaryWrite(fileData);
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}