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
    public partial class NIWRS_SETUP_CONDITION : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    string ID = Request.QueryString["ID"].ToString();
                    GET_REVIEW_STATUS();
                    GET_LISTING_CONDITION_FIELDS(ID);
                    GET_DATA(ID);
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
                btnsubmit.Text = "Configuration has been Frozen";
                btnsubmit.CssClass = btnsubmit.CssClass.Replace("btn-primary", "btn-danger");
               

            }
        }
        private void GET_LISTING_CONDITION_FIELDS(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_LISTING_CONDITION_FIELDS", ID: ID);

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

        private void GET_DATA(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "SELECT_LISTING", ID: ID);

                lblList.Text = ds.Tables[0].Rows[0]["LISTNAME"].ToString();

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

        private void UPDATE_CONDITION()
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

                if (drpLISTField1.SelectedValue == "STATUS")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_STATUS_SP(ACTION: "GET_STATUS_ID_TEXT", STATUSNAME: txtLISTValue1.Text);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Value1 = ds.Tables[0].Rows[0]["STATUSCODE"].ToString();
                    }
                    else
                    {
                        Value1 = txtLISTValue1.Text;
                    }
                }
                else
                {
                    Value1 = txtLISTValue1.Text;
                }

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

                    if (drpLISTField2.SelectedValue == "STATUS")
                    {
                        DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS_ID_TEXT", STATUSNAME: txtLISTValue2.Text);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Value2 = ds.Tables[0].Rows[0]["STATUSCODE"].ToString();
                        }
                        else
                        {
                            Value2 = txtLISTValue2.Text;
                        }
                    }
                    else
                    {
                        Value2 = txtLISTValue2.Text;
                    }

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

                        if (drpLISTField3.SelectedValue == "STATUS")
                        {
                            DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS_ID_TEXT", STATUSNAME: txtLISTValue3.Text);

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Value3 = ds.Tables[0].Rows[0]["STATUSCODE"].ToString();
                            }
                            else
                            {
                                Value3 = txtLISTValue3.Text;
                            }
                        }
                        else
                        {
                            Value3 = txtLISTValue3.Text;
                        }

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

                            if (drpLISTField4.SelectedValue == "STATUS")
                            {
                                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS_ID_TEXT", STATUSNAME: txtLISTValue4.Text);

                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    Value4 = ds.Tables[0].Rows[0]["STATUSCODE"].ToString();
                                }
                                else
                                {
                                    Value4 = txtLISTValue4.Text;
                                }
                            }
                            else
                            {
                                Value4 = txtLISTValue4.Text;
                            }

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

                                if (drpLISTField5.SelectedValue == "STATUS")
                                {
                                    DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS_ID_TEXT", STATUSNAME: txtLISTValue5.Text);

                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        Value5 = ds.Tables[0].Rows[0]["STATUSCODE"].ToString();
                                    }
                                    else
                                    {
                                        Value5 = txtLISTValue5.Text;
                                    }
                                }
                                else
                                {
                                    Value5 = txtLISTValue5.Text;
                                }

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

                dal_IWRS.IWRS_SET_LIST_SP(
                ACTION: "UPDATE_LISTING_CONDITION",
                ID: Request.QueryString["ID"].ToString(),

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
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_CONDITION();

                Response.Write("<script> alert('Condition Set successfully.')</script>");
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
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
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
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

    }
}