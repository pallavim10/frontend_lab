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
    public partial class NCTMS_SPONSOR_REVIEW_OPEN_CRF : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    hdnSITEID.Value = Request.QueryString["SITEID"].ToString();
                    hdnSVID.Value = Request.QueryString["SVID"].ToString();
                    hdnVISITID.Value = Request.QueryString["VISITID"].ToString();
                    hdnVISIT.Value = Request.QueryString["VISIT"].ToString();

                    FILL_MODULES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FILL_MODULES()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_MODULES_AGAINST_VISIT_NOM_SPONSOR",
                SITEID: hdnSVID.Value,
                VISITID: hdnVISIT.Value,
                SVID: hdnSVID.Value
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grd_OpenCRF.DataSource = ds.Tables[0];
                    Grd_OpenCRF.DataBind();

                    btnSponsorFinal.Visible = true;
                    btnReturnToPM.Visible = true;
                }
                else
                {
                    Grd_OpenCRF.DataSource = null;
                    Grd_OpenCRF.DataBind();

                    btnSponsorFinal.Visible = false;
                    btnReturnToPM.Visible = false;
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
                ImageButton lnkVISITPAGENO = e.Row.FindControl("lnkPAGENUM") as ImageButton;

                string PAGESTATUS = dr["PAGESTATUS"].ToString();

                if (PAGESTATUS == "True")
                {
                    lnkVISITPAGENO.ImageUrl = "img/green file.png";
                    lnkVISITPAGENO.ToolTip = "Complete";
                }
                else
                {
                    lnkVISITPAGENO.ToolTip = "Not Entered";
                }

                LinkButton lblViewAllComment = (LinkButton)e.Row.FindControl("lblViewAllComment");
                Label lblAllCommentCount = (Label)e.Row.FindControl("lblAllCommentCount");

                if (dr["COMMENTCOUNT"].ToString() == "0")
                {
                    lblViewAllComment.Visible = false;
                }
                else
                {
                    lblViewAllComment.Visible = true;
                    lblAllCommentCount.Text = dr["COMMENTCOUNT"].ToString();
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

                    string MODULEID = (gvr.FindControl("MODULEID") as Label).Text;
                    string MODULENAME = (gvr.FindControl("MODULENAME") as Label).Text;
                    string VISITID = hdnVISITID.Value;
                    string VISIT = (gvr.FindControl("txtVisitCode") as Label).Text;
                    string INVID = hdnSITEID.Value;

                    if ((gvr.FindControl("MULTIPLEYN") as Label).Text == "True")
                    {
                        Session["BACKTOMAINPAGE"] = Request.RawUrl.ToString();
                        Response.Redirect("NCTMS_DataEntry_MultipleData_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&VISIT_NOM=" + hdnSVID.Value + "&SVID=" + hdnSVID.Value + "&ACTION=SPONSOR");
                    }
                    else
                    {
                        Session["BACKTOMAINPAGE"] = Request.RawUrl.ToString();
                        Response.Redirect("NCTMS_DataEntry_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&VISIT_NOM=" + hdnSVID.Value + "&SVID=" + hdnSVID.Value + "&ACTION=SPONSOR");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void InsertUpdateSV(string Status, string SubStatus)
        {
            try
            {
                dal.CTMS_GetSetPV(SVID: hdnSVID.Value,
                    SITEID: hdnSITEID.Value,
                    VISITID: hdnVISITID.Value,
                    STATUS: Status,
                    SUB_STATUS: SubStatus
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSponsorFinal_Click(object sender, EventArgs e)
        {
            try
            {
                InsertUpdateSV("Final", "Final by Sponsor");

                INSERT_COMMENT_PD_DATA();

                SendEmail("Final_by_Sponsor");

                string MESSAGE = hdnVISIT.Value + " with Visit Id: " + hdnSVID.Value + " is submited successfully by Sponsor";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NCTMS_SPONSOR_REVIEW_LIST.aspx';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnReturnToPM_Click(object sender, EventArgs e)
        {
            try
            {
                InsertUpdateSV("For Review", "Return To PM");

                SendEmail("Return_To_PM");

                string MESSAGE = hdnVISIT.Value + " with Visit Id: " + hdnSVID.Value + " is successfully return to PM";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NCTMS_SPONSOR_REVIEW_LIST.aspx';", true);
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
                Response.Redirect("NCTMS_SPONSOR_REVIEW_LIST.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_COMMENT_PD_DATA()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_PD_COMMENT",
                SVID: hdnSVID.Value
                     );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dal.ProtocolVoilation_SP(
                           Action: "INSERT_ProtocolViolation",
                           Project_ID: Session["PROJECTID"].ToString(),
                           INVID: ds.Tables[0].Rows[i]["INVID"].ToString(),
                           SUBJID: ds.Tables[0].Rows[i]["SUBJID"].ToString(),
                           MVID: ds.Tables[0].Rows[i]["ChecklistID"].ToString(),
                           Department: "Operations",
                           Description: ds.Tables[0].Rows[i]["Comments"].ToString(),
                           Summary: ds.Tables[0].Rows[i]["Comments"].ToString(),
                           Source: hdnVISIT.Value,
                           Refrence: ds.Tables[0].Rows[i]["ChecklistID"].ToString(),
                           VISITNUM: ds.Tables[0].Rows[i]["ChecklistID"].ToString(),
                           Nature: "",
                           PDCODE1: "",
                           PDCODE2: "",
                           Close_Date: "",
                           Status: "New"
                           );
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SendEmail(string Type)
        {
            try
            {
                DataSet ds1 = dal.CTMS_DATA_SP(ACTION: "GET_VISIT_EMAIL_IDS",
                VISITID: hdnVISITID.Value,
                SITEID: hdnSITEID.Value,
                SVID: hdnSVID.Value,
                UPDATEQUERY: Type
                        );

                if (ds1.Tables[0].Rows.Count > 0)
                {

                    string EMAILID = ds1.Tables[0].Rows[0]["EMAILIDS"].ToString();

                    string EMAIL_CC = ds1.Tables[0].Rows[0]["CCEMAILIDS"].ToString();

                    string EMAIL_BCC = ds1.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();

                    string BODY = ds1.Tables[0].Rows[0]["BODY"].ToString();

                    string SUBJECT = ds1.Tables[0].Rows[0]["SUBJECT"].ToString();

                    CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();

                    commFun.Email_Users(EMAILID, EMAIL_CC, SUBJECT, BODY, EMAIL_BCC);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;

            }
        }
    }
}