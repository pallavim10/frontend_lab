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
    public partial class NIWRS_KIT_EXPIRY : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_BLOCK();
                    GET_KITNO();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_BLOCK()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_BOLOCK");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBlock.DataSource = ds;
                    ddlBlock.DataValueField = "LOTNO";
                    ddlBlock.DataTextField = "LOTNO";
                    ddlBlock.DataBind();
                    ddlBlock.Items.Insert(0, new ListItem("--All--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_KITNO()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_CENTRAL_SP(ACTION: "GET_LOTNO",
                LOTNO: ddlBlock.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlKitNo.DataSource = ds;
                    ddlKitNo.DataValueField = "KITNO";
                    ddlKitNo.DataTextField = "KITNO";
                    ddlKitNo.DataBind();
                    ddlKitNo.Items.Insert(0, new ListItem("--All--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_KITNO();
                GETDATA();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlKitNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_REPORT_SP(ACTION: "GET_KIT_DATA",
                LOTNO: ddlBlock.SelectedValue,
                KITNO: ddlKitNo.SelectedValue,
                EXPIRYDAT: txtExpiryDate.Text
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblCentralDepot.Text = ds.Tables[0].Rows[0]["CENTRAL_DEPOT"].ToString();
                    lblCentralCountry.Text = ds.Tables[0].Rows[0]["CENTRAL_COUNTRY"].ToString();
                    lblCountryDepot.Text = ds.Tables[0].Rows[0]["COUNTRY_DEPOT"].ToString();
                    lblCountrySite.Text = ds.Tables[0].Rows[0]["COUNTRY_SITE"].ToString();
                    lblSite.Text = ds.Tables[0].Rows[0]["SITE"].ToString();

                    DivData.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "Update_expiry_Date",
                //    LOTNO: ddlBlock.SelectedValue,
                //    KITNO: ddlKitNo.SelectedValue,
                //    TREAT_GRP: txtExpiryDate.Text,
                //    EXPIRYDAT: txtExp.Text,
                //    EXPIRY_COMMENT: txtDesc.Text,
                //    ENTEREDBY: Session["USER_ID"].ToString()
                //    );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void txtExpiryDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GETDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}