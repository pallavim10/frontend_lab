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
    public partial class DM_SetLabDefault : System.Web.UI.Page
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

                lblList.Text = "Set Lab Reference Range Mapping (" + Request.QueryString["MODULENAME"].ToString() + ")";
                if (!Page.IsPostBack)
                {
                    Bind_Visit();
                    GET_PRIMARY_Module();
                    GET_SECONDARY_Module();
                    GET_MAPPING_Fields();
                    GET_DATA();

                    if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv();", true);
                        grdLabDefaults.Columns[18].Visible = false;
                    }
                    else
                    {
                        grdLabDefaults.Columns[18].Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void Bind_Visit()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpVisit.DataSource = ds.Tables[0];
                    drpVisit.DataValueField = "VISITNUM";
                    drpVisit.DataTextField = "VISIT";
                    drpVisit.DataBind();
                    drpVisit.Items.Insert(0, new ListItem("--Select--", "0"));

                    drpPrimaryVisit.DataSource = ds.Tables[0];
                    drpPrimaryVisit.DataValueField = "VISITNUM";
                    drpPrimaryVisit.DataTextField = "VISIT";
                    drpPrimaryVisit.DataBind();
                    drpPrimaryVisit.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpVisit.Items.Clear();
                    drpPrimaryVisit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_PRIMARY_Module()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_PRIMARY_SINGLE_Module");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSexModule.DataSource = ds.Tables[0];
                    drpSexModule.DataValueField = "ID";
                    drpSexModule.DataTextField = "MODULENAME";
                    drpSexModule.DataBind();
                    drpSexModule.Items.Insert(0, new ListItem("--Select--", "0"));

                    DrpPrimaryModule.DataSource = ds.Tables[0];
                    DrpPrimaryModule.DataValueField = "ID";
                    DrpPrimaryModule.DataTextField = "MODULENAME";
                    DrpPrimaryModule.DataBind();
                    DrpPrimaryModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSexModule.DataSource = null;
                    drpSexModule.DataBind();

                    DrpPrimaryModule.DataSource = null;
                    DrpPrimaryModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DrpPrimaryModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_Fields(DrpPrimaryModule.SelectedValue, DrpPrimaryFields);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SECONDARY_Module()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_SECONDARY_Module");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DrpSecModule.DataSource = ds.Tables[0];
                    DrpSecModule.DataValueField = "ID";
                    DrpSecModule.DataTextField = "MODULENAME";
                    DrpSecModule.DataBind();
                    DrpSecModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DrpSecModule.DataSource = null;
                    DrpSecModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_Fields(string Module_ID = null, DropDownList drp = null)
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_LAB_FIELDS", MODULEID: Module_ID);

                drp.DataSource = ds.Tables[0];
                drp.DataValueField = "ID";
                drp.DataTextField = "FIELDNAME";
                drp.DataBind();
                drp.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSexModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_FIELDS", MODULEID: drpSexModule.SelectedValue);

                drpSexFieldId.DataSource = ds.Tables[0];
                drpSexFieldId.DataValueField = "ID";
                drpSexFieldId.DataTextField = "FIELDNAME";
                drpSexFieldId.DataBind();
                drpSexFieldId.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MAPPING_Fields()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_FIELDS", MODULEID: Request.QueryString["ID"].ToString());

                drpLabIdField.DataSource = ds.Tables[0];
                drpLabIdField.DataValueField = "ID";
                drpLabIdField.DataTextField = "FIELDNAME";
                drpLabIdField.DataBind();
                drpLabIdField.Items.Insert(0, new ListItem("--Select--", "0"));

                drpLowLimitField.DataSource = ds.Tables[0];
                drpLowLimitField.DataValueField = "ID";
                drpLowLimitField.DataTextField = "FIELDNAME";
                drpLowLimitField.DataBind();
                drpLowLimitField.Items.Insert(0, new ListItem("--Select--", "0"));

                drpUpperLimitField.DataSource = ds.Tables[0];
                drpUpperLimitField.DataValueField = "ID";
                drpUpperLimitField.DataTextField = "FIELDNAME";
                drpUpperLimitField.DataBind();
                drpUpperLimitField.Items.Insert(0, new ListItem("--Select--", "0"));

                drpTestNameField.DataSource = ds.Tables[0];
                drpTestNameField.DataValueField = "ID";
                drpTestNameField.DataTextField = "FIELDNAME";
                drpTestNameField.DataBind();
                drpTestNameField.Items.Insert(0, new ListItem("--Select--", "0"));

                drpUnitFieldId.DataSource = ds.Tables[0];
                drpUnitFieldId.DataValueField = "ID";
                drpUnitFieldId.DataTextField = "FIELDNAME";
                drpUnitFieldId.DataBind();
                drpUnitFieldId.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DrpSecModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_Fields(DrpSecModule.SelectedValue, DrpSecFields);
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
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_DEFAULT_MAPPING", MODULEID: Request.QueryString["ID"].ToString());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdLabDefaults.DataSource = ds.Tables[0];
                    grdLabDefaults.DataBind();
                }
                else
                {
                    grdLabDefaults.DataSource = null;
                    grdLabDefaults.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnsubmitDefine_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "INSERT_DEFAULT_MAPPING",
                   VISITNUM: drpVisit.SelectedValue,
                   MODULEID: Request.QueryString["ID"].ToString(),
                   PRIMARY_VISITNUM: drpPrimaryVisit.SelectedValue,
                   PRIMARY_MODULEID: DrpPrimaryModule.SelectedValue,
                   PRIMARY_FIELDID: DrpPrimaryFields.SelectedValue,
                   SECONDARY_MODULEID: DrpSecModule.SelectedValue,
                   SECONDARY_FIELDID: DrpSecFields.SelectedValue,
                   SEX_MODULEID: drpSexModule.SelectedValue,
                   SEX_FIELDID: drpSexFieldId.SelectedValue,
                   AGE_UNIT: drpAgeUnit.SelectedValue,
                   LABID_FIELDID: drpLabIdField.SelectedValue,
                   LL_FIELDID: drpLowLimitField.SelectedValue,
                   UL_FIELDID: drpUpperLimitField.SelectedValue,
                   TESTNAME_FIELDID: drpTestNameField.SelectedValue,
                   UNIT_FIELDID: drpUnitFieldId.SelectedValue
                   );

                Response.Write("<script>alert('Lab Defaults mapping add successfully!')</script>");
                string jScript = "<script>window.open('DM_CreateModuleNew.aspx', '_self', ''); window.close();</script>"; ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", jScript);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl.ToString());
        }

        protected void grdLabDefaults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "EditCrit")
                {
                    ViewState["EDIT_CRIT"] = id;
                    SELECT_CRIT(id);
                    lbtnsubmitDefine.Visible = false;
                    lbtnUpdateDefine.Visible = true;

                    if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                    {
                        lbtnsubmitDefine.Visible = false;
                        lbtnUpdateDefine.Visible = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv();", true);
                    }
                }
                else if (e.CommandName == "DeleteCrit")
                {
                    DELETE_CRIT(id);

                    Response.Write("<script> alert('Lab Reference Range deleted successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_CRIT(string id)
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_DEFAULT_MAPPING_BYID", ID: id);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Bind_Visit();
                    drpVisit.SelectedValue = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                    drpPrimaryVisit.SelectedValue = ds.Tables[0].Rows[0]["PRIMARY_VISITNUM"].ToString();

                    GET_PRIMARY_Module();
                    DrpPrimaryModule.SelectedValue = ds.Tables[0].Rows[0]["PRIMARY_MODULEID"].ToString();

                    GET_Fields(DrpPrimaryModule.SelectedValue, DrpPrimaryFields);
                    DrpPrimaryFields.SelectedValue = ds.Tables[0].Rows[0]["PRIMARY_FIELDID"].ToString();

                    GET_SECONDARY_Module();
                    DrpSecModule.SelectedValue = ds.Tables[0].Rows[0]["SECONDARY_MODULEID"].ToString();

                    GET_Fields(DrpSecModule.SelectedValue, DrpSecFields);
                    DrpSecFields.SelectedValue = ds.Tables[0].Rows[0]["SECONDARY_FIELDID"].ToString();

                    GET_Fields(DrpSecModule.SelectedValue, DrpSecFields);
                    DrpSecFields.SelectedValue = ds.Tables[0].Rows[0]["SECONDARY_FIELDID"].ToString();

                    drpSexModule.SelectedValue = ds.Tables[0].Rows[0]["SEX_COL_MODULEID"].ToString();

                    drpSexModule_SelectedIndexChanged(this, EventArgs.Empty);
                    drpSexFieldId.SelectedValue = ds.Tables[0].Rows[0]["SEX_COL_FIELDID"].ToString();

                    drpAgeUnit.SelectedValue = ds.Tables[0].Rows[0]["AGE_UNIT"].ToString();

                    GET_MAPPING_Fields();
                    drpLabIdField.SelectedValue = ds.Tables[0].Rows[0]["LABID_COL_FIELDID"].ToString();
                    drpLowLimitField.SelectedValue = ds.Tables[0].Rows[0]["LL_COL_FIELDID"].ToString();
                    drpUpperLimitField.SelectedValue = ds.Tables[0].Rows[0]["UL_COL_FIELDID"].ToString();
                    drpTestNameField.SelectedValue = ds.Tables[0].Rows[0]["TESTNAME_COL_FIELDID"].ToString();
                    drpUnitFieldId.SelectedValue = ds.Tables[0].Rows[0]["UNIT_COL_FIELDID"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void DELETE_CRIT(string ID)
        {
            try
            {
                dal_DB.DB_LAB_REFERENCE_SP(ACTION: "DELETE_LAB_DEFAULT_BYID", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "UPDATE_DEFAULT_MAPPING",
                   VISITNUM: drpVisit.SelectedValue,
                   MODULEID: Request.QueryString["ID"].ToString(),
                   PRIMARY_VISITNUM: drpPrimaryVisit.SelectedValue,
                   PRIMARY_MODULEID: DrpPrimaryModule.SelectedValue,
                   PRIMARY_FIELDID: DrpPrimaryFields.SelectedValue,
                   SECONDARY_MODULEID: DrpSecModule.SelectedValue,
                   SECONDARY_FIELDID: DrpSecFields.SelectedValue,
                   SEX_MODULEID: drpSexModule.SelectedValue,
                   SEX_FIELDID: drpSexFieldId.SelectedValue,
                   AGE_UNIT: drpAgeUnit.SelectedValue,
                   LABID_FIELDID: drpLabIdField.SelectedValue,
                   LL_FIELDID: drpLowLimitField.SelectedValue,
                   UL_FIELDID: drpUpperLimitField.SelectedValue,
                   UNIT_FIELDID: drpUnitFieldId.SelectedValue,
                   TESTNAME_FIELDID: drpTestNameField.SelectedValue,
                   ID: ViewState["EDIT_CRIT"].ToString()
                   );

                Response.Write("<script>alert('Lab Defaults mapping updated successfully!')</script>");
                string jScript = "<script>window.open('DM_CreateModuleNew.aspx', '_self', ''); window.close();</script>"; ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", jScript);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdLabDefaults_PreRender(object sender, EventArgs e)
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