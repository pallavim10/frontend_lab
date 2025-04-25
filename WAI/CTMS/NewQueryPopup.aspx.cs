using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class NewQueryPopup : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_MM = new DAL_MM();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdnPVID.Value = Request.QueryString["PVID"].ToString();
                hdnRECID.Value = Request.QueryString["RECID"].ToString();
                lblSUBJID.Text = Request.QueryString["Subject"].ToString();
                lblLISTING.Text = Request.QueryString["LISTINGNAME"].ToString();
                hdnLISTING_ID.Value = Request.QueryString["LISTING_ID"].ToString();

                if (Request.QueryString["INVID"] != null)
                {
                    lblSITEID.Text = Request.QueryString["INVID"];
                }
                else
                {
                    DataSet ds = dal_MM.MM_QUERY_SP(ACTION: "GET_INVID_BY_SUBJID", SUBJID: lblSUBJID.Text);

                    lblSITEID.Text = ds.Tables[0].Rows[0][0].ToString();
                }

                if (!this.IsPostBack)
                {
                    if (Request.QueryString["PVID"] != null)
                    {
                        DataSet ds = dal_MM.MM_QUERY_SP(ACTION: "GET_MM_DM_QUERY", PVID: Request.QueryString["PVID"].ToString(), RECID: Request.QueryString["RECID"].ToString());

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Change", "opeDMQuery('" + Request.QueryString["PVID"].ToString() + "', " + Request.QueryString["RECID"].ToString() + ");", true);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void bntSave_Click(object sender, EventArgs e)
        {
            DAL dal;
            dal = new DAL();

            dal_MM.MM_QUERY_SP(
                ACTION: "RAISE_MANUAL_QUERY",
                SITEID: lblSITEID.Text,
                SUBJID: lblSUBJID.Text,
                SOURCE: lblLISTING.Text,
                PVID: hdnPVID.Value,
                RECID: hdnRECID.Value,
                LISTING_ID: hdnLISTING_ID.Value,
                QUERYTEXT: txtQueryDetail.Text
                );

            Response.Write("<script> alert('Query Raised successfully.'); </script>");
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }


    }
}