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
    public partial class SITE_LISTING_DATA : System.Web.UI.Page
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
                    DataSet ds = dal.DM_LISTINGS_SP(Action: "GETLISTINGBY_ID", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString(), ID: Request.QueryString["LISTID"].ToString());
                    hdnlistid.Value = Request.QueryString["LISTID"].ToString();
                    lblHeader.Text = ds.Tables[0].Rows[0]["NAME"].ToString();

                    if (ds.Tables[0].Rows[0]["TRANSPOSE"].ToString() == "True")
                    {
                        hdntranspose.Value = "VisitNameVise";
                    }
                    else
                    {
                        hdntranspose.Value = "FieldNameVise";
                    }

                    GET_OnClick();
                    COUNTRY();
                    SITE_AGAINST_COUNTRY();
                    GetIndication();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void COUNTRY()
        {
            try
            {
                DataSet ds = dal.GetSiteID(Action: "GET_COUNTRYID_AND_INVID", User_ID: Session["User_ID"].ToString());
                drpCountry.DataSource = ds.Tables[0];
                drpCountry.DataTextField = "COUNTRYNAME";
                drpCountry.DataValueField = "COUNTRYID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SITE_AGAINST_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GetSiteID(Action: "GET_COUNTRYID_AND_INVID", Country_ID: drpCountry.SelectedValue, User_ID: Session["User_ID"].ToString());
                drpInvID.DataSource = ds.Tables[1];
                drpInvID.DataTextField = "INVID";
                drpInvID.DataValueField = "INVID";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetIndication()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_INDICATION", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpIndication.DataSource = ds.Tables[0];
                    drpIndication.DataValueField = "ID";
                    drpIndication.DataTextField = "INDICATION";
                    drpIndication.DataBind();
                    drpIndication.Items.Insert(0, new ListItem("--ALL--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SITE_AGAINST_COUNTRY();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpIndication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                lnktranspose.Visible = false;
                //BINDSTATUSDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal.getSetDM(Action: "GET_PATIENT_REG", INVID: drpInvID.SelectedValue, VERSIONID: drpIndication.SelectedValue);
                drpSubID.DataSource = ds.Tables[0];
                drpSubID.DataValueField = "SUBJID";
                drpSubID.DataBind();
                drpSubID.Items.Insert(0, new ListItem("--All--", "0"));

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                lnktranspose.Visible = true;
                DataSet ds = new DataSet();
                if (hdntranspose.Value == "VisitNameVise")
                {
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, COUNTRYID: drpCountry.SelectedValue);
                    DataTable dt = GenerateTransposedTable(ds.Tables[0]);
                    ds = new DataSet();
                    ds.Tables.Add(dt);
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue, COUNTRYID: drpCountry.SelectedValue);
                }
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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);
                    DataTable dt = GenerateTransposedTable(ds.Tables[0]);
                    ds = new DataSet();
                    ds.Tables.Add(dt);
                }
                else
                {
                    hdntranspose.Value = "FieldNameVise";
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);
                }
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
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);
                    DataTable dt = GenerateTransposedTable(ds.Tables[0]);
                    ds = new DataSet();
                    ds.Tables.Add(dt);
                }

                DataTable newDT = new DataTable();

                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    DataRow[] rows = ds.Tables[0].Select(" [" + dc.ColumnName.ToString() + "] IS NOT NULL ");
                    double num;
                    if (double.TryParse(rows[0][dc.ColumnName].ToString(), out num))
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
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);
                    DataTable dt = GenerateTransposedTable(ds.Tables[0]);
                    ds = new DataSet();
                    ds.Tables.Add(dt);
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
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);
                }
                else
                {
                    ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA", PROJECTID: Session["PROJECTID"].ToString(), LISTING_ID: Request.QueryString["LISTID"].ToString(), SUBJECTID: drpSubID.SelectedValue, INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);
                    DataTable dt = GenerateTransposedTable(ds.Tables[0]);
                    ds = new DataSet();
                    ds.Tables.Add(dt);
                }
                ds.Tables[0].TableName = lblHeader.Text;
                Multiple_Export_Excel.ExportToRTF(ds.Tables[0], lblHeader.Text, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

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
                    if (dc.ColumnName != "Subject" && dc.ColumnName != "VISIT")
                    {
                        if (columns.Contains(inputTable.Rows[i]["VISIT"].ToString()))
                        {
                            DataRow drNew = outputTable.NewRow();
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

                if (hdntranspose.Value == "FieldNameVise")
                {
                    DataTable OnClickDT = (DataTable)ViewState["OnClickDT"];

                    foreach (DataRow dr in OnClickDT.Rows)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            if (gridDataOnClick[i].ToString() == dr["FIELDNAME"].ToString())
                            {
                                e.Row.Cells[i].Attributes.Add("onclick", dr["OnClick"].ToString());
                                e.Row.Cells[i].CssClass = e.Row.Cells[i].CssClass + " fontBlue";
                            }
                        }
                    }
                }
            }
        }

        public void GET_OnClick()
        {
            try
            {
                DataSet ds = dal.DM_LISTINGS_SP(
                Action: "GET_OnClick",
                LISTING_ID: Request.QueryString["LISTID"].ToString()
                );

                ViewState["OnClickDT"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}