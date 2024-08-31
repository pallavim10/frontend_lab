using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class UMT_Emails : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    GET_USER_ACTIVATION();
                    GET_USER_UNLOCK();
                    GET_ASSIGN_USER_ROLE();
                    GET_USER_ROLE_CHANGE();
                    GET_SITE_ACTIVATION();
                    GET_SECURITY_QUESTION();
                    GET_SITE_USER_APPROVAL();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void INSERT_USER_ACTIVATION()
        {
            try
            {
                DataSet ds = dal.UMT_EMAIL_SP(
                              ACTION: "INSERT_USER_ACTIVATION_DEACTIVATION",
                              ACTIONS: "User Activation / Deactivation",
                              To: txtTOACEMAILIDs.InnerText,
                              CC: txtCCACTEMAILIDs.InnerText,
                              BCC: txtBCCACEMAILIDs.InnerText
                           );
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        private void GET_USER_ACTIVATION()
        {
            try
            {
                DataSet ds = dal.UMT_EMAIL_SP(
                           ACTION: "GET_USER_ACTIVATION_DEACTIVATION"
                        );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtTOACEMAILIDs.InnerText = ds.Tables[0].Rows[0]["To"].ToString();
                    txtCCACTEMAILIDs.InnerText = ds.Tables[0].Rows[0]["CC"].ToString();
                    txtBCCACEMAILIDs.InnerText = ds.Tables[0].Rows[0]["BCC"].ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        protected void btnSubmitUserActEmails_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_USER_ACTIVATION();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','User activation/deactivation email set successfully.', 'success');", true);
                GET_USER_ACTIVATION();
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void INSERT_USER_UNLOCK()
        {
            try
            {
                try
                {
                    DataSet ds = dal.UMT_EMAIL_SP(
                                  ACTION: "INSERT_USER_UNLOCK",
                                  ACTIONS: "User Unlock",
                                  To: txtTOUNLOCKEMAILIDs.InnerText,
                                  CC: txtCCUNLOCKEMAILIDs.InnerText,
                                  BCC: txtBCCUNLOCKEMAILIDs.InnerText
                               );
                }
                catch (Exception ex)
                {
                     ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        private void GET_USER_UNLOCK()
        {
            try
            {
                DataSet ds = dal.UMT_EMAIL_SP(
                           ACTION: "GET_USER_UNLOCK"
                        );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtTOUNLOCKEMAILIDs.InnerText = ds.Tables[0].Rows[0]["To"].ToString();
                    txtCCUNLOCKEMAILIDs.InnerText = ds.Tables[0].Rows[0]["CC"].ToString();
                    txtBCCUNLOCKEMAILIDs.InnerText = ds.Tables[0].Rows[0]["BCC"].ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        protected void btnSubmitUserUnlockEmails_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_USER_UNLOCK();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','User unlock email set successfully.', 'success');", true);
                GET_USER_UNLOCK();
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void INSERT_ASSIGN_USER_ROLE()
        {
            try
            {
                try
                {
                    DataSet ds = dal.UMT_EMAIL_SP(
                                  ACTION: "INSERT_ASSIGN_USER_ROLE",
                                  ACTIONS: "Assign User Role",
                                  To: txtTOROLEEMAILIDs.InnerText,
                                  CC: txtCCROLEEMAILIDs.InnerText,
                                  BCC: txtBCCROLEEMAILIDs.InnerText
                               );
                }
                catch (Exception ex)
                {
                     ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        private void GET_ASSIGN_USER_ROLE()
        {
            try
            {
                DataSet ds = dal.UMT_EMAIL_SP(
                           ACTION: "GET_ASSIGN_USER_ROLE"
                        );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtTOROLEEMAILIDs.InnerText = ds.Tables[0].Rows[0]["To"].ToString();
                    txtCCROLEEMAILIDs.InnerText = ds.Tables[0].Rows[0]["CC"].ToString();
                    txtBCCROLEEMAILIDs.InnerText = ds.Tables[0].Rows[0]["BCC"].ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        protected void btnSubmitUserRoleEmails_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_ASSIGN_USER_ROLE();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','Assign User Role email set successfully.', 'success');", true);
                GET_ASSIGN_USER_ROLE();
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void INSERT_USER_ROLE_CHANGE()
        {
            try
            {
                try
                {
                    DataSet ds = dal.UMT_EMAIL_SP(
                                  ACTION: "INSERT_USER_ROLE_CHANGE",
                                  ACTIONS: "User Role Change",
                                  To: txtTOROLECHANGEEMAILIDs.InnerText,
                                  CC: txtCCROLECHANGEEMAILIDs.InnerText,
                                  BCC: txtBCCROLECHANGEEMAILIDs.InnerText
                               );
                }
                catch (Exception ex)
                {
                     ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        private void GET_USER_ROLE_CHANGE()
        {
            try
            {
                try
                {
                    DataSet ds = dal.UMT_EMAIL_SP(
                               ACTION: "GET_USER_ROLE_CHANGE"
                            );
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtTOROLECHANGEEMAILIDs.InnerText = ds.Tables[0].Rows[0]["To"].ToString();
                        txtCCROLECHANGEEMAILIDs.InnerText = ds.Tables[0].Rows[0]["CC"].ToString();
                        txtBCCROLECHANGEEMAILIDs.InnerText = ds.Tables[0].Rows[0]["BCC"].ToString();
                    }
                }
                catch (Exception ex)
                {
                     ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        protected void btnSubmitUserRoleChangeEmails_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_USER_ROLE_CHANGE();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','User role change email set successfully.', 'success');", true);
                GET_USER_ROLE_CHANGE();
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void INSERT_SITE_ACTIVATION()
        {
            try
            {
                try
                {
                    DataSet ds = dal.UMT_EMAIL_SP(
                                  ACTION: "INSERT_SITE_ACTIVATION",
                                  ACTIONS: "Site Activation / Deactivation",
                                  To: txtTOSITEACTEMAILIDs.InnerText,
                                  CC: txtCCSITEACTMAILIDs.InnerText,
                                  BCC: txtBCCSITEACTEMAILIDs.InnerText
                               );
                }
                catch (Exception ex)
                {
                     ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        private void GET_SITE_ACTIVATION()
        {
            try
            {
                try
                {
                    DataSet ds = dal.UMT_EMAIL_SP(
                               ACTION: "GET_SITE_ACTIVATION_DEACTIVATION"
                            );
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtTOSITEACTEMAILIDs.InnerText = ds.Tables[0].Rows[0]["To"].ToString();
                        txtCCSITEACTMAILIDs.InnerText = ds.Tables[0].Rows[0]["CC"].ToString();
                        txtBCCSITEACTEMAILIDs.InnerText = ds.Tables[0].Rows[0]["BCC"].ToString();
                    }
                }
                catch (Exception ex)
                {
                     ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        protected void btnSubmitSiteActEmails_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_SITE_ACTIVATION();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','Site activation/deactivation email set successfully.', 'success');", true);
                GET_SITE_ACTIVATION();
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        protected void lbAssignINVUserExport_Click(object sender, EventArgs e)
        {
            try
            {
                GET_ALL_UserMails(header: "Emails Details", Action: "GET_ALL_EMAILS", ACTIONS : "User Activation / Deactivation");

            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void GET_ALL_UserMails(string header = null, string Action = null,string ACTIONS = null)
        {
            try
            {
                DataSet ds = new DataSet();
                string xlname = header;
                ds = dal.UMT_EMAIL_SP(ACTION: Action,ACTIONS: ACTIONS);
                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        protected void btnSubSecQuestion_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_SECURITY_QUESTION();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','Re-set security question email set successfully.', 'success');", true);
                GET_SECURITY_QUESTION();
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        private void INSERT_SECURITY_QUESTION()
        {
            try
            {
                try
                {
                    DataSet ds = dal.UMT_EMAIL_SP(
                                  ACTION: "INSERT_SECURITY_QUESTION",
                                  ACTIONS: "Reset Security Question",
                                  To: TxtTOSECUQUESTION.InnerText,
                                  CC: TxtCCSECUQUESTION.InnerText,
                                  BCC: TxtBCCSECUQUESTION.InnerText
                               );
                }
                catch (Exception ex)
                {
                     ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        private void GET_SECURITY_QUESTION()
        {
            try
            {
                try
                {
                    DataSet ds = dal.UMT_EMAIL_SP(
                               ACTION: "GET_SECURITY_QUESTION"
                            );
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        TxtTOSECUQUESTION.InnerText = ds.Tables[0].Rows[0]["To"].ToString();
                        TxtCCSECUQUESTION.InnerText = ds.Tables[0].Rows[0]["CC"].ToString();
                        TxtBCCSECUQUESTION.InnerText = ds.Tables[0].Rows[0]["BCC"].ToString();
                    }
                }
                catch (Exception ex)
                {
                     ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        protected void btnSiteUserApprovalEmails_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_SITE_USER_APPROVAL();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','Site user approval email set successfully.', 'success');", true);
                GET_SITE_USER_APPROVAL();
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        private void INSERT_SITE_USER_APPROVAL()
        {
            try
            {
                DataSet ds = dal.UMT_EMAIL_SP(
                              ACTION: "INSERT_SITE_USER_APPROVAL",
                              ACTIONS: "Site User Approval",
                              To: txtTOSiteUserAppMAILIDs.InnerText,
                              CC: txtCCSiteUserAppMAILIDs.InnerText,
                              BCC: txtBCCSiteUserAppMAILIDs.InnerText
                           );
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        private void GET_SITE_USER_APPROVAL()
        {
            try
            {
                try
                {
                    DataSet ds = dal.UMT_EMAIL_SP(
                               ACTION: "GET_SITE_USER_APPROVAL"
                            );
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtTOSiteUserAppMAILIDs.InnerText = ds.Tables[0].Rows[0]["To"].ToString();
                        txtCCSiteUserAppMAILIDs.InnerText = ds.Tables[0].Rows[0]["CC"].ToString();
                        txtBCCSiteUserAppMAILIDs.InnerText = ds.Tables[0].Rows[0]["BCC"].ToString();
                    }
                }
                catch (Exception ex)
                {
                     ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        protected void lbtUnlockExport_Click(object sender, EventArgs e)
        {
            try
            {
                GET_ALL_UserMails(header: "Emails Details", Action: "GET_ALL_EMAILS", ACTIONS: "User Unlock");

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void lbtnAssUserRolExport_Click(object sender, EventArgs e)
        {
            try
            {
                GET_ALL_UserMails(header: "Emails Details", Action: "GET_ALL_EMAILS", ACTIONS: "Assign User Role");

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void lbtnExpRolechaange_Click(object sender, EventArgs e)
        {
            try
            {
                GET_ALL_UserMails(header: "Emails Details", Action: "GET_ALL_EMAILS", ACTIONS: "User Role Change");

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void lbtnSiteAcDeacExport_Click(object sender, EventArgs e)
        {
            try
            {
                GET_ALL_UserMails(header: "Emails Details", Action: "GET_ALL_EMAILS", ACTIONS: "Site Activation / Deactivation");

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void lbtnReSetSecExport_Click(object sender, EventArgs e)
        {
            try
            {
                GET_ALL_UserMails(header: "Emails Details", Action: "GET_ALL_EMAILS", ACTIONS: "Reset Security Question");

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void lbtnSiteAppExport_Click(object sender, EventArgs e)
        {
            try
            {
                GET_ALL_UserMails(header: "Emails Details", Action: "GET_ALL_EMAILS", ACTIONS: "Site User Approval");

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}