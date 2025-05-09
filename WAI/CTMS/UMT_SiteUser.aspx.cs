﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class UMT_SiteUser : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SITEID();
                    GET_SITE_USER();
                    BIND_STUDYROLE();
                    GET_SYSTEM("");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SYSTEM(string UserID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_SYSTEM",
                    User_ID: UserID
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    repeatSystem.DataSource = ds.Tables[0];
                    repeatSystem.DataBind();
                }
                else
                {
                    repeatSystem.DataSource = null;
                    repeatSystem.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_SYSTEM()
        {
            try
            {
                for (int i = 0; i < repeatSystem.Items.Count; i++)
                {
                    CheckBox ChkSelect = (CheckBox)repeatSystem.Items[i].FindControl("ChkSelect");

                    Label lblSystemID = (Label)repeatSystem.Items[i].FindControl("lblSystemID");
                    Label lblSystemName = (Label)repeatSystem.Items[i].FindControl("lblSystemName");

                    TextBox txtSystemNotes = (TextBox)repeatSystem.Items[i].FindControl("txtSystemNotes");

                    CheckBox ChkSubsysIWRS = (CheckBox)repeatSystem.Items[i].FindControl("ChkSubsysIWRS");
                    Label lblSubsystemIWRS = (Label)repeatSystem.Items[i].FindControl("lblSubsystemIWRS");
                    CheckBox ChkSubSysPharma = (CheckBox)repeatSystem.Items[i].FindControl("ChkSubSysPharma");
                    Label lblSubSysPharma = (Label)repeatSystem.Items[i].FindControl("lblSubSysPharma");
                    CheckBox chksubsysDM = (CheckBox)repeatSystem.Items[i].FindControl("chksubsysDM");
                    Label LblsubsysDM = (Label)repeatSystem.Items[i].FindControl("LblsubsysDM");
                    CheckBox ChksubsyseSource = (CheckBox)repeatSystem.Items[i].FindControl("ChksubsyseSource");
                    Label lblsubsyseSource = (Label)repeatSystem.Items[i].FindControl("lblsubsyseSource");
                    CheckBox chlReadOnlyeSource = (CheckBox)repeatSystem.Items[i].FindControl("chlReadOnlyeSource");
                    Label LblReadOnly = (Label)repeatSystem.Items[i].FindControl("LblReadOnly");

                    string SubSytem = "";
                    if (ChkSubsysIWRS.Checked)
                    {
                        SubSytem = lblSubsystemIWRS.Text;
                    }
                    if (ChkSubSysPharma.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblSubSysPharma.Text;
                        }
                        else
                        {
                            SubSytem = "," + lblSubSysPharma.Text;
                        }

                    }
                    if (chksubsysDM.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = LblsubsysDM.Text;
                        }
                        else
                        {
                            SubSytem = "," + LblsubsysDM.Text;
                        }

                    }
                    if (ChksubsyseSource.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblsubsyseSource.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblsubsyseSource.Text;
                        }

                    }
                    if (chlReadOnlyeSource.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = LblReadOnly.Text;
                        }
                        else
                        {
                            SubSytem += ","  + LblReadOnly.Text;
                        }

                    }

                    if (ChkSelect.Checked)
                    {
                        DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                                        ACTION: "INSERT_SYSTEM",
                                        User_ID: hdnID.Value,
                                        SystemID: lblSystemID.Text,
                                        SystemName: lblSystemName.Text,
                                        SubSystem: SubSytem,
                                        NOTES: txtSystemNotes.Text
                                        );

                    }
                    else
                    {
                        DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                                           ACTION: "DELETE_SYSTEM",
                                           SystemID: lblSystemID.Text,
                                           SystemName: lblSystemName.Text,
                                           User_ID: hdnID.Value
                                           );
                    }

                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_STUDYROLE()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                          ACTION: "GET_STUDYROLE"
                          );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpStudyRole.DataSource = ds.Tables[0];
                    drpStudyRole.DataTextField = "StudyRole";
                    drpStudyRole.DataValueField = "StudyRole";
                    drpStudyRole.DataBind();
                    drpStudyRole.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_SITEID()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_SITE_ID"
                    );

                drpSiteID.DataSource = ds.Tables[0];
                drpSiteID.DataValueField = "SiteID";
                drpSiteID.DataTextField = "SiteID";
                drpSiteID.DataBind();
                drpSiteID.Items.Insert(0, new ListItem("--Select Site Id--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void GET_SITE_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "GET_SITE_USER",
                    SiteID: drpSiteID.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdSiteUser.DataSource = ds.Tables[0];
                    grdSiteUser.DataBind();
                }
                else
                {
                    grdSiteUser.DataSource = null;
                    grdSiteUser.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_SITE_USER()
        {
            DataSet da = dal_UMT.UMT_SITE_USER_SP(
                                        ACTION: "INSERT_SITE_USER",
                                        Fname: txtFirstName.Text,
                                        Lname: txtLastName.Text,
                                        EmailID: txtEmailid.Text,
                                        ContactNo: txtContactNo.Text,
                                        NOTES: txtNotes.Text,
                                        Blind: drpUnblind.SelectedValue,
                                        StudyRole: drpStudyRole.Text,
                                        SiteID: drpSiteID.SelectedValue,
                                        SITE_PI: drpSITEPI.SelectedValue
                                    );

            hdnID.Value = da.Tables[0].Rows[0][0].ToString();
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHECK_USER_EXISTS("INSERT"))
                {
                    Response.Write("<script language=javascript>alert('User already exists with this Name and Email ID');</script>");
                }
                else if (drpSITEPI.SelectedValue == "Yes" && CHECK_SITE_PI("INSERT"))
                {
                    Response.Write("<script language=javascript>alert('Same site can not have multiple Principal Investigators');</script>");
                }
                else
                {
                    INSERT_SITE_USER();
                    INSERT_SYSTEM();

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Site User Created Successfully');  window.location.href = 'UMT_SiteUser.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool CHECK_USER_EXISTS(string ACTION)
        {
            bool exists = false;
            try
            {
                string Fname = txtFirstName.Text.Trim();
                string Lname = txtLastName.Text.Trim();
                string EmailID = txtEmailid.Text.Trim();

                DataSet ds = new DataSet();

                if (ACTION == "UPDATE")
                {
                    ds = dal_UMT.UMT_USERS_SP(ACTION: "CHECK_USER_EXISTS", Fname: Fname, Lname: Lname, EmailID: EmailID, ID: ViewState["ID"].ToString());
                }
                else
                {
                    ds = dal_UMT.UMT_USERS_SP(ACTION: "CHECK_USER_EXISTS", Fname: Fname, Lname: Lname, EmailID: EmailID);
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    exists = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return exists;
        }

        private bool CHECK_SITE_PI(string ACTION)
        {
            bool exists = false;
            try
            {
                DataSet ds = new DataSet();

                if (ACTION == "UPDATE")
                {
                    ds = dal_UMT.UMT_SITE_USER_SP(ACTION: "CHECK_SITE_PI", SiteID: drpSiteID.SelectedValue, ID: ViewState["ID"].ToString());
                }
                else
                {
                    ds = dal_UMT.UMT_SITE_USER_SP(ACTION: "CHECK_SITE_PI", SiteID: drpSiteID.SelectedValue);
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    exists = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return exists;
        }

        private void UPDATE_SITE_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                             ACTION: "UPDATE_SITE_USER",
                             ID: ViewState["ID"].ToString(),
                             Fname: txtFirstName.Text,
                             Lname: txtLastName.Text,
                             EmailID: txtEmailid.Text,
                             ContactNo: txtContactNo.Text,
                             NOTES: txtNotes.Text,
                             Blind: drpUnblind.SelectedValue,
                             StudyRole: drpStudyRole.Text,
                             SiteID: drpSiteID.SelectedValue,
                             SITE_PI: drpSITEPI.SelectedValue
                            );

                hdnID.Value = ViewState["SiteUserID"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHECK_USER_EXISTS("UPDATE"))
                {
                    Response.Write("<script language=javascript>alert('User already exists with this Name and Email ID');</script>");
                }
                else if (drpSITEPI.SelectedValue == "Yes" && CHECK_SITE_PI("UPDATE"))
                {
                    Response.Write("<script language=javascript>alert('Same site can not have multiple Principal Investigators');</script>");
                }
                else
                {
                    UPDATE_SITE_USER();
                    INSERT_SYSTEM();

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Site User Updated Successfully');  window.location.href = 'UMT_SiteUser.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUserDetails_PreRender(object sender, EventArgs e)
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

        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;


                Label lblUserID = row.FindControl("lblUserID") as Label;

                string SiteUserID = lblUserID.Text.ToString();

                ViewState["SiteUserID"] = SiteUserID;

                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;
                if (e.CommandName == "EIDIT")
                {
                    EDIT_SITE_USER(ID);
                    GET_SYSTEM(SiteUserID);
                    lbtnSubmit.Visible = false;
                    lbnUpdate.Visible = true;
                }
                else if (e.CommandName == "DELETED")
                {
                    DELETE_SITE_USER(ID);
                    GET_SITE_USER();
                }
                else if (e.CommandName == "ACTIVATION")
                {
                    REQUEST_USER_ACTIVATE(SiteUserID);
                    GET_SITE_USER();
                }
                else if (e.CommandName == "DEACTIVATION")
                {
                    REQUEST_USER_DEACTIVATE(SiteUserID);
                    GET_SITE_USER();
                }
                else if (e.CommandName == "LOCK")
                {
                    REQUEST_USER_LOCK(SiteUserID);
                    GET_SITE_USER();
                }
                else if (e.CommandName == "ReSet_Question")
                {
                    REQUEST_QUESTION(SiteUserID);
                    GET_SITE_USER();
                }
                else if (e.CommandName == "Resend_Password")
                {
                    REQUEST_PASSWORD(SiteUserID);
                    GET_SITE_USER();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void REQUEST_USER_ACTIVATE(string UserID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "REQUEST_USER_ACTIVATE", SiteUserID: UserID,
                     REQ_DESC: "ACTIVATION"
                   );
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Generated for Activation')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void REQUEST_USER_DEACTIVATE(string UserID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "REQUEST_USER_DEACTIVATE", SiteUserID: UserID,
                     REQ_DESC: "DEACTIVATION"
                   );
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Generated for Deactivation')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void REQUEST_USER_LOCK(string UserID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "REQUEST_USER_LOCK", SiteUserID: UserID,
                     REQ_DESC: "UNLOCK"
                   );
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Generated for Unlock')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void REQUEST_QUESTION(string UserID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "REQUEST_QUESTION",
                    SiteUserID: UserID,
                    REQ_DESC: "SECURITY QUESTION"

                   );
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Request Generated for Security Question')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void REQUEST_PASSWORD(string UserID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "REQUEST_PASSWORD",
                     SiteUserID: UserID,
                     REQ_DESC: "RESET PASSWORD"
                   );
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Generated for Password')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_SITE_USER(string ID)
        {
            try
            {
                DataSet dsSystem = dal_UMT.UMT_SITE_USER_SP(
                    ACTION: "DELETE_USERID_AGIANS_SYSTEM",
                    ID: ID
                    );

                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                  ACTION: "DELETE_SITE_USER",
                  ID: ID
                  );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Site User Deleted Successfully.')", true);
                GET_SITE_USER();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT_SITE_USER(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_USER_SP(
                               ACTION: "EDIT_SITE_USER",
                               ID: ID
                           );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {

                    txtFirstName.Text = ds.Tables[0].Rows[0]["Fname"].ToString();
                    txtLastName.Text = ds.Tables[0].Rows[0]["Lname"].ToString();
                    txtEmailid.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                    txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    txtNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
                    drpUnblind.SelectedValue = ds.Tables[0].Rows[0]["Blind"].ToString();
                    drpStudyRole.SelectedValue = ds.Tables[0].Rows[0]["StudyRole"].ToString();
                    drpSiteID.SelectedValue = ds.Tables[0].Rows[0]["SiteID"].ToString();
                    drpSITEPI.SelectedValue = ds.Tables[0].Rows[0]["SITE_PI"].ToString();
                }
                else
                {
                    grdSiteUser.DataSource = null;
                    grdSiteUser.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                lbtnSubmit.Visible = true;
                lbnUpdate.Visible = false;
                CLEAR();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CLEAR()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmailid.Text = "";
            txtContactNo.Text = "";
            txtNotes.Text = "";
            drpStudyRole.SelectedIndex = 0;
            drpUnblind.SelectedIndex = 0;
            drpSiteID.SelectedIndex = 0;

            GET_SYSTEM("");
        }

        protected void repeatSystem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DAL dal;
                    dal = new DAL();

                    DataRowView row = (DataRowView)e.Item.DataItem;

                    CheckBox ChkSelect = (CheckBox)e.Item.FindControl("ChkSelect");

                    Label lblSystemName = (Label)e.Item.FindControl("lblSystemName");

                    TextBox txtSystemNotes = (TextBox)e.Item.FindControl("txtSystemNotes");
                    txtSystemNotes.Attributes.Add("maxlength", "200");

                    HtmlGenericControl divSubsysIWRS = e.Item.FindControl("divSubsysIWRS") as HtmlGenericControl;
                    HtmlGenericControl divSubSysPharma = e.Item.FindControl("divSubSysPharma") as HtmlGenericControl;
                    HtmlGenericControl divsubsysDM = e.Item.FindControl("divsubsysDM") as HtmlGenericControl;
                    HtmlGenericControl divsubsyseSource = e.Item.FindControl("divsubsyseSource") as HtmlGenericControl;

                    CheckBox ChkSubsysIWRS = (CheckBox)e.Item.FindControl("ChkSubsysIWRS");
                    CheckBox ChkSubSysPharma = (CheckBox)e.Item.FindControl("ChkSubSysPharma");
                    CheckBox chksubsysDM = (CheckBox)e.Item.FindControl("chksubsysDM");
                    CheckBox ChksubsyseSource = (CheckBox)e.Item.FindControl("ChksubsyseSource");
                    CheckBox chlReadOnlyeSource = (CheckBox)e.Item.FindControl("chlReadOnlyeSource");
                    HiddenField HiddenSubSytem = (HiddenField)e.Item.FindControl("HiddenSubSytem");

                    DataRowView rowView = e.Item.DataItem as DataRowView;
                    if (rowView != null)
                    {
                        string IsSelected = rowView["IsSelected"].ToString();

                        if (IsSelected == "True")
                        {
                            txtSystemNotes.Visible = true;
                            txtSystemNotes.Text = rowView["Notes"].ToString();

                            ChkSelect.Checked = true;
                            if (lblSystemName.Text == "IWRS")
                            {
                                divSubsysIWRS.Visible = true;

                                if (HiddenSubSytem.Value == "Unblinding")
                                {
                                    ChkSubsysIWRS.Checked = true;
                                }
                            }
                            else if (lblSystemName.Text == "Pharmacovigilance")
                            {
                                divSubSysPharma.Visible = true;

                                if (HiddenSubSytem.Value == "Sign-Off")
                                {
                                    ChkSubSysPharma.Checked = true;
                                }
                            }
                            else if (lblSystemName.Text == "Data Management")
                            {
                                divsubsysDM.Visible = true;
                                if (HiddenSubSytem.Value == "Sign-Off")
                                {
                                    chksubsysDM.Checked = true;
                                }
                            }
                            else if (lblSystemName.Text == "eSource")
                            {
                                divsubsyseSource.Visible = true;
                                if (HiddenSubSytem.Value.Split(',').Contains("Sign-Off"))
                                {
                                    ChksubsyseSource.Checked = true;

                                }
                                if (HiddenSubSytem.Value.Split(',').Contains("Read-Only"))
                                {

                                    chlReadOnlyeSource.Checked = true;
                                }
                            }
                        }
                        else
                        {
                            ChkSelect.Checked = false;
                            txtSystemNotes.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbSiteUsersDetailsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Site Users Details";

                DataSet ds = dal_UMT.UMT_LOG_SP(
                    ACTION: "GET_SITE_USER"
                    );
                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSiteUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    Label lblReviewStatus = (e.Row.FindControl("lblReviewStatus") as Label);
                    LinkButton lbtdeleteuser = (e.Row.FindControl("lbtdeleteuser") as LinkButton);

                    LinkButton lbtActive = (e.Row.FindControl("lbtActive") as LinkButton);
                    LinkButton lbtDeactive = (e.Row.FindControl("lbtDeactive") as LinkButton);
                    LinkButton lbtnReqforActi = (e.Row.FindControl("lbtnReqforActi") as LinkButton);
                    LinkButton lbtnReqForDactive = (e.Row.FindControl("lbtnReqForDactive") as LinkButton);

                    HiddenField Active = (HiddenField)e.Row.FindControl("HiddenActive");
                    HiddenField Lock = (HiddenField)e.Row.FindControl("HiddenLOCK");
                    HiddenField SECURITY = (HiddenField)e.Row.FindControl("HiddenSECURITY");

                    HiddenField ACTIVATION_Pending = (HiddenField)e.Row.FindControl("ACTIVATION_Pending");

                    HiddenField DEACTIVATION_Pending = (HiddenField)e.Row.FindControl("DEACTIVATION_Pending");
                    HiddenField UNLOCK_Pending = (HiddenField)e.Row.FindControl("UNLOCK_Pending");
                    HiddenField RESET_PASSWORD_Pending = (HiddenField)e.Row.FindControl("RESET_PASSWORD_Pending");
                    HiddenField SECURITY_QUESTION_Pending = (HiddenField)e.Row.FindControl("SECURITY_QUESTION_Pending");

                    LinkButton lbtLock = (e.Row.FindControl("lbtLock") as LinkButton);
                    LinkButton lbtUnlock = (e.Row.FindControl("lbtUnlock") as LinkButton);

                    LinkButton lbresendPassword = (e.Row.FindControl("lbresendPassword") as LinkButton);
                    LinkButton lbtnGenReqPass = (e.Row.FindControl("lbtnGenReqPass") as LinkButton);

                    LinkButton lblResetQuestion = (e.Row.FindControl("lblResetQuestion") as LinkButton);
                    LinkButton lbtnGenReqSecQues = (e.Row.FindControl("lbtnGenReqSecQues") as LinkButton);

                    if (dr["SYSTEM_COUNTS"].ToString() == "0")
                    {
                        lbtnReqforActi.Visible = false;
                        lbtActive.Visible = false;
                        lbtDeactive.Visible = false;
                        lbtnReqForDactive.Visible = false;

                        lbtLock.Visible = false;
                        lbtUnlock.Visible = false;

                        lbresendPassword.Visible = false;
                        lbtnGenReqPass.Visible = false;

                        lblResetQuestion.Visible = false;
                        lbtnGenReqSecQues.Visible = false;
                    }
                    else
                    {
                        if (Active.Value == "True")
                        {
                            if (Convert.ToInt32(DEACTIVATION_Pending.Value) > 0)
                            {
                                lbtnReqforActi.Visible = false;
                                lbtActive.Visible = false;
                                lbtDeactive.Visible = false;
                                lbtnReqForDactive.Visible = true;
                            }
                            else
                            {
                                lbtnReqforActi.Visible = false;
                                lbtActive.Visible = true;
                                lbtDeactive.Visible = false;
                                lbtnReqForDactive.Visible = false;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ACTIVATION_Pending.Value) > 0)
                            {
                                lbtnReqforActi.Visible = true;
                                lbtActive.Visible = false;
                                lbtDeactive.Visible = false;
                                lbtnReqForDactive.Visible = false;
                            }
                            else
                            {
                                lbtnReqforActi.Visible = false;
                                lbtActive.Visible = false;
                                lbtDeactive.Visible = true;
                                lbtnReqForDactive.Visible = false;
                            }
                        }

                        if (Lock.Value == "True")
                        {
                            if (Convert.ToInt32(UNLOCK_Pending.Value) > 0)
                            {
                                lbtLock.Visible = false;
                                lbtUnlock.Visible = true;
                            }
                            else
                            {
                                lbtLock.Visible = true;
                                lbtUnlock.Visible = false;
                            }
                        }
                        else
                        {
                            lbtLock.Visible = false;
                            lbtUnlock.Visible = false;
                        }

                        if (Convert.ToInt32(RESET_PASSWORD_Pending.Value) > 0)
                        {
                            lbresendPassword.Visible = false;
                            lbtnGenReqPass.Visible = true;
                        }
                        else
                        {
                            lbresendPassword.Visible = true;
                            lbtnGenReqPass.Visible = false;
                        }

                        if (Convert.ToInt32(SECURITY_QUESTION_Pending.Value) > 0)
                        {
                            lblResetQuestion.Visible = false;
                            lbtnGenReqSecQues.Visible = true;
                        }
                        else
                        {
                            lblResetQuestion.Visible = true;
                            lbtnGenReqSecQues.Visible = false;
                        }

                        if (lblReviewStatus.Text == "Pending" || lblReviewStatus.Text == "Disapprove")
                        {
                            lbtdeleteuser.Visible = true;
                            lbtActive.Visible = false;
                            lbtDeactive.Visible = false;
                            lbtnReqforActi.Visible = false;
                            lbtnReqForDactive.Visible = false;
                            lbtLock.Visible = false;
                            lbtUnlock.Visible = false;
                            lbresendPassword.Visible = false;
                            lbtnGenReqPass.Visible = false;
                            lblResetQuestion.Visible = false;
                            lbtnGenReqSecQues.Visible = false;
                        }
                        else
                        {
                            lbtdeleteuser.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ChkSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < repeatSystem.Items.Count; i++)
            {

                CheckBox ChkSelect = (CheckBox)repeatSystem.Items[i].FindControl("ChkSelect");

                Label lblSystemName = (Label)repeatSystem.Items[i].FindControl("lblSystemName");

                TextBox txtSystemNotes = (TextBox)repeatSystem.Items[i].FindControl("txtSystemNotes");

                HtmlGenericControl divSubsysIWRS = repeatSystem.Items[i].FindControl("divSubsysIWRS") as HtmlGenericControl;
                HtmlGenericControl divSubSysPharma = repeatSystem.Items[i].FindControl("divSubSysPharma") as HtmlGenericControl;
                HtmlGenericControl divsubsysDM = repeatSystem.Items[i].FindControl("divsubsysDM") as HtmlGenericControl;
                HtmlGenericControl divsubsyseSource = repeatSystem.Items[i].FindControl("divsubsyseSource") as HtmlGenericControl;

                if (ChkSelect.Checked)
                {
                    txtSystemNotes.Visible = true;
                }
                else
                {
                    txtSystemNotes.Visible = false;
                }

                if (lblSystemName.Text == "IWRS" && ChkSelect.Checked && drpUnblind.SelectedValue == "Blinded")
                {
                    divSubsysIWRS.Visible = true;
                }
                else
                {
                    divSubsysIWRS.Visible = false;
                }

                if (lblSystemName.Text == "Pharmacovigilance" && ChkSelect.Checked)
                {
                    divSubSysPharma.Visible = true;

                }
                else
                {
                    divSubSysPharma.Visible = false;

                }
                if (lblSystemName.Text == "Data Management" && ChkSelect.Checked)
                {
                    divsubsysDM.Visible = true;

                }
                else
                {
                    divsubsysDM.Visible = false;

                }
                if (lblSystemName.Text == "eSource" && ChkSelect.Checked)
                {
                    divsubsyseSource.Visible = true;

                }
                else
                {
                    divsubsyseSource.Visible = false;

                }
            }
        }

        protected void drpSiteID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SITE_USER();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpUnblind_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChkSelect_CheckedChanged(this, e);
        }
    }

}
