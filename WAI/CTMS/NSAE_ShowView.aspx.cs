using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class NSAE_ShowView : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["PREV_URL"] = Request.RawUrl.ToString();

                    lblHeader.Text = Request.QueryString["HEADER"].ToString();

                    if (Request.QueryString["TYPE"] != null)
                    {
                        if (Request.QueryString["TYPE"].ToString().Contains("OnlySite"))
                        {
                            divSubject.Visible = false;
                        }

                        if (Request.QueryString["TYPE"].ToString().Contains("NoDownload"))
                        {
                            divDownload.Visible = false;
                        }

                        if (Request.QueryString["TYPE"].ToString().Contains("OnlySubject"))
                        {
                            divSAEID.Visible = false;
                        }

                        if (Request.QueryString["TYPE"].ToString().Contains("NoFilter"))
                        {
                            divSIte.Visible = false;
                            divSubject.Visible = false;
                            divSAEID.Visible = false;
                        }
                    }

                    GET_SITE();
                    GET_SUBJECTS();
                    GET_DATA();
                    GET_SAEID();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SAEID()
        {
            try
            {
                DataSet ds = dal.SAE_ADD_UPDATE_NEW(ACTION: "GET_SAEIDS_REPORTS", SUBJECTID: ddlSUBJID.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSAEID.DataSource = ds.Tables[0];
                    drpSAEID.DataValueField = "SAEID";
                    drpSAEID.DataTextField = "SAEID";
                    drpSAEID.DataBind();
                    drpSAEID.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    drpSAEID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_DATA()
        {
            try
            {
                string ViewName = Request.QueryString["VIEWNAME"].ToString();

                string TYPE = Request.QueryString["TYPE"].ToString();

                DataSet ds = dal.SAE_SHOW_VIEW_SP(VIEWNAME: ViewName, SITEID: ddlSite.SelectedValue, SUBJID: ddlSUBJID.SelectedValue, USERID: Session["User_ID"].ToString(), TYPE: TYPE, SAEID: drpSAEID.SelectedValue);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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

        private void GET_SITE()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 1)
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
                DataSet ds = dal.SAE_ADD_UPDATE(ACTION: "GET_SAE_SUBJECTS", INVID: ddlSite.SelectedValue);

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

        protected void ddlSUBJID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_SAEID();

                GET_DATA();
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
                GET_SUBJECTS();

                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSAEID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_DATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSingleSheet();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportSingleSheet()
        {
            try
            {
                string ViewName = Request.QueryString["VIEWNAME"].ToString();

                string TYPE = Request.QueryString["TYPE"].ToString();

                DataSet ds = dal.SHOW_VIEW_SP(VIEWNAME: ViewName, SITEID: ddlSite.SelectedValue, SUBJID: ddlSUBJID.SelectedValue, USERID: Session["User_ID"].ToString(), TYPE: TYPE);

                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public bool ISDATE(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsNumeric(string value)
        {
            try
            {
                int number;
                bool result = int.TryParse(value, out number);
                return result;
            }
            catch (Exception ex) { return false; }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToPDF();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportToPDF()
        {
            try
            {
                string ViewName = Request.QueryString["VIEWNAME"].ToString();

                string TYPE = Request.QueryString["TYPE"].ToString();

                DataSet ds = dal.SHOW_VIEW_SP(VIEWNAME: ViewName, SITEID: ddlSite.SelectedValue, SUBJID: ddlSUBJID.SelectedValue, USERID: Session["User_ID"].ToString(), TYPE: TYPE);
                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}