using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class NIWRS_UNBLIND_APP : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    grdUNBLINDAPP.Columns[2].HeaderText = Session["SUBJECTTEXT"].ToString();
                    GetSites();
                    GET_SUBSITE();
                    GET_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetSites()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        drpSite.DataSource = ds.Tables[0];
                        drpSite.DataValueField = "INVNAME";
                        drpSite.DataBind();
                    }
                    else
                    {
                        drpSite.DataSource = ds.Tables[0];
                        drpSite.DataValueField = "INVNAME";
                        drpSite.DataBind();
                        drpSite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBSITE()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_SUBSITE", SITEID: drpSite.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        drpSubSite.DataSource = ds.Tables[0];
                        drpSubSite.DataValueField = "SubSiteID";
                        drpSubSite.DataTextField = "SubSiteID";
                        drpSubSite.DataBind();
                    }
                    else
                    {
                        drpSubSite.DataSource = ds.Tables[0];
                        drpSubSite.DataValueField = "SubSiteID";
                        drpSubSite.DataTextField = "SubSiteID";
                        drpSubSite.DataBind();
                        drpSubSite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
                else
                {
                    drpSubSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_DATA()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UNBLIND_SP(ACTION: "GET_UNBLIND_APPROVE_LIST", SITEID: drpSite.SelectedValue, SUBSITEID: drpSubSite.SelectedValue, ENTEREDBY: Session["User_ID"].ToString());
                grdUNBLINDAPP.DataSource = ds;
                grdUNBLINDAPP.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBSITE();
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSubSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdUNBLINDAPP_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();

                if (e.CommandName == "Unblind")
                {
                    Response.Redirect("NIWRS_UNBLIND.aspx?ID=" + ID);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}