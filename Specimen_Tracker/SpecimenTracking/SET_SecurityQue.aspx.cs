using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class SET_SecurityQue : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dal_UMT.UMT_SECURITY_QUES_SP(
                    ACTION: "UPDATE_SECURITY",
                    UserID: Session["User_ID"].ToString(),
                    SECURITY_QUE: txtQue.Text,
                    SECURITY_ANS: txtAns.Text
                    );

                string script = @"
                        swal({
                        title: 'Success!',
                        text: 'Security question set successfully.',
                        icon: 'success',
                        button: 'OK'
                 }).then((value) => {
                        window.location.href = 'LoginPage.aspx'; 
                });";

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }
    }
}