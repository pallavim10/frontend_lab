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
    public partial class NIWRS_FORMS_SET_VALUES : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            TxtFormula.Attributes.Add("MaxLength", "500");
            try
            {
                if (!IsPostBack)
                {
                    hdnFieldvalue.Value = Request.QueryString["FIELDID"].ToString();
                    hdnFieldName.Value = Request.QueryString["FieldName"].ToString();
                    lblFieldName.Text = hdnFieldName.Value;
                    lblList.Text = hdnFieldName.Value;
                    GET_REVIEW_STATUS();
                    GetVisible();
                    GET_IWRS_MODULES();
                    GET_ADD_CRIT_FIELDS();
                    GET_CRIT();
                    DISABLE_BUTTONS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        private void GET_REVIEW_STATUS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_CONFIGURATION_REVIEW");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //Review
                    hdnREVIEWSTATUS.Value = ds.Tables[0].Rows[0]["ANS"].ToString();
                }
                else
                {
                    //Unreview
                    hdnREVIEWSTATUS.Value = "Unreview";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DISABLE_BUTTONS()
        {
            if (hdnREVIEWSTATUS.Value == "Review")
            {
                btnsubmit.Enabled = false;
                btnUpdate.Enabled = false;
                btnsubmit.Text = "Configuration has been Frozen";
                btnUpdate.Text = "Configuration has been Frozen";
                btnsubmit.CssClass = btnsubmit.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdate.CssClass = btnUpdate.CssClass.Replace("btn-primary", "btn-danger");

            }
        }
        public void GetVisible()
        {
            btnsubmit.Visible = false;
            btnUpdate.Visible = false;
            TxtFormula.Visible = false;

            lblsetcriteria.Visible = false;
            drpLISTField1.Visible = false;
            drpLISTCondition1.Visible = false;
            drpLISTAndOr1.Visible = false;
            drpLISTField2.Visible = false;
            drpLISTCondition2.Visible = false;
            drpLISTAndOr2.Visible = false;
            drpLISTField3.Visible = false;
            drpLISTCondition3.Visible = false;
            drpLISTAndOr3.Visible = false;
            drpLISTField4.Visible = false;
            drpLISTCondition4.Visible = false;
            drpLISTAndOr4.Visible = false;
            drpLISTField5.Visible = false;
            drpLISTCondition5.Visible = false;
            txtLISTValue1.Visible = false;
            txtLISTValue2.Visible = false;
            txtLISTValue3.Visible = false;
            txtLISTValue4.Visible = false;
            txtLISTValue5.Visible = false;
            txtvalues.Visible = false;
            lblFormula.Visible = false;
            showformula.Visible = false;
            txtvalues.Visible = false;
            lbltxtvalue.Visible = false;
            txtSEQNO.Visible = false;
            lblsequence.Visible = false;
            btncancel.Visible = false;
        }

        public void GET_IWRS_MODULES()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_IWRS_MODULES");

                // BIND_FIELDS(drpLISTField1, ds);
                //BIND_FIELDS(drpLISTField2, ds);
                //BIND_FIELDS(drpLISTField3, ds);
                //BIND_FIELDS(drpLISTField4, ds);
                // BIND_FIELDS(drpLISTField5, ds);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                lblErrorMsg.Text = ex.ToString();
            }
        }
        private void GET_ADD_CRIT_FIELDS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_ADD_OnSubmitCRIT_FIELDS", ID: Request.QueryString["FORMID"].ToString());

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

        private void BIND_OPTIONS(DropDownList ddl, HiddenField hf, TextBox tx)
        {
            try
            {
                DataSet ds = new DataSet();
                if (ddl.SelectedValue == "STATUS")
                {
                    ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_STATUS");
                    string Values = "";
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["STATUSNAME"].ToString() + ",";
                        }
                    }

                    if (Values != "")
                    {
                        hf.Value = Values.TrimEnd(',');
                    }
                }
                else
                {
                    ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_FORM_SPEC_FIELD_ANS", VARIABLENAME: ddl.SelectedValue);
                    string Values = "";
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["VALUE"].ToString() + ",";
                        }
                    }

                    if (Values != "")
                    {
                        hf.Value = Values.TrimEnd(',');
                    }
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
                BIND_OPTIONS(drpLISTField1, hfValue1, txtLISTValue1);
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
                BIND_OPTIONS(drpLISTField2, hfValue2, txtLISTValue2);
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
                BIND_OPTIONS(drpLISTField3, hfValue3, txtLISTValue3);
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
                BIND_OPTIONS(drpLISTField4, hfValue4, txtLISTValue4);
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
                BIND_OPTIONS(drpLISTField5, hfValue5, txtLISTValue5);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
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

                Response.Write("<script> alert('Criteria/Formula Defined Successfully.')</script>");

                GET_CRIT();
                CLEAR_CRIT();
                GetVisible();
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

                Response.Write("<script> alert('Criteria/Formula Updated Successfully.')</script>");

                GET_CRIT();
                CLEAR_CRIT();
                GetVisible();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
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
                METHOD = Ddcalculation.SelectedValue,
                FORMID = Request.QueryString["FORMID"].ToString(),
                //MODULE_IDS = hdnmoduleids.Value,
                VALUES = txtvalues.Text,
                SEQNO = txtSEQNO.Text,
                FORMULA = TxtFormula.Text,
                MODULEID = null, MODULENAME = null, TABLENAME = null, VARIABLENAME = null,
                Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
                Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
                Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
                CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null,
                CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;


                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;

                if (FIELDNAME1 == "STATUS")
                {
                    DataSet dsStatus = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_STATUS_ID_TEXT", FIELDNAME: txtLISTValue1.Text);

                    if (dsStatus.Tables[0].Rows.Count > 0)
                    {
                        Value1 = dsStatus.Tables[0].Rows[0]["STATUSCODE"].ToString();
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

                    if (FIELDNAME2 == "STATUS")
                    {
                        DataSet dsStatus = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_STATUS_ID_TEXT", FIELDNAME: txtLISTValue2.Text);

                        if (dsStatus.Tables[0].Rows.Count > 0)
                        {
                            Value2 = dsStatus.Tables[0].Rows[0]["STATUSCODE"].ToString();
                        }
                        else
                        {
                            Value2 = txtLISTValue2.Text;
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

                        if (FIELDNAME3 == "STATUS")
                        {
                            DataSet dsStatus = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_STATUS_ID_TEXT", FIELDNAME: txtLISTValue3.Text);

                            if (dsStatus.Tables[0].Rows.Count > 0)
                            {
                                Value3 = dsStatus.Tables[0].Rows[0]["STATUSCODE"].ToString();
                            }
                            else
                            {
                                Value3 = txtLISTValue3.Text;
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

                            if (FIELDNAME4 == "STATUS")
                            {
                                DataSet dsStatus = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_STATUS_ID_TEXT", FIELDNAME: txtLISTValue4.Text);

                                if (dsStatus.Tables[0].Rows.Count > 0)
                                {
                                    Value4 = dsStatus.Tables[0].Rows[0]["STATUSCODE"].ToString();
                                }
                                else
                                {
                                    Value4 = txtLISTValue4.Text;
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

                                if (FIELDNAME5 == "STATUS")
                                {
                                    DataSet dsStatus = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_STATUS_ID_TEXT", FIELDNAME: txtLISTValue5.Text);

                                    if (dsStatus.Tables[0].Rows.Count > 0)
                                    {
                                        Value5 = dsStatus.Tables[0].Rows[0]["STATUSCODE"].ToString();
                                    }
                                    else
                                    {
                                        Value5 = txtLISTValue5.Text;
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
                DataSet dsMODULE = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_MODULEDATA", ID: Request.QueryString["FORMID"].ToString());

                if (dsMODULE.Tables[0].Rows.Count > 0)
                {
                    MODULEID = dsMODULE.Tables[0].Rows[0]["MODULEID"].ToString();
                    MODULENAME = dsMODULE.Tables[0].Rows[0]["MODULENAME"].ToString();
                    TABLENAME = dsMODULE.Tables[0].Rows[0]["TABLENAME"].ToString();
                }
                DataSet dsVariablename = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_VARIABLENAME", ID: Request.QueryString["FIELDID"].ToString());
                if (dsVariablename.Tables[0].Rows.Count > 0)
                {
                    VARIABLENAME = dsVariablename.Tables[0].Rows[0]["VARIABLENAME"].ToString();
                }
                if (METHOD == "By Formula")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(
                                ACTION: "INSERT_FORMS_SET_VALUE",
                                FormCode: FORMID,
                                SEQNO: null,
                                ANS: null,
                                FIELDID: hdnFieldvalue.Value,
                                FIELDNAME: hdnFieldName.Value,
                                // PageName: MODULE_IDS,
                                VARIABLENAME: dsVariablename.Tables[0].Rows[0]["VARIABLENAME"].ToString(),
                                MODULEID: dsMODULE.Tables[0].Rows[0]["MODULEID"].ToString(),
                                MODULENAME: dsMODULE.Tables[0].Rows[0]["MODULENAME"].ToString(),
                                TABLENAME: dsMODULE.Tables[0].Rows[0]["TABLENAME"].ToString(),
                                METHOD: Ddcalculation.SelectedValue,
                                FORMULA: TxtFormula.Text,
                                Criteria: null,
                                CritCode: null,

                                Field1: null,
                                CONDITION1: null,
                                Value1: null,
                                AndOr1: null,

                                Field2: null,
                                Condition2: null,
                                Value2: null,
                                AndOr2: null,

                                Field3: null,
                                Condition3: null,
                                Value3: null,
                                AndOr3: null,

                                Field4: null,
                                Condition4: null,
                                Value4: null,
                                AndOr4: null,

                                Field5: null,
                                Condition5: null,
                                Value5: null,
                                ENTEREDBY: Session["USER_ID"].ToString()
                                    );
                }

                else if (METHOD == "By Criteria")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(
                                ACTION: "INSERT_FORMS_SET_VALUE",
                                FormCode: FORMID,
                                SEQNO: SEQNO,
                                ANS: VALUES,
                                FIELDID: hdnFieldvalue.Value,
                                FIELDNAME: hdnFieldName.Value,
                                //PageName: MODULE_IDS,
                                VARIABLENAME: dsVariablename.Tables[0].Rows[0]["VARIABLENAME"].ToString(),
                                MODULEID: dsMODULE.Tables[0].Rows[0]["MODULEID"].ToString(),
                                MODULENAME: dsMODULE.Tables[0].Rows[0]["MODULENAME"].ToString(),
                                TABLENAME: dsMODULE.Tables[0].Rows[0]["TABLENAME"].ToString(),
                                METHOD: Ddcalculation.SelectedValue,
                                FORMULA: null,
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
                                ENTEREDBY: Session["USER_ID"].ToString()
                                    );
                }

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
                METHOD = Ddcalculation.SelectedValue,
                FORMID = Request.QueryString["FORMID"].ToString(),
                VALUES = txtvalues.Text,
                SEQNO = txtSEQNO.Text,
                FORMULA = TxtFormula.Text,
                MODULEID = null, MODULENAME = null, TABLENAME = null, VARIABLENAME = null,
                Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
                Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
                Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
                CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null,
                CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;

                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;

                if (FIELDNAME1 == "STATUS")
                {
                    DataSet dsStatus = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS_ID_TEXT", STATUSNAME: txtLISTValue1.Text);

                    if (dsStatus.Tables[0].Rows.Count > 0)
                    {
                        Value1 = dsStatus.Tables[0].Rows[0]["STATUSCODE"].ToString();
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

                    if (FIELDNAME2 == "STATUS")
                    {
                        DataSet dsStatus = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS_ID_TEXT", STATUSNAME: txtLISTValue2.Text);

                        if (dsStatus.Tables[0].Rows.Count > 0)
                        {
                            Value2 = dsStatus.Tables[0].Rows[0]["STATUSCODE"].ToString();
                        }
                        else
                        {
                            Value2 = txtLISTValue2.Text;
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

                        if (FIELDNAME3 == "STATUS")
                        {
                            DataSet dsStatus = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS_ID_TEXT", STATUSNAME: txtLISTValue3.Text);

                            if (dsStatus.Tables[0].Rows.Count > 0)
                            {
                                Value3 = dsStatus.Tables[0].Rows[0]["STATUSCODE"].ToString();
                            }
                            else
                            {
                                Value3 = txtLISTValue3.Text;
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

                            if (FIELDNAME4 == "STATUS")
                            {
                                DataSet dsStatus = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS_ID_TEXT", STATUSNAME: txtLISTValue4.Text);

                                if (dsStatus.Tables[0].Rows.Count > 0)
                                {
                                    Value4 = dsStatus.Tables[0].Rows[0]["STATUSCODE"].ToString();
                                }
                                else
                                {
                                    Value4 = txtLISTValue4.Text;
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

                                if (FIELDNAME5 == "STATUS")
                                {
                                    DataSet dsStatus = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS_ID_TEXT", STATUSNAME: txtLISTValue5.Text);

                                    if (dsStatus.Tables[0].Rows.Count > 0)
                                    {
                                        Value5 = dsStatus.Tables[0].Rows[0]["STATUSCODE"].ToString();
                                    }
                                    else
                                    {
                                        Value5 = txtLISTValue5.Text;
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


                DataSet dsMODULE = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_NIWRS_MODULEDATA", ID: Session["ID"].ToString());

                if (dsMODULE.Tables[0].Rows.Count > 0)
                {
                    MODULEID = dsMODULE.Tables[0].Rows[0]["MODULEID"].ToString();
                    MODULENAME = dsMODULE.Tables[0].Rows[0]["MODULENAME"].ToString();
                    TABLENAME = dsMODULE.Tables[0].Rows[0]["TABLENAME"].ToString();
                    VARIABLENAME = dsMODULE.Tables[0].Rows[0]["VARIABLENAME"].ToString();
                }

                if (METHOD == "By Formula")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(
                                ACTION: "UPDATE_FORMS_SET_VALUE",
                                FormCode: FORMID,
                                SEQNO: null,
                                ANS: null,
                                FIELDID: hdnFieldvalue.Value,
                                FIELDNAME: hdnFieldName.Value,
                                // PageName: MODULE_IDS,
                                VARIABLENAME: dsMODULE.Tables[0].Rows[0]["VARIABLENAME"].ToString(),
                                MODULEID: dsMODULE.Tables[0].Rows[0]["MODULEID"].ToString(),
                                MODULENAME: dsMODULE.Tables[0].Rows[0]["MODULENAME"].ToString(),
                                TABLENAME: dsMODULE.Tables[0].Rows[0]["TABLENAME"].ToString(),
                                METHOD: Ddcalculation.SelectedValue,
                                FORMULA: TxtFormula.Text,
                                Criteria: null,
                                CritCode: null,

                                Field1: null,
                                CONDITION1: null,
                                Value1: null,
                                AndOr1: null,

                                Field2: null,
                                Condition2: null,
                                Value2: null,
                                AndOr2: null,

                                Field3: null,
                                Condition3: null,
                                Value3: null,
                                AndOr3: null,

                                Field4: null,
                                Condition4: null,
                                Value4: null,
                                AndOr4: null,

                                Field5: null,
                                Condition5: null,
                                Value5: null,
                                ID: Session["ID"].ToString(),
                                ENTEREDBY: Session["USER_ID"].ToString()
                                    );
                }

                else if (METHOD == "By Criteria")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(
                                ACTION: "UPDATE_FORMS_SET_VALUE",
                                FormCode: FORMID,
                                SEQNO: SEQNO,
                                ANS: VALUES,
                                FIELDID: hdnFieldvalue.Value,
                                FIELDNAME: hdnFieldName.Value,
                                VARIABLENAME: dsMODULE.Tables[0].Rows[0]["VARIABLENAME"].ToString(),
                                MODULEID: dsMODULE.Tables[0].Rows[0]["MODULEID"].ToString(),
                                MODULENAME: dsMODULE.Tables[0].Rows[0]["MODULENAME"].ToString(),
                                TABLENAME: dsMODULE.Tables[0].Rows[0]["TABLENAME"].ToString(),
                                METHOD: Ddcalculation.SelectedValue,
                                FORMULA: null,
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
                                ID: Session["ID"].ToString(),
                                ENTEREDBY: Session["USER_ID"].ToString()
                              );
                }

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
                btncancel.Visible = true;

                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "SELECT_FORMS_SET_VALUE", ID: ID);
                Ddcalculation.SelectedValue = ds.Tables[0].Rows[0]["METHOD"].ToString();

                if (Ddcalculation.SelectedValue == "By Formula")
                {

                    lblFormula.Visible = true;
                    TxtFormula.Visible = true;
                    if(hdnFieldName.Value.StartsWith("Age"))
                    {
                        showformula.Visible = true;
                    }

                    Ddcalculation.SelectedValue = ds.Tables[0].Rows[0]["METHOD"].ToString();
                    TxtFormula.Text = ds.Tables[0].Rows[0]["FORMULA"].ToString();

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);

                }
                else if (Ddcalculation.SelectedValue == "By Criteria")
                {
                    TxtFormula.Visible = false;
                    lblsetcriteria.Visible = true;
                    drpLISTField1.Visible = true;
                    drpLISTCondition1.Visible = true;
                    drpLISTAndOr1.Visible = true;
                    drpLISTField2.Visible = true;
                    drpLISTCondition2.Visible = true;
                    drpLISTAndOr2.Visible = true;
                    drpLISTField3.Visible = true;
                    drpLISTCondition3.Visible = true;
                    drpLISTAndOr3.Visible = true;
                    drpLISTField4.Visible = true;
                    drpLISTCondition4.Visible = true;
                    drpLISTAndOr4.Visible = true;
                    drpLISTField5.Visible = true;
                    drpLISTCondition5.Visible = true;
                    txtLISTValue1.Visible = true;
                    txtLISTValue2.Visible = true;
                    txtLISTValue3.Visible = true;
                    txtLISTValue4.Visible = true;
                    txtLISTValue5.Visible = true;
                    txtvalues.Visible = true;
                    lbltxtvalue.Visible = true;
                    txtSEQNO.Visible = true;
                    lblsequence.Visible = true;
                    lblFormula.Visible = false;
                    showformula.Visible = false;
                    btncancel.Visible = false;
                    divalues.Visible = true;
                    divsequence.Visible = true;


                    txtvalues.Text = ds.Tables[0].Rows[0]["VALUE"].ToString();
                    txtSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

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

                    BIND_OPTIONS(drpLISTField1, hfValue1, txtLISTValue1);
                    BIND_OPTIONS(drpLISTField2, hfValue2, txtLISTValue2);
                    BIND_OPTIONS(drpLISTField3, hfValue3, txtLISTValue3);
                    BIND_OPTIONS(drpLISTField4, hfValue4, txtLISTValue4);
                    BIND_OPTIONS(drpLISTField5, hfValue5, txtLISTValue5);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
                }
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
                dal_IWRS.IWRS_SET_FORM_SP(ACTION: "DELETE_FORMS_SET_VALUE", ENTEREDBY: Session["USER_ID"].ToString(), ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdFormsSetValue_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["ID"] = id;
                ViewState["editFORMCritID"] = id;
                if (e.CommandName == "EditCrit")
                {
                    SELECT_CRIT(id);
                }
                else if (e.CommandName == "DeleteCrit")
                {
                    DELETE_CRIT(id);
                    GET_CRIT();
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
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_FORMS_SET_VALUE", ID: Request.QueryString["FORMID"].ToString(), FIELDID: Request.QueryString["FIELDID"].ToString());
                grdFormsSetValue.DataSource = ds;
                grdFormsSetValue.DataBind();

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

                txtvalues.Text = "";
                txtSEQNO.Text = "";

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
                Ddcalculation.SelectedIndex = 0;

                TxtFormula.Text = "";
                showformula.Visible = false;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Ddcalculation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Ddcalculation.SelectedItem.Value == "By Formula")
            {
                btnsubmit.Visible = true;
                btnUpdate.Visible = false;
                TxtFormula.Visible = true;
                lblsetcriteria.Visible = false;
                drpLISTField1.Visible = false;
                drpLISTCondition1.Visible = false;
                drpLISTAndOr1.Visible = false;
                drpLISTField2.Visible = false;
                drpLISTCondition2.Visible = false;
                drpLISTAndOr2.Visible = false;
                drpLISTField3.Visible = false;
                drpLISTCondition3.Visible = false;
                drpLISTAndOr3.Visible = false;
                drpLISTField4.Visible = false;
                drpLISTCondition4.Visible = false;
                drpLISTAndOr4.Visible = false;
                drpLISTField5.Visible = false;
                drpLISTCondition5.Visible = false;
                txtLISTValue1.Visible = false;
                txtLISTValue2.Visible = false;
                txtLISTValue3.Visible = false;
                txtLISTValue4.Visible = false;
                txtLISTValue5.Visible = false;
                txtvalues.Visible = false;
                lblFormula.Visible = true;
                if (hdnFieldName.Value.StartsWith("Age"))
                {
                    showformula.Visible = true;
                }
                txtvalues.Visible = false;
                lbltxtvalue.Visible = false;
                txtSEQNO.Visible = false;
                lblsequence.Visible = false;
                btncancel.Visible = true;
                divalues.Visible = false;
                divsequence.Visible = false;
            }

            else if (Ddcalculation.SelectedItem.Value == "By Criteria")
            {
                btnsubmit.Visible = true;
                btnUpdate.Visible = false;
                TxtFormula.Visible = false;
                lblsetcriteria.Visible = true;
                drpLISTField1.Visible = true;
                drpLISTCondition1.Visible = true;
                drpLISTAndOr1.Visible = true;
                drpLISTField2.Visible = true;
                drpLISTCondition2.Visible = true;
                drpLISTAndOr2.Visible = true;
                drpLISTField3.Visible = true;
                drpLISTCondition3.Visible = true;
                drpLISTAndOr3.Visible = true;
                drpLISTField4.Visible = true;
                drpLISTCondition4.Visible = true;
                drpLISTAndOr4.Visible = true;
                drpLISTField5.Visible = true;
                drpLISTCondition5.Visible = true;
                txtLISTValue1.Visible = true;
                txtLISTValue2.Visible = true;
                txtLISTValue3.Visible = true;
                txtLISTValue4.Visible = true;
                txtLISTValue5.Visible = true;
                txtvalues.Visible = true;
                lbltxtvalue.Visible = true;
                txtSEQNO.Visible = true;
                lblsequence.Visible = true;
                lblFormula.Visible = false;
                showformula.Visible = false;
                btncancel.Visible = true;
                divalues.Visible = true;
                divsequence.Visible = true;
            }
            else if (Ddcalculation.SelectedItem.Value == "Select")
            {
                GetVisible();
            }

        }

        protected void grdFormsSetValue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    LinkButton lbtnDelete = (e.Row.FindControl("lbtnDelete") as LinkButton);

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtnDelete.Enabled = false;
                        lbtnDelete.ToolTip = "Configuration has been Frozen";
                        lbtnDelete.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnYear_Click(object sender, EventArgs e)
        {
            TxtFormula.Text = "";
            TxtFormula.Text = "DATEDIFF(YEAR, [Date of Birth], [Informed Consent/Randomization Date]) - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, [Date of Birth], [Informed Consent / Randomization Date]), [Date of Birth]) > [Informed Consent / Randomization Date] THEN 1 ELSE 0 END";
        }

        protected void btnMonths_Click(object sender, EventArgs e)
        {
            TxtFormula.Text = "";
            TxtFormula.Text = "DATEDIFF(MONTH, [Date of Birth], [Informed Consent/Randomization Date])";
        }

        protected void btnDays_Click(object sender, EventArgs e)
        {
            TxtFormula.Text = "";
            TxtFormula.Text = "DATEDIFF(DAY, [Date of Birth], [Informed Consent/Randomization Date])";
        }

        protected void btnWeeks_Click(object sender, EventArgs e)
        {
            TxtFormula.Text = "";
            TxtFormula.Text = "DATEDIFF(WEEK, [Date of Birth], [Informed Consent/Randomization Date])";
        }

        protected void btnYMD_Click(object sender, EventArgs e)
        {
            TxtFormula.Text = "";
            TxtFormula.Text = "CAST(FLOOR(DATEDIFF(DAY, [Date of Birth], [Informed Consent/Randomization Date]) / 365.25) AS VARCHAR) + ' Years, ' + CAST(FLOOR((DATEDIFF(MONTH, [Date of Birth], [Informed Consent / Randomization Date])) % 12) AS VARCHAR) + ' Months, ' + CAST(DATEDIFF(DAY, DATEADD(MONTH, DATEDIFF(MONTH, [Date of Birth], [Informed Consent / Randomization Date]), [Date of Birth]), [Informed Consent / Randomization Date]) AS VARCHAR) + ' Days'";
        }

        protected void btnMD_Click(object sender, EventArgs e)
        {
            TxtFormula.Text = "";
            TxtFormula.Text = "CAST(FLOOR((DATEDIFF(MONTH, [Date of Birth], [Informed Consent/Randomization Date])) % 12) AS VARCHAR) + ' Months, ' + CAST(DATEDIFF(DAY, DATEADD(MONTH, DATEDIFF(MONTH, [Date of Birth], [Informed Consent / Randomization Date]), [Date of Birth]), [Informed Consent / Randomization Date]) AS VARCHAR) + ' Days'";
        }
    }
}
