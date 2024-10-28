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
    public partial class SHIPMENT_SHIPPED : System.Web.UI.Page
    {
        DAL_DE Dal_DE = new DAL_DE();
        DAL_MF Dal_MF = new DAL_MF();

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
                    GET_ALIQUOT();
                    GET_VISIT();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_ALIQUOT()
        {
            try
            {
                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_ALIQUOT");

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpALIQUOTTYPE.DataSource = ds.Tables[0];
                    drpALIQUOTTYPE.DataValueField = "ALIQUOTTYPE";
                    drpALIQUOTTYPE.DataTextField = "ALIQUOTTYPE";
                    drpALIQUOTTYPE.DataBind();

                    drpALIQUOTTYPE.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_VISIT()
        {
            try
            {
                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_VISIT");

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {

                    drpVISIT.DataSource = ds.Tables[0];
                    drpVISIT.DataValueField = "VISITNUM";
                    drpVISIT.DataTextField = "VISITNAME";
                    drpVISIT.DataBind();

                    drpVISIT.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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

        protected void Grid_Data_PreRender(object sender, EventArgs e)
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
                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(
                ACTION: "GET_SHIPMENT_SHIPPED",
                SITEID: drpSite.SelectedValue,
                VISITNUM: drpVISIT.SelectedValue,
                ALIQUOTTYPE: drpALIQUOTTYPE.SelectedValue,
                FROMDAT: txtFROMDAT.Text,
                TODAT: txtTODAT.Text
                );

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    Grid_Data.DataSource = ds.Tables[0];
                    Grid_Data.DataBind();
                    divRecord.Visible = true;
                }
                else
                {
                    Grid_Data.DataSource = null;
                    Grid_Data.DataBind();
                    divRecord.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void Grid_Data_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = row.RowIndex;

                string ID = e.CommandArgument.ToString();
                string CommandName = e.CommandName.ToString();

                if (CommandName == "SHIP")
                {
                    Label lblsiteid = row.FindControl("lblsiteid") as Label;
                    Label lblVisit = row.FindControl("lblVisit") as Label;
                    Label lblSPECTYP = row.FindControl("lblSPECTYP") as Label;
                    Label lblLABNAME = row.FindControl("lblLABNAME") as Label;
                    Label lblALIQUOTTYPE = row.FindControl("lblALIQUOTTYPE") as Label;
                    Label lblALIQUOTID = row.FindControl("lblALIQUOTID") as Label;
                    Label lblFROMDAT = row.FindControl("lblFROMDAT") as Label;
                    Label lblTODAT = row.FindControl("lblTODAT") as Label;
                    Label lblTOTAL_SID = row.FindControl("lblTOTAL_SID") as Label;
                    Label lblTOTAL_ALQ = row.FindControl("lblTOTAL_ALQ") as Label;

                    hdnID.Value = ID;
                    modalSiteID.Text = lblsiteid.Text;
                    modalVisit.Text = lblVisit.Text;
                    modalSPECTYP.Text = lblSPECTYP.Text;
                    modalLABNAME.Text = lblLABNAME.Text;
                    modalALIQUOTTYPE.Text = lblALIQUOTTYPE.Text;
                    modalALIQUOTID.Text = lblALIQUOTID.Text;
                    modalFROMDAT.Text = lblFROMDAT.Text;
                    modalTODAT.Text = lblTODAT.Text;
                    modalSID.Text = lblTOTAL_SID.Text;
                    modalALQS.Text = lblTOTAL_ALQ.Text;

                    ModalShipment.Show();
                }
                else if (CommandName == "SHOW")
                {
                    DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "SHOW_SHIPMENT_DATA_ByID", SHIPMENTID: e.CommandArgument.ToString());
                    grdDATA.DataSource = ds;
                    grdDATA.DataBind();
                    modalDATA.Show();
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
                Dal_MF.SHIPMENT_MENIFEST_SP(
                ACTION: "CHANGE_MANIFEST",
                SHIPMENTID: hdnID.Value,
                SHIPMENTDAT: txtSHIPMENTDAT.Text,
                AWBNUM: txtAWBNUM.Text,
                REASON: txtReason.Text
                );

                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                        swal({
                            title: 'Success!',
                            text: 'Shipment details updated successfully.',
                            icon: 'success',
                            button: 'OK'
                        }).then(function(){
                                         window.location.href = window.location.href; });
                        ", true);

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ModalShipment.Hide();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        protected void lbtncloseDATA_Click(object sender, EventArgs e)
        {
            try
            {
                modalDATA.Hide();
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
                GET_SHIPMENT_SHIPED_EXPORT();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private void GET_SHIPMENT_SHIPED_EXPORT()
        {
            try
            {
                string xlname = "Shipment Shipped Report";

                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(
                   ACTION: "GET_SHIPMENT_SHIPPED_EXPORT",
                    SITEID: drpSite.SelectedValue,
                    VISITNUM: drpVISIT.SelectedValue,
                    ALIQUOTTYPE: drpALIQUOTTYPE.SelectedValue,
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
                        if (copiedTable.Columns.Contains("Total SIDs"))
                        {
                            copiedTable.Columns.Remove("Total SIDs");
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

        protected void Grid_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                GridView gv = (sender as GridView);

                if (Session["SID_ACTIVE"].ToString() == "False")
                {
                    gv.HeaderRow.Cells[2].Visible = false;
                    e.Row.Cells[2].Visible = false;
                }

            }
        }

        protected void grdDATA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                GridView gv = (sender as GridView);

                gv.HeaderRow.Cells[9].Visible = false;
                e.Row.Cells[9].Visible = false;

                if (Session["SID_ACTIVE"].ToString() == "False")
                {
                    gv.HeaderRow.Cells[2].Visible = false;
                    e.Row.Cells[2].Visible = false;
                }
                gv.HeaderRow.Cells[3].Text = Session["Subject ID"].ToString();
            }
        }
    }
}