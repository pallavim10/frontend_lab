using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;
using System.Data.SqlClient;

namespace CTMS
{
    public partial class DM_DataSDV : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        DataTable dt_Ref_Note = new DataTable();
        DataTable dtDropDownList = new DataTable();
        DataTable DM_SDVDETAILS_LOGS = new DataTable("DM_SDVDETAILS_LOGS");

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

                    hdnPVID.Value = Session["PROJECTID"].ToString() + "-" + Request.QueryString["INVID"].ToString() + "-" + Request.QueryString["SUBJID"].ToString() + "-" + Request.QueryString["VISITID"].ToString() + "-" + Request.QueryString["MODULEID"].ToString() + "-" + 1;

                    lblPVID.Text = hdnPVID.Value;
                    lblModuleName.Text = drpModule.SelectedItem.Text;
                    hdnVISITID.Value = Request.QueryString["VISITID"].ToString();

                    lblSiteId.Text = "SITE ID : " + Request.QueryString["INVID"].ToString();
                    lblSubjectId.Text = "SUBJECT ID : " + Request.QueryString["SUBJID"].ToString();
                    lblVisit.Text = "VISIT : " + Request.QueryString["VISIT"].ToString();

                    if (drpModule.SelectedValue == "0" || drpModule.SelectedValue == "")
                    {
                        grd_Data.DataSource = null;
                        grd_Data.DataBind();
                    }
                    else
                    {
                        if (Request.QueryString["MULTIPLEYN"] == "0")
                        {
                            grd_Data.Visible = true;

                            GetDataExists_SingleEntry();
                            GetDataExists_Deleted_SingleEntry();
                        }
                        else if (Request.QueryString["MULTIPLEYN"] == "1")
                        {
                            if (Request.QueryString["RECID"] != null)
                            {
                                grd_Data.Visible = true;
                                hdnRECID.Value = Request.QueryString["RECID"].ToString();
                                GetStructure(grd_Data);
                                GetRecords(grd_Data);
                            }
                            else
                            {
                                grd_Data.Visible = false;
                                btnSAVESDV.Visible = false;
                                btnCancle.Visible = false;
                                divVerifyAll.Visible = false;
                                hdnRECID.Value = "-1";
                            }

                            GetDataExists();
                            GetDataExists_Deleted();
                        }
                    }

                    Get_Page_Status();
                    GetSign_info();
                    GetSDVDetails();
                    GetQueryDetails();
                    GetAuditDetails();
                    GetCommentsetails();
                    GETHELPDATA();
                    CHECK_Reference_Note();

                    if (Request.QueryString["DELETED"] == "1")
                    {
                        btnSAVESDV.Visible = false;
                        divVerifyAll.Visible = false;
                        btnCancle.Visible = true;
                    }

                    if (Convert.ToString(Request.QueryString["VISITSDV"]) != null)
                    {
                        btnBacktoHomePage.Visible = false;
                    }
                }
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
                    drpSubID.Items.Insert(0, new ListItem("--Select--", "Select"));

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
                if (drpSubID.SelectedIndex != 0)
                {
                    DataSet ds = dal_DM.DM_MODULE_SP(ACTION: "GET_MODULE_MULTIPLE",
                        ID: drpModule.SelectedValue
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                        {
                            Response.Redirect("DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue + "&MULTIPLEYN=0", false);
                        }
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                        {
                            Response.Redirect("DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue + "&MULTIPLEYN=1", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue + "&MULTIPLEYN=" + Request.QueryString["MULTIPLEYN"], false);
                    }
                }
                else
                {
                    grd_Data.DataSource = null;
                    grd_Data.DataBind();

                    btnSAVESDV.Visible = false;
                    btnCancle.Visible = false;
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

                if (drpVisit.SelectedIndex != 0)
                {
                    DataSet ds = dal_DM.DM_MODULE_SP(ACTION: "GET_MODULE_MULTIPLE",
                        ID: drpModule.SelectedValue
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                        {
                            Response.Redirect("DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue + "&MULTIPLEYN=0", false);
                        }
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                        {
                            Response.Redirect("DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue + "&MULTIPLEYN=1", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue + "&MULTIPLEYN=" + Request.QueryString["MULTIPLEYN"], false);
                    }
                }
                else
                {
                    grd_Data.DataSource = null;
                    grd_Data.DataBind();

                    btnSAVESDV.Visible = false;
                    btnCancle.Visible = false;
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
                if (drpModule.SelectedIndex != 0)
                {
                    DataSet ds = dal_DM.DM_MODULE_SP(ACTION: "GET_MODULE_MULTIPLE",
                        ID: drpModule.SelectedValue
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                        {
                            Response.Redirect("DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue + "&MULTIPLEYN=0", false);
                        }
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                        {
                            Response.Redirect("DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue + "&MULTIPLEYN=1", false);
                        }
                    }
                }
                else
                {
                    grd_Data.DataSource = null;
                    grd_Data.DataBind();

                    btnSAVESDV.Visible = false;
                    btnCancle.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        protected void Get_Page_Status()
        {
            try
            {
                DataSet ds = new DataSet();
                if (hdnRECID.Value == "-1")
                {
                    ds = dal_DM.DM_PAGE_STATUS_SP(ACTION: "GET_PAGE_STATUS_PVID", PVID: hdnPVID.Value);
                }
                else
                {
                    ds = dal_DM.DM_PAGE_STATUS_SP(ACTION: "GET_PAGE_STATUS_PVID_RECID", PVID: hdnPVID.Value, RECID: hdnRECID.Value);
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
                else
                {
                    lblModuleStatus.Text = "Not Started";
                    hdnIsComplete.Value = "0";
                    hdnFreezeStatus.Value = "0";
                    hdnLockStatus.Value = "0";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
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
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
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
                    string SDV = dr["SDV"].ToString();
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

                        CheckBox chkSDV = (CheckBox)e.Row.FindControl("chkSDV");

                        if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                        {
                            if (READYN == "True" || (SDV == "False" || SDV == ""))
                            {
                                chkSDV.Visible = false;
                            }
                            else
                            {
                                divVerifyAll.Visible = true;
                                chkSDV.Visible = true;
                                btnSAVESDV.Visible = true;
                                chkSDV.CssClass = "chkSDV_" + VARIABLENAME + " sdvCheckbox";

                                if (dr["Critic_DP"].ToString() == "True")
                                {
                                    chkSDV.CssClass = chkSDV.CssClass + " chkCriticalDP";
                                    chkSDV.ToolTip = "Critical DP";
                                }
                            }
                        }

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

                    string SDV = dr["SDV"].ToString();

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
                                    btnEdit.DataBind(); if (dtDropDownList.Rows.Count == 0)
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

                        CheckBox chkSDV = (CheckBox)e.Row.FindControl("chkSDV");

                        if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                        {
                            if (READYN == "True" || (SDV == "False" || SDV == ""))
                            {
                                chkSDV.Visible = false;
                            }
                            else
                            {
                                divVerifyAll.Visible = true;
                                chkSDV.Visible = true;
                                btnSAVESDV.Visible = true;
                                chkSDV.CssClass = "chkSDV_" + VARIABLENAME + " sdvCheckbox";

                                if (dr["Critic_DP"].ToString() == "True")
                                {
                                    chkSDV.CssClass = chkSDV.CssClass + " chkCriticalDP";
                                    chkSDV.ToolTip = "Critical DP";
                                }
                            }
                        }

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

                    string SDV = dr["SDV"].ToString();
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

                        CheckBox chkSDV = (CheckBox)e.Row.FindControl("chkSDV");

                        if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                        {
                            if (READYN == "True" || (SDV == "False" || SDV == ""))
                            {
                                chkSDV.Visible = false;
                            }
                            else
                            {
                                divVerifyAll.Visible = true;
                                chkSDV.Visible = true;
                                btnSAVESDV.Visible = true;
                                chkSDV.CssClass = "chkSDV_" + VARIABLENAME + " sdvCheckbox";

                                if (dr["Critic_DP"].ToString() == "True")
                                {
                                    chkSDV.CssClass = chkSDV.CssClass + " chkCriticalDP";
                                    chkSDV.ToolTip = "Critical DP";
                                }
                            }
                        }

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
                    string SDV = dr["SDV"].ToString();

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

                        CheckBox chkSDV = (CheckBox)e.Row.FindControl("chkSDV");

                        if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                        {
                            if (READYN == "True" || (SDV == "False" || SDV == ""))
                            {
                                chkSDV.Visible = false;
                            }
                            else
                            {
                                divVerifyAll.Visible = true;
                                chkSDV.Visible = true;
                                btnSAVESDV.Visible = true;
                                chkSDV.CssClass = "chkSDV_" + VARIABLENAME + " sdvCheckbox";

                                if (dr["Critic_DP"].ToString() == "True")
                                {
                                    chkSDV.CssClass = chkSDV.CssClass + " chkCriticalDP";
                                    chkSDV.ToolTip = "Critical DP";
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
                                        if (COLVAL == "")
                                        {
                                            ((TextBox)grd.Rows[rownum].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
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
                                            //REQUIRED TRUE Or FALSE
                                            if (COLVAL == "")
                                            {
                                                ((TextBox)grd_Data1.Rows[a].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
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
                                                //REQUIRED TRUE Or FALSE
                                                if (COLVAL == "")
                                                {
                                                    ((TextBox)grd_Data2.Rows[b].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
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
                                                    //REQUIRED TRUE Or FALSE
                                                    if (COLVAL == "")
                                                    {
                                                        ((TextBox)grd_Data3.Rows[c].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
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
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Change", "callChange_ReadOnly();", true);
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

        protected void GetDataExists_SingleEntry()
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
                            grd_Records.DataSource = null;
                            grd_Records.DataBind();
                            GetStructure(grd_Data);
                            GetRecords(grd_Data);
                        }
                    }
                    else
                    {
                        grd_Records.DataSource = null;
                        grd_Records.DataBind();
                        btnSAVESDV.Visible = false;
                    }
                }
                else
                {
                    hdnRECID.Value = Request.QueryString["RECID"].ToString();
                    GetStructure(grd_Data);
                    GetRecords(grd_Data);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void GetDataExists_Deleted_SingleEntry()
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

                        divExistingRecord.Visible = true;
                        lblTotalDeletedRecords.Visible = true;
                        lblTotalDeletedRecords.Text = "(" + grdDeletedRcords.Rows.Count + ")";
                    }
                }
                else
                {
                    grdDeletedRcords.DataSource = null;
                    grdDeletedRcords.DataBind();
                    divExistingRecord.Visible = false;
                    lblTotalDeletedRecords.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnSAVESDV_Click(object sender, EventArgs e)
        {
            try
            {
                InsertUpdate_SDV();

                string MSG = "";

                MSG = "DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + Request.QueryString["SUBJID"] + "&MULTIPLEYN=" + Request.QueryString["MULTIPLEYN"];

                Response.Write("<script> alert('Record SDV successfully.');window.location='" + MSG + "'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void InsertUpdate_SDV()
        {
            try
            {
                string COLUMN = "", varname = "", CHECK = "", OLDVALUE = "", isSDVComplete = "1";

                for (int rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
                {
                    string strdata = "";
                    varname = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                    string CONTROLTYPE;
                    CONTROLTYPE = ((Label)grd_Data.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                    OLDVALUE = ((HiddenField)grd_Data.Rows[rownum].FindControl("hdnSDV")).Value;
                    string READYN = ((Label)grd_Data.Rows[rownum].FindControl("READYN")).Text;
                    string SDV = ((Label)grd_Data.Rows[rownum].FindControl("SDV")).Text;

                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                    {
                        if (READYN != "True" || SDV == "True")
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
                                            strdata += "," + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
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

                            if (strdata != "")
                            {
                                strdata = strdata.Replace("'", "''");
                            }

                            if (OLDVALUE.ToString() != Convert.ToString(((CheckBox)grd_Data.Rows[rownum].FindControl("chkSDV")).Checked))
                            {
                                if (COLUMN != "")
                                {
                                    COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                                }
                                else
                                {
                                    COLUMN = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                                }

                                if (((CheckBox)grd_Data.Rows[rownum].FindControl("chkSDV")).Checked)
                                {
                                    if (CHECK != "")
                                    {
                                        CHECK = CHECK + " @ni$h 1 ";
                                    }
                                    else
                                    {
                                        CHECK = " 1 ";
                                    }
                                }
                                else
                                {
                                    if (CHECK != "")
                                    {
                                        CHECK = CHECK + " @ni$h 0 ";
                                    }
                                    else
                                    {
                                        CHECK = " 0 ";
                                    }
                                }
                            }

                            if (!((CheckBox)grd_Data.Rows[rownum].FindControl("chkSDV")).Checked && ((CheckBox)grd_Data.Rows[rownum].FindControl("chkSDV")).Visible == true)
                            {
                                isSDVComplete = "2";
                            }
                        }
                    }

                    GridView grd_Data1 = grd_Data.Rows[rownum].FindControl("grd_Data1") as GridView;

                    for (int b = 0; b < grd_Data1.Rows.Count; b++)
                    {
                        string Val_Child;
                        string strdata1 = "";
                        Val_Child = ((Label)grd_Data1.Rows[b].FindControl("lblVal_Child")).Text;
                        varname = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                        CONTROLTYPE = ((Label)grd_Data1.Rows[b].FindControl("lblCONTROLTYPE")).Text;
                        OLDVALUE = ((HiddenField)grd_Data1.Rows[b].FindControl("hdnSDV")).Value;
                        READYN = ((Label)grd_Data1.Rows[b].FindControl("READYN")).Text;
                        SDV = ((Label)grd_Data1.Rows[b].FindControl("SDV")).Text;

                        if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                        {
                            if (READYN != "True" || SDV == "True")
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
                                                strdata += "¸" + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
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

                                if (strdata1 != "")
                                {
                                    strdata1 = strdata1.Replace("'", "''");
                                }

                                foreach (string val in strdata.Split('¸'))
                                {
                                    if (checkStringContains(Val_Child, val) || (Val_Child == "Is Not Blank" && strdata != "") || Val_Child == "Compare")
                                    {
                                        if (OLDVALUE.ToString() != Convert.ToString(((CheckBox)grd_Data1.Rows[b].FindControl("chkSDV")).Checked))
                                        {
                                            if (COLUMN != "")
                                            {
                                                COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                            }
                                            else
                                            {
                                                COLUMN = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                            }

                                            if (((CheckBox)grd_Data1.Rows[b].FindControl("chkSDV")).Checked)
                                            {
                                                if (CHECK != "")
                                                {
                                                    CHECK = CHECK + " @ni$h 1 ";
                                                }
                                                else
                                                {
                                                    CHECK = " 1 ";
                                                }
                                            }
                                            else
                                            {
                                                if (CHECK != "")
                                                {
                                                    CHECK = CHECK + " @ni$h 0 ";
                                                }
                                                else
                                                {
                                                    CHECK = " 0 ";
                                                }
                                            }
                                        }

                                        if (!((CheckBox)grd_Data1.Rows[b].FindControl("chkSDV")).Checked && ((CheckBox)grd_Data1.Rows[b].FindControl("chkSDV")).Visible == true)
                                        {
                                            isSDVComplete = "2";
                                        }
                                    }
                                }
                            }
                        }

                        GridView grd_Data2 = grd_Data1.Rows[b].FindControl("grd_Data2") as GridView;

                        for (int c = 0; c < grd_Data2.Rows.Count; c++)
                        {
                            string Val_Child1 = ((Label)grd_Data2.Rows[c].FindControl("lblVal_Child")).Text;
                            string strdata2 = "";
                            varname = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                            CONTROLTYPE = ((Label)grd_Data2.Rows[c].FindControl("lblCONTROLTYPE")).Text;
                            OLDVALUE = ((HiddenField)grd_Data2.Rows[c].FindControl("hdnSDV")).Value;
                            READYN = ((Label)grd_Data2.Rows[c].FindControl("READYN")).Text;
                            SDV = ((Label)grd_Data2.Rows[c].FindControl("SDV")).Text;

                            if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                            {
                                if (READYN != "True" || SDV == "True")
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
                                                    strdata2 += "," + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
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

                                    if (strdata2 != "")
                                    {
                                        strdata2 = strdata2.Replace("'", "''");
                                    }

                                    foreach (string val in strdata1.Split('¸'))
                                    {
                                        if (checkStringContains(Val_Child1, val) || (Val_Child1 == "Is Not Blank" && strdata1 != "") || Val_Child1 == "Compare")
                                        {
                                            if (OLDVALUE.ToString() != Convert.ToString(((CheckBox)grd_Data2.Rows[c].FindControl("chkSDV")).Checked))
                                            {
                                                if (COLUMN != "")
                                                {
                                                    COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                                }
                                                else
                                                {
                                                    COLUMN = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                                }

                                                if (((CheckBox)grd_Data2.Rows[c].FindControl("chkSDV")).Checked)
                                                {
                                                    if (CHECK != "")
                                                    {
                                                        CHECK = CHECK + " @ni$h 1 ";
                                                    }
                                                    else
                                                    {
                                                        CHECK = " 1 ";
                                                    }
                                                }
                                                else
                                                {
                                                    if (CHECK != "")
                                                    {
                                                        CHECK = CHECK + " @ni$h 0 ";
                                                    }
                                                    else
                                                    {
                                                        CHECK = " 0 ";
                                                    }
                                                }
                                            }

                                            if (!((CheckBox)grd_Data2.Rows[c].FindControl("chkSDV")).Checked && ((CheckBox)grd_Data2.Rows[c].FindControl("chkSDV")).Visible == true)
                                            {
                                                isSDVComplete = "2";
                                            }
                                        }
                                    }
                                }
                            }

                            GridView grd_Data3 = grd_Data2.Rows[c].FindControl("grd_Data3") as GridView;

                            for (int d = 0; d < grd_Data3.Rows.Count; d++)
                            {
                                string Val_Child2 = ((Label)grd_Data3.Rows[d].FindControl("lblVal_Child")).Text;
                                string strdata3 = "";
                                varname = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                CONTROLTYPE = ((Label)grd_Data3.Rows[d].FindControl("lblCONTROLTYPE")).Text;
                                OLDVALUE = ((HiddenField)grd_Data3.Rows[d].FindControl("hdnSDV")).Value;
                                READYN = ((Label)grd_Data3.Rows[d].FindControl("READYN")).Text;
                                SDV = ((Label)grd_Data3.Rows[d].FindControl("SDV")).Text;

                                if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                                {
                                    if (READYN != "True" || SDV == "True")
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
                                                        strdata3 += "," + ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString();
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

                                        if (strdata3 != "")
                                        {
                                            strdata3 = strdata3.Replace("'", "''");
                                        }

                                        foreach (string val in strdata2.Split('¸'))
                                        {
                                            if (checkStringContains(Val_Child2, val) || (Val_Child2 == "Is Not Blank" && strdata2 != "") || Val_Child2 == "Compare")
                                            {
                                                if (OLDVALUE.ToString() != Convert.ToString(((CheckBox)grd_Data3.Rows[d].FindControl("chkSDV")).Checked))
                                                {
                                                    if (COLUMN != "")
                                                    {
                                                        COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                                    }
                                                    else
                                                    {
                                                        COLUMN = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                                    }

                                                    if (((CheckBox)grd_Data3.Rows[d].FindControl("chkSDV")).Checked)
                                                    {
                                                        if (CHECK != "")
                                                        {
                                                            CHECK = CHECK + " @ni$h 1 ";
                                                        }
                                                        else
                                                        {
                                                            CHECK = " 1 ";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (CHECK != "")
                                                        {
                                                            CHECK = CHECK + " @ni$h 0 ";
                                                        }
                                                        else
                                                        {
                                                            CHECK = " 0 ";
                                                        }
                                                    }
                                                }

                                                if (!((CheckBox)grd_Data3.Rows[d].FindControl("chkSDV")).Checked && ((CheckBox)grd_Data3.Rows[d].FindControl("chkSDV")).Visible == true)
                                                {
                                                    isSDVComplete = "2";
                                                }
                                            }
                                        }
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

                string[] colArr = COLUMN.Split(new string[] { " @ni$h " }, StringSplitOptions.None);
                string[] chkArr = CHECK.Split(new string[] { " @ni$h " }, StringSplitOptions.None);

                string colChecked = "", colUnChecked = "";

                for (int i = 0; i < colArr.Length; i++)
                {
                    if (chkArr[i] != "")
                    {
                        ADD_NEW_ROW_DATA(colArr[i], hdnRECID.Value, Convert.ToBoolean(Convert.ToInt32(chkArr[i].Replace("N'", "").Replace("'", "").Trim())));

                        if (chkArr[i].Replace("N'", "").Replace("'", "").Trim() == "1")
                        {
                            colChecked += "," + colArr[i].Trim();
                        }
                        else
                        {
                            colUnChecked += "," + colArr[i].Trim();
                        }
                    }
                }

                dal_DM.DM_SDVDETAILS_SP(ACTION: "INSERT_UPDATE_SDV",
                    PVID: hdnPVID.Value,
                    RECID: hdnRECID.Value,
                    INVID: Request.QueryString["INVID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    VISITNUM: hdnVISITID.Value,
                    VISIT: drpVisit.SelectedItem.Text,
                    MODULEID: drpModule.SelectedValue,
                    MODULENAME: drpModule.SelectedItem.Text,
                    colChecked: colChecked,
                    colUnChecked: colUnChecked,
                    isSDVComplete: isSDVComplete
                    );

                if (DM_SDVDETAILS_LOGS.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal_DM.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "DM_SDVDETAILS_LOGS";

                        sqlbc.ColumnMappings.Add("PVID", "PVID");
                        sqlbc.ColumnMappings.Add("RECID", "RECID");
                        sqlbc.ColumnMappings.Add("INVID", "INVID");
                        sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                        sqlbc.ColumnMappings.Add("VISITNUM", "VISITNUM");
                        sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                        sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                        sqlbc.ColumnMappings.Add("SDVYN", "SDVYN");
                        sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                        sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                        sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                        sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");

                        sqlbc.WriteToServer(DM_SDVDETAILS_LOGS);

                        DM_SDVDETAILS_LOGS.Clear();
                    }
                }
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

        protected void ADD_NEW_ROW_DATA(string VariableName, string RECID, bool SDVYN)
        {
            try
            {
                CREATE_MR_DT();

                DataRow myDataRow;
                myDataRow = DM_SDVDETAILS_LOGS.NewRow();
                myDataRow["PVID"] = hdnPVID.Value;
                myDataRow["RECID"] = RECID;
                myDataRow["INVID"] = Request.QueryString["INVID"].ToString();
                myDataRow["SUBJID"] = Request.QueryString["SUBJID"].ToString();
                myDataRow["VISITNUM"] = hdnVISITID.Value;
                myDataRow["MODULEID"] = drpModule.SelectedValue;
                myDataRow["VARIABLENAME"] = VariableName.Trim();
                myDataRow["SDVYN"] = SDVYN;
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                DM_SDVDETAILS_LOGS.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CREATE_MR_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (DM_SDVDETAILS_LOGS.Columns.Count == 0)
                {
                    dtColumn = new DataColumn();
                    DM_SDVDETAILS_LOGS.Columns.Add("PVID");
                    DM_SDVDETAILS_LOGS.Columns.Add("RECID");
                    DM_SDVDETAILS_LOGS.Columns.Add("INVID");
                    DM_SDVDETAILS_LOGS.Columns.Add("SUBJID");
                    DM_SDVDETAILS_LOGS.Columns.Add("VISITNUM");
                    DM_SDVDETAILS_LOGS.Columns.Add("MODULEID");
                    DM_SDVDETAILS_LOGS.Columns.Add("VARIABLENAME");
                    DM_SDVDETAILS_LOGS.Columns.Add("SDVYN");
                    DM_SDVDETAILS_LOGS.Columns.Add("ENTEREDBY");
                    DM_SDVDETAILS_LOGS.Columns.Add("ENTEREDBYNAME");
                    DM_SDVDETAILS_LOGS.Columns.Add("ENTEREDDAT");
                    DM_SDVDETAILS_LOGS.Columns.Add("ENTERED_TZVAL");
                }
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

                        lblTotalRecords.Text = "Total " + grd_Records.Rows.Count + " Records Entered.";
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
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Records_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lnkPAGENUM = (LinkButton)e.Row.FindControl("lnkPAGENUM");
                LinkButton lnkQUERYSTATUS = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                HtmlControl ADICON = (HtmlControl)e.Row.FindControl("ADICON");
                CheckBox chkSDV = (CheckBox)e.Row.FindControl("chkSDV");
                Button btnSDV = (Button)grd_Records.HeaderRow.FindControl("btnSDV");

                DataSet dsMOD = dal_DM.DM_MULTIPLE_RECORS_SP(ACTION: "GET_MODULE_FOR_SDV",
                    MODULEID: Request.QueryString["MODULEID"].ToString());

                if (dsMOD.Tables.Count > 0 && dsMOD.Tables[0].Rows.Count > 0 && dsMOD.Tables[0].Rows[0]["SDV"].ToString() == "0")
                {
                    chkSDV.Visible = false;
                }
                else
                {
                    btnSDV.Visible = true;
                    chkSDV.Visible = true;

                    if (dr["SDVSTATUS"].ToString() == "1")
                    {
                        chkSDV.Checked = true;
                    }
                    else
                    {
                        chkSDV.Checked = false;
                    }
                }

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

                grd_Records.HeaderRow.Cells[8].Visible = false;
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

                e.Row.Cells[8].Visible = false;
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
            }
        }

        protected void lnkPAGENUM_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label REC_ID = (Label)row.FindControl("lblRECID");

                hdnRECID.Value = REC_ID.Text;
                string PVID = hdnPVID.Value;

                Response.Redirect("DM_DataSDV.aspx?MODULEID=" + Request.QueryString["MODULEID"] + "&MODULENAME=" + Request.QueryString["MODULENAME"] + "&VISITID=" + Request.QueryString["VISITID"] + "&Indication=" + Request.QueryString["Indication"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + Request.QueryString["SUBJID"] + "&MULTIPLEYN=" + Request.QueryString["MULTIPLEYN"] + "&RECID=" + REC_ID.Text);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnBacktoHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DM_CRFFORSDV.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSDV_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grd_Records.Rows.Count; i++)
                {
                    CheckBox chkSDV = (CheckBox)grd_Records.Rows[i].FindControl("chkSDV");
                    Label lblRECID = (Label)grd_Records.Rows[i].FindControl("lblRECID");

                    if (chkSDV.Checked == true)
                    {
                        dal_DM.DM_SDVDETAILS_SP(
                          ACTION: "INSERT_UPDATE_FULL_SDV",
                          PVID: hdnPVID.Value,
                          RECID: lblRECID.Text,
                          SUBJID: Request.QueryString["SUBJID"].ToString(),
                          VISITNUM: hdnVISITID.Value,
                          VISIT: drpVisit.SelectedItem.Text,
                          MODULEID: drpModule.SelectedValue,
                          MODULENAME: drpModule.SelectedItem.Text,
                          SDVYN: 1,
                          INVID: Request.QueryString["INVID"].ToString()
                          );
                    }
                    else
                    {
                        dal_DM.DM_SDVDETAILS_SP(
                          ACTION: "INSERT_UPDATE_FULL_SDV",
                          PVID: hdnPVID.Value,
                          RECID: lblRECID.Text,
                          SUBJID: Request.QueryString["SUBJID"].ToString(),
                          VISITNUM: hdnVISITID.Value,
                          VISIT: drpVisit.SelectedItem.Text,
                          MODULEID: drpModule.SelectedValue,
                          MODULENAME: drpModule.SelectedItem.Text,
                          SDVYN: 0,
                          INVID: Request.QueryString["INVID"].ToString()
                          );
                    }
                }

                string MSG = "";

                MSG = "DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&Indication=" + Request.QueryString["Indication"] + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + Request.QueryString["SUBJID"] + "&MULTIPLEYN=" + Request.QueryString["MULTIPLEYN"];

                Response.Write("<script> alert('Record SDV successfully.');window.location='" + MSG + "'; </script>");
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
                lblErrorMsg.Text = ex.Message.ToString();
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

                grdDeletedRcords.HeaderRow.Cells[7].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[8].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[9].Visible = false;
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

                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
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
            }
        }

        protected void lnkPAGENUMDeleted_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label REC_ID = (Label)row.FindControl("lblRECID");

                hdnRECID.Value = REC_ID.Text;
                string PVID = hdnPVID.Value;

                Response.Redirect("DM_DataSDV.aspx?MODULEID=" + Request.QueryString["MODULEID"] + "&MODULENAME=" + Request.QueryString["MODULENAME"] + "&VISITID=" + Request.QueryString["VISITID"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + Request.QueryString["SUBJID"] + "&MULTIPLEYN=" + Request.QueryString["MULTIPLEYN"] + "&RECID=" + REC_ID.Text + "&DELETED=1");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DM_DataSDV.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + drpVisit.SelectedValue + "&VISIT=" + drpVisit.SelectedItem.Text + "&INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + drpSubID.SelectedValue + "&MULTIPLEYN=" + Request.QueryString["MULTIPLEYN"], false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected string REMOVEHTML(string str)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");

            return str;
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
                        divHideShow.Attributes.Remove("class");
                        divHideShow.Attributes.Add("class", " disp-block");

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

        public void GetSDVDetails()
        {
            try
            {
                DataSet ds = dal_DM.DM_SDVDETAILS_SP(ACTION: "GET_SDV_DATA_COUNT",
                   PVID: hdnPVID.Value,
                   RECID: hdnRECID.Value
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdSDVDETAILS.DataSource = ds;
                    grdSDVDETAILS.DataBind();
                }
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