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
    public partial class DE_EDIT_L2 : System.Web.UI.Page
    {
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
                    lblSUBSCRID.Text = Session["Subject ID"].ToString();
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

        protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            GET_SUBJECT();
        }

        protected void lbtnGETDATA_Click(object sender, EventArgs e)
        {
            GET_DATA();
            divRecord.Visible = true;
        }

        private void GET_DATA()
        {
            DataSet ds = Dal_DE.DATA_ENTRYLIST_SP(
                ACTION: "GET_DATA_L2_Update",
                SITEID: drpSite.SelectedValue,
                SUBJID: drpSubject.SelectedValue,
                SID: txtSpecimenID.Text);

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                Grid_Data.DataSource = ds.Tables[0];
                Grid_Data.DataBind();
            }
            else
            {
                Grid_Data.DataSource = null;
                Grid_Data.DataBind();
                divRecord.Visible = false;
            }
        }

        protected void Grid_Data_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EDITED")
                {
                    Response.Redirect("DATA_ENTRY_L2.aspx?ID=" + e.CommandArgument.ToString() + "&TYPE=Edit");
                }
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
                gv.HeaderRow.Cells[3].Text = Session["Subject ID"].ToString();
                if (Session["SID_ACTIVE"].ToString() == "False")
                {
                    gv.HeaderRow.Cells[5].Visible = false;
                    e.Row.Cells[5].Visible = false;
                }

            }
        }
    }
}