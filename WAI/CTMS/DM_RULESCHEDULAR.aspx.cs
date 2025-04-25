using CTMS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DM_RULESCHEDULAR : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SCHEDULE();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SCHEDULE()
        {
            try
            {
                DataSet ds = dal_DM.DM_SCHEDULE_SP(ACTION: "GET_SCHEDULE");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtrulePeriod.Text = ds.Tables[0].Rows[0]["WINDOWPERIOD"].ToString();
                    txtdate.Text = ds.Tables[0].Rows[0]["STARTDATE"].ToString();
                    txttime.Text = ds.Tables[0].Rows[0]["STARTTIME"].ToString();
                    lbnUpdate.Visible = true;
                }
                else 
                {
                    lbnUpdate.Visible = false;
                }
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

                dal_DM.DM_SCHEDULE_SP(ACTION: "INSERT_SECHULAR", WINDOWPERIOD: txtrulePeriod.Text,STARTDATE: txtdate.Text,STARTTIME:txttime.Text);
                lbnUpdate.Visible = true;
                Response.Write("<script>alert('Rule Schedular Defined Successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dal_DM.DM_SCHEDULE_SP(ACTION: "DELETE_SECHULAR");

                txtrulePeriod.Text = "";
                txtdate.Text = "";
                txttime.Text = "";
                lbnUpdate.Visible = false;
                Response.Write("<script>alert('Rule Schedular deleted Successfully.')</script>");

                

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        

    }
}