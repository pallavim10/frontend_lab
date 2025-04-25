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
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class NSAE_SETUP_TYPE : System.Web.UI.Page
    {
        DAL_SAE dal_SAE = new DAL_SAE();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_MODULES();
                    GET_GRD_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MODULES()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                          ACTION: "GET_MODULES"
                          );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpModule.DataSource = ds.Tables[0];
                    drpModule.DataValueField = "MODULEID";
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpModule.DataSource = null;
                    drpModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_FIELD()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                    ACTION: "GET_FILED",
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpEventId.DataSource = ds.Tables[0];
                    drpEventId.DataValueField = "ID";
                    drpEventId.DataTextField = "FIELDNAME";
                    drpEventId.DataBind();
                    drpEventId.Items.Insert(0, new ListItem("--Select--", "0"));


                    drpEventTerm.DataSource = ds.Tables[0];
                    drpEventTerm.DataValueField = "ID";
                    drpEventTerm.DataTextField = "FIELDNAME";
                    drpEventTerm.DataBind();
                    drpEventTerm.Items.Insert(0, new ListItem("--Select--", "0"));                    
                }
                else
                {
                    drpModule.DataSource = null;
                    drpModule.DataBind();
                }

                DataSet ds1 = dal_SAE.SAE_SETUP_SP(
                    ACTION: "GET_FILED_SETUP_TYPE"
                    );

                if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables.Count > 0)
                {
                    drpAwarenes.DataSource = ds1.Tables[0];
                    drpAwarenes.DataValueField = "ID";
                    drpAwarenes.DataTextField = "FIELDNAME";
                    drpAwarenes.DataBind();
                    drpAwarenes.Items.Insert(0, new ListItem("--Select--", "0"));

                    drpAwarenestime.DataSource = ds1.Tables[0];
                    drpAwarenestime.DataValueField = "ID";
                    drpAwarenestime.DataTextField = "FIELDNAME";
                    drpAwarenestime.DataBind();
                    drpAwarenestime.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //private void GET_FIELD()
        //{
        //    try
        //    {
        //        DataSet ds = dal_SAE.SAE_SETUP_SP(ACTION: "GET_FILED", MODULEID: drpModule.SelectedValue);

        //        BIND_FIELDS(drpEventId, ds);
        //        BIND_FIELDS(drpEventTerm, ds);
        //        BIND_FIELDS(drpDeley, ds);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.ToString();
        //    }
        //}

        private void BIND_FIELDS(DropDownList ddl, DataSet ds)
        {
            try
            {
                ddl.DataSource = ds.Tables[0];
                ddl.DataValueField = "ID";
                ddl.DataTextField = "FIELDNAME";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_GRD_DATA()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                    ACTION: "GET_GRD_DATA"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdSetup.DataSource = ds.Tables[0];
                    grdSetup.DataBind();
                }
                else
                {
                    grdSetup.DataSource = null;
                    grdSetup.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                    ACTION: "GET_SAE_SETUP_BY_MODULE",
                    MODULEID: drpModule.SelectedValue
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GET_MODULES();
                    drpModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();

                    GET_FIELD();
                    drpEventId.SelectedValue = ds.Tables[0].Rows[0]["SPID_FIELDID"].ToString();
                    drpEventTerm.SelectedValue = ds.Tables[0].Rows[0]["TERM_FIELDID"].ToString();
                    drpAwarenes.SelectedValue = ds.Tables[0].Rows[0]["DELAY_FIELDID"].ToString();
                    drpAwarenestime.SelectedValue = ds.Tables[0].Rows[0]["DELAY_TIME_FIELDID"].ToString();

                    lbtnSubmit.Visible = false;
                    lbnUpdate.Visible = true;
                }
                else
                {
                    GET_FIELD();
                    lbtnSubmit.Visible = true;
                    lbnUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_MODULE()
        {
            try
            {
                DataSet da = dal_SAE.SAE_SETUP_SP(
                    ACTION: "INSERT_MODULE",
                    MODULEID: drpModule.SelectedValue,
                    SPID_FIELDID: drpEventId.SelectedValue,
                    TERM_FIELDID: drpEventTerm.SelectedValue,
                    DELAY_FIELDID: drpAwarenes.SelectedValue,
                    TIME_FIELDID: drpAwarenestime.SelectedValue
                    );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_MODULE()
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                    ACTION: "INSERT_MODULE",
                    MODULEID: drpModule.SelectedValue,
                    SPID_FIELDID: drpEventId.SelectedValue,
                    TERM_FIELDID: drpEventTerm.SelectedValue,
                    DELAY_FIELDID: drpAwarenes.SelectedValue,
                    TIME_FIELDID: drpAwarenestime.SelectedValue
                    );
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
                INSERT_MODULE();

                Response.Write("<script> alert('SAE Setup inserted Successfully'); window.location.href = 'NSAE_SETUP_TYPE.aspx';</script>");
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
                UPDATE_MODULE();

                Response.Write("<script> alert('SAE Setup updated Successfully'); window.location.href = 'NSAE_SETUP_TYPE.aspx';</script>");
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
                Response.Redirect(Request.Url.ToString(), false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void grdSetup_PreRender(object sender, EventArgs e)
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

        protected void grdSetup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;

                if (e.CommandName == "EIDIT")
                {
                    EDIT_MODULE(ID);
                    GET_GRD_DATA();
                    lbtnSubmit.Visible = false;
                    lbnUpdate.Visible = true;
                }

                else if (e.CommandName == "DELETED")
                {
                    DELETE_MODULE(ID);

                    Response.Write("<script> alert('Module deleted Successfully'); window.location.href = 'NSAE_SETUP_TYPE.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT_MODULE(string ID)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                               ACTION: "EDIT_MODULE",
                               ID: ID
                           );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GET_MODULES();
                    drpModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();

                    GET_FIELD();
                    drpEventId.SelectedValue = ds.Tables[0].Rows[0]["SPID_FIELDID"].ToString();
                    drpEventTerm.SelectedValue = ds.Tables[0].Rows[0]["TERM_FIELDID"].ToString();
                    drpAwarenes.SelectedValue = ds.Tables[0].Rows[0]["DELAY_FIELDID"].ToString();
                    drpAwarenestime.SelectedValue = ds.Tables[0].Rows[0]["DELAY_TIME_FIELDID"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_MODULE(string ID)
        {
            try
            {
                DataSet ds = dal_SAE.SAE_SETUP_SP(
                  ACTION: "DELETE_MODULE",
                  ID: ID
                  );

                GET_GRD_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR()
        {
            Response.Redirect(Request.RawUrl.ToString());
        }

        protected void grdSetup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["COUNT"].ToString();
                    LinkButton lbtdeleteModule = (e.Row.FindControl("lbtdeleteModule") as LinkButton);
                    if (Convert.ToInt32(id) > 0)
                    {
                        lbtdeleteModule.Visible = false;
                    }
                    else
                    {
                        lbtdeleteModule.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}