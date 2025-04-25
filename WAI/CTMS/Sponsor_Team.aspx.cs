using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace CTMS
{
    public partial class Sponsor_Team : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction Comfun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Sponsor();
                    fill_Country();
                    Get_Bind_Members();
                    lbtnUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_Country()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "GET_COUNTRY"
                );
                ddlCountry.DataSource = ds.Tables[0];
                ddlCountry.DataValueField = "CNTRYID";
                ddlCountry.DataTextField = "COUNTRY";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void fill_State()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "GET_STATE",
                CNTRYID: ddlCountry.SelectedValue
                );
                ddlstate.DataSource = ds.Tables[0];
                ddlstate.DataValueField = "ID";
                ddlstate.DataTextField = "StateName";
                ddlstate.DataBind();
                ddlstate.Items.Insert(0, new ListItem("--Select State--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void fill_City()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetCOUNTRYDETAILS(
                Action: "GET_CITY",
                STATEID: ddlstate.SelectedValue
                );
                ddlcity.DataSource = ds.Tables[0];
                ddlcity.DataValueField = "city_id";
                ddlcity.DataTextField = "city_name";
                ddlcity.DataBind();
                ddlcity.Items.Insert(0, new ListItem("--Select City--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void bind_Sponsor()
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(Action: "GET_Sponsors");
                ddlSponsor.DataSource = ds.Tables[0];
                ddlSponsor.DataValueField = "ID";
                ddlSponsor.DataTextField = "Name";
                ddlSponsor.DataBind();
                ddlSponsor.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Members()
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(Action: "GET_Team", Sponsor_ID: ddlSponsor.SelectedValue);
                gvEmp.DataSource = ds.Tables[0];
                gvEmp.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Get_Bind_Members()
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(Action: "GET_SpTeamMember");
                gvEmp.DataSource = ds.Tables[0];
                gvEmp.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT()
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(
                Action: "INSERT_Team",
                Sponsor_ID: ddlSponsor.SelectedValue,
                FirstName: txtFirstName.Text,
                LastName: txtLastName.Text,
                EmailID: txtEmailID.Text,
                Mobile: txtMobilePhone.Text,
                Country: ddlCountry.SelectedValue,
                State: ddlstate.SelectedValue,
                City: ddlcity.SelectedValue,
                Address: txtAddress.Text,
                ZIP: txtPostal.Text,
                Department: txtDepartment.Text,
                Designation: txtDesignation.Text,
                Notes: txtNotes.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmailID.Text = "";
                txtMobilePhone.Text = "";
                ddlCountry.SelectedIndex = 0;
                ddlstate.Items.Clear();
                ddlcity.Items.Clear();
                txtAddress.Text = "";
                txtPostal.Text = "";
                txtDepartment.Text = "";
                txtDesignation.Text = "";
                txtNotes.Text = "";

                bind_Members();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE()
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(
                Action: "UPDATE_Team",
                ID: Session["MEMBERID"].ToString(),
                FirstName: txtFirstName.Text,
                LastName: txtLastName.Text,
                EmailID: txtEmailID.Text,
                Mobile: txtMobilePhone.Text,
                Country: ddlCountry.SelectedValue,
                State: ddlstate.SelectedValue,
                City: ddlcity.SelectedValue,
                Address: txtAddress.Text,
                ZIP: txtPostal.Text,
                Department: txtDepartment.Text,
                Designation: txtDesignation.Text,
                Notes: txtNotes.Text,
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()
                    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0]["Alert"] + "')", true);
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmailID.Text = "";
                txtMobilePhone.Text = "";
                ddlCountry.SelectedIndex = 0;
                ddlstate.Items.Clear();
                ddlcity.Items.Clear();
                txtAddress.Text = "";
                txtPostal.Text = "";
                txtDepartment.Text = "";
                txtDesignation.Text = "";
                txtNotes.Text = "";

                lbtnSubmit.Visible = true;
                lbtnUpdate.Visible = false;

                bind_Members();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        private void DELETE(string id)
        {
            try
            {
                dal.Sponsor_SP(Action: "DELETE_Team", ID: id, ENTEREDBY: Session["User_ID"].ToString(),
                     IPADDRESS: Comfun.GetIpAddress()
                    );

                bind_Members();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT(string id)
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(Action: "SELECT_Team", ID: id);
                ddlSponsor.SelectedValue = ds.Tables[0].Rows[0]["Sponsor_ID"].ToString();
                txtFirstName.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                txtLastName.Text = ds.Tables[0].Rows[0]["LastName"].ToString();
                txtEmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                txtMobilePhone.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                fill_Country();
                ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["Country"].ToString();
                fill_State();
                ddlstate.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString();
                fill_City();
                ddlcity.SelectedValue = ds.Tables[0].Rows[0]["City"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtPostal.Text = ds.Tables[0].Rows[0]["ZIP"].ToString();
                txtDepartment.Text = ds.Tables[0].Rows[0]["Department"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                txtNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();

                lbtnSubmit.Visible = false;
                lbtnUpdate.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSponsor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Members();
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
                UPDATE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmailID.Text = "";
            txtMobilePhone.Text = "";
            ddlCountry.SelectedIndex = 0;
            ddlstate.Items.Clear();
            ddlcity.Items.Clear();
            txtAddress.Text = "";
            txtPostal.Text = "";
            txtDepartment.Text = "";
            txtDesignation.Text = "";
            txtNotes.Text = "";

            lbtnSubmit.Visible = true;
            lbtnUpdate.Visible = false;
        }

        protected void gvEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["MEMBERID"] = id;
                if (e.CommandName == "Edit1")
                {
                    EDIT(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    DELETE(id);
                    bind_Members();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string count = drv["Count"].ToString();
                    CheckBox Chk_Sel_Fun = (e.Row.FindControl("Chk_Sel_Fun") as CheckBox);
                    CheckBox Chk_Sel_Remove_Fun = (e.Row.FindControl("Chk_Sel_Remove_Fun") as CheckBox);
                    LinkButton lbtndeleteEmp = (e.Row.FindControl("lbtndeleteEmp") as LinkButton);

                    if (Convert.ToInt32(count) > 0)
                    {
                        lbtndeleteEmp.Visible = false;
                        Chk_Sel_Fun.Visible = false;
                        Chk_Sel_Remove_Fun.Visible = true;
                    }
                    else
                    {
                        lbtndeleteEmp.Visible = true;
                        Chk_Sel_Fun.Visible = true;
                        Chk_Sel_Remove_Fun.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Btn_Add_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvEmp.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvEmp.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        string ID = ((Label)gvEmp.Rows[i].FindControl("lbl_EMPID")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Sponsor_SP(Action: "Add_To_ProjectTeam", Project_ID: Session["PROJECTID"].ToString(), ID: ID);
                    }
                }
                bind_Members();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Btn_Remove_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvEmp.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvEmp.Rows[i].FindControl("Chk_Sel_Remove_Fun");

                    if (ChAction.Checked)
                    {
                        string ID = ((Label)gvEmp.Rows[i].FindControl("lbl_EMPID")).Text;
                        string ProjectID = Session["PROJECTID"].ToString();
                        dal.Sponsor_SP(Action: "Remove_From_ProjectTeam", Project_ID: Session["PROJECTID"].ToString(), ID: ID);
                    }
                }
                bind_Members();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_City();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_State();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvEmp_PreRender(object sender, EventArgs e)
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



        protected void btnSponsorTeamMemberExport_Click(object sender, EventArgs e)
        {
            try
            {

                Sponsor_Master(header: "Sponsor Team Member", Action: "GET_SponsorTeamMember");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Sponsor_Master(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal.Export_Data_SP(Action: Action, PRODUCTID: PRODUCT_ID, SUBCLASSID: SUBCLASS_ID);

                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}