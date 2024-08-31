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

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Field Created Successfully.', 'success');", true);
                GET_FIELDS();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void INSERT_FIELDS()
        {
            DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                       ACTION: "INSERT_FIELDS",
                                       FIELDNAME: txtFieldsName.Text,
                                       VARIABLENAME: txtVariableName.Text,
                                       SEQNO: txtSeqNo.Text,
                                       CONTROLTYPE: drpControlType.SelectedValue
                                       );
        }
        private void GET_CLEAR()
        {
            txtFieldsName.Text = "";
            txtVariableName.Text = "";
            txtSeqNo.Text = "";
            drpControlType.ClearSelection();
            lbnUpdate.Visible = false;
            lbtnSubmit.Visible = true;

        }


        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            UPDATE_FIELDS();
            GET_CLEAR();
            lbtnSubmit.Visible = true;
            lbnUpdate.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Field Updated Successfully.', 'success');", true);
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
                                      ID: ViewState["ID"].ToString()
                                      ); 
            }
            catch(Exception ex)
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

        }

        protected void grdMngOptFields_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;


                    LinkButton lbtEnable = (e.Row.FindControl("lbtEnable") as LinkButton);
                    LinkButton lbtDiable = (e.Row.FindControl("lbtDiable") as LinkButton);
                    HiddenField Active = (HiddenField)e.Row.FindControl("HdnISACTIVE");
                    HiddenField HdnControlType = (HiddenField)e.Row.FindControl("HdnControlType");
                    TextBox txtFieldName = (e.Row.FindControl("txtFieldName") as TextBox);
                    LinkButton lbtAddOptionr = (e.Row.FindControl("lbtAddOptionr") as LinkButton);


                    string ControlType = HdnControlType.Value.Trim();
                    if (Active.Value == "True")
                    {
                        lbtEnable.Visible = true;
                        lbtDiable.Visible = false;
                    }
                    else
                    {
                        lbtEnable.Visible = false;
                        lbtDiable.Visible = true;
                        txtFieldName.Enabled = false;
                    }
                    if(ControlType == "Dropdown" || ControlType == "Radio Button" || ControlType == "Checkbox")
                    {
                        lbtAddOptionr.Visible = true;
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

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Field Deleted Successfully.', 'success');", true);

           
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
                
                string FIELDNAME = "", ID ="";

                GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

                FIELDNAME = (row.FindControl("txtFieldName") as TextBox).Text;
                ID = (row.FindControl("lblID") as Label).Text;

                DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                      ACTION: "UPDATE_FIELDNAME",
                                      FIELDNAME: FIELDNAME,
                                      ID: ID
                                      );

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'FieldName changed Successfully.', 'success');", true);

                GET_OPTION();
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'FieldName Disable Successfully.', 'success');", true);
                    GET_OPTION();
                }
                else if (CommandName == "ENABLE")
                {
                    DataSet ds = Dal_Setup.SETUP_FIELD_SP(
                                           ACTION: "DISABLE",
                                           ISACTIVE: true,
                                           ID: ID
                                           );
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'FieldName Enable Successfully.', 'success');", true);
                    GET_OPTION();
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}