using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_RULES_LISTS : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Bind_Visit();
                    Bind_Module();
                    Bind_GV();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUserDetails_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        public void Bind_Visit()
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_VISIT");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlVisit.DataSource = ds.Tables[0];
                    ddlVisit.DataValueField = "VISITNUM";
                    ddlVisit.DataTextField = "VISIT";
                    ddlVisit.DataBind();
                    ddlVisit.Items.Insert(0, new ListItem("All Visits", "000"));
                }
                else
                {
                    ddlVisit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Module();
                Bind_Field();
                Bind_GV();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Module()
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_MODULE_DM_PROJECT_MASTER",
                    VISITNUM: ddlVisit.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.DataSource = ds.Tables[0];
                    ddlModule.DataValueField = "MODULEID";
                    ddlModule.DataTextField = "MODULENAME";
                    ddlModule.DataBind();
                    ddlModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Field();
                Bind_GV();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Field()
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_FIELD_DM_PROJECT_MASTER",
                    MODULEID: ddlModule.SelectedValue,
                    VISITNUM: ddlVisit.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlField.DataSource = ds.Tables[0];
                    ddlField.DataTextField = "FIELDNAME";
                    ddlField.DataValueField = "FIELD_ID";
                    ddlField.DataBind();
                    ddlField.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlField.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_GV();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_GV()
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(
                    ACTION: "GET_RULE",
                    VISITNUM: ddlVisit.SelectedValue,
                    MODULEID: ddlModule.SelectedValue,
                    FIELDID: ddlField.SelectedValue
                    );

                gvRules.DataSource = ds.Tables[0];
                gvRules.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvRules_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ACTIVATE = dr["ACTIVATE"].ToString();
                    string DEACTIVATE = dr["DEACTIVATE"].ToString();
                    string COUNTS = dr["COUNTS"].ToString();

                    CheckBox Chek_Activate = (CheckBox)e.Row.FindControl("Chek_Activate");
                    CheckBox Chek_Deactivate = (CheckBox)e.Row.FindControl("Chek_Deactivate");
                    LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                    CheckBox chkStatus = (CheckBox)e.Row.FindControl("chkStatus");
                    HiddenField lblStatus = (HiddenField)e.Row.FindControl("lblStatus");
                    CheckBox checkbox = (CheckBox)e.Row.FindControl("Chk_ActivateDeactive");
                    if (lblStatus != null && !(checkbox.Checked))
                    {
                        // Set the label's Text to "false" if it meets your condition (or always, if needed).
                        lblStatus.Value = "false";
                    }
                    else
                    {
                        lblStatus.Value = "true";
                    }

                    if (ACTIVATE == "True")
                    {
                        //btnActivate.Visible = false;
                        //btnDeactivate.Visible = true;
                        checkbox.Checked = true;
                        lblStatus.Value = "true";
                    }
                    else if (DEACTIVATE == "True")
                    {
                        //btnActivate.Visible = true;
                        //btnDeactivate.Visible = false;
                        checkbox.Checked = false;
                        lblStatus.Value = "false";
                    }

                    if (COUNTS == "0")
                    {
                        lbtnDelete.Visible = true;
                    }
                    else
                    {
                        lbtnDelete.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnAddNewRule_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DM_ADD_NEW_RULE.aspx?VISITNUM=" + ddlVisit.SelectedValue + "&MODULEID=" + ddlModule.SelectedValue + "&FIELDID=" + ddlField.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the GridViewRow of the checkbox that triggered the event
                GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;

                // Find controls in the row
                CheckBox chkStatus = (CheckBox)row.FindControl("Chk_ActivateDeactive");
                HiddenField lblStatus = (HiddenField)row.FindControl("lblStatus");
                Label lblRowID = (Label)row.FindControl("lblID"); // Get the RowID label or control

                // Validate controls
                if (chkStatus == null || lblStatus == null || lblRowID == null)
                {
                    lblErrorMsg.Text = "Unexpected error: Required controls not found.";
                    return;
                }
                // Get the RowID (ID of the current row)
                string rowID = lblRowID.Text;
                // Update status based on the checkbox state
                if (!string.IsNullOrEmpty(lblStatus.Value) && lblStatus.Value != "Undefined")
                {
                    if (lblStatus.Value == "false" && chkStatus.Checked)
                    {
                        lblStatus.Value = "true";
                        Activate_Click(rowID);
                    }
                    else if (lblStatus.Value == "true" && !chkStatus.Checked)
                    {
                        lblStatus.Value = "false";
                        Deactivate_Click(rowID);
                    }
                }

                // Check if any checkbox is selected across all rows
                bool isAnyChecked = false;
                foreach (GridViewRow gridRow in gvRules.Rows)
                {
                    CheckBox chk = (CheckBox)gridRow.FindControl("Chk_ActivateDeactive");
                    if (chk != null && chk.Checked)
                    {
                        isAnyChecked = true;
                        break;
                    }
                }

                // Show error if no checkbox is checked
                if (!isAnyChecked)
                {
                    lblErrorMsg.Text = "Please select at least one checkbox.";
                    return;
                }

                // Clear error messages if validation passes
                lblErrorMsg.Text = "";
                // Optionally, show success feedback
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Status updated successfully.');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void gvRules_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                if (e.CommandName == "EditRule")
                {
                    hdnRuleID.Value = ID;
                    EDIT_RULE(ID);
                }
                else if (e.CommandName == "DeleteRule")
                {
                    DELETE_RULE(ID);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Rule deleted successfully.'); window.location.href='" + Request.RawUrl.ToString() + "'", true);
                }
                //else if (e.CommandName == "ActivateRule")
                //{
                //    RULE_ACT_RULE(ID);

                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Rule activated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "'", true);
                //}
                //else if (e.CommandName == "DeactivateRule")
                //{
                //    RULE_DEACT_RULE(ID);

                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Rule deactivated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "'", true);
                //}
                else if (e.CommandName == "CopyRule")
                {
                    RULE_COPY(ID);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Rule copied successfully.'); window.location.href='" + Request.RawUrl.ToString() + "'", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void RULE_COPY(string ID)
        {
            try
            {
                dal_DB.DB_RULE_SP(ACTION: "RULE_COPY", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void EDIT_RULE(string RULE_ID)
        {
            try
            {
                Response.Redirect("DM_ADD_NEW_RULE.aspx?ID=" + RULE_ID + "");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DELETE_RULE(string RULE_ID)
        {
            try
            {
                dal_DB.DB_RULE_SP(ACTION: "DELETE_RULE", ID: RULE_ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //protected void RULE_ACT_RULE(string ID)
        //{
        //    try
        //    {
        //        dal_DB.DB_RULE_SP(ACTION: "RULE_ACT", ID: ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        //protected void RULE_DEACT_RULE(string ID)
        //{
        //    try
        //    {
        //        dal_DB.DB_RULE_SP(ACTION: "RULE_DEACT", ID: ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        protected void lbtnExportFunc_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_RULE_FUNCTIONS_EXPORT");

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Rule Functions_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnExportRules_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_RULES_EXPORT");

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Rule Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Active_Deactive_btnclk(object sender, EventArgs e)
        {
            try
            {
                bool isAnyChecked = false;
                bool isAnyUnchecked = false;
                List<string> activatedIDs = new List<string>();
                Console.WriteLine("ActivatedIDS" + activatedIDs);
                List<string> deactivatedIDs = new List<string>();
                Console.WriteLine("deactivatedIDsIDS" + deactivatedIDs);
                string uncheckedID = "";
                CheckBox checkAllBox = (CheckBox)gvRules.HeaderRow.FindControl("ChekAll_chkbox");                
               
                for (int i = 0; i < gvRules.Rows.Count; i++)
                {
                    string ID = ((Label)gvRules.Rows[i].FindControl("lblID")).Text;
                    CheckBox checkBox = (CheckBox)gvRules.Rows[i].FindControl("Chk_ActivateDeactive");
                    HiddenField lblstatus = (HiddenField)gvRules.Rows[i].FindControl("lblStatus");
                    string RULE_ID = ((Label)gvRules.Rows[i].FindControl("lblRULE_ID")).Text;

                    if (checkBox != null)
                    {
                        // Check if the checkbox is checked
                        if (checkBox.Checked)
                        {
                            isAnyChecked = true;
                            uncheckedID = ID;
                            if (!string.IsNullOrEmpty(lblstatus.Value) && lblstatus.Value != "Undefined")
                            {
                                if (lblstatus.Value == "false")
                                {
                                    lblstatus.Value = "true";
                                    Activate_Click(ID); // Pass the ID of the checked checkbox for activation
                                    activatedIDs.Add(RULE_ID);
                                }
                            }
                        }
                        else // Checkbox is unchecked
                        {
                            isAnyUnchecked = true;
                            uncheckedID = ID; // Capture the ID of the unchecked checkbox

                            if (!string.IsNullOrEmpty(lblstatus.Value) && lblstatus.Value == "true")
                            {
                                lblstatus.Value = "false";
                                Deactivate_Click(ID); // Pass the ID of the unchecked checkbox for deactivation
                                deactivatedIDs.Add(RULE_ID);
                            }
                        }

                    }
                }
                
                if ((!checkAllBox.Checked) && (!isAnyChecked))
                {
                    //lblErrorMsg.Text = "Please select at least one checkbox.";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please select at least one checkbox.');", true);
                    return;
                }


                // Proceed with further logic if needed
                lblErrorMsg.Text = ""; // Clear any previous error message
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.StackTrace.ToString();
            }
        }

        protected void Activate_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean Activate = false;
                for (int i = 0; i < gvRules.Rows.Count; i++)
                {
                    string ID = ((Label)gvRules.Rows[i].FindControl("lblID")).Text;

                    CheckBox Chek_Activate = (CheckBox)gvRules.Rows[i].FindControl("Chek_Activate");
                    CheckBox checkBox = (CheckBox)gvRules.Rows[i].FindControl("Chk_ActivateDeactive");
                    if (checkBox.Checked)
                    {
                        dal_DB.DB_RULE_SP(
                                ACTION: "RULE_ACTIVATED",
                                ID: ID
                            );
                        Activate = true;
                    }
                }
                if (Activate == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Rule activated Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Deactivate_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean Deactivate = false;
                for (int i = 0; i < gvRules.Rows.Count; i++)
                {
                    string ID = ((Label)gvRules.Rows[i].FindControl("lblID")).Text;

                    CheckBox Chek_Deactivate = (CheckBox)gvRules.Rows[i].FindControl("Chek_Deactivate");
                    CheckBox checkBox = (CheckBox)gvRules.Rows[i].FindControl("Chk_ActivateDeactive");
                    HiddenField lblStatus = (HiddenField)gvRules.Rows[i].FindControl("lblStatus");
                    if (!checkBox.Checked)
                    {
                        dal_DB.DB_RULE_SP(
                                ACTION: "RULE_DEACTIVATED",
                                ID: ID
                            );

                        Deactivate = true;
                    }
                }
                if (Deactivate == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Rule deactivated Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Activate_Click(string rowID)
        {
            try
            {
                Boolean Activate = false;

                //string ID = ((Label)gvRules.Rows[i].FindControl("lblID")).Text;

                //CheckBox Chek_Activate = (CheckBox)gvRules.Rows[i].FindControl("Chek_Activate");
                //CheckBox checkBox = (CheckBox)gvRules.Rows[i].FindControl("Chk_ActivateDeactive");

                if (rowID != null)
                {
                    dal_DB.DB_RULE_SP(
                            ACTION: "RULE_ACTIVATED",
                            ID: rowID
                        );
                    Activate = true;
                }
                
                if (Activate == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Rule activated/deactivated Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void Deactivate_Click(string rowID)
        {
            try
            {
                Boolean Deactivate = false;
                
                    //string ID = ((Label)gvRules.Rows[i].FindControl("lblID")).Text;

                    //CheckBox Chek_Deactivate = (CheckBox)gvRules.Rows[i].FindControl("Chek_Deactivate");
                    //CheckBox checkBox = (CheckBox)gvRules.Rows[i].FindControl("Chk_ActivateDeactive");
                    //HiddenField lblStatus = (HiddenField)gvRules.Rows[i].FindControl("lblStatus");
                    if (rowID != null)
                    {
                        dal_DB.DB_RULE_SP(
                                ACTION: "RULE_DEACTIVATED",
                                ID: rowID
                            );

                        Deactivate = true;
                    }
                
                if (Deactivate == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Rule activated/deactivated Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}