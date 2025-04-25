using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SCHEDULAR
{
    public class Global : System.Web.HttpApplication
    {
        DAL dal = new DAL();

        DAL_DM dal_DM = new DAL_DM();
        private static Timer _timer;


        protected void Application_Start(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (Session["User_ID"] != null && Session["PROJECTID"] != null)
                {
                    Read_Emails();
                }

                _timer = new Timer(60000); // Check every 60 seconds
                _timer.Elapsed += TimerElapsed;
                _timer.AutoReset = true;
                _timer.Enabled = true;
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string WindowsPeriod = "", startDateStr = "", startTimeStr = "", LogStartDTStr = "", LogEndDTStr = "", LogEndDTStr_New = "", LogStartDTStr_New = "";
                Boolean Rulerun = false;

                DataSet ds = dal_DM.DM_SCHEDULE_SP(ACTION: "GET_SCHEDULE");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    WindowsPeriod = ds.Tables[0].Rows[0]["WINDOWPERIOD"].ToString();

                    startDateStr = ds.Tables[0].Rows[0]["STARTDATE"].ToString();
                    startTimeStr = ds.Tables[0].Rows[0]["STARTTIME"].ToString();

                }

                DateTime Start_Date = DateTime.Parse(startDateStr);

                Start_Date = Start_Date.AddDays(Convert.ToInt32(WindowsPeriod));

                TimeSpan Start_time = TimeSpan.ParseExact(startTimeStr, "hh\\:mm", null);
                string Starttime = Start_time.ToString(@"hh\:mm");

                DataSet ds1 = dal_DM.DM_SCHEDULE_SP(ACTION: "GET_SCHEDUALAR_LOG");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    LogStartDTStr = ds1.Tables[0].Rows[0]["STARTDATE"].ToString();
                    LogEndDTStr = ds1.Tables[0].Rows[0]["ENDDATE"].ToString();
                }

                DateTime LogStartdate = DateTime.Parse(LogStartDTStr);
                DateTime LogendDate = DateTime.Parse(LogEndDTStr);

                if (ds1.Tables.Count == 0 && ds.Tables[0].Rows.Count == 0)
                {
                    if (Start_Date == DateTime.Now.Date && Starttime == DateTime.Now.ToString("HH:mm"))
                    {
                        //Insert Start date into Log Table 
                        dal_DM.DM_SCHEDULE_SP(ACTION: "INSERT_RULE_STARTDATE", STARTDATE: DateTime.Now.ToString());
                        //Run Rule Code 

                        Rulerun = true;
                    }
                    if (Rulerun == true)
                    {
                        //Insert EndDate into log Table 
                        dal_DM.DM_SCHEDULE_SP(ACTION: "INSERT_RULE_ENDDATE", ENDDATE: DateTime.Now.ToString());
                    }
                }
                else if (ds1.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (LogendDate.ToString() != "")
                    {
                        Start_Date = LogendDate.AddDays(Convert.ToInt32(WindowsPeriod));

                        //Insert Start date into Log Table 
                        dal_DM.DM_SCHEDULE_SP(ACTION: "INSERT_RULE_STARTDATE", STARTDATE: DateTime.Now.ToString());

                        DataSet ds2 = dal_DM.DM_SCHEDULE_SP(ACTION: "GET_SCHEDUALAR_LOG");
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            LogEndDTStr_New = ds2.Tables[0].Rows[0]["ENDDATE"].ToString();
                        }

                        DateTime LogendDate_New = DateTime.Parse(LogEndDTStr_New);

                        if (Start_Date == DateTime.Now.Date && Starttime == DateTime.Now.ToString("HH:mm") && LogendDate_New.ToString() == "")
                        {
                            //Run Rule Code 

                            Rulerun = true;
                        }
                    }

                    else if (LogendDate.ToString() == "")
                    {
                        DataSet ds3 = dal_DM.DM_SCHEDULE_SP(ACTION: "GET_SCHEDUALAR_LOG");
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            LogStartDTStr_New = ds3.Tables[0].Rows[0]["STARTDATE"].ToString();
                        }

                        DateTime Start_Date_NEW = DateTime.Parse(LogStartDTStr_New);

                        if (Start_Date_NEW == DateTime.Now.Date && Starttime == DateTime.Now.ToString("HH:mm"))
                        {
                            //Insert Start date into Log Table 
                            dal_DM.DM_SCHEDULE_SP(ACTION: "INSERT_RULE_STARTDATE", STARTDATE: DateTime.Now.ToString());
                            //Run Rule Code 

                            Rulerun = true;
                        }
                    }

                    if (Rulerun == true)
                    {
                        //Insert EndDate into log Table 
                        dal_DM.DM_SCHEDULE_SP(ACTION: "INSERT_RULE_ENDDATE", ENDDATE: DateTime.Now.ToString());
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (Session["User_ID"] != null && Session["PROJECTID"] != null)
                {
                    Read_Emails();
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (Session["User_ID"] != null && Session["PROJECTID"] != null)
                {
                    Read_Emails();
                }
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (Session["User_ID"] != null && Session["PROJECTID"] != null)
                {
                    Read_Emails();
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (Session["User_ID"] != null && Session["PROJECTID"] != null)
                {
                    Read_Emails();
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private void Read_Emails()
        {
            //DataSet ds = dal.Communication_SP(Action: "Get_Un_Pwd", UserID: Session["User_ID"].ToString());
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    string Username = ds.Tables[0].Rows[0]["Mail_Username"].ToString();
            //    string Password = ds.Tables[0].Rows[0]["Mail_Password"].ToString();

            //    if (Username != "" && Password != "")
            //    {
            //        Pop3Client pop3Client;
            //        pop3Client = new Pop3Client();
            //        pop3Client.Connect("mail.diagnosearch.com", 995, true);
            //        pop3Client.Authenticate(Username, Password, AuthenticationMethod.UsernameAndPassword);

            //        int count = pop3Client.GetMessageCount();
            //        int counter = 0;

            //        for (int i = count; i >= 1; i--)
            //        {
            //            Message message = pop3Client.GetMessage(i);
            //            Email email = new Email()
            //            {
            //                MessageNumber = i,
            //                Subject = message.Headers.Subject,
            //                DateSent = message.Headers.DateSent,
            //                From = message.Headers.From.Address.ToString(),
            //                MessageUniqueNumber = message.Headers.MessageId.ToString(),
            //                To = message.Headers.To.ToString(),
            //            };

            //            string to = string.Empty;
            //            foreach (OpenPop.Mime.Header.RfcMailAddress objectItem in message.Headers.To)
            //            {
            //                to += objectItem.Address + ";";
            //            }
            //            if (!string.IsNullOrEmpty(to))
            //            {
            //                email.To = to.Remove(to.Length - 1);
            //            }

            //            string cc = string.Empty;
            //            foreach (OpenPop.Mime.Header.RfcMailAddress objectItem in message.Headers.Cc)
            //            {
            //                cc += objectItem.Address + ";";
            //            }
            //            if (!string.IsNullOrEmpty(cc))
            //            {
            //                email.CC = cc.Remove(cc.Length - 1);
            //            }

            //            string bcc = string.Empty; ;
            //            foreach (OpenPop.Mime.Header.RfcMailAddress objectItem in message.Headers.Bcc)
            //            {
            //                bcc += objectItem.Address + ";";
            //            }
            //            if (!string.IsNullOrEmpty(bcc))
            //            {
            //                email.BCC = bcc.Remove(bcc.Length - 1);
            //            }

            //            DataSet dsUniqueNum_ID = dal.Communication_SP(Action: "Check_UNIQUENUM_Exist", MessageUniqueNumber: email.MessageUniqueNumber);

            //            if (dsUniqueNum_ID.Tables[0].Rows[0]["RESULT"].ToString() == "NO")
            //            {

            //                if (email.Subject.Contains("[Comn#"))
            //                {
            //                    string UniqueID = Between(email.Subject, "[Comn#", "]");

            //                    MessagePart body = message.FindFirstHtmlVersion();
            //                    if (body != null)
            //                    {
            //                        email.Body = body.GetBodyAsText();
            //                    }
            //                    else
            //                    {
            //                        body = message.FindFirstPlainTextVersion();
            //                        if (body != null)
            //                        {
            //                            email.Body = body.GetBodyAsText();
            //                        }
            //                    }

            //                    DataSet ds1 = dal.Communication_SP(Action: "GET_FOLDERNAME",
            //                        FromID: email.From,
            //                        ToID: email.To,
            //                        CcID: email.CC,
            //                        BccID: email.BCC,
            //                        Subject: email.Subject,
            //                        Body: email.Body
            //                        );

            //                    if (ds1.Tables[0].Rows[0]["RESULT"].ToString() != "NULL")
            //                    {
            //                        string CommID = Insert_RecMail(UniqueID, email.MessageUniqueNumber, email.From, email.To, email.CC, email.BCC, email.Subject, email.Body, email.DateSent, ds1.Tables[0].Rows[0]["RESULT"].ToString());

            //                        List<MessagePart> attachments = message.FindAllAttachments();

            //                        DataTable DtAttach = new DataTable();
            //                        DtAttach.Columns.Add("FileName", typeof(System.String));
            //                        DtAttach.Columns.Add("ContentType", typeof(System.String));
            //                        DtAttach.Columns.Add("Data", typeof(System.Byte[]));

            //                        foreach (MessagePart attachment in attachments)
            //                        {
            //                            Insert_Attachments(CommID, attachment.FileName, attachment.ContentType.MediaType, attachment.Body);
            //                        }
            //                    }
            //                }
            //            }

            //            counter++;
            //            if (counter > 2)
            //            {
            //                break;
            //            }
            //        }

            //    }
            //}
        }
    }
}