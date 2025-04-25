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
    public partial class DM_ANOTATED_CRF_VISIT_WISE : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GetStructure();
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        private void GetStructure()
        {
            try
            {
                DataSet ds = new DataSet();

                if (Request.QueryString["VISITID"] == null)
                {
                    ds = dal.DB_DATA_SP(ACTION: "GET_VISITS_ANNOTATED_CRF");
                }
                else
                {
                    ds = dal.DB_DATA_SP(ACTION: "GET_VISITS_ANNOTATED_CRF", VISITNUM: Request.QueryString["VISITID"].ToString());
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeat_AllModule.DataSource = ds.Tables[0];
                    repeat_AllModule.DataBind();
                }
                else
                {
                    repeat_AllModule.DataSource = null;
                    repeat_AllModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void repeat_AllModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    DataSet ds;
                    ds = new DataSet();
                    HDNMODULEID.Value = row["MODULEID"].ToString();
                    hdnVISITID.Value = row["VISITNUM"].ToString();
                    //Label lblModuleName = (Label)e.Item.FindControl("lblModuleName");
                    //lblModuleName.Text = Session["PROJECTIDTEXT"].ToString() + ": " + row["MODULENAME"].ToString();
                    GridView grd_Data = (GridView)e.Item.FindControl("grd_Data");

                    ds = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_ANNONATED",
                    PROJECTID: Session["PROJECTID"].ToString(),
                    MODULEID: HDNMODULEID.Value,
                    VISITNUM: row["VISITNUM"].ToString()
                    );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_Data.DataSource = ds.Tables[0];
                        grd_Data.DataBind();

                    }
                    else
                    {
                        grd_Data.DataSource = null;
                        grd_Data.DataBind();
                    }

                    GridView grd_Module_Crit = (GridView)e.Item.FindControl("grd_Module_Crit");
                    HtmlTableRow TRMODULECRIT = (HtmlTableRow)e.Item.FindControl("TRMODULECRIT");

                    DataSet ds1 = dal.DB_DATA_SP(ACTION: "GET_Conditional_Visibility_ANNOTATED",
                    MODULEID: HDNMODULEID.Value,
                    VISITNUM: row["VISITNUM"].ToString()
                    );

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        grd_Module_Crit.DataSource = ds1.Tables[0];
                        grd_Module_Crit.DataBind();
                        TRMODULECRIT.Visible = true;
                    }
                    else
                    {
                        grd_Module_Crit.DataSource = null;
                        grd_Module_Crit.DataBind();
                        TRMODULECRIT.Visible = false;
                    }

                    GridView grd_OnSubmit_CRIT = (GridView)e.Item.FindControl("grd_OnSubmit_CRIT");
                    HtmlTableRow TRONSUBMIT_CRT = (HtmlTableRow)e.Item.FindControl("TRONSUBMIT_CRT");

                    DataSet ds2 = dal.DB_DATA_SP(ACTION: "GET_ONSUBMIT_CRIT_ANNOTATED",
                    MODULEID: HDNMODULEID.Value,
                    VISITNUM: row["VISITNUM"].ToString()
                    );

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        grd_OnSubmit_CRIT.DataSource = ds2.Tables[0];
                        grd_OnSubmit_CRIT.DataBind();
                        TRONSUBMIT_CRT.Visible = true;
                    }
                    else
                    {
                        grd_OnSubmit_CRIT.DataSource = null;
                        grd_OnSubmit_CRIT.DataBind();
                        TRONSUBMIT_CRT.Visible = false;
                    }

                    GridView grd_OnChange_CRIT = (GridView)e.Item.FindControl("grd_OnChange_CRIT");
                    HtmlTableRow TRONCHANGE = (HtmlTableRow)e.Item.FindControl("TRONCHANGE");

                    DataSet ds3 = dal.DB_DATA_SP(ACTION: "GET_ONCHANGE_CRIT_ANNOTATDED",
                         MODULEID: HDNMODULEID.Value,
                         VISITNUM: row["VISITNUM"].ToString()
                        );

                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        grd_OnChange_CRIT.DataSource = ds3.Tables[0];
                        grd_OnChange_CRIT.DataBind();
                        TRONCHANGE.Visible = true;
                    }
                    else
                    {
                        grd_OnChange_CRIT.DataSource = null;
                        grd_OnChange_CRIT.DataBind();
                        TRONCHANGE.Visible = false;
                    }

                    GridView grd_Visit_CRIT = (GridView)e.Item.FindControl("grd_Visit_CRIT");
                    HtmlTableRow TRVISITCRIT = (HtmlTableRow)e.Item.FindControl("TRVISITCRIT");

                    DataSet ds4 = dal.DB_DATA_SP(ACTION: "GET_VISIT_VISIBILITY_CRIT_ANNOTATDED",
                    MODULEID: HDNMODULEID.Value,
                    VISITNUM: row["VISITNUM"].ToString()
                    );

                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        grd_Visit_CRIT.DataSource = ds4.Tables[0];
                        grd_Visit_CRIT.DataBind();
                        TRVISITCRIT.Visible = true;
                    }
                    else
                    {
                        grd_Visit_CRIT.DataSource = null;
                        grd_Visit_CRIT.DataBind();
                        TRVISITCRIT.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void grd_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DAL dal;
                    dal = new DAL();
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    hdnid.Value = ID;
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
                    string MANDATORY = dr["MANDATORY"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string FORMAT = dr["FORMAT"].ToString();
                    Label lblMandatory = (Label)e.Row.FindControl("lblMandatory");
                    Label lblCRTDP = (Label)e.Row.FindControl("lblCRTDP");
                    Label lblReadOnly = (Label)e.Row.FindControl("lblReadOnly");
                    Label lblInvisible = (Label)e.Row.FindControl("lblInvisible");
                    string Prefix = dr["Prefix"].ToString();
                    string INVISIBLE = dr["INVISIBLE"].ToString();

                    if (MANDATORY == "True")
                    {
                        lblMandatory.Text = "*";
                        lblMandatory.Visible = true;
                    }

                    if (Critic_DP == "True")
                    {
                        lblCRTDP.Text = "✔";
                        lblCRTDP.Visible = true;
                    }

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

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
                    if (READYN == "True")
                    {
                        lblReadOnly.Text = "𝓡";
                        lblReadOnly.Visible = true;
                    }

                    if (INVISIBLE == "True")
                    {
                        lblInvisible.Visible = true;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 200px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            btnEdit.Text = "DD-MMM-YYYY";
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            btnEdit.Text = "HH:MM";
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            btnEdit.Text = "dd/mm/yyyy";
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "N" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "N";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "D" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "D";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += "-" + FORMAT;
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "A" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "A";
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

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER_ANNOTATED", VariableName: VARIABLENAME, VISITNUM: hdnVISITID.Value);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                        repeat_RAD.Visible = true;

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER_ANNOTATED", VariableName: VARIABLENAME, VISITNUM: hdnVISITID.Value);

                        DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");  //'UserName' is ColumnName
                        foreach (DataRow row in rows)
                            ds.Tables[0].Rows.Remove(row);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void repeat_RAD_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string Optionname = row["TEXT"].ToString();
                    Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                    Repeater repeat_RAD1 = (Repeater)e.Item.FindControl("repeat_RAD1");
                    repeat_RAD1.Visible = true;
                    DataSet ds1 = new DataSet();
                    DataSet ds = new DataSet();

                    lblSEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                    lblSEQNO.Visible = true;

                    ds1 = dal.DB_DATA_SP(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                        PROJECTID: Session["PROJECTID"].ToString(),
                        MODULEID: HDNMODULEID.Value,
                        ID: hdnid.Value,
                        DEFAULTVAL: Optionname,
                        VISITNUM: hdnVISITID.Value
                        );

                    hdnfieldname1.Value = "";
                    repeat_RAD1.DataSource = ds1;
                    repeat_RAD1.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void repeat_RAD1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    Repeater repeat_RAD2 = (Repeater)e.Item.FindControl("repeat_RAD2");
                    repeat_RAD2.Visible = true;
                    DataSet ds1 = new DataSet();
                    DataSet ds = new DataSet();
                    Label lblheader = (Label)e.Item.FindControl("lblheader");

                    string ID = row["FIELD_ID"].ToString();
                    hdnid1.Value = ID;
                    string CONTROLTYPE = row["CONTROLTYPE"].ToString();
                    string VARIABLENAME = row["VARIABLENAME"].ToString();
                    string FIELDNAME = row["FIELDNAME"].ToString();
                    string CLASS = row["CLASS"].ToString();

                    string MAXLEN = row["MAXLEN"].ToString();
                    string UPPERCASE = row["UPPERCASE"].ToString();
                    string DEFULTVAL = row["DEFAULTVAL"].ToString();
                    string BOLDYN = row["BOLDYN"].ToString();
                    string UNLNYN = row["UNLNYN"].ToString();
                    string READYN = row["READYN"].ToString();
                    string MULTILINEYN = row["MULTILINEYN"].ToString();
                    string MANDATORY = row["MANDATORY"].ToString();
                    string Critic_DP = row["Critic_DP"].ToString();
                    string FORMAT = row["FORMAT"].ToString();
                    Label lblMandatory1 = (Label)e.Item.FindControl("lblMandatory1");
                    Label lblCRTDP1 = (Label)e.Item.FindControl("lblCRTDP1");
                    Label lblSEQNO1 = (Label)e.Item.FindControl("lblSEQNO1");
                    Label lblheader1SEQNO = (Label)e.Item.FindControl("lblheader1SEQNO");
                    Label lblReadOnly = (Label)e.Item.FindControl("lblReadOnly");
                    Label lblInvisible = (Label)e.Item.FindControl("lblInvisible");
                    string Prefix = row["Prefix"].ToString();
                    string INVISIBLE = row["INVISIBLE"].ToString();

                    if (BOLDYN == "True")
                    {
                        lblheader.CssClass = lblheader.CssClass + " fontbold";
                    }
                    if (UNLNYN == "True")
                    {
                        lblheader.Font.Underline = true;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELD1");
                        btnEdit.Visible = true;
                        lblheader.Text = FIELDNAME;
                        lblheader1SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                        lblheader1SEQNO.Visible = true;

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 200px;height: 40px;margin-bottom: 4px;");
                        }

                        if (MANDATORY == "True")
                        {
                            lblMandatory1.Text = "*";
                            lblMandatory1.Visible = true;
                        }

                        if (Critic_DP == "True")
                        {
                            lblCRTDP1.Text = "✔";
                            lblCRTDP1.Visible = true;
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }

                        if (READYN == "True")
                        {
                            lblReadOnly.Text = "𝓡";
                            lblReadOnly.Visible = true;
                        }

                        if (INVISIBLE == "True")
                        {
                            lblInvisible.Visible = true;
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            btnEdit.Text = "DD-MMM-YYYY";
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            btnEdit.Text = "HH:MM";
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            btnEdit.Text = "dd/mm/yyyy";
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "N" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "N";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "D" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "D";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += "-" + FORMAT;
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "A" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "A";
                            }
                        }

                        ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid1.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                        DataRow[] rows;
                        rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                        foreach (DataRow row1 in rows)
                            ds1.Tables[0].Rows.Remove(row1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            hdnfieldname2.Value = "";
                            repeat_RAD2.DataSource = ds1;
                            repeat_RAD2.DataBind();
                        }
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            CheckBox CHK_FIELD1 = (CheckBox)e.Item.FindControl("CHK_FIELD1");
                            CHK_FIELD1.Text = row["TEXT"].ToString();
                            CHK_FIELD1.Visible = true;
                            lblSEQNO1.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNO1.Visible = true;

                            if (hdnfieldname1.Value != VARIABLENAME)
                            {
                                lblheader.Text = FIELDNAME;
                                hdnfieldname1.Value = VARIABLENAME;
                                lblheader1SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheader1SEQNO.Visible = true;

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }

                                if (MANDATORY == "True")
                                {
                                    lblMandatory1.Text = "*";
                                    lblMandatory1.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDP1.Text = "✔";
                                    lblCRTDP1.Visible = true;
                                }
                            }

                            ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID:
                            HDNMODULEID.Value,
                            ID: hdnid1.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                            DataRow[] rows;
                            rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                            foreach (DataRow row1 in rows)
                                ds1.Tables[0].Rows.Remove(row1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                hdnfieldname2.Value = "";
                                repeat_RAD2.DataSource = ds1;
                                repeat_RAD2.DataBind();
                            }
                        }
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            RadioButton RAD_FIELD1 = (RadioButton)e.Item.FindControl("RAD_FIELD1");
                            RAD_FIELD1.Text = row["TEXT"].ToString();
                            RAD_FIELD1.Visible = true;
                            lblSEQNO1.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNO1.Visible = true;

                            if (hdnfieldname1.Value != VARIABLENAME)
                            {
                                lblheader.Text = FIELDNAME;
                                hdnfieldname1.Value = VARIABLENAME;
                                lblheader1SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheader1SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatory1.Text = "*";
                                    lblMandatory1.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDP1.Text = "✔";
                                    lblCRTDP1.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }

                            ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid1.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                            DataRow[] rows;
                            rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                            foreach (DataRow row1 in rows)
                                ds1.Tables[0].Rows.Remove(row1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                hdnfieldname2.Value = "";
                                repeat_RAD2.DataSource = ds1;
                                repeat_RAD2.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void repeat_RAD2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    DataSet ds1 = new DataSet();
                    DataSet ds = new DataSet();
                    Label lblheader2 = (Label)e.Item.FindControl("lblheader2");
                    Repeater repeat_RAD3 = (Repeater)e.Item.FindControl("repeat_RAD3");
                    repeat_RAD3.Visible = true;
                    string ID = row["FIELD_ID"].ToString();
                    hdnid2.Value = ID;
                    string CONTROLTYPE = row["CONTROLTYPE"].ToString();
                    string VARIABLENAME = row["VARIABLENAME"].ToString();
                    string FIELDNAME = row["FIELDNAME"].ToString();
                    string CLASS = row["CLASS"].ToString();

                    string MAXLEN = row["MAXLEN"].ToString();
                    string UPPERCASE = row["UPPERCASE"].ToString();
                    string DEFULTVAL = row["DEFAULTVAL"].ToString();
                    string BOLDYN = row["BOLDYN"].ToString();
                    string UNLNYN = row["UNLNYN"].ToString();
                    string READYN = row["READYN"].ToString();
                    string MULTILINEYN = row["MULTILINEYN"].ToString();
                    string MANDATORY = row["MANDATORY"].ToString();
                    string Critic_DP = row["Critic_DP"].ToString();
                    string FORMAT = row["FORMAT"].ToString();
                    Label lblMandatory2 = (Label)e.Item.FindControl("lblMandatory2");
                    Label lblCRTDP2 = (Label)e.Item.FindControl("lblCRTDP2");
                    Label lblSEQNO2 = (Label)e.Item.FindControl("lblSEQNO2");
                    Label lblheader2SEQNO = (Label)e.Item.FindControl("lblheader2SEQNO");
                    Label lblReadOnly = (Label)e.Item.FindControl("lblReadOnly");
                    Label lblInvisible = (Label)e.Item.FindControl("lblInvisible");
                    string Prefix = row["Prefix"].ToString();
                    string INVISIBLE = row["INVISIBLE"].ToString();

                    if (BOLDYN == "True")
                    {
                        lblheader2.CssClass = lblheader2.CssClass + " fontbold";
                    }
                    if (UNLNYN == "True")
                    {
                        lblheader2.Font.Underline = true;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELD2");
                        btnEdit.Visible = true;
                        lblheader2.Text = FIELDNAME;
                        lblheader2SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                        lblheader2SEQNO.Visible = true;

                        if (MANDATORY == "True")
                        {
                            lblMandatory2.Text = "*";
                            lblMandatory2.Visible = true;
                        }

                        if (Critic_DP == "True")
                        {
                            lblCRTDP2.Text = "✔";
                            lblCRTDP2.Visible = true;
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 200px;height: 40px;margin-bottom: 4px;");
                        }

                        if (READYN == "True")
                        {
                            lblReadOnly.Text = "𝓡";
                            lblReadOnly.Visible = true;
                        }

                        if (INVISIBLE == "True")
                        {
                            lblInvisible.Visible = true;
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            btnEdit.Text = "DD-MMM-YYYY";
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            btnEdit.Text = "HH:MM";
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            btnEdit.Text = "dd/mm/yyyy";
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "N" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "N";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "D" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "D";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += "-" + FORMAT;
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "A" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "A";
                            }
                        }

                        ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid1.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                        DataRow[] rows;
                        rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                        foreach (DataRow row1 in rows)
                            ds1.Tables[0].Rows.Remove(row1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            hdnfieldname2.Value = "";
                            repeat_RAD3.DataSource = ds1;
                            repeat_RAD3.DataBind();
                        }
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            CheckBox CHK_FIELD2 = (CheckBox)e.Item.FindControl("CHK_FIELD2");
                            CHK_FIELD2.Text = row["TEXT"].ToString();
                            CHK_FIELD2.Visible = true;
                            lblSEQNO2.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNO2.Visible = true;

                            if (hdnfieldname2.Value != VARIABLENAME)
                            {
                                lblheader2.Text = FIELDNAME;
                                hdnfieldname2.Value = VARIABLENAME;
                                lblheader2SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheader2SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatory2.Text = "*";
                                    lblMandatory2.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDP2.Text = "✔";
                                    lblCRTDP2.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }

                            ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid2.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                            DataRow[] rows;
                            rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                            foreach (DataRow row1 in rows)
                                ds1.Tables[0].Rows.Remove(row1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                repeat_RAD3.DataSource = ds1;
                                repeat_RAD3.DataBind();
                            }
                        }
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        RadioButton RAD_FIELD2 = (RadioButton)e.Item.FindControl("RAD_FIELD2");
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            RAD_FIELD2.Text = row["TEXT"].ToString();
                            RAD_FIELD2.Visible = true;
                            lblSEQNO2.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNO2.Visible = true;

                            if (hdnfieldname2.Value != VARIABLENAME)
                            {
                                lblheader2.Text = FIELDNAME;
                                hdnfieldname2.Value = VARIABLENAME;
                                lblheader2SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheader2SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatory2.Text = "*";
                                    lblMandatory2.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDP2.Text = "✔";
                                    lblCRTDP2.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }

                            ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid2.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                            DataRow[] rows;
                            rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                            foreach (DataRow row1 in rows)
                                ds1.Tables[0].Rows.Remove(row1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                repeat_RAD3.DataSource = ds1;
                                repeat_RAD3.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void repeat_RAD3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    DataSet ds1 = new DataSet();
                    DataSet ds = new DataSet();
                    string ID = row["FIELD_ID"].ToString();
                    hdnid2.Value = ID;
                    string CONTROLTYPE = row["CONTROLTYPE"].ToString();
                    string VARIABLENAME = row["VARIABLENAME"].ToString();
                    string FIELDNAME = row["FIELDNAME"].ToString();
                    Label lblheader3 = (Label)e.Item.FindControl("lblheader3");
                    string CLASS = row["CLASS"].ToString();

                    string MAXLEN = row["MAXLEN"].ToString();
                    string UPPERCASE = row["UPPERCASE"].ToString();
                    string DEFULTVAL = row["DEFAULTVAL"].ToString();
                    string BOLDYN = row["BOLDYN"].ToString();
                    string UNLNYN = row["UNLNYN"].ToString();
                    string READYN = row["READYN"].ToString();
                    string MULTILINEYN = row["MULTILINEYN"].ToString();
                    string MANDATORY = row["MANDATORY"].ToString();
                    string Critic_DP = row["Critic_DP"].ToString();
                    string FORMAT = row["FORMAT"].ToString();
                    Label lblMandatory3 = (Label)e.Item.FindControl("lblMandatory3");
                    Label lblCRTDP3 = (Label)e.Item.FindControl("lblCRTDP3");
                    Label lblSEQNO3 = (Label)e.Item.FindControl("lblSEQNO3");
                    Label lblheader3SEQNO = (Label)e.Item.FindControl("lblheader3SEQNO");
                    Label lblReadOnly = (Label)e.Item.FindControl("lblReadOnly");
                    Label lblInvisible = (Label)e.Item.FindControl("lblInvisible");
                    string Prefix = row["Prefix"].ToString();
                    string INVISIBLE = row["INVISIBLE"].ToString();

                    if (BOLDYN == "True")
                    {
                        lblheader3.CssClass = lblheader3.CssClass + " fontbold";
                    }
                    if (UNLNYN == "True")
                    {
                        lblheader3.Font.Underline = true;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELD3");
                        btnEdit.Visible = true;
                        lblheader3.Text = FIELDNAME;
                        lblheader3SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                        lblheader3SEQNO.Visible = true;

                        if (MANDATORY == "True")
                        {
                            lblMandatory3.Text = "*";
                            lblMandatory3.Visible = true;
                        }

                        if (Critic_DP == "True")
                        {
                            lblCRTDP3.Text = "✔";
                            lblCRTDP3.Visible = true;
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 200px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }

                        if (READYN == "True")
                        {
                            lblReadOnly.Text = "𝓡";
                            lblReadOnly.Visible = true;
                        }

                        if (INVISIBLE == "True")
                        {
                            lblInvisible.Visible = true;
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            btnEdit.Text = "DD-MMM-YYYY";
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            btnEdit.Text = "HH:MM";
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            btnEdit.Text = "dd/mm/yyyy";
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "N" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "N";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "D" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "D";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += "-" + FORMAT;
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "A" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "A";
                            }
                        }
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            CheckBox CHK_FIELD3 = (CheckBox)e.Item.FindControl("CHK_FIELD3");
                            CHK_FIELD3.Text = row["TEXT"].ToString();
                            CHK_FIELD3.Visible = true;
                            lblSEQNO3.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNO3.Visible = true;

                            if (hdnfieldname3.Value != VARIABLENAME)
                            {
                                lblheader3.Text = FIELDNAME;
                                hdnfieldname3.Value = VARIABLENAME;
                                lblheader3SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheader3SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatory3.Text = "*";
                                    lblMandatory3.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDP3.Text = "✔";
                                    lblCRTDP3.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }
                        }
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            RadioButton RAD_FIELD3 = (RadioButton)e.Item.FindControl("RAD_FIELD3");
                            RAD_FIELD3.Text = row["TEXT"].ToString();
                            RAD_FIELD3.Visible = true;
                            lblSEQNO3.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNO3.Visible = true;

                            if (hdnfieldname3.Value != VARIABLENAME)
                            {
                                lblheader3.Text = FIELDNAME;
                                hdnfieldname3.Value = VARIABLENAME;
                                lblheader3SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheader3SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatory3.Text = "*";
                                    lblMandatory3.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDP3.Text = "✔";
                                    lblCRTDP3.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void repeat_CHK_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string Optionname = row["TEXT"].ToString();
                    Label lblSEQNOc = (Label)e.Item.FindControl("lblSEQNOc");

                    Repeater repeat_CHK1 = (Repeater)e.Item.FindControl("repeat_CHK1");
                    repeat_CHK1.Visible = true;
                    DataSet ds1 = new DataSet();
                    DataSet ds = new DataSet();

                    lblSEQNOc.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                    lblSEQNOc.Visible = true;

                    ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                        PROJECTID: Session["PROJECTID"].ToString(),
                        MODULEID: HDNMODULEID.Value,
                        ID: hdnid.Value,
                        DEFULTVAL: Optionname,
                        VISITNUM: hdnVISITID.Value
                        );

                    hdnfieldname1.Value = "";
                    repeat_CHK1.DataSource = ds1;
                    repeat_CHK1.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void repeat_CHK1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    Repeater repeat_CHK2 = (Repeater)e.Item.FindControl("repeat_CHK2");
                    repeat_CHK2.Visible = true;
                    DataSet ds1 = new DataSet();
                    DataSet ds = new DataSet();
                    Label lblheaderc1 = (Label)e.Item.FindControl("lblheaderc1");

                    string ID = row["FIELD_ID"].ToString();
                    hdnid1.Value = ID;
                    string CONTROLTYPE = row["CONTROLTYPE"].ToString();
                    string VARIABLENAME = row["VARIABLENAME"].ToString();
                    string FIELDNAME = row["FIELDNAME"].ToString();
                    string CLASS = row["CLASS"].ToString();

                    string MAXLEN = row["MAXLEN"].ToString();
                    string UPPERCASE = row["UPPERCASE"].ToString();
                    string DEFULTVAL = row["DEFAULTVAL"].ToString();
                    string BOLDYN = row["BOLDYN"].ToString();
                    string UNLNYN = row["UNLNYN"].ToString();
                    string READYN = row["READYN"].ToString();
                    string MULTILINEYN = row["MULTILINEYN"].ToString();
                    string MANDATORY = row["MANDATORY"].ToString();
                    string Critic_DP = row["Critic_DP"].ToString();
                    string FORMAT = row["FORMAT"].ToString();
                    Label lblMandatoryc1 = (Label)e.Item.FindControl("lblMandatoryc1");
                    Label lblCRTDPc1 = (Label)e.Item.FindControl("lblCRTDPc1");
                    Label lblSEQNOc1 = (Label)e.Item.FindControl("lblSEQNOc1");
                    Label lblheaderc1SEQNO = (Label)e.Item.FindControl("lblheaderc1SEQNO");
                    Label lblReadOnly = (Label)e.Item.FindControl("lblReadOnly");
                    Label lblInvisible = (Label)e.Item.FindControl("lblInvisible");
                    string Prefix = row["Prefix"].ToString();
                    string INVISIBLE = row["INVISIBLE"].ToString();

                    if (BOLDYN == "True")
                    {
                        lblheaderc1.CssClass = lblheaderc1.CssClass + " fontbold";
                    }
                    if (UNLNYN == "True")
                    {
                        lblheaderc1.Font.Underline = true;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELDc1");
                        btnEdit.Visible = true;
                        lblheaderc1.Text = FIELDNAME;
                        lblheaderc1SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                        lblheaderc1SEQNO.Visible = true;

                        if (MANDATORY == "True")
                        {
                            lblMandatoryc1.Text = "*";
                            lblMandatoryc1.Visible = true;
                        }

                        if (Critic_DP == "True")
                        {
                            lblCRTDPc1.Text = "✔";
                            lblCRTDPc1.Visible = true;
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 200px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }

                        if (READYN == "True")
                        {
                            lblReadOnly.Text = "𝓡";
                            lblReadOnly.Visible = true;
                        }

                        if (INVISIBLE == "True")
                        {
                            lblInvisible.Visible = true;
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            btnEdit.Text = "DD-MMM-YYYY";
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            btnEdit.Text = "HH:MM";
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            btnEdit.Text = "dd/mm/yyyy";
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "N" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "N";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "D" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "D";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += "-" + FORMAT;
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "A" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "A";
                            }
                        }

                        ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid1.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                        DataRow[] rows;
                        rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                        foreach (DataRow row1 in rows)
                            ds1.Tables[0].Rows.Remove(row1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            hdnfieldname2.Value = "";
                            repeat_CHK2.DataSource = ds1;
                            repeat_CHK2.DataBind();
                        }
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            CheckBox CHK_FIELD1 = (CheckBox)e.Item.FindControl("CHK_FIELDc1");
                            CHK_FIELD1.Text = row["TEXT"].ToString();
                            CHK_FIELD1.Visible = true;
                            lblSEQNOc1.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNOc1.Visible = true;
                            
                            if (hdnfieldname1.Value != VARIABLENAME)
                            {
                                lblheaderc1.Text = FIELDNAME;
                                hdnfieldname1.Value = VARIABLENAME;
                                lblheaderc1SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheaderc1SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatoryc1.Text = "*";
                                    lblMandatoryc1.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDPc1.Text = "✔";
                                    lblCRTDPc1.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }

                            ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid1.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                            DataRow[] rows;
                            rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                            foreach (DataRow row1 in rows)
                                ds1.Tables[0].Rows.Remove(row1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                hdnfieldname2.Value = "";
                                repeat_CHK2.DataSource = ds1;
                                repeat_CHK2.DataBind();
                            }
                        }
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            RadioButton RAD_FIELD1 = (RadioButton)e.Item.FindControl("RAD_FIELDc1");
                            RAD_FIELD1.Text = row["TEXT"].ToString();
                            RAD_FIELD1.Visible = true;
                            lblSEQNOc1.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNOc1.Visible = true;

                            if (hdnfieldname1.Value != VARIABLENAME)
                            {
                                lblheaderc1.Text = FIELDNAME;
                                hdnfieldname1.Value = VARIABLENAME;
                                lblheaderc1SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheaderc1SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatoryc1.Text = "*";
                                    lblMandatoryc1.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDPc1.Text = "✔";
                                    lblCRTDPc1.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }

                            ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid1.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                            DataRow[] rows;
                            rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                            foreach (DataRow row1 in rows)
                                ds1.Tables[0].Rows.Remove(row1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                hdnfieldname2.Value = "";
                                repeat_CHK2.DataSource = ds1;
                                repeat_CHK2.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void repeat_CHK2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    DataSet ds1 = new DataSet();
                    DataSet ds = new DataSet();
                    Label lblheader2 = (Label)e.Item.FindControl("lblheaderc2");
                    Repeater repeat_CHK3 = (Repeater)e.Item.FindControl("repeat_CHK3");
                    repeat_CHK3.Visible = true;
                    string ID = row["FIELD_ID"].ToString();
                    hdnid2.Value = ID;
                    string CONTROLTYPE = row["CONTROLTYPE"].ToString();
                    string VARIABLENAME = row["VARIABLENAME"].ToString();
                    string FIELDNAME = row["FIELDNAME"].ToString();
                    string CLASS = row["CLASS"].ToString();

                    string MAXLEN = row["MAXLEN"].ToString();
                    string UPPERCASE = row["UPPERCASE"].ToString();
                    string DEFULTVAL = row["DEFAULTVAL"].ToString();
                    string BOLDYN = row["BOLDYN"].ToString();
                    string UNLNYN = row["UNLNYN"].ToString();
                    string READYN = row["READYN"].ToString();
                    string MULTILINEYN = row["MULTILINEYN"].ToString();
                    string MANDATORY = row["MANDATORY"].ToString();
                    string Critic_DP = row["Critic_DP"].ToString();
                    string FORMAT = row["FORMAT"].ToString();
                    Label lblMandatoryc2 = (Label)e.Item.FindControl("lblMandatoryc2");
                    Label lblCRTDPc2 = (Label)e.Item.FindControl("lblCRTDPc2");
                    Label lblSEQNOc2 = (Label)e.Item.FindControl("lblSEQNOc2");
                    Label lblheaderc2SEQNO = (Label)e.Item.FindControl("lblheaderc2SEQNO");
                    Label lblReadOnly = (Label)e.Item.FindControl("lblReadOnly");
                    Label lblInvisible = (Label)e.Item.FindControl("lblInvisible");
                    string Prefix = row["Prefix"].ToString();
                    string INVISIBLE = row["INVISIBLE"].ToString();

                    if (BOLDYN == "True")
                    {
                        lblheader2.CssClass = lblheader2.CssClass + " fontbold";
                    }
                    if (UNLNYN == "True")
                    {
                        lblheader2.Font.Underline = true;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELDc2");
                        btnEdit.Visible = true;
                        lblheader2.Text = FIELDNAME;
                        lblheaderc2SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                        lblheaderc2SEQNO.Visible = true;

                        if (MANDATORY == "True")
                        {
                            lblMandatoryc2.Text = "*";
                            lblMandatoryc2.Visible = true;
                        }

                        if (Critic_DP == "True")
                        {
                            lblCRTDPc2.Text = "✔";
                            lblCRTDPc2.Visible = true;
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 200px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }

                        if (READYN == "True")
                        {
                            lblReadOnly.Text = "𝓡";
                            lblReadOnly.Visible = true;
                        }

                        if (INVISIBLE == "True")
                        {
                            lblInvisible.Visible = true;
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            btnEdit.Text = "DD-MMM-YYYY";
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            btnEdit.Text = "HH:MM";
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            btnEdit.Text = "dd/mm/yyyy";
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "N" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "N";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "D" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "D";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += "-" + FORMAT;
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "A" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "A";
                            }
                        }

                        ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid1.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                        DataRow[] rows;
                        rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                        foreach (DataRow row1 in rows)
                            ds1.Tables[0].Rows.Remove(row1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            repeat_CHK3.DataSource = ds1;
                            repeat_CHK3.DataBind();
                        }
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            CheckBox CHK_FIELD2 = (CheckBox)e.Item.FindControl("CHK_FIELDc2");
                            CHK_FIELD2.Text = row["TEXT"].ToString();
                            CHK_FIELD2.Visible = true;
                            lblSEQNOc2.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNOc2.Visible = true;

                            if (hdnfieldname2.Value != VARIABLENAME)
                            {
                                lblheader2.Text = FIELDNAME;
                                hdnfieldname2.Value = VARIABLENAME;
                                lblheaderc2SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheaderc2SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatoryc2.Text = "*";
                                    lblMandatoryc2.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDPc2.Text = "✔";
                                    lblCRTDPc2.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }

                            ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid2.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                            DataRow[] rows;
                            rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                            foreach (DataRow row1 in rows)
                                ds1.Tables[0].Rows.Remove(row1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                repeat_CHK3.DataSource = ds1;
                                repeat_CHK3.DataBind();
                            }
                        }
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        RadioButton RAD_FIELD2 = (RadioButton)e.Item.FindControl("RAD_FIELDc2");
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            RAD_FIELD2.Text = row["TEXT"].ToString();
                            RAD_FIELD2.Visible = true;
                            lblSEQNOc2.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNOc2.Visible = true;

                            if (hdnfieldname2.Value != VARIABLENAME)
                            {
                                lblheader2.Text = FIELDNAME;
                                hdnfieldname2.Value = VARIABLENAME;
                                lblheaderc2SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheaderc2SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatoryc2.Text = "*";
                                    lblMandatoryc2.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDPc2.Text = "✔";
                                    lblCRTDPc2.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }

                            ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_REPORTS_DATA_ANNONATED",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            MODULEID: HDNMODULEID.Value,
                            ID: hdnid2.Value,
                            DEFULTVAL: row["TEXTS"].ToString(),
                            VISITNUM: hdnVISITID.Value
                            );

                            DataRow[] rows;
                            rows = ds1.Tables[0].Select("TEXTS = '--Select--'");
                            foreach (DataRow row1 in rows)
                                ds1.Tables[0].Rows.Remove(row1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                repeat_CHK3.DataSource = ds1;
                                repeat_CHK3.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void repeat_CHK3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    DataSet ds1 = new DataSet();
                    DataSet ds = new DataSet();
                    string ID = row["FIELD_ID"].ToString();
                    hdnid2.Value = ID;
                    string CONTROLTYPE = row["CONTROLTYPE"].ToString();
                    string VARIABLENAME = row["VARIABLENAME"].ToString();
                    string FIELDNAME = row["FIELDNAME"].ToString();
                    Label lblheader3 = (Label)e.Item.FindControl("lblheaderc3");
                    string CLASS = row["CLASS"].ToString();

                    string MAXLEN = row["MAXLEN"].ToString();
                    string UPPERCASE = row["UPPERCASE"].ToString();
                    string DEFULTVAL = row["DEFAULTVAL"].ToString();
                    string BOLDYN = row["BOLDYN"].ToString();
                    string UNLNYN = row["UNLNYN"].ToString();
                    string READYN = row["READYN"].ToString();
                    string MULTILINEYN = row["MULTILINEYN"].ToString();
                    string MANDATORY = row["MANDATORY"].ToString();
                    string Critic_DP = row["Critic_DP"].ToString();
                    string FORMAT = row["FORMAT"].ToString();
                    Label lblMandatoryc3 = (Label)e.Item.FindControl("lblMandatoryc3");
                    Label lblCRTDPc3 = (Label)e.Item.FindControl("lblCRTDPc3");
                    Label lblSEQNOc3 = (Label)e.Item.FindControl("lblSEQNOc3");
                    Label lblheaderc3SEQNO = (Label)e.Item.FindControl("lblheaderc3SEQNO");
                    Label lblReadOnly = (Label)e.Item.FindControl("lblReadOnly");
                    Label lblInvisible = (Label)e.Item.FindControl("lblInvisible");
                    string Prefix = row["Prefix"].ToString();
                    string INVISIBLE = row["INVISIBLE"].ToString();

                    if (BOLDYN == "True")
                    {
                        lblheader3.CssClass = lblheader3.CssClass + " fontbold";
                    }
                    if (UNLNYN == "True")
                    {
                        lblheader3.Font.Underline = true;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELDc3");
                        btnEdit.Visible = true;
                        lblheader3.Text = FIELDNAME;
                        lblheaderc3SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                        lblheaderc3SEQNO.Visible = true;

                        if (MANDATORY == "True")
                        {
                            lblMandatoryc3.Text = "*";
                            lblMandatoryc3.Visible = true;
                        }

                        if (Critic_DP == "True")
                        {
                            lblCRTDPc3.Text = "✔";
                            lblCRTDPc3.Visible = true;
                        }

                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 200px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }

                        if (READYN == "True")
                        {
                            lblReadOnly.Text = "𝓡";
                            lblReadOnly.Visible = true;
                        }

                        if (INVISIBLE == "True")
                        {
                            lblInvisible.Visible = true;
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            btnEdit.Text = "DD-MMM-YYYY";
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            btnEdit.Text = "HH:MM";
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            btnEdit.Text = "dd/mm/yyyy";
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "N" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "N";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "D" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "D";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += "-" + FORMAT;
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (MAXLEN != "0")
                            {
                                btnEdit.Text = "A" + MAXLEN;
                            }
                            else
                            {
                                btnEdit.Text = "A";
                            }
                        }
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            CheckBox CHK_FIELD3 = (CheckBox)e.Item.FindControl("CHK_FIELDc3");
                            CHK_FIELD3.Text = row["TEXT"].ToString();
                            CHK_FIELD3.Visible = true;
                            lblSEQNOc3.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNOc3.Visible = true;

                            if (hdnfieldname3.Value != VARIABLENAME)
                            {
                                lblheader3.Text = FIELDNAME;
                                hdnfieldname3.Value = VARIABLENAME;
                                lblheaderc3SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheaderc3SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatoryc3.Text = "*";
                                    lblMandatoryc3.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDPc3.Text = "✔";
                                    lblCRTDPc3.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }
                        }
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (row["TEXTS"].ToString() != "--Select--")
                        {
                            RadioButton RAD_FIELD3 = (RadioButton)e.Item.FindControl("RAD_FIELDc3");
                            RAD_FIELD3.Text = row["TEXT"].ToString();
                            RAD_FIELD3.Visible = true;
                            lblSEQNOc3.Text = "[" + row["DRPSEQNO"].ToString() + "]";
                            lblSEQNOc3.Visible = true;

                            if (hdnfieldname3.Value != VARIABLENAME)
                            {
                                lblheader3.Text = FIELDNAME;
                                hdnfieldname3.Value = VARIABLENAME;
                                lblheaderc3SEQNO.Text = "[" + row["SEQNO"].ToString() + "]";
                                lblheaderc3SEQNO.Visible = true;

                                if (MANDATORY == "True")
                                {
                                    lblMandatoryc3.Text = "*";
                                    lblMandatoryc3.Visible = true;
                                }

                                if (Critic_DP == "True")
                                {
                                    lblCRTDPc3.Text = "✔";
                                    lblCRTDPc3.Visible = true;
                                }

                                if (READYN == "True")
                                {
                                    lblReadOnly.Text = "𝓡";
                                    lblReadOnly.Visible = true;
                                }

                                if (INVISIBLE == "True")
                                {
                                    lblInvisible.Visible = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}