using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Data.SqlClient;

namespace CTMS
{
    public partial class User_Activity_Log : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_DATA();
                    Fill_GetUser();
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
                DataSet ds = dal.ACTIVITY_LOG_SP(Action: "GET", 
                    DateFrom: txtDateFrom.Text,
                    TimeFrom: txtTimeFrom.Text, 
                    DateTo: txtDateTo.Text, 
                    TimeTo: txtTimeTo.Text,
                    User_ID:drpUser.SelectedValue,
                    Function_Name:drpfunction.SelectedValue,
                    Section:drpSection.SelectedValue
                    );
                gridData.DataSource = ds;
                gridData.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
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
                DataTable dt = GetDataTable(gridData);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ToExcel(ds, lblHeader.Text + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
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
                DataTable dt = GetDataTable(gridData);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void btnGet_Click(object sender, EventArgs e)
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

        private void Fill_GetUser()
        {
          
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.ACTIVITY_LOG_SP(
            Action: "GET_User"
            );
            drpUser.DataSource = ds;
            drpUser.DataTextField = "User_Name";
            drpUser.DataValueField = "User_ID";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new ListItem("--All--", "0"));

            drpfunction.Items.Clear();
            drpfunction.Items.Insert(0, new ListItem("--All--", "0"));
        }
 
        protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
        {

            DAL dal;
            dal = new DAL();
            DataSet ds = dal.ACTIVITY_LOG_SP(
            Action: "GET_Section",
            User_ID:drpUser.SelectedValue
            );
            drpSection.DataSource = ds;
            drpSection.DataTextField = "Section";
            drpSection.DataBind();
            drpSection.Items.Insert(0, new ListItem("--All--", "0"));

            drpfunction.Items.Clear();
            drpfunction.Items.Insert(0, new ListItem("--All--", "0"));
        }

        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.ACTIVITY_LOG_SP(
            Action: "GET_Function_Name",
            Section:drpSection.SelectedValue,
            User_ID: drpUser.SelectedValue
            );

            drpfunction.Items.Clear();
            drpfunction.DataSource = ds;
            drpfunction.DataTextField = "Function_Name";
            drpfunction.DataBind();
            drpfunction.Items.Insert(0, new ListItem("--All--", "0"));
        }
    }
}