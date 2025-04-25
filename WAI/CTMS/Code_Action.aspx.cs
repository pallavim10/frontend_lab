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
    public partial class Code_Action : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {

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
                INSERT_APP_DISSAPPROVE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_APP_DISSAPPROVE()
        {
            try
            {
                if (Request.QueryString["AutoCodedLIB"].ToString() == "MedDRA")
                {
                    if (drpAction.SelectedValue == "Approve")
                    {
                        DataSet dt = dal.DB_CODE_SP(
                                         ACTION: "MedDRAData_APPROVAL",
                                             Approval: drpAction.SelectedValue,
                                             ApproveComm: txtComment.Text,
                                             ID: Request.QueryString["AUTO_ID"].ToString()
                                         );

                        Response.Write("<script> alert('Approve Successfully'); window.location.href = 'Code_Pending.aspx.';</script>");
                    }
                    else if (drpAction.SelectedValue == "Disapprove")
                    {
                        if (txtComment.Text.Trim() == "")
                        {
                            Response.Write("<script>alert('Please Enter Comment');</script>");
                        }
                        else
                        {
                            DataSet dt = dal.DB_CODE_SP(
                                        ACTION: "MedDRAData_APPROVAL",
                                            Approval: drpAction.SelectedValue,
                                            ApproveComm: txtComment.Text,
                                            ID: Request.QueryString["AUTO_ID"].ToString()
                                        );

                            Response.Write("<script> alert('Disapprove Successfully'); window.location.href = 'Code_Pending.aspx';</script>");
                        }

                    }
                }
                else if (Request.QueryString["AutoCodedLIB"].ToString() == "WHODD")
                {
                    if (drpAction.SelectedValue == "Approve")
                    {
                        DataSet dt = dal.DB_CODE_SP(
                                         ACTION: "WHODDataData_APPROVAL",
                                        Approval: drpAction.SelectedValue,
                                        ApproveComm: txtComment.Text,
                                        ID: Request.QueryString["AUTO_ID"].ToString()
                                         );

                        Response.Write("<script> alert('Approve Successfully'); window.location.href = 'Code_Pending.aspx.';</script>");
                    }
                    else if (drpAction.SelectedValue == "Disapprove")
                    {
                        if (txtComment.Text.Trim() == "")
                        {
                            Response.Write("<script>alert('Please Enter Comment');</script>");
                        }
                        else
                        {
                            DataSet dt = dal.DB_CODE_SP(
                                        ACTION: "WHODDataData_APPROVAL",
                                            Approval: drpAction.SelectedValue,
                                            ApproveComm: txtComment.Text,
                                            ID: Request.QueryString["AUTO_ID"].ToString()
                                        );

                            Response.Write("<script> alert('Disapprove Successfully'); window.location.href = 'Code_Pending.aspx';</script>");
                        }

                    }
                }


                //    if (drpAction.SelectedValue == "Approve")
                //{
                //    DataSet dt = dal.DB_CODE_SP(
                //                          ACTION: "MedDRAData_APPROVAL",
                //                              ENTEREDBY: Session["User_ID"].ToString(),
                //                              Approval: drpAction.SelectedValue,
                //                              ApproveComm: txtComment.Text,
                //                              ID: Request.QueryString["AUTO_ID"].ToString(),
                //                              RequestBy: Session["User_ID"].ToString()
                //                          );

                //    Response.Write("<script> alert('Approve Successfully'); window.location.href = 'Code_Pending.aspx.';</script>");
                //}
                //else if (drpAction.SelectedValue == "Disapprove")
                //{
                //    if (txtComment.Text.Trim() == "")
                //    {
                //        Response.Write("<script>alert('Please Enter Comment');</script>");
                //    }
                //    else
                //    {
                //        DataSet dt = dal.DB_CODE_SP(
                //                          ACTION: "WHODDataData_APPROVAL",
                //                              ENTEREDBY: Session["User_ID"].ToString(),
                //                              Approval: drpAction.SelectedValue,
                //                              ApproveComm: txtComment.Text,
                //                              ID: Request.QueryString["AUTO_ID"].ToString(),
                //                              RequestBy: Session["User_ID"].ToString()
                //                          );

                //        Response.Write("<script> alert('Disapprove Successfully'); window.location.href = 'Code_Pending.aspx';</script>");
                //    }

                //}

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            drpAction.SelectedIndex = 0;
            txtComment.Text = "";
        }
    }
}