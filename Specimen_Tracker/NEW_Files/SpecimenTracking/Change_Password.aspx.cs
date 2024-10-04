using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class Change_Password : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string script = "";
                lblOldPwd.Visible = false;
                lblNewPwd.Visible = false;

                DataSet ds = dal_UMT.UMT_UPDATE_PWD(
                    UserID: Session["User_ID"].ToString(),
                    OldPwd: txt_Old_Pwd.Text,
                    NewPwd: txt_New_Pwd.Text
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string RESULT = ds.Tables[0].Rows[0][0].ToString();

                    switch (RESULT)
                    {
                        case "Old password entered is invalid":
                            lblOldPwd.Visible = true;

                            break;

                        case "New password must not be same as last passwords":
                            lblNewPwd.Text = RESULT;
                            lblNewPwd.Visible = true;

                            break;

                        case "New password must be other than last five passwords":
                            lblNewPwd.Text = RESULT;
                            lblNewPwd.Visible = true;

                            break;

                        case "Password updated successfully, Please set security question":
                            
                            script = @"
                                    swal({
                                        title: 'Success!',
                                        text: 'Password changed successfully, Please set your security question.',
                                        icon: 'success',
                                        button: 'OK'
                                    }).then((value) => {
                                        window.location.href = 'SET_SecurityQue.aspx'; 
                                    });";

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);

                            break;

                        default:
                            
                            script = @"
                                    swal({
                                        title: 'Success!',
                                        text: 'Password changed successfully',
                                        icon: 'success',
                                        button: 'OK'
                                    }).then((value) => {
                                        window.location.href = 'LoginPage.aspx'; 
                                    });";

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }
    }
}