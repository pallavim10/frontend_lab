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
    public partial class NCTMS_APPROVED_OPEN_CRF : System.Web.UI.Page
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
                    hdnAction.Value = Request.QueryString["ACTION"].ToString();

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
                DataSet ds = new DataSet();

                if (Request.QueryString["ACTION"].ToString() == "PM_APPROVED")
                {
                    ds = dal.CTMS_DATA_SP(
                    ACTION: "GET_MODULE_APPROVED_PM",
                    SITEID: hdnSVID.Value,
                    VISITID: hdnVISIT.Value,
                    SVID: hdnSVID.Value
                    );
                }
                else
                {
                    ds = dal.CTMS_DATA_SP(
                       ACTION: "GET_MODULE_APPROVED_SPONSOR",
                       SITEID: hdnSVID.Value,
                       VISITID: hdnVISIT.Value,
                       SVID: hdnSVID.Value
                       );
                }

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
                        Response.Redirect("NCTMS_DataEntry_MultipleData_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&VISIT_NOM=" + hdnSVID.Value + "&SVID=" + hdnSVID.Value + "&ACTION=" + hdnAction.Value);
                    }
                    else
                    {
                        Session["BACKTOMAINPAGE"] = Request.RawUrl.ToString();
                        Response.Redirect("NCTMS_DataEntry_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&VISIT_NOM=" + hdnSVID.Value + "&SVID=" + hdnSVID.Value + "&ACTION=" + hdnAction.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnBacktoHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnAction.Value == "SPONSOR_APPROVED")
                {
                    Response.Redirect("NCTMS_SPONSOR_APPROVED_LIST.aspx");
                }
                else
                {
                    Response.Redirect("NCTMS_PM_APPROVED_LIST.aspx");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}