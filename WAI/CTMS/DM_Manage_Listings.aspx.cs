using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_Manage_Listings : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_DB dal_DB = new DAL_DB();
        CommonFunction.CommonFunction CF = new CommonFunction.CommonFunction();

        public string DefaultColorValue = "#000000";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetListing();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetListing()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GETLISTING", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["LIST"].ToString() != "THIS LIST IS ALREADY EXISTS")
                    {
                        drpList.DataSource = ds;
                        drpList.DataValueField = "ID";
                        drpList.DataTextField = "NAME";
                        drpList.DataBind();
                        drpList.Items.Insert(0, new ListItem("-Select-", "0"));

                        ddlOnClickListing.DataSource = ds;
                        ddlOnClickListing.DataValueField = "ID";
                        ddlOnClickListing.DataTextField = "NAME";
                        ddlOnClickListing.DataBind();
                        ddlOnClickListing.Items.Insert(0, new ListItem("-Select-", "0"));

                        drpACLTgList.DataSource = ds;
                        drpACLTgList.DataValueField = "ID";
                        drpACLTgList.DataTextField = "NAME";
                        drpACLTgList.DataBind();
                        drpACLTgList.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                    else
                    {
                        lblErrorMsg.Text = ds.Tables[0].Rows[0]["LIST"].ToString();
                    }
                }
                else
                {

                    drpList.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BINDMODULE()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "BINDMODULE", PROJECTID: Session["PROJECTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd_Module.DataSource = ds.Tables[0];
                    grd_Module.DataBind();
                }

                DataSet ds1 = dal_DB.DB_LIST_SP(Action: "GET_FIELDS", LISTING_ID: drpList.SelectedValue);
                drpField.DataSource = ds1.Tables[0];
                drpField.DataValueField = "ID";
                drpField.DataTextField = "FIELDNAME";
                drpField.DataBind();
                drpField.Items.Insert(0, new ListItem("-Select-", "0"));
                drpField.Items.Insert(1, new ListItem("Visit", "DM_VISITDETAILS.VISIT"));

                drpACLFields.DataSource = ds1.Tables[0];
                drpACLFields.DataValueField = "ID";
                drpACLFields.DataTextField = "FIELDNAME";
                drpACLFields.DataBind();
                drpACLFields.Items.Insert(0, new ListItem("-Select-", "0"));

                drpFormulaField.DataSource = ds1.Tables[1];
                drpFormulaField.DataValueField = "VALUE";
                drpFormulaField.DataTextField = "FIELDNAME";
                drpFormulaField.DataBind();
                drpFormulaField.Items.Insert(0, new ListItem("-Select Field-", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Module_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string ID = dr["ModuleID"].ToString();

                    GridView grd_Field = (GridView)e.Row.FindControl("grd_Field");
                    DataSet ds = dal_DB.DB_LIST_SP(Action: "BIDNFIELDSAGAINSMODULE", PROJECTID: Session["PROJECTID"].ToString(), MODULEID: ID, LISTING_ID: drpList.SelectedValue);
                    grd_Field.DataSource = ds.Tables[0];
                    grd_Field.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Field_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string LISTINGID = dr["LISTINGID"].ToString();
                    string Editable = dr["Editable"].ToString();

                    CheckBox chkListing = (CheckBox)e.Row.FindControl("chkListing");
                    CheckBox chkListingEDITABLE = (CheckBox)e.Row.FindControl("chkListingEDITABLE");

                    string txtSEQNO = ((TextBox)e.Row.FindControl("txtSEQNO")).Text;

                    if (LISTINGID != "" && txtSEQNO != "")
                    {
                        chkListing.Checked = true;
                        txtSEQNO = dr["SEQNO"].ToString();
                    }
                    else
                    {
                        chkListing.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnAddFieldsList_Click(object sender, EventArgs e)
        {
            try
            {
                bool CheckListin = false;

                for (int i = 0; i < grd_Module.Rows.Count; i++)
                {
                    GridView grd_Field = (GridView)grd_Module.Rows[i].FindControl("grd_Field");
                    string MODULEID = ((Label)grd_Module.Rows[i].FindControl("lblID")).Text;
                    for (int j = 0; j < grd_Field.Rows.Count; j++)
                    {
                        CheckBox chkListing = (CheckBox)grd_Field.Rows[j].FindControl("chkListing");
                        string fieldID = ((Label)grd_Field.Rows[j].FindControl("lblID")).Text;
                        string txtSEQNO = ((TextBox)grd_Field.Rows[j].FindControl("txtSEQNO")).Text;

                        if (chkListing.Checked == true && txtSEQNO != "")
                        {
                            DataSet ds = dal_DB.DB_LIST_SP(
                            Action: "Add_Listing",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            USERID: Session["User_ID"].ToString(),
                            SEQNO: txtSEQNO,
                            FIELDNAME: fieldID,
                            MODULEID: MODULEID,
                            LISTING_ID: drpList.SelectedValue
                            );

                            CheckListin = true;
                        }
                        else
                        {
                            DataSet ds = dal_DB.DB_LIST_SP(Action: "Delete_Listing", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString(), SEQNO: txtSEQNO, FIELDNAME: fieldID, MODULEID: MODULEID, LISTING_ID: drpList.SelectedValue);
                            
                        }
                    }
                }
                if (CheckListin)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields added successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields removed successfully.')", true);
                }
                BINDMODULE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void drpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpList.SelectedValue != "0")
                {
                    BINDMODULE();
                    btnAddFieldsList.Visible = true;

                    GET_ACM_Crit();
                    GET_ACM_Modules();
                    GET_AddFields();
                    GET_OnClicks();
                    GET_CRITERIA_ALL();
                    GET_PRIM_MODULE();
                    GET_PRIMARY_DETAILS_MODULE();
                    GET_PRIM_FIELDS();
                    GET_ADDED_PRIM_FIELDS();
                    //GET_PRIMARY_DETAILS_FIELDS();
                    GET_PRIMARY_COLOR();
                    GET_ARC_LISTINGS();
                    GET_ON_CLICK();
                    ShowHideDiv();
                    //DIVMANAGEVISIVLE.Visible = true;
                }
                else
                {
                    grd_Module.DataSource = null;
                    grd_Module.DataBind();
                    btnAddFieldsList.Visible = false;
                    //DIVMANAGEVISIVLE.Visible = false;
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ShowHideDiv()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "ShowHideLISTING", LISTING_ID: drpList.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string DM = ds.Tables[0].Rows[0]["DM"].ToString();
                    string IWRS = ds.Tables[0].Rows[0]["IWRS"].ToString();
                    string Saftey = ds.Tables[0].Rows[0]["Saftey"].ToString();
                    string MEDICAL = ds.Tables[0].Rows[0]["MEDICAL"].ToString();
                    if (DM == "True" || IWRS == "True" || Saftey == "True" || MEDICAL == "True")
                    {
                        if (MEDICAL == "True")
                        {
                            DIVMANAGEVISIVLE.Visible = true;
                            DivMngAdditional.Visible = true;
                            DivSetCritAcrossListing.Visible = true;
                        }
                        else
                        {
                            DIVMANAGEVISIVLE.Visible = true;
                            DivMngAdditional.Visible = false;
                            DivSetCritAcrossListing.Visible = false;
                        }
                    }
                    else
                    {
                        btnAddFieldsList.Visible = false;
                        DIVMANAGEVISIVLE.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_OnClicks()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "Get_NonClick_FIELDs", LISTING_ID: drpList.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MEDICAL"].ToString() == "True")
                    {
                        divOnClickEvent.Visible = true;
                        divOtherListings.Visible = true;
                        divSetPrim.Visible = true;
                        divColor.Visible = true;
                    }
                    else
                    {
                        divOnClickEvent.Visible = false;
                        divOtherListings.Visible = false;
                        divSetPrim.Visible = false;
                        divColor.Visible = false;
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        ddlOnClickField.DataSource = ds.Tables[1];
                        ddlOnClickField.DataValueField = "ID";
                        ddlOnClickField.DataTextField = "FIELDNAME";
                        ddlOnClickField.DataBind();
                        ddlOnClickField.Items.Insert(0, new ListItem("-Select-", "0"));
                        ddlOnClickField.Items.Insert(1, new ListItem("Subject", "Subject"));
                        ddlOnClickField.Items.Insert(1, new ListItem("VISIT", "VISIT"));
                    }

                    DataSet ds1 = dal_DB.DB_LIST_SP(Action: "GET_OnClick", LISTING_ID: drpList.SelectedValue);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlOtherListingField.DataSource = ds1.Tables[0];
                        ddlOtherListingField.DataValueField = "ID";
                        ddlOtherListingField.DataTextField = "FIELDNAME";
                        ddlOtherListingField.DataBind();
                        ddlOtherListingField.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                    else
                    {
                        ddlOtherListingField.DataSource = null;
                        ddlOtherListingField.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_VALUE", LISTING_ID: drpList.SelectedValue, FIELDID: drpField.SelectedValue);

                DataTable dt = ds.Tables[0];
                string Values = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Values += "" + dt.Rows[i]["Value"].ToString() + ",";
                }

                hfValues.Value = Values.TrimEnd(',');

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindValues();", true);

                GET_CRITERIA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Cancel()
        {
            try
            {
                drpField.SelectedIndex = 0;

                drpCondition1.SelectedIndex = 0;
                txtValue1.Text = "";
                drpAndOr1.SelectedIndex = 0;

                drpCondition2.SelectedIndex = 0;
                txtValue2.Text = "";
                drpAndOr2.SelectedIndex = 0;

                drpCondition3.SelectedIndex = 0;
                txtValue3.Text = "";
                drpAndOr3.SelectedIndex = 0;

                drpCondition4.SelectedIndex = 0;
                txtValue4.Text = "";
                drpAndOr4.SelectedIndex = 0;

                drpCondition5.SelectedIndex = 0;
                txtValue5.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSaveCrit_Click(object sender, EventArgs e)
        {
            try
            {
                Insert_Crit();
                GET_CRITERIA_ALL();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Insert_Crit()
        {
            try
            {
                string LISTING_ID = null, FIELDNAME = null,
            FIELDID = null, Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
            Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
            Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null,
            QUERY = null, QUERY1 = null, QUERY2 = null, QUERY3 = null, QUERY4 = null, QUERY5 = null;

                string Criteria = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null;

                LISTING_ID = drpList.SelectedValue;
                FIELDNAME = drpField.SelectedItem.Text;
                FIELDID = drpField.SelectedValue;

                Condition1 = drpCondition1.SelectedValue;
                Value1 = txtValue1.Text;
                if (drpAndOr1.SelectedIndex != 0)
                {
                    AndOr1 = drpAndOr1.SelectedItem.Text;
                }

                if (Condition1 == "IS NULL")
                {
                    QUERY1 = "  [" + FIELDNAME + "] " + Condition1 + " " + AndOr1 + " ";
                    CritQUERY1 = drpCondition1.SelectedItem.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    QUERY1 = "  [" + FIELDNAME + "] " + Condition1 + " " + AndOr1 + " ";
                    CritQUERY1 = drpCondition1.SelectedItem.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "[_]%")
                {
                    QUERY1 = "  [" + FIELDNAME + "] LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    CritQUERY1 = "" + FIELDNAME + " " + drpCondition1.SelectedItem.Text + " " + AndOr1 + " ";
                }
                else if (Condition1 == "![_]%")
                {
                    QUERY1 = "  [" + FIELDNAME + "] NOT LIKE '[" + Value1 + "]%' " + AndOr1 + " ";
                    CritQUERY1 = drpCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "%_%")
                {
                    QUERY1 = "  [" + FIELDNAME + "] LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    CritQUERY1 = drpCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                }
                else if (Condition1 == "!%_%")
                {
                    QUERY1 = "  [" + FIELDNAME + "] NOT LIKE '%" + Value1 + "%' " + AndOr1 + " ";
                    CritQUERY1 = drpCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                }
                else
                {
                    QUERY1 = "  [" + FIELDNAME + "] " + Condition1 + " '" + Value1 + "' " + AndOr1 + " ";
                    CritQUERY1 = drpCondition1.SelectedItem.Text + " " + Value1 + " " + AndOr1 + " ";
                }

                if (drpAndOr1.SelectedIndex != 0)
                {
                    Condition2 = drpCondition2.SelectedValue;
                    Value2 = txtValue2.Text;
                    if (drpAndOr2.SelectedIndex != 0)
                    {
                        AndOr2 = drpAndOr2.SelectedItem.Text;
                    }

                    if (Condition2 == "IS NULL")
                    {
                        QUERY2 = "  [" + FIELDNAME + "] " + Condition2 + " " + AndOr2 + " ";
                        CritQUERY2 = drpCondition2.SelectedItem.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        QUERY2 = "  [" + FIELDNAME + "] " + Condition2 + " " + AndOr2 + " ";
                        CritQUERY2 = drpCondition2.SelectedItem.Text + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "[_]%")
                    {
                        QUERY2 = "  [" + FIELDNAME + "] LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        CritQUERY2 = drpCondition2.SelectedItem.Text + " " + Value2 + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "![_]%")
                    {
                        QUERY2 = "  [" + FIELDNAME + "] NOT LIKE '[" + Value2 + "]%' " + AndOr2 + " ";
                        CritQUERY2 = drpCondition2.SelectedItem.Text + " " + Value2 + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "%_%")
                    {
                        QUERY2 = "  [" + FIELDNAME + "] LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        CritQUERY2 = drpCondition2.SelectedItem.Text + " " + Value2 + " " + AndOr2 + " ";
                    }
                    else if (Condition2 == "!%_%")
                    {
                        QUERY2 = "  [" + FIELDNAME + "] NOT LIKE '%" + Value2 + "%' " + AndOr2 + " ";
                        CritQUERY2 = drpCondition2.SelectedItem.Text + " " + Value2 + " " + AndOr2 + " ";
                    }
                    else
                    {
                        QUERY2 = "  [" + FIELDNAME + "] " + Condition2 + " '" + Value2 + "' " + AndOr2 + " ";
                        CritQUERY2 = drpCondition2.SelectedItem.Text + " " + Value2 + " " + AndOr2 + " ";
                    }

                    if (drpAndOr2.SelectedIndex != 0)
                    {
                        Condition3 = drpCondition3.SelectedValue;
                        Value3 = txtValue3.Text;
                        if (drpAndOr3.SelectedIndex != 0)
                        {
                            AndOr3 = drpAndOr3.SelectedItem.Text;
                        }

                        if (Condition3 == "IS NULL")
                        {
                            QUERY3 = "  [" + FIELDNAME + "] " + Condition3 + " " + AndOr3 + " ";
                            CritQUERY3 = drpCondition3.SelectedItem.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            QUERY3 = "  [" + FIELDNAME + "] " + Condition3 + " " + AndOr3 + " ";
                            CritQUERY3 = drpCondition3.SelectedItem.Text + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "[_]%")
                        {
                            QUERY3 = "  [" + FIELDNAME + "] LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            CritQUERY3 = drpCondition3.SelectedItem.Text + " " + Value3 + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "![_]%")
                        {
                            QUERY3 = "  [" + FIELDNAME + "] NOT LIKE '[" + Value3 + "]%' " + AndOr3 + " ";
                            CritQUERY3 = drpCondition3.SelectedItem.Text + " " + Value3 + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "%_%")
                        {
                            QUERY3 = "  [" + FIELDNAME + "] LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            CritQUERY3 = drpCondition3.SelectedItem.Text + " " + Value3 + " " + AndOr3 + " ";
                        }
                        else if (Condition3 == "!%_%")
                        {
                            QUERY3 = "  [" + FIELDNAME + "] NOT LIKE '%" + Value3 + "%' " + AndOr3 + " ";
                            CritQUERY3 = drpCondition3.SelectedItem.Text + " " + Value3 + " " + AndOr3 + " ";
                        }
                        else
                        {
                            QUERY3 = "  [" + FIELDNAME + "] " + Condition3 + " '" + Value3 + "' " + AndOr3 + " ";
                            CritQUERY3 = drpCondition3.SelectedItem.Text + " " + Value3 + " " + AndOr3 + " ";
                        }


                        if (drpAndOr3.SelectedIndex != 0)
                        {
                            Condition4 = drpCondition4.SelectedValue;
                            Value4 = txtValue4.Text;
                            if (drpAndOr4.SelectedIndex != 0)
                            {
                                AndOr4 = drpAndOr4.SelectedItem.Text;
                            }

                            if (Condition4 == "IS NULL")
                            {
                                QUERY4 = "  [" + FIELDNAME + "] " + Condition4 + " " + AndOr4 + " ";
                                CritQUERY4 = drpCondition4.SelectedItem.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                QUERY4 = "  [" + FIELDNAME + "] " + Condition4 + " " + AndOr4 + " ";
                                CritQUERY4 = drpCondition4.SelectedItem.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "[_]%")
                            {
                                QUERY4 = "  [" + FIELDNAME + "] LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                CritQUERY4 = drpCondition4.SelectedItem.Text + " " + txtValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "![_]%")
                            {
                                QUERY4 = "  [" + FIELDNAME + "] NOT LIKE '[" + Value4 + "]%' " + AndOr4 + " ";
                                CritQUERY4 = drpCondition4.SelectedItem.Text + " " + txtValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "%_%")
                            {
                                QUERY4 = "  [" + FIELDNAME + "] LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                CritQUERY4 = " " + drpCondition4.SelectedItem.Text + " " + txtValue4.Text + " " + AndOr4 + " ";
                            }
                            else if (Condition4 == "!%_%")
                            {
                                QUERY4 = "  [" + FIELDNAME + "] NOT LIKE '%" + Value4 + "%' " + AndOr4 + " ";
                                CritQUERY4 = " " + drpCondition4.SelectedItem.Text + " " + txtValue4.Text + " " + AndOr4 + " ";
                            }
                            else
                            {
                                QUERY4 = "  [" + FIELDNAME + "] " + Condition4 + " '" + Value4 + "' " + AndOr4 + " ";
                                CritQUERY4 = " " + drpCondition4.SelectedItem.Text + " " + txtValue4.Text + " " + AndOr4 + " ";
                            }


                            if (drpAndOr4.SelectedIndex != 0)
                            {
                                Condition5 = drpCondition5.SelectedValue;
                                Value5 = txtValue5.Text;

                                if (Condition5 == "IS NULL")
                                {
                                    QUERY5 = "  [" + FIELDNAME + "] " + Condition5 + " ";
                                    CritQUERY5 = drpCondition5.SelectedItem.Text;
                                }
                                else if (Condition5 == "IS NOT NULL")
                                {
                                    QUERY5 = "  [" + FIELDNAME + "] " + Condition5 + " ";
                                    CritQUERY5 = drpCondition5.SelectedItem.Text;
                                }
                                else if (Condition5 == "[_]%")
                                {
                                    QUERY5 = "  [" + FIELDNAME + "] LIKE '[" + Value5 + "]%' ";
                                    CritQUERY5 = drpCondition5.SelectedItem.Text + " " + txtValue5.Text + "";
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    QUERY5 = "  [" + FIELDNAME + "] NOT LIKE '[" + Value5 + "]%' ";
                                    CritQUERY5 = drpCondition5.SelectedItem.Text + " " + txtValue5.Text + "";
                                }
                                else if (Condition5 == "%_%")
                                {
                                    QUERY5 = "  [" + FIELDNAME + "] LIKE '%" + Value5 + "%' ";
                                    CritQUERY5 = drpCondition5.SelectedItem.Text + " " + txtValue5.Text + "";
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    QUERY5 = "  [" + FIELDNAME + "] NOT LIKE '%" + Value5 + "%' ";
                                    CritQUERY5 = drpCondition5.SelectedItem.Text + " " + txtValue5.Text + "";
                                }
                                else
                                {
                                    QUERY5 = "  [" + FIELDNAME + "] " + Condition5 + " '" + Value5 + "' ";
                                    CritQUERY5 = drpCondition5.SelectedItem.Text + " " + txtValue5.Text + "";
                                }
                            }
                        }
                    }
                }

                QUERY = QUERY1 + " " + QUERY2 + " " + QUERY3 + " " + QUERY4 + " " + QUERY5;

                Criteria = CritQUERY1 + " " + CritQUERY2 + " " + CritQUERY3 + " " + CritQUERY4 + " " + CritQUERY5;

                DataSet ds = dal_DB.DB_LIST_SP(
                 Action: "INSERT_CRITERIA",
                 LISTING_ID: LISTING_ID,
                 FIELDNAME: FIELDNAME,
                 FIELDID: FIELDID,
                 Condition1: Condition1,
                 Value1: Value1,
                 AndOr1: AndOr1,
                 Condition2: Condition2,
                 Value2: Value2,
                 AndOr2: AndOr2,
                 Condition3: Condition3,
                 Value3: Value3,
                 AndOr3: AndOr3,
                 Condition4: Condition4,
                 Value4: Value4,
                 AndOr4: AndOr4,
                 Condition5: Condition5,
                 Value5: Value5,
                 QUERY: QUERY,
                 FORMULA: Criteria,
                 USERID: Session["USER_ID"].ToString()
                 );



                Cancel();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_CRITERIA()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_CRITERIA", LISTING_ID: drpList.SelectedValue, FIELDID: drpField.SelectedValue);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        drpField.SelectedValue = ds.Tables[0].Rows[0]["FIELDID"].ToString();

                        drpCondition1.SelectedValue = ds.Tables[0].Rows[0]["Condition1"].ToString();
                        txtValue1.Text = ds.Tables[0].Rows[0]["Value1"].ToString();
                        drpAndOr1.SelectedValue = ds.Tables[0].Rows[0]["AndOr1"].ToString();

                        drpCondition2.SelectedValue = ds.Tables[0].Rows[0]["Condition2"].ToString();
                        txtValue2.Text = ds.Tables[0].Rows[0]["Value2"].ToString();
                        drpAndOr2.SelectedValue = ds.Tables[0].Rows[0]["AndOr2"].ToString();

                        drpCondition3.SelectedValue = ds.Tables[0].Rows[0]["Condition3"].ToString();
                        txtValue3.Text = ds.Tables[0].Rows[0]["Value3"].ToString();
                        drpAndOr3.SelectedValue = ds.Tables[0].Rows[0]["AndOr3"].ToString();

                        drpCondition4.SelectedValue = ds.Tables[0].Rows[0]["Condition4"].ToString();
                        txtValue4.Text = ds.Tables[0].Rows[0]["Value4"].ToString();
                        drpAndOr4.SelectedValue = ds.Tables[0].Rows[0]["AndOr4"].ToString();

                        drpCondition5.SelectedValue = ds.Tables[0].Rows[0]["Condition5"].ToString();
                        txtValue5.Text = ds.Tables[0].Rows[0]["Value5"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_CRITERIA_ALL()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_CRITERIA_ALL", LISTING_ID: drpList.SelectedValue);
                grdCriterias.DataSource = ds;
                grdCriterias.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_CRITERIA(string ID)
        {
            try
            {

                dal_DB.DB_LIST_SP(
                Action: "DELETE_CRITERIA", USERID: Session["USER_ID"].ToString(),
                ID: ID
                );

                GET_CRITERIA_ALL();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelCrit_Click(object sender, EventArgs e)
        {
            try
            {
                Cancel();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdCriterias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = Convert.ToString(e.CommandArgument);
                Session["LISTCritID"] = ID;
                if (e.CommandName == "DeleteCrit")
                {
                    DELETE_CRITERIA(ID);
                }
                if (e.CommandName == "EditCrit")
                {
                    EDIT_CRITERIA(ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT_CRITERIA(string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(
                Action: "EDIT_CRITERIA", ID: ID);

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpField.SelectedValue = ds.Tables[0].Rows[0]["FIELDID"].ToString();

                    drpCondition1.SelectedValue = ds.Tables[0].Rows[0]["Condition1"].ToString();
                    txtValue1.Text = ds.Tables[0].Rows[0]["Value1"].ToString();
                    drpAndOr1.SelectedValue = ds.Tables[0].Rows[0]["AndOr1"].ToString();

                    drpCondition2.SelectedValue = ds.Tables[0].Rows[0]["Condition2"].ToString();
                    txtValue2.Text = ds.Tables[0].Rows[0]["Value2"].ToString();
                    drpAndOr2.SelectedValue = ds.Tables[0].Rows[0]["AndOr2"].ToString();

                    drpCondition3.SelectedValue = ds.Tables[0].Rows[0]["Condition3"].ToString();
                    txtValue3.Text = ds.Tables[0].Rows[0]["Value3"].ToString();
                    drpAndOr3.SelectedValue = ds.Tables[0].Rows[0]["AndOr3"].ToString();

                    drpCondition4.SelectedValue = ds.Tables[0].Rows[0]["Condition4"].ToString();
                    txtValue4.Text = ds.Tables[0].Rows[0]["Value4"].ToString();
                    drpAndOr4.SelectedValue = ds.Tables[0].Rows[0]["AndOr4"].ToString();

                    drpCondition5.SelectedValue = ds.Tables[0].Rows[0]["Condition5"].ToString();
                    txtValue5.Text = ds.Tables[0].Rows[0]["Value5"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnAddFormula_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpFunction.SelectedIndex != 0)
                {
                    txtFormula.Text = txtFormula.Text + " " + drpFunction.SelectedValue;
                }
                else if (drpFormulaField.SelectedIndex != 0)
                {
                    txtFormula.Text = txtFormula.Text + " (" + drpFormulaField.SelectedValue + ")";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_AddFields()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_AddFields", LISTING_ID: drpList.SelectedValue);
                grdAddFields.DataSource = ds;
                grdAddFields.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void INSERT_AddFields()
        {
            try
            {
                if (txtAddFieldName.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Field Name');</script>");
                    return;
                }

                if (txtFormula.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Formula');</script>");
                    return;
                }

                DataSet ds = dal_DB.DB_LIST_SP(Action: "INSERT_AddFields", LISTING_ID: drpList.SelectedValue, FIELDNAME: txtAddFieldName.Text, FORMULA: txtFormula.Text, USERID: Session["USER_ID"].ToString());

                string dsPID = ds.Tables[0].Rows[0]["ID"].ToString();

                GET_AddFields();
                CANCEL_FIELD();
                BINDMODULE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void UPDATE_AddFields()
        {
            try
            {
                dal_DB.DB_LIST_SP(Action: "UPDATE_AddFields", ID: Session["AddFieldID"].ToString(), FIELDNAME: txtAddFieldName.Text, FORMULA: txtFormula.Text, USERID: Session["USER_ID"].ToString());

                GET_AddFields();
                CANCEL_FIELD();
                BINDMODULE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DELETE_AddFields(string ID)
        {
            try
            {
                dal_DB.DB_LIST_SP(Action: "DELETE_AddFields", USERID: Session["USER_ID"].ToString(), ID: ID);
                GET_AddFields();
                CANCEL_FIELD();
                BINDMODULE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SELECT_AddFields(string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "SELECT_AddFields", ID: ID);
                txtAddFieldName.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                txtFormula.Text = ds.Tables[0].Rows[0]["FORMULA"].ToString();
                btnUpdateField.Visible = true;
                btnAddField.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void CANCEL_FIELD()
        {
            try
            {
                txtAddFieldName.Text = "";
                txtFormula.Text = "";
                drpFunction.SelectedIndex = 0;
                drpFormulaField.SelectedIndex = 0;
                btnUpdateField.Visible = false;
                btnAddField.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdAddFields_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "EditField")
                {
                    Session["AddFieldID"] = id;
                    SELECT_AddFields(id);
                }
                else if (e.CommandName == "DeleteField")
                {
                    DELETE_AddFields(id);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnAddField_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_AddFields();
                GET_ACM_Modules();
                CANCEL_FIELD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateField_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_AddFields();
                CANCEL_FIELD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelField_Click(object sender, EventArgs e)
        {
            try
            {
                CANCEL_FIELD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlOnClickField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "SELECT_OnClick", FIELDNAME: ddlOnClickField.SelectedValue, LISTING_ID: drpList.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlOnClickListing.SelectedValue = ds.Tables[0].Rows[0]["OnClickListing"].ToString();
                    GET_OnClickFilters();
                    ddlOnClickFilter.SelectedValue = ds.Tables[0].Rows[0]["OnClickFilter"].ToString();
                }
                else
                {

                    ddlOnClickListing.SelectedIndex = 0;
                    ddlOnClickFilter.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_OnClick_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlOnClickField.SelectedValue != "0")
                {
                    dal_DB.DB_LIST_SP(Action: "SET_OnClick", FIELDNAME: ddlOnClickField.SelectedValue, LISTING_ID: drpList.SelectedValue, OnClickListing: ddlOnClickListing.SelectedValue, OnClickFilter: ddlOnClickFilter.SelectedValue);
                }
                else
                {
                    dal_DB.DB_LIST_SP(Action: "Remove_OnClick", FIELDNAME: ddlOnClickField.SelectedValue, LISTING_ID: drpList.SelectedValue);
                }

                GET_OnClicks();
                GET_ON_CLICK();
                ddlOnClickListing.SelectedIndex = 0;
                ddlOnClickFilter.SelectedIndex = 0;

                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "alert('OnClick Event Set Successfully.');", true);


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_OnClick_Click(object sender, EventArgs e)
        {
            try
            {
                ddlOnClickField.SelectedIndex = 0;
                ddlOnClickListing.SelectedIndex = 0;
                ddlOnClickFilter.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlOtherListingField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_Other_Listings();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_Other_Listings()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "SELECT_Other_Listings", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["USER_ID"].ToString(), LISTING_ID: drpList.SelectedValue, FIELDNAME: ddlOtherListingField.SelectedValue);

                gvNew.DataSource = ds.Tables[0];
                gvNew.DataBind();

                gvAdded.DataSource = ds.Tables[1];
                gvAdded.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvNew.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvNew.Rows[i].FindControl("Chk_Sel_Fun");
                    DropDownList ddlFilter = (DropDownList)gvNew.Rows[i].FindControl("ddlFilter");

                    if (ChAction.Checked && ddlFilter.SelectedIndex != 0)
                    {
                        Label lblID = (Label)gvNew.Rows[i].FindControl("lblID");

                        dal_DB.DB_LIST_SP(Action: "SET_Other_Listings", FIELDNAME: ddlOtherListingField.SelectedValue, LISTING_ID: drpList.SelectedValue, Other_Listings: lblID.Text, OnClickFilter: ddlFilter.SelectedValue);

                    }
                    else if (ChAction.Checked && ddlFilter.SelectedIndex == 0)
                    {
                        Label lblID = (Label)gvNew.Rows[i].FindControl("lblID");

                        dal_DB.DB_LIST_SP(Action: "SET_Other_Listings", FIELDNAME: ddlOtherListingField.SelectedValue, LISTING_ID: drpList.SelectedValue, Other_Listings: lblID.Text);
                    }
                }

                GET_Other_Listings();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvAdded.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAdded.Rows[i].FindControl("Chk_Sel_Fun");

                    Label lblID = (Label)gvAdded.Rows[i].FindControl("lblID");

                    if (ChAction.Checked)
                    {
                        dal_DB.DB_LIST_SP(Action: "REMOVE_Other_Listings", FIELDNAME: ddlOtherListingField.SelectedValue, LISTING_ID: drpList.SelectedValue, Other_Listings: lblID.Text);
                    }
                }

                GET_Other_Listings();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlOnClickListing_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_OnClickFilters();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_OnClickFilters()
        {
            try
            {
                DataSet ds1 = dal_DB.DB_LIST_SP(Action: "GET_OnClickFilters", LISTING_ID: ddlOnClickListing.SelectedValue);
                ddlOnClickFilter.DataSource = ds1.Tables[0];
                ddlOnClickFilter.DataValueField = "VALUE";
                ddlOnClickFilter.DataTextField = "FIELDNAME";
                ddlOnClickFilter.DataBind();
                ddlOnClickFilter.Items.Insert(0, new ListItem("-Select-", "0"));
                ddlOnClickFilter.Items.Insert(1, new ListItem("Subject", "DM_PV.SUBJID"));
                ddlOnClickFilter.Items.Insert(2, new ListItem("VISIT", "DM_VISITDETAILS.VISIT"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string ID = dr["ID"].ToString();

                    DropDownList ddlFilter = (DropDownList)e.Row.FindControl("ddlFilter");

                    DataSet ds1 = dal_DB.DB_LIST_SP(Action: "GET_OnClickFilters", LISTING_ID: ID);
                    ddlFilter.DataSource = ds1.Tables[0];
                    ddlFilter.DataValueField = "VALUE";
                    ddlFilter.DataTextField = "FIELDNAME";
                    ddlFilter.DataBind();
                    ddlFilter.Items.Insert(0, new ListItem("-Select-", "0"));
                    ddlFilter.Items.Insert(1, new ListItem("Subject", "DM_PV.SUBJID"));
                    ddlFilter.Items.Insert(2, new ListItem("VISIT", "DM_VISITDETAILS.VISIT"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SET_PRIMARY_DETAILS()
        {
            try
            {
                for (int i = 0; i < grdPriamryFields.Rows.Count; i++)
                {
                    CheckBox Chk_Sel_Fun = (CheckBox)grdPriamryFields.Rows[i].FindControl("Chk_Sel_Fun");
                    if (Chk_Sel_Fun.Checked == true)
                    {
                        string FieldID = ((Label)grdPriamryFields.Rows[i].FindControl("lblID")).Text;

                        dal_DB.DB_LIST_SP(Action: "SET_PRIMARY_DETAILS",
                        LISTING_ID: drpList.SelectedValue,
                        FIELDID: FieldID
                        );
                    }
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void REMOVE_PRIMARY_DETAILS()
        {
            try
            {
                for (int i = 0; i < grdAddedPriamryDetails.Rows.Count; i++)
                {
                    CheckBox Chk_Sel_Fun = (CheckBox)grdAddedPriamryDetails.Rows[i].FindControl("Chk_Sel_Fun");
                    if (Chk_Sel_Fun.Checked == true)
                    {
                        string FieldID = ((Label)grdAddedPriamryDetails.Rows[i].FindControl("lblID")).Text;

                        dal_DB.DB_LIST_SP(Action: "REMOVE_PRIMARY_DETAILS",
                        LISTING_ID: drpList.SelectedValue,
                        FIELDID: FieldID
                        );
                    }
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_PRIM_MODULE()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_PRIM_MODULE", LISTING_ID: drpList.SelectedValue);

                ddlPrimModule.DataSource = ds.Tables[0];
                ddlPrimModule.DataValueField = "MODULEID";
                ddlPrimModule.DataTextField = "MODULENAME";
                ddlPrimModule.DataBind();
                ddlPrimModule.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_PRIM_FIELDS()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_PRIM_FIELDS", LISTING_ID: drpList.SelectedValue, MODULEID: ddlPrimModule.SelectedValue);

                grdPriamryFields.DataSource = ds;
                grdPriamryFields.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ADDED_PRIM_FIELDS()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_ADDED_PRIM_FIELDS", LISTING_ID: drpList.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdAddedPriamryDetails.DataSource = ds;
                    grdAddedPriamryDetails.DataBind();

                    divColor.Visible = true;
                }
                else
                {
                    grdAddedPriamryDetails.DataSource = null;
                    grdAddedPriamryDetails.DataBind();
                    divColor.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_PRIMARY_DETAILS_MODULE()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_PRIMARY_DETAILS_MODULE", LISTING_ID: drpList.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["PrimaryMODULE"].ToString() != "") { ddlPrimModule.SelectedValue = ds.Tables[0].Rows[0]["PrimaryMODULE"].ToString(); }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //private void GET_PRIMARY_DETAILS_FIELDS()
        //{
        //    try
        //    {
        //        DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_PRIMARY_DETAILS_FIELDS", LISTING_ID: drpList.SelectedValue, MODULEID: ddlPrimModule.SelectedValue);

        //        lstPrimFields.ClearSelection();
        //        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                string temp = ds.Tables[0].Rows[i]["ID"].ToString();
        //                if (temp != "")
        //                {
        //                    ListItem itm = lstPrimFields.Items.FindByValue(temp);
        //                    if (itm != null)
        //                        itm.Selected = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            lstPrimFields.ClearSelection();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        protected void ddlPrimModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_PRIM_FIELDS();
                GET_ADDED_PRIM_FIELDS();
                //GET_PRIMARY_DETAILS_FIELDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddPrimaryFields_Click(object sender, EventArgs e)
        {
            try
            {
                SET_PRIMARY_DETAILS();
                GET_PRIM_FIELDS();
                GET_ADDED_PRIM_FIELDS();
                GET_PRIMARY_COLOR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemovePrimaryFields_Click(object sender, EventArgs e)
        {
            try
            {
                REMOVE_PRIMARY_DETAILS();
                GET_PRIM_FIELDS();
                GET_ADDED_PRIM_FIELDS();
                GET_PRIMARY_COLOR();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSetPrimColor_Click(object sender, EventArgs e)
        {
            try
            {
                SET_PRIMARY_COLOR();

                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "alert('Color Code Set Successfully.');", true);

                GET_PRIMARY_COLOR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvPrimColor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    Label lblOptions = (Label)e.Row.FindControl("lblOptions");
                    DropDownList drpColorCondition1 = (DropDownList)e.Row.FindControl("drpColorCondition1");
                    DropDownList drpColorOr = (DropDownList)e.Row.FindControl("drpColorOr");
                    DropDownList drpColorCondition2 = (DropDownList)e.Row.FindControl("drpColorCondition2");
                    HiddenField hfPrimColor = (HiddenField)e.Row.FindControl("hfPrimColor");

                    string ID = drv["ID"].ToString();
                    string Condition1 = drv["Condition1"].ToString();
                    string Or = drv["Or"].ToString();
                    string Condition2 = drv["Condition2"].ToString();
                    string Color = drv["Color"].ToString();

                    if (Condition1 != "")
                    {
                        drpColorCondition1.SelectedValue = Condition1;
                    }

                    if (Or != "")
                    {
                        drpColorOr.SelectedValue = Or;
                    }

                    if (Condition2 != "")
                    {
                        drpColorCondition2.SelectedValue = Condition2;
                    }

                    if (Color != "")
                    {
                        hfPrimColor.Value = Color;
                    }
                    else
                    {
                        hfPrimColor.Value = DefaultColorValue;
                    }

                    DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_VALUE", LISTING_ID: drpList.SelectedValue, FIELDID: ID);

                    DataTable dt = ds.Tables[0];
                    string Values = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Values += "" + dt.Rows[i]["Value"].ToString() + ",";
                    }

                    lblOptions.Text = Values.TrimEnd(',');

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_PRIMARY_COLOR()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(
                    Action: "GET_PRIMARY_COLOR",
                    LISTING_ID: drpList.SelectedValue
                    );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvPrimColor.DataSource = ds;
                    gvPrimColor.DataBind();

                    btnSetPrimColor.Visible = true;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindColorValues();", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindColorBox();", true);

                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SET_PRIMARY_COLOR()
        {
            try
            {
                for (int i = 0; i < gvPrimColor.Rows.Count; i++)
                {
                    string Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null, Value2 = null, FORMULA = null;

                    Label lblID = (Label)gvPrimColor.Rows[i].FindControl("lblID");
                    DropDownList drpColorCondition1 = (DropDownList)gvPrimColor.Rows[i].FindControl("drpColorCondition1");
                    TextBox txtAns1 = (TextBox)gvPrimColor.Rows[i].FindControl("txtAns1");
                    DropDownList drpColorOr = (DropDownList)gvPrimColor.Rows[i].FindControl("drpColorOr");
                    DropDownList drpColorCondition2 = (DropDownList)gvPrimColor.Rows[i].FindControl("drpColorCondition2");
                    TextBox txtAns2 = (TextBox)gvPrimColor.Rows[i].FindControl("txtAns2");
                    HiddenField hfPrimColor = (HiddenField)gvPrimColor.Rows[i].FindControl("hfPrimColor");

                    Condition1 = drpColorCondition1.SelectedValue;
                    Value1 = txtAns1.Text;
                    if (drpColorOr.SelectedIndex != 0)
                    {
                        AndOr1 = drpColorOr.SelectedValue;
                        Condition2 = drpColorCondition2.SelectedValue;
                        Value2 = txtAns2.Text;
                    }

                    FORMULA = hfPrimColor.Value;

                    dal_DB.DB_LIST_SP(
                            Action: "SET_PRIMARY_COLOR",
                            LISTING_ID: drpList.SelectedValue,
                            FIELDID: lblID.Text,
                            Condition1: Condition1,
                            Value1: Value1,
                            AndOr1: AndOr1,
                            Condition2: Condition2,
                            Value2: Value2,
                            FORMULA: FORMULA
                   );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpACLTgList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_Target_Fields();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitACL_OnClick_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_ACL_Crit();
                GET_ARC_LISTINGS();
                CLEAR_ACL_Crit();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelACL_OnClick_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_ACL_Crit();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_Target_Fields()
        {
            try
            {
                DataSet ds1 = dal_DB.DB_LIST_SP(Action: "GET_FIELDS", LISTING_ID: drpACLTgList.SelectedValue);
                drpACLTgListField.DataSource = ds1.Tables[0];
                drpACLTgListField.DataValueField = "ID";
                drpACLTgListField.DataTextField = "FIELDNAME";
                drpACLTgListField.DataBind();
                drpACLTgListField.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_ACL_Crit()
        {
            try
            {
                drpACLFields.SelectedIndex = 0;
                drpACLCondition.SelectedIndex = 0;
                drpACLTgList.SelectedIndex = 0;
                drpACLTgListField.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_ACL_Crit()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(
                   Action: "INSERT_ACL_Crit",
                   LISTING_ID: drpList.SelectedValue,
                   FIELDID: drpACLFields.SelectedValue,
                   FIELDNAME: drpACLFields.SelectedItem.Text,
                   Condition1: drpACLCondition.SelectedValue,
                   TG_LISTING_ID: drpACLTgList.SelectedValue,
                   TG_FIELDID: drpACLTgListField.SelectedValue,
                   TG_FIELDNAME: drpACLTgListField.SelectedItem.Text,
                   USERID: Session["USER_ID"].ToString()

                       );

                string dsPID = ds.Tables[0].Rows[0]["ID"].ToString();


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpACLFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SELECT_ACL_Crit();
                GET_ARC_LISTINGS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_ACL_Crit()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "SELECT_ACL_Crit", LISTING_ID: drpList.SelectedValue, FIELDID: drpACLFields.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpACLCondition.SelectedValue = ds.Tables[0].Rows[0]["CONDITION"].ToString();
                    drpACLTgList.SelectedValue = ds.Tables[0].Rows[0]["TG_LISTING_ID"].ToString();
                    GET_Target_Fields();
                    drpACLTgListField.SelectedValue = ds.Tables[0].Rows[0]["TG_FIELDID"].ToString();
                }
                else
                {
                    drpACLCondition.SelectedIndex = 0;
                    drpACLTgList.SelectedIndex = 0;
                    drpACLTgListField.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_ACM_Crit()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(
                   Action: "INSERT_ACM_Crit",
                   LISTING_ID: drpList.SelectedValue,
                   AndOr1: ddlListModules1.SelectedValue,
                   AndOr2: ddlListModules2.SelectedValue,
                   Value1: ddlListField1.SelectedValue,
                   Value2: ddlListField2.SelectedValue,
                   Condition1: ddlACM_Condition.SelectedValue,
                   Condition2: ddlACM_Condition.SelectedItem.Text,
                   USERID: Session["USER_ID"].ToString()
                   );

                string dsPID = ds.Tables[0].Rows[0]["ID"].ToString();


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_ACM_Crit()
        {
            try
            {
                dal_DB.DB_LIST_SP(
                Action: "UPDATE_ACM_Crit",
                LISTING_ID: drpList.SelectedValue,
                ID: ViewState["ACM_ID"].ToString(),
                AndOr1: ddlListModules1.SelectedValue,
                AndOr2: ddlListModules2.SelectedValue,
                Value1: ddlListField1.SelectedValue,
                Value2: ddlListField2.SelectedValue,
                Condition1: ddlACM_Condition.SelectedValue,
                Condition2: ddlACM_Condition.SelectedItem.Text,
                USERID: Session["USER_ID"].ToString()
                );


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_ACM_Crit(string ID)
        {
            try
            {


                dal_DB.DB_LIST_SP(
                Action: "DELETE_ACM_Crit", USERID: Session["USER_ID"].ToString(),
                ID: ID
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_ACM_Crit()
        {
            try
            {
                GET_ACM_Modules();
                GET_ACM_Fields(ddlListField1, ddlListModules1.SelectedValue);
                GET_ACM_Fields(ddlListField2, ddlListModules2.SelectedValue);
                ddlACM_Condition.SelectedIndex = 0;

                btnSubmitACM.Visible = true;
                btnUpdateACM.Visible = false;

                GET_ACM_Crit();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_ACM_Crit(string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "SELECT_ACM_Crit", ID: ID);

                ViewState["ACM_ID"] = ID;

                GET_ACM_Modules();
                ddlListModules1.SelectedValue = ds.Tables[0].Rows[0]["MODULEID1"].ToString();
                GET_ACM_Fields(ddlListField1, ddlListModules1.SelectedValue);
                ddlListField1.SelectedValue = ds.Tables[0].Rows[0]["FIELDID1"].ToString();

                ddlListModules2.SelectedValue = ds.Tables[0].Rows[0]["MODULEID2"].ToString();
                GET_ACM_Fields(ddlListField2, ddlListModules2.SelectedValue);
                ddlListField2.SelectedValue = ds.Tables[0].Rows[0]["FIELDID2"].ToString();

                ddlACM_Condition.SelectedValue = ds.Tables[0].Rows[0]["CONDITION"].ToString();

                btnSubmitACM.Visible = false;
                btnUpdateACM.Visible = true;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ACM_Crit()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_ACM_Crit", LISTING_ID: drpList.SelectedValue);
                grdACM.DataSource = ds;
                grdACM.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ARC_LISTINGS()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_ARC_LISTINGS", LISTING_ID: drpList.SelectedValue);
                grdArcListing.DataSource = ds;
                grdArcListing.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ON_CLICK()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_ON_CLICK", LISTING_ID: drpList.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdonClickEvent.DataSource = ds;
                    grdonClickEvent.DataBind();
                    //DivClickEventRecords.Visible = true;
                }
                else
                {
                    grdonClickEvent.DataSource = null;
                    grdonClickEvent.DataBind();
                    //DivClickEventRecords.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ACM_Modules()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_ACM_Modules", LISTING_ID: drpList.SelectedValue);

                ddlListModules1.DataSource = ds;
                ddlListModules1.DataValueField = "MODULEID";
                ddlListModules1.DataTextField = "MODULENAME";
                ddlListModules1.DataBind();
                ddlListModules1.Items.Insert(0, new ListItem("-Select-", "0"));

                ddlListModules2.DataSource = ds;
                ddlListModules2.DataValueField = "MODULEID";
                ddlListModules2.DataTextField = "MODULENAME";
                ddlListModules2.DataBind();
                ddlListModules2.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ACM_Fields(DropDownList ddl, string MODULEID)
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GET_ACM_Fields", LISTING_ID: drpList.SelectedValue, MODULEID: MODULEID);

                ddl.DataSource = ds;
                ddl.DataValueField = "FIELD_ID";
                ddl.DataTextField = "FIELDNAME";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlListModules1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_ACM_Fields(ddlListField1, ddlListModules1.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlListModules2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_ACM_Fields(ddlListField2, ddlListModules2.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitACM_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_ACM_Crit();
                CLEAR_ACM_Crit();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelACM_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_ACM_Crit();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateACM_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_ACM_Crit();
                CLEAR_ACM_Crit();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdACM_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "EditCrit")
                {
                    ViewState["ACM_ID"] = id;
                    SELECT_ACM_Crit(id);
                }
                else if (e.CommandName == "DeleteCrit")
                {
                    DELETE_ACM_Crit(id);
                    GET_ACM_Crit();
                    CLEAR_ACL_Crit();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdArcListing_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "EditArcListing")
                {
                    ViewState["ACM_ID"] = id;
                    SELECT_ARC_LISTINGS(id);
                }
                else if (e.CommandName == "DeleteACL")
                {
                    DELETE_ARC_Crit(id);
                    GET_ARC_LISTINGS();
                    CLEAR_ACL_Crit();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }
        private void SELECT_ARC_LISTINGS(string id)
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "SELECT_ARC_LISTINGS", ID: id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpACLFields.SelectedValue = ds.Tables[0].Rows[0]["FIELDID"].ToString();
                    drpACLCondition.SelectedValue = ds.Tables[0].Rows[0]["CONDITION"].ToString();
                    drpACLTgList.SelectedValue = ds.Tables[0].Rows[0]["TG_LISTING_ID"].ToString();
                    GET_Target_Fields();
                    drpACLTgListField.SelectedValue = ds.Tables[0].Rows[0]["TG_FIELDID"].ToString();
                }
                else
                {
                    drpACLFields.SelectedIndex = 0;
                    drpACLCondition.SelectedIndex = 0;
                    drpACLTgList.SelectedIndex = 0;
                    drpACLTgListField.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void DELETE_ARC_Crit(string id)
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "DELETE_ARC_LISTINGS", ID: id);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdonClickEvent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string FIELDNAME = e.CommandArgument.ToString();
                if (e.CommandName == "EditOnClick")
                {
                    EDIT_ONCLICK(FIELDNAME);
                }
                else if (e.CommandName == "DeleteOnClick")
                {
                    string id = e.CommandArgument.ToString();

                    DeleteOnClick(id);
                    GET_ON_CLICK();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DeleteOnClick(string id)
        {
            try
            {
                foreach (GridViewRow row in grdonClickEvent.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        HiddenField hdnID = (HiddenField)row.FindControl("hdnID");
                        string ID = hdnID.Value;
                        Label FIELDNAME = (Label)row.FindControl("lblFIELDNAME");
                        if (id == ID)
                        {
                            DataSet ds = dal_DB.DB_LIST_SP(Action: "DELETE_OnClick", LISTING_ID: drpList.SelectedValue, ID: id, FIELDNAME: FIELDNAME.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT_ONCLICK(string FIELDNAME)
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "SELECT_OnClick", FIELDNAME: FIELDNAME, LISTING_ID: drpList.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlOnClickField.SelectedValue = ds.Tables[0].Rows[0]["ID"].ToString();
                    ddlOnClickListing.SelectedValue = ds.Tables[0].Rows[0]["OnClickListing"].ToString();
                    GET_OnClickFilters();
                    ddlOnClickFilter.SelectedValue = ds.Tables[0].Rows[0]["OnClickFilter"].ToString();
                }
                else
                {
                    ddlOnClickField.SelectedIndex = 0;
                    ddlOnClickListing.SelectedIndex = 0;
                    ddlOnClickFilter.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}