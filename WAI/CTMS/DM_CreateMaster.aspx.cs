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
    public partial class DM_CreateMaster : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        public string FieldColorValue = "#000000";
        public string AnsColorValue = "#000000";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescrip.Attributes.Add("MaxLength", "2000");
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                {
                    GetMasterModules();
                    GetField();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GetMasterModules()
        {
            try
            {
                DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "GET_MASTER_MODULES");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string Values = "";
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Values += "" + ds.Tables[0].Rows[i]["MODULENAME"].ToString() + "¸";
                        }
                    }

                    hfValue1.Value = Values.TrimEnd('¸');

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "bindOptionValues();", true);
                }
                else
                {
                    txtModuleName.Text = "";
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
                DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "GET_MASTERFIELD", DOMAIN: txtDomain.Text);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdField.DataSource = ds.Tables[0];
                    grdField.DataBind();
                    btnExportExcel.Visible = true;
                }
                else
                {
                    grdField.DataSource = null;
                    grdField.DataBind();
                    btnExportExcel.Visible = false;
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

        private void GET_LINKMODULES()
        {
            try
            {
                DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "GET_MASTER_MODULES");

                if (drpSCControl.SelectedValue == "ChildModule" && ds.Tables[0].Rows.Count > 1)
                {
                    DataSet db = dal_DB.DB_MASTER_SP(ACTION: "GET_MODULES");
                    ddlChildModule.DataSource = db.Tables[0];
                    ddlChildModule.DataTextField = "MODULENAME";
                    ddlChildModule.DataValueField = "ID";
                    ddlChildModule.DataBind();
                    ddlChildModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (drpSCControl.SelectedValue == "ChildModule" && (ds.Tables[0].Rows.Count == 1)) 
                {
                    ddlChildModule.Items.Clear();
                    lblErrorMsg.Text = "Linked Module option is only available when more than one module are entered. Please, select another Control Type.";
                }
                else
                {

                    ddlChildModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
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
                DIVLinkedModule.Visible = false;
                hfFieldColor.Value = "";
                hfAnsColor.Value = "";
                lblErrorMsg.Text = "";
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
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
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
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
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
                        DivLabReferance.Visible = true;
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
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = false;
                        DivPrefix.Visible = false;
                        lblSignificant.Visible = true;
                        lblDataLinkages.Visible = false;
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
                    case "Child Module":
                        DIVLinkedModule.Visible = true;
                        divfeature.Visible = false;
                        divSignificant.Visible = false;
                        divDataLinkages.Visible = false;
                        DivDataEntry.Visible = false;
                        //Features
                        divBOLD.Visible = false;
                        divMaskField.Visible = false;
                        DivReadonly.Visible = false;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = false;
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
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = false;
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

                if (hdnOldControlType.Value == "CHECKBOX" || hdnOldControlType.Value == "RADIOBUTTON" || hdnOldControlType.Value == "DROPDOWN" || hdnOldControlType.Value == "ChildModule")
                {
                    if (hdnOldControlType.Value == "DROPDOWN" && (drpSCControl.SelectedValue == "CHECKBOX" || drpSCControl.SelectedValue == "RADIOBUTTON"))
                    {
                        dal_DB.DB_MASTER_SP(ACTION: "DELETE_SELECT_OPTIONS_BY_CHANGING_CONTROL",
                            VARIABLENAME: txtVariableName.Text,
                            DOMAIN: txtDomain.Text
                            );
                    }
                    else if (hdnOldControlType.Value != "DROPDOWN" && drpSCControl.SelectedValue == "DROPDOWN")
                    {
                        dal_DB.DB_MASTER_SP(ACTION: "ADD_SELECT_OPTIONS_BY_CHANGING_CONTROL",
                            VARIABLENAME: txtVariableName.Text,
                            DOMAIN: txtDomain.Text
                            );
                    }
                    else if (drpSCControl.SelectedValue != "CHECKBOX" && drpSCControl.SelectedValue != "RADIOBUTTON" && drpSCControl.SelectedValue != "DROPDOWN")
                    {
                        dal_DB.DB_MASTER_SP(ACTION: "DELETE_OPTIONS_BY_CHANGING_CONTROL",
                            VARIABLENAME: txtVariableName.Text,
                            DOMAIN: txtDomain.Text
);
                    }
                    else if (drpSCControl.SelectedValue == "ChildModule" && hdnOldControlType.Value != "ChildModule")
                    {
                        GET_LINKMODULES();
                    }
                }
                if (drpSCControl.SelectedValue == "ChildModule")
                {
                    GET_LINKMODULES();
                }

                hdnOldControlType.Value = drpSCControl.SelectedValue;
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
                if (drpSCControl.SelectedValue == "TEXTBOX" || drpSCControl.SelectedValue == "HTML TEXTBOX" || drpSCControl.SelectedValue == "NUMERIC")
                {
                    DIVmaxLenght.Visible = true;
                }
                else
                {
                    DIVmaxLenght.Visible = false;
                }
                if (drpSCControl.SelectedValue == "ChildModule")
                {
                    DIVLinkedModule.Visible = true;
                }
                else 
                {
                    DIVLinkedModule.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitField_Click(object sender, EventArgs e)
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

                string AUTOCODELIB = "";
                if (drpAutoCode.SelectedValue == "0")
                {
                    AUTOCODELIB = "";
                }
                else
                {
                    AUTOCODELIB = drpAutoCode.SelectedValue;
                }

                if (drpSCControl.SelectedValue == "HTML TEXTBOX" && chkMultiline.Checked == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check Freetext');", true);
                }
                else
                {
                    DataSet dsDomain = dal_DB.DB_MASTER_SP(ACTION: "CHECK_DOMAIN_EXISTS_OR_NOT", DOMAIN: txtDomain.Text, MODULENAME: txtModuleName.Text);

                    if (dsDomain.Tables.Count > 0 && dsDomain.Tables[0].Rows.Count > 0)
                    {
                        Response.Write("<script>alert('Domain name exists.');</script>");
                    }
                    else
                    {
                        if ((txtModuleName.Text != hdnoldModulename.Value || txtModuleSeqNo.Text != hdnOldModuleSeqNo.Value || txtDomain.Text != hdnOldDomain.Value) && grdField.Rows.Count > 0 && (hdnoldModulename.Value != "" && hdnOldModuleSeqNo.Value != "" && hdnOldDomain.Value != ""))
                        {
                            ModalPopupExtender2.Show();
                        }
                        else
                        {
                            DataSet dsVar = dal_DB.DB_MASTER_SP(ACTION: "CHECK_FIELD_VARIABLE",
                               MODULENAME: txtModuleName.Text,
                               VARIABLENAME: txtVariableName.Text
                               );

                            if (dsVar.Tables.Count > 0 && dsVar.Tables[0].Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Variable name already exists. Please change variable name.');", true);
                            }
                            else
                            {
                                DataSet ds = dal_DB.DB_MASTER_SP
                                (
                                ACTION: "INSERT_MASTER_MODUEL_FIELDS",
                                MODULENAME: txtModuleName.Text,
                                DOMAIN: txtDomain.Text,
                                MODULE_SEQNO: txtModuleSeqNo.Text,
                                FIELDNAME: txtFieldName.Text,
                                VARIABLENAME: txtVariableName.Text,
                                Descrip: txtDescrip.Text,
                                CONTROLTYPE: drpSCControl.SelectedValue,
                                FORMAT: txtFormat.Text,
                                SEQNO: txtFieldSeqno.Text,
                                MAXLEN: txtMaxLength.Text,
                                FieldColor: Request.Form["FieldColor"],
                                AnsColor: Request.Form["AnsColor"],
                                UPPERCASE: chkUppercase.Checked,
                                BOLDYN: chkBold.Checked,
                                UNLNYN: chkUnderline.Checked,
                                READYN: chkRead.Checked,
                                MULTILINEYN: chkMultiline.Checked,
                                REQUIREDYN: chkRequired.Checked,
                                INVISIBLE: chkInvisible.Checked,
                                AUTOCODE: chkAutoCode.Checked,
                                AutoCodeLIB: AUTOCODELIB,
                                InList: chkInList.Checked,
                                InListEditable: chkInlistEditable.Checked,
                                LabData: chkLab.Checked,
                                AutoNum: chkAutoNum.Checked,
                                Refer: chkREF.Checked,
                                Critic_DP: chkCriticDP.Checked,
                                Prefix: chkPrefix.Checked,
                                PrefixText: txtPrefix.Text,
                                DUPLICATE: chkduplicate.Checked,
                                NONREPETATIVE: chknonRepetative.Checked,
                                MANDATORY: chkMandatory.Checked,
                                DefaultData: DefaultData,
                                ParentLinked: chkParentLinked.Checked,
                                ChildLinked: chkChildLinked.Checked,
                                MEDOP: chkMEDOP.Checked,
                                LINKEDMODULEID: ddlChildModule.SelectedValue
                                );

                                btnSubmitField.Visible = true;
                                btnUpdateField.Visible = false;

                                Response.Write("<script> alert('" + txtFieldName.Text + " added successfully.'); window.location.href='DM_CreateMaster.aspx' </script>");
                            }
                        }
                    }
                }
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

                string AUTOCODELIB = "";
                if (drpAutoCode.SelectedValue == "0")
                {
                    AUTOCODELIB = "";
                }
                else
                {
                    AUTOCODELIB = drpAutoCode.SelectedValue;
                }

                if (drpSCControl.SelectedValue == "HTML TEXTBOX" && chkMultiline.Checked == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check Freetext');", true);
                }
                else
                {
                    if (txtModuleName.Text != hdnoldModulename.Value || txtModuleSeqNo.Text != hdnOldModuleSeqNo.Value || txtDomain.Text != hdnOldDomain.Value)
                    {
                        ModalPopupExtender2.Show();
                    }
                    else
                    {
                        DataSet dsVar = dal_DB.DB_MASTER_SP(ACTION: "CHECK_FIELD_VARIABLE",
                           MODULENAME: txtModuleName.Text,
                           VARIABLENAME: txtVariableName.Text,
                           ID: Session["ID"].ToString()
                           );

                        if (dsVar.Tables.Count > 0 && dsVar.Tables[0].Rows.Count > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Variable name already exists. Please change variable name.');", true);
                        }
                        else
                        {
                            DataSet ds = dal_DB.DB_MASTER_SP
                                (
                                ACTION: "UPDATE_MASTER_MODUEL_FIELDS",
                                ID: Session["ID"].ToString(),
                                MODULENAME: txtModuleName.Text,
                                DOMAIN: txtDomain.Text,
                                MODULE_SEQNO: txtModuleSeqNo.Text,
                                FIELDNAME: txtFieldName.Text,
                                VARIABLENAME: txtVariableName.Text,
                                Descrip: txtDescrip.Text,
                                CONTROLTYPE: drpSCControl.SelectedValue,
                                FORMAT: txtFormat.Text,
                                SEQNO: txtFieldSeqno.Text,
                                MAXLEN: txtMaxLength.Text,
                                FieldColor: Request.Form["FieldColor"],
                                AnsColor: Request.Form["AnsColor"],
                                UPPERCASE: chkUppercase.Checked,
                                BOLDYN: chkBold.Checked,
                                UNLNYN: chkUnderline.Checked,
                                READYN: chkRead.Checked,
                                MULTILINEYN: chkMultiline.Checked,
                                REQUIREDYN: chkRequired.Checked,
                                INVISIBLE: chkInvisible.Checked,
                                AUTOCODE: chkAutoCode.Checked,
                                AutoCodeLIB: AUTOCODELIB,
                                InList: chkInList.Checked,
                                InListEditable: chkInlistEditable.Checked,
                                LabData: chkLab.Checked,
                                AutoNum: chkAutoNum.Checked,
                                Refer: chkREF.Checked,
                                Critic_DP: chkCriticDP.Checked,
                                Prefix: chkPrefix.Checked,
                                PrefixText: txtPrefix.Text,
                                DUPLICATE: chkduplicate.Checked,
                                NONREPETATIVE: chknonRepetative.Checked,
                                MANDATORY: chkMandatory.Checked,
                                DefaultData: DefaultData,
                                ParentLinked: chkParentLinked.Checked,
                                ChildLinked: chkChildLinked.Checked,
                                MEDOP: chkMEDOP.Checked,
                                LINKEDMODULEID: ddlChildModule.SelectedValue
                                );

                            btnSubmitField.Visible = true;
                            btnUpdateField.Visible = false;

                            Response.Write("<script> alert('" + txtFieldName.Text + " updated successfully.'); window.location.href='DM_CreateMaster.aspx' </script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Remove("ID");
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnCancelField_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdField_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                string id = e.CommandArgument.ToString();
                Session["ID"] = id;

                if (e.CommandName == "EditField")
                {
                    DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "GET_MODULE_FIELD_MASTER_BYID", ID: Session["ID"].ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtModuleName.Text = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                        hdnoldModulename.Value = ds.Tables[0].Rows[0]["MODULENAME"].ToString();

                        txtDomain.Text = ds.Tables[0].Rows[0]["DOMAIN"].ToString();
                        hdnOldDomain.Value = ds.Tables[0].Rows[0]["DOMAIN"].ToString();

                        txtFieldName.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                        txtVariableName.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();


                        txtModuleSeqNo.Text = ds.Tables[0].Rows[0]["MODULE_SEQNO"].ToString();
                        hdnOldModuleSeqNo.Value = ds.Tables[0].Rows[0]["MODULE_SEQNO"].ToString();

                        txtFieldSeqno.Text = ds.Tables[0].Rows[0]["FIELD_SEQNO"].ToString();

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

                        // Handle LINKED MODULE logic
                        if (ds.Tables[0].Rows[0]["CONTROLTYPE"].ToString() == "ChildModule")
                        {
                            ddlChildModule.Visible = true;
                            GET_LINKMODULES();

                            // Populate the Linked Module dropdown value
                            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LINKED_MODULEID"].ToString()))
                            {
                                ddlChildModule.SelectedValue = ds.Tables[0].Rows[0]["LINKED_MODULEID"].ToString();
                            }
                        }
                        else
                        {
                            ddlChildModule.Visible = false;
                        }

                        hdnOldControlType.Value = drpSCControl.SelectedValue;
                        drpSCControl_SelectedIndexChanged(this, e);
                        ShowHideFormat();

                        txtFormat.Text = ds.Tables[0].Rows[0]["FORMAT"].ToString();
                        txtMaxLength.Text = ds.Tables[0].Rows[0]["MAXLEN"].ToString();
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

                        if (ds.Tables[0].Rows[0]["AUTOCODE"].ToString() == "True")
                        {
                            chkAutoCode.Checked = true;
                            drpAutoCode.Visible = true;

                            if (ds.Tables[0].Rows[0]["AutoCodeLIB"].ToString() != "")
                            {
                                drpAutoCode.SelectedValue = ds.Tables[0].Rows[0]["AutoCodeLIB"].ToString();
                            }
                        }
                        else
                        {
                            chkAutoCode.Checked = false;
                            drpAutoCode.Visible = false;
                            drpAutoCode.SelectedIndex = 0;
                        }

                        if (ds.Tables[0].Rows[0]["InList"].ToString() == "True")
                        {
                            chkInList.Checked = true;
                        }
                        else
                        {
                            chkInList.Checked = false;
                        }

                        ShowHideInlistEditable();

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

                        if (ds.Tables[0].Rows[0]["ParentLinked"].ToString() == "True")
                        {
                            chkParentLinked.Checked = true;
                            chkChildLinked.Enabled = false;
                        }
                        else
                        {
                            chkParentLinked.Checked = false;
                            chkChildLinked.Enabled = true;
                        }

                        if (ds.Tables[0].Rows[0]["ChildLinked"].ToString() == "True")
                        {
                            chkChildLinked.Checked = true;
                            chkParentLinked.Enabled = false;
                        }
                        else
                        {
                            chkChildLinked.Checked = false;
                            chkParentLinked.Enabled = true;
                        }

                        if (ds.Tables[0].Rows[0]["MEDOP"].ToString() == "True")
                        {
                            chkMEDOP.Checked = true;
                        }
                        else
                        {
                            chkMEDOP.Checked = false;
                        }

                        btnUpdateField.Visible = true;
                        btnSubmitField.Visible = false;
                    }
                }
                else if (e.CommandName == "DeleteField")
                {
                    string variablename = (gvr.FindControl("VARIABLENAME") as Label).Text;
                    string FIELDNAME = (gvr.FindControl("FIELDNAME") as Label).Text;
                    DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "DELETE_MODULE_FIELD_MASTER",
                        ID: Session["ID"].ToString(),
                        MODULENAME: txtModuleName.Text,
                        VARIABLENAME: variablename
                        );

                    Response.Write("<script> alert('" + FIELDNAME + " deleted successfully.'); window.location.href='DM_CreateMaster.aspx' </script>");
                }
            }
            catch (Exception ex)
            {
                Session.Remove("ID");
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView1_PreRender(object sender, EventArgs e)
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

        protected void grdField_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string CLASS = dr["CLASS"].ToString();
                    string ChildLinked = dr["ChildLinked"].ToString();
                    LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");

                    if (CONTROLTYPE == "DROPDOWN" || CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "CHECKBOX" || CLASS.Contains("OptionValues form-control"))
                    {
                        if (ChildLinked == "True")
                        {
                            LinkButton lnlAddOption_LINKED = (LinkButton)e.Row.FindControl("lnlAddOption_LINKED");
                            lnlAddOption_LINKED.Visible = true;
                        }
                        else
                        {
                            LinkButton lnlAddOption = (LinkButton)e.Row.FindControl("lnlAddOption");
                            lnlAddOption.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }

        }

        protected void txtModuleName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "GET_MASTER_MODULES_BY_MODULENAME", MODULENAME: txtModuleName.Text);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtDomain.Text = ds.Tables[0].Rows[0]["DOMAIN"].ToString();
                    txtModuleSeqNo.Text = ds.Tables[0].Rows[0]["MODULE_SEQNO"].ToString();
                    hdnoldModulename.Value = txtModuleName.Text;
                    hdnOldModuleSeqNo.Value = txtModuleSeqNo.Text;
                    hdnOldDomain.Value = txtDomain.Text;
                }
                else
                {
                    txtDomain.Text = "";
                    txtModuleSeqNo.Text = "";
                    hdnoldModulename.Value = "";
                    hdnOldModuleSeqNo.Value = "";
                    hdnOldDomain.Value = "";
                }

                GetField();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
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

                DataSet ds = dal_DB.DB_MASTER_SP
                    (
                    ACTION: "UPDATE_MASTER_MODUEL_DETAILS",
                    ID: Convert.ToString(Session["ID"]),
                    MODULENAME: txtModuleName.Text,
                    DOMAIN: txtDomain.Text,
                    MODULE_SEQNO: txtModuleSeqNo.Text,
                    FIELDNAME: txtFieldName.Text,
                    VARIABLENAME: txtVariableName.Text,
                    Descrip: txtDescrip.Text,
                    CONTROLTYPE: drpSCControl.SelectedValue,
                    FORMAT: txtFormat.Text,
                    SEQNO: txtFieldSeqno.Text,
                    MAXLEN: txtMaxLength.Text,
                    FieldColor: Request.Form["FieldColor"],
                    AnsColor: Request.Form["AnsColor"],
                    UPPERCASE: chkUppercase.Checked,
                    BOLDYN: chkBold.Checked,
                    UNLNYN: chkUnderline.Checked,
                    READYN: chkRead.Checked,
                    MULTILINEYN: chkMultiline.Checked,
                    REQUIREDYN: chkRequired.Checked,
                    INVISIBLE: chkInvisible.Checked,
                    AUTOCODE: chkAutoCode.Checked,
                    AutoCodeLIB: drpAutoCode.SelectedValue,
                    InList: chkInList.Checked,
                    InListEditable: chkInlistEditable.Checked,
                    LabData: chkLab.Checked,
                    AutoNum: chkAutoNum.Checked,
                    Refer: chkREF.Checked,
                    Critic_DP: chkCriticDP.Checked,
                    Prefix: chkPrefix.Checked,
                    PrefixText: txtPrefix.Text,
                    DUPLICATE: chkduplicate.Checked,
                    NONREPETATIVE: chknonRepetative.Checked,
                    MANDATORY: chkMandatory.Checked,
                    DefaultData: DefaultData,
                    ParentLinked: chkParentLinked.Checked,
                    ChildLinked: chkChildLinked.Checked,
                    MEDOP: chkMEDOP.Checked,
                    OLDMODULENAME: hdnoldModulename.Value,
                    OLDMODULESEQ: hdnOldModuleSeqNo.Value,
                    OLDDOMAIN: hdnOldDomain.Value,
                    LINKEDMODULEID: ddlChildModule.SelectedValue
                    );

                btnSubmitField.Visible = true;
                btnUpdateField.Visible = false;

                if (Convert.ToString(Session["ID"]) != "")
                {
                    Response.Write("<script> alert('Master Field updated successfully.'); window.location.href='DM_CreateMaster.aspx' </script>");
                }
                else
                {
                    Response.Write("<script> alert('Master Field added successfully.'); window.location.href='DM_CreateMaster.aspx' </script>");

                }
            }
            catch (Exception ex)
            {
                Session.Remove("ID");
                throw ex;
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "GET_MASTERFIELD_EXPORT", MODULENAME: txtModuleName.Text);

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Master Library Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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

        protected void txtVariableName_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtVariableName.Text, "^[a-zA-Z ]"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This textbox accepts only alphabetical characters'); ", true);
                txtVariableName.Text = "";
            }
            else if (txtVariableName.Text.Contains(" "))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter the variable name without spaces'); ", true);
                txtVariableName.Text = "";
            }
            else if (txtVariableName.Text.Contains("-"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter the variable name without -'); ", true);
                txtVariableName.Text = "";
            }
        }
    }
}