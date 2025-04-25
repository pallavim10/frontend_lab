using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class SAE_QUERY_REPORT : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_SAE dal_SAE = new DAL_SAE();
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
                    FillSITEID();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillSITEID()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(USERID: Session["User_ID"].ToString());
                ddlSite.DataSource = ds.Tables[0];
                ddlSite.DataValueField = "INVID";
                ddlSite.DataBind();

                FillSubject();
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
                DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_SAE_SUBJECTS",
                    INVID: ddlSite.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSUBJID.DataSource = ds.Tables[0];
                    ddlSUBJID.DataValueField = "SUBJID";
                    ddlSUBJID.DataTextField = "SUBJID";
                    ddlSUBJID.DataBind();
                    ddlSUBJID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
                }
                else
                {
                    ddlSUBJID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_SAEID()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_REPORT_SP(ACTION: "GET_SAEIDS", SUBJID: ddlSUBJID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSAEID.DataSource = ds.Tables[0];
                    drpSAEID.DataValueField = "SAEID";
                    drpSAEID.DataTextField = "SAEID";
                    drpSAEID.DataBind();
                    drpSAEID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
                }
                else
                {
                    drpSAEID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void ddlSUBJID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SAEID();

                gridData.DataSource = null;
                gridData.DataBind();
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
                FillSubject();

                gridData.DataSource = null;
                gridData.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSAEID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gridData.DataSource = null;
                gridData.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                GET_SAE_QUERY_REPORT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SAE_QUERY_REPORT()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_REPORT_SP(ACTION: "GET_SAE_QUERY_REPORT", ID: ddlSite.SelectedValue, SUBJID: ddlSUBJID.SelectedValue, SAEID: drpSAEID.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    gridData.DataSource = ds;
                    gridData.DataBind();
                }
                else
                {
                    gridData.DataSource = null;
                    gridData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gridData_PreRender(object sender, EventArgs e)
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

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lblComment = (LinkButton)e.Row.FindControl("lblComment");
                if (dr["QryAnsCount"].ToString() != "0")
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
                DataSet ds = dal_SAE.SAE_REPORT_SP(ACTION: "GET_SAE_QUERY_REPORT_EXPORT",
                    ID: ddlSite.SelectedValue,
                    SUBJID: ddlSUBJID.SelectedValue,
                    SAEID: drpSAEID.SelectedValue
                    );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_SAE Query Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

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