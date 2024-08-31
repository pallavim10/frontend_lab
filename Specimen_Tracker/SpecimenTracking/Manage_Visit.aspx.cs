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
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Category Already Exists.','warning');", true);

                }
            }
            catch (Exception ex) 
            {
                ex.StackTrace.ToString();
            }
        }
        protected void lnkedit_Click(object sender, EventArgs e) 
        {
            LinkButton lnkedit = sender as LinkButton;
            GridViewRow gridView = lnkedit.NamingContainer as GridViewRow;
            string VisitID = GrdVisits.DataKeys[gridView.RowIndex].Value.ToString();
            
            ViewState["ID"] = VisitID;
            try 
            {
                DataSet ds = DAL_SETUP.SETUP_VISIT_SP(ACTION: "EDIT_VISIT", ID: VisitID);
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0) 
                {
                    DataTable dt = ds.Tables[0];
                    foreach(DataRow row in dt.Rows)
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
            catch (Exception ex) 
            {
                ex.StackTrace.ToString();
            }
            
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

    protected void lnkDelete_Click(object sender, EventArgs e) 
        {
            try 
            {
                LinkButton lnkbtn = sender as LinkButton;
                GridViewRow grdrow = lnkbtn.NamingContainer as GridViewRow;
                string VISITID = GrdVisits.DataKeys[grdrow.RowIndex].Value.ToString();
                DELETE_VISIT(VISITID);
                GET_VISITMASTER();

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
                        text: 'Record inserted successfully.',
                        icon: 'success',
                        button: 'OK'
                    })
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
                VISITNUM: txtVistNo.Text) ;
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Record Updated successfully.',
                        icon: 'success',
                        button: 'OK'
                    })
                ", true);
            lbtnUpdate.Visible = false;
            lbtnSubmit.Visible = true;
            GET_VISITMASTER();
            CLEAR_VISITDT();
        }
        private void DELETE_VISIT(string VISITID) 
        {

            try
            {
                DataSet ds = DAL_SETUP.SETUP_VISIT_SP(ACTION: "DELETE_VISIT", ID: VISITID);
                string result = ds.Tables[0].Rows[0].ToString();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && result == "true")
                {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                                sessionStorage.setItem('deleteConfirmed', 'true');
                                swal({
                                    title: 'Success!',
                                    text: 'Record Deleted successfully.',
                                    icon: 'success',
                                    button: 'OK'
                                }).then(function() {
                                    window.location.reload(); // Reload the page after the alert
                                });
                            ", true);
                    
                }
                else 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Error!', 'Failed to delete record.', 'error');", true);
                }
               
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }
        private void CLEAR_VISITDT() 
        {
            txtVistName.Text = string.Empty;
            txtVistNo.Text = string.Empty;
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
                ex.StackTrace.ToString();
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
                ex.StackTrace.ToString();
            }
        }
        protected void GrdVisits_PageIndexChanging(object sender, GridViewPageEventArgs e) 
        {
            GrdVisits.PageIndex = e.NewPageIndex;
            this.GET_VISITMASTER();        
        }
        protected void lbtnExport_Click(object sender, EventArgs e)
        {

        }
    }
}