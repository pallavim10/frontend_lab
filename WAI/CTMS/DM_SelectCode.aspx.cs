using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Drawing;

namespace CTMS
{
    public partial class DM_SelectCode : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    //BindddlFiter1();
                    lblSUBJID.Text = "SUBJID - " + Request.QueryString["SUBJID"].ToString();
                    lblEVENTCODE.Text = "Event Code - " + Request.QueryString["ECODE"].ToString();
                    lblTERM.Text = "Term - " + Request.QueryString["TERM"].ToString();

                    if (Convert.ToString(Request.QueryString["CodeLIB"]) == "Meddra")
                    {
                        lblDictNameHeader.Text = "MedDRA Codes";
                        lblDictName.Text = "Select MedDRA Codes";
                    }
                    else
                    {
                        lblDictNameHeader.Text = "WHODD Codes";
                        lblDictName.Text = "Select WHODD Codes";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetMedraData()
        {
            try
            {
                DataTable dt = new DataTable();

                if (txtModifiedTerm.Text.Trim() != "")
                {
                    if (Convert.ToString(Request.QueryString["CodeLIB"]) == "Meddra")
                    {
                        dt = dal.GetMedraData(Filter5: txtModifiedTerm.Text, ProjectId: Session["PROJECTID"].ToString());

                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        GridView1.Visible = true;
                        GridView2.Visible = false;
                    }
                    else
                    {
                        dt = dal.Get_WHODData(pt_name: txtModifiedTerm.Text,
                            Filter1: txtModifiedTerm.Text,
                            Filter2: txtModifiedTerm.Text,
                            Filter3: txtModifiedTerm.Text,
                            Filter4: txtModifiedTerm.Text,
                            Filter5: txtModifiedTerm.Text
                            );

                        GridView2.DataSource = dt;
                        GridView2.DataBind();

                        GridView1.Visible = false;
                        GridView2.Visible = true;
                    }
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    GridView2.DataSource = null;
                    GridView2.DataBind();
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

        public void AutoCode(string lltNAME, string ID)
        {
            try
            {
                dal.CODING_SP(
                ACTION: "Uncode_Code",
                PVID: Request.QueryString["PVID"].ToString(),
                RECID: Request.QueryString["RECID"].ToString(),
                MODULEID: Request.QueryString["MODULEID"].ToString(),
                DATA: lltNAME,
                ID: ID,
                SUBJID: Request.QueryString["SUBJID"].ToString(),
                FIELDNAME: txtModifiedTerm.Text
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void txtModifiedTerm_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetMedraData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                string SAE = Convert.ToString(e.CommandArgument);
                if (e.CommandName == "Code")
                {
                    string lblllt_name = ((Label)GridView1.Rows[RowIndex].FindControl("lblllt_name")).Text;
                    AutoCode(lblllt_name, e.CommandArgument.ToString());

                    string PrevURL = Session["prevURL"].ToString();

                    if (PrevURL.ToString().Contains("&INVID="))
                    {
                        string newPrevURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, "") + PrevURL;

                        Uri myUri = new Uri(newPrevURL, UriKind.Absolute);

                        var nameValues = HttpUtility.ParseQueryString(myUri.Query.ToString());
                        nameValues.Set("INVID", Request.QueryString["INVID"].ToString());
                        nameValues.Set("STATUS", Request.QueryString["STATUS"].ToString());
                        string url = "/MM_LISTING_DATA.aspx";
                        Response.Redirect(url + "?" + nameValues, false);
                    }
                    else
                    {
                        PrevURL = PrevURL + "&INVID=" + Request.QueryString["INVID"].ToString() + "&STATUS=" + Request.QueryString["STATUS"].ToString();
                        Response.Redirect(PrevURL.ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        bool isMatched = true;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblllt_name = (Label)e.Row.FindControl("lblllt_name");

                if (isMatched)
                {
                    if (lblllt_name.Text.ToUpper() == txtModifiedTerm.Text.ToUpper())
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].BackColor = Color.Yellow;
                        }

                        isMatched = false;
                    }
                }
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label CMATC1C = (Label)e.Row.FindControl("CMATC1C");

                if (isMatched)
                {
                    if (CMATC1C.Text.ToUpper() == txtModifiedTerm.Text.ToUpper())
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].BackColor = Color.Yellow;
                        }

                        isMatched = false;
                    }
                }
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                string SAE = Convert.ToString(e.CommandArgument);
                if (e.CommandName == "Code")
                {
                    string CMATC1C = ((Label)GridView2.Rows[RowIndex].FindControl("CMATC1C")).Text;
                    AutoCode(CMATC1C, e.CommandArgument.ToString());

                    string PrevURL = Session["prevURL"].ToString();

                    if (PrevURL.ToString().Contains("&INVID="))
                    {
                        string newPrevURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, "") + PrevURL;

                        Uri myUri = new Uri(newPrevURL, UriKind.Absolute);

                        var nameValues = HttpUtility.ParseQueryString(myUri.Query.ToString());
                        nameValues.Set("INVID", Request.QueryString["INVID"].ToString());
                        nameValues.Set("STATUS", Request.QueryString["STATUS"].ToString());
                        string url = "/MM_LISTING_DATA.aspx";
                        Response.Redirect(url + "?" + nameValues, false);
                    }
                    else
                    {
                        PrevURL = PrevURL + "&INVID=" + Request.QueryString["INVID"].ToString() + "&STATUS=" + Request.QueryString["STATUS"].ToString();
                        Response.Redirect(PrevURL.ToString(), false);
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