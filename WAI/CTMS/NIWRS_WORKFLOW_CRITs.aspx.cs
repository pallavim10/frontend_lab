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
    public partial class NIWRS_WORKFLOW_CRITs : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();


        protected void Page_Load(object sender, EventArgs e)
        {
            txtMSGBOX.Attributes.Add("MaxLength", "200");
            txtEventHistory.Attributes.Add("MaxLength", "200");
            try
            {
                if (!IsPostBack)
                {
                    DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "SELECT_STEP", ID: Request.QueryString["ID"].ToString());

                    lblList.Text = ds.Tables[0].Rows[0]["HEADER"].ToString();
                    GET_ADD_CRIT_FIELDS(ds.Tables[0].Rows[0]["SOURCE_ID"].ToString(), ds.Tables[0].Rows[0]["SOURCE_TYPE"].ToString());
                    GET_REVIEW_STATUS();
                    GET_STATUS();
                    GET_VISIT();
                    GET_SETFIELDS();
                    GET_CRIT();
                    GET_OPEN_EMAIL();
                    DISABLE_BUTTONS();
                }

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "CallCkedit", "CallCkedit();", true);
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
        private void GET_VISIT()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_VISIT");
                ddlVisit.DataSource = ds.Tables[0];
                ddlVisit.DataValueField = "ID";
                ddlVisit.DataTextField = "VISIT";
                ddlVisit.DataBind();
                ddlVisit.Items.Insert(0, new ListItem("None", "0"));

                ddlNextVisit.DataSource = ds.Tables[0];
                ddlNextVisit.DataValueField = "ID";
                ddlNextVisit.DataTextField = "VISIT";
                ddlNextVisit.DataBind();
                ddlNextVisit.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_STATUS()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS");
                ddlStatus.DataSource = ds.Tables[0];
                ddlStatus.DataValueField = "STATUSCODE";
                ddlStatus.DataTextField = "STATUSNAME";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SETFIELDS()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_COLS");

                repeatSetFields.DataSource = ds;
                repeatSetFields.DataBind();
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
                if (ddl.SelectedValue == "STATUS")
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
                CritName = txtCritName.Text,
                SEQNO = txtSEQNO.Text,
            Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
            Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
            Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
            CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null,
            CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;

                string MSGBOX = null, NAVTO_TYPE = null, NAVTO = null, EVENTHIST = null, SETFIELD = ",",
                    EMAIL_SUBJECT = null, EMAIL_BODY = null, ApplVisit = null, PERFORM = null, NextVisit = null;
                bool SEND_EMAIL = false;

                ApplVisit = ddlVisit.SelectedValue;
                if (ddlVisit.SelectedValue != "0")
                {
                    NextVisit = ddlNextVisit.SelectedValue;
                }
                PERFORM = ddlPerform.SelectedValue;

                MSGBOX = txtMSGBOX.Text;
                EVENTHIST = txtEventHistory.Text;

                if (ddlStatus.SelectedIndex != 0)
                {
                    SETFIELD = " [STATUS] = '" + ddlStatus.SelectedValue + "',";
                }

                for (int i = 0; i < repeatSetFields.Items.Count; i++)
                {
                    TextBox txtSetFieldVal = (TextBox)repeatSetFields.Items[i].FindControl("txtSetFieldVal");
                    HiddenField hfCOLNAME = (HiddenField)repeatSetFields.Items[i].FindControl("hfCOLNAME");
                    if (txtSetFieldVal.Text != "")
                    {
                        if (SETFIELD != "")
                        {
                            SETFIELD += " [" + hfCOLNAME.Value + "] = '" + txtSetFieldVal.Text + "',";
                        }
                    }
                }

                NAVTO_TYPE = ddlNavType.SelectedValue;
                NAVTO = ddlNavTo.SelectedValue;

                SEND_EMAIL = chkSendEmail.Checked;
                if (SEND_EMAIL)
                {
                    EMAIL_SUBJECT = txtEmailSubject.Text;
                    EMAIL_BODY = txtEmailBody.Text;
                }
                else
                {
                    EMAIL_SUBJECT = "";
                    EMAIL_BODY = "";
                }

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

                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(
                                ACTION: "INSERT_CRIT",

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

                                PERFORM: PERFORM,
                                ApplVisit: ApplVisit,
                                NextVisit: NextVisit,
                                MSGBOX: MSGBOX,
                                SETFIELD: SETFIELD.TrimEnd(','),
                                NAVTO_TYPE: NAVTO_TYPE,
                                NAVTO: NAVTO,
                                EVENTHIST: EVENTHIST,
                                SEND_EMAIL: SEND_EMAIL,
                                EMAIL_SUBJECT: EMAIL_SUBJECT,
                                EMAIL_BODY: EMAIL_BODY
                                    );

                INSERT_SETFIELD(ds.Tables[0].Rows[0][0].ToString());

                INSERT_EMAIL(Request.QueryString["ID"].ToString(), ds.Tables[0].Rows[0][0].ToString());

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void INSERT_SETFIELD(string ID)
        {
            try
            {
                if (ddlStatus.SelectedIndex != 0)
                {
                    dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "INSERT_SETFIELD_CRIT", STEPID: Request.QueryString["ID"].ToString(), STEP_CRIT_ID: ID, FIELDNAME: "STATUS", VALUE: ddlStatus.SelectedValue);
                }
                else
                {
                    dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_SETFIELD_STEP_FIELD_CRIT", STEPID: Request.QueryString["ID"].ToString(), STEP_CRIT_ID: ID, FIELDNAME: "STATUS");
                }

                for (int i = 0; i < repeatSetFields.Items.Count; i++)
                {
                    HiddenField hfCOLNAME = (HiddenField)repeatSetFields.Items[i].FindControl("hfCOLNAME");
                    TextBox txtSetFieldVal = (TextBox)repeatSetFields.Items[i].FindControl("txtSetFieldVal");
                    if (txtSetFieldVal.Text != "")
                    {
                        dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "INSERT_SETFIELD_CRIT", STEPID: Request.QueryString["ID"].ToString(), STEP_CRIT_ID: ID, FIELDNAME: hfCOLNAME.Value, VALUE: txtSetFieldVal.Text);
                    }
                    else
                    {
                        dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_SETFIELD_STEP_FIELD_CRIT", STEPID: Request.QueryString["ID"].ToString(), STEP_CRIT_ID: ID, FIELDNAME: hfCOLNAME.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                CritName = txtCritName.Text,
                SEQNO = txtSEQNO.Text,
            Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
            Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
            Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
            CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null,
            CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;

                string MSGBOX = null, NAVTO_TYPE = null, NAVTO = null, EVENTHIST = null, SETFIELD = ",",
                    EMAIL_SUBJECT = null, EMAIL_BODY = null, ApplVisit = null, PERFORM = null, NextVisit = null;
                bool SEND_EMAIL = false;

                ApplVisit = ddlVisit.SelectedValue;
                if (ddlVisit.SelectedValue != "0")
                {
                    NextVisit = ddlNextVisit.SelectedValue;
                }
                PERFORM = ddlPerform.SelectedValue;

                MSGBOX = txtMSGBOX.Text;
                EVENTHIST = txtEventHistory.Text;

                if (ddlStatus.SelectedIndex != 0)
                {
                    SETFIELD = " [STATUS] = '" + ddlStatus.SelectedValue + "',";
                }

                for (int i = 0; i < repeatSetFields.Items.Count; i++)
                {
                    TextBox txtSetFieldVal = (TextBox)repeatSetFields.Items[i].FindControl("txtSetFieldVal");
                    HiddenField hfCOLNAME = (HiddenField)repeatSetFields.Items[i].FindControl("hfCOLNAME");
                    if (txtSetFieldVal.Text != "")
                    {
                        if (SETFIELD != "")
                        {
                            SETFIELD += " [" + hfCOLNAME.Value + "] = '" + txtSetFieldVal.Text + "',";
                        }
                    }
                }

                NAVTO_TYPE = ddlNavType.SelectedValue;
                NAVTO = ddlNavTo.SelectedValue;

                SEND_EMAIL = chkSendEmail.Checked;
                if (SEND_EMAIL)
                {
                    EMAIL_SUBJECT = txtEmailSubject.Text;
                    EMAIL_BODY = txtEmailBody.Text;
                }
                else
                {
                    EMAIL_SUBJECT = "";
                    EMAIL_BODY = "";
                }

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

                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(
                                ACTION: "UPDATE_CRIT",
                                ID: ViewState["editStepCritID"].ToString(),

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

                                PERFORM: PERFORM,
                                ApplVisit: ApplVisit,
                                NextVisit: NextVisit,
                                MSGBOX: MSGBOX,
                                SETFIELD: SETFIELD.TrimEnd(','),
                                NAVTO_TYPE: NAVTO_TYPE,
                                NAVTO: NAVTO,
                                EVENTHIST: EVENTHIST,
                                SEND_EMAIL: SEND_EMAIL,
                                EMAIL_SUBJECT: EMAIL_SUBJECT,
                                EMAIL_BODY: EMAIL_BODY,
                                ENTEREDBY: Session["USER_ID"].ToString()
                                    );

                INSERT_SETFIELD(ViewState["editStepCritID"].ToString());

                INSERT_EMAIL(Request.QueryString["ID"].ToString(), ViewState["editStepCritID"].ToString());
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

                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "SELECT_CRIT", ID: ID);

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

                ddlVisit.SelectedValue = ds.Tables[0].Rows[0]["ApplVisit"].ToString();
                ddlNextVisit.SelectedValue = ds.Tables[0].Rows[0]["NextVisit"].ToString();
                ddlPerform.SelectedValue = ds.Tables[0].Rows[0]["PERFORM"].ToString();
                txtMSGBOX.Text = ds.Tables[0].Rows[0]["MSGBOX"].ToString();
                ddlNavType.SelectedValue = ds.Tables[0].Rows[0]["NAVTO_TYPE"].ToString();
                GET_NavSOURCE();
                ddlNavTo.SelectedValue = ds.Tables[0].Rows[0]["NAVTO"].ToString();
                txtEventHistory.Text = ds.Tables[0].Rows[0]["EVENTHIST"].ToString();
                chkSendEmail.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["SEND_EMAIL"]);
                txtEmailSubject.Text = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                txtEmailBody.Text = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();

                BIND_OPTIONS(drpLISTField1, hfValue1);
                BIND_OPTIONS(drpLISTField2, hfValue2);
                BIND_OPTIONS(drpLISTField3, hfValue3);
                BIND_OPTIONS(drpLISTField4, hfValue4);
                BIND_OPTIONS(drpLISTField5, hfValue5);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();ShowEmailDiv();showDivNextVisit();", true);


                if (ds.Tables[0].Rows[0]["SETFIELD"].ToString() != "")
                {
                    DataSet dsSet = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_STEP_COLS_CRIT", STEPID: Request.QueryString["ID"].ToString(), STEP_CRIT_ID: ID);
                    repeatSetFields.DataSource = dsSet;
                    repeatSetFields.DataBind();

                    if (dsSet.Tables.Count > 1)
                    {
                        if (dsSet.Tables[1].Rows.Count > 0)
                        {
                            ddlStatus.SelectedValue = dsSet.Tables[1].Rows[0][0].ToString();
                        }
                    }
                }

                GET_STEP_CRIT_EMAIL(Request.QueryString["ID"].ToString(), ID);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
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

        private void DELETE_CRIT(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_CRIT", ID: ID);

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
                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_CRIT", STEPID: Request.QueryString["ID"].ToString());
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

                txtMSGBOX.Text = "";
                txtEventHistory.Text = "";
                ddlStatus.SelectedIndex = 0;
                GET_SETFIELDS();
                ddlNavType.SelectedIndex = 0;
                GET_NavSOURCE();

                chkSendEmail.Checked = false;
                txtEmailSubject.Text = "";
                txtEmailBody.Text = "";
                GET_OPEN_EMAIL();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_STEP_CRIT_EMAIL(string STEPID, string STEP_CRIT_ID)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_STEP_CRIT_EMAIL", STEPID: STEPID, STEP_CRIT_ID: STEP_CRIT_ID);
                gvEmailds.DataSource = ds;
                gvEmailds.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_OPEN_EMAIL()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_OPEN_EMAIL");
                gvEmailds.DataSource = ds;
                gvEmailds.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_EMAIL(string STEPID, string STEP_CRIT_ID)
        {
            try
            {
                if (chkSendEmail.Checked)
                {
                    for (int i = 0; i < gvEmailds.Rows.Count; i++)
                    {
                        TextBox txtEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtEMAILIDs");
                        TextBox txtCCEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtCCEMAILIDs");
                        TextBox txtBCCEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtBCCEMAILIDs");
                        Label lblSiteID = (Label)gvEmailds.Rows[i].FindControl("lblSiteID");

                        if (txtEMAILIDs.Text != "")
                        {
                            dal_IWRS.NIWRS_WORKFLOW_SP
                                (
                                ACTION: "INSERT_STEP_CRIT_EMAIL",
                                STEPID: STEPID,
                                STEP_CRIT_ID: STEP_CRIT_ID,
                                SITEID: lblSiteID.Text,
                                EMAIL_IDS: txtEMAILIDs.Text,
                                CCEMAIL_IDS: txtCCEMAILIDs.Text,
                                BCCEMAIL_IDS: txtBCCEMAILIDs.Text
                                );
                        }
                        else
                        {
                            dal_IWRS.NIWRS_WORKFLOW_SP
                                (
                                ACTION: "DELETE_STEP_CRIT_EMAIL",
                                STEPID: STEPID,
                                STEP_CRIT_ID: STEP_CRIT_ID,
                                SITEID: lblSiteID.Text
                                );
                        }
                    }
                }
                else
                {
                    dal_IWRS.NIWRS_WORKFLOW_SP
                        (
                        ACTION: "DELETE_ALL_STEP_CRIT_EMAIL",
                        STEPID: STEPID,
                        STEP_CRIT_ID: STEP_CRIT_ID
                        );
                }
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Criteria Defined added Successfully.'); ", true);
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

        protected void ddlNavType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_NavSOURCE();
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Criteria updated Successfully.'); ", true);
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

                ViewState["editStepCritID"] = id;
                if (e.CommandName == "EditCrit")
                {
                    SELECT_CRIT(id);
                }
                else if (e.CommandName == "DeleteCrit")
                {
                    DELETE_CRIT(id);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Criteria deleted Successfully.'); ", true);
                    GET_CRIT();

                    
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