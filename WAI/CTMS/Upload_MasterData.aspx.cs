using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace CTMS
{
    public partial class Upload_MasterData : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction Comfun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUploadSponserMaster_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileSponserMaster.HasFile)
                {
                    UploadSponserMaster();
                }
                else
                {
                    Response.Write("<script> alert('Please select file...');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void UploadSponserMaster()
        {
            DataTable excelData = new DataTable();
            try
            {
                string filename = fileSponserMaster.FileName;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable_Sponsor(filename);

                    foreach (DataRow dr in excelData.Rows)
                    {
                        dal.Sponsor_SP(
                        Action: "INSERT_SposnorExcel",
                        Company: dr["Company"].ToString(),
                        ContactNo: dr["ContactNo"].ToString(),
                        Website: dr["Website"].ToString(),
                        Country: dr["Country"].ToString(),
                        State: dr["State"].ToString(),
                        City: dr["City"].ToString(),
                        Address: dr["Address"].ToString(),
                        ENTEREDBY: Session["User_ID"].ToString(),
                        IPADDRESS: Comfun.GetIpAddress()
                        );
                    }

                    Response.Write("<script> alert('Records Uploaded successfully.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public DataTable ConvertExcelToDataTable_Sponsor(string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            fileSponserMaster.SaveAs(savepath + @"\" + FileName);
            string filePath = savepath + @"\" + FileName;
            //fileUpload.SaveAs(filePath);
            DataTable dtexcel = new DataTable();
            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() != ".csv")
            {
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
                    dtResult = dtexcel;
                }
            }
            else
            {
                try
                {
                    string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\\;Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";

                    connection = String.Format(connection, Path.GetDirectoryName(filePath));


                    OleDbDataAdapter csvAdapter;
                    csvAdapter = new OleDbDataAdapter("SELECT * FROM [" + Path.GetFileName(filePath) + "]", connection);

                    if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                    {
                        try
                        {
                            csvAdapter.Fill(dtexcel);
                            if ((dtexcel != null) && (dtexcel.Rows.Count > 0))
                            {
                                dtResult = dtexcel;
                            }
                            else
                            {
                                String msg = "No records found";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(String.Format("Error reading Table {0}.\n{1}", Path.GetFileName(filePath), ex.Message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            return dtResult; //Returning Dattable  
        }

        protected void btnUploadEmpoyeeMaster_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileEmployeeMaster.HasFile)
                {
                    UploadEmpoyeeMaster();
                }
                else
                {
                    Response.Write("<script> alert('Please select file...');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public DataTable ConvertExcelToDataTable_Employee(string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            fileEmployeeMaster.SaveAs(savepath + @"\" + FileName);
            string filePath = savepath + @"\" + FileName;
            //fileUpload.SaveAs(filePath);
            DataTable dtexcel = new DataTable();
            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() != ".csv")
            {
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
                    dtResult = dtexcel;
                }
            }
            else
            {
                try
                {
                    string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\\;Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";

                    connection = String.Format(connection, Path.GetDirectoryName(filePath));


                    OleDbDataAdapter csvAdapter;
                    csvAdapter = new OleDbDataAdapter("SELECT * FROM [" + Path.GetFileName(filePath) + "]", connection);

                    if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                    {
                        try
                        {
                            csvAdapter.Fill(dtexcel);
                            if ((dtexcel != null) && (dtexcel.Rows.Count > 0))
                            {
                                dtResult = dtexcel;
                            }
                            else
                            {
                                String msg = "No records found";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(String.Format("Error reading Table {0}.\n{1}", Path.GetFileName(filePath), ex.Message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            return dtResult; //Returning Dattable  
        }

        public void UploadEmpoyeeMaster()
        {
            DataTable excelData = new DataTable();
            try
            {
                string filename = fileEmployeeMaster.FileName;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable_Employee(filename);

                    foreach (DataRow dr in excelData.Rows)
                    {

                        dal.EmpMaster_SP(Action: "INSERT_EMPEXCEL",
                        EmpCode: dr["EmpCode"].ToString(),
                        FirstName: dr["FirstName"].ToString(),
                        LastName: dr["LastName"].ToString(),
                        EmailID: dr["EmailID"].ToString(),
                        Company: dr["Company"].ToString(),
                        JobTitle: dr["JobTitle"].ToString(),
                        BusinessPhone: dr["BusinessPhone"].ToString(),
                        HomePhone: dr["HomePhone"].ToString(),
                        MobilePhone: dr["MobilePhone"].ToString(),
                        FaxNumber: dr["FaxNumber"].ToString(),
                        Address: dr["Address"].ToString(),
                        City: dr["City"].ToString(),
                        State: dr["State"].ToString(),
                        Postal: dr["PostalCode"].ToString(),
                        Country: dr["Country"].ToString(),
                        Notes: dr["Notes"].ToString(),
                        PersonalEmailId: dr["PersonalEmailID"].ToString(),
                        ENTEREDBY: Session["User_ID"].ToString(),
                        IPADDRESS: Comfun.GetIpAddress()
                        );

                    }

                    Response.Write("<script> alert('Records Uploaded successfully.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUploadSiteMaster_Click(object sender, EventArgs e)
        {
            DataTable excelData = new DataTable();
            try
            {
                string filename = fileSiteMaster.FileName;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable_Site(filename);

                    foreach (DataRow dr in excelData.Rows)
                    {
                        dal.GetSetINSTITUTEDETAILS(
                     Action: "INSERT_INST_MASTEREXCEL",
                     CNTRYID: dr["Country"].ToString(),
                     State: dr["State"].ToString(),
                     INSTNAM: dr["InstituteName"].ToString(),
                     AREA: dr["Address"].ToString(),
                     CITY: dr["City"].ToString(),
                     ENTEREDBY: Session["User_ID"].ToString(),
                     Website: dr["WebSite"].ToString(),
                     IPADDRESS: Comfun.GetIpAddress()
                     );
                    }

                    Response.Write("<script> alert('Records Uploaded successfully.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public DataTable ConvertExcelToDataTable_Site(string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            fileSiteMaster.SaveAs(savepath + @"\" + FileName);
            string filePath = savepath + @"\" + FileName;
            //fileUpload.SaveAs(filePath);
            DataTable dtexcel = new DataTable();
            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() != ".csv")
            {
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
                    dtResult = dtexcel;
                }
            }
            else
            {
                try
                {
                    string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\\;Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";

                    connection = String.Format(connection, Path.GetDirectoryName(filePath));


                    OleDbDataAdapter csvAdapter;
                    csvAdapter = new OleDbDataAdapter("SELECT * FROM [" + Path.GetFileName(filePath) + "]", connection);

                    if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                    {
                        try
                        {
                            csvAdapter.Fill(dtexcel);
                            if ((dtexcel != null) && (dtexcel.Rows.Count > 0))
                            {
                                dtResult = dtexcel;
                            }
                            else
                            {
                                String msg = "No records found";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(String.Format("Error reading Table {0}.\n{1}", Path.GetFileName(filePath), ex.Message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            return dtResult; //Returning Dattable  
        }

        protected void btnUploadInvMaster_Click(object sender, EventArgs e)
        {
            DataTable excelData = new DataTable();
            try
            {
                string filename = fileINVMaster.FileName;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable_Investigator(filename);

                    foreach (DataRow dr in excelData.Rows)
                    {
                        DAL dal;
                        dal = new DAL();

                        DataSet ds = dal.GetSetINVDETAILS(
                       Action: "INSERT_INV_MASTEREXCEL",
                       INVNAME: dr["InvName"].ToString(),
                       INVQUAL: dr["Qualification"].ToString(),
                       INSTNAME: dr["SiteName"].ToString(),
                       INVSPEC: dr["Speciality"].ToString(),
                       MOBNO: dr["MobileNo"].ToString(),
                       ADDRESS: dr["Address"].ToString(),
                       TELNO: dr["TelNo"].ToString(),
                       FAXNO: dr["FaxNo"].ToString(),
                       EMAILID: dr["EmailId"].ToString(),
                       STATUS: "ACTIVE",
                       ENTEREDBY: Session["User_ID"].ToString(),
                       State: dr["State"].ToString(),
                       City: dr["City"].ToString(),
                       Project_ID: dr["Project_ID"].ToString(),
                       INVID: dr["INVID"].ToString(),
                       CNTRYID: dr["Country"].ToString(),
                       CONTTM: dr["ContactTime"].ToString(),
                       IPADDRESS: Comfun.GetIpAddress()
                    );


                    }

                    Response.Write("<script> alert('Records Uploaded successfully.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public DataTable ConvertExcelToDataTable_Investigator(string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            fileINVMaster.SaveAs(savepath + @"\" + FileName);
            string filePath = savepath + @"\" + FileName;
            //fileUpload.SaveAs(filePath);
            DataTable dtexcel = new DataTable();
            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() != ".csv")
            {
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
                    dtResult = dtexcel;
                }
            }
            else
            {
                try
                {
                    string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\\;Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";

                    connection = String.Format(connection, Path.GetDirectoryName(filePath));


                    OleDbDataAdapter csvAdapter;
                    csvAdapter = new OleDbDataAdapter("SELECT * FROM [" + Path.GetFileName(filePath) + "]", connection);

                    if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                    {
                        try
                        {
                            csvAdapter.Fill(dtexcel);
                            if ((dtexcel != null) && (dtexcel.Rows.Count > 0))
                            {
                                dtResult = dtexcel;
                            }
                            else
                            {
                                String msg = "No records found";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(String.Format("Error reading Table {0}.\n{1}", Path.GetFileName(filePath), ex.Message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            return dtResult; //Returning Dattable  
        }

        protected void btnancelSponserMaster_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Upload_MasterData.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUploadINVTEAMMEM_Click(object sender, EventArgs e)
        {
            DataTable excelData = new DataTable();
            try
            {
                string filename = fileINVMEMBER.FileName;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable_InvMember(filename);

                    foreach (DataRow dr in excelData.Rows)
                    {
                        dal.GetSetINVDETAILS(
                Action: "INSERT_TeamEXCEL",
                FirstName: dr["FirstName"].ToString(),
                LastName: dr["LastName"].ToString(),
                EMAILID: dr["EmailAddress"].ToString(),
                MOBNO: dr["MobilePhone"].ToString(),
                CNTRYID: dr["Country"].ToString(),
                State: dr["State"].ToString(),
                City: dr["City"].ToString(),
                ADDRESS: dr["Address"].ToString(),
                ZIP: dr["ZIP/PostalCode"].ToString(),
                Department: dr["Department"].ToString(),
                Designation: dr["Role"].ToString(),
                Notes: dr["Notes"].ToString(),
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()

                );
                    }

                    Response.Write("<script> alert('Records Uploaded successfully.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public DataTable ConvertExcelToDataTable_InvMember(string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            fileINVMEMBER.SaveAs(savepath + @"\" + FileName);
            string filePath = savepath + @"\" + FileName;
            //fileUpload.SaveAs(filePath);
            DataTable dtexcel = new DataTable();
            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() != ".csv")
            {
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
                    dtResult = dtexcel;
                }
            }
            else
            {
                try
                {
                    string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\\;Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";

                    connection = String.Format(connection, Path.GetDirectoryName(filePath));


                    OleDbDataAdapter csvAdapter;
                    csvAdapter = new OleDbDataAdapter("SELECT * FROM [" + Path.GetFileName(filePath) + "]", connection);

                    if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                    {
                        try
                        {
                            csvAdapter.Fill(dtexcel);
                            if ((dtexcel != null) && (dtexcel.Rows.Count > 0))
                            {
                                dtResult = dtexcel;
                            }
                            else
                            {
                                String msg = "No records found";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(String.Format("Error reading Table {0}.\n{1}", Path.GetFileName(filePath), ex.Message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            return dtResult; //Returning Dattable  
        }

        protected void btnuploadEthicsCommity_Click(object sender, EventArgs e)
        {
            DataTable excelData = new DataTable();
            try
            {
                string filename = fileEthicsCommity.FileName;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable_EthicCommity(filename);

                    foreach (DataRow dr in excelData.Rows)
                    {
                        DataSet ds = dal.GetSetEthicsCommityDETAILS(
                Action: "INSERT_ETHICS_MASTEREXCEL",
                INSTID: dr["Institute"].ToString(),
                ETHICSNAME: dr["Ethics Commity Name"].ToString(),
                ETHICSQUAL: dr["Ethics Commity Qualification"].ToString(),
                ETHICSSPEC: dr["Ethics Commity Specification"].ToString(),
                MOBNO: dr["MobilePhone"].ToString(),
                ADDRESS: dr["Address"].ToString(),
                TELNO: dr["TelNo"].ToString(),
                FAXNO: dr["FaxNo"].ToString(),
                EMAILID: dr["EmailId"].ToString(),
                STATUS: "ACTIVE",
                ENTEREDBY: Session["User_ID"].ToString(),
                State: dr["State"].ToString(),
                City: dr["City"].ToString(),
                CNTRYID: dr["Country"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()
                  );
                    }

                    Response.Write("<script> alert('Records Uploaded successfully.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public DataTable ConvertExcelToDataTable_EthicCommity(string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            fileEthicsCommity.SaveAs(savepath + @"\" + FileName);
            string filePath = savepath + @"\" + FileName;
            //fileUpload.SaveAs(filePath);
            DataTable dtexcel = new DataTable();
            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() != ".csv")
            {
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
                    dtResult = dtexcel;
                }
            }
            else
            {
                try
                {
                    string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\\;Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";

                    connection = String.Format(connection, Path.GetDirectoryName(filePath));


                    OleDbDataAdapter csvAdapter;
                    csvAdapter = new OleDbDataAdapter("SELECT * FROM [" + Path.GetFileName(filePath) + "]", connection);

                    if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                    {
                        try
                        {
                            csvAdapter.Fill(dtexcel);
                            if ((dtexcel != null) && (dtexcel.Rows.Count > 0))
                            {
                                dtResult = dtexcel;
                            }
                            else
                            {
                                String msg = "No records found";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(String.Format("Error reading Table {0}.\n{1}", Path.GetFileName(filePath), ex.Message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            return dtResult; //Returning Dattable  
        }

        protected void btnUploadEthicsCommityMem_Click(object sender, EventArgs e)
        {
            DataTable excelData = new DataTable();
            try
            {
                string filename = fileEthicsCommityMem.FileName;
                if (filename != "")
                {
                    excelData = ConvertExcelToDataTable_EthicCommityMem(filename);

                    foreach (DataRow dr in excelData.Rows)
                    {
                        dal.GetSetEthicsCommityDETAILS(
                Action: "INSERT_TeamEXCEL",
                ID: dr["EthicsNAME"].ToString(),
                FirstName: dr["FirstName"].ToString(),
                LastName: dr["LastName"].ToString(),
                EMAILID: dr["EmailId"].ToString(),
                MOBNO: dr["MobilePhone"].ToString(),
                CNTRYID: dr["Country"].ToString(),
                State: dr["State"].ToString(),
                City: dr["City"].ToString(),
                ADDRESS: dr["Address"].ToString(),
                ZIP: dr["ZIP/Postal Code"].ToString(),
                Department: dr["Department"].ToString(),
                Designation: dr["Role"].ToString(),
                ENTEREDBY: Session["User_ID"].ToString(),
                Notes: dr["Notes"].ToString(),
                IPADDRESS: Comfun.GetIpAddress()
                ) ;
                    }

                    Response.Write("<script> alert('Records Uploaded successfully.');</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public DataTable ConvertExcelToDataTable_EthicCommityMem(string FileName)
        {
            DataTable dtResult = null;
            string tempPath = "ExcelData";
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo info = new DirectoryInfo(Server.MapPath(tempPath));
                info.Create();
            }
            string savepath = Server.MapPath(tempPath);
            fileEthicsCommityMem.SaveAs(savepath + @"\" + FileName);
            string filePath = savepath + @"\" + FileName;
            //fileUpload.SaveAs(filePath);
            DataTable dtexcel = new DataTable();
            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() != ".csv")
            {
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
                    dtResult = dtexcel;
                }
            }
            else
            {
                try
                {
                    string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\\;Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";

                    connection = String.Format(connection, Path.GetDirectoryName(filePath));


                    OleDbDataAdapter csvAdapter;
                    csvAdapter = new OleDbDataAdapter("SELECT * FROM [" + Path.GetFileName(filePath) + "]", connection);

                    if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                    {
                        try
                        {
                            csvAdapter.Fill(dtexcel);
                            if ((dtexcel != null) && (dtexcel.Rows.Count > 0))
                            {
                                dtResult = dtexcel;
                            }
                            else
                            {
                                String msg = "No records found";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(String.Format("Error reading Table {0}.\n{1}", Path.GetFileName(filePath), ex.Message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            return dtResult; //Returning Dattable  
        }
    }
}