using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_DataEntry : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        DataTable dt_AuditTrail = new DataTable("DM_AUDITTRAIL");
        DataTable dt_AutoCode = new DataTable("dt_AutoCode");
        DataTable dt_Ref_Note = new DataTable();
        DataTable dtCurrentDATA = new DataTable();
        DataTable dtDropDownList = new DataTable();
        CommonFunction.CommonFunction_DM commFun = new CommonFunction.CommonFunction_DM();
        CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtDeletedReason.Attributes.Add("MaxLength", "500");
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.IsPostBack)
                {
                    FillSubject();
                    GetVisit();
                    GetModule();

                    if (Request.QueryString["OPENLINK"] == "1")
                    {
                        GOTO_OPENLINK_REFRENCE();
                    }
                    else
                    {
                        hdnPVID.Value = Session["PROJECTID"].ToString() + "-" + Request.QueryString["INVID"].ToString() + "-" + Request.QueryString["SUBJID"].ToString() + "-" + Request.QueryString["VISITID"].ToString() + "-" + Request.QueryString["MODULEID"].ToString() + "-" + 1;
                        hdn_PAGESTATUS.Value = "0";

                        lblPVID.Text = hdnPVID.Value;
                    }

                    lblSiteId.Text = "SITE ID : " + Request.QueryString["INVID"].ToString();
                    lblSubjectId.Text = "SUBJECT ID : " + Request.QueryString["SUBJID"].ToString();
                    lblVisit.Text = "VISIT : " + Request.QueryString["VISIT"].ToString();

                    hdnVISITID.Value = Request.QueryString["VISITID"].ToString();

                    if (drpModule.SelectedIndex != 0)
                    {
                        if (Request.QueryString["RECID"] == null)
                        {
                            hdnRECID.Value = "-1";
                        }
                        else
                        {
                            hdnRECID.Value = Request.QueryString["RECID"].ToString();
                        }

                        Get_Page_Status();
                        //GetStructure(grd_Data);
                        GetDataExists();
                        GetDataExists_Deleted();
                        GetQueryDetails();
                        GetAuditDetails();
                        GetCommentsetails();
                        GetOnPageSpecs();
                        GETHELPDATA();
                        GetSign_info();
                        CHECK_Reference_Note();

                        if (hdn_PAGESTATUS.Value == "0")
                        {
                            SET_VALUE_ONLOAD();
                        }
                    }
                    else
                    {
                        bntSaveComplete.Visible = false;
                        btnSaveIncomplete.Visible = false;
                        btnCancle.Visible = false;

                        btnNotApplicable.Visible = false;
                        btnModuleStatus.Visible = false;

                        btnDeleteData.CssClass = "disp-none";
                        DivDeletedRecords.Visible = false;
                        divSignOff.Visible = false;
                    }

                    if (Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                    {
                        btnDeleteData.CssClass = "disp-none";
                        btnSaveIncomplete.Visible = false;
                        bntSaveComplete.Visible = false;
                        btnCancle.Visible = true;
                    }

                    if (Convert.ToString(Session["LOCK_STATUS"]) != "" && hdnRECID.Value == "-1" && Convert.ToString(Request.QueryString["DELETED"]) != "1")
                    {
                        btnDeleteData.CssClass = "disp-none";
                        btnSaveIncomplete.Visible = false;
                        bntSaveComplete.Visible = false;
                        btnNotApplicable.Visible = false;
                        grd_Data.Visible = false;
                        divSignOff.Visible = false;
                        lblApplicableStatus.Visible = true;
                        lblApplicableStatus.Text = " || This module is locked.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal_DM.DM_SUBJECT_LIST_SP(INVID: Request.QueryString["INVID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    //drpSubID.Items.Insert(0, new ListItem("--Select--", "Select"));

                    if (Request.QueryString["SUBJID"] != null)
                    {
                        if (drpSubID.Items.FindByValue(Request.QueryString["SUBJID"].ToString()) != null)
                        {
                            drpSubID.Items.FindByValue(Request.QueryString["SUBJID"].ToString()).Selected = true;

                        }
                    }
                }
                else
                {
                    drpSubID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSubID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["RedirceToAnotherPage"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have unsaved changes. Please click on save complete button before navigating to another screen or tab.');", true);

                    if (Request.QueryString["SUBJID"] != null)
                    {
                        drpSubID.ClearSelection();
                        if (drpSubID.Items.FindByValue(Request.QueryString["SUBJID"].ToString()) != null)
                        {
                            drpSubID.Items.FindByValue(Request.QueryString["SUBJID"].ToString()).Selected = true;

                        }
                    }
                }
                else
                {
                    if (drpSubID.SelectedValue != "0")
                    {
                        DataSet ds = dal_DM.DM_MODULE_SP(ACTION: "GET_MODULE_MULTIPLE",
                            ID: drpModule.SelectedValue
                            );

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                            {
                                Response.Redirect("DM_DataEntry.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                            }
                            if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                            {
                                Response.Redirect("DM_DataEntry_MultipleData.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                            }
                        }
                        else
                        {
                            Response.Redirect("DM_DataEntry.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                        }
                    }
                    else
                    {
                        grd_Data.DataSource = null;
                        grd_Data.DataBind();

                        DivDeletedRecords.Visible = false;
                        divSignOff.Visible = false;

                        bntSaveComplete.Visible = false;
                        btnSaveIncomplete.Visible = false;
                        btnCancle.Visible = false;

                        btnNotApplicable.Visible = false;
                        btnModuleStatus.Visible = false;
                        btnDeleteData.CssClass = "disp-none";

                        repeat_AllModule.DataSource = null;
                        repeat_AllModule.DataBind();
                        divNote.Visible = false;
                    }

                    Session["DM_CRF_SUBJID"] = drpSubID.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GetVisit()
        {
            try
            {
                DataSet ds = dal_DM.DM_VISIT_DATA_SP(SUBJID: drpSubID.SelectedValue);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    if (dr["CritID"].ToString() != "" && dr["CritID"].ToString() != "0" && !toBeDeleted_Visit(dr["CritID"].ToString()) && drpSubID.SelectedValue != "Select")
                    {
                        dr.Delete();
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpVisit.DataSource = ds.Tables[0];
                    drpVisit.DataValueField = "VISITNUM";
                    drpVisit.DataTextField = "VISIT";
                    drpVisit.DataBind();
                    drpVisit.Items.Insert(0, new ListItem("--Select--", "Select"));

                    if (Request.QueryString["VISITID"] != null)
                    {
                        if (drpVisit.Items.FindByValue(Request.QueryString["VISITID"].ToString()) != null)
                        {
                            drpVisit.Items.FindByValue(Request.QueryString["VISITID"].ToString()).Selected = true;
                        }
                    }
                }
                else
                {
                    drpVisit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool toBeDeleted_Visit(string CritID)
        {
            bool res = false;
            try
            {
                DataSet ds = dal_DM.DM_VISIT_CRITERIA_SP(
                ID: CritID,
                SUBJID: drpSubID.SelectedValue,
                SITEID: Request.QueryString["INVID"].ToString()
                );

                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "1")
                {
                    res = true;
                }
                else
                {
                    res = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                res = false;
            }
            return res;
        }

        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["RedirceToAnotherPage"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have unsaved changes. Please click on save complete button before navigating to another screen or tab.');", true);

                    if (Request.QueryString["VISITID"] != null)
                    {
                        drpVisit.ClearSelection();
                        if (drpVisit.Items.FindByValue(Request.QueryString["VISITID"].ToString()) != null)
                        {
                            drpVisit.Items.FindByValue(Request.QueryString["VISITID"].ToString()).Selected = true;
                        }
                    }
                }
                else
                {
                    GetModule();

                    if (drpVisit.SelectedValue != "0")
                    {
                        DataSet ds = dal_DM.DM_MODULE_SP(ACTION: "GET_MODULE_MULTIPLE",
                            ID: drpModule.SelectedValue
                            );

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                            {
                                Response.Redirect("DM_DataEntry.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                            }
                            if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                            {
                                Response.Redirect("DM_DataEntry_MultipleData.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                            }
                        }
                        else
                        {
                            Response.Redirect("DM_DataEntry.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                        }
                    }
                    else
                    {
                        grd_Data.DataSource = null;
                        grd_Data.DataBind();

                        DivDeletedRecords.Visible = false;
                        divSignOff.Visible = false;

                        bntSaveComplete.Visible = false;
                        btnSaveIncomplete.Visible = false;
                        btnCancle.Visible = false;

                        btnNotApplicable.Visible = false;
                        btnModuleStatus.Visible = false;
                        btnDeleteData.CssClass = "disp-none";

                        repeat_AllModule.DataSource = null;
                        repeat_AllModule.DataBind();
                        divNote.Visible = false;
                    }

                    Session["DM_CRF_VISIT"] = drpVisit.SelectedValue;
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
                DataSet ds = dal_DM.DM_OPEN_CRF_SP(
                    SUBJID: drpSubID.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue
                    );

                for (int i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    if (dr["CritID"].ToString() != "" && dr["CritID"].ToString() != "0" && !toBeDeleted(dr["CritID"].ToString()))
                    {
                        dr.Delete();
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds.Tables[0];
                    drpModule.DataValueField = "MODULEID";
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataBind();

                    if (Request.QueryString["MODULEID"] != null)
                    {
                        if (drpModule.Items.FindByValue(Request.QueryString["MODULEID"].ToString()) != null)
                        {
                            drpModule.Items.FindByValue(Request.QueryString["MODULEID"].ToString()).Selected = true;
                        }
                    }

                    drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                    drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
                    lblModuleName.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool toBeDeleted(string CritID)
        {
            bool res = false;
            try
            {
                DataSet ds = dal_DM.DM_MODULE_CRITERIA_SP(
                ID: CritID,
                SUBJID: Request.QueryString["SUBJID"].ToString(),
                VISITNUM: Request.QueryString["VISITID"].ToString(),
                SITEID: Request.QueryString["INVID"].ToString()
                );

                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "1")
                {
                    res = true;
                }
                else
                {
                    res = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                res = false;
            }
            return res;
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["RedirceToAnotherPage"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have unsaved changes. Please click on save complete button before navigating to another screen or tab.');", true);

                    if (Request.QueryString["MODULEID"] != null)
                    {
                        drpModule.ClearSelection();
                        if (drpModule.Items.FindByValue(Request.QueryString["MODULEID"].ToString()) != null)
                        {
                            drpModule.Items.FindByValue(Request.QueryString["MODULEID"].ToString()).Selected = true;
                        }
                    }
                }
                else
                {
                    if (drpModule.SelectedValue != "0")
                    {
                        DataSet ds = dal_DM.DM_MODULE_SP(ACTION: "GET_MODULE_MULTIPLE",
                            ID: drpModule.SelectedValue
                            );

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                            {
                                Response.Redirect("DM_DataEntry.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                            }
                            if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                            {
                                Response.Redirect("DM_DataEntry_MultipleData.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                            }
                        }
                    }
                    else
                    {
                        grd_Data.DataSource = null;
                        grd_Data.DataBind();

                        DivDeletedRecords.Visible = false;
                        divSignOff.Visible = false;

                        bntSaveComplete.Visible = false;
                        btnSaveIncomplete.Visible = false;
                        btnCancle.Visible = false;

                        btnNotApplicable.Visible = false;
                        btnModuleStatus.Visible = false;
                        btnDeleteData.CssClass = "disp-none";

                        repeat_AllModule.DataSource = null;
                        repeat_AllModule.DataBind();
                        divNote.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        protected void GOTO_OPENLINK_REFRENCE()
        {
            try
            {
                string MODULEID = Request.QueryString["MODULEID"].ToString();
                string MODULENAME = Request.QueryString["MODULENAME"].ToString();
                string INVID = Request.QueryString["INVID"].ToString();
                string SUBJID = Request.QueryString["SUBJID"].ToString();

                DataSet ds = dal_DM.DM_GENERAL_SP(ACTION: "GET_VISIT_BY_MODULE", MODULEID: MODULEID);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string VISITID = "";
                    string VISIT = "";

                    VISITID = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                    if (VISITID != Request.QueryString["VISITID"].ToString())
                    {
                        VISIT = ds.Tables[0].Rows[0]["VISIT"].ToString();

                        if (Request.QueryString["REFERENCE"] != null)
                        {
                            Response.Redirect("DM_DataEntry.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1&REFERENCE=" + Request.QueryString["REFERENCE"].ToString());
                        }
                        else
                        {
                            Response.Redirect("DM_DataEntry.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1");
                        }
                    }
                }

                hdnPVID.Value = Session["PROJECTID"].ToString() + "-" + Request.QueryString["INVID"].ToString() + "-" + Request.QueryString["SUBJID"].ToString() + "-" + Request.QueryString["VISITID"].ToString() + "-" + Request.QueryString["MODULEID"].ToString() + "-" + 1;

                hdn_PAGESTATUS.Value = "0";
                hdnRECID.Value = "0";
                lblPVID.Text = hdnPVID.Value;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void GETHELPDATA()
        {
            try
            {
                DataSet ds = dal_DM.DM_INSTRUCTION_SP(MODULEID: Request.QueryString["MODULEID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    LiteralText.Text = ds.Tables[0].Rows[0]["DATA"].ToString();
                    divhelp.Visible = true;
                }
                else
                {
                    divhelp.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void Get_Page_Status()
        {
            try
            {
                DataSet ds = dal_DM.DM_PAGE_STATUS_SP(ACTION: "GET_PAGE_STATUS_PVID_RECID", PVID: hdnPVID.Value, RECID: hdnRECID.Value);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SET_PAGE_STATUS(ds);
                }
                else
                {
                    DataSet ds1 = dal_DM.DM_PAGE_STATUS_SP(ACTION: "GET_PAGE_STATUS_PVID", PVID: hdnPVID.Value);

                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        SET_PAGE_STATUS(ds1);
                    }
                    else
                    {
                        lblModuleStatus.Text = "Not Started";
                        lblApplicableStatus.Visible = false;
                        hdnIsComplete.Value = "0";
                        hdnFreezeStatus.Value = "0";
                        hdnLockStatus.Value = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void SET_PAGE_STATUS(DataSet ds)
        {
            hdnIsComplete.Value = ds.Tables[0].Rows[0]["IsComplete"].ToString();
            lblModuleStatus.Text = ds.Tables[0].Rows[0]["STATUS"].ToString();

            if (hdnIsComplete.Value == "2")
            {
                btnNotApplicable.Text = "Applicable";
            }
            else
            {
                btnNotApplicable.Text = "Not Applicable";
            }

            if (ds.Tables[0].Rows[0]["LOCKSTATUS"].ToString() == "True")
            {
                lblLockBy.Text = ds.Tables[0].Rows[0]["LOCKBYNAME"].ToString();
                lblLockDateTiemServer.Text = ds.Tables[0].Rows[0]["LOCK_CAL_DAT"].ToString();
                lblLockDateTimeUser.Text = ds.Tables[0].Rows[0]["LOCK_CAL_TZDAT"].ToString();

                hdnLockStatus.Value = "1";

                lbldatalock.Text = "&nbsp;||&nbsp; <img src='Images/DATALOCK.png' alt=''/>";
                lbldatalock.Visible = true;

                string DatalocktooltipText = "<div style='text-align: center; font-weight: lighter; color: white; background-color:gray; padding: 5px; border-radius: 5px;'>"
                              + "Locked Details</div>"
                              + "<table style='border-collapse: collapse; width: 100%; background-color: white; color: black;'>"
                              + "<tr><td style='color: blue; font-weight: lighter; padding: 5px;'>Locked By:</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["LOCKBYNAME"] + "</td></tr>"
                              + "<tr><td style='color: green; font-weight: lighter; padding: 5px;'>Datetime(Server):</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["LOCK_CAL_DAT"] + "</td></tr>"
                              + "<tr><td style='color: red; font-weight: lighter; padding: 5px;'>Datetime(User), (Timezone):</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["LOCK_CAL_TZDAT"] + "</td></tr>"
                              + "</table>";


                lbldatalock.Attributes["data-tooltip"] = DatalocktooltipText;
            }
            if (ds.Tables[0].Rows[0]["FREEZESTATUS"].ToString() == "True")
            {
                lblFreezeBy.Text = ds.Tables[0].Rows[0]["FREEZEBYNAME"].ToString();
                lblFreezeDatetimeServer.Text = ds.Tables[0].Rows[0]["FREEZE_CAL_DAT"].ToString();
                lblFreezeDatetimeUser.Text = ds.Tables[0].Rows[0]["FREEZE_CAL_TZDAT"].ToString();

                hdnFreezeStatus.Value = "1";

                lblFreeze.Text = "&nbsp;||&nbsp; <img src='Images/freezer.png' alt=''/>";
                lblFreeze.Visible = true;

                string FreezetooltipText = "<div style='text-align: center; font-weight: lighter; color: white; background-color:gray; padding: 5px; border-radius: 5px;'>"
                              + "Frozen Details</div>"
                              + "<table style='border-collapse: collapse; width: 100%; background-color: white; color: black;'>"
                              + "<tr><td style='color: blue; font-weight: lighter; padding: 5px;'>Frozen By:</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["FREEZEBYNAME"] + "</td></tr>"
                              + "<tr><td style='color: green; font-weight: lighter; padding: 5px;'>Datetime(Server):</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["FREEZE_CAL_DAT"] + "</td></tr>"
                              + "<tr><td style='color: red; font-weight: lighter; padding: 5px;'>Datetime(User), (Timezone):</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["FREEZE_CAL_TZDAT"] + "</td></tr>"
                              + "</table>";

                lblFreeze.Attributes["data-tooltip"] = FreezetooltipText;
            }
            if (ds.Tables[0].Rows[0]["SDVSTATUS"].ToString() != "0")
            {
                lblSDVStatus.Text = ds.Tables[0].Rows[0]["SDV_STATUSNAME"].ToString();
                lblSDVBy.Text = ds.Tables[0].Rows[0]["SDVBYNAME"].ToString();
                lblSDVDatetimeServer.Text = ds.Tables[0].Rows[0]["SDV_CAL_DAT"].ToString();
                lblSDVDatetimeUser.Text = ds.Tables[0].Rows[0]["SDV_CAL_TZDAT"].ToString();

                lblsdvcom.Text = "&nbsp;||&nbsp; <img src='Images/SDVCOMPL.png' alt=''/>";
                lblsdvcom.Visible = true;


                string SDVtooltipText = "<div style='text-align: center; font-weight: lighter; color: white; background-color:gray; padding: 5px; border-radius: 5px;'>"
                              + "SDV Details</div>"
                              + "<table style='border-collapse: collapse; width: 100%; background-color: white; color: black;'>"
                              + "<tr><td style='color: brown; font-weight: lighter; padding: 5px;'>SDV Status:</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["SDV_STATUSNAME"] + "</td></tr>"
                              + "<tr><td style='color: blue; font-weight: lighter; padding: 5px;'>SDV By:</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["SDVBYNAME"] + "</td></tr>"
                              + "<tr><td style='color: green; font-weight: lighter; padding: 5px;'>Datetime(Server):</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["SDV_CAL_DAT"] + "</td></tr>"
                              + "<tr><td style='color: red; font-weight: lighter; padding: 5px;'>Datetime(User), (Timezone):</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["SDV_CAL_TZDAT"] + "</td></tr>"
                              + "</table>";

                lblsdvcom.Attributes["data-tooltip"] = SDVtooltipText;
            }
            if (ds.Tables[0].Rows[0]["InvSignOff"].ToString() == "True")
            {
                lblInvSignOffBy.Text = ds.Tables[0].Rows[0]["InvSignOffBYNAME"].ToString();
                lblInvSignOffDatetimeServer.Text = ds.Tables[0].Rows[0]["InvSignOff_CAL_DAT"].ToString();
                lblInvSignOffDatetimeUser.Text = ds.Tables[0].Rows[0]["InvSignOff_CAL_TZDAT"].ToString();

                lblInvestigatorSign.Text = "&nbsp;||&nbsp; <img src='Images/InvestigatorSign.png' alt=''/>";
                lblInvestigatorSign.Visible = true;

                string InvestigatorSign = "<div style='text-align: center; font-weight: lighter; color: white; background-color:gray; padding: 5px; border-radius: 5px;'>"
                              + "Signed Off Details</div>"
                              + "<table style='border-collapse: collapse; width: 100%; background-color: white; color: black;'>"
                              + "<tr><td style='color: blue; font-weight: lighter; padding: 5px;'>Signed Off By:</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["InvSignOffBYNAME"] + "</td></tr>"
                              + "<tr><td style='color: green; font-weight: lighter; padding: 5px;'>Datetime(Server):</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["InvSignOff_CAL_DAT"] + "</td></tr>"
                              + "<tr><td style='color: red; font-weight: lighter; padding: 5px;'>Datetime(User), (Timezone):</td><td style='padding: 5px;'>" + ds.Tables[0].Rows[0]["InvSignOff_CAL_TZDAT"] + "</td></tr>"
                              + "</table>";

                lblInvestigatorSign.Attributes["data-tooltip"] = InvestigatorSign;
            }

            if (ds.Tables[0].Columns.Contains("PAGESTATUS"))
            {
                if (ds.Tables[0].Rows[0]["PAGESTATUS"].ToString() == "1")
                {
                    if (ds.Tables[0].Rows[0]["HasMissing"].ToString() == "True")
                    {

                        lblPageStatus.Text = "&nbsp;||&nbsp; <img src='img/REDFILE.png' alt='' style='height: 20px;'  />";
                        lblPageStatus.ToolTip = "Missing Fields";
                        lblPageStatus.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[0]["IsComplete"].ToString() == "0")
                    {

                        lblPageStatus.Text = "&nbsp;||&nbsp; <img src='img/orange file.png' alt='' style='height: 20px;'  />";
                        lblPageStatus.ToolTip = "Incomplete";
                        lblPageStatus.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[0]["IsComplete"].ToString() == "2")
                    {
                        lblPageStatus.Text = "&nbsp;||&nbsp; <img src='img/NotApplicableFile.png' alt='' style='height: 20px;' />";
                        lblPageStatus.ToolTip = "Not Applicable";
                    }
                    else
                    {

                        lblPageStatus.Text = "&nbsp;||&nbsp; <img src='img/green file.png' alt='' style='height: 20px;' />";
                        lblPageStatus.ToolTip = "Completed";
                        lblPageStatus.Visible = true;
                    }
                }
                else
                {
                    lblPageStatus.ToolTip = "Not Entered";
                }
            }
            else
            {
                if (ds.Tables[0].Rows[0]["IsMissing"].ToString() == "True")
                {

                    lblPageStatus.Text = "&nbsp;||&nbsp; <img src='img/REDFILE.png' alt='' style='height: 20px;'  />";
                    lblPageStatus.ToolTip = "Missing Fields";
                    lblPageStatus.Visible = true;
                }
                else if (ds.Tables[0].Rows[0]["IsComplete"].ToString() == "0")
                {

                    lblPageStatus.Text = "&nbsp;||&nbsp; <img src='img/orange file.png' alt='' style='height: 20px;'  />";
                    lblPageStatus.ToolTip = "Incomplete";
                    lblPageStatus.Visible = true;
                }
                else if (ds.Tables[0].Rows[0]["IsComplete"].ToString() == "2")
                {
                    lblPageStatus.Text = "&nbsp;||&nbsp; <img src='img/NotApplicableFile.png' alt='' style='height: 20px;' />";
                    lblPageStatus.ToolTip = "Not Applicable";
                }
                else
                {

                    lblPageStatus.Text = "&nbsp;||&nbsp; <img src='img/green file.png' alt='' style='height: 20px;' />";
                    lblPageStatus.ToolTip = "Completed";
                    lblPageStatus.Visible = true;
                }
            }
        }

        private void GetStructure(GridView grd)
        {
            try
            {
                lblModuleName.Text = Request.QueryString["MODULENAME"].ToString();

                DataSet ds = dal_DM.DM_STRUCTURE_SP(
                        ACTION: "GET_STRUCTURE",
                        MODULEID: Request.QueryString["MODULEID"].ToString(),
                        VISITNUM: hdnVISITID.Value
                        );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hfTablename.Value = ds.Tables[0].Rows[0]["TABLENAME"].ToString();

                    grd.DataSource = ds.Tables[0];
                    grd.DataBind();

                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        ViewState["dt_AutoCode"] = ds.Tables[1];
                    }

                    bntSaveComplete.Visible = true;
                    btnSaveIncomplete.Visible = true;
                    btnCancle.Visible = true;
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
                    bntSaveComplete.Visible = false;
                    btnSaveIncomplete.Visible = false;
                    btnCancle.Visible = false;
                    btnNotApplicable.Visible = false;
                    btnModuleStatus.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grd_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();
                    string LabData = dr["LabData"].ToString();
                    string Reference = dr["Refer"].ToString();
                    string AutoNum = dr["AutoNum"].ToString();

                    string MANDATORY = dr["MANDATORY"].ToString();

                    string DefaultData = dr["DefaultData"].ToString();

                    string ParentLinked = dr["ParentLinked"].ToString();
                    string ChildLinked = dr["ChildLinked"].ToString();
                    string ParentLinkedVARIABLENAME = dr["ParentLinkedVARIABLENAME"].ToString();
                    string PGL_TYPE = dr["PGL_TYPE"].ToString();
                    string DELETEDBY = dr["DELETEDBY"].ToString();

                    bool visibleControl = true;

                    if (PGL_TYPE != "")
                    {
                        if (DELETEDBY != "" && hdnRECID.Value == "-1")
                        {
                            e.Row.Visible = false;
                            visibleControl = false;
                        }
                        else if (DELETEDBY != "" && hdnRECID.Value != "-1")
                        {
                            e.Row.Visible = true;
                            visibleControl = true;
                        }
                        else
                        {
                            e.Row.Visible = true;
                            visibleControl = true;
                        }
                    }

                    if (visibleControl)
                    {
                        if (CONTROLTYPE == "ChildModule")
                        {
                            LinkButton LBTN_FIELD = (LinkButton)e.Row.FindControl("LBTN_FIELD");
                            LBTN_FIELD.Visible = true;
                        }

                        if (BOLDYN == "True")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.CssClass = lblField.CssClass + " fontbold";
                        }
                        if (UNLNYN == "True")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.Font.Underline = true;
                        }
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");
                            LinkButton lbtnclear = (LinkButton)e.Row.FindControl("lbtnclear");

                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN != "True")
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
                            else if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                                btnEdit.CssClass = btnEdit.CssClass.Replace("txtDate", "").Replace("txtDateNoFuture", "").Replace("txtDateMask", "");
                            }

                            if (MAXLEN != "" && MAXLEN != "0")
                            {
                                btnEdit.Attributes.Add("MaxLength", MAXLEN);
                            }
                            if (READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                                lbtnclear.Visible = false;
                            }
                            //else
                            //{
                            //    btnRightClick_Changed.Visible = true;
                            //}
                            if (MULTILINEYN == "True")
                            {
                                btnEdit.TextMode = TextBoxMode.MultiLine;
                                btnEdit.Width = 500;
                                btnEdit.Height = 100;
                            }
                            if (UPPERCASE == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                            }
                            if (AnsColor != "")
                            {
                                btnEdit.Style.Add("color", AnsColor);
                            }
                            if (AutoNum == "True")
                            {
                                btnEdit.Text = btnEdit.Text + GET_AutoNum(VARIABLENAME).ToString();
                            }
                            if (Reference == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                                if (Request["REFERENCE"] != null)
                                {
                                    btnEdit.Text = Request["REFERENCE"].ToString();
                                }
                            }

                            string Prefix = dr["Prefix"].ToString();

                            if (Prefix == "True")
                            {
                                if (btnEdit.Text == "")
                                {
                                    btnEdit.Text = dr["PrefixText"].ToString();
                                }
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                            if (CLASS == "OptionValues form-control")
                            {
                                DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                                string Values = "";
                                if (ds.Tables.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        Values += "" + ds.Tables[0].Rows[i]["TEXT"].ToString() + "¸";
                                    }
                                }

                                hfValue1.Value = Values.TrimEnd('¸');

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
                            }

                            if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                            if (btnEdit.CssClass.Contains("numericdecimal"))
                            {
                                string FORMAT = dr["FORMAT"].ToString();
                                btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                            }

                            if (Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
                        if (CONTROLTYPE == "DROPDOWN")
                        {
                            DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (ChildLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME; }
                            else
                            {
                                DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (hdnRECID.Value == "-1")
                                    {
                                        var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                        foreach (var row in rows)
                                        { row.Delete(); }
                                        ds.Tables[0].AcceptChanges();
                                    }

                                    btnEdit.DataSource = ds;
                                    btnEdit.DataTextField = "TEXT";
                                    btnEdit.DataValueField = "VALUE";
                                    btnEdit.DataBind();

                                    if (dtDropDownList.Rows.Count == 0)
                                    {
                                        dtDropDownList = ds.Tables[0];
                                    }
                                    else
                                    {
                                        dtDropDownList.Merge(ds.Tables[0]);
                                    }
                                }
                            }

                            if (AnsColor != "")
                            {
                                btnEdit.Style.Add("color", AnsColor);
                            }

                            if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                            {
                                btnEdit.Enabled = false;
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                            if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;
                        }
                        if (CONTROLTYPE == "CHECKBOX")
                        {
                            if (AnsColor == "")
                            {
                                AnsColor = "#000000";
                            }

                            Repeater repeat_CHK = (Repeater)e.Row.FindControl("repeat_CHK");
                            repeat_CHK.Visible = true;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                            System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                            newColumn.DefaultValue = AnsColor;
                            ds.Tables[0].Columns.Add(newColumn);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (hdnRECID.Value == "-1")
                                {
                                    var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                    foreach (var row in rows)
                                    { row.Delete(); }
                                    ds.Tables[0].AcceptChanges();
                                }

                                repeat_CHK.DataSource = ds;
                                repeat_CHK.DataBind();

                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                    }

                                    if (MANDATORY == "True")
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory";
                                    }

                                    if (DefaultData != "")
                                    {
                                        if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == DefaultData)
                                        {
                                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                        }
                                    }

                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
                                }
                            }
                            else
                            {
                                repeat_CHK.DataSource = null;
                                repeat_CHK.DataBind();
                            }
                        }
                        if (CONTROLTYPE == "RADIOBUTTON")
                        {
                            if (AnsColor == "")
                            {
                                AnsColor = "#000000";
                            }

                            Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                            repeat_RAD.Visible = true;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                            System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                            newColumn.DefaultValue = AnsColor;
                            ds.Tables[0].Columns.Add(newColumn);

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (hdnRECID.Value == "-1")
                                    {
                                        var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                        foreach (var row in rows)
                                        { row.Delete(); }
                                        ds.Tables[0].AcceptChanges();
                                    }

                                repeat_RAD.DataSource = ds;
                                repeat_RAD.DataBind();

                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                                    }

                                    if (MANDATORY == "True")
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory";
                                    }

                                    if (DefaultData != "")
                                    {
                                        if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == DefaultData)
                                        {
                                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                        }
                                    }

                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
                                }
                            }
                        }

                        if (FieldColor != "")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.Style.Add("color", FieldColor);
                        }

                        LinkButton AQ = (LinkButton)e.Row.FindControl("AQ");
                        AQ.CssClass += " AQ_" + VARIABLENAME + " disp-none";

                        LinkButton AWSQ = (LinkButton)e.Row.FindControl("AWSQ");
                        AWSQ.CssClass += " AWSQ_" + VARIABLENAME + " disp-none";

                        LinkButton CQ = (LinkButton)e.Row.FindControl("CQ");
                        CQ.CssClass += " CQ_" + VARIABLENAME + " disp-none";

                        LinkButton CM = (LinkButton)e.Row.FindControl("CM");
                        CM.CssClass += " CM_" + VARIABLENAME + " disp-none Comments";

                        LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                        AD.CssClass += " AD_" + VARIABLENAME + " disp-none";

                        GridView grd_Data1 = e.Row.FindControl("grd_Data1") as GridView;
                        DataSet ds1 = dal_DM.DM_STRUCTURE_SP(
                            ACTION: "GET_STRUCTURE_CHILD",
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            VISITNUM: Request.QueryString["VISITID"].ToString(),
                            FIELDID: ID
                            );

                        grd_Data1.DataSource = ds1.Tables[0];
                        grd_Data1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void grd_Data1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();

                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();
                    string LabData = dr["LabData"].ToString();
                    string Reference = dr["Refer"].ToString();
                    string AutoNum = dr["AutoNum"].ToString();

                    string MANDATORY = dr["MANDATORY"].ToString();

                    string DefaultData = dr["DefaultData"].ToString();

                    string ParentLinked = dr["ParentLinked"].ToString();
                    string ChildLinked = dr["ChildLinked"].ToString();
                    string ParentLinkedVARIABLENAME = dr["ParentLinkedVARIABLENAME"].ToString();
                    string PGL_TYPE = dr["PGL_TYPE"].ToString();
                    string DELETEDBY = dr["DELETEDBY"].ToString();

                    bool visibleControl = true;

                    if (PGL_TYPE != "")
                    {
                        if (DELETEDBY != "" && hdnRECID.Value == "-1")
                        {
                            e.Row.Visible = false;
                            visibleControl = false;
                        }
                        else if (DELETEDBY != "" && hdnRECID.Value != "-1")
                        {
                            e.Row.Visible = true;
                            visibleControl = true;
                        }
                        else
                        {
                            e.Row.Visible = true;
                            visibleControl = true;
                        }
                    }

                    if (visibleControl)
                    {
                        if (CONTROLTYPE == "ChildModule")
                        {
                            LinkButton LBTN_FIELD = (LinkButton)e.Row.FindControl("LBTN_FIELD");
                            LBTN_FIELD.Visible = true;
                        }

                        if (BOLDYN == "True")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.CssClass = lblField.CssClass + " fontbold";
                        }
                        if (UNLNYN == "True")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.Font.Underline = true;
                        }
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");
                            LinkButton lbtnclear = (LinkButton)e.Row.FindControl("lbtnclear1");
                            btnEdit.Visible = true;
                            //btnEdit.ID = VARIABLENAME;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN != "True")
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
                            else if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                                btnEdit.CssClass = btnEdit.CssClass.Replace("txtDate", "").Replace("txtDateNoFuture", "").Replace("txtDateMask", "");
                            }

                            if (MAXLEN != "" && MAXLEN != "0")
                            {
                                btnEdit.Attributes.Add("MaxLength", MAXLEN);
                            }
                            if (READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                                lbtnclear.Visible = false;
                            }
                            //else
                            //{
                            //         btnRightClick_Changed.Visible = true;
                            //}
                            if (MULTILINEYN == "True")
                            {
                                btnEdit.TextMode = TextBoxMode.MultiLine;
                                btnEdit.Width = 500;
                                btnEdit.Height = 100;
                            }
                            if (UPPERCASE == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                            }
                            if (AnsColor != "")
                            {
                                btnEdit.Style.Add("color", AnsColor);
                            }
                            if (AutoNum == "True")
                            {
                                btnEdit.Text = btnEdit.Text + GET_AutoNum(VARIABLENAME).ToString();
                            }
                            if (Reference == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                                if (Request["REFERENCE"] != null)
                                {
                                    btnEdit.Text = Request["REFERENCE"].ToString();
                                }
                            }

                            string Prefix = dr["Prefix"].ToString();

                            if (Prefix == "True")
                            {
                                if (btnEdit.Text == "")
                                {
                                    btnEdit.Text = dr["PrefixText"].ToString();
                                }
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                            if (CLASS == "OptionValues form-control")
                            {
                                DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                                string Values = "";
                                if (ds.Tables.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        Values += "" + ds.Tables[0].Rows[i]["TEXT"].ToString() + "¸";
                                    }
                                }

                                hfValue1.Value = Values.TrimEnd('¸');

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
                            }

                            if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                            if (btnEdit.CssClass.Contains("numericdecimal"))
                            {
                                string FORMAT = dr["FORMAT"].ToString();

                                btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                            }

                            if (Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
                        if (CONTROLTYPE == "DROPDOWN")
                        {
                            DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (ChildLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME; }
                            else
                            {
                                DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (hdnRECID.Value == "-1")
                                    {
                                        var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                        foreach (var row in rows)
                                        { row.Delete(); }
                                        ds.Tables[0].AcceptChanges();
                                    }

                                    btnEdit.DataSource = ds;
                                    btnEdit.DataTextField = "TEXT";
                                    btnEdit.DataValueField = "VALUE";
                                    btnEdit.DataBind();

                                    if (dtDropDownList.Rows.Count == 0)
                                    {
                                        dtDropDownList = ds.Tables[0];
                                    }
                                    else
                                    {
                                        dtDropDownList.Merge(ds.Tables[0]);
                                    }
                                }
                            }

                            if (AnsColor != "")
                            {
                                btnEdit.Style.Add("color", AnsColor);
                            }

                            if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                            {
                                btnEdit.Enabled = false;
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                            if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;
                        }
                        if (CONTROLTYPE == "CHECKBOX")
                        {
                            if (AnsColor == "")
                            {
                                AnsColor = "#000000";
                            }

                            Repeater repeat_CHK = (Repeater)e.Row.FindControl("repeat_CHK");
                            repeat_CHK.Visible = true;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                            System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                            newColumn.DefaultValue = AnsColor;
                            ds.Tables[0].Columns.Add(newColumn);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (hdnRECID.Value == "-1")
                                {
                                    var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                    foreach (var row in rows)
                                    { row.Delete(); }
                                    ds.Tables[0].AcceptChanges();
                                }
                                repeat_CHK.DataSource = ds;
                                repeat_CHK.DataBind();

                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                    }

                                    if (MANDATORY == "True")
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory";
                                    }

                                    if (DefaultData != "")
                                    {
                                        if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == DefaultData)
                                        {
                                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                        }
                                    }

                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
                                }
                            }
                        }
                        if (CONTROLTYPE == "RADIOBUTTON")
                        {
                            if (AnsColor == "")
                            {
                                AnsColor = "#000000";
                            }

                            Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                            repeat_RAD.Visible = true;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                            System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                            newColumn.DefaultValue = AnsColor;
                            ds.Tables[0].Columns.Add(newColumn);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (hdnRECID.Value == "-1")
                                {
                                    var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                    foreach (var row in rows)
                                    { row.Delete(); }
                                    ds.Tables[0].AcceptChanges();
                                }
                                repeat_RAD.DataSource = ds;
                                repeat_RAD.DataBind();

                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                                    }

                                    if (MANDATORY == "True")
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory";
                                    }

                                    if (DefaultData != "")
                                    {
                                        if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == DefaultData)
                                        {
                                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                        }
                                    }

                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
                                }
                            }
                        }

                        if (FieldColor != "")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.Style.Add("color", FieldColor);
                        }

                        LinkButton AQ = (LinkButton)e.Row.FindControl("AQ");
                        AQ.CssClass += " AQ_" + VARIABLENAME + " disp-none";

                        LinkButton AWSQ = (LinkButton)e.Row.FindControl("AWSQ");
                        AWSQ.CssClass += " AWSQ_" + VARIABLENAME + " disp-none";

                        LinkButton CQ = (LinkButton)e.Row.FindControl("CQ");
                        CQ.CssClass += " CQ_" + VARIABLENAME + " disp-none";

                        LinkButton CM = (LinkButton)e.Row.FindControl("CM");
                        CM.CssClass += " CM_" + VARIABLENAME + " disp-none Comments";

                        LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                        AD.CssClass += " AD_" + VARIABLENAME + " disp-none";

                        GridView grd_Data2 = e.Row.FindControl("grd_Data2") as GridView;
                        DataSet ds1 = dal_DM.DM_STRUCTURE_SP(
                            ACTION: "GET_STRUCTURE_CHILD",
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            VISITNUM: Request.QueryString["VISITID"].ToString(),
                            FIELDID: ID
                            );

                        grd_Data2.DataSource = ds1.Tables[0];
                        grd_Data2.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grd_Data2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();

                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();
                    string LabData = dr["LabData"].ToString();
                    string Reference = dr["Refer"].ToString();
                    string AutoNum = dr["AutoNum"].ToString();

                    string MANDATORY = dr["MANDATORY"].ToString();

                    string DefaultData = dr["DefaultData"].ToString();

                    string ParentLinked = dr["ParentLinked"].ToString();
                    string ChildLinked = dr["ChildLinked"].ToString();
                    string ParentLinkedVARIABLENAME = dr["ParentLinkedVARIABLENAME"].ToString();
                    string PGL_TYPE = dr["PGL_TYPE"].ToString();
                    string DELETEDBY = dr["DELETEDBY"].ToString();

                    bool visibleControl = true;

                    if (PGL_TYPE != "")
                    {
                        if (DELETEDBY != "" && hdnRECID.Value == "-1")
                        {
                            e.Row.Visible = false;
                            visibleControl = false;
                        }
                        else if (DELETEDBY != "" && hdnRECID.Value != "-1")
                        {
                            e.Row.Visible = true;
                            visibleControl = true;
                        }
                        else
                        {
                            e.Row.Visible = true;
                            visibleControl = true;
                        }
                    }

                    if (visibleControl)
                    {
                        if (CONTROLTYPE == "ChildModule")
                        {
                            LinkButton LBTN_FIELD = (LinkButton)e.Row.FindControl("LBTN_FIELD");
                            LBTN_FIELD.Visible = true;
                        }

                        if (BOLDYN == "True")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.CssClass = lblField.CssClass + " fontbold";
                        }
                        if (UNLNYN == "True")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.Font.Underline = true;
                        }
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");

                            LinkButton lbtnclear = (LinkButton)e.Row.FindControl("lbtnclear2");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN != "True")
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
                            else if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                                btnEdit.CssClass = btnEdit.CssClass.Replace("txtDate", "").Replace("txtDateNoFuture", "").Replace("txtDateMask", "");
                            }

                            if (MAXLEN != "" && MAXLEN != "0")
                            {
                                btnEdit.Attributes.Add("MaxLength", MAXLEN);
                            }
                            if (READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                                lbtnclear.Visible = false;
                            }
                            //else
                            //{
                            //    btnRightClick_Changed.Visible = true;
                            //}
                            if (MULTILINEYN == "True")
                            {
                                btnEdit.TextMode = TextBoxMode.MultiLine;
                                btnEdit.Width = 500;
                                btnEdit.Height = 100;
                            }
                            if (UPPERCASE == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                            }
                            if (AnsColor != "")
                            {
                                btnEdit.Style.Add("color", AnsColor);
                            }
                            if (AutoNum == "True")
                            {
                                btnEdit.Text = btnEdit.Text + GET_AutoNum(VARIABLENAME).ToString();
                            }
                            if (Reference == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                                if (Request["REFERENCE"] != null)
                                {
                                    btnEdit.Text = Request["REFERENCE"].ToString();
                                }
                            }

                            string Prefix = dr["Prefix"].ToString();

                            if (Prefix == "True")
                            {
                                if (btnEdit.Text == "")
                                {
                                    btnEdit.Text = dr["PrefixText"].ToString();
                                }
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                            if (CLASS == "OptionValues form-control")
                            {
                                DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                                string Values = "";
                                if (ds.Tables.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        Values += "" + ds.Tables[0].Rows[i]["TEXT"].ToString() + "¸";
                                    }
                                }

                                hfValue1.Value = Values.TrimEnd('¸');

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
                            }

                            if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                            if (btnEdit.CssClass.Contains("numericdecimal"))
                            {
                                string FORMAT = dr["FORMAT"].ToString();

                                btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                            }

                            if (Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
                        if (CONTROLTYPE == "DROPDOWN")
                        {
                            DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (ChildLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME; }
                            else
                            {
                                DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (hdnRECID.Value == "-1")
                                    {
                                        var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                        foreach (var row in rows)
                                        { row.Delete(); }
                                        ds.Tables[0].AcceptChanges();
                                    }

                                    btnEdit.DataSource = ds;
                                    btnEdit.DataTextField = "TEXT";
                                    btnEdit.DataValueField = "VALUE";
                                    btnEdit.DataBind();

                                    if (dtDropDownList.Rows.Count == 0)
                                    {
                                        dtDropDownList = ds.Tables[0];
                                    }
                                    else
                                    {
                                        dtDropDownList.Merge(ds.Tables[0]);
                                    }
                                }
                            }

                            if (AnsColor != "")
                            {
                                btnEdit.Style.Add("color", AnsColor);
                            }

                            if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                            {
                                btnEdit.Enabled = false;
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                            if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;
                        }
                        if (CONTROLTYPE == "CHECKBOX")
                        {
                            if (AnsColor == "")
                            {
                                AnsColor = "#000000";
                            }

                            Repeater repeat_CHK = (Repeater)e.Row.FindControl("repeat_CHK");
                            repeat_CHK.Visible = true;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                            System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                            newColumn.DefaultValue = AnsColor;
                            ds.Tables[0].Columns.Add(newColumn);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (hdnRECID.Value == "-1")
                                {
                                    var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                    foreach (var row in rows)
                                    { row.Delete(); }
                                    ds.Tables[0].AcceptChanges();
                                }
                                repeat_CHK.DataSource = ds;
                                repeat_CHK.DataBind();

                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                    }

                                    if (MANDATORY == "True")
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory";
                                    }

                                    if (DefaultData != "")
                                    {
                                        if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == DefaultData)
                                        {
                                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                        }
                                    }

                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
                                }
                            }
                        }
                        if (CONTROLTYPE == "RADIOBUTTON")
                        {
                            if (AnsColor == "")
                            {
                                AnsColor = "#000000";
                            }

                            Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                            repeat_RAD.Visible = true;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                            System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                            newColumn.DefaultValue = AnsColor;
                            ds.Tables[0].Columns.Add(newColumn);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (hdnRECID.Value == "-1")
                                {
                                    var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                    foreach (var row in rows)
                                    { row.Delete(); }
                                    ds.Tables[0].AcceptChanges();
                                }
                                repeat_RAD.DataSource = ds;
                                repeat_RAD.DataBind();

                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                                    }

                                    if (MANDATORY == "True")
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory";
                                    }

                                    if (DefaultData != "")
                                    {
                                        if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == DefaultData)
                                        {
                                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                        }
                                    }

                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
                                }
                            }
                        }

                        if (FieldColor != "")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.Style.Add("color", FieldColor);
                        }

                        LinkButton AQ = (LinkButton)e.Row.FindControl("AQ");
                        AQ.CssClass += " AQ_" + VARIABLENAME + " disp-none";

                        LinkButton AWSQ = (LinkButton)e.Row.FindControl("AWSQ");
                        AWSQ.CssClass += " AWSQ_" + VARIABLENAME + " disp-none";

                        LinkButton CQ = (LinkButton)e.Row.FindControl("CQ");
                        CQ.CssClass += " CQ_" + VARIABLENAME + " disp-none";

                        LinkButton CM = (LinkButton)e.Row.FindControl("CM");
                        CM.CssClass += " CM_" + VARIABLENAME + " disp-none Comments";

                        LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                        AD.CssClass += " AD_" + VARIABLENAME + " disp-none";

                        GridView grd_Data3 = e.Row.FindControl("grd_Data3") as GridView;
                        DataSet ds1 = dal_DM.DM_STRUCTURE_SP(
                            ACTION: "GET_STRUCTURE_CHILD",
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            VISITNUM: Request.QueryString["VISITID"].ToString(),
                            FIELDID: ID
                            );

                        grd_Data3.DataSource = ds1.Tables[0];
                        grd_Data3.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grd_Data3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();

                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();
                    string LabData = dr["LabData"].ToString();
                    string Reference = dr["Refer"].ToString();
                    string AutoNum = dr["AutoNum"].ToString();

                    string MANDATORY = dr["MANDATORY"].ToString();

                    string DefaultData = dr["DefaultData"].ToString();

                    string ParentLinked = dr["ParentLinked"].ToString();
                    string ChildLinked = dr["ChildLinked"].ToString();
                    string ParentLinkedVARIABLENAME = dr["ParentLinkedVARIABLENAME"].ToString();
                    string PGL_TYPE = dr["PGL_TYPE"].ToString();
                    string DELETEDBY = dr["DELETEDBY"].ToString();

                    bool visibleControl = true;

                    if (PGL_TYPE != "")
                    {
                        if (DELETEDBY != "" && hdnRECID.Value == "-1")
                        {
                            e.Row.Visible = false;
                            visibleControl = false;
                        }
                        else if (DELETEDBY != "" && hdnRECID.Value != "-1")
                        {
                            e.Row.Visible = true;
                            visibleControl = true;
                        }
                        else
                        {
                            e.Row.Visible = true;
                            visibleControl = true;
                        }
                    }

                    if (visibleControl)
                    {
                        if (CONTROLTYPE == "ChildModule")
                        {
                            LinkButton LBTN_FIELD = (LinkButton)e.Row.FindControl("LBTN_FIELD");
                            LBTN_FIELD.Visible = true;
                        }

                        if (BOLDYN == "True")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.CssClass = lblField.CssClass + " fontbold";
                        }
                        if (UNLNYN == "True")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.Font.Underline = true;
                        }
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");
                            LinkButton lbtnclear = (LinkButton)e.Row.FindControl("lbtnclear3");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN != "True")
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
                            else if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                                btnEdit.CssClass = btnEdit.CssClass.Replace("txtDate", "").Replace("txtDateNoFuture", "").Replace("txtDateMask", "");
                            }

                            if (MAXLEN != "" && MAXLEN != "0")
                            {
                                btnEdit.Attributes.Add("MaxLength", MAXLEN);
                            }
                            if (READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                                lbtnclear.Visible = false;
                            }
                            //else
                            //{
                            //    btnRightClick_Changed.Visible = true;
                            //}
                            if (MULTILINEYN == "True")
                            {
                                btnEdit.TextMode = TextBoxMode.MultiLine;
                                btnEdit.Width = 500;
                                btnEdit.Height = 100;
                            }
                            if (UPPERCASE == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                            }
                            if (AnsColor != "")
                            {
                                btnEdit.Style.Add("color", AnsColor);
                            }
                            if (AutoNum == "True")
                            {
                                btnEdit.Text = btnEdit.Text + GET_AutoNum(VARIABLENAME).ToString();
                            }
                            if (Reference == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                                if (Request["REFERENCE"] != null)
                                {
                                    btnEdit.Text = Request["REFERENCE"].ToString();
                                }
                            }

                            string Prefix = dr["Prefix"].ToString();

                            if (Prefix == "True")
                            {
                                if (btnEdit.Text == "")
                                {
                                    btnEdit.Text = dr["PrefixText"].ToString();
                                }
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                            if (CLASS == "OptionValues form-control")
                            {
                                DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                                string Values = "";
                                if (ds.Tables.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        Values += "" + ds.Tables[0].Rows[i]["TEXT"].ToString() + "¸";
                                    }
                                }

                                hfValue1.Value = Values.TrimEnd('¸');

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
                            }

                            if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                            if (btnEdit.CssClass.Contains("numericdecimal"))
                            {
                                string FORMAT = dr["FORMAT"].ToString();

                                btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                            }
                            if (Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
                        if (CONTROLTYPE == "DROPDOWN")
                        {
                            DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (ChildLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME; }
                            else
                            {
                                DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (hdnRECID.Value == "-1")
                                    {
                                        var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                        foreach (var row in rows)
                                        { row.Delete(); }
                                        ds.Tables[0].AcceptChanges();
                                    }

                                    btnEdit.DataSource = ds;
                                    btnEdit.DataTextField = "TEXT";
                                    btnEdit.DataValueField = "VALUE";
                                    btnEdit.DataBind();

                                    if (dtDropDownList.Rows.Count == 0)
                                    {
                                        dtDropDownList = ds.Tables[0];
                                    }
                                    else
                                    {
                                        dtDropDownList.Merge(ds.Tables[0]);
                                    }
                                }
                            }

                            if (AnsColor != "")
                            {
                                btnEdit.Style.Add("color", AnsColor);
                            }

                            if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                            {
                                btnEdit.Enabled = false;
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                            if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;
                        }
                        if (CONTROLTYPE == "CHECKBOX")
                        {
                            if (AnsColor == "")
                            {
                                AnsColor = "#000000";
                            }

                            Repeater repeat_CHK = (Repeater)e.Row.FindControl("repeat_CHK");
                            repeat_CHK.Visible = true;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                            System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                            newColumn.DefaultValue = AnsColor;
                            ds.Tables[0].Columns.Add(newColumn);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (hdnRECID.Value == "-1")
                                {
                                    var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                    foreach (var row in rows)
                                    { row.Delete(); }
                                    ds.Tables[0].AcceptChanges();
                                }
                                repeat_CHK.DataSource = ds;
                                repeat_CHK.DataBind();

                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                    }

                                    if (MANDATORY == "True")
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory";
                                    }

                                    if (DefaultData != "")
                                    {
                                        if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == DefaultData)
                                        {
                                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                        }
                                    }

                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
                                }
                            }
                        }
                        if (CONTROLTYPE == "RADIOBUTTON")
                        {
                            if (AnsColor == "")
                            {
                                AnsColor = "#000000";
                            }

                            Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                            repeat_RAD.Visible = true;

                            DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                                   Action: "GET_OPTIONS_LIST_VISIT",
                                   VARIABLENAME: VARIABLENAME,
                                   VISITNUM: hdnVISITID.Value
                                   );

                            System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                            newColumn.DefaultValue = AnsColor;
                            ds.Tables[0].Columns.Add(newColumn);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (hdnRECID.Value == "-1")
                                {
                                    var rows = ds.Tables[0].Select("DELETEDBY <> '' ");
                                    foreach (var row in rows)
                                    { row.Delete(); }
                                    ds.Tables[0].AcceptChanges();
                                }
                                repeat_RAD.DataSource = ds;
                                repeat_RAD.DataBind();

                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                                    }

                                    if (MANDATORY == "True")
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory";
                                    }

                                    if (DefaultData != "")
                                    {
                                        if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == DefaultData)
                                        {
                                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                        }
                                    }

                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
                                }
                            }
                        }

                        if (FieldColor != "")
                        {
                            Label lblField = (Label)e.Row.FindControl("lblFieldName");
                            lblField.Style.Add("color", FieldColor);
                        }

                        LinkButton AQ = (LinkButton)e.Row.FindControl("AQ");
                        AQ.CssClass += " AQ_" + VARIABLENAME + " disp-none";

                        LinkButton AWSQ = (LinkButton)e.Row.FindControl("AWSQ");
                        AWSQ.CssClass += " AWSQ_" + VARIABLENAME + " disp-none";

                        LinkButton CQ = (LinkButton)e.Row.FindControl("CQ");
                        CQ.CssClass += " CQ_" + VARIABLENAME + " disp-none";

                        LinkButton CM = (LinkButton)e.Row.FindControl("CM");
                        CM.CssClass += " CM_" + VARIABLENAME + " disp-none Comments";

                        LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                        AD.CssClass += " AD_" + VARIABLENAME + " disp-none";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private string GET_AutoNum(string VARIABLENAME)
        {
            string res = "";
            try
            {
                DataSet ds = dal_DM.DM_OPTIONS_DATA_SP(
                Action: "GET_AutoNum",
                SUBJID: Request.QueryString["SUBJID"].ToString(),
                TABLENAME: hfTablename.Value,
                MODULEID: Request.QueryString["MODULEID"].ToString(),
                VARIABLENAME: VARIABLENAME,
                VISITNUM: Request.QueryString["VISITID"].ToString()
                );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    res = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return res;
        }

        private void GetRecords(GridView grd)
        {
            try
            {
                string COLNAME, COLVAL, ENTEREDDAT = "";
                int rownum = 0;

                DataSet dsData = dal_DM.DM_MODULE_DATA_SP(
                      PVID: lblPVID.Text,
                      TABLENAME: hfTablename.Value,
                      VISITNUM: Request.QueryString["VISITID"].ToString(),
                      MODULEID: Request.QueryString["MODULEID"].ToString(),
                      RECID: hdnRECID.Value
                      );

                DataSet ds = new DataSet();
                if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                {
                    hdn_PAGESTATUS.Value = "1";
                    btnDeleteData.CssClass.Replace("disp-none", "");
                    btnCancle.Visible = false;
                    DataTable dt = GenerateTransposedTable(dsData.Tables[0]);
                    ds.Tables.Add(dt);

                    ENTEREDDAT = dsData.Tables[1].Rows[0]["ENTEREDDAT"].ToString();
                }
                else
                {
                    btnCancle.Visible = true;
                    btnDeleteData.CssClass = "disp-none";
                    hdn_PAGESTATUS.Value = "0";
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        for (rownum = 0; rownum < grd.Rows.Count; rownum++)
                        {
                            COLNAME = ((Label)grd.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                            string CONTROLTYPE;
                            CONTROLTYPE = ((Label)grd.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                            string DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                            string PREFIX;
                            PREFIX = ((Label)grd.Rows[rownum].FindControl("lblPREFIXTEXT")).Text;

                            string REQUIREDYN;
                            REQUIREDYN = ((Label)grd.Rows[rownum].FindControl("lblREQUIREDYN")).Text;

                            string CLASS;
                            CLASS = ((Label)grd.Rows[rownum].FindControl("lblCLASS")).Text;

                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();

                            bool visibleControl = true;

                            if (DataVariableName == COLNAME)
                            {
                                if (CHECK_PGL_VISIBILITY(((HiddenField)grd.Rows[rownum].FindControl("hdn_DELETEDBY")).Value, ENTEREDDAT, ((HiddenField)grd.Rows[rownum].FindControl("hdn_PGL_TYPE")).Value))
                                {
                                    grd.Rows[rownum].Visible = true;
                                    visibleControl = true;
                                }
                                else
                                {
                                    grd.Rows[rownum].Visible = false;
                                    visibleControl = false;
                                }

                                if (visibleControl)
                                {
                                    ((HiddenField)grd.Rows[rownum].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                    if (CONTROLTYPE == "TEXTBOX")
                                    {
                                        if (COLVAL != "")
                                        {
                                            ((HiddenField)grd.Rows[rownum].FindControl("HDN_FIELD")).Value = COLVAL;
                                            ((TextBox)grd.Rows[rownum].FindControl("TXT_FIELD")).Text = COLVAL;
                                        }

                                        if (REQUIREDYN == "True")
                                        {
                                            if (CLASS == "ckeditor")
                                            {
                                                if (COLVAL == "")
                                                {
                                                    HtmlControl divcontrol = (HtmlControl)grd.Rows[rownum].FindControl("divcontrol");
                                                    divcontrol.Attributes.Add("style", "background-color: yellow;");
                                                }
                                            }
                                            else
                                            {
                                                if (COLVAL == "")
                                                {
                                                    ((TextBox)grd.Rows[rownum].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                }
                                            }
                                        }
                                    }
                                    else if (CONTROLTYPE == "DROPDOWN")
                                    {
                                        for (int drp = ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).Items.Count - 1; drp >= 0; drp--)
                                        {
                                            for (int ab = 0; ab < dtDropDownList.Rows.Count; ab++)
                                            {
                                                if (((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).Items[drp].Value == dtDropDownList.Rows[ab]["TEXT"].ToString() && ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).Items[drp].Value != "--Select--")
                                                {
                                                    if (CHECK_PGL_VISIBILITY(dtDropDownList.Rows[ab]["DELETEDBY"].ToString(), ENTEREDDAT, dtDropDownList.Rows[ab]["PGL_TYPE"].ToString()))
                                                    {
                                                        ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                                        ((HiddenField)grd.Rows[rownum].FindControl("HDN_FIELD")).Value = COLVAL;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).Items.RemoveAt(drp);
                                                        break;
                                                    }
                                                }
                                            }
                                        }

                                        if (REQUIREDYN == "True")
                                        {
                                            //REQUIRED TRUE Or FALSE
                                            if (COLVAL == "")
                                            {
                                                ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                            }
                                        }
                                    }
                                    else if (CONTROLTYPE == "CHECKBOX")
                                    {
                                        string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');

                                        Repeater repeat_RAD = grd.Rows[rownum].FindControl("repeat_CHK") as Repeater;
                                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                        {
                                            if (CHECK_PGL_VISIBILITY(((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_DELETEDBY_CHECK")).Value, ENTEREDDAT, ((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_PGL_TYPE_CHECK")).Value))
                                            {
                                                foreach (string x in stringArray)
                                                {
                                                    if (x != "")
                                                    {
                                                        if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                        {
                                                            ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Visible = true;
                                                            ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Visible = false;
                                                ((HtmlGenericControl)repeat_RAD.Items[ab].FindControl("divWrapper1")).Visible = false;
                                            }
                                        }

                                        if (REQUIREDYN == "True")
                                        {
                                            //REQUIRED TRUE Or FALSE
                                            if (ds.Tables[0].Rows[i]["DATA"].ToString() == "")
                                            {
                                                HtmlControl divcontrol = (HtmlControl)grd.Rows[rownum].FindControl("divcontrol");
                                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                                            }
                                        }
                                    }
                                    else if (CONTROLTYPE == "RADIOBUTTON")
                                    {
                                        string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');

                                        Repeater repeat_RAD = grd.Rows[rownum].FindControl("repeat_RAD") as Repeater;
                                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                        {
                                            if (CHECK_PGL_VISIBILITY(((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_DELETEDBY_RADIO")).Value, ENTEREDDAT, ((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_PGL_TYPE_RADIO")).Value))
                                            {
                                                foreach (string x in stringArray)
                                                {
                                                    if (x != "")
                                                    {
                                                        if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                        {
                                                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                                        }
                                                        else
                                                        {
                                                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = false;
                                                        }

                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Visible = false;
                                                ((HtmlGenericControl)repeat_RAD.Items[ab].FindControl("divWrapper")).Visible = false;
                                            }
                                        }

                                        if (REQUIREDYN == "True")
                                        {
                                            //REQUIRED TRUE Or FALSE
                                            if (ds.Tables[0].Rows[i]["DATA"].ToString() == "")
                                            {
                                                HtmlControl divcontrol = (HtmlControl)grd.Rows[rownum].FindControl("divcontrol");
                                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                                            }
                                        }
                                    }
                                }
                            }

                            if (visibleControl)
                            {
                                GridView grd_Data1 = grd.Rows[rownum].FindControl("grd_Data1") as GridView;

                                for (int a = 0; a < grd_Data1.Rows.Count; a++)
                                {
                                    COLNAME = ((Label)grd_Data1.Rows[a].FindControl("lblVARIABLENAME")).Text;
                                    CONTROLTYPE = ((Label)grd_Data1.Rows[a].FindControl("lblCONTROLTYPE")).Text;
                                    DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                                    REQUIREDYN = ((Label)grd_Data1.Rows[a].FindControl("lblREQUIREDYN")).Text;
                                    CLASS = ((Label)grd_Data1.Rows[a].FindControl("lblCLASS")).Text;

                                    if (DataVariableName == COLNAME)
                                    {
                                        if (CHECK_PGL_VISIBILITY(((HiddenField)grd_Data1.Rows[a].FindControl("hdn_DELETEDBY")).Value, ENTEREDDAT, ((HiddenField)grd_Data1.Rows[a].FindControl("hdn_PGL_TYPE")).Value))
                                        {
                                            grd_Data1.Rows[a].Visible = true;
                                            visibleControl = true;
                                        }
                                        else
                                        {
                                            grd_Data1.Rows[a].Visible = false;
                                            visibleControl = false;
                                        }

                                        if (visibleControl)
                                        {
                                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                            ((HiddenField)grd_Data1.Rows[a].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                            if (CONTROLTYPE == "TEXTBOX")
                                            {
                                                if (COLVAL != "")
                                                {
                                                    ((HiddenField)grd_Data1.Rows[a].FindControl("HDN_FIELD")).Value = COLVAL;
                                                    ((TextBox)grd_Data1.Rows[a].FindControl("TXT_FIELD")).Text = COLVAL;
                                                }

                                                if (REQUIREDYN == "True")
                                                {
                                                    if (CLASS == "ckeditor")
                                                    {
                                                        if (COLVAL == "")
                                                        {
                                                            HtmlControl divcontrol = (HtmlControl)grd_Data1.Rows[a].FindControl("divcontrol");
                                                            divcontrol.Attributes.Add("style", "background-color: yellow;");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (COLVAL == "")
                                                        {
                                                            ((TextBox)grd_Data1.Rows[a].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (CONTROLTYPE == "DROPDOWN")
                                            {
                                                for (int drp = ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).Items.Count - 1; drp >= 0; drp--)
                                                {
                                                    for (int ab = 0; ab < dtDropDownList.Rows.Count; ab++)
                                                    {
                                                        if (((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).Items[drp].Value == dtDropDownList.Rows[ab]["TEXT"].ToString() && ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).Items[drp].Value != "--Select--")
                                                        {
                                                            if (CHECK_PGL_VISIBILITY(dtDropDownList.Rows[ab]["DELETEDBY"].ToString(), ENTEREDDAT, dtDropDownList.Rows[ab]["PGL_TYPE"].ToString()))
                                                            {
                                                                ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                                                ((HiddenField)grd_Data1.Rows[a].FindControl("HDN_FIELD")).Value = COLVAL;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).Items.RemoveAt(drp);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }

                                                if (REQUIREDYN == "True")
                                                {
                                                    //REQUIRED TRUE Or FALSE
                                                    if (COLVAL == "")
                                                    {
                                                        ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                    }
                                                }
                                            }
                                            else if (CONTROLTYPE == "CHECKBOX")
                                            {
                                                string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');

                                                Repeater repeat_RAD = grd_Data1.Rows[a].FindControl("repeat_CHK") as Repeater;
                                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                                {
                                                    if (CHECK_PGL_VISIBILITY(((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_DELETEDBY_CHECK")).Value, ENTEREDDAT, ((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_PGL_TYPE_CHECK")).Value))
                                                    {
                                                        foreach (string x in stringArray)
                                                        {
                                                            if (x != "")
                                                            {
                                                                if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                                {
                                                                    ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Visible = true;
                                                                    ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = false;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Visible = false;
                                                        ((HtmlGenericControl)repeat_RAD.Items[ab].FindControl("divWrapper1")).Visible = false;
                                                    }
                                                }

                                                if (REQUIREDYN == "True")
                                                {
                                                    //REQUIRED TRUE Or FALSE
                                                    if (ds.Tables[0].Rows[i]["DATA"].ToString() == "")
                                                    {
                                                        HtmlControl divcontrol = (HtmlControl)grd_Data1.Rows[a].FindControl("divcontrol");
                                                        divcontrol.Attributes.Add("style", "background-color: yellow;");
                                                    }
                                                }
                                            }
                                            else if (CONTROLTYPE == "RADIOBUTTON")
                                            {
                                                string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');

                                                Repeater repeat_RAD = grd_Data1.Rows[a].FindControl("repeat_RAD") as Repeater;
                                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                                {
                                                    if (CHECK_PGL_VISIBILITY(((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_DELETEDBY_RADIO")).Value, ENTEREDDAT, ((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_PGL_TYPE_RADIO")).Value))
                                                    {
                                                        foreach (string x in stringArray)
                                                        {
                                                            if (x != "")
                                                            {
                                                                if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                                {
                                                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                                                }
                                                                else
                                                                {
                                                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = false;
                                                                }

                                                                break;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Visible = false;
                                                        ((HtmlGenericControl)repeat_RAD.Items[ab].FindControl("divWrapper")).Visible = false;
                                                    }
                                                }

                                                if (REQUIREDYN == "True")
                                                {
                                                    //REQUIRED TRUE Or FALSE
                                                    if (ds.Tables[0].Rows[i]["DATA"].ToString() == "")
                                                    {
                                                        HtmlControl divcontrol = (HtmlControl)grd_Data1.Rows[a].FindControl("divcontrol");
                                                        divcontrol.Attributes.Add("style", "background-color: yellow;");
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (visibleControl)
                                    {
                                        GridView grd_Data2 = grd_Data1.Rows[a].FindControl("grd_Data2") as GridView;

                                        for (int b = 0; b < grd_Data2.Rows.Count; b++)
                                        {
                                            COLNAME = ((Label)grd_Data2.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                            CONTROLTYPE = ((Label)grd_Data2.Rows[b].FindControl("lblCONTROLTYPE")).Text;
                                            DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                                            REQUIREDYN = ((Label)grd_Data2.Rows[b].FindControl("lblREQUIREDYN")).Text;
                                            CLASS = ((Label)grd_Data2.Rows[b].FindControl("lblCLASS")).Text;

                                            if (DataVariableName == COLNAME)
                                            {
                                                if (CHECK_PGL_VISIBILITY(((HiddenField)grd_Data2.Rows[b].FindControl("hdn_DELETEDBY")).Value, ENTEREDDAT, ((HiddenField)grd_Data2.Rows[b].FindControl("hdn_PGL_TYPE")).Value))
                                                {
                                                    grd_Data2.Rows[b].Visible = true;
                                                    visibleControl = true;
                                                }
                                                else
                                                {
                                                    grd_Data2.Rows[b].Visible = false;
                                                    visibleControl = false;
                                                }

                                                if (visibleControl)
                                                {
                                                    COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                                    ((HiddenField)grd_Data2.Rows[b].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                                    if (CONTROLTYPE == "TEXTBOX")
                                                    {
                                                        if (COLVAL != "")
                                                        {
                                                            ((HiddenField)grd_Data2.Rows[b].FindControl("HDN_FIELD")).Value = COLVAL;
                                                            ((TextBox)grd_Data2.Rows[b].FindControl("TXT_FIELD")).Text = COLVAL;
                                                        }

                                                        if (REQUIREDYN == "True")
                                                        {
                                                            if (CLASS == "ckeditor")
                                                            {
                                                                if (COLVAL == "")
                                                                {
                                                                    HtmlControl divcontrol = (HtmlControl)grd_Data2.Rows[b].FindControl("divcontrol");
                                                                    divcontrol.Attributes.Add("style", "background-color: yellow;");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (COLVAL == "")
                                                                {
                                                                    ((TextBox)grd_Data2.Rows[b].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else if (CONTROLTYPE == "DROPDOWN")
                                                    {
                                                        for (int drp = ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).Items.Count - 1; drp >= 0; drp--)
                                                        {
                                                            for (int ab = 0; ab < dtDropDownList.Rows.Count; ab++)
                                                            {
                                                                if (((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).Items[drp].Value == dtDropDownList.Rows[ab]["TEXT"].ToString() && ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).Items[drp].Value != "--Select--")
                                                                {
                                                                    if (CHECK_PGL_VISIBILITY(dtDropDownList.Rows[ab]["DELETEDBY"].ToString(), ENTEREDDAT, dtDropDownList.Rows[ab]["PGL_TYPE"].ToString()))
                                                                    {
                                                                        ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                                                        break;
                                                                    }
                                                                    else
                                                                    {
                                                                        ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).Items.RemoveAt(drp);
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        if (REQUIREDYN == "True")
                                                        {
                                                            //REQUIRED TRUE Or FALSE
                                                            if (COLVAL == "")
                                                            {
                                                                ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                            }
                                                        }
                                                    }
                                                    else if (CONTROLTYPE == "CHECKBOX")
                                                    {
                                                        string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');

                                                        Repeater repeat_RAD = grd_Data2.Rows[b].FindControl("repeat_CHK") as Repeater;
                                                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                                        {
                                                            if (CHECK_PGL_VISIBILITY(((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_DELETEDBY_CHECK")).Value, ENTEREDDAT, ((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_PGL_TYPE_CHECK")).Value))
                                                            {
                                                                foreach (string x in stringArray)
                                                                {
                                                                    if (x != "")
                                                                    {
                                                                        if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                                        {
                                                                            ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Visible = true;
                                                                            ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = false;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Visible = false;
                                                                ((HtmlGenericControl)repeat_RAD.Items[ab].FindControl("divWrapper1")).Visible = false;
                                                            }
                                                        }

                                                        if (REQUIREDYN == "True")
                                                        {
                                                            //REQUIRED TRUE Or FALSE
                                                            if (ds.Tables[0].Rows[i]["DATA"].ToString() == "")
                                                            {
                                                                HtmlControl divcontrol = (HtmlControl)grd_Data2.Rows[b].FindControl("divcontrol");
                                                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                                                            }
                                                        }
                                                    }
                                                    else if (CONTROLTYPE == "RADIOBUTTON")
                                                    {
                                                        string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');

                                                        Repeater repeat_RAD = grd_Data2.Rows[b].FindControl("repeat_RAD") as Repeater;

                                                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                                        {
                                                            if (CHECK_PGL_VISIBILITY(((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_DELETEDBY_RADIO")).Value, ENTEREDDAT, ((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_PGL_TYPE_RADIO")).Value))
                                                            {
                                                                foreach (string x in stringArray)
                                                                {
                                                                    if (x != "")
                                                                    {
                                                                        if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                                        {
                                                                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = false;
                                                                        }

                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Visible = false;
                                                                ((HtmlGenericControl)repeat_RAD.Items[ab].FindControl("divWrapper")).Visible = false;
                                                            }
                                                        }

                                                        if (REQUIREDYN == "True")
                                                        {
                                                            //REQUIRED TRUE Or FALSE
                                                            if (ds.Tables[0].Rows[i]["DATA"].ToString() == "")
                                                            {
                                                                HtmlControl divcontrol = (HtmlControl)grd_Data2.Rows[b].FindControl("divcontrol");
                                                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            if (visibleControl)
                                            {
                                                GridView grd_Data3 = grd_Data2.Rows[b].FindControl("grd_Data3") as GridView;

                                                for (int c = 0; c < grd_Data3.Rows.Count; c++)
                                                {
                                                    COLNAME = ((Label)grd_Data3.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                                    CONTROLTYPE = ((Label)grd_Data3.Rows[c].FindControl("lblCONTROLTYPE")).Text;
                                                    DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                                                    REQUIREDYN = ((Label)grd_Data3.Rows[c].FindControl("lblREQUIREDYN")).Text;
                                                    CLASS = ((Label)grd_Data3.Rows[c].FindControl("lblCLASS")).Text;

                                                    if (DataVariableName == COLNAME)
                                                    {
                                                        if (CHECK_PGL_VISIBILITY(((HiddenField)grd_Data3.Rows[c].FindControl("hdn_DELETEDBY")).Value, ENTEREDDAT, ((HiddenField)grd_Data3.Rows[c].FindControl("hdn_PGL_TYPE")).Value))
                                                        {
                                                            grd_Data2.Rows[b].Visible = true;
                                                            visibleControl = true;
                                                        }
                                                        else
                                                        {
                                                            grd_Data2.Rows[b].Visible = false;
                                                            visibleControl = false;
                                                        }

                                                        if (visibleControl)
                                                        {
                                                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                                            ((HiddenField)grd_Data3.Rows[c].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                                            if (CONTROLTYPE == "TEXTBOX")
                                                            {
                                                                if (COLVAL != "")
                                                                {
                                                                    ((HiddenField)grd_Data3.Rows[c].FindControl("HDN_FIELD")).Value = COLVAL;
                                                                    ((TextBox)grd_Data3.Rows[c].FindControl("TXT_FIELD")).Text = COLVAL;
                                                                }

                                                                if (REQUIREDYN == "True")
                                                                {
                                                                    if (CLASS == "ckeditor")
                                                                    {
                                                                        if (COLVAL == "")
                                                                        {
                                                                            HtmlControl divcontrol = (HtmlControl)grd_Data3.Rows[c].FindControl("divcontrol");
                                                                            divcontrol.Attributes.Add("style", "background-color: yellow;");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (COLVAL == "")
                                                                        {
                                                                            ((TextBox)grd_Data3.Rows[c].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else if (CONTROLTYPE == "DROPDOWN")
                                                            {
                                                                for (int drp = ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).Items.Count - 1; drp >= 0; drp--)
                                                                {
                                                                    for (int ab = 0; ab < dtDropDownList.Rows.Count; ab++)
                                                                    {
                                                                        if (((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).Items[drp].Value == dtDropDownList.Rows[ab]["TEXT"].ToString() && ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).Items[drp].Value != "--Select--")
                                                                        {
                                                                            if (CHECK_PGL_VISIBILITY(dtDropDownList.Rows[ab]["DELETEDBY"].ToString(), ENTEREDDAT, dtDropDownList.Rows[ab]["PGL_TYPE"].ToString()))
                                                                            {
                                                                                ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                                                                break;
                                                                            }
                                                                            else
                                                                            {
                                                                                ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).Items.RemoveAt(drp);
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                if (REQUIREDYN == "True")
                                                                {
                                                                    //REQUIRED TRUE Or FALSE
                                                                    if (COLVAL == "")
                                                                    {
                                                                        ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                                    }
                                                                }
                                                            }
                                                            else if (CONTROLTYPE == "CHECKBOX")
                                                            {
                                                                string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');

                                                                Repeater repeat_RAD = grd_Data3.Rows[c].FindControl("repeat_CHK") as Repeater;
                                                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                                                {
                                                                    if (CHECK_PGL_VISIBILITY(((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_DELETEDBY_CHECK")).Value, ENTEREDDAT, ((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_PGL_TYPE_CHECK")).Value))
                                                                    {
                                                                        foreach (string x in stringArray)
                                                                        {
                                                                            if (x != "")
                                                                            {
                                                                                if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                                                {
                                                                                    ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Visible = true;
                                                                                    ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                                                    break;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = false;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Visible = false;
                                                                        ((HtmlGenericControl)repeat_RAD.Items[ab].FindControl("divWrapper1")).Visible = false;
                                                                    }
                                                                }

                                                                if (REQUIREDYN == "True")
                                                                {
                                                                    //REQUIRED TRUE Or FALSE
                                                                    if (ds.Tables[0].Rows[i]["DATA"].ToString() == "")
                                                                    {
                                                                        HtmlControl divcontrol = (HtmlControl)grd_Data3.Rows[c].FindControl("divcontrol");
                                                                        divcontrol.Attributes.Add("style", "background-color: yellow;");
                                                                    }
                                                                }
                                                            }
                                                            else if (CONTROLTYPE == "RADIOBUTTON")
                                                            {
                                                                string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');
                                                                Repeater repeat_RAD = grd_Data3.Rows[c].FindControl("repeat_RAD") as Repeater;

                                                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                                                {
                                                                    if (CHECK_PGL_VISIBILITY(((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_DELETEDBY_RADIO")).Value, ENTEREDDAT, ((HiddenField)repeat_RAD.Items[ab].FindControl("hdn_PGL_TYPE_RADIO")).Value))
                                                                    {
                                                                        foreach (string x in stringArray)
                                                                        {
                                                                            if (x != "")
                                                                            {
                                                                                if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                                                {
                                                                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                                                                }
                                                                                else
                                                                                {
                                                                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = false;
                                                                                }

                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Visible = false;
                                                                        ((HtmlGenericControl)repeat_RAD.Items[ab].FindControl("divWrapper")).Visible = false;
                                                                    }
                                                                }

                                                                if (REQUIREDYN == "True")
                                                                {
                                                                    //REQUIRED TRUE Or FALSE
                                                                    if (ds.Tables[0].Rows[i]["DATA"].ToString() == "")
                                                                    {
                                                                        HtmlControl divcontrol = (HtmlControl)grd_Data3.Rows[c].FindControl("divcontrol");
                                                                        divcontrol.Attributes.Add("style", "background-color: yellow;");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Change", "callChange();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected bool CHECK_PGL_VISIBILITY(string DELETEDBY, string ENTEREDDAT, string PGL_TYPE)
        {
            bool option_visibility = true;

            if (DELETEDBY != "" && Convert.ToDateTime(ENTEREDDAT) <= Convert.ToDateTime(Session["DB_STATUS_LOGS_LAST_DAT"]))
            {
                option_visibility = true;
            }
            else if (DELETEDBY != "" && Convert.ToDateTime(ENTEREDDAT) >= Convert.ToDateTime(Session["DB_STATUS_LOGS_LAST_DAT"]))
            {
                option_visibility = false;
            }
            else if (DELETEDBY == "" && PGL_TYPE == "Prospective" && Convert.ToDateTime(ENTEREDDAT) >= Convert.ToDateTime(Session["DB_STATUS_LOGS_LAST_DAT"]))
            {
                option_visibility = true;
            }
            else if (DELETEDBY == "" && PGL_TYPE == "Prospective" && Convert.ToDateTime(ENTEREDDAT) <= Convert.ToDateTime(Session["DB_STATUS_LOGS_LAST_DAT"]))
            {
                option_visibility = false;
            }
            else
            {
                option_visibility = true;
            }

            return option_visibility;
        }

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("VARIABLENAME");
            outputTable.Columns.Add("DATA");

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                foreach (DataColumn dc in inputTable.Columns)
                {
                    DataRow drNew = outputTable.NewRow();
                    drNew["VARIABLENAME"] = dc.ColumnName.ToString();
                    drNew["DATA"] = inputTable.Rows[i][dc.ColumnName];
                    outputTable.Rows.Add(drNew);
                }
            }

            return outputTable;
        }

        protected void bntSaveComplete_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHECK_OnSubmit_CRITs())
                {
                    INSERT_FORM_DATA();

                    UPDATE_SAE_SYNC();

                    Run_Rules();

                    if (hdn_PAGESTATUS.Value == "0")
                    {
                        AutoCode();
                    }

                    CHECK_OnSubmit_EMAIL_CRITs();

                    Session.Remove("RedirceToAnotherPage");

                    if (Request.QueryString["OPENLINK"] == "1")
                    {
                        Response.Write("<script> alert('Record saved as completed successfully.'); window.close(); </script>");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record saved as completed successfully.');window.location.href = 'DM_DataEntry.aspx?MODULEID=" + Request.QueryString["MODULEID"].ToString() + "&MODULENAME=" + Request.QueryString["MODULENAME"].ToString() + "&VISITID=" + Request.QueryString["VISITID"].ToString() + "&VISIT=" + Request.QueryString["VISIT"].ToString() + "&INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "';", true);
                    }
                }
                else
                {
                    if (hdnAllowable.Value == "True")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmMsg();", true);
                    }
                    else
                    {
                        // Convert the value to Base64
                        string base64Msg = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(hdnError_Msg.Value));

                        // Display the Base64-encoded message safely in JavaScript
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                            $"var encodedMsg = '{base64Msg}'; alert(atob(encodedMsg));", true);

                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + hdnError_Msg.Value + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        private void INSERT_FORM_DATA()
        {
            try
            {
                bool isColAdded = false, HasMissing = false;

                string COLUMN = "", varname = "", DATA = "", FieldName = "", Strata = "", INSERTQUERY = "", UPDATEQUERY = "", COLUMN_Audit = "", RECID = "";

                DataSet ds = dal_DM.DM_RECID_SP(
                    PVID: lblPVID.Text,
                    TABLENAME: hfTablename.Value,
                    SUBJID: Request.QueryString["SUBJID"].ToString()
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    RECID = (Convert.ToInt32(ds.Tables[0].Rows[0]["RECID"]) + 1).ToString();
                }
                else
                {
                    RECID = "0";
                }

                if (hdnRECID.Value != "-1")
                {
                    RECID = hdnRECID.Value;
                }

                hdnRECID.Value = RECID;

                for (int rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
                {
                    string strdata = "";
                    varname = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                    Strata = ((Label)grd_Data.Rows[rownum].FindControl("lblSTRATA")).Text;
                    string CONTROLTYPE;
                    CONTROLTYPE = ((Label)grd_Data.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                    string REQUIREDYN;
                    REQUIREDYN = ((Label)grd_Data.Rows[rownum].FindControl("lblREQUIREDYN")).Text;
                    string READYN = ((Label)grd_Data.Rows[rownum].FindControl("READYN")).Text;

                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                    {
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            strdata = ((TextBox)grd_Data.Rows[rownum].FindControl("TXT_FIELD")).Text;
                        }
                        else if (CONTROLTYPE == "DROPDOWN")
                        {
                            if (((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedIndex == -1)
                            {
                                strdata = ((HiddenField)grd_Data.Rows[rownum].FindControl("HDN_FIELD")).Value;
                            }
                            else if (((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                            {
                                strdata = ((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedValue;
                            }
                            else
                            {
                                strdata = "";
                            }
                        }
                        else if (CONTROLTYPE == "CHECKBOX")
                        {
                            Repeater repeat_CHK = grd_Data.Rows[rownum].FindControl("repeat_CHK") as Repeater;
                            for (int a = 0; a < repeat_CHK.Items.Count; a++)
                            {
                                if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                {
                                    if (strdata.ToString() == "")
                                    {
                                        strdata = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                    }
                                    else
                                    {
                                        strdata += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                    }
                                }
                            }
                        }
                        else if (CONTROLTYPE == "RADIOBUTTON")
                        {
                            Repeater repeat_RAD = grd_Data.Rows[rownum].FindControl("repeat_RAD") as Repeater;
                            for (int a = 0; a < repeat_RAD.Items.Count; a++)
                            {
                                if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                {
                                    strdata = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                }
                            }
                        }

                        if (FieldName != "")
                        {
                            FieldName = FieldName + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblFieldName1")).Text + "";
                        }
                        else
                        {
                            FieldName = ((Label)grd_Data.Rows[rownum].FindControl("lblFieldName1")).Text;
                        }

                        if (READYN != "True")
                        {
                            if (COLUMN_Audit != "")
                            {
                                COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                            }
                            else
                            {
                                COLUMN_Audit = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                            }
                        }

                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                        }
                        else
                        {
                            COLUMN = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                        }

                        if (strdata != "")
                        {
                            strdata = strdata.Replace("'", "''");
                        }

                        if (REQUIREDYN == "True" && strdata.Trim() == "")
                        {
                            HasMissing = true;
                        }

                        if (DATA != "")
                        {
                            if (strdata != "")
                            {
                                DATA = DATA + " @ni$h N'" + strdata + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h ''";
                            }
                        }
                        else
                        {
                            if (strdata != "")
                            {
                                DATA = "N'" + strdata + "'";
                            }
                            else
                            {
                                DATA = "''";
                            }
                        }
                    }

                    if (strdata != "" && Strata != "0")
                    {
                        dal_DM.DM_IWRS_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata, SUBJID: Request.QueryString["SUBJID"].ToString());
                    }

                    GridView grd_Data1 = grd_Data.Rows[rownum].FindControl("grd_Data1") as GridView;

                    for (int b = 0; b < grd_Data1.Rows.Count; b++)
                    {
                        string Val_Child;
                        string strdata1 = "";
                        Val_Child = ((Label)grd_Data1.Rows[b].FindControl("lblVal_Child")).Text;

                        varname = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                        Strata = ((Label)grd_Data1.Rows[b].FindControl("lblSTRATA")).Text;
                        CONTROLTYPE = ((Label)grd_Data1.Rows[b].FindControl("lblCONTROLTYPE")).Text;
                        REQUIREDYN = ((Label)grd_Data1.Rows[b].FindControl("lblREQUIREDYN")).Text;
                        READYN = ((Label)grd_Data1.Rows[b].FindControl("READYN")).Text;

                        if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                        {
                            if (CONTROLTYPE == "TEXTBOX")
                            {
                                strdata1 = ((TextBox)grd_Data1.Rows[b].FindControl("TXT_FIELD")).Text;
                            }
                            else if (CONTROLTYPE == "DROPDOWN")
                            {
                                if (((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedIndex == -1)
                                {
                                    strdata1 = ((HiddenField)grd_Data1.Rows[b].FindControl("HDN_FIELD")).Value;
                                }
                                else if (((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                {
                                    strdata1 = ((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedValue;
                                }
                                else
                                {
                                    strdata1 = "";
                                }
                            }
                            else if (CONTROLTYPE == "CHECKBOX")
                            {
                                Repeater repeat_CHK = grd_Data1.Rows[b].FindControl("repeat_CHK") as Repeater;
                                for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                {
                                    if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                    {
                                        if (strdata1.ToString() == "")
                                        {
                                            strdata1 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                        }
                                        else
                                        {
                                            strdata1 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                        }
                                    }
                                }
                            }
                            else if (CONTROLTYPE == "RADIOBUTTON")
                            {
                                Repeater repeat_RAD = grd_Data1.Rows[b].FindControl("repeat_RAD") as Repeater;
                                for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                {
                                    if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                    {
                                        strdata1 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                    }
                                }
                            }

                            if (FieldName != "")
                            {
                                FieldName = FieldName + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblFieldName1")).Text + "";
                            }
                            else
                            {
                                FieldName = ((Label)grd_Data1.Rows[b].FindControl("lblFieldName1")).Text;
                            }

                            if (READYN != "True")
                            {
                                if (COLUMN_Audit != "")
                                {
                                    COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                }
                                else
                                {
                                    COLUMN_Audit = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                }
                            }

                            if (strdata1 != "")
                            {
                                strdata1 = strdata1.Replace("'", "''");
                            }

                            isColAdded = false;

                            foreach (string val in strdata.Split('¸'))
                            {
                                if (checkStringContains(Val_Child, val) || (Val_Child == "Is Not Blank" && strdata != "") || Val_Child == "Compare")
                                {
                                    isColAdded = true;

                                    if (REQUIREDYN == "True" && strdata1.Trim() == "")
                                    {
                                        HasMissing = true;
                                    }

                                    if (COLUMN != "")
                                    {
                                        COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                    }
                                    else
                                    {
                                        COLUMN = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                    }

                                    if (DATA != "")
                                    {
                                        if (strdata1 != "")
                                        {
                                            DATA = DATA + " @ni$h N'" + strdata1 + "'";
                                        }
                                        else
                                        {
                                            DATA = DATA + " @ni$h NULL";
                                        }
                                    }
                                    else
                                    {
                                        if (strdata1 != "")
                                        {
                                            DATA = "N'" + strdata1 + "'";
                                        }
                                        else
                                        {
                                            DATA = "NULL";
                                        }
                                    }
                                }
                            }


                            if (!isColAdded)
                            {
                                if (((Label)grd_Data1.Rows[b].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data1.Rows[b].FindControl("AutoNum")).Text == "False")
                                {
                                    if (strdata1 != "")
                                    {
                                        ADD_NEW_ROW_DATA(((Label)grd_Data1.Rows[b].FindControl("lblFieldName1")).Text,
                                            ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text,
                                            strdata1.Replace("N'", "").Replace("'", ""),
                                            DBNull.Value.ToString(),
                                            "Parent Field Updated",
                                            DBNull.Value.ToString(),
                                            RECID
                                            );
                                    }
                                }

                                if (COLUMN != "")
                                {
                                    COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                }
                                else
                                {
                                    COLUMN = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                }

                                if (DATA != "")
                                {
                                    DATA = DATA + " @ni$h NULL";
                                }
                                else
                                {
                                    DATA = "NULL";
                                }
                            }

                            if (strdata1 != "" && Strata != "0")
                            {
                                dal_DM.DM_IWRS_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata1, SUBJID: Request.QueryString["SUBJID"].ToString());
                            }
                        }

                        GridView grd_Data2 = grd_Data1.Rows[b].FindControl("grd_Data2") as GridView;

                        for (int c = 0; c < grd_Data2.Rows.Count; c++)
                        {
                            string Val_Child1 = ((Label)grd_Data2.Rows[c].FindControl("lblVal_Child")).Text;
                            string strdata2 = "";
                            varname = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                            Strata = ((Label)grd_Data2.Rows[c].FindControl("lblSTRATA")).Text;
                            CONTROLTYPE = ((Label)grd_Data2.Rows[c].FindControl("lblCONTROLTYPE")).Text;
                            REQUIREDYN = ((Label)grd_Data2.Rows[c].FindControl("lblREQUIREDYN")).Text;
                            READYN = ((Label)grd_Data2.Rows[c].FindControl("READYN")).Text;

                            if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                            {
                                if (CONTROLTYPE == "TEXTBOX")
                                {
                                    strdata2 = ((TextBox)grd_Data2.Rows[c].FindControl("TXT_FIELD")).Text;
                                }
                                else if (CONTROLTYPE == "DROPDOWN")
                                {
                                    if (((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedIndex == -1)
                                    {
                                        strdata2 = ((HiddenField)grd_Data2.Rows[c].FindControl("HDN_FIELD")).Value;
                                    }
                                    else if (((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                    {
                                        strdata2 = ((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedValue;
                                    }
                                    else
                                    {
                                        strdata2 = "";
                                    }
                                }
                                else if (CONTROLTYPE == "CHECKBOX")
                                {
                                    Repeater repeat_CHK = grd_Data2.Rows[c].FindControl("repeat_CHK") as Repeater;
                                    for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                    {
                                        if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                        {
                                            if (strdata2.ToString() == "")
                                            {
                                                strdata2 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                            }
                                            else
                                            {
                                                strdata2 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                            }
                                        }
                                    }
                                }
                                else if (CONTROLTYPE == "RADIOBUTTON")
                                {
                                    Repeater repeat_RAD = grd_Data2.Rows[c].FindControl("repeat_RAD") as Repeater;
                                    for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                    {
                                        if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                        {
                                            strdata2 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                        }
                                    }
                                }

                                if (FieldName != "")
                                {
                                    FieldName = FieldName + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblFieldName1")).Text + "";
                                }
                                else
                                {
                                    FieldName = ((Label)grd_Data2.Rows[c].FindControl("lblFieldName1")).Text;
                                }

                                if (READYN != "True")
                                {
                                    if (COLUMN_Audit != "")
                                    {
                                        COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                    }
                                    else
                                    {
                                        COLUMN_Audit = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                    }
                                }

                                if (strdata2 != "")
                                {
                                    strdata2 = strdata2.Replace("'", "''");
                                }

                                isColAdded = false;

                                foreach (string val in strdata1.Split('¸'))
                                {
                                    if (checkStringContains(Val_Child1, val) || (Val_Child1 == "Is Not Blank" && strdata1 != "") || Val_Child1 == "Compare")
                                    {
                                        isColAdded = true;

                                        if (REQUIREDYN == "True" && strdata2.Trim() == "")
                                        {
                                            HasMissing = true;
                                        }

                                        if (COLUMN != "")
                                        {
                                            COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                        }
                                        else
                                        {
                                            COLUMN = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                        }

                                        if (DATA != "")
                                        {
                                            if (strdata2 != "")
                                            {
                                                DATA = DATA + " @ni$h N'" + strdata2 + "'";
                                            }
                                            else
                                            {
                                                DATA = DATA + " @ni$h NULL";
                                            }
                                        }
                                        else
                                        {
                                            if (strdata2 != "")
                                            {
                                                DATA = "N'" + strdata2 + "'";
                                            }
                                            else
                                            {
                                                DATA = "NULL";
                                            }
                                        }
                                    }
                                }


                                if (!isColAdded)
                                {
                                    if (((Label)grd_Data2.Rows[c].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data2.Rows[c].FindControl("AutoNum")).Text == "False")
                                    {
                                        if (strdata2 != "")
                                        {
                                            ADD_NEW_ROW_DATA(((Label)grd_Data2.Rows[c].FindControl("lblFieldName1")).Text,
                                            ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text,
                                            strdata2.Replace("N'", "").Replace("'", ""),
                                            DBNull.Value.ToString(),
                                            "Parent Field Updated",
                                            DBNull.Value.ToString(),
                                            RECID
                                            );
                                        }
                                    }

                                    if (COLUMN != "")
                                    {
                                        COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                    }
                                    else
                                    {
                                        COLUMN = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                    }

                                    if (DATA != "")
                                    {
                                        DATA = DATA + " @ni$h NULL";
                                    }
                                    else
                                    {
                                        DATA = "NULL";
                                    }
                                }

                                if (strdata2 != "" && Strata != "0")
                                {
                                    dal_DM.DM_IWRS_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata2, SUBJID: Request.QueryString["SUBJID"].ToString());
                                }
                            }

                            GridView grd_Data3 = grd_Data2.Rows[c].FindControl("grd_Data3") as GridView;

                            for (int d = 0; d < grd_Data3.Rows.Count; d++)
                            {
                                string Val_Child2 = ((Label)grd_Data3.Rows[d].FindControl("lblVal_Child")).Text;
                                string strdata3 = "";
                                varname = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                Strata = ((Label)grd_Data3.Rows[d].FindControl("lblSTRATA")).Text;
                                CONTROLTYPE = ((Label)grd_Data3.Rows[d].FindControl("lblCONTROLTYPE")).Text;
                                REQUIREDYN = ((Label)grd_Data3.Rows[d].FindControl("lblREQUIREDYN")).Text;
                                READYN = ((Label)grd_Data3.Rows[d].FindControl("READYN")).Text;

                                if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                                {
                                    if (CONTROLTYPE == "TEXTBOX")
                                    {
                                        strdata3 = ((TextBox)grd_Data3.Rows[d].FindControl("TXT_FIELD")).Text;
                                    }
                                    else if (CONTROLTYPE == "DROPDOWN")
                                    {
                                        if (((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedIndex == -1)
                                        {
                                            strdata3 = ((HiddenField)grd_Data3.Rows[d].FindControl("HDN_FIELD")).Value;
                                        }
                                        else if (((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                        {
                                            strdata3 = ((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedValue;
                                        }
                                        else
                                        {
                                            strdata3 = "";
                                        }
                                    }
                                    else if (CONTROLTYPE == "CHECKBOX")
                                    {
                                        Repeater repeat_CHK = grd_Data3.Rows[d].FindControl("repeat_CHK") as Repeater;
                                        for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                        {
                                            if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                            {
                                                if (strdata3.ToString() == "")
                                                {
                                                    strdata3 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                                }
                                                else
                                                {
                                                    strdata3 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                                }
                                            }
                                        }
                                    }
                                    else if (CONTROLTYPE == "RADIOBUTTON")
                                    {
                                        Repeater repeat_RAD = grd_Data3.Rows[d].FindControl("repeat_RAD") as Repeater;
                                        for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                        {
                                            if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                            {
                                                strdata3 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                            }
                                        }
                                    }

                                    if (READYN != "True")
                                    {
                                        if (COLUMN_Audit != "")
                                        {
                                            COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                        }
                                        else
                                        {
                                            COLUMN_Audit = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                        }
                                    }

                                    if (FieldName != "")
                                    {
                                        FieldName = FieldName + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblFieldName1")).Text + "";
                                    }
                                    else
                                    {
                                        FieldName = ((Label)grd_Data3.Rows[d].FindControl("lblFieldName1")).Text;
                                    }

                                    if (strdata3 != "")
                                    {
                                        strdata3 = strdata3.Replace("'", "''");
                                    }

                                    isColAdded = false;

                                    foreach (string val in strdata2.Split('¸'))
                                    {
                                        if (checkStringContains(Val_Child2, val) || (Val_Child2 == "Is Not Blank" && strdata2 != "") || Val_Child2 == "Compare")
                                        {
                                            isColAdded = true;

                                            if (REQUIREDYN == "True" && strdata3.Trim() == "")
                                            {
                                                HasMissing = true;
                                            }

                                            if (COLUMN != "")
                                            {
                                                COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                            }
                                            else
                                            {
                                                COLUMN = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                            }

                                            if (DATA != "")
                                            {
                                                if (strdata3 != "")
                                                {
                                                    DATA = DATA + " @ni$h N'" + strdata3 + "'";
                                                }
                                                else
                                                {
                                                    DATA = DATA + " @ni$h NULL";
                                                }
                                            }
                                            else
                                            {
                                                if (strdata3 != "")
                                                {
                                                    DATA = "N'" + strdata3 + "'";
                                                }
                                                else
                                                {
                                                    DATA = "NULL";
                                                }
                                            }
                                        }
                                    }

                                    if (!isColAdded)
                                    {
                                        if (((Label)grd_Data3.Rows[d].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data3.Rows[d].FindControl("AutoNum")).Text == "False")
                                        {
                                            if (strdata3 != "")
                                            {
                                                ADD_NEW_ROW_DATA(((Label)grd_Data3.Rows[d].FindControl("lblFieldName1")).Text,
                                                    ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text,
                                                    strdata3.Replace("N'", "").Replace("'", ""),
                                                    DBNull.Value.ToString(),
                                                    "Parent Field Updated",
                                                    DBNull.Value.ToString(),
                                                    RECID
                                                    );
                                            }
                                        }

                                        if (COLUMN != "")
                                        {
                                            COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                        }
                                        else
                                        {
                                            COLUMN = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                        }

                                        if (DATA != "")
                                        {
                                            DATA = DATA + " @ni$h NULL";
                                        }
                                        else
                                        {
                                            DATA = "NULL";
                                        }
                                    }

                                    if (strdata3 != "" && Strata != "0")
                                    {
                                        dal_DM.DM_IWRS_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata3, SUBJID: Request.QueryString["SUBJID"].ToString());
                                    }
                                }
                                strdata3 = "";
                            }
                            strdata2 = "";
                        }
                        strdata1 = "";
                    }
                    strdata = "";
                }

                //DDC & EDC Insert Data Query
                INSERTQUERY = "INSERT INTO [" + hfTablename.Value + "] ([PVID], [RECID], [SUBJID_DATA], [VISITNUM], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + hdnPVID.Value + "', '" + hdnRECID.Value + "', '" + Request.QueryString["SUBJID"].ToString() + "', '" + hdnVISITID.Value + "', '" + Session["User_ID"].ToString() + "','" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + ")";

                string[] colArr = COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                string[] dataArr = DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                string[] fieldArr = FieldName.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                string[] colArr_Audit = COLUMN_Audit.Split(new string[] { " @ni$h " }, StringSplitOptions.None);

                for (int i = 0; i < colArr.Length; i++)
                {
                    if (UPDATEQUERY == "")
                    {
                        UPDATEQUERY = "UPDATE [" + hfTablename.Value + "] SET UPDATEDDAT = GETDATE(), UPDATEDBYNAME = '" + Session["User_Name"].ToString() + "', UPDATED_TZVAL = '" + Session["TimeZone_Value"].ToString() + "', UPDATEDBY = '" + Session["USER_ID"].ToString() + "' ";

                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[i] + " = " + dataArr[i] + " ";
                    }
                    else
                    {
                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[i] + " = " + dataArr[i] + " ";
                    }

                    //Create Bulk Audit Trail dataset
                    if (hdn_PAGESTATUS.Value == "0")
                    {
                        if (colArr_Audit.Contains(colArr[i].Trim()))
                        {
                            ADD_NEW_ROW_DATA(fieldArr[i], colArr[i], "", dataArr[i].Replace("N'", "").Replace("'", ""), "Initial Entry", "", RECID);
                        }
                    }
                }

                UPDATEQUERY = UPDATEQUERY + " WHERE PVID = '" + hdnPVID.Value + "' AND RECID = '" + RECID + "' ";

                dal_DM.DM_INSERT_MODULE_DATA_SP(
                 ACTION: "INSERT_MODULE_DATA",
                 INSERTQUERY: INSERTQUERY,
                 UPDATEQUERY: UPDATEQUERY,
                 TABLENAME: hfTablename.Value,
                 PVID: hdnPVID.Value,
                 RECID: RECID,
                 SUBJID: Request.QueryString["SUBJID"].ToString(),
                 MODULEID: Request.QueryString["MODULEID"].ToString(),
                 VISITNUM: hdnVISITID.Value,
                 IsComplete: true,
                 IsMissing: HasMissing,
                 INVID: Request.QueryString["INVID"].ToString(),
                 VISIT: drpVisit.SelectedItem.Text,
                 MODULENAME: drpModule.SelectedItem.Text
                 );

                //eSource & DM Insert/Update PV Details Query
                dal_DM.DM_GetSetPV(
                    PVID: hdnPVID.Value,
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    VISITNUM: hdnVISITID.Value,
                    PAGESTATUS: "1",
                    HasMissing: HasMissing,
                    INVID: Request.QueryString["INVID"].ToString(),
                    VISIT: drpVisit.SelectedItem.Text,
                    MODULENAME: drpModule.SelectedItem.Text
                    );

                //Insert Bulk Audit Trail dataset
                if (dt_AuditTrail.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal_DM.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "DM_AUDITTRAIL";

                        sqlbc.ColumnMappings.Add("SOURCE", "SOURCE");
                        sqlbc.ColumnMappings.Add("PVID", "PVID");
                        sqlbc.ColumnMappings.Add("RECID", "RECID");
                        sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                        sqlbc.ColumnMappings.Add("VISITNUM", "VISITNUM");
                        sqlbc.ColumnMappings.Add("MODULENAME", "MODULENAME");
                        sqlbc.ColumnMappings.Add("FIELDNAME", "FIELDNAME");
                        sqlbc.ColumnMappings.Add("TABLENAME", "TABLENAME");
                        sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                        sqlbc.ColumnMappings.Add("OLDVALUE", "OLDVALUE");
                        sqlbc.ColumnMappings.Add("NEWVALUE", "NEWVALUE");
                        sqlbc.ColumnMappings.Add("REASON", "REASON");
                        sqlbc.ColumnMappings.Add("COMMENTS", "COMMENTS");
                        sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                        sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                        sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                        sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");
                        sqlbc.ColumnMappings.Add("DM", "DM");

                        sqlbc.WriteToServer(dt_AuditTrail);

                        if (hdn_PAGESTATUS.Value == "1")
                        {
                            if (ViewState["dt_AutoCode"] != null)
                            {
                                dt_AutoCode = ViewState["dt_AutoCode"] as DataTable;
                            }

                            if (dt_AutoCode != null && dt_AutoCode.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt_AutoCode.Rows.Count; i++)
                                {
                                    if (dt_AuditTrail.Select("VARIABLENAME = '" + dt_AutoCode.Rows[i]["VARIABLENAME"].ToString() + "'").Length > 0)
                                    {
                                        dal_DM.CODING_SP(
                                             ACTION: "AUTOCODE_CHANGE_DATA",
                                             PVID: hdnPVID.Value,
                                             RECID: RECID,
                                             MODULEID: drpModule.SelectedValue,
                                             VARIABLENAME: dt_AutoCode.Rows[i]["VARIABLENAME"].ToString()
                                             );
                                    }
                                }
                            }

                            dal_DM.DM_AUDITTRAIL_SP(
                                ACTION: "DATA_UPDATED_AUDITTRAIL_ACTION",
                                PVID: hdnPVID.Value,
                                RECID: RECID,
                                SUBJID: Request.QueryString["SUBJID"].ToString(),
                                VISITNUM: Request.QueryString["VISITID"].ToString(),
                                MODULEID: Request.QueryString["MODULEID"].ToString(),
                                FIELDNAME: dt_AuditTrail.Rows[0]["FIELDNAME"].ToString(),
                                VARIABLENAME: dt_AuditTrail.Rows[0]["VARIABLENAME"].ToString(),
                                REASON: dt_AuditTrail.Rows[0]["REASON"].ToString(),
                                INVID: Request.QueryString["INVID"].ToString(),
                                VISIT: drpVisit.SelectedItem.Text,
                                MODULENAME: drpModule.SelectedItem.Text
                                );

                            ADD_UPDATED_AT_ENTRY_LOGS(dt_AuditTrail, RECID);
                        }

                        dt_AuditTrail.Clear();
                    }
                }

                commFun.AutoSet_ESOURCE_PVs(hdnPVID.Value, RECID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ADD_UPDATED_AT_ENTRY_LOGS(DataTable dt, string RECID)
        {
            try
            {
                DataColumn dtColumn;
                DataTable DM_ACTIVITY_LOGS = new DataTable("DM_ACTIVITY_LOGS");

                if (DM_ACTIVITY_LOGS.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    DM_ACTIVITY_LOGS.Columns.Add("INVID");
                    DM_ACTIVITY_LOGS.Columns.Add("PVID");
                    DM_ACTIVITY_LOGS.Columns.Add("RECID");
                    DM_ACTIVITY_LOGS.Columns.Add("SUBJID");
                    DM_ACTIVITY_LOGS.Columns.Add("VISITNUM");
                    DM_ACTIVITY_LOGS.Columns.Add("VISIT");
                    DM_ACTIVITY_LOGS.Columns.Add("MODULEID");
                    DM_ACTIVITY_LOGS.Columns.Add("MODULENAME");
                    DM_ACTIVITY_LOGS.Columns.Add("FIELDNAME");
                    DM_ACTIVITY_LOGS.Columns.Add("ACT_TYPE");
                    DM_ACTIVITY_LOGS.Columns.Add("ACT_PERFORMED");
                    DM_ACTIVITY_LOGS.Columns.Add("ACT_DETAILS");
                    DM_ACTIVITY_LOGS.Columns.Add("ACT_BY");
                    DM_ACTIVITY_LOGS.Columns.Add("ACT_BYNAME");
                    DM_ACTIVITY_LOGS.Columns.Add("ACT_DAT");
                    DM_ACTIVITY_LOGS.Columns.Add("ACT_TZVAL");
                    DM_ACTIVITY_LOGS.Columns.Add("DM");
                }

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow myDataRow;
                    myDataRow = DM_ACTIVITY_LOGS.NewRow();
                    myDataRow["INVID"] = Request.QueryString["INVID"].ToString();
                    myDataRow["PVID"] = hdnPVID.Value;
                    myDataRow["RECID"] = RECID;
                    myDataRow["SUBJID"] = Request.QueryString["SUBJID"].ToString();
                    myDataRow["VISITNUM"] = hdnVISITID.Value;
                    myDataRow["VISIT"] = Request.QueryString["VISIT"].ToString();
                    myDataRow["MODULEID"] = Request.QueryString["MODULEID"].ToString();
                    myDataRow["MODULENAME"] = lblModuleName.Text;
                    myDataRow["FIELDNAME"] = dt.Rows[j]["FIELDNAME"].ToString().Trim();
                    myDataRow["ACT_TYPE"] = "Data Entry";
                    myDataRow["ACT_PERFORMED"] = dt.Rows[j]["Reason"].ToString().Trim();
                    myDataRow["ACT_DETAILS"] = REMOVEHTML(dt.Rows[j]["NEWVALUE"].ToString()).Replace("N'", "").Replace("'", "").Trim();
                    myDataRow["ACT_BY"] = Session["User_ID"].ToString();
                    myDataRow["ACT_BYNAME"] = Session["User_Name"].ToString();
                    myDataRow["ACT_DAT"] = DateTime.Now;
                    myDataRow["ACT_TZVAL"] = Session["TimeZone_Value"].ToString();
                    myDataRow["DM"] = 1;
                    DM_ACTIVITY_LOGS.Rows.Add(myDataRow);
                }

                //Insert Bulk Audit Trail dataset
                if (DM_ACTIVITY_LOGS.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal_DM.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "DM_ACTIVITY_LOGS";

                        sqlbc.ColumnMappings.Add("INVID", "INVID");
                        sqlbc.ColumnMappings.Add("PVID", "PVID");
                        sqlbc.ColumnMappings.Add("RECID", "RECID");
                        sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                        sqlbc.ColumnMappings.Add("VISITNUM", "VISITNUM");
                        sqlbc.ColumnMappings.Add("VISIT", "VISIT");
                        sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                        sqlbc.ColumnMappings.Add("MODULENAME", "MODULENAME");
                        sqlbc.ColumnMappings.Add("FIELDNAME", "FIELDNAME");
                        sqlbc.ColumnMappings.Add("ACT_TYPE", "ACT_TYPE");
                        sqlbc.ColumnMappings.Add("ACT_PERFORMED", "ACT_PERFORMED");
                        sqlbc.ColumnMappings.Add("ACT_DETAILS", "ACT_DETAILS");
                        sqlbc.ColumnMappings.Add("ACT_BY", "ACT_BY");
                        sqlbc.ColumnMappings.Add("ACT_BYNAME", "ACT_BYNAME");
                        sqlbc.ColumnMappings.Add("ACT_DAT", "ACT_DAT");
                        sqlbc.ColumnMappings.Add("ACT_TZVAL", "ACT_TZVAL");
                        sqlbc.ColumnMappings.Add("DM", "DM");

                        sqlbc.WriteToServer(DM_ACTIVITY_LOGS);

                        DM_ACTIVITY_LOGS.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void btnSaveIncomplete_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHECK_OnSubmit_CRITs())
                {
                    Session.Remove("RedirceToAnotherPage");

                    INSERT_FORM_DATA_Incomplete();

                    UPDATE_SAE_SYNC();

                    if (hdn_PAGESTATUS.Value == "0")
                    {
                        AutoCode();
                    }

                    CHECK_OnSubmit_EMAIL_CRITs();

                    if (Request.QueryString["OPENLINK"] == "1")
                    {
                        Response.Write("<script> alert('Record saved as incomplete successfully.'); window.close(); </script>");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record saved as incomplete successfully.');window.location.href = 'DM_DataEntry.aspx?MODULEID=" + Request.QueryString["MODULEID"].ToString() + "&MODULENAME=" + Request.QueryString["MODULENAME"].ToString() + "&VISITID=" + Request.QueryString["VISITID"].ToString() + "&VISIT=" + Request.QueryString["VISIT"].ToString() + "&INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "';", true);
                    }
                }
                else
                {
                    if (hdnAllowable.Value == "True")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmMsg();", true);
                    }
                    else
                    {
                        // Convert the value to Base64
                        string base64Msg = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(hdnError_Msg.Value));

                        // Display the Base64-encoded message safely in JavaScript
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                            $"var encodedMsg = '{base64Msg}'; alert(atob(encodedMsg));", true);

                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + hdnError_Msg.Value + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        private void INSERT_FORM_DATA_Incomplete()
        {
            try
            {
                bool isColAdded = false, HasMissing = false;

                string COLUMN = "", varname = "", DATA = "", FieldName = "", Strata = "", INSERTQUERY = "", UPDATEQUERY = "", RECID = "", COLUMN_Audit = "";

                DataSet ds = dal_DM.DM_RECID_SP(
                   PVID: lblPVID.Text,
                   TABLENAME: hfTablename.Value,
                   SUBJID: Request.QueryString["SUBJID"].ToString()
                   );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    RECID = (Convert.ToInt32(ds.Tables[0].Rows[0]["RECID"]) + 1).ToString();
                }
                else
                {
                    RECID = "0";
                }

                if (hdnRECID.Value != "-1")
                {
                    RECID = hdnRECID.Value;
                }

                hdnRECID.Value = RECID;

                for (int rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
                {
                    string strdata = "";
                    varname = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                    Strata = ((Label)grd_Data.Rows[rownum].FindControl("lblSTRATA")).Text;
                    string CONTROLTYPE;
                    CONTROLTYPE = ((Label)grd_Data.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                    string REQUIREDYN;
                    REQUIREDYN = ((Label)grd_Data.Rows[rownum].FindControl("lblREQUIREDYN")).Text;
                    string READYN = ((Label)grd_Data.Rows[rownum].FindControl("READYN")).Text;

                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                    {
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            strdata = ((TextBox)grd_Data.Rows[rownum].FindControl("TXT_FIELD")).Text;
                        }
                        else if (CONTROLTYPE == "DROPDOWN")
                        {
                            if (((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedIndex == -1)
                            {
                                strdata = ((HiddenField)grd_Data.Rows[rownum].FindControl("HDN_FIELD")).Value;
                            }
                            else if (((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                            {
                                strdata = ((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedValue;
                            }
                            else
                            {
                                strdata = "";
                            }
                        }
                        else if (CONTROLTYPE == "CHECKBOX")
                        {
                            Repeater repeat_CHK = grd_Data.Rows[rownum].FindControl("repeat_CHK") as Repeater;
                            for (int a = 0; a < repeat_CHK.Items.Count; a++)
                            {
                                if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                {
                                    if (strdata.ToString() == "")
                                    {
                                        strdata = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                    }
                                    else
                                    {
                                        strdata += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                    }
                                }
                            }
                        }
                        else if (CONTROLTYPE == "RADIOBUTTON")
                        {
                            Repeater repeat_RAD = grd_Data.Rows[rownum].FindControl("repeat_RAD") as Repeater;
                            for (int a = 0; a < repeat_RAD.Items.Count; a++)
                            {
                                if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                {
                                    strdata = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                }
                            }
                        }

                        if (FieldName != "")
                        {
                            FieldName = FieldName + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblFieldName1")).Text + "";
                        }
                        else
                        {
                            FieldName = ((Label)grd_Data.Rows[rownum].FindControl("lblFieldName1")).Text;
                        }

                        if (READYN != "True")
                        {
                            if (COLUMN_Audit != "")
                            {
                                COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                            }
                            else
                            {
                                COLUMN_Audit = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                            }
                        }

                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                        }
                        else
                        {
                            COLUMN = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                        }

                        if (strdata != "")
                        {
                            strdata = strdata.Replace("'", "''");
                        }

                        if (REQUIREDYN == "True" && strdata.Trim() == "")
                        {
                            HasMissing = true;
                        }

                        if (DATA != "")
                        {
                            if (strdata != "")
                            {
                                DATA = DATA + " @ni$h N'" + strdata + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h ''";
                            }
                        }
                        else
                        {
                            if (strdata != "")
                            {
                                DATA = "N'" + strdata + "'";
                            }
                            else
                            {
                                DATA = "''";
                            }
                        }
                    }

                    if (strdata != "" && Strata != "0")
                    {
                        dal_DM.DM_IWRS_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata, SUBJID: Request.QueryString["SUBJID"].ToString());
                    }

                    GridView grd_Data1 = grd_Data.Rows[rownum].FindControl("grd_Data1") as GridView;

                    for (int b = 0; b < grd_Data1.Rows.Count; b++)
                    {
                        string Val_Child;
                        string strdata1 = "";
                        Val_Child = ((Label)grd_Data1.Rows[b].FindControl("lblVal_Child")).Text;

                        varname = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                        Strata = ((Label)grd_Data1.Rows[b].FindControl("lblSTRATA")).Text;
                        CONTROLTYPE = ((Label)grd_Data1.Rows[b].FindControl("lblCONTROLTYPE")).Text;
                        REQUIREDYN = ((Label)grd_Data1.Rows[b].FindControl("lblREQUIREDYN")).Text;
                        READYN = ((Label)grd_Data1.Rows[b].FindControl("READYN")).Text;

                        if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                        {
                            if (CONTROLTYPE == "TEXTBOX")
                            {
                                strdata1 = ((TextBox)grd_Data1.Rows[b].FindControl("TXT_FIELD")).Text;
                            }
                            else if (CONTROLTYPE == "DROPDOWN")
                            {
                                if (((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedIndex == -1)
                                {
                                    strdata1 = ((HiddenField)grd_Data1.Rows[b].FindControl("HDN_FIELD")).Value;
                                }
                                else if (((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                {
                                    strdata1 = ((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedValue;
                                }
                                else
                                {
                                    strdata1 = "";
                                }
                            }
                            else if (CONTROLTYPE == "CHECKBOX")
                            {
                                Repeater repeat_CHK = grd_Data1.Rows[b].FindControl("repeat_CHK") as Repeater;
                                for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                {
                                    if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                    {
                                        if (strdata1.ToString() == "")
                                        {
                                            strdata1 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                        }
                                        else
                                        {
                                            strdata1 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                        }
                                    }
                                }
                            }
                            else if (CONTROLTYPE == "RADIOBUTTON")
                            {
                                Repeater repeat_RAD = grd_Data1.Rows[b].FindControl("repeat_RAD") as Repeater;
                                for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                {
                                    if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                    {
                                        strdata1 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                    }
                                }
                            }

                            if (FieldName != "")
                            {
                                FieldName = FieldName + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblFieldName1")).Text + "";
                            }
                            else
                            {
                                FieldName = ((Label)grd_Data1.Rows[b].FindControl("lblFieldName1")).Text;
                            }

                            if (READYN != "True")
                            {
                                if (COLUMN_Audit != "")
                                {
                                    COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                }
                                else
                                {
                                    COLUMN_Audit = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                }
                            }
                            if (strdata1 != "")
                            {
                                strdata1 = strdata1.Replace("'", "''");
                            }

                            isColAdded = false;

                            foreach (string val in strdata.Split('¸'))
                            {
                                if (checkStringContains(Val_Child, val) || (Val_Child == "Is Not Blank" && strdata != "") || Val_Child == "Compare")
                                {
                                    isColAdded = true;

                                    if (REQUIREDYN == "True" && strdata1.Trim() == "")
                                    {
                                        HasMissing = true;
                                    }

                                    if (COLUMN != "")
                                    {
                                        COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                    }
                                    else
                                    {
                                        COLUMN = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                    }

                                    if (DATA != "")
                                    {
                                        if (strdata1 != "")
                                        {
                                            DATA = DATA + " @ni$h N'" + strdata1 + "'";
                                        }
                                        else
                                        {
                                            DATA = DATA + " @ni$h NULL";
                                        }
                                    }
                                    else
                                    {
                                        if (strdata1 != "")
                                        {
                                            DATA = "N'" + strdata1 + "'";
                                        }
                                        else
                                        {
                                            DATA = "NULL";
                                        }
                                    }
                                }
                            }


                            if (!isColAdded)
                            {
                                if (((Label)grd_Data1.Rows[b].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data1.Rows[b].FindControl("AutoNum")).Text == "False")
                                {
                                    if (strdata1 != "")
                                    {
                                        ADD_NEW_ROW_DATA(((Label)grd_Data1.Rows[b].FindControl("lblFieldName1")).Text,
                                            ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text,
                                            strdata1.Replace("N'", "").Replace("'", ""),
                                            DBNull.Value.ToString(),
                                            "Parent Field Updated",
                                            DBNull.Value.ToString(),
                                            RECID
                                            );
                                    }
                                }

                                if (COLUMN != "")
                                {
                                    COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                }
                                else
                                {
                                    COLUMN = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                }

                                if (DATA != "")
                                {
                                    DATA = DATA + " @ni$h NULL";
                                }
                                else
                                {
                                    DATA = "NULL";
                                }
                            }

                            if (strdata1 != "" && Strata != "0")
                            {
                                dal_DM.DM_IWRS_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata1, SUBJID: Request.QueryString["SUBJID"].ToString());
                            }
                        }

                        GridView grd_Data2 = grd_Data1.Rows[b].FindControl("grd_Data2") as GridView;

                        for (int c = 0; c < grd_Data2.Rows.Count; c++)
                        {
                            string Val_Child1 = ((Label)grd_Data2.Rows[c].FindControl("lblVal_Child")).Text;
                            string strdata2 = "";
                            varname = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                            Strata = ((Label)grd_Data2.Rows[c].FindControl("lblSTRATA")).Text;
                            CONTROLTYPE = ((Label)grd_Data2.Rows[c].FindControl("lblCONTROLTYPE")).Text;
                            REQUIREDYN = ((Label)grd_Data2.Rows[c].FindControl("lblREQUIREDYN")).Text;
                            READYN = ((Label)grd_Data2.Rows[c].FindControl("READYN")).Text;

                            if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                            {
                                if (CONTROLTYPE == "TEXTBOX")
                                {
                                    strdata2 = ((TextBox)grd_Data2.Rows[c].FindControl("TXT_FIELD")).Text;
                                }
                                else if (CONTROLTYPE == "DROPDOWN")
                                {
                                    if (((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedIndex == -1)
                                    {
                                        strdata2 = ((HiddenField)grd_Data2.Rows[c].FindControl("HDN_FIELD")).Value;
                                    }
                                    else if (((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                    {
                                        strdata2 = ((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedValue;
                                    }
                                    else
                                    {
                                        strdata2 = "";
                                    }
                                }
                                else if (CONTROLTYPE == "CHECKBOX")
                                {
                                    Repeater repeat_CHK = grd_Data2.Rows[c].FindControl("repeat_CHK") as Repeater;
                                    for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                    {
                                        if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                        {
                                            if (strdata2.ToString() == "")
                                            {
                                                strdata2 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                            }
                                            else
                                            {
                                                strdata2 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                            }
                                        }
                                    }
                                }
                                else if (CONTROLTYPE == "RADIOBUTTON")
                                {
                                    Repeater repeat_RAD = grd_Data2.Rows[c].FindControl("repeat_RAD") as Repeater;
                                    for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                    {
                                        if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                        {
                                            strdata2 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                        }
                                    }
                                }

                                if (FieldName != "")
                                {
                                    FieldName = FieldName + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblFieldName1")).Text + "";
                                }
                                else
                                {
                                    FieldName = ((Label)grd_Data2.Rows[c].FindControl("lblFieldName1")).Text;
                                }

                                if (READYN != "True")
                                {
                                    if (COLUMN_Audit != "")
                                    {
                                        COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                    }
                                    else
                                    {
                                        COLUMN_Audit = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                    }
                                }

                                if (strdata2 != "")
                                {
                                    strdata2 = strdata2.Replace("'", "''");
                                }

                                isColAdded = false;

                                foreach (string val in strdata1.Split('¸'))
                                {
                                    if (checkStringContains(Val_Child1, val) || (Val_Child1 == "Is Not Blank" && strdata1 != "") || Val_Child1 == "Compare")
                                    {
                                        isColAdded = true;

                                        if (REQUIREDYN == "True" && strdata2.Trim() == "")
                                        {
                                            HasMissing = true;
                                        }

                                        if (COLUMN != "")
                                        {
                                            COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                        }
                                        else
                                        {
                                            COLUMN = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                        }

                                        if (DATA != "")
                                        {
                                            if (strdata2 != "")
                                            {
                                                DATA = DATA + " @ni$h N'" + strdata2 + "'";
                                            }
                                            else
                                            {
                                                DATA = DATA + " @ni$h NULL";
                                            }
                                        }
                                        else
                                        {
                                            if (strdata2 != "")
                                            {
                                                DATA = "N'" + strdata2 + "'";
                                            }
                                            else
                                            {
                                                DATA = "NULL";
                                            }
                                        }
                                    }
                                }


                                if (!isColAdded)
                                {
                                    if (((Label)grd_Data2.Rows[c].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data2.Rows[c].FindControl("AutoNum")).Text == "False")
                                    {
                                        if (strdata2 != "")
                                        {
                                            ADD_NEW_ROW_DATA(((Label)grd_Data2.Rows[c].FindControl("lblFieldName1")).Text,
                                                ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text,
                                                strdata2.Replace("N'", "").Replace("'", ""),
                                                DBNull.Value.ToString(),
                                                "Parent Field Updated",
                                                DBNull.Value.ToString(),
                                                RECID
                                                );
                                        }
                                    }

                                    if (COLUMN != "")
                                    {
                                        COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                    }
                                    else
                                    {
                                        COLUMN = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                    }

                                    if (DATA != "")
                                    {
                                        DATA = DATA + " @ni$h NULL";
                                    }
                                    else
                                    {
                                        DATA = "NULL";
                                    }
                                }

                                if (strdata2 != "" && Strata != "0")
                                {
                                    dal_DM.DM_IWRS_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata2, SUBJID: Request.QueryString["SUBJID"].ToString());
                                }
                            }

                            GridView grd_Data3 = grd_Data2.Rows[c].FindControl("grd_Data3") as GridView;

                            for (int d = 0; d < grd_Data3.Rows.Count; d++)
                            {
                                string Val_Child2 = ((Label)grd_Data3.Rows[d].FindControl("lblVal_Child")).Text;
                                string strdata3 = "";
                                varname = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                Strata = ((Label)grd_Data3.Rows[d].FindControl("lblSTRATA")).Text;
                                CONTROLTYPE = ((Label)grd_Data3.Rows[d].FindControl("lblCONTROLTYPE")).Text;
                                REQUIREDYN = ((Label)grd_Data3.Rows[d].FindControl("lblREQUIREDYN")).Text;
                                READYN = ((Label)grd_Data3.Rows[d].FindControl("READYN")).Text;

                                if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                                {
                                    if (CONTROLTYPE == "TEXTBOX")
                                    {
                                        strdata3 = ((TextBox)grd_Data3.Rows[d].FindControl("TXT_FIELD")).Text;
                                    }
                                    else if (CONTROLTYPE == "DROPDOWN")
                                    {
                                        if (((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedIndex == -1)
                                        {
                                            strdata3 = ((HiddenField)grd_Data3.Rows[d].FindControl("HDN_FIELD")).Value;
                                        }
                                        else if (((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                        {
                                            strdata3 = ((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedValue;
                                        }
                                        else
                                        {
                                            strdata3 = "";
                                        }
                                    }
                                    else if (CONTROLTYPE == "CHECKBOX")
                                    {
                                        Repeater repeat_CHK = grd_Data3.Rows[d].FindControl("repeat_CHK") as Repeater;
                                        for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                        {
                                            if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                            {
                                                if (strdata3.ToString() == "")
                                                {
                                                    strdata3 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                                }
                                                else
                                                {
                                                    strdata3 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                                }
                                            }
                                        }
                                    }
                                    else if (CONTROLTYPE == "RADIOBUTTON")
                                    {
                                        Repeater repeat_RAD = grd_Data3.Rows[d].FindControl("repeat_RAD") as Repeater;
                                        for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                        {
                                            if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                            {
                                                strdata3 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                            }
                                        }
                                    }

                                    if (FieldName != "")
                                    {
                                        FieldName = FieldName + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblFieldName1")).Text + "";
                                    }
                                    else
                                    {
                                        FieldName = ((Label)grd_Data3.Rows[d].FindControl("lblFieldName1")).Text;
                                    }

                                    if (READYN != "True")
                                    {
                                        if (COLUMN_Audit != "")
                                        {
                                            COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                        }
                                        else
                                        {
                                            COLUMN_Audit = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                        }
                                    }

                                    if (strdata3 != "")
                                    {
                                        strdata3 = strdata3.Replace("'", "''");
                                    }

                                    isColAdded = false;

                                    foreach (string val in strdata2.Split('¸'))
                                    {
                                        if (checkStringContains(Val_Child2, val) || (Val_Child2 == "Is Not Blank" && strdata2 != "") || Val_Child2 == "Compare")
                                        {
                                            isColAdded = true;

                                            if (REQUIREDYN == "True" && strdata3.Trim() == "")
                                            {
                                                HasMissing = true;
                                            }

                                            if (COLUMN != "")
                                            {
                                                COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                            }
                                            else
                                            {
                                                COLUMN = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                            }

                                            if (DATA != "")
                                            {
                                                if (strdata3 != "")
                                                {
                                                    DATA = DATA + " @ni$h N'" + strdata3 + "'";
                                                }
                                                else
                                                {
                                                    DATA = DATA + " @ni$h NULL";
                                                }
                                            }
                                            else
                                            {
                                                if (strdata3 != "")
                                                {
                                                    DATA = "N'" + strdata3 + "'";
                                                }
                                                else
                                                {
                                                    DATA = "NULL";
                                                }
                                            }
                                        }
                                    }

                                    if (!isColAdded)
                                    {
                                        if (((Label)grd_Data3.Rows[d].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data3.Rows[d].FindControl("AutoNum")).Text == "False")
                                        {
                                            if (strdata3 != "")
                                            {
                                                ADD_NEW_ROW_DATA(((Label)grd_Data3.Rows[d].FindControl("lblFieldName1")).Text,
                                                    ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text,
                                                    strdata3.Replace("N'", "").Replace("'", ""),
                                                    DBNull.Value.ToString(),
                                                    "Parent Field Updated",
                                                    DBNull.Value.ToString(),
                                                    RECID
                                                    );
                                            }
                                        }

                                        if (COLUMN != "")
                                        {
                                            COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                        }
                                        else
                                        {
                                            COLUMN = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                        }

                                        if (DATA != "")
                                        {
                                            DATA = DATA + " @ni$h NULL";
                                        }
                                        else
                                        {
                                            DATA = "NULL";
                                        }
                                    }

                                    if (strdata3 != "" && Strata != "0")
                                    {
                                        dal_DM.DM_IWRS_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata3, SUBJID: Request.QueryString["SUBJID"].ToString());
                                    }
                                }
                                strdata3 = "";
                            }
                            strdata2 = "";
                        }
                        strdata1 = "";
                    }
                    strdata = "";
                }

                //DDC & EDC Insert Data Query
                INSERTQUERY = "INSERT INTO [" + hfTablename.Value + "] ([PVID], [RECID], [SUBJID_DATA], [VISITNUM], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + hdnPVID.Value + "', '" + hdnRECID.Value + "', '" + Request.QueryString["SUBJID"].ToString() + "', '" + hdnVISITID.Value + "', '" + Session["User_ID"].ToString() + "','" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + ")";


                string[] colArr = COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                string[] dataArr = DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                string[] fieldArr = FieldName.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                string[] colArr_Audit = COLUMN_Audit.Split(new string[] { " @ni$h " }, StringSplitOptions.None);

                //Create Bulk Audit Trail dataset
                for (int i = 0; i < colArr.Length; i++)
                {
                    if (UPDATEQUERY == "")
                    {
                        UPDATEQUERY = "UPDATE [" + hfTablename.Value + "] SET UPDATEDDAT = GETDATE(), UPDATEDBYNAME = '" + Session["User_Name"].ToString() + "', UPDATED_TZVAL = '" + Session["TimeZone_Value"].ToString() + "', UPDATEDBY = '" + Session["USER_ID"].ToString() + "' ";

                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[i] + " = " + dataArr[i] + " ";
                    }
                    else
                    {
                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[i] + " = " + dataArr[i] + " ";
                    }

                    //Create Bulk Audit Trail dataset
                    if (hdn_PAGESTATUS.Value == "0")
                    {
                        if (colArr_Audit.Contains(colArr[i].Trim()))
                        {
                            ADD_NEW_ROW_DATA(fieldArr[i], colArr[i], "", dataArr[i].Replace("N'", "").Replace("'", ""), "Initial Entry", "", RECID);
                        }
                    }
                }

                UPDATEQUERY = UPDATEQUERY + " WHERE PVID = '" + hdnPVID.Value + "' AND RECID = '" + RECID + "' ";

                //eSource & DM & IWRS Insert Data Query
                dal_DM.DM_INSERT_MODULE_DATA_SP(
                 ACTION: "INSERT_MODULE_DATA",
                 INSERTQUERY: INSERTQUERY,
                 UPDATEQUERY: UPDATEQUERY,
                 TABLENAME: hfTablename.Value,
                 PVID: hdnPVID.Value,
                 RECID: RECID,
                 SUBJID: Request.QueryString["SUBJID"].ToString(),
                 MODULEID: Request.QueryString["MODULEID"].ToString(),
                 VISITNUM: hdnVISITID.Value,
                 IsComplete: false,
                 IsMissing: HasMissing,
                 INVID: Request.QueryString["INVID"].ToString(),
                 VISIT: drpVisit.SelectedItem.Text,
                 MODULENAME: drpModule.SelectedItem.Text
                 );

                //eSource & DM Insert/Update PV Details Query
                dal_DM.DM_GetSetPV(
                    Action: "InsertUpdatePV_InComplete",
                    PVID: hdnPVID.Value,
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    VISITNUM: hdnVISITID.Value,
                    PAGESTATUS: "1",
                    HasMissing: HasMissing,
                    INVID: Request.QueryString["INVID"].ToString(),
                    VISIT: drpVisit.SelectedItem.Text,
                    MODULENAME: drpModule.SelectedItem.Text
                    );

                //Insert Bulk Audit Trail dataset
                if (dt_AuditTrail.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal_DM.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "DM_AUDITTRAIL";

                        sqlbc.ColumnMappings.Add("SOURCE", "SOURCE");
                        sqlbc.ColumnMappings.Add("PVID", "PVID");
                        sqlbc.ColumnMappings.Add("RECID", "RECID");
                        sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                        sqlbc.ColumnMappings.Add("VISITNUM", "VISITNUM");
                        sqlbc.ColumnMappings.Add("MODULENAME", "MODULENAME");
                        sqlbc.ColumnMappings.Add("FIELDNAME", "FIELDNAME");
                        sqlbc.ColumnMappings.Add("TABLENAME", "TABLENAME");
                        sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                        sqlbc.ColumnMappings.Add("OLDVALUE", "OLDVALUE");
                        sqlbc.ColumnMappings.Add("NEWVALUE", "NEWVALUE");
                        sqlbc.ColumnMappings.Add("REASON", "REASON");
                        sqlbc.ColumnMappings.Add("COMMENTS", "COMMENTS");
                        sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                        sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                        sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                        sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");
                        sqlbc.ColumnMappings.Add("DM", "DM");

                        sqlbc.WriteToServer(dt_AuditTrail);

                        if (hdn_PAGESTATUS.Value == "1")
                        {
                            if (ViewState["dt_AutoCode"] != null)
                            {
                                dt_AutoCode = ViewState["dt_AutoCode"] as DataTable;
                            }

                            if (dt_AutoCode != null && dt_AutoCode.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt_AutoCode.Rows.Count; i++)
                                {
                                    if (dt_AuditTrail.Select("VARIABLENAME = '" + dt_AutoCode.Rows[i]["VARIABLENAME"].ToString() + "'").Length > 0)
                                    {
                                        dal_DM.CODING_SP(
                                             ACTION: "AUTOCODE_CHANGE_DATA",
                                             PVID: hdnPVID.Value,
                                             RECID: RECID,
                                             MODULEID: drpModule.SelectedValue,
                                             VARIABLENAME: dt_AutoCode.Rows[i]["VARIABLENAME"].ToString()
                                             );
                                    }
                                }
                            }

                            dal_DM.DM_AUDITTRAIL_SP(
                                ACTION: "DATA_UPDATED_AUDITTRAIL_ACTION",
                                PVID: hdnPVID.Value,
                                RECID: RECID,
                                SUBJID: Request.QueryString["SUBJID"].ToString(),
                                VISITNUM: Request.QueryString["VISITID"].ToString(),
                                MODULEID: Request.QueryString["MODULEID"].ToString(),
                                FIELDNAME: dt_AuditTrail.Rows[0]["FIELDNAME"].ToString(),
                                VARIABLENAME: dt_AuditTrail.Rows[0]["VARIABLENAME"].ToString(),
                                REASON: dt_AuditTrail.Rows[0]["REASON"].ToString(),
                                INVID: Request.QueryString["INVID"].ToString(),
                                VISIT: drpVisit.SelectedItem.Text,
                                MODULENAME: drpModule.SelectedItem.Text
                                );

                            ADD_UPDATED_AT_ENTRY_LOGS(dt_AuditTrail, RECID);
                        }

                        dt_AuditTrail.Clear();
                    }
                }

                commFun.AutoSet_ESOURCE_PVs(hdnPVID.Value, RECID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool checkStringContains(string arrString, string valString)
        {
            bool res = false;
            try
            {
                if (arrString.Contains('^'))
                {
                    string[] array = arrString.Split('^').ToArray();

                    res = Array.Exists(array, element => element == valString);
                }
                else
                {
                    if (arrString == valString)
                    {
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
            return res;
        }

        protected void btnBacktoHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["RedirceToAnotherPage"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have unsaved changes. Please click on save complete button before navigating to another screen or tab.');", true);
                }
                else
                {
                    if (Convert.ToString(Session["QUERY_URL"]) != "")
                    {
                        Session["BACKTOQUERY_REPORT"] = "1";
                        Response.Redirect(Session["QUERY_URL"].ToString());
                    }
                    else
                    {
                        Response.Redirect("DM_OpenCRF.aspx?VISITNUM=" + Request.QueryString["VISITID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void btnSubmitOnsubmitData_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_FORM_DATA();

                UPDATE_SAE_SYNC();

                Run_Rules();

                CHECK_OnSubmit_EMAIL_CRITs();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record saved as completed successfully.');window.location.href = 'DM_OpenCRF.aspx?VISITNUM=" + Request.QueryString["VISITID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        private bool CHECK_OnSubmit_CRITs()
        {
            bool RESULT = true;

            try
            {
                DataSet dsCases = dal_DM.DM_OnSubmit_CRIT_SP(
                    ACTION: "GET_OnSubmit_CRIT_Cases",
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    VISITNUM: Request.QueryString["VISITID"].ToString()
                    );

                foreach (DataRow drCases in dsCases.Tables[0].Rows)
                {
                    if (RESULT)
                    {
                        DataTable dtCurrentDATA = GET_GRID_DATATABLE();

                        DataTable dtCaseDATA = new DataTable();
                        dtCaseDATA.Columns.Add("VARIABLENAME");
                        dtCaseDATA.Columns.Add("DATA");

                        DataSet dsVarData = new DataSet();

                        string CASES = drCases["CritCode"].ToString();

                        if (CASES.Contains("[") && CASES.Contains("]"))
                        {
                            DataSet dsVariables = dal_DM.DM_OnSubmit_CRIT_SP(
                            ACTION: "GET_VARIABLE_OnSubmit_CRIT_Current",
                            ID: drCases["ID"].ToString(),
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            VISITNUM: Request.QueryString["VISITID"].ToString()
                            );

                            foreach (DataRow drVariables in dsVariables.Tables[0].Rows)
                            {
                                DataRow[] rows = dtCurrentDATA.Select(" VARIABLENAME = '" + drVariables["ColumnName"].ToString() + "' ");

                                if (CASES.Contains("[" + drVariables["VariableName"].ToString() + "]"))
                                {
                                    CASES = CASES.Replace("[" + drVariables["VariableName"].ToString() + "]", CheckDatatype(rows[0]["DATA"].ToString()));
                                }

                                dtCaseDATA.Rows.Add(drVariables["VariableName"].ToString(), rows[0]["DATA"].ToString());
                            }
                        }

                        if (CASES.Contains("[") && CASES.Contains("]"))
                        {
                            DataSet dsVariables = dal_DM.DM_OnSubmit_CRIT_SP(
                            ACTION: "GET_VARIABLE_OnSubmit_CRIT",
                            ID: drCases["ID"].ToString(),
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            VISITNUM: Request.QueryString["VISITID"].ToString()
                            );

                            foreach (DataRow drVariables in dsVariables.Tables[0].Rows)
                            {
                                if (drVariables["Derived"].ToString() != "True")
                                {
                                    string DATA = "";

                                    string VariableCONDITION = drVariables["Condition"].ToString();

                                    if (VariableCONDITION != "")
                                    {
                                        foreach (DataRow drData in dtCurrentDATA.Rows)
                                        {
                                            if (VariableCONDITION.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                            {
                                                VariableCONDITION = VariableCONDITION.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                            }
                                        }
                                    }

                                    dsVarData = dal_DM.DM_OnSubmit_CRIT_SP(
                                        ACTION: "GET_DATA_AGAINST_VARIABLE",
                                        VARIABLENAME: drVariables["ColumnName"].ToString(),
                                        TABLENAME: drVariables["TableName"].ToString(),
                                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                                        VISITNUM: drVariables["Visit_ID"].ToString(),
                                        MODULEID: drVariables["Module_ID"].ToString(),
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        Condition: VariableCONDITION
                                        );

                                    if (dsVarData.Tables[0].Rows.Count > 0)
                                    {
                                        DATA = dsVarData.Tables[0].Rows[0][0].ToString();
                                    }

                                    CASES = CASES.Replace("'[" + drVariables["VariableName"].ToString() + "]'", CheckDatatype(DATA));
                                    CASES = CASES.Replace("[" + drVariables["VariableName"].ToString() + "]", CheckDatatype(DATA));

                                    dtCaseDATA.Rows.Add(drVariables["VariableName"].ToString(), DATA);
                                }
                                else
                                {
                                    string DATA = "";
                                    string FORMULA = drVariables["Formula"].ToString();

                                    foreach (DataRow drData in dtCaseDATA.Rows)
                                    {
                                        if (FORMULA.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                        {
                                            FORMULA = FORMULA.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                        }
                                    }

                                    dsVarData = dal_DM.DM_OnSubmit_CRIT_SP(
                                        ACTION: "GET_DATA_DERIVED",
                                        Formula: FORMULA
                                        );

                                    if (dsVarData.Tables[0].Rows.Count > 0)
                                    {
                                        DATA = dsVarData.Tables[0].Rows[0][0].ToString();
                                    }

                                    CASES = CASES.Replace("'[" + drVariables["VariableName"].ToString() + "]'", CheckDatatype(DATA));
                                    CASES = CASES.Replace("[" + drVariables["VariableName"].ToString() + "]", CheckDatatype(DATA));

                                    dtCaseDATA.Rows.Add(drVariables["VariableName"].ToString(), DATA);
                                }
                            }
                        }

                        //CASES = CASES.Replace("''", "'");

                        CASES = CASES.Replace("'''", "''");

                        CASES = CASES.Replace("'''", "''");

                        CASES = CASES.Replace("[", "");

                        CASES = CASES.Replace("]", "");

                        DataSet dsRESULT = dal_DM.DM_OnSubmit_CRIT_SP(ACTION: "GET_OnSubmit_CRIT_Result",
                            Condition: CASES,
                            CritName: "CritName"
                            );

                        if (dsRESULT.Tables.Count > 0 && dsRESULT.Tables[0].Rows.Count > 0)
                        {
                            if (dsRESULT.Tables[0].Rows[0][0].ToString() == "CritName")
                            {
                                hdnError_Msg.Value = drCases["CritName"].ToString();
                                hdnAllowable.Value = drCases["ALLOWABLE"].ToString();
                                //Response.Write("<script> alert('" + dsRESULT.Tables[0].Rows[0][0].ToString() + "');</script>");

                                RESULT = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }

            return RESULT;
        }

        private DataTable GET_GRID_DATATABLE()
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("VARIABLENAME");
            outputTable.Columns.Add("DATA");
            outputTable.Columns.Add("CONTROLTYPE");
            outputTable.Columns.Add("GRIDNAME");
            outputTable.Columns.Add("CLIENTID");
            outputTable.Columns.Add("ROWNUM");

            string varname;

            for (int rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
            {
                string strdata = "";
                varname = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                string CONTROLTYPE;
                CONTROLTYPE = ((Label)grd_Data.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;

                if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                {
                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        strdata = ((TextBox)grd_Data.Rows[rownum].FindControl("TXT_FIELD")).Text;
                    }
                    else if (CONTROLTYPE == "DROPDOWN")
                    {
                        if (((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedIndex == -1)
                        {
                            strdata = ((HiddenField)grd_Data.Rows[rownum].FindControl("HDN_FIELD")).Value;
                        }
                        else if (((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                        {
                            strdata = ((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedValue;
                        }
                        else
                        {
                            strdata = "";
                        }
                    }
                    else if (CONTROLTYPE == "CHECKBOX")
                    {
                        Repeater repeat_CHK = grd_Data.Rows[rownum].FindControl("repeat_CHK") as Repeater;
                        for (int a = 0; a < repeat_CHK.Items.Count; a++)
                        {
                            if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                            {
                                if (strdata.ToString() == "")
                                {
                                    strdata = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                }
                                else
                                {
                                    strdata += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                }
                            }
                        }
                    }
                    else if (CONTROLTYPE == "RADIOBUTTON")
                    {
                        Repeater repeat_RAD = grd_Data.Rows[rownum].FindControl("repeat_RAD") as Repeater;
                        for (int a = 0; a < repeat_RAD.Items.Count; a++)
                        {
                            if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                            {
                                strdata = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                            }
                        }
                    }
                }

                outputTable.Rows.Add(varname, strdata, CONTROLTYPE, "grd_Data", grd_Data.ClientID, rownum);

                GridView grd_Data1 = grd_Data.Rows[rownum].FindControl("grd_Data1") as GridView;

                for (int b = 0; b < grd_Data1.Rows.Count; b++)
                {
                    string strdata1 = "";

                    varname = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                    CONTROLTYPE = ((Label)grd_Data1.Rows[b].FindControl("lblCONTROLTYPE")).Text;

                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                    {
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            strdata1 = ((TextBox)grd_Data1.Rows[b].FindControl("TXT_FIELD")).Text;
                        }
                        else if (CONTROLTYPE == "DROPDOWN")
                        {
                            if (((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedIndex == -1)
                            {
                                strdata1 = ((HiddenField)grd_Data1.Rows[b].FindControl("HDN_FIELD")).Value;
                            }
                            else if (((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                            {
                                strdata1 = ((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedValue;
                            }
                            else
                            {
                                strdata1 = "";
                            }
                        }
                        else if (CONTROLTYPE == "CHECKBOX")
                        {
                            Repeater repeat_CHK = grd_Data1.Rows[b].FindControl("repeat_CHK") as Repeater;
                            for (int a = 0; a < repeat_CHK.Items.Count; a++)
                            {
                                if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                {
                                    if (strdata1.ToString() == "")
                                    {
                                        strdata1 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                    }
                                    else
                                    {
                                        strdata1 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                    }
                                }
                            }
                        }
                        else if (CONTROLTYPE == "RADIOBUTTON")
                        {
                            Repeater repeat_RAD = grd_Data1.Rows[b].FindControl("repeat_RAD") as Repeater;
                            for (int a = 0; a < repeat_RAD.Items.Count; a++)
                            {
                                if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                {
                                    strdata1 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                }
                            }
                        }
                    }

                    outputTable.Rows.Add(varname, strdata1, CONTROLTYPE, "grd_Data1", grd_Data1.ClientID, b);

                    GridView grd_Data2 = grd_Data1.Rows[b].FindControl("grd_Data2") as GridView;

                    for (int c = 0; c < grd_Data2.Rows.Count; c++)
                    {
                        string strdata2 = "";
                        varname = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                        CONTROLTYPE = ((Label)grd_Data2.Rows[c].FindControl("lblCONTROLTYPE")).Text;

                        if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                        {
                            if (CONTROLTYPE == "TEXTBOX")
                            {
                                strdata2 = ((TextBox)grd_Data2.Rows[c].FindControl("TXT_FIELD")).Text;
                            }
                            else if (CONTROLTYPE == "DROPDOWN")
                            {
                                if (((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedIndex == -1)
                                {
                                    strdata2 = ((HiddenField)grd_Data2.Rows[c].FindControl("HDN_FIELD")).Value;
                                }
                                else if (((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                {
                                    strdata2 = ((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedValue;
                                }
                                else
                                {
                                    strdata2 = "";
                                }
                            }
                            else if (CONTROLTYPE == "CHECKBOX")
                            {
                                Repeater repeat_CHK = grd_Data2.Rows[c].FindControl("repeat_CHK") as Repeater;
                                for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                {
                                    if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                    {
                                        if (strdata2.ToString() == "")
                                        {
                                            strdata2 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                        }
                                        else
                                        {
                                            strdata2 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                        }
                                    }
                                }
                            }
                            else if (CONTROLTYPE == "RADIOBUTTON")
                            {
                                Repeater repeat_RAD = grd_Data2.Rows[c].FindControl("repeat_RAD") as Repeater;
                                for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                {
                                    if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                    {
                                        strdata2 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                    }
                                }
                            }
                        }

                        outputTable.Rows.Add(varname, strdata2, CONTROLTYPE, "grd_Data2", grd_Data2.ClientID, c);

                        GridView grd_Data3 = grd_Data2.Rows[c].FindControl("grd_Data3") as GridView;

                        for (int d = 0; d < grd_Data3.Rows.Count; d++)
                        {
                            string strdata3 = "";
                            varname = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                            CONTROLTYPE = ((Label)grd_Data3.Rows[d].FindControl("lblCONTROLTYPE")).Text;

                            if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                            {
                                if (CONTROLTYPE == "TEXTBOX")
                                {
                                    strdata3 = ((TextBox)grd_Data3.Rows[d].FindControl("TXT_FIELD")).Text;
                                }
                                else if (CONTROLTYPE == "DROPDOWN")
                                {
                                    if (((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedIndex == -1)
                                    {
                                        strdata3 = ((HiddenField)grd_Data3.Rows[d].FindControl("HDN_FIELD")).Value;
                                    }
                                    else if (((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                    {
                                        strdata3 = ((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedValue;
                                    }
                                    else
                                    {
                                        strdata3 = "";
                                    }
                                }
                                else if (CONTROLTYPE == "CHECKBOX")
                                {
                                    Repeater repeat_CHK = grd_Data3.Rows[d].FindControl("repeat_CHK") as Repeater;
                                    for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                    {
                                        if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                        {
                                            if (strdata3.ToString() == "")
                                            {
                                                strdata3 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                            }
                                            else
                                            {
                                                strdata3 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                            }
                                        }
                                    }
                                }
                                else if (CONTROLTYPE == "RADIOBUTTON")
                                {
                                    Repeater repeat_RAD = grd_Data3.Rows[d].FindControl("repeat_RAD") as Repeater;
                                    for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                    {
                                        if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                        {
                                            strdata3 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                        }
                                    }
                                }
                            }

                            outputTable.Rows.Add(varname, strdata3, CONTROLTYPE, "grd_Data3", grd_Data3.ClientID, d);

                            strdata3 = "";
                        }
                        strdata2 = "";
                    }
                    strdata1 = "";
                }
                strdata = "";
            }

            return outputTable;
        }

        private string CheckDatatype(string Val)
        {
            string RESULT = "";
            try
            {
                CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();

                int i = 0;
                float j = 0;
                double k = 0;
                DateTime l;

                if (Val.Contains("dd/"))
                {
                    Val = Val.Replace("dd/", "01/");
                }

                if (Val.Contains("mm/"))
                {
                    Val = Val.Replace("mm/", "01/");
                }

                if (int.TryParse(Val, out i))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (float.TryParse(Val, out j))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (double.TryParse(Val, out k))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (DateTime.TryParse(Val, out l) || comm.isDate(Val))
                {
                    RESULT = "dbo.CastDate('" + Val + "')";
                }
                else
                {
                    RESULT = "N'" + Val + "'";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }

            return RESULT;
        }

        public void GetOnPageSpecs()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_DM.DM_Specs_Module_Wise_SP
                    (
                    ACTION: "Get_specs_Module_Wise",
                    MODULEID: Request.QueryString["MODULEID"].ToString()
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdOnPageSpecs.DataSource = ds;
                    grdOnPageSpecs.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void GetSign_info()
        {
            try
            {
                DataSet ds = dal_DM.DM_SIGNOFF_SP(ACTION: "GET_SIGNOFF_DETAILS",
                    PVID: lblPVID.Text,
                    RECID: hdnRECID.Value
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gridsigninfo.DataSource = ds.Tables[0];
                    gridsigninfo.DataBind();
                }
                else
                {
                    gridsigninfo.DataSource = null;
                    gridsigninfo.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gridsigninfo_PreRender(object sender, EventArgs e)
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

        protected void ADD_NEW_ROW_DATA(string FieldName, string VariableName, string OldValue, string NewValue, string Reason, string Comment, string RECID)
        {
            try
            {
                CREATE_DM_AUDITTRAIL_DT();

                DataRow myDataRow;
                myDataRow = dt_AuditTrail.NewRow();
                myDataRow["SOURCE"] = "eCRF";
                myDataRow["PVID"] = hdnPVID.Value;
                myDataRow["RECID"] = RECID;
                myDataRow["SUBJID"] = Request.QueryString["SUBJID"].ToString();
                myDataRow["VISITNUM"] = hdnVISITID.Value;
                myDataRow["MODULENAME"] = lblModuleName.Text;
                myDataRow["FIELDNAME"] = FieldName.Trim();
                myDataRow["TABLENAME"] = hfTablename.Value;
                myDataRow["VARIABLENAME"] = VariableName.Trim();
                myDataRow["REASON"] = Reason;
                myDataRow["COMMENTS"] = Comment;
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                myDataRow["OLDVALUE"] = REMOVEHTML(OldValue).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["NEWVALUE"] = REMOVEHTML(NewValue).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["DM"] = true;
                dt_AuditTrail.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void CREATE_DM_AUDITTRAIL_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (dt_AuditTrail.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    dt_AuditTrail.Columns.Add("SOURCE");
                    dt_AuditTrail.Columns.Add("PVID");
                    dt_AuditTrail.Columns.Add("RECID");
                    dt_AuditTrail.Columns.Add("SUBJID");
                    dt_AuditTrail.Columns.Add("VISITNUM");
                    dt_AuditTrail.Columns.Add("MODULENAME");
                    dt_AuditTrail.Columns.Add("FIELDNAME");
                    dt_AuditTrail.Columns.Add("TABLENAME");
                    dt_AuditTrail.Columns.Add("VARIABLENAME");
                    dt_AuditTrail.Columns.Add("OLDVALUE");
                    dt_AuditTrail.Columns.Add("NEWVALUE");
                    dt_AuditTrail.Columns.Add("REASON");
                    dt_AuditTrail.Columns.Add("COMMENTS");
                    dt_AuditTrail.Columns.Add("ENTEREDBY");
                    dt_AuditTrail.Columns.Add("ENTEREDBYNAME");
                    dt_AuditTrail.Columns.Add("ENTEREDDAT");
                    dt_AuditTrail.Columns.Add("ENTERED_TZVAL");
                    dt_AuditTrail.Columns.Add("DM");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void UPDATE_SAE_SYNC()
        {
            try
            {
                dal_DM.DM_SAE_SYNC_SP(
                ACTION: "UPDATE_SAE_SYNC",
                PVID: hdnPVID.Value,
                RECID: hdnRECID.Value,
                MODULEID: Request.QueryString["MODULEID"].ToString(),
                SUBJID: Request.QueryString["SUBJID"].ToString()
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void DATA_Changed(object sender, EventArgs e)
        {
            try
            {
                dtCurrentDATA = GET_GRID_DATATABLE();

                string VARIABLENAME = "";

                GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

                VARIABLENAME = (row.FindControl("lblVARIABLENAME") as Label).Text;

                if (CHECK_SET_VALUE("DATACHANGE", VARIABLENAME))
                {
                    if (hdn_PAGESTATUS.Value == "1")
                    {
                        CHECK_VISIBILITY_CRTITERIAS(row, sender, e);
                    }
                }

                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Change", "callChange();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void btnRightClick(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

                txt_TableName.Text = hfTablename.Value;
                txt_VariableName.Text = (row.FindControl("lblVARIABLENAME") as Label).Text;
                txt_ModuleName.Text = lblModuleName.Text;
                txt_FieldName.Text = (row.FindControl("lblFieldName") as Label).Text;
                txt_OldValue.Text = (row.FindControl("HDN_OLD_VALUE") as HiddenField).Value;

                string CONTROLTYPE = (row.FindControl("lblCONTROLTYPE") as Label).Text;

                txt_NewValue.Text = "";

                switch (CONTROLTYPE)
                {
                    case "TEXTBOX":
                        (row.FindControl("TXT_FIELD") as TextBox).Text = "";
                        break;
                    case "DROPDOWN":
                        (row.FindControl("DRP_FIELD") as DropDownList).SelectedIndex = 0;
                        (row.FindControl("HDN_FIELD") as HiddenField).Value = "";
                        break;
                    case "RADIOBUTTON":
                        Repeater repeaterRAD = (row.FindControl("repeat_RAD") as Repeater);
                        foreach (RepeaterItem repeaterItem in repeaterRAD.Items)
                        {
                            if ((repeaterItem.FindControl("RAD_FIELD") as RadioButton).Checked)
                            {
                                (repeaterItem.FindControl("RAD_FIELD") as RadioButton).Checked = false;
                            }
                        }
                        break;
                    case "CHECKBOX":
                        Repeater repeater = (row.FindControl("repeat_CHK") as Repeater);
                        foreach (RepeaterItem repeaterItem in repeater.Items)
                        {
                            if ((repeaterItem.FindControl("CHK_FIELD") as CheckBox).Checked)
                            {
                                (repeaterItem.FindControl("CHK_FIELD") as CheckBox).Checked = false;
                            }
                        }
                        break;
                }

                UpdatePanel upnlBtn = (row.FindControl("upnlBtn") as UpdatePanel);

                UpdatePanel upnlBtn_refresh = (row.FindControl("upnlBtn_refresh") as UpdatePanel);

                if (hdnIsComplete.Value == "1")
                {
                    HiddenField hdnQuery_OverrideId = (HiddenField)Page.Master.FindControl("hdnQuery_OverrideId");
                    HiddenField hdnQueryVariableName = (HiddenField)Page.Master.FindControl("hdnQueryVariableName");

                    if (hdnQuery_OverrideId.Value != "" && hdnQueryVariableName.Value == txt_VariableName.Text)
                    {
                        DivAction.Visible = true;
                    }
                    else
                    {
                        DivAction.Visible = false;
                    }

                    upnlBtn.Update();
                    upnlBtn_refresh.Update();
                    updPnlIDDetail.Update();
                    ModalPopupExtender1.Show();
                }
                else if (hdnIsComplete.Value == "0")
                {
                    drp_Reason.SelectedItem.Text = "Data Saved Incomplete";
                    btn_Update_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHECK_OnSubmit_CRITs())
                {
                    Session["RedirceToAnotherPage"] = "1";

                    ADD_NEW_ROW_DATA(txt_FieldName.Text,
                        txt_VariableName.Text,
                        txt_OldValue.Text,
                        txt_NewValue.Text,
                        drp_Reason.SelectedItem.Text,
                        txt_Comments.Text,
                        hdnRECID.Value
                        );

                    drp_Reason.SelectedIndex = 0;
                    txt_Comments.Text = "";

                    if (hdnIsComplete.Value == "1")
                    {
                        INSERT_FORM_DATA();

                        Run_Rules_FIELD_WISE(Request.QueryString["SUBJID"].ToString(),
                            Request.QueryString["VISITID"].ToString(),
                            Request.QueryString["MODULEID"].ToString(),
                            txt_VariableName.Text
                            );
                    }
                    else if (hdnIsComplete.Value == "0")
                    {
                        INSERT_FORM_DATA_Incomplete();
                    }

                    HiddenField hdnQuery_OverrideId = (HiddenField)Page.Master.FindControl("hdnQuery_OverrideId");
                    HiddenField hdnQueryVariableName = (HiddenField)Page.Master.FindControl("hdnQueryVariableName");

                    if (hdnQuery_OverrideId.Value != "" && hdnQueryVariableName.Value == txt_VariableName.Text)
                    {
                        string Comments = "";
                        if (drpAction.SelectedValue == "Other")
                        {
                            Comments = drpAction.SelectedValue + ": " + txt_OverrideComm.Text;
                        }
                        else
                        {
                            Comments = drpAction.SelectedValue;
                        }

                        dal_DM.DM_QUERY_SP(ACTION: "Update_Comment_Status",
                            ID: hdnQuery_OverrideId.Value,
                            Comment: Comments
                            );
                    }

                    hdnQuery_OverrideId.Value = "";
                    hdnQueryVariableName.Value = "";

                    CHECK_OnSubmit_EMAIL_CRITs();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Reason for change added successfully.');window.location.href = '" + Request.Url.ToString() + "';", true);
                }
                else
                {
                    if (hdnAllowable.Value == "True")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmMsg();", true);
                    }
                    else
                    {
                        // Convert the value to Base64
                        string base64Msg = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(hdnError_Msg.Value));

                        // Display the Base64-encoded message safely in JavaScript
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                            $"var encodedMsg = '{base64Msg}'; alert(atob(encodedMsg));", true);

                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + hdnError_Msg.Value + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Run_Rules_FIELD_WISE(string SUBJID, string VISITID, string MODULEID, string VARIABLENAME)
        {
            try
            {
                DataSet dsDATA = dal_DM.DM_RUN_RULE(Action: "GET_DATA_RUN_VARIABLE",
                    SUBJID: SUBJID,
                    VISIT: VISITID,
                    Module_ID: MODULEID,
                    VARIABLENAME: VARIABLENAME
                    );

                if (dsDATA.Tables.Count > 0 && dsDATA.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drDATA in dsDATA.Tables[0].Rows)
                    {
                        string Para_SUBJID = SUBJID;
                        string Para_Visit_ID = VISITID;
                        string Para_ModuleId = MODULEID;

                        string Para_VariableName = drDATA["VARIABLENAME"].ToString();
                        string Para_PVID = drDATA["PVID"].ToString();
                        string Para_RECID = drDATA["RECID"].ToString();
                        string Para_UserID = drDATA["User"].ToString();

                        Check_Rules(Para_SUBJID, Para_Visit_ID, Para_VariableName, Para_ModuleId, Para_PVID, Para_RECID, Para_UserID);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Check_Rules(string Para_SUBJID, string Para_Visit_ID, string Para_VariableName, string Para_ModuleId, string Para_PVID, string Para_RECID, string Para_UserID)
        {
            try
            {
                string UNVISITNUM = "";
                if (drpVisit.SelectedValue.Contains("."))
                {
                    UNVISITNUM = drpVisit.SelectedValue.ToString().Substring(0, drpVisit.SelectedValue.ToString().IndexOf('.'));
                }

                DataSet dsVarData = new DataSet();

                string strdata = "", CONDITION = "";

                DataSet dsRule = dal_DM.DM_RUN_RULE(Action: "CHECK_RULE_AGAINST_VARIABLE_DM",
                    VISITNUM: Para_Visit_ID,
                    Module_ID: Para_ModuleId,
                    Para_VariableName: Para_VariableName
                    );

                foreach (DataRow drRule in dsRule.Tables[0].Rows)
                {
                    string OtherPVIDS = "";
                    string MainPVID = "", MainRECID = "0", MainVISITNUM = "", MainVISIT = "";
                    string DATA = "";

                    if (drRule["GEN_QUERY"].ToString() == "True" || drRule["SET_VALUE"].ToString() == "True")
                    {
                        try
                        {
                            DataTable table = new DataTable();
                            table.Columns.Add("VARIABLENAME", typeof(string));
                            table.Columns.Add("DATA", typeof(string));

                            bool RESULTS = false, isFAIL = false;

                            CONDITION = drRule["Condition"].ToString();

                            DataSet dsVariables = dal_DM.DM_RUN_RULE(Action: "GET_Rule_Variables_FOR_DM", RULE_ID: drRule["ID"].ToString());

                            string MainColumnName = drRule["VARIABLENAME"].ToString();
                            string MainVisit = drRule["VISITNUM"].ToString();

                            foreach (DataRow drVariable in dsVariables.Tables[1].Rows)
                            {
                                string VariableName = drVariable["VARIABLENAME_DEF"].ToString();
                                string CONTROLTYPE = drVariable["CONTROLTYPE"].ToString();
                                string Derived = drVariable["Derived"].ToString();
                                string VariableCONDITION = drVariable["Condition"].ToString();

                                if (Derived != "True")
                                {
                                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                                    {
                                        if (VariableCONDITION != "")
                                        {
                                            foreach (DataRow drData in table.Rows)
                                            {
                                                if (VariableCONDITION.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                                {
                                                    VariableCONDITION = VariableCONDITION.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                                }
                                            }
                                        }

                                        if (drVariable["VISITNUM"].ToString() == UNVISITNUM && Para_Visit_ID.Contains("."))
                                        {
                                            dsVarData = dal_DM.DM_RUN_RULE(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                TABLENAME: drVariable["TableName"].ToString(),
                                                SUBJID: Para_SUBJID,
                                                VISITNUM: Para_Visit_ID,
                                                Module_ID: Para_ModuleId,
                                                RECID: Para_RECID,
                                                PVID: Para_PVID,
                                                Condition: VariableCONDITION
                                                );
                                        }
                                        else
                                        {
                                            if (drVariable["VISITNUM"].ToString() == "000" && (Para_Visit_ID == drRule["VISITNUM"].ToString()) && (drVariable["MODULEID"].ToString() == drRule["MODULEID"].ToString()))
                                            {
                                                dsVarData = dal_DM.DM_RUN_RULE(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                TABLENAME: drVariable["TableName"].ToString(),
                                                SUBJID: Para_SUBJID,
                                                VISITNUM: "000",
                                                Module_ID: Para_ModuleId,
                                                RECID: Para_RECID,
                                                PVID: Para_PVID,
                                                Condition: VariableCONDITION
                                                );
                                            }
                                            else if (drVariable["VISITNUM"].ToString() == "000" && (drVariable["MODULEID"].ToString() == drRule["MODULEID"].ToString()))
                                            {
                                                dsVarData = dal_DM.DM_RUN_RULE(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                TABLENAME: drVariable["TableName"].ToString(),
                                                SUBJID: Para_SUBJID,
                                                VISITNUM: Para_Visit_ID,
                                                Module_ID: Para_ModuleId,
                                                RECID: Para_RECID,
                                                PVID: Para_PVID,
                                                Condition: VariableCONDITION
                                                );
                                            }
                                            else
                                            {
                                                dsVarData = dal_DM.DM_RUN_RULE(
                                                    Action: "GET_DATA_AGAINST_VARIABLE",
                                                    VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                    TABLENAME: drVariable["TableName"].ToString(),
                                                    SUBJID: Para_SUBJID,
                                                    VISITNUM: drVariable["VISITNUM"].ToString(),
                                                    Module_ID: Para_ModuleId,
                                                    RECID: Para_RECID,
                                                    PVID: Para_PVID,
                                                    Condition: VariableCONDITION
                                                    );
                                            }
                                        }

                                        if (dsVarData.Tables[0].Rows.Count > 0)
                                        {
                                            strdata = dsVarData.Tables[0].Rows[0][0].ToString();
                                            if (dsVarData.Tables[1].Rows.Count > 0)
                                            {
                                                OtherPVIDS += "," + dsVarData.Tables[1].Rows[0][0].ToString() + "(" + dsVarData.Tables[1].Rows[0][1].ToString() + ")";
                                            }
                                            else
                                            {
                                                OtherPVIDS += "," + Para_PVID + "(" + Para_RECID + ")";
                                            }
                                        }
                                        else
                                        {
                                            strdata = "";
                                            OtherPVIDS += "," + Para_PVID + "(" + Para_RECID + ")";
                                        }

                                        if ((MainColumnName == drVariable["VARIABLENAME"].ToString()) && ((MainVisit == drVariable["VISITNUM"].ToString() || (MainVisit.Contains(".") && (drVariable["VISITNUM"].ToString().Contains("."))))))
                                        {
                                            DATA = strdata;

                                            if (dsVarData.Tables[1].Rows.Count > 0)
                                            {
                                                MainPVID = dsVarData.Tables[1].Rows[0][0].ToString();
                                                MainRECID = dsVarData.Tables[1].Rows[0][1].ToString();
                                                MainVISITNUM = dsVarData.Tables[1].Rows[0][2].ToString();
                                                MainVISIT = DBNull.Value.ToString();
                                            }
                                            else
                                            {
                                                MainPVID = Para_PVID;
                                                MainRECID = Para_RECID;
                                                MainVISITNUM = Para_Visit_ID;
                                                MainVISIT = drpVisit.SelectedValue;
                                            }
                                        }

                                        table.Rows.Add(VariableName, strdata);

                                        if (CONDITION.Contains("[" + VariableName + "]"))
                                        {
                                            CONDITION = CONDITION.Replace("[" + VariableName + "]", CheckDatatype(strdata));
                                        }
                                    }
                                }
                                else
                                {
                                    string FORMULA = drVariable["Formula"].ToString();

                                    foreach (DataRow drData in table.Rows)
                                    {
                                        if (FORMULA.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                        {
                                            FORMULA = FORMULA.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                        }
                                    }

                                    dsVarData = dal_DM.DM_RUN_RULE(
                                        Action: "GET_DATA_DERIVED",
                                        FORMULA: FORMULA
                                        );

                                    if (dsVarData.Tables[0].Rows.Count > 0)
                                    {
                                        strdata = dsVarData.Tables[0].Rows[0][0].ToString();
                                    }
                                    else
                                    {
                                        strdata = "";
                                    }

                                    foreach (DataRow dr in table.Rows)
                                    {
                                        if (strdata.Contains("[" + dr["VARIABLENAME"] + "]"))
                                        {
                                            string CHKDATA = CheckDatatype(dr["DATA"].ToString());
                                            if (CHKDATA != "")
                                            {
                                                strdata = strdata.Replace("[" + dr["VARIABLENAME"] + "]", CHKDATA);
                                            }
                                        }
                                    }

                                    table.Rows.Add(VariableName, strdata);

                                    if (CONDITION.Contains("[" + VariableName + "]"))
                                    {
                                        CONDITION = CONDITION.Replace("[" + VariableName + "]", CheckDatatype(strdata));
                                    }
                                }

                            }

                            if (MainPVID == "")
                            {
                                MainPVID = Para_PVID;
                                MainRECID = Para_RECID;
                            }

                            DataSet dsResults = new DataSet();

                            if (CONDITION != "")
                            {
                                try
                                {

                                    //GET CONDITION TRUE OR FALSE
                                    SqlCommand cmd;
                                    SqlDataAdapter adp;

                                    SqlConnection con = new SqlConnection(dal_DM.getconstr());
                                    cmd = new SqlCommand("DM_RUN_RULE", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Action", "CHECKRULE_CONDITION");
                                    cmd.Parameters.AddWithValue("@Condition", CONDITION);

                                    con.Open();
                                    adp = new SqlDataAdapter(cmd);
                                    adp.Fill(dsResults);
                                    cmd.Dispose();
                                    con.Close();

                                    if (dsResults.Tables[0].Rows[0]["TESTED"].ToString() == "1")
                                    {
                                        RESULTS = true;
                                    }
                                    else
                                    {
                                        RESULTS = false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    isFAIL = true;
                                }
                            }
                            else
                            {
                                RESULTS = false;
                            }

                            if (!isFAIL)
                            {
                                if (RESULTS)
                                {
                                    if (drRule["GEN_QUERY"].ToString() == "True")
                                    {
                                        string QUERYTEXT = drRule["QueryText"].ToString();

                                        foreach (DataRow drData in table.Rows)
                                        {
                                            if (QUERYTEXT.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                            {
                                                QUERYTEXT = QUERYTEXT.Replace("[" + drData["VARIABLENAME"].ToString() + "]", drData["DATA"].ToString());
                                            }
                                        }

                                        Generate_Query
                                        (
                                        RULE_ID: drRule["RULEID"].ToString(),
                                        Nature: drRule["Nature"].ToString(),
                                        PVID: MainPVID,
                                        RECID: MainRECID,
                                        SUBJID: Para_SUBJID,
                                        Data: DATA,
                                        QUERYTEXT: QUERYTEXT,
                                        Module_ID: drRule["MODULEID"].ToString(),
                                        Field_ID: drRule["FIELDID"].ToString(),
                                        VARIABLENAME: drRule["VARIABLENAME"].ToString(),
                                        OtherPVIDS: OtherPVIDS,
                                        VISITNUM: MainVISITNUM,
                                        VISIT: MainVISIT
                                        );
                                    }

                                    if (drRule["SET_VALUE"].ToString() == "True")
                                    {
                                        CONDITION = "";

                                        foreach (DataRow drData in table.Rows)
                                        {
                                            if (CONDITION == "")
                                            {
                                                if (drRule["FORMULA_VALUE"].ToString().Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                                {
                                                    CONDITION = drRule["FORMULA_VALUE"].ToString().Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                                }
                                            }
                                            else
                                            {
                                                if (CONDITION.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                                {
                                                    CONDITION = CONDITION.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                                }
                                            }
                                        }

                                        DataSet DS_SETVALUE = dal_DM.DM_RUN_RULE(Action: "CHECKRULE_FORMULA_VALUE", FORMULA: CONDITION);

                                        DataSet dsSET_Value = dal_DM.DM_RUN_RULE(Action: "UPDATE_SET_VALUE",
                                            RULE_ID: drRule["RULEID"].ToString(),
                                            Value: DS_SETVALUE.Tables[0].Rows[0]["Data"].ToString(),
                                            SUBJID: drpSubID.SelectedValue,
                                            VISITNUM: Para_Visit_ID.ToString(),
                                            RECID: MainRECID,
                                            PVID: MainPVID
                                            );
                                    }
                                }
                                else
                                {
                                    Resolve_Query
                                        (
                                        RULE_ID: drRule["RULEID"].ToString(),
                                        SUBJID: Para_SUBJID,
                                        VARIABLENAME: drRule["VARIABLENAME"].ToString(),
                                        MainPVID: MainPVID,
                                        MainRECID: MainRECID,
                                        OtherPVIDS: OtherPVIDS
                                        );
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "window.location.href = '" + Request.Url.ToString() + "';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        public void GetQueryDetails()
        {
            try
            {
                DataSet ds = dal_DM.DM_QUERY_SP(ACTION: "GET_QUERY_DETAILS",
                    PVID: hdnPVID.Value,
                    RECID: hdnRECID.Value
                    );

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //divHideShow.Attributes.Remove("class");
                        //divHideShow.Attributes.Add("class", " disp-block");

                        grdQUERYDETAILS.DataSource = ds.Tables[0];
                        grdQUERYDETAILS.DataBind();
                    }

                    //Open Queries
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        grdOpenQuers.DataSource = ds.Tables[1];
                        grdOpenQuers.DataBind();
                    }

                    //Ans Queries
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        grdAnsQueries.DataSource = ds.Tables[2];
                        grdAnsQueries.DataBind();
                    }

                    //Closed Queries
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        grdcloseQueries.DataSource = ds.Tables[3];
                        grdcloseQueries.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        public void GetAuditDetails()
        {
            try
            {
                DataSet ds = dal_DM.DM_AUDITTRAIL_SP
                       (
                       ACTION: "GET_AUDITTRAIL_PVID_RECID",
                       PVID: hdnPVID.Value,
                       RECID: hdnRECID.Value
                       );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdAUDITTRAILDETAILS.DataSource = ds;
                    grdAUDITTRAILDETAILS.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void AutoCode()
        {
            try
            {
                dal_DM.CODING_SP(
                    ACTION: "AUTOCODE",
                    PVID: hdnPVID.Value,
                    RECID: hdnRECID.Value,
                    MODULEID: Request.QueryString["MODULEID"].ToString()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        public void GetCommentsetails()
        {
            try
            {
                DataSet ds = dal_DM.DM_COMMENT_SP(ACTION: "GET_COMMENTS_COUNT_PVID_RECID",
                   PVID: hdnPVID.Value,
                   RECID: hdnRECID.Value
                   );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdComments.DataSource = ds;
                    grdComments.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void Run_Rules()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string varname;

                for (int rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
                {
                    string strdata = "";
                    varname = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                    string CONTROLTYPE;
                    CONTROLTYPE = ((Label)grd_Data.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;

                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                    {
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            strdata = ((TextBox)grd_Data.Rows[rownum].FindControl("TXT_FIELD")).Text;
                        }
                        else if (CONTROLTYPE == "DROPDOWN")
                        {
                            strdata = ((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedValue;
                        }
                        else if (CONTROLTYPE == "CHECKBOX")
                        {
                            Repeater repeat_CHK = grd_Data.Rows[rownum].FindControl("repeat_CHK") as Repeater;
                            for (int a = 0; a < repeat_CHK.Items.Count; a++)
                            {
                                if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                {
                                    if (strdata.ToString() == "")
                                    {
                                        strdata = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                    }
                                    else
                                    {
                                        strdata += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                    }
                                }
                            }
                        }
                        else if (CONTROLTYPE == "RADIOBUTTON")
                        {
                            Repeater repeat_RAD = grd_Data.Rows[rownum].FindControl("repeat_RAD") as Repeater;
                            for (int a = 0; a < repeat_RAD.Items.Count; a++)
                            {
                                if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                {
                                    strdata = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                }
                            }
                        }

                        Check_Rules
                            (
                            SUBJID: Request.QueryString["SUBJID"].ToString(),
                            DATA: strdata,
                            Para_Visit_ID: Request.QueryString["VISITID"].ToString(),
                            Para_VariableName: varname,
                            Para_ModuleName: lblModuleName.Text,
                            TABLENAME: hfTablename.Value
                            );

                    }

                    GridView grd_Data1 = grd_Data.Rows[rownum].FindControl("grd_Data1") as GridView;

                    for (int b = 0; b < grd_Data1.Rows.Count; b++)
                    {
                        string Val_Child;
                        string strdata1 = "";
                        Val_Child = ((Label)grd_Data1.Rows[b].FindControl("lblVal_Child")).Text;
                        varname = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                        CONTROLTYPE = ((Label)grd_Data1.Rows[b].FindControl("lblCONTROLTYPE")).Text;

                        if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                        {
                            if (CONTROLTYPE == "TEXTBOX")
                            {
                                strdata1 = ((TextBox)grd_Data1.Rows[b].FindControl("TXT_FIELD")).Text;
                            }
                            else if (CONTROLTYPE == "DROPDOWN")
                            {
                                strdata1 = ((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedValue;
                            }
                            else if (CONTROLTYPE == "CHECKBOX")
                            {
                                Repeater repeat_CHK = grd_Data1.Rows[b].FindControl("repeat_CHK") as Repeater;
                                for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                {
                                    if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                    {
                                        if (strdata1.ToString() == "")
                                        {
                                            strdata1 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                        }
                                        else
                                        {
                                            strdata1 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                        }
                                    }
                                }
                            }
                            else if (CONTROLTYPE == "RADIOBUTTON")
                            {
                                Repeater repeat_RAD = grd_Data1.Rows[b].FindControl("repeat_RAD") as Repeater;
                                for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                {
                                    if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                    {
                                        strdata1 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                    }
                                }
                            }

                            Check_Rules
                            (
                            SUBJID: Request.QueryString["SUBJID"].ToString(),
                            DATA: strdata1,
                            Para_Visit_ID: Request.QueryString["VISITID"].ToString(),
                            Para_VariableName: varname,
                            Para_ModuleName: lblModuleName.Text,
                            TABLENAME: hfTablename.Value
                            );
                        }

                        GridView grd_Data2 = grd_Data1.Rows[b].FindControl("grd_Data2") as GridView;

                        for (int c = 0; c < grd_Data2.Rows.Count; c++)
                        {
                            string Val_Child1 = ((Label)grd_Data2.Rows[c].FindControl("lblVal_Child")).Text;
                            string strdata2 = "";
                            varname = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                            CONTROLTYPE = ((Label)grd_Data2.Rows[c].FindControl("lblCONTROLTYPE")).Text;

                            if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                            {
                                if (CONTROLTYPE == "TEXTBOX")
                                {
                                    strdata2 = ((TextBox)grd_Data2.Rows[c].FindControl("TXT_FIELD")).Text;
                                }
                                else if (CONTROLTYPE == "DROPDOWN")
                                {
                                    strdata2 = ((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedValue;
                                }
                                else if (CONTROLTYPE == "CHECKBOX")
                                {
                                    Repeater repeat_CHK = grd_Data2.Rows[c].FindControl("repeat_CHK") as Repeater;
                                    for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                    {
                                        if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                        {
                                            if (strdata2.ToString() == "")
                                            {
                                                strdata2 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                            }
                                            else
                                            {
                                                strdata2 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                            }
                                        }
                                    }
                                }
                                else if (CONTROLTYPE == "RADIOBUTTON")
                                {
                                    Repeater repeat_RAD = grd_Data2.Rows[c].FindControl("repeat_RAD") as Repeater;
                                    for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                    {
                                        if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                        {
                                            strdata2 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                        }
                                    }
                                }
                                Check_Rules
                                (
                                SUBJID: Request.QueryString["SUBJID"].ToString(),
                                DATA: strdata,
                                Para_Visit_ID: Request.QueryString["VISITID"].ToString(),
                                Para_VariableName: varname,
                                Para_ModuleName: lblModuleName.Text,
                                TABLENAME: hfTablename.Value
                                );
                            }

                            GridView grd_Data3 = grd_Data2.Rows[c].FindControl("grd_Data3") as GridView;

                            for (int d = 0; d < grd_Data3.Rows.Count; d++)
                            {
                                string Val_Child2 = ((Label)grd_Data3.Rows[d].FindControl("lblVal_Child")).Text;
                                string strdata3 = "";
                                varname = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                CONTROLTYPE = ((Label)grd_Data3.Rows[d].FindControl("lblCONTROLTYPE")).Text;

                                if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                                {
                                    if (CONTROLTYPE == "TEXTBOX")
                                    {
                                        strdata3 = ((TextBox)grd_Data3.Rows[d].FindControl("TXT_FIELD")).Text;
                                    }
                                    else if (CONTROLTYPE == "DROPDOWN")
                                    {
                                        strdata3 = ((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedValue;
                                    }
                                    else if (CONTROLTYPE == "CHECKBOX")
                                    {
                                        Repeater repeat_CHK = grd_Data3.Rows[d].FindControl("repeat_CHK") as Repeater;
                                        for (int a = 0; a < repeat_CHK.Items.Count; a++)
                                        {
                                            if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked == true)
                                            {
                                                if (strdata3.ToString() == "")
                                                {
                                                    strdata3 = ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                                }
                                                else
                                                {
                                                    strdata3 += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
                                                }
                                            }
                                        }
                                    }
                                    else if (CONTROLTYPE == "RADIOBUTTON")
                                    {
                                        Repeater repeat_RAD = grd_Data3.Rows[d].FindControl("repeat_RAD") as Repeater;
                                        for (int a = 0; a < repeat_RAD.Items.Count; a++)
                                        {
                                            if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked == true)
                                            {
                                                strdata3 = ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString();
                                            }
                                        }
                                    }

                                    Check_Rules
                                    (
                                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                                    DATA: strdata3,
                                    Para_Visit_ID: Request.QueryString["VISITID"].ToString(),
                                    Para_VariableName: varname,
                                    Para_ModuleName: lblModuleName.Text,
                                    TABLENAME: hfTablename.Value
                                    );

                                }
                                strdata3 = "";
                            }
                            strdata2 = "";
                        }
                        strdata1 = "";
                    }
                    strdata = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Check_Rules(string SUBJID, string DATA, string Para_Visit_ID, string Para_VariableName, string Para_ModuleName, string TABLENAME)
        {
            try
            {
                DataSet dsVarData = new DataSet();

                string strdata = "", CONDITION = "";

                string VISITID_FETCH_DATA = "";

                if (Request.QueryString["VISITID"].Contains("."))
                {
                    VISITID_FETCH_DATA = Request.QueryString["VISITID"].ToString().Substring(0, Request.QueryString["VISITID"].ToString().IndexOf('.'));
                }
                else
                {
                    VISITID_FETCH_DATA = Request.QueryString["VISITID"].ToString();
                }

                DataSet dsRule = dal_DM.DM_RUN_RULE(Action: "CHECK_RULE_AGAINST_VARIABLE_DM",
                    VISITNUM: VISITID_FETCH_DATA,
                    Module_ID: Request.QueryString["MODULEID"].ToString(),
                    Para_VariableName: Para_VariableName
                    );

                foreach (DataRow drRule in dsRule.Tables[0].Rows)
                {
                    string OtherPVIDS = "";
                    string MainPVID = "", MainRECID = "0", MainVISITNUM = "", MainVISIT = "";

                    if (drRule["GEN_QUERY"].ToString() == "True" || drRule["SET_VALUE"].ToString() == "True")
                    {
                        try
                        {
                            DataTable table = new DataTable();
                            table.Columns.Add("VARIABLENAME", typeof(string));
                            table.Columns.Add("DATA", typeof(string));

                            bool RESULTS = false, isFAIL = false;

                            CONDITION = drRule["Condition"].ToString();

                            DataSet dsVariables = dal_DM.DM_RUN_RULE(Action: "GET_Rule_Variables_FOR_DM", RULE_ID: drRule["ID"].ToString());

                            string MainColumnName = drRule["VARIABLENAME"].ToString();
                            string MainVisit = drRule["VISITNUM"].ToString();

                            foreach (DataRow drVariable in dsVariables.Tables[1].Rows)
                            {
                                string VariableName = drVariable["VARIABLENAME_DEF"].ToString();
                                string CONTROLTYPE = drVariable["CONTROLTYPE"].ToString();
                                string Derived = drVariable["Derived"].ToString();
                                string VariableCONDITION = drVariable["Condition"].ToString();

                                if (Derived != "True")
                                {
                                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                                    {
                                        if (VariableCONDITION != "")
                                        {
                                            foreach (DataRow drData in table.Rows)
                                            {
                                                if (VariableCONDITION.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                                {
                                                    VariableCONDITION = VariableCONDITION.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                                }
                                            }
                                        }

                                        if (drVariable["VISITNUM"].ToString() == VISITID_FETCH_DATA && Request.QueryString["VISITID"].Contains("."))
                                        {
                                            dsVarData = dal_DM.DM_RUN_RULE(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                TABLENAME: drVariable["TableName"].ToString(),
                                                SUBJID: SUBJID.ToString(),
                                                VISITNUM: Request.QueryString["VISITID"],
                                                Module_ID: Request.QueryString["MODULEID"].ToString(),
                                                RECID: hdnRECID.Value,
                                                PVID: lblPVID.Text,
                                                Condition: VariableCONDITION
                                                );
                                        }
                                        else
                                        {
                                            if (drVariable["VISITNUM"].ToString() == "000" && (Request.QueryString["VISITID"] == drRule["VISITNUM"].ToString()) && (drVariable["MODULEID"].ToString() == drRule["MODULEID"].ToString()))
                                            {
                                                dsVarData = dal_DM.DM_RUN_RULE(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                TABLENAME: drVariable["TableName"].ToString(),
                                                SUBJID: SUBJID.ToString(),
                                                VISITNUM: "000",
                                                Module_ID: Request.QueryString["MODULEID"].ToString(),
                                                RECID: hdnRECID.Value,
                                                PVID: lblPVID.Text,
                                                Condition: VariableCONDITION
                                                );
                                            }
                                            else if (drVariable["VISITNUM"].ToString() == "000" && (drVariable["MODULEID"].ToString() == drRule["MODULEID"].ToString()))
                                            {
                                                dsVarData = dal_DM.DM_RUN_RULE(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                TABLENAME: drVariable["TableName"].ToString(),
                                                SUBJID: SUBJID.ToString(),
                                                VISITNUM: Request.QueryString["VISITID"],
                                                Module_ID: Request.QueryString["MODULEID"].ToString(),
                                                RECID: hdnRECID.Value,
                                                PVID: lblPVID.Text,
                                                Condition: VariableCONDITION
                                                );
                                            }
                                            else
                                            {
                                                dsVarData = dal_DM.DM_RUN_RULE(
                                                    Action: "GET_DATA_AGAINST_VARIABLE",
                                                    VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                    TABLENAME: drVariable["TableName"].ToString(),
                                                    SUBJID: SUBJID.ToString(),
                                                    VISITNUM: drVariable["VISITNUM"].ToString(),
                                                    Module_ID: Request.QueryString["MODULEID"].ToString(),
                                                    RECID: hdnRECID.Value,
                                                    PVID: lblPVID.Text,
                                                    Condition: VariableCONDITION
                                                    );
                                            }
                                        }

                                        if (dsVarData.Tables[0].Rows.Count > 0)
                                        {
                                            strdata = dsVarData.Tables[0].Rows[0][0].ToString();
                                            if (dsVarData.Tables[1].Rows.Count > 0)
                                            {
                                                OtherPVIDS += "," + dsVarData.Tables[1].Rows[0][0].ToString() + "(" + dsVarData.Tables[1].Rows[0][1].ToString() + ")";
                                            }
                                            else
                                            {
                                                OtherPVIDS += "," + lblPVID.Text + "(" + hdnRECID.Value + ")";
                                            }
                                        }
                                        else
                                        {
                                            strdata = "";
                                            OtherPVIDS += "," + lblPVID.Text + "(" + hdnRECID.Value + ")";
                                        }

                                        if ((MainColumnName == drVariable["VARIABLENAME"].ToString()) && ((MainVisit == drVariable["VISITNUM"].ToString() || (MainVisit.Contains(".") && (drVariable["VISITNUM"].ToString().Contains("."))))))
                                        {
                                            DATA = strdata;

                                            if (dsVarData.Tables[1].Rows.Count > 0)
                                            {
                                                MainPVID = dsVarData.Tables[1].Rows[0][0].ToString();
                                                MainRECID = dsVarData.Tables[1].Rows[0][1].ToString();
                                                MainVISITNUM = dsVarData.Tables[1].Rows[0][2].ToString();
                                                MainVISIT = DBNull.Value.ToString();
                                            }
                                            else
                                            {
                                                MainPVID = lblPVID.Text;
                                                MainRECID = hdnRECID.Value;
                                                MainVISITNUM = hdnVISITID.Value;
                                                MainVISIT = Request.QueryString["VISIT"].ToString();
                                            }
                                        }

                                        table.Rows.Add(VariableName, strdata);

                                        if (CONDITION.Contains("[" + VariableName + "]"))
                                        {
                                            CONDITION = CONDITION.Replace("[" + VariableName + "]", CheckDatatype(strdata));
                                        }
                                    }
                                }
                                else
                                {
                                    string FORMULA = drVariable["Formula"].ToString();

                                    foreach (DataRow drData in table.Rows)
                                    {
                                        if (FORMULA.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                        {
                                            FORMULA = FORMULA.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                        }
                                    }

                                    dsVarData = dal_DM.DM_RUN_RULE(
                                        Action: "GET_DATA_DERIVED",
                                        FORMULA: FORMULA
                                        );

                                    if (dsVarData.Tables[0].Rows.Count > 0)
                                    {
                                        strdata = dsVarData.Tables[0].Rows[0][0].ToString();
                                    }
                                    else
                                    {
                                        strdata = "";
                                    }

                                    foreach (DataRow dr in table.Rows)
                                    {
                                        if (strdata.Contains("[" + dr["VARIABLENAME"] + "]"))
                                        {
                                            string CHKDATA = CheckDatatype(dr["DATA"].ToString());
                                            if (CHKDATA != "")
                                            {
                                                strdata = strdata.Replace("[" + dr["VARIABLENAME"] + "]", CHKDATA);
                                            }
                                        }
                                    }

                                    table.Rows.Add(VariableName, strdata);

                                    if (CONDITION.Contains("[" + VariableName + "]"))
                                    {
                                        CONDITION = CONDITION.Replace("[" + VariableName + "]", CheckDatatype(strdata));
                                    }
                                }

                            }

                            if (MainPVID == "")
                            {
                                MainPVID = lblPVID.Text;
                                MainRECID = hdnRECID.Value;
                            }

                            DataSet dsResults = new DataSet();

                            if (CONDITION != "")
                            {
                                try
                                {

                                    //GET CONDITION TRUE OR FALSE
                                    SqlCommand cmd;
                                    SqlDataAdapter adp;

                                    SqlConnection con = new SqlConnection(dal_DM.getconstr());
                                    cmd = new SqlCommand("DM_RUN_RULE", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Action", "CHECKRULE_CONDITION");
                                    cmd.Parameters.AddWithValue("@Condition", CONDITION);

                                    con.Open();
                                    adp = new SqlDataAdapter(cmd);
                                    adp.Fill(dsResults);
                                    cmd.Dispose();
                                    con.Close();

                                    if (dsResults.Tables[0].Rows[0]["TESTED"].ToString() == "1")
                                    {
                                        RESULTS = true;
                                    }
                                    else
                                    {
                                        RESULTS = false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    isFAIL = true;
                                }
                            }
                            else
                            {
                                RESULTS = false;
                            }

                            if (!isFAIL)
                            {
                                if (drRule["SET_VALUE"].ToString() == "True")
                                {
                                    CONDITION = "";

                                    foreach (DataRow drData in table.Rows)
                                    {
                                        if (CONDITION == "")
                                        {
                                            if (drRule["FORMULA_VALUE"].ToString().Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                            {
                                                CONDITION = drRule["FORMULA_VALUE"].ToString().Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (CONDITION.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                            {
                                                CONDITION = CONDITION.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                            }
                                        }
                                    }

                                    DataSet DS_SETVALUE = dal_DM.DM_RUN_RULE(Action: "CHECKRULE_FORMULA_VALUE", FORMULA: CONDITION);

                                    DataSet dsSET_Value = dal_DM.DM_RUN_RULE(Action: "UPDATE_SET_VALUE",
                                        RULE_ID: drRule["RULEID"].ToString(),
                                        Value: DS_SETVALUE.Tables[0].Rows[0]["Data"].ToString(),
                                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                                        VISITNUM: Request.QueryString["VISITID"].ToString(),
                                        RECID: MainRECID,
                                        PVID: MainPVID
                                        );
                                }
                                else
                                {
                                    if (RESULTS)
                                    {
                                        if (drRule["GEN_QUERY"].ToString() == "True")
                                        {
                                            string QUERYTEXT = drRule["QueryText"].ToString();

                                            foreach (DataRow drData in table.Rows)
                                            {
                                                if (QUERYTEXT.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                                {
                                                    QUERYTEXT = QUERYTEXT.Replace("[" + drData["VARIABLENAME"].ToString() + "]", drData["DATA"].ToString());
                                                }
                                            }

                                            Generate_Query
                                            (
                                            RULE_ID: drRule["RULEID"].ToString(),
                                            Nature: drRule["Nature"].ToString(),
                                            PVID: MainPVID,
                                            RECID: MainRECID,
                                            SUBJID: SUBJID,
                                            Data: DATA,
                                            QUERYTEXT: QUERYTEXT,
                                            Module_ID: drRule["MODULEID"].ToString(),
                                            Field_ID: drRule["FIELDID"].ToString(),
                                            VARIABLENAME: drRule["VARIABLENAME"].ToString(),
                                            OtherPVIDS: OtherPVIDS,
                                            VISITNUM: MainVISITNUM,
                                            VISIT: MainVISIT
                                            );
                                        }
                                    }
                                    else
                                    {
                                        Resolve_Query
                                            (
                                            RULE_ID: drRule["RULEID"].ToString(),
                                            SUBJID: SUBJID,
                                            VARIABLENAME: drRule["VARIABLENAME"].ToString(),
                                            MainPVID: MainPVID,
                                            MainRECID: MainRECID,
                                            OtherPVIDS: OtherPVIDS
                                            );
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Generate_Query(string RULE_ID, string Nature, string PVID, string RECID, string SUBJID, string Data, string QUERYTEXT, string Module_ID,
        string Field_ID, string VARIABLENAME, string OtherPVIDS, string VISITNUM, string VISIT)
        {
            try
            {
                dal_DM.DM_RUN_RULE(Action: "Generate_Query",
                    RULE_ID: RULE_ID,
                    Nature: Nature,
                    PVID: PVID,
                    RECID: RECID,
                    SUBJID: SUBJID,
                    Data: Data,
                    QUERYTEXT: QUERYTEXT,
                    Module_ID: Module_ID,
                    Field_ID: Field_ID,
                    VARIABLENAME: VARIABLENAME,
                    OtherPVIDS: OtherPVIDS,
                    VISIT: VISIT,
                    VISITNUM: VISITNUM,
                    INVID: Request.QueryString["INVID"].ToString()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Resolve_Query(string RULE_ID, string SUBJID, string VARIABLENAME, string MainPVID, string MainRECID, string OtherPVIDS)
        {
            try
            {
                dal_DM.DM_RUN_RULE(Action: "Resolve_Query",
                RULE_ID: RULE_ID,
                SUBJID: SUBJID,
                VARIABLENAME: VARIABLENAME,
                PVID: MainPVID,
                RECID: MainRECID,
                OtherPVIDS: OtherPVIDS
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnDELETESubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dal_DM.DM_DELETE_RECORDS_SP(
                    PVID: hdnPVID.Value,
                    RECID: hdnRECID.Value,
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    VISITNUM: Request.QueryString["VISITID"].ToString(),
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    INVID: Request.QueryString["INVID"].ToString(),
                    VISIT: Request.QueryString["VISIT"].ToString(),
                    MODULENAME: Request.QueryString["MODULENAME"].ToString(),
                    REASON: txtDeletedReason.Text
                    );

                Session.Remove("RedirceToAnotherPage");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted successfully.');window.location.href = 'DM_DataEntry.aspx?MODULEID=" + Request.QueryString["MODULEID"].ToString() + "&MODULENAME=" + Request.QueryString["MODULENAME"].ToString() + "&VISITID=" + Request.QueryString["VISITID"].ToString() + "&VISIT=" + Request.QueryString["VISIT"].ToString() + "&INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void GetDataExists()
        {
            try
            {
                DataSet ds = new DataSet();

                if (Request.QueryString["RECID"] == null)
                {
                    ds = dal_DM.DM_SINGLE_RECORS_SP(
                            ACTION: "CHECK_DATA_EXISTS",
                            PVID: hdnPVID.Value,
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            SUBJID: Request.QueryString["SUBJID"].ToString(),
                            VISITID: Request.QueryString["VISITID"].ToString(),
                            TABLENAME: hfTablename.Value
                          );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["RECID"].ToString() != "")
                        {
                            hdnRECID.Value = ds.Tables[0].Rows[0]["RECID"].ToString();
                            GetStructure(grd_Data);
                            GetRecords(grd_Data);
                        }
                    }
                    else
                    {
                        GetStructure(grd_Data);
                        GetRecords(grd_Data);
                    }
                }
                else
                {
                    GetStructure(grd_Data);
                    GetRecords(grd_Data);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GetDataExists_Deleted()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_DM.DM_SINGLE_RECORS_SP(
                         ACTION: "CHECK_DATA_EXISTS_Deleted",
                         PVID: hdnPVID.Value,
                         MODULEID: Request.QueryString["MODULEID"].ToString(),
                         SUBJID: Request.QueryString["SUBJID"].ToString(),
                         VISITID: Request.QueryString["VISITID"].ToString(),
                         TABLENAME: hfTablename.Value
                       );

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            foreach (DataColumn dc in ds.Tables[0].Columns)
                            {
                                if (dc.DataType.Name == "String")
                                {
                                    ds.Tables[0].Rows[i][dc.ColumnName] = REMOVEHTML(ds.Tables[0].Rows[i][dc.ColumnName].ToString());
                                }
                            }
                        }

                        grdDeletedRcords.DataSource = ds;
                        grdDeletedRcords.DataBind();

                        DivDeletedRecords.Visible = true;
                        lblTotalDeletedRecords.Visible = true;
                        lblTotalDeletedRecords.Text = "(" + grdDeletedRcords.Rows.Count + ")";
                    }
                }
                else
                {
                    grdDeletedRcords.DataSource = null;
                    grdDeletedRcords.DataBind();
                    DivDeletedRecords.Visible = false;
                    lblTotalDeletedRecords.Text = "";
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        protected void grdDeletedRcords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lnkPAGENUMDeleted = (LinkButton)e.Row.FindControl("lnkPAGENUMDeleted");
                LinkButton lnkQUERYSTATUS = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                HtmlControl ADICON = (HtmlControl)e.Row.FindControl("ADICON");

                string Visit = dr["Visit"].ToString();

                string QryCount = dr["QryCount"].ToString();

                if (QryCount == "0")
                {
                    lnkQUERYSTATUS.Attributes.Add("class", "disp-none");
                }
                LinkButton lnkQueryAns = (LinkButton)e.Row.FindControl("lnkQUERYANS");
                if (dr["QryAnsCount"].ToString() != "0")
                {
                    lnkQueryAns.Visible = true;
                }
                else
                {
                    lnkQueryAns.Visible = false;
                }

                LinkButton lnkQueryClose = (LinkButton)e.Row.FindControl("lnkQUERYCLOSE");
                if (dr["QryClosedCount"].ToString() != "0")
                {
                    lnkQueryClose.Visible = true;
                }
                else
                {
                    lnkQueryClose.Visible = false;
                }

                if (dr["AUDITSTATUS"].ToString() == "")
                {
                    AD.Attributes.Add("class", "disp-none");
                }
                else if (dr["AUDITSTATUS"].ToString() != "Initial Entry")
                {
                    AD.Attributes.Add("class", "");
                    ADICON.Attributes.Add("style", "color: red;font-size: 17px;");
                }
                else
                {
                    AD.Attributes.Add("class", "");
                    ADICON.Attributes.Add("style", "color: green;font-size: 17px;");
                }

                if (Visit != drpVisit.SelectedItem.Text)
                {
                    lnkPAGENUMDeleted.Visible = false;
                }

                grdDeletedRcords.HeaderRow.Cells[10].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[11].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[12].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[13].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[14].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[15].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[16].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[17].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[18].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[19].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[20].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[21].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[22].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[23].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[24].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[25].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[26].Visible = false;

                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;
                e.Row.Cells[21].Visible = false;
                e.Row.Cells[22].Visible = false;
                e.Row.Cells[23].Visible = false;
                e.Row.Cells[24].Visible = false;
                e.Row.Cells[25].Visible = false;
                e.Row.Cells[26].Visible = false;
            }
        }

        protected static string REMOVEHTML(string str)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");

            return str;
        }

        protected void lnkPAGENUMDeleted_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label REC_ID = (Label)row.FindControl("lblRECID");

                Response.Redirect("DM_DataEntry.aspx?MODULEID=" + Request.QueryString["MODULEID"] + "&MODULENAME=" + Request.QueryString["MODULENAME"] + "&VISITID=" + Request.QueryString["VISITID"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + Request.QueryString["SUBJID"] + "&RECID=" + REC_ID.Text + "&DELETED=1");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            if (Session["RedirceToAnotherPage"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have unsaved changes. Please click on save complete button before navigating to another screen or tab.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "window.location.href = 'DM_DataEntry.aspx?MODULEID=" + Request.QueryString["MODULEID"].ToString() + "&MODULENAME=" + Request.QueryString["MODULENAME"].ToString() + "&VISITID=" + Request.QueryString["VISITID"].ToString() + "&VISIT=" + Request.QueryString["VISIT"].ToString() + "&INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "';", true);
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.Url.ToString(), false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.Url.ToString(), false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void btnNotApplicable_Click(object sender, EventArgs e)
        {
            try
            {
                string MSG = "";
                if (btnNotApplicable.Text == "Not Applicable")
                {
                    dal_DM.DM_GetSetPV(
                    PVID: hdnPVID.Value,
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    VISITNUM: hdnVISITID.Value,
                    PAGESTATUS: "1",
                    NOTAPPLICABLE: "2",
                    MODULENAME: lblModuleName.Text,
                    INVID: Request.QueryString["INVID"].ToString(),
                    VISIT: drpVisit.SelectedItem.Text
                    );

                    MSG = lblModuleName.Text + " saved as not applicable.";
                }
                else
                {
                    dal_DM.DM_GetSetPV(
                        PVID: hdnPVID.Value,
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        MODULEID: Request.QueryString["MODULEID"].ToString(),
                        VISITNUM: hdnVISITID.Value,
                        PAGESTATUS: "1",
                        NOTAPPLICABLE: "0",
                        MODULENAME: lblModuleName.Text,
                        INVID: Request.QueryString["INVID"].ToString(),
                        VISIT: drpVisit.SelectedItem.Text
                        );

                    MSG = lblModuleName.Text + " saved as applicable.";
                }

                Response.Write("<script> alert('" + MSG + "');window.location='" + Request.RawUrl.ToString() + "'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool CHECK_Reference_Note()
        {
            bool RESULT = true;

            try
            {
                DataSet dsCases = dal_DM.DM_OnSubmit_CRIT_SP(
                    ACTION: "GET_Reference_Note_CRIT_Cases",
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    VISITNUM: Request.QueryString["VISITID"].ToString()
                    );

                foreach (DataRow drCases in dsCases.Tables[0].Rows)
                {
                    DataTable dtCurrentDATA = GET_GRID_DATATABLE();

                    DataTable dtCaseDATA = new DataTable();
                    dtCaseDATA.Columns.Add("VARIABLENAME");
                    dtCaseDATA.Columns.Add("DATA");

                    DataSet dsVarData = new DataSet();

                    string CASES = drCases["CritCode"].ToString();

                    if (CASES.Contains("[") && CASES.Contains("]"))
                    {
                        DataSet dsVariables = dal_DM.DM_OnSubmit_CRIT_SP(
                        ACTION: "GET_VARIABLE_OnSubmit_CRIT_Current",
                        ID: drCases["ID"].ToString(),
                        MODULEID: Request.QueryString["MODULEID"].ToString(),
                        VISITNUM: Request.QueryString["VISITID"].ToString()
                        );

                        foreach (DataRow drVariables in dsVariables.Tables[0].Rows)
                        {
                            DataRow[] rows = dtCurrentDATA.Select(" VARIABLENAME = '" + drVariables["ColumnName"].ToString() + "' ");

                            if (CASES.Contains("[" + drVariables["VariableName"].ToString() + "]"))
                            {
                                CASES = CASES.Replace("[" + drVariables["VariableName"].ToString() + "]", CheckDatatype(rows[0]["DATA"].ToString()));
                            }

                            dtCaseDATA.Rows.Add(drVariables["VariableName"].ToString(), rows[0]["DATA"].ToString());
                        }
                    }

                    if (CASES.Contains("[") && CASES.Contains("]"))
                    {
                        DataSet dsVariables = dal_DM.DM_OnSubmit_CRIT_SP(
                        ACTION: "GET_VARIABLE_OnSubmit_CRIT",
                        ID: drCases["ID"].ToString(),
                        MODULEID: Request.QueryString["MODULEID"].ToString(),
                        VISITNUM: Request.QueryString["VISITID"].ToString()
                        );

                        foreach (DataRow drVariables in dsVariables.Tables[0].Rows)
                        {
                            if (drVariables["Derived"].ToString() != "True")
                            {
                                string DATA = "";

                                string VariableCONDITION = drVariables["Condition"].ToString();

                                if (VariableCONDITION != "")
                                {
                                    foreach (DataRow drData in dtCurrentDATA.Rows)
                                    {
                                        if (VariableCONDITION.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                        {
                                            VariableCONDITION = VariableCONDITION.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                        }
                                    }
                                }

                                dsVarData = dal_DM.DM_OnSubmit_CRIT_SP(
                                        ACTION: "GET_DATA_AGAINST_VARIABLE",
                                        VARIABLENAME: drVariables["ColumnName"].ToString(),
                                        TABLENAME: drVariables["TableName"].ToString(),
                                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                                        VISITNUM: drVariables["Visit_ID"].ToString(),
                                        MODULEID: drVariables["Module_ID"].ToString(),
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        Condition: VariableCONDITION
                                        );

                                if (dsVarData.Tables[0].Rows.Count > 0)
                                {
                                    DATA = dsVarData.Tables[0].Rows[0][0].ToString();
                                }

                                CASES = CASES.Replace("'[" + drVariables["VariableName"].ToString() + "]'", CheckDatatype(DATA));
                                CASES = CASES.Replace("[" + drVariables["VariableName"].ToString() + "]", CheckDatatype(DATA));

                                dtCaseDATA.Rows.Add(drVariables["VariableName"].ToString(), DATA);
                            }
                            else
                            {
                                string DATA = "";
                                string FORMULA = drVariables["Formula"].ToString();

                                foreach (DataRow drData in dtCaseDATA.Rows)
                                {
                                    if (FORMULA.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                    {
                                        FORMULA = FORMULA.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                    }
                                }

                                dsVarData = dal_DM.DM_OnSubmit_CRIT_SP(
                                    ACTION: "GET_DATA_DERIVED",
                                    Formula: FORMULA
                                    );

                                if (dsVarData.Tables[0].Rows.Count > 0)
                                {
                                    DATA = dsVarData.Tables[0].Rows[0][0].ToString();
                                }

                                CASES = CASES.Replace("'[" + drVariables["VariableName"].ToString() + "]'", CheckDatatype(DATA));
                                CASES = CASES.Replace("[" + drVariables["VariableName"].ToString() + "]", CheckDatatype(DATA));

                                dtCaseDATA.Rows.Add(drVariables["VariableName"].ToString(), DATA);
                            }
                        }
                    }

                    //CASES = CASES.Replace("''", "'");

                    CASES = CASES.Replace("'''", "''");

                    CASES = CASES.Replace("'''", "''");

                    CASES = CASES.Replace("[", "");

                    CASES = CASES.Replace("]", "");

                    DataSet dsRESULT = dal_DM.DM_OnSubmit_CRIT_SP(ACTION: "GET_OnSubmit_CRIT_Result",
                        Condition: CASES,
                        CritName: "CritName"
                        );

                    if (dsRESULT.Tables.Count > 0 && dsRESULT.Tables[0].Rows.Count > 0)
                    {
                        if (dsRESULT.Tables[0].Rows[0][0].ToString() == "CritName")
                        {
                            hdnError_Msg.Value = drCases["CritName"].ToString();
                            hdnAllowable.Value = drCases["ALLOWABLE"].ToString();

                            foreach (DataRow drCase in dtCaseDATA.Rows)
                            {
                                if (hdnError_Msg.Value.Contains("[") && hdnError_Msg.Value.Contains("]"))
                                {
                                    hdnError_Msg.Value = hdnError_Msg.Value.Replace("[" + drCase["VARIABLENAME"].ToString() + "]", drCase["DATA"].ToString());
                                }
                            }

                            CREATE_REF_NOTE_TABLE(hdnError_Msg.Value);
                        }
                    }
                }

                if (dt_Ref_Note.Rows.Count > 0)
                {
                    repeat_AllModule.DataSource = dt_Ref_Note;
                    repeat_AllModule.DataBind();
                    divNote.Visible = true;
                }
                else
                {
                    repeat_AllModule.DataSource = null;
                    repeat_AllModule.DataBind();
                    divNote.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }

            return RESULT;
        }

        protected void CREATE_REF_NOTE_TABLE(string Note)
        {
            try
            {
                DataColumn dtColumn;

                if (dt_Ref_Note.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    dt_Ref_Note.Columns.Add("Note");
                }

                DataRow myDataRow;
                myDataRow = dt_Ref_Note.NewRow();
                myDataRow["Note"] = Note;
                dt_Ref_Note.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool CHECK_OnSubmit_EMAIL_CRITs()
        {
            bool RESULT = true;

            try
            {
                DataSet dsCases = dal_DM.DM_OnSubmit_CRIT_SP(
                    ACTION: "GET_ONSUBMIT_EMAILS_CRIT_Cases",
                    MODULEID: Request.QueryString["MODULEID"].ToString(),
                    VISITNUM: Request.QueryString["VISITID"].ToString()
                    );

                foreach (DataRow drCases in dsCases.Tables[0].Rows)
                {
                    if (RESULT)
                    {
                        DataTable dtCurrentDATA = GET_GRID_DATATABLE();

                        DataTable dtCaseDATA = new DataTable();
                        dtCaseDATA.Columns.Add("VARIABLENAME");
                        dtCaseDATA.Columns.Add("DATA");

                        DataSet dsVarData = new DataSet();

                        string CASES = drCases["CritCode"].ToString();

                        if (CASES.Contains("[") && CASES.Contains("]"))
                        {
                            DataSet dsVariables = dal_DM.DM_OnSubmit_CRIT_SP(
                            ACTION: "GET_VARIABLE_OnSubmit_CRIT_Current",
                            ID: drCases["ID"].ToString(),
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            VISITNUM: Request.QueryString["VISITID"].ToString()
                            );

                            foreach (DataRow drVariables in dsVariables.Tables[0].Rows)
                            {
                                DataRow[] rows = dtCurrentDATA.Select(" VARIABLENAME = '" + drVariables["ColumnName"].ToString() + "' ");

                                if (CASES.Contains("[" + drVariables["VariableName"].ToString() + "]"))
                                {
                                    CASES = CASES.Replace("[" + drVariables["VariableName"].ToString() + "]", CheckDatatype(rows[0]["DATA"].ToString()));
                                }

                                dtCaseDATA.Rows.Add(drVariables["VariableName"].ToString(), rows[0]["DATA"].ToString());
                            }
                        }

                        if (CASES.Contains("[") && CASES.Contains("]"))
                        {
                            DataSet dsVariables = dal_DM.DM_OnSubmit_CRIT_SP(
                            ACTION: "GET_VARIABLE_OnSubmit_CRIT",
                            ID: drCases["ID"].ToString(),
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            VISITNUM: Request.QueryString["VISITID"].ToString()
                            );

                            foreach (DataRow drVariables in dsVariables.Tables[0].Rows)
                            {
                                if (drVariables["Derived"].ToString() != "True")
                                {
                                    string DATA = "";

                                    string VariableCONDITION = drVariables["Condition"].ToString();

                                    if (VariableCONDITION != "")
                                    {
                                        foreach (DataRow drData in dtCurrentDATA.Rows)
                                        {
                                            if (VariableCONDITION.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                            {
                                                VariableCONDITION = VariableCONDITION.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                            }
                                        }
                                    }

                                    dsVarData = dal_DM.DM_OnSubmit_CRIT_SP(
                                        ACTION: "GET_DATA_AGAINST_VARIABLE",
                                        VARIABLENAME: drVariables["ColumnName"].ToString(),
                                        TABLENAME: drVariables["TableName"].ToString(),
                                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                                        VISITNUM: drVariables["Visit_ID"].ToString(),
                                        MODULEID: drVariables["Module_ID"].ToString(),
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        Condition: VariableCONDITION
                                        );

                                    if (dsVarData.Tables[0].Rows.Count > 0)
                                    {
                                        DATA = dsVarData.Tables[0].Rows[0][0].ToString();
                                    }

                                    CASES = CASES.Replace("'[" + drVariables["VariableName"].ToString() + "]'", CheckDatatype(DATA));
                                    CASES = CASES.Replace("[" + drVariables["VariableName"].ToString() + "]", CheckDatatype(DATA));

                                    dtCaseDATA.Rows.Add(drVariables["VariableName"].ToString(), DATA);
                                }
                                else
                                {
                                    string DATA = "";
                                    string FORMULA = drVariables["Formula"].ToString();

                                    foreach (DataRow drData in dtCaseDATA.Rows)
                                    {
                                        if (FORMULA.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                        {
                                            FORMULA = FORMULA.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                        }
                                    }

                                    dsVarData = dal_DM.DM_OnSubmit_CRIT_SP(
                                        ACTION: "GET_DATA_DERIVED",
                                        Formula: FORMULA
                                        );

                                    if (dsVarData.Tables[0].Rows.Count > 0)
                                    {
                                        DATA = dsVarData.Tables[0].Rows[0][0].ToString();
                                    }

                                    CASES = CASES.Replace("'[" + drVariables["VariableName"].ToString() + "]'", CheckDatatype(DATA));
                                    CASES = CASES.Replace("[" + drVariables["VariableName"].ToString() + "]", CheckDatatype(DATA));

                                    dtCaseDATA.Rows.Add(drVariables["VariableName"].ToString(), DATA);
                                }
                            }
                        }

                        //CASES = CASES.Replace("''", "'");

                        CASES = CASES.Replace("'''", "''");

                        CASES = CASES.Replace("'''", "''");

                        CASES = CASES.Replace("[", "");

                        CASES = CASES.Replace("]", "");

                        DataSet dsRESULT = dal_DM.DM_OnSubmit_CRIT_SP(ACTION: "GET_OnSubmit_EMAIL_CRIT_Result",
                            Condition: CASES
                            );

                        if (dsRESULT.Tables.Count > 0 && dsRESULT.Tables[0].Rows.Count > 0)
                        {
                            if (dsRESULT.Tables[0].Rows[0][0].ToString() != "0")
                            {
                                RESULT = false;

                                SendEmail(drCases["ID"].ToString(), drCases["EMAIL_BODY"].ToString(), drCases["EMAIL_SUBJECT"].ToString(), dtCaseDATA);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }

            return RESULT;
        }

        protected void SendEmail(string CRIT_ID, string EMAIL_BODY, string EMAIL_SUBJECT, DataTable dtCaseDATA)
        {
            try
            {
                CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();

                DataSet ds = dal_DM.DM_OnSubmit_CRIT_SP(ACTION: "GET_EMAILSIDS",
                        ID: CRIT_ID,
                        INVID: Request.QueryString["INVID"].ToString()
                        );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string EMAILID = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();

                    string EMAIL_CC = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();

                    string EMAIL_BCC = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();

                    string EmailSubject = EMAIL_SUBJECT;

                    string EmailBody = EMAIL_BODY;

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

                        if (EmailSubject.Contains("[SUBJID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[SUBJID]", drpSubID.SelectedValue);
                        }

                        if (EmailSubject.Contains("[VISIT]"))
                        {
                            EmailSubject = EmailSubject.Replace("[VISIT]", drpVisit.SelectedValue);
                        }

                        if (EmailSubject.Contains("[SITEID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[SITEID]", Request.QueryString["INVID"].ToString());
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

                        if (EmailBody.Contains("[PROJECTID]"))
                        {
                            EmailBody = EmailBody.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                        }

                        if (EmailBody.Contains("[SUBJID]"))
                        {
                            EmailBody = EmailBody.Replace("[SUBJID]", drpSubID.SelectedValue);
                        }

                        if (EmailBody.Contains("[VISIT]"))
                        {
                            EmailBody = EmailBody.Replace("[VISIT]", drpVisit.SelectedValue);
                        }

                        if (EmailBody.Contains("[SITEID]"))
                        {
                            EmailBody = EmailBody.Replace("[SITEID]", Request.QueryString["INVID"].ToString());
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
                            if (dtCaseDATA.Rows.Count > 0)
                            {
                                foreach (DataRow dc in dtCaseDATA.Rows)
                                {
                                    if (EmailBody.Contains("[") && EmailBody.Contains("]"))
                                    {
                                        EmailBody = EmailBody.Replace("[" + dc["VARIABLENAME"].ToString() + "]", dc["DATA"].ToString());
                                    }
                                }
                            }
                        }
                    }

                    DataSet ds1 = dal_DM.DM_EMAIL_SP(ACTION: "CHECK_EMAIL_STATUS", PVID: hdnPVID.Value, RECID: hdnRECID.Value, BODY: EmailBody);

                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["COUNTS"].ToString() == "0")
                        {
                            dal_DM.DM_EMAIL_SP
                                (
                                ACTION: "INSERT_LOG",
                                EMAILIDS: EMAILID,
                                CCEMAILIDS: EMAIL_CC,
                                BCCEMAILIDS: EMAIL_BCC,
                                SUBJECT: EmailSubject,
                                BODY: EmailBody,
                                PVID: hdnPVID.Value,
                                RECID: hdnRECID.Value,
                                SUBJID: drpSubID.SelectedValue,
                                VISITNUM: drpVisit.SelectedValue,
                                MODULEID: drpModule.SelectedValue,
                                SENT: true
                                );

                            comm.Email_Users(EMAILID, EMAIL_CC, EmailSubject, EmailBody, EMAIL_BCC);
                        }
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

        protected void btnDeleteData_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtModules = commFun.CHECK_MODULE_CRITERIA(
                              SUBJID: Request.QueryString["SUBJID"].ToString(),
                              PVID: lblPVID.Text,
                              RECID: hdnRECID.Value,
                              VISITID: Request.QueryString["VISITID"].ToString(),
                              MODULEID: Request.QueryString["MODULEID"].ToString(),
                              VARIABLENAME: "",
                              VALUE: ""
                              );

                DataTable dtVISIT = commFun.CHECK_VISIT_CRITERIA(
                            SUBJID: Request.QueryString["SUBJID"].ToString(),
                            PVID: lblPVID.Text,
                            RECID: hdnRECID.Value,
                            VISITID: Request.QueryString["VISITID"].ToString(),
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            VARIABLENAME: "",
                            VALUE: ""
                            );

                DataTable dtMerged = new DataTable();

                dtMerged.Merge(dtModules);
                dtMerged.Merge(dtVISIT);

                grdGetModules.DataSource = dtMerged;
                grdGetModules.DataBind();

                if (dtModules.Rows.Count > 0 || dtVISIT.Rows.Count > 0)
                {
                    updatepanl4.Update();
                    ModalPopupExtender3.Show();
                }
                else
                {
                    ModalPopupExtender2.Show();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void CHECK_VISIBILITY_CRTITERIAS(GridViewRow row, object sender, EventArgs e)
        {
            try
            {
                txt_TableName.Text = hfTablename.Value;
                txt_VariableName.Text = (row.FindControl("lblVARIABLENAME") as Label).Text;
                txt_ModuleName.Text = lblModuleName.Text;
                txt_FieldName.Text = (row.FindControl("lblFieldName") as Label).Text;
                txt_OldValue.Text = (row.FindControl("HDN_OLD_VALUE") as HiddenField).Value;

                string CONTROLTYPE = (row.FindControl("lblCONTROLTYPE") as Label).Text;

                string NEWVALUE = "";

                switch (CONTROLTYPE)
                {
                    case "TEXTBOX":
                        NEWVALUE = (row.FindControl("TXT_FIELD") as TextBox).Text;
                        break;
                    case "DROPDOWN":
                        NEWVALUE = (row.FindControl("DRP_FIELD") as DropDownList).Text;
                        break;
                    case "RADIOBUTTON":
                        Repeater repeaterRAD = (row.FindControl("repeat_RAD") as Repeater);
                        foreach (RepeaterItem repeaterItem in repeaterRAD.Items)
                        {
                            if ((repeaterItem.FindControl("RAD_FIELD") as RadioButton).Checked)
                            {
                                NEWVALUE = (repeaterItem.FindControl("RAD_FIELD") as RadioButton).Text;
                            }
                        }
                        break;
                    case "CHECKBOX":
                        Repeater repeater = (row.FindControl("repeat_CHK") as Repeater);
                        foreach (RepeaterItem repeaterItem in repeater.Items)
                        {
                            if ((repeaterItem.FindControl("CHK_FIELD") as CheckBox).Checked)
                            {
                                if (NEWVALUE != "")
                                {
                                    NEWVALUE += "¸" + (repeaterItem.FindControl("CHK_FIELD") as CheckBox).Text;
                                }
                                else
                                {
                                    NEWVALUE = (repeaterItem.FindControl("CHK_FIELD") as CheckBox).Text;
                                }
                            }
                        }
                        break;
                }

                if (NEWVALUE == "")
                {
                    NEWVALUE = (row.FindControl("HDN_FIELD") as HiddenField).Value;
                }

                txt_NewValue.Text = NEWVALUE;

                if (txt_NewValue.Text != txt_OldValue.Text)
                {
                    DataTable dtModules = commFun.CHECK_MODULE_CRITERIA(
                                SUBJID: Request.QueryString["SUBJID"].ToString(),
                                PVID: lblPVID.Text,
                                RECID: hdnRECID.Value,
                                VISITID: Request.QueryString["VISITID"].ToString(),
                                MODULEID: Request.QueryString["MODULEID"].ToString(),
                                VARIABLENAME: txt_VariableName.Text,
                                VALUE: txt_NewValue.Text
                                );

                    DataTable dtVISIT = commFun.CHECK_VISIT_CRITERIA(
                                SUBJID: Request.QueryString["SUBJID"].ToString(),
                                PVID: lblPVID.Text,
                                RECID: hdnRECID.Value,
                                VISITID: Request.QueryString["VISITID"].ToString(),
                                MODULEID: Request.QueryString["MODULEID"].ToString(),
                                VARIABLENAME: txt_VariableName.Text,
                                VALUE: txt_NewValue.Text
                                );

                    DataTable dtMerged = new DataTable();

                    dtMerged.Merge(dtModules);
                    dtMerged.Merge(dtVISIT);

                    grdGetModules.DataSource = dtMerged;
                    grdGetModules.DataBind();

                    if (dtModules.Rows.Count > 0 || dtVISIT.Rows.Count > 0)
                    {
                        updatepanl4.Update();
                        ModalPopupExtender3.Show();
                    }
                    else
                    {
                        if (hdnIsComplete.Value == "1")
                        {
                            HiddenField hdnQuery_OverrideId = (HiddenField)Page.Master.FindControl("hdnQuery_OverrideId");
                            HiddenField hdnQueryVariableName = (HiddenField)Page.Master.FindControl("hdnQueryVariableName");

                            if (hdnQuery_OverrideId.Value != "" && hdnQueryVariableName.Value == txt_VariableName.Text)
                            {
                                DivAction.Visible = true;
                            }
                            else
                            {
                                DivAction.Visible = false;
                            }

                            updPnlIDDetail.Update();
                            ModalPopupExtender1.Show();
                        }
                        else if (hdnIsComplete.Value == "0")
                        {
                            drp_Reason.SelectedItem.Text = "Data Saved Incomplete";
                            btn_Update_Click(sender, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
            }
        }

        private void SET_VALUE_ONLOAD()
        {
            try
            {
                DataSet ds = dal_DM.DM_Specs_Module_Wise_SP(ACTION: "GET_MODULE_CRITERIAS_VARIABLENAMES",
                    MODULEID: drpModule.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dtCurrentDATA = GET_GRID_DATATABLE();

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        CHECK_SET_VALUE("ONLOAD", dr["VARIABLENAME"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool CHECK_SET_VALUE(string EVENTS, string VARIABLENAME)
        {
            bool show_AT_Popup = true;

            try
            {
                bool RESULT = true;
                string resVARIABLENAME = "";

                DataSet dsCriterias = new DataSet();

                if (EVENTS == "ONLOAD")
                {
                    dsCriterias = dal_DM.DM_Specs_Module_Wise_SP(ACTION: "GET_CRITERIAS_ONLOAD", VARIABLENAME: VARIABLENAME, MODULEID: drpModule.SelectedValue);
                }
                else
                {
                    dsCriterias = dal_DM.DM_Specs_Module_Wise_SP(ACTION: "GET_CRITERIAS", VARIABLENAME: VARIABLENAME, MODULEID: drpModule.SelectedValue);
                }

                if (dsCriterias.Tables.Count > 0 && dsCriterias.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drCriteria in dsCriterias.Tables[0].Rows)
                    {
                        bool CRIT_Met = false;

                        string MainVARIABLENAME = drCriteria["VARIABLENAME"].ToString();

                        if (RESULT)
                        {
                            if (drCriteria["CritCode"].ToString().Trim() == "")
                            {
                                CRIT_Met = true;
                            }
                            else
                            {
                                string CRITCODE = drCriteria["CritCode"].ToString();

                                CRITCODE = CRITCODE.Replace("[Current Date]", "'" + comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy") + "'");
                                CRITCODE = CRITCODE.Replace("[Current Time]", "'" + comm.GetCurrentDateTimeByTimezone().ToString("HH:mm") + "'");

                                foreach (DataRow drCurrentDATA in dtCurrentDATA.Rows)
                                {
                                    CRITCODE = CRITCODE.Replace("[" + drCurrentDATA["VARIABLENAME"].ToString() + "]", CheckDatatype(drCurrentDATA["DATA"].ToString()));
                                }

                                if (CRITCODE != "")
                                {
                                    DataSet dsCRITRES = dal_DM.DM_Specs_Module_Wise_SP(ACTION: "CHECK_CONDITION", FORMULA: CRITCODE, MODULEID: drpModule.SelectedValue);
                                    if (dsCRITRES.Tables.Count > 0 && dsCRITRES.Tables[0].Rows.Count > 0)
                                    {
                                        if (dsCRITRES.Tables[0].Rows[0]["Data"].ToString() == "1")
                                        {
                                            CRIT_Met = true;
                                        }
                                    }
                                }
                                else
                                {
                                    CRIT_Met = true;
                                }
                            }

                            if (CRIT_Met)
                            {

                                string ERR_MSG = drCriteria["ERR_MSG"].ToString();

                                string RESTRICTED = drCriteria["RESTRICTED"].ToString();

                                string SETVALUE = drCriteria["SETVALUE"].ToString();

                                string SETVALUEDATA = drCriteria["SETVALUEDATA"].ToString();

                                string ISDERIVED = drCriteria["ISDERIVED"].ToString();

                                string ISDERIVED_VALUE = drCriteria["ISDERIVED_VALUE"].ToString();

                                if (SETVALUE == "True")
                                {
                                    if (ISDERIVED == "True")
                                    {
                                        string FORMULA = ISDERIVED_VALUE;

                                        FORMULA = FORMULA.Replace("[Current Date]", "'" + comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy") + "'");
                                        FORMULA = FORMULA.Replace("[Current Time]", "'" + comm.GetCurrentDateTimeByTimezone().ToString("HH:mm") + "'");

                                        foreach (DataRow drCurrentDATA in dtCurrentDATA.Rows)
                                        {
                                            if (drCurrentDATA["DATA"].ToString() != "")
                                            {
                                                FORMULA = FORMULA.Replace("[" + drCurrentDATA["VARIABLENAME"].ToString() + "]", CheckDatatype(drCurrentDATA["DATA"].ToString()));
                                            }
                                        }

                                        if (!FORMULA.Contains("[") && !FORMULA.Contains("]"))
                                        {
                                            DataSet dsRes = dal_DM.DM_Specs_Module_Wise_SP(ACTION: "GET_FORMULA_VALUE", FORMULA: FORMULA, MODULEID: drpModule.SelectedValue);

                                            if (dsRes.Tables.Count > 0 && dsRes.Tables[0].Rows.Count > 0)
                                            {
                                                string VALUE = dsRes.Tables[0].Rows[0]["Data"].ToString();

                                                DataRow[] rows = dtCurrentDATA.Select(" [VARIABLENAME] = '" + MainVARIABLENAME + "' ");

                                                SET_VALUE(MainVARIABLENAME, VALUE, rows[0]["CONTROLTYPE"].ToString(), rows[0]["GRIDNAME"].ToString(), rows[0]["CLIENTID"].ToString(), Convert.ToInt32(rows[0]["ROWNUM"]));

                                                //RESULT = false;
                                                resVARIABLENAME = MainVARIABLENAME;
                                            }
                                        }
                                    }
                                    else if (SETVALUEDATA != "")
                                    {
                                        SETVALUEDATA = SETVALUEDATA.Replace("[Current Date]", "" + comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy") + "");
                                        SETVALUEDATA = SETVALUEDATA.Replace("[Current Time]", "" + comm.GetCurrentDateTimeByTimezone().ToString("HH:mm") + "");

                                        DataRow[] rows = dtCurrentDATA.Select(" [VARIABLENAME] = '" + MainVARIABLENAME + "' ");

                                        SET_VALUE(MainVARIABLENAME, SETVALUEDATA, rows[0]["CONTROLTYPE"].ToString(), rows[0]["GRIDNAME"].ToString(), rows[0]["CLIENTID"].ToString(), Convert.ToInt32(rows[0]["ROWNUM"]));

                                        //RESULT = false;
                                        resVARIABLENAME = MainVARIABLENAME;
                                    }
                                }
                                else
                                {
                                    if (ERR_MSG != "")
                                    {
                                        // Convert the value to Base64
                                        string base64Msg = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ERR_MSG));

                                        // Display the Base64-encoded message safely in JavaScript
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"var encodedMsg = '{base64Msg}'; alert(atob(encodedMsg));", true);
                                    }

                                    if (RESTRICTED == "True")
                                    {
                                        show_AT_Popup = false;

                                        DataRow[] rows = dtCurrentDATA.Select(" [VARIABLENAME] = '" + MainVARIABLENAME + "' ");

                                        SET_VALUE(MainVARIABLENAME, "", rows[0]["CONTROLTYPE"].ToString(), rows[0]["GRIDNAME"].ToString(), rows[0]["CLIENTID"].ToString(), Convert.ToInt32(rows[0]["ROWNUM"]));

                                        //RESULT = false;
                                        resVARIABLENAME = MainVARIABLENAME;
                                    }

                                }

                            }
                        }

                        if (resVARIABLENAME != "" && resVARIABLENAME != VARIABLENAME && show_AT_Popup == true)
                        {
                            CHECK_SET_VALUE(EVENTS, resVARIABLENAME);
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            return show_AT_Popup;
        }

        private void SET_VALUE(string VARIABLENAME, string VALUE, string CONTROLTYPE, string GRIDNAME, string CLIENTID, int ROWNUM)
        {
            try
            {
                GridView grdName = new GridView();

                if (GRIDNAME == "grd_Data")
                {
                    grdName = grd_Data;
                }
                else if (GRIDNAME == "grd_Data1")
                {
                    int i = 0;
                    bool res = false;

                    while (i < grd_Data.Rows.Count && !res)
                    {
                        if (grd_Data.Rows[i].FindControl("grd_Data1").ClientID == CLIENTID)
                        {
                            grdName = (GridView)grd_Data.Rows[i].FindControl("grd_Data1");

                            res = true;
                        }

                        i++;
                    }
                }
                else if (GRIDNAME == "grd_Data2")
                {
                    int i1 = 0, i2 = 0;
                    bool res = false;

                    while (i1 < grd_Data.Rows.Count && !res)
                    {
                        GridView grd_Data2 = (GridView)grd_Data.Rows[i1].FindControl("grd_Data1");

                        while (i2 < grd_Data2.Rows.Count && !res)
                        {
                            if (grd_Data2.Rows[i2].FindControl("grd_Data2").ClientID == CLIENTID)
                            {
                                grdName = (GridView)grd_Data2.Rows[i2].FindControl("grd_Data2");

                                res = true;
                            }

                            i2++;
                        }

                        i1++;
                    }
                }
                else if (GRIDNAME == "grd_Data3")
                {
                    int i1 = 0, i2 = 0, i3 = 0;
                    bool res = false;

                    while (i1 < grd_Data.Rows.Count && !res)
                    {
                        GridView grd_Data1 = (GridView)grd_Data.Rows[i1].FindControl("grd_Data1");

                        while (i2 < grd_Data1.Rows.Count && !res)
                        {
                            GridView grd_Data2 = (GridView)grd_Data1.Rows[i2].FindControl("grd_Data2");

                            while (i3 < grd_Data2.Rows.Count && !res)
                            {
                                if (grd_Data2.Rows[i3].FindControl("grd_Data3").ClientID == CLIENTID)
                                {
                                    grdName = (GridView)grd_Data2.Rows[i3].FindControl("grd_Data3");

                                    res = true;
                                }

                                i3++;
                            }

                            i2++;
                        }

                        i1++;
                    }
                }

                if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Change", "UpdateChangeData('" + VARIABLENAME + "','" + VALUE + "','" + CONTROLTYPE + "');", true);
                }
                //if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                //{
                //    if (CONTROLTYPE == "TEXTBOX")
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Change", "UpdateChangeData('"+ VARIABLENAME + "','"+VALUE+"','"+ CONTROLTYPE + "');", true);
                //    }
                //    else if (CONTROLTYPE == "DROPDOWN")
                //    {
                //        ((DropDownList)grdName.Rows[ROWNUM].FindControl("DRP_FIELD")).SelectedValue = VALUE;
                //    }
                //    else if (CONTROLTYPE == "CHECKBOX")
                //    {
                //        Repeater repeat_CHK = grdName.Rows[ROWNUM].FindControl("repeat_CHK") as Repeater;
                //        for (int a = 0; a < repeat_CHK.Items.Count; a++)
                //        {
                //            if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString() == VALUE)
                //            {
                //                ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked = true;
                //            }
                //            else
                //            {
                //                ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked = false;
                //            }
                //        }
                //    }
                //    else if (CONTROLTYPE == "RADIOBUTTON")
                //    {
                //        Repeater repeat_RAD = grdName.Rows[ROWNUM].FindControl("repeat_RAD") as Repeater;
                //        for (int a = 0; a < repeat_RAD.Items.Count; a++)
                //        {
                //            if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString() == VALUE)
                //            {
                //                ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked = true;
                //            }
                //            else
                //            {
                //                ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked = false;
                //            }
                //        }
                //    }
                //}

                dtCurrentDATA = GET_GRID_DATATABLE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}
