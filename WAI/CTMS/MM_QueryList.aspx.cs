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
using CTMS;

namespace Risk_Management
{
    public partial class QueryList : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_MM = new DAL_MM();
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
                    GET_SITE();

                    bool SessionExist = false;

                    if (Session["drpSite"] != null)
                    {
                        drpSite.SelectedValue = Session["drpSite"].ToString();
                        GET_SUBJID();
                        SessionExist = true;
                        Session.Remove("drpSite");
                    }

                    if (Session["drpPatient"] != null)
                    {
                        drpPatient.SelectedValue = Session["drpPatient"].ToString();
                        GET_VISITS();
                        SessionExist = true;
                        Session.Remove("drpPatient");
                    }

                    if (Session["drpVisit"] != null)
                    {
                        drpVisit.SelectedValue = Session["drpVisit"].ToString();
                        GET_MODULES();
                        SessionExist = true;
                        Session.Remove("drpVisit");
                    }

                    if (Session["drpModule"] != null)
                    {
                        drpModule.SelectedValue = Session["drpModule"].ToString();
                        SessionExist = true;
                        Session.Remove("drpModule");
                    }

                    if (Session["drpQueryStatus"] != null)
                    {
                        drpQueryStatus.SelectedValue = Session["drpQueryStatus"].ToString();
                        SessionExist = true;
                        Session.Remove("drpQueryStatus");
                    }

                    if (Session["drpQueryType"] != null)
                    {
                        drpQueryType.SelectedValue = Session["drpQueryType"].ToString();
                        SessionExist = true;
                        Session.Remove("drpQueryType");
                    }

                    if (SessionExist)
                    {
                        GET_QUERY_FOR_REVIEW();
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_SITE()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GET_INVID_SP(USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSite.DataSource = ds.Tables[0];
                    drpSite.DataValueField = "INVNAME";
                    drpSite.DataBind();
                    drpSite.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    drpSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBJID()
        {
            try
            {
                DataSet ds = dal.GET_SUBJECT_SP(INVID: drpSite.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_VISITS()
        {
            try
            {
                DataSet ds = dal_MM.MM_QUERY_SP(
                    ACTION: "GET_VISITS",
                    SUBJID: drpPatient.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpVisit.DataSource = ds.Tables[0];
                    drpVisit.DataValueField = "VISITNUM";
                    drpVisit.DataTextField = "VISIT";
                    drpVisit.DataBind();
                    drpVisit.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    drpVisit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_MODULES()
        {
            try
            {
                DataSet ds = dal_MM.MM_QUERY_SP(
                  ACTION: "GET_MODULES",
                  SUBJID: drpPatient.SelectedValue,
                  VISITNUM: drpVisit.SelectedValue
                  );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds.Tables[0];
                    drpModule.DataValueField = "MODULEID";
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GET_QUERY_FOR_REVIEW()
        {
            try
            {
                DataSet ds = dal_MM.MM_QUERY_SP(
                ACTION: "GET_QUERY_FOR_REVIEW",
                SITEID: drpSite.SelectedValue,
                SUBJID: drpPatient.SelectedValue,
                VISITNUM: drpVisit.SelectedValue,
                MODULEID: drpModule.SelectedValue,
                STATUS: drpQueryStatus.SelectedValue,
                QUERYTYPE: drpQueryType.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdQUERY.DataSource = ds.Tables[0];
                    grdQUERY.DataBind();
                }
                else
                {
                    grdQUERY.DataSource = null;
                    grdQUERY.DataBind();
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
                GET_SUBJID();
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
                GET_VISITS();
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
                GET_MODULES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdQUERY_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Session["drpSite"] = drpSite.SelectedValue;
                Session["drpPatient"] = drpPatient.SelectedValue;
                Session["drpVisit"] = drpVisit.SelectedValue;
                Session["drpModule"] = drpModule.SelectedValue;
                Session["drpQueryStatus"] = drpQueryStatus.SelectedValue;
                Session["drpQueryType"] = drpQueryType.SelectedValue;

                if (e.CommandName == "ChangeQueryText")
                {
                    Response.Redirect("MM_Change_Report_QueryText.aspx?ID=" + e.CommandArgument);
                }
                else if (e.CommandName == "DELETEQUERY")
                {
                    Response.Redirect("MM_Delete_Query.aspx?ID=" + e.CommandArgument);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdQUERY_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lbldeletequery = (LinkButton)e.Row.FindControl("lbldeletequery");
                LinkButton lbtnChangeQueryText = (LinkButton)e.Row.FindControl("lbtnChangeQueryText");
                CheckBox chkreview = (CheckBox)e.Row.FindControl("chkreview");

                if (dr["MM_REVIEW"].ToString() == "True")
                {
                    chkreview.Visible = false;
                }
                else
                {
                    chkreview.Visible = true;
                    chkreview.ToolTip = "Review";
                }

                if (dr["Status"].ToString() != "Pending for Review/Post")
                {
                    chkreview.Visible = false;
                    lbldeletequery.Visible = false;
                    lbtnChangeQueryText.Visible = false;
                }

                if (dr["DELETE"].ToString() == "True")
                {
                    chkreview.Visible = false;
                    lbldeletequery.Visible = false;
                    lbtnChangeQueryText.Visible = false;
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

        protected void btnReview_Click(object sender, EventArgs e)
        {
            try
            {
                bool chkChecked = false;

                for (int i = 0; i < grdQUERY.Rows.Count; i++)
                {
                    CheckBox chkreview = (CheckBox)grdQUERY.Rows[i].FindControl("chkreview");
                    if (chkreview.Checked == true)
                    {
                        chkChecked = true;

                        string rowIndex = ((Label)grdQUERY.Rows[i].FindControl("ID")).Text;

                        DataSet ds = dal_MM.MM_QUERY_SP(ACTION: "UPDATE_QUERY_REVIEW", ID: rowIndex);
                    }
                }

                if (!chkChecked)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select at least one Query');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Queries Reviewed/Posted to DM Successfully'); window.location='MM_QueryList.aspx';", true);
                }
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
                DataSet ds = dal_MM.MM_QUERY_SP(
                    ACTION: "GET_QUERY_FOR_REVIEW_EXPORT",
                    SITEID: drpSite.SelectedValue,
                    SUBJID: drpPatient.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue,
                    MODULEID: drpModule.SelectedValue,
                    STATUS: drpQueryStatus.SelectedValue
                );

                ds.Tables[0].TableName = "MM Query for Review Reports";
                Multiple_Export_Excel.ToExcel(ds, "MM Query for Review Reports.xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public static bool IsDecimalCol(object o)
        {
            decimal result_ignored;
            return o != null &&
              !(o is DBNull) &&
              decimal.TryParse(Convert.ToString(o), out result_ignored);
        }

        public static bool IsIntCol(object o)
        {
            int result_ignored;
            return o != null &&
              !(o is DBNull) &&
              int.TryParse(Convert.ToString(o), out result_ignored);
        }

        public static bool IsDateCol(object o)
        {
            DateTime result_ignored;
            return o != null &&
              !(o is DBNull) &&
              DateTime.TryParse(Convert.ToString(o), out result_ignored);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GET_QUERY_FOR_REVIEW();
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

        protected void ShowDeleteComments(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

                Label lblID = this.Master.FindControl("lblID") as Label;
                lblID.Text = ID;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "OpenPopUp('popup_DeleteComments','Delete Comments');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}