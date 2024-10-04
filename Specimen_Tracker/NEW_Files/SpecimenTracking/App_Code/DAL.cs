using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpecimenTracking.App_Code
{
    public class DAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        //Get Connection
        DataSet ds;
        SqlCommand cmd;
        SqlDataAdapter adp;

        public DataSet EMAIL_INTEGRATION(string ACTION = null, string DISPLAYNAME = null, string HOSTNAME = null, string PORTNO = null, string USERNAME = null, string PASSWORD = null, string ID = null,
            bool SSL = false, string IPADDRESS = null)
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

                cmd = new SqlCommand("EMAIL_INTEGRATION_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@DISPLAYNAME", DISPLAYNAME);
                cmd.Parameters.AddWithValue("@HOSTNAME", HOSTNAME);
                cmd.Parameters.AddWithValue("@PORTNO", PORTNO);
                cmd.Parameters.AddWithValue("@USERNAME", USERNAME);
                cmd.Parameters.AddWithValue("@PASSWORD", PASSWORD);
                cmd.Parameters.AddWithValue("@SSL", SSL);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);

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

        public DataSet UMT_EMAIL_SP(string ID = null, string ACTIONS = null, string ACTION = null, string To = null, string CC = null, string BCC = null, string Email_Code = null, string SITEID = null)
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

                cmd = new SqlCommand("UMT_EMAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ACTIONS", ACTIONS);
                cmd.Parameters.AddWithValue("@To", To);
                cmd.Parameters.AddWithValue("@CC", CC);
                cmd.Parameters.AddWithValue("@BCC", BCC);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@Email_Code", Email_Code);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet EMAIL_SP(string ACTION = null, string ID = null, string EMAILIDS = null, string CCEMAILIDS = null
            , string BCCEMAILIDS = null, string SUBJECT = null, string BODY = null, bool SENT = false, string ERR = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("EMAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
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