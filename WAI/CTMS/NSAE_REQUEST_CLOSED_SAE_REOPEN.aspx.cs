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
    public partial class NSAE_REQUEST_CLOSED_SAE_REOPEN : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    FillINV();
                    GETDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP();

                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataValueField = "INVNAME";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--Select--", "0"));

                FillSubject();
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
                DataSet ds = dal.SAE_ADD_UPDATE(ACTION: "GET_SAE_SUBJECTS", INVID: drpInvID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--Select--", "0"));
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
                GET_SAEID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSAEID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SAEID()
        {
            try
            {
                DataSet ds = dal.SAE_ADD_UPDATE_NEW(ACTION: "GET_CLOSED_SAEIDS", SUBJECTID: drpSubID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSAEID.DataSource = ds.Tables[0];
                    drpSAEID.DataValueField = "SAEID";
                    drpSAEID.DataTextField = "SAEID";
                    drpSAEID.DataBind();
                    drpSAEID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSAEID.Items.Clear();
                }
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
                dal.SAE_ADD_UPDATE_NEW(ACTION: "INSERT_SAE_DCF",
                INVID: drpInvID.SelectedValue,
                SUBJECTID: drpSubID.SelectedValue,
                SAEID: drpSAEID.SelectedValue,
                Descrip: txtComment.Text
                );

                SendEmail(hdnStepId.Value);

                Response.Redirect("NSAE_REQUEST_CLOSED_SAE_REOPEN.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal.SAE_ADD_UPDATE_NEW(ACTION: "GET_DCF_DATA",
                INVID: drpInvID.SelectedValue,
                SUBJECTID: drpSubID.SelectedValue,
                SAEID: drpSAEID.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdSAE.DataSource = ds;
                    grdSAE.DataBind();
                }
                else
                {
                    grdSAE.DataSource = null;
                    grdSAE.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SendEmail(string StepID)
        {
            try
            {
                DataSet ds1 = dal.SAE_ADD_UPDATE(ACTION: "GET_EMAIL_IDS_WITH_DATA",
                        SUBJECTID: drpSubID.SelectedValue,
                        INVID: drpInvID.SelectedValue,
                        SAEID: drpSAEID.SelectedValue,
                        ID: StepID
                        );

                if (ds1.Tables[0].Rows.Count > 0)
                {

                    string EMAILID = ds1.Tables[0].Rows[0]["EMAILIDS"].ToString();

                    string EMAIL_CC = ds1.Tables[0].Rows[0]["CCEMAILIDS"].ToString();

                    string EMAIL_BCC = ds1.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();

                    string BODY = ds1.Tables[0].Rows[0]["MAIL_BODY"].ToString().Replace("[Date of Onset of Event]", ds1.Tables[0].Rows[0]["ONSETDAT"].ToString());

                    BODY = BODY.Replace("[Subject ID]", Request.QueryString["SUBJID"].ToString());

                    BODY = BODY.Replace("[SAE Term]", ds1.Tables[0].Rows[0]["SAETERM"].ToString());

                    BODY = BODY.Replace("[Site Name]", ds1.Tables[0].Rows[0]["INSTNAM"].ToString());

                    string SUBJECT = ds1.Tables[0].Rows[0]["MAIL_SUBJECT"].ToString().Replace("[SAEID]", drpSAEID.SelectedValue);

                    CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();

                    commFun.Email_Users(EMAILID, EMAIL_CC, SUBJECT, BODY, EMAIL_BCC);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;

            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
                {
                    //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gv.ShowFooter == true && gv.Rows.Count > 0)
                {
                    //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                    gv.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }
    }
}