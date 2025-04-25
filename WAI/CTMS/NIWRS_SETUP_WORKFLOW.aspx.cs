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
    public partial class NIWRS_SETUP_WORKFLOW : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtEventHistory.Attributes.Add("MaxLength", "200");
            txtMSGBOX.Attributes.Add("MaxLength", "200");
            try
            {

                if (!Page.IsPostBack)
                {
                    GET_REVIEW_STATUS();
                    GET_STEP();
                    GET_STATUS();
                    GET_VISIT();
                    GET_AutoPopList();
                    GET_SETFIELDS();
                    GET_OPEN_EMAIL();
                    DISABLE_BUTTONS();
                    GET_ParentMenu();
                }

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "CallCkedit", "CallCkedit();", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_REVIEW_STATUS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_CONFIGURATION_REVIEW");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //Review
                    hdnREVIEWSTATUS.Value = ds.Tables[0].Rows[0]["ANS"].ToString();
                }
                else
                {
                    //Unreview
                    hdnREVIEWSTATUS.Value = "Unreview";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void DISABLE_BUTTONS()
        {
            if (hdnREVIEWSTATUS.Value == "Review")
            {
                btnsubmit.Enabled = false;
                btnUpdate.Enabled = false;
                btnsubmit.Text = "Configuration has been Frozen";
                btnUpdate.Text = "Configuration has been Frozen";
                btnsubmit.CssClass = btnsubmit.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdate.CssClass = btnUpdate.CssClass.Replace("btn-primary", "btn-danger");
            }
        }
        private void GET_AutoPopList()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_LISTING");
                ddlAutoPopList.DataSource = ds.Tables[0];
                ddlAutoPopList.DataValueField = "ID";
                ddlAutoPopList.DataTextField = "LISTNAME";
                ddlAutoPopList.DataBind();
                ddlAutoPopList.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSOURCE_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SOURCE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SOURCE()
        {
            try
            {
                DataSet ds = new DataSet();
                if (ddlSOURCE_TYPE.SelectedValue == "Listing")
                {
                    if (btnUpdate.Visible == true)
                    {
                        ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_LISTING");
                    }
                    else
                    {
                        ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_LISTING_SOURCE");
                    }
                    ddlSource.DataSource = ds.Tables[0];
                    ddlSource.DataValueField = "ID";
                    ddlSource.DataTextField = "LISTNAME";
                    ddlSource.DataBind();
                    ddlSource.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (ddlSOURCE_TYPE.SelectedValue == "Form")
                {
                    if (btnUpdate.Visible == true)
                    {
                        ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_FORM");
                    }
                    else
                    {
                        ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_FORM_SOURCE");
                    }
                    ddlSource.DataSource = ds.Tables[0];
                    ddlSource.DataValueField = "ID";
                    ddlSource.DataTextField = "FORMNAME";
                    ddlSource.DataBind();
                    ddlSource.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlSource.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_NavSOURCE()
        {
            try
            {
                DataSet ds = new DataSet();
                if (ddlNavType.SelectedValue == "Listing")
                {
                    ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_LISTING");
                    ddlNavTo.DataSource = ds.Tables[0];
                    ddlNavTo.DataValueField = "ID";
                    ddlNavTo.DataTextField = "LISTNAME";
                    ddlNavTo.DataBind();
                    ddlNavTo.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (ddlNavType.SelectedValue == "Form")
                {
                    ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_FORM");
                    ddlNavTo.DataSource = ds.Tables[0];
                    ddlNavTo.DataValueField = "ID";
                    ddlNavTo.DataTextField = "FORMNAME";
                    ddlNavTo.DataBind();
                    ddlNavTo.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlNavTo.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_STATUS()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS");
                ddlStatus.DataSource = ds.Tables[0];
                ddlStatus.DataValueField = "STATUSCODE";
                ddlStatus.DataTextField = "STATUSNAME";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_VISIT()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_VISIT");
                ddlVisit.DataSource = ds.Tables[0];
                ddlVisit.DataValueField = "ID";
                ddlVisit.DataTextField = "VISIT";
                ddlVisit.DataBind();
                ddlVisit.Items.Insert(0, new ListItem("None", "0"));

                ddlNextVisit.DataSource = ds.Tables[0];
                ddlNextVisit.DataValueField = "ID";
                ddlNextVisit.DataTextField = "VISIT";
                ddlNextVisit.DataBind();
                ddlNextVisit.Items.Insert(0, new ListItem("None", "0"));

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SETFIELDS()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_COLS");

                repeatSetFields.DataSource = ds;
                repeatSetFields.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlNavType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_NavSOURCE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_STEP();
                GET_STEP();
                CLEAR_STEP();

                Response.Write("<script>alert('Step Added Successfully.');</script>");

                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("IWRS");
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
                UPDATE_STEP();
                GET_STEP();
                CLEAR_STEP();

                Response.Write("<script>alert('Step updated Successfully.');</script>");

                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("IWRS");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_STEP();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_STEP()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_STEP");
                grdSteps.DataSource = ds;
                grdSteps.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_STEP()
        {
            try
            {
                string HEADER = null, SEQNO = null, SOURCE_TYPE = null, SOURCE_ID = null, MSGBOX = null, NAVTO_TYPE = null, NAVTO = null,
                    EVENTHIST = null, PERFORM = null, ApplVisit = null, AutoPopList = null, SETFIELD = ",",
                    NAV_PARENT = null, EMAIL_SUBJECT = null, EMAIL_BODY = null, NextVisit = null;
                bool NAVMENU = false, SEND_EMAIL = false, DOWNLOAD = false;

                HEADER = txtHeader.Text;
                SEQNO = txtSEQNO.Text;
                SOURCE_TYPE = ddlSOURCE_TYPE.SelectedValue;
                SOURCE_ID = ddlSource.SelectedValue;
                ApplVisit = ddlVisit.SelectedValue;
                if (ddlVisit.SelectedValue != "0")
                {
                    NextVisit = ddlNextVisit.SelectedValue;
                }
                AutoPopList = ddlAutoPopList.SelectedValue;
                NAVMENU = chkNavMenu.Checked;
                NAV_PARENT = drpNAV_PARENT.SelectedValue;
                DOWNLOAD = chkDownloadAble.Checked;
                PERFORM = ddlPerform.SelectedValue;
                MSGBOX = txtMSGBOX.Text;
                EVENTHIST = txtEventHistory.Text;

                if (ddlStatus.SelectedIndex != 0)
                {
                    SETFIELD = " [STATUS] = '" + ddlStatus.SelectedValue + "',";
                }

                for (int i = 0; i < repeatSetFields.Items.Count; i++)
                {
                    TextBox txtSetFieldVal = (TextBox)repeatSetFields.Items[i].FindControl("txtSetFieldVal");
                    HiddenField hfCOLNAME = (HiddenField)repeatSetFields.Items[i].FindControl("hfCOLNAME");
                    if (txtSetFieldVal.Text != "")
                    {
                        if (SETFIELD != "")
                        {
                            SETFIELD += " [" + hfCOLNAME.Value + "] = '" + txtSetFieldVal.Text + "',";
                        }
                    }
                }

                NAVTO_TYPE = ddlNavType.SelectedValue;
                NAVTO = ddlNavTo.SelectedValue;

                SEND_EMAIL = chkSendEmail.Checked;
                if (SEND_EMAIL)
                {
                    EMAIL_SUBJECT = txtEmailSubject.Text;
                    EMAIL_BODY = txtEmailBody.Text;
                }
                else
                {
                    EMAIL_SUBJECT = "";
                    EMAIL_BODY = "";
                }

                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP
                    (
                    ACTION: "INSERT_STEP",
                    HEADER: HEADER,
                    SEQNO: SEQNO,
                    SOURCE_TYPE: SOURCE_TYPE,
                    SOURCE_ID: SOURCE_ID,
                    NAVMENU: NAVMENU,
                    NAV_PARENT: NAV_PARENT,
                    MSGBOX: MSGBOX,
                    SETFIELD: SETFIELD.TrimEnd(','),
                    NAVTO_TYPE: NAVTO_TYPE,
                    NAVTO: NAVTO,
                    EVENTHIST: EVENTHIST,
                    PERFORM: PERFORM,
                    ApplVisit: ApplVisit,
                    NextVisit: NextVisit,
                    AutoPopList: AutoPopList,
                    SEND_EMAIL: SEND_EMAIL,
                    EMAIL_SUBJECT: EMAIL_SUBJECT,
                    EMAIL_BODY: EMAIL_BODY,
                    PROJECTID: Session["PROJECTID"].ToString(),
                    DOWNLOAD: DOWNLOAD,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                if (SETFIELD != "")
                {
                    INSERT_SETFIELD(ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    DELETE_SETFIELD_ALL(ds.Tables[0].Rows[0][0].ToString());
                }

                INSERT_EMAIL(ds.Tables[0].Rows[0][0].ToString());

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_STEP()
        {
            try
            {
                string HEADER = null, SEQNO = null, SOURCE_TYPE = null, SOURCE_ID = null, MSGBOX = null, NAVTO_TYPE = null, NAVTO = null,
                    EVENTHIST = null, PERFORM = null, ApplVisit = null, AutoPopList = null, SETFIELD = ",", NextVisit = null,
                    NAV_PARENT = null, EMAIL_SUBJECT = null, EMAIL_BODY = null;
                bool NAVMENU = false, SEND_EMAIL = false, DOWNLOAD = false;

                HEADER = txtHeader.Text;
                SEQNO = txtSEQNO.Text;
                SOURCE_TYPE = ddlSOURCE_TYPE.SelectedValue;
                SOURCE_ID = ddlSource.SelectedValue;
                ApplVisit = ddlVisit.SelectedValue;
                if (ddlVisit.SelectedValue != "0")
                {
                    NextVisit = ddlNextVisit.SelectedValue;
                }
                AutoPopList = ddlAutoPopList.SelectedValue;
                NAVMENU = chkNavMenu.Checked;
                NAV_PARENT = drpNAV_PARENT.SelectedValue;
                DOWNLOAD = chkDownloadAble.Checked;
                PERFORM = ddlPerform.SelectedValue;
                MSGBOX = txtMSGBOX.Text;
                EVENTHIST = txtEventHistory.Text;

                if (ddlStatus.SelectedIndex != 0)
                {
                    SETFIELD = " [STATUS] = '" + ddlStatus.SelectedValue + "',";
                }

                for (int i = 0; i < repeatSetFields.Items.Count; i++)
                {
                    TextBox txtSetFieldVal = (TextBox)repeatSetFields.Items[i].FindControl("txtSetFieldVal");
                    HiddenField hfCOLNAME = (HiddenField)repeatSetFields.Items[i].FindControl("hfCOLNAME");
                    if (txtSetFieldVal.Text != "")
                    {
                        if (SETFIELD != "")
                        {
                            SETFIELD += " [" + hfCOLNAME.Value + "] = '" + txtSetFieldVal.Text + "',";
                        }
                    }
                }

                NAVTO_TYPE = ddlNavType.SelectedValue;
                NAVTO = ddlNavTo.SelectedValue;

                SEND_EMAIL = chkSendEmail.Checked;
                if (SEND_EMAIL)
                {
                    EMAIL_SUBJECT = txtEmailSubject.Text;
                    EMAIL_BODY = txtEmailBody.Text;
                }
                else
                {
                    EMAIL_SUBJECT = "";
                    EMAIL_BODY = "";
                }

                dal_IWRS.NIWRS_WORKFLOW_SP
                   (
                   ACTION: "DELETE_NAVPARENT_STEP",
                   ID: ViewState["editStepID"].ToString(),
                   NAV_PARENT: NAV_PARENT,
                   PROJECTID: Session["PROJECTID"].ToString()
                   );

                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP
                    (
                    ACTION: "UPDATE_STEP",
                    ID: ViewState["editStepID"].ToString(),
                    HEADER: HEADER,
                    SEQNO: SEQNO,
                    SOURCE_TYPE: SOURCE_TYPE,
                    SOURCE_ID: SOURCE_ID,
                    NAVMENU: NAVMENU,
                    NAV_PARENT: NAV_PARENT,
                    MSGBOX: MSGBOX,
                    SETFIELD: SETFIELD.TrimEnd(','),
                    NAVTO_TYPE: NAVTO_TYPE,
                    NAVTO: NAVTO,
                    EVENTHIST: EVENTHIST,
                    PERFORM: PERFORM,
                    ApplVisit: ApplVisit,
                    NextVisit: NextVisit,
                    AutoPopList: AutoPopList,
                    SEND_EMAIL: SEND_EMAIL,
                    EMAIL_SUBJECT: EMAIL_SUBJECT,
                    EMAIL_BODY: EMAIL_BODY,
                    PROJECTID: Session["PROJECTID"].ToString(),
                    DOWNLOAD: DOWNLOAD,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                if (SETFIELD != "")
                {
                    INSERT_SETFIELD(ViewState["editStepID"].ToString());
                }
                else
                {
                    DELETE_SETFIELD_ALL(ViewState["editStepID"].ToString());
                }

                INSERT_EMAIL(ViewState["editStepID"].ToString());

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_STEP_EMAIL(string STEPID)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_STEP_EMAIL", STEPID: STEPID);
                gvEmailds.DataSource = ds;
                gvEmailds.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_OPEN_EMAIL()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_OPEN_EMAIL");
                gvEmailds.DataSource = ds;
                gvEmailds.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_EMAIL(string STEPID)
        {
            try
            {
                if (chkSendEmail.Checked)
                {
                    for (int i = 0; i < gvEmailds.Rows.Count; i++)
                    {
                        TextBox txtEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtEMAILIDs");
                        TextBox txtCCEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtCCEMAILIDs");
                        TextBox txtBCCEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtBCCEMAILIDs");
                        Label lblSiteID = (Label)gvEmailds.Rows[i].FindControl("lblSiteID");

                        if (txtEMAILIDs.Text != "")
                        {
                            dal_IWRS.NIWRS_WORKFLOW_SP
                                (
                                ACTION: "INSERT_STEP_EMAIL",
                                STEPID: STEPID,
                                SITEID: lblSiteID.Text,
                                EMAIL_IDS: txtEMAILIDs.Text,
                                CCEMAIL_IDS: txtCCEMAILIDs.Text,
                                BCCEMAIL_IDS: txtBCCEMAILIDs.Text,
                                ENTEREDBY: Session["USER_ID"].ToString()
                                );
                        }
                        else
                        {
                            dal_IWRS.NIWRS_WORKFLOW_SP
                                (
                                ACTION: "DELETE_STEP_EMAIL",
                                STEPID: STEPID,
                                SITEID: lblSiteID.Text,
                                ENTEREDBY: Session["USER_ID"].ToString()
                                );
                        }
                    }
                }
                else
                {
                    dal_IWRS.NIWRS_WORKFLOW_SP
                        (
                        ACTION: "DELETE_ALL_STEP_EMAIL",
                        STEPID: STEPID
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_SETFIELD(string ID)
        {
            try
            {
                if (ddlStatus.SelectedIndex != 0)
                {
                    dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "INSERT_SETFIELD", STEPID: ID, FIELDNAME: "STATUS", VALUE: ddlStatus.SelectedValue);
                }
                else
                {
                    dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_SETFIELD_STEP_FIELD", STEPID: ID, FIELDNAME: "STATUS");
                }

                for (int i = 0; i < repeatSetFields.Items.Count; i++)
                {
                    HiddenField hfCOLNAME = (HiddenField)repeatSetFields.Items[i].FindControl("hfCOLNAME");
                    TextBox txtSetFieldVal = (TextBox)repeatSetFields.Items[i].FindControl("txtSetFieldVal");
                    if (txtSetFieldVal.Text != "")
                    {
                        dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "INSERT_SETFIELD", STEPID: ID, FIELDNAME: hfCOLNAME.Value, VALUE: txtSetFieldVal.Text);
                    }
                    else
                    {
                        dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_SETFIELD_STEP_FIELD", STEPID: ID, FIELDNAME: hfCOLNAME.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_SETFIELD_ALL(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_SETFIELD_STEP_ALL", STEPID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_STEP(string ID)
        {
            try
            {

                btnsubmit.Visible = false;
                btnUpdate.Visible = true;

                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "SELECT_STEP", ID: ID);

                txtHeader.Text = ds.Tables[0].Rows[0]["HEADER"].ToString();
                txtSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                ddlSOURCE_TYPE.SelectedValue = ds.Tables[0].Rows[0]["SOURCE_TYPE"].ToString();
                GET_SOURCE();
                ddlSource.SelectedValue = ds.Tables[0].Rows[0]["SOURCE_ID"].ToString();
                ddlVisit.SelectedValue = ds.Tables[0].Rows[0]["ApplVisit"].ToString();
                ddlAutoPopList.SelectedValue = ds.Tables[0].Rows[0]["AutoPopList"].ToString();
                ddlNextVisit.SelectedValue = ds.Tables[0].Rows[0]["NextVisit"].ToString();
                chkNavMenu.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["NAVMENU"]);
                drpNAV_PARENT.SelectedValue = ds.Tables[0].Rows[0]["NAV_PARENT"].ToString();
                ddlPerform.SelectedValue = ds.Tables[0].Rows[0]["PERFORM"].ToString();
                txtMSGBOX.Text = ds.Tables[0].Rows[0]["MSGBOX"].ToString();
                txtEventHistory.Text = ds.Tables[0].Rows[0]["EVENTHIST"].ToString();
                ddlNavType.SelectedValue = ds.Tables[0].Rows[0]["NAVTO_TYPE"].ToString();
                GET_NavSOURCE();
                ddlNavTo.SelectedValue = ds.Tables[0].Rows[0]["NAVTO"].ToString();
                chkDownloadAble.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DOWNLOAD"]);
                chkSendEmail.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["SEND_EMAIL"]);
                txtEmailSubject.Text = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                txtEmailBody.Text = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();
                ddlStatus.SelectedIndex = 0;
                if (ds.Tables[0].Rows[0]["SETFIELD"].ToString() != "")
                {
                    DataSet dsSet = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_STEP_COLS", STEPID: ID);
                    repeatSetFields.DataSource = dsSet;
                    repeatSetFields.DataBind();

                    if (dsSet.Tables.Count > 1)
                    {
                        if (dsSet.Tables[1].Rows.Count > 0)
                        {
                            ddlStatus.SelectedValue = dsSet.Tables[1].Rows[0][0].ToString();
                        }
                    }
                }

                GET_STEP_EMAIL(ID);

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "showDivParent&ShowEmailDiv", "showDivParent();ShowEmailDiv();showDivNextVisit();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_STEP(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_STEP", ENTEREDBY: Session["USER_ID"].ToString(), ID: ID, PROJECTID: Session["PROJECTID"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined step deleted successfully.'); ", true);
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_STEP()
        {
            try
            {
                txtHeader.Text = "";
                txtSEQNO.Text = "";
                ddlSOURCE_TYPE.SelectedIndex = 0;
                GET_SOURCE();
                chkNavMenu.Checked = false;
                drpNAV_PARENT.SelectedIndex = 0;
                ddlPerform.SelectedIndex = 0;
                txtMSGBOX.Text = "";
                txtEventHistory.Text = "";
                ddlStatus.SelectedIndex = 0;
                GET_SETFIELDS();
                ddlNavType.SelectedIndex = 0;
                GET_NavSOURCE();
                ddlVisit.SelectedIndex = 0;
                ddlNextVisit.SelectedIndex = 0;
                ddlAutoPopList.SelectedIndex = 0;
                chkSendEmail.Checked = false;
                txtEmailSubject.Text = "";
                txtEmailBody.Text = "";
                GET_OPEN_EMAIL();

                btnsubmit.Visible = true;
                btnUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSteps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editStepID"] = id;
                if (e.CommandName == "EditStep")
                {
                    SELECT_STEP(id);
                }
                else if (e.CommandName == "DeleteStep")
                {
                    DELETE_STEP(id);
                    GET_STEP();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSteps_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string AddFields = dr["AddFields"].ToString();

                    LinkButton lbtnAddClicks = (LinkButton)e.Row.FindControl("lbtnAddClicks");

                    if (AddFields != "0")
                    {
                        lbtnAddClicks.Visible = true;
                    }
                    else
                    {
                        lbtnAddClicks.Visible = false;
                    }

                    Label lblPERFORM = (Label)e.Row.FindControl("lblPERFORM");

                    LinkButton lbtnMngDosage = (LinkButton)e.Row.FindControl("lbtnMngDosage");

                    if (lblPERFORM.Text == "Dosing")
                    {
                        lbtnMngDosage.Visible = true;
                    }
                    else
                    {
                        lbtnMngDosage.Visible = false;
                    }

                    LinkButton lbtnDelete = (e.Row.FindControl("lbtnDelete") as LinkButton);

                    string COUNT = dr["COUNT"].ToString();

                    if (COUNT != "0")
                    {
                        lbtnDelete.Visible = false;
                    }
                    else
                    {
                        lbtnDelete.Visible = true;
                    }

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtnDelete.Enabled = false;
                        lbtnDelete.ToolTip = "Configuration has been Frozen";
                        lbtnDelete.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "EXPORT_WORKFLOW");

                string xlname = "IWRS WorkFlow.xls";
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


        protected void grdSteps_PreRender(object sender, EventArgs e)
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

        protected void btnSubmitParentMenu_Click(object sender, EventArgs e)
        {
            try
            {
                dal_IWRS.NIWRS_WORKFLOW_SP(
                    ACTION: "INSERT_ParentMenu",
                    NAV_PARENT: txtParentMenu.Text,
                    SEQNO: txtParentMenuSEQNO.Text
                    );

                CLEAR_ParentMenu();
                GET_ParentMenu();
                Response.Write("<script>alert('Parent Manu Added Successfully.');</script>");
                modalMngParent.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnUpdateParentMenu_Click(object sender, EventArgs e)
        {
            try
            {
                dal_IWRS.NIWRS_WORKFLOW_SP(
                    ACTION: "UPDATE_ParentMenu",
                    NAV_PARENT: txtParentMenu.Text,
                    SEQNO: txtParentMenuSEQNO.Text,
                    ID: ViewState["editMenuID"].ToString()
                    );

                CLEAR_ParentMenu();
                GET_ParentMenu();
                Response.Write("<script>alert('Parent Manu Updated Successfully.');</script>");
                modalMngParent.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }


        protected void btnCancelParentMenu_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_ParentMenu();
                modalMngParent.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void grdParentMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editMenuID"] = id;
                if (e.CommandName == "EditMenu")
                {
                    SELECT_ParentMenu(id);
                }
                else if (e.CommandName == "DeleteMenu")
                {
                    DELETE_ParentMenu(id);
                    GET_ParentMenu();

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Defined Parent manu deleted successfully.')", true);
                }

                modalMngParent.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_ParentMenu()
        {
            try
            {
                txtParentMenu.Text = "";
                txtParentMenuSEQNO.Text = "";
                btnSubmitParentMenu.Visible = true;
                btnUpdateParentMenu.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_ParentMenu()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_ParentMenu");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdParentMenu.DataSource = ds;
                    grdParentMenu.DataBind();

                    drpNAV_PARENT.Items.Clear();

                    drpNAV_PARENT.DataSource = ds.Tables[0];
                    drpNAV_PARENT.DataValueField = "NAME";
                    drpNAV_PARENT.DataTextField = "NAME";
                    drpNAV_PARENT.DataBind();
                    drpNAV_PARENT.Items.Insert(0, new ListItem("None", "0"));
                    drpNAV_PARENT.Items.Insert(1, new ListItem("Report (Blinded)", "Blinded"));
                    drpNAV_PARENT.Items.Insert(2, new ListItem("Report (Unblinded)", "Unblinded"));
                }
                else
                {
                    grdParentMenu.DataSource = null;
                    grdParentMenu.DataBind();

                    drpNAV_PARENT.Items.Clear();

                    drpNAV_PARENT.Items.Insert(0, new ListItem("None", "0"));
                    drpNAV_PARENT.Items.Insert(1, new ListItem("Report (Blinded)", "Blinded"));
                    drpNAV_PARENT.Items.Insert(2, new ListItem("Report (Unblinded)", "Unblinded"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void SELECT_ParentMenu(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "SELECT_ParentMenu", ID: ID);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtParentMenu.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                    txtParentMenuSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                    btnSubmitParentMenu.Visible = false;
                    btnUpdateParentMenu.Visible = true;
                }
                else
                {
                    CLEAR_ParentMenu();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void DELETE_ParentMenu(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_ParentMenu", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}