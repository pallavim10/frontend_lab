using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class NCTMS_VISIT_MASTER : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GETDATA();
                    GetIndication();
                    GetAddedModule();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_VISITTYPE_MASTER");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdVisitType.DataSource = ds;
                    grdVisitType.DataBind();

                    ddlvisitID.DataSource = ds;
                    ddlvisitID.DataTextField = "VISIT_NAME";
                    ddlvisitID.DataValueField = "ID";
                    ddlvisitID.DataBind();
                    ddlvisitID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    grdVisitType.DataSource = null;
                    grdVisitType.DataBind();

                    ddlvisitID.DataSource = null;
                    ddlvisitID.DataBind();
                    ddlvisitID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_VISITTYPE_MASTER();
                GETDATA();
                txtVisitInitial.Text = "";
                txtVisitName.Text = "";
                txtVisitSEQNO.Text = "";
                ddlUnblind.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_VISITTYPE_MASTER();
                GETDATA();
                btnUpdate.Visible = false;
                btnSubmit.Visible = true;
                txtVisitInitial.Text = "";
                txtVisitName.Text = "";
                txtVisitSEQNO.Text = "";
                ddlUnblind.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_VISITTYPE_MASTER()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "INSERT_VISITTYPE_MASTER",
                SEQNO: txtVisitSEQNO.Text,
                VISITNAME: txtVisitName.Text,
                VISIT_INITIAL: txtVisitInitial.Text,
                Unblinded: ddlUnblind.SelectedValue
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdVisitType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;

                if (e.CommandName == "Edit1")
                {
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                    EDIT_VISITTYPE(ID);
                }
                else if (e.CommandName == "Delete1")
                {
                    DELETE_VISITTYPE(ID);
                    GETDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void EDIT_VISITTYPE(string ID)
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_VISITTYPE_MASTER_BYID", ID: ID);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtVisitSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                    txtVisitName.Text = ds.Tables[0].Rows[0]["VISIT_NAME"].ToString();
                    txtVisitInitial.Text = ds.Tables[0].Rows[0]["VISIT_INITIAL"].ToString();
                    ddlUnblind.SelectedValue = ds.Tables[0].Rows[0]["Unblind"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETE_VISITTYPE(string ID)
        {
            try
            {
                dal.CTMS_DATA_SP(ACTION: "DELETE_VISITTYPE_MASTER", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void UPDATE_VISITTYPE_MASTER()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "UPDATE_VISITTYPE_MASTER",
                SEQNO: txtVisitSEQNO.Text,
                VISITNAME: txtVisitName.Text,
                VISIT_INITIAL: txtVisitInitial.Text,
                Unblinded: ddlUnblind.SelectedValue,
                ID: ViewState["ID"].ToString()
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdVisitType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string VISITCOUNT = drv["VISITCOUNTS"].ToString();

                    LinkButton lbtndelete = (e.Row.FindControl("lbtndelete") as LinkButton);

                    if (VISITCOUNT == "0")
                    {
                        lbtndelete.Visible = true;
                    }
                    else
                    {
                        lbtndelete.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void GetIndication()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_INDICATION", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModuleIndication.DataSource = ds.Tables[0];
                    drpModuleIndication.DataValueField = "ID";
                    drpModuleIndication.DataTextField = "INDICATION";
                    drpModuleIndication.DataBind();
                    drpModuleIndication.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetAddedModule()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_MODULE_DM_PROJECT_MASTER_CTMS", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: drpModuleIndication.SelectedValue, VISITNUM: drpModuleVisit.SelectedValue);
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
                throw ex;
            }
        }

        protected void btnSubmitModule_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdModuleVisit.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)grdModuleVisit.Rows[i].FindControl("chkVisit");
                    CheckBox chkTracker = (CheckBox)grdModuleVisit.Rows[i].FindControl("chkTracker");

                    if (ChAction.Checked)
                    {
                        Label MODULEID = (Label)grdModuleVisit.Rows[i].FindControl("MODULEID");
                        Label MODULENAME = (Label)grdModuleVisit.Rows[i].FindControl("MODULENAME");

                        DataSet ds = dal.DM_ADD_UPDATE
                        (
                        ACTION: "INSERT_DM_PROJECT_MASTER_CTMS",
                        PROJECTID: Session["PROJECTID"].ToString(),
                        VISITNUM: drpModuleVisit.SelectedValue,
                        INDICATION: drpModuleIndication.SelectedValue,
                        MODULEID: MODULEID.Text,
                        MODULENAME: MODULENAME.Text,
                        USERID: Session["User_ID"].ToString(),
                        CTMS_TRACKER: chkTracker.Checked
                        );
                    }
                }
                GetModuleGrid();
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

        protected void drpModuleIndication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_VISITBY_INDICATION_CTMS", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: drpModuleIndication.SelectedValue, VISITNUM: drpModuleVisit.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdModuleVisit.DataSource = ds.Tables[1];
                    grdModuleVisit.DataBind();

                    drpModuleVisit.DataSource = ds.Tables[0];
                    drpModuleVisit.DataValueField = "ID";
                    drpModuleVisit.DataTextField = "VISIT_NAME";
                    drpModuleVisit.DataBind();
                }
                else
                {
                    drpModuleVisit.Items.Clear();

                    grdModuleVisit.DataSource = null;
                    grdModuleVisit.DataBind();
                }

                GetModuleGrid();
                GetAddedModule();
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
                GetModuleGrid();
                GetAddedModule();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GetModuleGrid()
        {
            try
            {

                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_MODULEBY_INDICATION_VISIT_CTMS", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: drpModuleIndication.SelectedValue, VISITNUM: drpModuleVisit.SelectedValue);
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
                DAL dal;
                dal = new DAL();
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string VisitNum = (gvr.FindControl("VISITNUM") as Label).Text;
                string MODULEID = (gvr.FindControl("MODULEID") as Label).Text;
                string INDICATIONID = (gvr.FindControl("INDICATIONID") as Label).Text;

                if (e.CommandName == "DeleteField")
                {
                    DataSet ds = dal.DM_ADD_UPDATE(ACTION: "DELETE_CTMS_PROJECT_MASTER_ALL", INDICATION: INDICATIONID, VISITNUM: VisitNum, MODULEID: MODULEID);
                    GetAddedModule();
                    GetModuleGrid();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_EMAILIDS()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_EMAILS", VISITID: ddlvisitID.SelectedValue);

                gvVisitEmailds.DataSource = ds;
                gvVisitEmailds.DataBind();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitVisitEmails_Click(object sender, EventArgs e)
        {
            try
            {
                for (int a = 0; a < gvVisitEmailds.Rows.Count; a++)
                {
                    Label lblSiteID = gvVisitEmailds.Rows[a].FindControl("lblSiteID") as Label;
                    TextBox txtEMAILIDs = gvVisitEmailds.Rows[a].FindControl("txtEMAILIDs") as TextBox;
                    TextBox txtCCEMAILIDs = gvVisitEmailds.Rows[a].FindControl("txtCCEMAILIDs") as TextBox;
                    TextBox txtBCCEMAILIDs = gvVisitEmailds.Rows[a].FindControl("txtBCCEMAILIDs") as TextBox;

                    dal.CTMS_DATA_SP
                        (
                        ACTION: "INSERT_EMAILIDS",
                        EMAIL_IDS: txtEMAILIDs.Text,
                        CCEMAIL_IDS: txtCCEMAILIDs.Text,
                        BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                        SITEID: lblSiteID.Text,
                        VISITID: ddlvisitID.SelectedValue
                        );

                }

                GET_EMAILIDS();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Record Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelVisitEmails_Click(object sender, EventArgs e)
        {
            try
            {
                GET_EMAILIDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlvisitID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_EMAILIDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}