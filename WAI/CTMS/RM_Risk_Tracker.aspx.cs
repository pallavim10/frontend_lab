using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace CTMS
{
    public partial class RM_Risk_Tracker : System.Web.UI.Page
    {
        DAL dal = new DAL();
        Multiple_Export_Excel exportExcel = new Multiple_Export_Excel();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Status();
                    bind_Cat();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Status()
        {
            try
            {
                DataSet ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Status");
                dal.BindDropDown(ddlStatus, ds.Tables[0]);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Cat();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Cat()
        {
            try
            {
                DataSet ds = dal.Risk_Tracker_SP(Action: "Category", Project_ID: Session["PROJECTID"].ToString(), Status: ddlStatus.SelectedValue);
                gvCat.DataSource = ds.Tables[0];
                gvCat.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvCat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Category_ID = drv["Category_ID"].ToString();

                    GridView gvSubCat = e.Row.FindControl("gvSubCat") as GridView;
                    DataSet ds = dal.Risk_Tracker_SP(Action: "SubCategory", Category_ID: Category_ID, Project_ID: Session["PROJECTID"].ToString(), Status: ddlStatus.SelectedValue);
                    gvSubCat.DataSource = ds.Tables[0];
                    gvSubCat.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvSubCat.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvSubCat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string SubCategory_ID = drv["SubCategory_ID"].ToString();

                    GridView gvFactor = e.Row.FindControl("gvFactor") as GridView;
                    DataSet ds = dal.Risk_Tracker_SP(Action: "Factor", SubCategory_ID: SubCategory_ID, Project_ID: Session["PROJECTID"].ToString(), Status: ddlStatus.SelectedValue);
                    gvFactor.DataSource = ds.Tables[0];
                    gvFactor.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvFactor.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvFactor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Factor_ID = drv["Factor_ID"].ToString();

                    GridView gvEvents = e.Row.FindControl("gvEvents") as GridView;
                    DataSet ds = dal.Risk_Tracker_SP(Action: "Event", SubCategory_ID: Factor_ID, Project_ID: Session["PROJECTID"].ToString(), Status: ddlStatus.SelectedValue);
                    gvEvents.DataSource = ds.Tables[0];
                    gvEvents.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvEvents.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }

                    Label lbl_SubCount = e.Row.FindControl("lbl_SubCount") as Label;

                    if (drv["Up_Trigger"].ToString() != "" && drv["Up_Trigger"].ToString() != "0")
                    {
                        if (Convert.ToInt32(drv["Up_Trigger"]) < Convert.ToInt32(drv["Count"]))
                        {
                            lbl_SubCount.ForeColor = System.Drawing.Color.Red;
                            lbl_SubCount.ToolTip = "Count exceeds Trigger, Consider Upgrading Risk.";
                        }
                    }


                    int minActual = int.MaxValue;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["Days"].ToString() != "")
                        {
                            int Actual = dr.Field<int>("Days");
                            //if (Actual != 0)
                            //{
                            minActual = Math.Min(minActual, Actual);
                            //}
                        }
                    }

                    Label lbl_Days = e.Row.FindControl("lbl_Days") as Label;
                    lbl_Days.Text = minActual.ToString();

                    if (drv["Down_Trigger"].ToString() != "" && drv["Down_Trigger"].ToString() != "0")
                    {
                        if (Convert.ToInt32(drv["Down_Trigger"]) < minActual)
                        {
                            lbl_Days.ForeColor = System.Drawing.Color.Green;
                            lbl_Days.ToolTip = "Last Risk Event more than " + Convert.ToInt32(drv["Down_Trigger"]) + " Days old, Consider Downgrading Risk.";
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvEvents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string ID = drv["ID"].ToString();

                    GridView gvMitigation = e.Row.FindControl("gvMitigation") as GridView;
                    DataSet ds = dal.Risk_Mitigation_SP(Action: "GET", Event_ID: ID);
                    gvMitigation.DataSource = ds.Tables[0];
                    gvMitigation.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvMitigation.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        private void ExportSingleSheet()
        {
            try
            {
                DataSet ds = dal.Risk_Tracker_SP(Action: "ExportExcel", Project_ID: Session["PROJECTID"].ToString(), Status: ddlStatus.SelectedValue);
                ds.Tables[0].TableName = "Risk_Tracker";
                Multiple_Export_Excel.ToExcel(ds, "Risk_Tracker.xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportToExcel()
        {
            try
            {
                DataTable Category = new DataTable();
                DataTable SubCategory = new DataTable();
                DataTable Factor = new DataTable();
                DataTable Event = new DataTable();
                DataTable Mitigation = new DataTable();

                DataRow drCat;
                DataRow drSubCat;
                DataRow drFactor;
                DataRow drEvent;
                DataRow drMitigation;

                bool headcat = false;
                bool headSubCat = false;
                bool headFactor = false;
                bool headEvent = false;
                bool headMitigation = false;

                for (int a = 0; a < gvCat.Rows.Count; a++)
                {
                    if (headcat == false)
                    {
                        Category.Columns.Add(gvCat.HeaderRow.Cells[1].Text);
                        Category.Columns.Add(gvCat.HeaderRow.Cells[2].Text);
                        Category.TableName = "Category";

                        headcat = true;
                    }

                    drCat = Category.NewRow();

                    drCat[0] = ((Label)gvCat.Rows[a].FindControl("lbl_Category")).Text;
                    drCat[1] = ((Label)gvCat.Rows[a].FindControl("lbl_Count")).Text;

                    Category.Rows.Add(drCat);

                    if (headSubCat == true)
                    {
                        drSubCat = SubCategory.NewRow();
                        drSubCat[0] = "";
                        drSubCat[1] = "";
                        SubCategory.Rows.Add(drSubCat);
                    }

                    GridView gvSubCat = gvCat.Rows[a].FindControl("gvSubCat") as GridView;

                    for (int b = 0; b < gvSubCat.Rows.Count; b++)
                    {
                        if (headSubCat == false)
                        {
                            SubCategory.Columns.Add(gvSubCat.HeaderRow.Cells[1].Text);
                            SubCategory.Columns.Add(gvSubCat.HeaderRow.Cells[2].Text);
                            SubCategory.TableName = "Sub-Category";

                            headSubCat = true;
                        }

                        drSubCat = SubCategory.NewRow();

                        drSubCat[0] = ((Label)gvSubCat.Rows[b].FindControl("lbl_SubCat")).Text;
                        drSubCat[1] = ((Label)gvSubCat.Rows[b].FindControl("lbl_SubCount")).Text;

                        SubCategory.Rows.Add(drSubCat);

                        if (headFactor == true)
                        {
                            drFactor = Factor.NewRow();
                            drFactor[0] = "";
                            drFactor[1] = "";
                            drFactor[2] = "";
                            drFactor[3] = "";
                            drFactor[4] = "";
                            drFactor[5] = "";
                            drFactor[6] = "";
                            Factor.Rows.Add(drFactor);
                        }

                        GridView gvFactor = gvSubCat.Rows[b].FindControl("gvFactor") as GridView;

                        for (int c = 0; c < gvFactor.Rows.Count; c++)
                        {
                            if (headFactor == false)
                            {
                                Factor.Columns.Add(gvFactor.HeaderRow.Cells[1].Text);
                                Factor.Columns.Add(gvFactor.HeaderRow.Cells[2].Text);
                                Factor.Columns.Add(gvFactor.HeaderRow.Cells[3].Text);
                                Factor.Columns.Add(gvFactor.HeaderRow.Cells[4].Text);
                                Factor.Columns.Add(gvFactor.HeaderRow.Cells[5].Text);
                                Factor.Columns.Add(gvFactor.HeaderRow.Cells[6].Text);
                                Factor.Columns.Add(gvFactor.HeaderRow.Cells[7].Text);
                                Factor.TableName = "Factor";

                                headFactor = true;
                            }

                            drFactor = Factor.NewRow();

                            drFactor[0] = ((Label)gvFactor.Rows[c].FindControl("lbl_SubCat")).Text;
                            drFactor[1] = ((Label)gvFactor.Rows[c].FindControl("lbl_Probability")).Text;
                            drFactor[2] = ((Label)gvFactor.Rows[c].FindControl("lbl_Severity")).Text;
                            drFactor[3] = ((Label)gvFactor.Rows[c].FindControl("lbl_Detection")).Text;
                            drFactor[4] = ((Label)gvFactor.Rows[c].FindControl("lbl_RPN")).Text;
                            drFactor[5] = ((Label)gvFactor.Rows[c].FindControl("lbl_SubCount")).Text;
                            drFactor[6] = ((Label)gvFactor.Rows[c].FindControl("lbl_Days")).Text;

                            Factor.Rows.Add(drFactor);

                            if (headEvent == true)
                            {
                                drEvent = Event.NewRow();
                                drEvent[0] = "";
                                drEvent[1] = "";
                                drEvent[2] = "";
                                drEvent[3] = "";
                                drEvent[4] = "";
                                Event.Rows.Add(drEvent);
                            }

                            GridView gvEvents = gvFactor.Rows[c].FindControl("gvEvents") as GridView;

                            for (int d = 0; d < gvEvents.Rows.Count; d++)
                            {
                                if (headEvent == false)
                                {
                                    Event.Columns.Add(gvEvents.HeaderRow.Cells[1].Text);
                                    Event.Columns.Add(gvEvents.HeaderRow.Cells[2].Text);
                                    Event.Columns.Add(gvEvents.HeaderRow.Cells[3].Text);
                                    Event.Columns.Add(gvEvents.HeaderRow.Cells[4].Text);
                                    Event.Columns.Add(gvEvents.HeaderRow.Cells[5].Text);
                                    Event.TableName = "Risk Events";

                                    headEvent = true;
                                }

                                drEvent = Event.NewRow();

                                drEvent[0] = ((Label)gvEvents.Rows[d].FindControl("lbl_RiskCons")).Text;
                                drEvent[1] = ((Label)gvEvents.Rows[d].FindControl("lbl_Impact")).Text;
                                drEvent[2] = ((Label)gvEvents.Rows[d].FindControl("lbl_P")).Text;
                                drEvent[3] = ((Label)gvEvents.Rows[d].FindControl("lbl_D")).Text;
                                drEvent[4] = ((Label)gvEvents.Rows[d].FindControl("lbl_Days")).Text;

                                Event.Rows.Add(drEvent);

                                GridView gvMitigation = gvEvents.Rows[d].FindControl("gvMitigation") as GridView;

                                if (headMitigation == true)
                                {
                                    drMitigation = Mitigation.NewRow();
                                    drMitigation[0] = "";
                                    drMitigation[1] = "";
                                    drMitigation[2] = "";
                                    drMitigation[3] = "";
                                    drMitigation[4] = "";
                                    drMitigation[5] = "";
                                    Mitigation.Rows.Add(drMitigation);
                                }

                                for (int e = 0; e < gvMitigation.Rows.Count; e++)
                                {
                                    if (headMitigation == false)
                                    {
                                        Mitigation.Columns.Add(gvMitigation.HeaderRow.Cells[1].Text);
                                        Mitigation.Columns.Add(gvMitigation.HeaderRow.Cells[2].Text);
                                        Mitigation.Columns.Add(gvMitigation.HeaderRow.Cells[3].Text);
                                        Mitigation.Columns.Add(gvMitigation.HeaderRow.Cells[4].Text);
                                        Mitigation.Columns.Add(gvMitigation.HeaderRow.Cells[5].Text);
                                        Mitigation.Columns.Add(gvMitigation.HeaderRow.Cells[6].Text);
                                        Mitigation.TableName = "Risk Mitigations";

                                        headMitigation = true;
                                    }

                                    drMitigation = Mitigation.NewRow();

                                    //drMitigation[0] = ((Label)gvMitigation.Rows[e].FindControl("lbl_ID")).Text;
                                    drMitigation[0] = ((Label)gvMitigation.Rows[e].FindControl("lblCause")).Text;
                                    drMitigation[1] = ((Label)gvMitigation.Rows[e].FindControl("lblMitigation")).Text;
                                    drMitigation[2] = ((Label)gvMitigation.Rows[e].FindControl("lblPrimary_RES")).Text;
                                    drMitigation[3] = ((Label)gvMitigation.Rows[e].FindControl("lblSecondary_RES")).Text;
                                    drMitigation[4] = ((Label)gvMitigation.Rows[e].FindControl("lblDate")).Text;
                                    drMitigation[5] = ((Label)gvMitigation.Rows[e].FindControl("lblDateComplete")).Text;

                                    Mitigation.Rows.Add(drMitigation);
                                }
                            }
                        }
                    }
                }

                DataSet ExcelSheets = new DataSet();
                ExcelSheets.Tables.Add(Category);
                ExcelSheets.Tables.Add(SubCategory);
                ExcelSheets.Tables.Add(Factor);
                ExcelSheets.Tables.Add(Event);
                ExcelSheets.Tables.Add(Mitigation);

                Multiple_Export_Excel.ToExcel(ExcelSheets, "Risk_Tracker.xls", Page.Response);

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

        protected void btnWord_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToWord();
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
                DataSet ds = dal.Risk_Tracker_SP(Action: "ExportExcel", Project_ID: Session["PROJECTID"].ToString(), Status: ddlStatus.SelectedValue);
                ds.Tables[0].TableName = "Risk_Tracker";
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], "Risk_Tracker", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportToWord()
        {
            try
            {
                DataSet ds = dal.Risk_Tracker_SP(Action: "ExportExcel", Project_ID: Session["PROJECTID"].ToString(), Status: ddlStatus.SelectedValue);
                ds.Tables[0].TableName = "Risk_Tracker";
                Multiple_Export_Excel.ExportToWord(ds.Tables[0], "Risk_Tracker", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}