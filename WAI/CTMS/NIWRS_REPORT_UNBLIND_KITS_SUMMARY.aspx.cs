using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;

namespace CTMS
{
    public partial class NIWRS_REPORT_UNBLIND_KITS_SUMMARY : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_COUNTRY();
                    GET_SITE();
                    GET_KITS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP(USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlCountry.DataSource = ds.Tables[0];
                        ddlCountry.DataTextField = "COUNTRYNAME";
                        ddlCountry.DataValueField = "COUNTRYID";
                        ddlCountry.DataBind();
                    }
                    else
                    {
                        ddlCountry.DataSource = ds.Tables[0];
                        ddlCountry.DataTextField = "COUNTRYNAME";
                        ddlCountry.DataValueField = "COUNTRYID";
                        ddlCountry.DataBind();
                        ddlCountry.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                        ddlSite.Items.Insert(0, new ListItem("--All--", "-1"));
                    }
                    else
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                    }
                }
                else
                {
                    ddlSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_KITS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_REPORT_SP(ACTION: "GET_UNBLIND_KIT_SUMMARY", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue, USERID: Session["User_ID"].ToString());
                gvKits.DataSource = ds;
                gvKits.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvKits_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string TREAT_GRP = dr["Treatment Group"].ToString();
                    string SITEID = dr["Site Id"].ToString();

                    DataSet ds = dal_IWRS.IWRS_KITS_REPORT_SP(ACTION: "GET_UNBLIND_KIT_SUMMARY_AVL_KITS", TREAT_GRP: TREAT_GRP, SITEID: SITEID);
                    GridView gvAVLKits = (GridView)e.Row.FindControl("gvAVLKits");
                    gvAVLKits.DataSource = ds;
                    gvAVLKits.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SITE();
                GET_KITS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_KITS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_REPORT_SP(ACTION: "GET_UNBLIND_KIT_SUMMARY", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue, USERID: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
                }
                    
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvKits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;

                GridView gvAVLKits = (GridView)gvKits.Rows[index].FindControl("gvAVLKits");
                DataTable dt = GetDataTable(gvAVLKits);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = lblHeader.Text;
                if (e.CommandName == "Excel")
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
                        
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('No file avaliable'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                    }
                       
                }
                else if (e.CommandName == "PDF")
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('No file avaliable'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportPDF();
        }

        private void ExportPDF()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_REPORT_SP(ACTION: "GET_UNBLIND_KIT_SUMMARY", COUNTRYID: ddlCountry.SelectedValue, SITEID: ddlSite.SelectedValue, USERID: Session["User_ID"].ToString());

                if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
                }
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}