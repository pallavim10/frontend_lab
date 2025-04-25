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
    public partial class RISK_INDIC_ALGORITHM : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET();
                    GETCATEGORY();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETCATEGORY()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_RISKINDICAT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpCat1.DataSource = ds;
                    drpCat1.DataTextField = "Category";
                    drpCat1.DataValueField = "Category";
                    drpCat1.DataBind();
                    drpCat1.Items.Insert(0, new ListItem("None", ""));

                    drpCat2.DataSource = ds;
                    drpCat2.DataTextField = "Category";
                    drpCat2.DataValueField = "Category";
                    drpCat2.DataBind();
                    drpCat2.Items.Insert(0, new ListItem("None", ""));

                    drpCat3.DataSource = ds;
                    drpCat3.DataTextField = "Category";
                    drpCat3.DataValueField = "Category";
                    drpCat3.DataBind();
                    drpCat3.Items.Insert(0, new ListItem("None", ""));

                    drpCat4.DataSource = ds;
                    drpCat4.DataTextField = "Category";
                    drpCat4.DataValueField = "Category";
                    drpCat4.DataBind();
                    drpCat4.Items.Insert(0, new ListItem("None", ""));

                    drpCat5.DataSource = ds;
                    drpCat5.DataTextField = "Category";
                    drpCat5.DataValueField = "Category";
                    drpCat5.DataBind();
                    drpCat5.Items.Insert(0, new ListItem("None", ""));

                    drpCat6.DataSource = ds;
                    drpCat6.DataTextField = "Category";
                    drpCat6.DataValueField = "Category";
                    drpCat6.DataBind();
                    drpCat6.Items.Insert(0, new ListItem("None", ""));

                    drpCat7.DataSource = ds;
                    drpCat7.DataTextField = "Category";
                    drpCat7.DataValueField = "Category";
                    drpCat7.DataBind();
                    drpCat7.Items.Insert(0, new ListItem("None", ""));

                    drpCat8.DataSource = ds;
                    drpCat8.DataTextField = "Category";
                    drpCat8.DataValueField = "Category";
                    drpCat8.DataBind();
                    drpCat8.Items.Insert(0, new ListItem("None", ""));

                    drpCat9.DataSource = ds;
                    drpCat9.DataTextField = "Category";
                    drpCat9.DataValueField = "Category";
                    drpCat9.DataBind();
                    drpCat9.Items.Insert(0, new ListItem("None", ""));

                    drpCat10.DataSource = ds;
                    drpCat10.DataTextField = "Category";
                    drpCat10.DataValueField = "Category";
                    drpCat10.DataBind();
                    drpCat10.Items.Insert(0, new ListItem("None", ""));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void INSERT()
        {
            try
            {
                string SEQNO = null, Actionable = null, Condition = null
             , Field1 = null, Condition1 = null, Value1 = null, AndOr1 = null
             , Field2 = null, Condition2 = null, Value2 = null, AndOr2 = null
             , Field3 = null, Condition3 = null, Value3 = null, AndOr3 = null
             , Field4 = null, Condition4 = null, Value4 = null, AndOr4 = null
             , Field5 = null, Condition5 = null, Value5 = null, AndOr5 = null
             , Field6 = null, Condition6 = null, Value6 = null, AndOr6 = null
             , Field7 = null, Condition7 = null, Value7 = null, AndOr7 = null
             , Field8 = null, Condition8 = null, Value8 = null, AndOr8 = null
             , Field9 = null, Condition9 = null, Value9 = null, AndOr9 = null
             , Field10 = null, Condition10 = null, Value10 = null
             , Cat1 = null, Cat2 = null, Cat3 = null, Cat4 = null, Cat5 = null
             , Cat6 = null, Cat7 = null, Cat8 = null, Cat9 = null, Cat10 = null;

                Cat1 = drpCat1.SelectedValue;
                Cat2 = drpCat2.SelectedValue;
                Cat3 = drpCat3.SelectedValue;
                Cat4 = drpCat4.SelectedValue;
                Cat5 = drpCat5.SelectedValue;
                Cat6 = drpCat6.SelectedValue;
                Cat7 = drpCat7.SelectedValue;
                Cat8 = drpCat8.SelectedValue;
                Cat9 = drpCat9.SelectedValue;
                Cat10 = drpCat10.SelectedValue;

                SEQNO = txtSEQNO.Text;
                Actionable = txtActionable.Text;

                Field1 = drpField1.SelectedValue;
                Condition1 = drpCondition1.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value1 = txtValue1.Text;
                AndOr1 = drpAndOr1.SelectedValue;

                Field2 = drpField2.SelectedValue;
                Condition2 = drpCondition2.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value2 = txtValue2.Text;
                AndOr2 = drpAndOr2.SelectedValue;

                Field3 = drpField3.SelectedValue;
                Condition3 = drpCondition3.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value3 = txtValue3.Text;
                AndOr3 = drpAndOr3.SelectedValue;

                Field4 = drpField4.SelectedValue;
                Condition4 = drpCondition4.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value4 = txtValue4.Text;
                AndOr4 = drpAndOr4.SelectedValue;

                Field5 = drpField5.SelectedValue;
                Condition5 = drpCondition5.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value5 = txtValue5.Text;
                AndOr5 = drpAndOr5.SelectedValue;

                Field6 = drpField6.SelectedValue;
                Condition6 = drpCondition6.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value6 = txtValue6.Text;
                AndOr6 = drpAndOr6.SelectedValue;

                Field7 = drpField7.SelectedValue;
                Condition7 = drpCondition7.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value7 = txtValue7.Text;
                AndOr7 = drpAndOr7.SelectedValue;

                Field8 = drpField8.SelectedValue;
                Condition8 = drpCondition8.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value8 = txtValue8.Text;
                AndOr8 = drpAndOr8.SelectedValue;

                Field9 = drpField9.SelectedValue;
                Condition9 = drpCondition9.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value9 = txtValue9.Text;
                AndOr9 = drpAndOr9.SelectedValue;

                Field10 = drpField10.SelectedValue;
                Condition10 = drpCondition10.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value10 = txtValue10.Text;

                if (drpAndOr1.SelectedIndex != 0)
                {
                    if (Cat1 != "")
                    {
                        Condition += " [" + Cat1 + "(" + Field1 + ")] " + Condition1 + " " + Value1 + " " + AndOr1 + " ";
                    }
                    else
                    {
                        Condition += " [" + Field1 + "] " + Condition1 + " " + Value1 + " " + AndOr1 + " ";
                    }

                    if (drpAndOr2.SelectedIndex != 0)
                    {
                        if (Cat2 != "")
                        {
                            Condition += " [" + Cat2 + "(" + Field2 + ")] " + Condition2 + " " + Value2 + " " + AndOr2 + " ";
                        }
                        else
                        {
                            Condition += " [" + Field2 + "] " + Condition2 + " " + Value2 + " " + AndOr2 + " ";
                        }

                        if (drpAndOr3.SelectedIndex != 0)
                        {
                            if (Cat3 != "")
                            {
                                Condition += " [" + Cat3 + "(" + Field3 + ")] " + Condition3 + " " + Value3 + " " + AndOr3 + " ";
                            }
                            else
                            {
                                Condition += " [" + Field3 + "] " + Condition3 + " " + Value3 + " " + AndOr3 + " ";
                            }

                            if (drpAndOr4.SelectedIndex != 0)
                            {
                                if (Cat4 != "")
                                {
                                    Condition += " [" + Cat4 + "(" + Field4 + ")] " + Condition4 + " " + Value4 + " " + AndOr4 + " ";
                                }
                                else
                                {
                                    Condition += " [" + Field4 + "] " + Condition4 + " " + Value4 + " " + AndOr4 + " ";
                                }

                                if (drpAndOr5.SelectedIndex != 0)
                                {
                                    if (Cat5 != "")
                                    {
                                        Condition += " [" + Cat5 + "(" + Field5 + ")] " + Condition5 + " " + Value5 + " " + AndOr5 + " ";
                                    }
                                    else
                                    {
                                        Condition += " [" + Field5 + "] " + Condition5 + " " + Value5 + " " + AndOr5 + " ";
                                    }

                                    if (drpAndOr6.SelectedIndex != 0)
                                    {
                                        if (Cat6 != "")
                                        {
                                            Condition += " [" + Cat6 + "(" + Field6 + ")] " + Condition6 + " " + Value6 + " " + AndOr6 + " ";
                                        }
                                        else
                                        {
                                            Condition += " [" + Field6 + "] " + Condition6 + " " + Value6 + " " + AndOr6 + " ";
                                        }

                                        if (drpAndOr7.SelectedIndex != 0)
                                        {
                                            if (Cat7 != "")
                                            {
                                                Condition += " [" + Cat7 + "(" + Field7 + ")] " + Condition7 + " " + Value7 + " " + AndOr7 + " ";
                                            }
                                            else
                                            {
                                                Condition += " [" + Field7 + "] " + Condition7 + " " + Value7 + " " + AndOr7 + " ";
                                            }

                                            if (drpAndOr8.SelectedIndex != 0)
                                            {
                                                if (Cat8 != "")
                                                {
                                                    Condition += " [" + Cat8 + "(" + Field8 + ")] " + Condition8 + " " + Value8 + " " + AndOr8 + " ";
                                                }
                                                else
                                                {
                                                    Condition += " [" + Field8 + "] " + Condition8 + " " + Value8 + " " + AndOr8 + " ";
                                                }

                                                if (drpAndOr9.SelectedIndex != 0)
                                                {
                                                    if (Cat9 != "")
                                                    {
                                                        Condition += " [" + Cat9 + "(" + Field9 + ")] " + Condition9 + " " + Value9 + " " + AndOr9 + " ";
                                                    }
                                                    else
                                                    {
                                                        Condition += " [" + Field9 + "] " + Condition9 + " " + Value9 + " " + AndOr9 + " ";
                                                    }

                                                    if (Cat10 != "")
                                                    {
                                                        Condition += " [" + Cat10 + "(" + Field10 + ")] " + Condition10 + " " + Value10 + " ";
                                                    }
                                                    else
                                                    {
                                                        Condition += " [" + Field10 + "] " + Condition10 + " " + Value10 + " ";
                                                    }
                                                }
                                                else
                                                {
                                                    if (Cat9 != "")
                                                    {
                                                        Condition += " [" + Cat9 + "(" + Field9 + ")] " + Condition9 + " " + Value9 + " ";
                                                    }
                                                    else
                                                    {
                                                        Condition += " [" + Field9 + "] " + Condition9 + " " + Value9 + " ";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (Cat8 != "")
                                                {
                                                    Condition += " [" + Cat8 + "(" + Field8 + ")] " + Condition8 + " " + Value8 + " ";
                                                }
                                                else
                                                {
                                                    Condition += " [" + Field8 + "] " + Condition8 + " " + Value8 + " ";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Cat7 != "")
                                            {
                                                Condition += " [" + Cat7 + "(" + Field7 + ")] " + Condition7 + " " + Value7 + " ";
                                            }
                                            else
                                            {
                                                Condition += " [" + Field7 + "] " + Condition7 + " " + Value7 + " ";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Cat6 != "")
                                        {
                                            Condition += " [" + Cat6 + "(" + Field6 + ")] " + Condition6 + " " + Value6 + " ";
                                        }
                                        else
                                        {
                                            Condition += " [" + Field6 + "] " + Condition6 + " " + Value6 + " ";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Cat5 != "")
                                    {
                                        Condition += " [" + Cat5 + "(" + Field5 + ")] " + Condition5 + " " + Value5 + " ";
                                    }
                                    else
                                    {
                                        Condition += " [" + Field5 + "] " + Condition5 + " " + Value5 + " ";
                                    }
                                }
                            }
                            else
                            {
                                if (Cat4 != "")
                                {
                                    Condition += " [" + Cat4 + "(" + Field4 + ")] " + Condition4 + " " + Value4 + " ";
                                }
                                else
                                {
                                    Condition += " [" + Field4 + "] " + Condition4 + " " + Value4 + " ";
                                }
                            }
                        }
                        else
                        {
                            if (Cat3 != "")
                            {
                                Condition += " [" + Cat3 + "(" + Field3 + ")] " + Condition3 + " " + Value3 + " ";
                            }
                            else
                            {
                                Condition += " [" + Field3 + "] " + Condition3 + " " + Value3 + " ";
                            }
                        }
                    }
                    else
                    {
                        if (Cat2 != "")
                        {
                            Condition += " [" + Cat2 + "(" + Field2 + ")] " + Condition2 + " " + Value2 + " ";
                        }
                        else
                        {
                            Condition += " [" + Field2 + "] " + Condition2 + " " + Value2 + " ";
                        }
                    }
                }
                else
                {
                    if (Cat1 != "")
                    {
                        Condition += " [" + Cat1 + "(" + Field1 + ")] " + Condition1 + " " + Value1 + " ";
                    }
                    else
                    {
                        Condition += " [" + Field1 + "] " + Condition1 + " " + Value1 + " ";
                    }
                }


                dal.Risk_Indicator_SP(
                Action: "INSERT_ALGORITHM",
                SEQNO: SEQNO,
                Actionable: Actionable,

                Condition: Condition,

                Field1: drpField1.SelectedValue,
                Condition1: drpCondition1.SelectedValue,
                Value1: txtValue1.Text,
                AndOr1: drpAndOr1.SelectedValue,

                Field2: drpField2.SelectedValue,
                Condition2: drpCondition2.SelectedValue,
                Value2: txtValue2.Text,
                AndOr2: drpAndOr2.SelectedValue,

                Field3: drpField3.SelectedValue,
                Condition3: drpCondition3.SelectedValue,
                Value3: txtValue3.Text,
                AndOr3: drpAndOr3.SelectedValue,

                Field4: drpField4.SelectedValue,
                Condition4: drpCondition4.SelectedValue,
                Value4: txtValue4.Text,
                AndOr4: drpAndOr4.SelectedValue,

                Field5: drpField5.SelectedValue,
                Condition5: drpCondition5.SelectedValue,
                Value5: txtValue5.Text,
                AndOr5: drpAndOr5.SelectedValue,

                Field6: drpField6.SelectedValue,
                Condition6: drpCondition6.SelectedValue,
                Value6: txtValue6.Text,
                AndOr6: drpAndOr6.SelectedValue,

                Field7: drpField7.SelectedValue,
                Condition7: drpCondition7.SelectedValue,
                Value7: txtValue7.Text,
                AndOr7: drpAndOr7.SelectedValue,

                Field8: drpField8.SelectedValue,
                Condition8: drpCondition8.SelectedValue,
                Value8: txtValue8.Text,
                AndOr8: drpAndOr8.SelectedValue,

                Field9: drpField9.SelectedValue,
                Condition9: drpCondition9.SelectedValue,
                Value9: txtValue9.Text,
                AndOr9: drpAndOr9.SelectedValue,

                Field10: drpField10.SelectedValue,
                Condition10: drpCondition10.SelectedValue,
                Value10: txtValue10.Text,

                Cat1: drpCat1.SelectedValue,
                Cat2: drpCat2.SelectedValue,
                Cat3: drpCat3.SelectedValue,
                Cat4: drpCat4.SelectedValue,
                Cat5: drpCat5.SelectedValue,
                Cat6: drpCat6.SelectedValue,
                Cat7: drpCat7.SelectedValue,
                Cat8: drpCat8.SelectedValue,
                Cat9: drpCat9.SelectedValue,
                Cat10: drpCat10.SelectedValue

                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE()
        {
            try
            {
                string SEQNO = null, Actionable = null, Condition = null
             , Field1 = null, Condition1 = null, Value1 = null, AndOr1 = null
             , Field2 = null, Condition2 = null, Value2 = null, AndOr2 = null
             , Field3 = null, Condition3 = null, Value3 = null, AndOr3 = null
             , Field4 = null, Condition4 = null, Value4 = null, AndOr4 = null
             , Field5 = null, Condition5 = null, Value5 = null, AndOr5 = null
             , Field6 = null, Condition6 = null, Value6 = null, AndOr6 = null
             , Field7 = null, Condition7 = null, Value7 = null, AndOr7 = null
             , Field8 = null, Condition8 = null, Value8 = null, AndOr8 = null
             , Field9 = null, Condition9 = null, Value9 = null, AndOr9 = null
             , Field10 = null, Condition10 = null, Value10 = null
             , Cat1 = null, Cat2 = null, Cat3 = null, Cat4 = null, Cat5 = null
             , Cat6 = null, Cat7 = null, Cat8 = null, Cat9 = null, Cat10 = null;

                Cat1 = drpCat1.SelectedValue;
                Cat2 = drpCat2.SelectedValue;
                Cat3 = drpCat3.SelectedValue;
                Cat4 = drpCat4.SelectedValue;
                Cat5 = drpCat5.SelectedValue;
                Cat6 = drpCat6.SelectedValue;
                Cat7 = drpCat7.SelectedValue;
                Cat8 = drpCat8.SelectedValue;
                Cat9 = drpCat9.SelectedValue;
                Cat10 = drpCat10.SelectedValue;

                SEQNO = txtSEQNO.Text;
                Actionable = txtActionable.Text;

                Field1 = drpField1.SelectedValue;
                Condition1 = drpCondition1.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value1 = txtValue1.Text;
                AndOr1 = drpAndOr1.SelectedValue;

                Field2 = drpField2.SelectedValue;
                Condition2 = drpCondition2.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value2 = txtValue2.Text;
                AndOr2 = drpAndOr2.SelectedValue;

                Field3 = drpField3.SelectedValue;
                Condition3 = drpCondition3.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value3 = txtValue3.Text;
                AndOr3 = drpAndOr3.SelectedValue;

                Field4 = drpField4.SelectedValue;
                Condition4 = drpCondition4.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value4 = txtValue4.Text;
                AndOr4 = drpAndOr4.SelectedValue;

                Field5 = drpField5.SelectedValue;
                Condition5 = drpCondition5.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value5 = txtValue5.Text;
                AndOr5 = drpAndOr5.SelectedValue;

                Field6 = drpField6.SelectedValue;
                Condition6 = drpCondition6.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value6 = txtValue6.Text;
                AndOr6 = drpAndOr6.SelectedValue;

                Field7 = drpField7.SelectedValue;
                Condition7 = drpCondition7.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value7 = txtValue7.Text;
                AndOr7 = drpAndOr7.SelectedValue;

                Field8 = drpField8.SelectedValue;
                Condition8 = drpCondition8.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value8 = txtValue8.Text;
                AndOr8 = drpAndOr8.SelectedValue;

                Field9 = drpField9.SelectedValue;
                Condition9 = drpCondition9.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value9 = txtValue9.Text;
                AndOr9 = drpAndOr9.SelectedValue;

                Field10 = drpField10.SelectedValue;
                Condition10 = drpCondition10.SelectedValue.Replace("=>", ">=").Replace("=<", "<=");
                Value10 = txtValue10.Text;

                if (drpAndOr1.SelectedIndex != 0)
                {
                    if (Cat1 != "")
                    {
                        Condition += " [" + Cat1 + "(" + Field1 + ")] " + Condition1 + " " + Value1 + " " + AndOr1 + " ";
                    }
                    else
                    {
                        Condition += " [" + Field1 + "] " + Condition1 + " " + Value1 + " " + AndOr1 + " ";
                    }

                    if (drpAndOr2.SelectedIndex != 0)
                    {
                        if (Cat2 != "")
                        {
                            Condition += " [" + Cat2 + "(" + Field2 + ")] " + Condition2 + " " + Value2 + " " + AndOr2 + " ";
                        }
                        else
                        {
                            Condition += " [" + Field2 + "] " + Condition2 + " " + Value2 + " " + AndOr2 + " ";
                        }

                        if (drpAndOr3.SelectedIndex != 0)
                        {
                            if (Cat3 != "")
                            {
                                Condition += " [" + Cat3 + "(" + Field3 + ")] " + Condition3 + " " + Value3 + " " + AndOr3 + " ";
                            }
                            else
                            {
                                Condition += " [" + Field3 + "] " + Condition3 + " " + Value3 + " " + AndOr3 + " ";
                            }

                            if (drpAndOr4.SelectedIndex != 0)
                            {
                                if (Cat4 != "")
                                {
                                    Condition += " [" + Cat4 + "(" + Field4 + ")] " + Condition4 + " " + Value4 + " " + AndOr4 + " ";
                                }
                                else
                                {
                                    Condition += " [" + Field4 + "] " + Condition4 + " " + Value4 + " " + AndOr4 + " ";
                                }

                                if (drpAndOr5.SelectedIndex != 0)
                                {
                                    if (Cat5 != "")
                                    {
                                        Condition += " [" + Cat5 + "(" + Field5 + ")] " + Condition5 + " " + Value5 + " " + AndOr5 + " ";
                                    }
                                    else
                                    {
                                        Condition += " [" + Field5 + "] " + Condition5 + " " + Value5 + " " + AndOr5 + " ";
                                    }

                                    if (drpAndOr6.SelectedIndex != 0)
                                    {
                                        if (Cat6 != "")
                                        {
                                            Condition += " [" + Cat6 + "(" + Field6 + ")] " + Condition6 + " " + Value6 + " " + AndOr6 + " ";
                                        }
                                        else
                                        {
                                            Condition += " [" + Field6 + "] " + Condition6 + " " + Value6 + " " + AndOr6 + " ";
                                        }

                                        if (drpAndOr7.SelectedIndex != 0)
                                        {
                                            if (Cat7 != "")
                                            {
                                                Condition += " [" + Cat7 + "(" + Field7 + ")] " + Condition7 + " " + Value7 + " " + AndOr7 + " ";
                                            }
                                            else
                                            {
                                                Condition += " [" + Field7 + "] " + Condition7 + " " + Value7 + " " + AndOr7 + " ";
                                            }

                                            if (drpAndOr8.SelectedIndex != 0)
                                            {
                                                if (Cat8 != "")
                                                {
                                                    Condition += " [" + Cat8 + "(" + Field8 + ")] " + Condition8 + " " + Value8 + " " + AndOr8 + " ";
                                                }
                                                else
                                                {
                                                    Condition += " [" + Field8 + "] " + Condition8 + " " + Value8 + " " + AndOr8 + " ";
                                                }

                                                if (drpAndOr9.SelectedIndex != 0)
                                                {
                                                    if (Cat9 != "")
                                                    {
                                                        Condition += " [" + Cat9 + "(" + Field9 + ")] " + Condition9 + " " + Value9 + " " + AndOr9 + " ";
                                                    }
                                                    else
                                                    {
                                                        Condition += " [" + Field9 + "] " + Condition9 + " " + Value9 + " " + AndOr9 + " ";
                                                    }

                                                    if (Cat10 != "")
                                                    {
                                                        Condition += " [" + Cat10 + "(" + Field10 + ")] " + Condition10 + " " + Value10 + " ";
                                                    }
                                                    else
                                                    {
                                                        Condition += " [" + Field10 + "] " + Condition10 + " " + Value10 + " ";
                                                    }
                                                }
                                                else
                                                {
                                                    if (Cat9 != "")
                                                    {
                                                        Condition += " [" + Cat9 + "(" + Field9 + ")] " + Condition9 + " " + Value9 + " ";
                                                    }
                                                    else
                                                    {
                                                        Condition += " [" + Field9 + "] " + Condition9 + " " + Value9 + " ";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (Cat8 != "")
                                                {
                                                    Condition += " [" + Cat8 + "(" + Field8 + ")] " + Condition8 + " " + Value8 + " ";
                                                }
                                                else
                                                {
                                                    Condition += " [" + Field8 + "] " + Condition8 + " " + Value8 + " ";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Cat7 != "")
                                            {
                                                Condition += " [" + Cat7 + "(" + Field7 + ")] " + Condition7 + " " + Value7 + " ";
                                            }
                                            else
                                            {
                                                Condition += " [" + Field7 + "] " + Condition7 + " " + Value7 + " ";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Cat6 != "")
                                        {
                                            Condition += " [" + Cat6 + "(" + Field6 + ")] " + Condition6 + " " + Value6 + " ";
                                        }
                                        else
                                        {
                                            Condition += " [" + Field6 + "] " + Condition6 + " " + Value6 + " ";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Cat5 != "")
                                    {
                                        Condition += " [" + Cat5 + "(" + Field5 + ")] " + Condition5 + " " + Value5 + " ";
                                    }
                                    else
                                    {
                                        Condition += " [" + Field5 + "] " + Condition5 + " " + Value5 + " ";
                                    }
                                }
                            }
                            else
                            {
                                if (Cat4 != "")
                                {
                                    Condition += " [" + Cat4 + "(" + Field4 + ")] " + Condition4 + " " + Value4 + " ";
                                }
                                else
                                {
                                    Condition += " [" + Field4 + "] " + Condition4 + " " + Value4 + " ";
                                }
                            }
                        }
                        else
                        {
                            if (Cat3 != "")
                            {
                                Condition += " [" + Cat3 + "(" + Field3 + ")] " + Condition3 + " " + Value3 + " ";
                            }
                            else
                            {
                                Condition += " [" + Field3 + "] " + Condition3 + " " + Value3 + " ";
                            }
                        }
                    }
                    else
                    {
                        if (Cat2 != "")
                        {
                            Condition += " [" + Cat2 + "(" + Field2 + ")] " + Condition2 + " " + Value2 + " ";
                        }
                        else
                        {
                            Condition += " [" + Field2 + "] " + Condition2 + " " + Value2 + " ";
                        }
                    }
                }
                else
                {
                    if (Cat1 != "")
                    {
                        Condition += " [" + Cat1 + "(" + Field1 + ")] " + Condition1 + " " + Value1 + " ";
                    }
                    else
                    {
                        Condition += " [" + Field1 + "] " + Condition1 + " " + Value1 + " ";
                    }
                }

                dal.Risk_Indicator_SP(
                Action: "UPDATE_ALGORITHM",
                ID: ViewState["editAlgo"].ToString(),
                SEQNO: SEQNO,
                Actionable: Actionable,

                Condition: Condition,

                Field1: drpField1.SelectedValue,
                Condition1: drpCondition1.SelectedValue,
                Value1: txtValue1.Text,
                AndOr1: drpAndOr1.SelectedValue,

                Field2: drpField2.SelectedValue,
                Condition2: drpCondition2.SelectedValue,
                Value2: txtValue2.Text,
                AndOr2: drpAndOr2.SelectedValue,

                Field3: drpField3.SelectedValue,
                Condition3: drpCondition3.SelectedValue,
                Value3: txtValue3.Text,
                AndOr3: drpAndOr3.SelectedValue,

                Field4: drpField4.SelectedValue,
                Condition4: drpCondition4.SelectedValue,
                Value4: txtValue4.Text,
                AndOr4: drpAndOr4.SelectedValue,

                Field5: drpField5.SelectedValue,
                Condition5: drpCondition5.SelectedValue,
                Value5: txtValue5.Text,
                AndOr5: drpAndOr5.SelectedValue,

                Field6: drpField6.SelectedValue,
                Condition6: drpCondition6.SelectedValue,
                Value6: txtValue6.Text,
                AndOr6: drpAndOr6.SelectedValue,

                Field7: drpField7.SelectedValue,
                Condition7: drpCondition7.SelectedValue,
                Value7: txtValue7.Text,
                AndOr7: drpAndOr7.SelectedValue,

                Field8: drpField8.SelectedValue,
                Condition8: drpCondition8.SelectedValue,
                Value8: txtValue8.Text,
                AndOr8: drpAndOr8.SelectedValue,

                Field9: drpField9.SelectedValue,
                Condition9: drpCondition9.SelectedValue,
                Value9: txtValue9.Text,
                AndOr9: drpAndOr9.SelectedValue,

                Field10: drpField10.SelectedValue,
                Condition10: drpCondition10.SelectedValue,
                Value10: txtValue10.Text,

                Cat1: drpCat1.SelectedValue,
                Cat2: drpCat2.SelectedValue,
                Cat3: drpCat3.SelectedValue,
                Cat4: drpCat4.SelectedValue,
                Cat5: drpCat5.SelectedValue,
                Cat6: drpCat6.SelectedValue,
                Cat7: drpCat7.SelectedValue,
                Cat8: drpCat8.SelectedValue,
                Cat9: drpCat9.SelectedValue,
                Cat10: drpCat10.SelectedValue

                    );

                btnsubmit.Visible = true;
                btnUpdate.Visible = false;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET()
        {
            try
            {
                DataSet ds = dal.Risk_Indicator_SP(Action: "GET_ALGORITHM");
                grdAlgorithm.DataSource = ds;
                grdAlgorithm.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT(string ID)
        {
            try
            {
                DataSet ds = dal.Risk_Indicator_SP(Action: "SELECT_ALGORITHM", ID: ID);

                txtActionable.Text = ds.Tables[0].Rows[0]["Actionable"].ToString();
                txtSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

                drpField1.SelectedValue = ds.Tables[0].Rows[0]["Field1"].ToString();
                drpCondition1.SelectedValue = ds.Tables[0].Rows[0]["Condition1"].ToString();
                txtValue1.Text = ds.Tables[0].Rows[0]["Value1"].ToString();
                drpAndOr1.SelectedValue = ds.Tables[0].Rows[0]["AndOr1"].ToString();

                drpField2.SelectedValue = ds.Tables[0].Rows[0]["Field2"].ToString();
                drpCondition2.SelectedValue = ds.Tables[0].Rows[0]["Condition2"].ToString();
                txtValue2.Text = ds.Tables[0].Rows[0]["Value2"].ToString();
                drpAndOr2.SelectedValue = ds.Tables[0].Rows[0]["AndOr2"].ToString();

                drpField3.SelectedValue = ds.Tables[0].Rows[0]["Field3"].ToString();
                drpCondition3.SelectedValue = ds.Tables[0].Rows[0]["Condition3"].ToString();
                txtValue3.Text = ds.Tables[0].Rows[0]["Value3"].ToString();
                drpAndOr3.SelectedValue = ds.Tables[0].Rows[0]["AndOr3"].ToString();

                drpField4.SelectedValue = ds.Tables[0].Rows[0]["Field4"].ToString();
                drpCondition4.SelectedValue = ds.Tables[0].Rows[0]["Condition4"].ToString();
                txtValue4.Text = ds.Tables[0].Rows[0]["Value4"].ToString();
                drpAndOr4.SelectedValue = ds.Tables[0].Rows[0]["AndOr4"].ToString();

                drpField5.SelectedValue = ds.Tables[0].Rows[0]["Field5"].ToString();
                drpCondition5.SelectedValue = ds.Tables[0].Rows[0]["Condition5"].ToString();
                txtValue5.Text = ds.Tables[0].Rows[0]["Value5"].ToString();
                drpAndOr5.SelectedValue = ds.Tables[0].Rows[0]["AndOr5"].ToString();

                drpField6.SelectedValue = ds.Tables[0].Rows[0]["Field6"].ToString();
                drpCondition6.SelectedValue = ds.Tables[0].Rows[0]["Condition6"].ToString();
                txtValue6.Text = ds.Tables[0].Rows[0]["Value6"].ToString();
                drpAndOr6.SelectedValue = ds.Tables[0].Rows[0]["AndOr6"].ToString();

                drpField7.SelectedValue = ds.Tables[0].Rows[0]["Field7"].ToString();
                drpCondition7.SelectedValue = ds.Tables[0].Rows[0]["Condition7"].ToString();
                txtValue7.Text = ds.Tables[0].Rows[0]["Value7"].ToString();
                drpAndOr7.SelectedValue = ds.Tables[0].Rows[0]["AndOr7"].ToString();

                drpField8.SelectedValue = ds.Tables[0].Rows[0]["Field8"].ToString();
                drpCondition8.SelectedValue = ds.Tables[0].Rows[0]["Condition8"].ToString();
                txtValue8.Text = ds.Tables[0].Rows[0]["Value8"].ToString();
                drpAndOr8.SelectedValue = ds.Tables[0].Rows[0]["AndOr8"].ToString();

                drpField9.SelectedValue = ds.Tables[0].Rows[0]["Field9"].ToString();
                drpCondition9.SelectedValue = ds.Tables[0].Rows[0]["Condition9"].ToString();
                txtValue9.Text = ds.Tables[0].Rows[0]["Value9"].ToString();
                drpAndOr9.SelectedValue = ds.Tables[0].Rows[0]["AndOr9"].ToString();

                drpField10.SelectedValue = ds.Tables[0].Rows[0]["Field10"].ToString();
                drpCondition10.SelectedValue = ds.Tables[0].Rows[0]["Condition10"].ToString();
                txtValue10.Text = ds.Tables[0].Rows[0]["Value10"].ToString();

                drpCat1.SelectedValue = ds.Tables[0].Rows[0]["Cat1"].ToString();
                drpCat2.SelectedValue = ds.Tables[0].Rows[0]["Cat2"].ToString();
                drpCat3.SelectedValue = ds.Tables[0].Rows[0]["Cat3"].ToString();
                drpCat4.SelectedValue = ds.Tables[0].Rows[0]["Cat4"].ToString();
                drpCat5.SelectedValue = ds.Tables[0].Rows[0]["Cat5"].ToString();
                drpCat6.SelectedValue = ds.Tables[0].Rows[0]["Cat6"].ToString();
                drpCat7.SelectedValue = ds.Tables[0].Rows[0]["Cat7"].ToString();
                drpCat8.SelectedValue = ds.Tables[0].Rows[0]["Cat8"].ToString();
                drpCat9.SelectedValue = ds.Tables[0].Rows[0]["Cat9"].ToString();
                drpCat10.SelectedValue = ds.Tables[0].Rows[0]["Cat10"].ToString();

                btnsubmit.Visible = false;
                btnUpdate.Visible = true;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE(string ID)
        {
            try
            {
                dal.Risk_Indicator_SP(Action: "DELETE_ALGORITHM", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Active(string ID)
        {
            try
            {
                dal.Risk_Indicator_SP(Action: "ACTIVATE_ALGORITHM", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DeActive(string ID)
        {
            try
            {
                dal.Risk_Indicator_SP(Action: "DEACTIVATE_ALGORITHM", ID: ID);
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
                INSERT();
                Response.Redirect(Request.RawUrl.ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE();
                Response.Redirect(Request.RawUrl.ToString());
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
                Response.Redirect(Request.RawUrl.ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdAlgorithm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editAlgo"] = id;
                if (e.CommandName == "EditAlgo")
                {
                    SELECT(id);
                }
                else if (e.CommandName == "DeleteAlgo")
                {
                    DELETE(id);
                    Response.Redirect(Request.RawUrl.ToString());
                }
                else if (e.CommandName == "ActiveAlgo")
                {
                    Active(id);
                    Response.Redirect(Request.RawUrl.ToString());
                }
                else if (e.CommandName == "DeactiveAlgo")
                {
                    DeActive(id);
                    Response.Redirect(Request.RawUrl.ToString());
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdAlgorithm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    LinkButton lblActive = (e.Row.FindControl("lblActive") as LinkButton);
                    LinkButton lblDeActive = (e.Row.FindControl("lblDeActive") as LinkButton);
                    string Active = drv["Active"].ToString();
                    if (Active == "True")
                    {
                        lblActive.Visible = true;
                        lblDeActive.Visible = false;
                    }
                    else
                    {
                        lblActive.Visible = false;
                        lblDeActive.Visible = true;
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