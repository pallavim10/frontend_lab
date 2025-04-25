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
    public partial class Code_Pending : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gridData_PreRender(object sender, EventArgs e)
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

        private void GET_DATA()
        {
            try
            {
                DataSet dt = dal.DB_CODE_SP(
                  ACTION: "GET_PENDING_CODES",
                  AUTOCODELIB: drpdictionary.SelectedValue
                  );

                if (dt.Tables[0].Rows.Count > 0)
                {
                    gridData.DataSource = dt;
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

        protected void Approve_Click(object sender, EventArgs e)
        {
            try
            {
                bool SELCTED = true;

                for (int i = 0; i < gridData.Rows.Count; i++)
                {
                    CheckBox Chek_Approve = (CheckBox)gridData.Rows[i].FindControl("Chek_Approve");

                    if (Chek_Approve.Checked && SELCTED)
                    {
                        SELCTED = false;
                    }
                }
                if (SELCTED)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Select at least one record'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                else
                {
                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnApproval_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gridData.Rows.Count; i++)
                {
                    CheckBox Chek_Approve = (CheckBox)gridData.Rows[i].FindControl("Chek_Approve");
                    HiddenField hdnID = (HiddenField)gridData.Rows[i].FindControl("hdnID");

                    if (Chek_Approve.Checked)
                    {
                        int ID = Convert.ToInt32(hdnID.Value);

                        dal.DB_CODE_SP(
                            ACTION: "CODE_APPROVAL",
                            Approval: "Approved",
                            ApproveComm: txtApprovalComments.Text,
                            ID: ID.ToString(),
                            AUTOCODELIB: drpdictionary.SelectedValue);
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Approved Successfully'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelApproval_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void Disapprove_Click(object sender, EventArgs e)
        {
            try
            {
                bool SELCTED = true;

                for (int i = 0; i < gridData.Rows.Count; i++)
                {
                    CheckBox Chek_Disapprove = (CheckBox)gridData.Rows[i].FindControl("Chek_Disapprove");

                    if (Chek_Disapprove.Checked && SELCTED)
                    {
                        SELCTED = false;
                    }
                }
                if (SELCTED)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Select at least one record'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                else
                {
                    ModalPopupExtender2.Show();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitDisapprove_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gridData.Rows.Count; i++)
                {
                    CheckBox Chek_Disapprove = (CheckBox)gridData.Rows[i].FindControl("Chek_Disapprove");
                    HiddenField hdnID = (HiddenField)gridData.Rows[i].FindControl("hdnID");

                    if (Chek_Disapprove.Checked)
                    {
                        int ID = Convert.ToInt32(hdnID.Value);

                        DataSet dt = dal.DB_CODE_SP(
                                    ACTION: "CODE_APPROVAL",
                                    Approval: "Disapproved",
                                    ApproveComm: txtDisapproveComments.Text,
                                    ID: ID.ToString(),
                                    AUTOCODELIB: drpdictionary.SelectedValue
                                    );
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Disapproved Successfully'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelDisapprove_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[3].CssClass = "disp-none";
                e.Row.Cells[4].CssClass = "disp-none";
                e.Row.Cells[5].CssClass = "disp-none";
                e.Row.Cells[6].CssClass = "disp-none";
                e.Row.Cells[7].CssClass = "disp-none";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}