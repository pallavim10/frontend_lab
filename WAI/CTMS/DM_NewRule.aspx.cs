using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using PPT;

namespace CTMS
{
    public partial class DM_NewRule : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Bind_Indic();
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "callOpenCalcDiv();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Indic()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_INDICATION", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlIndication.DataSource = ds.Tables[0];
                    ddlIndication.DataValueField = "ID";
                    ddlIndication.DataTextField = "INDICATION";
                    ddlIndication.DataBind();
                    ddlIndication.Items.Insert(0, new ListItem("--Select--", "-1"));
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
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_VISIT_MASTER", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: ddlIndication.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVisit.DataSource = ds.Tables[0];
                    ddlVisit.DataValueField = "VISITNUM";
                    ddlVisit.DataTextField = "VISIT";
                    ddlVisit.DataBind();
                    ddlVisit.Items.Insert(0, new ListItem("--Select--", "-1"));
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

        public void Bind_Module()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_MODULE_DM_PROJECT_MASTER", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: ddlIndication.SelectedValue, VISITNUM: ddlVisit.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.DataSource = ds.Tables[0];
                    ddlModule.DataValueField = "MODULEID";
                    ddlModule.DataTextField = "MODULENAME";
                    ddlModule.DataBind();
                    ddlModule.Items.Insert(0, new ListItem("--Select--", "-1"));
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

        public void Bind_Field()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_FIELD_BYMODULEID", PROJECTID: Session["PROJECTID"].ToString(), MODULEID: ddlModule.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlField.DataSource = ds.Tables[0];
                    ddlField.DataTextField = "FIELDNAME";
                    ddlField.DataValueField = "ID";
                    ddlField.DataBind();
                    ddlField.Items.Insert(0, new ListItem("--Select--", "-1"));
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

        protected void ddlIndication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Visit();
                Bind_Module();
                Bind_Field();
                Bind_Calc_Visit();
                Bind_GV();
                ConcateRuleID();
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
                Bind_GV();
                ConcateRuleID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_GV();
                ConcateRuleID();
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
                Bind_GV();
                ConcateRuleID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_GV()
        {
            try
            {
                DataSet ds = dal.DM_RULE_SP(
                Action: "GET_RULE",
                Indication_ID: ddlIndication.SelectedValue,
                Visit_ID: ddlVisit.SelectedValue,
                Module_ID: ddlModule.SelectedValue,
                Field_ID: ddlField.SelectedValue
                    );

                gvRules.DataSource = ds.Tables[0];
                gvRules.DataBind();

                Session["RULE_COUNT"] = Convert.ToInt32(ds.Tables[1].Rows[0][0]) + 1;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Calc_Visit()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_VISIT_MASTER", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: ddlIndication.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVisit1.DataSource = ds.Tables[0];
                    ddlVisit1.DataValueField = "VISITNUM";
                    ddlVisit1.DataTextField = "VISIT";
                    ddlVisit1.DataBind();
                    ddlVisit1.Items.Insert(0, new ListItem("--Select--", "-1"));

                    ddlVisit2.DataSource = ds.Tables[0];
                    ddlVisit2.DataValueField = "VISITNUM";
                    ddlVisit2.DataTextField = "VISIT";
                    ddlVisit2.DataBind();
                    ddlVisit2.Items.Insert(0, new ListItem("--Select--", "-1"));

                    ddlVisit3.DataSource = ds.Tables[0];
                    ddlVisit3.DataValueField = "VISITNUM";
                    ddlVisit3.DataTextField = "VISIT";
                    ddlVisit3.DataBind();
                    ddlVisit3.Items.Insert(0, new ListItem("--Select--", "-1"));

                    ddlVisit4.DataSource = ds.Tables[0];
                    ddlVisit4.DataValueField = "VISITNUM";
                    ddlVisit4.DataTextField = "VISIT";
                    ddlVisit4.DataBind();
                    ddlVisit4.Items.Insert(0, new ListItem("--Select--", "-1"));

                    ddlVisit5.DataSource = ds.Tables[0];
                    ddlVisit5.DataValueField = "VISITNUM";
                    ddlVisit5.DataTextField = "VISIT";
                    ddlVisit5.DataBind();
                    ddlVisit5.Items.Insert(0, new ListItem("--Select--", "-1"));

                    ddlVisitFormula.DataSource = ds.Tables[0];
                    ddlVisitFormula.DataValueField = "VISITNUM";
                    ddlVisitFormula.DataTextField = "VISIT";
                    ddlVisitFormula.DataBind();
                    ddlVisitFormula.Items.Insert(0, new ListItem("--Select Visit--", "-1"));

                    ddlVisitFormula1.DataSource = ds.Tables[0];
                    ddlVisitFormula1.DataValueField = "VISITNUM";
                    ddlVisitFormula1.DataTextField = "VISIT";
                    ddlVisitFormula1.DataBind();
                    ddlVisitFormula1.Items.Insert(0, new ListItem("--Select Visit--", "-1"));

                    ddlVisitFormula2.DataSource = ds.Tables[0];
                    ddlVisitFormula2.DataValueField = "VISITNUM";
                    ddlVisitFormula2.DataTextField = "VISIT";
                    ddlVisitFormula2.DataBind();
                    ddlVisitFormula2.Items.Insert(0, new ListItem("--Select Visit--", "-1"));

                    ddlVisitFormula3.DataSource = ds.Tables[0];
                    ddlVisitFormula3.DataValueField = "VISITNUM";
                    ddlVisitFormula3.DataTextField = "VISIT";
                    ddlVisitFormula3.DataBind();
                    ddlVisitFormula3.Items.Insert(0, new ListItem("--Select Visit--", "-1"));

                    ddlVisitFormula4.DataSource = ds.Tables[0];
                    ddlVisitFormula4.DataValueField = "VISITNUM";
                    ddlVisitFormula4.DataTextField = "VISIT";
                    ddlVisitFormula4.DataBind();
                    ddlVisitFormula4.Items.Insert(0, new ListItem("--Select Visit--", "-1"));

                    ddlVisitFormula5.DataSource = ds.Tables[0];
                    ddlVisitFormula5.DataValueField = "VISITNUM";
                    ddlVisitFormula5.DataTextField = "VISIT";
                    ddlVisitFormula5.DataBind();
                    ddlVisitFormula5.Items.Insert(0, new ListItem("--Select Visit--", "-1"));
                }
                else
                {
                    ddlVisit1.Items.Clear();
                    ddlVisit2.Items.Clear();
                    ddlVisit3.Items.Clear();
                    ddlVisit4.Items.Clear();
                    ddlVisit5.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Calc_Module(DropDownList ddl, string Visit)
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_MODULE_DM_PROJECT_MASTER", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: ddlIndication.SelectedValue, VISITNUM: Visit);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds.Tables[0];
                    ddl.DataValueField = "MODULEID";
                    ddl.DataTextField = "MODULENAME";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--Select--", "-1"));
                }
                else
                {
                    ddl.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Calc_Field(DropDownList ddl, string Module)
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_FIELD_BYMODULEID", PROJECTID: Session["PROJECTID"].ToString(), MODULEID: Module);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds.Tables[0];
                    ddl.DataTextField = "FIELDNAME";
                    ddl.DataValueField = "ID";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--Select--", "-1"));
                }
                else
                {
                    ddl.Items.Clear();
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
                Bind_Calc_Module(ddlModule1, ddlVisit1.SelectedValue);
                Bind_Calc_Field(ddlField1, ddlModule1.SelectedValue);
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
                Bind_Calc_Field(ddlField1, ddlModule1.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Module(ddlModule2, ddlVisit2.SelectedValue);
                Bind_Calc_Field(ddlField2, ddlModule2.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModule2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Field(ddlField2, ddlModule2.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Module(ddlModule3, ddlVisit3.SelectedValue);
                Bind_Calc_Field(ddlField3, ddlModule3.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModule3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Field(ddlField3, ddlModule3.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Module(ddlModule4, ddlVisit4.SelectedValue);
                Bind_Calc_Field(ddlField4, ddlModule4.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModule4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Field(ddlField4, ddlModule4.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisit5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Module(ddlModule5, ddlVisit5.SelectedValue);
                Bind_Calc_Field(ddlField5, ddlModule5.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModule5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Field(ddlField5, ddlModule5.SelectedValue);
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
                string Indication_ID = null, RULE_ID = null, Actions = null, Nature = null, Informational = null, All = null, Description = null, QueryText = null, SEQNO = null,
            Visit_ID = null, Module_ID = null, Field_ID = null, Value = null, Condition = null, Formula = null,
            Math_Symbol1 = null, Visit_ID1 = null, Module_ID1 = null, Field_ID1 = null, Value1 = null, Condition1 = null, Formula1 = null,
            Math_Symbol2 = null, Visit_ID2 = null, Module_ID2 = null, Field_ID2 = null, Value2 = null, Condition2 = null, Formula2 = null,
            Math_Symbol3 = null, Visit_ID3 = null, Module_ID3 = null, Field_ID3 = null, Value3 = null, Condition3 = null, Formula3 = null,
            Math_Symbol4 = null, Visit_ID4 = null, Module_ID4 = null, Field_ID4 = null, Value4 = null, Condition4 = null, Formula4 = null,
            Math_Symbol5 = null, Visit_ID5 = null, Module_ID5 = null, Field_ID5 = null, Value5 = null, Condition5 = null, Formula5 = null;

                Indication_ID = ddlIndication.SelectedValue;
                RULE_ID = txtRuleID.Text;
                Actions = ddlAction.SelectedValue;
                Nature = ddlNature.SelectedValue;
                Informational = chkInformational.Checked.ToString();
                All = chkAll.Checked.ToString();

                Visit_ID = ddlVisit.SelectedValue;
                Module_ID = ddlModule.SelectedValue;
                Field_ID = ddlField.SelectedValue;
                Description = txtDescription.Text;
                QueryText = txtQuery.Text;
                SEQNO = txtSEQNO.Text;
                Value = txtValue.Text;
                Condition = ddlCondition.SelectedValue;
                Formula = hfFormula.Value;

                if (ddlSymbol1.SelectedIndex != 0)
                {
                    Math_Symbol1 = ddlSymbol1.SelectedValue;
                }
                Visit_ID1 = ddlVisit1.SelectedValue;
                Module_ID1 = ddlModule1.SelectedValue;
                Field_ID1 = ddlField1.SelectedValue;
                Value1 = txtValue1.Text;
                Condition1 = ddlCondition1.SelectedValue;
                Formula1 = hfFormula1.Value;

                if (ddlSymbol2.SelectedIndex != 0)
                {
                    Math_Symbol2 = ddlSymbol2.SelectedValue;
                    Visit_ID2 = ddlVisit2.SelectedValue;
                    Module_ID2 = ddlModule2.SelectedValue;
                    Field_ID2 = ddlField2.SelectedValue;
                    Value2 = txtValue2.Text;
                    Condition2 = ddlCondition2.SelectedValue;
                    Formula2 = hfFormula2.Value;
                }

                if (ddlSymbol3.SelectedIndex != 0)
                {
                    Math_Symbol3 = ddlSymbol3.SelectedValue;
                    Visit_ID3 = ddlVisit3.SelectedValue;
                    Module_ID3 = ddlModule3.SelectedValue;
                    Field_ID3 = ddlField3.SelectedValue;
                    Value3 = txtValue3.Text;
                    Condition3 = ddlCondition3.SelectedValue;
                    Formula3 = hfFormula3.Value;
                }

                if (ddlSymbol4.SelectedIndex != 0)
                {
                    Math_Symbol4 = ddlSymbol4.SelectedValue;
                    Visit_ID4 = ddlVisit4.SelectedValue;
                    Module_ID4 = ddlModule4.SelectedValue;
                    Field_ID4 = ddlField4.SelectedValue;
                    Value4 = txtValue4.Text;
                    Condition4 = ddlCondition4.SelectedValue;
                    Formula4 = hfFormula4.Value;
                }

                if (ddlSymbol5.SelectedIndex != 0)
                {
                    Math_Symbol5 = ddlSymbol5.SelectedValue;
                    Visit_ID5 = ddlVisit5.SelectedValue;
                    Module_ID5 = ddlModule5.SelectedValue;
                    Field_ID5 = ddlField5.SelectedValue;
                    Value5 = txtValue5.Text;
                    Condition5 = ddlCondition5.SelectedValue;
                    Formula5 = hfFormula5.Value;
                }

                //dal.DM_RULE_SP(
                //Action: "INSERT_RULE",
                //Project_ID: Session["PROJECTID"].ToString(),
                //Indication_ID: Indication_ID,
                //RULE_ID: RULE_ID,
                //Actions: Actions,
                //Nature: Nature,
                //Informational: Informational,
                //All: All,

                //Visit_ID: Visit_ID,
                //Module_ID: Module_ID,
                //Field_ID: Field_ID,
                //Description: Description,
                //QueryText: QueryText,
                //SEQNO: SEQNO,
                //Value: Value,
                //Condition: Condition,
                //Formula: Formula,

                //Math_Symbol1: Math_Symbol1,
                //Visit_ID1: Visit_ID1,
                //Module_ID1: Module_ID1,
                //Field_ID1: Field_ID1,
                //Condition1: Condition1,
                //Formula1: Formula1,
                //Value1: Value1,

                //Math_Symbol2: Math_Symbol2,
                //Visit_ID2: Visit_ID2,
                //Module_ID2: Module_ID2,
                //Field_ID2: Field_ID2,
                //Condition2: Condition2,
                //Formula2: Formula2,
                //Value2: Value2,

                //Math_Symbol3: Math_Symbol3,
                //Visit_ID3: Visit_ID3,
                //Module_ID3: Module_ID3,
                //Field_ID3: Field_ID3,
                //Condition3: Condition3,
                //Formula3: Formula3,
                //Value3: Value3,

                //Math_Symbol4: Math_Symbol4,
                //Visit_ID4: Visit_ID4,
                //Module_ID4: Module_ID4,
                //Field_ID4: Field_ID4,
                //Condition4: Condition4,
                //Formula4: Formula4,
                //Value4: Value4,

                //Math_Symbol5: Math_Symbol5,
                //Visit_ID5: Visit_ID5,
                //Module_ID5: Module_ID5,
                //Field_ID5: Field_ID5,
                //Condition5: Condition5,
                //Formula5: Formula5,
                //Value5: Value5
                    //);
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
                string Indication_ID = null, RULE_ID = null, Actions = null, Nature = null, Informational = null, All = null, Description = null, QueryText = null,
            Visit_ID = null, Module_ID = null, Field_ID = null, Value = null, Condition = null, Formula = null, SEQNO = null,
            Math_Symbol1 = null, Visit_ID1 = null, Module_ID1 = null, Field_ID1 = null, Value1 = null, Condition1 = null, Formula1 = null,
            Math_Symbol2 = null, Visit_ID2 = null, Module_ID2 = null, Field_ID2 = null, Value2 = null, Condition2 = null, Formula2 = null,
            Math_Symbol3 = null, Visit_ID3 = null, Module_ID3 = null, Field_ID3 = null, Value3 = null, Condition3 = null, Formula3 = null,
            Math_Symbol4 = null, Visit_ID4 = null, Module_ID4 = null, Field_ID4 = null, Value4 = null, Condition4 = null, Formula4 = null,
            Math_Symbol5 = null, Visit_ID5 = null, Module_ID5 = null, Field_ID5 = null, Value5 = null, Condition5 = null, Formula5 = null;

                Indication_ID = ddlIndication.SelectedValue;
                RULE_ID = txtRuleID.Text;
                Actions = ddlAction.SelectedValue;
                Nature = ddlNature.SelectedValue;
                Informational = chkInformational.Checked.ToString();
                All = chkAll.Checked.ToString();

                Visit_ID = ddlVisit.SelectedValue;
                Module_ID = ddlModule.SelectedValue;
                Field_ID = ddlField.SelectedValue;
                Description = txtDescription.Text;
                QueryText = txtQuery.Text;
                SEQNO = txtSEQNO.Text;
                Value = txtValue.Text;
                Condition = ddlCondition.SelectedValue;
                Formula = hfFormula.Value;

                if (ddlSymbol1.SelectedIndex != 0)
                {
                    Math_Symbol1 = ddlSymbol1.SelectedValue;
                }
                Visit_ID1 = ddlVisit1.SelectedValue;
                Module_ID1 = ddlModule1.SelectedValue;
                Field_ID1 = ddlField1.SelectedValue;
                Value1 = txtValue1.Text;
                Condition1 = ddlCondition1.SelectedValue;
                Formula1 = hfFormula1.Value;

                if (ddlSymbol2.SelectedIndex != 0)
                {
                    Math_Symbol2 = ddlSymbol2.SelectedValue;
                    Visit_ID2 = ddlVisit2.SelectedValue;
                    Module_ID2 = ddlModule2.SelectedValue;
                    Field_ID2 = ddlField2.SelectedValue;
                    Value2 = txtValue2.Text;
                    Condition2 = ddlCondition2.SelectedValue;
                    Formula2 = hfFormula2.Value;
                }

                if (ddlSymbol3.SelectedIndex != 0)
                {
                    Math_Symbol3 = ddlSymbol3.SelectedValue;
                    Visit_ID3 = ddlVisit3.SelectedValue;
                    Module_ID3 = ddlModule3.SelectedValue;
                    Field_ID3 = ddlField3.SelectedValue;
                    Value3 = txtValue3.Text;
                    Condition3 = ddlCondition3.SelectedValue;
                    Formula3 = hfFormula3.Value;
                }

                if (ddlSymbol4.SelectedIndex != 0)
                {
                    Math_Symbol4 = ddlSymbol4.SelectedValue;
                    Visit_ID4 = ddlVisit4.SelectedValue;
                    Module_ID4 = ddlModule4.SelectedValue;
                    Field_ID4 = ddlField4.SelectedValue;
                    Value4 = txtValue4.Text;
                    Condition4 = ddlCondition4.SelectedValue;
                    Formula4 = hfFormula4.Value;
                }

                if (ddlSymbol5.SelectedIndex != 0)
                {
                    Math_Symbol5 = ddlSymbol5.SelectedValue;
                    Visit_ID5 = ddlVisit5.SelectedValue;
                    Module_ID5 = ddlModule5.SelectedValue;
                    Field_ID5 = ddlField5.SelectedValue;
                    Value5 = txtValue5.Text;
                    Condition5 = ddlCondition5.SelectedValue;
                    Formula5 = hfFormula5.Value;
                }

                //dal.DM_RULE_SP(
                //Action: "UPDATE_RULE",
                //ID: Session["RULE_ID"].ToString(),
                //Indication_ID: Indication_ID,
                //RULE_ID: RULE_ID,
                //Actions: Actions,
                //Nature: Nature,
                //Informational: Informational,
                //All: All,

                //Visit_ID: Visit_ID,
                //Module_ID: Module_ID,
                //Field_ID: Field_ID,
                //Description: Description,
                //QueryText: QueryText,
                //SEQNO: SEQNO,
                //Value: Value,
                //Condition: Condition,
                //Formula: Formula,

                //Math_Symbol1: Math_Symbol1,
                //Visit_ID1: Visit_ID1,
                //Module_ID1: Module_ID1,
                //Field_ID1: Field_ID1,
                //Condition1: Condition1,
                //Formula1: Formula1,
                //Value1: Value1,

                //Math_Symbol2: Math_Symbol2,
                //Visit_ID2: Visit_ID2,
                //Module_ID2: Module_ID2,
                //Field_ID2: Field_ID2,
                //Condition2: Condition2,
                //Formula2: Formula2,
                //Value2: Value2,

                //Math_Symbol3: Math_Symbol3,
                //Visit_ID3: Visit_ID3,
                //Module_ID3: Module_ID3,
                //Field_ID3: Field_ID3,
                //Condition3: Condition3,
                //Formula3: Formula3,
                //Value3: Value3,

                //Math_Symbol4: Math_Symbol4,
                //Visit_ID4: Visit_ID4,
                //Module_ID4: Module_ID4,
                //Field_ID4: Field_ID4,
                //Condition4: Condition4,
                //Formula4: Formula4,
                //Value4: Value4,

                //Math_Symbol5: Math_Symbol5,
                //Visit_ID5: Visit_ID5,
                //Module_ID5: Module_ID5,
                //Field_ID5: Field_ID5,
                //Condition5: Condition5,
                //Formula5: Formula5,
                //Value5: Value5
                //    );

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
                DataSet ds = dal.DM_RULE_SP(
                Action: "EDIT_RULE",
                ID: RULE_ID
                    );

                DataTable dt = ds.Tables[0];

                ddlField.SelectedValue = dt.Rows[0]["Field_ID"].ToString();
                txtRuleID.Text = dt.Rows[0]["RULE_ID"].ToString();
                ddlAction.SelectedValue = dt.Rows[0]["Action"].ToString();
                ddlNature.SelectedValue = dt.Rows[0]["Nature"].ToString();

                if (dt.Rows[0]["Informational"].ToString() == "True")
                {
                    chkInformational.Checked = true;
                }
                if (dt.Rows[0]["All"].ToString() == "True")
                {
                    chkAll.Checked = true;
                }

                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                txtQuery.Text = dt.Rows[0]["QueryText"].ToString();
                txtSEQNO.Text = dt.Rows[0]["SEQNO"].ToString();
                txtValue.Text = dt.Rows[0]["Value"].ToString();
                ddlCondition.SelectedValue = dt.Rows[0]["Condition"].ToString();
                hfFormula.Value = dt.Rows[0]["Formula"].ToString();
                txtFormula.Text = dt.Rows[0]["Formula"].ToString().Replace("︹", " ");
                lbtnAddFormula.ToolTip = dt.Rows[0]["Formula"].ToString().Replace("︹", " ");

                ddlSymbol1.SelectedValue = dt.Rows[0]["Math_Symbol1"].ToString();
                if (dt.Rows[0]["Visit_ID1"].ToString() != "" && dt.Rows[0]["Visit_ID1"].ToString() != "-1")
                {
                    ddlVisit1.SelectedValue = dt.Rows[0]["Visit_ID1"].ToString();
                    Bind_Calc_Module(ddlModule1, ddlVisit1.SelectedValue);
                    ddlModule1.SelectedValue = dt.Rows[0]["Module_ID1"].ToString();
                    Bind_Calc_Field(ddlField1, ddlModule1.SelectedValue);
                    ddlField1.SelectedValue = dt.Rows[0]["Field_ID1"].ToString();
                }
                ddlCondition1.SelectedValue = dt.Rows[0]["Condition1"].ToString();
                hfFormula1.Value = dt.Rows[0]["Formula1"].ToString();
                txtFormula1.Text = dt.Rows[0]["Formula1"].ToString().Replace("︹", " ");
                txtValue1.Text = dt.Rows[0]["Value1"].ToString();
                lbtnAddFormula1.ToolTip = dt.Rows[0]["Formula1"].ToString().Replace("︹", " ");

                ddlSymbol2.SelectedValue = dt.Rows[0]["Math_Symbol2"].ToString();
                if (dt.Rows[0]["Visit_ID2"].ToString() != "" && dt.Rows[0]["Visit_ID2"].ToString() != "-1")
                {
                    ddlVisit2.SelectedValue = dt.Rows[0]["Visit_ID2"].ToString();
                    Bind_Calc_Module(ddlModule2, ddlVisit2.SelectedValue);
                    ddlModule2.SelectedValue = dt.Rows[0]["Module_ID2"].ToString();
                    Bind_Calc_Field(ddlField2, ddlModule2.SelectedValue);
                    ddlField2.SelectedValue = dt.Rows[0]["Field_ID2"].ToString();
                }
                ddlCondition2.SelectedValue = dt.Rows[0]["Condition2"].ToString();
                hfFormula2.Value = dt.Rows[0]["Formula2"].ToString();
                txtFormula2.Text = dt.Rows[0]["Formula2"].ToString().Replace("︹", " ");
                txtValue2.Text = dt.Rows[0]["Value2"].ToString();
                lbtnAddFormula2.ToolTip = dt.Rows[0]["Formula2"].ToString().Replace("︹", " ");

                ddlSymbol3.SelectedValue = dt.Rows[0]["Math_Symbol3"].ToString();
                if (dt.Rows[0]["Visit_ID3"].ToString() != "" && dt.Rows[0]["Visit_ID3"].ToString() != "-1")
                {
                    ddlVisit3.SelectedValue = dt.Rows[0]["Visit_ID3"].ToString();
                    Bind_Calc_Module(ddlModule3, ddlVisit3.SelectedValue);
                    ddlModule3.SelectedValue = dt.Rows[0]["Module_ID3"].ToString();
                    Bind_Calc_Field(ddlField3, ddlModule3.SelectedValue);
                    ddlField3.SelectedValue = dt.Rows[0]["Field_ID3"].ToString();
                }
                ddlCondition3.SelectedValue = dt.Rows[0]["Condition3"].ToString();
                hfFormula3.Value = dt.Rows[0]["Formula3"].ToString();
                txtFormula3.Text = dt.Rows[0]["Formula3"].ToString().Replace("︹", " ");
                txtValue3.Text = dt.Rows[0]["Value3"].ToString();
                lbtnAddFormula3.ToolTip = dt.Rows[0]["Formula3"].ToString().Replace("︹", " ");

                ddlSymbol4.SelectedValue = dt.Rows[0]["Math_Symbol4"].ToString();
                if (dt.Rows[0]["Visit_ID4"].ToString() != "" && dt.Rows[0]["Visit_ID4"].ToString() != "-1")
                {
                    ddlVisit4.SelectedValue = dt.Rows[0]["Visit_ID4"].ToString();
                    Bind_Calc_Module(ddlModule4, ddlVisit4.SelectedValue);
                    ddlModule4.SelectedValue = dt.Rows[0]["Module_ID4"].ToString();
                    Bind_Calc_Field(ddlField4, ddlModule4.SelectedValue);
                    ddlField4.SelectedValue = dt.Rows[0]["Field_ID4"].ToString();
                }
                ddlCondition4.SelectedValue = dt.Rows[0]["Condition4"].ToString();
                hfFormula4.Value = dt.Rows[0]["Formula4"].ToString();
                txtFormula4.Text = dt.Rows[0]["Formula4"].ToString().Replace("︹", " ");
                txtValue4.Text = dt.Rows[0]["Value4"].ToString();
                lbtnAddFormula4.ToolTip = dt.Rows[0]["Formula4"].ToString().Replace("︹", " ");

                ddlSymbol5.SelectedValue = dt.Rows[0]["Math_Symbol5"].ToString();
                if (dt.Rows[0]["Visit_ID5"].ToString() != "" && dt.Rows[0]["Visit_ID5"].ToString() != "-1")
                {
                    ddlVisit5.SelectedValue = dt.Rows[0]["Visit_ID5"].ToString();
                    Bind_Calc_Module(ddlModule5, ddlVisit5.SelectedValue);
                    ddlModule5.SelectedValue = dt.Rows[0]["Module_ID5"].ToString();
                    Bind_Calc_Field(ddlField5, ddlModule5.SelectedValue);
                    ddlField5.SelectedValue = dt.Rows[0]["Field_ID5"].ToString();
                }
                ddlCondition5.SelectedValue = dt.Rows[0]["Condition5"].ToString();
                hfFormula5.Value = dt.Rows[0]["Formula5"].ToString();
                txtFormula5.Text = dt.Rows[0]["Formula5"].ToString().Replace("︹", " ");
                txtValue5.Text = dt.Rows[0]["Value5"].ToString();
                lbtnAddFormula5.ToolTip = dt.Rows[0]["Formula5"].ToString().Replace("︹", " ");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DELETE_RULE(string RULE_ID)
        {
            try
            {
                dal.DM_RULE_SP(Action: "DELETE_RULE", ID: RULE_ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void CLEAR()
        {
            try
            {
                txtRuleID.Text = "";
                ddlAction.SelectedIndex = 0;
                ddlNature.SelectedIndex = 0;
                chkInformational.Checked = false;
                chkAll.Checked = false;
                txtDescription.Text = "";
                txtQuery.Text = "";
                txtSEQNO.Text = "";
                txtValue.Text = "";
                ddlCondition.SelectedIndex = 0;
                hfFormula.Value = "";
                txtFormula.Text = "";
                ddlFunctionFormula.SelectedIndex = 0;
                if (ddlVisitFormula.Items.Count > 0)
                {
                    ddlVisitFormula.SelectedIndex = 0;
                }
                if (ddlModuleFormula.Items.Count > 0)
                {
                    ddlModuleFormula.SelectedIndex = 0;
                }
                if (ddlFieldFormula.Items.Count > 0)
                {
                    ddlFieldFormula.SelectedIndex = 0;
                }
                lbtnAddFormula.ToolTip = "";

                ddlSymbol1.SelectedIndex = 0;
                if (ddlVisit1.Items.Count > 0)
                {
                    ddlVisit1.SelectedIndex = 0;
                }
                if (ddlModule1.Items.Count > 0)
                {
                    ddlModule1.SelectedIndex = 0;
                    ddlField1.SelectedIndex = 0;
                }
                ddlCondition1.SelectedIndex = 0;
                txtValue1.Text = "";
                hfFormula1.Value = "";
                txtFormula1.Text = "";
                ddlFunctionFormula1.SelectedIndex = 0;
                if (ddlVisitFormula1.Items.Count > 0)
                {
                    ddlVisitFormula1.SelectedIndex = 0;
                }
                if (ddlModuleFormula1.Items.Count > 0)
                {
                    ddlModuleFormula1.SelectedIndex = 0;
                }
                if (ddlFieldFormula1.Items.Count > 0)
                {
                    ddlFieldFormula1.SelectedIndex = 0;
                }
                lbtnAddFormula1.ToolTip = "";

                ddlSymbol2.SelectedIndex = 0;
                if (ddlVisit2.Items.Count > 0)
                {
                    ddlVisit2.SelectedIndex = 0;
                }
                if (ddlModule2.Items.Count > 0)
                {
                    ddlModule2.SelectedIndex = 0;
                    ddlField2.SelectedIndex = 0;
                }
                ddlCondition2.SelectedIndex = 0;
                hfFormula2.Value = "";
                txtValue2.Text = "";
                txtFormula2.Text = "";
                ddlFunctionFormula2.SelectedIndex = 0;
                if (ddlVisitFormula2.Items.Count > 0)
                {
                    ddlVisitFormula2.SelectedIndex = 0;
                }
                if (ddlModuleFormula2.Items.Count > 0)
                {
                    ddlModuleFormula2.SelectedIndex = 0;
                }
                if (ddlFieldFormula2.Items.Count > 0)
                {
                    ddlFieldFormula2.SelectedIndex = 0;
                }
                lbtnAddFormula2.ToolTip = "";

                ddlSymbol3.SelectedIndex = 0;
                if (ddlVisit3.Items.Count > 0)
                {
                    ddlVisit3.SelectedIndex = 0;
                }
                if (ddlModule3.Items.Count > 0)
                {
                    ddlModule3.SelectedIndex = 0;
                    ddlField3.SelectedIndex = 0;
                }
                ddlCondition3.SelectedIndex = 0;
                hfFormula3.Value = "";
                txtValue3.Text = "";
                txtFormula3.Text = "";
                ddlFunctionFormula3.SelectedIndex = 0;
                if (ddlVisitFormula3.Items.Count > 0)
                {
                    ddlVisitFormula3.SelectedIndex = 0;
                }
                if (ddlModuleFormula3.Items.Count > 0)
                {
                    ddlModuleFormula3.SelectedIndex = 0;
                }
                if (ddlFieldFormula3.Items.Count > 0)
                {
                    ddlFieldFormula3.SelectedIndex = 0;
                }
                lbtnAddFormula3.ToolTip = "";

                if (ddlVisit4.Items.Count > 0)
                {
                    ddlVisit4.SelectedIndex = 0;
                }
                if (ddlModule4.Items.Count > 0)
                {
                    ddlModule4.SelectedIndex = 0;
                    ddlField4.SelectedIndex = 0;
                }
                ddlCondition4.SelectedIndex = 0;
                hfFormula4.Value = "";
                txtValue4.Text = "";
                txtFormula4.Text = "";
                ddlFunctionFormula4.SelectedIndex = 0;
                if (ddlVisitFormula4.Items.Count > 0)
                {
                    ddlVisitFormula4.SelectedIndex = 0;
                }
                if (ddlModuleFormula4.Items.Count > 0)
                {
                    ddlModuleFormula4.SelectedIndex = 0;
                }
                if (ddlFieldFormula4.Items.Count > 0)
                {
                    ddlFieldFormula4.SelectedIndex = 0;
                }
                lbtnAddFormula4.ToolTip = "";

                ddlSymbol5.SelectedIndex = 0;
                if (ddlVisit5.Items.Count > 0)
                {
                    ddlVisit5.SelectedIndex = 0;
                }
                if (ddlModule5.Items.Count > 0)
                {
                    ddlModule5.SelectedIndex = 0;
                    ddlField5.SelectedIndex = 0;
                }
                ddlCondition5.SelectedIndex = 0;
                hfFormula5.Value = "";
                txtValue5.Text = "";
                txtFormula5.Text = "";
                ddlFunctionFormula5.SelectedIndex = 0;
                if (ddlVisitFormula5.Items.Count > 0)
                {
                    ddlVisitFormula5.SelectedIndex = 0;
                }
                if (ddlModuleFormula5.Items.Count > 0)
                {
                    ddlModuleFormula5.SelectedIndex = 0;
                }
                if (ddlFieldFormula5.Items.Count > 0)
                {
                    ddlFieldFormula5.SelectedIndex = 0;
                }
                lbtnAddFormula5.ToolTip = "";

                lbtnsubmit.Visible = true;
                lbtnUpdate.Visible = false;

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "callOpenCalcDiv();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void ConcateRuleID()
        {
            try
            {
                txtRuleID.Text = "i" + ddlIndication.SelectedValue + "_v" + ddlVisit.SelectedValue + "_m" + ddlModule.SelectedValue + "_f" + ddlField.SelectedValue + "-" + Session["RULE_COUNT"].ToString();
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
                Bind_GV();
                CLEAR();
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
                Bind_GV();
                CLEAR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvRules_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                if (e.CommandName == "EditRule")
                {
                    Session["RULE_ID"] = ID;
                    EDIT_RULE(ID);
                    lbtnsubmit.Visible = false;
                    lbtnUpdate.Visible = true;
                }
                else if (e.CommandName == "DeleteRule")
                {
                    DELETE_RULE(ID);
                    Bind_GV();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }


        //For Formula

        public void Bind_Calc_Module_FORMULA(DropDownList ddl, string Visit)
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_MODULE_DM_PROJECT_MASTER", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: ddlIndication.SelectedValue, VISITNUM: Visit);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds.Tables[0];
                    ddl.DataValueField = "ID_TABLE";
                    ddl.DataTextField = "MODULENAME";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--Select--", "-1"));
                }
                else
                {
                    ddl.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Bind_Calc_Field_FORMULA(DropDownList ddl, string Module)
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_FIELD_BYMODULEID", PROJECTID: Session["PROJECTID"].ToString(), MODULEID: GetUntilOrEmpty(Module));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds.Tables[0];
                    ddl.DataTextField = "FIELDNAME";
                    ddl.DataValueField = "VARIABLENAME";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--Select--", "-1"));
                }
                else
                {
                    ddl.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public string GetUntilOrEmpty(string text, string stopAt = "@")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }





        protected void lbtnFormula_Click(object sender, EventArgs e)
        {
            try
            {

                if (divFormula.Visible == true)
                {
                    divFormula.Visible = false;
                    lbtnFormula.ToolTip = txtFormula.Text;
                    hfFormula.Value = txtFormula.Text.Replace(" ", "︹");
                }
                else
                {
                    divFormula.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisitFormula_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Module_FORMULA(ddlModuleFormula, ddlVisitFormula.SelectedValue);
                Bind_Calc_Field_FORMULA(ddlFieldFormula, ddlModuleFormula.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModuleFormula_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Field_FORMULA(ddlFieldFormula, ddlModuleFormula.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddFormula_Click(object sender, EventArgs e)
        {
            try
            {
                string VMF = null, function = null;

                if (ddlFunctionFormula.SelectedIndex != 0)
                {
                    function = ddlFunctionFormula.SelectedValue;
                    txtFormula.Text = txtFormula.Text + " " + function;
                    hfFormula.Value = txtFormula.Text.Replace(" ", "︹");
                }
                else
                {

                    VMF = "@V-" + ddlVisitFormula.SelectedValue + "@M-" + ddlModuleFormula.SelectedValue.Split('@').Last() + "@F-" + ddlFieldFormula.SelectedValue + "@";

                    if (txtFormula.Text.Contains("<field>"))
                    {
                        Regex r = new Regex("<field>", RegexOptions.IgnoreCase);
                        txtFormula.Text = r.Replace(txtFormula.Text, VMF, 1);
                    }
                    else
                    {
                        txtFormula.Text = txtFormula.Text + " " + VMF;
                    }

                    hfFormula.Value = txtFormula.Text.Replace(" ", "︹");
                }

                ddlFunctionFormula.SelectedIndex = 0;

                if (ddlVisitFormula.Items.Count > 0)
                {
                    ddlVisitFormula.SelectedIndex = 0;
                }
                if (ddlModuleFormula.Items.Count > 0)
                {
                    ddlModuleFormula.SelectedIndex = 0;
                }
                if (ddlFieldFormula.Items.Count > 0)
                {
                    ddlFieldFormula.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void lbtnFormula1_Click(object sender, EventArgs e)
        {
            try
            {

                if (divFormula1.Visible == true)
                {
                    divFormula1.Visible = false;
                    lbtnFormula1.ToolTip = txtFormula1.Text;
                    hfFormula1.Value = txtFormula1.Text.Replace(" ", "︹");
                }
                else
                {
                    divFormula1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisitFormula1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Module_FORMULA(ddlModuleFormula1, ddlVisitFormula1.SelectedValue);
                Bind_Calc_Field_FORMULA(ddlFieldFormula1, ddlModuleFormula1.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModuleFormula1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Field_FORMULA(ddlFieldFormula1, ddlModuleFormula1.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddFormula1_Click(object sender, EventArgs e)
        {
            try
            {
                string VMF = null, function = null;

                if (ddlFunctionFormula1.SelectedIndex != 0)
                {
                    function = ddlFunctionFormula1.SelectedValue;
                    txtFormula1.Text = txtFormula1.Text + " " + function;
                    hfFormula1.Value = txtFormula1.Text.Replace(" ", "︹");
                }
                else
                {

                    VMF = "@V-" + ddlVisitFormula1.SelectedValue + "@M-" + ddlModuleFormula1.SelectedValue.Split('@').Last() + "@F-" + ddlFieldFormula1.SelectedValue + "@";
                    if (txtFormula1.Text.Contains("<field>"))
                    {
                        Regex r = new Regex("<field>", RegexOptions.IgnoreCase);
                        txtFormula1.Text = r.Replace(txtFormula1.Text, VMF, 1);
                    }
                    else
                    {
                        txtFormula1.Text = txtFormula1.Text + " " + VMF;
                    }
                    hfFormula1.Value = txtFormula1.Text.Replace(" ", "︹");
                }

                ddlFunctionFormula1.SelectedIndex = 0;
                if (ddlVisitFormula1.Items.Count > 0)
                {
                    ddlVisitFormula1.SelectedIndex = 0;
                }
                if (ddlModuleFormula1.Items.Count > 0)
                {
                    ddlModuleFormula1.SelectedIndex = 0;
                }
                if (ddlFieldFormula1.Items.Count > 0)
                {
                    ddlFieldFormula1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void lbtnFormula2_Click(object sender, EventArgs e)
        {
            try
            {

                if (divFormula2.Visible == true)
                {
                    divFormula2.Visible = false;
                    lbtnFormula2.ToolTip = txtFormula2.Text;
                    hfFormula2.Value = txtFormula2.Text.Replace(" ", "︹");
                }
                else
                {
                    divFormula2.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisitFormula2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Module_FORMULA(ddlModuleFormula2, ddlVisitFormula2.SelectedValue);
                Bind_Calc_Field_FORMULA(ddlFieldFormula2, ddlModuleFormula2.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModuleFormula2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Field_FORMULA(ddlFieldFormula2, ddlModuleFormula2.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddFormula2_Click(object sender, EventArgs e)
        {
            try
            {
                string VMF = null, function = null;

                if (ddlFunctionFormula2.SelectedIndex != 0)
                {
                    function = ddlFunctionFormula2.SelectedValue;
                    txtFormula2.Text = txtFormula2.Text + " " + function;
                    hfFormula2.Value = txtFormula2.Text.Replace(" ", "︹");
                }
                else
                {
                    VMF = "@V-" + ddlVisitFormula2.SelectedValue + "@M-" + ddlModuleFormula2.SelectedValue.Split('@').Last() + "@F-" + ddlFieldFormula2.SelectedValue + "@";
                    if (txtFormula2.Text.Contains("<field>"))
                    {
                        Regex r = new Regex("<field>", RegexOptions.IgnoreCase);
                        txtFormula2.Text = r.Replace(txtFormula2.Text, VMF, 1);
                    }
                    else
                    {
                        txtFormula2.Text = txtFormula2.Text + " " + VMF;
                    }
                    hfFormula2.Value = txtFormula2.Text.Replace(" ", "︹");
                }

                ddlFunctionFormula2.SelectedIndex = 0;

                if (ddlVisitFormula2.Items.Count > 0)
                {
                    ddlVisitFormula2.SelectedIndex = 0;
                }
                if (ddlModuleFormula2.Items.Count > 0)
                {
                    ddlModuleFormula2.SelectedIndex = 0;
                }
                if (ddlFieldFormula2.Items.Count > 0)
                {
                    ddlFieldFormula2.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void lbtnFormula3_Click(object sender, EventArgs e)
        {
            try
            {

                if (divFormula3.Visible == true)
                {
                    divFormula3.Visible = false;
                    lbtnFormula3.ToolTip = txtFormula3.Text;
                    hfFormula3.Value = txtFormula3.Text.Replace(" ", "︹");
                }
                else
                {
                    divFormula3.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisitFormula3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Module_FORMULA(ddlModuleFormula3, ddlVisitFormula3.SelectedValue);
                Bind_Calc_Field_FORMULA(ddlFieldFormula3, ddlModuleFormula3.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModuleFormula3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Field_FORMULA(ddlFieldFormula3, ddlModuleFormula3.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddFormula3_Click(object sender, EventArgs e)
        {
            try
            {
                string VMF = null, function = null;

                if (ddlFunctionFormula3.SelectedIndex != 0)
                {
                    function = ddlFunctionFormula3.SelectedValue;
                    txtFormula3.Text = txtFormula3.Text + " " + function;
                    hfFormula3.Value = txtFormula3.Text.Replace(" ", "︹");
                }
                else
                {
                    VMF = "@V-" + ddlVisitFormula3.SelectedValue + "@M-" + ddlModuleFormula3.SelectedValue.Split('@').Last() + "@F-" + ddlFieldFormula3.SelectedValue + "@";
                    if (txtFormula3.Text.Contains("<field>"))
                    {
                        Regex r = new Regex("<field>", RegexOptions.IgnoreCase);
                        txtFormula3.Text = r.Replace(txtFormula3.Text, VMF, 1);
                    }
                    else
                    {
                        txtFormula3.Text = txtFormula3.Text + " " + VMF;
                    }
                    hfFormula3.Value = txtFormula3.Text.Replace(" ", "︹");
                }

                ddlFunctionFormula3.SelectedIndex = 0;
                if (ddlVisitFormula3.Items.Count > 0)
                {
                    ddlVisitFormula3.SelectedIndex = 0;
                }
                if (ddlModuleFormula3.Items.Count > 0)
                {
                    ddlModuleFormula3.SelectedIndex = 0;
                }
                if (ddlFieldFormula3.Items.Count > 0)
                {
                    ddlFieldFormula3.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void lbtnFormula4_Click(object sender, EventArgs e)
        {
            try
            {

                if (divFormula4.Visible == true)
                {
                    divFormula4.Visible = false;
                    lbtnFormula4.ToolTip = txtFormula4.Text;
                    hfFormula4.Value = txtFormula4.Text.Replace(" ", "︹");
                }
                else
                {
                    divFormula4.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisitFormula4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Module_FORMULA(ddlModuleFormula4, ddlVisitFormula4.SelectedValue);
                Bind_Calc_Field_FORMULA(ddlFieldFormula4, ddlModuleFormula4.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModuleFormula4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Field_FORMULA(ddlFieldFormula4, ddlModuleFormula4.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddFormula4_Click(object sender, EventArgs e)
        {
            try
            {
                string VMF = null, function = null;

                if (ddlFunctionFormula4.SelectedIndex != 0)
                {
                    function = ddlFunctionFormula4.SelectedValue;
                    txtFormula4.Text = txtFormula4.Text + " " + function;
                    hfFormula4.Value = txtFormula4.Text.Replace(" ", "︹");
                }
                else
                {
                    VMF = "@V-" + ddlVisitFormula4.SelectedValue + "@M-" + ddlModuleFormula4.SelectedValue.Split('@').Last() + "@F-" + ddlFieldFormula4.SelectedValue + "@";
                    if (txtFormula4.Text.Contains("<field>"))
                    {
                        Regex r = new Regex("<field>", RegexOptions.IgnoreCase);
                        txtFormula4.Text = r.Replace(txtFormula4.Text, VMF, 1);
                    }
                    else
                    {
                        txtFormula4.Text = txtFormula4.Text + " " + VMF;
                    }
                    hfFormula4.Value = txtFormula4.Text.Replace(" ", "︹");
                }

                ddlFunctionFormula4.SelectedIndex = 0;
                if (ddlVisitFormula4.Items.Count > 0)
                {
                    ddlVisitFormula4.SelectedIndex = 0;
                }
                if (ddlModuleFormula4.Items.Count > 0)
                {
                    ddlModuleFormula4.SelectedIndex = 0;
                }
                if (ddlFieldFormula4.Items.Count > 0)
                {
                    ddlFieldFormula4.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void lbtnFormula5_Click(object sender, EventArgs e)
        {
            try
            {

                if (divFormula5.Visible == true)
                {
                    divFormula5.Visible = false;
                    lbtnFormula5.ToolTip = txtFormula5.Text;
                    hfFormula5.Value = txtFormula5.Text.Replace(" ", "︹");
                }
                else
                {
                    divFormula5.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisitFormula5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Module_FORMULA(ddlModuleFormula5, ddlVisitFormula5.SelectedValue);
                Bind_Calc_Field_FORMULA(ddlFieldFormula5, ddlModuleFormula5.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModuleFormula5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Calc_Field_FORMULA(ddlFieldFormula5, ddlModuleFormula5.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddFormula5_Click(object sender, EventArgs e)
        {
            try
            {
                string VMF = null, function = null;

                if (ddlFunctionFormula5.SelectedIndex != 0)
                {
                    function = ddlFunctionFormula5.SelectedValue;
                    txtFormula5.Text = txtFormula5.Text + " " + function;
                    hfFormula5.Value = txtFormula5.Text.Replace(" ", "︹");
                }
                else
                {
                    VMF = "@V-" + ddlVisitFormula5.SelectedValue + "@M-" + ddlModuleFormula5.SelectedValue.Split('@').Last() + "@F-" + ddlFieldFormula5.SelectedValue + "@";
                    if (txtFormula5.Text.Contains("<field>"))
                    {
                        Regex r = new Regex("<field>", RegexOptions.IgnoreCase);
                        txtFormula5.Text = r.Replace(txtFormula5.Text, VMF, 1);
                    }
                    else
                    {
                        txtFormula5.Text = txtFormula5.Text + " " + VMF;
                    }
                    hfFormula5.Value = txtFormula5.Text.Replace(" ", "︹");
                }

                ddlFunctionFormula5.SelectedIndex = 0;
                if (ddlVisitFormula5.Items.Count > 0)
                {
                    ddlVisitFormula5.SelectedIndex = 0;
                }
                if (ddlModuleFormula5.Items.Count > 0)
                {
                    ddlModuleFormula5.SelectedIndex = 0;
                }
                if (ddlFieldFormula5.Items.Count > 0)
                {
                    ddlFieldFormula5.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}