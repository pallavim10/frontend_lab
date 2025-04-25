using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class CTMS_CreateRescheduleAgenda : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["PROJECTID"] != null)
                    {
                        FillAgendaTopic();
                        FillInternalUser();
                        FillExternalSite();
                        GETDATA();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        private void GETDATA()
        {
            txtAgendaID.Text = Session["AgendaID"].ToString();
            DataSet ds = dal.CTMS_AGENDA(Action: "GET_AGENDA_DETAILS", Project_ID: Session["PROJECTID"].ToString(), ID: Session["AgendaID"].ToString());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {               
                    txtAgendaDate.Text = ds.Tables[0].Rows[0]["AgendaDT"].ToString();
                    txtAgendaTime.Text = ds.Tables[0].Rows[0]["AgendaTM"].ToString();
                    txtVanue.Text = ds.Tables[0].Rows[0]["Venue"].ToString();
                }

                //Fill Agenda Topic
                lstAgendaTopic.ClearSelection();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        string temp = ds.Tables[1].Rows[i]["TopicID"].ToString();
                        if (temp != "")
                        {
                            ListItem itm = lstAgendaTopic.Items.FindByValue(temp);
                            if (itm != null)
                                itm.Selected = true;
                        }
                    }
                }
                else
                {
                    lstAgendaTopic.ClearSelection();
                }


                //Fill Internal User
                if (ds.Tables[2].Rows.Count > 0)
                {
                    grdInternalUser.DataSource = ds.Tables[2];
                    grdInternalUser.DataBind();
                }
                else
                {
                    grdInternalUser.DataSource = null;
                    grdInternalUser.DataBind();
                }

                //Fill External User
                if (ds.Tables[3].Rows.Count > 0)
                {

                    grdExternalUser.DataSource = ds.Tables[3];
                    grdExternalUser.DataBind();
                }
                else
                {
                    grdExternalUser.DataSource = null;
                    grdExternalUser.DataBind();
                }

            }

        }


        private void FillAgendaTopic()
        {
            try
            {
                DataSet ds = dal.CTMS_AGENDA(Action: "GET_AGENDA_TOPIC");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstAgendaTopic.DataSource = ds.Tables[0];
                    lstAgendaTopic.DataValueField = "TopicID";
                    lstAgendaTopic.DataTextField = "AgendaTopic";
                    lstAgendaTopic.DataBind();
                    //  lstAgendaTopic.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillInternalUser()
        {
            try
            {
                DataSet ds = dal.CTMS_AGENDA(Action: "GET_InternalUser", Project_ID: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpInternalUser.DataSource = ds.Tables[0];
                    drpInternalUser.DataValueField = "Emp_ID";
                    drpInternalUser.DataTextField = "Emp_Name";
                    drpInternalUser.DataBind();
                    drpInternalUser.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetInternalUserTemp()
        {
            try
            {
                DataSet ds = dal.CTMS_AGENDA(Action: "GET_AgendaInternalUser", Project_ID: Session["PROJECTID"].ToString(), ID: txtAgendaID.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdInternalUser.DataSource = ds.Tables[0];
                    grdInternalUser.DataBind();
                }
                else
                {
                    grdInternalUser.DataSource = null;
                    grdInternalUser.DataBind();
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
                ds = dal.CTMS_AGENDA(Action: "INSERT_InternalUser", Project_ID: Session["PROJECTID"].ToString(), EMPID: drpInternalUser.SelectedValue, ENTEREDBY: Session["User_ID"].ToString(), ID: txtAgendaID.Text);
                drpInternalUser.SelectedValue = "0";
                GetInternalUserTemp();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            drpInternalUser.SelectedValue = "0";
            GetInternalUserTemp();
        }

        protected void grdAgendaTopic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();
                Session["ID"] = id;
                if (e.CommandName == "DeleteVisit")
                {
                    DataSet ds = dal.CTMS_AGENDA(Action: "DELETE_InternalUser", Project_ID: Session["PROJECTID"].ToString(), ID: Session["ID"].ToString());
                    GetInternalUserTemp();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



        private void FillExternalSite()
        {
            try
            {
                DataSet ds = dal.CTMS_AGENDA(Action: "ExternalUser_Site", Project_ID: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpExternalSite.DataSource = ds.Tables[0];
                    drpExternalSite.DataValueField = "INVID";
                    drpExternalSite.DataTextField = "INVID";
                    drpExternalSite.DataBind();
                    drpExternalSite.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetExternalUserTemp()
        {
            try
            {
                DataSet ds = dal.CTMS_AGENDA(Action: "GET_ExternallUser", Project_ID: Session["PROJECTID"].ToString(), ID: txtAgendaID.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdExternalUser.DataSource = ds.Tables[0];
                    grdExternalUser.DataBind();
                }
                else
                {
                    grdExternalUser.DataSource = null;
                    grdExternalUser.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void drpExternalSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_AGENDA(Action: "Get_ExternalUser", Project_ID: Session["PROJECTID"].ToString(), INVID: drpExternalSite.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpExternalUser.DataSource = ds.Tables[0];
                    drpExternalUser.DataValueField = "ID";
                    drpExternalUser.DataTextField = "Name";
                    drpExternalUser.DataBind();
                    drpExternalUser.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnExternalSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ds = dal.CTMS_AGENDA(Action: "INSERT_ExternalUser", Project_ID: Session["PROJECTID"].ToString(), EMPID: drpExternalUser.SelectedValue, INVID: drpExternalSite.SelectedValue, ENTEREDBY: Session["User_ID"].ToString(), ID: txtAgendaID.Text);
                drpExternalUser.SelectedValue = "0";
                drpExternalSite.SelectedValue = "0";
                GetExternalUserTemp();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void grdExternalUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();
                Session["ID"] = id;
                if (e.CommandName == "DeleteVisit")
                {
                    DataSet ds = dal.CTMS_AGENDA(Action: "DELETE_InternalUser", Project_ID: Session["PROJECTID"].ToString(), ID: Session["ID"].ToString());
                    GetExternalUserTemp();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExternalCancel_Click(object sender, EventArgs e)
        {
            drpExternalUser.SelectedValue = "0";
            drpExternalSite.SelectedValue = "0";
            GetExternalUserTemp();
        }

        protected void btnSubmitAgenda_Click(object sender, EventArgs e)
        {
            try
            {
                string topics = null;

                ds = dal.CTMS_AGENDA(Action: "UPDATE_Agenda", Project_ID: Session["PROJECTID"].ToString(),
                AgendaID:txtAgendaID.Text,
                AgendaDT: txtAgendaDate.Text,
                AgendaTM: txtAgendaTime.Text,
                Venue: txtVanue.Text,
                ENTEREDBY: Session["User_ID"].ToString()
                );


                //Delete Topic
                ds = dal.CTMS_AGENDA
                       (
                       Action: "Delete_Topic",
                       Project_ID: Session["PROJECTID"].ToString(),
                       AgendaID: txtAgendaID.Text,                      
                       ENTEREDBY: Session["User_ID"].ToString()
                       );

                //Insert Topic
                int a = 0;
                foreach (ListItem item in lstAgendaTopic.Items)
                {
                    if (item.Selected == true)
                    {
                        ds = dal.CTMS_AGENDA
                        (
                        Action: "INSERT_Topic",
                        Project_ID: Session["PROJECTID"].ToString(),
                        ID: txtAgendaID.Text,
                        TopicID: item.Value,
                        ENTEREDBY: Session["User_ID"].ToString()
                        );

                        a = a + 1;

                        if (topics == null)
                        {
                            topics = "Following are the Topics :";
                            topics += "\n" + a.ToString() + ". " + item.Text;
                        }
                        else
                        {
                            topics += "\n" + a.ToString() + ". " + item.Text;
                        }
                    }
                }

                ds = dal.CTMS_AGENDA
                      (
                      Action: "DELETE_User",
                      Project_ID: Session["PROJECTID"].ToString(),
                      ID: txtAgendaID.Text,
                      ENTEREDBY: Session["User_ID"].ToString()
                      );

                //Interal Emplyee
                for (int i = 0; i < grdInternalUser.Rows.Count; i++)
                {
                    Label EMPID = (Label)grdInternalUser.Rows[i].FindControl("User_ID");
                    ds = dal.CTMS_AGENDA
                    (
                    Action: "INSERT_InternalUser",
                    Project_ID: Session["PROJECTID"].ToString(),
                    ID: txtAgendaID.Text,
                    EMPID: EMPID.Text,
                    ENTEREDBY: Session["User_ID"].ToString()
                    );
                }

                //Exteral Emplyee
                for (int i = 0; i < grdExternalUser.Rows.Count; i++)
                {
                    Label EMPID = (Label)grdExternalUser.Rows[i].FindControl("User_ID");
                    Label INVID = (Label)grdExternalUser.Rows[i].FindControl("INVID");
                    ds = dal.CTMS_AGENDA
                    (
                    Action: "INSERT_ExternalUser",
                    Project_ID: Session["PROJECTID"].ToString(),
                    ID: txtAgendaID.Text,
                    EMPID: EMPID.Text,
                    INVID: INVID.Text,
                    ENTEREDBY: Session["User_ID"].ToString()
                    );
                }

                DataSet mailID = dal.CTMS_AGENDA
                    (
                    Action: "GET_EmailIDs",
                    Project_ID: Session["PROJECTID"].ToString(),
                    AgendaID: txtAgendaID.Text
                    );

                string EmailIDs = null;
                for (int i = 0; i < mailID.Tables[0].Rows.Count; i++)
                {
                    EmailIDs += mailID.Tables[0].Rows[i]["EmailID"].ToString() + ";";
                }
                if (EmailIDs != null)
                {
                    CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();
                    cf.Email_Invitation(EmailAddress: EmailIDs, subject: "Reschedule Meeting Invitation from " + mailID.Tables[1].Rows[0]["User_Name"].ToString() + "", Date: Convert.ToDateTime(txtAgendaDate.Text), Time: Convert.ToDateTime(txtAgendaTime.Text), Location: txtVanue.Text, Status: "CONFIRMED", body: topics);
                }
                Response.Write("<script> alert('Record Updated successfully.');window.location='CTMS_ViewAgenda.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }


        }
    }
}