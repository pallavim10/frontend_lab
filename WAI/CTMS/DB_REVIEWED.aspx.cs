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
    public partial class DB_REVIEWED : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_ID"] == null)
            {
                Response.Redirect("SessionExpired.aspx", false);
            }

            if (!Page.IsPostBack)
            {
                GetModule();
            }
        }

        public void GetModule()
        {
            try
            {
                DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_MODULES_FOR_FREEZING");

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

        protected void grd_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lbtnFreeze = (LinkButton)e.Row.FindControl("lbtnFreeze");
                    Label lblFrrezeStatus = (Label)e.Row.FindControl("lblFrrezeStatus");
                    string MODULEID = dr["ID"].ToString();

                    DataSet ds = dal_DB.DB_REVIEW_SP(ACTION: "GET_REVIEWED_COUNT", MODULEID: MODULEID);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["COUNTS"].ToString() == "1")
                        {
                            lbtnFreeze.Visible = true;
                            lblFrrezeStatus.Visible = false;
                        }
                        else
                        {
                            lbtnFreeze.Visible = false;
                            lblFrrezeStatus.Visible = true;
                            lblFrrezeStatus.Text = "Review Pending";
                        }
                    }
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

        protected void lbtnFreeze_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                string MODULEID = (gvr.FindControl("ID") as Label).Text;
                string MODULENAME = (gvr.FindControl("MODULENAME") as Label).Text;
                hdnModuleid.Value = MODULEID;
                hdnModulename.Value = MODULENAME;

                dal_DB.DB_REVIEW_SP(ACTION: "UPDATE_FREEZE_STATUS",
                    MODULEID: MODULEID
                    );

                SendEmail("Frozen", "");

                string MSG = MODULENAME + " has been Frozen.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + MSG + "'); window.location.href = '" + Request.RawUrl.ToString() + "' ", true);
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