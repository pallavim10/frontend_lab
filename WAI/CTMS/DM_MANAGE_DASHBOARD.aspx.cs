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
    public partial class DM_MANAGE_DASHBOARD : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!IsPostBack)
                {
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                    Response.Cache.SetNoStore();
                    GET_DATA_DASHBOARD();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }
        private void GET_DATA_DASHBOARD()
        {
            try
            {
                DataSet ds = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "GET_DM_DASHBOARD"
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdManageDashboard.DataSource = ds;
                    grdManageDashboard.DataBind();
                }
                else
                {
                    grdManageDashboard.DataSource = null;
                    grdManageDashboard.DataBind();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_DASHBOARD_SEQNO();
                CLEAR_SELECTION();


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var DashboardID = Session["ID"].ToString();

                DataSet dsCurrent = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "CHECK_DM_DASHBOARD", TYPE: Dashboardtype.SelectedValue, SEQNO: txtSeqNo.Text.ToString());
                if (dsCurrent != null && dsCurrent.Tables.Count > 0 && dsCurrent.Tables[0].Rows.Count > 0)
                {
                    string existingID = dsCurrent.Tables[0].Rows[0]["ID"].ToString();
                    string existingSeqNo = dsCurrent.Tables[0].Rows[0]["SEQUENCENO"].ToString();

                    if (existingID != DashboardID)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Another record already exists with the same Dashboard Type. Update not allowed.');", true);
                        return;
                    }

                    //if (VALEXISTS("Update", Dashboardtype.SelectedValue, txtSeqNo.Text.ToString()))
                    //{
                    //    DataSet ds = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "UPDATE_DM_DASHBOARD_SEQNO",
                    //         TYPE: Dashboardtype.SelectedValue,
                    //      SEQNO: txtSeqNo.Text.Trim(),
                    //        ID: DashboardID
                    //      );
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Dashboard Sequence updated successfully.');", true);
                    //    CLEAR_SELECTION();
                    //    GET_DATA_DASHBOARD();

                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Dashboard Type already selected.');", true);
                    //    CLEAR_SELECTION();
                    //}
                    if (existingID == DashboardID && existingSeqNo != txtSeqNo.Text.ToString())
                    {
                        // Proceed with update
                        dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "UPDATE_DM_DASHBOARD_SEQNO",
                            TYPE: Dashboardtype.SelectedValue,
                            SEQNO: txtSeqNo.Text.ToString(),
                            ID: DashboardID);

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Dashboard Sequence updated successfully.');", true);
                        CLEAR_SELECTION();
                        GET_DATA_DASHBOARD();
                        return;
                    }
                }

                dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "UPDATE_DM_DASHBOARD_SEQNO",
            TYPE: Dashboardtype.SelectedValue,
            SEQNO: txtSeqNo.Text.ToString(),
            ID: DashboardID);

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Dashboard Sequence updated successfully.');", true);

                // 🔹 5. Refresh Grid and Clear Selection
                CLEAR_SELECTION();
                GET_DATA_DASHBOARD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void INSERT_DASHBOARD_SEQNO()
        {
            try
            {
                DataSet dsExists = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "CHECK_DM_DASHBOARD_DATA", TYPE: Dashboardtype.SelectedValue);
                if (dsExists.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('This Dashboard Type already exists.');", true);
                    return;
                }
               
                    DataSet ds = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "INSERT_DM_DASHBOARD_SEQNO",
                          TYPE: Dashboardtype.SelectedValue,
                          SEQNO: txtSeqNo.Text.Trim()
                          );
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Dashboard Sequence added successfully.');", true);
                    GET_DATA_DASHBOARD();
                    CLEAR_SELECTION();
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdManageDashboard_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditDashboardSeqno" || e.CommandName == "DeleteDashboardSeqno")
            {
                try
                {
                    string DASHBOARDID = e.CommandArgument.ToString();
                    Session["ID"] = DASHBOARDID;
                    if (e.CommandName == "EditDashboardSeqno")
                    {
                        DataSet ds = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "EDIT_DM_DASHBOARD_SEQNO", ID: DASHBOARDID);
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            foreach (DataRow row in dt.Rows)
                            {
                                Dashboardtype.SelectedValue = row["TYPE"].ToString();
                                hdnTYPE.Value = row["TYPE"].ToString();
                                txtSeqNo.Text = row["SEQUENCENO"].ToString();
                                hdnID.Value = row["ID"].ToString();
                                hdnSeqNo.Value = row["SEQUENCENO"].ToString();
                            }
                        }
                        btnSubmitClick.Visible = false;
                        btnUpdateClick.Visible = true;

                    }
                    else if (e.CommandName == "DeleteDashboardSeqno")
                    {
                        string DASHBOARDTYPEID = e.CommandArgument.ToString();
                        DELETE_DASHBOARDSEQ(DASHBOARDID);
                        GET_DATA_DASHBOARD();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        private void DELETE_DASHBOARDSEQ(string DASHBOARDID)
        {
            try
            {
                DataSet ds = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "DELETE_DM_DASHBOARD_SEQNO", ID: DASHBOARDID);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Dashboard Sequence Deleted successfully.');", true);
                CLEAR_SELECTION();
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Failed to delete record.');", true);
                ex.StackTrace.ToString();
                CLEAR_SELECTION();
            }
        }

        protected void Dashboard_ValChange(object sender, EventArgs e)
        {
            try
            {
                var Selectedval = Dashboardtype.SelectedValue;

                if (Selectedval != "0" || Selectedval != "")
                {
                    DataSet ds = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "GET_DASHBOARD_BY_TYPE", TYPE: Selectedval);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // Check if multiple records exist
                        if (ds.Tables[0].Rows.Count > 1)
                        {
                            lblErrorMsg.Text = "Only one record per dashboard type is allowed.";
                            grdManageDashboard.DataSource = null;
                            grdManageDashboard.DataBind();
                            return;
                        }

                        // If only one record exists, bind data and trigger edit
                        grdManageDashboard.DataSource = ds;
                        grdManageDashboard.DataBind();

                        string dashboardID = ds.Tables[0].Rows[0]["ID"].ToString();
                        GridViewCommandEventArgs args = new GridViewCommandEventArgs(null, new CommandEventArgs("EditDashboardSeqno", dashboardID));
                        grdManageDashboard_RowCommand(this, args);
                    }
                    else
                    {
                        // No records found, allow insertion
                        GET_DATA_DASHBOARD();
                        txtSeqNo.Text = "";
                        btnSubmitClick.Visible = true;
                        btnUpdateClick.Visible = false;
                        //INSERT_DASHBOARD_SEQNO();                   

                    }
                }
                else
                {
                    GET_DATA_DASHBOARD();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }


        }

        //private bool VALEXISTS(string Action, string DashboardType, string SeqNo)
        //{
        //    bool SEQNOCHK = true;
        //    try
        //    {
        //        if (Action == "Update" && (DashboardType != "0" || DashboardType != "") && (DashboardType == hdnTYPE.Value) && (SeqNo != hdnSeqNo.Value.ToString() || SeqNo == hdnSeqNo.Value.ToString()))
        //        {
        //            DataSet ds = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "CHECK_DM_DASHBOARD", TYPE: DashboardType, SEQNO: SeqNo);
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                SEQNOCHK = false;
        //            }
        //        }
        //        else
        //        {
        //            if (Action == "Insert" && (DashboardType != "0" || DashboardType != "") && (!string.IsNullOrEmpty(SeqNo) || !string.IsNullOrWhiteSpace(SeqNo)))
        //            {
        //                DataSet ds = dal_DM.DM_MANAGE_DASHBOARD_SP(ACTION: "CHECK_DM_DASHBOARD_DATA", TYPE: DashboardType, SEQNO: SeqNo);
        //                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //                {
        //                    SEQNOCHK = false;
        //                }

        //            }
        //        }

        //    }
        //    catch (Exception ex)

        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Record Already Exists.');", true);
        //        ex.StackTrace.ToString();
        //    }

        //    return SEQNOCHK;
        //}
        protected void grdManageDashboard_PreRender(object sender, EventArgs e)
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


        private void CLEAR_SELECTION()
        {
            try
            {
                Dashboardtype.ClearSelection();
                txtSeqNo.Text = " ";
                btnUpdateClick.Visible = false;
                btnSubmitClick.Visible = true;
                GET_DATA_DASHBOARD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                CLEAR_SELECTION();
                GET_DATA_DASHBOARD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }
    }
}