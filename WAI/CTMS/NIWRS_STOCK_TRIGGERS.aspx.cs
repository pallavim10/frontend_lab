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
    public partial class NIWRS_STOCK_TRIGGERS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_QUES();
                    GET_REASON();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_QUES()
        {
            try
            {
                string QUESTION = "", QUECODE = "", ANS = "";

                QUESTION = "Number of Back-Up Kits are Allowed Without Sponsor Approval.";
                QUECODE = "BakKitWithoutApprov";
                ANS = txtReqBakWithoutApp.Text;

                dal_IWRS.IWRS_SET_OPTION_SP(
                    ACTION: "INSERT_QUES",
                    QUESTION: QUESTION,
                    QUECODE: QUECODE,
                    ANS: ANS,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );

                QUESTION = "Number of Back-Up Kits are Allowed With Sponsor Approval.";
                QUECODE = "BakKitWithApprov";
                ANS = txtReqBakWithApp.Text;

                dal_IWRS.IWRS_SET_OPTION_SP(
                  ACTION: "INSERT_QUES",
                  QUESTION: QUESTION,
                  QUECODE: QUECODE,
                  ANS: ANS,
                  ENTEREDBY: Session["USER_ID"].ToString()
                  );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_QUES()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_QUE_ANS", QUECODE: "BakKitWithoutApprov");

                txtReqBakWithoutApp.Text = "";
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtReqBakWithoutApp.Text = ds.Tables[0].Rows[0]["ANS"].ToString();
                    }
                }

                ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_QUE_ANS", QUECODE: "BakKitWithApprov");

                txtReqBakWithApp.Text = "";
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtReqBakWithApp.Text = ds.Tables[0].Rows[0]["ANS"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitQues_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_QUES();
                GET_QUES();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Back-Up Kit Limits Updated Successfully'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelQues_Click(object sender, EventArgs e)
        {
            try
            {
                GET_QUES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        //Define Reason for Back-Up Kit Starts

        private void CLEAR_REASON()
        {
            try
            {
                txtReasonSeqNo.Text = "";
                txtReason.Text = "";

                btnSubmitReason.Visible = true;
                btnUpdateReason.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_REASON()
        {
            try
            {
                dal_IWRS.IWRS_SET_OPTION_SP
                    (
                    ACTION: "INSERT_OPTION",
                    STRATA: "BakKit_REASON",
                    SEQNO: txtReasonSeqNo.Text,
                    ANS: txtReason.Text,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_REASON()
        {
            try
            {
                dal_IWRS.IWRS_SET_OPTION_SP
                    (
                    ACTION: "UPDATE_OPTION",
                    ID: ViewState["BakKit_REASON_ID"].ToString(),
                    STRATA: "BakKit_REASON",
                    SEQNO: txtReasonSeqNo.Text,
                    ANS: txtReason.Text,
                    ENTEREDBY: Session["USER_ID"].ToString()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_REASON(string ID)
        {
            try
            {
                dal_IWRS.NIWRS_SETUP_SP
                    (
                    ACTION: "DELETE_OPTION", ENTEREDBY: Session["USER_ID"].ToString(),
                    ID: ID
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void SELECT_REASON(string ID)
        {
            try
            {
                ViewState["BakKit_REASON_ID"] = ID;

                DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "SELECT_OPTION", ID: ID);

                txtReasonSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                txtReason.Text = ds.Tables[0].Rows[0]["TEXT"].ToString();

                btnSubmitReason.Visible = false;
                btnUpdateReason.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_REASON()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_OPTION", STRATA: "BakKit_REASON");
                gvReasons.DataSource = ds;
                gvReasons.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvReasons_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();

                if (e.CommandName == "EditReason")
                {
                    SELECT_REASON(id);
                }
                else if (e.CommandName == "DeleteReason")
                {
                    DELETE_REASON(id);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('defined Reasons for Back-Up Kit deleted successfully.'); ", true);
                    GET_REASON();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitReason_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_REASON();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('Reasons for Back-Up Kit defined successfully.'); ", true);
                GET_REASON();
                CLEAR_REASON();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateReason_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_REASON();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('defined Reasons for Back-Up Kit updated successfully.'); ", true);
                GET_REASON();
                CLEAR_REASON();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelReason_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_REASON();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        
    }
}