using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;

namespace CTMS
{
    public partial class NIWRS_DCF_Req : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SUBJECTTEXT.Text = Session["SUBJECTTEXT"].ToString();
                    GET_SITE();
                    GET_SUBSITE();
                    GET_SUBJECT();
                    GET_VISIT();
                    GET_FORMS();
                    GET_FIELDS();
                    GET_DATA();
                    GET_REASON();
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SelectOther();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_REASON()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_OPTION", STRATA: "DCF_REASON");
                drpReason.DataSource = ds.Tables[0];
                drpReason.DataValueField = "TEXT";
                drpReason.DataBind();
                drpReason.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVID";
                        ddlSite.DataBind();
                    }
                    else
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVID";
                        ddlSite.DataBind();
                        ddlSite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
                else
                {
                    ddlSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBSITE()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_SUBSITE", SITEID: ddlSite.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlSubSite.DataSource = ds.Tables[0];
                        ddlSubSite.DataValueField = "SubSiteID";
                        ddlSubSite.DataTextField = "SubSiteID";
                        ddlSubSite.DataBind();
                    }
                    else
                    {
                        ddlSubSite.DataSource = ds.Tables[0];
                        ddlSubSite.DataValueField = "SubSiteID";
                        ddlSubSite.DataTextField = "SubSiteID";
                        ddlSubSite.DataBind();
                        ddlSubSite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
                else
                {
                    ddlSubSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBJECT()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_SUBJECT_DCF", SITEID: ddlSite.SelectedValue, SUBSITEID: ddlSubSite.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSubject.DataSource = ds.Tables[0];
                    ddlSubject.DataValueField = "SUBJID";
                    ddlSubject.DataTextField = "SUBJID";
                    ddlSubject.DataBind();
                    ddlSubject.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlSubject.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_VISIT()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_VISIT", SITEID: ddlSubject.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlVisit.DataSource = ds.Tables[0];
                        ddlVisit.DataValueField = "ID";
                        ddlVisit.DataTextField = "VISIT";
                        ddlVisit.DataBind();
                    }
                    else
                    {
                        ddlVisit.DataSource = ds.Tables[0];
                        ddlVisit.DataValueField = "ID";
                        ddlVisit.DataTextField = "VISIT";
                        ddlVisit.DataBind();
                        ddlVisit.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
                else
                {
                    ddlVisit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_FORMS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_MODULE_DCF", VISIT: ddlVisit.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlForm.DataSource = ds.Tables[0];
                        ddlForm.DataValueField = "Source_ID";
                        ddlForm.DataTextField = "HEADER";
                        ddlForm.DataBind();
                    }
                    else
                    {
                        ddlForm.DataSource = ds.Tables[0];
                        ddlForm.DataValueField = "Source_ID";
                        ddlForm.DataTextField = "HEADER";
                        ddlForm.DataBind();
                        ddlForm.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
                else
                {
                    ddlForm.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_FIELDS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_FIELD_DCF", FormCode: ddlForm.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ddlField.DataSource = ds.Tables[0];
                        ddlField.DataValueField = "ID";
                        ddlField.DataTextField = "FIELDNAME";
                        ddlField.DataBind();
                    }
                    else
                    {
                        ddlField.DataSource = ds.Tables[0];
                        ddlField.DataValueField = "ID";
                        ddlField.DataTextField = "FIELDNAME";
                        ddlField.DataBind();
                        ddlField.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
                else
                {
                    ddlField.Items.Clear();
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
                DataSet ds = dal_IWRS.IWRS_STRUCTURE_SP(ACTION: "GET_STRUCTURE_DCF", ID: ddlField.SelectedValue, FormCode: ddlForm.SelectedValue);

                TXT_FIELD_new.Visible = false;
                TXT_FIELD_old.Visible = false;

                DRP_FIELD_new.Visible = false;
                DRP_FIELD_old.Visible = false;

                repeat_CHK_new.Visible = false;
                repeat_CHK_old.Visible = false;

                repeat_RAD_new.Visible = false;
                repeat_RAD_old.Visible = false;

                TXT_FIELD_old.Enabled = true;
                DRP_FIELD_old.Enabled = true;

                TXT_FIELD_new.Text = "";
                TXT_FIELD_old.Text = "";

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    divData.Visible = true;

                    DataRow dr = ds.Tables[0].Rows[0];

                    string ID = dr["ID"].ToString();
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string IWRS_TABLENAME = dr["IWRS_TABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();
                    string Mandatory = dr["Mandatory"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

                    DataSet dsDATA = dal_IWRS.IWRS_DCF_SP(ACTION: "GET_OLD_DATA_DCF", TABLENAME: IWRS_TABLENAME, VARIABLENAME: VARIABLENAME, SUBJID: ddlSubject.SelectedValue, VISIT: ddlVisit.SelectedValue);

                    if (dsDATA.Tables[0].Rows.Count > 0)
                    {
                        string oldDATA = "";
                        if (dsDATA.Tables.Count > 0)
                        {
                            if (dsDATA.Tables[0].Rows.Count > 0)
                            {
                                oldDATA = dsDATA.Tables[0].Rows[0][0].ToString();
                            }
                        }

                        hfControlType.Value = CONTROLTYPE;

                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            TXT_FIELD_old.Text = oldDATA;

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

                            if (Mandatory == "True")
                            {
                                TXT_FIELD_new.CssClass = TXT_FIELD_new.CssClass + " Mandatory";
                                TXT_FIELD_old.CssClass = TXT_FIELD_old.CssClass + " Mandatory";
                            }

                            TXT_FIELD_old.Enabled = false;
                        }
                        if (CONTROLTYPE == "DROPDOWN")
                        {
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

                            DRP_FIELD_old.SelectedValue = oldDATA;


                            if (AnsColor != "")
                            {
                                DRP_FIELD_new.Style.Add("color", AnsColor);
                                DRP_FIELD_old.Style.Add("color", AnsColor);
                            }

                            if (Mandatory == "True")
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

                            string[] stringArray = oldDATA.Split(',');
                            foreach (string x in stringArray)
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

                            string[] stringArray = oldDATA.Split(',');
                            foreach (string x in stringArray)
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
                        }
                    }
                    else
                    {
                        Response.Write("<script> alert('No Data Available for this Visit of this Subject.'); window.location.href = '" + Request.RawUrl.ToString() + "'; </script>");
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

        private void INSERT_DCF()
        {
            try
            {
                string Value1 = "", Value2 = "";
                string CONTROLTYPE = hfControlType.Value;

                if (CONTROLTYPE == "TEXTBOX")
                {
                    Value1 = TXT_FIELD_old.Text;
                    Value2 = TXT_FIELD_new.Text;
                }
                else if (CONTROLTYPE == "DROPDOWN")
                {
                    Value1 = DRP_FIELD_old.SelectedValue;
                    Value2 = DRP_FIELD_new.SelectedValue;
                }
                else if (CONTROLTYPE == "CHECKBOX")
                {
                    for (int a = 0; a < repeat_CHK_old.Items.Count; a++)
                    {
                        if (((CheckBox)repeat_CHK_old.Items[a].FindControl("CHK_FIELD_old")).Checked == true)
                        {
                            if (Value1.ToString() == "")
                            {
                                Value1 = ((CheckBox)repeat_CHK_old.Items[a].FindControl("CHK_FIELD_old")).Text.ToString();
                            }
                            else
                            {
                                Value1 += "," + ((CheckBox)repeat_CHK_old.Items[a].FindControl("CHK_FIELD_old")).Text.ToString();
                            }
                        }
                    }

                    for (int a = 0; a < repeat_CHK_new.Items.Count; a++)
                    {
                        if (((CheckBox)repeat_CHK_new.Items[a].FindControl("CHK_FIELD_new")).Checked == true)
                        {
                            if (Value1.ToString() == "")
                            {
                                Value2 = ((CheckBox)repeat_CHK_new.Items[a].FindControl("CHK_FIELD_new")).Text.ToString();
                            }
                            else
                            {
                                Value2 += "," + ((CheckBox)repeat_CHK_new.Items[a].FindControl("CHK_FIELD_new")).Text.ToString();
                            }
                        }
                    }
                }
                else if (CONTROLTYPE == "RADIOBUTTON")
                {
                    for (int a = 0; a < repeat_RAD_old.Items.Count; a++)
                    {
                        if (((RadioButton)repeat_RAD_old.Items[a].FindControl("RAD_FIELD_old")).Checked == true)
                        {
                            Value1 = ((RadioButton)repeat_RAD_old.Items[a].FindControl("RAD_FIELD_old")).Text.ToString();
                        }
                    }

                    for (int a = 0; a < repeat_RAD_new.Items.Count; a++)
                    {
                        if (((RadioButton)repeat_RAD_new.Items[a].FindControl("RAD_FIELD_new")).Checked == true)
                        {
                            Value2 = ((RadioButton)repeat_RAD_new.Items[a].FindControl("RAD_FIELD_new")).Text.ToString();
                        }
                    }
                }
                string DCFID = "";
                if (drpReason.SelectedValue == "Others")
                {
                    DataSet ds =  dal_IWRS.IWRS_DCF_SP(
                    ACTION: "INSERT_DCF",
                    SITEID: ddlSite.SelectedValue,
                    SUBSITEID: ddlSubSite.SelectedValue,
                    SUBJID: ddlSubject.SelectedValue,
                    VISIT: ddlVisit.SelectedValue,
                    DESCRIPTION: txtDesc.Text,
                    FormHeader: ddlForm.SelectedItem.Text,
                    FIELDID: ddlField.SelectedValue,
                    Value1: Value1,
                    Value2: Value2,
                    ERR_MSG: txtReason.Text,
                    ENTEREDBY: Session["User_ID"].ToString()
                    );

                    DCFID= ds.Tables[0].Rows[0]["ID"].ToString();

                }
                else
                {

                    DataSet ds =  dal_IWRS.IWRS_DCF_SP(
                    ACTION: "INSERT_DCF",
                    SITEID: ddlSite.SelectedValue,
                    SUBJID: ddlSubject.SelectedValue,
                    VISIT: ddlVisit.SelectedValue,
                    DESCRIPTION: txtDesc.Text,
                    FormHeader: ddlForm.SelectedItem.Text,
                    FIELDID: ddlField.SelectedValue,
                    Value1: Value1,
                    Value2: Value2,
                    ERR_MSG: drpReason.SelectedValue,
                    ENTEREDBY: Session["User_ID"].ToString()
                    );

                    DCFID = ds.Tables[0].Rows[0]["ID"].ToString();
                }

                SEND_MAIL(DCFID);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBSITE();
                GET_SUBJECT();
                GET_VISIT();
                GET_FORMS();
                GET_FIELDS();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSubSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBJECT();
                GET_VISIT();
                GET_FORMS();
                GET_FIELDS();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_VISIT();
                GET_FORMS();
                GET_FIELDS();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_FORMS();
                GET_FIELDS();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_FIELDS();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_DATA();
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
                if (drpReason.SelectedValue == "Others" && txtReason.Text == "")
                {
                    Response.Write("<script> alert('Please Specify Others. '); </script>");
                }
                else
                {
                    INSERT_DCF();

                    //Response.Redirect("NIWRS_DCF_Req.aspx");

                    Response.Write("<script> alert('DCF Requested Successfully. '); window.location.href = '" + Request.RawUrl.ToString() + "'; </script>");
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
                Response.Redirect(Request.RawUrl.ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL(string DCFID)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS_SITE", STRATA: "DCF", SITEID: ddlSite.SelectedValue);

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "Data_Change_Request_Generated");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[SITEID]", ddlSite.SelectedValue);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[SITEID]", ddlSite.SelectedValue);
                    BODY = BODY.Replace("[DCFID]", DCFID);
                    BODY = BODY.Replace("[SCREENINGID]", ddlSubject.SelectedValue);
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
    }
}