using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class AjaxFunction_SAE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                html += "<tr style='text-align: left;'>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        [System.Web.Services.WebMethod]
        public static string SAE_showAuditTrail(string Variablename, string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_GetAuditTrail(Variablename, SAEID, RECID, MODULEID);

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
        public static string SAE_showAuditTrail_MR(string Variablename, string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_GetAuditTrail_MR(Variablename, SAEID, RECID, MODULEID);

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
        public static string SAE_ShowComments(string Variablename, string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_GetFieldComments(Variablename, SAEID, RECID, MODULEID);

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
        public static string SAE_SetFielsComments(string VariableName, string SAEID, string RECID, string SAE, string Comments, string FieldName, string ModuleName, string INVID, string SUBJID, string STATUS, string MODULEID)
        {
            try
            {
                DAL_SAE dal_SAE = new DAL_SAE();
                DataSet ds = new DataSet();

                ds = dal_SAE.SAE_COMMENT_SP(
                    ACTION: "INSERT_FIELD_COMMENT",
                    VARIABLENAME: VariableName,
                    SAEID: SAEID,
                    RECID: RECID,
                    SAE: SAE,
                    INVID: INVID,
                    COMMENT: Comments,
                    FIELDNAME: FieldName,
                    MODULENAME: ModuleName,
                    SUBJID: SUBJID,
                    STATUS: STATUS,
                    MODULEID: MODULEID
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
        public static string SAE_ShowOpenQuery(string VARIABLENAME, string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_GetFieldQuery_OPEN(VARIABLENAME, SAEID, RECID, MODULEID);

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
        public static string SAE_ShowAnsQuery(string VARIABLENAME, string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_GetFieldQuery_ANS(VARIABLENAME, SAEID, RECID, MODULEID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_ANS_CLOSED_QUERY(ds);
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
        public static string SAE_ShowClosedQuery(string VARIABLENAME, string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_GetFieldQuery_CLOSED(VARIABLENAME, SAEID, RECID, MODULEID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_ANS_CLOSED_QUERY(ds);
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
        public static string SAE_ShowQueryComment(string ID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();
                DataSet ds = new DataSet();

                ds = comm_SAE.SAE_GET_QUERY_COMMENT(ID: ID);

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
        public static string SAE_Update_Comment_Status(string Id, string Comment, string QueryAction)
        {
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();
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

                ds = comm_SAE.SAE_Update_Comment_Status(
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
        public static string SAE_Update_Comment_Status_ReadOnly(string Id, string Comment, string Reason, string QUERYTEXT)
        {
            string MSG = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();
                DataSet ds = new DataSet();

                ds = comm_SAE.SAE_Update_Comment_Status_ReadOnly(
                    ID: Id,
                    Comment: Comment,
                    Reason: Reason,
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

        public static string ConvertDataTableToHTML_ANS_CLOSED_QUERY(DataSet ds)
        {
            string html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)

                if (ds.Tables[0].Columns[i].ColumnName == "ID")
                {
                    html += "<th scope='col' style='display:none'>ID</th>";
                }
                else if (ds.Tables[0].Columns[i].ColumnName == "QryAnsCount")
                {
                    html += "<th scope='col'>Comments</th>";
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
                    if (ds.Tables[0].Columns[j].ColumnName == "ID")
                    {
                        html += "<td style='display:none'>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                    }
                    else if (ds.Tables[0].Columns[j].ColumnName == "QryAnsCount")
                    {
                        if (ds.Tables[0].Rows[i][j].ToString() != "0")
                        {
                            html = html + "<td style='font-size:17px; color: darkmagenta;text-align: CENTER;'> <a onclick='SAE_ShowQueryComment(" + ds.Tables[0].Rows[i]["ID"].ToString() + ");' id='lnk' style='font-size:17px; color: darkmagenta;text-align: CENTER;'><i class='fa fa-comment'></i></a>" + "</td>";
                        }
                        else
                        {
                            html = html + "<td><i class='fa fa-comment' style='display:none'></i>" + "</td>";
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
        public static string SAE_Show_Page_Comments(string SAEID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_Show_Page_Comments(SAEID: SAEID,
                    MODULEID: MODULEID
                    );

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
        public static string SAE_Show_Internal_Comments(string SAEID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_Show_Internal_Comments(
                            SAEID: SAEID,
                            MODULEID: MODULEID
                            );

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
        public static string INSERT_PAGE_COMMENT(string SAEID, string RECID, string SAE, string Comments, string ModuleName, string INVID, string SUBJID, string STATUS, string MODULEID)
        {
            try
            {
                DAL_SAE dal_SAE = new DAL_SAE();
                DataSet ds = new DataSet();

                ds = dal_SAE.SAE_COMMENT_SP(
                    ACTION: "INSERT_PAGE_COMMENT",
                    SAEID: SAEID,
                    RECID: RECID,
                    SAE: SAE,
                    INVID: INVID,
                    COMMENT: Comments,
                    MODULENAME: ModuleName,
                    SUBJID: SUBJID,
                    STATUS: STATUS,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Module comment added successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string INSERT_INTERNAL_COMMENT(string SAEID, string RECID, string SAE, string Comments, string ModuleName, string INVID, string SUBJID, string STATUS, string MODULEID)
        {
            try
            {
                DAL_SAE dal_SAE = new DAL_SAE();
                DataSet ds = new DataSet();

                ds = dal_SAE.SAE_COMMENT_SP(
                    ACTION: "INSERT_INTERNAL_COMMENT",
                    SAEID: SAEID,
                    RECID: RECID,
                    SAE: SAE,
                    INVID: INVID,
                    COMMENT: Comments,
                    MODULENAME: ModuleName,
                    SUBJID: SUBJID,
                    STATUS: STATUS,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Internal comment added successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string SAE_ShowOpenQuery_SAEID(string SAEID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_ShowOpenQuery_SAEID(SAEID, MODULEID);

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
        public static string SAE_ShowOpenQuery_SAEID_RECID(string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_ShowOpenQuery_SAEID_RECID(SAEID, RECID, MODULEID);

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
        public static string SAE_ShowAnsQuery_SAEID_RECID(string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_ShowAnsQuery_SAEID_RECID(SAEID, RECID, MODULEID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_ANS_CLOSED_QUERY(ds);
                        return str;
                    }
                }

                return str;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        [System.Web.Services.WebMethod]
        public static string SAE_ShowAnsQuery_SAEID(string SAEID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_ShowAnsQuery_SAEID(SAEID, MODULEID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_ANS_CLOSED_QUERY(ds);
                        return str;
                    }
                }

                return str;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        [System.Web.Services.WebMethod]
        public static string SAE_ShowClosedQuery_SAEID_RECID(string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.SAE_ShowClosedQuery_SAEID_RECID(SAEID, RECID, MODULEID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML_ANS_CLOSED_QUERY(ds);
                        return str;
                    }
                }

                return str;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        [System.Web.Services.WebMethod]
        public static string Get_ALL_AUDIT_BY_SAEID_RECID(string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.Get_ALL_AUDIT_BY_SAEID_RECID(SAEID, RECID, MODULEID);

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
        public static string Get_ALL_AUDIT_BY_SAEID_RECID_MR(string SAEID, string RECID, string MODULEID)
        {
            string str = "";
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.Get_ALL_AUDIT_BY_SAEID_RECID_MR(SAEID, RECID, MODULEID);

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
        public static string Save_SAE_MQ(string SAEID, string RECID, string SUBJID, string QUERYTEXT, string MODULENAME, string MODULEID, string FIELDNAME, string TABLENAME, string VARIABLENAME)
        {
            try
            {
                CommonFunction_SAE comm_SAE = new CommonFunction_SAE();

                DataSet ds = comm_SAE.INSERT_MANUAL_QUERY(SAEID, RECID, SUBJID, QUERYTEXT, MODULENAME, MODULEID, FIELDNAME, TABLENAME, VARIABLENAME);

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Manual query generated successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string SAE_MarkAsSDV(string SAEID, string RECID, string MODULEID, string SUBJID, string INVID, string STATUS)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_SAE dal_SAE = new DAL_SAE();

                dal_SAE.SAE_SDVDETAILS_SP(
                    ACTION: "INSERT_UPDATE_FULL_SDV",
                    SAEID: SAEID,
                    RECID: RECID,
                    STATUS: STATUS,
                    MODULEID: MODULEID,
                    SUBJID: SUBJID,
                    INVID: INVID,
                    SDVYN: 1
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
        public static string SAE_UnMarkAsSDV(string SAEID, string RECID, string MODULEID, string SUBJID, string INVID, string STATUS)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_SAE dal_SAE = new DAL_SAE();

                dal_SAE.SAE_SDVDETAILS_SP(
                     ACTION: "INSERT_UPDATE_FULL_SDV",
                     SAEID: SAEID,
                     RECID: RECID,
                     STATUS: STATUS,
                     MODULEID: MODULEID,
                     SUBJID: SUBJID,
                     INVID: INVID,
                     SDVYN: 0
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

        [System.Web.Services.WebMethod]
        public static List<LinkedOptions> GetLinkedOptions(string VariableName, string ParentANS, string ParentVARIABLENAME, string SAEID, string RECID, string TABLENAME)
        {
            DAL_SAE dal_SAE = new DAL_SAE();

            List<LinkedOptions> lst = new List<LinkedOptions>();

            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "SAE_DM_DRP_DWN_MASTER_LINKED",
                VARIABLENAME: VariableName,
                ParentANS: ParentANS,
                SAEID: SAEID,
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
    }
}