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
    public partial class CTMS_CreateAgendaMom : System.Web.UI.Page
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
                    txtFromDate.Text = FromDate.ToString();
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
                DataSet ds = dal.CTMS_AGENDA(Action: "GET_STATUS_MOM", Project_ID: Session["PROJECTID"].ToString());
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
                ds = dal.CTMS_AGENDA(Action: "Get_MeetingList_MOM", Project_ID: Session["PROJECTID"].ToString(), FromDate: txtFromDate.Text, ToDate: txtToDate.Text, StatusID: drpStatus.SelectedValue);

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

  

        protected void grdMeeting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string DocExist = dr["DocExist"].ToString();
                    LinkButton lbtnDownloadDoc = (LinkButton)e.Row.FindControl("lbtnDownloadDoc");

                    if (DocExist != "0")
                    {
                        lbtnDownloadDoc.Visible = true;
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