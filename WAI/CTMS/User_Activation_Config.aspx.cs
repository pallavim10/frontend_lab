using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Net.Sockets;
using System.Net;

namespace CTMS
{
    public partial class User_Activation_Config : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GetEmailDetails();
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
                DataSet ds = dal.EMAIL_CONFIG_SP(
                     Action: "INSERT_UPDATE_USER_ACT_DETAILS",
                     E_Sub: txtEmailSubject.Text,
                     E_Body: txtEmailBody.Text,
                     E_CC: txtCCEMAILIDs.Text,
                     E_TO: txtTOEMAILIDs.Text,
                     IPADDRESS: GetIpAddress()
                     );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string ip_add = "";
            foreach (var ipp in host.AddressList)
            {
                if (ipp.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip_add = ipp.ToString();
                }
            }
            return ip_add;
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("User_Activation_Config.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvShow_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void GetEmailDetails()
        {
            try
            {
                DataSet ds = dal.EMAIL_CONFIG_SP(Action: "Get_User_Act_Details");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtEmailBody.Text = ds.Tables[0].Rows[0]["E_Body"].ToString();
                    txtEmailSubject.Text = ds.Tables[0].Rows[0]["E_Sub"].ToString();
                    txtTOEMAILIDs.Text = ds.Tables[0].Rows[0]["E_TO"].ToString();
                    txtCCEMAILIDs.Text = ds.Tables[0].Rows[0]["E_CC"].ToString();
                }
                else
                {
                    txtEmailBody.Text = "";
                    txtEmailSubject.Text = "";
                    txtTOEMAILIDs.Text = "";
                    txtCCEMAILIDs.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}