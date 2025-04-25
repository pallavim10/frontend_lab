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
    public partial class NIWRS_WORKFLOW_DOSAGE : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "SELECT_STEP", ID: Request.QueryString["ID"].ToString());

                    lblList.Text = ds.Tables[0].Rows[0]["HEADER"].ToString();
                    GET_ADD_CRIT_FIELDS(ds.Tables[0].Rows[0]["SOURCE_ID"].ToString(), ds.Tables[0].Rows[0]["SOURCE_TYPE"].ToString());
                    GET_REVIEW_STATUS();
                    GET_DOSAGE_CRIT();

                    GET_RANDARMS();
                    GET_DOSEARMS();
                    DISABLE_BUTTONS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void DISABLE_BUTTONS()
        {
            if (hdnREVIEWSTATUS.Value == "Review")
            {
                btnsubmit.Enabled = false;
                btnsubmit.Text = "Configuration has been Frozen";
                btnsubmit.CssClass = btnsubmit.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdate.Enabled = false;
                btnUpdate.Text = "Configuration has been Frozen";
                btnUpdate.CssClass = btnUpdate.CssClass.Replace("btn-primary", "btn-danger");

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
        private void GET_RANDARMS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_ARMS_SP(ACTION: "GET_RANDARMS");
                drpRANDARM.DataSource = ds.Tables[0];
                drpRANDARM.DataValueField = "TREAT_GRP";
                drpRANDARM.DataTextField = "TREAT_GRP";
                drpRANDARM.DataBind();
                drpRANDARM.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DOSEARMS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_ARMS_SP(ACTION: "GET_DOSEARMS");
                drpDOSEARM.DataSource = ds.Tables[0];
                drpDOSEARM.DataValueField = "TREAT_GRP";
                drpDOSEARM.DataTextField = "TREAT_GRP";
                drpDOSEARM.DataBind();
                drpDOSEARM.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ADD_CRIT_FIELDS(string ID, string TYPE)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_ADD_CRIT_FIELDS", ID: ID, FIELDNAME: TYPE, LISTID: Request.QueryString["ID"].ToString());

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
                ddl.DataTextField = "FIELDNAME";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", "0"));
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

                    hf.Value = Values.TrimEnd(',');
                }
                else if (ddl.SelectedValue == "STATUS")
                {
                    ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS");
                    string Values = "";
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["STATUSNAME"].ToString() + ",";
                        }
                    }

                    hf.Value = Values.TrimEnd(',');
                }
                else
                {
                    ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_FORM_SPEC_FIELD_ANS", VARIABLENAME: ddl.SelectedValue);
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

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_DOSAGE_CRIT()
        {
            try
            {
                string
                FIELDNAME1 = drpLISTField1.SelectedValue,
                FIELDNAME2 = drpLISTField2.SelectedValue,
                FIELDNAME3 = drpLISTField3.SelectedValue,
                FIELDNAME4 = drpLISTField4.SelectedValue,
                FIELDNAME5 = drpLISTField5.SelectedValue,
                CritName = txtCritName.Text,
                SEQNO = txtSEQNO.Text,
            Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
            Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
            Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
            CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null,
            CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;

                string RANDARM = null, DOSEARM = null;

                RANDARM = drpRANDARM.SelectedValue;
                DOSEARM = drpDOSEARM.SelectedValue;

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

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
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

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
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
                else if (Condition1.Contains(">") || Condition1.Contains("<"))
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " CAST(" + Value1 + " AS FLOAT) " + AndOr1 + " ";
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

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
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

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
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
                    else if (Condition2.Contains(">") || Condition2.Contains("<"))
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " CAST(" + Value2 + " AS FLOAT) " + AndOr2 + " ";
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

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
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

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
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
                        else if (Condition3.Contains(">") || Condition3.Contains("<"))
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " CAST(" + Value3 + " AS FLOAT) " + AndOr3 + " ";
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

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
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

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
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
                            else if (Condition4.Contains(">") || Condition4.Contains("<"))
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " CAST(" + Value4 + " AS FLOAT) " + AndOr4 + " ";
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
                                else if (Condition5.Contains(">") || Condition5.Contains("<"))
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " CAST(" + Value5 + " AS FLOAT) ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text + " ";
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

                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(
                                ACTION: "INSERT_DOSAGE_CRIT",

                                STEPID: Request.QueryString["ID"].ToString(),
                                SEQNO: SEQNO,
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

                                RANDARM: RANDARM,
                                DOSEARM: DOSEARM,
                                Quantity: txtKitQuantity.Text,
                                ENTEREDBY: Session["USER_ID"].ToString()
                                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void DELETE_SETFIELD_ALL(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_SETFIELD_STEP_ALL", STEPID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_DOSAGE_CRIT()
        {
            try
            {
                string
                FIELDNAME1 = drpLISTField1.SelectedValue,
                FIELDNAME2 = drpLISTField2.SelectedValue,
                FIELDNAME3 = drpLISTField3.SelectedValue,
                FIELDNAME4 = drpLISTField4.SelectedValue,
                FIELDNAME5 = drpLISTField5.SelectedValue,
                CritName = txtCritName.Text,
                SEQNO = txtSEQNO.Text,
            Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
            Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
            Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
            CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null,
            CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;

                string RANDARM = null, DOSEARM = null;

                RANDARM = drpRANDARM.SelectedValue;
                DOSEARM = drpDOSEARM.SelectedValue;

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

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
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

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
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
                else if (Condition1.Contains(">") || Condition1.Contains("<"))
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " CAST(" + Value1 + " AS FLOAT) " + AndOr1 + " ";
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

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
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

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
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
                    else if (Condition2.Contains(">") || Condition2.Contains("<"))
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " CAST(" + Value2 + " AS FLOAT) " + AndOr2 + " ";
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

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
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

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
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
                        else if (Condition3.Contains(">") || Condition3.Contains("<"))
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " CAST(" + Value3 + " AS FLOAT) " + AndOr3 + " ";
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

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
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

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
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
                            else if (Condition4.Contains(">") || Condition4.Contains("<"))
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " CAST(" + Value4 + " AS FLOAT) " + AndOr4 + " ";
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
                                else if (Condition5.Contains(">") || Condition5.Contains("<"))
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " CAST(" + Value5 + " AS FLOAT) ";
                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text + " ";
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

                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(
                                ACTION: "UPDATE_DOSAGE_CRIT",
                                ID: ViewState["editStepDosageCritID"].ToString(),

                                STEPID: Request.QueryString["ID"].ToString(),
                                SEQNO: SEQNO,
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

                                RANDARM: RANDARM,
                                DOSEARM: DOSEARM,
                                Quantity: txtKitQuantity.Text,
                                ENTEREDBY: Session["USER_ID"].ToString()
                                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void SELECT_DOSAGE_CRIT(string ID)
        {
            try
            {
                btnsubmit.Visible = false;
                btnUpdate.Visible = true;

                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "SELECT_DOSAGE_CRIT", ID: ID);

                txtCritName.Text = ds.Tables[0].Rows[0]["CritName"].ToString();
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

                drpRANDARM.SelectedValue = ds.Tables[0].Rows[0]["RANDARM"].ToString();

                drpDOSEARM.SelectedItem.Text = ds.Tables[0].Rows[0]["DOSEARM"].ToString();
                //drpDOSEARM.SelectedItem.Value = ds.Tables[0].Rows[0]["DOSEARM"].ToString();

                txtKitQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();

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

        private void DELETE_DOSAGE_CRIT(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_DOSAGE_CRIT", ENTEREDBY: Session["USER_ID"].ToString()
, ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DOSAGE_CRIT()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_DOSAGE_CRIT", STEPID: Request.QueryString["ID"].ToString());
                grdStepCrits.DataSource = ds;
                grdStepCrits.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_DOSAGE_CRIT()
        {
            try
            {
                btnsubmit.Visible = true;
                btnUpdate.Visible = false;

                txtCritName.Text = "";
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

                drpRANDARM.SelectedIndex = 0;
                drpDOSEARM.SelectedIndex = 0;
                txtKitQuantity.Text = "";

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
                INSERT_DOSAGE_CRIT();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Dosing criteria defined Successfully.'); ", true);

                GET_DOSAGE_CRIT();
                CLEAR_DOSAGE_CRIT();
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
                CLEAR_DOSAGE_CRIT();
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
                UPDATE_DOSAGE_CRIT();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('defined Dosing criteria updated  Successfully.'); ", true);

                GET_DOSAGE_CRIT();
                CLEAR_DOSAGE_CRIT();
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

                ViewState["editStepDosageCritID"] = id;
                if (e.CommandName == "EditCrit")
                {
                    SELECT_DOSAGE_CRIT(id);

                }
                else if (e.CommandName == "DeleteCrit")
                {
                    DELETE_DOSAGE_CRIT(id);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('defined Dosing criteria deleted  Successfully.'); ", true);

                    GET_DOSAGE_CRIT();
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

        protected void grdStepCrits_RowDataBound(object sender, GridViewRowEventArgs e)
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