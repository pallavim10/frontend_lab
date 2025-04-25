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
    public partial class AjaxFunction_DM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string EXECUTE_FORMULA(string FORMULA, string CritCode)
        {
            string str = "";
            try
            {
                DAL_DM dal_DM = new DAL_DM();

                DataSet ds = dal_DM.DM_Specs_Module_Wise_SP(ACTION: "EXECUTE_FORMULA",
                    FORMULA: FORMULA,
                    CritCode: CritCode
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    str = ds.Tables[0].Rows[0]["Data"].ToString();
                }
                else
                {
                    str = "";
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
        public static string CHECK_CONDITION(string FORMULA)
        {
            string str = "";
            try
            {
                DAL_DM dal_DM = new DAL_DM();

                DataSet ds = dal_DM.DM_Specs_Module_Wise_SP(ACTION: "CHECK_CONDITION",
                    FORMULA: FORMULA
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    str = ds.Tables[0].Rows[0]["Data"].ToString();
                }
                else
                {
                    str = "";
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
                html += "<tr style='text-align: left;word-break: break-all;'>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        [System.Web.Services.WebMethod]
        public static string showAuditTrail(string Variablename, string PVID, string RECID)
        {
            string str = "";
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = commFunDM.GetAuditTrail(Variablename, PVID, RECID);

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
        public static string Get_ALL_AUDIT_BY_PVID_RECID(string PVID, string RECID)
        {
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();
                DataSet ds = new DataSet();
                string str = "";

                ds = commFunDM.Get_ALL_AUDIT_BY_PVID_RECID(PVID: PVID, RECID: RECID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML(ds);
                        return str;
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        [System.Web.Services.WebMethod]
        public static string ShowComments(string Variablename, string PVID, string RECID)
        {
            string str = "";
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = commFunDM.GetFieldComments(Variablename, PVID, RECID);

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
        public static string SetFielsComments(string VariableName, string Comments, string FieldName, string PVID, string RECID, string SUBJID, string VISITID, string MODULEID, string MODULENAME, string VISIT, string INVID)
        {
            try
            {
                DAL_DM dal_DM = new DAL_DM();
                DataSet ds = new DataSet();

                ds = dal_DM.DM_COMMENT_SP(ACTION: "INSERT",
                    PVID: PVID,
                    RECID: RECID,
                    SUBJID: SUBJID,
                    VISITID: VISITID,
                    MODULEID: MODULEID,
                    MODULENAME: MODULENAME,
                    FIELDNAME: FieldName,
                    VARIABLENAME: VariableName,
                    COMMENT: Comments,
                    VISIT: VISIT,
                    INVID: INVID
                    );
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Field comment added successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string ShowOpenQuery(string Variablename, string PVID, string RECID, string PAGENAME)
        {
            string str = "";
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = commFunDM.GetFieldQuery_OPEN(Variablename, PVID, RECID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_OPEN_QUERY(ds, PAGENAME);
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
        public static string ShowOpenQuery_PVID(string PVID, string PAGENAME)
        {
            string str = "";
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = commFunDM.ShowOpenQuery_PVID(PVID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_OPEN_QUERY(ds, PAGENAME);
                        return str;
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        [System.Web.Services.WebMethod]
        public static string ShowOpenQuery_PVID_RECID(string PVID, string RECID, string PAGENAME)
        {
            string str = "";
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = commFunDM.ShowOpenQuery_PVID_RECID(PVID, RECID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_OPEN_QUERY(ds, PAGENAME);
                        return str;
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        public static string ConvertDataTableToHTML_OPEN_QUERY(DataSet ds, string PAGENAME)
        {
            string html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                if (ds.Tables[0].Columns[i].ColumnName == "PVID" || ds.Tables[0].Columns[i].ColumnName == "VARIABLENAME" || ds.Tables[0].Columns[i].ColumnName == "BLANK")
                {
                    html += "<th scope='col' style='display:none'>ID</th>";
                }
                else if (ds.Tables[0].Columns[i].ColumnName == "MM_QUERYID")
                {
                    html += "<th scope='col'>MM Query History</th>";
                }
                else if (ds.Tables[0].Columns[i].ColumnName == "Answer")
                {
                    if (PAGENAME == "DM_DataEntry_MultipleData_ReadOnly.aspx" || PAGENAME == "DM_DataEntry_ReadOnly.aspx" || PAGENAME == "DM_DataSDV.aspx" || PAGENAME == "DM_SDV_ALL.aspx")
                    {
                        html += "<th scope='col'>Action</th>";
                    }
                    else if (PAGENAME == "DM_DataEntry_MultipleData.aspx" || PAGENAME == "DM_DataEntry.aspx")
                    {
                        html += "<th scope='col'>Answer</th>";
                    }
                    else if (PAGENAME == "DM_DataEntry_MultipleData_INV_Read_Only.aspx" || PAGENAME == "DM_DataEntry_INV_Read_Only.aspx")
                    {
                        html += "<th scope='col' style='display:none'>Answer</th>";
                    }
                }
                else
                {
                    html += "<th scope='col'>" + ds.Tables[0].Columns[i].ColumnName + "</th>";
                }

            html += "</tr>";

            //add rows
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                html += "<tr style='text-align: left; word-break: break-all;'>";

                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    if (ds.Tables[0].Columns[j].ColumnName == "Answer")
                    {
                        if (PAGENAME == "DM_DataEntry_ReadOnly.aspx" || PAGENAME == "DM_DataEntry_MultipleData_ReadOnly.aspx" || PAGENAME == "DM_DataSDV.aspx" || PAGENAME == "DM_SDV_ALL.aspx")
                        {
                            html = html + "<td style='font-size:17px; color: blue;text-align: CENTER;'> <a onclick='OpenOverideData_ReadOnly(this," + ds.Tables[0].Rows[i][j].ToString() + ");' id='lnk' style='font-size:17px; color: blue;text-align: CENTER;'><i class='fa fa-reply'></i></a>" + "</td>";
                        }
                        else if (PAGENAME == "DM_DataEntry_MultipleData.aspx" || PAGENAME == "DM_DataEntry.aspx")
                        {
                            html = html + "<td style='font-size:17px; color: blue;text-align: CENTER;'> <a onclick='OpenOverideData(this," + ds.Tables[0].Rows[i][j].ToString() + ");' id='lnk' style='font-size:17px; color: blue;text-align: CENTER;'><i class='fa fa-reply'></i></a>" + "</td>";
                        }
                        else if (PAGENAME == "DM_DataEntry_MultipleData_INV_Read_Only.aspx" || PAGENAME == "DM_DataEntry_INV_Read_Only.aspx")
                        {
                            html += "<td style='display:none'></td>";
                        }
                    }
                    else if (ds.Tables[0].Columns[j].ColumnName == "PVID" || ds.Tables[0].Columns[j].ColumnName == "VARIABLENAME" || ds.Tables[0].Columns[j].ColumnName == "BLANK")
                    {
                        html += "<td style='display:none'>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                    }
                    else if (ds.Tables[0].Columns[j].ColumnName == "MM_QUERYID")
                    {
                        if (ds.Tables[0].Rows[i][j].ToString() != "")
                        {
                            html = html + "<td style='font-size:17px; color: darkmagenta;text-align: CENTER;'> <a onclick='Show_MM_QueryHistory(" + ds.Tables[0].Rows[i][j].ToString() + ");' id='lnk' style='font-size:17px; color: darkmagenta;text-align: CENTER;'><i class='fa fa-history'></i></a>" + "</td>";
                        }
                        else
                        {
                            html = html + "<td><i class='fa fa-history' style='display:none'></i>" + "</td>";
                        }
                    }
                    else
                    {
                        html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                    }

                html += "</tr>";
            }
            html += "</table>";

            return html;
        }

        [System.Web.Services.WebMethod]
        public static string ShowAnsQuery(string Variablename, string PVID, string RECID, string PAGENAME)
        {
            string str = "";
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = commFunDM.GetFieldQuery_ANS(Variablename, PVID, RECID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_ANS_QUERY(ds, PAGENAME);
                        return str;
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
        public static string ShowAnsQuery_PVID(string PVID, string PAGENAME)
        {
            string str = "";
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = commFunDM.ShowAnsQuery_PVID(PVID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_ANS_QUERY(ds, PAGENAME);
                        return str;
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
        public static string ShowAnsQuery_PVID_RECID(string PVID, string RECID, string PAGENAME)
        {
            string str = "";
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = commFunDM.ShowAnsQuery_PVID_RECID(PVID, RECID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_ANS_QUERY(ds, PAGENAME);
                        return str;
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
        public static string ShowClosedQuery(string Variablename, string PVID, string RECID)
        {
            string str = "";
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = commFunDM.GetFieldQuery_CLOSED(Variablename, PVID, RECID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_CLOSED_QUERY(ds);
                        return str;
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
        public static string ShowClosedQuery_PVID_RECID(string PVID, string RECID)
        {
            string str = "";
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = commFunDM.ShowClosedQuery_PVID_RECID(PVID, RECID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_CLOSED_QUERY(ds);
                        return str;
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

        public static string ConvertDataTableToHTML_ANS_QUERY(DataSet ds, string PAGENAME)
        {
            string html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                if (ds.Tables[0].Columns[i].ColumnName == "PVID" || ds.Tables[0].Columns[i].ColumnName == "VARIABLENAME")
                {
                    html += "<th scope='col' style='display:none'>ID</th>";
                }
                else if (ds.Tables[0].Columns[i].ColumnName == "MM_QUERYID")
                {
                    html += "<th scope='col'>MM Query History</th>";
                }
                else if (ds.Tables[0].Columns[i].ColumnName == "QryAnsCount")
                {
                    html += "<th scope='col'>Comments</th>";
                }
                else if (ds.Tables[0].Columns[i].ColumnName == "Answer")
                {
                    if (PAGENAME == "DM_DataEntry_MultipleData_ReadOnly.aspx" || PAGENAME == "DM_DataEntry_ReadOnly.aspx" || PAGENAME == "DM_DataSDV.aspx" || PAGENAME == "DM_SDV_ALL.aspx")
                    {
                        html += "<th scope='col'>Action</th>";
                    }
                    else if (PAGENAME == "DM_DataEntry_MultipleData.aspx" || PAGENAME == "DM_DataEntry.aspx")
                    {
                        html += "<th scope='col'>Answer</th>";
                    }
                    else if (PAGENAME == "DM_DataEntry_MultipleData_INV_Read_Only.aspx" || PAGENAME == "DM_DataEntry_INV_Read_Only.aspx")
                    {
                        html += "<th scope='col' style='display:none'>Answer</th>";
                    }
                }
                else
                {
                    html += "<th scope='col'>" + ds.Tables[0].Columns[i].ColumnName + "</th>";
                }

            html += "</tr>";

            //add rows
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                html += "<tr style='text-align: left;word-break: break-all;'>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    if (ds.Tables[0].Columns[j].ColumnName == "Answer")
                    {
                        if (PAGENAME == "DM_DataEntry_ReadOnly.aspx" || PAGENAME == "DM_DataEntry_MultipleData_ReadOnly.aspx" || PAGENAME == "DM_DataSDV.aspx" || PAGENAME == "DM_SDV_ALL.aspx")
                        {
                            html = html + "<td style='font-size:17px; color: blue;text-align: CENTER;'> <a onclick='OpenOverideData_ReadOnly(this," + ds.Tables[0].Rows[i][j].ToString() + ");' id='lnk' style='font-size:17px; color: blue;text-align: CENTER;'><i class='fa fa-reply'></i></a>" + "</td>";
                        }
                        else if (PAGENAME == "DM_DataEntry_MultipleData.aspx" || PAGENAME == "DM_DataEntry.aspx")
                        {
                            html = html + "<td style='font-size:17px; color: blue;text-align: CENTER;'> <a onclick='OpenOverideData(this," + ds.Tables[0].Rows[i][j].ToString() + ");' id='lnk' style='font-size:17px; color: blue;text-align: CENTER;'><i class='fa fa-reply'></i></a>" + "</td>";
                        }
                        else if (PAGENAME == "DM_DataEntry_MultipleData_INV_Read_Only.aspx" || PAGENAME == "DM_DataEntry_INV_Read_Only.aspx")
                        {
                            html += "<td style='display:none'></td>";
                        }
                    }
                    else if (ds.Tables[0].Columns[j].ColumnName == "PVID" || ds.Tables[0].Columns[j].ColumnName == "VARIABLENAME")
                    {
                        html += "<td style='display:none'>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                    }
                    else if (ds.Tables[0].Columns[j].ColumnName == "QryAnsCount")
                    {
                        if (ds.Tables[0].Rows[i][j].ToString() != "0")
                        {
                            html = html + "<td style='font-size:17px; color: darkmagenta;text-align: CENTER;'> <a onclick='ShowQueryComment(" + ds.Tables[0].Rows[i]["Query Id"].ToString() + ");' id='lnk' style='font-size:17px; color: darkmagenta;text-align: CENTER;'><i class='fa fa-comment'></i></a>" + "</td>";
                        }
                        else
                        {
                            html = html + "<td><i class='fa fa-comment' style='display:none'></i>" + "</td>";
                        }
                    }
                    else if (ds.Tables[0].Columns[j].ColumnName == "MM_QUERYID")
                    {
                        if (ds.Tables[0].Rows[i][j].ToString() != "")
                        {
                            html = html + "<td style='font-size:17px; color: darkmagenta;text-align: CENTER;'> <a onclick='Show_MM_QueryHistory(" + ds.Tables[0].Rows[i][j].ToString() + ");' id='lnk' style='font-size:17px; color: darkmagenta;text-align: CENTER;'><i class='fa fa-history'></i></a>" + "</td>";
                        }
                        else
                        {
                            html = html + "<td><i class='fa fa-history' style='display:none'></i>" + "</td>";
                        }
                    }
                    else
                    {
                        html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                    }
                html += "</tr>";
            }
            html += "</table>";

            return html;
        }

        public static string ConvertDataTableToHTML_CLOSED_QUERY(DataSet ds)
        {
            string html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                if (ds.Tables[0].Columns[i].ColumnName == "QryAnsCount")
                {
                    html += "<th scope='col'>Comments</th>";
                }
                else if (ds.Tables[0].Columns[i].ColumnName == "MM_QUERYID")
                {
                    html += "<th scope='col'>MM Query History</th>";
                }
                else
                {
                    html += "<th scope='col'>" + ds.Tables[0].Columns[i].ColumnName + "</th>";
                }

            html += "</tr>";

            //add rows
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                html += "<tr style='text-align: left;'>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    if (ds.Tables[0].Columns[j].ColumnName == "QryAnsCount")
                    {
                        if (ds.Tables[0].Rows[i][j].ToString() != "0")
                        {
                            html = html + "<td style='font-size:17px; color: darkmagenta;text-align: CENTER;'> <a onclick='ShowQueryComment(" + ds.Tables[0].Rows[i]["Query Id"].ToString() + ");' id='lnk' style='font-size:17px; color: darkmagenta;text-align: CENTER;'><i class='fa fa-comment'></i></a>" + "</td>";
                        }
                        else
                        {
                            html = html + "<td><i class='fa fa-comment' style='display:none'></i>" + "</td>";
                        }
                    }
                    else if (ds.Tables[0].Columns[j].ColumnName == "MM_QUERYID")
                    {
                        if (ds.Tables[0].Rows[i][j].ToString() != "")
                        {
                            html = html + "<td style='font-size:17px; color: darkmagenta;text-align: CENTER;'> <a onclick='Show_MM_QueryHistory(" + ds.Tables[0].Rows[i][j].ToString() + ");' id='lnk' style='font-size:17px; color: darkmagenta;text-align: CENTER;'><i class='fa fa-history'></i></a>" + "</td>";
                        }
                        else
                        {
                            html = html + "<td><i class='fa fa-history' style='display:none'></i>" + "</td>";
                        }
                    }
                    else
                    {
                        html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                    }
                html += "</tr>";
            }
            html += "</table>";

            return html;
        }

        [System.Web.Services.WebMethod]
        public static string ShowQueryComment(string ID)
        {
            string str = "";
            try
            {
                DAL_DM dal_DM = new DAL_DM();
                DataSet ds = new DataSet();

                ds = dal_DM.DM_QUERY_SP(ACTION: "GET_QUERY_COMMENT", ID: ID);

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
        public static string Show_MM_QueryHistory(string ID)
        {
            string str = "";
            try
            {
                DAL_DM dal_DM = new DAL_DM();
                DataSet ds = new DataSet();

                ds = dal_DM.DM_QUERY_SP(ACTION: "Show_MM_QueryHistory", ID: ID);

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
        public static string Update_Comment_Status_ReadOnly(string Id, string Comment, string Reason, string QUERYTEXT)
        {
            string MSG = "";
            try
            {
                DAL_DM dal_DM = new DAL_DM();
                DataSet ds = new DataSet();

                ds = dal_DM.DM_QUERY_SP(ACTION: "Update_Comment_Status_ReadOnly",
                    ID: Id,
                    Comment: Comment,
                    REASON: Reason,
                    QUERYTEXT: QUERYTEXT
                    );

                if (Reason == "Re-query")
                {
                    MSG = "Query has been Re-queried successfully.";
                }
                else if (Reason == "Routed To MM")
                {
                    MSG = "Query has been Routed To MM successfully.";
                }
                else if (Reason == "Closed query")
                {
                    MSG = "Query has been Closed successfully.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }

            return MSG;
        }

        [System.Web.Services.WebMethod]
        public static string Update_Comment_Status(string Id, string Comment, string QueryAction)
        {
            try
            {
                DAL_DM dal_DM = new DAL_DM();
                DataSet ds = new DataSet();
                string Comments = "";
                if (QueryAction == "Other")
                {
                    Comments = QueryAction + ": " + Comment;
                }
                else
                {
                    Comments = QueryAction;
                }

                ds = dal_DM.DM_QUERY_SP(ACTION: "Update_Comment_Status",
                    ID: Id,
                    Comment: Comments
                    );
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }

            return "Query answered successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string Save_MQ(string PVID, string RECID, string SUBJID, string QUERYTEXT, string MODULENAME, string FIELDNAME, string TABLENAME, string VARIABLENAME, string VISITNUM, string VISIT, string MODULEID, string INVID)
        {
            try
            {
                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                commFunDM.DM_Manual_Query_SP(
                    PVID: PVID,
                    RECID: RECID,
                    INVID: INVID,
                    SUBJID: SUBJID,
                    QUERYTEXT: QUERYTEXT,
                    MODULENAME: MODULENAME,
                    FIELDNAME: FIELDNAME,
                    TABLENAME: TABLENAME,
                    VARIABLENAME: VARIABLENAME,
                    VISITNUM: VISITNUM,
                    VISIT: VISIT,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Manual query generated successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string DM_MarkAsSDV(string PVID, string RECID, string VISITNUM, string MODULEID, string SUBJID, string INVID, string TABLENAME, string VISIT, string MODULENAME)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_DM dal_DM = new DAL_DM();

                dal_DM.DM_SDVDETAILS_SP(
                    ACTION: "INSERT_UPDATE_FULL_SDV",
                    PVID: PVID,
                    RECID: RECID,
                    SUBJID: SUBJID,
                    VISITNUM: VISITNUM,
                    MODULEID: MODULEID,
                    SDVYN: 1,
                    INVID: INVID,
                    VISIT: VISIT,
                    MODULENAME: MODULENAME
                    );

                SDVSTATUS = "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string DM_UnMarkAsSDV(string PVID, string RECID, string VISITNUM, string MODULEID, string SUBJID, string INVID, string TABLENAME, string VISIT, string MODULENAME)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_DM dal_DM = new DAL_DM();

                DataSet ds = new DataSet();

                dal_DM.DM_SDVDETAILS_SP(
                    ACTION: "INSERT_UPDATE_FULL_SDV",
                    PVID: PVID,
                    RECID: RECID,
                    SUBJID: SUBJID,
                    VISITNUM: VISITNUM,
                    MODULEID: MODULEID,
                    SDVYN: 0,
                    INVID: INVID,
                    VISIT: VISIT,
                    MODULENAME: MODULENAME
                    );

                SDVSTATUS = "False";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string GET_LAB_DATA(string SUBJID, string LABID, string LBTEST, string VISITID, string MODULEID)
        {
            DAL_DM dal_DM = new DAL_DM();
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_LAB_DATA_SP(
                        ACTION: "GET_LAB_DATA",
                        SUBJID: SUBJID,
                        LABID: LABID,
                        LBTEST: LBTEST,
                        VISITNUM: VISITID,
                        MODULEID: MODULEID
                        );
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "tblLabData";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return ds.GetXml();
        }

        [System.Web.Services.WebMethod]
        public static List<LinkedOptions> GetLinkedOptions(string VariableName, string ParentANS, string ParentVARIABLENAME, string VISITNUM, string PVID, string RECID, string TABLENAME)
        {
            if (VISITNUM == "") { VISITNUM = null; }

            DAL_DM dal_DM = new DAL_DM();

            List<LinkedOptions> lst = new List<LinkedOptions>();

            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "DM_DM_DRP_DWN_MASTER_LINKED",
                VARIABLENAME: VariableName,
                ParentANS: ParentANS,
                VISITNUM: VISITNUM,
                PVID: PVID,
                RECID: RECID,
                TABLENAME: TABLENAME
                );

            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new LinkedOptions() { VALUE = dr["VALUE"].ToString(), TEXT = dr["TEXT"].ToString() });
                }
            }

            return lst;
        }

        public class LinkedOptions
        {
            public string VALUE { get; set; }
            public string TEXT { get; set; }
        }

        [System.Web.Services.WebMethod]
        public static string MarkAsReview(string ID, string PVID, string RECID)
        {
            string STATUS = "";
            try
            {
                DAL_DM dal_DM = new DAL_DM();

                dal_DM.DM_AUDITTRAIL_SP(ACTION: "MarkAsReview", ID: ID, PVID: PVID, RECID: RECID);
                STATUS = "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return STATUS;
        }

        [System.Web.Services.WebMethod]
        public static string UnMarkAsReviewed(string ID, string PVID, string RECID)
        {
            string STATUS = "";
            try
            {
                DAL_DM dal_DM = new DAL_DM();

                dal_DM.DM_AUDITTRAIL_SP(ACTION: "UnMarkAsReviewed", ID: ID, PVID: PVID, RECID: RECID);
                STATUS = "False";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return STATUS;
        }

        [System.Web.Services.WebMethod]
        public static string showAuditTrail_NOT_SYNCED_FROM_DDC(string PVID, string RECID)
        {
            string str = "";
            try
            {
                DAL_DM dal_DM = new DAL_DM();

                CommonFunction.CommonFunction_DM commFunDM = new CommonFunction.CommonFunction_DM();

                DataSet ds = dal_DM.DM_AUDITTRAIL_SP(ACTION: "showAuditTrail_NOT_SYNCED_FROM_DDC", PVID: PVID, RECID: RECID);

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
        public static string CheckDatatype(string Val)
        {
            string RESULT = "";
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                int i = 0;
                float j = 0;
                double k = 0;
                DateTime l;

                if (Val.Contains("dd/"))
                {
                    Val = Val.Replace("dd/", "01/");
                }

                if (Val.Contains("mm/"))
                {
                    Val = Val.Replace("mm/", "01/");
                }

                if (int.TryParse(Val, out i))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (float.TryParse(Val, out j))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (double.TryParse(Val, out k))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (DateTime.TryParse(Val, out l) || cf.isDate(Val))
                {
                    RESULT = "dbo.CastDate('" + Val + "')";
                }
                else
                {
                    RESULT = "N'" + Val + "'";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RESULT;
        }
    }
}