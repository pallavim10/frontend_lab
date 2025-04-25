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
    public partial class CTMS_CreateAgendaTopic : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetAgendaTopic();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }


        private void GetAgendaTopic()
        {
            try
            {
                DataSet ds = dal.CTMS_AGENDA(Action: "GET_AGENDA_TOPIC");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdAgendaTopic.DataSource = ds.Tables[0];
                    grdAgendaTopic.DataBind();
                }
                else
                {
                    grdAgendaTopic.DataSource = null;
                    grdAgendaTopic.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_AGENDA(Action: "INSERT_AGENDA_TOPIC", AgendaTopic: txtTopicName.Text, ENTEREDBY: Session["User_ID"].ToString());
                txtTopicName.Text = "";
                GetAgendaTopic();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_AGENDA(Action: "UPDATE_AGENDA_TOPIC", AgendaTopic: txtTopicName.Text, ENTEREDBY: Session["User_ID"].ToString(),ID:Session["ID"].ToString());
                txtTopicName.Text = "";
                btnupdate.Visible = false;
                btnsubmit.Visible = true;
                GetAgendaTopic();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }      
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdAgendaTopic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();
                Session["ID"] = id;
                if (e.CommandName == "EditVisit")
                {
                    DataSet ds = dal.CTMS_AGENDA(Action: "GET_AGENDA_TOPICBYID", ID: Session["ID"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtTopicName.Text = ds.Tables[0].Rows[0]["AgendaTopic"].ToString();

                        btnupdate.Visible = true;
                        btnsubmit.Visible = false;
                    }
                }
                else if (e.CommandName == "DeleteVisit")
                {
                    DataSet ds = dal.CTMS_AGENDA(Action: "DELETE_AGENDA_TOPICBYID", ID: Session["ID"].ToString());
                    GetAgendaTopic();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}