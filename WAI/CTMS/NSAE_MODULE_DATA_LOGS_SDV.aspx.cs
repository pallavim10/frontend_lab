using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using CTMS.CommonFunction;
using PPT;

namespace CTMS
{
    public partial class NSAE_MODULE_DATA_LOGS_SDV : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    hdnSITEID.Value = Request.QueryString["INVID"].ToString();
                    hdnSubjectID.Value = Request.QueryString["SUBJID"].ToString();
                    hdnSAEID.Value = Request.QueryString["SAEID"].ToString();
                    hdnSAE.Value = Request.QueryString["SAE"].ToString();
                    hdnSTATUS.Value = Request.QueryString["STATUS"].ToString();

                    DataSet ds3 = dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "GET_COUT_SUPPORTING_DOC", SAEID: Request.QueryString["SAEID"].ToString());
                    if (ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                    {
                        lblCount.Text = ds3.Tables[0].Rows[0]["COUNT"].ToString();
                        lblCount.Visible = true;
                    }
                    else
                    {
                        lblCount.Text = "";
                        lblCount.Visible = false;
                    }

                    OpenCRF();
                    GetSign_info();

                    DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_SAE_LIST_DATA",
                        SAEID: Request.QueryString["SAEID"].ToString()
                        );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DivSAEDetails.Visible = true;
                        lblAESPID.Text = ds.Tables[0].Rows[0]["DM_SPID"].ToString();
                        lblAETERM.Text = ds.Tables[0].Rows[0]["DM_TERM"].ToString();
                        lblSAEID.Text = hdnSAEID.Value;
                        lblSAE.Text = hdnSAE.Value;
                        lblStatus.Text = hdnSTATUS.Value;

                        string Reason = ds.Tables[0].Rows[0]["REASON"].ToString();

                        if (Reason != null && Reason != "")
                        {
                            btnDelayedReason.Visible = true;
                            hdn_Delayed_Reason.Value = ds.Tables[0].Rows[0]["REASON"].ToString();
                            hdn_Delayed_ReasonBy.Value = ds.Tables[0].Rows[0]["REASONBYNAME"].ToString();
                            hdn_Delayed_DTServer.Value = ds.Tables[0].Rows[0]["REASON_CAL_DAT"].ToString();
                            hdn_Delayed_DTUser.Value = ds.Tables[0].Rows[0]["REASON_CAL_TZDAT"].ToString();
                        }
                        else
                        {
                            btnDelayedReason.Visible = false;
                        }
                    }
                    else
                    {
                        DivSAEDetails.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GetSign_info()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SIGNOFF_SP(
                    ACTION: "GET_INV_SIGNOFF_DETAILS",
                    SAEID: Request.QueryString["SAEID"].ToString()
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gridsigninfo.DataSource = ds.Tables[0];
                    gridsigninfo.DataBind();
                }
                else
                {
                    gridsigninfo.DataSource = null;
                    gridsigninfo.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw ex;
            }
        }

        private void OpenCRF()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_OPEN_CRF_SP(
                    ACTION: "GET_OPEN_CRF",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    STATUS: Request.QueryString["STATUS"].ToString()
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grd_OpenCRF.DataSource = ds.Tables[0];
                    Grd_OpenCRF.DataBind();
                }
                else
                {
                    Grd_OpenCRF.DataSource = null;
                    Grd_OpenCRF.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void Grd_OpenCRF_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                TextBox txtPAGESTATUS = e.Row.FindControl("txtPAGESTATUS") as TextBox;

                ImageButton lnkVISITPAGENO = e.Row.FindControl("lnkPAGENUM") as ImageButton;

                if (txtPAGESTATUS.Text == "1")
                {
                    if (dr["HasMissing"].ToString() == "True")
                    {
                        lnkVISITPAGENO.ImageUrl = "img/REDFILE.png";
                        lnkVISITPAGENO.ToolTip = "Missing Fields";
                    }
                    else if (dr["IsComplete"].ToString() == "0")
                    {
                        lnkVISITPAGENO.ImageUrl = "img/orange file.png";
                        lnkVISITPAGENO.ToolTip = "Incomplete";
                    }
                    else
                    {
                        lnkVISITPAGENO.ImageUrl = "img/green file.png";
                        lnkVISITPAGENO.ToolTip = "Complete";
                    }
                }
                else
                {
                    lnkVISITPAGENO.ToolTip = "Not Entered";
                }

                if (dr["QueryCount"].ToString() == "0")
                {
                    LinkButton lnkQuery = (LinkButton)e.Row.FindControl("lnkQuery");
                    lnkQuery.Visible = false;
                }

                LinkButton lbtnPageComment = (LinkButton)e.Row.FindControl("lbtnPageComment");
                Label lblPageComment = (Label)e.Row.FindControl("lblPageComment");
                if (dr["MODULE_COMMENT"].ToString() == "0")
                {
                    lbtnPageComment.Visible = false;
                }
                else
                {
                    lblPageComment.Text = dr["MODULE_COMMENT"].ToString();
                    lbtnPageComment.Visible = true;
                }

                //LinkButton lbtnInternalComment = (LinkButton)e.Row.FindControl("lbtnInternalComment");
                //Label lblInternalComment_Count = (Label)e.Row.FindControl("lblInternalComment_Count");
                //if (dr["INTERNAL_COMMENT"].ToString() == "0")
                //{
                //    lbtnInternalComment.Visible = false;
                //}
                //else
                //{
                //    lblInternalComment_Count.Text = dr["INTERNAL_COMMENT"].ToString();
                //    lbtnInternalComment.Visible = true;
                //}

                if (dr["SDVSTATUS"].ToString() == "0" && txtPAGESTATUS.Text == "1")
                {
                    e.Row.Cells[8].CssClass = "brd-1px-maroonimp";
                }

            }
        }

        protected void Grd_OpenCRF_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "GOTOPAGE")
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                    string SAEID = hdnSAEID.Value;
                    string SAE = hdnSAE.Value;
                    string STATUS = Request.QueryString["STATUS"].ToString();
                    string INVID = Request.QueryString["INVID"].ToString();
                    string SUBJID = Request.QueryString["SUBJID"].ToString();
                    string MODULEID = (gvr.FindControl("MODULEID") as Label).Text;

                    if ((gvr.FindControl("MULTIPLEYN") as Label).Text == "True")
                    {
                        Response.Redirect("NSAE_MULTIPLE_DATA_ENTRY_SDV.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&MODULEID=" + MODULEID, false);
                    }
                    else
                    {
                        Response.Redirect("NSAE_DATA_ENTRY_SDV.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&MODULEID=" + MODULEID, false);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnBacktoHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NSAE_LOG_EVENTS_SDV.aspx", false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gridsigninfo_PreRender(object sender, EventArgs e)
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

        protected void lbtnSupportingDocs_Click(object sender, EventArgs e)
        {
            SELECT_SUPPORT_DOC();
            ModalPopupExtender4.Show();
        }
        private void SELECT_SUPPORT_DOC()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "SELECT_SAE_SUPPORT_DOCS", SAEID: hdnSAEID.Value);
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdSupport_Doc.DataSource = ds;
                    grdSupport_Doc.DataBind();
                }
                else
                {
                    grdSupport_Doc.DataSource = null;
                    grdSupport_Doc.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSupport_Doc_PreRender(object sender, EventArgs e)
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

        protected void grdSupport_Doc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            if (e.CommandName == "DownloadSupportDoc")
            {
                IMPORT_SAE_SUPPORT_DOC(ID);
                Response.Write("<script> alert('Supporting Document Downloaded successfully ')</script>");
                ModalPopupExtender4.Show();
            }
        }

        private void IMPORT_SAE_SUPPORT_DOC(string ID)
        {
            try
            {
                string FileName, ContentType;
                byte[] fileData;
                DataSet ds = dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "IMPORT_SAE_SUPPORT_DOC",
                    ID: ID
                    );
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FileName = ds.Tables[0].Rows[0]["FILENAME"].ToString();
                        ContentType = ds.Tables[0].Rows[0]["CONTENTTYPE"].ToString();
                        fileData = (byte[])ds.Tables[0].Rows[0]["DATA"];
                        Response.Clear();
                        Response.ContentType = ContentType;
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                        Response.BinaryWrite(fileData);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSuportDocCancel_Click(object sender, EventArgs e)
        {
            ModalPopupExtender4.Hide();
        }
    }
}