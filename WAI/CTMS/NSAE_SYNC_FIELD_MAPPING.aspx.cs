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
    public partial class NSAE_SYNC_FIELD_MAPPING : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        CommonFunction.CommonFunction CF = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_SAE_Module();
                    GET_DM_Module();
                    GET_TYPE_OF_SAE();
                    lbUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_TYPE_OF_SAE()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_SAE_TYPE");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DrptypeofSAE.DataSource = ds.Tables[0];
                    DrptypeofSAE.DataValueField = "MODULEID";
                    DrptypeofSAE.DataTextField = "MODULENAME";
                    DrptypeofSAE.DataBind();
                    DrptypeofSAE.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DrptypeofSAE.DataSource = null;
                    DrptypeofSAE.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SAE_Module()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_SAE_MODULE_NAME_MAPPING_FIELD");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DrpSaeModuleName.DataSource = ds.Tables[0];
                    DrpSaeModuleName.DataValueField = "MODULEID";
                    DrpSaeModuleName.DataTextField = "MODULENAME";
                    DrpSaeModuleName.DataBind();
                    DrpSaeModuleName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DrpSaeModuleName.DataSource = null;
                    DrpSaeModuleName.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DM_Module()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_DM_MODULE_NAME_MAPPING_FIELD");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DrpDMModuleName.DataSource = ds.Tables[0];
                    DrpDMModuleName.DataValueField = "MODULEID";
                    DrpDMModuleName.DataTextField = "MODULENAME";
                    DrpDMModuleName.DataBind();
                    DrpDMModuleName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DrpDMModuleName.DataSource = null;
                    DrpDMModuleName.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DrpSaeModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GRD_GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DrpDMModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GRD_GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_SAESYNC();
                GET_SAE_Module();
                GET_DM_Module();

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Fields Mapped Successfully.'); window.location.href = 'NSAE_SYNC_FIELD_MAPPING.aspx' ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_SAESYNC()
        {
            for (int i = 0; i < GrdGetData.Rows.Count; i++)
            {
                Label lblsaefieldname = (Label)GrdGetData.Rows[i].FindControl("ID");

                DropDownList DrpDmField = (DropDownList)GrdGetData.Rows[i].FindControl("DrpDmField");
                string DM_FIELDID = DrpDmField.SelectedValue;
                if (DM_FIELDID != "0")
                {
                    DataSet ds = dal_SAE.SAE_SETUP_SP(
                                            ACTION: "INSERT_UPDATE_MAPPING_FIELD",
                                            SAE_MODULEID: DrpSaeModuleName.SelectedValue,
                                            SAE_FIELDID: lblsaefieldname.Text,
                                            DM_MODULEID: DrpDMModuleName.SelectedValue,
                                            DM_FIELDID: DrpDmField.SelectedValue,
                                            SAE_TYPE_MODULEID: DrptypeofSAE.SelectedValue
                                            );

                    
                }
                else
                {
                    DataSet ds = dal_SAE.SAE_SETUP_SP(
                                           ACTION: "DELETING_MAPPING_FIELD",
                                           SAE_FIELDID: lblsaefieldname.Text,
                                           DM_MODULEID: DrpDMModuleName.SelectedValue
                                           );
                }
            }

            
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("NSAE_SYNC_FIELD_MAPPING.aspx");
        }

        protected void lbUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                GET_SAE_Module();
                GET_DM_Module();
                lbUpdate.Visible = false;
                lbAdd.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GRD_GET_DATA()
        {
            try
            {
                string SAE_MODULEID = DrpSaeModuleName.SelectedValue;
                string DM_MODULEID = DrpDMModuleName.SelectedValue;

                DataSet ds = dal_SAE.SAE_SETUP_SP(
                    ACTION: "GET_SAE_FIELD_NAME_MAPPING_FIELD", ID: SAE_MODULEID, SAE_TYPE_MODULEID: DrptypeofSAE.SelectedValue);

                DataSet ds1 = dal_SAE.SAE_SETUP_SP(
                     ACTION: "GET_DM_FIELD_NAME_MAPPING_FIELD", ID: DM_MODULEID, SAE_TYPE_MODULEID: DrptypeofSAE.SelectedValue);

                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    ViewState["Grd_Data"] = ds1.Tables[0];
                    GrdGetData.DataSource = ds.Tables[0];
                    GrdGetData.DataBind();

                    lbAdd.Visible = true;
                    lbtnCancel.Visible = true;
                }
                else
                {
                    GrdGetData.DataSource = null;
                    GrdGetData.DataBind();

                    lbAdd.Visible = false;
                    lbtnCancel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GrdGetData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                string ID = dr["ID"].ToString();
                DropDownList DrpDmField = (DropDownList)e.Row.FindControl("DrpDmField");
                Label lblsaefieldname = (Label)e.Row.FindControl("lblsaefieldname");

                DataTable dt = ViewState["Grd_Data"] as DataTable;

                DrpDmField.DataSource = dt;
                DrpDmField.DataValueField = "ID";
                DrpDmField.DataTextField = "FIELDNAME";
                DrpDmField.DataBind();
                DrpDmField.Items.Insert(0, new ListItem("--Select--", "0"));

                if (dr["DM_FIELDID"].ToString() != "")
                {
                    DrpDmField.SelectedValue = dr["DM_FIELDID"].ToString();
                }
            }
        }

        protected void DrptypeofSAE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SAE_Module();
                GET_DM_Module();
                GRD_GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}