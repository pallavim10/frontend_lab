using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CTMS;
using PPT;
using CTMS.CommonFunction;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class UMT_Role : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    BIND_SYSTEMS();
                    GET_ROLES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSUBMITRoles_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHECK_ROLE_EXISTS("INSERT"))
                {
                    Response.Write("<script language=javascript>alert('Same Role Name already exists for this system.');</script>");
                }
                else
                {
                    dal_UMT.UMT_ROLE_SP(
                       ACTION: "INSERT_UMT_ROLES",
                       SystemID: drpSystem.SelectedValue,
                       RoleName: txtRoleName.Text,
                       Blind: ddlUnblind.SelectedValue,
                       Med_FORM: chkForm.Checked,
                       Med_FIELD: chkfield.Checked,
                       Sign_eSource: Check_eSource.Checked,
                       Sign_PV: Check_Safety.Checked,
                       Sign_DM: Check_eCRF.Checked,
                       ReadOnly_eSource: Check_eSourceReadOnly.Checked,
                       Admin_eSource: Check_eSourceAdmin.Checked,
                       CRA_eSource: Check_CRA.Checked
                        );

                    Response.Write("<script> alert('User Role Added Successfully'); window.location.href = 'UMT_Role.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_SYSTEMS()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_ROLE_SP(
                          ACTION: "GET_SYSTEMS"
                          );

                drpSystem.DataSource = ds;
                drpSystem.DataTextField = "SystemName";
                drpSystem.DataValueField = "SystemID";
                drpSystem.DataBind();
                drpSystem.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_ROLES()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_ROLE_SP(
                    ACTION: "GET_ROLES",
                    SystemID: drpSystem.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdRoles.DataSource = ds.Tables[0];
                    grdRoles.DataBind();
                }
                else
                {
                    grdRoles.DataSource = null;
                    grdRoles.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Clear()
        {
            drpSystem.SelectedIndex = 0;
            txtRoleName.Text = "";
            ddlUnblind.SelectedIndex = 0;
            chkForm.Checked = false;
            chkfield.Checked = false;
            Check_eSource.Checked = false;
            Check_Safety.Checked = false;
            Check_eCRF.Checked = false;
            Check_eSourceReadOnly.Checked = false;
            Check_eSourceAdmin.Checked = false;
            Check_CRA.Checked = false;
        }

        protected void btnCancelROles_Click(object sender, EventArgs e)
        {
            try
            {
                btnSUBMITRoles.Visible = true;
                btnUpdateRole.Visible = false;
                Clear();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void grdUserDetails_PreRender(object sender, EventArgs e)
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

        protected void grdRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;
                if (e.CommandName == "EIDIT")
                {
                    EIDIT_ROLES(ID);
                    drpSystem_SelectedIndexChanged(this, e);
                    btnSUBMITRoles.Visible = false;
                    btnUpdateRole.Visible = true;
                }
                else if (e.CommandName == "DELETED")
                {
                    DELETE_ROLES(ID);
                    GET_ROLES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_ROLES(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_ROLE_SP(
                  ACTION: "DELETE_ROLES",
                  ID: ID
                  );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Role Deleted Succesfully')", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EIDIT_ROLES(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_ROLE_SP(
                               ACTION: "EDIT_ROLES",
                               ID: ID
                           );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpSystem.SelectedValue = ds.Tables[0].Rows[0]["SystemID"].ToString();
                    txtRoleName.Text = ds.Tables[0].Rows[0]["RoleName"].ToString();
                    ddlUnblind.SelectedValue = ds.Tables[0].Rows[0]["Blind"].ToString();


                    if (ds.Tables[0].Rows[0]["Med_FORM"].ToString() == "True")
                    {
                        chkForm.Checked = true;
                    }
                    else
                    {
                        chkForm.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["Med_FIELD"].ToString() == "True")
                    {
                        chkfield.Checked = true;
                    }
                    else
                    {
                        chkfield.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["Sign_eSource"].ToString() == "True")
                    {
                        Check_eSource.Checked = true;
                    }
                    else
                    {
                        Check_eSource.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["Sign_PV"].ToString() == "True")
                    {
                        Check_Safety.Checked = true;
                    }
                    else
                    {
                        Check_Safety.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["Sign_DM"].ToString() == "True")
                    {
                        Check_eCRF.Checked = true;
                    }
                    else
                    {
                        Check_eCRF.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["ReadOnly_eSource"].ToString() == "True")
                    {
                        Check_eSourceReadOnly.Checked = true;
                    }
                    else
                    {
                        Check_eSourceReadOnly.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["Admin_eSource"].ToString() == "True")
                    {
                        Check_eSourceAdmin.Checked = true;
                    }
                    else
                    {
                        Check_eSourceAdmin.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["CRA_eSource"].ToString() == "True")
                    {
                        Check_CRA.Checked = true;
                    }
                    else
                    {
                        Check_CRA.Checked = false;
                    }
                }
                else
                {
                    grdRoles.DataSource = null;
                    grdRoles.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHECK_ROLE_EXISTS("UPDATE"))
                {
                    Response.Write("<script language=javascript>alert('Same Role Name already exists for this system.');</script>");
                }
                else
                {
                    dal_UMT.UMT_ROLE_SP(
                        ACTION: "UPDATE_ROLES",
                         ID: ViewState["ID"].ToString(),
                         SystemID: drpSystem.SelectedValue,
                         RoleName: txtRoleName.Text,
                         Blind: ddlUnblind.SelectedValue,
                         Med_FORM: chkForm.Checked,
                         Med_FIELD: chkfield.Checked,
                         Sign_eSource: Check_eSource.Checked,
                         Sign_PV: Check_Safety.Checked,
                         Sign_DM: Check_eCRF.Checked,
                         ReadOnly_eSource: Check_eSourceReadOnly.Checked,
                         Admin_eSource: Check_eSourceAdmin.Checked,
                         CRA_eSource: Check_CRA.Checked
                        );

                    Response.Write("<script> alert('User Role Updated Successfully'); window.location.href = 'UMT_Role.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbUserDetailsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Role Details";

                DataSet ds = dal_UMT.UMT_LOG_SP(
                    ACTION: "GET_ROLES"
                    );
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

        protected void drpSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (drpSystem.SelectedItem.Text)
                {
                    case "IWRS":
                        DivROLE.Visible = true;
                        divBlinded.Visible = true;
                        DiveSourceReadonly.Visible = false;
                        DivMedicalAuth.Visible = false;
                        lblSignoff.Visible = false;
                        divesource.Visible = false;
                        divPharmacovigilance.Visible = false;
                        diveCRF.Visible = false;

                        break;

                    case "Pharmacovigilance":
                        DivROLE.Visible = true;
                        divBlinded.Visible = true;
                        DiveSourceReadonly.Visible = false;
                        DivMedicalAuth.Visible = true;
                        lblSignoff.Visible = true;
                        divesource.Visible = false;
                        divPharmacovigilance.Visible = true;
                        diveCRF.Visible = false;

                        break;
                    case "Medical Monitoring":
                        DivROLE.Visible = true;
                        divBlinded.Visible = true;
                        DiveSourceReadonly.Visible = false;
                        DivMedicalAuth.Visible = false;
                        lblSignoff.Visible = false;
                        divesource.Visible = false;
                        divPharmacovigilance.Visible = false;
                        diveCRF.Visible = false;

                        break;
                    case "Data Management":
                        DivROLE.Visible = true;
                        divBlinded.Visible = true;
                        DiveSourceReadonly.Visible = false;
                        DivMedicalAuth.Visible = true;
                        lblSignoff.Visible = true;
                        divesource.Visible = false;
                        divPharmacovigilance.Visible = false;
                        diveCRF.Visible = true;

                        break;
                    case "eSource":
                        DivROLE.Visible = true;
                        divBlinded.Visible = true;
                        DiveSourceReadonly.Visible = true;
                        DivMedicalAuth.Visible = true;
                        lblSignoff.Visible = true;
                        divesource.Visible = true;
                        divPharmacovigilance.Visible = false;
                        diveCRF.Visible = false;

                        break;
                    case "eTMF":
                        DivROLE.Visible = true;
                        divBlinded.Visible = true;
                        DiveSourceReadonly.Visible = false;
                        DivMedicalAuth.Visible = false;
                        lblSignoff.Visible = false;
                        divesource.Visible = false;
                        divPharmacovigilance.Visible = false;
                        diveCRF.Visible = false;

                        break;
                    case "Coding":
                        DivROLE.Visible = true;
                        divBlinded.Visible = false;
                        DiveSourceReadonly.Visible = false;
                        DivMedicalAuth.Visible = false;
                        lblSignoff.Visible = false;
                        divesource.Visible = false;
                        divPharmacovigilance.Visible = false;
                        diveCRF.Visible = false;

                        break;
                    case "Design Bench":
                        DivROLE.Visible = true;
                        divBlinded.Visible = false;
                        DiveSourceReadonly.Visible = false;
                        DivMedicalAuth.Visible = false;
                        lblSignoff.Visible = false;
                        divesource.Visible = false;
                        divPharmacovigilance.Visible = false;
                        diveCRF.Visible = false;

                        break;
                    case "User Management":

                        DivROLE.Visible = true;
                        divBlinded.Visible = false;
                        DiveSourceReadonly.Visible = false;
                        DivMedicalAuth.Visible = false;
                        lblSignoff.Visible = false;
                        divesource.Visible = false;
                        divPharmacovigilance.Visible = false;
                        diveCRF.Visible = false;
                        break;

                    default:
                        DivROLE.Visible = false;
                        divBlinded.Visible = false;
                        DiveSourceReadonly.Visible = false;
                        DivMedicalAuth.Visible = false;
                        lblSignoff.Visible = false;
                        divesource.Visible = false;
                        divPharmacovigilance.Visible = false;
                        diveCRF.Visible = false;

                        break;
                }

                GET_ROLES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    HtmlControl iconMed_FORM = (HtmlControl)e.Row.FindControl("iconMed_FORM");

                    string lblID = ((Label)e.Row.FindControl("lblID")).Text;
                    LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtdeleteRole");


                    DataSet ds = dal_UMT.UMT_STUDYROLE_SP(ACTION: "CHECK_ROLE", ID: lblID);
                    string COUNT = ds.Tables[0].Rows[0]["Count"].ToString();
                    if (Convert.ToInt32(COUNT) > 0)
                    {
                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }

                    if (drv["Med_FORM"].ToString() == "True")
                    {
                        iconMed_FORM.Attributes.Add("class", "fa fa-check");
                        iconMed_FORM.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconMed_FORM.Attributes.Add("class", "fa fa-times");
                        iconMed_FORM.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconMed_FIELD = (HtmlControl)e.Row.FindControl("iconMed_FIELD");
                    if (drv["Med_FIELD"].ToString() == "True")
                    {
                        iconMed_FIELD.Attributes.Add("class", "fa fa-check");
                        iconMed_FIELD.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconMed_FIELD.Attributes.Add("class", "fa fa-times");
                        iconMed_FIELD.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconSign_eSource = (HtmlControl)e.Row.FindControl("iconSign_eSource");
                    if (drv["Sign_eSource"].ToString() == "True")
                    {
                        iconSign_eSource.Attributes.Add("class", "fa fa-check");
                        iconSign_eSource.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSign_eSource.Attributes.Add("class", "fa fa-times");
                        iconSign_eSource.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconSign_PV = (HtmlControl)e.Row.FindControl("iconSign_PV");
                    if (drv["Sign_PV"].ToString() == "True")
                    {
                        iconSign_PV.Attributes.Add("class", "fa fa-check");
                        iconSign_PV.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSign_PV.Attributes.Add("class", "fa fa-times");
                        iconSign_PV.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconSign_DM = (HtmlControl)e.Row.FindControl("iconSign_DM");
                    if (drv["Sign_DM"].ToString() == "True")
                    {
                        iconSign_DM.Attributes.Add("class", "fa fa-check");
                        iconSign_DM.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSign_DM.Attributes.Add("class", "fa fa-times");
                        iconSign_DM.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconReadOnly_eSource = (HtmlControl)e.Row.FindControl("iconReadOnly_eSource");
                    if (drv["ReadOnly_eSource"].ToString() == "True")
                    {
                        iconReadOnly_eSource.Attributes.Add("class", "fa fa-check");
                        iconReadOnly_eSource.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconReadOnly_eSource.Attributes.Add("class", "fa fa-times");
                        iconReadOnly_eSource.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconSDR_eSource = (HtmlControl)e.Row.FindControl("iconSDR_eSource");
                    if (drv["CRA_eSource"].ToString() == "True")
                    {
                        iconSDR_eSource.Attributes.Add("class", "fa fa-check");
                        iconSDR_eSource.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSDR_eSource.Attributes.Add("class", "fa fa-times");
                        iconSDR_eSource.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconAdmin_eSource = (HtmlControl)e.Row.FindControl("iconAdmin_eSource");
                    if (drv["Admin_eSource"].ToString() == "True")
                    {
                        iconAdmin_eSource.Attributes.Add("class", "fa fa-check");
                        iconAdmin_eSource.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconAdmin_eSource.Attributes.Add("class", "fa fa-times");
                        iconAdmin_eSource.Attributes.Add("style", "color: red;");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Check_eSourceReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_eSourceReadOnly.Checked || Check_CRA.Checked)
            {
                divesource.Visible = false;
                lblSignoff.Visible = false;
            }
            else
            {
                divesource.Visible = true;
                lblSignoff.Visible = true;
            }
        }

        private bool CHECK_ROLE_EXISTS(string ACTION)
        {
            bool exists = false;
            try
            {
                DataSet ds = new DataSet();

                if (ACTION == "UPDATE")
                {
                    ds = dal_UMT.UMT_ROLE_SP(ACTION: "CHECK_ROLE_EXISTS", SystemID: drpSystem.SelectedValue, RoleName: txtRoleName.Text, ID: ViewState["ID"].ToString());
                }
                else
                {
                    ds = dal_UMT.UMT_ROLE_SP(ACTION: "CHECK_ROLE_EXISTS", SystemID: drpSystem.SelectedValue, RoleName: txtRoleName.Text);
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    exists = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return exists;
        }

    }
}