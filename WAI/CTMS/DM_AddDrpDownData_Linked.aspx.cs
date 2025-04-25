using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;
using System.Web.Services;

namespace CTMS
{
    public partial class DM_AddDrpDownData_Linked : System.Web.UI.Page
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

                    if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv();", true);
                        grdData.Columns[9].Visible = false;
                    }
                    else
                    {
                        grdData.Columns[9].Visible = true;
                    }

                    if (Request.QueryString["PGL_TYPE"].ToString() == "Prospective")
                    {
                        drp_PGL_Type.SelectedValue = "Prospective";
                        drp_PGL_Type.Enabled = false;
                    }
                    else
                    {
                        drp_PGL_Type.Enabled = true;
                    }

                    MODULE_STATUS();
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
                DataSet ds = dal_DB.DB_DRP_SP(ACTION: "GET_DRPDOWNDATA_LINKED",
                    ID: Request.QueryString["ID"].ToString(),
                    VARIABLENAME: Request.QueryString["VARIABLENAME"].ToString()
                    );

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblField.Text = ds.Tables[0].Rows[0]["FIELDNAME"].ToString();
                        lblVariable.Text = ds.Tables[0].Rows[0]["VARIABLENAME"].ToString();

                        DataSet dsField = dal_DB.DB_DRP_SP(ACTION: "GET_ParentLINKED_Fields",
                            MODULEID: ds.Tables[0].Rows[0]["MODULEID"].ToString()
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

                //Db Status 
                DataSet ds2 = dal_DB.DB_REVIEW_SP(ACTION: "Get_Status_Logs_Count");

                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    int count = Convert.ToInt32(ds2.Tables[0].Rows[0]["DB_STATUS_LOGS_COUNT"]);
                    if (count > 1)
                    {
                        divPGL_Type.Visible = true;

                        if (ds2 != null && ds2.Tables.Count > 1 && ds2.Tables[1].Rows.Count > 0)
                        {
                            Session["DB_STATUS_LOGS_LAST_DAT"] = ds2.Tables[1].Rows[0]["ENTEREDDAT"].ToString();
                        }
                    }
                    else
                    {
                        divPGL_Type.Visible = false;
                        Session.Remove("DB_STATUS_LOGS_LAST_DAT");
                        drp_PGL_Type.SelectedIndex = 0;
                    }
                }
                else
                {
                    divPGL_Type.Visible = false;
                    Session.Remove("DB_STATUS_LOGS_LAST_DAT");
                    drp_PGL_Type.SelectedIndex = 0;
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
                DataSet ds1 = dal_DB.DB_DRP_SP(ACTION: "CHECK_MODULE_FIELD_ON_MASTER",
                    ID: Request.QueryString["ID"].ToString()
                    );

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    LlbFieldevisit.Text = lblField.Text;

                    grdVisitLinkedAddOption.DataSource = ds1;
                    grdVisitLinkedAddOption.DataBind();

                    // Ensure UpdatePanel refreshes the GridView
                    UpdatePanel2.Update();
                    // Refresh UpdatePanel
                    UpdatePanel1.Update();

                    ModalPopupExtender1.Show();
                }
                else
                {
                    ADD_OPTIONS();

                    // Update the GridView UpdatePanel
                    UpdatePanel2.Update();
                    // Refresh UpdatePanel
                    UpdatePanel1.Update();
                    // Clear the radio button selection
                    rdoPermanentDelete.Checked = false;
                    rdoProspectiveDelete.Checked = false;

                    //Response.Write("<script> alert('Field Option added successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Field Option added successfully.'); window.location.href='" + Request.RawUrl.ToString() + "';", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void ADD_OPTIONS()
        {
            try
            {
                string PGL_Type = string.Empty;

                if (drp_PGL_Type.SelectedValue != "0")
                {
                    PGL_Type = drp_PGL_Type.SelectedValue;
                }

                DataSet ds = dal_DB.DB_DRP_SP(
                    ACTION: "INSERT_DRPDOWNDATA_LINKED",
                    VARIABLENAME: lblVariable.Text,
                    DEFAULTVAL: txtText.Text,
                    SEQNO: txtSeqNo.Text,
                    CONTROLTYPE: Request.QueryString["CONTROL"].ToString(),
                    ParentField: ddlParentField.SelectedValue,
                    ParentANS: ddlParentANS.SelectedValue,
                    PGL_TYPE: PGL_Type.ToString()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds1 = dal_DB.DB_DRP_SP(ACTION: "CHECK_MODULE_FIELD_ON_MASTER",
                    ID: Request.QueryString["ID"].ToString()
                    );

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    Label4Field.Text = Request.QueryString["VARIABLENAME"].ToString();

                    grdVisitLinkedUpdateOption.DataSource = ds1;
                    grdVisitLinkedUpdateOption.DataBind();
                    UpdatePanel2.Update();
                    // Refresh UpdatePanel
                    UpdatePanel1.Update();
                    ModalPopupExtender2.Show();
                }
                else
                {
                    UPDATE_OPTION();

                    Session.Remove("ID");
                    // Update the GridView UpdatePanel
                    UpdatePanel2.Update();
                    // Refresh UpdatePanel
                    UpdatePanel1.Update();

                    //Response.Write("<script> alert('Field Option updated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Field Option updated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "';", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void UPDATE_OPTION()
        {
            try
            {
                DataSet ds = dal_DB.DB_DRP_SP(
                    ACTION: "UPDATE_DRPDOWNDATA_BYID_LINKED",
                    VARIABLENAME: lblVariable.Text,
                    DEFAULTVAL: txtText.Text,
                    SEQNO: txtSeqNo.Text,
                    ID: Session["ID"].ToString(),
                    ParentField: ddlParentField.SelectedValue,
                    ParentANS: ddlParentANS.SelectedValue,
                    FIELDID: Request.QueryString["ID"].ToString()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Session.Remove("ID");
            Response.Redirect(Request.RawUrl);
        }

        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditModule")
                {
                    string id = e.CommandArgument.ToString();
                    Session["ID"] = id;

                    DataSet ds = dal_DB.DB_DRP_SP(ACTION: "GET_DRPDOWNDATA_BYID", ID: Session["ID"].ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlParentANS.SelectedValue = ds.Tables[0].Rows[0]["ParentANS"].ToString();
                        txtText.Text = ds.Tables[0].Rows[0]["TEXT"].ToString();
                        txtSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();

                        btnupdate.Visible = true;
                        btnsubmit.Visible = false;

                        //if (Session["DB_STATUS_LOGS_LAST_DAT"] != null)
                        //{
                        //    if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() == "")
                        //    {
                        //        if (Convert.ToDateTime(ds.Tables[0].Rows[0]["ENTEREDDAT"].ToString()) >= Convert.ToDateTime(Session["DB_STATUS_LOGS_LAST_DAT"].ToString()))
                        //        {
                        //            divPGL_Type.Visible = true;

                        //            if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "")
                        //            {
                        //                drp_PGL_Type.SelectedValue = ds.Tables[0].Rows[0]["PGL_TYPE"].ToString();
                        //            }
                        //        }
                        //        else
                        //        {
                        //            divPGL_Type.Visible = false;
                        //            drp_PGL_Type.SelectedIndex = 0;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        divPGL_Type.Visible = true;

                        //        if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "")
                        //        {
                        //            drp_PGL_Type.SelectedValue = ds.Tables[0].Rows[0]["PGL_TYPE"].ToString();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    divPGL_Type.Visible = false;
                        //    drp_PGL_Type.SelectedIndex = 0;
                        //}

                        if (Request.QueryString["SYSTEM"].ToString() == "Data Management" || Request.QueryString["SYSTEM"].ToString() == "eSource")
                        {
                            if (Session["DB_STATUS_LOGS_LAST_DAT"] != null)
                            {
                                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["ENTEREDDAT"].ToString()) >= Convert.ToDateTime(Session["DB_STATUS_LOGS_LAST_DAT"].ToString()))
                                {
                                    divPGL_Type.Visible = true;
                                    drp_PGL_Type.Enabled = true;
                                    if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "Deleted")
                                    {
                                        drp_PGL_Type.SelectedValue = ds.Tables[0].Rows[0]["PGL_TYPE"].ToString();
                                    }
                                }
                                else
                                {
                                    divPGL_Type.Visible = true;
                                    drp_PGL_Type.Enabled = true;
                                    if (ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["PGL_TYPE"].ToString() != "Deleted")
                                    {
                                        drp_PGL_Type.SelectedValue = ds.Tables[0].Rows[0]["PGL_TYPE"].ToString();
                                        drp_PGL_Type.Enabled = false;
                                    }
                                    else
                                    {
                                        divPGL_Type.Visible = false;
                                        drp_PGL_Type.SelectedIndex = 0;
                                    }
                                }
                            }
                            else
                            {
                                divPGL_Type.Visible = false;
                                drp_PGL_Type.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            divPGL_Type.Visible = false;
                            drp_PGL_Type.SelectedIndex = 0;
                        }
                    }
                    UpdatePanel2.Update();

                    if (Request.QueryString["FREEZE"].ToString() == "Frozen" || Request.QueryString["FREEZE"].ToString() == "Un-Freeze Request Generated")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "DisableDiv();", true);
                    }
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["DELETEDBY"] != DBNull.Value &&
                         !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DELETEDBY"].ToString()))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "DisableDiv('Deleted');", true);
                    }

                    MODULE_STATUS();
                }
                else if (e.CommandName == "DeleteModule")
                {

                    string id = e.CommandArgument.ToString();

                    DataSet ds = dal_DB.DB_DRP_SP(
                        ACTION: "DELETE_DRPDOWNDATA_BYID",
                        ID: id,
                        FIELDID: Request.QueryString["ID"].ToString()
                        );

                    Session.Remove("ID");
                    Getdata();
                    UpdatePanel2.Update();
                    //Response.Write("<script> alert('Field deleted successfully.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Field deleted successfully');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Field deleted successfully'); window.location.href='" + Request.RawUrl.ToString() + "';", true);

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
                DataSet dsANS = dal_DB.DB_DRP_SP(ACTION: "GET_ParentLINKED_Fields_ANS", ID: ddlParentField.SelectedValue);

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

        protected void btnSubmitLinkedAddVisitOption_Click(object sender, EventArgs e)
        {
            try
            {
                ADD_OPTIONS();

                for (int i = 0; i < grdVisitLinkedAddOption.Rows.Count; i++)
                {
                    CheckBox Chk_VISIT = (CheckBox)grdVisitLinkedAddOption.Rows[i].FindControl("Chk_VISIT");

                    if (Chk_VISIT.Checked == true)
                    {
                        Label VISITNUM = (Label)grdVisitLinkedAddOption.Rows[i].FindControl("VISITNUM");

                        DataSet ds = dal_DB.DB_DM_SP(ACTION: "INSERT_DRPDOWNDATA_LINKED_VISIT",
                            VARIABLENAME: lblVariable.Text,
                            DEFAULTVAL: txtText.Text,
                            SEQNO: txtSeqNo.Text,
                            CONTROLTYPE: Request.QueryString["CONTROL"].ToString(),
                            ParentField: ddlParentField.SelectedValue,
                            ParentANS: ddlParentANS.SelectedValue,
                            VISITNUM: VISITNUM.Text,
                            PGL_TYPE: drp_PGL_Type.SelectedValue.ToString()
                            );
                    }
                }
                // Update the GridView UpdatePanel
                UpdatePanel2.Update();

                //Response.Write("<script> alert('The field option has been successfully added, and the addition has also been reflected at the visit level.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('The field option has been successfully added, and the addition has also been reflected at the visit level.'); window.location.href='" + Request.RawUrl.ToString() + "';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelLinkedAddVisitOption_Click(object sender, EventArgs e)
        {
            //Response.Write("<script> alert('The action to add the field option has been canceled. Please enter the details again to add the field.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('The action to add the field option has been canceled. Please enter the details again to add the field.'); window.location.href='" + Request.RawUrl.ToString() + "';", true);
        }

        protected void btnUpdateLinkedVisitOption_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_OPTION();

                for (int i = 0; i < grdVisitLinkedUpdateOption.Rows.Count; i++)
                {
                    Label VISITNUM = (Label)grdVisitLinkedUpdateOption.Rows[i].FindControl("VISITNUM");

                    DataSet ds = dal_DB.DB_DM_SP(ACTION: "UPDATE_DRPDOWNDATA_LINKED_VISIT",
                        VARIABLENAME: lblVariable.Text,
                        DEFAULTVAL: txtText.Text,
                        OLD_OPTIONTEXT: hdnOldText.Value,
                        SEQNO: txtSeqNo.Text,
                        CONTROLTYPE: Request.QueryString["CONTROL"].ToString(),
                        ParentField: ddlParentField.SelectedValue,
                        ParentANS: ddlParentANS.SelectedValue,
                        VISITNUM: VISITNUM.Text
                        );
                }

                Session.Remove("ID");
                // Update the GridView UpdatePanel
                UpdatePanel2.Update();

                //Response.Write("<script> alert('The field option has been successfully updated, and the update has also been reflected at the visit level.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('The field option has been successfully updated, and the update has also been reflected at the visit level.'); window.location.href='" + Request.RawUrl.ToString() + "';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelLinkedVisitOption_Click(object sender, EventArgs e)
        {
            //    Response.Write("<script> alert('The action to update the field option has been canceled. Please enter the details again to modify the field.'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");
            Session.Remove("ID");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('The action to update the field option has been canceled. Please enter the details again to modify the field.'); window.location.href='" + Request.RawUrl.ToString() + "';", true);

        }

        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    //string COUNT = dr["COUNT"].ToString();
                    LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");

                    if (lbtndeleteSection != null && !string.IsNullOrEmpty(lblVariable.Text))
                    {
                        string id = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                        string variableName = lblVariable.Text;
                        string clientID = lbtndeleteSection.ClientID;
                        lbtndeleteSection.OnClientClick = $"return CheckStatusLogsBeforeDelete('{id}', '{ variableName}','{dr["ENTEREDDAT"]}', '{this.ClientID}');";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            // Check which radio button is selected
            bool isPermanentDelete = rdoPermanentDelete.Checked;
            bool isProspectiveDelete = rdoProspectiveDelete.Checked;

            // If all are valid, proceed:

            string id = hdnDeleteFieldID.Value;
            // Make sure ViewState["VARIABLENAME"] is set (or store it in another hidden field)
            string varName = hdnVariableName.Value;
            string pglType = "Deleted";

            if (isPermanentDelete)
            {
                // Perform permanent delete operation


                DataSet ds = dal_DB.DB_DRP_SP(
                          ACTION: "DELETE_DRPDOWNDATA_BYID",
                          ID: id
                          );

                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Field deleted successfully');", true);



                Getdata();

                // Clear the radio button selection
                rdoPermanentDelete.Checked = false;
                rdoProspectiveDelete.Checked = false;

                // Update the GridView UpdatePanel
                UpdatePanel2.Update();
                UpdatePanel1.Update();
                mpeDeleteConfirmation.Hide();

                // Update the modal UpdatePanel (if necessary)
                //  upModalPopup.Update();

                // Show success message and hide modal without reloading the page
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndAlert", @"alert('Field deleted successfully'); $find('" + mpeDeleteConfirmation.ClientID + @"').hide();", true);

                //Response.Write("<script> alert('Field deleted successfully'); window.location.href='" + Request.RawUrl.ToString() + "' </script>");

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage",  @"alert('Field deleted successfully');  $find('" + mpeDeleteConfirmation.ClientID + @"').hide();  setTimeout(function() { window.location.href='" + Request.RawUrl.ToString() + "'; }, 500);", true);


                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Field deleted successfully.'); window.location.href='" + Request.RawUrl.ToString() + "';", true);

                //  Response.Redirect(Request.RawUrl);

            }
            else if (isProspectiveDelete)
            {
                DataSet ds = dal_DB.DB_DRP_SP(ACTION: "UPDATE_PGL_TYPE",
                   ID: id,
                   VARIABLENAME: varName,
                   PGL_TYPE: pglType
                 );

                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PGL Type updated successfully');", true);



                Getdata();

                // Update the GridView UpdatePanel
                UpdatePanel2.Update();
                UpdatePanel1.Update();
                mpeDeleteConfirmation.Hide();

                // Update the modal UpdatePanel (if necessary)
                //  upModalPopup.Update();

                // Clear the radio button selection
                rdoPermanentDelete.Checked = false;
                rdoProspectiveDelete.Checked = false;

                // Close the modal and show success message
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndAlert", @" alert('PGL Type updated successfully');  $find('" + mpeDeleteConfirmation.ClientID + @"').hide();", true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('PGL Type updated successfully.'); window.location.href='" + Request.RawUrl.ToString() + "';", true);

                //// Optionally rebind your GridView or refresh the page
                // Response.Redirect(Request.RawUrl);
            }
        }

        [WebMethod]
        public static bool CheckStatusLogs(string ENTEREDDAT)
        {
            if (HttpContext.Current.Session["DB_STATUS_LOGS_LAST_DAT"] != null)
            {
                if (Convert.ToDateTime(ENTEREDDAT) <= Convert.ToDateTime(HttpContext.Current.Session["DB_STATUS_LOGS_LAST_DAT"].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected void btnCancelDelete_Click(object sender, EventArgs e)
        {
            // Clear the radio button selection
            rdoPermanentDelete.Checked = false;
            rdoProspectiveDelete.Checked = false;
            // Hide the modal
            mpeDeleteConfirmation.Hide();

            // Update the UpdatePanel
            upModalPopup.Update();
        }

        protected void MODULE_STATUS()
        {
            try
            {
                bool disablepage = false;

                DataSet ds_SYSTEM_COUNT = dal_DB.DB_REVIEW_SP(ACTION: "GET_SYSTEMS_BYMODULE_FOR_REVIEW_PROCESS",
                    MODULEID: Request.QueryString["MODULEID"].ToString()
                    );

                DataSet ds1 = dal_DB.DB_REVIEW_SP("GET_MODULE_STATUS",
                       MODULEID: Request.QueryString["MODULEID"].ToString(),
                       SYSTEM: Request.QueryString["SYSTEM"].ToString()
                       );

                if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "1")  //Sent For Review
                {
                    if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_GEN_COUNT"].ToString() != "0")
                    {
                        hdnModuleStatus.Value = "Un-Review Request Generated";
                        grdData.Columns[9].Visible = false;
                        btnsubmit.Visible = false;
                        btnupdate.Visible = false;
                    }
                    else
                    {
                        hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                        grdData.Columns[9].Visible = false;
                        btnsubmit.Visible = false;
                        btnupdate.Visible = false;
                    }

                    disablepage = true;
                }
                else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "2" || ds1.Tables[0].Rows[0]["STATUS"].ToString() == "4") //Open For Edit From Designer && Sent Back To Designer From Reviewer
                {
                    hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                    grdData.Columns[9].Visible = true;
                }
                else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "3" || ds1.Tables[0].Rows[0]["STATUS"].ToString() == "5") //Reviewed && Un-Review Request Generated From Reviewer
                {
                    hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                    grdData.Columns[9].Visible = false;
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;

                    disablepage = true;
                }
                else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "6") //Un-Review Request Approved
                {
                    if (ds_SYSTEM_COUNT.Tables[0].Rows.Count.ToString() == ds1.Tables[0].Rows[0]["UNREVIEW_REQ_APPROVE_COUNT"].ToString())
                    {
                        hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                        grdData.Columns[9].Visible = true;

                        disablepage = true;
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_GEN_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = "Un-Review Request Generated";
                            grdData.Columns[9].Visible = false;
                            btnsubmit.Visible = false;
                            btnupdate.Visible = false;

                            disablepage = true;
                        }
                        else
                        {
                            hdnModuleStatus.Value = "";
                            grdData.Columns[9].Visible = false;
                            btnsubmit.Visible = false;
                            btnupdate.Visible = false;

                            disablepage = false;
                        }
                    }
                }
                else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "7") //Un-Review Request Disapproved
                {
                    if (ds1.Tables[0].Rows[0]["UNREVIEW_REQ_DISSAPPROVE_COUNT"].ToString() != "0")
                    {
                        hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                        grdData.Columns[9].Visible = true;

                        disablepage = false;
                    }
                    else
                    {
                        hdnModuleStatus.Value = "";
                        grdData.Columns[9].Visible = true;
                        disablepage = true;
                    }
                }
                else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "8" || ds1.Tables[0].Rows[0]["STATUS"].ToString() == "9") //Frozen && Un-Freeze Request Generated
                {
                    hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                    grdData.Columns[9].Visible = false;
                    btnsubmit.Visible = false;
                    btnupdate.Visible = false;

                    disablepage = true;
                }
                else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "10") //Un-Freeze Request Approved
                {
                    if (ds_SYSTEM_COUNT.Tables[0].Rows.Count.ToString() == ds1.Tables[0].Rows[0]["UNFREEZING_REQ_APPROVE_COUNT"].ToString())
                    {
                        hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                        grdData.Columns[9].Visible = true;

                        disablepage = true;
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_GEN_COUNT"].ToString() != "0")
                        {
                            hdnModuleStatus.Value = "Un-Freeze Request Generated";
                            grdData.Columns[9].Visible = false;
                            btnsubmit.Visible = false;
                            btnupdate.Visible = false;

                            disablepage = true;
                        }
                        else
                        {
                            hdnModuleStatus.Value = "";
                            grdData.Columns[9].Visible = false;
                            btnsubmit.Visible = false;
                            btnupdate.Visible = false;

                            disablepage = false;
                        }
                    }
                }
                else if (ds1.Tables[0].Rows[0]["STATUS"].ToString() == "11") //Un-Freeze Request Disapproved
                {
                    if (ds1.Tables[0].Rows[0]["UNFREEZING_REQ_DISSAPPROVE_COUNT"].ToString() != "0")
                    {
                        hdnModuleStatus.Value = ds1.Tables[0].Rows[0]["STATUSNAME"].ToString();
                        grdData.Columns[9].Visible = true;

                        disablepage = false;
                    }
                    else
                    {
                        hdnModuleStatus.Value = "";
                        grdData.Columns[9].Visible = true;
                        disablepage = true;
                    }
                }
                else
                {
                    grdData.Columns[9].Visible = true;
                }

                if (disablepage)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisableDiv('" + hdnModuleStatus.Value + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}