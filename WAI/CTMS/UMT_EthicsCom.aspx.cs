using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;

namespace CTMS
{
    public partial class UMT_EthicsCom : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FILL_SITE_ID();
                    GET_ETHICS_COMM();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void FILL_SITE_ID()
        {
            try
            {
                DataSet ds = dal.UMT_ETHICS_COMM_SP(
                    ACTION: "GET_SITE_ID"
                    );

                drpSiteId.DataSource = ds.Tables[0];
                drpSiteId.DataValueField = "SiteID";
                drpSiteId.DataTextField = "SiteID";
                drpSiteId.DataBind();
                drpSiteId.Items.Insert(0, new ListItem("--Select Site Id--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }
        private void GET_ETHICS_COMM()
        {
            try
            {
                DataSet ds = dal.UMT_ETHICS_COMM_SP(
                    ACTION: "GET_ETHICS_COMM"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grEthicsCommittee.DataSource = ds.Tables[0];
                    grEthicsCommittee.DataBind();
                }
                else
                {
                    grEthicsCommittee.DataSource = null;
                    grEthicsCommittee.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }        
        private void INSERT_ETHICS_COMM()
        {
            try
            {

                if (txtName.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Name');</script>");
                    return;
                }

                if (txtEmailid.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Email Id');</script>");
                    return;
                }

                if (txtContactNo.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Contact No');</script>");
                    return;
                }

                if (txtAddress.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Address');</script>");
                    return;
                }

                DataSet da = dal.UMT_ETHICS_COMM_SP(
                                            ACTION: "INSERT_ETHICS_COMM",
                                            SiteID: drpSiteId.SelectedValue,
                                            Name: txtName.Text,
                                            EmailID: txtEmailid.Text,
                                            ContactNo: txtContactNo.Text,
                                            Address: txtAddress.Text,
                                            ENTEREDBY: Session["USER_ID"].ToString()
                                        );

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Ethics Committee Created Successfully')", true);
                GET_ETHICS_COMM();
                CLEAR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_ETHICS_COMM();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void CLEAR()
        {
            txtName.Text = "";
            txtName.Text = "";
            txtEmailid.Text = "";
            txtContactNo.Text = "";
            drpSiteId.SelectedIndex = 0;
            txtAddress.Text = "";
        }
        private void UPDATE_ETHICS_COMM()
        {
            try
            {
                if (txtName.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Name');</script>");
                    return;
                }

                if (txtEmailid.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Email Id');</script>");
                    return;
                }

                if (txtContactNo.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Contact No');</script>");
                    return;
                }

                if (txtAddress.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Address');</script>");
                    return;
                }

                DataSet ds = dal.UMT_ETHICS_COMM_SP(
                                     ACTION: "UPDATE_ETHICS_COMM",
                                     ID: ViewState["ID"].ToString(),
                                     SiteID: drpSiteId.SelectedValue,
                                     Name: txtName.Text,
                                     EmailID: txtEmailid.Text,
                                     ContactNo: txtContactNo.Text,
                                     Address: txtAddress.Text,
                                     ENTEREDBY: Session["USER_ID"].ToString()
                                  );
                Response.Write("<script> alert('Ethics Committee Updated Successfully'); window.location.href = 'UMT_EthicsCom.aspx';</script>");
                GET_ETHICS_COMM();
                CLEAR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_ETHICS_COMM();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                lbtnSubmit.Visible = true;
                lbnUpdate.Visible = false;
                CLEAR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }
        protected void grEthicsCommittee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;
                if (e.CommandName == "EIDIT")
                {
                    EDIT_ETHICS_COMM(ID);
                    lbtnSubmit.Visible = false;
                    lbnUpdate.Visible = true;
                }
                else if (e.CommandName == "DELETED")
                {
                    DELETE_ETHICS_COMM(ID);
                    GET_ETHICS_COMM();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void EDIT_ETHICS_COMM(string ID)
        {
            try
            {
                DataSet ds = dal.UMT_ETHICS_COMM_SP(
                               ACTION: "EDIT_ETHICS_COMM",
                               ID: ID
                           );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {

                    drpSiteId.SelectedValue= ds.Tables[0].Rows[0]["SiteID"].ToString();
                    txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txtEmailid.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                    txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                else
                {
                    grEthicsCommittee.DataSource = null;
                    grEthicsCommittee.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }
        private void DELETE_ETHICS_COMM(string ID)
        {
            try
            {
                DataSet ds = dal.UMT_ETHICS_COMM_SP(
                  ACTION: "DELETE_ETHICS_COMM", ENTEREDBY: Session["USER_ID"].ToString(),
                  ID: ID
                  );


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Ethics Committee deleted Successfully')", true);
                GET_ETHICS_COMM();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }        
        protected void grdUserDetails_PreRender(object sender, EventArgs e)
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
    }
}