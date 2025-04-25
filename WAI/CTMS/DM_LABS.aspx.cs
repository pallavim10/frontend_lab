using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_LABS : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
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
            DAL dal = new DAL();

            DataSet ds = dal.GET_INVID_SP(USERID: Session["User_ID"].ToString());

            ddlSITE.DataSource = ds.Tables[0];
            ddlSITE.DataValueField = "INVID";
            ddlSITE.DataBind();
            ddlSITE.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        private void GET_LAB()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_LAB", SITEID: ddlSITE.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdLabs.DataSource = ds;
                    grdLabs.DataBind();
                    divRecords.Visible = true;
                }
                else
                {
                    grdLabs.DataSource = null;
                    grdLabs.DataBind();
                    divRecords.Visible = false;
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
                INSERT_LAB();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lab id and lab name added successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_LAB()
        {
            try
            {
                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "INSERT_LAB",
                    SITEID: ddlSITE.SelectedValue,
                    LABID: txtLABID.Text,
                    LABNAME: txtLABNAME.Text
                    );
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
                UPDATE_LAB();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lab id and lab name updated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_LAB()
        {
            try
            {
                dal_DB.DB_LAB_REFERENCE_SP(ACTION: "UPDATE_LAB",
                    LABID: txtLABID.Text,
                    LABNAME: txtLABNAME.Text,
                    ID: ViewState["DM_LabID"].ToString()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl.ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdLabs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditLab")
                {
                    SELECT_LAB(e.CommandArgument.ToString());
                }
                else if (e.CommandName == "DeleteLab")
                {
                    DELETE_LAB(e.CommandArgument.ToString());

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lab id and lab name deleted successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' ", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_LAB(string ID)
        {
            try
            {
                ViewState["DM_LabID"] = ID;

                DataSet ds = dal_DB.DB_LAB_REFERENCE_SP(ACTION: "GET_LAB_BYID", ID: ID);

                txtLABID.Text = ds.Tables[0].Rows[0]["LABID"].ToString();
                txtLABNAME.Text = ds.Tables[0].Rows[0]["LABNAME"].ToString();

                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_LAB(string ID)
        {
            try
            {
                dal_DB.DB_LAB_REFERENCE_SP(ACTION: "DELETE_LAB", ID: ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSITE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_LAB();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdLabs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string COUNTS = dr["COUNTS"].ToString();
                    LinkButton lbtndeleteLab = (LinkButton)e.Row.FindControl("lbtndeleteLab");

                    if (COUNTS != "0")
                    {
                        lbtndeleteLab.Visible = false;
                    }
                    else
                    {
                        lbtndeleteLab.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdLabs_PreRender(object sender, EventArgs e)
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