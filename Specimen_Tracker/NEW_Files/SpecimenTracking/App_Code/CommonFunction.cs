
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace SpecimenTracking.App_Code
{
    public class CommonFunction
    {
        DAL dal = new DAL();
        public DropDownList BindDropDown(DropDownList ddl, DataTable dt)
        {
            ddl.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "Text";
                ddl.DataValueField = "Value";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("--All--", "-1"));
            return ddl;
        }

        public DataTable GetDataTable(GridView grd)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (grd.HeaderRow != null)
            {

                for (int i = 0; i < grd.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(grd.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in grd.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public bool Email_Users(string EmailAddress, string CCEmailAddress, string subject, string body, string BCCEmailAddress = null)
        {
            bool _output = false;

            try
            {
                DataSet ds = dal.EMAIL_INTEGRATION(ACTION: "GET_DATA");

                string DISPLAYNAME = ds.Tables[0].Rows[0]["DISPLAYNAME"].ToString();
                string USERID = ds.Tables[0].Rows[0]["USERID"].ToString();
                string PASSWORD = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                string HOSTNAME = ds.Tables[0].Rows[0]["HOSTNAME"].ToString();
                string PORTNO = ds.Tables[0].Rows[0]["PORTNO"].ToString();
                bool SSL = Convert.ToBoolean(ds.Tables[0].Rows[0]["SSL"].ToString());

                string _FromID = USERID;
                string _PWD = PASSWORD;

                MailMessage MailMsg = new MailMessage();
                MailMsg.From = new System.Net.Mail.MailAddress(_FromID, DISPLAYNAME, System.Text.Encoding.UTF8);

                char t = ';';
                char c = ',';
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    if (!EmailAddress.Contains(c) && !EmailAddress.Contains(t))
                    {
                        MailMsg.To.Add(EmailAddress);
                    }
                    else
                    {
                        if (EmailAddress.Contains(t))
                        {
                            string[] _CCList = EmailAddress.Split(t);
                            for (int j = 0; j < _CCList.Length; j++)
                            {
                                if (_CCList[j].ToString() != "")
                                {
                                    MailMsg.To.Add(_CCList[j].ToString());
                                }
                            }
                        }

                        if (EmailAddress.Contains(c))
                        {
                            string[] _CCList = EmailAddress.Split(c);
                            for (int j = 0; j < _CCList.Length; j++)
                            {
                                if (_CCList[j].ToString() != "")
                                {
                                    MailMsg.To.Add(_CCList[j].ToString());
                                }
                            }
                        }
                    }

                }

                if (!string.IsNullOrEmpty(CCEmailAddress))
                {
                    if (!CCEmailAddress.Contains(t) && !CCEmailAddress.Contains(c))
                    {
                        MailMsg.CC.Add(CCEmailAddress);
                    }
                    else
                    {
                        if (CCEmailAddress.Contains(t))
                        {
                            string[] _CCList = CCEmailAddress.Split(t);
                            for (int j = 0; j < _CCList.Length; j++)
                            {
                                if (_CCList[j].ToString() != "")
                                {
                                    MailMsg.CC.Add(_CCList[j].ToString());
                                }
                            }
                        }


                        if (CCEmailAddress.Contains(c))
                        {
                            string[] _CCList = CCEmailAddress.Split(c);
                            for (int j = 0; j < _CCList.Length; j++)
                            {
                                if (_CCList[j].ToString() != "")
                                {
                                    MailMsg.CC.Add(_CCList[j].ToString());
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(BCCEmailAddress))
                {
                    if (!BCCEmailAddress.Contains(t) && !BCCEmailAddress.Contains(c))
                    {
                        MailMsg.Bcc.Add(BCCEmailAddress);
                    }
                    else
                    {
                        if (BCCEmailAddress.Contains(t))
                        {
                            string[] _BCCList = BCCEmailAddress.Split(t);
                            for (int j = 0; j < _BCCList.Length; j++)
                            {
                                if (_BCCList[j].ToString() != "")
                                {
                                    MailMsg.Bcc.Add(_BCCList[j].ToString());
                                }
                            }
                        }


                        if (BCCEmailAddress.Contains(c))
                        {
                            string[] _BCCList = BCCEmailAddress.Split(c);
                            for (int j = 0; j < _BCCList.Length; j++)
                            {
                                if (_BCCList[j].ToString() != "")
                                {
                                    MailMsg.Bcc.Add(_BCCList[j].ToString());
                                }
                            }
                        }
                    }
                }

                MailMsg.Body = body;
                MailMsg.Subject = subject;
                MailMsg.IsBodyHtml = true;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                SmtpClient mSmtpClient = new SmtpClient(HOSTNAME);
                mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                NetworkCredential basicCredential = new NetworkCredential(_FromID, _PWD);
                mSmtpClient.Credentials = basicCredential;
                mSmtpClient.EnableSsl = SSL;
                mSmtpClient.Port = Convert.ToInt32(PORTNO);
                mSmtpClient.Send(MailMsg);

                dal.EMAIL_SP
                    (
                    ACTION: "INSERT_LOG",
                    EMAILIDS: EmailAddress,
                    CCEMAILIDS: CCEmailAddress,
                    BCCEMAILIDS: BCCEmailAddress,
                    SUBJECT: subject,
                    BODY: body,
                    SENT: true
                    );

                _output = true;
            }
            catch (Exception ex)
            {
                dal.EMAIL_SP
                    (
                    ACTION: "INSERT_LOG",
                    EMAILIDS: EmailAddress,
                    CCEMAILIDS: CCEmailAddress,
                    BCCEMAILIDS: BCCEmailAddress,
                    SUBJECT: subject,
                    BODY: body,
                    SENT: false,
                    ERR: ex.Message.ToString()
                    );

                _output = false;
                throw new Exception(ex.Message);
            }
            return _output;
        }

        public bool Email_Invitation(DateTime Date, DateTime Time, string EmailAddress = null, string CCEmailAddress = null, string subject = null, string body = null, string Location = null, string Status = null)
        {
            bool _output = false;
            string startDate = Date.ToShortDateString() + " " + Time.ToShortTimeString();

            string _FromID = "ds.support@diagnosearch.com";
            string _PWD = "Taz18561";

            //string _FromID = "dssupport@diagnosearch.com";
            //string _PWD = "Hidk*873";


            try
            {
                MailMessage MailMsg = new MailMessage();
                MailMsg.From = new System.Net.Mail.MailAddress(_FromID, "DiagnoSearch", System.Text.Encoding.UTF8);
                //MailMsg.To.Add(EmailAddress);
                char t = ';';
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    string[] _CCList = EmailAddress.Split(t);
                    for (int j = 0; j < _CCList.Length; j++)
                    {
                        if (_CCList[j].ToString() != "")
                        {
                            MailMsg.To.Add(_CCList[j].ToString());
                        }
                    }
                }
                if (!string.IsNullOrEmpty(CCEmailAddress))
                {
                    string[] _CCList = CCEmailAddress.Split(t);
                    for (int j = 0; j < _CCList.Length; j++)
                    {
                        if (_CCList[j].ToString() != "")
                        {
                            MailMsg.CC.Add(_CCList[j].ToString());
                        }
                    }
                }

                string Method = "REQUEST";
                if (Status != "CONFIRMED")
                {
                    Method = "CANCEL";
                }

                //Making Invitation
                StringBuilder str = new StringBuilder();
                str.AppendLine("BEGIN:VCALENDAR");
                str.AppendLine("PRODID:-// DiagnoSearch");
                str.AppendLine("VERSION:2.0");
                str.AppendLine("METHOD:" + Method + "");
                str.AppendLine("BEGIN:VEVENT");

                str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", Convert.ToDateTime(startDate).ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z")));
                str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", Convert.ToDateTime(startDate).ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z")));
                str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", Convert.ToDateTime(startDate).ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z")));
                str.AppendLine(string.Format("CREATED:{0:yyyyMMddTHHmmssZ}", Convert.ToDateTime(startDate).ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z")));
                str.AppendLine(string.Format("LAST-MODIFIED:{0:yyyyMMddTHHmmssZ}", Convert.ToDateTime(startDate).ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z")));
                str.AppendLine("LOCATION:" + Location);
                str.AppendLine("UID:" + _FromID + "@" + Convert.ToDateTime(startDate).ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z") + "@" + Location);
                str.AppendLine("SEQUENCE:0");
                str.AppendLine(string.Format("DESCRIPTION:" + body + ""));
                //str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", body));
                str.AppendLine(string.Format("SUMMARY:Meeting"));
                str.AppendLine("STATUS:" + Status + "");
                str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", _FromID));

                for (int i = 0; i < MailMsg.To.Count; i++)
                {
                    str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", MailMsg.To[i].DisplayName, MailMsg.To[i].Address));
                }

                str.AppendLine("BEGIN:VALARM");
                str.AppendLine("TRIGGER:-PT15M");
                str.AppendLine("ACTION:DISPLAY");
                str.AppendLine("END:VALARM");

                str.AppendLine("END:VEVENT");
                str.AppendLine("END:VCALENDAR");
                System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType("text/calendar");
                ct.Parameters.Add("method", "REQUEST");
                ct.Parameters.Add("name", "invite.ics");
                AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), ct);
                MailMsg.AlternateViews.Add(avCal);

                MailMsg.Body = body;
                MailMsg.Subject = subject;
                MailMsg.IsBodyHtml = true;
                MailMsg.Priority = MailPriority.High;
                SmtpClient mSmtpClient = new SmtpClient("smtp.office365.com");
                NetworkCredential basicCredential = new NetworkCredential(_FromID, _PWD);
                mSmtpClient.Credentials = basicCredential;
                mSmtpClient.EnableSsl = true;
                mSmtpClient.Port = 587;
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

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;
            Uri redirectionUri = new Uri(redirectionUrl);
            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        //public bool Communication_SendMail(string Username, string Password, string ToEmailAddress, string CCEmailAddress, string BCCEmailAddres, string subject, string body, DataTable Attachments, string Importances)
        //{
        //    bool _output = false;

        //    string _FromID = Username;
        //    string _PWD = Password;

        //    try
        //    {
        //        ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
        //        service.UseDefaultCredentials = false;
        //        // Set specific credentials.
        //        service.Credentials = new WebCredentials(_FromID, _PWD, HttpContext.Current.Session["User_Name"].ToString());

        //        service.TraceEnabled = true;
        //        service.TraceFlags = TraceFlags.All;

        //        service.AutodiscoverUrl("diagnotest@diagnosearch.com", RedirectionUrlValidationCallback);
        //        //service.AutodiscoverUrl(_FromID, RedirectionCallback);

        //        //Microsoft.Exchange.WebServices.Data.Folder folder = new Microsoft.Exchange.WebServices.Data.Folder(service);
        //        //folder.DisplayName = "TEST";
        //        //folder.Save(WellKnownFolderName.Inbox);


        //        EmailMessage email = new EmailMessage(service);

        //        email.From = _FromID;

        //        char t = ';';

        //        if (!string.IsNullOrEmpty(ToEmailAddress))
        //        {
        //            string[] ToList = ToEmailAddress.Split(t);
        //            for (int a = 0; a < ToList.Length; a++)
        //            {
        //                if (ToList[a].ToString() != "")
        //                {
        //                    email.ToRecipients.Add(ToList[a].ToString());
        //                }
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(CCEmailAddress))
        //        {
        //            string[] CCList = CCEmailAddress.Split(t);
        //            for (int a = 0; a < CCList.Length; a++)
        //            {
        //                if (CCList[a].ToString() != "")
        //                {
        //                    email.CcRecipients.Add(CCList[a].ToString());
        //                }
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(BCCEmailAddres))
        //        {
        //            string[] BCCList = BCCEmailAddres.Split(t);
        //            for (int a = 0; a < BCCList.Length; a++)
        //            {
        //                if (BCCList[a].ToString() != "")
        //                {
        //                    email.BccRecipients.Add(BCCList[a].ToString());
        //                }
        //            }
        //        }

        //        foreach (DataRow dr in Attachments.Rows)
        //        {
        //            byte[] bytes = (byte[])dr["Data"];
        //            string fileName = dr["FileName"].ToString();
        //            string ContentType = dr["ContentType"].ToString();
        //            MemoryStream ms = new MemoryStream(bytes);
        //            System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(ms, fileName, ContentType);

        //            email.Attachments.AddFileAttachment(fileName, bytes);
        //        }

        //        email.Body = body;
        //        email.Subject = subject;

        //        if (Importances == "Low")
        //        {
        //            email.Importance = Importance.Low;
        //        }
        //        else if (Importances == "Normal")
        //        {
        //            email.Importance = Importance.Normal;
        //        }
        //        else if (Importances == "High")
        //        {
        //            email.Importance = Importance.High;
        //        }

        //        email.SendAndSaveCopy();
        //    }
        //    catch (Exception ex)
        //    {
        //        _output = false;
        //        throw new Exception(ex.Message);
        //    }
        //    return _output;
        //}

        public bool TestMail_Send(string Username, string Password, string ToEmailAddress, string subject, string body)
        {
            bool _output = false;

            string _FromID = Username;
            string _PWD = Password;

            try
            {
                MailMessage MailMsg = new MailMessage();
                MailMsg.From = new System.Net.Mail.MailAddress(_FromID, "DiagnoSearch", System.Text.Encoding.UTF8);
                char t = ';';

                if (!string.IsNullOrEmpty(ToEmailAddress))
                {
                    string[] ToList = ToEmailAddress.Split(t);
                    for (int a = 0; a < ToList.Length; a++)
                    {
                        if (ToList[a].ToString() != "")
                        {
                            MailMsg.To.Add(ToList[a].ToString());
                        }
                    }
                }

                MailMsg.Body = body;
                MailMsg.Subject = subject;
                MailMsg.IsBodyHtml = true;

                SmtpClient mSmtpClient = new SmtpClient("mail.diagnosearch.com");
                mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                NetworkCredential basicCredential = new NetworkCredential(_FromID, _PWD);
                mSmtpClient.Credentials = basicCredential;
                mSmtpClient.EnableSsl = true;
                mSmtpClient.Port = 587;
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

        // Return the name of the method that called this one.
        public string GetMethodName()
        {
            return new StackTrace(1).GetFrame(0).GetMethod().Name;
        }

        public bool isDate(string date)
        {
            bool result = false;

            Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");

            //Verify whether date entered in dd/MM/yyyy format.
            bool isValid = regex.IsMatch(date.Trim());

            //Verify whether entered date is Valid date.
            DateTime dt;
            isValid = DateTime.TryParseExact(date, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
            if (!isValid)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
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

        public DateTime GetCurrentDateTimeByTimezone()
        {
            DateTime resultDateTime = new DateTime();

            TimeZoneInfo UserTimezone = TimeZoneInfo.FindSystemTimeZoneById(GetTimeZone());

            resultDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, UserTimezone);

            return resultDateTime;
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

        public string Get_UserID()
        {
            if (HttpContext.Current.Session["USER_ID"] == null)
            {
                return "0";
            }
            else
            {
                return HttpContext.Current.Session["USER_ID"].ToString();
            }
        }

    }
}