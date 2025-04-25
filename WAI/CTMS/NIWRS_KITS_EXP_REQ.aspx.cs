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
    public partial class NIWRS_KITS_EXP_REQ : System.Web.UI.Page
    {
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtREQUEST_COMMENT.Attributes.Add("MaxLength", "200");
            try
            {

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

        private void BIND_OPTIONS(DropDownList ddl, HiddenField hf)
        {
            try
            {
                DataSet ds = new DataSet();
                if (ddl.SelectedValue == "KITNO")
                {
                    ds = dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "GETLIST_KITNO");
                    string Values = "";
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["KITNO"].ToString() + ",";
                        }
                    }

                    hf.Value = Values.TrimEnd(',');
                }
                else if (ddl.SelectedValue == "LOTNO")
                {
                    ds = dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "GETLIST_LOTNO");
                    string Values = "";
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["LOTNO"].ToString() + ",";
                        }
                    }

                    hf.Value = Values.TrimEnd(',');
                }
                else if (ddl.SelectedValue == "EXPIRY_DATE")
                {
                    ds = dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "GETLIST_EXPIRY_DATE");
                    string Values = "";
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["EXPIRY_DATE"].ToString() + ",";
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

        string CritQUERY, CritCodeQUERY;

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string FIELDNAME1 = drpLISTField1.SelectedValue,
                FIELDNAME2 = drpLISTField2.SelectedValue,
                FIELDNAME3 = drpLISTField3.SelectedValue,
                FIELDNAME4 = drpLISTField4.SelectedValue,
                FIELDNAME5 = drpLISTField5.SelectedValue,
            Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
            Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
            Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
            CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null,
            CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null;

                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;

                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    AndOr1 = drpLISTAndOr1.SelectedItem.Text;
                }

                if (Condition1 == "[_]%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '[" + Value1 + "]%' " + AndOr1 + " ";

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "![_]%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '[" + Value1 + "]%' " + AndOr1 + " ";

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "%_%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " LIKE '%" + Value1 + "%' " + AndOr1 + " ";

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "!%_%")
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }
                else
                {
                    CritCodeQUERY1 = "  " + FIELDNAME1 + " " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";

                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " " + txtLISTValue1.Text + " " + AndOr1 + " ";
                }

                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    Condition2 = drpLISTCondition2.SelectedValue;
                    Value2 = txtLISTValue2.Text;

                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        AndOr2 = drpLISTAndOr2.SelectedItem.Text;
                    }

                    if (Condition2 == "[_]%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '[" + Value2 + "]%' " + AndOr2 + " ";

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "![_]%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '[" + Value2 + "]%' " + AndOr2 + " ";

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "%_%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " LIKE '%" + Value2 + "%' " + AndOr2 + " ";

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "!%_%")
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }
                    else
                    {
                        CritCodeQUERY2 = "  " + FIELDNAME2 + " " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";

                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " " + txtLISTValue2.Text + " " + AndOr2 + " ";
                    }

                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        Condition3 = drpLISTCondition3.SelectedValue;
                        Value3 = txtLISTValue3.Text;

                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            AndOr3 = drpLISTAndOr3.SelectedItem.Text;
                        }

                        if (Condition3 == "[_]%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '[" + Value3 + "]%' " + AndOr3 + " ";

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "![_]%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '[" + Value3 + "]%' " + AndOr3 + " ";


                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "%_%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " LIKE '%" + Value3 + "%' " + AndOr3 + " ";

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "!%_%")
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }
                        else
                        {
                            CritCodeQUERY3 = "  " + FIELDNAME3 + " " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";

                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " " + txtLISTValue3.Text + " " + AndOr3 + " ";
                        }


                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            Condition4 = drpLISTCondition4.SelectedValue;
                            Value4 = txtLISTValue4.Text;

                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                AndOr4 = drpLISTAndOr4.SelectedItem.Text;
                            }

                            if (Condition4 == "[_]%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '[" + Value4 + "]%' " + AndOr4 + " ";

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "![_]%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '[" + Value4 + "]%' " + AndOr4 + " ";

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "%_%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " LIKE '%" + Value4 + "%' " + AndOr4 + " ";

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "!%_%")
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }
                            else
                            {
                                CritCodeQUERY4 = "  " + FIELDNAME4 + " " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";

                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " " + txtLISTValue4.Text + " " + AndOr4 + " ";
                            }


                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                Condition5 = drpLISTCondition5.SelectedValue;
                                Value5 = txtLISTValue5.Text;

                                if (Condition5 == "[_]%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '[" + Value5 + "]%' ";

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '[" + Value5 + "]%' ";

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else if (Condition5 == "%_%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " LIKE '%" + Value5 + "%' ";

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " NOT LIKE '%" + Value5 + "%' ";

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                                else
                                {
                                    CritCodeQUERY5 = "  " + FIELDNAME5 + " " + Condition5 + " '" + Value5 + "' ";

                                    CritQUERY5 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition5.SelectedItem.Text + " " + txtLISTValue5.Text;
                                }
                            }
                        }
                    }
                }


                CritCodeQUERY = CritCodeQUERY1 + " " + CritCodeQUERY2 + " " + CritCodeQUERY3 + " " + CritCodeQUERY4 + " " + CritCodeQUERY5;

                CritQUERY = CritQUERY1 + " " + CritQUERY2 + " " + CritQUERY3 + " " + CritQUERY4 + " " + CritQUERY5;

                ViewState["CritCodeQUERY"] = CritCodeQUERY;
                ViewState["CritQUERY"] = CritQUERY;


                string SITEID = "", Level = "", COUNTRYID = "";
                foreach (ListItem item in lstsite.Items)
                {
                    if (item.Selected)
                    {
                        if (SITEID == "")
                        {
                            SITEID = item.Value;
                        }
                        else
                        {
                            SITEID += "," + item.Value;
                        }
                    }
                }

                foreach (ListItem item in drplevel.Items)
                {
                    if (item.Selected)
                    {
                        if (Level == "")
                        {
                            Level = item.Value;
                        }
                        else
                        {
                            Level += "," + item.Value;
                        }
                    }
                }

                foreach (ListItem item in lstcountry.Items)
                {
                    if (item.Selected)
                    {
                        if (COUNTRYID == "")
                        {
                            COUNTRYID = item.Value;
                        }
                        else
                        {
                            COUNTRYID += "," + item.Value;
                        }
                    }
                }

                DataSet ds = dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "SEARCH_KITS", Criteria: CritQUERY, CritCode: CritCodeQUERY, LEVELS: Level, SITEIDS: SITEID, COUNTRYIDS: COUNTRYID);

                if (ds.Tables.Count > 0)
                {
                    divKITS.Visible = true;
                    divREQ.Visible = true;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_POOL.DataSource = ds.Tables[0];
                        grd_NIWRS_KITS_POOL.DataBind();
                        lbl_NIWRS_KITS_POOL.Text = grd_NIWRS_KITS_POOL.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_POOL.DataSource = null;
                        grd_NIWRS_KITS_POOL.DataBind();
                        lbl_NIWRS_KITS_POOL.Text = "0";
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataSource = ds.Tables[1];
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_ORDERS.Text = grd_NIWRS_KITS_COUNTRY_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_ORDERS.Text = "0";
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY.DataSource = ds.Tables[2];
                        grd_NIWRS_KITS_COUNTRY.DataBind();
                        lbl_NIWRS_KITS_COUNTRY.Text = grd_NIWRS_KITS_COUNTRY.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY.DataBind();
                        lbl_NIWRS_KITS_COUNTRY.Text = "0";
                    }

                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataSource = ds.Tables[3];
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.Text = grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.Text = "0";
                    }

                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataSource = ds.Tables[4];
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.Text = grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.Text = "0";

                    }

                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataSource = ds.Tables[5];
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS.Text = grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS.Text = "0";
                    }

                    if (ds.Tables[6].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE_ORDERS.DataSource = ds.Tables[6];
                        grd_NIWRS_KITS_SITE_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_ORDERS.Text = grd_NIWRS_KITS_SITE_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_SITE_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_ORDERS.Text = "0";
                    }

                    if (ds.Tables[7].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE.DataSource = ds.Tables[7];
                        grd_NIWRS_KITS_SITE.DataBind();
                        lbl_NIWRS_KITS_SITE.Text = grd_NIWRS_KITS_SITE.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE.DataSource = null;
                        grd_NIWRS_KITS_SITE.DataBind();
                        lbl_NIWRS_KITS_SITE.Text = "0";
                    }

                    if (ds.Tables[8].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataSource = ds.Tables[8];
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_TRANSF_ORDERS.Text = grd_NIWRS_KITS_SITE_TRANSF_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_TRANSF_ORDERS.Text = "0";
                    }
                }
                else
                {
                    divKITS.Visible = false;
                    divREQ.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void drplevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedItems = "";

                foreach (ListItem listItem in drplevel.Items)
                {
                    if (listItem.Selected)
                    {
                        selectedItems = listItem.Value;
                    }
                }

                if (selectedItems.Contains("Site") || selectedItems.Contains("Country"))
                {
                    if (selectedItems.Contains("Country"))
                    {
                        divCountry.Visible = true;
                        lstcountry.SelectionMode = ListSelectionMode.Multiple;
                        DataSet ds = dal.GET_COUNTRY_SP();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lstcountry.DataSource = ds.Tables[0];
                            lstcountry.DataValueField = "COUNTRYID";
                            lstcountry.DataTextField = "COUNTRYNAME";
                            lstcountry.DataBind();
                        }
                        else
                        {
                            lstcountry.DataSource = null;
                            lstcountry.DataBind();
                        }
                    }

                    if (selectedItems.Contains("Site"))
                    {
                        divSite.Visible = true;
                        lstsite.SelectionMode = ListSelectionMode.Multiple;

                        lstsite.DataSource = null;
                        lstsite.DataBind();

                        DataSet dsSite = new DataSet();

                        foreach (ListItem listItem in lstcountry.Items)
                        {
                            if (listItem.Selected)
                            {
                                DataSet ds = dal.GET_INVID_SP(COUNTRYID: listItem.Value);
                                if (dsSite.Tables.Count == 0)
                                {
                                    dsSite = ds.Copy();
                                }
                                else
                                {
                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        dsSite.Tables[0].Rows.Add(dr.ItemArray);
                                    }
                                }
                            }
                        }

                        if (dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
                        {
                            lstsite.DataSource = dsSite.Tables[0];
                            lstsite.DataValueField = "SiteID";
                            lstsite.DataBind();
                        }
                    }
                }
                else
                {
                    divCountry.Visible = false;
                    lstcountry.ClearSelection();

                    divSite.Visible = false;
                    lstsite.ClearSelection();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void lstcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsSite = new DataSet();

            foreach (ListItem listItem in lstcountry.Items)
            {
                if (listItem.Selected)
                {
                    DataSet ds = dal.GET_INVID_SP(COUNTRYID: listItem.Value);
                    if (dsSite.Tables.Count == 0)
                    {
                        dsSite = ds.Copy();
                    }
                    else
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            dsSite.Tables[0].Rows.Add(dr.ItemArray);
                        }
                    }
                }
            }

            if (dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                lstsite.DataSource = dsSite.Tables[0];
                lstsite.DataValueField = "SiteID";
                lstsite.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string SITEID = "", Level = "", COUNTRYID = "";
                foreach (ListItem item in lstsite.Items)
                {
                    if (item.Selected)
                    {
                        if (SITEID == "")
                        {
                            SITEID = item.Value;
                        }
                        else
                        {
                            SITEID += "," + item.Value;
                        }
                    }
                }

                foreach (ListItem item in drplevel.Items)
                {
                    if (item.Selected)
                    {
                        if (Level == "")
                        {
                            Level = item.Value;
                        }
                        else
                        {
                            Level += "," + item.Value;
                        }
                    }
                }

                foreach (ListItem item in lstcountry.Items)
                {
                    if (item.Selected)
                    {
                        if (COUNTRYID == "")
                        {
                            COUNTRYID = item.Value;
                        }
                        else
                        {
                            COUNTRYID += "," + item.Value;
                        }
                    }
                }
                DataSet ds = dal_IWRS.IWRS_KITS_EXP_SP(
                    ACTION: "INSERT_REQUEST",
                    Criteria: ViewState["CritQUERY"].ToString(),
                    CritCode: ViewState["CritCodeQUERY"].ToString(),
                    REQUEST_COMMENT: txtREQUEST_COMMENT.Text,
                    REQUEST_EXPDAT: txtREQUEST_EXPDAT.Text,
                    LEVELS: Level,
                    COUNTRYIDS: COUNTRYID,
                    SITEIDS: SITEID
                    );

                string GENERATEREQFOR = ViewState["CritQUERY"].ToString();
                string NEWEXPDATE = txtREQUEST_EXPDAT.Text;
                string REASONFOREXP = txtREQUEST_COMMENT.Text;

                SEND_MAIL(GENERATEREQFOR, NEWEXPDATE, REASONFOREXP);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dal_IWRS.IWRS_KITS_EXP_SP(
                    ACTION: "INSERT_REQUEST_DETAILS",
                    ID: ds.Tables[0].Rows[0]["ID"].ToString(),
                    REQUEST_COMMENT: txtREQUEST_COMMENT.Text
                    );
                }

                Response.Write("<script> alert('Request Generated Successfully.');window.location='NIWRS_KITS_EXP_REQ.aspx';</script>");


               
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        private void SEND_MAIL(string GENERATEREQFOR,  string NEWEXPDATE, string REASONFOREXP)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "GENERATE_EXPIRY_UPDATE");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "Generate_Kit_Expiry_Request");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[GENERATEREQFOR]", GENERATEREQFOR);
                    SUBJECT = SUBJECT.Replace("[NEWEXPDATE]", NEWEXPDATE);
                    SUBJECT = SUBJECT.Replace("[REASONFOREXP]", REASONFOREXP);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[GENERATEREQFOR]", GENERATEREQFOR);
                    BODY = BODY.Replace("[NEWEXPDATE]", NEWEXPDATE);
                    BODY = BODY.Replace("[REASONFOREXP]", REASONFOREXP);
                    BODY = BODY.Replace("[USERNAME]", Session["User_Name"].ToString());
                    BODY = BODY.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    BODY = BODY.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());
                }

                cf.Email_Users(
                EmailAddress: EMAILIDS,
                CCEmailAddress: CCEMAILIDS,
                BCCEmailAddress: BCCEMAILIDS,
                subject: SUBJECT,
                body: BODY
                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}