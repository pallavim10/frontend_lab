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
    public partial class Code_ManualCoding : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["AUTO_ID"] != null && Request.QueryString["AUTOCODELEB"] != null)
                    {
                        AUTOCODELEB.Value = Request.QueryString["AUTOCODELEB"].ToString();

                        DataSet dt = dal.DB_CODE_SP(
                               ACTION: "GET_RECODE_DATA",
                               AUTOCODELIB: AUTOCODELEB.Value,
                               ID: Request.QueryString["AUTO_ID"].ToString()
                               );

                        lstCodes.DataSource = dt;
                        lstCodes.DataBind();

                        ADD_DRP_ITEMS(drpField, AUTOCODELEB.Value);
                        ADD_DRP_ITEMS(drpField2, AUTOCODELEB.Value);
                        ADD_DRP_ITEMS(drpField3, AUTOCODELEB.Value);
                        ADD_DRP_ITEMS(drpField4, AUTOCODELEB.Value);
                        ADD_DRP_ITEMS(drpField5, AUTOCODELEB.Value);

                        GetData.Visible = false;
                        divRecode.Visible = true;
                    }
                    else
                    {
                        if (Request.QueryString["ACTION"] != null && Request.QueryString["ACTION"].ToString() == "ChangeCode")
                        {
                            DataSet dt = dal.DB_CODE_SP(
                               ACTION: "GET_MANUALCODE_DATA",
                               MODULEID: Request.QueryString["MODULEID"].ToString(),
                               PVID: Request.QueryString["PVID"].ToString(),
                               RECID: Request.QueryString["RECID"].ToString()
                               );

                            if (dt.Tables[0].Rows.Count > 0)
                            {
                                lblSiteID.Text = dt.Tables[0].Rows[0]["INVID"].ToString();
                                lblSubjectID.Text = dt.Tables[0].Rows[0]["SUBJID"].ToString();
                                AUTOCODELEB.Value = dt.Tables[0].Rows[0]["AutoCodeLIB"].ToString();
                                lblFIELDNAME.Text = dt.Tables[0].Rows[0]["FIELDNAME"].ToString();
                                lblTerm.Text = dt.Tables[0].Rows[0]["VARIABLENAME"].ToString();

                                ADD_DRP_ITEMS(drpField, AUTOCODELEB.Value);
                                ADD_DRP_ITEMS(drpField2, AUTOCODELEB.Value);
                                ADD_DRP_ITEMS(drpField3, AUTOCODELEB.Value);
                                ADD_DRP_ITEMS(drpField4, AUTOCODELEB.Value);
                                ADD_DRP_ITEMS(drpField5, AUTOCODELEB.Value);
                            }

                            DataSet ds = dal.DB_CODE_SP(
                                   ACTION: "GET_MANUALCODE_INFO",
                                   MODULEID: Request.QueryString["MODULEID"].ToString(),
                                   PVID: Request.QueryString["PVID"].ToString(),
                                   RECID: Request.QueryString["RECID"].ToString()
                                   );

                            DataTable dtTranspose = GenerateTransposedTable(ds.Tables[0]);
                            lstInfo.DataSource = dtTranspose;
                            lstInfo.DataBind();

                            DataSet dtRecode = dal.DB_CODE_SP(
                                   ACTION: "GET_RECODE_DATA",
                                   AUTOCODELIB: AUTOCODELEB.Value,
                                   MODULEID: Request.QueryString["MODULEID"].ToString(),
                                   PVID: Request.QueryString["PVID"].ToString(),
                                   RECID: Request.QueryString["RECID"].ToString()
                                   );

                            lstCodes.DataSource = dtRecode;
                            lstCodes.DataBind();

                            GetData.Visible = true;
                            divRecode.Visible = true;
                        }
                        else
                        {
                            DataSet dt = dal.DB_CODE_SP(
                                   ACTION: "GET_MANUALCODE_DATA",
                                   MODULEID: Request.QueryString["MODULEID"].ToString(),
                                   PVID: Request.QueryString["PVID"].ToString(),
                                   RECID: Request.QueryString["RECID"].ToString()
                                   );

                            if (dt.Tables[0].Rows.Count > 0)
                            {
                                lblSiteID.Text = dt.Tables[0].Rows[0]["INVID"].ToString();
                                lblSubjectID.Text = dt.Tables[0].Rows[0]["SUBJID"].ToString();
                                AUTOCODELEB.Value = dt.Tables[0].Rows[0]["AutoCodeLIB"].ToString();
                                lblFIELDNAME.Text = dt.Tables[0].Rows[0]["FIELDNAME"].ToString();
                                lblTerm.Text = dt.Tables[0].Rows[0]["VARIABLENAME"].ToString();

                                ADD_DRP_ITEMS(drpField, AUTOCODELEB.Value);
                                ADD_DRP_ITEMS(drpField2, AUTOCODELEB.Value);
                                ADD_DRP_ITEMS(drpField3, AUTOCODELEB.Value);
                                ADD_DRP_ITEMS(drpField4, AUTOCODELEB.Value);
                                ADD_DRP_ITEMS(drpField5, AUTOCODELEB.Value);
                            }

                            DataSet ds = dal.DB_CODE_SP(
                                   ACTION: "GET_MANUALCODE_INFO",
                                   MODULEID: Request.QueryString["MODULEID"].ToString(),
                                   PVID: Request.QueryString["PVID"].ToString(),
                                   RECID: Request.QueryString["RECID"].ToString()
                                   );

                            DataTable dtTranspose = GenerateTransposedTable(ds.Tables[0]);
                            lstInfo.DataSource = dtTranspose;
                            lstInfo.DataBind();

                            GetData.Visible = true;
                            divRecode.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("NAME");
            outputTable.Columns.Add("VAL");

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                foreach (DataColumn dc in inputTable.Columns)
                {
                    DataRow drNew = outputTable.NewRow();
                    drNew["NAME"] = dc.ColumnName.ToString();
                    drNew["VAL"] = inputTable.Rows[i][dc.ColumnName];
                    outputTable.Rows.Add(drNew);
                }
            }

            return outputTable;
        }

        private void ADD_DRP_ITEMS(DropDownList Drp, string LIB)
        {
            try
            {
                Drp.Items.Clear();

                ListItem item = new ListItem();

                item = new ListItem("--Select--", "0");
                Drp.Items.Add(item);

                if (LIB == "MedDRA")
                {
                    item = new ListItem("Primary SOC", "primary_soc_fg");
                    Drp.Items.Add(item);

                    item = new ListItem("System Organ Class", "soc_name");
                    Drp.Items.Add(item);

                    item = new ListItem("System Organ Class Code", "soc_code");
                    Drp.Items.Add(item);

                    item = new ListItem("High Level Group Term", "hlgt_name");
                    Drp.Items.Add(item);

                    item = new ListItem("High Level Group Term Code", "hlgt_code");
                    Drp.Items.Add(item);

                    item = new ListItem("High Level Term", "hlt_name");
                    Drp.Items.Add(item);

                    item = new ListItem("High Level Term Code", "hlt_code");
                    Drp.Items.Add(item);

                    item = new ListItem("Preferred Term", "pt_name");
                    Drp.Items.Add(item);

                    item = new ListItem("Preferred Term Code", "pt_code");
                    Drp.Items.Add(item);

                    item = new ListItem("Lowest Level Term", "llt_name");
                    Drp.Items.Add(item);

                    item = new ListItem("Lowest Level Term Code", "llt_code");
                    Drp.Items.Add(item);
                }
                else if (LIB == "WHODD")
                {
                    item = new ListItem("ATC Level 1", "CMATC1C");
                    Drp.Items.Add(item);

                    item = new ListItem("ATC Level 1 Code", "CMATC1CD");
                    Drp.Items.Add(item);

                    item = new ListItem("ATC Level 2", "CMATC2C");
                    Drp.Items.Add(item);

                    item = new ListItem("ATC Level 2 Code", "CMATC2CD");
                    Drp.Items.Add(item);

                    item = new ListItem("ATC Level 3", "CMATC3C");
                    Drp.Items.Add(item);

                    item = new ListItem("ATC Level 3 Code", "CMATC3CD");
                    Drp.Items.Add(item);

                    item = new ListItem("ATC Level 4", "CMATC4C");
                    Drp.Items.Add(item);

                    item = new ListItem("ATC Level 4 Code", "CMATC4CD");
                    Drp.Items.Add(item);

                    item = new ListItem("Drug Name", "CMATC5C");
                    Drp.Items.Add(item);

                    item = new ListItem("Drug Code", "CMATC5CD");
                    Drp.Items.Add(item);

                    item = new ListItem("Generic", "CMGEN");
                    Drp.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView1_PreRender(object sender, EventArgs e)
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BIND_GRD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            CLEAR();
        }

        private void CLEAR()
        {
            drpField.ClearSelection();
            drpField2.ClearSelection();
            drpField3.ClearSelection();
            drpField4.ClearSelection();
            drpField5.ClearSelection();

            txtLISTValue1.Text = "";
            txtLISTValue2.Text = "";
            txtLISTValue3.Text = "";
            txtLISTValue4.Text = "";
            txtLISTValue5.Text = "";

            drpLISTCondition1.ClearSelection();
            drpLISTCondition2.ClearSelection();
            drpLISTCondition3.ClearSelection();
            drpLISTCondition4.ClearSelection();
            drpLISTCondition5.ClearSelection();

            drpLISTAndOr1.ClearSelection();
            drpLISTAndOr2.ClearSelection();
            drpLISTAndOr3.ClearSelection();
            drpLISTAndOr4.ClearSelection();
        }

        private void BIND_GRD()
        {
            try
            {
                string
                FIELDNAME1 = drpField.SelectedValue,
                FIELDNAME2 = drpField2.SelectedValue,
                FIELDNAME3 = drpField3.SelectedValue,
                FIELDNAME4 = drpField4.SelectedValue,
                FIELDNAME5 = drpField5.SelectedValue,
            Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
            Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
            Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
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
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "[_]%")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] LIKE '" + Value1 + "%' " + AndOr1 + " ";
                }
                else if (Condition1 == "![_]%")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] NOT LIKE '" + Value1 + "%' " + AndOr1 + " ";
                }
                else if (Condition1 == "%_%")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                }
                else if (Condition1 == "!%_%")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                }
                else
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
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
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "[_]%")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] LIKE '" + Value2 + "%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "![_]%")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] NOT LIKE '" + Value2 + "%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "%_%")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                    }
                    else if (Condition2 == "!%_%")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                    }
                    else
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";
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
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "[_]%")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] LIKE '" + Value3 + "%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "![_]%")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] NOT LIKE '" + Value3 + "%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "%_%")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                        }
                        else if (Condition3 == "!%_%")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                        }
                        else
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";
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
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "[_]%")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] LIKE '" + Value4 + "%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "![_]%")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] NOT LIKE '" + Value4 + "%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "%_%")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                            }
                            else if (Condition4 == "!%_%")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                            }
                            else
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";
                            }


                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                Condition5 = drpLISTCondition5.SelectedValue;
                                Value5 = txtLISTValue5.Text;

                                if (Condition5 == "IS NULL")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " ";
                                }
                                else if (Condition5 == "IS NOT NULL")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " ";
                                }
                                else if (Condition5 == "[_]%")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] LIKE '" + Value5 + "%' ";
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] NOT LIKE '" + Value5 + "%' ";
                                }
                                else if (Condition5 == "%_%")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] LIKE '%" + Value5 + "%' ";
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] NOT LIKE '%" + Value5 + "%' ";
                                }
                                else
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME5 + "] " + Condition5 + " '" + Value5 + "' ";
                                }
                            }
                        }
                    }
                }


                CritCodeQUERY = CritCodeQUERY1 + " " + CritCodeQUERY2 + " " + CritCodeQUERY3 + " " + CritCodeQUERY4 + " " + CritCodeQUERY5;

                DataSet dt = dal.DB_CODE_SP(
                             ACTION: "SEARCH_LIBRARY",
                             CritCodeQUERY: CritCodeQUERY,
                             AUTOCODELIB: AUTOCODELEB.Value
                             );

                if (dt.Tables[0].Rows.Count > 0)
                {
                    gridData.DataSource = dt;
                    gridData.DataBind();
                }
                else
                {
                    gridData.DataSource = null;
                    gridData.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string AUTO_ID = e.CommandArgument.ToString();
                ViewState["AUTO_ID"] = AUTO_ID;

                if (e.CommandName == "CHEKCRIT")
                {
                    CHEK_MANUAL_CODING(AUTO_ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CHEK_MANUAL_CODING(string AUTO_ID)
        {
            if (Request.QueryString["AUTO_ID"] != null)
            {
                dal.DB_CODE_SP(
                    ACTION: "UPDATE_CODING",
                    ID: Request.QueryString["AUTO_ID"].ToString(),
                    AUTO_ID: AUTO_ID,
                    AUTOCODELIB: AUTOCODELEB.Value,
                    AutoCodedTerm: lblTerm.Text
                    );

                Response.Redirect("Code_Recode.aspx", true);
            }
            else
            {
                if (Request.QueryString["ACTION"] != null && Request.QueryString["ACTION"].ToString() == "ChangeCode")
                {
                    dal.DB_CODE_SP(
                    ACTION: "UPDATE_CODING",
                    AUTO_ID: AUTO_ID,
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    PVID: Request.QueryString["PVID"].ToString(),
                    RECID: Request.QueryString["RECID"].ToString(),
                    AUTOCODELIB: AUTOCODELEB.Value,
                    AutoCodedTerm: lblTerm.Text
                    );

                    Response.Redirect("Code_Mismatch.aspx", true);
                }
                else
                {
                    dal.DB_CODE_SP(
                        ACTION: "MANUAL_CODING",
                        ID: AUTO_ID,
                        AUTOCODELIB: AUTOCODELEB.Value,
                        MODULEID: Request.QueryString["MODULEID"].ToString(),
                        PVID: Request.QueryString["PVID"].ToString(),
                        RECID: Request.QueryString["RECID"].ToString(),
                        AutoCodedTerm: lblTerm.Text
                        );

                    Response.Redirect("Code_Uncode.aspx", true);
                }
            }
        }

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[1].CssClass = "disp-none";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnNotApplicable_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["AUTO_ID"] != null)
                {
                    dal.DB_CODE_SP(
                        ACTION: "NotApplicable_CODING",
                        ID: Request.QueryString["AUTO_ID"].ToString(),
                        AUTOCODELIB: AUTOCODELEB.Value,
                        AutoCodedTerm: lblTerm.Text
                        );

                    Response.Redirect("Code_Recode.aspx", true);
                }
                else
                {
                    if (Request.QueryString["ACTION"] != null && Request.QueryString["ACTION"].ToString() == "ChangeCode")
                    {
                        dal.DB_CODE_SP(
                        ACTION: "NotApplicable_CODING",
                        MODULEID: Request.QueryString["MODULEID"].ToString(),
                        PVID: Request.QueryString["PVID"].ToString(),
                        RECID: Request.QueryString["RECID"].ToString(),
                        AUTOCODELIB: AUTOCODELEB.Value,
                        AutoCodedTerm: lblTerm.Text
                        );

                        Response.Redirect("Code_Mismatch.aspx", true);
                    }
                    else
                    {
                        dal.DB_CODE_SP(
                            ACTION: "NotApplicable_CODING",
                            AUTOCODELIB: AUTOCODELEB.Value,
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            PVID: Request.QueryString["PVID"].ToString(),
                            RECID: Request.QueryString["RECID"].ToString(),
                            AutoCodedTerm: lblTerm.Text
                            );

                        Response.Redirect("Code_Uncode.aspx", true);
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