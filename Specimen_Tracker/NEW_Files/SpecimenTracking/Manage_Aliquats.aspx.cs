﻿using System;
using SpecimenTracking.App_Code;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SpecimenTracking
{
    public partial class Manage_Aliquats : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_SETUP DAL_SETUP = new DAL_SETUP();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_ALIQUOTMASTER();
                    BindVisits();
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
                if (Check_ALIQUOT("INSERT"))
                {
                    INSERT_ALIQUOT();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Aliquot Already Exists.','warning');", true);

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
                if (Check_ALIQUOT("UPDATE"))
                {
                    UPDATE_ALIQUOT();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Aliquot Already Exists.','warning');", true);
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
                string ALIQUOTID = GrdAliquots.DataKeys[grdrow.RowIndex].Value.ToString();
                DELETE_ALIQUOT(ALIQUOTID);
                GET_ALIQUOTMASTER();
                
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
                CLEAR_ALIQUOTDT();
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }

        }
        private void INSERT_ALIQUOT()
        {
            DataSet ds = DAL_SETUP.SETUP_ALIQUOT_SP(
                ACTION: "INSERT_ALIQUOT",
                SEQNO: txtSeqtNo.Text,
                ALIQUOTID: txtaliquotID.Text,
                ALIQUOTTYPE: txtaliquottype.Text,
                ALIQUOTNUM: txtaliquotnum.Text,
                ALIQUOTFROM: allocatefrom.Text, 
                ALIQUOTSEQTO: allocateto.Text);
            ;
            // Redirect to a different page or the same page            
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Aliquot Created Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            GET_ALIQUOTMASTER();
            CLEAR_ALIQUOTDT();
            
        }
        protected void lnkedit_Click(object sender, EventArgs e)
        {
            LinkButton lnkedit = sender as LinkButton;
            GridViewRow gridView = lnkedit.NamingContainer as GridViewRow;
            string AliquotRecID = GrdAliquots.DataKeys[gridView.RowIndex].Value.ToString();

            ViewState["ID"] = AliquotRecID;
            try
            {
                DataSet ds = DAL_SETUP.SETUP_ALIQUOT_SP(ACTION: "EDIT_ALIQUOT", ID: AliquotRecID);
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        txtaliquotID.Text = row["ALIQUOTID"].ToString();
                        hdnAliquotID.Value = row["ALIQUOTID"].ToString();
                        txtSeqtNo.Text = row["SEQNO"].ToString();
                        hdnSeqno.Value = row["SEQNO"].ToString();
                        txtaliquottype.Text = row["ALIQUOTTYPE"].ToString();
                        hdnAliquotType.Value = row["ALIQUOTTYPE"].ToString();
                        txtaliquotnum.Text = row["ALIQUOTNO"].ToString();
                        Hdnaliquotnum.Value = row["ALIQUOTNO"].ToString();
                        allocatefrom.Text = row["ALIQUOTSEQFROM"].ToString();
                        allocatefrm.Value = row["ALIQUOTSEQFROM"].ToString();
                        allocateto.Text = row["ALIQUOTSEQTO"].ToString();
                        allocatetwo.Value = row["ALIQUOTSEQTO"].ToString();
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
        private void UPDATE_ALIQUOT()
        {

            DataSet ds = DAL_SETUP.SETUP_ALIQUOT_SP(
                ACTION: "UPDATE_ALIQUOT",
                ID: ViewState["ID"].ToString(),
                ALIQUOTID: txtaliquotID.Text,
                SEQNO: txtSeqtNo.Text,
                ALIQUOTTYPE: txtaliquottype.Text,
                ALIQUOTNUM:txtaliquotnum.Text,
                ALIQUOTFROM:allocatefrom.Text,
                ALIQUOTSEQTO:allocateto.Text
                );
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Aliquot Updated Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            lbtnUpdate.Visible = false;
            lbtnSubmit.Visible = true;
            GET_ALIQUOTMASTER();
            CLEAR_ALIQUOTDT();
        }
        private void DELETE_ALIQUOT(string ALIQUOTRECID)
        {

            try
            {
               
                DataSet ds = DAL_SETUP.SETUP_ALIQUOT_SP(ACTION: "DELETE_ALIQUOT", ID: ALIQUOTRECID);
                //string result = ds.Tables[0].Rows[0].ToString();
                
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Aliquot Deleted Successfully.',
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
        private void CLEAR_ALIQUOTDT()
        {
            txtSeqtNo.Text = string.Empty;
            txtaliquotID.Text = string.Empty;
            txtaliquottype.Text = string.Empty;
            txtaliquotnum.Text = string.Empty;
            allocatefrom.Text = string.Empty;
            allocateto.Text = string.Empty;
            lbtnSubmit.Visible = true;
            lbtnUpdate.Visible = false;
        }
        private bool Check_ALIQUOT(string ACTIONS)
        {
            bool res = true;
            try
            {
                DataSet ds = new DataSet();
                if (ACTIONS == "INSERT")
                {
                    ds = DAL_SETUP.SETUP_ALIQUOT_SP(ACTION: "CHECK_ALIQUOTDT_EXISTS", SEQNO: txtSeqtNo.Text, ALIQUOTID: txtaliquotID.Text, ALIQUOTTYPE: txtaliquottype.Text,ALIQUOTNUM:txtaliquotnum.Text, ALIQUOTFROM: allocatefrom.Text ,ALIQUOTSEQTO:allocateto.Text);
                }
                else if (ACTIONS == "UPDATE")
                {
                    bool AliquotValCheck = VAL_CHECK(ViewState["ID"].ToString(),txtSeqtNo.Text, txtaliquotID.Text, txtaliquottype.Text, txtaliquotnum.Text, allocatefrom.Text, allocateto.Text );
                    if (AliquotValCheck == true)
                    {
                        ds = DAL_SETUP.SETUP_ALIQUOT_SP(ACTION: "CHECK_ALIQUOTDT_EXISTS", SEQNO: txtSeqtNo.Text, ALIQUOTID: txtaliquotID.Text, ALIQUOTTYPE: txtaliquottype.Text, ID: ViewState["ID"].ToString(), ALIQUOTNUM: txtaliquotnum.Text, ALIQUOTFROM: allocatefrom.Text, ALIQUOTSEQTO: allocateto.Text);
                    }
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
        protected void Check_SequenceNumber(object sender, EventArgs e) 
        {
            try 
            {
                string AliquotSeq = txtSeqtNo.Text.Trim();
                DataSet ds = DAL_SETUP.SETUP_ALIQUOT_SP(ACTION: "Check_SequenceNumber", SEQNO: AliquotSeq);
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Sequence Number Already Exists.','warning');", true);
                    txtSeqtNo.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "focusNext", "document.getElementById('" + txtSeqtNo.ClientID + "').focus();", true);
                }
                else 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "focusNext", "document.getElementById('" + txtaliquotID.ClientID + "').focus();", true);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        protected void CheckAliquotID(object sender, EventArgs e)
        {
            try
            {
                string AliquotID = txtaliquotID.Text.Trim();
                DataSet ds = DAL_SETUP.SETUP_ALIQUOT_SP(ACTION: "Check_AliquotID", ALIQUOTID: AliquotID);
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Aliquot ID Already Exists.','warning');", true);
                    txtaliquotID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "focusNext", "document.getElementById('" + txtaliquotID.ClientID + "').focus();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "focusNext", "document.getElementById('" + txtaliquottype.ClientID + "').focus();", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        protected void CheckAliquotNum(object sender, EventArgs e)
        {
            try
            {
                string AliquotNum = txtaliquotnum.Text.Trim();
                DataSet ds = DAL_SETUP.SETUP_ALIQUOT_SP(ACTION: "Check_AliquotNumber", ALIQUOTNUM: AliquotNum);
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Aliquot Number Already Exists.','warning');", true);
                    txtaliquotnum.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "focusNext", "document.getElementById('" + txtaliquotnum.ClientID + "').focus();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "focusNext", "document.getElementById('" + allocatefrom.ClientID + "').focus();", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        protected void Check_AliquotSEQALLOC(object sender, EventArgs e)
            {
            try
            {
                if (!string.IsNullOrWhiteSpace(allocatefrom.Text) && !string.IsNullOrWhiteSpace(allocateto.Text))
                {
                    string SEQFROM = allocatefrom.Text.Trim();
                    string SEQTO = allocateto.Text.Trim();
                    DataSet ds = DAL_SETUP.SETUP_ALIQUOT_SP(ACTION: "Check_AliquotSEQALLOC", ALIQUOTFROM: SEQFROM, ALIQUOTSEQTO: SEQTO);
                    //if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                    //{
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        // Ensure the table has rows
                        if (dt.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!', 'Please Enter Aliquot Sequence in the Correct Format.', 'warning');", true);
                            allocatefrom.Text = string.Empty;
                            allocateto.Text = string.Empty;
                            
                        }
                        else
                        {
                            // Handle no results scenario
                            Console.WriteLine("No overlapping sequences found.");
                        }
                    }
                    else
                    {
                        // Handle case where no table exists in the dataset
                        Console.WriteLine("No table returned in the dataset.");
                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Aliquot Sequence Already Exists.','warning');", true);
                    //allocatefrom.Text = string.Empty;
                    //allocateto.Text = string.Empty;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "focusNext", "document.getElementById('" + allocateto.ClientID + "').focus();", true);
                }
                else {
                    ScriptManager.RegisterStartupScript(this, GetType(), "focusNext", "document.getElementById('" + allocateto.ClientID + "').focus();", true);
                }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        protected bool VAL_CHECK(string ID, string AliquotSeqNumber, string AliquotID, string AliquotType, string AliquotNumber, string SeqAllocateFrm, string SeqAllocateTo) 
        {
            bool Aliquot_Val = false;

            if (!string.IsNullOrEmpty(AliquotSeqNumber) && !string.IsNullOrWhiteSpace(AliquotSeqNumber))
            {
            }
            else 
            { }


            return Aliquot_Val;
        }
        private void GET_ALIQUOTMASTER()
        {
            try
            {
                DataSet ds = DAL_SETUP.SETUP_ALIQUOT_SP(
                    ACTION: "GET_ALIQUOTS");
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GrdAliquots.DataSource = ds.Tables[0];
                    GrdAliquots.DataBind();
                }
                else
                {
                    GrdAliquots.DataSource = string.Empty;
                    GrdAliquots.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        protected void BindVisits() 
        {
            try 
            {
                DataSet ds = DAL_SETUP.Visit_Aliquot_Mapping(ACTION: "GET_VISITS");
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    dropdown_visits.DataSource = ds.Tables[0];
                    dropdown_visits.DataBind();
                    dropdown_visits.DataTextField = "VISITNAME";
                    dropdown_visits.DataValueField = "VISITNUM";
                    dropdown_visits.DataBind();
                    dropdown_visits.Items.Insert(0, new ListItem("--Select--", "0"));
                }

            }
            catch (Exception ex) 
            {
                ex.StackTrace.ToString();
            }
        }

        protected void dropdown_visit_SelectedIndexChanged(object sender, EventArgs e) 
        {
            try 
            {

                string SelectedVisitID = dropdown_visits.SelectedValue;
                
                if (SelectedVisitID != "0" && !string.IsNullOrEmpty(SelectedVisitID))
                {
                    Bind_NotMatchingCheckBoxList(SelectedVisitID);
                    MapAliquot.Visible = true;
                }
                else 
                {
                    grdviewAliquotVisit.DataSource = string.Empty;
                    grdviewAliquotVisit.DataBind();

                    gridAddedAliquot.DataSource = string.Empty;
                    gridAddedAliquot.DataBind();
                    MapAliquot.Visible = false;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
            
        }
        private void Bind_NotMatchingCheckBoxList(string SelectedVisitID) 
        {
            DataSet ds = new DataSet();
            ds = DAL_SETUP.Visit_Aliquot_Mapping(ACTION: "GET_ALIQUOT_FOR_MAPPING", VISITID: SelectedVisitID.ToString());

            if (ds.Tables.Count > 0)
            {
                grdviewAliquotVisit.DataSource = ds.Tables[0];
                grdviewAliquotVisit.DataBind();

                if (ds.Tables.Count > 1)
                {
                    gridAddedAliquot.DataSource = ds.Tables[1];
                    gridAddedAliquot.DataBind();
                }
                else
                {
                    gridAddedAliquot.DataSource = string.Empty;
                    gridAddedAliquot.DataBind();
                }
            }
            else 
            
            {
                grdviewAliquotVisit.DataSource = string.Empty;
                grdviewAliquotVisit.DataBind();

                gridAddedAliquot.DataSource = string.Empty;
                gridAddedAliquot.DataBind();
            }
            this.GET_ALIQUOTMASTER();
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try 
            {
                foreach (GridViewRow row in grdviewAliquotVisit.Rows ) 
                {
                    CheckBox checkBox = (CheckBox)row.FindControl("chkSelect");
                    if (checkBox.Checked)
                    {
                            HiddenField hfID = (HiddenField)row.FindControl("hfID");
                            DAL_SETUP.Visit_Aliquot_Mapping(
                             ACTION: "ADD_ALIQUOT",
                             VISITID: dropdown_visits.SelectedValue,
                             ALIQUOTID:hfID.Value,
                             ADDED: true
                                );
                    }
                    
                }
                Bind_NotMatchingCheckBoxList(dropdown_visits.SelectedValue);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        protected void btn_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gridAddedAliquot.Rows)
                {
                    CheckBox checkBox = (CheckBox)row.FindControl("chkSelect");
                    if (checkBox.Checked) 
                    {
                        HiddenField hdnfield = (HiddenField)row.FindControl("hfID");
                        DAL_SETUP.Visit_Aliquot_Mapping(ACTION: "REMOVE_ALIQUOT", VISITID: dropdown_visits.SelectedValue, hdnfield.Value, ADDED: false);
                    }
                }
                
                Bind_NotMatchingCheckBoxList(dropdown_visits.SelectedValue);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.StackTrace.ToString()); 
            }
        }

        protected void GrdAliquot_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hiddenField = e.Row.FindControl("hfALIQUOTID") as HiddenField;
                LinkButton lnkedit = e.Row.FindControl("lnkedit") as LinkButton;
                LinkButton lnkDelete = e.Row.FindControl("lnkdelete") as LinkButton;

                if (hiddenField != null && lnkedit != null && lnkDelete != null)
                {
                    string AliquotID = hiddenField.Value.ToString();
                    DataSet ds = DAL_SETUP.SETUP_ALIQUOT_SP(ACTION: "Check_AliquotExistInMapping", ID: AliquotID, ALIQUOTID: dropdown_visits.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                    {
                        // Check if the first value is TRUE, indicating the record exists
                        if (ds.Tables[0].Rows[0][0].ToString().Equals("TRUE", StringComparison.OrdinalIgnoreCase))
                        {
                            // Hide the delete button if record exists
                            lnkDelete.Visible = false;
                        }

                    }
                }

            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Aliquot";

                DataSet ds = DAL_SETUP.SETUP_LOGS_SP(
                   ACTION: "EXPORT_ALIQUOT"
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

        protected void lbtnExportMapping_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Aliquot Mapping";

                DataSet ds = DAL_SETUP.SETUP_LOGS_SP(
                   ACTION: "EXPORT_ALIQUOTMAPPING"
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

        protected void GrdAliquots_PreRender(object sender, EventArgs e)
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
                ex.ToString();
                throw;
            }
        }
    }
}