using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class MM_PD_LIST : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_MM = new DAL_MM();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["PD_COUNTRYID"] != null)
                    {
                        GET_COUNTRY();
                        drpCountry.SelectedValue = Session["PD_COUNTRYID"].ToString();
                        GET_SITEID();
                        drpInvID.SelectedValue = Session["PD_INVID"].ToString();
                        GET_SUBJID();
                        drpSubID.SelectedValue = Session["PD_SUBJID"].ToString();

                        Session.Remove("PD_COUNTRYID");
                        Session.Remove("PD_INVID");
                        Session.Remove("PD_SUBJID");
                    }
                    else
                    {
                        GET_COUNTRY();
                        GET_SITEID();
                        GET_SUBJID();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GET_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP();
                drpCountry.DataSource = ds.Tables[0];
                drpCountry.DataTextField = "COUNTRYNAME";
                drpCountry.DataValueField = "COUNTRYID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_SITEID()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(COUNTRYID: drpCountry.SelectedValue, USERID: Session["User_ID"].ToString());
                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataTextField = "INVID";
                drpInvID.DataValueField = "INVID";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBJID()
        {
            try
            {
                DataSet ds = dal.GET_SUBJECT_SP(INVID: drpInvID.SelectedValue);
                drpSubID.DataSource = ds.Tables[0];
                drpSubID.DataValueField = "SUBJID";
                drpSubID.DataBind();
                drpSubID.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            DataSet ds = dal_MM.MM_PD_SP(ACTION: "GET_PD", INVID: drpInvID.SelectedValue, SUBJID: drpSubID.SelectedValue);
            grdPROTVIOL.DataSource = ds;
            grdPROTVIOL.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataSet ds = dal_MM.MM_PD_SP(ACTION: "EXPORT_PD", INVID: drpInvID.SelectedValue, SUBJID: drpSubID.SelectedValue);

            string xlname = lblHeader.Text + ".xls";

            DataSet dsExport = new DataSet();

            for (int i = 1; i < ds.Tables.Count; i++)
            {
                ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                dsExport.Tables.Add(ds.Tables[i].Copy());
                i++;
            }
            Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
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

        protected void grdPROTVIOL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[2].Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void grdPROTVIOL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    Session["PD_COUNTRYID"] = drpCountry.SelectedValue;
                    Session["PD_INVID"] = drpInvID.SelectedValue;
                    Session["PD_SUBJID"] = drpSubID.SelectedValue;

                    Response.Redirect("MM_PD_DETAILS.aspx?PD_ID=" + e.CommandArgument);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            GET_SITEID();
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GET_SUBJID();
        }


        [System.Web.Services.WebMethod]
        public static string showAuditTrail(string ID)
        {
            string str = "";
            try
            {
                DAL_MM dal_MM = new DAL_MM();

                DataSet ds = dal_MM.MM_PD_SP(ACTION: "GET_AUDITTRAIL_PD", PD_ID: ID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return str;
        }

        private static string ConvertDataTableToHTML(DataSet ds)
        {
            string html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                html += "<th scope='col'>" + ds.Tables[0].Columns[i].ColumnName + "</th>";
            html += "</tr>";

            //add rows
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                html += "<tr style='text-align: left;'>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

    }
}