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
    public partial class ePRO_Subjects : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    lblHeader.Text = "Subject ID : " + Request.QueryString["SUBJID"].ToString();

                    GETDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GETDATA()
        {
            try
            {
                DataSet dsMain = dal.ePRO_WAD_SP(ACTION: "GET_DATA_SUBJECTS_MAIN");

                if (dsMain.Tables.Count > 0 && dsMain.Tables[0].Rows.Count > 0 && Request.QueryString["SUBJID"].ToString() != "" && Request.QueryString["SUBJID"].ToString() != "0")
                {
                    repeatDATA.DataSource = dsMain;
                    repeatDATA.DataBind();
                }
                else
                {
                    repeatDATA.DataSource = null;
                    repeatDATA.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatDATA_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string HEADER = row["HEADER"].ToString();
                    string EPRO_TABLENAME = row["EPRO_TABLENAME"].ToString();
                    string MODULEID = row["MODULEID"].ToString();
                    string DM_TABLENAME = row["TABLENAME"].ToString();

                    string TYPE = row["TYPE"].ToString();

                    GridView gvDATA = e.Item.FindControl("gvDATA") as GridView;
                    GridView gridData = e.Item.FindControl("gridData") as GridView;

                    DataSet ds = new DataSet();

                    if (EPRO_TABLENAME == "ePRO_DATA_PET")
                    {
                        ds = dal.ePRO_WAD_SP(
                        ACTION: "GET_DATA_Additional_PET",
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        MODULEID: MODULEID,
                        TABLENAME: EPRO_TABLENAME
                        );
                    }
                    else if (EPRO_TABLENAME == "ePRO_DATA_ePROAE" || EPRO_TABLENAME == "ePRO_DATA_ePROCM")
                    {
                        ds = dal.ePRO_WAD_SP(
                        ACTION: "GET_DATA_Additional",
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        MODULEID: MODULEID,
                        TABLENAME: EPRO_TABLENAME
                        );
                    }
                    else
                    {
                        ds = dal.ePRO_WAD_SP(
                        ACTION: "GET_DATA_SUBJECTS_INNER",
                        MODULEID: MODULEID,
                        TABLENAME: EPRO_TABLENAME,
                        DM_TABLENAME: DM_TABLENAME,
                        SUBJID: Request.QueryString["SUBJID"].ToString()
                        );
                    }

                    if (TYPE == "Compare")
                    {
                        gridData.Visible = false;

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 1)
                        {
                            gvDATA.DataSource = GenerateTransposedTable(ds.Tables[0]);
                            gvDATA.DataBind();
                        }
                        else
                        {
                            gvDATA.DataSource = null;
                            gvDATA.DataBind();
                        }
                    }
                    else
                    {
                        gvDATA.Visible = false;

                        if (ds.Tables.Count > 0)
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

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("VARIABLENAME");
            outputTable.Columns.Add("DATA");
            outputTable.Columns.Add("InvDATA");

            foreach (DataColumn dc in inputTable.Columns)
            {
                DataRow drNew = outputTable.NewRow();
                drNew["VARIABLENAME"] = dc.ColumnName.ToString();
                drNew["DATA"] = inputTable.Rows[0][dc.ColumnName];
                drNew["InvDATA"] = inputTable.Rows[1][dc.ColumnName];
                outputTable.Rows.Add(drNew);
            }

            return outputTable;
        }

    }
}