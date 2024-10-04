using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class EMAIL_INTEGRATION : System.Web.UI.Page
    {
        DAL dal = new DAL();

        DAL_UMT dal_UMT = new DAL_UMT();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    DataSet ds = dal.EMAIL_INTEGRATION(ACTION: "GET_DATA");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtDisplayName.Text = ds.Tables[0].Rows[0]["DISPLAYNAME"].ToString();
                        txtHostName.Text = ds.Tables[0].Rows[0]["HOSTNAME"].ToString();
                        txtPortNo.Text = ds.Tables[0].Rows[0]["PORTNO"].ToString();

                        if (ds.Tables[0].Rows[0]["SSL"].ToString() == "True")
                        {
                            chkSSL.Checked = true;
                        }
                        else
                        {
                            chkSSL.Checked = false;
                        }

                        txtUserName.Text = ds.Tables[0].Rows[0]["USERID"].ToString();
                        txtPassword.Text = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (MAIL_SEND(txtDisplayName.Text, txtHostName.Text, Convert.ToInt32(txtPortNo.Text), chkSSL.Checked, txtUserName.Text, txtPassword.Text))
                {
                    dal.EMAIL_INTEGRATION(ACTION: "INSERT_EMAIL_SETUP",
                    DISPLAYNAME: txtDisplayName.Text,
                    HOSTNAME: txtHostName.Text,
                    PORTNO: txtPortNo.Text,
                    SSL: chkSSL.Checked,
                    USERNAME: txtUserName.Text,
                    PASSWORD: txtPassword.Text,
                    IPADDRESS: GetIpAddress()
                    );

                    
                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','Mailbox configured successfully', 'success');", true);
                    //////ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Mailbox configured successfully'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Error!','Invalid configuration', 'Error');", true);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private bool MAIL_SEND(string DisplayName, string SMPT_Host, int Port, bool SSL, string Username, string Pwd)
        {
            bool _output = false;
            try
            {
                //Unable to send email from Office 365 using c# SmtpClient
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                MailMessage MailMsg = new MailMessage();
                MailMsg.From = new System.Net.Mail.MailAddress(Username, DisplayName, System.Text.Encoding.UTF8);
                MailMsg.To.Add(Username);
                MailMsg.Body = "Mailbox configuration test email";
                MailMsg.Subject = "Mailbox configuration test email";

                SmtpClient mSmtpClient = new SmtpClient(SMPT_Host);
                mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                NetworkCredential basicCredential = new NetworkCredential(Username, Pwd);
                mSmtpClient.Credentials = basicCredential;
                mSmtpClient.EnableSsl = SSL;
                mSmtpClient.Port = Port;
                mSmtpClient.Send(MailMsg);
                _output = true;
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
            return _output;
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

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            string xlname = "Mailbox Configuration";

            DataSet ds = dal_UMT.UMT_LOG_SP(
                ACTION: "GET_EMAIL_CONFIG"
                );
            DataSet dsExport = new DataSet();

            for (int i = 1; i < ds.Tables.Count; i++)
            {
                ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                dsExport.Tables.Add(ds.Tables[i].Copy());
                i++;
            }
            Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
        }
    }
}