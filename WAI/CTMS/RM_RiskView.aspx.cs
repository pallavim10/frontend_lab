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

namespace CTMS
{
    public partial class RM_RiskView : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["User_ID"] == null)
                    {
                        Response.Redirect("~/SessionExpired.aspx", false);
                    }

                    Session["TYPE"] = Request.QueryString["TYPE"]; //HttpUtility.HtmlDecode(Request.QueryString["TYPE"]);
                    Session["ID"] = Request.QueryString["RiskId"];// HttpUtility.HtmlDecode(Request.QueryString["RiskId"]);

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
                DataSet ds = new DataSet();
                ds = dal.getRiskList(Action: "Update", Id: Session["ID"].ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtRiskID.Text = ds.Tables[0].Rows[0]["RiskActualId"].ToString();
                    lblCateg.Text = ds.Tables[0].Rows[0]["Category"].ToString();
                    lblSubCat.Text = ds.Tables[0].Rows[0]["SubCategory"].ToString();
                    lblfactor.Text = ds.Tables[0].Rows[0]["Factor"].ToString();
                    txtRiskCons.Text = ds.Tables[0].Rows[0]["Risk_Description"].ToString();
                    txtRiskImpact.Text = ds.Tables[0].Rows[0]["Impacts"].ToString();
                    txtSugMitig.Text = ds.Tables[0].Rows[0]["Possible_Mitigations"].ToString();
                    txtSugRiskCat.Text = ds.Tables[0].Rows[0]["SugestedRiskCategory"].ToString();
                    lblnature.Text = ds.Tables[0].Rows[0]["RiskNature"].ToString();
                    txtTransCat.Text = ds.Tables[0].Rows[0]["TranscelerateCategory"].ToString();
                    txtTransSubCat.Text = ds.Tables[0].Rows[0]["TranscelerateSubCategory"].ToString();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}