using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;


namespace CTMS
{
    public partial class MM_QueryText_Change : System.Web.UI.Page
    {
        DAL dal = new DAL();
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
                    BIND_MM_QUERY_DATA();
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
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GET_INVID_SP();

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

        private void FillSubject()
        {
            try
            {
                //DataSet ds = dal.DM_QUERY_SP(
                //    ACTION: "GET_SUBJECT_LIST_MR",
                //    INVID: drpSite.SelectedValue
                //);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    drpPatient.DataSource = ds.Tables[0];
                //    drpPatient.DataValueField = "SUBJID";
                //    drpPatient.DataTextField = "SUBJID";
                //    drpPatient.DataBind();
                //    drpPatient.Items.Insert(0, new ListItem("--Select--", "0"));
                //}
                //else
                //{
                //    drpPatient.Items.Clear();
                //}
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
                //DataSet ds = dal.DM_QUERY_SP(
                //    ACTION: "GET_VISIT_LIST_MR",
                //    SUBJID: drpPatient.SelectedValue
                //    );

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    drpVisit.DataSource = ds.Tables[0];
                //    drpVisit.DataValueField = "VISITNUM";
                //    drpVisit.DataTextField = "VISIT";
                //    drpVisit.DataBind();
                //    drpVisit.Items.Insert(0, new ListItem("--Select Visit--", "0"));
                //}
                //else
                //{
                //    drpVisit.Items.Clear();
                //}
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
                //DataSet ds = dal.DM_QUERY_SP(
                //  ACTION: "GET_MODULES_MR",
                //  SUBJID: drpPatient.SelectedValue,
                //  VISITNUM: drpVisit.SelectedValue
                //  );
                
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    drpModule.DataSource = ds.Tables[0];
                //    drpModule.DataValueField = "MODULEID";
                //    drpModule.DataTextField = "MODULENAME";
                //    drpModule.DataBind();
                //    drpModule.Items.Insert(0, new ListItem("--Select Module--", "0"));
                //}
                //else
                //{
                //    drpModule.Items.Clear();
                //}
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void BIND_MM_QUERY_DATA()
        {
            try
            {
                //DataSet ds = dal.DM_QUERY_SP(
                //ACTION: "BIND_MM_QUERY_DATA_FOR_QUERYCHANGE",
                //INVID: drpSite.SelectedValue,
                //SUBJID: drpPatient.SelectedValue,
                //VISITNUM: drpVisit.SelectedValue,
                //PAGENUM: drpModule.SelectedValue,
                //QUERYSTATUS: drpQueryStatus.SelectedValue,
                //QUERYTYPE: drpQueryType.SelectedValue
                //);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    grdMMQueryDetailReports.DataSource = ds.Tables[0];
                //    grdMMQueryDetailReports.DataBind();
                //}
                //else
                //{
                //    grdMMQueryDetailReports.DataSource = null;
                //    grdMMQueryDetailReports.DataBind();
                //}
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
                if (e.CommandName == "ChangeQueryText")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                    string MODULEID = (row.FindControl("MODULEID") as Label).Text;
                    string MODULENAME = (row.FindControl("Txt_MODULENAME") as Label).Text;
                    string VISIT = (row.FindControl("VISIT") as Label).Text;
                    string INVID = (row.FindControl("INVID") as Label).Text;
                    string SUBJID = (row.FindControl("Subject") as Label).Text;

                    Response.Redirect("MM_Change_Report_QueryText.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&ID=" + rowIndex);
                }
                else if (e.CommandName == "DELETEQUERY")
                {
                    string rowIndex = e.CommandArgument.ToString();
                    TextBox lblMMQueryID = this.Master.FindControl("lblMMQueryID") as TextBox;
                    lblMMQueryID.Text = rowIndex;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "OpenPopUp('MM_popup_DeleteComments','Delete Reason');", true);
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

                LinkButton lbldeletequery = (LinkButton)e.Row.FindControl("lbldeletequery");
                LinkButton lbtnChangeQueryText = (LinkButton)e.Row.FindControl("lbtnChangeQueryText");
                Label txtDELETEBY = (Label)e.Row.FindControl("txtDELETEBY");
                Label RESOLVEBY = (Label)e.Row.FindControl("RESOLVEBY");
                Label OPENBY = (Label)e.Row.FindControl("OPENBY");
                Label Reviewed = (Label)e.Row.FindControl("Reviewed");
                CheckBox chkreview = (CheckBox)e.Row.FindControl("chkreview");

                if (dr["MM_REVIEW"].ToString() == "1")
                {
                    chkreview.Visible = false;
                    Reviewed.Text = dr["REVIEWEDBY"].ToString() + " (" + dr["REVIEWEDDAT"].ToString() + ")";
                }
                else
                {
                    chkreview.Visible = true;
                    chkreview.ToolTip = "Review";
                }

                if (dr["Status"].ToString() != "Open")
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

                    txtDELETEBY.Text = dr["DELETEBY"].ToString() + " (" + dr["DELETEDAT"].ToString() + ")";
                }

                if (dr["RESOLVEBY"].ToString() != "")
                {
                    RESOLVEBY.Text = dr["RESOLVEBY"].ToString() + " (" + dr["RESOLVE_DT"].ToString() + ")";
                }

                if (dr["OPENBY"].ToString() != "")
                {
                    OPENBY.Text = dr["OPENBY"].ToString() + " (" + dr["RAISE_DT"].ToString() + ")";
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
               
                for (int i = 0; i < grdMMQueryDetailReports.Rows.Count; i++)
                {
                    CheckBox chkreview = (CheckBox)grdMMQueryDetailReports.Rows[i].FindControl("chkreview");
                    if(chkreview.Checked == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check CheckBox')", true);
                    }
                    else if (chkreview.Checked == true)
                    {
                        string rowIndex = ((Label)grdMMQueryDetailReports.Rows[i].FindControl("ID")).Text;

                        //DataSet ds = dal.DM_QUERY_SP(ACTION: "UPDATE_QUERY_REVIEW",
                        //ID: rowIndex
                        //);

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                    }
                }
                BIND_MM_QUERY_DATA();
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
                //DataSet ds = dal.DM_QUERY_SP(
                // ACTION: "BIND_MM_QUERY_DATA_FOR_QUERYCHANGE_EXPORT",
                // INVID: drpSite.SelectedValue,
                // SUBJID: drpPatient.SelectedValue,
                // VISITNUM: drpVisit.SelectedValue,
                // PAGENUM: drpModule.SelectedValue,
                // QUERYSTATUS: drpQueryStatus.SelectedValue,
                // QUERYTYPE: drpQueryType.SelectedValue
                // );

                //ds.Tables[0].TableName = "MM Query Reports" + commFun.GetCurrentDateTimeByTimezone();
                //Multiple_Export_Excel.ToExcel(ds, "MM Query Reports" + commFun.GetCurrentDateTimeByTimezone() + ".xls", Page.Response);
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