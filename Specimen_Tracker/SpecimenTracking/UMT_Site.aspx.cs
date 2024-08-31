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
    public partial class UMT_Site : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FILL_COUNTRY();
                    GET_SITE();
                    GET_TIMEZONE();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private void FILL_COUNTRY()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(
                             ACTION: "GET_COUNTRY"
                            );

                ddlCountry.DataSource = ds.Tables[0];
                ddlCountry.DataValueField = "CNTRYID";
                ddlCountry.DataTextField = "COUNTRY";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FILL_STATE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FILL_CITY();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(
                    ACTION: "GET_SITE"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdSite.DataSource = ds.Tables[0];
                    grdSite.DataBind();
                }
                else
                {
                    grdSite.DataSource = null;
                    grdSite.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void INSERT_SITE()
        {
            try
            {
                if (txtSiteId.Text.Trim() == "")
                {                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Please Enter Site Id')", true);
                    return;
                }

                if (txtSiteName.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Please Enter Site Name')", true);
                    return;
                }

                if (txtEmailid.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Please Enter Email Id')", true);
                    return;
                }

                if (txtContactNo.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Please Enter Contact No')", true);
                    return;
                }

                if (txtAddress.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Please Enter Address')", true);
                    return;
                }

                DataSet da = dal_UMT.UMT_SITE_SP(
                                            ACTION: "INSERT_SITE"
                                            ,
                                            SiteID: txtSiteId.Text,
                                            SiteName: txtSiteName.Text,
                                            EmailID: txtEmailid.Text,
                                            ContactNo: txtContactNo.Text,
                                            CountryID: ddlCountry.SelectedValue,
                                            StateID: ddlstate.SelectedValue,
                                            CityID: ddlcity.SelectedValue,
                                            Timezone: ddlTimeZone.SelectedValue,
                                            Address: txtAddress.Text
                                        );
                string script = @"
                     swal({
                     title: 'Success!',
                     text: 'Site Created Successfully',
                     icon: 'success',
                     button: 'OK'                      
                     });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", script, true);
                GET_SITE();
                CLEAR();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CLEAR()
        {
            txtSiteId.Text = "";
            txtSiteName.Text = "";
            txtEmailid.Text = "";
            txtContactNo.Text = "";
            ddlCountry.SelectedIndex = 0;
            ddlstate.ClearSelection();
            ddlcity.SelectedIndex = 0;
            txtAddress.Text = "";
        }


        protected void GET_TIMEZONE()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(
                                        ACTION: "GETTIMEZONE"
                                        );

                ddlTimeZone.DataSource = ds;
                ddlTimeZone.DataValueField = "ID";
                ddlTimeZone.DataTextField = "TimeZone";
                ddlTimeZone.DataBind();
                ddlTimeZone.Items.Insert(0, new ListItem("--Select TimeZone--", "0"));
                ddlTimeZone.SelectedValue = "87";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void FILL_STATE()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal_UMT.UMT_SITE_SP(
                ACTION: "GET_STATE",
                CountryID: ddlCountry.SelectedValue
                );
                ddlstate.DataSource = ds.Tables[0];
                ddlstate.DataValueField = "ID";
                ddlstate.DataTextField = "StateName";
                ddlstate.DataBind();
                ddlstate.Items.Insert(0, new ListItem("--Select State--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }
        private void FILL_CITY()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(
                    ACTION: "GET_CITY",
                    StateID: ddlstate.SelectedValue
                    );
                ddlcity.DataSource = ds.Tables[0];
                ddlcity.DataValueField = "city_id";
                ddlcity.DataTextField = "city_name";
                ddlcity.DataBind();
                ddlcity.Items.Insert(0, new ListItem("--Select City--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

         protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_SITE("INSERT"))
                {
                    INSERT_SITE();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this,GetType(),"showalert", $"swal('Warning!','Site Id " + txtSiteId.Text + " is already available.')", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool Check_SITE(string ACTIONS)
        {
            bool res = true;
            try
            {
                DataSet ds = new DataSet();

                if (ACTIONS == "INSERT")
                {
                    ds = dal_UMT.UMT_SITE_SP(ACTION: "CHECK_SITEID_EXIST", SiteID: txtSiteId.Text);
                }
                else if (ACTIONS == "UPDATE")
                {
                    ds = dal_UMT.UMT_SITE_SP(ACTION: "CHECK_SITEID_EXIST", SiteID: txtSiteId.Text, ID: ViewState["ID"].ToString());
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return res;
        }

        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_SITE("UPDATE"))
                {
                    UPDATE_SITE();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Site Id " + txtSiteId.Text + " is already available.')", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UPDATE_SITE()
        {
            try
            {
                if (txtSiteId.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Please Enter Site Id')", true);
                    return;
                }

                if (txtSiteName.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Please Enter Site Name')", true);
                    return;
                }

                if (txtEmailid.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Please Enter Email Id')", true);
                    return;
                }

                if (txtContactNo.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Please Enter Contact No')", true);
                    return;
                }

                if (txtAddress.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!','Please Enter Address')", true);
                    return;
                }

                DataSet ds = dal_UMT.UMT_SITE_SP(
                                 ACTION: "UPDATE_SITE",
                                 ID: ViewState["ID"].ToString(),
                                 SiteID: txtSiteId.Text,
                                 SiteName: txtSiteName.Text,
                                 EmailID: txtEmailid.Text,
                                 ContactNo: txtContactNo.Text,
                                 CountryID: ddlCountry.SelectedValue,
                                 StateID: ddlstate.SelectedValue,
                                 CityID: ddlcity.SelectedValue,
                                 Timezone: ddlTimeZone.SelectedValue,
                                 Address: txtAddress.Text
                              );

                string script = @"
                     swal({
                     title: 'Success!',
                     text: 'Site Updated Successfully',
                     icon: 'success',
                     button: 'OK'
                     }).then((value) => {
                        window.location.href = 'UMT_Site.aspx'; 
                     });";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                GET_SITE();
                CLEAR();
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
        protected void grdSite_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                ViewState["ID"] = ID;
                if (e.CommandName == "EIDIT")
                {
                    EDIT_SITE(ID);
                    lbtnSubmit.Visible = false;
                    lbnUpdate.Visible = true;
                }
                else if (e.CommandName == "DELETED")
                {
                    DELETE_SITE(ID);
                    GET_SITE();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_SITE(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(
                  ACTION: "DELETE_SITE",
                  ID: ID
                  );
                string script = @"
                     swal({
                     title: 'Success!',
                     text: 'Site Deleted Successfully',
                     icon: 'success',
                     button: 'OK'                     
                     });";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                GET_SITE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EDIT_SITE(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_SITE_SP(
                               ACTION: "EDIT_SITE",
                               ID: ID
                           );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtSiteId.Text = ds.Tables[0].Rows[0]["SiteID"].ToString();
                    if (ds.Tables[0].Rows[0]["COUNTS"].ToString() != "0")
                    {
                        txtSiteId.Enabled = false;
                        lblSiteIDErr.Visible = true;
                    }
                    else
                    {
                        txtSiteId.Enabled = true;
                        lblSiteIDErr.Visible = false;
                    }
                    txtSiteName.Text = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    txtEmailid.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                    txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    FILL_COUNTRY();
                    ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["CountryID"].ToString();
                    FILL_STATE();
                    ddlstate.SelectedValue = ds.Tables[0].Rows[0]["StateID"].ToString();
                    FILL_CITY();
                    ddlcity.SelectedValue = ds.Tables[0].Rows[0]["CityID"].ToString();
                    ddlTimeZone.SelectedValue = ds.Tables[0].Rows[0]["Timezone"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                else
                {
                    grdSite.DataSource = null;
                    grdSite.DataBind();
                }
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

        protected void grdSite_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    LinkButton lbtdeleteuser = (e.Row.FindControl("lbtdeleteuser") as LinkButton);

                    if (drv["COUNTS"].ToString() != "0")
                    {
                        lbtdeleteuser.Visible = false;
                    }
                    else
                    {
                        lbtdeleteuser.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbSiteDetailsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Site Details";

                DataSet ds = dal_UMT.UMT_LOG_SP(
                     ACTION: "GET_SITE");
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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}