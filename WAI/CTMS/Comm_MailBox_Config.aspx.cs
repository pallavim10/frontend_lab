using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class Comm_MailBox_Config : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction comfunc = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Get_Un_Pwd();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Un_Pwd()
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "Get_Un_Pwd", UserID: Session["User_ID"].ToString());
                txtMailUsername.Text = ds.Tables[0].Rows[0]["Mail_Username"].ToString();
                txtMailPassword.Text = ds.Tables[0].Rows[0]["Mail_Password"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = Send_Mail();
                if (result)
                {
                    Insert_Un_Pwd();
                    Response.Write("<script> alert('Mailbox Configured Successfully.');window.location='Comm_MailBox_Config.aspx'; </script>");
                }
                else
                {
                    Response.Write("<script> alert('Invalid Username or Password.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool Send_Mail()
        {
            bool Success = false;
            try
            {
                Success = comfunc.TestMail_Send(txtMailUsername.Text, txtMailPassword.Text, txtMailUsername.Text, "Mailbox Configured Successfully", "Thanks for Mailbox Configuration");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return Success;
        }

        private void Insert_Un_Pwd()
        {
            try
            {
                dal.Communication_SP(Action: "Insert_Un_Pwd", UserID: Session["User_ID"].ToString(), Mail_Username: txtMailUsername.Text, Mail_Password: txtMailPassword.Text);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}