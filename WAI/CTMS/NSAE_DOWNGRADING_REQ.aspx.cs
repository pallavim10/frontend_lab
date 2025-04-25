using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class NSAE_DOWNGRADING_REQ : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        DAL dal = new DAL();
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
                    FillINV();
                    GetData();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            DataSet ds = dal.GET_INVID_SP(
                    USERID: Session["User_ID"].ToString()
                    );
            drpInvID.DataSource = ds.Tables[0];
            drpInvID.DataValueField = "INVID";
            drpInvID.DataBind();

            FillSubject();
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_SAE_SUBJECTS",
                    INVID: drpInvID.SelectedValue,
                    STATUS: "Downgrading SAE"
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("All", "0"));
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
                DataSet ds = dal_SAE.SAE_STATUS_LIST_SP(ACTION: "GET_PENDING_DOWNGRADING_REQ_LIST",
                    SUBJID: drpSubID.SelectedValue,
                    INVID: drpInvID.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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

                    Response.Redirect("NSAE_DOWNGRADING_REQ_LIST.aspx?SAEID=" + SAEID + "&STATUS=" + STATUS, false);
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
    }
}