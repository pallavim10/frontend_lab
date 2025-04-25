using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Drawing;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ListItem = System.Web.UI.WebControls.ListItem;
using Control = System.Web.UI.Control;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using System.Web.Services;


namespace CTMS
{

    public partial class DM_CreateModuleNew : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_DB dal_DB = new DAL_DB();
        public string FieldColorValue = "#000000";
        public string AnsColorValue = "#000000";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescrip.Attributes.Add("MaxLength", "2000");
            txtReason.Attributes.Add("MaxLength", "500");
            txtOpenForEditReason.Attributes.Add("MaxLength", "500");
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                {
                    Session.Remove("ID");
                    GetSystems();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GetSystems()
        {
            try
            {
                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_USER_SYSTEMS");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpSystem.DataSource = ds.Tables[0];
                    drpSystem.DataValueField = "SystemName";
                    drpSystem.DataTextField = "SystemName";
                    drpSystem.DataBind();
                    drpSystem.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSystem.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                divshowsystem.Visible = true;
                GetModule();
                drpModule_SelectedIndexChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SYSTEMANCONTROLWISE()
        {
            try
            {
                if (drpSystem.SelectedValue == "0")
                {
                    Response.Redirect("DM_CreateModuleNew.aspx");

                }
                else if (drpSystem.SelectedValue == "External/Independent")
                {
                    divControl.Visible = false;
                    drpSCControl.Visible = false;
                    DivDesc.Visible = false;
                    DivDisplay.Visible = false;
                    DivDisplay.Visible = false;
                    DivSignificant.Visible = false;
                    DivLinkaged.Visible = false;
                    DivEntry.Visible = false;

                    divshowsystem.Visible = true;

                }
                else if (drpSystem.SelectedValue == "eSource")
                {
                    divDuplicate.Visible = false;
                    divControl.Visible = true;
                    drpSCControl.Visible = true;
                    DivDesc.Visible = true;
                }
                else if (drpSystem.SelectedValue == "IWRS")
                {
                    divDuplicate.Visible = false;
                    divReferances.Visible = false;
                    DivRequiredInfo.Visible = false;
                    divCriticalDP.Visible = false;
                    divMedAuthRes.Visible = false;
                    DivLinkedParent.Visible = false;
                    divLickedChild.Visible = false;
                    divLinkedDataFlowfield.Visible = false;
                    divSequentialYN.Visible = false;
                    divNonRepetative.Visible = false;
                    divInList.Visible = false;
                    divPrefix.Visible = false;
                    divControl.Visible = true;
                    drpSCControl.Visible = true;
                    DivDesc.Visible = true;
                    DivLinkaged.Visible = false;
                    DivEntry.Visible = false;
                    divSDV.Visible = false;
                }
                else if (drpSystem.SelectedValue == "Pharmacovigilance")
                {
                    divReferances.Visible = false;
                    divControl.Visible = true;
                    drpSCControl.Visible = true;
                    DivDesc.Visible = true;
                }
                else if (drpSystem.SelectedValue == "Data Management")
                {
                    divDuplicate.Visible = false;
                    divControl.Visible = true;
                    drpSCControl.Visible = true;
                    DivDesc.Visible = true;
                }
                else if (drpSystem.SelectedValue == "Protocol Deviation")
                {
                    divDuplicate.Visible = false;
                    divReferances.Visible = false;
                    divCriticalDP.Visible = false;
                    divMedAuthRes.Visible = false;
                    divLinkedDataFlowfield.Visible = false;
                    divNonRepetative.Visible = false;
                    DivRecords.Visible = true;
                    DivMasterData.Visible = true;
                    DivManageMap.Visible = true;
                    divControl.Visible = true;
                    drpSCControl.Visible = true;
                    DivDesc.Visible = true;
                    divParent.Visible = false;
                    divInList.Visible = false;
                }
                else if (drpSystem.SelectedValue == "Solicited Response")
                {
                    divDuplicate.Visible = false;
                    divReferances.Visible = false;
                    divCriticalDP.Visible = false;
                    divMedAuthRes.Visible = false;
                    divLinkedDataFlowfield.Visible = false;
                    divNonRepetative.Visible = false;
                    DivRecords.Visible = true;
                    DivMasterData.Visible = true;
                    DivManageMap.Visible = true;
                    divControl.Visible = true;
                    drpSCControl.Visible = true;
                    DivDesc.Visible = true;
                    divParent.Visible = false;
                    divInList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();

            }
        }

        public void GetModule()
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULENAME_BY_SYSTEM", SYSTEM: drpSystem.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds.Tables[0];
                    drpModule.DataValueField = "ID";
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpModule.SelectedValue != "0")
                {
                    GetField();
                    GetMasterModules();
                    GetTreeview();
                    ClearFieldSection();
                    ANS_CHILD();
                    GET_LINKMODULES();
                    DivRecords.Visible = true;
                    DivMasterData.Visible = true;
                    DivManageMap.Visible = true;
                    lbtnExportCrfIntruc.Visible = true;

                    if (drpSystem.SelectedValue == "External/Independent")
                    {
                        DivMasterData.Visible = false;
                        DivManageMap.Visible = false;
                        lnkCodeMapping.Visible = false;
                        lbtn_set_labDefault.Visible = false;
                        lbtnsetOnsubmitCrits.Visible = false;
                        btnSendToReview.Visible = false;
                        btnReviewLogs.Visible = false;
                        btnOpenForEdit.Visible = false;
                        divPGL_Type.Visible = false;
                        drp_PGL_Type.SelectedIndex = 0;

                    }
                    else if (drpSystem.SelectedValue == "IWRS" || drpSystem.SelectedValue == "External/Independent" || drpSystem.SelectedValue == "Pharmacovigilance")
                    {
                        lnkCodeMapping.Visible = false;
                        lbtn_set_labDefault.Visible = false;
                        lbtnsetOnsubmitCrits.Visible = false;
                        divPGL_Type.Visible = false;
                        drp_PGL_Type.SelectedIndex = 0;

                    }
                    else if (drpSystem.SelectedValue == "Data Management" || drpSystem.SelectedValue == "eSource")
                    {
                        divPGL_Type.Visible = true;
                        drp_PGL_Type.Enabled = true;
                    }

                    btnSubmitField.Visible = true;
                    btnCancelField.Visible = true;
                    btnUpdateField.Visible = false;
                    lnkClearMapping.Visible = true;

                    showFeatures();
                    MODULE_STATUS();
                }
                else
                {
                    DivRecords.Visible = false;
                    DivMasterData.Visible = false;
                    DivManageMap.Visible = false;
                    btnSendToReview.Visible = false;
                    btnReviewLogs.Visible = false;
                    btnOpenForEdit.Visible = false;
                    btnSubmitField.Visible = false;
                    lnkClearMapping.Visible = false;
                    btnCancelField.Visible = false;
                    btnUpdateField.Visible = false;
                    divPGL_Type.Visible = false;
                    drp_PGL_Type.SelectedIndex = 0;
                    ClearFieldSection();
                    showFeatures();
                }

                if (drpSystem.SelectedValue != "External/Independent")
                {
                    drpSCControl.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void MODULE_STATUS()
        {
            try
            {
                bool disablepage = false;

                DataSet ds_SYSTEM_COUNT = dal_DB.DB_REVIEW_SP(ACTION: "GET_SYSTEMS_BYMODULE_FOR_REVIEW_PROCESS",
                    MODULEID: drpModule.SelectedValue
                    );

                DataSet ds1 = dal_DB.DB_REVIEW_SP("GET_MODULE_STATUS",
                       MODULEID: drpModule.SelectedValue,
                       SYSTEM: drpSystem.SelectedValue
                       );


                if (drpSystem.SelectedValue == "Data Management" || drpSystem.SelectedValue == "eSource")
                {
                    //Db Status 
                    DataSet ds2 = dal_DB.DB_REVIEW_SP(ACTION: "Get_Status_Logs_Count");

                    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                    {
                        int count = Convert.ToInt32(ds2.Tables[0].Rows[0]["DB_STATUS_LOGS_COUNT"]);
                        if (count > 1)
                        {
                            divPGL_Type.Visible = true;
                            drp_PGL_Type.Enabled = true;
                            if (ds2 != null && ds2.Tables.Count > 1 && ds2.Tables[1].Rows.Count > 0)
                            {
                                Session["DB_STATUS_LOGS_LAST_DAT"] = ds2.Tables[1].Rows[0]["ENTEREDDAT"].ToString();
                            }
                        }
                        else
                        {
                            divPGL_Type.Visible = false;
                            Session.Remove("DB_STATUS_LOGS_LAST_DAT");
                            drp_PGL_Type.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        divPGL_Type.Visible = false;
                        Session.Remove("DB_STATUS_LOGS_LAST_DAT");
                        drp_PGL_Type.SelectedIndex = 0;
                    }
                }
                else
                {
                    divPGL_Type.Visible = false;
                    drp_PGL_Type.SelectedIndex = 0;

                }



                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "1")  //Sent For Review
                    {
                        if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_GEN_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = "Un-Review Request Generated";
                            grdField.Columns[12].Visible = false;
                            lbtnAddMoreChild.Enabled = false;
                            btnSendToReview.Visible = false;
                            btnOpenForEdit.Visible = false;
                            btnSubmitField.Visible = false;
                            btnUpdateField.Visible = false;
                            lnkClearMapping.Visible = false;
                        }
                        else
                        {
                            hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                            grdField.Columns[12].Visible = false;
                            lbtnAddMoreChild.Enabled = false;
                            btnSendToReview.Visible = false;
                            btnOpenForEdit.Visible = true;
                            btnSubmitField.Visible = false;
                            btnUpdateField.Visible = false;
                            lnkClearMapping.Visible = false;

                            if (ds1.Tables[0].Rows[0]["REVIEWED_COUNT"].ToString() != "0")
                            {
                                btnOpenForEdit.Visible = false;
                            }
                        }

                        disablepage = true;
                    }
                    else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "2" || ds1.Tables[0].Rows[0]["STATUS"].ToString() == "4") //Open For Edit From Designer && Sent Back To Designer From Reviewer
                    {
                        if (grdField.Rows.Count > 0)
                        {
                            btnSendToReview.Visible = true;
                            btnOpenForEdit.Visible = false;
                        }
                        else
                        {
                            btnSendToReview.Visible = false;
                            btnOpenForEdit.Visible = false;
                        }

                        lbtnAddMoreChild.Enabled = true;
                        hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                        grdField.Columns[12].Visible = true;
                    }
                    else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "3" || ds1.Tables[0].Rows[0]["STATUS"].ToString() == "5") //Reviewed && Un-Review Request Generated From Reviewer
                    {
                        hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                        grdField.Columns[12].Visible = false;
                        lbtnAddMoreChild.Enabled = false;
                        btnSendToReview.Visible = false;
                        btnOpenForEdit.Visible = false;
                        btnSubmitField.Visible = false;
                        btnUpdateField.Visible = false;

                        lnkClearMapping.Visible = false;
                        disablepage = true;
                    }
                    else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "6") //Un-Review Request Approved
                    {
                        if (ds_SYSTEM_COUNT.Tables[0].Rows.Count.ToString() == ds1.Tables[0].Rows[0]["UNREVIEW_REQ_APPROVE_COUNT"].ToString())
                        {
                            hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                            grdField.Columns[12].Visible = true;
                            lbtnAddMoreChild.Enabled = true;
                            btnSendToReview.Visible = true;
                            btnOpenForEdit.Visible = false;

                            disablepage = true;
                        }
                        else
                        {
                            if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_GEN_COUNT"].ToString() != "0")
                            {
                                hdnModuleStatus.Value = "Un-Review Request Generated";
                                grdField.Columns[12].Visible = false;
                                lbtnAddMoreChild.Enabled = false;
                                btnSendToReview.Visible = false;
                                btnOpenForEdit.Visible = false;
                                btnSubmitField.Visible = false;
                                btnUpdateField.Visible = false;
                                lnkClearMapping.Visible = false;

                                disablepage = true;
                            }
                            else
                            {
                                hdnModuleStatus.Value = "";
                                grdField.Columns[12].Visible = false;
                                lbtnAddMoreChild.Enabled = false;
                                btnSendToReview.Visible = false;
                                btnOpenForEdit.Visible = false;
                                btnSubmitField.Visible = false;
                                btnUpdateField.Visible = false;
                                lnkClearMapping.Visible = false;

                                disablepage = false;
                            }
                        }
                    }
                    else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "7") //Un-Review Request Disapproved
                    {
                        if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_DISSAPPROVE_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                            grdField.Columns[12].Visible = true;
                            lbtnAddMoreChild.Enabled = true;
                            btnSendToReview.Visible = true;
                            btnOpenForEdit.Visible = false;

                            disablepage = false;
                        }
                        else
                        {
                            hdnModuleStatus.Value = "";
                            grdField.Columns[12].Visible = true;
                            lbtnAddMoreChild.Enabled = true;
                            btnSendToReview.Visible = true;
                            btnOpenForEdit.Visible = false;
                            disablepage = true;
                        }
                    }
                    else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "8" || ds1.Tables[0].Rows[0]["STATUS"].ToString() == "9") //Frozen && Un-Freeze Request Generated
                    {
                        hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                        grdField.Columns[12].Visible = false;
                        lbtnAddMoreChild.Enabled = false;
                        btnSendToReview.Visible = false;
                        btnOpenForEdit.Visible = false;
                        btnSubmitField.Visible = false;
                        btnUpdateField.Visible = false;
                        lnkClearMapping.Visible = false;

                        disablepage = true;
                    }
                    else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "10") //Un-Freeze Request Approved
                    {
                        if (ds_SYSTEM_COUNT.Tables[0].Rows.Count.ToString() == ds1.Tables[0].Rows[0]["UNFREEZING_REQ_APPROVE_COUNT"].ToString())
                        {
                            hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                            grdField.Columns[12].Visible = true;
                            lbtnAddMoreChild.Enabled = true;
                            btnSendToReview.Visible = true;
                            btnOpenForEdit.Visible = false;

                            disablepage = true;
                        }
                        else
                        {
                            if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_GEN_COUNT"].ToString() != "0")
                            {
                                hdnModuleStatus.Value = "Un-Freeze Request Generated";
                                grdField.Columns[12].Visible = false;
                                lbtnAddMoreChild.Enabled = false;
                                btnSendToReview.Visible = false;
                                btnOpenForEdit.Visible = false;
                                btnSubmitField.Visible = false;
                                btnUpdateField.Visible = false;
                                lnkClearMapping.Visible = false;

                                disablepage = true;
                            }
                            else
                            {
                                hdnModuleStatus.Value = "";
                                grdField.Columns[12].Visible = false;
                                lbtnAddMoreChild.Enabled = false;
                                btnSendToReview.Visible = false;
                                btnOpenForEdit.Visible = false;
                                btnSubmitField.Visible = false;
                                btnUpdateField.Visible = false;
                                lnkClearMapping.Visible = false;

                                disablepage = false;
                            }
                        }
                    }
                    else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "11") //Un-Freeze Request Disapproved
                    {
                        if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_DISSAPPROVE_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                            grdField.Columns[12].Visible = true;
                            lbtnAddMoreChild.Enabled = true;
                            btnSendToReview.Visible = true;
                            btnOpenForEdit.Visible = false;

                            disablepage = false;
                        }
                        else
                        {
                            hdnModuleStatus.Value = "";
                            grdField.Columns[12].Visible = true;
                            lbtnAddMoreChild.Enabled = true;
                            btnSendToReview.Visible = true;
                            btnOpenForEdit.Visible = false;
                            disablepage = true;
                        }
                    }
                    else
                    {
                        grdField.Columns[12].Visible = true;
                        hdnModuleStatus.Value = "";
                    }

                    if (ds1.Tables[0].Rows[0]["LOGS_COUNTS"].ToString() != "0")
                    {
                        btnReviewLogs.Visible = true;
                    }
                    else
                    {
                        btnReviewLogs.Visible = false;
                    }

                    if (disablepage)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "DisableDiv('" + hdnModuleStatus.Value + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetField()
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULEFIELD_BYMODULEID",
                    MODULEID: drpModule.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdField.DataSource = ds.Tables[0];
                    grdField.DataBind();

                    btnSendToReview.Visible = true;
                    btnOpenForEdit.Visible = false;
                    lbtnsetOnsubmitCrits.Visible = true;

                    ddlFIELD.DataSource = ds.Tables[0];
                    ddlFIELD.DataTextField = "FIELDNAME";
                    ddlFIELD.DataValueField = "ID";
                    ddlFIELD.DataBind();
                    ddlFIELD.Items.Insert(0, new ListItem("--Select--", "0"));

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        if (ds.Tables[1].Rows[0]["LabData"].ToString() == "True")
                        {
                            lbtn_set_labDefault.Visible = true;
                        }
                        else
                        {
                            lbtn_set_labDefault.Visible = false;
                        }
                    }
                    else
                    {
                        lbtn_set_labDefault.Visible = false;
                    }

                    if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                    {
                        if (ds.Tables[2].Rows[0]["AUTOCODE"].ToString() == "True")
                        {
                            lnkCodeMapping.Visible = true;
                        }
                        else
                        {
                            lnkCodeMapping.Visible = false;
                        }
                    }
                    else
                    {
                        lnkCodeMapping.Visible = false;
                    }

                }
                else
                {
                    grdField.DataSource = null;
                    grdField.DataBind();

                    btnSendToReview.Visible = false;
                    btnOpenForEdit.Visible = false;

                    ddlFIELD.Items.Clear();
                    lbtn_set_labDefault.Visible = false;
                    lnkCodeMapping.Visible = false;
                    lbtnsetOnsubmitCrits.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GetMasterModules()
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MASTER_MODULES");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModuleMaster.DataSource = ds.Tables[0];
                    drpModuleMaster.DataValueField = "MODULENAME";
                    drpModuleMaster.DataTextField = "MODULENAME";
                    drpModuleMaster.DataBind();
                    drpModuleMaster.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpModuleMaster.Items.Clear();
                }

                DataSet ds1 = dal_DB.DB_MODULE_SP(ACTION: "GET_MASTER_FIELDS");

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    gvChecklists.DataSource = ds1.Tables[0];
                    gvChecklists.DataBind();
                }
                else
                {
                    gvChecklists.DataSource = null;
                    gvChecklists.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GetMasterFields()
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MASTER_FIELDS", MODULENAME: drpModuleMaster.SelectedValue, MODULEID: drpModule.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvChecklists.DataSource = ds.Tables[0];
                    gvChecklists.DataBind();
                }
                else
                {
                    gvChecklists.DataSource = null;
                    gvChecklists.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkPrefix_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPrefix.Checked)
                {
                    txtPrefix.Visible = true;
                }
                else
                {
                    txtPrefix.Visible = false;
                    txtPrefix.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkDefault_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDefault.Checked)
                {
                    txtDefaultData.Visible = true;
                }
                else
                {
                    txtDefaultData.Visible = false;
                    txtDefaultData.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkInList_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ShowHideInlistEditable();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ShowHideInlistEditable()
        {
            try
            {
                if (chkInList.Checked)
                {
                    divInlistEditable.Visible = true;
                }
                else
                {
                    divInlistEditable.Visible = false;
                    chkInlistEditable.Checked = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LINKMODULES()
        {
            try
            {
                if (drpSCControl.SelectedValue == "ChildModule" && drpModule.SelectedValue != "0")
                {
                    DataSet ds = dal_DB.DB_FIELD_SP(ACTION: "GET_FIELDS_MODULES_MAPPING", MODULEID: drpModule.SelectedValue);
                    ddlChildModule.DataSource = ds.Tables[1];
                    ddlChildModule.DataTextField = "MODULENAME";
                    ddlChildModule.DataValueField = "ID";
                    ddlChildModule.DataBind();
                    ddlChildModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }

        protected void drpSCControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearAllFeatures();
                showFeatures();


                if (hdnOldControlType.Value == "CHECKBOX" || hdnOldControlType.Value == "RADIOBUTTON" || hdnOldControlType.Value == "DROPDOWN" || hdnOldControlType.Value == "ChildModule")
                {
                    if (hdnOldControlType.Value == "DROPDOWN" && (drpSCControl.SelectedValue == "CHECKBOX" || drpSCControl.SelectedValue == "RADIOBUTTON"))
                    {
                        dal_DB.DB_DRP_SP(ACTION: "DELETE_SELECT_OPTIONS_BY_CHANGING_CONTROL",
                            VARIABLENAME: txtVariableName.Text
                            );
                    }
                    else if (hdnOldControlType.Value != "DROPDOWN" && drpSCControl.SelectedValue == "DROPDOWN")
                    {
                        dal_DB.DB_DRP_SP(ACTION: "ADD_SELECT_OPTIONS_BY_CHANGING_CONTROL",
                            VARIABLENAME: txtVariableName.Text
                            );
                    }
                    else if (drpSCControl.SelectedValue != "CHECKBOX" && drpSCControl.SelectedValue != "RADIOBUTTON" && drpSCControl.SelectedValue != "DROPDOWN" && drpSCControl.SelectedValue != "ChildModule")
                    {
                        dal_DB.DB_DRP_SP(ACTION: "DELETE_OPTIONS_BY_CHANGING_CONTROL",
                            VARIABLENAME: txtVariableName.Text
                            );
                    }
                    else if (drpSCControl.SelectedValue == "ChildModule" && hdnOldControlType.Value != "ChildModule")
                    {
                        GET_LINKMODULES();
                    }
                }
                if (drpSCControl.SelectedValue == "ChildModule")
                {
                    GET_LINKMODULES();
                }
                hdnOldControlType.Value = drpSCControl.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkAutoCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCode.Checked)
                {
                    drpAutoCode.Visible = true;
                    drpAutoCode.SelectedValue = "0";
                }
                else
                {
                    drpAutoCode.Visible = false;
                    drpAutoCode.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void showFeatures()
        {
            try
            {
                switch (drpSCControl.SelectedItem.Text)
                {
                    case "CHECKBOX":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        DIVChildModule.Visible = false;
                        //---Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //---Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //--DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divParent.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //---DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = true;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;

                    case "TEXTBOX":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        DIVmaxLenght.Visible = true;
                        DIVChildModule.Visible = false;
                        divFormat.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = true;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = true;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = true;
                        divLinkedDataFlowfield.Visible = true;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = true;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = true;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "TEXTBOX with Options":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = true;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = true;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = true;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "HTML TEXTBOX":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        DIVmaxLenght.Visible = true;
                        divFormat.Visible = false;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = true;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = true;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "RADIOBUTTON":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = true;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "DROPDOWN":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = true;
                        divLickedChild.Visible = true;
                        divReferances.Visible = true;
                        divParent.Visible = true;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = true;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "TIME":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        DIVmaxLenght.Visible = false;
                        divFormat.Visible = false;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "NUMERIC":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = true;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = true;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = true;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "DATE without Futuristic Date":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "DECIMAL":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = true;
                        DIVmaxLenght.Visible = false;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = true;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "HEADER":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = false;
                        DivReadonly.Visible = false;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = false;
                        DivMandatoryInfo.Visible = false;                        
                        divSDV.Visible = false;
                        divCriticalDP.Visible = false;
                        divMedAuthRes.Visible = false;
                        divDuplicate.Visible = false;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divParent.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = false;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = true;
                        lblDataLinkages.Visible = true;
                        lblDataEntry.Visible = true;
                        divPGL_Type.Visible = false;
                        drp_PGL_Type.SelectedIndex = 0;
                        drp_PGL_Type.SelectedIndex = 0;
                        break;
                    case "DATE":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "DATE with Input Mask":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        DIVChildModule.Visible = false;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = true;
                        divCriticalDP.Visible = false;
                        break;
                    case "Child Module":
                        DivDisplay.Visible = false;
                        DivSignificant.Visible = false;
                        DivLinkaged.Visible = false;
                        DivEntry.Visible = false;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        DIVChildModule.Visible = true;
                        ddlChildModule.Visible = true;
                        //Features
                        divBOLD.Visible = false;
                        divMaskField.Visible = false;
                        DivReadonly.Visible = false;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = false;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = false;
                        DivMandatoryInfo.Visible = false;
                        divMedAuthRes.Visible = false;
                        divDuplicate.Visible = false;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divParent.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = false;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        divSDV.Visible = false;
                        divCriticalDP.Visible = false;
                        break;
                    default:
                        DivDisplay.Visible = false;
                        DivSignificant.Visible = false;
                        DivLinkaged.Visible = false;
                        DivEntry.Visible = false;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        DIVChildModule.Visible = false;
                        divSDV.Visible = false;
                        divCriticalDP.Visible = false;
                        break;
                }
                SYSTEMANCONTROLWISE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitField_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsMap = dal_DB.DB_FIELD_SP(ACTION: "CHECK_CODE_MAPPING", MODULEID: drpModule.SelectedValue, VARIABLENAME: txtVariableName.Text);

                if (dsMap.Tables.Count > 0 && dsMap.Tables[0].Rows.Count > 0)
                {
                    if (drpAutoCode.SelectedValue == "MedDRA")
                    {
                        if ("MedDRA" != dsMap.Tables[0].Rows[0]["AutoCodeLIB"].ToString())
                        {
                            DataSet dsMedra = dal.DB_CODE_SP(ACTION: "DELETE_CODEMAPING", MODULEID: drpModule.SelectedValue);
                        }
                    }
                    else if (drpAutoCode.SelectedValue == "WHODD")
                    {
                        if ("WHODD" != dsMap.Tables[0].Rows[0]["AutoCodeLIB"].ToString())
                        {
                            DataSet dswhodd = dal.DB_CODE_SP(ACTION: "DELETE_CODEMAPING", MODULEID: drpModule.SelectedValue);
                        }
                    }
                }

                if (drpSCControl.SelectedValue == "HTML TEXTBOX" && chkMultiline.Checked == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check Freetext');", true);
                }
                else
                {
                    DataSet dsVar = dal_DB.DB_FIELD_SP(ACTION: "CHECK_FIELD_VARIABLE",
                        MODULEID: drpModule.SelectedValue,
                        VARIABLENAME: txtVariableName.Text,
                        SYSTEM: drpSystem.SelectedValue
                        );

                    if (dsVar.Tables.Count > 0 && dsVar.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a unique variable name as the variable name already exists in the " + drpSystem.SelectedValue + " System.');", true);
                    }
                    else
                    {
                        DataSet ds1 = dal_DB.DB_FIELD_SP(ACTION: "CHECK_MODULE_ON_MASTER",
                        MODULEID: drpModule.SelectedValue);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            LlbModulevisit.Text = drpModule.SelectedItem.Text;
                            LlbFieldevisit.Text = txtFieldName.Text;
                            Label7.Text = LlbModulevisit.Text + " available in below visit.";

                            grdvisitSubmit.DataSource = ds1;
                            grdvisitSubmit.DataBind();

                            ModalPopupExtender1.Show();
                        }
                        else
                        {
                            btnSubmitField.Visible = true;
                            btnCancelField.Visible = true;
                            btnUpdateField.Visible = false;
                            lnkClearMapping.Visible = true;
                            ADD_FIELDS();
                            ClearFieldSection();
                            GetField();
                            MODULE_STATUS();
                            GetTreeview();
                            showFeatures();
                            drpModule_SelectedIndexChanged(this, e);

                            // Clear the radio button selection
                            rdoPermanentDelete.Checked = false;
                            rdoProspectiveDelete.Checked = false;

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field added successfully');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void ADD_FIELDS()
        {
            try
            {
                string DefaultData = "";

                if (chkDefault.Checked)
                {
                    DefaultData = txtDefaultData.Text;
                }

                if (!chkInList.Checked)
                {
                    chkInlistEditable.Checked = false;
                }

                string AUTOCODELIB = "";
                if (drpAutoCode.SelectedValue == "0")
                {
                    AUTOCODELIB = "";
                }
                else
                {
                    AUTOCODELIB = drpAutoCode.SelectedValue;
                }

                string PGL_TYPE = "";
                if (drp_PGL_Type.SelectedValue != "0")
                {
                    PGL_TYPE = drp_PGL_Type.SelectedValue;
                }

                DataSet ds = dal_DB.DB_FIELD_SP
                    (
                    ACTION: "INSERT_MODULEFIELD",
                    MODULEID: drpModule.SelectedValue,
                    MODULENAME: drpModule.SelectedItem.Text,
                    FIELDNAME: txtFieldName.Text,
                    VARIABLENAME: txtVariableName.Text,
                    Descrip: txtDescrip.Text,
                    CONTROLTYPE: drpSCControl.SelectedValue,
                    FORMAT: txtFormat.Text,
                    SEQNO: txtSeqno.Text,
                    MAXLEN: txtMaxLength.Text,
                    LINKEDMODULEID: ddlChildModule.SelectedValue,
                    FieldColor: Request.Form["FieldColor"],
                    AnsColor: Request.Form["AnsColor"],
                    UPPERCASE: chkUppercase.Checked,
                    BOLDYN: chkBold.Checked,
                    UNLNYN: chkUnderline.Checked,
                    READYN: chkRead.Checked,
                    MULTILINEYN: chkMultiline.Checked,
                    REQUIREDYN: chkRequired.Checked,
                    INVISIBLE: chkInvisible.Checked,
                    AUTOCODE: chkAutoCode.Checked,
                    AutoCodeLIB: AUTOCODELIB,
                    InList: chkInList.Checked,
                    InListEditable: chkInlistEditable.Checked,
                    LabData: chkLab.Checked,
                    AutoNum: chkAutoNum.Checked,
                    Refer: chkREF.Checked,
                    Critic_DP: chkCriticDP.Checked,
                    Prefix: chkPrefix.Checked,
                    PrefixText: txtPrefix.Text,
                    DUPLICATE: chkduplicate.Checked,
                    NONREPETATIVE: chknonRepetative.Checked,
                    MANDATORY: chkMandatory.Checked,
                    DefaultData: DefaultData,
                    ParentLinked: chkParentLinked.Checked,
                    ChildLinked: chkChildLinked.Checked,
                    MEDOP: chkMEDOP.Checked,
                    PGL_TYPE: PGL_TYPE,
                    SDV: chkSDV.Checked

                    );

                if (drpSCControl.SelectedValue != "ChildModule")
                {
                    string DATATYPE = "NVARCHAR(2000)";
                    if (txtMaxLength.Text != "" && txtMaxLength.Text != "0")
                    {
                        DATATYPE = "NVARCHAR(" + txtMaxLength.Text + ")";
                    }

                    if (drpSCControl.SelectedValue == "DECIMAL" || drpSCControl.SelectedValue == "NUMERIC")
                    {
                        DATATYPE = "FLOAT";
                    }
                    else if (drpSCControl.SelectedValue == "DATE" || drpSCControl.SelectedValue == "DATENOFUTURE")
                    {
                        DATATYPE = "NVARCHAR(11)";
                    }
                    else if (drpSCControl.SelectedValue == "DATEINPUTMASK")
                    {
                        DATATYPE = "NVARCHAR(10)";
                    }

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (ds.Tables[0].Rows[j]["Table"].ToString() != "")
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "ADD_COLUMN",
                            TABLE: ds.Tables[0].Rows[j]["Table"].ToString(),
                            COLUMN: txtVariableName.Text,
                            DATATYPE: DATATYPE
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void ClearFieldSection()
        {
            try
            {
                txtFieldName.Text = "";
                txtVariableName.Text = "";
                txtDescrip.Text = "";
                drpSCControl.SelectedIndex = 0;
                txtFormat.Text = "";
                //txtFormat.Visible = false;
                txtSeqno.Text = "";
                txtMaxLength.Text = "";
                //txtMaxLength.Visible = false;
                chkUppercase.Checked = false;
                chkBold.Checked = false;
                chkUnderline.Checked = false;
                chkRead.Checked = false;
                chkMultiline.Checked = false;
                chkRequired.Checked = false;
                chkInvisible.Checked = false;
                chkAutoCode.Checked = false;
                drpAutoCode.SelectedIndex = 0;
                drpAutoCode.Visible = false;
                chkInList.Checked = false;
                chkInlistEditable.Checked = false;
                divInlistEditable.Visible = false;
                chkLab.Checked = false;
                chkAutoNum.Checked = false;
                chkREF.Checked = false;
                chkCriticDP.Checked = false;
                chkPrefix.Checked = false;
                txtPrefix.Text = "";
                txtPrefix.Visible = false;
                chkduplicate.Checked = false;
                chknonRepetative.Checked = false;
                chkMandatory.Checked = false;
                chkDefault.Checked = false;
                txtDefaultData.Text = "";
                txtDefaultData.Visible = false;
                chkParentLinked.Checked = false;
                chkChildLinked.Checked = false;
                chkMEDOP.Checked = false;
                hfFieldColor.Value = "";
                hfAnsColor.Value = "";
                DIVChildModule.Visible = true;
                ddlChildModule.Visible = false;
                ddlChildModule.ClearSelection();
                drp_PGL_Type.SelectedIndex = 0;
                chkSDV.Checked = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void ClearAllFeatures()
        {
            try
            {
                txtFormat.Text = "";
                txtMaxLength.Text = "";
                chkUppercase.Checked = false;
                chkBold.Checked = false;
                chkUnderline.Checked = false;
                chkRead.Checked = false;
                chkMultiline.Checked = false;
                chkRequired.Checked = false;
                chkInvisible.Checked = false;
                chkAutoCode.Checked = false;
                drpAutoCode.SelectedIndex = 0;
                drpAutoCode.Visible = false;
                chkInList.Checked = false;
                chkInlistEditable.Checked = false;
                divInlistEditable.Visible = false;
                chkLab.Checked = false;
                chkAutoNum.Checked = false;
                chkREF.Checked = false;
                chkCriticDP.Checked = false;
                chkPrefix.Checked = false;
                txtPrefix.Text = "";
                txtPrefix.Visible = false;
                chkduplicate.Checked = false;
                chknonRepetative.Checked = false;
                chkMandatory.Checked = false;
                chkDefault.Checked = false;
                txtDefaultData.Text = "";
                txtDefaultData.Visible = false;
                chkParentLinked.Checked = false;
                chkChildLinked.Checked = false;
                chkParentLinked.Enabled = true;
                chkChildLinked.Enabled = true;
                chkMEDOP.Checked = false;
                hfFieldColor.Value = "";
                hfAnsColor.Value = "";
                DIVChildModule.Visible = true;
                ddlChildModule.Visible = false;
                ddlChildModule.ClearSelection();
                chkSDV.Checked = false;
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
                if (!chkInList.Checked)
                {
                    chkInlistEditable.Checked = false;
                }

                DataSet dsMap = dal_DB.DB_FIELD_SP(ACTION: "CHECK_CODE_MAPPING", MODULEID: drpModule.SelectedValue, VARIABLENAME: txtVariableName.Text);

                if (dsMap.Tables.Count > 0 && dsMap.Tables[0].Rows.Count > 0)
                {
                    if (drpAutoCode.SelectedValue == "MedDRA")
                    {
                        if ("MedDRA" != dsMap.Tables[0].Rows[0]["AutoCodeLIB"].ToString())
                        {
                            DataSet dsMedra = dal.DB_CODE_SP(ACTION: "DELETE_CODEMAPING", MODULEID: drpModule.SelectedValue, AutoCodedTerm: txtVariableName.Text);
                        }
                    }
                    else if (drpAutoCode.SelectedValue == "WHODD")
                    {
                        if ("WHODD" != dsMap.Tables[0].Rows[0]["AutoCodeLIB"].ToString())
                        {
                            DataSet dswhodd = dal.DB_CODE_SP(ACTION: "DELETE_CODEMAPING", MODULEID: drpModule.SelectedValue, AutoCodedTerm: txtVariableName.Text);
                        }
                    }
                }

                if (drpSCControl.SelectedValue == "HTML TEXTBOX" && chkMultiline.Checked == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check Freetext');", true);
                }
                else
                {
                    DataSet dsVar = dal_DB.DB_FIELD_SP(ACTION: "CHECK_FIELD_VARIABLE",
                        MODULEID: drpModule.SelectedValue,
                        VARIABLENAME: txtVariableName.Text,
                        ID: Session["ID"].ToString(),
                        SYSTEM: drpSystem.SelectedValue
                        );

                    if (dsVar.Tables.Count > 0 && dsVar.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a unique variable name as the variable name already exists in the " + drpSystem.SelectedValue + " System.');", true);

                    }
                    else
                    {
                        DataSet ds1 = dal_DB.DB_FIELD_SP(ACTION: "CHECK_MODULE_FIELD_ON_MASTER",
                        MODULEID: drpModule.SelectedValue,
                        ID: Session["ID"].ToString());

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            Label3Module.Text = drpModule.SelectedItem.Text;
                            Label4Field.Text = txtFieldName.Text;
                            //Label1.Text = Label4Field.Text + " is available in below visit.";

                            grdVisitUpdate.DataSource = ds1;
                            grdVisitUpdate.DataBind();

                            ModalPopupExtender2.Show();
                        }
                        else
                        {
                            DataSet ds2 = dal_DB.DB_FIELD_SP(ACTION: "CHECK_MODULE_ON_MASTER",
                            MODULEID: drpModule.SelectedValue);

                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                LlbModulevisit.Text = drpModule.SelectedItem.Text;
                                LlbFieldevisit.Text = txtFieldName.Text;
                                Label7.Text = LlbModulevisit.Text + " available in below visit.";

                                grdvisitSubmit.DataSource = ds2;
                                grdvisitSubmit.DataBind();

                                ModalPopupExtender1.Show();
                            }
                            else
                            {
                                btnSubmitField.Visible = true;
                                btnCancelField.Visible = true;
                                btnUpdateField.Visible = false;
                                lnkClearMapping.Visible = true;

                                UPDATE_FIELD();
                                ClearFieldSection();
                                GetField();
                                MODULE_STATUS();
                                GetTreeview();
                                showFeatures();
                                drpModule_SelectedIndexChanged(this, e);

                                Session.Remove("ID");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field updated successfully.');", true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void UPDATE_FIELD()
        {
            try
            {
                string DefaultData = "";

                if (chkDefault.Checked)
                {
                    DefaultData = txtDefaultData.Text;
                }

                string AUTOCODELIB = "";
                if (drpAutoCode.SelectedValue == "0")
                {
                    AUTOCODELIB = "";
                }
                else
                {
                    AUTOCODELIB = drpAutoCode.SelectedValue;
                }

                string PGL_TYPE = "";
                if (drp_PGL_Type.SelectedValue != "0")
                {
                    PGL_TYPE = drp_PGL_Type.SelectedValue;
                }

                DataSet ds = dal_DB.DB_FIELD_SP
                    (
                    ACTION: "UPDATE_MODULEFIELD",
                    ID: Session["ID"].ToString(),
                    MODULEID: drpModule.SelectedValue,
                    MODULENAME: drpModule.SelectedItem.Text,
                    FIELDNAME: txtFieldName.Text,
                    VARIABLENAME: txtVariableName.Text,
                    Descrip: txtDescrip.Text,
                    CONTROLTYPE: drpSCControl.SelectedValue,
                    FORMAT: txtFormat.Text,
                    SEQNO: txtSeqno.Text,
                    MAXLEN: txtMaxLength.Text,
                    LINKEDMODULEID: ddlChildModule.SelectedValue,
                    FieldColor: Request.Form["FieldColor"],
                    AnsColor: Request.Form["AnsColor"],
                    UPPERCASE: chkUppercase.Checked,
                    BOLDYN: chkBold.Checked,
                    UNLNYN: chkUnderline.Checked,
                    READYN: chkRead.Checked,
                    MULTILINEYN: chkMultiline.Checked,
                    REQUIREDYN: chkRequired.Checked,
                    INVISIBLE: chkInvisible.Checked,
                    AUTOCODE: chkAutoCode.Checked,
                    AutoCodeLIB: AUTOCODELIB,
                    InList: chkInList.Checked,
                    InListEditable: chkInlistEditable.Checked,
                    LabData: chkLab.Checked,
                    AutoNum: chkAutoNum.Checked,
                    Refer: chkREF.Checked,
                    Critic_DP: chkCriticDP.Checked,
                    Prefix: chkPrefix.Checked,
                    PrefixText: txtPrefix.Text,
                    DUPLICATE: chkduplicate.Checked,
                    NONREPETATIVE: chknonRepetative.Checked,
                    MANDATORY: chkMandatory.Checked,
                    DefaultData: DefaultData,
                    ParentLinked: chkParentLinked.Checked,
                    ChildLinked: chkChildLinked.Checked,
                    MEDOP: chkMEDOP.Checked,
                    PGL_TYPE: PGL_TYPE,
                    SDV: chkSDV.Checked
                    );

                if (drpSCControl.SelectedValue != "ChildModule")
                {
                    string DATATYPE = "NVARCHAR(2000)";
                    if (txtMaxLength.Text != "" && txtMaxLength.Text != "0")
                    {
                        DATATYPE = "NVARCHAR(" + txtMaxLength.Text + ")";
                    }

                    if (drpSCControl.SelectedValue == "DECIMAL" || drpSCControl.SelectedValue == "NUMERIC")
                    {
                        DATATYPE = "FLOAT";
                    }
                    else if (drpSCControl.SelectedValue == "DATE" || drpSCControl.SelectedValue == "DATENOFUTURE")
                    {
                        DATATYPE = "NVARCHAR(11)";
                    }
                    else if (drpSCControl.SelectedValue == "DATEINPUTMASK")
                    {
                        DATATYPE = "NVARCHAR(10)";
                    }

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (ds.Tables[0].Rows[j]["Table"].ToString() != "")
                        {
                            dal_DB.DB_TABLES_SP
                                (
                                Action: "RENAME_COLUMN",
                                    TABLE: ds.Tables[0].Rows[j]["Table"].ToString(),
                                    COLUMN: txtVariableName.Text,
                                    OLDCOLUMN: hfOLDVARIABLENAME.Value,
                                    DATATYPE: DATATYPE
                                );
                        }
                    }
                }

                dal_DB.DB_DRP_SP(ACTION: "UPDATE_OPITONS_VAR_BY_OLDVAR",
                    OLD_VARIABLENAME: hfOLDVARIABLENAME.Value,
                    VARIABLENAME: txtVariableName.Text
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelField_Click(object sender, EventArgs e)
        {
            ClearFieldSection();
            GetField();
            MODULE_STATUS();
            GetTreeview();
            showFeatures();
            drpModule_SelectedIndexChanged(this, e);

            Session.Remove("ID");
        }

        protected void grdField_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                LinkButton ctrl = e.CommandSource as LinkButton;
                //if (ctrl != null)
                //{

                string id = e.CommandArgument.ToString();
                Session["ID"] = id;

                //PGL_TYPE
                if (e.CommandName == "EditField")
                {

                    // Get the row containing the clicked LinkButton
                    GridViewRow row = (GridViewRow)ctrl.NamingContainer;

                    // Find the Label inside the row
                    Label lblPGL_TYPE = row.FindControl("PGL_TYPE") as Label;

                    drp_PGL_Type.SelectedIndex = 0;


                    DataSet ds = dal_DB.DB_FIELD_SP(ACTION: "GET_MODULEFIELD_BYID", ID: id);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        divField.Focus();
                        drpModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();

                        txtFieldName.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                        txtVariableName.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();
                        txtMaxLength.Text = ds.Tables[0].Rows[0]["MAXLEN"].ToString();
                        txtSeqno.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

                        if ((ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString() == "TEXTBOX") && (drpSystem.SelectedValue != "External/Independent"))
                        {
                            if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("OptionValues"))
                            {
                                drpSCControl.SelectedValue = "TEXTBOX with Options";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("txtDateNoFuture"))
                            {
                                drpSCControl.SelectedValue = "DATENOFUTURE";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("txtDateMask"))
                            {
                                drpSCControl.SelectedValue = "DATEINPUTMASK";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("txtDate"))
                            {
                                drpSCControl.SelectedValue = "DATE";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("numericdecimal"))
                            {
                                drpSCControl.SelectedValue = "DECIMAL";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("numeric"))
                            {
                                drpSCControl.SelectedValue = "NUMERIC";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("txtTime"))
                            {
                                drpSCControl.SelectedValue = "TIME";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("ckeditor"))
                            {
                                drpSCControl.SelectedValue = "HTML TEXTBOX";
                            }
                            else
                            {
                                drpSCControl.SelectedValue = ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString();
                            }
                        }
                        else
                        {
                            drpSCControl.SelectedValue = ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString();
                        }

                        hdnOldControlType.Value = drpSCControl.SelectedValue;
                        showFeatures();
                        // Handle LINKED MODULE logic
                        if (ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString() == "ChildModule")
                        {
                            ddlChildModule.Visible = true;
                            GET_LINKMODULES();

                            // Populate the Linked Module dropdown value
                            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LINKED_MODULEID"].ToString()))
                            {
                                ddlChildModule.SelectedValue = ds.Tables[0].Rows[0]["LINKED_MODULEID"].ToString();
                            }
                        }
                        else
                        {
                            ddlChildModule.Visible = false;
                        }
                        txtFormat.Text = ds.Tables[0].Rows[0]["FORMAT"].ToString();

                        if (ds.Tables[0].Rows[0]["BOLDYN"].ToString() == "True")
                        {
                            chkBold.Checked = true;
                        }
                        else
                        {
                            chkBold.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["READYN"].ToString() == "True")
                        {
                            chkRead.Checked = true;
                        }
                        else
                        {
                            chkRead.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["MULTILINEYN"].ToString() == "True")
                        {
                            chkMultiline.Checked = true;
                        }
                        else
                        {
                            chkMultiline.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["UNLNYN"].ToString() == "True")
                        {
                            chkUnderline.Checked = true;
                        }
                        else
                        {
                            chkUnderline.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["REQUIREDYN"].ToString() == "True")
                        {
                            chkRequired.Checked = true;
                        }
                        else
                        {
                            chkRequired.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["INVISIBLE"].ToString() == "True")
                        {
                            chkInvisible.Checked = true;
                        }
                        else
                        {
                            chkInvisible.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["AUTOCODE"].ToString() == "True")
                        {
                            chkAutoCode.Checked = true;
                            drpAutoCode.Visible = true;
                            lnkCodeMapping.Visible = true;
                            if (drpSCControl.SelectedValue == "RADIOBUTTON")
                            {
                                lblSignificant.Visible = false;
                            }
                            if (ds.Tables[0].Rows[0]["AutoCodeLIB"].ToString() != "")
                            {
                                drpAutoCode.SelectedValue = ds.Tables[0].Rows[0]["AutoCodeLIB"].ToString();
                            }
                        }
                        else
                        {
                            chkAutoCode.Checked = false;
                            drpAutoCode.Visible = false;
                            lnkCodeMapping.Visible = false;
                            drpAutoCode.SelectedIndex = 0;
                        }

                        if (ds.Tables[0].Rows[0]["InList"].ToString() == "True")
                        {
                            chkInList.Checked = true;
                        }
                        else
                        {
                            chkInList.Checked = false;
                        }

                        ShowHideInlistEditable();

                        if (ds.Tables[0].Rows[0]["InListEditable"].ToString() == "True")
                        {
                            chkInlistEditable.Checked = true;
                        }
                        else
                        {
                            chkInlistEditable.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["LabData"].ToString() == "True")
                        {
                            chkLab.Checked = true;
                        }
                        else
                        {
                            chkLab.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["UPPERCASE"].ToString() == "True")
                        {
                            chkUppercase.Checked = true;
                        }
                        else
                        {
                            chkUppercase.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["AUTONUM"].ToString() == "True")
                        {
                            chkAutoNum.Checked = true;
                        }
                        else
                        {
                            chkAutoNum.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["REFER"].ToString() == "True")
                        {
                            chkREF.Checked = true;
                        }
                        else
                        {
                            chkREF.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["DUPLICATE"].ToString() == "True")
                        {
                            chkduplicate.Checked = true;
                        }
                        else
                        {
                            chkduplicate.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["NONREPETATIVE"].ToString() == "True")
                        {
                            chknonRepetative.Checked = true;
                        }
                        else
                        {
                            chknonRepetative.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["MANDATORY"].ToString() == "True")
                        {
                            chkMandatory.Checked = true;
                        }
                        else
                        {
                            chkMandatory.Checked = false;
                        }

                        txtDescrip.Text = ds.Tables[0].Rows[0]["Descrip"].ToString();

                        if (ds.Tables[0].Rows[0]["Prefix"].ToString() == "True")
                        {
                            chkPrefix.Checked = true;
                            txtPrefix.Visible = true;
                            txtPrefix.Text = ds.Tables[0].Rows[0]["PrefixText"].ToString();
                        }
                        else
                        {
                            chkPrefix.Checked = false;
                            txtPrefix.Visible = false;
                            txtPrefix.Text = "";
                        }


                        if (chkRead.Checked == false)
                        {
                            if (ds.Tables[0].Rows[0]["SDV"].ToString() == "True")
                            {
                                chkSDV.Checked = true;
                                divCriticalDP.Visible = true;
                                if (ds.Tables[0].Rows[0]["Critic_DP"].ToString() == "True")
                                {
                                    chkCriticDP.Checked = true;
                                }
                                else
                                {
                                    chkCriticDP.Checked = false;
                                }
                            }
                            else
                            {
                                divCriticalDP.Visible = false;
                                chkSDV.Checked = false;
                            }
                        }
                        else
                        {
                            divCriticalDP.Visible = false;
                            chkSDV.Checked = false;
                        }

                        FieldColorValue = ds.Tables[0].Rows[0]["FieldColor"].ToString();
                        AnsColorValue = ds.Tables[0].Rows[0]["AnsColor"].ToString();

                        hfOLDVARIABLENAME.Value = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();

                        hfFieldColor.Value = ds.Tables[0].Rows[0]["FieldColor"].ToString();
                        hfAnsColor.Value = ds.Tables[0].Rows[0]["AnsColor"].ToString();

                        if (ds.Tables[0].Rows[0]["DefaultData"].ToString() != "")
                        {
                            txtDefaultData.Text = ds.Tables[0].Rows[0]["DefaultData"].ToString();
                            chkDefault.Checked = true;
                            txtDefaultData.Visible = true;
                        }
                        else
                        {
                            txtDefaultData.Text = "";
                            chkDefault.Checked = false;
                            txtDefaultData.Visible = false;
                        }

                        if (ds.Tables[0].Rows[0]["ParentLinked"].ToString() == "True")
                        {
                            chkParentLinked.Checked = true;
                        }
                        else
                        {
                            chkParentLinked.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["ChildLinked"].ToString() == "True")
                        {
                            chkChildLinked.Checked = true;
                        }
                        else
                        {
                            chkChildLinked.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["MEDOP"].ToString() == "True")
                        {
                            chkMEDOP.Checked = true;
                        }
                        else
                        {
                            chkMEDOP.Checked = false;
                        }
                        if (drpSystem.SelectedValue == "External/Independent")
                        {
                            grdField.Columns[2].ControlStyle.CssClass = "disp-none";
                            grdField.Columns[3].ControlStyle.CssClass = "disp-none";
                            grdField.Columns[5].ControlStyle.CssClass = "disp-none";
                        }
                        btnUpdateField.Visible = true;
                        btnSubmitField.Visible = false;
                        lnkClearMapping.Visible = true;
                        btnCancelField.Visible = true;

                        MODULE_STATUS();

                        if (drpSystem.SelectedValue == "Data Management" || drpSystem.SelectedValue == "eSource")
                        {
                            if (Session["DB_STATUS_LOGS_LAST_DAT"] != null)
                            {
                                //if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() == "")
                                //{
                                //    if (Convert.ToDateTime(ds.Tables[0].Rows[0]["ENTEREDDAT"].ToString()) >= Convert.ToDateTime(Session["DB_STATUS_LOGS_LAST_DAT"].ToString()))
                                //    {
                                //        divPGL_Type.Visible = true;

                                //        if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "Deleted")
                                //        {
                                //            drp_PGL_Type.SelectedValue = ds.Tables[0].Rows[0]["PGL_TYPE"].ToString();
                                //        }
                                //    }
                                //    else
                                //    {
                                //        divPGL_Type.Visible = false;
                                //        drp_PGL_Type.SelectedIndex = 0;
                                //    }
                                //}
                                //else
                                //{
                                //    divPGL_Type.Visible = true;

                                //    if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "Deleted")
                                //    {
                                //        drp_PGL_Type.SelectedValue = ds.Tables[0].Rows[0]["PGL_TYPE"].ToString();
                                //    }
                                //}

                                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["ENTEREDDAT"].ToString()) >= Convert.ToDateTime(Session["DB_STATUS_LOGS_LAST_DAT"].ToString()))
                                {
                                    divPGL_Type.Visible = true;
                                    drp_PGL_Type.Enabled = true;
                                    if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "Deleted")
                                    {
                                        drp_PGL_Type.SelectedValue = ds.Tables[0].Rows[0]["PGL_TYPE"].ToString();
                                    }
                                }
                                else
                                {
                                    divPGL_Type.Visible = true;
                                    drp_PGL_Type.Enabled = true;
                                    if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "Deleted")
                                    {
                                        drp_PGL_Type.SelectedValue = ds.Tables[0].Rows[0]["PGL_TYPE"].ToString();
                                        drp_PGL_Type.Enabled = false;
                                    }
                                    else
                                    {
                                        divPGL_Type.Visible = false;
                                        drp_PGL_Type.SelectedIndex = 0;
                                    }
                                }
                            }
                            else
                            {
                                divPGL_Type.Visible = false;
                                drp_PGL_Type.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            divPGL_Type.Visible = false;
                            drp_PGL_Type.SelectedIndex = 0;
                        }

                        if ((ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["DELETEDBY"] != DBNull.Value &&
                            !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DELETEDBY"].ToString())) || (lblPGL_TYPE.Text == "Deleted"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "DisableDiv('Deleted');", true);
                        }
                    }
                }
                if (e.CommandName.StartsWith("DeleteField"))
                {
                    DataSet ds = dal_DB.DB_FIELD_SP(ACTION: "DELETE_MODULEFIELD",
                        ID: id,
                        MODULEID: drpModule.SelectedValue
                        );

                    Session.Remove("ID");
                    GetField();
                    MODULE_STATUS();
                    GetTreeview();
                    drpModule_SelectedIndexChanged(this, e);
                    // Clear the radio button selection
                    rdoPermanentDelete.Checked = false;
                    rdoProspectiveDelete.Checked = false;

                    UpdatePanel1.Update(); // Update the panel explicitly
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Field deleted successfully');", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Btn_Add_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvChecklists.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvChecklists.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        string txtFIELDNAME = ((Label)gvChecklists.Rows[i].FindControl("txtFIELDNAME")).Text;
                        string txtSeqNO = ((Label)gvChecklists.Rows[i].FindControl("txtSeqNO")).Text;
                        string ID = ((Label)gvChecklists.Rows[i].FindControl("lbl_ID")).Text;
                        string VARIABLENAME = ((Label)gvChecklists.Rows[i].FindControl("VARIABLENAME")).Text;

                        if (txtFIELDNAME == "")
                        {
                            throw new Exception("Please Enter Field Name");
                        }
                        if (txtSeqNO == "")
                        {
                            throw new Exception("Please Enter SeqNO");
                        }
                        if (VARIABLENAME == "")
                        {
                            throw new Exception("Please Enter Variable Name");
                        }

                        DataSet ds = dal_DB.DB_FIELD_SP
                           (
                           ACTION: "INSERT_MODULEFIELD_FROM_MASTER",
                           ID: ID,
                           MODULEID: drpModule.SelectedValue,
                           MODULENAME: drpModule.SelectedItem.Text,
                           VARIABLENAME: VARIABLENAME,
                           FIELDNAME: txtFIELDNAME,
                           SEQNO: txtSeqNO
                           );

                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            if (ds.Tables[0].Rows[j]["Table"].ToString() != "")
                            {
                                dal_DB.DB_TABLES_SP(
                                Action: "ADD_COLUMN",
                                TABLE: ds.Tables[0].Rows[j]["Table"].ToString(),
                                COLUMN: VARIABLENAME,
                                DATATYPE: "NVARCHAR(MAX)"
                                );
                            }
                        }
                    }
                }

                ClearFieldSection();
                GetField();
                GetMasterModules();
                MODULE_STATUS();
                GetTreeview();
                drpModule_SelectedIndexChanged(this, e);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field added successfully');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void drpModuleMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetMasterFields();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvChecklists_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    CheckBox Chk_Sel_Fun = (CheckBox)e.Row.FindControl("Chk_Sel_Fun");
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                    DataSet dsVar = dal_DB.DB_FIELD_SP(ACTION: "CHECK_FIELD_VARIABLE",
                        MODULEID: drpModule.SelectedValue,
                        VARIABLENAME: VARIABLENAME,
                        SYSTEM: drpSystem.SelectedValue
                        );


                    if (dsVar.Tables.Count > 0 && dsVar.Tables[0].Rows.Count > 0)
                    {
                        Chk_Sel_Fun.Visible = false;
                        lblStatus.Visible = true;
                        string Status = "";
                        foreach (DataRow row in dsVar.Tables[0].Rows)
                        {
                            if (Status == "")
                            {
                                Status = row["MODULENAME"].ToString();
                            }
                            else
                            {
                                Status += ", " + row["MODULENAME"].ToString();
                            }
                        }

                        lblStatus.Text = "Variable Already Exists in Module : " + Status;
                    }
                    else
                    {
                        Chk_Sel_Fun.Visible = true;
                        lblStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
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

        protected void grdField_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    //string COUNT = dr["COUNT"].ToString();
                    string CLASS = dr["CLASS"].ToString();
                    string ChildLinked = dr["ChildLinked"].ToString();
                    LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");

                    if (drpSystem.SelectedValue == "External/Independent")
                    {
                        grdField.Columns[2].ControlStyle.CssClass = "disp-none";
                        grdField.Columns[3].ControlStyle.CssClass = "disp-none";
                        grdField.Columns[5].ControlStyle.CssClass = "disp-none";
                    }
                    else if (drpSystem.SelectedValue == "IWRS")
                    {
                        grdField.Columns[3].ControlStyle.CssClass = "disp-none";
                    }
                    else
                    {
                        grdField.Columns[2].Visible = true;
                        grdField.Columns[3].Visible = true;
                        grdField.Columns[5].Visible = true;
                    }

                    if (CONTROLTYPE == "DROPDOWN" || CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "CHECKBOX" || CLASS.Contains("OptionValues form-control"))
                    {
                        if (ChildLinked == "True")
                        {
                            LinkButton lnlAddOption_LINKED = (LinkButton)e.Row.FindControl("lnlAddOption_LINKED");
                            lnlAddOption_LINKED.Visible = true;
                        }
                        else
                        {
                            LinkButton lnlAddOption = (LinkButton)e.Row.FindControl("lnlAddOption");
                            lnlAddOption.Visible = true;
                        }
                    }

                    //if (COUNT.ToString().Trim() != "0")
                    //{
                    //    lbtndeleteSection.Visible = false;
                    //}
                    //else
                    //{
                    //    lbtndeleteSection.Visible = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void lbtnAddMoreChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlChildNo29.Visible == true)
                {
                    divChild30.Visible = true;
                    ddlChildNo30.Visible = true;
                }

                if (ddlChildNo28.Visible == true)
                {
                    divChild29.Visible = true;
                    ddlChildNo29.Visible = true;
                }

                if (ddlChildNo27.Visible == true)
                {
                    divChild28.Visible = true;
                    ddlChildNo28.Visible = true;
                }

                if (ddlChildNo26.Visible == true)
                {
                    divChild27.Visible = true;
                    ddlChildNo27.Visible = true;
                }

                if (ddlChildNo25.Visible == true)
                {
                    divChild26.Visible = true;
                    ddlChildNo26.Visible = true;
                }

                if (ddlChildNo24.Visible == true)
                {
                    divChild25.Visible = true;
                    ddlChildNo25.Visible = true;
                }

                if (ddlChildNo23.Visible == true)
                {
                    divChild24.Visible = true;
                    ddlChildNo24.Visible = true;
                }

                if (ddlChildNo22.Visible == true)
                {
                    divChild23.Visible = true;
                    ddlChildNo23.Visible = true;
                }

                if (ddlChildNo21.Visible == true)
                {
                    divChild22.Visible = true;
                    ddlChildNo22.Visible = true;
                }

                if (ddlChildNo20.Visible == true)
                {
                    divChild21.Visible = true;
                    ddlChildNo21.Visible = true;
                }

                if (ddlChildNo19.Visible == true)
                {
                    divChild20.Visible = true;
                    ddlChildNo20.Visible = true;
                }

                if (ddlChildNo18.Visible == true)
                {
                    divChild19.Visible = true;
                    ddlChildNo19.Visible = true;
                }

                if (ddlChildNo17.Visible == true)
                {
                    divChild18.Visible = true;
                    ddlChildNo18.Visible = true;
                }

                if (ddlChildNo16.Visible == true)
                {
                    divChild17.Visible = true;
                    ddlChildNo17.Visible = true;
                }

                if (ddlChildNo15.Visible == true)
                {
                    divChild16.Visible = true;
                    ddlChildNo16.Visible = true;
                }

                if (ddlChildNo14.Visible == true)
                {
                    divChild15.Visible = true;
                    ddlChildNo15.Visible = true;
                }

                if (ddlChildNo13.Visible == true)
                {
                    divChild14.Visible = true;
                    ddlChildNo14.Visible = true;
                }

                if (ddlChildNo12.Visible == true)
                {
                    divChild13.Visible = true;
                    ddlChildNo13.Visible = true;
                }

                if (ddlChildNo11.Visible == true)
                {
                    divChild12.Visible = true;
                    ddlChildNo12.Visible = true;
                }

                if (ddlChildNo10.Visible == true)
                {
                    divChild11.Visible = true;
                    ddlChildNo11.Visible = true;
                }

                if (ddlChildNo9.Visible == true)
                {
                    divChild10.Visible = true;
                    ddlChildNo10.Visible = true;
                }

                if (ddlChildNo8.Visible == true)
                {
                    divChild9.Visible = true;
                    ddlChildNo9.Visible = true;
                }

                if (ddlChildNo7.Visible == true)
                {
                    divChild8.Visible = true;
                    ddlChildNo8.Visible = true;
                }

                if (ddlChildNo6.Visible == true)
                {
                    divChild7.Visible = true;
                    ddlChildNo7.Visible = true;
                }

                if (ddlChildNo5.Visible == true)
                {
                    divChild6.Visible = true;
                    ddlChildNo6.Visible = true;
                }

                if (ddlChildNo4.Visible == true)
                {
                    divChild5.Visible = true;
                    ddlChildNo5.Visible = true;
                }

                if (ddlChildNo3.Visible == true)
                {
                    divChild4.Visible = true;
                    ddlChildNo4.Visible = true;
                }

                if (ddlChildNo2.Visible == true)
                {
                    divChild3.Visible = true;
                    ddlChildNo3.Visible = true;
                }
                else
                {
                    divChild2.Visible = true;
                    ddlChildNo2.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlFIELD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CHECK_LEVEL4())
            {
                ANS_CHILD();
            }
            else
            {
                ddlFIELD.SelectedIndex = 0;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This field can not be mapped further.');", true);
            }
        }

        private bool CHECK_LEVEL4()
        {
            bool res = true;
            try
            {
                foreach (TreeNode TreeNode1 in treeFieldMap.Nodes)
                {
                    foreach (TreeNode TreeNode2 in TreeNode1.ChildNodes)
                    {
                        foreach (TreeNode TreeNode3 in TreeNode2.ChildNodes)
                        {
                            foreach (TreeNode TreeNode4 in TreeNode3.ChildNodes)
                            {
                                if (TreeNode4.Value == ddlFIELD.SelectedValue)
                                {
                                    res = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return res;
        }

        private void ANS_CHILD()
        {
            try
            {
                DataSet ds1 = dal_DB.DB_FIELD_SP(ACTION: "GET_OPTIONS_LIST", ID: ddlFIELD.SelectedValue);

                ddlAnsTrigger.DataSource = ds1;
                ddlAnsTrigger.DataTextField = "TEXT";
                ddlAnsTrigger.DataValueField = "VALUE";
                ddlAnsTrigger.DataBind();
                ddlAnsTrigger.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlAnsTrigger.Items.Insert(1, new ListItem("Is Not Blank", "Is Not Blank"));

                DataSet ds = dal_DB.DB_FIELD_SP(ACTION: "GET_FIELDS_MODULES_MAPPING",
                    ID: ddlFIELD.SelectedValue,
                    MODULEID: drpModule.SelectedValue);

                ddlChildNo.DataSource = ds.Tables[0];
                ddlChildNo.DataTextField = "FIELDNAME";
                ddlChildNo.DataValueField = "ID";
                ddlChildNo.DataBind();
                ddlChildNo.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo2.DataSource = ds.Tables[0];
                ddlChildNo2.DataTextField = "FIELDNAME";
                ddlChildNo2.DataValueField = "ID";
                ddlChildNo2.DataBind();
                ddlChildNo2.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo3.DataSource = ds.Tables[0];
                ddlChildNo3.DataTextField = "FIELDNAME";
                ddlChildNo3.DataValueField = "ID";
                ddlChildNo3.DataBind();
                ddlChildNo3.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo4.DataSource = ds.Tables[0];
                ddlChildNo4.DataTextField = "FIELDNAME";
                ddlChildNo4.DataValueField = "ID";
                ddlChildNo4.DataBind();
                ddlChildNo4.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo5.DataSource = ds.Tables[0];
                ddlChildNo5.DataTextField = "FIELDNAME";
                ddlChildNo5.DataValueField = "ID";
                ddlChildNo5.DataBind();
                ddlChildNo5.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo6.DataSource = ds.Tables[0];
                ddlChildNo6.DataTextField = "FIELDNAME";
                ddlChildNo6.DataValueField = "ID";
                ddlChildNo6.DataBind();
                ddlChildNo6.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo7.DataSource = ds.Tables[0];
                ddlChildNo7.DataTextField = "FIELDNAME";
                ddlChildNo7.DataValueField = "ID";
                ddlChildNo7.DataBind();
                ddlChildNo7.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo8.DataSource = ds.Tables[0];
                ddlChildNo8.DataTextField = "FIELDNAME";
                ddlChildNo8.DataValueField = "ID";
                ddlChildNo8.DataBind();
                ddlChildNo8.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo9.DataSource = ds.Tables[0];
                ddlChildNo9.DataTextField = "FIELDNAME";
                ddlChildNo9.DataValueField = "ID";
                ddlChildNo9.DataBind();
                ddlChildNo9.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo10.DataSource = ds.Tables[0];
                ddlChildNo10.DataTextField = "FIELDNAME";
                ddlChildNo10.DataValueField = "ID";
                ddlChildNo10.DataBind();
                ddlChildNo10.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo11.DataSource = ds.Tables[0];
                ddlChildNo11.DataTextField = "FIELDNAME";
                ddlChildNo11.DataValueField = "ID";
                ddlChildNo11.DataBind();
                ddlChildNo11.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo12.DataSource = ds.Tables[0];
                ddlChildNo12.DataTextField = "FIELDNAME";
                ddlChildNo12.DataValueField = "ID";
                ddlChildNo12.DataBind();
                ddlChildNo12.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo13.DataSource = ds.Tables[0];
                ddlChildNo13.DataTextField = "FIELDNAME";
                ddlChildNo13.DataValueField = "ID";
                ddlChildNo13.DataBind();
                ddlChildNo13.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo14.DataSource = ds.Tables[0];
                ddlChildNo14.DataTextField = "FIELDNAME";
                ddlChildNo14.DataValueField = "ID";
                ddlChildNo14.DataBind();
                ddlChildNo14.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo15.DataSource = ds.Tables[0];
                ddlChildNo15.DataTextField = "FIELDNAME";
                ddlChildNo15.DataValueField = "ID";
                ddlChildNo15.DataBind();
                ddlChildNo15.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo16.DataSource = ds.Tables[0];
                ddlChildNo16.DataTextField = "FIELDNAME";
                ddlChildNo16.DataValueField = "ID";
                ddlChildNo16.DataBind();
                ddlChildNo16.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo17.DataSource = ds.Tables[0];
                ddlChildNo17.DataTextField = "FIELDNAME";
                ddlChildNo17.DataValueField = "ID";
                ddlChildNo17.DataBind();
                ddlChildNo17.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo18.DataSource = ds.Tables[0];
                ddlChildNo18.DataTextField = "FIELDNAME";
                ddlChildNo18.DataValueField = "ID";
                ddlChildNo18.DataBind();
                ddlChildNo18.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo19.DataSource = ds.Tables[0];
                ddlChildNo19.DataTextField = "FIELDNAME";
                ddlChildNo19.DataValueField = "ID";
                ddlChildNo19.DataBind();
                ddlChildNo19.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo20.DataSource = ds.Tables[0];
                ddlChildNo20.DataTextField = "FIELDNAME";
                ddlChildNo20.DataValueField = "ID";
                ddlChildNo20.DataBind();
                ddlChildNo20.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo21.DataSource = ds.Tables[0];
                ddlChildNo21.DataTextField = "FIELDNAME";
                ddlChildNo21.DataValueField = "ID";
                ddlChildNo21.DataBind();
                ddlChildNo21.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo22.DataSource = ds.Tables[0];
                ddlChildNo22.DataTextField = "FIELDNAME";
                ddlChildNo22.DataValueField = "ID";
                ddlChildNo22.DataBind();
                ddlChildNo22.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo23.DataSource = ds.Tables[0];
                ddlChildNo23.DataTextField = "FIELDNAME";
                ddlChildNo23.DataValueField = "ID";
                ddlChildNo23.DataBind();
                ddlChildNo23.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo24.DataSource = ds.Tables[0];
                ddlChildNo24.DataTextField = "FIELDNAME";
                ddlChildNo24.DataValueField = "ID";
                ddlChildNo24.DataBind();
                ddlChildNo24.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo25.DataSource = ds.Tables[0];
                ddlChildNo25.DataTextField = "FIELDNAME";
                ddlChildNo25.DataValueField = "ID";
                ddlChildNo25.DataBind();
                ddlChildNo25.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo26.DataSource = ds.Tables[0];
                ddlChildNo26.DataTextField = "FIELDNAME";
                ddlChildNo26.DataValueField = "ID";
                ddlChildNo26.DataBind();
                ddlChildNo26.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo27.DataSource = ds.Tables[0];
                ddlChildNo27.DataTextField = "FIELDNAME";
                ddlChildNo27.DataValueField = "ID";
                ddlChildNo27.DataBind();
                ddlChildNo27.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo28.DataSource = ds.Tables[0];
                ddlChildNo28.DataTextField = "FIELDNAME";
                ddlChildNo28.DataValueField = "ID";
                ddlChildNo28.DataBind();
                ddlChildNo28.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo29.DataSource = ds.Tables[0];
                ddlChildNo29.DataTextField = "FIELDNAME";
                ddlChildNo29.DataValueField = "ID";
                ddlChildNo29.DataBind();
                ddlChildNo29.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlChildNo30.DataSource = ds.Tables[0];
                ddlChildNo30.DataTextField = "FIELDNAME";
                ddlChildNo30.DataValueField = "ID";
                ddlChildNo30.DataBind();
                ddlChildNo30.Items.Insert(0, new ListItem("--Select--", "0"));

                //ddlChildModule.DataSource = ds.Tables[1];
                //ddlChildModule.DataTextField = "MODULENAME";
                //ddlChildModule.DataValueField = "ID";
                //ddlChildModule.DataBind();
                //ddlChildModule.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_CHILD_Click(object sender, EventArgs e)
        {
            try
            {
                SUBMIT_CHILD();
                GetTreeview();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Child field mapped successfully');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SUBMIT_CHILD()
        {
            try
            {
                string CHILD = null;

                if (ddlChildNo.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo.SelectedValue;
                    }
                }
                if (ddlChildNo2.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo2.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo2.SelectedValue;
                    }
                }
                if (ddlChildNo3.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo3.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo3.SelectedValue;
                    }
                }
                if (ddlChildNo4.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo4.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo4.SelectedValue;
                    }
                }
                if (ddlChildNo5.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo5.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo5.SelectedValue;
                    }
                }
                if (ddlChildNo6.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo6.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo6.SelectedValue;
                    }
                }
                if (ddlChildNo7.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo7.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo7.SelectedValue;
                    }
                }
                if (ddlChildNo8.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo8.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo8.SelectedValue;
                    }
                }
                if (ddlChildNo9.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo9.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo9.SelectedValue;
                    }
                }
                if (ddlChildNo10.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo10.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo10.SelectedValue;
                    }
                }
                if (ddlChildNo11.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo11.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo11.SelectedValue;
                    }
                }
                if (ddlChildNo12.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo12.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo12.SelectedValue;
                    }
                }
                if (ddlChildNo13.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo13.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo13.SelectedValue;
                    }
                }
                if (ddlChildNo14.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo14.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo14.SelectedValue;
                    }
                }
                if (ddlChildNo15.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo15.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo15.SelectedValue;
                    }
                }
                if (ddlChildNo16.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo16.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo16.SelectedValue;
                    }
                }
                if (ddlChildNo17.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo17.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo17.SelectedValue;
                    }
                }
                if (ddlChildNo18.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo18.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo18.SelectedValue;
                    }
                }
                if (ddlChildNo19.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo19.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo19.SelectedValue;
                    }
                }
                if (ddlChildNo20.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo20.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo20.SelectedValue;
                    }
                }
                if (ddlChildNo21.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo21.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo21.SelectedValue;
                    }
                }
                if (ddlChildNo22.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo22.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo22.SelectedValue;
                    }
                }
                if (ddlChildNo23.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo23.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo23.SelectedValue;
                    }
                }
                if (ddlChildNo24.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo24.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo24.SelectedValue;
                    }
                }
                if (ddlChildNo25.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo25.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo25.SelectedValue;
                    }
                }
                if (ddlChildNo26.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo26.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo26.SelectedValue;
                    }
                }
                if (ddlChildNo27.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo27.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo27.SelectedValue;
                    }
                }
                if (ddlChildNo28.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo28.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo28.SelectedValue;
                    }
                }
                if (ddlChildNo29.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo29.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo29.SelectedValue;
                    }
                }
                if (ddlChildNo30.SelectedValue != "0")
                {
                    if (CHILD == null)
                    {
                        CHILD += ddlChildNo30.SelectedValue;
                    }
                    else
                    {
                        CHILD += "," + ddlChildNo30.SelectedValue;
                    }
                }


                dal_DB.DB_FIELD_SP(ACTION: "INSERT_FIELD_CHILD",
                    ID: ddlFIELD.SelectedValue,
                    VAL_Child: ddlAnsTrigger.SelectedValue,
                    CHILD_ID: CHILD
                    );

                CANCEL_CHILD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CANCEL_CHILD()
        {
            ddlAnsTrigger.SelectedIndex = 0;
            ddlAnsTrigger.DataSource = null;
            ddlAnsTrigger.DataBind();

            ddlChildNo.SelectedIndex = 0;
            ddlChildNo.DataSource = null;
            ddlChildNo.DataBind();

            divChild2.Visible = false;
            ddlChildNo2.Visible = false;
            ddlChildNo2.SelectedIndex = 0;
            ddlChildNo2.DataSource = null;
            ddlChildNo2.DataBind();

            divChild3.Visible = false;
            ddlChildNo3.Visible = false;
            ddlChildNo3.SelectedIndex = 0;
            ddlChildNo3.DataSource = null;
            ddlChildNo3.DataBind();

            divChild4.Visible = false;
            ddlChildNo4.Visible = false;
            ddlChildNo4.SelectedIndex = 0;
            ddlChildNo4.DataSource = null;
            ddlChildNo4.DataBind();

            divChild5.Visible = false;
            ddlChildNo5.Visible = false;
            ddlChildNo5.SelectedIndex = 0;
            ddlChildNo5.DataSource = null;
            ddlChildNo5.DataBind();

            divChild6.Visible = false;
            ddlChildNo6.Visible = false;
            ddlChildNo6.SelectedIndex = 0;
            ddlChildNo6.DataSource = null;
            ddlChildNo6.DataBind();

            divChild7.Visible = false;
            ddlChildNo7.Visible = false;
            ddlChildNo7.SelectedIndex = 0;
            ddlChildNo7.DataSource = null;
            ddlChildNo7.DataBind();

            divChild8.Visible = false;
            ddlChildNo8.Visible = false;
            ddlChildNo8.SelectedIndex = 0;
            ddlChildNo8.DataSource = null;
            ddlChildNo8.DataBind();

            divChild9.Visible = false;
            ddlChildNo9.Visible = false;
            ddlChildNo9.SelectedIndex = 0;
            ddlChildNo9.DataSource = null;
            ddlChildNo9.DataBind();

            divChild10.Visible = false;
            ddlChildNo10.Visible = false;
            ddlChildNo10.SelectedIndex = 0;
            ddlChildNo10.DataSource = null;
            ddlChildNo10.DataBind();

            divChild11.Visible = false;
            ddlChildNo11.Visible = false;
            ddlChildNo11.SelectedIndex = 0;
            ddlChildNo11.DataSource = null;
            ddlChildNo11.DataBind();

            divChild12.Visible = false;
            ddlChildNo12.Visible = false;
            ddlChildNo12.SelectedIndex = 0;
            ddlChildNo12.DataSource = null;
            ddlChildNo12.DataBind();

            divChild13.Visible = false;
            ddlChildNo13.Visible = false;
            ddlChildNo13.SelectedIndex = 0;
            ddlChildNo13.DataSource = null;
            ddlChildNo13.DataBind();

            divChild14.Visible = false;
            ddlChildNo14.Visible = false;
            ddlChildNo14.SelectedIndex = 0;
            ddlChildNo14.DataSource = null;
            ddlChildNo14.DataBind();

            divChild15.Visible = false;
            ddlChildNo15.Visible = false;
            ddlChildNo15.SelectedIndex = 0;
            ddlChildNo15.DataSource = null;
            ddlChildNo15.DataBind();

            divChild16.Visible = false;
            ddlChildNo16.Visible = false;
            ddlChildNo16.SelectedIndex = 0;
            ddlChildNo16.DataSource = null;
            ddlChildNo16.DataBind();

            divChild17.Visible = false;
            ddlChildNo17.Visible = false;
            ddlChildNo17.SelectedIndex = 0;
            ddlChildNo17.DataSource = null;
            ddlChildNo17.DataBind();

            divChild18.Visible = false;
            ddlChildNo18.Visible = false;
            ddlChildNo18.SelectedIndex = 0;
            ddlChildNo18.DataSource = null;
            ddlChildNo18.DataBind();

            divChild19.Visible = false;
            ddlChildNo19.Visible = false;
            ddlChildNo19.SelectedIndex = 0;
            ddlChildNo19.DataSource = null;
            ddlChildNo19.DataBind();

            divChild20.Visible = false;
            ddlChildNo20.Visible = false;
            ddlChildNo20.SelectedIndex = 0;
            ddlChildNo20.DataSource = null;
            ddlChildNo20.DataBind();

            divChild21.Visible = false;
            ddlChildNo21.Visible = false;
            ddlChildNo21.SelectedIndex = 0;
            ddlChildNo21.DataSource = null;
            ddlChildNo21.DataBind();

            divChild22.Visible = false;
            ddlChildNo22.Visible = false;
            ddlChildNo22.SelectedIndex = 0;
            ddlChildNo22.DataSource = null;
            ddlChildNo22.DataBind();

            divChild23.Visible = false;
            ddlChildNo23.Visible = false;
            ddlChildNo23.SelectedIndex = 0;
            ddlChildNo23.DataSource = null;
            ddlChildNo23.DataBind();

            divChild24.Visible = false;
            ddlChildNo24.Visible = false;
            ddlChildNo24.SelectedIndex = 0;
            ddlChildNo24.DataSource = null;
            ddlChildNo24.DataBind();

            divChild25.Visible = false;
            ddlChildNo25.Visible = false;
            ddlChildNo25.SelectedIndex = 0;
            ddlChildNo25.DataSource = null;
            ddlChildNo25.DataBind();

            divChild26.Visible = false;
            ddlChildNo26.Visible = false;
            ddlChildNo26.SelectedIndex = 0;
            ddlChildNo26.DataSource = null;
            ddlChildNo26.DataBind();

            divChild27.Visible = false;
            ddlChildNo27.Visible = false;
            ddlChildNo27.SelectedIndex = 0;
            ddlChildNo27.DataSource = null;
            ddlChildNo27.DataBind();

            divChild28.Visible = false;
            ddlChildNo28.Visible = false;
            ddlChildNo28.SelectedIndex = 0;
            ddlChildNo28.DataSource = null;
            ddlChildNo28.DataBind();

            divChild29.Visible = false;
            ddlChildNo29.Visible = false;
            ddlChildNo29.SelectedIndex = 0;
            ddlChildNo29.DataSource = null;
            ddlChildNo29.DataBind();

            divChild30.Visible = false;
            ddlChildNo30.Visible = false;
            ddlChildNo30.SelectedIndex = 0;
            ddlChildNo30.DataSource = null;
            ddlChildNo30.DataBind();

        }

        protected void btnCancel_CHILD_Click(object sender, EventArgs e)
        {
            try
            {
                CANCEL_CHILD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlAnsTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CHILD_FIELD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CHILD_FIELD()
        {
            try
            {
                //ddlChildModule.SelectedIndex = 0;
                //ddlChildNum1.SelectedIndex = 0;
                ddlChildNo.SelectedIndex = 0;
                ddlChildNo2.SelectedIndex = 0;
                ddlChildNo3.SelectedIndex = 0;
                ddlChildNo4.SelectedIndex = 0;
                ddlChildNo5.SelectedIndex = 0;
                ddlChildNo6.SelectedIndex = 0;
                ddlChildNo7.SelectedIndex = 0;
                ddlChildNo8.SelectedIndex = 0;
                ddlChildNo9.SelectedIndex = 0;
                ddlChildNo10.SelectedIndex = 0;
                ddlChildNo11.SelectedIndex = 0;
                ddlChildNo12.SelectedIndex = 0;
                ddlChildNo13.SelectedIndex = 0;
                ddlChildNo14.SelectedIndex = 0;
                ddlChildNo15.SelectedIndex = 0;
                ddlChildNo16.SelectedIndex = 0;
                ddlChildNo17.SelectedIndex = 0;
                ddlChildNo18.SelectedIndex = 0;
                ddlChildNo19.SelectedIndex = 0;
                ddlChildNo20.SelectedIndex = 0;
                ddlChildNo21.SelectedIndex = 0;
                ddlChildNo22.SelectedIndex = 0;
                ddlChildNo23.SelectedIndex = 0;
                ddlChildNo24.SelectedIndex = 0;
                ddlChildNo25.SelectedIndex = 0;
                ddlChildNo26.SelectedIndex = 0;
                ddlChildNo27.SelectedIndex = 0;
                ddlChildNo28.SelectedIndex = 0;
                ddlChildNo29.SelectedIndex = 0;
                ddlChildNo30.SelectedIndex = 0;

                divChild2.Visible = false;
                divChild3.Visible = false;
                divChild4.Visible = false;
                divChild5.Visible = false;
                divChild6.Visible = false;
                divChild7.Visible = false;
                divChild8.Visible = false;
                divChild9.Visible = false;
                divChild10.Visible = false;
                divChild11.Visible = false;
                divChild12.Visible = false;
                divChild13.Visible = false;
                divChild14.Visible = false;
                divChild15.Visible = false;
                divChild16.Visible = false;
                divChild17.Visible = false;
                divChild18.Visible = false;
                divChild19.Visible = false;
                divChild20.Visible = false;
                divChild21.Visible = false;
                divChild22.Visible = false;
                divChild23.Visible = false;
                divChild24.Visible = false;
                divChild25.Visible = false;
                divChild26.Visible = false;
                divChild27.Visible = false;
                divChild28.Visible = false;
                divChild29.Visible = false;
                divChild30.Visible = false;

                DataSet ds = dal_DB.DB_FIELD_SP(ACTION: "SELECT_FIELD_CHILD",
                    ID: ddlFIELD.SelectedValue,
                    VAL_Child: ddlAnsTrigger.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlAnsTrigger.SelectedValue = ds.Tables[0].Rows[0]["VAL_Child"].ToString();

                    //if (ds.Tables[0].Rows[0]["CHILD_MODULE"].ToString() != "0" && ds.Tables[0].Rows[0]["CHILD_MODULE"].ToString() != "")
                    //{
                    //    ddlChildModule.SelectedValue = ds.Tables[0].Rows[0]["CHILD_MODULE"].ToString();
                    //}

                    if (ds.Tables[0].Rows[0]["CHILD_IDS"].ToString() != "0" && ds.Tables[0].Rows[0]["CHILD_IDS"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[0]["CHILD_IDS"].ToString().Contains(","))
                        {
                            string[] childValue = ds.Tables[0].Rows[0]["CHILD_IDS"].ToString().Split(',');

                            if (childValue.Length == 2)
                            {
                                divChild2.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                            }
                            else if (childValue.Length == 3)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                            }
                            else if (childValue.Length == 4)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                            }
                            else if (childValue.Length == 5)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                            }
                            else if (childValue.Length == 6)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                            }
                            else if (childValue.Length == 7)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                            }
                            else if (childValue.Length == 8)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                            }
                            else if (childValue.Length == 9)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                            }
                            else if (childValue.Length == 10)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                            }
                            else if (childValue.Length == 11)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                            }
                            else if (childValue.Length == 12)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                            }
                            else if (childValue.Length == 13)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                            }
                            else if (childValue.Length == 14)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                            }
                            else if (childValue.Length == 15)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                            }
                            else if (childValue.Length == 16)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                            }
                            else if (childValue.Length == 17)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                            }
                            else if (childValue.Length == 18)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                            }
                            else if (childValue.Length == 19)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                            }
                            else if (childValue.Length == 20)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                            }
                            else if (childValue.Length == 21)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;
                                divChild21.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;
                                ddlChildNo21.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                                ddlChildNo21.SelectedValue = childValue[20];
                            }
                            else if (childValue.Length == 22)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;
                                divChild21.Visible = true;
                                divChild22.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;
                                ddlChildNo21.Visible = true;
                                ddlChildNo22.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                                ddlChildNo21.SelectedValue = childValue[20];
                                ddlChildNo22.SelectedValue = childValue[21];
                            }
                            else if (childValue.Length == 23)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;
                                divChild21.Visible = true;
                                divChild22.Visible = true;
                                divChild23.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;
                                ddlChildNo21.Visible = true;
                                ddlChildNo22.Visible = true;
                                ddlChildNo23.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                                ddlChildNo21.SelectedValue = childValue[20];
                                ddlChildNo22.SelectedValue = childValue[21];
                                ddlChildNo23.SelectedValue = childValue[21];
                            }
                            else if (childValue.Length == 24)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;
                                divChild21.Visible = true;
                                divChild22.Visible = true;
                                divChild23.Visible = true;
                                divChild24.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;
                                ddlChildNo21.Visible = true;
                                ddlChildNo22.Visible = true;
                                ddlChildNo23.Visible = true;
                                ddlChildNo24.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                                ddlChildNo21.SelectedValue = childValue[20];
                                ddlChildNo22.SelectedValue = childValue[21];
                                ddlChildNo23.SelectedValue = childValue[22];
                                ddlChildNo24.SelectedValue = childValue[23];
                            }
                            else if (childValue.Length == 25)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;
                                divChild21.Visible = true;
                                divChild22.Visible = true;
                                divChild23.Visible = true;
                                divChild24.Visible = true;
                                divChild25.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;
                                ddlChildNo21.Visible = true;
                                ddlChildNo22.Visible = true;
                                ddlChildNo23.Visible = true;
                                ddlChildNo24.Visible = true;
                                ddlChildNo25.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                                ddlChildNo21.SelectedValue = childValue[20];
                                ddlChildNo22.SelectedValue = childValue[21];
                                ddlChildNo23.SelectedValue = childValue[22];
                                ddlChildNo24.SelectedValue = childValue[23];
                                ddlChildNo25.SelectedValue = childValue[24];
                            }
                            else if (childValue.Length == 26)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;
                                divChild21.Visible = true;
                                divChild22.Visible = true;
                                divChild23.Visible = true;
                                divChild24.Visible = true;
                                divChild25.Visible = true;
                                divChild26.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;
                                ddlChildNo21.Visible = true;
                                ddlChildNo22.Visible = true;
                                ddlChildNo23.Visible = true;
                                ddlChildNo24.Visible = true;
                                ddlChildNo25.Visible = true;
                                ddlChildNo26.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                                ddlChildNo21.SelectedValue = childValue[20];
                                ddlChildNo22.SelectedValue = childValue[21];
                                ddlChildNo23.SelectedValue = childValue[22];
                                ddlChildNo24.SelectedValue = childValue[23];
                                ddlChildNo25.SelectedValue = childValue[24];
                                ddlChildNo26.SelectedValue = childValue[25];
                            }
                            else if (childValue.Length == 27)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;
                                divChild21.Visible = true;
                                divChild22.Visible = true;
                                divChild23.Visible = true;
                                divChild24.Visible = true;
                                divChild25.Visible = true;
                                divChild26.Visible = true;
                                divChild27.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;
                                ddlChildNo21.Visible = true;
                                ddlChildNo22.Visible = true;
                                ddlChildNo23.Visible = true;
                                ddlChildNo24.Visible = true;
                                ddlChildNo25.Visible = true;
                                ddlChildNo26.Visible = true;
                                ddlChildNo27.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                                ddlChildNo21.SelectedValue = childValue[20];
                                ddlChildNo22.SelectedValue = childValue[21];
                                ddlChildNo23.SelectedValue = childValue[22];
                                ddlChildNo24.SelectedValue = childValue[23];
                                ddlChildNo25.SelectedValue = childValue[24];
                                ddlChildNo26.SelectedValue = childValue[25];
                                ddlChildNo27.SelectedValue = childValue[26];
                            }
                            else if (childValue.Length == 28)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;
                                divChild21.Visible = true;
                                divChild22.Visible = true;
                                divChild23.Visible = true;
                                divChild24.Visible = true;
                                divChild25.Visible = true;
                                divChild26.Visible = true;
                                divChild27.Visible = true;
                                divChild28.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;
                                ddlChildNo21.Visible = true;
                                ddlChildNo22.Visible = true;
                                ddlChildNo23.Visible = true;
                                ddlChildNo24.Visible = true;
                                ddlChildNo25.Visible = true;
                                ddlChildNo26.Visible = true;
                                ddlChildNo27.Visible = true;
                                ddlChildNo28.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                                ddlChildNo21.SelectedValue = childValue[20];
                                ddlChildNo22.SelectedValue = childValue[21];
                                ddlChildNo23.SelectedValue = childValue[22];
                                ddlChildNo24.SelectedValue = childValue[23];
                                ddlChildNo25.SelectedValue = childValue[24];
                                ddlChildNo26.SelectedValue = childValue[25];
                                ddlChildNo27.SelectedValue = childValue[26];
                                ddlChildNo28.SelectedValue = childValue[27];
                            }
                            else if (childValue.Length == 29)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;
                                divChild21.Visible = true;
                                divChild22.Visible = true;
                                divChild23.Visible = true;
                                divChild24.Visible = true;
                                divChild25.Visible = true;
                                divChild26.Visible = true;
                                divChild27.Visible = true;
                                divChild28.Visible = true;
                                divChild29.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;
                                ddlChildNo21.Visible = true;
                                ddlChildNo22.Visible = true;
                                ddlChildNo23.Visible = true;
                                ddlChildNo24.Visible = true;
                                ddlChildNo25.Visible = true;
                                ddlChildNo26.Visible = true;
                                ddlChildNo27.Visible = true;
                                ddlChildNo28.Visible = true;
                                ddlChildNo29.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                                ddlChildNo21.SelectedValue = childValue[20];
                                ddlChildNo22.SelectedValue = childValue[21];
                                ddlChildNo23.SelectedValue = childValue[22];
                                ddlChildNo24.SelectedValue = childValue[23];
                                ddlChildNo25.SelectedValue = childValue[24];
                                ddlChildNo26.SelectedValue = childValue[25];
                                ddlChildNo27.SelectedValue = childValue[26];
                                ddlChildNo28.SelectedValue = childValue[27];
                                ddlChildNo29.SelectedValue = childValue[28];
                            }
                            else if (childValue.Length == 30)
                            {
                                divChild2.Visible = true;
                                divChild3.Visible = true;
                                divChild4.Visible = true;
                                divChild5.Visible = true;
                                divChild6.Visible = true;
                                divChild7.Visible = true;
                                divChild8.Visible = true;
                                divChild9.Visible = true;
                                divChild10.Visible = true;
                                divChild11.Visible = true;
                                divChild12.Visible = true;
                                divChild13.Visible = true;
                                divChild14.Visible = true;
                                divChild15.Visible = true;
                                divChild16.Visible = true;
                                divChild17.Visible = true;
                                divChild18.Visible = true;
                                divChild19.Visible = true;
                                divChild20.Visible = true;
                                divChild21.Visible = true;
                                divChild22.Visible = true;
                                divChild23.Visible = true;
                                divChild24.Visible = true;
                                divChild25.Visible = true;
                                divChild26.Visible = true;
                                divChild27.Visible = true;
                                divChild28.Visible = true;
                                divChild29.Visible = true;
                                divChild30.Visible = true;

                                ddlChildNo.Visible = true;
                                ddlChildNo2.Visible = true;
                                ddlChildNo3.Visible = true;
                                ddlChildNo4.Visible = true;
                                ddlChildNo5.Visible = true;
                                ddlChildNo6.Visible = true;
                                ddlChildNo7.Visible = true;
                                ddlChildNo8.Visible = true;
                                ddlChildNo9.Visible = true;
                                ddlChildNo10.Visible = true;
                                ddlChildNo11.Visible = true;
                                ddlChildNo12.Visible = true;
                                ddlChildNo13.Visible = true;
                                ddlChildNo14.Visible = true;
                                ddlChildNo15.Visible = true;
                                ddlChildNo16.Visible = true;
                                ddlChildNo17.Visible = true;
                                ddlChildNo18.Visible = true;
                                ddlChildNo19.Visible = true;
                                ddlChildNo20.Visible = true;
                                ddlChildNo21.Visible = true;
                                ddlChildNo22.Visible = true;
                                ddlChildNo23.Visible = true;
                                ddlChildNo24.Visible = true;
                                ddlChildNo25.Visible = true;
                                ddlChildNo26.Visible = true;
                                ddlChildNo27.Visible = true;
                                ddlChildNo28.Visible = true;
                                ddlChildNo29.Visible = true;
                                ddlChildNo30.Visible = true;

                                ddlChildNo.SelectedValue = childValue[0];
                                ddlChildNo2.SelectedValue = childValue[1];
                                ddlChildNo3.SelectedValue = childValue[2];
                                ddlChildNo4.SelectedValue = childValue[3];
                                ddlChildNo5.SelectedValue = childValue[4];
                                ddlChildNo6.SelectedValue = childValue[5];
                                ddlChildNo7.SelectedValue = childValue[6];
                                ddlChildNo8.SelectedValue = childValue[7];
                                ddlChildNo9.SelectedValue = childValue[8];
                                ddlChildNo10.SelectedValue = childValue[9];
                                ddlChildNo11.SelectedValue = childValue[10];
                                ddlChildNo12.SelectedValue = childValue[11];
                                ddlChildNo13.SelectedValue = childValue[12];
                                ddlChildNo14.SelectedValue = childValue[13];
                                ddlChildNo15.SelectedValue = childValue[14];
                                ddlChildNo16.SelectedValue = childValue[15];
                                ddlChildNo17.SelectedValue = childValue[16];
                                ddlChildNo18.SelectedValue = childValue[17];
                                ddlChildNo19.SelectedValue = childValue[18];
                                ddlChildNo20.SelectedValue = childValue[19];
                                ddlChildNo21.SelectedValue = childValue[20];
                                ddlChildNo22.SelectedValue = childValue[21];
                                ddlChildNo23.SelectedValue = childValue[22];
                                ddlChildNo24.SelectedValue = childValue[23];
                                ddlChildNo25.SelectedValue = childValue[24];
                                ddlChildNo26.SelectedValue = childValue[25];
                                ddlChildNo27.SelectedValue = childValue[26];
                                ddlChildNo28.SelectedValue = childValue[27];
                                ddlChildNo29.SelectedValue = childValue[28];
                                ddlChildNo30.SelectedValue = childValue[29];
                            }

                        }
                        else
                        {
                            ddlChildNo.SelectedValue = ds.Tables[0].Rows[0]["CHILD_IDS"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //For Treeview of Field Mapings

        private void GetTreeview()
        {
            try
            {
                DataSet ds = dal_DB.DB_MAP_SP(ACTION: "GET_FIELD_TREE", MODULEID: drpModule.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    treeFieldMap.Nodes.Clear();
                    PopulateTreeView(ds.Tables[0], 0, null);
                    lbtnViewMap.Visible = true;

                }
                else
                {
                    treeFieldMap.Nodes.Clear();
                    lbtnViewMap.Visible = false;
                }

                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    lnkClearMapping.Visible = true;
                }
                else
                {
                    lnkClearMapping.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["Field"].ToString(),
                    Value = row["ID"].ToString(),
                    SelectAction = TreeNodeSelectAction.None
                };

                if (int.Parse(row["ParentID"].ToString()) == 0)
                {
                    treeFieldMap.Nodes.Add(child);
                    DataSet ds1 = dal_DB.DB_MAP_SP(ACTION: "GET_FIELD_TREE_CHILD", MODULEID: drpModule.SelectedValue, ID: child.Value.ToString());

                    PopulateTreeView_CHILD1(ds1.Tables[0], int.Parse(row["ParentID"].ToString()), child);
                }
                else
                {
                    treeNode.ChildNodes.Add(child);
                }
            }
        }

        private void PopulateTreeView_CHILD1(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["Field"].ToString(),
                    Value = row["ID"].ToString(),
                    SelectAction = TreeNodeSelectAction.None
                };

                treeNode.ChildNodes.Add(child);

                DataSet ds1 = dal_DB.DB_MAP_SP(ACTION: "GET_FIELD_TREE_CHILD", MODULEID: drpModule.SelectedValue, ID: child.Value.ToString());

                PopulateTreeView_CHILD2(ds1.Tables[0], int.Parse(row["ParentID"].ToString()), child);
            }
        }

        private void PopulateTreeView_CHILD2(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["Field"].ToString(),
                    Value = row["ID"].ToString(),
                    SelectAction = TreeNodeSelectAction.None
                };

                treeNode.ChildNodes.Add(child);

                DataSet ds1 = dal_DB.DB_MAP_SP(ACTION: "GET_FIELD_TREE_CHILD", MODULEID: drpModule.SelectedValue, ID: child.Value.ToString());
                PopulateTreeView_CHILD3(ds1.Tables[0], int.Parse(row["ParentID"].ToString()), child);
            }
        }

        private void PopulateTreeView_CHILD3(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["Field"].ToString(),
                    Value = row["ID"].ToString(),
                    SelectAction = TreeNodeSelectAction.None
                };

                treeNode.ChildNodes.Add(child);
            }
        }
        //End Treeview of Field Mapings

        protected void btnSubmitModuleField_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["ID"] != null)
                {
                    UPDATE_FIELD();
                }
                else
                {
                    ADD_FIELDS();
                }

                for (int i = 0; i < grdvisitSubmit.Rows.Count; i++)
                {
                    CheckBox Chk_VISIT = (CheckBox)grdvisitSubmit.Rows[i].FindControl("Chk_VISIT");

                    if (Chk_VISIT.Checked == true)
                    {
                        Label VISITNUM = (Label)grdvisitSubmit.Rows[i].FindControl("VISITNUM");

                        DataSet ds = dal_DB.DB_DM_SP(ACTION: "INSERT_VISIT_FIELD",
                        VARIABLENAME: txtVariableName.Text,
                        MODULEID: drpModule.SelectedValue,
                        VISITNUM: VISITNUM.Text
                        );

                        string PGL_TYPE = "";
                        if (drp_PGL_Type.SelectedValue != "0")
                        {
                            PGL_TYPE = drp_PGL_Type.SelectedValue;
                        }

                        dal_DB.DB_DM_SP(ACTION: "ADD_OPITONS_VAR_BY_OLDVAR_VISIT",
                            OLD_VARIABLENAME: hfOLDVARIABLENAME.Value,
                            VARIABLENAME: txtVariableName.Text,
                            VISITNUM: VISITNUM.Text,
                            MODULEID: drpModule.SelectedValue,
                            PGL_TYPE: PGL_TYPE
                            );
                    }
                }

                btnSubmitField.Visible = true;
                btnCancelField.Visible = true;
                btnUpdateField.Visible = false;
                lnkClearMapping.Visible = true;

                ClearFieldSection();
                GetField();
                MODULE_STATUS();
                GetTreeview();
                showFeatures();
                drpModule_SelectedIndexChanged(this, e);
                Session.Remove("ID");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The field has been successfully added, and the addition has also been reflected at the visit level.');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelModuleField_Click(object sender, EventArgs e)
        {
            try
            {
                ClearFieldSection();
                GetField();
                MODULE_STATUS();
                GetTreeview();
                showFeatures();
                drpModule_SelectedIndexChanged(this, e);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The field addition action has been canceled. Please enter the details again to add the field.');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateVisitField_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["ID"] != null)
                {
                    UPDATE_FIELD();
                }
                else
                {
                    ADD_FIELDS();
                }

                for (int i = 0; i < grdVisitUpdate.Rows.Count; i++)
                {
                    Label VISITNUM = (Label)grdVisitUpdate.Rows[i].FindControl("VISITNUM");

                    dal_DB.DB_DM_SP(ACTION: "UPDATE_OPITONS_VAR_BY_OLDVAR_VISIT",
                        OLD_VARIABLENAME: hfOLDVARIABLENAME.Value,
                        VARIABLENAME: txtVariableName.Text,
                        VISITNUM: VISITNUM.Text,
                        MODULEID: drpModule.SelectedValue
                        );

                    dal_DB.DB_DM_SP(ACTION: "UPDATE_VISIT_FIELD",
                        VARIABLENAME: txtVariableName.Text,
                        MODULEID: drpModule.SelectedValue,
                        VISITNUM: VISITNUM.Text,
                        ID: Session["ID"].ToString()
                        );
                }

                btnSubmitField.Visible = true;
                btnUpdateField.Visible = false;
                btnCancelField.Visible = true;
                lnkClearMapping.Visible = true;
                Session.Remove("ID");
                ClearFieldSection();
                GetField();
                MODULE_STATUS();
                GetTreeview();
                showFeatures();
                drpModule_SelectedIndexChanged(this, e);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The field has been successfully updated and the update has also been reflected at the visit level.');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelVisitField_Click(object sender, EventArgs e)
        {
            try
            {
                ClearFieldSection();
                GetField();
                MODULE_STATUS();
                GetTreeview();
                showFeatures();
                drpModule_SelectedIndexChanged(this, e);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The action to update the field has been canceled. Please enter the details again to modify the field.');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdField_PreRender(object sender, EventArgs e)
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

        protected void btnSendToReview_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_SYSTEMS_BYMODULE_FOR_REVIEW_PROCESS",
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblReviewHeader.InnerText = drpModule.SelectedItem.Text + " module is available at below systems.";

                    lstSystems.DataSource = ds.Tables[0];
                    lstSystems.DataBind();
                }
                else
                {
                    lstSystems.DataSource = null;
                    lstSystems.DataBind();
                }

                updpnlSendforreview.Update();
                ModalPopupExtender3.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitReview_Click(object sender, EventArgs e)
        {
            try
            {
                string Systems = "";

                for (int i = 0; i < lstSystems.Items.Count; i++)
                {
                    dal_DB.DB_REVIEW_SP(ACTION: "INSERT_MODULE_SEND_TO_REVIEW",
                        MODULEID: drpModule.SelectedValue,
                        SYSTEM: (lstSystems.Items[i].FindControl("lblSystemName") as Label).Text,
                        SEND_TO_REVIEW: true,
                        REVIEW: false,
                        REASON: txtReason.Text
                        );

                    if (Systems == "")
                    {
                        Systems = (lstSystems.Items[i].FindControl("lblSystemName") as Label).Text;
                    }
                    else
                    {
                        Systems += ", " + (lstSystems.Items[i].FindControl("lblSystemName") as Label).Text;
                    }
                }

                SendEmail("Send For Review", txtReason.Text);

                string MSG = drpModule.SelectedItem.Text + " has been sent for review for " + Systems + " System.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + MSG + "'); window.location.href = '" + Request.RawUrl.ToString() + "' ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtReason.Text = "";
                ModalPopupExtender3.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnOpenForEdit_Click(object sender, EventArgs e)
        {
            try
            {
                txtOpenForEditReason.Attributes.Add("MaxLength", "500");

                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_SYSTEMS_BYMODULE_FOR_REVIEW_PROCESS",
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblEditForEditHeader.InnerText = drpModule.SelectedItem.Text + " module is available at below systems.";

                    lstOpenForEditSystems.DataSource = ds.Tables[0];
                    lstOpenForEditSystems.DataBind();
                }
                else
                {
                    lstOpenForEditSystems.DataSource = null;
                    lstOpenForEditSystems.DataBind();
                }

                updPnlIDDetail.Update();
                ModalPopupExtender4.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitEditForEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string Systems = "";

                for (int i = 0; i < lstOpenForEditSystems.Items.Count; i++)
                {
                    dal_DB.DB_REVIEW_SP(ACTION: "UPDATE_MODULE_OPEN_FOR_EDIT",
                        MODULEID: drpModule.SelectedValue,
                        SYSTEM: (lstOpenForEditSystems.Items[i].FindControl("lblOpenForEditSystemName") as Label).Text,
                        SEND_TO_REVIEW: false,
                        REVIEW: false,
                        REASON: txtOpenForEditReason.Text
                        );

                    if (Systems == "")
                    {
                        Systems = (lstOpenForEditSystems.Items[i].FindControl("lblOpenForEditSystemName") as Label).Text;
                    }
                    else
                    {
                        Systems += ", " + (lstOpenForEditSystems.Items[i].FindControl("lblOpenForEditSystemName") as Label).Text;
                    }
                }

                SendEmail("Open For Edit From Designer", txtOpenForEditReason.Text);

                string MSG = drpModule.SelectedItem.Text + " has been open for edit for " + Systems + " System.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + MSG + "'); window.location.href = '" + Request.RawUrl.ToString() + "' ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelEditForEdit_Click(object sender, EventArgs e)
        {
            try
            {
                txtOpenForEditReason.Text = "";
                ModalPopupExtender4.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SendEmail(string ACTIONS, string REASON)
        {
            try
            {
                CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();

                DataSet dsSystem = dal_DB.DB_REVIEW_SP(ACTION: "GET_SYSTEMS_BYMODULE",
                    MODULEID: drpModule.SelectedValue
                    );

                if (dsSystem.Tables.Count > 0 && dsSystem.Tables[0].Rows.Count > 0)
                {
                    DataSet ds = dal_DB.DB_EMAIL_REVIEW_SP(ACTION: "GET_EMAILSIDS",
                        ACTIVITY: ACTIONS,
                        SYSTEMS: dsSystem.Tables[0].Rows[0]["SystemName"].ToString()
                        );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string EMAILID = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();

                        string EMAIL_CC = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();

                        string EMAIL_BCC = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();

                        string EmailSubject = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

                        string EmailBody = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();

                        if (EmailSubject.Contains("[") && EmailSubject.Contains("]"))
                        {
                            if (EmailSubject.Contains("[PROJECTID]"))
                            {
                                EmailSubject = EmailSubject.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                            }

                            if (EmailSubject.Contains("[MODULENAME]"))
                            {
                                EmailSubject = EmailSubject.Replace("[MODULENAME]", drpModule.SelectedItem.Text);
                            }

                            if (EmailSubject.Contains("[COMMENT]"))
                            {
                                EmailSubject = EmailSubject.Replace("[COMMENT]", REASON);
                            }

                            if (EmailSubject.Contains("[SYSTEM]"))
                            {
                                EmailSubject = EmailSubject.Replace("[SYSTEM]", dsSystem.Tables[0].Rows[0]["SystemName"].ToString());
                            }

                            if (EmailSubject.Contains("[USER]"))
                            {
                                EmailSubject = EmailSubject.Replace("[USER]", Session["User_Name"].ToString());
                            }

                            if (EmailSubject.Contains("[DATETIME]"))
                            {
                                EmailSubject = EmailSubject.Replace("[DATETIME]", comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                            }

                            if (EmailSubject.Contains("[") && EmailSubject.Contains("]"))
                            {
                                if (ds.Tables.Count > 1)
                                {
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        foreach (DataColumn dc in ds.Tables[1].Columns)
                                        {
                                            if (EmailSubject.Contains("[" + dc.ToString() + "]"))
                                            {
                                                EmailSubject = EmailSubject.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (EmailBody.Contains("[") && EmailBody.Contains("]"))
                        {
                            if (EmailBody.Contains("[MODULENAME]"))
                            {
                                EmailBody = EmailBody.Replace("[MODULENAME]", drpModule.SelectedItem.Text);
                            }

                            if (EmailBody.Contains("[SYSTEM]"))
                            {
                                EmailBody = EmailBody.Replace("[SYSTEM]", dsSystem.Tables[0].Rows[0]["SystemName"].ToString());
                            }

                            if (EmailBody.Contains("[COMMENT]"))
                            {
                                EmailBody = EmailBody.Replace("[COMMENT]", REASON);
                            }

                            if (EmailBody.Contains("[PROJECTID]"))
                            {
                                EmailBody = EmailBody.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                            }

                            if (EmailBody.Contains("[USER]"))
                            {
                                EmailBody = EmailBody.Replace("[USER]", Session["User_Name"].ToString());
                            }

                            if (EmailBody.Contains("[DATETIME]"))
                            {
                                EmailBody = EmailBody.Replace("[DATETIME]", comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                            }

                            if (EmailBody.Contains("[") && EmailBody.Contains("]"))
                            {
                                if (ds.Tables.Count > 1)
                                {
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        foreach (DataColumn dc in ds.Tables[1].Columns)
                                        {
                                            if (EmailBody.Contains("[" + dc.ToString() + "]"))
                                            {
                                                EmailBody = EmailBody.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        comm.Email_Users(EMAILID, EMAIL_CC, EmailSubject, EmailBody, EMAIL_BCC);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;

            }
        }

        [System.Web.Services.WebMethod]
        public static string REVIEW_HISTORY(string MODULEID)
        {
            string str = "";
            try
            {
                DAL_DB dal_DB = new DAL_DB();

                DataSet ds = dal_DB.DB_REVIEW_SP(
                    ACTION: "GET_REVIEW_LOGS",
                    MODULEID: MODULEID
                    );

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML(ds);
                    }
                    else
                    {
                        str = "No Record Available.";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return str;
        }

        public static string ConvertDataTableToHTML(DataSet ds)
        {
            string html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                html += "<th scope='col'>" + ds.Tables[0].Columns[i].ColumnName + "</th>";
            html += "</tr>";

            //add rows
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                html += "<tr style='text-align: left;'>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        protected void txtVariableName_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtVariableName.Text, "^[a-zA-Z ]"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This textbox accepts only alphabetical characters'); ", true);
                txtVariableName.Text = "";
            }
            else if (txtVariableName.Text.Contains(" "))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter the variable name without spaces'); ", true);
                txtVariableName.Text = "";
            }
            else if (txtVariableName.Text.Contains("-"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter the variable name without -'); ", true);
                txtVariableName.Text = "";
            }
        }

        protected void lbtnExportSpecs_Click(object sender, EventArgs e)
        {
            try
            {
                string MODULEID = "";
                DataSet dsModule = new DataSet();

                if (drpSystem.SelectedValue != "0" && drpModule.SelectedValue != "0")
                {
                    MODULEID = drpModule.SelectedValue;
                }
                else if (drpSystem.SelectedValue != "0" && drpModule.SelectedValue == "0")
                {
                    dsModule = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULENAME_BY_SYSTEM", SYSTEM: drpSystem.SelectedValue);

                    for (int j = 0; j < dsModule.Tables[0].Rows.Count; j++)
                    {
                        if (MODULEID == "")
                        {
                            MODULEID = dsModule.Tables[0].Rows[j]["ID"].ToString();
                        }
                        else
                        {
                            MODULEID += " ," + dsModule.Tables[0].Rows[j]["ID"].ToString();
                        }
                    }
                }
                else if (drpSystem.SelectedValue == "0" && drpModule.SelectedValue == "")
                {
                    DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_USER_SYSTEMS");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dsModule = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULENAME_BY_SYSTEM", SYSTEM: ds.Tables[0].Rows[i]["SystemName"].ToString());

                        for (int j = 0; j < dsModule.Tables[0].Rows.Count; j++)
                        {
                            if (MODULEID == "")
                            {
                                MODULEID = dsModule.Tables[0].Rows[j]["ID"].ToString();
                            }
                            else
                            {
                                MODULEID += " ," + dsModule.Tables[0].Rows[j]["ID"].ToString();
                            }
                        }
                    }
                }

                DataSet ds2 = dal_DB.DB_FIELD_SP(ACTION: "GET_MODULE_SPECS_EXPORT",
                    MODULEID: MODULEID
                    );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_CRF Specifications_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds2.Tables.Count; i++)
                {
                    ds2.Tables[i].TableName = ds2.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds2.Tables[i].Copy());
                    i++;
                }

                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public static string RemoveDuplicates(string input)
        {
            return new string(input.ToCharArray().Distinct().ToArray());
        }

        protected void chkParentLinked_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkParentLinked.Checked)
                {
                    chkChildLinked.Enabled = false;
                }
                else
                {
                    chkChildLinked.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkChildLinked_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkChildLinked.Checked)
                {
                    chkParentLinked.Enabled = false;
                }
                else
                {
                    chkParentLinked.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnExportCrfIntruc_Click(object sender, EventArgs e)
        {

            DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_DATA_INSTRUCTIONS", MODULEID: drpModule.SelectedValue);
            DataTable dataTable = ds.Tables[0];

            ExportToWordInMemory(dataTable);

        }

        public string StripHtml(string html)
        {

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            string plainText = document.DocumentNode.InnerText;

            return WebUtility.HtmlDecode(plainText);
        }

        public void ExportToWordInMemory(DataTable dataTable)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(memoryStream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
                    {
                        MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                        mainPart.Document = new Document(new Body());

                        Body body = mainPart.Document.Body;

                        foreach (DataRow row in dataTable.Rows)
                        {
                            string ModuleName = StripHtml(row["MODULENAME"].ToString());
                            string CRF_DATA = StripHtml(row["DATA"].ToString());
                            string SAE_DATA = StripHtml(row["SAE_DATA"].ToString());

                            string[] CRF_Data = CRF_DATA.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                            string[] SAE_Data = SAE_DATA.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);


                            body.Append(new Paragraph(new Run(new RunProperties(new Bold()), new Text("Module Name:")), new Run(new Text(" ")), new Run(new Text(ModuleName))));


                            body.Append(new Paragraph(new Run(new RunProperties(new Bold()), new Text("CRF Help (Instructions):"))));


                            foreach (string paragraphText in CRF_Data)
                            {
                                if (!string.IsNullOrWhiteSpace(paragraphText))
                                {
                                    Paragraph data_crf = new Paragraph(new Run(new Text(paragraphText)));
                                    body.Append(data_crf);
                                }
                            }


                            body.Append(new Paragraph(new Run(new RunProperties(new Bold()), new Text("Pharmacovigilance Help (Instructions):"))));


                            foreach (string paragraphText in SAE_Data)
                            {
                                if (!string.IsNullOrWhiteSpace(paragraphText))
                                {
                                    Paragraph data_Sae = new Paragraph(new Run(new Text(paragraphText)));
                                    body.Append(data_Sae);
                                }
                            }


                            body.Append(new Paragraph(new Run(new Break() { Type = BreakValues.Page })));
                        }
                    }


                    memoryStream.Seek(0, SeekOrigin.Begin);


                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=IntructionsData.docx");
                    HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
                    HttpContext.Current.Response.End();
                }
            }
            catch (Exception ex)
            {

                lblErrorMsg.Text = ex.Message.ToString();
            }


        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            // Check which radio button is selected
            bool isPermanentDelete = rdoPermanentDelete.Checked;
            bool isProspectiveDelete = rdoProspectiveDelete.Checked;

            // If all are valid, proceed:

            string id = hdnDeleteFieldID.Value;
            // Make sure ViewState["VARIABLENAME"] is set (or store it in another hidden field)
            string varName = hdnVariableName.Value;
            string moduleId = drpModule.SelectedValue;
            string pglType = "Deleted";

            if (isPermanentDelete)
            {
                // Perform permanent delete operation


                DataSet ds = dal_DB.DB_FIELD_SP(ACTION: "DELETE_MODULEFIELD",
                        ID: id,
                        MODULEID: moduleId,
                        VARIABLENAME: varName
                    );

                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field deleted successfully');", true);



                GetField();
                MODULE_STATUS();
                GetTreeview();
                drpModule_SelectedIndexChanged(this, e);

                // Clear the radio button selection
                rdoPermanentDelete.Checked = false;
                rdoProspectiveDelete.Checked = false;

                // Update the GridView UpdatePanel
                UpdatePanel1.Update();

                // Update the modal UpdatePanel (if necessary)
                //  upModalPopup.Update();

                // Show success message and hide modal without reloading the page
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndAlert", @"alert('Field deleted successfully'); $find('" + mpeDeleteConfirmation.ClientID + @"').hide();", true);

                //  Response.Redirect(Request.RawUrl);

            }
            else if (isProspectiveDelete)
            {
                DataSet ds = dal_DB.DB_FIELD_SP(ACTION: "UPDATE_PGL_TYPE",
                   ID: id,
                   MODULEID: moduleId,
                   VARIABLENAME: varName,
                   PGL_TYPE: pglType
                 );

                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PGL Type updated successfully');", true);



                GetField();
                MODULE_STATUS();
                GetTreeview();
                drpModule_SelectedIndexChanged(this, e);


                // Clear the radio button selection
                rdoPermanentDelete.Checked = false;
                rdoProspectiveDelete.Checked = false;
                // Update the GridView UpdatePanel
                UpdatePanel1.Update();

                // Update the modal UpdatePanel (if necessary)
                //  upModalPopup.UpdategrdField_PreRender

                // Close the modal and show success message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndAlert", @" alert('PGL Type updated successfully');  $find('" + mpeDeleteConfirmation.ClientID + @"').hide();", true);

                //// Optionally rebind your GridView or refresh the page
                // Response.Redirect(Request.RawUrl);
            }
        }

        [WebMethod]
        public static bool CheckStatusLogs(string ENTEREDDAT)
        {
            if (HttpContext.Current.Session["DB_STATUS_LOGS_LAST_DAT"] != null)
            {
                if (Convert.ToDateTime(ENTEREDDAT) <= Convert.ToDateTime(HttpContext.Current.Session["DB_STATUS_LOGS_LAST_DAT"].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            //// Check if the DataSet and DataTable are valid
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    // Assuming your SQL query now returns a count of rows
            //    int count = Convert.ToInt32(ds.Tables[0].Rows[0]["DB_STATUS_LOGS_COUNT"]);  // Replace "COUNTS" with your actual column name

            //    // Return true if count is greater than 0
            //    return count > 0;
            //}

            //return false;
        }

        protected void btnCancelDelete_Click(object sender, EventArgs e)
        {
            // Clear the radio button selection
            rdoPermanentDelete.Checked = false;
            rdoProspectiveDelete.Checked = false;
            // Hide the modal explicitly
            mpeDeleteConfirmation.Hide();
            // Update the UpdatePanel
            upModalPopup.Update();
        }

        protected void lnkClearMapping_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_MAP_SP(ACTION: "GET_FIELD_TREE", MODULEID: drpModule.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    DataSet ds1 = dal_DB.DB_MAP_SP(ACTION: "Clear_Mapping",
                                  MODULEID: drpModule.SelectedValue);

                    GetField();
                    MODULE_STATUS();
                    GetTreeview();
                    drpModule_SelectedIndexChanged(this, e);

                    // Show success message and hide modal without reloading the page
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Mapping Cleared Successfully!'); ", true);
                }

                GetTreeview();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void chkRead_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRead.Checked == true)
            {
                divSDV.Visible = false;
            }
            else
            {
                divSDV.Visible = true;
            }
        }

        protected void chkSDV_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSDV.Checked == true)
            {
                divCriticalDP.Visible = true;
            }
            else
            {
                divCriticalDP.Visible = false;
                chkCriticDP.Checked = false;
            }
        }
    }
}
