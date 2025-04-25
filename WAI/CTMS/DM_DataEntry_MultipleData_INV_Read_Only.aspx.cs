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
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_DataEntry_MultipleData_INV_Read_Only : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        DataTable dt_Ref_Note = new DataTable();
        DataTable dtDropDownList = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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

                    if (Request.QueryString["RECID"] == null)
                    {
                        hdnRECID.Value = "-1";
                    }
                    else
                    {
                        hdnRECID.Value = Request.QueryString["RECID"].ToString();
                    }

                    lblSiteId.Text = "SITE ID : " + Request.QueryString["INVID"].ToString();
                    lblSubjectId.Text = "SUBJECT ID : " + Request.QueryString["SUBJID"].ToString();
                    lblVisit.Text = "VISIT : " + Request.QueryString["VISIT"].ToString();

                    hdnVISITID.Value = Request.QueryString["VISITID"].ToString();

                    if (drpModule.SelectedIndex != 0)
                    {
                        Get_Page_Status();
                        GetStructure(grd_Data);
                        GetRecords(grd_Data);
                        GetDataExists();
                        GetDataExists_Deleted();
                        GetQueryDetails();
                        GetAuditDetails();
                        GetCommentsetails();
                        GETHELPDATA();
                        GetSign_info();
                        CHECK_Reference_Note();
                    }
                    else
                    {
                        btnCancle.Visible = false;
                        btnModuleStatus.Visible = false;
                        divExistingRecord.Visible = false;
                        divSignOff.Visible = false;
                    }

                    if (hfModuleLimit.Value != "" && hfModuleLimit.Value != "0" && hfModuleData.Value != "" && hfModuleData.Value != "0")
                    {
                        if (Convert.ToInt32(hfModuleLimit.Value) <= Convert.ToInt32(hfModuleData.Value))
                        {
                            if (hdnRECID.Value == "-1")
                            {
                                grd_Data.Visible = false;
                                btnCancle.Visible = false;

                                lblLimitReached.Text = "This module has a maximum limit of [" + hfModuleLimit.Value + "] records. You have reached this limit. For further assistance, contact the system administrator.";

                                lblLimit.Visible = false;

                                lblLimitReached.Visible = true;
                            }
                        }
                    }

                    if (Convert.ToString(Request.QueryString["DELETED"]) == "1" || hdnFreezeStatus.Value == "1" || hdnLockStatus.Value == "1")
                    {
                        btnCancle.Visible = true;
                    }

                    if (Convert.ToString(Session["LOCK_STATUS"]) != "" && hdnRECID.Value == "-1" && Convert.ToString(Request.QueryString["DELETED"]) != "1")
                    {
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
                            Response.Redirect("DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1&REFERENCE=" + Request.QueryString["REFERENCE"].ToString());
                        }
                        else
                        {
                            Response.Redirect("DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1");
                        }
                    }
                }

                hdnPVID.Value = Session["PROJECTID"].ToString() + "-" + Request.QueryString["INVID"].ToString() + "-" + Request.QueryString["SUBJID"].ToString() + "-" + Request.QueryString["VISITID"].ToString() + "-" + Request.QueryString["MODULEID"].ToString() + "-" + 1;

                hdn_PAGESTATUS.Value = "0";
                lblPVID.Text = hdnPVID.Value;
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
                if (drpSubID.SelectedValue != "0")
                {
                    DataSet ds = dal_DM.DM_MODULE_SP(ACTION: "GET_MODULE_MULTIPLE",
                        ID: drpModule.SelectedValue
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                        {
                            Response.Redirect("DM_DataEntry_INV_Read_Only.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                        }
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                        {
                            Response.Redirect("DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                        }
                    }
                    else
                    {
                        Response.Redirect("DM_DataEntry_INV_Read_Only.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                    }
                }
                else
                {
                    grd_Data.DataSource = null;
                    grd_Data.DataBind();

                    divExistingRecord.Visible = false;
                    divSignOff.Visible = false;
                }

                Session["DM_CRF_SUBJID"] = drpSubID.SelectedValue;
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
                            Response.Redirect("DM_DataEntry_INV_Read_Only.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                        }
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                        {
                            Response.Redirect("DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                        }
                    }
                    else
                    {
                        Response.Redirect("DM_DataEntry_INV_Read_Only.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                    }
                }
                else
                {
                    grd_Data.DataSource = null;
                    grd_Data.DataBind();

                    divExistingRecord.Visible = false;
                    divSignOff.Visible = false;
                }

                Session["DM_CRF_VISIT"] = drpVisit.SelectedValue;
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
                if (drpModule.SelectedValue != "0")
                {
                    DataSet ds = dal_DM.DM_MODULE_SP(ACTION: "GET_MODULE_MULTIPLE",
                        ID: drpModule.SelectedValue
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                        {
                            Response.Redirect("DM_DataEntry_INV_Read_Only.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                        }
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                        {
                            Response.Redirect("DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue, false);
                        }
                    }
                }
                else
                {
                    grd_Data.DataSource = null;
                    grd_Data.DataBind();

                    divExistingRecord.Visible = false;
                    divSignOff.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
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

                    btnCancle.Visible = true;
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();

                    btnCancle.Visible = false;

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
                    string DEFULTVAL = dr["DEFAULTVAL"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();
                    string LabData = dr["LabData"].ToString();
                    string Reference = dr["Refer"].ToString();
                    string AutoNum = dr["AutoNum"].ToString();

                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();

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
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if ((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask")))
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }

                            string Prefix = dr["Prefix"].ToString();

                            if (Prefix == "True")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }

                            if (MAXLEN != "" && MAXLEN != "0")
                            {
                                btnEdit.Attributes.Add("MaxLength", MAXLEN);
                            }
                            if (READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
                            if (DEFULTVAL != "")
                            {
                                btnEdit.Text = DEFULTVAL;
                            }
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

                                if (Request.QueryString["REFERENCE"] != null)
                                {
                                    btnEdit.Text = Request.QueryString["REFERENCE"].ToString();
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

                            if (btnEdit.CssClass.Contains("numericdecimal"))
                            {
                                string FORMAT = dr["FORMAT"].ToString();

                                btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                            }

                            btnEdit.Enabled = false;

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;
                        }
                        if (CONTROLTYPE == "DROPDOWN")
                        {
                            DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (ChildLinked == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME;
                                if (NONREPETATIVE == "True")
                                {
                                    btnEdit.CssClass = btnEdit.CssClass + " NONREPETATIVE";
                                }
                            }
                            else
                            {
                                DataSet ds = new DataSet();

                                if (NONREPETATIVE == "True")
                                {
                                    if (hdnRECID.Value != "-1")
                                    {
                                        ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                            VARIABLENAME: VARIABLENAME,
                                            TABLENAME: hfTablename.Value,
                                            PVID: hdnPVID.Value,
                                            RECID: hdnRECID.Value,
                                            VISITNUM: hdnVISITID.Value
                                            );
                                    }
                                    else
                                    {
                                        ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                            VARIABLENAME: VARIABLENAME,
                                            TABLENAME: hfTablename.Value,
                                            PVID: hdnPVID.Value,
                                            VISITNUM: hdnVISITID.Value
                                            );
                                    }
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(
                                    Action: "GET_OPTIONS_LIST_VISIT",
                                    VARIABLENAME: VARIABLENAME,
                                    VISITNUM: hdnVISITID.Value
                                    );
                                }

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

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                            if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                            if (LabData == "True") { btnEdit.CssClass = btnEdit.CssClass + " LabDefault"; }

                            btnEdit.Enabled = false;

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

                            DataSet ds = new DataSet();

                            if (NONREPETATIVE == "True")
                            {
                                if (hdnRECID.Value != "-1")
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_DM.DM_OPTIONS_DATA_SP(
                                Action: "GET_OPTIONS_LIST_VISIT",
                                VARIABLENAME: VARIABLENAME,
                                VISITNUM: hdnVISITID.Value
                                );
                            }

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

                                   ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;

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

                            DataSet ds = new DataSet();

                            if (NONREPETATIVE == "True")
                            {
                                if (hdnRECID.Value != "-1")
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_DM.DM_OPTIONS_DATA_SP(
                                Action: "GET_OPTIONS_LIST_VISIT",
                                VARIABLENAME: VARIABLENAME,
                                VISITNUM: hdnVISITID.Value
                                );
                            }

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

                                  ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;

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

                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();

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
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if ((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask")))
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }

                            string Prefix = dr["Prefix"].ToString();

                            if (Prefix == "True")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }

                            if (MAXLEN != "" && MAXLEN != "0")
                            {
                                btnEdit.Attributes.Add("MaxLength", MAXLEN);
                            }
                            if (READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
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

                                if (Request.QueryString["REFERENCE"] != null)
                                {
                                    btnEdit.Text = Request.QueryString["REFERENCE"].ToString();
                                }
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                            if (btnEdit.CssClass.Contains("numericdecimal"))
                            {
                                string FORMAT = dr["FORMAT"].ToString();

                                btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
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

                            btnEdit.Enabled = false;

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;
                        }
                        if (CONTROLTYPE == "DROPDOWN")
                        {
                            DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (ChildLinked == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME;
                                if (NONREPETATIVE == "True")
                                {
                                    btnEdit.CssClass = btnEdit.CssClass + " NONREPETATIVE";
                                }
                            }
                            else
                            {
                                DataSet ds = new DataSet();

                                if (NONREPETATIVE == "True")
                                {
                                    if (hdnRECID.Value != "-1")
                                    {
                                        ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                            VARIABLENAME: VARIABLENAME,
                                            TABLENAME: hfTablename.Value,
                                            PVID: hdnPVID.Value,
                                            RECID: hdnRECID.Value,
                                            VISITNUM: hdnVISITID.Value
                                            );
                                    }
                                    else
                                    {
                                        ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                            VARIABLENAME: VARIABLENAME,
                                            TABLENAME: hfTablename.Value,
                                            PVID: hdnPVID.Value,
                                            VISITNUM: hdnVISITID.Value
                                            );
                                    }
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(
                                    Action: "GET_OPTIONS_LIST_VISIT",
                                    VARIABLENAME: VARIABLENAME,
                                    VISITNUM: hdnVISITID.Value
                                    );
                                }

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

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                            if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                            if (LabData == "True") { btnEdit.CssClass = btnEdit.CssClass + " LabDefault"; }

                            btnEdit.Enabled = false;

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

                            DataSet ds = new DataSet();

                            if (NONREPETATIVE == "True")
                            {
                                if (hdnRECID.Value != "-1")
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_DM.DM_OPTIONS_DATA_SP(
                                Action: "GET_OPTIONS_LIST_VISIT",
                                VARIABLENAME: VARIABLENAME,
                                VISITNUM: hdnVISITID.Value
                                );
                            }

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

                                   ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;

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

                            DataSet ds = new DataSet();

                            if (NONREPETATIVE == "True")
                            {
                                if (hdnRECID.Value != "-1")
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_DM.DM_OPTIONS_DATA_SP(
                                Action: "GET_OPTIONS_LIST_VISIT",
                                VARIABLENAME: VARIABLENAME,
                                VISITNUM: hdnVISITID.Value
                                );
                            }

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

                                   ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;

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

                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();

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
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if ((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask")))
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }

                            string Prefix = dr["Prefix"].ToString();

                            if (Prefix == "True")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }

                            if (MAXLEN != "" && MAXLEN != "0")
                            {
                                btnEdit.Attributes.Add("MaxLength", MAXLEN);
                            }
                            if (READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
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

                                if (Request.QueryString["REFERENCE"] != null)
                                {
                                    btnEdit.Text = Request.QueryString["REFERENCE"].ToString();
                                }
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                            if (btnEdit.CssClass.Contains("numericdecimal"))
                            {
                                string FORMAT = dr["FORMAT"].ToString();

                                btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
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

                            btnEdit.Enabled = false;

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;
                        }
                        if (CONTROLTYPE == "DROPDOWN")
                        {
                            DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;


                            if (ChildLinked == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME;
                                if (NONREPETATIVE == "True")
                                {
                                    btnEdit.CssClass = btnEdit.CssClass + " NONREPETATIVE";
                                }
                            }
                            else
                            {
                                DataSet ds = new DataSet();

                                if (NONREPETATIVE == "True")
                                {
                                    if (hdnRECID.Value != "-1")
                                    {
                                        ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                            VARIABLENAME: VARIABLENAME,
                                            TABLENAME: hfTablename.Value,
                                            PVID: hdnPVID.Value,
                                            RECID: hdnRECID.Value,
                                            VISITNUM: hdnVISITID.Value
                                            );
                                    }
                                    else
                                    {
                                        ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                            VARIABLENAME: VARIABLENAME,
                                            TABLENAME: hfTablename.Value,
                                            PVID: hdnPVID.Value,
                                            VISITNUM: hdnVISITID.Value
                                            );
                                    }
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(
                                    Action: "GET_OPTIONS_LIST_VISIT",
                                    VARIABLENAME: VARIABLENAME,
                                    VISITNUM: hdnVISITID.Value
                                    );
                                }

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

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                            if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                            if (LabData == "True") { btnEdit.CssClass = btnEdit.CssClass + " LabDefault"; }

                            btnEdit.Enabled = false;

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

                            DataSet ds = new DataSet();

                            if (NONREPETATIVE == "True")
                            {
                                if (hdnRECID.Value != "-1")
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_DM.DM_OPTIONS_DATA_SP(
                                Action: "GET_OPTIONS_LIST_VISIT",
                                VARIABLENAME: VARIABLENAME,
                                VISITNUM: hdnVISITID.Value
                                );
                            }

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

                                 ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
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

                            DataSet ds = new DataSet();

                            if (NONREPETATIVE == "True")
                            {
                                if (hdnRECID.Value != "-1")
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_DM.DM_OPTIONS_DATA_SP(
                                Action: "GET_OPTIONS_LIST_VISIT",
                                VARIABLENAME: VARIABLENAME,
                                VISITNUM: hdnVISITID.Value
                                );
                            }

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

                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;

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

                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();

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
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if ((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask")))
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }

                            string Prefix = dr["Prefix"].ToString();

                            if (Prefix == "True")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }

                            if (MAXLEN != "" && MAXLEN != "0")
                            {
                                btnEdit.Attributes.Add("MaxLength", MAXLEN);
                            }
                            if (READYN == "True")
                            {
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
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

                                if (Request.QueryString["REFERENCE"] != null)
                                {
                                    btnEdit.Text = Request.QueryString["REFERENCE"].ToString();
                                }
                            }

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                            if (btnEdit.CssClass.Contains("numericdecimal"))
                            {
                                string FORMAT = dr["FORMAT"].ToString();

                                btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
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

                            btnEdit.Enabled = false;

                            btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;
                        }
                        if (CONTROLTYPE == "DROPDOWN")
                        {
                            DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                            btnEdit.ToolTip = FIELDNAME;

                            if (ChildLinked == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME;
                                if (NONREPETATIVE == "True")
                                {
                                    btnEdit.CssClass = btnEdit.CssClass + " NONREPETATIVE";
                                }
                            }
                            else
                            {
                                DataSet ds = new DataSet();

                                if (NONREPETATIVE == "True")
                                {
                                    if (hdnRECID.Value != "-1")
                                    {
                                        ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                            VARIABLENAME: VARIABLENAME,
                                            TABLENAME: hfTablename.Value,
                                            PVID: hdnPVID.Value,
                                            RECID: hdnRECID.Value,
                                            VISITNUM: hdnVISITID.Value
                                            );
                                    }
                                    else
                                    {
                                        ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                            VARIABLENAME: VARIABLENAME,
                                            TABLENAME: hfTablename.Value,
                                            PVID: hdnPVID.Value,
                                            VISITNUM: hdnVISITID.Value
                                            );
                                    }
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(
                                    Action: "GET_OPTIONS_LIST_VISIT",
                                    VARIABLENAME: VARIABLENAME,
                                    VISITNUM: hdnVISITID.Value
                                    );
                                }

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

                            if (MANDATORY == "True")
                            {
                                btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                            }

                            if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                            if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                            if (LabData == "True") { btnEdit.CssClass = btnEdit.CssClass + " LabDefault"; }

                            btnEdit.Enabled = false;

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

                            DataSet ds = new DataSet();

                            if (NONREPETATIVE == "True")
                            {
                                if (hdnRECID.Value != "-1")
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_DM.DM_OPTIONS_DATA_SP(
                                Action: "GET_OPTIONS_LIST_VISIT",
                                VARIABLENAME: VARIABLENAME,
                                VISITNUM: hdnVISITID.Value
                                );
                            }

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

                                  ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;

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

                            DataSet ds = new DataSet();

                            if (NONREPETATIVE == "True")
                            {
                                if (hdnRECID.Value != "-1")
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        RECID: hdnRECID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_DM.DM_OPTIONS_DATA_SP(Action: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        PVID: hdnPVID.Value,
                                        VISITNUM: hdnVISITID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_DM.DM_OPTIONS_DATA_SP(
                                Action: "GET_OPTIONS_LIST_VISIT",
                                VARIABLENAME: VARIABLENAME,
                                VISITNUM: hdnVISITID.Value
                                );
                            }

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

                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;

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
                string COLNAME, COLVAL;
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
                    DataTable dt = GenerateTransposedTable(dsData.Tables[0]);
                    ds.Tables.Add(dt);
                }
                else
                {
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

                            string REQUIREDYN;
                            REQUIREDYN = ((Label)grd.Rows[rownum].FindControl("lblREQUIREDYN")).Text;

                            string PREFIX;
                            PREFIX = ((Label)grd.Rows[rownum].FindControl("lblPREFIXTEXT")).Text;

                            string CLASS;
                            CLASS = ((Label)grd.Rows[rownum].FindControl("lblCLASS")).Text;

                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                            if (DataVariableName == COLNAME)
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
                                    COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                    ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                    ((HiddenField)grd.Rows[rownum].FindControl("HDN_FIELD")).Value = COLVAL;

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
                                    foreach (string x in stringArray)
                                    {
                                        Repeater repeat_RAD = grd.Rows[rownum].FindControl("repeat_CHK") as Repeater;
                                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                        {
                                            if (x != "")
                                            {
                                                if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                {
                                                    ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                }
                                            }
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
                                    foreach (string x in stringArray)
                                    {
                                        Repeater repeat_RAD = grd.Rows[rownum].FindControl("repeat_RAD") as Repeater;
                                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
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
                                            }
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

                            GridView grd_Data1 = grd.Rows[rownum].FindControl("grd_Data1") as GridView;

                            for (int a = 0; a < grd_Data1.Rows.Count; a++)
                            {
                                COLNAME = ((Label)grd_Data1.Rows[a].FindControl("lblVARIABLENAME")).Text;
                                CONTROLTYPE = ((Label)grd_Data1.Rows[a].FindControl("lblCONTROLTYPE")).Text;
                                DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                                REQUIREDYN = ((Label)grd_Data1.Rows[a].FindControl("lblREQUIREDYN")).Text;
                                PREFIX = ((Label)grd_Data1.Rows[a].FindControl("lblPREFIXTEXT")).Text;
                                CLASS = ((Label)grd_Data1.Rows[a].FindControl("lblCLASS")).Text;

                                if (DataVariableName == COLNAME)
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
                                        COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                        ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                        ((HiddenField)grd_Data1.Rows[a].FindControl("HDN_FIELD")).Value = COLVAL;

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
                                        foreach (string x in stringArray)
                                        {
                                            Repeater repeat_RAD = grd_Data1.Rows[a].FindControl("repeat_CHK") as Repeater;
                                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                            {
                                                if (x != "")
                                                {
                                                    if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                    {
                                                        ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                    }
                                                }
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
                                        foreach (string x in stringArray)
                                        {
                                            Repeater repeat_RAD = grd_Data1.Rows[a].FindControl("repeat_RAD") as Repeater;
                                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
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
                                                }
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

                                GridView grd_Data2 = grd_Data1.Rows[a].FindControl("grd_Data2") as GridView;

                                for (int b = 0; b < grd_Data2.Rows.Count; b++)
                                {
                                    COLNAME = ((Label)grd_Data2.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                    CONTROLTYPE = ((Label)grd_Data2.Rows[b].FindControl("lblCONTROLTYPE")).Text;
                                    DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                                    REQUIREDYN = ((Label)grd_Data2.Rows[b].FindControl("lblREQUIREDYN")).Text;
                                    PREFIX = ((Label)grd_Data2.Rows[b].FindControl("lblPREFIXTEXT")).Text;
                                    CLASS = ((Label)grd_Data2.Rows[b].FindControl("lblCLASS")).Text;

                                    if (DataVariableName == COLNAME)
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
                                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                            ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                            ((HiddenField)grd_Data2.Rows[b].FindControl("HDN_FIELD")).Value = COLVAL;

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
                                            foreach (string x in stringArray)
                                            {
                                                Repeater repeat_RAD = grd_Data2.Rows[b].FindControl("repeat_CHK") as Repeater;
                                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                                {
                                                    if (x != "")
                                                    {
                                                        if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                        {
                                                            ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                        }
                                                    }
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
                                            foreach (string x in stringArray)
                                            {
                                                Repeater repeat_RAD = grd_Data2.Rows[b].FindControl("repeat_RAD") as Repeater;
                                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
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
                                                    }
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

                                    GridView grd_Data3 = grd_Data2.Rows[b].FindControl("grd_Data3") as GridView;

                                    for (int c = 0; c < grd_Data3.Rows.Count; c++)
                                    {
                                        COLNAME = ((Label)grd_Data3.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                        CONTROLTYPE = ((Label)grd_Data3.Rows[c].FindControl("lblCONTROLTYPE")).Text;
                                        DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                                        REQUIREDYN = ((Label)grd_Data3.Rows[c].FindControl("lblREQUIREDYN")).Text;
                                        PREFIX = ((Label)grd_Data3.Rows[c].FindControl("lblPREFIXTEXT")).Text;
                                        CLASS = ((Label)grd_Data3.Rows[c].FindControl("lblCLASS")).Text;

                                        if (DataVariableName == COLNAME)
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
                                                COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                                ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                                ((HiddenField)grd_Data3.Rows[c].FindControl("HDN_FIELD")).Value = COLVAL;

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
                                                foreach (string x in stringArray)
                                                {
                                                    Repeater repeat_RAD = grd_Data3.Rows[c].FindControl("repeat_CHK") as Repeater;
                                                    for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                                    {
                                                        if (x != "")
                                                        {
                                                            if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToLowerInvariant().ToString() == x.ToLowerInvariant())
                                                            {
                                                                ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                            }
                                                        }
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
                                                foreach (string x in stringArray)
                                                {
                                                    Repeater repeat_RAD = grd_Data3.Rows[c].FindControl("repeat_RAD") as Repeater;
                                                    for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
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
                                                        }
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

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Change", "callChange();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
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

        protected void btnBacktoHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(Session["QUERY_URL"]) != "")
                {
                    Session["BACKTOQUERY_REPORT"] = "1";
                    Response.Redirect(Session["QUERY_URL"].ToString());
                }
                else if (Request.QueryString["OPENLINK"] == "1")
                {
                    Response.Write("<script> window.close(); </script>");
                }
                else
                {
                    Response.Redirect("DM_OpenCRF_INV_ReadOnly.aspx?VISITNUM=" + Request.QueryString["VISITID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string CheckDatatype(string Val)
        {
            string RESULT = "";
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

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
                else if (DateTime.TryParse(Val, out l) || cf.isDate(Val))
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
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        public void GetQueryDetails()
        {
            try
            {
                DataSet ds = dal_DM.DM_QUERY_SP(
                    ACTION: "GET_QUERY_DETAILS",
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

        public void GetCommentsetails()
        {
            try
            {
                DataSet ds = dal_DM.DM_COMMENT_SP(
                   ACTION: "GET_COMMENTS_COUNT_PVID_RECID",
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

        protected void lnkPAGENUM_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label REC_ID = (Label)row.FindControl("lblRECID");

                Response.Redirect("DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + Request.QueryString["MODULEID"] + "&MODULENAME=" + Request.QueryString["MODULENAME"] + "&VISITID=" + Request.QueryString["VISITID"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + Request.QueryString["SUBJID"] + "&RECID=" + REC_ID.Text);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void lnkPAGENUMDeleted_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label REC_ID = (Label)row.FindControl("lblRECID");

                Response.Redirect("DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + Request.QueryString["MODULEID"] + "&MODULENAME=" + Request.QueryString["MODULENAME"] + "&VISITID=" + Request.QueryString["VISITID"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + Request.QueryString["SUBJID"] + "&RECID=" + REC_ID.Text + "&DELETED=1");
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

        protected void GetDataExists()
        {
            try
            {
                DataSet ds;
                DataSet dsDataCount;

                if (drpModule.SelectedIndex != 0)
                {
                    DataSet dsMOD = dal_DM.DM_MULTIPLE_RECORS_SP(ACTION: "GET_MODULENAME_BYID",
                    MODULEID: Request.QueryString["MODULEID"].ToString()
                    );

                    hfTablename.Value = dsMOD.Tables[0].Rows[0]["TABLENAME"].ToString();

                    ds = dal_DM.DM_MULTIPLE_RECORS_SP(
                             ACTION: "CHECK_DATA_EXISTS",
                             PVID: hdnPVID.Value,
                             MODULEID: Request.QueryString["MODULEID"].ToString(),
                             SUBJID: Request.QueryString["SUBJID"].ToString(),
                             VISITID: Request.QueryString["VISITID"].ToString(),
                             TABLENAME: hfTablename.Value
                           );

                    dsDataCount = dal_DM.DM_MULTIPLE_RECORS_SP(
                         ACTION: "CHECK_DATA_EXISTS_Count",
                         SUBJID: Request.QueryString["SUBJID"].ToString(),
                         TABLENAME: hfTablename.Value
                       );

                    if (dsMOD.Tables[0].Rows[0]["Limit"].ToString() != "0" && dsMOD.Tables[0].Rows[0]["Limit"].ToString() != "")
                    {
                        hfModuleLimit.Value = dsMOD.Tables[0].Rows[0]["Limit"].ToString();

                        lblLimit.Text = "This module has a maximum limit of [" + hfModuleLimit.Value + "] records. Please ensure you stay within this limit.";

                        lblLimit.Visible = true;
                    }

                    if (dsDataCount.Tables.Count > 0 && dsDataCount.Tables[0].Rows.Count > 0)
                    {
                        hfModuleData.Value = dsDataCount.Tables[0].Rows[0]["DataCount"].ToString();
                    }

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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

                        grd_Records.DataSource = ds;
                        grd_Records.DataBind();

                        lblTotalRecords.Text = " (" + grd_Records.Rows.Count + ")";
                        lblTotalRecords.Visible = true;
                    }

                    if (ds.Tables.Count > 1)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            int remain = 0, Count_Data = 0, Count_DRP = 0;
                            string FIELDNAME = null;
                            Count_Data = Convert.ToInt32(ds.Tables[1].Rows[0]["Count_Data"]);
                            Count_DRP = Convert.ToInt32(ds.Tables[1].Rows[0]["Count_DRP"]);
                            FIELDNAME = ds.Tables[1].Rows[0]["FIELDNAME"].ToString();
                            remain = Convert.ToInt32(Count_DRP) - Convert.ToInt32(Count_Data);

                            if (remain != 0)
                            {
                                lblRemaining.Text = "Note : " + remain + " out of " + Count_DRP + " " + FIELDNAME + " not entered.";
                            }
                        }
                    }
                }
                else
                {
                    grd_Records.DataSource = null;
                    grd_Records.DataBind();

                    lblTotalRecords.Text = "";
                    lblTotalRecords.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void GetDataExists_Deleted()
        {
            try
            {
                DataSet ds;

                if (drpModule.SelectedIndex != 0)
                {
                    DataSet dsMOD = dal_DM.DM_MULTIPLE_RECORS_SP(ACTION: "GET_MODULENAME_BYID",
                    MODULEID: Request.QueryString["MODULEID"].ToString()
                    );

                    hfTablename.Value = dsMOD.Tables[0].Rows[0]["TABLENAME"].ToString();

                    ds = dal_DM.DM_MULTIPLE_RECORS_SP(
                              ACTION: "CHECK_DATA_EXISTS_Deleted",
                              PVID: hdnPVID.Value,
                              MODULEID: Request.QueryString["MODULEID"].ToString(),
                              SUBJID: Request.QueryString["SUBJID"].ToString(),
                              VISITID: Request.QueryString["VISITID"].ToString(),
                              TABLENAME: hfTablename.Value
                            );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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

                        lblTotalDeletedRecords.Visible = true;

                        lblTotalDeletedRecords.Text = "(" + grdDeletedRcords.Rows.Count + ")";
                    }
                    else
                    {
                        grdDeletedRcords.DataSource = null;
                        grdDeletedRcords.DataBind();

                        lblTotalDeletedRecords.Text = "";
                    }
                }
                else
                {
                    grdDeletedRcords.DataSource = null;
                    grdDeletedRcords.DataBind();

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

        protected void grd_Records_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable InlistEditableDT = (DataTable)ViewState["InlistEditableDT"];
            DataTable InlistDT = (DataTable)ViewState["InlistDT"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lnkPAGENUM = (LinkButton)e.Row.FindControl("lnkPAGENUM");
                LinkButton lnkQUERYSTATUS = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                HtmlControl ADICON = (HtmlControl)e.Row.FindControl("ADICON");

                string IsComplete = dr["IsComplete"].ToString();

                string Visit = dr["Visit"].ToString();

                if (IsComplete == "False")
                {
                    lnkPAGENUM.Visible = false;
                }
                else
                {
                    lnkPAGENUM.Visible = true;
                }

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

                if (Visit != drpVisit.SelectedItem.Text)
                {
                    lnkPAGENUM.Visible = false;
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

                grd_Records.HeaderRow.Cells[9].Visible = false;
                grd_Records.HeaderRow.Cells[10].Visible = false;
                grd_Records.HeaderRow.Cells[11].Visible = false;
                grd_Records.HeaderRow.Cells[12].Visible = false;
                grd_Records.HeaderRow.Cells[13].Visible = false;
                grd_Records.HeaderRow.Cells[14].Visible = false;
                grd_Records.HeaderRow.Cells[15].Visible = false;
                grd_Records.HeaderRow.Cells[16].Visible = false;
                grd_Records.HeaderRow.Cells[17].Visible = false;
                grd_Records.HeaderRow.Cells[18].Visible = false;
                grd_Records.HeaderRow.Cells[19].Visible = false;
                grd_Records.HeaderRow.Cells[20].Visible = false;
                grd_Records.HeaderRow.Cells[21].Visible = false;
                grd_Records.HeaderRow.Cells[22].Visible = false;
                grd_Records.HeaderRow.Cells[23].Visible = false;
                grd_Records.HeaderRow.Cells[24].Visible = false;
                grd_Records.HeaderRow.Cells[25].Visible = false;
                grd_Records.HeaderRow.Cells[26].Visible = false;
                grd_Records.HeaderRow.Cells[27].Visible = false;
                grd_Records.HeaderRow.Cells[28].Visible = false;
                grd_Records.HeaderRow.Cells[29].Visible = false;
                grd_Records.HeaderRow.Cells[30].Visible = false;

                e.Row.Cells[9].Visible = false;
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
                e.Row.Cells[27].Visible = false;
                e.Row.Cells[28].Visible = false;
                e.Row.Cells[29].Visible = false;
                e.Row.Cells[30].Visible = false;
            }
        }

        protected static string REMOVEHTML(string str)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");

            return str;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "window.location.href = 'DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + Request.QueryString["MODULEID"].ToString() + "&MODULENAME=" + Request.QueryString["MODULENAME"].ToString() + "&VISITID=" + Request.QueryString["VISITID"].ToString() + "&VISIT=" + Request.QueryString["VISIT"].ToString() + "&INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "';", true);
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

                            if (rows.Length > 0)
                            {
                                if (CASES.Contains("[" + drVariables["VariableName"].ToString() + "]"))
                                {
                                    CASES = CASES.Replace("[" + drVariables["VariableName"].ToString() + "]", CheckDatatype(rows[0]["DATA"].ToString()));
                                }

                                dtCaseDATA.Rows.Add(drVariables["VariableName"].ToString(), rows[0]["DATA"].ToString());
                            }
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

        private DataTable GET_GRID_DATATABLE()
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("VARIABLENAME");
            outputTable.Columns.Add("DATA");

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
                }

                outputTable.Rows.Add(varname, strdata);

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
                    }

                    outputTable.Rows.Add(varname, strdata1);

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
                        }

                        outputTable.Rows.Add(varname, strdata2);

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
                            }

                            outputTable.Rows.Add(varname, strdata3);

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
    }
}