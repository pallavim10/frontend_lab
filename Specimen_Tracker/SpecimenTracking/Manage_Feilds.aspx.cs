using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class Manage_Feilds : System.Web.UI.Page
    {
        DAL_SETUP Dal_Setup = new DAL_SETUP();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_OPTION();
                    GET_FIELDS();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void GET_FIELDS()
        {
            try
            {

                DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                    ACTION: "GET_FIELDS"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GrdMngFields.DataSource = ds.Tables[0];
                    GrdMngFields.DataBind();
                }
                else
                {
                    GrdMngFields.DataSource = null;
                    GrdMngFields.DataBind();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void GET_OPTION()
        {
            try
            {

                DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                    ACTION: "GET_DEFAULTFIELDS"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdMngOptFields.DataSource = ds.Tables[0];
                    grdMngOptFields.DataBind();
                }
                else
                {
                    grdMngOptFields.DataSource = null;
                    grdMngOptFields.DataBind();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_FIELDS();

                GET_CLEAR();

                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Field created successfully.', 'success');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Field created successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                GET_FIELDS();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void INSERT_FIELDS()
        {
            if (rbtnFirst.Checked || rbtnSecond.Checked)
            {
                DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                       ACTION: "INSERT_FIELDS",
                                       FIELDNAME: txtFieldsName.Text,
                                       VARIABLENAME: txtVariableName.Text,
                                       SEQNO: txtSeqNo.Text,
                                       CONTROLTYPE: drpControlType.SelectedValue,
                                       FIRSTENTRY: rbtnFirst.Checked,
                                       SECONDENTRY: rbtnSecond.Checked,
                                       REPEAT: chkRepeat.Checked,
                                       REQUIRED: chkRequired.Checked,
                                       MAXLENGHT: txtmaxlength.Text
                                       );
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Select Entry Type.', 'warning');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", @"
                    swal({
                        title: 'Warning!',
                        text: 'Please Select Entry Type.',
                        icon: 'warning',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            }

        }
        private void GET_CLEAR()
        {
            txtFieldsName.Text = "";
            txtVariableName.Text = "";
            txtSeqNo.Text = "";
            txtmaxlength.Text = "";
            drpControlType.ClearSelection();
            lbnUpdate.Visible = false;
            lbtnSubmit.Visible = true;
            rbtnFirst.Checked = false;
            rbtnSecond.Checked = false;
            chkRepeat.Checked = false;
            chkRequired.Checked = false;

        }


        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            UPDATE_FIELDS();
            GET_CLEAR();
            lbtnSubmit.Visible = true;
            lbnUpdate.Visible = false;
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Field updated successfully.', 'success');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Field updated successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            GET_FIELDS();

        }

        private void UPDATE_FIELDS()
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                      ACTION: "UPDATE_FIELDS",
                                      FIELDNAME: txtFieldsName.Text,
                                      VARIABLENAME: txtVariableName.Text,
                                      SEQNO: txtSeqNo.Text,
                                      CONTROLTYPE: drpControlType.SelectedValue,
                                       FIRSTENTRY: rbtnFirst.Checked,
                                       SECONDENTRY: rbtnSecond.Checked,
                                       REPEAT: chkRepeat.Checked,
                                       REQUIRED: chkRequired.Checked,
                                       MAXLENGHT: txtmaxlength.Text,
                                      ID: ViewState["ID"].ToString()
                                      );
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            GET_CLEAR();
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            EXPORT_MANAGEFIELD();
        }

        private void EXPORT_MANAGEFIELD()
        {
            try
            {
                string xlname = "Manage Fields";

                DataSet ds = Dal_Setup.SETUP_LOGS_SP(
                   ACTION: "EXPORT_MANAGEFIELD"
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

        protected void grdMngOptFields_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;


                    LinkButton lbtEnable = (e.Row.FindControl("lbtEnable") as LinkButton);
                    LinkButton lbtDisable = (e.Row.FindControl("lbtDisable") as LinkButton);
                    HiddenField Active = (HiddenField)e.Row.FindControl("HdnISACTIVE");
                    HiddenField HdnControlType = (HiddenField)e.Row.FindControl("HdnControlType");
                    HiddenField VariableName = (HiddenField)e.Row.FindControl("HdnVariableName");
                    TextBox txtFieldName = (e.Row.FindControl("txtFieldName") as TextBox);
                    LinkButton lbtAddOptionr = (e.Row.FindControl("lbtAddOptionr") as LinkButton);

                    LinkButton lbtRepeatYes = (e.Row.FindControl("lbtRepeatYes") as LinkButton);
                    LinkButton lbtRepeatNo = (e.Row.FindControl("lbtRepeatNo") as LinkButton);
                    HiddenField Repeat = (HiddenField)e.Row.FindControl("HdnRepeat");

                    DropDownList drplevel = (e.Row.FindControl("drplevel") as DropDownList);
                    HiddenField Level1 = (HiddenField)e.Row.FindControl("HdnLebvel1");
                    HiddenField Level2 = (HiddenField)e.Row.FindControl("HdnLebvel2");

                    if (Level1.Value == "True")
                    {
                        drplevel.SelectedValue = "Level 1";
                    }
                    else if (Level2.Value == "True")
                    {
                        drplevel.SelectedValue = "Level 2";
                    }
                    else
                    {
                        drplevel.SelectedValue = "0";
                    }

                    if(VariableName.Value == "SCANALQ")
                    {
                        drplevel.Enabled = false;
                    }
                    string ControlType = HdnControlType.Value.Trim();
                    if (Active.Value == "True")
                    {
                        lbtEnable.Visible = true;
                        lbtDisable.Visible = false;

                    }
                    else
                    {
                        lbtEnable.Visible = false;
                        lbtDisable.Visible = true;
                        txtFieldName.Enabled = false;

                    }

                    if (Repeat.Value == "True")
                    {
                        lbtRepeatYes.Visible = true;
                        lbtRepeatNo.Visible = false;

                        if (ControlType == "TABLE")
                        {
                            lbtRepeatYes.Visible = false;
                            lbtRepeatNo.Visible = false;
                            txtFieldName.Enabled = false;

                        }
                    }
                    else
                    {
                        lbtRepeatYes.Visible = false;
                        lbtRepeatNo.Visible = true;

                        if (ControlType == "TABLE")
                        {
                            lbtRepeatYes.Visible = false;
                            lbtRepeatNo.Visible = false;
                            txtFieldName.Enabled = false;
                        }

                    }

                    if (ControlType == "Dropdown" || ControlType == "Radio Button" || ControlType == "Checkbox")
                    {
                        if (Active.Value == "True")
                        {
                            lbtAddOptionr.Visible = true;
                        }
                        else
                        {
                            lbtAddOptionr.Visible = false;
                        }
                    }
                    else
                    {
                        lbtAddOptionr.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void GrdMngFields_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            ViewState["ID"] = ID;
            string CommandName = e.CommandName.ToString();
            if (CommandName == "EDITED")
            {
                EDIT_FIELDS(ID);
                lbtnSubmit.Visible = false;
                lbnUpdate.Visible = true;
            }
            else if (CommandName == "DELETED")
            {
                DELETE_FIELDS(ID);
                GET_FIELDS();
            }
        }

        private void EDIT_FIELDS(string ID)
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                      ACTION: "EDIT_FIELDS",
                                      ID: ID
                                      );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtFieldsName.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                    txtVariableName.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();
                    txtSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                    drpControlType.SelectedValue = ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString();
                    txtmaxlength.Text = ds.Tables[0].Rows[0]["MAXLEN"].ToString();
                    if (ds.Tables[0].Rows[0]["FIRSTENTRY"].ToString() == "True")
                    {
                        rbtnFirst.Checked = true;
                    }
                    else
                    {
                        rbtnFirst.Checked = false;
                    }
                    if (ds.Tables[0].Rows[0]["SECONDENTRY"].ToString() == "True")
                    {
                        rbtnSecond.Checked = true;
                    }
                    else
                    {
                        rbtnSecond.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["REPEAT"].ToString() == "True")
                    {
                        chkRepeat.Checked = true;
                    }
                    else
                    {
                        chkRepeat.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["REQUIRED"].ToString() == "True")
                    {
                        chkRequired.Checked = true;
                    }
                    else
                    {
                        chkRequired.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void DELETE_FIELDS(string ID)
        {
            DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                      ACTION: "DELETE_FIELDS",
                                      ID: ID
                                      );

            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Field Deleted Successfully.', 'success');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                                swal({
                                    title: 'Success!',
                                    text: 'Field Deleted Successfully.',
                                    icon: 'success',
                                    button: 'OK'
                                }).then(function(){
                                     window.location.href = window.location.href; });
                            ", true);

        }

        protected void GrdMngFields_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    HiddenField HdnControl = (HiddenField)e.Row.FindControl("HdnControl");
                    LinkButton lbtAddOption = (e.Row.FindControl("lbtAddOption") as LinkButton);


                    string ControlType = HdnControl.Value.Trim();

                    if (ControlType == "Dropdown" || ControlType == "Radio Button" || ControlType == "Checkbox")
                    {
                        lbtAddOption.Visible = true;
                    }
                    else
                    {
                        lbtAddOption.Visible = false;
                    }

                    HtmlControl iconFIRSTENTRY = (HtmlControl)e.Row.FindControl("iconFIRSTENTRY");
                    if (dr["FIRSTENTRY"].ToString() == "True")
                    {
                        iconFIRSTENTRY.Attributes.Add("class", "fa fa-check");
                        iconFIRSTENTRY.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconFIRSTENTRY.Attributes.Add("class", "fa fa-times");
                        iconFIRSTENTRY.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconSECONDENTRY = (HtmlControl)e.Row.FindControl("iconSECONDENTRY");
                    if (dr["SECONDENTRY"].ToString() == "True")
                    {
                        iconSECONDENTRY.Attributes.Add("class", "fa fa-check");
                        iconSECONDENTRY.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSECONDENTRY.Attributes.Add("class", "fa fa-times");
                        iconSECONDENTRY.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconREPEAT = (HtmlControl)e.Row.FindControl("iconREPEAT");
                    if (dr["REPEAT"].ToString() == "True")
                    {
                        iconREPEAT.Attributes.Add("class", "fa fa-check");
                        iconREPEAT.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconREPEAT.Attributes.Add("class", "fa fa-times");
                        iconREPEAT.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconREQUIRED = (HtmlControl)e.Row.FindControl("iconREQUIRED");
                    if (dr["REQUIRED"].ToString() == "True")
                    {
                        iconREQUIRED.Attributes.Add("class", "fa fa-check");
                        iconREQUIRED.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconREQUIRED.Attributes.Add("class", "fa fa-times");
                        iconREQUIRED.Attributes.Add("style", "color: red;");
                    }

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void DATA_Changed(object sender, EventArgs e)
        {
            try
            {

                string FIELDNAME = "", ID = "";

                GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

                FIELDNAME = (row.FindControl("txtFieldName") as TextBox).Text;
                ID = (row.FindControl("lblID") as Label).Text;

                DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                      ACTION: "UPDATE_FIELDNAME",
                                      FIELDNAME: FIELDNAME,
                                      ID: ID
                                      );

                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Fieldname changed successfully.', 'success');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Fieldname changed successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                GET_OPTION();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void DATA_Changed1(object sender, EventArgs e)
        {
            VARIABLENAME_EXIST();
        }

        private void VARIABLENAME_EXIST()
        {
            try
            {
                string VARIABLENAME = "";
                VARIABLENAME = txtVariableName.Text;
                DataSet ds = Dal_Setup.SETUP_FIELD_SP(ACTION: "VARIABLENAME_EXIST", VARIABLENAME: VARIABLENAME);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtVariableName.Text = "";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'VariableName already exists.', 'warning');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", @"
                    swal({
                        title: 'Warning!',
                        text: 'VariableName already exists.',
                        icon: 'warning',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void grdMngOptFields_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                string CommandName = e.CommandName.ToString();

                if (CommandName == "DISABLE")
                {
                    DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                          ACTION: "DISABLE",
                                          ISACTIVE: false,
                                          ID: ID
                                          );
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Field disable successfully.', 'success');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Field disable successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                    GET_OPTION();
                }
                else if (CommandName == "ENABLE")
                {
                    DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                           ACTION: "DISABLE",
                                           ISACTIVE: true,
                                           ID: ID
                                           );
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Field enable successfully.', 'success');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Field enable successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                    GET_OPTION();
                }
                else if (CommandName == "REPEATNO")
                {
                    DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                          ACTION: "REPEATNO",
                                          REPEAT: false,
                                          ID: ID
                                          );
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Repeat disable successfully.', 'success');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Repeat disable successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                    GET_OPTION();
                }
                else if (CommandName == "REPEATYES")
                {
                    DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                           ACTION: "REPEATYES",
                                           REPEAT: true,
                                           ID: ID
                                           );
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Repeat enable successfully.', 'success');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Repeat enable successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);

                    GET_OPTION();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void drplevel_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lblID = (row.FindControl("lblID") as Label);
                string ID = lblID.Text;
                string drplevel = ddl.SelectedValue;

                if (drplevel == "Level 1")
                {
                    DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                              ACTION: "LEVEL_ONE",
                                              FIRSTENTRY: true,
                                              SECONDENTRY: false,
                                              ID: ID
                                              );
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Applicable for Level 1 successfully.', 'success');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Applicable for Level 1 successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                    GET_OPTION();
                }
                else if (drplevel == "Level 2")
                {
                    DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                              ACTION: "LEVEL_TWO",
                                              SECONDENTRY: true,
                                              FIRSTENTRY: false,
                                              ID: ID
                                              );
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Applicable for Level 2 successfully.', 'success');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Applicable for Level 2 successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
                    GET_OPTION();
                }
            }
        }

        
    }
}