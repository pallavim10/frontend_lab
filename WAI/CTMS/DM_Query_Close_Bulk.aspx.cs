using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_Query_Close_Bulk : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();
     

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
                    if (!string.IsNullOrEmpty(Request.QueryString["INVID"]))
                    {
                        GETDATA();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GETDATA()
        {
            try
            {
                DataSet ds = dal_DM.DM_QUERY_REPORT_SP(
                ACTION: "GET_BLINDED_QUERY_REPORT",
                INVID: Request.QueryString["INVID"].ToString(),
                SUBJID: Request.QueryString["SUBJID"].ToString(),
                VISITNUM: Request.QueryString["VISITNUM"].ToString(),
                MODULEID: Request.QueryString["MODULEID"].ToString(),
                FIELDID: Request.QueryString["FIELDID"].ToString(),
                QUERY_STATUS: Request.QueryString["QUERY_STATUS"].ToString(),
                QUERY_TYPE: Request.QueryString["QUERY_TYPE"].ToString()
                );

                if (ds.Tables[0] != null)
                {
                    grdQueryDetailReports.DataSource = ds.Tables[0];
                    grdQueryDetailReports.DataBind();
                }
                else
                {
                    grdQueryDetailReports.DataSource = null;
                    grdQueryDetailReports.DataBind();
                }
               
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdQueryDetailReports_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();

                DataSet ds = dal_DM.DM_QUERY_REPORT_SP(ACTION: "GetQueryDetailsByID", QUERYID: ID);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string MODULEID = ds.Tables[0].Rows[0]["MODULEID"].ToString();
                    string MODULENAME = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                    string VISITID = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                    string VISIT = ds.Tables[0].Rows[0]["VISIT"].ToString();
                    string INVID = ds.Tables[0].Rows[0]["INVID"].ToString();
                    string SUBJID = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                    string RECID = ds.Tables[0].Rows[0]["RECID"].ToString();

                    Session["QUERY_FIELDID"] = Request.QueryString["FIELDID"].ToString();
                    Session["QUERY_STATUS"] = Request.QueryString["QUERY_STATUS"].ToString();
                    Session["QUERY_TYPE"] = Request.QueryString["QUERY_TYPE"].ToString();

                    string UserType = Session["UserType"].ToString();

                    var URL = "";

                    if (UserType == "Site")
                    {
                        if ((ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0) && ds.Tables[1].Rows[0]["COUNTS"].ToString() == "1")
                        {
                            if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "1")
                            {
                                URL = "DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                            }
                            else
                            {
                                URL = "DM_DataEntry_INV_Read_Only.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                            }
                        }
                        else
                        {
                            if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "1")
                            {
                                URL = "DM_DataEntry_MultipleData.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                            }
                            else
                            {
                                URL = "DM_DataEntry.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                            }
                        }
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "1")
                        {
                            URL = "DM_DataEntry_MultipleData_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                        }
                        else
                        {
                            URL = "DM_DataEntry_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                        }
                    }

                    Session["QUERY_URL"] = "DM_Query_Reports.aspx";
                    Response.Redirect(URL);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdQueryDetailReports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;                
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
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

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("DM_Query_Reports.aspx?INVID=" + Request.QueryString["INVID"] + "&SUBJID=" + Request.QueryString["SUBJID"] + "&VISITNUM=" + Request.QueryString["VISITNUM"] + "&MODULEID=" + Request.QueryString["MODULEID"] + "&FIELDID=" + Request.QueryString["FIELDID"] + "&QUERY_STATUS=" + Request.QueryString["QUERY_STATUS"] + "&QUERY_TYPE=" + Request.QueryString["QUERY_TYPE"] + "");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Redirect to the same URL
            string currentUrl = Request.Url.AbsoluteUri;
            Response.Redirect(currentUrl, true);
        }

        protected void btnclosequery_Click(object sender, EventArgs e)
        {
            List<string> selectedIDs = new List<string>();

            // Loop through each row on the current page
            foreach (GridViewRow row in grdQueryDetailReports.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Find the checkbox and hidden field in the row
                    CheckBox Chek_Closed = row.FindControl("Chek_Closed") as CheckBox;
                    HiddenField hfRowId = row.FindControl("hfRowId") as HiddenField;

                    if (Chek_Closed != null && hfRowId.Value != null && Chek_Closed.Checked)
                    {
                        // Add the ID to the list if the checkbox is checked
                        // Assuming DataKeys are set for the GridView
                        selectedIDs.Add(hfRowId.Value);
                    }
                }
            }

            if (selectedIDs.Count > 0)
            {
                // Store selected IDs in ViewState or HiddenField for later use
                ViewState["selectedIDs"] = string.Join(",", selectedIDs);

                // Show the modal
                ModalPopupExtender1.Show();

              
            }
            else
            {
                // Provide feedback if no rows are selected
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select at least one row.');", true);
            }
        }

        public void Update_Bulk_Close(string ID, string Comments = "")
        {
            try
            {
                DAL_DM dal_DM = new DAL_DM();
                DataSet ds = new DataSet();       

                ds = dal_DM.DM_QUERY_SP(ACTION: "Closed_Bulk_Query",
                    ID: ID,
                    Comment: Comments
                    );

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert(Query Closed Successfully.'); ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                txt_Comments.Text = string.Empty;
                ViewState["SelectedIds"] = null;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "window.location.href = '" + Request.Url.ToString() + "';", true);

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Comments for close query canceled successfully.');window.location.href = '" + Request.Url.ToString() + "';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            string selectedIDs = ViewState["selectedIDs"] as string;
            string comment = txt_Comments.Text;

            if (!string.IsNullOrEmpty(selectedIDs) && !string.IsNullOrEmpty(comment))
            {

                Update_Bulk_Close(selectedIDs, comment); // Replace with actual DB logic
             
                // Clear the inputs
                txt_Comments.Text = string.Empty;
                ViewState["selectedIDs"] = null;

                ModalPopupExtender1.Hide();
                // Provide feedback to the user
         
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Query Closed Successfully.');window.location.href = '" + Request.Url.ToString() + "';", true);


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a comment.');", true);
                ModalPopupExtender1.Show(); // Reopen the modal for correction
            }
        }
    }
}