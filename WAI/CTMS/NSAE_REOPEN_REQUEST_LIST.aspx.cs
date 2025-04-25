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
    public partial class NSAE_REOPEN_REQUEST_LIST : System.Web.UI.Page
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
                DataSet ds = dal.GET_INVID_SP();

                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataValueField = "INVNAME";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--Select--", "0"));

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

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                GETDATA();
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
                GET_SAEID();
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SAEID()
        {
            try
            {
                DataSet ds = dal.SAE_ADD_UPDATE_NEW(ACTION: "GET_CLOSED_SAEIDS", SUBJECTID: drpSubID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSAEID.DataSource = ds.Tables[0];
                    drpSAEID.DataValueField = "SAEID";
                    drpSAEID.DataTextField = "SAEID";
                    drpSAEID.DataBind();
                    drpSAEID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSAEID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal.SAE_ADD_UPDATE_NEW(ACTION: "GET_DCF_DATA_PENDING",
                INVID: drpInvID.SelectedValue,
                SUBJECTID: drpSubID.SelectedValue,
                SAEID: drpSAEID.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdSAE.DataSource = ds;
                    grdSAE.DataBind();
                }
                else
                {
                    grdSAE.DataSource = null;
                    grdSAE.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
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

        protected void grdSAE_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                string ID = Convert.ToString(e.CommandArgument);

                if (e.CommandName == "SAEApprove")
                {
                    string SAEID = ((Label)grdSAE.Rows[RowIndex].FindControl("lblSAEID")).Text;

                    string SUBJID = ((Label)grdSAE.Rows[RowIndex].FindControl("SUBJID")).Text;

                    string INVID = ((Label)grdSAE.Rows[RowIndex].FindControl("INVID")).Text;

                    Response.Redirect("NSAE_APPROVE_REJECT_DCF.aspx?SAEID=" + SAEID + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&ID=" + ID + "&ACTION=Approved");

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ConfirmMsg('" + SAEID + "','" + SUBJID + "','" + INVID + "'," + ID + ");", true);
                }
                else if (e.CommandName == "SAEReject")
                {
                    string SAEID = ((Label)grdSAE.Rows[RowIndex].FindControl("lblSAEID")).Text;

                    string SUBJID = ((Label)grdSAE.Rows[RowIndex].FindControl("SUBJID")).Text;

                    string INVID = ((Label)grdSAE.Rows[RowIndex].FindControl("INVID")).Text;

                    Response.Redirect("NSAE_APPROVE_REJECT_DCF.aspx?SAEID=" + SAEID + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&ID=" + ID + "&ACTION=Reject");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSAEID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}