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
                    GET_SITE();
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
                ex.ToString();
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
                ex.ToString();
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
                SITEID: drpSite.SelectedValue);

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
                ex.Message.ToString();
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
                    Label lblALIQUOTTYPE = row.FindControl("lblALIQUOTTYPE") as Label;
                    Label lblFROMDAT = row.FindControl("lblFROMDAT") as Label;
                    Label lblTODAT = row.FindControl("lblTODAT") as Label;
                    Label lblTOTAL_SID = row.FindControl("lblTOTAL_SID") as Label;
                    Label lblTOTAL_ALQ = row.FindControl("lblTOTAL_ALQ") as Label;
                    Label lblSHIPMENTDAT = row.FindControl("lblSHIPMENTDAT") as Label;
                    Label lblAWBNUM = row.FindControl("lblAWBNUM") as Label;

                    hdnID.Value = ID;
                    modalSiteID.Text = lblsiteid.Text;
                    modalVisit.Text = lblVisit.Text;
                    modalSPECTYP.Text = lblSPECTYP.Text;
                    modalALIQUOTTYPE.Text = lblALIQUOTTYPE.Text;
                    modalFROMDAT.Text = lblFROMDAT.Text;
                    modalTODAT.Text = lblTODAT.Text;
                    modalSID.Text = lblTOTAL_SID.Text;
                    modalALQS.Text = lblTOTAL_ALQ.Text;
                    txtSHIPMENTDAT.Text = lblSHIPMENTDAT.Text;
                    txtAWBNUM.Text = lblAWBNUM.Text;

                    ModalShipment.Show();
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
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
                ex.ToString();
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
                ex.ToString();
            }
        }

    }
}