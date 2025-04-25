using CTMS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class NSAE_AddDrpDownData : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    lblVariable.Text = Request.QueryString["VARIABLENAME"].ToString();
                    lblField.Text = Request.QueryString["FIELDNAME"].ToString();
                    Getdata();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        private void Getdata()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_DRPDOWNDATA",
                    ID: Request.QueryString["ID"].ToString(),
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
                throw ex;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "INSERT_DRPDOWNDATA",
                    VARIABLENAME: lblVariable.Text,
                    DEFAULTVAL: txtText.Text,
                    SEQNO: txtSeqNo.Text
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
                DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                    ACTION: "UPDATE_DRPDOWNDATA_BYID",
                    DEFAULTVAL: txtText.Text,
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
                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;
                if (e.CommandName == "EditModule")
                {
                    DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(ACTION: "GET_DRPDOWNDATA_BYID", ID: e.CommandArgument.ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtText.Text = ds.Tables[0].Rows[0]["TEXT"].ToString();
                        hdnOldText.Value = ds.Tables[0].Rows[0]["TEXT"].ToString();
                        txtSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

                        btnupdate.Visible = true;
                        btnsubmit.Visible = false;
                    }

                }
                else if (e.CommandName == "DeleteModule")
                {
                    DataSet ds = dal_SAE.SAE_OPTIONS_DATA_SP(
                        ACTION: "DELETE_DRPDOWNDATA_BYID",
                        ID: e.CommandArgument.ToString()
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
                    //DataRowView dr = e.Row.DataItem as DataRowView;
                    //string COUNT = dr["COUNT"].ToString();
                    //LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");

                    //if (COUNT != "0")
                    //{
                    //    lbtndeleteSection.Visible = false;
                    //}
                    //else
                    //{
                    //    lbtndeleteSection.Visible = true;
                    //}
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