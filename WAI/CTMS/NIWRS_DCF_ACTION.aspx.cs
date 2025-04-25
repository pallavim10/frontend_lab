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
    public partial class NIWRS_DCF_ACTION : System.Web.UI.Page
    {
        DataTable dt_AuditTrail = new DataTable("NIWRS_AUDITTRAIL");
        DataTable dt_DM_AuditTrail = new DataTable("DM_AUDITTRAIL");

        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtComment.Attributes.Add("MaxLength", "200");
            try
            {
                if (!IsPostBack)
                {
                    SUBJECTTEXT.Text = Session["SUBJECTTEXT"].ToString();
                    GET_DATA();
                    GET_USER_LIST();
                    GET_SUBJECT_KITS();
                    GET_KITS();
                    GET_STATUS();
                    GET_RANDNUMBERs();
                    GET_VISIT_DATES();
                    GET_VISITS();
                    GET_SUBJECT_VISIT_DATES_MASTER();
                    GET_ADDITIONAL_FIELDS();
                    GET_MODULES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DATA()
        {
            try
            {
                DataSet dsMainData = dal_IWRS.IWRS_DCF_SP(ACTION: "SELECT_DCF_ACTION", ID: Request.QueryString["ID"].ToString());

                DataRow drMainData = dsMainData.Tables[0].Rows[0];

                lblID.Text = drMainData["ID"].ToString();
                lblSite.Text = drMainData["SITE"].ToString();
                lblSubSite.Text = drMainData["SUBSITE"].ToString();
                lblSubject.Text = drMainData["SUBJID"].ToString();
                hfSUBJID.Value = drMainData["SUBJID"].ToString();
                lblVisit.Text = drMainData["VISITNAME"].ToString();
                lblForm.Text = drMainData["FORM"].ToString();
                lblField.Text = drMainData["FIELDNAME"].ToString();
                lblDesc.Text = drMainData["DESCRIPTION"].ToString();
                lblReason.Text = drMainData["REASON"].ToString();

                DataSet ds = dal_IWRS.IWRS_STRUCTURE_SP(ACTION: "GET_STRUCTURE_DCF", ID: drMainData["FIELD"].ToString());

                TXT_FIELD_new.Visible = false;
                TXT_FIELD_old.Visible = false;

                DRP_FIELD_new.Visible = false;
                DRP_FIELD_old.Visible = false;

                repeat_CHK_new.Visible = false;
                repeat_CHK_old.Visible = false;

                repeat_RAD_new.Visible = false;
                repeat_RAD_old.Visible = false;

                TXT_FIELD_old.Enabled = false;
                TXT_FIELD_new.Enabled = false;

                DRP_FIELD_old.Enabled = false;
                DRP_FIELD_new.Enabled = false;

                TXT_FIELD_new.Text = "";
                TXT_FIELD_old.Text = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    divData.Visible = true;

                    DataRow dr = ds.Tables[0].Rows[0];

                    string ID = dr["ID"].ToString();
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();
                    string MANDATORY = dr["MANDATORY"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

                    string oldDATA = drMainData["OLD_Val"].ToString();
                    string newDATA = drMainData["NEW_Val"].ToString();

                    hfControlType.Value = CONTROLTYPE;

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TXT_FIELD_old.Text = oldDATA;
                        TXT_FIELD_new.Text = newDATA;

                        TXT_FIELD_new.Visible = true;
                        TXT_FIELD_old.Visible = true;

                        TXT_FIELD_new.CssClass = CLASS;
                        TXT_FIELD_old.CssClass = CLASS;

                        if (MAXLEN != "" && MAXLEN != "0")
                        {
                            TXT_FIELD_new.MaxLength = Convert.ToInt32(MAXLEN);
                            TXT_FIELD_old.MaxLength = Convert.ToInt32(MAXLEN);
                        }
                        if (READYN == "True")
                        {
                            TXT_FIELD_new.ReadOnly = true;
                            TXT_FIELD_old.ReadOnly = true;
                        }
                        if (MULTILINEYN == "True")
                        {
                            TXT_FIELD_new.TextMode = TextBoxMode.MultiLine;
                            TXT_FIELD_new.Attributes.Add("style", "width: 300px;");

                            TXT_FIELD_old.TextMode = TextBoxMode.MultiLine;
                            TXT_FIELD_old.Attributes.Add("style", "width: 300px;");
                        }
                        if (CLASS.Contains("txtTime"))
                        {
                            TXT_FIELD_new.Attributes.Add("onchange", "ValidTime(this)");
                            TXT_FIELD_old.Attributes.Add("onchange", "ValidTime(this)");
                        }
                        if (UPPERCASE == "True")
                        {
                            TXT_FIELD_new.CssClass = TXT_FIELD_new.CssClass + " txtuppercase";
                            TXT_FIELD_old.CssClass = TXT_FIELD_old.CssClass + " txtuppercase";
                        }
                        if (AnsColor != "")
                        {
                            TXT_FIELD_new.Style.Add("color", AnsColor);
                            TXT_FIELD_old.Style.Add("color", AnsColor);
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            TXT_FIELD_new.Text = dr["PrefixText"].ToString();
                            TXT_FIELD_old.Text = dr["PrefixText"].ToString();
                        }

                        if (MANDATORY == "True")
                        {
                            TXT_FIELD_new.CssClass = TXT_FIELD_new.CssClass + " Mandatory";
                            TXT_FIELD_old.CssClass = TXT_FIELD_old.CssClass + " Mandatory";
                        }

                        TXT_FIELD_old.Enabled = false;
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DRP_FIELD_old.SelectedValue = oldDATA;
                        DRP_FIELD_new.SelectedValue = newDATA;

                        DRP_FIELD_new.Visible = true;
                        DRP_FIELD_new.CssClass = CLASS;

                        DRP_FIELD_old.Visible = true;
                        DRP_FIELD_old.CssClass = CLASS;

                        DataSet dsDRP = new DataSet();
                        dsDRP = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        DRP_FIELD_new.DataSource = dsDRP;
                        DRP_FIELD_new.DataTextField = "TEXT";
                        DRP_FIELD_new.DataValueField = "VALUE";
                        DRP_FIELD_new.DataBind();

                        DRP_FIELD_old.DataSource = dsDRP;
                        DRP_FIELD_old.DataTextField = "TEXT";
                        DRP_FIELD_old.DataValueField = "VALUE";
                        DRP_FIELD_old.DataBind();

                        if (AnsColor != "")
                        {
                            DRP_FIELD_new.Style.Add("color", AnsColor);
                            DRP_FIELD_old.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            DRP_FIELD_new.CssClass = DRP_FIELD_new.CssClass + " Mandatory";
                            DRP_FIELD_old.CssClass = DRP_FIELD_old.CssClass + " Mandatory";
                        }

                        DRP_FIELD_old.Enabled = false;
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        repeat_CHK_new.Visible = true;
                        repeat_CHK_old.Visible = true;

                        DataSet dsDRP = new DataSet();

                        dsDRP = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        dsDRP.Tables[0].Columns.Add(newColumn);

                        repeat_CHK_new.DataSource = dsDRP;
                        repeat_CHK_new.DataBind();

                        repeat_CHK_old.DataSource = dsDRP;
                        repeat_CHK_old.DataBind();

                        string[] stringArrayOLD = oldDATA.Split(',');
                        foreach (string x in stringArrayOLD)
                        {
                            for (int ab = 0; ab < repeat_CHK_old.Items.Count; ab++)
                            {
                                if (x != "")
                                {
                                    if (((CheckBox)repeat_CHK_old.Items[ab].FindControl("CHK_FIELD_old")).Text.ToString() == x)
                                    {
                                        ((CheckBox)repeat_CHK_old.Items[ab].FindControl("CHK_FIELD_old")).Checked = true;
                                    }

                                    ((CheckBox)repeat_CHK_old.Items[ab].FindControl("CHK_FIELD_old")).Enabled = false;
                                }
                            }
                        }

                        string[] stringArrayNEW = newDATA.Split(',');
                        foreach (string x in stringArrayNEW)
                        {
                            for (int ab = 0; ab < repeat_CHK_new.Items.Count; ab++)
                            {
                                if (x != "")
                                {
                                    if (((CheckBox)repeat_CHK_new.Items[ab].FindControl("CHK_FIELD_new")).Text.ToString() == x)
                                    {
                                        ((CheckBox)repeat_CHK_new.Items[ab].FindControl("CHK_FIELD_new")).Checked = true;
                                    }

                                    ((CheckBox)repeat_CHK_new.Items[ab].FindControl("CHK_FIELD_new")).Enabled = false;
                                }
                            }
                        }

                    }
                    if (CONTROLTYPE == "RADIOBUTTON")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        repeat_RAD_new.Visible = true;
                        repeat_RAD_old.Visible = true;

                        DataSet dsDRP = new DataSet();

                        dsDRP = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        dsDRP.Tables[0].Columns.Add(newColumn);

                        repeat_RAD_new.DataSource = dsDRP;
                        repeat_RAD_new.DataBind();

                        repeat_RAD_old.DataSource = dsDRP;
                        repeat_RAD_old.DataBind();

                        string[] stringArrayOLD = oldDATA.Split(',');
                        foreach (string x in stringArrayOLD)
                        {
                            for (int ab = 0; ab < repeat_RAD_old.Items.Count; ab++)
                            {
                                if (x != "")
                                {
                                    if (((CheckBox)repeat_RAD_old.Items[ab].FindControl("RAD_FIELD_old")).Text.ToString() == x)
                                    {
                                        ((CheckBox)repeat_RAD_old.Items[ab].FindControl("RAD_FIELD_old")).Checked = true;
                                    }

                                    ((CheckBox)repeat_RAD_old.Items[ab].FindControl("RAD_FIELD_old")).Enabled = false;
                                }
                            }
                        }

                        string[] stringArrayNEW = newDATA.Split(',');
                        foreach (string x in stringArrayNEW)
                        {
                            for (int ab = 0; ab < repeat_RAD_new.Items.Count; ab++)
                            {
                                if (x != "")
                                {
                                    if (((CheckBox)repeat_RAD_new.Items[ab].FindControl("RAD_FIELD_new")).Text.ToString() == x)
                                    {
                                        ((CheckBox)repeat_RAD_new.Items[ab].FindControl("RAD_FIELD_new")).Checked = true;
                                    }

                                    ((CheckBox)repeat_RAD_new.Items[ab].FindControl("RAD_FIELD_new")).Enabled = false;
                                }
                            }
                        }

                    }

                }
                else
                {
                    divData.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtComment.Text.Trim() == "")
                {
                    Response.Write("<script> alert('Please Enter Comment.');</script>");
                }
                else
                {
                    dal_IWRS.IWRS_DCF_SP(ACTION: "UPDATE_DCF_ACTION",
                    STATUS: ddlAction.SelectedValue,
                    ACTION_Comments: txtComment.Text,
                    ACTION_BY: Session["User_ID"].ToString(),
                    ID: Request.QueryString["ID"].ToString()
                    );
                    string DCFID = Request.QueryString["ID"].ToString();
                    if (ddlAction.SelectedValue == "Approve")
                    {
                        string STRATA = "Data Change Request Approved";
                        SEND_MAIL(STRATA, DCFID);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('DCF Request Approved Successfully.');  window.location.href = 'NIWRS_DCF_LIST.aspx';", true);

                    }

                    if (ddlAction.SelectedValue == "Disapprove")
                    {
                        string STRATA = "Data Change Request Disapproved";
                        SEND_MAIL(STRATA, DCFID);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('DCF Request Disapproved Successfully.');  window.location.href = 'NIWRS_DCF_LIST.aspx';", true);

                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL(string STRATA, string DCFID)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";

                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_DCF_EMAILS_BYSITEID", STRATA: "DCF", SITEID: lblSite.Text);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: STRATA);

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[SITEID]", lblSite.Text);
                    SUBJECT = SUBJECT.Replace("[SCREENINGID]", lblSubject.Text);
                    SUBJECT = SUBJECT.Replace("[COMMENT]", txtComment.Text);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[SITEID]", lblSite.Text);
                    BODY = BODY.Replace("[DCFID]", DCFID);
                    BODY = BODY.Replace("[SCREENINGID]", lblSubject.Text);
                    BODY = BODY.Replace("[COMMENT]", txtComment.Text);
                    BODY = BODY.Replace("[USERNAME]", Session["User_Name"].ToString());
                    BODY = BODY.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    BODY = BODY.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());
                }
                cf.Email_Users(
                EmailAddress: EMAILIDS,
                CCEmailAddress: CCEMAILIDS,
                BCCEmailAddress: BCCEMAILIDS,
                subject: SUBJECT,
                body: BODY
                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAction.SelectedValue == "Take Action")
                {
                    //string URL = "NIWRS_DCF_ACTION.aspx?ID=" + Request.QueryString["ID"].ToString();
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + URL + "','_newtab');", true);

                    DivAction.Visible = true;
                }
                else
                {
                    DivAction.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
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

        private void GET_USER_LIST()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_USER_LIST");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlKitActUser.DataSource = ds;
                        ddlKitActUser.DataTextField = "User_Name";
                        ddlKitActUser.DataValueField = "USER_ID";
                        ddlKitActUser.DataBind();
                        ddlKitActUser.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBJECT_KITS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_SUBJECT_KITS_DCF", SITEID: lblSite.Text, SUBJID: lblSubject.Text);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvKitSubject.DataSource = ds;
                        gvKitSubject.DataBind();
                    }
                    else
                    {
                        gvKitSubject.DataSource = null;
                        gvKitSubject.DataBind();
                    }
                }
                else
                {
                    gvKitSubject.DataSource = null;
                    gvKitSubject.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_KITS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_KITS_SITE_DCF", SITEID: lblSite.Text, SUBJID: lblSubject.Text);
                ddlKitNo.DataSource = ds;
                ddlKitNo.DataTextField = "KITNO";
                ddlKitNo.DataValueField = "KITNO";
                ddlKitNo.DataBind();
                ddlKitNo.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_STATUS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_STATUS");
                ddlStatus.DataSource = ds;
                ddlStatus.DataTextField = "STATUSNAME";
                ddlStatus.DataValueField = "STATUSCODE";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_RANDNUMBERs()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_RANDNUMBERs");
                drpRandNo.DataSource = ds;
                drpRandNo.DataTextField = "RANDNO";
                drpRandNo.DataValueField = "RANDNO";
                drpRandNo.DataBind();
                drpRandNo.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_VISITS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_VISITS");

                ddlKitVisit.DataSource = ds;
                ddlKitVisit.DataTextField = "VISIT";
                ddlKitVisit.DataValueField = "VISITNUM";
                ddlKitVisit.DataBind();
                ddlKitVisit.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlNextVisit.DataSource = ds;
                ddlNextVisit.DataTextField = "VISIT";
                ddlNextVisit.DataValueField = "VISIT";
                ddlNextVisit.DataBind();
                ddlNextVisit.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlLastVisit.DataSource = ds;
                ddlLastVisit.DataTextField = "VISIT";
                ddlLastVisit.DataValueField = "VISIT";
                ddlLastVisit.DataBind();
                ddlLastVisit.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_VISIT_DATES()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_VISIT_DATES", SUBJID: lblSubject.Text);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdVisitDates.DataSource = ds;
                        grdVisitDates.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBJECT_VISIT_DATES_MASTER()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_SUBJECT_VISIT_DATES_MASTER", SUBJID: lblSubject.Text);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlLastVisit.SelectedValue = ds.Tables[0].Rows[0]["LAST_VISIT"].ToString();
                        txtLastVisitDate.Text = ds.Tables[0].Rows[0]["LAST_VISIT_DATE"].ToString();
                        ddlNextVisit.SelectedValue = ds.Tables[0].Rows[0]["NEXT_VISIT"].ToString();
                        txtNextVisitDate.Text = ds.Tables[0].Rows[0]["NEXT_VISIT_DATE"].ToString();
                        ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS"].ToString();
                        drpRandNo.SelectedValue = ds.Tables[0].Rows[0]["RAND_NUM"].ToString();
                        txtEarlyDate.Text = ds.Tables[0].Rows[0]["EARLY_DATE"].ToString();
                        txtLateDate.Text = ds.Tables[0].Rows[0]["LATE_DATE"].ToString();

                        hdnLastVisitOld.Value = ddlLastVisit.SelectedItem.Text;
                        hdnLastVisitDateOld.Value = txtLastVisitDate.Text;
                        hdnNextVisitOld.Value = ddlNextVisit.SelectedItem.Text;
                        hdnNextVisitDateOld.Value = txtNextVisitDate.Text;
                        hdnStatusOld.Value = ddlStatus.SelectedItem.Text;
                        hdnRandNoOld.Value = drpRandNo.SelectedItem.Text;
                        hdnEarlyDateOld.Value = txtEarlyDate.Text;
                        hdnLateDateOld.Value = txtLateDate.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ADDITIONAL_FIELDS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_ADDITIONAL_FIELDS", SUBJID: lblSubject.Text);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("DATA", typeof(string));

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            row["DATA"] = ds.Tables[1].Rows[0][row["COL_NAME"].ToString()].ToString();
                        }
                    }

                    repeatAdditionalFields.DataSource = ds.Tables[0];
                    repeatAdditionalFields.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlKitStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlKitStatus.SelectedValue != "Dispensed")
                {
                    DivKitReason.Visible = true;
                    txtKitReason.CssClass = "form-control";
                }
                else
                {
                    DivKitReason.Visible = false;
                    txtKitReason.CssClass = "form-control required1";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlKitNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnKitnoOld.Value = ddlKitNo.SelectedItem.Text;

                ddlKitStatus.SelectedIndex = 0;
                ddlKitVisit.SelectedIndex = 0;

                txtKitReason.CssClass = "form-control";

                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_KIT_DETAILS", SITEID: lblSite.Text, SUBJID: lblSubject.Text, KITNO: ddlKitNo.SelectedValue);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["VISITNUM"].ToString() != "" && ds.Tables[0].Rows[0]["VISITNUM"].ToString() != "0")
                        {
                            ddlKitVisit.SelectedValue = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                            hdnKitVisitOld.Value = ddlKitVisit.SelectedItem.Text;
                        }

                        if (ds.Tables[0].Rows[0]["BLOCKED"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Blocked";
                        }
                        else if (ds.Tables[0].Rows[0]["DAMAGED"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Damaged";
                            hdnKitReasonOld.Value = ds.Tables[0].Rows[0]["DAMAGE_REASON"].ToString();
                        }
                        else if (ds.Tables[0].Rows[0]["QUARANTINE"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Quarantined";
                        }
                        else if (ds.Tables[0].Rows[0]["RETURNED"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Returned";
                        }
                        else if (ds.Tables[0].Rows[0]["DESTROY"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Destroyed";
                        }
                        else if (ds.Tables[0].Rows[0]["DISPENSE"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Dispensed";
                        }
                        else if (ds.Tables[0].Rows[0]["REJECT"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Rejected";
                        }

                        hdnKitStatusOld.Value = ddlKitStatus.SelectedItem.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvKitSubject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string KITNO = e.CommandArgument.ToString();

                DivKitReason.Visible = false;
                ddlKitStatus.SelectedIndex = 0;

                ddlKitNo.SelectedValue = KITNO;
                hdnKitnoOld.Value = ddlKitNo.SelectedItem.Text;

                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_KIT_DETAILS", SITEID: lblSite.Text, SUBJID: lblSubject.Text, KITNO: KITNO);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlKitVisit.SelectedValue = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                        hdnKitVisitOld.Value = ds.Tables[0].Rows[0]["VISIT"].ToString();

                        hdnKitStatusOld.Value = ddlKitStatus.SelectedItem.Text;

                        if (ds.Tables[0].Rows[0]["BLOCKED"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Blocked";
                        }
                        else if (ds.Tables[0].Rows[0]["DAMAGED"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Damaged";
                            hdnKitReasonOld.Value = ds.Tables[0].Rows[0]["DAMAGE_REASON"].ToString();
                            DivKitReason.Visible = true;
                        }
                        else if (ds.Tables[0].Rows[0]["QUARANTINE"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Quarantined";
                        }
                        else if (ds.Tables[0].Rows[0]["RETURNED"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Returned";
                        }
                        else if (ds.Tables[0].Rows[0]["DESTROY"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Destroyed";
                        }
                        else if (ds.Tables[0].Rows[0]["DISPENSE"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Dispensed";
                        }
                        else if (ds.Tables[0].Rows[0]["REJECT"].ToString() == "True")
                        {
                            ddlKitStatus.SelectedValue = "Rejected";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitKit_Click(object sender, EventArgs e)
        {
            try
            {
                string KIT_STATUS = ddlKitStatus.SelectedValue;

                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "ACTION_KIT",
                KITNO: ddlKitNo.SelectedValue,
                STATUS: KIT_STATUS,
                ACTION_Comments: txtKitReason.Text,
                SUBJID: lblSubject.Text,
                SITEID: lblSite.Text,
                VISIT: ddlKitVisit.SelectedItem.Text,
                VISITNUM: ddlKitVisit.SelectedValue,
                KITACTBY: ddlKitActUser.SelectedValue,
                KITACTDAT: txtKitActDate.Text
                );

                INSERT_KIT_AUDIT_TRAIL(KIT_STATUS);

                UPLOAD_IWRS_AUDITTRAIL();

                Response.Write("<script> alert('Kit Status Updated Successfully.');</script>");

                GET_SUBJECT_KITS();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_KIT_AUDIT_TRAIL(string KIT_STATUS)
        {
            try
            {
                if (ddlKitNo.SelectedItem.Text != hdnKitnoOld.Value)
                {
                    IWRS_ADD_NEW_ROW_DATA(
                        DCFID: Request.QueryString["ID"].ToString(),
                        REASON: "Kit number updated",
                        SUBJID: lblSubject.Text,
                        MODULENAME: "Kit Management",
                        TABLENAME: "Kit Management",
                        FIELDNAME: "Kit Number",
                        VARIABLENAME: "KITNO",
                        OLDVALUE: hdnKitnoOld.Value,
                        NEWVALUE: ddlKitNo.SelectedItem.Text
                        );
                }

                if (ddlKitStatus.SelectedItem.Text != hdnKitStatusOld.Value)
                {
                    IWRS_ADD_NEW_ROW_DATA(
                           DCFID: Request.QueryString["ID"].ToString(),
                           REASON: "Kit no. " + ddlKitNo.SelectedValue + " - Status updated ",
                           SUBJID: lblSubject.Text,
                           MODULENAME: "Kit Management",
                           TABLENAME: "Kit Management",
                           FIELDNAME: "Kit Status",
                           VARIABLENAME: "STATUS",
                           OLDVALUE: hdnKitStatusOld.Value,
                           NEWVALUE: ddlKitStatus.SelectedItem.Text
                           );
                }

                if (ddlKitVisit.SelectedItem.Text != hdnKitVisitOld.Value)
                {
                    IWRS_ADD_NEW_ROW_DATA(
                           DCFID: Request.QueryString["ID"].ToString(),
                           REASON: "Kit no. " + ddlKitNo.SelectedValue + " - Visit updated ",
                           SUBJID: lblSubject.Text,
                           MODULENAME: "Kit Management",
                           TABLENAME: "Kit Management",
                           FIELDNAME: "Kit Visit",
                           VARIABLENAME: "VISIT",
                           OLDVALUE: hdnKitVisitOld.Value,
                           NEWVALUE: ddlKitVisit.SelectedItem.Text
                           );
                }

                if (ddlKitActUser.SelectedItem.Text != hdnActUserOld.Value)
                {
                    IWRS_ADD_NEW_ROW_DATA(
                           DCFID: Request.QueryString["ID"].ToString(),
                           REASON: "Kit no. " + ddlKitNo.SelectedValue + " - " + KIT_STATUS + " by user updated ",
                           SUBJID: lblSubject.Text,
                           MODULENAME: "Kit Management",
                           TABLENAME: "Kit Management",
                           FIELDNAME: "Kit User",
                           VARIABLENAME: "USERNAME",
                           OLDVALUE: ddlKitActUser.SelectedItem.Text,
                           NEWVALUE: hdnActUserOld.Value
                           );
                }

                if (txtKitReason.Text != hdnKitReasonOld.Value)
                {
                    IWRS_ADD_NEW_ROW_DATA(
                           DCFID: Request.QueryString["ID"].ToString(),
                           REASON: "Kit no. " + ddlKitNo.SelectedValue + " - Reason for Damage ",
                           SUBJID: lblSubject.Text,
                           MODULENAME: "Kit Management",
                           TABLENAME: "Kit Management",
                           FIELDNAME: "Kit Damage Reason",
                           VARIABLENAME: "DAMAGEREASN",
                           OLDVALUE: txtKitReason.Text,
                           NEWVALUE: hdnKitReasonOld.Value
                           );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitSubjectMaster_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdVisitDates.Rows.Count; i++)
                {
                    string SUBJID = ((Label)grdVisitDates.Rows[i].FindControl("SUBJID")).Text;
                    string Visit = ((Label)grdVisitDates.Rows[i].FindControl("VISIT")).Text;
                    string txtVISIT_DATE = ((TextBox)grdVisitDates.Rows[i].FindControl("txtVISIT_DATE")).Text;
                    string txtEARLY_DATE = ((TextBox)grdVisitDates.Rows[i].FindControl("txtEARLY_DATE")).Text;
                    string txtLATE_DATE = ((TextBox)grdVisitDates.Rows[i].FindControl("txtLATE_DATE")).Text;
                    string txtACTUAL_VISIT_DATE = ((TextBox)grdVisitDates.Rows[i].FindControl("txtACTUAL_VISIT_DATE")).Text;
                    HiddenField hdnVISIT_DATEOld = (HiddenField)grdVisitDates.Rows[i].FindControl("hdnVISIT_DATEOld");
                    HiddenField hdnEARLY_DATEOld = (HiddenField)grdVisitDates.Rows[i].FindControl("hdnEARLY_DATEOld");
                    HiddenField hdnLATE_DATEOld = (HiddenField)grdVisitDates.Rows[i].FindControl("hdnLATE_DATEOld");
                    HiddenField hdnACTUAL_VISIT_DATEOld = (HiddenField)grdVisitDates.Rows[i].FindControl("hdnACTUAL_VISIT_DATEOld");

                    dal_IWRS.IWRS_DCF_SP(ACTION: "UPDATE_VISIT_DATES",
                        SUBJID: SUBJID,
                        VISIT: Visit,
                        VISIT_DATE: txtVISIT_DATE,
                        EARLY_DATE: txtEARLY_DATE,
                        LATE_DATE: txtLATE_DATE,
                        ACTUAL_VISIT_DATE: txtACTUAL_VISIT_DATE
                        );

                    if (txtVISIT_DATE != hdnVISIT_DATEOld.Value)
                    {
                        IWRS_ADD_NEW_ROW_DATA(
                            DCFID: Request.QueryString["ID"].ToString(),
                            REASON: Visit + " - Next Visit date updated",
                            SUBJID: lblSubject.Text,
                            MODULENAME: "Visit Dates",
                            TABLENAME: "VISIT DATES",
                            FIELDNAME: "Next Visit Date",
                            VARIABLENAME: "NEXT_VISDAT",
                            OLDVALUE: hdnVISIT_DATEOld.Value,
                            NEWVALUE: txtVISIT_DATE
                            );
                    }

                    if (txtEARLY_DATE != hdnEARLY_DATEOld.Value)
                    {
                        IWRS_ADD_NEW_ROW_DATA(
                            DCFID: Request.QueryString["ID"].ToString(),
                            REASON: Visit + " - Early Visit date updated",
                            SUBJID: lblSubject.Text,
                            MODULENAME: "Visit Dates",
                            TABLENAME: "VISIT DATES",
                            FIELDNAME: "Early Visit Date",
                            VARIABLENAME: "EARLY_VISDAT",
                            OLDVALUE: hdnEARLY_DATEOld.Value,
                            NEWVALUE: txtEARLY_DATE
                            );
                    }

                    if (txtLATE_DATE != hdnLATE_DATEOld.Value)
                    {
                        IWRS_ADD_NEW_ROW_DATA(
                            DCFID: Request.QueryString["ID"].ToString(),
                            REASON: Visit + " - Late Visit date updated",
                            SUBJID: lblSubject.Text,
                            MODULENAME: "Visit Dates",
                            TABLENAME: "VISIT DATES",
                            FIELDNAME: "Late Visit Date",
                            VARIABLENAME: "LATE_VISDAT",
                            OLDVALUE: hdnEARLY_DATEOld.Value,
                            NEWVALUE: txtEARLY_DATE
                            );
                    }

                    if (txtACTUAL_VISIT_DATE != hdnACTUAL_VISIT_DATEOld.Value)
                    {
                        IWRS_ADD_NEW_ROW_DATA(
                            DCFID: Request.QueryString["ID"].ToString(),
                            REASON: Visit + " - Actual Visit date updated",
                            SUBJID: lblSubject.Text,
                            MODULENAME: "Visit Dates",
                            TABLENAME: "VISIT DATES",
                            FIELDNAME: "Actual Visit Date",
                            VARIABLENAME: "VISDAT",
                            OLDVALUE: hdnEARLY_DATEOld.Value,
                            NEWVALUE: txtEARLY_DATE
                            );
                    }
                }

                string RANDNO = "";
                if (drpRandNo.SelectedValue == "0")
                {
                    RANDNO = "";
                }
                else
                {
                    RANDNO = drpRandNo.SelectedValue;
                }

                INSERT_VISIT_DAT_AUDIT_TRAIL();

                dal_IWRS.IWRS_DCF_SP(ACTION: "UPDATE_VISIT_DATES_SUBJECT_MASTER",
                    STATUS: ddlStatus.SelectedValue,
                    NEXT_VISIT: ddlNextVisit.SelectedValue,
                    NEXT_VISIT_DATE: txtNextVisitDate.Text,
                    LAST_VISIT: ddlLastVisit.SelectedValue,
                    LAST_VISIT_DATE: txtLastVisitDate.Text,
                    EARLY_DATE: txtEarlyDate.Text,
                    LATE_DATE: txtLateDate.Text,
                    SUBJID: lblSubject.Text,
                    RAND_NUM: RANDNO
                    );

                if (repeatAdditionalFields.Items.Count > 0)
                {
                    foreach (RepeaterItem item in repeatAdditionalFields.Items)
                    {
                        Label lblFIELDNAME = (Label)item.FindControl("lblFIELDNAME");
                        TextBox txtDATA = (TextBox)item.FindControl("txtDATA");
                        HiddenField hdnOldDATA = (HiddenField)item.FindControl("hdnOldDATA");
                        HiddenField hdnVARIABLENAME = (HiddenField)item.FindControl("hdnVARIABLENAME");

                        if (txtDATA.Text != hdnOldDATA.Value)
                        {
                            dal_IWRS.IWRS_DCF_SP(ACTION: "UPDATE_ADDITIONAL_FIELDS",
                                VARIABLENAME: hdnVARIABLENAME.Value,
                                SUBJID: lblSubject.Text,
                                NEW_VALUE: txtDATA.Text
                                );

                            IWRS_ADD_NEW_ROW_DATA(
                            DCFID: Request.QueryString["ID"].ToString(),
                            REASON: lblFIELDNAME.Text + " - Additional field updated",
                            SUBJID: lblSubject.Text,
                            MODULENAME: "Additional Fields",
                            TABLENAME: "SUBJECT MASTER",
                            FIELDNAME: lblFIELDNAME.Text,
                            VARIABLENAME: hdnVARIABLENAME.Value,
                            OLDVALUE: hdnOldDATA.Value,
                            NEWVALUE: txtDATA.Text
                            );
                        }

                    }
                }

                Response.Write("<script> alert('Subject Master Updated Successfully.');</script>");
                Response.Redirect("NIWRS_DCF_ACTION.aspx?ID=" + Request.QueryString["ID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_VISIT_DAT_AUDIT_TRAIL()
        {
            try
            {
                if (ddlStatus.SelectedItem.Text != hdnStatusOld.Value)
                {
                    dal_IWRS.IWRS_DCF_SP(ACTION: "INSERT_AUDIT_TRAIL",
                        OLD_VALUE: hdnStatusOld.Value,
                        NEW_VALUE: ddlStatus.SelectedItem.Text,
                        FIELD: "Subject Status",
                        ACTION_BY: Session["User_ID"].ToString(),
                        ID: Request.QueryString["ID"].ToString(),
                        SUBJID: lblSubject.Text
                        );
                }

                if (ddlLastVisit.SelectedItem.Text != hdnLastVisitOld.Value)
                {
                    dal_IWRS.IWRS_DCF_SP(ACTION: "INSERT_AUDIT_TRAIL",
                        OLD_VALUE: hdnLastVisitOld.Value,
                        NEW_VALUE: ddlLastVisit.SelectedItem.Text,
                        FIELD: "Last Visit",
                        ACTION_BY: Session["User_ID"].ToString(),
                        ID: Request.QueryString["ID"].ToString(),
                        SUBJID: lblSubject.Text
                        );
                }

                if (txtLastVisitDate.Text != hdnLastVisitDateOld.Value)
                {
                    dal_IWRS.IWRS_DCF_SP(ACTION: "INSERT_AUDIT_TRAIL",
                        OLD_VALUE: hdnLastVisitDateOld.Value,
                        NEW_VALUE: txtLastVisitDate.Text,
                        FIELD: "Last Visit Date",
                        ACTION_BY: Session["User_ID"].ToString(),
                        ID: Request.QueryString["ID"].ToString(),
                        SUBJID: lblSubject.Text
                        );
                }

                if (ddlNextVisit.SelectedItem.Text != hdnNextVisitOld.Value)
                {
                    dal_IWRS.IWRS_DCF_SP(ACTION: "INSERT_AUDIT_TRAIL",
                        OLD_VALUE: hdnNextVisitOld.Value,
                        NEW_VALUE: ddlNextVisit.SelectedItem.Text,
                        FIELD: "Next Visit",
                        ACTION_BY: Session["User_ID"].ToString(),
                        ID: Request.QueryString["ID"].ToString(),
                        SUBJID: lblSubject.Text
                        );
                }

                if (txtNextVisitDate.Text != hdnNextVisitDateOld.Value)
                {
                    dal_IWRS.IWRS_DCF_SP(ACTION: "INSERT_AUDIT_TRAIL",
                        OLD_VALUE: hdnNextVisitDateOld.Value,
                        NEW_VALUE: txtNextVisitDate.Text,
                        FIELD: "Next Visit Date",
                        ACTION_BY: Session["User_ID"].ToString(),
                        ID: Request.QueryString["ID"].ToString(),
                        SUBJID: lblSubject.Text
                        );
                }

                if (txtEarlyDate.Text != hdnEarlyDateOld.Value)
                {
                    dal_IWRS.IWRS_DCF_SP(ACTION: "INSERT_AUDIT_TRAIL",
                        OLD_VALUE: hdnEarlyDateOld.Value,
                        NEW_VALUE: txtEarlyDate.Text,
                        FIELD: "Early Visit Date",
                        ACTION_BY: Session["User_ID"].ToString(),
                        ID: Request.QueryString["ID"].ToString(),
                        SUBJID: lblSubject.Text
                        );
                }

                if (txtLateDate.Text != hdnLateDateOld.Value)
                {
                    dal_IWRS.IWRS_DCF_SP(ACTION: "INSERT_AUDIT_TRAIL",
                        OLD_VALUE: hdnLateDateOld.Value,
                        NEW_VALUE: txtLateDate.Text,
                        FIELD: "Late Visit Date",
                        ACTION_BY: Session["User_ID"].ToString(),
                        ID: Request.QueryString["ID"].ToString(),
                        SUBJID: lblSubject.Text
                        );
                }

                if (drpRandNo.SelectedItem.Text != hdnRandNoOld.Value)
                {
                    dal_IWRS.IWRS_DCF_SP(ACTION: "INSERT_AUDIT_TRAIL",
                        OLD_VALUE: hdnRandNoOld.Value,
                        NEW_VALUE: drpRandNo.SelectedItem.Text,
                        FIELD: "Rand No",
                        ACTION_BY: Session["User_ID"].ToString(),
                        ID: Request.QueryString["ID"].ToString(),
                        SUBJID: lblSubject.Text
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MODULES()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_MODULES");

                ddlModule.DataSource = ds;
                ddlModule.DataTextField = "FORMNAME";
                ddlModule.DataValueField = "ID";
                ddlModule.DataBind();
                ddlModule.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_STRUCTURE();

                SET_VALUE_ONLOAD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_STRUCTURE()
        {
            try
            {
                hfMODULEID.Value = ddlModule.SelectedValue;

                DataSet ds = dal_IWRS.IWRS_STRUCTURE_SP(ACTION: "GET_STRUCTURE", ID: ddlModule.SelectedValue);
                grd_Data.DataSource = ds;
                grd_Data.DataBind();

                hfTablename.Value = ds.Tables[0].Rows[0]["IWRS_TABLENAME"].ToString();
                hfDM_MODULEID.Value = ds.Tables[0].Rows[0]["MODULEID"].ToString();
                hfMODULEID.Value = ds.Tables[0].Rows[0]["FORMID"].ToString();

                DataSet dsModuleStatus = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_DM_PAGE_STATUS", MODULEID: ddlModule.SelectedValue, SUBJID: lblSubject.Text);

                if (dsModuleStatus.Tables.Count > 0 && dsModuleStatus.Tables[0].Rows.Count > 0)
                {
                    divSubjectDataNote.Visible = true;
                    GetRecords(true);
                }
                else
                {
                    divSubjectDataNote.Visible = false;
                    GetRecords(false);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SET_VALUE_ONLOAD()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_VALUE_SP(ACTION: "GET_MODULE_CRITERIAS_VARIABLENAMES", MODULEID: hfDM_MODULEID.Value, FORMID: hfMODULEID.Value);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
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
                    string MANDATORY = dr["MANDATORY"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    if (INVISIBLE == "True")
                    {
                        e.Row.Style.Add("visibility", "collapse");
                        e.Row.Style.Add("display", "none");
                        e.Row.Visible = false;
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
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;

                        if (MAXLEN != "" && MAXLEN != "0")
                        {
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);
                        }
                        if (READYN == "True")
                        {
                            btnEdit.Attributes.Add("readonly", "readonly");
                        }

                        if (VARIABLENAME == "EX_DAT" || VARIABLENAME == "EXDAT")
                        {
                            btnEdit.Text = Session["IWRS_CurrentDate"].ToString();
                        }
                        else if (VARIABLENAME == "EXTIM")
                        {
                            btnEdit.Text = commFun.GetCurrentDateTimeByTimezone().ToString("HH:mm");
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 300px;");
                        }
                        if (CLASS.Contains("txtTime"))
                        {
                            btnEdit.Attributes.Add("onchange", "ValidTime(this)");
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }
                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            btnEdit.Text = dr["PrefixText"].ToString();
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();
                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;

                        DataSet ds;
                        ds = new DataSet();
                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                            btnEdit.CssClass = btnEdit.CssClass + " class";
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

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory";
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

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory";
                            }
                        }
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    GridView grd_Data1 = e.Row.FindControl("grd_Data1") as GridView;

                    DataSet ds1 = dal_IWRS.IWRS_STRUCTURE_SP(ACTION: "GET_STRUCTURE_CHILD", ID: ddlModule.SelectedValue, FIELDID: ID);
                    grd_Data1.DataSource = ds1.Tables[0];
                    grd_Data1.DataBind();

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
                    string MANDATORY = dr["MANDATORY"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    if (INVISIBLE == "True")
                    {
                        e.Row.Style.Add("visibility", "collapse");
                        e.Row.Style.Add("display", "none");
                        e.Row.Visible = false;
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
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;

                        if (MAXLEN != "" && MAXLEN != "0")
                        {
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);
                        }
                        if (READYN == "True")
                        {
                            btnEdit.Attributes.Add("readonly", "readonly");
                        }

                        if (VARIABLENAME == "EX_DAT" || VARIABLENAME == "EXDAT")
                        {
                            btnEdit.Text = Session["IWRS_CurrentDate"].ToString();
                        }
                        else if (VARIABLENAME == "EXTIM")
                        {
                            btnEdit.Text = commFun.GetCurrentDateTimeByTimezone().ToString("HH:mm");
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 300px;");
                        }
                        if (CLASS.Contains("txtTime"))
                        {
                            btnEdit.Attributes.Add("onchange", "ValidTime(this)");
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }
                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            btnEdit.Text = dr["PrefixText"].ToString();
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();
                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;

                        DataSet ds;
                        ds = new DataSet();
                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                            btnEdit.CssClass = btnEdit.CssClass + " class";
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

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory";
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

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory";
                            }
                        }
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    GridView grd_Data2 = e.Row.FindControl("grd_Data2") as GridView;

                    DataSet ds1 = dal_IWRS.IWRS_STRUCTURE_SP(ACTION: "GET_STRUCTURE_CHILD", ID: ddlModule.SelectedValue, FIELDID: ID);
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
                    string MANDATORY = dr["MANDATORY"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    if (INVISIBLE == "True")
                    {
                        e.Row.Style.Add("visibility", "collapse");
                        e.Row.Style.Add("display", "none");
                        e.Row.Visible = false;
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
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;

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
                            btnEdit.Attributes.Add("style", "width: 300px;");
                        }
                        if (CLASS.Contains("txtTime"))
                        {
                            btnEdit.Attributes.Add("onchange", "ValidTime(this)");
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }
                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            btnEdit.Text = dr["PrefixText"].ToString();
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();
                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;

                        DataSet ds;
                        ds = new DataSet();
                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                            btnEdit.CssClass = btnEdit.CssClass + " class";
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

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory";
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

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory";
                            }
                        }
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    GridView grd_Data3 = e.Row.FindControl("grd_Data3") as GridView;

                    DataSet ds1 = dal_IWRS.IWRS_STRUCTURE_SP(ACTION: "GET_STRUCTURE_CHILD", ID: ddlModule.SelectedValue, FIELDID: ID);
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
                    string MANDATORY = dr["MANDATORY"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    if (INVISIBLE == "True")
                    {
                        e.Row.Style.Add("visibility", "collapse");
                        e.Row.Style.Add("display", "none");
                        e.Row.Visible = false;
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
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;

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
                            btnEdit.Attributes.Add("style", "width: 300px;");
                        }
                        if (CLASS.Contains("txtTime"))
                        {
                            btnEdit.Attributes.Add("onchange", "ValidTime(this)");
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }
                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            btnEdit.Text = dr["PrefixText"].ToString();
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();
                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;

                        DataSet ds;
                        ds = new DataSet();
                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                            btnEdit.CssClass = btnEdit.CssClass + " class";
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

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory";
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

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory";
                            }
                        }
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
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

        private void GetRecords(bool isFreeze)
        {
            try
            {
                string COLNAME, COLVAL;
                int rownum = 0;

                DAL dal;
                dal = new DAL();
                DataSet dsData;

                dsData = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_DATA", MODULEID: ddlModule.SelectedValue, SUBJID: lblSubject.Text);

                DataTable dt = GenerateTransposedTable(dsData.Tables[0]);
                DataSet ds = new DataSet();

                ds.Tables.Add(dt);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        for (rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
                        {
                            COLNAME = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                            string CONTROLTYPE;
                            CONTROLTYPE = ((Label)grd_Data.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                            string DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();

                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();

                            if (DataVariableName == COLNAME)
                            {
                                ((HiddenField)grd_Data.Rows[rownum].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                if (CONTROLTYPE == "TEXTBOX")
                                {
                                    COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                    if (COLVAL != "")
                                    {
                                        ((TextBox)grd_Data.Rows[rownum].FindControl("TXT_FIELD")).Text = COLVAL;
                                    }

                                    if (isFreeze)
                                    {
                                        ((TextBox)grd_Data.Rows[rownum].FindControl("TXT_FIELD")).Enabled = false;
                                    }

                                }
                                else if (CONTROLTYPE == "DROPDOWN")
                                {
                                    COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                    ((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedValue = COLVAL;

                                    if (isFreeze)
                                    {
                                        ((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).Enabled = false;
                                    }
                                }
                                else if (CONTROLTYPE == "CHECKBOX")
                                {
                                    string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split(',');
                                    foreach (string x in stringArray)
                                    {
                                        Repeater repeat_RAD = grd_Data.Rows[rownum].FindControl("repeat_CHK") as Repeater;
                                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                        {
                                            if (x != "")
                                            {
                                                if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == x)
                                                {
                                                    ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                }
                                            }

                                            if (isFreeze)
                                            {
                                                ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                            }
                                        }
                                    }
                                }
                                else if (CONTROLTYPE == "RADIOBUTTON")
                                {
                                    string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split(',');
                                    foreach (string x in stringArray)
                                    {
                                        Repeater repeat_RAD = grd_Data.Rows[rownum].FindControl("repeat_RAD") as Repeater;
                                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                        {
                                            if (x != "")
                                            {
                                                if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.ToString() == x)
                                                {
                                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                                }
                                            }

                                            if (isFreeze)
                                            {
                                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
                                            }
                                        }
                                    }
                                }
                            }

                            GridView grd_Data1 = grd_Data.Rows[rownum].FindControl("grd_Data1") as GridView;

                            for (int a = 0; a < grd_Data1.Rows.Count; a++)
                            {
                                COLNAME = ((Label)grd_Data1.Rows[a].FindControl("lblVARIABLENAME")).Text;
                                CONTROLTYPE = ((Label)grd_Data1.Rows[a].FindControl("lblCONTROLTYPE")).Text;
                                DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();

                                if (DataVariableName == COLNAME)
                                {
                                    ((HiddenField)grd_Data1.Rows[a].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                    if (CONTROLTYPE == "TEXTBOX")
                                    {
                                        COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();

                                        if (COLVAL != "")
                                        {
                                            ((TextBox)grd_Data1.Rows[a].FindControl("TXT_FIELD")).Text = COLVAL;
                                        }

                                        if (isFreeze)
                                        {
                                            ((TextBox)grd_Data1.Rows[a].FindControl("TXT_FIELD")).Enabled = false;
                                        }
                                    }
                                    else if (CONTROLTYPE == "DROPDOWN")
                                    {
                                        COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                        ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).SelectedValue = COLVAL;

                                        if (isFreeze)
                                        {
                                            ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).Enabled = false;
                                        }
                                    }
                                    else if (CONTROLTYPE == "CHECKBOX")
                                    {
                                        string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split(',');
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

                                                if (isFreeze)
                                                {
                                                    ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                                }
                                            }
                                        }
                                    }
                                    else if (CONTROLTYPE == "RADIOBUTTON")
                                    {
                                        string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split(',');
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
                                                }

                                                if (isFreeze)
                                                {
                                                    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
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

                                    if (DataVariableName == COLNAME)
                                    {
                                        ((HiddenField)grd_Data2.Rows[b].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                        if (CONTROLTYPE == "TEXTBOX")
                                        {
                                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();

                                            if (COLVAL != "")
                                            {
                                                ((TextBox)grd_Data2.Rows[b].FindControl("TXT_FIELD")).Text = COLVAL;
                                            }

                                            if (isFreeze)
                                            {
                                                ((TextBox)grd_Data2.Rows[b].FindControl("TXT_FIELD")).Enabled = false;
                                            }
                                        }
                                        else if (CONTROLTYPE == "DROPDOWN")
                                        {
                                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                            ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).SelectedValue = COLVAL;

                                            if (isFreeze)
                                            {
                                                ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).Enabled = false;
                                            }
                                        }
                                        else if (CONTROLTYPE == "CHECKBOX")
                                        {
                                            string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split(',');
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

                                                    if (isFreeze)
                                                    {
                                                        ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                                    }
                                                }
                                            }
                                        }
                                        else if (CONTROLTYPE == "RADIOBUTTON")
                                        {
                                            string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split(',');
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
                                                    }

                                                    if (isFreeze)
                                                    {
                                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
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

                                        if (DataVariableName == COLNAME)
                                        {
                                            ((HiddenField)grd_Data3.Rows[c].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                            if (CONTROLTYPE == "TEXTBOX")
                                            {
                                                COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();

                                                if (COLVAL != "")
                                                {
                                                    ((TextBox)grd_Data3.Rows[c].FindControl("TXT_FIELD")).Text = COLVAL;
                                                }

                                                if (isFreeze)
                                                {
                                                    ((TextBox)grd_Data3.Rows[c].FindControl("TXT_FIELD")).Enabled = false;
                                                }
                                            }
                                            else if (CONTROLTYPE == "DROPDOWN")
                                            {
                                                COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                                ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).SelectedValue = COLVAL;

                                                if (isFreeze)
                                                {
                                                    ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).Enabled = false;
                                                }
                                            }
                                            else if (CONTROLTYPE == "CHECKBOX")
                                            {
                                                string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split(',');
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

                                                        if (isFreeze)
                                                        {
                                                            ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Enabled = false;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (CONTROLTYPE == "RADIOBUTTON")
                                            {
                                                string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split(',');
                                                foreach (string x in stringArray)
                                                {
                                                    Repeater repeat_RAD = grd_Data3.Rows[c].FindControl("repeat_RAD") as Repeater;
                                                    for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                                    {
                                                        if (x != "")
                                                        {
                                                            if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).ToString() == x)
                                                            {
                                                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                                            }
                                                        }

                                                        if (isFreeze)
                                                        {
                                                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Enabled = false;
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

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Change", "callChange();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void DATA_Changed(object sender, EventArgs e)
        {
            try
            {
                string VARIABLENAME = "";

                GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

                VARIABLENAME = (row.FindControl("lblVARIABLENAME") as Label).Text;

                CHECK_SET_VALUE("DATACHANGE", VARIABLENAME);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CHECK_SET_VALUE(string EVENTS, string VARIABLENAME)
        {
            try
            {
                bool RESULT = true;
                string resVARIABLENAME = "";

                DataSet dsCriterias = new DataSet();

                if (EVENTS == "ONLOAD")
                {
                    dsCriterias = dal_IWRS.IWRS_SET_VALUE_SP(ACTION: "GET_CRITERIAS_ONLOAD", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);
                }
                else
                {
                    dsCriterias = dal_IWRS.IWRS_SET_VALUE_SP(ACTION: "GET_CRITERIAS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);
                }

                if (dsCriterias.Tables.Count > 0 && dsCriterias.Tables[0].Rows.Count > 0)
                {
                    DataTable dtCurrentDATA = GET_GRID_DATATABLE();

                    foreach (DataRow drCriteria in dsCriterias.Tables[0].Rows)
                    {
                        string MainVARIABLENAME = drCriteria["VARIABLENAME"].ToString();

                        if (RESULT)
                        {
                            if (drCriteria["METHOD"].ToString() == "By Criteria")
                            {
                                string CRITCODE = drCriteria["CritCode"].ToString();

                                foreach (DataRow drCurrentDATA in dtCurrentDATA.Rows)
                                {
                                    CRITCODE = CRITCODE.Replace("[" + drCurrentDATA["VARIABLENAME"].ToString() + "]", CheckDatatype(drCurrentDATA["DATA"].ToString()));
                                }

                                DataSet dsRes = dal_IWRS.IWRS_SET_VALUE_SP(ACTION: "EVAL_CRITERIA", CRITCODE: CRITCODE, FORMID: hfMODULEID.Value);
                                if (dsRes.Tables.Count > 0 && dsRes.Tables[0].Rows.Count > 0)
                                {
                                    if (dsRes.Tables[0].Rows[0]["RESULTS"].ToString() == "1")
                                    {
                                        DataRow[] rows = dtCurrentDATA.Select(" [VARIABLENAME] = '" + MainVARIABLENAME + "' ");

                                        SET_VALUE(MainVARIABLENAME, drCriteria["VALUE"].ToString(), rows[0]["CONTROLTYPE"].ToString(), rows[0]["GRIDNAME"].ToString(), rows[0]["CLIENTID"].ToString(), Convert.ToInt32(rows[0]["ROWNUM"]));

                                        //RESULT = false;
                                        resVARIABLENAME = MainVARIABLENAME;
                                    }
                                }

                            }
                            else if (drCriteria["METHOD"].ToString() == "By Formula")
                            {
                                string FORMULA = drCriteria["FORMULA"].ToString();

                                foreach (DataRow drCurrentDATA in dtCurrentDATA.Rows)
                                {
                                    if (drCurrentDATA["DATA"].ToString() != "")
                                    {
                                        FORMULA = FORMULA.Replace("[" + drCurrentDATA["VARIABLENAME"].ToString() + "]", CheckDatatype(drCurrentDATA["DATA"].ToString()));
                                    }
                                }

                                if (!FORMULA.Contains("[") && !FORMULA.Contains("]"))
                                {
                                    DataSet dsRes = dal_IWRS.IWRS_SET_VALUE_SP(ACTION: "GET_FORMULA_VALUE", FORMULA: FORMULA, FORMID: hfMODULEID.Value);

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
                        }

                        if (resVARIABLENAME != "" && resVARIABLENAME != VARIABLENAME)
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
                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        ((TextBox)grdName.Rows[ROWNUM].FindControl("TXT_FIELD")).Text = VALUE;
                    }
                    else if (CONTROLTYPE == "DROPDOWN")
                    {
                        ((DropDownList)grdName.Rows[ROWNUM].FindControl("DRP_FIELD")).SelectedValue = VALUE;
                    }
                    else if (CONTROLTYPE == "CHECKBOX")
                    {
                        Repeater repeat_CHK = grdName.Rows[ROWNUM].FindControl("repeat_CHK") as Repeater;
                        for (int a = 0; a < repeat_CHK.Items.Count; a++)
                        {
                            if (((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Text.ToString() == VALUE)
                            {
                                ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked = true;
                            }
                            else
                            {
                                ((CheckBox)repeat_CHK.Items[a].FindControl("CHK_FIELD")).Checked = false;
                            }
                        }
                    }
                    else if (CONTROLTYPE == "RADIOBUTTON")
                    {
                        Repeater repeat_RAD = grdName.Rows[ROWNUM].FindControl("repeat_RAD") as Repeater;
                        for (int a = 0; a < repeat_RAD.Items.Count; a++)
                        {
                            if (((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Text.ToString() == VALUE)
                            {
                                ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked = true;
                            }
                            else
                            {
                                ((RadioButton)repeat_RAD.Items[a].FindControl("RAD_FIELD")).Checked = false;
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
                    Val = Val.Replace("dd/", "01");
                }

                if (Val.Contains("mm/"))
                {
                    Val = Val.Replace("mm/", "01");
                }

                if (int.TryParse(Val, out i) && Val != "")
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (float.TryParse(Val, out j) && Val != "")
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (double.TryParse(Val, out k) && Val != "")
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
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
                        strdata = ((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedItem.Text;
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
                            strdata1 = ((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedItem.Text;
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
                                strdata2 = ((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedItem.Text;
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
                                    strdata3 = ((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedItem.Text;
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

            DataSet dsAddFields = dal_IWRS.IWRS_DATA_SP(ACTION: "GET_AddFields_DATA", SUBJID: hfSUBJID.Value);

            if (dsAddFields.Tables.Count > 0 && dsAddFields.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn dc in dsAddFields.Tables[0].Columns)
                {
                    outputTable.Rows.Add(dc.ColumnName, dsAddFields.Tables[0].Rows[0][dc.ColumnName].ToString(), "", "", "", "");
                }
            }


            DataSet dsDETAILS = dal_IWRS.IWRS_GET_SUBJECT_DETAILS_SP(ACTION: "GET_SUBJECT_DETAILS", SUBJID: hfSUBJID.Value, FORMID: hfMODULEID.Value);

            if (dsDETAILS.Tables.Count > 1 && dsDETAILS.Tables[1].Rows.Count > 0)
            {
                foreach (DataColumn dc in dsDETAILS.Tables[1].Columns)
                {
                    outputTable.Rows.Add(dc.ColumnName, dsDETAILS.Tables[1].Rows[0][dc.ColumnName].ToString(), "", "", "", "");
                }
            }

            return outputTable;
        }

        protected void btnDatasubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_FORM_DATA();

                Response.Write("<script> alert('Subject Data Updated Successfully.');</script>");
                Response.Redirect("NIWRS_DCF_ACTION.aspx?ID=" + Request.QueryString["ID"].ToString());
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
                DAL dal;
                dal = new DAL();
                string varname;

                bool isColAdded = false;

                string COLUMN = "";
                string DATA = "";
                string old_DATA = "";

                string syncCOLUMN = "";
                string syncDATA = "";
                string old_syncDATA = "";

                string Strata = "";

                string INSERTQUERY = "", UPDATEQUERY = "";

                for (int rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
                {
                    string strdata = "", old_strdata = "";
                    varname = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                    Strata = ((Label)grd_Data.Rows[rownum].FindControl("lblSTRATA")).Text;
                    string CONTROLTYPE;
                    CONTROLTYPE = ((Label)grd_Data.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                    string DM_SYNC;
                    DM_SYNC = ((Label)grd_Data.Rows[rownum].FindControl("IWRS_DM_SYNC")).Text;

                    old_strdata = ((HiddenField)grd_Data.Rows[rownum].FindControl("HDN_OLD_VALUE")).Value;

                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                    {
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            if (((TextBox)grd_Data.Rows[rownum].FindControl("TXT_FIELD")).Text.Contains(((Label)grd_Data.Rows[rownum].FindControl("lblPREFIXTEXT")).Text))
                            {
                                if (((Label)grd_Data.Rows[rownum].FindControl("lblPREFIXTEXT")).Text != "")
                                {
                                    ((TextBox)grd_Data.Rows[rownum].FindControl("TXT_FIELD")).Text.Replace(((Label)grd_Data.Rows[rownum].FindControl("lblPREFIXTEXT")).Text, "");
                                }
                            }
                            strdata = ((Label)grd_Data.Rows[rownum].FindControl("lblPREFIXTEXT")).Text + ((TextBox)grd_Data.Rows[rownum].FindControl("TXT_FIELD")).Text;
                        }
                        else if (CONTROLTYPE == "DROPDOWN")
                        {
                            if (((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                            {
                                strdata = ((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedItem.Text;
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

                        if (DATA != "")
                        {
                            if (strdata != "")
                            {
                                DATA = DATA + " @ni$h N'" + strdata + "'";
                                old_DATA = old_DATA + " @ni$h N'" + old_strdata + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h ''";
                                old_DATA = old_DATA + " @ni$h ''";
                            }
                        }
                        else
                        {
                            if (strdata != "")
                            {
                                DATA = "N'" + strdata + "'";
                                old_DATA = "N'" + old_strdata + "'";
                            }
                            else
                            {
                                DATA = "''";
                                old_DATA = "''";
                            }
                        }

                        if (DM_SYNC != "" && DM_SYNC != "0")
                        {
                            if (syncCOLUMN != "")
                            {
                                syncCOLUMN = syncCOLUMN + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                            }
                            else
                            {
                                syncCOLUMN = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                            }

                            if (syncDATA != "")
                            {
                                if (strdata != "")
                                {
                                    syncDATA = syncDATA + " @ni$h '" + strdata + "'";
                                    old_syncDATA = old_syncDATA + " @ni$h '" + old_strdata + "'";
                                }
                                else
                                {
                                    syncDATA = syncDATA + " @ni$h ''";
                                    old_syncDATA = old_syncDATA + " @ni$h ''";
                                }
                            }
                            else
                            {
                                if (strdata != "")
                                {
                                    syncDATA = "N'" + strdata + "'";
                                    old_syncDATA = "N'" + old_strdata + "'";
                                }
                                else
                                {
                                    syncDATA = "''";
                                    old_syncDATA = "''";
                                }
                            }
                        }
                    }
                    if (strdata != "" && Strata != "0")
                    {
                        dal_IWRS.IWRS_DATA_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata, SUBJID: lblSubject.Text);
                    }

                    GridView grd_Data1 = grd_Data.Rows[rownum].FindControl("grd_Data1") as GridView;

                    for (int b = 0; b < grd_Data1.Rows.Count; b++)
                    {
                        string Val_Child;
                        string strdata1 = "", old_strdata1 = "";
                        Val_Child = ((Label)grd_Data1.Rows[b].FindControl("lblVal_Child")).Text;

                        varname = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                        Strata = ((Label)grd_Data1.Rows[b].FindControl("lblSTRATA")).Text;
                        CONTROLTYPE = ((Label)grd_Data1.Rows[b].FindControl("lblCONTROLTYPE")).Text;
                        DM_SYNC = ((Label)grd_Data1.Rows[b].FindControl("IWRS_DM_SYNC")).Text;

                        old_strdata1 = ((HiddenField)grd_Data1.Rows[b].FindControl("HDN_OLD_VALUE")).Value;

                        if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                        {
                            if (CONTROLTYPE == "TEXTBOX")
                            {
                                if (((TextBox)grd_Data1.Rows[b].FindControl("TXT_FIELD")).Text.Contains(((Label)grd_Data1.Rows[b].FindControl("lblPREFIXTEXT")).Text))
                                {
                                    if (((Label)grd_Data1.Rows[b].FindControl("lblPREFIXTEXT")).Text != "")
                                    {
                                        ((TextBox)grd_Data1.Rows[b].FindControl("TXT_FIELD")).Text.Replace(((Label)grd_Data1.Rows[b].FindControl("lblPREFIXTEXT")).Text, "");
                                    }
                                }
                                strdata1 = ((Label)grd_Data1.Rows[b].FindControl("lblPREFIXTEXT")).Text + ((TextBox)grd_Data1.Rows[b].FindControl("TXT_FIELD")).Text;
                            }
                            else if (CONTROLTYPE == "DROPDOWN")
                            {
                                if (((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                {
                                    strdata1 = ((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedItem.Text;
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

                            isColAdded = false;

                            foreach (string val in strdata.Split('¸'))
                            {
                                if (checkStringContains(Val_Child, val) || (Val_Child == "Is Not Blank" && strdata != "") || Val_Child == "Compare")
                                {
                                    isColAdded = true;

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
                                            old_DATA = old_DATA + " @ni$h N'" + old_strdata1 + "'";
                                        }
                                        else
                                        {
                                            DATA = DATA + " @ni$h NULL";
                                            old_DATA = old_DATA + " @ni$h NULL";

                                            strdata1 = "";
                                        }
                                    }
                                    else
                                    {
                                        if (strdata1 != "")
                                        {
                                            DATA = "N'" + strdata1 + "'";
                                            old_DATA = "N'" + old_strdata1 + "'";
                                        }
                                        else
                                        {
                                            DATA = "NULL";
                                            old_DATA = "NULL";

                                            strdata1 = "";
                                        }
                                    }

                                    if (DM_SYNC != "" && DM_SYNC != "0")
                                    {
                                        if (syncCOLUMN != "")
                                        {
                                            syncCOLUMN = syncCOLUMN + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                        }
                                        else
                                        {
                                            syncCOLUMN = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                        }

                                        if (syncDATA != "")
                                        {
                                            if (strdata1 != "")
                                            {
                                                syncDATA = syncDATA + " @ni$h N'" + strdata1 + "'";
                                                old_syncDATA = old_syncDATA + " @ni$h N'" + old_strdata1 + "'";
                                            }
                                            else
                                            {
                                                syncDATA = syncDATA + " @ni$h NULL";
                                                old_syncDATA = old_syncDATA + " @ni$h NULL";

                                                strdata1 = "";
                                            }
                                        }
                                        else
                                        {
                                            if (strdata1 != "")
                                            {
                                                syncDATA = "N'" + strdata1 + "'";
                                                old_syncDATA = "N'" + old_strdata1 + "'";
                                            }
                                            else
                                            {
                                                syncDATA = "NULL";
                                                old_syncDATA = "NULL";

                                                strdata1 = "";
                                            }
                                        }
                                    }
                                }
                            }

                            if (!isColAdded)
                            {
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
                                    old_DATA = old_DATA + " @ni$h NULL";

                                    strdata1 = "";
                                }
                                else
                                {
                                    DATA = "NULL";
                                    old_DATA = "NULL";

                                    strdata1 = "";
                                }
                            }

                            if (strdata1 != "" && Strata != "0")
                            {
                                dal_IWRS.IWRS_DATA_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata1, SUBJID: lblSubject.Text);
                            }
                        }

                        GridView grd_Data2 = grd_Data1.Rows[b].FindControl("grd_Data2") as GridView;

                        for (int c = 0; c < grd_Data2.Rows.Count; c++)
                        {
                            string Val_Child1 = ((Label)grd_Data2.Rows[c].FindControl("lblVal_Child")).Text;
                            string strdata2 = "", old_strdata2 = "";
                            varname = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                            Strata = ((Label)grd_Data2.Rows[c].FindControl("lblSTRATA")).Text;
                            CONTROLTYPE = ((Label)grd_Data2.Rows[c].FindControl("lblCONTROLTYPE")).Text;
                            DM_SYNC = ((Label)grd_Data2.Rows[c].FindControl("IWRS_DM_SYNC")).Text;

                            old_strdata2 = ((HiddenField)grd_Data2.Rows[c].FindControl("HDN_OLD_VALUE")).Value;

                            if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                            {
                                if (CONTROLTYPE == "TEXTBOX")
                                {
                                    if (((TextBox)grd_Data2.Rows[c].FindControl("TXT_FIELD")).Text.Contains(((Label)grd_Data2.Rows[c].FindControl("lblPREFIXTEXT")).Text))
                                    {
                                        if (((Label)grd_Data2.Rows[c].FindControl("lblPREFIXTEXT")).Text != "")
                                        {
                                            ((TextBox)grd_Data2.Rows[c].FindControl("TXT_FIELD")).Text.Replace(((Label)grd_Data2.Rows[c].FindControl("lblPREFIXTEXT")).Text, "");
                                        }
                                    }
                                    strdata2 = ((Label)grd_Data2.Rows[c].FindControl("lblPREFIXTEXT")).Text + ((TextBox)grd_Data2.Rows[c].FindControl("TXT_FIELD")).Text;
                                }
                                else if (CONTROLTYPE == "DROPDOWN")
                                {
                                    if (((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                    {
                                        strdata2 = ((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedItem.Text;
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
                                    if (checkStringContains(Val_Child1, val) || (Val_Child1 == "Is Not Blank" && val != "") || Val_Child1 == "Compare")
                                    {
                                        isColAdded = true;

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
                                                old_DATA = old_DATA + " @ni$h N'" + old_strdata2 + "'";
                                            }
                                            else
                                            {
                                                DATA = DATA + " @ni$h NULL";
                                                old_DATA = old_DATA + " @ni$h NULL";
                                            }
                                        }
                                        else
                                        {
                                            if (strdata2 != "")
                                            {
                                                DATA = "N'" + strdata2 + "'";
                                                old_DATA = "N'" + old_strdata2 + "'";
                                            }
                                            else
                                            {
                                                DATA = "NULL";
                                                old_DATA = "NULL";
                                            }
                                        }

                                        if (DM_SYNC != "" && DM_SYNC != "0")
                                        {
                                            if (syncCOLUMN != "")
                                            {
                                                syncCOLUMN = syncCOLUMN + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                            }
                                            else
                                            {
                                                syncCOLUMN = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                            }

                                            if (syncDATA != "")
                                            {
                                                if (strdata2 != "")
                                                {
                                                    syncDATA = syncDATA + " @ni$h N'" + strdata2 + "'";
                                                    old_syncDATA = old_syncDATA + " @ni$h N'" + old_strdata2 + "'";
                                                }
                                                else
                                                {
                                                    syncDATA = syncDATA + " @ni$h NULL";
                                                    old_syncDATA = old_syncDATA + " @ni$h NULL";

                                                    strdata2 = "";
                                                }
                                            }
                                            else
                                            {
                                                if (strdata2 != "")
                                                {
                                                    syncDATA = "N'" + strdata2 + "'";
                                                    old_syncDATA = "N'" + old_strdata2 + "'";
                                                }
                                                else
                                                {
                                                    syncDATA = "NULL";
                                                    old_syncDATA = "NULL";

                                                    strdata2 = "";
                                                }
                                            }
                                        }
                                    }
                                }

                                if (strdata2 != "" && Strata != "0")
                                {
                                    dal_IWRS.IWRS_DATA_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata2, SUBJID: lblSubject.Text);
                                }
                            }

                            GridView grd_Data3 = grd_Data2.Rows[c].FindControl("grd_Data3") as GridView;

                            for (int d = 0; d < grd_Data3.Rows.Count; d++)
                            {
                                string Val_Child2 = ((Label)grd_Data3.Rows[d].FindControl("lblVal_Child")).Text;
                                string strdata3 = "", old_strdata3 = "";
                                varname = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                Strata = ((Label)grd_Data3.Rows[d].FindControl("lblSTRATA")).Text;
                                CONTROLTYPE = ((Label)grd_Data3.Rows[d].FindControl("lblCONTROLTYPE")).Text;
                                DM_SYNC = ((Label)grd_Data3.Rows[d].FindControl("IWRS_DM_SYNC")).Text;

                                old_strdata3 = ((HiddenField)grd_Data3.Rows[d].FindControl("HDN_OLD_VALUE")).Value;

                                if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                                {
                                    if (CONTROLTYPE == "TEXTBOX")
                                    {
                                        if (((TextBox)grd_Data3.Rows[d].FindControl("TXT_FIELD")).Text.Contains(((Label)grd_Data3.Rows[d].FindControl("lblPREFIXTEXT")).Text))
                                        {
                                            if (((Label)grd_Data3.Rows[d].FindControl("lblPREFIXTEXT")).Text != "")
                                            {
                                                ((TextBox)grd_Data3.Rows[d].FindControl("TXT_FIELD")).Text.Replace(((Label)grd_Data3.Rows[d].FindControl("lblPREFIXTEXT")).Text, "");
                                            }
                                        }
                                        strdata3 = ((Label)grd_Data3.Rows[d].FindControl("lblPREFIXTEXT")).Text + ((TextBox)grd_Data3.Rows[d].FindControl("TXT_FIELD")).Text;
                                    }
                                    else if (CONTROLTYPE == "DROPDOWN")
                                    {
                                        if (((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
                                        {
                                            strdata3 = ((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedItem.Text;
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
                                        if (checkStringContains(Val_Child2, val) || (Val_Child2 == "Is Not Blank" && val != "") || Val_Child2 == "Compare")
                                        {
                                            isColAdded = true;

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
                                                    old_DATA = old_DATA + " @ni$h N'" + old_strdata3 + "'";
                                                }
                                                else
                                                {
                                                    DATA = DATA + " @ni$h NULL";
                                                    old_DATA = old_DATA + " @ni$h NULL";
                                                }
                                            }
                                            else
                                            {
                                                if (strdata3 != "")
                                                {
                                                    DATA = "N'" + strdata3 + "'";
                                                    old_DATA = "N'" + old_strdata3 + "'";
                                                }
                                                else
                                                {
                                                    DATA = "NULL";
                                                    old_DATA = "NULL";
                                                }
                                            }

                                            if (DM_SYNC != "" && DM_SYNC != "0")
                                            {
                                                if (syncCOLUMN != "")
                                                {
                                                    syncCOLUMN = syncCOLUMN + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                                }
                                                else
                                                {
                                                    syncCOLUMN = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                                }

                                                if (syncDATA != "")
                                                {
                                                    if (strdata3 != "")
                                                    {
                                                        syncDATA = syncDATA + " @ni$h N'" + strdata3 + "'";
                                                        old_syncDATA = old_syncDATA + " @ni$h N'" + old_strdata3 + "'";
                                                    }
                                                    else
                                                    {
                                                        syncDATA = syncDATA + " @ni$h NULL";
                                                        old_syncDATA = old_syncDATA + " @ni$h NULL";

                                                        strdata3 = "";
                                                    }
                                                }
                                                else
                                                {
                                                    if (strdata3 != "")
                                                    {
                                                        syncDATA = "N'" + strdata3 + "'";
                                                        old_syncDATA = "N'" + old_strdata3 + "'";
                                                    }
                                                    else
                                                    {
                                                        syncDATA = "NULL";
                                                        old_syncDATA = "NULL";

                                                        strdata3 = "";
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (strdata3 != "" && Strata != "0")
                                    {
                                        dal_IWRS.IWRS_DATA_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata3, SUBJID: lblSubject.Text);
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

                if (hfApplVisit.Value != "0" && hfApplVisit.Value != "")
                {
                    INSERTQUERY = "INSERT INTO [" + hfTablename.Value + "] ([SUBJID_DATA], [SITEID], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + lblSubject.Text + "', '" + lblSite.Text + "', '" + Session["User_ID"].ToString() + "','" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + " )";
                }
                else
                {
                    INSERTQUERY = "INSERT INTO [" + hfTablename.Value + "] ([SUBJID_DATA], [SITEID], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + lblSubject.Text + "', '" + lblSite.Text + "', '" + Session["User_ID"].ToString() + "','" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + " )";
                }

                string[] colArr = COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                string[] dataArr = DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                string[] old_dataArr = old_DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);

                for (int i = 0; i < colArr.Length; i++)
                {
                    if (UPDATEQUERY == "")
                    {
                        UPDATEQUERY = "UPDATE [" + hfTablename.Value + "] SET ENTEREDDAT = GETDATE(), ENTEREDBY = '" + Session["USER_ID"].ToString() + "' ";
                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[i] + " = " + dataArr[i] + " ";
                    }
                    else
                    {
                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[i] + " = " + dataArr[i] + " ";
                    }

                    if (old_dataArr[i].ToString() != dataArr[i].ToString())
                    {
                        IWRS_ADD_NEW_ROW_DATA(
                            DCFID: Request.QueryString["ID"].ToString(),
                            REASON: "Data updated as per DCF",
                            SUBJID: lblSubject.Text,
                            MODULENAME: ddlModule.SelectedItem.Text,
                            FIELDNAME: "",
                            TABLENAME: hfTablename.Value,
                            VARIABLENAME: colArr[i],
                            OLDVALUE: old_dataArr[i],
                            NEWVALUE: dataArr[i]
                            );
                    }

                }
                UPDATEQUERY = UPDATEQUERY + " WHERE SUBJID_DATA = '" + lblSubject.Text + "' ";

                dal_IWRS.IWRS_INSERT_DATA_SP(
                 ACTION: "INSERT_FORM_DATA",
                 INSERTQUERY: INSERTQUERY,
                 TABLENAME: hfTablename.Value,
                 SUBJID: lblSubject.Text,
                 UPDATEQUERY: UPDATEQUERY
                 );


                UPLOAD_IWRS_AUDITTRAIL();

                string Sync_FIELDS = syncCOLUMN.Replace("@ni$h", ",");

                DataSet dsMODULEs = dal_IWRS.IWRS_SYNC_SP(ACTION: "GET_IWRS_DM_SYNC_MODULES", FIELDNAME: Sync_FIELDS);

                foreach (DataRow drModules in dsMODULEs.Tables[0].Rows)
                {
                    string SYNC_MODULEID = drModules["ID"].ToString();
                    string SYNC_MODULENAME = drModules["MODULENAME"].ToString();
                    string SYNC_TABLENAME = drModules["TABLENAME"].ToString();

                    DataSet dsVisit = dal_IWRS.IWRS_SYNC_SP(ACTION: "GET_DM_VISITID", MODULEID: SYNC_MODULEID);

                    if (dsVisit.Tables[0].Rows.Count > 0)
                    {
                        string VISITID = dsVisit.Tables[0].Rows[0]["VISITNUM"].ToString();
                        string VISIT = dsVisit.Tables[0].Rows[0]["VISIT"].ToString();

                        string SYNC_COL = "", SYNC_Data = "", old_SYNC_Data = "";

                        string syncINSERTQUERY = "", syncUPDATEQUERY = "";

                        string PVID = Session["PROJECTID"].ToString() + "-" + lblSite.Text + "-" + lblSubject.Text + "-" + VISITID + "-" + SYNC_MODULEID + "-1";
                        string RECID = "0";

                        string[] synccolArr = syncCOLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                        string[] syncdataArr = syncDATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                        string[] old_syncdataArr = old_syncDATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);

                        foreach (DataRow drModuleFields in dsMODULEs.Tables[1].Rows)
                        {
                            string SYNC_FIELD_MODULEID = drModuleFields["MODULEID"].ToString();
                            string SYNC_FIELD_VARIABLENAME = drModuleFields["VARIABLENAME"].ToString();

                            if (SYNC_MODULEID == SYNC_FIELD_MODULEID)
                            {
                                for (int i = 0; i < synccolArr.Length; i++)
                                {
                                    if (SYNC_FIELD_VARIABLENAME.Trim() == synccolArr[i].Trim().ToString())
                                    {
                                        if (SYNC_COL != "")
                                        {
                                            SYNC_COL = SYNC_COL + " , " + synccolArr[i].Trim().ToString();
                                        }
                                        else
                                        {
                                            SYNC_COL = synccolArr[i].Trim().ToString();
                                        }

                                        if (SYNC_Data != "")
                                        {
                                            SYNC_Data = SYNC_Data + " , " + syncdataArr[i].Trim().ToString();
                                        }
                                        else
                                        {
                                            SYNC_Data = syncdataArr[i].Trim().ToString();
                                        }

                                        if (old_SYNC_Data != "")
                                        {
                                            old_SYNC_Data = old_SYNC_Data + " , " + old_syncdataArr[i].Trim().ToString();
                                        }
                                        else
                                        {
                                            old_SYNC_Data = old_syncdataArr[i].Trim().ToString();
                                        }
                                    }
                                }
                            }
                        }

                        string[] qryColArr = SYNC_COL.Split(new string[] { "," }, StringSplitOptions.None);
                        string[] qryDataArr = SYNC_Data.Split(new string[] { "," }, StringSplitOptions.None);
                        string[] old_qryDataArr = old_SYNC_Data.Split(new string[] { "," }, StringSplitOptions.None);

                        syncINSERTQUERY = "INSERT INTO [" + SYNC_TABLENAME + "] ([PVID], [RECID], [SUBJID_DATA], [VISITNUM], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + SYNC_COL.Replace("@ni$h", ",") + ") VALUES ('" + PVID + "', '" + RECID + "', '" + lblSubject.Text + "', '" + VISITID + "', '" + Session["User_ID"].ToString() + "','" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + SYNC_Data.Replace("@ni$h", ",") + ")";

                        for (int i = 0; i < qryColArr.Length; i++)
                        {
                            if (syncUPDATEQUERY == "")
                            {
                                syncUPDATEQUERY = "UPDATE [" + SYNC_TABLENAME + "] SET UPDATEDDAT = GETDATE(), UPDATEDBYNAME = '" + Session["User_Name"].ToString() + "', UPDATED_TZVAL = '" + Session["TimeZone_Value"].ToString() + "', UPDATEDBY = '" + Session["USER_ID"].ToString() + "' ";

                                syncUPDATEQUERY = syncUPDATEQUERY + ", " + qryColArr[i] + " = " + qryDataArr[i] + " ";
                            }
                            else
                            {
                                syncUPDATEQUERY = syncUPDATEQUERY + ", " + qryColArr[i] + " = " + qryDataArr[i] + " ";
                            }

                            if (old_qryDataArr[i].ToString() != qryDataArr[i].ToString())
                            {
                                DM_ADD_NEW_ROW_DATA(
                                PVID: PVID,
                                RECID: RECID,
                                SUBJID: lblSubject.Text,
                                VISITNUM: VISITID,
                                MODULENAME: SYNC_MODULENAME,
                                TABLENAME: SYNC_TABLENAME,
                                VariableName: qryColArr[i],
                                OldValue: old_qryDataArr[i],
                                NewValue: qryDataArr[i],
                                Reason: "Other",
                                Comment: "IWRS DCF ID : " + Request.QueryString["ID"].ToString()
                                );
                            }
                        }

                        syncUPDATEQUERY = syncUPDATEQUERY + " WHERE PVID = '" + PVID + "' AND RECID = '" + RECID + "' AND SUBJID_DATA = '" + lblSubject.Text + "' ";

                        DAL_DM dal_dm = new DAL_DM();

                        dal_dm.DM_INSERT_MODULE_DATA_SP(
                         ACTION: "INSERT_MODULE_DATA",
                         INSERTQUERY: syncINSERTQUERY,
                         UPDATEQUERY: syncUPDATEQUERY,
                         TABLENAME: SYNC_TABLENAME,
                         PVID: PVID,
                         RECID: RECID,
                         SUBJID: lblSubject.Text,
                         MODULEID: SYNC_MODULEID,
                         VISITNUM: VISITID,
                         IsComplete: true,
                         IsMissing: false,
                         INVID: lblSite.Text
                         );

                        dal_dm.DM_GetSetPV(
                        PVID: PVID,
                        SUBJID: lblSubject.Text,
                        MODULEID: SYNC_MODULEID,
                        VISITNUM: VISITID,
                        PAGESTATUS: "1",
                        HasMissing: false,
                        INVID: lblSite.Text
                        );

                        dal.ESOURCE_INSERT_MODULE_DATA_SP(
                         ACTION: "INSERT_MODULE_DATA",
                         INSERTQUERY: syncINSERTQUERY.Replace("DM_DATA_", "ESOURCE_DATA_"),
                         UPDATEQUERY: syncUPDATEQUERY.Replace("DM_DATA_", "ESOURCE_DATA_"),
                         TABLENAME: SYNC_TABLENAME.Replace("DM_DATA_", "ESOURCE_DATA_"),
                         RECID: RECID,
                         SUBJID: hfSUBJID.Value,
                         MODULEID: SYNC_MODULEID,
                         VISITNUM: VISITID,
                         IsComplete: false,
                         IsMissing: false,
                         FREEZESTATUS: "False",
                         MODULENAME: SYNC_MODULENAME,
                         VISIT: VISIT
                         );

                        dal.ESOURCE_GetSetPV(
                        PVID: PVID,
                        SUBJID: hfSUBJID.Value,
                        MODULEID: SYNC_MODULEID,
                        VISITNUM: VISITID,
                        PAGESTATUS: "1",
                        HasMissing: false,
                        FREEZESTATUS: "False",
                        MODULENAME: SYNC_MODULENAME,
                        VISIT: VISIT
                        );

                        if (dt_DM_AuditTrail.Rows.Count > 0)
                        {
                            SqlConnection con = new SqlConnection(dal.getconstr());

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
                                sqlbc.ColumnMappings.Add("TABLENAME", "TABLENAME");
                                sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                                sqlbc.ColumnMappings.Add("FIELDNAME", "FIELDNAME");
                                sqlbc.ColumnMappings.Add("OLDVALUE", "OLDVALUE");
                                sqlbc.ColumnMappings.Add("NEWVALUE", "NEWVALUE");
                                sqlbc.ColumnMappings.Add("REASON", "REASON");
                                sqlbc.ColumnMappings.Add("COMMENTS", "COMMENTS");
                                sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                                sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                                sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                                sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");
                                sqlbc.ColumnMappings.Add("DM", "DM");

                                sqlbc.WriteToServer(dt_DM_AuditTrail);

                                dt_DM_AuditTrail.Clear();
                            }
                        }

                    }
                }

                dal_IWRS.IWRS_SYNC_SP(ACTION: "UPDATE_DM_AUDITTRAIL_FIELDNAME", SUBJID: lblSubject.Text);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPLOAD_IWRS_AUDITTRAIL()
        {
            try
            {
                //Insert Bulk Audit Trail of IWRS
                if (dt_AuditTrail.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "NIWRS_AUDITTRAIL";

                        sqlbc.ColumnMappings.Add("SOURCE", "SOURCE");
                        sqlbc.ColumnMappings.Add("DCFID", "DCFID");
                        sqlbc.ColumnMappings.Add("REASON", "REASON");
                        sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                        sqlbc.ColumnMappings.Add("MODULENAME", "MODULENAME");
                        sqlbc.ColumnMappings.Add("TABLENAME", "TABLENAME");
                        sqlbc.ColumnMappings.Add("FIELDNAME", "FIELDNAME");
                        sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                        sqlbc.ColumnMappings.Add("OLDVALUE", "OLDVALUE");
                        sqlbc.ColumnMappings.Add("NEWVALUE", "NEWVALUE");
                        sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                        sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                        sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                        sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");

                        sqlbc.WriteToServer(dt_AuditTrail);

                        dt_AuditTrail.Clear();
                    }
                }

                dt_AuditTrail = new DataTable();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CREATE_IWRS_AUDITTRAIL_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (dt_AuditTrail.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    dt_AuditTrail.Columns.Add("SOURCE");
                    dt_AuditTrail.Columns.Add("DCFID");
                    dt_AuditTrail.Columns.Add("REASON");
                    dt_AuditTrail.Columns.Add("SUBJID");
                    dt_AuditTrail.Columns.Add("MODULENAME");
                    dt_AuditTrail.Columns.Add("TABLENAME");
                    dt_AuditTrail.Columns.Add("FIELDNAME");
                    dt_AuditTrail.Columns.Add("VARIABLENAME");
                    dt_AuditTrail.Columns.Add("OLDVALUE");
                    dt_AuditTrail.Columns.Add("NEWVALUE");
                    dt_AuditTrail.Columns.Add("ENTEREDDAT");
                    dt_AuditTrail.Columns.Add("ENTEREDBY");
                    dt_AuditTrail.Columns.Add("ENTEREDBYNAME");
                    dt_AuditTrail.Columns.Add("ENTERED_TZVAL");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void IWRS_ADD_NEW_ROW_DATA(string DCFID, string REASON, string SUBJID, string MODULENAME, string TABLENAME, string FIELDNAME, string VARIABLENAME, string OLDVALUE, string NEWVALUE)
        {
            try
            {
                CREATE_IWRS_AUDITTRAIL_DT();

                DataRow myDataRow;
                myDataRow = dt_AuditTrail.NewRow();
                myDataRow["SOURCE"] = "IWRS";
                myDataRow["DCFID"] = DCFID;
                myDataRow["REASON"] = REASON;
                myDataRow["SUBJID"] = SUBJID;
                myDataRow["MODULENAME"] = MODULENAME;
                myDataRow["TABLENAME"] = TABLENAME.Trim();
                myDataRow["FIELDNAME"] = FIELDNAME.Trim();
                myDataRow["VARIABLENAME"] = VARIABLENAME.Trim();
                myDataRow["OLDVALUE"] = REMOVEHTML(OLDVALUE).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["NEWVALUE"] = REMOVEHTML(NEWVALUE).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                dt_AuditTrail.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
            }
        }

        protected static string REMOVEHTML(string str)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");

            return str;
        }

        protected void DM_ADD_NEW_ROW_DATA(string PVID, string RECID, string SUBJID, string VISITNUM, string MODULENAME, string TABLENAME, string VariableName, string OldValue, string NewValue, string Reason, string Comment)
        {
            try
            {
                CREATE_DM_AUDITTRAIL_DT();

                DataRow myDataRow;
                myDataRow = dt_DM_AuditTrail.NewRow();
                myDataRow["SOURCE"] = "IWRS";
                myDataRow["PVID"] = PVID;
                myDataRow["RECID"] = RECID;
                myDataRow["SUBJID"] = SUBJID;
                myDataRow["VISITNUM"] = VISITNUM;
                myDataRow["MODULENAME"] = MODULENAME;
                myDataRow["TABLENAME"] = TABLENAME;
                myDataRow["VARIABLENAME"] = VariableName.Trim();
                myDataRow["FIELDNAME"] = "";
                myDataRow["REASON"] = Reason;
                myDataRow["COMMENTS"] = Comment;
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                myDataRow["OLDVALUE"] = REMOVEHTML(OldValue).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["NEWVALUE"] = REMOVEHTML(NewValue).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["DM"] = "True";
                dt_DM_AuditTrail.Rows.Add(myDataRow);
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

                if (dt_DM_AuditTrail.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    dt_DM_AuditTrail.Columns.Add("SOURCE");
                    dt_DM_AuditTrail.Columns.Add("PVID");
                    dt_DM_AuditTrail.Columns.Add("RECID");
                    dt_DM_AuditTrail.Columns.Add("SUBJID");
                    dt_DM_AuditTrail.Columns.Add("VISITNUM");
                    dt_DM_AuditTrail.Columns.Add("MODULENAME");
                    dt_DM_AuditTrail.Columns.Add("TABLENAME");
                    dt_DM_AuditTrail.Columns.Add("VARIABLENAME");
                    dt_DM_AuditTrail.Columns.Add("FIELDNAME");
                    dt_DM_AuditTrail.Columns.Add("OLDVALUE");
                    dt_DM_AuditTrail.Columns.Add("NEWVALUE");
                    dt_DM_AuditTrail.Columns.Add("REASON");
                    dt_DM_AuditTrail.Columns.Add("COMMENTS");
                    dt_DM_AuditTrail.Columns.Add("ENTEREDBY");
                    dt_DM_AuditTrail.Columns.Add("ENTEREDBYNAME");
                    dt_DM_AuditTrail.Columns.Add("ENTEREDDAT");
                    dt_DM_AuditTrail.Columns.Add("ENTERED_TZVAL");
                    dt_DM_AuditTrail.Columns.Add("DM");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public bool ISDATE(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ISNUMERIC(string number)
        {
            try
            {
                int n;
                bool isNumeric = int.TryParse(number, out n);
                return isNumeric;
            }
            catch
            {
                return false;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = "";
                ddlAction.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdVisitDates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.RowIndex == 0)
                    {
                        TextBox txtVISIT_DATE = (TextBox)e.Row.FindControl("txtVISIT_DATE");
                        txtVISIT_DATE.Enabled = false;

                        TextBox txtEARLY_DATE = (TextBox)e.Row.FindControl("txtEARLY_DATE");
                        txtEARLY_DATE.Enabled = false;

                        TextBox txtLATE_DATE = (TextBox)e.Row.FindControl("txtLATE_DATE");
                        txtLATE_DATE.Enabled = false;
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