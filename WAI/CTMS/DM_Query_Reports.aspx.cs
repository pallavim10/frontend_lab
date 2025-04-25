using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_Query_Reports : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
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
                    FillINV();
                    FillVisit();
                    FillModule();
                    FillFielde();
                    FillQueryStatus();

                    if (Session["QUERY_TYPE"] != null)
                    {
                        if (drpQueryType.Items.Contains(new ListItem(Session["QUERY_TYPE"].ToString())))
                        {
                            drpQueryType.SelectedValue = Session["QUERY_TYPE"].ToString();
                        }
                    }

                    Session["QUERY_TYPE"] = drpQueryType.SelectedValue;

                    if (Session["BACKTOQUERY_REPORT"] != null)
                    {
                        btnSearch_Click(this, e);
                        Session.Remove("BACKTOQUERY_REPORT");
                    }

                    if (!string.IsNullOrEmpty(Request.QueryString["INVID"]))
                    {
                        btnSearch_Click(this, e);
                        btnclosed.Visible = true;
                    }
                    else
                    {
                        btnclosed.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            DAL dal = new DAL();

            DataSet ds = dal.GET_INVID_SP(
                     USERID: Session["User_ID"].ToString()
                     );

            drpSite.DataSource = ds.Tables[0];
            drpSite.DataValueField = "INVID";
            drpSite.DataBind();
            drpSite.Items.Insert(0, new ListItem("--All--", "0"));

            if (Session["QUERY_INVID"] != null)
            {
                if (drpSite.Items.Contains(new ListItem(Session["QUERY_INVID"].ToString())))
                {
                    drpSite.SelectedValue = Session["QUERY_INVID"].ToString();
                }
            }

            Session["QUERY_INVID"] = drpSite.SelectedValue;

            FillSubject();
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal_DM.DM_QUERY_REPORT_SP(ACTION: "GET_QUERY_SUBJECTS", INVID: drpSite.SelectedValue);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpPatient.DataSource = ds.Tables[0];
                    drpPatient.DataValueField = "SUBJID";
                    drpPatient.DataTextField = "SUBJID";
                    drpPatient.DataBind();
                    drpPatient.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    drpPatient.Items.Clear();
                }

                if (Session["QUERY_SUBJID"] != null)
                {
                    if (drpPatient.Items.FindByValue(Session["QUERY_SUBJID"].ToString()) != null)
                    {
                        drpPatient.SelectedValue = Session["QUERY_SUBJID"].ToString();
                    }
                }

                Session["QUERY_SUBJID"] = drpPatient.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillVisit()
        {
            try
            {
                DataSet ds = dal_DM.DM_QUERY_REPORT_SP(ACTION: "GET_QUERY_VISITS",
                    SUBJID: drpPatient.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpVisit.DataSource = ds.Tables[0];
                    drpVisit.DataValueField = "VISITNUM";
                    drpVisit.DataTextField = "VISIT";
                    drpVisit.DataBind();
                    drpVisit.Items.Insert(0, new ListItem("--Select Visit--", "0"));
                }
                else
                {
                    drpVisit.Items.Clear();
                }

                if (Session["QUERY_VISITID"] != null)
                {
                    if (drpVisit.Items.FindByValue(Session["QUERY_VISITID"].ToString()) != null)
                    {
                        drpVisit.SelectedValue = Session["QUERY_VISITID"].ToString();
                    }
                }

                Session["QUERY_VISITID"] = drpVisit.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillModule()
        {
            try
            {
                DataSet ds = dal_DM.DM_QUERY_REPORT_SP(ACTION: "GET_QUERY_MODULES",
                    SUBJID: drpPatient.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds.Tables[0];
                    drpModule.DataValueField = "MODULEID";
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("--Select Module--", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                }

                if (Session["QUERY_MODULEID"] != null)
                {
                    if (drpModule.Items.FindByValue(Session["QUERY_MODULEID"].ToString()) != null)
                    {
                        drpModule.SelectedValue = Session["QUERY_MODULEID"].ToString();
                    }
                }

                Session["QUERY_MODULEID"] = drpModule.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillFielde()
        {
            try
            {
                DataSet ds = dal_DM.DM_QUERY_REPORT_SP(ACTION: "GET_QUERY_FIELD",
                    SUBJID: drpPatient.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue,
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpField.DataSource = ds.Tables[0];
                    drpField.DataValueField = "FIELDID";
                    drpField.DataTextField = "FIELDNAME";
                    drpField.DataBind();
                    drpField.Items.Insert(0, new ListItem("--Select Field--", "0"));
                }
                else
                {
                    drpField.Items.Clear();
                }

                if (Session["QUERY_FIELDID"] != null)
                {
                    if (drpField.Items.FindByValue(Session["QUERY_FIELDID"].ToString()) != null)
                    {
                        drpField.SelectedValue = Session["QUERY_FIELDID"].ToString();
                    }
                }

                Session["QUERY_FIELDID"] = drpField.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillQueryStatus()
        {
            try
            {
                DataSet ds = dal_DM.DM_QUERY_REPORT_SP(ACTION: "GET_QUERY_STATUS");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpQueryStatus.DataSource = ds.Tables[0];
                    drpQueryStatus.DataValueField = "Status";
                    drpQueryStatus.DataTextField = "StatusName";
                    drpQueryStatus.DataBind();
                    drpQueryStatus.Items.Insert(0, new ListItem("--All--", "-1"));
                }
                else
                {
                    drpQueryStatus.Items.Clear();
                }

                if (Session["QUERY_STATUS"] != null)
                {
                    if (drpQueryStatus.Items.FindByValue(Session["QUERY_STATUS"].ToString()) != null)
                    {
                        drpQueryStatus.SelectedValue = Session["QUERY_STATUS"].ToString();
                    }
                }

                Session["QUERY_STATUS"] = drpQueryStatus.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GETDATA()
        {
            try
            {
                DataSet ds = dal_DM.DM_QUERY_REPORT_SP(
                ACTION: "GET_BLINDED_QUERY_REPORT",
                INVID: drpSite.SelectedValue,
                SUBJID: drpPatient.SelectedValue,
                VISITNUM: drpVisit.SelectedValue,
                MODULEID: drpModule.SelectedValue,
                FIELDID: drpField.SelectedValue,
                QUERY_STATUS: drpQueryStatus.SelectedValue,
                QUERY_TYPE: drpQueryType.SelectedValue
                );

                if (ds.Tables[0] != null)
                {
                    grdQueryDetailReports.DataSource = ds.Tables[0];
                    grdQueryDetailReports.DataBind();
                }
                else
                {
                    grdQueryDetailReports.DataSource = null;
                    grdQueryDetailReports.DataBind();
                }

                if (ds.Tables[1] != null)
                {
                    grdCommulativeReports.DataSource = ds.Tables[1];
                    grdCommulativeReports.DataBind();
                }
                else
                {
                    grdCommulativeReports.DataSource = null;
                    grdCommulativeReports.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();

                grdQueryDetailReports.DataSource = null;
                grdQueryDetailReports.DataBind();

                grdCommulativeReports.DataSource = null;
                grdCommulativeReports.DataBind();

                Session["QUERY_INVID"] = drpSite.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillVisit();

                grdQueryDetailReports.DataSource = null;
                grdQueryDetailReports.DataBind();

                grdCommulativeReports.DataSource = null;
                grdCommulativeReports.DataBind();

                Session["QUERY_SUBJID"] = drpPatient.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillModule();

                grdQueryDetailReports.DataSource = null;
                grdQueryDetailReports.DataBind();

                grdCommulativeReports.DataSource = null;
                grdCommulativeReports.DataBind();

                Session["QUERY_VISITID"] = drpVisit.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillFielde();

                grdQueryDetailReports.DataSource = null;
                grdQueryDetailReports.DataBind();

                grdCommulativeReports.DataSource = null;
                grdCommulativeReports.DataBind();

                Session["QUERY_MODULEID"] = drpModule.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["INVID"]))
                {
                    drpSite.SelectedValue = Request.QueryString["INVID"].ToString();
                    drpPatient.SelectedValue = Request.QueryString["SUBJID"].ToString();
                    drpVisit.SelectedValue = Request.QueryString["VISITNUM"].ToString();
                    drpModule.SelectedValue = Request.QueryString["MODULEID"].ToString();
                    drpField.SelectedValue = Request.QueryString["FIELDID"].ToString();
                    drpQueryStatus.SelectedValue = Request.QueryString["QUERY_STATUS"].ToString();
                    drpQueryType.SelectedValue = Request.QueryString["QUERY_TYPE"].ToString();
                }
               GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            string url = Request.RawUrl;
            if (url.Contains("?"))
            {
                string path = url.Substring(0, url.IndexOf("?"));
                Response.Redirect(path);
            }
            else
            {
                Response.Redirect(url);
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

        protected void grdQueryDetailReports_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();

                DataSet ds = dal_DM.DM_QUERY_REPORT_SP(ACTION: "GetQueryDetailsByID", QUERYID: ID);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string MODULEID = ds.Tables[0].Rows[0]["MODULEID"].ToString();
                    string MODULENAME = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                    string VISITID = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                    string VISIT = ds.Tables[0].Rows[0]["VISIT"].ToString();
                    string INVID = ds.Tables[0].Rows[0]["INVID"].ToString();
                    string SUBJID = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                    string RECID = ds.Tables[0].Rows[0]["RECID"].ToString();

                    Session["QUERY_FIELDID"] = drpField.SelectedValue;
                    Session["QUERY_STATUS"] = drpQueryStatus.SelectedValue;
                    Session["QUERY_TYPE"] = drpQueryType.SelectedValue;

                    string UserType = Session["UserType"].ToString();

                    var URL = "";

                    if (UserType == "Site")
                    {
                        if ((ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0) && ds.Tables[1].Rows[0]["COUNTS"].ToString() == "1")
                        {
                            if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "1")
                            {
                                URL = "DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                            }
                            else
                            {
                                URL = "DM_DataEntry_INV_Read_Only.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                            }
                        }
                        else
                        {
                            if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "1")
                            {
                                URL = "DM_DataEntry_MultipleData.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                            }
                            else
                            {
                                URL = "DM_DataEntry.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                            }
                        }
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "1")
                        {
                            URL = "DM_DataEntry_MultipleData_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                        }
                        else
                        {
                            URL = "DM_DataEntry_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                        }
                    }

                    Session["QUERY_URL"] = "DM_Query_Reports.aspx";
                    Response.Redirect(URL);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdQueryDetailReports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lnkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                if (dr["STATUSTEXT"].ToString() == "Open Query" || dr["STATUSTEXT"].ToString() == "Answered Query")
                {
                    lnkSelect.Visible = true;
                }

                if (Session["UserType"].ToString() == "Site")
                {
                    lnkSelect.Text = "Answer";
                }
                else
                {
                    lnkSelect.Text = "Action";
                }

                LinkButton lblComment = (LinkButton)e.Row.FindControl("lblComment");
                if (dr["Query_Comment_Count"].ToString() != "0")
                {
                    lblComment.Visible = true;
                }
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
                DataSet ds = dal_DM.DM_QUERY_REPORT_SP(
                ACTION: "GET_BLINDED_QUERY_REPORT_EXPORT",
                INVID: drpSite.SelectedValue,
                SUBJID: drpPatient.SelectedValue,
                VISITNUM: drpVisit.SelectedValue,
                MODULEID: drpModule.SelectedValue,
                FIELDID: drpField.SelectedValue,
                QUERY_STATUS: drpQueryStatus.SelectedValue,
                QUERY_TYPE: drpQueryType.SelectedValue
                );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_DM Query Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

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

        protected void btnclosed_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpQueryStatus.SelectedItem.Text == "Open Query")
                {
                    if (drpSite.SelectedValue != "0")
                    {
                        Response.Redirect("DM_Query_Close_Bulk.aspx?INVID=" + drpSite.SelectedValue + "&SUBJID=" + drpPatient.SelectedValue + "&VISITNUM=" + drpVisit.SelectedValue + "&MODULEID=" + drpModule.SelectedValue + "&FIELDID=" + drpField.SelectedValue + "&QUERY_STATUS=" + drpQueryStatus.SelectedValue + "&QUERY_TYPE=" + drpQueryType.SelectedValue + "");
                    }
                    else
                    {
                         ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Please Select Site ID.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void drpQueryStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpQueryStatus.SelectedItem.Text == "Open Query")
            {
                btnclosed.Visible = true;
            }
            else
            {
                btnclosed.Visible = false;
            }
        }
    }
}