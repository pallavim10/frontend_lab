using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Web;

namespace DataTransferSystem.App_Code
{
    public class DATA_DAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        //Get Connection
        DataSet ds;
        SqlCommand cmd;
        SqlDataAdapter adp;

        public DataTable Get_Category(string Action)
        {
            using (SqlCommand cmd = new SqlCommand("Proc_Category", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Connection = con;
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();
                DataTable dt_category = new DataTable();
                using (SqlDataAdapter adapt = new SqlDataAdapter(cmd))
                {
                    adapt.Fill(dt_category);
                }
                con.Close();

                return dt_category;
            }
        }

        public DataTable Get_Users()
        {
            using (SqlCommand cmd = new SqlCommand("Proc_Tickets_Helpdesk", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Get_Users");
                cmd.Connection = con;
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();
                DataTable dt_User = new DataTable();
                using (SqlDataAdapter adapt = new SqlDataAdapter(cmd))
                {
                    adapt.Fill(dt_User);
                }
                con.Close();

                return dt_User;
            }
        }
        public DataSet LOGIN_LOGS_SP(string ACTION = null, string UserName = null, string Name = null, string FirstName = null, string LastName = null, string Result = null, string HostIP = null, string IPADDRESS = null, string GUID = null, string SID = null, string EmailID = null, string ContactNo = null, string Department = null, string Designation = null)
        {
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("LOGIN_LOGS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@Result", Result);
                cmd.Parameters.AddWithValue("@HostIP", HostIP);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);
                cmd.Parameters.AddWithValue("@GUID", GUID);
                cmd.Parameters.AddWithValue("@SID", SID);
                cmd.Parameters.AddWithValue("@ContactNo", ContactNo);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Designation", Designation);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
       
        public DateTime GetCurrentDateTimeByTimezone()
        {
            DateTime resultDateTime = new DateTime();

            TimeZoneInfo UserTimezone = TimeZoneInfo.FindSystemTimeZoneById(GetTimeZone());

            resultDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, UserTimezone);

            return resultDateTime;
        }

        public string GetTimeZone()
        {
            if (HttpContext.Current.Session["TimeZone_Standard"] == null)
            {
                return "India Standard Time";
            }
            else
            {
                return HttpContext.Current.Session["TimeZone_Standard"].ToString();
            }
        }

        public DataSet MANAGE_USER_SP(string ACTION = null, string ID = null, string UserName = null, bool Masters = false, bool Users = false, string Name = null, string GUID = null, string SID = null, string EmailID = null, string ContactNo = null, string Department = null, string Designation = null)
        {
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("MANAGE_USER_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Masters", Masters);
                cmd.Parameters.AddWithValue("@Users", Users);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@GUID", GUID);
                cmd.Parameters.AddWithValue("@SID", SID);
                cmd.Parameters.AddWithValue("@ContactNo", ContactNo);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Designation", Designation);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        public string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string ip_add = "";
            foreach (var ipp in host.AddressList)
            {
                if (ipp.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip_add = ipp.ToString();
                }
            }
            return ip_add;
        }
    
    }
}