using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class IWRS_Threshold_Metric : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_SITE();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP();
                gvSites.DataSource = ds;
                gvSites.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSites_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string COUNTRYID = dr["COUNTRYID"].ToString();
                    string SITEID = dr["SITEID"].ToString();

                    DataSet ds = dal_IWRS.IWRS_KITS_SETUP_SP(ACTION: "GET_KITS_TRIGGERS", COUNTRYID: COUNTRYID, SITEID: SITEID);
                    GridView gvTriggers = (GridView)e.Row.FindControl("gvTriggers");
                    gvTriggers.DataSource = ds;
                    gvTriggers.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_TRIGGERS();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Triggers Set Successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_TRIGGERS()
        {
            try
            {
                for (int a = 0; a < gvSites.Rows.Count; a++)
                {
                    string COUNTRYID = ((HiddenField)gvSites.Rows[a].FindControl("COUNTRYID")).Value;
                    string SITEID = ((Label)gvSites.Rows[a].FindControl("SITEID")).Text;

                    GridView gvTriggers = gvSites.Rows[a].FindControl("gvTriggers") as GridView;
                    for (int b = 0; b < gvTriggers.Rows.Count; b++)
                    {
                        string TREAT_GRP = ((Label)gvTriggers.Rows[b].FindControl("TREAT_GRP")).Text;
                        string TREAT_GRP_NAME = ((Label)gvTriggers.Rows[b].FindControl("TREAT_GRP_NAME")).Text;
                        string TREAT_STRENGTH = ((Label)gvTriggers.Rows[b].FindControl("TREAT_STRENGTH")).Text;
                        string TRIGGER_VAL = ((TextBox)gvTriggers.Rows[b].FindControl("TRIGGER_VAL")).Text;
                        string RESPO_EMAIL = ((TextBox)gvTriggers.Rows[b].FindControl("RESPO_EMAIL")).Text;


                        dal_IWRS.IWRS_KITS_SETUP_SP(
                        ACTION: "UPDATE_KITS_TRIGGERS",
                        COUNTRYID: COUNTRYID,
                        SITEID: SITEID,
                        TREAT_GRP: TREAT_GRP,
                        TREAT_GRP_NAME: TREAT_GRP_NAME,
                        TREAT_STRENGTH: TREAT_STRENGTH,
                        TRIGGER_VAL: TRIGGER_VAL,
                        RESPO_EMAIL: RESPO_EMAIL
                       );

                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}