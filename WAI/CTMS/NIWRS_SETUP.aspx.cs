using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CTMS
{
    public partial class NIWRS_SETUP : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["SUBJECTTEXT"] != null)
                    {
                        txtSubjectID.Text = Session["SUBJECTTEXT"].ToString();
                    }


                    GET_UNBRPT_TEMPLATE();
                    GET_REVIEW_STATUS();
                    GetSites();
                    GET_STRATA_FIELDS();
                    GET_IWRS_MODULES();
                    GET_VISIT();
                    GET_STATUS();
                    GET_FORM();
                    GET_LISTING();
                    GET_COLS();
                    GET_GRAPH();
                    GET_STATUS_DASHBOARD("0");
                    GET_DASHBOARD();
                    GET_UNBLIND_REASON();
                    GET_DCF_REASON();
                    RandGrd();
                    Dosearm();
                    Getddlvisiblefalse();
                    DISABLE_BUTTONS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        private void GET_UNBRPT_TEMPLATE()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_QUE_ANS", QUECODE: "UNBLINDINGREPORT");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lbtnDownloadtemp.Text = ds.Tables[0].Rows[0]["FileName"].ToString();
                    }
                }
                else
                {
                    lbtnDownloadtemp.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitQues_Click(object sender, EventArgs e)
        {
            try
            {
                Insert_Ques();

                Response.Write("<script>alert('Participant id inserted Successfully')</script>");

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelQues_Click(object sender, EventArgs e)
        {
            Response.Redirect("NIWRS_SETUP.aspx");
        }

        private void Insert_Ques()
        {
            try
            {
                string QUESTION = "", QUECODE = "", ANS = "";

                QUESTION = "Participant id must be.";
                QUECODE = "SUBJECTTEXT";
                ANS = txtSubjectID.Text;

                dal_IWRS.IWRS_SET_OPTION_SP(
                    ACTION: "INSERT_QUES",
                    QUESTION: QUESTION,
                    QUECODE: QUECODE,
                    ANS: ANS,
                    ENTEREDBY: Session["USER_ID"].ToString()

                    );

                Session["SUBJECTTEXT"] = txtSubjectID.Text;
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

        private void Getddlvisiblefalse()
        {
            divddlWindows.Visible = false;
            divddlEarly.Visible = false;
            divddllate.Visible = false;

        }

        private void GetSites()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSites.DataSource = ds.Tables[0];
                    drpSites.DataValueField = "INVID";
                    drpSites.DataBind();
                    drpSites.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //SubSite Section Starts

        private void INSERT_SUBSITE()
        {
            try
            {
                dal_IWRS.IWRS_SET_SUBSITE_SP
                    (
                    ACTION: "INSERT_SUBSITE",
                    SITEID: drpSites.SelectedValue,
                    SUBSITEID: txtSubSiteID.Text,
                    SUBSITENAME: txtSubSiteName.Text
                    );

                Response.Write("<script>alert('Sub Site Added Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_SUBSITE()
        {
            try
            {
                dal_IWRS.IWRS_SET_SUBSITE_SP
                    (
                    ACTION: "UPDATE_SUBSITE",
                    ID: ViewState["editSUBSITEID"].ToString(),
                    SITEID: drpSites.SelectedValue,
                    SUBSITEID: txtSubSiteID.Text,
                    SUBSITENAME: txtSubSiteName.Text,
                    USER_ID: Session["USER_ID"].ToString()

                    );

                Response.Write("<script>alert('Sub Site Updated Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_SUBSITE()
        {
            try
            {
                txtSubSiteID.Text = "";
                txtSubSiteName.Text = "";
                btnSubmitSubSite.Visible = true;
                btnUpdateSubSite.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_SUBSITE(string ID)
        {
            try
            {
                dal_IWRS.IWRS_SET_SUBSITE_SP
                    (
                    ACTION: "DELETE_SUBSITE",
                    ID: ID
                    );
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Sub-Site deleted successfully.'); ", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_SUBSITE(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP
                            (
                            ACTION: "SELECT_SUBSITE",
                            ID: ID
                            );

                txtSubSiteID.Text = ds.Tables[0].Rows[0]["SUBSITEID"].ToString();
                txtSubSiteName.Text = ds.Tables[0].Rows[0]["SubSiteName"].ToString();

                btnSubmitSubSite.Visible = false;
                btnUpdateSubSite.Visible = true;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBSITE()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_SUBSITE_SP
                            (
                            ACTION: "GET_SUBSITE",
                            SITEID: drpSites.SelectedValue
                            );
                grdSubSites.DataSource = ds;
                grdSubSites.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBSITE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSubSites_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();

                ViewState["editSUBSITEID"] = id;

                if (e.CommandName == "EditSubSite")
                {
                    SELECT_SUBSITE(id);
                }
                else if (e.CommandName == "DeleteSubSite")
                {
                    DELETE_SUBSITE(id);
                    GET_SUBSITE();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitSubSite_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_SUBSITE();
                GET_SUBSITE();
                CLEAR_SUBSITE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateSubSite_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_SUBSITE();
                GET_SUBSITE();
                CLEAR_SUBSITE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelSubSite_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_SUBSITE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //SubSite Section End





        //Visit Section Starts

        protected void drpIndication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_VISIT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_VISIT()
        {
            try
            {
                string WINDOW = null, EARLY = null, LATE = null;
                if (ddlVisModule.SelectedValue == "-1")
                {
                    WINDOW = ddlWinPeroids.SelectedValue;
                    EARLY = EarlyWinPeroid.SelectedValue;
                    LATE = LateWinPeroid.SelectedValue;
                }
                else
                {
                    WINDOW = txtWindow.Text;
                    EARLY = txtEarly.Text;
                    LATE = txtLate.Text;
                }

                dal_IWRS.IWRS_SET_VISIT_SP
                    (
                    ACTION: "INSERT_VISIT",
                    VISITNUM: txtVisitSEQNO.Text,
                    VISIT: txtVisitName.Text,
                    WINDOW: WINDOW,
                    EARLY: EARLY,
                    LATE: LATE,
                    MODULEID: ddlVisModule.SelectedValue,
                    FIELDID: ddlVisField.SelectedValue,
                    DOSING: chkDosing.Checked,
                    ENTEREDBY: Session["USER_ID"].ToString(),
                    VisitSummarySeq: txtVisitSummarySeq.Text,
                    Applicable_For_VisitSummary: chkVisitSummary.Checked
                    );

                Response.Write("<script>alert('Visit Defined Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_VISIT()
        {
            try
            {
                string WINDOW = null, EARLY = null, LATE = null;
                if (ddlVisModule.SelectedValue == "-1")
                {
                    WINDOW = ddlWinPeroids.SelectedValue;
                    EARLY = EarlyWinPeroid.SelectedValue;
                    LATE = LateWinPeroid.SelectedValue;
                }
                else
                {
                    WINDOW = txtWindow.Text;
                    EARLY = txtEarly.Text;
                    LATE = txtLate.Text;
                }

                dal_IWRS.IWRS_SET_VISIT_SP
                    (
                    ACTION: "UPDATE_VISIT",
                    ID: ViewState["editVISITID"].ToString(),
                    VISITNUM: txtVisitSEQNO.Text,
                    MODULEID: ddlVisModule.SelectedValue,
                    FIELDID: ddlVisField.SelectedValue,
                    VISIT: txtVisitName.Text,
                    WINDOW: WINDOW,
                    EARLY: EARLY,
                    LATE: LATE,
                    DOSING: chkDosing.Checked,
                    ENTEREDBY: Session["USER_ID"].ToString(),
                    VisitSummarySeq: txtVisitSummarySeq.Text,
                    Applicable_For_VisitSummary: chkVisitSummary.Checked
                    );

                Response.Write("<script>alert('Defined Visit Updated Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_VISIT()
        {
            try
            {
                txtVisitSEQNO.Text = "";
                txtVisitName.Text = "";
                txtWindow.Text = "";
                txtEarly.Text = "";
                txtLate.Text = "";
                ddlVisModule.SelectedIndex = 0;
                ddlWinPeroids.SelectedIndex = 0;
                EarlyWinPeroid.SelectedIndex = 0;
                LateWinPeroid.SelectedIndex = 0;
                ddlVisField.ClearSelection();
                chkDosing.Checked = false;
                ddlVisField.Visible = false;
                btnsubmitVisit.Visible = true;
                btnupdateVisit.Visible = false;
                txtVisitSummarySeq.Text = "";
                chkVisitSummary.Checked = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_VISIT(string ID)
        {
            try
            {
                dal_IWRS.IWRS_SET_VISIT_SP
                    (
                    ACTION: "DELETE_VISIT",
                    ID: ID
                    );
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Visit deleted successfully.'); ", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_VISIT(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_VISIT_SP
                            (
                            ACTION: "SELECT_VISIT",
                            ID: ID
                            );
                txtVisitSEQNO.Text = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                txtVisitName.Text = ds.Tables[0].Rows[0]["VISIT"].ToString();
                ddlVisModule.SelectedValue = ds.Tables[0].Rows[0]["MODULE"].ToString();

                GET_VISFIELDS();
                if (ddlVisModule.SelectedIndex != 0)
                {
                    ddlVisField.SelectedValue = ds.Tables[0].Rows[0]["FIELD"].ToString();
                }
                if (ddlVisModule.SelectedValue == "-1")
                {
                    ddlWinPeroids.SelectedValue = ds.Tables[0].Rows[0]["WINDOW"].ToString();
                    EarlyWinPeroid.SelectedValue = ds.Tables[0].Rows[0]["EARLY"].ToString();
                    LateWinPeroid.SelectedValue = ds.Tables[0].Rows[0]["LATE"].ToString();
                }
                else
                {
                    txtWindow.Text = ds.Tables[0].Rows[0]["WINDOW"].ToString();
                    txtEarly.Text = ds.Tables[0].Rows[0]["EARLY"].ToString();
                    txtLate.Text = ds.Tables[0].Rows[0]["LATE"].ToString();

                }
                if (ds.Tables[0].Rows[0]["DOSING"].ToString() == "True")
                {
                    chkDosing.Checked = true;
                }
                else
                {
                    chkDosing.Checked = false;
                }

                txtVisitSummarySeq.Text = ds.Tables[0].Rows[0]["VisitSummarySEQ"].ToString();

                if (ds.Tables[0].Rows[0]["Applicable_For_VisitSummary"].ToString() == "True")
                {
                    chkVisitSummary.Checked = true;
                }
                else
                {
                    chkVisitSummary.Checked = false;
                }

                btnsubmitVisit.Visible = false;
                btnupdateVisit.Visible = true;
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
                btnSubmitSubSite.Enabled = false;
                btnUpdateSubSite.Enabled = false;
                btnSubmitSubSite.Text = "Configuration has been Frozen";
                btnUpdateSubSite.Text = "Configuration has been Frozen";
                btnSubmitSubSite.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdateSubSite.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnsubmitVisit.Enabled = false;
                btnupdateVisit.Enabled = false;
                btnsubmitVisit.Text = "Configuration has been Frozen";
                btnupdateVisit.Text = "Configuration has been Frozen";
                btnsubmitVisit.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnupdateVisit.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnsubmitStatus.Enabled = false;
                btnupdateStatus.Enabled = false;
                btnsubmitStatus.Text = "Configuration has been Frozen";
                btnupdateStatus.Text = "Configuration has been Frozen";
                btnsubmitStatus.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnupdateStatus.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnSubmitForm.Enabled = false;
                btnUpdateForm.Enabled = false;
                btnSubmitForm.Text = "Configuration has been Frozen";
                btnUpdateForm.Text = "Configuration has been Frozen";
                btnSubmitForm.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdateForm.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnSubmitCol.Enabled = false;
                btnUpdateCol.Enabled = false;
                btnSubmitCol.Text = "Configuration has been Frozen";
                btnUpdateCol.Text = "Configuration has been Frozen";
                btnSubmitCol.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdateCol.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnSubmitList.Enabled = false;
                btnUpdateList.Enabled = false;
                btnSubmitList.Text = "Configuration has been Frozen";
                btnUpdateList.Text = "Configuration has been Frozen";
                btnSubmitList.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdateList.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnSubmitSTRATA.Enabled = false;
                btnSubmitSTRATA.Text = "Configuration has been Frozen";
                btnSubmitSTRATA.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnSubmitGraph.Enabled = false;
                btnUpdateGraph.Enabled = false;
                btnSubmitGraph.Text = "Configuration has been Frozen";
                btnUpdateGraph.Text = "Configuration has been Frozen";
                btnSubmitGraph.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdateGraph.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnSubmit_Dashborad.Enabled = false;
                btnUpdate_Dashborad.Enabled = false;
                btnSubmit_Dashborad.Text = "Configuration has been Frozen";
                btnUpdate_Dashborad.Text = "Configuration has been Frozen";
                btnSubmit_Dashborad.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdate_Dashborad.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnSubmitUnblindReason.Enabled = false;
                btnUpdateUnblindReason.Enabled = false;
                btnSubmitUnblindReason.Text = "Configuration has been Frozen";
                btnUpdateUnblindReason.Text = "Configuration has been Frozen";
                btnSubmitUnblindReason.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdateUnblindReason.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnSubmitDCFReason.Enabled = false;
                btnUpdateDCFReason.Enabled = false;
                btnSubmitDCFReason.Text = "Configuration has been Frozen";
                btnUpdateDCFReason.Text = "Configuration has been Frozen";
                btnSubmitDCFReason.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdateDCFReason.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnSubmitRandomizationArm.Enabled = false;
                btnUpdateRandomizationArm.Enabled = false;
                btnSubmitRandomizationArm.Text = "Configuration has been Frozen";
                btnUpdateRandomizationArm.Text = "Configuration has been Frozen";
                btnSubmitRandomizationArm.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdateRandomizationArm.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");

                btnSubmitDosingArm.Enabled = false;
                btnUpdateDosingArm.Enabled = false;
                btnSubmitDosingArm.Text = "Configuration has been Frozen";
                btnUpdateDosingArm.Text = "Configuration has been Frozen";
                btnSubmitDosingArm.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdateDosingArm.CssClass = btnSubmitSubSite.CssClass.Replace("btn-primary", "btn-danger");
            }
        }

        private void GET_VISIT()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_VISIT_SP(ACTION: "GET_VISIT");
                grdVisit.DataSource = ds;
                grdVisit.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_VISFIELDS()
        {
            try
            {
                if (ddlVisModule.SelectedIndex != 0 && ddlVisModule.SelectedIndex != 1)
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_VIS_FIELDS", MODULEID: ddlVisModule.SelectedValue);

                    ddlVisField.Items.Clear();
                    ddlVisField.DataSource = ds;
                    ddlVisField.DataValueField = "ID";
                    ddlVisField.DataTextField = "FIELDNAME";
                    ddlVisField.DataBind();

                    ddlVisField.Visible = true;
                    divtxtWindow.Visible = true;
                    divtxtearlyWindow.Visible = true;
                    divtxtlateWindow.Visible = true;
                    divddlWindows.Visible = false;
                    divddlEarly.Visible = false;
                    divddllate.Visible = false;

                }
                else if (ddlVisModule.SelectedIndex == 0)
                {
                    ddlVisField.Visible = false;
                    divddlWindows.Visible = false;
                    divddlEarly.Visible = false;
                    divddllate.Visible = false;
                    divtxtWindow.Visible = true;
                    divtxtearlyWindow.Visible = true;
                    divtxtlateWindow.Visible = true;
                    ddlVisField.Items.Clear();

                }
                else if (ddlVisModule.SelectedIndex == 1)
                {
                    ddlVisField.Visible = false;
                    divtxtWindow.Visible = false;
                    divtxtearlyWindow.Visible = false;
                    divtxtlateWindow.Visible = false;
                    divddlWindows.Visible = true;
                    divddlEarly.Visible = true;
                    divddllate.Visible = true;
                    ddlVisField.Items.Clear();
                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdVisit_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();


                string id = e.CommandArgument.ToString();

                ViewState["editVISITID"] = id;
                if (e.CommandName == "EditVisit")
                {
                    SELECT_VISIT(id);
                }
                else if (e.CommandName == "DeleteVisit")
                {
                    DELETE_VISIT(id);
                    GET_VISIT();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitVisit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_VISIT();
                GET_VISIT();
                CLEAR_VISIT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateVisit_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_VISIT();
                GET_VISIT();
                CLEAR_VISIT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelnVisit_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_VISIT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_VISFIELDS();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Visit Section End




        //Status Section Starts

        private void INSERT_Status()
        {
            try
            {
                dal_IWRS.IWRS_SET_STATUS_SP
                    (
                    ACTION: "INSERT_STATUS",
                    STATUSCODE: txtStatusCode.Text,
                    STATUSNAME: txtStatusName.Text,
                    IWRS_Graph: chkIWRSGraph.Checked,
                    IWRS_Tile: chkIWRSTile.Checked,
                    CONDITION1: ddlStatusCondition.SelectedValue,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );
                Response.Write("<script> alert('Status Defined Successfully.')</script>");

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_Status()
        {
            try
            {
                dal_IWRS.IWRS_SET_STATUS_SP
                    (
                    ACTION: "UPDATE_STATUS",
                    ID: ViewState["editStatusID"].ToString(),
                    STATUSCODE: txtStatusCode.Text,
                    STATUSNAME: txtStatusName.Text,
                    IWRS_Graph: chkIWRSGraph.Checked,
                    IWRS_Tile: chkIWRSTile.Checked,
                    CONDITION1: ddlStatusCondition.SelectedValue,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                Response.Write("<script> alert('Defined Status Updated Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_STATUS()
        {
            try
            {
                txtStatusCode.Text = "";
                txtStatusName.Text = "";
                chkIWRSGraph.Checked = false;
                chkIWRSTile.Checked = false;
                ddlStatusCondition.SelectedIndex = 0;
                ddlStatusCondition.CssClass = ddlStatusCondition.CssClass + " disp-none";
                btnsubmitStatus.Visible = true;
                btnupdateStatus.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_STATUS(string ID)
        {
            try
            {
                dal_IWRS.IWRS_SET_STATUS_SP
                    (
                    ACTION: "DELETE_STATUS",
                    ID: ID
                    );

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Status deleted Successfully.'); ", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_STATUS(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_STATUS_SP
                            (
                            ACTION: "SELECT_STATUS",
                            ID: ID
                            );

                txtStatusCode.Text = ds.Tables[0].Rows[0]["STATUSCODE"].ToString();
                txtStatusName.Text = ds.Tables[0].Rows[0]["STATUSNAME"].ToString();
                if (ds.Tables[0].Rows[0]["IWRS_Graph"].ToString() == "True")
                {
                    chkIWRSGraph.Checked = true;
                }
                else
                {
                    chkIWRSGraph.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["IWRS_Tile"].ToString() == "True")
                {
                    chkIWRSTile.Checked = true;
                }
                else
                {
                    chkIWRSTile.Checked = false;
                }

                ddlStatusCondition.SelectedValue = ds.Tables[0].Rows[0]["CONDITION"].ToString();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showStatusCondition", "showStatusCondition(); ", true);


                btnsubmitStatus.Visible = false;
                btnupdateStatus.Visible = true;
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
                DataSet ds = dal_IWRS.IWRS_SET_STATUS_SP(ACTION: "GET_STATUS");
                grdStatus.DataSource = ds;
                grdStatus.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();

                ViewState["editStatusID"] = id;
                if (e.CommandName == "EditStatus")
                {
                    SELECT_STATUS(id);
                }
                else if (e.CommandName == "DeleteStatus")
                {
                    DELETE_STATUS(id);
                    GET_STATUS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitStatus_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_Status();
                GET_STATUS();
                CLEAR_STATUS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdateStatus_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_Status();
                GET_STATUS();
                CLEAR_STATUS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancelnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_STATUS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Status Section End

        public void GET_IWRS_MODULES()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_IWRS_MODULES");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.DataSource = ds.Tables[0];
                    ddlModule.DataValueField = "ID";
                    ddlModule.DataTextField = "MODULENAME";
                    ddlModule.DataBind();
                    ddlModule.Items.Insert(0, new ListItem("--Select--", "0"));

                    ddlVisModule.DataSource = ds.Tables[0];
                    ddlVisModule.DataValueField = "ID";
                    ddlVisModule.DataTextField = "MODULENAME";
                    ddlVisModule.DataBind();
                    ddlVisModule.Items.Insert(0, new ListItem("None", "0"));
                    ddlVisModule.Items.Insert(1, new ListItem("MULTIPLE DEPENDENCY", "-1"));

                    lstModule.Items.Clear();
                    lstModule.DataSource = ds;
                    lstModule.DataValueField = "ID";
                    lstModule.DataTextField = "MODULENAME";
                    lstModule.DataBind();
                    lstModule.Items.Insert(0, new ListItem("Patient Details", "0"));
                }
                else
                {
                    ddlVisModule.Items.Insert(0, new ListItem("None", "0"));
                    ddlVisModule.Items.Insert(1, new ListItem("MULTIPLE DEPENDENCY", "-1"));

                    lstModule.Items.Insert(0, new ListItem("Patient Details", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MODULE_FIELDS_STRATA()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_MODULE_FIELDS_STRATA", MODULEID: ddlModule.SelectedValue);
                lstFIELDS.Items.Clear();
                lstFIELDS.DataSource = ds;
                lstFIELDS.DataValueField = "ID";
                lstFIELDS.DataTextField = "FIELDNAME";
                lstFIELDS.DataBind();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Cnt"].ToString() != "0")
                    {
                        ListItem itm = lstFIELDS.Items.FindByValue(dr["ID"].ToString());
                        if (itm != null)
                            itm.Selected = true;
                        else
                            itm.Selected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Form Section Starts

        private void INSERT_FORM()
        {
            try
            {
                dal_IWRS.IWRS_SET_FORM_SP
                    (
                    ACTION: "INSERT_FORM",
                    FormName: txtFormName.Text,
                    MODULEID: ddlModule.SelectedValue,
                    MODULENAME: ddlModule.SelectedItem.Text,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );
                Response.Write("<script> alert('Forms Defined Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_FORM()
        {
            try
            {
                dal_IWRS.IWRS_SET_FORM_SP
                    (
                    ACTION: "UPDATE_FORM",
                    ID: ViewState["editFormID"].ToString(),
                    FormName: txtFormName.Text,
                    MODULEID: ddlModule.SelectedValue,
                    MODULENAME: ddlModule.SelectedItem.Text,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                Response.Write("<script> alert('Defined Forms Updated Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_FORM()
        {
            try
            {
                txtFormName.Text = "";
                ddlModule.SelectedIndex = 0;
                GET_MODULE_FIELDS_STRATA();
                btnSubmitForm.Visible = true;
                btnUpdateForm.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_FORM(string ID)
        {
            try
            {
                dal_IWRS.IWRS_SET_FORM_SP
                    (
                    ACTION: "DELETE_FORM",
                    ID: ID
                    );
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Forms Deleted Successfully.'); ", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_FORM(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP
                            (
                            ACTION: "SELECT_FORM",
                            ID: ID
                            );

                txtFormName.Text = ds.Tables[0].Rows[0]["FORMNAME"].ToString();
                ddlModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();
                GET_MODULE_FIELDS_STRATA();

                btnSubmitForm.Visible = false;
                btnUpdateForm.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_FORM()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP
                            (
                            ACTION: "GET_FORM"
                            );
                grdForm.DataSource = ds;
                grdForm.DataBind();

                ddlGraphForm.DataSource = ds.Tables[0];
                ddlGraphForm.DataValueField = "ID";
                ddlGraphForm.DataTextField = "FORMNAME";
                ddlGraphForm.DataBind();
                ddlGraphForm.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_STRATA_FIELDS()
        {
            try
            {
                foreach (ListItem item in lstFIELDS.Items)
                {
                    if (item.Selected == true)
                    {
                        dal_IWRS.IWRS_SET_FORM_SP(ACTION: "INSERT_STRATA_FIELDS", FIELDID: item.Value.ToString());

                        Response.Write("<script> alert('Strata Defined Successfully.')</script>");
                    }
                    else
                    {
                        dal_IWRS.IWRS_FORM_SP(ACTION: "DELETE_STRATA_FIELDS", FIELDID: item.Value.ToString());


                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdForm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editFormID"] = id;
                if (e.CommandName == "EditForm")
                {
                    SELECT_FORM(id);
                }
                else if (e.CommandName == "DeleteForm")
                {
                    DELETE_FORM(id);
                    GET_FORM();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitForm_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_FORM();
                INSERT_STRATA_FIELDS();
                GET_STRATA_FIELDS();
                GET_FORM();
                CLEAR_FORM();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateForm_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_FORM();
                INSERT_STRATA_FIELDS();
                GET_STRATA_FIELDS();
                GET_FORM();
                CLEAR_FORM();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelForm_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_FORM();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_MODULE_FIELDS_STRATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Form Section Ends


        //Listing Section Starts

        private void INSERT_LISTING()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_LIST_SP(ACTION: "INSERT_LISTING", LISTNAME: txtListName.Text);

                INSERT_LISTING_DETAILS(ds.Tables[0].Rows[0][0].ToString());

                Response.Write("<script> alert('Listing Defined Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_LISTING_DETAILS(string ID)
        {
            try
            {
                for (int j = 0; j < grd_Field.Rows.Count; j++)
                {
                    CheckBox chkListing = (CheckBox)grd_Field.Rows[j].FindControl("chkListing");
                    string FIELDID = ((Label)grd_Field.Rows[j].FindControl("lblID")).Text;
                    string txtSEQNO = ((TextBox)grd_Field.Rows[j].FindControl("txtSEQNO")).Text;
                    string MODULEID = ((Label)grd_Field.Rows[j].FindControl("lblMODULEID")).Text;
                    string FIELDNAME = ((Label)grd_Field.Rows[j].FindControl("lblField")).Text;
                    if (chkListing.Checked == true && txtSEQNO != "")
                    {
                        DataSet ds = dal_IWRS.IWRS_SET_LIST_SP(
                            ACTION: "INSERT_LISTING_DETAILS",
                            LISTID: ID,
                            SEQNO: txtSEQNO,
                            FIELDID: FIELDID,
                            MODULEID: MODULEID,
                            FIELDNAME: FIELDNAME,
                            ENTEREDBY: Session["USER_ID"].ToString()
                            );
                    }
                    else
                    {
                        DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "DELETE_LISTING_DETAILS_BYFIELID", LISTID: ID, FIELDID: FIELDID, MODULEID: MODULEID, ENTEREDBY: Session["USER_ID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_LISTING()
        {
            try
            {
                dal_IWRS.IWRS_SET_LIST_SP(
                    ACTION: "UPDATE_LISTING",
                    LISTNAME: txtListName.Text,
                    LISTID: ViewState["editLISTID"].ToString(),
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                INSERT_LISTING_DETAILS(ViewState["editLISTID"].ToString());

                Response.Write("<script> alert('Defined Listing Updated Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_LISTING(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_SETUP_SP(ACTION: "DELETE_LISTING", ENTEREDBY: Session["USER_ID"].ToString(), ID: ID);

                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MODULE_FIELDS()
        {
            try
            {
                string MODULEIDS = null;

                foreach (ListItem item in lstModule.Items)
                {
                    if (item.Selected == true)
                    {
                        if (MODULEIDS != null)
                        {
                            MODULEIDS += "," + item.Value.ToString();
                        }
                        else
                        {
                            MODULEIDS += item.Value.ToString();
                        }
                    }
                    else
                    {
                        if (btnUpdateList.Visible == true)
                        {
                            dal_IWRS.NIWRS_SETUP_SP(ACTION: "DELETE_LISTING_DETAILS_BYMODULEID", LISTID: ViewState["editLISTID"].ToString(), MODULEID: item.Value.ToString());
                        }
                    }
                }

                DataSet ds = new DataSet();

                if (btnUpdateList.Visible == true)
                {
                    ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_MODULE_FIELDS", MODULENAME: MODULEIDS, LISTID: ViewState["editLISTID"].ToString());
                }
                else
                {
                    ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_MODULE_FIELDS", MODULENAME: MODULEIDS);
                }

                grd_Field.DataSource = ds;
                grd_Field.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_LISTING(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "SELECT_LISTING", ID: ID);

                txtListName.Text = ds.Tables[0].Rows[0]["LISTNAME"].ToString();

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    if (dr["MODULEID"] != "")
                    {
                        ListItem itm = lstModule.Items.FindByValue(dr["MODULEID"].ToString());
                        if (itm != null)
                        {
                            itm.Selected = true;
                        }
                    }
                }

                grd_Field.DataSource = ds.Tables[2];
                grd_Field.DataBind();

                btnUpdateList.Visible = true;
                btnSubmitList.Visible = false;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_LISTING()
        {
            try
            {
                txtListName.Text = "";
                GET_IWRS_MODULES();

                btnSubmitList.Visible = true;
                btnUpdateList.Visible = false;

                GET_MODULE_FIELDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LISTING()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_LISTING");
                grdList.DataSource = ds;
                grdList.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_MODULE_FIELDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Field_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    CheckBox chkListing = (CheckBox)e.Row.FindControl("chkListing");
                    string txtSEQNO = ((TextBox)e.Row.FindControl("txtSEQNO")).Text;
                    if (txtSEQNO != "")
                    {
                        chkListing.Checked = true;
                    }
                    else
                    {
                        chkListing.Checked = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitList_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_LISTING();
                CLEAR_LISTING();
                GET_LISTING();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateList_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_LISTING();
                CLEAR_LISTING();
                GET_LISTING();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelList_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_LISTING();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editLISTID"] = id;

                if (e.CommandName == "EditList")
                {
                    SELECT_LISTING(id);
                }
                else if (e.CommandName == "DeleteList")
                {
                    DELETE_LISTING(id);
                    GET_LISTING();

                    //Response.Write("<script> alert('Listing deleted successfully ')</script>");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Listing deleted successfully.');  window.location.href = 'NIWRS_SETUP.aspx' ", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string ID = dr["ID"].ToString();

                    GridView grdListField = (GridView)e.Row.FindControl("grdListField");
                    DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_LISTING_DETAILS", LISTID: ID);
                    grdListField.DataSource = ds.Tables[0];
                    grdListField.DataBind();

                    LinkButton lbtndeleteSection = (e.Row.FindControl("lbtndeleteSection") as LinkButton);

                    string COUNT = dr["COUNT"].ToString();

                    if (COUNT != "0")
                    {
                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndeleteSection.Enabled = false;
                        lbtndeleteSection.ToolTip = "Configuration has been Frozen";
                        lbtndeleteSection.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Listing Section Ends



        //Columns Section Starts

        private void INSERT_COLS()
        {
            try
            {
                dal_IWRS.IWRS_SET_COLS_SP
                    (
                    ACTION: "INSERT_COLS",
                    COL_NAME: txtColName.Text,
                    FIELDNAME: txtFieldName.Text,
                    IWRS_Graph: chkColGraph.Checked,
                    IWRS_Tile: chkColTile.Checked,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                Response.Write("<script> alert('Additional Fields Defined Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_COLS()
        {
            try
            {
                dal_IWRS.IWRS_SET_COLS_SP
                    (
                    ACTION: "UPDATE_COLS",
                    ID: ViewState["editColsID"].ToString(),
                    COL_NAME: txtColName.Text,
                    FIELDNAME: txtFieldName.Text,
                    IWRS_Graph: chkColGraph.Checked,
                    IWRS_Tile: chkColTile.Checked,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                Response.Write("<script> alert('Defined Additional Fields Updated Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_COLS()
        {
            try
            {
                txtColName.Text = "";
                txtFieldName.Text = "";


                chkColGraph.Checked = false;
                chkColTile.Checked = false;
                btnSubmitCol.Visible = true;
                btnUpdateCol.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_COLS(string ID)
        {
            try
            {
                dal_IWRS.IWRS_SET_COLS_SP
                    (
                    ACTION: "DELETE_COLS",
                    ID: ID
                    );
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Additional Fields Deleted Successfully.'); ", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_COLS(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_COLS_SP
                            (
                            ACTION: "SELECT_COLS",
                            ID: ID
                            );

                txtFieldName.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                txtColName.Text = ds.Tables[0].Rows[0]["COL_NAME"].ToString();

                if (ds.Tables[0].Rows[0]["IWRS_Graph"].ToString() == "True")
                {
                    chkColGraph.Checked = true;
                }
                else
                {
                    chkColGraph.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["IWRS_Tile"].ToString() == "True")
                {
                    chkColTile.Checked = true;
                }
                else
                {
                    chkColTile.Checked = false;
                }

                btnSubmitCol.Visible = false;
                btnUpdateCol.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_COLS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_COLS_SP
                            (
                            ACTION: "GET_COLS"
                            );
                grdCols.DataSource = ds;
                grdCols.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitCol_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_COLS();
                CLEAR_COLS();
                GET_COLS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateCol_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_COLS();
                CLEAR_COLS();
                GET_COLS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelCol_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_COLS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdCols_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editColsID"] = id;

                if (e.CommandName == "EditCol")
                {
                    SELECT_COLS(id);
                }
                else if (e.CommandName == "DeleteCol")
                {
                    DELETE_COLS(id);
                    GET_COLS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Columns Section Ends

        //Strata Section Starts

        private void GET_STRATA_FIELDS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_STRATA_FIELDS");
                grdStrataFields.DataSource = ds;
                grdStrataFields.DataBind();

                if (grdStrataFields.Rows.Count > 0)
                {
                    divStrata.Visible = true;
                    btnSubmitSTRATA.Visible = true;
                    btnCancelSTRATA.Visible = true;
                }
                else
                {
                    divStrata.Visible = false;
                    btnSubmitSTRATA.Visible = false;
                    btnCancelSTRATA.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_STRATA_FIELDS_ANS()
        {
            try
            {
                for (int a = 0; a < grdStrataFields.Rows.Count; a++)
                {
                    GridView grdANS_STRATA = grdStrataFields.Rows[a].FindControl("grdANS_STRATA") as GridView;
                    Label lblFIELDID = grdStrataFields.Rows[a].FindControl("lblFIELDID") as Label;
                    Label lblVARIABLENAME = grdStrataFields.Rows[a].FindControl("lblVARIABLENAME") as Label;

                    for (int b = 0; b < grdANS_STRATA.Rows.Count; b++)
                    {
                        Label lblANS = grdANS_STRATA.Rows[b].FindControl("lblANS") as Label;
                        TextBox txtSTRATA = grdANS_STRATA.Rows[b].FindControl("txtSTRATA") as TextBox;

                        if (txtSTRATA.Text != "")
                        {
                            dal_IWRS.IWRS_SET_FORM_SP(
                            ACTION: "INSERT_STRATA_FIELDS_ANS",
                            FIELDID: lblFIELDID.Text,
                            ANSWER: lblANS.Text,
                            STRATA: txtSTRATA.Text,
                            VARIABLENAME: lblVARIABLENAME.Text,
                            ENTEREDBY: Session["USER_ID"].ToString()
                            );
                        }
                        else
                        {
                            dal_IWRS.IWRS_FORM_SP(
                            ACTION: "DELETE_STRATA_FIELDS_ANS",
                            FIELDID: lblFIELDID.Text,
                            ANSWER: lblANS.Text,
                            STRATA: txtSTRATA.Text,
                            VARIABLENAME: lblVARIABLENAME.Text,
                            ENTEREDBY: Session["USER_ID"].ToString()
                            );
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitSTRATA_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_STRATA_FIELDS_ANS();
                GET_STRATA_FIELDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelSTRATA_Click(object sender, EventArgs e)
        {
            try
            {
                GET_STRATA_FIELDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdStrataFields_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();

                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_STRATA_FIELDS_ANS", VARIABLENAME: VARIABLENAME);
                    GridView grdANS_STRATA = (GridView)e.Row.FindControl("grdANS_STRATA");

                    grdANS_STRATA.DataSource = ds;
                    grdANS_STRATA.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        //Define Graphs Starts

        private void CLEAR_GRAPHS()
        {
            try
            {
                txtGraphHeader.Text = "";
                ddlGraphForm.SelectedIndex = 0;
                ddlGraphField.Items.Clear();
                chkBar.Checked = false;
                chkPie.Checked = false;

                btnSubmitGraph.Visible = true;
                btnUpdateGraph.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_GRAPH()
        {
            try
            {
                dal_IWRS.IWRS_SET_DB_SP(
                ACTION: "INSERT_GRAPH",
                FormName: ddlGraphForm.SelectedValue,
                VARIABLENAME: ddlGraphField.SelectedValue,
                FIELDNAME: ddlGraphField.SelectedItem.Text,
                IWRS_Graph: chkBar.Checked,
                IWRS_Tile: chkPie.Checked,
                ENTEREDBY: Session["USER_ID"].ToString(),
                MODULENAME: txtGraphHeader.Text
                    );

                Response.Write("<script> alert('Graph Defined Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_GRAPH()
        {
            try
            {
                dal_IWRS.IWRS_SET_DB_SP(
                ACTION: "UPDATE_GRAPH",
                ID: ViewState["editGRAPHID"].ToString(),
                FormName: ddlGraphForm.SelectedValue,
                VARIABLENAME: ddlGraphField.SelectedValue,
                FIELDNAME: ddlGraphField.SelectedItem.Text,
                IWRS_Graph: chkBar.Checked,
                IWRS_Tile: chkPie.Checked,
                ENTEREDBY: Session["USER_ID"].ToString(),
                MODULENAME: txtGraphHeader.Text
                    );

                Response.Write("<script> alert('Defined Graph Updated Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_GRAPH(string Id)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "SELECT_GRAPH", ID: Id);

                ddlGraphForm.SelectedValue = ds.Tables[0].Rows[0]["FORM"].ToString();
                GET_FORM_FIELDS();
                ddlGraphField.SelectedValue = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();
                txtGraphHeader.Text = ds.Tables[0].Rows[0]["HEADER"].ToString();

                if (ds.Tables[0].Rows[0]["BAR"].ToString() == "True")
                {
                    chkBar.Checked = true;
                }
                else
                {
                    chkBar.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["PIE"].ToString() == "True")
                {
                    chkPie.Checked = true;
                }
                else
                {
                    chkPie.Checked = false;
                }

                btnSubmitGraph.Visible = false;
                btnUpdateGraph.Visible = true;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_GRAPH(string Id)
        {
            try
            {
                dal_IWRS.IWRS_SET_DB_SP(ACTION: "DELETE_GRAPH", ENTEREDBY: Session["USER_ID"].ToString(), ID: Id);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Graphs Deleted Successfully.'); ", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_GRAPH()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_GRAPH");
                grdGraphs.DataSource = ds;
                grdGraphs.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_FORM_FIELDS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_FORM_SPEC", ID: ddlGraphForm.SelectedValue);

                ddlGraphField.DataSource = ds;
                ddlGraphField.DataValueField = "VARIABLENAME";
                ddlGraphField.DataTextField = "FIELDNAME";
                ddlGraphField.DataBind();
                ddlGraphField.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdGraphs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editGRAPHID"] = id;

                if (e.CommandName == "EditGraph")
                {
                    SELECT_GRAPH(id);
                }
                else if (e.CommandName == "DeleteGraph")
                {
                    DELETE_GRAPH(id);
                    GET_GRAPH();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlGraphForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_FORM_FIELDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitGraph_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_GRAPH();
                GET_GRAPH();
                CLEAR_GRAPHS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateGraph_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_GRAPH();
                GET_GRAPH();
                CLEAR_GRAPHS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelGraph_Click(object sender, EventArgs e)
        {
            try
            {
                GET_GRAPH();
                CLEAR_GRAPHS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Define Graphs Ends



        //Define Dashboard Starts

        private void GET_STATUS_DASHBOARD(string DASH_ID)
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_STATUS_DASHBOARD", ID: DASH_ID);
                gvDashboardStatus.DataSource = ds;
                gvDashboardStatus.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_DASHBOARD()
        {
            try
            {
                string SEQNO, Header;
                bool GRAPH, TILE, ENROLL;

                SEQNO = txtDashboardSeqNo.Text;
                Header = txtDashboardHeader.Text;
                GRAPH = chkDashboardStatusGraph.Checked;
                TILE = chkDashboardStatusTile.Checked;
                ENROLL = chkDashboardStatusEnroll.Checked;

                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(
                            ACTION: "INSERT_DASHBOARD",
                            SEQNO: SEQNO,
                            FormHeader: Header,
                            IWRS_Graph: GRAPH,
                            IWRS_Tile: TILE,
                            DOSING: ENROLL,
                            ENTEREDBY: Session["USER_ID"].ToString()
                                );

                INSERT_DASHBOARD_ITEM(ds.Tables[0].Rows[0][0].ToString());

                Response.Write("<script> alert('Dashboard Defined Successfully.')</script>");

                CLEAR_DASHBOARD();
                GET_DASHBOARD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_DASHBOARD()
        {
            try
            {
                string SEQNO, Header;
                bool GRAPH, TILE, ENROLL;

                SEQNO = txtDashboardSeqNo.Text;
                Header = txtDashboardHeader.Text;
                GRAPH = chkDashboardStatusGraph.Checked;
                TILE = chkDashboardStatusTile.Checked;
                ENROLL = chkDashboardStatusEnroll.Checked;

                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(
                            ACTION: "UPDATE_DASHBOARD",
                            ID: ViewState["editSTATUSDASHBOARD"].ToString(),
                            SEQNO: SEQNO,
                            FormHeader: Header,
                            IWRS_Graph: GRAPH,
                            IWRS_Tile: TILE,
                            ENTEREDBY: Session["USER_ID"].ToString(),
                            DOSING: ENROLL
                                );

                INSERT_DASHBOARD_ITEM(ViewState["editSTATUSDASHBOARD"].ToString());

                Response.Write("<script> alert('Defined Dashboard Updated Successfully.')</script>");

                CLEAR_DASHBOARD();
                GET_DASHBOARD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_DASHBOARD_ITEM(string DASH_ID)
        {
            try
            {
                for (int j = 0; j < gvDashboardStatus.Rows.Count; j++)
                {
                    CheckBox chkDashboard = (CheckBox)gvDashboardStatus.Rows[j].FindControl("chkDashboard");
                    string ID = ((Label)gvDashboardStatus.Rows[j].FindControl("lblID")).Text;
                    string SEQNO = ((TextBox)gvDashboardStatus.Rows[j].FindControl("txtSEQNO")).Text;
                    string STATUSCODE = ((Label)gvDashboardStatus.Rows[j].FindControl("lblSTATUSCODE")).Text;
                    string Condition = ((DropDownList)gvDashboardStatus.Rows[j].FindControl("ddlCondition")).SelectedValue;

                    if (chkDashboard.Checked == true)
                    {
                        DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "INSERT_DASHBOARD_ITEM", ID: DASH_ID, CRITERIA_ID: ID, STATUSCODE: STATUSCODE, Criteria: Condition, ENTEREDBY: Session["USER_ID"].ToString(), SEQNO: SEQNO);
                    }
                    else
                    {
                        DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "DELETE_DASHBOARD_ITEM", ENTEREDBY: Session["USER_ID"].ToString(), ID: DASH_ID, CRITERIA_ID: ID);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_DASHBOARD()
        {
            try
            {
                txtDashboardHeader.Text = "";
                txtDashboardSeqNo.Text = "";
                chkDashboardStatusGraph.Checked = false;
                chkDashboardStatusTile.Checked = false;
                chkDashboardStatusEnroll.Checked = false;

                GET_STATUS_DASHBOARD("0");

                btnSubmit_Dashborad.Visible = true;
                btnUpdate_Dashborad.Visible = false;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_DASHBOARD(string DASH_ID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "SELECT_DASHBOARD", ID: DASH_ID);

                GET_STATUS_DASHBOARD(DASH_ID);

                txtDashboardHeader.Text = ds.Tables[0].Rows[0]["Header"].ToString();
                txtDashboardSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

                if (ds.Tables[0].Rows[0]["GRAPH"].ToString() == "True")
                {
                    chkDashboardStatusGraph.Checked = true;
                }
                else
                {
                    chkDashboardStatusGraph.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["TILE"].ToString() == "True")
                {
                    chkDashboardStatusTile.Checked = true;
                }
                else
                {
                    chkDashboardStatusTile.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["ENROLL"].ToString() == "True")
                {
                    chkDashboardStatusEnroll.Checked = true;
                }
                else
                {
                    chkDashboardStatusEnroll.Checked = false;
                }

                btnUpdate_Dashborad.Visible = true;
                btnSubmit_Dashborad.Visible = false;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_DASHBOARD(string DASH_ID)
        {
            try
            {
                dal_IWRS.IWRS_SET_DB_SP(ACTION: "DELETE_DASHBOARD", ENTEREDBY: Session["USER_ID"].ToString(), ID: DASH_ID);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Dashboard Deleted Successfully.'); ", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DASHBOARD()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_DASHBOARD");
                gvStatusDashboard.DataSource = ds;
                gvStatusDashboard.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvDashboardStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string Count = dr["Count"].ToString();

                    CheckBox chkDashboard = (CheckBox)e.Row.FindControl("chkDashboard");
                    DropDownList ddlCondition = (DropDownList)e.Row.FindControl("ddlCondition");

                    if (Count != "0")
                    {
                        chkDashboard.Checked = true;
                    }
                    else
                    {
                        chkDashboard.Checked = false;
                    }

                    if (dr["CRITERIA"].ToString() != "")
                    {
                        ddlCondition.SelectedValue = dr["CRITERIA"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Dashborad_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_DASHBOARD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdate_Dashborad_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_DASHBOARD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Dashborad_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_DASHBOARD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvStatusDashboard_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editSTATUSDASHBOARD"] = id;

                if (e.CommandName == "EditDash")
                {
                    SELECT_DASHBOARD(id);
                }
                else if (e.CommandName == "DeleteDash")
                {
                    DELETE_DASHBOARD(id);
                    GET_DASHBOARD();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Define Dashboard End

        //Define Reason for Unblinding Starts

        private void CLEAR_UNBLIND_REASON()
        {
            try
            {
                txtUnblindSeqNo.Text = "";
                txtUnblindReason.Text = "";

                btnSubmitUnblindReason.Visible = true;
                btnUpdateUnblindReason.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_UNBLIND_REASON()
        {
            try
            {
                dal_IWRS.NIWRS_SETUP_SP
                    (
                    ACTION: "INSERT_OPTION",
                    STRATA: "UNBLIND_REASON",
                    SEQNO: txtUnblindSeqNo.Text,
                    ANS: txtUnblindReason.Text,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Reason for Unblinding Defined Successfully.'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_UNBLIND_REASON()
        {
            try
            {
                dal_IWRS.NIWRS_SETUP_SP
                    (
                    ACTION: "UPDATE_OPTION",
                    ID: ViewState["UNBLIND_REASON_ID"].ToString(),
                    STRATA: "UNBLIND_REASON",
                    SEQNO: txtUnblindSeqNo.Text,
                    ENTEREDBY: Session["USER_ID"].ToString(),
                    ANS: txtUnblindReason.Text
                    );
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Reason for Unblinding Updated Successfully.'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_UNBLIND_REASON(string ID)
        {
            try
            {
                dal_IWRS.IWRS_SET_OPTION_SP
                    (
                    ACTION: "DELETE_OPTION", ENTEREDBY: Session["USER_ID"].ToString(),
                    ID: ID
                    );
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Reason for Unblinding Deleted Successfully.'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_UNBLIND_REASON(string ID)
        {
            try
            {
                ViewState["UNBLIND_REASON_ID"] = ID;

                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "SELECT_OPTION", ID: ID);

                txtUnblindSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                txtUnblindReason.Text = ds.Tables[0].Rows[0]["TEXT"].ToString();

                btnSubmitUnblindReason.Visible = false;
                btnUpdateUnblindReason.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_UNBLIND_REASON()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_OPTION", STRATA: "UNBLIND_REASON");
                gvUnblindReasons.DataSource = ds;
                gvUnblindReasons.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvUnblindReasons_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "EditReason")
                {
                    SELECT_UNBLIND_REASON(id);
                }
                else if (e.CommandName == "DeleteReason")
                {
                    DELETE_UNBLIND_REASON(id);
                    GET_UNBLIND_REASON();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitUnblindReason_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_UNBLIND_REASON();
                GET_UNBLIND_REASON();
                CLEAR_UNBLIND_REASON();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateUnblindReason_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_UNBLIND_REASON();
                GET_UNBLIND_REASON();
                CLEAR_UNBLIND_REASON();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelUnblindReason_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_UNBLIND_REASON();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Define Reason for Unblinding Ends



        //Define Reason for DCF Starts

        private void CLEAR_DCF_REASON()
        {
            try
            {
                txtDCFSeqNo.Text = "";
                txtDCFReason.Text = "";

                btnSubmitDCFReason.Visible = true;
                btnUpdateDCFReason.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_DCF_REASON()
        {
            try
            {
                dal_IWRS.NIWRS_SETUP_SP
                    (
                    ACTION: "INSERT_OPTION",
                    STRATA: "DCF_REASON",
                    SEQNO: txtDCFSeqNo.Text,
                    ANS: txtDCFReason.Text,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Reason for DCF Defined Successfully.'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_DCF_REASON()
        {
            try
            {
                dal_IWRS.NIWRS_SETUP_SP
                    (
                    ACTION: "UPDATE_OPTION",
                    ID: ViewState["DCF_REASON_ID"].ToString(),
                    STRATA: "DCF_REASON",
                    SEQNO: txtDCFSeqNo.Text,
                    ANS: txtDCFReason.Text,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Reason for DCF Updated Successfully.'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_DCF_REASON(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_SETUP_SP
                    (
                    ACTION: "DELETE_OPTION", ENTEREDBY: Session["USER_ID"].ToString(),
                    ID: ID
                    );
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Reason for DCF Deleted Successfully.'); ", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_DCF_REASON(string ID)
        {
            try
            {
                ViewState["DCF_REASON_ID"] = ID;

                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "SELECT_OPTION", ID: ID);

                txtDCFSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                txtDCFReason.Text = ds.Tables[0].Rows[0]["TEXT"].ToString();

                btnSubmitDCFReason.Visible = false;
                btnUpdateDCFReason.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DCF_REASON()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_OPTION", STRATA: "DCF_REASON");
                gvDCFReason.DataSource = ds;
                gvDCFReason.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvDCFReason_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "EditReason")
                {
                    SELECT_DCF_REASON(id);
                }
                else if (e.CommandName == "DeleteReason")
                {
                    DELETE_DCF_REASON(id);
                    GET_DCF_REASON();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitDCFReason_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_DCF_REASON();
                GET_DCF_REASON();
                CLEAR_DCF_REASON();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateDCFReason_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_DCF_REASON();
                GET_DCF_REASON();
                CLEAR_DCF_REASON();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelDCFReason_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_DCF_REASON();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Define Reason for Unblinding Ends

        //---------------------------------------------------------Randomization Code here 

        private void RandGrd()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_ARMS_SP(
                     ACTION: "GET_RAND"
                    );
                grdRandArm.DataSource = ds;
                grdRandArm.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitRandomizationArm_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_Randomiazation();
                RandGrd();
                Clear();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_Randomiazation()
        {
            try
            {
                dal_IWRS.IWRS_SET_ARMS_SP
                    (
                    ACTION: "INSERT_RAND",
                    TREAT_GRP: txtTreatmentCode.Text,
                    TREAT_GRP_NAME: txtTreatmentName.Text,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Randomization Arms Defined Successfully.'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateRandomizationArm_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_Randomiazation();
                RandGrd();
                Clear();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_Randomiazation()
        {
            try
            {
                dal_IWRS.IWRS_SET_ARMS_SP
                    (
                    ACTION: "UPDATE_RAND",
                       ID: ViewState["ID"].ToString(),
                       TREAT_GRP: txtTreatmentCode.Text,
                       TREAT_GRP_NAME: txtTreatmentName.Text,
                       ENTEREDBY: Session["USER_ID"].ToString()
                     );
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Randomization Arms Updated Successfully.'); ", true);
                RandGrd();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancleRandomizationArm_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtTreatmentCode.Text = "";
            txtTreatmentName.Text = "";
            btnUpdateRandomizationArm.Visible = false;
            btnSubmitRandomizationArm.Visible = true;

        }

        protected void grdRandArm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "EditRand")
                {
                    EIDTRAND(id);
                }
                else if (e.CommandName == "DeleteRand")
                {
                    DELETERANE(id);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETERANE(string id)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_ARMS_SP(ACTION: "DELETE_RAND", ENTEREDBY: Session["USER_ID"].ToString(), ID: id);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Randomization Arms Deleted Successfully.'); ", true);

                RandGrd();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EIDTRAND(string id)
        {
            try
            {
                ViewState["ID"] = id;

                DataSet ds = dal_IWRS.IWRS_SET_ARMS_SP(ACTION: "EIDT_RAND", ID: id);

                if (ds.Tables.Count > 0)
                {
                    txtTreatmentCode.Text = ds.Tables[0].Rows[0]["TREAT_GRP"].ToString();
                    txtTreatmentName.Text = ds.Tables[0].Rows[0]["TREAT_GRP_NAME"].ToString();

                    btnSubmitRandomizationArm.Visible = false;
                    btnUpdateRandomizationArm.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        //---------------------------------------------------------Dosing Code here
        private void Dosearm()
        {
            DataSet Ds = dal_IWRS.IWRS_SET_ARMS_SP(
                ACTION: "GET_DOSING"
                );

            grdDosArm.DataSource = Ds;
            grdDosArm.DataBind();
        }

        protected void grdDosArm_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string Id = e.CommandArgument.ToString();
            if (e.CommandName == "EditDose")
            {
                EditDosingarm(Id);
            }
            else if (e.CommandName == "DeleteDose")
            {
                DeleteDosingarm(Id);
            }
        }

        private void DeleteDosingarm(string id)
        {
            try
            {
                DataSet Ds = dal_IWRS.IWRS_SET_ARMS_SP(ACTION: "DELETE_DOSING", ENTEREDBY: Session["USER_ID"].ToString(), ID: id);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Dosing Arms Deleted Successfully.'); ", true);

                Dosearm();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EditDosingarm(string id)
        {
            try
            {
                ViewState["ID"] = id;
                DataSet Ds = dal_IWRS.IWRS_SET_ARMS_SP(ACTION: "EDIT_DOSING", ID: id);

                txtDosingCode.Text = Ds.Tables[0].Rows[0]["TREAT_GRP"].ToString();
                txtDosingName.Text = Ds.Tables[0].Rows[0]["TREAT_GRP_NAME"].ToString();

                btnSubmitDosingArm.Visible = false;
                btnUpdateDosingArm.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnSubmitDosingArm_Click(object sender, EventArgs e)
        {
            try
            {
                dal_IWRS.IWRS_SET_ARMS_SP(
                       ACTION: "INSERT_DOSING",
                       TREAT_GRP: txtDosingCode.Text,
                       TREAT_GRP_NAME: txtDosingName.Text,
                       ENTEREDBY: Session["USER_ID"].ToString()
                       );

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Dosing Arms Defined Successfully.'); ", true);
                Dosearm();
                ClearDosing();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateDosingArm_Click(object sender, EventArgs e)
        {
            try
            {
                dal_IWRS.IWRS_SET_ARMS_SP(
                       ACTION: "UPDATE_DOSING",
                       ID: ViewState["ID"].ToString(),
                       TREAT_GRP: txtDosingCode.Text,
                       TREAT_GRP_NAME: txtDosingName.Text,
                       ENTEREDBY: Session["USER_ID"].ToString()
                       );
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Defined Dosing Arms Updated Successfully.'); ", true);
                Dosearm();
                ClearDosing();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void ClearDosing()
        {
            txtDosingCode.Text = "";
            txtDosingName.Text = "";
            btnSubmitDosingArm.Visible = true;
            btnUpdateDosingArm.Visible = false;
        }

        protected void btnCancelDosingArm_Click(object sender, EventArgs e)
        {
            ClearDosing();
        }

        protected void grdVisit_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lbtnmultipleDependency = (e.Row.FindControl("lbtnmultipleDependency") as LinkButton);
                    LinkButton lbtndeleteSection = (e.Row.FindControl("lbtndeleteSection") as LinkButton);

                    string MODULE = dr["MODULE"].ToString();
                    string COUNT = dr["COUNT"].ToString();

                    if (MODULE == "-1")
                    {
                        lbtnmultipleDependency.Visible = true;
                    }
                    else
                    {
                        lbtnmultipleDependency.Visible = false;
                    }

                    if (COUNT != "0")
                    {
                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndeleteSection.Enabled = false;
                        lbtndeleteSection.ToolTip = "Configuration has been Frozen";
                        lbtndeleteSection.OnClientClick = "return ConfigFrozen_MSG()";
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lbtndeleteSection = (e.Row.FindControl("lbtndeleteSection") as LinkButton);

                    string COUNT = dr["COUNT"].ToString();

                    if (COUNT != "0")
                    {
                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndeleteSection.Enabled = false;
                        lbtndeleteSection.ToolTip = "Configuration has been Frozen";
                        lbtndeleteSection.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdCols_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lbtndeleteSection = (e.Row.FindControl("lbtndeleteSection") as LinkButton);

                    string COUNT = dr["COUNT"].ToString();

                    if (COUNT != "0")
                    {

                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndeleteSection.Enabled = false;
                        lbtndeleteSection.ToolTip = "Configuration has been Frozen";
                        lbtndeleteSection.OnClientClick = "return ConfigFrozen_MSG()";
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdForm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lbtndeleteSection = (e.Row.FindControl("lbtndeleteSection") as LinkButton);

                    string COUNT = dr["COUNT"].ToString();

                    if (COUNT != "0")
                    {
                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndeleteSection.Enabled = false;
                        lbtndeleteSection.ToolTip = "Configuration has been Frozen";
                        lbtndeleteSection.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSubSites_PreRender(object sender, EventArgs e)
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

        protected void grdSubSites_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    LinkButton lbtndeleteSection = (e.Row.FindControl("lbtndeleteSection") as LinkButton);

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndeleteSection.Enabled = false;
                        lbtndeleteSection.ToolTip = "Configuration has been Frozen";
                        lbtndeleteSection.OnClientClick = "return ConfigFrozen_MSG()";
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdGraphs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    LinkButton lbtndeleteSection = (e.Row.FindControl("lbtndeleteSection") as LinkButton);

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndeleteSection.Enabled = false;
                        lbtndeleteSection.ToolTip = "Configuration has been Frozen";
                        lbtndeleteSection.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvStatusDashboard_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    LinkButton lbtndelete = (e.Row.FindControl("lbtndelete") as LinkButton);

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndelete.Enabled = false;
                        lbtndelete.ToolTip = "Configuration has been Frozen";
                        lbtndelete.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvUnblindReasons_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    LinkButton lbtndelete = (e.Row.FindControl("lbtndelete") as LinkButton);

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndelete.Enabled = false;
                        lbtndelete.ToolTip = "Configuration has been Frozen";
                        lbtndelete.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvDCFReason_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    LinkButton lbtndelete = (e.Row.FindControl("lbtndelete") as LinkButton);

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndelete.Enabled = false;
                        lbtndelete.ToolTip = "Configuration has been Frozen";
                        lbtndelete.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdRandArm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    LinkButton lbtndelete = (e.Row.FindControl("lbtndelete") as LinkButton);

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndelete.Enabled = false;
                        lbtndelete.ToolTip = "Configuration has been Frozen";
                        lbtndelete.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdDosArm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    LinkButton lbtndelete = (e.Row.FindControl("lbtndelete") as LinkButton);

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndelete.Enabled = false;
                        lbtndelete.ToolTip = "Configuration has been Frozen";
                        lbtndelete.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void lbnsubsiteExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Sub-Sites.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "SUBSITE_AUDITTRAIL"
                     );
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

        protected void LbtnVisitExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Visits.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "VISITS_AUDITTRAIL"
                     );
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

        protected void LbtnStatusExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Status.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "STATUS_AUDITTRAIL"
                     );
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

        protected void LbtnAddFieldExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Additional-Fields.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "ADDITIONALFIELD_AUDITTRAIL"
                     );
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

        protected void LbtnFormsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Forms.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "FORMS_AUDITTRAIL"
                     );
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

        protected void LbtnListingExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Listings.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "LISTINGS_AUDITTRAIL"
                     );
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

        protected void LbtnGraphExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Graphs.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "GRAPHS_AUDITTRAIL"
                     );
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

        protected void LbtnDashboardExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Status-DashBoard.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "STATUS_DASHBOARD_AUDITTRAIL"
                     );
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

        protected void LbtnReaUnbliExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Reasons-Unblinding.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "REASONSUNBLINDING_AUDITTRAIL"
                     );
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

        protected void LbtnReaDCFExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Reasons-DCF.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "REASONSDCF_AUDITTRAIL"
                     );
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

        protected void LbtnRandArmsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Randomization-Arms.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "RANDOM_ARMS_AUDITTRAIL"
                     );
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

        protected void LbtnDosingArmsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Dosing-Arms.xls";

                DataSet ds = dal_IWRS.IWRS_LOG_SP(
                     ACTION: "DOSING_ARMS_AUDITTRAIL"
                     );
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

        protected void lbtnDownloadtemp_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName, ContentType;
                byte[] fileData;
                DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "DOWNLOAD_REPORT_TEMPLATE");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                        ContentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                        fileData = (byte[])ds.Tables[0].Rows[0]["DATA"];
                        Response.Clear();
                        Response.ContentType = ContentType;
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                        Response.BinaryWrite(fileData);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnUploadTemp_Click(object sender, EventArgs e)
        {
            try
            {

                UPLOAD_UNBL_RPT_TEMP();
                GET_UNBRPT_TEMPLATE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void UPLOAD_UNBL_RPT_TEMP()
        {
            try
            {
                string fileName = UploadRPTTEMPLATE.FileName;
                string contentType = UploadRPTTEMPLATE.PostedFile.ContentType;

                string folderPath = Server.MapPath("~/UNBL_RPT_TEMPLATE/");


                string fileExtension = Path.GetExtension(fileName).ToLower();
                int fileSize = UploadRPTTEMPLATE.PostedFile.ContentLength;

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(Server.MapPath("~/UNBL_RPT_TEMPLATE/"), UploadRPTTEMPLATE.FileName);

                UploadRPTTEMPLATE.SaveAs(filePath);

                byte[] fileData;
                using (Stream stream = UploadRPTTEMPLATE.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();

                    }
                }
                string QUESTION = "", QUECODE = "", ANS = "";

                QUESTION = "Unblinding report template  must be.";
                QUECODE = "UNBLINDINGREPORT";
                if (fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".docm")
                {

                    dal_IWRS.IWRS_SET_OPTION_SP(
                                        ACTION: "INSERT_QUES",
                                        QUESTION: QUESTION,
                                        QUECODE: QUECODE,
                                        ANS: ANS,
                                        ENTEREDBY: Session["USER_ID"].ToString(),
                                        FileName: fileName,
                                        ContentType: contentType,
                                        fileData: fileData
                                    );

                    Response.Write("<script>alert('Unblinding report template uploaded Successfully.')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please select word file only.')</script>");
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnCancelTemp_Click(object sender, EventArgs e)
        {
            Response.Redirect("NIWRS_SETUP.aspx");
        }
    }

}