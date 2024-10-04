using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class RegisterSuperUser : System.Web.UI.Page
    {
        CommonFunction cf = new CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CHECK_USER_EXIST();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void CHECK_USER_EXIST()
        {
            SqlConnection SqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            DataSet ds = UMT_USERS_SP(SqlConn, ACTION: "CHECK_USER_EXIST");
            if (ds.Tables[0].Rows[0]["Count"].ToString() != "0")
            {
                string script = @"
                         swal({
                         title: 'Success!',
                         text: 'Super User Already Exists. Click OK to Login.',
                         icon: 'success',
                         button: 'OK'
                }).then((value) => {
                         window.location.href = 'LoginPage.aspx'; 
                });";

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
            }

        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtSponsor.Text.Trim() != "" && txtPROJECT.Text.Trim() != "" && txtFirstName.Text.Trim() != "" && txtLastName.Text.Trim() != "" && txtEmailID.Text.Trim() != "" && txtContactNo.Text.Trim() != "")
            {
                if (!int.TryParse(txtContactNo.Text.Trim(), out _))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter a valid Contact Number.', 'warning');", true);
                }
                else
                {
                    CREATE_SuperUser();

                    string script = @"
                         swal({
                         title: 'Success!',
                         text: 'Super User Register Successfully.',
                         icon: 'success',
                         button: 'OK'
                            }).then((value) => {
                         window.location.href = 'LoginPage.aspx'; 
                    });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                }



            }
            else if (txtSponsor.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Sponsor Name.', 'warning');", true);
            }
            else if (txtPROJECT.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Project Name.', 'warning');", true);
            }
            else if (txtFirstName.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter First Name.', 'warning');", true);
            }
            else if (txtLastName.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Last Name.', 'warning');", true);
            }
            else if (txtEmailID.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Email ID.', 'warning');", true);
            }
            else if (txtContactNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Please Enter Contact Number.', 'warning');", true);
            }

        }

        private void CREATE_SuperUser()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;

            try
            {
                cmd = new SqlCommand("UMT_SUPER_USERS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fname", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@Lname", txtLastName.Text);
                cmd.Parameters.AddWithValue("@EmailID", txtEmailID.Text);
                cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@PROJECT", txtPROJECT.Text);
                cmd.Parameters.AddWithValue("@SPONSOR", txtSponsor.Text);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();

                string USERID = ds.Tables[0].Rows[0]["USER_ID"].ToString();
                USER_ACTIVATION_MAIL_FIRST(con, txtEmailID.Text, txtFirstName.Text + " " + txtLastName.Text, "Super User", USERID);
                USER_ACTIVATION_MAIL_SECOND(con, USERID, txtEmailID.Text);
                USER_ACTIVATION_MAIL_THIRD(con, USERID, txtEmailID.Text);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
        }

        private void USER_ACTIVATION_MAIL_FIRST(SqlConnection SqlConn, string User_EmailID, string USERNAME, string STUDYROLE, string USERID)
        {
            try
            {
                DataSet ds1 = UMT_USER_DETAILS_SP(SqlConn, USERID);
                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "", ACTIONS = "", SYSTEM_LIST = "", URL = "";
                ACTIONS = "User Activation / Deactivation";
                ds = UMT_EMAIL_SP(con: SqlConn, ACTION: "GET_USER_ACTIVATION_DEACTIVATION", ACTIONS: ACTIONS);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["To"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CC"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCC"].ToString();
                }

                EMAILIDS += "," + User_EmailID;

                DataSet ds1Roles = UMT_USERS_SP(SqlConn, ACTION: "Show_UserRoles", User_ID: USERID);

                foreach (DataRow dr in ds1Roles.Tables[0].Rows)
                {
                    SYSTEM_LIST += "<li>" + dr["System Name"].ToString() + "</li>";
                }
                URL = "/Login.aspx";
                SUBJECT = "User Activation";
                dsEmail = UMT_EMAIL_SP(con: SqlConn, ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "USER_ACTIVATION_MAIL_FIRST");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USERNAME]", USERNAME);
                    SUBJECT = SUBJECT.Replace("[STUDYROLE]", STUDYROLE);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", ds1.Tables[0].Rows[0]["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[USERNAME]", USERNAME);
                    BODY = BODY.Replace("[STUDYROLE]", STUDYROLE);
                    BODY = BODY.Replace("[URL]", URL);
                    BODY = BODY.Replace("[SYSTEM_LIST]", SYSTEM_LIST);
                    BODY = BODY.Replace("[USERNAME]", ds1.Tables[0].Rows[0]["User_Name"].ToString());
                    BODY = BODY.Replace("[DATETIME]", DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                    BODY = BODY.Replace("[TIMEZONE]", ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString());
                }

                MAIL_SEND(EMAILIDS, CCEMAILIDS, BCCEMAILIDS, SUBJECT, BODY);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void USER_ACTIVATION_MAIL_SECOND(SqlConnection SqlConn, string USERID, string User_EmailID)
        {
            try
            {
                DataSet ds1 = UMT_USER_DETAILS_SP(SqlConn, USERID);

                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                SUBJECT = "User Activation";
                EMAILIDS = User_EmailID;
                dsEmail = UMT_EMAIL_SP(con: SqlConn, ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "USER_ACTIVATION_MAIL_SECOND");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USERID]", USERID);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", ds1.Tables[0].Rows[0]["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[USERID]", USERID);
                    BODY = BODY.Replace("[USERNAME]", ds1.Tables[0].Rows[0]["User_Name"].ToString());
                    BODY = BODY.Replace("[DATETIME]", DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                    BODY = BODY.Replace("[TIMEZONE]", ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString());
                }

                MAIL_SEND(EMAILIDS, CCEMAILIDS, BCCEMAILIDS, SUBJECT, BODY);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void USER_ACTIVATION_MAIL_THIRD(SqlConnection SqlConn, string USERID, string User_EmailID)
        {
            try
            {
                string PWD = "";
                DataSet ds1 = UMT_USER_DETAILS_SP(SqlConn, USERID);
                if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables.Count > 0)
                {
                    PWD = ds1.Tables[0].Rows[0]["PWD"].ToString();
                }

                DataSet ds = new DataSet();
                DataSet dsEmail = new DataSet();
                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                EMAILIDS = User_EmailID;
                SUBJECT = "User Activation";
                dsEmail = UMT_EMAIL_SP(con: SqlConn, ACTION: "GET_EMAIL_TEMPLATE", Email_Code: "USER_ACTIVATION_MAIL_THIRD");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[USERNAME]", ds1.Tables[0].Rows[0]["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", ds1.Tables[0].Rows[0]["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[PWD]", PWD);
                    BODY = BODY.Replace("[USERNAME]", ds1.Tables[0].Rows[0]["User_Name"].ToString());
                    BODY = BODY.Replace("[DATETIME]", DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                    BODY = BODY.Replace("[TIMEZONE]", ds1.Tables[0].Rows[0]["TimeZone_Value"].ToString());
                }

                MAIL_SEND(EMAILIDS, CCEMAILIDS, BCCEMAILIDS, SUBJECT, BODY);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void MAIL_SEND(string EMAILIDS, string CCEMAILIDS, string BCCEMAILIDS, string SUBJECT, string BODY)
        {
            try
            {
                cf.Email_Users(
                    EmailAddress: EMAILIDS,
                    CCEmailAddress: CCEMAILIDS,
                    BCCEmailAddress: BCCEMAILIDS,
                    subject: SUBJECT,
                    body: BODY
                  );
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private DataSet UMT_USER_DETAILS_SP(SqlConnection Con, string USERID)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("UMT_USER_DETAILS_SP", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", USERID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                adp = null;
                Con.Close();
            }
            return ds;
        }

        private DataSet UMT_EMAIL_SP(SqlConnection con, string ACTIONS = null, string ACTION = null, string Email_Code = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("UMT_EMAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ACTIONS", ACTIONS);
                cmd.Parameters.AddWithValue("@Email_Code", Email_Code);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        private DataSet UMT_USERS_SP(SqlConnection Conn, string ACTION, string User_ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("UMT_USERS_SP", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@User_ID", User_ID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                adp = null;
                Conn.Close();
            }
            return ds;
        }

    }
}