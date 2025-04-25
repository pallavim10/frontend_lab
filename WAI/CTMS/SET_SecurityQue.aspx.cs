using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
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

                Response.Write("<script> alert('Security question set successfully');window.location='login.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}