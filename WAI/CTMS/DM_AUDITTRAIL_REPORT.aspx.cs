using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_AUDITTRAIL_REPORT : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", false);
                }

                if (!this.IsPostBack)
                {
                    GET_SITEID();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_SITEID()
        {
            try
            {
                DAL dal = new DAL();

                DataSet ds = dal.GET_INVID_SP(
                        USERID: Session["User_ID"].ToString()
                        );

                ddlSiteId.DataSource = ds.Tables[0];
                ddlSiteId.DataValueField = "INVNAME";
                ddlSiteId.DataBind();
                ddlSiteId.Items.Insert(0, new ListItem("--Select--", "0"));

                GET_SUBJECT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSiteId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBJECT();

                grdAuditTrail.DataSource = null;
                grdAuditTrail.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_SUBJECT()
        {
            try
            {
                DataSet ds = dal_DM.DM_AUDITTRAIL_SP(ACTION: "GET_SUBJID", INVID: ddlSiteId.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSubjectId.DataSource = ds;
                    ddlSubjectId.DataTextField = "SUBJID";
                    ddlSubjectId.DataValueField = "SUBJID";
                    ddlSubjectId.DataBind();
                    ddlSubjectId.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    ddlSubjectId.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSubjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_VISIT();

                grdAuditTrail.DataSource = null;
                grdAuditTrail.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_VISIT()
        {
            try
            {
                DataSet ds = dal_DM.DM_AUDITTRAIL_SP(ACTION: "GET_VISITS", SUBJID: ddlSubjectId.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVisitId.DataSource = ds;
                    ddlVisitId.DataTextField = "VISIT";
                    ddlVisitId.DataValueField = "VISITNUM";
                    ddlVisitId.DataBind();
                    ddlVisitId.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    ddlVisitId.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_MODULE();

                grdAuditTrail.DataSource = null;
                grdAuditTrail.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_MODULE()
        {
            try
            {
                DataSet ds = dal_DM.DM_AUDITTRAIL_SP(ACTION: "GET_MODULES",
                SUBJID: ddlSubjectId.SelectedValue,
                VISITNUM: ddlVisitId.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModuleId.DataSource = ds;
                    ddlModuleId.DataTextField = "MODULENAME";
                    ddlModuleId.DataValueField = "ID";
                    ddlModuleId.DataBind();
                    ddlModuleId.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    ddlModuleId.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModuleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_FIELD();

                grdAuditTrail.DataSource = null;
                grdAuditTrail.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_FIELD()
        {
            try
            {
                DataSet ds = dal_DM.DM_AUDITTRAIL_SP(ACTION: "GET_FIELDS",
                MODULEID: ddlModuleId.SelectedValue,
                VISITNUM: ddlVisitId.SelectedValue,
                SUBJID: ddlSubjectId.SelectedValue
                );

                ddlFieldId.Items.Clear();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFieldId.DataSource = ds;
                    ddlFieldId.DataTextField = "FIELDNAME";
                    ddlFieldId.DataValueField = "FIELD_ID";
                    ddlFieldId.DataBind();
                    ddlFieldId.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    ddlFieldId.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlFieldId_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAuditTrail.DataSource = null;
            grdAuditTrail.DataBind();
        }

        protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAuditTrail.DataSource = null;
            grdAuditTrail.DataBind();
        }

        protected void ddlTransact_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAuditTrail.DataSource = null;
            grdAuditTrail.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
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

        protected void GET_DATA()
        {
            try
            {
                DataSet ds = dal_DM.DM_AUDITTRAIL_SP(ACTION: "GET_AUDIT_REPORT",
                    INVID: ddlSiteId.SelectedValue,
                    SUBJID: ddlSubjectId.SelectedValue,
                    VISITNUM: ddlVisitId.SelectedValue,
                    MODULEID: ddlModuleId.SelectedValue,
                    FIELDID: ddlFieldId.SelectedValue,
                    REASON: ddlReason.SelectedValue,
                    TRANSACT: ddlTransact.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdAuditTrail.DataSource = ds;
                    grdAuditTrail.DataBind();
                }
                else
                {
                    grdAuditTrail.DataSource = null;
                    grdAuditTrail.DataBind();
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
                DataSet ds = dal_DM.DM_AUDITTRAIL_SP(ACTION: "GET_AUDIT_REPORT_EXPORT",
                INVID: ddlSiteId.SelectedValue,
                SUBJID: ddlSubjectId.SelectedValue,
                VISITNUM: ddlVisitId.SelectedValue,
                MODULEID: ddlModuleId.SelectedValue,
                FIELDID: ddlFieldId.SelectedValue,
                REASON: ddlReason.SelectedValue,
                TRANSACT: ddlTransact.SelectedValue
                );

                ds.Tables[0].TableName = "AuditTrail Report";

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_AuditTrail Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel(ds, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }        
    }
}