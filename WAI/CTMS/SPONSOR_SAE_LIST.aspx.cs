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
    public partial class SPONSOR_SAE_LIST : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["UserGroup_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }
                FillINV();
                FillSubject();
                GetData();
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

                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataValueField = "INVNAME";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("All", "0"));

                FillSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal.SAE_ADD_UPDATE(ACTION: "GET_SAE_SUBJECTS", INVID: drpInvID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSubID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GetData()
        {
            try
            {
                DataSet ds = dal.SAE_ADD_UPDATE_NEW(ACTION: "GET_SPONSOR_SAE_LIST", PROJECTID: Session["PROJECTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdSAELog.DataSource = ds;
                    grdSAELog.DataBind();
                }
                else
                {
                    grdSAELog.DataSource = null;
                    grdSAELog.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSubID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSAELog_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                string SAE = Convert.ToString(e.CommandArgument);
                if (e.CommandName == "EditSAE")
                {
                    string SAEID = ((Label)grdSAELog.Rows[RowIndex].FindControl("lblSAEID")).Text;

                    string STATUS = ((Label)grdSAELog.Rows[RowIndex].FindControl("lblStatus")).Text;

                    string INVID = ((Label)grdSAELog.Rows[RowIndex].FindControl("INVID")).Text;

                    string SUBJID = ((Label)grdSAELog.Rows[RowIndex].FindControl("SUBJID")).Text;

                    string NEW_FOLLOWUPNO = ((Label)grdSAELog.Rows[RowIndex].FindControl("NEW_FOLLOWUP")).Text;

                    Response.Redirect("SPONSOR_SAE_DETAILS.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID, false);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView1_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }
    }
}