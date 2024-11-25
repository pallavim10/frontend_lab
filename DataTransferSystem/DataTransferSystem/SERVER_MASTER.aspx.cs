using System;
using DataTransferSystem.App_Code;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DataTransferSystem
{
    public partial class SERVER_MASTER : System.Web.UI.Page
    {
        DAL_SERVER_MASTER dal = new DAL_SERVER_MASTER();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_SERVERMASTER();
                }
            }
            catch (Exception ex) 
            {
                ex.StackTrace.ToString();
            }

        }
        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid(txtServername.Text.Trim()) && IsValid(txtuserID.Text.Trim()) && IsValid(txtpassword.Text.Trim()))
                {
                    if(IfExists(txtServername.Text.Trim()) == false) 
                    {
                        INSERT_SERVERDETAILS();
                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Server Details Already Exists.','warning');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Enter Vaild Server Details.','warning');", true);
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid(txtServername.Text.Trim()) && IsValid(txtuserID.Text.Trim()) && IsValid(txtpassword.Text.Trim()))

                {
                    if ((IfExists(txtServername.Text.Trim()) == false) && (txtuserID.Text.Trim() != hdnUserID.Value.Trim()) && (txtpassword.Text.Trim() != hdnPassword.Value.Trim()))
                    {
                        UPDATE_SERVERDETAILS();
                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Server Details Already Exists.','warning');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Enter vaild Server Details.','warning');", true);
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }

        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_SERVERDT();
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }

        private void INSERT_SERVERDETAILS()
        {
            try
            {
                DataSet da = dal.SERVER_MASTER_SP(
                    ACTION: "INSERT_SERVERDETAILS",
                    SERVERNAME: txtServername.Text,
                    USER_ID: txtuserID.Text,
                    PASSWORD: txtpassword.Text
                    );

                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Server Details Created Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                GET_SERVERMASTER();
                CLEAR_SERVERDT();
            }
            catch (Exception ex) 
            {
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }
        }

        private void UPDATE_SERVERDETAILS()
        {
            try
            {
                DataSet da = dal.SERVER_MASTER_SP(
                    ACTION: "UPDATE_SERVERDETAILS",
                    ID: ViewState["ID"].ToString(),
                    SERVERNAME: txtServername.Text,
                    USER_ID: txtuserID.Text,
                    PASSWORD: txtpassword.Text
                    );
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Server Details Updated Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                lbtnUpdate.Visible = false;
                lbtnSubmit.Visible = true;
                GET_SERVERMASTER();
                CLEAR_SERVERDT();
            }
            catch (Exception ex) 
            {
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }
        }
        protected void GrdServerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditServerdetails" || e.CommandName == "DeleteServerdetails")
            {
                try
                {
                    string SERVERID = e.CommandArgument.ToString();
                    if (e.CommandName == "EditServerdetails")
                    {
                        ViewState["ID"] = SERVERID;
                        DataSet ds = dal.SERVER_MASTER_SP(ACTION: "EDIT_SERVERDETAILS", ID: SERVERID);
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            foreach (DataRow row in dt.Rows)
                            {
                                txtServername.Text = row["SERVERNAME"].ToString();
                                hdnServerName.Value = row["SERVERNAME"].ToString();
                                txtuserID.Text = row["USERID"].ToString();
                                hdnUserID.Value = row["USERID"].ToString();
                                txtpassword.Text = row["PASSWORD"].ToString();
                                hdnPassword.Value = row["PASSWORD"].ToString();
                            }
                        }
                        lbtnSubmit.Visible = false;
                        lbtnUpdate.Visible = true;

                    }
                    else if (e.CommandName == "DeleteServerdetails")
                    {
                        string SERVER_ID = e.CommandArgument.ToString();
                        DELETE_SERVERDETAILS(SERVER_ID);
                        GET_SERVERMASTER();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        private void DELETE_SERVERDETAILS(string SERVERID)
        {
            try
            {
                DataSet ds = dal.SERVER_MASTER_SP(ACTION: "DELETE_SERVER_RECORD", ID: SERVERID);

                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                                swal({
                                    title: 'Success!',
                                    text: 'Server Record Deleted Successfully.',
                                    icon: 'success',
                                    button: 'OK'
                                }).then(function(){
                                     window.location.href = window.location.href; });
                            ", true);


            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error!', 'Failed to delete record.', 'error');", true);
                ex.StackTrace.ToString();
            }
        }

        private void CLEAR_SERVERDT()
        {
            txtServername.Text = string.Empty;
            txtuserID.Text = string.Empty;
            txtpassword.Text = string.Empty;
            lblErrorMsg.Text = string.Empty;
            lblNumError.Text = string.Empty;
            lbtnSubmit.Visible = true;
            lblErrorMsg.Visible = false;
            lblNumError.Visible = false;
            lbtnUpdate.Visible = false;
        }

        private bool IsValid(string checkingnull)
        {
            return !string.IsNullOrWhiteSpace(checkingnull) &&
                   !string.IsNullOrEmpty(checkingnull) &&
                   checkingnull != "0";
        }
        private bool IfExists(string servername) 
        {
            bool exists = false;
            try 
            {               
                if (!string.IsNullOrEmpty(servername) && !string.IsNullOrWhiteSpace(servername))
                {
                    DataSet ds = dal.SERVER_MASTER_SP(ACTION: "CHECK_SERVERNAME", SERVERNAME: servername);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        exists = true;
                    }
                    else
                    {
                        exists = false;
                    }
                }                                

            }
            catch (Exception ex) 
            {
                ex.StackTrace.ToString();
            }
            return exists;
        }
        private void GET_SERVERMASTER()
        {
            try
            {
                DataSet ds = dal.SERVER_MASTER_SP(
                    ACTION: "GET_SERVERRECORDS");
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GrdServerDetails.DataSource = ds.Tables[0];
                    GrdServerDetails.DataBind();
                }
                else
                {
                    GrdServerDetails.DataSource = null;
                    GrdServerDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        protected void GrdServerDetails_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
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
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }
        }
    }
}