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
    public partial class Manage_Criteria : System.Web.UI.Page
    {
        DAL_SETUP Dal_Setup = new DAL_SETUP();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_FIELDS();
                    GET_DEFINE_CRIT();
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }

        private void GET_FIELDS()
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_FIELDS");

                BIND_FIELDS(drpLISTField1, ds);
                BIND_FIELDS(drpLISTField2, ds);
                BIND_FIELDS(drpLISTField3, ds);
                BIND_FIELDS(drpLISTField4, ds);
                BIND_FIELDS(drpLISTField5, ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void BIND_OPTIONS(DropDownList ddl, HiddenField hf)
        {
            try
            {
                DataSet ds = new DataSet();
                if (ddl.SelectedValue.Trim() == "VISIT")
                {
                    //BIND VISIT Name
                    ds = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");
                    string Values = "";
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["VISITNAME"].ToString() + ",";
                        }
                    }

                    hf.Value = Values.TrimEnd(',');
                }
                else
                {

                    ds = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_FIELDS_OPTION", VARIABLENAME: ddl.SelectedValue.Trim());

                    string Values = "";
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["OPTION_VALUE"].ToString() + ",";
                        }
                    }
                    hf.Value = Values.TrimEnd(',');
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
                ddl.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            catch (Exception ex)
            {
                ex.ToString();
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
                ex.Message.ToString();
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
                ex.Message.ToString();
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
                ex.Message.ToString();
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
                ex.Message.ToString();
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
                ex.Message.ToString();
            }

        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            INSERT_CRIT();
            GET_DEFINE_CRIT();
            CLEAR_Field();

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Define Criteria Inserted Successfully.', 'success');", true);
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
                Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
                Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
                Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
                CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null,
                CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;


                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;

                if (FIELDNAME1 == "VISIT")
                {
                    DataSet dsStatus = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");

                    if (dsStatus.Tables[0].Rows.Count > 0)
                    {
                        Value1 = dsStatus.Tables[0].Rows[0]["VISITNAME"].ToString();
                    }
                    else
                    {
                        Value1 = txtLISTValue1.Text;
                    }
                }

                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    AndOr1 = drpLISTAndOr1.SelectedItem.Text;
                }

                if (Condition1 == "IS NULL")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "[_]%")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "![_]%")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] NOT LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "%_%")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "!%_%")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }

                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    Condition2 = drpLISTCondition2.SelectedValue;
                    Value2 = txtLISTValue2.Text;

                    if (FIELDNAME2 == "VISIT")
                    {
                        DataSet dsStatus = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");

                        if (dsStatus.Tables[0].Rows.Count > 0)
                        {
                            Value2 = dsStatus.Tables[0].Rows[0]["VISITNAME"].ToString();
                        }
                        else
                        {
                            Value2 = txtLISTValue1.Text;
                        }
                    }

                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        AndOr2 = drpLISTAndOr2.SelectedItem.Text;
                    }

                    if (Condition2 == "IS NULL")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "[_]%")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "![_]%")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] NOT LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "%_%")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "!%_%")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }

                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        Condition3 = drpLISTCondition3.SelectedValue;
                        Value3 = txtLISTValue3.Text;

                        if (FIELDNAME3 == "VISIT")
                        {
                            DataSet dsStatus = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");

                            if (dsStatus.Tables[0].Rows.Count > 0)
                            {
                                Value3 = dsStatus.Tables[0].Rows[0]["VISITNAME"].ToString();
                            }
                            else
                            {
                                Value3 = txtLISTValue1.Text;
                            }
                        }

                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            AndOr3 = drpLISTAndOr3.SelectedItem.Text;
                        }

                        if (Condition3 == "IS NULL")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "[_]%")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "![_]%")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] NOT LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "%_%")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "!%_%")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }


                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            Condition4 = drpLISTCondition4.SelectedValue;
                            Value4 = txtLISTValue4.Text;

                            if (FIELDNAME4 == "VISIT")
                            {
                                DataSet dsStatus = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");

                                if (dsStatus.Tables[0].Rows.Count > 0)
                                {
                                    Value4 = dsStatus.Tables[0].Rows[0]["VISITNAME"].ToString();
                                }
                                else
                                {
                                    Value4 = txtLISTValue1.Text;
                                }
                            }

                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                AndOr4 = drpLISTAndOr4.SelectedItem.Text;
                            }

                            if (Condition4 == "IS NULL")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "[_]%")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "![_]%")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] NOT LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "%_%")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "!%_%")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }


                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                Condition5 = drpLISTCondition5.SelectedValue;
                                Value5 = txtLISTValue5.Text;

                                if (FIELDNAME5 == "VISIT")
                                {
                                    DataSet dsStatus = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");

                                    if (dsStatus.Tables[0].Rows.Count > 0)
                                    {
                                        Value5 = dsStatus.Tables[0].Rows[0]["VISITNAME"].ToString();
                                    }
                                    else
                                    {
                                        Value5 = txtLISTValue1.Text;
                                    }
                                }

                                if (Condition5 == "IS NULL")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " ";
                                }
                                else if (Condition5 == "IS NOT NULL")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " ";
                                    }

                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " ";
                                }
                                else if (Condition5 == "[_]%")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] LIKE '[" + Value5 + "]%' ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '[" + Value5 + "]%' ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] NOT LIKE '[" + Value5 + "]%' ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '[" + Value5 + "]%' ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else if (Condition5 == "%_%")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] LIKE '%" + Value5 + "%' ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '%" + Value5 + "%' ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] NOT LIKE '%" + Value5 + "%' ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '%" + Value5 + "%' ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " '" + Value5 + "' ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " '" + Value5 + "' ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                            }
                        }
                    }
                }


                CritCodeQUERY = CritCodeQUERY1 + " " + CritCodeQUERY2 + " " + CritCodeQUERY3 + " " + CritCodeQUERY4 + " " + CritCodeQUERY5;

                CritQUERY = CritQUERY1 + " " + CritQUERY2 + " " + CritQUERY3 + " " + CritQUERY4 + " " + CritQUERY5;


                DataSet ds = Dal_Setup.SETUP_CRIT_SP(
                    ACTION: "INSERT_CRIT",
                    SEQNO: txtSeqtNo.Text,
                    DESCRIPTION: txtNotes.Text,
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
                   Value5: txtLISTValue5.Text

                    );
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void CLEAR_Field()
        {
            try
            {
                lbtnSubmit.Visible = true;
                lbnUpdate.Visible = false;

                txtNotes.Text = "";
                txtSeqtNo.Text = "";

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
                ex.Message.ToString();
            }

        }

        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            UPDATE_CRIT();
            GET_DEFINE_CRIT();
            CLEAR_Field();

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Define Criteria Updated Successfully.', 'success');", true);
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
                Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
                Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
                Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
                CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null,
                CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;


                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;

                if (FIELDNAME1 == "VISIT")
                {
                    DataSet dsStatus = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");

                    if (dsStatus.Tables[0].Rows.Count > 0)
                    {
                        Value1 = dsStatus.Tables[0].Rows[0]["VISITNAME"].ToString();
                    }
                    else
                    {
                        Value1 = txtLISTValue1.Text;
                    }
                }

                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    AndOr1 = drpLISTAndOr1.SelectedItem.Text;
                }

                if (Condition1 == "IS NULL")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "[_]%")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "![_]%")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] NOT LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "%_%")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "!%_%")
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else
                {
                    if (!FIELDNAME1.Contains("CAST("))
                    {
                        CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                    }
                    else
                    {
                        CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                    }

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }

                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    Condition2 = drpLISTCondition2.SelectedValue;
                    Value2 = txtLISTValue2.Text;

                    if (FIELDNAME2 == "VISIT")
                    {
                        DataSet dsStatus = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");

                        if (dsStatus.Tables[0].Rows.Count > 0)
                        {
                            Value2 = dsStatus.Tables[0].Rows[0]["VISITNAME"].ToString();
                        }
                        else
                        {
                            Value2 = txtLISTValue1.Text;
                        }
                    }

                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        AndOr2 = drpLISTAndOr2.SelectedItem.Text;
                    }

                    if (Condition2 == "IS NULL")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "[_]%")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "![_]%")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] NOT LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "%_%")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "!%_%")
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else
                    {
                        if (!FIELDNAME2.Contains("CAST("))
                        {
                            CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";
                        }
                        else
                        {
                            CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";
                        }

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }

                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        Condition3 = drpLISTCondition3.SelectedValue;
                        Value3 = txtLISTValue3.Text;

                        if (FIELDNAME3 == "VISIT")
                        {
                            DataSet dsStatus = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");

                            if (dsStatus.Tables[0].Rows.Count > 0)
                            {
                                Value3 = dsStatus.Tables[0].Rows[0]["VISITNAME"].ToString();
                            }
                            else
                            {
                                Value3 = txtLISTValue1.Text;
                            }
                        }

                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            AndOr3 = drpLISTAndOr3.SelectedItem.Text;
                        }

                        if (Condition3 == "IS NULL")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "[_]%")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "![_]%")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] NOT LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "%_%")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "!%_%")
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else
                        {
                            if (!FIELDNAME3.Contains("CAST("))
                            {
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";
                            }

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }


                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            Condition4 = drpLISTCondition4.SelectedValue;
                            Value4 = txtLISTValue4.Text;

                            if (FIELDNAME4 == "VISIT")
                            {
                                DataSet dsStatus = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");

                                if (dsStatus.Tables[0].Rows.Count > 0)
                                {
                                    Value4 = dsStatus.Tables[0].Rows[0]["VISITNAME"].ToString();
                                }
                                else
                                {
                                    Value4 = txtLISTValue1.Text;
                                }
                            }

                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                AndOr4 = drpLISTAndOr4.SelectedItem.Text;
                            }

                            if (Condition4 == "IS NULL")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "[_]%")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "![_]%")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] NOT LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "%_%")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "!%_%")
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else
                            {
                                if (!FIELDNAME4.Contains("CAST("))
                                {
                                    CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";
                                }
                                else
                                {
                                    CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";
                                }

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }


                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                Condition5 = drpLISTCondition5.SelectedValue;
                                Value5 = txtLISTValue5.Text;

                                if (FIELDNAME5 == "VISIT")
                                {
                                    DataSet dsStatus = Dal_Setup.SETUP_CRIT_SP(ACTION: "GET_VISIT");

                                    if (dsStatus.Tables[0].Rows.Count > 0)
                                    {
                                        Value5 = dsStatus.Tables[0].Rows[0]["VISITNAME"].ToString();
                                    }
                                    else
                                    {
                                        Value5 = txtLISTValue1.Text;
                                    }
                                }

                                if (Condition5 == "IS NULL")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " ";
                                }
                                else if (Condition5 == "IS NOT NULL")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " ";
                                    }

                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " ";
                                }
                                else if (Condition5 == "[_]%")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] LIKE '[" + Value5 + "]%' ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '[" + Value5 + "]%' ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] NOT LIKE '[" + Value5 + "]%' ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '[" + Value5 + "]%' ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else if (Condition5 == "%_%")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] LIKE '%" + Value5 + "%' ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '%" + Value5 + "%' ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] NOT LIKE '%" + Value5 + "%' ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '%" + Value5 + "%' ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else
                                {
                                    if (!FIELDNAME5.Contains("CAST("))
                                    {
                                        CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " '" + Value5 + "' ";
                                    }
                                    else
                                    {
                                        CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " '" + Value5 + "' ";
                                    }

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                            }
                        }
                    }
                }


                CritCodeQUERY = CritCodeQUERY1 + " " + CritCodeQUERY2 + " " + CritCodeQUERY3 + " " + CritCodeQUERY4 + " " + CritCodeQUERY5;

                CritQUERY = CritQUERY1 + " " + CritQUERY2 + " " + CritQUERY3 + " " + CritQUERY4 + " " + CritQUERY5;


                DataSet ds = Dal_Setup.SETUP_CRIT_SP(
                    ACTION: "UPDATE_CRIT",
                    SEQNO: txtSeqtNo.Text,
                    DESCRIPTION: txtNotes.Text,
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
                   ID: ViewState["ID"].ToString()
                    );
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            CLEAR_Field();
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            EXPORT_CRITERIA();
        }
        private void EXPORT_CRITERIA()
        {
            try
            {
                string xlname = "Manage Criteria";

                DataSet ds = Dal_Setup.SETUP_LOGS_SP(
                   ACTION: "EXPORT_CRITERIA"
                   );
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
                ex.Message.ToString();
            }
        }

        protected void GrdCRIT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            ViewState["ID"] = ID;
            string CommandName = e.CommandName.ToString();
            if (CommandName == "EDITED")
            {
                EDIT_CRIT(ID);
                lbtnSubmit.Visible = false;
                lbnUpdate.Visible = true;
            }
            else if (CommandName == "DELETED")
            {
                DELETE_CRIT(ID);
                GET_FIELDS();
            }
        }

        private void EDIT_CRIT(string ID)
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_CRIT_SP(ACTION: "EDIT_CRIT", ID: ID);
                if(ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtSeqtNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                    txtNotes.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();

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
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void DELETE_CRIT(string ID)
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_CRIT_SP(
                                      ACTION: "DELETE_CRIT",
                                      ID: ID
                                      );

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Success!', 'Define Criteria Deleted Successfully.', 'success');", true);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void GET_DEFINE_CRIT()
        {
            try
            {
                DataSet ds = Dal_Setup.SETUP_CRIT_SP(
                    ACTION: "GET_DEFINE_CRIT"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GrdCRIT.DataSource = ds.Tables[0];
                    GrdCRIT.DataBind();
                }
                else
                {
                    GrdCRIT.DataSource = null;
                    GrdCRIT.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}