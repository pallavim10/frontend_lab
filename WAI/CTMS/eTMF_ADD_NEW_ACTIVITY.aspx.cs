using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_ADD_NEW_ACTIVITY : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    if (!this.IsPostBack)
                    {
                        DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "Get_FOL_SUBFOL", ID: Request.QueryString["ArtifactsID"].ToString());

                        lblZones.Text = ds.Tables[0].Rows[0]["Folder"].ToString();
                        lblSections.Text = ds.Tables[0].Rows[0]["Sub_Folder_Name"].ToString();
                        lblArtifacts.Text = ds.Tables[0].Rows[0]["Artifact_Name"].ToString();

                        Session["SET_ZoneID"] = ds.Tables[0].Rows[0]["ZoneID"].ToString();
                        Session["SET_SectionID"] = ds.Tables[0].Rows[0]["SectionID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("eTMF_SetExpDocs.aspx");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "INSERT_NEW_ACTIVITY",
                 RefNo: txtRefNo.Text,
                 DocName: txtDocName.Text,
                 AutoReplace: ddlAutoReplace.SelectedValue,
                 VerTYPE: ddlVerType.SelectedValue,
                 VerDate: chkVerDate.Checked,
                 VerSPEC: chkVerSpec.Checked,
                 UnblindingType: ddlUnblind.SelectedValue,
                 ID: Request.QueryString["ArtifactsID"].ToString()
                 );

                dal_eTMF.eTMF_SET_SP(ACTION: "INSERT_NEW_ACTIVITY_REFNO",
                DocID: ds.Tables[0].Rows[0]["ID"].ToString(),
                DocTypeId: Request.QueryString["DOCTYPEID"].ToString(),
                ID: Request.QueryString["ArtifactsID"].ToString()
                );

                string URL = Session["prevURL"].ToString();
                Response.Write("<script> alert('Sub-Artifact Added successfully.');window.location='" + URL + "'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}