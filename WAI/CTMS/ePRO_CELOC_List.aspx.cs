using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Data.SqlClient;

namespace CTMS
{
    public partial class ePRO_CELOC_List : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["PREV_URL"] = Request.RawUrl.ToString();

                    GET_SITE();
                    GET_SUBJECT();
                    GET_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBJECT()
        {
            try
            {
                DataSet ds = dal.ePRO_WAD_SP(ACTION: "GET_DATA_SUBJECTS_DOSED", SITEID: ddlSite.SelectedValue, USERID: Session["User_ID"].ToString());
                ddlSubject.DataSource = ds;
                ddlSubject.DataValueField = "SUBJID";
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, new ListItem("--Select--", "0"));

                if (Session["Review_SUBJID"] != null)
                {
                    ddlSubject.SelectedValue = Session["Review_SUBJID"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DATA()
        {
            try
            {
                if (ddlSubject.SelectedIndex != 0)
                {
                    DataSet dsData = dal.ePRO_WAD_SP(ACTION: "GET_SOLICITED_LOC_SUMMARY", SUBJID: ddlSubject.SelectedValue, USERID: Session["User_ID"].ToString());

                    if (dsData.Tables.Count > 0)
                    {
                        gridData.DataSource = dsData;
                        gridData.DataBind();
                    }
                    else
                    {
                        gridData.DataSource = null;
                        gridData.DataBind();
                    }
                }
                else
                {
                    hdnLASTDOSE.Value = "";

                    gridData.DataSource = null;
                    gridData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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

        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GetSiteID(Action: "INVID", PROJECTID: Session["PROJECTID"].ToString(), User_ID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                        ddlSite.Items.Insert(0, new ListItem("--Select--", "-1"));
                    }
                    else
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                    }

                    if (Session["Review_SITEID"] != null)
                    {
                        ddlSite.SelectedValue = Session["Review_SITEID"].ToString();
                    }
                }
                else
                {
                    ddlSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBJECT();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        string[] gridDataCOLS;
        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    string Headers = null;
                    foreach (TableCell RowCell in e.Row.Cells)
                    {
                        if (Headers == null)
                        {
                            Headers = RowCell.Text;
                        }
                        else
                        {
                            Headers = Headers + "ª" + RowCell.Text;
                        }
                    }

                    gridDataCOLS = Headers.Split('ª');
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Attributes.Add("style", "color: black;text-align: left;");
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    if (Session["UserType"].ToString() == "Investigator" || Session["UserType"].ToString() == "Investigator Team Member" || Session["UserGroup_ID"].ToString() == "Administrator")
                    {
                        string VISITID = "15";

                        string MODULEID = "29";

                        string PAGENAME = Request.RawUrl.ToString().Replace("/", "");

                        string MODULENAME = "SOLICITED LOCAL ADVERSE EVENT";

                        for (int i = 1; i < e.Row.Cells.Count; i++)
                        {
                            string CLICKDATE = e.Row.Cells[i].Text;
                            string TERM = drv["Signs/Symptoms"].ToString().Trim();

                            string DATE = gridDataCOLS[i].ToString().Remove(gridDataCOLS[i].ToString().IndexOf(')')).Substring(gridDataCOLS[i].ToString().IndexOf('(') + 1);

                            DataSet ds = dal.ePRO_WAD_SP(ACTION: "CHECK_PUSH_OR_NOT", SUBJID: ddlSubject.SelectedValue, LASTDOSE: hdnLASTDOSE.Value, IC1: DATE, TERM: TERM);

                            if (ds.Tables[0].Rows.Count < 1 && ds.Tables[1].Rows.Count != 0)
                            {
                                if (e.Row.Cells[i].Text != "No")
                                {
                                    e.Row.CssClass = e.Row.CssClass + " fontBlue";

                                    hdnLASTDOSE.Value = ds.Tables[1].Rows[0]["LASTDOSE"].ToString();

                                    string URL = "ePRO_PUSH_To_eCRF.aspx?INVID=" + ddlSite.SelectedValue + "&SUBJID=" + ddlSubject.SelectedValue + "&VISITID=" + VISITID + "&MODULEID=" + MODULEID + "&VISITCOUNT=1&Indication=19" + "&LASTDOSE=" + hdnLASTDOSE.Value + "&MODULENAME=" + MODULENAME + "&TERM=" + TERM + "&PAGENAME=" + PAGENAME + "&DATE=" + DATE;

                                    e.Row.Cells[i].Attributes.Add("onclick", "ViewEventDetails(this,'" + URL + "');");
                                }
                                else
                                {
                                    e.Row.Cells[i].Attributes.Add("style", "color: black;text-align: center;");
                                }
                            }
                            else
                            {
                                e.Row.Cells[i].Attributes.Add("style", "color: black;text-align: center;");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string GET_AutoNum(string VARIABLENAME, string SUBJID, string TABLENAME, string MODULEID, string VISITNUM)
        {
            string res = "";
            try
            {
                DataSet ds = dal.GetSet_DM_ProjectData(
                Action: "GET_AutoNum",
                SUBJID: SUBJID,
                TABLENAME: TABLENAME,
                MODULEID: MODULEID,
                VARIABLENAME: VARIABLENAME,
                VISITNUM: VISITNUM
                );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    res = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return res;
        }

        protected void grdSyncData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Review")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;

                    string LAST_DOSE = hdnLASTDOSE.Value;

                    string TABLENAME = (row.FindControl("EPRO_TABLENAME") as Label).Text;
                    string TERM = (row.FindControl("CELOCTERM") as Label).Text;

                    Session["Review_SITEID"] = ddlSite.SelectedValue;
                    Session["Review_SUBJID"] = ddlSubject.SelectedValue;

                    string VISITID = "12";

                    string MODULEID = "29";

                    Response.Redirect("ePro_Review_Data.aspx?TABLENAME=" + TABLENAME + "&LAST_DOSE=" + LAST_DOSE + "&SUBJID=" + ddlSubject.SelectedValue + "&TERM=" + TERM + "&PAGENAME=ePRO_CELOC_List" + "&INVID=" + ddlSite.SelectedValue + "&VISITID=" + VISITID + "&MODULEID=" + MODULEID + "&VISITCOUNT=1");
                }
                else if (e.CommandName == "Push")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;

                    string LAST_DOSE = hdnLASTDOSE.Value;

                    string TABLENAME = (row.FindControl("TABLENAME") as Label).Text;
                    string MODULENAME = (row.FindControl("MODULENAME") as Label).Text;

                    string TERM = "", DIA = "", ONGO = "", STDAT = "", ENDAT = "", SEV = "", TRT = "";

                    TERM = (row.FindControl("CELOCTERM") as Label).Text;
                    STDAT = (row.FindControl("CELOCSTDAT") as Label).Text;
                    ONGO = (row.FindControl("CELOCONGO") as Label).Text;
                    ENDAT = (row.FindControl("CELOCENDAT") as Label).Text;
                    DIA = (row.FindControl("CELOCDIA") as Label).Text;
                    SEV = (row.FindControl("CELOCSEV") as Label).Text;
                    TRT = (row.FindControl("CELOCTRT") as Label).Text;

                    string VISITID = "";
                    if (hdnLASTDOSE.Value == "Day 0")
                    {
                        VISITID = "4";
                    }
                    else
                    {
                        VISITID = "7";
                    }

                    string MODULEID = "59";

                    //Response.Redirect("ePRO_PUSH_To_eCRF.aspx?INVID=" + ddlSite.SelectedValue + "&VISITID=" + VISITID + "&MODULEID=" + MODULEID + "&VISITCOUNT=-1" + "&Indication=19" + "&LASTDOSE=" + LAST_DOSE + "&");

                    DataSet ds;
                    string RECID = "";
                    string PVID = Session["PROJECTID"].ToString() + "-" + ddlSite.SelectedValue + "-" + ddlSubject.SelectedValue + "-" + VISITID + "-" + MODULEID + "-1";

                    string INSERTQUERY = "";

                    DataSet dsMOD = dal.DM_ADD_UPDATE(ACTION: "GET_MODULENAME_BYID", ID: MODULEID);

                    ds = dal.GetSet_DM_ProjectData(
                     Action: "MAX_REC_ID_ALL",
                     PVID: PVID,
                     MODULENAME: MODULENAME,
                     TABLENAME: TABLENAME,
                     SUBJID: ddlSubject.SelectedValue,
                     PROJECTID: Session["PROJECTID"].ToString()
                     );

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            RECID = (Convert.ToInt32(ds.Tables[0].Rows[0]["RECID"]) + 1).ToString();
                        }
                        else
                        {
                            RECID = "0";
                        }
                    }
                    else
                    {
                        RECID = "0";
                    }

                    INSERTQUERY = "INSERT INTO [" + TABLENAME + "] ([PVID], [RECID], [SUBJID_DATA], [VISITNUM], [VISITCOUNT], [ENTEREDBY], [ENTEREDDAT], [IsComplete], CELOCTERM, CELOCDIA, CELOCSEV, CELOCSTDAT, CELOCENDAT, CELOCTRT, CELOCSPID, CELOCYN, CELOCONGO) VALUES ('" + PVID + "', '" + RECID + "', '" + ddlSubject.SelectedValue + "', '" + VISITID + "', '1', '" + Session["USER_ID"].ToString() + "', GETDATE(), 0, '" + TERM + "', '" + DIA + "', '" + SEV + "', '" + STDAT + "', '" + ENDAT + "','" + TRT + "', 'L-" + GET_AutoNum("CELOCSPID", ddlSubject.SelectedValue, TABLENAME, MODULEID, VISITID) + "', 'Yes', '" + ONGO + "')";

                    dal.GetSet_DM_ProjectData(
                             Action: "INSERT_MODULE_DATA",
                             TABLENAME: TABLENAME,
                             PVID: PVID,
                             RECID: RECID,
                             VISITNUM: VISITID,
                             SUBJID: ddlSubject.SelectedValue,
                             INSERTQUERY: INSERTQUERY
                             );

                    dal.DM_PV_SP(
                    PVID: PVID,
                    INVID: ddlSite.SelectedValue,
                    SUBJID: ddlSubject.SelectedValue,
                    PAGENUM: MODULEID,
                    VISITNUM: VISITID,
                    PAGESTATUS: "1",
                    ENTEREDBY: Session["USER_ID"].ToString(),
                    VISITCOUNT: "1"
                    );

                    dal.ePRO_WAD_SP(
                    ACTION: "INSERT_PUSH_RECORD",
                    SUBJID: ddlSubject.SelectedValue,
                    LASTDOSE: hdnLASTDOSE.Value,
                    USERID: Session["USER_ID"].ToString(),
                    LANG: TERM
                    );

                    GET_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSyncData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string PUSHCOUNT = dr["PUSHCOUNT"].ToString();
                    string REVIEWED = dr["REVIEWED"].ToString();

                    //LinkButton lbtnPush = (LinkButton)e.Row.FindControl("lbtnPush");
                    LinkButton lbtnReview = (LinkButton)e.Row.FindControl("lbtnReview");

                    if (ViewState["PushAllow"].ToString() == "Yes")
                    {
                        if (PUSHCOUNT == "0")
                        {
                            lbtnReview.Visible = true;
                        }
                        else
                        {
                            lbtnReview.Visible = false;
                        }
                    }
                    else
                    {
                        lbtnReview.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;

                string LAST_DOSE = hdnLASTDOSE.Value;

                string VISITID = "12";

                string MODULEID = "29";

                string AETERM = (row.FindControl("lblAETERM") as Label).Text;
                string TERM = (row.FindControl("lbtnSignsSymptoms") as LinkButton).Text;

                string PAGENAME = Request.RawUrl.ToString().Replace("/", "");

                string MODULENAME = "SOLICITED LOCAL ADVERSE EVENT";
                Response.Redirect("ePRO_PUSH_To_eCRF.aspx?INVID=" + ddlSite.SelectedValue + "&SUBJID=" + ddlSubject.SelectedValue + "&VISITID=" + VISITID + "&MODULEID=" + MODULEID + "&VISITCOUNT=1&Indication=19" + "&LASTDOSE=" + LAST_DOSE + "&MODULENAME=" + MODULENAME + "&TERM=" + AETERM + "&PAGENAME=" + PAGENAME);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal.ePRO_WAD_SP(ACTION: "GET_SOLICITED_LOC_SUMMARY", SUBJID: ddlSubject.SelectedValue, USERID: Session["User_ID"].ToString());

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToPDF();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportToPDF()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal.ePRO_WAD_SP(ACTION: "GET_SOLICITED_LOC_SUMMARY", SUBJID: ddlSubject.SelectedValue, USERID: Session["User_ID"].ToString());

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}