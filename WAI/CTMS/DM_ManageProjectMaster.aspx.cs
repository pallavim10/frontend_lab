using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DM_ManageProjectMaster : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        public string FieldColorValue = "#000000";
        public string AnsColorValue = "#000000";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                {
                    GET_VISITS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GET_VISITS()
        {
            try
            {
                DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_VISIT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModuleVisit.DataSource = ds.Tables[0];
                    drpModuleVisit.DataValueField = "VISITNUM";
                    drpModuleVisit.DataTextField = "VISIT";
                    drpModuleVisit.DataBind();
                    drpModuleVisit.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpModuleVisit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpModule.SelectedValue != "0")
                {
                    GetField();
                    drpSCControl.SelectedIndex = 0;
                    drpSCControl_SelectedIndexChanged(this, e);
                    divRecord.Visible = true;
                    drp_PGL_Type.SelectedIndex = 0;
                }
                else
                {
                    divRecord.Visible = false;
                    drpSCControl_SelectedIndexChanged(this, e);
                }

                divRecord.Focus();
                ClearFieldSection();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpModuleVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpModuleVisit.SelectedIndex != 0)
                {
                    GetModule();
                    divshowsystem.Visible = true;
                    divbutton.Visible = true;


                }
                else
                {
                    GetModule();
                    divshowsystem.Visible = false;
                    divbutton.Visible = false;
                    divfeature.Visible = false;
                    divSignificant.Visible = false;
                    divDataLinkages.Visible = false;
                    DivDataEntry.Visible = false;
                }

                ClearFieldSection();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GetModule()
        {
            try
            {
                DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_Added_PROJECT_MASTER",
                    VISITNUM: drpModuleVisit.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds.Tables[0];
                    drpModule.DataValueField = "MODULEID";
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                    divRecord.Visible = false;
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
                DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_FIELD_DM_PROJECT_MASTER",
                    MODULEID: drpModule.SelectedValue,
                    VISITNUM: drpModuleVisit.SelectedValue
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
                drp_PGL_Type.SelectedIndex = 0;
                txtFormat.Text = "";
                txtSeqno.Text = "";
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
                chkInList.Checked = false;
                chkInlistEditable.Checked = false;
                chkLab.Checked = false;
                chkAutoNum.Checked = false;
                chkREF.Checked = false;
                chkCriticDP.Checked = false;
                chkPrefix.Checked = false;
                txtPrefix.Text = "";
                chkduplicate.Checked = false;
                chknonRepetative.Checked = false;
                chkMandatory.Checked = false;
                chkDefault.Checked = false;
                txtDefaultData.Text = "";
                chkParentLinked.Checked = false;
                chkChildLinked.Checked = false;
                chkMEDOP.Checked = false;
                hfFieldColor.Value = "";
                hfAnsColor.Value = "";
                chkEsourceField.Checked = false;
                drp_PGL_Type.SelectedIndex = 0;
                chkSDV.Checked = false;
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
                hfFieldColor.Value = "";
                hfAnsColor.Value = "";
                chkSDV.Checked = false;
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
                        divPGL_Type.Visible = true;

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
                        divPGL_Type.Visible = true;

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
                        divPGL_Type.Visible = true;

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
                        divPGL_Type.Visible = true;

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
                        divPGL_Type.Visible = true;

                        //DataLinkage
                        DivLinkedFieldParent.Visible = false;
                        DivLinkedFieldChild.Visible = false;
                        DivLabReferance.Visible = false;
                        divParent.Visible = false;       //AutoCode
                        DivLinkedDataflowField.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = true;
                        lblDataEntry.Visible = false;
                        //DataEntry
                        DivSeqAutoNumbering.Visible = false;
                        DivNonRepetative.Visible = false;
                        DivInListData.Visible = true;
                        DivPrefix.Visible = false;
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
                        divPGL_Type.Visible = true;

                        //DataLinkage
                        DivLinkedFieldParent.Visible = true;
                        DivLinkedFieldChild.Visible = true;
                        DivLabReferance.Visible = true;
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
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        divPGL_Type.Visible = true;

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
                        divPGL_Type.Visible = true;

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
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        divPGL_Type.Visible = true;

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
                        divPGL_Type.Visible = true;

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
                        lblSignificant.Visible = true;
                        lblDataLinkages.Visible = true;
                        lblDataEntry.Visible = true;
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
                        divPGL_Type.Visible = true;

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
                        divPGL_Type.Visible = true;

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
                        DivReadonly.Visible = true;
                        divUpperCase.Visible = false;
                        DivUnderline.Visible = true;
                        DivFreetext.Visible = false;
                        //Significant
                        DivRequiredInfo.Visible = true;
                        DivMandatoryInfo.Visible = true;
                        DivCriticalDP.Visible = true;
                        DivMedicalAuthRes.Visible = true;
                        divPGL_Type.Visible = true;

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
                        divPGL_Type.Visible = true;

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
                if (drpSCControl.SelectedValue == "TEXTBOX" || drpSCControl.SelectedValue == "HTML TEXTBOX" || drpSCControl.SelectedValue == "NUMERIC")
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

                string AUTOCODELIB = "";
                if (drpAutoCode.SelectedValue == "0")
                {
                    AUTOCODELIB = "";
                }
                else
                {
                    AUTOCODELIB = drpAutoCode.SelectedValue;
                }
                string PGL_TYPE = string.Empty;

                if (drp_PGL_Type.SelectedValue == "0")
                {
                    PGL_TYPE = "";
                }
                else
                {
                    PGL_TYPE = drp_PGL_Type.SelectedValue;
                }

                DataSet ds = dal_DB.DB_DM_SP
                (
                ACTION: "UPDATE_DM_PROJECT_MASTER",
                ID: Session["ID"].ToString(),
                MODULEID: drpModule.SelectedValue,
                MODULENAME: drpModule.SelectedItem.Text,
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
                chk_eSOURCE_FIELD:chkEsourceField.Checked,
                PGL_TYPE: PGL_TYPE,
                SDV:chkSDV.Checked
                );

                btnUpdateField.Visible = false;

                ClearFieldSection();
                GetField();

                Session.Remove("ID");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field updated successfully.');", true);
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
                    string ChildLinked = dr["ChildLinked"].ToString();
                    string CLASS = dr["CLASS"].ToString();
                    string COUNT = dr["COUNT"].ToString();
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

                    if (COUNT != "0")
                    {
                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }


                    HtmlControl iconESource_Field = (HtmlControl)e.Row.FindControl("iconESource_Field");
                    if (dr["eSOURCE_FIELD"].ToString() == "True")
                    {
                        iconESource_Field.Attributes.Add("class", "fa fa-check");
                        iconESource_Field.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconESource_Field.Attributes.Add("class", "fa fa-times");
                        iconESource_Field.Attributes.Add("style", "color: red;");
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
                string VisitNum = (gvr.FindControl("VISITNUM") as Label).Text;
                string VARIABLENAME = (gvr.FindControl("VARIABLENAME") as Label).Text;

                string id = e.CommandArgument.ToString();
                Session["ID"] = id;
                Session["VisitNum"] = VisitNum;
                if (e.CommandName == "EditField")
                {
                    DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_DM_PROJECT_MASTER_BYID",
                        ID: Session["ID"].ToString(),
                        VISITNUM: drpModuleVisit.SelectedValue
                        );

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

                        if (chkRead.Checked == false)
                        {
                            if (ds.Tables[0].Rows[0]["SDV"].ToString() == "True")
                            {
                                chkSDV.Checked = true;
                            }
                            else
                            {
                                chkSDV.Checked = false;
                            }
                        }
                        else
                        {
                            chkSDV.Checked = false;
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

                        hfOLDVARIABLENAME.Value = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();

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

                        if (ds.Tables[0].Rows[0]["eSOURCE_FIELD"].ToString() == "True")
                        {
                            chkEsourceField.Checked = true;
                        }
                        else
                        {
                            chkEsourceField.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "")
                        {
                            drp_PGL_Type.SelectedValue = ds.Tables[0].Rows[0]["PGL_TYPE"].ToString();
                        }
                        else
                        {
                            drp_PGL_Type.SelectedIndex = 0;
                        }

                       



                        btnUpdateField.Visible = true;
                        btnCancelField.Visible = true;
                    }

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "DisableDiv();", true);
                }
                else if (e.CommandName == "DeleteField")
                {
                    dal_DB.DB_DM_SP(ACTION: "DELETE_DM_PROJECT_MASTER", ID: Session["ID"].ToString());

                    GetField();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field deleted successfully');", true);

                    //DataSet ds = dal_DB.DB_DM_SP(ACTION: "CHECK_VARIABLE_DATA_EXISTS_OR_NOT",
                    //    VISITNUM: drpModuleVisit.SelectedValue,
                    //    VARIABLENAME: VARIABLENAME
                    //    );

                    //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["COUNT"].ToString() != "0")
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot delete this field because the data has already been entered');", true);
                    //}
                    //else
                    //{
                    //    dal_DB.DB_DM_SP(ACTION: "DELETE_DM_PROJECT_MASTER", ID: Session["ID"].ToString());

                    //    GetField();

                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field deleted successfully');", true);
                    //}
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

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_VISIT_MODULE_SPECS_EXPORT");

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Visit Module List_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

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

        protected void chkRead_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRead.Checked == true)
            {
                divSDV.Visible = false;
            }
            else
            {
                divSDV.Visible = true;
            }
        }
    }
}