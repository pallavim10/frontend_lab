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
    public partial class UMT_ASSIGN_LABS : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BIND_USER();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void BIND_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(
                        ACTION: "GET_USERS"
                        );

                DrpUser.DataSource = ds.Tables[0];
                DrpUser.DataTextField = "FirstName";
                DrpUser.DataValueField = "UserID";
                DrpUser.DataBind();
                DrpUser.Items.Insert(0, new ListItem("--Select User--", "0"));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Assigned Labs";

                DataSet ds = dal_UMT.UMT_REPORT_SP(ACTION: "GET_USER_LABS");
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
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnRemoveLab_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean status = false;
                for (int i = 0; i < grdAddedgrdLab.Rows.Count; i++)
                {
                    CheckBox chkAddedLabID = (CheckBox)grdAddedgrdLab.Rows[i].FindControl("chkAddedLabID");

                    if (chkAddedLabID.Checked == true)
                    {
                        Label lblLabID = (Label)grdAddedgrdLab.Rows[i].FindControl("lblLabID");
                        Label ID = (Label)grdAddedgrdLab.Rows[i].FindControl("ID");

                        DataSet ds = dal_UMT.UMT_LAB_SP(
                             ACTION: "DELETED_ADDED_LAB",
                             LabID: lblLabID.Text,
                             ID:ID.Text,
                             User_ID:DrpUser.SelectedValue
                          );
                        status = true;

                    }
                   
                }

                if (status == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                            swal({
                                title: 'Success!',
                                text: 'Lab Removed Successfully.',
                                icon: 'success',
                                button: 'OK'
                            }).then(function(){
                                             window.location.href = window.location.href; });
                        ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                            swal({
                                title: 'Warning!',
                                text: 'Please Select a Lab.',
                                icon: 'warning',
                                button: 'OK'
                            }).then(function(){
                                             window.location.href = window.location.href; });
                        ", true);
                }

                if (DrpUser.SelectedValue != "0")
                {
                    GET_ADD_LABS();
                    GET_ADDED_LABS();
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnAddLab_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean statusadd = false;
                for (int i = 0; i < grdLab.Rows.Count; i++)
                {
                    CheckBox ChkLabID = (CheckBox)grdLab.Rows[i].FindControl("ChkLabID");

                    if (ChkLabID.Checked == true)
                    {
                        Label lblLabID = (Label)grdLab.Rows[i].FindControl("lblLabID");

                        DataSet ds = dal_UMT.UMT_LAB_SP(
                             ACTION: "INSERT_ADD_LAB",
                             User_ID: DrpUser.SelectedValue,
                             LabID: lblLabID.Text
                          );
                        statusadd = true;
                    }

                }

                
                if (statusadd == true && DrpUser.SelectedValue != "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Lab Assigned Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                    ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                            swal({
                                title: 'Warning!',
                                text: 'Please Select a Lab.',
                                icon: 'warning',
                                button: 'OK'
                            }).then(function(){
                                             window.location.href = window.location.href; });
                        ", true);
                }

                if (DrpUser.SelectedValue != "0")
                {
                    GET_ADD_LABS();
                    GET_ADDED_LABS();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void GET_ADD_LABS()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_LAB_SP(
                    ACTION: "GET_ADD_LABS",
                    User_ID: DrpUser.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    grdLab.DataSource = ds.Tables[0];
                    grdLab.DataBind();
                }
                else
                {
                    grdLab.DataSource = null;
                    grdLab.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_ADDED_LABS()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_LAB_SP(
                    ACTION: "GET_ADDED_LABS",
                    User_ID: DrpUser.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdAddedgrdLab.DataSource = ds.Tables[0];
                    grdAddedgrdLab.DataBind();
                }
                else
                {
                    grdAddedgrdLab.DataSource = null;
                    grdAddedgrdLab.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        protected void DrpUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DrpUser.SelectedValue != "0")
                {
                    GET_ADD_LABS();
                    GET_ADDED_LABS();
                }
                else
                {
                    grdLab.DataSource = null;
                    grdLab.DataBind();
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
    }
}