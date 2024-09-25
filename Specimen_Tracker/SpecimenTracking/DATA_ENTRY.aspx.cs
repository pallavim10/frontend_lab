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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SpecimenType();
                    GET_SITE();
                    GET_SUBJECT();
                    GET_SID();
                    GET_VISIT();
                    GET_ENTRY_FIELDS();
                }
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
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_ALIQUOT_PREP_First", VISITNUM: drpVisit.SelectedValue, SID: drpSpecimen.SelectedValue);

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

                    drpVisit.Focus();
                }
                else
                {
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

                                radioDATA.Enabled = false;
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

                                chkDATA.Enabled = false;
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
                drpSpecimen.Focus();
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

                    TextBox txtDATA = (TextBox)e.Item.FindControl("txtDATA");
                    DropDownList drpDATA = (DropDownList)e.Item.FindControl("drpDATA");
                    Repeater repeatRadio = (Repeater)e.Item.FindControl("repeatRadio");
                    Repeater repeatChk = (Repeater)e.Item.FindControl("repeatChk");
                    Control divRadChk = (Control)e.Item.FindControl("divRadChk");

                    switch (drv["CONTROLTYPE"].ToString().Trim())
                    {
                        case "Readonly":
                            txtDATA.Visible = true;
                            txtDATA.Attributes.Add("Readonly", "Readonly");
                            break;

                        case "Textbox":
                            txtDATA.Visible = true;

                            if (drv["VARIABLENAME"].ToString().Trim() == "INITIAL")
                            {
                                txtDATA.CssClass = txtDATA.CssClass + " initial";
                            }

                            break;

                        case "Multiline Textbox":
                            txtDATA.Visible = true;
                            txtDATA.TextMode = TextBoxMode.MultiLine;
                            break;

                        case "Date":
                            txtDATA.Visible = true;
                            txtDATA.CssClass = txtDATA.CssClass + " txtDate";
                            break;

                        case "Time":
                            txtDATA.Visible = true;
                            txtDATA.CssClass = txtDATA.CssClass + " txtTime";
                            break;

                        case "Dropdown":
                            drpDATA.Visible = true;
                            ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_FIELD_OPTIONS", VARIABLENAME: drv["VARIABLENAME"].ToString().Trim());
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                drpDATA.DataSource = ds.Tables[0];
                                drpDATA.DataValueField = "OPTION_VALUE";
                                drpDATA.DataBind();
                                drpDATA.Items.Insert(0, new ListItem("--Select--", "0"));
                            }
                            break;

                        case "Radio Button":
                            divRadChk.Visible = true;
                            ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_FIELD_OPTIONS", VARIABLENAME: drv["VARIABLENAME"].ToString().Trim());
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                repeatRadio.DataSource = ds.Tables[0];
                                repeatRadio.DataBind();
                            }
                            break;

                        case "Checkbox":
                            divRadChk.Visible = true;
                            ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_FIELD_OPTIONS", VARIABLENAME: drv["VARIABLENAME"].ToString().Trim());
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                repeatChk.DataSource = ds.Tables[0];
                                repeatChk.DataBind();
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
                            CritName: drCases["Criteria"].ToString()
                            );

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

                HiddenField hdnVARIABLENAME = (HiddenField)item.FindControl("hdnVARIABLENAME");
                Label lblFIELDNAME = (Label)item.FindControl("lblFIELDNAME");

                if (hdnVARIABLENAME.Value == "BRTHDAT" || hdnVARIABLENAME.Value == "COLDAT")
                {
                    SET_AGE(lblFIELDNAME.Text);
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
    }
}