using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using CTMS.CommonFunction;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using HtmlAgilityPack;
using System.Net;


namespace CTMS
{
    public partial class DM_CreateModuleNew1 : System.Web.UI.Page
    {

        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtData.Attributes.Add("MaxLength", "1000");
            //txtSAEHelpData.Attributes.Add("MaxLength", "1000");
            try
            {
                if (!Page.IsPostBack)
                {
                    string System = "";

                    GetModule(System);
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
                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_USER_SYSTEMS");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpSystem.DataSource = ds.Tables[0];
                    drpSystem.DataValueField = "SystemName";
                    drpSystem.DataTextField = "SystemName";
                    drpSystem.DataBind();
                    drpSystem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
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

        public void GetModule(string SystemName)
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULENAME", SYSTEM: SystemName);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdModule.DataSource = ds.Tables[0];
                    grdModule.DataBind();
                    btnExportExcel.Visible = true;
                }
                else
                {
                    grdModule.DataSource = null;
                    grdModule.DataBind();
                    btnExportExcel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitModule_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtModuleName.Text.Trim() == "")
                {
                    Response.Write("<script>alert('Please Enter Module Name');</script>");
                }
                if (txtDomain.Text.Trim() == "")
                {
                    Response.Write("<script>alert('Please Enter Domain Name');</script>");
                }
                if (txtModuleSeqNo.Text.Trim() == "")
                {
                    Response.Write("<script>alert('Please Enter SeqNo.');</script>");
                }
                if ((chkblinded.Checked == false && chkUnblinded.Checked == false) && (chkSafety.Checked == true || chkDM.Checked == true || chkESource.Checked == true))
                {
                    Response.Write("<script>alert('Please check Blinded/Unblinded');</script>");
                }
                else if (txtModuleName.Text.Trim() != "" && txtDomain.Text.Trim() != "" && txtModuleSeqNo.Text.Trim() != "")
                {
                    DataSet dsDomain = dal_DB.DB_MODULE_SP(ACTION: "CHECK_DOMAIN_EXISTS_OR_NOT", DOMAIN: txtDomain.Text);

                    if (dsDomain.Tables.Count > 0 && dsDomain.Tables[0].Rows.Count > 0)
                    {
                        Response.Write("<script>alert('Domain name exists.');</script>");
                    }
                    else
                    {
                        DataSet ds = dal_DB.DB_MODULE_SP
                        (
                        ACTION: "INSERT_MODULENAME",
                        PROJECTID: Session["PROJECTID"].ToString(),
                        MODULENAME: txtModuleName.Text,
                        DOMAIN: txtDomain.Text,
                        SEQNO: txtModuleSeqNo.Text,
                        MultipleYN: chkMultiple.Checked,
                        Safety: chkSafety.Checked,
                        DM: chkDM.Checked,
                        IWRS: chkIWRS.Checked,
                        ePRO: chkePRO.Checked,
                        eSource: chkESource.Checked,
                        PD: chkPD.Checked,
                        HelpData: chkHelpData.Checked,
                        MEDOP: chkMEDOP.Checked,
                        CTMS: chkCTMS.Checked,
                        EXT: chkExternal_Independent.Checked,
                        SAE_HELPDATA: chkSafetyHelpData.Checked,
                        UNBLINDED: chkUnblinded.Checked,
                        BLINDED: chkblinded.Checked,
                        MultipleYN_SAE: chkMultipleSAE.Checked,
                        eCRF_SignOff: Check_eCRF.Checked,
                        LIMIT: txtLimit.Text
                        );

                        if (chkHelpData.Checked == false)
                        {
                            txtData.Text = "";
                        }

                        if (chkSafetyHelpData.Checked == false)
                        {
                            txtSAEHelpData.Text = "";
                        }
                        if (chkHelpData.Checked == true && HtmlToPlainText(txtData.Text).Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Add DM Help Data')", true);
                        }
                        if (chkSafetyHelpData.Checked == true && HtmlToPlainSAEHelpData(txtSAEHelpData.Text).Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Add Safety Help Data')", true);
                        }
                        else if (chkHelpData.Checked == true || chkSafetyHelpData.Checked == true)
                        {
                            dal_DB.DB_MODULE_SP(ACTION: "Insert_HelpData",
                            MODULEID: ds.Tables[0].Rows[0]["ID"].ToString(),
                            HelpDesc: txtData.Text,
                            SAE_HelpDesc: txtSAEHelpData.Text
                            );
                        }

                        if (chkDM.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE",
                            TABLE: ds.Tables[0].Rows[0]["Table"].ToString()
                                );
                        }
                        if (chkSafety.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_Safety",
                            TABLE: ds.Tables[0].Rows[0]["Safety_TABLENAME"].ToString()
                                );
                        }
                        if (chkIWRS.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_IWRS",
                            TABLE: ds.Tables[0].Rows[0]["IWRS_TABLENAME"].ToString()
                                );
                        }
                        if (chkePRO.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_ePRO",
                            TABLE: ds.Tables[0].Rows[0]["ePRO_TABLENAME"].ToString()
                                );
                        }

                        if (chkESource.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_ESOURCE",
                            TABLE: ds.Tables[0].Rows[0]["ESOURCE_TABLENAME"].ToString()
                                );
                        }

                        if (chkPD.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_PD",
                            TABLE: ds.Tables[0].Rows[0]["PD_TABLENAME"].ToString()
                                );
                        }

                        if (chkCTMS.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_CTMS",
                            TABLE: ds.Tables[0].Rows[0]["CTMS_TABLENAME"].ToString()
                                );
                        }
                        if (chkExternal_Independent.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_EXTERNAL_INDEPENDENT",
                            TABLE: ds.Tables[0].Rows[0]["EXT_TABLENAME"].ToString()
                                );
                        }

                        DataSet ds1 = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULEFIELD_BYMODULEID", MODULEID: ds.Tables[0].Rows[0]["MODULEID"].ToString());

                        if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                string DATATYPE = "NVARCHAR(MAX)";
                                if (dr["MAXLEN"].ToString() != "" && dr["MAXLEN"].ToString() != "0")
                                {
                                    DATATYPE = "NVARCHAR(" + dr["MAXLEN"].ToString() + ")";
                                }
                                if (dr["DATATYPE"].ToString() == "DECIMAL")
                                {
                                    DATATYPE = "FLOAT";
                                }
                                else if (dr["DATATYPE"].ToString() == "NUMERIC")
                                {
                                    DATATYPE = "FLOAT";
                                }
                                else if (dr["CONTROLTYPE"].ToString() == "DATE" || dr["CONTROLTYPE"].ToString() == "DATENOFUTURE")
                                {
                                    DATATYPE = "NVARCHAR(11)";
                                }
                                else if (dr["CONTROLTYPE"].ToString() == "DATEINPUTMASK")
                                {
                                    DATATYPE = "NVARCHAR(10)";
                                }

                                if (chkSafety.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                       (
                                       Action: "RENAME_COLUMN",
                                       TABLE: ds.Tables[0].Rows[0]["Safety_TABLENAME"].ToString(),
                                       COLUMN: dr["VARIABLENAME"].ToString(),
                                       OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                       DATATYPE: DATATYPE
                                       );
                                }

                                if (chkDM.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["Table"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkIWRS.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["IWRS_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkePRO.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["ePRO_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkESource.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["ESOURCE_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkPD.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["PD_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkCTMS.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["CTMS_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkExternal_Independent.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["EXT_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }
                            }
                        }

                        Response.Write("<script> alert('Module data added successfully.'); window.location.href='DM_CreateModuleNew1.aspx' </script>");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public string HtmlToPlainText(string txtData)
        {

            const string stripFormatting = @"<[^>]*(>|$)(\W|\n|\r)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);


            var textData = txtData;

            textData = System.Net.WebUtility.HtmlDecode(textData);

            textData = lineBreakRegex.Replace(textData, Environment.NewLine);

            textData = stripFormattingRegex.Replace(textData, string.Empty);
            return textData;
        }

        public string HtmlToPlainSAEHelpData(string txtSAEHelpData)
        {

            const string stripFormatting = @"<[^>]*(>|$)(\W|\n|\r)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);


            var textSAEHelpData = txtSAEHelpData;

            textSAEHelpData = System.Net.WebUtility.HtmlDecode(textSAEHelpData);

            textSAEHelpData = lineBreakRegex.Replace(textSAEHelpData, Environment.NewLine);

            textSAEHelpData = stripFormattingRegex.Replace(textSAEHelpData, string.Empty);
            return textSAEHelpData;
        }

        protected void btnUpdateModule_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtModuleName.Text.Trim() == "")
                {
                    Response.Write("<script>alert('Please Enter Module Name');</script>");
                }
                if (txtDomain.Text.Trim() == "")
                {
                    Response.Write("<script>alert('Please Enter Domain Name');</script>");
                }
                if (txtModuleSeqNo.Text.Trim() == "")
                {
                    Response.Write("<script>alert('Please Enter SeqNo.');</script>");
                }
                if ((chkblinded.Checked == false && chkUnblinded.Checked == false) && (chkSafety.Checked == true || chkDM.Checked == true || chkESource.Checked == true))
                {
                    Response.Write("<script>alert('Please check Blinded/Unblinded');</script>");
                }
                else if (txtModuleName.Text.Trim() != "" && txtDomain.Text.Trim() != "" && txtModuleSeqNo.Text.Trim() != "")
                {
                    DataSet dsDomain = dal_DB.DB_MODULE_SP(ACTION: "CHECK_DOMAIN_EXISTS_OR_NOT", DOMAIN: txtDomain.Text,
                        MODULEID: Session["ID"].ToString());

                    if (dsDomain.Tables.Count > 0 && dsDomain.Tables[0].Rows.Count > 0)
                    {
                        Response.Write("<script>alert('Domain name exists.');</script>");
                    }
                    else
                    {
                        DataSet ds = dal_DB.DB_MODULE_SP(
                            ACTION: "UPDATE_MODULENAME",
                            MODULENAME: txtModuleName.Text,
                            DOMAIN: txtDomain.Text,
                            SEQNO: txtModuleSeqNo.Text,
                            ID: Session["ID"].ToString(),
                            MultipleYN: chkMultiple.Checked,
                            Safety: chkSafety.Checked,
                            DM: chkDM.Checked,
                            IWRS: chkIWRS.Checked,
                            ePRO: chkePRO.Checked,
                            eSource: chkESource.Checked,
                            PD: chkPD.Checked,
                            HelpData: chkHelpData.Checked,
                            MEDOP: chkMEDOP.Checked,
                            CTMS: chkCTMS.Checked,
                            EXT: chkExternal_Independent.Checked,
                            SAE_HELPDATA: chkSafetyHelpData.Checked,
                            UNBLINDED: chkUnblinded.Checked,
                            BLINDED: chkblinded.Checked,
                            MultipleYN_SAE: chkMultipleSAE.Checked,
                            eCRF_SignOff: Check_eCRF.Checked,
                            LIMIT: txtLimit.Text
                            );

                        dal_DB.DB_REVIEW_SP(ACTION: "DELETE_MODULE_FROM_REVIEW",
                            MODULEID: Session["ID"].ToString()
                            );

                        if (chkHelpData.Checked == false)
                        {
                            txtData.Text = "";
                        }

                        if (chkSafetyHelpData.Checked == false)
                        {
                            txtSAEHelpData.Text = "";
                        }
                        if (chkHelpData.Checked == true && HtmlToPlainText(txtData.Text).Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Add DM Help Data')", true);
                        }
                        if (chkSafetyHelpData.Checked == true && HtmlToPlainSAEHelpData(txtSAEHelpData.Text).Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Add Safety Help Data')", true);
                        }
                        else if (chkHelpData.Checked == true || chkSafetyHelpData.Checked == true)
                        {
                            dal_DB.DB_MODULE_SP(ACTION: "Insert_HelpData",
                            MODULEID: Session["ID"].ToString(),
                            HelpDesc: txtData.Text,
                            SAE_HelpDesc: txtSAEHelpData.Text
                            );
                        }

                        if (chkDM.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE",
                            TABLE: ds.Tables[0].Rows[0]["TABLENAME"].ToString()
                                );
                        }
                        else
                        {
                            DataSet ds1 = dal_DB.DB_MODULE_SP(ACTION: "DELETE_MODULENAME", ID: Session["ID"].ToString(), DM: true, TABLENAME: ds.Tables[0].Rows[0]["TABLENAME"].ToString());
                        }

                        if (chkSafety.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_Safety",
                            TABLE: ds.Tables[0].Rows[0]["Safety_TABLENAME"].ToString()
                                );
                        }
                        else
                        {
                            DataSet ds1 = dal_DB.DB_MODULE_SP(ACTION: "DELETE_MODULENAME", ID: Session["ID"].ToString(), Safety: true, TABLENAME: ds.Tables[0].Rows[0]["Safety_TABLENAME"].ToString());
                        }

                        if (chkIWRS.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_IWRS",
                            TABLE: ds.Tables[0].Rows[0]["IWRS_TABLENAME"].ToString()
                                );
                        }
                        else
                        {
                            DataSet ds1 = dal_DB.DB_MODULE_SP(ACTION: "DELETE_MODULENAME", ID: Session["ID"].ToString(), IWRS: true, TABLENAME: ds.Tables[0].Rows[0]["IWRS_TABLENAME"].ToString());
                        }

                        if (chkePRO.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_ePRO",
                            TABLE: ds.Tables[0].Rows[0]["ePRO_TABLENAME"].ToString()
                                );
                        }
                        else
                        {
                            DataSet ds1 = dal_DB.DB_MODULE_SP(ACTION: "DELETE_MODULENAME", ID: Session["ID"].ToString(), ePRO: true, TABLENAME: ds.Tables[0].Rows[0]["ePRO_TABLENAME"].ToString());
                        }

                        if (chkESource.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_ESOURCE",
                            TABLE: ds.Tables[0].Rows[0]["ESOURCE_TABLENAME"].ToString()
                                );
                        }
                        else
                        {
                            DataSet ds1 = dal_DB.DB_MODULE_SP(ACTION: "DELETE_MODULENAME", ID: Session["ID"].ToString(), eSource: true, TABLENAME: ds.Tables[0].Rows[0]["ESOURCE_TABLENAME"].ToString());
                        }

                        if (chkPD.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_PD",
                            TABLE: ds.Tables[0].Rows[0]["PD_TABLENAME"].ToString()
                                );
                        }
                        else
                        {
                            DataSet ds1 = dal_DB.DB_MODULE_SP(ACTION: "DELETE_MODULENAME", ID: Session["ID"].ToString(), PD: true, TABLENAME: ds.Tables[0].Rows[0]["PD_TABLENAME"].ToString());
                        }

                        if (chkCTMS.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_CTMS",
                            TABLE: ds.Tables[0].Rows[0]["CTMS_TABLENAME"].ToString()
                                );
                        }
                        else
                        {
                            DataSet ds1 = dal_DB.DB_MODULE_SP(ACTION: "DELETE_MODULENAME", ID: Session["ID"].ToString(), CTMS: true, TABLENAME: ds.Tables[0].Rows[0]["CTMS_TABLENAME"].ToString());
                        }
                        if (chkExternal_Independent.Checked == true)
                        {
                            dal_DB.DB_TABLES_SP(
                            Action: "CREATE_TABLE_EXTERNAL_INDEPENDENT",
                            TABLE: ds.Tables[0].Rows[0]["EXT_TABLENAME"].ToString()
                                );
                        }
                        else
                        {
                            DataSet ds1 = dal_DB.DB_MODULE_SP(ACTION: "DELETE_MODULENAME", ID: Session["ID"].ToString(), EXT: true, TABLENAME: ds.Tables[0].Rows[0]["EXT_TABLENAME"].ToString());
                        }

                        DataSet ds2 = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULEFIELD_BYMODULEID",
                            MODULEID: Session["ID"].ToString());

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds2.Tables[0].Rows)
                            {
                                string DATATYPE = "NVARCHAR(MAX)";
                                if (dr["MAXLEN"].ToString() != "" && dr["MAXLEN"].ToString() != "0")
                                {
                                    DATATYPE = "NVARCHAR(" + dr["MAXLEN"].ToString() + ")";
                                }
                                if (dr["DATATYPE"].ToString() == "DECIMAL")
                                {
                                    DATATYPE = "FLOAT";
                                }
                                else if (dr["DATATYPE"].ToString() == "NUMERIC")
                                {
                                    DATATYPE = "FLOAT";
                                }
                                else if (dr["CONTROLTYPE"].ToString() == "DATE" || dr["CONTROLTYPE"].ToString() == "DATENOFUTURE")
                                {
                                    DATATYPE = "NVARCHAR(11)";
                                }
                                else if (dr["CONTROLTYPE"].ToString() == "DATEINPUTMASK")
                                {
                                    DATATYPE = "NVARCHAR(10)";
                                }

                                if (chkSafety.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                       (
                                       Action: "RENAME_COLUMN",
                                       TABLE: ds.Tables[0].Rows[0]["Safety_TABLENAME"].ToString(),
                                       COLUMN: dr["VARIABLENAME"].ToString(),
                                       OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                       DATATYPE: DATATYPE
                                       );

                                    dal_DB.DB_TABLES_SP
                                       (
                                       Action: "RENAME_COLUMN",
                                       TABLE: ds.Tables[0].Rows[0]["Safety_TABLENAME"].ToString() + "_LOGS",
                                       COLUMN: dr["VARIABLENAME"].ToString(),
                                       OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                       DATATYPE: DATATYPE
                                       );
                                }

                                if (chkDM.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkIWRS.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["IWRS_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkePRO.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["ePRO_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkESource.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["ESOURCE_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkPD.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["PD_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkCTMS.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                   (
                                   Action: "RENAME_COLUMN",
                                   TABLE: ds.Tables[0].Rows[0]["CTMS_TABLENAME"].ToString(),
                                   COLUMN: dr["VARIABLENAME"].ToString(),
                                   OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                   DATATYPE: DATATYPE
                                   );
                                }

                                if (chkExternal_Independent.Checked)
                                {
                                    dal_DB.DB_TABLES_SP
                                    (
                                    Action: "RENAME_COLUMN",
                                    TABLE: ds.Tables[0].Rows[0]["EXT_TABLENAME"].ToString(),
                                    COLUMN: dr["VARIABLENAME"].ToString(),
                                    OLDCOLUMN: dr["VARIABLENAME"].ToString(),
                                    DATATYPE: DATATYPE
                                    );
                                }
                            }
                        }

                        Session.Remove("ID");
                        Response.Write("<script> alert('Module data Updated successfully.'); window.location.href='DM_CreateModuleNew1.aspx' </script>");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnCancelModule_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdModule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["ID"] = id;
                if (e.CommandName == "EditModule")
                {
                    DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULENAME_BYID", ID: Session["ID"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hfOLDMODULE.Value = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                        txtModuleName.Text = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                        txtDomain.Text = ds.Tables[0].Rows[0]["DOMAIN"].ToString();
                        txtModuleSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                        {
                            chkMultiple.Checked = true;
                        }
                        else
                        {
                            chkMultiple.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["Safety"].ToString() == "True")
                        {
                            chkSafety.Checked = true;

                        }
                        else
                        {
                            chkSafety.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["DM"].ToString() == "True")
                        {
                            chkDM.Checked = true;
                        }
                        else
                        {
                            chkDM.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["IWRS"].ToString() == "True")
                        {
                            chkIWRS.Checked = true;
                        }
                        else
                        {
                            chkIWRS.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["ePRO"].ToString() == "True")
                        {
                            chkePRO.Checked = true;
                        }
                        else
                        {
                            chkePRO.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["eSource"].ToString() == "True")
                        {
                            chkESource.Checked = true;
                        }
                        else
                        {
                            chkESource.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["PD"].ToString() == "True")
                        {
                            chkPD.Checked = true;
                        }
                        else
                        {
                            chkPD.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["CTMS"].ToString() == "True")
                        {
                            chkCTMS.Checked = true;
                        }
                        else
                        {
                            chkCTMS.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["EXT"].ToString() == "True")
                        {
                            chkExternal_Independent.Checked = true;
                        }
                        else
                        {
                            chkExternal_Independent.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["HELPDATA"].ToString() == "True")
                        {
                            chkHelpData.Checked = true;
                            divHelpData.Visible = true;
                            txtData.Text = ds.Tables[0].Rows[0]["DATA"].ToString();
                        }
                        else
                        {
                            chkHelpData.Checked = false;
                            divHelpData.Visible = false;
                        }

                        if (ds.Tables[0].Rows[0]["SAE_HELPDATA"].ToString() == "True")
                        {
                            chkSafetyHelpData.Checked = true;
                            divSAEHelpData.Visible = true;

                            txtSAEHelpData.Text = ds.Tables[0].Rows[0]["SAE_DATA"].ToString();
                        }
                        else
                        {
                            chkSafetyHelpData.Checked = false;
                            divSAEHelpData.Visible = false;
                        }

                        if (ds.Tables[0].Rows[0]["MEDOP"].ToString() == "True")
                        {
                            chkMEDOP.Checked = true;
                        }
                        else
                        {
                            chkMEDOP.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["UNBLINDED"].ToString() == "True")
                        {
                            chkUnblinded.Checked = true;
                        }
                        else
                        {
                            chkUnblinded.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["BLINDED"].ToString() == "True")
                        {
                            chkblinded.Checked = true;
                        }
                        else
                        {
                            chkblinded.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["MULTIPLEYN_SAE"].ToString() == "True")
                        {
                            chkMultipleSAE.Checked = true;

                        }
                        else
                        {
                            chkMultipleSAE.Checked = false;

                        }

                        if (ds.Tables[0].Rows[0]["eCRF_SignOff"].ToString() == "True")
                        {
                            Check_eCRF.Checked = true;
                        }
                        else
                        {
                            Check_eCRF.Checked = false;
                        }

                        txtLimit.Text = ds.Tables[0].Rows[0]["LIMIT"].ToString();
                        chkMultiple_CheckedChanged(this, e);
                        chkAllSystem_CheckedChanged(this, e);
                        chkMultipleSAE_CheckedChanged(this, e);
                        btnUpdateModule.Visible = true;
                        btnSubmitModule.Visible = false;
                    }

                    MODULE_STATUS(Session["ID"].ToString());

                    //DataSet ds1 = dal_DB.DB_REVIEW_SP("GET_MODULE_REVIEW_FREEZE_STATUS", MODULEID: Session["ID"].ToString());

                    //if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    //{
                    //    if (ds1.Tables[0].Rows[0]["SEND_TO_REVIEW"].ToString() == "True" && ds1.Tables[0].Rows[0]["REVIEW"].ToString() != "True" && ds1.Tables[0].Rows[0]["FREEZE"].ToString() != "True" && ds1.Tables[0].Rows[0]["OPEN_FOR_EDIT"].ToString() != "True")
                    //    {
                    //        hdnModuleStatus.Value = "Sent For Review";

                    //        if (ds1.Tables[0].Rows[0]["FIELD_COUNT"].ToString() != "0")
                    //        {
                    //            btnOpenForEdit.Visible = true;
                    //        }
                    //        else
                    //        {
                    //            btnOpenForEdit.Visible = false;
                    //        }

                    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv('" + hdnModuleStatus.Value + "');", true);
                    //    }
                    //    else if (ds1.Tables[0].Rows[0]["SEND_TO_REVIEW"].ToString() == "True" && ds1.Tables[0].Rows[0]["REVIEW"].ToString() == "True" && ds1.Tables[0].Rows[0]["FREEZE"].ToString() != "True" && ds1.Tables[0].Rows[0]["OPEN_FOR_EDIT"].ToString() != "True")
                    //    {
                    //        hdnModuleStatus.Value = "Reviewed";

                    //        btnOpenForEdit.Visible = false;

                    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv('" + hdnModuleStatus.Value + "');", true);
                    //    }
                    //    else if (ds1.Tables[0].Rows[0]["SEND_TO_REVIEW"].ToString() == "True" && ds1.Tables[0].Rows[0]["REVIEW"].ToString() == "True" && ds1.Tables[0].Rows[0]["FREEZE"].ToString() == "True" && ds1.Tables[0].Rows[0]["OPEN_FOR_EDIT"].ToString() != "True")
                    //    {
                    //        hdnModuleStatus.Value = "Frozen";
                    //        btnOpenForEdit.Visible = false;

                    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv('" + hdnModuleStatus.Value + "');", true);
                    //    }
                    //    else
                    //    {
                    //        hdnModuleStatus.Value = "";
                    //        btnOpenForEdit.Visible = false;
                    //    }

                    //    if (ds1.Tables[0].Rows[0]["REVIEW_COUNTS"].ToString() != "0")
                    //    {
                    //        btnReviewLogs.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        btnReviewLogs.Visible = false;
                    //    }
                    //}
                }
                else if (e.CommandName == "DeleteModule")
                {
                    DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "DELETE_MODULENAME_WITH_TABLES", ID: Session["ID"].ToString());

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Module deleted successfully'); window.location.href='DM_CreateModuleNew1.aspx' ", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdModule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string COUNT = dr["COUNT"].ToString();
                    LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");
                    HiddenField DM = (HiddenField)e.Row.FindControl("DM");
                    HiddenField eSource = (HiddenField)e.Row.FindControl("eSource");

                    if (COUNT != "0")
                    {
                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }

                    if (dr["FREEZE"].ToString() == "Frozen")
                    {
                        lbtndeleteSection.Visible = false;
                    }

                    LinkButton lbtnReviewHistory = (LinkButton)e.Row.FindControl("lbtnReviewHistory");

                    if (dr["REVIEW_COUNT"].ToString() != "0")
                    {
                        lbtnReviewHistory.Visible = true;
                    }
                    else
                    {
                        lbtnReviewHistory.Visible = false;
                    }

                    LinkButton lbtnSetCriteria = (LinkButton)e.Row.FindControl("lbtnSetCriteria");
                    if (dr["DM"].ToString() == "True" || dr["eSource"].ToString() == "True")
                    {
                        lbtnSetCriteria.Visible = true;
                    }
                    else
                    {
                        lbtnSetCriteria.Visible = false;
                    }

                    HtmlControl iconSafety = (HtmlControl)e.Row.FindControl("iconPharmacovigilance");
                    if (dr["Safety"].ToString() == "True")
                    {
                        iconSafety.Attributes.Add("class", "fa fa-check");
                        iconSafety.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSafety.Attributes.Add("class", "fa fa-times");
                        iconSafety.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconDataManagement = (HtmlControl)e.Row.FindControl("iconDataManagement");
                    if (dr["DM"].ToString() == "True")
                    {
                        iconDataManagement.Attributes.Add("class", "fa fa-check");
                        iconDataManagement.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconDataManagement.Attributes.Add("class", "fa fa-times");
                        iconDataManagement.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconeSource = (HtmlControl)e.Row.FindControl("iconeSource");
                    if (dr["eSource"].ToString() == "True")
                    {
                        iconeSource.Attributes.Add("class", "fa fa-check");
                        iconeSource.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconeSource.Attributes.Add("class", "fa fa-times");
                        iconeSource.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconIWRS = (HtmlControl)e.Row.FindControl("iconIWRS");
                    if (dr["IWRS"].ToString() == "True")
                    {
                        iconIWRS.Attributes.Add("class", "fa fa-check");
                        iconIWRS.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconIWRS.Attributes.Add("class", "fa fa-times");
                        iconIWRS.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconePRO = (HtmlControl)e.Row.FindControl("iconePRO");
                    if (dr["ePRO"].ToString() == "True")
                    {
                        iconePRO.Attributes.Add("class", "fa fa-check");
                        iconePRO.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconePRO.Attributes.Add("class", "fa fa-times");
                        iconePRO.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconPD = (HtmlControl)e.Row.FindControl("iconPD");
                    if (dr["PD"].ToString() == "True")
                    {
                        iconPD.Attributes.Add("class", "fa fa-check");
                        iconPD.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconPD.Attributes.Add("class", "fa fa-times");
                        iconPD.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconEXT = (HtmlControl)e.Row.FindControl("iconEXT");
                    if (dr["EXT"].ToString() == "True")
                    {
                        iconEXT.Attributes.Add("class", "fa fa-check");
                        iconEXT.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconEXT.Attributes.Add("class", "fa fa-times");
                        iconEXT.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconMULTIPLEYN = (HtmlControl)e.Row.FindControl("iconMULTIPLEYN");
                    if (dr["MULTIPLEYN"].ToString() == "True")
                    {
                        iconMULTIPLEYN.Attributes.Add("class", "fa fa-check");
                        iconMULTIPLEYN.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconMULTIPLEYN.Attributes.Add("class", "fa fa-times");
                        iconMULTIPLEYN.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconMULTIPLEYNSAE = (HtmlControl)e.Row.FindControl("iconMULTIPLEYNSAE");
                    if (dr["MULTIPLEYN_SAE"].ToString() == "True")
                    {
                        iconMULTIPLEYNSAE.Attributes.Add("class", "fa fa-check");
                        iconMULTIPLEYNSAE.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconMULTIPLEYNSAE.Attributes.Add("class", "fa fa-times");
                        iconMULTIPLEYNSAE.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconBlinded = (HtmlControl)e.Row.FindControl("iconBlinded");
                    if (dr["Blinded"].ToString() == "True")
                    {
                        iconBlinded.Attributes.Add("class", "fa fa-check");
                        iconBlinded.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconBlinded.Attributes.Add("class", "fa fa-times");
                        iconBlinded.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconUnblinded = (HtmlControl)e.Row.FindControl("iconUnblinded");
                    if (dr["Unblinded"].ToString() == "True")
                    {
                        iconUnblinded.Attributes.Add("class", "fa fa-check");
                        iconUnblinded.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconUnblinded.Attributes.Add("class", "fa fa-times");
                        iconUnblinded.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconMedop = (HtmlControl)e.Row.FindControl("iconMedop");
                    if (dr["Medop"].ToString() == "True")
                    {
                        iconMedop.Attributes.Add("class", "fa fa-check");
                        iconMedop.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconMedop.Attributes.Add("class", "fa fa-times");
                        iconMedop.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconeCRF_SignOff = (HtmlControl)e.Row.FindControl("iconeCRF_SignOff");
                    if (dr["eCRF_SignOff"].ToString() == "True")
                    {
                        iconeCRF_SignOff.Attributes.Add("class", "fa fa-check");
                        iconeCRF_SignOff.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconeCRF_SignOff.Attributes.Add("class", "fa fa-times");
                        iconeCRF_SignOff.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconHelp_CRF = (HtmlControl)e.Row.FindControl("iconHelp_CRF");
                    if (dr["Help_CRF"].ToString() == "True")
                    {
                        iconHelp_CRF.Attributes.Add("class", "fa fa-check");
                        iconHelp_CRF.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconHelp_CRF.Attributes.Add("class", "fa fa-times");
                        iconHelp_CRF.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconHelp_Pharmacovigilance = (HtmlControl)e.Row.FindControl("iconHelp_Pharmacovigilance");
                    if (dr["Help_Pharmacovigilance"].ToString() == "True")
                    {
                        iconHelp_Pharmacovigilance.Attributes.Add("class", "fa fa-check");
                        iconHelp_Pharmacovigilance.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconHelp_Pharmacovigilance.Attributes.Add("class", "fa fa-times");
                        iconHelp_Pharmacovigilance.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconLIMIT = (HtmlControl)e.Row.FindControl("iconLIMIT");
                    if (dr["LIMIT"].ToString() == "True")
                    {
                        iconLIMIT.Attributes.Add("class", "fa fa-check");
                        iconLIMIT.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconLIMIT.Attributes.Add("class", "fa fa-times");
                        iconLIMIT.Attributes.Add("style", "color: red;");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void chkHelpData_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtModuleName.Text != "")
                {
                    if (chkHelpData.Checked == true)
                    {
                        divHelpData.Visible = true;
                    }
                    else
                    {
                        divHelpData.Visible = false;
                    }
                }
                else
                {
                    chkHelpData.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('First fill all data')", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkSafetyHelpData_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtModuleName.Text != "")
                {
                    if (chkSafetyHelpData.Checked == true)
                    {
                        divSAEHelpData.Visible = true;
                    }
                    else
                    {
                        divSAEHelpData.Visible = false;
                    }
                }
                else
                {
                    chkSafetyHelpData.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('First fill all data')", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkMultiple_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMultiple.Checked == true)
            {
                lbllimit.Visible = true;
            }
            else
            {
                if (chkMultipleSAE.Checked)
                {
                    lbllimit.Visible = true;
                }
                else
                {
                    lbllimit.Visible = false;
                    txtLimit.Text = "";
                }
            }
        }

        protected void grdModule_PreRender(object sender, EventArgs e)
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

        protected void chkAllSystem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSafety.Checked)
                {
                    //safety related divs
                    divBlindedUnbl.Visible = true;
                    divhelpInstruction.Visible = true;
                    divhelpSefty.Visible = true;
                    divINVMedOpinion.Visible = true;
                    divmedOpinion.Visible = true;
                    divMultiple.Visible = true;
                    divmultiSefty.Visible = true;

                    if (chkDM.Checked)
                    {
                        divINVeCRF.Visible = true;
                        divhelpCrf.Visible = true;
                        divmultiCRF.Visible = true;
                    }
                    else if (chkDM.Checked && chkESource.Checked)
                    {
                        divINVeCRF.Visible = true;
                        divhelpCrf.Visible = true;
                        divmultiCRF.Visible = true;
                    }
                    else if (chkESource.Checked)
                    {
                        divhelpCrf.Visible = true;
                        divmultiCRF.Visible = true;
                        divINVeCRF.Visible = false;
                    }
                    else
                    {
                        //other than safety divs
                        divhelpCrf.Visible = false;
                        chkHelpData.Checked = false;
                        divHelpData.Visible = false;
                        txtData.Text = "";
                        divINVeCRF.Visible = false;
                        Check_eCRF.Checked = false;
                        divmultiCRF.Visible = false;
                        chkMultiple.Checked = false;
                    }

                    if (chkMultiple.Checked || chkMultipleSAE.Checked)
                    {
                        lbllimit.Visible = true;
                    }
                    else
                    {
                        lbllimit.Visible = false;
                    }

                    if (chkHelpData.Checked)
                    {
                        divHelpData.Visible = true;
                    }
                    else
                    {
                        divHelpData.Visible = false;
                    }

                    if (chkSafetyHelpData.Checked)
                    {
                        divSAEHelpData.Visible = true;
                    }
                    else
                    {
                        divSAEHelpData.Visible = false;
                    }
                }
                else if (chkDM.Checked)
                {
                    //safety & DDC related divs
                    divBlindedUnbl.Visible = true;
                    divhelpInstruction.Visible = true;
                    divhelpCrf.Visible = true;
                    divINVMedOpinion.Visible = true;
                    divmedOpinion.Visible = true;
                    divINVeCRF.Visible = true;
                    divMultiple.Visible = true;
                    divmultiCRF.Visible = true;

                    if (chkMultiple.Checked)
                    {
                        lbllimit.Visible = true;
                    }
                    else
                    {
                        lbllimit.Visible = false;
                    }

                    if (chkHelpData.Checked)
                    {
                        divHelpData.Visible = true;
                    }
                    else
                    {
                        divHelpData.Visible = false;
                    }

                    //other than safety & DDC divs
                    divhelpSefty.Visible = false;
                    chkSafetyHelpData.Checked = false;
                    divmultiSefty.Visible = false;
                    chkMultipleSAE.Checked = false;
                    divSAEHelpData.Visible = false;
                    txtSAEHelpData.Text = "";
                }
                else if (chkESource.Checked)
                {
                    //safety & DDC related divs
                    divBlindedUnbl.Visible = true;
                    divhelpInstruction.Visible = true;
                    divhelpCrf.Visible = true;
                    divINVMedOpinion.Visible = true;
                    divmedOpinion.Visible = true;
                    divMultiple.Visible = true;
                    divmultiCRF.Visible = true;

                    if (chkMultiple.Checked)
                    {
                        lbllimit.Visible = true;
                    }
                    else
                    {
                        lbllimit.Visible = false;
                    }

                    if (chkHelpData.Checked)
                    {
                        divHelpData.Visible = true;
                    }
                    else
                    {
                        divHelpData.Visible = false;
                    }

                    //other than safety & DDC divs
                    divINVeCRF.Visible = false;
                    Check_eCRF.Checked = false;
                    divhelpSefty.Visible = false;
                    chkSafetyHelpData.Checked = false;
                    divmultiSefty.Visible = false;
                    chkMultipleSAE.Checked = false;
                    divSAEHelpData.Visible = false;
                    txtSAEHelpData.Text = "";
                }
                else
                {
                    //all uncheck systems
                    divBlindedUnbl.Visible = false;
                    chkblinded.Checked = false;
                    chkUnblinded.Checked = false;
                    divhelpInstruction.Visible = false;
                    divhelpCrf.Visible = false;
                    chkHelpData.Checked = false;
                    divhelpSefty.Visible = false;
                    chkSafetyHelpData.Checked = false;
                    divHelpData.Visible = false;
                    txtData.Text = "";
                    divSAEHelpData.Visible = false;
                    txtSAEHelpData.Text = "";
                    divINVMedOpinion.Visible = false;
                    divmedOpinion.Visible = false;
                    chkMEDOP.Checked = false;
                    divINVeCRF.Visible = false;
                    Check_eCRF.Checked = false;
                    divMultiple.Visible = false;
                    divmultiCRF.Visible = false;
                    chkMultiple.Checked = false;
                    divmultiSefty.Visible = false;
                    chkMultipleSAE.Checked = false;
                    lbllimit.Visible = false;
                    txtLimit.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        [System.Web.Services.WebMethod]
        public static string REVIEW_HISTORY(string MODULEID)
        {
            string str = "";
            try
            {
                DAL_DB dal_DB = new DAL_DB();

                DataSet ds = dal_DB.DB_REVIEW_SP(
                    ACTION: "GET_REVIEW_LOGS",
                    MODULEID: MODULEID
                    );

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML(ds);
                    }
                    else
                    {
                        str = "No Record Available.";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return str;
        }

        public static string ConvertDataTableToHTML(DataSet ds)
        {
            string html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                html += "<th scope='col'>" + ds.Tables[0].Columns[i].ColumnName + "</th>";
            html += "</tr>";

            //add rows
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                html += "<tr style='text-align: left;'>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        protected void chkMultipleSAE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMultipleSAE.Checked == true)
            {
                lbllimit.Visible = true;
            }
            else
            {
                if (chkMultiple.Checked)
                {
                    lbllimit.Visible = true;
                }
                else
                {
                    lbllimit.Visible = false;
                    txtLimit.Text = "";
                }
            }
        }

        protected void btnOpenForEdit_Click(object sender, EventArgs e)
        {
            try
            {
                txtOpenForEditReason.Attributes.Add("MaxLength", "500");

                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_SYSTEMS_BYMODULE_FOR_REVIEW_PROCESS",
                    MODULEID: Convert.ToString(Session["ID"])
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblEditForEditHeader.InnerText = txtModuleName.Text + " module is available at below systems.";

                    lstOpenForEditSystems.DataSource = ds.Tables[0];
                    lstOpenForEditSystems.DataBind();
                }
                else
                {
                    lstOpenForEditSystems.DataSource = null;
                    lstOpenForEditSystems.DataBind();
                }

                updPnlIDDetail.Update();
                ModalPopupExtender4.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitEditForEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string Systems = "";

                for (int i = 0; i < lstOpenForEditSystems.Items.Count; i++)
                {
                    dal_DB.DB_REVIEW_SP(ACTION: "UPDATE_MODULE_OPEN_FOR_EDIT",
                        MODULEID: Convert.ToString(Session["ID"]),
                        SYSTEM: (lstOpenForEditSystems.Items[i].FindControl("lblOpenForEditSystemName") as Label).Text,
                        SEND_TO_REVIEW: false,
                        REVIEW: false,
                        REASON: txtOpenForEditReason.Text
                        );

                    if (Systems == "")
                    {
                        Systems = (lstOpenForEditSystems.Items[i].FindControl("lblOpenForEditSystemName") as Label).Text;
                    }
                    else
                    {
                        Systems += " and " + (lstOpenForEditSystems.Items[i].FindControl("lblOpenForEditSystemName") as Label).Text;
                    }
                }

                SendEmail("Open For Edit From Designer", txtOpenForEditReason.Text);

                string MSG = txtModuleName.Text + " has been open for edit for " + Systems + " System.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + MSG + "'); window.location.href = '" + Request.RawUrl.ToString() + "' ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelEditForEdit_Click(object sender, EventArgs e)
        {
            try
            {
                txtOpenForEditReason.Text = "";
                ModalPopupExtender4.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SendEmail(string ACTIONS, string REASON)
        {
            try
            {
                CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();

                DataSet dsSystem = dal_DB.DB_REVIEW_SP(ACTION: "GET_SYSTEMS_BYMODULE",
                    MODULEID: Convert.ToString(Session["ID"])
                    );

                if (dsSystem.Tables.Count > 0 && dsSystem.Tables[0].Rows.Count > 0)
                {
                    DataSet ds = dal_DB.DB_EMAIL_REVIEW_SP(ACTION: "GET_EMAILSIDS",
                        ACTIVITY: ACTIONS,
                        SYSTEMS: dsSystem.Tables[0].Rows[0]["SystemName"].ToString()
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
                            if (EmailSubject.Contains("[PROJECTID]"))
                            {
                                EmailSubject = EmailSubject.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                            }

                            if (EmailSubject.Contains("[MODULENAME]"))
                            {
                                EmailSubject = EmailSubject.Replace("[MODULENAME]", txtModuleName.Text);
                            }

                            if (EmailSubject.Contains("[COMMENT]"))
                            {
                                EmailSubject = EmailSubject.Replace("[COMMENT]", REASON);
                            }

                            if (EmailSubject.Contains("[SYSTEM]"))
                            {
                                EmailSubject = EmailSubject.Replace("[SYSTEM]", dsSystem.Tables[0].Rows[0]["SystemName"].ToString());
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
                            if (EmailBody.Contains("[MODULENAME]"))
                            {
                                EmailBody = EmailBody.Replace("[MODULENAME]", txtModuleName.Text);
                            }

                            if (EmailBody.Contains("[SYSTEM]"))
                            {
                                EmailBody = EmailBody.Replace("[SYSTEM]", dsSystem.Tables[0].Rows[0]["SystemName"].ToString());
                            }

                            if (EmailBody.Contains("[COMMENT]"))
                            {
                                EmailBody = EmailBody.Replace("[COMMENT]", REASON);
                            }

                            if (EmailBody.Contains("[PROJECTID]"))
                            {
                                EmailBody = EmailBody.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;

            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULENAME_EXPORT");

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Module List_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetModule(drpSystem.SelectedItem.Text);
        }

        protected void lbtnExportCrfIntruc_Click(object sender, EventArgs e)
        {

            DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_DATA_INSTRUCTIONS");
            DataTable dataTable = ds.Tables[0];

            ExportToWordInMemory(dataTable);

        }

        public string StripHtml(string html)
        {

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            string plainText = document.DocumentNode.InnerText;

            return WebUtility.HtmlDecode(plainText);
        }

        public void ExportToWordInMemory(DataTable dataTable)
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {

                using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(memoryStream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
                {

                    MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                    mainPart.Document = new Document(new Body());

                    Body body = mainPart.Document.Body;

                    Paragraph paragraph = new Paragraph();
                    Run run = new Run();

                    foreach (DataRow row in dataTable.Rows)
                    {

                        string ModuleName = StripHtml(row["MODULENAME"].ToString());
                        string CRF_DATA = StripHtml(row["DATA"].ToString());
                        string SAE_DATA = StripHtml(row["SAE_DATA"].ToString());

                        string[] CRF_Data = CRF_DATA.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                        string[] SAE_Data = SAE_DATA.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                        body.Append(new Paragraph(new Run(new RunProperties(new Bold()), new Text("Module Name: ")), new Run(new Text(" ")), new Run(new Text(ModuleName))));

                        body.Append(new Paragraph(new Run(new RunProperties(new Bold()), new Text("CRF Help (Instructions):"))));

                        foreach (string paragraphText in CRF_Data)
                        {
                            Paragraph data_crf = new Paragraph(new Run(new Text(paragraphText)));
                            body.Append(data_crf);
                        }


                        body.Append(new Paragraph(new Run(new RunProperties(new Bold()), new Text("Pharmacovigilance Help (Instructions):"))));
                        foreach (string paragraphText in SAE_Data)
                        {
                            Paragraph data_Sae = new Paragraph(new Run(new Text(paragraphText)));
                            body.Append(data_Sae);
                        }
                        body.Append(new Paragraph(new Run(new Break() { Type = BreakValues.Page })));
                    }
                }


                memoryStream.Seek(0, SeekOrigin.Begin);

                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=IntructionsData.doc");
                HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
                HttpContext.Current.Response.End();
            }

        }

        protected void MODULE_STATUS(string MODULEID)
        {
            try
            {
                bool disablepage = false;

                DataSet ds_SYSTEM_COUNT = dal_DB.DB_REVIEW_SP(ACTION: "GET_SYSTEMS_BYMODULE_FOR_REVIEW_PROCESS",
                    MODULEID: MODULEID
                    );

                DataSet ds1 = dal_DB.DB_REVIEW_SP("GET_MODULE_STATUS_CREATE_MODULE",
                       MODULEID: MODULEID,
                       SYSTEM: drpSystem.SelectedValue
                       );

                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["FREEZE"].ToString() == "True")
                    {
                        hdnModuleStatus.Value = "Frozen";
                        btnOpenForEdit.Visible = false;

                        disablepage = true;
                    }

                    if (ds1.Tables[0].Rows[0]["SENTFORREVIEW_COUNT"].ToString() != "0")  //Sent For Review
                    {
                        if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_GEN_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = "Un-Review Request Generated";
                            btnOpenForEdit.Visible = false;
                            btnOpenForEdit.Visible = false;
                            btnSubmitModule.Visible = false;
                            btnUpdateModule.Visible = false;
                        }
                        else
                        {
                            hdnModuleStatus.Value = "Sent For Review";
                            btnOpenForEdit.Visible = true;
                            btnOpenForEdit.Visible = true;
                            btnSubmitModule.Visible = false;
                            btnUpdateModule.Visible = false;

                            if (ds1.Tables[0].Rows[0]["REVIEWED_COUNT"].ToString() != "0")
                            {
                                btnOpenForEdit.Visible = false;
                            }
                        }

                        disablepage = true;
                    }
                    else if (ds1.Tables[0].Rows[0]["OPENFOREDIT_COUNT"].ToString() != "0" || ds1.Tables[0].Rows[0]["SENDBACKTODES_COUNT"].ToString() != "0") //Open For Edit From Designer && Sent Back To Designer From Reviewer
                    {
                        btnOpenForEdit.Visible = false;

                        if (ds1.Tables[0].Rows[0]["OPENFOREDIT_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = "Open For Edit";
                        }
                        else if (ds1.Tables[0].Rows[0]["SENDBACKTODES_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = "Sent Back To Designer From Reviewer";
                        }
                    }
                    else if (ds1.Tables[0].Rows[0]["REVIEWED_COUNT"].ToString() != "0" || ds1.Tables[0].Rows[0]["UNREVIEW_REQ_GEN_COUNT"].ToString() != "0") //Reviewed && Un-Review Request Generated From Reviewer
                    {
                        btnOpenForEdit.Visible = false;
                        btnOpenForEdit.Visible = false;
                        btnSubmitModule.Visible = false;
                        btnUpdateModule.Visible = false;

                        if (ds1.Tables[0].Rows[0]["REVIEWED_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = "Reviewed";
                        }
                        else if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_GEN_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = "Un-Review Request Generated";
                        }

                        disablepage = true;
                    }
                    else if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_APPROVE_COUNT"].ToString() != "0") //Un-Review Request Approved
                    {
                        if (ds_SYSTEM_COUNT.Tables[0].Rows.Count.ToString() == ds1.Tables[0].Rows[0]["UNREVIEW_REQ_APPROVE_COUNT"].ToString())
                        {
                            btnOpenForEdit.Visible = false;

                            if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_APPROVE_COUNT"].ToString() != "0")
                            {
                                hdnModuleStatus.Value = "Un-Review Request Approved";
                            }

                            disablepage = true;
                        }
                        else
                        {
                            if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_GEN_COUNT"].ToString() != "0")
                            {
                                hdnModuleStatus.Value = "Un-Review Request Generated";
                                btnOpenForEdit.Visible = false;
                                btnOpenForEdit.Visible = false;
                                btnSubmitModule.Visible = false;
                                btnUpdateModule.Visible = false;

                                disablepage = true;
                            }
                            else
                            {
                                hdnModuleStatus.Value = "";
                                btnOpenForEdit.Visible = false;
                                btnOpenForEdit.Visible = false;
                                btnSubmitModule.Visible = false;
                                btnUpdateModule.Visible = false;

                                disablepage = false;
                            }
                        }
                    }
                    else if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_DISSAPPROVE_COUNT"].ToString() != "0") //Un-Review Request Disapproved
                    {
                        if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_DISSAPPROVE_COUNT"].ToString() != "0")
                        {
                            btnOpenForEdit.Visible = false;

                            if (ds1.Tables[0].Rows[0]["REVIEWED_COUNT"].ToString() != "0")
                            {
                                hdnModuleStatus.Value = "Un-Review Request Disapproved";
                            }

                            disablepage = false;
                        }
                        else
                        {
                            hdnModuleStatus.Value = "";
                            btnOpenForEdit.Visible = false;

                            disablepage = true;
                        }
                    }
                    else if (ds1.Tables[0].Rows[0]["FREEZE"].ToString() == "True" || ds1.Tables[0].Rows[0]["UNFREEZING_REQ_GEN_COUNT"].ToString() != "0") //Frozen && Un-Freeze Request Generated
                    {
                        btnOpenForEdit.Visible = false;
                        btnSubmitModule.Visible = false;
                        btnUpdateModule.Visible = false;
                        
                        if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_GEN_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = "Un-Freeze Request Generated";
                        }

                        disablepage = true;
                    }
                    else if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_APPROVE_COUNT"].ToString() != "0") //Un-Freeze Request Approved
                    {
                        if (ds_SYSTEM_COUNT.Tables[0].Rows.Count.ToString() == ds1.Tables[0].Rows[0]["UNFREEZING_REQ_APPROVE_COUNT"].ToString())
                        {
                            btnOpenForEdit.Visible = false;

                            if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_APPROVE_COUNT"].ToString() != "0")
                            {
                                hdnModuleStatus.Value = "Un-Freeze Request Approved";
                            }

                            disablepage = true;
                        }
                        else
                        {
                            if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_GEN_COUNT"].ToString() != "0")
                            {
                                hdnModuleStatus.Value = "Un-Freeze Request Generated";
                                btnOpenForEdit.Visible = false;
                                btnSubmitModule.Visible = false;
                                btnUpdateModule.Visible = false;

                                disablepage = true;
                            }
                            else
                            {
                                hdnModuleStatus.Value = "";
                                btnOpenForEdit.Visible = false;
                                btnSubmitModule.Visible = false;
                                btnUpdateModule.Visible = false;

                                disablepage = false;
                            }
                        }
                    }
                    else if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_DISSAPPROVE_COUNT"].ToString() != "0") //Un-Freeze Request Disapproved
                    {
                        if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_DISSAPPROVE_COUNT"].ToString() != "0")
                        {
                            if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_DISSAPPROVE_COUNT"].ToString() != "0")
                            {
                                hdnModuleStatus.Value = "Un-Freeze Request Disapproved";
                            }

                            btnOpenForEdit.Visible = false;

                            disablepage = false;
                        }
                        else
                        {
                            hdnModuleStatus.Value = "";
                            btnOpenForEdit.Visible = false;
                            disablepage = true;
                        }
                    }
                    else
                    {
                        hdnModuleStatus.Value = "";
                    }

                    if (ds1.Tables[0].Rows[0]["LOGS_COUNTS"].ToString() != "0")
                    {
                        btnReviewLogs.Visible = true;
                    }
                    else
                    {
                        btnReviewLogs.Visible = false;
                    }

                    if (disablepage)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv('" + hdnModuleStatus.Value + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}