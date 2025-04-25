using CTMS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DM_AddDrpDownData_Visit_Linked : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtText.Attributes.Add("MaxLength", "10000");
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
                DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_DRPDOWNDATA_LINKED_VISIT",
                    ID: Request.QueryString["ID"].ToString(),
                    VISITNUM: Request.QueryString["VISITNUM"].ToString(),
                    VARIABLENAME: Request.QueryString["VARIABLENAME"].ToString()
                    );

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblVisit.Text = ds.Tables[0].Rows[0]["VISIT"].ToString();
                        lblField.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                        lblVariable.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();

                        DataSet dsField = dal_DB.DB_DM_SP(ACTION: "GET_ParentLINKED_Fields_VISIT",
                            MODULEID: ds.Tables[0].Rows[0]["MODULEID"].ToString(),
                            VISITNUM: Request.QueryString["VISITNUM"].ToString()
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
                DataSet ds = dal_DB.DB_DM_SP(
                ACTION: "INSERT_DRPDOWNDATA_LINKED_VISIT",
                VARIABLENAME: lblVariable.Text,
                DEFAULTVAL: txtText.Text,
                SEQNO: txtSeqNo.Text,
                CONTROLTYPE: Request.QueryString["CONTROL"].ToString(),
                ParentField: ddlParentField.SelectedValue,
                ParentANS: ddlParentANS.SelectedValue,
                VISITNUM: Request.QueryString["VISITNUM"].ToString()
                );

                txtText.Text = "";
                txtSeqNo.Text = "";
                ddlParentANS.SelectedIndex = 0;

                Getdata();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field option added successfully');", true);
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
                DataSet ds = dal_DB.DB_DM_SP(
                ACTION: "UPDATE_DRPDOWNDATA_BYID_LINKED_VISIT",
                VARIABLENAME: lblVariable.Text,
                DEFAULTVAL: txtText.Text,
                SEQNO: txtSeqNo.Text,
                ID: Session["ID"].ToString(),
                ParentField: ddlParentField.SelectedValue,
                ParentANS: ddlParentANS.SelectedValue,
                VISITNUM: Request.QueryString["VISITNUM"].ToString()
                );

                txtText.Text = "";
                txtSeqNo.Text = "";
                ddlParentANS.SelectedIndex = 0;
                btnsubmit.Visible = true;
                btnupdate.Visible = false;

                Getdata();

                Session.Remove("ID");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field option updated successfully');", true);
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

                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string txtTEXT = (gvr.FindControl("txtTEXT") as Label).Text;

                if (e.CommandName == "EditModule")
                {
                    DataSet ds = dal_DB.DB_DM_SP(ACTION: "GET_DRPDOWNDATA_BYID_VISIT", ID: Session["ID"].ToString());

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
                    dal_DB.DB_DM_SP(ACTION: "DELETE_DRPDOWNDATA_BYID_VISIT", ID: Session["ID"].ToString());

                    Getdata();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field option deleted successfully');", true);
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
                DataSet dsANS = dal_DB.DB_DM_SP(ACTION: "GET_ParentLINKED_Fields_ANS_VISIT",
                    ID: ddlParentField.SelectedValue,
                    VISITNUM: Request.QueryString["VISITNUM"].ToString()
                    );

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