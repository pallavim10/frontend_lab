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
    public partial class DB_REVIEW_EMAIL_SETUP : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEmailBody.Attributes.Add("MaxLength", "1000");
            try
            {
                
                if (!Page.IsPostBack)
                {
                    GET_EMAIL_STEP();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_EMAIL_STEP()
        {
            try
            {
                DataSet ds = dal_DB.DB_EMAIL_REVIEW_SP(
                    ACTION: "GET_EMAIL_STEP"
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdSteps.DataSource = ds;
                    grdSteps.DataBind();
                }
                else
                {
                    grdSteps.DataSource = null;
                    grdSteps.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_EMAIL_REVIEW_SP(
                        ACTION: "INSERT_REVIEW_EMAIL",
                            EMAILID: txtEMAILIDs.Text,
                            CC_EMAILID: txtCCEMAILIDs.Text,
                            BCC_EMAILID: txtBCCEMAILIDs.Text,
                            EMAIL_BODY: txtEmailBody.Text,
                            EMAIL_SUBJECT: txtEmailSubject.Text,
                            ACTIVITY: drpAction.SelectedItem.Text
                            );
                ClearSelection();
                Response.Redirect("DB_REVIEW_EMAIL_SETUP.aspx", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                
                DataSet ds = dal_DB.DB_EMAIL_REVIEW_SP(
                        ACTION: "INSERT_REVIEW_EMAIL",
                            EMAILID: txtEMAILIDs.Text,
                            CC_EMAILID: txtCCEMAILIDs.Text,
                            BCC_EMAILID: txtBCCEMAILIDs.Text,
                            EMAIL_BODY: txtEmailBody.Text,
                            EMAIL_SUBJECT: txtEmailSubject.Text,
                            ACTIVITY: drpAction.SelectedItem.Text
                            );
                ClearSelection();
                Response.Redirect("DB_REVIEW_EMAIL_SETUP.aspx", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearSelection();
                Response.Redirect("DB_REVIEW_EMAIL_SETUP.aspx", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdSteps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "EditStep")
                {
                    SELECT_STEP(id);
                }
                else if (e.CommandName == "DeleteStep")
                {
                    DELETE_STEP(id);
                    Response.Redirect("DB_REVIEW_EMAIL_SETUP.aspx", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SELECT_STEP(string ID)
        {
            try
            {
                lbtnsubmit.Visible = false;
                lbtnUpdate.Visible = true;

                DataSet ds = dal_DB.DB_EMAIL_REVIEW_SP(
                    ACTION: "GET_REVIEW_EMAILS_BY_ID",
                    ID: ID
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpAction.SelectedValue = ds.Tables[0].Rows[0]["ACTIVITY"].ToString();
                    txtEmailSubject.Text = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                    txtEmailBody.Text = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();
                    txtEMAILIDs.Text = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    txtCCEMAILIDs.Text = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    txtBCCEMAILIDs.Text = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETE_STEP( string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_EMAIL_REVIEW_SP(
                        ACTION: "DELETE_REVIEW_ACTIVITY",
                        ID: ID
                        );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_EMAIL_REVIEW_SP(
                    ACTION: "GET_REVIEW_EMAILS_BY_ACTIVITY",
                    ACTIVITY: drpAction.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpAction.SelectedItem.Text = ds.Tables[0].Rows[0]["ACTIVITY"].ToString();
                    txtEmailSubject.Text = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                    txtEmailBody.Text = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();
                    txtEMAILIDs.Text = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    txtCCEMAILIDs.Text = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    txtBCCEMAILIDs.Text = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }
                else
                {
                    ClearSelection();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ClearSelection()
        {
            txtEmailSubject.Text = "";
            txtEmailBody.Text = "";
            txtEMAILIDs.Text = "";
            txtCCEMAILIDs.Text = "";
            txtBCCEMAILIDs.Text = "";
        }

        protected void grdSteps_PreRender(object sender, EventArgs e)
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