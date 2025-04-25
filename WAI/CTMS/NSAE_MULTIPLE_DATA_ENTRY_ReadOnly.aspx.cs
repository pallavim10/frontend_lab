using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;
using System.Data.SqlClient;


namespace CTMS
{
    public partial class NSAE_MULTIPLE_DATA_ENTRY_ReadOnly : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        DAL dal = new DAL();
        CommonFunction_SAE comm = new CommonFunction_SAE();
        DataTable SAE_MR_LOGS = new DataTable("SAE_MR_LOGS");
        DataTable SAE_AuditTrail = new DataTable("SAE_AUDITTRAIL");

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
                    lblSiteId.Text = "SITE ID : " + Request.QueryString["INVID"].ToString();
                    lblSubjectId.Text = "SUBJECT ID : " + Request.QueryString["SUBJID"].ToString();
                    lblSAE.Text = "SAE : " + Request.QueryString["SAE"].ToString();
                    lblSAEID.Text = Request.QueryString["SAEID"].ToString();
                    lblStatus.Text = "STATUS : " + Request.QueryString["STATUS"].ToString();

                    hdnSAE.Value = Request.QueryString["SAE"].ToString();
                    hdnSAEID.Value = Request.QueryString["SAEID"].ToString();

                    hdnStatus.Value = Request.QueryString["STATUS"].ToString();

                    GETMODULENAME();

                    if (drpModule.SelectedValue != "0")
                    {
                        CHECK_MM_MODULE();
                        GetStructure(grd_Data);

                        if (Request.QueryString["RECID"] == null)
                        {
                            GET_NEW_RECID();
                            btnCancle.Visible = false;
                            btnSAVEMR.Visible = false;
                            divverifyall.Visible = false;
                            grd_Data.DataSource = null;
                            grd_Data.DataBind();
                        }
                        else
                        {
                            hdnRECID.Value = Request.QueryString["RECID"].ToString();
                            btnCancle.Visible = true;
                            btnSAVEMR.Visible = true;
                            divverifyall.Visible = true;
                            GetRecords(grd_Data);
                            SAE_GetCommentsetails();
                            SAE_GetAuditDetails();
                            SAE_GetQueryDetails();
                            SAE_GetMRDetails();
                            GetSign_info();
                            Get_Page_Status();
                        }

                        SAE_GET_Page_COMMENTS();
                        SAE_GET_INERNAL_COMMENTS();
                        GETHELPDATA();
                        GetDataExists();
                        GetDataExists_Deleted();
                    }
                    else
                    {
                        btnSAVEMR.Visible = false;
                        btnCancle.Visible = false;
                        divverifyall.Visible = false;
                        divExistingRecord.Visible = false;
                        divSignOff.Visible = false;
                    }

                    if (Request.QueryString["DELETED"] == "1")
                    {
                        divverifyall.Visible = false;
                        btnCancle.Visible = true;
                        btnSAVEMR.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_NEW_RECID()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_RECID_SP(
                   SAEID: hdnSAEID.Value,
                   TABLENAME: hfTablename.Value,
                   SUBJID: Request.QueryString["SUBJID"].ToString()
                   );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdnRECID.Value = (Convert.ToInt32(ds.Tables[0].Rows[0]["RECID"]) + 1).ToString();
                }
                else
                {
                    hdnRECID.Value = "0";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CHECK_MM_MODULE()
        {
            try
            {
                DataSet ds_MM = dal_SAE.SAE_GENERAL_SP(ACTION: "CHECK_MODULE_MM", MODULEID: drpModule.SelectedValue);

                if (ds_MM.Tables[0].Rows.Count > 0)
                {
                    if (ds_MM.Tables[0].Rows[0]["SAE_MM"].ToString() == "True")
                    {
                        bntSaveComplete.Visible = true;
                        btnSaveIncomplete.Visible = true;
                        btnSAVEMR.Visible = false;
                        divverifyall.Visible = false;
                    }
                    else
                    {
                        bntSaveComplete.Visible = false;
                        btnSaveIncomplete.Visible = false;
                        btnSAVEMR.Visible = true;
                        divverifyall.Visible = true;
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
                DataSet ds = dal_SAE.SAE_INSTRUCTION_SP(MODULEID: drpModule.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    LiteralText.Text = ds.Tables[0].Rows[0]["SAE_DATA"].ToString();
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
                DataSet ds = dal_SAE.SAE_PAGE_STATUS_SP(ACTION: "GET_PAGE_STATUS_SAEID_RECID_MODULEID",
                    SAEID: hdnSAEID.Value,
                    RECID: hdnRECID.Value,
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdnIsComplete.Value = ds.Tables[0].Rows[0]["IsComplete"].ToString();
                    hdn_PAGESTATUS.Value = ds.Tables[0].Rows[0]["PAGESTATUS"].ToString();

                    if (ds.Tables[0].Rows[0]["REVIEWEDSTATUS"].ToString() == "True")
                    {
                        lblPageStatus.Visible = true;

                        lblReviewedBy.Text = ds.Tables[0].Rows[0]["REVIEWEDBYNAME"].ToString();
                        lblReviewedDatetimeServer.Text = ds.Tables[0].Rows[0]["REVIEWED_CAL_DAT"].ToString();
                        lblReviewedDatetimeUser.Text = ds.Tables[0].Rows[0]["REVIEWED_CAL_TZDAT"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["SDVSTATUS"].ToString() != "0")
                    {
                        lblPageStatus.Visible = true;
                        lblSDVStatus.Text = ds.Tables[0].Rows[0]["SDV_STATUSNAME"].ToString();
                        lblSDVBy.Text = ds.Tables[0].Rows[0]["SDVBYNAME"].ToString();
                        lblSDVDatetimeServer.Text = ds.Tables[0].Rows[0]["SDV_CAL_DAT"].ToString();
                        lblSDVDatetimeUser.Text = ds.Tables[0].Rows[0]["SDV_CAL_TZDAT"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["MRSTATUS"].ToString() != "0")
                    {
                        lblPageStatus.Visible = true;
                        lblMRStatus.Text = ds.Tables[0].Rows[0]["MR_STATUSNAME"].ToString();
                        lblMedicalReviewedBy.Text = ds.Tables[0].Rows[0]["MRBYNAME"].ToString();
                        lblMEdicalDatetimeServer.Text = ds.Tables[0].Rows[0]["MR_CAL_DAT"].ToString();
                        lblMedicalDatetimeUser.Text = ds.Tables[0].Rows[0]["MR_CAL_TZDAT"].ToString();

                        if (ds.Tables[0].Rows[0]["MRSTATUS"].ToString() == "1")
                        {
                            Chk_Verify_All.Checked = true;
                            Chk_Verify_All.Enabled = false;
                        }
                        else
                        {
                            Chk_Verify_All.Checked = false;
                            Chk_Verify_All.Enabled = true;
                        }
                    }

                    if (ds.Tables[0].Rows[0]["InvSignOff"].ToString() == "True")
                    {
                        lblPageStatus.Visible = true;
                        lblSignBy.Text = ds.Tables[0].Rows[0]["InvSignOffBYNAME"].ToString();
                        lblSignByDateTiemServer.Text = ds.Tables[0].Rows[0]["InvSignOff_CAL_DAT"].ToString();
                        lblSignByDateTimeUser.Text = ds.Tables[0].Rows[0]["InvSignOff_CAL_TZDAT"].ToString();
                    }
                }
                else
                {
                    hdnIsComplete.Value = "0";
                }

                DataSet ds1 = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_SAE_LIST_DATA", SAEID: hdnSAEID.Value);

                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    hdnDM_PVID.Value = ds1.Tables[0].Rows[0]["DM_PVID"].ToString();
                    hdnDM_RECID.Value = ds1.Tables[0].Rows[0]["DM_RECID"].ToString();
                    hdnDM_TABLENAME.Value = ds1.Tables[0].Rows[0]["TABLENAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        public void GETMODULENAME()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_SAE.SAE_MODULE_SP(ACTION: "GET_SAE_MODULE_MM");

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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpModule_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                string MSG = "";

                DataSet ds = dal_SAE.SAE_MODULE_SP(ACTION: "GET_MODULE_MULTIPLE", MODULEID: drpModule.SelectedValue);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                    {
                        MSG = "NSAE_MULTIPLE_DATA_ENTRY_ReadOnly.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue + "&ACTION=" + Request.QueryString["ACTION"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                    {
                        MSG = "NSAE_DATA_ENTRY_ReadOnly.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue + "&ACTION=" + Request.QueryString["ACTION"].ToString();
                    }
                }
                Response.Redirect(MSG, false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        private void GetStructure(GridView grd)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_STRUCTURE_SP(
                    ACTION: "GET_STRUCTURE_MM",
                    MODULEID: drpModule.SelectedValue,
                    SAEID: hdnSAEID.Value
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblModuleName.Text = drpModule.SelectedItem.Text;
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

        private void GetRecords(GridView grd)
        {
            try
            {
                string COLNAME, COLVAL;
                int rownum = 0;
                DataSet dsData = new DataSet();

                if (hfSAE_MM.Value == "True")
                {
                    dsData = dal_SAE.SAE_MODULE_DATA_SP(
                        MODULEID: drpModule.SelectedValue,
                        TABLENAME: hfTablename.Value,
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        SAEID: Request.QueryString["SAEID"].ToString(),
                        RECID: hdnRECID.Value
                        );
                }
                else
                {
                    dsData = dal_SAE.SAE_MODULE_DATA_MR_SP(
                        MODULEID: drpModule.SelectedValue,
                        TABLENAME: hfTablename.Value,
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        SAEID: Request.QueryString["SAEID"].ToString(),
                        RECID: hdnRECID.Value,
                        STATUS: hdnStatus.Value
                        );
                }

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
                            string PREFIX;
                            PREFIX = ((Label)grd.Rows[rownum].FindControl("lblPREFIXTEXT")).Text;

                            string REQUIREDYN;
                            REQUIREDYN = ((Label)grd.Rows[rownum].FindControl("lblREQUIREDYN")).Text;

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

                                    string Class = ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).CssClass;

                                    if (REQUIREDYN == "True")
                                    {
                                        //REQUIRED TRUE Or FALSE
                                        if (COLVAL == "" || COLVAL == "0")
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
                                                if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == x)
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
                                                if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == x)
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
                                            //REQUIRED TRUE Or FALSE
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

                                        string Class = ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).CssClass;

                                        if (REQUIREDYN == "True")
                                        {
                                            //REQUIRED TRUE Or FALSE
                                            if (COLVAL == "" || COLVAL == "0")
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
                                                    if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == x)
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
                                                    if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == x)
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
                                                //REQUIRED TRUE Or FALSE
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
                                            ((HiddenField)grd_Data2.Rows[b].FindControl("HDN_FIELD")).Value = COLVAL;
                                            ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).SelectedValue = COLVAL;

                                            string Class = ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).CssClass;

                                            if (REQUIREDYN == "True")
                                            {
                                                //REQUIRED TRUE Or FALSE
                                                if (COLVAL == "" || COLVAL == "0")
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
                                                        if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == x)
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
                                                        if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == x)
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
                                                    //REQUIRED TRUE Or FALSE
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

                                                string Class = ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).CssClass;

                                                if (REQUIREDYN == "True")
                                                {
                                                    //REQUIRED TRUE Or FALSE
                                                    if (COLVAL == "" || COLVAL == "0")
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
                                                            if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == x)
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
                                                            if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == x)
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Change", "callChange_ReadOnly();", true);
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

                    string MEDICAL_REVIEW = dr["MEDICAL_REVIEW"].ToString();

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
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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

                        if (Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
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
                                    ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        SAEID: hdnSAEID.Value,
                                        RECID: hdnRECID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        SAEID: hdnSAEID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                                   ACTION: "GET_OPTIONS_LIST",
                                   VARIABLENAME: VARIABLENAME
                                   );
                            }

                            btnEdit.DataSource = ds;
                            btnEdit.DataTextField = "TEXT";
                            btnEdit.DataValueField = "VALUE";
                            btnEdit.DataBind();
                        }

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                        if (LabData == "True") { btnEdit.CssClass = btnEdit.CssClass + " LabDefault"; }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
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
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value,
                                    RECID: hdnRECID.Value
                                    );
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value
                                    );
                            }
                        }
                        else
                        {
                            ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory"; ;
                            }
                        }

                        if (DefaultData != "")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == DefaultData)
                                {
                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                }
                            }
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                }
                            }
                        }

                        for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                        {
                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
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
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value,
                                    RECID: hdnRECID.Value
                                    );
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value
                                    );
                            }
                        }
                        else
                        {
                            ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory"; ;
                            }
                        }

                        if (DefaultData != "")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == DefaultData)
                                {
                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                }
                            }
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                                }
                            }
                        }

                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                        {
                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
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

                    CheckBox chkMR = (CheckBox)e.Row.FindControl("chkMR");

                    if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                    {
                        chkMR.CssClass = "chkMR_" + VARIABLENAME + " sdvCheckbox";

                        if (MEDICAL_REVIEW == "True")
                        {
                            chkMR.Visible = false;
                        }

                        if (dr["Critic_DP"].ToString() == "True")
                        {
                            chkMR.CssClass = chkMR.CssClass + " chkCriticalDP";
                            chkMR.ToolTip = "Critical DP";
                        }
                    }

                    GridView grd_Data1 = e.Row.FindControl("grd_Data1") as GridView;
                    DataSet ds1 = dal_SAE.SAE_STRUCTURE_SP(
                        ACTION: "GET_STRUCTURE_CHILD_MM",
                        MODULEID: drpModule.SelectedValue,
                        FIELDID: ID
                        );

                    grd_Data1.DataSource = ds1.Tables[0];
                    grd_Data1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
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

                    string MEDICAL_REVIEW = dr["MEDICAL_REVIEW"].ToString();

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

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();

                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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
                        if (Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
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
                                    ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        SAEID: hdnSAEID.Value,
                                        RECID: hdnRECID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        SAEID: hdnSAEID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                                   ACTION: "GET_OPTIONS_LIST",
                                   VARIABLENAME: VARIABLENAME
                                   );
                            }

                            btnEdit.DataSource = ds;
                            btnEdit.DataTextField = "TEXT";
                            btnEdit.DataValueField = "VALUE";
                            btnEdit.DataBind();
                        }

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                        if (LabData == "True") { btnEdit.CssClass = btnEdit.CssClass + " LabDefault"; }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
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
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value,
                                    RECID: hdnRECID.Value
                                    );
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value
                                    );
                            }
                        }
                        else
                        {
                            ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory"; ;
                            }
                        }

                        if (DefaultData != "")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == DefaultData)
                                {
                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                }
                            }
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                }
                            }
                        }

                        for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                        {
                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
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
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value,
                                    RECID: hdnRECID.Value
                                    );
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value
                                    );
                            }
                        }
                        else
                        {
                            ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory"; ;
                            }
                        }

                        if (DefaultData != "")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == DefaultData)
                                {
                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                }
                            }
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                                }
                            }
                        }

                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                        {
                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
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

                    CheckBox chkMR = (CheckBox)e.Row.FindControl("chkMR");

                    if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                    {
                        chkMR.CssClass = "chkMR_" + VARIABLENAME + " sdvCheckbox";

                        if (MEDICAL_REVIEW == "True")
                        {
                            chkMR.Visible = false;
                        }

                        if (dr["Critic_DP"].ToString() == "True")
                        {
                            chkMR.CssClass = chkMR.CssClass + " chkCriticalDP";
                            chkMR.ToolTip = "Critical DP";
                        }
                    }

                    GridView grd_Data2 = e.Row.FindControl("grd_Data2") as GridView;
                    DataSet ds1 = dal_SAE.SAE_STRUCTURE_SP(
                        ACTION: "GET_STRUCTURE_CHILD_MM",
                        MODULEID: drpModule.SelectedValue,
                        FIELDID: ID
                        );

                    grd_Data2.DataSource = ds1.Tables[0];
                    grd_Data2.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
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

                    string MEDICAL_REVIEW = dr["MEDICAL_REVIEW"].ToString();

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

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();

                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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
                        if (Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
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
                                    ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        SAEID: hdnSAEID.Value,
                                        RECID: hdnRECID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        SAEID: hdnSAEID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                                   ACTION: "GET_OPTIONS_LIST",
                                   VARIABLENAME: VARIABLENAME
                                   );
                            }

                            btnEdit.DataSource = ds;
                            btnEdit.DataTextField = "TEXT";
                            btnEdit.DataValueField = "VALUE";
                            btnEdit.DataBind();
                        }

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

                        if (LabData == "True") { btnEdit.CssClass = btnEdit.CssClass + " LabDefault"; }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
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
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value,
                                    RECID: hdnRECID.Value
                                    );
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value
                                    );
                            }
                        }
                        else
                        {
                            ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory"; ;
                            }
                        }

                        if (DefaultData != "")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == DefaultData)
                                {
                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                }
                            }
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                }
                            }
                        }

                        for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                        {
                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
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
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value,
                                    RECID: hdnRECID.Value
                                    );
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value
                                    );
                            }
                        }
                        else
                        {
                            ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory"; ;
                            }
                        }

                        if (DefaultData != "")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == DefaultData)
                                {
                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                }
                            }
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                                }
                            }
                        }

                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                        {
                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
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

                    CheckBox chkMR = (CheckBox)e.Row.FindControl("chkMR");

                    if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                    {
                        chkMR.CssClass = "chkMR_" + VARIABLENAME + " sdvCheckbox";

                        if (MEDICAL_REVIEW == "True")
                        {
                            chkMR.Visible = false;
                        }

                        if (dr["Critic_DP"].ToString() == "True")
                        {
                            chkMR.CssClass = chkMR.CssClass + " chkCriticalDP";
                            chkMR.ToolTip = "Critical DP";
                        }
                    }

                    GridView grd_Data3 = e.Row.FindControl("grd_Data3") as GridView;
                    DataSet ds1 = dal_SAE.SAE_STRUCTURE_SP(
                        ACTION: "GET_STRUCTURE_CHILD_MM",
                        MODULEID: drpModule.SelectedValue,
                        FIELDID: ID
                        );

                    grd_Data3.DataSource = ds1.Tables[0];
                    grd_Data3.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
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

                    string MEDICAL_REVIEW = dr["MEDICAL_REVIEW"].ToString();

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

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();

                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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
                        if (Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
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
                                    ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        SAEID: hdnSAEID.Value,
                                        RECID: hdnRECID.Value
                                        );
                                }
                                else
                                {
                                    ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                        VARIABLENAME: VARIABLENAME,
                                        TABLENAME: hfTablename.Value,
                                        SAEID: hdnSAEID.Value
                                        );
                                }
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                                   ACTION: "GET_OPTIONS_LIST",
                                   VARIABLENAME: VARIABLENAME
                                   );
                            }

                            btnEdit.DataSource = ds;
                            btnEdit.DataTextField = "TEXT";
                            btnEdit.DataValueField = "VALUE";
                            btnEdit.DataBind();
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

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                btnEdit.Enabled = false;
                            }
                        }
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
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value,
                                    RECID: hdnRECID.Value
                                    );
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value
                                    );
                            }
                        }
                        else
                        {
                            ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory"; ;
                            }
                        }

                        if (DefaultData != "")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == DefaultData)
                                {
                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                }
                            }
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                }
                            }
                        }

                        for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                        {
                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
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
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_With",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value,
                                    RECID: hdnRECID.Value
                                    );
                            }
                            else
                            {
                                ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_NONREPEAT_OPTIONS_WithOut",
                                    VARIABLENAME: VARIABLENAME,
                                    TABLENAME: hfTablename.Value,
                                    SAEID: hdnSAEID.Value
                                    );
                            }
                        }
                        else
                        {
                            ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory"; ;
                            }
                        }

                        if (DefaultData != "")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == DefaultData)
                                {
                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                }
                            }
                        }

                        if (hfSAE_MM.Value != "True")
                        {
                            if (MEDICAL_REVIEW != "True")
                            {
                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                                }
                            }
                        }

                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                        {
                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
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

                    CheckBox chkMR = (CheckBox)e.Row.FindControl("chkMR");

                    if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                    {
                        chkMR.CssClass = "chkMR_" + VARIABLENAME + " sdvCheckbox";

                        if (MEDICAL_REVIEW == "True")
                        {
                            chkMR.Visible = false;
                        }

                        if (dr["Critic_DP"].ToString() == "True")
                        {
                            chkMR.CssClass = chkMR.CssClass + " chkCriticalDP";
                            chkMR.ToolTip = "Critical DP";
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

        private string GET_AutoNum(string VARIABLENAME)
        {
            string res = "";
            try
            {
                DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                ACTION: "GET_AutoNum",
                SUBJID: Request.QueryString["SUBJID"].ToString(),
                TABLENAME: hfTablename.Value,
                MODULEID: Request.QueryString["MODULEID"].ToString(),
                VARIABLENAME: VARIABLENAME
                );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    res = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
            }
            return res;
        }

        protected void GetDataExists()
        {
            try
            {
                DataSet ds;
                DataSet dsDataCount;

                if (drpModule.SelectedIndex != 0)
                {
                    DataSet dsMOD = dal_SAE.SAE_MULTIPLE_RECORS_SP(ACTION: "GET_MODULENAME_BYID",
                    MODULEID: Request.QueryString["MODULEID"].ToString()
                    );

                    hfTablename.Value = dsMOD.Tables[0].Rows[0]["Safety_TABLENAME"].ToString();

                    ds = dal_SAE.SAE_MULTIPLE_RECORS_SP(
                              ACTION: "CHECK_DATA_EXISTS_MR",
                              SAEID: hdnSAEID.Value,
                              MODULEID: Request.QueryString["MODULEID"].ToString(),
                              SUBJID: Request.QueryString["SUBJID"].ToString(),
                              TABLENAME: hfTablename.Value,
                              STATUS: hdnStatus.Value
                            );

                    dsDataCount = dal_SAE.SAE_MULTIPLE_RECORS_SP(
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
                throw;
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
                string IsComplete = dr["IsComplete"].ToString();

                if (IsComplete == "0")
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
                grd_Records.HeaderRow.Cells[31].Visible = false;
                grd_Records.HeaderRow.Cells[32].Visible = false;
                grd_Records.HeaderRow.Cells[33].Visible = false;
                grd_Records.HeaderRow.Cells[34].Visible = false;
                grd_Records.HeaderRow.Cells[35].Visible = false;

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
                e.Row.Cells[31].Visible = false;
                e.Row.Cells[32].Visible = false;
                e.Row.Cells[33].Visible = false;
                e.Row.Cells[34].Visible = false;
                e.Row.Cells[35].Visible = false;
            }

        }

        protected void GetDataExists_Deleted()
        {
            try
            {
                DataSet ds;

                if (drpModule.SelectedIndex != 0)
                {
                    DataSet dsMOD = dal_SAE.SAE_MULTIPLE_RECORS_SP(ACTION: "GET_MODULENAME_BYID",
                    MODULEID: Request.QueryString["MODULEID"].ToString()
                    );

                    hfTablename.Value = dsMOD.Tables[0].Rows[0]["Safety_TABLENAME"].ToString();

                    ds = dal_SAE.SAE_MULTIPLE_RECORS_SP(
                              ACTION: "CHECK_DATA_EXISTS_MR_Deleted",
                              SAEID: hdnSAEID.Value,
                              MODULEID: Request.QueryString["MODULEID"].ToString(),
                              SUBJID: Request.QueryString["SUBJID"].ToString(),
                              TABLENAME: hfTablename.Value,
                              STATUS: hdnStatus.Value
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

                LinkButton lnkQUERYSTATUS = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                HtmlControl ADICON = (HtmlControl)e.Row.FindControl("ADICON");

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
                grdDeletedRcords.HeaderRow.Cells[27].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[28].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[29].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[30].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[31].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[32].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[33].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[34].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[35].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[36].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[37].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[38].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[39].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[40].Visible = false;
                grdDeletedRcords.HeaderRow.Cells[41].Visible = false;

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
                e.Row.Cells[31].Visible = false;
                e.Row.Cells[32].Visible = false;
                e.Row.Cells[33].Visible = false;
                e.Row.Cells[34].Visible = false;
                e.Row.Cells[35].Visible = false;
                e.Row.Cells[36].Visible = false;
                e.Row.Cells[37].Visible = false;
                e.Row.Cells[38].Visible = false;
                e.Row.Cells[39].Visible = false;
                e.Row.Cells[40].Visible = false;
                e.Row.Cells[41].Visible = false;
            }
        }

        protected string REMOVEHTML(string str)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");

            return str;
        }

        protected void btnSAVEMR_Click(object sender, EventArgs e)
        {
            try
            {
                InsertUpdateMH_MR();

                string MSG = "NSAE_MULTIPLE_DATA_ENTRY_ReadOnly.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue + "&ACTION=" + Request.QueryString["ACTION"].ToString();

                Response.Write("<script> alert('Medical Review Updated successfully.'); window.location.href='" + MSG + "' </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void InsertUpdateMH_MR()
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
                    OLDVALUE = ((HiddenField)grd_Data.Rows[rownum].FindControl("hdnMR")).Value;

                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                    {
                        if (((Label)grd_Data.Rows[rownum].FindControl("MEDICAL_REVIEW")).Text != "True")
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

                            if (strdata != "")
                            {
                                strdata = strdata.Replace("'", "''");
                            }

                            if (OLDVALUE.ToString() != Convert.ToString(((CheckBox)grd_Data.Rows[rownum].FindControl("chkMR")).Checked))
                            {
                                if (COLUMN != "")
                                {
                                    COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                                }
                                else
                                {
                                    COLUMN = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                                }

                                if (((CheckBox)grd_Data.Rows[rownum].FindControl("chkMR")).Checked)
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

                            if (!((CheckBox)grd_Data.Rows[rownum].FindControl("chkMR")).Checked)
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
                        OLDVALUE = ((HiddenField)grd_Data1.Rows[b].FindControl("hdnMR")).Value;

                        if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                        {
                            if (((Label)grd_Data1.Rows[b].FindControl("MEDICAL_REVIEW")).Text != "True")
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

                                if (strdata1 != "")
                                {
                                    strdata1 = strdata1.Replace("'", "''");
                                }

                                foreach (string val in strdata.Split('¸'))
                                {
                                    if (checkStringContains(Val_Child, val) || (Val_Child == "Is Not Blank" && strdata != "") || Val_Child == "Compare")
                                    {
                                        if (OLDVALUE.ToString() != Convert.ToString(((CheckBox)grd_Data1.Rows[b].FindControl("chkMR")).Checked))
                                        {
                                            if (COLUMN != "")
                                            {
                                                COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                            }
                                            else
                                            {
                                                COLUMN = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                            }

                                            if (((CheckBox)grd_Data1.Rows[b].FindControl("chkMR")).Checked)
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

                                        if (!((CheckBox)grd_Data1.Rows[b].FindControl("chkMR")).Checked)
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
                            OLDVALUE = ((HiddenField)grd_Data2.Rows[c].FindControl("hdnMR")).Value;

                            if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                            {
                                if (((Label)grd_Data2.Rows[c].FindControl("MEDICAL_REVIEW")).Text != "True")
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

                                    if (strdata2 != "")
                                    {
                                        strdata2 = strdata2.Replace("'", "''");
                                    }

                                    foreach (string val in strdata1.Split('¸'))
                                    {
                                        if (checkStringContains(Val_Child1, val) || (Val_Child1 == "Is Not Blank" && strdata1 != "") || Val_Child1 == "Compare")
                                        {
                                            if (OLDVALUE.ToString() != Convert.ToString(((CheckBox)grd_Data2.Rows[c].FindControl("chkMR")).Checked))
                                            {
                                                if (COLUMN != "")
                                                {
                                                    COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                                }
                                                else
                                                {
                                                    COLUMN = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                                }

                                                if (((CheckBox)grd_Data2.Rows[c].FindControl("chkMR")).Checked)
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

                                            if (!((CheckBox)grd_Data2.Rows[c].FindControl("chkMR")).Checked)
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
                                OLDVALUE = ((HiddenField)grd_Data3.Rows[d].FindControl("hdnMR")).Value;

                                if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                                {
                                    if (((Label)grd_Data3.Rows[d].FindControl("MEDICAL_REVIEW")).Text != "True")
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

                                        if (strdata3 != "")
                                        {
                                            strdata3 = strdata3.Replace("'", "''");
                                        }

                                        foreach (string val in strdata2.Split('¸'))
                                        {
                                            if (checkStringContains(Val_Child2, val) || (Val_Child2 == "Is Not Blank" && strdata2 != "") || Val_Child2 == "Compare")
                                            {
                                                if (OLDVALUE.ToString() != Convert.ToString(((CheckBox)grd_Data3.Rows[d].FindControl("chkMR")).Checked))
                                                {
                                                    if (COLUMN != "")
                                                    {
                                                        COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                                    }
                                                    else
                                                    {
                                                        COLUMN = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                                    }

                                                    if (((CheckBox)grd_Data3.Rows[d].FindControl("chkMR")).Checked)
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

                                                if (!((CheckBox)grd_Data3.Rows[d].FindControl("chkMR")).Checked)
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
                        ADD_NEW_ROW_DATA_MR(colArr[i], hdnRECID.Value, Convert.ToBoolean(Convert.ToInt32(chkArr[i].Replace("N'", "").Replace("'", "").Trim())));

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

                dal_SAE.SAE_MEDICAL_REVIEW_SP(ACTION: "INSERT_UPDATE_MR",
                    SAEID: hdnSAEID.Value,
                    RECID: hdnRECID.Value,
                    STATUS: hdnStatus.Value,
                    MODULEID: drpModule.SelectedValue,
                    colChecked: colChecked,
                    colUnChecked: colUnChecked,
                    isSDVComplete: isSDVComplete,
                    MODULENAME: drpModule.SelectedItem.Text
                    );

                if (SAE_MR_LOGS.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "SAE_MEDICAL_REVIEW_LOGS";

                        sqlbc.ColumnMappings.Add("SAEID", "SAEID");
                        sqlbc.ColumnMappings.Add("RECID", "RECID");
                        sqlbc.ColumnMappings.Add("STATUS", "STATUS");
                        sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                        sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                        sqlbc.ColumnMappings.Add("MRYN", "MRYN");
                        sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                        sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                        sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                        sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");

                        sqlbc.WriteToServer(SAE_MR_LOGS);

                        SAE_MR_LOGS.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ADD_NEW_ROW_DATA_MR(string VariableName, string RECID, bool MRYN)
        {
            try
            {
                CREATE_MR_DT();

                DataRow myDataRow;
                myDataRow = SAE_MR_LOGS.NewRow();
                myDataRow["SAEID"] = hdnSAEID.Value;
                myDataRow["RECID"] = RECID;
                myDataRow["STATUS"] = hdnStatus.Value;
                myDataRow["MODULEID"] = drpModule.SelectedValue;
                myDataRow["VARIABLENAME"] = VariableName.Trim();
                myDataRow["MRYN"] = MRYN;
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                SAE_MR_LOGS.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
            }
        }

        protected void CREATE_MR_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (SAE_MR_LOGS.Columns.Count == 0)
                {
                    dtColumn = new DataColumn();
                    SAE_MR_LOGS.Columns.Add("SAEID");
                    SAE_MR_LOGS.Columns.Add("RECID");
                    SAE_MR_LOGS.Columns.Add("STATUS");
                    SAE_MR_LOGS.Columns.Add("MODULEID");
                    SAE_MR_LOGS.Columns.Add("VARIABLENAME");
                    SAE_MR_LOGS.Columns.Add("MRYN");
                    SAE_MR_LOGS.Columns.Add("ENTEREDBY");
                    SAE_MR_LOGS.Columns.Add("ENTEREDBYNAME");
                    SAE_MR_LOGS.Columns.Add("ENTEREDDAT");
                    SAE_MR_LOGS.Columns.Add("ENTERED_TZVAL");
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
                DataSet ds = dal_SAE.SAE_SIGNOFF_SP(
                    ACTION: "GET_SIGNOFF_DETAILS",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    RECID: hdnRECID.Value,
                    MODULEID: drpModule.SelectedValue
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
                Response.Redirect("NSAE_MODULE_DATA_LOGS_MR.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + Request.QueryString["SAE"].ToString() + "&STATUS=" + Request.QueryString["STATUS"].ToString() + "&SAEID=" + Request.QueryString["SAEID"].ToString() + "&ACTION=" + Request.QueryString["ACTION"].ToString(), false);
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

                Response.Redirect("NSAE_MULTIPLE_DATA_ENTRY_ReadOnly.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue + "&RECID=" + REC_ID.Text + "&ACTION=" + Request.QueryString["ACTION"].ToString(), false);
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

                Response.Redirect("NSAE_MULTIPLE_DATA_ENTRY_ReadOnly.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue + "&RECID=" + REC_ID.Text + "&ACTION=" + Request.QueryString["ACTION"].ToString() + "&DELETED=1");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("NSAE_MULTIPLE_DATA_ENTRY_ReadOnly.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue + "&ACTION=" + Request.QueryString["ACTION"].ToString());
        }

        protected void DATA_Changed(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

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
                if (hdnIsComplete.Value == "1")
                {
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

        protected void btnRightClick(object sender, EventArgs e)
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

            if (hdnIsComplete.Value == "1")
            {
                upnlBtn.Update();
                updPnlIDDetail.Update();
                ModalPopupExtender1.Show();
            }
            else if (hdnIsComplete.Value == "0")
            {
                drp_Reason.SelectedItem.Text = "Data Saved Incomplete";
                btn_Update_Click(sender, e);
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
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
                    InsertUpdatedata(1);
                }
                else if (hdnIsComplete.Value == "0")
                {
                    InsertUpdatedata(0);
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Reason for change added successfully.');window.location.href = '" + Request.Url.ToString() + "';", true);
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Reason for change canceled successfully.');window.location.href = '" + Request.Url.ToString() + "';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void bntSaveComplete_Click(object sender, EventArgs e)
        {
            try
            {
                InsertUpdatedata(1);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record saved as completed successfully.');window.location.href = '" + Request.Url.ToString() + "';", true);
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
                InsertUpdatedata(0);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record saved as incomplete successfully.');window.location.href = '" + Request.Url.ToString() + "';", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void InsertUpdatedata(int CompleteOrNot)
        {
            try
            {
                bool isColAdded = false, HasMissing = false;
                string COLUMN = "", varname = "", DATA = "", FieldName = "", INSERTQUERY = "", UPDATEQUERY = "", SYNC_COLUMN = "", COLUMN_Audit = "";

                for (int rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
                {
                    string strdata = "";
                    varname = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                    string CONTROLTYPE;
                    CONTROLTYPE = ((Label)grd_Data.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                    string REQUIREDYN;
                    REQUIREDYN = ((Label)grd_Data.Rows[rownum].FindControl("lblREQUIREDYN")).Text;
                    string READYN = ((Label)grd_Data.Rows[rownum].FindControl("READYN")).Text;
                    string SYNC_COUNT = ((Label)grd_Data.Rows[rownum].FindControl("SYNC_COUNT")).Text;

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
                            FieldName = FieldName + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblFieldName")).Text + "";
                        }
                        else
                        {
                            FieldName = ((Label)grd_Data.Rows[rownum].FindControl("lblFieldName")).Text;
                        }

                        if (SYNC_COUNT != "0")
                        {
                            if (SYNC_COLUMN != "")
                            {
                                SYNC_COLUMN = SYNC_COLUMN + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                            }
                            else
                            {
                                SYNC_COLUMN = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                            }
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
                                DATA = DATA + " @ni$h '" + strdata + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";

                                strdata = "";
                            }
                        }
                        else
                        {
                            if (strdata != "")
                            {
                                DATA = "'" + strdata + "'";
                            }
                            else
                            {
                                DATA = "NULL";

                                strdata = "";
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
                        REQUIREDYN = ((Label)grd_Data1.Rows[b].FindControl("lblREQUIREDYN")).Text;
                        READYN = ((Label)grd_Data1.Rows[b].FindControl("READYN")).Text;
                        SYNC_COUNT = ((Label)grd_Data1.Rows[b].FindControl("SYNC_COUNT")).Text;

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
                                FieldName = FieldName + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblFieldName")).Text + "";
                            }
                            else
                            {
                                FieldName = ((Label)grd_Data1.Rows[b].FindControl("lblFieldName")).Text;
                            }

                            if (SYNC_COUNT != "0")
                            {
                                if (SYNC_COLUMN != "")
                                {
                                    SYNC_COLUMN = SYNC_COLUMN + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                }
                                else
                                {
                                    SYNC_COLUMN = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                }
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
                                            DATA = DATA + " @ni$h '" + strdata1 + "'";
                                        }
                                        else
                                        {
                                            DATA = DATA + " @ni$h NULL";

                                            strdata1 = "";
                                        }
                                    }
                                    else
                                    {
                                        if (strdata1 != "")
                                        {
                                            DATA = "'" + strdata1 + "'";
                                        }
                                        else
                                        {
                                            DATA = "NULL";

                                            strdata1 = "";
                                        }
                                    }
                                }
                            }

                            if (((Label)grd_Data1.Rows[b].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data1.Rows[b].FindControl("AutoNum")).Text == "False")
                            {
                                if (!isColAdded)
                                {
                                    if (strdata1 != "")
                                    {
                                        ADD_NEW_ROW_DATA(((Label)grd_Data1.Rows[b].FindControl("lblFieldName")).Text,
                                            ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text,
                                            strdata1.Replace("N'", "").Replace("'", ""),
                                            DBNull.Value.ToString(),
                                            "Parent Field Updated",
                                            DBNull.Value.ToString(),
                                            hdnRECID.Value
                                            );
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

                                        strdata1 = "";
                                    }
                                    else
                                    {
                                        DATA = "NULL";

                                        strdata1 = "";
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
                            REQUIREDYN = ((Label)grd_Data2.Rows[c].FindControl("lblREQUIREDYN")).Text;
                            READYN = ((Label)grd_Data2.Rows[c].FindControl("READYN")).Text;
                            SYNC_COUNT = ((Label)grd_Data2.Rows[c].FindControl("SYNC_COUNT")).Text;

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
                                    FieldName = FieldName + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblFieldName")).Text + "";
                                }
                                else
                                {
                                    FieldName = ((Label)grd_Data2.Rows[c].FindControl("lblFieldName")).Text;
                                }

                                if (SYNC_COUNT != "0")
                                {
                                    if (SYNC_COLUMN != "")
                                    {
                                        SYNC_COLUMN = SYNC_COLUMN + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                    }
                                    else
                                    {
                                        SYNC_COLUMN = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                    }
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

                                        if (REQUIREDYN == "True" && strdata1.Trim() == "")
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
                                                DATA = DATA + " @ni$h '" + strdata2 + "'";
                                            }
                                            else
                                            {
                                                DATA = DATA + " @ni$h NULL";

                                                strdata2 = "";
                                            }
                                        }
                                        else
                                        {
                                            if (strdata2 != "")
                                            {
                                                DATA = "'" + strdata2 + "'";
                                            }
                                            else
                                            {
                                                DATA = "NULL";

                                                strdata2 = "";
                                            }
                                        }
                                    }
                                }

                                if (((Label)grd_Data2.Rows[c].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data2.Rows[c].FindControl("AutoNum")).Text == "False")
                                {
                                    if (!isColAdded)
                                    {
                                        if (strdata2 != "")
                                        {
                                            ADD_NEW_ROW_DATA(((Label)grd_Data2.Rows[c].FindControl("lblFieldName")).Text,
                                            ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text,
                                            strdata2.Replace("N'", "").Replace("'", ""),
                                            DBNull.Value.ToString(),
                                            "Parent Field Updated",
                                            DBNull.Value.ToString(),
                                            hdnRECID.Value
                                            );
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

                                            strdata2 = "";
                                        }
                                        else
                                        {
                                            DATA = "NULL";

                                            strdata2 = "";
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
                                REQUIREDYN = ((Label)grd_Data3.Rows[d].FindControl("lblREQUIREDYN")).Text;
                                READYN = ((Label)grd_Data3.Rows[d].FindControl("READYN")).Text;
                                SYNC_COUNT = ((Label)grd_Data3.Rows[d].FindControl("SYNC_COUNT")).Text;

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

                                    if (SYNC_COUNT != "0")
                                    {
                                        if (SYNC_COLUMN != "")
                                        {
                                            SYNC_COLUMN = SYNC_COLUMN + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                        }
                                        else
                                        {
                                            SYNC_COLUMN = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
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
                                        FieldName = FieldName + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblFieldName")).Text + "";
                                    }
                                    else
                                    {
                                        FieldName = ((Label)grd_Data3.Rows[d].FindControl("lblFieldName")).Text;
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

                                            if (REQUIREDYN == "True" && strdata2.Trim() == "")
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
                                                    DATA = DATA + " @ni$h '" + strdata3 + "'";
                                                }
                                                else
                                                {
                                                    DATA = DATA + " @ni$h NULL";

                                                    strdata3 = "";
                                                }
                                            }
                                            else
                                            {
                                                if (strdata3 != "")
                                                {
                                                    DATA = "'" + strdata3 + "'";
                                                }
                                                else
                                                {
                                                    DATA = "NULL";

                                                    strdata3 = "";
                                                }
                                            }
                                        }
                                    }

                                    if (((Label)grd_Data3.Rows[d].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data3.Rows[d].FindControl("AutoNum")).Text == "False")
                                    {
                                        if (!isColAdded)
                                        {
                                            if (strdata3 != "")
                                            {
                                                ADD_NEW_ROW_DATA(((Label)grd_Data3.Rows[d].FindControl("lblFieldName")).Text,
                                                    ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text,
                                                    strdata3.Replace("N'", "").Replace("'", ""),
                                                    DBNull.Value.ToString(),
                                                    "Parent Field Updated",
                                                    DBNull.Value.ToString(),
                                                    hdnRECID.Value
                                                    );
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

                                                strdata3 = "";
                                            }
                                            else
                                            {
                                                DATA = "NULL";

                                                strdata3 = "";
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

                INSERTQUERY = "INSERT INTO [" + hfTablename.Value + "] ([SAEID], [SUBJID_DATA], [INVID], [SAE], [STATUS], [RECID], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + hdnSAEID.Value + "','" + Request.QueryString["SUBJID"].ToString() + "', '" + Request.QueryString["INVID"].ToString() + "', NULL, '" + hdnStatus.Value + "','" + hdnRECID.Value + "','" + Session["USER_ID"].ToString() + "', '" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + ")";

                string[] colArr = COLUMN.Split(new string[] { " @ni$h " }, StringSplitOptions.None);
                string[] dataArr = DATA.Split(new string[] { " @ni$h " }, StringSplitOptions.None);
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

                    if (hdn_PAGESTATUS.Value == "0")
                    {
                        if (colArr_Audit.Contains(colArr[i].Trim()))
                        {
                            ADD_NEW_ROW_DATA(fieldArr[i], colArr[i], "", dataArr[i].Replace("N'", "").Replace("'", ""), "Initial Entry", "", hdnRECID.Value);
                        }
                    }
                }

                UPDATEQUERY = UPDATEQUERY + " WHERE SAEID = '" + hdnSAEID.Value + "' AND RECID = '" + hdnRECID.Value + "' AND SUBJID_DATA = '" + Request.QueryString["SUBJID"].ToString() + "'";

                if (SAE_AuditTrail.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "SAE_AUDITTRAIL";

                        sqlbc.ColumnMappings.Add("SAEID", "SAEID");
                        sqlbc.ColumnMappings.Add("RECID", "RECID");
                        sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                        sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                        sqlbc.ColumnMappings.Add("MODULENAME", "MODULENAME");
                        sqlbc.ColumnMappings.Add("FIELDNAME", "FIELDNAME");
                        sqlbc.ColumnMappings.Add("TABLENAME", "TABLENAME");
                        sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                        sqlbc.ColumnMappings.Add("OLDVALUE", "OLDVALUE");
                        sqlbc.ColumnMappings.Add("NEWVALUE", "NEWVALUE");
                        sqlbc.ColumnMappings.Add("REASON", "REASON");
                        sqlbc.ColumnMappings.Add("SOURCE", "SOURCE");
                        sqlbc.ColumnMappings.Add("COMMENTS", "COMMENTS");
                        sqlbc.ColumnMappings.Add("STATUS", "STATUS");
                        sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                        sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                        sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                        sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");
                        sqlbc.ColumnMappings.Add("MR", "MR");

                        sqlbc.WriteToServer(SAE_AuditTrail);

                        if (hdn_PAGESTATUS.Value == "1")
                        {
                            ADD_UPDATED_AT_ENTRY_LOGS(SAE_AuditTrail, hdnRECID.Value);
                        }

                        SAE_AuditTrail.Clear();
                    }
                }

                dal_SAE.SAE_INSERT_MODULE_DATA_SP(
                     ACTION: "INSERT_MODULE_DATA",
                     TABLENAME: hfTablename.Value,
                     SUBJID: Request.QueryString["SUBJID"].ToString(),
                     INSERTQUERY: INSERTQUERY,
                     UPDATEQUERY: UPDATEQUERY,
                     SAEID: hdnSAEID.Value,
                     RECID: hdnRECID.Value,
                     IsComplete: CompleteOrNot,
                     IsMissing: HasMissing,
                     PAGESTATUS: "1",
                     DM_PVID: hdnDM_PVID.Value,
                     DM_RECID: hdnDM_RECID.Value,
                     MODULEID: drpModule.SelectedValue,
                     SAE: hdnSAE.Value,
                     INVID: Request.QueryString["INVID"].ToString(),
                     STATUS: hdnStatus.Value,
                     MODULENAME: drpModule.SelectedItem.Text
                     );

                dal_SAE.SAE_INSERT_MODULE_DATA_SP(
                     ACTION: "INSERT_MODULE_DATA_MR",
                     TABLENAME: hfTablename.Value,
                     SUBJID: Request.QueryString["SUBJID"].ToString(),
                     INSERTQUERY: INSERTQUERY,
                     UPDATEQUERY: UPDATEQUERY,
                     SAEID: hdnSAEID.Value,
                     RECID: hdnRECID.Value,
                     IsComplete: CompleteOrNot,
                     IsMissing: HasMissing,
                     PAGESTATUS: "1",
                     DM_PVID: hdnDM_PVID.Value,
                     DM_RECID: hdnDM_RECID.Value,
                     MODULEID: drpModule.SelectedValue,
                     SAE: hdnSAE.Value,
                     INVID: Request.QueryString["INVID"].ToString(),
                     STATUS: hdnStatus.Value,
                     MODULENAME: drpModule.SelectedItem.Text
                     );

                dal_SAE.SAE_INSERT_PV_SP(
                    ACTION: "INSERT_MODULE_LOGS",
                    SAEID: hdnSAEID.Value,
                    RECID: hdnRECID.Value,
                    SAE: hdnSAE.Value,
                    INVID: Request.QueryString["INVID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    STATUS: hdnStatus.Value,
                    MODULEID: drpModule.SelectedValue,
                    HasMissing: HasMissing,
                    IsComplete: CompleteOrNot,
                    PAGESTATUS: "1",
                    DM_PVID: hdnDM_PVID.Value,
                    DM_RECID: hdnDM_RECID.Value
                    );
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
                DataTable SAE_ACTIVITY_LOGS = new DataTable("SAE_ACTIVITY_LOGS");

                if (SAE_ACTIVITY_LOGS.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    SAE_ACTIVITY_LOGS.Columns.Add("SAEID");
                    SAE_ACTIVITY_LOGS.Columns.Add("RECID");
                    SAE_ACTIVITY_LOGS.Columns.Add("SAE");
                    SAE_ACTIVITY_LOGS.Columns.Add("INVID");
                    SAE_ACTIVITY_LOGS.Columns.Add("SUBJID");
                    SAE_ACTIVITY_LOGS.Columns.Add("STATUS");
                    SAE_ACTIVITY_LOGS.Columns.Add("MODULEID");
                    SAE_ACTIVITY_LOGS.Columns.Add("MODULENAME");
                    SAE_ACTIVITY_LOGS.Columns.Add("FIELDNAME");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_TYPE");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_PERFORMED");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_DETAILS");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_BY");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_BYNAME");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_DAT");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_TZVAL");
                }

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow myDataRow;
                    myDataRow = SAE_ACTIVITY_LOGS.NewRow();
                    myDataRow["SAEID"] = hdnSAEID.Value;
                    myDataRow["RECID"] = RECID;
                    myDataRow["SAE"] = hdnSAE.Value;
                    myDataRow["INVID"] = Request.QueryString["INVID"].ToString();
                    myDataRow["SUBJID"] = Request.QueryString["SUBJID"].ToString();
                    myDataRow["STATUS"] = "Initial; Incomplete";
                    myDataRow["MODULEID"] = drpModule.SelectedValue;
                    myDataRow["MODULENAME"] = drpModule.SelectedItem.Text;
                    myDataRow["FIELDNAME"] = dt.Rows[j]["FIELDNAME"].ToString().Trim();
                    myDataRow["ACT_TYPE"] = "Data Entry";
                    myDataRow["ACT_PERFORMED"] = dt.Rows[j]["Reason"].ToString().Trim();
                    myDataRow["ACT_DETAILS"] = REMOVEHTML(dt.Rows[j]["NEWVALUE"].ToString()).Replace("N'", "").Replace("'", "").Trim();
                    myDataRow["ACT_BY"] = Session["User_ID"].ToString();
                    myDataRow["ACT_BYNAME"] = Session["User_Name"].ToString();
                    myDataRow["ACT_DAT"] = DateTime.Now;
                    myDataRow["ACT_TZVAL"] = Session["TimeZone_Value"].ToString();

                    SAE_ACTIVITY_LOGS.Rows.Add(myDataRow);
                }

                //Insert Bulk Audit Trail dataset
                if (SAE_ACTIVITY_LOGS.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "SAE_ACTIVITY_LOGS";

                        sqlbc.ColumnMappings.Add("SAEID", "SAEID");
                        sqlbc.ColumnMappings.Add("RECID", "RECID");
                        sqlbc.ColumnMappings.Add("SAE", "SAE");
                        sqlbc.ColumnMappings.Add("INVID", "INVID");
                        sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                        sqlbc.ColumnMappings.Add("STATUS", "STATUS");
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

                        sqlbc.WriteToServer(SAE_ACTIVITY_LOGS);

                        SAE_ACTIVITY_LOGS.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void ADD_NEW_ROW_DATA(string FieldName, string VariableName, string OldValue, string NewValue, string Reason, string Comment, string RECID)
        {
            try
            {
                CREATE_DM_AUDITTRAIL_DT();

                DataRow myDataRow;
                myDataRow = SAE_AuditTrail.NewRow();
                myDataRow["SAEID"] = hdnSAEID.Value;
                myDataRow["RECID"] = RECID;
                myDataRow["SUBJID"] = Request.QueryString["SUBJID"].ToString();
                myDataRow["MODULEID"] = drpModule.SelectedValue;
                myDataRow["MODULENAME"] = drpModule.SelectedItem.Text;
                myDataRow["FIELDNAME"] = FieldName.Trim();
                myDataRow["TABLENAME"] = hfTablename.Value;
                myDataRow["VARIABLENAME"] = VariableName.Trim();
                myDataRow["OLDVALUE"] = REMOVEHTML(OldValue).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["NEWVALUE"] = REMOVEHTML(NewValue).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["REASON"] = Reason;
                myDataRow["SOURCE"] = "Pharmacovigilance";
                myDataRow["COMMENTS"] = Comment;
                myDataRow["STATUS"] = hdnStatus.Value;
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                myDataRow["MR"] = true;
                SAE_AuditTrail.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
            }
        }

        protected void CREATE_DM_AUDITTRAIL_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (SAE_AuditTrail.Columns.Count == 0)
                {
                    dtColumn = new DataColumn();
                    SAE_AuditTrail.Columns.Add("SAEID");
                    SAE_AuditTrail.Columns.Add("RECID");
                    SAE_AuditTrail.Columns.Add("SUBJID");
                    SAE_AuditTrail.Columns.Add("MODULEID");
                    SAE_AuditTrail.Columns.Add("MODULENAME");
                    SAE_AuditTrail.Columns.Add("FIELDNAME");
                    SAE_AuditTrail.Columns.Add("TABLENAME");
                    SAE_AuditTrail.Columns.Add("VARIABLENAME");
                    SAE_AuditTrail.Columns.Add("OLDVALUE");
                    SAE_AuditTrail.Columns.Add("NEWVALUE");
                    SAE_AuditTrail.Columns.Add("REASON");
                    SAE_AuditTrail.Columns.Add("SOURCE");
                    SAE_AuditTrail.Columns.Add("COMMENTS");
                    SAE_AuditTrail.Columns.Add("STATUS");
                    SAE_AuditTrail.Columns.Add("ENTEREDBY");
                    SAE_AuditTrail.Columns.Add("ENTEREDBYNAME");
                    SAE_AuditTrail.Columns.Add("ENTEREDDAT");
                    SAE_AuditTrail.Columns.Add("ENTERED_TZVAL");
                    SAE_AuditTrail.Columns.Add("MR");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SAE_GetAuditDetails()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_AUDITTRAIL_SP
                      (
                      ACTION: "GET_AUDITTRAIL_SAEID_RECID_MR",
                      SAEID: hdnSAEID.Value,
                      RECID: hdnRECID.Value,
                      MODULEID: drpModule.SelectedValue
                      );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SAE_grdAUDITTRAILDETAILS.DataSource = ds;
                    SAE_grdAUDITTRAILDETAILS.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SAE_GetCommentsetails()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_COMMENT_SP(ACTION: "GET_COMMENTS_COUNT_SAEID_RECID",
                  SAEID: hdnSAEID.Value,
                  RECID: hdnRECID.Value,
                  MODULEID: drpModule.SelectedValue
                  );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SAE_grdComments.DataSource = ds;
                    SAE_grdComments.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SAE_GetQueryDetails()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_QUERY_SP(ACTION: "GET_QUERY_DETAILS",
                   SAEID: hdnSAEID.Value,
                   RECID: hdnRECID.Value,
                   MODULEID: drpModule.SelectedValue
                   );

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        divHideShow.Attributes.Remove("class");
                        divHideShow.Attributes.Add("class", " disp-block");

                        SAE_grdQUERYDETAILS.DataSource = ds.Tables[0];
                        SAE_grdQUERYDETAILS.DataBind();
                    }

                    //Open Queries
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        SAE_grdOpenQuers.DataSource = ds.Tables[1];
                        SAE_grdOpenQuers.DataBind();
                    }

                    //Ans Queries
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        SAE_grdAnsQueries.DataSource = ds.Tables[2];
                        SAE_grdAnsQueries.DataBind();
                    }

                    //Closed Queries
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        SAE_grdcloseQueries.DataSource = ds.Tables[3];
                        SAE_grdcloseQueries.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SAE_GET_Page_COMMENTS()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_COMMENT_SP(
                    ACTION: "GET_PAGE_COMMENTS_COUNT_SAEID",
                    SAEID: hdnSAEID.Value,
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblComment_Count.Text = ds.Tables[0].Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SAE_GET_INERNAL_COMMENTS()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_COMMENT_SP(
                    ACTION: "GET_INTERNAL_COMMENT_SAEID",
                    SAEID: hdnSAEID.Value,
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblInternalComment_Count.Text = ds.Tables[0].Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SAE_GetMRDetails()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_MEDICAL_REVIEW_SP(ACTION: "GET_MR_DATA_COUNT",
                   SAEID: hdnSAEID.Value,
                   RECID: hdnRECID.Value,
                   MODULEID: drpModule.SelectedValue,
                   STATUS: hdnStatus.Value
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grMRDETAILS.DataSource = ds;
                    grMRDETAILS.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}