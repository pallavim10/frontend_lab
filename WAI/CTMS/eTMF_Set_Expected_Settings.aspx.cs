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
    public partial class eTMF_Set_Expected_Settings : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        CommonFunction.CommonFunction ComFun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    lblDocName.Text = Request.QueryString["DOCNAME"].ToString();
                    CHECKDIV();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void CHECKDIV()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GET_DOC_SETTING", DocID: Request.QueryString["DOCID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["SetEmail"].ToString() == "True")
                    {
                        divEmail.Visible = true;

                        if (ds.Tables[0].Rows[0]["E_UPLOAD"].ToString() == "True")
                        {
                            chkUpload.Checked = true;
                        }
                        else
                        {
                            chkUpload.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["E_DOWNLOAD"].ToString() == "True")
                        {
                            chkdownload.Checked = true;
                        }
                        else
                        {
                            chkdownload.Checked = false;
                        }
                    }

                    txtDateTitle.Text = ds.Tables[0].Rows[0]["Datetitle"].ToString();

                    if (ds.Tables[0].Rows[0]["VerSPEC"].ToString() == "True")
                    {
                        divSPEC.Visible = true;

                        txtSPECtitle.Text = ds.Tables[0].Rows[0]["SPECtitle"].ToString();
                        GetSPEC();
                    }
                    else
                    {
                        txtSPECtitle.Text = "";
                    }

                    txtExpComment.Text = ds.Tables[0].Rows[0]["Comment"].ToString();
                    txtInstruction.Text = ds.Tables[0].Rows[0]["Info"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetSPEC()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GET_DRPDOWNDATA",
                Comment: txtText.Text,
                DocID: Request.QueryString["DOCID"].ToString()
                );

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

        protected void btnsubmitSPEC_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "INSERT_DRPDOWNDATA",
                Comment: txtText.Text,
                SEQNO: txtSeqNo.Text,
                DocID: Request.QueryString["DOCID"].ToString()
                );

                txtText.Text = "";
                txtSeqNo.Text = "";
                GetSPEC();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnupdateSPEC_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "UPDATE_DRPDOWNDATA_BYID",
                Comment: txtText.Text,
                SEQNO: txtSeqNo.Text,
                ID: Session["eTMFVarID"].ToString()
                );

                txtText.Text = "";
                txtSeqNo.Text = "";
                btnsubmitSPEC.Visible = true;
                btnupdateSPEC.Visible = false;
                GetSPEC();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancelSPEC_Click(object sender, EventArgs e)
        {
            txtSeqNo.Text = "";
            txtText.Text = "";
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
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "GET_DRPDOWNDATA_BYID", ID: Session["eTMFVarID"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtText.Text = ds.Tables[0].Rows[0]["TEXT"].ToString();
                        txtSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                        btnupdateSPEC.Visible = true;
                        btnsubmitSPEC.Visible = false;
                    }
                }
                else if (e.CommandName == "DeleteModule")
                {
                    DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "DELETE_DRPDOWNDATA_BYID", ID: Session["eTMFVarID"].ToString());
                    GetSPEC();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitComm_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "SetExpDoc_Comment",
                ID: Request.QueryString["DOCID"].ToString(),
                Comment: txtExpComment.Text
                );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnSubmitSPECtitle_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "SetSPECtitle",
                 ID: Request.QueryString["DOCID"].ToString(),
                 SPECtitle: txtSPECtitle.Text
                 );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnSubmitDateTitle_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "SetDatetitle",
                ID: Request.QueryString["DOCID"].ToString(),
                DateTitle: txtDateTitle.Text
                );


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnsubmitInst_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "SetDocInstruction",
                 ID: Request.QueryString["DOCID"].ToString(),
                 Comment: txtInstruction.Text
                 );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnSubmitEmail_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_SET_SP(ACTION: "SetEmailFor_Upload_Download",
                ID: Request.QueryString["DOCID"].ToString(),
                E_Download: chkUpload.Checked,
                E_Upload: chkUpload.Checked
                );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

    }
}