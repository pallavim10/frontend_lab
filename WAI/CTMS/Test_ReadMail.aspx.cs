using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class Test_ReadMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Read_Emails();
        }

        
        [Serializable]
        public class Email
        {
            public Email()
            {
                this.Attachments = new List<Attachment>();
            }
            public int MessageNumber { get; set; }
            public string From { get; set; }
            public string CC { get; set; }
            public string BCC { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public DateTime DateSent { get; set; }
            public List<Attachment> Attachments { get; set; }
        }

        [Serializable]
        public class Attachment
        {
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public byte[] Content { get; set; }
        }
    }
}