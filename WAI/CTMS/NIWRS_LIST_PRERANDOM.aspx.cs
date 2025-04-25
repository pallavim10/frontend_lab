using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;

namespace CTMS
{
    public partial class NIWRS_LIST_PRERANDOM : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["PREV_URL"] = Request.RawUrl.ToString();

                    GET_SITE();
                    GET_SUBSITE();
                    GET_LIST();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_AddFields(string ID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_LIST_SP
                            (
                            ACTION: "GET_LIST_ADDFIELD_OnClick",
                            LISTID: ID
                            );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["FIELDNAME"].ToString() == "Screening Id")
                    {
                        ds.Tables[0].Rows[0]["FIELDNAME"] = Session["SUBJECTTEXT"].ToString();
                    }
                }

                ViewState["AddFieldsDT"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LIST()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_LIST_SP(ACTION: "GET_LIST", ID: Request.QueryString["STEPID"].ToString(), SITEID: ddlSite.SelectedValue, SUBSITEID: ddlSubSite.SelectedValue);

                lblHeader.Text = ds.Tables[0].Rows[0]["HEADER"].ToString();
                hfSTEPID.Value = Request.QueryString["STEPID"].ToString();

                GET_AddFields(ds.Tables[0].Rows[0]["SOURCE_ID"].ToString());
                GET_LIST_DETAILS(ds.Tables[0].Rows[0]["SOURCE_ID"].ToString());

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LIST_DETAILS(string SOURCE_ID)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_LIST_SP(ACTION: "GET_LIST_DETAILS_PRERANDOM", LISTID: SOURCE_ID, SITEID: ddlSite.SelectedValue, SUBSITEID: ddlSubSite.SelectedValue, ENTEREDBY: Session["User_ID"].ToString());
                ds.Tables[0].Columns["Screening Id"].ColumnName = Session["SUBJECTTEXT"].ToString();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridData.DataSource = ds;
                    gridData.DataBind();
                }
                else
                {
                    gridData.DataSource = null;
                    gridData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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

        string[] gridDataFields;
        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string Headers = null;
                foreach (TableCell RowCell in e.Row.Cells)
                {
                    if (Headers == null)
                    {
                        Headers = RowCell.Text;
                    }
                    else
                    {
                        Headers = Headers + "," + RowCell.Text;
                    }
                }

                gridDataFields = Headers.Split(',');
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                DataTable AddFieldsDT = (DataTable)ViewState["AddFieldsDT"];

                foreach (DataRow dr in AddFieldsDT.Rows)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        if (gridDataFields[i].ToString() == dr["FIELDNAME"].ToString())
                        {
                            e.Row.Cells[i].Attributes.Add("onclick", dr["OnClick"].ToString());
                            e.Row.Cells[i].CssClass = e.Row.Cells[i].CssClass + " fontBlue";
                        }
                    }
                }
            }
        }

        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                        ddlSite.Items.Insert(0, new ListItem("--All--", "-1"));
                    }
                    else
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                    }
                }
                else
                {
                    ddlSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SUBSITE()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_SUBSITE", SITEID: ddlSite.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        ddlSubSite.DataSource = ds.Tables[0];
                        ddlSubSite.DataValueField = "SubSiteID";
                        ddlSubSite.DataTextField = "SubSiteID";
                        ddlSubSite.DataBind();
                        ddlSubSite.Items.Insert(0, new ListItem("--All--", "-1"));
                    }
                    else
                    {
                        ddlSubSite.DataSource = ds.Tables[0];
                        ddlSubSite.DataValueField = "SubSiteID";
                        ddlSubSite.DataTextField = "SubSiteID";
                        ddlSubSite.DataBind();
                    }
                }
                else
                {
                    ddlSubSite.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBSITE();
                GET_LIST();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSubSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_LIST();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}