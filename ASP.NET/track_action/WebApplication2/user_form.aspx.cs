using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using ClosedXML.Excel;
using System.Data.OleDb;
using System.Text;
using OfficeOpenXml;
namespace WebApplication2
{
    public partial class user_form : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        int flgchk;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Console.WriteLine("Hello");
                EmployeeRecord();
                ViewState["Originalvalue"] = txt_empname.Text;
                //this.TriggerActionByUser(txt_username.Text.Trim());
            }
            flgchk = 0;

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        void InsertRecordToDB() 
        {            
            
            SetContext(con, txt_username.Text.Trim());
            try
            {
                cmd.Parameters.AddWithValue("@EmpName", txt_empname.Text);
                cmd.Parameters.AddWithValue("@EmpAddr", txt_empaddr.Text);
                cmd.Parameters.AddWithValue("@Empemailid", txt_emailid.Text);
                cmd.Parameters.AddWithValue("@EmpEdu", txt_empedu.Text);
                cmd.Parameters.AddWithValue("@EmpAge", Convert.ToInt32(txt_empage.Text));
                cmd.CommandText = "Insert Into tblEmployeeInfo(EmpName,EmpAddr,EmailId,EmpEduc,EmpAge)" + " Values (@EmpName,@EmpAddr,@Empemailid,@EmpEdu,@EmpAge);";
                //cmd.CommandText = "Insert Into tblEmployeeInfo(EmpName,EmpAddr,EmailId,EmpEduc)" + " Values (@EmpName,@EmpAddr,@Empemailid,@EmpEdu);";
                // SqlCommand cmd = new SqlCommand("InsertProd", con);
                // cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();// used when the uery won't return any value
                cmd.CommandText = "SELECT TOP 1 Empid FROM tblEmployeeInfo ORDER BY Empid DESC;";
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                lbl_msg.Visible = true;
                lbl_msg.Text = "Record Inserted Successfully at position " + id.ToString() + ".";

            }
            catch (Exception ex)
            {
                GridViewRow row = GrdvwEmployee.SelectedRow;
                lbl_msg.Visible = true;
                lbl_msg.Text = ex.StackTrace + "the id = " + row.Cells[1].Text+".......";
            }
            cleardata();
            //lbl_msg.Visible = false;

            con.Close();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        void UpdateRecordToDB() 
        {
            
            SetContext(con, txt_username.Text.Trim());
            GridViewRow row = GrdvwEmployee.SelectedRow;

            //int sid = Convert.ToInt32(lblid.Text);
            cmd.Parameters.AddWithValue("@EmpName", txt_empname.Text);
            cmd.Parameters.AddWithValue("@EmpAddr", txt_empaddr.Text);
            cmd.Parameters.AddWithValue("@EmpEmail", txt_emailid.Text);
            cmd.Parameters.AddWithValue("@EmpEduc", txt_empedu.Text);
            cmd.Parameters.AddWithValue("@EmpAge", Convert.ToInt32(txt_empage.Text));
            cmd.Parameters.AddWithValue("@Empid",Convert.ToInt32(row.Cells[1].Text.ToString()));
            cmd.CommandText = "Update tblEmployeeInfo set EmpName = @EmpName,"+ " EmpAddr = @EmpAddr, EmailId = @EmpEmail,EmpEduc = @EmpEduc,EmpAge = @EmpAge   Where Empid = @Empid ;";
            // SqlCommand cmdd = new SqlCommand("EMP_UpdateRecord", con);
            //cmdd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            cleardata();
            con.Close();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        void DeleteRecordFROMDB() 
        {
            SetContext(con, txt_username.Text.Trim());
            GridViewRow row = GrdvwEmployee.SelectedRow;


            cmd.Parameters.AddWithValue("@Empid", Convert.ToInt32(row.Cells[1].Text.ToString()));
            cmd.CommandText = "Delete from tblEmployeeInfo Where Empid = @Empid";            
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            cleardata();
            con.Close();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected virtual void SetContext(IDbConnection conn, string UserName)
        {
            string currentUserName = UserName;
            string spName = "sp_set_context";
            if (conn != null)
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                IDbDataParameter param = cmd.CreateParameter();
                param.ParameterName = "@username";
                param.DbType = DbType.String;
                param.Size = 255;
                param.Value = currentUserName;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        private void TriggerActionByUser(string userName)
        {
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT  [Empid],[CreatedDate],[Command],[Action] FROM TriggerData  order by CreatedDate desc";//Where InsertedBy = (SELECT ORIGINAL_LOGIN())
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd.Connection = con;
            lbl_msg.Visible = true;
            cmd.Connection = con;
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            gvTriggerView.DataSource = dt;
            gvTriggerView.DataBind();
            gvTriggerView.EmptyDataText = "New User.";
        }

        void cleardata()
        {
            txt_empname.Text = string.Empty;
           txt_empaddr.Text=  string.Empty;
            txt_emailid.Text =   string.Empty;
             txt_empedu.Text = string.Empty;
        }
        protected void btn_DeleteClick(object sender, EventArgs e)
        {
            DeleteRecordFROMDB();
            
        }

        protected void btn_UpdateClick(object sender, EventArgs e)
        {
            UpdateRecordToDB();
        }
        protected void btn_InsertClick(object sender, EventArgs e)
        {
            string originalValue = ViewState["OriginalValue"] as string;
            string updatedValue = txt_empname.Text;
            if(originalValue != updatedValue) {
                lbl_msg.Visible = true;
                lbl_msg.Text = "The Updated Value is: "+updatedValue;
            }
            if (Page.IsValid)
            {
                InsertRecordToDB();
                EmployeeRecord();
                TriggerActionByUser(txt_username.Text.Trim());
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdvwEmployee.SelectedRow;
            lblid.Text = row.Cells[1].Text;
            txt_empname.Text = row.Cells[2].Text;
            txt_empaddr.Text = row.Cells[3].Text;
            txt_emailid.Text = row.Cells[4].Text;
            txt_empedu.Text = row.Cells[5].Text;
            txt_empage.Text = row.Cells[6].Text;
            btnInsert.Visible = false;

        }

        protected void btnClicked_Continue(object sender, EventArgs e)
        {
            Employeeinfo.Visible = true;
            lbluser.Text = txt_username.Text;
            flgchk = 1;
            EmployeeRecord();
            TriggerActionByUser(txt_username.Text.Trim());
        }
        void EmployeeRecord() 
        {
            SqlCommand cmd = new SqlCommand("EmpEntry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Select");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            GrdvwEmployee.DataSource = dt;
            GrdvwEmployee.DataBind();
            
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Code to export data to Excel
            ExportToExcel(GrdvwEmployee);
            //ExportToExcel_db();
        }

        protected void ExportToExcel(GridView GrdvwEmployee)
        {
            Response.Clear();   
            Response.Buffer = true; //buffer output value 
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "UTF-8";
            //Exporting the gridview to excel 
            string filename = "EmployeeDetails_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
           
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; //"application/ms-excel";//"application/vnd.ms-excel"
            Response.AddHeader("content-disposition",string.Format("attachment; filename="+filename));

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage exp_pck = new ExcelPackage()) 
            {
                ExcelWorksheet excelWorksheet = exp_pck.Workbook.Worksheets.Add("Employee Record");
                //Add Header Row
                for (int i =  1; i < GrdvwEmployee.HeaderRow.Cells.Count; i++)
                {
                    excelWorksheet.Cells[1, i].Value = GrdvwEmployee.HeaderRow.Cells[i].Text;
                    //GrdvwEmployee.HeaderRow.Cells[i].Style.Add("background-color", "green");
                    excelWorksheet.Cells[1, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    excelWorksheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green);

                    // Apply border
                    excelWorksheet.Cells[1, i].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    excelWorksheet.Cells[1, i].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    excelWorksheet.Cells[1, i].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    excelWorksheet.Cells[1, i].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                }

                //Add Data Row
                for (int i = 0; i < GrdvwEmployee.Rows.Count; i++) 
                {
                    for (int j = 1; j < GrdvwEmployee.Rows[i].Cells.Count; j++)
                    {
                        excelWorksheet.Cells[i + 2, j].Value = GrdvwEmployee.Rows[i].Cells[j].Text;

                        // Apply border to data cells
                        excelWorksheet.Cells[i + 2, j].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        excelWorksheet.Cells[i + 2, j].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        excelWorksheet.Cells[i + 2, j].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        excelWorksheet.Cells[i + 2, j].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }
                }
                // Auto-fit columns
                excelWorksheet.Cells.AutoFitColumns();
                Response.BinaryWrite(exp_pck.GetAsByteArray());

            }


            Response.End();
        }
        /* protected void ExportToExcel_db() 
         {
             // StringWriter stringWriter = new StringWriter();
             // HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
             //Response.Cache.SetCacheability(HttpCacheability.NoCache);

             using (ExcelEngine excelEngine = new ExcelEngine()) 
             {
                 IApplication iapplication = excelEngine.Application;

             }
         }*/
        protected void btnClearValues() 
        { 
        
        }

    }
}
