using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class DATA_ENTRY : System.Web.UI.Page
    {
        DAL_DE Dal_DE = new DAL_DE();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SpecimenType();
                    GET_SITE();
                    GET_SUBJECT();
                    GET_VISIT();
                    GET_ENTRY_FIELDS();
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }

        private void GET_ENTRY_FIELDS()
        {
            try
            {
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_ENTRY_FIELDS_First");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeatFields.DataSource = ds;
                    repeatFields.DataBind();
                }
                else
                {
                    repeatFields.DataSource = null;
                    repeatFields.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void GET_SpecimenType()
        {
            try
            {
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_SpecimenType", VARIABLENAME: "SPECTYP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSpecimenType.DataSource = ds.Tables[1];
                    drpSpecimenType.DataValueField = "OPTION_VALUE";
                    drpSpecimenType.DataBind();

                    if (ds.Tables[1].Rows.Count > 1)
                    {
                        drpSpecimenType.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }

        private void GET_SITE()
        {
            try
            {
                DataSet ds = Dal_DE.GET_SITEID_SP();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpsite.DataSource = ds.Tables[0];
                    drpsite.DataValueField = "SiteID";
                    drpsite.DataBind();

                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        drpsite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }

        private void GET_SUBJECT()
        {
            try
            {
                DataSet ds = Dal_DE.GET_SUBJID_SP(drpsite.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubject.DataSource = ds.Tables[0];
                    drpSubject.DataValueField = "SUBJID";
                    drpSubject.DataBind();
                }
                drpSubject.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }

        private void GET_VISIT()
        {
            try
            {
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_VISIT");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpVisit.DataSource = ds.Tables[0];
                    drpVisit.DataValueField = "VISITNUM";
                    drpVisit.DataTextField = "VISITNAME";
                    drpVisit.DataBind();
                }
                drpVisit.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void drpsite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBJECT();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_ALIQUOTS();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void GET_ALIQUOTS()
        {
            try
            {
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_ALIQUOT_PREP_First", VISITNUM: drpVisit.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    divAliquotPrep.Visible = true;

                    gridAliquots.DataSource = ds.Tables[1];
                    gridAliquots.DataBind();
                }
                else
                {
                    divAliquotPrep.Visible = false;
                    gridAliquots.DataSource = null;
                    gridAliquots.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void txtSpecimen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "CHECK_SpecimenID", SITEID: drpsite.SelectedValue, SID: txtSpecimen.Text);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Specimen ID already used', 'warning');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid Specimen ID', 'warning');", true);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = Dal_DE.DATA_ENTRY_SP(ACTION: "CHECK_SUBJID", SITEID: drpsite.SelectedValue, SUBJID: drpSubject.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"swal('Warning!', 'Invalid Subject ID', 'warning');", true);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void repeatFields_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView drv = (DataRowView)e.Item.DataItem;

                    DataSet ds = new DataSet();

                    TextBox txtDATA = (TextBox)e.Item.FindControl("txtDATA");
                    DropDownList drpDATA = (DropDownList)e.Item.FindControl("drpDATA");
                    Repeater repeatRadio = (Repeater)e.Item.FindControl("repeatRadio");
                    Repeater repeatChk = (Repeater)e.Item.FindControl("repeatChk");

                    switch (drv["CONTROLTYPE"].ToString().Trim())
                    {
                        case "Textbox":
                            txtDATA.Visible = true;
                            break;

                        case "Multiline Textbox":
                            txtDATA.Visible = true;
                            txtDATA.TextMode = TextBoxMode.MultiLine;
                            break;

                        case "Date":
                            txtDATA.Visible = true;
                            txtDATA.CssClass = txtDATA.CssClass + " txtDate";
                            break;

                        case "Time":
                            txtDATA.Visible = true;
                            txtDATA.CssClass = txtDATA.CssClass + " txtTime";
                            break;

                        case "Dropdown":
                            drpDATA.Visible = true;
                            ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_FIELD_OPTIONS", VARIABLENAME: drv["VARIABLENAME"].ToString().Trim());
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                drpDATA.DataSource = ds.Tables[0];
                                drpDATA.DataValueField = "OPTION_VALUE";
                                drpDATA.DataBind();
                                drpDATA.Items.Insert(0, new ListItem("--Select--", "0"));
                            }

                            break;

                        case "Radio Button":
                            ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_FIELD_OPTIONS", VARIABLENAME: drv["VARIABLENAME"].ToString().Trim());
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                repeatRadio.DataSource = ds.Tables[0];
                                repeatRadio.DataBind();
                            }

                            break;

                        case "Checkbox":
                            ds = Dal_DE.DATA_ENTRY_SP(ACTION: "GET_FIELD_OPTIONS", VARIABLENAME: drv["VARIABLENAME"].ToString().Trim());
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                repeatChk.DataSource = ds.Tables[0];
                                repeatChk.DataBind();
                            }

                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}