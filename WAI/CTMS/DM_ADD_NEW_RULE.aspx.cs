using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_ADD_NEW_RULE : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescription.Attributes.Add("MaxLength", "1000");
            txtQuery.Attributes.Add("MaxLength", "1000");
            

            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["ID"] == null)
                    {
                        Bind_Visit();
                    }
                    else
                    {
                        hdnID.Value = Request.QueryString["ID"].ToString();
                        EDIT_RULE(hdnID.Value);
                        lbtnsubmit.Visible = false;
                        lbtnUpdate.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void EDIT_RULE(string RULE_ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "EDIT_RULE", ID: RULE_ID);

                Bind_Visit();
                ddlVisit.SelectedValue = ds.Tables[0].Rows[0]["VISITNUM"].ToString();

                Bind_Module();
                ddlModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();

                Bind_Field();
                ddlField.SelectedValue = ds.Tables[0].Rows[0]["FIELDID"].ToString();

                txtRuleID.Text = ds.Tables[0].Rows[0]["RULEID"].ToString();

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["GEN_QUERY"].ToString()) == true)
                {
                    rbtActions.SelectedValue = "Generate Query";
                }

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["SET_VALUE"].ToString()) == true)
                {
                    rbtActions.SelectedValue = "Set Field Value";
                }

                ddlNature.SelectedValue = ds.Tables[0].Rows[0]["Nature"].ToString();

                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtQuery.Text = ds.Tables[0].Rows[0]["QueryText"].ToString();
                txtSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                txtcondtion.Text = ds.Tables[0].Rows[0]["Condition"].ToString();
                txtformulaforsetvalue.Text = ds.Tables[0].Rows[0]["FORMULA_VALUE"].ToString();
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
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_VISIT");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlVisit.DataSource = ds.Tables[0];
                    ddlVisit.DataValueField = "VISITNUM";
                    ddlVisit.DataTextField = "VISIT";
                    ddlVisit.DataBind();
                    ddlVisit.Items.Insert(0, new ListItem("All Visits", "000"));

                    if (Request.QueryString["VISITNUM"] != null)
                    {
                        ddlVisit.SelectedValue = Request.QueryString["VISITNUM"].ToString();
                    }

                    Bind_Module();
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

                    if (Request.QueryString["MODULEID"] != null)
                    {
                        ddlModule.SelectedValue = Request.QueryString["MODULEID"].ToString();
                    }

                    Bind_Field();
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

                    if (Request.QueryString["FIELDID"] != null)
                    {
                        ddlField.SelectedValue = Request.QueryString["FIELDID"].ToString();
                    }
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

        protected void lbtnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_RULE();
                lbtnsubmit.Visible = false;
                divVariableDeclaration.Visible = true;
                lbtnUpdate.Visible = true;

                Bind_Visit1();
                Bind_Module1();
                Bind_Field1();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void INSERT_RULE()
        {
            try
            {
                bool GEN_QUERY = false, SET_VALUE = false;
                if (rbtActions.SelectedValue == "Generate Query")
                {
                    GEN_QUERY = true;
                }

                if (rbtActions.SelectedValue == "Set Field Value")
                {
                    SET_VALUE = true;
                }

                DataSet ds = dal_DB.DB_RULE_SP(
                    ACTION: "INSERT_RULE",
                    VISITNUM: ddlVisit.SelectedValue,
                    MODULEID: ddlModule.SelectedValue,
                    FIELDID: ddlField.SelectedValue,
                    RULEID: txtRuleID.Text,
                    SEQNO: txtSEQNO.Text,
                    NATURE: ddlNature.SelectedValue,
                    GEN_QUERY: GEN_QUERY,
                    SET_VALUE: SET_VALUE,
                    Description: txtDescription.Text,
                    QueryText: txtQuery.Text
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ID"].ToString() == "Already Exists")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Rule Id already exists. Please change rule id.')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Rule details addded successfully. Please define rule variables.')", true);

                        hdnID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_RULE();

                divVariableDeclaration.Visible = true;
                GET_VARIABLEDEC();

                Bind_Visit1();
                Bind_Module1();
                Bind_Field1();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void UPDATE_RULE()
        {
            try
            {
                bool GEN_QUERY = false, SET_VALUE = false;
                if (rbtActions.SelectedValue == "Generate Query")
                {
                    GEN_QUERY = true;
                }

                if (rbtActions.SelectedValue == "Set Field Value")
                {
                    SET_VALUE = true;
                }

                DataSet ds = dal_DB.DB_RULE_SP(
                    ACTION: "UPDATE_RULE",
                    VISITNUM: ddlVisit.SelectedValue,
                    MODULEID: ddlModule.SelectedValue,
                    FIELDID: ddlField.SelectedValue,
                    RULEID: txtRuleID.Text,
                    SEQNO: txtSEQNO.Text,
                    NATURE: ddlNature.SelectedValue,
                    GEN_QUERY: GEN_QUERY,
                    SET_VALUE: SET_VALUE,
                    Description: txtDescription.Text,
                    QueryText: txtQuery.Text,
                    ID: hdnID.Value
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Rule Id already exists. Please change rule id.')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Rule details updated successfully. Please define rule variables.')", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("DM_RULES_LISTS.aspx", true);
        }

        protected void rbtActions_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtActions.SelectedValue == "Set Field Value")
                {
                    divforsetvalue.Visible = true;
                    txtcondtion.Text = "";
                    txtformulaforsetvalue.Text = "";
                    divSetCondition.Visible = true;
                }
                else
                {
                    txtformulaforsetvalue.Text = "";
                    divforsetvalue.Visible = false;
                    divSetCondition.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Define Variables
        public void Bind_Visit1()
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_VISIT");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlVisit1.DataSource = ds.Tables[0];
                    ddlVisit1.DataValueField = "VISITNUM";
                    ddlVisit1.DataTextField = "VISIT";
                    ddlVisit1.DataBind();
                    ddlVisit1.Items.Insert(0, new ListItem("All Visits", "000"));
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

        protected void ddlVisit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Module1();
                Bind_Field1();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Module1()
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_MODULE_DM_PROJECT_MASTER",
                    VISITNUM: ddlVisit1.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule1.DataSource = ds.Tables[0];
                    ddlModule1.DataValueField = "MODULEID";
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

        protected void ddlModule1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Field1();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Field1()
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_FIELD_DM_PROJECT_MASTER",
                    MODULEID: ddlModule1.SelectedValue,
                    VISITNUM: ddlVisit1.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlField1.DataSource = ds.Tables[0];
                    ddlField1.DataTextField = "FIELDNAME";
                    ddlField1.DataValueField = "FIELD_ID";
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

        protected void GET_VARIABLEDEC()
        {
            try
            {
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_Rule_Variables", RULEID: hdnID.Value);

                gvVariableDeclare.DataSource = ds.Tables[0];
                gvVariableDeclare.DataBind();

                ViewState["dtVariableDeclare"] = ds.Tables[0];

                repeatVariables.DataSource = ds.Tables[1];
                repeatVariables.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbtnNextSetConditions.Visible = true;
                }
                else
                {
                    lbtnNextSetConditions.Visible = false;
                    divDefineCondition.Visible = false;
                    divEvaluateConditions.Visible = false;
                    divFinalSubmit.Visible = false;
                }
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
                    divEvaluateConditions.Visible = false;
                    divFinalSubmit.Visible = false;
                    divDerivedFalse.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Variable Names entered : " + varNotExists + "')", true);
                }
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
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "Insert_Rule_Variables",
                    VARIABLENAME_DEF: txtRuleVariableName.Text,
                    SEQNO: txtVariableSEQNO.Text,
                    DERIVED: chkDerived.Checked.ToString(),
                    FORMULA: txtFormula.Text,
                    VISITNUM: ddlVisit1.SelectedValue,
                    MODULEID: ddlModule1.SelectedValue,
                    FIELDID: ddlField1.SelectedValue,
                    CONDITION: txtVariableCondition.Text,
                    RULEID: hdnID.Value
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Variable addded successfully.')", true);
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

                    lbtnSubmitVariableDec.Visible = true;
                    lbtnUpdateVariableDec.Visible = false;
                    divDerivedFalse.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Variable Names entered : " + varNotExists + "')", true);
                }
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
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "Update_Rule_Variables",
                    VARIABLENAME_DEF: txtRuleVariableName.Text,
                    SEQNO: txtVariableSEQNO.Text,
                    DERIVED: chkDerived.Checked.ToString(),
                    FORMULA: txtFormula.Text,
                    VISITNUM: ddlVisit1.SelectedValue,
                    MODULEID: ddlModule1.SelectedValue,
                    FIELDID: ddlField1.SelectedValue,
                    CONDITION: txtVariableCondition.Text,
                    RULEID: hdnID.Value,
                    ID: hdnVariableID.Value
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Variable updated successfully.')", true);
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
                hdnVariableID.Value = "";
                divDefineCondition.Visible = false;
                divEvaluateConditions.Visible = false;
                lbtnSubmitVariableDec.Visible = true;
                lbtnUpdateVariableDec.Visible = false;
                divFinalSubmit.Visible = false;
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

                divDefineCondition.Visible = false;
                divEvaluateConditions.Visible = false;
                divFinalSubmit.Visible = false;

                if (e.CommandName == "EditRule")
                {
                    hdnVariableID.Value = ID;
                    EDIT_VARIABLE(ID);
                }
                else if (e.CommandName == "DeleteRule")
                {
                    DELETE_VARIABLE(ID); ;
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
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "GET_Rule_Variables_BYID", ID: ID);

                txtRuleVariableName.Text = ds.Tables[0].Rows[0]["VARIABLENAME_DEF"].ToString();
                chkDerived.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Derived"].ToString());

                if (chkDerived.Checked == true)
                {
                    divDerivedTrue.Visible = true;
                    divDerivedFalse.Visible = false;
                    txtVariableSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                    txtFormula.Text = ds.Tables[0].Rows[0]["Formula"].ToString();
                }
                else
                {
                    divDerivedTrue.Visible = false;
                    divDerivedFalse.Visible = true;
                    txtVariableSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                    Bind_Visit1();
                    ddlVisit1.SelectedValue = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                    Bind_Module1();
                    ddlModule1.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();
                    Bind_Field1();
                    ddlField1.SelectedValue = ds.Tables[0].Rows[0]["FIELDID"].ToString();
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
                DataSet ds = dal_DB.DB_RULE_SP(ACTION: "Delete_Variable", ID: ID);
                GET_VARIABLEDEC();
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
                }
                else
                {
                    divDefineCondition.Visible = false;
                }

                if (rbtActions.SelectedValue == "Set Field Value")
                {
                    divforsetvalue.Visible = true;
                    divSetCondition.Visible = true;
                }
                else
                {
                    divforsetvalue.Visible = false;
                    divSetCondition.Visible = true;
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Variables added successfully. Please define condition or set value formula.')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSubmitConditions_Click(object sender, EventArgs e)
        {
            try
            {
                dal_DB.DB_RULE_SP(ACTION: "UPDATE_RULE_CONDITION",
                    CONDITION: txtcondtion.Text,
                    SET_VALUE_FORMULA: txtformulaforsetvalue.Text,
                    ID: hdnID.Value
                    );

                divEvaluateConditions.Visible = true;
                divFinalSubmit.Visible = true;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Condition or set value formula added/modified successfully. Please evaluate conditions.')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatVariables_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView dr = e.Item.DataItem as DataRowView;
                    DAL_DM dal_DM = new DAL_DM();

                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();
                    string MAXLEN = dr["MAXLEN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string DERIVED = dr["Derived"].ToString();

                    if (DERIVED != "True")
                    {
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (MULTILINEYN == "True")
                            {
                                btnEdit.TextMode = TextBoxMode.MultiLine;
                                btnEdit.Attributes.Add("style", "width: 300px;");
                            }
                        }
                        if (CONTROLTYPE == "DROPDOWN")
                        {
                            DropDownList btnEdit = (DropDownList)e.Item.FindControl("DRP_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_OPTIONS_LIST", VARIABLENAME: VARIABLENAME);
                            btnEdit.DataSource = ds;
                            btnEdit.DataTextField = "TEXT";
                            btnEdit.DataValueField = "VALUE";
                            btnEdit.DataBind();
                        }
                        if (CONTROLTYPE == "CHECKBOX")
                        {
                            Repeater repeat_CHK = (Repeater)e.Item.FindControl("repeat_CHK");
                            repeat_CHK.Visible = true;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_OPTIONS_LIST", VARIABLENAME: VARIABLENAME);
                            repeat_CHK.DataSource = ds;
                            repeat_CHK.DataBind();
                        }
                        if (CONTROLTYPE == "RADIOBUTTON")
                        {
                            Repeater repeat_RAD = (Repeater)e.Item.FindControl("repeat_RAD");
                            repeat_RAD.Visible = true;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_OPTIONS_LIST", VARIABLENAME: VARIABLENAME);
                            repeat_RAD.DataSource = ds;
                            repeat_RAD.DataBind();
                        }
                    }
                    else
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.Text = dr["Formula"].ToString();
                        btnEdit.ReadOnly = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + "form-control";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCheckEvaluateCondition_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal = new DAL();
                string varname = "", CONTROLTYPE = "", strdata = "", CONDITION = "", Derived = "", NewVariableName = "";
                DataTable table = new DataTable();
                table.Columns.Add("VARIABLENAME", typeof(string));
                table.Columns.Add("DATA", typeof(string));

                for (int i = 0; i < repeatVariables.Items.Count; i++)
                {
                    varname = ((Label)repeatVariables.Items[i].FindControl("lblConditionVariable")).Text;
                    CONTROLTYPE = ((Label)repeatVariables.Items[i].FindControl("lblConditionControlType")).Text;
                    Derived = ((Label)repeatVariables.Items[i].FindControl("Derived")).Text;

                    if (Derived != "True")
                    {
                        if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                        {
                            if (CONTROLTYPE == "TEXTBOX")
                            {
                                strdata = CheckDatatype(((TextBox)repeatVariables.Items[i].FindControl("TXT_FIELD")).Text);
                            }
                            else if (CONTROLTYPE == "DROPDOWN")
                            {
                                strdata = CheckDatatype(((DropDownList)repeatVariables.Items[i].FindControl("DRP_FIELD")).SelectedValue);
                            }
                            else if (CONTROLTYPE == "CHECKBOX")
                            {
                                Repeater repeat_CHK = repeatVariables.Items[i].FindControl("repeat_CHK") as Repeater;
                                string Chk = "";
                                for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                {
                                    if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                    {
                                        if (Chk.ToString() == "")
                                        {
                                            Chk = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                        }
                                        else
                                        {
                                            Chk += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                        }
                                    }
                                }
                                strdata = CheckDatatype(Chk);
                            }
                            else if (CONTROLTYPE == "RADIOBUTTON")
                            {
                                Repeater repeat_RAD = repeatVariables.Items[i].FindControl("repeat_RAD") as Repeater;
                                string Rad = "";
                                for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                {
                                    if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                    {
                                        Rad = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                    }
                                }
                                strdata = CheckDatatype(Rad);
                            }

                            table.Rows.Add(varname, strdata);

                            NewVariableName = "[" + varname + "]";

                            if (CONDITION == "")
                            {
                                if (txtcondtion.Text.Contains(NewVariableName))
                                {
                                    CONDITION = txtcondtion.Text.Replace(NewVariableName, strdata);
                                }
                            }
                            else
                            {
                                if (CONDITION.Contains(NewVariableName))
                                {
                                    CONDITION = CONDITION.Replace(NewVariableName, strdata);
                                }
                            }
                        }
                    }
                    else
                    {
                        strdata = ((TextBox)repeatVariables.Items[i].FindControl("TXT_FIELD")).Text;

                        foreach (DataRow dr in table.Rows)
                        {
                            string drVARIABLENAME = "[" + dr["VARIABLENAME"] + "]";

                            if (strdata.Contains(drVARIABLENAME))
                            {
                                string CHKDATA = CheckDatatype(dr["DATA"].ToString());
                                if (CHKDATA != "")
                                {
                                    strdata = strdata.Replace(drVARIABLENAME, CHKDATA);
                                }
                            }
                        }

                        SqlCommand cmd1;
                        DataSet ds1 = new DataSet();
                        SqlDataAdapter sda;
                        try
                        {
                            SqlConnection con = new SqlConnection(dal.getconstr());

                            cmd1 = new SqlCommand("DB_RULE_SP", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.AddWithValue("@Action", "CHECKRULE_FORMULA_VALUE");
                            cmd1.Parameters.AddWithValue("@Formula", strdata);

                            con.Open();
                            sda = new SqlDataAdapter(cmd1);
                            sda.Fill(ds1);
                            cmd1.Dispose();
                            con.Close();
                        }
                        catch (SqlException sqlEx)
                        {
                            divSyntexError.Visible = true;
                            divResult.Visible = false;
                            lblRed.Text = sqlEx.Message.ToString();
                        }

                        if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            strdata = ds1.Tables[0].Rows[0]["Data"].ToString();
                        }

                        table.Rows.Add(varname, strdata);

                        NewVariableName = "[" + varname + "]";

                        if (CONDITION == "")
                        {
                            if (txtcondtion.Text.Contains(NewVariableName))
                            {
                                CONDITION = txtcondtion.Text.Replace(NewVariableName, strdata);
                            }
                        }
                        else
                        {
                            if (CONDITION.Contains(NewVariableName))
                            {
                                CONDITION = CONDITION.Replace(NewVariableName, strdata);
                            }
                        }
                    }
                }

                table.Rows.Add("Condition: ", CONDITION);

                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd;
                    SqlDataAdapter adp;

                    SqlConnection con = new SqlConnection(dal.getconstr());

                    bool GEN_QUERY = false, SET_VALUE = false;
                    if (rbtActions.SelectedValue == "Generate Query")
                    {
                        GEN_QUERY = true;
                    }

                    if (rbtActions.SelectedValue == "Set Field Value")
                    {
                        SET_VALUE = true;
                    }

                    cmd = new SqlCommand("DB_RULE_SP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "CHECKRULE_CONDITION");
                    cmd.Parameters.AddWithValue("@ID", hdnID.Value);
                    cmd.Parameters.AddWithValue("@Condition", CONDITION);

                    con.Open();
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(ds);
                    cmd.Dispose();
                    con.Close();

                    hdnTested.Value = ds.Tables[0].Rows[0]["TESTED"].ToString();

                    divSyntexError.Visible = false;

                    if (ds.Tables[0].Rows[0]["TESTED"].ToString() == "1")
                    {
                        lblBlack.Text = "True";
                    }
                    else
                    {
                        lblBlack.Text = "False";
                    }
                    divResult.Visible = true;
                    rptEvaluateResults.DataSource = table;
                    rptEvaluateResults.DataBind();
                }
                catch (SqlException sqlEx)
                {
                    divSyntexError.Visible = true;
                    divResult.Visible = false;
                    lblRed.Text = sqlEx.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnFinalSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool GEN_QUERY = false, SET_VALUE = false;
                if (rbtActions.SelectedValue == "Generate Query")
                {
                    GEN_QUERY = true;
                }

                if (rbtActions.SelectedValue == "Set Field Value")
                {
                    SET_VALUE = true;
                }

                DataSet ds = dal_DB.DB_RULE_SP(
                    ACTION: "UPDATE_FINAL_SUBMIT_RULE",
                    VISITNUM: ddlVisit.SelectedValue,
                    MODULEID: ddlModule.SelectedValue,
                    FIELDID: ddlField.SelectedValue,
                    RULEID: txtRuleID.Text,
                    SEQNO: txtSEQNO.Text,
                    NATURE: ddlNature.SelectedValue,
                    GEN_QUERY: GEN_QUERY,
                    SET_VALUE: SET_VALUE,
                    Description: txtDescription.Text,
                    QueryText: txtQuery.Text,
                    CONDITION: txtcondtion.Text,
                    SET_VALUE_FORMULA: txtformulaforsetvalue.Text,
                    ID: hdnID.Value
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Rule created successfully.');window.location='DM_RULES_LISTS.aspx'; ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public bool ISDATE(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
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

        private string CheckDatatype(string Val)
        {
            string RESULT = "";
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                int i = 0;
                float j = 0;
                double k = 0;
                DateTime l;

                if (Val.Contains("dd/"))
                {
                    Val = Val.Replace("dd/", "01/");
                }

                if (Val.Contains("mm/"))
                {
                    Val = Val.Replace("mm/", "01/");
                }

                if (int.TryParse(Val, out i))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (float.TryParse(Val, out j))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (double.TryParse(Val, out k))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (DateTime.TryParse(Val, out l) || cf.isDate(Val))
                {
                    RESULT = "dbo.CastDate('" + Val + "')";
                }
                else
                {
                    RESULT = "N'" + Val + "'";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            return RESULT;
        }
    }
}