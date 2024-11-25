using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace DataTransferSystem
{
    public class ActiveDirectory
    {
        string AD_path = ConfigurationManager.AppSettings["AD_path"].ToString();

        string AD_user = ConfigurationManager.AppSettings["AD_user"].ToString();
        string AD_pwd = ConfigurationManager.AppSettings["AD_pwd"].ToString();

        public string[] ValidateUser(string Username)
        {

            string directory_path = AD_path;
            string userName = AD_user;
            string pwd = AD_pwd;

            string[] data = new string[3];
            try
            {
                DirectoryEntry entry = new DirectoryEntry(directory_path, userName, pwd, AuthenticationTypes.Secure);
                DirectorySearcher dssearch = new DirectorySearcher(entry);
                dssearch.Filter = "(sAMAccountName=" + Username + ")";
                SearchResult rs = dssearch.FindOne();
                SecurityIdentifier sid = new SecurityIdentifier((byte[])rs.GetDirectoryEntry().Properties["objectSid"].Value, 0);
                data[0] = rs.GetDirectoryEntry().Guid.ToString();
                data[1] = sid.ToString();


            }
            catch (Exception e)
            {
                data[0] = "Invalid";
                data[1] = e.Message;
            }
            return data;
        }
        public bool AuthenticateUser(string Username, string Pwd)
        {
            string directory_path = AD_path;
            string userName = Username;
            string pwd = Pwd;


            bool res = false;
            string[] data = new string[7];

            try
            {
                DirectoryEntry entry = new DirectoryEntry(directory_path, userName, pwd, AuthenticationTypes.Secure);
                DirectorySearcher dssearch = new DirectorySearcher(entry);
                dssearch.Filter = "(sAMAccountName=" + Username + ")";
                SearchResult rs = dssearch.FindOne();
                res = true;
            }
            catch (Exception e)
            {
                res = false;
                e.StackTrace.ToString();
            }
            return res;
        }
        public string[] GetUserDetails(string Username, string Pwd)
        {
            string directory_path = AD_path;

            string[] data = new string[7];

            try
            {
                DirectoryEntry entry = new DirectoryEntry(directory_path, Username, Pwd, AuthenticationTypes.Secure);
                DirectorySearcher dssearch = new DirectorySearcher(entry);
                dssearch.Filter = "(sAMAccountName=" + Username + ")";
                SearchResult rs = dssearch.FindOne();
                DirectoryEntry dsresult = rs.GetDirectoryEntry();


                if (dsresult.Properties["cn"][0] != null && dsresult.Properties["cn"][0].ToString() != "")
                {
                    data[0] = dsresult.Properties["cn"][0].ToString();

                }
                else
                {
                    data[0] = "";
                }

                if (!dsresult.Properties.Contains("telephoneNumber"))
                {
                    data[1] = "";
                }
                else
                {
                    data[1] = dsresult.Properties["telephoneNumber"][0].ToString();
                }

                if (!dsresult.Properties.Contains("mail"))
                {
                    data[2] = "";
                }
                else
                {
                    data[2] = dsresult.Properties["mail"][0].ToString();
                }

                if (!dsresult.Properties.Contains("department"))
                {
                    data[3] = "";
                }
                else
                {
                    data[3] = dsresult.Properties["department"][0].ToString();
                }


                if (!dsresult.Properties.Contains("title"))
                {
                    data[4] = "";
                }
                else
                {
                    data[4] = dsresult.Properties["title"][0].ToString();
                }
                if (!dsresult.Properties.Contains("givenName"))
                {
                    data[5] = "";
                }
                else
                {
                    data[5] = dsresult.Properties["givenName"][0].ToString();
                }


                if (!dsresult.Properties.Contains("sn"))
                {
                    data[6] = "";
                }
                else
                {
                    data[6] = dsresult.Properties["sn"][0].ToString();
                }


            }
            catch (Exception e)
            {
                data[0] = "Invalid";
                e.StackTrace.ToString();
            }
            return data;
        }
    }
}