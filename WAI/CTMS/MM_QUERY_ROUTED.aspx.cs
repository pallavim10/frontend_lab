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

namespace CTMS
{
    public partial class MM_QUERY_ROUTED : System.Web.UI.Page
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

                    if (Session["drpQueryType"] != null)
                    {
                        drpQueryType.SelectedValue = Session["drpQueryType"].ToString();
                        SessionExist = true;
                        Session.Remove("drpQueryType");
                    }

                    if (SessionExist)
                    {
                        GET_QUERY_ROUTEDtoMM();
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

        protected void GET_QUERY_ROUTEDtoMM()
        {
            try
            {
                DataSet ds = dal_MM.MM_QUERY_SP(
                ACTION: "GET_QUERY_ROUTEDtoMM",
                SITEID: drpSite.SelectedValue,
                SUBJID: drpPatient.SelectedValue,
                VISITNUM: drpVisit.SelectedValue,
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
                Session["drpQueryType"] = drpQueryType.SelectedValue;

                if (e.CommandName == "TakeAction")
                {   
                    Response.Redirect("MM_QueryRouted_Comments.aspx?ID=" + e.CommandArgument);
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
                DataSet ds = dal_MM.MM_QUERY_SP(
                    ACTION: "GET_QUERY_ROUTEDtoMM_EXPORT",
                    SITEID: drpSite.SelectedValue,
                    SUBJID: drpPatient.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue,
                    MODULEID: drpModule.SelectedValue,
                    QUERYTYPE: drpQueryType.SelectedValue
                );

                ds.Tables[0].TableName = "MM Query Routed From DM Reports";
                Multiple_Export_Excel.ToExcel(ds, "MM Query Routed From DM  Reports.xls", Page.Response);
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
                GET_QUERY_ROUTEDtoMM();
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

    }
}