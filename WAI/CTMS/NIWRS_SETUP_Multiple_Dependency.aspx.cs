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
    public partial class NIWRS_SETUP_Multiple_Dependency : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_REVIEW_STATUS();
                    GET_IWRS_MODULES();
                    GET_VISFIELDS();
                    GET_ADD_CRIT_FIELDS();
                    Grd_GetData();
                    DISABLE_BUTTONS();
                    btnUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
        private void GET_IWRS_MODULES()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_IWRS_MODULES");
                ddlVisModule.DataSource = ds.Tables[0];
                ddlVisModule.DataValueField = "ID";
                ddlVisModule.DataTextField = "MODULENAME";
                ddlVisModule.DataBind();
                ddlVisModule.Items.Insert(0, new ListItem("None", "0"));

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_VISFIELDS()
        {
            try
            {
                if (ddlVisModule.SelectedIndex != 0)
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_VIS_FIELDS", MODULEID: ddlVisModule.SelectedValue);
                    ddlVisField.Items.Clear();
                    ddlVisField.DataSource = ds;
                    ddlVisField.DataValueField = "ID";
                    ddlVisField.DataTextField = "FIELDNAME";
                    ddlVisField.DataBind();

                    ddlVisField.Visible = true;
                }
                else
                {
                    ddlVisField.Visible = false;
                }

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
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_ADD_OnSubmitCRIT_FIELDS", ID: Request.QueryString["VISITNUM"].ToString());

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
                if (ddl.SelectedValue == "INDICATION")
                {
                    ds = dal.DM_ADD_UPDATE(ACTION: "GET_INDICATION", PROJECTID: Session["PROJECTID"].ToString());
                    string Values = "";
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["INDICATION"].ToString() + ",";
                        }
                    }

                    if (Values != "")
                    {
                        hf.Value = Values.TrimEnd(',');
                    }
                    else
                    {
                        hf.Value = "";
                        tx.Text = "";
                    }
                }
                else if (ddl.SelectedValue == "STATUS")
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
                    else
                    {
                        hf.Value = "";
                        tx.Text = "";
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
                    else
                    {
                        hf.Value = "";
                        tx.Text = "";
                    }
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            INSERT_MULTIPLE_DEPENDENCY_VISIT();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Multiple Dependency Defined Successfully.'); ", true);
            Grd_GetData();
            CLEAR_Field();
        }

        private void INSERT_MULTIPLE_DEPENDENCY_VISIT()
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


                DataSet ds = dal_IWRS.IWRS_SET_VISIT_SP(
                    ACTION: "INSERT_MULTIPLE_DEPENDENCY_VISIT",
                    VISITNUM: Request.QueryString["VISITNUM"].ToString(),
                    VISIT: TxtDepName.Text,
                    SEQNO: txtSEQNO.Text,
                    WINDOW: txtWindow.Text,
                    EARLY: txtEarly.Text,
                    LATE: txtLate.Text,
                    MODULEID: ddlVisModule.SelectedValue,
                    FIELDID: ddlVisField.SelectedValue,
                    ENTEREDBY: Session["USER_ID"].ToString(),
                    // DEPEND : ddlVisModule.SelectedValue,
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
                   Value5: txtLISTValue5.Text

                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_Field()
        {
            try
            {
                btnsubmit.Visible = true;
                btncancel.Visible = true;
                btnUpdate.Visible = false;

                TxtDepName.Text = "";
                txtSEQNO.Text = "";
                ddlVisModule.SelectedIndex = 0;
                ddlVisField.Visible = false;
                txtWindow.Text = "";
                txtEarly.Text = "";
                txtLate.Text = "";


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

        protected void ddlVisModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_VISFIELDS();
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

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_Field();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdMultDepemdency_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                Session["ID"] = ID;

                if (e.CommandName == "EditMUltDENDCY")
                {
                    SELECT_Multiple_Dependency(ID);
                }
                else if (e.CommandName == "DeleteMUltDENDCY")
                {
                    DELETE_Multiple_Dependency(ID);
                    Grd_GetData();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Grd_GetData()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_VISIT_SP(ACTION: "GET_Multiple_Dependency", ID: Request.QueryString["VISITNUM"].ToString());
                grdMultDepemdency.DataSource = ds;
                grdMultDepemdency.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void SELECT_Multiple_Dependency(string ID)
        {
            try
            {
                btnsubmit.Visible = false;
                btnUpdate.Visible = true;

                DataSet ds = dal_IWRS.IWRS_SET_VISIT_SP(ACTION: "SELECT_Multiple_Dependency", ID: ID);
                TxtDepName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                ddlVisModule.SelectedValue = ds.Tables[0].Rows[0]["MODULE"].ToString();

                GET_VISFIELDS();
                if (ddlVisModule.SelectedIndex != 0)
                {
                    ddlVisField.SelectedValue = ds.Tables[0].Rows[0]["FIELD"].ToString();
                }
                txtWindow.Text = ds.Tables[0].Rows[0]["WINDOW"].ToString();
                txtEarly.Text = ds.Tables[0].Rows[0]["EARLY"].ToString();
                txtLate.Text = ds.Tables[0].Rows[0]["LATE"].ToString();

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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_Multiple_Dependency(string ID)
        {
            try
            {
                dal_IWRS.IWRS_SET_VISIT_SP(ACTION: "DELETE_Multiple_Dependency", ENTEREDBY: Session["USER_ID"].ToString(), ID: ID);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Multiple Dependency Deleled Successfully.'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            Update_Multiple_Dependency();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Multiple Dependency Updated Successfully.'); ", true);

            Grd_GetData();
            CLEAR_Field();
        }

        private void Update_Multiple_Dependency()
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
                                CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " ";
                            }
                            else
                            {
                                CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " ";
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


                DataSet ds = dal_IWRS.IWRS_SET_VISIT_SP(
                    ACTION: "Update_Multiple_Dependency",
                    VISITNUM: Request.QueryString["VISITNUM"].ToString(),
                    VISIT: TxtDepName.Text,
                    SEQNO: txtSEQNO.Text,
                    WINDOW: txtWindow.Text,
                    EARLY: txtEarly.Text,
                    LATE: txtLate.Text,
                    MODULEID: ddlVisModule.SelectedValue,
                    FIELDID: ddlVisField.SelectedValue,
                    ENTEREDBY: Session["USER_ID"].ToString(),
                    DEPEND: null,
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
                    ID: Session["ID"].ToString()

                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdMultDepemdency_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }
}