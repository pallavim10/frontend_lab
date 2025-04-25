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
    public partial class DM_REFRESH_LAB_REF_RANGE : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserGroup_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!IsPostBack)
                {
                    FillINV();
                    GET_TestName();
                    GET_Modules();
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

            DataSet ds = dal.GET_INVID_SP(USERID: Session["User_ID"].ToString());

            ddlSITE.DataSource = ds.Tables[0];
            ddlSITE.DataValueField = "INVID";
            ddlSITE.DataBind();
            ddlSITE.Items.Insert(0, new ListItem("--Select--", "0"));


        }

        protected void ddlSITE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_LabName();

                grd_Records.DataSource = null;
                grd_Records.DataBind();
                btnExport.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LabName()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(
                    ACTION: "GET_LAB",
                    SITEID: ddlSITE.SelectedValue
                    );

                ddlLabName.DataSource = ds.Tables[0];
                ddlLabName.DataValueField = "LABID";
                ddlLabName.DataTextField = "LABNAME";
                ddlLabName.DataBind();
                ddlLabName.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlLabName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grd_Records.DataSource = null;
                grd_Records.DataBind();
                btnExport.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_TestName()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(
                ACTION: "GET_TestName"
                );

                ddlTestName.DataSource = ds.Tables[0];
                ddlTestName.DataValueField = "VALUE";
                ddlTestName.DataTextField = "TEXT";
                ddlTestName.DataBind();
                ddlTestName.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlTestName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grd_Records.DataSource = null;
                grd_Records.DataBind();
                btnExport.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_Modules()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(
                ACTION: "GET_LAB_MODULES"
                );

                drpModuleName.DataSource = ds.Tables[0];
                drpModuleName.DataValueField = "MODULEID";
                drpModuleName.DataTextField = "MODULENAME";
                drpModuleName.DataBind();
                drpModuleName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grd_Records.DataSource = null;
                grd_Records.DataBind();
                btnExport.Visible = false;
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
                GET_LAB_SUBJECT_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LAB_SUBJECT_DATA()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_LAB_MODULE_DATA",
                    SITEID: ddlSITE.SelectedValue,
                    LABID: ddlLabName.SelectedValue,
                    LABNAME: ddlLabName.SelectedItem.Text,
                    TESTNAME: ddlTestName.SelectedValue,
                    MODULEID: drpModuleName.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Remove("RowNo");
                    grd_Records.DataSource = ds;
                    grd_Records.DataBind();
                    btnRefresh.Visible = true;
                    btnExport.Visible = true;
                }
                else
                {
                    grd_Records.DataSource = null;
                    grd_Records.DataBind();
                    btnRefresh.Visible = false;
                    btnExport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl.ToString());
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "UPDATE_LAB_MODULE_DATA",
                    SITEID: ddlSITE.SelectedValue,
                    LABID: ddlLabName.SelectedValue,
                    LABNAME: ddlLabName.SelectedItem.Text,
                    TESTNAME: ddlTestName.SelectedValue,
                    MODULEID: drpModuleName.SelectedValue
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lab Referance Ranges refreshed successfully.');", true);

                GET_LAB_SUBJECT_DATA();
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
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_LAB_MODULE_DATA",
                     SITEID: ddlSITE.SelectedValue,
                     LABID: ddlLabName.SelectedValue,
                     LABNAME: ddlLabName.SelectedItem.Text,
                     TESTNAME: ddlTestName.SelectedValue,
                     MODULEID: drpModuleName.SelectedValue
                     );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Lab Reference Range(" + drpModuleName.SelectedItem.Text + ")_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel(ds, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}