using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class Survey_User : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string ID = INSERT_USER();

                Response.Redirect("Survey_Form.aspx?UserID=" + ID + "&Name=" + txtName.Text + "");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string INSERT_USER()
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal.SURVEY_SP(ACTION: "INSERT_USER", Name: txtName.Text, EmailID: txtEmailID.Text);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            return ds.Tables[0].Rows[0][0].ToString();
        }
    }
}