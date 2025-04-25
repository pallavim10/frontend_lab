using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class NIWRS_STOP_CLAUSE : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMsg.Attributes.Add("MaxLength", "100");

            try
            {
                if (!IsPostBack)
                {
                    GetSites();
                    Bind_Module();
                    Bind_SCModule();
                    GET_VARIABLEDEC();
                    GET_CRIT();
                    GET_NavSOURCE();
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
                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "GET_MODULES");
                if (ds.Tables[0].Rows.Count > 0)
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

        public void Bind_SCModule()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_STOP_CLAUSE_SP(ACTION: "GET_MODULES");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSCModule.DataSource = ds.Tables[0];
                    ddlSCModule.DataValueField = "MODULEID";
                    ddlSCModule.DataTextField = "MODULENAME";
                    ddlSCModule.DataBind();
                    ddlSCModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlSCModule.Items.Clear();
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
                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "GET_FIELDS", MODULEID: ddlModule.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlField.DataSource = ds.Tables[0];
                    ddlField.DataTextField = "FIELDNAME";
                    ddlField.DataValueField = "ID";
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

        protected void CLEAR_VARIABLEDEC()
        {
            txtRuleVariableName.Text = "";
            ddlModule.SelectedIndex = 0;
            ddlField.Items.Clear();
        }

        protected void GET_VARIABLEDEC()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "GET_SC_VARIABLES", MODULEID: ddlModule.SelectedValue);

                gvVariableDeclare.DataSource = ds.Tables[0];
                gvVariableDeclare.DataBind();

                BIND_FIELDS(drpLISTField1, ds);
                BIND_FIELDS(drpLISTField2, ds);
                BIND_FIELDS(drpLISTField3, ds);
                BIND_FIELDS(drpLISTField4, ds);
                BIND_FIELDS(drpLISTField5, ds);
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
                INSERT_VARIABLEDEC();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Variable defined successfully.'); ", true);
                CLEAR_VARIABLEDEC();
                GET_VARIABLEDEC();
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
                UPDATE_VARIABLEDEC();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('defined variable updated successfully.'); ", true);

                GET_VARIABLEDEC();
                CLEAR_VARIABLEDEC();

                lbtnUpdateVariableDec.Visible = false;
                lbtnSubmitVariableDec.Visible = true;
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
                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "INSERT_SC_VARIABLES",
                VARIABLENAME: txtRuleVariableName.Text,
                MODULEID: ddlModule.SelectedValue,
                FIELDID: ddlField.SelectedValue,
                MODULENAME: ddlModule.SelectedItem.Text,
                FIELDNAME: ddlField.SelectedItem.Text,
                ENTEREDBY: Session["USER_ID"].ToString()
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
                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "UPDATE_SC_VARIABLES",
                   VARIABLENAME: txtRuleVariableName.Text,
                   MODULEID: ddlModule.SelectedValue,
                   FIELDID: ddlField.SelectedValue,
                   MODULENAME: ddlModule.SelectedItem.Text,
                   FIELDNAME: ddlField.SelectedItem.Text,
                   ID: ViewState["ID"].ToString(),
                   ENTEREDBY: Session["USER_ID"].ToString()
                   );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvVariableDeclare_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();

                ViewState["ID"] = ID;
                if (e.CommandName == "EDITVAR")
                {
                    EDIT_VARIABLE(ID);
                }
                else if (e.CommandName == "DELETEVAR")
                {
                    DELETE_VARIABLE(ID);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('defined variable deleted successfully.'); ", true);
                    GET_VARIABLEDEC();
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
                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "SELECT_SC_VARIABLES", ID: ID);

                txtRuleVariableName.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();
                Bind_Module();
                ddlModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();
                Bind_Field();
                ddlField.SelectedValue = ds.Tables[0].Rows[0]["FIELDID"].ToString();

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
                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "DELETE_SC_VARIABLES", ENTEREDBY: Session["USER_ID"].ToString(), ID: ID);

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
                DataSet ds = dal_IWRS.NIWRS_STOP_CLAUSE_SP(ACTION: "GET_ADD_OnSubmitCRIT_FIELDS", ID: Request.QueryString["ID"].ToString());

                BIND_FIELDS(drpLISTField1, ds);
                BIND_FIELDS(drpLISTField2, ds);
                BIND_FIELDS(drpLISTField3, ds);
                BIND_FIELDS(drpLISTField4, ds);
                BIND_FIELDS(drpLISTField5, ds);
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
                ddl.Items.Insert(0, new ListItem("--Select--", "0"));
                ddl.Items.Insert(1, new ListItem("Site Id", "SITEID"));
                ddl.Items.Insert(2, new ListItem("Treatment Arm", "TREAT_GRP"));
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
                DataSet ds = new DataSet();
                string Values = "";

                if (ddl.SelectedValue == "SITEID")
                {
                    ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "GETSITES");

                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["VALUE"].ToString() + ",";
                        }
                    }
                }
                else if (ddl.SelectedValue == "TREAT_GRP")
                {
                    ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "GET_Treatment_Arm");

                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["VALUE"].ToString() + ",";
                        }
                    }
                }
                else
                {
                    ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_FORM_SPEC_FIELD_ANS", VARIABLENAME: ddl.SelectedValue);

                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["VALUE"].ToString() + ",";
                        }
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

        private void INSERT_CRIT()
        {
            try
            {
                string
                MODULEID = ddlSCModule.SelectedValue,
                FIELDNAME1 = drpLISTField1.SelectedValue,
                FIELDNAME2 = drpLISTField2.SelectedValue,
                FIELDNAME3 = drpLISTField3.SelectedValue,
                FIELDNAME4 = drpLISTField4.SelectedValue,
                FIELDNAME5 = drpLISTField5.SelectedValue,
                CritName = txtName.Text,
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
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
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
                else if (Condition1.Contains(">") || Condition1.Contains("<"))
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " " + Value1 + " " + AndOr1 + " ";
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
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
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
                    else if (Condition2.Contains(">") || Condition2.Contains("<"))
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " " + Value2 + " " + AndOr2 + " ";
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
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
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
                        else if (Condition3.Contains(">") || Condition3.Contains("<"))
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " " + Value3 + " " + AndOr3 + " ";
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
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
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
                            else if (Condition4.Contains(">") || Condition4.Contains("<"))
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " " + Value4 + " " + AndOr4 + " ";
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
                                else if (Condition5.Contains(">") || Condition5.Contains("<"))
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " " + Value5 + " ";
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

                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(
                                ACTION: "INSERT_SC_CRIT",

                                MODULEID: MODULEID,

                                MSGBOX: txtMsg.Text,
                                LIMIT: txtLimit.Text,

                                BEFORE: chkBefore.Checked,
                                AFTER: chkAfter.Checked,

                                NAVTO_TYPE: ddlNavType.SelectedValue,
                                NAVTO: ddlNavTo.SelectedValue,

                                CritName: CritName,
                                Criteria: CritQUERY,
                                CritCode: CritCodeQUERY,

                                Field1: drpLISTField1.SelectedValue,
                                Condition1: drpLISTCondition1.SelectedValue,
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
                                ENTEREDBY: Session["USER_ID"].ToString(),
                                SITEID: ddlSITEID.SelectedValue
                                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void UPDATE_CRIT()
        {
            try
            {
                string
                MODULEID = ddlSCModule.SelectedValue,
                FIELDNAME1 = drpLISTField1.SelectedValue,
                FIELDNAME2 = drpLISTField2.SelectedValue,
                FIELDNAME3 = drpLISTField3.SelectedValue,
                FIELDNAME4 = drpLISTField4.SelectedValue,
                FIELDNAME5 = drpLISTField5.SelectedValue,
                CritName = txtName.Text,
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
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
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
                else if (Condition1.Contains(">") || Condition1.Contains("<"))
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " " + Value1 + " " + AndOr1 + " ";
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
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
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
                    else if (Condition2.Contains(">") || Condition2.Contains("<"))
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " " + Value2 + " " + AndOr2 + " ";
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
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
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
                        else if (Condition3.Contains(">") || Condition3.Contains("<"))
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " " + Value3 + " " + AndOr3 + " ";
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
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
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
                            else if (Condition4.Contains(">") || Condition4.Contains("<"))
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " " + Value4 + " " + AndOr4 + " ";
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
                                else if (Condition5.Contains(">") || Condition5.Contains("<"))
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " " + Value5 + " ";
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

                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(
                                ACTION: "UPDATE_SC_CRIT",
                                ID: ViewState["editFORMCritID"].ToString(),

                                MODULEID: MODULEID,

                                MSGBOX: txtMsg.Text,
                                LIMIT: txtLimit.Text,

                                NAVTO_TYPE: ddlNavType.SelectedValue,
                                NAVTO: ddlNavTo.SelectedValue,

                                BEFORE: chkBefore.Checked,
                                AFTER: chkAfter.Checked,

                                CritName: CritName,
                                Criteria: CritQUERY,
                                CritCode: CritCodeQUERY,

                                Field1: drpLISTField1.SelectedValue,
                                Condition1: drpLISTCondition1.SelectedValue,
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

                                SITEID: ddlSITEID.SelectedValue,
                                ENTEREDBY: Session["USER_ID"].ToString()
                                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void SELECT_CRIT(string ID)
        {
            try
            {
                btnsubmit.Visible = false;
                btnUpdate.Visible = true;

                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "SELECT_SC_CRIT", ID: ID);

                txtName.Text = ds.Tables[0].Rows[0]["CritName"].ToString();

                if (ds.Tables[0].Rows[0]["BEFORE"].ToString() == "True")
                {
                    chkBefore.Checked = true;
                }
                else
                {
                    chkBefore.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["AFTER"].ToString() == "True")
                {
                    chkAfter.Checked = true;
                }
                else
                {
                    chkAfter.Checked = false;
                }

                ddlSCModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();

                ddlSITEID.SelectedValue = ds.Tables[0].Rows[0]["SITEID"].ToString();

                txtMsg.Text = ds.Tables[0].Rows[0]["MSGBOX"].ToString();

                ddlNavType.SelectedValue = ds.Tables[0].Rows[0]["NAVTO_TYPE"].ToString();
                GET_NavSOURCE();
                ddlNavTo.SelectedValue = ds.Tables[0].Rows[0]["NAVTO"].ToString();

                txtLimit.Text = ds.Tables[0].Rows[0]["LIMIT"].ToString();

                drpLISTField1.SelectedValue = ds.Tables[0].Rows[0]["Field1"].ToString();
                drpLISTCondition1.SelectedValue = ds.Tables[0].Rows[0]["Condition1"].ToString();
                txtLISTValue1.Text = ds.Tables[0].Rows[0]["Value1"].ToString();
                drpLISTAndOr1.SelectedValue = ds.Tables[0].Rows[0]["AndOr1"].ToString();

                drpLISTField2.SelectedValue = ds.Tables[0].Rows[0]["Field2"].ToString();
                drpLISTCondition2.SelectedValue = ds.Tables[0].Rows[0]["Condition2"].ToString();
                txtLISTValue2.Text = ds.Tables[0].Rows[0]["Value2"].ToString();
                drpLISTAndOr2.SelectedValue = ds.Tables[0].Rows[0]["AndOr2"].ToString();

                drpLISTField3.SelectedValue = ds.Tables[0].Rows[0]["Field3"].ToString();
                drpLISTCondition3.SelectedValue = ds.Tables[0].Rows[0]["Condition3"].ToString();
                txtLISTValue3.Text = ds.Tables[0].Rows[0]["Value3"].ToString();
                drpLISTAndOr3.SelectedValue = ds.Tables[0].Rows[0]["AndOr3"].ToString();

                drpLISTField4.SelectedValue = ds.Tables[0].Rows[0]["Field4"].ToString();
                drpLISTCondition4.SelectedValue = ds.Tables[0].Rows[0]["Condition4"].ToString();
                txtLISTValue4.Text = ds.Tables[0].Rows[0]["Value4"].ToString();
                drpLISTAndOr4.SelectedValue = ds.Tables[0].Rows[0]["AndOr4"].ToString();

                drpLISTField5.SelectedValue = ds.Tables[0].Rows[0]["Field5"].ToString();
                drpLISTCondition5.SelectedValue = ds.Tables[0].Rows[0]["Condition5"].ToString();
                txtLISTValue5.Text = ds.Tables[0].Rows[0]["Value5"].ToString();

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

        private void DELETE_CRIT(string ID)
        {
            try
            {
                dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "DELETE_SC_CRIT", ENTEREDBY: Session["USER_ID"].ToString(), ID: ID);
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
                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "GET_SC_CRIT");
                grdStepCrits.DataSource = ds;
                grdStepCrits.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_CRIT()
        {
            try
            {
                btnsubmit.Visible = true;
                btnUpdate.Visible = false;

                Bind_SCModule();

                txtMsg.Text = "";
                txtLimit.Text = "";
                txtName.Text = "";

                chkBefore.Checked = false;
                chkAfter.Checked = false;

                ddlNavType.SelectedIndex = 0;
                GET_NavSOURCE();

                ddlSITEID.SelectedIndex = 0;

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

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_CRIT();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Stop Clause defined successfully.'); ", true);
                GET_CRIT();
                CLEAR_CRIT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_CRIT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_CRIT();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('defined Stop Clause updated successfully.'); ", true);
                GET_CRIT();
                CLEAR_CRIT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void grdStepCrits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editFORMCritID"] = id;
                if (e.CommandName == "EditCrit")
                {
                    SELECT_CRIT(id);
                }
                else if (e.CommandName == "DeleteCrit")
                {
                    DELETE_CRIT(id);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('defined Stop Clause deleted successfully.'); ", true);
                    GET_CRIT();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_NavSOURCE()
        {
            try
            {
                DataSet ds = new DataSet();
                if (ddlNavType.SelectedValue == "Listing")
                {
                    ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_LISTING");
                    ddlNavTo.DataSource = ds.Tables[0];
                    ddlNavTo.DataValueField = "ID";
                    ddlNavTo.DataTextField = "LISTNAME";
                    ddlNavTo.DataBind();
                    ddlNavTo.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (ddlNavType.SelectedValue == "Form")
                {
                    ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_FORM");
                    ddlNavTo.DataSource = ds.Tables[0];
                    ddlNavTo.DataValueField = "ID";
                    ddlNavTo.DataTextField = "FORMNAME";
                    ddlNavTo.DataBind();
                    ddlNavTo.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlNavTo.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlNavType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_NavSOURCE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetSites()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                    USERID: Session["User_ID"].ToString()
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSITEID.DataSource = ds.Tables[0];
                    ddlSITEID.DataValueField = "INVNAME";
                    ddlSITEID.DataBind();
                    ddlSITEID.Items.Insert(0, new ListItem("None", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvVariableDeclare_PreRender(object sender, EventArgs e)
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                string lblHeader = "Stop Clauses";

                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "EXPORT_STOP_CLAUSE");

                //ds.Tables[0].Columns["Participant Id"].ColumnName = Session["SUBJECTTEXT"].ToString();

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                Multiple_Export_Excel.ToExcel(dsExport, lblHeader + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportPDF();
        }

        private void ExportPDF()
        {
            try
            {
                string lblHeader = "Stop Clauses";

                DataSet ds = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "EXPORT_STOP_CLAUSE");

                //ds.Tables[0].Columns["Participant Id"].ColumnName = Session["SUBJECTTEXT"].ToString();
                //{
                   
                //}

                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}