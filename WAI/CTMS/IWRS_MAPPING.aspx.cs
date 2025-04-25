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
    public partial class IWRS_MAPPING : System.Web.UI.Page
    {
        DAL dal = new DAL();

        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetStructure(grd_Data);
            }
            catch (Exception ex)
            {

            }
        }

        private void GetStructure(GridView grd)
        {
            try
            {
                DataSet ds;
                ds = new DataSet();

                DataSet dsFORM = dal_IWRS.IWRS_MAPPING_SP(ACTION: "GETMODULE_FOR_REPORT", FORMID: Request.QueryString["ID"].ToString());

                if (dsFORM.Tables.Count > 0 && dsFORM.Tables[0].Rows.Count > 0)
                {
                    hdnMODULEID.Value = dsFORM.Tables[0].Rows[0]["MODULEID"].ToString();
                    hdnMODULENAME.Value = dsFORM.Tables[0].Rows[0]["MODULENAME"].ToString();
                    lblModuleName.Text = dsFORM.Tables[0].Rows[0]["FORMNAME"].ToString();
                }

                ds = dal_IWRS.IWRS_MAPPING_SP(ACTION: "GET_STRUCTURE", MODULEID: hdnMODULEID.Value, FORMID: Request.QueryString["ID"].ToString());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblModuleName.Visible = true;
                    grd.DataSource = ds.Tables[0];
                    grd.DataBind();
                }
                else
                {
                    lblModuleName.Text = "";
                    lblModuleName.Visible = false;
                    grd.DataSource = null;
                    grd.DataBind();
                }
            }
            catch (Exception ex)
            {
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

                    string ID = dr["ID"].ToString();
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

                    string MANDATORY = dr["MANDATORY"].ToString();

                    string DefaultData = dr["DefaultData"].ToString();

                    Label lblDisplayFeature = (Label)e.Row.FindControl("lblDisplayFeature");
                    Label lblDataSignificance = (Label)e.Row.FindControl("lblDataSignificance");
                    Label lblDataLinkages = (Label)e.Row.FindControl("lblDataLinkages");
                    Label lblMultipleDataEntry = (Label)e.Row.FindControl("lblMultipleDataEntry");
                    string INVISIBLE = dr["INVISIBLE"].ToString();

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Bold YN";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Bold YN";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Mask Field YN";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Mask Field YN";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Font.Underline = true;

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Underline YN";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Underline YN";
                        }
                    }

                    if (READYN == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Read only YN";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Read only YN";
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
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " Freetext YN";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", Freetext YN";
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

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                           ACTION: "GET_DRP_DWN_MASTER",
                           VARIABLENAME: VARIABLENAME,
                           FORMID: Request.QueryString["ID"].ToString()
                           );

                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

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

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    }
                    if (CONTROLTYPE == "RADIOBUTTON")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                        repeat_RAD.Visible = true;

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    DataSet ds1 = dal_IWRS.IWRS_MAPPING_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: hdnMODULEID.Value,
                        FORMID: Request.QueryString["ID"].ToString(),
                        FIELDID: ID
                        );

                    grd_Data1.DataSource = ds1.Tables[0];
                    grd_Data1.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Data1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["ID"].ToString();
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

                    string MANDATORY = dr["MANDATORY"].ToString();

                    string DefaultData = dr["DefaultData"].ToString();

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

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Bold YN";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Bold YN";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Mask Field YN";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Mask Field YN";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Font.Underline = true;

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Underline YN";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Underline YN";
                        }
                    }

                    if (READYN == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Read only YN";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Read only YN";
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
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " Freetext YN";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", Freetext YN";
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

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                           ACTION: "GET_DRP_DWN_MASTER",
                           VARIABLENAME: VARIABLENAME,
                           FORMID: Request.QueryString["ID"].ToString()
                           );

                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

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

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    }
                    if (CONTROLTYPE == "RADIOBUTTON")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                        repeat_RAD.Visible = true;

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    DataSet ds1 = dal_IWRS.IWRS_MAPPING_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: hdnMODULEID.Value,
                        FORMID: Request.QueryString["ID"].ToString(),
                        FIELDID: ID
                        );

                    grd_Data2.DataSource = ds1.Tables[0];
                    grd_Data2.DataBind();
                }
            }
            catch (Exception ex)
            {
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

                    string ID = dr["ID"].ToString();
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

                    string MANDATORY = dr["MANDATORY"].ToString();

                    string DefaultData = dr["DefaultData"].ToString();

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

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Bold YN";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Bold YN";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Mask Field YN";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Mask Field YN";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Font.Underline = true;

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Underline YN";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Underline YN";
                        }
                    }

                    if (READYN == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Read only YN";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Read only YN";
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
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " Freetext YN";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", Freetext YN";
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

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                           ACTION: "GET_DRP_DWN_MASTER",
                           VARIABLENAME: VARIABLENAME,
                           FORMID: Request.QueryString["ID"].ToString()
                           );

                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

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

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    }
                    if (CONTROLTYPE == "RADIOBUTTON")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                        repeat_RAD.Visible = true;

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    DataSet ds1 = dal_IWRS.IWRS_MAPPING_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: hdnMODULEID.Value,
                        FORMID: Request.QueryString["ID"].ToString(),
                        FIELDID: ID
                        );

                    grd_Data3.DataSource = ds1.Tables[0];
                    grd_Data3.DataBind();
                }
            }
            catch (Exception ex)
            {
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

                    string ID = dr["ID"].ToString();
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

                    string MANDATORY = dr["MANDATORY"].ToString();

                    string DefaultData = dr["DefaultData"].ToString();

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

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Bold YN";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Bold YN";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Mask Field YN";
                        }
                        else
                        {
                            {
                                lblDisplayFeature.Text += ", Mask Field YN";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Font.Underline = true;

                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Underline YN";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Underline YN";
                        }
                    }

                    if (READYN == "True")
                    {
                        if (lblDisplayFeature.Text == "")
                        {
                            lblDisplayFeature.Text = " Read only YN";
                        }
                        else
                        {
                            lblDisplayFeature.Text += ", Read only YN";
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
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);

                            if (lblDisplayFeature.Text == "")
                            {
                                lblDisplayFeature.Text = " Freetext YN";
                            }
                            else
                            {
                                lblDisplayFeature.Text += ", Freetext YN";
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

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                           ACTION: "GET_DRP_DWN_MASTER",
                           VARIABLENAME: VARIABLENAME,
                           FORMID: Request.QueryString["ID"].ToString()
                           );

                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

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

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                    }
                    if (CONTROLTYPE == "RADIOBUTTON")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                        repeat_RAD.Visible = true;

                        DataSet ds = dal_IWRS.IWRS_MAPPING_SP(
                               ACTION: "GET_DRP_DWN_MASTER",
                               VARIABLENAME: VARIABLENAME,
                               FORMID: Request.QueryString["ID"].ToString()
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
                throw;
            }
        }
    }
}