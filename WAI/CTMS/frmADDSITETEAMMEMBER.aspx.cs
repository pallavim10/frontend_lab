using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Net;
using System.Net.Sockets;

namespace CTMS
{
    public partial class frmADDSITETEAMMEMBER : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction Comfun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //bind_Investigator();
                    bind_Members();
                    fill_Country();
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

        private void bind_Members()
        {
            try
            {
                DataSet ds = dal.GetSetINVDETAILS(Action: "GET_Team");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvEmp.DataSource = ds.Tables[0];
                    gvEmp.DataBind();
                }
                else
                {
                    gvEmp.DataSource = null;
                    gvEmp.DataBind();
                }
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
                DataSet ds = dal.GetSetINVDETAILS(
                Action: "INSERT_Team",
                FirstName: txtFirstName.Text,
                LastName: txtLastName.Text,
                EMAILID: txtEmailID.Text,
                MOBNO: txtMobilePhone.Text,
                CNTRYID: ddlCountry.SelectedValue,
                State: ddlstate.SelectedValue,
                City: ddlcity.SelectedValue,
                ADDRESS: txtAddress.Text,
                ZIP: txtPostal.Text,
                Department: txtDepartment.Text,
                Designation: txtDesignation.Text,
                Notes: txtNotes.Text,
                STARTDATE: txtstartdate.Text,
                ENDDATE: txtenddate.Text,
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
                txtstartdate.Text = "";
                txtenddate.Text = "";

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
                DataSet ds = dal.GetSetINVDETAILS(
                Action: "UPDATE_Team",
                ID: Session["MEMBERID"].ToString(),
                FirstName: txtFirstName.Text,
                LastName: txtLastName.Text,
                EMAILID: txtEmailID.Text,
                MOBNO: txtMobilePhone.Text,
                CNTRYID: ddlCountry.SelectedValue,
                State: ddlstate.SelectedValue,
                City: ddlcity.SelectedValue,
                ADDRESS: txtAddress.Text,
                ZIP: txtPostal.Text,
                Department: txtDepartment.Text,
                Designation: txtDesignation.Text,
                Notes: txtNotes.Text,
                STARTDATE: txtstartdate.Text,
                ENDDATE: txtenddate.Text,
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
                txtstartdate.Text = "";
                txtenddate.Text = "";

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
            
            dal = new DAL();
            try
            {
                dal.GetSetINVDETAILS(Action: "DELETE_Team", ID: id, ENTEREDBY: Session["User_ID"].ToString(),
                      IPADDRESS: Comfun.GetIpAddress());

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
                DataSet ds = dal.GetSetINVDETAILS(Action: "SELECT_Team", ID: id);

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
                txtstartdate.Text = ds.Tables[0].Rows[0]["STARTDAT1"].ToString();
                txtenddate.Text = ds.Tables[0].Rows[0]["ENDDATE1"].ToString();

                lbtnSubmit.Visible = false;
                lbtnUpdate.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlInvestigator_SelectedIndexChanged(object sender, EventArgs e)
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
                    LinkButton lbtndeleteEmp = (e.Row.FindControl("lbtndeleteEmp") as LinkButton);

                    if (count == "0")
                    {
                        lbtndeleteEmp.Visible = true;
                    }
                    else
                    {
                        lbtndeleteEmp.Visible = false;
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
        //lbInvMemberEport_Click
        protected void lbInvMemberEport_Click(object sender, EventArgs e)
        {
            try
            {
                Add_INV_TeamMember(header: "Investigator Team Member", Action: "Add_INV_TeamMember");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Add_INV_TeamMember(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null)
        {
            try
            {
                DataSet ds = new DataSet();
                DAL dal;
                dal = new DAL();
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