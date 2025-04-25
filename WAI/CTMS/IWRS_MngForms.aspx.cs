using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CTMS
{
    public partial class IWRS_MngForms : System.Web.UI.Page
    {
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        public string FieldColorValue = "#000000";
        public string AnsColorValue = "#000000";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescrip.Attributes.Add("MaxLength", "200");
            try
            {
                if (!Page.IsPostBack)
                {
                    HDNFORMID.Value = Request.QueryString["FORMID"].ToString();
                    GET_REVIEW_STATUS();
                    GetField();
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
                btnUpdateField.Enabled = false;
                btnUpdateField.Text = "Configuration has been Frozen";
                btnUpdateField.CssClass = btnUpdateField.CssClass.Replace("btn-primary", "btn-danger");
            }
        }
        public void GetField()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_PROJECT_MASTER_FIELDS", FORMID: HDNFORMID.Value);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdField.DataSource = ds.Tables[0];
                    grdField.DataBind();
                }
                else
                {
                    grdField.DataSource = null;
                    grdField.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void ClearFieldSection()
        {
            try
            {
                txtFieldName.Text = "";
                lblVariableName.Text = "";
                txtDescrip.Text = "";
                drpSCControl.SelectedIndex = 0;
                txtFormat.Text = "";
                txtSeqno.Text = "";
                txtMaxLength.Text = "";
                chkUppercase.Checked = false;
                chkBold.Checked = false;
                chkUnderline.Checked = false;
                chkRead.Checked = false;
                chkMultiline.Checked = false;
                chkInvisible.Checked = false;
                chkMandatory.Checked = false;
                chkDefault.Checked = false;
                chkVisitSummary.Checked = false;
                chkDosingSummary.Checked = false;
                chkDosingSummary_U.Checked = false;
                chkRandomizationKitSummary.Checked = false;
                chkRandomizationTreatmentSummary.Checked = false;


                txtDefaultData.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSCControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideFormat();
        }

        private void ShowHideFormat()
        {
            try
            {
                if (drpSCControl.SelectedValue == "DECIMAL")
                {
                    divFormat.Visible = true;
                }
                else
                {
                    divFormat.Visible = false;
                }
                if (drpSCControl.SelectedValue == "TEXTBOX" || drpSCControl.SelectedValue == "HTML TEXTBOX")
                {
                    DIVmaxLenght.Visible = true;
                }
                else
                {
                    DIVmaxLenght.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkDefault_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDefault.Checked)
                {
                    txtDefaultData.Visible = true;
                }
                else
                {
                    txtDefaultData.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelField_Click(object sender, EventArgs e)
        {
            ClearFieldSection();
        }

        protected void btnUpdateField_Click(object sender, EventArgs e)
        {
            try
            {
                string DefaultData = "";

                if (chkDefault.Checked)
                {
                    DefaultData = txtDefaultData.Text;
                }

                DataSet ds = dal_IWRS.IWRS_SET_FORM_SP
                (
                ACTION: "UPDATE_PROJECT_MASTER_FIELDS",
                FORMID: HDNFORMID.Value,
                ID: ViewState["ID"].ToString(),
                FIELDNAME: txtFieldName.Text,
                VARIABLENAME: lblVariableName.Text,
                Descrip: txtDescrip.Text,
                CONTROLTYPE: drpSCControl.SelectedValue,
                FORMAT: txtFormat.Text,
                SEQNO: txtSeqno.Text,
                MAXLEN: txtMaxLength.Text,
                FieldColor: Request.Form["FieldColor"],
                AnsColor: Request.Form["AnsColor"],
                UPPERCASE: chkUppercase.Checked,
                BOLDYN: chkBold.Checked,
                UNLNYN: chkUnderline.Checked,
                READYN: chkRead.Checked,
                MULTILINEYN: chkMultiline.Checked,
                INVISIBLE: chkInvisible.Checked,
                MANDATORY: chkMandatory.Checked,
                RPT_VISITSUM: chkVisitSummary.Checked,
                RPT_DOSESUM_B: chkDosingSummary.Checked,
                RPT_DOSESUM_U: chkDosingSummary_U.Checked,
                RPT_RANDKITSUM: chkRandomizationKitSummary.Checked,
                RPT_RANDTRTSUM: chkRandomizationTreatmentSummary.Checked,
                DefaultData: DefaultData
                );

                btnUpdateField.Visible = false;
                //btnCancelField.Visible = false;

                Response.Write("<script> alert('Field Updated Successfully.')</script>");

                ClearFieldSection();
                GetField();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void grdField_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string DataExist = dr["DataExist"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    string IWRS_DM_SYNC = dr["IWRS_DM_SYNC"].ToString();

                    string IWRS = dr["IWRS"].ToString();

                    LinkButton lbtnSync = (LinkButton)e.Row.FindControl("lbtnSync");
                    LinkButton lbtnUNSYNC = (LinkButton)e.Row.FindControl("lbtnUNSYNC");
                    LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                    LinkButton LbtnUndo = (LinkButton)e.Row.FindControl("LbtnUndo");
                    LinkButton lbtnSetFieldValues = (LinkButton)e.Row.FindControl("lbtnSetFieldValues");

                    if (CONTROLTYPE == "DROPDOWN" || CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "CHECKBOX" || CLASS.Contains("OptionValues form-control"))
                    {
                        LinkButton lnlAddOption = (LinkButton)e.Row.FindControl("lnlAddOption");
                        lnlAddOption.Visible = true;
                    }

                    if (DataExist != "0")
                    {
                        lbtnDelete.Visible = false;
                    }
                    else
                    {
                        lbtnDelete.Visible = true;
                    }

                    if (IWRS_DM_SYNC == "True")
                    {
                        lbtnUNSYNC.Visible = true;
                    }
                    else
                    {
                        lbtnSync.Visible = true;
                    }

                    if (IWRS == "True")
                    {
                        LbtnUndo.Visible = false;
                    }
                    else if (IWRS == "False" || IWRS == "")
                    {
                        lbtnSetFieldValues.Visible = false;
                        lbtnSync.Visible = false;
                        lbtnDelete.Visible = false;
                        LbtnUndo.Visible = true;
                    }

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtnDelete.Enabled = false;
                        lbtnDelete.ToolTip = "Configuration has been Frozen";
                        lbtnDelete.OnClientClick = "return ConfigFrozen_MSG()";

                        lbtnSync.Enabled = false;
                        lbtnSync.ToolTip = "Configuration has been Frozen";
                        lbtnSync.OnClientClick = "return ConfigFrozen_MSG()";

                        lbtnUNSYNC.Enabled = false;
                        lbtnUNSYNC.ToolTip = "Configuration has been Frozen";
                        lbtnUNSYNC.OnClientClick = "return ConfigFrozen_MSG()";

                        LbtnUndo.Enabled = false;
                        LbtnUndo.ToolTip = "Configuration has been Frozen";
                        LbtnUndo.OnClientClick = "return ConfigFrozen_MSG()";

                        lbtnSetFieldValues.Enabled = false;
                        lbtnSetFieldValues.ToolTip = "Configuration has been Frozen";
                        lbtnSetFieldValues.OnClientClick = "return ConfigFrozen_MSG()";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }

        }

        protected void grdField_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                ViewState["ID"] = e.CommandArgument.ToString();
                if (e.CommandName == "EditField")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "SELECT_FIELDS",
                        FORMID: HDNFORMID.Value,
                        ID: e.CommandArgument.ToString()
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtFieldName.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                        lblVariableName.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();
                        txtMaxLength.Text = ds.Tables[0].Rows[0]["MAXLEN"].ToString();
                        txtSeqno.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

                        if (ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString() == "TEXTBOX")
                        {
                            if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("OptionValues"))
                            {
                                drpSCControl.SelectedValue = "TEXTBOX with Options";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("txtDateNoFuture"))
                            {
                                drpSCControl.SelectedValue = "DATENOFUTURE";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("txtDateMask"))
                            {
                                drpSCControl.SelectedValue = "DATEINPUTMASK";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("txtDate"))
                            {
                                drpSCControl.SelectedValue = "DATE";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("numericdecimal"))
                            {
                                drpSCControl.SelectedValue = "DECIMAL";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("numeric"))
                            {
                                drpSCControl.SelectedValue = "NUMERIC";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("txtTime"))
                            {
                                drpSCControl.SelectedValue = "TIME";
                            }
                            else if (ds.Tables[0].Rows[0]["CLASS"].ToString().Contains("ckeditor"))
                            {
                                drpSCControl.SelectedValue = "HTML TEXTBOX";
                            }
                            else
                            {
                                drpSCControl.SelectedValue = ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString();
                            }
                        }
                        else
                        {
                            drpSCControl.SelectedValue = ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString();
                        }

                        ShowHideFormat();

                        txtFormat.Text = ds.Tables[0].Rows[0]["FORMAT"].ToString();

                        if (ds.Tables[0].Rows[0]["BOLDYN"].ToString() == "True")
                        {
                            chkBold.Checked = true;
                        }
                        else
                        {
                            chkBold.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["READYN"].ToString() == "True")
                        {
                            chkRead.Checked = true;
                        }
                        else
                        {
                            chkRead.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["MULTILINEYN"].ToString() == "True")
                        {
                            chkMultiline.Checked = true;
                        }
                        else
                        {
                            chkMultiline.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["UNLNYN"].ToString() == "True")
                        {
                            chkUnderline.Checked = true;
                        }
                        else
                        {
                            chkUnderline.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["INVISIBLE"].ToString() == "True")
                        {
                            chkInvisible.Checked = true;
                        }
                        else
                        {
                            chkInvisible.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["UPPERCASE"].ToString() == "True")
                        {
                            chkUppercase.Checked = true;
                        }
                        else
                        {
                            chkUppercase.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["MANDATORY"].ToString() == "True")
                        {
                            chkMandatory.Checked = true;
                        }
                        else
                        {
                            chkMandatory.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["RPT_VISITSUM"].ToString() == "True")
                        {
                            chkVisitSummary.Checked = true;
                        }
                        else
                        {
                            chkVisitSummary.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["RPT_DOSESUM_B"].ToString() == "True")
                        {
                            chkDosingSummary.Checked = true;
                        }
                        else
                        {
                            chkDosingSummary.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["RPT_DOSESUM_U"].ToString() == "True")
                        {
                            chkDosingSummary_U.Checked = true;
                        }
                        else
                        {
                            chkDosingSummary_U.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["RPT_RANDKITSUM"].ToString() == "True")
                        {
                            chkRandomizationKitSummary.Checked = true;
                        }
                        else
                        {
                            chkRandomizationKitSummary.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["RPT_RANDTRTSUM"].ToString() == "True")
                        {
                            chkRandomizationTreatmentSummary.Checked = true;
                        }
                        else
                        {
                            chkRandomizationTreatmentSummary.Checked = false;
                        }

                        txtDescrip.Text = ds.Tables[0].Rows[0]["Descrip"].ToString();

                        hfFieldColor.Value = ds.Tables[0].Rows[0]["FieldColor"].ToString();
                        hfAnsColor.Value = ds.Tables[0].Rows[0]["AnsColor"].ToString();

                        if (ds.Tables[0].Rows[0]["DefaultData"].ToString() != "")
                        {
                            txtDefaultData.Text = ds.Tables[0].Rows[0]["DefaultData"].ToString();
                            chkDefault.Checked = true;
                            txtDefaultData.Visible = true;
                        }
                        else
                        {
                            txtDefaultData.Text = "";
                            chkDefault.Checked = false;
                            txtDefaultData.Visible = false;
                        }

                        btnUpdateField.Visible = true;
                        btnCancelField.Visible = true;
                    }
                }
                else if (e.CommandName == "DELETEFIELD")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(
                        ACTION: "DELETE_FEILDS",
                        FORMID: HDNFORMID.Value,
                        ID: e.CommandArgument.ToString()
                        );
                    Response.Write("<script> alert('Deleted Successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
                    GetField();
                }
                else if (e.CommandName == "Sync")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(
                        ACTION: "SYNC_FEILDS",
                        FORMID: HDNFORMID.Value,
                        ID: e.CommandArgument.ToString());
                    Response.Write("<script> alert('Sync Enabled Successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
                    GetField();
                }
                else if (e.CommandName == "UNSYNC")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(
                        ACTION: "UNSYNC_FEILDS",
                        FORMID: HDNFORMID.Value,
                        ID: e.CommandArgument.ToString());

                    Response.Write("<script> alert('Sync Disabled Successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
                    GetField();
                }
                else if (e.CommandName == "UNDOFIELD")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(
                        ACTION: "UNDO_FIELDS",
                        FORMID: HDNFORMID.Value,
                        ID: e.CommandArgument.ToString());
                    Response.Write("<script> alert('Restore Successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
                    GetField();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdField_PreRender(object sender, EventArgs e)
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