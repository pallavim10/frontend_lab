using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CTMS
{
    public partial class RM_Risk_List : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_ID"] == null)
            {
                Response.Redirect("~/SessionExpired.aspx", false);
            }

            getData();

            //try
            //{
            //    if (!this.IsPostBack)
            //    {
            //        DataSet ds = new DataSet();
            //        ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "DEPT");


            //        Drp_Department.DataSource = ds;
            //        Drp_Department.DataTextField = "TEXT";
            //        Drp_Department.DataValueField = "VALUE";
            //        Drp_Department.DataBind();
            //        ListItem removeItem = Drp_Department.Items.FindByValue("0");
            //        Drp_Department.Items.Remove(removeItem);
            //        Drp_Department.Items.Insert(0, new ListItem("All", "0"));


            //        ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Status");
            //        drpStatus.DataSource = ds;
            //        drpStatus.DataTextField = "TEXT";
            //        drpStatus.DataValueField = "VALUE";
            //        drpStatus.DataBind();
            //        ListItem item = drpStatus.Items.FindByValue("0");
            //        drpStatus.Items.Remove(item);
            //        drpStatus.Items.Insert(0, new ListItem("All", "0"));

            //        getData();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    lblErrorMsg.Text = ex.Message.ToString();
            //}
        }

        private void getData()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.getRiskList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    GrdRisk_list.DataSource = ds.Tables[0];
                    GrdRisk_list.DataBind();
                }
                else
                {
                    GrdRisk_list.DataSource = null;
                    GrdRisk_list.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnAddRisk_Click(object sender, EventArgs e)
        {
            getData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            getData();
        }


        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        protected void GrdRisk_list_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Risk_ID")
                {
                    string RiskId = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
                    string TYPE = HttpUtility.UrlEncode(Encrypt("UPDATE"));
                    Response.Write("<script>window.open('RM_Risk_MitigationDetails.aspx?RiskId='" + RiskId + "'&TYPE='" + TYPE + "', 'hello', 'width=700,height=400,scrollbars=yes');</script>");

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}