using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SpecimenTracking.App_Code;

namespace SpecimenTracking
{
    public partial class Manage_Visit : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_SETUP DAL_SETUP = new DAL_SETUP();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_VISITMASTER();
                    // Set initial focus on the first textbox
                    ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('" + txtVistNo.ClientID + "').focus();", true);
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
                if (Check_VISIT("INSERT"))
                {
                    INSERT_VISIT();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Visit Already Exists.','warning');", true);

                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }
        protected void lnkedit_Click(object sender, EventArgs e)
        {
            
           
        }
        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_VISIT("UPDATE"))

                {
                    UPDATE_VISIT();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Visit Already Exists.','warning');", true);
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
                CLEAR_VISITDT();
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }
        private void INSERT_VISIT()
        {
            DataSet da = DAL_SETUP.SETUP_VISIT_SP(
                ACTION: "INSERT_VISIT",
                VISITNAME: txtVistName.Text,
                VISITNUM: txtVistNo.Text);

            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Visit Created Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            GET_VISITMASTER();
            CLEAR_VISITDT();
        }

        private void UPDATE_VISIT()
        {

            DataSet da = DAL_SETUP.SETUP_VISIT_SP(
                ACTION: "UPDATE_VISIT",
                ID: ViewState["ID"].ToString(),
                VISITNAME: txtVistName.Text,
                VISITNUM: txtVistNo.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Visit Updated Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            lbtnUpdate.Visible = false;
            lbtnSubmit.Visible = true;
            GET_VISITMASTER();
            CLEAR_VISITDT();
        }
        protected void GrdVisits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
                if (e.CommandName == "EditVisit" || e.CommandName == "DeleteVisit")
            {
                try
                {
                    string VISITID = e.CommandArgument.ToString();
                    if (e.CommandName == "EditVisit")
                    {
                        ViewState["ID"] = VISITID;
                        DataSet ds = DAL_SETUP.SETUP_VISIT_SP(ACTION: "EDIT_VISIT", ID: VISITID);
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            foreach (DataRow row in dt.Rows)
                            {
                                txtVistNo.Text = row["VISITNUM"].ToString();
                                hdnVisitNum.Value = row["VISITNUM"].ToString();
                                txtVistName.Text = row["VISITNAME"].ToString();
                                hdnVisitName.Value = row["VISITNAME"].ToString();
                            }
                        }
                        lbtnSubmit.Visible = false;
                        lbtnUpdate.Visible = true;

                    }
                    else if (e.CommandName == "DeleteVisit")
                    {
                        string VisitID = e.CommandArgument.ToString();
                        DELETE_VISIT(VISITID);
                        GET_VISITMASTER();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        private void DELETE_VISIT(string VISITID)
        {
            try
            {
                DataSet ds = DAL_SETUP.SETUP_VISIT_SP(ACTION: "DELETE_VISIT", ID: VISITID);
                
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                                swal({
                                    title: 'Success!',
                                    text: 'Visit Deleted Successfully.',
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
        private void CLEAR_VISITDT() 
        {
            txtVistName.Text = string.Empty;
            txtVistNo.Text = string.Empty;
            lblErrorMsg.Text = string.Empty;
            lblNumError.Text = string.Empty;
            lbtnSubmit.Visible = true;
            lblErrorMsg.Visible = false;
            lblNumError.Visible = false;
            lbtnUpdate.Visible = false;
        }

        private bool Check_VISIT(string ACTIONS) 
        {
            bool res = true;
            try 
            {
                DataSet ds = new DataSet();
                if (ACTIONS == "INSERT")
                {
                    ds = DAL_SETUP.SETUP_VISIT_SP(ACTION: "CHECK_VISITDT_EXISTS", VISITNAME: txtVistName.Text, VISITNUM: txtVistNo.Text);
                    //ds = DAL_SETUP.SETUP_VISIT_SP(ACTION: "CHECK_VISITDT_EXISTS", VISITNAME: txtVistName.Text, VISITNUM: txtVistNo.Text);
                }
                else if (ACTIONS == "UPDATE") 
                {
                    ds = DAL_SETUP.SETUP_VISIT_SP(ACTION: "CHECK_VISITID_EXISTS", VISITNAME: txtVistName.Text, VISITNUM: txtVistNo.Text, ID: ViewState["ID"].ToString());
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >0)
                {
                    res = false;
                }
            }
            catch (Exception ex) 
            {
              Console.WriteLine(ex.StackTrace.ToString());
            }
            return res;
        }
        private void GET_VISITMASTER() 
        {
            try
            {
                DataSet ds = DAL_SETUP.SETUP_VISIT_SP(
                    ACTION: "GET_VISITS");
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GrdVisits.DataSource = ds.Tables[0];
                    GrdVisits.DataBind();
                }
                else 
                {
                    GrdVisits.DataSource = null;
                    GrdVisits.DataBind();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }
        protected void GrdVisits_PageIndexChanging(object sender, GridViewPageEventArgs e) 
        {
            GrdVisits.PageIndex = e.NewPageIndex;
            this.GET_VISITMASTER();        
        }

        protected void VisitNameChanged(object sender, EventArgs e) 
        {
            try 
            {
                //bool vsname = true;
                string visitname =  txtVistName.Text;
                DataSet ds = new DataSet();
                ds = DAL_SETUP.SETUP_VISIT_SP(ACTION: "CHECK_VISITNAME_EXISTS", VISITNAME: visitname);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //vsname = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Visit Name Already Exists.','warning');", true);
                    txtVistName.Text = string.Empty;
                    //lblErrorMsg.Text = visitname + " the visit name Already Exists.";
                    //lblErrorMsg.Visible = true;
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }
        protected void VisitNumChanged(object sender, EventArgs e)
        {
            try
            {
                //bool vsname = true;
                string visitno = txtVistNo.Text;
                DataSet ds = new DataSet();
                ds = DAL_SETUP.SETUP_VISIT_SP(ACTION: "CHECK_VISITNO_EXISTS", VISITNUM: visitno);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //vsname = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Visit Number Already Exists.','warning');", true);
                    txtVistNo.Text = string.Empty;
                    //lblNumError.Text = visitno + " the visit number Already Exists.";
                    //lblNumError.Visible = true;
                }
                else 
                {
                    // Some processing, followed by setting focus on the next field
                    ScriptManager.RegisterStartupScript(this, GetType(), "focusNext", "document.getElementById('" +txtVistName.ClientID + "').focus();", true);
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.StackTrace.ToString());
            }
        }
        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Visit";

                DataSet ds = DAL_SETUP.SETUP_LOGS_SP(
                   ACTION: "EXPORT_VISIT"
                   );
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
    }
}