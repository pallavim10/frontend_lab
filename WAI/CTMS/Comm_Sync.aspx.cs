using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

namespace CTMS
{
    public partial class Comm_Sync : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction comfunc = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                Remove_Temp();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Btn_Sync_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 i;

                for (i = 0; i < Grd_Mails.Rows.Count; i++)
                {

                    CheckBox Chk_Sync;
                    Chk_Sync = (CheckBox)Grd_Mails.Rows[i].FindControl("Chk_Sync");

                    if (Chk_Sync.Checked)
                    {
                        Label txtID;
                        txtID = (Label)Grd_Mails.Rows[i].FindControl("txtID");

                        DataSet ds = dal.Communication_SP(Action: "Insert_TempToMail", PROJECTID: Session["PROJECTID"].ToString(), UserID: Session["User_ID"].ToString(), ID: txtID.Text);

                        string CommID = ds.Tables[0].Rows[0]["ID"].ToString();

                        dal.Communication_SP(Action: "Insert_TempToAttachment", Comm_ID: CommID, ID: txtID.Text);
                    }
                }
                Response.Write("<script> alert('Mailbox Synced Successfully.');window.location='Comm_Sync.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void Remove_Temp()
        {
            try
            {
                dal.Communication_SP(Action: "Remove_Temp", UserID: Session["User_ID"].ToString(), PROJECTID: Session["PROJECTID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool isEmailValid(string FromID, string Subject, string Body)
        {
            bool Valid = false;
            try
            {
                if (txtEMAILID.Text != "")
                {
                    if (FromID.Contains(txtEMAILID.Text))
                    {
                        Valid = true;
                    }
                }

                if (txtSubject.Text != "")
                {
                    if (!Valid)
                    {
                        if (Subject.Contains(txtSubject.Text))
                        {
                            Valid = true;
                        }
                    }
                }

                if (txtBody.Text != "")
                {
                    if (!Valid)
                    {
                        if (Body.Contains(txtBody.Text.Trim()))
                        {
                            Valid = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return Valid;
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
            public string To { get; set; }
            public string CC { get; set; }
            public string BCC { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string Importance { get; set; }
            public DateTime DateSent { get; set; }
            public List<Attachment> Attachments { get; set; }
            public string MessageUniqueNumber { get; set; }
        }

        [Serializable]
        public class Attachment
        {
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public byte[] Content { get; set; }
        }

        public string Between(string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        private void Insert_Attachments(string CommId, string FileName, string ContentType, byte[] Data)
        {
            try
            {
                dal.Communication_SP(Action: "Insert_TempAttachment", Comm_ID: CommId, FileName: FileName, ContentType: ContentType, Data: Data, UserID: Session["User_ID"].ToString(), PROJECTID: Session["PROJECTID"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Insert_TempMail(string UNIQUEID, string MessageUniqueNum, string FromID, string ToID, string CcID, string BccID, string Subject, string Body, DateTime DateTimeSent, string Importance, string RULEID)
        {
            string ReturnValue = "";
            try
            {
                string UserID = null, PROJECTID = null;

                UserID = HttpContext.Current.Session["User_ID"].ToString();
                PROJECTID = HttpContext.Current.Session["PROJECTID"].ToString();

                DataSet ds = dal.Communication_SP(Action: "Insert_TempMail",
                MessageUniqueNumber: MessageUniqueNum,
                FromID: FromID,
                ToID: ToID,
                CcID: CcID,
                BccID: BccID,
                Subject: Subject,
                Body: Body,
                DateTimeSent: Convert.ToDateTime(DateTimeSent, CultureInfo.CurrentCulture).ToString("yyyy-MM-dd hh:MM:ss"),
                UserID: UserID,
                PROJECTID: PROJECTID,
                UNIQUEID: UNIQUEID,
                Importance: Importance,
                ID: RULEID
                );

                ReturnValue = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ReturnValue;
        }

    }
}