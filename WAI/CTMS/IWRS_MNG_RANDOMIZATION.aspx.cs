using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class IWRS_MNG_RANDOMIZATION : System.Web.UI.Page
    {
        DAL dal = new DAL();

        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                    GET_SITEIDs();
                    GET_RAND_POOL_BLOCK();
                    GET_RAND_POOL();

                    
                    GET_BLOCK();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        
        private void GET_RAND_POOL()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UPLOAD_SP(ACTION: "GET_RAND_POOL", Value1: drpFrom.SelectedValue, Value2: drpTo.SelectedValue);

                gvRands.DataSource = ds;
                gvRands.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_RAND_POOL_BLOCK()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UPLOAD_SP(ACTION: "GET_RAND_POOL_BLOCK");

                drpFrom.DataSource = ds.Tables[0];
                drpFrom.DataValueField = "BLOCK";
                drpFrom.DataBind();
                drpFrom.Items.Insert(0, new ListItem("--All--", "0"));


                drpTo.DataSource = ds.Tables[0];
                drpTo.DataValueField = "BLOCK";
                drpTo.DataBind();
                drpTo.Items.Insert(0, new ListItem("--All--", "0"));


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_SITEIDs()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(
                   USERID: Session["User_ID"].ToString()
                   );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        drpSITEID.DataSource = ds.Tables[0];
                        drpSITEID.DataValueField = "INVNAME";
                        drpSITEID.DataBind();
                    }
                    else
                    {
                        drpSITEID.DataSource = ds.Tables[0];
                        drpSITEID.DataValueField = "INVNAME";
                        drpSITEID.DataBind();
                        drpSITEID.Items.Insert(0, new ListItem("--All--", "0"));
                    }
                }
                else
                {
                    drpSITEID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAllocate_Click(object sender, EventArgs e)
        {

            try
            {
                dal_IWRS.IWRS_UPLOAD_SP(
                    ACTION: "ALLOCATE_RAND_NUMBERS",
                    SITEID: drpSITEID.SelectedValue,
                    Value1: drpFrom.SelectedValue,
                    Value2: drpTo.SelectedValue
                    );

                GET_RAND_POOL();

                Response.Write("<script>alert('Randomization Numbers allocate successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_RAND_POOL();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_RAND_POOL();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_BLOCK()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_UPLOAD_SP(ACTION: "GET_RAND_BLOCK");

                ddlFromBlock.DataSource = ds.Tables[0];
                ddlFromBlock.DataValueField = "BLOCK";
                ddlFromBlock.DataBind();
                ddlFromBlock.Items.Insert(0, new ListItem("--All--", "0"));


                ddlToBlock.DataSource = ds.Tables[0];
                ddlToBlock.DataValueField = "BLOCK";
                ddlToBlock.DataBind();
                ddlToBlock.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btn_UnBlock(object sender, EventArgs e)
        {

            try
            {
                bool kitSelected = true;
                for (int i = 0; i < grdRandNos.Rows.Count; i++)
                {
                    CheckBox Chek_UnBlock = (CheckBox)grdRandNos.Rows[i].FindControl("Chek_UnBlock");

                    if (Chek_UnBlock.Checked && kitSelected)
                    {
                        kitSelected = false;
                    }
                }
                if (kitSelected)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Select at least one randomization no..'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                else
                {
                    modalpop_UnBlock.Show();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitUnBlock_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdRandNos.Rows.Count; i++)
                {
                    string RANDNO = ((Label)grdRandNos.Rows[i].FindControl("RANDNO")).Text;

                    CheckBox Chek_UnBlock = (CheckBox)grdRandNos.Rows[i].FindControl("Chek_UnBlock");

                    if (Chek_UnBlock.Checked)
                    {
                        dal_IWRS.IWRS_RANDNO_SP(ACTION: "UnBlock", RANDNO: RANDNO, COMM: txtUnBlockComments.Text);
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Randomization numbers UnBlocked successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelUnBlock_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btn_Block(object sender, EventArgs e)
        {

            try
            {
                bool kitSelected = true;
                for (int i = 0; i < grdRandNos.Rows.Count; i++)
                {
                    CheckBox Chek_Block = (CheckBox)grdRandNos.Rows[i].FindControl("Chek_Block");

                    if (Chek_Block.Checked && kitSelected)
                    {
                        kitSelected = false;
                    }
                }
                if (kitSelected)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Select at least one randomization no..'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
                }
                else
                {
                    modalpop_Block.Show();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitBlock_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdRandNos.Rows.Count; i++)
                {
                    string RANDNO = ((Label)grdRandNos.Rows[i].FindControl("RANDNO")).Text;

                    CheckBox Chek_Block = (CheckBox)grdRandNos.Rows[i].FindControl("Chek_Block");

                    if (Chek_Block.Checked)
                    {
                        dal_IWRS.IWRS_RANDNO_SP(ACTION: "BLOCK", RANDNO: RANDNO, COMM: txtBlockComments.Text);
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Randomization numbers blocked successfully.'); window.location.href = '" + Request.RawUrl.ToString() + "'; ", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelBlock_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_RANDNO_SP(ACTION: "GET_RANDNO", FromBlock: ddlFromBlock.SelectedValue, ToBlock: ddlToBlock.SelectedValue);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdRandNos.DataSource = ds;
                    grdRandNos.DataBind();
                }
                else
                {
                    grdRandNos.DataSource = null;
                    grdRandNos.DataBind();
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

        protected void grdRandNos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string STATUS = dr["STATUS"].ToString();

                    CheckBox Chek_Block = (CheckBox)e.Row.FindControl("Chek_Block");
                    CheckBox Chek_UnBlock = (CheckBox)e.Row.FindControl("Chek_UnBlock");

                    if (STATUS == "2")
                    {
                        Chek_Block.Visible = false;
                        Chek_UnBlock.Visible = true;
                    }
                    else
                    {
                        Chek_Block.Visible = true;
                        Chek_UnBlock.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //protected void lbnMNGRANDMExport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string xlname = "Randomization-Number.xls";

        //        DataSet ds = dal_IWRS.IWRS_RANDNO_SP(ACTION: "GET_RANDNO_EXPORT", FromBlock: ddlFromBlock.SelectedValue, ToBlock: ddlToBlock.SelectedValue);

        //        Multiple_Export_Excel.ToExcel(ds, xlname, Page.Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        private void GetData()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_AUDITTRAIL_SP(ACTION: "GET_RONDOMIZATION_NO_LOGS",
                    FROMDATE: txtDateFrom.Text,
                    TODATE: txtDateTo.Text
                    );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GrdData.DataSource = ds.Tables[0];
                    GrdData.DataBind();

                }
                else
                {
                    GrdData.DataSource = null;
                    GrdData.DataBind();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            ExportToPDF();
        }

        private void ExportToPDF()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_AUDITTRAIL_SP(ACTION: "GET_RONDOMIZATION_NO_LOGS",
                    FROMDATE: txtDateFrom.Text,
                    TODATE: txtDateTo.Text
                    );

                ds.Tables[0].TableName = "IWRS Randomization No Report";
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], "IWRS Randomization No Report", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GrdData_PreRender(object sender, EventArgs e)
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            DataSet ds = dal_IWRS.IWRS_AUDITTRAIL_SP(ACTION: "GET_RONDOMIZATION_NO_LOGS",
                FROMDATE: txtDateFrom.Text,
                TODATE: txtDateTo.Text
                );

            ds.Tables[0].TableName = "Randomization No. Log";
            Multiple_Export_Excel.ToExcel(ds.Tables[0], "Randomization No. Log", Page.Response);
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportPDF();
        }

        private void ExportPDF()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_AUDITTRAIL_SP(ACTION: "GET_RONDOMIZATION_NO_LOGS",
                FROMDATE: txtDateFrom.Text,
                TODATE: txtDateTo.Text
                );

                ds.Tables[0].TableName = "Randomization No. Log";
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], "Randomization No. Log", Page.Response);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnRanLogData_Click(object sender, EventArgs e)
        {
            ActiveTabID.Value = "tab-3";
            GetData();
        }
    }
}