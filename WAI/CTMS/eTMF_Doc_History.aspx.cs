using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class eTMF_Doc_History : System.Web.UI.Page
    {
        DAL_eTMF dal_eTMF = new DAL_eTMF();

        DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    if (!this.IsPostBack)
                    {
                        ds = dal_eTMF.eTMF_DATA_SP(ACTION: "GET_DOCUMENT_HISTORY", ID: Request.QueryString["ID"].ToString());
                        gvFiles.DataSource = ds;
                        gvFiles.DataBind();
                    }
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

        protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    int index = e.Row.RowIndex;

                    if (index > 0)
                    {
                        int prevIndex = index - 1;

                        for (int i = 1; i < e.Row.Cells.Count; i++)
                        {
                            string PrevCellValue = ds.Tables[0].Rows[prevIndex][i].ToString();
                            string CellValue = e.Row.Cells[i].Text;

                            if (CellValue == "&nbsp;") { CellValue = ""; }

                            if (PrevCellValue != CellValue)
                            {
                                e.Row.Cells[i].CssClass = e.Row.Cells[i].CssClass + " highlight";
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}