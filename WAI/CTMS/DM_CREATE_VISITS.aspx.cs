using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class DM_CREATE_VISITS : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                {
                    GetVisit();
                    GET_VISITS_DEPENDENCY();
                    GET_VISITS_WISE_MODULE();
                    GET_VISITS_WISE_MODULES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GET_VISITS_DEPENDENCY()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISITS_DEPENDENCY");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpVisit.DataSource = ds.Tables[0];
                    drpVisit.DataValueField = "VISITNUM";
                    drpVisit.DataTextField = "VISIT";
                    drpVisit.DataBind();
                    drpVisit.Items.Insert(0, new ListItem("--Select--", "0"));
                    drpVisit.Items.Insert(1, new ListItem("IWRS", "IWRS"));
                    drpProgTrackerVisits.DataSource = ds.Tables[0];
                    drpProgTrackerVisits.DataValueField = "VISITNUM";
                    drpProgTrackerVisits.DataTextField = "VISIT";
                    drpProgTrackerVisits.DataBind();
                    drpProgTrackerVisits.Items.Insert(0, new ListItem("--Select--", "0"));
                    drpProgTrackerVisits.Items.Insert(1, new ListItem("IWRS", "IWRS"));
                }
                else
                {
                    drpVisit.Items.Clear();
                    drpProgTrackerVisits.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GetVisit()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdVisit.DataSource = ds.Tables[0];
                    grdVisit.DataBind();

                }
                else
                {
                    grdVisit.DataSource = null;
                    grdVisit.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmitSectionVisit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVisitID.Text.Trim() != "" && txtVisitName.Text.Trim() != "")
                {
                    DataSet dsvisit = dal_DB.DB_VISIT_SP(ACTION: "CHECK_VISITNUM_VISIT", VISITNUM: txtVisitID.Text, VISIT: txtVisitName.Text);

                    if (dsvisit.Tables.Count > 0 && dsvisit.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + dsvisit.Tables[0].Rows[0]["MSG"].ToString() + "');", true);
                    }
                    else
                    {

                        if (chkUnschedule.Checked == true)
                        {
                            DataSet ds_UnsVisit = dal_DB.DB_VISIT_SP(ACTION: "CHECK_UNSC_VISIT_COUNT");

                            if (ds_UnsVisit.Tables.Count > 0 && ds_UnsVisit.Tables[0].Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have already made one unscheduled visit.');", true);
                            }
                            else
                            {
                                DataSet ds = dal_DB.DB_VISIT_SP(
                                    ACTION: "INSERT_VISIT",
                                    PROJECTID: Session["PROJECTID"].ToString(),
                                    VISITNUM: txtVisitID.Text,
                                    VISIT: txtVisitName.Text,
                                    Unscheduled: chkUnschedule.Checked,
                                    Repeat: chkRepeat.Checked,
                                    SUBJID_PROG: chkProgTracker.Checked,
                                    PUBLISH_DM: chkPublish_DM.Checked,
                                    PUBLISH_eSOURCE: chkPublish_eSource.Checked,
                                    WINDOW: txtWindow.Text.Replace(" ", string.Empty),
                                    EARLY: txtEarly.Text.Replace(" ", string.Empty),
                                    LATE: txtLate.Text.Replace(" ", string.Empty),
                                    VISITNUM_DEP: drpVisit.SelectedValue.Trim(),
                                    MODULEID_DEP: drpModule.SelectedValue.Trim(),
                                    FIELDID_DEP: drpField.SelectedValue.Trim(),
                                    VISITNUM_PROG: drpProgTrackerVisits.SelectedValue.Trim(),
                                    MODULEID_PROG: drpProgTrackerModule.SelectedValue.Trim(),
                                    FIELDID_PROG: drpProgTrackerField.SelectedValue.Trim()
                                    );

                                Response.Write("<script> alert('Visit Created Successfully.'); window.location.href='DM_CREATE_VISITS.aspx' </script>");
                            }
                        }
                        else
                        {
                            DataSet ds = dal_DB.DB_VISIT_SP(
                                ACTION: "INSERT_VISIT",
                                PROJECTID: Session["PROJECTID"].ToString(),
                                VISITNUM: txtVisitID.Text,
                                VISIT: txtVisitName.Text,
                                Unscheduled: chkUnschedule.Checked,
                                Repeat: chkRepeat.Checked,
                                SUBJID_PROG: chkProgTracker.Checked,
                                PUBLISH_DM: chkPublish_DM.Checked,
                                PUBLISH_eSOURCE: chkPublish_eSource.Checked,
                                WINDOW: txtWindow.Text.Replace(" ", string.Empty),
                                EARLY: txtEarly.Text.Replace(" ", string.Empty),
                                LATE: txtLate.Text.Replace(" ", string.Empty),
                                VISITNUM_DEP: drpVisit.SelectedValue.Trim(),
                                MODULEID_DEP: drpModule.SelectedValue.Trim(),
                                FIELDID_DEP: drpField.SelectedValue.Trim(),
                                VISITNUM_PROG: drpProgTrackerVisits.SelectedValue.Trim(),
                                MODULEID_PROG: drpProgTrackerModule.SelectedValue.Trim(),
                                FIELDID_PROG: drpProgTrackerField.SelectedValue.Trim()
                                );

                            Response.Write("<script> alert('Visit Created Successfully.'); window.location.href='DM_CREATE_VISITS.aspx' </script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnupdateSectionVisit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVisitID.Text.Trim() != "" && txtVisitName.Text.Trim() != "")
                {
                    DataSet dsvisit = dal_DB.DB_VISIT_SP(ACTION: "CHECK_VISITNUM_VISIT", VISITNUM: txtVisitID.Text, VISIT: txtVisitName.Text, ID: Session["VISITID"].ToString());

                    if (dsvisit.Tables.Count > 0 && dsvisit.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + dsvisit.Tables[0].Rows[0]["MSG"].ToString() + "');", true);
                    }
                    else
                    {

                        if (chkUnschedule.Checked == true)
                        {
                            DataSet ds_UnsVisit = dal_DB.DB_VISIT_SP(ACTION: "CHECK_UNSC_VISIT_COUNT", ID: Session["VISITID"].ToString());

                            if (ds_UnsVisit.Tables.Count > 0 && ds_UnsVisit.Tables[0].Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have already made one unscheduled visit.');", true);
                            }
                            else
                            {
                                DataSet ds = dal_DB.DB_VISIT_SP(
                                ACTION: "UPDATE_VISIT",
                                PROJECTID: Session["PROJECTID"].ToString(),
                                VISITNUM: txtVisitID.Text,
                                VISIT: txtVisitName.Text,
                                Unscheduled: chkUnschedule.Checked,
                                Repeat: chkRepeat.Checked,
                                SUBJID_PROG: chkProgTracker.Checked,
                                ID: Session["VISITID"].ToString(),
                                PUBLISH_DM: chkPublish_DM.Checked,
                                PUBLISH_eSOURCE: chkPublish_eSource.Checked,
                                WINDOW: txtWindow.Text.Replace(" ", string.Empty),
                                EARLY: txtEarly.Text.Replace(" ", string.Empty),
                                LATE: txtLate.Text.Replace(" ", string.Empty),
                                VISITNUM_DEP: drpVisit.SelectedValue.Trim(),
                                MODULEID_DEP: drpModule.SelectedValue.Trim(),
                                FIELDID_DEP: drpField.SelectedValue.Trim(),
                                VISITNUM_PROG: drpProgTrackerVisits.SelectedValue.Trim(),
                                MODULEID_PROG: drpProgTrackerModule.SelectedValue.Trim(),
                                FIELDID_PROG: drpProgTrackerField.SelectedValue.Trim()
                                );

                                Response.Write("<script> alert('Visit Updated Successfully.'); window.location.href='DM_CREATE_VISITS.aspx' </script>");
                            }
                        }
                        else
                        {
                            DataSet ds = dal_DB.DB_VISIT_SP(
                            ACTION: "UPDATE_VISIT",
                            PROJECTID: Session["PROJECTID"].ToString(),
                            VISITNUM: txtVisitID.Text,
                            VISIT: txtVisitName.Text,
                            Unscheduled: chkUnschedule.Checked,
                            Repeat: chkRepeat.Checked,
                            SUBJID_PROG: chkProgTracker.Checked,
                            ID: Session["VISITID"].ToString(),
                            PUBLISH_DM: chkPublish_DM.Checked,
                            PUBLISH_eSOURCE: chkPublish_eSource.Checked,
                            WINDOW: txtWindow.Text.Replace(" ", string.Empty),
                            EARLY: txtEarly.Text.Replace(" ", string.Empty),
                            LATE: txtLate.Text.Replace(" ", string.Empty),
                            VISITNUM_DEP: drpVisit.SelectedValue.Trim(),
                            MODULEID_DEP: drpModule.SelectedValue.Trim(),
                            FIELDID_DEP: drpField.SelectedValue.Trim(),
                            VISITNUM_PROG: drpProgTrackerVisits.SelectedValue.Trim(),
                            MODULEID_PROG: drpProgTrackerModule.SelectedValue.Trim(),
                            FIELDID_PROG: drpProgTrackerField.SelectedValue.Trim()
                            );

                            Response.Write("<script> alert('Visit Updated Successfully.'); window.location.href='DM_CREATE_VISITS.aspx' </script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancelSectionVisit_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdVisit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();

                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                Session["VISITID"] = id;
                if (e.CommandName == "EditVisit")
                {
                    DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT_BYID",
                        ID: Session["VISITID"].ToString()
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtVisitID.Text = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                        txtVisitName.Text = ds.Tables[0].Rows[0]["VISIT"].ToString();

                        txtWindow.Text = ds.Tables[0].Rows[0]["WINDOW"].ToString();
                        txtEarly.Text = ds.Tables[0].Rows[0]["EARLY"].ToString();
                        txtLate.Text = ds.Tables[0].Rows[0]["LATE"].ToString();
                        GET_VISITS_DEPENDENCY();
                        if (ds.Tables[0].Rows[0]["VISITNUM_DEP"].ToString() != "")
                        {
                            drpVisit.SelectedValue = ds.Tables[0].Rows[0]["VISITNUM_DEP"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["VISITNUM_PROG"].ToString() != "")
                        {
                            drpProgTrackerVisits.SelectedValue = ds.Tables[0].Rows[0]["VISITNUM_PROG"].ToString();
                        }
                        GET_VISITS_WISE_MODULE();
                        if (ds.Tables[0].Rows[0]["MODULEID_DEP"].ToString() != "")
                        {
                            drpModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID_DEP"].ToString();
                        }
                        GET_VISITS_WISE_MODULES();
                        if (ds.Tables[0].Rows[0]["MODULEID_PROG"].ToString() != "")
                        {
                            drpProgTrackerModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID_PROG"].ToString();
                        }
                        GET_VISIT_WISE_FIELD();
                        if (ds.Tables[0].Rows[0]["FIELDID_DEP"].ToString() != "")
                        {
                            drpField.SelectedValue = ds.Tables[0].Rows[0]["FIELDID_DEP"].ToString();
                        }
                        GET_VISIT_WISE_FIELDS();
                        if (ds.Tables[0].Rows[0]["FIELDID_PROG"].ToString() != "")
                        {
                            drpProgTrackerField.SelectedValue = ds.Tables[0].Rows[0]["FIELDID_PROG"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["Unscheduled"].ToString() == "True")
                        {
                            chkUnschedule.Checked = true;
                        }
                        else
                        {
                            chkUnschedule.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["Repeat"].ToString() == "True")
                        {
                            chkRepeat.Checked = true;
                        }
                        else
                        {
                            chkRepeat.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["PUBLISH_DM"].ToString() == "True")
                        {
                            chkPublish_DM.Checked = true;
                        }
                        else
                        {
                            chkPublish_DM.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0]["PUBLISH_eSOURCE"].ToString() == "True")
                        {
                            chkPublish_eSource.Checked = true;
                        }
                        else
                        {
                            chkPublish_eSource.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["SUBJID_PROG"].ToString() == "True")
                        {
                            chkProgTracker.Checked = true;
                        }
                        else
                        {
                            chkProgTracker.Checked = false;
                        }

                        btnupdateSectionVisit.Visible = true;
                        btnsubmitSectionVisit.Visible = false;
                    }
                }
                else if (e.CommandName == "DeleteVisit")
                {
                    DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "DELETE_VISIT",
                        ID: Session["VISITID"].ToString()
                        );

                    //Response.Write("<script> alert('Visit deleted successfully.'); window.location.href='DM_CREATE_VISITS.aspx';</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage",
    "alert('Visit Deleted successfully.'); window.location.href='DM_CREATE_VISITS.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdVisit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string COUNT = dr["COUNT"].ToString();
                    LinkButton lbtndeleteSection = (LinkButton)e.Row.FindControl("lbtndeleteSection");

                    if (COUNT != "0")
                    {
                        lbtndeleteSection.Visible = false;
                    }
                    else
                    {
                        lbtndeleteSection.Visible = true;
                    }

                    LinkButton lbtnSetCriteria = (LinkButton)e.Row.FindControl("lbtnSetCriteria");
                    if (dr["Unscheduled"].ToString() == "True")
                    {
                        lbtnSetCriteria.Visible = false;
                    }

                    HtmlControl iconunscheduled = (HtmlControl)e.Row.FindControl("iconunscheduled");
                    if (dr["Unscheduled"].ToString() == "True")
                    {
                        iconunscheduled.Attributes.Add("class", "fa fa-check");
                        iconunscheduled.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconunscheduled.Attributes.Add("class", "fa fa-times");
                        iconunscheduled.Attributes.Add("style", "color: red;");
                    }

                    HtmlControl iconsubjidprog = (HtmlControl)e.Row.FindControl("iconsubjidprog");
                    if (dr["SUBJID_PROG"].ToString() == "True")
                    {
                        iconsubjidprog.Attributes.Add("class", "fa fa-check");
                        iconsubjidprog.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconsubjidprog.Attributes.Add("class", "fa fa-times");
                        iconsubjidprog.Attributes.Add("style", "color: red;");
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdVisit_PreRender(object sender, EventArgs e)
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

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISITS_EXPORT");

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Visit Master List_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

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

        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_VISITS_WISE_MODULE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_VISITS_WISE_MODULE()
        {
            try
            {
                DataSet ds = null;
                if(drpVisit.SelectedValue == "IWRS") 
                {
                   ds =  dal_DB.DB_VISIT_SP(ACTION: "GET_VISITS_WISE_MODULE", VISITNUM: drpVisit.SelectedValue); 
                }
                else
                {
                    ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISITS_WISE_MODULE");
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpModule.DataSource = ds.Tables[0];
                    drpModule.DataValueField = "MODULEID";
                    drpModule.DataTextField = "MODULENAME";
                    drpModule.DataBind();
                    drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_VISIT_WISE_FIELD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_VISIT_WISE_FIELD()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT_WISE_FIELD", ID: drpModule.SelectedValue);
                //DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT_WISE_FIELD", VISITNUM: drpVisit.SelectedValue, ID: drpModule.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpField.DataSource = ds.Tables[0];
                    drpField.DataValueField = "FIELD_ID";
                    drpField.DataTextField = "FIELDNAME";
                    drpField.DataBind();
                    drpField.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpField.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        protected void drpProgVisits_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_VISITS_WISE_MODULES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void GET_VISITS_WISE_MODULES()
        {
            try
            {
                DataSet ds = null;
                if (drpProgTrackerVisits.SelectedValue == "IWRS")
                {
                    ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISITS_WISE_MODULE", VISITNUM: drpProgTrackerVisits.SelectedValue);
                }
                else
                {
                    ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISITS_WISE_MODULE");
                }
                //DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISITS_WISE_MODULE", VISITNUM: drpProgTrackerVisits.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpProgTrackerModule.DataSource = ds.Tables[0];
                    drpProgTrackerModule.DataValueField = "MODULEID";
                    drpProgTrackerModule.DataTextField = "MODULENAME";
                    drpProgTrackerModule.DataBind();
                    drpProgTrackerModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpProgTrackerModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void drpProgModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_VISIT_WISE_FIELDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_VISIT_WISE_FIELDS()
        {
            try
            {
                DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT_WISE_FIELDS", ID: drpProgTrackerModule.SelectedValue);
                //DataSet ds = dal_DB.DB_VISIT_SP(ACTION: "GET_VISIT_WISE_FIELD", VISITNUM: drpProgTrackerVisits.SelectedValue, ID: drpProgTrackerModule.SelectedValue);
                if (ds != null && ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0 && drpProgTrackerModule.SelectedValue != "0")
                {
                    drpProgTrackerField.DataSource = ds.Tables[0];
                    drpProgTrackerField.DataValueField = "FIELD_ID";
                    drpProgTrackerField.DataTextField = "FIELDNAME";
                    drpProgTrackerField.DataBind();
                    drpProgTrackerField.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpProgTrackerField.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}