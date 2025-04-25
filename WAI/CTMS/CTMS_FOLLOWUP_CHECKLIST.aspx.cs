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
    public partial class CTMS_FOLLOWUP_CHECKLIST : System.Web.UI.Page
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
                GETDATA();
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

        public void GETDATA()
        {
            try
            {
                DataSet ds = dal.GetSetChecklistComments(
                    Action: "GET_FOLLOWUP_CHECKLIST_DATA",
                    PROJECTID: Session["ProjectId"].ToString(),
                    INVID: ddlSiteId.SelectedValue,
                    ChecklistID: drpVisitID.SelectedItem.Text
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdChecklistComments.DataSource = ds.Tables[0];
                    grdChecklistComments.DataBind();
                }
                else
                {
                    grdChecklistComments.DataSource = null;
                    grdChecklistComments.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdChecklistComments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string Issue = dr["Issue"].ToString();
                    string Internal = dr["Internal"].ToString();
                    string Followup = dr["Followup"].ToString();
                    string Report = dr["Report"].ToString();
                    string Observation = dr["Observation"].ToString();

                    if (Issue == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Row.FindControl("CHK_Issue");
                        CHK.Checked = true;
                    }
                    if (Internal == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Row.FindControl("CHK_Internal");
                        CHK.Checked = true;
                    }
                    if (Followup == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Row.FindControl("CHK_Followup");
                        CHK.Checked = true;
                    }
                    if (Observation == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Row.FindControl("CHK_Observation");
                        CHK.Checked = true;
                    }
                    if (Report == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Row.FindControl("CHK_REPORT");
                        CHK.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
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
    }
}