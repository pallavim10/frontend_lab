using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using CTMS.CommonFunction;
using PPT;
using System.IO;

namespace CTMS
{
    public partial class NSAE_MODULE_DATA_LOGS : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        DAL dal = new DAL();
        CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.IsPostBack)
                {
                    txtNotes.Attributes.Add("MaxLength", "500");

                    hfanyBlank.Value = "false";
                    hffirstClick.Value = "false";

                    hdnSITEID.Value = Request.QueryString["INVID"].ToString();
                    hdnSubjectID.Value = Request.QueryString["SUBJID"].ToString();
                    hdnSAEID.Value = Request.QueryString["SAEID"].ToString();
                    hdnSAE.Value = Request.QueryString["SAE"].ToString();
                    hdnSTATUS.Value = Request.QueryString["STATUS"].ToString();

                    if (Session["SignOff_Safety"].ToString() == "1" || Session["SignOff_Safety"].ToString() == "True" || Session["SignOff_Safety"].ToString() == "true")
                    {
                        btnINVSignOff.Visible = true;
                    }
                    else
                    {
                        btnINVSignOff.Visible = false;
                    }

                    OpenCRF();
                    GetSign_info();

                    DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_SAE_LIST_DATA",
                        SAEID: Request.QueryString["SAEID"].ToString()
                        );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DivSAEDetails.Visible = true;
                        lblAESPID.Text = ds.Tables[0].Rows[0]["DM_SPID"].ToString();
                        lblAETERM.Text = ds.Tables[0].Rows[0]["DM_TERM"].ToString();
                        lblSAEID.Text = hdnSAEID.Value;
                        lblSAE.Text = hdnSAE.Value;
                        lblStatus.Text = hdnSTATUS.Value;

                        string Reason = ds.Tables[0].Rows[0]["REASON"].ToString();

                        if (Reason != null && Reason != "")
                        {
                            btnDelayedReason.Visible = true;
                            hdn_Delayed_Reason.Value = ds.Tables[0].Rows[0]["REASON"].ToString();
                            hdn_Delayed_ReasonBy.Value = ds.Tables[0].Rows[0]["REASONBYNAME"].ToString();
                            hdn_Delayed_DTServer.Value = ds.Tables[0].Rows[0]["REASON_CAL_DAT"].ToString();
                            hdn_Delayed_DTUser.Value = ds.Tables[0].Rows[0]["REASON_CAL_TZDAT"].ToString();
                        }
                        else
                        {
                            btnDelayedReason.Visible = false;
                        }
                    }
                    else
                    {
                        DivSAEDetails.Visible = false;
                    }

                    DataSet ds1 = dal_SAE.SAE_DELETED_SP(ACTION: "GET_DOWNGRADING_REQ_SAE_DETAILS",
                        SAEID: Request.QueryString["SAEID"].ToString(),
                        STATUS: Request.QueryString["STATUS"].ToString()
                        );

                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        btnDeleteSAE.Text = "Downgrading Request Reason";
                        hdn_DeletedReq.Value = ds1.Tables[0].Rows[0]["REQ"].ToString();
                        hdn_DeletedReq_Reason.Value = ds1.Tables[0].Rows[0]["REQ_REASON"].ToString();
                        hdn_DeletedReq_ReasonBy.Value = ds1.Tables[0].Rows[0]["REQ_BYNAME"].ToString();
                        hdn_DeletedReq_DTServer.Value = ds1.Tables[0].Rows[0]["REQ_CAL_DAT"].ToString();
                        hdn_DeletedReq_DTUser.Value = ds1.Tables[0].Rows[0]["REQ_CAL_TZDAT"].ToString();
                    }
                    else
                    {
                        btnDeleteSAE.Text = "Downgrading SAE";
                    }

                    DataSet ds3 = dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "GET_COUT_SUPPORTING_DOC", SAEID: Request.QueryString["SAEID"].ToString());
                    if (ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                    {
                        lblCount.Text = ds3.Tables[0].Rows[0]["COUNT"].ToString();
                        lblCount.Visible = true;
                    }
                    else
                    {
                        lblCount.Text = "";
                        lblCount.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GetSign_info()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SIGNOFF_SP(
                    ACTION: "GET_INV_SIGNOFF_DETAILS",
                    SAEID: Request.QueryString["SAEID"].ToString()
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gridsigninfo.DataSource = ds.Tables[0];
                    gridsigninfo.DataBind();
                }
                else
                {
                    gridsigninfo.DataSource = null;
                    gridsigninfo.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        private void OpenCRF()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_OPEN_CRF_SP(
                ACTION: "GET_OPEN_CRF",
                SAEID: Request.QueryString["SAEID"].ToString()
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grd_OpenCRF.DataSource = ds.Tables[0];
                    Grd_OpenCRF.DataBind();
                }
                else
                {
                    Grd_OpenCRF.DataSource = null;
                    Grd_OpenCRF.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void Grd_OpenCRF_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                TextBox txtPAGESTATUS = e.Row.FindControl("txtPAGESTATUS") as TextBox;

                ImageButton lnkVISITPAGENO = e.Row.FindControl("lnkPAGENUM") as ImageButton;

                if (txtPAGESTATUS.Text == "1")
                {
                    if (dr["HasMissing"].ToString() == "True")
                    {
                        lnkVISITPAGENO.ImageUrl = "img/REDFILE.png";
                        lnkVISITPAGENO.ToolTip = "Missing Fields";
                    }
                    else if (dr["IsComplete"].ToString() == "0")
                    {
                        lnkVISITPAGENO.ImageUrl = "img/orange file.png";
                        lnkVISITPAGENO.ToolTip = "Incomplete";
                    }
                    else
                    {
                        lnkVISITPAGENO.ImageUrl = "img/green file.png";
                        lnkVISITPAGENO.ToolTip = "Complete";
                    }
                }
                else
                {
                    lnkVISITPAGENO.ToolTip = "Not Entered";
                }

                if (dr["QueryCount"].ToString() == "0")
                {
                    LinkButton lnkQuery = (LinkButton)e.Row.FindControl("lnkQuery");
                    lnkQuery.Visible = false;
                }

                if (dr["AnsQueryCount"].ToString() == "0")
                {
                    LinkButton lnkQUERYANS = (LinkButton)e.Row.FindControl("lnkQUERYANS");
                    lnkQUERYANS.Visible = false;
                }

                LinkButton lbtnPageComment = (LinkButton)e.Row.FindControl("lbtnPageComment");
                Label lblPageComment = (Label)e.Row.FindControl("lblPageComment");
                if (dr["MODULE_COMMENT"].ToString() == "0")
                {
                    lbtnPageComment.Visible = false;
                }
                else
                {
                    lblPageComment.Text = dr["MODULE_COMMENT"].ToString();
                    lbtnPageComment.Visible = true;
                }

                if (dr["REVIEWEDSTATUS"].ToString() != "True" && txtPAGESTATUS.Text == "1")
                {
                    e.Row.Cells[6].CssClass = "brd-1px-maroonimp";

                    btnINVSignOff.Visible = false;
                }
            }
        }

        protected void Grd_OpenCRF_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "GOTOPAGE")
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                    string SAEID = hdnSAEID.Value;
                    string SAE = hdnSAE.Value;
                    string STATUS = Request.QueryString["STATUS"].ToString();
                    string INVID = Request.QueryString["INVID"].ToString();
                    string SUBJID = Request.QueryString["SUBJID"].ToString();
                    string MODULEID = (gvr.FindControl("MODULEID") as Label).Text;

                    string NEW_FOLLOWUPNO = "";
                    if (Request.QueryString["NEW_FOLLOWUPNO"] != null)
                    {
                        NEW_FOLLOWUPNO = Request.QueryString["NEW_FOLLOWUPNO"].ToString();
                    }

                    if ((gvr.FindControl("MULTIPLEYN") as Label).Text == "True")
                    {
                        if (NEW_FOLLOWUPNO != "")
                        {
                            Response.Redirect("NSAE_MULTIPLE_DATA_ENTRY.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&MODULEID=" + MODULEID + "&NEW_FOLLOWUPNO=" + NEW_FOLLOWUPNO, false);
                        }
                        else
                        {
                            Response.Redirect("NSAE_MULTIPLE_DATA_ENTRY.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&MODULEID=" + MODULEID, false);
                        }
                    }
                    else
                    {
                        if (NEW_FOLLOWUPNO != "")
                        {
                            Response.Redirect("NSAE_DATA_ENTRY.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&MODULEID=" + MODULEID + "&NEW_FOLLOWUPNO=" + NEW_FOLLOWUPNO, false);
                        }
                        else
                        {
                            Response.Redirect("NSAE_DATA_ENTRY.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&MODULEID=" + MODULEID, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        int NEW_NO;
        protected void btnINVSignOff_Click(object sender, EventArgs e)
        {
            if (hffirstClick.Value != "true")
            {
                hffirstClick.Value = "true";

                for (int i = 0; i < Grd_OpenCRF.Rows.Count; i++)
                {
                    TextBox txtPAGESTATUS = (TextBox)Grd_OpenCRF.Rows[i].FindControl("txtPAGESTATUS");

                    if (txtPAGESTATUS.Text == "" || txtPAGESTATUS.Text == "0")
                    {
                        Grd_OpenCRF.Rows[i].Attributes.Add("class", "brd-1px-maroonimp");

                        hfanyBlank.Value = "true";
                    }
                }

            }
            else
            {
                hfanyBlank.Value = "false";
            }

            if (hfanyBlank.Value != "true")
            {
                ModalPopupExtender2.Show();

            }
            else
            {
                string MESSAGE = " Highlighted forms are not entered. Please enter details OR if you want to proceed to Submit the form then click Investigator verify and Sign off again.";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); ", true);
            }
        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            string str = Convert.ToString(txt_UserName.Text);
            string pass = Convert.ToString(txt_Pwd.Text);
            string compid = Session["User_ID"].ToString();
            ViewState["pass"] = pass;

            if (chkINVSIGN.Checked == true)
            {
                // if (str == compid && pass == comppass)
                if (str == compid)
                {
                    ViewState["User_IDP"] = str;
                    Check_AUTH();
                    txt_UserName.Text = "";
                }
                else
                {
                    ModalPopupExtender2.Show();
                    Response.Write("<script>alert('Please enter valid User Id')</script>");
                }
            }
            else
            {
                ModalPopupExtender2.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Sign on Checkbox'); ", true);
                txt_Pwd.Text = ViewState["pass"].ToString();
            }

        }

        private void Check_AUTH()
        {
            try
            {
                DAL_UMT dal_UMT = new DAL_UMT();

                DataSet dsAuth = dal_UMT.UMT_Auth(UserID: txt_UserName.Text, Pwd: txt_Pwd.Text);

                if (dsAuth.Tables.Count > 0 && dsAuth.Tables[0].Rows.Count > 0)
                {
                    string RESULT = dsAuth.Tables[0].Rows[0][0].ToString();

                    switch (RESULT)
                    {
                        case "Account Locked":
                            Response.Write("<script> alert('Your account has been locked'); window.location='Login.aspx';  </script>");

                            break;

                        case "Invalid Credentials, Account Locked":
                            Response.Write("<script> alert('Invalid credentials, Your account has been locked'); window.location='Login.aspx';  </script>");

                            break;

                        case "Invalid Credentials":
                            Response.Write("<script> alert('Invalid credentials');</script>");
                            ModalPopupExtender2.Show();

                            break;

                        case "Invalid User ID":
                            Response.Write("<script> alert('Invalid User ID');</script>");
                            ModalPopupExtender2.Show();

                            break;

                        default:
                            SHOW_STATUS_POPUP();

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SHOW_STATUS_POPUP()
        {
            try
            {
                string STATUS = "";

                if (Request.QueryString["STATUS"].ToString() == "Initial; Incomplete" || Request.QueryString["STATUS"].ToString() == "Change Initial Report")
                {
                    btnNewStatus.Text = "Initial; Complete";

                    SaveDataStatus();
                }
                else if (Request.QueryString["STATUS"].ToString() == "Initial; Complete")
                {
                    DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_LAST_SAE_SUBMIT_DATE",
                        SAEID: hdnSAEID.Value,
                        STATUS: hdnSTATUS.Value
                        );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["ENTEREDDAT"].ToString() != "")
                        {
                            DateTime SAE_LOG_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["ENTEREDDAT"].ToString());

                            TimeSpan diff = DateTime.Now - SAE_LOG_DATE;
                            int MINUTES = Convert.ToInt32(diff.TotalMinutes);

                            if (MINUTES <= 1440)
                            {
                                btnOldStatus.Visible = true;
                                btnOldStatus.Text = "Initial; Complete";
                                btnNewStatus.Text = "Follow up 1";
                            }
                            else
                            {
                                btnOldStatus.Visible = false;
                                btnNewStatus.Text = "Follow up 1";
                            }
                        }
                        else
                        {
                            btnOldStatus.Visible = false;
                            btnNewStatus.Text = "Follow up 1";
                        }

                        ModalPopupExtender1.Show();
                    }
                }
                else if (Request.QueryString["STATUS"].ToString().Contains("Follow up"))
                {
                    string[] numbers = Regex.Split(Request.QueryString["STATUS"].ToString(), @"\D+");
                    int i = 0;
                    foreach (string value in numbers)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            i = int.Parse(value);
                        }
                    }

                    NEW_NO = i + 1;
                    STATUS = "Follow up " + NEW_NO;
                    btnOldStatus.Visible = false;
                    spanor.Visible = false;
                    btnNewStatus.Text = STATUS;
                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SaveDataStatus()
        {
            try
            {
                dal_SAE.SAE_INSERT_PV_SP(ACTION: "INSERT_SAE_SUBJECT_STATUS_LOGS",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    STATUS: btnNewStatus.Text
                    );

                dal_SAE.SAE_MEDICAL_REVIEW_SP(ACTION: "MODIFY_SUBMITED_MR_STATUS_DATA",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    OLDSTATUS: lblStatus.Text,
                    STATUS: btnNewStatus.Text
                    );

                dal_SAE.SAE_SDVDETAILS_SP(ACTION: "MODIFY_SUBMITED_SDV_STATUS_DATA",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    OLDSTATUS: lblStatus.Text,
                    STATUS: btnNewStatus.Text
                    );

                dal_SAE.SAE_INSERT_PV_SP(ACTION: "UPDATE_STATUS",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    STATUS: btnNewStatus.Text
                    );

                INSERT_STATUS_DATA(btnNewStatus.Text);

                dal_SAE.SAE_SIGNOFF_SP(ACTION: "INSERT_INV_SIGNOFF",
                     SAEID: hdnSAEID.Value,
                     SAE: hdnSAE.Value,
                     INVID: Request.QueryString["INVID"].ToString(),
                     SUBJID: Request.QueryString["SUBJID"].ToString(),
                     STATUS: btnNewStatus.Text
                     );

                string MESSAGE = "SAEID: " + Request.QueryString["SAEID"].ToString() + " with Status " + btnNewStatus.Text + " Successfully";

                if (btnNewStatus.Text == "Initial; Complete")
                {
                    SendEmail(Request.QueryString["SAEID"].ToString(), btnNewStatus.Text, "Initial; Complete");

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NSAE_LOGGED_EVENTS_LIST_Complete.aspx';", true);
                }
                else if (btnNewStatus.Text.Contains("Follow up"))
                {
                    SendEmail(Request.QueryString["SAEID"].ToString(), btnNewStatus.Text, "Follow up");

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NSAE_LOGGED_EVENTS_LIST_FollowUp.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_STATUS_DATA(string Status)
        {
            try
            {
                INSERT_DATA_STATUS_LOGS(Status);

                dal_SAE.SAE_INSERT_MODULE_DATA_SP(ACTION: "INSERT_STATUS_LOG",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    STATUS: Status
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void INSERT_DATA_STATUS_LOGS(string STATUS)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_SAE_MODULE_TABLENAME");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dal_SAE.SAE_INSERT_MODULE_DATA_SP(ACTION: "INSERT_SAE_STATUS_LOGS",
                        SAEID: Request.QueryString["SAEID"].ToString(),
                        STATUS: STATUS,
                        TABLENAME: ds.Tables[0].Rows[i]["TABLENAME"].ToString()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnOldStatus_click(object sender, EventArgs e)
        {
            try
            {
                dal_SAE.SAE_INSERT_PV_SP(ACTION: "INSERT_SAE_SUBJECT_STATUS_LOGS",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    STATUS: btnOldStatus.Text
                    );

                dal_SAE.SAE_MEDICAL_REVIEW_SP(ACTION: "MODIFY_SUBMITED_MR_STATUS_DATA",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    STATUS: btnOldStatus.Text
                    );

                dal_SAE.SAE_SDVDETAILS_SP(ACTION: "MODIFY_SUBMITED_SDV_STATUS_DATA",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    STATUS: btnOldStatus.Text
                    );

                dal_SAE.SAE_INSERT_PV_SP(ACTION: "UPDATE_STATUS",
                    SAEID: Request.QueryString["SAEID"].ToString(),
                    STATUS: btnOldStatus.Text
                    );

                INSERT_STATUS_DATA(btnOldStatus.Text);

                dal_SAE.SAE_SIGNOFF_SP(ACTION: "INSERT_INV_SIGNOFF",
                     SAEID: hdnSAEID.Value,
                     SAE: hdnSAE.Value,
                     INVID: Request.QueryString["INVID"].ToString(),
                     SUBJID: Request.QueryString["SUBJID"].ToString(),
                     STATUS: btnOldStatus.Text
                     );

                string MESSAGE = "SAEID: " + Request.QueryString["SAEID"].ToString() + " with Status " + btnOldStatus.Text + " Successfully";

                if (btnOldStatus.Text == "Initial; Complete")
                {
                    SendEmail(Request.QueryString["SAEID"].ToString(), btnOldStatus.Text, "Updated Initial; Complete");

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NSAE_LOGGED_EVENTS_LIST_Complete.aspx';", true);
                }
                else if (btnOldStatus.Text.Contains("Follow up"))
                {
                    SendEmail(Request.QueryString["SAEID"].ToString(), btnOldStatus.Text, "Follow up");

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NSAE_LOGGED_EVENTS_LIST_FollowUp.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnNewStatus_click(object sender, EventArgs e)
        {
            try
            {
                SaveDataStatus();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnBacktoHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["STATUS"].ToString() == "Initial; Incomplete" || Request.QueryString["STATUS"].ToString() == "Change Initial Report")
                {
                    Response.Redirect("NSAE_LOGGED_EVENTS_LIST.aspx", false);
                }
                else if (Request.QueryString["STATUS"].ToString() == "Initial; Complete")
                {
                    Response.Redirect("NSAE_LOGGED_EVENTS_LIST_Complete.aspx", false);
                }
                else if (Request.QueryString["STATUS"].ToString().Contains("Follow"))
                {
                    Response.Redirect("NSAE_LOGGED_EVENTS_LIST_FollowUp.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SendEmail(string SAEID, string Status, string ACTIONS)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_GET_EMAIL_DATA_SP(
                        INVID: Request.QueryString["INVID"].ToString(),
                        SAEID: SAEID,
                        ACTIONS: ACTIONS
                        );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string EMAILID = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();

                    string EMAIL_CC = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();

                    string EMAIL_BCC = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();

                    string EmailSubject = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

                    string EmailBody = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();

                    if (EmailSubject.Contains("[") && EmailSubject.Contains("]"))
                    {
                        if (EmailSubject.Contains("[SUBJID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[SUBJID]", Request.QueryString["SUBJID"].ToString());
                        }

                        if (EmailSubject.Contains("[PROJECTID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                        }

                        if (EmailSubject.Contains("[SITEID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[SITEID]", Request.QueryString["INVID"].ToString());
                        }

                        if (EmailSubject.Contains("[SAEID]"))
                        {
                            EmailSubject = EmailSubject.Replace("[SAEID]", SAEID);
                        }

                        if (EmailSubject.Contains("[STATUS]"))
                        {
                            EmailSubject = EmailSubject.Replace("[STATUS]", Status);
                        }

                        if (EmailSubject.Contains("[USER]"))
                        {
                            EmailSubject = EmailSubject.Replace("[USER]", Session["User_Name"].ToString());
                        }

                        if (EmailSubject.Contains("[DATETIME]"))
                        {
                            EmailSubject = EmailSubject.Replace("[DATETIME]", comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                        }

                        if (EmailSubject.Contains("[") && EmailSubject.Contains("]"))
                        {
                            if (ds.Tables.Count > 1)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    foreach (DataColumn dc in ds.Tables[1].Columns)
                                    {
                                        if (EmailSubject.Contains("[" + dc.ToString() + "]"))
                                        {
                                            EmailSubject = EmailSubject.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (EmailBody.Contains("[") && EmailBody.Contains("]"))
                    {
                        if (EmailBody.Contains("[SUBJID]"))
                        {
                            EmailBody = EmailBody.Replace("[SUBJID]", Request.QueryString["SUBJID"].ToString());
                        }

                        if (EmailBody.Contains("[PROJECTID]"))
                        {
                            EmailBody = EmailBody.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                        }

                        if (EmailBody.Contains("[SITEID]"))
                        {
                            EmailBody = EmailBody.Replace("[SITEID]", Request.QueryString["INVID"].ToString());
                        }

                        if (EmailBody.Contains("[SAEID]"))
                        {
                            EmailBody = EmailBody.Replace("[SAEID]", SAEID);
                        }

                        if (EmailBody.Contains("[STATUS]"))
                        {
                            EmailBody = EmailBody.Replace("[STATUS]", Status);
                        }

                        if (EmailBody.Contains("[USER]"))
                        {
                            EmailBody = EmailBody.Replace("[USER]", Session["User_Name"].ToString());
                        }

                        if (EmailBody.Contains("[DATETIME]"))
                        {
                            EmailBody = EmailBody.Replace("[DATETIME]", comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                        }

                        if (EmailBody.Contains("[") && EmailBody.Contains("]"))
                        {
                            if (ds.Tables.Count > 1)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    foreach (DataColumn dc in ds.Tables[1].Columns)
                                    {
                                        if (EmailBody.Contains("[" + dc.ToString() + "]"))
                                        {
                                            EmailBody = EmailBody.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }

                    comm.Email_Users(EMAILID, EMAIL_CC, EmailSubject, EmailBody, EMAIL_BCC);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;

            }
        }

        protected void gridsigninfo_PreRender(object sender, EventArgs e)
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

        protected void btnSubmitDeleteSAE_Click(object sender, EventArgs e)
        {
            try
            {
                dal_SAE.SAE_DELETED_SP(
                    ACTION: "UPDATE_Downgrading_SAE_REQ",
                    SAEID: hdnSAEID.Value,
                    SAE: hdnSAE.Value,
                    STATUS: hdnSTATUS.Value,
                    REASON: txtDeleteReason.Text
                    );

                string MESSAGE = "Downgrading request of this SAE has been send to medical team. Once the medical team approve this request then this SAE automatically Deleted.";

                SendEmail(Request.QueryString["SAEID"].ToString(), hdnSTATUS.Value, "Generate Downgrading SAE Request");

                if (hdnSTATUS.Value == "Initial; Incomplete" || hdnSTATUS.Value == "Change Initial Report")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NSAE_LOGGED_EVENTS_LIST.aspx';", true);
                }
                else if (hdnSTATUS.Value == "Initial; Complete")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NSAE_LOGGED_EVENTS_LIST_Complete.aspx';", true);
                }
                else if (hdnSTATUS.Value.Contains("Follow"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NSAE_LOGGED_EVENTS_LIST_FollowUp.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnCancelDeleteSAE_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
        }

        protected void lbtnSupportingDocs_Click(object sender, EventArgs e)
        {
            SELECT_SUPPORT_DOC();
            ModalPopupExtender4.Show();
        }

        protected void BtnsubmitSupportingDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (UpldSupportingDoc.FileName != "")
                {
                    UploadSupportingDoc();
                    SELECT_SUPPORT_DOC();
                    Response.Write("<script> alert('Supporting Document Uploaded successfully ')</script>");
                    ModalPopupExtender4.Show();
                }
                else
                {
                    Response.Write("<script> alert('Please select supporting document file.')</script>");
                    ModalPopupExtender4.Show();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UploadSupportingDoc()
        {
            try
            {
                string fileName = UpldSupportingDoc.FileName;
                string contentType = UpldSupportingDoc.PostedFile.ContentType;
                byte[] fileData;
                using (Stream stream = UpldSupportingDoc.FileContent)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileData = ms.ToArray();
                    }
                }

                dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "INSERT_SUPPORT_DOC",
                    SAEID: hdnSAEID.Value,
                    FileName: fileName,
                    ContentType: contentType,
                    fileData: fileData,
                    Notes: txtNotes.Text
                    );
                Clear();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Clear()
        {
            txtNotes.Text = "";
        }

        protected void btnCancelSuportDoc_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl.ToString());
        }

        protected void grdSupport_Doc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            if (e.CommandName == "DeleteSupportDoc")
            {
                DeleteSupportDoc(ID);
                SELECT_SUPPORT_DOC();
                Response.Write("<script> alert('Supporting Document Deleted successfully ')</script>");
                ModalPopupExtender4.Show();
            }
            else if (e.CommandName == "DownloadSupportDoc")
            {
                IMPORT_SAE_SUPPORT_DOC(ID);
                Response.Write("<script> alert('Supporting Document Downloaded successfully ')</script>");
                ModalPopupExtender4.Show();
            }
        }

        private void IMPORT_SAE_SUPPORT_DOC(string ID)
        {
            try
            {
                string FileName, ContentType;
                byte[] fileData;
                DataSet ds = dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "IMPORT_SAE_SUPPORT_DOC",
                    ID: ID
                    );
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FileName = ds.Tables[0].Rows[0]["FILENAME"].ToString();
                        ContentType = ds.Tables[0].Rows[0]["CONTENTTYPE"].ToString();
                        fileData = (byte[])ds.Tables[0].Rows[0]["DATA"];
                        Response.Clear();
                        Response.ContentType = ContentType;
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

                        // Append cookie
                        HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                        cookie.Value = "Flag";
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);
                        // end

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

        private void DeleteSupportDoc(string ID)
        {
            try
            {
                dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "DELETE_SUPPORT_DOC",
                    ID: ID
                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_SUPPORT_DOC()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SUPPORTING_DOC_SP(ACTION: "SELECT_SAE_SUPPORT_DOCS", SAEID: hdnSAEID.Value);
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdSupport_Doc.DataSource = ds;
                    grdSupport_Doc.DataBind();
                    divgrdSupport_Doc.Visible = true;
                }
                else
                {
                    grdSupport_Doc.DataSource = null;
                    grdSupport_Doc.DataBind();
                    divgrdSupport_Doc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSupport_Doc_PreRender(object sender, EventArgs e)
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

        protected void grdSupport_Doc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lblDeleteSupportDoc = (e.Row.FindControl("lblDeleteSupportDoc") as LinkButton);
                    LinkButton lblDownloadSupportDoc = (e.Row.FindControl("lblDownloadSupportDoc") as LinkButton);
                    HiddenField hdnDeleted = (e.Row.FindControl("hdnDeleted") as HiddenField);

                    if (hdnDeleted.Value == "True")
                    {
                        lblDownloadSupportDoc.Visible = false;
                        lblDeleteSupportDoc.Visible = false;
                    }
                    else
                    {
                        lblDownloadSupportDoc.Visible = true;
                        lblDeleteSupportDoc.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}