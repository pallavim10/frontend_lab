using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class UMT_User : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtNotes.Attributes.Add("maxlength", "200");

                    GET_USER();
                    BIND_STUDYROLE();
                    GET_TIMEZONE();
                    GET_SYSTEM("");
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_STUDYROLE()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "GET_STUDYROLE");
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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_TIMEZONE()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(ACTION: "GETTIMEZONE");

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

        private void GET_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                    ACTION: "GET_USER"
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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SYSTEM(string UserID)
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                    ACTION: "GET_SYSTEM",
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
                lblErrorMsg.Text = ex.Message.ToString();
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

                    CheckBox ChkSubsysIWRS = (CheckBox)repeatSystem.Items[i].FindControl("ChkSubsysIWRS");
                    Label lblSubsystemIWRS = (Label)repeatSystem.Items[i].FindControl("lblSubsystemIWRS");

                    CheckBox ChkSubSysPharma = (CheckBox)repeatSystem.Items[i].FindControl("ChkSubSysPharma");
                    Label lblSubSysPharma = (Label)repeatSystem.Items[i].FindControl("lblSubSysPharma");
                    //DM
                    CheckBox chksubsysFreeze = (CheckBox)repeatSystem.Items[i].FindControl("chksubsysFreeze");
                    Label lblsubsysFreeze = (Label)repeatSystem.Items[i].FindControl("lblsubsysFreeze");
                    CheckBox chksubsysUnFreeze = (CheckBox)repeatSystem.Items[i].FindControl("chksubsysUnFreeze");
                    Label lblsubsysUNFreeze = (Label)repeatSystem.Items[i].FindControl("lblsubsysUNFreeze");
                    CheckBox chksubsysLock = (CheckBox)repeatSystem.Items[i].FindControl("chksubsysLock");
                    Label lblsubsysLock = (Label)repeatSystem.Items[i].FindControl("lblsubsysLock");
                    CheckBox chksubsysUnlock = (CheckBox)repeatSystem.Items[i].FindControl("chksubsysUnlock");
                    Label lblsubsysunLock = (Label)repeatSystem.Items[i].FindControl("lblsubsysunLock");
                    //eSource
                    CheckBox ChksubsysSourceDataReview = (CheckBox)repeatSystem.Items[i].FindControl("ChksubsysSourceDataReview");
                    Label lblsubsysSourceDataReview = (Label)repeatSystem.Items[i].FindControl("lblsubsysSourceDataReview");
                    CheckBox chlReadOnlyeSource = (CheckBox)repeatSystem.Items[i].FindControl("chlReadOnlyeSource");
                    Label LblReadOnly = (Label)repeatSystem.Items[i].FindControl("LblReadOnly");
                    //Design bench
                    CheckBox ChksubDesignbench = (CheckBox)repeatSystem.Items[i].FindControl("ChksubDesignbench");
                    Label lblDesigner = (Label)repeatSystem.Items[i].FindControl("lblDesigner");
                    CheckBox ChksubApprover = (CheckBox)repeatSystem.Items[i].FindControl("ChksubApprover");
                    Label lblApprover = (Label)repeatSystem.Items[i].FindControl("lblApprover");
                    //eTMF
                    CheckBox chkArchivist = (CheckBox)repeatSystem.Items[i].FindControl("chkArchivist");
                    Label lblchkArchivist = (Label)repeatSystem.Items[i].FindControl("lblchkArchivist");
                    CheckBox chkConfiSpecialist = (CheckBox)repeatSystem.Items[i].FindControl("chkConfiSpecialist");
                    Label lblConfiSpecialist = (Label)repeatSystem.Items[i].FindControl("lblConfiSpecialist");
                    CheckBox ChkDocuSpecialist = (CheckBox)repeatSystem.Items[i].FindControl("ChkDocuSpecialist");
                    Label lblDocuSpecialist = (Label)repeatSystem.Items[i].FindControl("lblDocuSpecialist");
                    CheckBox chkContributor = (CheckBox)repeatSystem.Items[i].FindControl("chkContributor");
                    Label lblContributor = (Label)repeatSystem.Items[i].FindControl("lblContributor");
                    CheckBox ChkQC = (CheckBox)repeatSystem.Items[i].FindControl("ChkQC");
                    Label lblQC = (Label)repeatSystem.Items[i].FindControl("lblQC");


                    string SubSytem = "";
                    if (ChkSubsysIWRS.Checked)
                    {
                        SubSytem = lblSubsystemIWRS.Text;
                    }
                    if (ChkSubSysPharma.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblSubSysPharma.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblSubSysPharma.Text;
                        }

                    }
                    if (chksubsysFreeze.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblsubsysFreeze.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblsubsysFreeze.Text;
                        }

                    }

                    if (chksubsysUnFreeze.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblsubsysUNFreeze.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblsubsysUNFreeze.Text;
                        }

                    }
                    if (chksubsysLock.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblsubsysLock.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblsubsysLock.Text;
                        }

                    }
                    if (chksubsysUnlock.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblsubsysunLock.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblsubsysunLock.Text;
                        }

                    }

                    if (ChksubsysSourceDataReview.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblsubsysSourceDataReview.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblsubsysSourceDataReview.Text;
                        }

                    }
                    if (chlReadOnlyeSource.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = LblReadOnly.Text;
                        }
                        else
                        {
                            SubSytem += "," + LblReadOnly.Text;
                        }

                    }

                    if (ChksubDesignbench.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblDesigner.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblDesigner.Text;
                        }

                    }
                    if (ChksubApprover.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblApprover.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblApprover.Text;
                        }

                    }

                    if (chkArchivist.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblchkArchivist.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblchkArchivist.Text;
                        }

                    }
                    if (chkConfiSpecialist.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblConfiSpecialist.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblConfiSpecialist.Text;
                        }

                    }
                    if (ChkDocuSpecialist.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblDocuSpecialist.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblDocuSpecialist.Text;
                        }

                    }
                    if (chkContributor.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblContributor.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblContributor.Text;
                        }

                    }
                    if (ChkQC.Checked)
                    {
                        if (SubSytem == "")
                        {
                            SubSytem = lblQC.Text;
                        }
                        else
                        {
                            SubSytem += "," + lblQC.Text;
                        }

                    }


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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_USER_DATA()
        {
            DataSet da = dal_UMT.UMT_USERS_SP(
                                        ACTION: "INSERT_USER",
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
                    Response.Write("<script language=javascript>alert('User already exists with this Name and Email Id');</script>");
                }
                else
                {
                    INSERT_USER_DATA();
                    INSERT_SYSTEM();

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Internal User Created Successfully');  window.location.href = 'UMT_User.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
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
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return exists;
        }

        private void UPDATE_USER()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_USERS_SP(
                         ACTION: "UPDATE_USER",
                         ID: ViewState["ID"].ToString(),
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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHECK_USER_EXISTS("UPDATE"))
                {
                    Response.Write("<script language=javascript>alert('User already exists with same Name and Email ID');</script>");
                }
                else
                {
                    UPDATE_USER();
                    INSERT_SYSTEM();

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Internal User Updated Successfully');  window.location.href = 'UMT_User.aspx';", true);
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
                lblErrorMsg.Text = ex.Message.ToString();
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

                Response.Write("<script> alert('User deleted Successfully'); window.location.href = 'UMT_User.aspx';</script>");
                GET_USER();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                    drpUnblind.SelectedValue = ds.Tables[0].Rows[0]["Blind"].ToString();
                    drpStudyRole.SelectedValue = ds.Tables[0].Rows[0]["StudyRole"].ToString();
                    ddlTimeZone.SelectedValue = ds.Tables[0].Rows[0]["Timezone"].ToString();
                    txtNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
                }
                else
                {
                    grdUser.DataSource = null;
                    grdUser.DataBind();
                }
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

        protected void repeatSystem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DAL dal;
                    dal = new DAL();

                    DataRowView row = (DataRowView)e.Item.DataItem;

                    CheckBox ChkSelect = (CheckBox)e.Item.FindControl("ChkSelect");

                    Label lblSystemName = (Label)e.Item.FindControl("lblSystemName");

                    TextBox txtSystemNotes = (TextBox)e.Item.FindControl("txtSystemNotes");
                    txtSystemNotes.Attributes.Add("maxlength", "200");

                    HtmlGenericControl divSubsysIWRS = e.Item.FindControl("divSubsysIWRS") as HtmlGenericControl;
                    HtmlGenericControl divSubSysPharma = e.Item.FindControl("divSubSysPharma") as HtmlGenericControl;
                    HtmlGenericControl divsubsysDM = e.Item.FindControl("divsubsysDM") as HtmlGenericControl;
                    HtmlGenericControl divsubsyseSource = e.Item.FindControl("divsubsyseSource") as HtmlGenericControl;
                    HtmlGenericControl divsubsysDesignbench = e.Item.FindControl("divsubsysDesignbench") as HtmlGenericControl;
                    HtmlGenericControl divsubsyseTMF = e.Item.FindControl("divsubsyseTMF") as HtmlGenericControl;

                    CheckBox ChkSubsysIWRS = (CheckBox)e.Item.FindControl("ChkSubsysIWRS");
                    CheckBox ChkSubSysPharma = (CheckBox)e.Item.FindControl("ChkSubSysPharma");
                    //CheckBox chksubsysDM = (CheckBox)e.Item.FindControl("chksubsysDM");

                    CheckBox ChksubsysSourceDataReview = (CheckBox)e.Item.FindControl("ChksubsysSourceDataReview");
                    CheckBox chlReadOnlyeSource = (CheckBox)e.Item.FindControl("chlReadOnlyeSource");

                    CheckBox chksubsysFreeze = (CheckBox)e.Item.FindControl("chksubsysFreeze");
                    CheckBox chksubsysUnFreeze = (CheckBox)e.Item.FindControl("chksubsysUnFreeze");
                    CheckBox chksubsysLock = (CheckBox)e.Item.FindControl("chksubsysLock");
                    CheckBox chksubsysUnlock = (CheckBox)e.Item.FindControl("chksubsysUnlock");

                    CheckBox ChksubDesignbench = (CheckBox)e.Item.FindControl("ChksubDesignbench");
                    CheckBox ChksubApprover = (CheckBox)e.Item.FindControl("ChksubApprover");

                    CheckBox chkArchivist = (CheckBox)e.Item.FindControl("chkArchivist");
                    CheckBox chkConfiSpecialist = (CheckBox)e.Item.FindControl("chkConfiSpecialist");
                    CheckBox ChkDocuSpecialist = (CheckBox)e.Item.FindControl("ChkDocuSpecialist");
                    CheckBox chkContributor = (CheckBox)e.Item.FindControl("chkContributor");
                    CheckBox ChkQC = (CheckBox)e.Item.FindControl("ChkQC");



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
                            if (lblSystemName.Text == "IWRS")
                            {
                                divSubsysIWRS.Visible = false;
                                if (HiddenSubSytem.Value == "Unblinding")
                                {
                                    ChkSubsysIWRS.Checked = false;
                                }
                            }
                            else if (lblSystemName.Text == "Pharmacovigilance")
                            {
                                divSubSysPharma.Visible = true;

                                if (HiddenSubSytem.Value == "Sign-Off")
                                {
                                    ChkSubSysPharma.Checked = false;
                                }
                            }
                            else if (lblSystemName.Text == "Data Management")
                            {
                                divsubsysDM.Visible = true;
                                if (HiddenSubSytem.Value.Split(',').Contains("Freeze"))
                                {
                                    chksubsysFreeze.Checked = true;
                                }
                                if (HiddenSubSytem.Value.Split(',').Contains("UnFreeze"))
                                {
                                    chksubsysUnFreeze.Checked = true;
                                }
                                if (HiddenSubSytem.Value.Split(',').Contains("Lock"))
                                {
                                    chksubsysLock.Checked = true;
                                }
                                if (HiddenSubSytem.Value.Split(',').Contains("Unlock"))
                                {
                                    chksubsysUnlock.Checked = true;
                                }
                            }
                            else if (lblSystemName.Text == "eSource")
                            {
                                divsubsyseSource.Visible = true;
                                if (HiddenSubSytem.Value.Split(',').Contains("Source Data Review"))
                                {
                                    ChksubsysSourceDataReview.Checked = true;

                                }
                                if (HiddenSubSytem.Value.Split(',').Contains("Read-Only"))
                                {

                                    chlReadOnlyeSource.Checked = true;
                                }
                            }
                            else if (lblSystemName.Text == "Design Bench")
                            {
                                divsubsysDesignbench.Visible = true;
                                if (HiddenSubSytem.Value.Split(',').Contains("Designer"))
                                {
                                    ChksubDesignbench.Checked = true;

                                }
                                if (HiddenSubSytem.Value.Split(',').Contains("Approver"))
                                {

                                    ChksubApprover.Checked = true;
                                }
                            }
                            else if (lblSystemName.Text == "eTMF")
                            {
                                divsubsyseTMF.Visible = true;
                                if (HiddenSubSytem.Value.Split(',').Contains("Archivist"))
                                {
                                    chkArchivist.Checked = true;

                                }
                                if (HiddenSubSytem.Value.Split(',').Contains("Configuration Specialist"))
                                {

                                    chkConfiSpecialist.Checked = true;
                                }
                                if (HiddenSubSytem.Value.Split(',').Contains("Document Specialist"))
                                {

                                    ChkDocuSpecialist.Checked = true;
                                }
                                if (HiddenSubSytem.Value.Split(',').Contains("Contributor"))
                                {

                                    chkContributor.Checked = true;
                                }
                                if (HiddenSubSytem.Value.Split(',').Contains("QC"))
                                {

                                    ChkQC.Checked = true;
                                }
                            }
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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lblUserDetailsExport_Click(object sender, EventArgs e)
        {
            try
            {
                string xlname = "Internal Users Details";

                DataSet ds = dal_UMT.UMT_LOG_SP(
                   ACTION: "GET_USER"
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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ChkSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < repeatSystem.Items.Count; i++)
            {

                CheckBox ChkSelect = (CheckBox)repeatSystem.Items[i].FindControl("ChkSelect");

                Label lblSystemName = (Label)repeatSystem.Items[i].FindControl("lblSystemName");

                TextBox txtSystemNotes = (TextBox)repeatSystem.Items[i].FindControl("txtSystemNotes");

                HtmlGenericControl divSubsysIWRS = repeatSystem.Items[i].FindControl("divSubsysIWRS") as HtmlGenericControl;
                HtmlGenericControl divSubSysPharma = repeatSystem.Items[i].FindControl("divSubSysPharma") as HtmlGenericControl;
                HtmlGenericControl divsubsysDM = repeatSystem.Items[i].FindControl("divsubsysDM") as HtmlGenericControl;
                HtmlGenericControl divsubsyseSource = repeatSystem.Items[i].FindControl("divsubsyseSource") as HtmlGenericControl;
                HtmlGenericControl divsubsysDesignbench = repeatSystem.Items[i].FindControl("divsubsysDesignbench") as HtmlGenericControl;
                HtmlGenericControl divsubsyseTMF = repeatSystem.Items[i].FindControl("divsubsyseTMF") as HtmlGenericControl;

                if (ChkSelect.Checked)
                {
                    txtSystemNotes.Visible = true;
                }
                else
                {
                    txtSystemNotes.Visible = false;
                }

                if (lblSystemName.Text == "IWRS" && ChkSelect.Checked && drpUnblind.SelectedValue == "Blinded")
                {
                    divSubsysIWRS.Visible = false;
                }
                else
                {
                    divSubsysIWRS.Visible = false;
                }

                if (lblSystemName.Text == "Pharmacovigilance" && ChkSelect.Checked)
                {
                    divSubSysPharma.Visible = false;

                }
                else
                {
                    divSubSysPharma.Visible = false;

                }
                if (lblSystemName.Text == "Data Management" && ChkSelect.Checked)
                {
                    divsubsysDM.Visible = true;

                }
                else
                {
                    divsubsysDM.Visible = false;

                }
                if (lblSystemName.Text == "eSource" && ChkSelect.Checked)
                {
                    divsubsyseSource.Visible = true;

                }
                else
                {
                    divsubsyseSource.Visible = false;
                }
                if (lblSystemName.Text == "Design Bench" && ChkSelect.Checked)
                {
                    divsubsysDesignbench.Visible = true;

                }
                else
                {
                    divsubsysDesignbench.Visible = false;
                }
                if (lblSystemName.Text == "eTMF" && ChkSelect.Checked)
                {
                    divsubsyseTMF.Visible = true;

                }
                else
                {
                    divsubsyseTMF.Visible = false;
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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}