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
    public partial class MANAGE_FEILDS_DATA : System.Web.UI.Page
    {
        DAL_SETUP Dal_Setup = new DAL_SETUP();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_PROPERTIES();
                    DISPLAY();
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void DISPLAY()
        {
            try
            {
                string VARIABLENAME = Request.QueryString["VARIABLENAME"].ToString();
                if (VARIABLENAME == "SID" || VARIABLENAME == "SUBJID")
                {
                    divMaxLength.Visible = true;
                    divVerifyMaster.Visible = true;
                    divControl.Visible = true;
                    divverifySID.Visible = false;
                }
                else if (VARIABLENAME == "SCANALQ")
                {
                    divverifySID.Visible = true;
                    divMaxLength.Visible = false;
                    divVerifyMaster.Visible = false;
                    divControl.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void GET_PROPERTIES()
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_FIELDOPTION_SP(
                    ACTION: "GET_PROPERTIES",
                    ID: Request.QueryString["ID"].ToString(),
                    VARIABLENAME: Request.QueryString["VARIABLENAME"].ToString()
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpControlType.SelectedValue = ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString();
                    drpVerifyMaster.SelectedValue = ds.Tables[0].Rows[0]["ISVERIFY"].ToString();
                    drpVerifySID.SelectedValue = ds.Tables[0].Rows[0]["ISVERIFY"].ToString();
                    txtMaxLength.Text = ds.Tables[0].Rows[0]["MAXLEN"].ToString();

                    if(drpControlType.SelectedValue == "Textbox")
                    {
                        Max_Length.Attributes["class"] = Max_Length.Attributes["class"].Replace("d-none", "");
                        txtMaxLength.CssClass += " required";
                    }
                    else
                    {
                        txtMaxLength.CssClass = txtMaxLength.CssClass.Replace("required", "").Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SUBMIT_PROPERTIES();
                GET_PROPERTIES();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void SUBMIT_PROPERTIES()
        {
            try
            {
                if (Request.QueryString["VARIABLENAME"].ToString() == "SID")
                {
                    DataSet ds = Dal_Setup.SETUP_FIELDOPTION_SP(
                    ACTION: "UPDATE_SID_PROPERTIES",
                    CONTROLTYPE: drpControlType.SelectedItem.Text,
                    MAXLEN: txtMaxLength.Text,
                    ISVERIFY: drpVerifyMaster.SelectedItem.Text,
                    ID: Request.QueryString["ID"].ToString(),
                    VARIABLENAME: Request.QueryString["VARIABLENAME"].ToString()
                    );

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Properties Updated successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                    ", true);
                }
                else if (Request.QueryString["VARIABLENAME"].ToString() == "SUBJID")
                {
                    DataSet ds = Dal_Setup.SETUP_FIELDOPTION_SP(
                    ACTION: "UPDATE_SUBJID_PROPERTIES",
                    CONTROLTYPE: drpControlType.SelectedItem.Text,
                    MAXLEN: txtMaxLength.Text,
                    ISVERIFY: drpVerifyMaster.SelectedItem.Text,
                    ID: Request.QueryString["ID"].ToString(),
                    VARIABLENAME: Request.QueryString["VARIABLENAME"].ToString()
                    );

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Properties Updated successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                    ", true);
                }
                else if (Request.QueryString["VARIABLENAME"].ToString() == "SCANALQ")
                {
                    DataSet ds = Dal_Setup.SETUP_FIELDOPTION_SP(
                    ACTION: "UPDATE_SCANALQ_PROPERTIES",
                    ISVERIFY: drpVerifySID.SelectedItem.Text,
                    ID: Request.QueryString["ID"].ToString(),
                    VARIABLENAME: Request.QueryString["VARIABLENAME"].ToString()
                    );

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Properties Updated successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                    ", true);
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GET_PROPERTIES();
        }
    }
}