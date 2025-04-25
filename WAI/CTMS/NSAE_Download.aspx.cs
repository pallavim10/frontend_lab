using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IronPdf;
using IronPdf.Security;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class NSAE_Download : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_MODULE();
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
                            LeftText = Session["PROJECTIDTEXT"].ToString() + " - " + Request.QueryString["SAEID"].ToString() + " - " + Request.QueryString["STATUS"].ToString(),
                            RightText = "Subject SAE Data",
                            DrawDividerLine = false,
                            FontFamily = "Arial",
                            FontSize = 8
                        },
                        Footer = new SimpleHeaderFooter()
                        {
                            LeftText = Session["User_Name"].ToString() + " - " + DateTime.Now.ToString("yyyyMMdd"),
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
                        "" + Request.QueryString["SAEID"].ToString() + " - " + Request.QueryString["STATUS"].ToString() + "-" + DateTime.Now.ToString("yyyyMMdd") + ".pdf",
                        AspxToPdfOptions
                   );
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void GET_MODULE()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_PRINT_REPORT_SP(ACTION: "GET_ANOTATED_SAE_PV",
                SUBJID: Request.QueryString["SUBJID"].ToString(),
                SITEID: Request.QueryString["SITEID"].ToString(),
                SAEID: Request.QueryString["SAEID"].ToString(),
                STATUS: Request.QueryString["STATUS"].ToString()
                );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeat_AllModule.DataSource = ds;
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
                lblErrorMsg.Text = ex.Message.ToString();
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

                    DataSet ds_grd_Data = dal_SAE.SAE_PRINT_REPORT_SP(
                        ACTION: "GET_STRUCTURE",
                        MODULEID: row["MODULEID"].ToString(),
                        SAEID: row["SAEID"].ToString(),
                        SUBJID: row["SUBJID"].ToString(),
                        RECID: row["RECID"].ToString()
                        );

                    if (ds_grd_Data.Tables[0].Rows.Count > 0)
                    {
                        grd_Data.DataSource = ds_grd_Data;
                        grd_Data.DataBind();
                    }
                    else
                    {
                        grd_Data.DataSource = null;
                        grd_Data.DataBind();
                    }

                    GridView grdAudit = (GridView)e.Item.FindControl("grdAudit");
                    HtmlTableRow TRAUDIT = (HtmlTableRow)e.Item.FindControl("TRAUDIT");

                    GridView grdQuery = (GridView)e.Item.FindControl("grdQuery");
                    HtmlTableRow TRQUERY = (HtmlTableRow)e.Item.FindControl("TRQUERY");

                    GridView grdQryComment = (GridView)e.Item.FindControl("grdQryComment");
                    HtmlTableRow TRQUERY_COMMENT = (HtmlTableRow)e.Item.FindControl("TRQUERY_COMMENT");

                    GridView grdEventLogs = (GridView)e.Item.FindControl("grdEventLogs");
                    HtmlTableRow TREvent_Logs = (HtmlTableRow)e.Item.FindControl("TREvent_Logs");

                    GridView grdFieldComment = (GridView)e.Item.FindControl("grdFieldComment");
                    HtmlTableRow TRFIELDCOMMENT = (HtmlTableRow)e.Item.FindControl("TRFIELDCOMMENT");

                    GridView grdModuleComment = (GridView)e.Item.FindControl("grdModuleComment");
                    HtmlTableRow TRPAGECOMMENT = (HtmlTableRow)e.Item.FindControl("TRPAGECOMMENT");

                    GridView grdDocs = (GridView)e.Item.FindControl("grdDocs");
                    HtmlTableRow TRDOCS = (HtmlTableRow)e.Item.FindControl("TRDOCS");

                    DataSet ds1 = dal_SAE.SAE_PRINT_REPORT_SP(ACTION: "GET_DOC_QUR_AUDIT_SIGNOFF",
                    SAEID: row["SAEID"].ToString(),
                    RECID: row["RECID"].ToString(),
                    MODULEID: row["MODULEID"].ToString()
                    );

                    //Audit Data
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        grdAudit.DataSource = ds1.Tables[0];
                        grdAudit.DataBind();
                        TRAUDIT.Visible = true;
                    }
                    else
                    {
                        grdAudit.DataSource = null;
                        grdAudit.DataBind();
                        TRAUDIT.Visible = false;
                    }

                    //Query Data
                    if (ds1.Tables[1].Rows.Count > 0)
                    {
                        grdQuery.DataSource = ds1.Tables[1];
                        grdQuery.DataBind();
                        TRQUERY.Visible = true;
                    }
                    else
                    {
                        grdQuery.DataSource = null;
                        grdQuery.DataBind();
                        TRQUERY.Visible = false;
                    }

                    //Query Comments
                    if (ds1.Tables[2].Rows.Count > 0)
                    {
                        grdQryComment.DataSource = ds1.Tables[2];
                        grdQryComment.DataBind();
                        TRQUERY_COMMENT.Visible = true;
                    }
                    else
                    {
                        grdQryComment.DataSource = null;
                        grdQryComment.DataBind();
                        TRQUERY_COMMENT.Visible = false;
                    }

                    //Field Comment
                    if (ds1.Tables[3].Rows.Count > 0)
                    {
                        grdFieldComment.DataSource = ds1.Tables[3];
                        grdFieldComment.DataBind();
                        TRFIELDCOMMENT.Visible = true;
                    }
                    else
                    {
                        grdFieldComment.DataSource = null;
                        grdFieldComment.DataBind();
                        TRFIELDCOMMENT.Visible = false;
                    }

                    //Page Comment
                    if (ds1.Tables[4].Rows.Count > 0)
                    {
                        grdModuleComment.DataSource = ds1.Tables[4];
                        grdModuleComment.DataBind();
                        TRPAGECOMMENT.Visible = true;
                    }
                    else
                    {
                        grdModuleComment.DataSource = null;
                        grdModuleComment.DataBind();
                        TRPAGECOMMENT.Visible = false;
                    }

                    //Supporting Documents
                    if (ds1.Tables[5].Rows.Count > 0)
                    {
                        grdDocs.DataSource = ds1.Tables[5];
                        grdDocs.DataBind();
                        TRDOCS.Visible = true;
                    }
                    else
                    {
                        grdDocs.DataSource = null;
                        grdDocs.DataBind();
                        TRDOCS.Visible = false;
                    }

                    //Event Logs
                    if (ds1.Tables[6].Rows.Count > 0)
                    {
                        grdEventLogs.DataSource = ds1.Tables[6];
                        grdEventLogs.DataBind();
                        TREvent_Logs.Visible = true;
                    }
                    else
                    {
                        grdEventLogs.DataSource = null;
                        grdEventLogs.DataBind();
                        TREvent_Logs.Visible = false;
                    }
                }
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
                    DataSet ds = new DataSet();
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    hdnid.Value = ID;
                    string btnEdit = ((Label)e.Row.FindControl("TXT_FIELD")).Text;

                    GridView grd_Data1 = e.Row.FindControl("grd_Data1") as GridView;

                    DataSet ds1 = dal_SAE.SAE_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE_CHILD",
                    MODULEID: dr["MODULEID"].ToString(),
                    FIELDID: ID,
                    SAEID: dr["SAEID"].ToString(),
                    RECID: dr["RECID"].ToString(),
                    DefaultData: btnEdit,
                    SUBJID: dr["SUBJID_DATA"].ToString()
                    );

                    if (ds1.Tables.Count > 0)
                    {
                        grd_Data1.DataSource = ds1.Tables[0];
                        grd_Data1.DataBind();
                    }
                    else
                    {
                        grd_Data1.DataSource = null;
                        grd_Data1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Data1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataSet ds = new DataSet();
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    hdnid.Value = ID;
                    string VAL_Child = dr["VAL_Child"].ToString();
                    string btnEdit = ((Label)e.Row.FindControl("TXT_FIELD1")).Text;

                    if (VAL_Child != null && VAL_Child != "")
                    {
                        GridView grd_Data2 = e.Row.FindControl("grd_Data2") as GridView;

                        DataSet ds1 = dal_SAE.SAE_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: dr["MODULEID"].ToString(),
                        FIELDID: ID,
                        SAEID: dr["SAEID"].ToString(),
                        RECID: dr["RECID"].ToString(),
                        DefaultData: btnEdit,
                        SUBJID: dr["SUBJID_DATA"].ToString()
                        );

                        if (ds1.Tables.Count > 0)
                        {
                            grd_Data2.DataSource = ds1.Tables[0];
                            grd_Data2.DataBind();
                        }
                        else
                        {
                            grd_Data2.DataSource = null;
                            grd_Data2.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Data2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataSet ds = new DataSet();
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    hdnid.Value = ID;
                    string VAL_Child = dr["VAL_Child"].ToString();
                    string btnEdit = ((Label)e.Row.FindControl("TXT_FIELD2")).Text;

                    if (VAL_Child != null && VAL_Child != "")
                    {
                        GridView grd_Data3 = e.Row.FindControl("grd_Data3") as GridView;

                        DataSet ds1 = dal_SAE.SAE_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE_CHILD",
                        MODULEID: dr["MODULEID"].ToString(),
                        FIELDID: ID,
                        SAEID: dr["SAEID"].ToString(),
                        RECID: dr["RECID"].ToString(),
                        DefaultData: btnEdit,
                        SUBJID: dr["SUBJID_DATA"].ToString()
                        );

                        if (ds1.Tables.Count > 0)
                        {
                            grd_Data3.DataSource = ds1.Tables[0];
                            grd_Data3.DataBind();
                        }
                        else
                        {
                            grd_Data3.DataSource = null;
                            grd_Data3.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Data3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataSet ds = new DataSet();
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    hdnid.Value = ID;
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    Label btnEdit = (Label)e.Row.FindControl("TXT_FIELD3");

                    if (CONTROLTYPE != "HEADER")
                    {
                        ds = dal_SAE.SAE_PRINT_REPORT_SP(ACTION: "GET_ANNOTATED_MODULE_DATA",
                            SAEID: dr["SAEID"].ToString(),
                            RECID: dr["RECID"].ToString(),
                            STATUS: dr["STATUS"].ToString(),
                            TABLENAME: dr["TABLENAME"].ToString() + "_LOGS",
                            VARIABLENAME: dr["VARIABLENAME"].ToString()
                            );

                        btnEdit.Visible = true;

                        btnEdit.Text = ds.Tables[0].Rows[0][VARIABLENAME].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}