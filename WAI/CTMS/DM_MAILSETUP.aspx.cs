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
    public partial class DM_MAILSETUP : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_EMAILIDS("Freezed");
                    GET_EMAILIDS("UnFreezRequest");
                    GET_EMAILIDS("UnFreezed");
                    GET_EMAILIDS("Locked");
                    GET_EMAILIDS("UnLockRequest");
                    GET_EMAILIDS("UnLocked");
                    GET_EMAILIDS("INVSIGNOFF");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_EMAILIDS(string TYPE)
        {
            try
            {
                if (TYPE == "Freezed")
                {
                    DataSet ds = dal.GetSet_DM_ProjectData(Action: "GET_EMAILS_DM_SETUP", CONTROLTYPE: "Freezed");
                    grdFreezed.DataSource = ds;
                    grdFreezed.DataBind();

                    txtSubjectFreezed.Text = ds.Tables[0].Rows[0]["Email_Subject"].ToString();
                    txtBodyFreezed.Text = ds.Tables[0].Rows[0]["Email_Body"].ToString();
                }
                else if (TYPE == "UnFreezRequest")
                {
                    DataSet ds = dal.GetSet_DM_ProjectData(Action: "GET_EMAILS_DM_SETUP", CONTROLTYPE: "UnFreezRequest");
                    grdUnFreezRequest.DataSource = ds;
                    grdUnFreezRequest.DataBind();
                }
                else if (TYPE == "UnFreezed")
                {
                    DataSet ds = dal.GetSet_DM_ProjectData(Action: "GET_EMAILS_DM_SETUP", CONTROLTYPE: "UnFreezed");
                    grdUnFreezed.DataSource = ds;
                    grdUnFreezed.DataBind();

                    txtSubjectUnfreezed.Text = ds.Tables[0].Rows[0]["Email_Subject"].ToString();
                    txtBodyUnfreezed.Text = ds.Tables[0].Rows[0]["Email_Body"].ToString();
                }
                else if (TYPE == "Locked")
                {
                    DataSet ds = dal.GetSet_DM_ProjectData(Action: "GET_EMAILS_DM_SETUP", CONTROLTYPE: "Locked");
                    grdLocked.DataSource = ds;
                    grdLocked.DataBind();

                    txtSubjectLocked.Text = ds.Tables[0].Rows[0]["Email_Subject"].ToString();
                    txtBodyLocked.Text = ds.Tables[0].Rows[0]["Email_Body"].ToString();
                }
                else if (TYPE == "UnLockRequest")
                {
                    DataSet ds = dal.GetSet_DM_ProjectData(Action: "GET_EMAILS_DM_SETUP", CONTROLTYPE: "UnLockRequest");
                    grdUnlockRequest.DataSource = ds;
                    grdUnlockRequest.DataBind();

                }
                else if (TYPE == "UnLocked")
                {
                    DataSet ds = dal.GetSet_DM_ProjectData(Action: "GET_EMAILS_DM_SETUP", CONTROLTYPE: "UnLocked");
                    grdUnLocked.DataSource = ds;
                    grdUnLocked.DataBind();

                    txtSubjectUnLocked.Text = ds.Tables[0].Rows[0]["Email_Subject"].ToString();
                    txtBodyUnlocked.Text = ds.Tables[0].Rows[0]["Email_Body"].ToString();
                }
                else if (TYPE == "INVSIGNOFF")
                {
                    DataSet ds = dal.GetSet_DM_ProjectData(Action: "GET_EMAILS_DM_SETUP", CONTROLTYPE: "INVSIGNOFF");
                    grdINVSignOff.DataSource = ds;
                    grdINVSignOff.DataBind();

                    txtSubjectINVSignOff.Text = ds.Tables[0].Rows[0]["Email_Subject"].ToString();
                    txtBodyINVSignOff.Text = ds.Tables[0].Rows[0]["Email_Body"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitFreezed_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_Freezed_EMAILIDS();
                GET_EMAILIDS("Freezed");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Freezed Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelFreezed_Click(object sender, EventArgs e)
        {
            try
            {
                GET_EMAILIDS("Freezed");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_Freezed_EMAILIDS()
        {
            for (int a = 0; a < grdFreezed.Rows.Count; a++)
            {
                Label lblSiteID = grdFreezed.Rows[a].FindControl("lblSiteID") as Label;
                TextBox txtEMAILIDs = grdFreezed.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                TextBox txtCCEMAILIDs = grdFreezed.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                TextBox txtBCCEMAILIDs = grdFreezed.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                dal.GetSet_DM_ProjectData
                    (
                    Action: "INSERT_SETUP_EMAILIDS",
                    EMAIL_IDS: txtEMAILIDs.Text,
                    CCEMAIL_IDS: txtCCEMAILIDs.Text,
                    BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                    INVID: lblSiteID.Text,
                    CONTROLTYPE: "Freezed",
                    Email_Subject: txtSubjectFreezed.Text,
                    Email_body: txtBodyFreezed.Text
                    );
            }

        }

        protected void btnSubmitUnFreezRequest_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_UnFreezRequest_EMAILIDS();
                GET_EMAILIDS("UnFreezRequest");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('UnFreez Request Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelUnFreezRequest_Click(object sender, EventArgs e)
        {
            try
            {
                GET_EMAILIDS("UnFreezRequest");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_UnFreezRequest_EMAILIDS()
        {
            for (int a = 0; a < grdUnFreezRequest.Rows.Count; a++)
            {
                Label lblSiteID = grdUnFreezRequest.Rows[a].FindControl("lblSiteID") as Label;
                TextBox txtEMAILIDs = grdUnFreezRequest.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                TextBox txtCCEMAILIDs = grdUnFreezRequest.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                TextBox txtBCCEMAILIDs = grdUnFreezRequest.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                dal.GetSet_DM_ProjectData
                    (
                    Action: "INSERT_SETUP_EMAILIDS",
                    EMAIL_IDS: txtEMAILIDs.Text,
                    CCEMAIL_IDS: txtCCEMAILIDs.Text,
                    BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                    INVID: lblSiteID.Text,
                    CONTROLTYPE: "UnFreezRequest"
                    );
            }
        }

        protected void btnSubmitUnFreezed_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_UnFreezed_EMAILIDS();
                GET_EMAILIDS("UnFreezed");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('UnFreezed Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelUnFreezed_Click(object sender, EventArgs e)
        {
            try
            {
                GET_EMAILIDS("UnFreezed");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_UnFreezed_EMAILIDS()
        {
            for (int a = 0; a < grdUnFreezed.Rows.Count; a++)
            {
                Label lblSiteID = grdUnFreezed.Rows[a].FindControl("lblSiteID") as Label;
                TextBox txtEMAILIDs = grdUnFreezed.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                TextBox txtCCEMAILIDs = grdUnFreezed.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                TextBox txtBCCEMAILIDs = grdUnFreezed.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                dal.GetSet_DM_ProjectData
                    (
                    Action: "INSERT_SETUP_EMAILIDS",
                    EMAIL_IDS: txtEMAILIDs.Text,
                    CCEMAIL_IDS: txtCCEMAILIDs.Text,
                    BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                    INVID: lblSiteID.Text,
                    CONTROLTYPE: "UnFreezed",
                    Email_Subject: txtSubjectUnfreezed.Text,
                    Email_body: txtBodyUnfreezed.Text
                    );
            }

        }

        protected void btnSubmitLocked_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_Locked_EMAILIDS();
                GET_EMAILIDS("Locked");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Locked Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelLocked_Click(object sender, EventArgs e)
        {
            try
            {
                GET_EMAILIDS("Locked");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_Locked_EMAILIDS()
        {
            for (int a = 0; a < grdLocked.Rows.Count; a++)
            {
                Label lblSiteID = grdLocked.Rows[a].FindControl("lblSiteID") as Label;
                TextBox txtEMAILIDs = grdLocked.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                TextBox txtCCEMAILIDs = grdLocked.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                TextBox txtBCCEMAILIDs = grdLocked.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                dal.GetSet_DM_ProjectData
                    (
                    Action: "INSERT_SETUP_EMAILIDS",
                    EMAIL_IDS: txtEMAILIDs.Text,
                    CCEMAIL_IDS: txtCCEMAILIDs.Text,
                    BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                    INVID: lblSiteID.Text,
                    CONTROLTYPE: "Locked",
                    Email_Subject: txtSubjectLocked.Text,
                    Email_body: txtBodyLocked.Text
                    );
            }

        }

        protected void btnSubmitUnlockRequest_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_UnLockRequest_EMAILIDS();
                GET_EMAILIDS("UnLockRequest");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('UnLock Request Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelUnlockRequest_Click(object sender, EventArgs e)
        {
            try
            {
                GET_EMAILIDS("UnLockRequest");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_UnLockRequest_EMAILIDS()
        {
            for (int a = 0; a < grdUnlockRequest.Rows.Count; a++)
            {
                Label lblSiteID = grdUnlockRequest.Rows[a].FindControl("lblSiteID") as Label;
                TextBox txtEMAILIDs = grdUnlockRequest.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                TextBox txtCCEMAILIDs = grdUnlockRequest.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                TextBox txtBCCEMAILIDs = grdUnlockRequest.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                dal.GetSet_DM_ProjectData
                    (
                    Action: "INSERT_SETUP_EMAILIDS",
                    EMAIL_IDS: txtEMAILIDs.Text,
                    CCEMAIL_IDS: txtCCEMAILIDs.Text,
                    BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                    INVID: lblSiteID.Text,
                    CONTROLTYPE: "UnLockRequest"
                    );
            }

        }

        protected void btnSubmitUnLocked_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_UnLocked_EMAILIDS();
                GET_EMAILIDS("UnLocked");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('UnLocked Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelUnLocked_Click(object sender, EventArgs e)
        {
            try
            {
                GET_EMAILIDS("UnLocked");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_UnLocked_EMAILIDS()
        {
            for (int a = 0; a < grdUnLocked.Rows.Count; a++)
            {
                Label lblSiteID = grdUnLocked.Rows[a].FindControl("lblSiteID") as Label;
                TextBox txtEMAILIDs = grdUnLocked.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                TextBox txtCCEMAILIDs = grdUnLocked.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                TextBox txtBCCEMAILIDs = grdUnLocked.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                dal.GetSet_DM_ProjectData
                    (
                    Action: "INSERT_SETUP_EMAILIDS",
                    EMAIL_IDS: txtEMAILIDs.Text,
                    CCEMAIL_IDS: txtCCEMAILIDs.Text,
                    BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                    INVID: lblSiteID.Text,
                    CONTROLTYPE: "UnLocked",
                    Email_Subject: txtSubjectUnLocked.Text,
                    Email_body: txtBodyUnlocked.Text
                    );
            }

        }

        protected void btnSubmitINVSIGNOFF_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_INSIGNOFF_EMAILIDS();
                GET_EMAILIDS("INVSIGNOFF");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Investigator Sign Off Email IDs Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCANCELINVSIGNOFF_Click(object sender, EventArgs e)
        {
            try
            {
                GET_EMAILIDS("INVSIGNOFF");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_INSIGNOFF_EMAILIDS()
        {
            for (int a = 0; a < grdINVSignOff.Rows.Count; a++)
            {
                Label lblSiteID = grdINVSignOff.Rows[a].FindControl("lblSiteID") as Label;
                TextBox txtEMAILIDs = grdINVSignOff.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                TextBox txtCCEMAILIDs = grdINVSignOff.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                TextBox txtBCCEMAILIDs = grdINVSignOff.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                dal.GetSet_DM_ProjectData
                    (
                    Action: "INSERT_SETUP_EMAILIDS",
                    EMAIL_IDS: txtEMAILIDs.Text,
                    CCEMAIL_IDS: txtCCEMAILIDs.Text,
                    BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                    INVID: lblSiteID.Text,
                    CONTROLTYPE: "INVSIGNOFF",
                    Email_Subject: txtSubjectINVSignOff.Text,
                    Email_body: txtBodyINVSignOff.Text
                    );
            }

        }
    }
}