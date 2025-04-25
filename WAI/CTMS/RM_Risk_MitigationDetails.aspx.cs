using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace CTMS
{
    public partial class RM_Risk_MitigationDetails : System.Web.UI.Page
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


                Session["TYPE"] = Request.QueryString["TYPE"]; //HttpUtility.HtmlDecode(Request.QueryString["TYPE"]);
                Session["RiskID"] = Request.QueryString["RiskId"];// HttpUtility.HtmlDecode(Request.QueryString["RiskId"]);

                if (!this.IsPostBack)
                {
                    getData();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void getData()
        {
            try
            {
                DataTable dt = dal.getBucketById(Id: Session["RiskID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    hfdID.Value = dt.Rows[0]["RiskId"].ToString();
                    lblRiskIds.Text = dt.Rows[0]["RiskActualId"].ToString();
                    lbl_Category.Text = dt.Rows[0]["Category"].ToString();
                    lbl_SubCategory.Text = dt.Rows[0]["SubCategory"].ToString();
                    lbl_Factor.Text = dt.Rows[0]["Factor"].ToString();
                    drpProbability.SelectedValue = dt.Rows[0]["P"].ToString();
                    hf_P.Value = dt.Rows[0]["P"].ToString();
                    drpSeverity.SelectedValue = dt.Rows[0]["S"].ToString();
                    hf_S.Value = dt.Rows[0]["S"].ToString();
                    drpDetectability.SelectedValue = dt.Rows[0]["D"].ToString();
                    hf_D.Value = dt.Rows[0]["D"].ToString();
                    txtRPN.Text = dt.Rows[0]["RPN"].ToString();
                    txtRiskCategory.Text = dt.Rows[0]["RiskCategory"].ToString();
                    txtNotes.Text = dt.Rows[0]["RNotes"].ToString();
                    if (dt.Rows[0]["Up_Trigger"].ToString() != "0" || dt.Rows[0]["Up_Trigger"].ToString() != "")
                    {
                        txtUp.Text = dt.Rows[0]["Up_Trigger"].ToString();
                    }
                    if (dt.Rows[0]["Down_Trigger"].ToString() != "0" || dt.Rows[0]["Down_Trigger"].ToString() != "")
                    {
                        txtDown.Text = dt.Rows[0]["Down_Trigger"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        protected void bntSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (hf_P.Value != drpProbability.SelectedValue || hf_S.Value != drpSeverity.SelectedValue || hf_D.Value != drpDetectability.SelectedValue)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "OpenPopup();", true);
                }
                else
                {
                    string msg = dal.updateRiskBucket
                        (
                        Id: Session["RiskID"].ToString(),
                        P: drpProbability.SelectedItem.Text,
                        S: drpSeverity.SelectedItem.Text,
                        D: drpDetectability.SelectedItem.Text,
                        RPN: txtRPN.Text,
                        RiskCat: txtRiskCategory.Text,
                        Notes: txtNotes.Text,
                        Up_Trigger: txtUp.Text,
                        Down_Trigger: txtDown.Text,
                        User_ID: Session["User_ID"].ToString()
                        );

                    Response.Write("<script> alert('Record Updated successfully.')</script>");
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}