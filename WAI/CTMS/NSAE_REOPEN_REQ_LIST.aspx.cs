using CTMS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class NSAE_REOPEN_REQ_LIST : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    DataSet ds = dal_SAE.SAE_CLOSE_SP(ACTION: "GET_CLOSE_SAE_DETAILS",
                        SAEID: Request.QueryString["SAEID"].ToString()
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblEventId.Text = ds.Tables[0].Rows[0]["DM_SPID"].ToString();
                        lblEventTerm.Text = ds.Tables[0].Rows[0]["DM_TERM"].ToString();
                        lblSAEID.Text = ds.Tables[0].Rows[0]["SAEID"].ToString();
                        lblSAE.Text = ds.Tables[0].Rows[0]["SAE"].ToString();
                        lblStatus.Text = ds.Tables[0].Rows[0]["STATUS"].ToString();
                        lblINVID.Text = ds.Tables[0].Rows[0]["INVID"].ToString();
                        lblSUBJID.Text = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                        lblClosedBy.Text = ds.Tables[0].Rows[0]["CLOSEDBYNAME"].ToString();
                        lblClosedDTUTC.Text = ds.Tables[0].Rows[0]["CLOSED_CAL_DAT"].ToString();
                        lblClosedDTServer.Text = ds.Tables[0].Rows[0]["CLOSED_CAL_TZDAT"].ToString();
                        lblClosedReason.Text = ds.Tables[0].Rows[0]["CLOSEDREASON"].ToString();
                    }

                    GET_REQUEST_LOGS();
                    GET_MODULE();

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
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_REQUEST_LOGS()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_CLOSE_SP(ACTION: "GET_REQUEST_LOGS",
                    SAEID: lblSAEID.Text,
                    STATUS: lblStatus.Text
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gridActionLogs.DataSource = ds;
                    gridActionLogs.DataBind();
                }
                else
                {
                    gridActionLogs.DataSource = null;
                    gridActionLogs.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
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

        protected void GET_MODULE()
        {
            try
            {
                DataSet ds = new DataSet();

                if (Convert.ToString(Session["UserType"]).Contains("Site"))
                {
                    ds = dal_SAE.SAE_MODULE_SP(ACTION: "GET_SAE_MODULE");
                }
                else
                {
                    ds = dal_SAE.SAE_MODULE_SP(ACTION: "GET_SAE_MODULE_MM");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeatData.DataSource = ds;
                    repeatData.DataBind();
                }
                else
                {
                    repeatData.DataSource = null;
                    repeatData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    Control anchor = e.Item.FindControl("anchor") as Control;

                    GridView grd_Records = (GridView)e.Item.FindControl("grd_Records");

                    DataSet ds = dal_SAE.SAE_CLOSE_SP(
                            ACTION: "GET_CLOSED_SAE_RECORD",
                            SAEID: lblSAEID.Text,
                            TABLENAME: row["TABLENAME"].ToString(),
                            MODULEID: row["MODULEID"].ToString()
                            );

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            if (dc.DataType.Name == "String")
                            {
                                ds.Tables[0].Rows[i][dc.ColumnName] = REMOVEHTML(ds.Tables[0].Rows[i][dc.ColumnName].ToString());
                            }
                        }
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_Records.DataSource = ds;
                        grd_Records.DataBind();

                        anchor.Visible = true;
                    }
                    else
                    {
                        grd_Records.DataSource = null;
                        grd_Records.DataBind();

                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected string REMOVEHTML(string str)
        {
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");
            str = str.Replace("&nbsp;", "");

            return str;
        }

        protected void grd_Records_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lnkQUERYSTATUS = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                HtmlControl ADICON = (HtmlControl)e.Row.FindControl("ADICON");

                string QryCount = dr["QryCount"].ToString();

                if (QryCount == "0")
                {
                    lnkQUERYSTATUS.Attributes.Add("class", "disp-none");
                }
                LinkButton lnkQueryAns = (LinkButton)e.Row.FindControl("lnkQUERYANS");
                if (dr["QryAnsCount"].ToString() != "0")
                {
                    lnkQueryAns.Visible = true;
                }
                else
                {
                    lnkQueryAns.Visible = false;
                }
                LinkButton lnkQueryClose = (LinkButton)e.Row.FindControl("lnkQUERYCLOSE");
                if (dr["QryClosedCount"].ToString() != "0")
                {
                    lnkQueryClose.Visible = true;
                }
                else
                {
                    lnkQueryClose.Visible = false;
                }

                if (dr["AUDITSTATUS"].ToString() == "")
                {
                    AD.Attributes.Add("class", "disp-none");
                }
                else if (dr["AUDITSTATUS"].ToString() != "Initial Entry")
                {
                    AD.Attributes.Add("class", "");
                    ADICON.Attributes.Add("style", "color: red;font-size: 17px;");
                }
                else
                {
                    AD.Attributes.Add("class", "");
                    ADICON.Attributes.Add("style", "color: green;font-size: 17px;");
                }

                string DELETE = dr["DELETE"].ToString();

                if (DELETE == "True")
                {
                    e.Row.Attributes.Add("class", "brd-1px-maroonimp");
                }

                GridView grd_Records = (GridView)sender;

                grd_Records.HeaderRow.Cells[7].Visible = false;
                grd_Records.HeaderRow.Cells[8].Visible = false;
                grd_Records.HeaderRow.Cells[9].Visible = false;
                grd_Records.HeaderRow.Cells[10].Visible = false;
                grd_Records.HeaderRow.Cells[11].Visible = false;
                grd_Records.HeaderRow.Cells[12].Visible = false;
                grd_Records.HeaderRow.Cells[13].Visible = false;
                grd_Records.HeaderRow.Cells[14].Visible = false;
                grd_Records.HeaderRow.Cells[15].Visible = false;
                grd_Records.HeaderRow.Cells[16].Visible = false;
                grd_Records.HeaderRow.Cells[17].Visible = false;
                grd_Records.HeaderRow.Cells[18].Visible = false;
                grd_Records.HeaderRow.Cells[19].Visible = false;
                grd_Records.HeaderRow.Cells[20].Visible = false;
                grd_Records.HeaderRow.Cells[21].Visible = false;
                grd_Records.HeaderRow.Cells[22].Visible = false;
                grd_Records.HeaderRow.Cells[23].Visible = false;
                grd_Records.HeaderRow.Cells[24].Visible = false;
                grd_Records.HeaderRow.Cells[25].Visible = false;
                grd_Records.HeaderRow.Cells[26].Visible = false;
                grd_Records.HeaderRow.Cells[27].Visible = false;
                grd_Records.HeaderRow.Cells[28].Visible = false;
                grd_Records.HeaderRow.Cells[29].Visible = false;

                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;
                e.Row.Cells[21].Visible = false;
                e.Row.Cells[22].Visible = false;
                e.Row.Cells[23].Visible = false;
                e.Row.Cells[24].Visible = false;
                e.Row.Cells[25].Visible = false;
                e.Row.Cells[26].Visible = false;
                e.Row.Cells[27].Visible = false;
                e.Row.Cells[28].Visible = false;
                e.Row.Cells[29].Visible = false;
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
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

        protected void btnApproveReq_Click(object sender, EventArgs e)
        {
            try
            {
                dal_SAE.SAE_CLOSE_SP(
                    ACTION: "UPDATE_REOPEN_REQ_APPR_ACTION",
                    SAEID: lblSAEID.Text,
                    REASON: txtReason.Text
                    );

                string MESSAGE = "Re-Open request has been approved Successfully. ";

                SendEmail(Request.QueryString["SAEID"].ToString(), "Approved", "Re-Open Request (Approve/Disapprove)");

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NSAE_REOPEN_REQ.aspx';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnDisApproveReq_Click(object sender, EventArgs e)
        {
            try
            {
                dal_SAE.SAE_CLOSE_SP(
                    ACTION: "UPDATE_REOPEN_REQ_DISSAPPR_ACTION",
                    SAEID: lblSAEID.Text,
                    REASON: txtReason.Text
                    );

                string MESSAGE = "Re-Open request has been dissapproved. ";

                SendEmail(Request.QueryString["SAEID"].ToString(), "Disapproved", "Re-Open Request (Approve/Disapprove)");

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NSAE_REOPEN_REQ.aspx';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
        }

        protected void SendEmail(string SAEID, string DECISION, string ACTIONS)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_GET_EMAIL_DATA_SP(
                        INVID: lblINVID.Text,
                        SAEID: SAEID,
                        ACTIONS: ACTIONS
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
                            EmailSubject = EmailSubject.Replace("[SUBJID]", lblSUBJID.Text);
                        }

                        if (EmailSubject.Contains("[PROJECTID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                        }

                        if (EmailSubject.Contains("[SITEID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[SITEID]", lblINVID.Text);
                        }

                        if (EmailSubject.Contains("[SAEID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[SAEID]", SAEID);
                        }

                        if (EmailSubject.Contains("[STATUS]"))
                        {
                            EmailSubject = EmailSubject.Replace("[STATUS]", lblStatus.Text);
                        }

                        if (EmailSubject.Contains("[USER]"))
                        {
                            EmailSubject = EmailSubject.Replace("[USER]", Session["User_Name"].ToString());
                        }

                        if (EmailSubject.Contains("[DATETIME]"))
                        {
                            EmailSubject = EmailSubject.Replace("[DATETIME]", comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                        }

                        if (EmailSubject.Contains("[DECISION]"))
                        {
                            EmailSubject = EmailSubject.Replace("[DECISION]", DECISION);
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
                            EmailBody = EmailBody.Replace("[SUBJID]", lblSUBJID.Text);
                        }

                        if (EmailBody.Contains("[PROJECTID]"))
                        {
                            EmailBody = EmailBody.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                        }

                        if (EmailBody.Contains("[SITEID]"))
                        {
                            EmailBody = EmailBody.Replace("[SITEID]", lblINVID.Text);
                        }

                        if (EmailBody.Contains("[SAEID]"))
                        {
                            EmailBody = EmailBody.Replace("[SAEID]", SAEID);
                        }

                        if (EmailBody.Contains("[STATUS]"))
                        {
                            EmailBody = EmailBody.Replace("[STATUS]", lblStatus.Text);
                        }

                        if (EmailBody.Contains("[USER]"))
                        {
                            EmailBody = EmailBody.Replace("[USER]", Session["User_Name"].ToString());
                        }

                        if (EmailBody.Contains("[DATETIME]"))
                        {
                            EmailBody = EmailBody.Replace("[DATETIME]", comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                        }

                        if (EmailBody.Contains("[DECISION]"))
                        {
                            EmailBody = EmailBody.Replace("[DECISION]", DECISION);
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

        protected void lbtnSupportingDocs_Click(object sender, EventArgs e)
        {
            SELECT_SUPPORT_DOC();
            ModalPopupExtender4.Show();
        }

        private void SELECT_SUPPORT_DOC()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "SELECT_SAE_SUPPORT_DOCS", SAEID: lblSAEID.Text);
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

        protected void grdSupport_Doc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lblDownloadSupportDoc = (e.Row.FindControl("lblDownloadSupportDoc") as LinkButton);
                    HiddenField hdnDeleted = (e.Row.FindControl("hdnDeleted") as HiddenField);

                    if (hdnDeleted.Value == "True")
                    {
                        lblDownloadSupportDoc.Visible = false;
                    }
                    else
                    {
                        lblDownloadSupportDoc.Visible = true;
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