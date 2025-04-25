using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DB_UNREVIEW_REQUESTS_LIST : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtReason.Attributes.Add("MaxLength", "500");
            if (!Page.IsPostBack)
            {
                GetSystems();
            }
        }

        protected void GetSystems()
        {
            try
            {
                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_USER_SYSTEMS_REVIEW");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    drpSystem.DataSource = ds.Tables[0];
                    drpSystem.DataValueField = "SystemName";
                    drpSystem.DataTextField = "SystemName";
                    drpSystem.DataBind();
                    drpSystem.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSystem.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetModule();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetModule()
        {
            try
            {
                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_MODULES_FOR_UNREVIEW_REQUEST", SYSTEM: drpSystem.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdModule.DataSource = ds.Tables[0];
                    grdModule.DataBind();
                }
                else
                {
                    grdModule.DataSource = null;
                    grdModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdModule_PreRender(object sender, EventArgs e)
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

        protected void lbtnAction_Click(object sender, EventArgs e)
        {
            try
            {
                txtReason.Attributes.Add("MaxLength", "500");
                LinkButton btn = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;

                string MODULEID = (gvr.FindControl("ID") as Label).Text;
                string MODULENAME = (gvr.FindControl("MODULENAME") as Label).Text;
                hdnModuleid.Value = MODULEID;
                hdnModulename.Value = MODULENAME;

                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_SYSTEMS_BYMODULE_BYUSER",
                    MODULEID: MODULEID
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblReviewHeader.InnerText = MODULENAME + " module is available at below systems.";

                    lstSystems.DataSource = ds.Tables[0];
                    lstSystems.DataBind();
                }
                else
                {
                    lstSystems.DataSource = null;
                    lstSystems.DataBind();
                }

                ModalPopupExtender2.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnApproveUnReviewReq_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lstSystems.Items.Count; i++)
                {
                    dal_DB.DB_REVIEW_SP(ACTION: "UPDATE_UNREVIEW_REQ_APP",
                        MODULEID: hdnModuleid.Value,
                        SYSTEM: (lstSystems.Items[i].FindControl("lblSystemName") as Label).Text,
                        REVIEW: false,
                        REASON: txtReason.Text
                        );
                }

                dal_DB.DB_REVIEW_SP(ACTION: "UPDATE_UNREVIEW_STATUS",
                        MODULEID: hdnModuleid.Value,
                        REVIEW: false,
                        REASON: txtReason.Text
                        );

                SendEmail("Approved Un-Review Request", txtReason.Text);

                string MSG = "Un-Review request has been approved for " + hdnModulename.Value + " module.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + MSG + "'); window.location.href = '" + Request.RawUrl.ToString() + "' ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnDisapproveUnReviewReq_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lstSystems.Items.Count; i++)
                {
                    dal_DB.DB_REVIEW_SP(ACTION: "UPDATE_UNREVIEW_REQ_DisApp",
                        MODULEID: hdnModuleid.Value,
                        SYSTEM: (lstSystems.Items[i].FindControl("lblSystemName") as Label).Text,
                        REVIEW: false,
                        REASON: txtReason.Text
                        );
                }

                SendEmail("Disapproved Un-Review Request", txtReason.Text);

                string MSG = "Un-Review request has been disapproved for " + hdnModulename.Value + " module.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + MSG + "'); window.location.href = '" + Request.RawUrl.ToString() + "' ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtReason.Text = "";
                ModalPopupExtender2.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        [System.Web.Services.WebMethod]
        public static string REVIEW_HISTORY(string MODULEID)
        {
            string str = "";
            try
            {
                DAL_DB dal_DB = new DAL_DB();

                DataSet ds = dal_DB.DB_REVIEW_SP(
                    ACTION: "GET_REVIEW_LOGS",
                    MODULEID: MODULEID
                    );

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        str = ConvertDataTableToHTML(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
            return str;
        }

        public static string ConvertDataTableToHTML(DataSet ds)
        {
            string html = "<table class='table table-bordered table-striped' style='font-size:Small; border-collapse:collapse; '>";
            //add header row
            html += "<tr style='text-align: left;color: blue;'>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                html += "<th scope='col'>" + ds.Tables[0].Columns[i].ColumnName + "</th>";
            html += "</tr>";

            //add rows
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                html += "<tr style='text-align: left;'>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    html += "<td>" + ds.Tables[0].Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        protected void SendEmail(string ACTIONS, string REASON)
        {
            try
            {
                CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();

                DataSet dsSystem = dal_DB.DB_REVIEW_SP(ACTION: "GET_SYSTEMS_BYMODULE",
                    MODULEID: hdnModuleid.Value
                    );

                if (dsSystem.Tables.Count > 0 && dsSystem.Tables[0].Rows.Count > 0)
                {
                    DataSet ds = dal_DB.DB_EMAIL_REVIEW_SP(ACTION: "GET_EMAILSIDS",
                        ACTIVITY: ACTIONS,
                        SYSTEMS: dsSystem.Tables[0].Rows[0]["SystemName"].ToString()
                        );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string EMAILID = ds.Tables[0].Rows[0]["EMAILIDS"].ToString();

                        string EMAIL_CC = ds.Tables[0].Rows[0]["CCEMAILIDS"].ToString();

                        string EMAIL_BCC = ds.Tables[0].Rows[0]["BCCEMAILIDS"].ToString();

                        string EmailSubject = ds.Tables[0].Rows[0]["EMAIL_SUBJECT"].ToString();

                        string EmailBody = ds.Tables[0].Rows[0]["EMAIL_BODY"].ToString();

                        if (EmailSubject.Contains("[") && EmailSubject.Contains("]"))
                        {
                            if (EmailSubject.Contains("[PROJECTID]"))
                            {
                                EmailSubject = EmailSubject.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                            }

                            if (EmailSubject.Contains("[MODULENAME]"))
                            {
                                EmailSubject = EmailSubject.Replace("[MODULENAME]", hdnModulename.Value);
                            }

                            if (EmailSubject.Contains("[COMMENT]"))
                            {
                                EmailSubject = EmailSubject.Replace("[COMMENT]", REASON);
                            }

                            if (EmailSubject.Contains("[SYSTEM]"))
                            {
                                EmailSubject = EmailSubject.Replace("[SYSTEM]", dsSystem.Tables[0].Rows[0]["SystemName"].ToString());
                            }

                            if (EmailSubject.Contains("[COMMENT]"))
                            {
                                EmailSubject = EmailSubject.Replace("[COMMENT]", txtReason.Text);
                            }

                            if (EmailSubject.Contains("[USER]"))
                            {
                                EmailSubject = EmailSubject.Replace("[USER]", Session["User_Name"].ToString());
                            }

                            if (EmailSubject.Contains("[DATETIME]"))
                            {
                                EmailSubject = EmailSubject.Replace("[DATETIME]", comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                            }

                            if (EmailSubject.Contains("[") && EmailSubject.Contains("]"))
                            {
                                if (ds.Tables.Count > 1)
                                {
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        foreach (DataColumn dc in ds.Tables[1].Columns)
                                        {
                                            if (EmailSubject.Contains("[" + dc.ToString() + "]"))
                                            {
                                                EmailSubject = EmailSubject.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (EmailBody.Contains("[") && EmailBody.Contains("]"))
                        {
                            if (EmailBody.Contains("[MODULENAME]"))
                            {
                                EmailBody = EmailBody.Replace("[MODULENAME]", hdnModulename.Value);
                            }

                            if (EmailBody.Contains("[SYSTEM]"))
                            {
                                EmailBody = EmailBody.Replace("[SYSTEM]", dsSystem.Tables[0].Rows[0]["SystemName"].ToString());
                            }

                            if (EmailBody.Contains("[COMMENT]"))
                            {
                                EmailBody = EmailBody.Replace("[COMMENT]", REASON);
                            }

                            if (EmailBody.Contains("[PROJECTID]"))
                            {
                                EmailBody = EmailBody.Replace("[PROJECTID]", Convert.ToString(Session["PROJECTIDTEXT"]));
                            }

                            if (EmailBody.Contains("[USER]"))
                            {
                                EmailBody = EmailBody.Replace("[USER]", Session["User_Name"].ToString());
                            }

                            if (EmailBody.Contains("[DATETIME]"))
                            {
                                EmailBody = EmailBody.Replace("[DATETIME]", comm.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy hh:mm tt"));
                            }

                            if (EmailBody.Contains("[") && EmailBody.Contains("]"))
                            {
                                if (ds.Tables.Count > 1)
                                {
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        foreach (DataColumn dc in ds.Tables[1].Columns)
                                        {
                                            if (EmailBody.Contains("[" + dc.ToString() + "]"))
                                            {
                                                EmailBody = EmailBody.Replace("[" + dc.ToString() + "]", ds.Tables[1].Rows[0][dc].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        comm.Email_Users(EMAILID, EMAIL_CC, EmailSubject, EmailBody, EMAIL_BCC);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
                throw;

            }
        }
    }
}