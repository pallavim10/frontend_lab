using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class AjaxFunction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        public static string setNavigationPath(string NavigationPath)
        {
            try
            {
                HttpCookie nameCookie = new HttpCookie("NavigationPath");

                nameCookie.Values["NavigationPath"] = NavigationPath;

                HttpContext.Current.Response.Cookies.Add(nameCookie);
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return NavigationPath;
        }

        [System.Web.Services.WebMethod]
        public static string showAuditTrail(string TABLENAME, string ID)
        {
            string str = "";
            try
            {
                DAL_UMT dal = new DAL_UMT();

                DataSet ds = dal.UMT_LOG_SP(ACTION: "GET_AUDITTRAIL", TABLENAME: TABLENAME, ID: ID);

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
        [System.Web.Services.WebMethod]
        public static string SETUP_showAuditTrail(string TABLENAME, string ID)
        {
            string str = "";
            try
            {
                DAL_SETUP dal_SetUp = new DAL_SETUP();

                DataSet ds = dal_SetUp.SETUP_LOGS_SP(ACTION: "GET_AUDITTRAIL", TABLENAME: TABLENAME, ID: ID);

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
        [System.Web.Services.WebMethod]
        public static string DE_showAuditTrail(string TABLENAME, string ID)
        {
            string str = "";
            try
            {
                DAL_DE Dal_DE = new DAL_DE();
                DataSet ds = Dal_DE.DE_LOG_SP(ACTION: "GET_AUDITTRAIL", TABLENAME: TABLENAME, ID: ID);

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

        [System.Web.Services.WebMethod]
        public static string SHOW_ALIQUOT_DATA(string ID)
        {
            string str = "";
            try
            {
                DAL_DE Dal_DE = new DAL_DE();
                DataSet ds = Dal_DE.DATA_ENTRYLIST_SP(ACTION: "GET_DATA_ALIQUOT_SDV", SID: ID);

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
        public static string ConvertDataTableToHTML(DataSet ds)
        {
            string html = "<table class='table table-bordered table-striped table-responsive =' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                html += "<th scope='col'>" + ds.Tables[0].Columns[i].ColumnName + "</td>";
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