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
    public partial class NIWRS_WORKFLOW_CLICKs : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMSGBOX.Attributes.Add("MaxLength","200");
            txtEventHistory.Attributes.Add("MaxLength","200");
            try
            {
                if (!IsPostBack)
                {
                    GET_REVIEW_STATUS();
                    GET_CLICKs();
                    GET_STATUS();
                    GET_VISIT();
                    GET_SETFIELDS();
                    GET_OPEN_EMAIL();
                    DISABLE_BUTTONS();
                }

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "CallCkedit", "CallCkedit();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void DISABLE_BUTTONS()
        {
            if (hdnREVIEWSTATUS.Value == "Review")
            {
                btnsubmit.Enabled = false;
                btnsubmit.Text = "Configuration has been Frozen";
                btnsubmit.CssClass = btnsubmit.CssClass.Replace("btn-primary", "btn-danger");
                

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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_CLICKs()
        {
            try
            {
                DataSet dsSource = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "SELECT_STEP", ID: Request.QueryString["ID"].ToString());

                lblList.Text = dsSource.Tables[0].Rows[0]["HEADER"].ToString();

                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_LIST_ADDFIELD", LISTID: dsSource.Tables[0].Rows[0]["SOURCE_ID"].ToString());

                grdClicks.DataSource = ds.Tables[0];
                grdClicks.DataBind();

                ddlField.DataSource = ds.Tables[0];
                ddlField.DataValueField = "ID";
                ddlField.DataTextField = "FIELDNAME";
                ddlField.DataBind();
                ddlField.Items.Insert(0, new ListItem("--Select--", "0"));
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

        private void UPDATE_CLICKs()
        {
            try
            {
                string MSGBOX = null, NAVTO_TYPE = null, NAVTO = null, EVENTHIST = null, SETFIELD = ",",
                    EMAIL_SUBJECT = null, EMAIL_BODY = null, ApplVisit = null, PERFORM = null;
                bool SEND_EMAIL = false;

                ApplVisit = ddlVisit.SelectedValue;
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

                dal_IWRS.NIWRS_WORKFLOW_SP(
                    ACTION: "UPDATE_CLICKs",
                    PERFORM: PERFORM,
                    ApplVisit: ApplVisit,
                    MSGBOX: MSGBOX,
                    SETFIELD: SETFIELD.TrimEnd(','),
                    NAVTO_TYPE: NAVTO_TYPE,
                    NAVTO: NAVTO,
                    EVENTHIST: EVENTHIST,
                    SEND_EMAIL: SEND_EMAIL,
                    EMAIL_SUBJECT: EMAIL_SUBJECT,
                    EMAIL_BODY: EMAIL_BODY,
                    ID: ddlField.SelectedValue,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                INSERT_SETFIELD(ddlField.SelectedValue);

                INSERT_EMAIL(ddlField.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_CLICKs()
        {
            try
            {
                txtMSGBOX.Text = "";
                txtEventHistory.Text = "";
                ddlStatus.SelectedIndex = 0;
                GET_SETFIELDS();
                ddlNavType.SelectedIndex = 0;
                GET_NavSOURCE();
                chkSendEmail.Checked = false;
                txtEmailBody.Text = "";
                txtEmailSubject.Text = "";
                GET_OPEN_EMAIL();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_CLICKs(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "SELECT_LIST_ADDFIELD", ID: ID);

                ddlField.SelectedValue = ds.Tables[0].Rows[0]["ID"].ToString();

                ddlVisit.SelectedValue = ds.Tables[0].Rows[0]["ApplVisit"].ToString();
                ddlPerform.SelectedValue = ds.Tables[0].Rows[0]["PERFORM"].ToString();
                txtMSGBOX.Text = ds.Tables[0].Rows[0]["MSGBOX"].ToString();
                ddlNavType.SelectedValue = ds.Tables[0].Rows[0]["NAVTO_TYPE"].ToString();
                GET_NavSOURCE();
                ddlNavTo.SelectedValue = ds.Tables[0].Rows[0]["NAVTO"].ToString();
                txtEventHistory.Text = ds.Tables[0].Rows[0]["EVENTHIST"].ToString();
                chkSendEmail.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["SEND_EMAIL"]);
                txtEmailSubject.Text = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                txtEmailBody.Text = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();

                if (ds.Tables[0].Rows[0]["SETFIELD"].ToString() != "")
                {
                    DataSet dsSet = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_STEP_FIELD", STEPID: ddlField.SelectedValue);
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

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowEmailDiv();", true);

                GET_FIELD_EMAIL(ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_FIELD_EMAIL(string FIELDID)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "GET_FIELD_EMAIL", STEPID: FIELDID);
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

        private void INSERT_EMAIL(string FIELDID)
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
                                ACTION: "INSERT_FIELD_EMAIL",
                                STEPID: FIELDID,
                                SITEID: lblSiteID.Text,
                                EMAIL_IDS: txtEMAILIDs.Text,
                                CCEMAIL_IDS: txtCCEMAILIDs.Text,
                                BCCEMAIL_IDS: txtBCCEMAILIDs.Text
                                );
                        }
                        else
                        {
                            dal_IWRS.NIWRS_WORKFLOW_SP
                                (
                                ACTION: "DELETE_FIELD_CRIT_EMAIL",
                                STEPID: FIELDID,
                                SITEID: lblSiteID.Text
                                );
                        }
                    }
                }
                else
                {
                    dal_IWRS.NIWRS_WORKFLOW_SP
                        (
                        ACTION: "DELETE_ALL_STEP_CRIT_EMAIL",
                        STEPID: FIELDID
                        );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_CLICKs(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "UPDATE_CLICKs", ENTEREDBY: Session["USER_ID"].ToString(), ID: ID);
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
                    dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "INSERT_SETFIELD_FIELD", STEPID: ddlField.SelectedValue, FIELDNAME: "STATUS", VALUE: ddlStatus.SelectedValue);
                }
                else
                {
                    dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_SETFIELD_FIELD", STEPID: ddlField.SelectedValue, FIELDNAME: "STATUS");
                }

                for (int i = 0; i < repeatSetFields.Items.Count; i++)
                {
                    HiddenField hfCOLNAME = (HiddenField)repeatSetFields.Items[i].FindControl("hfCOLNAME");
                    TextBox txtSetFieldVal = (TextBox)repeatSetFields.Items[i].FindControl("txtSetFieldVal");
                    if (txtSetFieldVal.Text != "")
                    {
                        dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "INSERT_SETFIELD_FIELD", STEPID: ddlField.SelectedValue, FIELDNAME: hfCOLNAME.Value, VALUE: txtSetFieldVal.Text);
                    }
                    else
                    {
                        dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_SETFIELD_FIELD", STEPID: ddlField.SelectedValue, FIELDNAME: hfCOLNAME.Value);
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
                dal_IWRS.NIWRS_WORKFLOW_SP(ACTION: "DELETE_SETFIELD_STEP_ALL", ENTEREDBY: Session["USER_ID"].ToString(), STEPID: ID);
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

        protected void grdClicks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "EditClick")
                {
                    SELECT_CLICKs(id);
                }
                else if (e.CommandName == "DeleteClick")
                {
                    DELETE_CLICKs(id);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('defined Criteria deleted  Successfully.'); ", true);

                    GET_CLICKs();
                }
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
                UPDATE_CLICKs();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Criteria defined Successfully.'); ", true);
                CLEAR_CLICKs();
                GET_CLICKs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_CLICKs();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdClicks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    
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

        protected void grdClicks_PreRender(object sender, EventArgs e)
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
    }
}