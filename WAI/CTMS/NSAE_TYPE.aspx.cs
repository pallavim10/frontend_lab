using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class NSAE_TYPE : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_Module();
                    Bind_SAE_TYPE();
                    GET_SAETYPE();
                }
                lbUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void Bind_SAE_TYPE()
        {
            try
            {
                DataSet ds = dal.SAE_TYPE_SP(ACTION: "GET_SAE_TYPE");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Drp_SAEType.DataSource = ds.Tables[0];
                    Drp_SAEType.DataValueField = "ID";
                    Drp_SAEType.DataTextField = "TYPE";
                    Drp_SAEType.DataBind();
                }
                else
                {
                    Drp_SAEType.DataSource = null;
                    Drp_SAEType.DataBind();
                }
                Drp_SAEType.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_SAE_TYPE();
                Bind_SAE_TYPE();
                GET_Module();
                GET_SAETYPE();
                txtSeqno.Text = "";
                txtSaeType.Text = "";
                lbUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        public void INSERT_SAE_TYPE()
        {
            try
            {
                DataSet ds = dal.SAE_TYPE_SP(ACTION: "INSERT_SAE_TYPE",
                                            SaeTYPE: txtSaeType.Text,
                                            MODULEID: DrpModuleName.SelectedValue,
                                            SEQNO: txtSeqno.Text
                                            );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        public void UPDATE_SAE_TYPE(string TYPEID)
        {
            try
            {
                DataSet ds = dal.SAE_TYPE_SP(ACTION: "UPDATE_SAE_TYPE",
                                            SaeTYPE: txtSaeType.Text,
                                            MODULEID: DrpModuleName.SelectedValue,
                                            SEQNO: txtSeqno.Text,
                                            TYPEID: TYPEID
                                            );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {

            GET_Module();
            GET_SAETYPE();
            txtSaeType.Text = "";
            txtSeqno.Text = "";
            lbSubmit.Visible = true;
            lbUpdate.Visible = false;
        }

        protected void lbUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string id = ViewState["ID"].ToString();
                UPDATE_SAE_TYPE(id);
                txtSaeType.Text = "";
                txtSeqno.Text = "";
                lbSubmit.Visible = true;
                lbUpdate.Visible = false;
                GET_Module();
                GET_SAETYPE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_Module()
        {
            try
            {
                DataSet ds = dal.SAE_TYPE_SP(ACTION: "GET_SAE_MODULE_NAME_MAPPING_FIELD");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DrpModuleName.DataSource = ds.Tables[0];
                    DrpModuleName.DataValueField = "MODULEID";
                    DrpModuleName.DataTextField = "MODULENAME";
                    DrpModuleName.DataBind();
                }
                else
                {
                    DrpModuleName.DataSource = null;
                    DrpModuleName.DataBind();
                }
                DrpModuleName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnAddField_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_Fields();
                GET_SAETYPE();
                GET_AvailableField();
                GET_Field_Details();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnRemoveField_Click(object sender, EventArgs e)
        {
            try
            {
                Remove_Fields();
                GET_SAETYPE();
                GET_AvailableField();
                GET_Field_Details();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void Drp_SAEType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_AvailableField();
                GET_Field_Details();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_AvailableField()
        {
            try
            {
                DataSet ds = dal.SAE_TYPE_SP(ACTION: "GET_AvField", TYPEID: Drp_SAEType.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvAvFields.DataSource = ds;
                    gvAvFields.DataBind();
                }
                else
                {
                    gvAvFields.DataSource = null;
                    gvAvFields.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GET_Field_Details()
        {
            try
            {
                DataSet ds = dal.SAE_TYPE_SP(ACTION: "GET_SAE_TYPE_FIELD", TYPEID: Drp_SAEType.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvAddedFields.DataSource = ds;
                    gvAddedFields.DataBind();
                }
                else
                {
                    gvAddedFields.DataSource = null;
                    gvAddedFields.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void INSERT_Fields()
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvAvFields.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAvFields.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblfieldname = (Label)gvAvFields.Rows[i].FindControl("lblfieldname");
                        Label lblfieldid = (Label)gvAvFields.Rows[i].FindControl("lblfieldid");
                        Label lblVARIABLENAME = (Label)gvAvFields.Rows[i].FindControl("lblVARIABLENAME");
                        Label lblTABLENAME = (Label)gvAvFields.Rows[i].FindControl("lblTABLENAME");
                        TextBox txtSEQNO = (TextBox)gvAvFields.Rows[i].FindControl("txtSEQNO");
                        if (txtSEQNO.Text == "")
                        {
                            txtSEQNO.Text = "0";
                        }
                        DataSet ds = dal.SAE_TYPE_SP(ACTION: "INSERT_Fields", TYPEID: Drp_SAEType.SelectedValue, SEQNO: txtSEQNO.Text, FIELDID: lblfieldid.Text, FIELDNAME: lblfieldname.Text, VARIABLENAME: lblVARIABLENAME.Text, TABLENAME: lblTABLENAME.Text);
                        txtSEQNO.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void Remove_Fields()
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvAddedFields.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAddedFields.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblTYPEID = (Label)gvAddedFields.Rows[i].FindControl("lblTYPEID");
                        Label FIELDID = (Label)gvAddedFields.Rows[i].FindControl("lblFIELDID");
                        DataSet ds = dal.SAE_TYPE_SP(ACTION: "Remove_Fields", TYPEID: lblTYPEID.Text, FIELDID: FIELDID.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void GET_SAETYPE()
        {
            try
            {
                DataSet ds = dal.SAE_TYPE_SP(ACTION: "GET_SAE_TYPE_DETAILS");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdSAETYPE.DataSource = ds.Tables[0];
                    grdSAETYPE.DataBind();
                }
                else
                {
                    grdSAETYPE.DataSource = null;
                    grdSAETYPE.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSAETYPE_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;
                if (e.CommandName == "EditSAETYPE")
                {
                    EDIT_SAE_TYPE(ID);
                    lbSubmit.Visible = false;
                    lbUpdate.Visible = true;
                }
                else if (e.CommandName == "DeleteSAETYPE")
                {
                    DELETE_SAE_TYPE(ID);
                    GET_SAETYPE();
                    Bind_SAE_TYPE();
                    lbSubmit.Visible = true;
                    lbUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void EDIT_SAE_TYPE(string ID)
        {
            try
            {
                DataSet ds = dal.SAE_TYPE_SP(ACTION: "GET_SAE_TYPE_BYID", TYPEID: ID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSaeType.Text = ds.Tables[0].Rows[0]["TYPE"].ToString();
                    DrpModuleName.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();
                    txtSeqno.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETE_SAE_TYPE(string ID)
        {
            try
            {
                DataSet ds = dal.SAE_TYPE_SP(ACTION: "REMOVE_SAE_TYPE_BYID", TYPEID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSAETYPE_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string STATE_COUNTS = drv["FIELD_COUNTS"].ToString();
                    LinkButton lbtndelete = (e.Row.FindControl("lbtndelete") as LinkButton);
                    if (STATE_COUNTS == "0")
                    {
                        lbtndelete.Visible = true;
                    }
                    else
                    {
                        lbtndelete.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}