using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class NCTMS_SITE_VISIT_OPEN_CRF : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    hfanyBlank.Value = "false";
                    hffirstClick.Value = "false";

                    FillINV();
                    //FillVISITS_TYPE();
                    //FillVISITS_NOM();
                    FILL_MODULES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            try
            {
                DataSet ds = dal.GetSiteID(
                Action: "INVID",
                PROJECTID: Session["PROJECTID"].ToString(),
                User_ID: Session["User_ID"].ToString()
                );

                ddlSiteId.DataSource = ds.Tables[0];
                ddlSiteId.DataValueField = "INVNAME";
                ddlSiteId.DataBind();
                ddlSiteId.Items.Insert(0, new ListItem("--Select--", "0"));

                if (Session["CTMS_CRF_SITEID"] != null)
                {
                    if (ddlSiteId.Items.Contains(new ListItem(Session["CTMS_CRF_SITEID"].ToString())))
                    {
                        ddlSiteId.SelectedValue = Session["CTMS_CRF_SITEID"].ToString();
                    }
                }

                if (Request.QueryString["CTMS_CRF_SITEID"] != null)
                {
                    if (ddlSiteId.Items.FindByValue(Request.QueryString["CTMS_CRF_SITEID"].ToString()) != null)
                    {
                        ddlSiteId.SelectedValue = Request.QueryString["CTMS_CRF_SITEID"].ToString();
                    }
                }

                Session["CTMS_CRF_SITEID"] = ddlSiteId.SelectedValue;

                FillVISITS_TYPE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillVISITS_TYPE()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_VISITTYPE_MASTER"
                );

                drpVisitType.DataSource = ds.Tables[0];
                drpVisitType.DataValueField = "ID";
                drpVisitType.DataTextField = "VISIT_NAME";
                drpVisitType.DataBind();
                drpVisitType.Items.Insert(0, new ListItem("--Select Visit Type--", "0"));

                if (Session["CTMS_CRF_VISITTYPE"] != null)
                {
                    if (drpVisitType.Items.FindByValue(Session["CTMS_CRF_VISITTYPE"].ToString()) != null)
                    {
                        drpVisitType.SelectedValue = Session["CTMS_CRF_VISITTYPE"].ToString();
                    }
                }

                if (Request.QueryString["CTMS_VISITTYPEID"] != null)
                {
                    if (drpVisitType.Items.FindByValue(Request.QueryString["CTMS_VISITTYPEID"].ToString()) != null)
                    {
                        drpVisitType.SelectedValue = Request.QueryString["CTMS_VISITTYPEID"].ToString();
                    }
                }

                Session["CTMS_CRF_VISITTYPE"] = drpVisitType.SelectedValue;

                FillVISITS_NOM();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillVISITS_NOM()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_VISIT_NOM_AGAINST_USER_CRA",
                SITEID: ddlSiteId.SelectedValue,
                VISITID: drpVisitType.SelectedValue
                );

                drpVisitID.DataSource = ds.Tables[0];
                drpVisitID.DataValueField = "VISIT_NOM";
                drpVisitID.DataTextField = "VISIT_NOM";
                drpVisitID.DataBind();
                drpVisitID.Items.Insert(0, new ListItem("--Select Visit Id--", "0"));

                if (Session["CTMS_CRF_VISITID"] != null)
                {
                    if (drpVisitID.Items.FindByValue(Session["CTMS_CRF_VISITID"].ToString()) != null)
                    {
                        drpVisitID.SelectedValue = Session["CTMS_CRF_VISITID"].ToString();
                    }
                }

                if (Request.QueryString["CTMS_CRF_VISITID"] != null)
                {
                    if (drpVisitID.Items.FindByValue(Request.QueryString["CTMS_CRF_VISITID"].ToString()) != null)
                    {
                        drpVisitID.SelectedValue = Request.QueryString["CTMS_CRF_VISITID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpVisitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillVISITS_NOM();
                FILL_MODULES();

                Session["CTMS_CRF_VISITTYPE"] = drpVisitType.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSiteId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillVISITS_TYPE();

                FillVISITS_NOM();

                FILL_MODULES();

                Session["CTMS_CRF_SITEID"] = ddlSiteId.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpVisitID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FILL_MODULES();

                Session["CTMS_CRF_VISITID"] = drpVisitID.SelectedValue;
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
                ACTION: "GET_MODULES_AGAINST_VISIT_NOM_CRA",
                SITEID: ddlSiteId.SelectedValue,
                VISITID: drpVisitID.SelectedValue,
                SVID: drpVisitID.SelectedItem.Text
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grd_OpenCRF.DataSource = ds.Tables[0];
                    Grd_OpenCRF.DataBind();

                    btnCRAFinal.Visible = true;
                    btnTrackers.Visible = true;
                }
                else
                {
                    Grd_OpenCRF.DataSource = null;
                    Grd_OpenCRF.DataBind();

                    btnCRAFinal.Visible = false;
                    btnTrackers.Visible = false;
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
                    string VISITID = drpVisitType.SelectedValue;
                    string VISIT = (gvr.FindControl("txtVisitCode") as Label).Text;
                    string INVID = ddlSiteId.SelectedValue;

                    if ((gvr.FindControl("MULTIPLEYN") as Label).Text == "True")
                    {
                        Response.Redirect("NCTMS_DataEntry_MultipleData.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&VISIT_NOM=" + drpVisitID.SelectedItem.Text + "&SVID=" + drpVisitID.SelectedValue);
                    }
                    else
                    {
                        Response.Redirect("NCTMS_DataEntry.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&VISIT_NOM=" + drpVisitID.SelectedItem.Text + "&SVID=" + drpVisitID.SelectedValue);
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        protected void btnCRAFinal_Click(object sender, EventArgs e)
        {
            try
            {
                if (hffirstClick.Value != "true")
                {
                    hffirstClick.Value = "true";

                    for (int i = 0; i < Grd_OpenCRF.Rows.Count; i++)
                    {
                        TextBox txtPAGESTATUS = (TextBox)Grd_OpenCRF.Rows[i].FindControl("txtPAGESTATUS");

                        if (txtPAGESTATUS.Text == "" || txtPAGESTATUS.Text == "False")
                        {
                            Grd_OpenCRF.Rows[i].Attributes.Add("class", "brd-1px-redimp");

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
                    InsertUpdateSV();

                    SendEmail("Final by CRA");

                    string MESSAGE = drpVisitType.SelectedItem.Text + " with Visit Id: " + drpVisitID.SelectedValue + " is submited successfully to PM";

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); window.location='NCTMS_SITE_VISIT_OPEN_CRF.aspx';", true);
                }
                else
                {
                    string MESSAGE = " Highlighted forms are not entered. Please enter details OR if you want to proceed to Submit the form then click Final again.";

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + MESSAGE.ToString() + "'); ", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void InsertUpdateSV()
        {
            try
            {
                dal.CTMS_GetSetPV(SVID: drpVisitID.SelectedValue,
                    SITEID: ddlSiteId.SelectedValue,
                    VISITID: drpVisitType.SelectedValue,
                    STATUS: "For Review",
                    SUB_STATUS: "Final by CRA"
                    );
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
                VISITID: drpVisitType.SelectedValue,
                SITEID: drpVisitID.SelectedValue,
                SVID: drpVisitID.SelectedItem.Text,
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