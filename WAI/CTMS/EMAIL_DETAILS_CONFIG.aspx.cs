using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;


namespace CTMS
{
    public partial class EMAIL_DETAILS_CONFIG : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_OPEN_EMAIL();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvEmailds.Rows.Count; i++)
                {
                    TextBox txtEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtEMAILIDs");
                    TextBox txtCCEMAILIDs = (TextBox)gvEmailds.Rows[i].FindControl("txtCCEMAILIDs");
                    Label lblEmailType = (Label)gvEmailds.Rows[i].FindControl("lblEmailType");

                    DataSet ds = dal.EMAIL_CONFIG_SP(
                     Action: "UPDATE_EMAIL_DETAILS_CONFIG",
                     Email_Type: lblEmailType.Text,
                     E_CC: txtCCEMAILIDs.Text,
                     E_TO: txtEMAILIDs.Text
                     );
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("EMAIL_DETAILS_CONFIG.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GET_OPEN_EMAIL()
        {
            try
            {
                DataSet ds = dal.EMAIL_CONFIG_SP(Action: "Email_Deatils_Config");
                gvEmailds.DataSource = ds;
                gvEmailds.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}