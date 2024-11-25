using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataTransferSystem.App_Code
{
    public class DAL_UPLOAD_TEMPLATE
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
        public DataSet UPLOAD_TEMPLATE_SP(string ACTION = null, string ID = null, string FILENAME = null, string CONTENT_TYPE = null, byte[] DATA_TYPE = null, string SIZE = null, string FILE_EXTENSION = null, string DATA_TRANSFER_COLUMNS = null, string TRANSFER = null)
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

                cmd = new SqlCommand("UPLOAD_TEMPLATE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FILENAME", FILENAME);
                cmd.Parameters.AddWithValue("@CONTENT_TYPE", CONTENT_TYPE);
                cmd.Parameters.AddWithValue("@DATA_TYPE", DATA_TYPE);
                cmd.Parameters.AddWithValue("@SIZE", SIZE);
                cmd.Parameters.AddWithValue("@FILE_EXTENSION", FILE_EXTENSION);
                cmd.Parameters.AddWithValue("@TRANSFER_TYPE", TRANSFER);
                cmd.Parameters.AddWithValue("@DATA_TRANSFER_COLUMNS", DATA_TRANSFER_COLUMNS);
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