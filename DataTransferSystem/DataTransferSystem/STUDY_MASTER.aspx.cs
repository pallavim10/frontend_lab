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
    public partial class STUDY_MASTER : System.Web.UI.Page
    {
        DAL_STUDY_MASTER DAL = new DAL_STUDY_MASTER();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GETSTUDYDETAILS();
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
                if (IsValid(txtStudyname.Text.Trim()) && IsValid(txtSponsorName.Text.Trim()))
                {
                    if (IfExists(txtSponsorName.Text.Trim(),txtStudyname.Text.Trim()) == false)
                    {
                        INSERT_STUDYDETAILS();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Study Details Already Exists.','warning');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Enter Valid Study Details.','warning');", true);
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
                if (IsValid(txtStudyname.Text.Trim()) && IsValid(txtSponsorName.Text.Trim()))
                {
                    if (IfExists(txtSponsorName.Text.Trim(), txtStudyname.Text.Trim()) == false)
                    {
                        UPDATE_STUDYDETAILS();
                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Study Details Already Exists.','warning');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Enter vaild Study Details.','warning');", true);
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
                CLEAR_STUDYDT();
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }

        private void CLEAR_STUDYDT()
        {
            txtStudyname.Text = string.Empty;
            txtSponsorName.Text = string.Empty;
            lblNumError.Text = string.Empty;
            lbtnSubmit.Visible = true;
            lblNumError.Visible = false;
            lbtnUpdate.Visible = false;
        }

        private bool IsValid(string checkingnull)
        {
            
                return !string.IsNullOrWhiteSpace(checkingnull) &&
                       !string.IsNullOrEmpty(checkingnull) &&
                       checkingnull != "0";
           
        }
        private bool IfExists(string SponsorName, string StudyName)
        {
            bool exists = false;
            try
            {
                if (!string.IsNullOrEmpty(SponsorName) && !string.IsNullOrWhiteSpace(SponsorName))
                {
                    DataSet ds = DAL.STUDY_MASTER_SP(ACTION: "CHECK_SPONSORNAME", SPONSORNAME: SponsorName, STUDYNAME: StudyName);
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
        private void INSERT_STUDYDETAILS()
        {
            DataSet da = DAL.STUDY_MASTER_SP(
                ACTION: "INSERT_STUDYMASTER",
                STUDYNAME: txtStudyname.Text,
                SPONSORNAME: txtSponsorName.Text
                );

            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Study Details Created Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            GETSTUDYDETAILS();
            CLEAR_STUDYDT();
        }

        private void UPDATE_STUDYDETAILS()
        {

            DataSet da = DAL.STUDY_MASTER_SP(
                ACTION: "UPDATE_STUDYMASTER",
                ID: ViewState["ID"].ToString(),
                STUDYNAME: txtStudyname.Text,
                SPONSORNAME: txtSponsorName.Text
                );
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Study Details Updated Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            lbtnUpdate.Visible = false;
            lbtnSubmit.Visible = true;
            GETSTUDYDETAILS();
            CLEAR_STUDYDT();
        }
        protected void GrdStudyDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditStudydetails" || e.CommandName == "DeleteStudydetails")
            {
                try
                {
                    string STUDYID = e.CommandArgument.ToString();
                    if (e.CommandName == "EditStudydetails")
                    {
                        ViewState["ID"] = STUDYID;
                        DataSet ds = DAL.STUDY_MASTER_SP(ACTION: "EDIT_STUDYMASTER", ID: STUDYID);
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            foreach (DataRow row in dt.Rows)
                            {
                                txtStudyname.Text = row["STUDYNAME"].ToString();
                                hdnStudyName.Value = row["STUDYNAME"].ToString();
                                txtSponsorName.Text = row["SPONSORNAME"].ToString();
                                hdnSponsorName.Value = row["SPONSORNAME"].ToString();
                            }
                        }
                        lbtnSubmit.Visible = false;
                        lbtnUpdate.Visible = true;

                    }
                    else if (e.CommandName == "DeleteStudydetails")
                    {
                        string STUDY_ID = e.CommandArgument.ToString();
                        DELETE_Studydetails(STUDY_ID);
                        GETSTUDYDETAILS();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        private void DELETE_Studydetails(string STUDYID)
        {
            try
            {
                DataSet ds = DAL.STUDY_MASTER_SP(ACTION: "DELETE_STUDYMASTER_RECORD", ID: STUDYID);

                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                                swal({
                                    title: 'Success!',
                                    text: 'Study Record Deleted Successfully.',
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
        private void GETSTUDYDETAILS() 
        {
            
            try 
            {
                DataSet ds = DAL.STUDY_MASTER_SP(ACTION: "GET_STUDYRECORDS");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GrdStudyDetails.DataSource = ds.Tables[0];
                    GrdStudyDetails.DataBind();
                }
                else 
                {
                    GrdStudyDetails.DataSource = null;
                    GrdStudyDetails.DataBind();
                }
            }
            catch (Exception ex) 
            {
                ex.StackTrace.ToString();
            }
        }
        protected void GrdStudyDetails_PreRender(object sender, EventArgs e)
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