using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class IWRS_PROJECT_DRPS : System.Web.UI.Page
    {
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtText.Attributes.Add("MaxLength","200");
            try
            {
                if (!Page.IsPostBack)
                {
                    lblVariable.Text = Request.QueryString["VARIABLENAME"].ToString();
                    GET_REVIEW_STATUS();
                    Getdata();
                    DISABLE_BUTTONS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        private void GET_REVIEW_STATUS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_CONFIGURATION_REVIEW");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //Review
                    hdnREVIEWSTATUS.Value = ds.Tables[0].Rows[0]["ANS"].ToString();
                }
                else
                {
                    //Unreview
                    hdnREVIEWSTATUS.Value = "Unreview";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void DISABLE_BUTTONS()
        {
            if (hdnREVIEWSTATUS.Value == "Review")
            {
                btnsubmit.Enabled = false;
                btnsubmit.Text = "Configuration has been Frozen";
                btnsubmit.CssClass = btnsubmit.CssClass.Replace("btn-primary", "btn-danger");

                btnupdate.Enabled = false;
                btnupdate.Text = "Configuration has been Frozen";
                btnupdate.CssClass = btnupdate.CssClass.Replace("btn-primary", "btn-danger");
            }
        }
        private void Getdata()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_DRPDOWNDATA",
                    FORMID: Request.QueryString["FORMID"].ToString(),
                    VARIABLENAME: Request.QueryString["VARIABLENAME"].ToString()
                    );

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblField.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                        lblVariable.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        grdData.DataSource = ds.Tables[1];
                        grdData.DataBind();
                    }
                    else
                    {
                        grdData.DataSource = null;
                        grdData.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "INSERT_DRPDOWNDATA",
                    VARIABLENAME: lblVariable.Text,
                    FIELDNAME: txtText.Text,
                    SEQNO: txtSeqNo.Text,
                    CONTROLTYPE: Request.QueryString["CONTROL"].ToString(),
                    FORMID: Request.QueryString["FORMID"].ToString()
                    );

                Response.Write("<script> alert('Field Option added successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(
                    ACTION: "UPDATE_DRPDOWNDATA_BYID",
                    FIELDNAME: txtText.Text,
                    SEQNO: txtSeqNo.Text,
                    ID: ViewState["ID"].ToString()
                    );

                Response.Write("<script> alert('Field Option updated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                ViewState["ID"] = id;
                if (e.CommandName == "EditOption")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_DRPDOWNDATA_BYID", ID: id);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtText.Text = ds.Tables[0].Rows[0]["TEXT"].ToString();
                        txtSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

                        btnupdate.Visible = true;
                        btnsubmit.Visible = false;
                    }
                }
                else if (e.CommandName == "DeleteOption")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(
                        ACTION: "DELETE_DRPDOWNDATA_BYID",
                        ID: id,
                        FORMID: Request.QueryString["FORMID"].ToString()
                        );
                    Response.Write("<script> alert('Field Option deleted successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
                    Getdata();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdData_PreRender(object sender, EventArgs e)
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

        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    
                    LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtndeleteSection");

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtnDelete.Enabled = false;
                        lbtnDelete.ToolTip = "Configuration has been Frozen";
                        lbtnDelete.OnClientClick = "return ConfigFrozen_MSG()";
                    }
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