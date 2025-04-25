using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;

namespace CTMS
{
    public partial class NIWRS_ALLOC_RANDNO : System.Web.UI.Page
    {
        DAL dal = new DAL();

        DAL_IWRS dal_IWRS = new DAL_IWRS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SITEIDs();
                    GET_RAND_POOL_BLOCK();
                    GET_RAND_POOL();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_RAND_POOL()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UPLOAD_SP(ACTION: "GET_RAND_POOL", Value1: drpFrom.SelectedValue, Value2: drpTo.SelectedValue);

                gvRands.DataSource = ds;
                gvRands.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_RAND_POOL_BLOCK()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UPLOAD_SP(ACTION: "GET_RAND_POOL_BLOCK");

                drpFrom.DataSource = ds.Tables[0];
                drpFrom.DataValueField = "BLOCK";
                drpFrom.DataBind();
                drpFrom.Items.Insert(0, new ListItem("--All--", "0"));


                drpTo.DataSource = ds.Tables[0];
                drpTo.DataValueField = "BLOCK";
                drpTo.DataBind();
                drpTo.Items.Insert(0, new ListItem("--All--", "0"));


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITEIDs()
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
                        drpSITEID.DataSource = ds.Tables[0];
                        drpSITEID.DataValueField = "INVNAME";
                        drpSITEID.DataBind();
                    }
                    else
                    {
                        drpSITEID.DataSource = ds.Tables[0];
                        drpSITEID.DataValueField = "INVNAME";
                        drpSITEID.DataBind();
                        drpSITEID.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                }
                else
                {
                    drpSITEID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAllocate_Click(object sender, EventArgs e)
        {
            try
            {
                dal_IWRS.IWRS_UPLOAD_SP(
                    ACTION: "ALLOCATE_RAND_NUMBERS",
                    SITEID: drpSITEID.SelectedValue,
                    Value1: drpFrom.SelectedValue,
                    Value2: drpTo.SelectedValue
                    );


                GET_RAND_POOL();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_RAND_POOL();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_RAND_POOL();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}