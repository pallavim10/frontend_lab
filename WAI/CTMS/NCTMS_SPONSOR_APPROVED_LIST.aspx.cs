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
    public partial class NCTMS_SPONSOR_APPROVED_LIST : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    FillINV();
                    GETDATA();
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

                //if (Session["CTMS_CRF_SITEID"] != null)
                //{
                //    if (ddlSiteId.Items.Contains(new ListItem(Session["CTMS_CRF_SITEID"].ToString())))
                //    {
                //        ddlSiteId.SelectedValue = Session["CTMS_CRF_SITEID"].ToString();
                //    }
                //}

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

                //if (Session["CTMS_CRF_VISITTYPE"] != null)
                //{
                //    if (drpVisitType.Items.FindByValue(Session["CTMS_CRF_VISITTYPE"].ToString()) != null)
                //    {
                //        drpVisitType.SelectedValue = Session["CTMS_CRF_VISITTYPE"].ToString();
                //    }
                //}

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
                ACTION: "GET_VISIT_NOM_AGAINST_USER",
                SITEID: ddlSiteId.SelectedValue,
                VISITID: drpVisitType.SelectedValue
                );

                drpVisitID.DataSource = ds.Tables[0];
                drpVisitID.DataValueField = "VISIT_NOM";
                drpVisitID.DataTextField = "VISIT_NOM";
                drpVisitID.DataBind();
                drpVisitID.Items.Insert(0, new ListItem("--Select Visit Id--", "0"));

                if (Request.QueryString["CTMS_CRF_VISITID"] != null)
                {
                    if (drpVisitID.Items.FindByText(Request.QueryString["CTMS_CRF_VISITID"].ToString()) != null)
                    {
                        drpVisitID.SelectedItem.Text = Request.QueryString["CTMS_CRF_VISITID"].ToString();
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
                GETDATA();
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

                GETDATA();

                //Session["CTMS_CRF_SITEID"] = ddlSiteId.SelectedValue;
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
                GETDATA();

                Session["CTMS_CRF_VISITID"] = drpVisitID.SelectedValue;
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
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_SPONSOR_APPROVED_LIST",
                SVID: drpVisitID.SelectedValue,
                SITEID: ddlSiteId.SelectedValue,
                VISITID: drpVisitType.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdVisits.DataSource = ds;
                    grdVisits.DataBind();
                }
                else
                {
                    grdVisits.DataSource = null;
                    grdVisits.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvEmp_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void grdVisits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string SVID = e.CommandArgument.ToString();

                if (e.CommandName == "GoToOpenCRF")
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                    string SITEID = (gvr.FindControl("SITEID") as Label).Text;
                    string VISITID = (gvr.FindControl("VISITID") as Label).Text;
                    string VISIT_NAME = (gvr.FindControl("VISIT_NAME") as Label).Text;

                    Response.Redirect("NCTMS_APPROVED_OPEN_CRF.aspx?SVID=" + SVID + "&SITEID=" + SITEID + "&VISITID=" + VISITID + "&VISIT=" + VISIT_NAME + "&ACTION=SPONSOR_APPROVED", false);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}