using SpecimenTracking.App_Code;
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
                    if (Request.QueryString["ID"] != null)
                    {
                        hdnEditMode.Value = "true";
                        hdnEditID.Value = Request.QueryString["ID"].ToString();
                    }
                    else
                    {
                        hdnEditMode.Value = "false";
                        hdnEditID.Value = "";
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
                ExceptionLogging.SendErrorToText(ex);
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
                txtReason.Text = "";

                modalReason.Show();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                ExceptionLogging.SendErrorToText(ex);
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

                ExceptionLogging.SendErrorToText(ex);
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

                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_SID()
        {
            try
            {
                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_SID_First_Edit", SITEID: drpsite.SelectedValue, ID: hdnEditID.Value);

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        divSID.Visible = true;
                        hdnSID_VERIFY.Value = ds.Tables[1].Rows[0]["ISVERIFY"].ToString().Trim();

                        if (ds.Tables[1].Rows[0]["CONTROLTYPE"].ToString().Trim() == "Textbox")
                        {
                            txtSpecimen.Visible = true;
                            drpSpecimen.Visible = false;
                            if (ds.Tables[1].Rows[0]["MAXLEN"].ToString().Trim() != "0" && ds.Tables[1].Rows[0]["MAXLEN"].ToString().Trim() != "")
                            {
                                txtSpecimen.MaxLength = Convert.ToInt32(ds.Tables[1].Rows[0]["MAXLEN"].ToString().Trim());
                            }
                        }
                        else
                        {
                            txtSpecimen.Visible = false;
                            drpSpecimen.Visible = true;

                            drpSpecimen.Items.Clear();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                drpSpecimen.DataSource = ds.Tables[0];
                                drpSpecimen.DataValueField = "SpecimenID";
                                drpSpecimen.DataBind();
                            }
                            drpSpecimen.Items.Insert(0, new ListItem("", ""));
                        }
                    }
                    else
                    {
                        divSID.Visible = false;
                    }

                    EditDATA = ds.Tables[2];

                    drpSpecimen.SelectedValue = EditDATA.Rows[0]["SID"].ToString();
                    txtSpecimen.Text = EditDATA.Rows[0]["SID"].ToString();

                    drpsite.SelectedValue = EditDATA.Rows[0]["SITEID"].ToString();
                    drpsite.Enabled = false;

                    txtComment.Text = EditDATA.Rows[0]["Comment"].ToString();
                    hdnOldComment.Value = EditDATA.Rows[0]["Comment"].ToString();
                }
                else
                {
                    drpSpecimen.Items.Clear();
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_SID_First", SITEID: drpsite.SelectedValue);

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        divSID.Visible = true;
                        hdnSID_VERIFY.Value = ds.Tables[1].Rows[0]["ISVERIFY"].ToString().Trim();
                        if (ds.Tables[1].Rows[0]["CONTROLTYPE"].ToString().Trim() == "Textbox")
                        {
                            txtSpecimen.Visible = true;
                            drpSpecimen.Visible = false;
                            if (ds.Tables[1].Rows[0]["MAXLEN"].ToString().Trim() != "0" && ds.Tables[1].Rows[0]["MAXLEN"].ToString().Trim() != "")
                            {
                                txtSpecimen.MaxLength = Convert.ToInt32(ds.Tables[1].Rows[0]["MAXLEN"].ToString().Trim());
                            }
                        }
                        else
                        {
                            txtSpecimen.Visible = false;
                            drpSpecimen.Visible = true;

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                drpSpecimen.DataSource = ds.Tables[0];
                                drpSpecimen.DataValueField = "SpecimenID";
                                drpSpecimen.DataBind();
                            }
                            drpSpecimen.Items.Insert(0, new ListItem("", ""));
                        }
                    }
                    else
                    {
                        divSID.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_SUBJECT()
        {
            try
            {
                DataSet ds = Dal_DE.GET_SUBJID_SP(drpsite.SelectedValue);

                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    divSubject.Visible = true;
                    hdnSUBJID_VERIFY.Value = ds.Tables[1].Rows[0]["ISVERIFY"].ToString().Trim();

                    if (ds.Tables[1].Rows[0]["CONTROLTYPE"].ToString().Trim() == "Textbox")
                    {
                        txtSubject.Visible = true;
                        drpSubject.Visible = false;
                        if (ds.Tables[1].Rows[0]["MAXLEN"].ToString().Trim() != "0" && ds.Tables[1].Rows[0]["MAXLEN"].ToString().Trim() != "")
                        {
                            txtSpecimen.MaxLength = Convert.ToInt32(ds.Tables[1].Rows[0]["MAXLEN"].ToString().Trim());
                        }
                    }
                    else
                    {
                        txtSubject.Visible = false;
                        drpSubject.Visible = true;

                        drpSubject.Items.Clear();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            drpSubject.DataSource = ds.Tables[0];
                            drpSubject.DataValueField = "SUBJID";
                            drpSubject.DataBind();
                        }
                        drpSubject.Items.Insert(0, new ListItem("", ""));
                    }
                }
                else
                {
                    divSubject.Visible = false;
                }

                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    drpSubject.SelectedValue = EditDATA.Rows[0]["SUBJID"].ToString();
                    txtSubject.Text = EditDATA.Rows[0]["SUBJID"].ToString();
                    hdnOldSubject.Value = EditDATA.Rows[0]["SUBJID"].ToString();
                }
                else
                {
                    drpSubject.SelectedIndex = 0;
                    txtSubject.Text = "";
                }
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_VISIT()
        {
            try
            {
                string SUBJID = "";

                if (drpSubject.Visible)
                {
                    SUBJID = drpSubject.SelectedValue;
                }
                else
                {
                    SUBJID = txtSubject.Text;
                }

                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    drpVisit.Items.Clear();
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_VISIT_Edit", SUBJID: SUBJID, SPECTYP: drpSpecimenType.SelectedValue, ID: hdnEditID.Value);
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
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_VISIT", SUBJID: SUBJID, SPECTYP: drpSpecimenType.SelectedValue);
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
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void drpsite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpSpecimen.Visible)
                {
                    GET_SID();
                }

                if (drpSubject.Visible)
                {
                    GET_SUBJECT();
                }

                FocusWithoutScroll(drpSpecimenType);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                            FocusWithoutScroll(txtDATA);
                        }
                        else if (CONTROLTYPE == "Dropdown")
                        {
                            DropDownList drpDATA = (DropDownList)item.FindControl("drpDATA");
                            FocusWithoutScroll(drpDATA);
                        }
                        else if (CONTROLTYPE == "Radio Button")
                        {
                            Repeater repeatRadio = (Repeater)item.FindControl("repeatRadio");
                            if (repeatRadio.Items.Count > 0)
                            {
                                RepeaterItem itemRadio = repeatRadio.Items[0];
                                RadioButton radioDATA = (RadioButton)itemRadio.FindControl("radioDATA");
                                FocusWithoutScroll(radioDATA);
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
                            }
                        }
                    }
                }

                GET_ALIQUOTS();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_ALIQUOTS()
        {
            try
            {
                string SID = "", SUBJID = "";

                if (drpSpecimen.Visible)
                {
                    SID = drpSpecimen.SelectedValue;
                }
                else
                {
                    SID = txtSpecimen.Text;
                }

                if (drpSubject.Visible)
                {
                    SUBJID = drpSubject.SelectedValue;
                }
                else
                {
                    SUBJID = txtSubject.Text;
                }

                if (Convert.ToBoolean(hdnEditMode.Value))
                {
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_ALIQUOT_PREP_First_Edit", SUBJID: SUBJID, VISITNUM: drpVisit.SelectedValue, SID: SID, SITEID: drpsite.SelectedValue, ID: hdnEditID.Value);

                    DataRow[] drALIQUOT_PREP = ds.Tables[0].Select(" VARIABLENAME = 'ALIQUOT_PREP' ");
                    DataRow[] drSIDALQ = ds.Tables[0].Select(" VARIABLENAME = 'SIDALQ' ");
                    DataRow[] drSCANALQ = ds.Tables[0].Select(" VARIABLENAME = 'SCANALQ' ");

                    ViewState["dtALIQUOTFIELDS"] = ds.Tables[1];

                    if (drALIQUOT_PREP.Length > 0)
                    {
                        divAliquotPrep.Visible = true;

                        gridAliquots.DataSource = ds.Tables[2];
                        gridAliquots.DataBind();

                        if (drSIDALQ.Length > 0)
                        {
                            gridAliquots.Columns[2].Visible = true;
                        }
                        else
                        {
                            gridAliquots.Columns[2].Visible = false;
                        }

                        if (drSCANALQ.Length > 0)
                        {
                            hdnALIQUOT_VERIFY.Value = drSCANALQ[0]["ISVERIFY"].ToString();

                            gridAliquots.Columns[3].Visible = true;
                        }
                        else
                        {
                            gridAliquots.Columns[3].Visible = false;
                        }

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            foreach (GridViewRow gvRow in gridAliquots.Rows)
                            {
                                Repeater rptALIQUOT = (Repeater)gvRow.FindControl("rptALIQUOT");
                                Label lblALIQUOTID = (Label)gvRow.FindControl("lblALIQUOTID");

                                foreach (RepeaterItem repeater in rptALIQUOT.Items)
                                {
                                    HiddenField hdnVARIABLENAME = (HiddenField)repeater.FindControl("hdnVARIABLENAME");

                                    TextBox txtDATA = (TextBox)repeater.FindControl("txtDATA");
                                    HiddenField hdnOldDATA = (HiddenField)repeater.FindControl("hdnOldDATA");

                                    DataRow[] drDATA = ds.Tables[2].Select(" ALIQUOTID = '" + lblALIQUOTID.Text + "' ");

                                    txtDATA.Text = drDATA[0][hdnVARIABLENAME.Value].ToString();
                                    hdnOldDATA.Value = drDATA[0][hdnVARIABLENAME.Value].ToString();
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
                else
                {
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_ALIQUOT_PREP_First", SUBJID: SUBJID, VISITNUM: drpVisit.SelectedValue, SID: SID, SITEID: drpsite.SelectedValue, ID: hdnEditID.Value);

                    DataRow[] drALIQUOT_PREP = ds.Tables[0].Select(" VARIABLENAME = 'ALIQUOT_PREP' ");
                    DataRow[] drSIDALQ = ds.Tables[0].Select(" VARIABLENAME = 'SIDALQ' ");
                    DataRow[] drSCANALQ = ds.Tables[0].Select(" VARIABLENAME = 'SCANALQ' ");

                    ViewState["dtALIQUOTFIELDS"] = ds.Tables[1];

                    if (drALIQUOT_PREP.Length > 0)
                    {
                        divAliquotPrep.Visible = true;

                        gridAliquots.DataSource = ds.Tables[2];
                        gridAliquots.DataBind();

                        if (drSIDALQ.Length > 0)
                        {
                            gridAliquots.Columns[2].Visible = true;
                        }
                        else
                        {
                            gridAliquots.Columns[2].Visible = false;
                        }

                        if (drSCANALQ.Length > 0)
                        {
                            hdnALIQUOT_VERIFY.Value = drSCANALQ[0]["ISVERIFY"].ToString();

                            gridAliquots.Columns[3].Visible = true;
                        }
                        else
                        {
                            gridAliquots.Columns[3].Visible = false;
                        }

                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            ChangeColumnDataType(ds.Tables[3], "SEQNO", typeof(int));

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
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        static void ChangeColumnDataType(DataTable table, string columnName, Type newType)
        {
            // Add a new column with the desired data type
            DataColumn newColumn = new DataColumn(columnName + "_new", newType);
            table.Columns.Add(newColumn);

            // Copy and convert the data from the old column to the new column
            foreach (DataRow row in table.Rows)
            {
                row[newColumn.ColumnName] = Convert.ChangeType(row[columnName], newType);
            }

            // Remove the old column
            table.Columns.Remove(columnName);

            // Rename the new column to the original name
            newColumn.ColumnName = columnName;
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
                            FocusWithoutScroll(drpSpecimen);
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Specimen ID already entered', 'warning');", true);
                        }
                        else
                        {
                            GET_ALIQUOTS();

                            if (drpSubject.Visible)
                            {
                                FocusWithoutScroll(drpSubject);
                            }
                            else
                            {
                                FocusWithoutScroll(txtSubject);
                            }
                        }
                    }
                    else
                    {
                        if (hdnSID_VERIFY.Value == "Yes")
                        {
                            drpSpecimen.SelectedIndex = 0;
                            FocusWithoutScroll(drpSpecimen);
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid Specimen ID', 'warning');", true);
                        }
                    }
                }
                else
                {
                    FocusWithoutScroll(drpSpecimen);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please enter Specimen ID', 'warning');", true);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void txtSpecimen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSpecimen.Text != "")
                {
                    DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "CHECK_SpecimenID", SITEID: drpsite.SelectedValue, SID: txtSpecimen.Text);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            txtSpecimen.Text = "";
                            FocusWithoutScroll(txtSpecimen);
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Specimen ID already entered', 'warning');", true);
                        }
                        else
                        {
                            GET_ALIQUOTS();

                            if (drpSubject.Visible)
                            {
                                FocusWithoutScroll(drpSubject);
                            }
                            else
                            {
                                FocusWithoutScroll(txtSubject);
                            }
                        }
                    }
                    else
                    {
                        if (hdnSID_VERIFY.Value == "Yes")
                        {
                            txtSpecimen.Text = "";
                            FocusWithoutScroll(txtSpecimen);
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid Specimen ID', 'warning');", true);
                        }
                    }
                }
                else
                {
                    FocusWithoutScroll(txtSpecimen);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please enter Specimen ID', 'warning');", true);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                        FocusWithoutScroll(drpVisit);
                    }
                }
                else
                {
                    GET_ENTRY_FIELDS();

                    if (hdnSUBJID_VERIFY.Value == "Yes")
                    {
                        drpSubject.SelectedIndex = 0;
                        FocusWithoutScroll(drpSubject);
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid Subject ID', 'warning');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void txtSubject_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "CHECK_SUBJID", SITEID: drpsite.SelectedValue, SUBJID: txtSubject.Text);
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
                            NEWVAL: txtSubject.Text);

                        txtReason.Focus();
                    }
                    else
                    {
                        FocusWithoutScroll(drpVisit);
                    }
                }
                else
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

                    if (hdnSUBJID_VERIFY.Value == "Yes")
                    {
                        txtSubject.Text = "";
                        FocusWithoutScroll(txtSubject);
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid Subject ID', 'warning');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                            txtDATA.CssClass = "form-control";
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
                ExceptionLogging.SendErrorToText(ex);
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
                    if (divSID.Visible)
                    {
                        FocusWithoutScroll(drpSpecimen);
                    }
                    else
                    {
                        FocusWithoutScroll(drpSubject);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                            if (drv["REQUIRED"].ToString() == "True")
                            {
                                txtDATA.CssClass = txtDATA.CssClass + " required";
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
                            if (drv["REQUIRED"].ToString() == "True")
                            {
                                txtDATA.CssClass = txtDATA.CssClass + " required";
                            }

                            break;

                        case "Multiline Textbox":
                            txtDATA.Visible = true;
                            txtDATA.TextMode = TextBoxMode.MultiLine;
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                txtDATA.Text = EditDATA.Rows[0][VARIABLENAME].ToString();
                            }
                            if (drv["REQUIRED"].ToString() == "True")
                            {
                                txtDATA.CssClass = txtDATA.CssClass + " required";
                            }

                            break;

                        case "Date":
                            txtDATA.Visible = true;
                            txtDATA.CssClass = txtDATA.CssClass + " txtDate";
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                txtDATA.Text = EditDATA.Rows[0][VARIABLENAME].ToString();
                            }
                            if (drv["REQUIRED"].ToString() == "True")
                            {
                                txtDATA.CssClass = txtDATA.CssClass + " required";
                            }

                            break;

                        case "Time":
                            txtDATA.Visible = true;
                            txtDATA.CssClass = txtDATA.CssClass + " txtTime";
                            if (Convert.ToBoolean(hdnEditMode.Value))
                            {
                                txtDATA.Text = EditDATA.Rows[0][VARIABLENAME].ToString();
                            }
                            if (drv["REQUIRED"].ToString() == "True")
                            {
                                txtDATA.CssClass = txtDATA.CssClass + " required";
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
                            if (drv["REQUIRED"].ToString() == "True")
                            {
                                drpDATA.CssClass = drpDATA.CssClass + " required";
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

                            if (drv["REQUIRED"].ToString() == "True")
                            {
                                foreach (RepeaterItem itemRadio in repeatRadio.Items)
                                {
                                    ((RadioButton)itemRadio.FindControl("radioDATA")).CssClass = ((RadioButton)itemRadio.FindControl("radioDATA")).CssClass + " required";
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

                            if (drv["REQUIRED"].ToString() == "True")
                            {
                                foreach (RepeaterItem itemChk in repeatChk.Items)
                                {
                                    ((RadioButton)itemChk.FindControl("chkDATA")).CssClass = ((RadioButton)itemChk.FindControl("chkDATA")).CssClass + " required";
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
                ExceptionLogging.SendErrorToText(ex);
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
                ExceptionLogging.SendErrorToText(ex);
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

                    if (Convert.ToBoolean(hdnEditMode.Value))
                    {
                        string script = @"
                                    swal({
                                        title: 'Success',
                                        text: 'Specimen ID entered successfully.',
                                        icon: 'success',
                                        button: 'OK'
                                    }).then((value) => {
                                        window.location.href = 'DE_EDIT_L1.aspx?ID=" + hdnEditID.Value + "';}); ";

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    }
                    else
                    {
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

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                ExceptionLogging.SendErrorToText(ex);
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
                ExceptionLogging.SendErrorToText(ex);

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

                string SID = "", SUBJID = "";

                if (drpSpecimen.Visible)
                {
                    SID = drpSpecimen.SelectedValue;
                }
                else
                {
                    SID = txtSpecimen.Text;
                }

                if (drpSubject.Visible)
                {
                    SUBJID = drpSubject.SelectedValue;
                }
                else
                {
                    SUBJID = txtSubject.Text;
                }

                COLUMN = COLUMN + " @ni$h SITEID";
                DATA = DATA + " @ni$h '" + drpsite.SelectedValue + "' ";

                COLUMN = COLUMN + " @ni$h SPECTYP";
                DATA = DATA + " @ni$h '" + drpSpecimenType.SelectedValue + "' ";

                COLUMN = COLUMN + " @ni$h SID";
                DATA = DATA + " @ni$h '" + SID + "' ";

                COLUMN = COLUMN + " @ni$h SUBJID";
                DATA = DATA + " @ni$h '" + SUBJID + "' ";

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

                UPDATEQUERY = UPDATEQUERY + " WHERE ID = '" + hdnEditID.Value + "' ";

                Dal_DE.DATA_ENTRY_SP(
                    ACTION: "INSERT_SPECIMEN_DATA_First",
                    INSERTQUERY: INSERTQUERY,
                    UPDATEQUERY: UPDATEQUERY,
                    ID: hdnEditID.Value,
                    SID: SID,
                    SUBJID: SUBJID,
                    VISITNUM: drpVisit.SelectedValue
                    );

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void INSERT_ALIQUOT_DATA()
        {
            try
            {
                string SID = "", SUBJID = "";

                if (drpSpecimen.Visible)
                {
                    SID = drpSpecimen.SelectedValue;
                }
                else
                {
                    SID = txtSpecimen.Text;
                }

                if (drpSubject.Visible)
                {
                    SUBJID = drpSubject.SelectedValue;
                }
                else
                {
                    SUBJID = txtSubject.Text;
                }

                foreach (GridViewRow gvRow in gridAliquots.Rows)
                {
                    string UPDATEQUERY = "";

                    HiddenField hdnID = (HiddenField)gvRow.FindControl("hdnID");
                    Label lblALIQUOTID = (Label)gvRow.FindControl("lblALIQUOTID");
                    Label lblALIQUOTTYPE = (Label)gvRow.FindControl("lblALIQUOTTYPE");
                    Label lblALIQUOTNO_CONCAT = (Label)gvRow.FindControl("lblALIQUOTNO_CONCAT");
                    TextBox txtALIQUOTNO = (TextBox)gvRow.FindControl("txtALIQUOTNO");
                    TextBox txtBOXNO = (TextBox)gvRow.FindControl("txtBOXNO");
                    TextBox txtSLOTNO = (TextBox)gvRow.FindControl("txtSLOTNO");
                    Repeater rptALIQUOT = (Repeater)gvRow.FindControl("rptALIQUOT");

                    if (txtBOXNO.Text != "" && txtSLOTNO.Text != "")
                    {
                        foreach (RepeaterItem item in rptALIQUOT.Items)
                        {
                            string repeatVARIABLENAME = "", repeatDATA = "";

                            HiddenField hdnVARIABLENAME = (HiddenField)item.FindControl("hdnVARIABLENAME");
                            repeatVARIABLENAME = hdnVARIABLENAME.Value.Trim();

                            TextBox txtDATA = (TextBox)item.FindControl("txtDATA");
                            repeatDATA = txtDATA.Text;

                            if (UPDATEQUERY == "")
                            {
                                UPDATEQUERY = "UPDATE [ALIQUOT_DATA] SET [" + repeatVARIABLENAME + "] = '" + repeatDATA + "' ";
                            }
                            else
                            {
                                UPDATEQUERY = UPDATEQUERY + ", " + repeatVARIABLENAME + " = '" + repeatDATA + "' ";
                            }
                        }

                        if (UPDATEQUERY != "")
                        {
                            UPDATEQUERY = UPDATEQUERY + " WHERE  ALQID = '" + hdnID.Value + "' AND [SID] = '" + SID + "' AND SUBJID = '" + SUBJID + "' AND VISITNUM = '" + drpVisit.SelectedValue + "' ";
                        }

                        Dal_DE.DATA_ENTRY_SP(
                        ACTION: "INSERT_ALIQUOT_DATA_First",
                        SITEID: drpsite.SelectedValue,
                        SID: SID,
                        SUBJID: SUBJID,
                        VISITNUM: drpVisit.SelectedValue,
                        VISIT: drpVisit.SelectedItem.Text,
                        ALQID: hdnID.Value,
                        ALIQUOTID: lblALIQUOTID.Text,
                        ALIQUOTTYPE: lblALIQUOTTYPE.Text,
                        ALIQUOTNO_CONCAT: lblALIQUOTNO_CONCAT.Text,
                        ALIQUOTNO: txtALIQUOTNO.Text,
                        BOXNO: txtBOXNO.Text,
                        SLOTNO: txtSLOTNO.Text,
                        UPDATEQUERY: UPDATEQUERY
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                        SET_AGE();
                    }

                    FOCUS_NEXT_ITEM(Convert.ToInt32(hdnINDEX.Value));
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private DataTable GET_GRID_DATATABLE()
        {
            string SID = "", SUBJID = "";

            if (drpSpecimen.Visible)
            {
                SID = drpSpecimen.SelectedValue;
            }
            else
            {
                SID = txtSpecimen.Text;
            }

            if (drpSubject.Visible)
            {
                SUBJID = drpSubject.SelectedValue;
            }
            else
            {
                SUBJID = txtSubject.Text;
            }

            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("VARIABLENAME");
            outputTable.Columns.Add("DATA");

            outputTable.Rows.Add("SITEID", drpsite.SelectedValue);
            outputTable.Rows.Add("SPECTYP", drpSpecimenType.SelectedValue);
            outputTable.Rows.Add("SID", SID);
            outputTable.Rows.Add("SUBJID", SUBJID);
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

        private void SET_AGE()
        {
            try
            {
                foreach (RepeaterItem item in repeatFields.Items)
                {
                    HiddenField hdnVARIABLENAME = (HiddenField)item.FindControl("hdnVARIABLENAME");

                    if (hdnVARIABLENAME.Value.Trim() == "AGE")
                    {
                        HiddenField hdnCONTROLTYPE = (HiddenField)item.FindControl("hdnCONTROLTYPE");
                        Label lblFIELDNAME = (Label)item.FindControl("lblFIELDNAME");

                        string FIELDNAME = lblFIELDNAME.Text;

                        string CONTROLTYPE = hdnCONTROLTYPE.Value.Trim();

                        if (CONTROLTYPE == "Textbox" || CONTROLTYPE == "Date" || CONTROLTYPE == "Time" || CONTROLTYPE == "Multiline Textbox" || CONTROLTYPE == "Readonly")
                        {
                            dtCurrentDATA = GET_GRID_DATATABLE();

                            TextBox txtDATA = (TextBox)item.FindControl("txtDATA");

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
                            txtDATA.Text = AGE.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
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

                if (txtALIQUOTNO.Text != lblALIQUOTNO_CONCAT.Text && hdnALIQUOT_VERIFY.Value == "Yes")
                {
                    txtALIQUOTNO.Text = "";
                    FocusWithoutScroll(txtALIQUOTNO);

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
                ExceptionLogging.SendErrorToText(ex);
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
                        FocusWithoutScroll(txtBOXNO);
                        result = false;

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Same box no. can not be used for " + gv_lblALIQUOTTYPE.Text + " and " + lblALIQUOTTYPE.Text + ".', 'warning');", true);
                    }
                    else if (gv_hdnINDEX.Value != hdnINDEX.Value && gv_txtBOXNO.Text == txtBOXNO.Text && (gv_txtBOXNO.Text != "" && gv_txtSLOTNO.Text == txtSLOTNO.Text))
                    {
                        txtBOXNO.Text = "";
                        FocusWithoutScroll(txtBOXNO);
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
                        FocusWithoutScroll(txtBOXNO);
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
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private string CHECK_BOXNO_SLOTNO(string BOXNO, string SLOTNO)
        {
            string MSG = "";
            try
            {
                string SID = "", SUBJID = "";

                if (drpSpecimen.Visible)
                {
                    SID = drpSpecimen.SelectedValue;
                }
                else
                {
                    SID = txtSpecimen.Text;
                }

                if (drpSubject.Visible)
                {
                    SUBJID = drpSubject.SelectedValue;
                }
                else
                {
                    SUBJID = txtSubject.Text;
                }


                DataSet ds = Dal_DE.DATA_ENTRY_SP(
                    ACTION: "CHECK_BOXNO_SLOTNO",
                    SID: SID,
                    SUBJID: SUBJID,
                    VISITNUM: drpVisit.SelectedValue,
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
                ExceptionLogging.SendErrorToText(ex);
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
                        FocusWithoutScroll(txtSLOTNO);
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
                        FocusWithoutScroll(txtSLOTNO);
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', '" + MSG + "', 'warning');", true);
                    }
                    else
                    {
                        int INDEX = Convert.ToInt32(hdnINDEX.Value);
                        if (gridAliquots.Rows.Count > INDEX + 1)
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
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnSubmitReason_Click(object sender, EventArgs e)
        {
            try
            {
                string SID = "", SUBJID = "";

                if (drpSpecimen.Visible)
                {
                    SID = drpSpecimen.SelectedValue;
                }
                else
                {
                    SID = txtSpecimen.Text;
                }

                if (drpSubject.Visible)
                {
                    SUBJID = drpSubject.SelectedValue;
                }
                else
                {
                    SUBJID = txtSubject.Text;
                }


                if (hdnReasonALQID.Value != "")
                {
                    Dal_DE.DATA_ENTRY_SP(
                        ACTION: "UPDATE_ALIQUOT_DATA",
                        ID: hdnEditID.Value,
                        SID: SID,
                        SUBJID: SUBJID,
                        VISITNUM: drpVisit.SelectedValue,
                        VISIT: drpVisit.SelectedItem.Text,
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
                        ID: hdnEditID.Value,
                        SID: SID,
                        SUBJID: SUBJID,
                        VISITNUM: drpVisit.SelectedValue,
                        VISIT: drpVisit.SelectedItem.Text,
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
                            ID: hdnEditID.Value,
                            SID: SID,
                            SUBJID: SUBJID,
                            VISITNUM: drpVisit.SelectedValue,
                            VISIT: drpVisit.SelectedItem.Text,
                            ALQID: hdnReasonALQID.Value,
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
                        SET_AGE();
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
                                        window.location.href = 'DATA_ENTRY.aspx?ID=" + hdnEditID.Value + "'; }); ";

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnCancelReason_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DATA_ENTRY.aspx?ID=" + hdnEditID.Value + "");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void txtDATA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = (TextBox)sender;
                RepeaterItem childRepeaterItem = (RepeaterItem)txtBox.NamingContainer;
                Repeater childRepeater = (Repeater)childRepeaterItem.NamingContainer;
                GridViewRow parentGridViewRow = (GridViewRow)childRepeater.NamingContainer;
                GridView parentGridView = (GridView)parentGridViewRow.NamingContainer;

                Label lblFIELDNAME = (Label)childRepeaterItem.FindControl("lblFIELDNAME");
                HiddenField hdnVARIABLENAME = (HiddenField)childRepeaterItem.FindControl("hdnVARIABLENAME");
                TextBox txtDATA = (TextBox)childRepeaterItem.FindControl("txtDATA");
                HiddenField hdnOldDATA = (HiddenField)childRepeaterItem.FindControl("hdnOldDATA");

                HiddenField hdnID = (HiddenField)parentGridViewRow.FindControl("hdnID");
                HiddenField hdnINDEX = (HiddenField)parentGridViewRow.FindControl("hdnINDEX");

                if (txtDATA.Text != hdnOldDATA.Value)
                {
                    if (Convert.ToBoolean(hdnEditMode.Value))
                    {
                        SHOW_MODAL_REASON(
                            FIELDNAME: lblFIELDNAME.Text,
                            VARIABLENAME: hdnVARIABLENAME.Value,
                            INDEX: hdnINDEX.Value,
                            OLDVAL: hdnOldDATA.Value,
                            NEWVAL: txtDATA.Text);

                        hdnReasonALQID.Value = hdnID.Value;

                        txtReason.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void gridAliquots_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Control divALIQUOT = (Control)e.Row.FindControl("divALIQUOT");
                    Repeater rptALIQUOT = (Repeater)e.Row.FindControl("rptALIQUOT");

                    DataTable dtALIQUOTFIELDS = (DataTable)ViewState["dtALIQUOTFIELDS"];

                    if (dtALIQUOTFIELDS.Rows.Count > 0)
                    {
                        divALIQUOT.Visible = true;
                        rptALIQUOT.DataSource = dtALIQUOTFIELDS;
                        rptALIQUOT.DataBind();
                    }
                    else
                    {
                        divALIQUOT.Visible = false;
                        rptALIQUOT.DataSource = null;
                        rptALIQUOT.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
    }
}