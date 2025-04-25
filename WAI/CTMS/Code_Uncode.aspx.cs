using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class Code_Uncode : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    BIND_FORM();
                    GET_SITE();

                    if (Session["CODE_MODULEID"] == null && Session["CODE_SITEID"] == null && Session["CODE_SUBJID"] == null)
                    {
                        drpForm.SelectedValue = "";
                        ddlSUBJID.SelectedValue = "";
                        ddlSite.SelectedValue = "";
                    }
                    else
                    {
                        drpForm.SelectedValue = Session["CODE_MODULEID"].ToString();
                        GET_SITE();
                        ddlSite.SelectedValue = Session["CODE_SITEID"].ToString();
                        GET_SUBJECTS();
                        ddlSUBJID.SelectedValue = Session["CODE_SUBJID"].ToString();
                        SITESUBJ.Visible = true;
                        divSIte.Visible = true;
                        divSubject.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void BIND_FORM()
        {
            try
            {
                DataSet dt = dal.DB_CODE_SP(
                       ACTION: "GET_MODULENAME"
                       );

                if (dt.Tables[0].Rows.Count > 0)
                {
                    drpForm.DataSource = dt;
                    drpForm.DataTextField = "MODULENAME";
                    drpForm.DataValueField = "ID";
                    drpForm.DataBind();
                    drpForm.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSite.DataSource = ds.Tables[0];
                        ddlSite.DataValueField = "INVNAME";
                        ddlSite.DataBind();
                        ddlSite.Items.Insert(0, new ListItem("--All--", "0"));
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
        private void GET_SUBJECTS()
        {
            try
            {
                DataSet ds = dal.GET_SUBJECT_SP(INVID: ddlSite.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSUBJID.DataSource = ds.Tables[0];
                    ddlSUBJID.DataValueField = "SUBJID";
                    ddlSUBJID.DataTextField = "SUBJID";
                    ddlSUBJID.DataBind();
                    ddlSUBJID.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    ddlSUBJID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SUBJECTS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void drpForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpForm.SelectedValue != "0")
                {
                    SITESUBJ.Visible = true;
                    divSIte.Visible = true;
                    divSubject.Visible = true;
                }
                else
                {
                    SITESUBJ.Visible = false;
                    divSIte.Visible = false;
                    divSubject.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }
        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        public void GetData()
        {
            try
            {
                DataSet dt = dal.DB_CODE_SP(
                ACTION: "GET_UNCODED_DATA",
                MODULEID: drpForm.SelectedValue,
                SUBJID: ddlSUBJID.SelectedValue,
                SITEID: ddlSite.SelectedValue
                );

                Session["CODE_MODULEID"] = drpForm.SelectedValue;
                Session["CODE_SUBJID"] = ddlSUBJID.SelectedValue;
                Session["CODE_SITEID"] = ddlSite.SelectedValue;

                if (dt.Tables[0].Rows.Count > 0)
                {
                    gridData.DataSource = dt;
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
        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[4].CssClass = "disp-none";
                e.Row.Cells[5].CssClass = "disp-none";
                e.Row.Cells[6].CssClass = "disp-none";
                e.Row.Cells[7].CssClass = "disp-none";
                e.Row.Cells[8].CssClass = "disp-none";
                e.Row.Cells[9].CssClass = "disp-none";

                //DataTable InlistEditableDT = (DataTable)ViewState["InlistEditableDT"];
                //DataTable InlistDT = (DataTable)ViewState["InlistDT"];

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    LinkButton lnkQUERYSTATUS = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                    LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                    HtmlControl ADICON = (HtmlControl)e.Row.FindControl("ADICON");

                    string QryCount = dr["QryCount"].ToString();

                    if (QryCount == "0")
                    {
                        lnkQUERYSTATUS.Attributes.Add("class", "disp-none");
                    }

                    LinkButton lnkQueryAns = (LinkButton)e.Row.FindControl("lnkQUERYANS");
                    if (dr["QryAnsCount"].ToString() != "0")
                    {
                        lnkQueryAns.Visible = true;
                    }
                    else
                    {
                        lnkQueryAns.Visible = false;
                    }

                    LinkButton lnkQueryClose = (LinkButton)e.Row.FindControl("lnkQUERYCLOSE");
                    if (dr["QryClosedCount"].ToString() != "0")
                    {
                        lnkQueryClose.Visible = true;
                    }
                    else
                    {
                        lnkQueryClose.Visible = false;
                    }

                    if (dr["AUDITSTATUS"].ToString() == "")
                    {
                        AD.Attributes.Add("class", "disp-none");
                    }
                    else if (dr["AUDITSTATUS"].ToString() != "Initial Entry")
                    {
                        AD.Attributes.Add("class", "");
                        ADICON.Attributes.Add("style", "color: red;font-size: 17px;");
                    }
                    else
                    {
                        AD.Attributes.Add("class", "");
                        ADICON.Attributes.Add("style", "color: green;font-size: 17px;");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Session["CODE_MODULEID"] = drpForm.SelectedValue;
                Session["CODE_SUBJID"] = ddlSUBJID.SelectedValue;
                Session["CODE_SITEID"] = ddlSite.SelectedValue;

                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                int rowIndex = gvr.RowIndex;

                GridViewRow row = gridData.Rows[rowIndex];

                string MODULEID = drpForm.SelectedValue;
                string PVID = row.Cells[4].Text;
                string RECID = row.Cells[5].Text;
                string INVID = row.Cells[10].Text;
                string SUBJID = row.Cells[11].Text;

                if (e.CommandName == "ManualCode")
                {
                     Response.Redirect("Code_ManualCoding.aspx?MODULEID=" + MODULEID + "&PVID=" + PVID + "&RECID=" + RECID);
                }
                else if (e.CommandName == "View")
                {
                    
                    DataSet ds = dal.DB_CODE_SP(ACTION: "GET_DM_SAE_ID", MODULEID: MODULEID, PVID: PVID, RECID: RECID);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string VISITID = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                        string VISIT = ds.Tables[0].Rows[0]["VISIT"].ToString();
                        string MODULENAME = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                        string MULTIPLEYN = ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString();
                        string NewUrl = "";
                        if (MULTIPLEYN == "True")
                        {
                            NewUrl = "DM_DataEntry_MultipleData_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&PVID=" + PVID + "&RECID=" + RECID + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&VISITID=" + VISITID + "&VISIT=" + VISIT;
                        }
                        else
                        {
                            NewUrl = "DM_DataEntry_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID;
                        }
                        string script = "window.open('" + NewUrl + "', '_blank');";
                        ClientScript.RegisterStartupScript(this.GetType(), "OpenInNewTab", script, true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}