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
    public partial class DM_PATIENT_REG : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["PVID"] = null;
                if (!this.IsPostBack)
                {
                    if (Session["UserGroup_ID"] == null)
                    {
                        Response.Redirect("SessionExpired.aspx", true);
                        return;
                    }
                    else
                    {
                        FillINV();
                        GetIndication();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void FillINV()
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.GET_INVID_SP();
            drpInvID.DataSource = ds.Tables[0];
            drpInvID.DataValueField = "INVNAME";
            drpInvID.DataBind();

            FillSubject();
            if (Session["UserGroup_ID"].ToString() == "Investigator" || Session["UserGroup_ID"].ToString() == "Co_Investigator")
            {
                string Userid = Session["User_ID"].ToString();
                // DivINV.Visible = false;
            }
        }

        public void GetIndication()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_INDICATION", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpIndication.DataSource = ds.Tables[0];
                    drpIndication.DataValueField = "ID";
                    drpIndication.DataTextField = "INDICATION";
                    drpIndication.DataBind();
                    drpIndication.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_SUBJECT_UnReg", PROJECTID: Session["PROJECTID"].ToString(), INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--Select--", "Select"));
                }
                else
                {
                    drpSubID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                GETPATIENTDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpIndication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                GETPATIENTDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.getSetDM(Action: "INSERT_PATIENT_REG", INVID: drpInvID.SelectedValue, VERSIONID: drpIndication.SelectedValue, SUBJID: drpSubID.SelectedItem.Text, BRTHDAT: txtDOB.Text, SEX: drp_GENDER.SelectedValue, SAESUBJINI: txtInitials.Text, ENTEREDBY: Session["USER_ID"].ToString());
                GETPATIENTDATA();
                Clear();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GETPATIENTDATA()
        {
            try
            {
                DataSet ds = dal.getSetDM(Action: "GET_PATIENT_REG", INVID: drpInvID.SelectedValue, VERSIONID: drpIndication.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grd_PatientReg.DataSource = ds;
                    Grd_PatientReg.DataBind();
                }
                else
                {
                    Grd_PatientReg.DataSource = null;
                    Grd_PatientReg.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtDOB.Text = "";
            txtInitials.Text = "";
            drp_GENDER.SelectedIndex = 0;
            drpInvID.SelectedIndex = Convert.ToInt32(drpInvID.SelectedIndex);
            drpSubID.SelectedIndex = Convert.ToInt32(drpSubID.SelectedIndex);
        }

        protected void Grd_PatientReg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = Convert.ToString(e.CommandArgument);
                if (e.CommandName == "reg_EDIT")
                {
                    DataSet ds = dal.getSetDM(Action: "EDIT_PATIENT_REG", SUBJID: id);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        drpInvID.SelectedValue = ds.Tables[0].Rows[0]["INVID"].ToString();
                        drpIndication.SelectedValue = ds.Tables[0].Rows[0]["Indication"].ToString();
                        drpSubID.SelectedItem.Text = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                        drpSubID.Enabled = false;
                        txtInitials.Text = ds.Tables[0].Rows[0]["DM_INTIALS"].ToString();
                        txtDOB.Text = ds.Tables[0].Rows[0]["DM_DOBDAT"].ToString();
                        drp_GENDER.SelectedValue = ds.Tables[0].Rows[0]["DM_GENDER"].ToString();
                    }
                }
                else if (e.CommandName == "reg_DELETE")
                {
                    DataSet ds = dal.getSetDM(Action: "DELETE_PATIENT_REG", SUBJID: id);
                    GETPATIENTDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSubID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETPATIENTDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}