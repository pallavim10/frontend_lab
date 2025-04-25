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
    public partial class RESOURCES_CDASH_LIBRARY : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    hdnPAGINATION.Value = "1";
                    GETMODULENAME();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETMODULENAME()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_CDESH_LIB_MODULE");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.DataSource = ds;
                    ddlModule.DataTextField = "Module";
                    ddlModule.DataValueField = "Module";
                    ddlModule.DataBind();
                }

                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_CDESH_LIB_DATA", CDESH_MODULE: ddlModule.SelectedValue, FIELDNAME: txtFieldName.Text, VARIABLENAME: txtVariablename.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblOBSERVATION.Text = ds.Tables[0].Rows[0]["Observation_Class"].ToString();
                    lblDOMAIN.Text = ds.Tables[0].Rows[0]["Domain"].ToString();
                    lblDATASCENARIO.Text = ds.Tables[0].Rows[0]["Data_Collection_Scenario"].ToString();
                    lblIMP_OPTION.Text = ds.Tables[0].Rows[0]["Implementation_Options"].ToString();
                    lblVARIABLE.Text = ds.Tables[0].Rows[0]["CDASHIG_Variable"].ToString();
                    lblVARLVL.Text = ds.Tables[0].Rows[0]["CDASHIG_Variable_Label"].ToString();
                    lblControltype.Text = ds.Tables[0].Rows[0]["Control_Type"].ToString();
                    lbllenght.Text = ds.Tables[0].Rows[0]["Field_Length"].ToString();
                    lblModuleYN.Text = ds.Tables[0].Rows[0]["Standard_Module_Yes_No"].ToString();
                    lblDEFINATION.Text = ds.Tables[0].Rows[0]["DRAFT_CDASHIG_Definition"].ToString();
                    lblQUERYTEXT.Text = ds.Tables[0].Rows[0]["Question_Text"].ToString();
                    lblPROMPT.Text = ds.Tables[0].Rows[0]["Prompt"].ToString();
                    lblDATATYPE.Text = ds.Tables[0].Rows[0]["Data_Type"].ToString();
                    lblCORE.Text = ds.Tables[0].Rows[0]["CDASHIG_Core"].ToString();
                    lblCOMP_INST.Text = ds.Tables[0].Rows[0]["Case_Report_Form_Completion_Instructions"].ToString();
                    lblTARGET.Text = ds.Tables[0].Rows[0]["SDTMIG_Target"].ToString();
                    lblMAP_INST.Text = ds.Tables[0].Rows[0]["Mapping_Instructions"].ToString();
                    lblCONTROLLED.Text = ds.Tables[0].Rows[0]["Controlled_Terminology_Codelist_Name"].ToString();
                    lblSUBSETCONTROLLED.Text = ds.Tables[0].Rows[0]["CDASH_Codelist_Name"].ToString();
                    lblIMPLEMENTATION.Text = ds.Tables[0].Rows[0]["Implementation_Notes"].ToString();
                }
                else
                {
                    hdnCurrentID.Value = "0";
                    hdnPAGINATION.Value = "0";
                    CLEAR();
                }

                hdnTotal.Value = ds.Tables[0].Rows.Count.ToString();
                GET_NEXT_PREV_ID();
                PAGINATION(hdnTotal.Value);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnPAGINATION.Value = "1";
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void PAGINATION(string TOTAL)
        {
            try
            {
                lblpagination.Text = "Page: " + hdnPAGINATION.Value + " out of " + TOTAL;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_NEXT_PREV_ID()
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_NEXT_PREV_ID_CDESH_LIB", CDESH_MODULE: ddlModule.SelectedValue, ID: hdnCurrentID.Value, FIELDNAME: txtFieldName.Text, VARIABLENAME: txtVariablename.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["PreviousValue"].ToString() != "")
                    {
                        btnPrevious.Visible = true;
                        hdnPREVID.Value = ds.Tables[0].Rows[0]["PreviousValue"].ToString();
                    }
                    else
                    {
                        btnPrevious.Visible = false;
                    }
                    if (ds.Tables[0].Rows[0]["NextValue"].ToString() != "")
                    {
                        btnNext.Visible = true;
                        hdnNEXTID.Value = ds.Tables[0].Rows[0]["NextValue"].ToString();
                    }
                    else
                    {
                        btnNext.Visible = false;
                    }
                }
                else
                {
                    btnPrevious.Visible = false;
                    btnNext.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_CDESH_LIB_DATA", CDESH_MODULE: ddlModule.SelectedValue, ID: hdnPREVID.Value);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblOBSERVATION.Text = ds.Tables[0].Rows[0]["Observation_Class"].ToString();
                    lblDOMAIN.Text = ds.Tables[0].Rows[0]["Domain"].ToString();
                    lblDATASCENARIO.Text = ds.Tables[0].Rows[0]["Data_Collection_Scenario"].ToString();
                    lblIMP_OPTION.Text = ds.Tables[0].Rows[0]["Implementation_Options"].ToString();
                    lblVARIABLE.Text = ds.Tables[0].Rows[0]["CDASHIG_Variable"].ToString();
                    lblVARLVL.Text = ds.Tables[0].Rows[0]["CDASHIG_Variable_Label"].ToString();
                    lblControltype.Text = ds.Tables[0].Rows[0]["Control_Type"].ToString();
                    lbllenght.Text = ds.Tables[0].Rows[0]["Field_Length"].ToString();
                    lblModuleYN.Text = ds.Tables[0].Rows[0]["Standard_Module_Yes_No"].ToString();
                    lblDEFINATION.Text = ds.Tables[0].Rows[0]["DRAFT_CDASHIG_Definition"].ToString();
                    lblQUERYTEXT.Text = ds.Tables[0].Rows[0]["Question_Text"].ToString();
                    lblPROMPT.Text = ds.Tables[0].Rows[0]["Prompt"].ToString();
                    lblDATATYPE.Text = ds.Tables[0].Rows[0]["Data_Type"].ToString();
                    lblCORE.Text = ds.Tables[0].Rows[0]["CDASHIG_Core"].ToString();
                    lblCOMP_INST.Text = ds.Tables[0].Rows[0]["Case_Report_Form_Completion_Instructions"].ToString();
                    lblTARGET.Text = ds.Tables[0].Rows[0]["SDTMIG_Target"].ToString();
                    lblMAP_INST.Text = ds.Tables[0].Rows[0]["Mapping_Instructions"].ToString();
                    lblCONTROLLED.Text = ds.Tables[0].Rows[0]["Controlled_Terminology_Codelist_Name"].ToString();
                    lblSUBSETCONTROLLED.Text = ds.Tables[0].Rows[0]["CDASH_Codelist_Name"].ToString();
                    lblIMPLEMENTATION.Text = ds.Tables[0].Rows[0]["Implementation_Notes"].ToString();
                }
                else
                {
                    hdnCurrentID.Value = "0";
                    hdnPAGINATION.Value = "0";
                    CLEAR();
                }

                hdnPAGINATION.Value = (Convert.ToInt32(hdnPAGINATION.Value) - 1).ToString();
                PAGINATION(hdnTotal.Value);
                GET_NEXT_PREV_ID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.RESOURCES_DATA(Action: "GET_CDESH_LIB_DATA", CDESH_MODULE: ddlModule.SelectedValue, ID: hdnNEXTID.Value);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnCurrentID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblOBSERVATION.Text = ds.Tables[0].Rows[0]["Observation_Class"].ToString();
                    lblDOMAIN.Text = ds.Tables[0].Rows[0]["Domain"].ToString();
                    lblDATASCENARIO.Text = ds.Tables[0].Rows[0]["Data_Collection_Scenario"].ToString();
                    lblIMP_OPTION.Text = ds.Tables[0].Rows[0]["Implementation_Options"].ToString();
                    lblVARIABLE.Text = ds.Tables[0].Rows[0]["CDASHIG_Variable"].ToString();
                    lblVARLVL.Text = ds.Tables[0].Rows[0]["CDASHIG_Variable_Label"].ToString();
                    lblControltype.Text = ds.Tables[0].Rows[0]["Control_Type"].ToString();
                    lbllenght.Text = ds.Tables[0].Rows[0]["Field_Length"].ToString();
                    lblModuleYN.Text = ds.Tables[0].Rows[0]["Standard_Module_Yes_No"].ToString();
                    lblDEFINATION.Text = ds.Tables[0].Rows[0]["DRAFT_CDASHIG_Definition"].ToString();
                    lblQUERYTEXT.Text = ds.Tables[0].Rows[0]["Question_Text"].ToString();
                    lblPROMPT.Text = ds.Tables[0].Rows[0]["Prompt"].ToString();
                    lblDATATYPE.Text = ds.Tables[0].Rows[0]["Data_Type"].ToString();
                    lblCORE.Text = ds.Tables[0].Rows[0]["CDASHIG_Core"].ToString();
                    lblCOMP_INST.Text = ds.Tables[0].Rows[0]["Case_Report_Form_Completion_Instructions"].ToString();
                    lblTARGET.Text = ds.Tables[0].Rows[0]["SDTMIG_Target"].ToString();
                    lblMAP_INST.Text = ds.Tables[0].Rows[0]["Mapping_Instructions"].ToString();
                    lblCONTROLLED.Text = ds.Tables[0].Rows[0]["Controlled_Terminology_Codelist_Name"].ToString();
                    lblSUBSETCONTROLLED.Text = ds.Tables[0].Rows[0]["CDASH_Codelist_Name"].ToString();
                    lblIMPLEMENTATION.Text = ds.Tables[0].Rows[0]["Implementation_Notes"].ToString();
                }
                else
                {
                    hdnCurrentID.Value = "0";
                    hdnPAGINATION.Value = "0";
                    CLEAR();
                }

                hdnPAGINATION.Value = (Convert.ToInt32(hdnPAGINATION.Value) + 1).ToString();
                PAGINATION(hdnTotal.Value);
                GET_NEXT_PREV_ID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void txtFieldName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                hdnPAGINATION.Value = "1";
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void txtVariablename_TextChanged(object sender, EventArgs e)
        {
            try
            {
                hdnPAGINATION.Value = "1";
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CLEAR()
        {
            lblOBSERVATION.Text = "";
            lblDOMAIN.Text = "";
            lblDATASCENARIO.Text = "";
            lblIMP_OPTION.Text = "";
            lblVARIABLE.Text = "";
            lblVARLVL.Text = "";
            lblControltype.Text = "";
            lbllenght.Text = "";
            lblModuleYN.Text = "";
            lblDEFINATION.Text = "";
            lblQUERYTEXT.Text = "";
            lblPROMPT.Text = "";
            lblDATATYPE.Text = "";
            lblCORE.Text = "";
            lblCOMP_INST.Text = "";
            lblTARGET.Text = "";
            lblMAP_INST.Text = "";
            lblCONTROLLED.Text = "";
            lblSUBSETCONTROLLED.Text = "";
            lblIMPLEMENTATION.Text = "";
        }
    }
}