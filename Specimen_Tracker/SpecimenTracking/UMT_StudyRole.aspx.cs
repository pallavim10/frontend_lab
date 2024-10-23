using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class UMT_StudyRole : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_STUDYROLE_USER();
                }
            }
            catch (Exception ex)
            {
                 ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void GET_STUDYROLE_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_STUDYROLE_SP(
                    ACTION: "GET_STUDYROLE_USER"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdStudyRoles.DataSource = ds.Tables[0];
                    grdStudyRoles.DataBind();
                }
                else
                {
                    grdStudyRoles.DataSource = null;
                    grdStudyRoles.DataBind();
                }

            }
            catch (Exception ex)
            {
                 ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void INSERT_STUDYROLE_USER()
        {
            try
            {
                if (txtStudyRole.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Please Enter Study Role.', 'warning');", true);
                    
                }
                if (chkInternal.Checked || chkSite.Checked || chkSponsor.Checked || chkExternal.Checked)
                {
                    if(txtStudyRole.Text.Trim() != "")
                    {
                        DataSet da = dal_UMT.UMT_STUDYROLE_SP(
                                            ACTION: "INSERT_STUDYROLE_USER",
                                            StudyRole: txtStudyRole.Text,
                                            Internal: chkInternal.Checked,
                                            Site: chkSite.Checked,
                                            Sponsor: chkSponsor.Checked,
                                            OtherExternal: chkExternal.Checked
                                        );

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','Study role  created successfully.', 'success');", true);

                        GET_STUDYROLE_USER();
                        CLEAR();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Please select at least one Applicable For.', 'warning');", true);
                }
            }
            catch (Exception ex)
            {
                 ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void CLEAR()
        {
            txtStudyRole.Text = "";
            chkInternal.Checked = false;
            chkSite.Checked = false;
            chkSponsor.Checked = false;
            chkExternal.Checked = false;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_STUDYROLE_USER();
            }
            catch (Exception ex)
            {
                 ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void UPDATE_STUDYROLE_USER()
        {
            try
            {
                if (txtStudyRole.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!', 'Please Enter Study Role.', 'warning');", true);
                    
                }

                if (chkInternal.Checked || chkSite.Checked || chkSponsor.Checked || chkExternal.Checked)
                {
                    if (txtStudyRole.Text.Trim() != "")
                    {
                        DataSet da = dal_UMT.UMT_STUDYROLE_SP(
                                            ACTION: "UPDATE_STUDYROLE_USER",
                                            ID: ViewState["ID"].ToString(),
                                            StudyRole: txtStudyRole.Text,
                                            Internal: chkInternal.Checked,
                                            Site: chkSite.Checked,
                                            Sponsor: chkSponsor.Checked,
                                            OtherExternal: chkExternal.Checked
                                        );

                        GET_STUDYROLE_USER();
                        CLEAR();


                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','Study Role Updated Successfully', 'success');", true);
                    }
                }
                else
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Warning!','Please select at least one Applicable For.', 'warning');", true);
                }

            }
            catch (Exception ex)
            {
                 ExceptionLogging.SendErrorToText(ex);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_STUDYROLE_USER();
            }
            catch (Exception ex)
            {
                 ExceptionLogging.SendErrorToText(ex);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            CLEAR();
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
                 ExceptionLogging.SendErrorToText(ex);
                throw;
            }
        }
        protected void grdStudyRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;
                if (e.CommandName == "EditStudy")
                {
                    EDIT_STUDYROLE_USER(ID);
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (e.CommandName == "DeleteStudyRole")
                {
                    DELETE_STUDYROLE_USER(ID);
                    GET_STUDYROLE_USER();
                }
            }
            catch (Exception ex)
            {
                 ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void DELETE_STUDYROLE_USER(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_STUDYROLE_SP(
                  ACTION: "DELETE_STUDYROLE_USER",
                  ID: ID
                  );

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success!','Study Role deleted Successfully.', 'success');", true);
                GET_STUDYROLE_USER();
            }
            catch (Exception ex)
            {
                 ExceptionLogging.SendErrorToText(ex);
            }
        }
        private void EDIT_STUDYROLE_USER(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_STUDYROLE_SP(
                               ACTION: "EDIT_STUDYROLE_USER",
                               ID: ID
                           );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {

                    txtStudyRole.Text = ds.Tables[0].Rows[0]["StudyRole"].ToString();

                    if (ds.Tables[0].Rows[0]["Internal"].ToString() == "True")
                    {
                        chkInternal.Checked = true;
                    }
                    else
                    {
                        chkInternal.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["Site"].ToString() == "True")
                    {
                        chkSite.Checked = true;
                    }
                    else
                    {
                        chkSite.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["Sponsor"].ToString() == "True")
                    {
                        chkSponsor.Checked = true;
                    }
                    else
                    {
                        chkSponsor.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["OtherExternal"].ToString() == "True")
                    {
                        chkExternal.Checked = true;
                    }
                    else
                    {
                        chkExternal.Checked = false;
                    }

                }
                else
                {
                    grdStudyRoles.DataSource = null;
                    grdStudyRoles.DataBind();
                }
            }
            catch (Exception ex)
            {
                 ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbStudyRoleExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Study Role Details";

                DataSet ds = dal_UMT.UMT_LOG_SP(
                   ACTION: "GET_STUDYROLE_USER"
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

        protected void grdStudyRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    HtmlControl iconInternal = (HtmlControl)e.Row.FindControl("iconInternal");
                    string StudyRole = ((Label)e.Row.FindControl("lblStudyRole")).Text;
                    LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");


                    DataSet ds = dal_UMT.UMT_STUDYROLE_SP(ACTION: "CHECK_STUDYROLE", StudyRole: StudyRole);
                    string COUNT = ds.Tables[0].Rows[0]["Count"].ToString();
                    if (Convert.ToInt32(COUNT) > 0)
                    {
                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }


                    if (drv["Internal"].ToString() == "True")
                    {
                        iconInternal.Attributes.Add("class", "fa fa-check");
                        iconInternal.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconInternal.Attributes.Add("class", "fa fa-times");
                        iconInternal.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconSite = (HtmlControl)e.Row.FindControl("iconSite");
                    if (drv["Site"].ToString() == "True")
                    {
                        iconSite.Attributes.Add("class", "fa fa-check");
                        iconSite.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSite.Attributes.Add("class", "fa fa-times");
                        iconSite.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconSponsor = (HtmlControl)e.Row.FindControl("iconSponsor");
                    if (drv["Sponsor"].ToString() == "True")
                    {
                        iconSponsor.Attributes.Add("class", "fa fa-check");
                        iconSponsor.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSponsor.Attributes.Add("class", "fa fa-times");
                        iconSponsor.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconExternal = (HtmlControl)e.Row.FindControl("iconExternal");
                    if (drv["OtherExternal"].ToString() == "True")
                    {
                        iconExternal.Attributes.Add("class", "fa fa-check");
                        iconExternal.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconExternal.Attributes.Add("class", "fa fa-times");
                        iconExternal.Attributes.Add("style", "color: red;");
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