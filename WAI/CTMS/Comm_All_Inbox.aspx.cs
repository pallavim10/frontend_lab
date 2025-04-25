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
    public partial class Comm_All_Inbox : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetListing();
                    GETCOMMRULES();
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
                DataSet ds = dal.DM_LISTINGS_SP(Action: "COMM_GETLISTING", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdListing.DataSource = ds;
                    grdListing.DataBind();

                    ddlFoldername.DataSource = ds;
                    ddlFoldername.DataValueField = "ID";
                    ddlFoldername.DataTextField = "LISTNAME";
                    ddlFoldername.DataBind();
                    ddlFoldername.Items.Insert(0, new ListItem("None", "None"));
                }
                else
                {
                    grdListing.DataSource = null;
                    grdListing.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnAddListing_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(
                Action: "COMM_InsertListing",
                PROJECTID: Session["PROJECTID"].ToString(),
                USERID: Session["User_ID"].ToString(),
                LISTING_NAME: txtListing.Text,
                PARENT: txtParent.Text,
                SEQNO: txtLISTSEQNO.Text,
                MASTERDB: Session["InitialCatalog"].ToString()
                );

                txtListing.Text = "";
                txtLISTSEQNO.Text = "";

                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("Communication");
                GetListing();

                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "ChangeDivQuery();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnUpdateListing_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds1 = dal.DM_LISTINGS_SP(
                Action: "COMM_Delete_Parent_Listing",
                PROJECTID: Session["PROJECTID"].ToString(),
                USERID: Session["User_ID"].ToString(),
                LISTING_NAME: txtListing.Text,
                ID: ViewState["COMM_LISTID"].ToString(),
                SEQNO: txtLISTSEQNO.Text,
                MASTERDB: Session["InitialCatalog"].ToString()
                );

                DataSet ds = dal.DM_LISTINGS_SP(
                Action: "COMM_UpdateListing",
                PROJECTID: Session["PROJECTID"].ToString(),
                USERID: Session["User_ID"].ToString(),
                LISTING_NAME: txtListing.Text,
                ID: ViewState["COMM_LISTID"].ToString(),
                MASTERDB: Session["InitialCatalog"].ToString(),
                SEQNO: txtLISTSEQNO.Text,
                PARENT: txtParent.Text
                );

                txtListing.Text = "";
                txtLISTSEQNO.Text = "";
                btnAddListing.Visible = true;
                btnUpdateListing.Visible = false;

                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("Communication");
                GetListing();

                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "ChangeDivQuery();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdListing_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = Convert.ToString(e.CommandArgument);
                ViewState["COMM_LISTID"] = ID;
                if (e.CommandName == "EditList")
                {
                    EditList(ID);
                }
                else if (e.CommandName == "DeleteList")
                {
                    DeleteList();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdListing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string COUNTS = dr["COUNTS"].ToString();
                    LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");

                    if (COUNTS == "0")
                    {
                        lbtndeleteSection.Visible = true;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void EditList(string ID)
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(Action: "COMM_GETLISTING_BYID", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString(), ID: ID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtListing.Text = ds.Tables[0].Rows[0]["LISTNAME"].ToString();
                    txtParent.Text = ds.Tables[0].Rows[0]["PARENTLIST_NAME"].ToString();
                    txtLISTSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "ChangeDivQuery();", true);

                }
                btnAddListing.Visible = false;
                btnUpdateListing.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DeleteList()
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(
                Action: "COMM_DeleteList",
                ID: ViewState["COMM_LISTID"].ToString(),
                MASTERDB: Session["InitialCatalog"].ToString(),
                PROJECTID: Session["PROJECTID"].ToString()
                );

                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("Communication");
                GetListing();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETCOMMRULES()
        {
            try
            {
                DataSet ds = dal.Communication_SP(
                Action: "COMM_GET_OnSubmit_CRIT",
                PROJECTID: Session["PROJECTID"].ToString(),
                UserID: Session["User_ID"].ToString()
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdStepCrits.DataSource = ds;
                    grdStepCrits.DataBind();
                }
                else
                {
                    grdStepCrits.DataSource = null;
                    grdStepCrits.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnRuleSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlFoldername.SelectedItem.Text != "None" || ddlFlag.SelectedItem.Text != "None")
                {
                    INSERT_CRIT();
                    GETCOMMRULES();
                    CLEAR_RULES();
                    //DataSet ds = dal.Communication_SP(Action: "INSERT_RULES",
                    //    PROJECTID: Session["PROJECTID"].ToString(),
                    //    UserID: Session["User_ID"].ToString(),
                    //    FromID: txtrulefrom.Text,
                    //    COND1: ddlSymbol1.SelectedItem.Text,
                    //    ToID: txtruleTo.Text,
                    //    COND2: ddlSymbol2.SelectedItem.Text,
                    //    CcID: txtruleCC.Text,
                    //    COND3: ddlSymbol3.SelectedItem.Text,
                    //    BccID: txtruleBCC.Text,
                    //    COND4: ddlSymbol4.SelectedItem.Text,
                    //    Subject: txtruleSubject.Text,
                    //    FOLDERNAME: ddlFoldername.SelectedItem.Text,
                    //    Importance: ddlFlag.SelectedValue
                    //    );


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please set atleast one Rule')", true);
                }
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
                FIELDNAME6 = drpLISTField6.SelectedValue,
                CritName = txtCritName.Text,
                SEQNO = txtSEQNO.Text,
            Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
            Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
            Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null, AndOr5 = null, Condition6 = null, Value6 = null,
            CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null, CritQUERY6 = null,
            CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null, CritCodeQUERY6 = null;


                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;
                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    AndOr1 = drpLISTAndOr1.SelectedItem.Text;
                }

                if (Condition1 == "IS NULL")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
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
                    Value2 = txtLISTValue2.Text;
                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        AndOr2 = drpLISTAndOr2.SelectedItem.Text;
                    }

                    if (Condition2 == "IS NULL")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
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
                        Value3 = txtLISTValue3.Text;
                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            AndOr3 = drpLISTAndOr3.SelectedItem.Text;
                        }

                        if (Condition3 == "IS NULL")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
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
                            Value4 = txtLISTValue4.Text;
                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                AndOr4 = drpLISTAndOr4.SelectedItem.Text;
                            }

                            if (Condition4 == "IS NULL")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
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
                                Value5 = txtLISTValue5.Text;

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

                            if (drpLISTAndOr5.SelectedIndex != 0)
                            {
                                Condition6 = drpLISTCondition6.SelectedValue;
                                Value6 = txtLISTValue6.Text;

                                if (Condition5 == "IS NULL")
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] " + Condition6 + " ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " ";
                                }
                                else if (Condition5 == "IS NOT NULL")
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] " + Condition6 + " ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " ";
                                }
                                else if (Condition5 == "[_]%")
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] LIKE '[" + Value6 + "]%' ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " " + Value6;
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] NOT LIKE '[" + Value6 + "]%' ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " " + Value6;
                                }
                                else if (Condition5 == "%_%")
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] LIKE '%" + Value6 + "%' ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " " + Value6;
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME6 + "] NOT LIKE '%" + Value6 + "%' ";
                                    CritQUERY6 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " " + Value6;
                                }
                                else
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] " + Condition6 + " '" + Value6 + "' ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " " + Value6;
                                }
                            }
                        }
                    }
                }


                CritCodeQUERY = CritCodeQUERY1 + " " + CritCodeQUERY2 + " " + CritCodeQUERY3 + " " + CritCodeQUERY4 + " " + CritCodeQUERY5 + "" + CritCodeQUERY6;

                CritQUERY = CritQUERY1 + " " + CritQUERY2 + " " + CritQUERY3 + " " + CritQUERY4 + " " + CritQUERY5 + " " + CritQUERY6;

                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(
                                ACTION: "COMM_INSERT_OnSubmit_CRIT",
                                SEQNO: SEQNO,
                                CritName: CritName,
                                Criteria: CritQUERY,
                                CritCode: CritCodeQUERY,

                                Field1: drpLISTField1.SelectedValue,
                                CONDITION1: drpLISTCondition1.SelectedValue,
                                Value1: txtLISTValue1.Text,
                                AndOr1: drpLISTAndOr1.SelectedValue,

                                Field2: drpLISTField2.SelectedValue,
                                Condition2: drpLISTCondition2.SelectedValue,
                                VALUE2: txtLISTValue2.Text,
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
                                AndOr5: drpLISTAndOr5.SelectedValue,

                                Field6: drpLISTField6.SelectedValue,
                                Condition6: drpLISTCondition6.SelectedValue,
                                Value6: txtLISTValue6.Text,

                                FIELDNAME: ddlFoldername.SelectedValue,
                                FormCode: ddlFlag.SelectedValue
                                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnRuleUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlFoldername.SelectedItem.Text != "None" || ddlFlag.SelectedItem.Text != "None")
                {
                    UPDATE_CRIT();
                    GETCOMMRULES();
                    CLEAR_RULES();
                    //DataSet ds = dal.Communication_SP(Action: "INSERT_RULES",
                    //    PROJECTID: Session["PROJECTID"].ToString(),
                    //    UserID: Session["User_ID"].ToString(),
                    //    FromID: txtrulefrom.Text,
                    //    COND1: ddlSymbol1.SelectedItem.Text,
                    //    ToID: txtruleTo.Text,
                    //    COND2: ddlSymbol2.SelectedItem.Text,
                    //    CcID: txtruleCC.Text,
                    //    COND3: ddlSymbol3.SelectedItem.Text,
                    //    BccID: txtruleBCC.Text,
                    //    COND4: ddlSymbol4.SelectedItem.Text,
                    //    Subject: txtruleSubject.Text,
                    //    FOLDERNAME: ddlFoldername.SelectedItem.Text,
                    //    Importance: ddlFlag.SelectedValue
                    //    );
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please set atleast one Rule')", true);
                }
                //if (txtrulefrom.Text != "" || txtruleTo.Text != "" || txtruleCC.Text != "" || txtruleBCC.Text != "" || txtruleSubject.Text != "")
                //{
                //    DataSet ds = dal.Communication_SP(Action: "UPDATE_RULES",
                //       FromID: txtrulefrom.Text,
                //        COND1: ddlSymbol1.SelectedItem.Text,
                //        ToID: txtruleTo.Text,
                //        COND2: ddlSymbol2.SelectedItem.Text,
                //        CcID: txtruleCC.Text,
                //        COND3: ddlSymbol3.SelectedItem.Text,
                //        BccID: txtruleBCC.Text,
                //        COND4: ddlSymbol4.SelectedItem.Text,
                //        Subject: txtruleSubject.Text,
                //        FOLDERNAME: ddlFoldername.SelectedItem.Text,
                //        Importance: ddlFlag.SelectedValue,
                //        ID: ViewState["COMM_RULEID"].ToString()
                //       );

                //    btnRuleSubmit.Visible = true;
                //    btnRuleUpdate.Visible = false;

                //    CLEAR_RULES();
                //    GETCOMMRULES();
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please set atleast one Rule')", true);
                //}

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
                FIELDNAME6 = drpLISTField6.SelectedValue,
                CritName = txtCritName.Text,
                SEQNO = txtSEQNO.Text,
            Condition1 = null, Value1 = null, AndOr1 = null, Condition2 = null,
            Value2 = null, AndOr2 = null, Condition3 = null, Value3 = null, AndOr3 = null,
            Condition4 = null, Value4 = null, AndOr4 = null, Condition5 = null, Value5 = null, AndOr5 = null, Condition6 = null, Value6 = null,
            CritQUERY = null, CritQUERY1 = null, CritQUERY2 = null, CritQUERY3 = null, CritQUERY4 = null, CritQUERY5 = null, CritQUERY6 = null,
            CritCodeQUERY = null, CritCodeQUERY1 = null, CritCodeQUERY2 = null, CritCodeQUERY3 = null, CritCodeQUERY4 = null, CritCodeQUERY5 = null, CritCodeQUERY6 = null;

                Condition1 = drpLISTCondition1.SelectedValue;
                Value1 = txtLISTValue1.Text;
                if (drpLISTAndOr1.SelectedIndex != 0)
                {
                    AndOr1 = drpLISTAndOr1.SelectedItem.Text;
                }

                if (Condition1 == "IS NULL")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
                }
                else if (Condition1 == "IS NOT NULL")
                {
                    CritCodeQUERY1 = "  [" + FIELDNAME1 + "] " + Condition1 + " ";
                    CritQUERY1 = "  " + drpLISTField1.SelectedItem.Text + " " + drpLISTCondition1.SelectedItem.Text + " ";
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
                    Value2 = txtLISTValue2.Text;
                    if (drpLISTAndOr2.SelectedIndex != 0)
                    {
                        AndOr2 = drpLISTAndOr2.SelectedItem.Text;
                    }

                    if (Condition2 == "IS NULL")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
                    }
                    else if (Condition2 == "IS NOT NULL")
                    {
                        CritCodeQUERY2 = "  [" + FIELDNAME2 + "] " + Condition2 + " ";
                        CritQUERY2 = "  " + drpLISTField2.SelectedItem.Text + " " + drpLISTCondition2.SelectedItem.Text + " ";
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
                        Value3 = txtLISTValue3.Text;
                        if (drpLISTAndOr3.SelectedIndex != 0)
                        {
                            AndOr3 = drpLISTAndOr3.SelectedItem.Text;
                        }

                        if (Condition3 == "IS NULL")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
                        }
                        else if (Condition3 == "IS NOT NULL")
                        {
                            CritCodeQUERY3 = "  [" + FIELDNAME3 + "] " + Condition3 + " ";
                            CritQUERY3 = "  " + drpLISTField3.SelectedItem.Text + " " + drpLISTCondition3.SelectedItem.Text + " ";
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
                            Value4 = txtLISTValue4.Text;
                            if (drpLISTAndOr4.SelectedIndex != 0)
                            {
                                AndOr4 = drpLISTAndOr4.SelectedItem.Text;
                            }

                            if (Condition4 == "IS NULL")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
                            }
                            else if (Condition4 == "IS NOT NULL")
                            {
                                CritCodeQUERY4 = "  [" + FIELDNAME4 + "] " + Condition4 + " ";
                                CritQUERY4 = "  " + drpLISTField4.SelectedItem.Text + " " + drpLISTCondition4.SelectedItem.Text + " ";
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
                                Value5 = txtLISTValue5.Text;

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

                            if (drpLISTAndOr5.SelectedIndex != 0)
                            {
                                Condition6 = drpLISTCondition6.SelectedValue;
                                Value6 = txtLISTValue6.Text;

                                if (Condition5 == "IS NULL")
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] " + Condition6 + " ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " ";
                                }
                                else if (Condition5 == "IS NOT NULL")
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] " + Condition6 + " ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " ";
                                }
                                else if (Condition5 == "[_]%")
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] LIKE '[" + Value6 + "]%' ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " " + Value6;
                                }
                                else if (Condition5 == "![_]%")
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] NOT LIKE '[" + Value6 + "]%' ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " " + Value6;
                                }
                                else if (Condition5 == "%_%")
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] LIKE '%" + Value6 + "%' ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " " + Value6;
                                }
                                else if (Condition5 == "!%_%")
                                {
                                    CritCodeQUERY5 = "  [" + FIELDNAME6 + "] NOT LIKE '%" + Value6 + "%' ";
                                    CritQUERY6 = "  " + drpLISTField5.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " " + Value6;
                                }
                                else
                                {
                                    CritCodeQUERY6 = "  [" + FIELDNAME6 + "] " + Condition6 + " '" + Value6 + "' ";
                                    CritQUERY6 = "  " + drpLISTField6.SelectedItem.Text + " " + drpLISTCondition6.SelectedItem.Text + " " + Value6;
                                }
                            }
                        }
                    }
                }


                CritCodeQUERY = CritCodeQUERY1 + " " + CritCodeQUERY2 + " " + CritCodeQUERY3 + " " + CritCodeQUERY4 + " " + CritCodeQUERY5 + "" + CritCodeQUERY6;

                CritQUERY = CritQUERY1 + " " + CritQUERY2 + " " + CritQUERY3 + " " + CritQUERY4 + " " + CritQUERY5 + " " + CritQUERY6;

                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(
                                ACTION: "UPDATE_OnSubmit_CRIT",
                                ID: ViewState["editFORMCritID"].ToString(),
                                SEQNO: SEQNO,
                                CritName: CritName,
                                Criteria: CritQUERY,
                                CritCode: CritCodeQUERY,

                                Field1: drpLISTField1.SelectedValue,
                                CONDITION1: drpLISTCondition1.SelectedValue,
                                Value1: txtLISTValue1.Text,
                                AndOr1: drpLISTAndOr1.SelectedValue,

                                Field2: drpLISTField2.SelectedValue,
                                Condition2: drpLISTCondition2.SelectedValue,
                                VALUE2: txtLISTValue2.Text,
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
                                AndOr5: drpLISTAndOr5.SelectedValue,

                                Field6: drpLISTField6.SelectedValue,
                                Condition6: drpLISTCondition6.SelectedValue,
                                Value6: txtLISTValue6.Text,

                                FIELDNAME: ddlFoldername.SelectedValue,
                                FormCode: ddlFlag.SelectedValue
                                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnRuleCancel_Click(object sender, EventArgs e)
        {
            CLEAR_RULES();
        }

        private void CLEAR_RULES()
        {
            try
            {
                btnRuleSubmit.Visible = true;
                btnRuleUpdate.Visible = false;

                txtSEQNO.Text = "";
                txtCritName.Text = "";

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
                drpLISTAndOr5.SelectedIndex = 0;

                drpLISTField6.SelectedIndex = 0;
                drpLISTCondition6.SelectedIndex = 0;
                txtLISTValue6.Text = "";

                ddlFoldername.SelectedIndex = 0;
                ddlFlag.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdStepCrits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editFORMCritID"] = id;
                if (e.CommandName == "EditCrit")
                {
                    SELECT_CRIT(id);
                }
                else if (e.CommandName == "DeleteCrit")
                {
                    DELETE_CRIT(id);
                    GETCOMMRULES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_CRIT(string ID)
        {
            try
            {
                btnRuleSubmit.Visible = false;
                btnRuleUpdate.Visible = true;

                DataSet ds = dal.Communication_SP(Action: "COMM_SELECT_OnSubmit_CRIT", ID: ID);

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
                drpLISTAndOr5.SelectedValue = ds.Tables[0].Rows[0]["AndOr5"].ToString();

                drpLISTField6.SelectedValue = ds.Tables[0].Rows[0]["Field6"].ToString();
                drpLISTCondition6.SelectedValue = ds.Tables[0].Rows[0]["Condition6"].ToString();
                txtLISTValue6.Text = ds.Tables[0].Rows[0]["Value6"].ToString();

                ddlFoldername.SelectedValue = ds.Tables[0].Rows[0]["FOLDERNAME"].ToString();
                ddlFlag.SelectedValue = ds.Tables[0].Rows[0]["FLAGE"].ToString();
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
                //dal.NIWRS_SETUP_SP(ACTION: "COMM_DELETE_OnSubmit_CRIT", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdRules_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = Convert.ToString(e.CommandArgument);
                ViewState["COMM_RULEID"] = ID;
                if (e.CommandName == "EditList")
                {
                    EditRules(ID);
                }
                else if (e.CommandName == "DeleteList")
                {
                    Deleterules();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void EditRules(string ID)
        {
            try
            {
                DataSet ds = dal.Communication_SP(Action: "COMM_GETRULES_BYID", ID: ID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //txtrulefrom.Text = ds.Tables[0].Rows[0]["FROMID"].ToString();
                    //ddlSymbol1.SelectedValue = ds.Tables[0].Rows[0]["COND1"].ToString();
                    //txtruleTo.Text = ds.Tables[0].Rows[0]["TOID"].ToString();
                    //ddlSymbol2.SelectedValue = ds.Tables[0].Rows[0]["COND2"].ToString();
                    //txtruleCC.Text = ds.Tables[0].Rows[0]["CCID"].ToString();
                    //ddlSymbol3.SelectedValue = ds.Tables[0].Rows[0]["COND3"].ToString();
                    //txtruleBCC.Text = ds.Tables[0].Rows[0]["CCID"].ToString();
                    //ddlSymbol4.SelectedValue = ds.Tables[0].Rows[0]["COND4"].ToString();
                    //txtruleSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                    //GetListing();
                    //ddlFoldername.SelectedValue = ds.Tables[0].Rows[0]["FOLDERNAME"].ToString();
                    //ddlFlag.SelectedValue = ds.Tables[0].Rows[0]["FLAG"].ToString();
                }

                btnRuleSubmit.Visible = false;
                btnRuleUpdate.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Deleterules()
        {
            try
            {
                DataSet ds = dal.Communication_SP(
                Action: "DELETE_RULES",
                ID: ViewState["COMM_RULEID"].ToString()
                );

                GETCOMMRULES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}