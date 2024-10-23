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
    public partial class ACKNOWLEDGEMENT : System.Web.UI.Page
    {
        DAL_MF Dal_MF = new DAL_MF();
        DAL_DE Dal_DE = new DAL_DE();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["SID_ACTIVE"].ToString() == "True")
                    {
                        divSID.Visible = true;
                    }
                    else
                    {
                        divSID.Visible = false;
                    }
                    GET_SITE();
                    GET_SUBJECT();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_SITE()
        {
            try
            {
                DataSet ds = Dal_DE.GET_SITEID_SP();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSite.DataSource = ds.Tables[0];
                    drpSite.DataValueField = "SiteID";
                    drpSite.DataBind();

                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        drpSite.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {

                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_SUBJECT()
        {
            try
            {
                DataSet ds = Dal_DE.GET_SUBJID_SP(drpSite.SelectedValue);
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

                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            GET_SUBJECT();
        }

        protected void GridData_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
                {

                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gv.ShowFooter == true && gv.Rows.Count > 0)
                {

                    gv.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                throw;
            }
        }

        protected void lbtnGETDATA_Click(object sender, EventArgs e)
        {
            GET_DATA();
        }

        private void GET_DATA()
        {
            try
            {
                DataSet ds = Dal_MF.ACKNOWLEDGEMENT_SP(
                ACTION: "GET_DATA_ACKNOWLEDGED",
                SITEID: drpSite.SelectedValue,
                SUBJID: drpSubject.SelectedValue,
                SID: txtSpecimenID.Text,
                FROMDAT: txtFROMDAT.Text,
                TODAT: txtTODAT.Text
                );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    Grid_Data.DataSource = ds.Tables[0];
                    Grid_Data.DataBind();
                    divRecord.Visible = true;
                    btnSubmit.Visible = true;

                }
                else
                {
                    Grid_Data.DataSource = null;
                    Grid_Data.DataBind();
                    divRecord.Visible = false;
                    btnSubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void Grid_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    DropDownList drpAcknowledge = (e.Row.FindControl("drpAcknowledge") as DropDownList);
                    HiddenField HdnAcknowledge = (HiddenField)e.Row.FindControl("HdnAcknowledge");
                    if (HdnAcknowledge.Value == "Yes")
                    {
                        drpAcknowledge.SelectedValue = "Yes";
                    }
                    else if (HdnAcknowledge.Value == "No")
                    {
                        drpAcknowledge.SelectedValue = "No";
                    }

                    GridView gv = (sender as GridView);

                    gv.HeaderRow.Cells[3].Visible = false;
                    e.Row.Cells[3].Visible = false;

                    gv.HeaderRow.Cells[4].Visible = false;
                    e.Row.Cells[4].Visible = false;

                    gv.HeaderRow.Cells[5].Visible = false;
                    e.Row.Cells[5].Visible = false;

                    gv.HeaderRow.Cells[6].Visible = false;
                    e.Row.Cells[6].Visible = false;

                    gv.HeaderRow.Cells[7].Visible = false;
                    e.Row.Cells[7].Visible = false;

                    gv.HeaderRow.Cells[8].Visible = false;
                    e.Row.Cells[8].Visible = false;

                    if (Session["SID_ACTIVE"].ToString() == "False")
                    {
                        gv.HeaderRow.Cells[11].Visible = false;
                        e.Row.Cells[11].Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool Acknowledge = false;
                foreach (GridViewRow row in Grid_Data.Rows)
                {
                    DropDownList drpAcknowledge = (DropDownList)row.FindControl("drpAcknowledge");
                    Label lblID = (Label)row.FindControl("lblID");

                    if (drpAcknowledge.SelectedItem.Text != "")
                    {
                        DataSet ds = Dal_MF.ACKNOWLEDGEMENT_SP(ACTION: "PERFORM_ACKNOWLEDGE",
                            ACKNOWLEDGEMENT: drpAcknowledge.SelectedItem.Text,
                            ID: lblID.Text);
                        Acknowledge = true;
                    }
                }

                if (Acknowledge == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                        swal({
                            title: 'Success!',
                            text: 'Acknowledged successfully.',
                            icon: 'success',
                            button: 'OK'
                        }).then(function(){
                                         window.location.href = window.location.href; });
                        ", true);
                }

                GET_DATA();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                GET_ACKNOWLEDGE_EXPORT();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_ACKNOWLEDGE_EXPORT()
        {
            try
            {
                string xlname = "Acknowledge Details Report";

                DataSet ds = Dal_MF.ACKNOWLEDGEMENT_SP(
                   ACTION: "GET_DATA_ACKNOWLEDGED_EXPORT",
                   SITEID: drpSite.SelectedValue,
                   SUBJID: drpSubject.SelectedValue,
                   SID: txtSpecimenID.Text,
                   FROMDAT: txtFROMDAT.Text,
                   TODAT: txtTODAT.Text
                   );

                DataSet dsExport = new DataSet();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    DataTable copiedTable = ds.Tables[i].Copy();
                    if (Session["SID_ACTIVE"].ToString() == "False")
                    {
                        if (copiedTable.Columns.Contains("Specimen ID"))
                        {
                            copiedTable.Columns.Remove("Specimen ID");
                        }
                    }
                    dsExport.Tables.Add(copiedTable);
                    i++;
                }
                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }
    }
}