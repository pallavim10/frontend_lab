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
    public partial class Comm_InboxView : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Read();
                View();
                Attachments();
            }
        }

        private void Read()
        {
            try
            {
                dal.Communication_SP(Action: "Read", ID: Request.QueryString["ComId"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void View()
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "Inbox_View", ID: Request.QueryString["ComId"].ToString());

                lblSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                lblFrom.Text = ds.Tables[0].Rows[0]["From"].ToString();
                lblTo.Text = ds.Tables[0].Rows[0]["To"].ToString();
                lblCc.Text = ds.Tables[0].Rows[0]["Cc"].ToString();
                if (lblCc.Text == "")
                {
                    divCc.Visible = false;
                }
                lblBcc.Text = ds.Tables[0].Rows[0]["Bcc"].ToString();
                if (lblBcc.Text == "")
                {
                    divBcc.Visible = false;
                }
                if (divCc.Visible == false && divBcc.Visible == false)
                {
                    divCCs.Visible = false;
                }
                litBody.Text = ds.Tables[0].Rows[0]["Body"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Attachments()
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "Get_Attachments", Comm_ID: Request.QueryString["ComId"].ToString());

                repeatAttach.DataSource = ds.Tables[0];
                repeatAttach.DataBind();

                if (ds.Tables[0].Rows.Count < 1)
                {
                    divAttach.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnReply_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Comm_NewMail.aspx?ComID=" + Request.QueryString["ComId"].ToString() + "&ComType=Reply");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnReplyAll_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Comm_NewMail.aspx?ComID=" + Request.QueryString["ComId"].ToString() + "&ComType=ReplyAll");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnForward_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Comm_NewMail.aspx?ComID=" + Request.QueryString["ComId"].ToString() + "&ComType=Forward");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatAttach_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                byte[] bytes;
                string fileName, contentType;

                DataSet ds = dal.Communication_SP(Action: "Download_Attachments", ID: ID);

                bytes = (byte[])ds.Tables[0].Rows[0]["Data"];
                contentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                fileName = ds.Tables[0].Rows[0]["FileName"].ToString();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}