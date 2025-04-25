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
    public partial class NSAE_SETUP : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_EMAIL_STEP();
                    GET_ALL_SITES();
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
                DataSet ds = dal_SAE.SAE_SETUP_SP(
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

        protected void GET_ALL_SITES()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                            ACTION: "GET_ALL_SITES"
                        );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvEmailds.DataSource = ds;
                    gvEmailds.DataBind();
                }
                else
                {
                    gvEmailds.DataSource = null;
                    gvEmailds.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_EMAIL(string STEP_ID)
        {
            try
            {
                for (int i = 0; i < gvEmailds.Rows.Count; i++)
                {
                    TextBox txtEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtEMAILIDs");
                    TextBox txtCCEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtCCEMAILIDs");
                    TextBox txtBCCEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtBCCEMAILIDs");
                    Label lblSiteID = (Label)gvEmailds.Rows[i].FindControl("lblSiteID");
                    string ID = STEP_ID;

                    if (txtEMAILIDs.Text != "")
                    {
                        dal_SAE.SAE_SETUP_SP
                            (
                            ACTION: "INSERT_STEP_EMAIL",
                            INVID: lblSiteID.Text,
                            EMAILID: txtEMAILIDs.Text,
                            CC_EMAILID: txtCCEMAILIDs.Text,
                            BCC_EMAILID: txtBCCEMAILIDs.Text,
                            EMAIL_BODY: txtEmailBody.Text,
                            EMAIL_SUBJECT: txtEmailSubject.Text,
                            ID: ID,
                            ACTIONS: drpAction.SelectedItem.Text
                            );
                    }
                    else
                    {

                        dal_SAE.SAE_SETUP_SP(
                                ACTION: "DELETE_STEP_EMAIL",
                                ID: ID,
                                INVID: lblSiteID.Text
                                );
                    }
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
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                                    ACTION: "SAE_SETUP_NAME",
                                    ACTIONS: drpAction.SelectedItem.Text,
                                    EMAIL_SUBJECT: txtEmailSubject.Text,
                                    EMAIL_BODY: txtEmailBody.Text
                                    );

                string STEP_ID = ds.Tables[0].Rows[0]["ID"].ToString();

                INSERT_EMAIL(STEP_ID);

                Response.Write("<script> alert('Email Defined Successfully.'); window.location.href = 'NSAE_SETUP.aspx';</script>");
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
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                               ACTION: "SAE_SETUP_NAME",
                               ACTIONS: drpAction.SelectedItem.Text,
                               EMAIL_SUBJECT: txtEmailSubject.Text,
                               EMAIL_BODY: txtEmailBody.Text,
                               ID: hdnStepID.Value
                               );



                INSERT_EMAIL(hdnStepID.Value);
                Response.Write("<script> alert('Defined email updated Successfully.'); window.location.href = 'NSAE_SETUP.aspx';</script>");
                
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
                Response.Redirect("NSAE_SETUP.aspx", true);
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

                hdnStepID.Value = id;

                if (e.CommandName == "EditStep")
                {
                    SELECT_STEP(id);
                }
                else if (e.CommandName == "DeleteStep")
                {
                    DELETE_STEP(id);

                    Response.Redirect("NSAE_SETUP.aspx", true);
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

                DataSet ds = dal_SAE.SAE_SETUP_SP(
                                ACTION: "GET_STEPS_BY_ID",
                                ID: ID
                                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpAction.SelectedItem.Text = ds.Tables[0].Rows[0]["ACTIONS"].ToString();
                    txtEmailSubject.Text = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                    txtEmailBody.Text = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();

                    DataSet ds1 = dal_SAE.SAE_SETUP_SP(
                                    ACTION: "GET_ALL_SITES",
                                    ID: ID
                                    );
                    gvEmailds.DataSource = ds1;
                    gvEmailds.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETE_STEP(string ID)
        {
            try
            {

                DataSet ds = dal_SAE.SAE_SETUP_SP(
                        ACTION: "DELETE_STEP",
                        ID: ID
                        );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void drpAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                    ACTION: "GET_STEPS_BY_ACTIVITY",
                    ACTIONS: drpAction.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpAction.SelectedItem.Text = ds.Tables[0].Rows[0]["ACTIONS"].ToString();
                    txtEmailSubject.Text = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();
                    txtEmailBody.Text = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();

                    DataSet ds1 = dal_SAE.SAE_SETUP_SP(
                        ACTION: "GET_ALL_SITES",
                        ID: ds.Tables[0].Rows[0]["ID"].ToString()
                        );

                    gvEmailds.DataSource = ds1;
                    gvEmailds.DataBind();
                }
                else
                {
                    txtEmailSubject.Text = "";
                    txtEmailBody.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}