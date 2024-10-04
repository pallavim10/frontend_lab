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
    public partial class SDVPage : System.Web.UI.Page
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

                ex.ToString();
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
                DataSet ds = Dal_MF.SDV_SP(
                ACTION: "GET_DATA_SDV",
                SITEID: drpSite.SelectedValue,
                SUBJID: drpSubject.SelectedValue,
                SID: txtSpecimenID.Text);

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    Grid_Data.DataSource = ds.Tables[0];
                    Grid_Data.DataBind();
                    divRecord.Visible = true;
                    btnSDV.Visible = true;
                }
                else
                {
                    Grid_Data.DataSource = null;
                    Grid_Data.DataBind();
                    divRecord.Visible = false;
                    btnSDV.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnSDV_Click(object sender, EventArgs e)
        {
            try
            {
                bool SDV = false;
                foreach (GridViewRow row in Grid_Data.Rows)
                {
                    CheckBox checkBox = (CheckBox)row.FindControl("chkSelect");
                    if (checkBox.Checked)
                    {
                        Label hfID = (Label)row.FindControl("lblID");
                        string SID = hfID.Text;

                        DataSet ds = Dal_MF.SDV_SP(ACTION: "PERFORM_SDV", SID: SID);

                        SDV = true;
                    }

                }
                if (SDV == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", @"
                    swal({
                        title: 'Success!',
                        text: 'SDV Performed Successfully.',
                        icon: 'success',
                        button: 'OK'
                    }).then(function(){
                                     window.location.href = window.location.href; });", true);
                    GET_DATA();
                }
                else
                {
                    GET_DATA();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void Grid_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = (sender as GridView);

                gv.HeaderRow.Cells[4].Visible = false;
                e.Row.Cells[4].Visible = false;

                gv.HeaderRow.Cells[5].Visible = false;
                e.Row.Cells[5].Visible = false;

                gv.HeaderRow.Cells[6].Visible = false;
                e.Row.Cells[6].Visible = false;

                gv.HeaderRow.Cells[7].Visible = false;
                e.Row.Cells[7].Visible = false;
            }
        }
    }
}