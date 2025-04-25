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
    public partial class RM_INDIC_TRIGGER : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    BINDDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BINDDATA()
        {
            try
            {
                DataSet ds = dal.Risk_Indicator_SP(Action: "GET_RM_INDIC_TRIGGER", ID: Request.QueryString["TILEID"].ToString());

                lblTileName.Text = ds.Tables[0].Rows[0]["TileName"].ToString();

                txtL1Trig.Text = ds.Tables[0].Rows[0]["L1"].ToString();
                txtL2Trig.Text = ds.Tables[0].Rows[0]["L2"].ToString();

                ddlLV0Trig.SelectedValue = ds.Tables[0].Rows[0]["LV0"].ToString();
                ddlLV1Trig.SelectedValue = ds.Tables[0].Rows[0]["LV1"].ToString();
                ddlLV2Trig.SelectedValue = ds.Tables[0].Rows[0]["LV2"].ToString();

                txtCL1Trig.Text = ds.Tables[0].Rows[0]["CL1"].ToString();
                txtCL2Trig.Text = ds.Tables[0].Rows[0]["CL2"].ToString();

                ddlCLV0Trig.SelectedValue = ds.Tables[0].Rows[0]["CLV0"].ToString();
                ddlCLV1Trig.SelectedValue = ds.Tables[0].Rows[0]["CLV1"].ToString();
                ddlCLV2Trig.SelectedValue = ds.Tables[0].Rows[0]["CLV2"].ToString();

                txtInvL1Trig.Text = ds.Tables[0].Rows[0]["InvL1"].ToString();
                txtInvL2Trig.Text = ds.Tables[0].Rows[0]["InvL2"].ToString();

                ddlInvLV0Trig.SelectedValue = ds.Tables[0].Rows[0]["InvLV0"].ToString();
                ddlInvLV1Trig.SelectedValue = ds.Tables[0].Rows[0]["InvLV1"].ToString();
                ddlInvLV2Trig.SelectedValue = ds.Tables[0].Rows[0]["InvLV2"].ToString();

                txtLAct1.Text = ds.Tables[0].Rows[0]["LAct1"].ToString();
                txtLAct2.Text = ds.Tables[0].Rows[0]["LAct2"].ToString();

                txtCLAct1.Text = ds.Tables[0].Rows[0]["CLAct1"].ToString();
                txtCLAct2.Text = ds.Tables[0].Rows[0]["CLAct2"].ToString();

                txtInvLAct1.Text = ds.Tables[0].Rows[0]["InvLAct1"].ToString();
                txtInvLAct2.Text = ds.Tables[0].Rows[0]["InvLAct2"].ToString();

                ddlLPost1.SelectedValue = ds.Tables[0].Rows[0]["LPost1"].ToString();
                ddlLPost2.SelectedValue = ds.Tables[0].Rows[0]["LPost2"].ToString();

                ddlCLPost1.SelectedValue = ds.Tables[0].Rows[0]["CLPost1"].ToString();
                ddlCLPost2.SelectedValue = ds.Tables[0].Rows[0]["CLPost2"].ToString();

                ddlInvLPost1.SelectedValue = ds.Tables[0].Rows[0]["InvLPost1"].ToString();
                ddlInvLPost2.SelectedValue = ds.Tables[0].Rows[0]["InvLPost2"].ToString();

                if (ds.Tables[0].Rows[0]["IndicID"].ToString() == "")
                {
                    divSetRiskIndic.Visible = true;
                }
                else
                {
                    divSetRiskIndic.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void bntSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.Risk_Indicator_SP(Action: "INSERT_RM_INDC_TRIG",
                L1: txtL1Trig.Text,
                L2: txtL2Trig.Text,
                CL1: txtCL1Trig.Text,
                CL2: txtCL2Trig.Text,
                InvL1: txtInvL1Trig.Text,
                InvL2: txtInvL2Trig.Text,
                LV0: ddlLV0Trig.SelectedValue,
                LV1: ddlLV1Trig.SelectedValue,
                LV2: ddlLV2Trig.SelectedValue,
                CLV0: ddlCLV0Trig.SelectedValue,
                CLV1: ddlCLV1Trig.SelectedValue,
                CLV2: ddlCLV2Trig.SelectedValue,
                InvLV0: ddlInvLV0Trig.SelectedValue,
                InvLV1: ddlInvLV1Trig.SelectedValue,
                InvLV2: ddlInvLV2Trig.SelectedValue,
                LAct1: txtLAct1.Text,
                LAct2: txtLAct2.Text,
                CLAct1: txtCLAct1.Text,
                CLAct2: txtCLAct2.Text,
                InvLAct1: txtInvLAct1.Text,
                InvLAct2: txtInvLAct2.Text,
                LPost1: ddlLPost1.SelectedValue,
                LPost2: ddlLPost2.SelectedValue,
                CLPost1: ddlCLPost1.SelectedValue,
                CLPost2: ddlCLPost2.SelectedValue,
                InvLPost1: ddlInvLPost1.SelectedValue,
                InvLPost2: ddlInvLPost2.SelectedValue,
                ID: Request.QueryString["TILEID"].ToString()
                );

                Response.Write("<script> alert('Triggers Set Successfully.')</script>");
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}