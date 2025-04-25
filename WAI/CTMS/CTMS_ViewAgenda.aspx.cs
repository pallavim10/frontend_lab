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
    public partial class CTMS_ViewAgenda : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    txtToDate.Text = DateTime.Today.Date.ToString("dd-MMM-yyyy");
                    DateTime FromDate = new DateTime();
                    FromDate = Convert.ToDateTime(txtToDate.Text);
                    FromDate = FromDate.AddDays(-30);
                    txtFromDate.Text = FromDate.ToString("dd-MMM-yyyy");        
                    FillStatus();
                    GetMeeting();                         
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillStatus()
        {
            try
            {
                DataSet ds = dal.CTMS_AGENDA(Action: "GET_STATUS", Project_ID: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpStatus.DataSource = ds.Tables[0];
                    drpStatus.DataValueField = "StatusID";
                    drpStatus.DataTextField = "Status";
                    drpStatus.DataBind();
                    drpStatus.Items.Insert(0, new ListItem("--All--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetMeeting()
        {
            try
            {
                ds = dal.CTMS_AGENDA(Action: "Get_MeetingList", Project_ID: Session["PROJECTID"].ToString(), FromDate: txtFromDate.Text, ToDate: txtToDate.Text, StatusID: drpStatus.SelectedValue);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdMeeting.DataSource = ds.Tables[0];
                        grdMeeting.DataBind();
                    }
                    else
                    {
                        grdMeeting.DataSource = null;
                        grdMeeting.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdMeeting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();
                Session["ID"] = id;
                if (e.CommandName == "Completed")
                {
                    DataSet ds = dal.CTMS_AGENDA(Action: "UPDATE_Meeting", Project_ID: Session["PROJECTID"].ToString(), ID: Session["ID"].ToString(), StatusID: "2");
                    GetMeeting();
                }
                if (e.CommandName == "Reschedule")
                {
                    DataSet ds = dal.CTMS_AGENDA(Action: "RESCHEDULE_AGENDA", Project_ID: Session["PROJECTID"].ToString(), ID: Session["ID"].ToString(), StatusID: "3");

                    Session["AgendaID"] = ds.Tables[0].Rows[0]["AgendaID"].ToString();
                    Response.Redirect("CTMS_CreateRescheduleAgenda.aspx");
                }
                if (e.CommandName == "FollowUp")
                {
                    DataSet ds = dal.CTMS_AGENDA(Action: "FOLLOWUP_AGENDA", Project_ID: Session["PROJECTID"].ToString(), ID: Session["ID"].ToString(), StatusID: "4");

                    Session["AgendaID"] = ds.Tables[0].Rows[0]["AgendaID"].ToString();
                    Response.Redirect("CTMS_CreateRescheduleAgenda.aspx");
                }
                if (e.CommandName == "Cancelled")
                {
                    DataSet ds = dal.CTMS_AGENDA(Action: "UPDATE_Meeting", Project_ID: Session["PROJECTID"].ToString(), ID: Session["ID"].ToString(), StatusID: "5");
                    GetMeeting();

                    DataSet mailID = dal.CTMS_AGENDA
                    (
                    Action: "GET_EmailIDs",
                    Project_ID: Session["PROJECTID"].ToString(),
                    AgendaID: id
                    );

                    string EmailIDs = null;
                    for (int i = 0; i < mailID.Tables[0].Rows.Count; i++)
                    {
                        EmailIDs += mailID.Tables[0].Rows[i]["EmailID"].ToString() + ";";
                    }

                    DataSet DateTime = dal.CTMS_AGENDA(Action: "GET_AGENDA_DETAILS", Project_ID: Session["PROJECTID"].ToString(), ID: Session["AgendaID"].ToString());

                    CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();
                    cf.Email_Invitation(EmailAddress: EmailIDs, subject: "Meeting Cancelled", Date: Convert.ToDateTime(DateTime.Tables[0].Rows[0]["AgendaDT"].ToString()), Time: Convert.ToDateTime(DateTime.Tables[0].Rows[0]["AgendaTM"].ToString()), Status: "CANCELLED", Location: DateTime.Tables[0].Rows[0]["Venue"].ToString());
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void grdMeeting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string StatusID = dr["StatusID"].ToString();
                    LinkButton lnkCompleted = (LinkButton)e.Row.FindControl("lnkCompleted");
                    LinkButton lnkreschedule = (LinkButton)e.Row.FindControl("lnkreschedule");
                    LinkButton lnkFollowUp = (LinkButton)e.Row.FindControl("lnkFollowUp");
                    LinkButton lnkCancel = (LinkButton)e.Row.FindControl("lnkCancel");

                    if (StatusID == "1")
                    {
                        lnkFollowUp.Visible = false;
                    }
                    if (StatusID == "2")
                    {
                        lnkreschedule.Visible = false;
                        lnkCompleted.Visible = false;
                        lnkCancel.Visible = false;
                    }
                    if (StatusID == "3")
                    {
                        lnkCompleted.Visible = false;
                        lnkFollowUp.Visible = false;
                        lnkreschedule.Visible = false;
                        lnkCancel.Visible = false;
                    }
                    if (StatusID == "4")
                    {
                        lnkCompleted.Visible = false;
                        lnkFollowUp.Visible = false;
                        lnkreschedule.Visible = false;
                        lnkCancel.Visible = false;
                    }
                    if (StatusID == "5")
                    {
                        lnkCompleted.Visible = false;
                        lnkFollowUp.Visible = false;
                        lnkreschedule.Visible = false;
                        lnkCancel.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                GetMeeting();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }
    }
}