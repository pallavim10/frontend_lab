﻿using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class DATA_ENTRY : System.Web.UI.Page
    {
        DAL_DE Dal_DE = new DAL_DE();

        DataTable EditDATA = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["SID"] != null)
                    {
                        hdnEditMode.Value = "true";
                        hdnEditSID.Value = Request.QueryString["SID"].ToString();
                    }
                    else
                    {
                        hdnEditMode.Value = "false";
                        hdnEditSID.Value = "";
                    }

                    GET_SITE();
                    GET_SID();
                    GET_SpecimenType();
                    GET_SUBJECT();
                    GET_VISIT();
                    GET_ENTRY_FIELDS();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void SHOW_MODAL_REASON(string FIELDNAME, string VARIABLENAME, string INDEX, string OLDVAL, string NEWVAL)
        {
            try
            {
                lblFieldName.Text = FIELDNAME;
                hdnReasonVARIABLENAME.Value = VARIABLENAME;
                hdnReasonINDEX.Value = INDEX;
                lblOldVal.Text = OLDVAL;
                lblNewVal.Text = NEWVAL;
                txtComment.Text = "";

                modalReason.Show();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void GET_ENTRY_FIELDS()
        {
            try
            {
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_ENTRY_FIELDS_First");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeatFields.DataSource = ds;
                    repeatFields.DataBind();
                }
                else
                {
                    repeatFields.DataSource = null;
                    repeatFields.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void GET_SpecimenType()
        {
            try
            {
                drpSpecimenType.Items.Clear();
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_SpecimenType", VARIABLENAME: "SPECTYP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSpecimenType.DataSource = ds.Tables[1];
                    drpSpecimenType.DataValueField = "OPTION_VALUE";
                    drpSpecimenType.DataBind();

                    if (ds.Tables[1].Rows.Count > 1)
                    {
                        drpSpecimenType.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }

                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    drpSpecimenType.SelectedValue = EditDATA.Rows[0]["SPECTYP"].ToString();
                    hdnOldSpecimenType.Value = EditDATA.Rows[0]["SPECTYP"].ToString();
                }
                else
                {
                    drpSpecimenType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }

        private void GET_SITE()
        {
            try
            {
                drpsite.Items.Clear();
                DataSet ds = Dal_DE.GET_SITEID_SP();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpsite.DataSource = ds.Tables[0];
                    drpsite.DataValueField = "SiteID";
                    drpsite.DataBind();

                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        drpsite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }

        private void GET_SID()
        {
            try
            {
                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    drpSpecimen.Items.Clear();
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_SID_First_Edit", SITEID: drpsite.SelectedValue, SID: hdnEditSID.Value);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        drpSpecimen.DataSource = ds.Tables[0];
                        drpSpecimen.DataValueField = "SpecimenID";
                        drpSpecimen.DataBind();
                    }
                    drpSpecimen.SelectedValue = hdnEditSID.Value;
                    drpSpecimen.Enabled = false;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        EditDATA = ds.Tables[1];
                    }

                    drpsite.SelectedValue = EditDATA.Rows[0]["SITEID"].ToString();
                    drpsite.Enabled = false;

                    txtComment.Text = EditDATA.Rows[0]["Comment"].ToString();
                    hdnOldComment.Value = EditDATA.Rows[0]["Comment"].ToString();
                }
                else
                {
                    drpSpecimen.Items.Clear();
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_SID_First", SITEID: drpsite.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        drpSpecimen.DataSource = ds.Tables[0];
                        drpSpecimen.DataValueField = "SpecimenID";
                        drpSpecimen.DataBind();
                    }
                    drpSpecimen.Items.Insert(0, new ListItem("", ""));
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }

        private void GET_SUBJECT()
        {
            try
            {
                drpSubject.Items.Clear();
                DataSet ds = Dal_DE.GET_SUBJID_SP(drpsite.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubject.DataSource = ds.Tables[0];
                    drpSubject.DataValueField = "SUBJID";
                    drpSubject.DataBind();
                }
                drpSubject.Items.Insert(0, new ListItem("", ""));

                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    drpSubject.SelectedValue = EditDATA.Rows[0]["SUBJID"].ToString();
                    hdnOldSubject.Value = EditDATA.Rows[0]["SUBJID"].ToString();
                }
                else
                {
                    drpSubject.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }

        private void GET_VISIT()
        {
            try
            {
                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    drpVisit.Items.Clear();
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_VISIT_Edit", SUBJID: drpSubject.SelectedValue, SPECTYP: drpSpecimenType.SelectedValue, SID: hdnEditSID.Value);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        drpVisit.DataSource = ds.Tables[0];
                        drpVisit.DataValueField = "VISITNUM";
                        drpVisit.DataTextField = "VISITNAME";
                        drpVisit.DataBind();
                    }
                    drpVisit.Items.Insert(0, new ListItem("--Select--", "0"));
                    drpVisit.SelectedValue = EditDATA.Rows[0]["VISITNUM"].ToString();
                    hdnOldVisit.Value = EditDATA.Rows[0]["VISIT"].ToString();
                    drpVisit_SelectedIndexChanged(this, EventArgs.Empty);
                }
                else
                {
                    drpVisit.Items.Clear();
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_VISIT", SUBJID: drpSubject.SelectedValue, SPECTYP: drpSpecimenType.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        drpVisit.DataSource = ds.Tables[0];
                        drpVisit.DataValueField = "VISITNUM";
                        drpVisit.DataTextField = "VISITNAME";
                        drpVisit.DataBind();
                    }
                    drpVisit.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void drpsite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SID();
                GET_SUBJECT();
                drpSpecimenType.Focus();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(hdnEditMode.Value) && drpVisit.SelectedItem.Text != hdnOldVisit.Value)
                {
                    SHOW_MODAL_REASON(
                        FIELDNAME: "Visit",
                        VARIABLENAME: "VISIT",
                        INDEX: "",
                        OLDVAL: hdnOldVisit.Value,
                        NEWVAL: drpVisit.SelectedItem.Text);

                    txtReason.Focus();
                }
                else
                {
                    if (repeatFields.Items.Count > 0)
                    {
                        RepeaterItem item = repeatFields.Items[0];

                        HiddenField hdnCONTROLTYPE = (HiddenField)item.FindControl("hdnCONTROLTYPE");
                        string CONTROLTYPE = hdnCONTROLTYPE.Value.Trim();

                        if (CONTROLTYPE == "Textbox" || CONTROLTYPE == "Date" || CONTROLTYPE == "Time" || CONTROLTYPE == "Multiline Textbox" || CONTROLTYPE == "Readonly")
                        {
                            TextBox txtDATA = (TextBox)item.FindControl("txtDATA");
                            txtDATA.Focus();
                        }
                        else if (CONTROLTYPE == "Dropdown")
                        {
                            DropDownList drpDATA = (DropDownList)item.FindControl("drpDATA");
                            drpDATA.Focus();
                        }
                        else if (CONTROLTYPE == "Radio Button")
                        {
                            Repeater repeatRadio = (Repeater)item.FindControl("repeatRadio");
                            if (repeatRadio.Items.Count > 0)
                            {
                                RepeaterItem itemRadio = repeatRadio.Items[0];
                                RadioButton radioDATA = (RadioButton)itemRadio.FindControl("radioDATA");
                                radioDATA.Focus();
                            }
                        }
                        else if (CONTROLTYPE == "Checkbox")
                        {
                            Repeater repeatChk = (Repeater)item.FindControl("repeatChk");
                            if (repeatChk.Items.Count > 0)
                            {
                                RepeaterItem itemChk = repeatChk.Items[0];
                                CheckBox chkDATA = (CheckBox)itemChk.FindControl("chkDATA");
                                chkDATA.Focus();
                            }
                        }
                    }
                }

                GET_ALIQUOTS();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void GET_ALIQUOTS()
        {
            try
            {
                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_ALIQUOT_PREP_First_Edit", VISITNUM: drpVisit.SelectedValue, SID: drpSpecimen.SelectedValue, SITEID: drpsite.SelectedValue);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0)
                    {
                        divAliquotPrep.Visible = true;

                        gridAliquots.DataSource = ds.Tables[2];
                        gridAliquots.DataBind();

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            gridAliquots.Columns[3].Visible = true;
                        }
                        else
                        {
                            gridAliquots.Columns[3].Visible = false;
                        }
                    }
                    else
                    {
                        divAliquotPrep.Visible = false;
                        gridAliquots.DataSource = null;
                        gridAliquots.DataBind();
                    }
                }
                else
                {
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_ALIQUOT_PREP_First", VISITNUM: drpVisit.SelectedValue, SID: drpSpecimen.SelectedValue, SITEID: drpsite.SelectedValue);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0)
                    {
                        divAliquotPrep.Visible = true;

                        gridAliquots.DataSource = ds.Tables[2];
                        gridAliquots.DataBind();

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            gridAliquots.Columns[3].Visible = true;
                        }
                        else
                        {
                            gridAliquots.Columns[3].Visible = false;
                        }

                        if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                        {
                            foreach (GridViewRow gvRow in gridAliquots.Rows)
                            {
                                HiddenField hdnALIQUOTSEQFROM = (HiddenField)gvRow.FindControl("hdnALIQUOTSEQFROM");
                                HiddenField hdnALIQUOTSEQTO = (HiddenField)gvRow.FindControl("hdnALIQUOTSEQTO");
                                TextBox txtBOXNO = (TextBox)gvRow.FindControl("txtBOXNO");
                                TextBox txtSLOTNO = (TextBox)gvRow.FindControl("txtSLOTNO");

                                if (hdnALIQUOTSEQFROM.Value != "" && hdnALIQUOTSEQTO.Value != "" && hdnALIQUOTSEQFROM.Value != "0" && hdnALIQUOTSEQTO.Value != "0")
                                {
                                    DataRow[] drBoxSlot = ds.Tables[3].Select("SEQNO >= " + hdnALIQUOTSEQFROM.Value + " AND SEQNO <= " + hdnALIQUOTSEQTO.Value + " AND STATUS = 0 ", "SEQNO ASC");

                                    if (drBoxSlot.Length > 0)
                                    {
                                        txtBOXNO.Text = drBoxSlot[0]["BOXNO"].ToString();
                                        txtSLOTNO.Text = drBoxSlot[0]["SLOTNO"].ToString();

                                        ds.Tables[3].Rows[0]["STATUS"] = "1";
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        divAliquotPrep.Visible = false;
                        gridAliquots.DataSource = null;
                        gridAliquots.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void drpSpecimen_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpSpecimen.SelectedIndex != 0 && drpSpecimen.SelectedValue != "")
                {
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "CHECK_SpecimenID", SITEID: drpsite.SelectedValue, SID: drpSpecimen.SelectedValue);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            drpSpecimen.SelectedIndex = 0;
                            drpSpecimen.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Specimen ID already entered', 'warning');", true);
                        }
                        else
                        {
                            GET_ALIQUOTS();
                            drpSubject.Focus();
                        }
                    }
                    else
                    {
                        drpSpecimen.SelectedIndex = 0;
                        drpSpecimen.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid Specimen ID', 'warning');", true);
                    }
                }
                else
                {
                    drpSpecimen.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please neter Specimen ID', 'warning');", true);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "CHECK_SUBJID", SITEID: drpsite.SelectedValue, SUBJID: drpSubject.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GET_VISIT();

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0 && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                    {
                        SET_SUBJECT_DATA(ds.Tables[1], ds.Tables[2]);
                    }
                    else
                    {
                        GET_ENTRY_FIELDS();
                    }



                    if (Convert.ToBoolean(hdnEditMode.Value))
                    {
                        SHOW_MODAL_REASON(
                            FIELDNAME: "Subject ID",
                            VARIABLENAME: "SUBJID",
                            INDEX: "",
                            OLDVAL: hdnOldSubject.Value,
                            NEWVAL: drpSubject.SelectedValue);

                        txtReason.Focus();
                    }
                    else
                    {
                        drpVisit.Focus();
                    }
                }
                else
                {
                    GET_ENTRY_FIELDS();
                    drpSubject.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid Subject ID', 'warning');", true);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void SET_SUBJECT_DATA(DataTable dtFIELDS, DataTable dtDATA)
        {
            try
            {
                foreach (RepeaterItem item in repeatFields.Items)
                {
                    string repeatVARIABLENAME = "";

                    HiddenField hdnVARIABLENAME = (HiddenField)item.FindControl("hdnVARIABLENAME");
                    repeatVARIABLENAME = hdnVARIABLENAME.Value.Trim();

                    DataRow[] rows = dtFIELDS.Select(" VARIABLENAME = '" + repeatVARIABLENAME + "' ");

                    if (rows.Length > 0)
                    {
                        string DATA = dtDATA.Rows[0][repeatVARIABLENAME].ToString();

                        HiddenField hdnCONTROLTYPE = (HiddenField)item.FindControl("hdnCONTROLTYPE");
                        string CONTROLTYPE = hdnCONTROLTYPE.Value.Trim();

                        if (CONTROLTYPE == "Textbox" || CONTROLTYPE == "Date" || CONTROLTYPE == "Time" || CONTROLTYPE == "Multiline Textbox" || CONTROLTYPE == "Readonly")
                        {
                            TextBox txtDATA = (TextBox)item.FindControl("txtDATA");
                            txtDATA.Text = DATA;
                            txtDATA.Attributes.Add("Readonly", "Readonly");
                        }
                        else if (CONTROLTYPE == "Dropdown")
                        {
                            DropDownList drpDATA = (DropDownList)item.FindControl("drpDATA");
                            drpDATA.SelectedValue = DATA;
                            drpDATA.Enabled = false;
                        }
                        else if (CONTROLTYPE == "Radio Button")
                        {
                            Repeater repeatRadio = (Repeater)item.FindControl("repeatRadio");
                            foreach (RepeaterItem itemRadio in repeatRadio.Items)
                            {
                                RadioButton radioDATA = (RadioButton)itemRadio.FindControl("radioDATA");

                                if (radioDATA.Text == DATA)
                                {
                                    radioDATA.Checked = true;
                                }
                                else
                                {
                                    radioDATA.Checked = false;
                                }
                            }
                        }
                        else if (CONTROLTYPE == "Checkbox")
                        {
                            Repeater repeatChk = (Repeater)item.FindControl("repeatChk");

                            foreach (RepeaterItem itemChk in repeatChk.Items)
                            {
                                CheckBox chkDATA = (CheckBox)itemChk.FindControl("chkDATA");

                                if (DATA.Split(',').Contains(chkDATA.Text))
                                {
                                    chkDATA.Checked = true;
                                }
                                else
                                {
                                    chkDATA.Checked = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void drpSpecimenType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_VISIT();

                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    SHOW_MODAL_REASON(
                        FIELDNAME: "Specimen Type",
                        VARIABLENAME: "SPECTYP",
                        INDEX: "",
                        OLDVAL: hdnOldSpecimenType.Value,
                        NEWVAL: drpSpecimenType.SelectedValue);

                    txtReason.Focus();
                }
                else
                {
                    drpSpecimen.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void repeatFields_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView drv = (DataRowView)e.Item.DataItem;

                    DataSet ds = new DataSet();

                    HiddenField hdnOldDATA = (HiddenField)e.Item.FindControl("hdnOldDATA");
                    TextBox txtDATA = (TextBox)e.Item.FindControl("txtDATA");
                    DropDownList drpDATA = (DropDownList)e.Item.FindControl("drpDATA");
                    Repeater repeatRadio = (Repeater)e.Item.FindControl("repeatRadio");
                    Repeater repeatChk = (Repeater)e.Item.FindControl("repeatChk");
                    Control divRadChk = (Control)e.Item.FindControl("divRadChk");

                    string VARIABLENAME = drv["VARIABLENAME"].ToString().Trim();

                    if (Convert.ToBoolean(hdnEditMode.Value))
                    {
                        hdnOldDATA.Value = EditDATA.Rows[0][VARIABLENAME].ToString();
                    }

                    switch (drv["CONTROLTYPE"].ToString().Trim())
                    {
                        case "Readonly":
                            txtDATA.Visible = true;
                            txtDATA.Attributes.Add("Readonly", "Readonly");
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                txtDATA.Text = EditDATA.Rows[0][VARIABLENAME].ToString();
                            }
                            break;

                        case "Textbox":
                            txtDATA.Visible = true;

                            if (drv["VARIABLENAME"].ToString().Trim() == "INITIAL")
                            {
                                txtDATA.CssClass = txtDATA.CssClass + " initial";
                            }
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                txtDATA.Text = EditDATA.Rows[0][VARIABLENAME].ToString();
                            }

                            break;

                        case "Multiline Textbox":
                            txtDATA.Visible = true;
                            txtDATA.TextMode = TextBoxMode.MultiLine;
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                txtDATA.Text = EditDATA.Rows[0][VARIABLENAME].ToString();
                            }
                            break;

                        case "Date":
                            txtDATA.Visible = true;
                            txtDATA.CssClass = txtDATA.CssClass + " txtDate";
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                txtDATA.Text = EditDATA.Rows[0][VARIABLENAME].ToString();
                            }
                            break;

                        case "Time":
                            txtDATA.Visible = true;
                            txtDATA.CssClass = txtDATA.CssClass + " txtTime";
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                txtDATA.Text = EditDATA.Rows[0][VARIABLENAME].ToString();
                            }
                            break;

                        case "Dropdown":
                            drpDATA.Visible = true;
                            ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_FIELD_OPTIONS", VARIABLENAME: VARIABLENAME);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                drpDATA.DataSource = ds.Tables[0];
                                drpDATA.DataValueField = "OPTION_VALUE";
                                drpDATA.DataBind();
                                drpDATA.Items.Insert(0, new ListItem("--Select--", "0"));
                            }
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                drpDATA.SelectedValue = EditDATA.Rows[0][VARIABLENAME].ToString();
                            }
                            break;

                        case "Radio Button":
                            divRadChk.Visible = true;
                            ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_FIELD_OPTIONS", VARIABLENAME: VARIABLENAME);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                repeatRadio.DataSource = ds.Tables[0];
                                repeatRadio.DataBind();
                            }
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                foreach (RepeaterItem itemRadio in repeatRadio.Items)
                                {
                                    RadioButton radioDATA = (RadioButton)itemRadio.FindControl("radioDATA");

                                    if (radioDATA.Text == EditDATA.Rows[0][VARIABLENAME].ToString())
                                    {
                                        radioDATA.Checked = true;
                                    }
                                    else
                                    {
                                        radioDATA.Checked = false;
                                    }
                                }
                            }
                            break;

                        case "Checkbox":
                            divRadChk.Visible = true;
                            ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_FIELD_OPTIONS", VARIABLENAME: VARIABLENAME);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                repeatChk.DataSource = ds.Tables[0];
                                repeatChk.DataBind();
                            }
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                foreach (RepeaterItem itemChk in repeatChk.Items)
                                {
                                    CheckBox chkDATA = (CheckBox)itemChk.FindControl("chkDATA");

                                    if (EditDATA.Rows[0][VARIABLENAME].ToString().Split(',').Contains(chkDATA.Text))
                                    {
                                        chkDATA.Checked = true;
                                    }
                                    else
                                    {
                                        chkDATA.Checked = false;
                                    }
                                }
                            }
                            break;
                    }

                    if (drv["MAXLEN"].ToString() != "" && drv["MAXLEN"].ToString() != "0")
                    {
                        if (drv["CONTROLTYPE"].ToString().Trim() == "Textbox" || drv["CONTROLTYPE"].ToString().Trim() == "Multiline Textbox")
                        {
                            txtDATA.MaxLength = Convert.ToInt32(drv["MAXLEN"]);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DATA_ENTRY.aspx");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string MSG = CHECK_CRIETRIA();

                if (MSG != "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', '" + MSG + "', 'warning');", true);
                }
                else
                {
                    INSERT_SPECIMEN_DATA();
                    INSERT_ALIQUOT_DATA();

                    string script = @"
                                    swal({
                                        title: 'Success',
                                        text: 'Specimen ID entered successfully.',
                                        icon: 'success',
                                        button: 'OK'
                                    }).then((value) => {
                                        window.location.href = 'DATA_ENTRY.aspx'; 
                                    });";

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private string CHECK_CRIETRIA()
        {
            string RESULT = "";
            try
            {
                dtCurrentDATA = GET_GRID_DATATABLE();

                DataSet dsCRIT = Dal_DE.CRITERIA_SP(ACTION: "GET_CRITERIA");

                if (dsCRIT.Tables.Count > 0 && dsCRIT.Tables[0].Rows.Count > 0)
                {
                    int counter = 0;

                    while (RESULT == "" && counter < dsCRIT.Tables[0].Rows.Count)
                    {
                        DataRow drCases = dsCRIT.Tables[0].Rows[counter];

                        string CASES = drCases["CritCode"].ToString();

                        CASES = CASES.Replace("'[", "[");

                        CASES = CASES.Replace("]'", "]");

                        if (CASES.Contains("[") && CASES.Contains("]"))
                        {
                            foreach (DataRow drDATA in dtCurrentDATA.Rows)
                            {
                                CASES = CASES.Replace("[" + drDATA["VARIABLENAME"].ToString() + "]", CheckDatatype(drDATA["DATA"].ToString()));
                            }
                        }

                        DataSet dsRESULT = Dal_DE.CRITERIA_SP(
                            ACTION: "GET_RESULT",
                            Condition: CASES,
                            DESCRIPTION: drCases["DESCRIPTION"].ToString()
                            );

                        if (dsRESULT.Tables.Count > 0 && dsRESULT.Tables[0].Rows.Count > 0)
                        {
                            RESULT = dsRESULT.Tables[0].Rows[0][0].ToString();
                        }

                        counter++;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return RESULT;
        }

        private string CheckDatatype(string Val)
        {
            string RESULT = "";
            try
            {
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
                else if (DateTime.TryParse(Val, out l) || isDate(Val))
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
                ex.ToString();

            }

            return RESULT;
        }

        private bool isDate(string date)
        {
            bool result = false;

            Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");

            //Verify whether date entered in dd/MM/yyyy format.
            bool isValid = regex.IsMatch(date.Trim());

            //Verify whether entered date is Valid date.
            DateTime dt;
            isValid = DateTime.TryParseExact(date, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
            if (!isValid)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private void INSERT_SPECIMEN_DATA()
        {
            try
            {
                string INSERTQUERY = "", UPDATEQUERY = "", COLUMN = "", DATA = "";

                COLUMN = COLUMN + " @ni$h SITEID";
                DATA = DATA + " @ni$h '" + drpsite.SelectedValue + "' ";

                COLUMN = COLUMN + " @ni$h SPECTYP";
                DATA = DATA + " @ni$h '" + drpSpecimenType.SelectedValue + "' ";

                COLUMN = COLUMN + " @ni$h SID";
                DATA = DATA + " @ni$h '" + drpSpecimen.SelectedValue + "' ";

                COLUMN = COLUMN + " @ni$h SUBJID";
                DATA = DATA + " @ni$h '" + drpSubject.SelectedValue + "' ";

                COLUMN = COLUMN + " @ni$h VISITNUM";
                DATA = DATA + " @ni$h '" + drpVisit.SelectedValue + "' ";

                COLUMN = COLUMN + " @ni$h VISIT";
                DATA = DATA + " @ni$h '" + drpVisit.SelectedItem.Text + "' ";

                COLUMN = COLUMN + " @ni$h COMMENTS";
                DATA = DATA + " @ni$h '" + txtComment.Text + "' ";

                foreach (RepeaterItem item in repeatFields.Items)
                {
                    string repeatVARIABLENAME = "", repeatDATA = "";

                    HiddenField hdnVARIABLENAME = (HiddenField)item.FindControl("hdnVARIABLENAME");
                    repeatVARIABLENAME = hdnVARIABLENAME.Value.Trim();

                    HiddenField hdnCONTROLTYPE = (HiddenField)item.FindControl("hdnCONTROLTYPE");
                    string CONTROLTYPE = hdnCONTROLTYPE.Value.Trim();

                    if (CONTROLTYPE == "Textbox" || CONTROLTYPE == "Date" || CONTROLTYPE == "Time" || CONTROLTYPE == "Multiline Textbox" || CONTROLTYPE == "Readonly")
                    {
                        TextBox txtDATA = (TextBox)item.FindControl("txtDATA");
                        repeatDATA = txtDATA.Text;
                    }
                    else if (CONTROLTYPE == "Dropdown")
                    {
                        DropDownList drpDATA = (DropDownList)item.FindControl("drpDATA");
                        repeatDATA = drpDATA.SelectedValue;
                    }
                    else if (CONTROLTYPE == "Radio Button")
                    {
                        Repeater repeatRadio = (Repeater)item.FindControl("repeatRadio");
                        foreach (RepeaterItem itemRadio in repeatRadio.Items)
                        {
                            RadioButton radioDATA = (RadioButton)itemRadio.FindControl("radioDATA");

                            if (radioDATA.Checked)
                            {
                                repeatDATA = radioDATA.Text;
                            }
                        }
                    }
                    else if (CONTROLTYPE == "Checkbox")
                    {
                        Repeater repeatChk = (Repeater)item.FindControl("repeatChk");

                        foreach (RepeaterItem itemChk in repeatChk.Items)
                        {
                            CheckBox chkDATA = (CheckBox)itemChk.FindControl("chkDATA");

                            if (chkDATA.Checked)
                            {
                                if (repeatDATA != "")
                                {
                                    repeatDATA += "," + chkDATA.Text;
                                }
                                else
                                {
                                    repeatDATA = chkDATA.Text;
                                }
                            }
                        }
                    }

                    COLUMN = COLUMN + " @ni$h [" + repeatVARIABLENAME + "]";
                    DATA = DATA + " @ni$h '" + repeatDATA + "'";
                }

                if (COLUMN.StartsWith(" @ni$h "))
                {
                    COLUMN = COLUMN.Substring(7, COLUMN.Length - 7);
                    DATA = DATA.Substring(7, DATA.Length - 7);
                }

                INSERTQUERY = "INSERT INTO [SPECIMEN_DATA] (ENTEREDBY, ENTEREDBYNAME, ENTEREDDAT, ENTERED_TZVAL, ENTEREDFIRSTBY, ENTEREDFIRSTBYNAME, ENTEREDFIRSTDAT, ENTEREDFIRST_TZVAL, " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + Session["USER_ID"].ToString() + "', '" + Session["User_Name"].ToString() + "' , GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', '" + Session["USER_ID"].ToString() + "', '" + Session["User_Name"].ToString() + "' , GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + ")";

                string[] COLArr = COLUMN.Split(new string[] { " @ni$h " }, StringSplitOptions.None);
                string[] DATAArr = DATA.Split(new string[] { " @ni$h " }, StringSplitOptions.None);

                for (int i = 0; i < COLArr.Length; i++)
                {
                    if (UPDATEQUERY == "")
                    {
                        UPDATEQUERY = "UPDATE [SPECIMEN_DATA] SET " +
                            "UPDATEDBY = '" + Session["USER_ID"].ToString() + "', UPDATEDBYNAME = '" + Session["User_Name"].ToString() + "', UPDATEDDAT = GETDATE(), UPDATED_TZVAL = '" + Session["TimeZone_Value"].ToString() + "', " +
                            "UPDATEFIRSTDBY = '" + Session["USER_ID"].ToString() + "', UPDATEDFIRSTBYNAME = '" + Session["User_Name"].ToString() + "', UPDATEDFIRSTDAT = GETDATE(), UPDATEDFIRST_TZVAL  = '" + Session["TimeZone_Value"].ToString() + "' ";
                    }
                    UPDATEQUERY = UPDATEQUERY + ", " + COLArr[i] + " = " + DATAArr[i] + " ";
                }

                UPDATEQUERY = UPDATEQUERY + " WHERE SID = '" + drpSpecimen.SelectedValue + "' ";

                Dal_DE.DATA_ENTRY_SP(
                    ACTION: "INSERT_SPECIMEN_DATA_First",
                    INSERTQUERY: INSERTQUERY,
                    UPDATEQUERY: UPDATEQUERY,
                    SID: drpSpecimen.SelectedValue);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void INSERT_ALIQUOT_DATA()
        {
            try
            {
                foreach (GridViewRow gvRow in gridAliquots.Rows)
                {
                    HiddenField hdnID = (HiddenField)gvRow.FindControl("hdnID");
                    Label lblALIQUOTID = (Label)gvRow.FindControl("lblALIQUOTID");
                    Label lblALIQUOTTYPE = (Label)gvRow.FindControl("lblALIQUOTTYPE");
                    Label lblALIQUOTNO_CONCAT = (Label)gvRow.FindControl("lblALIQUOTNO_CONCAT");
                    TextBox txtALIQUOTNO = (TextBox)gvRow.FindControl("txtALIQUOTNO");
                    TextBox txtBOXNO = (TextBox)gvRow.FindControl("txtBOXNO");
                    TextBox txtSLOTNO = (TextBox)gvRow.FindControl("txtSLOTNO");

                    if (txtBOXNO.Text != "" && txtSLOTNO.Text != "")
                    {
                        Dal_DE.DATA_ENTRY_SP(
                        ACTION: "INSERT_ALIQUOT_DATA_First",
                        SITEID: drpsite.SelectedValue,
                        SID: drpSpecimen.SelectedValue,
                        SUBJID: drpSubject.SelectedValue,
                        VISITNUM: drpVisit.SelectedValue,
                        VISIT: drpVisit.SelectedItem.Text,
                        ALQID: hdnID.Value,
                        ALIQUOTID: lblALIQUOTID.Text,
                        ALIQUOTTYPE: lblALIQUOTTYPE.Text,
                        ALIQUOTNO_CONCAT: lblALIQUOTNO_CONCAT.Text,
                        ALIQUOTNO: txtALIQUOTNO.Text,
                        BOXNO: txtBOXNO.Text,
                        SLOTNO: txtSLOTNO.Text
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        DataTable dtCurrentDATA = new DataTable();
        protected void btnDATA_Changed_Click(object sender, EventArgs e)
        {
            try
            {
                dtCurrentDATA = GET_GRID_DATATABLE();

                RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

                HiddenField hdnINDEX = (HiddenField)item.FindControl("hdnINDEX");
                HiddenField hdnOldDATA = (HiddenField)item.FindControl("hdnOldDATA");
                HiddenField hdnVARIABLENAME = (HiddenField)item.FindControl("hdnVARIABLENAME");
                Label lblFIELDNAME = (Label)item.FindControl("lblFIELDNAME");

                HiddenField hdnCONTROLTYPE = (HiddenField)item.FindControl("hdnCONTROLTYPE");
                string CONTROLTYPE = hdnCONTROLTYPE.Value.Trim();
                string repeatDATA = "";

                if (CONTROLTYPE == "Textbox" || CONTROLTYPE == "Date" || CONTROLTYPE == "Time" || CONTROLTYPE == "Multiline Textbox" || CONTROLTYPE == "Readonly")
                {
                    TextBox txtDATA = (TextBox)item.FindControl("txtDATA");
                    repeatDATA = txtDATA.Text;
                }
                else if (CONTROLTYPE == "Dropdown")
                {
                    DropDownList drpDATA = (DropDownList)item.FindControl("drpDATA");
                    repeatDATA = drpDATA.SelectedValue;
                }
                else if (CONTROLTYPE == "Radio Button")
                {
                    Repeater repeatRadio = (Repeater)item.FindControl("repeatRadio");
                    foreach (RepeaterItem itemRadio in repeatRadio.Items)
                    {
                        RadioButton radioDATA = (RadioButton)itemRadio.FindControl("radioDATA");

                        if (radioDATA.Checked)
                        {
                            repeatDATA = radioDATA.Text;
                        }
                    }
                }
                else if (CONTROLTYPE == "Checkbox")
                {
                    Repeater repeatChk = (Repeater)item.FindControl("repeatChk");

                    foreach (RepeaterItem itemChk in repeatChk.Items)
                    {
                        CheckBox chkDATA = (CheckBox)itemChk.FindControl("chkDATA");

                        if (chkDATA.Checked)
                        {
                            if (repeatDATA != "")
                            {
                                repeatDATA += "," + chkDATA.Text;
                            }
                            else
                            {
                                repeatDATA = chkDATA.Text;
                            }
                        }
                    }
                }


                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    SHOW_MODAL_REASON(
                        FIELDNAME: lblFIELDNAME.Text,
                        VARIABLENAME: hdnVARIABLENAME.Value,
                        INDEX: hdnINDEX.Value,
                        OLDVAL: hdnOldDATA.Value,
                        NEWVAL: repeatDATA);

                    txtReason.Focus();
                }
                else
                {
                    if (hdnVARIABLENAME.Value == "BRTHDAT" || hdnVARIABLENAME.Value == "COLDAT")
                    {
                        SET_AGE(lblFIELDNAME.Text);
                    }

                    FOCUS_NEXT_ITEM(Convert.ToInt32(hdnINDEX.Value));
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void FOCUS_NEXT_ITEM(int INDEX)
        {
            try
            {
                if (repeatFields.Items.Count > INDEX)
                {
                    RepeaterItem item = repeatFields.Items[INDEX + 1];

                    HiddenField hdnVARIABLENAME = (HiddenField)item.FindControl("hdnVARIABLENAME");
                    Label lblFIELDNAME = (Label)item.FindControl("lblFIELDNAME");

                    HiddenField hdnCONTROLTYPE = (HiddenField)item.FindControl("hdnCONTROLTYPE");
                    string CONTROLTYPE = hdnCONTROLTYPE.Value.Trim();

                    if (CONTROLTYPE == "Textbox" || CONTROLTYPE == "Date" || CONTROLTYPE == "Time" || CONTROLTYPE == "Multiline Textbox" || CONTROLTYPE == "Readonly")
                    {
                        TextBox txtDATA = (TextBox)item.FindControl("txtDATA");
                        FocusWithoutScroll(txtDATA);
                        //txtDATA.Focus();
                    }
                    else if (CONTROLTYPE == "Dropdown")
                    {
                        DropDownList drpDATA = (DropDownList)item.FindControl("drpDATA");
                        FocusWithoutScroll(drpDATA);
                        //drpDATA.Focus();
                    }
                    else if (CONTROLTYPE == "Radio Button")
                    {
                        Repeater repeatRadio = (Repeater)item.FindControl("repeatRadio");

                        if (repeatRadio.Items.Count > 0)
                        {
                            RepeaterItem itemRadio = repeatRadio.Items[0];
                            RadioButton radioDATA = (RadioButton)itemRadio.FindControl("radioDATA");
                            FocusWithoutScroll(radioDATA);
                            //radioDATA.Focus();
                        }
                    }
                    else if (CONTROLTYPE == "Checkbox")
                    {
                        Repeater repeatChk = (Repeater)item.FindControl("repeatChk");

                        if (repeatChk.Items.Count > 0)
                        {
                            RepeaterItem itemChk = repeatChk.Items[0];
                            CheckBox chkDATA = (CheckBox)itemChk.FindControl("chkDATA");
                            FocusWithoutScroll(chkDATA);
                            //chkDATA.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private DataTable GET_GRID_DATATABLE()
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("VARIABLENAME");
            outputTable.Columns.Add("DATA");

            outputTable.Rows.Add("SITEID", drpsite.SelectedValue);
            outputTable.Rows.Add("SPECTYP", drpSpecimenType.SelectedValue);
            outputTable.Rows.Add("SID", drpSpecimen.SelectedValue);
            outputTable.Rows.Add("SUBJID", drpSubject.SelectedValue);
            outputTable.Rows.Add("VISITNUM", drpVisit.SelectedValue);
            outputTable.Rows.Add("VISIT", drpVisit.SelectedItem.Text);


            foreach (RepeaterItem item in repeatFields.Items)
            {
                string repeatVARIABLENAME = "", repeatDATA = "";

                HiddenField hdnVARIABLENAME = (HiddenField)item.FindControl("hdnVARIABLENAME");
                repeatVARIABLENAME = hdnVARIABLENAME.Value.Trim();

                HiddenField hdnCONTROLTYPE = (HiddenField)item.FindControl("hdnCONTROLTYPE");
                string CONTROLTYPE = hdnCONTROLTYPE.Value.Trim();

                if (CONTROLTYPE == "Textbox" || CONTROLTYPE == "Date" || CONTROLTYPE == "Time" || CONTROLTYPE == "Multiline Textbox" || CONTROLTYPE == "Readonly")
                {
                    TextBox txtDATA = (TextBox)item.FindControl("txtDATA");
                    repeatDATA = txtDATA.Text;
                }
                else if (CONTROLTYPE == "Dropdown")
                {
                    DropDownList drpDATA = (DropDownList)item.FindControl("drpDATA");
                    repeatDATA = drpDATA.SelectedValue;
                }
                else if (CONTROLTYPE == "Radio Button")
                {
                    Repeater repeatRadio = (Repeater)item.FindControl("repeatRadio");
                    foreach (RepeaterItem itemRadio in repeatRadio.Items)
                    {
                        RadioButton radioDATA = (RadioButton)itemRadio.FindControl("radioDATA");

                        if (radioDATA.Checked)
                        {
                            repeatDATA = radioDATA.Text;
                        }
                    }
                }
                else if (CONTROLTYPE == "Checkbox")
                {
                    Repeater repeatChk = (Repeater)item.FindControl("repeatChk");

                    foreach (RepeaterItem itemChk in repeatChk.Items)
                    {
                        CheckBox chkDATA = (CheckBox)itemChk.FindControl("chkDATA");

                        if (chkDATA.Checked)
                        {
                            if (repeatDATA != "")
                            {
                                repeatDATA += "," + chkDATA.Text;
                            }
                            else
                            {
                                repeatDATA = chkDATA.Text;
                            }
                        }
                    }
                }

                outputTable.Rows.Add(repeatVARIABLENAME, repeatDATA);
            }

            return outputTable;
        }

        private void SET_AGE(string FIELDNAME)
        {
            try
            {
                dtCurrentDATA = GET_GRID_DATATABLE();

                string BRTHDAT = "", COLDAT = "";

                int AGE = 0;

                DataRow[] rows = dtCurrentDATA.Select(" VARIABLENAME = 'BRTHDAT' ");
                BRTHDAT = rows[0]["DATA"].ToString();

                rows = dtCurrentDATA.Select(" VARIABLENAME = 'COLDAT' ");
                COLDAT = rows[0]["DATA"].ToString();

                DateTime DOB = DateTime.Parse(BRTHDAT);
                DateTime DOC = DateTime.Parse(COLDAT);
                TimeSpan ageSpan = DOC - DOB;

                if (FIELDNAME.Contains("Day"))
                {
                    AGE = (int)ageSpan.TotalDays;
                }
                else if (FIELDNAME.Contains("Week"))
                {
                    AGE = (int)(ageSpan.TotalDays / 7);
                }
                else if (FIELDNAME.Contains("Month"))
                {
                    int ageYears = DOC.Year - DOB.Year;
                    int ageMonths = DOC.Month - DOB.Month;

                    if (DOC.Day < DOB.Day)
                    {
                        ageMonths--;
                    }

                    if (ageMonths < 0)
                    {
                        ageMonths += 12;
                        ageYears--;
                    }

                    AGE = (ageYears * 12) + ageMonths;
                }
                else
                {
                    AGE = DOC.Year - DOB.Year;

                    if (DOC < DOB.AddYears(AGE))
                    {
                        AGE--;
                    }
                }

                foreach (RepeaterItem item in repeatFields.Items)
                {
                    HiddenField hdnVARIABLENAME = (HiddenField)item.FindControl("hdnVARIABLENAME");

                    if (hdnVARIABLENAME.Value.Trim() == "AGE")
                    {
                        HiddenField hdnCONTROLTYPE = (HiddenField)item.FindControl("hdnCONTROLTYPE");
                        string CONTROLTYPE = hdnCONTROLTYPE.Value.Trim();

                        if (CONTROLTYPE == "Textbox" || CONTROLTYPE == "Date" || CONTROLTYPE == "Time" || CONTROLTYPE == "Multiline Textbox" || CONTROLTYPE == "Readonly")
                        {
                            TextBox txtDATA = (TextBox)item.FindControl("txtDATA");
                            txtDATA.Text = AGE.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void FocusWithoutScroll(Control control)
        {
            string script = @"document.getElementById('" + control.ClientID + @"').focus({preventScroll:true});";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "FocusWithoutScroll", script, true);
        }

        protected void txtALIQUOTNO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;

                HiddenField hdnINDEX = (HiddenField)row.FindControl("hdnINDEX");
                HiddenField hdnID = (HiddenField)row.FindControl("hdnID");
                Label lblALIQUOTNO_CONCAT = (Label)row.FindControl("lblALIQUOTNO_CONCAT");
                TextBox txtALIQUOTNO = (TextBox)row.FindControl("txtALIQUOTNO");
                HiddenField hdnOldALIQUOTNO = (HiddenField)row.FindControl("hdnOldALIQUOTNO");
                TextBox txtBOXNO = (TextBox)row.FindControl("txtBOXNO");

                if (txtALIQUOTNO.Text != lblALIQUOTNO_CONCAT.Text)
                {
                    txtALIQUOTNO.Text = "";
                    txtALIQUOTNO.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Entered Aliquot no. must be same as Aliquot no. populated in the third column.', 'warning');", true);
                }
                else
                {
                    if (Convert.ToBoolean(hdnEditMode.Value))
                    {
                        SHOW_MODAL_REASON(
                            FIELDNAME: "Aliquot No.",
                            VARIABLENAME: "ALIQUOTNO",
                            INDEX: hdnINDEX.Value,
                            OLDVAL: hdnOldALIQUOTNO.Value,
                            NEWVAL: txtALIQUOTNO.Text);

                        hdnReasonALQID.Value = hdnID.Value;

                        txtReason.Focus();


                    }
                    else
                    {
                        FocusWithoutScroll(txtBOXNO);

                        //txtBOXNO.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void txtBOXNO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;

                HiddenField hdnINDEX = (HiddenField)row.FindControl("hdnINDEX");
                HiddenField hdnID = (HiddenField)row.FindControl("hdnID");
                TextBox txtBOXNO = (TextBox)row.FindControl("txtBOXNO");
                TextBox txtSLOTNO = (TextBox)row.FindControl("txtSLOTNO");
                Label lblALIQUOTTYPE = (Label)row.FindControl("lblALIQUOTTYPE");

                bool result = true;

                foreach (GridViewRow gvRow in gridAliquots.Rows)
                {
                    HiddenField gv_hdnINDEX = (HiddenField)gvRow.FindControl("hdnINDEX");
                    Label gv_lblALIQUOTTYPE = (Label)gvRow.FindControl("lblALIQUOTTYPE");
                    TextBox gv_txtSLOTNO = (TextBox)gvRow.FindControl("txtSLOTNO");
                    TextBox gv_txtBOXNO = (TextBox)gvRow.FindControl("txtBOXNO");

                    if (gv_hdnINDEX.Value != hdnINDEX.Value && gv_lblALIQUOTTYPE.Text != lblALIQUOTTYPE.Text && (gv_txtBOXNO.Text != "" && gv_txtBOXNO.Text == txtBOXNO.Text))
                    {
                        txtBOXNO.Text = "";
                        txtBOXNO.Focus();
                        result = false;

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Same box no. can not be used for " + gv_lblALIQUOTTYPE.Text + " and " + lblALIQUOTTYPE.Text + ".', 'warning');", true);
                    }
                    else if (gv_hdnINDEX.Value != hdnINDEX.Value && gv_txtBOXNO.Text == txtBOXNO.Text && (gv_txtBOXNO.Text != "" && gv_txtSLOTNO.Text == txtSLOTNO.Text))
                    {
                        txtBOXNO.Text = "";
                        txtBOXNO.Focus();
                        result = false;

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Same combination of Box no. and Slot no. can not be used multiple times.', 'warning');", true);
                    }
                }

                if (result)
                {
                    string MSG = CHECK_BOXNO_SLOTNO(txtBOXNO.Text, txtSLOTNO.Text);

                    if (MSG != "")
                    {
                        txtBOXNO.Text = "";
                        txtBOXNO.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', '" + MSG + "', 'warning');", true);
                    }
                    else
                    {
                        if (Convert.ToBoolean(hdnEditMode.Value))
                        {
                            HiddenField hdnOldBOXNO = (HiddenField)row.FindControl("hdnOldBOXNO");

                            SHOW_MODAL_REASON(
                                FIELDNAME: "Box No.",
                                VARIABLENAME: "BOXNO",
                                INDEX: hdnINDEX.Value,
                                OLDVAL: hdnOldBOXNO.Value,
                                NEWVAL: txtBOXNO.Text);

                            hdnReasonALQID.Value = hdnID.Value;

                            txtReason.Focus();
                        }
                        else
                        {
                            FocusWithoutScroll(txtSLOTNO);

                            //txtSLOTNO.Focus();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private string CHECK_BOXNO_SLOTNO(string BOXNO, string SLOTNO)
        {
            string MSG = "";
            try
            {
                DataSet ds = Dal_DE.DATA_ENTRY_SP(
                    ACTION: "CHECK_BOXNO_SLOTNO",
                    SID: drpSpecimen.SelectedValue,
                    SITEID: drpsite.SelectedValue,
                    BOXNO: BOXNO,
                    SLOTNO: SLOTNO
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return MSG;
        }

        protected void txtSLOTNO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;

                HiddenField hdnID = (HiddenField)row.FindControl("hdnID");
                HiddenField hdnINDEX = (HiddenField)row.FindControl("hdnINDEX");
                TextBox txtBOXNO = (TextBox)row.FindControl("txtBOXNO");
                TextBox txtSLOTNO = (TextBox)row.FindControl("txtSLOTNO");

                bool result = true;

                foreach (GridViewRow gvRow in gridAliquots.Rows)
                {
                    HiddenField gv_hdnINDEX = (HiddenField)gvRow.FindControl("hdnINDEX");
                    TextBox gv_txtSLOTNO = (TextBox)gvRow.FindControl("txtSLOTNO");
                    TextBox gv_txtBOXNO = (TextBox)gvRow.FindControl("txtBOXNO");

                    if (gv_hdnINDEX.Value != hdnINDEX.Value && gv_txtBOXNO.Text == txtBOXNO.Text && (gv_txtSLOTNO.Text != "" && gv_txtSLOTNO.Text == txtSLOTNO.Text))
                    {
                        txtSLOTNO.Text = "";
                        txtSLOTNO.Focus();
                        result = false;

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Same combination of Box no. and Slot no. can not be used for multiple Aliquots.', 'warning');", true);
                    }
                }

                if (result)
                {
                    string MSG = CHECK_BOXNO_SLOTNO(txtBOXNO.Text, txtSLOTNO.Text);

                    if (MSG != "")
                    {
                        txtSLOTNO.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', '" + MSG + "', 'warning');", true);
                        txtSLOTNO.Focus();
                    }
                    else
                    {
                        int INDEX = Convert.ToInt32(hdnINDEX.Value);
                        if (gridAliquots.Rows.Count > INDEX)
                        {
                            GridViewRow gvRow = gridAliquots.Rows[INDEX + 1];

                            if (gridAliquots.Columns[3].Visible)
                            {
                                TextBox gv_txtALIQUOTNO = (TextBox)gvRow.FindControl("txtALIQUOTNO");

                                //gv_txtALIQUOTNO.Focus();
                                FocusWithoutScroll(gv_txtALIQUOTNO);

                            }
                            else
                            {
                                TextBox gv_txtBOXNO = (TextBox)gvRow.FindControl("txtBOXNO");

                                //gv_txtBOXNO.Focus();
                                FocusWithoutScroll(gv_txtBOXNO);
                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                HiddenField hdnOldSLOTNO = (HiddenField)row.FindControl("hdnOldSLOTNO");

                                SHOW_MODAL_REASON(
                                    FIELDNAME: "Slot No.",
                                    VARIABLENAME: "SLOTNO",
                                    INDEX: hdnINDEX.Value,
                                    OLDVAL: hdnOldSLOTNO.Value,
                                    NEWVAL: txtSLOTNO.Text);

                                hdnReasonALQID.Value = hdnID.Value;

                                txtReason.Focus();
                            }
                            else
                            {
                                //btnSubmit.Focus();

                                FocusWithoutScroll(btnSubmit);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnSubmitReason_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnReasonALQID.Value != "")
                {
                    Dal_DE.DATA_ENTRY_SP(
                        ACTION: "UPDATE_ALIQUOT_DATA",
                        SID: hdnEditSID.Value,
                        ALQID: hdnReasonALQID.Value,
                        FIELDNAME: lblFieldName.Text,
                        VARIABLENAME: hdnReasonVARIABLENAME.Value,
                        COLUMNNAME: hdnReasonVARIABLENAME.Value,
                        OLDVAL: lblOldVal.Text,
                        NEWVAL: lblNewVal.Text,
                        Reason: txtReason.Text
                        );
                }
                else
                {
                    Dal_DE.DATA_ENTRY_SP(
                        ACTION: "UPDATE_SPECIMEN_DATA",
                        SID: hdnEditSID.Value,
                        FIELDNAME: lblFieldName.Text,
                        VARIABLENAME: hdnReasonVARIABLENAME.Value,
                        COLUMNNAME: hdnReasonVARIABLENAME.Value,
                        OLDVAL: lblOldVal.Text,
                        NEWVAL: lblNewVal.Text,
                        Reason: txtReason.Text
                        );

                    if (hdnReasonVARIABLENAME.Value == "VISIT")
                    {
                        Dal_DE.DATA_ENTRY_SP(
                            ACTION: "UPDATE_SPECIMEN_DATA",
                            FIELDNAME: lblFieldName.Text,
                            VARIABLENAME: "VISITNUM",
                            COLUMNNAME: "VISITNUM",
                            OLDVAL: lblOldVal.Text,
                            NEWVAL: drpVisit.SelectedValue,
                            Reason: txtReason.Text
                            );
                    }

                    if (hdnReasonVARIABLENAME.Value == "BRTHDAT" || hdnReasonVARIABLENAME.Value == "COLDAT")
                    {
                        SET_AGE(lblFieldName.Text);
                    }
                }

                INSERT_SPECIMEN_DATA();
                INSERT_ALIQUOT_DATA();

                string script = @"
                                    swal({
                                        title: 'Success',
                                        text: 'Data updated successfully.',
                                        icon: 'success',
                                        button: 'OK'
                                    }).then((value) => {
                                        window.location.href = 'DATA_ENTRY.aspx?SID=" + hdnEditSID.Value + "'; }); ";

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnCancelReason_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DATA_ENTRY.aspx?SID=" + hdnEditSID.Value + "");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}