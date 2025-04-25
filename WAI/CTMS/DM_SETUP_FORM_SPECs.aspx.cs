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
    public partial class DM_SETUP_FORM_SPECs : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtERR_MSG.Attributes.Add("MaxLength", "500");
            txtERR_MSG.Attributes.Add("MaxLength", "2000");
            try
            {
                if (!IsPostBack)
                {
                    drpLISTField1.CssClass = "form-control required width200px";
                    drpLISTCondition1.CssClass = "form-control required width200px";                    
                    
                    hdnVariablename.Value = Request.QueryString["VARIABLENAME"].ToString();
                    Bind_Visit();
                    GET_ADD_CRIT_FIELDS();
                    GET_CRIT();
                    BIND_FORMULA_GRID();
                    if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv();", true);
                        grdStepCrits.Columns[11].Visible = false;
                    }
                    else
                    {
                        grdStepCrits.Columns[11].Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_CRIT()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_OnChange_CRIT",
                    FIELDID: Request.QueryString["FIELDID"].ToString());

                grdStepCrits.DataSource = ds;
                grdStepCrits.DataBind();

                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    lblList.Text = ds.Tables[1].Rows[0]["FIELDNAME"].ToString();
                    hdnFieldname.Value = ds.Tables[1].Rows[0]["FIELDNAME"].ToString();
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
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_ADD_OnSubmitCRIT_FIELDS",
                    MODULEID: Request.QueryString["MODULEID"].ToString()
                    );

                BIND_FIELDS(ddlFields, ds);
                BIND_FIELDS(drpLISTField1, ds);
                BIND_FIELDS(drpLISTField2, ds);
                BIND_FIELDS(drpLISTField3, ds);
                BIND_FIELDS(drpLISTField4, ds);
                BIND_FIELDS(drpLISTField5, ds);
                BIND_FORMULA();
                BIND_FORMULA_GRID();
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
                ddl.DataTextField = "FIELDNAME";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select Field--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void BIND_FORMULA()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_ADD_OnSubmitCRIT_FIELDS",
                   MODULEID: Request.QueryString["MODULEID"].ToString()
                   );
                ddlFormula.DataSource = ds.Tables[1];
                ddlFormula.DataValueField = "ID";
                ddlFormula.DataTextField = "Name";
                ddlFormula.DataBind();
                ddlFormula.Items.Insert(0, new ListItem("--Select Formula--", "0"));
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
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_FORM_SPEC_FIELD_ANS",
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

        public void Bind_Visit()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVISITID.DataSource = ds.Tables[0];
                    ddlVISITID.DataValueField = "VISITNUM";
                    ddlVISITID.DataTextField = "VISIT";
                    ddlVISITID.DataBind();
                    ddlVISITID.Items.Insert(0, new ListItem("--Select Visit--", "0"));

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

        private void INSERT_CRIT()
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
                    if (drpLISTField1.SelectedValue == "0")
                    {
                        CritCodeQUERY1 = "";
                        CritQUERY1 = "";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                        CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                    }
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

                string setvaluefield = string.Empty;
                if (ddlFields.SelectedValue != "")
                {
                    setvaluefield = ddlFields.SelectedValue;
                }

                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(
                    ACTION: "INSERT_OnChange_CRIT",
                    VISITNUM: ddlVISITID.SelectedValue,
                    SEQNO: SEQNO,
                    CritName: CritName,
                    Criteria: CritQUERY,
                    CritCode: CritCodeQUERY,
                    FIELDID: Request.QueryString["FIELDID"].ToString(),
                    FIELDNAME: hdnFieldname.Value,
                    VARIABLENAME: hdnVariablename.Value,
                    ISDERIVED: ChkIsDerived.Checked,
                    SETVALUE: chkSetValue.Checked,
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
                    Restricted: chkRestrict.Checked,
                   
                    SETFIELDID: setvaluefield,
                    SETVALUEDATA: txtSetValue.Text,
                    ISDERIVED_VALUE: TxtIsDerivedValue.Text,
                    MODULEID: Request.QueryString["MODULEID"].ToString()
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
                    if (drpLISTField1.SelectedValue == "0")
                    {
                        CritCodeQUERY1 = "";
                        CritQUERY1 = "";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                        CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                    }
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

                string setvaluefield = string.Empty;
                if (ddlFields.SelectedValue != "")
                {
                    setvaluefield = ddlFields.SelectedValue;
                }

                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(
                    ACTION: "UPDATE_OnChange_CRIT",
                    VISITNUM: ddlVISITID.SelectedValue,
                    SEQNO: SEQNO,
                    CritName: CritName,
                    Criteria: CritQUERY,
                    CritCode: CritCodeQUERY,
                    FIELDID: Request.QueryString["FIELDID"].ToString(),
                    FIELDNAME: hdnFieldname.Value,
                    VARIABLENAME: hdnVariablename.Value,

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
                    ID: ViewState["ID"].ToString(),

                    Restricted: chkRestrict.Checked,
                    SETVALUE: chkSetValue.Checked,
                    SETFIELDID: setvaluefield,
                    SETVALUEDATA: txtSetValue.Text,
                    ISDERIVED: ChkIsDerived.Checked,
                    ISDERIVED_VALUE: TxtIsDerivedValue.Text,
                    MODULEID: Request.QueryString["MODULEID"].ToString()
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

                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "SELECT_OnChange_CRIT", ID: ID);

                txtSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                ddlVISITID.SelectedValue = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                drpLISTField1.SelectedValue = ds.Tables[0].Rows[0]["Field1"].ToString();
                drpLISTCondition1.SelectedValue = ds.Tables[0].Rows[0]["CONDITION1"].ToString();
                txtLISTValue1.Text = ds.Tables[0].Rows[0]["VALUE1"].ToString();
                drpLISTAndOr1.SelectedValue = ds.Tables[0].Rows[0]["AND_OR1"].ToString();

                drpLISTField2.SelectedValue = ds.Tables[0].Rows[0]["Field2"].ToString();
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

                if (ds.Tables[0].Rows[0]["SETVALUE"].ToString() == "True")
                {
                    divrestrict.Visible = false;
                    divErrormsg.Visible = false;

                    divIsderived.Visible = true;
                    SETVALUEVISIBLE.Visible = true;

                    chkSetValue.Checked = true;
                    ddlFields.SelectedValue = ds.Tables[0].Rows[0]["SETFIELDID"].ToString();
                    txtSetValue.Text = ds.Tables[0].Rows[0]["SETVALUEDATA"].ToString();

                    if (ds.Tables[0].Rows[0]["ISDERIVED"].ToString() == "True")
                    {
                        divIsderived.Visible = true;
                        SETVALUEVISIBLE.Visible = true;
                        TxtIsDerivedValue.Visible = true;
                        txtSetValue.Visible = false;

                        ChkIsDerived.Checked = true;
                        txtSetValue.Visible = false;
                        ddlFormula.Visible = true;
                        lbtnFormulaAdd.Visible = true;
                        ddlFields.SelectedValue = ds.Tables[0].Rows[0]["SETFIELDID"].ToString();
                        TxtIsDerivedValue.Text = ds.Tables[0].Rows[0]["ISDERIVED_VALUE"].ToString();
                    }
                    else
                    {
                        ChkIsDerived.Checked = false;
                        ChkIsDerived.Visible = true;
                        TxtIsDerivedValue.Visible = false;
                        ddlFormula.Visible = false;
                        lbtnFormulaAdd.Visible = false;
                    }
                }
                else
                {
                    chkSetValue.Checked = false;
                    SETVALUEVISIBLE.Visible = false;
                    divIsderived.Visible = false;
                    divrestrict.Visible = true;
                    divErrormsg.Visible = true;
                    ddlFormula.Visible = false;
                    lbtnFormulaAdd.Visible = false;
                    divSetValue.Visible = false;
                }

                if (ds.Tables[0].Rows[0]["Restricted"].ToString() == "True")
                {
                    chkRestrict.Checked = true;

                    txtERR_MSG.Text = ds.Tables[0].Rows[0]["ERR_MSG"].ToString();
                }
                else
                {
                    chkRestrict.Checked = false;
                }

                txtERR_MSG.Text = ds.Tables[0].Rows[0]["ERR_MSG"].ToString();

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
                dal_DB.DB_SETUP_CRITs_SP(ACTION: "DELETE_OnChange_CRIT", ID: ID);

                Response.Write("<script> alert('OnChange criteria deleted successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
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

                Response.Write("<script> alert('OnChange criteria added successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
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
                Response.Redirect(Request.RawUrl);
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

                Response.Write("<script> alert('OnChange criteria updated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
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

                ViewState["ID"] = id;
                if (e.CommandName == "EditCrit")
                {
                    SELECT_CRIT(id);

                    if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv();", true);
                    }
                }
                else if (e.CommandName == "DeleteCrit")
                {
                    DELETE_CRIT(id);
                }
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

        protected void chkSetValue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSetValue.Checked == true)
                {
                    ddlFields.Visible = true;
                    txtSetValue.Visible = true;
                    txtSetValue.Text = "";
                    divErrormsg.Visible = false;
                    divrestrict.Visible = false;
                    chkRestrict.Checked = false;
                    SETVALUEVISIBLE.Visible = true;
                    divIsderived.Visible = true;
                    ChkIsDerived.Checked = false;
                    TxtIsDerivedValue.Visible = false;
                    ddlFields.SelectedIndex = 0;           
                    ddlFormula.Visible = false;
                    ddlFormula.SelectedIndex = 0;
                    lbtnFormulaAdd.Visible = false;

                    if (ChkIsDerived.Checked == true)
                    {
                        drpLISTField1.CssClass = "form-control required width200px";
                        drpLISTCondition1.CssClass = "form-control required width200px";
                    }
                    else
                    {
                        drpLISTField1.CssClass = "form-control width200px";
                        drpLISTCondition1.CssClass = "form-control width200px";
                    }


                }
                else
                {
                    ddlFields.Visible = false;
                    txtSetValue.Visible = false;
                    divErrormsg.Visible = true;
                    divrestrict.Visible = true;
                    chkRestrict.Checked = false;
                    SETVALUEVISIBLE.Visible = false;
                    divIsderived.Visible = false;
                    ChkIsDerived.Checked = false;
                    TxtIsDerivedValue.Visible = false;
                    TxtIsDerivedValue.Text = "";
                    txtSetValue.Text = "";
                    ddlFields.SelectedIndex = 0;
                    //drpLISTField1.CssClass = "form-control required width200px";
                    //drpLISTCondition1.CssClass = "form-control required width200px";

                   

                    ddlFormula.Visible = false;
                    ddlFormula.SelectedIndex = 0;
                    lbtnFormulaAdd.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(ddlFields, hfValueSetValue);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ChkIsDerived_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkIsDerived.Checked == true)
                {
                    ddlFields.Visible = true;
                    txtSetValue.Visible = false;
                    TxtIsDerivedValue.Visible = true;

                    TxtIsDerivedValue.Text = "";
                    SETVALUEVISIBLE.Visible = true;
                    chkSetValue.Checked = true;
                    txtSetValue.Text = "";

                    ddlFields.SelectedIndex = 0;
                    ddlFormula.Visible = true;
                    ddlFormula.SelectedIndex = 0;
                    lbtnFormulaAdd.Visible = true;

                    if(chkSetValue.Checked == true)
                    {
                          drpLISTField1.CssClass = "form-control required width200px";
                          drpLISTCondition1.CssClass = "form-control required width200px";  
                    }
                              

                }
                else
                {
                    ddlFields.Visible = true;
                    txtSetValue.Visible = true;
                    SETVALUEVISIBLE.Visible = true;
                    divIsderived.Visible = true;
                    TxtIsDerivedValue.Visible = false;
                    TxtIsDerivedValue.Text = "";
                    txtSetValue.Text = "";
                    ddlFields.SelectedIndex = 0;


                    ddlFormula.Visible = false;
                    ddlFormula.SelectedIndex = 0;
                    lbtnFormulaAdd.Visible = false;

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
                    drpLISTField1.CssClass = "form-control width200px";
                    drpLISTCondition1.CssClass = "form-control width200px";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void txtSetValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BIND_OPTIONS(ddlFields, hfValueSetValue);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);

                if (txtSetValue.Text != "")
                {
                    txtERR_MSG.Enabled = false;
                    txtERR_MSG.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkRestrict_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRestrict.Checked == true)
                {
                    divSetValue.Visible = false;
                    divIsderived.Visible = false;
                }
                else
                {
                    divSetValue.Visible = true;
                    ChkIsDerived.Checked = false;
                    TxtIsDerivedValue.Text = "";
                    ddlFields.SelectedIndex = 0;
                    ddlFormula.SelectedIndex = 0;
                    txtSetValue.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdStepCrits_PreRender(object sender, EventArgs e)
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
        protected void grdViewFormula_PreRender(object sender, EventArgs e)
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

        protected void ddlFormula_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get Formula from database based on selected name in dropdown
            DataTable dt = dal_DB.DB_Formula(ACTION: "GET_FORMULAS", ID: ddlFormula.SelectedValue.ToString()
                  );

            // Check if DataTable has rows
            if (dt != null && dt.Rows.Count > 0)
            {
                // Assign values from the DataTable to TextBox controls
                TxtIsDerivedValue.Text = dt.Rows[0]["Formula"].ToString(); // Feel Database value in Textbox
            }
            else
            {
                // Handle the case when no data is returned
                TxtIsDerivedValue.Text = string.Empty;
            }
        }
        private void BIND_CUSTOM_FORMULA(string formulatype, string formulaval)
        {
            try
            {

                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "Insert_CustomFormula", Name: formulatype,
                    Formula: formulaval);
                BIND_FORMULA();
                BIND_FORMULA_GRID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void BIND_FORMULA_GRID() 
        {
            try 
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_FORMULAS_DATA");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdViewFormula.DataSource = ds.Tables[0];
                    grdViewFormula.DataBind();
                }
                else 
                {
                    grdViewFormula.DataSource = null;
                    grdViewFormula.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnfrmSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_formulatype.Text != "" && txt_formulatype.Text != "undefined" && txt_formulatype.Text != null && txt_formulaval.Text != "" && txt_formulaval.Text != "undefined" && txt_formulaval.Text != null)
                {
                    BIND_CUSTOM_FORMULA(txt_formulatype.Text.Trim(), txt_formulaval.Text.Trim());
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Formula added successfully.');", true);
                    CLEAR_Fields();
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Please enter valid formula details.');", true);
                    ModalPopupExtender2.Show();
                    
                }
                // Response.Write("<script> alert('OnChange criteria added successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
                ModalPopupExtender2.Show();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        protected void btnfrmUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_FORMULA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        protected void ClosePopupFormula(object sender, EventArgs e) 
        {
            try
            {
                BIND_FORMULA();
                CLEAR_Fields();
                ModalPopupExtender2.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        protected void btnfrmCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_Fields();
                BIND_FORMULA();
                BIND_FORMULA_GRID();
                ModalPopupExtender2.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        private void CLEAR_Fields() 
        {
            txt_formulatype.Text = string.Empty;
            txt_formulaval.Text = string.Empty;
            TxtIsDerivedValue.Text = string.Empty;
            ddlFields.SelectedIndex = 0;
            txtSetValue.Text = "";
            btnfrmsubmit.Visible = true;
            btnfrmupdate.Visible = false;
          
        }

        protected void grdViewFormula_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["FORMULAID"] = id;
                if (e.CommandName == "EditFormula")
                {
                    SELECT_FORMULA(id);

                    if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv();", true);
                    }
                    
                }
                else if (e.CommandName == "DeleteFormula")
                {
                    DELETE_FORMULA(id);
                    ModalPopupExtender2.Show();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_FORMULA()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(
                       ACTION: "UPDATE_FORMULA", Name: txt_formulatype.Text.Trim(), Formula: txt_formulaval.Text.Trim(), ID: ViewState["FORMULAID"].ToString());
                BIND_FORMULA();
                BIND_FORMULA_GRID();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Formula updated successfully.');", true);
                ModalPopupExtender2.Show();
                CLEAR_Fields();
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_FORMULA(string ID)
        {
            try
            {
                btnfrmsubmit.Visible = false;
                btnfrmupdate.Visible = true;
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "SELECT_FORMULA_BYID", ID: ID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txt_formulatype.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txt_formulaval.Text = ds.Tables[0].Rows[0]["Formula"].ToString();
                }
                ModalPopupExtender2.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void DELETE_FORMULA(string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "DELETE_FORMULA",  ID: ID);
                BIND_FORMULA();
                BIND_FORMULA_GRID();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Formula deleted successfully.')", true);
                ModalPopupExtender2.Show();
                CLEAR_Fields();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}