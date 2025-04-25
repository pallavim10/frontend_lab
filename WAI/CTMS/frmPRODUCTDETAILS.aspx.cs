using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class frmPRODUCTDETAILS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx");
                }
                if (!this.IsPostBack)
                {
                    GetRecords();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        public void GetRecords()
        {
            try
            {
                dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                ds = dal.GetSetPRODUCTDETAILS(Action: "GET_DATA");
                PRODUCTDETAILS.DataSource = ds;
                PRODUCTDETAILS.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void InsertUpdatePRODUCTDETAILS(int rownum)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "INSERT",
                PRODUCTID: ((TextBox)PRODUCTDETAILS.Rows[rownum].FindControl("PRODUCTID")).Text,
                PRODUCTNAM: ((TextBox)PRODUCTDETAILS.Rows[rownum].FindControl("PRODUCTNAM")).Text,             
                ENTEREDBY: Session["User_ID"].ToString()
                );
            }
            catch (Exception ex)
            {
                throw ex;
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void bntSave_Click(object sender, EventArgs e)
        {
            try
            {
                int rownum;
                for (rownum = 0; rownum < PRODUCTDETAILS.Rows.Count; rownum++)
                {
                    if (((TextBox)PRODUCTDETAILS.Rows[rownum].FindControl("PRODUCTID")).Text == "")
                    {
                        lblErrorMsg.Text = "Please enter PRODUCT ID.";
                    }
                    else if (((TextBox)PRODUCTDETAILS.Rows[rownum].FindControl("PRODUCTNAM")).Text == "")
                    {
                        lblErrorMsg.Text = "Please enter PRODUCT NAME .";
                    }
                    else
                    {
                        InsertUpdatePRODUCTDETAILS(rownum);
                    }
                }
                Response.Write("<script> alert('Record Updated successfully.');window.location='frmPRODUCTDETAILS.aspx'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void PROJDETAILS_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string PRODUCTID = dr["PRODUCTID"].ToString();
                    if (PRODUCTID != "")
                    {
                        e.Row.Cells[0].Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



        protected void bntAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Table Structure
                DataRow drCurrentRow = null;
                DataTable dtCurrentTable;
                int rowIndex = 0;
                int i;
                dtCurrentTable = new DataTable();
                dtCurrentTable.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("PRODUCTNAM", typeof(string)));              
                if (PRODUCTDETAILS.Rows.Count > 0)
                {
                    for (i = 0; i < PRODUCTDETAILS.Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["PRODUCTID"] = ((TextBox)PRODUCTDETAILS.Rows[rowIndex].FindControl("PRODUCTID")).Text;
                        drCurrentRow["PRODUCTNAM"] = ((TextBox)PRODUCTDETAILS.Rows[rowIndex].FindControl("PRODUCTNAM")).Text;                     
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        rowIndex++;
                    }

                    //Add Empty Row
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["PRODUCTID"] = "";
                    drCurrentRow["PRODUCTNAM"] = "";                 
                    dtCurrentTable.Rows.Add(drCurrentRow);
                }
                PRODUCTDETAILS.DataSource = dtCurrentTable;
                PRODUCTDETAILS.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}