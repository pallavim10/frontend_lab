using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DB_VISIT_ANNOTATED_CRF_MAPPING : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                {
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
                DataSet ds = dal_DB.DB_ANNOTATED_PRINT_SP(ACTION: "GET_USER_SYSTEMS");

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
                GET_VISITS();
                GetModule();
                hdnMODULEID.Value = drpModule.SelectedValue;

                grd_Data.DataSource = null;
                grd_Data.DataBind();

                lblModuleName.Text = "";
                lblModuleName.Visible = false;
                grd_Data.DataSource = null;
                grd_Data.DataBind();
                divModuleCrit.Visible = false;
                divOnsubmitCrit.Visible = false;
                divOnChangeCrit.Visible = false;
                divLabDefaults.Visible = false;
                divCoding.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_VISITS()
        {
            try
            {
                DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_VISIT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModuleVisit.DataSource = ds.Tables[0];
                    drpModuleVisit.DataValueField = "VISITNUM";
                    drpModuleVisit.DataTextField = "VISIT";
                    drpModuleVisit.DataBind();
                    drpModuleVisit.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpModuleVisit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpModuleVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetModule();

                lblModuleName.Text = "";
                lblVisit.Text = "";
                divgrd.Visible = false;
                hdnMODULEID.Value = drpModule.SelectedValue;
                grd_Data.DataSource = null;
                grd_Data.DataBind();
                divModuleCrit.Visible = false;
                divVisitCrit.Visible = false;
                divOnsubmitCrit.Visible = false;
                divOnChangeCrit.Visible = false;
                divLabDefaults.Visible = false;
                divCoding.Visible = false;
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
                DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_Added_PROJECT_MASTER",
                    VISITNUM: drpModuleVisit.SelectedValue,
                    SYSTEM: drpSystem.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds.Tables[0];
                    drpModule.DataValueField = "MODULEID";
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
                throw ex;
            }
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblModuleName.Text = "";
                lblVisit.Text = "";
                hdnMODULEID.Value = drpModule.SelectedValue;
                divgrd.Visible = false;
                grd_Data.DataSource = null;
                grd_Data.DataBind();
                divModuleCrit.Visible = false;
                divVisitCrit.Visible = false;
                divOnsubmitCrit.Visible = false;
                divOnChangeCrit.Visible = false;
                divLabDefaults.Visible = false;
                divCoding.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnShowAnnotatedModules_Click(object sender, EventArgs e)
        {
            try
            {
                GetStructure(grd_Data);
                GET_OnSubmit_Crit();
                GET_OnChange_Crit();
                GET_MODULE_CRITs();
                GET_LAB_DEFAULTS_DATA();
                SHOW_CODING_DATA();
                GET_VISIT_CRITs();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('DB_ANNOTATED_VISIT_MODULES_PRINT.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITNUM=" + drpModuleVisit.SelectedValue + "&VISIT=" + drpModuleVisit.SelectedItem.Text + "&SYSTEM=" + drpSystem.SelectedItem.Text + "','_newtab');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDownloadAllModules_Click(object sender, EventArgs e)
        {
            try
            {
                string MODULEID = "", MODULENAME = "";
                if (drpModule.SelectedValue == "")
                {
                    MODULEID = "0";
                    MODULENAME = "";
                }
                else
                {
                    MODULEID = drpModule.SelectedValue;
                    MODULENAME = drpModule.SelectedItem.Text;
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('DB_ANNOTATED_VISIT_MODULES_PRINT.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISIT=" + drpModuleVisit.SelectedItem.Text + "&VISITNUM=" + drpModuleVisit.SelectedValue + "&SYSTEM=" + drpSystem.SelectedItem.Text + "','_newtab');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetStructure(GridView grd)
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                        ACTION: "GET_STRUCTURE",
                        MODULEID: drpModule.SelectedValue,
                        VISITNUM: drpModuleVisit.SelectedValue,
                        SYSTEM: drpSystem.SelectedValue
                        );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblModuleName.Text = drpModule.SelectedItem.Text;
                    lblVisit.Text = drpModuleVisit.SelectedItem.Text;
                    divgrd.Visible = true;
                    grd.DataSource = ds.Tables[0];
                    grd.DataBind();
                }
                else
                {
                    lblModuleName.Text = "";
                    lblVisit.Text = "";
                    divgrd.Visible = false;
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

                    Label lblDisplayFeature = (Label)e.Row.FindControl("lblDisplayFeature");
                    Label lblDataSignificance = (Label)e.Row.FindControl("lblDataSignificance");
                    Label lblDataLinkages = (Label)e.Row.FindControl("lblDataLinkages");
                    Label lblMultipleDataEntry = (Label)e.Row.FindControl("lblMultipleDataEntry");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string PGL_TYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Bold";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Bold";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Mask Field";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Mask Field";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Font.Underline = true;

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Underline";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Underline";
                        }
                    }

                    if (READYN == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Read only";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Read only";
                        }
                    }

                    if (MANDATORY == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Mandatory Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Mandatory Information";
                        }
                    }

                    if (REQUIREDYN == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Required Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Required Information";
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Critical Data Point";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Critical Data Point";
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Medical Authority Response";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Medical Authority Response";
                        }
                    }

                    if (PGL_TYPE != "")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = PGL_TYPE;
                        }
                        else
                        {
                            lblDataSignificance.Text += ", " + PGL_TYPE;
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " SDV/SDR";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", SDV/SDR";
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " Lab Referance Range";
                        }
                        else
                        {
                            lblDataLinkages.Text += ", Lab Referance Range";
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " AutoCode: " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblDataLinkages.Text += ", AutoCode: " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (DefaultData != "")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " Protocal Predefine Data: " + DefaultData;
                        }
                        else
                        {
                            lblDataLinkages.Text += ", Protocal Predefine Data: " + DefaultData;
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblMultipleDataEntry.Text == "")
                        {
                            lblMultipleDataEntry.Text = " Non-Repetitive";
                        }
                        else
                        {
                            lblMultipleDataEntry.Text += ", Non-Repetitive";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblMultipleDataEntry.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblMultipleDataEntry.Text = " In List Data (Editable)";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text = " In List Data";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblMultipleDataEntry.Text += ", In List Data (Editable)";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text += ", In List Data";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Duplicates Check Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Duplicates Check Information";
                        }
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN != "True")
                        {
                            if (REQUIREDYN == "True")
                            {
                                btnEdit.BackColor = System.Drawing.Color.Yellow;
                            }
                            else
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
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
                        }

                        if (AutoNum == "True")
                        {
                            if (lblMultipleDataEntry.Text == "")
                            {
                                lblMultipleDataEntry.Text = " Sequential Auto-Numbering";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text += ", Sequential Auto-Numbering";
                            }
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Width = 500;
                            btnEdit.Height = 100;
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " Freetext";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", Freetext";
                            }
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " UpperCase Only";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", UpperCase Only";
                            }
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

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Dataflow Field";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Dataflow Field";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }

                            if (lblMultipleDataEntry.Text == "")
                            {
                                if (dr["PrefixText"].ToString() != "")
                                {
                                    lblMultipleDataEntry.Text = " Prefix (" + dr["PrefixText"].ToString() + ")";
                                }
                                else
                                {
                                    lblMultipleDataEntry.Text = " Prefix";
                                }
                            }
                            else
                            {
                                if (dr["PrefixText"].ToString() != "")
                                {
                                    lblMultipleDataEntry.Text += ", Prefix (" + dr["PrefixText"].ToString() + ")";
                                }
                                else
                                {
                                    lblMultipleDataEntry.Text += ", Prefix";
                                }
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
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

                        if (btnEdit.Text == "")
                        {
                            if (DefaultData != "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                        }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();
                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }

                        if (REQUIREDYN == "True")
                        {
                            if (btnEdit.CssClass.Contains("ckeditor"))
                            {
                                HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                            }
                            else
                            {
                                btnEdit.CssClass += btnEdit.CssClass + " REQUIRED";
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

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Field(Child)";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Field(Child)";
                            }
                        }
                        else
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
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

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " ParentLinked";

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Field(Parent)";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Field(Parent)";
                            }
                        }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (REQUIREDYN == "True")
                        {
                            if (btnEdit.CssClass.Contains("ckeditor"))
                            {
                                HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                            }
                            else
                            {
                                btnEdit.CssClass += btnEdit.CssClass + " REQUIRED";
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

                        DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True")
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

                        for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                        {
                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
                        }

                        if (REQUIREDYN == "True")
                        {
                            HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                            divcontrol.Attributes.Add("style", "background-color: yellow;");
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

                        DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (READYN == "True")
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

                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                        {
                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
                        }

                        if (REQUIREDYN == "True")
                        {
                            HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                            divcontrol.Attributes.Add("style", "background-color: yellow;");
                        }
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    HtmlControl divDisplayFeatures = (HtmlControl)e.Row.FindControl("divDisplayFeatures");
                    if (lblDisplayFeature.Text != "")
                    {
                        divDisplayFeatures.Visible = true;
                    }
                    else
                    {
                        divDisplayFeatures.Visible = false;
                    }

                    HtmlControl divDataSignificance = (HtmlControl)e.Row.FindControl("divDataSignificance");
                    if (lblDataSignificance.Text != "")
                    {
                        divDataSignificance.Visible = true;
                    }
                    else
                    {
                        divDataSignificance.Visible = false;
                    }

                    HtmlControl divDataLinkages = (HtmlControl)e.Row.FindControl("divDataLinkages");
                    if (lblDataLinkages.Text != "")
                    {
                        divDataLinkages.Visible = true;
                    }
                    else
                    {
                        divDataLinkages.Visible = false;
                    }

                    HtmlControl divMultipleDataEntry = (HtmlControl)e.Row.FindControl("divMultipleDataEntry");
                    if (lblMultipleDataEntry.Text != "")
                    {
                        divMultipleDataEntry.Visible = true;
                    }
                    else
                    {
                        divMultipleDataEntry.Visible = false;
                    }

                    GridView grd_Data1 = e.Row.FindControl("grd_Data1") as GridView;
                    DataSet ds1 = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: drpModule.SelectedValue,
                        VISITNUM: drpModuleVisit.SelectedValue,
                        FIELDID: ID,
                        SYSTEM: drpSystem.SelectedValue
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

                    Label lblDisplayFeature = (Label)e.Row.FindControl("lblDisplayFeature");
                    Label lblDataSignificance = (Label)e.Row.FindControl("lblDataSignificance");
                    Label lblDataLinkages = (Label)e.Row.FindControl("lblDataLinkages");
                    Label lblMultipleDataEntry = (Label)e.Row.FindControl("lblMultipleDataEntry");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string PGL_TYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Bold";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Bold";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Mask Field";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Mask Field";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Font.Underline = true;

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Underline";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Underline";
                        }
                    }

                    if (READYN == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Read only";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Read only";
                        }
                    }

                    if (MANDATORY == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Mandatory Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Mandatory Information";
                        }
                    }

                    if (REQUIREDYN == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Required Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Required Information";
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Critical Data Point";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Critical Data Point";
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Medical Authority Response";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Medical Authority Response";
                        }
                    }

                    if (PGL_TYPE != "")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = PGL_TYPE;
                        }
                        else
                        {
                            lblDataSignificance.Text += ", " + PGL_TYPE;
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " SDV/SDR";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", SDV/SDR";
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " Lab Referance Range";
                        }
                        else
                        {
                            lblDataLinkages.Text += ", Lab Referance Range";
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " AutoCode: " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblDataLinkages.Text += ", AutoCode: " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (DefaultData != "")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " Protocal Predefine Data: " + DefaultData;
                        }
                        else
                        {
                            lblDataLinkages.Text += ", Protocal Predefine Data: " + DefaultData;
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblMultipleDataEntry.Text == "")
                        {
                            lblMultipleDataEntry.Text = " Non-Repetitive";
                        }
                        else
                        {
                            lblMultipleDataEntry.Text += ", Non-Repetitive";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblMultipleDataEntry.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblMultipleDataEntry.Text = " In List Data (Editable)";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text = " In List Data";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblMultipleDataEntry.Text += ", In List Data (Editable)";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text += ", In List Data";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Duplicates Check Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Duplicates Check Information";
                        }
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN != "True")
                        {
                            if (REQUIREDYN == "True")
                            {
                                btnEdit.BackColor = System.Drawing.Color.Yellow;
                            }
                            else
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
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
                        }

                        if (AutoNum == "True")
                        {
                            if (lblMultipleDataEntry.Text == "")
                            {
                                lblMultipleDataEntry.Text = " Sequential Auto-Numbering";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text += ", Sequential Auto-Numbering";
                            }
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Width = 500;
                            btnEdit.Height = 100;
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " Freetext";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", Freetext";
                            }
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " UpperCase Only";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", UpperCase Only";
                            }
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

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Dataflow Field";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Dataflow Field";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }

                            if (lblMultipleDataEntry.Text == "")
                            {
                                if (dr["PrefixText"].ToString() != "")
                                {
                                    lblMultipleDataEntry.Text = " Prefix (" + dr["PrefixText"].ToString() + ")";
                                }
                                else
                                {
                                    lblMultipleDataEntry.Text = " Prefix";
                                }
                            }
                            else
                            {
                                if (dr["PrefixText"].ToString() != "")
                                {
                                    lblMultipleDataEntry.Text += ", Prefix (" + dr["PrefixText"].ToString() + ")";
                                }
                                else
                                {
                                    lblMultipleDataEntry.Text += ", Prefix";
                                }
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
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

                        if (btnEdit.Text == "")
                        {
                            if (DefaultData != "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                        }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();
                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }

                        if (REQUIREDYN == "True")
                        {
                            if (btnEdit.CssClass.Contains("ckeditor"))
                            {
                                HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                            }
                            else
                            {
                                btnEdit.CssClass += btnEdit.CssClass + " REQUIRED";
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

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Field(Child)";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Field(Child)";
                            }
                        }
                        else
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
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

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " ParentLinked";

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Field(Parent)";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Field(Parent)";
                            }
                        }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (REQUIREDYN == "True")
                        {
                            if (btnEdit.CssClass.Contains("ckeditor"))
                            {
                                HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                            }
                            else
                            {
                                btnEdit.CssClass += btnEdit.CssClass + " REQUIRED";
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

                        DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True")
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

                        for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                        {
                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
                        }

                        if (REQUIREDYN == "True")
                        {
                            HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                            divcontrol.Attributes.Add("style", "background-color: yellow;");
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

                        DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (READYN == "True")
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

                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                        {
                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
                        }

                        if (REQUIREDYN == "True")
                        {
                            HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                            divcontrol.Attributes.Add("style", "background-color: yellow;");
                        }
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    HtmlControl divDisplayFeatures = (HtmlControl)e.Row.FindControl("divDisplayFeatures");
                    if (lblDisplayFeature.Text != "")
                    {
                        divDisplayFeatures.Visible = true;
                    }
                    else
                    {
                        divDisplayFeatures.Visible = false;
                    }

                    HtmlControl divDataSignificance = (HtmlControl)e.Row.FindControl("divDataSignificance");
                    if (lblDataSignificance.Text != "")
                    {
                        divDataSignificance.Visible = true;
                    }
                    else
                    {
                        divDataSignificance.Visible = false;
                    }

                    HtmlControl divDataLinkages = (HtmlControl)e.Row.FindControl("divDataLinkages");
                    if (lblDataLinkages.Text != "")
                    {
                        divDataLinkages.Visible = true;
                    }
                    else
                    {
                        divDataLinkages.Visible = false;
                    }

                    HtmlControl divMultipleDataEntry = (HtmlControl)e.Row.FindControl("divMultipleDataEntry");
                    if (lblMultipleDataEntry.Text != "")
                    {
                        divMultipleDataEntry.Visible = true;
                    }
                    else
                    {
                        divMultipleDataEntry.Visible = false;
                    }

                    GridView grd_Data2 = e.Row.FindControl("grd_Data2") as GridView;
                    DataSet ds1 = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: drpModule.SelectedValue,
                        VISITNUM: drpModuleVisit.SelectedValue,
                        FIELDID: ID,
                        SYSTEM: drpSystem.SelectedValue
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

                    Label lblDisplayFeature = (Label)e.Row.FindControl("lblDisplayFeature");
                    Label lblDataSignificance = (Label)e.Row.FindControl("lblDataSignificance");
                    Label lblDataLinkages = (Label)e.Row.FindControl("lblDataLinkages");
                    Label lblMultipleDataEntry = (Label)e.Row.FindControl("lblMultipleDataEntry");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string PGL_TYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Bold";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Bold";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Mask Field";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Mask Field";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Font.Underline = true;

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Underline";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Underline";
                        }
                    }

                    if (READYN == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Read only";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Read only";
                        }
                    }

                    if (MANDATORY == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Mandatory Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Mandatory Information";
                        }
                    }

                    if (REQUIREDYN == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Required Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Required Information";
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Critical Data Point";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Critical Data Point";
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Medical Authority Response";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Medical Authority Response";
                        }
                    }

                    if (PGL_TYPE != "")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = PGL_TYPE;
                        }
                        else
                        {
                            lblDataSignificance.Text += ", " + PGL_TYPE;
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " SDV/SDR";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", SDV/SDR";
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " Lab Referance Range";
                        }
                        else
                        {
                            lblDataLinkages.Text += ", Lab Referance Range";
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " AutoCode: " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblDataLinkages.Text += ", AutoCode: " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (DefaultData != "")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " Protocal Predefine Data: " + DefaultData;
                        }
                        else
                        {
                            lblDataLinkages.Text += ", Protocal Predefine Data: " + DefaultData;
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblMultipleDataEntry.Text == "")
                        {
                            lblMultipleDataEntry.Text = " Non-Repetitive";
                        }
                        else
                        {
                            lblMultipleDataEntry.Text += ", Non-Repetitive";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblMultipleDataEntry.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblMultipleDataEntry.Text = " In List Data (Editable)";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text = " In List Data";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblMultipleDataEntry.Text += ", In List Data (Editable)";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text += ", In List Data";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Duplicates Check Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Duplicates Check Information";
                        }
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN != "True")
                        {
                            if (REQUIREDYN == "True")
                            {
                                btnEdit.BackColor = System.Drawing.Color.Yellow;
                            }
                            else
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
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
                        }

                        if (AutoNum == "True")
                        {
                            if (lblMultipleDataEntry.Text == "")
                            {
                                lblMultipleDataEntry.Text = " Sequential Auto-Numbering";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text += ", Sequential Auto-Numbering";
                            }
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Width = 500;
                            btnEdit.Height = 100;
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " Freetext";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", Freetext";
                            }
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " UpperCase Only";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", UpperCase Only";
                            }
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

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Dataflow Field";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Dataflow Field";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }

                            if (lblMultipleDataEntry.Text == "")
                            {
                                if (dr["PrefixText"].ToString() != "")
                                {
                                    lblMultipleDataEntry.Text = " Prefix (" + dr["PrefixText"].ToString() + ")";
                                }
                                else
                                {
                                    lblMultipleDataEntry.Text = " Prefix";
                                }
                            }
                            else
                            {
                                if (dr["PrefixText"].ToString() != "")
                                {
                                    lblMultipleDataEntry.Text += ", Prefix (" + dr["PrefixText"].ToString() + ")";
                                }
                                else
                                {
                                    lblMultipleDataEntry.Text += ", Prefix";
                                }
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
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

                        if (btnEdit.Text == "")
                        {
                            if (DefaultData != "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                        }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();
                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }

                        if (REQUIREDYN == "True")
                        {
                            if (btnEdit.CssClass.Contains("ckeditor"))
                            {
                                HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                            }
                            else
                            {
                                btnEdit.CssClass += btnEdit.CssClass + " REQUIRED";
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

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Field(Child)";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Field(Child)";
                            }
                        }
                        else
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
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

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " ParentLinked";

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Field(Parent)";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Field(Parent)";
                            }
                        }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (REQUIREDYN == "True")
                        {
                            if (btnEdit.CssClass.Contains("ckeditor"))
                            {
                                HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                            }
                            else
                            {
                                btnEdit.CssClass += btnEdit.CssClass + " REQUIRED";
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

                        DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True")
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

                        for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                        {
                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
                        }

                        if (REQUIREDYN == "True")
                        {
                            HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                            divcontrol.Attributes.Add("style", "background-color: yellow;");
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

                        DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (READYN == "True")
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

                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                        {
                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
                        }

                        if (REQUIREDYN == "True")
                        {
                            HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                            divcontrol.Attributes.Add("style", "background-color: yellow;");
                        }
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    HtmlControl divDisplayFeatures = (HtmlControl)e.Row.FindControl("divDisplayFeatures");
                    if (lblDisplayFeature.Text != "")
                    {
                        divDisplayFeatures.Visible = true;
                    }
                    else
                    {
                        divDisplayFeatures.Visible = false;
                    }

                    HtmlControl divDataSignificance = (HtmlControl)e.Row.FindControl("divDataSignificance");
                    if (lblDataSignificance.Text != "")
                    {
                        divDataSignificance.Visible = true;
                    }
                    else
                    {
                        divDataSignificance.Visible = false;
                    }

                    HtmlControl divDataLinkages = (HtmlControl)e.Row.FindControl("divDataLinkages");
                    if (lblDataLinkages.Text != "")
                    {
                        divDataLinkages.Visible = true;
                    }
                    else
                    {
                        divDataLinkages.Visible = false;
                    }

                    HtmlControl divMultipleDataEntry = (HtmlControl)e.Row.FindControl("divMultipleDataEntry");
                    if (lblMultipleDataEntry.Text != "")
                    {
                        divMultipleDataEntry.Visible = true;
                    }
                    else
                    {
                        divMultipleDataEntry.Visible = false;
                    }

                    GridView grd_Data3 = e.Row.FindControl("grd_Data3") as GridView;
                    DataSet ds1 = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: drpModule.SelectedValue,
                        VISITNUM: drpModuleVisit.SelectedValue,
                        FIELDID: ID,
                        SYSTEM: drpSystem.SelectedValue
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

                    Label lblDisplayFeature = (Label)e.Row.FindControl("lblDisplayFeature");
                    Label lblDataSignificance = (Label)e.Row.FindControl("lblDataSignificance");
                    Label lblDataLinkages = (Label)e.Row.FindControl("lblDataLinkages");
                    Label lblMultipleDataEntry = (Label)e.Row.FindControl("lblMultipleDataEntry");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string PGL_TYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Bold";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Bold";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Mask Field";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Mask Field";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Font.Underline = true;

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Underline";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Underline";
                        }
                    }

                    if (READYN == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Read only";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Read only";
                        }
                    }

                    if (MANDATORY == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Mandatory Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Mandatory Information";
                        }
                    }

                    if (REQUIREDYN == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Required Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Required Information";
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Critical Data Point";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Critical Data Point";
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Medical Authority Response";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Medical Authority Response";
                        }
                    }

                    if (PGL_TYPE != "")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = PGL_TYPE;
                        }
                        else
                        {
                            lblDataSignificance.Text += ", " + PGL_TYPE;
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " SDV/SDR";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", SDV/SDR";
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " Lab Referance Range";
                        }
                        else
                        {
                            lblDataLinkages.Text += ", Lab Referance Range";
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " AutoCode: " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblDataLinkages.Text += ", AutoCode: " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (DefaultData != "")
                    {
                        if (lblDataLinkages.Text == "")
                        {
                            lblDataLinkages.Text = " Protocal Predefine Data: " + DefaultData;
                        }
                        else
                        {
                            lblDataLinkages.Text += ", Protocal Predefine Data: " + DefaultData;
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblMultipleDataEntry.Text == "")
                        {
                            lblMultipleDataEntry.Text = " Non-Repetitive";
                        }
                        else
                        {
                            lblMultipleDataEntry.Text += ", Non-Repetitive";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblMultipleDataEntry.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblMultipleDataEntry.Text = " In List Data (Editable)";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text = " In List Data";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblMultipleDataEntry.Text += ", In List Data (Editable)";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text += ", In List Data";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblDataSignificance.Text == "")
                        {
                            lblDataSignificance.Text = " Duplicates Check Information";
                        }
                        else
                        {
                            lblDataSignificance.Text += ", Duplicates Check Information";
                        }
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (((btnEdit.CssClass.Contains("txtDate") || btnEdit.CssClass.Contains("txtDateNoFuture")) && (!btnEdit.CssClass.Contains("txtDateMask"))) && READYN != "True")
                        {
                            if (REQUIREDYN == "True")
                            {
                                btnEdit.BackColor = System.Drawing.Color.Yellow;
                            }
                            else
                            {
                                btnEdit.BackColor = System.Drawing.Color.White;
                                btnEdit.Attributes.Add("readonly", "readonly");
                            }
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
                        }

                        if (AutoNum == "True")
                        {
                            if (lblMultipleDataEntry.Text == "")
                            {
                                lblMultipleDataEntry.Text = " Sequential Auto-Numbering";
                            }
                            else
                            {
                                lblMultipleDataEntry.Text += ", Sequential Auto-Numbering";
                            }
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Width = 500;
                            btnEdit.Height = 100;
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " Freetext";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", Freetext";
                            }
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " UpperCase Only";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", UpperCase Only";
                            }
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

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Dataflow Field";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Dataflow Field";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }

                            if (lblMultipleDataEntry.Text == "")
                            {
                                if (dr["PrefixText"].ToString() != "")
                                {
                                    lblMultipleDataEntry.Text = " Prefix (" + dr["PrefixText"].ToString() + ")";
                                }
                                else
                                {
                                    lblMultipleDataEntry.Text = " Prefix";
                                }
                            }
                            else
                            {
                                if (dr["PrefixText"].ToString() != "")
                                {
                                    lblMultipleDataEntry.Text += ", Prefix (" + dr["PrefixText"].ToString() + ")";
                                }
                                else
                                {
                                    lblMultipleDataEntry.Text += ", Prefix";
                                }
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
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

                        if (btnEdit.Text == "")
                        {
                            if (DefaultData != "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                        }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();
                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }

                        if (REQUIREDYN == "True")
                        {
                            if (btnEdit.CssClass.Contains("ckeditor"))
                            {
                                HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                            }
                            else
                            {
                                btnEdit.CssClass += btnEdit.CssClass + " REQUIRED";
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

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Field(Child)";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Field(Child)";
                            }
                        }
                        else
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
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

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " ParentLinked";

                            if (lblDataLinkages.Text == "")
                            {
                                lblDataLinkages.Text = " Linked Field(Parent)";
                            }
                            else
                            {
                                lblDataLinkages.Text += ", Linked Field(Parent)";
                            }
                        }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (REQUIREDYN == "True")
                        {
                            if (btnEdit.CssClass.Contains("ckeditor"))
                            {
                                HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                                divcontrol.Attributes.Add("style", "background-color: yellow;");
                            }
                            else
                            {
                                btnEdit.CssClass += btnEdit.CssClass + " REQUIRED";
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

                        DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True")
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

                        for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                        {
                            ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " " + VARIABLENAME;
                        }

                        if (REQUIREDYN == "True")
                        {
                            HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                            divcontrol.Attributes.Add("style", "background-color: yellow;");
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

                        DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(
                               ACTION: "GET_OPTIONS_LIST_VISIT",
                               VARIABLENAME: VARIABLENAME,
                               VISITNUM: drpModuleVisit.SelectedValue
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (READYN == "True")
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

                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                        {
                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " " + VARIABLENAME;
                        }

                        if (REQUIREDYN == "True")
                        {
                            HtmlControl divcontrol = (HtmlControl)e.Row.FindControl("divcontrol");
                            divcontrol.Attributes.Add("style", "background-color: yellow;");
                        }
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    HtmlControl divDisplayFeatures = (HtmlControl)e.Row.FindControl("divDisplayFeatures");
                    if (lblDisplayFeature.Text != "")
                    {
                        divDisplayFeatures.Visible = true;
                    }
                    else
                    {
                        divDisplayFeatures.Visible = false;
                    }

                    HtmlControl divDataSignificance = (HtmlControl)e.Row.FindControl("divDataSignificance");
                    if (lblDataSignificance.Text != "")
                    {
                        divDataSignificance.Visible = true;
                    }
                    else
                    {
                        divDataSignificance.Visible = false;
                    }

                    HtmlControl divDataLinkages = (HtmlControl)e.Row.FindControl("divDataLinkages");
                    if (lblDataLinkages.Text != "")
                    {
                        divDataLinkages.Visible = true;
                    }
                    else
                    {
                        divDataLinkages.Visible = false;
                    }

                    HtmlControl divMultipleDataEntry = (HtmlControl)e.Row.FindControl("divMultipleDataEntry");
                    if (lblMultipleDataEntry.Text != "")
                    {
                        divMultipleDataEntry.Visible = true;
                    }
                    else
                    {
                        divMultipleDataEntry.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void GET_OnSubmit_Crit()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "DM_GET_OnSubmit_CRIT",
                    MODULEID: hdnMODULEID.Value
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdOnSubmitCrit.DataSource = ds;
                    grdOnSubmitCrit.DataBind();
                    divOnsubmitCrit.Visible = true;
                }
                else
                {
                    grdOnSubmitCrit.DataSource = null;
                    grdOnSubmitCrit.DataBind();
                    divOnsubmitCrit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_OnChange_Crit()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_OnChange_CRIT_ALL",
                    MODULEID: hdnMODULEID.Value
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdOnChangeCrit.DataSource = ds;
                    grdOnChangeCrit.DataBind();
                    divOnChangeCrit.Visible = true;
                }
                else
                {
                    grdOnChangeCrit.DataSource = null;
                    grdOnChangeCrit.DataBind();
                    divOnChangeCrit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_MODULE_CRITs()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_MODULE_CRITs", MODULEID: hdnMODULEID.Value);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdModuleCrits.DataSource = ds;
                    grdModuleCrits.DataBind();
                    divModuleCrit.Visible = true;
                }
                else
                {
                    grdModuleCrits.DataSource = null;
                    grdModuleCrits.DataBind();
                    divModuleCrit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_VISIT_CRITs()
        {
            try
            {
                DataSet ds = dal_DB.DB_SETUP_CRITs_SP(ACTION: "GET_VISIT_CRITs", VISITNUM: drpModuleVisit.SelectedValue);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdVisitCrits.DataSource = ds;
                    grdVisitCrits.DataBind();
                    divVisitCrit.Visible = true;
                }
                else
                {
                    grdVisitCrits.DataSource = null;
                    grdVisitCrits.DataBind();
                    divVisitCrit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LAB_DEFAULTS_DATA()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_DEFAULT_MAPPING", MODULEID: hdnMODULEID.Value);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdLabDefaults.DataSource = ds.Tables[0];
                    grdLabDefaults.DataBind();
                    divLabDefaults.Visible = true;
                }
                else
                {
                    grdLabDefaults.DataSource = null;
                    grdLabDefaults.DataBind();
                    divLabDefaults.Visible = false;
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

        protected void SHOW_CODING_DATA()
        {
            try
            {
                DataSet AutoCodeLIB = dal.DB_CODE_SP(ACTION: "GET_AUTOCODELEB", MODULEID: drpModule.SelectedValue);

                if (AutoCodeLIB.Tables.Count > 0 && AutoCodeLIB.Tables[0].Rows.Count > 0)
                {
                    if (AutoCodeLIB.Tables[0].Rows[0]["AutoCodeLIB"].ToString() == "MedDRA")
                    {
                        GET_MEDDRADATA();
                    }
                    else if (AutoCodeLIB.Tables[0].Rows[0]["AutoCodeLIB"].ToString() == "WHODD")
                    {
                        GET_WHODDDATA();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void GET_MEDDRADATA()
        {
            try
            {
                DataSet ds = dal.DB_CODE_SP(ACTION: "GET_MEDDRA_MAPPING_RECOARDS", MODULEID: drpModule.SelectedValue);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdMeddraData.DataSource = ds;
                    grdMeddraData.DataBind();
                    divCoding.Visible = true;
                    divMeddraRecoard.Visible = true;
                    divWhoddRecoard.Visible = false;
                }
                else
                {
                    grdMeddraData.DataSource = null;
                    grdMeddraData.DataBind();
                    divCoding.Visible = false;
                    divMeddraRecoard.Visible = false;
                    divWhoddRecoard.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_WHODDDATA()
        {
            try
            {
                DataSet ds = dal.DB_CODE_SP(ACTION: "GET_WHODD_MAPPING_RECOARDS", MODULEID: drpModule.SelectedValue);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdWhoddData.DataSource = ds;
                    grdWhoddData.DataBind();
                    divCoding.Visible = true;
                    divMeddraRecoard.Visible = false;
                    divWhoddRecoard.Visible = true;
                }
                else
                {
                    grdWhoddData.DataSource = null;
                    grdWhoddData.DataBind();
                    divCoding.Visible = false;
                    divMeddraRecoard.Visible = false;
                    divWhoddRecoard.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdMeddraData_PreRender(object sender, EventArgs e)
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

        protected void grdWhoddData_PreRender(object sender, EventArgs e)
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
    }
}