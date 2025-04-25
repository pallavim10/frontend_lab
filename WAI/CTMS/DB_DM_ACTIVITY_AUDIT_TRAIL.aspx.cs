using CTMS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DB_DM_ACTIVITY_AUDIT_TRAIL : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                {
                    GET_Visits();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlActivity.SelectedValue == "Visits" || ddlActivity.SelectedValue == "Visit Visibility")
                {
                    GET_Visits();

                    divVisit.Visible = true;
                    divModules.Visible = false;
                    divFields.Visible = false;
                    btnShow.Visible = true;
                }
                else if (ddlActivity.SelectedValue == "Visit Level Fields" || ddlActivity.SelectedValue == "Visit Level Fields Options")
                {
                    GET_Visits();
                    GET_MODULES();
                    GET_FIELDS();

                    divVisit.Visible = true;
                    divModules.Visible = true;
                    divFields.Visible = true;
                    btnShow.Visible = true;
                }
                else
                {
                    divVisit.Visible = false;
                    divModules.Visible = false;
                    divFields.Visible = false;
                    btnShow.Visible = false;
                }

                grdRecords.DataSource = null;
                grdRecords.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_Visits()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT_FOR_AUDIT_TRAIL");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpVisit.DataSource = ds;
                    drpVisit.DataTextField = "VISIT";
                    drpVisit.DataValueField = "VISITNUM";
                    drpVisit.DataBind();
                    drpVisit.Items.Insert(0, new ListItem("All", "0"));
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

        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_MODULES();
                GET_FIELDS();

                grdRecords.DataSource = null;
                grdRecords.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_MODULES()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_MODULES_BY_VISIT_FOR_AT",
                    VISITNUM: drpVisit.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds;
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataValueField = "MODULEID";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                }
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
                GET_FIELDS();

                grdRecords.DataSource = null;
                grdRecords.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_FIELDS()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_FIELD_BY_MODULES_BY_VISIT_FOR_AT",
                    ID: drpModule.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlFields.DataSource = ds;
                    ddlFields.DataTextField = "FIELDNAME";
                    ddlFields.DataValueField = "ID";
                    ddlFields.DataBind();
                    ddlFields.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlFields.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdRecords.DataSource = null;
                grdRecords.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_ACTIVITY_AUDIT_TRAIL_SP(ACTION: ddlActivity.SelectedValue,
                    MODULEID: drpModule.SelectedValue,
                    FIELDID: ddlFields.SelectedValue,
                    VISITID: drpVisit.SelectedValue
                   );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdRecords.DataSource = ds.Tables[0];
                    grdRecords.DataBind();

                    btnExportExcel.Visible = true;
                }
                else
                {
                    grdRecords.DataSource = null;
                    grdRecords.DataBind();

                    btnExportExcel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_ACTIVITY_AUDIT_TRAIL_SP(ACTION: ddlActivity.SelectedValue,
                    MODULEID: drpModule.SelectedValue,
                    FIELDID: ddlFields.SelectedValue,
                    VISITID: drpVisit.SelectedValue
                   );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_" + ddlActivity.SelectedValue + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel(ds, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdField_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }
    }
}