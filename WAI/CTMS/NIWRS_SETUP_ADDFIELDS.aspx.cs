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
    public partial class NIWRS_SETUP_ADDFIELDS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_REVIEW_STATUS();
                    SELECT_LISTING();
                    GET_LIST_ADDFIELD();
                    GET_LIST_MODULE();
                    DISABLE_BUTTONS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                btnSubmitCol.Enabled = false;
                btnUpdateCol.Enabled = false;
                btnSubmitCol.Text = "Configuration has been Frozen";
                btnUpdateCol.Text = "Configuration has been Frozen";
                btnSubmitCol.CssClass = btnSubmitCol.CssClass.Replace("btn-primary", "btn-danger");
                btnUpdateCol.CssClass = btnUpdateCol.CssClass.Replace("btn-primary", "btn-danger");

            }
        }
        //Columns Section Starts

        private void SELECT_LISTING()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_LIST_SP(ACTION: "SELECT_LISTING", ID: Request.QueryString["ID"].ToString());

                lblList.Text = ds.Tables[0].Rows[0]["LISTNAME"].ToString();
                lblListing.Text = lblList.Text;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_LIST_ADDFIELD()
        {
            try
            {
                dal_IWRS.IWRS_SET_LIST_SP
                    (
                    ACTION: "INSERT_LIST_ADDFIELD",
                    COL_NAME: txtColName.Text,
                    FIELDNAME: txtFieldName.Text,
                    LISTID: Request.QueryString["ID"].ToString(),
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                Response.Write("<script> alert('Additional Fields added successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_LIST_ADDFIELD()
        {
            try
            {
                dal_IWRS.IWRS_SET_LIST_SP
                    (
                    ACTION: "UPDATE_LIST_ADDFIELD",
                    ID: ViewState["editLColsID"].ToString(),
                    COL_NAME: txtColName.Text,
                    FIELDNAME: txtFieldName.Text,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                Response.Write("<script> alert('Defined Fields Updated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR_COLS()
        {
            try
            {
                txtColName.Text = "";
                txtFieldName.Text = "";
                btnSubmitCol.Visible = true;
                btnUpdateCol.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_LIST_ADDFIELD(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_SETUP_SP
                    (
                    ACTION: "DELETE_LIST_ADDFIELD",
                    ENTEREDBY: Session["USER_ID"].ToString(),
                    ID: ID
                    );

                Response.Write("<script> alert('Defined Fields Deleted successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_LIST_ADDFIELD(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_LIST_SP
                            (
                            ACTION: "SELECT_LIST_ADDFIELD",
                            ID: ID
                            );

                txtFieldName.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                txtColName.Text = ds.Tables[0].Rows[0]["COL_NAME"].ToString();

                btnSubmitCol.Visible = false;
                btnUpdateCol.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LIST_ADDFIELD()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP
                            (
                            ACTION: "GET_LIST_ADDFIELD",
                            LISTID: Request.QueryString["ID"].ToString()
                            );
                grdCols.DataSource = ds;
                grdCols.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LIST_MODULE()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP
                            (
                            ACTION: "GET_LIST_MODULE",
                            LISTID: Request.QueryString["ID"].ToString()
                            );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds.Tables[0];
                    drpModule.DataValueField = "MODULEID";
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("None", "0"));

                }

                if(ds.Tables[1].Rows.Count > 0)
                {
                    drpModule.SelectedValue = ds.Tables[1].Rows[0]["ENTRY_MODULE"].ToString();
                }    
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnSubmitCol_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_LIST_ADDFIELD();
                CLEAR_COLS();
                GET_LIST_ADDFIELD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateCol_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_LIST_ADDFIELD();
                CLEAR_COLS();
                GET_LIST_ADDFIELD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelCol_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_COLS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdCols_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                ViewState["editLColsID"] = id;

                if (e.CommandName == "EditCol")
                {
                    SELECT_LIST_ADDFIELD(id);
                }
                else if (e.CommandName == "DeleteCol")
                {
                    DELETE_LIST_ADDFIELD(id);
                    GET_LIST_ADDFIELD();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdCols_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    LinkButton lbtndeleteSection = (e.Row.FindControl("lbtndeleteSection") as LinkButton);

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtndeleteSection.Enabled = false;
                        lbtndeleteSection.ToolTip = "Configuration has been Frozen";
                        lbtndeleteSection.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dal_IWRS.NIWRS_SETUP_SP
                            (
                            ACTION: "UPDATEMODULELIST",
                            MODULEID: drpModule.SelectedValue,
                            LISTID: Request.QueryString["ID"].ToString()
                            );

                Response.Write("<script> alert('Module details updated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            
        }

        //Columns Section Ends
    }
}