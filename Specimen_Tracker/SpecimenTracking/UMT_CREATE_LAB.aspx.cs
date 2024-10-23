using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class UMT_CREATE_LAB : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_LABS();
                    
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_LABS()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_LAB_SP(
                    ACTION: "GET_LABS"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    GrdLabs.DataSource = ds.Tables[0];
                    GrdLabs.DataBind();
                }
                else
                {
                    GrdLabs.DataSource = null;
                    GrdLabs.DataBind();
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_CLEAR()
        {
            try
            {
                txtLabName.Text = "";
                lbtnSubmit.Visible = true;
                lbtnUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            INSERT_LAB();
            GET_LABS();
            GET_LABS();
        }
        private void INSERT_LAB()
        {
            try
            {
                if(txtLabName.Text.Trim() != "")
                {
                    DataSet ds = dal_UMT.UMT_LAB_SP(ACTION: "INSERT_LAB", LABNAME: txtLabName.Text);

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                        swal({
                            title: 'Success!',
                            text: 'Lab Define Successfully.',
                            icon: 'success',
                            button: 'OK'
                        }).then(function(){
                                         window.location.href = window.location.href; });
                        ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                        swal({
                            title: 'Success!',
                            text: 'Please Define Lab.',
                            icon: 'success',
                            button: 'OK'
                        }).then(function(){
                                         window.location.href = window.location.href; });
                        ", true);
                }
                
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void UPDATE_LAB()
        {
            try
            {
                if (txtLabName.Text.Trim() != "")
                {
                    DataSet ds = dal_UMT.UMT_LAB_SP(
                        ACTION: "UPDATE_LAB",
                        LABNAME: txtLabName.Text,
                        ID: ViewState["ID"].ToString());

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                        swal({
                            title: 'Success!',
                            text: 'Defined Lab Updated Successfully.',
                            icon: 'success',
                            button: 'OK'
                        }).then(function(){
                                         window.location.href = window.location.href; });
                        ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                        swal({
                            title: 'Success!',
                            text: 'Please Define Lab.',
                            icon: 'success',
                            button: 'OK'
                        }).then(function(){
                                         window.location.href = window.location.href; });
                        ", true);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void EDIT_LAB(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_LAB_SP(
                               ACTION: "EDIT_LAB",
                               ID: ID
                           );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtLabName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    
                }
                else
                {
                    GrdLabs.DataSource = null;
                    GrdLabs.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void DELETE_LAB(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_LAB_SP(
                  ACTION: "DELETE_LAB",
                  ID: ID
                  );
                string script = @"
                     swal({
                     title: 'Success!',
                     text: 'Defined Lab deleted Successfully.',
                     icon: 'success',
                     button: 'OK'
                     }).then((value) => {
                       window.location.href = window.location.href; });
                     });";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);

                GET_LABS();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            UPDATE_LAB();
            GET_LABS();
            GET_CLEAR();

        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            GET_CLEAR();
        }

        protected void GrdLabs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            ViewState["ID"] = ID;
            if (e.CommandName == "EditLAB")
            {
                EDIT_LAB(ID);
                lbtnSubmit.Visible = false;
                lbtnUpdate.Visible = true;
            }
            else if (e.CommandName == "DeleteLAB")
            {
                DELETE_LAB(ID);
                GET_LABS();
            }
        }

        protected void GrdLabs_PreRender(object sender, EventArgs e)
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
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Define Labs";

                DataSet ds = dal_UMT.UMT_REPORT_SP(
                   ACTION: "EXPORT_LABS"
                   );
                DataSet dsExport = new DataSet();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void GrdLabs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    string lblID = ((Label)e.Row.FindControl("lblID")).Text;
                    LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");


                    DataSet ds = dal_UMT.UMT_LAB_SP(ACTION: "CHECK_LAB", ID: lblID);
                    string COUNT = ds.Tables[0].Rows[0]["Count"].ToString();
                    if (Convert.ToInt32(COUNT) > 0)
                    {
                        btnDelete.Visible = false;
                    }
                    else
                    {
                        btnDelete.Visible = true;
                    }

                    
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
    }
}