using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CTMS.CommonFunction
{
    public class DAL_MM
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);

        public DataSet ANALYT_SUBJECTS(string ACTION = null, string SUBJID = null, string MODULEID = null, string LISTINGID = null)
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
                cmd = new SqlCommand("ANALYT_SUBJECTS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@LISTINGID", LISTINGID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
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

        public DataSet MM_QUERY_SP(string ACTION = null, string ID = null, string VISITNUM = null, string MODULEID = null,
     string LISTING_ID = null, string PVID = null, string QUERYTEXT = null, string QUERYTYPE = null,
     string RECID = null, string SITEID = null, string SOURCE = null, string STATUS = null,
     string SUBJID = null, string COUNTRYID = null)
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
                cmd = new SqlCommand("MM_QUERY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);

                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);

                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);

                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet MM_STATS_SP(string ACTION = null, string COMMENTS = null, string ID = null,
     string LISTING_ID = null, string PROJECT = null, string PVID = null, string QUERYTEXT = null, string QUERYTYPE = null,
     string RECID = null, string REVIEW = null, string PEER_REVIEW = null, string SITE = null, string SOURCE = null, string Status = null,
     string SUBJID = null, string USERID = null, string PRIORITY = null, string DEPT = null, string SUB_TEST = null, string LAB_TEST = null,
     string SUB_TEST_DAT = null, string VISITDAT = null, string CATEGORYID = null, string Grade = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
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
                cmd = new SqlCommand("MM_STATS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@PROJECT", PROJECT);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@REVIEW", REVIEW);
                cmd.Parameters.AddWithValue("@PEER_REVIEW", PEER_REVIEW);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@PRIORITY", PRIORITY);
                cmd.Parameters.AddWithValue("@DEPT", DEPT);
                cmd.Parameters.AddWithValue("@SUB_TEST", SUB_TEST);
                cmd.Parameters.AddWithValue("@LAB_TEST", LAB_TEST);
                cmd.Parameters.AddWithValue("@SUB_TEST_DAT", SUB_TEST_DAT);
                cmd.Parameters.AddWithValue("@VISITDAT", VISITDAT);
                cmd.Parameters.AddWithValue("@CATEGORYID", CATEGORYID);
                cmd.Parameters.AddWithValue("@Grade", Grade);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet MM_DASH_SP(string ACTION = null, string COMMENTS = null, string ID = null,
     string LISTING_ID = null, string PROJECT = null, string PVID = null, string QUERYTEXT = null, string QUERYTYPE = null,
     string RECID = null, string REVIEW = null, string PEER_REVIEW = null, string SITE = null, string SOURCE = null, string Status = null,
     string SUBJID = null, string USERID = null, string PRIORITY = null, string DEPT = null, string SUB_TEST = null, string LAB_TEST = null,
     string SUB_TEST_DAT = null, string VISITDAT = null, string CATEGORYID = null, string Grade = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
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
                cmd = new SqlCommand("MM_DASH_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@PROJECT", PROJECT);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@REVIEW", REVIEW);
                cmd.Parameters.AddWithValue("@PEER_REVIEW", PEER_REVIEW);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@PRIORITY", PRIORITY);
                cmd.Parameters.AddWithValue("@DEPT", DEPT);
                cmd.Parameters.AddWithValue("@SUB_TEST", SUB_TEST);
                cmd.Parameters.AddWithValue("@LAB_TEST", LAB_TEST);
                cmd.Parameters.AddWithValue("@SUB_TEST_DAT", SUB_TEST_DAT);
                cmd.Parameters.AddWithValue("@VISITDAT", VISITDAT);
                cmd.Parameters.AddWithValue("@CATEGORYID", CATEGORYID);
                cmd.Parameters.AddWithValue("@Grade", Grade);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataSet MM_ACTION_SP(string ACTION = null, string ID = null,
     string LISTING_ID = null, string PVID = null, string RECID = null, string SITE = null, string SOURCE = null, string Status = null,
     string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null, UserGroup_ID = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                if (HttpContext.Current.Session["UserGroupID"] != null)
                {
                    UserGroup_ID = HttpContext.Current.Session["UserGroupID"].ToString();
                }

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("MM_ACTION_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@UserGroup_ID", UserGroup_ID);
                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataSet MM_COMMENT_SP(string ACTION = null, string COMMENTS = null, string ID = null,
     string LISTING_ID = null, string PVID = null, string QUERYTYPE = null, string RECID = null, string SOURCE = null, string COUNTRYID = null, string INVID = null, string SUBJID = null)
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
                cmd = new SqlCommand("MM_COMMENT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet MM_DATA_SP(string ACTION = null, string COMMENTS = null, string ID = null,
     string LISTING_ID = null, string PROJECT = null, string PVID = null, string QUERYTEXT = null, string QUERYTYPE = null,
     string RECID = null, string REVIEW = null, string PEER_REVIEW = null, string SITE = null, string SOURCE = null, string Status = null,
     string SUBJID = null, string USERID = null, string PRIORITY = null, string DEPT = null, string SUB_TEST = null, string LAB_TEST = null,
     string SUB_TEST_DAT = null, string VISITDAT = null, string CATEGORYID = null, string Grade = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
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
                cmd = new SqlCommand("MM_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@PROJECT", PROJECT);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@REVIEW", REVIEW);
                cmd.Parameters.AddWithValue("@PEER_REVIEW", PEER_REVIEW);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@PRIORITY", PRIORITY);
                cmd.Parameters.AddWithValue("@DEPT", DEPT);
                cmd.Parameters.AddWithValue("@SUB_TEST", SUB_TEST);
                cmd.Parameters.AddWithValue("@LAB_TEST", LAB_TEST);
                cmd.Parameters.AddWithValue("@SUB_TEST_DAT", SUB_TEST_DAT);
                cmd.Parameters.AddWithValue("@VISITDAT", VISITDAT);
                cmd.Parameters.AddWithValue("@CATEGORYID", CATEGORYID);
                cmd.Parameters.AddWithValue("@Grade", Grade);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet MM_LAB_ADD_SP(string ACTION = null, string COMMENTS = null, string ID = null,
     string LISTING_ID = null, string PROJECT = null, string PVID = null, string QUERYTEXT = null, string QUERYTYPE = null,
     string RECID = null, string REVIEW = null, string PEER_REVIEW = null, string SITE = null, string SOURCE = null, string Status = null,
     string SUBJID = null, string USERID = null, string PRIORITY = null, string DEPT = null, string SUB_TEST = null, string LAB_TEST = null,
     string SUB_TEST_DAT = null, string VISITDAT = null, string CATEGORYID = null, string Grade = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
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
                cmd = new SqlCommand("MM_LAB_ADD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@PROJECT", PROJECT);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@REVIEW", REVIEW);
                cmd.Parameters.AddWithValue("@PEER_REVIEW", PEER_REVIEW);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@PRIORITY", PRIORITY);
                cmd.Parameters.AddWithValue("@DEPT", DEPT);
                cmd.Parameters.AddWithValue("@SUB_TEST", SUB_TEST);
                cmd.Parameters.AddWithValue("@LAB_TEST", LAB_TEST);
                cmd.Parameters.AddWithValue("@SUB_TEST_DAT", SUB_TEST_DAT);
                cmd.Parameters.AddWithValue("@VISITDAT", VISITDAT);
                cmd.Parameters.AddWithValue("@CATEGORYID", CATEGORYID);
                cmd.Parameters.AddWithValue("@Grade", Grade);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet MM_LAB_SP(string ACTION = null, string COMMENTS = null, string ID = null,
     string LISTING_ID = null, string PROJECT = null, string PVID = null, string QUERYTEXT = null, string QUERYTYPE = null,
     string RECID = null, string REVIEW = null, string PEER_REVIEW = null, string SITE = null, string SOURCE = null, string Status = null,
     string SUBJID = null, string USERID = null, string PRIORITY = null, string DEPT = null, string SUB_TEST = null, string LAB_TEST = null,
     string SUB_TEST_DAT = null, string VISITDAT = null, string CATEGORYID = null, string Grade = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
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
                cmd = new SqlCommand("MM_LAB_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@PROJECT", PROJECT);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@REVIEW", REVIEW);
                cmd.Parameters.AddWithValue("@PEER_REVIEW", PEER_REVIEW);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@PRIORITY", PRIORITY);
                cmd.Parameters.AddWithValue("@DEPT", DEPT);
                cmd.Parameters.AddWithValue("@SUB_TEST", SUB_TEST);
                cmd.Parameters.AddWithValue("@LAB_TEST", LAB_TEST);
                cmd.Parameters.AddWithValue("@SUB_TEST_DAT", SUB_TEST_DAT);
                cmd.Parameters.AddWithValue("@VISITDAT", VISITDAT);
                cmd.Parameters.AddWithValue("@CATEGORYID", CATEGORYID);
                cmd.Parameters.AddWithValue("@Grade", Grade);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet MM_LAB_ACTION_SP(string ACTION = null, string COMMENTS = null, string ID = null,
     string LISTING_ID = null, string PROJECT = null, string PVID = null, string QUERYTEXT = null, string QUERYTYPE = null,
     string RECID = null, string REVIEW = null, string PEER_REVIEW = null, string SITE = null, string SOURCE = null, string Status = null,
     string SUBJID = null, string USERID = null, string PRIORITY = null, string DEPT = null, string SUB_TEST = null, string LAB_TEST = null,
     string SUB_TEST_DAT = null, string VISITDAT = null, string CATEGORYID = null, string Grade = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
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
                cmd = new SqlCommand("MM_LAB_ACTION_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@PROJECT", PROJECT);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@REVIEW", REVIEW);
                cmd.Parameters.AddWithValue("@PEER_REVIEW", PEER_REVIEW);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@PRIORITY", PRIORITY);
                cmd.Parameters.AddWithValue("@DEPT", DEPT);
                cmd.Parameters.AddWithValue("@SUB_TEST", SUB_TEST);
                cmd.Parameters.AddWithValue("@LAB_TEST", LAB_TEST);
                cmd.Parameters.AddWithValue("@SUB_TEST_DAT", SUB_TEST_DAT);
                cmd.Parameters.AddWithValue("@VISITDAT", VISITDAT);
                cmd.Parameters.AddWithValue("@CATEGORYID", CATEGORYID);
                cmd.Parameters.AddWithValue("@Grade", Grade);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet MM_LISTINGS_SP(string ACTION = null, string COMMENTS = null, string ID = null,
     string LISTING_ID = null, string PROJECT = null, string PVID = null, string QUERYTEXT = null, string QUERYTYPE = null,
     string RECID = null, string REVIEW = null, string PEER_REVIEW = null, string SITE = null, string SOURCE = null, string Status = null,
     string SUBJID = null, string USERID = null, string PRIORITY = null, string DEPT = null, string SUB_TEST = null, string LAB_TEST = null,
     string SUB_TEST_DAT = null, string VISITDAT = null, string CATEGORYID = null, string Grade = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
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
                cmd = new SqlCommand("MM_LISTINGS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@PROJECT", PROJECT);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@REVIEW", REVIEW);
                cmd.Parameters.AddWithValue("@PEER_REVIEW", PEER_REVIEW);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@PRIORITY", PRIORITY);
                cmd.Parameters.AddWithValue("@DEPT", DEPT);
                cmd.Parameters.AddWithValue("@SUB_TEST", SUB_TEST);
                cmd.Parameters.AddWithValue("@LAB_TEST", LAB_TEST);
                cmd.Parameters.AddWithValue("@SUB_TEST_DAT", SUB_TEST_DAT);
                cmd.Parameters.AddWithValue("@VISITDAT", VISITDAT);
                cmd.Parameters.AddWithValue("@CATEGORYID", CATEGORYID);
                cmd.Parameters.AddWithValue("@Grade", Grade);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet MM_LIST_SP(string ACTION = null, string ID = null, string LISTING_ID = null, string PREV_LISTID = null, string OnClickFilter = null, string FIELDNAME = null, string MODULEID = null, string FIELDID = null, string COUNTRYID = null, string INVID = null, string SUBJID = null, string STATUS = null, string PVID = null, string RECID = null)
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
                cmd = new SqlCommand("MM_LIST_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@PREV_LISTID", PREV_LISTID);
                cmd.Parameters.AddWithValue("@OnClickFilter", OnClickFilter);

                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);

                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);

                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet MM_PD_SP(string ACTION = null, string INVID = null, string SUBJID = null, string PD_ID = null, string PROCESS = null, string CAT = null, string SUBCAT = null, string CLASS_MED = null, string NOTES_MED = null, string IMPACT_MED = null, string CLASS_SPONSOR = null, string NOTES_SPONSOR = null, string SUMMARY_SPONSOR = null)
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
                cmd = new SqlCommand("MM_PD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);

                cmd.Parameters.AddWithValue("@PD_ID", PD_ID);
                cmd.Parameters.AddWithValue("@PROCESS", PROCESS);
                cmd.Parameters.AddWithValue("@CAT", CAT);
                cmd.Parameters.AddWithValue("@SUBCAT", SUBCAT);
                cmd.Parameters.AddWithValue("@CLASS_MED", CLASS_MED);
                cmd.Parameters.AddWithValue("@NOTES_MED", NOTES_MED);
                cmd.Parameters.AddWithValue("@IMPACT_MED", IMPACT_MED);
                cmd.Parameters.AddWithValue("@CLASS_SPONSOR", CLASS_SPONSOR);
                cmd.Parameters.AddWithValue("@NOTES_SPONSOR", NOTES_SPONSOR);
                cmd.Parameters.AddWithValue("@SUMMARY_SPONSOR", SUMMARY_SPONSOR);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.CommandTimeout = 0;
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

    }
}