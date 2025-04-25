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
using PPT;
using ExcelDataReader;
using CTMS.CommonFunction;
using ClosedXML.Excel;

namespace CTMS
{
    public partial class DM_UPLOAD_MODULE_FIELDS : System.Web.UI.Page
    {
        DAL_DB dal_DB = new DAL_DB();
        DataTable DM_MODULE_FIELD = new DataTable();
        DataTable DM_DRP_DWN_MASTER = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    fileModuleField.Attributes["onchange"] = "UploadFile(this)";
                    Session.Remove("Downloaded");
                    Session.Remove("excelData");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUploadModuleField_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["Downloaded"] == null)
                {
                    UploadModuleField();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnancelModuleField_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DM_UPLOAD_MODULE_FIELDS.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void UploadModuleField()
        {
            DataTable excelData = new DataTable();
            string MSG = "";
            try
            {
                excelData = (DataTable)ViewState["SCRexcelData"];

                if (excelData == null)
                {
                    Response.Write("<script> alert('Please Select File For Upload.');</script>");
                }
                else
                {
                    string ExistsModuleName = "", ExistsVariableName = "";
                    int ROWSCOUNT = 0;
                    DataView view = new DataView(excelData);
                    DataTable distinctModules = view.ToTable(true, drpModulename.SelectedValue, drpDomainName.SelectedValue, drpModuleSeqNo.SelectedValue);

                    DataSet dsMasterData = dal_DB.DB_UPLOAD_SP(ACTION: "GET_MASTER_MODULE_FIELDS");

                    for (int i = 0; i < distinctModules.Rows.Count; i++)
                    {
                        DataSet dsmodulename = dal_DB.DB_UPLOAD_SP(ACTION: "ADD_UPDATE_MODULES",
                                MODULENAME: distinctModules.Rows[i][drpModulename.SelectedValue].ToString().Trim(),
                                DOMAIN: distinctModules.Rows[i][drpDomainName.SelectedValue].ToString().Trim(),
                                MODULE_SEQNO: distinctModules.Rows[i][drpModuleSeqNo.SelectedValue].ToString().Trim()
                                );

                        if (dsmodulename.Tables.Count > 0 && dsmodulename.Tables[0].Rows.Count > 0)
                        {
                            DataRow[] dr = excelData.Select("[Domain Name] = '" + distinctModules.Rows[i][drpDomainName.SelectedValue].ToString().Trim() + "'");

                            for (int j = 0; j < dr.Length; j++)
                            {
                                if (dsmodulename.Tables[0].Rows[0]["FREEZE"].ToString() == "True")
                                {
                                    dr[j]["Upload result"] = "The module is frozen.";

                                    excelData.AcceptChanges();
                                }
                                else
                                {
                                    if (dsMasterData.Tables.Count > 0 && dsMasterData.Tables[0].Rows.Count > 0)
                                    {
                                        DataRow[] dr1 = dsMasterData.Tables[0].Select("DOMAIN = '" + distinctModules.Rows[i][drpDomainName.SelectedValue].ToString().Trim() + "'  AND VARIABLENAME = '" + dr[j][drpVarialbleName.SelectedValue].ToString().Trim() + "'");

                                        if (dr1.Length > 0)
                                        {
                                            dr[j]["Upload result"] = "Variable Name is already present in the database.";

                                            excelData.AcceptChanges();
                                        }
                                        else
                                        {
                                            ROWSCOUNT += 1;

                                            var types = Getcontroltype(dr[j][drpControlType.SelectedValue].ToString().Trim());
                                            string CONTROLTYPE = types.Item1;
                                            string CLASS = types.Item2;
                                            string DATATYPE = types.Item3;

                                            ADD_NEW_ROW_DATA(dsmodulename.Tables[0].Rows[0]["ID"].ToString().Trim(),
                                                dsmodulename.Tables[0].Rows[0]["MODULENAME"].ToString().Trim(),
                                                dr[j][drpVarialbleName.SelectedValue].ToString().Trim(),
                                                dr[j][drpFieldname.SelectedValue].ToString().Trim(),
                                                dr[j][drpFieldSEQNO.SelectedValue].ToString().Trim(),
                                                CONTROLTYPE.Trim(),
                                                CLASS.Trim(),
                                                DATATYPE.Trim(),
                                                dr[j][drpLenght.SelectedValue].ToString().Trim(),
                                                dr[j][drpBold.SelectedValue].ToString().Trim(),
                                                dr[j][drpUnderline.SelectedValue].ToString().Trim(),
                                                dr[j][drpReadonly.SelectedValue].ToString().Trim(),
                                                dr[j][drpMultiline.SelectedValue].ToString().Trim(),
                                                dr[j][drpRequired.SelectedValue].ToString().Trim(),
                                                dr[j][drpInvisible.SelectedValue].ToString().Trim(),
                                                dr[j][drpUppercase.SelectedValue].ToString().Trim(),
                                                dr[j][drpInlist.SelectedValue].ToString().Trim(),
                                                dr[j][drpInlisteditable.SelectedValue].ToString().Trim(),
                                                dr[j][drpLabdefualt.SelectedValue].ToString().Trim(),
                                                dr[j][drpReference.SelectedValue].ToString().Trim(),
                                                dr[j][drpAutonumber.SelectedValue].ToString().Trim(),
                                                dr[j][drpPrefixYN.SelectedValue].ToString().Trim(),
                                                dr[j][drpPrefixdata.SelectedValue].ToString().Trim(),
                                                dr[j][drpCriticalDP.SelectedValue].ToString().Trim(),
                                                dr[j][drpDescription.SelectedValue].ToString().Trim(),
                                                dr[j][drpNonRepetative.SelectedValue].ToString().Trim(),
                                                dr[j][drpMandatory.SelectedValue].ToString().Trim(),
                                                dr[j][drpDefaultData.SelectedValue].ToString().Trim(),
                                                dr[j][drpLinkedfieldprent.SelectedValue].ToString().Trim(),
                                                dr[j][drpLinkedfieldchild.SelectedValue].ToString().Trim(),
                                                dr[j][drpMedOpinion.SelectedValue].ToString().Trim(),
                                                dr[j][drpFormat.SelectedValue].ToString().Trim(),
                                                dr[j][drpAutocode.SelectedValue].ToString().Trim(),
                                                dr[j][drpAutoCodeLibrary.SelectedValue].ToString().Trim(),
                                                dr[j][drpDuplicatescheck.SelectedValue].ToString().Trim(),
                                                dr[j][drpPGLTYPE.SelectedValue].ToString().Trim(),
                                                dr[j][drpSDV.SelectedValue].ToString().Trim()
                                                );

                                            if (dr[j][drpAnswer.SelectedValue].ToString().Trim() != "")
                                            {
                                                ADD_OPTIONS(dr[j][drpVarialbleName.SelectedValue].ToString().Trim(),
                                                    dr[j][drpControlType.SelectedValue].ToString().Trim(),
                                                    dr[j][drpAnswer.SelectedValue].ToString().Trim()
                                                    );
                                            }

                                            dr[j]["Upload result"] = "Variable Uploaded.";

                                            excelData.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        ROWSCOUNT += 1;

                                        var types = Getcontroltype(dr[j][drpControlType.SelectedValue].ToString().Trim());
                                        string CONTROLTYPE = types.Item1;
                                        string CLASS = types.Item2;
                                        string DATATYPE = types.Item3;

                                        ADD_NEW_ROW_DATA(dsmodulename.Tables[0].Rows[0]["ID"].ToString().Trim(),
                                                dsmodulename.Tables[0].Rows[0]["MODULENAME"].ToString().Trim(),
                                                dr[j][drpVarialbleName.SelectedValue].ToString().Trim(),
                                                dr[j][drpFieldname.SelectedValue].ToString().Trim(),
                                                dr[j][drpFieldSEQNO.SelectedValue].ToString().Trim(),
                                                CONTROLTYPE.Trim(),
                                                CLASS.Trim(),
                                                DATATYPE.Trim(),
                                                dr[j][drpLenght.SelectedValue].ToString().Trim(),
                                                dr[j][drpBold.SelectedValue].ToString().Trim(),
                                                dr[j][drpUnderline.SelectedValue].ToString().Trim(),
                                                dr[j][drpReadonly.SelectedValue].ToString().Trim(),
                                                dr[j][drpMultiline.SelectedValue].ToString().Trim(),
                                                dr[j][drpRequired.SelectedValue].ToString().Trim(),
                                                dr[j][drpInvisible.SelectedValue].ToString().Trim(),
                                                dr[j][drpUppercase.SelectedValue].ToString().Trim(),
                                                dr[j][drpInlist.SelectedValue].ToString().Trim(),
                                                dr[j][drpInlisteditable.SelectedValue].ToString().Trim(),
                                                dr[j][drpLabdefualt.SelectedValue].ToString().Trim(),
                                                dr[j][drpReference.SelectedValue].ToString().Trim(),
                                                dr[j][drpAutonumber.SelectedValue].ToString().Trim(),
                                                dr[j][drpPrefixYN.SelectedValue].ToString().Trim(),
                                                dr[j][drpPrefixdata.SelectedValue].ToString().Trim(),
                                                dr[j][drpCriticalDP.SelectedValue].ToString().Trim(),
                                                dr[j][drpDescription.SelectedValue].ToString().Trim(),
                                                dr[j][drpNonRepetative.SelectedValue].ToString().Trim(),
                                                dr[j][drpMandatory.SelectedValue].ToString().Trim(),
                                                dr[j][drpDefaultData.SelectedValue].ToString().Trim(),
                                                dr[j][drpLinkedfieldprent.SelectedValue].ToString().Trim(),
                                                dr[j][drpLinkedfieldchild.SelectedValue].ToString().Trim(),
                                                dr[j][drpMedOpinion.SelectedValue].ToString().Trim(),
                                                dr[j][drpFormat.SelectedValue].ToString().Trim(),
                                                dr[j][drpAutocode.SelectedValue].ToString().Trim(),
                                                dr[j][drpAutoCodeLibrary.SelectedValue].ToString().Trim(),
                                                dr[j][drpDuplicatescheck.SelectedValue].ToString().Trim(),
                                                dr[j][drpPGLTYPE.SelectedValue].ToString().Trim(),
                                                dr[j][drpSDV.SelectedValue].ToString().Trim()
                                                );

                                        if (dr[j][drpAnswer.SelectedValue].ToString().Trim() != "")
                                        {
                                            ADD_OPTIONS(dr[j][drpVarialbleName.SelectedValue].ToString().Trim(),
                                                dr[j][drpControlType.SelectedValue].ToString().Trim(),
                                                dr[j][drpAnswer.SelectedValue].ToString().Trim()
                                                );
                                        }

                                        dr[j]["Upload result"] = "Variable Uploaded.";

                                        excelData.AcceptChanges();
                                    }
                                }
                            }

                            if (DM_MODULE_FIELD.Rows.Count > 0)
                            {
                                blukpublish();
                            }
                        }
                    }

                    SAVE_UPLOAD_FILE(excelData);

                    MSG = "Total " + ROWSCOUNT + " records uploaded successfully";
                    Session["excelData"] = excelData;
                    //Multiple_Export_Excel.ToExcel(excelData, excelData.TableName.ToString() + ".xls", Page.Response);

                    string outputFileName = excelData.TableName.ToString();

                    string script = $@"window.location.href = 'DownloadExcel.aspx?file={outputFileName}';setTimeout(function() {{location.href = 'DM_UPLOAD_MODULE_FIELDS.aspx';}}, 1000);";

                    ScriptManager.RegisterStartupScript(this, GetType(), "downloadAndRefresh", script, true);
                    Session["Downloaded"] = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SAVE_UPLOAD_FILE(DataTable excelData)
        {
            try
            {
                string FileName = excelData.TableName.ToString();

                if (excelData.TableName.Length > 30)
                {
                    excelData.TableName = excelData.TableName.Substring(0, 30);
                }

                //Saved Excel Data with upload status
                byte[] fileData;

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(excelData);

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        fileData = MyMemoryStream.ToArray();
                    }
                }

                dal_DB.DB_UPLOAD_SP(ACTION: "INSERT_UPLOAD_FILE_LOGS",
                   FileName: FileName,
                   ContentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                   fileData: fileData,
                   NEV_MENU_NAME: lblHeader.InnerText
                   );

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected Tuple<string, string, string> Getcontroltype(string controltype)
        {
            string CONTROLTYPE = "", CLASS = "", DATATYPE = "";

            if (controltype == "DATE without Futuristic Date")
            {
                CONTROLTYPE = "TEXTBOX";
                CLASS = "txtDateNoFuture form-control";
                DATATYPE = "DATE";
            }
            else if (controltype == "DATE with Input Mask")
            {
                CONTROLTYPE = "TEXTBOX";
                CLASS = "txtDateMask form-control";
                DATATYPE = "DATE";
            }
            else if (controltype == "DATE")
            {
                CONTROLTYPE = "TEXTBOX";
                CLASS = "txtDate form-control";
                DATATYPE = "DATE";
            }
            else if (controltype == "TIME")
            {
                CONTROLTYPE = "TEXTBOX";
                CLASS = "txtTime form-control";
                DATATYPE = "TIME";
            }
            else if (controltype == "NUMERIC")
            {
                CONTROLTYPE = "TEXTBOX";
                CLASS = "numeric form-control";
                DATATYPE = "NUMBER";
            }
            else if (controltype == "DECIMAL")
            {
                CONTROLTYPE = "TEXTBOX";
                CLASS = "numericdecimal form-control";
                DATATYPE = "FLOAT";
            }
            else if (controltype == "TEXTBOX" || controltype == "CHECKBOX" || controltype == "RADIOBUTTON" || controltype == "DROPDOWN")
            {
                CONTROLTYPE = controltype;
                CLASS = "form-control";
                DATATYPE = "TEXT";
            }
            else if (controltype == "TEXTBOX with Options")
            {
                CONTROLTYPE = "TEXTBOX";
                CLASS = "OptionValues form-control";
                DATATYPE = "TEXT";
            }
            else if (controltype == "HTML TEXTBOX")
            {
                CONTROLTYPE = "TEXTBOX";
                CLASS = "ckeditor";
                DATATYPE = "TEXT";
            }
            else if (controltype == "HEADER")
            {
                CONTROLTYPE = "HEADER";
                CLASS = "";
                DATATYPE = "";
            }

            return Tuple.Create(CONTROLTYPE, CLASS, DATATYPE);
        }

        protected void blukpublish()
        {
            DAL dal = new DAL();
            SqlConnection con = new SqlConnection(dal.getconstr());

            var options = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.KeepIdentity;

            using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), options))
            {
                if (con.State != ConnectionState.Open) { con.Open(); }

                sqlbc.DestinationTableName = "DM_MODULE_FIELD";

                sqlbc.ColumnMappings.Add("MODULEID", "MODULEID");
                sqlbc.ColumnMappings.Add("MODULENAME", "MODULENAME");
                sqlbc.ColumnMappings.Add("VARIABLENAME", "VARIABLENAME");
                sqlbc.ColumnMappings.Add("FIELDNAME", "FIELDNAME");
                sqlbc.ColumnMappings.Add("SEQNO", "SEQNO");
                sqlbc.ColumnMappings.Add("CONTROLTYPE", "CONTROLTYPE");
                sqlbc.ColumnMappings.Add("CLASS", "CLASS");
                sqlbc.ColumnMappings.Add("DATATYPE", "DATATYPE");
                sqlbc.ColumnMappings.Add("MAXLEN", "MAXLEN");
                sqlbc.ColumnMappings.Add("BOLDYN", "BOLDYN");
                sqlbc.ColumnMappings.Add("UNLNYN", "UNLNYN");
                sqlbc.ColumnMappings.Add("READYN", "READYN");
                sqlbc.ColumnMappings.Add("MULTILINEYN", "MULTILINEYN");
                sqlbc.ColumnMappings.Add("REQUIREDYN", "REQUIREDYN");
                sqlbc.ColumnMappings.Add("INVISIBLE", "INVISIBLE");
                sqlbc.ColumnMappings.Add("UPPERCASE", "UPPERCASE");
                sqlbc.ColumnMappings.Add("InList", "InList");
                sqlbc.ColumnMappings.Add("InListEditable", "InListEditable");
                sqlbc.ColumnMappings.Add("LabData", "LabData");
                sqlbc.ColumnMappings.Add("Refer", "Refer");
                sqlbc.ColumnMappings.Add("AutoNum", "AutoNum");
                sqlbc.ColumnMappings.Add("Prefix", "Prefix");
                sqlbc.ColumnMappings.Add("PrefixText", "PrefixText");
                sqlbc.ColumnMappings.Add("Critic_DP", "Critic_DP");
                sqlbc.ColumnMappings.Add("Descrip", "Descrip");
                sqlbc.ColumnMappings.Add("NONREPETATIVE", "NONREPETATIVE");
                sqlbc.ColumnMappings.Add("MANDATORY", "MANDATORY");
                sqlbc.ColumnMappings.Add("DefaultData", "DefaultData");
                sqlbc.ColumnMappings.Add("ParentLinked", "ParentLinked");
                sqlbc.ColumnMappings.Add("ChildLinked", "ChildLinked");
                sqlbc.ColumnMappings.Add("MEDOP", "MEDOP");
                sqlbc.ColumnMappings.Add("FORMAT", "FORMAT");
                sqlbc.ColumnMappings.Add("AUTOCODE", "AUTOCODE");
                sqlbc.ColumnMappings.Add("AutoCodeLIB", "AutoCodeLIB");
                sqlbc.ColumnMappings.Add("DUPLICATE", "DUPLICATE");
                sqlbc.ColumnMappings.Add("PGL_TYPE", "PGL_TYPE");
                sqlbc.ColumnMappings.Add("SDV", "SDV");
                sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");

                sqlbc.WriteToServer(DM_MODULE_FIELD);
                DM_MODULE_FIELD.Clear();
            }
        }

        protected void ADD_NEW_ROW_DATA(string MODULEID, string MODULENAME, string VARIABLENAME, string FIELDNAME, string SEQNO, string CONTROLTYPE, string CLASS, string DATATYPE, string MAXLEN, string BOLDYN, string UNLNYN, string READYN, string MULTILINEYN, string REQUIREDYN, string INVISIBLE, string UPPERCASE, string InList, string InListEditable, string LabData, string Refer, string AutoNum, string Prefix, string PrefixText, string Critic_DP, string Descrip, string NONREPETATIVE, string MANDATORY, string DefaultData, string ParentLinked, string ChildLinked, string MEDOP, string FORMAT, string AUTOCODE, string AutoCodeLIB, string DUPLICATE, string PGL_TYPE, string SDV)
        {
            try
            {
                CREATE_DM_MODULE_FIELD_DT();

                DataRow myDataRow;
                myDataRow = DM_MODULE_FIELD.NewRow();
                myDataRow["MODULEID"] = Convert.ToInt32(MODULEID);
                myDataRow["MODULENAME"] = MODULENAME;
                myDataRow["VARIABLENAME"] = VARIABLENAME;
                myDataRow["FIELDNAME"] = FIELDNAME;
                myDataRow["SEQNO"] = Convert.ToInt32(SEQNO);
                myDataRow["CONTROLTYPE"] = CONTROLTYPE;
                myDataRow["CLASS"] = CLASS;
                myDataRow["DATATYPE"] = DATATYPE;
                myDataRow["MAXLEN"] = Convert.ToInt32(checkintstring(MAXLEN));
                myDataRow["BOLDYN"] = Convert.ToBoolean(checkboolstring(BOLDYN));
                myDataRow["UNLNYN"] = Convert.ToBoolean(checkboolstring(UNLNYN));
                myDataRow["READYN"] = Convert.ToBoolean(checkboolstring(READYN));
                myDataRow["MULTILINEYN"] = Convert.ToBoolean(checkboolstring(MULTILINEYN));
                myDataRow["REQUIREDYN"] = Convert.ToBoolean(checkboolstring(REQUIREDYN));
                myDataRow["INVISIBLE"] = Convert.ToBoolean(checkboolstring(INVISIBLE));
                myDataRow["UPPERCASE"] = Convert.ToBoolean(checkboolstring(UPPERCASE));
                myDataRow["InList"] = Convert.ToBoolean(checkboolstring(InList));
                myDataRow["InListEditable"] = Convert.ToBoolean(checkboolstring(InListEditable));
                myDataRow["LabData"] = Convert.ToBoolean(checkboolstring(LabData));
                myDataRow["Refer"] = Convert.ToBoolean(checkboolstring(Refer));
                myDataRow["AutoNum"] = Convert.ToBoolean(checkboolstring(AutoNum));
                myDataRow["Prefix"] = Convert.ToBoolean(checkboolstring(Prefix));
                myDataRow["PrefixText"] = PrefixText;
                myDataRow["Critic_DP"] = Convert.ToBoolean(checkboolstring(Critic_DP));
                myDataRow["Descrip"] = Descrip;
                myDataRow["NONREPETATIVE"] = Convert.ToBoolean(checkboolstring(NONREPETATIVE));
                myDataRow["MANDATORY"] = Convert.ToBoolean(checkboolstring(MANDATORY));
                myDataRow["DefaultData"] = DefaultData;
                myDataRow["ParentLinked"] = Convert.ToBoolean(checkboolstring(ParentLinked));
                myDataRow["ChildLinked"] = Convert.ToBoolean(checkboolstring(ChildLinked));
                myDataRow["MEDOP"] = Convert.ToBoolean(checkboolstring(MEDOP));
                myDataRow["FORMAT"] = FORMAT;
                myDataRow["AUTOCODE"] = Convert.ToBoolean(checkboolstring(AUTOCODE));
                myDataRow["AutoCodeLIB"] = AutoCodeLIB;
                myDataRow["DUPLICATE"] = Convert.ToBoolean(checkboolstring(DUPLICATE));
                myDataRow["PGL_TYPE"] = PGL_TYPE;
                myDataRow["SDV"] = Convert.ToBoolean(checkboolstring(SDV));
                myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                myDataRow["ENTEREDDAT"] = DateTime.Now;
                myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                DM_MODULE_FIELD.Rows.Add(myDataRow);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public static bool checkboolstring(string s)
        {
            return (s == null || s == String.Empty || s == "0" || s == "No" || s == "NO") ? false : true;
        }

        public static int checkintstring(string s)
        {
            return (s == null || s == String.Empty || s == "0") ? 0 : Convert.ToInt32(s);
        }

        protected void CREATE_DM_MODULE_FIELD_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (DM_MODULE_FIELD.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    DM_MODULE_FIELD.Columns.Add("MODULEID");
                    DM_MODULE_FIELD.Columns.Add("MODULENAME");
                    DM_MODULE_FIELD.Columns.Add("VARIABLENAME");
                    DM_MODULE_FIELD.Columns.Add("FIELDNAME");
                    DM_MODULE_FIELD.Columns.Add("SEQNO");
                    DM_MODULE_FIELD.Columns.Add("CONTROLTYPE");
                    DM_MODULE_FIELD.Columns.Add("CLASS");
                    DM_MODULE_FIELD.Columns.Add("DATATYPE");
                    DM_MODULE_FIELD.Columns.Add("MAXLEN");
                    DM_MODULE_FIELD.Columns.Add("BOLDYN");
                    DM_MODULE_FIELD.Columns.Add("UNLNYN");
                    DM_MODULE_FIELD.Columns.Add("READYN");
                    DM_MODULE_FIELD.Columns.Add("MULTILINEYN");
                    DM_MODULE_FIELD.Columns.Add("REQUIREDYN");
                    DM_MODULE_FIELD.Columns.Add("INVISIBLE");
                    DM_MODULE_FIELD.Columns.Add("UPPERCASE");
                    DM_MODULE_FIELD.Columns.Add("InList");
                    DM_MODULE_FIELD.Columns.Add("InListEditable");
                    DM_MODULE_FIELD.Columns.Add("LabData");
                    DM_MODULE_FIELD.Columns.Add("Refer");
                    DM_MODULE_FIELD.Columns.Add("AutoNum");
                    DM_MODULE_FIELD.Columns.Add("Prefix");
                    DM_MODULE_FIELD.Columns.Add("PrefixText");
                    DM_MODULE_FIELD.Columns.Add("Critic_DP");
                    DM_MODULE_FIELD.Columns.Add("Descrip");
                    DM_MODULE_FIELD.Columns.Add("NONREPETATIVE");
                    DM_MODULE_FIELD.Columns.Add("MANDATORY");
                    DM_MODULE_FIELD.Columns.Add("DefaultData");
                    DM_MODULE_FIELD.Columns.Add("ParentLinked");
                    DM_MODULE_FIELD.Columns.Add("ChildLinked");
                    DM_MODULE_FIELD.Columns.Add("MEDOP");
                    DM_MODULE_FIELD.Columns.Add("FORMAT");
                    DM_MODULE_FIELD.Columns.Add("AUTOCODE");
                    DM_MODULE_FIELD.Columns.Add("AutoCodeLIB");
                    DM_MODULE_FIELD.Columns.Add("DUPLICATE");
                    DM_MODULE_FIELD.Columns.Add("PGL_TYPE");
                    DM_MODULE_FIELD.Columns.Add("SDV");
                    DM_MODULE_FIELD.Columns.Add("ENTEREDBY");
                    DM_MODULE_FIELD.Columns.Add("ENTEREDBYNAME");
                    DM_MODULE_FIELD.Columns.Add("ENTEREDDAT");
                    DM_MODULE_FIELD.Columns.Add("ENTERED_TZVAL");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnMODULE_FIELD_Click(object sender, EventArgs e)
        {
            try
            {
                GET_DRP_COLS(fileModuleField);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void clearModuleField()
        {
            try
            {
                drpModulename.Items.Clear();
                drpModulename.ClearSelection();
                drpDomainName.Items.Clear();
                drpDomainName.ClearSelection();
                drpModuleSeqNo.Items.Clear();
                drpModuleSeqNo.ClearSelection();
                drpFieldname.Items.Clear();
                drpFieldname.ClearSelection();
                drpVarialbleName.Items.Clear();
                drpVarialbleName.ClearSelection();
                drpDescription.Items.Clear();
                drpDescription.ClearSelection();
                drpControlType.Items.Clear();
                drpControlType.ClearSelection();
                drpFormat.Items.Clear();
                drpFormat.ClearSelection();
                drpFieldSEQNO.Items.Clear();
                drpFieldSEQNO.ClearSelection();
                drpLenght.Items.Clear();
                drpLenght.ClearSelection();
                drpUppercase.Items.Clear();
                drpUppercase.ClearSelection();
                drpBold.Items.Clear();
                drpBold.ClearSelection();
                drpUnderline.Items.Clear();
                drpUnderline.ClearSelection();
                drpReadonly.Items.Clear();
                drpReadonly.ClearSelection();
                drpMultiline.Items.Clear();
                drpMultiline.ClearSelection();
                drpRequired.Items.Clear();
                drpRequired.ClearSelection();
                drpInvisible.Items.Clear();
                drpInvisible.ClearSelection();
                drpAutocode.Items.Clear();
                drpAutocode.ClearSelection();
                drpAutoCodeLibrary.Items.Clear();
                drpAutoCodeLibrary.ClearSelection();
                drpInlist.Items.Clear();
                drpInlist.ClearSelection();
                drpInlisteditable.Items.Clear();
                drpInlisteditable.ClearSelection();
                drpLabdefualt.Items.Clear();
                drpLabdefualt.ClearSelection();
                drpAutonumber.Items.Clear();
                drpAutonumber.ClearSelection();
                drpReference.Items.Clear();
                drpReference.ClearSelection();
                drpCriticalDP.Items.Clear();
                drpCriticalDP.ClearSelection();
                drpPrefixYN.Items.Clear();
                drpPrefixYN.ClearSelection();
                drpPrefixdata.Items.Clear();
                drpPrefixdata.ClearSelection();
                drpDuplicatescheck.Items.Clear();
                drpDuplicatescheck.ClearSelection();
                drpNonRepetative.Items.Clear();
                drpNonRepetative.ClearSelection();
                drpMandatory.Items.Clear();
                drpMandatory.ClearSelection();
                drpDefaultData.Items.Clear();
                drpDefaultData.ClearSelection();
                drpLinkedfieldprent.Items.Clear();
                drpLinkedfieldprent.ClearSelection();
                drpLinkedfieldchild.Items.Clear();
                drpLinkedfieldchild.ClearSelection();
                drpMedOpinion.Items.Clear();
                drpMedOpinion.ClearSelection();
                drpAnswer.Items.Clear();
                drpAnswer.ClearSelection();
                drpPGLTYPE.Items.Clear();
                drpPGLTYPE.ClearSelection();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        // Method to check if the file is locked
        public bool IsFileLocked(string filePath)
        {
            FileStream fileStream = null;
            try
            {
                // Attempt to open the file with exclusive access to check if it's locked
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return false; // If we can open it, the file is not locked
            }
            catch (IOException)
            {
                return true; // If we cannot open it due to IOException, the file is locked
            }
            finally
            {
                // Close the file if it was successfully opened
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }
        private void GET_DRP_COLS(FileUpload fileUpload)
        {
            try
            {
                DataTable excelData = new DataTable();
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
                    if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() != ".csv")
                    {
                        if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                        else
                            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

                        //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                        //    ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";


                        //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                        //     ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                        OleDbConnection conn = new OleDbConnection(strConn);
                        conn.Open();
                        DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        //Looping Total Sheet of Xl File

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

                            // ✅ Check if the sheet contains data
                            if (excelData.Rows.Count == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' No Data Found.'); window.location.href = '" + Request.RawUrl.ToString() + "';", true);
                                return;
                            }


                            bool isColumnBlank = false; // Flag to track blank cells

                            // Ensure the DataTable has at least one row
                            if (excelData.Rows.Count > 0)
                            {
                                // Get the actual column count based on headers
                                int headerCount = 0;

                                foreach (DataRow row in excelData.Rows)
                                {
                                    // Check if the first column is blank
                                    if (string.IsNullOrWhiteSpace(row[0].ToString()))
                                    {
                                        isColumnBlank = true; // Found a blank first column
                                        break; // Stop checking further
                                    }
                                }
                            }

                            // Show alert if the first column is blank, otherwise proceed
                            if (isColumnBlank)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data Found!'); window.location.href = '" + Request.RawUrl.ToString() + "' ", true);
                            }
                        }
                        conn.Close();
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
                                        excelData = dtexcel;
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

                    excelData.TableName = filename;
                    excelData.Columns.Add("Upload result");

                    DataTable dtExcelSheet = new DataTable();
                    dtExcelSheet.Columns.Add("Column", typeof(String));

                    int cols = excelData.Columns.Count;
                    for (int i = 0; i < cols; i++)
                    {
                        dtExcelSheet.Rows.Add(excelData.Columns[i]);
                    }

                    BIND_DRP_COLS(drpModulename, dtExcelSheet);
                    BIND_DRP_COLS(drpDomainName, dtExcelSheet);
                    BIND_DRP_COLS(drpModuleSeqNo, dtExcelSheet);
                    BIND_DRP_COLS(drpFieldname, dtExcelSheet);
                    BIND_DRP_COLS(drpVarialbleName, dtExcelSheet);
                    BIND_DRP_COLS(drpDescription, dtExcelSheet);
                    BIND_DRP_COLS(drpControlType, dtExcelSheet);
                    BIND_DRP_COLS(drpFieldSEQNO, dtExcelSheet);
                    BIND_DRP_COLS(drpLenght, dtExcelSheet);
                    BIND_DRP_COLS(drpUppercase, dtExcelSheet);
                    BIND_DRP_COLS(drpBold, dtExcelSheet);
                    BIND_DRP_COLS(drpUnderline, dtExcelSheet);
                    BIND_DRP_COLS(drpReadonly, dtExcelSheet);
                    BIND_DRP_COLS(drpMultiline, dtExcelSheet);
                    BIND_DRP_COLS(drpRequired, dtExcelSheet);
                    BIND_DRP_COLS(drpInvisible, dtExcelSheet);
                    BIND_DRP_COLS(drpAutocode, dtExcelSheet);
                    BIND_DRP_COLS(drpAutoCodeLibrary, dtExcelSheet);
                    BIND_DRP_COLS(drpInlist, dtExcelSheet);
                    BIND_DRP_COLS(drpInlisteditable, dtExcelSheet);
                    BIND_DRP_COLS(drpLabdefualt, dtExcelSheet);
                    BIND_DRP_COLS(drpAutonumber, dtExcelSheet);
                    BIND_DRP_COLS(drpReference, dtExcelSheet);
                    BIND_DRP_COLS(drpCriticalDP, dtExcelSheet);
                    BIND_DRP_COLS(drpPrefixYN, dtExcelSheet);
                    BIND_DRP_COLS(drpPrefixdata, dtExcelSheet);
                    BIND_DRP_COLS(drpDuplicatescheck, dtExcelSheet);
                    BIND_DRP_COLS(drpNonRepetative, dtExcelSheet);
                    BIND_DRP_COLS(drpMandatory, dtExcelSheet);
                    BIND_DRP_COLS(drpDefaultData, dtExcelSheet);
                    BIND_DRP_COLS(drpLinkedfieldprent, dtExcelSheet);
                    BIND_DRP_COLS(drpLinkedfieldchild, dtExcelSheet);
                    BIND_DRP_COLS(drpMedOpinion, dtExcelSheet);
                    BIND_DRP_COLS(drpAnswer, dtExcelSheet);
                    BIND_DRP_COLS(drpPGLTYPE, dtExcelSheet);
                    BIND_DRP_COLS(drpSDV, dtExcelSheet);
                    BIND_DRP_COLS(drpFormat, dtExcelSheet);

                    ViewState["SCRexcelData"] = excelData;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BIND_DRP_COLS(DropDownList drp, DataTable dt)
        {
            try
            {
                drp.DataSource = dt;
                drp.DataValueField = "Column";
                drp.DataTextField = "Column";
                drp.DataBind();
                drp.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ADD_OPTIONS(string VARIABLENAME, string CONTROLTYPE, string OPTIONS)
        {
            try
            {
                CREATE_DM_DRP_DWN_MASTER_DT();

                if (CONTROLTYPE == "DROPDOWN")
                {
                    DataRow myDataRow;
                    myDataRow = DM_DRP_DWN_MASTER.NewRow();
                    myDataRow["VARIABLE_NAME"] = VARIABLENAME;
                    myDataRow["VALUE"] = "--Select--";
                    myDataRow["TEXT"] = "--Select--";
                    myDataRow["SEQNO"] = 0;
                    myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                    myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                    myDataRow["ENTEREDDAT"] = DateTime.Now;
                    myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                    DM_DRP_DWN_MASTER.Rows.Add(myDataRow);
                }

                int SEQNO = 0;
                string[] drFieldsOptions = OPTIONS.Split('$');

                foreach (string Splitoptions in drFieldsOptions)
                {
                    SEQNO += 1;

                    DataRow myDataRow;
                    myDataRow = DM_DRP_DWN_MASTER.NewRow();
                    myDataRow["VARIABLE_NAME"] = VARIABLENAME;
                    myDataRow["VALUE"] = Splitoptions;
                    myDataRow["TEXT"] = Splitoptions;
                    myDataRow["SEQNO"] = SEQNO;
                    myDataRow["ENTEREDBY"] = Session["User_ID"].ToString();
                    myDataRow["ENTEREDBYNAME"] = Session["User_Name"].ToString();
                    myDataRow["ENTEREDDAT"] = DateTime.Now;
                    myDataRow["ENTERED_TZVAL"] = Session["TimeZone_Value"].ToString();
                    DM_DRP_DWN_MASTER.Rows.Add(myDataRow);
                }

                DAL dal = new DAL();
                SqlConnection con = new SqlConnection(dal.getconstr());

                var options = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.KeepIdentity;

                using (SqlBulkCopy sqlbc = new SqlBulkCopy(con.ConnectionString.ToString(), options))
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    sqlbc.DestinationTableName = "DM_DRP_DWN_MASTER";

                    sqlbc.ColumnMappings.Add("VARIABLE_NAME", "VARIABLE_NAME");
                    sqlbc.ColumnMappings.Add("VALUE", "VALUE");
                    sqlbc.ColumnMappings.Add("TEXT", "TEXT");
                    sqlbc.ColumnMappings.Add("SEQNO", "SEQNO");
                    sqlbc.ColumnMappings.Add("ENTEREDBY", "ENTEREDBY");
                    sqlbc.ColumnMappings.Add("ENTEREDBYNAME", "ENTEREDBYNAME");
                    sqlbc.ColumnMappings.Add("ENTEREDDAT", "ENTEREDDAT");
                    sqlbc.ColumnMappings.Add("ENTERED_TZVAL", "ENTERED_TZVAL");

                    sqlbc.WriteToServer(DM_DRP_DWN_MASTER);
                    DM_DRP_DWN_MASTER.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void CREATE_DM_DRP_DWN_MASTER_DT()
        {
            try
            {
                DataColumn dtColumn;

                if (DM_DRP_DWN_MASTER.Columns.Count == 0)
                {
                    // Create Name column.
                    dtColumn = new DataColumn();
                    DM_DRP_DWN_MASTER.Columns.Add("VARIABLE_NAME");
                    DM_DRP_DWN_MASTER.Columns.Add("VALUE");
                    DM_DRP_DWN_MASTER.Columns.Add("TEXT");
                    DM_DRP_DWN_MASTER.Columns.Add("SEQNO");
                    DM_DRP_DWN_MASTER.Columns.Add("ENTEREDBY");
                    DM_DRP_DWN_MASTER.Columns.Add("ENTEREDBYNAME");
                    DM_DRP_DWN_MASTER.Columns.Add("ENTEREDDAT");
                    DM_DRP_DWN_MASTER.Columns.Add("ENTERED_TZVAL");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnExportSampleFile_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DB.DB_UPLOAD_SP(ACTION: "EXPORT_Modules_Fields_SAMPLE_FILE");

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = ds.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds.Tables[i].Copy());
                    i++;
                }

                dal_DB.DB_DOWNLOAD_LOGS_SP(ACTION: "INSERT_DOWNLOAD_LOGS",
                    FIELNAME: "Module & Fields Sample File.xls",
                    FUNCTIONNAME: "Module & Fields Sample File",
                    PAGENAME: Session["menu"].ToString()
                    );

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Module & Fields Sample File_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                Multiple_Export_Excel.ToExcel_Restricted(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public string GetCurrentPageName()
        {
            string sPath = Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }
    }
}