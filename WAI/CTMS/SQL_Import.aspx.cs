using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class SQL_Import : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["import"].ConnectionString);
        int[] a = new int[50];
        int[] a1 = new int[50];
        Table tbl = new Table();
        TableHeaderRow thr = new TableHeaderRow();
        TableHeaderCell th = new TableHeaderCell();
        TableHeaderCell th2 = new TableHeaderCell();
        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        TableCell tc2 = new TableCell();

        protected void Pre_Render(object sender, EventArgs e)
        {
            RecreateControlsSpecialChars("ddlSpecialChars", "DropDownList");
            RecreateControls("ddlExcelSheetCols", "DropDownList");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    BindTable();
                    bindgvMappings();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        public void BindTable()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string query = "SELECT '-Select-' as Tables union all SELECT TABLE_NAME as Tables FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            con.Open();
            sda.Fill(ds, "Tables");
            dt = ds.Tables[0];
            con.Close();
            ddlTable.DataSource = dt;
            ddlTable.DataTextField = "Tables";
            ddlTable.DataValueField = "Tables";
            ddlTable.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            DataTable excelData = new DataTable();
            try
            {
                string filename = fileUpload.FileName;
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
                    //fileUpload.SaveAs(filePath);
                    DataTable dtexcel = new DataTable();
                    bool hasHeaders = false;
                    string HDR = hasHeaders ? "Yes" : "No";
                    string strConn;
                    if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                    else
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
                    OleDbConnection conn = new OleDbConnection(strConn);
                    conn.Open();
                    DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    //Looping Total Sheet of Xl File
                    /*foreach (DataRow schemaRow in schemaTable.Rows)
                    {
                    }*/
                    //Looping a first Sheet of Xl File
                    DataRow schemaRow = schemaTable.Rows[0];
                    string sheet = schemaRow["TABLE_NAME"].ToString();
                    if (!sheet.EndsWith("_"))
                    {
                        string query = "SELECT  * FROM [" + sheet + "]";
                        OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                        dtexcel.Locale = CultureInfo.CurrentCulture;
                        daexcel.Fill(dtexcel);
                        excelData = dtexcel;
                        Session["ExcelData"] = dtexcel;
                    }

                    conn.Close();

                    DataTable dtExcelSheet = new DataTable();
                    Session["ExcelSheet"] = dtExcelSheet;
                    dtExcelSheet.Columns.Add("Column", typeof(String));
                    int cols = excelData.Columns.Count;
                    for (int i = 0; i < cols; i++)
                    {
                        dtExcelSheet.Rows.Add(excelData.Columns[i]);
                    }

                    gvExcelCols.DataSource = dtExcelSheet;
                    gvExcelCols.DataBind();

                    ddlExcelCols.DataSource = dtExcelSheet;
                    ddlExcelCols.DataTextField = "Column";
                    ddlExcelCols.DataValueField = "Column";
                    ddlExcelCols.DataBind();
                    ddlExcelCols.Items.Insert(0, "-Select-");

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void ddlTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bindSqlColumns();
                bindConstraints();
            }
            catch (Exception ex)
            {
                con.Close();
                lblError.Text = ex.Message.ToString();
            }
        }

        public void bindSqlColumns()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string query = "SELECT COLUMN_NAME as Columns, DATA_TYPE as DataType, IS_NULLABLE as NullAble FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @Table ORDER BY ORDINAL_POSITION";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Table", ddlTable.SelectedValue.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(ds, "Tables");
                dt = ds.Tables[0];
                con.Close();
                gvSqlColumns.DataSource = dt;
                gvSqlColumns.DataBind();
                ddlSqlCols.DataSource = dt;
                ddlSqlCols.DataTextField = "Columns";
                ddlSqlCols.DataValueField = "Columns";
                ddlSqlCols.DataBind();
                ddlSqlCols.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
            }
        }

        public void bindConstraints()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string query = "SELECT (SELECT COLUMN_NAME from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME=t.CONSTRAINT_NAME ) as Columns , CONSTRAINT_TYPE, CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS t WHERE TABLE_NAME=@Table";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Table", ddlTable.SelectedValue.ToString());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            con.Open();
            sda.Fill(ds, "Tables");
            dt = ds.Tables[0];
            con.Close();
            gvSqlConstraint.DataSource = dt;
            gvSqlConstraint.DataBind();
        }

        protected void chkMerge_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkMerge.Checked == true)
                {
                    divDDLs.Visible = true;
                    divMerge.Visible = true;
                    int cnt1 = FindOccurence("ddlSpecialChars");
                    CreateDropDownListSpecialChars("ddlSpecialChars_" + Convert.ToString(cnt1 + 1));

                    int cnt = FindOccurence("ddlExcelSheetCols");
                    CreateDropDownList("ddlExcelSheetCols_" + Convert.ToString(cnt + 1));

                    lbtnAddMergeCol.Enabled = true;
                }
                else
                {
                    divDDLs.Visible = false;
                    divMerge.Visible = false;
                    lbtnAddMergeCol.Enabled = false;
                    pnlDDLs.Controls.Clear();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        public void bindDatatype()
        {
            ddlDataType.Items.Clear();
            ddlDataType.Items.Insert(0, "-Select-");
            ddlDataType.Items.Insert(1, "bigint");
            ddlDataType.Items.Insert(2, "binary");
            ddlDataType.Items.Insert(3, "bit");
            ddlDataType.Items.Insert(4, "char");
            ddlDataType.Items.Insert(5, "date");
            ddlDataType.Items.Insert(6, "datetime");
            ddlDataType.Items.Insert(7, "datetime2");
            ddlDataType.Items.Insert(8, "datetimeoffset");
            ddlDataType.Items.Insert(9, "decimal");
            ddlDataType.Items.Insert(10, "float");
            ddlDataType.Items.Insert(11, "geography");
            ddlDataType.Items.Insert(12, "geometry");
            ddlDataType.Items.Insert(13, "hierarchyid");
            ddlDataType.Items.Insert(14, "image");
            ddlDataType.Items.Insert(15, "int");
            ddlDataType.Items.Insert(16, "money");
            ddlDataType.Items.Insert(17, "nchar");
            ddlDataType.Items.Insert(18, "ntext");
            ddlDataType.Items.Insert(19, "numeric");
            ddlDataType.Items.Insert(20, "nvarchar");
            ddlDataType.Items.Insert(21, "real");
            ddlDataType.Items.Insert(22, "smalldatetime");
            ddlDataType.Items.Insert(23, "smallint");
            ddlDataType.Items.Insert(24, "smallmoney");
            ddlDataType.Items.Insert(25, "sql_variant");
            ddlDataType.Items.Insert(26, "text");
            ddlDataType.Items.Insert(27, "time");
            ddlDataType.Items.Insert(28, "timestamp");
            ddlDataType.Items.Insert(29, "tinyint");
            ddlDataType.Items.Insert(30, "uniqueidentifier");
            ddlDataType.Items.Insert(31, "varbinary");
            ddlDataType.Items.Insert(32, "varbinary");
            ddlDataType.Items.Insert(33, "varchar");
            ddlDataType.Items.Insert(34, "xml");
        }

        protected void chkAddColumn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAddColumn.Checked == true)
                {
                    txtColumn.Enabled = true;
                    ddlDataType.Enabled = true;
                    lbtnAdd.Enabled = true;
                    bindDatatype();
                }
                else
                {
                    txtColumn.Enabled = false;
                    ddlDataType.Enabled = false;
                    lbtnAdd.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "ALTER TABLE " + ddlTable.SelectedValue.ToString() + " ADD " + txtColumn.Text + " " + ddlDataType.SelectedValue.ToString() + "";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                txtColumn.Text = "";
                bindDatatype();
                bindSqlColumns();
                bindConstraints();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "str", "alert('Column Added Successfully...');", true);
            }
            catch (Exception ex)
            {
                con.Close();
                if (ex.Message.ToString().Contains("Column names in each table must be unique"))
                {
                    lblError.Text = "This column is already exists in " + ddlTable.SelectedValue.ToString() + " Table";
                }
                else
                {
                    lblError.Text = ex.Message.ToString();
                }
            }
        }

        private void CreateDropDownList(string ID)//ID:ddlDynamic-1 parent:0
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = ID;

            DataTable dt = (DataTable)Session["ExcelSheet"];
            ddl.DataSource = dt;
            ddl.DataTextField = "Column";
            ddl.DataValueField = "Column";
            ddl.DataBind();
            ddl.Items.Insert(0, "-Select-");
            ddl.CssClass = "form-control";

            tc.CssClass = "col-md-2";
            tc.Controls.Add(ddl);

            RequiredFieldValidator validate_option = new RequiredFieldValidator();
            validate_option.ControlToValidate = ddl.ID;
            validate_option.ErrorMessage = "*";
            validate_option.ForeColor = Color.Red;
            validate_option.ValidationGroup = "btnSubmit";
            tc.Controls.Add(validate_option);

            Literal lt = new Literal();
            lt.Text = "<br />";
            tc.Controls.Add(lt);

            Label lbl = new Label();
            lbl.Text = "Select Excel Sheet Column : ";

            th.Controls.Clear();
            th.CssClass = "col-md-2";
            th.Controls.Add(lbl);
            thr.Controls.Add(th);
            tbl.Controls.Add(thr);

            tr.Cells.Add(tc);
            tbl.Controls.Add(tr);
            pnlDDLs.Controls.Add(tbl);
        }

        private void CreateDropDownListSpecialChars(string ID)//ID:ddlDynamic-1 parent:0
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = ID;

            ddl.Items.Clear();
            ddl.Items.Insert(0, "-Select-");
            ddl.Items.Insert(1, "!");
            ddl.Items.Insert(2, "#");
            ddl.Items.Insert(3, "$");
            ddl.Items.Insert(4, "%");
            ddl.Items.Insert(5, "&");
            ddl.Items.Insert(6, "*");
            ddl.Items.Insert(7, "+");
            ddl.Items.Insert(8, ",");
            ddl.Items.Insert(9, "-");
            ddl.Items.Insert(10, ".");
            ddl.Items.Insert(11, "/");
            ddl.Items.Insert(12, ":");
            ddl.Items.Insert(13, ";");
            ddl.Items.Insert(14, "<");
            ddl.Items.Insert(15, "=");
            ddl.Items.Insert(16, ">");
            ddl.Items.Insert(17, "?");
            ddl.Items.Insert(18, "@");
            ddl.Items.Insert(19, "[");
            //ddl.Items.Insert(20, "\");
            ddl.Items.Insert(20, "]");
            ddl.Items.Insert(21, "^");
            ddl.Items.Insert(22, "_");
            ddl.Items.Insert(23, "{");
            ddl.Items.Insert(24, "|");
            ddl.Items.Insert(25, "}	");
            ddl.Items.Insert(26, "~");
            ddl.CssClass = "form-control";

            tc2.CssClass = "col-md-2";
            tc2.Controls.Add(ddl);

            RequiredFieldValidator validate_option = new RequiredFieldValidator();
            validate_option.ControlToValidate = ddl.ID;
            validate_option.ErrorMessage = "*";
            validate_option.ForeColor = Color.Red;
            validate_option.ValidationGroup = "btnSubmit";
            tc2.Controls.Add(validate_option);

            Literal lt = new Literal();
            lt.Text = "<br />";
            tc2.Controls.Add(lt);

            Label lbl = new Label();
            lbl.Text = "Select Special Character : ";

            th2.Controls.Clear();
            th2.CssClass = "col-md-2";
            th2.Controls.Add(lbl);
            thr.Controls.Add(th2);
            tbl.Controls.Add(thr);

            tr.Cells.Add(tc2);
            tbl.Controls.Add(tr);
            pnlDDLs.Controls.Add(tbl);

        }

        protected void lbtnAddMergeCol_Click(object sender, EventArgs e)
        {
            int cnt1 = FindOccurence("ddlSpecialChars");
            CreateDropDownListSpecialChars("ddlSpecialChars_" + Convert.ToString(cnt1 + 1));

            int cnt = FindOccurence("ddlExcelSheetCols");
            CreateDropDownList("ddlExcelSheetCols_" + Convert.ToString(cnt + 1));
        }

        private int FindOccurence(string substr)
        {
            //return 2;
            string reqstr = Request.Form.ToString();

            return ((reqstr.Length - reqstr.Replace(substr, "").Length) / substr.Length);
        }

        private void RecreateControls(string ctrlPrefix, string ctrlType)
        {
            string[] ctrls = Request.Form.ToString().Split('&');
            int cnt = FindOccurence(ctrlPrefix);
            if (cnt > 0)
            {
                for (int k = 1; k <= cnt; k++)
                {
                    for (int i = 0; i < ctrls.Length; i++)
                    {
                        if (ctrls[i].Contains(ctrlPrefix + "_" + k.ToString()) && !ctrls[i].Contains("EVENTTARGET"))
                        {
                            string ctrlID = ctrls[i].Split('=')[0];

                            if (ctrlType == "DropDownList")
                            {
                                CreateDropDownList(ctrlID);
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void RecreateControlsSpecialChars(string ctrlPrefix, string ctrlType)
        {
            string[] ctrls = Request.Form.ToString().Split('&');
            int cnt = FindOccurence(ctrlPrefix);
            if (cnt > 0)
            {
                for (int k = 1; k <= cnt; k++)
                {
                    for (int i = 0; i < ctrls.Length; i++)
                    {
                        if (ctrls[i].Contains(ctrlPrefix + "_" + k.ToString()) && !ctrls[i].Contains("EVENTTARGET"))
                        {
                            string ctrlID = ctrls[i].Split('=')[0];

                            if (ctrlType == "DropDownList")
                            {
                                CreateDropDownListSpecialChars(ctrlID);
                            }
                            break;
                        }
                    }
                }
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string DestTable = ddlTable.SelectedValue.ToString();
                string DestColumn = ddlSqlCols.SelectedValue.ToString();
                string SourceCol = ddlExcelCols.SelectedValue.ToString();
                string specialChar = null;
                string excelCols = null;

                if (chkMerge.Checked == true)
                {
                    foreach (TableRow tr123 in tbl.Controls)
                    {
                        foreach (TableCell tc123 in tr123.Controls)
                        {
                            foreach (Control DDLs in tc123.Controls)
                            {
                                if (DDLs is DropDownList)
                                {
                                    if (((DropDownList)DDLs).ID.Contains("ddlSpecialChars"))
                                    {
                                        specialChar += " " + ((DropDownList)DDLs).SelectedValue.ToString();
                                    }
                                    else if (((DropDownList)DDLs).ID.Contains("ddlExcelSheetCols"))
                                    {
                                        excelCols += " " + ((DropDownList)DDLs).SelectedValue.ToString();
                                    }
                                }
                            }
                        }
                    }
                    string[] specialCharArr = specialChar.Split(' ');
                    string[] excelColsArr = excelCols.Split(' ');

                    for (int i = 1; i < specialCharArr.Length; i++)
                    {
                        SourceCol += " " + specialCharArr[i];
                        SourceCol += " " + excelColsArr[i];
                    }
                }
                else
                {
                    SourceCol = ddlExcelCols.SelectedValue.ToString();
                }

                string Sources = SourceCol;

                string query = "INSERT INTO ExcelMappings (DestTable, DestColumn, Source) VALUES (@DestTable, @DestColumn, @Source)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DestTable", DestTable);
                cmd.Parameters.AddWithValue("@DestColumn", DestColumn);
                cmd.Parameters.AddWithValue("@Source", Sources);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                ddlSqlCols.SelectedIndex = 0;
                ddlExcelCols.SelectedIndex = 0;
                chkMerge.Checked = false;
                chkMerge_CheckedChanged(sender, e);
                bindgvMappings();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "str", "alert('Mapping Created Successfully...');", true);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }

        }

        public void bindgvMappings()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string query = "SELECT *,  (SELECT REPLACE(Source,' ',' + ')) AS Sources FROM ExcelMappings";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(ds, "ExcelMappings");
                dt = ds.Tables[0];
                con.Close();
                Session["Mappings"] = dt;
                gvMappings.DataSource = dt;
                gvMappings.DataBind();
            }
            catch (Exception ex)
            {
                con.Close();
                if (ex.Message.ToString() == "Invalid object name 'ExcelMappings'.")
                {
                    try
                    {
                        string query = "CREATE TABLE [dbo].[ExcelMappings]([Id][bigint] IDENTITY(1, 1) NOT NULL, [DestTable] [nvarchar](max) NULL,[DestColumn] [nvarchar](max) NULL,[Source] [nvarchar](max) NULL,CONSTRAINT[PK_ExcelMappings] PRIMARY KEY CLUSTERED([Id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]";
                        SqlCommand cmd = new SqlCommand(query, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        bindgvMappings();
                    }
                    catch (Exception ex1)
                    {
                        lblError.Text = ex1.Message.ToString();
                    }
                }
            }
        }

        protected void gvMappings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string Id = e.CommandArgument.ToString();
                if (e.CommandName == "Del")
                {
                    string query = "DELETE FROM ExcelMappings WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    bindgvMappings();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void gvMappings_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void lbtnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["ExcelSheet"] != null)
                {
                    DataTable ExcelData = (DataTable)Session["ExcelData"];
                    DataTable Mappings = (DataTable)Session["Mappings"];
                    string DestTable = ""; string DestColumn = ""; string Source = ""; string Sources = "";

                    foreach (DataRow drExcel in ExcelData.Rows)
                    {
                        foreach (DataRow drMap in Mappings.Rows)
                        {
                            DestTable = drMap["DestTable"].ToString();
                            DestColumn += drMap["DestColumn"].ToString() + ", ";

                            string SourceColumn = drMap["Source"].ToString();
                            string[] sourceArr = SourceColumn.Split(' ');

                            for (int i = 0; i < sourceArr.Length; i++)
                            {
                                if (IsEven(i))
                                {
                                    Source += drExcel["" + sourceArr[i] + ""];
                                }
                                else if (IsOdd(i))
                                {
                                    Source += sourceArr[i];
                                }
                            }
                            Sources += "'" + Source + "', ";
                            Source = "";
                        }
                        DestColumn = DestColumn.Remove(DestColumn.Length - 2);
                        Sources = Sources.Remove(Sources.Length - 2);
                        RunMappings(DestTable, DestColumn, Sources);
                        Sources = "";
                        DestTable = "";
                        DestColumn = "";
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "str", "alert('Please Upload Excel Sheet...');", true);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "str", "alert('Data Uploaded Successfully...');", true);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        public void RunMappings(string DestTable, string DestColumn, string Source)
        {
            try
            {
                string query = "INSERT INTO " + DestTable + " (" + DestColumn + ") VALUES (" + Source + ")";
                SqlCommand cmd = new SqlCommand(query, con);
                //cmd.Parameters.AddWithValue("@Source", Source);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                lblError.Text = ex.Message.ToString();
            }
        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        //protected void chkSheet_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkSheet.Checked == true)
        //        {
        //            txtSheet.Enabled = true;
        //        }
        //        else
        //        {
        //            txtSheet.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = ex.Message.ToString();
        //    }
        //}
    }
}