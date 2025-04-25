using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PPT;

namespace CTMS.CommonFunction
{
    public class DAL_eTMF
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);


        public DataSet eTMF_SPECS_SP(string ACTION = null, string ID = null, string BLINDED = null, string UNBLINDED = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("eTMF_SPECS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@BLINDED", BLINDED);
                cmd.Parameters.AddWithValue("@UNBLINDED", UNBLINDED);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;

        }

        public DataSet eTMF_UPLOAD_SP(string ACTION = null, string FOLDERID = null, string SUBFOLDERID = null, string DeadlineDate = null,
            string ARTIFACTS = null, string DOCTYPEID = null, string RefNo = null, string UploadFileName = null, string DocName = null,
            string CountryID = null, string SiteID = null, string SEQNO = null, string USERID = null, string VersionID = null,
            string PROJECTID = null, string DocID = null, string Status = null, string NOTE = null, string ExpiryDate = null,
            string UploadSubTaskId = null, string UploadDepartmentId = null, string UploadTaskId = null, string UploadSectionId = null,
            string UploadArtifactId = null, string AutoNomenclature = null, string UploadFileName_Editable = null, string Functions = null,
            string FileType_Editable = null, string SubStatus = null, string REFRENCE_DOC = null, string SUBJID = null,
            string SysFileName = null, bool SendEmail = false, string Size_Editable = null, string ToEmailIDs = null,
            string DOC_REMINDERDAT = null, string CCEmailIDs = null, string SPEC = null, string Size = null, string UploadDateTime = null,
            string ReplaceFile = null, string FileType = null, string DOC_VERSIONNO = null, string UniqueRefNo = null,
            string UploadDocTypeid = null, string INDIVIDUAL = null, string UploadZoneId = null, string DOC_DATETIME = null,
            string UploadBy = null, string ID = null, string UPDATEREASON = null, string RECEIPTDAT = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                cmd = new SqlCommand("eTMF_UPLOAD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@FOLDERID", FOLDERID);
                cmd.Parameters.AddWithValue("@SUBFOLDERID", SUBFOLDERID);
                cmd.Parameters.AddWithValue("@DeadlineDate", DeadlineDate);
                cmd.Parameters.AddWithValue("@ARTIFACTS", ARTIFACTS);
                cmd.Parameters.AddWithValue("@DOCTYPEID", DOCTYPEID);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                cmd.Parameters.AddWithValue("@UploadFileName", UploadFileName);
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@CountryID", CountryID);
                cmd.Parameters.AddWithValue("@SiteID", SiteID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@VersionID", VersionID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@DocID", DocID);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@NOTE", NOTE);
                cmd.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                cmd.Parameters.AddWithValue("@UploadSubTaskId", UploadSubTaskId);
                cmd.Parameters.AddWithValue("@UploadDepartmentId", UploadDepartmentId);
                cmd.Parameters.AddWithValue("@UploadTaskId", UploadTaskId);
                cmd.Parameters.AddWithValue("@UploadSectionId", UploadSectionId);
                cmd.Parameters.AddWithValue("@UploadArtifactId", UploadArtifactId);
                cmd.Parameters.AddWithValue("@AutoNomenclature", AutoNomenclature);
                cmd.Parameters.AddWithValue("@UploadFileName_Editable", UploadFileName_Editable);
                cmd.Parameters.AddWithValue("@Functions", Functions);
                cmd.Parameters.AddWithValue("@FileType_Editable", FileType_Editable);
                cmd.Parameters.AddWithValue("@SubStatus", SubStatus);
                cmd.Parameters.AddWithValue("@REFRENCE_DOC", REFRENCE_DOC);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SysFileName", SysFileName);
                cmd.Parameters.AddWithValue("@SendEmail", SendEmail);
                cmd.Parameters.AddWithValue("@Size_Editable", Size_Editable);
                cmd.Parameters.AddWithValue("@ToEmailIDs", ToEmailIDs);
                cmd.Parameters.AddWithValue("@DOC_REMINDERDAT", DOC_REMINDERDAT);
                cmd.Parameters.AddWithValue("@CCEmailIDs", CCEmailIDs);
                cmd.Parameters.AddWithValue("@SPEC", SPEC);
                cmd.Parameters.AddWithValue("@Size", Size);
                cmd.Parameters.AddWithValue("@UploadDateTime", UploadDateTime);
                cmd.Parameters.AddWithValue("@ReplaceFile", ReplaceFile);
                cmd.Parameters.AddWithValue("@FileType", FileType);
                cmd.Parameters.AddWithValue("@DOC_VERSIONNO", DOC_VERSIONNO);
                cmd.Parameters.AddWithValue("@UniqueRefNo", UniqueRefNo);
                cmd.Parameters.AddWithValue("@UploadDocTypeid", UploadDocTypeid);
                cmd.Parameters.AddWithValue("@INDIVIDUAL", INDIVIDUAL);
                cmd.Parameters.AddWithValue("@UploadZoneId", UploadZoneId);
                cmd.Parameters.AddWithValue("@DOC_DATETIME", DOC_DATETIME);
                cmd.Parameters.AddWithValue("@UploadBy", UploadBy);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@RECEIPTDAT", RECEIPTDAT);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@UPDATEREASON", UPDATEREASON);


                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet eTMF_SET_SP(string ACTION = null, string DocName = null, string RefNo = null, string USERID = null,
            string Project_ID = null, string DocType = null, string User = null, bool ETMF = false, bool CTMS = false, bool QA = false, string Artifact_Name = null, string Folder_ID = null,
            string SEQNO = null, string DocID = null, string UnblindingType = null, bool E_Upload = false, string VerTYPE = null,
            string AutoReplace = null, bool E_Download = false, string UniqueRefNo = null, string DocTypeId = null,
            string SPECtitle = null, string SubFolder_ID = null, string Comment = null, string Sub_Task_ID = null,
            string DateTitle = null, string Task_ID = null, string Folder = null, string FileName = null,
            string ContentType = null, string Data = null, string INVID = null, string ID = null, string Dept_Id = null,
            bool VerSPEC = false, bool VerDate = false, bool Email_Enable = false, string DEFINITION = null, string REVIEW_STATUS = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("eTMF_SET_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@DocType", DocType);
                cmd.Parameters.AddWithValue("@User", User);
                cmd.Parameters.AddWithValue("@ETMF", ETMF);
                cmd.Parameters.AddWithValue("@Artifact_Name", Artifact_Name);
                cmd.Parameters.AddWithValue("@Folder_ID", Folder_ID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@DocID", DocID);
                cmd.Parameters.AddWithValue("@UnblindingType", UnblindingType);
                cmd.Parameters.AddWithValue("@E_Upload", E_Upload);
                cmd.Parameters.AddWithValue("@VerTYPE", VerTYPE);
                cmd.Parameters.AddWithValue("@AutoReplace", AutoReplace);
                cmd.Parameters.AddWithValue("@E_Download", E_Download);
                cmd.Parameters.AddWithValue("@UniqueRefNo", UniqueRefNo);
                cmd.Parameters.AddWithValue("@DocTypeId", DocTypeId);
                cmd.Parameters.AddWithValue("@SPECtitle", SPECtitle);
                cmd.Parameters.AddWithValue("@SubFolder_ID", SubFolder_ID);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@Sub_Task_ID", Sub_Task_ID);
                cmd.Parameters.AddWithValue("@DateTitle", DateTitle);
                cmd.Parameters.AddWithValue("@Task_ID", Task_ID);
                cmd.Parameters.AddWithValue("@Folder", Folder);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Dept_Id", Dept_Id);
                cmd.Parameters.AddWithValue("@VerDate", VerDate);
                cmd.Parameters.AddWithValue("@VerSPEC", VerSPEC);
                cmd.Parameters.AddWithValue("@Email_Enable", Email_Enable);
                cmd.Parameters.AddWithValue("@DEFINITION", DEFINITION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@REVIEW_STATUS", REVIEW_STATUS);


                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet eTMF_DATA_SP(string SUBFOLDERID = null, string ID = null, string UploadZoneId = null, string ARTIFACTS = null, string UploadBy = null, string FOLDERID = null, string RefNo = null, string UploadTaskId = null, string USERID = null, string Status = null, string DocID = null, string UploadSectionId = null, string UploadArtifactId = null, string SubFolder_ID = null, string DocTypeId = null, string QCDocLocat = null, string QCDocLegible = null, string QCDocCurrent = null, string Folder_ID = null, string DocName = null, string DeadlineDate = null, string UploadSubTaskId = null, string UploadDocTypeid = null, string UploadDepartmentId = null, string QCDocGuide = null, string VersionID = null, string SEQNO = null, string INDIVIDUAL = null, string NOTE = null, string FileType = null, string Size = null, string Functions = null, string UploadFileName = null, string DOC_DATETIME = null, string ExpiryDate = null, string DOC_VERSIONNO = null, string SiteID = null, string CountryID = null, string AutoNomenclature = null, string UniqueRefNo = null, string Project_ID = null, string SnapId = null, string User = null, string SectionId = null, string ZoneID = null, string DOCTYPEID = null, string ACTION = null,string GroupID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("eTMF_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SUBFOLDERID", SUBFOLDERID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@UploadZoneId", UploadZoneId);
                cmd.Parameters.AddWithValue("@ARTIFACTS", ARTIFACTS);
                cmd.Parameters.AddWithValue("@UploadBy", UploadBy);
                cmd.Parameters.AddWithValue("@FOLDERID", FOLDERID);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                cmd.Parameters.AddWithValue("@UploadTaskId", UploadTaskId);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@DocID", DocID);
                cmd.Parameters.AddWithValue("@UploadSectionId", UploadSectionId);
                cmd.Parameters.AddWithValue("@UploadArtifactId", UploadArtifactId);
                cmd.Parameters.AddWithValue("@SubFolder_ID", SubFolder_ID);
                cmd.Parameters.AddWithValue("@DocTypeId", DocTypeId);
                cmd.Parameters.AddWithValue("@QCDocLocat", QCDocLocat);
                cmd.Parameters.AddWithValue("@QCDocLegible", QCDocLegible);
                cmd.Parameters.AddWithValue("@QCDocCurrent", QCDocCurrent);
                cmd.Parameters.AddWithValue("@Folder_ID", Folder_ID);
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@DeadlineDate", DeadlineDate);
                cmd.Parameters.AddWithValue("@UploadSubTaskId", UploadSubTaskId);
                cmd.Parameters.AddWithValue("@UploadDocTypeid", UploadDocTypeid);
                cmd.Parameters.AddWithValue("@UploadDepartmentId", UploadDepartmentId);
                cmd.Parameters.AddWithValue("@QCDocGuide", QCDocGuide);
                cmd.Parameters.AddWithValue("@VersionID", VersionID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@INDIVIDUAL", INDIVIDUAL);
                cmd.Parameters.AddWithValue("@NOTE", NOTE);
                cmd.Parameters.AddWithValue("@FileType", FileType);
                cmd.Parameters.AddWithValue("@Size", Size);
                cmd.Parameters.AddWithValue("@Functions", Functions);
                cmd.Parameters.AddWithValue("@UploadFileName", UploadFileName);
                cmd.Parameters.AddWithValue("@DOC_DATETIME", DOC_DATETIME);
                cmd.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                cmd.Parameters.AddWithValue("@DOC_VERSIONNO", DOC_VERSIONNO);
                cmd.Parameters.AddWithValue("@SiteID", SiteID);
                cmd.Parameters.AddWithValue("@CountryID", CountryID);
                cmd.Parameters.AddWithValue("@AutoNomenclature", AutoNomenclature);
                cmd.Parameters.AddWithValue("@UniqueRefNo", UniqueRefNo);
                cmd.Parameters.AddWithValue("@SnapId", SnapId);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@User", User);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@GroupID", GroupID);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);



                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }


        public DataSet eTMF_View_SP(string ACTION = null, string ID = null, string CountryID = null, string SiteID = null, string DocID = null,
            string DocName = null, string RefNo = null, string UniqueRefNo = null, string AutoNomenclature = null, string UploadFileName = null,
            string VersionID = null, string SysFileName = null, string Size = null, string FileType = null, string Status = null, string UploadBy = null,
            string UploadDateTime = null, string ExpiryDate = null, string UploadTaskId = null, string UploadSubTaskId = null, string UploadSectionId = null,
            string UploadArtifactId = null, string DeadlineDate = null, string SEQNO = null, string UploadDocTypeid = null, string UploadZoneId = null, string DOCTYPEID = null,
            string FOLDERID = null, string SUBFOLDERID = null, string ARTIFACTS = null, string UploadDepartmentId = null, string DOC_VERSIONNO = null,
            string DOC_DATETIME = null, string NOTE = null, string Functions = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string TZ_VAL = null;
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd = new SqlCommand("eTMF_View_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@CountryID", CountryID);
                cmd.Parameters.AddWithValue("@SiteID", SiteID);
                cmd.Parameters.AddWithValue("@DocID", DocID);
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                cmd.Parameters.AddWithValue("@UniqueRefNo", UniqueRefNo);
                cmd.Parameters.AddWithValue("@AutoNomenclature", AutoNomenclature);
                cmd.Parameters.AddWithValue("@UploadFileName", UploadFileName);
                cmd.Parameters.AddWithValue("@VersionID", VersionID);
                cmd.Parameters.AddWithValue("@SysFileName", SysFileName);
                cmd.Parameters.AddWithValue("@Size", Size);
                cmd.Parameters.AddWithValue("@FileType", FileType);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@UploadBy", UploadBy);
                cmd.Parameters.AddWithValue("@UploadDateTime", UploadDateTime);
                cmd.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                cmd.Parameters.AddWithValue("@DeadlineDate", DeadlineDate);
                cmd.Parameters.AddWithValue("@UploadDepartmentId", UploadDepartmentId);
                cmd.Parameters.AddWithValue("@UploadTaskId", UploadTaskId);
                cmd.Parameters.AddWithValue("@UploadSubTaskId", UploadSubTaskId);
                cmd.Parameters.AddWithValue("@UploadSectionId", UploadSectionId);
                cmd.Parameters.AddWithValue("@UploadArtifactId", UploadArtifactId);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@UploadDocTypeid", UploadDocTypeid);
                cmd.Parameters.AddWithValue("@UploadZoneId", UploadZoneId);
                cmd.Parameters.AddWithValue("@DOCTYPEID", DOCTYPEID);
                cmd.Parameters.AddWithValue("@FOLDERID", FOLDERID);
                cmd.Parameters.AddWithValue("@SUBFOLDERID", SUBFOLDERID);
                cmd.Parameters.AddWithValue("@ARTIFACTS", ARTIFACTS);
                cmd.Parameters.AddWithValue("@DOC_VERSIONNO", DOC_VERSIONNO);
                cmd.Parameters.AddWithValue("@DOC_DATETIME", DOC_DATETIME);
                cmd.Parameters.AddWithValue("@NOTE", NOTE);
                cmd.Parameters.AddWithValue("@Functions", Functions);
                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@USERNAME", HttpContext.Current.Session["User_Name"].ToString());
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
            }
            return ds;
        }

        public DataSet eTMF_REPORT_SP(string ACTION = null, string ID = null, string GroupID = null, string ZONEID = null, string SECTIONID = null, string ARTIFACTID = null, string DocID = null, string CountryID = null, string SiteID = null, string Status = null, string UPLOADBY = null, string CritCode = null, string ModifReloc = null,string DocType = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, USERNAME = null, TZ_VAL = null;

                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    USERNAME = HttpContext.Current.Session["User_Name"].ToString();
                }

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd = new SqlCommand("eTMF_REPORT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@GroupID", GroupID);
                cmd.Parameters.AddWithValue("@ZONEID", ZONEID);
                cmd.Parameters.AddWithValue("@SECTIONID", SECTIONID);
                cmd.Parameters.AddWithValue("@ARTIFACTID", ARTIFACTID);
                cmd.Parameters.AddWithValue("@DocID", DocID);
                cmd.Parameters.AddWithValue("@CountryID", CountryID);
                cmd.Parameters.AddWithValue("@SiteID", SiteID);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@UPLOADBY", UPLOADBY);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@ModifReloc", ModifReloc);
                cmd.Parameters.AddWithValue("@DocType", DocType);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@USERNAME", USERNAME);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet eTMF_Group_SP(string ACTION = null, string ID = null, string Group_Name = null, string GroupID = null, string ArtifactId = null, string DocId = null, string ZoneID = null, string SectionId = null, bool Type_Event = false, bool Type_Milestone = false, string PROJECTID = null, string RefNo = null, string UniqueRefNo = null, string FILEID = null, string DocTypeId = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, USERNAME = null, TZ_VAL = null;

                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    USERNAME = HttpContext.Current.Session["User_Name"].ToString();
                }

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("eTMF_Group_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Group_Name", Group_Name);
                cmd.Parameters.AddWithValue("@GroupID", GroupID);
                cmd.Parameters.AddWithValue("@ArtifactId", ArtifactId);
                cmd.Parameters.AddWithValue("@DocId", DocId);
                cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@Type_Event", Type_Event);
                cmd.Parameters.AddWithValue("@Type_Milestone", Type_Milestone);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                cmd.Parameters.AddWithValue("@UniqueRefNo", UniqueRefNo);
                cmd.Parameters.AddWithValue("@FILEID", FILEID);
                cmd.Parameters.AddWithValue("@DocTypeId", DocTypeId);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@USERNAME", USERNAME);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet eTMF_GROUP_MATRIX_SP(string Action = null, string ID = null, string USERID = null, string ZoneID = null, string GroupID = null, string SectionID = null, string ArtifactID = null, string RefNo = null, string DocTypeId = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("eTMF_GROUP_MATRIX_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
                cmd.Parameters.AddWithValue("@GroupID", GroupID);
                cmd.Parameters.AddWithValue("@SectionID", SectionID);
                cmd.Parameters.AddWithValue("@ArtifactID", ArtifactID);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                cmd.Parameters.AddWithValue("@DocTypeId", DocTypeId);



                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
            }
            return ds;
        }


        public DataSet eTMF_ACTION_LOG_SP(string ACTION = null, string ACTIONS = null, string TABLENAME = null, string AUTOID = null, string FROMDAT = null, string TODAT = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, USERNAME = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    USERNAME = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("eTMF_ACTION_LOG_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@USERNAME", USERNAME);
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@ACTIONS", ACTIONS);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@AUTOID", AUTOID);
                cmd.Parameters.AddWithValue("@FROMDAT", FROMDAT);
                cmd.Parameters.AddWithValue("@TODAT", TODAT);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;

        }

        public DataSet eTMF_LOG_SP(string ACTION = null, string TABLENAME = null, string ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null, TZ_VAL = null, USERNAME = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    USERNAME = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("eTMF_LOG_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@USERNAME", USERNAME);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ds.Dispose();
            }
            return ds;

        }
    }
}