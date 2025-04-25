using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using CTMS.CommonFunction;
using CTMS;

namespace CTMS
{
    public partial class MM_QueryRouted_Comments : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_MM = new DAL_MM();
        DAL_DM dal_DM = new DAL_DM();

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
                    DataSet ds = dal_MM.MM_QUERY_SP(ACTION: "GET_QUERY_DETAILS", ID: Request.QueryString["ID"].ToString());
                    lblSITEID.Text = ds.Tables[0].Rows[0]["SITEID"].ToString();
                    lblSUBJID.Text = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                    lblVISIT.Text = ds.Tables[0].Rows[0]["VISIT"].ToString();
                    lblMODULENAME.Text = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                    lblQUERYTEXT.Text = ds.Tables[0].Rows[0]["QUERYTEXT"].ToString();
                    DM_QUERYID.Value = ds.Tables[0].Rows[0]["DM_QUERYID"].ToString();

                    DataSet dsComments = dal_MM.MM_QUERY_SP(ACTION: "GET_QUERY_COMMENTS", ID: DM_QUERYID.Value);
                    grdComments.DataSource = dsComments.Tables[0];
                    grdComments.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dal_DM.DM_QUERY_SP(
                    ACTION: "Update_Comment_Status_MM",
                    ID: Request.QueryString["ID"].ToString(),
                    Comment: txtComments.Text,
                    REASON: ddlReason.SelectedValue
                    );

                if (ddlReason.SelectedValue == "Routed to DM")
                {
                    Response.Write("<script> alert('Query Routed to DM Successfully.');window.location='MM_QUERY_ROUTED.aspx'; </script>");
                }
                else
                {
                    Response.Write("<script> alert('Query Closed Successfully.');window.location='MM_QUERY_ROUTED.aspx'; </script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("MM_QUERY_ROUTED.aspx");
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

        protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlReason.SelectedValue == "Closed query")
                {
                    txtComments.CssClass = "form-control width300px";
                }
                else
                {
                    txtComments.CssClass = "form-control required width300px";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}