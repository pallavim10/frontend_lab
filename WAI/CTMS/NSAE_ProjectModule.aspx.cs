using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class NSAE_ProjectModule : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_Added_PROJECT_MASTER();
                    GET_UnAdded_PROJECT_MASTER();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_Added_PROJECT_MASTER()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                    ACTION: "GET_Added_PROJECT_MASTER"
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvAddedModules.DataSource = ds;
                    gvAddedModules.DataBind();
                    lbtnRemoveFromGrp.Visible = true;
                }
                else
                {
                    gvAddedModules.DataSource = null;
                    gvAddedModules.DataBind();
                    lbtnRemoveFromGrp.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_UnAdded_PROJECT_MASTER()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                        ACTION: "GET_UnAdded_PROJECT_MASTER"
                        );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvAllModules.DataSource = ds;
                    gvAllModules.DataBind();
                    lbtnAddToGrp.Visible = true;
                }
                else
                {
                    gvAllModules.DataSource = null;
                    gvAllModules.DataBind();
                    lbtnAddToGrp.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddToGrp_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_SAE_PROJECT_MASTER();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_SAE_PROJECT_MASTER()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                bool Chkselect = false;
                for (int i = 0; i < gvAllModules.Rows.Count; i++)
                {
                    CheckBox ChAction, chkMedicalReview;
                    ChAction = (CheckBox)gvAllModules.Rows[i].FindControl("Chk_Sel_Fun");
                    chkMedicalReview = (CheckBox)gvAllModules.Rows[i].FindControl("chkMedicalReview");
                    
                    if (ChAction.Checked)
                    {
                        Label lblModuleID = (Label)gvAllModules.Rows[i].FindControl("lblModuleID");
                        TextBox txtSEQNO = (TextBox)gvAllModules.Rows[i].FindControl("txtSEQNO");
                        string ProjectID = Session["PROJECTID"].ToString();
                        if(txtSEQNO.Text.Trim()!="")
                        {
                            DataSet ds = dal_SAE.SAE_SETUP_SP(
                                                                ACTION: "INSERT_SAE_PROJECT_MASTER",
                                                                MODULEID: lblModuleID.Text,
                                                                SEQNO: txtSEQNO.Text
                                                                );
                            Chkselect = true;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please enter Sequence No.');", true);
                        }
                    }
                    if (chkMedicalReview.Checked)
                    {
                        Label lblModuleID = (Label)gvAllModules.Rows[i].FindControl("lblModuleID");

                        dal_SAE.SAE_SETUP_SP(
                            ACTION: "ADD_MODULE_FOR_MM",
                            MODULEID: lblModuleID.Text
                            );
                    }
                }
                if (!Chkselect)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please select module.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Module added successfully.');", true);
                }
                GET_Added_PROJECT_MASTER();
                GET_UnAdded_PROJECT_MASTER();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void lbtnRemoveFromGrp_Click(object sender, EventArgs e)
        {
            try
            {
                DELETE_SAE_PROJECT_MASTER_ALL();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_SAE_PROJECT_MASTER_ALL()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                bool Chkselect = false;
                for (int i = 0; i < gvAddedModules.Rows.Count; i++)
                {
                    
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAddedModules.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblModuleID = (Label)gvAddedModules.Rows[i].FindControl("lblModuleID");
                        string ProjectID = Session["PROJECTID"].ToString();

                        DataSet dsVISITID = dal_SAE.SAE_SETUP_SP(
                                             ACTION: "GETID_DELETE_SAE_PROJECT_MASTER",
                                             MODULEID: lblModuleID.Text
                                           );

                        dal_SAE.SAE_SETUP_SP(
                                ACTION: "DELETE_SAE_PROJECT_MASTER_ALL",
                                MODULEID: lblModuleID.Text
                                );
                        Chkselect = true;
                        
                    }
                }
                if (!Chkselect)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please select module.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Module removed successfully.');", true);
                }
                GET_Added_PROJECT_MASTER();
                GET_UnAdded_PROJECT_MASTER();
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void gvAddedModules_PreRender(object sender, EventArgs e)
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

        protected void gvAddedModules_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    HtmlControl iconSAE_MM = (HtmlControl)e.Row.FindControl("iconSAE_MM");
                    if (drv["SAE_MM"].ToString() == "True")
                    {
                        iconSAE_MM.Attributes.Add("class", "fa fa-check");
                        iconSAE_MM.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSAE_MM.Attributes.Add("class", "fa fa-times");
                        iconSAE_MM.Attributes.Add("style", "color: red;");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvAllModules_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string REVIEW_COUNT = dr["REVIEW_COUNT"].ToString();
                    CheckBox chkMedicalReview = (CheckBox)e.Row.FindControl("chkMedicalReview");
                    CheckBox Chk_Sel_Fun = (CheckBox)e.Row.FindControl("Chk_Sel_Fun");
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                    if (REVIEW_COUNT != "0")
                    {
                        lblStatus.Text = "";
                    }
                    else
                    {
                        chkMedicalReview.Visible = false;
                        Chk_Sel_Fun.Visible = false;
                        lblStatus.Text = "Review Pending";
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