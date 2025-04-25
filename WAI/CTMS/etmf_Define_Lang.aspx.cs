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
    public partial class etmf_Define_Lang : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    txtVariable.Text = Request.QueryString["VARIABLENAME"].ToString();
                    Getdata();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        private void Getdata()
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "GET_DRPDOWNDATA",
                NOTE: txtVariable.Text
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtVariable.Text = ds.Tables[0].Rows[0]["VARIABLE_NAME"].ToString();
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdData.DataSource = ds.Tables[0];
                    grdData.DataBind();
                }
                else
                {
                    grdData.DataSource = null;
                    grdData.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "INSERT_DRPDOWNDATA",
                NOTE: txtVariable.Text,
                DocName: txtText.Text,
                SEQNO: txtSeqNo.Text
                );

                txtText.Text = "";
                txtSeqNo.Text = "";
                Getdata();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.eTMF_SP(ACTION: "UPDATE_DRPDOWNDATA_BYID",
                NOTE: txtVariable.Text,
                DocName: txtText.Text,
                SEQNO: txtSeqNo.Text,
                ID: Session["eTMFVarID"].ToString()
                );

                txtText.Text = "";
                txtSeqNo.Text = "";
                btnsubmit.Visible = true;
                btnupdate.Visible = false;
                Getdata();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();
                Session["eTMFVarID"] = id;
                if (e.CommandName == "EditModule")
                {
                    DataSet ds = dal.eTMF_SP(ACTION: "GET_DRPDOWNDATA_BYID", ID: Session["eTMFVarID"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtText.Text = ds.Tables[0].Rows[0]["TEXT"].ToString();
                        txtSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                        btnupdate.Visible = true;
                        btnsubmit.Visible = false;
                    }
                }
                else if (e.CommandName == "DeleteModule")
                {
                    DataSet ds = dal.eTMF_SP(ACTION: "DELETE_DRPDOWNDATA_BYID", ID: Session["eTMFVarID"].ToString());
                    Getdata();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}