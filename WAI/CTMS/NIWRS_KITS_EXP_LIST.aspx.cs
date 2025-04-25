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
    public partial class NIWRS_KITS_EXP_LIST : System.Web.UI.Page
    {
        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtComment.Attributes.Add("MaxLength", "200");

            try
            {
                if (!IsPostBack)
                {
                    GET_REQUEST_LIST();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_REQUEST_LIST()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "GET_REQUEST_LIST");
                gvREUQESTS.DataSource = ds;
                gvREUQESTS.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvREUQESTS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    Label lbl_NIWRS_KITS_POOL = (Label)e.Row.FindControl("lbl_NIWRS_KITS_POOL");
                    Label lbl_NIWRS_KITS_COUNTRY_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_COUNTRY_ORDERS");
                    Label lbl_NIWRS_KITS_COUNTRY = (Label)e.Row.FindControl("lbl_NIWRS_KITS_COUNTRY");
                    Label lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS");
                    Label lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS");
                    Label lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS");
                    Label lbl_NIWRS_KITS_SITE_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_SITE_ORDERS");
                    Label lbl_NIWRS_KITS_SITE = (Label)e.Row.FindControl("lbl_NIWRS_KITS_SITE");
                    Label lbl_NIWRS_KITS_SITE_TRANSF_ORDERS = (Label)e.Row.FindControl("lbl_NIWRS_KITS_SITE_TRANSF_ORDERS");

                    GridView grd_NIWRS_KITS_POOL = (GridView)e.Row.FindControl("grd_NIWRS_KITS_POOL");
                    GridView grd_NIWRS_KITS_COUNTRY_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_COUNTRY_ORDERS");
                    GridView grd_NIWRS_KITS_COUNTRY = (GridView)e.Row.FindControl("grd_NIWRS_KITS_COUNTRY");
                    GridView grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS");
                    GridView grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS");
                    GridView grd_NIWRS_KITS_SITE_COUNTRY_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_SITE_COUNTRY_ORDERS");
                    GridView grd_NIWRS_KITS_SITE_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_SITE_ORDERS");
                    GridView grd_NIWRS_KITS_SITE = (GridView)e.Row.FindControl("grd_NIWRS_KITS_SITE");
                    GridView grd_NIWRS_KITS_SITE_TRANSF_ORDERS = (GridView)e.Row.FindControl("grd_NIWRS_KITS_SITE_TRANSF_ORDERS");

                    DataSet ds = dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "GET_REQUEST_DETAILS", ID: drv["ID"].ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_POOL.DataSource = ds.Tables[0];
                        grd_NIWRS_KITS_POOL.DataBind();
                        lbl_NIWRS_KITS_POOL.Text = grd_NIWRS_KITS_POOL.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_POOL.DataSource = null;
                        grd_NIWRS_KITS_POOL.DataBind();
                        lbl_NIWRS_KITS_POOL.Text = "0";
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataSource = ds.Tables[1];
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_ORDERS.Text = grd_NIWRS_KITS_COUNTRY_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_ORDERS.Text = "0";
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY.DataSource = ds.Tables[2];
                        grd_NIWRS_KITS_COUNTRY.DataBind();
                        lbl_NIWRS_KITS_COUNTRY.Text = grd_NIWRS_KITS_COUNTRY.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY.DataBind();
                        lbl_NIWRS_KITS_COUNTRY.Text = "0";
                    }

                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataSource = ds.Tables[3];
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.Text = grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS.Text = "0";
                    }

                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataSource = ds.Tables[4];
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.Text = grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.DataBind();
                        lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS.Text = "0";

                    }

                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataSource = ds.Tables[5];
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS.Text = grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_SITE_COUNTRY_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS.Text = "0";
                    }

                    if (ds.Tables[6].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE_ORDERS.DataSource = ds.Tables[6];
                        grd_NIWRS_KITS_SITE_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_ORDERS.Text = grd_NIWRS_KITS_SITE_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_SITE_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_ORDERS.Text = "0";
                    }

                    if (ds.Tables[7].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE.DataSource = ds.Tables[7];
                        grd_NIWRS_KITS_SITE.DataBind();
                        lbl_NIWRS_KITS_SITE.Text = grd_NIWRS_KITS_SITE.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE.DataSource = null;
                        grd_NIWRS_KITS_SITE.DataBind();
                        lbl_NIWRS_KITS_SITE.Text = "0";
                    }

                    if (ds.Tables[8].Rows.Count > 0)
                    {
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataSource = ds.Tables[8];
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_TRANSF_ORDERS.Text = grd_NIWRS_KITS_SITE_TRANSF_ORDERS.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataSource = null;
                        grd_NIWRS_KITS_SITE_TRANSF_ORDERS.DataBind();
                        lbl_NIWRS_KITS_SITE_TRANSF_ORDERS.Text = "0";
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvREUQESTS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                hdnApproval.Value = e.CommandName.ToString();
                hdnREQUESTID.Value = e.CommandArgument.ToString();
                modalApp.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
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
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void btnSumit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtComment.Text.Trim() != "")
                {
                    dal_IWRS.IWRS_KITS_EXP_SP(
                    ACTION: "UPDATE_REQUEST_APPROVAL",
                    ID: hdnREQUESTID.Value,
                    ACTDETAILS: hdnApproval.Value,
                    ACTCOMMENT: txtComment.Text
                    );

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request " + hdnApproval.Value + " Successfully.');window.location='NIWRS_KITS_EXP_LIST.aspx'", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter comment.');", true);
                    modalApp.Show();
                }

                string GENERATEREQFOR = "";
                string NEWEXPDATE = "";
                string STATUS = hdnApproval.Value;

                DataSet ds = dal_IWRS.IWRS_KITS_EXP_SP(ACTION: "GET_REQUEST_LIST_NEW",ID: hdnREQUESTID.Value);

                if(ds.Tables.Count> 0 && ds.Tables[0].Rows.Count >0)
                {
                    GENERATEREQFOR = ds.Tables[0].Rows[0]["Criteria"].ToString();
                    NEWEXPDATE = ds.Tables[0].Rows[0]["REQUEST_EXPDAT"].ToString();

                }

                if (hdnApproval.Value == "Approved")
                {
                    SEND_MAIL(GENERATEREQFOR, NEWEXPDATE, STATUS);
                }
                else
                {
                    SEND_MAIL(GENERATEREQFOR, NEWEXPDATE, STATUS);
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SEND_MAIL(string GENERATEREQFOR,  string NEWEXPDATE, string STATUS)
        {
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                string EMAILIDS = "", CCEMAILIDS = "", BCCEMAILIDS = "", SUBJECT = "", BODY = "";
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_EMAILS", STRATA: "REQUEST_UPDATE_EXPIRY_STATUS");

                if (ds.Tables.Count > 0)
                {
                    EMAILIDS = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();
                    CCEMAILIDS = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();
                    BCCEMAILIDS = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();
                }

                DataSet dsEmail = dal_IWRS.IWRS_SET_EMAIL_SP(ACTION: "GET_EMAIL_TEMPLATE", STRATA: "Status_Of_Update_Kit_Expiry_Request");

                if (dsEmail.Tables.Count > 0 && dsEmail.Tables[0].Rows.Count > 0)
                {
                    SUBJECT = dsEmail.Tables[0].Rows[0]["Email_Subject"].ToString();
                    SUBJECT = SUBJECT.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    SUBJECT = SUBJECT.Replace("[GENERATEREQFOR]", GENERATEREQFOR);
                    SUBJECT = SUBJECT.Replace("[NEWEXPDATE]", NEWEXPDATE);
                    SUBJECT = SUBJECT.Replace("[STATUS]", STATUS);
                    SUBJECT = SUBJECT.Replace("[USERNAME]", Session["User_Name"].ToString());
                    SUBJECT = SUBJECT.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    SUBJECT = SUBJECT.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());

                    BODY = dsEmail.Tables[0].Rows[0]["Email_Body"].ToString();
                    BODY = BODY.Replace("[PROJECTID]", Session["PROJECTIDTEXT"].ToString());
                    BODY = BODY.Replace("[GENERATEREQFOR]", GENERATEREQFOR);
                    BODY = BODY.Replace("[NEWEXPDATE]", NEWEXPDATE);
                    BODY = BODY.Replace("[STATUS]", STATUS);
                    BODY = BODY.Replace("[USERNAME]", Session["User_Name"].ToString());
                    BODY = BODY.Replace("[DATETIME]", cf.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy HH:mm"));
                    BODY = BODY.Replace("[TIMEZONE]", Session["TimeZone_Value"].ToString());
                }

                cf.Email_Users(
                EmailAddress: EMAILIDS,
                CCEmailAddress: CCEMAILIDS,
                BCCEmailAddress: BCCEMAILIDS,
                subject: SUBJECT,
                body: BODY
                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
  
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
  
    }
}