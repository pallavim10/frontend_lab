using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CTMS.CommonFunction
{
    public class DAL_SAE
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);

        public DataSet SAE_GET_SERIOUS_EVENTS_SP(string INVID = null, string SUBJID = null, string LISTID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_GET_SERIOUS_EVENTS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@LISTID", LISTID);
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

        public DataSet SAE_INSTRUCTION_SP(string MODULEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_INSTRUCTION_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
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

        public DataSet SAE_PREV_NEXT_MODULE_LOGS_SP(string MODULEID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_PREV_NEXT_MODULE_LOGS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
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

        public DataSet SAE_PAGE_STATUS_SP(string ACTION = null, string SAEID = null, string RECID = null, string MODULEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_PAGE_STATUS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
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

        public DataSet SAE_MODULE_SP(string ACTION = null, string MODULEID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_MODULE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
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

        public DataSet SAE_STRUCTURE_SP(string ACTION = null, string MODULEID = null, string FIELDID = null, string DM_PVID = null, string SAEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string MEDAUTH_FIELD = null;
                if (HttpContext.Current.Session["MEDAUTH_FIELD"] != null)
                {
                    MEDAUTH_FIELD = HttpContext.Current.Session["MEDAUTH_FIELD"].ToString();
                }

                cmd = new SqlCommand("SAE_STRUCTURE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
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

        public DataSet SAE_MODULE_DATA_SP(string SUBJID = null, string MODULEID = null, string TABLENAME = null, string SAEID = null, string RECID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_MODULE_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
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

        public DataSet SAE_MODULE_DATA_MR_SP(string SUBJID = null, string MODULEID = null, string TABLENAME = null, string SAEID = null, string RECID = null, string STATUS = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_MODULE_DATA_MR_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
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

        public DataSet SAE_SYNC_MODULE_DATA_SP(string MODULEID = null, string TABLENAME = null, string DM_PVID = null, string DM_RECID = null, string SAEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_SYNC_MODULE_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@DM_RECID", DM_RECID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
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

        public DataSet SAE_OPTIONS_DATA_SP(string ACTION = null, string VARIABLENAME = null, string TABLENAME = null, string SAEID = null, string RECID = null, string MODULEID = null, string SUBJID = null, string VALUE = null, string ID = null, string DEFAULTVAL = null, string SEQNO = null, string OLD_OPTIONTEXT = null, string VISITNUM = null, string ParentANS = null)
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
                cmd = new SqlCommand("SAE_OPTIONS_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VALUE", VALUE);

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@DEFAULTVAL", DEFAULTVAL);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@OLD_OPTIONTEXT", OLD_OPTIONTEXT);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
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
                cmd.Parameters.AddWithValue("@ParentANS", ParentANS);

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

        public DataSet SAE_SYNC_AUDITTRAIL_SP(string SAEID = null, string RECID = null, string SAE = null, string INVID = null, string SUBJID = null, string STATUS = null, string DM_PVID = null, string DM_RECID = null, string SYNCED_VARIABLENAMES = null, string MODULEID = null)
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

                cmd = new SqlCommand("SAE_SYNC_AUDITTRAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@DM_RECID", DM_RECID);
                cmd.Parameters.AddWithValue("@SYNCED_VARIABLENAMES", SYNCED_VARIABLENAMES);
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

        public DataSet SAE_INSERT_MODULE_DATA_SP(string ACTION = null, string TABLENAME = null, string STATUS = null, string SAEID = null, string RECID = null, string INSERTQUERY = null, string UPDATEQUERY = null, string INVID = null, string SUBJID = null, string MODULEID = null, int IsComplete = 0, bool IsMissing = false, string SAE = null, string PAGESTATUS = null, string DM_PVID = null, string DM_RECID = null, string MODULENAME = null)
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

                cmd = new SqlCommand("SAE_INSERT_MODULE_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@IsComplete", IsComplete);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@PAGESTATUS", PAGESTATUS);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@DM_RECID", DM_RECID);
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

        public DataSet SAE_INSERT_PV_SP(string ACTION = null, string SAEID = null, string RECID = null, string SAE = null, string INVID = null, string SUBJID = null, string STATUS = null, string MODULEID = null, bool HasMissing = false, string PAGESTATUS = null, int IsComplete = 0, string DM_PVID = null, string DM_RECID = null, string TABLENAME = null)
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

                cmd = new SqlCommand("SAE_INSERT_PV_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@HasMissing", HasMissing);
                cmd.Parameters.AddWithValue("@PAGESTATUS", PAGESTATUS);
                cmd.Parameters.AddWithValue("@IsComplete", IsComplete);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@DM_RECID", DM_RECID);
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

        public DataSet SAE_CHECK_DUPLICATE_SP(string TEMPID = null, string SUBJID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_CHECK_DUPLICATE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TEMPID", TEMPID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
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

        public DataSet SAE_GENERAL_SP(string ACTION = null, string INVID = null, string SUBJID = null, string DM_PVID = null, string STATUS = null, string SAEID = null, string MODULEID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_GENERAL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
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

        public DataSet SAE_UPDATE_TEMPID_SP(string SAE = null, string SAEID = null, string RECID = null, string TEMPID = null, string DM_PVID = null, string DM_RECID = null, string TABLENAME = null, string INVID = null, string SUBJID = null, string MODULEID = null, string MODULENAME = null)
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

                cmd = new SqlCommand("SAE_UPDATE_TEMPID_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@TEMPID", TEMPID);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@DM_RECID", DM_RECID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
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

        public DataSet SAE_GET_EMAIL_DATA_SP(string SAEID = null, string ACTIONS = null, string INVID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_GET_EMAIL_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@ACTIONS", ACTIONS);
                cmd.Parameters.AddWithValue("@INVID", INVID);
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

        public DataSet SAE_AUDITTRAIL_SP(string ACTION = null, string SAEID = null, string RECID = null, string SAE = null, string INVID = null, string SUBJID = null, string STATUS = null, string VARIABLENAME = null, string FIELDNAME = null, string REASON = null, string MODULEID = null, string MODULENAME = null)
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

                cmd = new SqlCommand("SAE_AUDITTRAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@REASON", REASON);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
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

        public DataSet SAE_COMMENT_SP(string ACTION = null, string SAEID = null, string RECID = null, string INVID = null, string SUBJID = null, string MODULEID = null, string MODULENAME = null, string FIELDNAME = null, string VARIABLENAME = null, string COMMENT = null, string SAE = null, string STATUS = null)
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

                cmd = new SqlCommand("SAE_COMMENT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@COMMENT", COMMENT);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
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

        public DataSet SAE_STATUS_LIST_SP(string ACTION = null, string INVID = null, string SUBJID = null, string STATUS = null, string SAEID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_STATUS_LIST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
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

        public DataSet SAE_OPEN_CRF_SP(string ACTION = null, string SAEID = null, string STATUS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_OPEN_CRF_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
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

        public DataSet SAE_SIGNOFF_SP(string ACTION = null, string SAEID = null, string RECID = null, string SAE = null, string INVID = null, string SUBJID = null, string MODULEID = null, string STATUS = null)
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

                cmd = new SqlCommand("SAE_SIGNOFF_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
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

        public DataSet SAE_DELAY_REASONE_SP(string ACTION = null, string SAEID = null, string REASON = null)
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

                cmd = new SqlCommand("SAE_DELAY_REASONE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@REASON", REASON);
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

        public DataSet SAE_QUERY_SP(string ACTION = null, string SAEID = null, string RECID = null, string VARIABLENAME = null, string ID = null, string Comment = null, string REASON = null, string MODULEID = null, string QUERYTEXT = null)
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

                cmd = new SqlCommand("SAE_QUERY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
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

        public DataSet SAE_MULTIPLE_RECORS_SP(string ACTION = null, string SAEID = null, string RECID = null, string SUBJID = null, string STATUS = null, string MODULEID = null, string TABLENAME = null, string VARIABLENAME = null)
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

                cmd = new SqlCommand("SAE_MULTIPLE_RECORS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
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

        public DataSet SAE_RECID_SP(string SAEID = null, string SUBJID = null, string TABLENAME = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_RECID_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
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

        public DataSet SAE_COMPLETE_DATA_SP(string SAEID = null, string RECID = null, string MODULEID = null)
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

                cmd = new SqlCommand("SAE_COMPLETE_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
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

        public DataSet SAE_DELETE_RECORDS_SP(string SAEID = null, string RECID = null, string SAE = null, string STATUS = null, string INVID = null, string SUBJID = null, string MODULEID = null, string REASON = null)
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

                cmd = new SqlCommand("SAE_DELETE_RECORDS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@REASON", REASON);
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
                throw;
            }
            finally
            {
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet SAE_SYNC_MULTIPLE_MODULE_DATA_SP(string MODULEID = null, string SAEID = null, string DM_PVID = null, string DM_RECID = null, string DM_TABLENAME = null, string SAE_TABLENAME = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_SYNC_MULTIPLE_MODULE_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@DM_RECID", DM_RECID);
                cmd.Parameters.AddWithValue("@DM_TABLENAME", DM_TABLENAME);
                cmd.Parameters.AddWithValue("@SAE_TABLENAME", SAE_TABLENAME);
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

        public DataSet SAE_INSERT_MULTIPLE_SYNC_DATA_SP(string SAEID = null, string RECID = null, string SAE = null, string INVID = null, string SUBJID = null, string STATUS = null, string DM_PVID = null, string DM_RECID = null, string TABLENAME = null, string MODULEID = null, int IsComplete = 0, bool IsMissing = false, string PAGESTATUS = null, string MODULENAME = null)
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

                cmd = new SqlCommand("SAE_INSERT_MULTIPLE_SYNC_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@DM_RECID", DM_RECID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@IsComplete", IsComplete);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@PAGESTATUS", PAGESTATUS);
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

        public DataSet SAE_UPDATE_MULTIPLE_SYNC_DATA_SP(string SAEID = null, string RECID = null, string SAE = null, string INVID = null, string SUBJID = null, string STATUS = null, string DM_PVID = null, string DM_RECID = null, string TABLENAME = null, string MODULEID = null, int IsComplete = 0, bool IsMissing = false, string PAGESTATUS = null, string MODULENAME = null)
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

                cmd = new SqlCommand("SAE_UPDATE_MULTIPLE_SYNC_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@DM_RECID", DM_RECID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@IsComplete", IsComplete);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@PAGESTATUS", PAGESTATUS);
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

        public DataSet SAE_INSLIST_EDITABLE_SP(string ACTION = null, string MODULEID = null)
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

                cmd = new SqlCommand("SAE_INSLIST_EDITABLE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
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

        public DataSet SAE_Manual_Query_SP(string ACTION = null, string SAEID = null, string RECID = null, string INVID = null, string SUBJID = null, string QUERYTEXT = null, string MODULEID = null, string MODULENAME = null, string FIELDID = null, string FIELDNAME = null, string TABLENAME = null, string VARIABLENAME = null)
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

                cmd = new SqlCommand("SAE_Manual_Query_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
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

        public DataSet SAE_MEDICAL_REVIEW_SP(string ACTION = null, string SAEID = null, string RECID = null, string VARIABLENAME = null, bool MRYN = false, string MODULEID = null, string STATUS = null, string colChecked = null, string colUnChecked = null, string isSDVComplete = null, string MODULENAME = null, string OLDSTATUS = null)
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

                cmd = new SqlCommand("SAE_MEDICAL_REVIEW_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@MRYN", MRYN);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@OLDSTATUS", OLDSTATUS);
                cmd.Parameters.AddWithValue("@colChecked", colChecked);
                cmd.Parameters.AddWithValue("@colUnChecked", colUnChecked);
                cmd.Parameters.AddWithValue("@isSDVComplete", isSDVComplete);
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

        public DataSet SAE_CLOSE_SP(string ACTION = null, string SAEID = null, string SAE = null, string STATUS = null, string TABLENAME = null, string MODULEID = null, string REASON = null)
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

                cmd = new SqlCommand("SAE_CLOSE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@REASON", REASON);
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
                throw;
            }
            finally
            {
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet SAE_DELETED_SP(string ACTION = null, string SAEID = null, string SAE = null, string STATUS = null, string TABLENAME = null, string MODULEID = null, string REASON = null)
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

                cmd = new SqlCommand("SAE_DELETED_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@REASON", REASON);
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
                throw;
            }
            finally
            {
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet SAE_SDVDETAILS_SP(string ACTION, string SAEID = null, string RECID = null, string INVID = null, string SUBJID = null, string VARIABLENAME = null, int SDVYN = 0, string MODULEID = null, string STATUS = null, string TABLENAME = null, string colChecked = null, string colUnChecked = null, string isSDVComplete = null, string OLDSTATUS = null)
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
                cmd = new SqlCommand("SAE_SDVDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@SDVYN", SDVYN);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@OLDSTATUS", OLDSTATUS);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
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
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet SAE_REPORT_SP(string ACTION = null, string SAEID = null, string SUBJID = null, string STATUS = null, string REPORTSTATUS = null, string ID = null, string REPORT_ID = null, string TABLENAME = null, string VARIABLENAMES = null, string TYPE = null)
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

                cmd = new SqlCommand("SAE_REPORT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@REPORTSTATUS", REPORTSTATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@REPORT_ID", REPORT_ID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAMES", VARIABLENAMES);
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
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

        public DataSet SAE_SETUP_SP(string ACTION = null, string ID = null, string SAE_MODULEID = null, string SAE_FIELDID = null,
             string DM_MODULEID = null, string DM_FIELDID = null, string SAE_TYPE = null, string MODULEID = null, string SEQNO = null,
             string CONTROLTYPE = null, string CLASS = null, string DATATYPE = null, string FIELDNAME = null, string VARIABLENAME = null,
             string MAXLEN = null, string DEFAULTVAL = null, bool BOLDYN = false, bool UNLNYN = false, bool READYN = false, bool MULTILINEYN = false,
             string ALLVISYN = null, bool UPPERCASE = false, string CONTINUE = null, bool REQUIREDYN = false, bool INVISIBLE = false,
             string FieldColor = null, string AnsColor = null, bool InList = false, bool InListEditable = false, bool LabData = false, string Refer = null,
             bool AutoNum = false, bool Prefix = false, string PrefixText = null, bool Critic_DP = false, string Descrip = null,
             bool NONREPETATIVE = false, bool MANDATORY = false, bool HelpData = false, bool DUPLICATE = false, bool MEDICAL_REVIEW = false,
             string INVID = null, string EMAILID = null, string CC_EMAILID = null, string BCC_EMAILID = null, string EMAIL_BODY = null, string EMAIL_SUBJECT = null,
             string ACTIONS = null, string SAE_TYPE_MODULEID = null, string DefaultData = null, bool ParentLinked = false, bool ChildLinked = false, bool MEDOP = false,
             string MODULENAME = null, string TABLENAME = null, string SPID_FIELDID = null, string SPID_VARIABLENAME = null, string SPID_FIELDNAME = null, string TERM_FIELDID = null, string TERM_VARIABLENAME = null, string TERM_FIELDNAME = null, string DELAY_FIELDID = null, string DELAY_VARIABLENAME = null, string DELAY_FIELDNAME = null
             , string REPORT_NAME = null, bool INTERNAL = false, bool SITE = false, string FILENAME = null, string CONTENT_TYPE = null, byte[] DATA_TYPE = null, string SIZE = null, string REPORT_ID = null, string TIME_FIELDID = null, string TIME_VARIABLENAME = null, string TIME_FIELDNAME = null)
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
                cmd = new SqlCommand("SAE_SETUP_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SAE_MODULEID", SAE_MODULEID);

                cmd.Parameters.AddWithValue("@SAE_FIELDID", SAE_FIELDID);
                cmd.Parameters.AddWithValue("@DM_MODULEID", DM_MODULEID);
                cmd.Parameters.AddWithValue("@DM_FIELDID", DM_FIELDID);
                cmd.Parameters.AddWithValue("@SAE_TYPE", SAE_TYPE);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@CLASS", CLASS);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@DEFAULTVAL", DEFAULTVAL);
                cmd.Parameters.AddWithValue("@BOLDYN", BOLDYN);
                cmd.Parameters.AddWithValue("@UNLNYN", UNLNYN);
                cmd.Parameters.AddWithValue("@READYN", READYN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@ALLVISYN", ALLVISYN);
                cmd.Parameters.AddWithValue("@UPPERCASE", UPPERCASE);
                cmd.Parameters.AddWithValue("@CONTINUE", CONTINUE);
                cmd.Parameters.AddWithValue("@REQUIREDYN", REQUIREDYN);
                cmd.Parameters.AddWithValue("@INVISIBLE", INVISIBLE);
                cmd.Parameters.AddWithValue("@FieldColor", FieldColor);
                cmd.Parameters.AddWithValue("@AnsColor", AnsColor);
                cmd.Parameters.AddWithValue("@InList", InList);
                cmd.Parameters.AddWithValue("@InListEditable", InListEditable);
                cmd.Parameters.AddWithValue("@LabData", LabData);
                cmd.Parameters.AddWithValue("@Refer", Refer);
                cmd.Parameters.AddWithValue("@AutoNum", AutoNum);
                cmd.Parameters.AddWithValue("@Prefix", Prefix);

                cmd.Parameters.AddWithValue("@PrefixText", PrefixText);
                cmd.Parameters.AddWithValue("@Critic_DP", Critic_DP);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                cmd.Parameters.AddWithValue("@NONREPETATIVE", NONREPETATIVE);
                cmd.Parameters.AddWithValue("@MANDATORY", MANDATORY);
                cmd.Parameters.AddWithValue("@HelpData", HelpData);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);
                cmd.Parameters.AddWithValue("@MEDICAL_REVIEW", MEDICAL_REVIEW);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);

                cmd.Parameters.AddWithValue("@REPORT_NAME", REPORT_NAME);
                cmd.Parameters.AddWithValue("@INTERNAL", INTERNAL);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@FILENAME", FILENAME);
                cmd.Parameters.AddWithValue("@CONTENT_TYPE", CONTENT_TYPE);
                cmd.Parameters.AddWithValue("@DATA_TYPE", DATA_TYPE);
                cmd.Parameters.AddWithValue("@SIZE", SIZE);
                cmd.Parameters.AddWithValue("@REPORT_ID", REPORT_ID);

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
                cmd.Parameters.AddWithValue("@CC_EMAILID", CC_EMAILID);
                cmd.Parameters.AddWithValue("@BCC_EMAILID", BCC_EMAILID);
                cmd.Parameters.AddWithValue("@EMAIL_BODY", EMAIL_BODY);
                cmd.Parameters.AddWithValue("@EMAIL_SUBJECT", EMAIL_SUBJECT);
                cmd.Parameters.AddWithValue("@ACTIONS", ACTIONS);
                cmd.Parameters.AddWithValue("@SAE_TYPE_MODULEID", SAE_TYPE_MODULEID);

                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);
                cmd.Parameters.AddWithValue("@ParentLinked", ParentLinked);
                cmd.Parameters.AddWithValue("@ChildLinked", ChildLinked);
                cmd.Parameters.AddWithValue("@MEDOP", MEDOP);

                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@SPID_FIELDID", SPID_FIELDID);
                cmd.Parameters.AddWithValue("@SPID_VARIABLENAME", SPID_VARIABLENAME);
                cmd.Parameters.AddWithValue("@SPID_FIELDNAME", SPID_FIELDNAME);
                cmd.Parameters.AddWithValue("@TERM_FIELDID", TERM_FIELDID);
                cmd.Parameters.AddWithValue("@TERM_VARIABLENAME", TERM_VARIABLENAME);
                cmd.Parameters.AddWithValue("@TERM_FIELDNAME", TERM_FIELDNAME);

                cmd.Parameters.AddWithValue("@DELAY_FIELDID", DELAY_FIELDID);
                cmd.Parameters.AddWithValue("@DELAY_VARIABLENAME", DELAY_VARIABLENAME);
                cmd.Parameters.AddWithValue("@DELAY_FIELDNAME", DELAY_FIELDNAME);

                cmd.Parameters.AddWithValue("@TIME_FIELDID", TIME_FIELDID);
                cmd.Parameters.AddWithValue("@TIME_VARIABLENAME", TIME_VARIABLENAME);
                cmd.Parameters.AddWithValue("@TIME_FIELDNAME", TIME_FIELDNAME);

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

        public DataSet SAE_SUPPORTING_DOC_SP(string ACTION = null, string ID = null, string SAEID = null, string Notes = null, string FileName = null, string ContentType = null, byte[] fileData = null, string INVID = null, string SUBJID = null, string STATUS = null)
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
                cmd = new SqlCommand("SAE_SUPPORTING_DOC_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Notes", Notes);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@fileData", fileData);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
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

        public DataSet SAE_SINGLE_RECORS_SP(string ACTION = null, string SAEID = null, string RECID = null, string SUBJID = null, string MODULEID = null, string TABLENAME = null, string VARIABLENAME = null, string STATUS = null)
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

                cmd = new SqlCommand("SAE_SINGLE_RECORS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
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

        public DataSet SAE_PRINT_REPORT_SP(string ACTION = null, string SITEID = null, string SUBJID = null, string SAEID = null, string RECID = null, string STATUS = null, string MODULEID = null, string FIELDID = null, string VARIABLENAME = null, string TABLENAME = null, string DefaultData = null)
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

                cmd = new SqlCommand("SAE_PRINT_REPORT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);

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

        public DataSet SAE_DASHBORAD_SP(string ACTION = null, string CountryID = null, string SiteID = null, string LISTING_ID = null)
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

                cmd = new SqlCommand("SAE_DASHBORAD_SP", con);
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

        public DataSet SAE_DB_AUDIT_TRAIL_SP(string ID = null, string ACTION = null, string TABLENAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_AUDIT_TRAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);

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

        public DataSet SAE_Specs_Module_Wise_SP(string ACTION = null, string MODULEID = null, string FORMULA = null, string CritCode = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_Specs_Module_Wise_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FORMULA", FORMULA);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);

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
