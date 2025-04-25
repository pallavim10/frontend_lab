using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;

namespace CTMS
{
    public partial class NIWRS_ACTIONS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();
        CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();

        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["IWRS_CurrentDate"] == null)
                {
                    Session["IWRS_CurrentDate"] = cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy");
                }

                if (!IsPostBack)
                {
                    hfSTEPID.Value = Request.QueryString["STEPID"].ToString();

                    if (Request.QueryString["TYPE"] == null)
                    {
                        ACTIONS_FORM();
                    }
                    else
                    {
                        if (Request.QueryString["TYPE"] == "FIELD")
                        {
                            ACTIONS_FIELD();
                        }
                        else
                        {
                            ACTIONS_LIST();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ACTIONS_FIELD()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_ACTIONS_SP(ACTION: "DO_ACTION_FIELD", SUBJID: Request.QueryString["SUBJID"].ToString(), STEPID: Request.QueryString["STEPID"].ToString(), FIELDID: Request.QueryString["ID"].ToString(), CurrentDate: Session["IWRS_CurrentDate"].ToString());


                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["ApplVisit"].ToString() != "" && ds.Tables[0].Rows[0]["ApplVisit"].ToString() != "0")
                    {
                        UPDATE_APPLVISIT(Request.QueryString["SUBJID"].ToString(), ds.Tables[0].Rows[0]["ApplVisit"].ToString(), ds.Tables[0].Rows[0]["NextVisit"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["PERFORM"].ToString() != "" && ds.Tables[0].Rows[0]["PERFORM"].ToString() != "0")
                    {
                        if (ds.Tables[0].Rows[0]["PERFORM"].ToString() == "Randomization")
                        {
                            UPDATE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["PERFORM"].ToString() == "De-Randomization")
                        {
                            REMOVE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["PERFORM"].ToString() == "Dosing")
                        {
                            KIT_DISPENSE(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISIT"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString());

                            UPDATE_SUBJECT_DOSING(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString(), Request.QueryString["DOSEINDIC"].ToString());
                        }
                    }

                    if (ds.Tables[0].Rows[0]["SETFIELD"].ToString() != "")
                    {
                        SET_FIELD(ds.Tables[0].Rows[0]["SETFIELD"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["EVENTHIST"].ToString() != "")
                    {
                        EVENT_HISTORY(ds.Tables[0].Rows[0]["EVENTHIST"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["SEND_EMAIL"].ToString() == "True" && ds.Tables[0].Rows[0]["EMAILIDS"].ToString() != "" && ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "" && ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "")
                    {
                        SEND_EMAIL(
                            EmailIDs: ds.Tables[0].Rows[0]["EMAILIDS"].ToString(),
                            CcEmailIDs: ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString(),
                            BccEmailIDs: ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString(),
                            EmailSubject: ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString(),
                            EmailBody: ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString()
                            );
                    }

                    if (ds.Tables[0].Rows[0]["MSGBOX"].ToString() != "" && ds.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[0]["URL"].ToString() != "")
                        {
                            MSGBOX_NAVIGATE(ds.Tables[0].Rows[0]["MSGBOX"].ToString(), ds.Tables[0].Rows[0]["URL"].ToString());
                        }
                        else
                        {
                            MSGBOX_NAVIGATE(ds.Tables[0].Rows[0]["MSGBOX"].ToString(), "IWRS_Status_Dashboard.aspx");
                        }
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["MSGBOX"].ToString() != "")
                        {
                            MSGBOX(ds.Tables[0].Rows[0]["MSGBOX"].ToString());
                        }

                        if (ds.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                        {
                            if (ds.Tables[0].Rows[0]["URL"].ToString() != "")
                            {
                                NAVIGATE(ds.Tables[0].Rows[0]["URL"].ToString());
                            }
                            else
                            {
                                NAVIGATE("IWRS_Status_Dashboard.aspx");
                            }
                        }
                    }
                }
                else
                {
                    DataSet dsActs = dal_IWRS.IWRS_ACTIONS_SP(ACTION: "GET_DEFAULT_ACTIONS_FIELD", SUBJID: Request.QueryString["SUBJID"].ToString(), STEPID: Request.QueryString["STEPID"].ToString(), FIELDID: Request.QueryString["ID"].ToString());

                    if (dsActs.Tables[0].Rows.Count > 0)
                    {

                        if (dsActs.Tables[0].Rows[0]["ApplVisit"].ToString() != "" && dsActs.Tables[0].Rows[0]["ApplVisit"].ToString() != "0")
                        {
                            UPDATE_APPLVISIT(Request.QueryString["SUBJID"].ToString(), dsActs.Tables[0].Rows[0]["ApplVisit"].ToString(), dsActs.Tables[0].Rows[0]["NextVisit"].ToString());
                        }

                        if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() != "" && dsActs.Tables[0].Rows[0]["PERFORM"].ToString() != "0")
                        {
                            if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() == "Randomization")
                            {
                                UPDATE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                            }
                            else if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() == "De-Randomization")
                            {
                                REMOVE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                            }
                            else if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() == "Dosing")
                            {
                                KIT_DISPENSE(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISIT"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString());

                                UPDATE_SUBJECT_DOSING(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString(), Request.QueryString["DOSEINDIC"].ToString());
                            }
                        }

                        if (dsActs.Tables[0].Rows[0]["SETFIELD"].ToString() != "")
                        {
                            SET_FIELD(dsActs.Tables[0].Rows[0]["SETFIELD"].ToString());
                        }

                        if (dsActs.Tables[0].Rows[0]["EVENTHIST"].ToString() != "")
                        {
                            EVENT_HISTORY(dsActs.Tables[0].Rows[0]["EVENTHIST"].ToString());
                        }

                        if (dsActs.Tables[0].Rows[0]["SEND_EMAIL"].ToString() == "True" && dsActs.Tables[0].Rows[0]["EMAILIDS"].ToString() != "" && dsActs.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "" && dsActs.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "")
                        {
                            SEND_EMAIL(
                            EmailIDs: dsActs.Tables[0].Rows[0]["EMAILIDS"].ToString(),
                            CcEmailIDs: dsActs.Tables[0].Rows[0]["CCEMAILIDS"].ToString(),
                            BccEmailIDs: dsActs.Tables[0].Rows[0]["BCCEMAILIDS"].ToString(),
                            EmailSubject: dsActs.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString(),
                            EmailBody: dsActs.Tables[0].Rows[0]["EMAIL_BODY"].ToString()
                            );
                        }

                        if (dsActs.Tables[0].Rows[0]["MSGBOX"].ToString() != "" && dsActs.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && dsActs.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                        {
                            if (dsActs.Tables[0].Rows[0]["URL"].ToString() != "")
                            {
                                MSGBOX_NAVIGATE(dsActs.Tables[0].Rows[0]["MSGBOX"].ToString(), dsActs.Tables[0].Rows[0]["URL"].ToString());
                            }
                            else
                            {
                                MSGBOX_NAVIGATE(dsActs.Tables[0].Rows[0]["MSGBOX"].ToString(), "IWRS_Status_Dashboard.aspx");
                            }
                        }
                        else
                        {
                            if (dsActs.Tables[0].Rows[0]["MSGBOX"].ToString() != "")
                            {
                                MSGBOX(dsActs.Tables[0].Rows[0]["MSGBOX"].ToString());
                            }

                            if (dsActs.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && dsActs.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                            {
                                if (dsActs.Tables[0].Rows[0]["URL"].ToString() != "")
                                {
                                    NAVIGATE(dsActs.Tables[0].Rows[0]["URL"].ToString());
                                }
                                else
                                {
                                    NAVIGATE("IWRS_Status_Dashboard.aspx");
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ACTIONS_LIST()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_ACTIONS_SP(ACTION: "DO_ACTION_LIST", SUBJID: Request.QueryString["SUBJID"].ToString(), STEPID: Request.QueryString["STEPID"].ToString(), CurrentDate: Session["IWRS_CurrentDate"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["ApplVisit"].ToString() != "" && ds.Tables[0].Rows[0]["ApplVisit"].ToString() != "0")
                    {
                        UPDATE_APPLVISIT(Request.QueryString["SUBJID"].ToString(), ds.Tables[0].Rows[0]["ApplVisit"].ToString(), ds.Tables[0].Rows[0]["NextVisit"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["PERFORM"].ToString() != "" && ds.Tables[0].Rows[0]["PERFORM"].ToString() != "0")
                    {
                        if (ds.Tables[0].Rows[0]["PERFORM"].ToString() == "Randomization")
                        {
                            UPDATE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["PERFORM"].ToString() == "De-Randomization")
                        {
                            REMOVE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["PERFORM"].ToString() == "Dosing")
                        {
                            KIT_DISPENSE(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISIT"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString());

                            UPDATE_SUBJECT_DOSING(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString(), Request.QueryString["DOSEINDIC"].ToString());
                        }
                    }

                    if (ds.Tables[0].Rows[0]["SETFIELD"].ToString() != "")
                    {
                        SET_FIELD(ds.Tables[0].Rows[0]["SETFIELD"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["EVENTHIST"].ToString() != "")
                    {
                        EVENT_HISTORY(ds.Tables[0].Rows[0]["EVENTHIST"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["SEND_EMAIL"].ToString() == "True" && ds.Tables[0].Rows[0]["EMAILIDS"].ToString() != "" && ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "" && ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "")
                    {
                        SEND_EMAIL(
                            EmailIDs: ds.Tables[0].Rows[0]["EMAILIDS"].ToString(),
                            CcEmailIDs: ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString(),
                            BccEmailIDs: ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString(),
                            EmailSubject: ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString(),
                            EmailBody: ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString()
                            );
                    }

                    if (ds.Tables[0].Rows[0]["MSGBOX"].ToString() != "" && ds.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[0]["URL"].ToString() != "")
                        {
                            MSGBOX_NAVIGATE(ds.Tables[0].Rows[0]["MSGBOX"].ToString(), ds.Tables[0].Rows[0]["URL"].ToString());
                        }
                        else
                        {
                            MSGBOX_NAVIGATE(ds.Tables[0].Rows[0]["MSGBOX"].ToString(), "IWRS_Status_Dashboard.aspx");
                        }
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["MSGBOX"].ToString() != "")
                        {
                            MSGBOX(ds.Tables[0].Rows[0]["MSGBOX"].ToString());
                        }

                        if (ds.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                        {
                            if (ds.Tables[0].Rows[0]["URL"].ToString() != "")
                            {
                                NAVIGATE(ds.Tables[0].Rows[0]["URL"].ToString());
                            }
                            else
                            {
                                NAVIGATE("IWRS_Status_Dashboard.aspx");
                            }
                        }
                    }
                }
                else
                {
                    DataSet dsActs = dal_IWRS.IWRS_ACTIONS_SP(ACTION: "GET_DEFAULT_ACTIONS", STEPID: Request.QueryString["STEPID"].ToString(), SUBJID: Request.QueryString["SUBJID"].ToString());

                    if (dsActs.Tables[0].Rows.Count > 0)
                    {

                        if (dsActs.Tables[0].Rows[0]["ApplVisit"].ToString() != "" && dsActs.Tables[0].Rows[0]["ApplVisit"].ToString() != "0")
                        {
                            UPDATE_APPLVISIT(Request.QueryString["SUBJID"].ToString(), dsActs.Tables[0].Rows[0]["ApplVisit"].ToString(), dsActs.Tables[0].Rows[0]["NextVisit"].ToString());
                        }

                        if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() != "" && dsActs.Tables[0].Rows[0]["PERFORM"].ToString() != "0")
                        {
                            if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() == "Randomization")
                            {
                                UPDATE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                            }
                            else if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() == "De-Randomization")
                            {
                                REMOVE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                            }
                            else if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() == "Dosing")
                            {
                                KIT_DISPENSE(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISIT"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString());

                                UPDATE_SUBJECT_DOSING(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString(), Request.QueryString["DOSEINDIC"].ToString());
                            }
                        }

                        if (dsActs.Tables[0].Rows[0]["SETFIELD"].ToString() != "")
                        {
                            SET_FIELD(dsActs.Tables[0].Rows[0]["SETFIELD"].ToString());
                        }

                        if (dsActs.Tables[0].Rows[0]["EVENTHIST"].ToString() != "")
                        {
                            EVENT_HISTORY(dsActs.Tables[0].Rows[0]["EVENTHIST"].ToString());
                        }

                        if (dsActs.Tables[0].Rows[0]["SEND_EMAIL"].ToString() == "True" && dsActs.Tables[0].Rows[0]["EMAILIDS"].ToString() != "" && dsActs.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "" && dsActs.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "")
                        {
                            SEND_EMAIL(
                            EmailIDs: dsActs.Tables[0].Rows[0]["EMAILIDS"].ToString(),
                            CcEmailIDs: dsActs.Tables[0].Rows[0]["CCEMAILIDS"].ToString(),
                            BccEmailIDs: dsActs.Tables[0].Rows[0]["BCCEMAILIDS"].ToString(),
                            EmailSubject: dsActs.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString(),
                            EmailBody: dsActs.Tables[0].Rows[0]["EMAIL_BODY"].ToString()
                            );
                        }

                        if (dsActs.Tables[0].Rows[0]["MSGBOX"].ToString() != "" && dsActs.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && dsActs.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                        {
                            if (dsActs.Tables[0].Rows[0]["URL"].ToString() != "")
                            {
                                MSGBOX_NAVIGATE(dsActs.Tables[0].Rows[0]["MSGBOX"].ToString(), dsActs.Tables[0].Rows[0]["URL"].ToString());
                            }
                            else
                            {
                                MSGBOX_NAVIGATE(dsActs.Tables[0].Rows[0]["MSGBOX"].ToString(), "IWRS_Status_Dashboard.aspx");
                            }
                        }
                        else
                        {
                            if (dsActs.Tables[0].Rows[0]["MSGBOX"].ToString() != "")
                            {
                                MSGBOX(dsActs.Tables[0].Rows[0]["MSGBOX"].ToString());
                            }

                            if (dsActs.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && dsActs.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                            {
                                if (dsActs.Tables[0].Rows[0]["URL"].ToString() != "")
                                {
                                    NAVIGATE(dsActs.Tables[0].Rows[0]["URL"].ToString());
                                }
                                else
                                {
                                    NAVIGATE("IWRS_Status_Dashboard.aspx");
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ACTIONS_FORM()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_ACTIONS_SP(ACTION: "DO_ACTIONS_FORM", SUBJID: Request.QueryString["SUBJID"].ToString(), STEPID: Request.QueryString["STEPID"].ToString(), CurrentDate: Session["IWRS_CurrentDate"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ApplVisit"].ToString() != "" && ds.Tables[0].Rows[0]["ApplVisit"].ToString() != "0")
                    {
                        UPDATE_APPLVISIT(Request.QueryString["SUBJID"].ToString(), ds.Tables[0].Rows[0]["ApplVisit"].ToString(), ds.Tables[0].Rows[0]["NextVisit"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["PERFORM"].ToString() != "" && ds.Tables[0].Rows[0]["PERFORM"].ToString() != "0")
                    {
                        if (ds.Tables[0].Rows[0]["PERFORM"].ToString() == "Randomization")
                        {
                            UPDATE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["PERFORM"].ToString() == "De-Randomization")
                        {
                            REMOVE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["PERFORM"].ToString() == "Dosing")
                        {
                            KIT_DISPENSE(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISIT"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString());

                            UPDATE_SUBJECT_DOSING(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString(), Request.QueryString["DOSEINDIC"].ToString());
                        }
                    }

                    if (ds.Tables[0].Rows[0]["SETFIELD"].ToString() != "")
                    {
                        SET_FIELD(ds.Tables[0].Rows[0]["SETFIELD"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["EVENTHIST"].ToString() != "")
                    {
                        EVENT_HISTORY(ds.Tables[0].Rows[0]["EVENTHIST"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["SEND_EMAIL"].ToString() == "True" && ds.Tables[0].Rows[0]["EMAILIDS"].ToString() != "" && ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "" && ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "")
                    {
                        SEND_EMAIL(
                            EmailIDs: ds.Tables[0].Rows[0]["EMAILIDS"].ToString(),
                            CcEmailIDs: ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString(),
                            BccEmailIDs: ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString(),
                            EmailSubject: ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString(),
                            EmailBody: ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString()
                            );
                    }

                    bool STOP_CLAUSE = CHECK_StopClause(ds.Tables[0].Rows[0]["SOURCE_ID"].ToString());

                    if (STOP_CLAUSE)
                    {
                        if (ds.Tables[0].Rows[0]["MSGBOX"].ToString() != "" && ds.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                        {
                            if (ds.Tables[0].Rows[0]["URL"].ToString() != "")
                            {
                                MSGBOX_NAVIGATE(ds.Tables[0].Rows[0]["MSGBOX"].ToString(), ds.Tables[0].Rows[0]["URL"].ToString());
                            }
                            else
                            {
                                MSGBOX_NAVIGATE(ds.Tables[0].Rows[0]["MSGBOX"].ToString(), "IWRS_Status_Dashboard.aspx");
                            }
                        }
                        else
                        {
                            if (ds.Tables[0].Rows[0]["MSGBOX"].ToString() != "")
                            {
                                MSGBOX(ds.Tables[0].Rows[0]["MSGBOX"].ToString());
                            }

                            if (ds.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                            {
                                if (ds.Tables[0].Rows[0]["URL"].ToString() != "")
                                {
                                    NAVIGATE(ds.Tables[0].Rows[0]["URL"].ToString());
                                }
                                else
                                {
                                    NAVIGATE("IWRS_Status_Dashboard.aspx");
                                }
                            }
                        }

                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["MSGBOX"].ToString() != "")
                        {
                            MSGBOX(ds.Tables[0].Rows[0]["MSGBOX"].ToString());
                        }

                        if (hfSTOPCLAUSE_MSG.Value != "" && hfSTOPCLAUSE_URL.Value != "")
                        {
                            MSGBOX_NAVIGATE(hfSTOPCLAUSE_MSG.Value, hfSTOPCLAUSE_URL.Value);
                        }
                        else if (hfSTOPCLAUSE_MSG.Value != "")
                        {
                            MSGBOX(hfSTOPCLAUSE_MSG.Value);
                        }
                        else if (hfSTOPCLAUSE_URL.Value != "")
                        {
                            NAVIGATE(hfSTOPCLAUSE_URL.Value);
                        }
                        else
                        {
                            NAVIGATE("IWRS_Status_Dashboard.aspx");
                        }
                    }
                }
                else
                {
                    DataSet dsActs = dal_IWRS.IWRS_ACTIONS_SP(ACTION: "GET_DEFAULT_ACTIONS", STEPID: Request.QueryString["STEPID"].ToString(), SUBJID: Request.QueryString["SUBJID"].ToString());

                    if (dsActs.Tables[0].Rows.Count > 0)
                    {
                        if (dsActs.Tables[0].Rows[0]["ApplVisit"].ToString() != "" && dsActs.Tables[0].Rows[0]["ApplVisit"].ToString() != "0")
                        {
                            UPDATE_APPLVISIT(Request.QueryString["SUBJID"].ToString(), dsActs.Tables[0].Rows[0]["ApplVisit"].ToString(), dsActs.Tables[0].Rows[0]["NextVisit"].ToString());
                        }

                        if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() != "" && dsActs.Tables[0].Rows[0]["PERFORM"].ToString() != "0")
                        {
                            if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() == "Randomization")
                            {
                                UPDATE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                            }
                            else if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() == "De-Randomization")
                            {
                                REMOVE_RANDOMIZATION_DETAILS(Request.QueryString["SUBJID"].ToString());
                            }
                            else if (dsActs.Tables[0].Rows[0]["PERFORM"].ToString() == "Dosing")
                            {
                                KIT_DISPENSE(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISIT"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString());

                                UPDATE_SUBJECT_DOSING(Request.QueryString["SUBJID"].ToString(), Request.QueryString["DOSEVISITNUM"].ToString(), Request.QueryString["DOSEINDIC"].ToString());
                            }
                        }

                        if (dsActs.Tables[0].Rows[0]["SETFIELD"].ToString() != "")
                        {
                            SET_FIELD(dsActs.Tables[0].Rows[0]["SETFIELD"].ToString());
                        }

                        if (dsActs.Tables[0].Rows[0]["EVENTHIST"].ToString() != "")
                        {
                            EVENT_HISTORY(dsActs.Tables[0].Rows[0]["EVENTHIST"].ToString());
                        }

                        if (dsActs.Tables[0].Rows[0]["SEND_EMAIL"].ToString() == "True" && dsActs.Tables[0].Rows[0]["EMAILIDS"].ToString() != "" && dsActs.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "" && dsActs.Tables[0].Rows[0]["EMAIL_BODY"].ToString() != "")
                        {
                            SEND_EMAIL(
                            EmailIDs: dsActs.Tables[0].Rows[0]["EMAILIDS"].ToString(),
                            CcEmailIDs: dsActs.Tables[0].Rows[0]["CCEMAILIDS"].ToString(),
                            BccEmailIDs: dsActs.Tables[0].Rows[0]["BCCEMAILIDS"].ToString(),
                            EmailSubject: dsActs.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString(),
                            EmailBody: dsActs.Tables[0].Rows[0]["EMAIL_BODY"].ToString()
                            );
                        }

                        bool STOP_CLAUSE = CHECK_StopClause(dsActs.Tables[0].Rows[0]["SOURCE_ID"].ToString());

                        if (STOP_CLAUSE)
                        {
                            if (dsActs.Tables[0].Rows[0]["MSGBOX"].ToString() != "" && dsActs.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && dsActs.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                            {
                                if (dsActs.Tables[0].Rows[0]["URL"].ToString() != "")
                                {
                                    MSGBOX_NAVIGATE(dsActs.Tables[0].Rows[0]["MSGBOX"].ToString(), dsActs.Tables[0].Rows[0]["URL"].ToString());
                                }
                                else
                                {
                                    MSGBOX_NAVIGATE(dsActs.Tables[0].Rows[0]["MSGBOX"].ToString(), "IWRS_Status_Dashboard.aspx");
                                }
                            }
                            else
                            {
                                if (dsActs.Tables[0].Rows[0]["MSGBOX"].ToString() != "")
                                {
                                    MSGBOX(dsActs.Tables[0].Rows[0]["MSGBOX"].ToString());
                                }

                                if (dsActs.Tables[0].Rows[0]["NAVTO_TYPE"].ToString() != "" && dsActs.Tables[0].Rows[0]["NAVTO"].ToString() != "")
                                {
                                    if (dsActs.Tables[0].Rows[0]["URL"].ToString() != "")
                                    {
                                        NAVIGATE(dsActs.Tables[0].Rows[0]["URL"].ToString());
                                    }
                                    else
                                    {
                                        NAVIGATE("IWRS_Status_Dashboard.aspx");
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dsActs.Tables[0].Rows[0]["MSGBOX"].ToString() != "")
                            {
                                MSGBOX(dsActs.Tables[0].Rows[0]["MSGBOX"].ToString());
                            }

                            if (hfSTOPCLAUSE_MSG.Value != "" && hfSTOPCLAUSE_URL.Value != "")
                            {
                                MSGBOX_NAVIGATE(hfSTOPCLAUSE_MSG.Value, hfSTOPCLAUSE_URL.Value);
                            }
                            else if (hfSTOPCLAUSE_MSG.Value != "")
                            {
                                MSGBOX(hfSTOPCLAUSE_MSG.Value);
                            }
                            else if (hfSTOPCLAUSE_URL.Value != "")
                            {
                                NAVIGATE(hfSTOPCLAUSE_URL.Value);
                            }
                            else
                            {
                                NAVIGATE("IWRS_Status_Dashboard.aspx");
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SET_FIELD(string FIELDS)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_GET_SUBJECT_DETAILS_SP(ACTION: "GET_SUBJECT_DETAILS", SUBJID: Request.QueryString["SUBJID"].ToString());

                if (FIELDS.Contains("[") && FIELDS.Contains("]"))
                {
                    if (FIELDS.Contains("[SUBJID]"))
                    {
                        FIELDS = FIELDS.Replace("[SUBJID]", ds.Tables[0].Rows[0]["SUBJID"].ToString());
                    }

                    if (FIELDS.Contains("[SITEID]"))
                    {
                        FIELDS = FIELDS.Replace("[SITEID]", ds.Tables[0].Rows[0]["SITEID"].ToString());
                    }

                    if (FIELDS.Contains("[SUBSITEID]"))
                    {
                        FIELDS = FIELDS.Replace("[SUBSITEID]", ds.Tables[0].Rows[0]["SUBSITEID"].ToString());
                    }

                    if (FIELDS.Contains("[RANDNO]"))
                    {
                        FIELDS = FIELDS.Replace("[RANDNO]", ds.Tables[0].Rows[0]["RAND_NUM"].ToString());
                    }

                    if (FIELDS.Contains("[INDICATION]"))
                    {
                        FIELDS = FIELDS.Replace("[INDICATION]", ds.Tables[0].Rows[0]["INDICATION"].ToString());
                    }

                    if (FIELDS.Contains("[TGID]"))
                    {
                        FIELDS = FIELDS.Replace("[TGID]", ds.Tables[0].Rows[0]["TREAT_GRP"].ToString());
                    }

                    if (FIELDS.Contains("[TGNAME]"))
                    {
                        FIELDS = FIELDS.Replace("[TGNAME]", ds.Tables[0].Rows[0]["TREAT_GRP_NAME"].ToString());
                    }

                    if (FIELDS.Contains("[KITNO]"))
                    {
                        string KITNOs = "";

                        foreach (DataRow drKITNO in ds.Tables[0].Rows)
                        {
                            if (KITNOs != "")
                            {
                                if (!KITNOs.Contains(drKITNO["KITNO"].ToString()))
                                {
                                    KITNOs += ", " + drKITNO["KITNO"].ToString();
                                }
                            }
                            else
                            {
                                KITNOs = drKITNO["KITNO"].ToString();
                            }
                        }

                        FIELDS = FIELDS.Replace("[KITNO]", KITNOs);
                    }

                    if (FIELDS.Contains("[EARLYRANDDATE]"))
                    {
                        FIELDS = FIELDS.Replace("[EARLYRANDDATE]", ds.Tables[0].Rows[0]["EARLY_RAND_BY"].ToString());
                    }

                    if (FIELDS.Contains("[RANDDATE]"))
                    {
                        FIELDS = FIELDS.Replace("[RANDDATE]", ds.Tables[0].Rows[0]["RAND_BY"].ToString());
                    }

                    if (FIELDS.Contains("[LATERANDDATE]"))
                    {
                        FIELDS = FIELDS.Replace("[LATERANDDATE]", ds.Tables[0].Rows[0]["LATE_RAND_BY"].ToString());
                    }

                    if (FIELDS.Contains("[LASTVISIT]"))
                    {
                        FIELDS = FIELDS.Replace("[LASTVISIT]", ds.Tables[0].Rows[0]["LAST_VISIT"].ToString());
                    }

                    if (FIELDS.Contains("[LASTVISITDATE]"))
                    {
                        FIELDS = FIELDS.Replace("[LASTVISITDATE]", ds.Tables[0].Rows[0]["LAST_VISIT_DATE"].ToString());
                    }

                    if (FIELDS.Contains("[NEXTVISIT]"))
                    {
                        FIELDS = FIELDS.Replace("[NEXTVISIT]", ds.Tables[0].Rows[0]["NEXT_VISIT"].ToString());
                    }

                    if (FIELDS.Contains("[EARLYVISITDATE]"))
                    {
                        FIELDS = FIELDS.Replace("[EARLYVISITDATE]", ds.Tables[0].Rows[0]["EARLY_DATE"].ToString());
                    }

                    if (FIELDS.Contains("[VISITDATE]"))
                    {
                        FIELDS = FIELDS.Replace("[VISITDATE]", ds.Tables[0].Rows[0]["NEXT_VISIT_DATE"].ToString());
                    }

                    if (FIELDS.Contains("[LATEVISITDATE]"))
                    {
                        FIELDS = FIELDS.Replace("[LATEVISITDATE]", ds.Tables[0].Rows[0]["LATE_DATE"].ToString());
                    }

                    if (FIELDS.Contains("[USER]"))
                    {
                        FIELDS = FIELDS.Replace("[USER]", Session["User_Name"].ToString());
                    }

                    if (FIELDS.Contains("[DATETIME]"))
                    {
                        FIELDS = FIELDS.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    }

                    if (FIELDS.Contains("[") && FIELDS.Contains("]"))
                    {
                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataColumn dc in ds.Tables[1].Columns)
                                {
                                    if (FIELDS.Contains("[" + dc.ToString() + "]"))
                                    {
                                        FIELDS = FIELDS.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                    }
                                }
                            }
                        }
                    }
                }

                dal_IWRS.IWRS_ACTIONS_SP(ACTION: "ACTION_SET_FIELD", SUBJID: Request.QueryString["SUBJID"].ToString(), INSERTQUERY: FIELDS);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void MSGBOX(string MSG)
        {
            try
            {
                if (MSG.Contains("[") && MSG.Contains("]"))
                {
                    DataSet ds = dal_IWRS.IWRS_GET_SUBJECT_DETAILS_SP(ACTION: "GET_SUBJECT_DETAILS", SUBJID: Request.QueryString["SUBJID"].ToString());

                    if (MSG.Contains("[SUBJID]"))
                    {
                        MSG = MSG.Replace("[SUBJID]", ds.Tables[0].Rows[0]["SUBJID"].ToString());
                    }

                    if (MSG.Contains("[SITEID]"))
                    {
                        MSG = MSG.Replace("[SITEID]", ds.Tables[0].Rows[0]["SITEID"].ToString());
                    }

                    if (MSG.Contains("[SUBSITEID]"))
                    {
                        MSG = MSG.Replace("[SUBSITEID]", ds.Tables[0].Rows[0]["SUBSITEID"].ToString());
                    }

                    if (MSG.Contains("[RANDNO]"))
                    {
                        MSG = MSG.Replace("[RANDNO]", ds.Tables[0].Rows[0]["RAND_NUM"].ToString());
                    }

                    if (MSG.Contains("[INDICATION]"))
                    {
                        MSG = MSG.Replace("[INDICATION]", ds.Tables[0].Rows[0]["INDICATION"].ToString());
                    }

                    if (MSG.Contains("[KITNO]"))
                    {
                        string KITNOs = "";

                        foreach (DataRow drKITNO in ds.Tables[0].Rows)
                        {
                            if (KITNOs != "")
                            {
                                if (!KITNOs.Contains(drKITNO["KITNO"].ToString()))
                                {
                                    KITNOs += ", " + drKITNO["KITNO"].ToString();
                                }
                            }
                            else
                            {
                                KITNOs = drKITNO["KITNO"].ToString();
                            }
                        }

                        MSG = MSG.Replace("[KITNO]", KITNOs);
                    }

                    if (MSG.Contains("[TGID]"))
                    {
                        MSG = MSG.Replace("[TGID]", ds.Tables[0].Rows[0]["TREAT_GRP"].ToString());
                    }

                    if (MSG.Contains("[TGNAME]"))
                    {
                        MSG = MSG.Replace("[TGNAME]", ds.Tables[0].Rows[0]["TREAT_GRP_NAME"].ToString());
                    }

                    if (MSG.Contains("[EARLYRANDDATE]"))
                    {
                        MSG = MSG.Replace("[EARLYRANDDATE]", ds.Tables[0].Rows[0]["EARLY_RAND_BY"].ToString());
                    }

                    if (MSG.Contains("[RANDDATE]"))
                    {
                        MSG = MSG.Replace("[RANDDATE]", ds.Tables[0].Rows[0]["RAND_BY"].ToString());
                    }

                    if (MSG.Contains("[LATERANDDATE]"))
                    {
                        MSG = MSG.Replace("[LATERANDDATE]", ds.Tables[0].Rows[0]["LATE_RAND_BY"].ToString());
                    }

                    if (MSG.Contains("[LASTVISIT]"))
                    {
                        MSG = MSG.Replace("[LASTVISIT]", ds.Tables[0].Rows[0]["LAST_VISIT"].ToString());
                    }

                    if (MSG.Contains("[LASTVISITDATE]"))
                    {
                        MSG = MSG.Replace("[LASTVISITDATE]", ds.Tables[0].Rows[0]["LAST_VISIT_DATE"].ToString());
                    }

                    if (MSG.Contains("[NEXTVISIT]"))
                    {
                        MSG = MSG.Replace("[NEXTVISIT]", ds.Tables[0].Rows[0]["NEXT_VISIT"].ToString());
                    }

                    if (MSG.Contains("[EARLYVISITDATE]"))
                    {
                        MSG = MSG.Replace("[EARLYVISITDATE]", ds.Tables[0].Rows[0]["EARLY_DATE"].ToString());
                    }

                    if (MSG.Contains("[VISITDATE]"))
                    {
                        MSG = MSG.Replace("[VISITDATE]", ds.Tables[0].Rows[0]["NEXT_VISIT_DATE"].ToString());
                    }

                    if (MSG.Contains("[LATEVISITDATE]"))
                    {
                        MSG = MSG.Replace("[LATEVISITDATE]", ds.Tables[0].Rows[0]["LATE_DATE"].ToString());
                    }

                    if (MSG.Contains("[USER]"))
                    {
                        MSG = MSG.Replace("[USER]", Session["User_Name"].ToString());
                    }

                    if (MSG.Contains("[DATETIME]"))
                    {
                        MSG = MSG.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    }

                    if (MSG.Contains("[") && MSG.Contains("]"))
                    {
                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataColumn dc in ds.Tables[1].Columns)
                                {
                                    if (MSG.Contains("[" + dc.ToString() + "]"))
                                    {
                                        MSG = MSG.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                    }
                                }
                            }
                        }
                    }
                }

                Response.Write("<script> alert('" + MSG + "');</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void MSGBOX_NAVIGATE(string MSG, string URL)
        {
            try
            {
                if (MSG.Contains("[") && MSG.Contains("]"))
                {
                    DataSet ds = dal_IWRS.IWRS_GET_SUBJECT_DETAILS_SP(ACTION: "GET_SUBJECT_DETAILS", SUBJID: Request.QueryString["SUBJID"].ToString());

                    if (MSG.Contains("[SUBJID]"))
                    {
                        MSG = MSG.Replace("[SUBJID]", ds.Tables[0].Rows[0]["SUBJID"].ToString());
                    }

                    if (MSG.Contains("[SITEID]"))
                    {
                        MSG = MSG.Replace("[SITEID]", ds.Tables[0].Rows[0]["SITEID"].ToString());
                    }

                    if (MSG.Contains("[SUBSITEID]"))
                    {
                        MSG = MSG.Replace("[SUBSITEID]", ds.Tables[0].Rows[0]["SUBSITEID"].ToString());
                    }

                    if (MSG.Contains("[RANDNO]"))
                    {
                        MSG = MSG.Replace("[RANDNO]", ds.Tables[0].Rows[0]["RAND_NUM"].ToString());
                    }

                    if (MSG.Contains("[INDICATION]"))
                    {
                        MSG = MSG.Replace("[INDICATION]", ds.Tables[0].Rows[0]["INDICATION"].ToString());
                    }

                    if (MSG.Contains("[KITNO]"))
                    {
                        string KITNOs = "";

                        foreach (DataRow drKITNO in ds.Tables[0].Rows)
                        {
                            if (KITNOs != "")
                            {
                                if (!KITNOs.Contains(drKITNO["KITNO"].ToString()))
                                {
                                    KITNOs += ", " + drKITNO["KITNO"].ToString();
                                }
                            }
                            else
                            {
                                KITNOs = drKITNO["KITNO"].ToString();
                            }
                        }

                        MSG = MSG.Replace("[KITNO]", KITNOs);
                    }

                    if (MSG.Contains("[TGID]"))
                    {
                        MSG = MSG.Replace("[TGID]", ds.Tables[0].Rows[0]["TREAT_GRP"].ToString());
                    }

                    if (MSG.Contains("[TGNAME]"))
                    {
                        MSG = MSG.Replace("[TGNAME]", ds.Tables[0].Rows[0]["TREAT_GRP_NAME"].ToString());
                    }

                    if (MSG.Contains("[EARLYRANDDATE]"))
                    {
                        MSG = MSG.Replace("[EARLYRANDDATE]", ds.Tables[0].Rows[0]["EARLY_RAND_BY"].ToString());
                    }

                    if (MSG.Contains("[RANDDATE]"))
                    {
                        MSG = MSG.Replace("[RANDDATE]", ds.Tables[0].Rows[0]["RAND_BY"].ToString());
                    }

                    if (MSG.Contains("[LATERANDDATE]"))
                    {
                        MSG = MSG.Replace("[LATERANDDATE]", ds.Tables[0].Rows[0]["LATE_RAND_BY"].ToString());
                    }

                    if (MSG.Contains("[LASTVISIT]"))
                    {
                        MSG = MSG.Replace("[LASTVISIT]", ds.Tables[0].Rows[0]["LAST_VISIT"].ToString());
                    }

                    if (MSG.Contains("[LASTVISITDATE]"))
                    {
                        MSG = MSG.Replace("[LASTVISITDATE]", ds.Tables[0].Rows[0]["LAST_VISIT_DATE"].ToString());
                    }

                    if (MSG.Contains("[NEXTVISIT]"))
                    {
                        MSG = MSG.Replace("[NEXTVISIT]", ds.Tables[0].Rows[0]["NEXT_VISIT"].ToString());
                    }

                    if (MSG.Contains("[EARLYVISITDATE]"))
                    {
                        MSG = MSG.Replace("[EARLYVISITDATE]", ds.Tables[0].Rows[0]["EARLY_DATE"].ToString());
                    }

                    if (MSG.Contains("[VISITDATE]"))
                    {
                        MSG = MSG.Replace("[VISITDATE]", ds.Tables[0].Rows[0]["NEXT_VISIT_DATE"].ToString());
                    }

                    if (MSG.Contains("[LATEVISITDATE]"))
                    {
                        MSG = MSG.Replace("[LATEVISITDATE]", ds.Tables[0].Rows[0]["LATE_DATE"].ToString());
                    }

                    if (MSG.Contains("[USER]"))
                    {
                        MSG = MSG.Replace("[USER]", Session["User_Name"].ToString());
                    }

                    if (MSG.Contains("[DATETIME]"))
                    {
                        MSG = MSG.Replace("[DATETIME]", commFun.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                    }

                    if (MSG.Contains("[") && MSG.Contains("]"))
                    {
                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataColumn dc in ds.Tables[1].Columns)
                                {
                                    if (MSG.Contains("[" + dc.ToString() + "]"))
                                    {
                                        MSG = MSG.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                    }
                                }
                            }
                        }
                    }
                }

                if (URL != "IWRS_Status_Dashboard.aspx" && !URL.Contains("_LIST"))
                {
                    URL = URL + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "";
                    Response.Write("<script> alert('" + MSG + "'); window.location.href = '" + URL + "'; </script>");
                }
                else
                {
                    Response.Write("<script> alert('" + MSG + "'); window.location.href = '" + URL + "'; </script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void NAVIGATE(string URL)
        {
            try
            {

                if (URL != "IWRS_Status_Dashboard.aspx" && !URL.Contains("_LIST"))
                {
                    URL = URL + "&SUBJID=" + Request.QueryString["SUBJID"].ToString() + "";
                    Response.Redirect(URL, false);
                }
                else
                {
                    Response.Redirect(URL, false);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EVENT_HISTORY(string EVENT)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_GET_SUBJECT_DETAILS_SP(ACTION: "GET_SUBJECT_DETAILS", SUBJID: Request.QueryString["SUBJID"].ToString());

                if (EVENT.Contains("[") && EVENT.Contains("]"))
                {
                    if (EVENT.Contains("[SUBJID]"))
                    {
                        EVENT = EVENT.Replace("[SUBJID]", ds.Tables[0].Rows[0]["SUBJID"].ToString());
                    }

                    if (EVENT.Contains("[SITEID]"))
                    {
                        EVENT = EVENT.Replace("[SITEID]", ds.Tables[0].Rows[0]["SITEID"].ToString());
                    }

                    if (EVENT.Contains("[SUBSITEID]"))
                    {
                        EVENT = EVENT.Replace("[SUBSITEID]", ds.Tables[0].Rows[0]["SUBSITEID"].ToString());
                    }

                    if (EVENT.Contains("[RANDNO]"))
                    {
                        EVENT = EVENT.Replace("[RANDNO]", ds.Tables[0].Rows[0]["RAND_NUM"].ToString());
                    }

                    if (EVENT.Contains("[INDICATION]"))
                    {
                        EVENT = EVENT.Replace("[INDICATION]", ds.Tables[0].Rows[0]["INDICATION"].ToString());
                    }

                    if (EVENT.Contains("[TGID]"))
                    {
                        EVENT = EVENT.Replace("[TGID]", ds.Tables[0].Rows[0]["TREAT_GRP"].ToString());
                    }

                    if (EVENT.Contains("[TGNAME]"))
                    {
                        EVENT = EVENT.Replace("[TGNAME]", ds.Tables[0].Rows[0]["TREAT_GRP_NAME"].ToString());
                    }

                    if (EVENT.Contains("[KITNO]"))
                    {
                        string KITNOs = "";

                        foreach (DataRow drKITNO in ds.Tables[0].Rows)
                        {
                            if (KITNOs != "")
                            {
                                if (!KITNOs.Contains(drKITNO["KITNO"].ToString()))
                                {
                                    KITNOs += ", " + drKITNO["KITNO"].ToString();
                                }
                            }
                            else
                            {
                                KITNOs = drKITNO["KITNO"].ToString();
                            }
                        }

                        EVENT = EVENT.Replace("[KITNO]", KITNOs);
                    }

                    if (EVENT.Contains("[EARLYRANDDATE]"))
                    {
                        EVENT = EVENT.Replace("[EARLYRANDDATE]", ds.Tables[0].Rows[0]["EARLY_RAND_BY"].ToString());
                    }

                    if (EVENT.Contains("[RANDDATE]"))
                    {
                        EVENT = EVENT.Replace("[RANDDATE]", ds.Tables[0].Rows[0]["RAND_BY"].ToString());
                    }

                    if (EVENT.Contains("[LATERANDDATE]"))
                    {
                        EVENT = EVENT.Replace("[LATERANDDATE]", ds.Tables[0].Rows[0]["LATE_RAND_BY"].ToString());
                    }

                    if (EVENT.Contains("[LASTVISIT]"))
                    {
                        EVENT = EVENT.Replace("[LASTVISIT]", ds.Tables[0].Rows[0]["LAST_VISIT"].ToString());
                    }

                    if (EVENT.Contains("[LASTVISITDATE]"))
                    {
                        EVENT = EVENT.Replace("[LASTVISITDATE]", ds.Tables[0].Rows[0]["LAST_VISIT_DATE"].ToString());
                    }

                    if (EVENT.Contains("[NEXTVISIT]"))
                    {
                        EVENT = EVENT.Replace("[NEXTVISIT]", ds.Tables[0].Rows[0]["NEXT_VISIT"].ToString());
                    }

                    if (EVENT.Contains("[EARLYVISITDATE]"))
                    {
                        EVENT = EVENT.Replace("[EARLYVISITDATE]", ds.Tables[0].Rows[0]["EARLY_DATE"].ToString());
                    }

                    if (EVENT.Contains("[VISITDATE]"))
                    {
                        EVENT = EVENT.Replace("[VISITDATE]", ds.Tables[0].Rows[0]["NEXT_VISIT_DATE"].ToString());
                    }

                    if (EVENT.Contains("[LATEVISITDATE]"))
                    {
                        EVENT = EVENT.Replace("[LATEVISITDATE]", ds.Tables[0].Rows[0]["LATE_DATE"].ToString());
                    }

                    if (EVENT.Contains("[USER]"))
                    {
                        EVENT = EVENT.Replace("[USER]", Session["User_Name"].ToString());
                    }

                    if (EVENT.Contains("[DATETIME]"))
                    {
                        EVENT = EVENT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    }

                    if (EVENT.Contains("[") && EVENT.Contains("]"))
                    {
                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataColumn dc in ds.Tables[1].Columns)
                                {
                                    if (EVENT.Contains("[" + dc.ToString() + "]"))
                                    {
                                        EVENT = EVENT.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                    }
                                }
                            }
                        }
                    }
                }

                dal_IWRS.IWRS_LOG_SP(ACTION: "EVENT_HISTORY", ERR_MSG: EVENT, SUBJID: Request.QueryString["SUBJID"].ToString(), SITEID: ds.Tables[0].Rows[0]["SITEID"].ToString(), SUBSITEID: ds.Tables[0].Rows[0]["SITEID"].ToString(), ENTEREDBY: Session["USER_ID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_EMAIL(string EmailIDs, string CcEmailIDs, string BccEmailIDs, string EmailSubject, string EmailBody)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_GET_SUBJECT_DETAILS_SP(ACTION: "GET_SUBJECT_DETAILS", SUBJID: Request.QueryString["SUBJID"].ToString());

                if (EmailSubject.Contains("[") && EmailSubject.Contains("]"))
                {
                    if (EmailSubject.Contains("[SUBJID]"))
                    {
                        EmailSubject = EmailSubject.Replace("[SUBJID]", ds.Tables[0].Rows[0]["SUBJID"].ToString());
                    }

                    if (EmailSubject.Contains("[SITEID]"))
                    {
                        EmailSubject = EmailSubject.Replace("[SITEID]", ds.Tables[0].Rows[0]["SITEID"].ToString());
                    }

                    if (EmailSubject.Contains("[SUBSITEID]"))
                    {
                        EmailSubject = EmailSubject.Replace("[SUBSITEID]", ds.Tables[0].Rows[0]["SUBSITEID"].ToString());
                    }

                    if (EmailSubject.Contains("[RANDNO]"))
                    {
                        EmailSubject = EmailSubject.Replace("[RANDNO]", ds.Tables[0].Rows[0]["RAND_NUM"].ToString());
                    }

                    if (EmailSubject.Contains("[INDICATION]"))
                    {
                        EmailSubject = EmailSubject.Replace("[INDICATION]", ds.Tables[0].Rows[0]["INDICATION"].ToString());
                    }

                    if (EmailSubject.Contains("[TGID]"))
                    {
                        EmailSubject = EmailSubject.Replace("[TGID]", ds.Tables[0].Rows[0]["TREAT_GRP"].ToString());
                    }

                    if (EmailSubject.Contains("[TGNAME]"))
                    {
                        EmailSubject = EmailSubject.Replace("[TGNAME]", ds.Tables[0].Rows[0]["TREAT_GRP_NAME"].ToString());
                    }

                    if (EmailSubject.Contains("[KITNO]"))
                    {
                        string KITNOs = "";

                        foreach (DataRow drKITNO in ds.Tables[0].Rows)
                        {
                            if (KITNOs != "")
                            {
                                if (!KITNOs.Contains(drKITNO["KITNO"].ToString()))
                                {
                                    KITNOs += ", " + drKITNO["KITNO"].ToString();
                                }
                            }
                            else
                            {
                                KITNOs = drKITNO["KITNO"].ToString();
                            }
                        }

                        EmailSubject = EmailSubject.Replace("[KITNO]", KITNOs);
                    }

                    if (EmailSubject.Contains("[USER]"))
                    {
                        EmailSubject = EmailSubject.Replace("[USER]", Session["User_Name"].ToString());
                    }

                    if (EmailSubject.Contains("[DATETIME]"))
                    {
                        EmailSubject = EmailSubject.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
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
                        EmailBody = EmailBody.Replace("[SUBJID]", ds.Tables[0].Rows[0]["SUBJID"].ToString());
                    }

                    if (EmailBody.Contains("[SITEID]"))
                    {
                        EmailBody = EmailBody.Replace("[SITEID]", ds.Tables[0].Rows[0]["SITEID"].ToString());
                    }

                    if (EmailBody.Contains("[SUBSITEID]"))
                    {
                        EmailBody = EmailBody.Replace("[SUBSITEID]", ds.Tables[0].Rows[0]["SUBSITEID"].ToString());
                    }

                    if (EmailBody.Contains("[RANDNO]"))
                    {
                        EmailBody = EmailBody.Replace("[RANDNO]", ds.Tables[0].Rows[0]["RAND_NUM"].ToString());
                    }

                    if (EmailBody.Contains("[INDICATION]"))
                    {
                        EmailBody = EmailBody.Replace("[INDICATION]", ds.Tables[0].Rows[0]["INDICATION"].ToString());
                    }

                    if (EmailBody.Contains("[TGID]"))
                    {
                        EmailBody = EmailBody.Replace("[TGID]", ds.Tables[0].Rows[0]["TREAT_GRP"].ToString());
                    }

                    if (EmailBody.Contains("[TGNAME]"))
                    {
                        EmailBody = EmailBody.Replace("[TGNAME]", ds.Tables[0].Rows[0]["TREAT_GRP_NAME"].ToString());
                    }

                    if (EmailBody.Contains("[KITNO]"))
                    {
                        string KITNOs = "";

                        foreach (DataRow drKITNO in ds.Tables[0].Rows)
                        {
                            if (KITNOs != "")
                            {
                                if (!KITNOs.Contains(drKITNO["KITNO"].ToString()))
                                {
                                    KITNOs += ", " + drKITNO["KITNO"].ToString();
                                }
                            }
                            else
                            {
                                KITNOs = drKITNO["KITNO"].ToString();
                            }
                        }

                        EmailBody = EmailBody.Replace("[KITNO]", KITNOs);
                    }

                    if (EmailBody.Contains("[EARLYRANDDATE]"))
                    {
                        EmailBody = EmailBody.Replace("[EARLYRANDDATE]", ds.Tables[0].Rows[0]["EARLY_RAND_BY"].ToString());
                    }

                    if (EmailBody.Contains("[RANDDATE]"))
                    {
                        EmailBody = EmailBody.Replace("[RANDDATE]", ds.Tables[0].Rows[0]["RAND_BY"].ToString());
                    }

                    if (EmailBody.Contains("[LATERANDDATE]"))
                    {
                        EmailBody = EmailBody.Replace("[LATERANDDATE]", ds.Tables[0].Rows[0]["LATE_RAND_BY"].ToString());
                    }

                    if (EmailBody.Contains("[LASTVISIT]"))
                    {
                        EmailBody = EmailBody.Replace("[LASTVISIT]", ds.Tables[0].Rows[0]["LAST_VISIT"].ToString());
                    }

                    if (EmailBody.Contains("[LASTVISITDATE]"))
                    {
                        EmailBody = EmailBody.Replace("[LASTVISITDATE]", ds.Tables[0].Rows[0]["LAST_VISIT_DATE"].ToString());
                    }

                    if (EmailBody.Contains("[NEXTVISIT]"))
                    {
                        EmailBody = EmailBody.Replace("[NEXTVISIT]", ds.Tables[0].Rows[0]["NEXT_VISIT"].ToString());
                    }

                    if (EmailBody.Contains("[EARLYVISITDATE]"))
                    {
                        EmailBody = EmailBody.Replace("[EARLYVISITDATE]", ds.Tables[0].Rows[0]["EARLY_DATE"].ToString());
                    }

                    if (EmailBody.Contains("[VISITDATE]"))
                    {
                        EmailBody = EmailBody.Replace("[VISITDATE]", ds.Tables[0].Rows[0]["NEXT_VISIT_DATE"].ToString());
                    }

                    if (EmailBody.Contains("[LATEVISITDATE]"))
                    {
                        EmailBody = EmailBody.Replace("[LATEVISITDATE]", ds.Tables[0].Rows[0]["LATE_DATE"].ToString());
                    }

                    if (EmailBody.Contains("[USER]"))
                    {
                        EmailBody = EmailBody.Replace("[USER]", Session["User_Name"].ToString());
                    }

                    if (EmailBody.Contains("[DATETIME]"))
                    {
                        EmailBody = EmailBody.Replace("[DATETIME]", commFun.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
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

                cf.Email_Users(
                EmailAddress: EmailIDs,
                CCEmailAddress: CcEmailIDs,
                BCCEmailAddress: BccEmailIDs,
                subject: EmailSubject,
                body: EmailBody
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_RANDOMIZATION_DETAILS(string SUBJID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_AVL_SP(ACTION: "GET_NEW_RANDO_NO", SUBJID: SUBJID);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["RANDNO"].ToString() == "" || ds.Tables[0].Rows[0]["RANDNO"].ToString() == "0")
                    {
                        Response.Write("<script> alert('Randomization number not available'); window.location.href = 'IWRS_Status_Dashboard.aspx'; </script>");
                    }
                    else
                    {
                        dal_IWRS.IWRS_ACTIONS_SP(
                        ACTION: "UPDATE_RANDOMIZATION_DETAILS",
                        STEPID: hfSTEPID.Value,
                        SUBJID: SUBJID,
                        RANDNO: ds.Tables[0].Rows[0]["RANDNO"].ToString(),
                        CurrentDate: Session["IWRS_CurrentDate"].ToString()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void REMOVE_RANDOMIZATION_DETAILS(string SUBJID)
        {
            try
            {
                dal_IWRS.IWRS_ACTIONS_SP(
                ACTION: "REMOVE_RANDOMIZATION_DETAILS",
                STEPID: hfSTEPID.Value,
                SUBJID: SUBJID,
                CurrentDate: Session["IWRS_CurrentDate"].ToString()
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_APPLVISIT(string SUBJID, string VISIT, string NextVISIT)
        {
            try
            {
                if (VISIT != "0" && VISIT != "")
                {
                    dal_IWRS.IWRS_VISIT_SP(
                    ACTION: "UPDATE_APPLVISIT",
                    STEPID: hfSTEPID.Value,
                    VISIT: VISIT,
                    SUBJID: SUBJID,
                    NEXT_VISITNUM: NextVISIT,
                    CurrentDate: Session["IWRS_CurrentDate"].ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void KIT_DISPENSE(string SUBJID, string DOSEVISIT, string DOSEVISITNUM)
        {
            try
            {
                DataSet dsCheckArm = dal_IWRS.IWRS_KITS_SP(ACTION: "CHECK_ARMS_Criteria", SUBJID: SUBJID, STEPID: Request.QueryString["STEPID"].ToString());

                if (dsCheckArm.Tables.Count > 0 && dsCheckArm.Tables[0].Rows.Count > 0 && dsCheckArm.Tables[0].Rows[0]["TREAT_GRP"].ToString() != "0")
                {
                    foreach (DataRow dr in dsCheckArm.Tables[0].Rows)
                    {
                        int Quantity = 1;

                        if (dr["Quantity"].ToString() != "")
                        {
                            Quantity = Convert.ToInt32(dr["Quantity"]);
                        }
                        
                        dal_IWRS.IWRS_KITS_SP(
                        ACTION: "KIT_DISPENSE_CRITERIA",
                        STEPID: hfSTEPID.Value,
                        SUBJID: SUBJID,
                        TREAT_GRP: dr["TREAT_GRP"].ToString(),
                        TREAT_STRENGTH: "",
                        VISIT: DOSEVISIT,
                        VISITNUM: DOSEVISITNUM,
                        CurrentDate: Session["IWRS_CurrentDate"].ToString(),
                        QUANTITY: Quantity.ToString()
                        );
                    }
                }
                else
                {

                    DataSet dsArm = dal_IWRS.IWRS_KITS_SP(ACTION: "GET_ARMS", SUBJID: SUBJID, STEPID: Request.QueryString["STEPID"].ToString());

                    foreach (DataRow dr in dsArm.Tables[0].Rows)
                    {
                        dal_IWRS.IWRS_KITS_SP(
                        ACTION: "KIT_DISPENSE",
                        STEPID: hfSTEPID.Value,
                        SUBJID: SUBJID,
                        TREAT_GRP: dr["TREAT_GRP"].ToString(),
                        TREAT_STRENGTH: dr["TREAT_STRENGTH"].ToString(),
                        VISIT: DOSEVISIT,
                        VISITNUM: DOSEVISITNUM,
                        CurrentDate: Session["IWRS_CurrentDate"].ToString()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_SUBJECT_DOSING(string SUBJID, string DOSEVISITNUM, string DOSEINDIC)
        {
            try
            {
                dal_IWRS.IWRS_KITS_SP(
                ACTION: "UPDATE_SUBJECT_DOSING",
                VISITNUM: DOSEVISITNUM,
                INDICATION_ID: DOSEINDIC,
                SUBJID: SUBJID,
                ENTEREDBY: Session["USER_ID"].ToString(),
                CurrentDate: Session["IWRS_CurrentDate"].ToString()
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool CHECK_StopClause(string SOURCE_ID)
        {
            bool Result = false;
            try
            {
                DataSet dsClause = dal_IWRS.IWRS_STOP_CLAUSE_SP(
                ACTION: "GET_STOP_CLAUSE_AFTER_CRIT",
                MODULEID: SOURCE_ID,
                SUBJID: Request.QueryString["SUBJID"].ToString()
                );

                if (dsClause.Tables.Count > 0 && dsClause.Tables[0].Rows.Count > 0 && dsClause.Tables[0].Rows[0][0].ToString() != "")
                {
                    DataRow drClause = dsClause.Tables[0].Rows[0];

                    DataSet dsVarData = new DataSet();

                    string CASES = drClause["CASES"].ToString();

                    if (CASES.Contains("[") && CASES.Contains("]"))
                    {
                        DataSet dsVariables = dal_IWRS.IWRS_STOP_CLAUSE_SP(
                        ACTION: "GET_SC_VARIABLES_DATA",
                        SUBJID: Request.QueryString["SUBJID"].ToString()
                        );

                        if (dsVariables.Tables.Count > 1)
                        {
                            DataRow drVariablesDATA = dsVariables.Tables[1].Rows[0];

                            CASES = CASES.Replace("[SITEID]", CheckDatatype(drVariablesDATA["SITEID"].ToString()));

                            CASES = CASES.Replace("[TREAT_GRP]", CheckDatatype(drVariablesDATA["TREAT_GRP"].ToString()));

                            foreach (DataRow drVariables in dsVariables.Tables[0].Rows)
                            {
                                if (CASES.Contains("[" + drVariables["VARIABLENAME"].ToString() + "]"))
                                {
                                    CASES = CASES.Replace("[" + drVariables["VARIABLENAME"].ToString() + "]", CheckDatatype(drVariablesDATA[drVariables["VARIABLENAME"].ToString()].ToString()));
                                }
                            }
                        }
                    }

                    DataSet dsRESULT = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "GET_SC_RESULT", Criteria: CASES);

                    if (dsRESULT.Tables.Count > 0 && dsRESULT.Tables[0].Rows.Count > 0)
                    {
                        string CritCode = dsRESULT.Tables[0].Rows[0]["CritCode"].ToString();
                        string Limit = dsRESULT.Tables[0].Rows[0]["LIMIT"].ToString();
                        string MSGBOX = dsRESULT.Tables[0].Rows[0]["MSGBOX"].ToString();
                        string URL = dsRESULT.Tables[0].Rows[0]["URL"].ToString();

                        DataSet dsSUBJECTs = dal_IWRS.IWRS_STOP_CLAUSE_SP(ACTION: "GET_SUBJECT_COUNT", CritCode: CritCode);

                        string SubjectCount = dsSUBJECTs.Tables[0].Rows[0]["SubjectCount"].ToString();

                        if (Convert.ToInt32(Limit) <= Convert.ToInt32(SubjectCount))
                        {
                            Result = false;

                            hfSTOPCLAUSE_MSG.Value = MSGBOX;
                            hfSTOPCLAUSE_URL.Value = URL;
                        }
                        else
                        {
                            Result = true;
                        }
                    }
                    else
                    {
                        Result = true;
                    }

                }
                else
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return Result;
        }

        private string CheckDatatype(string Val)
        {
            string RESULT = "";
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                int i = 0;
                float j = 0;
                double k = 0;
                DateTime l;

                if (Val.Contains("dd/"))
                {
                    Val = Val.Replace("dd/", "01");
                }

                if (Val.Contains("mm/"))
                {
                    Val = Val.Replace("mm/", "01");
                }

                if (int.TryParse(Val, out i))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (float.TryParse(Val, out j))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (double.TryParse(Val, out k))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (DateTime.TryParse(Val, out l) || cf.isDate(Val))
                {
                    RESULT = "dbo.CastDate('" + Val + "')";
                }
                else
                {
                    RESULT = "N'" + Val + "'";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            return RESULT;
        }
    }
}