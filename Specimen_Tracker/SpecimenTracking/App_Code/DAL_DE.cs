using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpecimenTracking.App_Code
{
    public class DAL_DE
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        //Get Connection
        DataSet ds;
        SqlCommand cmd;
        SqlDataAdapter adp;

        public string getconstr()
        {
            return ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        }

        public DataSet DATA_ENTRY_SP(string ACTION = null, string ID = null, string SITEID = null, string VISITNUM = null, string VISIT = null, string SID = null, string SUBJID = null, string VARIABLENAME = null, string INSERTQUERY = null, string UPDATEQUERY = null, string ALQID = null, string ALIQUOTID = null, string ALIQUOTTYPE = null, string ALIQUOTNO_CONCAT = null, string ALIQUOTNO = null, string BOXNO = null, string SLOTNO = null, string SPECTYP = null, string FIELDNAME = null, string COLUMNNAME = null, string OLDVAL = null, string NEWVAL = null, string Reason = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string UserID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    UserID = HttpContext.Current.Session["USER_ID"].ToString();
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

                cmd = new SqlCommand("DATA_ENTRY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@SID", SID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);

                cmd.Parameters.AddWithValue("@ALQID", ALQID);
                cmd.Parameters.AddWithValue("@ALIQUOTID", ALIQUOTID);
                cmd.Parameters.AddWithValue("@ALIQUOTTYPE", ALIQUOTTYPE);
                cmd.Parameters.AddWithValue("@ALIQUOTNO_CONCAT", ALIQUOTNO_CONCAT);
                cmd.Parameters.AddWithValue("@ALIQUOTNO", ALIQUOTNO);
                cmd.Parameters.AddWithValue("@BOXNO", BOXNO);
                cmd.Parameters.AddWithValue("@SLOTNO", SLOTNO);
                cmd.Parameters.AddWithValue("@SPECTYP", SPECTYP);

                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@COLUMNNAME", COLUMNNAME);
                cmd.Parameters.AddWithValue("@OLDVAL", OLDVAL);
                cmd.Parameters.AddWithValue("@NEWVAL", NEWVAL);
                cmd.Parameters.AddWithValue("@Reason", Reason);

                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet GET_SITEID_SP(string USERID = null, string COUNTRYID = null)
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

                cmd = new SqlCommand("GET_SITEID_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
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

        public DataSet GET_SUBJID_SP(string SITEID)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("GET_SUBJID_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
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

        public DataSet CRITERIA_SP(string ACTION = null, string Condition = null, string DESCRIPTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("CRITERIA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@Condition", Condition);
                cmd.Parameters.AddWithValue("@DESCRIPTION", DESCRIPTION);
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

        public DataSet DATA_ENTRYLIST_SP(string ACTION = null, string SITEID = null, string VISITNUM = null, string SID = null, string SUBJID = null, string VARIABLENAME = null, string ACKNOWLEDGEMENT = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string UserID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    UserID = HttpContext.Current.Session["USER_ID"].ToString();
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

                cmd = new SqlCommand("DATA_ENTRYLIST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@SID", SID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ACKNOWLEDGEMENT", ACKNOWLEDGEMENT);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DE_LOG_SP(string ACTION = null, string FROMDATE = null, string TODATE = null, string USERNAME = null, string TABLENAME = null, string ID = null)
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
                cmd = new SqlCommand("DE_LOG_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE);
                cmd.Parameters.AddWithValue("@TODATE", TODATE);
                cmd.Parameters.AddWithValue("@USERNAME", USERNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@ID", ID);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }
    }

}