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

        public DataSet DATA_ENTRY_SP(string ACTION = null, string SITEID = null, string VISITNUM = null, string SID = null, string SUBJID = null, string VARIABLENAME = null)
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
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@SID", SID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);

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

    }

}