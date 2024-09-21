using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpecimenTracking.App_Code
{
    public class DAL_MF
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

        public DataSet SHIPMENT_MENIFEST_SP(string ACTION = null, string ID = null, string SEQNO = null, string VARIABLENAME = null, string FIELDNAME = null, string FIELD_ID = null, string CONTROLTYPE = null, string VISITNUM = null, string VISITNAME = null, string SITEID = null, string SUBJID = null, string SPECIMENID = null)
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

                cmd = new SqlCommand("SHIPMENT_MENIFEST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);

                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);

                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                cmd.Parameters.AddWithValue("@FIELD_ID", FIELD_ID);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISITNAME", VISITNAME);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SPECIMENID", SPECIMENID);

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