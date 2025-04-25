using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Text.RegularExpressions;

namespace CTMS
{
    public partial class CTMS_ENROLLMENT_PLAN : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["UserGroup_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }
                FillINV();
                GETDATA();
                GETDATA1();
            }
        }

        public void FillINV()
        {
            try
            {
                DataSet ds = dal.GetSiteID(
                Action: "INVID",
                PROJECTID: Session["PROJECTID"].ToString(),
                User_ID: Session["User_ID"].ToString()
                );
                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataValueField = "INVNAME";
                drpInvID.DataBind();

                drpINVID1.DataSource = ds.Tables[0];
                drpINVID1.DataValueField = "INVNAME";
                drpINVID1.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void txtEnrollSteps_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_ENROLLMENT_PLAN(Action: "INSERT_ENROLL_DATA", INVID: drpInvID.SelectedValue,
                ENROLLMENT_STEPS: txtEnrollSteps.Text, TYPE: "Enrollment Plan",EnrollmentStartMonth:drpMonth.SelectedItem.Text,
                EnrollmentStartYear:drpYear.SelectedValue);
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GETDATA()
        {
            try
            {
                DataSet ds = dal.CTMS_ENROLLMENT_PLAN(
                Action: "GET_ENROLL_DATA",
                INVID: drpInvID.SelectedValue,
                TYPE: "Enrollment Plan"
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtEnrollSteps.Text = ds.Tables[0].Rows.Count.ToString();
                    grdEnrollmentStep.DataSource = ds.Tables[0];
                    grdEnrollmentStep.DataBind();
                }
                else
                {
                    txtEnrollSteps.Text = "";
                    grdEnrollmentStep.DataSource = null;
                    grdEnrollmentStep.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnEnrollSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdEnrollmentStep.Rows.Count; i++)
                {
                    string MONTHS = ((Label)grdEnrollmentStep.Rows[i].FindControl("lblmonth")).Text;
                    string SUBJECTS = ((TextBox)grdEnrollmentStep.Rows[i].FindControl("lblsubjects")).Text;

                    DataSet ds = dal.CTMS_ENROLLMENT_PLAN(Action: "UPDATE_ENROLL_DATA",
                    MONTHS: MONTHS,
                    NO_OF_SUBJECTS: SUBJECTS,
                    ENROLLMENT_STEPS: txtEnrollSteps.Text,
                    INVID: drpInvID.SelectedValue,
                    TYPE: "Enrollment Plan"
                    );
                }
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpINVID1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETDATA1();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void txtRand_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_ENROLLMENT_PLAN(Action: "INSERT_ENROLL_DATA", INVID: drpINVID1.SelectedValue,
                ENROLLMENT_STEPS: txtRand.Text, TYPE: "Randmization Plan",EnrollmentStartMonth:drpMonth1.SelectedItem.Text,
                EnrollmentStartYear:drpYear1.SelectedValue);
                GETDATA1();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GETDATA1()
        {
            try
            {
                DataSet ds = dal.CTMS_ENROLLMENT_PLAN(
                Action: "GET_ENROLL_DATA",
                INVID: drpINVID1.SelectedValue,
                TYPE: "Randmization Plan"
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRand.Text = ds.Tables[0].Rows.Count.ToString();
                    grdRad.DataSource = ds.Tables[0];
                    grdRad.DataBind();
                }
                else
                {
                    txtRand.Text = "";
                    grdRad.DataSource = null;
                    grdRad.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnRand_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdRad.Rows.Count; i++)
                {
                    string MONTHS = ((Label)grdRad.Rows[i].FindControl("lblmonth")).Text;
                    string SUBJECTS = ((TextBox)grdRad.Rows[i].FindControl("lblsubjects")).Text;

                    DataSet ds = dal.CTMS_ENROLLMENT_PLAN(Action: "UPDATE_ENROLL_DATA",
                    MONTHS: MONTHS,
                    NO_OF_SUBJECTS: SUBJECTS,
                    ENROLLMENT_STEPS: txtEnrollSteps.Text,
                    INVID: drpInvID.SelectedValue,
                    TYPE: "Randmization Plan"
                    );
                }
                GETDATA1();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}