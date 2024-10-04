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
    public partial class UMT_SponsorUser : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SPONSOR_NAME();
                    GET_USER();
                    BIND_STUDYROLE();
                    GET_TIMEZONE();
                    GET_SYSTEM("");
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void GET_SPONSOR_NAME()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                          ACTION: "GET_SPONSOR_NAME"
                          );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtCompany.Text = ds.Tables[0].Rows[0]["SPONSOR_NAME"].ToString();
                    txtCompany.ReadOnly = true;
                }
                else
                {
                    txtCompany.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void BIND_STUDYROLE()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                          ACTION: "GET_STUDYROLE_SPONSOR"
                          );
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpStudyRole.DataSource = ds.Tables[0];
                    drpStudyRole.DataTextField = "StudyRole";
                    drpStudyRole.DataValueField = "StudyRole";
                    drpStudyRole.DataBind();
                    drpStudyRole.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        protected void GET_TIMEZONE()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
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
                 ex.Message.ToString();
            }
        }

        private void GET_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                    ACTION: "GET_USER_SPONSOR"
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    grdUser.DataSource = ds.Tables[0];
                    grdUser.DataBind();
                }
                else
                {
                    grdUser.DataSource = null;
                    grdUser.DataBind();
                }

            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void GET_SYSTEM(string UserID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                    ACTION: "GET_SYSTEM_SPONSOR",
                    User_ID: UserID
                    );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    repeatSystem.DataSource = ds.Tables[0];
                    repeatSystem.DataBind();
                }
                else
                {
                    repeatSystem.DataSource = null;
                    repeatSystem.DataBind();
                }

            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void INSERT_SYSTEM()
        {
            try
            {
                for (int i = 0; i < repeatSystem.Items.Count; i++)
                {
                    CheckBox ChkSelect = (CheckBox)repeatSystem.Items[i].FindControl("ChkSelect");

                    Label lblSystemID = (Label)repeatSystem.Items[i].FindControl("lblSystemID");
                    Label lblSystemName = (Label)repeatSystem.Items[i].FindControl("lblSystemName");

                    TextBox txtSystemNotes = (TextBox)repeatSystem.Items[i].FindControl("txtSystemNotes");
                    txtSystemNotes.Attributes.Add("maxlength", "200");


                    string SubSytem = "";

                    if (ChkSelect.Checked)
                    {
                        DataSet ds = dal_UMT.UMT_USERS_SP(
                                        ACTION: "INSERT_SYSTEM",
                                        User_ID: hdnID.Value,
                                        SystemID: lblSystemID.Text,
                                        SystemName: lblSystemName.Text,
                                        SubSystem: SubSytem,
                                        NOTES: txtSystemNotes.Text
                                        );
                    }
                    else
                    {
                        DataSet ds = dal_UMT.UMT_USERS_SP(
                                           ACTION: "DELETE_SYSTEM",
                                           SystemID: lblSystemID.Text,
                                           SystemName: lblSystemName.Text,
                                           User_ID: hdnID.Value
                                           );
                    }
                }

            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void INSERT_USER_DATA()
        {
            DataSet da = dal_UMT.UMT_USERS_SP(
                                        ACTION: "INSERT_USER_SPONSOR",
                                        Company: txtCompany.Text,
                                        Fname: txtFirstName.Text,
                                        Lname: txtLastName.Text,
                                        EmailID: txtEmailid.Text,
                                        ContactNo: txtContactNo.Text,
                                        NOTES: txtNotes.Text,
                                        Blind: drpUnblind.SelectedValue,
                                        StudyRole: drpStudyRole.SelectedValue,
                                        Timezone: ddlTimeZone.SelectedValue
                                    );

            hdnID.Value = da.Tables[0].Rows[0]["USER_ID"].ToString();
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHECK_USER_EXISTS("INSERT"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'User already exists with same Name and Email ID.', 'warning');", true);
                }
                else
                {
                    INSERT_USER_DATA();
                    INSERT_SYSTEM();

                    string script = @"
                     swal({
                     title: 'Success!',
                     text: 'Sponsor User Created Successfully.',
                     icon: 'success',
                     button: 'OK'
                     }).then((value) => {
                        window.location.href = 'UMT_SponsorUser.aspx'; 
                     });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    GET_USER();
                    CLEAR();
                    
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void UPDATE_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                         ACTION: "UPDATE_USER_SPONSOR",
                         ID: ViewState["ID"].ToString(),
                         Company: txtCompany.Text,
                         Fname: txtFirstName.Text,
                         Lname: txtLastName.Text,
                         EmailID: txtEmailid.Text,
                         ContactNo: txtContactNo.Text,
                         NOTES: txtNotes.Text,
                         Blind: drpUnblind.SelectedValue,
                         StudyRole: drpStudyRole.Text,
                         Timezone: ddlTimeZone.SelectedValue
                        );

                hdnID.Value = ViewState["UserID"].ToString();
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHECK_USER_EXISTS("UPDATE"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'User already exists with same Name and Email ID.', 'warning');", true);
                }
                else
                {
                    UPDATE_USER();
                    INSERT_SYSTEM();

                    string script = @"
                     swal({
                     title: 'Success!',
                     text: 'Sponsor User Updated Successfully.',
                     icon: 'success',
                     button: 'OK'
                     }).then((value) => {
                        window.location.href = 'UMT_SponsorUser.aspx'; 
                     });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                    GET_USER();
                    CLEAR();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
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
                 ex.ToString();
                throw;
            }
        }

        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;


                Label lblUserID = row.FindControl("lblUserID") as Label;

                string UserID = lblUserID.Text.ToString();
                ViewState["UserID"] = UserID;

                string ID = e.CommandArgument.ToString();

                ViewState["ID"] = ID;
                if (e.CommandName == "EIDIT")
                {
                    EDIT_USER(ID);
                    GET_SYSTEM(UserID);
                    lbtnSubmit.Visible = false;
                    lbnUpdate.Visible = true;
                }
                else if (e.CommandName == "DELETED")
                {
                    DELETE_USER(ID);
                    GET_USER();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void DELETE_USER(string ID)
        {
            try
            {
                DataSet dsSystem = dal_UMT.UMT_USERS_SP(
                ACTION: "DELETE_USERID_SYSTEM",
                ID: ID
                );

                DataSet ds = dal_UMT.UMT_USERS_SP(
                  ACTION: "DELETE_USER",
                  ID: ID
                  );
                string script = @"
                     swal({
                     title: 'Success!',
                     text: 'Sponsor User deleted Successfully.',
                     icon: 'success',
                     button: 'OK'
                     }).then((value) => {
                        window.location.href = 'UMT_SponsorUser.aspx'; 
                     });";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                GET_USER();
                
                
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        private void EDIT_USER(string ID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                               ACTION: "EDIT_USER",
                               ID: ID
                           );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtFirstName.Text = ds.Tables[0].Rows[0]["Fname"].ToString();
                    txtLastName.Text = ds.Tables[0].Rows[0]["Lname"].ToString();
                    txtEmailid.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                    txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    txtNotes.Text = ds.Tables[0].Rows[0]["NOTES"].ToString();
                    drpUnblind.SelectedValue = ds.Tables[0].Rows[0]["Blind"].ToString();
                    drpStudyRole.SelectedValue = ds.Tables[0].Rows[0]["StudyRole"].ToString();
                    ddlTimeZone.SelectedValue = ds.Tables[0].Rows[0]["Timezone"].ToString();
                }
                else
                {
                    grdUser.DataSource = null;
                    grdUser.DataBind();
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.Url.ToString(), false);
                lbtnSubmit.Visible = true;
                lbnUpdate.Visible = false;
                CLEAR();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CLEAR()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmailid.Text = "";
            txtContactNo.Text = "";
            txtNotes.Text = "";
            drpStudyRole.SelectedIndex = 0;
            drpUnblind.SelectedIndex = 0;

            GET_TIMEZONE();

            GET_SYSTEM("");
        }

        private bool CHECK_USER_EXISTS(string ACTION)
        {
            bool exists = false;
            try
            {
                string Fname = txtFirstName.Text.Trim();
                string Lname = txtLastName.Text.Trim();
                string EmailID = txtEmailid.Text.Trim();

                DataSet ds = new DataSet();

                if (ACTION == "UPDATE")
                {
                    ds = dal_UMT.UMT_USERS_SP(ACTION: "CHECK_USER_EXISTS", Fname: Fname, Lname: Lname, EmailID: EmailID, ID: ViewState["ID"].ToString());
                }
                else
                {
                    ds = dal_UMT.UMT_USERS_SP(ACTION: "CHECK_USER_EXISTS", Fname: Fname, Lname: Lname, EmailID: EmailID);
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    exists = true;
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
            return exists;
        }

        protected void repeatSystem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    DataRowView row = (DataRowView)e.Item.DataItem;

                    CheckBox ChkSelect = (CheckBox)e.Item.FindControl("ChkSelect");

                    Label lblSystemName = (Label)e.Item.FindControl("lblSystemName");

                    TextBox txtSystemNotes = (TextBox)e.Item.FindControl("txtSystemNotes");
                    txtSystemNotes.Attributes.Add("maxlength", "200");


                    
                    HiddenField HiddenSubSytem = (HiddenField)e.Item.FindControl("HiddenSubSytem");

                    DataRowView rowView = e.Item.DataItem as DataRowView;
                    if (rowView != null)
                    {
                        string IsSelected = rowView["IsSelected"].ToString();

                        if (IsSelected == "True")
                        {
                            txtSystemNotes.Visible = true;
                            txtSystemNotes.Text = rowView["Notes"].ToString();

                            ChkSelect.Checked = true;

                        }
                        else
                        {
                            ChkSelect.Checked = false;
                            txtSystemNotes.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }

        protected void lblUserDetailsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Sponsor Users Details";

                DataSet ds = dal_UMT.UMT_LOG_SP(
                   ACTION: "GET_USER_SPONSOR"
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
                 ex.Message.ToString();
            }
        }

        protected void ChkSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < repeatSystem.Items.Count; i++)
            {

                CheckBox ChkSelect = (CheckBox)repeatSystem.Items[i].FindControl("ChkSelect");

                Label lblSystemName = (Label)repeatSystem.Items[i].FindControl("lblSystemName");

                TextBox txtSystemNotes = (TextBox)repeatSystem.Items[i].FindControl("txtSystemNotes");


                
                if (ChkSelect.Checked)
                {
                    txtSystemNotes.Visible = true;
                }
                else
                {
                    txtSystemNotes.Visible = false;
                }

                
            }
        }

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lbtdeleteuser = (e.Row.FindControl("lbtdeleteuser") as LinkButton);

                    if (dr["SYSTEM_COUNTS"].ToString() != "0")
                    {
                        lbtdeleteuser.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
    }
}