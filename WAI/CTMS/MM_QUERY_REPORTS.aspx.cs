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

namespace CTMS
{
    public partial class MM_QUERY_REPORTS : System.Web.UI.Page
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

                if (!this.Page.IsPostBack)
                {
                    FillINV();
                    FillVisit();
                    FillModule();

                    if (Session["MM_QUERY_TYPE"] != null)
                    {
                        if (drpQueryType.Items.Contains(new ListItem(Session["MM_QUERY_TYPE"].ToString())))
                        {
                            drpQueryType.SelectedValue = Session["MM_QUERY_TYPE"].ToString();
                        }
                    }

                    Session["MM_QUERY_TYPE"] = drpQueryType.SelectedValue;

                    if (Session["MM_QUERY_STATUS"] != null)
                    {
                        if (drpQueryStatus.Items.Contains(new ListItem(Session["MM_QUERY_STATUS"].ToString())))
                        {
                            drpQueryStatus.SelectedValue = Session["MM_QUERY_STATUS"].ToString();
                        }
                    }

                    Session["MM_QUERY_STATUS"] = drpQueryStatus.SelectedValue;

                    if (Session["BACKTO_MM_QUERY_REPORT"] != null)
                    {
                        btnSearch_Click(this, e);
                        Session.Remove("BACKTO_MM_QUERY_REPORT");
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
            try
            {
                DAL dal = new DAL();
                DataSet ds = dal.GET_INVID_SP(
                        USERID: Session["User_ID"].ToString()
                        );

                drpSite.DataSource = ds.Tables[0];
                drpSite.DataValueField = "INVID";
                drpSite.DataBind();

                FillSubject();

                if (Session["MM_QUERY_INVID"] != null)
                {
                    if (drpSite.Items.Contains(new ListItem(Session["MM_QUERY_INVID"].ToString())))
                    {
                        drpSite.SelectedValue = Session["MM_QUERY_INVID"].ToString();
                    }
                }

                Session["MM_QUERY_INVID"] = drpSite.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "GET_SUBJECT_LIST", INVID: drpSite.SelectedValue);

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

                if (Session["MM_QUERY_SUBJID"] != null)
                {
                    if (drpPatient.Items.FindByValue(Session["MM_QUERY_SUBJID"].ToString()) != null)
                    {
                        drpPatient.SelectedValue = Session["MM_QUERY_SUBJID"].ToString();
                    }
                }

                Session["MM_QUERY_SUBJID"] = drpPatient.SelectedValue;
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
                DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "GET_VISIT_LIST",
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

                if (Session["MM_QUERY_VISITID"] != null)
                {
                    if (drpVisit.Items.FindByValue(Session["MM_QUERY_VISITID"].ToString()) != null)
                    {
                        drpVisit.SelectedValue = Session["MM_QUERY_VISITID"].ToString();
                    }
                }

                Session["MM_QUERY_VISITID"] = drpVisit.SelectedValue;
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
                DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "GET_MODULES_LIST",
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

                if (Session["MM_QUERY_MODULEID"] != null)
                {
                    if (drpModule.Items.FindByValue(Session["MM_QUERY_MODULEID"].ToString()) != null)
                    {
                        drpModule.SelectedValue = Session["MM_QUERY_MODULEID"].ToString();
                    }
                }

                Session["MM_QUERY_MODULEID"] = drpModule.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();

                grdMMQueryDetailReports.DataSource = null;
                grdMMQueryDetailReports.DataBind();

                Session["MM_QUERY_INVID"] = drpSite.SelectedValue;
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

                grdMMQueryDetailReports.DataSource = null;
                grdMMQueryDetailReports.DataBind();

                Session["MM_QUERY_SUBJID"] = drpPatient.SelectedValue;
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

                grdMMQueryDetailReports.DataSource = null;
                grdMMQueryDetailReports.DataBind();

                Session["MM_QUERY_VISITID"] = drpVisit.SelectedValue;
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
                grdMMQueryDetailReports.DataSource = null;
                grdMMQueryDetailReports.DataBind();

                Session["MM_QUERY_MODULEID"] = drpModule.SelectedValue;
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
                BIND_MM_QUERY_DATA();
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

        protected void BIND_MM_QUERY_DATA()
        {
            try
            {
                DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "BIND_MM_QUERY_DATA",
                INVID: drpSite.SelectedValue,
                SUBJID: drpPatient.SelectedValue,
                VISITNUM: drpVisit.SelectedValue,
                MODULEID: drpModule.SelectedValue,
                QUERY_STATUS: drpQueryStatus.SelectedValue,
                QUERY_TYPE: drpQueryType.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdMMQueryDetailReports.DataSource = ds.Tables[0];
                    grdMMQueryDetailReports.DataBind();
                }
                else
                {
                    grdMMQueryDetailReports.DataSource = null;
                    grdMMQueryDetailReports.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdMMQueryDetailReports_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                Session["MM_QUERY_STATUS"] = drpQueryStatus.SelectedValue;
                Session["MM_QUERY_TYPE"] = drpQueryType.SelectedValue;
                Session["MM_QUERY_SUBJID"] = drpPatient.SelectedValue;
                Session["MM_QUERY_INVID"] = drpSite.SelectedValue;
                Session["MM_QUERY_VISITID"] = drpVisit.SelectedValue;
                Session["MM_QUERY_MODULEID"] = drpModule.SelectedValue;

                if (e.CommandName == "Push")
                {
                    Response.Redirect("MM_Push_Query.aspx?QUERYID=" + rowIndex);
                }
                else if (e.CommandName == "Link")
                {
                    Response.Redirect("MM_LINKED_QUERY.aspx?QUERYID=" + rowIndex);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdMMQueryDetailReports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                HtmlControl DivLinkPush = (HtmlControl)e.Row.FindControl("DivLinkPush");
                HtmlControl divActivity = (HtmlControl)e.Row.FindControl("divActivity");
                Label ACTIVITY = (Label)e.Row.FindControl("ACTIVITY");
                Label BYNAME = (Label)e.Row.FindControl("BYNAME");
                Label CAL_DAT = (Label)e.Row.FindControl("CAL_DAT");
                Label CAL_TZDAT = (Label)e.Row.FindControl("CAL_TZDAT");

                if (dr["PUSHED"].ToString() == "True")
                {
                    DivLinkPush.Visible = false;
                    divActivity.Visible = true;
                    ACTIVITY.Text = "Pushed To DM";
                    BYNAME.Text = dr["PUSHEDBYNAME"].ToString();
                    CAL_DAT.Text = dr["PUSHED_CAL_DAT"].ToString();
                    CAL_TZDAT.Text = dr["PUSHED_CAL_TZDAT"].ToString();
                }

                if (dr["LINKED"].ToString() == "True")
                {
                    DivLinkPush.Visible = false;
                    divActivity.Visible = true;
                    ACTIVITY.Text = "Linked To DM";
                    BYNAME.Text = dr["LINKEDBYNAME"].ToString();
                    CAL_DAT.Text = dr["LINKED_CAL_DAT"].ToString();
                    CAL_TZDAT.Text = dr["LINKED_CAL_TZDAT"].ToString();
                }
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
                DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "BIND_MM_QUERY_DATA_EXPORT",
                 INVID: drpSite.SelectedValue,
                 SUBJID: drpPatient.SelectedValue,
                 VISITNUM: drpVisit.SelectedValue,
                 MODULEID: drpModule.SelectedValue,
                 QUERY_STATUS: drpQueryStatus.SelectedValue,
                 QUERY_TYPE: drpQueryType.SelectedValue
                 );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_MM Query Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

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
    }
}