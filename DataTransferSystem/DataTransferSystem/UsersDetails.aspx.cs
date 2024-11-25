using DataTransferSystem.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataTransferSystem
{
    public partial class UsersDetails : System.Web.UI.Page
    {
        DATA_DAL Data_Dal = new DATA_DAL();
        ActiveDirectory activeDirectory = new ActiveDirectory();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx");

                }

                GET_DETAILS();
            }
        }
        private void GET_DETAILS()
        {
            DataSet ds = Data_Dal.MANAGE_USER_SP(ACTION: "GET_USERS_DETAILS", ID: Request.QueryString["ID"].ToString());

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                txtUserName.Text = ds.Tables[0].Rows[0]["User_Name"].ToString();
                txtFullName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                txtEmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                txtDepartment.Text = ds.Tables[0].Rows[0]["Department"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();


            }
            DataSet ds1 = Data_Dal.MANAGE_USER_SP(ACTION: "GET_USERS_ACCESS_DETAILS", ID: Request.QueryString["ID"].ToString());
            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows[0]["masters"].ToString() == "True")
                {
                    chkMngMaster.Checked = true;
                }
                else
                {
                    chkMngMaster.Checked = false;
                }

                if (ds1.Tables[0].Rows[0]["Users"].ToString() == "True")
                {
                    chkMngUsers.Checked = true;
                }
                else
                {
                    chkMngUsers.Checked = false;
                }
            }
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = Data_Dal.MANAGE_USER_SP(ACTION: "INSERT_USERS_ACCESS",
                    Masters: chkMngMaster.Checked,
                    Users: chkMngUsers.Checked,
                    UserName: Session["UserName"].ToString(),
                    ID: Request.QueryString["ID"].ToString()
                    );
                UPDATE_USERS_DETAILS();


                CLEAR();

                string script = @"
                     swal({
                     title: 'Success!',
                     text: 'User details updated successfully.',
                     icon: 'success',
                     button: 'OK'
                     }).then((value) => {
                        window.location.href = 'ManageUsers.aspx'; 
                     });";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UPDATE_USERS_DETAILS()
        {
            string[] data = activeDirectory.ValidateUser(Session["Username"].ToString());

            if (txtFullName.Text == string.Empty)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Full Name', 'error');", true);

            }
            else if (txtEmailID.Text == string.Empty)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Email Id', 'error');", true);

            }
            else if (txtContactNo.Text == string.Empty)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Contact No', 'error');", true);

            }
            else if (txtDepartment.Text == string.Empty)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Department', 'error');", true);

            }
            else if (txtDesignation.Text == string.Empty)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Designation', 'error');", true);

            }
            DataSet ds = Data_Dal.MANAGE_USER_SP(
                      ACTION: "UPDATE_USER_DETAILS",
                      UserName: txtUserName.Text,
                      Name: txtFullName.Text,
                      EmailID: txtEmailID.Text,
                      SID: data[1].ToString(),
                      GUID: data[0].ToString(),
                      ContactNo: txtContactNo.Text,
                      Department: txtDepartment.Text,
                      Designation: txtDesignation.Text,
                      ID: Request.QueryString["ID"].ToString()
                      );

        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            CLEAR();
            Response.Redirect("~/ManageUsers.aspx");
        }

        private void CLEAR()
        {
            try
            {

                txtUserName.Text = "";
                txtFullName.Text = "";
                txtContactNo.Text = "";
                txtEmailID.Text = "";
                txtDepartment.Text = "";
                txtDesignation.Text = "";
                chkMngMaster.Checked = false;
                chkMngUsers.Checked = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}