using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using ExcelDataReader;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_Mapping_Tool : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GetModule();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetModule()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal_DB.DB_MAP_SP(ACTION: "GET_MODULENAME");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.DataSource = ds.Tables[0];
                    ddlModule.DataValueField = "ID";
                    ddlModule.DataTextField = "MODULENAME";
                    ddlModule.DataBind();
                    ddlModule.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlModule.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_FIELDS()
        {
            try
            {
                DataSet dsDetails = dal_DB.DB_MAP_SP(ACTION: "GET_MODULE_DETAILS",
                    MODULEID: ddlModule.SelectedValue
                    );

                DataSet ds = dal_DB.DB_MAP_SP(ACTION: "GET_FIELDS_MAPPINGS",
                    MODULEID: ddlModule.SelectedValue,
                    SHEET_NAME: hfSHEETNAME.Value
                    );

                ddlField.Items.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlField.DataSource = ds.Tables[0];
                    Session["FIELDNAME"] = ds.Tables[0];
                    ddlField.DataTextField = "FIELDNAME";
                    ddlField.DataValueField = "ID";
                    ddlField.DataBind();
                }
                else
                {
                    ddlField.DataSource = null;
                    ddlField.DataBind();
                }

                ddlField.Items.Insert(0, new ListItem("--Select--", "0"));

                if (dsDetails.Tables.Count > 0 && dsDetails.Tables[0].Rows.Count > 0 && dsDetails.Tables[0].Rows[0]["EXT"].ToString() != "True")
                {
                    ddlField.Items.Insert(1, new ListItem("SUBJECT NO.", "SUBJID_DATA"));
                    ddlField.Items.Insert(2, new ListItem("VISIT", "VISIT"));
                    ddlField.Items.Insert(3, new ListItem("INVESTIGATOR ID", "SITEID"));
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DeleteMap(string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_MAP_SP(ACTION: "DELETE_MAPPING",
                    SHEET_NAME: hfSHEETNAME.Value,
                    MODULEID: ID
                    );

                grdMap.DataSource = ds;
                grdMap.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.Enabled = false;
                }
                else
                {
                    ddlModule.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SetPrim(string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_MAP_SP(ACTION: "SETPRIM_MAPPING",
                    SHEET_NAME: hfSHEETNAME.Value,
                    MODULEID: ID
                    );

                grdMap.DataSource = ds;
                grdMap.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.Enabled = false;
                }
                else
                {
                    ddlModule.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void RemovePrim(string ID)
        {
            try
            {
                DataSet ds = dal_DB.DB_MAP_SP(ACTION: "REMOVEPRIM_MAPPING",
                    SHEET_NAME: hfSHEETNAME.Value,
                    MODULEID: ID
                    );

                grdMap.DataSource = ds;
                grdMap.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.Enabled = false;
                }
                else
                {
                    ddlModule.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BindData()
        {
            try
            {
                DataSet ds = dal_DB.DB_MAP_SP(ACTION: "GET_MAPPING",
                    SHEET_NAME: hfSHEETNAME.Value
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.SelectedValue = ds.Tables[0].Rows[0]["MODULEID"].ToString();
                    grdMap.DataSource = ds;
                    grdMap.DataBind();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlModule.Enabled = false;
                    }
                    else
                    {
                        ddlModule.Enabled = true;
                    }

                    GET_FIELDS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void INSERT_MAP()
        {
            try
            {
                string COLUMNNAME = "";

                if (ddlExcelCol.Visible == true && ddlExcelCol.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlExcelCol.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlExcelCol.SelectedValue;
                    }
                }

                if (ddlChildNo2.Visible == true && ddlChildNo2.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo2.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo2.SelectedValue;
                    }
                }

                if (ddlChildNo3.Visible == true && ddlChildNo3.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo3.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo3.SelectedValue;
                    }
                }

                if (ddlChildNo4.Visible == true && ddlChildNo4.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo4.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo4.SelectedValue;
                    }
                }

                if (ddlChildNo5.Visible == true && ddlChildNo5.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo5.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo5.SelectedValue;
                    }

                }

                if (ddlChildNo6.Visible == true && ddlChildNo6.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo6.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo6.SelectedValue;
                    }
                }

                if (ddlChildNo7.Visible == true && ddlChildNo7.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo7.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo7.SelectedValue;
                    }
                }

                if (ddlChildNo8.Visible == true && ddlChildNo8.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo8.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo8.SelectedValue;
                    }
                }

                if (ddlChildNo9.Visible == true && ddlChildNo9.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo9.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo9.SelectedValue;
                    }
                }

                if (ddlChildNo10.Visible == true && ddlChildNo11.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo10.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo10.SelectedValue;
                    }
                }

                if (ddlChildNo11.Visible == true && ddlChildNo11.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo11.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo11.SelectedValue;
                    }
                }

                if (ddlChildNo12.Visible == true && ddlChildNo12.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo12.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo12.SelectedValue;
                    }
                }

                if (ddlChildNo13.Visible == true && ddlChildNo13.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo13.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo13.SelectedValue;
                    }
                }

                if (ddlChildNo14.Visible == true && ddlChildNo14.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo14.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo14.SelectedValue;
                    }
                }

                if (ddlChildNo15.Visible == true && ddlChildNo14.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo15.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo15.SelectedValue;
                    }
                }

                if (ddlChildNo16.Visible == true && ddlChildNo16.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo16.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo16.SelectedValue;
                    }
                }

                if (ddlChildNo17.Visible == true && ddlChildNo17.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo17.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo17.SelectedValue;
                    }
                }

                if (ddlChildNo18.Visible == true && ddlChildNo18.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo18.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo18.SelectedValue;
                    }
                }

                if (ddlChildNo19.Visible == true && ddlChildNo19.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo19.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo19.SelectedValue;
                    }
                }

                if (ddlChildNo20.Visible == true && ddlChildNo20.SelectedIndex != 0)
                {
                    if (COLUMNNAME == "")
                    {
                        COLUMNNAME = ddlChildNo20.SelectedValue;
                    }
                    else
                    {
                        COLUMNNAME = COLUMNNAME + "," + ddlChildNo20.SelectedValue;
                    }
                }

                DataSet ds = dal_DB.DB_MAP_SP
                (
                ACTION: "INSERT_MAPPING",
                SHEET_NAME: hfSHEETNAME.Value,
                SHEET_COLUMN: COLUMNNAME,
                MODULEID: ddlModule.SelectedValue,
                FIELDNAME: ddlField.SelectedValue,
                Cur_ANSWERS: txtDefaultVal.Text
                );

                grdMap.DataSource = ds;
                grdMap.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.Enabled = false;
                }
                else
                {
                    ddlModule.Enabled = true;
                }

                ddlExcelCol.SelectedIndex = 0;
                divChild2.Visible = false;
                divChild3.Visible = false;
                divChild4.Visible = false;
                divChild5.Visible = false;
                divChild6.Visible = false;
                divChild7.Visible = false;
                divChild8.Visible = false;
                divChild9.Visible = false;
                divChild10.Visible = false;
                divChild11.Visible = false;
                divChild12.Visible = false;
                divChild13.Visible = false;
                divChild14.Visible = false;
                divChild15.Visible = false;
                divChild16.Visible = false;
                divChild17.Visible = false;
                divChild18.Visible = false;
                divChild19.Visible = false;
                divChild20.Visible = false;
                txtDefaultVal.Text = "";
                GET_FIELDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_FIELDS();
                AUTOMAPPPING();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void AUTOMAPPPING()
        {
            try
            {
                DataTable dtField = (DataTable)Session["FIELDNAME"];
                DataTable dtSheetCol = (DataTable)Session["ExcelSheet"];

                for (int i = 0; i < dtField.Rows.Count; i++)
                {
                    string FieldID = dtField.Rows[i]["ID"].ToString();
                    string VARIABLENAME = dtField.Rows[i]["VARIABLENAME"].ToString();

                    DataRow[] SheetCol = dtSheetCol.Select("Column LIKE '%" + VARIABLENAME + "%' ");
                    bool colValue1 = false;
                    bool colValue2 = false;
                    string ColValue2Col = "";

                    foreach (DataRow row in SheetCol)
                    {
                        string Colname = row["Column"].ToString();

                        if (Colname == VARIABLENAME)
                        {
                            colValue1 = true;
                        }

                        if (Colname == VARIABLENAME + "_C")
                        {
                            colValue2 = true;
                            ColValue2Col = VARIABLENAME + "_C";
                        }

                        string ColumnName = "";

                        if (colValue2 == true)
                        {
                            ColumnName = ColValue2Col;
                        }
                        else if (colValue1 == true)
                        {
                            ColumnName = VARIABLENAME;
                        }

                        if (ColumnName != "")
                        {
                            DataSet ds = dal_DB.DB_MAP_SP(ACTION: "INSERT_MAPPING",
                                SHEET_NAME: hfSHEETNAME.Value,
                                SHEET_COLUMN: ColumnName,
                                MODULEID: ddlModule.SelectedValue,
                                FIELDNAME: FieldID
                                );
                        }
                    }
                }

                BindData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdMap_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteMap")
                {
                    DeleteMap(e.CommandArgument.ToString());
                }
                else if (e.CommandName == "SetPrim")
                {
                    SetPrim(e.CommandArgument.ToString());
                }
                else if (e.CommandName == "RemovePrim")
                {
                    RemovePrim(e.CommandArgument.ToString());
                }
                GET_FIELDS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable excelData = new DataTable();
                string filename = fileUpload.FileName;

                hfSHEETNAME.Value = filename;
                if (filename != "")
                {
                    string tempPath = "ExcelData";
                    if (!Directory.Exists(tempPath))
                    {
                        DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                        info.Create();
                    }
                    string savepath = Server.MapPath(tempPath);
                    fileUpload.SaveAs(savepath + @"\" + filename);
                    string filePath = savepath + @"\" + filename;
                    if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    {
                        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        DataSet result = excelReader.AsDataSet();
                        excelReader.Close();
                        excelData = result.Tables[0];
                    }
                    else if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xls")
                    {
                        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        DataSet result = excelReader.AsDataSet();
                        excelReader.Close();
                        excelData = result.Tables[0];
                    }
                    else
                    {
                        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateCsvReader(stream);
                        DataSet result = excelReader.AsDataSet();
                        excelReader.Close();
                        excelData = result.Tables[0];
                    }

                    if (excelData.Rows.Count > 0)
                    {
                        DataTable dtExcelSheet = new DataTable();
                        dtExcelSheet.Columns.Add("Column", typeof(String));
                        int cols = excelData.Columns.Count;
                        for (int i = 0; i < cols; i++)
                        {
                            dtExcelSheet.Rows.Add(excelData.Rows[0][i]);
                        }

                        Session["ExcelSheet"] = dtExcelSheet;
                        ddlExcelCol.DataSource = dtExcelSheet;
                        ddlExcelCol.DataTextField = "Column";
                        ddlExcelCol.DataValueField = "Column";
                        ddlExcelCol.DataBind();
                        ddlExcelCol.Items.Insert(0, new ListItem("--Select--", "0"));

                        divModule.Visible = true;
                        divMapping.Visible = true;
                    }
                    else
                    {
                        divModule.Visible = false;
                        divMapping.Visible = false;
                    }
                }

                BindData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_MAP();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddMoreChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlChildNo19.Visible == true)
                {
                    divChild20.Visible = true;
                    ddlChildNo20.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo19.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo20.DataSource = dt;
                    ddlChildNo20.DataTextField = "Column";
                    ddlChildNo20.DataValueField = "Column";
                    ddlChildNo20.DataBind();
                    ddlChildNo20.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo18.Visible == true)
                {
                    divChild19.Visible = true;
                    ddlChildNo19.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo18.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo19.DataSource = dt;
                    ddlChildNo19.DataTextField = "Column";
                    ddlChildNo19.DataValueField = "Column";
                    ddlChildNo19.DataBind();
                    ddlChildNo19.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo17.Visible == true)
                {
                    divChild18.Visible = true;
                    ddlChildNo18.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo17.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo18.DataSource = dt;
                    ddlChildNo18.DataTextField = "Column";
                    ddlChildNo18.DataValueField = "Column";
                    ddlChildNo18.DataBind();
                    ddlChildNo18.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo16.Visible == true)
                {
                    divChild17.Visible = true;
                    ddlChildNo17.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo16.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo17.DataSource = dt;
                    ddlChildNo17.DataTextField = "Column";
                    ddlChildNo17.DataValueField = "Column";
                    ddlChildNo17.DataBind();
                    ddlChildNo17.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo15.Visible == true)
                {
                    divChild16.Visible = true;
                    ddlChildNo16.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo15.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo16.DataSource = dt;
                    ddlChildNo16.DataTextField = "Column";
                    ddlChildNo16.DataValueField = "Column";
                    ddlChildNo16.DataBind();
                    ddlChildNo16.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo14.Visible == true)
                {
                    divChild15.Visible = true;
                    ddlChildNo15.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo14.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo15.DataSource = dt;
                    ddlChildNo15.DataTextField = "Column";
                    ddlChildNo15.DataValueField = "Column";
                    ddlChildNo15.DataBind();
                    ddlChildNo15.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo13.Visible == true)
                {
                    divChild14.Visible = true;
                    ddlChildNo14.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo13.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo14.DataSource = dt;
                    ddlChildNo14.DataTextField = "Column";
                    ddlChildNo14.DataValueField = "Column";
                    ddlChildNo14.DataBind();
                    ddlChildNo14.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo12.Visible == true)
                {
                    divChild13.Visible = true;
                    ddlChildNo13.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo12.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo13.DataSource = dt;
                    ddlChildNo13.DataTextField = "Column";
                    ddlChildNo13.DataValueField = "Column";
                    ddlChildNo13.DataBind();
                    ddlChildNo13.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo11.Visible == true)
                {
                    divChild12.Visible = true;
                    ddlChildNo12.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo11.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo12.DataSource = dt;
                    ddlChildNo12.DataTextField = "Column";
                    ddlChildNo12.DataValueField = "Column";
                    ddlChildNo12.DataBind();
                    ddlChildNo12.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo10.Visible == true)
                {
                    divChild11.Visible = true;
                    ddlChildNo11.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo10.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo11.DataSource = dt;
                    ddlChildNo11.DataTextField = "Column";
                    ddlChildNo11.DataValueField = "Column";
                    ddlChildNo11.DataBind();
                    ddlChildNo11.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo9.Visible == true)
                {
                    divChild10.Visible = true;
                    ddlChildNo10.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo9.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo10.DataSource = dt;
                    ddlChildNo10.DataTextField = "Column";
                    ddlChildNo10.DataValueField = "Column";
                    ddlChildNo10.DataBind();
                    ddlChildNo10.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo8.Visible == true)
                {
                    divChild9.Visible = true;
                    ddlChildNo9.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo8.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo9.DataSource = dt;
                    ddlChildNo9.DataTextField = "Column";
                    ddlChildNo9.DataValueField = "Column";
                    ddlChildNo9.DataBind();
                    ddlChildNo9.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo7.Visible == true)
                {
                    divChild8.Visible = true;
                    ddlChildNo8.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo7.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo8.DataSource = dt;
                    ddlChildNo8.DataTextField = "Column";
                    ddlChildNo8.DataValueField = "Column";
                    ddlChildNo8.DataBind();
                    ddlChildNo8.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo6.Visible == true)
                {
                    divChild7.Visible = true;
                    ddlChildNo7.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo6.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo7.DataSource = dt;
                    ddlChildNo7.DataTextField = "Column";
                    ddlChildNo7.DataValueField = "Column";
                    ddlChildNo7.DataBind();
                    ddlChildNo7.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo5.Visible == true)
                {
                    divChild6.Visible = true;
                    ddlChildNo6.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo5.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo6.DataSource = dt;
                    ddlChildNo6.DataTextField = "Column";
                    ddlChildNo6.DataValueField = "Column";
                    ddlChildNo6.DataBind();
                    ddlChildNo6.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo4.Visible == true)
                {
                    divChild5.Visible = true;
                    ddlChildNo5.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo4.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo5.DataSource = dt;
                    ddlChildNo5.DataTextField = "Column";
                    ddlChildNo5.DataValueField = "Column";
                    ddlChildNo5.DataBind();
                    ddlChildNo5.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo3.Visible == true)
                {
                    divChild4.Visible = true;
                    ddlChildNo4.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo3.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo4.DataSource = dt;
                    ddlChildNo4.DataTextField = "Column";
                    ddlChildNo4.DataValueField = "Column";
                    ddlChildNo4.DataBind();
                    ddlChildNo4.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else if (ddlChildNo2.Visible == true)
                {
                    divChild3.Visible = true;
                    ddlChildNo3.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlChildNo2.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo3.DataSource = dt;
                    ddlChildNo3.DataTextField = "Column";
                    ddlChildNo3.DataValueField = "Column";
                    ddlChildNo3.DataBind();
                    ddlChildNo3.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
                else
                {
                    divChild2.Visible = true;
                    ddlChildNo2.Visible = true;
                    DataTable dt = (DataTable)Session["ExcelSheet"];
                    DataRow[] rows;
                    rows = dt.Select("Column = '" + ddlExcelCol.SelectedItem.Value + "'");
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                    ddlChildNo2.DataSource = dt;
                    ddlChildNo2.DataTextField = "Column";
                    ddlChildNo2.DataValueField = "Column";
                    ddlChildNo2.DataBind();
                    ddlChildNo2.Items.Insert(0, new ListItem("--Select--", "0"));
                    dt = null;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCloseChildNo2_Click(object sender, EventArgs e)
        {
            divChild2.Visible = false;
            ddlChildNo2.Visible = false;
        }

        protected void lbtnCloseChildNo3_Click(object sender, EventArgs e)
        {
            divChild3.Visible = false;
            ddlChildNo3.Visible = false;
        }

        protected void lbtnCloseChildNo4_Click(object sender, EventArgs e)
        {
            divChild4.Visible = false;
            ddlChildNo4.Visible = false;
        }

        protected void lbtnCloseChildNo5_Click(object sender, EventArgs e)
        {
            divChild5.Visible = false;
            ddlChildNo5.Visible = false;
        }

        protected void lbtnCloseChildNo6_Click(object sender, EventArgs e)
        {
            divChild6.Visible = false;
            ddlChildNo6.Visible = false;
        }

        protected void lbtnCloseChildNo7_Click(object sender, EventArgs e)
        {
            divChild7.Visible = false;
            ddlChildNo7.Visible = false;
        }

        protected void lbtnCloseChildNo8_Click(object sender, EventArgs e)
        {
            divChild8.Visible = false;
            ddlChildNo8.Visible = false;
        }

        protected void lbtnCloseChildNo9_Click(object sender, EventArgs e)
        {
            divChild9.Visible = false;
            ddlChildNo9.Visible = false;
        }

        protected void lbtnCloseChildNo10_Click(object sender, EventArgs e)
        {
            divChild10.Visible = false;
            ddlChildNo10.Visible = false;
        }

        protected void lbtnCloseChildNo11_Click(object sender, EventArgs e)
        {
            divChild11.Visible = false;
            ddlChildNo11.Visible = false;
        }

        protected void lbtnCloseChildNo12_Click(object sender, EventArgs e)
        {
            divChild12.Visible = false;
            ddlChildNo12.Visible = false;
        }

        protected void lbtnCloseChildNo13_Click(object sender, EventArgs e)
        {
            divChild13.Visible = false;
            ddlChildNo13.Visible = false;
        }

        protected void lbtnCloseChildNo14_Click(object sender, EventArgs e)
        {
            divChild14.Visible = false;
            ddlChildNo14.Visible = false;
        }

        protected void lbtnCloseChildNo15_Click(object sender, EventArgs e)
        {
            divChild15.Visible = false;
            ddlChildNo15.Visible = false;
        }

        protected void lbtnCloseChildNo16_Click(object sender, EventArgs e)
        {
            divChild16.Visible = false;
            ddlChildNo16.Visible = false;
        }

        protected void lbtnCloseChildNo17_Click(object sender, EventArgs e)
        {
            divChild17.Visible = false;
            ddlChildNo17.Visible = false;
        }

        protected void lbtnCloseChildNo18_Click(object sender, EventArgs e)
        {
            divChild18.Visible = false;
            ddlChildNo18.Visible = false;
        }

        protected void lbtnCloseChildNo19_Click(object sender, EventArgs e)
        {
            divChild19.Visible = false;
            ddlChildNo19.Visible = false;
        }

        protected void lbtnCloseChildNo20_Click(object sender, EventArgs e)
        {
            divChild20.Visible = false;
            ddlChildNo20.Visible = false;
        }

        protected void grdMap_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    LinkButton lbtnSetPrim = (LinkButton)e.Row.FindControl("lbtnSetPrim");
                    LinkButton lbtnRemovePrim = (LinkButton)e.Row.FindControl("lbtnRemovePrim");

                    if (drv["FIELDNAME"].ToString() == "SUBJECT NO." || drv["FIELDNAME"].ToString() == "VISIT" || drv["FIELDNAME"].ToString() == "INVESTIGATOR ID")
                    {
                        lbtnSetPrim.Visible = false;
                        lbtnRemovePrim.Visible = false;
                    }
                    else
                    {
                        if (drv["PRIM"].ToString() == "True")
                        {
                            lbtnSetPrim.Visible = false;
                            lbtnRemovePrim.Visible = true;
                        }
                        else
                        {
                            lbtnSetPrim.Visible = true;
                            lbtnRemovePrim.Visible = false;
                        }
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