using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Text.RegularExpressions;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_SetOnsubmitCrits : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtERR_MSG.Attributes.Add("MaxLength", "500");
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.IsPostBack)
                {
                    string userInput = txtEmailBody.Text; // Get user input
                    string sanitizedInput = HttpUtility.HtmlEncode(userInput);

                    lblList.Text = Request.QueryString["MODULENAME"].ToString();

                    Bind_Visit();
                    GET_CRIT();
                    GET_OPEN_EMAIL();

                    if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv();", true);
                        grdStepCrits.Columns[10].Visible = false;
                    }
                    else
                    {
                        grdStepCrits.Columns[10].Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Visit()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVISITID.DataSource = ds.Tables[0];
                    ddlVISITID.DataValueField = "VISITNUM";
                    ddlVISITID.DataTextField = "VISIT";
                    ddlVISITID.DataBind();
                    ddlVISITID.Items.Insert(0, new ListItem("All Visits", "000"));
                    //if (Request.QueryString["SYSTEM"].ToString() == "Protocol Deviation" || Request.QueryString["SYSTEM"].ToString() == "Solicited Response")
                    //{
                    // ddlVISITID.Items.Insert(0, new ListItem("All Visits", "000"));
                    //}
                }
                else
                {
                    ddlVISITID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_CRIT()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "DM_GET_OnSubmit_CRIT",
                    MODULEID: Request.QueryString["MODULEID"].ToString()
                    );

                grdStepCrits.DataSource = ds;
                grdStepCrits.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnsubmitDefine_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_CRIT();

                divVariableDeclaration.Visible = true;
                divDefineCondition.Visible = false;
                lbtnsubmitDefine.Visible = false;
                lbtnUpdateDefine.Visible = true;

                Bind_Visit1();
                Bind_Module();
                Bind_Field();
                GET_VARIABLEDEC();

                upnlBtn.Update();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_CRIT()
        {
            try
            {
                if (ddlSelectAction.SelectedValue == "Show message with Restriction OnSubmit")
                {
                    chkAllowable.Checked = false;
                }
                else if (ddlSelectAction.SelectedValue == "Show message without Restriction OnSubmit")
                {
                    chkAllowable.Checked = true;
                }
                else
                {
                    chkAllowable.Checked = false;
                }

                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(
                    ACTION: "DM_INSERT_OnSubmit_CRIT",
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    MODULENAME: lblList.Text,
                    SEQNO: txtSEQNO.Text,
                    ERR_MSG: txtERR_MSG.Text,
                    VISITNUM: ddlVISITID.SelectedValue,
                    ALLOWABLE: chkAllowable.Checked,
                    EMAIL_SUBJECT: txtEmailSubject.Text,
                    EMAIL_BODY: txtEmailBody.Text,
                    ACTIONS: ddlSelectAction.SelectedItem.Text
                    );

                hdnCRIT_ID.Value = ds.Tables[0].Rows[0]["CRIT_ID"].ToString();

                if (ddlSelectAction.SelectedItem.Text == "Send Emails OnSubmit")
                {
                    INSERT_EMAIL(hdnCRIT_ID.Value);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void CLEAR_CRIT()
        {
            try
            {
                btnsubmit.Visible = true;

                txtSEQNO.Text = "";
                txtERR_MSG.Text = "";

                drpLISTField1.SelectedIndex = 0;
                drpLISTCondition1.SelectedIndex = 0;
                txtLISTValue1.Text = "";
                drpLISTAndOr1.SelectedIndex = 0;

                drpLISTField2.SelectedIndex = 0;
                drpLISTCondition2.SelectedIndex = 0;
                txtLISTValue2.Text = "";
                drpLISTAndOr2.SelectedIndex = 0;

                drpLISTField3.SelectedIndex = 0;
                drpLISTCondition3.SelectedIndex = 0;
                txtLISTValue3.Text = "";
                drpLISTAndOr3.SelectedIndex = 0;

                drpLISTField4.SelectedIndex = 0;
                drpLISTCondition4.SelectedIndex = 0;
                txtLISTValue4.Text = "";
                drpLISTAndOr4.SelectedIndex = 0;

                drpLISTField5.SelectedIndex = 0;
                drpLISTCondition5.SelectedIndex = 0;
                txtLISTValue5.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_EMAIL(string dsPID)
        {
            try
            {
                for (int i = 0; i < gvEmailds.Rows.Count; i++)
                {
                    TextBox txtEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtEMAILIDs");
                    TextBox txtCCEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtCCEMAILIDs");
                    TextBox txtBCCEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtBCCEMAILIDs");
                    Label lblSiteID = (Label)gvEmailds.Rows[i].FindControl("lblSiteID");

                    if (txtEMAILIDs.Text != "")
                    {
                        DataSet ds = dal_DB.DB_SETUP_CRITs_SP(
                           ACTION: "INSERT_SETUP_EMAIL",
                           ID: dsPID,
                           SITEID: lblSiteID.Text,
                           EMAILIDS: txtEMAILIDs.Text,
                           CCEMAILIDS: txtCCEMAILIDs.Text,
                           BCCEMAILIDS: txtBCCEMAILIDs.Text,
                           TABLENAME: "DM_Onsubmit_CRITs"
                           );
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Visit1()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVisit1.DataSource = ds.Tables[0];
                    ddlVisit1.DataValueField = "VISITNUM";
                    ddlVisit1.DataTextField = "VISIT";
                    ddlVisit1.DataBind();
                    ddlVisit1.Items.Insert(0, new ListItem("All Visits", "000"));
                    //if (Request.QueryString["SYSTEM"].ToString() == "Protocol Deviation" || Request.QueryString["SYSTEM"].ToString() == "Solicited Response")
                    //{
                    //    ddlVisit1.Items.Insert(0, new ListItem("All Visits", "000"));
                    //}
                }
                else
                {
                    ddlVisit1.Items.Clear();
                }
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
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_MODULES_ONSUBMIT_CRIT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule1.DataSource = ds.Tables[0];
                    ddlModule1.DataValueField = "ID";
                    ddlModule1.DataTextField = "MODULENAME";
                    ddlModule1.DataBind();
                    ddlModule1.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlModule1.Items.Clear();
                }
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
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_MODULE_FIELDS",
                MODULEID: ddlModule1.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlField1.DataSource = ds.Tables[0];
                    ddlField1.DataTextField = "FIELDNAME";
                    ddlField1.DataValueField = "ID";
                    ddlField1.DataBind();
                    ddlField1.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlField1.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_CRIT(string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "DM_GET_OnSubmit_CRIT_BYID", ID: ID);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtERR_MSG.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    txtSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                    ddlVISITID.SelectedValue = ds.Tables[0].Rows[0]["VISITID"].ToString();
                    ddlSelectAction.SelectedValue = ds.Tables[0].Rows[0]["ACTIONS"].ToString();
                    txtEmailSubject.Text = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                    txtEmailBody.Text = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();

                    if (ds.Tables[0].Rows[0]["ALLOWABLE"].ToString() == "True")
                    {
                        chkAllowable.Checked = true;
                    }
                    else
                    {
                        chkAllowable.Checked = false;
                    }

                    if (ddlSelectAction.SelectedItem.Text == "Send Emails OnSubmit")
                    {
                        GET_ONSubmit_CRIT_EMAIL(ID);
                    }
                    else
                    {
                        divEmail.Visible = false;
                        divErrMesg.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_ONSubmit_CRIT_EMAIL(string ID)
        {
            DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "DM_OnSubmit_CRIT_EMAIL", ID: ID);

            gvEmailds.DataSource = ds;
            gvEmailds.DataBind();

            divEmail.Visible = true;
            divErrMesg.Visible = false;
        }

        private void DELETE_CRIT(string ID)
        {
            try
            {
                dal_DB.DB_SETUP_CRITs_SP(ACTION: "DM_DELETE_OnSubmit_CRIT", ID: ID);

                dal_DB.DB_SETUP_CRITs_SP(ACTION: "DM_DELETE_OnSubmit_CRIT_EMAIL", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdStepCrits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                hdnCRIT_ID.Value = id;
                if (e.CommandName == "EditCrit")
                {
                    SELECT_CRIT(id);
                    lbtnsubmitDefine.Visible = false;
                    lbtnUpdateDefine.Visible = true;

                    ViewState["EDIT_CRIT"] = "1";

                    if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv();", true);

                        gvVariableDeclare.Columns[5].Visible = false;

                        divVariableDeclaration.Visible = true;
                        divDefineCondition.Visible = true;

                        Bind_Visit1();
                        Bind_Module();
                        Bind_Field();
                        GET_VARIABLEDEC();

                        lbtnNextSetConditions.Visible = false;
                        lbtnCancelVariableDec.Visible = false;
                        lbtnUpdateVariableDec.Visible = false;
                        lbtnSubmitVariableDec.Visible = false;
                    }
                    else
                    {
                        gvVariableDeclare.Columns[5].Visible = true;
                        divVariableDeclaration.Visible = false;
                        divDefineCondition.Visible = false;
                    }
                }
                else if (e.CommandName == "DeleteCrit")
                {
                    DELETE_CRIT(id);

                    Response.Write("<script> alert('OnSubmit/OnLoad criteria deleted successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUpdateDefine_Click(object sender, EventArgs e)
        {
            try
            {
                divVariableDeclaration.Visible = true;
                divDefineCondition.Visible = false;
                lbtnsubmitDefine.Visible = false;
                lbtnUpdateDefine.Visible = true;

                UPDATE_CRIT();
                Bind_Visit1();
                Bind_Module();
                GET_VARIABLEDEC();

                upnlBtn.Update();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_CRIT()
        {
            try
            {
                if (ddlSelectAction.SelectedValue == "Show message with Restriction OnSubmit")
                {
                    chkAllowable.Checked = false;
                }
                else if (ddlSelectAction.SelectedValue == "Show message without Restriction OnSubmit")
                {
                    chkAllowable.Checked = true;
                }
                else
                {
                    chkAllowable.Checked = false;
                }

                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(
                    ACTION: "DM_UPDATE_OnSubmit_CRIT",
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    Criteria_ID: hdnCRIT_ID.Value,
                    MODULENAME: lblList.Text,
                    SEQNO: txtSEQNO.Text,
                    ERR_MSG: txtERR_MSG.Text,
                    VISITNUM: ddlVISITID.SelectedValue,
                    ALLOWABLE: chkAllowable.Checked,
                    EMAIL_SUBJECT: txtEmailSubject.Text,
                    EMAIL_BODY: txtEmailBody.Text,
                    ACTIONS: ddlSelectAction.SelectedItem.Text
                    );

                if (ddlSelectAction.SelectedItem.Text == "Send Emails OnSubmit")
                {
                    INSERT_EMAIL(hdnCRIT_ID.Value);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GET_VARIABLEDEC()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_CRITs_Variables", ID: hdnCRIT_ID.Value);

                gvVariableDeclare.DataSource = ds.Tables[0];
                gvVariableDeclare.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbtnNextSetConditions.Visible = true;
                    divDefineCondition.Visible = true;

                    GET_ADD_CRIT_FIELDS();

                    if (Convert.ToString(ViewState["EDIT_CRIT"]) == "1")
                    {
                        GET_FIELDS_BYID();
                    }
                }
                else
                {
                    lbtnNextSetConditions.Visible = false;
                    divDefineCondition.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModule1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Field();
                upnlBtn.Update();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSubmitVariableDec_Click(object sender, EventArgs e)
        {
            try
            {
                string varNotExists = "";
                bool varExists = true;

                if (chkDerived.Checked)
                {
                    string[] formulaVar = txtFormula.Text.Split(']');

                    for (int i = 0; i < formulaVar.Length; i++)
                    {
                        if (formulaVar[i].ToString().Contains("["))
                        {
                            string var = GetValAfter(formulaVar[i].ToString(), "[");

                            bool resExists = false;

                            for (int a = 0; a < gvVariableDeclare.Rows.Count; a++)
                            {
                                string gvVar = (gvVariableDeclare.Rows[a].FindControl("lblVariableName") as Label).Text;

                                if (!resExists)
                                {
                                    if (var == gvVar)
                                    {
                                        resExists = true;
                                    }
                                }
                            }

                            if (!resExists)
                            {
                                if (varNotExists != "")
                                {
                                    varNotExists += ", " + var;
                                }
                                else
                                {
                                    varNotExists = var;
                                }

                                varExists = false;
                            }
                        }
                    }
                }

                if (varExists)
                {
                    INSERT_VARIABLEDEC();
                    CLEAR_VARIABLEDEC();
                    GET_VARIABLEDEC();
                    lbtnSubmitVariableDec.Visible = true;
                    lbtnUpdateVariableDec.Visible = false;

                    divDefineCondition.Visible = false;
                    divDerivedFalse.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Variable Names entered : " + varNotExists + "')", true);
                }

                upnlBtn.Update();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUpdateVariableDec_Click(object sender, EventArgs e)
        {
            try
            {
                string varNotExists = "";
                bool varExists = true;

                if (chkDerived.Checked)
                {

                    string[] formulaVar = txtFormula.Text.Split(']');

                    for (int i = 0; i < formulaVar.Length; i++)
                    {
                        if (formulaVar[i].ToString().Contains("["))
                        {
                            string var = GetValAfter(formulaVar[i].ToString(), "[");

                            bool resExists = false;

                            for (int a = 0; a < gvVariableDeclare.Rows.Count; a++)
                            {
                                string gvVar = (gvVariableDeclare.Rows[a].FindControl("lblVariableName") as Label).Text;

                                if (!resExists)
                                {
                                    if (var == gvVar)
                                    {
                                        resExists = true;
                                    }
                                }
                            }

                            if (!resExists)
                            {
                                if (varNotExists != "")
                                {
                                    varNotExists += ", " + var;
                                }
                                else
                                {
                                    varNotExists = var;
                                }

                                varExists = false;
                            }
                        }
                    }
                }

                if (varExists)
                {
                    UPDATE_VARIABLEDEC();
                    GET_VARIABLEDEC();
                    CLEAR_VARIABLEDEC();

                    Session.Remove("VARIABLEID");

                    lbtnSubmitVariableDec.Visible = true;
                    lbtnUpdateVariableDec.Visible = false;
                    divDerivedFalse.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Variable Names entered : " + varNotExists + "')", true);
                }

                upnlBtn.Update();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancelVariableDec_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_VARIABLEDEC();

                divDefineCondition.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_VARIABLEDEC()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "Insert_Rule_Variables",
                VARIABLENAME: txtRuleVariableName.Text,
                Criteria_ID: hdnCRIT_ID.Value,
                VISITNUM: ddlVisit1.SelectedValue,
                MODULEID: ddlModule1.SelectedValue,
                FIELDID: ddlField1.SelectedValue,
                DERIVED: chkDerived.Checked.ToString(),
                Formula: txtFormula.Text,
                Condition: txtVariableCondition.Text,
                SEQNO: txtVariableSEQNO.Text
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void UPDATE_VARIABLEDEC()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "Update_Rule_Variables",
                VARIABLENAME: txtRuleVariableName.Text,
                Criteria_ID: hdnCRIT_ID.Value,
                VISITNUM: ddlVisit1.SelectedValue,
                MODULEID: ddlModule1.SelectedValue,
                FIELDID: ddlField1.SelectedValue,
                DERIVED: chkDerived.Checked.ToString(),
                Formula: txtFormula.Text,
                ID: Session["VARIABLEID"].ToString(),
                Condition: txtVariableCondition.Text,
                SEQNO: txtVariableSEQNO.Text
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CLEAR_VARIABLEDEC()
        {
            txtRuleVariableName.Text = "";
            txtVariableSEQNO.Text = "";
            txtFormula.Text = "";
            chkDerived.Checked = false;
            ddlVisit1.SelectedIndex = 0;
            ddlModule1.SelectedIndex = 0;
            ddlField1.Items.Clear();
            txtVariableCondition.Text = "";
            divDerivedTrue.Visible = false;
            divDerivedFalse.Visible = true;
        }

        protected void gvVariableDeclare_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();

                Session["VARIABLEID"] = ID;
                divDefineCondition.Visible = false;

                if (e.CommandName == "EditRule")
                {
                    EDIT_VARIABLE(ID);

                    if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                    {
                        gvVariableDeclare.Columns[5].Visible = false;

                        divVariableDeclaration.Visible = true;
                        divDefineCondition.Visible = true;
                        lbtnNextSetConditions.Visible = false;
                        lbtnCancelVariableDec.Visible = false;
                        lbtnUpdateVariableDec.Visible = false;
                        lbtnSubmitVariableDec.Visible = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "DisableDiv();", true);
                    }
                    else
                    {
                        gvVariableDeclare.Columns[5].Visible = true;
                        divDefineCondition.Visible = false;
                    }

                    upnlBtn.Update();
                }
                else if (e.CommandName == "DeleteRule")
                {
                    DELETE_VARIABLE(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void EDIT_VARIABLE(string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_CRITs_Variables_BYID", ID: ID);

                txtRuleVariableName.Text = ds.Tables[0].Rows[0]["VariableName"].ToString();
                chkDerived.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Derived"].ToString());

                if (chkDerived.Checked == true)
                {
                    divDerivedTrue.Visible = true;
                    divDerivedFalse.Visible = false;
                    txtFormula.Text = ds.Tables[0].Rows[0]["Formula"].ToString();
                    txtVariableSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                }
                else
                {
                    divDerivedTrue.Visible = false;
                    divDerivedFalse.Visible = true;
                    txtVariableSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                    Bind_Visit();
                    ddlVisit1.SelectedValue = ds.Tables[0].Rows[0]["Visit_ID"].ToString();
                    Bind_Module();
                    ddlModule1.SelectedValue = ds.Tables[0].Rows[0]["Module_ID"].ToString();
                    Bind_Field();
                    ddlField1.SelectedValue = ds.Tables[0].Rows[0]["Field_ID"].ToString();
                    txtVariableCondition.Text = ds.Tables[0].Rows[0]["Condition"].ToString();
                }

                lbtnUpdateVariableDec.Visible = true;
                lbtnSubmitVariableDec.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETE_VARIABLE(string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "Delete_Variable",
                    ID: ID);

                GET_VARIABLEDEC();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkDerived_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDerived.Checked == true)
                {
                    divDerivedTrue.Visible = true;
                    divDerivedFalse.Visible = false;
                }
                else
                {
                    divDerivedTrue.Visible = false;
                    divDerivedFalse.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnNextSetConditions_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvVariableDeclare.Rows.Count > 0)
                {
                    divDefineCondition.Visible = true;
                    GET_ADD_CRIT_FIELDS();

                    if (Convert.ToString(ViewState["EDIT_CRIT"]) == "1")
                    {
                        GET_FIELDS_BYID();
                    }
                }
                else
                {
                    divDefineCondition.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ADD_CRIT_FIELDS()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_ONSUBMIT_CRIT_FIELDS",
                MODULEID: Request.QueryString["MODULEID"].ToString(),
                Criteria_ID: hdnCRIT_ID.Value
                );

                BIND_FIELDS(drpLISTField1, ds);
                BIND_FIELDS(drpLISTField2, ds);
                BIND_FIELDS(drpLISTField3, ds);
                BIND_FIELDS(drpLISTField4, ds);
                BIND_FIELDS(drpLISTField5, ds);

                btnsubmit.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_FIELDS_BYID()
        {
            try
            {
                btnsubmit.Visible = true;

                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "SELECT_OnSubmit_CRIT", ID: hdnCRIT_ID.Value);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpLISTField1.SelectedValue = ds.Tables[0].Rows[0]["FIELD1"].ToString();
                    drpLISTCondition1.SelectedValue = ds.Tables[0].Rows[0]["CONDITION1"].ToString();
                    txtLISTValue1.Text = ds.Tables[0].Rows[0]["VALUE1"].ToString();
                    drpLISTAndOr1.SelectedValue = ds.Tables[0].Rows[0]["AND_OR1"].ToString();

                    drpLISTField2.SelectedValue = ds.Tables[0].Rows[0]["FIELD2"].ToString();
                    drpLISTCondition2.SelectedValue = ds.Tables[0].Rows[0]["CONDITION2"].ToString();
                    txtLISTValue2.Text = ds.Tables[0].Rows[0]["VALUE2"].ToString();
                    drpLISTAndOr2.SelectedValue = ds.Tables[0].Rows[0]["AND_OR2"].ToString();

                    drpLISTField3.SelectedValue = ds.Tables[0].Rows[0]["FIELD3"].ToString();
                    drpLISTCondition3.SelectedValue = ds.Tables[0].Rows[0]["CONDITION3"].ToString();
                    txtLISTValue3.Text = ds.Tables[0].Rows[0]["VALUE3"].ToString();
                    drpLISTAndOr3.SelectedValue = ds.Tables[0].Rows[0]["AND_OR3"].ToString();

                    drpLISTField4.SelectedValue = ds.Tables[0].Rows[0]["FIELD4"].ToString();
                    drpLISTCondition4.SelectedValue = ds.Tables[0].Rows[0]["CONDITION4"].ToString();
                    txtLISTValue4.Text = ds.Tables[0].Rows[0]["VALUE4"].ToString();
                    drpLISTAndOr4.SelectedValue = ds.Tables[0].Rows[0]["AND_OR4"].ToString();

                    drpLISTField5.SelectedValue = ds.Tables[0].Rows[0]["FIELD5"].ToString();
                    drpLISTCondition5.SelectedValue = ds.Tables[0].Rows[0]["CONDITION5"].ToString();
                    txtLISTValue5.Text = ds.Tables[0].Rows[0]["VALUE5"].ToString();
                }

                BIND_OPTIONS(drpLISTField1, hfValue1);
                BIND_OPTIONS(drpLISTField2, hfValue2);
                BIND_OPTIONS(drpLISTField3, hfValue3);
                BIND_OPTIONS(drpLISTField4, hfValue4);
                BIND_OPTIONS(drpLISTField5, hfValue5);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void BIND_FIELDS(DropDownList ddl, DataSet ds)
        {
            try
            {
                ddl.DataSource = ds.Tables[0];
                ddl.DataValueField = "VARIABLENAME";
                ddl.DataTextField = "VARIABLENAME";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select Variable--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void BIND_OPTIONS(DropDownList ddl, HiddenField hf)
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_FORM_SPEC_FIELD_ANS_ONSETCRIT",
                    VARIABLENAME: ddl.SelectedValue);

                string Values = "";

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Values += "" + ds.Tables[0].Rows[i]["VALUE"].ToString() + ",";
                    }
                }

                hf.Value = Values.TrimEnd(',');
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLISTField1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(drpLISTField1, hfValue1);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLISTField2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(drpLISTField2, hfValue2);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLISTField3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(drpLISTField3, hfValue3);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLISTField4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(drpLISTField4, hfValue4);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpLISTField5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(drpLISTField5, hfValue5);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string GetValAfter(string Val, string Char)
        {
            string result = "";
            try
            {
                result = Val.Substring(Val.LastIndexOf(Char) + 1);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return result;
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_CRIT_FINAL();

                Response.Write("<script> alert('OnSubmit/OnLoad criteria added/modified successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_CRIT_FINAL()
        {
            try
            {
                string
                FIELDNAME1 = drpLISTField1.SelectedValue,
                FIELDNAME2 = drpLISTField2.SelectedValue,
                FIELDNAME3 = drpLISTField3.SelectedValue,
                FIELDNAME4 = drpLISTField4.SelectedValue,
                FIELDNAME5 = drpLISTField5.SelectedValue,
                CritName = txtERR_MSG.Text,
                SEQNO = txtSEQNO.Text,
                Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
                Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
                Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
                CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null,
                CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;

                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;
                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    AndOr1 = drpLISTAndOr1.SelectedItem.Text;
                }

                if (Condition1 == "IS NULL")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " " + AndOr1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " " + AndOr1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "[_]%")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "![_]%")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] NOT LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "%_%")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "!%_%")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                }
                else
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                }

                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    Condition2 = drpLISTCondition2.SelectedValue;
                    Value2 = txtLISTValue2.Text;
                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        AndOr2 = drpLISTAndOr2.SelectedItem.Text;
                    }

                    if (Condition2 == "IS NULL")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " " + AndOr2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " " + AndOr2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "[_]%")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + Value2 + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "![_]%")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] NOT LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + Value2 + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "%_%")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + Value2 + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "!%_%")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + Value2 + " " + AndOr2 + " ";
                    }
                    else
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + Value2 + " " + AndOr2 + " ";
                    }

                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        Condition3 = drpLISTCondition3.SelectedValue;
                        Value3 = txtLISTValue3.Text;
                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            AndOr3 = drpLISTAndOr3.SelectedItem.Text;
                        }

                        if (Condition3 == "IS NULL")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " " + AndOr3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " " + AndOr3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "[_]%")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + Value3 + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "![_]%")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] NOT LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + Value3 + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "%_%")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + Value3 + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "!%_%")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + Value3 + " " + AndOr3 + " ";
                        }
                        else
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + Value3 + " " + AndOr3 + " ";
                        }


                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            Condition4 = drpLISTCondition4.SelectedValue;
                            Value4 = txtLISTValue4.Text;
                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                AndOr4 = drpLISTAndOr4.SelectedItem.Text;
                            }

                            if (Condition4 == "IS NULL")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " " + AndOr4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " " + AndOr4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "[_]%")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + Value4 + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "![_]%")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] NOT LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + Value4 + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "%_%")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + Value4 + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "!%_%")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + Value4 + " " + AndOr4 + " ";
                            }
                            else
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + Value4 + " " + AndOr4 + " ";
                            }


                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                Condition5 = drpLISTCondition5.SelectedValue;
                                Value5 = txtLISTValue5.Text;

                                if (Condition5 == "IS NULL")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " ";
                                }
                                else if (Condition5 == "IS NOT NULL")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " ";
                                }
                                else if (Condition5 == "[_]%")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] LIKE '[" + Value5 + "]%' ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + Value5;
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] NOT LIKE '[" + Value5 + "]%' ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + Value5;
                                }
                                else if (Condition5 == "%_%")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] LIKE '%" + Value5 + "%' ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + Value5;
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] NOT LIKE '%" + Value5 + "%' ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + Value5;
                                }
                                else
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " '" + Value5 + "' ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + Value5;
                                }
                            }
                        }
                    }
                }


                CritCodeQUERY = CritCodeQUERY1 + " " + CritCodeQUERY2 + " " + CritCodeQUERY3 + " " + CritCodeQUERY4 + " " + CritCodeQUERY5;

                CritQUERY = CritQUERY1 + " " + CritQUERY2 + " " + CritQUERY3 + " " + CritQUERY4 + " " + CritQUERY5;

                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(
                    ACTION: "DM_UPDATE_OnSubmit_CRIT_FINAL",

                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    Criteria_ID: hdnCRIT_ID.Value,
                    MODULENAME: lblList.Text,
                    SEQNO: SEQNO,
                    CritName: CritName,
                    Criteria: CritQUERY,
                    CritCode: CritCodeQUERY,

                    Field1: drpLISTField1.SelectedValue,
                    CONDITION1: drpLISTCondition1.SelectedValue,
                    Value1: txtLISTValue1.Text,
                    AndOr1: drpLISTAndOr1.SelectedValue,

                    Field2: drpLISTField2.SelectedValue,
                    Condition2: drpLISTCondition2.SelectedValue,
                    Value2: txtLISTValue2.Text,
                    AndOr2: drpLISTAndOr2.SelectedValue,

                    Field3: drpLISTField3.SelectedValue,
                    Condition3: drpLISTCondition3.SelectedValue,
                    Value3: txtLISTValue3.Text,
                    AndOr3: drpLISTAndOr3.SelectedValue,

                    Field4: drpLISTField4.SelectedValue,
                    Condition4: drpLISTCondition4.SelectedValue,
                    Value4: txtLISTValue4.Text,
                    AndOr4: drpLISTAndOr4.SelectedValue,

                    Field5: drpLISTField5.SelectedValue,
                    Condition5: drpLISTCondition5.SelectedValue,
                    Value5: txtLISTValue5.Text,

                    ERR_MSG: txtERR_MSG.Text,

                    ALLOWABLE: chkAllowable.Checked
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_OPEN_EMAIL()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_OPEN_EMAIL");
                gvEmailds.DataSource = ds;
                gvEmailds.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSelectAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectAction.SelectedValue == "Send Emails OnSubmit")
            {
                divErrMesg.Visible = false;
                divEmail.Visible = true;
                chkAllowable.Checked = false;
                txtERR_MSG.Text = "";
            }
            else if (ddlSelectAction.SelectedValue == "Show message with Restriction OnSubmit" || ddlSelectAction.SelectedValue == "Show message without Restriction OnSubmit" || ddlSelectAction.SelectedValue == "Reference Note OnLoad")
            {
                divErrMesg.Visible = true;
                divEmail.Visible = false;

                if (ddlSelectAction.SelectedValue == "Show message with Restriction OnSubmit")
                {
                    chkAllowable.Checked = false;
                }
                else if (ddlSelectAction.SelectedValue == "Show message without Restriction OnSubmit")
                {
                    chkAllowable.Checked = true;
                }
                else
                {
                    chkAllowable.Checked = false;
                }
            }
            else
            {
                divErrMesg.Visible = false; ;
                divEmail.Visible = false;
                chkAllowable.Checked = false;
            }
        }

        protected void gridsigninfo_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void grdStepCrits_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }
    }
}