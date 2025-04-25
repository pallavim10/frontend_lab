using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using CTMS.CommonFunction;
using PPT;

namespace CTMS
{
    public partial class NSAE_NEW_LOG : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        DAL dal = new DAL();
        CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();
        DataTable SAE_AuditTrail = new DataTable("SAE_AUDITTRAIL");

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.IsPostBack)
                {
                    lblSiteId.Text = "SITE ID : " + Request.QueryString["INVID"].ToString();
                    lblSubjectId.Text = "SUBJECT ID : " + Request.QueryString["SUBJID"].ToString();
                    lblStatus.Text = "STATUS : Initial; Incomplete";

                    GETMODULENAME();

                    if (drpModule.SelectedValue != null)
                    {
                        if (Request.QueryString["MODULEID"] != null)
                        {
                            drpModule.SelectedValue = Request.QueryString["MODULEID"].ToString();
                        }

                        DataSet ds = dal_SAE.SAE_PREV_NEXT_MODULE_LOGS_SP(MODULEID: drpModule.SelectedValue);

                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["PreviousValue"].ToString() != "")
                            {
                                btnPrev.Visible = true;
                                hdnPrevValue.Value = ds.Tables[0].Rows[0]["PreviousValue"].ToString();
                            }
                            else
                            {
                                btnPrev.Visible = false;
                            }
                            if (ds.Tables[0].Rows[0]["NextValue"].ToString() != "")
                            {
                                bntSaveComplete.Visible = true;
                                btnNext.Visible = true;
                                hdnnextvalue.Value = ds.Tables[0].Rows[0]["NextValue"].ToString();
                            }
                            else
                            {
                                bntSaveComplete.Visible = true;
                                btnNext.Visible = false;
                            }
                        }

                        if (Request.QueryString["TEMPID"] != null)
                        {
                            hdnSAEID.Value = Request.QueryString["TEMPID"].ToString();
                        }

                        Get_Page_Status();
                        GetStructure(grd_Data);

                        GetRecords(grd_Data);
                        SAE_GetAuditDetails();
                        SAE_GetCommentsetails();
                        SAE_GetOnPageSpecs();
                        GETHELPDATA();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETHELPDATA()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_INSTRUCTION_SP(MODULEID: drpModule.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    LiteralText.Text = ds.Tables[0].Rows[0]["SAE_DATA"].ToString();
                    divhelp.Visible = true;
                }
                else
                {
                    divhelp.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Get_Page_Status()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_PAGE_STATUS_SP(ACTION: "GET_PAGE_STATUS_TEMPID",
                    SAEID: hdnSAEID.Value,
                    RECID: hdnRECID.Value,
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdnIsComplete.Value = ds.Tables[0].Rows[0]["IsComplete"].ToString();
                    hdn_PAGESTATUS.Value = ds.Tables[0].Rows[0]["PAGESTATUS"].ToString();
                }
                else
                {
                    hdnIsComplete.Value = "False";
                    hdn_PAGESTATUS.Value = "0";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        public void GETMODULENAME()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_SAE.SAE_MODULE_SP(ACTION: "GET_SAE_LOG_MODULE");

                drpModule.DataSource = ds.Tables[0];
                drpModule.DataValueField = "MODULEID";
                drpModule.DataTextField = "MODULENAME";
                drpModule.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetStructure(GridView grd)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_STRUCTURE_SP(
                    ACTION: "GET_STRUCTURE",
                    MODULEID: drpModule.SelectedValue,
                    DM_PVID: Convert.ToString(Request.QueryString["DM_PVID"])
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd.DataSource = ds.Tables[0];
                    grd.DataBind();

                    lblModuleName.Text = drpModule.SelectedItem.Text;
                    hfTablename.Value = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                }
                else
                {
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

        private void GetRecords(GridView grd)
        {
            try
            {
                string COLNAME, COLVAL;
                int rownum = 0;

                DataSet dsData = new DataSet();

                if (Request.QueryString["TEMPID"] != null)
                {
                    dsData = dal_SAE.SAE_MODULE_DATA_SP(
                         MODULEID: drpModule.SelectedValue,
                         TABLENAME: hfTablename.Value,
                         SUBJID: Request.QueryString["SUBJID"].ToString(),
                         SAEID: Request.QueryString["TEMPID"].ToString(),
                         RECID: hdnRECID.Value
                         );

                    if (dsData.Tables[0].Rows.Count == 0)
                    {
                        if (Request.QueryString["DM_RECID"] != null)
                        {
                            dsData = dal_SAE.SAE_SYNC_MODULE_DATA_SP(
                                  DM_PVID: Convert.ToString(Request.QueryString["DM_PVID"]),
                                  DM_RECID: Convert.ToString(Request.QueryString["DM_RECID"]),
                                  MODULEID: drpModule.SelectedValue,
                                  TABLENAME: Convert.ToString(Request.QueryString["TABLENAME"])
                                  );
                        }
                    }
                }
                else
                {
                    if (Request.QueryString["DM_RECID"] != null)
                    {
                        dsData = dal_SAE.SAE_SYNC_MODULE_DATA_SP(
                            DM_PVID: Convert.ToString(Request.QueryString["DM_PVID"]),
                            DM_RECID: Convert.ToString(Request.QueryString["DM_RECID"]),
                            MODULEID: drpModule.SelectedValue,
                            TABLENAME: Convert.ToString(Request.QueryString["TABLENAME"])
                            );
                    }
                }



                DataSet ds = new DataSet();
                if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = GenerateTransposedTable(dsData.Tables[0]);
                    ds.Tables.Add(dt);
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        for (rownum = 0; rownum < grd.Rows.Count; rownum++)
                        {
                            COLNAME = ((Label)grd.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                            string CONTROLTYPE;
                            CONTROLTYPE = ((Label)grd.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                            string DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                            string PREFIX;
                            PREFIX = ((Label)grd.Rows[rownum].FindControl("lblPREFIXTEXT")).Text;

                            string REQUIREDYN;
                            REQUIREDYN = ((Label)grd.Rows[rownum].FindControl("lblREQUIREDYN")).Text;

                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                            if (DataVariableName == COLNAME)
                            {
                                ((HiddenField)grd.Rows[rownum].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                if (CONTROLTYPE == "TEXTBOX")
                                {
                                    if (COLVAL != "")
                                    {
                                        ((HiddenField)grd.Rows[rownum].FindControl("HDN_FIELD")).Value = COLVAL;
                                        ((TextBox)grd.Rows[rownum].FindControl("TXT_FIELD")).Text = COLVAL;
                                    }

                                    if (REQUIREDYN == "True")
                                    {
                                        //REQUIRED TRUE Or FALSE
                                        if (COLVAL == "")
                                        {
                                            ((TextBox)grd.Rows[rownum].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                        }
                                    }
                                }
                                else if (CONTROLTYPE == "DROPDOWN")
                                {
                                    COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                    ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                    ((HiddenField)grd.Rows[rownum].FindControl("HDN_FIELD")).Value = COLVAL;

                                    string Class = ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).CssClass;

                                    if (REQUIREDYN == "True")
                                    {
                                        //REQUIRED TRUE Or FALSE
                                        if (COLVAL == "")
                                        {
                                            ((DropDownList)grd.Rows[rownum].FindControl("DRP_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                        }
                                    }
                                }
                                else if (CONTROLTYPE == "CHECKBOX")
                                {
                                    string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');
                                    foreach (string x in stringArray)
                                    {
                                        Repeater repeat_RAD = grd.Rows[rownum].FindControl("repeat_CHK") as Repeater;
                                        for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                                        {
                                            if (x != "")
                                            {
                                                if (((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Text.ToString() == x)
                                                {
                                                    ((CheckBox)repeat_RAD.Items[ab].FindControl("CHK_FIELD")).Checked = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (CONTROLTYPE == "RADIOBUTTON")
                                {
                                    string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');
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

                            GridView grd_Data1 = grd.Rows[rownum].FindControl("grd_Data1") as GridView;

                            for (int a = 0; a < grd_Data1.Rows.Count; a++)
                            {
                                COLNAME = ((Label)grd_Data1.Rows[a].FindControl("lblVARIABLENAME")).Text;
                                CONTROLTYPE = ((Label)grd_Data1.Rows[a].FindControl("lblCONTROLTYPE")).Text;
                                DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                                REQUIREDYN = ((Label)grd_Data1.Rows[a].FindControl("lblREQUIREDYN")).Text;

                                if (DataVariableName == COLNAME)
                                {
                                    COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                    ((HiddenField)grd_Data1.Rows[a].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                    if (CONTROLTYPE == "TEXTBOX")
                                    {
                                        if (COLVAL != "")
                                        {
                                            ((HiddenField)grd_Data1.Rows[a].FindControl("HDN_FIELD")).Value = COLVAL;
                                            ((TextBox)grd_Data1.Rows[a].FindControl("TXT_FIELD")).Text = COLVAL;
                                        }

                                        if (REQUIREDYN == "True")
                                        {
                                            //REQUIRED TRUE Or FALSE
                                            if (COLVAL == "")
                                            {
                                                ((TextBox)grd_Data1.Rows[a].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                            }
                                        }
                                    }
                                    else if (CONTROLTYPE == "DROPDOWN")
                                    {
                                        COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                        ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                        ((HiddenField)grd_Data1.Rows[a].FindControl("HDN_FIELD")).Value = COLVAL;

                                        string Class = ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).CssClass;

                                        if (REQUIREDYN == "True")
                                        {
                                            //REQUIRED TRUE Or FALSE
                                            if (COLVAL == "")
                                            {
                                                ((DropDownList)grd_Data1.Rows[a].FindControl("DRP_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                            }
                                        }

                                    }
                                    else if (CONTROLTYPE == "CHECKBOX")
                                    {
                                        string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');
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
                                            }
                                        }
                                    }
                                    else if (CONTROLTYPE == "RADIOBUTTON")
                                    {
                                        string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');
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

                                GridView grd_Data2 = grd_Data1.Rows[a].FindControl("grd_Data2") as GridView;

                                for (int b = 0; b < grd_Data2.Rows.Count; b++)
                                {
                                    COLNAME = ((Label)grd_Data2.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                    CONTROLTYPE = ((Label)grd_Data2.Rows[b].FindControl("lblCONTROLTYPE")).Text;
                                    DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                                    REQUIREDYN = ((Label)grd_Data2.Rows[b].FindControl("lblREQUIREDYN")).Text;

                                    if (DataVariableName == COLNAME)
                                    {
                                        COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                        ((HiddenField)grd_Data2.Rows[b].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                        if (CONTROLTYPE == "TEXTBOX")
                                        {
                                            if (COLVAL != "")
                                            {
                                                ((HiddenField)grd_Data2.Rows[b].FindControl("HDN_FIELD")).Value = COLVAL;
                                                ((TextBox)grd_Data2.Rows[b].FindControl("TXT_FIELD")).Text = COLVAL;
                                            }

                                            if (REQUIREDYN == "True")
                                            {
                                                //REQUIRED TRUE Or FALSE
                                                if (COLVAL == "")
                                                {
                                                    ((TextBox)grd_Data2.Rows[b].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                }
                                            }
                                        }
                                        else if (CONTROLTYPE == "DROPDOWN")
                                        {
                                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                            ((HiddenField)grd_Data2.Rows[b].FindControl("HDN_FIELD")).Value = COLVAL;
                                            ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).SelectedValue = COLVAL;

                                            string Class = ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).CssClass;

                                            if (REQUIREDYN == "True")
                                            {
                                                //REQUIRED TRUE Or FALSE
                                                if (COLVAL == "")
                                                {
                                                    ((DropDownList)grd_Data2.Rows[b].FindControl("DRP_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                }
                                            }
                                        }
                                        else if (CONTROLTYPE == "CHECKBOX")
                                        {
                                            string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');
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
                                                }
                                            }
                                        }
                                        else if (CONTROLTYPE == "RADIOBUTTON")
                                        {
                                            string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');
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

                                    GridView grd_Data3 = grd_Data2.Rows[b].FindControl("grd_Data3") as GridView;

                                    for (int c = 0; c < grd_Data3.Rows.Count; c++)
                                    {
                                        COLNAME = ((Label)grd_Data3.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                        CONTROLTYPE = ((Label)grd_Data3.Rows[c].FindControl("lblCONTROLTYPE")).Text;
                                        DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();
                                        REQUIREDYN = ((Label)grd_Data3.Rows[c].FindControl("lblREQUIREDYN")).Text;

                                        if (DataVariableName == COLNAME)
                                        {
                                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                            ((HiddenField)grd_Data3.Rows[c].FindControl("HDN_OLD_VALUE")).Value = COLVAL;

                                            if (CONTROLTYPE == "TEXTBOX")
                                            {
                                                if (COLVAL != "")
                                                {
                                                    ((HiddenField)grd_Data3.Rows[c].FindControl("HDN_FIELD")).Value = COLVAL;
                                                    ((TextBox)grd_Data3.Rows[c].FindControl("TXT_FIELD")).Text = COLVAL;
                                                }

                                                if (REQUIREDYN == "True")
                                                {
                                                    //REQUIRED TRUE Or FALSE
                                                    if (COLVAL == "")
                                                    {
                                                        ((TextBox)grd_Data3.Rows[c].FindControl("TXT_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                    }
                                                }
                                            }
                                            else if (CONTROLTYPE == "DROPDOWN")
                                            {
                                                COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                                                ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).SelectedValue = COLVAL;
                                                ((HiddenField)grd_Data3.Rows[c].FindControl("HDN_FIELD")).Value = COLVAL;

                                                string Class = ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).CssClass;

                                                if (REQUIREDYN == "True")
                                                {
                                                    //REQUIRED TRUE Or FALSE
                                                    if (COLVAL == "")
                                                    {
                                                        ((DropDownList)grd_Data3.Rows[c].FindControl("DRP_FIELD")).BackColor = System.Drawing.Color.Yellow;
                                                    }
                                                }

                                            }
                                            else if (CONTROLTYPE == "CHECKBOX")
                                            {
                                                string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');
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
                                                    }
                                                }
                                            }
                                            else if (CONTROLTYPE == "RADIOBUTTON")
                                            {
                                                string[] stringArray = ds.Tables[0].Rows[i]["DATA"].ToString().Split('¸');
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

                    int SYNC_COUNT = Convert.ToInt32(dr["SYNC_COUNT"]);

                    if (SYNC_COUNT > 0)
                    {
                        READYN = "True";
                        CLASS = CLASS.Replace("txtDateNoFuture", "");
                        CLASS = CLASS.Replace("txtDate", "");
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
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
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
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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

                        if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();
                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }

                        if (Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (ChildLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME; }
                        else
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

                        //DataRow[] rows;
                        //rows = ds.Tables[0].Select("TEXT = 'Follow-up'");

                        //foreach (DataRow row in rows)
                        //    ds.Tables[0].Rows.Remove(row);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        //for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                        //{
                        //    ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).Checked = true;
                        //}

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
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

                    LinkButton AQ = (LinkButton)e.Row.FindControl("AQ");
                    AQ.CssClass += " AQ_" + VARIABLENAME + " disp-none";

                    LinkButton AWSQ = (LinkButton)e.Row.FindControl("AWSQ");
                    AWSQ.CssClass += " AWSQ_" + VARIABLENAME + " disp-none";

                    LinkButton CQ = (LinkButton)e.Row.FindControl("CQ");
                    CQ.CssClass += " CQ_" + VARIABLENAME + " disp-none";

                    LinkButton CM = (LinkButton)e.Row.FindControl("CM");
                    CM.CssClass += " CM_" + VARIABLENAME + " disp-none Comments";

                    LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                    AD.CssClass += " AD_" + VARIABLENAME + " disp-none";

                    GridView grd_Data1 = e.Row.FindControl("grd_Data1") as GridView;
                    DataSet ds1 = dal_SAE.SAE_STRUCTURE_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: drpModule.SelectedValue,
                        FIELDID: ID
                        );

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

                    int SYNC_COUNT = Convert.ToInt32(dr["SYNC_COUNT"]);

                    if (SYNC_COUNT > 0)
                    {
                        READYN = "True";
                        CLASS = CLASS.Replace("txtDateNoFuture", "");
                        CLASS = CLASS.Replace("txtDate", "");
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Row.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        //btnEdit.ID = VARIABLENAME;
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
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
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
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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

                        if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();

                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }

                        if (Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (ChildLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME; }
                        else
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
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

                    LinkButton AQ = (LinkButton)e.Row.FindControl("AQ");
                    AQ.CssClass += " AQ_" + VARIABLENAME + " disp-none";

                    LinkButton AWSQ = (LinkButton)e.Row.FindControl("AWSQ");
                    AWSQ.CssClass += " AWSQ_" + VARIABLENAME + " disp-none";

                    LinkButton CQ = (LinkButton)e.Row.FindControl("CQ");
                    CQ.CssClass += " CQ_" + VARIABLENAME + " disp-none";

                    LinkButton CM = (LinkButton)e.Row.FindControl("CM");
                    CM.CssClass += " CM_" + VARIABLENAME + " disp-none Comments";

                    LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                    AD.CssClass += " AD_" + VARIABLENAME + " disp-none";

                    GridView grd_Data2 = e.Row.FindControl("grd_Data2") as GridView;
                    DataSet ds1 = dal_SAE.SAE_STRUCTURE_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: drpModule.SelectedValue,
                        FIELDID: ID
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

                    int SYNC_COUNT = Convert.ToInt32(dr["SYNC_COUNT"]);

                    if (SYNC_COUNT > 0)
                    {
                        READYN = "True";
                        CLASS = CLASS.Replace("txtDateNoFuture", "");
                        CLASS = CLASS.Replace("txtDate", "");
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
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
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
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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

                        if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();

                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }

                        if (Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (ChildLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME; }
                        else
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
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

                    LinkButton AQ = (LinkButton)e.Row.FindControl("AQ");
                    AQ.CssClass += " AQ_" + VARIABLENAME + " disp-none";

                    LinkButton AWSQ = (LinkButton)e.Row.FindControl("AWSQ");
                    AWSQ.CssClass += " AWSQ_" + VARIABLENAME + " disp-none";

                    LinkButton CQ = (LinkButton)e.Row.FindControl("CQ");
                    CQ.CssClass += " CQ_" + VARIABLENAME + " disp-none";

                    LinkButton CM = (LinkButton)e.Row.FindControl("CM");
                    CM.CssClass += " CM_" + VARIABLENAME + " disp-none Comments";

                    LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                    AD.CssClass += " AD_" + VARIABLENAME + " disp-none";

                    GridView grd_Data3 = e.Row.FindControl("grd_Data3") as GridView;
                    DataSet ds1 = dal_SAE.SAE_STRUCTURE_SP(
                        ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: drpModule.SelectedValue,
                        FIELDID: ID
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

                    int SYNC_COUNT = Convert.ToInt32(dr["SYNC_COUNT"]);

                    if (SYNC_COUNT > 0)
                    {
                        READYN = "True";
                        CLASS = CLASS.Replace("txtDateNoFuture", "");
                        CLASS = CLASS.Replace("txtDate", "");
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
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
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
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = dr["PrefixText"].ToString();
                            }
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        HiddenField hfValue1 = (HiddenField)e.Row.FindControl("hfValue1");

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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

                        if (btnEdit.Text == "") { btnEdit.Text = DefaultData; }

                        btnEdit.CssClass = btnEdit.CssClass + " " + VARIABLENAME;

                        if (btnEdit.CssClass.Contains("numericdecimal"))
                        {
                            string FORMAT = dr["FORMAT"].ToString();

                            btnEdit.Attributes.Add("data-inputmask", "'mask': '" + FORMAT + "'");
                        }
                        if (Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (ChildLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " linked" + ParentLinkedVARIABLENAME; }
                        else
                        {
                            DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
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

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (DefaultData != "") { btnEdit.SelectedValue = DefaultData; }

                        if (ParentLinked == "True") { btnEdit.CssClass = btnEdit.CssClass + " ParentLinked"; }

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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
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

                        DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                               ACTION: "GET_OPTIONS_LIST",
                               VARIABLENAME: VARIABLENAME
                               );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                        if (READYN == "True" || Convert.ToString(Request.QueryString["DELETED"]) == "1")
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

                    LinkButton AQ = (LinkButton)e.Row.FindControl("AQ");
                    AQ.CssClass += " AQ_" + VARIABLENAME + " disp-none";

                    LinkButton AWSQ = (LinkButton)e.Row.FindControl("AWSQ");
                    AWSQ.CssClass += " AWSQ_" + VARIABLENAME + " disp-none";

                    LinkButton CQ = (LinkButton)e.Row.FindControl("CQ");
                    CQ.CssClass += " CQ_" + VARIABLENAME + " disp-none";

                    LinkButton CM = (LinkButton)e.Row.FindControl("CM");
                    CM.CssClass += " CM_" + VARIABLENAME + " disp-none Comments";

                    LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                    AD.CssClass += " AD_" + VARIABLENAME + " disp-none";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void InsertUpdatedata()
        {
            try
            {
                bool isColAdded = false, HasMissing = false;
                string COLUMN = "", varname = "", DATA = "", FieldName = "", INSERTQUERY = "", UPDATEQUERY = "", SYNC_COLUMN = "", COLUMN_Audit = "";

                for (int rownum = 0; rownum < grd_Data.Rows.Count; rownum++)
                {
                    string strdata = "";
                    varname = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                    string CONTROLTYPE;
                    CONTROLTYPE = ((Label)grd_Data.Rows[rownum].FindControl("lblCONTROLTYPE")).Text;
                    string REQUIREDYN;
                    REQUIREDYN = ((Label)grd_Data.Rows[rownum].FindControl("lblREQUIREDYN")).Text;
                    string READYN = ((Label)grd_Data.Rows[rownum].FindControl("READYN")).Text;
                    string SYNC_COUNT = ((Label)grd_Data.Rows[rownum].FindControl("SYNC_COUNT")).Text;

                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                    {
                        if (CONTROLTYPE == "TEXTBOX")
                        {
                            strdata = ((TextBox)grd_Data.Rows[rownum].FindControl("TXT_FIELD")).Text;
                        }
                        else if (CONTROLTYPE == "DROPDOWN")
                        {
                            strdata = ((DropDownList)grd_Data.Rows[rownum].FindControl("DRP_FIELD")).SelectedValue;
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

                        if (FieldName != "")
                        {
                            FieldName = FieldName + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblFieldName")).Text + "";
                        }
                        else
                        {
                            FieldName = ((Label)grd_Data.Rows[rownum].FindControl("lblFieldName")).Text;
                        }

                        if (SYNC_COUNT != "0")
                        {
                            if (SYNC_COLUMN != "")
                            {
                                SYNC_COLUMN = SYNC_COLUMN + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                            }
                            else
                            {
                                SYNC_COLUMN = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
                            }
                        }

                        if (READYN != "True")
                        {
                            if (COLUMN_Audit != "")
                            {
                                COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text + "";
                            }
                            else
                            {
                                COLUMN_Audit = ((Label)grd_Data.Rows[rownum].FindControl("lblVARIABLENAME")).Text;
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

                        if (REQUIREDYN == "True" && strdata.Trim() == "")
                        {
                            HasMissing = true;
                        }

                        if (DATA != "")
                        {
                            if (strdata != "")
                            {
                                DATA = DATA + " @ni$h '" + strdata + "'";
                            }
                            else
                            {
                                DATA = DATA + " @ni$h NULL";

                                strdata = "";
                            }
                        }
                        else
                        {
                            if (strdata != "")
                            {
                                DATA = "'" + strdata + "'";
                            }
                            else
                            {
                                DATA = "NULL";

                                strdata = "";
                            }
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
                        REQUIREDYN = ((Label)grd_Data1.Rows[b].FindControl("lblREQUIREDYN")).Text;
                        READYN = ((Label)grd_Data1.Rows[b].FindControl("READYN")).Text;
                        SYNC_COUNT = ((Label)grd_Data1.Rows[b].FindControl("SYNC_COUNT")).Text;

                        if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                        {
                            if (CONTROLTYPE == "TEXTBOX")
                            {
                                strdata1 = ((TextBox)grd_Data1.Rows[b].FindControl("TXT_FIELD")).Text;
                            }
                            else if (CONTROLTYPE == "DROPDOWN")
                            {
                                strdata1 = ((DropDownList)grd_Data1.Rows[b].FindControl("DRP_FIELD")).SelectedValue;
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

                            if (FieldName != "")
                            {
                                FieldName = FieldName + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblFieldName")).Text + "";
                            }
                            else
                            {
                                FieldName = ((Label)grd_Data1.Rows[b].FindControl("lblFieldName")).Text;
                            }

                            if (SYNC_COUNT != "0")
                            {
                                if (SYNC_COLUMN != "")
                                {
                                    SYNC_COLUMN = SYNC_COLUMN + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                }
                                else
                                {
                                    SYNC_COLUMN = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
                                }
                            }

                            if (READYN != "True")
                            {
                                if (COLUMN_Audit != "")
                                {
                                    COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text + "";
                                }
                                else
                                {
                                    COLUMN_Audit = ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text;
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

                                    if (REQUIREDYN == "True" && strdata1.Trim() == "")
                                    {
                                        HasMissing = true;
                                    }

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
                                            DATA = DATA + " @ni$h '" + strdata1 + "'";
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
                                            DATA = "'" + strdata1 + "'";
                                        }
                                        else
                                        {
                                            DATA = "NULL";

                                            strdata1 = "";
                                        }
                                    }
                                }
                            }

                            if (((Label)grd_Data1.Rows[b].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data1.Rows[b].FindControl("AutoNum")).Text == "False")
                            {
                                if (!isColAdded)
                                {
                                    if (strdata1 != "")
                                    {
                                        ADD_NEW_ROW_DATA(((Label)grd_Data1.Rows[b].FindControl("lblFieldName")).Text,
                                            ((Label)grd_Data1.Rows[b].FindControl("lblVARIABLENAME")).Text,
                                            strdata1.Replace("N'", "").Replace("'", ""),
                                            DBNull.Value.ToString(),
                                            "Parent Field Updated",
                                            DBNull.Value.ToString(),
                                            hdnRECID.Value
                                            );
                                    }

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
                        }

                        GridView grd_Data2 = grd_Data1.Rows[b].FindControl("grd_Data2") as GridView;

                        for (int c = 0; c < grd_Data2.Rows.Count; c++)
                        {
                            string Val_Child1 = ((Label)grd_Data2.Rows[c].FindControl("lblVal_Child")).Text;
                            string strdata2 = "";
                            varname = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                            CONTROLTYPE = ((Label)grd_Data2.Rows[c].FindControl("lblCONTROLTYPE")).Text;
                            REQUIREDYN = ((Label)grd_Data2.Rows[c].FindControl("lblREQUIREDYN")).Text;
                            READYN = ((Label)grd_Data2.Rows[c].FindControl("READYN")).Text;
                            SYNC_COUNT = ((Label)grd_Data2.Rows[c].FindControl("SYNC_COUNT")).Text;

                            if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                            {
                                if (CONTROLTYPE == "TEXTBOX")
                                {
                                    strdata2 = ((TextBox)grd_Data2.Rows[c].FindControl("TXT_FIELD")).Text;
                                }
                                else if (CONTROLTYPE == "DROPDOWN")
                                {
                                    strdata2 = ((DropDownList)grd_Data2.Rows[c].FindControl("DRP_FIELD")).SelectedValue;
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

                                if (FieldName != "")
                                {
                                    FieldName = FieldName + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblFieldName")).Text + "";
                                }
                                else
                                {
                                    FieldName = ((Label)grd_Data2.Rows[c].FindControl("lblFieldName")).Text;
                                }

                                if (SYNC_COUNT != "0")
                                {
                                    if (SYNC_COLUMN != "")
                                    {
                                        SYNC_COLUMN = SYNC_COLUMN + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                    }
                                    else
                                    {
                                        SYNC_COLUMN = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
                                    }
                                }

                                if (READYN != "True")
                                {
                                    if (COLUMN_Audit != "")
                                    {
                                        COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text + "";
                                    }
                                    else
                                    {
                                        COLUMN_Audit = ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text;
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

                                        if (REQUIREDYN == "True" && strdata1.Trim() == "")
                                        {
                                            HasMissing = true;
                                        }

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
                                                DATA = DATA + " @ni$h '" + strdata2 + "'";
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
                                                DATA = "'" + strdata2 + "'";
                                            }
                                            else
                                            {
                                                DATA = "NULL";

                                                strdata2 = "";
                                            }
                                        }
                                    }
                                }

                                if (((Label)grd_Data2.Rows[c].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data2.Rows[c].FindControl("AutoNum")).Text == "False")
                                {
                                    if (!isColAdded)
                                    {
                                        if (strdata2 != "")
                                        {
                                            ADD_NEW_ROW_DATA(((Label)grd_Data2.Rows[c].FindControl("lblFieldName")).Text,
                                            ((Label)grd_Data2.Rows[c].FindControl("lblVARIABLENAME")).Text,
                                            strdata2.Replace("N'", "").Replace("'", ""),
                                            DBNull.Value.ToString(),
                                            "Parent Field Updated",
                                            DBNull.Value.ToString(),
                                            hdnRECID.Value
                                            );
                                        }

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
                            }

                            GridView grd_Data3 = grd_Data2.Rows[c].FindControl("grd_Data3") as GridView;

                            for (int d = 0; d < grd_Data3.Rows.Count; d++)
                            {
                                string Val_Child2 = ((Label)grd_Data3.Rows[d].FindControl("lblVal_Child")).Text;
                                string strdata3 = "";
                                varname = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                CONTROLTYPE = ((Label)grd_Data3.Rows[d].FindControl("lblCONTROLTYPE")).Text;
                                REQUIREDYN = ((Label)grd_Data3.Rows[d].FindControl("lblREQUIREDYN")).Text;
                                READYN = ((Label)grd_Data3.Rows[d].FindControl("READYN")).Text;
                                SYNC_COUNT = ((Label)grd_Data3.Rows[d].FindControl("SYNC_COUNT")).Text;

                                if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                                {
                                    if (CONTROLTYPE == "TEXTBOX")
                                    {
                                        strdata3 = ((TextBox)grd_Data3.Rows[d].FindControl("TXT_FIELD")).Text;
                                    }
                                    else if (CONTROLTYPE == "DROPDOWN")
                                    {
                                        strdata3 = ((DropDownList)grd_Data3.Rows[d].FindControl("DRP_FIELD")).SelectedValue;
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

                                    if (SYNC_COUNT != "0")
                                    {
                                        if (SYNC_COLUMN != "")
                                        {
                                            SYNC_COLUMN = SYNC_COLUMN + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                        }
                                        else
                                        {
                                            SYNC_COLUMN = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                        }
                                    }

                                    if (READYN != "True")
                                    {
                                        if (COLUMN_Audit != "")
                                        {
                                            COLUMN_Audit = COLUMN_Audit + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text + "";
                                        }
                                        else
                                        {
                                            COLUMN_Audit = ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text;
                                        }
                                    }

                                    if (FieldName != "")
                                    {
                                        FieldName = FieldName + " @ni$h " + ((Label)grd_Data3.Rows[d].FindControl("lblFieldName")).Text + "";
                                    }
                                    else
                                    {
                                        FieldName = ((Label)grd_Data3.Rows[d].FindControl("lblFieldName")).Text;
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

                                            if (REQUIREDYN == "True" && strdata2.Trim() == "")
                                            {
                                                HasMissing = true;
                                            }

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
                                                    DATA = DATA + " @ni$h '" + strdata3 + "'";
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
                                                    DATA = "'" + strdata3 + "'";
                                                }
                                                else
                                                {
                                                    DATA = "NULL";

                                                    strdata3 = "";
                                                }
                                            }
                                        }
                                    }

                                    if (((Label)grd_Data3.Rows[d].FindControl("lblPREFIXTEXT")).Text == "" && ((Label)grd_Data3.Rows[d].FindControl("AutoNum")).Text == "False")
                                    {
                                        if (!isColAdded)
                                        {
                                            if (strdata3 != "")
                                            {
                                                ADD_NEW_ROW_DATA(((Label)grd_Data3.Rows[d].FindControl("lblFieldName")).Text,
                                                    ((Label)grd_Data3.Rows[d].FindControl("lblVARIABLENAME")).Text,
                                                    strdata3.Replace("N'", "").Replace("'", ""),
                                                    DBNull.Value.ToString(),
                                                    "Parent Field Updated",
                                                    DBNull.Value.ToString(),
                                                    hdnRECID.Value
                                                    );
                                            }

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

                if (Request.QueryString["TEMPID"] == null)
                {
                    hdnSAEID.Value = GET_TEMP_ID();
                }
                else
                {
                    hdnSAEID.Value = Request.QueryString["TEMPID"].ToString();
                }

                if (hdnSAEID.Value != "")
                {
                    INSERTQUERY = "INSERT INTO [" + hfTablename.Value + "] ([SAEID], [SUBJID_DATA], [INVID], [SAE], [STATUS], [RECID], [ENTEREDBY], [ENTEREDBYNAME], [ENTEREDDAT], [ENTERED_TZVAL], " + COLUMN.Replace("@ni$h", ",") + ") VALUES ('" + hdnSAEID.Value + "','" + Request.QueryString["SUBJID"].ToString() + "', '" + Request.QueryString["INVID"].ToString() + "', NULL, '" + "Initial; Incomplete" + "','" + hdnRECID.Value + "','" + Session["USER_ID"].ToString() + "', '" + Session["User_Name"].ToString() + "', GETDATE(), '" + Session["TimeZone_Value"].ToString() + "', " + DATA.Replace("@ni$h", ",") + ")";

                    string[] colArr = COLUMN.Split(new string[] { " @ni$h " }, StringSplitOptions.None);
                    string[] dataArr = DATA.Split(new string[] { " @ni$h " }, StringSplitOptions.None);
                    string[] fieldArr = FieldName.Split(new string[] { "@ni$h" }, StringSplitOptions.None);
                    string[] colArr_Sync = SYNC_COLUMN.Trim().Split(new string[] { " @ni$h " }, StringSplitOptions.None);
                    string[] colArr_Audit = COLUMN_Audit.Split(new string[] { " @ni$h " }, StringSplitOptions.None);

                    string SyncedVariableNames = "";

                    for (int i = 0; i < colArr.Length; i++)
                    {
                        if (UPDATEQUERY == "")
                        {
                            UPDATEQUERY = "UPDATE [" + hfTablename.Value + "] SET UPDATEDDAT = GETDATE(), UPDATEDBYNAME = '" + Session["User_Name"].ToString() + "', UPDATED_TZVAL = '" + Session["TimeZone_Value"].ToString() + "', UPDATEDBY = '" + Session["USER_ID"].ToString() + "' ";

                            UPDATEQUERY = UPDATEQUERY + ", " + colArr[i] + " = " + dataArr[i] + " ";
                        }
                        else
                        {
                            UPDATEQUERY = UPDATEQUERY + ", " + colArr[i] + " = " + dataArr[i] + " ";
                        }

                        if (colArr_Sync.Contains(colArr[i].Trim()))
                        {
                            if (SyncedVariableNames == "")
                            {
                                SyncedVariableNames = colArr[i];
                            }
                            else
                            {
                                SyncedVariableNames = SyncedVariableNames + ", " + colArr[i];
                            }
                        }
                        else
                        {
                            if (hdn_PAGESTATUS.Value == "0")
                            {
                                if (colArr_Audit.Contains(colArr[i].Trim()))
                                {
                                    ADD_NEW_ROW_DATA(fieldArr[i], colArr[i], "", dataArr[i].Replace("N'", "").Replace("'", ""), "Initial Entry", "", hdnRECID.Value);
                                }
                            }
                        }
                    }

                    UPDATEQUERY = UPDATEQUERY + " WHERE SAEID = '" + hdnSAEID.Value + "' AND RECID = '" + hdnRECID.Value + "' AND SUBJID_DATA = '" + Request.QueryString["SUBJID"].ToString() + "'";

                    dal_SAE.SAE_SYNC_AUDITTRAIL_SP(
                        SAEID: hdnSAEID.Value,
                        RECID: hdnRECID.Value,
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        STATUS: "Initial; Incomplete",
                        MODULEID: drpModule.SelectedValue,
                        DM_PVID: Convert.ToString(Request.QueryString["DM_PVID"]),
                        DM_RECID: Convert.ToString(Request.QueryString["DM_RECID"]),
                        SYNCED_VARIABLENAMES: SyncedVariableNames
                        );

                    if (SAE_AuditTrail.Rows.Count > 0)
                    {
                        SqlConnection con = new SqlConnection(dal.getconstr());

                        using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                        {
                            if (con.State != ConnectionState.Open) { con.Open(); }

                            sqlbc.DestinationTableName = "SAE_AUDITTRAIL";

                            sqlbc.ColumnMappings.Add("SAEID", "SAEID");
                            sqlbc.ColumnMappings.Add("RECID", "RECID");
                            sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                            sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                            sqlbc.ColumnMappings.Add("MODULENAME", "MODULENAME");
                            sqlbc.ColumnMappings.Add("FIELDNAME", "FIELDNAME");
                            sqlbc.ColumnMappings.Add("TABLENAME", "TABLENAME");
                            sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                            sqlbc.ColumnMappings.Add("OLDVALUE", "OLDVALUE");
                            sqlbc.ColumnMappings.Add("NEWVALUE", "NEWVALUE");
                            sqlbc.ColumnMappings.Add("REASON", "REASON");
                            sqlbc.ColumnMappings.Add("SOURCE", "SOURCE");
                            sqlbc.ColumnMappings.Add("COMMENTS", "COMMENTS");
                            sqlbc.ColumnMappings.Add("STATUS", "STATUS");
                            sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                            sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                            sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                            sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");
                            sqlbc.ColumnMappings.Add("MR", "MR");

                            sqlbc.WriteToServer(SAE_AuditTrail);

                            if (hdn_PAGESTATUS.Value == "1")
                            {
                                ADD_UPDATED_AT_ENTRY_LOGS(SAE_AuditTrail, hdnRECID.Value);
                            }

                            SAE_AuditTrail.Clear();
                        }
                    }

                    dal_SAE.SAE_INSERT_MODULE_DATA_SP(
                         ACTION: "INSERT_MODULE_DATA",
                         TABLENAME: hfTablename.Value,
                         SUBJID: Request.QueryString["SUBJID"].ToString(),
                         INSERTQUERY: INSERTQUERY,
                         UPDATEQUERY: UPDATEQUERY,
                         SAEID: hdnSAEID.Value,
                         RECID: hdnRECID.Value,
                         IsComplete: 1,
                         IsMissing: HasMissing,
                         PAGESTATUS: "1",
                         DM_PVID: Convert.ToString(Request.QueryString["DM_PVID"]),
                         DM_RECID: Convert.ToString(Request.QueryString["DM_RECID"]),
                         MODULEID: drpModule.SelectedValue,
                         SAE: hdnSAE.Value,
                         INVID: Request.QueryString["INVID"].ToString(),
                         STATUS: "Initial; Incomplete",
                         MODULENAME: drpModule.SelectedItem.Text
                     );

                    dal_SAE.SAE_INSERT_PV_SP(
                        ACTION: "INSERT_MODULE_LOGS",
                        SAEID: hdnSAEID.Value,
                        RECID: hdnRECID.Value,
                        SAE: hdnSAE.Value,
                        INVID: Request.QueryString["INVID"].ToString(),
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        STATUS: "Initial; Incomplete",
                        MODULEID: drpModule.SelectedValue,
                        HasMissing: HasMissing,
                        IsComplete: 1,
                        PAGESTATUS: "1",
                        DM_PVID: Convert.ToString(Request.QueryString["DM_PVID"]),
                        DM_RECID: Convert.ToString(Request.QueryString["DM_RECID"])
                        );

                    dal_SAE.SAE_INSERT_MODULE_DATA_SP(ACTION: "UPDATE_DM_PVID_RECID",
                           SAEID: hdnSAEID.Value,
                           RECID: hdnRECID.Value,
                           MODULEID: drpModule.SelectedValue,
                           TABLENAME: hfTablename.Value
                           );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ADD_UPDATED_AT_ENTRY_LOGS(DataTable dt, string RECID)
        {
            try
            {
                DataColumn dtColumn;
                DataTable SAE_ACTIVITY_LOGS = new DataTable("SAE_ACTIVITY_LOGS");

                if (SAE_ACTIVITY_LOGS.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    SAE_ACTIVITY_LOGS.Columns.Add("SAEID");
                    SAE_ACTIVITY_LOGS.Columns.Add("RECID");
                    SAE_ACTIVITY_LOGS.Columns.Add("SAE");
                    SAE_ACTIVITY_LOGS.Columns.Add("INVID");
                    SAE_ACTIVITY_LOGS.Columns.Add("SUBJID");
                    SAE_ACTIVITY_LOGS.Columns.Add("STATUS");
                    SAE_ACTIVITY_LOGS.Columns.Add("MODULEID");
                    SAE_ACTIVITY_LOGS.Columns.Add("MODULENAME");
                    SAE_ACTIVITY_LOGS.Columns.Add("FIELDNAME");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_TYPE");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_PERFORMED");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_DETAILS");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_BY");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_BYNAME");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_DAT");
                    SAE_ACTIVITY_LOGS.Columns.Add("ACT_TZVAL");
                }

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow myDataRow;
                    myDataRow = SAE_ACTIVITY_LOGS.NewRow();
                    myDataRow["SAEID"] = hdnSAEID.Value;
                    myDataRow["RECID"] = RECID;
                    myDataRow["SAE"] = hdnSAE.Value;
                    myDataRow["INVID"] = Request.QueryString["INVID"].ToString();
                    myDataRow["SUBJID"] = Request.QueryString["SUBJID"].ToString();
                    myDataRow["STATUS"] = "Initial; Incomplete";
                    myDataRow["MODULEID"] = drpModule.SelectedValue;
                    myDataRow["MODULENAME"] = drpModule.SelectedItem.Text;
                    myDataRow["FIELDNAME"] = dt.Rows[j]["FIELDNAME"].ToString().Trim();
                    myDataRow["ACT_TYPE"] = "Data Entry";
                    myDataRow["ACT_PERFORMED"] = dt.Rows[j]["Reason"].ToString().Trim();
                    myDataRow["ACT_DETAILS"] = REMOVEHTML(dt.Rows[j]["NEWVALUE"].ToString()).Replace("N'", "").Replace("'", "").Trim();
                    myDataRow["ACT_BY"] = Session["User_ID"].ToString();
                    myDataRow["ACT_BYNAME"] = Session["User_Name"].ToString();
                    myDataRow["ACT_DAT"] = DateTime.Now;
                    myDataRow["ACT_TZVAL"] = Session["TimeZone_Value"].ToString();

                    SAE_ACTIVITY_LOGS.Rows.Add(myDataRow);
                }

                //Insert Bulk Audit Trail dataset
                if (SAE_ACTIVITY_LOGS.Rows.Count > 0)
                {
                    SqlConnection con = new SqlConnection(dal.getconstr());

                    using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), SqlBulkCopyOptions.KeepIdentity))
                    {
                        if (con.State != ConnectionState.Open) { con.Open(); }

                        sqlbc.DestinationTableName = "SAE_ACTIVITY_LOGS";

                        sqlbc.ColumnMappings.Add("SAEID", "SAEID");
                        sqlbc.ColumnMappings.Add("RECID", "RECID");
                        sqlbc.ColumnMappings.Add("SAE", "SAE");
                        sqlbc.ColumnMappings.Add("INVID", "INVID");
                        sqlbc.ColumnMappings.Add("SUBJID", "SUBJID");
                        sqlbc.ColumnMappings.Add("STATUS", "STATUS");
                        sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                        sqlbc.ColumnMappings.Add("MODULENAME", "MODULENAME");
                        sqlbc.ColumnMappings.Add("FIELDNAME", "FIELDNAME");
                        sqlbc.ColumnMappings.Add("ACT_TYPE", "ACT_TYPE");
                        sqlbc.ColumnMappings.Add("ACT_PERFORMED", "ACT_PERFORMED");
                        sqlbc.ColumnMappings.Add("ACT_DETAILS", "ACT_DETAILS");
                        sqlbc.ColumnMappings.Add("ACT_BY", "ACT_BY");
                        sqlbc.ColumnMappings.Add("ACT_BYNAME", "ACT_BYNAME");
                        sqlbc.ColumnMappings.Add("ACT_DAT", "ACT_DAT");
                        sqlbc.ColumnMappings.Add("ACT_TZVAL", "ACT_TZVAL");

                        sqlbc.WriteToServer(SAE_ACTIVITY_LOGS);

                        SAE_ACTIVITY_LOGS.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected string GET_TEMP_ID()
        {
            string TEMPID = "";
            try
            {
                DataSet ds = dal_SAE.SAE_INSERT_PV_SP(ACTION: "GET_TEMP_ID",
                     INVID: Request.QueryString["INVID"].ToString(),
                     SUBJID: Request.QueryString["SUBJID"].ToString(),
                     DM_PVID: Convert.ToString(Request.QueryString["DM_PVID"]),
                     DM_RECID: Convert.ToString(Request.QueryString["DM_RECID"]),
                     STATUS: "Initial; Incomplete",
                     TABLENAME: Convert.ToString(Request.QueryString["TABLENAME"])
                     );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    TEMPID = ds.Tables[0].Rows[0]["ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return TEMPID;
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

        protected void bntSaveComplete_Click(object sender, EventArgs e)
        {
            try
            {
                hdnNextClick.Value = hdnRECID.Value;

                InsertUpdatedata();

                if (btnNext.Visible == false)
                {
                    DataSet dsduplicate = dal_SAE.SAE_CHECK_DUPLICATE_SP(
                                        TEMPID: hdnSAEID.Value,
                                        SUBJID: Request.QueryString["SUBJID"].ToString()
                                        );

                    if (dsduplicate.Tables[0].Rows.Count < 1)
                    {
                        DataSet ds = dal_SAE.SAE_GENERAL_SP(
                                    ACTION: "GET_LAST_SAEID",
                                    SUBJID: Request.QueryString["SUBJID"].ToString()
                                    );

                        string SAE = ds.Tables[0].Rows[0]["SAE"].ToString();

                        string SAEID = Request.QueryString["INVID"].ToString() + "-" + Request.QueryString["SUBJID"].ToString() + "-" + SAE;

                        dal_SAE.SAE_UPDATE_TEMPID_SP(
                                            SAE: SAE,
                                            SAEID: SAEID,
                                            RECID: hdnRECID.Value,
                                            TEMPID: hdnSAEID.Value,
                                            DM_PVID: Convert.ToString(Request.QueryString["DM_PVID"]),
                                            DM_RECID: Convert.ToString(Request.QueryString["DM_RECID"]),
                                            TABLENAME: Convert.ToString(Request.QueryString["TABLENAME"]),
                                            INVID: Request.QueryString["INVID"].ToString(),
                                            SUBJID: Request.QueryString["SUBJID"].ToString(),
                                            MODULEID: drpModule.SelectedValue,
                                            MODULENAME: drpModule.SelectedItem.Text
                                           );

                        SendEmail(SAEID);

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('New SAE created Successfully'); window.location='NSAE_LOGGED_EVENTS_LIST.aspx';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('This SAE is already Exists,Please confirm data and create again.'); window.location='SafetyDashboard.aspx';", true);
                    }
                }
                else
                {
                    string MSG = "";

                    if (Request.QueryString["DM_PVID"] != null)
                    {
                        MSG = "NSAE_NEW_LOG.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&DM_PVID=" + Convert.ToString(Request.QueryString["DM_PVID"]) + "&DM_RECID=" + Convert.ToString(Request.QueryString["DM_RECID"]) + "&TEMPID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue + "&TABLENAME=" + Convert.ToString(Request.QueryString["TABLENAME"]) + "&LISTID=" + Convert.ToString(Request.QueryString["LISTID"]) + "&LISTNAME=" + Convert.ToString(Request.QueryString["LISTNAME"]);
                    }
                    else
                    {
                        MSG = "NSAE_NEW_LOG.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&TEMPID=" + hdnSAEID.Value + "&MODULEID=" + drpModule.SelectedValue + "&LISTID=" + Convert.ToString(Request.QueryString["LISTID"]) + "&LISTNAME=" + Convert.ToString(Request.QueryString["LISTNAME"]);
                    }

                    Response.Write("<script> alert('Record Updated successfully.'); window.location.href='" + MSG + "' </script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                string MSG = "";

                if (Request.QueryString["DM_PVID"] != null)
                {
                    MSG = "NSAE_NEW_LOG.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&DM_PVID=" + Convert.ToString(Request.QueryString["DM_PVID"]) + "&DM_RECID=" + Convert.ToString(Request.QueryString["DM_RECID"]) + "&TEMPID=" + hdnSAEID.Value + "&MODULEID=" + hdnPrevValue.Value + "&TABLENAME=" + Convert.ToString(Request.QueryString["TABLENAME"]) + "&LISTID=" + Convert.ToString(Request.QueryString["LISTID"]) + "&LISTNAME=" + Convert.ToString(Request.QueryString["LISTNAME"]);
                }

                else
                {
                    MSG = "NSAE_NEW_LOG.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&TEMPID=" + hdnSAEID.Value + "&MODULEID=" + hdnPrevValue.Value;
                }

                Response.Redirect(MSG);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                hdnNextClick.Value = "1";
                InsertUpdatedata();

                string MSG = "";

                if (Request.QueryString["DM_PVID"] != null)
                {
                    MSG = "NSAE_NEW_LOG.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&DM_PVID=" + Convert.ToString(Request.QueryString["DM_PVID"]) + "&DM_RECID=" + Convert.ToString(Request.QueryString["DM_RECID"]) + "&TEMPID=" + hdnSAEID.Value + "&MODULEID=" + hdnnextvalue.Value + "&TABLENAME=" + Convert.ToString(Request.QueryString["TABLENAME"]) + "&LISTID=" + Convert.ToString(Request.QueryString["LISTID"]) + "&LISTNAME=" + Convert.ToString(Request.QueryString["LISTNAME"]);
                }
                else
                {
                    MSG = "NSAE_NEW_LOG.aspx?INVID=" + Request.QueryString["INVID"].ToString() + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "&TEMPID=" + hdnSAEID.Value + "&MODULEID=" + hdnnextvalue.Value + "&LISTID=" + Convert.ToString(Request.QueryString["LISTID"]) + "&LISTNAME=" + Convert.ToString(Request.QueryString["LISTNAME"]);
                }

                Response.Redirect(MSG);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SendEmail(string SAEID)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_GET_EMAIL_DATA_SP(
                        INVID: Request.QueryString["INVID"].ToString(),
                        SAEID: SAEID,
                        ACTIONS: hdnActions.Value
                        );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string EMAILID = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();

                    string EMAIL_CC = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();

                    string EMAIL_BCC = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();

                    string EmailSubject = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

                    string EmailBody = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();

                    if (EmailSubject.Contains("[") && EmailSubject.Contains("]"))
                    {
                        if (EmailSubject.Contains("[SUBJID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[SUBJID]", Request.QueryString["SUBJID"].ToString());
                        }

                        if (EmailSubject.Contains("[PROJECTID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                        }

                        if (EmailSubject.Contains("[SITEID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[SITEID]", Request.QueryString["INVID"].ToString());
                        }

                        if (EmailSubject.Contains("[SAEID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[SAEID]", SAEID);
                        }

                        if (EmailSubject.Contains("[USER]"))
                        {
                            EmailSubject = EmailSubject.Replace("[USER]", Session["User_Name"].ToString());
                        }

                        if (EmailSubject.Contains("[DATETIME]"))
                        {
                            EmailSubject = EmailSubject.Replace("[DATETIME]", comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                        }

                        if (EmailSubject.Contains("[") && EmailSubject.Contains("]"))
                        {
                            if (ds.Tables.Count > 1)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    foreach (DataColumn dc in ds.Tables[1].Columns)
                                    {
                                        if (EmailSubject.Contains("[" + dc.ToString() + "]"))
                                        {
                                            EmailSubject = EmailSubject.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (EmailBody.Contains("[") && EmailBody.Contains("]"))
                    {
                        if (EmailBody.Contains("[SUBJID]"))
                        {
                            EmailBody = EmailBody.Replace("[SUBJID]", Request.QueryString["SUBJID"].ToString());
                        }

                        if (EmailBody.Contains("[PROJECTID]"))
                        {
                            EmailBody = EmailBody.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                        }

                        if (EmailBody.Contains("[SITEID]"))
                        {
                            EmailBody = EmailBody.Replace("[SITEID]", Request.QueryString["INVID"].ToString());
                        }

                        if (EmailBody.Contains("[SAEID]"))
                        {
                            EmailBody = EmailBody.Replace("[SAEID]", SAEID);
                        }

                        if (EmailBody.Contains("[USER]"))
                        {
                            EmailBody = EmailBody.Replace("[USER]", Session["User_Name"].ToString());
                        }

                        if (EmailBody.Contains("[DATETIME]"))
                        {
                            EmailBody = EmailBody.Replace("[DATETIME]", comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                        }

                        if (EmailBody.Contains("[") && EmailBody.Contains("]"))
                        {
                            if (ds.Tables.Count > 1)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    foreach (DataColumn dc in ds.Tables[1].Columns)
                                    {
                                        if (EmailBody.Contains("[" + dc.ToString() + "]"))
                                        {
                                            EmailBody = EmailBody.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }

                    comm.Email_Users(EMAILID, EMAIL_CC, EmailSubject, EmailBody, EMAIL_BCC);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;

            }
        }

        protected void ADD_NEW_ROW_DATA(string FieldName, string VariableName, string OldValue, string NewValue, string Reason, string Comment, string RECID)
        {
            try
            {
                CREATE_DM_AUDITTRAIL_DT();

                DataRow myDataRow;
                myDataRow = SAE_AuditTrail.NewRow();
                myDataRow["SAEID"] = hdnSAEID.Value;
                myDataRow["RECID"] = RECID;
                myDataRow["SUBJID"] = Request.QueryString["SUBJID"].ToString();
                myDataRow["MODULEID"] = drpModule.SelectedValue;
                myDataRow["MODULENAME"] = drpModule.SelectedItem.Text;
                myDataRow["FIELDNAME"] = FieldName.Trim();
                myDataRow["TABLENAME"] = hfTablename.Value;
                myDataRow["VARIABLENAME"] = VariableName.Trim();
                myDataRow["OLDVALUE"] = REMOVEHTML(OldValue).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["NEWVALUE"] = REMOVEHTML(NewValue).Replace("N'", "").Replace("'", "").Trim();
                myDataRow["REASON"] = Reason;
                myDataRow["SOURCE"] = "Pharmacovigilance";
                myDataRow["COMMENTS"] = Comment;
                myDataRow["STATUS"] = "Initial; Incomplete";
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                myDataRow["MR"] = false;
                SAE_AuditTrail.Rows.Add(myDataRow);
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

                if (SAE_AuditTrail.Columns.Count == 0)
                {
                    dtColumn = new DataColumn();
                    SAE_AuditTrail.Columns.Add("SAEID");
                    SAE_AuditTrail.Columns.Add("RECID");
                    SAE_AuditTrail.Columns.Add("SUBJID");
                    SAE_AuditTrail.Columns.Add("MODULEID");
                    SAE_AuditTrail.Columns.Add("MODULENAME");
                    SAE_AuditTrail.Columns.Add("FIELDNAME");
                    SAE_AuditTrail.Columns.Add("TABLENAME");
                    SAE_AuditTrail.Columns.Add("VARIABLENAME");
                    SAE_AuditTrail.Columns.Add("OLDVALUE");
                    SAE_AuditTrail.Columns.Add("NEWVALUE");
                    SAE_AuditTrail.Columns.Add("REASON");
                    SAE_AuditTrail.Columns.Add("SOURCE");
                    SAE_AuditTrail.Columns.Add("COMMENTS");
                    SAE_AuditTrail.Columns.Add("STATUS");
                    SAE_AuditTrail.Columns.Add("ENTEREDBY");
                    SAE_AuditTrail.Columns.Add("ENTEREDBYNAME");
                    SAE_AuditTrail.Columns.Add("ENTEREDDAT");
                    SAE_AuditTrail.Columns.Add("ENTERED_TZVAL");
                    SAE_AuditTrail.Columns.Add("MR");
                }
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

        public void SAE_GetAuditDetails()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_AUDITTRAIL_SP
                      (
                      ACTION: "GET_AUDITTRAIL_SAEID_RECID",
                      SAEID: hdnSAEID.Value,
                      RECID: hdnRECID.Value,
                      MODULEID: drpModule.SelectedValue
                      );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SAE_grdAUDITTRAILDETAILS.DataSource = ds;
                    SAE_grdAUDITTRAILDETAILS.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SAE_GetCommentsetails()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_COMMENT_SP(ACTION: "GET_COMMENTS_COUNT_SAEID_RECID",
                  SAEID: hdnSAEID.Value,
                  RECID: hdnRECID.Value
                  );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SAE_grdComments.DataSource = ds;
                    SAE_grdComments.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnBacktoHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NSAE_LISTING_DATA.aspx?LISTID=" + Convert.ToString(Request.QueryString["LISTID"]) + "&LISTNAME=" + Convert.ToString(Request.QueryString["LISTNAME"]), false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DATA_Changed(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

            txt_TableName.Text = hfTablename.Value;
            txt_VariableName.Text = (row.FindControl("lblVARIABLENAME") as Label).Text;
            txt_ModuleName.Text = lblModuleName.Text;
            txt_FieldName.Text = (row.FindControl("lblFieldName") as Label).Text;
            txt_OldValue.Text = (row.FindControl("HDN_OLD_VALUE") as HiddenField).Value;

            string CONTROLTYPE = (row.FindControl("lblCONTROLTYPE") as Label).Text;
            string NEWVALUE = "";
            switch (CONTROLTYPE)
            {
                case "TEXTBOX":
                    NEWVALUE = (row.FindControl("TXT_FIELD") as TextBox).Text;
                    break;
                case "DROPDOWN":
                    NEWVALUE = (row.FindControl("DRP_FIELD") as DropDownList).Text;
                    break;
                case "RADIOBUTTON":
                    Repeater repeaterRAD = (row.FindControl("repeat_RAD") as Repeater);
                    foreach (RepeaterItem repeaterItem in repeaterRAD.Items)
                    {
                        if ((repeaterItem.FindControl("RAD_FIELD") as RadioButton).Checked)
                        {
                            NEWVALUE = (repeaterItem.FindControl("RAD_FIELD") as RadioButton).Text;
                        }
                    }
                    break;
                case "CHECKBOX":
                    Repeater repeater = (row.FindControl("repeat_CHK") as Repeater);
                    foreach (RepeaterItem repeaterItem in repeater.Items)
                    {
                        if ((repeaterItem.FindControl("CHK_FIELD") as CheckBox).Checked)
                        {
                            if (NEWVALUE != "")
                            {
                                NEWVALUE += "¸" + (repeaterItem.FindControl("CHK_FIELD") as CheckBox).Text;
                            }
                            else
                            {
                                NEWVALUE = (repeaterItem.FindControl("CHK_FIELD") as CheckBox).Text;
                            }
                        }
                    }
                    break;
            }

            txt_NewValue.Text = NEWVALUE;

            if (txt_NewValue.Text != txt_OldValue.Text)
            {
                updPnlIDDetail.Update();
                ModalPopupExtender1.Show();
            }
        }

        protected void btnRightClick(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

            txt_TableName.Text = hfTablename.Value;
            txt_VariableName.Text = (row.FindControl("lblVARIABLENAME") as Label).Text;
            txt_ModuleName.Text = lblModuleName.Text;
            txt_FieldName.Text = (row.FindControl("lblFieldName") as Label).Text;
            txt_OldValue.Text = (row.FindControl("HDN_OLD_VALUE") as HiddenField).Value;

            string CONTROLTYPE = (row.FindControl("lblCONTROLTYPE") as Label).Text;

            txt_NewValue.Text = "";

            switch (CONTROLTYPE)
            {
                case "TEXTBOX":
                    (row.FindControl("TXT_FIELD") as TextBox).Text = "";
                    break;
                case "DROPDOWN":
                    (row.FindControl("DRP_FIELD") as DropDownList).SelectedIndex = 0;
                    break;
                case "RADIOBUTTON":
                    Repeater repeaterRAD = (row.FindControl("repeat_RAD") as Repeater);
                    foreach (RepeaterItem repeaterItem in repeaterRAD.Items)
                    {
                        if ((repeaterItem.FindControl("RAD_FIELD") as RadioButton).Checked)
                        {
                            (repeaterItem.FindControl("RAD_FIELD") as RadioButton).Checked = false;
                        }
                    }
                    break;
                case "CHECKBOX":
                    Repeater repeater = (row.FindControl("repeat_CHK") as Repeater);
                    foreach (RepeaterItem repeaterItem in repeater.Items)
                    {
                        if ((repeaterItem.FindControl("CHK_FIELD") as CheckBox).Checked)
                        {
                            (repeaterItem.FindControl("CHK_FIELD") as CheckBox).Checked = false;
                        }
                    }
                    break;
            }

            UpdatePanel upnlBtn = (row.FindControl("upnlBtn") as UpdatePanel);

            if (hdnIsComplete.Value == "True")
            {
                upnlBtn.Update();
                updPnlIDDetail.Update();
                ModalPopupExtender1.Show();
            }
            else
            {
                drp_Reason.SelectedItem.Text = "Data Saved Incomplete";
                btn_Update_Click(sender, e);
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                ADD_NEW_ROW_DATA(txt_FieldName.Text,
                    txt_VariableName.Text,
                    txt_OldValue.Text,
                    txt_NewValue.Text,
                    drp_Reason.SelectedItem.Text,
                    txt_Comments.Text,
                    hdnRECID.Value
                    );

                drp_Reason.SelectedIndex = 0;
                txt_Comments.Text = "";

                InsertUpdatedata();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Reason for change added successfully.');window.location.href = '" + Request.Url.ToString() + "';", true);
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Reason for change canceled successfully.');window.location.href = '" + Request.Url.ToString() + "';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SAE_GetOnPageSpecs()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_SAE.SAE_Specs_Module_Wise_SP
                    (
                    ACTION: "Get_specs_Module_Wise",
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    SAE_grdOnPageSpecs.DataSource = ds;
                    SAE_grdOnPageSpecs.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }
    }
}