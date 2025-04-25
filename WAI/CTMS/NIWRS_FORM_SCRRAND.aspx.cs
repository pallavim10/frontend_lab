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
using System.Text.RegularExpressions;

namespace CTMS
{
    public partial class NIWRS_FORM_SCRRAND : System.Web.UI.Page
    {
        DataTable dt_AuditTrail = new DataTable("NIWRS_AUDITTRAIL");
        DataTable dt_DM_AuditTrail = new DataTable("DM_AUDITTRAIL");
        DataTable dtCurrentDATA = new DataTable();

        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();

        string RAND_DATE;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SUBJECTTEXT.Text = Session["SUBJECTTEXT"].ToString();

                    string Maxlength = "";

                    DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_QUE_ANS", QUECODE: "SUBJECTLENGTH");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Maxlength = ds.Tables[0].Rows[0]["ANS"].ToString();

                            if (Maxlength != null)
                            {
                                txtScreeningID.MaxLength = int.Parse(Maxlength);
                            }
                            else
                            {
                                txtScreeningID.MaxLength = 6;
                            }
                        }
                    }

                    Session["PREV_URL"] = Request.RawUrl.ToString();
                    Get_Sites();
                    drpSite_SelectedIndexChanged(sender, e);

                    GET_AVL_KITS();
                    GET_FORM();
                    GET_STRUCTURE();

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
                    dtCurrentDATA = GET_GRID_DATATABLE();

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


        private void Get_Sites()
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
                        drpSite.DataSource = ds.Tables[0];
                        drpSite.DataValueField = "INVID";
                        drpSite.DataBind();
                    }
                    else
                    {
                        drpSite.DataSource = ds.Tables[0];
                        drpSite.DataValueField = "INVID";
                        drpSite.DataBind();
                        drpSite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
                else
                {
                    drpSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_AVL_RAND_NUM()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_AVL_SP(ACTION: "GET_AVL_RAND_NUM", SUBJID: txtScreeningID.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["RANDNO"].ToString() == "0")
                    {
                        string MSG = ds.Tables[0].Rows[0]["MSG"].ToString();
                        string URL = "IWRS_Status_Dashboard.aspx";
                        //Response.Write("<script> alert('" + MSG + "'); window.location.href = '" + URL + "'; </script>");
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('" + MSG + "'); window.location.href = '" + URL + "'; ", true);
                    }
                    else
                    {
                        hfRANDID.Value = ds.Tables[0].Rows[0]["RANDNO"].ToString();
                        hfRANDTREATGRP.Value = ds.Tables[0].Rows[0]["TREAT_GRP"].ToString();

                        Response.Redirect("NIWRS_ACTIONS.aspx?STEPID=" + Request.QueryString["STEPID"].ToString() + "&SUBJID=" + hfSUBJID.Value + "&RANDNO=" + hfRANDID.Value + "&VISIT=" + hfApplVisit.Value + "");
                    }
                }
                else
                {
                    //Response.Redirect("NIWRS_ACTIONS.aspx?STEPID=" + Request.QueryString["STEPID"].ToString() + "&SUBJID=" + hfSUBJID.Value + "&RANDNO=" + hfRANDID.Value + "&VISIT=" + hfApplVisit.Value + "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Randomization Number Not Available'); window.location.href = 'IWRS_Status_Dashboard.aspx'; ", true);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_AVL_KITS()
        {
            try
            {
                if (drpSite.SelectedValue != "0")
                {
                    DataSet dsArm = dal_IWRS.IWRS_KITS_SP(ACTION: "GET_ALL_ARMS", SUBJID: hfSUBJID.Value);

                    foreach (DataRow dr in dsArm.Tables[0].Rows)
                    {
                        DataSet ds = dal_IWRS.IWRS_KITS_SP(ACTION: "GET_AVL_KITS", TREAT_GRP: dr["TREAT_GRP"].ToString(), TREAT_STRENGTH: dr["TREAT_STRENGTH"].ToString(), SITEID: hfSITEID.Value);

                        if (ds.Tables[0].Rows.Count < 1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Kits Not Available'); window.location.href = 'IWRS_Status_Dashboard.aspx'; ", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_FORM()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_FORM_SP(ACTION: "GET_FORM", ID: Request.QueryString["STEPID"].ToString());

                lblHeader.Text = ds.Tables[0].Rows[0]["HEADER"].ToString();
                hfMODULEID.Value = ds.Tables[0].Rows[0]["SOURCE_ID"].ToString();
                hfTablename.Value = ds.Tables[0].Rows[0]["IWRS_TABLENAME"].ToString();
                hfDM_SYNC.Value = ds.Tables[0].Rows[0]["DM_SYNC"].ToString();
                hfDM_Tablename.Value = ds.Tables[0].Rows[0]["DM_TABLENAME"].ToString();
                hfDM_MODULEID.Value = ds.Tables[0].Rows[0]["DM_MODULEID"].ToString();
                hfApplVisit.Value = ds.Tables[0].Rows[0]["ApplVisit"].ToString();
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
                DataSet ds = dal_IWRS.IWRS_STRUCTURE_SP(ACTION: "GET_STRUCTURE", ID: hfMODULEID.Value);
                grd_Data.DataSource = ds;
                grd_Data.DataBind();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Change", "callChange();", true);
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

                            if (VARIABLENAME == "RNSITEID")
                            {
                                btnEdit.Text = hfSITEID.Value;
                            }
                            else if (VARIABLENAME == "RCNTRY" || FIELDNAME == "Country")
                            {
                                btnEdit.Text = hfCOUNTRY.Value;
                            }
                            else if (VARIABLENAME == "RAND_DAT" || VARIABLENAME == "RNDAT")
                            {
                                btnEdit.Text = Session["IWRS_CurrentDate"].ToString();
                            }
                            else if (VARIABLENAME == "RNTIM")
                            {
                                btnEdit.Text = commFun.GetCurrentDateTimeByTimezone().ToString("HH:mm");
                            }
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
                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);
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

                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);

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

                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);

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

                    DataSet ds1 = dal_IWRS.IWRS_STRUCTURE_SP(ACTION: "GET_STRUCTURE_CHILD", ID: hfMODULEID.Value, FIELDID: ID);
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

                            if (VARIABLENAME == "RNSITEID")
                            {
                                btnEdit.Text = hfSITEID.Value;
                            }
                            else if (VARIABLENAME == "RCNTRY" || FIELDNAME == "Country")
                            {
                                btnEdit.Text = hfCOUNTRY.Value;
                            }
                            else if (VARIABLENAME == "RAND_DAT" || VARIABLENAME == "RNDAT")
                            {
                                btnEdit.Text = Session["IWRS_CurrentDate"].ToString();
                            }
                            else if (VARIABLENAME == "RNTIM")
                            {
                                btnEdit.Text = commFun.GetCurrentDateTimeByTimezone().ToString("HH:mm");
                            }
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
                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);
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

                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);

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

                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);

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

                    DataSet ds1 = dal_IWRS.IWRS_STRUCTURE_SP(ACTION: "GET_STRUCTURE_CHILD", ID: hfMODULEID.Value, FIELDID: ID);
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

                            if (VARIABLENAME == "RNSITEID")
                            {
                                btnEdit.Text = hfSITEID.Value;
                            }
                            else if (VARIABLENAME == "RCNTRY" || FIELDNAME == "Country")
                            {
                                btnEdit.Text = hfCOUNTRY.Value;
                            }
                            else if (VARIABLENAME == "RAND_DAT" || VARIABLENAME == "RNDAT")
                            {
                                btnEdit.Text = Session["IWRS_CurrentDate"].ToString();
                            }
                            else if (VARIABLENAME == "RNTIM")
                            {
                                btnEdit.Text = commFun.GetCurrentDateTimeByTimezone().ToString("HH:mm");
                            }
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
                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);
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

                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);

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

                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);

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

                    DataSet ds1 = dal_IWRS.IWRS_STRUCTURE_SP(ACTION: "GET_STRUCTURE_CHILD", ID: hfMODULEID.Value, FIELDID: ID);
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
                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);
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

                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);

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

                        ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_OPTIONS", VARIABLENAME: VARIABLENAME, FORMID: hfMODULEID.Value);

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

        private bool INSERT_SCREENING_ID()
        {
            bool RES = false;
            hfSCR_NA.Value = "";
            try
            {
                DataSet ds = dal_IWRS.IWRS_ACTIONS_SP(
                ACTION: "INSERT_SCREENING_ID",
                STEPID: Request.QueryString["STEPID"].ToString(),
                SITEID: drpSite.SelectedValue,
                SUBJID: txtScreeningID.Text
                );

                hfSUBJID.Value = txtScreeningID.Text;
                drpSite.SelectedValue = drpSite.SelectedValue;

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "Added")
                    {
                        RES = true;
                    }
                    else
                    {
                        RES = false;
                        hfSCR_NA.Value = ds.Tables[0].Rows[0]["RESULT"].ToString();
                    }
                }
                else
                {
                    RES = false;
                    hfSCR_NA.Value = "Something went wrong.";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return RES;
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dtCurrentDATA = GET_GRID_DATATABLE();

                SET_VALUE_ONLOAD();

                hfSUBJID.Value = txtScreeningID.Text;
                drpSite.SelectedValue = drpSite.SelectedValue;

                if (drpSite.SelectedValue != txtScreeningID.Text)
                {
                    if (CHECK_ONSUBMIT_INPUTMASK_CRIT())
                    {
                        if (CHECK_OnSubmit_CRITs())
                        {
                            if (CHECK_StopClause())
                            {
                                if (INSERT_SCREENING_ID())
                                {
                                    INSERT_FORM_DATA();

                                    GET_AVL_KITS();

                                    GET_AVL_RAND_NUM();

                                    //Response.Redirect("NIWRS_ACTIONS.aspx?STEPID=" + Request.QueryString["STEPID"].ToString() + "&SUBJID=" + hfSUBJID.Value + "&RANDNO=" + hfRANDID.Value + "&VISIT=" + hfApplVisit.Value + "");
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('" + hfSCR_NA.Value + "'); ", true);
                                }
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Site ID and " + Session["SUBJECTTEXT"].ToString() + " can not be same.'); ", true);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("IWRS_HomePage.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool CHECK_OnSubmit_CRITs()
        {
            bool RESULT = true;

            try
            {
                DataSet dsCases = dal_IWRS.IWRS_CRIT_SP(ACTION: "GET_OnSubmit_CRIT_Cases", FormCode: hfMODULEID.Value);
                string CASES = "";
                if (dsCases.Tables.Count > 0 && dsCases.Tables[0].Rows.Count > 0)
                {
                    CASES = dsCases.Tables[0].Rows[0][0].ToString();
                }

                if (CASES != "")
                {
                    foreach (DataRow drCurrentDATA in dtCurrentDATA.Rows)
                    {
                        CASES = CASES.Replace("[" + drCurrentDATA["VARIABLENAME"].ToString() + "]", CheckDatatype(drCurrentDATA["DATA"].ToString()));
                    }
                }

                CASES = CASES.Replace("'''", "''");

                CASES = CASES.Replace("'''", "''");

                CASES = CASES.Replace("'[", "[");

                CASES = CASES.Replace("]'", "]");

                if (CASES.Contains("[") && CASES.Contains("]"))
                {
                    DataSet ds = dal_IWRS.IWRS_GET_SUBJECT_DETAILS_SP(ACTION: "GET_SUBJECT_DETAILS", SUBJID: hfSUBJID.Value);

                    if (CASES.Contains("[SUBJID]"))
                    {
                        CASES = CASES.Replace("[SUBJID]", ds.Tables[0].Rows[0]["SUBJID"].ToString());
                    }

                    if (CASES.Contains("[SITEID]"))
                    {
                        CASES = CASES.Replace("[SITEID]", ds.Tables[0].Rows[0]["SITEID"].ToString());
                    }

                    if (CASES.Contains("[SUBSITEID]"))
                    {
                        CASES = CASES.Replace("[SUBSITEID]", ds.Tables[0].Rows[0]["SUBSITEID"].ToString());
                    }

                    if (CASES.Contains("[RANDNO]"))
                    {
                        CASES = CASES.Replace("[RANDNO]", ds.Tables[0].Rows[0]["RAND_NUM"].ToString());
                    }

                    if (CASES.Contains("[INDICATION]"))
                    {
                        CASES = CASES.Replace("[INDICATION]", ds.Tables[0].Rows[0]["INDICATION"].ToString());
                    }

                    if (CASES.Contains("[KITNO]"))
                    {
                        string KITNOs = "";

                        foreach (DataRow drKITNO in ds.Tables[0].Rows)
                        {
                            if (KITNOs != "")
                            {
                                KITNOs += ", " + drKITNO["KITNO"].ToString();
                            }
                            else
                            {
                                KITNOs = drKITNO["KITNO"].ToString();
                            }
                        }

                        CASES = CASES.Replace("[KITNO]", KITNOs);
                    }

                    if (CASES.Contains("[TGID]"))
                    {
                        CASES = CASES.Replace("[TGID]", ds.Tables[0].Rows[0]["TREAT_GRP"].ToString());
                    }

                    if (CASES.Contains("[TGNAME]"))
                    {
                        CASES = CASES.Replace("[TGNAME]", ds.Tables[0].Rows[0]["TREAT_GRP_NAME"].ToString());
                    }

                    if (CASES.Contains("[EARLYRANDDATE]"))
                    {
                        if (ISDATE(ds.Tables[0].Rows[0]["EARLY_RAND_BY"].ToString()))
                        {
                            CASES = CASES.Replace("[EARLYRANDDATE]", "CAST('" + ds.Tables[0].Rows[0]["EARLY_RAND_BY"].ToString() + "' AS DATETIME)");
                        }
                        else
                        {
                            CASES = CASES.Replace("[EARLYRANDDATE]", ds.Tables[0].Rows[0]["EARLY_RAND_BY"].ToString());
                        }
                    }

                    if (CASES.Contains("[RANDDATE]"))
                    {
                        if (ISDATE(ds.Tables[0].Rows[0]["RAND_BY"].ToString()))
                        {
                            CASES = CASES.Replace("[RANDDATE]", "CAST('" + ds.Tables[0].Rows[0]["RAND_BY"].ToString() + "' AS DATETIME)");
                        }
                        else
                        {
                            CASES = CASES.Replace("[RANDDATE]", ds.Tables[0].Rows[0]["RAND_BY"].ToString());
                        }
                    }

                    if (CASES.Contains("[LATERANDDATE]"))
                    {
                        if (ISDATE(ds.Tables[0].Rows[0]["LATE_RAND_BY"].ToString()))
                        {
                            CASES = CASES.Replace("[LATERANDDATE]", "CAST('" + ds.Tables[0].Rows[0]["LATE_RAND_BY"].ToString() + "' AS DATETIME)");
                        }
                        else
                        {
                            CASES = CASES.Replace("[LATERANDDATE]", ds.Tables[0].Rows[0]["LATE_RAND_BY"].ToString());
                        }
                    }

                    if (CASES.Contains("[LASTVISIT]"))
                    {
                        CASES = CASES.Replace("[LASTVISIT]", ds.Tables[0].Rows[0]["LAST_VISIT"].ToString());
                    }

                    if (CASES.Contains("[LASTVISITDATE]"))
                    {
                        if (ISDATE(ds.Tables[0].Rows[0]["LAST_VISIT_DATE"].ToString()))
                        {
                            CASES = CASES.Replace("[LASTVISITDATE]", "CAST('" + ds.Tables[0].Rows[0]["LAST_VISIT_DATE"].ToString() + "' AS DATETIME)");
                        }
                        else
                        {
                            CASES = CASES.Replace("[LASTVISITDATE]", ds.Tables[0].Rows[0]["LAST_VISIT_DATE"].ToString());
                        }
                    }

                    if (CASES.Contains("[NEXTVISIT]"))
                    {
                        CASES = CASES.Replace("[NEXTVISIT]", ds.Tables[0].Rows[0]["NEXT_VISIT"].ToString());
                    }

                    if (CASES.Contains("[EARLYVISITDATE]"))
                    {
                        if (ISDATE(ds.Tables[0].Rows[0]["EARLY_DATE"].ToString()))
                        {
                            CASES = CASES.Replace("[EARLYVISITDATE]", "CAST('" + ds.Tables[0].Rows[0]["EARLY_DATE"].ToString() + "' AS DATETIME)");
                        }
                        else
                        {
                            CASES = CASES.Replace("[EARLYVISITDATE]", ds.Tables[0].Rows[0]["EARLY_DATE"].ToString());
                        }
                    }

                    if (CASES.Contains("[VISITDATE]"))
                    {
                        if (ISDATE(ds.Tables[0].Rows[0]["NEXT_VISIT_DATE"].ToString()))
                        {
                            CASES = CASES.Replace("[VISITDATE]", "CAST('" + ds.Tables[0].Rows[0]["NEXT_VISIT_DATE"].ToString() + "' AS DATETIME)");
                        }
                        else
                        {
                            CASES = CASES.Replace("[VISITDATE]", ds.Tables[0].Rows[0]["NEXT_VISIT_DATE"].ToString());
                        }
                    }

                    if (CASES.Contains("[LATEVISITDATE]"))
                    {
                        if (ISDATE(ds.Tables[0].Rows[0]["LATE_DATE"].ToString()))
                        {
                            CASES = CASES.Replace("[LATEVISITDATE]", "CAST('" + ds.Tables[0].Rows[0]["LATE_DATE"].ToString() + "' AS DATETIME)");
                        }
                        else
                        {
                            CASES = CASES.Replace("[LATEVISITDATE]", ds.Tables[0].Rows[0]["LATE_DATE"].ToString());
                        }
                    }

                    if (CASES.Contains("[USER]"))
                    {
                        CASES = CASES.Replace("[USER]", Session["User_Name"].ToString());
                    }

                    if (CASES.Contains("[DATETIME]"))
                    {
                        if (ISDATE(ds.Tables[0].Rows[0]["LATE_DATE"].ToString()))
                        {
                            CASES = CASES.Replace("[LATEVISITDATE]", "CAST('" + commFun.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt") + "' AS DATETIME)");
                        }
                        else
                        {
                            CASES = CASES.Replace("[DATETIME]", commFun.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                        }
                    }

                    if (CASES.Contains("[") && CASES.Contains("]"))
                    {
                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataColumn dc in ds.Tables[1].Columns)
                                {
                                    if (CASES.Contains("[" + dc.ToString() + "]"))
                                    {
                                        if (ISDATE(ds.Tables[1].Rows[0][dc].ToString()))
                                        {
                                            CASES = CASES.Replace("[" + dc.ToString() + "]", "CAST('" + ds.Tables[1].Rows[0][dc].ToString() + "' AS DATETIME)");
                                        }
                                        else
                                        {
                                            CASES = CASES.Replace("[" + dc.ToString() + "]", "'" + ds.Tables[1].Rows[0][dc].ToString() + "'");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                DataSet dsRESULT = dal_IWRS.IWRS_CRIT_SP(ACTION: "GET_OnSubmit_CRIT_Result", DATA: CASES);

                if (dsRESULT.Tables.Count > 0 && dsRESULT.Tables[0].Rows.Count > 0)
                {
                    if (dsRESULT.Tables[0].Rows[0][0].ToString() != "")
                    {
                        Response.Write("<script> alert('" + dsRESULT.Tables[0].Rows[0][0].ToString() + "');</script>");

                        RESULT = false;
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            return RESULT;
        }

        private bool CHECK_StopClause()
        {
            bool Result = false;
            try
            {
                DataSet dsClause = dal_IWRS.NIWRS_STOP_CLAUSE_SP(
                ACTION: "GET_STOP_CLAUSE_BEFORE_CRIT",
                MODULEID: hfDM_MODULEID.Value,
                SUBJID: txtScreeningID.Text
                );

                if (dsClause.Tables.Count > 0 && dsClause.Tables[0].Rows.Count > 0 && dsClause.Tables[0].Rows[0][0].ToString() != "")
                {
                    DataRow drClause = dsClause.Tables[0].Rows[0];

                    DataTable dtCaseDATA = new DataTable();
                    dtCaseDATA.Columns.Add("VARIABLENAME");
                    dtCaseDATA.Columns.Add("DATA");

                    DataSet dsVarData = new DataSet();

                    string CASES = drClause["CASES"].ToString();

                    foreach (DataRow drCurrentDATA in dtCurrentDATA.Rows)
                    {
                        CASES = CASES.Replace("[" + drCurrentDATA["VARIABLENAME"].ToString() + "]", CheckDatatype(drCurrentDATA["DATA"].ToString()));
                    }

                    if (CASES.Contains("[") && CASES.Contains("]"))
                    {
                        DataSet dsVariables = dal_IWRS.NIWRS_STOP_CLAUSE_SP(
                        ACTION: "GET_SC_VARIABLES_DATA",
                        SUBJID: hfSUBJID.Value
                        );

                        if (dsVariables.Tables.Count > 1)
                        {
                            DataRow drVariablesDATA = dsVariables.Tables[1].Rows[0];

                            CASES = CASES.Replace("[SITEID]", CheckDatatype(drVariablesDATA["SITEID"].ToString()));

                            CASES = CASES.Replace("[TREAT_GRP]", CheckDatatype(drVariablesDATA["TREAT_GRP"].ToString()));

                            foreach (DataRow drVariables in dsVariables.Tables[0].Rows)
                            {
                                if (CASES.Contains("[" + drVariables["VARIABLENAME"].ToString() + "]"))
                                {
                                    CASES = CASES.Replace("[" + drVariables["VARIABLENAME"].ToString() + "]", CheckDatatype(drVariablesDATA[drVariables["VARIABLENAME"].ToString()].ToString()));
                                }
                            }
                        }
                    }

                    DataSet dsRESULT = dal_IWRS.NIWRS_STOP_CLAUSE_SP(ACTION: "GET_SC_RESULT", Criteria: CASES);

                    if (dsRESULT.Tables.Count > 0 && dsRESULT.Tables[0].Rows.Count > 0)
                    {
                        string CritCode = dsRESULT.Tables[0].Rows[0]["CritCode"].ToString();
                        string Limit = dsRESULT.Tables[0].Rows[0]["LIMIT"].ToString();
                        string MSGBOX = dsRESULT.Tables[0].Rows[0]["MSGBOX"].ToString();

                        DataSet dsSUBJECTs = dal_IWRS.NIWRS_STOP_CLAUSE_SP(ACTION: "GET_SUBJECT_COUNT", CritCode: CritCode);

                        string SubjectCount = dsSUBJECTs.Tables[0].Rows[0]["SubjectCount"].ToString();

                        if (Convert.ToInt32(Limit) <= Convert.ToInt32(SubjectCount))
                        {
                            Result = false;

                            Response.Write("<script> alert('" + MSGBOX + "');</script>");
                        }
                        else
                        {
                            Result = true;
                        }
                    }
                    else
                    {
                        Result = true;
                    }

                }
                else
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() != "Conversion failed when converting date and/or time from character string.")
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            return Result;
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


            DataSet dsDETAILS = dal_IWRS.IWRS_GET_SUBJECT_DETAILS_SP(ACTION: "GET_SUBJECT_DETAILS", SUBJID: txtScreeningID.Text, FORMID: hfMODULEID.Value);

            if (dsDETAILS.Tables.Count > 1 && dsDETAILS.Tables[1].Rows.Count > 0)
            {
                foreach (DataColumn dc in dsDETAILS.Tables[1].Columns)
                {
                    outputTable.Rows.Add(dc.ColumnName, dsDETAILS.Tables[1].Rows[0][dc.ColumnName].ToString(), "", "", "", "");
                }
            }

            return outputTable;
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

                string syncCOLUMN = "";
                string syncDATA = "";

                string Strata = "";

                string INSERTQUERY = "", UPDATEQUERY = "";

                for (int rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
                {
                    string strdata = "";
                    varname = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                    Strata = ((Label)grd_Data.Rows[rownum].FindControl("lblSTRATA")).Text;
                    string CONTROLTYPE;
                    CONTROLTYPE = ((Label)grd_Data.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                    string DM_SYNC;
                    DM_SYNC = ((Label)grd_Data.Rows[rownum].FindControl("IWRS_DM_SYNC")).Text;

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
                            }
                            else
                            {
                                DATA = DATA + " @ni$h ''";
                            }
                        }
                        else
                        {
                            if (strdata != "")
                            {
                                DATA = "N'" + strdata + "'";
                            }
                            else
                            {
                                DATA = "''";
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
                                }
                                else
                                {
                                    syncDATA = syncDATA + " @ni$h ''";
                                }
                            }
                            else
                            {
                                if (strdata != "")
                                {
                                    syncDATA = "N'" + strdata + "'";
                                }
                                else
                                {
                                    syncDATA = "''";
                                }
                            }
                        }
                    }
                    if (strdata != "" && Strata != "0")
                    {
                        dal_IWRS.IWRS_DATA_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata, SUBJID: hfSUBJID.Value);
                    }

                    GridView grd_Data1 = grd_Data.Rows[rownum].FindControl("grd_Data1") as GridView;

                    for (int b = 0; b < grd_Data1.Rows.Count; b++)
                    {
                        string Val_Child;
                        string strdata1 = "";
                        Val_Child = ((Label)grd_Data1.Rows[b].FindControl("lblVal_Child")).Text;

                        varname = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                        Strata = ((Label)grd_Data1.Rows[b].FindControl("lblSTRATA")).Text;
                        CONTROLTYPE = ((Label)grd_Data1.Rows[b].FindControl("lblCONTROLTYPE")).Text;
                        DM_SYNC = ((Label)grd_Data1.Rows[b].FindControl("IWRS_DM_SYNC")).Text;

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
                                            }
                                            else
                                            {
                                                syncDATA = syncDATA + " @ni$h NULL";

                                                strdata1 = "";
                                            }
                                        }
                                        else
                                        {
                                            if (strdata1 != "")
                                            {
                                                syncDATA = "N'" + strdata1 + "'";
                                            }
                                            else
                                            {
                                                syncDATA = "NULL";

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

                                    strdata1 = "";
                                }
                                else
                                {
                                    DATA = "NULL";

                                    strdata1 = "";
                                }
                            }

                            if (strdata1 != "" && Strata != "0")
                            {
                                dal_IWRS.IWRS_DATA_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata1, SUBJID: hfSUBJID.Value);
                            }
                        }

                        GridView grd_Data2 = grd_Data1.Rows[b].FindControl("grd_Data2") as GridView;

                        for (int c = 0; c < grd_Data2.Rows.Count; c++)
                        {
                            string Val_Child1 = ((Label)grd_Data2.Rows[c].FindControl("lblVal_Child")).Text;
                            string strdata2 = "";
                            varname = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                            Strata = ((Label)grd_Data2.Rows[c].FindControl("lblSTRATA")).Text;
                            CONTROLTYPE = ((Label)grd_Data2.Rows[c].FindControl("lblCONTROLTYPE")).Text;
                            DM_SYNC = ((Label)grd_Data2.Rows[c].FindControl("IWRS_DM_SYNC")).Text;

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
                                            }
                                            else
                                            {
                                                DATA = DATA + " @ni$h NULL";
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
                                                }
                                                else
                                                {
                                                    syncDATA = syncDATA + " @ni$h NULL";

                                                    strdata2 = "";
                                                }
                                            }
                                            else
                                            {
                                                if (strdata2 != "")
                                                {
                                                    syncDATA = "N'" + strdata2 + "'";
                                                }
                                                else
                                                {
                                                    syncDATA = "NULL";

                                                    strdata2 = "";
                                                }
                                            }
                                        }
                                    }
                                }

                                if (strdata2 != "" && Strata != "0")
                                {
                                    dal_IWRS.IWRS_DATA_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata2, SUBJID: hfSUBJID.Value);
                                }
                            }

                            GridView grd_Data3 = grd_Data2.Rows[c].FindControl("grd_Data3") as GridView;

                            for (int d = 0; d < grd_Data3.Rows.Count; d++)
                            {
                                string Val_Child2 = ((Label)grd_Data3.Rows[d].FindControl("lblVal_Child")).Text;
                                string strdata3 = "";
                                varname = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                Strata = ((Label)grd_Data3.Rows[d].FindControl("lblSTRATA")).Text;
                                CONTROLTYPE = ((Label)grd_Data3.Rows[d].FindControl("lblCONTROLTYPE")).Text;
                                DM_SYNC = ((Label)grd_Data3.Rows[d].FindControl("IWRS_DM_SYNC")).Text;

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
                                                }
                                                else
                                                {
                                                    DATA = DATA + " @ni$h NULL";
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
                                                    }
                                                    else
                                                    {
                                                        syncDATA = syncDATA + " @ni$h NULL";

                                                        strdata3 = "";
                                                    }
                                                }
                                                else
                                                {
                                                    if (strdata3 != "")
                                                    {
                                                        syncDATA = "N'" + strdata3 + "'";
                                                    }
                                                    else
                                                    {
                                                        syncDATA = "NULL";

                                                        strdata3 = "";
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (strdata3 != "" && Strata != "0")
                                    {
                                        dal_IWRS.IWRS_DATA_SP(ACTION: "UPDATE_STRATA", VARIABLENAME: varname, ANSWER: strdata3, SUBJID: hfSUBJID.Value);
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
                    INSERTQUERY = "INSERT INTO [" + hfTablename.Value + "] ([SUBJID_DATA], [SITEID], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + hfSUBJID.Value + "', '" + hfSITEID.Value + "', '" + Session["User_ID"].ToString() + "','" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + " )";
                }
                else
                {
                    INSERTQUERY = "INSERT INTO [" + hfTablename.Value + "] ([SUBJID_DATA], [SITEID], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + hfSUBJID.Value + "', '" + hfSITEID.Value + "', '" + Session["User_ID"].ToString() + "','" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + " )";
                }

                string[] colArr = COLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                string[] dataArr = DATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
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

                    IWRS_ADD_NEW_ROW_DATA(
                        STEPID: Request.QueryString["STEPID"].ToString(),
                        REASON: "Initial Entry",
                        SUBJID: hfSUBJID.Value,
                        MODULENAME: lblHeader.Text,
                        TABLENAME: hfTablename.Value,
                        VARIABLENAME: colArr[i],
                        OLDVALUE: "",
                        NEWVALUE: dataArr[i]
                        );
                }
                UPDATEQUERY = UPDATEQUERY + " WHERE SUBJID_DATA = '" + hfSUBJID.Value + "' ";

                dal_IWRS.IWRS_INSERT_DATA_SP(
                 ACTION: "INSERT_FORM_DATA",
                 INSERTQUERY: INSERTQUERY,
                 TABLENAME: hfTablename.Value,
                 SUBJID: hfSUBJID.Value,
                 UPDATEQUERY: UPDATEQUERY
                 );

                //Insert Bulk Audit Trail of IWRS
                if (dt_AuditTrail.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "NIWRS_AUDITTRAIL";

                        sqlbc.ColumnMappings.Add("SOURCE", "SOURCE");
                        sqlbc.ColumnMappings.Add("STEPID", "STEPID");
                        sqlbc.ColumnMappings.Add("REASON", "REASON");
                        sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                        sqlbc.ColumnMappings.Add("MODULENAME", "MODULENAME");
                        sqlbc.ColumnMappings.Add("TABLENAME", "TABLENAME");
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

                        string SYNC_COL = "", SYNC_Data = "";

                        string syncINSERTQUERY = "", syncUPDATEQUERY = "";

                        string PVID = Session["PROJECTID"].ToString() + "-" + hfSITEID.Value + "-" + hfSUBJID.Value + "-" + VISITID + "-" + SYNC_MODULEID + "-1";
                        string RECID = "0";

                        string[] synccolArr = syncCOLUMN.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                        string[] syncdataArr = syncDATA.Split(new string[] { "@ni$h" }, StringSplitOptions.None);

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
                                    }
                                }
                            }
                        }

                        string[] qryColArr = SYNC_COL.Split(new string[] { "," }, StringSplitOptions.None);
                        string[] qryDataArr = SYNC_Data.Split(new string[] { "," }, StringSplitOptions.None);

                        syncINSERTQUERY = "INSERT INTO [" + SYNC_TABLENAME + "] ([PVID], [RECID], [SUBJID_DATA], [VISITNUM], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + SYNC_COL.Replace("@ni$h", ",") + ") VALUES ('" + PVID + "', '" + RECID + "', '" + hfSUBJID.Value + "', '" + VISITID + "', '" + Session["User_ID"].ToString() + "','" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + SYNC_Data.Replace("@ni$h", ",") + ")";

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

                            DM_ADD_NEW_ROW_DATA(
                                PVID: PVID,
                                RECID: RECID,
                                SUBJID: hfSUBJID.Value,
                                VISITNUM: VISITID,
                                MODULENAME: SYNC_MODULENAME,
                                TABLENAME: SYNC_TABLENAME,
                                VariableName: qryColArr[i],
                                OldValue: "",
                                NewValue: qryDataArr[i],
                                Reason: "Initial Entry",
                                Comment: ""
                                );
                        }

                        syncUPDATEQUERY = syncUPDATEQUERY + " WHERE PVID = '" + PVID + "' AND RECID = '" + RECID + "' AND SUBJID_DATA = '" + hfSUBJID.Value + "' ";

                        DAL_DM dal_dm = new DAL_DM();

                        dal_dm.DM_INSERT_MODULE_DATA_SP(
                         ACTION: "INSERT_MODULE_DATA",
                         INSERTQUERY: syncINSERTQUERY,
                         UPDATEQUERY: syncUPDATEQUERY,
                         TABLENAME: SYNC_TABLENAME,
                         PVID: PVID,
                         RECID: RECID,
                         SUBJID: hfSUBJID.Value,
                         MODULEID: SYNC_MODULEID,
                         VISITNUM: VISITID,
                         IsComplete: true,
                         IsMissing: false,
                         INVID: hfSITEID.Value
                         );

                        dal_dm.DM_GetSetPV(
                        PVID: PVID,
                        SUBJID: hfSUBJID.Value,
                        MODULEID: SYNC_MODULEID,
                        VISITNUM: VISITID,
                        PAGESTATUS: "1",
                        HasMissing: false,
                        INVID: hfSITEID.Value
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

                        dal.ESOURCE_INSERT_MODULE_DATA_SP(
                         ACTION: "INSERT_MODULE_DATA",
                         INSERTQUERY: syncINSERTQUERY.Replace("DM_DATA_", "ESOURCE_DATA_"),
                         UPDATEQUERY: syncUPDATEQUERY.Replace("DM_DATA_", "ESOURCE_DATA_"),
                         TABLENAME: SYNC_TABLENAME.Replace("DM_DATA_", "ESOURCE_DATA_"),
                         PVID: PVID,
                         RECID: RECID,
                         SUBJID: hfSUBJID.Value,
                         MODULEID: SYNC_MODULEID,
                         VISITNUM: VISITID,
                         IsComplete: true,
                         IsMissing: false,
                         FREEZESTATUS: "False",
                         MODULENAME: SYNC_MODULENAME,
                         VISIT: VISIT,
                         INVID: hfSITEID.Value
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
                        VISIT: VISIT,
                        INVID: hfSITEID.Value
                        );

                    }
                }

                dal_IWRS.IWRS_SYNC_SP(ACTION: "UPDATE_DM_AUDITTRAIL_FIELDNAME", SUBJID: hfSUBJID.Value);
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

        protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hfSITEID.Value = drpSite.SelectedValue;

                DataSet ds = dal_IWRS.IWRS_ACTIONS_SP(ACTION: "GET_COUNTRY_BySITEID", SITEID: drpSite.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hfCOUNTRY.Value = ds.Tables[0].Rows[0]["COUNTRY"].ToString();
                }

                GET_FORM();
                GET_STRUCTURE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DATA_Changed(object sender, EventArgs e)
        {
            try
            {
                dtCurrentDATA = GET_GRID_DATATABLE();

                string VARIABLENAME = "";

                GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

                VARIABLENAME = (row.FindControl("lblVARIABLENAME") as Label).Text;

                CHECK_SET_VALUE("DATACHANGE", VARIABLENAME);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Change", "callChange();", true);
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
                    foreach (DataRow drCriteria in dsCriterias.Tables[0].Rows)
                    {
                        string MainVARIABLENAME = drCriteria["VARIABLENAME"].ToString();

                        if (RESULT)
                        {
                            if (drCriteria["METHOD"].ToString() == "By Criteria")
                            {
                                string CRITCODE = drCriteria["CritCode"].ToString();

                                CRITCODE = CRITCODE.Replace("[Current Date]", "'" + commFun.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy") + "'");
                                CRITCODE = CRITCODE.Replace("[Current Time]", "'" + commFun.GetCurrentDateTimeByTimezone().ToString("HH:mm") + "'");

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

                                        RESULT = false;
                                        resVARIABLENAME = MainVARIABLENAME;
                                    }
                                }

                            }
                            else if (drCriteria["METHOD"].ToString() == "By Formula")
                            {
                                string FORMULA = drCriteria["FORMULA"].ToString();

                                FORMULA = FORMULA.Replace("[Current Date]", "'" + commFun.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy") + "'");
                                FORMULA = FORMULA.Replace("[Current Time]", "'" + commFun.GetCurrentDateTimeByTimezone().ToString("HH:mm") + "'");

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

                                        RESULT = false;
                                        resVARIABLENAME = MainVARIABLENAME;
                                    }
                                }
                            }
                        }
                    }

                }

                if (resVARIABLENAME != "" && resVARIABLENAME != VARIABLENAME)
                {
                    CHECK_SET_VALUE(EVENTS, resVARIABLENAME);
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

                dtCurrentDATA = GET_GRID_DATATABLE();
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
                    dt_AuditTrail.Columns.Add("STEPID");
                    dt_AuditTrail.Columns.Add("REASON");
                    dt_AuditTrail.Columns.Add("SUBJID");
                    dt_AuditTrail.Columns.Add("MODULENAME");
                    dt_AuditTrail.Columns.Add("TABLENAME");
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

        protected void IWRS_ADD_NEW_ROW_DATA(string STEPID, string REASON, string SUBJID, string MODULENAME, string TABLENAME, string VARIABLENAME, string OLDVALUE, string NEWVALUE)
        {
            try
            {
                CREATE_IWRS_AUDITTRAIL_DT();

                DataRow myDataRow;
                myDataRow = dt_AuditTrail.NewRow();
                myDataRow["SOURCE"] = "IWRS";
                myDataRow["STEPID"] = STEPID;
                myDataRow["REASON"] = REASON;
                myDataRow["SUBJID"] = SUBJID;
                myDataRow["MODULENAME"] = MODULENAME;
                myDataRow["TABLENAME"] = TABLENAME.Trim();
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
                myDataRow["FIELDNAME"] = VariableName.Trim();
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

        protected void txtScreeningID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                INPUTMASK_VALIDATE();

                DataSet ds = dal_IWRS.IWRS_AVL_SP(
                    ACTION: "CHECK_MANUAL_SUBJID",
                    SITEID: drpSite.SelectedValue,
                    SUBJID: txtScreeningID.Text
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() != "")
                    {
                        txtScreeningID.Text = "";

                        string MSG = ds.Tables[0].Rows[0][0].ToString();

                        MSG = MSG.Replace("[SUBJID]", SUBJECTTEXT.Text);

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('" + MSG + "'); ", true);
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void INPUTMASK_VALIDATE()
        {
            try
            {

                dtCurrentDATA = GET_GRID_DATATABLE();

                CHECK_ONSUBMIT_INPUTMASK_CRIT();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool FINDINPUTMASK(string INPUTMASK, string INPUTVALUE, string SiteID)
        {
            bool areEqual = true;
            try
            {

                if (!string.IsNullOrEmpty(INPUTMASK))
                {
                    INPUTMASK = INPUTMASK.Replace("[SiteID]", SiteID);
                    INPUTMASK = INPUTMASK.Replace("[", "").Replace("]", "");

                    for (int i = 0; i < INPUTMASK.Length; i++)
                    {
                        char char_INPUTMASK = INPUTMASK[i];
                        char char_INPUT = INPUTVALUE[i];

                        if (char_INPUTMASK != 'X')
                        {
                            if (char_INPUTMASK != char_INPUT)
                            {
                                areEqual = false;
                            }
                        }
                    }
                }
                if (areEqual == false)
                {
                    string SubjectText = Session["SUBJECTTEXT"]?.ToString();
                    string script = $"alert('Invalid format. Please use the format {INPUTMASK} with {SubjectText} as {INPUTVALUE}.');";

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", script, true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return areEqual;

        }
        private bool CHECK_ONSUBMIT_INPUTMASK_CRIT()
        {
            bool RESULT = true;

            try
            {
                DataSet dsCases = dal_IWRS.IWRS_MANG_SUBJECT_CRIT_SP(ACTION: "GET_INPUTMASK_SUBJ_CRIT");

                string CASES = "";
                if (dsCases.Tables.Count > 0 && dsCases.Tables[0].Rows.Count > 0)
                {
                    CASES = dsCases.Tables[0].Rows[0][0].ToString();
                }

                if (CASES != "")
                {
                    foreach (DataRow drCurrentDATA in dtCurrentDATA.Rows)
                    {
                        CASES = CASES.Replace("[" + drCurrentDATA["VARIABLENAME"].ToString() + "]", CheckDatatype(drCurrentDATA["DATA"].ToString()));
                    }
                }

                CASES = CASES.Replace("'''", "''");

                CASES = CASES.Replace("'''", "''");

                CASES = CASES.Replace("'[", "[");

                CASES = CASES.Replace("]'", "]");

                DataSet dsRESULT = dal_IWRS.IWRS_MANG_SUBJECT_CRIT_SP(ACTION: "GET_INPUTMASK_CRIT_Result", DATA: CASES);

                if (dsRESULT.Tables.Count > 0 && dsRESULT.Tables[0].Rows.Count > 0)
                {
                    if (dsRESULT.Tables[0].Rows[0][0].ToString() != "")
                    {

                        string INPUTVALUE = txtScreeningID.Text;
                        string SiteID = drpSite.Text;
                        string INPUTMASK = dsRESULT.Tables[0].Rows[0][0].ToString();

                        if (!FINDINPUTMASK(INPUTMASK, INPUTVALUE, SiteID))
                        {
                            RESULT = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            return RESULT;
        }
    }
}