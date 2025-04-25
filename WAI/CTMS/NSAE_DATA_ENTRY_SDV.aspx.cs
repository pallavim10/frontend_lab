using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using CTMS.CommonFunction;
using PPT;

namespace CTMS
{
    public partial class NSAE_DATA_ENTRY_SDV : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        DAL dal = new DAL();
        CommonFunction_SAE comm = new CommonFunction_SAE();
        DataTable SAE_SDV_LOGS = new DataTable("SAE_SDV_LOGS");

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
                        GetStructure(grd_Data);

                        GetDataExists();
                        GetDataExists_Deleted();

                        Get_Page_Status();

                        if (hdn_PAGESTATUS.Value == "0")
                        {
                            btnCancle.Visible = false;
                            btnSAVESDV.Visible = false;
                            divverifyall.Visible = false;
                            grd_Data.DataSource = null;
                            grd_Data.DataBind();
                        }
                        else
                        {
                            btnCancle.Visible = true;
                            btnSAVESDV.Visible = true;
                            divverifyall.Visible = true;

                            SAE_GetCommentsetails();
                            SAE_GetAuditDetails();
                            SAE_GetQueryDetails();
                            SAE_GetSDVDetails();
                            GetSign_info();
                            Get_Page_Status();
                        }

                        SAE_GET_Page_COMMENTS();
                        GETHELPDATA();
                    }
                    else
                    {
                        grd_Data.DataSource = null;
                        grd_Data.DataBind();
                    }

                    if (Request.QueryString["RECID"] != null)
                    {
                        btnCancle.Visible = true;
                    }
                    else
                    {
                        btnCancle.Visible = false;
                    }

                    if (Request.QueryString["DELETED"] != null)
                    {
                        btnCancle.Visible = true;
                        btnSAVESDV.Visible = false;
                    }
                    else
                    {
                        btnCancle.Visible = false;
                        btnSAVESDV.Visible = true;
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

                        if (ds.Tables[0].Rows[0]["SDVSTATUS"].ToString() == "1")
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

                    if (ds.Tables[0].Rows[0]["MRSTATUS"].ToString() != "0")
                    {
                        lblPageStatus.Visible = true;
                        lblMRStatus.Text = ds.Tables[0].Rows[0]["MR_STATUSNAME"].ToString();
                        lblMedicalReviewedBy.Text = ds.Tables[0].Rows[0]["MRBYNAME"].ToString();
                        lblMEdicalDatetimeServer.Text = ds.Tables[0].Rows[0]["MR_CAL_DAT"].ToString();
                        lblMedicalDatetimeUser.Text = ds.Tables[0].Rows[0]["MR_CAL_TZDAT"].ToString();
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
                    hdn_PAGESTATUS.Value = "0";
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
                        MSG = "NSAE_MULTIPLE_DATA_ENTRY_SDV.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue;
                    }
                    if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                    {
                        MSG = "NSAE_DATA_ENTRY_SDV.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue;
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
                    ACTION: "GET_STRUCTURE",
                    MODULEID: drpModule.SelectedValue,
                    SAEID: hdnSAEID.Value
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd.DataSource = ds.Tables[0];
                    grd.DataBind();

                    lblModuleName.Text = drpModule.SelectedItem.Text;
                    hfTablename.Value = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
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

                DataSet dsData = dal_SAE.SAE_MODULE_DATA_SP(
                    MODULEID: drpModule.SelectedValue,
                    TABLENAME: hfTablename.Value,
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    SAEID: Request.QueryString["SAEID"].ToString(),
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

                        if (MEDICAL_REVIEW != "True")
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
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (MEDICAL_REVIEW != "True")
                        {
                            btnEdit.Enabled = false;
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        if (MEDICAL_REVIEW != "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        if (MEDICAL_REVIEW != "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
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

                    CheckBox chkSDV = (CheckBox)e.Row.FindControl("chkSDV");

                    if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                    {
                        chkSDV.CssClass = "chkSDV_" + VARIABLENAME + " sdvCheckbox";

                        if (dr["Critic_DP"].ToString() == "True")
                        {
                            chkSDV.CssClass = chkSDV.CssClass + " chkCriticalDP";
                            chkSDV.ToolTip = "Critical DP";
                        }
                    }

                    GridView grd_Data1 = e.Row.FindControl("grd_Data1") as GridView;
                    DataSet ds1 = dal_SAE.SAE_STRUCTURE_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
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

                        if (MEDICAL_REVIEW != "True")
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
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (MEDICAL_REVIEW != "True")
                        {
                            btnEdit.Enabled = false;
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        if (MEDICAL_REVIEW != "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        if (MEDICAL_REVIEW != "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
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

                    CheckBox chkSDV = (CheckBox)e.Row.FindControl("chkSDV");

                    if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                    {
                        chkSDV.CssClass = "chkSDV_" + VARIABLENAME + " sdvCheckbox";

                        if (dr["Critic_DP"].ToString() == "True")
                        {
                            chkSDV.CssClass = chkSDV.CssClass + " chkCriticalDP";
                            chkSDV.ToolTip = "Critical DP";
                        }
                    }

                    GridView grd_Data2 = e.Row.FindControl("grd_Data2") as GridView;
                    DataSet ds1 = dal_SAE.SAE_STRUCTURE_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
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

                        if (MEDICAL_REVIEW != "True")
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
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (MEDICAL_REVIEW != "True")
                        {
                            btnEdit.Enabled = false;
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        if (MEDICAL_REVIEW != "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        if (MEDICAL_REVIEW != "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
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

                    CheckBox chkSDV = (CheckBox)e.Row.FindControl("chkSDV");

                    if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                    {
                        chkSDV.CssClass = "chkSDV_" + VARIABLENAME + " sdvCheckbox";

                        if (dr["Critic_DP"].ToString() == "True")
                        {
                            chkSDV.CssClass = chkSDV.CssClass + " chkCriticalDP";
                            chkSDV.ToolTip = "Critical DP";
                        }
                    }

                    GridView grd_Data3 = e.Row.FindControl("grd_Data3") as GridView;
                    DataSet ds1 = dal_SAE.SAE_STRUCTURE_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
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

                        if (MEDICAL_REVIEW != "True")
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
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (MEDICAL_REVIEW != "True")
                        {
                            btnEdit.Enabled = false;
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        if (MEDICAL_REVIEW != "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

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

                        if (MEDICAL_REVIEW != "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
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

                    CheckBox chkSDV = (CheckBox)e.Row.FindControl("chkSDV");

                    if (CONTROLTYPE != "HEADER" || CONTROLTYPE != "ChildModule")
                    {
                        chkSDV.CssClass = "chkSDV_" + VARIABLENAME + " sdvCheckbox";

                        if (dr["Critic_DP"].ToString() == "True")
                        {
                            chkSDV.CssClass = chkSDV.CssClass + " chkCriticalDP";
                            chkSDV.ToolTip = "Critical DP";
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

        protected void btnSAVESDV_Click(object sender, EventArgs e)
        {
            try
            {
                InsertUpdate_SDV();

                string MSG = "NSAE_DATA_ENTRY_SDV.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue;

                Response.Write("<script> alert('Source Data Verified successfully.'); window.location.href='" + MSG + "' </script>");
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

                        if (!((CheckBox)grd_Data.Rows[rownum].FindControl("chkSDV")).Checked)
                        {
                            isSDVComplete = "2";
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

                                    if (!((CheckBox)grd_Data1.Rows[b].FindControl("chkSDV")).Checked)
                                    {
                                        isSDVComplete = "2";
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

                                        if (!((CheckBox)grd_Data2.Rows[c].FindControl("chkSDV")).Checked)
                                        {
                                            isSDVComplete = "2";
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

                                            if (!((CheckBox)grd_Data3.Rows[d].FindControl("chkSDV")).Checked)
                                            {
                                                isSDVComplete = "2";
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
                        ADD_NEW_ROW_DATA_SDV(colArr[i], hdnRECID.Value, Convert.ToBoolean(Convert.ToInt32(chkArr[i].Replace("N'", "").Replace("'", "").Trim())));

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

                dal_SAE.SAE_SDVDETAILS_SP(ACTION: "INSERT_UPDATE_SDV",
                    SAEID: hdnSAEID.Value,
                    RECID: hdnRECID.Value,
                    STATUS: hdnStatus.Value,
                    INVID: Request.QueryString["INVID"].ToString(),
                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                    MODULEID: drpModule.SelectedValue,
                    colChecked: colChecked,
                    colUnChecked: colUnChecked,
                    isSDVComplete: isSDVComplete
                    );

                if (SAE_SDV_LOGS.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "SAE_SDVDETAILS_LOGS";

                        sqlbc.ColumnMappings.Add("SAEID", "SAEID");
                        sqlbc.ColumnMappings.Add("RECID", "RECID");
                        sqlbc.ColumnMappings.Add("STATUS", "STATUS");
                        sqlbc.ColumnMappings.Add("INVID", "INVID");
                        sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                        sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                        sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                        sqlbc.ColumnMappings.Add("SDVYN", "SDVYN");
                        sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                        sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                        sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                        sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");

                        sqlbc.WriteToServer(SAE_SDV_LOGS);

                        SAE_SDV_LOGS.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnBacktoHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NSAE_MODULE_DATA_LOGS_SDV.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + Request.QueryString["SAE"].ToString() + "&STATUS=" + Request.QueryString["STATUS"].ToString() + "&SAEID=" + Request.QueryString["SAEID"].ToString(), false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ADD_NEW_ROW_DATA_SDV(string VariableName, string RECID, bool SDVYN)
        {
            try
            {
                CREATE_SDV_DT();

                DataRow myDataRow;
                myDataRow = SAE_SDV_LOGS.NewRow();
                myDataRow["SAEID"] = hdnSAEID.Value;
                myDataRow["RECID"] = RECID;
                myDataRow["STATUS"] = hdnStatus.Value;
                myDataRow["INVID"] = Request.QueryString["INVID"].ToString();
                myDataRow["SUBJID"] = Request.QueryString["SUBJID"].ToString();
                myDataRow["MODULEID"] = drpModule.SelectedValue;
                myDataRow["VARIABLENAME"] = VariableName.Trim();
                myDataRow["SDVYN"] = SDVYN;
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                SAE_SDV_LOGS.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
            }
        }

        protected void CREATE_SDV_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (SAE_SDV_LOGS.Columns.Count == 0)
                {
                    dtColumn = new DataColumn();
                    SAE_SDV_LOGS.Columns.Add("SAEID");
                    SAE_SDV_LOGS.Columns.Add("RECID");
                    SAE_SDV_LOGS.Columns.Add("STATUS");
                    SAE_SDV_LOGS.Columns.Add("INVID");
                    SAE_SDV_LOGS.Columns.Add("SUBJID");
                    SAE_SDV_LOGS.Columns.Add("MODULEID");
                    SAE_SDV_LOGS.Columns.Add("VARIABLENAME");
                    SAE_SDV_LOGS.Columns.Add("SDVYN");
                    SAE_SDV_LOGS.Columns.Add("ENTEREDBY");
                    SAE_SDV_LOGS.Columns.Add("ENTEREDBYNAME");
                    SAE_SDV_LOGS.Columns.Add("ENTEREDDAT");
                    SAE_SDV_LOGS.Columns.Add("ENTERED_TZVAL");
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

        protected string REMOVEHTML(string str)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");

            return str;
        }

        public void SAE_GetAuditDetails()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_AUDITTRAIL_SP
                      (
                      ACTION: "GET_AUDITTRAIL_SAEID_RECID",
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

        public void SAE_GetSDVDetails()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SDVDETAILS_SP(ACTION: "GET_SDV_DATA_COUNT",
                   SAEID: hdnSAEID.Value,
                   RECID: hdnRECID.Value,
                   MODULEID: drpModule.SelectedValue,
                   STATUS: hdnStatus.Value
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grSDVDETAILS.DataSource = ds;
                    grSDVDETAILS.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("NSAE_DATA_ENTRY_SDV.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue);
        }

        protected void GetDataExists()
        {
            try
            {
                DataSet ds = new DataSet();

                if (Request.QueryString["RECID"] == null)
                {
                    ds = dal_SAE.SAE_SINGLE_RECORS_SP(
                            ACTION: "CHECK_DATA_EXISTS",
                            SAEID: hdnSAEID.Value,
                            MODULEID: Request.QueryString["MODULEID"].ToString(),
                            SUBJID: Request.QueryString["SUBJID"].ToString(),
                            TABLENAME: hfTablename.Value
                          );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["RECID"].ToString() != "")
                        {
                            hdnRECID.Value = ds.Tables[0].Rows[0]["RECID"].ToString();
                            GetRecords(grd_Data);
                        }
                    }
                    else
                    {
                        hdnRECID.Value = "-1";
                    }
                }
                else
                {
                    hdnRECID.Value = Request.QueryString["RECID"].ToString();

                    GetRecords(grd_Data);
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
                DataSet ds = new DataSet();

                ds = dal_SAE.SAE_SINGLE_RECORS_SP(
                         ACTION: "CHECK_DATA_EXISTS_Deleted",
                         SAEID: hdnSAEID.Value,
                         MODULEID: Request.QueryString["MODULEID"].ToString(),
                         SUBJID: Request.QueryString["SUBJID"].ToString(),
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
            }
        }

        protected void lnkPAGENUMDeleted_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label REC_ID = (Label)row.FindControl("lblRECID");

                Response.Redirect("NSAE_DATA_ENTRY_SDV.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&SAE=" + hdnSAE.Value + "&STATUS=" + hdnStatus.Value + "&SAEID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue + "&RECID=" + REC_ID.Text + "&DELETED=1");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
            }
        }
    }
}