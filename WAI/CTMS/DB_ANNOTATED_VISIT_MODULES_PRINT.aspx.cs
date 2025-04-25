using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;
using IronPdf;

namespace CTMS
{
    public partial class DB_ANNOTATED_VISIT_MODULES_PRINT : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Convert.ToString(Request.QueryString["MODULEID"]) != null)
                    {
                        hdnMODULEID.Value = Request.QueryString["MODULEID"].ToString();
                    }

                    if (Convert.ToString(Request.QueryString["MODULENAME"]) != null)
                    {
                        hdnMODULENAME.Value = Request.QueryString["MODULENAME"].ToString();
                    }

                    if (Convert.ToString(Request.QueryString["VISITNUM"]) != null)
                    {
                        hdnVISITNUM.Value = Request.QueryString["VISITNUM"].ToString();
                    }

                    if (Convert.ToString(Request.QueryString["VISIT"]) != null)
                    {
                        hdnVISIT.Value = Request.QueryString["VISIT"].ToString();
                    }

                    if (Convert.ToString(Request.QueryString["SYSTEM"]) != null)
                    {
                        hdnSYSTEM.Value = Request.QueryString["SYSTEM"].ToString();
                    }

                    GetStructure();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();

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
                            LeftText = Session["PROJECTIDTEXT"].ToString() + "_" + hdnVISIT.Value.ToString().Replace("--Select--", "All Visits") + "_" + hdnMODULENAME.Value.ToString().Replace("0", "All Modules").Replace(" ", "All Modules"),
                            RightText = "Visit Wise Annotated eCRF",
                            DrawDividerLine = false,
                            FontFamily = "Arial",
                            FontSize = 8
                        },
                        Footer = new SimpleHeaderFooter()
                        {
                            LeftText = Session["User_Name"].ToString() + "_" + DateTime.Now.ToString("yyyyMMdd hh:mm tt"),
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
                        CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen,
                        PrintHtmlBackgrounds = true,
                        EnableJavaScript = true,
                        GrayScale = false,
                    };

                    AspxToPdf.RenderThisPageAsPdf(
                        IronPdf.AspxToPdf.FileBehavior.Attachment,
                        " " + Session["PROJECTIDTEXT"].ToString() + "_" + hdnVISIT.Value.ToString().Replace("--Select--", "All Visits") + "_" + hdnMODULENAME.Value.ToString().Replace("0", "All Modules").Replace(" ", "All Modules") + "-" + DateTime.Now.ToString("yyyyMMdd") + ".pdf",
                        AspxToPdfOptions
                   );
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
                DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_VISIT_MODULES_eCRF",
                    VISITNUM: hdnVISITNUM.Value,
                    MODULEID: hdnMODULEID.Value,
                    SYSTEM: hdnSYSTEM.Value
                    );

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
                    GridView grd_Data = (GridView)e.Item.FindControl("grd_Data");

                    DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_ANNOTATED",
                        MODULEID: row["MODULEID"].ToString(),
                        VISITNUM: row["VISITNUM"].ToString(),
                        SYSTEM: hdnSYSTEM.Value
                        );

                    hdnVISITNUM.Value = row["VISITNUM"].ToString();
                    hdnMODULEID.Value = row["MODULEID"].ToString();

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

                    Label lblIndications = (Label)e.Row.FindControl("lblIndications");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string FORMAT = dr["FORMAT"].ToString();
                    string PGLTYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (MANDATORY == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " *";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", *";
                            }
                        }
                    }

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-bold'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-bold'></i>";
                            }
                        }
                    }

                    if (PGLTYPE != "" && PGLTYPE != "0")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = PGLTYPE;
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", " + PGLTYPE;
                            }
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-check-circle'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-check-circle'></i>";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-eye-slash'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-eye-slash'></i>";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Row.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }

                    if (READYN == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <label style='color: maroon;font-family:cursive'>R</label>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <label style='color: maroon;font-family:cursive'>R</label>";
                            }
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " ⚠️";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", ⚠️";
                            }
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-user-md'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-user-md'></i>";
                            }
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-flask'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-flask'></i>";
                            }
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-exchange-alt''></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-exchange-alt''></i>";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i> (Editable)";
                            }
                            else
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i>";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>(Editable)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-clone'></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-clone'></i>";
                        }
                    }

                    if (CONTROLTYPE == "ChildModule")
                    {
                        Label btnEdit = (Label)e.Row.FindControl("LBL_FIELD");
                        btnEdit.Visible = true;
                        btnEdit.Text = dr["MODULENAME"].ToString();
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        Label btnEdit = (Label)e.Row.FindControl("LBL_TXT_FIELD");
                        btnEdit.Visible = true;

                        if (MULTILINEYN == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Free Text";
                            }
                            else
                            {
                                btnEdit.Text += ", Free Text";
                            }

                            btnEdit.Attributes.Add("style", "width: 300px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Uppercase";
                            }
                            else
                            {
                                btnEdit.Text += ", Uppercase";
                            }
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "DD-MMM-YYYY";
                            }
                            else
                            {
                                btnEdit.Text += ", DD-MMM-YYYY";
                            }
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "HH:MM";
                            }
                            else
                            {
                                btnEdit.Text += ", HH:MM";
                            }
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "dd/mm/yyyy";
                            }
                            else
                            {
                                btnEdit.Text += ", dd/mm/yyyy";
                            }
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Numeric";
                            }
                            else
                            {
                                btnEdit.Text += ", Numeric";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Decimal";
                            }
                            else
                            {
                                btnEdit.Text += ", Decimal";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += " (" + FORMAT + ")";
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "AN";
                            }
                            else
                            {
                                btnEdit.Text += ", AN";
                            }
                        }

                        if (MAXLEN != "0")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Max Length(" + MAXLEN + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Max Length(" + MAXLEN + ")";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                        }

                        if (DefaultData == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                            else
                            {
                                btnEdit.Text += ", Default Data: " + DefaultData;
                            }
                        }

                        if (REQUIREDYN == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REQUIRED";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (Reference == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                            if (Request["REFERENCE"] != null)
                            {
                                btnEdit.Text = Request["REFERENCE"].ToString();
                            }
                        }

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                            Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                            repeat_RAD.Visible = true;
                            DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                            foreach (DataRow row in rows)
                                ds.Tables[0].Rows.Remove(row);

                            repeat_RAD.DataSource = ds;
                            repeat_RAD.DataBind();
                        }

                        if (AutoNum == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-sort-numeric-asc'></i>";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-sort-numeric-asc'></i>";
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

                        ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_OPTIONS_LIST_VISIT",
                            VARIABLENAME: VARIABLENAME,
                            VISITNUM: hdnVISITNUM.Value,
                            ID: ID,
                            MODULEID: dr["MODULEID"].ToString()
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

                        if (REQUIREDYN == "True")
                        {
                            for (int ab = 0; ab < repeat_CHK.Items.Count; ab++)
                            {
                                ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass = ((CheckBox)repeat_CHK.Items[ab].FindControl("CHK_FIELD")).CssClass + " REQUIRED"; ;
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
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        Repeater repeat_RAD = (Repeater)e.Row.FindControl("repeat_RAD");
                        repeat_RAD.Visible = true;

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_OPTIONS_LIST_VISIT",
                            VARIABLENAME: VARIABLENAME,
                            VISITNUM: hdnVISITNUM.Value,
                            ID: ID,
                            MODULEID: dr["MODULEID"].ToString()
                            );

                        DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                        foreach (DataRow row in rows)
                            ds.Tables[0].Rows.Remove(row);

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

                        if (REQUIREDYN == "True")
                        {
                            for (int ab = 0; ab < repeat_RAD.Items.Count; ab++)
                            {
                                ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass = ((RadioButton)repeat_RAD.Items[ab].FindControl("RAD_FIELD")).CssClass + " REQUIRED"; ;
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

                        if (ParentLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(P)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(P)";
                            }
                        }

                        if (ChildLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(C)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(C)";
                            }
                        }
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
                    hdnfieldname1.Value = "";
                    string Optionname = row["TEXT"].ToString();
                    Repeater repeat_RAD1 = (Repeater)e.Item.FindControl("repeat_RAD1");
                    repeat_RAD1.Visible = true;
                    DataSet ds1 = new DataSet();
                    DataSet ds = new DataSet();

                    ds1 = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                        MODULEID: row["MODULEID"].ToString(),
                        VISITNUM: row["VISITNUM"].ToString(),
                        FIELDID: row["FIELD_ID"].ToString(),
                        OPTIONS: Optionname,
                        SYSTEM: hdnSYSTEM.Value
                        );

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
                    DataRowView dr = (DataRowView)e.Item.DataItem;
                    Repeater repeat_RAD2 = (Repeater)e.Item.FindControl("repeat_RAD2");
                    repeat_RAD2.Visible = true;
                    hdnfieldname2.Value = "";
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

                    Label lblIndications = (Label)e.Item.FindControl("lblIndications");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string FORMAT = dr["FORMAT"].ToString();
                    string PGLTYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (MANDATORY == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " *";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", *";
                            }
                        }
                    }

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-bold'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-bold'></i>";
                            }
                        }
                    }

                    if (PGLTYPE != "" && PGLTYPE != "0")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = PGLTYPE;
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", " + PGLTYPE;
                            }
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-check-circle'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-check-circle'></i>";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-eye-slash'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-eye-slash'></i>";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }

                    if (READYN == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <label style='color: maroon;font-family:cursive'>R</label>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <label style='color: maroon;font-family:cursive'>R</label>";
                            }
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " ⚠️";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", ⚠️";
                            }
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-user-md'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-user-md'></i>";
                            }
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-flask'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-flask'></i>";
                            }
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-exchange-alt''></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-exchange-alt''></i>";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i> (Editable)";
                            }
                            else
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i>";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>(Editable)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-clone'></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-clone'></i>";
                        }
                    }

                    if (CONTROLTYPE == "ChildModule")
                    {
                        if (hdnfieldname1.Value != VARIABLENAME)
                        {
                            Label btnEdit = (Label)e.Item.FindControl("LBL_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.Text = dr["MODULENAME"].ToString();

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;
                        }

                        hdnfieldname1.Value = VARIABLENAME;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        Label btnEdit = (Label)e.Item.FindControl("LBL_TXT_FIELD");
                        btnEdit.Visible = true;

                        Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                        lblSEQNO.Visible = true;

                        Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                        lblFieldName.Visible = true;

                        Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                        lblVARIABLENAME.Visible = true;

                        Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                        lbltextType.Visible = true;

                        lblIndications.Visible = true;

                        if (MULTILINEYN == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Free Text";
                            }
                            else
                            {
                                btnEdit.Text += ", Free Text";
                            }

                            btnEdit.Attributes.Add("style", "width: 300px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Uppercase";
                            }
                            else
                            {
                                btnEdit.Text += ", Uppercase";
                            }
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "DD-MMM-YYYY";
                            }
                            else
                            {
                                btnEdit.Text += ", DD-MMM-YYYY";
                            }
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "HH:MM";
                            }
                            else
                            {
                                btnEdit.Text += ", HH:MM";
                            }
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "dd/mm/yyyy";
                            }
                            else
                            {
                                btnEdit.Text += ", dd/mm/yyyy";
                            }
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Numeric";
                            }
                            else
                            {
                                btnEdit.Text += ", Numeric";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Decimal";
                            }
                            else
                            {
                                btnEdit.Text += ", Decimal";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += " (" + FORMAT + ")";
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "AN";
                            }
                            else
                            {
                                btnEdit.Text += ", AN";
                            }
                        }

                        if (MAXLEN != "0")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Max Length(" + MAXLEN + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Max Length(" + MAXLEN + ")";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                        }

                        if (DefaultData == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                            else
                            {
                                btnEdit.Text += ", Default Data: " + DefaultData;
                            }
                        }

                        if (REQUIREDYN == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REQUIRED";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (Reference == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                            if (Request["REFERENCE"] != null)
                            {
                                btnEdit.Text = Request["REFERENCE"].ToString();
                            }
                        }

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                            DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                            foreach (DataRow row in rows)
                                ds.Tables[0].Rows.Remove(row);

                            repeat_RAD2.DataSource = ds;
                            repeat_RAD2.DataBind();
                        }

                        if (AutoNum == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-sort-numeric-asc'></i>";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-sort-numeric-asc'></i>";
                            }
                        }

                        DataSet ds1;
                        ds1 = new DataSet();
                        hdnfieldname1.Value = VARIABLENAME;
                        ds1 = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds1.Tables[0].Columns.Add(newColumn);

                        repeat_RAD2.DataSource = ds1;
                        repeat_RAD2.DataBind();
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        CheckBox CHK_FIELD = (CheckBox)e.Item.FindControl("CHK_FIELD");
                        CHK_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            CHK_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (CHK_FIELD.Text.ToString() == DefaultData)
                            {
                                CHK_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname1.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }
                        DataSet ds;
                        ds = new DataSet();
                        hdnfieldname1.Value = VARIABLENAME;
                        ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD2.DataSource = ds;
                        repeat_RAD2.DataBind();
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        RadioButton RAD_FIELD = (RadioButton)e.Item.FindControl("RAD_FIELD");
                        RAD_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            RAD_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (RAD_FIELD.Text.ToString() == DefaultData)
                            {
                                RAD_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname1.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }

                        DataSet ds;
                        ds = new DataSet();
                        hdnfieldname1.Value = VARIABLENAME;
                        ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                        foreach (DataRow row in rows)
                            ds.Tables[0].Rows.Remove(row);

                        repeat_RAD2.DataSource = ds;
                        repeat_RAD2.DataBind();

                        if (ParentLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(P)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(P)";
                            }
                        }

                        if (ChildLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(C)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(C)";
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
                    DataRowView dr = (DataRowView)e.Item.DataItem;
                    Repeater repeat_RAD3 = (Repeater)e.Item.FindControl("repeat_RAD3");
                    repeat_RAD3.Visible = true;
                    hdnfieldname3.Value = "";
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

                    Label lblIndications = (Label)e.Item.FindControl("lblIndications");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string FORMAT = dr["FORMAT"].ToString();
                    string PGLTYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (MANDATORY == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " *";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", *";
                            }
                        }
                    }

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-bold'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-bold'></i>";
                            }
                        }
                    }

                    if (PGLTYPE != "" && PGLTYPE != "0")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = PGLTYPE;
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", " + PGLTYPE;
                            }
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-check-circle'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-check-circle'></i>";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-eye-slash'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-eye-slash'></i>";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }

                    if (READYN == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <label style='color: maroon;font-family:cursive'>R</label>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <label style='color: maroon;font-family:cursive'>R</label>";
                            }
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " ⚠️";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", ⚠️";
                            }
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-user-md'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-user-md'></i>";
                            }
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-flask'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-flask'></i>";
                            }
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-exchange-alt''></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-exchange-alt''></i>";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i> (Editable)";
                            }
                            else
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i>";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>(Editable)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-clone'></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-clone'></i>";
                        }
                    }

                    if (CONTROLTYPE == "ChildModule")
                    {
                        if (hdnfieldname2.Value != VARIABLENAME)
                        {
                            Label btnEdit = (Label)e.Item.FindControl("LBL_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.Text = dr["MODULENAME"].ToString();

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;
                        }

                        hdnfieldname2.Value = VARIABLENAME;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        Label btnEdit = (Label)e.Item.FindControl("LBL_TXT_FIELD");
                        btnEdit.Visible = true;

                        Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                        lblSEQNO.Visible = true;

                        Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                        lblFieldName.Visible = true;

                        Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                        lblVARIABLENAME.Visible = true;

                        Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                        lbltextType.Visible = true;

                        lblIndications.Visible = true;

                        if (MULTILINEYN == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Free Text";
                            }
                            else
                            {
                                btnEdit.Text += ", Free Text";
                            }

                            btnEdit.Attributes.Add("style", "width: 300px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Uppercase";
                            }
                            else
                            {
                                btnEdit.Text += ", Uppercase";
                            }
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "DD-MMM-YYYY";
                            }
                            else
                            {
                                btnEdit.Text += ", DD-MMM-YYYY";
                            }
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "HH:MM";
                            }
                            else
                            {
                                btnEdit.Text += ", HH:MM";
                            }
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "dd/mm/yyyy";
                            }
                            else
                            {
                                btnEdit.Text += ", dd/mm/yyyy";
                            }
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Numeric";
                            }
                            else
                            {
                                btnEdit.Text += ", Numeric";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Decimal";
                            }
                            else
                            {
                                btnEdit.Text += ", Decimal";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += " (" + FORMAT + ")";
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "AN";
                            }
                            else
                            {
                                btnEdit.Text += ", AN";
                            }
                        }

                        if (MAXLEN != "0")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Max Length(" + MAXLEN + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Max Length(" + MAXLEN + ")";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                        }

                        if (DefaultData == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                            else
                            {
                                btnEdit.Text += ", Default Data: " + DefaultData;
                            }
                        }

                        if (REQUIREDYN == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REQUIRED";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (Reference == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                            if (Request["REFERENCE"] != null)
                            {
                                btnEdit.Text = Request["REFERENCE"].ToString();
                            }
                        }

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                            DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                            foreach (DataRow row in rows)
                                ds.Tables[0].Rows.Remove(row);

                            repeat_RAD3.DataSource = ds;
                            repeat_RAD3.DataBind();
                        }

                        if (AutoNum == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-sort-numeric-asc'></i>";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-sort-numeric-asc'></i>";
                            }
                        }

                        DataSet ds1 = new DataSet();
                        hdnfieldname2.Value = VARIABLENAME;
                        ds1 = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds1.Tables[0].Columns.Add(newColumn);

                        repeat_RAD3.DataSource = ds1;
                        repeat_RAD3.DataBind();
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        CheckBox CHK_FIELD = (CheckBox)e.Item.FindControl("CHK_FIELD");
                        CHK_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            CHK_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (CHK_FIELD.Text.ToString() == DefaultData)
                            {
                                CHK_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname2.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }
                        DataSet ds;
                        ds = new DataSet();
                        hdnfieldname2.Value = VARIABLENAME;
                        ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD3.DataSource = ds;
                        repeat_RAD3.DataBind();
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        RadioButton RAD_FIELD = (RadioButton)e.Item.FindControl("RAD_FIELD");
                        RAD_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            RAD_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (RAD_FIELD.Text.ToString() == DefaultData)
                            {
                                RAD_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname2.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }

                        DataSet ds;
                        ds = new DataSet();
                        hdnfieldname2.Value = VARIABLENAME;
                        ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                        foreach (DataRow row in rows)
                            ds.Tables[0].Rows.Remove(row);

                        repeat_RAD3.DataSource = ds;
                        repeat_RAD3.DataBind();

                        if (ParentLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(P)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(P)";
                            }
                        }

                        if (ChildLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(C)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(C)";
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
                    DataRowView dr = (DataRowView)e.Item.DataItem;

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

                    Label lblIndications = (Label)e.Item.FindControl("lblIndications");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string FORMAT = dr["FORMAT"].ToString();
                    string PGLTYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (MANDATORY == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " *";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", *";
                            }
                        }
                    }

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-bold'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-bold'></i>";
                            }
                        }
                    }

                    if (PGLTYPE != "" && PGLTYPE != "0")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = PGLTYPE;
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", " + PGLTYPE;
                            }
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-check-circle'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-check-circle'></i>";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-eye-slash'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-eye-slash'></i>";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }

                    if (READYN == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <label style='color: maroon;font-family:cursive'>R</label>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <label style='color: maroon;font-family:cursive'>R</label>";
                            }
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " ⚠️";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", ⚠️";
                            }
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-user-md'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-user-md'></i>";
                            }
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-flask'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-flask'></i>";
                            }
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-exchange-alt''></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-exchange-alt''></i>";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i> (Editable)";
                            }
                            else
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i>";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>(Editable)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-clone'></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-clone'></i>";
                        }
                    }

                    if (CONTROLTYPE == "ChildModule")
                    {
                        if (hdnfieldname3.Value != VARIABLENAME)
                        {
                            Label btnEdit = (Label)e.Item.FindControl("LBL_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.Text = dr["MODULENAME"].ToString();

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;
                        }

                        hdnfieldname3.Value = VARIABLENAME;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        Label btnEdit = (Label)e.Item.FindControl("LBL_TXT_FIELD");
                        btnEdit.Visible = true;

                        Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                        lblSEQNO.Visible = true;

                        Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                        lblFieldName.Visible = true;

                        Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                        lblVARIABLENAME.Visible = true;

                        Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                        lbltextType.Visible = true;

                        lblIndications.Visible = true;

                        if (MULTILINEYN == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Free Text";
                            }
                            else
                            {
                                btnEdit.Text += ", Free Text";
                            }

                            btnEdit.Attributes.Add("style", "width: 300px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Uppercase";
                            }
                            else
                            {
                                btnEdit.Text += ", Uppercase";
                            }
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "DD-MMM-YYYY";
                            }
                            else
                            {
                                btnEdit.Text += ", DD-MMM-YYYY";
                            }
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "HH:MM";
                            }
                            else
                            {
                                btnEdit.Text += ", HH:MM";
                            }
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "dd/mm/yyyy";
                            }
                            else
                            {
                                btnEdit.Text += ", dd/mm/yyyy";
                            }
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Numeric";
                            }
                            else
                            {
                                btnEdit.Text += ", Numeric";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Decimal";
                            }
                            else
                            {
                                btnEdit.Text += ", Decimal";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += " (" + FORMAT + ")";
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "AN";
                            }
                            else
                            {
                                btnEdit.Text += ", AN";
                            }
                        }

                        if (MAXLEN != "0")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Max Length(" + MAXLEN + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Max Length(" + MAXLEN + ")";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                        }

                        if (DefaultData == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                            else
                            {
                                btnEdit.Text += ", Default Data: " + DefaultData;
                            }
                        }

                        if (REQUIREDYN == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REQUIRED";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (Reference == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                            if (Request["REFERENCE"] != null)
                            {
                                btnEdit.Text = Request["REFERENCE"].ToString();
                            }
                        }

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                            Repeater repeat_RAD = (Repeater)e.Item.FindControl("repeat_RAD");
                            repeat_RAD.Visible = true;
                            DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                            foreach (DataRow row in rows)
                                ds.Tables[0].Rows.Remove(row);

                            repeat_RAD.DataSource = ds;
                            repeat_RAD.DataBind();
                        }

                        if (AutoNum == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-sort-numeric-asc'></i>";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-sort-numeric-asc'></i>";
                            }
                        }
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        CheckBox CHK_FIELD = (CheckBox)e.Item.FindControl("CHK_FIELD");
                        CHK_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            CHK_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (CHK_FIELD.Text.ToString() == DefaultData)
                            {
                                CHK_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname3.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }

                        hdnfieldname3.Value = VARIABLENAME;
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        RadioButton RAD_FIELD = (RadioButton)e.Item.FindControl("RAD_FIELD");
                        RAD_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            RAD_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (RAD_FIELD.Text.ToString() == DefaultData)
                            {
                                RAD_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname3.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }

                        hdnfieldname3.Value = VARIABLENAME;

                        if (ParentLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(P)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(P)";
                            }
                        }

                        if (ChildLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(C)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(C)";
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
                    hdnfieldname1.Value = "";
                    Repeater repeat_CHK1 = (Repeater)e.Item.FindControl("repeat_CHK1");
                    repeat_CHK1.Visible = true;
                    DataSet ds1 = new DataSet();
                    DataSet ds = new DataSet();

                    ds1 = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                        MODULEID: row["MODULEID"].ToString(),
                        VISITNUM: row["VISITNUM"].ToString(),
                        FIELDID: row["FIELD_ID"].ToString(),
                        OPTIONS: Optionname,
                        SYSTEM: hdnSYSTEM.Value
                        );

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
                    DataRowView dr = (DataRowView)e.Item.DataItem;
                    Repeater repeat_CHK2 = (Repeater)e.Item.FindControl("repeat_CHK2");
                    repeat_CHK2.Visible = true;
                    hdnfieldname2.Value = "";
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

                    Label lblIndications = (Label)e.Item.FindControl("lblIndications");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string FORMAT = dr["FORMAT"].ToString();
                    string PGLTYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (MANDATORY == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " *";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", *";
                            }
                        }
                    }

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-bold'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-bold'></i>";
                            }
                        }
                    }

                    if (PGLTYPE != "" && PGLTYPE != "0")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = PGLTYPE;
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", " + PGLTYPE;
                            }
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-check-circle'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-check-circle'></i>";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-eye-slash'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-eye-slash'></i>";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }

                    if (READYN == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <label style='color: maroon;font-family:cursive'>R</label>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <label style='color: maroon;font-family:cursive'>R</label>";
                            }
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " ⚠️";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", ⚠️";
                            }
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-user-md'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-user-md'></i>";
                            }
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-flask'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-flask'></i>";
                            }
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-exchange-alt''></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-exchange-alt''></i>";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i> (Editable)";
                            }
                            else
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i>";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>(Editable)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-clone'></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-clone'></i>";
                        }
                    }

                    if (CONTROLTYPE == "ChildModule")
                    {
                        if (hdnfieldname1.Value != VARIABLENAME)
                        {
                            Label btnEdit = (Label)e.Item.FindControl("LBL_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.Text = dr["MODULENAME"].ToString();

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;
                        }

                        hdnfieldname1.Value = VARIABLENAME;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        Label btnEdit = (Label)e.Item.FindControl("LBL_TXT_FIELD");
                        btnEdit.Visible = true;

                        Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                        lblSEQNO.Visible = true;

                        Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                        lblFieldName.Visible = true;

                        Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                        lblVARIABLENAME.Visible = true;

                        Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                        lbltextType.Visible = true;

                        lblIndications.Visible = true;

                        if (MULTILINEYN == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Free Text";
                            }
                            else
                            {
                                btnEdit.Text += ", Free Text";
                            }

                            btnEdit.Attributes.Add("style", "width: 300px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Uppercase";
                            }
                            else
                            {
                                btnEdit.Text += ", Uppercase";
                            }
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "DD-MMM-YYYY";
                            }
                            else
                            {
                                btnEdit.Text += ", DD-MMM-YYYY";
                            }
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "HH:MM";
                            }
                            else
                            {
                                btnEdit.Text += ", HH:MM";
                            }
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "dd/mm/yyyy";
                            }
                            else
                            {
                                btnEdit.Text += ", dd/mm/yyyy";
                            }
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Numeric";
                            }
                            else
                            {
                                btnEdit.Text += ", Numeric";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Decimal";
                            }
                            else
                            {
                                btnEdit.Text += ", Decimal";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += " (" + FORMAT + ")";
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "AN";
                            }
                            else
                            {
                                btnEdit.Text += ", AN";
                            }
                        }

                        if (MAXLEN != "0")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Max Length(" + MAXLEN + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Max Length(" + MAXLEN + ")";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                        }

                        if (DefaultData == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                            else
                            {
                                btnEdit.Text += ", Default Data: " + DefaultData;
                            }
                        }

                        if (REQUIREDYN == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REQUIRED";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (Reference == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                            if (Request["REFERENCE"] != null)
                            {
                                btnEdit.Text = Request["REFERENCE"].ToString();
                            }
                        }

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                            DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                            foreach (DataRow row in rows)
                                ds.Tables[0].Rows.Remove(row);

                            repeat_CHK2.DataSource = ds;
                            repeat_CHK2.DataBind();
                        }

                        if (AutoNum == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-sort-numeric-asc'></i>";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-sort-numeric-asc'></i>";
                            }
                        }

                        DataSet ds1 = new DataSet();
                        hdnfieldname2.Value = VARIABLENAME;
                        ds1 = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds1.Tables[0].Columns.Add(newColumn);

                        repeat_CHK2.DataSource = ds1;
                        repeat_CHK2.DataBind();
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        CheckBox CHK_FIELD = (CheckBox)e.Item.FindControl("CHK_FIELD");
                        CHK_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            CHK_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (CHK_FIELD.Text.ToString() == DefaultData)
                            {
                                CHK_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname1.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }

                        DataSet ds;
                        ds = new DataSet();
                        hdnfieldname1.Value = VARIABLENAME;
                        ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK2.DataSource = ds;
                        repeat_CHK2.DataBind();
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        RadioButton RAD_FIELD = (RadioButton)e.Item.FindControl("RAD_FIELD");
                        RAD_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            RAD_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (RAD_FIELD.Text.ToString() == DefaultData)
                            {
                                RAD_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname1.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }
                        DataSet ds;
                        ds = new DataSet();
                        hdnfieldname1.Value = VARIABLENAME;
                        ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                        foreach (DataRow row in rows)
                            ds.Tables[0].Rows.Remove(row);

                        repeat_CHK2.DataSource = ds;
                        repeat_CHK2.DataBind();

                        if (ParentLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(P)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(P)";
                            }
                        }

                        if (ChildLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(C)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(C)";
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
                    DataRowView dr = (DataRowView)e.Item.DataItem;
                    Repeater repeat_CHK3 = (Repeater)e.Item.FindControl("repeat_CHK3");
                    repeat_CHK3.Visible = true;
                    hdnfieldname3.Value = "";
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

                    Label lblIndications = (Label)e.Item.FindControl("lblIndications");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string FORMAT = dr["FORMAT"].ToString();
                    string PGLTYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (MANDATORY == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " *";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", *";
                            }
                        }
                    }

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-bold'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-bold'></i>";
                            }
                        }
                    }

                    if (PGLTYPE != "" && PGLTYPE != "0")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = PGLTYPE;
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", " + PGLTYPE;
                            }
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-check-circle'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-check-circle'></i>";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-eye-slash'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-eye-slash'></i>";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }

                    if (READYN == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <label style='color: maroon;font-family:cursive'>R</label>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <label style='color: maroon;font-family:cursive'>R</label>";
                            }
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " ⚠️";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", ⚠️";
                            }
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-user-md'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-user-md'></i>";
                            }
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-flask'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-flask'></i>";
                            }
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-exchange-alt''></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-exchange-alt''></i>";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i> (Editable)";
                            }
                            else
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i>";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>(Editable)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-clone'></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-clone'></i>";
                        }
                    }

                    if (CONTROLTYPE == "ChildModule")
                    {
                        if (hdnfieldname2.Value != VARIABLENAME)
                        {
                            Label btnEdit = (Label)e.Item.FindControl("LBL_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.Text = dr["MODULENAME"].ToString();

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;
                        }

                        hdnfieldname2.Value = VARIABLENAME;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        Label btnEdit = (Label)e.Item.FindControl("LBL_TXT_FIELD");
                        btnEdit.Visible = true;

                        Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                        lblSEQNO.Visible = true;

                        Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                        lblFieldName.Visible = true;

                        Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                        lblVARIABLENAME.Visible = true;

                        Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                        lbltextType.Visible = true;

                        lblIndications.Visible = true;

                        if (MULTILINEYN == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Free Text";
                            }
                            else
                            {
                                btnEdit.Text += ", Free Text";
                            }

                            btnEdit.Attributes.Add("style", "width: 300px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Uppercase";
                            }
                            else
                            {
                                btnEdit.Text += ", Uppercase";
                            }
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "DD-MMM-YYYY";
                            }
                            else
                            {
                                btnEdit.Text += ", DD-MMM-YYYY";
                            }
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "HH:MM";
                            }
                            else
                            {
                                btnEdit.Text += ", HH:MM";
                            }
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "dd/mm/yyyy";
                            }
                            else
                            {
                                btnEdit.Text += ", dd/mm/yyyy";
                            }
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Numeric";
                            }
                            else
                            {
                                btnEdit.Text += ", Numeric";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Decimal";
                            }
                            else
                            {
                                btnEdit.Text += ", Decimal";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += " (" + FORMAT + ")";
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "AN";
                            }
                            else
                            {
                                btnEdit.Text += ", AN";
                            }
                        }

                        if (MAXLEN != "0")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Max Length(" + MAXLEN + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Max Length(" + MAXLEN + ")";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                        }

                        if (DefaultData == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                            else
                            {
                                btnEdit.Text += ", Default Data: " + DefaultData;
                            }
                        }

                        if (REQUIREDYN == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REQUIRED";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (Reference == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                            if (Request["REFERENCE"] != null)
                            {
                                btnEdit.Text = Request["REFERENCE"].ToString();
                            }
                        }

                        if (CLASS == "OptionValues form-control")
                        {
                            DataSet ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                            DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                            foreach (DataRow row in rows)
                                ds.Tables[0].Rows.Remove(row);

                            repeat_CHK3.DataSource = ds;
                            repeat_CHK3.DataBind();
                        }

                        if (AutoNum == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-sort-numeric-asc'></i>";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-sort-numeric-asc'></i>";
                            }
                        }

                        DataSet ds1 = new DataSet();
                        hdnfieldname2.Value = VARIABLENAME;
                        ds1 = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds1.Tables[0].Columns.Add(newColumn);

                        repeat_CHK3.DataSource = ds1;
                        repeat_CHK3.DataBind();
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        CheckBox CHK_FIELD = (CheckBox)e.Item.FindControl("CHK_FIELD");
                        CHK_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            CHK_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (CHK_FIELD.Text.ToString() == DefaultData)
                            {
                                CHK_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname2.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }

                        DataSet ds;
                        ds = new DataSet();
                        hdnfieldname2.Value = VARIABLENAME;
                        ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK3.DataSource = ds;
                        repeat_CHK3.DataBind();
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        RadioButton RAD_FIELD = (RadioButton)e.Item.FindControl("RAD_FIELD");
                        RAD_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            RAD_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (RAD_FIELD.Text.ToString() == DefaultData)
                            {
                                RAD_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname2.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }

                        DataSet ds;
                        ds = new DataSet();
                        hdnfieldname2.Value = VARIABLENAME;
                        ds = dal_DB.DB_VISIT_ANNOTATED_PRINT_SP(ACTION: "GET_STRUCTURE_CHILD_ANNOTATED",
                                MODULEID: dr["MODULEID"].ToString(),
                                VISITNUM: dr["VISITNUM"].ToString(),
                                FIELDID: dr["FIELD_ID"].ToString(),
                                OPTIONS: dr["TEXT"].ToString(),
                                SYSTEM: hdnSYSTEM.Value
                                );

                        DataRow[] rows = ds.Tables[0].Select("TEXT = '--Select--' OR TEXT = '---Select---'");
                        foreach (DataRow row in rows)
                            ds.Tables[0].Rows.Remove(row);

                        repeat_CHK3.DataSource = ds;
                        repeat_CHK3.DataBind();

                        if (ParentLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(P)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(P)";
                            }
                        }

                        if (ChildLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(C)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(C)";
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
                    DataRowView dr = (DataRowView)e.Item.DataItem;

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

                    Label lblIndications = (Label)e.Item.FindControl("lblIndications");
                    string INVISIBLE = dr["INVISIBLE"].ToString();
                    string REQUIREDYN = dr["REQUIREDYN"].ToString();
                    string Critic_DP = dr["Critic_DP"].ToString();
                    string MEDOP = dr["MEDOP"].ToString();
                    string AUTOCODE = dr["AUTOCODE"].ToString();
                    string AutoCodeLIB = dr["AutoCodeLIB"].ToString();
                    string NONREPETATIVE = dr["NONREPETATIVE"].ToString();
                    string InList = dr["InList"].ToString();
                    string InListEditable = dr["InListEditable"].ToString();
                    string DUPLICATE = dr["DUPLICATE"].ToString();
                    string FORMAT = dr["FORMAT"].ToString();
                    string PGLTYPE = dr["PGL_TYPE"].ToString();
                    string SDV = dr["SDV"].ToString();

                    if (MANDATORY == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " *";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", *";
                            }
                        }
                    }

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";

                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-bold'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-bold'></i>";
                            }
                        }
                    }

                    if (PGLTYPE != "" && PGLTYPE != "0")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = PGLTYPE;
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", " + PGLTYPE;
                            }
                        }
                    }

                    if (SDV == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-check-circle'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-check-circle'></i>";
                            }
                        }
                    }

                    if (INVISIBLE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-eye-slash'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-eye-slash'></i>";
                            }
                        }
                    }

                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }

                    if (READYN == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <label style='color: maroon;font-family:cursive'>R</label>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <label style='color: maroon;font-family:cursive'>R</label>";
                            }
                        }
                    }

                    if (Critic_DP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " ⚠️";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", ⚠️";
                            }
                        }
                    }

                    if (MEDOP == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-user-md'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-user-md'></i>";
                            }
                        }
                    }

                    if (LabData == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-flask'></i>";
                        }
                        else
                        {
                            {
                                lblIndications.Text += ", <i class='fa fa-flask'></i>";
                            }
                        }
                    }

                    if (AutoCodeLIB != "")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-desktop'></i> " + "(" + AutoCodeLIB + ")";
                        }
                    }

                    if (NONREPETATIVE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-exchange-alt''></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-exchange-alt''></i>";
                        }
                    }

                    if (InList == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i> (Editable)";
                            }
                            else
                            {
                                lblIndications.Text = " <i class='fa fa-list'></i>";
                            }
                        }
                        else
                        {
                            if (InListEditable == "True")
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>(Editable)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-list'></i>";
                            }
                        }
                    }

                    if (DUPLICATE == "True")
                    {
                        if (lblIndications.Text == "")
                        {
                            lblIndications.Text = " <i class='fa fa-clone'></i>";
                        }
                        else
                        {
                            lblIndications.Text += ", <i class='fa fa-clone'></i>";
                        }
                    }

                    if (CONTROLTYPE == "ChildModule")
                    {
                        if (hdnfieldname3.Value != VARIABLENAME)
                        {
                            Label btnEdit = (Label)e.Item.FindControl("LBL_FIELD");
                            btnEdit.Visible = true;
                            btnEdit.Text = dr["MODULENAME"].ToString();

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;
                        }

                        hdnfieldname3.Value = VARIABLENAME;
                    }

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        Label btnEdit = (Label)e.Item.FindControl("LBL_TXT_FIELD");
                        btnEdit.Visible = true;

                        Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                        lblSEQNO.Visible = true;

                        Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                        lblFieldName.Visible = true;

                        Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                        lblVARIABLENAME.Visible = true;

                        Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                        lbltextType.Visible = true;

                        lblIndications.Visible = true;

                        if (MULTILINEYN == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Free Text";
                            }
                            else
                            {
                                btnEdit.Text += ", Free Text";
                            }

                            btnEdit.Attributes.Add("style", "width: 300px;height: 40px;margin-bottom: 4px;");
                        }

                        if (UPPERCASE == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Uppercase";
                            }
                            else
                            {
                                btnEdit.Text += ", Uppercase";
                            }
                        }

                        if (CLASS == "txtDateNoFuture form-control" || CLASS == "txtDate form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "DD-MMM-YYYY";
                            }
                            else
                            {
                                btnEdit.Text += ", DD-MMM-YYYY";
                            }
                        }
                        else if (CLASS == "txtTime form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "HH:MM";
                            }
                            else
                            {
                                btnEdit.Text += ", HH:MM";
                            }
                        }
                        else if (CLASS == "txtDateMask form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "dd/mm/yyyy";
                            }
                            else
                            {
                                btnEdit.Text += ", dd/mm/yyyy";
                            }
                        }
                        else if (CLASS == "numeric form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Numeric";
                            }
                            else
                            {
                                btnEdit.Text += ", Numeric";
                            }
                        }
                        else if (CLASS == "numericdecimal form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Decimal";
                            }
                            else
                            {
                                btnEdit.Text += ", Decimal";
                            }

                            if (FORMAT != "")
                            {
                                btnEdit.Text += " (" + FORMAT + ")";
                            }
                        }
                        else if (CLASS == "form-control")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "AN";
                            }
                            else
                            {
                                btnEdit.Text += ", AN";
                            }
                        }

                        if (MAXLEN != "0")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Max Length(" + MAXLEN + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Max Length(" + MAXLEN + ")";
                            }
                        }

                        string Prefix = dr["Prefix"].ToString();

                        if (Prefix == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = "Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                            else
                            {
                                btnEdit.Text += ", Prefix : (" + dr["PrefixText"].ToString() + ")";
                            }
                        }

                        if (DefaultData == "True")
                        {
                            if (btnEdit.Text == "")
                            {
                                btnEdit.Text = DefaultData;
                            }
                            else
                            {
                                btnEdit.Text += ", Default Data: " + DefaultData;
                            }
                        }

                        if (REQUIREDYN == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REQUIRED";
                        }

                        if (READYN == "True")
                        {
                            btnEdit.Enabled = false;
                        }

                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }

                        if (MANDATORY == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " Mandatory";
                        }

                        if (Reference == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " REFERENCE";

                            if (Request["REFERENCE"] != null)
                            {
                                btnEdit.Text = Request["REFERENCE"].ToString();
                            }
                        }

                        if (AutoNum == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-sort-numeric-asc'></i>";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-sort-numeric-asc'></i>";
                            }
                        }
                    }
                    if (CONTROLTYPE == "CHECKBOX")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        CheckBox CHK_FIELD = (CheckBox)e.Item.FindControl("CHK_FIELD");
                        CHK_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            CHK_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            CHK_FIELD.CssClass = CHK_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (CHK_FIELD.Text.ToString() == DefaultData)
                            {
                                CHK_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname3.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }

                        hdnfieldname3.Value = VARIABLENAME;
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        RadioButton RAD_FIELD = (RadioButton)e.Item.FindControl("RAD_FIELD");
                        RAD_FIELD.Visible = true;

                        if (READYN == "True")
                        {
                            RAD_FIELD.Enabled = false;
                        }

                        if (MANDATORY == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " Mandatory";
                        }

                        if (REQUIREDYN == "True")
                        {
                            RAD_FIELD.CssClass = RAD_FIELD.CssClass + " REQUIRED";
                        }

                        if (DefaultData != "")
                        {
                            if (RAD_FIELD.Text.ToString() == DefaultData)
                            {
                                RAD_FIELD.Checked = true;
                            }
                        }

                        Label lblOPTIONSEQNO = (Label)e.Item.FindControl("lblOPTIONSEQNO");
                        lblOPTIONSEQNO.Visible = true;

                        if (hdnfieldname3.Value != VARIABLENAME)
                        {
                            Label lblSEQNO = (Label)e.Item.FindControl("lblSEQNO");
                            lblSEQNO.Visible = true;

                            Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
                            lblFieldName.Visible = true;

                            Label lblVARIABLENAME = (Label)e.Item.FindControl("lblVARIABLENAME");
                            lblVARIABLENAME.Visible = true;

                            Label lbltextType = (Label)e.Item.FindControl("lbltextType");
                            lbltextType.Visible = true;

                            lblIndications.Visible = true;
                        }

                        hdnfieldname3.Value = VARIABLENAME;

                        if (ParentLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(P)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(P)";
                            }
                        }

                        if (ChildLinked == "True")
                        {
                            if (lblIndications.Text == "")
                            {
                                lblIndications.Text = " <i class='fa fa-link'></i>(C)";
                            }
                            else
                            {
                                lblIndications.Text += ", <i class='fa fa-link'></i>(C)";
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