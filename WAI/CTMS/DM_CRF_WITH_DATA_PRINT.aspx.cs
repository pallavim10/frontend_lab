using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;
using System.Web.UI.HtmlControls;
using IronPdf;

namespace CTMS
{
    public partial class DM_CRF_WITH_DATA_PRINT : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["PROJECTID"] != null)
                    {
                        Session["PROJECTID"] = Request.QueryString["PROJECTID"].ToString();
                    }

                    if (Request.QueryString["PROJECTIDTEXT"] != null)
                    {
                        Session["PROJECTIDTEXT"] = Request.QueryString["PROJECTIDTEXT"].ToString();
                    }

                    if (Request.QueryString["User_Name"] != null)
                    {
                        Session["User_Name"] = Request.QueryString["User_Name"].ToString();
                    }

                    hdnINVID.Value = Request.QueryString["INVID"].ToString();
                    hdnSUBJID.Value = Request.QueryString["SUBJID"].ToString();
                    hdnVISIT.Value = Request.QueryString["VISITNUM"].ToString();
                    hdnVISITNAME.Value = Request.QueryString["VISITNAME"].ToString();

                    GET_ANOTATED_VISITS_PV();
                }
            }
            catch (Exception ex)
            {

            }
        }

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
                            LeftText = Session["PROJECTIDTEXT"].ToString() + "_" + hdnINVID.Value + "_" + hdnSUBJID.Value + "_" + hdnVISITNAME.Value,
                            RightText = "eCRF",
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
                    };

                    AspxToPdf.RenderThisPageAsPdf(
                        IronPdf.AspxToPdf.FileBehavior.Attachment,
                        "" + Session["PROJECTIDTEXT"].ToString() + "_" + hdnINVID.Value + "-" + hdnSUBJID.Value + "_" + hdnVISITNAME.Value + "_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf",
                        AspxToPdfOptions
                   );
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GET_ANOTATED_VISITS_PV()
        {
            try
            {
                DataSet ds = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_ANOTATED_VISITS_PV",
                SUBJID: hdnSUBJID.Value,
                VISITNUM: hdnVISIT.Value
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
                //lblErrorMsg.Text = ex.Message.ToString();
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

                    GridView grd_Data = (GridView)e.Item.FindControl("grd_Data");

                    ds = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE",
                    MODULEID: row["MODULEID"].ToString(),
                    VISITNUM: row["VISITNUM"].ToString(),
                    PVID: row["PVID"].ToString(),
                    RECID: row["RECID"].ToString(),
                    SUBJID: row["SUBJID"].ToString()
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

                    DataSet ds1 = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_CONT_DOC_QUR_AUDIT_SIGNOFF",
                    PVID: row["PVID"].ToString(),
                    RECID: row["RECID"].ToString()
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

                    //Event Logs
                    if (ds1.Tables[4].Rows.Count > 0)
                    {
                        grdEventLogs.DataSource = ds1.Tables[4];
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

                    DataSet ds1 = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE_CHILD",
                    VISITNUM: dr["VISITNUM"].ToString(),
                    MODULEID: dr["MODULEID"].ToString(),
                    FIELDID: ID,
                    PVID: dr["PVID"].ToString(),
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
                //
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

                        DataSet ds1 = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE_CHILD",
                        VISITNUM: dr["VISITNUM"].ToString(),
                        MODULEID: dr["MODULEID"].ToString(),
                        FIELDID: ID,
                        PVID: dr["PVID"].ToString(),
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
                //
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

                        DataSet ds1 = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE_CHILD",
                        VISITNUM: dr["VISITNUM"].ToString(),
                        MODULEID: dr["MODULEID"].ToString(),
                        FIELDID: ID,
                        PVID: dr["PVID"].ToString(),
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
                //
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
                        ds = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_ANNOTATED_MODULE_DATA",
                            PVID: dr["PVID"].ToString(),
                            RECID: dr["RECID"].ToString(),
                            TABLENAME: dr["TABLENAME"].ToString(),
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