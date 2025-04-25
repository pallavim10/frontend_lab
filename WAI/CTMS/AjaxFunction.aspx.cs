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

namespace eCRF_Templete
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
        public static string IWRS_showAuditTrail(string TABLENAME, string ID)
        {
            string str = "";
            try
            {
                DAL_IWRS dal_IWRS = new DAL_IWRS();

                DataSet ds = dal_IWRS.IWRS_LOG_SP(ACTION: "GET_AUDITTRAIL", TABLENAME: TABLENAME, ID: ID);

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
        public static string DB_showAuditTrail(string TABLENAME, string ID)
        {
            string str = "";
            try
            {
                DAL_DB dal_DB = new DAL_DB();

                DataSet ds = dal_DB.DB_DM_AUDIT_TRAIL_SP(ACTION: "GET_AUDITTRAIL", TABLENAME: TABLENAME, ID: ID);

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
        public static string SAE_showAuditTrail(string TABLENAME, string ID)
        {
            string str = "";
            try
            {
                DAL_SAE dal_SAE = new DAL_SAE();

                DataSet ds = dal_SAE.SAE_DB_AUDIT_TRAIL_SP(ACTION: "GET_AUDITTRAIL", TABLENAME: TABLENAME, ID: ID);

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
        public static string eTMF_showAuditTrail(string TABLENAME, string ID)
        {
            string str = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();

                DataSet ds = dal_eTMF.eTMF_LOG_SP(ACTION: "GET_AUDITTRAIL", TABLENAME: TABLENAME,ID: ID);

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
        [WebMethod(EnableSession = true)]
        public static string getconstrCHILD()
        {
            return HttpContext.Current.Session["CHILD_CONN"].ToString();
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

        //Method for get  SUBJID from session    
        [WebMethod(EnableSession = true)]
        public static string GetSubjID()
        {
            try
            {
                return HttpContext.Current.Session["SUBJID"].ToString();
            }
            catch (Exception ex)
            {
                // return ex.Message.ToString();
                throw;
            }
        }

        //Method for get  INV from session    
        [WebMethod(EnableSession = true)]
        public static string GetINVID()
        {
            try
            {
                return HttpContext.Current.Session["INVID"].ToString();
            }
            catch (Exception ex)
            {
                // return ex.Message.ToString();
                throw;
            }
        }

        //Method for update data in table and audit trial    
        [System.Web.Services.WebMethod]
        public static string UpdateData(string VISITNUM, string FieldName, string OldValue, string NewValue, string Reason,
            string Comments, string ControlType, string DM_SUBJID, string DM_INVID)
        {
            SqlCommand cmd;
            SqlConnection con = new SqlConnection();
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(HttpContext.Current.Session["CHILD_CONN"].ToString());
            }
            else
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
            }
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(getconstrCHILD());
            }
            try
            {

                string Project_ID = GetProjectId();
                string UserId = GetUserId();
                string SUBJID = DM_SUBJID;
                string INVID = DM_INVID;

                if (VISITNUM != "NULL")//for visitdate update and audit trail
                {
                    cmd = new SqlCommand("CHANGE_SP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UPDATE_Sub_Visit_Data");
                    cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                    cmd.Parameters.AddWithValue("@INVID", Convert.ToInt32(INVID));
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                    cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                    cmd.Parameters.AddWithValue("@FIELDNAME", FieldName);
                    cmd.Parameters.AddWithValue("@OLDVALUE", OldValue);
                    cmd.Parameters.AddWithValue("@NEWVALUE", NewValue);
                    cmd.Parameters.AddWithValue("@REASON", Reason);
                    cmd.Parameters.AddWithValue("@COMMENTS", Comments);
                    cmd.Parameters.AddWithValue("@CHANGEBY", UserId);
                    cmd.Parameters.AddWithValue("@ControlType", ControlType);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else//for demograph data and audit trail
                {
                    if (FieldName == "DOB")
                    {
                        string date1 = System.DateTime.Now.ToString("dd-MMM-yyyy");
                        DateTime dt2 = Convert.ToDateTime(NewValue);
                        DateTime dt1 = Convert.ToDateTime(date1);
                        TimeSpan span = dt1 - dt2;
                        if (9862 >= span.TotalDays)
                        {
                            throw new Exception("Age should be greater than or equal to 27 years.");
                        }
                    }
                    cmd = new SqlCommand("CHANGE_SP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UPDATE");
                    cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                    cmd.Parameters.AddWithValue("@INVID", Convert.ToInt32(INVID));
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                    if (VISITNUM != "NULL")
                    {
                        cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                    }
                    cmd.Parameters.AddWithValue("@FIELDNAME", FieldName);
                    cmd.Parameters.AddWithValue("@OLDVALUE", OldValue);
                    cmd.Parameters.AddWithValue("@NEWVALUE", NewValue);
                    cmd.Parameters.AddWithValue("@REASON", Reason);
                    cmd.Parameters.AddWithValue("@COMMENTS", Comments);
                    cmd.Parameters.AddWithValue("@CHANGEBY", UserId);
                    cmd.Parameters.AddWithValue("@ControlType", ControlType);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            finally
            {
                cmd = null;
            }
            return "Record Updated successfully.";
        }


        //Method for override query by invetigator and reopen query by cdm
        [System.Web.Services.WebMethod]
        public static string GetEnrollment(string ProjectID, string INVID, string Age_Group)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                ds = dal.InsertUpdateSubjectRegistration(Action: "19", Project_ID: ProjectID, INVID: INVID, Age_Group: Age_Group);
                if (ds.Tables.Count > 0)
                {
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            var str = "<table id ='grdgetComments' Class='table table-bordered table-striped'><tbody>";
                            str = str + "<tr> <th>Subject ID</th><th>Randomization No.</th> <th>Date of Screening</th> ";

                            //create column
                            for (int i = 5; i < ds.Tables[0].Columns.Count; i++)
                            {
                                str = str + "<th>";
                                str = str + ds.Tables[0].Columns[i].ToString();
                                str = str + "</th>";
                            }
                            str = str + "</tr>";

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                str = str + "<tr>";
                                for (int j = 2; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    string TEXT = ds.Tables[0].Rows[i][j].ToString();
                                    string[] words = TEXT.Split(';');
                                    int length = words.Length;
                                    string Actual = words[0];
                                    if (length > 1)
                                    {
                                        string Schedule = words[1];
                                        str = str + "<td>" + Actual + "</br></br>" + Schedule + "</td>";
                                    }
                                    else
                                    {
                                        str = str + "<td>" + Actual + "</td>";
                                    }

                                }
                                str = str + "</tr>";
                            }
                            str = str + "</tbody></table>";
                            return str;
                        }
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
        public static string INSERT_CHECKLIST_QUERY(string Action, string ProjectID, string SECTIONID, string SUBSECTIONID, string INVID, string MVID, string CHECKLIST_ID, string QUERY, string QID, string Comments)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                string UserId = HttpContext.Current.Session["User_ID"].ToString();

                if (Action == "INSERT_CHECKLIST_QUERY")
                {
                    ds = dal.GetSetMonitoringVisit(
                    Action: Action,
                    PROJECTID: ProjectID,
                    SECTIONID: SECTIONID,
                    SUBSECTIONID: SUBSECTIONID,
                    INVID: INVID,
                    MVID: MVID,
                    CHECKLIST_ID: CHECKLIST_ID,
                    QUERY: QUERY,
                    ENTEREDBY: UserId
                    );
                    return "Record Updated successfully.";
                }
                if (Action == "CHECKLIST_QUERY_COMMENTS")
                {
                    ds = dal.GetSetMonitoringVisit(
                    Action: Action,
                    PROJECTID: ProjectID,
                    SECTIONID: SECTIONID,
                    SUBSECTIONID: SUBSECTIONID,
                    INVID: INVID,
                    MVID: MVID,
                    CHECKLIST_ID: CHECKLIST_ID,
                    QID: QID,
                    COMMENTS: Comments,
                    ENTEREDBY: UserId
                    );
                    return "Record Updated successfully.";
                }
                if (Action == "CLOSE_QUERY")
                {
                    ds = dal.GetSetMonitoringVisit(
                    Action: Action,
                    PROJECTID: ProjectID,
                    SECTIONID: SECTIONID,
                    SUBSECTIONID: SUBSECTIONID,
                    INVID: INVID,
                    MVID: MVID,
                    CHECKLIST_ID: CHECKLIST_ID,
                    QID: QID,
                    COMMENTS: Comments,
                    ENTEREDBY: UserId
                    );
                    return "Record Updated successfully.";
                }
                if (Action == "GET_CHECKLIST_QUERY")
                {
                    ds = dal.GetSetMonitoringVisit(
                    Action: Action,
                    PROJECTID: ProjectID,
                    SECTIONID: SECTIONID,
                    SUBSECTIONID: SUBSECTIONID,
                    INVID: INVID,
                    MVID: MVID,
                    CHECKLIST_ID: CHECKLIST_ID,
                    QUERY: QUERY,
                    ENTEREDBY: UserId
                    );
                    if (ds != null)
                    {
                        var str = "";
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            str = "<span id='MainContent_Label14' class='wrapperLable'>Query</span>";
                            str = str + "<table id ='grdManualQuery' font-family='Arial' style='border: 1px solid black; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                            str = str + "<tr><th style='border: 1px solid black;'>ID</th><th style='border: 1px solid black;'>Query</th><th style='border: 1px solid black;'>Entered Date</th><th style='border: 1px solid black;'>Raised By</th>";

                            if (UserId == ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ENTEREDBY"].ToString())
                            {
                                str = str + "<th style='border: 1px solid black;'>Close Query</th>";
                            }
                            str = str + "</tr>";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                str = str + "<tr style='text-align: left;'>";
                                str = str +
                                       "</td><td style='border: 1px solid black; text-align: CENTER;'>" +
                                    ds.Tables[0].Rows[i]["QID"].ToString() +
                                    "</td><td style='border: 1px solid black;'>" +
                                    ds.Tables[0].Rows[i]["Query"].ToString() +
                                    "</td><td style='border: 1px solid black; text-align: CENTER;'>" +
                                    ds.Tables[0].Rows[i]["DTENTERED"].ToString() +
                                    "</td><td style='border: 1px solid black; text-align: CENTER;'>" +
                                    ds.Tables[0].Rows[i]["User_Name"].ToString() +
                                    "</td>";
                                if (UserId == ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ENTEREDBY"].ToString())
                                {
                                    str = str + "<td style='border: 1px solid black; text-align: CENTER;'> <input name='ctl00$MainContent$btnMQSave' value='Close' class='btn btn-primary btn-sm' onclick='CloseQuery(this);' id='MainContent_btnMQCloseQuery'  type='submit'></td>";
                                }
                                str = str + "</tr>";
                            }
                            str = str + "</tbody></table>";

                            str = str + "<input id='MQQID_Id' class='disp-none' value =" + ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["QID"].ToString() + " >";
                            str = str + "<input id='MQUser_Id' class='disp-none' value =" + ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ENTEREDBY"].ToString() + " >";

                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                str = str + "<span id='MainContent_Label15' style='margin-top: 4px' class='wrapperLable'>Comments</span>";
                                str = str + "<table id ='grdManualQueryComments' font-family='Arial' style='border: 1px solid black; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                                str = str + "<tr><th style='border: 1px solid black;'>Comments</th><th style='border: 1px solid black; width:100px'>Entered Date</th><th style='border: 1px solid black;'>Entered By</th></tr>";
                                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                {
                                    str = str + "<tr style='text-align: left;'>";
                                    str = str + "</td><td style='border: 1px solid black;'>" +
                                        ds.Tables[1].Rows[i]["COMMENTS"].ToString() +
                                        "</td><td style='border: 1px solid black; text-align: CENTER;'>" +
                                        ds.Tables[1].Rows[i]["DTENTERED"].ToString() +
                                        "</td></td><td style='border: 1px solid black; text-align: CENTER;'>" +
                                       ds.Tables[1].Rows[i]["User_Name"].ToString() + "</td>";
                                    str = str + "</tr>";
                                }
                                str = str + "</tbody></table>";
                            }
                            //if (UserGroup_ID == ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["UserGroup_Name"].ToString())
                            //{
                            //    str = str + " <div class='formControl'  style='margin: 10px 0px;' id='divMQAction'>";
                            //    str = str + "<span id='Label21'  style='width: 104px;' class='wrapperLable'>Reason</span>";
                            //    str = str + "<select name='ct300$drpMQAction' id='drpMQAction' class='width245px'>";
                            //    str = str + "<option value='0'>--Select--</option>";
                            //    str = str + "<option value='Resolve'>Resolve</option>";
                            //    str = str + "<option value='Override'>Override</option>";
                            //}
                        }
                        return str;
                    }
                    return "";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }

        }


        //Method for update data in table and audit trial    
        [System.Web.Services.WebMethod]
        public static string BudgetPP_Audit_Trail(string Action, string Task_ID, string Sub_Task_ID, string FieldName, string TableName, string VariableName, string OldValue, string NewValue, string Reason, string Comments, string AutoId)
        {
            SqlCommand cmd;
            SqlConnection con = new SqlConnection();
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(HttpContext.Current.Session["CHILD_CONN"].ToString());
            }
            else
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
            }
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(getconstrCHILD());
            }
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp;
                string Project_ID = HttpContext.Current.Session["PROJECTID"].ToString();
                string UserId = HttpContext.Current.Session["User_ID"].ToString();

                cmd = new SqlCommand("Budget_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Task_ID", Task_ID);
                cmd.Parameters.AddWithValue("@Sub_Task_ID", Sub_Task_ID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FieldName);
                cmd.Parameters.AddWithValue("@TABLENAME", TableName);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VariableName);
                cmd.Parameters.AddWithValue("@OLDVALUE", OldValue);
                cmd.Parameters.AddWithValue("@NEWVALUE", NewValue);
                cmd.Parameters.AddWithValue("@REASON", Reason);
                cmd.Parameters.AddWithValue("@COMMENTS", Comments);
                cmd.Parameters.AddWithValue("@CHANGEBY", UserId);
                cmd.Parameters.AddWithValue("@ID", AutoId);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();

                if (Action == "Get_Audit_Trail")
                {
                    if (ds != null)
                    {
                        var str = "";
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            str = str + "<table id ='grdAuditTrail' font-family='Arial' style='border: 1px solid black; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                            str = str + "<tr>";
                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                            {
                                str = str + "<th style='border: 1px solid black;'> "
                                    + ds.Tables[0].Columns[i].ToString() + "</th>";
                            }
                            str = str + "</tr>";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                str = str + "<tr style='text-align: left;'>";

                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    str = str + "<td style='border: 1px solid black;'> "
                                        + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                                }
                                str = str + "</tr>";
                            }
                            str = str + "</tbody></table>";
                        }
                        return str;
                    }
                    return "";
                }
                else
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            finally
            {
                cmd = null;
            }
            //return "Record Updated successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string BudgetPP_Audit_Trail_CRA(string Action, string INVID, string Task_ID, string Sub_Task_ID, string FieldName, string TableName, string VariableName, string OldValue, string NewValue, string Reason, string Comments, string AutoId)
        {
            SqlCommand cmd;
            SqlConnection con = new SqlConnection();
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(HttpContext.Current.Session["CHILD_CONN"].ToString());
            }
            else
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
            }
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(getconstrCHILD());
            }
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp;
                string Project_ID = HttpContext.Current.Session["PROJECTID"].ToString();
                string UserId = HttpContext.Current.Session["User_ID"].ToString();

                cmd = new SqlCommand("Budget_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@Task_ID", Task_ID);
                cmd.Parameters.AddWithValue("@Sub_Task_ID", Sub_Task_ID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FieldName);
                cmd.Parameters.AddWithValue("@TABLENAME", TableName);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VariableName);
                cmd.Parameters.AddWithValue("@OLDVALUE", OldValue);
                cmd.Parameters.AddWithValue("@NEWVALUE", NewValue);
                cmd.Parameters.AddWithValue("@REASON", Reason);
                cmd.Parameters.AddWithValue("@COMMENTS", Comments);
                cmd.Parameters.AddWithValue("@CHANGEBY", UserId);
                cmd.Parameters.AddWithValue("@ID", AutoId);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();

                if (Action == "Get_Audit_Trail_CRA")
                {
                    if (ds != null)
                    {
                        var str = "";
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            str = str + "<table id ='grdAuditTrail' font-family='Arial' style='border: 1px solid black; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                            str = str + "<tr>";
                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                            {
                                str = str + "<th style='border: 1px solid black;'> "
                                    + ds.Tables[0].Columns[i].ToString() + "</th>";
                            }
                            str = str + "</tr>";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                str = str + "<tr style='text-align: left;'>";

                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    str = str + "<td style='border: 1px solid black;'> "
                                        + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                                }
                                str = str + "</tr>";
                            }
                            str = str + "</tbody></table>";
                        }
                        return str;
                    }
                    return "";
                }
                else
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            finally
            {
                cmd = null;
            }
            //return "Record Updated successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string BudgetPP_Audit_Trail_Recurring(string Action, string INVID, string Task_ID, string Sub_Task_ID, string FieldName, string TableName, string VariableName, string OldValue, string NewValue, string Reason, string Comments, string AutoId)
        {
            SqlCommand cmd;
            SqlConnection con = new SqlConnection();
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(HttpContext.Current.Session["CHILD_CONN"].ToString());
            }
            else
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
            }
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(getconstrCHILD());
            }
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp;
                string Project_ID = HttpContext.Current.Session["PROJECTID"].ToString();
                string UserId = HttpContext.Current.Session["User_ID"].ToString();

                cmd = new SqlCommand("Budget_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@Task_ID", Task_ID);
                cmd.Parameters.AddWithValue("@Sub_Task_ID", Sub_Task_ID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FieldName);
                cmd.Parameters.AddWithValue("@TABLENAME", TableName);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VariableName);
                cmd.Parameters.AddWithValue("@OLDVALUE", OldValue);
                cmd.Parameters.AddWithValue("@NEWVALUE", NewValue);
                cmd.Parameters.AddWithValue("@REASON", Reason);
                cmd.Parameters.AddWithValue("@COMMENTS", Comments);
                cmd.Parameters.AddWithValue("@CHANGEBY", UserId);
                cmd.Parameters.AddWithValue("@ID", AutoId);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();

                if (Action == "Get_Audit_Trail_Recurring")
                {
                    if (ds != null)
                    {
                        var str = "";
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            str = str + "<table id ='grdAuditTrail' font-family='Arial' style='border: 1px solid black; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                            str = str + "<tr>";
                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                            {
                                str = str + "<th style='border: 1px solid black;'> "
                                    + ds.Tables[0].Columns[i].ToString() + "</th>";
                            }
                            str = str + "</tr>";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                str = str + "<tr style='text-align: left;'>";

                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    str = str + "<td style='border: 1px solid black;'> "
                                        + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                                }
                                str = str + "</tr>";
                            }
                            str = str + "</tbody></table>";
                        }
                        return str;
                    }
                    return "";
                }
                else
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            finally
            {
                cmd = null;
            }
            //return "Record Updated successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string Risk_Audit_Trail(string Action, string ID, string Risk_ID, string FieldName, string TableName, string VariableName, string OldValue, string NewValue, string Reason, string Comments, string AutoId)
        {
            SqlCommand cmd;
            SqlConnection con = new SqlConnection();
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(HttpContext.Current.Session["CHILD_CONN"].ToString());
            }
            else
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
            }
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(getconstrCHILD());
            }
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp;
                string Project_ID = HttpContext.Current.Session["PROJECTID"].ToString();
                string UserId = HttpContext.Current.Session["User_ID"].ToString();

                cmd = new SqlCommand("Budget_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Risk_ID", Risk_ID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FieldName);
                cmd.Parameters.AddWithValue("@TABLENAME", TableName);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VariableName);
                cmd.Parameters.AddWithValue("@OLDVALUE", OldValue);
                cmd.Parameters.AddWithValue("@NEWVALUE", NewValue);
                cmd.Parameters.AddWithValue("@REASON", Reason);
                cmd.Parameters.AddWithValue("@COMMENTS", Comments);
                cmd.Parameters.AddWithValue("@CHANGEBY", UserId);
                cmd.Parameters.AddWithValue("@ID", AutoId);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();

                if (Action == "Get_Audit_Trail")
                {
                    if (ds != null)
                    {
                        var str = "";
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            str = str + "<table id ='grdAuditTrail' font-family='Arial' style='border: 1px solid black; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                            str = str + "<tr>";
                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                            {
                                str = str + "<th style='border: 1px solid black;'> "
                                    + ds.Tables[0].Columns[i].ToString() + "</th>";
                            }
                            str = str + "</tr>";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                str = str + "<tr style='text-align: left;'>";

                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    str = str + "<td style='border: 1px solid black;'> "
                                        + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                                }
                                str = str + "</tr>";
                            }
                            str = str + "</tbody></table>";
                        }
                        return str;
                    }
                    return "";
                }
                else
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            finally
            {
                cmd = null;
            }
            //return "Record Updated successfully.";
        }

        //Method for get  pvid from session    
        [WebMethod(EnableSession = true)]
        public static string GetPvId()
        {
            try
            {
                return HttpContext.Current.Session["PVID"].ToString();
            }
            catch (Exception ex)
            {
                // return "Object reference not set to an instance of an object.";
                throw;
            }

        }
        //Method for get  RECID from session    
        [WebMethod(EnableSession = true)]
        public static string GetRecId()
        {
            try
            {
                return HttpContext.Current.Session["RECID"].ToString();
            }
            catch (Exception ex)
            {
                //return ex.ToString();
                throw;
            }
        }

        [System.Web.Services.WebMethod]
        public static string Risk_saveData(string P, string S, string D, string RPN, string RiskCat, string Notes, string Up, string Down, string Comment)
        {
            string RiskID = HttpContext.Current.Session["RiskID"].ToString();
            string UserId = HttpContext.Current.Session["User_ID"].ToString();

            SqlCommand cmd;
            DataSet ds;
            SqlConnection con = new SqlConnection();
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(HttpContext.Current.Session["CHILD_CONN"].ToString());
            }
            else
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
            }
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(getconstrCHILD());
            }
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("updateRiskBucket", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", RiskID);
                cmd.Parameters.AddWithValue("@P", P);
                cmd.Parameters.AddWithValue("@S", S);
                cmd.Parameters.AddWithValue("@D", D);
                cmd.Parameters.AddWithValue("@RPN", RPN);
                cmd.Parameters.AddWithValue("@RiskCat", RiskCat);
                cmd.Parameters.AddWithValue("@Notes", Notes);
                cmd.Parameters.AddWithValue("@Up_Trigger", Up);
                cmd.Parameters.AddWithValue("@Down_Trigger", Down);
                cmd.Parameters.AddWithValue("@User_ID", UserId);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return "Record Updated Successfully. ";
        }

        [System.Web.Services.WebMethod]
        public static string SET_Dashboard(string X, string Y, string Width, string Height, string ID)
        {
            string Project_ID = HttpContext.Current.Session["PROJECTID"].ToString();
            string User_ID = HttpContext.Current.Session["User_ID"].ToString();

            SqlCommand cmd;
            DataSet ds;
            SqlConnection con = new SqlConnection();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("DASHBOARD_ASSIGNING", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", "SET");
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@USERID", User_ID);
                cmd.Parameters.AddWithValue("@PROJECTID", Project_ID);
                cmd.Parameters.AddWithValue("@X", X);
                cmd.Parameters.AddWithValue("@Y", Y);
                cmd.Parameters.AddWithValue("@Width", Width);
                cmd.Parameters.AddWithValue("@Height", Height);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return "Record Updated Successfully. ";
        }

        [System.Web.Services.WebMethod]
        public static string SET_RiskIndicator(string X, string Y, string Width, string Height, string ID)
        {
            SqlCommand cmd;
            DataSet ds;
            SqlConnection con = new SqlConnection();

            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(getconstrCHILD());
            }

            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("Risk_Indicator_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SET_RiskIndicator");
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@L1", X);
                cmd.Parameters.AddWithValue("@L2", Y);
                cmd.Parameters.AddWithValue("@CL1", Width);
                cmd.Parameters.AddWithValue("@CL2", Height);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return "Record Updated Successfully. ";
        }


        [System.Web.Services.WebMethod]
        public static string SET_RiskIndicator_User(string X, string Y, string Width, string Height, string ID)
        {
            SqlCommand cmd;
            DataSet ds;
            SqlConnection con = new SqlConnection();

            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(getconstrCHILD());
            }

            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("Risk_Indicator_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SET_RiskIndicator_User");
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@L1", X);
                cmd.Parameters.AddWithValue("@L2", Y);
                cmd.Parameters.AddWithValue("@CL1", Width);
                cmd.Parameters.AddWithValue("@CL2", Height);
                cmd.Parameters.AddWithValue("@Result", GetUserId());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return "Record Updated Successfully. ";
        }


        //Method for override query by invetigator and reopen query by cdm
        [System.Web.Services.WebMethod]
        public static string GetFilterData(string FilterName)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                //ds = dal.GetCustomizedAE_Filter_SP(Action: "GetFilterList_Data", FunctionName: FilterName);
                ds = dal.GetCustomizedAE_Filter_SP(Action: "INSERT_AEFILTER", FunctionName: FilterName);
                //return "</tbody></table>";
                //if (ds != null)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        var str = "<table id ='grdgetComments' Class='table table-bordered table-striped'><tbody>";
                //        str = str + "<tr>";

                //        //create column
                //        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                //        {
                //            str = str + "<th>";
                //            str = str + ds.Tables[0].Columns[i].ToString();
                //            str = str + "</th>";
                //        }
                //        str = str + "</tr>";

                //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //        {
                //            str = str + "<tr>";
                //            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                //            {

                //                str = str + "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";

                //            }
                //            str = str + "</tr>";
                //        }
                //        str = str + "</tbody></table>";
                //        return str;

                //    }
                //}
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        //Method for get  pvid from session    
        [WebMethod(EnableSession = true)]
        public static string GetSAEID()
        {
            try
            {
                return HttpContext.Current.Session["SAEID"].ToString();
            }
            catch (Exception ex)
            {
                // return "Object reference not set to an instance of an object.";
                throw;
            }

        }

        //Method for get  pvid from session    
        [WebMethod(EnableSession = true)]
        public static string GetSAE()
        {
            try
            {
                return HttpContext.Current.Session["SAE_SAE"].ToString();
            }
            catch (Exception ex)
            {
                // return "Object reference not set to an instance of an object.";
                throw;
            }

        }

        //Method for get  pvid from session    
        [WebMethod(EnableSession = true)]
        public static string GetSAE_INVID()
        {
            try
            {
                return HttpContext.Current.Session["SAE_INVID"].ToString();
            }
            catch (Exception ex)
            {
                // return "Object reference not set to an instance of an object.";
                throw;
            }

        }

        //Method for get  pvid from session    
        [WebMethod(EnableSession = true)]
        public static string GetSAE_SUBJID()
        {
            try
            {
                return HttpContext.Current.Session["SAE_SUBJID"].ToString();
            }
            catch (Exception ex)
            {
                // return "Object reference not set to an instance of an object.";
                throw;
            }

        }

        //Method for get  RECID from session    
        [WebMethod(EnableSession = true)]
        public static string GetSAE_RecId()
        {
            try
            {
                return HttpContext.Current.Session["SAE_RECID"].ToString();
            }
            catch (Exception ex)
            {
                //return ex.ToString();
                throw;
            }
        }

        //get manual query data and comments on click of manual query icon
        [System.Web.Services.WebMethod]
        public static string SAE_GetManualQueryData(string VariableName, string ContId, string MODULEID, string SAEID, string RECID)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                string UserGroup_ID = HttpContext.Current.Session["UserGroup_ID"].ToString();

                string MASTERDBNAME = HttpContext.Current.Session["InitialCatalog"].ToString();

                ds = dal.DM_ManQuery_SP(Action: "Get_ManualQuery_Data",
                SAEID: SAEID,
                VARIABLENAME: VariableName,
                RECID: (RECID),
                MODULEID: MODULEID,
                MASTERDB: MASTERDBNAME
                );

                if (ds != null)
                {
                    var str = "";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = "<table id ='MainContent_grdManualQuery' font-family='Arial' style='border: 1px solid black;  margin-left: 104px; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                        str = str + "<tr><th style='border: 1px solid black; display:none'>ID</th><th style='border: 1px solid black;'>Field Name</th><th style='border: 1px solid black;'>Query Text</th><th style='border: 1px solid black;'>STATUS</th><th style='border: 1px solid black;'>RAISED BY</th><th style='border: 1px solid black;'>RAISED DATE</th></tr>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            str = str + "<tr style='text-align: left;'>";
                            str = str + "<td style='border: 1px solid black; display:none'>" + ds.Tables[0].Rows[i]["ID"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["FIELDNAME"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["QUERYTEXT"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["STATUSTEXT"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["User_Name"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["QRYGENDAT"].ToString() + "</td>";
                            str = str + "</tr>";
                        }
                        str = str + "</tbody></table>";

                        str = str + "<input id='MQQID_Id' class='disp-none' value =" + ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ID"].ToString() + " >";
                        str = str + "<input id='MQUserGroup_Id' class='disp-none' value =" + ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["UserGroup_Name"].ToString() + " >";

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            str = str + "<table id ='MainContent_grdManualQueryComments' font-family='Arial' style='margin-left: 104px; border: 1px solid black; margin-top: 10px; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                            str = str + "<tr><th style='border: 1px solid black;'>Comments</th><th style='border: 1px solid black;'>ENTERED BY</th><th style='border: 1px solid black;'>ENTERED DATE</th></tr>";
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                str = str + "<tr style='text-align: left;'>";
                                str = str + "<td style='border: 1px solid black;'>" + ds.Tables[1].Rows[i]["COMMENTS"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[1].Rows[i]["ENTEREDBY"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[1].Rows[i]["DTENTEREDDATE"].ToString() + "</td>";
                                str = str + "</tr>";
                            }
                            str = str + "</tbody></table>";
                        }
                        if (UserGroup_ID == ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["UserGroup_Name"].ToString())
                        {
                            str = str + " <div class='formControl'  style='margin: 10px 0px;display: inline-flex;' id='divMQAction'>";
                            str = str + "<span id='Label21'  style='width: 104px;' class='wrapperLable'>Action</span>";
                            str = str + "<select name='ct300$drpMQAction' id='drpMQAction' class='form-control width245px'>";
                            str = str + "<option value='0'>None</option>";
                            str = str + "<option value='Resolve'>Resolve</option>";
                            str = str + "<option value='Override'>Override</option>";
                        }
                    }
                    return str;
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }


        //Method for get  STATUS from session    
        [WebMethod(EnableSession = true)]
        public static string GetSTATUS()
        {
            try
            {
                return HttpContext.Current.Session["SAE_STATUS"].ToString();
            }
            catch (Exception ex)
            {
                // return "Object reference not set to an instance of an object.";
                throw;
            }

        }


        //Enter SUBJECT
        [System.Web.Services.WebMethod]
        public static string ePRO_SUBJECT(string SUBJECT, string USERID, string MASTERDB)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                ds = dal.ePRO_ADD_UPDATE(ACTION: "INSERT_SUBJECT_TO_USERDETAILS", SUBJECTID: SUBJECT, USERID: USERID, MASTERDB: MASTERDB);
                HttpContext.Current.Session["ePRO_SUBJECTS"] = SUBJECT;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Record Updated successfully";
        }

        [System.Web.Services.WebMethod]
        public static string POST_TO_RISK(string CategoryId, string SubcategoryId, string TopicId, string Risk_Description, string RiskNature, string Risk_Count, string Risk_Manager
            , string Dept, string Source, string Reference, string RStatus, string ROwner, string IssueID, string PROTOCOLID)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                if (IssueID != "")
                {
                    dal.insertRiskEvent(ACTION: "POST_TO_RISK",
                        CategoryId: CategoryId,
                        SubcategoryId: SubcategoryId,
                        TopicId: TopicId,
                        Risk_Description: Risk_Description,
                        Possible_Mitigations: "",
                        RiskNature: RiskNature,
                        ProjectId: HttpContext.Current.Session["PROJECTID"].ToString(),
                        Risk_Count: Risk_Count,
                        Risk_Date: DateTime.Now.ToString(),
                        Risk_Manager: Risk_Manager,
                        Dept: Dept,
                        issueId: IssueID,
                        Source: Source,
                        Reference: IssueID,
                        RStatus: RStatus,
                        ROwner: ROwner
                        );
                }
                else
                {
                    dal.insertRiskEvent(ACTION: "POST_TO_RISK",
                        CategoryId: CategoryId,
                        SubcategoryId: SubcategoryId,
                        TopicId: TopicId,
                        Risk_Description: Risk_Description,
                        Possible_Mitigations: "",
                        RiskNature: RiskNature,
                        ProjectId: HttpContext.Current.Session["PROJECTID"].ToString(),
                        Risk_Count: Risk_Count,
                        Risk_Date: DateTime.Now.ToString(),
                        Risk_Manager: Risk_Manager,
                        Dept: Dept,
                        PROTOCOLID: PROTOCOLID,
                        Source: "Protocol Deviation",
                        Reference: PROTOCOLID,
                        RStatus: RStatus,
                        ROwner: ROwner
                        );
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Record Updated successfully";
        }


        [System.Web.Services.WebMethod]
        public static string POST_PROTOCOL_TO_ISSUE(string InvID, string SUBJID, string Dept, string Summary, string Status, string Classification, string Description
            , string RefID, string Source)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                dal.getsetISSUES(Action: "POST_PROTOCOL_TO_ISSUE",
            Project_ID: GetProjectId(),
            INVID: InvID,
            SUBJID: SUBJID,
            Department: Dept,
            Summary: Summary,
            Status: Status,
            Priority: Classification,
            Description: Description,
            ISSUEBy: GetUserId(),
            Source: "Protocol Deviation",
            Refrence: RefID,
            ENTEREDBY: GetUserId()
            );

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Record Updated successfully";
        }

        [System.Web.Services.WebMethod]
        public static string POST_PROTOCOL_TO_ISSUE_ALL(string InvID, string SUBJID, string Dept, string Summary, string Status, string Classification, string Description
            , string RefID, string Source)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                dal.getsetISSUES(Action: "POST_PROTOCOL_TO_ISSUE_ALL",
            Project_ID: GetProjectId(),
            INVID: InvID,
            SUBJID: SUBJID,
            Department: Dept,
            Summary: Summary,
            Status: Status,
            Priority: Classification,
            Description: Description,
            ISSUEBy: GetUserId(),
            Source: "Protocol Deviation",
            Refrence: RefID,
            ENTEREDBY: GetUserId()
            );

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Record Updated successfully";
        }

        [System.Web.Services.WebMethod]
        public static string INSERT_RISK(string CategoryId, string SubcategoryId, string TopicId, string Risk_Description, string RiskNature, string Risk_Count, string Risk_Manager
            , string Dept, string Source, string Reference, string RStatus, string ROwner, string IssueID, string PROTOCOLID)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                if (IssueID != "")
                {
                    dal.insertRiskEvent(ACTION: "INSERT_RISK",
                        CategoryId: CategoryId,
                        SubcategoryId: SubcategoryId,
                        TopicId: TopicId,
                        Risk_Description: Risk_Description,
                        Possible_Mitigations: "",
                        RiskNature: RiskNature,
                        ProjectId: HttpContext.Current.Session["PROJECTID"].ToString(),
                        Risk_Count: Risk_Count,
                        Risk_Date: DateTime.Now.ToString(),
                        Risk_Manager: Risk_Manager,
                        Dept: Dept,
                        issueId: IssueID,
                        Source: Source,
                        Reference: IssueID,
                        RStatus: RStatus,
                        ROwner: ROwner
                        );
                }
                else
                {
                    dal.insertRiskEvent(ACTION: "INSERT_RISK",
                        CategoryId: CategoryId,
                        SubcategoryId: SubcategoryId,
                        TopicId: TopicId,
                        Risk_Description: Risk_Description,
                        Possible_Mitigations: "",
                        RiskNature: RiskNature,
                        ProjectId: HttpContext.Current.Session["PROJECTID"].ToString(),
                        Risk_Count: Risk_Count,
                        Risk_Date: DateTime.Now.ToString(),
                        Risk_Manager: Risk_Manager,
                        Dept: Dept,
                        PROTOCOLID: PROTOCOLID,
                        Source: "Protocol Deviation",
                        Reference: PROTOCOLID,
                        RStatus: RStatus,
                        ROwner: ROwner
                        );
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Record Updated successfully";
        }

        [System.Web.Services.WebMethod]
        public static void SetSession(string value)
        {
            HttpContext.Current.Session["TOPFREQAENUM"] = value;
        }


        [System.Web.Services.WebMethod]
        public static void UPDATE_ACTIVITY_LOG(string ID)
        {
            try
            {
                DAL dal = new DAL();

                if (ID != "undefined")
                {
                    dal.ACTIVITY_LOG_SP
                    (
                    Action: "UPDATE",
                    ID: ID
                    );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [System.Web.Services.WebMethod]
        public static bool checkValid(string URL)
        {
            bool res = true;
            try
            {
                string PROJECTID = GetProjectId();
                string USERID = GetUserId();

                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                ds = dal.ManageUserGroups(ACTION: "checkValid", FUNCTIONNAME: URL, User_ID: USERID, PROJECTID: PROJECTID);
                if (ds.Tables[0].Rows.Count < 1)
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return res;
        }



        //Method for get  userid from session    
        [WebMethod(EnableSession = true)]
        public static string GET_TIMEZONE()
        {
            try
            {
                return HttpContext.Current.Session["TimeZone_Value"].ToString();
            }
            catch (Exception ex)
            {
                //  return ex.Message.ToString();
                throw;
            }
        }

        [System.Web.Services.WebMethod]
        //public static string SetExpDoc(string ID, string ExpDoc, string Comments)
        //{
        //    string SDVSTATUS = "";
        //    try
        //    {
        //         DAL_eTMF dal_eTMF = new DAL_eTMF();

        //        dal_eTMF.eTMF_SET_SP(ACTION: "SetExpDoc", ID: ID, NoExpDocs: ExpDoc, Comment: Comments);

        //        if (ExpDoc == "0")
        //        {
        //            SDVSTATUS = "True";
        //        }
        //        else
        //        {
        //            SDVSTATUS = "False";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //        throw;
        //    }
        //    return SDVSTATUS;
        //}

        //[System.Web.Services.WebMethod]
        public static string SetVerDateYes(string ID)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                CommonFunction ComFun = new CommonFunction();
                dal_eTMF.eTMF_SET_SP(ACTION: "SetVerDateYes", ID: ID);

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string SetVerDateNo(string ID)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                CommonFunction ComFun = new CommonFunction();
                dal_eTMF.eTMF_SET_SP(ACTION: "SetVerDateNo", ID: ID);

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string SetVerType(string ID, string VerType)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                CommonFunction ComFun = new CommonFunction();
                dal_eTMF.eTMF_SET_SP
                    (ACTION: "SetVerTYPE",
                    ID: ID,
                    VerTYPE: VerType
                    );


            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string SetAutoReplace(string ID, string AutoReplace)
        {
            string SDVSTATUS = "";
            try
            {

                DAL_eTMF dal_eTMF = new DAL_eTMF();
                CommonFunction ComFun = new CommonFunction();
                dal_eTMF.eTMF_SET_SP
                    (ACTION: "SetAutoReplace",
                    ID: ID,
                    AutoReplace: AutoReplace
                    );
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string SetUnblinding(string ID, string UnblindingType)
        {
            string SDVSTATUS = "";
            try
            {
                CommonFunction ComFun = new CommonFunction();
                DAL_eTMF dal_eTMF = new DAL_eTMF();

                dal_eTMF.eTMF_SET_SP(ACTION: "SetUnblinding", ID: ID, UnblindingType: UnblindingType);



            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string REACT_MARK_SDV(string SITEID, string SUBJID, string TABLENAME, string AUTOID)
        {
            string SDVSTATUS = "";
            try
            {
                DAL dal = new DAL();

                DataSet ds;
                ds = new DataSet();

                string USERID = GetUserId();

                ds = dal.DM_SDV_SP(
                            ACTION: "MARK_SDV",
                            SITEID: SITEID,
                            SUBJID: SUBJID,
                            TABLENAME: TABLENAME,
                            AUTOID: AUTOID,
                            SDVBY: USERID
                            );

                SDVSTATUS = ds.Tables[0].Rows[0]["SDVSTATUS"].ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string REACT_UNMARK_SDV(string SITEID, string SUBJID, string TABLENAME, string AUTOID)
        {
            string SDVSTATUS = "";
            try
            {
                DAL dal = new DAL();

                DataSet ds;
                ds = new DataSet();

                string USERID = GetUserId();

                ds = dal.DM_SDV_SP(
                            ACTION: "UNMARK_SDV",
                            SITEID: SITEID,
                            SUBJID: SUBJID,
                            TABLENAME: TABLENAME,
                            AUTOID: AUTOID,
                            SDVBY: USERID
                            );

                SDVSTATUS = ds.Tables[0].Rows[0]["SDVSTATUS"].ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string REACT_GET_NOTES(string TABLENAME, string AUTOID)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                ds = dal.DM_SDV_SP(ACTION: "GET_NOTES",
                TABLENAME: TABLENAME,
                AUTOID: AUTOID
                );

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var str = "<table id ='grdNotes' font-family='Arial' style='border: 1px solid black; color:#333333;width: 100%;font-size:small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                        str = str + "<tr><th style='border: 1px solid black;'>Notes</th><th style='border: 1px solid black;'>User Name</th><th style='border: 1px solid black;'>Date</th></tr>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            str = str + "<tr style='text-align: left;'>";
                            str = str + "<td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["NOTES"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["User_Name"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["ENTEREDDAT"].ToString() + "</td>";
                            str = str + "</tr>";
                        }
                        str = str + "</tbody></table>";
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
        public static string GetDocComment(string ID)
        {
            string Comment = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                CommonFunction ComFun = new CommonFunction();
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GetDocComment", ID: ID);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    Comment = ds.Tables[0].Rows[0]["Comment"].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return Comment;
        }

        [System.Web.Services.WebMethod]
        public static string SetDocOwner(string ID, string UserName)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                CommonFunction ComFun = new CommonFunction();

                dal_eTMF.eTMF_SET_SP(ACTION: "SetDocOwner", ID: ID, User: UserName);

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string INSERT_DOC_OPEN_LOGS(string DOCID)
        {
            string msg = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                string USERID = GetUserId();

                dal_eTMF.eTMF_UPLOAD_SP(
                    ACTION: "INSERT_DOC_OPEN_LOGS",
                    DocID: DOCID,
                    USERID: USERID
                    );
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return msg;
        }

        //getaAudit trail of particular field
        [System.Web.Services.WebMethod]
        public static string CTMS_GetAuditTrail(string VariableName, string ContId, string CTMS_SVID, string CTMS_RECID)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                string SVID = CTMS_SVID;
                int RECID = Convert.ToInt32(CTMS_RECID);
                string TIMEZONE = GET_TIMEZONE();

                ds = dal.CTMS_GetAUDITTRAILDETAILS(Action: "GET_DATA_PARTICULAR", SVID: SVID, VARIABLENAME: VariableName, RECID: RECID, TIMEZONE: TIMEZONE);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var str = "<table id ='grdAuditTrail' font-family='Arial' style='border: 1px solid black; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                        str = str + "<tr><th style='border: 1px solid black;'>FIELDNAME</th><th style='border: 1px solid black;'>OLDVALUE</th><th style='border: 1px solid black;'>NEWVALUE</th><th style='border: 1px solid black;'>REASON</th><th style='border: 1px solid black;'>COMMENTS</th><th style='border: 1px solid black;'>CHANGE BY</th><th style='border: 1px solid black;'>CHANGE DATE</th></tr>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            str = str + "<tr style='text-align: left;'>";
                            str = str + "<td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["FIELDNAME"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["OLDVALUE"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["NEWVALUE"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["REASON"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["COMMENTS"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["CHANGEBY"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["CHANGEDAT"].ToString() + "</td>";
                            str = str + "</tr>";
                        }
                        str = str + "</tbody></table>";
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

        //get comments of particular field
        [System.Web.Services.WebMethod]
        public static string CTMS_GetFielsComments(string VariableName, string ContId, string CTMS_SVID, string CTMS_RECID)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                string SVID = CTMS_SVID;
                int RECID = Convert.ToInt32(CTMS_RECID);
                string TimeZone = GET_TIMEZONE();

                ds = dal.CTMS_GetFieldComments(Action: "GET_DATA", SVID: SVID, CONTID: Convert.ToInt16(ContId), VARIABLENAME: VariableName, RECID: RECID, TIMEZONE: TimeZone);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var str = "<table id ='CTMS_grdfieldComments' font-family='Arial' style='border: 1px solid black; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                        str = str + "<tr><th style='border: 1px solid black;'>FIELDNAME</th><th style='border: 1px solid black;'>COMMENTS</th><th style='border: 1px solid black;'>ENTERED BY</th><th style='border: 1px solid black;'>ENTERED DATE</th></tr>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            str = str + "<tr style='text-align: left;'>";
                            str = str + "<td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["VARIABLENAME"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["COMMENTS"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["ENTEREDBY"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["ENTEREDDAT"].ToString() + "</td>";
                            str = str + "</tr>";
                        }
                        str = str + "</tbody></table>";
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

        //Method for update data in table and audit trial
        [System.Web.Services.WebMethod]
        public static string CTMS_UpdateData(string ContId, string ModuleName, string FieldName, string TableName, string VariableName,
            string OldValue, string NewValue, string Reason, string Comments, string Query, string ControlType, string CTMS_SVID,
            string CTMS_RECID)
        {
            SqlCommand cmd;
            SqlConnection con = new SqlConnection();
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(HttpContext.Current.Session["CHILD_CONN"].ToString());
            }
            else
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
            }
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(getconstrCHILD());
            }
            try
            {

                string SVID = CTMS_SVID;
                string UserId = GetUserId();
                string SubjID = "";
                string RECID = CTMS_RECID;


                cmd = new SqlCommand("CTMS_CHANGE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@SVID", SVID);
                cmd.Parameters.AddWithValue("@CONTID", Convert.ToInt32(ContId));
                cmd.Parameters.AddWithValue("@RECID", Convert.ToInt32(RECID));
                cmd.Parameters.AddWithValue("@MODULENAME", ModuleName);
                cmd.Parameters.AddWithValue("@FIELDNAME", FieldName);
                cmd.Parameters.AddWithValue("@TABLENAME", TableName);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VariableName);
                cmd.Parameters.AddWithValue("@OLDVALUE", OldValue);
                cmd.Parameters.AddWithValue("@NEWVALUE", NewValue);
                cmd.Parameters.AddWithValue("@REASON", Reason);
                cmd.Parameters.AddWithValue("@COMMENTS", Comments);
                cmd.Parameters.AddWithValue("@CHANGEBY", UserId);
                cmd.Parameters.AddWithValue("@ControlType", ControlType);
                cmd.Parameters.AddWithValue("@Source", "eCRF");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            finally
            {
                cmd = null;
            }
            return "Record Updated successfully.";
        }

        //get comments of particular field
        [System.Web.Services.WebMethod]
        public static string CTMS_SetFielsComments(string VariableName, string ContId, string TableName, string Comments, string FieldName,
            string CTMS_SVID, string CTMS_RECID)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                string SVID = CTMS_SVID;
                string USERID = GetUserId();
                int RECID = Convert.ToInt32(CTMS_RECID);

                ds = dal.CTMS_GetFieldComments(Action: "INSERT", SVID: SVID, CONTID: Convert.ToInt16(ContId), RECID: RECID,
                VARIABLENAME: VariableName, TABLENAME: TableName, FIELDNAME: FieldName, COMMENTS: Comments, ENTEREDBY: USERID);

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Record Updated successfully.";
        }

        //Method for update data in table and audit trial    
        [System.Web.Services.WebMethod]
        public static string SAE_UNLOCK(string SAEID, string SUBJID, string INVID, string ID)
        {
            SqlCommand cmd;
            SqlConnection con = new SqlConnection();
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(HttpContext.Current.Session["CHILD_CONN"].ToString());
            }
            else
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
            }
            if (HttpContext.Current.Session["CHILD_CONN"] != null)
            {
                con = new SqlConnection(getconstrCHILD());
            }
            try
            {
                string UserId = GetUserId();

                DAL dal = new DAL();

                dal.SAE_ADD_UPDATE_NEW(ACTION: "SAE_UNLOCK",
                SAEID: SAEID,
                SUBJECTID: SUBJID,
                INVID: INVID,
                ID: ID
                );
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            finally
            {
                cmd = null;
            }
            return "Record Updated successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string SetEmailYes(string ID)
        {
            string SDVSTATUS = "";
            try
            {
                CommonFunction ComFun = new CommonFunction();
                DAL_eTMF dal_eTMF = new DAL_eTMF();

                dal_eTMF.eTMF_SET_SP(ACTION: "SetEmailYes", ID: ID);


            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string SetEmailNo(string ID)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                dal_eTMF.eTMF_SET_SP(ACTION: "SetEmailNo", ID: ID);
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string SetVerSPECYes(string ID)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                CommonFunction ComFun = new CommonFunction();

                dal_eTMF.eTMF_SET_SP(ACTION: "SetVerSPECYes", ID: ID);


            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string SetVerSPECNo(string ID)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                CommonFunction ComFun = new CommonFunction();

                dal_eTMF.eTMF_SET_SP(ACTION: "SetVerSPECNo", ID: ID);

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string SetDocInstruction(string ID, string Comments)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();

                dal_eTMF.eTMF_SET_SP(ACTION: "SetDocInstruction", ID: ID, Comment: Comments);

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
        public static string GetDoc_Instruction(string ID)
        {
            string INFO = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                DataSet ds = new DataSet();

                ds = dal_eTMF.eTMF_SET_SP(ACTION: "GetDoc_Instruction", ID: ID);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    INFO = ds.Tables[0].Rows[0]["Info"].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return INFO;
        }

        [System.Web.Services.WebMethod]
        public static string SetExpDoc_Comment(string ID, string Comments)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                CommonFunction ComFun = new CommonFunction();
                dal_eTMF.eTMF_SET_SP(
                    ACTION: "SetExpDoc_Comment",
                    ID: ID,
                    Comment: Comments
                    );



            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string UPDATE_DOC_NAME(string ID, string DOCNAME)
        {
            string SDVSTATUS = "";
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();

                CommonFunction ComFun = new CommonFunction();

                DataSet ds = dal_eTMF.eTMF_SET_SP
                   (
                    ACTION: "UPDATE_DOC_NAME",
                    ID: ID,
                    DocName: DOCNAME
                    );


            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string Delete_Doc(string ID, string DOCNAME)
        {
            string SDVSTATUS = "";
            try
            {
                CommonFunction ComFun = new CommonFunction();
                DAL_eTMF dal_eTMF = new DAL_eTMF();




                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "Delete_Doc", ID: ID, DocName: DOCNAME);
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        //getaAudit trail of particular field
        [System.Web.Services.WebMethod]
        public static string CTMS_GetAuditTrail_ALL(string SVID, string RECID, string TABLENAME)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                string TIMEZONE = GET_TIMEZONE();

                ds = dal.CTMS_GetAUDITTRAILDETAILS(Action: "GET_DATA_ALL_SDV", SVID: SVID, RECID: Convert.ToInt32(RECID), TIMEZONE: TIMEZONE, TABLENAME: TABLENAME);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var str = "<table id ='grdAuditTrail' font-family='Arial' style='border: 1px solid black; color:#333333;font-size:X-Small;border-collapse:collapse;text-align: center' cellpadding='4' cellspacing='0'><tbody>";
                        str = str + "<tr><th style='border: 1px solid black;'>FIELDNAME</th><th style='border: 1px solid black;'>OLDVALUE</th><th style='border: 1px solid black;'>NEWVALUE</th><th style='border: 1px solid black;'>REASON</th><th style='border: 1px solid black;'>COMMENTS</th><th style='border: 1px solid black;'>CHANGE BY</th><th style='border: 1px solid black;'>CHANGE DATE</th></tr>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            str = str + "<tr style='text-align: left;'>";
                            str = str + "<td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["FIELDNAME"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["OLDVALUE"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["NEWVALUE"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["REASON"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["COMMENTS"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["CHANGEBY"].ToString() + "</td><td style='border: 1px solid black;'>" + ds.Tables[0].Rows[i]["CHANGEDAT"].ToString() + "</td>";
                            str = str + "</tr>";
                        }
                        str = str + "</tbody></table>";
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

        //get comments of particular field

        [System.Web.Services.WebMethod]
        public static string SAE_GetOverrideComments(string Id)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds = new DataSet();
                string str = "";

                ds = dal.SAE_GetRecordsQueryDetails(Action: "SAE_GET_OVERRIDE_QUERY_COMMENT", ID: Convert.ToInt32(Id));

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
        public static string SAE_ADD_PAGE_COMMENTS(string SAEID, string RECID, string COMMENTS, string STATUS,
           string MODULEID)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                ds = dal.SAE_GetFieldComments(
                        Action: "SAE_ADD_Page_COMMENTS",
                        SAEID: SAEID,
                        Comments: COMMENTS,
                        MODULEID: MODULEID,
                        STATUS: STATUS
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
        public static string SAE_DATASYNC(string SAEID, string RECID, string SAE, string TableName, string STATUS, string SUBJID,
            string SITEID, string MODULEID, string PROJECTID, string USERID, string AEPVID, string AERECID, string SYNC_UPDATE)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                ds = dal.SAE_ADD_UPDATE(
                        ACTION: "SAE_MAX_REC_ID",
                        SAEID: SAEID,
                        TABLENAME: TableName
                        );


                if (SYNC_UPDATE == "1")
                {
                    dal.SAE_ADD_UPDATE(ACTION: "UPDATE_MULTIPLE_PAGE_SYNC_DATA",
                    SAEID: SAEID,
                    SAE: SAE,
                    STATUS: STATUS,
                    SUBJECTID: SUBJID,
                    INVID: SITEID,
                    MODULEID: MODULEID,
                    PROJECTID: PROJECTID,
                    TABLENAME: TableName,
                    RECID: RECID,
                    ENTEREDBY: USERID,
                    DM_PVID: AEPVID,
                    DM_RECID: AERECID
                    );


                    dal.SAE_ADD_UPDATE(ACTION: "INSERT_DM_AUDIT_TO_SAE_UDATED_DATA",
                    SAEID: SAEID,
                    SAE: SAE,
                    STATUS: STATUS,
                    SUBJECTID: SUBJID,
                    INVID: SITEID,
                    MODULEID: MODULEID,
                    PROJECTID: PROJECTID,
                    TABLENAME: TableName,
                    RECID: RECID,
                    ENTEREDBY: USERID,
                    DM_PVID: AEPVID,
                    DM_RECID: AERECID
                    );
                }
                else
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            RECID = (Convert.ToInt32(ds.Tables[0].Rows[0]["RECID"]) + 1).ToString();
                        }
                        else
                        {
                            RECID = "0";
                        }
                    }
                    else
                    {
                        RECID = "0";
                    }

                    dal.SAE_ADD_UPDATE(ACTION: "INSERT_MULTIPLE_PAGE_SYNC_DATA",
                    SAEID: SAEID,
                    SAE: SAE,
                    STATUS: STATUS,
                    SUBJECTID: SUBJID,
                    INVID: SITEID,
                    MODULEID: MODULEID,
                    PROJECTID: PROJECTID,
                    TABLENAME: TableName,
                    RECID: RECID,
                    ENTEREDBY: USERID,
                    DM_PVID: AEPVID,
                    DM_RECID: AERECID
                    );

                    dal.SAE_ADD_UPDATE(ACTION: "INSERT_DM_AUDIT_TO_SAE",
                    SAEID: SAEID,
                    SAE: SAE,
                    STATUS: STATUS,
                    SUBJECTID: SUBJID,
                    INVID: SITEID,
                    MODULEID: MODULEID,
                    PROJECTID: PROJECTID,
                    TABLENAME: TableName,
                    RECID: RECID,
                    ENTEREDBY: USERID,
                    DM_PVID: AEPVID,
                    DM_RECID: AERECID
                    );
                }

                dal.SAE_ADD_UPDATE(ACTION: "INSERT_SAE_LIST",
                SAEID: SAEID,
                INVID: SITEID,
                SUBJECTID: SUBJID,
                PROJECTID: PROJECTID,
                STATUS: STATUS,
                SAE: SAE,
                ENTEREDBY: USERID
                );

                dal.SAE_ADD_UPDATE(ACTION: "INSERT_SAE_LOG_STATUS_DATA",
                SAEID: SAEID,
                INVID: SITEID,
                SUBJECTID: SUBJID,
                PROJECTID: PROJECTID,
                STATUS: STATUS,
                SAE: SAE,
                ENTEREDBY: USERID,
                MODULEID: MODULEID,
                IsComplete: true,
                IsMissing: false,
                TABLENAME: TableName
                );

                if (TableName == "SAE_DATA_LI" || TableName == "SAE_DATA_AEDES" || TableName == "SAE_DATA_NPT")
                {
                    DataSet ds1 = dal.SAE_ADD_UPDATE(ACTION: "GET_SYNC_PVID", SAEID: SAEID, TABLENAME: TableName, RECID: RECID);

                    dal.SAE_ADD_UPDATE(ACTION: "UPDATE_DM_DM_SAENO",
                    SAEID: SAEID,
                    TABLENAME: TableName,
                    DM_PVID: ds1.Tables[0].Rows[0]["SYNC_PVID"].ToString(),
                    DM_RECID: ds1.Tables[0].Rows[0]["SYNC_RECID"].ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return "Record Updated successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string SAE_ADD_MR_PAGE_COMMENTS(string SAEID, string RECID, string COMMENTS, string STATUS,
            string MODULEID)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                ds = dal.SAE_GetFieldComments(
                        Action: "SAE_ADD_MR_PAGE_COMMENTS",
                        SAEID: SAEID,
                        Comments: COMMENTS,
                        MODULEID: MODULEID,
                        STATUS: STATUS
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
        public static string SAE_PATIENT_SDV(string TABLENAME, string SAEID, string RECID, string MODULEID, string SUBJID)
        {
            string SDVSTATUS = "";
            try
            {
                DAL dal = new DAL();

                DataSet ds;
                ds = new DataSet();

                string PROJECTID = GetProjectId();
                string USERID = GetUserId();

                DataSet ds1 = dal.SAE_ADD_UPDATE(
                    ACTION: "Update_SDV_TO_SAE_LIST",
                        TABLENAME: TABLENAME,
                         SAEID: SAEID,
                         RECID: RECID,
                         MODULEID: MODULEID,
                         SUBJECTID: SUBJID,
                         USERID: USERID
                        );

                SDVSTATUS = ds1.Tables[0].Rows[0]["SDVSTATUS"].ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        [System.Web.Services.WebMethod]
        public static string SAE_PATIENT_SDV_UNDOE(string TABLENAME, string SAEID, string RECID, string MODULEID, string SUBJID)
        {
            string SDVSTATUS = "";
            try
            {
                DAL dal = new DAL();

                DataSet ds;
                ds = new DataSet();

                string PROJECTID = GetProjectId();
                string USERID = GetUserId();

                DataSet ds1 = dal.SAE_ADD_UPDATE(
                    ACTION: "Update_UNDONE_SDV_TO_SAE_LIST",
                    TABLENAME: TABLENAME,
                     SAEID: SAEID,
                     RECID: RECID,
                     MODULEID: MODULEID,
                     SUBJECTID: SUBJID,
                     USERID: USERID
                        );

                SDVSTATUS = ds1.Tables[0].Rows[0]["SDVSTATUS"].ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return SDVSTATUS;
        }

        //create manual query 
        [System.Web.Services.WebMethod]
        public static string SAE_SetManualQuery(string ContId, string QID, string QueryText, string ModuleName, string FieldName,
            string TableName, string VariableName, string SAEID, string RECID, string SUBJID, string STATUS, string MODULEID)
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                string USERID = GetUserId();

                ds = dal.SAE_SetManualQuery(Action: "INSERT",
                SAEID: SAEID,
                CONTID: Convert.ToInt16(ContId),
                RECID: Convert.ToInt32(RECID),
                QID: QID,
                SUBJID: SUBJID,
                QUERYTEXT: QueryText,
                MODULENAME: ModuleName,
                FIELDNAME: FieldName,
                TABLENAME: TableName,
                VARIABLENAME: VariableName,
                ENTEREDBY: USERID,
                SAE_STATUS: STATUS,
                MODULEID: MODULEID
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
        public static string MarkAsReview_DELETE_AUDIT(string ID, string PVID, string RECID)
        {
            string STATUS = "";
            try
            {
                DAL dal = new DAL();

                //dal.GetAUDITTRAILDETAILS(Action: "MarkAsReview_DELETE_AUDIT", ID: ID, PVID: PVID, RECID: Convert.ToInt32(RECID));
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
        public static string UnMarkAsReviewed_DELETE_AUDIT(string ID, string PVID, string RECID)
        {
            string STATUS = "";
            try
            {
                DAL dal = new DAL();

                //dal.GetAUDITTRAILDETAILS(Action: "UnMarkAsReviewed_DELETE_AUDIT", ID: ID, PVID: PVID, RECID: Convert.ToInt32(RECID));
                STATUS = "False";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return STATUS;
        }

        // MM_Query_Deleted Method//
        [System.Web.Services.WebMethod]
        public static string MM_ADD_DELETED_QUERY_COMMENT(string ID, string Comment)
        {
            DAL dal = new DAL();
            DataSet ds = new DataSet();
            string msg = "";
            string UserId = GetUserId();
            try
            {
                //dal.DM_QUERY_SP(
                //    ACTION: "DELETE_QUERY_REVIEW",
                //    ID: ID,
                //    Comment: Comment
                //    );
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return "Record Updated successfully.";
        }

        [System.Web.Services.WebMethod]
        public static string GET_LAB_DATA(string SUBJID, string LABID, string LBTEST, string VISITID, string MODULEID)
        {
            //string VISITID
            DAL dal = new DAL();
            DataSet ds;
            ds = new DataSet();
            try
            {
                ds = dal.DM_LAB_SP(
                        ACTION: "GET_LAB_DATA",
                        SUBJID: SUBJID,
                        LABID: LABID,
                        LBTEST: LBTEST,
                        VISITID: VISITID,
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

        //[System.Web.Services.WebMethod]
        //public static string GET_MM_ALL_COMMENT(string ID)
        //{
        //    try
        //    {
        //        DAL dal = new DAL();
        //        DataSet ds = new DataSet();
        //        string str = "";


        //        ds = dal.GetQueryReports(ACTION: "QueryReport_COMMENTS", ID: ID);

        //        if (ds != null)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                str = ConvertDataTableToHTML(ds);
        //                return str;
        //            }
        //        }

        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //        throw;
        //    }
        //}
        //End Used Methods

        [System.Web.Services.WebMethod]
        public static string UPDATE_UniqueRefNo(string ID, string UniqueRefNo)
        {
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                DataSet ds;
                ds = new DataSet();
                ds = dal_eTMF.eTMF_SET_SP(
                        ACTION: "Update_BD_DOC_RefNo",
                        ID: ID,
                        UniqueRefNo: UniqueRefNo
                        );
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }

        }

        [System.Web.Services.WebMethod]
        public static string UPDATE_RefNo(string ID, string RefNo)
        {
            try
            {
                DAL_eTMF dal_eTMF = new DAL_eTMF();
                DataSet ds;
                ds = new DataSet();
                ds = dal_eTMF.eTMF_SET_SP(
                        ACTION: "UPDATE_RefNo",
                        ID: ID,
                        RefNo: RefNo
                        );
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }

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
        public static List<LinkedOptions> GetLinkedOptions(string VariableName, string ParentANS, string ParentVARIABLENAME)
        {
            DAL_DB dal_DB = new DAL_DB();

            List<LinkedOptions> lst = new List<LinkedOptions>();

            DataSet ds = dal_DB.DB_DRP_SP(ACTION: "DM_DM_DRP_DWN_MASTER_LINKED",
                VARIABLENAME: VariableName,
                ParentANS: ParentANS
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