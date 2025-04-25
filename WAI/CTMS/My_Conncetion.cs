using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace IWRS_LIB
{
    public class My_Conncetion
    {
        public string getconstr()
        {
            //string str = "Data Source=INTERNET-SERVER;Initial Catalog=HPV_WAD;User ID=SA;Password=igate123@";
            string str = ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString;
            return str;
        }

        public bool Email_Users(string EmailAddress, string CCEmailAddress, string subject, string body)
        {
            bool _output = false;
            string _FromID = "naveen.shetty@diagnosearch.com";
            string _PWD = "music@123";
            try
            {
                //string str = "Naveen";
                //string str = csCommon.RetrieveHttpContent("http://localhost:3111/SP2/frmEmailSOA.aspx?aaid=" + AgentID + "&dts=" + StartDate + "&dte=" + EndDate + "&nm=" + AgentName);
                MailMessage MailMsg = new MailMessage();
                MailMsg.From = new MailAddress(_FromID, "TMS System", System.Text.Encoding.UTF8);
                //MailMsg.To.Add(EmailAddress);
                char t = ';';
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    string[] _CCList = EmailAddress.Split(t);
                    for (int j = 0; j < _CCList.Length; j++)
                    {
                        MailMsg.To.Add(_CCList[j].ToString());
                    }
                }
                if (!string.IsNullOrEmpty(CCEmailAddress))
                {
                    string[] _CCList = CCEmailAddress.Split(t);
                    for (int j = 0; j < _CCList.Length; j++)
                    {
                        MailMsg.CC.Add(_CCList[j].ToString());
                    }
                }
                MailMsg.Body = body;
                MailMsg.Subject = subject;
                MailMsg.IsBodyHtml = true;
                MailMsg.Priority = MailPriority.High;
                SmtpClient mSmtpClient = new SmtpClient("mail.diagnosearch.com");
                NetworkCredential basicCredential = new NetworkCredential(_FromID, _PWD);
                mSmtpClient.Credentials = basicCredential;
                mSmtpClient.EnableSsl = true;
                mSmtpClient.Port = 25;
                mSmtpClient.Send(MailMsg);
                _output = true;
            }
            catch (Exception ex)
            {
                _output = false;
                throw new Exception(ex.Message);
            }
            return _output;
        }

    }
}