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
    public partial class eTMF_DOC_QC_COMMENT : System.Web.UI.Page
    {
        DAL dal = new DAL();
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
                        GETDATA();
                    }
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
                DataSet ds = dal.eTMF_QC_SP(ACTION: "GET_FILENAME", ID: Request.QueryString["ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblfilename.Text = ds.Tables[0].Rows[0]["UploadFileName"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAction.SelectedValue == "Approved")
                {
                    if (hffirstClick.Value != "true")
                    {
                        hffirstClick.Value = "true";

                        if (ddlDocNom.SelectedValue == "No")
                        {
                            hfanyNo.Value = "true";
                            ddlDocNom.CssClass = "form-control required brd-1px-redimp";
                        }

                        if (ddlDocLegible.SelectedValue == "No")
                        {
                            hfanyNo.Value = "true";
                            ddlDocLegible.CssClass = "form-control required brd-1px-redimp";
                        }

                        if (ddlDocOrient.SelectedValue == "No")
                        {
                            hfanyNo.Value = "true";
                            ddlDocOrient.CssClass = "form-control required brd-1px-redimp";
                        }

                        if (ddlDocLocate.SelectedValue == "No")
                        {
                            hfanyNo.Value = "true";
                            ddlDocLocate.CssClass = "form-control required brd-1px-redimp";
                        }

                        if (ddlDocAttr.SelectedValue == "No")
                        {
                            hfanyNo.Value = "true";
                            ddlDocAttr.CssClass = "form-control required brd-1px-redimp";
                        }
                        if (ddlDocComplete.SelectedValue == "No")
                        {
                            hfanyNo.Value = "true";
                            ddlDocComplete.CssClass = "form-control required brd-1px-redimp";
                        }
                        
                    }
                    else
                    {
                        hfanyNo.Value = "false";
                    }
                }

                if (hfanyNo.Value != "true")
                {
                    DataSet ds = dal.eTMF_QC_SP(ACTION: "UPDATE_QC_DOC_COMMENT",
                        ID: Request.QueryString["ID"].ToString(),
                        UploadBy: Session["User_Id"].ToString(),
                        NOTE: txtReason.Text,
                        QCDOC_CORRECT_NOM: ddlDocNom.SelectedValue,
                        QCDoc_Legible: ddlDocLegible.SelectedValue,
                        QCDOC_CORRECT_ORIEN: ddlDocOrient.SelectedValue,
                        QCDoc_Locat: ddlDocLocate.SelectedValue,
                        QCDOC_DOC_ATTRI: ddlDocAttr.SelectedValue,
                        QCDOC_COMPLETE: ddlDocComplete.SelectedValue,
                        QC_ACTION: ddlAction.SelectedValue
                        ); ;

                    Response.Write("<script> alert('Document QCed as " + ddlAction.SelectedItem.Text + ".');window.close();</script>");
                }
                else
                {
                    txtReason.CssClass = "form-control required brd-1px-redimp";

                    string MESSAGE = "Comment is required if you have approved, even though one or more response is No.";

                    Response.Write("<script> alert('" + MESSAGE + "');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Write("<script>window.close();</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

    }
}