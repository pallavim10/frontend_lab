using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class ePRO_PUSH_To_eCRF : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["INVID"] == null || Request.QueryString["SUBJID"] == null || Request.QueryString["VISITID"] == null || Request.QueryString["Indication"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.IsPostBack)
                {
                    //if (drpModule.SelectedValue == "0")
                    //{
                    //    lblModuleName.Text = "";
                    //}
                    //else
                    //{
                    //    lblModuleName.Text = drpModule.SelectedItem.Text;
                    //}

                    hfDM_RECID.Value = "-1";

                    lblPVID.Text = Session["PROJECTID"].ToString() + "-" + Request.QueryString["INVID"].ToString() + "-" + Request.QueryString["SUBJID"].ToString() + "-" + Request.QueryString["VISITID"].ToString() + "-" + Request.QueryString["MODULEID"].ToString() + "-" + Request.QueryString["VISITCOUNT"].ToString();
                    hfDM_VISITNUM.Value = Request.QueryString["VISITID"].ToString();
                    hfDM_PAGENUM.Value = Request.QueryString["MODULEID"].ToString();
                    hfDM_PAGESTATUS.Value = "0";
                    hfDM_PVID.Value = lblPVID.Text;

                    lblSiteId.Text = "SITE ID : " + Request.QueryString["INVID"].ToString();
                    lblSubjectId.Text = "SUBJECT ID : " + Request.QueryString["SUBJID"].ToString();
                    lblVisit.Text = "VISIT : " + Request.QueryString["VISITID"].ToString();
                    lblIndication.Text = "INDICATION : " + Request.QueryString["Indication"].ToString();

                    if (Request.QueryString["RECID"] == null)
                    {
                        hfDM_RECID.Value = "-1";
                    }
                    else
                    {
                        hfDM_RECID.Value = Request.QueryString["RECID"].ToString();
                    }

                    GetStructure();
                    GetRecords(grd_Data);

                    GetAuditDetails();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();

            }
        }

        private string GET_STANDARD_DATA(string VARIABLENAME)
        {
            string DATA = "";
            try
            {
                DataSet ds = dal.GetSet_DM_ProjectData(Action: "GET_STANDARD_DATA", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DATA = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            return DATA;
        }

        private string GET_AutoNum(string VARIABLENAME)
        {
            string res = "";
            try
            {
                DataSet ds = dal.GetSet_DM_ProjectData(
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

        protected void DSMH_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DAL dal;
                    dal = new DAL();

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
                    string Source = dr["Source"].ToString();

                    string STANDARD = dr["STANDARD"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();

                    string MANDATORY = dr["MANDATORY"].ToString();

                    hdnSourceValue.Value = Source;
                    HtmlControl ICON = (HtmlControl)e.Row.FindControl("ICONCLASS");
                    Label lblicon = (Label)e.Row.FindControl("lblicon");

                    if (Source == "IWRS")
                    {
                        ICON.Attributes.Add("class", "fas fa-dice");
                        ICON.Visible = true;
                        lblicon.ToolTip = "IWRS";
                    }
                    else if (Source == "Resource")
                    {
                        ICON.Attributes.Add("class", "fas fa-toolbox");
                        ICON.Visible = true;
                        lblicon.ToolTip = "Resource";
                    }
                    else if (Source == "EMR")
                    {
                        ICON.Attributes.Add("class", "fas fa-book");
                        ICON.Visible = true;
                        lblicon.ToolTip = "EMR";
                    }
                    else if (Source == "Paper CRF" || Source == "eCRF")
                    {
                        ICON.Attributes.Add("class", "fas fa-coins");
                        ICON.Visible = true;
                        lblicon.ToolTip = "eCRF";
                    }
                    else if (Source == "ePRO")
                    {
                        ICON.Attributes.Add("class", "fas fa-chalkboard-teacher");
                        ICON.Visible = true;
                        lblicon.ToolTip = "ePRO";
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
                            btnEdit.Attributes.Add("style", "width: 300px;");
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

                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                btnEdit.Text = data;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (FIELDNAME.ToUpperInvariant() == "Lab ID".ToUpperInvariant() || FIELDNAME.ToUpperInvariant() == "Laboratory ID".ToUpperInvariant())
                        {
                            DataSet ds;
                            ds = new DataSet();
                            ds = dal.LAB_MASTER_SP(Action: "GET_Lab", INVID: Request.QueryString["INVID"].ToString());
                            btnEdit.DataSource = ds;
                            btnEdit.DataTextField = "Lab_Name";
                            btnEdit.DataValueField = "Lab_ID";
                            btnEdit.DataBind();
                            btnEdit.Items.Insert(0, new ListItem("--Select--", "0"));
                            if (Session["DM_Multi_Lab_ID"] != null)
                            {
                                btnEdit.SelectedValue = Session["DM_Multi_Lab_ID"].ToString();
                            }

                            btnEdit.Attributes.Add("onchange", "GetDefaultData(this)");
                        }
                        else
                        {
                            DataSet ds;
                            ds = new DataSet();

                            if (NONREPETATIVE == "True")
                            {
                                if (hfDM_RECID.Value != "-1")
                                {
                                    ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                                }
                                else
                                {
                                    ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                                }
                            }
                            else
                            {
                                ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
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

                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                btnEdit.SelectedValue = data;
                            }
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
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


                        if (NONREPETATIVE == "True")
                        {
                            if (hfDM_RECID.Value != "-1")
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                            else
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                        }
                        else
                        {
                            ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();


                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.Contains(data.Replace(" ", String.Empty)))
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                    }
                                }
                            }
                        }

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


                        if (NONREPETATIVE == "True")
                        {
                            if (hfDM_RECID.Value != "-1")
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                            else
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                        }
                        else
                        {
                            ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();


                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.Contains(data.Replace(" ", String.Empty)))
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                    }
                                }
                            }
                        }

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
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    HtmlImage img = (HtmlImage)e.Row.FindControl("GMQ");
                    img.ID = "GMQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("AQ");
                    img.ID = "AQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("MQ");
                    img.ID = "MQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("CM");
                    img.ID = "CM_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("AD");
                    img.ID = "AD_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    GridView grd_Data1 = e.Row.FindControl("grd_Data1") as GridView;
                    if (Session["DM_Multi_Lab_ID"] != null && LabData == "True")
                    {
                        if (Request.QueryString["MODULEID"] != null)
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), ID: ID, INDICATION: Request.QueryString["Indication"].ToString(), FIELDNAME: FIELDNAME, SITEID: Request.QueryString["INVID"].ToString(), SUBJID: Request.QueryString["SUBJID"].ToString(), LabID: Session["DM_Multi_Lab_ID"].ToString(),
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data1.DataSource = ds1.Tables[0];
                            grd_Data1.DataBind();
                        }
                        else
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), ID: ID, INDICATION: Request.QueryString["Indication"].ToString(), FIELDNAME: FIELDNAME, SITEID: Request.QueryString["INVID"].ToString(), SUBJID: Request.QueryString["SUBJID"].ToString(), LabID: Session["DM_Multi_Lab_ID"].ToString(),
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data1.DataSource = ds1.Tables[0];
                            grd_Data1.DataBind();
                        }
                    }
                    else
                    {
                        if (Request.QueryString["MODULEID"] != null)
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), INDICATION: Request.QueryString["Indication"].ToString(), ID: ID,
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data1.DataSource = ds1.Tables[0];
                            grd_Data1.DataBind();
                        }
                        else
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), INDICATION: Request.QueryString["Indication"].ToString(), ID: ID,
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data1.DataSource = ds1.Tables[0];
                            grd_Data1.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void DSMH1_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    string DEFULTVAL = dr["DEFAULTVAL1"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();
                    string LabData = dr["LabData"].ToString();
                    string Reference = dr["Refer"].ToString();
                    string AutoNum = dr["AutoNum"].ToString();
                    string Source = dr["Source"].ToString();

                    string STANDARD = dr["STANDARD"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();

                    string MANDATORY = dr["MANDATORY"].ToString();

                    hdnSourceValue.Value = Source;
                    HtmlControl ICON = (HtmlControl)e.Row.FindControl("ICONCLASS");
                    Label lblicon = (Label)e.Row.FindControl("lblicon");

                    if (Source == "IWRS")
                    {
                        ICON.Attributes.Add("class", "fas fa-dice");
                        ICON.Visible = true;
                        lblicon.ToolTip = "IWRS";
                    }
                    else if (Source == "Resource")
                    {
                        ICON.Attributes.Add("class", "fas fa-toolbox");
                        ICON.Visible = true;
                        lblicon.ToolTip = "Resource";
                    }
                    else if (Source == "EMR")
                    {
                        ICON.Attributes.Add("class", "fas fa-book");
                        ICON.Visible = true;
                        lblicon.ToolTip = "EMR";
                    }
                    else if (Source == "Paper CRF" || Source == "eCRF")
                    {
                        ICON.Attributes.Add("class", "fas fa-coins");
                        ICON.Visible = true;
                        lblicon.ToolTip = "eCRF";
                    }
                    else if (Source == "ePRO")
                    {
                        ICON.Attributes.Add("class", "fas fa-chalkboard-teacher");
                        ICON.Visible = true;
                        lblicon.ToolTip = "ePRO";
                    }

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
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

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
                            btnEdit.Attributes.Add("style", "width: 300px;");
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

                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                btnEdit.Text = data;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (FIELDNAME.ToUpperInvariant() == "Lab ID".ToUpperInvariant() || FIELDNAME.ToUpperInvariant() == "Laboratory ID".ToUpperInvariant())
                        {
                            DataSet ds;
                            ds = new DataSet();
                            ds = dal.LAB_MASTER_SP(Action: "GET_Lab", INVID: Request.QueryString["INVID"].ToString());
                            btnEdit.DataSource = ds;
                            btnEdit.DataTextField = "Lab_Name";
                            btnEdit.DataValueField = "Lab_ID";
                            btnEdit.DataBind();
                            btnEdit.Items.Insert(0, new ListItem("--Select--", "0"));
                            if (Session["DM_Multi_Lab_ID"] != null)
                            {
                                btnEdit.SelectedValue = Session["DM_Multi_Lab_ID"].ToString();
                            }

                            btnEdit.Attributes.Add("onchange", "GetDefaultData(this)");
                        }
                        else
                        {
                            DataSet ds;
                            ds = new DataSet();


                            if (NONREPETATIVE == "True")
                            {
                                if (hfDM_RECID.Value != "-1")
                                {
                                    ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                                }
                                else
                                {
                                    ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                                }
                            }
                            else
                            {
                                ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
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

                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                btnEdit.SelectedValue = data;
                            }
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
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


                        if (NONREPETATIVE == "True")
                        {
                            if (hfDM_RECID.Value != "-1")
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                            else
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                        }
                        else
                        {
                            ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();


                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.Contains(data.Replace(" ", String.Empty)))
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                    }
                                }
                            }
                        }

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


                        if (NONREPETATIVE == "True")
                        {
                            if (hfDM_RECID.Value != "-1")
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                            else
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                        }
                        else
                        {
                            ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();


                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.Contains(data.Replace(" ", String.Empty)))
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                    }
                                }
                            }
                        }

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
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    HtmlImage img = (HtmlImage)e.Row.FindControl("GMQ");
                    img.ID = "GMQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("AQ");
                    img.ID = "AQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("MQ");
                    img.ID = "MQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("CM");
                    img.ID = "CM_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("AD");
                    img.ID = "AD_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);


                    GridView grd_Data2 = e.Row.FindControl("grd_Data2") as GridView;
                    if (Session["DM_Multi_Lab_ID"] != null && LabData == "True")
                    {
                        if (Request.QueryString["MODULEID"] != null)
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), ID: ID, INDICATION: Request.QueryString["Indication"].ToString(), FIELDNAME: FIELDNAME, SITEID: Request.QueryString["INVID"].ToString(), SUBJID: Request.QueryString["SUBJID"].ToString(), LabID: Session["DM_Multi_Lab_ID"].ToString(),
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data2.DataSource = ds1.Tables[0];
                            grd_Data2.DataBind();
                        }
                        else
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), ID: ID, INDICATION: Request.QueryString["Indication"].ToString(), FIELDNAME: FIELDNAME, SITEID: Request.QueryString["INVID"].ToString(), SUBJID: Request.QueryString["SUBJID"].ToString(), LabID: Session["DM_Multi_Lab_ID"].ToString(),
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data2.DataSource = ds1.Tables[0];
                            grd_Data2.DataBind();
                        }
                    }
                    else
                    {
                        if (Request.QueryString["MODULEID"] != null)
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), INDICATION: Request.QueryString["Indication"].ToString(), ID: ID,
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data2.DataSource = ds1.Tables[0];
                            grd_Data2.DataBind();
                        }
                        else
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), INDICATION: Request.QueryString["Indication"].ToString(), ID: ID,
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data2.DataSource = ds1.Tables[0];
                            grd_Data2.DataBind();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void DSMH2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

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
                    string DEFULTVAL = dr["DEFAULTVAL1"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();
                    string LabData = dr["LabData"].ToString();
                    string Reference = dr["Refer"].ToString();
                    string AutoNum = dr["AutoNum"].ToString();
                    string Source = dr["Source"].ToString();

                    string STANDARD = dr["STANDARD"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();

                    string MANDATORY = dr["MANDATORY"].ToString();

                    hdnSourceValue.Value = Source;
                    HtmlControl ICON = (HtmlControl)e.Row.FindControl("ICONCLASS");
                    Label lblicon = (Label)e.Row.FindControl("lblicon");

                    if (Source == "IWRS")
                    {
                        ICON.Attributes.Add("class", "fas fa-dice");
                        ICON.Visible = true;
                        lblicon.ToolTip = "IWRS";
                    }
                    else if (Source == "Resource")
                    {
                        ICON.Attributes.Add("class", "fas fa-toolbox");
                        ICON.Visible = true;
                        lblicon.ToolTip = "Resource";
                    }
                    else if (Source == "EMR")
                    {
                        ICON.Attributes.Add("class", "fas fa-book");
                        ICON.Visible = true;
                        lblicon.ToolTip = "EMR";
                    }
                    else if (Source == "Paper CRF" || Source == "eCRF")
                    {
                        ICON.Attributes.Add("class", "fas fa-coins");
                        ICON.Visible = true;
                        lblicon.ToolTip = "eCRF";
                    }
                    else if (Source == "ePRO")
                    {
                        ICON.Attributes.Add("class", "fas fa-chalkboard-teacher");
                        ICON.Visible = true;
                        lblicon.ToolTip = "ePRO";
                    }

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
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

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
                            btnEdit.Attributes.Add("style", "width: 300px;");
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

                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                btnEdit.Text = data;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (FIELDNAME.ToUpperInvariant() == "Lab ID".ToUpperInvariant() || FIELDNAME.ToUpperInvariant() == "Laboratory ID".ToUpperInvariant())
                        {
                            DataSet ds;
                            ds = new DataSet();
                            ds = dal.LAB_MASTER_SP(Action: "GET_Lab", INVID: Request.QueryString["INVID"].ToString());
                            btnEdit.DataSource = ds;
                            btnEdit.DataTextField = "Lab_Name";
                            btnEdit.DataValueField = "Lab_ID";
                            btnEdit.DataBind();
                            btnEdit.Items.Insert(0, new ListItem("--Select--", "0"));
                            if (Session["DM_Multi_Lab_ID"] != null)
                            {
                                btnEdit.SelectedValue = Session["DM_Multi_Lab_ID"].ToString();
                            }

                            btnEdit.Attributes.Add("onchange", "GetDefaultData(this)");
                        }
                        else
                        {
                            DataSet ds;
                            ds = new DataSet();


                            if (NONREPETATIVE == "True")
                            {
                                if (hfDM_RECID.Value != "-1")
                                {
                                    ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                                }
                                else
                                {
                                    ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                                }
                            }
                            else
                            {
                                ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
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

                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                btnEdit.SelectedValue = data;
                            }
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
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


                        if (NONREPETATIVE == "True")
                        {
                            if (hfDM_RECID.Value != "-1")
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                            else
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                        }
                        else
                        {
                            ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();


                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.Contains(data.Replace(" ", String.Empty)))
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                    }
                                }
                            }
                        }

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


                        if (NONREPETATIVE == "True")
                        {
                            if (hfDM_RECID.Value != "-1")
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                            else
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                        }
                        else
                        {
                            ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();


                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.Contains(data.Replace(" ", String.Empty)))
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                    }
                                }
                            }
                        }

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
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    HtmlImage img = (HtmlImage)e.Row.FindControl("GMQ");
                    img.ID = "GMQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("AQ");
                    img.ID = "AQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("MQ");
                    img.ID = "MQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("CM");
                    img.ID = "CM_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("AD");
                    img.ID = "AD_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);


                    GridView grd_Data3 = e.Row.FindControl("grd_Data3") as GridView;
                    if (Session["DM_Multi_Lab_ID"] != null && LabData == "True")
                    {
                        if (Request.QueryString["MODULEID"] != null)
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), ID: ID, INDICATION: Request.QueryString["Indication"].ToString(), FIELDNAME: FIELDNAME, SITEID: Request.QueryString["INVID"].ToString(), SUBJID: Request.QueryString["SUBJID"].ToString(), LabID: Session["DM_Multi_Lab_ID"].ToString(),
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data3.DataSource = ds1.Tables[0];
                            grd_Data3.DataBind();
                        }
                        else
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), ID: ID, INDICATION: Request.QueryString["Indication"].ToString(), FIELDNAME: FIELDNAME, SITEID: Request.QueryString["INVID"].ToString(), SUBJID: Request.QueryString["SUBJID"].ToString(), LabID: Session["DM_Multi_Lab_ID"].ToString(),
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data3.DataSource = ds1.Tables[0];
                            grd_Data3.DataBind();
                        }
                    }
                    else
                    {
                        if (Request.QueryString["MODULEID"] != null)
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), INDICATION: Request.QueryString["Indication"].ToString(), ID: ID,
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data3.DataSource = ds1.Tables[0];
                            grd_Data3.DataBind();
                        }
                        else
                        {
                            DataSet ds1 = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE_CHILD", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), INDICATION: Request.QueryString["Indication"].ToString(), ID: ID,
                                PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                            grd_Data3.DataSource = ds1.Tables[0];
                            grd_Data3.DataBind();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void DSMH3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

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
                    string DEFULTVAL = dr["DEFAULTVAL1"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();
                    string LabData = dr["LabData"].ToString();
                    string Reference = dr["Refer"].ToString();
                    string AutoNum = dr["AutoNum"].ToString();
                    string Source = dr["Source"].ToString();

                    string STANDARD = dr["STANDARD"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();

                    string MANDATORY = dr["MANDATORY"].ToString();

                    hdnSourceValue.Value = Source;
                    HtmlControl ICON = (HtmlControl)e.Row.FindControl("ICONCLASS");
                    Label lblicon = (Label)e.Row.FindControl("lblicon");

                    if (Source == "IWRS")
                    {
                        ICON.Attributes.Add("class", "fas fa-dice");
                        ICON.Visible = true;
                        lblicon.ToolTip = "IWRS";
                    }
                    else if (Source == "Resource")
                    {
                        ICON.Attributes.Add("class", "fas fa-toolbox");
                        ICON.Visible = true;
                        lblicon.ToolTip = "Resource";
                    }
                    else if (Source == "EMR")
                    {
                        ICON.Attributes.Add("class", "fas fa-book");
                        ICON.Visible = true;
                        lblicon.ToolTip = "EMR";
                    }
                    else if (Source == "Paper CRF" || Source == "eCRF")
                    {
                        ICON.Attributes.Add("class", "fas fa-coins");
                        ICON.Visible = true;
                        lblicon.ToolTip = "eCRF";
                    }
                    else if (Source == "ePRO")
                    {
                        ICON.Attributes.Add("class", "fas fa-chalkboard-teacher");
                        ICON.Visible = true;
                        lblicon.ToolTip = "ePRO";
                    }

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
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

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
                            btnEdit.Attributes.Add("style", "width: 300px;");
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

                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                btnEdit.Text = data;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (FIELDNAME.ToUpperInvariant() == "Lab ID".ToUpperInvariant() || FIELDNAME.ToUpperInvariant() == "Laboratory ID".ToUpperInvariant())
                        {
                            DataSet ds;
                            ds = new DataSet();
                            ds = dal.LAB_MASTER_SP(Action: "GET_Lab", INVID: Request.QueryString["INVID"].ToString());
                            btnEdit.DataSource = ds;
                            btnEdit.DataTextField = "Lab_Name";
                            btnEdit.DataValueField = "Lab_ID";
                            btnEdit.DataBind();
                            btnEdit.Items.Insert(0, new ListItem("--Select--", "0"));
                            if (Session["DM_Multi_Lab_ID"] != null)
                            {
                                btnEdit.SelectedValue = Session["DM_Multi_Lab_ID"].ToString();
                            }

                            btnEdit.Attributes.Add("onchange", "GetDefaultData(this)");
                        }
                        else
                        {
                            DataSet ds;
                            ds = new DataSet();


                            if (NONREPETATIVE == "True")
                            {
                                if (hfDM_RECID.Value != "-1")
                                {
                                    ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                                }
                                else
                                {
                                    ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                                }
                            }
                            else
                            {
                                ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
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

                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                btnEdit.SelectedValue = data;
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
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


                        if (NONREPETATIVE == "True")
                        {
                            if (hfDM_RECID.Value != "-1")
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                            else
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                        }
                        else
                        {
                            ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();


                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                                {
                                    if (((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Text.Contains(data.Replace(" ", String.Empty)))
                                    {
                                        ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                    }
                                }
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " Mandatory"; ;
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


                        if (NONREPETATIVE == "True")
                        {
                            if (hfDM_RECID.Value != "-1")
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_With", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, RECID: hfDM_RECID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                            else
                            {
                                ds = dal.GetSet_DM_ProjectData(Action: "GET_NONREPEAT_OPTIONS_WithOut", VARIABLENAME: VARIABLENAME, TABLENAME: hfTablename.Value, PVID: hfDM_PVID.Value, VISITNUM: hfDM_VISITNUM.Value);
                            }
                        }
                        else
                        {
                            ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();


                        if (STANDARD == "True")
                        {
                            string data = GET_STANDARD_DATA(VARIABLENAME);
                            if (data != "")
                            {
                                for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                {
                                    if (((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Text.Contains(data.Replace(" ", String.Empty)))
                                    {
                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                                    }
                                }
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " Mandatory"; ;
                            }
                        }
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    HtmlImage img = (HtmlImage)e.Row.FindControl("GMQ");
                    img.ID = "GMQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("AQ");
                    img.ID = "AQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("MQ");
                    img.ID = "MQ_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("CM");
                    img.ID = "CM_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("AD");
                    img.ID = "AD_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                }
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
                hfDM_PAGESTATUS.Value = "1";

                InsertUpdatedata(grd_Data);

                hfDM_RECID.Value = "-1";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetRecords(GridView grd)
        {
            try
            {
                string strcmd, COLNAME, COLVAL, TABLENAME;
                int rownum = 0;
                strcmd = "";
                TABLENAME = grd.ID;
                string PVID = lblPVID.Text;


                DAL dal;
                dal = new DAL();

                DataSet dsDATA = new DataSet();

                if (Request.QueryString["MODULEID"].ToString() == "30")
                {
                    dsDATA = dal.ePRO_WAD_SP(
                          ACTION: "GET_DATA_FOR_SYS_PUSH",
                          SUBJID: Request.QueryString["SUBJID"].ToString(),
                          LASTDOSE: Request.QueryString["LASTDOSE"].ToString(),
                          TERM: Request.QueryString["TERM"].ToString(),
                          IC1: Request.QueryString["DATE"].ToString()
                          );
                }
                else if (Request.QueryString["MODULEID"].ToString() == "29")
                {
                    dsDATA = dal.ePRO_WAD_SP(
                              ACTION: "GET_DATA_FOR_LOC_PUSH",
                              SUBJID: Request.QueryString["SUBJID"].ToString(),
                              LASTDOSE: Request.QueryString["LASTDOSE"].ToString(),
                              TERM: Request.QueryString["TERM"].ToString(),
                              IC1: Request.QueryString["DATE"].ToString()
                              );
                }
                else if (Request.QueryString["MODULEID"].ToString() == "78")
                {
                    dsDATA = dal.ePRO_WAD_SP(
                              ACTION: "GET_DATA_FOR_AESI_PUSH",
                              SUBJID: Request.QueryString["SUBJID"].ToString(),
                              LASTDOSE: Request.QueryString["LASTDOSE"].ToString(),
                              TERM: Request.QueryString["TERM"].ToString(),
                              IC1: Request.QueryString["DATE"].ToString()
                              );
                }

                DataTable dt = GenerateTransposedTable(dsDATA.Tables[0]);
                DataSet ds = new DataSet();

                ds.Tables.Add(dt);

                if (ds.Tables[0].Rows.Count > 0)
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
                                ((HiddenField)grd.Rows[rownum].FindControl("hdnGrid")).Value = COLVAL;

                                if (CONTROLTYPE == "TEXTBOX")
                                {
                                    if (COLVAL.Contains("dd/") || COLVAL.Contains("mm/") || COLVAL.Contains("/yyyy"))
                                    {
                                        ((HiddenField)grd.Rows[rownum].FindControl("HDN_FIELD")).Value = COLVAL;
                                    }
                                    else
                                    {
                                        if (COLVAL != "")
                                        {
                                            ((TextBox)grd.Rows[rownum].FindControl("TXT_FIELD")).Text = COLVAL;
                                        }
                                    }

                                    if (REQUIREDYN == "True")
                                    {
                                        //REQUIRED TRUE Or FALSE
                                        if (COLVAL == "")
                                        {
                                            string Class = ((TextBox)grd.Rows[rownum].FindControl("TXT_FIELD")).CssClass;
                                            Class = Class + " bkColorYellow";
                                            ((TextBox)grd.Rows[rownum].FindControl("TXT_FIELD")).CssClass = Class;
                                        }
                                    }


                                }
                                else if (CONTROLTYPE == "DROPDOWN")
                                {
                                    COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                    ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).SelectedValue = COLVAL;

                                    if (REQUIREDYN == "True")
                                    {
                                        //REQUIRED TRUE Or FALSE
                                        if (COLVAL == "")
                                        {
                                            string Class = ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).CssClass;
                                            Class = Class + " bkColorYellow";
                                            ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).CssClass = Class;
                                        }
                                    }

                                }
                                else if (CONTROLTYPE == "CHECKBOX")
                                {
                                    string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split(',');
                                    foreach (string x in stringArray)
                                    {
                                        Repeater repeat_RAD = grd.Rows[rownum].FindControl("repeat_CHK") as Repeater;
                                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                        {
                                            if (x != "")
                                            {
                                                if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToString().Trim() == x.Trim())
                                                {
                                                    ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (CONTROLTYPE == "RADIOBUTTON")
                                {
                                    string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split(',');
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
                                }
                            }

                            ((TextBox)grd.Rows[rownum].FindControl("ContID")).Text = rownum.ToString();

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
                                    ((HiddenField)grd_Data1.Rows[a].FindControl("hdnGrid1")).Value = COLVAL;

                                    if (CONTROLTYPE == "TEXTBOX")
                                    {
                                        COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();

                                        if (COLVAL.Contains("dd/") || COLVAL.Contains("mm/") || COLVAL.Contains("/yyyy"))
                                        {
                                            ((HiddenField)grd_Data1.Rows[a].FindControl("HDN_FIELD")).Value = COLVAL;
                                        }
                                        else
                                        {
                                            if (COLVAL != "")
                                            {
                                                ((TextBox)grd_Data1.Rows[a].FindControl("TXT_FIELD")).Text = COLVAL;
                                            }
                                        }

                                        if (REQUIREDYN == "True")
                                        {
                                            //REQUIRED TRUE Or FALSE
                                            if (COLVAL == "")
                                            {
                                                string Class = ((TextBox)grd_Data1.Rows[a].FindControl("TXT_FIELD")).CssClass;
                                                Class = Class + " bkColorYellow";
                                                ((TextBox)grd_Data1.Rows[a].FindControl("TXT_FIELD")).CssClass = Class;
                                            }
                                        }


                                    }
                                    else if (CONTROLTYPE == "DROPDOWN")
                                    {
                                        COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                        ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).SelectedValue = COLVAL;

                                        if (REQUIREDYN == "True")
                                        {
                                            //REQUIRED TRUE Or FALSE
                                            if (COLVAL == "")
                                            {
                                                string Class = ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).CssClass;
                                                Class = Class + " bkColorYellow";
                                                ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).CssClass = Class;
                                            }
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
                                                    if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToString().Trim() == x.Trim())
                                                    {
                                                        ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                    }
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
                                                    else
                                                    {
                                                        ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                ((TextBox)grd_Data1.Rows[a].FindControl("ContID")).Text = a.ToString();

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
                                        ((HiddenField)grd_Data2.Rows[b].FindControl("hdnGrid2")).Value = COLVAL;

                                        if (CONTROLTYPE == "TEXTBOX")
                                        {
                                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                            if (COLVAL.Contains("dd/") || COLVAL.Contains("mm/") || COLVAL.Contains("/yyyy"))
                                            {
                                                ((HiddenField)grd_Data2.Rows[b].FindControl("HDN_FIELD")).Value = COLVAL;
                                            }
                                            else
                                            {
                                                if (COLVAL != "")
                                                {
                                                    ((TextBox)grd_Data2.Rows[b].FindControl("TXT_FIELD")).Text = COLVAL;
                                                }
                                            }

                                            if (REQUIREDYN == "True")
                                            {
                                                //REQUIRED TRUE Or FALSE
                                                if (COLVAL == "")
                                                {
                                                    string Class = ((TextBox)grd_Data2.Rows[b].FindControl("TXT_FIELD")).CssClass;
                                                    Class = Class + " bkColorYellow";
                                                    ((TextBox)grd_Data2.Rows[b].FindControl("TXT_FIELD")).CssClass = Class;
                                                }
                                            }


                                        }
                                        else if (CONTROLTYPE == "DROPDOWN")
                                        {
                                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                            ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).SelectedValue = COLVAL;

                                            if (REQUIREDYN == "True")
                                            {
                                                //REQUIRED TRUE Or FALSE
                                                if (COLVAL == "")
                                                {
                                                    string Class = ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).CssClass;
                                                    Class = Class + " bkColorYellow";
                                                    ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).CssClass = Class;
                                                }
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
                                                        if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToString().Trim() == x.Trim())
                                                        {
                                                            ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                        }
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
                                                        else
                                                        {
                                                            ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    ((TextBox)grd_Data2.Rows[b].FindControl("ContID")).Text = b.ToString();

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
                                            ((HiddenField)grd_Data3.Rows[c].FindControl("hdnGrid3")).Value = COLVAL;

                                            if (CONTROLTYPE == "TEXTBOX")
                                            {
                                                COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                                if (COLVAL.Contains("dd/") || COLVAL.Contains("mm/") || COLVAL.Contains("/yyyy"))
                                                {
                                                    ((HiddenField)grd_Data3.Rows[c].FindControl("HDN_FIELD")).Value = COLVAL;
                                                }
                                                else
                                                {
                                                    if (COLVAL != "")
                                                    {
                                                        ((TextBox)grd_Data3.Rows[c].FindControl("TXT_FIELD")).Text = COLVAL;
                                                    }
                                                }

                                                if (REQUIREDYN == "True")
                                                {
                                                    //REQUIRED TRUE Or FALSE
                                                    if (COLVAL == "")
                                                    {
                                                        string Class = ((TextBox)grd_Data3.Rows[c].FindControl("TXT_FIELD")).CssClass;
                                                        Class = Class + " bkColorYellow";
                                                        ((TextBox)grd_Data3.Rows[c].FindControl("TXT_FIELD")).CssClass = Class;
                                                    }
                                                }


                                            }
                                            else if (CONTROLTYPE == "DROPDOWN")
                                            {
                                                COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                                ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).SelectedValue = COLVAL;

                                                if (REQUIREDYN == "True")
                                                {
                                                    //REQUIRED TRUE Or FALSE
                                                    if (COLVAL == "")
                                                    {
                                                        string Class = ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).CssClass;
                                                        Class = Class + " bkColorYellow";
                                                        ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).CssClass = Class;
                                                    }
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
                                                            if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToString().Trim() == x.Trim())
                                                            {
                                                                ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                            }
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
                                            }
                                        }

                                        ((TextBox)grd_Data3.Rows[c].FindControl("ContID")).Text = c.ToString();
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

        string RECID;

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

        protected void InsertUpdatedata(GridView grd)
        {
            try
            {
                DataSet ds;
                string MODName = "";

                bool isColAdded = false;

                string COLUMN = "";
                string DATA = "";

                string INSERTQUERY = "";
                string UPDATEQUERY = "";

                if (Request.QueryString["MODULENAME"] != null)
                {
                    MODName = Request.QueryString["MODULENAME"].ToString();

                    DataSet dsMOD = dal.DM_ADD_UPDATE(ACTION: "GET_MODULENAME_BYID", ID: Request.QueryString["MODULEID"].ToString());

                    if (dsMOD.Tables[0].Rows[0]["ALLVISYN"].ToString() == "True")
                    {
                        ds = dal.GetSet_DM_ProjectData(
                         Action: "MAX_REC_ID_ALL",
                         PVID: lblPVID.Text,
                         MODULENAME: MODName,
                         TABLENAME: hfTablename.Value,
                         SUBJID: Request.QueryString["SUBJID"].ToString(),
                         PROJECTID: Session["PROJECTID"].ToString()
                         );
                    }
                    else
                    {
                        ds = dal.GetSet_DM_ProjectData(
                             Action: "MAX_REC_ID",
                             PVID: lblPVID.Text,
                             MODULENAME: MODName,
                         TABLENAME: hfTablename.Value,
                             SUBJID: Request.QueryString["SUBJID"].ToString(),
                             PROJECTID: Session["PROJECTID"].ToString()
                             );
                    }
                }
                else
                {
                    MODName = Request.QueryString["MODULENAME"].ToString();
                    DataSet dsMOD = dal.DM_ADD_UPDATE(ACTION: "GET_MODULENAME_BYID", ID: Request.QueryString["MODULEID"].ToString());

                    if (dsMOD.Tables[0].Rows[0]["ALLVISYN"].ToString() == "True")
                    {
                        ds = dal.GetSet_DM_ProjectData(
                         Action: "MAX_REC_ID_ALL",
                         PVID: lblPVID.Text,
                         MODULENAME: MODName,
                         SUBJID: Request.QueryString["SUBJID"].ToString(),
                         PROJECTID: Session["PROJECTID"].ToString(),
                         TABLENAME: hfTablename.Value
                         );
                    }
                    else
                    {
                        ds = dal.GetSet_DM_ProjectData(
                             Action: "MAX_REC_ID",
                             PVID: lblPVID.Text,
                             MODULENAME: MODName,
                             SUBJID: Request.QueryString["SUBJID"].ToString(),
                             PROJECTID: Session["PROJECTID"].ToString(),
                         TABLENAME: hfTablename.Value
                             );
                    }
                }

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        RECID = (Convert.ToInt32(ds.Tables[0].Rows[0]["RECID"]) + 1).ToString();
                    }
                    else
                    {
                        RECID = "0";
                    }
                }
                else
                {
                    RECID = "0";
                }


                if (hfDM_RECID.Value != "-1")
                {
                    RECID = hfDM_RECID.Value;
                }

                hfDM_RECID.Value = RECID;


                for (int rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
                {
                    dal = new DAL();
                    string strdata = "";
                    string varname = "";

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
                            if (((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
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

                        if (COLUMN != "")
                        {
                            COLUMN = COLUMN + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                        }
                        else
                        {
                            COLUMN = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                        }

                        if (DATA != "")
                        {
                            DATA = DATA + " @ni$h N'" + strdata + "'";
                        }
                        else
                        {
                            DATA = "N'" + strdata + "'";
                        }
                    }

                    string hdnGrid = ((HiddenField)grd_Data.Rows[rownum].FindControl("hdnGrid")).Value;

                    if (strdata != "")
                    {
                        if (hdnGrid != strdata)
                        {
                            dal.ePRO_WAD_SP(ACTION: "INSERT_AUDIT_DATA",
                            SUBJID: Request.QueryString["SUBJID"].ToString(),
                            RECID: hfDM_RECID.Value,
                            PVID: hfDM_PVID.Value,
                            MODULENAME: Request.QueryString["MODULENAME"].ToString(),
                            FIELDNAME: ((Label)grd_Data.Rows[rownum].FindControl("lblFieldName")).Text,
                            VARIABLENAME: ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text,
                            TABLENAME: hfTablename.Value,
                            OLDVALUE: hdnGrid,
                            NEWVALUE: strdata,
                            USERID: Session["User_Id"].ToString(),
                            LASTDOSE: Request.QueryString["LASTDOSE"].ToString()
                            );
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

                        if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                        {
                            if (CONTROLTYPE == "TEXTBOX")
                            {
                                strdata1 = ((TextBox)grd_Data1.Rows[b].FindControl("TXT_FIELD")).Text;
                            }
                            else if (CONTROLTYPE == "DROPDOWN")
                            {
                                if (((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
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
                                            DATA = "N'" + strdata1 + "'";
                                        }
                                        else
                                        {
                                            DATA = "NULL";

                                            strdata1 = "";
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

                                    strdata1 = "";
                                }
                                else
                                {
                                    DATA = "NULL";

                                    strdata1 = "";
                                }
                            }

                        }

                        string hdnGrid1 = ((HiddenField)grd_Data1.Rows[b].FindControl("hdnGrid1")).Value;

                        if (strdata1 != "")
                        {
                            if (hdnGrid1 != strdata1)
                            {
                                dal.ePRO_WAD_SP(ACTION: "INSERT_AUDIT_DATA",
                                SUBJID: Request.QueryString["SUBJID"].ToString(),
                                RECID: hfDM_RECID.Value,
                                PVID: hfDM_PVID.Value,
                                MODULENAME: Request.QueryString["MODULENAME"].ToString(),
                                FIELDNAME: ((Label)grd_Data1.Rows[b].FindControl("lblFieldName")).Text,
                                VARIABLENAME: ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text,
                                TABLENAME: hfTablename.Value,
                                OLDVALUE: hdnGrid1,
                                NEWVALUE: strdata1,
                                USERID: Session["User_Id"].ToString(),
                                LASTDOSE: Request.QueryString["LASTDOSE"].ToString()
                                );
                            }
                        }

                        GridView grd_Data2 = grd_Data1.Rows[b].FindControl("grd_Data2") as GridView;

                        for (int c = 0; c < grd_Data2.Rows.Count; c++)
                        {
                            string Val_Child1 = ((Label)grd_Data2.Rows[c].FindControl("lblVal_Child")).Text;
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
                                    if (((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
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

                                isColAdded = false;

                                foreach (string val in strdata1.Split('¸'))
                                {
                                    if (checkStringContains(Val_Child1, val) || (Val_Child1 == "Is Not Blank" && strdata1 != "") || Val_Child1 == "Compare")
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
                                                DATA = "N'" + strdata2 + "'";
                                            }
                                            else
                                            {
                                                DATA = "NULL";

                                                strdata2 = "";
                                            }
                                        }
                                    }
                                }

                                if (!isColAdded)
                                {
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

                            string hdnGrid2 = ((HiddenField)grd_Data2.Rows[c].FindControl("hdnGrid2")).Value;

                            if (strdata2 != "")
                            {
                                if (hdnGrid2 != strdata2)
                                {
                                    dal.ePRO_WAD_SP(ACTION: "INSERT_AUDIT_DATA",
                                    SUBJID: Request.QueryString["SUBJID"].ToString(),
                                    RECID: hfDM_RECID.Value,
                                    PVID: hfDM_PVID.Value,
                                    MODULENAME: Request.QueryString["MODULENAME"].ToString(),
                                    FIELDNAME: ((Label)grd_Data2.Rows[c].FindControl("lblFieldName")).Text,
                                    VARIABLENAME: ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text,
                                    TABLENAME: hfTablename.Value,
                                    OLDVALUE: hdnGrid2,
                                    NEWVALUE: strdata2,
                                    USERID: Session["User_Id"].ToString(),
                                    LASTDOSE: Request.QueryString["LASTDOSE"].ToString()
                                    );
                                }
                            }

                            GridView grd_Data3 = grd_Data2.Rows[c].FindControl("grd_Data3") as GridView;

                            for (int d = 0; d < grd_Data3.Rows.Count; d++)
                            {
                                string Val_Child2 = ((Label)grd_Data3.Rows[d].FindControl("lblVal_Child")).Text;
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
                                        if (((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedItem.Text != "--Select--")
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

                                    isColAdded = false;

                                    foreach (string val in strdata2.Split('¸'))
                                    {
                                        if (checkStringContains(Val_Child2, val) || (Val_Child2 == "Is Not Blank" && strdata2 != "") || Val_Child2 == "Compare")
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
                                                    DATA = "N'" + strdata3 + "'";
                                                }
                                                else
                                                {
                                                    DATA = "NULL";

                                                    strdata3 = "";
                                                }
                                            }
                                        }
                                    }

                                    if (!isColAdded)
                                    {
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

                                    string hdnGrid3 = ((HiddenField)grd_Data3.Rows[d].FindControl("hdnGrid3")).Value;

                                    if (strdata3 != "")
                                    {
                                        if (hdnGrid3 != strdata3)
                                        {
                                            dal.ePRO_WAD_SP(ACTION: "INSERT_AUDIT_DATA",
                                            SUBJID: Request.QueryString["SUBJID"].ToString(),
                                            RECID: hfDM_RECID.Value,
                                            PVID: hfDM_PVID.Value,
                                            MODULENAME: Request.QueryString["MODULENAME"].ToString(),
                                            FIELDNAME: ((Label)grd_Data3.Rows[d].FindControl("lblFieldName")).Text,
                                            VARIABLENAME: ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text,
                                            TABLENAME: hfTablename.Value,
                                            OLDVALUE: hdnGrid3,
                                            NEWVALUE: strdata3,
                                            USERID: Session["User_Id"].ToString(),
                                            LASTDOSE: Request.QueryString["LASTDOSE"].ToString()
                                            );
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

                string[] colArr = COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                string[] dataArr = DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);

                INSERTQUERY = "INSERT INTO [" + hfTablename.Value + "] ([PVID], [RECID], [SUBJID_DATA], [VISITNUM], [VISITCOUNT], [ENTEREDBY], [ENTEREDDAT], [IsComplete], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + lblPVID.Text + "', '" + hfDM_RECID.Value + "', '" + Request.QueryString["SUBJID"].ToString() + "', '" + Request.QueryString["VISITID"].ToString() + "', '" + Request.QueryString["VISITCOUNT"].ToString() + "', '" + Session["USER_ID"].ToString() + "', GETDATE(), 1, " + DATA.Replace("@ni$h", ",") + " )";

                for (int i = 0; i < colArr.Length; i++)
                {
                    if (UPDATEQUERY == "")
                    {
                        UPDATEQUERY = "UPDATE [" + hfTablename.Value + "] SET UPDATEDDAT = GETDATE(), [IsComplete] = 1, UPDATEDBY = '" + Session["USER_ID"].ToString() + "' ";
                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[i] + " = " + dataArr[i] + " ";
                    }
                    else
                    {
                        UPDATEQUERY = UPDATEQUERY + ", " + colArr[i] + " = " + dataArr[i] + " ";
                    }

                }

                UPDATEQUERY = UPDATEQUERY + " WHERE PVID = '" + lblPVID.Text + "' AND RECID = '" + hfDM_RECID.Value + "' AND SUBJID_DATA = '" + Request.QueryString["SUBJID"] + "' ";

                dal.GetSet_DM_ProjectData(
                 Action: "INSERT_MODULE_DATA",
                 TABLENAME: hfTablename.Value,
                 PVID: lblPVID.Text,
                 RECID: hfDM_RECID.Value,
                 VISITNUM: Request.QueryString["VISITID"].ToString(),
                 SUBJID: Request.QueryString["SUBJID"].ToString(),
                 INSERTQUERY: INSERTQUERY,
                 UPDATEQUERY: UPDATEQUERY
                 );


                dal.DM_PV_SP(
                PVID: lblPVID.Text,
                INVID: Request.QueryString["INVID"].ToString(),
                SUBJID: Request.QueryString["SUBJID"].ToString(),
                PAGENUM: Request.QueryString["MODULEID"].ToString(),
                VISITNUM: Request.QueryString["VISITID"].ToString(),
                PAGESTATUS: "1",
                ENTEREDBY: Session["USER_ID"].ToString(),
                VISITCOUNT: "1"
                );

                dal.ePRO_WAD_SP(
                ACTION: "INSERT_PUSH_RECORD",
                SUBJID: Request.QueryString["SUBJID"].ToString(),
                LASTDOSE: Request.QueryString["LASTDOSE"].ToString(),
                USERID: Session["USER_ID"].ToString(),
                LANG: Request.QueryString["TERM"].ToString(),
                IC1: Request.QueryString["DATE"].ToString()
                );

                string PAGENAME = Request.QueryString["PAGENAME"].ToString();

                Response.Redirect(PAGENAME);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutoCode()
        {
            try
            {
                dal.GetSet_DM_ProjectData(
                        Action: "AUTOCODE_Multiple",
                        PROJECTID: Session["PROJECTID"].ToString(),
                        PVID: lblPVID.Text,
                        RECID: RECID,
                        MODULEID: Request.QueryString["MODULEID"].ToString(),
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        ENTEREDBY: Session["USER_ID"].ToString()
                        );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetStructure()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                if (Session["DM_Multi_Lab_ID"] != null)
                {
                    if (Request.QueryString["MODULEID"] != null)
                    {
                        ds = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), INDICATION: Request.QueryString["Indication"].ToString(),
                            PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                    }
                    else
                    {
                        ds = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), INDICATION: Request.QueryString["Indication"].ToString(),
                            PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hfTablename.Value = ds.Tables[1].Rows[0]["TABLENAME"].ToString();
                        //GetDataExists();

                        grd_Data.DataSource = ds;
                        grd_Data.DataBind();

                        if (Request.QueryString["MODULEID"] != null)
                        {
                            grd_Data.Attributes.Add("ToolTip", Request.QueryString["MODULENAME"].ToString());
                        }
                        else
                        {
                            grd_Data.Attributes.Add("ToolTip", Request.QueryString["MODULENAME"].ToString());
                        }

                        bntSaveComplete.Visible = true;

                        if (Request.QueryString["MODULEID"] != null)
                        {
                            lblModuleName.Text = Request.QueryString["MODULENAME"].ToString();
                        }
                        else
                        {
                            lblModuleName.Text = Request.QueryString["MODULENAME"].ToString();
                        }
                    }

                    Session["DM_Multi_Lab_ID"] = null;
                }
                else
                {
                    if (Request.QueryString["MODULEID"] != null)
                    {
                        ds = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), INDICATION: Request.QueryString["Indication"].ToString(),
                            PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                    }
                    else
                    {
                        ds = dal.ePRO_WAD_SP(ACTION: "GET_STRUCTURE", VISITNUM: Request.QueryString["VISITID"].ToString(), MODULEID: Request.QueryString["MODULEID"].ToString(), INDICATION: Request.QueryString["Indication"].ToString(),
                            PVID: lblPVID.Text, RECID: hfDM_RECID.Value);
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hfTablename.Value = ds.Tables[1].Rows[0]["TABLENAME"].ToString();
                        //GetDataExists();

                        grd_Data.DataSource = ds;
                        grd_Data.DataBind();

                        if (Request.QueryString["MODULEID"] != null)
                        {
                            grd_Data.Attributes.Add("ToolTip", Request.QueryString["MODULENAME"].ToString());
                        }
                        else
                        {
                            grd_Data.Attributes.Add("ToolTip", Request.QueryString["MODULENAME"].ToString());
                        }

                        bntSaveComplete.Visible = true;

                        if (Request.QueryString["MODULEID"] != null)
                        {
                            lblModuleName.Text = Request.QueryString["MODULENAME"].ToString();
                        }
                        else
                        {
                            lblModuleName.Text = Request.QueryString["MODULENAME"].ToString();
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

        [System.Web.Services.WebMethod]
        public static void GetDefaultData(string LabID)
        {
            try
            {
                HttpContext.Current.Session["DM_Multi_Lab_ID"] = LabID;
            }
            catch (Exception ex)
            {
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

        public void GetAuditDetails()
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                ds = dal.ePRO_WAD_SP
                    (
                    ACTION: "GET_AUDIT_DATA",
                    PVID: hfDM_PVID.Value,
                    RECID: hfDM_RECID.Value
                    );
                if (ds.Tables[0].Rows.Count > 0)
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
    }
}