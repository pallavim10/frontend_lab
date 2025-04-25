using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class NSAE_PROJECTFIELD : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        public string FieldColorValue = "#000000";
        public string AnsColorValue = "#000000";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    GetModule();
                    GetField();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        public void GetField()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                                    ACTION: "GET_FIELD_SAE_PROJECT_MASTER",
                                    MODULEID: drpModule.SelectedValue
                                    );

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
                throw ex;
            }
        }

        public void GetModule()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                            ACTION: "GET_MODULENAME_Safety"
                            );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds.Tables[0];
                    drpModule.DataValueField = "ID";
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetField();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
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

                if (!chkInList.Checked)
                {
                    chkInlistEditable.Checked = false;
                }

                DataSet ds = dal_SAE.SAE_SETUP_SP
                (
                ACTION: "UPDATE_SAE_PROJECT_MASTER",
                ID: Session["ID"].ToString(),
                FIELDNAME: txtFieldName.Text,
                VARIABLENAME: lblVariableName.Text,
                CONTROLTYPE: drpSCControl.SelectedValue,
                MAXLEN: txtMaxLength.Text,
                BOLDYN: chkBold.Checked,
                READYN: chkRead.Checked,
                MULTILINEYN: chkMultiline.Checked,
                UNLNYN: chkUnderline.Checked,
                UPPERCASE: chkUppercase.Checked,
                SEQNO: txtSeqno.Text,
                REQUIREDYN: chkRequired.Checked,
                INVISIBLE: chkInvisible.Checked,
                InList: chkInList.Checked,
                InListEditable: chkInlistEditable.Checked,
                LabData: chkLab.Checked,
                FieldColor: hfFieldColor.Value,
                AnsColor: hfAnsColor.Value,
                AutoNum: chkAutoNum.Checked,
                Critic_DP: chkCriticDP.Checked,
                Descrip: txtDescrip.Text,
                Prefix: chkPrefix.Checked,
                PrefixText: txtPrefix.Text,
                DUPLICATE: chkduplicate.Checked,
                NONREPETATIVE: chknonRepetative.Checked,
                MANDATORY: chkMandatory.Checked,
                MEDICAL_REVIEW: chkMedicalReview.Checked,
                DefaultData: DefaultData,
                ParentLinked: chkParentLinked.Checked,
                ChildLinked: chkChildLinked.Checked,
                MEDOP: chkMEDOP.Checked
                );

                btnSubmitField.Visible = true;
                btnUpdateField.Visible = false;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field Updated Successfully')", true);
                ClearFieldSection();
                GetField();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void ClearFieldSection()
        {
            try
            {
                txtFieldName.Text = "";
                lblVariableName.Text = "";
                txtMaxLength.Text = "";
                txtSeqno.Text = "";
                //txtTableName.Text = "";
                //lblVariableName.Text = "";
                drpSCControl.SelectedIndex = 0;
                chkBold.Checked = false;
                chkUnderline.Checked = false;
                //chkContinue.Checked = false;
                chkMultiline.Checked = false;
                chkRead.Checked = false;
                chkUppercase.Checked = false;
                chkRequired.Checked = false;
                chkInvisible.Checked = false;
                chkInList.Checked = false;
                ShowHideInlistEditable();
                chkInlistEditable.Checked = false;
                chkLab.Checked = false;
                chkPrefix.Checked = false;
                txtPrefix.Visible = false;
                txtPrefix.Text = "";
                chkAutoNum.Checked = false;
                chkREF.Checked = false;
                chkCriticDP.Checked = false;
                chkduplicate.Checked = false;
                chknonRepetative.Checked = false;
                chkMandatory.Checked = false;
                txtDescrip.Text = "";
                chkMedicalReview.Checked = false;

                chkDefault.Checked = false;
                chkParentLinked.Checked = false;
                chkChildLinked.Checked = false;
                chkMEDOP.Checked = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdField_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                string id = e.CommandArgument.ToString();
                Session["ID"] = id;
                if (e.CommandName == "EditField")
                {
                    DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_SAE_PROJECT_MASTER_BYID", ID: Session["ID"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        drpModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();

                        txtFieldName.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                        lblVariableName.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();
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
                        drpSCControl_SelectedIndexChanged(this, e);
                        ShowHideInlistEditable();

                        txtMaxLength.Text = ds.Tables[0].Rows[0]["MAXLEN"].ToString();
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
                        if (ds.Tables[0].Rows[0]["REQUIREDYN"].ToString() == "True")
                        {
                            chkRequired.Checked = true;
                        }
                        else
                        {
                            chkRequired.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["INVISIBLE"].ToString() == "True")
                        {
                            chkInvisible.Checked = true;
                        }
                        else
                        {
                            chkInvisible.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["InList"].ToString() == "True")
                        {
                            chkInList.Checked = true;
                        }
                        else
                        {
                            chkInList.Checked = false;
                        }
                        
                        if (ds.Tables[0].Rows[0]["InListEditable"].ToString() == "True")
                        {
                            chkInlistEditable.Checked = true;
                        }
                        else
                        {
                            chkInlistEditable.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["LabData"].ToString() == "True")
                        {
                            chkLab.Checked = true;
                        }
                        else
                        {
                            chkLab.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["UPPERCASE"].ToString() == "True")
                        {
                            chkUppercase.Checked = true;
                        }
                        else
                        {
                            chkUppercase.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["AUTONUM"].ToString() == "True")
                        {
                            chkAutoNum.Checked = true;
                        }
                        else
                        {
                            chkAutoNum.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["REFER"].ToString() == "True")
                        {
                            chkREF.Checked = true;
                        }
                        else
                        {
                            chkREF.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["Critic_DP"].ToString() == "True")
                        {
                            chkCriticDP.Checked = true;
                        }
                        else
                        {
                            chkCriticDP.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["DUPLICATE"].ToString() == "True")
                        {
                            chkduplicate.Checked = true;
                        }
                        else
                        {
                            chkduplicate.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["DefaultData"].ToString() == "True")
                        {
                            chkDefault.Checked = true;
                        }
                        else
                        {
                            chkDefault.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["ParentLinked"].ToString() == "True")
                        {
                            chkParentLinked.Checked = true;
                        }
                        else
                        {
                            chkParentLinked.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["ChildLinked"].ToString() == "True")
                        {
                            chkChildLinked.Checked = true;
                        }
                        else
                        {
                            chkChildLinked.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["MEDOP"].ToString() == "True")
                        {
                            chkMEDOP.Checked = true;
                        }
                        else
                        {
                            chkMEDOP.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["NONREPETATIVE"].ToString() == "True")
                        {
                            chknonRepetative.Checked = true;
                        }
                        else
                        {
                            chknonRepetative.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["MANDATORY"].ToString() == "True")
                        {
                            chkMandatory.Checked = true;
                        }
                        else
                        {
                            chkMandatory.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["MEDICAL_REVIEW"].ToString() == "True")
                        {
                            chkMedicalReview.Checked = true;
                        }
                        else
                        {
                            chkMedicalReview.Checked = false;
                        }

                        txtDescrip.Text = ds.Tables[0].Rows[0]["Descrip"].ToString();

                        if (ds.Tables[0].Rows[0]["Prefix"].ToString() == "True")
                        {
                            chkPrefix.Checked = true;
                            txtPrefix.Visible = true;
                            txtPrefix.Text = ds.Tables[0].Rows[0]["PrefixText"].ToString();
                        }
                        else
                        {
                            chkPrefix.Checked = false;
                            txtPrefix.Visible = false;
                            txtPrefix.Text = "";
                        }

                        FieldColorValue = ds.Tables[0].Rows[0]["FieldColor"].ToString();
                        AnsColorValue = ds.Tables[0].Rows[0]["AnsColor"].ToString();

                        hfFieldColor.Value = ds.Tables[0].Rows[0]["FieldColor"].ToString();
                        hfAnsColor.Value = ds.Tables[0].Rows[0]["AnsColor"].ToString();

                        btnUpdateField.Visible = true;
                        btnCancelField.Visible = true;
                        btnSubmitField.Visible = false;

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "DisableDiv();", true);
                    }
                }
                else if (e.CommandName == "DeleteField")
                {


                    DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "DELETE_SAE_PROJECT_MASTER", ID: Session["ID"].ToString());
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field deleted Successfully')", true);
                    GetField();
                    //Response.Write("<script>alert('Variable Name deleted Successfully')</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                    string VariableExist = dr["VariableExist"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    if (CONTROLTYPE == "DROPDOWN" || CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "CHECKBOX" || CLASS.Contains("OptionValues form-control"))
                    {
                        if (VariableExist != "0")
                        {
                            LinkButton lnlAddOption = (LinkButton)e.Row.FindControl("lnlAddOption");
                            lnlAddOption.Visible = false;
                        }
                        else
                        {
                            LinkButton lnlAddOption = (LinkButton)e.Row.FindControl("lnlAddOption");
                            lnlAddOption.Visible = true;
                        }
                    }

                    if (DataExist != "0")
                    {
                        LinkButton lbtnupdate = (LinkButton)e.Row.FindControl("lbtnupdate");
                        lbtnupdate.Visible = false;

                        LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");
                        lbtndeleteSection.Visible = false;

                        LinkButton lnlAddOption = (LinkButton)e.Row.FindControl("lnlAddOption");
                        lnlAddOption.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }

        }

        protected void chkInList_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ShowHideInlistEditable();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ShowHideInlistEditable()
        {
            try
            {
                if (chkInList.Checked)
                {
                    divInlistEditable.Visible = true;
                }
                else
                {
                    divInlistEditable.Visible = false;
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

        protected void chkParentLinked_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkParentLinked.Checked)
                {
                    chkChildLinked.Enabled = false;
                }
                else
                {
                    chkChildLinked.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkChildLinked_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkChildLinked.Checked)
                {
                    chkParentLinked.Enabled = false;
                }
                else
                {
                    chkParentLinked.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void chkAutoCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCode.Checked)
                {
                    drpAutoCode.Visible = true;
                    drpAutoCode.SelectedValue = "0";
                }
                else
                {
                    drpAutoCode.Visible = false;
                    drpAutoCode.SelectedValue = "0";
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

        protected void chkPrefix_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPrefix.Checked)
                {
                    txtPrefix.Visible = true;
                }
                else
                {
                    txtPrefix.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSCControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearAllFeatures();
                switch (drpSCControl.SelectedItem.Text)
                {
                    case "CHECKBOX":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //---Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //---Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //--DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = true;
                        lblDataEntry.Visible = false;
                        //---DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;

                        break;

                    case "TEXTBOX":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = true;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = true;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = true;       //AutoCode
                        DivLinkedDataflowField.Visible = true;
                        DivProtocalPredefineData.Visible = true;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = true;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = true;
                        break;
                    case "TEXTBOX with Options":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = true;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = true;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "HTML TEXTBOX":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = true;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = true;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = true;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "RADIOBUTTON":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = true;
                        lblDataEntry.Visible = false;
                        break;
                    case "DROPDOWN":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = true;
                        DivLinkedFieldChild.Visible = true;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = true;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "TIME":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = false;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;

                        break;
                    case "NUMERIC":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = true;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "DATE without Futuristic Date":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = false;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "DECIMAL":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;

                        break;
                    case "HEADER":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = false;
                        DivReadonly.Visible = false;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = false;
                        DivMandatoryInfo.Visible = false;
                        DivCriticalDP.Visible = false;
                        DivMedicalAuthRes.Visible = false;
                        DivDuplicatesChkInfo.Visible = false;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = false;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = true;
                        lblDataLinkages.Visible = true;
                        lblDataEntry.Visible = true;
                        break;
                    case "DATE":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = false;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;

                        break;
                    case "DATE with Input Mask":
                        divfeature.Visible = true;
                        divSignificant.Visible = true;
                        divDataLinkages.Visible = true;
                        DivDataEntry.Visible = true;
                        //Features
                        divBOLD.Visible = true;
                        divMaskField.Visible = true;
                        DivReadonly.Visible = false;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        DivDuplicatesChkInfo.Visible = true;
                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;

                    default:
                        divfeature.Visible = false;
                        divSignificant.Visible = false;
                        divDataLinkages.Visible = false;
                        DivDataEntry.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;

                        break;
                }

                ShowHideFormat();


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
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

        public void ClearAllFeatures()
        {
            try
            {
                txtFormat.Text = "";
                txtMaxLength.Text = "";
                chkUppercase.Checked = false;
                chkBold.Checked = false;
                chkUnderline.Checked = false;
                chkRead.Checked = false;
                chkMultiline.Checked = false;
                chkRequired.Checked = false;
                chkInvisible.Checked = false;
                chkAutoCode.Checked = false;
                drpAutoCode.SelectedIndex = 0;
                drpAutoCode.Visible = false;
                chkInList.Checked = false;
                chkInlistEditable.Checked = false;
                divInlistEditable.Visible = false;
                chkLab.Checked = false;
                chkAutoNum.Checked = false;
                chkREF.Checked = false;
                chkCriticDP.Checked = false;
                chkPrefix.Checked = false;
                txtPrefix.Text = "";
                txtPrefix.Visible = false;
                chkduplicate.Checked = false;
                chknonRepetative.Checked = false;
                chkMandatory.Checked = false;
                chkDefault.Checked = false;
                txtDefaultData.Text = "";
                txtDefaultData.Visible = false;
                chkParentLinked.Checked = false;
                chkChildLinked.Checked = false;
                chkParentLinked.Enabled = true;
                chkChildLinked.Enabled = true;
                chkMEDOP.Checked = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelField_Click(object sender, EventArgs e)
        {
            ClearFieldSection();
        }
    }
}