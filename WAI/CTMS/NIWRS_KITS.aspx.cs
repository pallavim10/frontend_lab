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
    public partial class NIWRS_KITS : System.Web.UI.Page
    {
        bool KitsEnd = false, VisitWindow = false;
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_KitLimits();

                    GET_VISIT_WINDOW();

                    GET_KITS();

                    ErrMSG();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_VISIT_WINDOW()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_KITS_SP(ACTION: "GET_BAK_VISIT", SUBJID: Request.QueryString["SUBJID"].ToString(), VISIT: Request.QueryString["VISIT"].ToString());

                if (Convert.ToDateTime(Session["IWRS_CurrentDate"]) < Convert.ToDateTime(ds.Tables[0].Rows[0]["EARLY_DATE"].ToString()) || Convert.ToDateTime(Session["IWRS_CurrentDate"]) > Convert.ToDateTime(ds.Tables[0].Rows[0]["LATE_DATE"].ToString()))
                {
                    VisitWindow = true;
                }
                else
                {
                    VisitWindow = false;
                }



            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_KitLimits()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_QUE_ANS", QUECODE: "BakKitWithoutApprov");

                hfKitWithoutApprov.Value = "";
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hfKitWithoutApprov.Value = ds.Tables[0].Rows[0]["ANS"].ToString();
                    }
                }

                ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_QUE_ANS", QUECODE: "BakKitWithApprov");

                hfKitWithApprov.Value = "";
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hfKitWithApprov.Value = ds.Tables[0].Rows[0]["ANS"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ErrMSG()
        {
            try
            {
                if (KitsEnd)
                {
                    int withOut = 0;
                    if (hfKitWithoutApprov.Value != "")
                    {
                        withOut = Convert.ToInt32(hfKitWithoutApprov.Value) * TreatmentCount;
                    }

                    int with = Convert.ToInt32(hfKitWithApprov.Value) * TreatmentCount;

                    lblErrorMsg.Text = "Note : As per the Study Only " + (with + withOut) + " instances of Backup Kit is Allowed.";
                }
                else if (VisitWindow)
                {
                    lblErrorMsg.Text = "Window Period Exceeded.";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        int TreatmentCount = 0;
        private void GET_KITS()
        {
            try
            {
                DataSet dsKits = dal_IWRS.IWRS_REPORTS_SP(ACTION: "GET_KITS", VISIT: Request.QueryString["VISIT"].ToString(), SUBJID: Request.QueryString["SUBJID"].ToString());

                AddLimitResult(dsKits.Tables[1]);

                DataRow[] drBAKKITS = dsKits.Tables[0].Select(" [Reason; If Back Up Kit Used] IS NOT NULL ");

                DataRow[] drPRIMKITS = dsKits.Tables[0].Select(" [Reason; If Back Up Kit Used] IS NULL ");

                TreatmentCount = drPRIMKITS.Length;

                if (hfKitWithoutApprov.Value != "")
                {
                    int withOut = Convert.ToInt32(hfKitWithoutApprov.Value) * TreatmentCount;

                    if (drBAKKITS.Length >= withOut)
                    {
                        hfApproval.Value = "Yes";

                        if (hfKitWithApprov.Value == "")
                        {
                            hfApproval.Value = "NA";
                        }
                    }
                    else
                    {
                        hfApproval.Value = "No";
                    }
                }

                if (hfKitWithApprov.Value != "")
                {
                    int withOut = 0;
                    if (hfKitWithoutApprov.Value != "")
                    {
                        withOut = Convert.ToInt32(hfKitWithoutApprov.Value) * TreatmentCount;
                    }

                    int with = Convert.ToInt32(hfKitWithApprov.Value) * TreatmentCount;

                    if (drBAKKITS.Length >= (with + withOut))
                    {
                        hfApproval.Value = "NA";
                    }
                }

                if (hfApproval.Value == "NA")
                {
                    int withOut = 0;
                    if (hfKitWithoutApprov.Value != "")
                    {
                        withOut = Convert.ToInt32(hfKitWithoutApprov.Value) * TreatmentCount;
                    }

                    int with = Convert.ToInt32(hfKitWithApprov.Value) * TreatmentCount;

                    lblErrorMsg.Text = "Note : As per the Study Only " + (with + withOut) + " instances of Backup Kit is Allowed.";
                }



                gridData.DataSource = dsKits;
                gridData.DataBind();



            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void AddLimitResult(DataTable table)
        {

            DataColumn limitResultColumn = new DataColumn("Result", typeof(bool))
            {
                DefaultValue = false
            };

            table.Columns.Add(limitResultColumn);

            foreach (DataRow row in table.Rows)
            {

                int KitCount = Convert.ToInt32(hfKitWithoutApprov.Value) + Convert.ToInt32(hfKitWithApprov.Value) + 1;
                string Value = row["Count"].ToString();
                int Count = Convert.ToInt32(Value);
                row["Result"] = Count >= KitCount ? "True" : "False";

            }

            ViewState["TABLEIDX"] = table;

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

                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[11].Visible = false;

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
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[11].Visible = false;
                DataRowView drv = e.Row.DataItem as DataRowView;

                string HdnDISPENSE_IDX = drv["DISPENSE_IDX"].ToString();

                if (hfApproval.Value == "NA")
                {
                    if (!KitsEnd)
                    {
                        KitsEnd = true;
                    }
                }

                if (hfApproval.Value != "NA" && !VisitWindow)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        if (gridDataFields[i].ToString() == "Kit Number")
                        {
                            if (drv["Reason; If Back Up Kit Used"].ToString() == "")
                            {
                                DataTable dtdata = (DataTable)ViewState["TABLEIDX"];
                                if(dtdata != null && dtdata.Rows.Count > 0)
                                {
                                    DataRow[] selectedRows = dtdata.Select($"IDX = {HdnDISPENSE_IDX}");

                                    foreach (DataRow row in selectedRows)
                                    {
                                        if (row["Result"].ToString() == "False")
                                        {
                                            e.Row.Cells[i].Attributes.Add("onclick", "ShowKitDetails(this);");
                                            e.Row.Cells[i].CssClass = e.Row.Cells[i].CssClass + " fontBlue";
                                        }
                                    }
                                }
                                
                            }
                        }
                    }
                }
            }
        }
    }
}