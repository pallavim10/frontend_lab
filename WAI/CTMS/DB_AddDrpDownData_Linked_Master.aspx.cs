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
    public partial class DB_AddDrpDownData_Linked_Master : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    lblVariable.Text = Request.QueryString["VARIABLENAME"].ToString();

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
                DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "GET_DRPDOWNDATA_LINKED",
                    ID: Request.QueryString["ID"].ToString(),
                    VARIABLENAME: Request.QueryString["VARIABLENAME"].ToString()
                    );

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblField.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                        lblVariable.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();

                        DataSet dsField = dal_DB.DB_MASTER_SP(ACTION: "GET_ParentLINKED_Fields",
                            MODULENAME: ds.Tables[0].Rows[0]["MODULENAME"].ToString()
                            );

                        ddlParentField.DataSource = dsField;
                        ddlParentField.DataTextField = "FIELDNAME";
                        ddlParentField.DataValueField = "ID";
                        ddlParentField.DataBind();
                        ddlParentField.Items.Insert(0, new ListItem("--Select--", "0"));
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        ddlParentField.SelectedValue = ds.Tables[1].Rows[0]["ParentField"].ToString();
                        ddlParentField.Enabled = false;

                        GET_PARENT_ANS();

                        grdData.DataSource = ds.Tables[1];
                        grdData.DataBind();
                    }
                    else
                    {
                        ddlParentField.Enabled = true;

                        grdData.DataSource = null;
                        grdData.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_MASTER_SP(
                ACTION: "INSERT_DRPDOWNDATA_LINKED",
                VARIABLENAME: lblVariable.Text,
                OPTIONVALUE: txtText.Text,
                SEQNO: txtSeqNo.Text,
                CONTROLTYPE: Request.QueryString["CONTROL"].ToString(),
                ParentField: ddlParentField.SelectedValue,
                ParentANS: ddlParentANS.SelectedValue
                );

                txtText.Text = "";
                txtSeqNo.Text = "";
                ddlParentANS.SelectedIndex = 0;

                Response.Write("<script> alert('Field Option added successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
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
                DataSet ds = dal_DB.DB_MASTER_SP(
                ACTION: "UPDATE_DRPDOWNDATA_BYID_LINKED",
                VARIABLENAME: lblVariable.Text,
                OPTIONVALUE: txtText.Text,
                SEQNO: txtSeqNo.Text,
                ID: Session["ID"].ToString(),
                ParentField: ddlParentField.SelectedValue,
                ParentANS: ddlParentANS.SelectedValue
                );

                txtText.Text = "";
                txtSeqNo.Text = "";
                ddlParentANS.SelectedIndex = 0;
                btnsubmit.Visible = true;
                btnupdate.Visible = false;

                Response.Write("<script> alert('Field Option updated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
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
                string id = e.CommandArgument.ToString();
                Session["ID"] = id;

                if (e.CommandName == "EditModule")
                {
                    DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "GET_DRPDOWNDATA_BYID", ID: Session["ID"].ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlParentANS.SelectedValue = ds.Tables[0].Rows[0]["ParentANS"].ToString();
                        txtText.Text = ds.Tables[0].Rows[0]["TEXT"].ToString();
                        txtSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

                        btnupdate.Visible = true;
                        btnsubmit.Visible = false;
                    }
                }
                else if (e.CommandName == "DeleteModule")
                {
                    DataSet ds = dal_DB.DB_MASTER_SP(ACTION: "DELETE_DRPDOWNDATA_BYID", ID: Session["ID"].ToString());

                    Getdata();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlParentField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_PARENT_ANS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_PARENT_ANS()
        {
            try
            {
                DataSet dsANS = dal_DB.DB_MASTER_SP(ACTION: "GET_ParentLINKED_Fields_ANS", ID: ddlParentField.SelectedValue);

                ddlParentANS.DataSource = dsANS;
                ddlParentANS.DataTextField = "VALUE";
                ddlParentANS.DataValueField = "VALUE";
                ddlParentANS.DataBind();
                ddlParentANS.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdData_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }
    }
}