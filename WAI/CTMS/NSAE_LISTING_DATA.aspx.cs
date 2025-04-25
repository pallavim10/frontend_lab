using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CTMS.CommonFunction;
using PPT;

namespace CTMS
{
    public partial class NSAE_LISTING_DATA : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_ID"] == null)
            {
                Response.Redirect("SessionExpired.aspx", true);
                return;
            }

            try
            {
                if (!Page.IsPostBack)
                {
                    lblHeader.Text = Request.QueryString["LISTNAME"].ToString();

                    FillINV();
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
                DataSet ds = dal.GET_SUBJECT_SP(INVID: drpInvID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    drpSubID.Items.Clear();
                }

                if (Session["DM_CRF_SUBJID"] != null)
                {
                    if (drpSubID.Items.Contains(new ListItem(Session["DM_CRF_SUBJID"].ToString())))
                    {
                        drpSubID.SelectedValue = Session["DM_CRF_SUBJID"].ToString();
                    }
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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_SAE.SAE_GET_SERIOUS_EVENTS_SP(
                LISTID: Request.QueryString["LISTID"].ToString(),
                SUBJID: drpSubID.SelectedValue,
                INVID: drpInvID.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridData.DataSource = ds;
                    gridData.DataBind();
                }
                else
                {
                    gridData.DataSource = null;
                    gridData.DataBind();
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

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gridData.Rows[rowIndex];

                string PVID = row.Cells[1].Text;
                string RECID = row.Cells[2].Text;
                string SUBJID = row.Cells[3].Text;

                DataSet ds = dal_SAE.SAE_GENERAL_SP(ACTION: "GET_TABLENAME", DM_PVID: PVID);

                Response.Redirect("NSAE_NEW_LOG.aspx?INVID=" + drpInvID.SelectedValue + "&SUBJID=" + SUBJID + "&DM_PVID=" + PVID + "&DM_RECID=" + RECID + "&TABLENAME=" + ds.Tables[0].Rows[0]["TABLENAME"].ToString() + "&LISTNAME=" + Request.QueryString["LISTNAME"].ToString() + "&LISTID=" + Request.QueryString["LISTID"].ToString(), false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}