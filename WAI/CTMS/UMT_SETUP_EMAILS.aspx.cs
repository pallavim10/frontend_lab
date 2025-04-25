using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class UMT_SETUP_EMAILS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_EMAILIDS_SITE("Site Activation / Deactivation");
                    GET_EMAILIDS_USERS("User Activation / Deactivation");
                    GET_EMAILIDS_USER_UNLOCK("User Unlock");
                    GET_EMAILIDS_USER_ROLE("User Role");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_EMAILIDS_SITE(string ACTIONS)
        {
            try
            {
                DataSet ds = dal.UMT_EMAIL_SP(ACTION: "GET_SITE_ACTIVATION_DEACTIVATION", ACTIONS: ACTIONS);
                gvSite.DataSource = ds;
                gvSite.DataBind();
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_EMAILIDS_USERS(string ACTIONS)
        {
            try
            {
                DataSet ds = dal.UMT_EMAIL_SP(ACTION: "GET_USER_ACTIVATION_DEACTIVATION", ACTIONS: ACTIONS);
                gvUsers.DataSource = ds;
                gvUsers.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_EMAILIDS_USER_UNLOCK(string ACTIONS)
        {
            try
            {
                DataSet ds = dal.UMT_EMAIL_SP(ACTION: "GET_USER_UNLOCK", ACTIONS: ACTIONS);
                gvUserUnlock.DataSource = ds;
                gvUserUnlock.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_EMAILIDS_USER_ROLE(string ACTIONS)
        {
            try
            {
                DataSet ds = dal.UMT_EMAIL_SP(ACTION: "GET_USER_ROLE", ACTIONS: ACTIONS);
                gvUserRole.DataSource = ds;
                gvUserRole.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitSite_Click(object sender, EventArgs e)
        {
            try
            {
                IINSERT_SITE_ACTIVATION_DEACTIVATION();

                Response.Write("<script> alert('Insert Site Activation/Deactivation Emails IDs Successfully');</script>");

                GET_EMAILIDS_SITE("Site Activation / Deactivation");
                
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void IINSERT_SITE_ACTIVATION_DEACTIVATION()
        {
            try
            {
                string ACTIONS = "Site Activation / Deactivation";
                for (int a = 0; a < gvSite.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvSite.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvSite.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvSite.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    DataSet ds = dal.UMT_EMAIL_SP(
                        ACTION: "INSERT_SITE_ACTIVATION_DEACTIVATION",
                        To: txtEMAILIDs.Text,
                        CC: txtCCEMAILIDs.Text,
                        BCC: txtBCCEMAILIDs.Text,
                        ACTIONS: ACTIONS
                        );
                }
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void btnCancelSite_Click(object sender, EventArgs e)
        {
            GET_EMAILIDS_SITE("Site Activation / Deactivation");
        }

        protected void btnUsrActiSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_USER_ACTIVATION_DEACTIVATION();

                Response.Write("<script> alert('Insert User Activation/Deactivation Emails IDs Successfully');</script>");

                GET_EMAILIDS_USERS("User Activation / Deactivation");
               
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_USER_ACTIVATION_DEACTIVATION()
        {
            try
            {
                string ACTIONS = "User Activation / Deactivation";
                for (int a = 0; a < gvUsers.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvUsers.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvUsers.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvUsers.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    DataSet ds = dal.UMT_EMAIL_SP(
                        ACTION: "INSERT_USER_ACTIVATION_DEACTIVATION",
                        To: txtEMAILIDs.Text,
                        CC: txtCCEMAILIDs.Text,
                        BCC: txtBCCEMAILIDs.Text,
                        ACTIONS: ACTIONS
                        );
                }
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnUsrActiCancel_Click(object sender, EventArgs e)
        {
            GET_EMAILIDS_USERS("User Activation / Deactivation");
        }

        protected void btnUserUnlockSub_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_USER_UNLOCK();

                Response.Write("<script> alert('Insert User Unlock Emails IDs Successfully');</script>");

                GET_EMAILIDS_USER_UNLOCK("User Unlock");
               
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_USER_UNLOCK()
        {
            try
            {
                string ACTIONS = "User Unlock";
                for (int a = 0; a < gvUserUnlock.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvUserUnlock.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvUserUnlock.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvUserUnlock.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    DataSet ds = dal.UMT_EMAIL_SP(
                        ACTION: "INSERT_USER_UNLOCK",
                        To: txtEMAILIDs.Text,
                        CC: txtCCEMAILIDs.Text,
                        BCC: txtBCCEMAILIDs.Text,
                        ACTIONS: ACTIONS
                        );
                }
                
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUserUnlockCan_Click(object sender, EventArgs e)
        {
            GET_EMAILIDS_USER_UNLOCK("User Unlock");
        }

        protected void btnUserRoleSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_USER_ROLE();

                Response.Write("<script> alert('Insert User role Emails IDs Successfully');</script>");

                GET_EMAILIDS_USER_ROLE("User Role");

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_USER_ROLE()
        {
            try
            {
                string ACTIONS = "User Role";

                for (int a = 0; a < gvUserRole.Rows.Count; a++)
                {
                    TextBox txtEMAILIDs = gvUserRole.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvUserRole.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvUserRole.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    DataSet ds = dal.UMT_EMAIL_SP(
                        ACTION: "INSERT_USER_ROLE",
                        To: txtEMAILIDs.Text,
                        CC: txtCCEMAILIDs.Text,
                        BCC: txtBCCEMAILIDs.Text,
                        ACTIONS: ACTIONS
                        );
                }
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnUserRoleCancel_Click(object sender, EventArgs e)
        {
            GET_EMAILIDS_USER_ROLE("User Role");
        }

    }
}