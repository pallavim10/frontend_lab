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
    public partial class Registrations : System.Web.UI.Page
    {
        DATA_DAL Data_Dal = new DATA_DAL();
        ActiveDirectory activeDirectory = new ActiveDirectory();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GETDATA();
                    if (Session["UserName"] == null)
                    {
                        Response.Redirect("SessionExpired.aspx", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            INSERT_DATA();

        }
        private void GETDATA()
        {
            string[] Details = new string[5];
            try
            {
                Details = activeDirectory.GetUserDetails(Session["Username"].ToString(), Session["Pwd"].ToString());

                txtUserName.Text = Session["Username"].ToString();
                txtFullName.Text = Details[0].ToString();
                txtContactNo.Text = Details[1].ToString();
                txtEmailID.Text = Details[2].ToString();
                txtDepartment.Text = Details[3].ToString();
                txtDesignation.Text = Details[4].ToString();
                Session["FirstName"] = Details[5].ToString();
                Session["LastName"] = Details[6].ToString();

                ViewState["Role"] = Details[4].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void INSERT_DATA()
        {
            string[] data = activeDirectory.ValidateUser(Session["Username"].ToString());

            if (txtFullName.Text == string.Empty)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Full Name', 'error');", true);
                //return; // return because we don't want to run normal code of buton click
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
            else
            {
                if (ViewState["Role"].ToString().Contains("Admin"))
                {
                    DataSet ds = Data_Dal.LOGIN_LOGS_SP(
                       ACTION: "INSERT_NEW_USER_Admin",
                       UserName: txtUserName.Text,
                       Name: txtFullName.Text,
                       EmailID: txtEmailID.Text,
                       SID: data[1].ToString(),
                       GUID: data[0].ToString(),
                       ContactNo: txtContactNo.Text,
                       Department: txtDepartment.Text,
                       Designation: txtDesignation.Text
                       );


                }
                else
                {
                    DataSet ds = Data_Dal.LOGIN_LOGS_SP(
                       ACTION: "INSERT_NEW_USER",
                       UserName: txtUserName.Text,
                       Name: txtFullName.Text,
                       EmailID: txtEmailID.Text,
                       SID: data[1].ToString(),
                       GUID: data[0].ToString(),
                       ContactNo: txtContactNo.Text,
                       Department: txtDepartment.Text,
                       Designation: txtDesignation.Text
                       );
                }

                Session["FullName"] = txtFullName.Text;

                Data_Dal.LOGIN_LOGS_SP(
                            ACTION: "INSERT_LOGIN_LOG",
                            UserName: txtUserName.Text,
                            FirstName: Session["FirstName"].ToString(),
                            LastName: Session["LastName"].ToString(),
                            Result: "Registered  New User Successfully",
                            HostIP: Data_Dal.GetMACAddress(),
                            IPADDRESS: Data_Dal.GetIpAddress()
                            );
            }
            string script = @"
                     swal({
                     title: 'Registration Successful',
                     text: 'Click OK to Log In.',
                     icon: 'success',
                     button: 'OK'
                     }).then((value) => {
                        window.location.href = 'LoginPage.aspx'; 
                     });";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);

        }
    }
}