using CTMS.CommonFunction;
using IronPdf;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class View_CRF : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_DM dal_DM = new DAL_DM();
        ResourceManager rm;
        CultureInfo ci;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }
                if (!IsPostBack)
                {
                    IronPdf.License.LicenseKey = "IRONPDF.DIAGNOSEARCHLIFESCIENCESPVTLTD.IX9662-97976B320E-HB2IJFBLHG-ZXGAD4CKL4S5-KPU7RQEQJGOF-EURUO4C4EEUS-YLRHN3LM34LI-NI2AT4-LRM2AVH5MEWJEA-779997023.UNLIMITED.1YR-JA5RIV.RENEW.SUPPORT.07.MAR.2023";

                    // Set Application scope Temp Files Path.  
                    // This changes System.IO.Path.GetTempFileName and System.IO.Path.GetTempPath behavior for the entire .NET application
                    var MyTempPath = @"C:\WAI\";
                    Environment.SetEnvironmentVariable("TEMP", MyTempPath, EnvironmentVariableTarget.Process);
                    Environment.SetEnvironmentVariable("TMP", MyTempPath, EnvironmentVariableTarget.Process);
                    // Set IronPDF Temp Path
                    IronPdf.Installation.TempFolderPath = System.IO.Path.Combine(MyTempPath, "IronPdf");

                    IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;

                    IronPdf.Logging.Logger.EnableDebugging = true;
                    IronPdf.Logging.Logger.LogFilePath = "C:/WAI"; //May be set to a directory name or full file /Default.log
                    IronPdf.Logging.Logger.LoggingMode = IronPdf.Logging.Logger.LoggingModes.All;

                    IronPdf.Installation.DefaultRenderingEngine = IronPdf.Rendering.PdfRenderingEngine.Chrome;

                    ChromePdfRenderer myChromePdfRenderer = new IronPdf.ChromePdfRenderer();
                    var Renderer = new IronPdf.ChromePdfRenderer();
                    Renderer.RenderingOptions.PaperOrientation = IronPdf.Rendering.PdfPaperOrientation.Landscape;

                    Renderer.RenderingOptions.RenderDelay = 400;

                    var AspxToPdfOptions = new IronPdf.Rendering.AdaptivePdfRenderOptions()
                    {
                        Header = new SimpleHeaderFooter()
                        {
                            LeftText = Session["PROJECTIDTEXT"].ToString(),
                            RightText = "eCRF",
                            DrawDividerLine = false,
                            FontFamily = "Arial",
                            FontSize = 8
                        },
                        Footer = new SimpleHeaderFooter()
                        {
                            RightText = "Page {page} of {total-pages}",
                            FontFamily = "Arial",
                            FontSize = 8,
                        },
                        MarginLeft = 5,
                        MarginRight = 5,
                        MarginTop = 15,
                        MarginBottom = 15,
                        PaperOrientation = IronPdf.Rendering.PdfPaperOrientation.Landscape,
                        FitToPaper = true,
                    };

                    AspxToPdf.RenderThisPageAsPdf(
                        IronPdf.AspxToPdf.FileBehavior.Attachment,
                        "" + Session["PROJECTIDTEXT"].ToString() + "_" + Request.QueryString["MODULEID"].ToString()  + ".pdf",
                        AspxToPdfOptions
                   );
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GetLanguages()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            rm = new ResourceManager("eSource_VPM_SA.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
            ci = Thread.CurrentThread.CurrentCulture;
        }

        private string GetLangString(string Name)
        {
            try
            {
                return rm.GetString(Name, ci);
                //return Name;
            }
            catch (Exception ex)
            {
                return Name;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                GetLanguages();

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

                ds = dal_DM.DM_eCRF_SP(ACTION: "VIEW_CRF_GET_STRUCTURE", MODULEID: Request.QueryString["MODULEID"].ToString());

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ds.Tables[0].Rows[i]["FIELDNAME"] = GetLangString(ds.Tables[0].Rows[i]["FIELDNAME"].ToString());
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd.DataSource = ds.Tables[0];
                    grd.DataBind();
                    lblHeader.Text = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                    hfTablename.Value = ds.Tables[0].Rows[0]["ePRO_TABLENAME"].ToString();
                    hfDM_Tablename.Value = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
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

        protected void grd_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DAL dal;
                    dal = new DAL();

                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["ID"].ToString();
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

                    Label lblFieldNameFrench = (Label)e.Row.FindControl("lblFieldNameFrench");

                    lblFieldNameFrench.Text = "( " + GetLangString(VARIABLENAME) + " )";

                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        CONTROLTYPE = "RADIOBUTTON";
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

                        if (MAXLEN != "")
                        {
                            btnEdit.MaxLength = Convert.ToInt32(MAXLEN);
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
                        if (Reference == "True")
                        {
                            btnEdit.Text = "";
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            btnEdit.Text = dr["PrefixText"].ToString();
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        DataSet ds;
                        ds = new DataSet();
                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
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

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();
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

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        DataRow[] rows;

                        rows = ds.Tables[0].Select("TEXT = '--Select--'");

                        foreach (DataRow row in rows)
                            ds.Tables[0].Rows.Remove(row);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();

                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    GridView grd_Data1 = e.Row.FindControl("grd_Data1") as GridView;
                    DataSet ds1 = dal_DM.DM_eCRF_SP(ACTION: "VIEW_CRF_GET_STRUCTURE_CHILD", MODULEID: Request.QueryString["MODULEID"].ToString(), ID: ID);

                    if (ds1.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            ds1.Tables[0].Rows[i]["FIELDNAME"] = GetLangString(ds1.Tables[0].Rows[i]["FIELDNAME"].ToString());
                            ds1.Tables[0].Rows[i]["VAL_Child"] = GetLangString(ds1.Tables[0].Rows[i]["VAL_Child"].ToString());
                        }
                    }

                    grd_Data1.DataSource = ds1;
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
                DAL dal;
                dal = new DAL();

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
                    e.Row.Attributes.Add("class", "table-bordered-top");

                    Label lblFieldNameFrench = (Label)e.Row.FindControl("lblFieldNameFrench");

                    lblFieldNameFrench.Text = "( " + GetLangString(VARIABLENAME) + " )";

                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        CONTROLTYPE = "RADIOBUTTON";
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

                        if (MAXLEN != "")
                        {
                            btnEdit.MaxLength = Convert.ToInt32(MAXLEN);
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

                        }
                        if (CLASS.Contains("txtTime"))
                        {
                            btnEdit.Attributes.Add("onchange", "ValidTime(this)");
                            btnEdit.CssClass = btnEdit.CssClass + " TimeMask";
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
                            btnEdit.Text = "";
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            btnEdit.Text = dr["PrefixText"].ToString();
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        DataSet ds;
                        ds = new DataSet();
                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();


                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
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

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();
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

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        DataRow[] rows;

                        rows = ds.Tables[0].Select("TEXT = '--Select--'");

                        foreach (DataRow row in rows)
                            ds.Tables[0].Rows.Remove(row);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    GridView grd_Data2 = e.Row.FindControl("grd_Data2") as GridView;
                    DataSet ds1 = dal_DM.DM_eCRF_SP(ACTION: "VIEW_CRF_GET_STRUCTURE_CHILD", MODULEID: Request.QueryString["MODULEID"].ToString(), ID: ID);

                    if (ds1.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            ds1.Tables[0].Rows[i]["FIELDNAME"] = GetLangString(ds1.Tables[0].Rows[i]["FIELDNAME"].ToString());
                            ds1.Tables[0].Rows[i]["VAL_Child"] = GetLangString(ds1.Tables[0].Rows[i]["VAL_Child"].ToString());
                        }
                    }

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
                DAL dal;
                dal = new DAL();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    e.Row.Attributes.Add("class", "table-bordered-top");
                    string ID = dr["ID"].ToString();
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

                    Label lblFieldNameFrench = (Label)e.Row.FindControl("lblFieldNameFrench");

                    lblFieldNameFrench.Text = "( " + GetLangString(VARIABLENAME) + " )";

                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        CONTROLTYPE = "RADIOBUTTON";
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

                        if (MAXLEN != "")
                        {
                            btnEdit.MaxLength = Convert.ToInt32(MAXLEN);
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
                        if (Reference == "True")
                        {
                            btnEdit.Text = "";
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            btnEdit.Text = dr["PrefixText"].ToString();
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        DataSet ds;
                        ds = new DataSet();
                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
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

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();
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

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        DataRow[] rows;

                        rows = ds.Tables[0].Select("TEXT = '--Select--'");

                        foreach (DataRow row in rows)
                            ds.Tables[0].Rows.Remove(row);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    GridView grd_Data3 = e.Row.FindControl("grd_Data3") as GridView;
                    DataSet ds1 = dal_DM.DM_eCRF_SP(ACTION: "VIEW_CRF_GET_STRUCTURE_CHILD", MODULEID: Request.QueryString["MODULEID"].ToString(), ID: ID);

                    if (ds1.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            ds1.Tables[0].Rows[i]["FIELDNAME"] = GetLangString(ds1.Tables[0].Rows[i]["FIELDNAME"].ToString());
                            ds1.Tables[0].Rows[i]["VAL_Child"] = GetLangString(ds1.Tables[0].Rows[i]["VAL_Child"].ToString());
                        }
                    }

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
                DAL dal;
                dal = new DAL();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    e.Row.Attributes.Add("class", "table-bordered-top");
                    string ID = dr["ID"].ToString();
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

                    Label lblFieldNameFrench = (Label)e.Row.FindControl("lblFieldNameFrench");

                    lblFieldNameFrench.Text = "( " + GetLangString(VARIABLENAME) + " )";

                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        CONTROLTYPE = "RADIOBUTTON";
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

                        if (MAXLEN != "")
                        {
                            btnEdit.MaxLength = Convert.ToInt32(MAXLEN);
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
                        if (Reference == "True")
                        {
                            btnEdit.Text = "";
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            btnEdit.Text = dr["PrefixText"].ToString();
                        }
                    }
                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        DropDownList btnEdit = (DropDownList)e.Row.FindControl("DRP_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.CssClass = btnEdit.CssClass + " " + CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        DataSet ds;
                        ds = new DataSet();
                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        btnEdit.DataSource = ds;
                        btnEdit.DataTextField = "TEXT";
                        btnEdit.DataValueField = "VALUE";
                        btnEdit.DataBind();

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
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

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();
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

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        DataRow[] rows;

                        rows = ds.Tables[0].Select("TEXT = '--Select--'");

                        foreach (DataRow row in rows)
                            ds.Tables[0].Rows.Remove(row);

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["TEXT"] = GetLangString(ds.Tables[0].Rows[i]["TEXT"].ToString());
                                ds.Tables[0].Rows[i]["VALUE"] = GetLangString(ds.Tables[0].Rows[i]["VALUE"].ToString());
                            }
                        }

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();
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
    }
}