using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Drawing;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_DB dal_DB = new DAL_DB();
        public string FieldColorValue = "#000000";
        public string AnsColorValue = "#000000";
        protected void Page_Load(object sender, EventArgs e)
        {

            txtDescrip.Attributes.Add("MaxLength", "500");

            txtReason.Attributes.Add("MaxLength", "500");
            txtOpenForEditReason.Attributes.Add("MaxLength", "500");


            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                {
                    GetSystems();
                
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void lbtnExportSpecs_Click(object sender, EventArgs e)
        {
            
        }
        protected void GetSystems()
        {
            try
            {
                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_USER_SYSTEMS");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpSystem.DataSource = ds.Tables[0];
                    drpSystem.DataValueField = "SystemName";
                    drpSystem.DataTextField = "SystemName";
                    drpSystem.DataBind();
                    drpSystem.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSystem.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetModule()
        {
            try
            {
                DataSet ds = dal_DB.DB_MODULE_SP(ACTION: "GET_MODULENAME_BY_SYSTEM", SYSTEM: drpSystem.SelectedValue);

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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void showFeatures()
        {
            try
            {
                switch (drpSCControl.SelectedItem.Text)
                {
                    case "CHECKBOX":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //--DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //---DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = true;
                        lblDataEntry.Visible = false;
                        break;

                    case "TEXTBOX":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        DIVmaxLenght.Visible = true;
                        divFormat.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = true;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = true;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = true;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "TEXTBOX with Options":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "HTML TEXTBOX":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        DIVmaxLenght.Visible = true;
                        divFormat.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = true;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "RADIOBUTTON":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = true;
                        lblDataEntry.Visible = false;
                        break;
                    case "DROPDOWN":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = true;
                        divLickedChild.Visible = true;
                        divReferances.Visible = true;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = true;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "TIME":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        DIVmaxLenght.Visible = false;
                        divFormat.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "NUMERIC":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = true;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "DATE without Futuristic Date":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "DECIMAL":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = true;
                        DIVmaxLenght.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "HEADER":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
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
                        divCriticalDP.Visible = false;
                        divMedAuthRes.Visible = false;
                        divDuplicate.Visible = false;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = false;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = false;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = true;
                        lblDataLinkages.Visible = true;
                        lblDataEntry.Visible = true;
                        break;
                    case "DATE":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;
                    case "DATE with Input Mask":
                        DivDisplay.Visible = true;
                        DivSignificant.Visible = true;
                        DivLinkaged.Visible = true;
                        DivEntry.Visible = true;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
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
                        divCriticalDP.Visible = true;
                        divMedAuthRes.Visible = true;
                        divDuplicate.Visible = true;
                        //DataLinkage
                        DivLinkedParent.Visible = false;
                        divLickedChild.Visible = false;
                        divReferances.Visible = false;
                        divLinkedDataFlowfield.Visible = false;
                        DivProtocalPredefineData.Visible = true;
                        //DataEntry
                        divSequentialYN.Visible = false;
                        divNonRepetative.Visible = false;
                        divInList.Visible = true;
                        divPrefix.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;
                        break;

                    default:
                        DivDisplay.Visible = false;
                        DivSignificant.Visible = false;
                        DivLinkaged.Visible = false;
                        DivEntry.Visible = false;
                        divFormat.Visible = false;
                        DIVmaxLenght.Visible = false;
                        lblSignificant.Visible = false;
                        lblDataLinkages.Visible = false;
                        lblDataEntry.Visible = false;

                        break;
                }
               SYSTEMANCONTROLWISE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSendToReview_Click(object sender, EventArgs e)
        {

        }

        protected void btnOpenForEdit_Click(object sender, EventArgs e)
        {

        }

        protected void drpSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {              
                divshowsystem.Visible = true;              
                GetModule();
                drpModule_SelectedIndexChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpModule.SelectedValue != "0")
                {
                    //GetField();
                    //GetMasterModules();
                    //MODULE_STATUS();
                    //GetTreeview();
                    //ClearFieldSection();
                    //ANS_CHILD();

                    DivRecords.Visible = true;
                    DivMasterData.Visible = true;
                    DivManageMap.Visible = true;

                    if (drpSystem.SelectedValue == "External/Independent")
                    {
                        DivMasterData.Visible = false;
                        DivManageMap.Visible = false;
                        lnkCodeMapping.Visible = false;
                        lbtn_set_labDefault.Visible = false;
                        lbtnsetOnsubmitCrits.Visible = false;
                        btnSendToReview.Visible = false;
                        btnReviewLogs.Visible = false;
                        btnOpenForEdit.Visible = false;
                    }
                    else if (drpSystem.SelectedValue == "IWRS" || drpSystem.SelectedValue == "External/Independent" || drpSystem.SelectedValue == "Pharmacovigilance")
                    {
                        lnkCodeMapping.Visible = false;
                        lbtn_set_labDefault.Visible = false;
                        lbtnsetOnsubmitCrits.Visible = false;

                    }

                    btnSubmitField.Visible = true;
                    btnUpdateField.Visible = false;
                    showFeatures();
                }
                else
                {
                    DivRecords.Visible = false;
                    DivMasterData.Visible = false;
                    DivManageMap.Visible = false;
                    btnSendToReview.Visible = false;
                    btnReviewLogs.Visible = false;
                    btnOpenForEdit.Visible = false;
                  showFeatures();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();

            }
        }

        private void SYSTEMANCONTROLWISE()
        {
            try
            {
                if (drpSystem.SelectedValue == "0")
                {
                    Response.Redirect("DM_CreateModuleNew.aspx");

                }
                else if (drpSystem.SelectedValue == "External/Independent")
                {
                    divControl.Visible = false;
                    drpSCControl.Visible = false;
                    DivDesc.Visible = false;
                    DivDisplay.Visible = false;
                    DivDisplay.Visible = false;
                    DivSignificant.Visible = false;
                    DivLinkaged.Visible = false;
                    DivEntry.Visible = false;

                    divshowsystem.Visible = true;

                }
                else if (drpSystem.SelectedValue == "eSource")
                {
                    divDuplicate.Visible = false;
                }
                else if (drpSystem.SelectedValue == "IWRS")
                {
                    divDuplicate.Visible = false;
                    divReferances.Visible = false;
                    DivRequiredInfo.Visible = false;
                    divCriticalDP.Visible = false;
                    divMedAuthRes.Visible = false;
                    DivLinkedParent.Visible = false;
                    divLickedChild.Visible = false;
                    divLinkedDataFlowfield.Visible = false;
                    divSequentialYN.Visible = false;
                    divNonRepetative.Visible = false;
                    divInList.Visible = false;
                    divPrefix.Visible = false;
                }
                else if (drpSystem.SelectedValue == "Pharmacovigilance")
                {
                    divReferances.Visible = false;
                }
                else if (drpSystem.SelectedValue == "Data Management")
                {
                    divDuplicate.Visible = false;
                }
                else if (drpSystem.SelectedValue == "Protocol Deviation")
                {
                    divDuplicate.Visible = false;
                    divReferances.Visible = false;
                    divCriticalDP.Visible = false;
                    divMedAuthRes.Visible = false;
                    divLinkedDataFlowfield.Visible = false;
                    divNonRepetative.Visible = false;
                }
                else if (drpSystem.SelectedValue == "ePRO")
                {
                    divDuplicate.Visible = false;
                    divReferances.Visible = false;
                    divMedAuthRes.Visible = false;
                    divLinkedDataFlowfield.Visible = false;
                    divSequentialYN.Visible = false;
                    divNonRepetative.Visible = false;
                    divInList.Visible = false;
                    divPrefix.Visible = false;
                    lblDataEntry.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void txtVariableName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void drpSCControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chkAutoCode_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkChildLinked_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkParentLinked_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkDefault_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkInList_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkPrefix_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmitField_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdateField_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelField_Click(object sender, EventArgs e)
        {

        }

        protected void grdField_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdField_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdField_PreRender(object sender, EventArgs e)
        {

        }

        protected void drpModuleMaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvChecklists_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {

        }

        protected void ddlFIELD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlAnsTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnAddMoreChild_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_CHILD_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_CHILD_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmitModule_CHILD_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelModule_CHILD_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdateVisitField_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelVisitField_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmitReview_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmitReview_Click1(object sender, EventArgs e)
        {

        }

        protected void btnSubmitEditForEdit_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelEditForEdit_Click(object sender, EventArgs e)
        {

        }

        protected void Btn_Add_Fun_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmitModuleField_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelModuleField_Click(object sender, EventArgs e)
        {

        }
    }
}