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
    public partial class MANAGE_ADD_OPTION : System.Web.UI.Page
    {

        DAL_SETUP Dal_Setup = new DAL_SETUP();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GET_ADDED_OPTION(Request.QueryString["ID"].ToString());
            }

           
        }
        protected void btnaddOption_Click(object sender, EventArgs e)
        {
            INSERT_OPTION();
            ClearOption();
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Option Added Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            GET_ADDED_OPTION(Request.QueryString["ID"].ToString());
            

        }

        private void GET_ADDED_OPTION(string ID)
        {
            DataSet ds = Dal_Setup.SETUP_FIELDOPTION_SP(
                        ACTION: "GET_ADDED_OPTION",
                        ID: ID
                        );

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                GrdOption.DataSource = ds.Tables[0];
                GrdOption.DataBind();
            }
            else
            {
                GrdOption.DataSource = null;
                GrdOption.DataBind();
            }

            DataSet ds2 = Dal_Setup.SETUP_FIELDOPTION_SP(
                        ACTION: "GET_FIELDSOPTION",
                        ID: ID
                        );
            if (ds2.Tables[0].Rows.Count > 0 && ds2.Tables.Count > 0)
            {
                txtfiledNameOpt.Text = ds2.Tables[0].Rows[0]["FIELDNAME"].ToString();
                txtVariableOpt.Text = ds2.Tables[0].Rows[0]["VARIABLENAME"].ToString();
            }
        }
        private void INSERT_OPTION()
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_FIELDOPTION_SP(
                        ACTION: "INSERT_OPTION",
                        FIELD_ID: Request.QueryString["ID"].ToString(),
                        VARIABLENAME: txtVariableOpt.Text.Trim(),
                        SEQNO: txtSquenceNoOpt.Text,
                        OPTION: txtoption.Text
                    );
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private void ClearOption()
        {
            txtSquenceNoOpt.Text = "";
            txtoption.Text = "";
            btnaddOption.Visible = true;
            btnUpdateOption.Visible = false;
        }

        protected void btnUpdateOption_Click(object sender, EventArgs e)
        {
            UPDATE_OPTION();

            btnaddOption.Visible = true;
            btnUpdateOption.Visible = false;
            ClearOption();
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Option Updated Successfully.', 'success');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Option Updated Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);
            GET_ADDED_OPTION(Request.QueryString["ID"].ToString());
        }

        private void UPDATE_OPTION()
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_FIELDOPTION_SP(
                        ACTION: "UPDATE_OPTION",
                        FIELD_ID: Request.QueryString["ID"].ToString(),
                        VARIABLENAME: txtVariableOpt.Text.Trim(),
                        SEQNO: txtSquenceNoOpt.Text,
                        OPTION: txtoption.Text,
                        ID: ViewState["Option_ID"].ToString()
                    );
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnCancelOption_Click(object sender, EventArgs e)
        {
            ClearOption();

        }
        protected void GrdOption_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Option_ID = e.CommandArgument.ToString();
            ViewState["Option_ID"] = Option_ID;
            string CommandName = e.CommandName.ToString();

            if (CommandName == "EDITED")
            {
                EDIT_OPTION(Option_ID);
                btnaddOption.Visible = false;
                btnUpdateOption.Visible = true;

               
            }
            else if (CommandName == "DELETED")
            {

                DELETE_OPTION(Option_ID);
                
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'Option Deleted Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });
                ", true);

                GET_ADDED_OPTION(Request.QueryString["ID"].ToString());
            }
        }

        private void EDIT_OPTION(string ID)
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_FIELDOPTION_SP(
                        ACTION: "EDIT_OPTION",
                        ID: ID
                    );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtfiledNameOpt.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                    txtVariableOpt.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();
                    txtSquenceNoOpt.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                    txtoption.Text = ds.Tables[0].Rows[0]["OPTION_VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();

            }
        }

        private void DELETE_OPTION(string ID)
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_FIELDOPTION_SP(
                        ACTION: "DELETE_OPTION",
                        ID: ID
                    );
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                

            }
        }
    }
}