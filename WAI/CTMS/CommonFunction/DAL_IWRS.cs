using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPT
{
    public class DAL_IWRS
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
        SqlDataAdapter adp;


        CTMS.IWRS.WAI_IWRS IWRS = new CTMS.IWRS.WAI_IWRS();

        public DataSet IWRS_STRATA_SP(string ACTION = null, string STRATA = null, string VARIABLENAME = null, string ANSWER = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_STRATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@STRATA", STRATA);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ANSWER", ANSWER);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_AUDITTRAIL_SP(string ID = null, string ACTION = null, string SITEID = null, string SUBJECT_ID = null, string USERID = null, string DCFID = null, string FROMDATE = null, string TODATE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_AUDITTRAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJECT_ID", SUBJECT_ID);
                cmd.Parameters.AddWithValue("@DCFID", DCFID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE);
                cmd.Parameters.AddWithValue("@TODATE", TODATE);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet NIWRS_SETUP_SP(
            string ACTION = null, string AND_OR = null, string AndOr1 = null, string AndOr2 = null, string AndOr3 = null, string AndOr4 = null,
            string ANSWER = null, string CONDITION1 = null, string Condition2 = null, string Condition3 = null, string Condition4 = null, string Condition5 = null,
            string CritCode = null, string Criteria = null, string CRITERIA_ID = null, string CritName = null, string DESCRIPTION = null, string DTENTERED = null,
            string ENTERED_DT = null, string ENTERED_USER = null, string ENTEREDBY = null, string ERR_MSG = null, string Field1 = null,
            string Field2 = null, string Field3 = null, string Field4 = null, string Field5 = null, string FIELDID = null, string FIELDNAME = null,
            string FormCode = null, string FormHeader = null, string FormName = null, string ID = null, string INDICATION = null, string INDICATION_ID = null,
            bool IsApplicable = false, string MODULEID = null, string MODULENAME = null, string PageName = null, string STRATA = null, string TABLENAME = null,
            string UPDATED_DT = null, string UPDATED_USER = null, string Value1 = null, string VALUE2 = null, string Value3 = null, string Value4 = null,
            string Value5 = null, string VARIABLENAME = null, string VISIT = null, string VISITNUM = null, string VISIT_ID = null,
            string STATUSCODE = null, string STATUSNAME = null, string LISTNAME = null, string LISTID = null, string SEQNO = null,
            string COL_NAME = null, string SITEID = null, string SubSiteID = null, string SubSiteName = null,
            string WINDOW = null, string EARLY = null, string LATE = null, string LAST_VISIT = null,
            string LAST_VISIT_DATE = null, string NEXT_VISIT = null, string NEXT_VISIT_DATE = null,
            string EARLY_DATE = null, string LATE_DATE = null, bool IWRS_Graph = false, bool IWRS_Tile = false, bool DOSING = false,
            string EMAIL_IDS = null, string CCEMAIL_IDS = null, string BCCEMAIL_IDS = null
            , string QUESTION = null, string QUECODE = null, string ANS = null, string Field6 = null, string Condition6 = null, string Value6 = null, string AndOr5 = null, string VisitSummarySeq = null, bool Applicable_For_VisitSummary = false, string TREAT_GRP = null, string TREAT_GRP_NAME = null, string METHOD = null, string FORMULA = null, string DEPEND = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("NIWRS_SETUP_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@AND_OR", AND_OR);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@ANSWER", ANSWER);
                cmd.Parameters.AddWithValue("@COL_NAME", COL_NAME);
                cmd.Parameters.AddWithValue("@CONDITION1", CONDITION1);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@CRITERIA_ID", CRITERIA_ID);
                cmd.Parameters.AddWithValue("@CritName", CritName);
                cmd.Parameters.AddWithValue("@DESCRIPTION", DESCRIPTION);
                cmd.Parameters.AddWithValue("@DTENTERED", DTENTERED);
                cmd.Parameters.AddWithValue("@ENTERED_DT", ENTERED_DT);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@ERR_MSG", ERR_MSG);
                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Field5", Field5);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@FormCode", FormCode);
                cmd.Parameters.AddWithValue("@FormHeader", FormHeader);
                cmd.Parameters.AddWithValue("@FormName", FormName);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@INDICATION_ID", INDICATION_ID);
                cmd.Parameters.AddWithValue("@IsApplicable", IsApplicable);
                cmd.Parameters.AddWithValue("@LISTNAME", LISTNAME);
                cmd.Parameters.AddWithValue("@LISTID", LISTID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@PageName", PageName);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@STATUSNAME", STATUSNAME);
                cmd.Parameters.AddWithValue("@STATUSCODE", STATUSCODE);
                cmd.Parameters.AddWithValue("@STRATA", STRATA);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SubSiteID", SubSiteID);
                cmd.Parameters.AddWithValue("@SubSiteName", SubSiteName);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@UPDATED_DT", UPDATED_DT);
                cmd.Parameters.AddWithValue("@UPDATED_USER", UPDATED_USER);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@VALUE2", VALUE2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT_ID", VISIT_ID);

                cmd.Parameters.AddWithValue("@WINDOW", WINDOW);
                cmd.Parameters.AddWithValue("@EARLY", EARLY);
                cmd.Parameters.AddWithValue("@LATE", LATE);
                cmd.Parameters.AddWithValue("@LAST_VISIT", LAST_VISIT);
                cmd.Parameters.AddWithValue("@LAST_VISIT_DATE", LAST_VISIT_DATE);
                cmd.Parameters.AddWithValue("@NEXT_VISIT", NEXT_VISIT);
                cmd.Parameters.AddWithValue("@NEXT_VISIT_DATE", NEXT_VISIT_DATE);
                cmd.Parameters.AddWithValue("@EARLY_DATE", EARLY_DATE);
                cmd.Parameters.AddWithValue("@LATE_DATE", LATE_DATE);

                cmd.Parameters.AddWithValue("@IWRS_Graph", IWRS_Graph);
                cmd.Parameters.AddWithValue("@IWRS_Tile", IWRS_Tile);
                cmd.Parameters.AddWithValue("@DOSING", DOSING);

                cmd.Parameters.AddWithValue("@EMAIL_IDS", EMAIL_IDS);
                cmd.Parameters.AddWithValue("@CCEMAIL_IDS", CCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@BCCEMAIL_IDS", BCCEMAIL_IDS);

                cmd.Parameters.AddWithValue("@QUESTION", QUESTION);
                cmd.Parameters.AddWithValue("@QUECODE", QUECODE);
                cmd.Parameters.AddWithValue("@ANS", ANS);

                cmd.Parameters.AddWithValue("@Field6", Field6);
                cmd.Parameters.AddWithValue("@Condition6", Condition6);
                cmd.Parameters.AddWithValue("@Value6", Value6);
                cmd.Parameters.AddWithValue("@AndOr5", AndOr5);

                cmd.Parameters.AddWithValue("@VisitSummarySeq", VisitSummarySeq);
                cmd.Parameters.AddWithValue("@Applicable_For_VisitSummary", Applicable_For_VisitSummary);


                cmd.Parameters.AddWithValue("@TREAT_GRP", TREAT_GRP);
                cmd.Parameters.AddWithValue("@TREAT_GRP_NAME", TREAT_GRP_NAME);
                cmd.Parameters.AddWithValue("@METHOD", METHOD);
                cmd.Parameters.AddWithValue("@FORMULA", FORMULA);
                cmd.Parameters.AddWithValue("@DEPEND", DEPEND);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return ds;
        }

        public DataSet IWRS_SET_VALUE_SP(string ACTION = null, string VARIABLENAME = null, string MODULEID = null, string CRITCODE = null, string FORMULA = null, string FORMID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_SET_VALUE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@CRITCODE", CRITCODE);
                cmd.Parameters.AddWithValue("@FORMULA", FORMULA);
                cmd.Parameters.AddWithValue("@FORMID", FORMID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet NIWRS_DASHOARD_SP(string ACTION = null, string STATUSCODE = null, string COL_NAME = null, string ENTEREDBY = null,
            string ID = null, string SITEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd = new SqlCommand("NIWRS_DASHOARD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@COL_NAME", COL_NAME);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@STATUSCODE", STATUSCODE);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return ds;
        }

        public DataSet IWRS_INSERT_DATA_SP(string ACTION = null, string TABLENAME = null, string SUBJID = null, string INSERTQUERY = null, string UPDATEQUERY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {

                ds = IWRS.IWRS_INSERT_DATA_SP(ACTION: ACTION,
                    TABLENAME: TABLENAME,
                    SUBJID: SUBJID,
                    INSERTQUERY: INSERTQUERY,
                    UPDATEQUERY: UPDATEQUERY
                    );


            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet NIWRS_WORKFLOW_SP
            (
            string ACTION = null, string AndOr1 = null, string AndOr2 = null, string AndOr3 = null, string AndOr4 = null,
            string Condition1 = null, string Condition2 = null, string Condition3 = null, string Condition4 = null, string Condition5 = null,
            string CritCode = null, string Criteria = null, string CritName = null, string EVENTHIST = null, string Field1 = null,
            string Field2 = null, string Field3 = null, string Field4 = null, string Field5 = null, string FIELDNAME = null, string HEADER = null,
            string ID = null, string INDICATION = null, string INDICATION_ID = null, string MSGBOX = null, bool NAVMENU = false, string NAVTO = null,
            string NAVTO_TYPE = null, string PERFORM = null, string SETFIELD = null, string SOURCE_ID = null, string SOURCE_TYPE = null, string STEP_CRIT_ID = null,
            string STEPID = null, string VALUE = null, string ApplVisit = null, string AutoPopList = null, string Value1 = null, string Value2 = null, string Value3 = null,
            string Value4 = null, string Value5 = null, string SEQNO = null, string PROJECTID = null, string NAV_PARENT = null,
            bool SEND_EMAIL = false, string EMAIL_IDS = null, string CCEMAIL_IDS = null, string BCCEMAIL_IDS = null, string EMAIL_SUBJECT = null, string EMAIL_BODY = null,
            string SITEID = null, bool DOWNLOAD = false, string NextVisit = null, string RANDARM = null, string DOSEARM = null, string Quantity = null, string FIELDID = null, string ENTEREDBY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("NIWRS_WORKFLOW_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@Condition1", Condition1);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@CritName", CritName);
                cmd.Parameters.AddWithValue("@EVENTHIST", EVENTHIST);
                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Field5", Field5);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@HEADER", HEADER);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@INDICATION_ID", INDICATION_ID);
                //cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);
                cmd.Parameters.AddWithValue("@MSGBOX", MSGBOX);
                cmd.Parameters.AddWithValue("@NAVMENU", NAVMENU);
                cmd.Parameters.AddWithValue("@NAV_PARENT", NAV_PARENT);
                cmd.Parameters.AddWithValue("@NAVTO", NAVTO);
                cmd.Parameters.AddWithValue("@NAVTO_TYPE", NAVTO_TYPE);
                cmd.Parameters.AddWithValue("@PERFORM", PERFORM);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@SETFIELD", SETFIELD);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@SOURCE_ID", SOURCE_ID);
                cmd.Parameters.AddWithValue("@SOURCE_TYPE", SOURCE_TYPE);
                cmd.Parameters.AddWithValue("@STEP_CRIT_ID", STEP_CRIT_ID);
                cmd.Parameters.AddWithValue("@STEPID", STEPID);
                cmd.Parameters.AddWithValue("@VALUE", VALUE);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@ApplVisit", ApplVisit);
                cmd.Parameters.AddWithValue("@AutoPopList", AutoPopList);
                cmd.Parameters.AddWithValue("@NextVisit", NextVisit);
                cmd.Parameters.AddWithValue("@SEND_EMAIL", SEND_EMAIL);
                cmd.Parameters.AddWithValue("@EMAIL_IDS", EMAIL_IDS);
                cmd.Parameters.AddWithValue("@CCEMAIL_IDS", CCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@BCCEMAIL_IDS", BCCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@EMAIL_SUBJECT", EMAIL_SUBJECT);
                cmd.Parameters.AddWithValue("@EMAIL_BODY", EMAIL_BODY);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@DOWNLOAD", DOWNLOAD);
                cmd.Parameters.AddWithValue("@RANDARM", RANDARM);
                cmd.Parameters.AddWithValue("@DOSEARM", DOSEARM);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return ds;
        }


        public DataTable NIWRS_UPLOAD_SP(string ACTION = null, string COLUMNNAME = null, string DATA = null, string INSERTQUERY = null, string WHERE = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd;
            SqlDataAdapter adp;
            string query = "";
            try
            {
                cmd = new SqlCommand("NIWRS_UPLOAD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@COLUMNNAME", COLUMNNAME);
                cmd.Parameters.AddWithValue("@DATA", DATA);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@WHERE", WHERE);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return dt;
        }


        public DataSet NIWRS_STOP_CLAUSE_SP(string ACTION = null, string AndOr1 = null, string AndOr2 = null, string AndOr3 = null, string AndOr4 = null,
string COLNAME = null, string Condition1 = null, string Condition2 = null, string Condition3 = null, string Condition4 = null, string Condition5 = null,
    string CritCode = null, string Criteria = null, string CritName = null, string Field1 = null, string Field2 = null, string Field3 = null, string Field4 = null,
    string Field5 = null, string FIELDID = null, string FIELDNAME = null, string ID = null, string LIMIT = null, string MODULEID = null, string MODULENAME = null, string MSGBOX = null,
    string SEQNO = null, string TABLENAME = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string VARIABLENAME = null,
    bool BEFORE = false, bool AFTER = false, string SUBJID = null, string NAVTO = null, string NAVTO_TYPE = null, string SITEID = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                cmd = new SqlCommand("NIWRS_STOP_CLAUSE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@COLNAME", COLNAME);
                cmd.Parameters.AddWithValue("@Condition1", Condition1);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@CritName", CritName);
                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Field5", Field5);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LIMIT", LIMIT);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@MSGBOX", MSGBOX);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@BEFORE", BEFORE);
                cmd.Parameters.AddWithValue("@AFTER", AFTER);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@NAVTO", NAVTO);
                cmd.Parameters.AddWithValue("@NAVTO_TYPE", NAVTO_TYPE);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }


        public DataSet IWRS_UNBLIND_SP(string SUBJID = null, string ENTEREDBY = null, string FIELDNAME = null, string CONDITION1 = null, string SITEID = null,
            string COUNTRYID = null, string SUBSITEID = null, string STATUSNAME = null, string UNBLINDDAT = null, string UNBLINDTIM = null, string TREAT_GRP = null, string ACTION = null, string TREAT_GRP_NAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string User_Name = null, TZ_VAL = null;

                cmd = new SqlCommand("IWRS_UNBLIND_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@CONDITION1", CONDITION1);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@STATUSNAME", STATUSNAME);
                cmd.Parameters.AddWithValue("@UNBLINDDAT", UNBLINDDAT);
                cmd.Parameters.AddWithValue("@UNBLINDTIM", UNBLINDTIM);
                cmd.Parameters.AddWithValue("@TREAT_GRP", TREAT_GRP);
                cmd.Parameters.AddWithValue("@TREAT_GRP_NAME", TREAT_GRP_NAME);

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                cmd.Parameters.AddWithValue("@User_Name", User_Name);

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_SET_DB_SP(string CRITERIA_ID = null, string COL_NAME = null, string ENTEREDBY = null, string ID = null,
           string FormHeader = null, string TABLENAME = null, string SITEID = null, string VARIABLENAME = null,
           string FIELDNAME = null, string CONDITION1 = null, string STATUSCODE = null, string MODULENAME = null,
           string SEQNO = null, string Criteria = null, string FormName = null,
           string ACTION = null, string User_ID = null, string Project_ID = null, string Section = null,
            bool IWRS_Graph = false, bool IWRS_Tile = false, bool DOSING = false)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_SET_DB_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CRITERIA_ID", CRITERIA_ID);
                cmd.Parameters.AddWithValue("@COL_NAME", COL_NAME);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FormHeader", FormHeader);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@CONDITION1", CONDITION1);
                cmd.Parameters.AddWithValue("@STATUSCODE", STATUSCODE);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@IWRS_Graph", IWRS_Graph);
                cmd.Parameters.AddWithValue("@FormName", FormName);
                cmd.Parameters.AddWithValue("@DOSING", DOSING);
                cmd.Parameters.AddWithValue("@IWRS_Tile", IWRS_Tile);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }
        public DataSet IWRS_GET_SUBJECT_DETAILS_SP(string ACTION = null, string SUBJID = null, string FORMID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_GET_SUBJECT_DETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@FORMID", FORMID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_STRUCTURE_SP(string ACTION = null, string ID = null, string FIELDID = null, string FormCode = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;

            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_STRUCTURE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@FormCode", FormCode);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_AVL_SP(string TREAT_STRENGTH = null, string TREAT_GRP = null, string QUANTITY = null, string STRATA = null,
            string CurrentDate = null, string SUBJID = null, string SITEID = null, string SUBSITEID = null, string RANDNO = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_AVL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TREAT_STRENGTH", TREAT_STRENGTH);
                cmd.Parameters.AddWithValue("@TREAT_GRP", TREAT_GRP);
                cmd.Parameters.AddWithValue("@QUANTITY", QUANTITY);
                cmd.Parameters.AddWithValue("@STRATA", STRATA);
                cmd.Parameters.AddWithValue("@CurrentDate", CurrentDate);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@RANDNO", RANDNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_ACTIONS_SP(string ID = null, string NAV_TYPE = null, string ACTION = null, string INSERTQUERY = null, string NAV_ID = null,
            string SUBJID = null, string LISTID = null, string SITEID = null, string STEPID = null, string WHERE = null, string FIELDID = null,
            string SUBSITEID = null, string RANDNO = null, string STRATA = null, string VISITNUM = null, string ENTERED_DT = null, string NEXT_VISIT = null,
            string WINDOW = null, string EARLY = null, string LATE = null, string MODULEID = null, string VARIABLENAME = null, string TABLENAME = null, string DATA = null, string NEXT_VISIT_DATE = null,
            string EARLY_DATE = null, string LATE_DATE = null, string CurrentDate = null, string INDICATION = null, string INDICATION_ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {

                ds = IWRS.IWRS_ACTIONS_SP(ID: ID,
                NAV_TYPE: NAV_TYPE,
                ACTION: ACTION,
                INSERTQUERY: INSERTQUERY,
                NAV_ID: NAV_ID,
                SUBJID: SUBJID,
                LISTID: LISTID,
                SITEID: SITEID,
                STEPID: STEPID,
                WHERE: WHERE,
                FIELDID: FIELDID,
                SUBSITEID: SUBSITEID,
                RANDNO: RANDNO,
                STRATA: STRATA,
                VISITNUM: VISITNUM,
                ENTERED_DT: ENTERED_DT,
                NEXT_VISIT: NEXT_VISIT,
                WINDOW: WINDOW,
                EARLY: EARLY,
                LATE: LATE,
                MODULEID: MODULEID,
                VARIABLENAME: VARIABLENAME,
                TABLENAME: TABLENAME,
                DATA: DATA,
                NEXT_VISIT_DATE: NEXT_VISIT_DATE,
                EARLY_DATE: EARLY_DATE,
                LATE_DATE: LATE_DATE,
                CurrentDate: CurrentDate,
                INDICATION : INDICATION,
                INDICATION_ID : INDICATION_ID);
            }
            catch (Exception)
            {
                throw;
                con.Close();
            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet IWRS_KITS_SP(string SITEID = null, string SUBSITEID = null, string RANDNO = null, string SUBJID = null, string VISITNUM = null, string ENTEREDBY = null, string VISIT = null, string OLD_KITNO = null, string NEW_KITNO = null, string REASON = null, string STEPID = null, string CurrentDate = null, string WHERE = null, string TREAT_STRENGTH = null,
            string STATUSNAME = null, string TREAT_GRP = null, string LAST_VISIT = null, string LAST_VISIT_DATE = null, string QUANTITY = null, string ACTION = null, string INDICATION_ID = null, string LOTNO = null, string KITNO = null, string EXPIRYDAT = null, string EXPIRY_COMMENT = null, string USERID = null, string User_Name = null , string TZ_VAL = null, string DISPENSE_IDX = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                ds = IWRS.IWRS_KITS_SP(SITEID: SITEID,
                SUBSITEID: SUBSITEID,
                RANDNO: RANDNO,
                SUBJID: SUBJID,
                VISITNUM: VISITNUM,
                ENTEREDBY: ENTEREDBY,
                VISIT: VISIT,
                OLD_KITNO: OLD_KITNO,
                NEW_KITNO: NEW_KITNO,
                REASON: REASON,
                STEPID: STEPID,
                CurrentDate: CurrentDate,
                WHERE: WHERE,
                TREAT_STRENGTH: TREAT_STRENGTH,
                STATUSNAME: STATUSNAME,
                TREAT_GRP: TREAT_GRP,
                LAST_VISIT: LAST_VISIT,
                LAST_VISIT_DATE: LAST_VISIT_DATE,
                QUANTITY: QUANTITY,
                ACTION: ACTION,
                INDICATION_ID: INDICATION_ID,
                LOTNO: LOTNO,
                KITNO: KITNO,
                EXPIRYDAT: EXPIRYDAT,
                EXPIRY_COMMENT: EXPIRY_COMMENT,
                USERID: USERID,
                User_Name : User_Name,
                TZ_VAL : TZ_VAL,
                DISPENSE_IDX: DISPENSE_IDX);
            }
            catch (Exception)
            {
                throw;
                con.Close();
            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet IWRS_LOG_SP(string ID = null, string SITEID = null, string SUBSITEID = null, string ERR_MSG = null, string SUBJID = null, string ENTEREDBY = null, string ACTION = null, string TABLENAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_LOG_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@ERR_MSG", ERR_MSG);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@UserName", User_Name);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_DATA_SP(string LISTID = null, string STEPID = null, string SUBJID = null, string WHERE = null, string TABLENAME = null, string MODULEID = null, string UPDATEQUERY = null,
            string INSERTQUERY = null, string STRATA = null, string VARIABLENAME = null, string ANSWER = null, string ACTION = null, string INDICATION_ID = null, string INDICATION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LISTID", LISTID);
                cmd.Parameters.AddWithValue("@STEPID", STEPID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@WHERE", WHERE);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@STRATA", STRATA);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ANSWER", ANSWER);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@INDICATION_ID", INDICATION_ID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_REPORTS_SP(string LISTID = null, string SUBJID = null, string SITEID = null, string WHERE = null, string VISIT = null,
            string ENTEREDBY = null, string SUBSITEID = null, string COUNTRYID = null, string ACTION = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string FROMDATE = null, string TODATE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_REPORTS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LISTID", LISTID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@WHERE", WHERE);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE);
                cmd.Parameters.AddWithValue("@TODATE", TODATE);


                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_DCF_SP(string ID = null, string FIELDNAME = null, string MODULENAME = null, string FormCode = null, string VISIT = null,
            string VISITNUM = null, string VARIABLENAME = null, string TABLENAME = null, string SUBJID = null, string SITEID = null, string RANDNO = null, string FIELDID = null, string Value2 = null, string SUBSITEID = null, string DESCRIPTION = null, string FormHeader = null,
            string Value1 = null, string ERR_MSG = null, string ENTEREDBY = null, string MODULEID = null, string KITNO = null, string FIELD = null,
            string OLD_VALUE = null, string NEW_VALUE = null, string ACTION_BY = null, string UPDATEQUERY = null, string INSERTQUERY = null, string PVID = null,
            string RECID = null, string STATUS = null, string ACTION_Comments = null, string VISIT_DATE = null, string EARLY_DATE = null, string LATE_DATE = null,
            string ACTUAL_VISIT_DATE = null, string NEXT_VISIT = null, string NEXT_VISIT_DATE = null, string LAST_VISIT = null, string LAST_VISIT_DATE = null,
            string RAND_NUM = null, string ACTION = null, string KITACTBY = null, string KITACTDAT = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("IWRS_DCF_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FormCode", FormCode);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@RANDNO", RANDNO);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@DESCRIPTION", DESCRIPTION);
                cmd.Parameters.AddWithValue("@FormHeader", FormHeader);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@ERR_MSG", ERR_MSG);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@KITNO", KITNO);
                cmd.Parameters.AddWithValue("@FIELD", FIELD);
                cmd.Parameters.AddWithValue("@OLD_VALUE", OLD_VALUE);
                cmd.Parameters.AddWithValue("@NEW_VALUE", NEW_VALUE);
                cmd.Parameters.AddWithValue("@ACTION_BY", ACTION_BY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION_Comments", ACTION_Comments);
                cmd.Parameters.AddWithValue("@VISIT_DATE", VISIT_DATE);
                cmd.Parameters.AddWithValue("@EARLY_DATE", EARLY_DATE);
                cmd.Parameters.AddWithValue("@LATE_DATE", LATE_DATE);
                cmd.Parameters.AddWithValue("@ACTUAL_VISIT_DATE", ACTUAL_VISIT_DATE);
                cmd.Parameters.AddWithValue("@NEXT_VISIT", NEXT_VISIT);
                cmd.Parameters.AddWithValue("@NEXT_VISIT_DATE", NEXT_VISIT_DATE);
                cmd.Parameters.AddWithValue("@LAST_VISIT", LAST_VISIT);
                cmd.Parameters.AddWithValue("@LAST_VISIT_DATE", LAST_VISIT_DATE);
                cmd.Parameters.AddWithValue("@RAND_NUM", RAND_NUM);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@KITACTBY", KITACTBY);
                cmd.Parameters.AddWithValue("@KITACTDAT", KITACTDAT);

                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_FORM_SP(string ID = null, string FIELDID = null, string FormCode = null, string VARIABLENAME = null, string COL_NAME = null,
            string INDICATION_ID = null, string ANSWER = null, string ACTION = null, string STRATA = null, string ENTEREDBY = null, string INDICATION = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_FORM_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@FormCode", FormCode);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@COL_NAME", COL_NAME);
                cmd.Parameters.AddWithValue("@INDICATION_ID", INDICATION_ID);
                cmd.Parameters.AddWithValue("@ANSWER", ANSWER);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@STRATA", STRATA);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_LIST_SP(string ID = null, string LISTID = null, string NAV_TYPE = null, string WHERE = null, string COUNTRYID = null,
            string SITEID = null, string ENTEREDBY = null, string SUBSITEID = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_LIST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTID", LISTID);
                cmd.Parameters.AddWithValue("@NAV_TYPE", NAV_TYPE);
                cmd.Parameters.AddWithValue("@WHERE", WHERE);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_CRIT_SP(string FormCode = null, string CurrentDate = null, string DATA = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_CRIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FormCode", FormCode);
                cmd.Parameters.AddWithValue("@CurrentDate", CurrentDate);
                cmd.Parameters.AddWithValue("@DATA", DATA);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_VISIT_SP(string SUBJID = null, string VISIT = null, string CritCode = null, string Criteria = null, string Field1 = null, string Field2 = null, string Field3 = null, string Field4 = null, string Field5 = null, string Field6 = null, string CONDITION1 = null, string Condition2 = null, string Condition3 = null, string Condition4 = null, string Condition5 = null, string Condition6 = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string Value6 = null, string AndOr1 = null, string AndOr2 = null, string AndOr3 = null, string AndOr4 = null, string AndOr5 = null, string DEPEND = null, string EARLY_DATE = null, string LATE_DATE = null, string LAST_VISIT_DATE = null, string NEXT_VISIT_DATE = null, string CurrentDate = null, string LAST_VISIT = null, string COL_NAME = null, string VISITNUM = null, string INDICATION_ID = null, string NEXT_VISIT = null, string NEXT_VISITNUM = null, string WINDOW = null, string EARLY = null, string LATE = null, string MODULEID = null,
            string VARIABLENAME = null, string FIELDID = null, string SEQNO = null, string TABLENAME = null, string DATA = null, string ACTION = null, string STEPID = null, string ENTEREDBY = null, string ENTEREDBYNAME = null, string ENTERED_TZVAL = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    ENTEREDBY = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    ENTEREDBYNAME = HttpContext.Current.Session["User_Name"].ToString();
                }

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    ENTERED_TZVAL = "+05:30";
                }
                else
                {
                    ENTERED_TZVAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                ds = IWRS.IWRS_VISIT_SP(SUBJID: SUBJID,
                    VISIT: VISIT,
                    CritCode: CritCode,
                    Criteria: Criteria,
                    Field1: Field1,
                    Field2: Field2,
                    Field3: Field3,
                    Field4: Field4,
                    Field5: Field5,
                    Field6: Field6,
                    CONDITION1: CONDITION1,
                    Condition2: Condition2,
                    Condition3: Condition3,
                    Condition4: Condition4,
                    Condition5: Condition5,
                    Condition6: Condition6,
                    Value1: Value1,
                    Value2: Value2,
                    Value3: Value3,
                    Value4: Value4,
                    Value5: Value5,
                    Value6: Value6,
                    AndOr1: AndOr1,
                    AndOr2: AndOr2,
                    AndOr3: AndOr3,
                    AndOr4: AndOr4,
                    AndOr5: AndOr5,
                    DEPEND: DEPEND,
                    EARLY_DATE: EARLY_DATE,
                    LATE_DATE: LATE_DATE,
                    LAST_VISIT_DATE: LAST_VISIT_DATE,
                    NEXT_VISIT_DATE: NEXT_VISIT_DATE,
                    CurrentDate: CurrentDate,
                    LAST_VISIT: LAST_VISIT,
                    COL_NAME: COL_NAME,
                    VISITNUM: VISITNUM,
                    INDICATION_ID: INDICATION_ID,
                    NEXT_VISIT: NEXT_VISIT,
                    NEXT_VISITNUM: NEXT_VISITNUM,
                    WINDOW: WINDOW,
                    EARLY: EARLY,
                    LATE: LATE,
                    MODULEID: MODULEID,
                    VARIABLENAME: VARIABLENAME,
                    FIELDID: FIELDID,
                    SEQNO: SEQNO,
                    TABLENAME: TABLENAME,
                    DATA: DATA,
                    ACTION: ACTION,
                    STEPID: STEPID,
                    ENTEREDBY: ENTEREDBY,
                    ENTEREDBYNAME: ENTEREDBYNAME,
                    ENTERED_TZVAL: ENTERED_TZVAL);
            }
            catch (Exception)
            {
                throw;
                con.Close();
            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet IWRS_KITS_COMMENTS_SP(string ACTION = null, string ID = null, string KitNo = null, string Comment = null, string UserId = null, string DateTime = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_KITS_COMMENTS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@KitNo", KitNo);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@DateTime", DateTime);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_KITS_COUNTRY_SP(string ID = null, string SHIPMENTID = null, string SITEID = null, string ORDERID = null, string SHIPMENTDATE = null, string RECEIPTDATE = null, string BLOCKEDCOMM = null, string DAMAGEDCOMM = null, string QUARANTINECOMM = null, string COUNTRYID = null, string REJECTCOMM = null, string KITNO = null, string TREAT_GRP = null, string UNQUARANTINECOMM = null, string ACTION = null, string RETURNEDCOMM = null, string RETENTIONCOMM = null, string DESTROYCOMM = null, string TABLENAME = null, string ACCEPTCOMM = null, string EXPIRECOMM = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("IWRS_KITS_COUNTRY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SHIPMENTID", SHIPMENTID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@ORDERID", ORDERID);
                cmd.Parameters.AddWithValue("@SHIPMENTDATE", SHIPMENTDATE);
                cmd.Parameters.AddWithValue("@RECEIPTDATE", RECEIPTDATE);
                cmd.Parameters.AddWithValue("@BLOCKEDCOMM", BLOCKEDCOMM);
                cmd.Parameters.AddWithValue("@DAMAGEDCOMM", DAMAGEDCOMM);
                cmd.Parameters.AddWithValue("@QUARANTINECOMM", QUARANTINECOMM);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@REJECTCOMM", REJECTCOMM);
                cmd.Parameters.AddWithValue("@KITNO", KITNO);
                cmd.Parameters.AddWithValue("@TREAT_GRP", TREAT_GRP);
                cmd.Parameters.AddWithValue("@UNQUARANTINECOMM", UNQUARANTINECOMM);
                cmd.Parameters.AddWithValue("@RETURNEDCOMM", RETURNEDCOMM);
                cmd.Parameters.AddWithValue("@RETENTIONCOMM", RETENTIONCOMM);
                cmd.Parameters.AddWithValue("@DESTROYCOMM", DESTROYCOMM);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ACCEPTCOMM", ACCEPTCOMM);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@EXPIRECOMM", EXPIRECOMM);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_KITS_SITE_SP(string COUNTRYID = null, string SITEID = null, string TRIGGER_VAL = null, string TREAT_GRP = null, string TREAT_STRENGTH = null, string TREAT_GRP_NAME = null, string RESPO_EMAIL = null, string MAIL_BODY = null, string ORDERID = null, string SHIPMENTID = null, string ID = null, string RECEIPTDATE = null, string KITNO = null, string BLOCKEDCOMM = null, string DESTROYCOMM = null, string SHIPMENTDATE = null, string DAMAGEDCOMM = null, string QUARANTINECOMM = null, string REJECTCOMM = null, string RETURNEDCOMM = null, string UNQUARANTINECOMM = null, string ACTION = null, string RETENTIONCOMM = null, string QUARANTINEDCOMM = null, string TABLENAME = null, string UNRETENTIONCOMM = null, string ACCEPTCOMM = null, string EXPIRECOMM = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {

                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("IWRS_KITS_SITE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@TRIGGER_VAL", TRIGGER_VAL);
                cmd.Parameters.AddWithValue("@TREAT_GRP", TREAT_GRP);
                cmd.Parameters.AddWithValue("@TREAT_STRENGTH", TREAT_STRENGTH);
                cmd.Parameters.AddWithValue("@TREAT_GRP_NAME", TREAT_GRP_NAME);
                cmd.Parameters.AddWithValue("@RESPO_EMAIL", RESPO_EMAIL);
                cmd.Parameters.AddWithValue("@MAIL_BODY", MAIL_BODY);
                cmd.Parameters.AddWithValue("@ORDERID", ORDERID);
                cmd.Parameters.AddWithValue("@SHIPMENTID", SHIPMENTID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@RECEIPTDATE", RECEIPTDATE);
                cmd.Parameters.AddWithValue("@KITNO", KITNO);
                cmd.Parameters.AddWithValue("@BLOCKEDCOMM", BLOCKEDCOMM);
                cmd.Parameters.AddWithValue("@DESTROYCOMM", DESTROYCOMM);
                cmd.Parameters.AddWithValue("@SHIPMENTDATE", SHIPMENTDATE);
                cmd.Parameters.AddWithValue("@DAMAGEDCOMM", DAMAGEDCOMM);
                cmd.Parameters.AddWithValue("@QUARANTINECOMM", QUARANTINECOMM);
                cmd.Parameters.AddWithValue("@REJECTCOMM", REJECTCOMM);
                cmd.Parameters.AddWithValue("@RETURNEDCOMM", RETURNEDCOMM);
                cmd.Parameters.AddWithValue("@UNQUARANTINECOMM", UNQUARANTINECOMM);
                cmd.Parameters.AddWithValue("@UNRETENTIONCOMM", UNRETENTIONCOMM);
                cmd.Parameters.AddWithValue("@RETENTIONCOMM", RETENTIONCOMM);
                cmd.Parameters.AddWithValue("@QUARANTINEDCOMM", QUARANTINEDCOMM);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@EXPIRECOMM", EXPIRECOMM);
                cmd.Parameters.AddWithValue("@ACCEPTCOMM", ACCEPTCOMM);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_KITS_CENTRAL_SP(string ID = null, string COUNTRYID = null, string SITEID = null, string LOTNO = null, string ORDERID = null, string UNQUARANTINECOMM = null, string RETENTIONCOMM = null, string BLOCKEDCOMM = null, string TREAT_GRP = null, string KITNO = null, string ACTION = null, string TREAT_GRP_NAME = null, string UNRETENTIONCOMM = null, string TABLENAME = null, string RETURNEDCOMM = null, string DESTROYCOMM = null,string ACCEPTCOMM =null, string QUARANTINECOMM = null, string EXPIRECOMM = null, string DAMAGEDCOMM = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("IWRS_KITS_CENTRAL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@LOTNO", LOTNO);
                cmd.Parameters.AddWithValue("@ACCEPTCOMM", ACCEPTCOMM);
                cmd.Parameters.AddWithValue("@ORDERID", ORDERID);
                cmd.Parameters.AddWithValue("@UNQUARANTINECOMM", UNQUARANTINECOMM);
                cmd.Parameters.AddWithValue("@QUARANTINECOMM", QUARANTINECOMM);
                cmd.Parameters.AddWithValue("@DAMAGEDCOMM", DAMAGEDCOMM);
                cmd.Parameters.AddWithValue("@RETENTIONCOMM", RETENTIONCOMM);
                cmd.Parameters.AddWithValue("@BLOCKEDCOMM", BLOCKEDCOMM);
                cmd.Parameters.AddWithValue("@TREAT_GRP", TREAT_GRP);
                cmd.Parameters.AddWithValue("@KITNO", KITNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@TREAT_GRP_NAME", TREAT_GRP_NAME);
                cmd.Parameters.AddWithValue("@UNRETENTIONCOMM", UNRETENTIONCOMM);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@RETURNEDCOMM", RETURNEDCOMM);
                cmd.Parameters.AddWithValue("@DESTROYCOMM", DESTROYCOMM);
                cmd.Parameters.AddWithValue("@EXPIRECOMM", EXPIRECOMM);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_KITS_REPORT_SP(string KITNO = null, string LOTNO = null, string COUNTRYID = null, string SITEID = null, string USERID = null, string EXPIRYDAT = null,
            string TREAT_GRP = null, string TREAT_GRP_NAME = null, string TRIGGER_VAL = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_KITS_REPORT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@KITNO", KITNO);
                cmd.Parameters.AddWithValue("@LOTNO", LOTNO);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@EXPIRYDAT", EXPIRYDAT);
                cmd.Parameters.AddWithValue("@TREAT_GRP", TREAT_GRP);
                cmd.Parameters.AddWithValue("@TREAT_GRP_NAME", TREAT_GRP_NAME);
                cmd.Parameters.AddWithValue("@TRIGGER_VAL", TRIGGER_VAL);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_KITS_SETUP_SP(string COUNTRYID = null, string SITEID = null, string TRIGGER_VAL = null, string TREAT_GRP = null, string TREAT_STRENGTH = null, string TREAT_GRP_NAME = null, string ACTION = null, string RESPO_EMAIL = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                cmd = new SqlCommand("IWRS_KITS_SETUP_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@TRIGGER_VAL", TRIGGER_VAL);
                cmd.Parameters.AddWithValue("@TREAT_GRP", TREAT_GRP);
                cmd.Parameters.AddWithValue("@TREAT_STRENGTH", TREAT_STRENGTH);
                cmd.Parameters.AddWithValue("@TREAT_GRP_NAME", TREAT_GRP_NAME);
                cmd.Parameters.AddWithValue("@RESPO_EMAIL", RESPO_EMAIL);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_KITS_EXP_SP(string ACTION = null, string ID = null, string Criteria = null, string CritCode = null, string ACTDETAILS = null, string ACTCOMMENT = null, string REQUEST_COMMENT = null, string REQUEST_EXPDAT = null, string SITEIDS = null, string LEVELS = null, string COUNTRYIDS = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null, USERID = null;
                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("IWRS_KITS_EXP_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);

                cmd.Parameters.AddWithValue("@REQUEST_COMMENT", REQUEST_COMMENT);
                cmd.Parameters.AddWithValue("@REQUEST_EXPDAT", REQUEST_EXPDAT);

                cmd.Parameters.AddWithValue("@ACTDETAILS", ACTDETAILS);
                cmd.Parameters.AddWithValue("@ACTCOMMENT", ACTCOMMENT);

                cmd.Parameters.AddWithValue("@SITEIDS", SITEIDS);
                cmd.Parameters.AddWithValue("@LEVELS", LEVELS);
                cmd.Parameters.AddWithValue("@COUNTRYIDS", COUNTRYIDS);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);



                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_UPLOAD_SP(string ID = null, string Value1 = null, string Value2 = null, string SITEID = null, string CritCode = null, string COLUMNNAME = null, string WHERE = null, string DATA = null, string INSERTQUERY = null, string ACTION = null, string MODULEID = null, string LLTCD = null, string PTCD = null, string PTNM = null, string HLTCD = null, string HLTNM = null, string HLGTCD = null, string HLGTNM = null, string SOCCD = null, string SOCNM = null, string LLTNM = null,string TREAT_GRP = null,string TREAT_GRP_NAME = null
            )
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null, USERID = null;
                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("IWRS_UPLOAD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@COLUMNNAME", COLUMNNAME);
                cmd.Parameters.AddWithValue("@WHERE", WHERE);
                cmd.Parameters.AddWithValue("@DATA", DATA);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TREAT_GRP", TREAT_GRP);
                cmd.Parameters.AddWithValue("@TREAT_GRP_NAME", TREAT_GRP_NAME);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_SET_VISIT_SP(string ID = null, string VISIT = null, string COL_NAME = null, string INDICATION_ID = null, string SITEID = null, string DEPEND = null, string FIELDNAME = null, string MODULENAME = null, string VISITNUM = null, string ENTEREDBY = null, string AND_OR = null, string AndOr1 = null, string AndOr2 = null, string AndOr3 = null, string AndOr4 = null, string AndOr5 = null, string Field1 = null, string Field2 = null, string Field3 = null, string Field4 = null, string Field5 = null, string Field6 = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null,
            string Value5 = null, string Value6 = null, string CONDITION1 = null, string Condition2 = null, string Condition3 = null, string Condition4 = null, string Condition5 = null, string Condition6 = null, string INDICATION = null, string CritCode = null, string Criteria = null, string SEQNO = null, string MODULEID = null,
            string VisitSummarySeq = null, bool DOSING = false, bool Applicable_For_VisitSummary = false, string EARLY = null, string FIELDID = null, string LATE = null, string WINDOW = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }
                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_SET_VISIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@COL_NAME", COL_NAME);
                cmd.Parameters.AddWithValue("@INDICATION_ID", INDICATION_ID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@DEPEND", DEPEND);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@AND_OR", AND_OR);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@AndOr5", AndOr5);
                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Field5", Field5);
                cmd.Parameters.AddWithValue("@Field6", Field6);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@Value6", Value6);
                cmd.Parameters.AddWithValue("@CONDITION1", CONDITION1);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@Condition6", Condition6);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VisitSummarySeq", VisitSummarySeq);
                cmd.Parameters.AddWithValue("@Applicable_For_VisitSummary", Applicable_For_VisitSummary);
                cmd.Parameters.AddWithValue("@DOSING", DOSING);
                cmd.Parameters.AddWithValue("@EARLY", EARLY);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@LATE", LATE);
                cmd.Parameters.AddWithValue("@WINDOW", WINDOW);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_SET_COLS_SP(string ID = null, string FIELDNAME = null, string COL_NAME = null, string ENTEREDBY = null, string ACTION = null, bool IWRS_Graph = false, bool IWRS_Tile = false)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_SET_COLS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@COL_NAME", COL_NAME);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@IWRS_Graph", IWRS_Graph);
                cmd.Parameters.AddWithValue("@IWRS_Tile", IWRS_Tile);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_SET_ARMS_SP(string ID = null, string TREAT_GRP = null, string TREAT_GRP_NAME = null, string ACTION = null, string ENTEREDBY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_SET_ARMS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TREAT_GRP", TREAT_GRP);
                cmd.Parameters.AddWithValue("@TREAT_GRP_NAME", TREAT_GRP_NAME);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);


                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_SET_FORM_SP(string ACTION = null, string ID = null, string FORMID = null, string LISTID = null, string FIELDID = null, string MODULENAME = null, string MODULEID = null, string STRATA = null, string LIMIT = null, string INDICATION = null, string ANSWER = null, string COL_NAME = null, string CritName = null, string CritCode = null, string Criteria = null, string FormCode = null, string ERR_MSG = null, string Condition3 = null, string Condition4 = null, string Condition5 = null, string Condition6 = null, string AndOr1 = null, string AndOr2 = null, string AndOr3 = null, string AndOr4 = null, string AndOr5 = null, string Field1 = null, string Field2 = null, string Field3 = null, string Field4 = null, string Field5 = null, string Field6 = null, string ANS = null, string Value1 = null, string SEQNO = null, string AND_OR = null, string FORMULA = null, string METHOD = null, string ENTEREDBY = null, string CONDITION1 = null, string DESCRIPTION = null, string INDICATION_ID = null, string Condition2 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string Value6 = null,
            string VARIABLENAME = null, string FIELDNAME = null, string TABLENAME = null, string FormName = null,
            string Descrip = null, string CONTROLTYPE = null, string FORMAT = null, string MAXLEN = null, string AnsColor = null, string FieldColor = null, string DefaultData = null, bool UPPERCASE = false, bool INVISIBLE = false, bool UNLNYN = false, bool BOLDYN = false, bool READYN = false, bool MULTILINEYN = false, bool MANDATORY = false, bool RPT_VISITSUM = false,bool RPT_DOSESUM_B = false,bool RPT_DOSESUM_U = false,bool RPT_RANDKITSUM = false,bool RPT_RANDTRTSUM = false)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_SET_FORM_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FORMID", FORMID);
                cmd.Parameters.AddWithValue("@LISTID", LISTID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STRATA", STRATA);
                cmd.Parameters.AddWithValue("@LIMIT", LIMIT);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@ANSWER", ANSWER);
                cmd.Parameters.AddWithValue("@COL_NAME", COL_NAME);
                cmd.Parameters.AddWithValue("@CritName", CritName);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@FormCode", FormCode);
                cmd.Parameters.AddWithValue("@ERR_MSG", ERR_MSG);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@Condition6", Condition6);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@AndOr5", AndOr5);
                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Field5", Field5);
                cmd.Parameters.AddWithValue("@Field6", Field6);
                cmd.Parameters.AddWithValue("@ANS", ANS);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@AND_OR", AND_OR);
                cmd.Parameters.AddWithValue("@FORMULA", FORMULA);
                cmd.Parameters.AddWithValue("@METHOD", METHOD);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@CONDITION1", CONDITION1);
                cmd.Parameters.AddWithValue("@DESCRIPTION", DESCRIPTION);
                cmd.Parameters.AddWithValue("@INDICATION_ID", INDICATION_ID);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@Value6", Value6);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@FormName", FormName);

                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@FORMAT", FORMAT);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@AnsColor", AnsColor);
                cmd.Parameters.AddWithValue("@FieldColor", FieldColor);
                cmd.Parameters.AddWithValue("@UPPERCASE", UPPERCASE);
                cmd.Parameters.AddWithValue("@INVISIBLE", INVISIBLE);
                cmd.Parameters.AddWithValue("@UNLNYN", UNLNYN);
                cmd.Parameters.AddWithValue("@BOLDYN", BOLDYN);
                cmd.Parameters.AddWithValue("@READYN", READYN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);
                cmd.Parameters.AddWithValue("@MANDATORY", MANDATORY);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                cmd.Parameters.AddWithValue("@RPT_VISITSUM", RPT_VISITSUM);
                cmd.Parameters.AddWithValue("@RPT_DOSESUM_B", RPT_DOSESUM_B);
                cmd.Parameters.AddWithValue("@RPT_DOSESUM_U", RPT_DOSESUM_U);
                cmd.Parameters.AddWithValue("@RPT_RANDKITSUM", RPT_RANDKITSUM);
                cmd.Parameters.AddWithValue("@RPT_RANDTRTSUM", RPT_RANDTRTSUM);


                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_SET_LIST_SP(string ID = null, string MODULEID = null, string AndOr1 = null, string AndOr2 = null, string AndOr3 = null, string AndOr4 = null, string AndOr5 = null, string FIELDID = null, string DESCRIPTION = null, string FIELDNAME = null, string LISTID = null, string COL_NAME = null, string LISTNAME = null, string VARIABLENAME = null, string SEQNO = null, string TABLENAME = null, string Field1 = null, string Field2 = null, string Field3 = null, string Field4 = null, string Field5 = null, string Field6 = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string Value6 = null, string CONDITION1 = null, string Condition2 = null, string Condition3 = null, string Condition4 = null, string Condition5 = null,
            string Condition6 = null, string Criteria = null, string CritCode = null, string ACTION = null, string ENTEREDBY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {

                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_SET_LIST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@AndOr5", AndOr5);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@DESCRIPTION", DESCRIPTION);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@LISTID", LISTID);
                cmd.Parameters.AddWithValue("@COL_NAME", COL_NAME);
                cmd.Parameters.AddWithValue("@LISTNAME", LISTNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Field5", Field5);
                cmd.Parameters.AddWithValue("@Field6", Field6);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@Value6", Value6);
                cmd.Parameters.AddWithValue("@CONDITION1", CONDITION1);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@Condition6", Condition6);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_SET_OPTION_SP(string ID = null, string STRATA = null, string QUECODE = null, string SEQNO = null, string ANS = null, string FileName = null, string ContentType = null, byte[] fileData = null, string QUESTION = null, string ACTION = null, string ENTEREDBY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_SET_OPTION_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@STRATA", STRATA);
                cmd.Parameters.AddWithValue("@QUECODE", QUECODE);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@ANS", ANS);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@fileData", fileData);
                cmd.Parameters.AddWithValue("@QUESTION", QUESTION);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_SET_STATUS_SP(string ID = null, string STATUSNAME = null, string STATUSCODE = null, string ENTEREDBY = null,
           string CONDITION1 = null, string ACTION = null, string INDICATION_ID = null, bool IWRS_Graph = false, bool IWRS_Tile = false)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_SET_STATUS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@STATUSNAME", STATUSNAME);
                cmd.Parameters.AddWithValue("@STATUSCODE", STATUSCODE);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@IWRS_Graph", IWRS_Graph);
                cmd.Parameters.AddWithValue("@IWRS_Tile", IWRS_Tile);
                cmd.Parameters.AddWithValue("@CONDITION1", CONDITION1);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INDICATION_ID", INDICATION_ID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_SET_SUBSITE_SP(string ID = null, string SITEID = null, string SUBSITEID = null, string SUBSITENAME = null, string ACTION = null, string USER_ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USER_ID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_SET_SUBSITE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@SUBSITENAME", SUBSITENAME);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USER_ID", USER_ID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_SET_EMAIL_SP(string STRATA = null, string SITEID = null, string COUNTRY = null, string EMAIL_IDS = null, string CCEMAIL_IDS = null, string BCCEMAIL_IDS = null, string ACTION = null, string ENTEREDBY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null, USER_ID = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USER_ID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_SET_EMAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@STRATA", STRATA);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@COUNTRY", COUNTRY);
                cmd.Parameters.AddWithValue("@EMAIL_IDS", EMAIL_IDS);
                cmd.Parameters.AddWithValue("@CCEMAIL_IDS", CCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@BCCEMAIL_IDS", BCCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@USER_ID", USER_ID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);


                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }


        public DataSet IWRS_STOP_CLAUSE_SP(string ID = null, string MODULEID = null, string CritCode = null, string Criteria = null, string CritName = null, string FIELDNAME = null, string SUBJID = null, string SITEID = null, string MODULENAME = null, string Field1 = null, string Field2 = null, string Field3 = null, string Field4 = null, string Field5 = null, string Condition1 = null, string Condition2 = null, string Condition3 = null, string Condition4 = null, string Condition5 = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string AndOr1 = null, string AndOr2 = null, string AndOr3 = null, string AndOr4 = null, string MSGBOX = null, string COLNAME = null, string FIELDID = null, string LIMIT = null, string VARIABLENAME = null, string NAVTO_TYPE = null, string TABLENAME = null, string NAVTO = null, string ACTION = null, bool BEFORE = false, bool AFTER = false, string ENTEREDBY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_STOP_CLAUSE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@CritName", CritName);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Field5", Field5);
                cmd.Parameters.AddWithValue("@Condition1", Condition1);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@MSGBOX", MSGBOX);
                cmd.Parameters.AddWithValue("@COLNAME", COLNAME);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@LIMIT", LIMIT);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@NAVTO_TYPE", NAVTO_TYPE);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@AFTER", AFTER);
                cmd.Parameters.AddWithValue("@NAVTO", NAVTO);
                cmd.Parameters.AddWithValue("@BEFORE", BEFORE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);


                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_SYNC_SP(string ACTION = null, string MODULEID = null, string FIELDNAME = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_SYNC_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_MAPPING_SP(string ACTION = null, string FIELDID = null, string FORMID = null, string MODULEID = null, string VARIABLENAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("IWRS_MAPPING_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@FORMID", FORMID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_RANDNO_SP(string ACTION = null, string FromBlock = null, string ToBlock = null, string RANDNO = null, string COMM = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;

                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd = new SqlCommand("IWRS_RANDNO_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@FromBlock", FromBlock);
                cmd.Parameters.AddWithValue("@ToBlock", ToBlock);
                cmd.Parameters.AddWithValue("@RANDNO", RANDNO);
                cmd.Parameters.AddWithValue("@COMM", COMM);
                cmd.Parameters.AddWithValue("@UserID", USERID);
                cmd.Parameters.AddWithValue("@UserName", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_MANG_SUBJECT_CRIT_SP(string ACTION = null,string INPUTMASK = null, string ID = null, string FIELDID = null,
            string MODULENAME = null, string MODULEID = null, string STRATA = null, string INDICATION = null, 
            string ANSWER = null, string COL_NAME = null, string CritName = null, string CritCode = null, 
            string Criteria = null, string FormCode = null, string ERR_MSG = null, string Condition3 = null, 
            string Condition4 = null, string Condition5 = null, string Condition6 = null, string AndOr1 = null, 
            string AndOr2 = null, string AndOr3 = null, string AndOr4 = null, string AndOr5 = null, string Field1 = null, 
            string Field2 = null, string Field3 = null, string Field4 = null, string Field5 = null, string Field6 = null, 
            string Value1 = null, string SEQNO = null, string AND_OR = null, string FORMULA = null, string METHOD = null, 
            string ENTEREDBY = null, string CONDITION1 = null, string DESCRIPTION = null, string INDICATION_ID = null, 
            string Condition2 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, 
            string Value6 = null, string VARIABLENAME = null, string FIELDNAME = null, string TABLENAME = null, 
            string FormName = null, string FORMID = null, string ANS = null, string QUESTION = null, string QUECODE = null,
            string FileName = null, string ContentType = null,string fileData = null,string DATA = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_MANG_SUBJECT_CRIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INPUTMASK", INPUTMASK);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STRATA", STRATA);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@ANSWER", ANSWER);
                cmd.Parameters.AddWithValue("@COL_NAME", COL_NAME);
                cmd.Parameters.AddWithValue("@CritName", CritName);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@FormCode", FormCode);
                cmd.Parameters.AddWithValue("@ERR_MSG", ERR_MSG);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@Condition6", Condition6);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@AndOr5", AndOr5);
                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Field5", Field5);
                cmd.Parameters.AddWithValue("@Field6", Field6);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@AND_OR", AND_OR);
                cmd.Parameters.AddWithValue("@FORMULA", FORMULA);
                cmd.Parameters.AddWithValue("@METHOD", METHOD);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@CONDITION1", CONDITION1);
                cmd.Parameters.AddWithValue("@DESCRIPTION", DESCRIPTION);
                cmd.Parameters.AddWithValue("@INDICATION_ID", INDICATION_ID);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@Value6", Value6);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@FormName", FormName);
                cmd.Parameters.AddWithValue("@FORMID", FORMID);
                cmd.Parameters.AddWithValue("@ANS", ANS);
                cmd.Parameters.AddWithValue("@QUESTION", QUESTION);
                cmd.Parameters.AddWithValue("@QUECODE", QUECODE);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@fileData", fileData);
                cmd.Parameters.AddWithValue("@DATA", DATA);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_DOCS_SP(string ID = null, string DOCS_NAME = null,  string FileName = null, string ContentType = null, byte[] fileData = null,  string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_DOCS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@DOCS_NAME", DOCS_NAME);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@fileData", fileData);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet IWRS_UNBLIND_REPORT_SP(string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("IWRS_UNBLIND_REPORT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

    }
}