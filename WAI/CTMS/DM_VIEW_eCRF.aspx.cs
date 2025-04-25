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
    public partial class DM_VIEW_eCRF : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.IsPostBack)
                {
                    GETDATA();
                }
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
                DataSet ds = dal_DM.DM_eCRF_SP(ACTION: "GETMODULE_FOR_REPORT_CREATECRF");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.DataSource = ds;
                    ddlModule.DataTextField = "MODULENAME";
                    ddlModule.DataValueField = "ID";
                    ddlModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("View_CRF.aspx?MODULEID=" + ddlModule.SelectedValue);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}