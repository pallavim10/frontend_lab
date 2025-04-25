using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class AjaxFunction_MM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Method for get  userid from session    
        [WebMethod(EnableSession = true)]
        public static string GetUserId()
        {
            try
            {
                return HttpContext.Current.Session["User_ID"].ToString();
            }
            catch (Exception ex)
            {
                //  return ex.Message.ToString();
                throw;
            }
        }

        //Method for get  Project ID from session    
        [WebMethod(EnableSession = true)]
        public static string GetProjectId()
        {
            try
            {
                return HttpContext.Current.Session["PROJECTID"].ToString();
            }
            catch (Exception ex)
            {
                //return ex.Message.ToString();
                throw;
            }
        }

        [System.Web.Services.WebMethod]
        public static string MM_Reviewed(string SOURCE, string PVID, string RECID, string LISTING_ID, string SUBJID)
        {
            string status = "";
            try
            {
                DAL_MM dal_MM = new DAL_MM();
                DataSet ds = new DataSet();

                ds = dal_MM.MM_ACTION_SP(
                        ACTION: "REVEIW",
                        SOURCE: SOURCE,
                        PVID: PVID,
                        RECID: RECID,
                        LISTING_ID: LISTING_ID,
                        SUBJID: SUBJID
                        );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    status = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    status = "Not Reviewed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return status;
        }

        [System.Web.Services.WebMethod]
        public static string MM_AutoQuery(string SOURCE, string PVID, string RECID, string LISTING_ID, string SUBJID, string REFERENCE)
        {
            try
            {
                DAL_MM dal_MM = new DAL_MM();
                DataSet ds;
                ds = new DataSet();

                dal_MM.MM_QUERY_SP(
                ACTION: "RAISE_AUTO_QUERY",
                SOURCE: SOURCE,
                PVID: PVID,
                RECID: RECID,
                LISTING_ID: LISTING_ID,
                SUBJID: SUBJID,
                QUERYTEXT: "Auto Raised Query"
                );
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Record Updated successfully.";
        }


        [System.Web.Services.WebMethod]
        public static string ShowComments_QUERY_Popup(string ID)
        {
            string str = "";
            try
            {
                DAL_MM dal_MM = new DAL_MM();

                DataSet ds = dal_MM.MM_QUERY_SP(ACTION: "GET_QUERY_COMMENTS_Popup", ID: ID);

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
        public static string ShowHistory_QUERY_Popup(string ID)
        {
            string str = "";
            try
            {
                DAL_MM dal_MM = new DAL_MM();

                DataSet ds = dal_MM.MM_QUERY_SP(ACTION: "GET_QUERY_HISTORY_Popup", ID: ID);

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
            string html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                html += "<th scope='col'>" + ds.Tables[0].Columns[i].ColumnName + "</th>";
            html += "</tr>";

            //add rows
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                html += "<tr style='text-align: left; word-break: break-all;'>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
    }
}