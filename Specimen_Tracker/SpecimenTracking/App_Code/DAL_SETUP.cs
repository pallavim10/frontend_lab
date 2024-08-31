using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpecimenTracking.App_Code
{
    public class DAL_SETUP
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


        public DataSet SETUP_VISIT_SP(string ACTION = null, string ID = null,string VISITNUM = null, string VISITNAME = null)
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

                cmd = new SqlCommand("SETUP_VISIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISITNAME", VISITNAME);
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

        public DataSet SETUP_LOGS_SP(string ACTION = null, string FROMDATE = null, string TODATE = null, string USERNAME = null, string TABLENAME = null, string ID = null)
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
                cmd = new SqlCommand("SETUP_LOGS_SP", con);
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
        public DataSet SETUP_ALIQUOT_SP(string ACTION = null, string ID = null, string SEQNO = null, string ALIQUOTID = null, string ALIQUOTTYPE = null )
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

                cmd = new SqlCommand("SETUP_ALIQUOT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@ALIQUOTID", ALIQUOTID);
                cmd.Parameters.AddWithValue("@ALIQUOTTYPE", ALIQUOTTYPE);
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

        public DataSet SETUP_FIELD_SP(string ACTION = null, string ID = null, string SEQNO = null, string VARIABLENAME = null, string FIELDNAME = null, string CONTROLTYPE = null, bool ISACTIVE = false, bool ISDEFAULT = false)
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

                cmd = new SqlCommand("SETUP_FIELD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@ISACTIVE", ISACTIVE);
                cmd.Parameters.AddWithValue("@ISDEFAULT", ISDEFAULT);
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
        // 

        public DataSet SETUP_FIELDOPTION_SP(string ACTION = null, string ID = null, string FIELD_ID = null, string FIELDNAME = null,string VARIABLENAME = null,string SEQNO = null,string CONTROLTYPE = null,bool ISACTIVE = false,bool ISDEFAULT = false,string OPTION = null)
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

                cmd = new SqlCommand("SETUP_FIELDOPTION_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FIELD_ID", FIELD_ID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@ISACTIVE", ISACTIVE);
                cmd.Parameters.AddWithValue("@ISDEFAULT", ISDEFAULT);
                cmd.Parameters.AddWithValue("@OPTION", OPTION);
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

        public DataSet SETUP_UPLOADMASTER_SP(string ACTION = null, string ID = null, string SID = null, string SUBJECTID = null,string SITEID = null)
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

                cmd = new SqlCommand("SETUP_UPLOADMASTER_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SID", SID);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
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
    }
}