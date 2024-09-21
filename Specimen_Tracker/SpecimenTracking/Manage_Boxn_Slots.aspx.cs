using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using SpecimenTracking.App_Code;
namespace SpecimenTracking
{
    public partial class Manage_Boxn_Slots : System.Web.UI.Page
    {
        DAL_SETUP DAL_SETUP = new DAL_SETUP();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GET_BOXNSLOT();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }

        }

        private void GET_BOXNSLOT()
        {
            try 
            {
                DataSet ds = DAL_SETUP.SETUP_BOX_MASTER_SP(ACTION: "GET_BOXNSLOT");
                if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) 
                {
                    GrdBoxnSlot.DataSource = ds.Tables[0];
                    GrdBoxnSlot.DataBind();
                }
                else 
                {
                    GrdBoxnSlot.DataSource = string.Empty;
                    GrdBoxnSlot.DataBind();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e) 
        {
            try 
            {
                DataSet ds = DAL_SETUP.SETUP_BOX_MASTER_SP(ACTION: "INSERT_BOXNSLOT", SITE_ID: dropdown_siteID.SelectedValue, BOXFROM: txtBoxFrom.Text, BOXTO: txtBoxTo.Text,
                    SLOTR_X: txtSlotRow.Text , SLOTC_Y: txtSlotColumn.Text);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Record inserted successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                GET_BOXNSLOT();
                CLEAR_BOXNSLOTDT();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }

        }

        protected void lbnUpdate_Click(object sender, EventArgs e) 
        {
            try 
            {
                DataSet ds = DAL_SETUP.SETUP_BOX_MASTER_SP(ACTION: "UPDATE_BOXNSLOT", SITE_ID: dropdown_siteID.SelectedValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }

        }

        protected void lbtnCancel_Click(object sender, EventArgs e) 
        {
            try 
            {
                CLEAR_BOXNSLOTDT();           
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        private void CLEAR_BOXNSLOTDT() 
        {
            dropdown_siteID.ClearSelection();
            txtBoxFrom.Text = string.Empty;
            txtBoxTo.Text = string.Empty;
            txtSlotRow.Text = string.Empty;
            txtSlotColumn.Text = string.Empty;
            lbtnSubmit.Visible = true;
            lbtnUpdate.Visible = false;

        }
        protected void GrdBoxnSlot_PageIndexChanging(object sender, GridViewPageEventArgs e) 
        {
            GrdBoxnSlot.PageIndex = e.NewPageIndex;
            this.GET_BOXNSLOT();
        }
        protected void GrdBoxnSlot_RowCommand(object sender, GridViewRowEventArgs e)        
        {
            try { }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }
        protected void lbtnExport_Click(object sender, EventArgs e)
        {

        }
    }
}