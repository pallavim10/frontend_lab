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
        public DataSet SETUP_ALIQUOT_SP(string ACTION = null, string ID = null, string SEQNO = null, string ALIQUOTID = null, string ALIQUOTTYPE = null,string ALIQUOTNUM = null, string ALIQUOTFROM = null, string ALIQUOTSEQTO = null )
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
                cmd.Parameters.AddWithValue("@ALIQUOTNUM",ALIQUOTNUM);
                cmd.Parameters.AddWithValue("@ALIQUOTFROM", ALIQUOTFROM);
                cmd.Parameters.AddWithValue("@ALIQUOTSEQTO",ALIQUOTSEQTO);
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

        public DataSet SETUP_FIELD_SP(string ACTION = null, string ID = null, string SEQNO = null, string VARIABLENAME = null, string FIELDNAME = null, string CONTROLTYPE = null, bool ISACTIVE = false, bool ISDEFAULT = false,bool FIRSTENTRY = false, bool SECONDENTRY = false, bool REPEAT = false, bool REQUIRED = false, string MAXLENGHT = null)
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
                cmd.Parameters.AddWithValue("@FIRSTENTRY", FIRSTENTRY);
                cmd.Parameters.AddWithValue("@SECONDENTRY", SECONDENTRY);
                cmd.Parameters.AddWithValue("@MAXLENGHT", MAXLENGHT);
                cmd.Parameters.AddWithValue("@REPEAT", REPEAT);
                cmd.Parameters.AddWithValue("@REQUIRED", REQUIRED);
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

        public DataSet SETUP_UPLOADMASTER_SP(string ACTION = null, string ID = null, string SID = null, string SUBJECTID = null,string SITEID = null,string SEQNO = null, string SLOTNO = null, string BOXNO = null)
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

                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@SLOTNO", SLOTNO);
                cmd.Parameters.AddWithValue("@BOXNO", BOXNO);

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

        public DataSet SETUP_SHIPMENT_MANIFEST_SP(string ACTION = null, string ID = null, string FILENAME = null, string CONTENT_TYPE = null, byte[] DATA_TYPE = null, string SIZE =null)
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

                cmd = new SqlCommand("SETUP_SHIPMENT_MANIFEST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FILENAME", FILENAME);
                cmd.Parameters.AddWithValue("@CONTENT_TYPE", CONTENT_TYPE);
                cmd.Parameters.AddWithValue("@DATA_TYPE", DATA_TYPE);
                cmd.Parameters.AddWithValue("@SIZE", SIZE);
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

        public DataSet SETUP_CRIT_SP(string ACTION = null, string ID = null, string SEQNO = null,string DESCRIPTION = null, string VARIABLENAME = null, string FIELDNAME = null, string AndOr1 = null, string AndOr2 = null, string AndOr3 = null, string AndOr4 = null,string Condition1 = null, string Condition2 = null, string Condition3 = null, string Condition4 = null, string Condition5 = null,string CritCode = null, string Criteria = null,  string Field1 = null,string Field2 = null, string Field3 = null, string Field4 = null, string Field5 = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null)
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

                cmd = new SqlCommand("SETUP_CRIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@DESCRIPTION", DESCRIPTION);

                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);

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

                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);

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
        //public DataTable GET_BindAliquot(string Action)
        //{
        //        DataSet ds = new DataSet();
        //        using (SqlCommand cmd = new SqlCommand("VISIT_ALIQUOT_MAPPING", con)) 
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Action", Action);
        //            cmd.Connection = con;
        //            if (con.State == ConnectionState.Open) { con.Close(); }
        //            con.Open();
        //            DataTable dt = new DataTable();
        //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) 
        //            {
        //                sda.Fill(dt);
        //            }

        //            return dt;
        //        }

            

        //}
        public DataSet Visit_Aliquot_Mapping(string ACTION, string VISITID = null, string ALIQUOTID = null, bool ADDED = false) 
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd;
                SqlDataAdapter adp;
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

                cmd = new SqlCommand("VISIT_ALIQUOT_MAPPING", con);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", ACTION);
                    cmd.Parameters.AddWithValue("@VisitID", VISITID);
                    cmd.Parameters.AddWithValue("@AliquotID", ALIQUOTID);
                    cmd.Parameters.AddWithValue("@Added", ADDED);
                    adp = new SqlDataAdapter(cmd);
                    
                        adp.Fill(ds);
                    cmd.Dispose();
                }
                return ds;
            }
            catch (Exception ex) 
            {
               string error = ex.StackTrace.ToString();
                return ds;
                
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }

        }

        public DataSet SETUP_BOX_MASTER_SP(string ACTION = null, string ID = null, string SITE_ID = null, string BOXFROM = null, string BOXTO = null, string SLOTR_X = null, string SLOTC_Y = null)
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

                cmd = new SqlCommand("SETUP_BOX_SLOT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SITE_ID", SITE_ID);
                cmd.Parameters.AddWithValue("@BOXFROM", BOXFROM);
                cmd.Parameters.AddWithValue("@BOXTO", BOXTO);
                cmd.Parameters.AddWithValue("@SLOTR_X", SLOTR_X);
                cmd.Parameters.AddWithValue("@SLOTC_Y", SLOTC_Y);
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