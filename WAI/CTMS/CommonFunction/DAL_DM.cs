using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CTMS.CommonFunction
{
    public class DAL_DM
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
        SqlDataAdapter adp;

        CTMS.DM.DM DM = new CTMS.DM.DM();

        public string getconstr()
        {
            return ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString;
        }

        public DataSet DM_VISIT_SP(string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_VISIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_VISIT_CRITERIA_SP(string ID = null, string SUBJID = null, string SITEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_VISIT_CRITERIA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_OPEN_CRF_SP(string SUBJID = null, string VISITNUM = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TIMEZONE = null, MEDAUTH_FORM = null, Unblind = null;
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TIMEZONE = "+05:30";
                }
                else
                {
                    TIMEZONE = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                if (HttpContext.Current.Session["MEDAUTH_FORM"] != null)
                {
                    MEDAUTH_FORM = HttpContext.Current.Session["MEDAUTH_FORM"].ToString();
                }

                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["Unblind"] != null)
                {
                    Unblind = HttpContext.Current.Session["Unblind"].ToString();
                }

                cmd = new SqlCommand("DM_OPEN_CRF_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);
                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", MEDAUTH_FORM);
                cmd.Parameters.AddWithValue("@Unblind", Unblind);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_MODULE_CRITERIA_SP(string SUBJID = null, string VISITNUM = null, string ID = null, string SITEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_MODULE_CRITERIA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@ID", ID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();

            }
            return ds;
        }

        public DataSet DM_UNSC_VISIT_SP(string ACTION = null, string SUBJID = null)
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

                cmd = new SqlCommand("DM_UNSC_VISIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
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
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_SUBJECT_LIST_SP(string INVID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_SUBJECT_LIST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INVID", INVID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_MODULE_SP(string ACTION = null, string ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_MODULE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_VISIT_DATA_SP(string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_VISIT_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet GetSet_DM_ProjectData(string Action = null, string PVID = null,
           int ContID = 0, string VARIABLENAME = null, string FIELDNAME = null,
               string TABLENAME = null, string CONTROLTYPE = null, string CLASS = null,
            string DATATYPE = null, string DATA = null, string RECID = null, int CONTYN = 0, string ENTEREDBY = null,
            string UPDATEDBY = null, string PROJECTID = null, string MODULENAME = null, string VISITNUM = null, string SUBJID = null,
            string MODULEID = null, string INDICATION = null, string INSERTQUERY = null, string UPDATEQUERY = null
            , string IMPORT_MODULEID = null, string EMAIL_IDS = null, string CCEMAIL_IDS = null, string BCCEMAIL_IDS = null, string INVID = null,
            string Email_Subject = null, string Email_body = null, bool IsMissing = false, string ID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();

            try
            {
                string MEDAUTH_FORM = null;
                if (HttpContext.Current.Session["MEDAUTH_FORM"] != null)
                {
                    MEDAUTH_FORM = HttpContext.Current.Session["MEDAUTH_FORM"].ToString();
                }

                string MEDAUTH_FIELD = null;
                if (HttpContext.Current.Session["MEDAUTH_FIELD"] != null)
                {
                    MEDAUTH_FIELD = HttpContext.Current.Session["MEDAUTH_FIELD"].ToString();
                }

                cmd = new SqlCommand("DM_PROJECT_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@ContID", ContID);
                cmd.Parameters.AddWithValue("@RecID", RECID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@CLASS", CLASS);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);

                if (CONTROLTYPE == "DROPDOWN")
                {
                    if (DATA != "0")
                    {
                        cmd.Parameters.AddWithValue("@DATA", DATA);
                    }
                }
                else
                {
                    if (DATA != "")
                    {
                        cmd.Parameters.AddWithValue("@DATA", DATA);
                    }
                }

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);
                cmd.Parameters.AddWithValue("@CONTYN", CONTYN);
                cmd.Parameters.AddWithValue("@IMPORT_MODULEID", IMPORT_MODULEID);
                cmd.Parameters.AddWithValue("@EMAIL_IDS", EMAIL_IDS);
                cmd.Parameters.AddWithValue("@CCEMAIL_IDS", CCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@BCCEMAIL_IDS", BCCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@EMAIL_SUBJECT", Email_Subject);
                cmd.Parameters.AddWithValue("@EMAIL_BODY", Email_body);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", MEDAUTH_FORM);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
                cmd.Parameters.AddWithValue("@ID", ID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
               // cmd.Dispose();
               // con.Close();
            }
            catch (Exception ex)
            {
                throw;
                con.Close();

            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_GENERAL_SP(string ACTION = null, string MODULEID = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_GENERAL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_INSTRUCTION_SP(string MODULEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_INSTRUCTION_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_PAGE_STATUS_SP(string PVID = null, string RECID = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_PAGE_STATUS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();

            }
            return ds;
        }

        public DataSet DM_STRUCTURE_SP(string ACTION = null, string MODULEID = null, string FIELDID = null, string VISITNUM = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                string MEDAUTH_FORM = null;
                if (HttpContext.Current.Session["MEDAUTH_FORM"] != null)
                {
                    MEDAUTH_FORM = HttpContext.Current.Session["MEDAUTH_FORM"].ToString();
                }

                string MEDAUTH_FIELD = null;
                if (HttpContext.Current.Session["MEDAUTH_FIELD"] != null)
                {
                    MEDAUTH_FIELD = HttpContext.Current.Session["MEDAUTH_FIELD"].ToString();
                }

                cmd = new SqlCommand("DM_STRUCTURE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", MEDAUTH_FORM);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
               // con.Close();
               // return ds;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;              

            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_OPTIONS_DATA_SP(string Action = null, string VARIABLENAME = null, string VISITNUM = null, string TABLENAME = null, string PVID = null, string RECID = null, string MODULEID = null, string SUBJID = null, string VALUE = null, string ParentANS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {

                cmd = new SqlCommand("DM_OPTIONS_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VALUE", VALUE);
                cmd.Parameters.AddWithValue("@ParentANS", ParentANS);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_MODULE_DATA_SP(string VISITNUM = null, string SUBJID = null, string MODULEID = null, string TABLENAME = null, string PVID = null, string RECID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_MODULE_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_RECID_SP(string PVID = null, string SUBJID = null, string TABLENAME = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("DM_RECID_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                //con.Close();
                //return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();

            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_IWRS_SP(string ACTION = null, string SUBJID = null, string VARIABLENAME = null, string ANSWER = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = "";
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("DM_IWRS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ANSWER", ANSWER);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet CODING_SP(string ACTION = null, string PVID = null, string RECID = null, string MODULEID = null,
            string VARIABLENAME = null)
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                ds = DM.CODING_SP(ACTION: ACTION,
                            PVID: PVID,
                            RECID: RECID,
                            MODULEID: MODULEID,
                            VARIABLENAME: VARIABLENAME,
                            USERID: USERID,
                            User_Name: User_Name,
                            TZ_VAL: TZ_VAL
                            );
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
               

            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_AUDITTRAIL_SP(string ACTION = null, string PVID = null, string RECID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string VARIABLENAME = null, string REASON = null, string FIELDNAME = null, string INVID = null, string VISIT = null, string MODULENAME = null, string TRANSACT = null, string FIELDID = null, string ID = null)
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
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                ds = DM.DM_AUDITTRAIL_SP(ACTION: ACTION,
                                PVID: PVID,
                                RECID: RECID,
                                INVID: INVID,
                                SUBJID: SUBJID,
                                VISITNUM: VISITNUM,
                                VISIT: VISIT,
                                MODULEID: MODULEID,
                                MODULENAME: MODULENAME,
                                USERID: USERID,
                                User_Name: User_Name,
                                TZ_VAL: TZ_VAL,
                                VARIABLENAME: VARIABLENAME,
                                REASON: REASON,
                                FIELDNAME: FIELDNAME,
                                TRANSACT: TRANSACT,
                                FIELDID: FIELDID,
                                ID: ID
                                );
            }
            catch (Exception ex)
            {
                con.Close();
                throw;               

            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_INSERT_MODULE_DATA_SP(string ACTION = null, string TABLENAME = null, string PVID = null, string RECID = null, string INSERTQUERY = null, string UPDATEQUERY = null, string SUBJID = null, string VISITNUM = null, string INVID = null, string MODULEID = null, bool IsComplete = false, bool IsMissing = false, string VISIT = null, string MODULENAME = null)
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                ds = DM.DM_INSERT_MODULE_DATA_SP(ACTION: ACTION,
                                    TABLENAME: TABLENAME,
                                    PVID: PVID,
                                    RECID: RECID,
                                    SUBJID: SUBJID,
                                    VISITNUM: VISITNUM,
                                    VISIT: VISIT,
                                    INVID: INVID,
                                    MODULEID: MODULEID,
                                    MODULENAME: MODULENAME,
                                    USERID: USERID,
                                    INSERTQUERY: INSERTQUERY,
                                    UPDATEQUERY: UPDATEQUERY,
                                    User_Name: User_Name,
                                    TZ_VAL: TZ_VAL,
                                    IsComplete: IsComplete,
                                    IsMissing: IsMissing
                                    );
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }


        public void DM_GetSetPV(string Action = null, string PVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string PAGESTATUS = null, bool HasMissing = false, string INVID = null, string NOTAPPLICABLE = null, string MODULENAME = null, string VISIT = null)
        {
            SqlCommand cmd;
            DataSet ds = new DataSet();
            try
            {
                string TZ_VAL = null, User_Name = null, USERID = null;

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

                DM.DM_GetSetPV(
                        Action: Action,
                        PVID: PVID,
                        INVID: INVID,
                        SUBJID: SUBJID,
                        VISITNUM: VISITNUM,
                        VISIT: VISIT,
                        MODULEID: MODULEID,
                        PAGESTATUS: PAGESTATUS,
                        HasMissing: HasMissing,
                        NOTAPPLICABLE: NOTAPPLICABLE,
                        MODULENAME: MODULENAME,
                        USERID: USERID,
                        User_Name: User_Name,
                        TZ_VAL: TZ_VAL);
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                con.Close();
            }
        }

        public DataSet DM_OnSubmit_CRIT_SP(string ACTION = null, string MODULEID = null, string VISITNUM = null, string SUBJID = null, string ID = null, string TABLENAME = null, string VARIABLENAME = null, string Condition = null, string Formula = null, string CritName = null, string INVID = null, string PVID = null, string RECID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("DM_OnSubmit_CRIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@Condition", Condition);
                cmd.Parameters.AddWithValue("@Formula", Formula);
                cmd.Parameters.AddWithValue("@CritName", CritName);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                //con.Close();
                //return ds;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_Specs_Module_Wise_SP(string ACTION = null, string MODULEID = null, string FORMULA = null, string CritCode = null, string VARIABLENAME = null, string VISITNUM = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("DM_Specs_Module_Wise_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FORMULA", FORMULA);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
               // con.Close();
               // return ds;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;               
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_SIGNOFF_SP(string ACTION = null, string PVID = null, string RECID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string TABLENAME = null, string VISIT = null, string MODULENAME = null)
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

                cmd = new SqlCommand("DM_SIGNOFF_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
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
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                //con.Close();
                //return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_SAE_SYNC_SP(string ACTION = null, string PVID = null, string RECID = null, string MODULEID = null, string SUBJID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                ds = DM.DM_SAE_SYNC_SP(
                    ACTION: ACTION,
                    PVID: PVID,
                    RECID: RECID,
                    MODULEID: MODULEID,
                    SUBJID: SUBJID);
            }
            catch (Exception ex)
            {
                con.Close();
                throw;               
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_QUERY_SP(string ACTION = null, string PVID = null, string RECID = null, string VARIABLENAME = null, string ID = null, string Comment = null, string REASON = null, string QUERYTEXT = null)
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

                cmd = new SqlCommand("DM_QUERY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@REASON", REASON);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
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
                //con.Close();
                //return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();


            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }


        public DataSet DM_COMMENT_SP(string ACTION = null, string PVID = null, string RECID = null, string SUBJID = null, string VISITID = null, string MODULEID = null, string MODULENAME = null, string FIELDNAME = null, string VARIABLENAME = null, string COMMENT = null, string VISIT = null, string INVID = null)
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

                cmd = new SqlCommand("DM_COMMENT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@COMMENT", COMMENT);
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
                //con.Close();
                //return ds;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_RUN_RULE(string Action = null, string RULE_ID = null, string Nature = null, string SUBJID = null, string Para_Visit_ID = null, string Para_VariableName = null, string Para_ModuleName = null, string PVID = null, string RECID = null, string Data = null, string QUERYTEXT = null, string Module_ID = null, string Field_ID = null, string VARIABLENAME = null, string VISIT = null, string OtherPVIDS = null, string VISITNUM = null, string INVID = null, string TABLENAME = null, string Condition = null, string FORMULA = null, string Value = null)
        {
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

                SqlCommand cmd = new SqlCommand("DM_RUN_RULE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@RULE_ID", RULE_ID);
                cmd.Parameters.AddWithValue("@Nature", Nature);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@Para_Visit_ID", Para_Visit_ID);
                cmd.Parameters.AddWithValue("@Para_VariableName", Para_VariableName);
                cmd.Parameters.AddWithValue("@Para_ModuleName", Para_ModuleName);

                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@Module_ID", Module_ID);
                cmd.Parameters.AddWithValue("@Field_ID", Field_ID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@OtherPVIDS", OtherPVIDS);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@Condition", Condition);
                cmd.Parameters.AddWithValue("@FORMULA", FORMULA);
                cmd.Parameters.AddWithValue("@Value", Value);
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
                con.Close();
                throw ex;
            }
            finally
            {
                con.Close();

            }
            return ds;
        }

        public DataSet DM_DELETE_RECORDS_SP(string PVID = null, string RECID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string VISIT = null, string MODULENAME = null, string REASON = null)
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

                ds = DM.DM_DELETE_RECORDS_SP(PVID: PVID,
                                        RECID: RECID,
                                        INVID: INVID,
                                        SUBJID: SUBJID,
                                        VISITNUM: VISITNUM,
                                        VISIT: VISIT,
                                        MODULEID: MODULEID,
                                        MODULENAME: MODULENAME,
                                        REASON: REASON,
                                        USERID: USERID,
                                        User_Name: User_Name,
                                        TZ_VAL: TZ_VAL);
            }
            catch (Exception ex)
            {
                con.Close();
                throw;

            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_SINGLE_RECORS_SP(string ACTION = null, string PVID = null, string RECID = null, string SUBJID = null, string VISITID = null, string MODULEID = null, string TABLENAME = null, string VARIABLENAME = null)
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

                cmd = new SqlCommand("DM_SINGLE_RECORS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
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
               // con.Close();
               // return ds;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_COMPLETE_DATA_SP(string PVID = null, string RECID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string VISIT = null, string MODULEID = null, string MODULENAME = null)
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                ds = DM.DM_COMPLETE_DATA_SP(PVID: PVID,
                               RECID: RECID,
                               INVID: INVID,
                               SUBJID: SUBJID,
                               VISITNUM: VISITNUM,
                               VISIT: VISIT,
                               MODULEID: MODULEID,
                               MODULENAME: MODULENAME,
                               USERID: USERID,
                               User_Name: User_Name,
                               TZ_VAL: TZ_VAL);
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_INSLIST_EDITABLE_SP(string ACTION = null, string VISITID = null, string MODULEID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                string MEDAUTH_FIELD = null;
                if (HttpContext.Current.Session["MEDAUTH_FIELD"] != null)
                {
                    MEDAUTH_FIELD = HttpContext.Current.Session["MEDAUTH_FIELD"].ToString();
                }

                cmd = new SqlCommand("DM_INSLIST_EDITABLE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                //con.Close();
                //return ds;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_MULTIPLE_RECORS_SP(string ACTION = null, string PVID = null, string RECID = null, string SUBJID = null, string VISITID = null, string MODULEID = null, string TABLENAME = null, string VARIABLENAME = null)
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

                cmd = new SqlCommand("DM_MULTIPLE_RECORS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
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
               // con.Close();
               // return ds;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;               
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_SDVDETAILS_SP(string ACTION, string PVID = null, string RECID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string VARIABLENAME = null, int SDVYN = 0, string colChecked = null, string colUnChecked = null, string isSDVComplete = null, string VISIT = null, string MODULENAME = null)
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

                //Get Data for Update
                cmd = new SqlCommand("DM_SDVDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@SDVYN", SDVYN);
                cmd.Parameters.AddWithValue("@colChecked", colChecked);
                cmd.Parameters.AddWithValue("@colUnChecked", colUnChecked);
                cmd.Parameters.AddWithValue("@isSDVComplete", isSDVComplete);
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
            catch (Exception ex)
            {
                con.Close();
                throw;

            }
            finally
            {
                //ds.Dispose();
                con.Close();

            }
            return ds;
        }

        public DataSet DM_VISIT_OPEN_CRF_SDV_SP(string PVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string TABLENAME = null)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_VISIT_OPEN_CRF_SDV_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_FREEZE_SP(string ACTION = null, string PVID = null, string RECID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string TABLENAME = null, string VISIT = null, string MODULENAME = null, string COUNTRYID = null)
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

                cmd = new SqlCommand("DM_FREEZE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);

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
                con.Close();
                throw;

            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet REACT_SDV_SP(string ACTION = null, string ID = null, string SITEID = null, string SUBJID = null
            , string TABLENAME = null, string AUTOID = null, string SDVBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("REACT_SDV_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@AUTOID", AUTOID);
                cmd.Parameters.AddWithValue("@SDVBY", SDVBY);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_LOCK_SP(string ACTION = null, string PVID = null, string RECID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string TABLENAME = null, string VISIT = null, string MODULENAME = null, string COUNTRYID = null, string TYPE = null)
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

                cmd = new SqlCommand("DM_LOCK_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
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
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@TYPE", TYPE);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_UNFREEZE_REQUEST_SP(string ACTION = null, string PVID = null, string RECID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string TABLENAME = null, string COMMENT = null, string VISIT = null, string MODULENAME = null)
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

                cmd = new SqlCommand("DM_UNFREEZE_REQUEST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
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
                cmd.Parameters.AddWithValue("@COMMENT", COMMENT);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_SYNC_EDC_DATA(string PVID = null, string RECID = null, string INVID = null, string MODULEID = null, string @VISITNUM = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_SYNC_EDC_DATA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_UNLOCK_REQUEST_SP(string ACTION = null, string PVID = null, string RECID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string TABLENAME = null, string COMMENT = null, string VISIT = null, string MODULENAME = null)
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

                cmd = new SqlCommand("DM_UNLOCK_REQUEST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
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
                cmd.Parameters.AddWithValue("@COMMENT", COMMENT);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_QUERY_REPORT_SP(string ACTION = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string FIELDID = null, string QUERY_STATUS = null, string QUERY_TYPE = null, string QUERYID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string UNBLIND = "";

                if (HttpContext.Current.Session["Unblind"] != null)
                {
                    UNBLIND = HttpContext.Current.Session["Unblind"].ToString();
                }

                cmd = new SqlCommand("DM_QUERY_REPORT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@QUERY_STATUS", QUERY_STATUS);
                cmd.Parameters.AddWithValue("@QUERY_TYPE", QUERY_TYPE);
                cmd.Parameters.AddWithValue("@UNBLIND", UNBLIND);
                cmd.Parameters.AddWithValue("@QUERYID", QUERYID);
                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["USER_ID"].ToString());
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_DATA_IMPACT_WARNING_SP(string ACTION = null, string VISITID = null, string MODULEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_DATA_IMPACT_WARNING_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_Manual_Query_SP(string ACTION, string PVID = null, string RECID = null, string SUBJID = null, string VISITNUM = null, string VISIT = null, string QUERYTEXT = null, string MODULEID = null, string MODULENAME = null, string FIELDID = null, string FIELDNAME = null, string TABLENAME = null, string VARIABLENAME = null, string INVID = null)
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

                cmd = new SqlCommand("DM_Manual_Query_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ENTEREDBY", USERID);
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
                con.Close();
                throw;
            }
            finally
            {
                //ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_OnChange_MODULE_CRITERIA_SP(string ACTION = null, string SUBJID = null, string VISITNUM = null, string ID = null, string VARIABLENAME = null, string VALUE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string SITEID = null;

                cmd = new SqlCommand("DM_OnChange_MODULE_CRITERIA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@VALUE", VALUE);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_OnChange_VISIT_CRITERIA_SP(string ACTION = null, string SUBJID = null, string VISITNUM = null, string ID = null, string VARIABLENAME = null, string VALUE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string SITEID = null;

                cmd = new SqlCommand("DM_OnChange_VISIT_CRITERIA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@VALUE", VALUE);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_LAB_DATA_SP(string ACTION = null, string MODULEID = null, string SUBJID = null, string LABID = null, string LBTEST = null, string SEX = null, string INVID = null, string VISITNUM = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("DM_LAB_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@LABID", LABID);
                cmd.Parameters.AddWithValue("@LBTEST", LBTEST);
                cmd.Parameters.AddWithValue("@SEX", SEX);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
              //  con.Close();
              //  return ds;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_MM_QUERY_SP(string ACTION = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string FIELDID = null, string QUERY_STATUS = null, string QUERY_TYPE = null, string QUERYID = null, string PVID = null, string RECID = null, string QUERYTEXT = null, string DM_QUERYID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string UNBLIND = "", USERID = null, TZ_VAL = null, User_Name = null;

                if (HttpContext.Current.Session["Unblind"] != null)
                {
                    UNBLIND = HttpContext.Current.Session["Unblind"].ToString();
                }

                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("DM_MM_QUERY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@QUERY_STATUS", QUERY_STATUS);
                cmd.Parameters.AddWithValue("@QUERY_TYPE", QUERY_TYPE);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@UNBLIND", UNBLIND);
                cmd.Parameters.AddWithValue("@QUERYID", QUERYID);
                cmd.Parameters.AddWithValue("@DM_QUERYID", DM_QUERYID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
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
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_LIST_SP(string ACTION = null, string ID = null, string MODULEID = null, string LISTING_ID = null,
            string COUNTRYID = null, string INVID = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, User_Name = null, TZ_VAL = null;

                cmd = new SqlCommand("DM_LIST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);

                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_PRINT_REPORT_SP(string ACTION = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string PVID = null, string RECID = null, string FIELDID = null, string VARIABLENAME = null, string TABLENAME = null, string DefaultData = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string MEDAUTH_FIELD = null, MEDAUTH_FORM = null;

                if (HttpContext.Current.Session["MEDAUTH_FORM"] != null)
                {
                    MEDAUTH_FORM = HttpContext.Current.Session["MEDAUTH_FORM"].ToString();
                }

                if (HttpContext.Current.Session["MEDAUTH_FIELD"] != null)
                {
                    MEDAUTH_FIELD = HttpContext.Current.Session["MEDAUTH_FIELD"].ToString();
                }

                cmd = new SqlCommand("DM_PRINT_REPORT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", MEDAUTH_FORM);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_EMAIL_SP(string ACTION = null, string ID = null, string PVID = null, string RECID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string EMAILIDS = null, string CCEMAILIDS = null, string BCCEMAILIDS = null, string SUBJECT = null, string BODY = null, bool SENT = false, string ERR = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_EMAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@EMAILIDS", EMAILIDS);
                cmd.Parameters.AddWithValue("@CCEMAILIDS", CCEMAILIDS);
                cmd.Parameters.AddWithValue("@BCCEMAILIDS", BCCEMAILIDS);
                cmd.Parameters.AddWithValue("@SUBJECT", SUBJECT);
                cmd.Parameters.AddWithValue("@BODY", BODY);
                cmd.Parameters.AddWithValue("@SENT", SENT);
                cmd.Parameters.AddWithValue("@ERR", ERR);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_REPORTS_SP(string ACTION = null, string SITEID = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null;

                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("DM_REPORTS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_DASHBORAD_SP(string ACTION = null, string CountryID = null, string SiteID = null, string LISTING_ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("DM_DASHBORAD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@CountryID", CountryID);
                cmd.Parameters.AddWithValue("@SiteID", SiteID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_DDC_SP(string ACTION = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("DM_DDC_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@USERID", USERID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_VISIT_COMPLETED_VS_SDV_REPORT(string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_VISIT_COMPLETED_VS_SDV_REPORT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;

            }
            finally
            {
                ////ds.Dispose();
                con.Close();
            }
            return ds;
        }

        public DataSet DM_MANAGE_DASHBOARD_SP(string ACTION = null, string ID = null, string TYPE = null, string SEQNO = null)
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("DM_MANAGE_DASHBOARD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_SCHEDULE_SP(string ACTION = null, string WINDOWPERIOD = null, string STARTDATE = null, string STARTTIME = null, string ENDDATE = null)
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
                cmd = new SqlCommand("DM_SCHEDULE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@WINDOWPERIOD", WINDOWPERIOD);
                cmd.Parameters.AddWithValue("@STARTDATE", STARTDATE);
                cmd.Parameters.AddWithValue("@STARTTIME", STARTTIME);
                cmd.Parameters.AddWithValue("@ENDDATE", ENDDATE);
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

        public DataSet DM_SUBJECT_PROG_TRAC_SP(string INVID = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("DM_SUBJECT_PROG_TRAC_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.CommandTimeout = 0;
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

        public DataSet DM_eCRF_SP( string ACTION =null, string INVID = null, string SUBJID = null, string MODULEID = null,string ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("DM_eCRF_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }
    }
}