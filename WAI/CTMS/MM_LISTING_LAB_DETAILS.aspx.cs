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
    public partial class MM_LISTING_LAB_DETAILS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["UserGroup_ID"] == null)
                    {
                        Response.Redirect("SessionExpired.aspx", true);
                        return;
                    }
                    DataSet ds = dal.DM_LISTINGS_SP(Action: "GETLISTINGBY_ID", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString(), ID: Request.QueryString["LISTING_ID"].ToString());

                    hdnValue.Value = Request.QueryString["VALUE"].ToString();
                    hdnlistid.Value = Request.QueryString["LISTING_ID"].ToString();
                    hdnSUBJID.Value = Request.QueryString["SUBJID"].ToString();
                    hdnPREV_LISTID.Value = Request.QueryString["PREV_LISTID"].ToString();
                    hdnFIELDID.Value = Request.QueryString["FIELDID"].ToString();

                    if (ds.Tables[0].Rows[0]["TRANSPOSE"].ToString() == "True")
                    {
                        hdntranspose.Value = "VisitNameVise";
                    }
                    else
                    {
                        hdntranspose.Value = "FieldNameVise";
                    }

                    lblHeader.Text = ds.Tables[0].Rows[0]["NAME"].ToString();

                    GET_OnClick();
                    GET_Other_Listings();
                    GET_DATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GET_Other_Listings()
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(
                Action: "GET_Other_Listings",
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                FIELDNAME: Request.QueryString["FIELDID"].ToString()
                );

                repeatOtherListings.DataSource = ds;
                repeatOtherListings.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_OnClick()
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(
                Action: "GET_OnClick",
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString()
                );

                ViewState["OnClickDT"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_DATA()
        {
            try
            {
                lnktranspose.Visible = true;
                DataSet ds = new DataSet();
                if (hdntranspose.Value == "VisitNameVise")
                {
                    ds = dal.DM_LISTINGS_SP(
                    Action: "GETLISTDATA_TRANSPOSE_DETAILS",
                    PROJECTID: Session["PROJECTID"].ToString(),
                    LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                    SUBJECTID: Request.QueryString["SUBJID"].ToString()
                    );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gridData_Tran.DataSource = ds;
                        gridData_Tran.DataBind();
                    }
                    else
                    {
                        gridData_Tran.DataSource = null;
                        gridData_Tran.DataBind();
                    }

                    gridData.Visible = false;
                    gridData_Tran.Visible = true;
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "MM_GETLISTDATA_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                OnClickFilter: Request.QueryString["VALUE"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString(),
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                FIELDID: Request.QueryString["FIELDID"].ToString()
                );
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

                    gridData.Visible = true;
                    gridData_Tran.Visible = false;
                }

                if (Request.QueryString["TEST"] != null)
                {
                    GET_LAB_DETAILS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_LAB_DETAILS()
        {
            try
            {
                hdnPVID.Value = Request.QueryString["PVID"].ToString();
                hdnRECID.Value = Request.QueryString["RECID"].ToString();

                if (Request.QueryString["TEST"] != null)
                {
                    DataSet ds = dal.DM_LISTINGS_SP(Action: "GET_LAB_DETAILS", SUBJECTID: Request.QueryString["SUBJID"].ToString(), TEST: Request.QueryString["TEST"].ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblSUBJID.Text = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                        lblTest.Text = ds.Tables[0].Rows[0]["TEST"].ToString();
                        lblScr_Res.Text = ds.Tables[0].Rows[0]["SCR_Result"].ToString();
                        lblFlag.Text = ds.Tables[0].Rows[0]["SCR_Flag"].ToString();
                        lblLLN.Text = ds.Tables[0].Rows[0]["SCR_LLN"].ToString();
                        lblULN.Text = ds.Tables[0].Rows[0]["SCR_ULN"].ToString();

                        lblLMDV_Visit.Text = ds.Tables[0].Rows[0]["LMDV_VISIT"].ToString();
                        lblLMDV.Text = ds.Tables[0].Rows[0]["LMDV"].ToString();
                        lblLMDV_Flag.Text = ds.Tables[0].Rows[0]["LMDV_Flag"].ToString();
                        lblLMDV_Change_Base.Text = ds.Tables[0].Rows[0]["LMDV_Change_Base"].ToString();
                        lblLMDV_Change_LLN.Text = ds.Tables[0].Rows[0]["LMDV_Change_LLN"].ToString();
                        lblLMDV_Cat.Text = ds.Tables[0].Rows[0]["LMDV_Cat"].ToString();
                        lblLMDV_ManCat.Text = ds.Tables[0].Rows[0]["LMDV_ManCat"].ToString();

                        lblHMDV_Visit.Text = ds.Tables[0].Rows[0]["HMDV_VISIT"].ToString();
                        lblHMDV.Text = ds.Tables[0].Rows[0]["HMDV"].ToString();
                        lblHMDV_Flag.Text = ds.Tables[0].Rows[0]["HMDV_Flag"].ToString();
                        lblHMDV_Change_Base.Text = ds.Tables[0].Rows[0]["HMDV_Change_Base"].ToString();
                        lblHMDV_Change_LLN.Text = ds.Tables[0].Rows[0]["HMDV_Change_LLN"].ToString();
                        lblHMDV_Cat.Text = ds.Tables[0].Rows[0]["HMDV_Cat"].ToString();
                        lblHMDV_ManCat.Text = ds.Tables[0].Rows[0]["HMDV_ManCat"].ToString();

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

        string[] gridDataOnClick;
        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (hdntranspose.Value == "FieldNameVise")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                }
                else
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                }

                string Headers = null;
                foreach (TableCell RowCell in e.Row.Cells)
                {
                    if (Headers == null)
                    {
                        Headers = RowCell.Text;
                    }
                    else
                    {
                        Headers = Headers + "ª" + RowCell.Text;
                    }
                }

                gridDataOnClick = Headers.Split('ª');
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                //string PVID = drv["PVID"].ToString();

                if (hdntranspose.Value == "FieldNameVise")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                }
                else
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                }

                DataTable OnClickDT = (DataTable)ViewState["OnClickDT"];

                foreach (DataRow dr in OnClickDT.Rows)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        //if (gridDataOnClick[i].ToString() == "Subject")
                        //{
                        //    e.Row.Cells[i].Attributes.Add("onclick", "ViewSubjectDetails(this);");
                        //    e.Row.Cells[i].CssClass = e.Row.Cells[i].CssClass + " fontBlue";
                        //}
                        //else
                        if (gridDataOnClick[i].ToString() == dr["FIELDNAME"].ToString())
                        {
                            e.Row.Cells[i].Attributes.Add("onclick", dr["OnClick"].ToString());
                            e.Row.Cells[i].CssClass = e.Row.Cells[i].CssClass + " fontBlue";
                        }
                    }
                }
            }
        }

        protected void btngetTransposData_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (hdntranspose.Value == "FieldNameVise")
                {
                    hdntranspose.Value = "VisitNameVise";
                    ds = ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA_TRANSPOSE_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
                );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gridData_Tran.DataSource = ds;
                        gridData_Tran.DataBind();
                    }
                    else
                    {
                        gridData_Tran.DataSource = null;
                        gridData_Tran.DataBind();
                    }

                    gridData.Visible = false;
                    gridData_Tran.Visible = true;
                }
                else
                {
                    hdntranspose.Value = "FieldNameVise";
                    ds = dal.DM_LISTINGS_SP(
                Action: "MM_GETLISTDATA_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                OnClickFilter: Request.QueryString["VALUE"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString(),
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                FIELDID: Request.QueryString["FIELDID"].ToString()
                );
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

                    gridData.Visible = true;
                    gridData_Tran.Visible = false;
                }
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
                DataSet ds = new DataSet();
                if (hdntranspose.Value == "FieldNameVise")
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                OnClickFilter: Request.QueryString["VALUE"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString(),
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                FIELDID: Request.QueryString["FIELDID"].ToString()
                );
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA_TRANSPOSE_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
                );
                }

                DataTable newDT = new DataTable();

                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    if (hdntranspose.Value == "FieldNameVise")
                    {
                        DataRow[] rows = ds.Tables[0].Select(" [" + dc.ColumnName.ToString() + "] IS NOT NULL ");

                        if (rows.Length > 0)
                        {
                            if (IsNumeric(rows[0][dc.ColumnName].ToString()))
                            {
                                newDT.Columns.Add(dc.ColumnName, typeof(int));
                            }
                            else if (ISDATE(rows[0][dc.ColumnName].ToString()))
                            {
                                newDT.Columns.Add(dc.ColumnName, typeof(DateTime));
                            }
                            else
                            {
                                newDT.Columns.Add(dc.ColumnName, typeof(string));
                            }
                        }
                        else
                        {
                            newDT.Columns.Add(dc.ColumnName, typeof(string));
                        }
                    }
                }

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    newDT.Rows.Add(dr.ItemArray);
                //}

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    newDT.ImportRow(row);
                }

                ds = new DataSet();

                ds.Tables.Add(newDT);

                ds.Tables[0].TableName = lblHeader.Text;
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

        protected void btnRTF_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToRTF();
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
                DataSet ds = new DataSet();
                if (hdntranspose.Value == "FieldNameVise")
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                OnClickFilter: Request.QueryString["VALUE"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString(),
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                FIELDID: Request.QueryString["FIELDID"].ToString()
                );
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA_TRANSPOSE_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
                );
                }

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToPDF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void ExportToRTF()
        {
            try
            {
                DataSet ds = new DataSet();
                if (hdntranspose.Value == "FieldNameVise")
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                OnClickFilter: Request.QueryString["VALUE"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString(),
                PREV_LISTID: Request.QueryString["PREV_LISTID"].ToString(),
                FIELDID: Request.QueryString["FIELDID"].ToString()
                );
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(
                Action: "GETLISTDATA_TRANSPOSE_DETAILS",
                PROJECTID: Session["PROJECTID"].ToString(),
                LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                SUBJECTID: Request.QueryString["SUBJID"].ToString()
                );
                }

                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToRTF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("PVID");
            outputTable.Columns.Add("RECID");
            outputTable.Columns.Add("Subject");
            outputTable.Columns.Add("FIELDNAME");

            DataColumnCollection columns = outputTable.Columns;

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                if (!columns.Contains(inputTable.Rows[i]["VISIT"].ToString()))
                {
                    outputTable.Columns.Add(inputTable.Rows[i]["VISIT"].ToString());
                }
            }

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                foreach (DataColumn dc in inputTable.Columns)
                {
                    if (dc.ColumnName != "Subject" && dc.ColumnName != "VISIT" && dc.ColumnName != "PVID" && dc.ColumnName != "RECID" && dc.ColumnName != "QueryCount" && dc.ColumnName != "ListStatus" && dc.ColumnName != "AnotherListStatus")
                    {
                        if (columns.Contains(inputTable.Rows[i]["VISIT"].ToString()))
                        {
                            DataRow drNew = outputTable.NewRow();
                            drNew["PVID"] = inputTable.Rows[i]["PVID"];
                            drNew["Subject"] = inputTable.Rows[i]["Subject"];
                            drNew["FIELDNAME"] = dc.ColumnName;
                            drNew[inputTable.Rows[i]["VISIT"].ToString()] = inputTable.Rows[i][dc.ColumnName];
                            outputTable.Rows.Add(drNew);
                        }
                    }
                }
            }

            return outputTable;
        }

        protected void repeatData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;

                    string Condition1 = row["Condition1"].ToString();
                    string Ans1 = row["Ans1"].ToString();
                    string Or = row["Or"].ToString();
                    string Condition2 = row["Condition2"].ToString();
                    string Ans2 = row["Ans2"].ToString();
                    string Color = row["Color"].ToString();
                    string DATA = row["DATA"].ToString();

                    Label lblData = (Label)e.Item.FindControl("lblData");

                    bool Result = false;

                    if (Condition1 != "")
                    {

                        if (Condition1 == "IS NULL" && DATA == "")
                        {
                            Result = true;
                        }
                        else if (Condition1 == "IS NOT NULL" && DATA != "")
                        {
                            Result = true;
                        }
                        else if (Condition1 == "=" && DATA == Ans1)
                        {
                            Result = true;
                        }
                        else if (Condition1 == "!=" && DATA != Ans1)
                        {
                            Result = true;
                        }
                        else if (Condition1 == ">")
                        {
                            if (IsNumeric(DATA) && IsNumeric(Ans1))
                            {
                                if (Convert.ToInt32(DATA) > Convert.ToInt32(Ans1))
                                {
                                    Result = true;
                                }
                            }
                        }
                        else if (Condition1 == "=>")
                        {
                            if (IsNumeric(DATA) && IsNumeric(Ans1))
                            {
                                if (Convert.ToInt32(DATA) >= Convert.ToInt32(Ans1))
                                {
                                    Result = true;
                                }
                            }
                        }
                        else if (Condition1 == "<")
                        {
                            if (IsNumeric(DATA) && IsNumeric(Ans1))
                            {
                                if (Convert.ToInt32(DATA) < Convert.ToInt32(Ans1))
                                {
                                    Result = true;
                                }
                            }
                        }
                        else if (Condition1 == "=<")
                        {
                            if (IsNumeric(DATA) && IsNumeric(Ans1))
                            {
                                if (Convert.ToInt32(DATA) <= Convert.ToInt32(Ans1))
                                {
                                    Result = true;
                                }
                            }
                        }
                        else if (Condition1 == "[_]%")
                        {
                            if (DATA.StartsWith(Ans1))
                            {
                                Result = true;
                            }
                        }
                        else if (Condition1 == "![_]%")
                        {
                            if (!DATA.StartsWith(Ans1))
                            {
                                Result = true;
                            }
                        }
                        else if (Condition1 == "%_%")
                        {
                            if (DATA.Contains(Ans1))
                            {
                                Result = true;
                            }
                        }
                        else if (Condition1 == "!%_%")
                        {
                            if (!DATA.Contains(Ans1))
                            {
                                Result = true;
                            }
                        }

                        if (Or != "" && Result == false)
                        {
                            if (Condition2 == "IS NULL" && DATA == "")
                            {
                                Result = true;
                            }
                            else if (Condition2 == "IS NOT NULL" && DATA != "")
                            {
                                Result = true;
                            }
                            else if (Condition2 == "=" && DATA == Ans2)
                            {
                                Result = true;
                            }
                            else if (Condition2 == "!=" && DATA != Ans2)
                            {
                                Result = true;
                            }
                            else if (Condition2 == ">")
                            {
                                if (IsNumeric(DATA) && IsNumeric(Ans2))
                                {
                                    if (Convert.ToInt32(DATA) > Convert.ToInt32(Ans2))
                                    {
                                        Result = true;
                                    }
                                }
                            }
                            else if (Condition2 == "=>")
                            {
                                if (IsNumeric(DATA) && IsNumeric(Ans2))
                                {
                                    if (Convert.ToInt32(DATA) >= Convert.ToInt32(Ans2))
                                    {
                                        Result = true;
                                    }
                                }
                            }
                            else if (Condition2 == "<")
                            {
                                if (IsNumeric(DATA) && IsNumeric(Ans2))
                                {
                                    if (Convert.ToInt32(DATA) < Convert.ToInt32(Ans2))
                                    {
                                        Result = true;
                                    }
                                }
                            }
                            else if (Condition2 == "=<")
                            {
                                if (IsNumeric(DATA) && IsNumeric(Ans2))
                                {
                                    if (Convert.ToInt32(DATA) <= Convert.ToInt32(Ans2))
                                    {
                                        Result = true;
                                    }
                                }
                            }
                            else if (Condition2 == "[_]%")
                            {
                                if (DATA.StartsWith(Ans2))
                                {
                                    Result = true;
                                }
                            }
                            else if (Condition2 == "![_]%")
                            {
                                if (!DATA.StartsWith(Ans2))
                                {
                                    Result = true;
                                }
                            }
                            else if (Condition2 == "%_%")
                            {
                                if (DATA.Contains(Ans2))
                                {
                                    Result = true;
                                }
                            }
                            else if (Condition2 == "!%_%")
                            {
                                if (!DATA.Contains(Ans2))
                                {
                                    Result = true;
                                }
                            }
                        }
                    }

                    if (Result == true && Color != "")
                    {
                        lblData.Style.Add("color", Color);
                        lblData.Font.Bold = true;
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
    }
}