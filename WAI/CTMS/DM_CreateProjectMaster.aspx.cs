using CTMS.CommonFunction;
using PPT;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DM_CreateProjectMaster : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_DB dal_DB = new DAL_DB();
        CommonFunction.CommonFunction CF = new CommonFunction.CommonFunction();

        public string FieldColorValue = "#000000";
        public string AnsColorValue = "#000000";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_VISITS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GET_VISITS()
        {
            try
            {
                DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_VISIT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModuleVisit.DataSource = ds.Tables[0];
                    drpModuleVisit.DataValueField = "VISITNUM";
                    drpModuleVisit.DataTextField = "VISIT";
                    drpModuleVisit.DataBind();

                    GetAllModule();
                    GetAddedModule();

                }
                else
                {
                    drpModuleVisit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpModuleVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetAllModule();
                GetAddedModule();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GetAllModule()
        {
            try
            {
                DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_DM_MODULES",
                    VISITNUM: drpModuleVisit.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdModuleVisit.DataSource = ds.Tables[0];
                    grdModuleVisit.DataBind();
                }
                else
                {
                    grdModuleVisit.DataSource = null;
                    grdModuleVisit.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GetAddedModule()
        {
            try
            {
                DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_MODULE_DM_PROJECT_MASTER",
                    VISITNUM: drpModuleVisit.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdModule.DataSource = ds.Tables[0];
                    grdModule.DataBind();
                }
                else
                {
                    grdModule.DataSource = null;
                    grdModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdModule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string DataExist = dr["DataExist"].ToString();

                    if (DataExist != "0")
                    {
                        LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");
                        lbtndeleteSection.Visible = false;
                    }

                    HtmlControl iconESource_Module = (HtmlControl)e.Row.FindControl("iconESource_Module");
                    if (dr["eSOURCE_MODULE"].ToString() == "True")
                    {
                        iconESource_Module.Attributes.Add("class", "fa fa-check");
                        iconESource_Module.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconESource_Module.Attributes.Add("class", "fa fa-times");
                        iconESource_Module.Attributes.Add("style", "color: red;");
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdModule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string VisitNum = (gvr.FindControl("VISITNUM") as Label).Text;
                string MODULEID = (gvr.FindControl("MODULEID") as Label).Text;

                if (e.CommandName == "DeleteModule")
                {
                    DataSet ds = dal_DB.DB_DM_SP(ACTION: "DELETE_DM_PROJECT_MASTER_ALL",
                        VISITNUM: VisitNum,
                        MODULEID: MODULEID
                        );
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Module Removed Successfully.');", true);

                    GetAddedModule();
                    GetAllModule();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitModule_Click(object sender, EventArgs e)
        {
            try
            {
                bool Chkselect = false;
               
                for (int i = 0; i < grdModuleVisit.Rows.Count; i++)
                {
                    
                    CheckBox ChAction = (CheckBox)grdModuleVisit.Rows[i].FindControl("chkVisit");
                    CheckBox chkeSourceModule = (CheckBox)grdModuleVisit.Rows[i].FindControl("chkeSourceModule");

                    if (ChAction.Checked)
                    {
                        Label MODULEID = (Label)grdModuleVisit.Rows[i].FindControl("MODULEID");
                        Label MODULENAME = (Label)grdModuleVisit.Rows[i].FindControl("MODULENAME");

                        DataSet ds = dal_DB.DB_DM_SP
                        (
                        ACTION: "INSERT_DM_PROJECT_MASTER",
                        VISITNUM: drpModuleVisit.SelectedValue,
                        MODULEID: MODULEID.Text,
                        MODULENAME: MODULENAME.Text,
                        chk_eSourceModule : chkeSourceModule.Checked
                        );

                        Chkselect = true;
                    }
                }
                if(!Chkselect)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select at least one Module.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Module added in " + drpModuleVisit.SelectedItem.Text + " visit Successfully.');", true);
                }
                
                GetAllModule();
                GetAddedModule();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnCancelModule_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdModule_PreRender(object sender, EventArgs e)
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

        protected void grdModuleVisit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string REVIEW_COUNT = dr["REVIEW_COUNT"].ToString();

                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                    if (REVIEW_COUNT != "0")
                    {
                        lblStatus.Text = "";
                    }
                    else
                    {
                        CheckBox chkVisit = (CheckBox)e.Row.FindControl("chkVisit");
                        chkVisit.Visible = false;
                        lblStatus.Text = "Review Pending";
                    }
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