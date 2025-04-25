using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;


namespace PPT
{
    public class DAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
        //Get Connection
        DataSet ds;
        SqlCommand cmd;
        SqlDataAdapter adp;



        public string getconstr()
        {
            return ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString;
        }

        //public string Get_MasterConnection_Off()
        //{
        //    return ConfigurationManager.ConnectionStrings["MasterConnection_Off"].ConnectionString;
        //}

        //public string Get_ChildConnection_Off()
        //{
        //    return ConfigurationManager.ConnectionStrings["ChildConnection_Off"].ConnectionString;
        //}

        //public string getconstrCHILD()
        //{
        //    return HttpContext.Current.Session["CHILD_CONN"].ToString();
        //    //return Get_ChildConnection_Off();
        //}

        public string Get_User_Id()
        {
            return HttpContext.Current.Session["User_ID"].ToString();
        }

        #region Master Data layer

        public DataSet ACTIVITY_LOG_SP(string Action = null, string ID = null, string User_ID = null, string Project = null,
            string Page_Name = null, string Function_Name = null, string Section = null, string DateFrom = null, string TimeFrom = null,
            string DateTo = null, string TimeTo = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("ACTIVITY_LOG_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                cmd.Parameters.AddWithValue("@Project", Project);
                cmd.Parameters.AddWithValue("@Page_Name", Page_Name);
                cmd.Parameters.AddWithValue("@Function_Name", Function_Name);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@TimeFrom", TimeFrom);
                cmd.Parameters.AddWithValue("@DateTo", DateTo);
                cmd.Parameters.AddWithValue("@TimeTo", TimeTo);
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
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet GetProjectName(string Action = null, string strCMD = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand(strCMD, con);
                cmd.CommandType = CommandType.Text;
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
                ////ds.Dispose();
            }
            return ds;
        }
        public DataSet GetUserGroupID(string Action = null, string Project_Name = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("Get_User_Group_ID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
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
                ////ds.Dispose();
            }
            return ds;
        }
        public DataSet GetUserGroupfunctions(string Action = null, string Project_Name = null, string UserGroup_Name = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("Get_UserGroup_functions", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                cmd.Parameters.AddWithValue("@UserGroup_Name", Project_Name);
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
                ////ds.Dispose();
            }
            return ds;
        }
        public void AddUserRights(string Action = null, string Project_Name = null, string UserGroup_Name = null, int FunctionID = -1, string User_Name = null, string Parent = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Add_Up_Del_User_Group_Fun", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                cmd.Parameters.AddWithValue("@UserGroup_Name", UserGroup_Name);
                cmd.Parameters.AddWithValue("@FunctionID", FunctionID);
                cmd.Parameters.AddWithValue("@Parent", Parent);
                cmd.Parameters.AddWithValue("@EnteredBy", ENTEREDBY);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
        }
        public void AddUserGroupFun(string Action = null, string Project_Name = null, string UserGroup_Name = null, int FunctionID = -1, string Parent = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Add_Up_Del_User_Group_Fun", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                cmd.Parameters.AddWithValue("@UserGroup_Name", UserGroup_Name);
                cmd.Parameters.AddWithValue("@FunctionID", FunctionID);
                cmd.Parameters.AddWithValue("@Parent", Parent);
                cmd.Parameters.AddWithValue("@EnteredBy", ENTEREDBY);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
        }
        public void FrmUserGroup(string Action = null, string Project_Name = null, string UserGroup_Name = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Add_Up_Del_User_Group_Fun", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Frm_User_Group");
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                cmd.Parameters.AddWithValue("@UserGroup_Name", UserGroup_Name);
                cmd.Parameters.AddWithValue("@EnteredBy", ENTEREDBY);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
        }
        public void AddUserGroups(string Action = null, string Project_Name = null, string UserGroup_Name = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Add_Upd_UserGroups", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                cmd.Parameters.AddWithValue("@UserGroup_Name", UserGroup_Name);
                cmd.Parameters.AddWithValue("@EnteredBy", ENTEREDBY);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
        }
        public DataSet GetSiteID(string Action = null, string Project_Name = null, string INVID = null, string User_ID = null, string PROJECTID = null,
            string Country_ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("Get_Site_ID_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                cmd.Parameters.AddWithValue("@Site_ID", INVID);
                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@Country_ID", Country_ID);
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
                ////ds.Dispose();
            }
            return ds;
        }
        public DataSet AddUserProfile(string Action = null, string Project_Name = null, string USerGroup_Name = null, string CNTRYID = null, string User_Name = null, string Site_ID = null, string INVNAME = null, string Email = null, string User_Dis_Name = null, string ENTEREDBY = null, string USERID = null, string UserType = null, string EmpCode = null,
            string INVID = null, string PROJECTID = null, string USERGROUPID = null, string TIMEZONE = null, bool MEDAUTH_FORM = false,
            bool MEDAUTH_FIELD = false, string Unblind = null, bool Esource = false, bool safety = false, bool eCrf = false, string IPADDRESS = null,
            bool eSource_ReadOnly = false, string Parent = null, string FunctionName = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("Add_User_Profile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                cmd.Parameters.AddWithValue("@USerGroup_Name", USerGroup_Name);
                cmd.Parameters.AddWithValue("@CNTRYID", CNTRYID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@Site_ID", Site_ID);
                cmd.Parameters.AddWithValue("@INVNAME", INVNAME);
                cmd.Parameters.AddWithValue("@EnteredBy", ENTEREDBY);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@User_Dis_Name", User_Dis_Name);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@UserType", UserType);
                cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@USERGROUPID", USERGROUPID);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);
                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", MEDAUTH_FORM);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
                cmd.Parameters.AddWithValue("@Unblind", Unblind);

                cmd.Parameters.AddWithValue("@SignOff_eSource", Esource);
                cmd.Parameters.AddWithValue("@SignOff_Safety", safety);
                cmd.Parameters.AddWithValue("@SignOff_eCRF", eCrf);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);
                cmd.Parameters.AddWithValue("@eSource_ReadOnly", eSource_ReadOnly);
                cmd.Parameters.AddWithValue("@Parent", Parent);
                cmd.Parameters.AddWithValue("@FunctionName", FunctionName);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
            }
            return ds;
        }

        public DataSet GetUserID(string Action = null, string Project_Name = null, string UserGroup_Name = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("Get_User_ID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                cmd.Parameters.AddWithValue("@UserGroup_Name", UserGroup_Name);
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
                ////ds.Dispose();
            }
            return ds;
        }
        public void AddUserFun(string Action = null, string Project_Name = null, string USerGroup_Name = null, string User_Name = null, string Fn_ID = null, string Parent = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Add_Up_Del_User_Fun", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                cmd.Parameters.AddWithValue("@USerGroup_Name", USerGroup_Name);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@FunctionID", Fn_ID);
                cmd.Parameters.AddWithValue("@EnteredBy", ENTEREDBY);
                cmd.Parameters.AddWithValue("@Parent", Parent);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
        }
        public DataSet Get_Email_ID(string Action = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("Get_Email_ID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
            return ds;
        }

        //GET SET VISIT DEATILS
        public DataSet GetSetVISITDETAILS(string Action = null, string Project_ID = null, int VISITNUM = -1, string VISIT = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("VISITDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);

                if (VISITNUM != -1) { cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM); }
                if (VISIT != "") { cmd.Parameters.AddWithValue("@VISIT", VISIT); }

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //GET SET VISIT WINDOW
        public DataSet GetSetVISITWINDOW(string Action = null, string Project_ID = null, string VISITNUM = null,
            string COMPARE_VISIT = null, string WIN_FROM = null, string WIN_TO = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("VISITWINDOW_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);

                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@COMPARE_VISIT", COMPARE_VISIT);
                cmd.Parameters.AddWithValue("@WIN_FROM", WIN_FROM);
                cmd.Parameters.AddWithValue("@WIN_TO", WIN_TO);

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        //GET SET SUBJECT DEATILS
        public DataSet GetSetSUBJECTDETAILS(string Action = null, string Project_ID = null, string STUDYID = null, string INVID = null, string SUBJID = null, bool RECYN = false, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SUBJDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);

                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@DELEGATEYN", RECYN);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //GET SET INV DEATILS
        //GET SET INV DEATILS
        public DataSet GetSetINVDETAILS(string Action = null, string Project_ID = null, string CNTRYID = null, string INSTID = null, string INVCOD = null,
            string INVID = null, string INVNAME = null, string INVQUAL = null, string INVSPEC = null, string INVSPECSPE = null, string MOBNO = null, string CONTTM = null,
            string ADDRESS = null, string TELNO = null, string FAXNO = null, string EMAILID = null, string CCEMAILID = null,
            string DTENTERED = null, string TMENTERED = null, string STATUS = null, string DEACTIVATEDON = null, string ENTEREDBY = null,
            string SiteDepot = null, string Order_ID_Prefix = null, int Order_ID_LastNo = 0, string Order_IDSD_Prefix = null, int Order_IDSD_LastNo = 0,
            string State = null, string City = null, string FirstName = null, string LastName = null, string ZIP = null, string Department = null, string Designation = null,
            string Notes = null, string ID = null, string INSTNAME = null, string STARTDATE = null, string ENDDATE = null, string INVTEAMID = null, string IPADDRESS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("INVDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@CNTRYID", CNTRYID);
                cmd.Parameters.AddWithValue("@INSTID", INSTID);


                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@INVCOD", INVCOD);
                cmd.Parameters.AddWithValue("@INVNAM", INVNAME);
                cmd.Parameters.AddWithValue("@ADDRESS", ADDRESS);

                cmd.Parameters.AddWithValue("@INVQUAL", INVQUAL);
                cmd.Parameters.AddWithValue("@INVSPEC", INVSPEC);
                cmd.Parameters.AddWithValue("@INVSPECSPE", INVSPECSPE);
                cmd.Parameters.AddWithValue("@MOBNO", MOBNO);
                cmd.Parameters.AddWithValue("@TELNO", TELNO);
                cmd.Parameters.AddWithValue("@FAXNO", FAXNO);
                cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
                cmd.Parameters.AddWithValue("@CONTTM", CONTTM);

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@DEACTIVATEDON", DEACTIVATEDON);
                cmd.Parameters.AddWithValue("@State", State);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@ZIP", ZIP);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Designation", Designation);
                cmd.Parameters.AddWithValue("@Notes", Notes);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@INSTNAME", INSTNAME);
                cmd.Parameters.AddWithValue("@STARTDATE", STARTDATE);
                cmd.Parameters.AddWithValue("@ENDDATE", ENDDATE);
                cmd.Parameters.AddWithValue("@INVTEAMID", INVTEAMID);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //GET SET PAGE DEATILS
        public DataSet GetSetPAGEDETAILS(string Action = null, int Project_ID = -1, string STUDYID = null, string VISITNUM = null, string PAGENUM = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("PAGEDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@STUDYID", STUDYID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@PAGENUM", PAGENUM);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //GET SET PRODUCT DEATILS
        public DataSet GetSetPRODUCTDETAILS(string Action = null, string PRODUCTID = null, string PRODUCTNAM = null, string ENTEREDBY = null, string ID = null, string IPADDRESS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("PRODUCTDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PRODUCTID", PRODUCTID);
                cmd.Parameters.AddWithValue("@PRODUCTNAM", PRODUCTNAM);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        //GET SET PROJECT DEATILS
        public DataSet GetSetPROJECTDETAILS(string Action = null, int Project_ID = -1, string PRODUCTID = null, string PROJNAME = null, string SPONSOR = null, string PROJSTDAT = null, string PROJDUR = null, string ENTEREDBY = null
       , string THERAREA = null, string INDC = null, string PHASE = null, bool ISACTIVE = false, int Re_Screen_WP = -1, int Rand_WP = -1,
      int NoReScreen = -1, int Re_Screen_WP_End = -1, int Rand_WP_End = -1, bool Rand_No_Site_Specific = false,
      bool MMA_Approval_Req = false, bool Site_Depot_Req = false, bool ProjectStrataYN = false, bool GenderStrataYN = false,
            string StudyAwardDate = null, string PrimaryOBJ = null, string PROJENDDAT = null, string PROJACTENDAT = null, string TherSubClass = null,
            string Ther_INDIC = null, string ProductName = null, string ComparatorName = null, string NoOfScreened = null, string NoOfRandom = null,
            string NoOfEval = null, string NoOfSites = null, string EnrollDur = null, string NoOfPatients = null, string EnrollSTDAT = null,
            string EnrollENDAT = null, string EnrollACTSTDAT = null, string EnrollACTENDAT = null, string PROJTITLE = null, string EnrollRate = null, string PROJACTSTDAT = null,
             string IWRS = null, string DM = null, string LBD = null, string ConnString = null, string ChildDBName = null, string CTMS = null,
            string SITE_MGMT = null, string SAFETY = null, string ObjectiveType = null, string IPADDRESS = null, string eSource = null, string eTMF = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("PROJDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@PRODUCTID", PRODUCTID);
                cmd.Parameters.AddWithValue("@PROJNAME", PROJNAME);
                cmd.Parameters.AddWithValue("@SPONSOR", SPONSOR);
                if (PROJSTDAT != "")
                {
                    cmd.Parameters.AddWithValue("@PROJSTDAT", PROJSTDAT);
                }
                cmd.Parameters.AddWithValue("@PROJDUR", PROJDUR);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);


                cmd.Parameters.AddWithValue("@THERAREA", THERAREA);
                cmd.Parameters.AddWithValue("@INDC", INDC);
                cmd.Parameters.AddWithValue("@PHASE", PHASE);
                cmd.Parameters.AddWithValue("@ISACTIVE", ISACTIVE);
                cmd.Parameters.AddWithValue("@Re_Screen_WP", Re_Screen_WP);
                cmd.Parameters.AddWithValue("@Rand_WP", Rand_WP);
                cmd.Parameters.AddWithValue("@NoReScreen", NoReScreen);
                cmd.Parameters.AddWithValue("@Re_Screen_WP_End", Re_Screen_WP_End);
                cmd.Parameters.AddWithValue("@Rand_WP_End", Rand_WP_End);
                cmd.Parameters.AddWithValue("@Rand_No_Site_Specific", Rand_No_Site_Specific);
                cmd.Parameters.AddWithValue("@MMA_Approval_Req", MMA_Approval_Req);
                cmd.Parameters.AddWithValue("@Site_Depot_Req", Site_Depot_Req);
                cmd.Parameters.AddWithValue("@ProjectStrataYN", ProjectStrataYN);
                cmd.Parameters.AddWithValue("@GenderStrataYN", GenderStrataYN);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);
                cmd.Parameters.AddWithValue("@eSource", eSource);
                cmd.Parameters.AddWithValue("@eTMF", eTMF);

                if (StudyAwardDate != "")
                {
                    cmd.Parameters.AddWithValue("@StudyAwardDate", StudyAwardDate);
                }
                cmd.Parameters.AddWithValue("@PrimaryOBJ", PrimaryOBJ);

                if (PROJENDDAT != "")
                {
                    cmd.Parameters.AddWithValue("@PROJENDDAT", PROJENDDAT);
                }
                if (PROJACTENDAT != "")
                {
                    cmd.Parameters.AddWithValue("@PROJACTENDAT", PROJACTENDAT);
                }
                cmd.Parameters.AddWithValue("@TherSubClass", TherSubClass);
                cmd.Parameters.AddWithValue("@Ther_INDIC", Ther_INDIC);
                cmd.Parameters.AddWithValue("@ProductName", ProductName);
                cmd.Parameters.AddWithValue("@ComparatorName", ComparatorName);
                cmd.Parameters.AddWithValue("@NoOfScreened", NoOfScreened);
                cmd.Parameters.AddWithValue("@NoOfRandom", NoOfRandom);
                cmd.Parameters.AddWithValue("@NoOfEval", NoOfEval);
                cmd.Parameters.AddWithValue("@NoOfSites", NoOfSites);
                cmd.Parameters.AddWithValue("@EnrollDur", EnrollDur);
                cmd.Parameters.AddWithValue("@NoOfPatients", NoOfPatients);
                if (EnrollSTDAT != "")
                {
                    cmd.Parameters.AddWithValue("@EnrollSTDAT", EnrollSTDAT);
                }
                if (EnrollENDAT != "")
                {
                    cmd.Parameters.AddWithValue("@EnrollENDAT", EnrollENDAT);
                }
                if (EnrollACTSTDAT != "")
                {
                    cmd.Parameters.AddWithValue("@EnrollACTSTDAT", EnrollACTSTDAT);
                }
                if (EnrollACTENDAT != "")
                {
                    cmd.Parameters.AddWithValue("@EnrollACTENDAT", EnrollACTENDAT);
                }
                cmd.Parameters.AddWithValue("@PROJTITLE", PROJTITLE);
                cmd.Parameters.AddWithValue("@EnrollRate", EnrollRate);
                if (PROJACTSTDAT != "")
                {
                    cmd.Parameters.AddWithValue("@PROJACTSTDAT", PROJACTSTDAT);
                }

                cmd.Parameters.AddWithValue("@IWRS", IWRS);
                cmd.Parameters.AddWithValue("@DM", DM);
                cmd.Parameters.AddWithValue("@LBD", LBD);
                cmd.Parameters.AddWithValue("@CTMS", CTMS);
                cmd.Parameters.AddWithValue("@SITE_MGMT", SITE_MGMT);
                cmd.Parameters.AddWithValue("@SAFETY", SAFETY);
                cmd.Parameters.AddWithValue("@ConnString", ConnString);
                cmd.Parameters.AddWithValue("@ChildDBName", ChildDBName);
                cmd.Parameters.AddWithValue("@ObjectiveType", ObjectiveType);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        //GET SET COUNTRY DEATILS
        //GET SET COUNTRY DEATILS
        public DataSet GetSetCOUNTRYDETAILS(string Action = null, string Project_Name = null, string CNTRYID = null,
        string COUNTRYCOD = null, string COUNTRY = null, string ENTEREDBY = null,
            string Order_ID_Prefix = null, int Order_ID_LastNo = 0, string Order_IDSD_Prefix = null, int Order_IDSD_LastNo = 0,
            string STATEID = null, string CITYID = null, string STATENAME = null, string CITYNAME = null, string IPADDRESS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("COUNTRYDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                if (CNTRYID != "0")
                {
                    cmd.Parameters.AddWithValue("@CNTRYID", CNTRYID);
                }
                cmd.Parameters.AddWithValue("@COUNTRYCOD", COUNTRYCOD);
                cmd.Parameters.AddWithValue("@COUNTRY", COUNTRY);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@Order_ID_Prefix", Order_ID_Prefix);
                cmd.Parameters.AddWithValue("@Order_ID_LastNo", Order_ID_LastNo);
                cmd.Parameters.AddWithValue("@Order_IDSD_Prefix", Order_IDSD_Prefix);
                cmd.Parameters.AddWithValue("@Order_IDSD_LastNo", Order_IDSD_LastNo);
                cmd.Parameters.AddWithValue("@STATEID", STATEID);
                cmd.Parameters.AddWithValue("@CITYID", CITYID);
                cmd.Parameters.AddWithValue("@STATENAME", STATENAME);
                cmd.Parameters.AddWithValue("@CITYNAME", CITYNAME);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }


        //GET SET INSTITUTE DEATILS
        public DataSet GetSetINSTITUTEDETAILS(string Action = null, string Project_Name = null, string CNTRYID = null,
        string INSTID = null, string INSTNAM = null, string CITY = null, string AREA = null, string INSTCITY = null,
            string ENTEREDBY = null, string Project_ID = null, string State = null, string Website = null, string IPADDRESS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("INSTITUTEDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_Name", Project_Name);
                if (CNTRYID != "0")
                {
                    cmd.Parameters.AddWithValue("@CNTRYID", CNTRYID);
                }
                cmd.Parameters.AddWithValue("@State", State);
                cmd.Parameters.AddWithValue("@INSTID", INSTID);
                cmd.Parameters.AddWithValue("@INSTNAM", INSTNAM);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                cmd.Parameters.AddWithValue("@AREA", AREA);
                cmd.Parameters.AddWithValue("@INSTCITY", INSTCITY);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@Website", Website);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public DataSet GET_INVESTIGATORSIGNOFF_DATA(String Action = null, String InvId = null, string SubjId = null, string VisitNumber = null, string Page = null, string EnteredBy = null, string Project_ID = null, string SDVSTATUS = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand("GET_INVESTIGATORSIGNOFF_DATA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@INVID", InvId);
                cmd.Parameters.AddWithValue("@SUBJID", SubjId);
                cmd.Parameters.AddWithValue("@VISITNUM", VisitNumber);
                cmd.Parameters.AddWithValue("@SDVSTATUS", SDVSTATUS);
                cmd.Parameters.AddWithValue("@PAGENUM", Page);
                cmd.Parameters.AddWithValue("@EnteredBy", EnteredBy);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@TIMEZONE", HttpContext.Current.Session["TimeZone_Value"].ToString());
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

        //Get Data
        public DataSet GetRecordOpenCRF(string strCMD = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand(strCMD, con);
                cmd.CommandType = CommandType.Text;
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

        public DataTable AddNewRowToGridInvDetails(GridView Grd)
        {
            //Table Structure
            DataRow drCurrentRow = null;
            DataTable dtCurrentTable;
            int rowIndex = 0;
            int StudyId = 0;
            int i;
            dtCurrentTable = new DataTable();
            dtCurrentTable.Columns.Add(new DataColumn("STUDYID", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("INVID", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("INVNAME", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("TELNO", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("FAXNO", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("EMAILID", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("CCEMAILID", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("STATUS", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("DEACTIVATEDON", typeof(string)));

            dtCurrentTable.Columns.Add(new DataColumn("UPDATE_FLAG", typeof(string)));

            if (Grd.Rows.Count > 0)
            {
                for (i = 0; i < Grd.Rows.Count; i++)
                {
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["STUDYID"] = ((TextBox)Grd.Rows[rowIndex].FindControl("STUDYID")).Text;
                    drCurrentRow["INVID"] = ((TextBox)Grd.Rows[rowIndex].FindControl("INVID")).Text;
                    drCurrentRow["INVNAME"] = ((TextBox)Grd.Rows[rowIndex].FindControl("INVNAME")).Text;
                    drCurrentRow["ADDRESS"] = ((TextBox)Grd.Rows[rowIndex].FindControl("ADDRESS")).Text;
                    drCurrentRow["TELNO"] = ((TextBox)Grd.Rows[rowIndex].FindControl("TELNO")).Text;
                    drCurrentRow["FAXNO"] = ((TextBox)Grd.Rows[rowIndex].FindControl("FAXNO")).Text;
                    drCurrentRow["EMAILID"] = ((TextBox)Grd.Rows[rowIndex].FindControl("EMAILID")).Text;
                    drCurrentRow["CCEMAILID"] = ((TextBox)Grd.Rows[rowIndex].FindControl("CCEMAILID")).Text;
                    drCurrentRow["STATUS"] = ((TextBox)Grd.Rows[rowIndex].FindControl("STATUS")).Text;
                    drCurrentRow["DEACTIVATEDON"] = ((TextBox)Grd.Rows[rowIndex].FindControl("DEACTIVATEDON")).Text;

                    drCurrentRow["UPDATE_FLAG"] = ((TextBox)Grd.Rows[rowIndex].FindControl("UPDATE_FLAG")).Text;

                    StudyId = Convert.ToInt32(((TextBox)Grd.Rows[rowIndex].FindControl("STUDYID")).Text);
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    rowIndex++;
                }

                //Add Empty Row
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["STUDYID"] = StudyId;
                drCurrentRow["INVID"] = "";
                drCurrentRow["INVNAME"] = string.Empty;
                drCurrentRow["ADDRESS"] = string.Empty;
                drCurrentRow["TELNO"] = string.Empty;
                drCurrentRow["FAXNO"] = string.Empty;
                drCurrentRow["EMAILID"] = string.Empty;
                drCurrentRow["CCEMAILID"] = string.Empty;
                drCurrentRow["STATUS"] = string.Empty;
                drCurrentRow["DEACTIVATEDON"] = string.Empty;

                drCurrentRow["UPDATE_FLAG"] = "0";
                dtCurrentTable.Rows.Add(drCurrentRow);
            }

            return dtCurrentTable;

        }
        public DataTable AddNewRowToGridVisitDetails(GridView Grd)
        {
            //Table Structure
            DataRow drCurrentRow = null;
            DataTable dtCurrentTable;
            int rowIndex = 0;
            int i;
            dtCurrentTable = new DataTable();
            dtCurrentTable.Columns.Add(new DataColumn("Project_ID", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("VISITNUM", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("VISIT", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("UPDATE_FLAG", typeof(string)));

            if (Grd.Rows.Count > 0)
            {
                for (i = 0; i < Grd.Rows.Count; i++)
                {
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["Project_ID"] = ((TextBox)Grd.Rows[rowIndex].FindControl("Project_ID")).Text;
                    drCurrentRow["VISITNUM"] = ((TextBox)Grd.Rows[rowIndex].FindControl("VISITNUM")).Text;
                    drCurrentRow["VISIT"] = ((TextBox)Grd.Rows[rowIndex].FindControl("VISIT")).Text;
                    drCurrentRow["UPDATE_FLAG"] = ((TextBox)Grd.Rows[rowIndex].FindControl("UPDATE_FLAG")).Text;
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    rowIndex++;
                }

                //Add Empty Row
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["Project_ID"] = "";
                drCurrentRow["VISITNUM"] = "";
                drCurrentRow["VISIT"] = string.Empty;
                drCurrentRow["UPDATE_FLAG"] = "0";
                dtCurrentTable.Rows.Add(drCurrentRow);
            }

            return dtCurrentTable;

        }
        public DataTable AddNewRowToGridPageDetails(GridView Grd)
        {
            //Table Structure
            DataRow drCurrentRow = null;
            DataTable dtCurrentTable;
            int rowIndex = 0;
            int i;
            dtCurrentTable = new DataTable();
            dtCurrentTable.Columns.Add(new DataColumn("STUDYID", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("VISITNUM", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("PAGENUM", typeof(string)));
            dtCurrentTable.Columns.Add(new DataColumn("UPDATE_FLAG", typeof(string)));

            if (Grd.Rows.Count > 0)
            {
                for (i = 0; i < Grd.Rows.Count; i++)
                {
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["STUDYID"] = ((TextBox)Grd.Rows[rowIndex].FindControl("STUDYID")).Text;
                    drCurrentRow["VISITNUM"] = ((TextBox)Grd.Rows[rowIndex].FindControl("VISITNUM")).Text;
                    drCurrentRow["PAGENUM"] = ((TextBox)Grd.Rows[rowIndex].FindControl("PAGENUM")).Text;
                    drCurrentRow["UPDATE_FLAG"] = ((TextBox)Grd.Rows[rowIndex].FindControl("UPDATE_FLAG")).Text;
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    rowIndex++;
                }

                //Add Empty Row
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["STUDYID"] = "";
                drCurrentRow["VISITNUM"] = "";
                drCurrentRow["PAGENUM"] = string.Empty;
                drCurrentRow["UPDATE_FLAG"] = "0";
                dtCurrentTable.Rows.Add(drCurrentRow);
            }
            return dtCurrentTable;
        }

        #endregion Master Data layer

        #region CTMS

        //GET SET Master Structure
        public DataSet GetSetProjectData(string Action = null, string Project_ID = null, string ID = null, string INVID = null, string MVID = null, int ContID = 0, string RECID = null, string COUNTYN = null, string COUNTNO = null, string MODULENAME = null, string SECTIONID = null, string SUBMODULENAME = null, string SUBSECTIONID = null, string VARIABLENAME = null, string FIELDNAME = null, string TABLENAME = null, string CONTROLTYPE = null, string CLASS = null, string DATATYPE = null, string DATA = null, int CONTYN = 0, string ENTEREDBY = null, string UPDATEDBY = null, string STUDYCENTERNAME = null, string LOCATION = null, string DISTANCE_CENTER = null, string POPULATION = null, string TOTAL_SUBJECT = null, string SCREEN_FAILURE_RATE = null, string SUBJECT_RANDOMIZED = null, string DROP_OUTRATE = null, string NAME = null, string CV_COLLECTED = null, string ROLE = null, string MET_AT_VISIT = null, string REGULATORY_AGENCY = null,
        string LAST_INSPECTION = null, string CHECKLISTID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("PROJECT_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@MVID", MVID);
                cmd.Parameters.AddWithValue("@ContID", ContID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@COUNTYN", COUNTYN);
                cmd.Parameters.AddWithValue("@COUNTNO", COUNTNO);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@SECTIONID", SECTIONID);
                cmd.Parameters.AddWithValue("@SUBMODULENAME", SUBMODULENAME);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);

                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@CLASS", CLASS);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                if (CONTROLTYPE == "DROPDOWN")
                {
                    if (DATA != "0")
                    {
                        cmd.Parameters.AddWithValue("@DATA", DATA);
                    }
                }
                else
                {
                    if (DATA != "")
                    {
                        cmd.Parameters.AddWithValue("@DATA", DATA);
                    }
                }
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);
                cmd.Parameters.AddWithValue("@CONTYN", CONTYN);

                cmd.Parameters.AddWithValue("@STUDYCENTERNAME", STUDYCENTERNAME);
                cmd.Parameters.AddWithValue("@LOCATION", LOCATION);
                cmd.Parameters.AddWithValue("@DISTANCE_CENTER", DISTANCE_CENTER);
                cmd.Parameters.AddWithValue("@POPULATION", POPULATION);



                cmd.Parameters.AddWithValue("@TOTAL_SUBJECT", TOTAL_SUBJECT);
                cmd.Parameters.AddWithValue("@SCREEN_FAILURE_RATE", SCREEN_FAILURE_RATE);
                cmd.Parameters.AddWithValue("@SUBJECT_RANDOMIZED", SUBJECT_RANDOMIZED);
                cmd.Parameters.AddWithValue("@DROP_OUTRATE", DROP_OUTRATE);


                cmd.Parameters.AddWithValue("@NAME", NAME);
                cmd.Parameters.AddWithValue("@CV_COLLECTED", CV_COLLECTED);
                cmd.Parameters.AddWithValue("@ROLE", ROLE);
                cmd.Parameters.AddWithValue("@MET_AT_VISIT", MET_AT_VISIT);

                cmd.Parameters.AddWithValue("@REGULATORY_AGENCY", REGULATORY_AGENCY);
                cmd.Parameters.AddWithValue("@LAST_INSPECTION", LAST_INSPECTION);
                cmd.Parameters.AddWithValue("@CHECKLISTID", CHECKLISTID);


                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }




        //GET SET Master Structure
        public DataSet GetSetMaster(string Action = null, string TABLENAME = null, string MODULENAME = null, string SUBMODULENAME = null, string CHECKLISTID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("MASTER_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@SUBMODULENAME", SUBMODULENAME);
                cmd.Parameters.AddWithValue("@CHECKLISTID", CHECKLISTID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //GET Dropdown data

        //GET SET Master Structure
        public DataSet GetSetMonitoringVisit(string Action = null, string MVID = null, string PROJECTID = null, string INVID = null, string VISITTYPE = null,
            string CRA = null, string VSTDAT = null, string VEDAT = null, string STATUS = null, string ENTEREDBY = null,
            string NoofVisit = null, string Frequency = null, string VISITNO = null, string SECTIONID = null, string SUBSECTIONID = null
            , string QID = null, string QUERY = null, string COMMENTS = null, string CHECKLIST_ID = null, string COUNTRYID = null, string USER_ID = null
            )
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("Monitoring_Visit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@MVID", MVID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@VISITTYPE", VISITTYPE);
                cmd.Parameters.AddWithValue("@CRA", CRA);
                cmd.Parameters.AddWithValue("@VSTDAT", VSTDAT);
                cmd.Parameters.AddWithValue("@VEDAT", VEDAT);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@NoofVisit", NoofVisit);
                cmd.Parameters.AddWithValue("@Frequency", Frequency);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@VISITNO", VISITNO);
                cmd.Parameters.AddWithValue("@SECTIONID", SECTIONID);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);
                cmd.Parameters.AddWithValue("@QID", QID);
                cmd.Parameters.AddWithValue("@QUERY", QUERY);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@CHECKLIST_ID", CHECKLIST_ID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@User_ID", USER_ID);


                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //GET SET Master Structure
        public DataSet GetSetChecklistComments(string Action = null, string MVID = null, string PROJECTID = null,
                  string INVID = null, string ChecklistID = null,
                  string SECTIONID = null, string SUBSECTIONID = null, string Comments = null,
                  string Issue = null, string Internal = null, string Followup = null, string Observation = null, string Report = null,
                  string ENTEREDBY = null, string SUBJID = null, string CheckListRow_ID = null, string PD = null, string ID = null,
            string RECID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SP_Checklist_Comments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", PROJECTID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@ChecklistID", ChecklistID);
                cmd.Parameters.AddWithValue("@SECTIONID", SECTIONID);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);
                cmd.Parameters.AddWithValue("@Comments", Comments);
                cmd.Parameters.AddWithValue("@Issue", Issue);
                cmd.Parameters.AddWithValue("@Internal", Internal);
                cmd.Parameters.AddWithValue("@Followup", Followup);
                cmd.Parameters.AddWithValue("@Observation", Observation);
                cmd.Parameters.AddWithValue("@Report", Report);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@CheckListRowID", CheckListRow_ID);
                cmd.Parameters.AddWithValue("@PD", PD);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }


        public DataSet InsertUpdateSubjectRegistration(string Action = null, string Project_ID = null, string INVID = null,
           string SUBJID = null, string CONSENT_DATE = null, string GENDER = null,
           string DOB = null, string VISITNUM = null, string ACTUAL_DATE = null,
           string IS_ARRIVE = null, string ET_DATE = null,
           string IS_ELIGIBLE = null,
           string REAS_ELIGIBLE = null, string REAS_ELIGIBLE_OTHER = null,
           string SUB_WITHDRAW = null, string REAS_WITHDRAW = null, string REAS_WITHDRAW_OTHER = null, string FROM_SCH_DT = null,
           string TO_SCH_DT = null, string COMPARE_VISIT = null, string WIN_FROM = null, string WIN_TO = null, string DELEGATEYN = null, string RANDNO = null,
            string SCREENFAIL = null, string REAS_SCREENFAIL = null, string RANDFAIL = null,
           string ENTEREDBY = null, string Comments = null, string Age_Group = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SubjectRegister_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@CONSENT_DATE", CONSENT_DATE);
                cmd.Parameters.AddWithValue("@GENDER", GENDER);
                cmd.Parameters.AddWithValue("@DOB", DOB);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@REAS_ELIGIBLE", REAS_ELIGIBLE);
                cmd.Parameters.AddWithValue("@REAS_WITHDRAW", REAS_WITHDRAW);
                cmd.Parameters.AddWithValue("@FROM_SCH_DT", FROM_SCH_DT);
                cmd.Parameters.AddWithValue("@TO_SCH_DT", TO_SCH_DT);
                cmd.Parameters.AddWithValue("@COMPARE_VISIT", COMPARE_VISIT);
                cmd.Parameters.AddWithValue("@WIN_FROM", WIN_FROM);
                cmd.Parameters.AddWithValue("@WIN_TO", WIN_TO);
                cmd.Parameters.AddWithValue("@DELEGATEYN", DELEGATEYN);
                cmd.Parameters.AddWithValue("@RANDNO", RANDNO);
                if (ACTUAL_DATE != "") { cmd.Parameters.AddWithValue("@ACTUAL_DATE", ACTUAL_DATE); }
                if (IS_ELIGIBLE != "0") { cmd.Parameters.AddWithValue("@IS_ELIGIBLE", IS_ELIGIBLE); }
                if (REAS_ELIGIBLE_OTHER != null) { cmd.Parameters.AddWithValue("@REAS_ELIGIBLE_OTHER", REAS_ELIGIBLE_OTHER); }
                if (SUB_WITHDRAW != null) { cmd.Parameters.AddWithValue("@SUB_WITHDRAW", SUB_WITHDRAW); }
                if (REAS_WITHDRAW_OTHER != null) { cmd.Parameters.AddWithValue("@REAS_WITHDRAW_OTHER", REAS_WITHDRAW_OTHER); }
                if (ET_DATE != "") { cmd.Parameters.AddWithValue("@ET_DATE", ET_DATE); }
                if (IS_ARRIVE != null) { cmd.Parameters.AddWithValue("@IS_ARRIVE", IS_ARRIVE); }
                if (SCREENFAIL != "0") { cmd.Parameters.AddWithValue("@SCREENFAIL", SCREENFAIL); }
                if (REAS_SCREENFAIL != "") { cmd.Parameters.AddWithValue("@REAS_SCREENFAIL", REAS_SCREENFAIL); }
                if (RANDFAIL != "0") { cmd.Parameters.AddWithValue("@RANDFAIL", RANDFAIL); }
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@Age_Group", Age_Group);
                cmd.Parameters.AddWithValue("@Comments", Comments);


                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
            return ds;
        }


        public DataSet DM_RULE_SP(string Action = null, string ID = null, string Project_ID = null, string Indication_ID = null, string RULE_ID = null, string Actions = null, string Nature = null, string Informational = null, string All = null,
           string Description = null, string QueryText = null, string SEQNO = null, string GEN_QUERY = null, string SET_VALUE = null,
           string Visit_ID = null, string Module_ID = null, string Field_ID = null, string Value = null, string Condition = null, string Formula = null,
           string VARIABLENAMEDEC = null, string DERIVED = null, string SUBJID = null, string VISITNO = null, string RECID = null, string PVID = null, string TESTED = null,
           string ColumnName = null, string TableName = null, string ONESIDED = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DM_RULE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Nature", Nature);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Indication_ID", Indication_ID);
                cmd.Parameters.AddWithValue("@RULE_ID", RULE_ID);
                cmd.Parameters.AddWithValue("@Actions", Actions);
                cmd.Parameters.AddWithValue("@Informational", Informational);
                cmd.Parameters.AddWithValue("@All", All);
                cmd.Parameters.AddWithValue("@Visit_ID", Visit_ID);
                cmd.Parameters.AddWithValue("@Module_ID", Module_ID);
                cmd.Parameters.AddWithValue("@Field_ID", Field_ID);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@QueryText", QueryText);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@Value", Value);
                cmd.Parameters.AddWithValue("@Condition", Condition);
                cmd.Parameters.AddWithValue("@Formula", Formula);
                cmd.Parameters.AddWithValue("@TESTED", TESTED);
                cmd.Parameters.AddWithValue("@GEN_QUERY", GEN_QUERY);
                cmd.Parameters.AddWithValue("@SET_VALUE", SET_VALUE);
                cmd.Parameters.AddWithValue("@VARIABLENAMEDEC", VARIABLENAMEDEC);
                cmd.Parameters.AddWithValue("@DERIVED", DERIVED);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNO", VISITNO);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@ColumnName", ColumnName);
                cmd.Parameters.AddWithValue("@TableName", TableName);
                cmd.Parameters.AddWithValue("@ONESIDED", ONESIDED);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }

        public DataSet DM_RUN_RULE(string Action = null, string RULE_ID = null, string Nature = null, string Project_ID = null, string SUBJID = null, string Para_Indication_ID = null, string Para_Visit_ID = null, string Para_VariableName = null, string Para_ModuleName = null,
            string PVID = null, string ContID = null, string RECID = null, string Data = null, string QUERYTEXT = null, string Module_ID = null,
            string Field_ID = null, string VARIABLENAME = null, string FORMULA1 = null, string Informational = null
            , string COLUMN = null, string TABLE = null, string VISIT = null, string OtherPVIDS = null, string ENTEREDBY = null, string VISITNUM = null, string INVID = null)
        {
            DataSet ds = new DataSet();
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
                SqlCommand cmd = new SqlCommand("DM_RUN_RULE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@RULE_ID", RULE_ID);
                cmd.Parameters.AddWithValue("@Nature", Nature);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@Para_Visit_ID", Para_Visit_ID);
                cmd.Parameters.AddWithValue("@Para_VariableName", Para_VariableName);
                cmd.Parameters.AddWithValue("@Para_ModuleName", Para_ModuleName);

                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@Module_ID", Module_ID);
                cmd.Parameters.AddWithValue("@Field_ID", Field_ID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@Informational", Informational);
                cmd.Parameters.AddWithValue("@TABLE", TABLE);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@OtherPVIDS", OtherPVIDS);
                cmd.Parameters.AddWithValue("@ENTEREDBY", HttpContext.Current.Session["USER_ID"].ToString());
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
                cmd.Parameters.AddWithValue("@INVID", INVID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }

        public DataSet DM_VISIT_CRITERIA_SP(string ID = null, string SUBJID = null, string SITEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_VISIT_CRITERIA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
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
                ////ds.Dispose();
            }
            return ds;
        }

        //Get Set ICRLog

        public DataSet GetSetICRLog(string Action = null, string Project_ID = null, string INVID = null, string ContID = null,
            string LANG = null, string SIVVERDAT = null, string SIVVER = null, string APPDAT = null, string COM = null, string ENTEREDBY = null, string UPDATEDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("[ICDLog_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@ContID", ContID);
                cmd.Parameters.AddWithValue("@LANG", LANG);
                cmd.Parameters.AddWithValue("@SIVVERDAT", SIVVERDAT);
                cmd.Parameters.AddWithValue("@SIVVER", SIVVER);
                cmd.Parameters.AddWithValue("@APPDAT", APPDAT);
                cmd.Parameters.AddWithValue("@COM", COM);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        //Get Set ICRLogDetails

        public DataSet GetSetICRLogDetails(string Action = null, string Project_ID = null, string INVID = null, string ContID = null,
            string SUBJID = null, string VISDAT = null, string ICSDAT = null, string ICDLANGVER = null, string COM = null, string MVID = null,
            string MVDAT = null, string MVVERYFIEDBY = null, string ENTEREDBY = null, string UPDATEDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("[ICDLogDetails_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@ContID", ContID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISDAT", VISDAT);
                cmd.Parameters.AddWithValue("@ICSDAT", ICSDAT);
                cmd.Parameters.AddWithValue("@ICDLANGVER", ICDLANGVER);
                cmd.Parameters.AddWithValue("@COM", COM);
                cmd.Parameters.AddWithValue("@MVID", MVID);
                cmd.Parameters.AddWithValue("@MVDAT", MVDAT);
                cmd.Parameters.AddWithValue("@MVVERYFIEDBY", MVVERYFIEDBY);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);


                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        /*Reports Section Start*/

        //Get MV Reports
        public DataSet GetMVReports(string SpName = null, string Action = null, string Project_ID = null, string MVID = null, string SUBSECTIONID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(SpName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@MVID", MVID);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //Get Site Qualification Reports
        public DataSet GetReports(string SpName = null, string Action = null, string Project_ID = null, string SQID = null, string SUBSECTIONID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(SpName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@SQID", SQID);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //Get SiteInitiation Reports
        public DataSet GetReport_SI(string SpName = null, string Action = null, string Project_ID = null, string SIID = null, string SUBSECTIONID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(SpName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@SIID", SIID);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //Get Site CloseOut Reports
        public DataSet GetReport_CO(string SpName = null, string Action = null, string Project_ID = null, string COID = null, string SUBSECTIONID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(SpName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@COID", COID);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //Get Reports Identificaiton
        public DataSet GetReportsIdentificaiton(string SpName = null, string Project_ID = null, string MVID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(SpName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@MVID", MVID);


                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //Get Reports Comments
        public DataSet GetReportComments(string SpName = null, string Action = null, string Project_ID = null, string checklistID = null, string SUBSECTIONID = null)
        {

            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(SpName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@MVID", checklistID);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);


                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;

        }


        /*Reports Section End*/

        //SAE LOG
        public DataSet GetSetSAELOG(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null,
           string SAEID = null, string VERSIONID = null, string CAUSINV = null, string CAUSSPON = null, string INIREC = null, string ID = null,
            string INIRA = null, string INIEC = null, string INISPONS = null, string REGRAECHOI = null, string REGOTHSITE = null,
            string VISITID = null, string VISITDATE = null, string AMOUNTPAID = null, string PAYMENTDT = null, string PAIDBY = null,
            string RECEIVEDBY = null, string ENTEREDBY = null, string SUBJNAME = null, string CONTACTINFO = null, string UNIQUEID = null, string ELIGIBILTYSTATUS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("[CTMS_SAE_DETAILS]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@CAUSINV", CAUSINV);
                cmd.Parameters.AddWithValue("@CAUSSPON", CAUSSPON);
                cmd.Parameters.AddWithValue("@INIREC", INIREC);
                cmd.Parameters.AddWithValue("@INIRA", INIRA);
                cmd.Parameters.AddWithValue("@INIEC", INIEC);
                cmd.Parameters.AddWithValue("@INISPONS", INISPONS);
                cmd.Parameters.AddWithValue("@REGRAECHOI", REGRAECHOI);
                cmd.Parameters.AddWithValue("@REGOTHSITE", REGOTHSITE);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@VISITDATE", VISITDATE);
                cmd.Parameters.AddWithValue("@AMOUNTPAID", AMOUNTPAID);
                cmd.Parameters.AddWithValue("@PAYMENTDT", PAYMENTDT);
                cmd.Parameters.AddWithValue("@PAIDBY", PAIDBY);
                cmd.Parameters.AddWithValue("@RECEIVEDBY", RECEIVEDBY);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SUBJNAME", SUBJNAME);
                cmd.Parameters.AddWithValue("@CONTACTINFO", CONTACTINFO);
                cmd.Parameters.AddWithValue("@UNIQUEID", UNIQUEID);
                cmd.Parameters.AddWithValue("@ELIGIBILTYSTATUS", ELIGIBILTYSTATUS);



                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }



        //Site training record
        public DataSet GetSetSiteTrainingLog(string Action = null, string Project_ID = null, string INVID = null, string TrainingID = null, string DATETraining = null, bool ModTrainOnSite = false, bool ModTrainTel = false, bool ModTrainWebex = false,
        bool ModOther = false, string ModOtherSpec = null, bool STUDPROT = false, bool SAFTRPT = false, bool INFCP = false, bool VISPROC = false, bool LABSAMP = false, bool SUBDAIRY = false, bool ECRFCOMP = false, bool IWRS = false, bool SDR = false,
        bool ISFDOC = false, bool RAND = false, bool IPPA = false, bool INVNACC = false, bool STORTEMP = false, bool IPANYOTH = false, string IPANYOTH_SPEC = null, bool NEWTRAIL = false, bool NONCOMP = false, bool AMENDSTUD = false, bool TRAINANYOTH = false, string TRAINANYOTH_SPEC = null, string TRAINDETAILS = null,
        string DTENTERED = null, string Comments = null, string TraineeName = null, string TRAINERNAME = null, string ENTEREDBY = null, string UPDATEDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("getSet_SiteTrainingLog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@TrainingID", TrainingID);
                cmd.Parameters.AddWithValue("@DATETraining", DATETraining);
                cmd.Parameters.AddWithValue("@ModTrainOnSite", ModTrainOnSite);
                cmd.Parameters.AddWithValue("@ModTrainTel", ModTrainTel);
                cmd.Parameters.AddWithValue("@ModTrainWebex", ModTrainWebex);
                cmd.Parameters.AddWithValue("@ModOther", ModOther);
                cmd.Parameters.AddWithValue("@ModOtherSpec", ModOtherSpec);
                cmd.Parameters.AddWithValue("@STUDPROT", STUDPROT);
                cmd.Parameters.AddWithValue("@SAFTRPT", SAFTRPT);
                cmd.Parameters.AddWithValue("@INFCP", INFCP);
                cmd.Parameters.AddWithValue("@VISPROC", VISPROC);
                cmd.Parameters.AddWithValue("@LABSAMP", LABSAMP);
                cmd.Parameters.AddWithValue("@SUBDAIRY", SUBDAIRY);
                cmd.Parameters.AddWithValue("@ECRFCOMP", ECRFCOMP);
                cmd.Parameters.AddWithValue("@IWRS", IWRS);
                cmd.Parameters.AddWithValue("@SDR", SDR);
                cmd.Parameters.AddWithValue("@ISFDOC", ISFDOC);
                cmd.Parameters.AddWithValue("@RAND", RAND);
                cmd.Parameters.AddWithValue("@IPPA", IPPA);
                cmd.Parameters.AddWithValue("@INVNACC", INVNACC);
                cmd.Parameters.AddWithValue("@STORTEMP", STORTEMP);
                cmd.Parameters.AddWithValue("@IPANYOTH", IPANYOTH);
                cmd.Parameters.AddWithValue("@IPANYOTH_SPEC", IPANYOTH_SPEC);
                cmd.Parameters.AddWithValue("@NEWTRAIL", NEWTRAIL);
                cmd.Parameters.AddWithValue("@NONCOMP", NONCOMP);
                cmd.Parameters.AddWithValue("@AMENDSTUD", AMENDSTUD);
                cmd.Parameters.AddWithValue("@TRAINANYOTH", TRAINANYOTH);
                cmd.Parameters.AddWithValue("@TRAINANYOTH_SPEC", TRAINANYOTH_SPEC);
                cmd.Parameters.AddWithValue("@TRAINDETAILS", TRAINDETAILS);
                cmd.Parameters.AddWithValue("@DTENTERED", DTENTERED);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@Comments", Comments);
                cmd.Parameters.AddWithValue("@TraineeName", TraineeName);
                cmd.Parameters.AddWithValue("@TRAINERNAME", TRAINERNAME);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        //


        public DataSet GEN_ENROLMENT(string Action = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("GenSubEnrol_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public DataSet GetReportScreeningLog(string SpName = null, string Action = null, string Project_ID = null, string INVID = null)
        {

            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(SpName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);


                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;

        }

        public DataSet GetReportEnrolmentLog(string SpName = null, string Action = null, string Project_ID = null, string INVID = null, string Age_Group = null)
        {

            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(SpName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@Age_Group", Age_Group);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;

        }



        #endregion CTMS

        #region datamatrix
        public DataSet getdatamatrix(string ProjectID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("get_risk_matrix", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_ID", ProjectID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        #endregion
        #region MEDICAL

        //GET Dropdown data
        public DataSet GetCustomizedAE_Filter_SP(string Action = null, string Project_ID = null, string FieldName = null,
            string Query = null, string FunctionName = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("CustomizedAE_Filter_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@FieldName", @FieldName);
                cmd.Parameters.AddWithValue("@Query", Query);
                cmd.Parameters.AddWithValue("@FunctionName", FunctionName);
                cmd.Parameters.AddWithValue("@ENTEREDBY", @ENTEREDBY);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }



        public DataSet GetGradeDashboard(string Action = null, string Project_ID = null, string INVID = null, string Grade = null, string Test = null,
            string SUBJID = null, string FrmDate = null, string ToDate = null, string STATUS = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_Grade_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PROJECTID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@Grade", Grade);
                cmd.Parameters.AddWithValue("@Test", Test);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@FrmDate", FrmDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetEventList(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null,
            string Code = null, string UserID = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_AE_EVENTLIST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (Code != null)
                {
                    cmd.Parameters.AddWithValue("@AESPID", Code);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetUncodedEvents(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string UserID = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_AE_EVENTUNCODED", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetSeriousEvents(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string UserID = null, string Filter_Type = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_AE_EVENTSERIOUS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
                cmd.Parameters.AddWithValue("@Filter_Type", Filter_Type);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetOngoingEvents(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string UserID = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_AE_EVENTONGOING", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetDoseDelayedEvents(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string UserID = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_AE_DOSEDELAYWITHDRAW", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetRelatedEvents(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string UserID = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_AE_EVENTRELATED", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetInfusionReaction(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string UserID = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_AE_INFUSIONREACTION", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetSeverity12(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string UserID = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_AE_EVENT12SEV", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetSeverity34(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string UserID = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_AE_EVENT34SEV", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
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
                ////ds.Dispose();
            }
            return ds; ;
        }


        public DataSet GetLaboratoryData(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null,
        string HIGHVALUE = null, string LBTEST = null, string UserID = null, string MedraCode = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_Get_LaboratoryData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);

                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (LBTEST != "0")
                {
                    cmd.Parameters.AddWithValue("@LBTEST", LBTEST);
                }
                if (HIGHVALUE != "")
                {
                    cmd.Parameters.AddWithValue("@HIGHVALUE", HIGHVALUE);
                }

                cmd.Parameters.AddWithValue("@UserID", UserID);

                cmd.Parameters.AddWithValue("@MedraCode", MedraCode);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet SetQueryAE(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null,
           string EventCode = null, string EventTerm = null, string PCode = null, string QueryDetail = null,
               string Rule = null, string Status = null, string RuleType = null, string UserID = null, string LabTest = null, string QueryId = null, string Refrence = null, string Source = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_Query_Management", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@EventCode", EventCode);
                cmd.Parameters.AddWithValue("@EventTerm", EventTerm);
                cmd.Parameters.AddWithValue("@PCode", PCode);
                cmd.Parameters.AddWithValue("@QueryDetail", QueryDetail);
                if (Rule != "")
                {
                    cmd.Parameters.AddWithValue("@Rule", Rule);
                }
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@RuleType", RuleType);
                cmd.Parameters.AddWithValue("@UserID", UserID);

                cmd.Parameters.AddWithValue("@QueryId", QueryId);
                cmd.Parameters.AddWithValue("@Refrence", Refrence);
                cmd.Parameters.AddWithValue("@Source", Source);

                cmd.Parameters.AddWithValue("@LabTest", LabTest);


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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetCMList(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string AESPID = null, string UserID = null, string FromDate = null, string ToDate = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_CM_LIST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@AESPID", AESPID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
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
                ////ds.Dispose();
            }
            return ds; ;
        }


        public DataSet GetMHList(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string AESPID = null, string UserID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_MH_LIST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@AESPID", AESPID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet GetVSList(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string AESPID = null, string UserID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_VS_LIST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@AESPID", AESPID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet GetUncodedCMList(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string AESPID = null, string UserID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_CM_UNCODED", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@AESPID", AESPID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet GetOngoingCMList(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string AESPID = null, string UserID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_CM_ONGOING", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@AESPID", AESPID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        //Lab catagorisation
        public DataSet GetCategoryCount(string Action = null, string PROJECTID = null, string INVID = null, string SUBJID = null, string Category = null,
            string UserID = null, string STATUS = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;

            try
            {
                //Get Data
                cmd = new SqlCommand("MM_GetCategoryCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);

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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet LabResultCategarization(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string CommentID = null, string LBTEST = null, string VISDTL = null, string VISDTH = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_LabResultCategarization", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", ProjectID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@LBTEST", LBTEST);
                cmd.Parameters.AddWithValue("@CommentID", CommentID);
                if (VISDTL != "")
                {
                    cmd.Parameters.AddWithValue("@VISDTL", VISDTL);
                }
                if (VISDTH != "")
                {
                    cmd.Parameters.AddWithValue("@VISDTH", VISDTH);
                }

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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet VisitsCrosstab(string Action = null, string Action1 = null, string ProjectID = null, string INVID = null, string SUBJID = null, string LBTEST = null, string OrganClass = null, string OrganClass2 = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_GET_VISIT_CROSSTAB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Action1", Action1);
                cmd.Parameters.AddWithValue("@Project_ID", ProjectID);
                cmd.Parameters.AddWithValue("@INV", INVID);
                cmd.Parameters.AddWithValue("@SUB", SUBJID);
                cmd.Parameters.AddWithValue("@TESTNAME", LBTEST);
                cmd.Parameters.AddWithValue("@OrganClass", OrganClass);
                cmd.Parameters.AddWithValue("@OrganClass2", OrganClass2);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet LabReCategarization(string Action = null, int @Project_ID = 0, string INVID = null, string SUBJID = null, string TEST = null, string VISIT = null, string VISDAT = null,
            string OldCat = null, string OffLowCat = null, string OffHighCat = null, string Comments = null, string SUB_TEST = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_ReCategarization", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@TEST", TEST);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@VISDAT", VISDAT);
                cmd.Parameters.AddWithValue("@SUB_TEST", SUB_TEST);

                if (OldCat != "")
                {
                    cmd.Parameters.AddWithValue("@OldCat", OldCat);
                }
                if (OffLowCat != "0")
                {
                    cmd.Parameters.AddWithValue("@OffLowCat", OffLowCat);
                }
                if (OffHighCat != "0")
                {
                    cmd.Parameters.AddWithValue("@OffHighCat", OffHighCat);
                }

                cmd.Parameters.AddWithValue("@Comments", Comments);

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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet LabAddComments(string Action = null, string @Project_ID = null, string INVID = null, string SUBJID = null, string LBTEST = null, string AECode = null, string Comments = null, string UserID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("MM_SP_LabAllCatComment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", @Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@LBTEST", LBTEST);
                cmd.Parameters.AddWithValue("@AECode", AECode);
                cmd.Parameters.AddWithValue("@Comments", Comments);
                cmd.Parameters.AddWithValue("@UserID", UserID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet uvVisit(string Action = null, string ProjectID = null, string INV = null, string SUB = null, string TESTNAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("MM_GET_VISIT_CROSSTAB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", ProjectID);
                cmd.Parameters.AddWithValue("@INV", INV);
                cmd.Parameters.AddWithValue("@SUB", SUB);
                cmd.Parameters.AddWithValue("@TESTNAME", TESTNAME);
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
                ////ds.Dispose();
            }
            return ds; ;
        }


        public DataSet GetEventOfInterest(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("[MM_GET_AE_DATA]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);

                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
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
                ////ds.Dispose();
            }
            return ds; ;
        }


        public DataSet GetEventDashboardData(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("[Get_EventDashboard_Data]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", ProjectID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }

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
                ////ds.Dispose();
            }
            return ds; ;
        }


        public DataSet GetMM_SP_AE_FILTER(string Action = null, string ProjectID = null, string INVID = null, string SUBJID = null, string RULE = null, string RULE1 = null, string FunctionName = null, string AETYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("[MM_SP_AE_FILTER]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (RULE != "0")
                {
                    cmd.Parameters.AddWithValue("@RULE", RULE);
                }
                if (RULE1 != "0")
                {
                    cmd.Parameters.AddWithValue("@RULE1", RULE1);
                }
                if (FunctionName != "")
                {
                    cmd.Parameters.AddWithValue("@FunctionName", FunctionName);
                }
                if (AETYPE != "0")
                {
                    cmd.Parameters.AddWithValue("@AETYPE", AETYPE);
                }
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        #endregion MEDICAL

        #region SAE

        public DropDownList BindDropDown(DropDownList ddl, DataTable dt)
        {
            ddl.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "TEXT";
                ddl.DataValueField = "VALUE";
                ddl.DataBind();
            }

            return ddl;
        }

        public DataSet getDDLValue(string Action = null, string Project_ID = null, string SERVICE = null, string VARIABLENAME = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("DDLMaster_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@SERVICE", SERVICE);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public bool ChkVal(CheckBox chk)
        {
            if (chk.Checked == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DataSet getSetCM(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, string CONTID = null, string CMTRT = null, string CMDSTXT = null, string CMDOSU = null, string CMDOSFRQ = null, string CMROUTE = null, string CMSTDAT = null, string CMSTTIM = null, string CMENDAT = null, string CMENTIM = null, string CMONGO = null, string CMINDC = null, string ENTEREDBY = null, string CMSPID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_CM_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@CMTRT", CMTRT);
                cmd.Parameters.AddWithValue("@CMDSTXT", CMDSTXT);
                cmd.Parameters.AddWithValue("@CMDOSU", CMDOSU);
                cmd.Parameters.AddWithValue("@CMDOSFRQ", CMDOSFRQ);
                cmd.Parameters.AddWithValue("@CMROUTE", CMROUTE);
                cmd.Parameters.AddWithValue("@CMSTDAT", CMSTDAT);
                cmd.Parameters.AddWithValue("@CMSTTIM", CMSTTIM);
                cmd.Parameters.AddWithValue("@CMENDAT", CMENDAT);
                cmd.Parameters.AddWithValue("@CMENTIM", CMENTIM);
                cmd.Parameters.AddWithValue("@CMSPID", CMSPID);
                if (CMONGO == "99")
                {
                    cmd.Parameters.AddWithValue("@CMONGO", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CMONGO", CMONGO);
                }
                cmd.Parameters.AddWithValue("@CMINDC", CMINDC);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }




        public DataSet getSetCMYN(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, bool CMYN = false, string ENTEREDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_CMYN_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@CMYN", CMYN);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getSetDD(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, bool DDNA = false, string DSSTDAT = null, string DSSTTIM = null, string AUPERF = null, string PRCDTH = null, string ENTEREDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_DD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                if (DDNA != false)
                {
                    cmd.Parameters.AddWithValue("@DDNA", DDNA);
                }
                cmd.Parameters.AddWithValue("@DSSTDAT", DSSTDAT);
                cmd.Parameters.AddWithValue("@DSSTTIM", DSSTTIM);
                cmd.Parameters.AddWithValue("@AUPERF", AUPERF);
                cmd.Parameters.AddWithValue("@PRCDTH", PRCDTH);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getSetDM(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, string SAESUBJINI = null, string BRTHDAT = null, string SAEAGE = null, string SAEAGEU = null, string SEX = null, string PREGYN = null, string ENTEREDBY = null, string Sub_INI_F = null, string Sub_INI_M = null, string Sub_INI_L = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_DM_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@SAESUBJINI", SAESUBJINI);
                cmd.Parameters.AddWithValue("@BRTHDAT", BRTHDAT);
                if (SAEAGE != "")
                {
                    cmd.Parameters.AddWithValue("@SAEAGE", SAEAGE);
                }
                cmd.Parameters.AddWithValue("@SAEAGEU", SAEAGEU);
                cmd.Parameters.AddWithValue("@SEX", SEX);
                if (PREGYN != "0")
                {
                    cmd.Parameters.AddWithValue("@PREGYN", PREGYN);
                }
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@Sub_INI_F", Sub_INI_F);
                cmd.Parameters.AddWithValue("@Sub_INI_M", Sub_INI_M);
                cmd.Parameters.AddWithValue("@Sub_INI_L", Sub_INI_L);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet getSetVS(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, string VSORRESWT = null, string VSORRESUWT = null, string VSORRESHT = null, string VSORRESUHT = null, string ENTEREDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_VS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);

                if (VSORRESWT != "")
                {
                    cmd.Parameters.AddWithValue("@VSORRESWT", VSORRESWT);
                }
                if (VSORRESUWT != "")
                {
                    cmd.Parameters.AddWithValue("@VSORRESUWT", VSORRESUWT);
                }
                if (VSORRESHT != "")
                {
                    cmd.Parameters.AddWithValue("@VSORRESHT", VSORRESHT);
                }
                if (VSORRESUHT != "")
                {
                    cmd.Parameters.AddWithValue("@VSORRESUHT", VSORRESUHT);
                }

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet getSetCMEVT(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, int CONTID = 0, string CMTRT = null, string CMDSTXT = null, string CMDOSU = null, string CMDOSFRQ = null, string CMROUTE = null, string CMSTDAT = null, string CMSTTIM = null, string CMENDAT = null, string CMENTIM = null, string CMONGO = null, string ENTEREDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_CMEVT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@CMTRT", CMTRT);
                cmd.Parameters.AddWithValue("@CMDSTXT", CMDSTXT);
                cmd.Parameters.AddWithValue("@CMDOSU", CMDOSU);
                cmd.Parameters.AddWithValue("@CMDOSFRQ", CMDOSFRQ);
                cmd.Parameters.AddWithValue("@CMROUTE", CMROUTE);
                cmd.Parameters.AddWithValue("@CMSTDAT", CMSTDAT);
                cmd.Parameters.AddWithValue("@CMSTTIM", CMSTTIM);
                cmd.Parameters.AddWithValue("@CMENDAT", CMENDAT);
                cmd.Parameters.AddWithValue("@CMENTIM", CMENTIM);
                cmd.Parameters.AddWithValue("@CMONGO", CMONGO);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getSetCMEVTYN(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, bool CMYN = false, string ENTEREDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_CMEVTYN_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@CMYN", CMYN);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getSetHOSPDET(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, bool CMYN = false, bool HOSPNA = false, string HOSPDAT = null, string HOSPPRODAT = null, bool HOSPPRONA = false, string SUBDIS = null, string DISDAT = null, string ENTEREDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_HOSPDET_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                if (HOSPNA != false)
                {
                    cmd.Parameters.AddWithValue("@HOSPNA", HOSPNA);
                }
                cmd.Parameters.AddWithValue("@HOSPDAT", HOSPDAT);
                cmd.Parameters.AddWithValue("@HOSPPRODAT", HOSPPRODAT);
                if (HOSPPRONA != false)
                {
                    cmd.Parameters.AddWithValue("@HOSPPRONA", HOSPPRONA);
                }
                if (SUBDIS != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBDIS", SUBDIS);
                }
                cmd.Parameters.AddWithValue("@DISDAT", DISDAT);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }



        public DataSet getSetMH(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, int CONTID = 0, string MHTERM = null, string MHSTDAT = null, string MHENDAT = null, string MHONGO = null, string MHCOM = null, string ENTEREDBY = null, string MHSPID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_MH_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@MHTERM", MHTERM);
                cmd.Parameters.AddWithValue("@MHSTDAT", MHSTDAT);
                cmd.Parameters.AddWithValue("@MHENDAT", MHENDAT);
                cmd.Parameters.AddWithValue("@MHSPID", MHSPID);
                if (MHONGO == "99")
                {
                    cmd.Parameters.AddWithValue("@MHONGO", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@MHONGO", MHONGO);
                }
                cmd.Parameters.AddWithValue("@MHCOM", MHCOM);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet getSetMHYN(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, bool MHYN = false, string ENTEREDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_MHYN_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@MHYN", MHYN);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getSetSAE(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, string SAETERM = null, string AESTDAT = null, string AESTTIM = null, string SAESTDAT = null, string SAESTTIM = null, string SAEAWDAT = null, string SAEAWTIM = null, bool SAESDTH = false, bool SAESLIFE = false, bool SAESHOSP = false, bool SAESDISAB = false, bool SAESCONG = false, bool SAESMIE = false, string SAESEV = null, string SAEREL = null, string SAEOUT = null, string SAEENDAT = null, string SAEENTIM = null, string ENTEREDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_SAE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@SAETERM", SAETERM);
                cmd.Parameters.AddWithValue("@AESTDAT", AESTDAT);
                cmd.Parameters.AddWithValue("@AESTTIM", AESTTIM);
                cmd.Parameters.AddWithValue("@SAESTDAT", SAESTDAT);
                cmd.Parameters.AddWithValue("@SAESTTIM", SAESTTIM);
                cmd.Parameters.AddWithValue("@SAEAWDAT", SAEAWDAT);
                cmd.Parameters.AddWithValue("@SAEAWTIM", SAEAWTIM);
                cmd.Parameters.AddWithValue("@SAESDTH", SAESDTH);
                cmd.Parameters.AddWithValue("@SAESLIFE", SAESLIFE);
                cmd.Parameters.AddWithValue("@SAESHOSP", SAESHOSP);
                cmd.Parameters.AddWithValue("@SAESDISAB", SAESDISAB);
                cmd.Parameters.AddWithValue("@SAESCONG", SAESCONG);
                cmd.Parameters.AddWithValue("@SAESMIE", SAESMIE);
                if (SAESEV != "0")
                {
                    cmd.Parameters.AddWithValue("@SAESEV", SAESEV);
                }
                if (SAEREL != "0")
                {
                    cmd.Parameters.AddWithValue("@SAEREL", SAEREL);
                }
                if (SAEOUT != "0")
                {
                    cmd.Parameters.AddWithValue("@SAEOUT", SAEOUT);
                }
                cmd.Parameters.AddWithValue("@SAEENDAT", SAEENDAT);
                cmd.Parameters.AddWithValue("@SAEENTIM", SAEENTIM);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getCASDET(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string AESTDAT = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_GET_CASDET", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@AESTDAT", AESTDAT);
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
                ////ds.Dispose();
            }
            return ds; ;
        }



        public DataSet getSAEData(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string AESTDAT = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_GET_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@AESTDAT", AESTDAT);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        //public DataSet GetSiteID(string Action = null, string Project_Name = null, string INVID = null, string User_ID = null)
        //{

        //    DataSet ds = new DataSet();

        //    SqlCommand cmd;

        //    SqlDataAdapter adp;

        //    try
        //    {

        //        cmd = new SqlCommand("Get_Site_ID_Details", con);

        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@Action", Action);

        //        cmd.Parameters.AddWithValue("@Project_Name", Project_Name);

        //        cmd.Parameters.AddWithValue("@Site_ID", INVID);

        //        cmd.Parameters.AddWithValue("@User_ID", User_ID);

        //        adp = new SqlDataAdapter(cmd);

        //        adp.Fill(ds);

        //        cmd.Dispose();

        //    }

        //    catch (Exception ex)
        //    {

        //        throw;

        //    }

        //    finally
        //    {

        //        ////ds.Dispose();

        //    }

        //    return ds;

        //}

        //public DataSet GetSetPROJECTDETAILS(string Action = null, int Project_ID = -1, string PRODUCTID = null, string PROJNAME = null, string SPONSOR = null, string PROJSTDAT = null, string PROJDUR = null, string ENTEREDBY = null)
        //{

        //    SqlCommand cmd;

        //    SqlDataAdapter adp;

        //    DataSet ds = new DataSet();

        //    try
        //    {

        //        cmd = new SqlCommand("PROJDETAILS_SP", con);

        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@Action", Action);

        //        cmd.Parameters.AddWithValue("@Project_ID", Project_ID);

        //        cmd.Parameters.AddWithValue("@PRODUCTID", PRODUCTID);

        //        cmd.Parameters.AddWithValue("@PROJNAME", PROJNAME);

        //        cmd.Parameters.AddWithValue("@SPONSOR", SPONSOR);

        //        if (PROJSTDAT != "")
        //        {

        //            cmd.Parameters.AddWithValue("@PROJSTDAT", PROJSTDAT);

        //        }

        //        cmd.Parameters.AddWithValue("@PROJDUR", PROJDUR);

        //        cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);

        //        con.Open();

        //        adp = new SqlDataAdapter(cmd);

        //        adp.Fill(ds);

        //        cmd.Dispose();

        //        con.Close();

        //        return ds;

        //    }

        //    catch (Exception ex)
        //    {

        //        throw;

        //    }

        //    finally
        //    {

        //        cmd = null;

        //        adp = null;

        //    }

        //    return ds;

        //}

        public DataSet getSetFOLLOWUP(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_CREATE_FOLLOWUP_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet sendREPORT(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_SEND_REPORT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet setCASDET(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, int SAERVER = 0, bool NULYN = false, string AESTDAT = null, string NULREAS = null, string TRANSDAT = null, string SAERPTRGNM = null, string SAERPTRFSNM = null, bool COMPSTAT = false, string ENTEREDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_CASDET_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@SAERVER", SAERVER);
                cmd.Parameters.AddWithValue("@NULYN", NULYN);
                cmd.Parameters.AddWithValue("@AESTDAT", AESTDAT);
                cmd.Parameters.AddWithValue("@NULREAS", NULREAS);
                cmd.Parameters.AddWithValue("@TRANSDAT", TRANSDAT);
                cmd.Parameters.AddWithValue("@SAERPTRGNM", SAERPTRGNM);
                cmd.Parameters.AddWithValue("@SAERPTRFSNM", SAERPTRFSNM);
                cmd.Parameters.AddWithValue("@COMPSTAT", COMPSTAT);
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
                ////ds.Dispose();
            }
            return ds; ;
        }


        public DataSet getSetSAE_Comments(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string SAEID = null, string VERSIONID = null, string ModuleName = null, string Comments = null, string ENTEREDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("SAE_Comments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@VERSIONID", VERSIONID);
                cmd.Parameters.AddWithValue("@ModuleName", ModuleName);
                cmd.Parameters.AddWithValue("@Comments", Comments);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
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
                ////ds.Dispose();
            }
            return ds;
        }


        #endregion SAE

        #region GCP

        public DataSet getGCP(string Action = null, string Project_ID = null, string INVID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Graph_GCP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }


        #endregion

        #region widmedical
        public DataSet getMMwidget(string Action = null, string Project_ID = null, string INVID = null, string COUNTRYID = null, string USER_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Widget_Medical_Monitoring", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@User_ID", USER_ID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        #endregion

        #region labdata
        public DataSet getlabdata(string Action = null, string Project_ID = null, string INVID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Graph_Laboratory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        #endregion

        #region pd
        public DataSet getscreenfailure(string Project_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Screen_Fail_Rate_Monthwise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getpd(string Action = null, string Project_ID = null, string INVID = null, string COUNTRYID = null, string USER_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Widget_Graph_PD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@User_ID", USER_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        public DataSet sitereview(string Action = null, string Project_ID = null, string INVID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Widget_Graph_ProjectManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        #endregion
        #region Graph
        public DataSet getMedicalMonitoringSummary(string Action = null, string Project_ID = null, string INVID = null, string PERCENT = null, string VISIT = null, string SUBJID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Graph_Medical_Monitoring", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@PERCENT", PERCENT);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet enrollmentcombinegraph(string Action = null, string Project_ID = null, string INVID = null, string COUNTRYID = null, string USER_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Enrollment_Rate_Monthwise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@User_ID", USER_ID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet screenrate(string Project_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Widget_Screen_Rate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                //cmd.Parameters.AddWithValue("@INVID", INVID);
                //cmd.Parameters.AddWithValue("@top", top);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet aesitewise(string Action = null, string Project_ID = null, string INVID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Widget_Graph_SiteReview", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                //cmd.Parameters.AddWithValue("@top", top);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet querydata(string Action = null, string Project_ID = null, string INVID = null, string COUNTRYID = null, string USER_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Widget_Graph_Query", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@User_ID", USER_ID);
                //cmd.Parameters.AddWithValue("@top", top);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet mdicaldashgraph(string Action = null, string Project_ID = null, string INVID = null, string top = null,
            string COUNTRYID = null, string USER_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Widget_Graph_AE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                cmd.Parameters.AddWithValue("@top", top);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@USER_ID", USER_ID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet graphLab(string Action = null, string Project_ID = null, string INVID = null, string top = null, string COUNTRYID = null, string USER_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Graph_Laboratory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@top", top);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@User_ID", USER_ID);
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
                ////ds.Dispose();
            }
            return ds;
        }
        public DataSet dashboard(string Action = null, string Project_ID = null, string INVID = null, string COUNTRYID = null, string USER_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Graph_CTMS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@User_ID", USER_ID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        #endregion Graph

        #region ISSUE
        public DataSet getsetISSUES(string Action = null, string ISSUES_ID = null, string Project_ID = null, string INVID = null,
            string SUBJID = null, string VISITNUM = null, string MVID = null, string SectionID = null, string SubSectionID = null,
            string Summary = null, string Status = null, string Priority = null,
            string Department = null, string ISSUEDate = null, string DueDate = null, string ResolutionDate = null,
            string ResolvedVersion = null, string ISSUEBy = null, string AssignedTo = null, string DtAssigned = null,
            string AssignedBy = null, string ChangedBy = null, string Description = null, string Nature = null,
            string DateClosed = null, string PDCODE1 = null, string PD1Catagory = null, string PDCODE2 = null, string PD2Catagory = null,
            string Source = null, string Refrence = null, string EventCode = null, string Rule = null,
            string DTENTERED = null, string ENTEREDBY = null, string UPDATEDDAT = null,
            string UPDATEDBY = null, int FactorID = 0, string Factor = null, string PVID = null, string RECID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("ISSUES_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ISSUES_ID", ISSUES_ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                if (INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }

                if (SUBJID != "0" && SUBJID != "")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                if (MVID != "0" && MVID != "")
                {
                    cmd.Parameters.AddWithValue("@MVID", MVID);
                }

                if (SectionID != "0" && SectionID != "")
                {
                    cmd.Parameters.AddWithValue("@SectionID", SectionID);
                }
                if (SubSectionID != "0" && SubSectionID != "")
                {
                    cmd.Parameters.AddWithValue("@SubSectionID", SubSectionID);
                }

                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@Summary", Summary);
                if (Status != "0")
                {
                    cmd.Parameters.AddWithValue("@Status", Status);
                }
                cmd.Parameters.AddWithValue("@Priority", Priority);
                cmd.Parameters.AddWithValue("@Department", Department);
                if (ISSUEDate != "")
                {
                    cmd.Parameters.AddWithValue("@ISSUEDate", ISSUEDate);
                }

                if (DueDate != "")
                {
                    cmd.Parameters.AddWithValue("@DueDate", DueDate);
                }
                if (ResolutionDate != "")
                {
                    cmd.Parameters.AddWithValue("@ResolutionDate", ResolutionDate);
                }
                cmd.Parameters.AddWithValue("@ResolvedVersion", ResolvedVersion);
                cmd.Parameters.AddWithValue("@ISSUEBy", ISSUEBy);
                cmd.Parameters.AddWithValue("@AssignedTo", AssignedTo);
                cmd.Parameters.AddWithValue("@DtAssigned", DtAssigned);
                cmd.Parameters.AddWithValue("@AssignedBy", AssignedBy);
                cmd.Parameters.AddWithValue("@ChangedBy", ChangedBy);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Nature", Nature);
                if (DateClosed != "")
                {
                    cmd.Parameters.AddWithValue("@DateClosed", DateClosed);
                }
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);
                if (PDCODE1 != "0")
                {
                    cmd.Parameters.AddWithValue("@PDCODE1", PDCODE1);
                    cmd.Parameters.AddWithValue("@PD1Catagory", PD1Catagory);
                }

                if (PDCODE2 != "0")
                {
                    cmd.Parameters.AddWithValue("@PDCODE2", PDCODE2);
                    cmd.Parameters.AddWithValue("@PD2Catagory", PD2Catagory);
                }
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Refrence", Refrence);
                if (EventCode != "")
                {
                    cmd.Parameters.AddWithValue("@EventCode", EventCode);
                }
                if (Rule != "")
                {
                    cmd.Parameters.AddWithValue("@Rule", Rule);
                }
                cmd.Parameters.AddWithValue("@FactorId", FactorID);
                cmd.Parameters.AddWithValue("@Factor", Factor);

                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);

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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getsetRisk_SP(string Action = null, string RISK_ID = null, string ISSUES_ID = null, string Project_ID = null,
            string INVID = null, string SUBJID = null, string VISITNUM = null, string MVID = null, string SECTIONID = null,
            string SUBSECTIONID = null, string DateIdentified = null, string IdentifiedBy = null, string RiskDescription = null,
            string RiskImpact = null, string RPN = null, string RiskCategory = null, string Department = null, string P = null,
            string S = null, string D = null, string RiskStatus = null, string RiskType = null, string RootCause = null,
            string Source = null, string Refrence = null, string RiskOwner = null, string RISKCOM_ID = null, string Comment = null,
            string CommentDate = null, string RISK_ACAUSE_ID = null, string ALLIED_CAUSE = null,
            string Nature = null, string PDCODE1 = null, string PD1Catagory = null, string PDCODE2 = null, string PD2Catagory = null,
            string Category = null, string SubCategory = null, string Factor = null, string FactorID = null, string RiskCons = null,
            string Impact = null, string SugMitig = null, string SugRiskCat = null, string TransCat = null, string TransSubCat = null,
            string DTENTERED = null, string ENTEREDBY = null, string UPDATEDDAT = null, string UPDATEDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("RISK_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@RISK_ID", RISK_ID);
                cmd.Parameters.AddWithValue("@ISSUES_ID", ISSUES_ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MVID", MVID);
                cmd.Parameters.AddWithValue("@SECTIONID", SECTIONID);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);
                cmd.Parameters.AddWithValue("@DateIdentified", DateIdentified);
                cmd.Parameters.AddWithValue("@IdentifiedBy", IdentifiedBy);
                cmd.Parameters.AddWithValue("@RiskDescription", RiskDescription);
                cmd.Parameters.AddWithValue("@RiskImpact", RiskImpact);
                cmd.Parameters.AddWithValue("@RPN", RPN);
                cmd.Parameters.AddWithValue("@RiskCategory", RiskCategory);
                if (Department != "0")
                {
                    cmd.Parameters.AddWithValue("@Department", Department);
                }
                cmd.Parameters.AddWithValue("@P", P);
                cmd.Parameters.AddWithValue("@S", S);
                cmd.Parameters.AddWithValue("@D", D);
                if (RiskStatus != "0")
                {
                    cmd.Parameters.AddWithValue("@RiskStatus", RiskStatus);
                }
                if (RiskType != "0")
                {
                    cmd.Parameters.AddWithValue("@RiskType", RiskType);
                }

                cmd.Parameters.AddWithValue("@RootCause", RootCause);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Refrence", Refrence);
                cmd.Parameters.AddWithValue("@RiskOwner", RiskOwner);
                cmd.Parameters.AddWithValue("@RISKCOM_ID", RISKCOM_ID);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@CommentDate", CommentDate);

                cmd.Parameters.AddWithValue("@RISK_ACAUSE_ID", RISK_ACAUSE_ID);
                cmd.Parameters.AddWithValue("@ALLIED_CAUSE", ALLIED_CAUSE);
                if (Nature != "0")
                {
                    cmd.Parameters.AddWithValue("@Nature", Nature);
                }
                if (PDCODE1 != "0")
                {
                    cmd.Parameters.AddWithValue("@PDCODE1", PDCODE1);
                    cmd.Parameters.AddWithValue("@PD1Catagory", PD1Catagory);
                }
                if (PDCODE2 != "0")
                {
                    cmd.Parameters.AddWithValue("@PDCODE2", PDCODE2);
                    cmd.Parameters.AddWithValue("@PD2Catagory", PD2Catagory);
                }


                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);

                //New fields
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@SubCategory", SubCategory);
                cmd.Parameters.AddWithValue("@Factor", Factor);
                cmd.Parameters.AddWithValue("@RiskCons", RiskCons);
                cmd.Parameters.AddWithValue("@Impact", Impact);
                cmd.Parameters.AddWithValue("@SugMitig", SugMitig);
                cmd.Parameters.AddWithValue("@SugRiskCat", SugRiskCat);
                cmd.Parameters.AddWithValue("@TransCat", TransCat);
                cmd.Parameters.AddWithValue("@TransSubCat", TransSubCat);

                ////


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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getsetRiskMitigation(string Action = null, string RISK_ID = null, string RISKMIT_ID = null, string RiskMitigation = null, string RiskResponsibility = null, string DateTarget = null, string DateCommunicated = null, string DateActioned = null, string DTENTERED = null, string ENTEREDBY = null, string UPDATEDDAT = null, string UPDATEDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("RISKMITIGATION_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@RISK_ID", RISK_ID);
                cmd.Parameters.AddWithValue("@RISKMIT_ID", RISKMIT_ID);
                cmd.Parameters.AddWithValue("@RiskMitigation", RiskMitigation);
                cmd.Parameters.AddWithValue("@RiskResponsibility", RiskResponsibility);
                cmd.Parameters.AddWithValue("@DateTarget", DateTarget);
                cmd.Parameters.AddWithValue("@DateCommunicated", DateCommunicated);
                cmd.Parameters.AddWithValue("@DateActioned", DateActioned);

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);

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
                ////ds.Dispose();
            }
            return ds; ;
        }


        public DataSet getsetIssueComments(string Action = null, string ISSUESCOM_ID = null, string ISSUES_ID = null, string CommentDate = null,
            string Comment = null, string ENTEREDBY = null, string UPDATEDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("[ISSUESCOMMENTS_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ISSUESCOM_ID", ISSUESCOM_ID);
                cmd.Parameters.AddWithValue("@ISSUES_ID", ISSUES_ID);
                cmd.Parameters.AddWithValue("@CommentDate", CommentDate);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);


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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getsetAttachments(string Action = null, string ISSUESATTACH_ID = null, string ISSUES_ID = null, string Name = null,
                string ContentType = null, byte[] Attachments = null, string ENTEREDBY = null, string UPDATEDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("[ISSUESATTACH_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ISSUESATTACH_ID", ISSUESATTACH_ID);
                cmd.Parameters.AddWithValue("@ISSUES_ID", ISSUES_ID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@Attachments", Attachments);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);


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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getsetIssueRootCause(string Action = null, string ISSUESRC_ID = null, string ISSUES_ID = null, string RootCause = null,
          string DtAssigned = null, string AssignedBy = null, string ENTEREDBY = null, string UPDATEDBY = null, string Sub_cause = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("[ISSUESROOTCAUSE_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ISSUESRC_ID", ISSUESRC_ID);
                cmd.Parameters.AddWithValue("@ISSUES_ID", ISSUES_ID);
                cmd.Parameters.AddWithValue("@RootCause", RootCause);
                cmd.Parameters.AddWithValue("@Sub_cause", Sub_cause);
                cmd.Parameters.AddWithValue("@DtAssigned", DtAssigned);
                cmd.Parameters.AddWithValue("@AssignedBy", AssignedBy);

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);


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
                ////ds.Dispose();
            }
            return ds; ;
        }



        public DataSet getsetIssueActionable(string Action = null, string ISSUESACT_ID = null, string ISSUES_ID = null, string ActionBy = null,
          string DueDate = null, string Actionable = null, string DateCompleted = null, string ENTEREDBY = null, string UPDATEDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("[ISSUESACTIONS_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ISSUESACT_ID", ISSUESACT_ID);
                cmd.Parameters.AddWithValue("@ISSUES_ID", ISSUES_ID);
                cmd.Parameters.AddWithValue("@ActionBy", ActionBy);
                cmd.Parameters.AddWithValue("@DueDate", DueDate);
                cmd.Parameters.AddWithValue("@Actionable", Actionable);
                cmd.Parameters.AddWithValue("@DateCompleted", DateCompleted);

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);


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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getsetIssueImpact(string Action = null, string ISS_Imapct_ID = null, string ISSUES_ID = null, string Project_ID = null,
         string Impact = null, string ENTEREDBY = null, string UPDATEDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("[ISSUESIMPACT_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ISS_Imapct_ID", ISS_Imapct_ID);
                cmd.Parameters.AddWithValue("@ISSUES_ID", ISSUES_ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Impact", Impact);

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);

                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);


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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getsetPDCode(string Action = null, string NATURE = null, string PDCODE1 = null, string PDCODE2 = null,
            string CATEGORY = null, string SUB_CATEGORY = null, string FACTOR = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("[PDCODE_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@NATURE", NATURE);
                cmd.Parameters.AddWithValue("@PDCODE1", PDCODE1);
                cmd.Parameters.AddWithValue("@PDCODE2", PDCODE2);
                cmd.Parameters.AddWithValue("@CATEGORY", CATEGORY);
                cmd.Parameters.AddWithValue("@SUB_CATEGORY", SUB_CATEGORY);
                cmd.Parameters.AddWithValue("@FACTOR", FACTOR);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet ProtocolVoilation_SP(string Action = null, string PROTVOIL_ID = null, string ISSUES_ID = null, string Project_ID = null,
          string INVID = null, string SUBJID = null, string VISITNUM = null, string MVID = null, string Description = null,
             string Summary = null, string Nature = null, string PDCODE1 = null, string PDCODE2 = null, string Department = null,
               string ENTEREDBY = null, string UPDATEDBY = null, string Status = null, string Priority_Default = null, string Priority_Ops = null, string Priority_Med = null,
            string Priority_Final = null, string Source = null, string Refrence = null, string Rationalise = null,
            string Dt_Otcome = null, string Close_Date = null, string ActiveDate = null, string ActiveBy = null, string PDMaster_ID = null, string DUPLICATE = null,
            string COUNTRYID = null, string Dt_IRB = null)
        {
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
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                ds = new DataSet();
                cmd = new SqlCommand("[ProtocolVoilation_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PROTVOIL_ID", PROTVOIL_ID);
                cmd.Parameters.AddWithValue("@PDMaster_ID", PDMaster_ID);
                cmd.Parameters.AddWithValue("@ISSUES_ID", ISSUES_ID);
                if (INVID != "" && INVID != "0")
                {
                    cmd.Parameters.AddWithValue("@INVID", INVID);
                }
                if (SUBJID != "" && SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MVID", MVID);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Summary", Summary);
                cmd.Parameters.AddWithValue("@Nature", Nature);
                cmd.Parameters.AddWithValue("@PDCODE1", PDCODE1);
                cmd.Parameters.AddWithValue("@PDCODE2", PDCODE2);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Priority_Default", Priority_Default);
                cmd.Parameters.AddWithValue("@Priority_Ops", Priority_Ops);
                cmd.Parameters.AddWithValue("@Priority_Med", Priority_Med);
                cmd.Parameters.AddWithValue("@Priority_Final", Priority_Final);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Refrence", Refrence);
                cmd.Parameters.AddWithValue("@Rationalise", Rationalise);
                if (Dt_Otcome != "")
                {
                    cmd.Parameters.AddWithValue("@Dt_Otcome", Dt_Otcome);
                }
                if (Close_Date != "")
                {
                    cmd.Parameters.AddWithValue("@Close_Date", Close_Date);
                }
                cmd.Parameters.AddWithValue("@ActiveDate", ActiveDate);
                cmd.Parameters.AddWithValue("@ActiveBy", ActiveBy);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@Dt_IRB", Dt_IRB);

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
                ////ds.Dispose();
            }
            return ds; ;
        }


        public DataSet getsetPDComments(string Action = null, string PDCOM_ID = null, string PROTVOIL_ID = null, string CommentDate = null, string Comment = null)
        {
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                ds = new DataSet();
                cmd = new SqlCommand("[PDCOMMENTS_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PDCOM_ID", PDCOM_ID);
                cmd.Parameters.AddWithValue("@PROTVOIL_ID", PROTVOIL_ID);
                cmd.Parameters.AddWithValue("@CommentDate", CommentDate);
                cmd.Parameters.AddWithValue("@Comment", Comment);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
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
                ////ds.Dispose();
            }
            return ds; ;
        }


        public DataSet getsetPDImpact(string Action = null, string PD_Imapct_ID = null, string PROTVOIL_ID = null, string Impact = null)
        {
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                ds = new DataSet();
                cmd = new SqlCommand("[PDIMPACT_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PD_Imapct_ID", PD_Imapct_ID);
                cmd.Parameters.AddWithValue("@PROTVOIL_ID", PROTVOIL_ID);
                cmd.Parameters.AddWithValue("@Impact", Impact);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
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
                ////ds.Dispose();
            }
            return ds; ;
        }



        public DataSet getsetPDCAPA(string Action = null, string PDCAPA_ID = null, string PROTVOIL_ID = null, string CAPA = null,
   string Responsibility = null, string Resolution_DT = null)
        {
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                ds = new DataSet();
                cmd = new SqlCommand("[PDCAPA_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PDCAPA_ID", PDCAPA_ID);
                cmd.Parameters.AddWithValue("@PROTVOIL_ID", PROTVOIL_ID);
                cmd.Parameters.AddWithValue("@CAPA", CAPA);
                cmd.Parameters.AddWithValue("@Responsibility", Responsibility);
                cmd.Parameters.AddWithValue("@Resolution_DT", Resolution_DT);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet getsetRelatedIssues(string Action = null, string ISSUESREL_ID = null, string ISSUES_ID = null, string Summary = null,
            string RISSUES_ID = null, string ENTEREDBY = null, string UPDATEDBY = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("[ISSUESRELATED_SP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ISSUESREL_ID", ISSUESREL_ID);
                cmd.Parameters.AddWithValue("@ISSUES_ID", ISSUES_ID);
                cmd.Parameters.AddWithValue("@Summary", Summary);
                cmd.Parameters.AddWithValue("@RISSUES_ID", RISSUES_ID);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        #endregion ISSUE

        #region QUERY
        public DataSet getsetQuery(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string EventCode = null,
                                    string EventTerm = null, string PCode = null, string QueryDetail = null, string Rule = null, string Status = null,
                                    string QueryBY = null, string QueryDT = null, string ActionBY = null, string ActionDT = null, string Source = null,
                                    string Refrence = null, string Department = null, string Priority = null, string QueryType = null, string LabTest = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("QUERY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                if (EventCode != "")
                {
                    cmd.Parameters.AddWithValue("@EventCode", EventCode);
                }
                if (EventTerm != "")
                {
                    cmd.Parameters.AddWithValue("@EventTerm", EventTerm);
                }
                cmd.Parameters.AddWithValue("@PCode", PCode);
                cmd.Parameters.AddWithValue("@QueryDetail", QueryDetail);
                cmd.Parameters.AddWithValue("@Rule", Rule);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@QueryBY", QueryBY);
                cmd.Parameters.AddWithValue("@QueryDT", QueryDT);
                cmd.Parameters.AddWithValue("@ActionBY", ActionBY);
                cmd.Parameters.AddWithValue("@ActionDT", ActionDT);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Refrence", Refrence);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Priority", Priority);
                cmd.Parameters.AddWithValue("@QueryType", QueryType);
                cmd.Parameters.AddWithValue("@LabTest", LabTest);
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
                ////ds.Dispose();
            }
            return ds; ;
        }
        #endregion QUERY

        public DataSet Get_Email_Details(string Action = null, string ProjectID = null, string INVID = null, string Event_ID = null)
        {
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("Get_Email_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_ID", ProjectID);
                cmd.Parameters.AddWithValue("@Inv_ID", INVID);
                cmd.Parameters.AddWithValue("@Event_ID", Event_ID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }


        public DataSet Get_MM_GET_GRAPH_DATA(string Action = null, string ProjectID = null, string INVID = null,
        string SUBJID = null, string IEFailDesc = null, string top = null, string AEPT = null, string LBTEST = null, string RiskImpact = null)
        {
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("MM_GET_GRAPH_DATA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", ProjectID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@IEFailDesc", IEFailDesc);
                cmd.Parameters.AddWithValue("@top", top);
                cmd.Parameters.AddWithValue("@AEPT", AEPT);
                cmd.Parameters.AddWithValue("@LBTEST", LBTEST);
                cmd.Parameters.AddWithValue("@RiskImpact", RiskImpact);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public DataSet getRiskList(string Action = "", string Id = "", string CategoryId = "", string SubCategoryId = "")
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("getRiskList", con);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                cmd.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public DataSet getRiskRemoveList(string ProjectId = null, string Action = null, string CatID = null, string SubCatID = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("getRiskRemoveList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@CatID", CatID);
                cmd.Parameters.AddWithValue("@SubCatID", SubCatID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public string insertRiskEvent(string ACTION = "", string CategoryId = "", string SubcategoryId = "", string TopicId = "", string Risk_Description = "", string Impacts = "", string Possible_Mitigations = "", string SugestedRiskCategory = "", string RiskNature = "", string TranscelerateCategory = "", string TranscelerateSubCategory = "", string ProjectId = "", string Probability = "", string Severity = "", string Detection = "", string RPN = "", string Risk_Notes = "", string Risk_Count = "", string Risk_Date = "", string Risk_Manager = "", string Dept = "", string issueId = "",
            string Source = "", string Reference = "", string RStatus = "", string RType = "", string ROwner = "", string PROTOCOLID = null)
        {
            string msg = "";
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("insertRiskEvent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                cmd.Parameters.AddWithValue("@SubcategoryId", SubcategoryId);
                cmd.Parameters.AddWithValue("@TopicId", TopicId);
                cmd.Parameters.AddWithValue("@Risk_Description", Risk_Description);
                cmd.Parameters.AddWithValue("@Impacts", Impacts);
                cmd.Parameters.AddWithValue("@Possible_Mitigations", Possible_Mitigations);
                cmd.Parameters.AddWithValue("@SugestedRiskCategory", SugestedRiskCategory);
                cmd.Parameters.AddWithValue("@RiskNature", RiskNature);
                cmd.Parameters.AddWithValue("@TranscelerateCategory", TranscelerateCategory);
                cmd.Parameters.AddWithValue("@TranscelerateSubCategory", TranscelerateSubCategory);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@Probability", Probability);
                cmd.Parameters.AddWithValue("@Severity", Severity);
                cmd.Parameters.AddWithValue("@Detection", Detection);
                cmd.Parameters.AddWithValue("@RPN", RPN);
                cmd.Parameters.AddWithValue("@Risk_Notes", Risk_Notes);
                cmd.Parameters.AddWithValue("@Risk_Count", Risk_Count);
                cmd.Parameters.AddWithValue("@Risk_Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Risk_Manager", Risk_Manager);
                cmd.Parameters.AddWithValue("@Dept", Dept);
                cmd.Parameters.AddWithValue("@issueId", issueId);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Reference", Reference);
                cmd.Parameters.AddWithValue("@RStatus", RStatus);
                cmd.Parameters.AddWithValue("@RType", RType);
                cmd.Parameters.AddWithValue("@ROwner", ROwner);
                cmd.Parameters.AddWithValue("@PROTOCOLID", PROTOCOLID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Close();
                msg = "Record Inserted Successfully";
            }
            catch (Exception ex)
            {
                con.Close();
                msg = ex.ToString();
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return msg;
        }

        public DataTable getcategory(string Action = "", string Categoryname = "", string Categoryvalue = "", string SubCategoryname = "", string SubCategoryvalue = "", string Factorname = "", string ProjectId = "")
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("getRisk_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Categoryname ", Categoryname);
                cmd.Parameters.AddWithValue("@Categoryvalue", Categoryvalue);
                cmd.Parameters.AddWithValue("@SubCategoryname", SubCategoryname);
                cmd.Parameters.AddWithValue("@SubCategoryvalue", SubCategoryvalue);
                cmd.Parameters.AddWithValue("@Factorname", Factorname);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectId);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Open();
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public DataSet getEventCount(string Category = "", string SubCategory = "", string Factor = "", string ProjectId = "")
        {
            ds = new DataSet();
            string count = "";
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("getEventCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@SubCategory", SubCategory);
                cmd.Parameters.AddWithValue("@Factor", Factor);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                adp = new SqlDataAdapter(cmd);
                con.Open();
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                count = ex.Message.ToString();
                con.Close();
            }
            return ds;
        }

        public DataSet UpdateRisk(string id = "", string desc = "", string impact = "", string posmit = "", string sugriskcat = "", string transcat = "", string tranSubCat = "", string nature = "", string category = "", string subCat = "", string factor = "", string keyvalue = "")
        {
            ds = new DataSet();
            string count = "";
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("UpdateRisk", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(id));
                cmd.Parameters.AddWithValue("@Category", Convert.ToInt32(category));
                cmd.Parameters.AddWithValue("@SubCategory", Convert.ToInt32(subCat));
                cmd.Parameters.AddWithValue("@Factor", Convert.ToInt32(factor));
                cmd.Parameters.AddWithValue("@Desc", desc);
                cmd.Parameters.AddWithValue("@Impact", impact);
                cmd.Parameters.AddWithValue("@PosMit", posmit);
                cmd.Parameters.AddWithValue("@SugRiskCat", sugriskcat);
                cmd.Parameters.AddWithValue("@RNature", nature);
                cmd.Parameters.AddWithValue("@TransCat", transcat);
                cmd.Parameters.AddWithValue("@TranSubCat", tranSubCat);
                cmd.Parameters.AddWithValue("@keyValue", keyvalue);
                adp = new SqlDataAdapter(cmd);
                con.Open();
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                count = ex.Message.ToString();
                con.Close();
            }
            return ds;
        }

        public DataSet GetRiskByID(string id = "")
        {
            ds = new DataSet();
            string count = "";
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("GetRiskByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(id));
                adp = new SqlDataAdapter(cmd);
                con.Open();
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                count = ex.Message.ToString();
                con.Close();
            }
            return ds;
        }

        //public DataTable getcategory(string Action = "", string Categoryname = "", string Categoryvalue = "", string SubCategoryname = "", string SubCategoryvalue = "", string Factorname = "")
        //{
        //    ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        cmd = new SqlCommand("getRisk_Master", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Action", Action);
        //        cmd.Parameters.AddWithValue("@Categoryname ", Categoryname);
        //        cmd.Parameters.AddWithValue("@Categoryvalue", Categoryvalue);
        //        cmd.Parameters.AddWithValue("@SubCategoryname", SubCategoryname);
        //        cmd.Parameters.AddWithValue("@SubCategoryvalue", SubCategoryvalue);
        //        cmd.Parameters.AddWithValue("@Factorname", Factorname);
        //        adp = new SqlDataAdapter(cmd);
        //        adp.Fill(ds);
        //        con.Open();
        //        dt = ds.Tables[0];
        //        cmd.Dispose();
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        con.Close();
        //    }
        //    return dt;
        //}


        public string checkcategory(string Action = "", string Categoryname = "", string Categoryvalue = "", string SubCategoryname = "", string SubCategoryvalue = "", string Factorname = "")
        {
            ds = new DataSet();
            string msg = "";
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("checkmasterdata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Categoryname", Categoryname);
                cmd.Parameters.AddWithValue("@Categoryvalue", Categoryvalue);
                cmd.Parameters.AddWithValue("@SubCategoryname", SubCategoryname);
                cmd.Parameters.AddWithValue("@SubCategoryvalue", SubCategoryvalue);
                cmd.Parameters.AddWithValue("@Factorname", Factorname);
                adp = new SqlDataAdapter(cmd);
                con.Open();
                adp.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    msg = "Present";
                }
                else
                {
                    msg = "Absent";
                }
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return msg;
        }

        public DataTable checktypeid(string Action = "", string Categoryname = "", string Categoryvalue = "", string SubCategoryname = "", string SubCategoryvalue = "", string Factorname = "")
        {
            DataTable dt = new DataTable();
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("checktypeidmasterdata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Categoryname", Categoryname);
                cmd.Parameters.AddWithValue("@Categoryvalue", Categoryvalue);
                cmd.Parameters.AddWithValue("@SubCategoryname", SubCategoryname);
                cmd.Parameters.AddWithValue("@SubCategoryvalue", SubCategoryvalue);
                cmd.Parameters.AddWithValue("@Factorname", Factorname);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Open();
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return dt;
        }

        public string insertmasterdata(string Action = "", string Categoryname = "", string Categoryvalue = "", string SubCategoryname = "", string SubCategoryvalue = "", string Factorname = "", string Typeid = "")
        {
            DataTable dt = new DataTable();
            ds = new DataSet();
            string msg = "";
            try
            {
                cmd = new SqlCommand("insertmasterdata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Categoryname", Categoryname);
                cmd.Parameters.AddWithValue("@Categoryvalue", Categoryvalue);
                cmd.Parameters.AddWithValue("@SubCategoryname", SubCategoryname);
                cmd.Parameters.AddWithValue("@SubCategoryvalue", SubCategoryvalue);
                cmd.Parameters.AddWithValue("@Factorname", Factorname);
                cmd.Parameters.AddWithValue("@Typeid", Typeid);
                con.Open();
                cmd.ExecuteNonQuery();
                msg = "Success";
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return msg;
        }


        public string updatemasterdata(string Action = "", string Id = "", string Categoryname = "", string Categoryvalue = "", string SubCategoryname = "", string SubCategoryvalue = "", string Factorname = "", string Typeid = "")
        {
            DataTable dt = new DataTable();
            ds = new DataSet();
            string msg = "";
            try
            {
                cmd = new SqlCommand("UpdateMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Categoryname", Categoryname);
                cmd.Parameters.AddWithValue("@Categoryvalue", Categoryvalue);
                cmd.Parameters.AddWithValue("@SubCategoryname", SubCategoryname);
                cmd.Parameters.AddWithValue("@SubCategoryvalue", SubCategoryvalue);
                cmd.Parameters.AddWithValue("@Factorname", Factorname);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return msg;
        }

        public DataTable Bindmaster(string Action = "", string Id = "")
        {
            DataTable dt = new DataTable();
            ds = new DataSet();
            string msg = "";
            try
            {
                cmd = new SqlCommand("BindMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Id", Id);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return dt;
        }

        public string DeleteMaster(string Action = "", string Id = "")
        {
            DataTable dt = new DataTable();
            ds = new DataSet();
            string msg = "";
            try
            {
                cmd = new SqlCommand("DeleteMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return msg;
        }

        public DataTable checkRisk_Master(string Action = "", string Id = "")
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("checkRisk_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@id ", Id);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Open();
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return dt;
        }

        public string insertriskdata(string Action = "", string CategoryValue = "", string SubCategoryvalue = "", string Factorsvalue = "", string Risk_Consideration = "", string Impact = "", string Suggested_Mitigation = "", string Suggested_Riskcategory = "", string Risk_Nature = "", string Transcelerate_category = "", string Transcelerate_Subcategory = "", string Identifiedby = "", string keyvalue = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("insertriskdata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@CategoryValue", CategoryValue);
                cmd.Parameters.AddWithValue("@SubCategoryvalue", SubCategoryvalue);
                cmd.Parameters.AddWithValue("@Factorsvalue", Factorsvalue);
                cmd.Parameters.AddWithValue("@Risk_Consideration", Risk_Consideration);
                cmd.Parameters.AddWithValue("@Impact", Impact);
                cmd.Parameters.AddWithValue("@Suggested_Mitigation", Suggested_Mitigation);
                cmd.Parameters.AddWithValue("@Suggested_Riskcategory", Suggested_Riskcategory);
                cmd.Parameters.AddWithValue("@Risk_Nature", Risk_Nature);
                cmd.Parameters.AddWithValue("@Transcelerate_category", Transcelerate_category);
                cmd.Parameters.AddWithValue("@Transcelerate_Subcategory", Transcelerate_Subcategory);
                cmd.Parameters.AddWithValue("@Identifiedby", Identifiedby);
                cmd.Parameters.AddWithValue("@keyvalue", keyvalue);
                con.Open();
                cmd.ExecuteNonQuery();
                msg = "Success";
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return msg;
        }

        public string insertRiskBucket(string Action = null, string ProjectId = "", string RiskId = "", string RCat = "", string RSubCat = "", string RFactor = "", string RManager = "", string RiskActualID = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("insertRiskBucket", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@RiskId", RiskId);
                cmd.Parameters.AddWithValue("@RCat", RCat);
                cmd.Parameters.AddWithValue("@RSubCat", RSubCat);
                cmd.Parameters.AddWithValue("@RFactor", RFactor);
                cmd.Parameters.AddWithValue("@RManager", RManager);
                cmd.Parameters.AddWithValue("@RiskActualId", RiskActualID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Open();
                cmd.Dispose();
                con.Close();
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    msg = dt.Rows[0][0].ToString();
                }
                else
                {
                    msg = "Success";
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return msg;
        }

        public DataSet Risk_Cause_SP(string Action = null, string ID = null, string Event_ID = null, string Project_ID = null,
            string Cause = null, string SubCause = null, string Comment = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Risk_Cause_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Event_ID", Event_ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Cause", Cause);
                cmd.Parameters.AddWithValue("@SubCause", SubCause);
                cmd.Parameters.AddWithValue("@Comment", Comment);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }

        public DataSet Risk_Indicator_SP(string Action = null, string ID = null
            , string L1 = null, string L2 = null, string CL1 = null, string CL2 = null, string InvL1 = null, string InvL2 = null
            , string LV0 = null, string LV1 = null, string LV2 = null, string CLV0 = null, string CLV1 = null, string CLV2 = null
            , string InvLV0 = null, string InvLV1 = null, string InvLV2 = null, string Result = null
            , string LAct1 = null, string LAct2 = null, string CLAct1 = null, string CLAct2 = null, string InvLAct1 = null, string InvLAct2 = null
            , string LPost1 = null, string LPost2 = null, string CLPost1 = null, string CLPost2 = null, string InvLPost1 = null, string InvLPost2 = null
            , string SEQNO = null, string Actionable = null, string Condition = null
            , string Field1 = null, string Condition1 = null, string Value1 = null, string AndOr1 = null
            , string Field2 = null, string Condition2 = null, string Value2 = null, string AndOr2 = null
            , string Field3 = null, string Condition3 = null, string Value3 = null, string AndOr3 = null
            , string Field4 = null, string Condition4 = null, string Value4 = null, string AndOr4 = null
            , string Field5 = null, string Condition5 = null, string Value5 = null, string AndOr5 = null
            , string Field6 = null, string Condition6 = null, string Value6 = null, string AndOr6 = null
            , string Field7 = null, string Condition7 = null, string Value7 = null, string AndOr7 = null
            , string Field8 = null, string Condition8 = null, string Value8 = null, string AndOr8 = null
            , string Field9 = null, string Condition9 = null, string Value9 = null, string AndOr9 = null
            , string Field10 = null, string Condition10 = null, string Value10 = null
            , string Score = null, string Green = null, string Yellow = null, string Red = null
            , string X = null, string Y = null, string Z = null
            , string A = null, string B = null, string C = null, string D = null, string E = null
            , string Cat1 = null, string Cat2 = null, string Cat3 = null, string Cat4 = null, string Cat5 = null
            , string Cat6 = null, string Cat7 = null, string Cat8 = null, string Cat9 = null, string Cat10 = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Risk_Indicator_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@L1", L1);
                cmd.Parameters.AddWithValue("@L2", L2);
                cmd.Parameters.AddWithValue("@CL1", CL1);
                cmd.Parameters.AddWithValue("@CL2", CL2);
                cmd.Parameters.AddWithValue("@InvL1", InvL1);
                cmd.Parameters.AddWithValue("@InvL2", InvL2);
                cmd.Parameters.AddWithValue("@LV0", LV0);
                cmd.Parameters.AddWithValue("@LV1", LV1);
                cmd.Parameters.AddWithValue("@LV2", LV2);
                cmd.Parameters.AddWithValue("@CLV0", CLV0);
                cmd.Parameters.AddWithValue("@CLV1", CLV1);
                cmd.Parameters.AddWithValue("@CLV2", CLV2);
                cmd.Parameters.AddWithValue("@InvLV0", InvLV0);
                cmd.Parameters.AddWithValue("@InvLV1", InvLV1);
                cmd.Parameters.AddWithValue("@InvLV2", InvLV2);
                cmd.Parameters.AddWithValue("@Result", Result);
                cmd.Parameters.AddWithValue("@LAct1", LAct1);
                cmd.Parameters.AddWithValue("@LAct2", LAct2);
                cmd.Parameters.AddWithValue("@CLAct1", CLAct1);
                cmd.Parameters.AddWithValue("@CLAct2", CLAct2);
                cmd.Parameters.AddWithValue("@InvLAct1", InvLAct1);
                cmd.Parameters.AddWithValue("@InvLAct2", InvLAct2);
                cmd.Parameters.AddWithValue("@LPost1", LPost1);
                cmd.Parameters.AddWithValue("@LPost2", LPost2);
                cmd.Parameters.AddWithValue("@CLPost1", CLPost1);
                cmd.Parameters.AddWithValue("@CLPost2", CLPost2);
                cmd.Parameters.AddWithValue("@InvLPost1", InvLPost1);
                cmd.Parameters.AddWithValue("@InvLPost2", InvLPost2);

                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@Actionable", Actionable);
                cmd.Parameters.AddWithValue("@Condition", Condition);
                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@Condition1", Condition1);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@Field5", Field5);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@AndOr5", AndOr5);
                cmd.Parameters.AddWithValue("@Field6", Field6);
                cmd.Parameters.AddWithValue("@Condition6", Condition6);
                cmd.Parameters.AddWithValue("@Value6", Value6);
                cmd.Parameters.AddWithValue("@AndOr6", AndOr6);
                cmd.Parameters.AddWithValue("@Field7", Field7);
                cmd.Parameters.AddWithValue("@Condition7", Condition7);
                cmd.Parameters.AddWithValue("@Value7", Value7);
                cmd.Parameters.AddWithValue("@AndOr7", AndOr7);
                cmd.Parameters.AddWithValue("@Field8", Field8);
                cmd.Parameters.AddWithValue("@Condition8", Condition8);
                cmd.Parameters.AddWithValue("@Value8", Value8);
                cmd.Parameters.AddWithValue("@AndOr8", AndOr8);
                cmd.Parameters.AddWithValue("@Field9", Field9);
                cmd.Parameters.AddWithValue("@Condition9", Condition9);
                cmd.Parameters.AddWithValue("@Value9", Value9);
                cmd.Parameters.AddWithValue("@AndOr9", AndOr9);
                cmd.Parameters.AddWithValue("@Field10", Field10);
                cmd.Parameters.AddWithValue("@Condition10", Condition10);
                cmd.Parameters.AddWithValue("@Value10", Value10);

                cmd.Parameters.AddWithValue("@Score", Score);
                cmd.Parameters.AddWithValue("@Green", Green);
                cmd.Parameters.AddWithValue("@Yellow", Yellow);
                cmd.Parameters.AddWithValue("@Red", Red);
                cmd.Parameters.AddWithValue("@X", X);
                cmd.Parameters.AddWithValue("@Y", Y);
                cmd.Parameters.AddWithValue("@Z", Z);
                cmd.Parameters.AddWithValue("@A", A);
                cmd.Parameters.AddWithValue("@B", B);
                cmd.Parameters.AddWithValue("@C", C);
                cmd.Parameters.AddWithValue("@D", D);
                cmd.Parameters.AddWithValue("@E", E);

                cmd.Parameters.AddWithValue("@Cat1", Cat1);
                cmd.Parameters.AddWithValue("@Cat2", Cat2);
                cmd.Parameters.AddWithValue("@Cat3", Cat3);
                cmd.Parameters.AddWithValue("@Cat4", Cat4);
                cmd.Parameters.AddWithValue("@Cat5", Cat5);
                cmd.Parameters.AddWithValue("@Cat6", Cat6);
                cmd.Parameters.AddWithValue("@Cat7", Cat7);
                cmd.Parameters.AddWithValue("@Cat8", Cat8);
                cmd.Parameters.AddWithValue("@Cat9", Cat9);
                cmd.Parameters.AddWithValue("@Cat10", Cat10);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }

        public string DeleteRiskBucket(string Id = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("DeleteRiskBucket", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Id);
                con.Open();
                cmd.ExecuteNonQuery();
                msg = "Success";
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return msg;
        }

        public string deleteRiskBank(string Id = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("deleteRiskBank", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Id);
                con.Open();
                cmd.ExecuteNonQuery();
                msg = "Success";
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return msg;
        }

        public DataTable getBucketById(string Id = "")
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("getBucketById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id ", Id);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Open();
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return dt;
        }

        public string updateRiskBucket(string Id = "", string P = "", string S = "", string D = "", string RPN = "", string RiskCat = "", string Notes = "", string Down_Trigger = "", string Up_Trigger = "", string User_ID = "", string Comment = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("updateRiskBucket", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@P", P);
                cmd.Parameters.AddWithValue("@S", S);
                cmd.Parameters.AddWithValue("@D", D);
                cmd.Parameters.AddWithValue("@RPN", RPN);
                cmd.Parameters.AddWithValue("@RiskCat", RiskCat);
                cmd.Parameters.AddWithValue("@Notes", Notes);
                cmd.Parameters.AddWithValue("@Up_Trigger", Up_Trigger);
                cmd.Parameters.AddWithValue("@Down_Trigger", Down_Trigger);
                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                con.Open();
                cmd.ExecuteNonQuery();
                msg = "Success";
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return msg;
        }
        public DataSet RM_Get_Risk_Matrix(string ProjectID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("RM_Get_Risk_Matrix", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_ID", ProjectID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet RM_Get_Risk_Matrix_Events(string ProjectID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("RM_Get_Risk_Matrix_Events", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_ID", ProjectID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataTable getprojectevents(string Action = "", string Id = "", string From = "", string To = "", string ProjectId = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("getprojectevents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@From", From);
                cmd.Parameters.AddWithValue("@To", To);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Open();
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return dt;
        }

        public DataTable getRisk_Impact(string Action = "", string RISK_ID = "", string RiskImpact = "", string ENTEREDBY = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("getRisk_Impact", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@RISK_ID", RISK_ID);
                cmd.Parameters.AddWithValue("@Risk_Impacts", RiskImpact);
                cmd.Parameters.AddWithValue("@EnteredBy", ENTEREDBY);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Open();
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return dt;
        }

        public DataTable Update_ProjEvents(string Action = "", string REvent_desc = "", string RISK_ID = "", string status = "", string RImpacts = "",
            string source = "", string Reference = "", string RootCause = "", string Risk_Owner = "", string Comments = "", string Rcategory = "",
            string RSubcategory = "", string Rfactors = "", string Risk_Type = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("Update_ProjEvents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@REvent_desc", REvent_desc);
                cmd.Parameters.AddWithValue("@RISK_ID", RISK_ID);
                cmd.Parameters.AddWithValue("@RImpacts", RImpacts);
                cmd.Parameters.AddWithValue("@source", source);
                cmd.Parameters.AddWithValue("@Reference", Reference);
                cmd.Parameters.AddWithValue("@RootCause", RootCause);
                cmd.Parameters.AddWithValue("@Risk_Owner", Risk_Owner);
                cmd.Parameters.AddWithValue("@Comments", Comments);
                cmd.Parameters.AddWithValue("@Rcategory", Rcategory);
                cmd.Parameters.AddWithValue("@RSubcategory", RSubcategory);
                cmd.Parameters.AddWithValue("@Rfactors", Rfactors);
                cmd.Parameters.AddWithValue("@Risk_Type", Risk_Type);
                cmd.Parameters.AddWithValue("@Risk_Status", status);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                con.Open();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return dt;
        }

        public DataSet getsetRisk_Type(string Action = "", string RISK_ID = "", string RiskType = "", string ENTEREDBY = "", string variablename = "", string ProjectId = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("GET_Risk_TYPE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@RISK_ID", RISK_ID);
                cmd.Parameters.AddWithValue("@RiskType", RiskType);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@variablename", variablename);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Open();
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return ds;
        }

        public DataSet getBucketBetween(string ProjectID = "", string From = "", string To = "")
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("getBucketBetween", con);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectID);
                cmd.Parameters.AddWithValue("@From", From);
                cmd.Parameters.AddWithValue("@To", To);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public DataTable GetMedraFilters(string Filter = "", string F1 = "", string F2 = "", string F3 = "", string F4 = "", string ProjectId = "")
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("GetMedraFilters", con);
                cmd.Parameters.AddWithValue("@Filter", Filter);
                cmd.Parameters.AddWithValue("@F1", F1);
                cmd.Parameters.AddWithValue("@F2", F2);
                cmd.Parameters.AddWithValue("@F3", F3);
                cmd.Parameters.AddWithValue("@F4", F4);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(ds);
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return dt;
        }

        public DataTable GetMedraData(string Filter1 = "", string Filter2 = "", string Filter3 = "", string Filter4 = "", string Filter5 = "", string ProjectId = "")
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("GetMedraData", con);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@Filter1", Filter1);
                cmd.Parameters.AddWithValue("@Filter2", Filter2);
                cmd.Parameters.AddWithValue("@Filter3", Filter3);
                cmd.Parameters.AddWithValue("@Filter4", Filter4);
                cmd.Parameters.AddWithValue("@Filter5", Filter5);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(ds);
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return dt;
        }

        public DataSet Graphs_Risk(string Action = "", string ProjectId = "", string VariableName = "")
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Graphs_Risk", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@VariableName", VariableName);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataSet Graphs_Risk_Events(string Action = "", string ProjectId = "", string RiskImpact = "", string VariableName = "",
            string INVID = null, string COUNTRYID = null, string USER_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("Graphs_Risk_Events", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@RiskImpact", RiskImpact);
                cmd.Parameters.AddWithValue("@VariableName", VariableName);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@User_ID", USER_ID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public string MedraProjectBucket(string Id = "", string ProjectId = "", string pt_code = "", string llt_code = "", string exp_percent = "",
            bool EXPECTED_EVENTS = false, bool AE_OF_INTEREST = false, string ACTION = null)
        {
            string msg = "";
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("MedraProjectBucket", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@pt_code", pt_code);
                cmd.Parameters.AddWithValue("@llt_code", llt_code);
                cmd.Parameters.AddWithValue("@exp_percent", exp_percent);
                cmd.Parameters.AddWithValue("@EXPECTED_EVENTS", EXPECTED_EVENTS);
                cmd.Parameters.AddWithValue("@AE_OF_INTEREST", AE_OF_INTEREST);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                msg = "Done";
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                throw;
            }
            finally
            {
                ////ds.Dispose();
            }
            return msg; ;
        }

        public DataSet getExpectedEventsPercent(string Project_ID = null)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand("getExpectedEventsPercent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", Project_ID);
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
                ////ds.Dispose();
            }
            return ds; ;
        }

        public DataTable BindExpectedEvents(string Action = "", string ProjectId = "", string Id = "")
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("MM_MeddraExpectedEvents", con);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(ds);
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return dt;
        }

        public string DeleteExpectedEvents(string Action = "", string Id = "", string ProjectId = "")
        {
            string msg = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("MM_MeddraExpectedEvents", con);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                msg = "Deleted";
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return msg;
        }

        public DataTable getRisk_Impact(string Action = "", string RISK_ID = "", string RiskImpact = "", string ENTEREDBY = "", string VariableName = "", string ProjectId = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("getRisk_Impact", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@RISK_ID", RISK_ID);
                cmd.Parameters.AddWithValue("@Risk_Impacts", RiskImpact);
                cmd.Parameters.AddWithValue("@EnteredBy", ENTEREDBY);
                cmd.Parameters.AddWithValue("@VariableName", VariableName);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Open();
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return dt;
        }

        public DataTable getUncodedEvent(string Id = "")
        {
            string msg = "";
            ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("getUncodedEvent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Open();
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return dt;
        }

        public string UpdateUncodedEvents(string Id = "", string AEPT = "", string AEPTC = "")
        {
            string msg = "";
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("UpdateUncodedEvents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@AEPT", Id);
                cmd.Parameters.AddWithValue("@AEPTC", Id);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                msg = "Done";
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return msg;
        }





        // Unblind Log
        public DataSet GetSetUnblindLog(string Action = null, string ID = null, string Project_ID = null, string INVID = null, string SUBJID = null,
                                           string SAETERM = null, string DATDEATH = null, string DRUGNAM = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("Unblind_Log_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SAETERM", SAETERM);
                if (DATDEATH != "")
                {
                    cmd.Parameters.AddWithValue("@DATDEATH", DATDEATH);
                }
                cmd.Parameters.AddWithValue("@DRUGNAM", DRUGNAM);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        // Signature Log
        public DataSet GetSetSignatureLog(string Action = null, string ID = null, string Project_ID = null, string INVID = null,
                                           string SitePerName = null, string SitePerRole = null, string AuthCode = null,
                                           bool Sign = false, bool SignShort = false, string STDATAUTH = null,
                                           string ENDDATAUTH = null, bool INVSIGN_ST = false, bool INVSIGN_END = false
                                            , string INVSIGNDAT_ST = null, string INVSIGNDAT_END = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("Signature_Log_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SitePerName", SitePerName);
                cmd.Parameters.AddWithValue("@SitePerRole", SitePerRole);
                cmd.Parameters.AddWithValue("@AuthCode", AuthCode);
                cmd.Parameters.AddWithValue("@Sign", Sign);
                cmd.Parameters.AddWithValue("@SignShort", SignShort);

                if (STDATAUTH != "")
                {
                    cmd.Parameters.AddWithValue("@STDATAUTH", STDATAUTH);
                }

                if (ENDDATAUTH != "")
                {
                    cmd.Parameters.AddWithValue("@ENDDATAUTH", ENDDATAUTH);
                }

                cmd.Parameters.AddWithValue("@INVSIGN_ST", INVSIGN_ST);
                cmd.Parameters.AddWithValue("@INVSIGN_END", INVSIGN_END);

                if (INVSIGNDAT_ST != "")
                {
                    cmd.Parameters.AddWithValue("@INVSIGNDAT_ST", INVSIGNDAT_ST);
                }

                if (INVSIGNDAT_END != "")
                {
                    cmd.Parameters.AddWithValue("@INVSIGNDAT_END", INVSIGNDAT_END);
                }


                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        public DataSet User_Activation_Deactivation(string Action = null, string User_Name = null, string User_ID = null, string ENTEREDBY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("User_Activation_Deactivation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
            return ds;
        }



        public DataSet GetDRP_DWN_MASTER(string Action = null, string VARIABLE_NAME = null, string VALUE = null, string TEXT = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("DRP_DWN_MASTER_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@VARIABLE_NAME", VARIABLE_NAME);
                cmd.Parameters.AddWithValue("@VALUE", VALUE);
                cmd.Parameters.AddWithValue("@TEXT", TEXT);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public bool CheckDate(string date)
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

        //Get PreInitiation Reports
        public DataSet GetReport_PI(string SpName = null, string Action = null, string Project_ID = null, string PIID = null, string SUBSECTIONID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(SpName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@MVID", PIID);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }


        public DataSet GetReportSiteTrainingRecord(string SpName = null, string Action = null, string TrainingID = null, string Project_ID = null, string INVID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(SpName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@TrainingID", TrainingID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }




        public DataSet drp_SectionsMaster(string SectionID = null, string SECTION = null, string SUBSECTIONID = null, string SUBSECTION = null, string Action = null, string SEQNO = null, string PreFix = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("drp_SectionsMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SECTIONID", SectionID);
                cmd.Parameters.AddWithValue("@SECTION", SECTION);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SUBSECTIONID);
                cmd.Parameters.AddWithValue("@SUBSECTION", SUBSECTION);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@PREFIX", PreFix);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public DataSet get_ChecklistMaster(string SectionID = null, string SubSectionID = null, string ProjectID = null, string Action = null, string Id = null, string VariableName = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("get_ChecklistMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SECTIONID", SectionID);
                cmd.Parameters.AddWithValue("@SUBSECTIONID", SubSectionID);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectID);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@VariableName", VariableName);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }




        public string Insert_Checklists(string Id = null, string FieldName = null, string ProjectID = null, string Action = null, string SectionId = null, string SubSectionId = null, string variablename = null, string controlType = null, string Date = null, string SubSectionYN = null, string EnteredBy = null, string SEQNO = null, string DROPDOWN_YN = null, string SUBCHECKLISTNAME = null, string CHECKLISTID = null, string MultipleYN = null)
        {
            string msg = "";
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("Insert_Checklists", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.Parameters.AddWithValue("@fieldName", FieldName);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@SubSectionId", SubSectionId);
                cmd.Parameters.AddWithValue("@VariableName", variablename);
                cmd.Parameters.AddWithValue("@ControlType", controlType);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@EnteredBy", EnteredBy);
                cmd.Parameters.AddWithValue("@SubSectionYN", SubSectionYN);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@DROPDOWN_YN", DROPDOWN_YN);
                cmd.Parameters.AddWithValue("@CHECKLISTID", CHECKLISTID);
                cmd.Parameters.AddWithValue("@SUBCHECKLISTNAME", SUBCHECKLISTNAME);
                cmd.Parameters.AddWithValue("@MultipleYN", MultipleYN);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                msg = "Done";
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return msg;
        }


        public DataSet CTMS_Tracker(string Id = null, string FieldName = null, string ProjectID = null, string Action = null,
            string SectionId = null, string SubSectionId = null, string variablename = null, string controlType = null, string Date = null,
            string SubSectionYN = null, string EnteredBy = null, string SEQNO = null, string DROPDOWN_YN = null,
            string SUBCHECKLISTNAME = null, string CHECKLISTID = null, string TrackerId = null, string TrackerName = null, string Prefix = null,
            string INVID = null, string DATA = null, string MAXLEN = null, bool MULTILINEYN = false, string RECID = null, string ContID = null)
        {
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("CTMS_Tracker_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.Parameters.AddWithValue("@TrackerId", TrackerId);
                cmd.Parameters.AddWithValue("@TrackerName", TrackerName);
                cmd.Parameters.AddWithValue("@fieldName", FieldName);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@SubSectionId", SubSectionId);
                cmd.Parameters.AddWithValue("@VariableName", variablename);
                cmd.Parameters.AddWithValue("@ControlType", controlType);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@EnteredBy", EnteredBy);
                cmd.Parameters.AddWithValue("@SubSectionYN", SubSectionYN);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@DROPDOWN_YN", DROPDOWN_YN);
                cmd.Parameters.AddWithValue("@CHECKLISTID", CHECKLISTID);
                cmd.Parameters.AddWithValue("@SUBCHECKLISTNAME", @SUBCHECKLISTNAME);
                cmd.Parameters.AddWithValue("@Prefix", Prefix);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@DATA", DATA);

                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@ContID", ContID);

                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            return ds;
        }


        public DataSet Budget_SP(string Action = null, string Dept_Id = null, string Dept_Name = null, string DtEntered = null, string EnteredBy = null, string Complexity = null, string Project_Id = null, string Role_ID = null, string DS = null, string Hrs = null, string Sponsor = null, string Sub_Task_ID = null, string Task_ID = null, string Sub_Task_Name = null, string Task_Name = null, string Amt = null, string Role = null, string Rate = null, string ID = null, string Milestone = null, string Budget = null,
                    string Rate1 = null, string Rate2 = null, string Rate3 = null, string Rate4 = null, string Rate5 = null, string Rate6 = null, string Rate7 = null, string Rate8 = null, string Rate9 = null, string Rate10 = null, string Site = null, string Others = null, string PassThrough = null, string SEQNO = null, string DtPlan = null, string DtActual = null, string DtOgPlan = null, string Version_ID = null, string Unit = null, string Cost_Per_Unit = null, string Total_Amt = null, string Multiple = null, string Document = null, string INVID = null, string GroupName = null, string TOM = null, string Event = null
            , string MASTERDB = null, string Folder_ID = null, string SubFolder_ID = null, string Download = null, string iTOM = null, string Doc_ID = null, string Recurring = null, string Timeline = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Budget_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Dept_ID", Dept_Id);
                cmd.Parameters.AddWithValue("@Dept_Name", Dept_Name);
                cmd.Parameters.AddWithValue("@DtEntered", DtEntered);
                cmd.Parameters.AddWithValue("@EnteredBy", EnteredBy);
                cmd.Parameters.AddWithValue("@Complexity", Complexity);
                cmd.Parameters.AddWithValue("@Project_Id", Project_Id);
                cmd.Parameters.AddWithValue("@Role_ID", Role_ID);
                cmd.Parameters.AddWithValue("@DS", DS);
                cmd.Parameters.AddWithValue("@Hrs", Hrs);
                cmd.Parameters.AddWithValue("@Sponsor", Sponsor);
                cmd.Parameters.AddWithValue("@Sub_Task_ID", Sub_Task_ID);
                cmd.Parameters.AddWithValue("@Task_ID", Task_ID);
                cmd.Parameters.AddWithValue("@Sub_Task_Name", Sub_Task_Name);
                cmd.Parameters.AddWithValue("@Task_Name", Task_Name);
                cmd.Parameters.AddWithValue("@Amt", Amt);
                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.Parameters.AddWithValue("@Rate", Rate);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Milestone", Milestone);
                cmd.Parameters.AddWithValue("@Budget", Budget);
                cmd.Parameters.AddWithValue("@Rate1", Rate1);
                cmd.Parameters.AddWithValue("@Rate2", Rate2);
                cmd.Parameters.AddWithValue("@Rate3", Rate3);
                cmd.Parameters.AddWithValue("@Rate4", Rate4);
                cmd.Parameters.AddWithValue("@Rate5", Rate5);
                cmd.Parameters.AddWithValue("@Rate6", Rate6);
                cmd.Parameters.AddWithValue("@Rate7", Rate7);
                cmd.Parameters.AddWithValue("@Rate8", Rate8);
                cmd.Parameters.AddWithValue("@Rate9", Rate9);
                cmd.Parameters.AddWithValue("@Rate10", Rate10);
                cmd.Parameters.AddWithValue("@Site", Site);
                cmd.Parameters.AddWithValue("@Others", Others);
                cmd.Parameters.AddWithValue("@PassThrough", PassThrough);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@DtPlan", DtPlan);
                cmd.Parameters.AddWithValue("@DtActual", DtActual);
                cmd.Parameters.AddWithValue("@Version_ID", Version_ID);
                cmd.Parameters.AddWithValue("@Unit", Unit);
                cmd.Parameters.AddWithValue("@Cost_Per_Unit", Cost_Per_Unit);
                cmd.Parameters.AddWithValue("@Total_Amt", Total_Amt);
                cmd.Parameters.AddWithValue("@Multiple", Multiple);
                cmd.Parameters.AddWithValue("@Document", Document);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@GroupName", GroupName);
                cmd.Parameters.AddWithValue("@TOM", TOM);
                cmd.Parameters.AddWithValue("@iTOM", iTOM);
                cmd.Parameters.AddWithValue("@Event", Event);
                //cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);

                cmd.Parameters.AddWithValue("@Doc_ID", Doc_ID);
                cmd.Parameters.AddWithValue("@Folder_ID", Folder_ID);
                cmd.Parameters.AddWithValue("@SubFolder_ID", SubFolder_ID);

                cmd.Parameters.AddWithValue("@Download", Download);
                cmd.Parameters.AddWithValue("@Recurring", Recurring);

                cmd.Parameters.AddWithValue("@Timeline", Timeline);

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




        public DataSet CTMS_Matrix_SP(string Action = null, string ID = null, string Group_ID = null, string P_Task_ID = null, string P_SubTask_ID = null, string C_Task_ID = null, string C_SubTask_ID = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("CTMS_Matrix_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Group_ID", Group_ID);
                cmd.Parameters.AddWithValue("@P_Task_ID", P_Task_ID);
                cmd.Parameters.AddWithValue("@P_SubTask_ID", P_SubTask_ID);
                cmd.Parameters.AddWithValue("@C_Task_ID", C_Task_ID);
                cmd.Parameters.AddWithValue("@C_SubTask_ID", C_SubTask_ID);

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
            return ds;
        }

        public DataSet CTMS_Documents(string Action = null, string ID = null, string Project_ID = null, string Task_ID = null, string Sub_Task_ID = null, string INVID = null, string DocType = null, string FileName = null, string ContentType = null, byte[] Data = null, string Folder = null, string SEQNO = null, string Folder_ID = null, string SubFolder_ID = null, string User = null,
           bool CTMS = false, bool ETMF = false, bool SITE = false, bool SPONSOR = false, bool QA = false, string RefNo = null, string Artifact_Name = null, string DocName = null, string AutoReplace = null, string DocID = null, string DocTypeId = null, bool Internal = false, string UniqueRefNo = null,
           bool VerDate = false, string VerTYPE = null, string Comment = null, string UnblindingType = null, bool E_Download = false, bool E_Upload = false, string SPECtitle = null, string DateTitle = null, string DEFINITION = null, string USER_ID = null, bool VerSPEC = false, bool Email_Enable = false)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("CTMS_Document_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Task_ID", Task_ID);
                cmd.Parameters.AddWithValue("@Sub_Task_ID", Sub_Task_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@DocType", DocType);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@Folder", Folder);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@Folder_ID", Folder_ID);
                cmd.Parameters.AddWithValue("@SubFolder_ID", SubFolder_ID);
                cmd.Parameters.AddWithValue("@User", User);
                //cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);

                cmd.Parameters.AddWithValue("@CTMS", CTMS);
                cmd.Parameters.AddWithValue("@ETMF", ETMF);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SPONSOR", SPONSOR);
                cmd.Parameters.AddWithValue("@QA", QA);

                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                cmd.Parameters.AddWithValue("@Artifact_Name", Artifact_Name);

                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@AutoReplace", AutoReplace);
                cmd.Parameters.AddWithValue("@DocID", DocID);
                cmd.Parameters.AddWithValue("@DocTypeId", DocTypeId);

                cmd.Parameters.AddWithValue("@Internal", Internal);

                cmd.Parameters.AddWithValue("@UniqueRefNo", UniqueRefNo);
                cmd.Parameters.AddWithValue("@VerDate", VerDate);
                cmd.Parameters.AddWithValue("@VerTYPE", VerTYPE);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@UnblindingType", UnblindingType);
                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["USER_ID"].ToString());
                cmd.Parameters.AddWithValue("@E_Download", E_Download);
                cmd.Parameters.AddWithValue("@E_Upload", E_Upload);
                cmd.Parameters.AddWithValue("@SPECtitle", SPECtitle);
                cmd.Parameters.AddWithValue("@DateTitle", DateTitle);

                cmd.Parameters.AddWithValue("@DEFINITION", DEFINITION);
                cmd.Parameters.AddWithValue("@VerSPEC", VerSPEC);
                cmd.Parameters.AddWithValue("@Email_Enable", Email_Enable);


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




        public DataSet CommonSP_Execution(String SPName = null, String Action = null, String InvId = null, string SubjId = null, string visit = null, string INDICATION = null,
            string MEDAUTH_FORM = null, string MEDAUTH_FIELD = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand(SPName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@INVID", InvId);
                cmd.Parameters.AddWithValue("@SUBJID", SubjId);
                cmd.Parameters.AddWithValue("@visit", visit);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", MEDAUTH_FORM);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
                cmd.Parameters.AddWithValue("@Unblind", HttpContext.Current.Session["Unblind"].ToString());
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

        //GET SET PV
        public void DM_PV_SP(string Action = null, string PVID = null, string PROJECTID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string PAGENUM = null,
            string PAGESTATUS = null, string ENTEREDBY = null, string SUBJINI = null, string INDICATION = null, string VISITCOUNT = null, bool HasMissing = false)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("DM_PV_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@PAGENUM", PAGENUM);
                cmd.Parameters.AddWithValue("@PAGESTATUS", PAGESTATUS);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SUBJINI", SUBJINI);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@VISITCOUNT", VISITCOUNT);
                cmd.Parameters.AddWithValue("@HasMissing", HasMissing);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
        }

        public DataSet GetRecordsQueryDetails(string Action = null, string PVID = null, int ID = 0, string Comment = null, string Reason = null,
            int RECID = 0, string USERID = null, string VARIABLENAME = null, bool MULTIPLEYN = false)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand("DM_QUERYDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@REASON", Reason);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@MULTIPLEYN", MULTIPLEYN);

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


        public void ExecuteProc(string PVID = null, string SUBJID = null, string TableName = null, string MODULENAME = null)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("DM_EXEC_PROC", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@TableName", TableName);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
        }
        //GET SET Master Structure


        //Set MANUAL QUERY
        public DataSet SetManualQuery(String Action, String PVID = null, int CONTID = 0, String QID = null, String SUBJID = null,
            String QUERYTEXT = null, String MODULENAME = null, String FIELDNAME = null, string TABLENAME = null, string VARIABLENAME = null,
            string ENTEREDBY = null, int RECID = 0, string TIMEZONE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_Manual_Query_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@QID", QID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ENTEREDBY", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);

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

        public DataSet SiteBudget_SP(string Action = null, string Id = null, string Task_Id = null, string Activity = null, string Monthly = null, string VisitWise = null, string Project_Id = null, string Dept_Id = null, string Visit = null, string Visit_Id = null, string Act_Id = null, string Sub_Task_ID = null, string Rate = null, string Site_ID = null, string Type = null, string Units = null, string Amount = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SiteBudget_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.Parameters.AddWithValue("@Dept_ID", Dept_Id);
                cmd.Parameters.AddWithValue("@Task_ID", Task_Id);
                cmd.Parameters.AddWithValue("@Sub_Task_ID", Sub_Task_ID);
                cmd.Parameters.AddWithValue("@Activity", Activity);
                cmd.Parameters.AddWithValue("@Monthly", Monthly);
                cmd.Parameters.AddWithValue("@VisitWise", VisitWise);
                cmd.Parameters.AddWithValue("@Project_ID", Project_Id);
                cmd.Parameters.AddWithValue("@Visit", Visit);
                cmd.Parameters.AddWithValue("@Visit_Id", Visit_Id);
                cmd.Parameters.AddWithValue("@Act_Id", Act_Id);
                cmd.Parameters.AddWithValue("@Rate", Rate);
                cmd.Parameters.AddWithValue("@Site_ID", Site_ID);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Units", Units);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);

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

        public DataSet Training_SP(string Action = null, string ID = null, string Role_ID = null, string Emp_ID = null, string StartDate = null, string EndDate = null, string Project_ID = null, string Section_ID = null, string Section = null, string SubSection_ID = null, string SubSection = null, string Site = null, string Internal = null, string TrainingPlan = null, string FileName = null, string ContentType = null, byte[] Data = null, string INVID = null, string Name = null, string Role = null, string EmailID = null,
            string MASTERDBNAME = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Training_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Role_ID", Role_ID);
                cmd.Parameters.AddWithValue("@Emp_ID", Emp_ID);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Section_ID", Section_ID);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@SubSection_ID", SubSection_ID);
                cmd.Parameters.AddWithValue("@SubSection", SubSection);
                cmd.Parameters.AddWithValue("@Site", Site);
                cmd.Parameters.AddWithValue("@Internal", Internal);
                cmd.Parameters.AddWithValue("@TrainingPlan", TrainingPlan);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                //cmd.Parameters.AddWithValue("@DBNAME", DBNAME);

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
            return ds;
        }
        public DataSet Train_Verification_SP(string Action = null, string ID = null, string Plan_ID = null, string SEQNO = null, string FIELDNAME = null, string CONTROLTYPE = null, string CLASS = null, string QueNo = null, string Items = null, string Date = null, string SubSec_ID = null, string Section_ID = null, string ANS = null, string Project_ID = null, string Emp_ID = null, string SubSection_IDs = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Train_Verification_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Plan_ID", Plan_ID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@CLASS", CLASS);
                cmd.Parameters.AddWithValue("@QueNo", QueNo);
                cmd.Parameters.AddWithValue("@Items", Items);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@SubSection_ID", SubSec_ID);
                cmd.Parameters.AddWithValue("@Section_ID", Section_ID);
                cmd.Parameters.AddWithValue("@ANS", ANS);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Emp_ID", Emp_ID);
                cmd.Parameters.AddWithValue("@SubSection_IDs", SubSection_IDs);

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
            return ds;
        }

        public DataSet EmpMaster_SP(string Action = null, string ID = null, string EmpCode = null, string FirstName = null, string LastName = null, string EmailID = null, string JobTitle = null, string BusinessPhone = null, string HomePhone = null, string MobilePhone = null, string FaxNumber = null, string Address = null, string City = null, string State = null, string Postal = null, string Country = null, string Notes = null,
            string Company = null, string FileName = null, string ContentType = null, byte[] Data = null, string PersonalEmailId = null, string ENTEREDBY = null, string IPADDRESS = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("EmpMaster_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@JobTitle", JobTitle);
                cmd.Parameters.AddWithValue("@BusinessPhone", BusinessPhone);
                cmd.Parameters.AddWithValue("@HomePhone", HomePhone);
                cmd.Parameters.AddWithValue("@MobilePhone", MobilePhone);
                cmd.Parameters.AddWithValue("@FaxNumber", FaxNumber);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@State", State);
                cmd.Parameters.AddWithValue("@Postal", Postal);
                cmd.Parameters.AddWithValue("@Country", Country);
                cmd.Parameters.AddWithValue("@Notes", Notes);
                cmd.Parameters.AddWithValue("@Company", Company);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@PersonalEmailId", PersonalEmailId);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);
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
            return ds;
        }

        //11-Oct-2018
        public DataSet EntryStatus(string Action = null, string Project_ID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string MODULENAME = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DM_Incomplete_Entry_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);

                if (SUBJID != "0")
                {
                    cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                }
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);

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
            return ds;
        }



        // Get Data Lock / Un Lock
        public DataSet Get_Lock_Unlock_Data(string Action = null, string InvId = null, string SubjId = null, string VisitNumber = null, string Page = null, string EnteredBy = null, string Project_ID = null, string VISITCOUNT = null, string PVID = null, string TableName = null, string RECID = null, bool MULTIPLEYN = false)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand("Get_Data_Lock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@INVID", InvId);
                cmd.Parameters.AddWithValue("@SUBJID", SubjId);
                cmd.Parameters.AddWithValue("@VISITNUM", VisitNumber);
                cmd.Parameters.AddWithValue("@PAGENUM", Page);
                cmd.Parameters.AddWithValue("@EnteredBy", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@VISITCOUNT", VISITCOUNT);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@TABLENAME", TableName);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MULTIPLEYN", MULTIPLEYN);
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

        //Fill Module And Field Names
        public DataSet Get_Module_Field(String Action, String ModuleName)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand("Get_Act_DeAct_Rules", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ModuleName", ModuleName);
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

        //Get Active and Deactive Proc Name
        public DataSet Get_Act_DeAct_Rules(String Action, string ActiveYN, string ModuleName,
            string FieldName, string ProcName)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand("Get_Act_DeAct_Rules", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Active", ActiveYN);
                cmd.Parameters.AddWithValue("@ModuleName", ModuleName);
                cmd.Parameters.AddWithValue("@FieldName", FieldName);
                cmd.Parameters.AddWithValue("@ProcName", ProcName);
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


        public DataSet CTMS_AGENDA(string Action = null, string ID = null, string Project_ID = null,
            string INVID = null, string FileName = null, string ContentType = null,
            byte[] Data = null, string AgendaTopic = null, string SEQNO = null, string ENTEREDBY = null, string EMPID = null,
            string AgendaDT = null, string AgendaTM = null, string Venue = null, string RescheduleRef = null, string FollowUpRef = null
            , string TopicID = null, string StatusID = null, string AgendaID = null, string ToDate = null, string FromDate = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("CTMS_AGENDA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@AgendaTopic", AgendaTopic);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@EMPID", EMPID);


                cmd.Parameters.AddWithValue("@AgendaDT", AgendaDT);
                cmd.Parameters.AddWithValue("@AgendaTM", AgendaTM);
                cmd.Parameters.AddWithValue("@Venue", Venue);
                cmd.Parameters.AddWithValue("@RescheduleRef", RescheduleRef);
                cmd.Parameters.AddWithValue("@FollowUpRef", FollowUpRef);
                cmd.Parameters.AddWithValue("@TopicID", TopicID);
                cmd.Parameters.AddWithValue("@StatusID", StatusID);
                cmd.Parameters.AddWithValue("@AgendaID", AgendaID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }


        public DataSet Sponsor_SP(string Action = null, string Address = null, string City = null, string Company = null, string ContactNo = null, string Country = null,
            string Department = null, string Designation = null, string EmailID = null, string FirstName = null, string ID = null, string LastName = null, string Mobile = null,
            string Notes = null, string Project_ID = null, string Sponsor_ID = null, string State = null, string Website = null,
            string ZIP = null, string ENTEREDBY = null, string IPADDRESS = null)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("Sponsor_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@Company", Company);
                cmd.Parameters.AddWithValue("@ContactNo", ContactNo);
                cmd.Parameters.AddWithValue("@Country", Country);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Designation", Designation);
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Mobile", Mobile);
                cmd.Parameters.AddWithValue("@Notes", Notes);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Sponsor_ID", Sponsor_ID);
                cmd.Parameters.AddWithValue("@State", State);
                cmd.Parameters.AddWithValue("@Website", Website);
                cmd.Parameters.AddWithValue("@ZIP", ZIP);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);
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



        public DataSet Risk_Mitigation_SP(string Action = null, string ID = null, string Event_ID = null, string Cause_ID = null,
                    string Project_ID = null, string Mitigation = null, string Primary_Type = null, string Primary_RES = null, string Secondary_Type = null,
                    string Secondary_RES = null, string Date = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Risk_Mitigation_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Event_ID", Event_ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Cause_ID", Cause_ID);
                cmd.Parameters.AddWithValue("@Mitigation", Mitigation);
                cmd.Parameters.AddWithValue("@Primary_Type", Primary_Type);
                cmd.Parameters.AddWithValue("@Primary_RES", Primary_RES);
                cmd.Parameters.AddWithValue("@Secondary_Type", Secondary_Type);
                cmd.Parameters.AddWithValue("@Secondary_RES", Secondary_RES);
                cmd.Parameters.AddWithValue("@Date", Date);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }

        public DataSet Risk_Tracker_SP(string Action = null, string Category_ID = null, string SubCategory_ID = null, string Status = null, string Project_ID = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Risk_Tracker_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Category_ID", Category_ID);
                cmd.Parameters.AddWithValue("@SubCategory_ID", SubCategory_ID);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }

        public DataSet Dashboard_SP(string Action = null, string ID = null, string User_ID = null, string Chart_ID = null, string Project_ID = null,
            string X = null, string Y = null, string Width = null, string Height = null, string Section = null, string Type = null,
            string ENTEREDBY = null, string INVID = null, string COUNTRYID = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DASHBOARD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                cmd.Parameters.AddWithValue("@Chart_ID", Chart_ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@X", X);
                cmd.Parameters.AddWithValue("@Y", Y);
                cmd.Parameters.AddWithValue("@Width", Width);
                cmd.Parameters.AddWithValue("@Height", Height);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@ENTEREDBY", User_ID);
                //cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }



        //public DataSet DM_RUN_RULE(string Action = null, string RULE_ID = null, string Nature = null, string SUBJID = null, string Para_Visit_ID = null,
        //    string Para_VariableName = null, string Para_ModuleName = null, string PVID = null, string RECID = null, string Data = null,
        //    string QUERYTEXT = null, string Module_ID = null, string Field_ID = null, string VARIABLENAME = null, string Informational = null,
        //    string TABLE = null, string VISIT = null, string OtherPVIDS = null, string ENTEREDBY = null, string COLUMN = null, string ContID = null,
        //    string Project_ID = null, string FORMULA1 = null, string Para_Indication_ID = null
        //    )
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string USERID = null, TZ_VAL = null, User_Name = null, INVID = null;
        //        if (HttpContext.Current.Session["USER_ID"] != null)
        //        {
        //            USERID = HttpContext.Current.Session["USER_ID"].ToString();
        //        }

        //        if (HttpContext.Current.Session["User_Name"] != null)
        //        {
        //            User_Name = HttpContext.Current.Session["User_Name"].ToString();
        //        }

        //        if (HttpContext.Current.Session["User_SITE"] != null)
        //        {
        //            INVID = HttpContext.Current.Session["User_SITE"].ToString();
        //        }

        //        SqlCommand cmd = new SqlCommand("DM_RUN_RULE", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Action", Action);
        //        cmd.Parameters.AddWithValue("@RULE_ID", RULE_ID);
        //        cmd.Parameters.AddWithValue("@Nature", Nature);
        //        cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
        //        cmd.Parameters.AddWithValue("@Para_Visit_ID", Para_Visit_ID);
        //        cmd.Parameters.AddWithValue("@Para_VariableName", Para_VariableName);
        //        cmd.Parameters.AddWithValue("@Para_ModuleName", Para_ModuleName);

        //        cmd.Parameters.AddWithValue("@PVID", PVID);
        //        cmd.Parameters.AddWithValue("@RECID", RECID);
        //        cmd.Parameters.AddWithValue("@Data", Data);
        //        cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
        //        cmd.Parameters.AddWithValue("@Module_ID", Module_ID);
        //        cmd.Parameters.AddWithValue("@Field_ID", Field_ID);
        //        cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);


        //        cmd.Parameters.AddWithValue("@Informational", Informational);

        //        cmd.Parameters.AddWithValue("@VISIT", VISIT);
        //        cmd.Parameters.AddWithValue("@TABLE", TABLE);

        //        cmd.Parameters.AddWithValue("@OtherPVIDS", OtherPVIDS);

        //        cmd.Parameters.AddWithValue("@ENTEREDBY", HttpContext.Current.Session["USER_ID"].ToString());
        //        cmd.Parameters.AddWithValue("@User_Name", User_Name);
        //        if (HttpContext.Current.Session["TimeZone_Value"] == null)
        //        {
        //            TZ_VAL = "+05:30";
        //        }
        //        else
        //        {
        //            TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
        //        }

        //        cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
        //        cmd.Parameters.AddWithValue("@COLUMN", COLUMN);
        //        cmd.Parameters.AddWithValue("@ContID", ContID);
        //        cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
        //        cmd.Parameters.AddWithValue("@FORMULA1", FORMULA1);
        //        cmd.Parameters.AddWithValue("@Para_Indication_ID", Para_Indication_ID);


        //        con.Open();
        //        adp = new SqlDataAdapter(cmd);
        //        adp.Fill(ds);
        //        cmd.Dispose();
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        con.Close();
        //        throw ex;
        //    }
        //    finally
        //    {
        //    }
        //    return ds;
        //}



        public DataSet Lab_Data_SP(string Action = null, string PROJECTID = null, string ID = null, string Item = null, string TestName = null, string TestID = null, string INVID = null, string LabID = null, string ItemID = null, string DATA = null, string RECID = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Lab_Data_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Item", Item);
                cmd.Parameters.AddWithValue("@TestName", TestName);
                cmd.Parameters.AddWithValue("@TestID", TestID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@LabID", LabID);
                cmd.Parameters.AddWithValue("@ItemID", ItemID);
                cmd.Parameters.AddWithValue("@DATA", DATA);
                cmd.Parameters.AddWithValue("@RECID", RECID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }

        public DataSet LAB_MASTER_SP(string Action = null, string ID = null, string INVID = null, string Lab_ID = null, string Lab_Name = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("LAB_MASTER_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@Lab_ID", Lab_ID);
                cmd.Parameters.AddWithValue("@Lab_Name", Lab_Name);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }

        public DataSet DM_DATA_REQUEST(string Action = null, string InvId = null, string SubjId = null, string VisitNumber = null, string Page = null,
            string EnteredBy = null, string Project_ID = null, string VISITCOUNT = null, string REQUEST = null, string REQ_ID = null,
            string Status = null, string Comments = null, string USERID = null, bool MULTIPLEYN = false, string RECID = "0", string PVID = null, string TABLENAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand("DM_DATA_REQUEST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@INVID", InvId);
                cmd.Parameters.AddWithValue("@SUBJID", SubjId);
                cmd.Parameters.AddWithValue("@VISITNUM", VisitNumber);
                cmd.Parameters.AddWithValue("@PAGENUM", Page);
                cmd.Parameters.AddWithValue("@EnteredBy", EnteredBy);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@VISITCOUNT", VISITCOUNT);
                cmd.Parameters.AddWithValue("@REQUEST", REQUEST);
                cmd.Parameters.AddWithValue("@REQ_ID", REQ_ID);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Comments", Comments);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@MULTIPLEYN", MULTIPLEYN);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);

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


        public DataSet Communication_SP(string Action = null, string ID = null, string MessageUniqueNumber = null, string FromID = null, string ToID = null
            , string CcID = null, string BccID = null, string Subject = null, string Body = null, bool Flag = false, string Importance = null, string DateTimeSent = null,
            string UserID = null, string PROJECTID = null, string Comm_ID = null, string FileName = null, string ContentType = null, byte[] Data = null
            , string ParentComm = null, string Type = null, string Nature = null, string Notes = null, string Department = null, string Reference = null
            , string Event = null, string UNIQUEID = null, bool Success = false, string ORIGINS = null, string Mail_Username = null, string Mail_Password = null, string DBNAME = null, string FOLDERNAME = null, string COND1 = null, string COND2 = null, string COND3 = null, string COND4 = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Communication_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MessageUniqueNumber", MessageUniqueNumber);
                cmd.Parameters.AddWithValue("@FromID ", FromID);
                cmd.Parameters.AddWithValue("@ToID", ToID);
                cmd.Parameters.AddWithValue("@CcID", CcID);
                cmd.Parameters.AddWithValue("@BccID", BccID);
                cmd.Parameters.AddWithValue("@Subject", Subject);
                cmd.Parameters.AddWithValue("@Body", Body);
                cmd.Parameters.AddWithValue("@Flag", Flag);
                cmd.Parameters.AddWithValue("@Importance", Importance);
                cmd.Parameters.AddWithValue("@DateTimeSent", DateTimeSent);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@Comm_ID", Comm_ID);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@ParentComm", ParentComm);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Nature", Nature);
                cmd.Parameters.AddWithValue("@Notes", Notes);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Reference", Reference);
                cmd.Parameters.AddWithValue("@Event", Event);
                cmd.Parameters.AddWithValue("@UNIQUEID", UNIQUEID);
                cmd.Parameters.AddWithValue("@Success", Success);
                cmd.Parameters.AddWithValue("@ORIGINS", ORIGINS);
                cmd.Parameters.AddWithValue("@Mail_Username", Mail_Username);
                cmd.Parameters.AddWithValue("@Mail_Password", Mail_Password);
                //cmd.Parameters.AddWithValue("@DBNAME", MasterDB);
                cmd.Parameters.AddWithValue("@FOLDERNAME", FOLDERNAME);
                cmd.Parameters.AddWithValue("@COND1", COND1);
                cmd.Parameters.AddWithValue("@COND2", COND2);
                cmd.Parameters.AddWithValue("@COND3", COND3);
                cmd.Parameters.AddWithValue("@COND4", COND4);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
            }
            return ds;
        }

        private DateTime nowDate()
        {
            return DateTime.Now;
        }

        public DataSet CTMS_SDV_DATA(string ACTION = null, string PROJECTID = null, string INVID = null, string SUBSITEID = null, string SUBJECTID = null,
      string VISITNUM = null, string VISIT = null, string USERID = null, string MODULENAME = null, string ID = null
          , string SEQNO = null, string INDICATION = null, string MODULEID = null
      )
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("CTMS_SDV_DATA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }






        //Set MANUAL QUERY
        public DataSet SAE_SetManualQuery(String Action, String SAEID = null, int CONTID = 0, String QID = null, String SUBJID = null,
        String QUERYTEXT = null, String MODULENAME = null, String FIELDNAME = null, string TABLENAME = null, string VARIABLENAME = null,
        string ENTEREDBY = null, int RECID = 0, string SAE = null, string PROJECTID = null, string INVID = null, string Version = null
            , string MODULEID = null, string SAE_STATUS = null, string MASTERDB = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_Manual_Query_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@QID", QID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ENTEREDBY", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@Version", Version);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SAE_STATUS", SAE_STATUS);
                //cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);
                cmd.Parameters.AddWithValue("@TIMEZONE", HttpContext.Current.Session["TimeZone_Value"].ToString());

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

        //Get AUDITTRAIL DETAILS
        public DataSet SAE_GetAUDITTRAILDETAILS(string Action, string SAEID = null, int CONTID = 0, string TABLENAME = null, string VARIABLENAME = null,
            int RECID = 0, string MODULENAME = null, string SUBJID = null, string MODULEID = null, string STATUS = null, string MASTERDB = null, string TIMEZONE = null)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_AUDITTRAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                //cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);
                cmd.Parameters.AddWithValue("@TIMEZONE", HttpContext.Current.Session["TimeZone_Value"].ToString());

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

        //Get FIELD COMMENTS DETAILS
        public DataSet SAE_GetFieldComments(String Action = null, string VariableName = null, string ContId = null, string TableName = null,
            string Comments = null, string FieldName = null, string ModuleName = null, string INVID = null, string ENTEREDBY = null,
            string SUBJECTID = null, string SAE = null, string SAEID = null, string STATUS = null,
            string MODULEID = null, string RECID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_COMMENTSDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@CONTID", ContId);
                cmd.Parameters.AddWithValue("@TABLENAME", TableName);
                cmd.Parameters.AddWithValue("@FIELDNAME", FieldName);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VariableName);
                cmd.Parameters.AddWithValue("@COMMENTS", Comments);
                cmd.Parameters.AddWithValue("@ENTEREDBY", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJECTID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@MODULENAME", ModuleName);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
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

        //Get set SDV DETAILS
        public DataSet SAE_GetSetSDVDETAILS(string Action = null, string SAEID = null, int CONTID = 0, string TABLENAME = null,
            string VARIABLENAME = null, bool SDVYN = false, string ENTEREDBY = null, int RECID = 0,
            string MODULEID = null)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand("SAE_SDVDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@SDVYN", SDVYN);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);

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

        public DataSet SAE_GetRecordsQueryDetails(string Action = null, string SAEID = null, int ID = 0, string Comment = null,
        string Reason = null, int RECID = 0,
        string SAE = null, string SUBJID = null, string MODULENAME = null, string MODULEID = null, string STATUS = null, string VARIABLENAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand("SAE_QUERYDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@REASON", Reason);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
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

        public DataSet SAE_GetPAGEINFO(string SAEID = null, string MODULENAME = null, string SAE = null, string SUBJID = null, string MODULEID = null, string STATUS = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("SAE_Get_PAGESTATUS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
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
                ////ds.Dispose();
            }
            return ds; ;
        }



        public DataSet Documents_SP(string Action = null, string ID = null, string ProjectID = null, string DocID = null, string SecID = null,
            string SEQNO = null, string DocName = null, string SectionName = null, string UserID = null, string Data = null, string NewData = null,
            string OldData = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand("Documents_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@DocID", DocID);
                cmd.Parameters.AddWithValue("@SecID", SecID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@SectionName", SectionName);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@NewData", NewData);
                cmd.Parameters.AddWithValue("@OldData", OldData);
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

        public DataSet DM_UPLOAD_SP(string Action = null, string PROJECTID = null, string USERID = null, DataTable TempTable = null, DataTable TempSiteSub = null, string MODULENAME = null
            , string FIELDNAME = null, string SHEET_NAME = null, string SHEET_COLUMN = null, string MODULEID = null
            , string Cur_MODULENAME = null, string Cur_FIELDNAME = null, string Cur_VARIABLENAME = null
            , string Cur_CONTROLTYPE = null, string Cur_ANSWERS = null, string Cur_SEQNO = null, string Cur_LENGTH = null
            , string Cur_Description = null, string IMPORTQUERY = null
            , string TABLENAME = null, string PVID = null, string INSERTQUERY = null, string UPDATEQUERY = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_UPLOAD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@SHEET_NAME", SHEET_NAME);
                cmd.Parameters.AddWithValue("@SHEET_COLUMN", SHEET_COLUMN);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TempTable", TempTable);
                cmd.Parameters.AddWithValue("@TempSiteSub", TempSiteSub);

                cmd.Parameters.AddWithValue("@Cur_MODULENAME", Cur_MODULENAME);
                cmd.Parameters.AddWithValue("@Cur_FIELDNAME", Cur_FIELDNAME);
                cmd.Parameters.AddWithValue("@Cur_VARIABLENAME", Cur_VARIABLENAME);
                cmd.Parameters.AddWithValue("@Cur_CONTROLTYPE", Cur_CONTROLTYPE);
                cmd.Parameters.AddWithValue("@Cur_ANSWERS", Cur_ANSWERS);
                cmd.Parameters.AddWithValue("@Cur_SEQNO", Cur_SEQNO);
                cmd.Parameters.AddWithValue("@Cur_LENGTH", Cur_LENGTH);
                cmd.Parameters.AddWithValue("@Cur_Description", Cur_Description);

                cmd.Parameters.AddWithValue("@IMPORTQUERY", IMPORTQUERY);

                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);

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

        public void GetSetPV(string Action = null, string PVID = null, string PROJECTID = null, string INVID = null, string SUBJID = null, string VISITNUM = null, string PAGENUM = null,
            string PAGESTATUS = null, string ENTEREDBY = null, string SUBJINI = null, string INDICATION = null, string VISITCOUNT = null, bool HasMissing = false)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("DM_PV_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@PAGENUM", PAGENUM);
                cmd.Parameters.AddWithValue("@PAGESTATUS", PAGESTATUS);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SUBJINI", SUBJINI);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@VISITCOUNT", VISITCOUNT);
                cmd.Parameters.AddWithValue("@HasMissing", HasMissing);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
        }



        public DataSet DM_LISTINGS_SP(string Action = null, string ID = null, string LISTING_ID = null, string FIELDNAME = null,
            string FIELDID = null, string Condition1 = null, string Value1 = null, string AndOr1 = null, string Condition2 = null,
            string Value2 = null, string AndOr2 = null, string Condition3 = null, string Value3 = null, string AndOr3 = null,
            string Condition4 = null, string Value4 = null, string AndOr4 = null, string Condition5 = null, string Value5 = null,
            string QUERY = null, string LISTING_NAME = null, bool DM = false, bool CTMS = false, bool MEDICAL = false
            , bool RISK = false, bool SPONSOR = false, bool SITE = false, string MODULEID = null, string SEQNO = null
            , string USERID = null, string PROJECTID = null, string INVID = null, string INDICATION = null, string SUBJECTID = null
            , string FORMULA = null, string MASTERDB = null, string OnClickEvent = null, string OnClickListing = null
            , string Other_Listings = null, string OnClickFilter = null, string PREV_LISTID = null, string STATUS = null
            , bool TRANSPOSE = false, bool Listing_DashBoard = false, string PARENT = null, bool TILES = false, bool Graphs = false,
            bool UNEXP = false, bool DSMB = false, bool IWRS = false, bool MANUAL_CODE = false, string COUNTRYID = null, string TEST = null
            , string TG_LISTING_ID = null, string TG_FIELDNAME = null, string TG_FIELDID = null, string PVID = null, string RECID = null
            , bool PAT_REV = false, bool RISK_INDICATOR = false, bool STUDY_REV = false, bool ITT = false, bool PPP = false, bool Editable = false, bool QueryReport = false, string AutocodeLIB = null, bool Saftey = false, bool CommentReport = false)
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

                cmd = new SqlCommand("DM_LISTINGS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@LISTING_NAME", LISTING_NAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@FORMULA", FORMULA);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@Condition1", Condition1);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@QUERY", QUERY);
                cmd.Parameters.AddWithValue("@DM", DM);
                cmd.Parameters.AddWithValue("@CTMS", CTMS);
                cmd.Parameters.AddWithValue("@MEDICAL", MEDICAL);
                cmd.Parameters.AddWithValue("@RISK", RISK);
                cmd.Parameters.AddWithValue("@SPONSOR", SPONSOR);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);
                cmd.Parameters.AddWithValue("@OnClickEvent", OnClickEvent);
                cmd.Parameters.AddWithValue("@OnClickListing", OnClickListing);
                cmd.Parameters.AddWithValue("@Other_Listings", Other_Listings);
                cmd.Parameters.AddWithValue("@OnClickFilter", OnClickFilter);
                cmd.Parameters.AddWithValue("@PREV_LISTID", PREV_LISTID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@TRANSPOSE", TRANSPOSE);
                cmd.Parameters.AddWithValue("@Listing_DashBoard", Listing_DashBoard);
                cmd.Parameters.AddWithValue("@PARENT", PARENT);
                cmd.Parameters.AddWithValue("@TILES", TILES);
                cmd.Parameters.AddWithValue("@Graphs", Graphs);
                cmd.Parameters.AddWithValue("@UNEXP", UNEXP);
                cmd.Parameters.AddWithValue("@DSMB", DSMB);
                cmd.Parameters.AddWithValue("@IWRS", IWRS);
                cmd.Parameters.AddWithValue("@MANUAL_CODE", MANUAL_CODE);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@TEST", TEST);
                cmd.Parameters.AddWithValue("@TG_LISTING_ID", TG_LISTING_ID);
                cmd.Parameters.AddWithValue("@TG_FIELDID", TG_FIELDID);
                cmd.Parameters.AddWithValue("@TG_FIELDNAME", TG_FIELDNAME);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@PVIDS", PVID);
                cmd.Parameters.AddWithValue("@PAT_REV", PAT_REV);
                cmd.Parameters.AddWithValue("@STUDY_REV", STUDY_REV);
                cmd.Parameters.AddWithValue("@RISK_INDICATOR", RISK_INDICATOR);
                cmd.Parameters.AddWithValue("@ITT", ITT);
                cmd.Parameters.AddWithValue("@PPP", PPP);
                cmd.Parameters.AddWithValue("@Editable", Editable);
                cmd.Parameters.AddWithValue("@QueryReport", QueryReport);
                cmd.Parameters.AddWithValue("@AutocodeLIB", AutocodeLIB);
                cmd.Parameters.AddWithValue("@Saftey", Saftey);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@CommentReport", CommentReport);


                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.CommandTimeout = 0;
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
                ds.Dispose();
            }
            return ds;
        }

        public DataTable getconstring(string ACTION = null, string PROJECTID = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("GETCONN", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return dt;
        }

        public DataTable CREATECHILDDB(string ACTION = null, string CHILDDBNAME = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd;
            SqlDataAdapter adp;
            string query = "";
            try
            {
                cmd = new SqlCommand("CREATECHILDDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@CHILDDBNAME", CHILDDBNAME);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return dt;
        }

        public bool CREATECHILDDBSCRIPT(string filename, string ConnString)
        {
            bool Success = true;
            SqlConnection con1 = new SqlConnection();
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~\SQLSCRIPTS\" + filename);
            FileInfo file = new FileInfo(path);
            string script = file.OpenText().ReadToEnd();
            IEnumerable<string> commandStrings = Regex.Split(script, "^\\s*GO\\s*$", RegexOptions.Multiline);
            if (ConnString != null)
            {
                con1 = new SqlConnection(ConnString);
            }

            con1.Open();
            try
            {
                foreach (string commandString in commandStrings)
                {
                    if (commandString.Trim() != "")
                    {
                        new SqlCommand(commandString, con1).ExecuteNonQuery();
                    }
                }
                Success = true;
            }
            catch (Exception er)
            {
                er.ToString();
                con1.Close();
                Success = false;
            }
            con1.Close();
            return Success;
        }

        public void Defaultscript(string filename, string ConnString)
        {
            SqlConnection con1 = new SqlConnection();
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~\SQLSCRIPTS\" + filename);
            FileInfo file = new FileInfo(path);
            string script = file.OpenText().ReadToEnd();
            IEnumerable<string> commandStrings = Regex.Split(script, "^\\s*GO\\s*$", RegexOptions.Multiline);
            if (ConnString != null)
            {
                con1 = new SqlConnection(ConnString);
            }

            con1.Open();
            try
            {
                foreach (string commandString in commandStrings)
                {
                    if (commandString.Trim() != "")
                    {
                        new SqlCommand(commandString, con1).ExecuteNonQuery();
                    }
                }
            }
            catch (Exception er)
            {
                er.ToString();
                con1.Close();
            }
            con1.Close();
        }

        public DataSet ManageUserGroups(string ACTION = null, string PROJECTID = null, string GroupName = null,
            string ENTEREDBY = null, string ID = null, string UserGroup_Name = null, string countryID = null, string PROJNAME = null,
            string User_ID = null, string Parent = null, string FUNCTIONNAME = null, string FUNCTIONID = null, string UserGroupID = null,
            string StartDate = null, string EndDate = null, string StartTime = null, string EndTime = null, string IPADDRESS = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data
                cmd = new SqlCommand("ManageUserGroups", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@GroupName", GroupName);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@UserGroup_Name", UserGroup_Name);
                cmd.Parameters.AddWithValue("@CNTRYID", countryID);
                cmd.Parameters.AddWithValue("@PROJNAME", PROJNAME);
                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                cmd.Parameters.AddWithValue("@PARENTNAME", Parent);
                cmd.Parameters.AddWithValue("@FUNCTIONNAME", FUNCTIONNAME);
                cmd.Parameters.AddWithValue("@FUNCTIONID", FUNCTIONID);
                cmd.Parameters.AddWithValue("@UserGroupID", UserGroupID);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@StartTime", StartTime);
                cmd.Parameters.AddWithValue("@EndTime", EndTime);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);

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

            }
            return ds;
        }

        public DataSet GetSetEthicsCommityDETAILS(string Action = null, string Project_ID = null, string CNTRYID = null, string INSTID = null,
           string ETHICSNAME = null, string ETHICSQUAL = null, string ETHICSSPEC = null, string ETHICSSPECSPE = null, string MOBNO = null,
           string ADDRESS = null, string TELNO = null, string FAXNO = null, string EMAILID = null, string CCEMAILID = null,
           string DTENTERED = null, string TMENTERED = null, string STATUS = null, string DEACTIVATEDON = null, string ENTEREDBY = null,
           string State = null, string City = null, string FirstName = null, string LastName = null, string ZIP = null, string Department = null, string Designation = null,
           string Notes = null, string ID = null, string STARTDATE = null, string ENDDATE = null, string IPADDRESS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("EthicCommityDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@CNTRYID", CNTRYID);
                cmd.Parameters.AddWithValue("@INSTID", INSTID);

                cmd.Parameters.AddWithValue("@ETHICSNAME", ETHICSNAME);
                cmd.Parameters.AddWithValue("@ADDRESS", ADDRESS);

                cmd.Parameters.AddWithValue("@ETHICSQUAL", ETHICSQUAL);
                cmd.Parameters.AddWithValue("@ETHICSSPEC", ETHICSSPEC);
                cmd.Parameters.AddWithValue("@ETHICSSPECSPE", ETHICSSPECSPE);
                cmd.Parameters.AddWithValue("@MOBNO", MOBNO);
                cmd.Parameters.AddWithValue("@TELNO", TELNO);
                cmd.Parameters.AddWithValue("@FAXNO", FAXNO);
                cmd.Parameters.AddWithValue("@EMAILID", EMAILID);

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@DEACTIVATEDON", DEACTIVATEDON);
                cmd.Parameters.AddWithValue("@State", State);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@ZIP", ZIP);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Designation", Designation);
                cmd.Parameters.AddWithValue("@Notes", Notes);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@STARTDATE", STARTDATE);
                cmd.Parameters.AddWithValue("@ENDDATE", ENDDATE);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public DataSet MM_LISTINGS_SP(string ACTION = null, string COMMENTS = null, string ID = null,
     string LISTING_ID = null, string PROJECT = null, string PVID = null, string QUERYTEXT = null, string QUERYTYPE = null,
     string RECID = null, string REVIEW = null, string PEER_REVIEW = null, string SITE = null, string SOURCE = null, string Status = null,
     string SUBJID = null, string USERID = null, string PRIORITY = null, string DEPT = null, string SUB_TEST = null, string LAB_TEST = null,
     string SUB_TEST_DAT = null, string VISITDAT = null, string CATEGORYID = null, string Grade = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("MM_LISTINGS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@PROJECT", PROJECT);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@REVIEW", REVIEW);
                cmd.Parameters.AddWithValue("@PEER_REVIEW", PEER_REVIEW);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SOURCE", SOURCE);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@PRIORITY", PRIORITY);
                cmd.Parameters.AddWithValue("@DEPT", DEPT);
                cmd.Parameters.AddWithValue("@SUB_TEST", SUB_TEST);
                cmd.Parameters.AddWithValue("@LAB_TEST", LAB_TEST);
                cmd.Parameters.AddWithValue("@SUB_TEST_DAT", SUB_TEST_DAT);
                cmd.Parameters.AddWithValue("@VISITDAT", VISITDAT);
                cmd.Parameters.AddWithValue("@CATEGORYID", CATEGORYID);
                cmd.Parameters.AddWithValue("@Grade", Grade);
                cmd.CommandTimeout = 0;
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
                con.Close();
            }
            return ds;
        }

        public DataSet GET_INVID_SP(string USERID = null, string COUNTRYID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("GET_INVID_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
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
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet GET_COUNTRY_SP(string USERID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("GET_COUNTRY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", USERID);
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
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet CTMS_ENROLLMENT_PLAN(string Action = null, string INVID = null, string ENROLLMENT_STEPS = null,
            string NO_OF_SUBJECTS = null, string MONTHS = null, string ID = null, string TYPE = null, string EnrollmentStartMonth = null,
            string EnrollmentStartYear = null, string COUNTRYID = null, string USER_ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("CTMS_ENROLLMENT_PLAN", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", Action);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@ENROLLMENT_STEPS", ENROLLMENT_STEPS);
                cmd.Parameters.AddWithValue("@NO_OF_SUBJECTS", NO_OF_SUBJECTS);
                cmd.Parameters.AddWithValue("@MONTHS", MONTHS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
                cmd.Parameters.AddWithValue("@EnrollmentStartMonth", EnrollmentStartMonth);
                cmd.Parameters.AddWithValue("@EnrollmentStartYear", EnrollmentStartYear);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@USER_ID", USER_ID);
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
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet RESOURCES_DATA(string Action = null, string CATEGORY = null, string SUBCATE = null,
           string NEXTID = null, string PREVID = null, string ID = null, string EXPERIANCE = null, string RELATIVE_VALUE = null, string CORE = null,
            string LEVEL_SECU = null, string FREQUENCY = null, string PRIMARYPI = null, string SECONDARYPI = null, string CDESH_MODULE = null,
            string FIELDNAME = null, string VARIABLENAME = null, string Risk_Indicator = null, string Discussion_Details = null
            , string Threshold_Basis = null, string Scorecard = null, string RACT_Traceability = null, string Risk_SubCat = null, string Risk_Fact = null
            , string Risk_Impacts = null, string Risk_Type = null, string Risk_Description = null, string Risk_Description_Inv = null, string Risk_Description_C = null,
            string Weighting = null, string Mitigation_Actions = null, string Risk_Cat = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("RESOURCES_DATA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", Action);
                cmd.Parameters.AddWithValue("@CATEGORY", CATEGORY);
                cmd.Parameters.AddWithValue("@SUBCATE", SUBCATE);
                cmd.Parameters.AddWithValue("@NEXTID", NEXTID);
                cmd.Parameters.AddWithValue("@PREVID", PREVID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@EXPERIANCE", EXPERIANCE);
                cmd.Parameters.AddWithValue("@RELATIVE_VALUE", RELATIVE_VALUE);
                cmd.Parameters.AddWithValue("@CORE", CORE);
                cmd.Parameters.AddWithValue("@LEVEL_SECU", LEVEL_SECU);
                cmd.Parameters.AddWithValue("@FREQUENCY", FREQUENCY);
                cmd.Parameters.AddWithValue("@PRIMARYPI", PRIMARYPI);
                cmd.Parameters.AddWithValue("@SECONDARYPI", SECONDARYPI);
                cmd.Parameters.AddWithValue("@CDESH_MODULE", CDESH_MODULE);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);

                cmd.Parameters.AddWithValue("@Risk_Indicator", Risk_Indicator);
                cmd.Parameters.AddWithValue("@Discussion_Details", Discussion_Details);
                cmd.Parameters.AddWithValue("@Threshold_Basis", Threshold_Basis);
                cmd.Parameters.AddWithValue("@Scorecard", Scorecard);
                cmd.Parameters.AddWithValue("@Weighting", Weighting);
                cmd.Parameters.AddWithValue("@Mitigation_Actions", Mitigation_Actions);
                cmd.Parameters.AddWithValue("@Risk_Cat", Risk_Cat);
                cmd.Parameters.AddWithValue("@RACT_Traceability", RACT_Traceability);
                cmd.Parameters.AddWithValue("@Risk_SubCat", Risk_SubCat);
                cmd.Parameters.AddWithValue("@Risk_Fact", Risk_Fact);
                cmd.Parameters.AddWithValue("@Risk_Impacts", Risk_Impacts);
                cmd.Parameters.AddWithValue("@Risk_Type", Risk_Type);
                cmd.Parameters.AddWithValue("@Risk_Description", Risk_Description);
                cmd.Parameters.AddWithValue("@Risk_Description_Inv", Risk_Description_Inv);
                cmd.Parameters.AddWithValue("@Risk_Description_C", Risk_Description_C);

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
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet ePRO_ADD_UPDATE(string ACTION = null, string PROJECTID = null, string INVID = null, string SUBSITEID = null, string SUBJECTID = null,
           string VISITNUM = null, string VISIT = null, string USERID = null, string MODULENAME = null, string ID = null,
               string FIELDNAME = null, string VARIABLENAME = null, string TABLENAME = null, string CONTROLTYPE = null, string CLASS = null,
               string DATATYPE = null, string DEFULTVAL = null, string MAXLEN = null, bool BOLDYN = false, bool UNLNYN = false,
               bool READYN = false, bool MULTILINEYN = false, bool UPPERCASE = false, bool CONTINUE = false, bool ALLVISYN = false, string SEQNO = null, string INDICATION = null, string MODULEID = null,
           string VISITCOUNT = null, bool REQUIREDYN = false, bool INVISIBLE = false, string CHILD_ID = null, string VAL_Child = null, string FieldColor = null, string AnsColor = null,
           bool AUTOCODE = false, bool InList = false, string LabID = null, bool LabData = false, bool AutoNum = false, bool REF = false, bool Prefix = false, string PrefixText = null
           , string Limit = null, bool Male = false, bool Female = false, bool Safety = false, bool DM = false, bool ePRO = false, bool IWRS = false, bool Critic_DP = false,
           string Descrip = null, bool DUPLICATE = false, bool IMPORT_FROM_ANOTHER_MODULE = false, string QUERYTYPE = null, string QUERYSTATUS = null, bool Survey = false,
            string FORMNAME = null, string MASTERDB = null, string INSERTQUERY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("ePRO_ADD_UPDATE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@ID", ID);


                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@CLASS", CLASS);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                cmd.Parameters.AddWithValue("@DEFAULTVAL", DEFULTVAL);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@BOLDYN", BOLDYN);
                cmd.Parameters.AddWithValue("@UNLNYN", UNLNYN);
                cmd.Parameters.AddWithValue("@READYN", READYN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@UPPERCASE", UPPERCASE);
                cmd.Parameters.AddWithValue("@CONTINUE", CONTINUE);
                cmd.Parameters.AddWithValue("@ALLVISYN", ALLVISYN);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VISITCOUNT", VISITCOUNT);
                cmd.Parameters.AddWithValue("@REQUIREDYN", REQUIREDYN);
                cmd.Parameters.AddWithValue("@INVISIBLE", INVISIBLE);
                cmd.Parameters.AddWithValue("@CHILD_ID", CHILD_ID);
                cmd.Parameters.AddWithValue("@VAL_Child", VAL_Child);
                cmd.Parameters.AddWithValue("@FieldColor", FieldColor);
                cmd.Parameters.AddWithValue("@AnsColor", AnsColor);
                cmd.Parameters.AddWithValue("@AUTOCODE", AUTOCODE);
                cmd.Parameters.AddWithValue("@InList", InList);
                cmd.Parameters.AddWithValue("@LabID", LabID);
                cmd.Parameters.AddWithValue("@LabData", LabData);
                cmd.Parameters.AddWithValue("@Refer", REF);
                cmd.Parameters.AddWithValue("@Critic_DP", Critic_DP);
                cmd.Parameters.AddWithValue("@AutoNum", AutoNum);

                cmd.Parameters.AddWithValue("@Prefix", Prefix);
                cmd.Parameters.AddWithValue("@PrefixText", PrefixText);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);

                cmd.Parameters.AddWithValue("@Limit", Limit);
                cmd.Parameters.AddWithValue("@Male", Male);
                cmd.Parameters.AddWithValue("@Female", Female);
                cmd.Parameters.AddWithValue("@Safety", Safety);
                cmd.Parameters.AddWithValue("@DM", DM);
                cmd.Parameters.AddWithValue("@IWRS", IWRS);
                cmd.Parameters.AddWithValue("@ePRO", ePRO);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);
                cmd.Parameters.AddWithValue("@IMPORT_FROM_ANOTHER_MODULE", IMPORT_FROM_ANOTHER_MODULE);
                cmd.Parameters.AddWithValue("@QUERYSTATUS", QUERYSTATUS);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@Survey", Survey);
                cmd.Parameters.AddWithValue("@FORMNAME", FORMNAME);
                //cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }



        public DataSet SURVEY_SP(string ACTION = null, string ID = null, string Name = null,
            string Organisation = null, string Designation = null, string EmailID = null,
            string ContactNo = null, string MODULEID = null, string INSERTQUERY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SURVEY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Organisation", Organisation);
                cmd.Parameters.AddWithValue("@Designation", Designation);
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@ContactNo", ContactNo);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
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
                ////ds.Dispose();
            }
            return ds;
        }


        public DataSet EMAIL_SP(string ACTION = null, string ID = null, string EMAILIDS = null, string CCEMAILIDS = null
            , string BCCEMAILIDS = null, string SUBJECT = null, string BODY = null, bool SENT = false, string ERR = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("EMAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@EMAILIDS", EMAILIDS);
                cmd.Parameters.AddWithValue("@CCEMAILIDS", CCEMAILIDS);
                cmd.Parameters.AddWithValue("@BCCEMAILIDS", BCCEMAILIDS);
                cmd.Parameters.AddWithValue("@SUBJECT", SUBJECT);
                cmd.Parameters.AddWithValue("@BODY", BODY);
                cmd.Parameters.AddWithValue("@SENT", SENT);
                cmd.Parameters.AddWithValue("@ERR", ERR);
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
                ////ds.Dispose();
            }
            return ds;
        }


        public DataSet ESOURCE_CONFIG_SP(string ACTION = null, string DELETE = null, string ENTEREDBY = null, string ENTEREDDAT = null,
            string ID = null, string MODULEID = null, string SECTION = null, string SEQNO = null, string SUBJID = null, string SUBJID_DATA = null,
            string UPDATEDBY = null, string UPDATEDDAT = null, string VISIT_ID = null, string VISIT = null, bool NEW = false, bool OLD = false
            , string RELSUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("ESOURCE_CONFIG_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@DELETE", DELETE);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SECTION", SECTION);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SUBJID_DATA", SUBJID_DATA);
                cmd.Parameters.AddWithValue("@VISIT_ID", VISIT_ID);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@NEW", NEW);
                cmd.Parameters.AddWithValue("@OLD", OLD);
                cmd.Parameters.AddWithValue("@RELSUBJID", RELSUBJID);
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
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet ESOURCE_DATA_SP(string ACTION = null, string SUBJID = null, string MODULEID = null, string ID = null,
           string TABLENAME = null, string INSERTQUERY = null, string UPDATEQUERY = null, string MOBILENO = null, string FIELDNAME = null,
           string PVID = null, string RECID = null, string VISITNUM = null, string SITEID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("eSOURCE_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DASHBOARD_ASSIGNING(string Action = null, string USERGROUPID = null, string USERID = null, string PROJECTID = null,
           string TYPE = null, string TYPENAME = null, string TYPEID = null, string ENTEREDBY = null, string ID = null, string FUNCTIONID = null,
            string TABSCOLOR = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DASHBOARD_ASSIGNING", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@USERGROUPID", USERGROUPID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
                cmd.Parameters.AddWithValue("@TYPENAME", TYPENAME);
                cmd.Parameters.AddWithValue("@TYPEID", TYPEID);
                cmd.Parameters.AddWithValue("@FUNCTIONID", FUNCTIONID);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TABSCOLOR", TABSCOLOR);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet DASHBOARD_MASTER(string Action = null, string PROJECTID = null, string INVID = null, string COUNTRYID = null,
           string USER_ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DASHBOARD_MASTER", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@USER_ID", USER_ID);
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet ePRO_DATA_SP(string ACTION = null, string SUBJID = null, string MODULEID = null, string ID = null,
            string TABLENAME = null, string INSERTQUERY = null, string UPDATEQUERY = null, string MOBILENO = null, string FIELDNAME = null,
            string PVID = null, string RECID = null, string VISITNUM = null, string SITEID = null, string IC1 = null, string IC2 = null, string IC3 = null, string IC4 = null,
                string IC5 = null, string IC6 = null, string IC7 = null, string IC8 = null, string IC9 = null, string IC10 = null, string IC11 = null, string IC12 = null, string IC13 = null,
            string IC14 = null, string IC15 = null, string IC16 = null, string IC17 = null, string IC18 = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("ePRO_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@IC1", IC1);
                cmd.Parameters.AddWithValue("@IC2", IC2);
                cmd.Parameters.AddWithValue("@IC3", IC3);
                cmd.Parameters.AddWithValue("@IC4", IC4);
                cmd.Parameters.AddWithValue("@IC5", IC5);
                cmd.Parameters.AddWithValue("@IC6", IC6);
                cmd.Parameters.AddWithValue("@IC7", IC7);
                cmd.Parameters.AddWithValue("@IC8", IC8);
                cmd.Parameters.AddWithValue("@IC9", IC9);
                cmd.Parameters.AddWithValue("@IC10", IC10);
                cmd.Parameters.AddWithValue("@IC11", IC11);
                cmd.Parameters.AddWithValue("@IC12", IC12);
                cmd.Parameters.AddWithValue("@IC13", IC13);
                cmd.Parameters.AddWithValue("@IC14", IC14);
                cmd.Parameters.AddWithValue("@IC15", IC15);
                cmd.Parameters.AddWithValue("@IC16", IC16);
                cmd.Parameters.AddWithValue("@IC17", IC17);
                cmd.Parameters.AddWithValue("@IC18", IC18);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }


        public DataSet ePRO_WAD_SP(string ACTION = null, string ID = null, string SUBJID = null, string MODULEID = null,
            string TABLENAME = null, string SITEID = null, string USERID = null, string MOBILENO = null, string DM_TABLENAME = null,
            string ePRO_DATE_Filter = null, string LANG = null, string IC1 = null, string IC2 = null, string IC3 = null, string IC4 = null,
                string IC5 = null, string IC6 = null, string IC7 = null, string IC8 = null, string IC9 = null, string IC10 = null, string IC11 = null, string IC12 = null, string IC13 = null,
            string IC14 = null, string IC15 = null, string IC16 = null, string IC17 = null, string IC18 = null, string LASTDOSE = null,
            string TERM = null, string PVID = null, string VARIABLENAME = null, string RECID = null, string TIMEZONE = null, string MODULENAME = null,
            string FIELDNAME = null, string OLDVALUE = null, string NEWVALUE = null, string REASON = null, string COMMENTS = null, string INDICATION = null, string VISITNUM = null, string LabID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("ePRO_WAD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                cmd.Parameters.AddWithValue("@DM_TABLENAME", DM_TABLENAME);
                cmd.Parameters.AddWithValue("@ePRO_DATE_Filter", ePRO_DATE_Filter);
                cmd.Parameters.AddWithValue("@LANG", LANG);
                cmd.Parameters.AddWithValue("@IC1", IC1);
                cmd.Parameters.AddWithValue("@IC2", IC2);
                cmd.Parameters.AddWithValue("@IC3", IC3);
                cmd.Parameters.AddWithValue("@IC4", IC4);
                cmd.Parameters.AddWithValue("@IC5", IC5);
                cmd.Parameters.AddWithValue("@IC6", IC6);
                cmd.Parameters.AddWithValue("@IC7", IC7);
                cmd.Parameters.AddWithValue("@IC8", IC8);
                cmd.Parameters.AddWithValue("@IC9", IC9);
                cmd.Parameters.AddWithValue("@IC10", IC10);
                cmd.Parameters.AddWithValue("@IC11", IC11);
                cmd.Parameters.AddWithValue("@IC12", IC12);
                cmd.Parameters.AddWithValue("@IC13", IC13);
                cmd.Parameters.AddWithValue("@IC14", IC14);
                cmd.Parameters.AddWithValue("@IC15", IC15);
                cmd.Parameters.AddWithValue("@IC16", IC16);
                cmd.Parameters.AddWithValue("@IC17", IC17);
                cmd.Parameters.AddWithValue("@IC18", IC18);
                cmd.Parameters.AddWithValue("@LASTDOSE", LASTDOSE);
                cmd.Parameters.AddWithValue("@TERM", TERM);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@OLDVALUE", OLDVALUE);
                cmd.Parameters.AddWithValue("@NEWVALUE", NEWVALUE);
                cmd.Parameters.AddWithValue("@REASON", REASON);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }


        public DataSet SMS_SENDING(string ACTION = null, string PURPOSE = null, string MOBILENO = null, string MESSAGE = null,
            string ERROR = null, string SUBJID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SMS_SENDING", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.Parameters.AddWithValue("@MESSAGE", MESSAGE);
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                cmd.Parameters.AddWithValue("@PURPOSE", PURPOSE);
                cmd.Parameters.AddWithValue("@ERROR", ERROR);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
            return ds;
        }

        public DataSet SAE_MEDICAL_REVIEW_SP(string Action = null, string SAEID = null, int CONTID = 0, string TABLENAME = null,
            string VARIABLENAME = null, bool MRYN = false, string ENTEREDBY = null, int RECID = 0,
            string MODULEID = null, string STATUS = null)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                //Get Data for Update
                cmd = new SqlCommand("SAE_MEDICAL_REVIEW_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@MRYN", MRYN);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);

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


        public DataSet eTMF_SP(string ACTION = null, string ID = null, string CountryID = null, string SiteID = null, string DocID = null,
           string DocName = null, string RefNo = null, string UniqueRefNo = null, string AutoNomenclature = null, string UploadFileName = null,
           string VersionID = null, string SysFileName = null, string Size = null, string FileType = null, string Status = null, string SubStatus = null, string UploadBy = null,
           string UploadDateTime = null, string ExpiryDate = null, string UploadTaskId = null, string UploadSubTaskId = null, string UploadSectionId = null,
           string UploadArtifactId = null, string DeadlineDate = null, string SEQNO = null, string UploadDocTypeid = null, string UploadZoneId = null, string DOCTYPEID = null,
           string FOLDERID = null, string SUBFOLDERID = null, string ARTIFACTS = null, string UploadDepartmentId = null, string DOC_VERSIONNO = null,
           string DOC_DATETIME = null, string NOTE = null, string Functions = null, string INDIVIDUAL = null, string UploadFileName_Editable = null, string Size_Editable = null,
           string FileType_Editable = null, string ReplaceFile = null, string REFRENCE_DOC = null, string USERID = null, string SUBJID = null,
           bool SendEmail = false, string ToEmailIDs = null, string CCEmailIDs = null, string SPEC = null, string DOC_REMINDERDAT = null,
           string QCDoc_Locat = null, string QCDoc_Legible = null, string QCDOC_CORRECT_NOM = null, string QCDOC_CORRECT_ORIEN = null,
           string QCDOC_DOC_ATTRI = null, string QC_ACTION = null, string SubFolder_ID = null, string Folder_ID = null, string Project_ID = null, string DocType = null)
        {
            DataSet ds = new DataSet();
            try
            {
                string TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                SqlCommand cmd = new SqlCommand("eTMF_SP", con);
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
                cmd.Parameters.AddWithValue("@SubStatus", SubStatus);
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
                cmd.Parameters.AddWithValue("@INDIVIDUAL", INDIVIDUAL);
                cmd.Parameters.AddWithValue("@UploadFileName_Editable", UploadFileName_Editable);
                cmd.Parameters.AddWithValue("@Size_Editable", Size_Editable);
                cmd.Parameters.AddWithValue("@FileType_Editable", FileType_Editable);
                cmd.Parameters.AddWithValue("@ReplaceFile", ReplaceFile);
                cmd.Parameters.AddWithValue("@REFRENCE_DOC", REFRENCE_DOC);
                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@ToEmailIDs", ToEmailIDs);
                cmd.Parameters.AddWithValue("@CCEmailIDs", CCEmailIDs);
                cmd.Parameters.AddWithValue("@SendEmail", SendEmail);
                cmd.Parameters.AddWithValue("@SPEC", SPEC);
                cmd.Parameters.AddWithValue("@DOC_REMINDERDAT", DOC_REMINDERDAT);
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@QCDoc_Locat", QCDoc_Locat);
                cmd.Parameters.AddWithValue("@QCDoc_Legible", QCDoc_Legible);
                cmd.Parameters.AddWithValue("@QCDOC_CORRECT_NOM", QCDOC_CORRECT_NOM);
                cmd.Parameters.AddWithValue("@QCDOC_CORRECT_ORIEN", QCDOC_CORRECT_ORIEN);
                cmd.Parameters.AddWithValue("@QCDOC_DOC_ATTRI", QCDOC_DOC_ATTRI);
                cmd.Parameters.AddWithValue("@QC_ACTION", QC_ACTION);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@SubFolder_ID", SubFolder_ID);
                cmd.Parameters.AddWithValue("@Folder_ID", Folder_ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@DocType", DocType);

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


        public DataSet eTMF_QC_SP(string ACTION = null, string ID = null, string CountryID = null, string SiteID = null, string DocID = null,
           string DocName = null, string RefNo = null, string UniqueRefNo = null, string AutoNomenclature = null, string UploadFileName = null,
           string VersionID = null, string SysFileName = null, string Size = null, string FileType = null, string Status = null, string SubStatus = null, string UploadBy = null,
           string UploadDateTime = null, string ExpiryDate = null, string UploadTaskId = null, string UploadSubTaskId = null, string UploadSectionId = null,
           string UploadArtifactId = null, string DeadlineDate = null, string SEQNO = null, string UploadDocTypeid = null, string UploadZoneId = null, string DOCTYPEID = null,
           string FOLDERID = null, string SUBFOLDERID = null, string ARTIFACTS = null, string UploadDepartmentId = null, string DOC_VERSIONNO = null,
           string DOC_DATETIME = null, string NOTE = null, string Functions = null, string INDIVIDUAL = null, string UploadFileName_Editable = null, string Size_Editable = null,
           string FileType_Editable = null, string ReplaceFile = null, string REFRENCE_DOC = null, string USERID = null, string SUBJID = null,
           bool SendEmail = false, string ToEmailIDs = null, string CCEmailIDs = null, string SPEC = null, string DOC_REMINDERDAT = null,
           string QCDoc_Locat = null, string QCDoc_Legible = null, string QCDOC_CORRECT_NOM = null, string QCDOC_CORRECT_ORIEN = null,
           string QCDOC_DOC_ATTRI = null, string QC_ACTION = null, string QCDOC_COMPLETE = null)
        {
            DataSet ds = new DataSet();
            try
            {
                string TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                SqlCommand cmd = new SqlCommand("eTMF_QC_SP", con);
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
                cmd.Parameters.AddWithValue("@SubStatus", SubStatus);
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
                cmd.Parameters.AddWithValue("@INDIVIDUAL", INDIVIDUAL);
                cmd.Parameters.AddWithValue("@UploadFileName_Editable", UploadFileName_Editable);
                cmd.Parameters.AddWithValue("@Size_Editable", Size_Editable);
                cmd.Parameters.AddWithValue("@FileType_Editable", FileType_Editable);
                cmd.Parameters.AddWithValue("@ReplaceFile", ReplaceFile);
                cmd.Parameters.AddWithValue("@REFRENCE_DOC", REFRENCE_DOC);
                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@ToEmailIDs", ToEmailIDs);
                cmd.Parameters.AddWithValue("@CCEmailIDs", CCEmailIDs);
                cmd.Parameters.AddWithValue("@SendEmail", SendEmail);
                cmd.Parameters.AddWithValue("@SPEC", SPEC);
                cmd.Parameters.AddWithValue("@DOC_REMINDERDAT", DOC_REMINDERDAT);
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@QCDoc_Locat", QCDoc_Locat);
                cmd.Parameters.AddWithValue("@QCDoc_Legible", QCDoc_Legible);
                cmd.Parameters.AddWithValue("@QCDOC_CORRECT_NOM", QCDOC_CORRECT_NOM);
                cmd.Parameters.AddWithValue("@QCDOC_CORRECT_ORIEN", QCDOC_CORRECT_ORIEN);
                cmd.Parameters.AddWithValue("@QCDOC_DOC_ATTRI", QCDOC_DOC_ATTRI);
                cmd.Parameters.AddWithValue("@QC_ACTION", QC_ACTION);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@QCDOC_COMPLETE", QCDOC_COMPLETE);
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

        public DataSet DM_LAB_SP(string ACTION = null, string AGEHI = null, string AGELO = null, string EFFDATEFROM = null, string EFFDATETO = null, string ID = null,
string LABID = null, string LABNAME = null, string LBORNRHI = null, string LBORNRLO = null, string LBORRESU = null, string LBTEST = null, string SEX = null,
    string SITEID = null, string SUBJID = null, string PVID = null, string RECID = null, string VISITID = null, string MODULEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_LAB_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@AGEHI", AGEHI);
                cmd.Parameters.AddWithValue("@AGELO", AGELO);
                cmd.Parameters.AddWithValue("@EFFDATEFROM", EFFDATEFROM);
                cmd.Parameters.AddWithValue("@EFFDATETO", EFFDATETO);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@LABID", LABID);
                cmd.Parameters.AddWithValue("@LABNAME", LABNAME);
                cmd.Parameters.AddWithValue("@LBORNRHI", LBORNRHI);
                cmd.Parameters.AddWithValue("@LBORNRLO", LBORNRLO);
                cmd.Parameters.AddWithValue("@LBORRESU", LBORRESU);
                cmd.Parameters.AddWithValue("@LBTEST", LBTEST);
                cmd.Parameters.AddWithValue("@SEX", SEX);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
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
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet SHOW_VIEW_SP(string VIEWNAME = null, string SUBJID = null, string SITEID = null, string USERID = null, string TYPE = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SHOW_VIEW_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VIEWNAME", VIEWNAME);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@TYPE", TYPE);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet eTMF_Snapshot_SP(string ACTION = null, string ID = null, string Snapshot = null, string SnapId = null,
            string ArtifactId = null, string DocId = null, string ZoneID = null, string SectionId = null,
            bool Site = false, bool eTMF = false, bool Sponsor = false, string RefNo = null, string UniqueRefNo = null, string FileId = null,
            string DocTypeId = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("eTMF_Snapshot_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Snapshot", Snapshot);
                cmd.Parameters.AddWithValue("@SnapId", SnapId);
                cmd.Parameters.AddWithValue("@ArtifactId", ArtifactId);
                cmd.Parameters.AddWithValue("@DocId", DocId);
                cmd.Parameters.AddWithValue("@ZoneID", ZoneID);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@Site", Site);
                cmd.Parameters.AddWithValue("@eTMF", eTMF);
                cmd.Parameters.AddWithValue("@Sponsor", Sponsor);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                cmd.Parameters.AddWithValue("@UniqueRefNo", UniqueRefNo);
                //cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@FileId", FileId);
                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@DocTypeId", DocTypeId);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }




        public DataSet SAE_REPORT(string ACTION = null, string SAEID = null, string SUBJID = null, string STATUS = null, string REPORTSTATUS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_REPORT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@REPORTSTATUS", REPORTSTATUS);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet SAE_UpdateData(string ACTION = null, string ContId = null, string ModuleName = null, string FieldName = null,
            string TableName = null, string VariableName = null, string OldValue = null, string NewValue = null, string Reason = null,
            string Comments = null, string Query = null, string ControlType = null, string SAEID = null,
            string RECID = null, string SUBJID = null, string MODULEID = null, string STATUS = null, string CHANGEBY = null, string Source = null, string AE_PVID = null, string AE_RECID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();

            try
            {
                if (OldValue == "--Select--")
                {
                    OldValue = "";
                }

                if (NewValue == "--Select--")
                {
                    NewValue = "";
                }

                cmd = new SqlCommand("SAE_CHANGE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@CONTID", Convert.ToInt32(ContId));
                cmd.Parameters.AddWithValue("@RECID", Convert.ToInt32(RECID));
                cmd.Parameters.AddWithValue("@MODULENAME", ModuleName);
                cmd.Parameters.AddWithValue("@FIELDNAME", FieldName);
                cmd.Parameters.AddWithValue("@TABLENAME", TableName);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VariableName);
                cmd.Parameters.AddWithValue("@OLDVALUE", OldValue);
                cmd.Parameters.AddWithValue("@NEWVALUE", NewValue);
                cmd.Parameters.AddWithValue("@REASON", Reason);
                cmd.Parameters.AddWithValue("@COMMENTS", Comments);
                cmd.Parameters.AddWithValue("@CHANGEBY", CHANGEBY);
                cmd.Parameters.AddWithValue("@ControlType", ControlType);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@TIMEZONE", HttpContext.Current.Session["TimeZone_Value"].ToString());
                cmd.Parameters.AddWithValue("@AE_PVID", AE_PVID);
                cmd.Parameters.AddWithValue("@AE_RECID", AE_RECID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
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

                SqlCommand cmd = new SqlCommand("eTMF_View_SP", con);
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

        public DataSet eTMF_Process_SP(string ACTION = null, string ID = null, string CountryID = null, string SiteID = null, string DocID = null,
            string DocName = null, string RefNo = null, string UniqueRefNo = null, string AutoNomenclature = null, string UploadFileName = null,
            string VersionID = null, string SysFileName = null, string Size = null, string FileType = null, string Status = null, string UploadBy = null,
            string UploadDateTime = null, string ExpiryDate = null, string UploadTaskId = null, string UploadSubTaskId = null, string UploadSectionId = null,
            string UploadArtifactId = null, string DeadlineDate = null, string SEQNO = null, string UploadDocTypeid = null, string UploadZoneId = null, string DOCTYPEID = null,
            string FOLDERID = null, string SUBFOLDERID = null, string ARTIFACTS = null, string UploadDepartmentId = null, string DOC_VERSIONNO = null,
            string DOC_DATETIME = null, string NOTE = null, string Functions = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("eTMF_Process_SP", con);
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
                cmd.Parameters.AddWithValue("@USERID", Get_User_Id());

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

        public DataSet USER_REPORT_ACTIVITY_LOG_SP(string Action = null, string ID = null, string User_ID = null, string Project = null,
            string Page_Name = null, string Function_Name = null, string Section = null, string DateFrom = null, string TimeFrom = null,
            string DateTo = null, string TimeTo = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("USER_REPORT_ACTIVITY_LOG_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                cmd.Parameters.AddWithValue("@Project", Project);
                cmd.Parameters.AddWithValue("@Page_Name", Page_Name);
                cmd.Parameters.AddWithValue("@Function_Name", Function_Name);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@TimeFrom", TimeFrom);
                cmd.Parameters.AddWithValue("@DateTo", DateTo);
                cmd.Parameters.AddWithValue("@TimeTo", TimeTo);
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
                ////ds.Dispose();
            }
            return ds;
        }

        public DataSet ANALYT_SUBJECTS(string ACTION = null, string SUBJID = null, string MODULEID = null, string USERID = null, string LISTINGID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("ANALYT_SUBJECTS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@LISTINGID", LISTINGID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet CTMS_DATA_SP(string ACTION = null, string ID = null, string INVID = null, string SEQNO = null, string SUBJID = null,
            string SITEID = null, string VISITID = null, string VISITNAME = null, string VISIT_INITIAL = null, string VISIT_NOM = null,
            string STARTDAT = null, string ENDDAT = null, string EMPID = null, string USERID = null, string MODULEID = null, string SVID = null,
            string RECID = null, string TABLENAME = null, string INSERTQUERY = null, string UPDATEQUERY = null, bool IsMissing = false, string VARIABLENAME = null,
            string EMAIL_IDS = null, string CCEMAIL_IDS = null, string BCCEMAIL_IDS = null, string Unblinded = null, string Comment = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("CTMS_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@VISITNAME", VISITNAME);
                cmd.Parameters.AddWithValue("@VISIT_INITIAL", VISIT_INITIAL);
                cmd.Parameters.AddWithValue("@VISIT_NOM", VISIT_NOM);
                cmd.Parameters.AddWithValue("@STARTDAT", STARTDAT);
                cmd.Parameters.AddWithValue("@ENDDAT", ENDDAT);
                cmd.Parameters.AddWithValue("@EMPID", EMPID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SVID", SVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@EMAIL_IDS", EMAIL_IDS);
                cmd.Parameters.AddWithValue("@CCEMAIL_IDS", CCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@BCCEMAIL_IDS", BCCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@Unblinded", Unblinded);
                cmd.Parameters.AddWithValue("@Unblind", HttpContext.Current.Session["Unblind"].ToString());
                cmd.Parameters.AddWithValue("@Comment", Comment);

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

        //Method for update data in table and audit trial    
        public DataSet CTMS_UpdateDataForAudit(string Action = null, string SVID = null, string ContId = null, string RECID = null,
            string ModuleName = null, string FieldName = null, string TableName = null, string VariableName = null, string OldValue = null,
            string NewValue = null, string Reason = null, string Comments = null, string Query = null, string ControlType = null,
            string ModuleID = null, string Source = null, string CHANGEBY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("CTMS_CHANGE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SVID", SVID);
                cmd.Parameters.AddWithValue("@CONTID", Convert.ToInt32(ContId));
                cmd.Parameters.AddWithValue("@RECID", Convert.ToInt32(RECID));
                cmd.Parameters.AddWithValue("@MODULENAME", ModuleName);
                cmd.Parameters.AddWithValue("@FIELDNAME", FieldName);
                cmd.Parameters.AddWithValue("@TABLENAME", TableName);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VariableName);
                cmd.Parameters.AddWithValue("@OLDVALUE", OldValue);
                cmd.Parameters.AddWithValue("@NEWVALUE", NewValue);
                cmd.Parameters.AddWithValue("@REASON", Reason);
                cmd.Parameters.AddWithValue("@COMMENTS", Comments);
                cmd.Parameters.AddWithValue("@CHANGEBY", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@ControlType", ControlType);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
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
                ////ds.Dispose();
            }
            return ds;
        }

        //Get FIELD COMMENTS DETAILS
        public DataSet CTMS_GetFieldComments(String Action, String SVID = null, int CONTID = 0, string TABLENAME = null, string FIELDNAME = null,
            string VARIABLENAME = null, string COMMENTS = null, string ENTEREDBY = null, int RECID = 0, string TIMEZONE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("CTMS_COMMENTSDETAILS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SVID", SVID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@ENTEREDBY", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);

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


        //Get AUDITTRAIL DETAILS
        public DataSet CTMS_GetAUDITTRAILDETAILS(string Action, string SVID = null, int CONTID = 0, string TABLENAME = null, string VARIABLENAME = null,
            int RECID = 0, string TIMEZONE = null)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("CTMS_AUDITTRAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SVID", SVID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);

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

        //GET SET SV MODULE
        public void CTMS_SV_MODULE_SP(string Action = null, string SVID = null, string SITEID = null, string SUBJID = null, string VISITID = null,
            string MODULEID = null, string IsComplete = null, string ENTEREDBY = null)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("CTMS_SV_MODULE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SVID", SVID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@IsComplete", IsComplete);
                cmd.Parameters.AddWithValue("@ENTEREDBY", HttpContext.Current.Session["User_Id"].ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
        }

        //GET SET SV
        public void CTMS_GetSetPV(string Action = null, string SVID = null, string SITEID = null, string SUBJID = null, string VISITID = null,
            string MODULEID = null, string ENTEREDBY = null, string STATUS = null, string SUB_STATUS = null)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("CTMS_SV_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@SVID", SVID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@SUB_STATUS", SUB_STATUS);
                cmd.Parameters.AddWithValue("@ENTEREDBY", HttpContext.Current.Session["User_Id"].ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
            }
        }

        public DataSet CTMS_REPORTS(string ACTION = null, string SVID = null, string INVID = null,
            string VISITID = null, string VISITNAME = null, string FOLLOWUP = null, string USER = null, string CL = null, string COL = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("CTMS_REPORTS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SVID", SVID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@VISITNAME", VISITNAME);
                cmd.Parameters.AddWithValue("@FOLLOWUP", FOLLOWUP);
                cmd.Parameters.AddWithValue("@USER", USER);
                cmd.Parameters.AddWithValue("@CL", CL);
                cmd.Parameters.AddWithValue("@COL", COL);

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

        public DataSet SAE_ADD_UPDATE_NEW(string ACTION = null, string PROJECTID = null, string INVID = null, string SUBSITEID = null, string SUBJECTID = null,
    string VISITNUM = null, string VISIT = null, string USERID = null, string MODULENAME = null, string ID = null,
        string FIELDNAME = null, string VARIABLENAME = null, string TABLENAME = null, string CONTROLTYPE = null, string CLASS = null,
        string DATATYPE = null, string DEFULTVAL = null, string MAXLEN = null, bool BOLDYN = false, bool UNLNYN = false,
        bool READYN = false, bool MULTILINEYN = false, bool UPPERCASE = false, bool CONTINUE = false, bool ALLVISYN = false,
        string SEQNO = null, string INDICATION = null, string MODULEID = null, string VISITCOUNT = null, bool REQUIREDYN = false, bool INVISIBLE = false,
        string CHILD_ID = null, string VAL_Child = null, string FieldColor = null, string AnsColor = null, bool AUTOCODE = false, bool InList = false,
       string LabID = null, bool LabData = false, bool AutoNum = false, bool REF = false, bool Prefix = false, string PrefixText = null
    , string Limit = null, bool Male = false, bool Female = false, bool Safety = false, bool DM = false, string SAEID = null,
     int ContID = 0, string DATA = null, string ENTEREDBY = null, string SAE = null, string VERSION = null, string RECID = null, int CONTYN = 0,
     bool SDVYN = false, string OLDSAEID = null, string PAGESTATUS = null, string INSERTQUERY = null, string UPDATEQUERY = null, string STATUS = null,
     string SELECTQUERY = null, string REASON = null, string SIGNATURE = null, string STATUS_ACTIVITY = null, string NEWSTATUS = null, string PURPOSE = null,
     string EMAILID = null, string CC_EMAILID = null, string BCC_EMAILID = null, string NAME = null, string USERTYPE = null, string MASTERDB = null, string MAIL_SUBJECT = null, string MAIL_BODY = null,
      bool Standard = false, bool NonRepetative = false, bool Mandatory = false, bool HelpData = false, bool Critic_DP = false,
      string Descrip = null, bool DUPLICATE = false, string AE_PVID = null, string AE_RECID = null, string TEMPID = null, string LISTING_ID = null,
      string COUNTRYID = null, bool IsComplete = false, bool IsMissing = false, bool MEDICAL_REVIEW = false,
      string UploadFileName = null, string Size = null, string FileType = null, string DocName = null, string SysFileName = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_ADD_UPDATE_NEW", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@ID", ID);


                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@CLASS", CLASS);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                cmd.Parameters.AddWithValue("@DEFAULTVAL", DEFULTVAL);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@BOLDYN", BOLDYN);
                cmd.Parameters.AddWithValue("@UNLNYN", UNLNYN);
                cmd.Parameters.AddWithValue("@READYN", READYN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@UPPERCASE", UPPERCASE);
                cmd.Parameters.AddWithValue("@CONTINUE", CONTINUE);
                cmd.Parameters.AddWithValue("@ALLVISYN", ALLVISYN);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);

                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["User_ID"].ToString());
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VISITCOUNT", VISITCOUNT);
                cmd.Parameters.AddWithValue("@REQUIREDYN", REQUIREDYN);
                cmd.Parameters.AddWithValue("@INVISIBLE", INVISIBLE);
                cmd.Parameters.AddWithValue("@CHILD_ID", CHILD_ID);
                cmd.Parameters.AddWithValue("@VAL_Child", VAL_Child);
                cmd.Parameters.AddWithValue("@FieldColor", FieldColor);
                cmd.Parameters.AddWithValue("@AnsColor", AnsColor);
                cmd.Parameters.AddWithValue("@AUTOCODE", AUTOCODE);
                cmd.Parameters.AddWithValue("@InList", InList);
                cmd.Parameters.AddWithValue("@LabID", LabID);
                cmd.Parameters.AddWithValue("@LabData", LabData);
                cmd.Parameters.AddWithValue("@Refer", REF);
                cmd.Parameters.AddWithValue("@AutoNum", AutoNum);

                cmd.Parameters.AddWithValue("@Prefix", Prefix);
                cmd.Parameters.AddWithValue("@PrefixText", PrefixText);

                cmd.Parameters.AddWithValue("@Limit", Limit);
                cmd.Parameters.AddWithValue("@Male", Male);
                cmd.Parameters.AddWithValue("@Female", Female);
                cmd.Parameters.AddWithValue("@Safety", Safety);
                cmd.Parameters.AddWithValue("@DM", DM);

                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@ContID", ContID);
                cmd.Parameters.AddWithValue("@DATA", DATA);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@VERSION", VERSION);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@CONTYN", CONTYN);
                cmd.Parameters.AddWithValue("@SDVYN", SDVYN);
                cmd.Parameters.AddWithValue("@OLDSAEID", OLDSAEID);
                cmd.Parameters.AddWithValue("@PAGESTATUS", PAGESTATUS);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@SELECTQUERY", SELECTQUERY);
                cmd.Parameters.AddWithValue("@REASON", REASON);
                cmd.Parameters.AddWithValue("@SIGNATURE", SIGNATURE);
                cmd.Parameters.AddWithValue("@STATUS_ACTIVITY", STATUS_ACTIVITY);
                cmd.Parameters.AddWithValue("@NEWSTATUS", NEWSTATUS);
                cmd.Parameters.AddWithValue("@PURPOSE", PURPOSE);
                cmd.Parameters.AddWithValue("@NAME", NAME);
                cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
                cmd.Parameters.AddWithValue("@CC_EMAILID", CC_EMAILID);
                cmd.Parameters.AddWithValue("@BCC_EMAILID", BCC_EMAILID);
                cmd.Parameters.AddWithValue("@USERTYPE", USERTYPE);
                //cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);
                cmd.Parameters.AddWithValue("@MAIL_SUBJECT", MAIL_SUBJECT);
                cmd.Parameters.AddWithValue("@MAIL_BODY", MAIL_BODY);

                cmd.Parameters.AddWithValue("@Standard", Standard);
                cmd.Parameters.AddWithValue("@NonRepetative", NonRepetative);
                cmd.Parameters.AddWithValue("@Mandatory", Mandatory);
                cmd.Parameters.AddWithValue("@HelpData", HelpData);
                cmd.Parameters.AddWithValue("@Critic_DP", Critic_DP);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);
                cmd.Parameters.AddWithValue("@AE_PVID", AE_PVID);
                cmd.Parameters.AddWithValue("@AE_RECID", AE_RECID);
                cmd.Parameters.AddWithValue("@TEMPID", TEMPID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@IsComplete", IsComplete);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@MEDICAL_REVIEW", MEDICAL_REVIEW);
                cmd.Parameters.AddWithValue("@TIMEZONE", HttpContext.Current.Session["TimeZone_Value"].ToString());
                cmd.Parameters.AddWithValue("@UploadFileName", UploadFileName);
                cmd.Parameters.AddWithValue("@Size", Size);
                cmd.Parameters.AddWithValue("@FileType", FileType);
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@SysFileName", SysFileName);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet SAE_SHOW_VIEW_SP(string VIEWNAME = null, string SUBJID = null, string SITEID = null, string USERID = null, string TYPE = null, string SAEID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_SHOW_VIEW_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VIEWNAME", VIEWNAME);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DB_SP(string ACTION = null, string MODULE = null, string INVID = null, string USERID = null, string COUNTRYID = null,
            string TYPES = null, string CATEGORY = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("DB_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULE", MODULE);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@TYPES", TYPES);
                cmd.Parameters.AddWithValue("@CATEGORY", CATEGORY);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_ADD_UPDATE(string ACTION = null, string PROJECTID = null, string INVID = null, string SUBSITEID = null, string SUBJECTID = null,
       string VISITNUM = null, string VISIT = null, string USERID = null, string MODULENAME = null, string ID = null,
           string FIELDNAME = null, string VARIABLENAME = null, string TABLENAME = null, string CONTROLTYPE = null, string CLASS = null,
           string DATATYPE = null, string DEFULTVAL = null, string MAXLEN = null, bool BOLDYN = false, bool UNLNYN = false,
           bool READYN = false, bool MULTILINEYN = false, bool UPPERCASE = false, bool CONTINUE = false, bool ALLVISYN = false, string SEQNO = null, string INDICATION = null, string MODULEID = null,
       string VISITCOUNT = null, bool REQUIREDYN = false, bool INVISIBLE = false, string CHILD_ID = null, string VAL_Child = null, string FieldColor = null, string AnsColor = null,
       bool AUTOCODE = false, bool InList = false, string LabID = null, bool LabData = false, bool AutoNum = false, bool REF = false, bool Prefix = false, string PrefixText = null
       , string Limit = null, bool Safety = false, bool DM = false, bool ePRO = false, bool IWRS = false, bool Critic_DP = false,
       string Descrip = null, bool DUPLICATE = false, string QUERYTYPE = null, string QUERYSTATUS = null, bool Survey = false, bool eSource = false
       , bool DMIWRS_Sync = false, string COUNTRYID = null, string RECID = null, string PVID = null, string DOMAIN = null, string HEADER = null, string GROUP_ID = null, string FIELDID = null,
       bool Standard = false, bool NonRepetative = false, bool Mandatory = false, bool HelpData = false, string DefaultData = null, bool Unscheduled = false, bool ParentLinked = false, bool ChildLinked = false,
       string ParentLinkedVARIABLENAME = null, string ParentField = null, string ParentANS = null, string ParentVARIABLENAME = null, bool OneClickEntry = false, bool InListEditable = false,
       string TIMEZONE = null, bool MEDOP = false, bool CTMS = false, bool SAE_HelpData = false, bool PUBLISH_DM = false, bool PUBLISH_eSOURCE = false, bool UNBLINDED = false, bool MultipleYN_SAE = false, bool PD = false, string PD_TABLENAME = null,
       bool CTMS_TRACKER = false, string FORMAT = null, bool eCRF_SignOff = false, string PRIMARY_MODULE_ID = null, string PRIMARY_FIELD_ID = null, string SECONDARY_MODULE_ID = null,
       string SECONDARY_FIELD_ID = null, bool BLINDED = false, string AGE_UNIT = null, string AutoCodeLIB = null, bool SAE_SYNC = false)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                string MEDAUTH_FORM = null;
                if (HttpContext.Current.Session["MEDAUTH_FORM"] != null)
                {
                    MEDAUTH_FORM = HttpContext.Current.Session["MEDAUTH_FORM"].ToString();
                }

                string MEDAUTH_FIELD = null;
                if (HttpContext.Current.Session["MEDAUTH_FIELD"] != null)
                {
                    MEDAUTH_FIELD = HttpContext.Current.Session["MEDAUTH_FIELD"].ToString();
                }

                cmd = new SqlCommand("DM_ADD_UPDATE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@CLASS", CLASS);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                cmd.Parameters.AddWithValue("@DEFAULTVAL", DEFULTVAL);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@BOLDYN", BOLDYN);
                cmd.Parameters.AddWithValue("@UNLNYN", UNLNYN);
                cmd.Parameters.AddWithValue("@READYN", READYN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@UPPERCASE", UPPERCASE);
                cmd.Parameters.AddWithValue("@CONTINUE", CONTINUE);
                cmd.Parameters.AddWithValue("@ALLVISYN", ALLVISYN);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);

                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["User_ID"].ToString());
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VISITCOUNT", VISITCOUNT);
                cmd.Parameters.AddWithValue("@REQUIREDYN", REQUIREDYN);
                cmd.Parameters.AddWithValue("@INVISIBLE", INVISIBLE);
                cmd.Parameters.AddWithValue("@CHILD_ID", CHILD_ID);
                cmd.Parameters.AddWithValue("@VAL_Child", VAL_Child);
                cmd.Parameters.AddWithValue("@FieldColor", FieldColor);
                cmd.Parameters.AddWithValue("@AnsColor", AnsColor);
                cmd.Parameters.AddWithValue("@AUTOCODE", AUTOCODE);
                cmd.Parameters.AddWithValue("@InList", InList);
                cmd.Parameters.AddWithValue("@LabID", LabID);
                cmd.Parameters.AddWithValue("@LabData", LabData);
                cmd.Parameters.AddWithValue("@Refer", REF);
                cmd.Parameters.AddWithValue("@Critic_DP", Critic_DP);
                cmd.Parameters.AddWithValue("@AutoNum", AutoNum);

                cmd.Parameters.AddWithValue("@Prefix", Prefix);
                cmd.Parameters.AddWithValue("@PrefixText", PrefixText);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);

                cmd.Parameters.AddWithValue("@Limit", Limit);


                cmd.Parameters.AddWithValue("@Safety", Safety);
                cmd.Parameters.AddWithValue("@DM", DM);
                cmd.Parameters.AddWithValue("@IWRS", IWRS);
                cmd.Parameters.AddWithValue("@ePRO", ePRO);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);

                cmd.Parameters.AddWithValue("@QUERYSTATUS", QUERYSTATUS);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QUERYTYPE);
                cmd.Parameters.AddWithValue("@Survey", Survey);
                cmd.Parameters.AddWithValue("@eSource", eSource);

                cmd.Parameters.AddWithValue("@DMIWRS_Sync", DMIWRS_Sync);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);

                cmd.Parameters.AddWithValue("@DOMAIN", DOMAIN);

                cmd.Parameters.AddWithValue("@Standard", Standard);
                cmd.Parameters.AddWithValue("@NonRepetative", NonRepetative);
                cmd.Parameters.AddWithValue("@Mandatory", Mandatory);
                cmd.Parameters.AddWithValue("@HelpData", HelpData);

                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);

                cmd.Parameters.AddWithValue("@Unscheduled", Unscheduled);

                cmd.Parameters.AddWithValue("@ParentLinked", ParentLinked);
                cmd.Parameters.AddWithValue("@ChildLinked", ChildLinked);
                cmd.Parameters.AddWithValue("@ParentLinkedVARIABLENAME", ParentLinkedVARIABLENAME);

                cmd.Parameters.AddWithValue("@ParentField", ParentField);
                cmd.Parameters.AddWithValue("@ParentANS", ParentANS);
                cmd.Parameters.AddWithValue("@ParentVARIABLENAME", ParentVARIABLENAME);

                cmd.Parameters.AddWithValue("@OneClickEntry", OneClickEntry);
                cmd.Parameters.AddWithValue("@InListEditable", InListEditable);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);
                cmd.Parameters.AddWithValue("@MEDOP", MEDOP);

                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", MEDAUTH_FORM);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
                cmd.Parameters.AddWithValue("@CTMS", CTMS);
                cmd.Parameters.AddWithValue("@SAE_HelpData", SAE_HelpData);
                cmd.Parameters.AddWithValue("@PUBLISH_DM", PUBLISH_DM);
                cmd.Parameters.AddWithValue("@PUBLISH_eSOURCE", PUBLISH_eSOURCE);
                cmd.Parameters.AddWithValue("@UNBLINDED", UNBLINDED);
                cmd.Parameters.AddWithValue("@BLINDED", BLINDED);
                cmd.Parameters.AddWithValue("@MultipleYN_SAE", MultipleYN_SAE);
                cmd.Parameters.AddWithValue("@PD", PD);
                cmd.Parameters.AddWithValue("@PD_TABLENAME", PD_TABLENAME);
                cmd.Parameters.AddWithValue("@CTMS_TRACKER", CTMS_TRACKER);
                cmd.Parameters.AddWithValue("@FORMAT", FORMAT);
                cmd.Parameters.AddWithValue("@eCRF_SignOff", eCRF_SignOff);
                cmd.Parameters.AddWithValue("@PRIMARY_MODULE_ID", PRIMARY_MODULE_ID);
                cmd.Parameters.AddWithValue("@PRIMARY_FIELD_ID", PRIMARY_FIELD_ID);
                cmd.Parameters.AddWithValue("@SECONDARY_MODULE_ID", SECONDARY_MODULE_ID);
                cmd.Parameters.AddWithValue("@SECONDARY_FIELD_ID", SECONDARY_FIELD_ID);
                cmd.Parameters.AddWithValue("@AGE_UNIT", AGE_UNIT);
                cmd.Parameters.AddWithValue("@SAE_SYNC", SAE_SYNC);

                cmd.Parameters.AddWithValue("@AutoCodeLIB", AutoCodeLIB);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet SAE_ADD_UPDATE(string ACTION = null, string PROJECTID = null, string INVID = null, string SUBSITEID = null, string SUBJECTID = null,
            string VISITNUM = null, string VISIT = null, string USERID = null, string MODULENAME = null, string ID = null,
                string FIELDNAME = null, string VARIABLENAME = null, string TABLENAME = null, string CONTROLTYPE = null, string CLASS = null,
                string DATATYPE = null, string DEFULTVAL = null, string MAXLEN = null, bool BOLDYN = false, bool UNLNYN = false,
                bool READYN = false, bool MULTILINEYN = false, bool UPPERCASE = false, bool CONTINUE = false, bool ALLVISYN = false,
                string SEQNO = null, string INDICATION = null, string MODULEID = null, string VISITCOUNT = null, bool REQUIREDYN = false, bool INVISIBLE = false,
                string CHILD_ID = null, string VAL_Child = null, string FieldColor = null, string AnsColor = null, bool AUTOCODE = false, bool InList = false,
               string LabID = null, bool LabData = false, bool AutoNum = false, bool REF = false, bool Prefix = false, string PrefixText = null
            , string Limit = null, bool Male = false, bool Female = false, bool Safety = false, bool DM = false, string SAEID = null,
             int ContID = 0, string DATA = null, string ENTEREDBY = null, string SAE = null, string VERSION = null, string RECID = null, int CONTYN = 0,
             bool SDVYN = false, string OLDSAEID = null, string PAGESTATUS = null, string INSERTQUERY = null, string UPDATEQUERY = null, string STATUS = null,
             string SELECTQUERY = null, string REASON = null, string SIGNATURE = null, string STATUS_ACTIVITY = null, string NEWSTATUS = null, string PURPOSE = null,
             string EMAILID = null, string CC_EMAILID = null, string BCC_EMAILID = null, string NAME = null, string USERTYPE = null, string MASTERDB = null, string MAIL_SUBJECT = null, string MAIL_BODY = null,
              bool Standard = false, bool NonRepetative = false, bool Mandatory = false, bool HelpData = false, bool Critic_DP = false,
              string Descrip = null, bool DUPLICATE = false, string DM_PVID = null, string DM_RECID = null, string TEMPID = null, string LISTING_ID = null,
              string COUNTRYID = null, bool IsComplete = false, bool IsMissing = false, bool MEDICAL_REVIEW = false)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SAE_ADD_UPDATE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PROJECTID", HttpContext.Current.Session["PROJECTID"].ToString());
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBSITEID", SUBSITEID);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@ID", ID);


                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@CLASS", CLASS);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                cmd.Parameters.AddWithValue("@DEFAULTVAL", DEFULTVAL);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@BOLDYN", BOLDYN);
                cmd.Parameters.AddWithValue("@UNLNYN", UNLNYN);
                cmd.Parameters.AddWithValue("@READYN", READYN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@UPPERCASE", UPPERCASE);
                cmd.Parameters.AddWithValue("@CONTINUE", CONTINUE);
                cmd.Parameters.AddWithValue("@ALLVISYN", ALLVISYN);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);

                cmd.Parameters.AddWithValue("@USERID", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VISITCOUNT", VISITCOUNT);
                cmd.Parameters.AddWithValue("@REQUIREDYN", REQUIREDYN);
                cmd.Parameters.AddWithValue("@INVISIBLE", INVISIBLE);
                cmd.Parameters.AddWithValue("@CHILD_ID", CHILD_ID);
                cmd.Parameters.AddWithValue("@VAL_Child", VAL_Child);
                cmd.Parameters.AddWithValue("@FieldColor", FieldColor);
                cmd.Parameters.AddWithValue("@AnsColor", AnsColor);
                cmd.Parameters.AddWithValue("@AUTOCODE", AUTOCODE);
                cmd.Parameters.AddWithValue("@InList", InList);
                cmd.Parameters.AddWithValue("@LabID", LabID);
                cmd.Parameters.AddWithValue("@LabData", LabData);
                cmd.Parameters.AddWithValue("@Refer", REF);
                cmd.Parameters.AddWithValue("@AutoNum", AutoNum);

                cmd.Parameters.AddWithValue("@Prefix", Prefix);
                cmd.Parameters.AddWithValue("@PrefixText", PrefixText);

                cmd.Parameters.AddWithValue("@Limit", Limit);
                cmd.Parameters.AddWithValue("@Male", Male);
                cmd.Parameters.AddWithValue("@Female", Female);
                cmd.Parameters.AddWithValue("@Safety", Safety);
                cmd.Parameters.AddWithValue("@DM", DM);

                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@ContID", ContID);
                cmd.Parameters.AddWithValue("@DATA", DATA);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@VERSION", VERSION);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@CONTYN", CONTYN);
                cmd.Parameters.AddWithValue("@SDVYN", SDVYN);
                cmd.Parameters.AddWithValue("@OLDSAEID", OLDSAEID);
                cmd.Parameters.AddWithValue("@PAGESTATUS", PAGESTATUS);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@SELECTQUERY", SELECTQUERY);
                cmd.Parameters.AddWithValue("@REASON", REASON);
                cmd.Parameters.AddWithValue("@SIGNATURE", SIGNATURE);
                cmd.Parameters.AddWithValue("@STATUS_ACTIVITY", STATUS_ACTIVITY);
                cmd.Parameters.AddWithValue("@NEWSTATUS", NEWSTATUS);
                cmd.Parameters.AddWithValue("@PURPOSE", PURPOSE);
                cmd.Parameters.AddWithValue("@NAME", NAME);
                cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
                cmd.Parameters.AddWithValue("@CC_EMAILID", CC_EMAILID);
                cmd.Parameters.AddWithValue("@BCC_EMAILID", BCC_EMAILID);
                cmd.Parameters.AddWithValue("@USERTYPE", USERTYPE);
                cmd.Parameters.AddWithValue("@MAIL_SUBJECT", MAIL_SUBJECT);
                cmd.Parameters.AddWithValue("@MAIL_BODY", MAIL_BODY);

                cmd.Parameters.AddWithValue("@Standard", Standard);
                cmd.Parameters.AddWithValue("@NonRepetative", NonRepetative);
                cmd.Parameters.AddWithValue("@Mandatory", Mandatory);
                cmd.Parameters.AddWithValue("@HelpData", HelpData);
                cmd.Parameters.AddWithValue("@Critic_DP", Critic_DP);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);
                cmd.Parameters.AddWithValue("@DM_PVID", DM_PVID);
                cmd.Parameters.AddWithValue("@DM_RECID", DM_RECID);
                cmd.Parameters.AddWithValue("@TEMPID", TEMPID);
                cmd.Parameters.AddWithValue("@LISTING_ID", LISTING_ID);
                cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
                cmd.Parameters.AddWithValue("@IsComplete", IsComplete);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@MEDICAL_REVIEW", MEDICAL_REVIEW);
                cmd.Parameters.AddWithValue("@TIMEZONE", HttpContext.Current.Session["TimeZone_Value"].ToString());

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet EXPORT_DATA_SP(string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("EXPORT_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ACTION);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
            }
            return ds;
        }

        public DataSet EMAIL_INTEGRATION(string ACTION = null, string DISPLAYNAME = null, string HOSTNAME = null, string PORTNO = null, string USERNAME = null, string PASSWORD = null, string ID = null,
            bool SSL = false, string IPADDRESS = null)
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd = new SqlCommand("EMAIL_INTEGRATION_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@DISPLAYNAME", DISPLAYNAME);
                cmd.Parameters.AddWithValue("@HOSTNAME", HOSTNAME);
                cmd.Parameters.AddWithValue("@PORTNO", PORTNO);
                cmd.Parameters.AddWithValue("@USERNAME", USERNAME);
                cmd.Parameters.AddWithValue("@PASSWORD", PASSWORD);
                cmd.Parameters.AddWithValue("@SSL", SSL);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
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

            }
            return ds;
        }

        public DataSet Export_Data_SP(string Action = null, string PRODUCTID = null, string PRODUCTNAM = null, string ENTEREDBY = null, string SUBCLASSID = null, string ID = null, string FUNCTIONNAME = null, string UserGroup_ID = null, string MODULE = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("EXPORT_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PRODUCTID", PRODUCTID);
                cmd.Parameters.AddWithValue("@PRODUCTNAM", PRODUCTNAM);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SUBCLASSID", SUBCLASSID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FUNCTIONNAME", FUNCTIONNAME);
                cmd.Parameters.AddWithValue("@UserGroup_ID", UserGroup_ID);
                cmd.Parameters.AddWithValue("@MODULE", MODULE);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }

        public DataSet EMAIL_CONFIG_SP(string Action = null, string Email_Type = null, string E_Sub = null, string E_Body = null, string E_CC = null
            , string ID = null, string E_TO = null, string IPADDRESS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {

                cmd = new SqlCommand("EMAIL_CONFIG_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Email_Type", Email_Type);
                cmd.Parameters.AddWithValue("@E_Sub", E_Sub);
                cmd.Parameters.AddWithValue("@E_Body", E_Body);
                cmd.Parameters.AddWithValue("@E_CC", E_CC);
                cmd.Parameters.AddWithValue("@E_TO", E_TO);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }
        public DataSet SAE_DATA_PRINT(string ACTION, string SAEID = null, string SUBJID = null, string INVID = null, string STATUS = null, string MODULENAME = null, string MODULEID = null, string TABLENAME = null, string RECID = null, string VARIABLENAME = null, string PROJECTID = null, string DefaultData = null, string FIELD_ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_DATA_PRINT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@FIELD_ID", FIELD_ID);
                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);
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

        public DataSet GetSet_DM_ProjectData(string Action = null, string PVID = null,
           int ContID = 0, string VARIABLENAME = null, string FIELDNAME = null,
               string TABLENAME = null, string CONTROLTYPE = null, string CLASS = null,
            string DATATYPE = null, string DATA = null, string RECID = null, int CONTYN = 0, string ENTEREDBY = null,
            string UPDATEDBY = null, string PROJECTID = null, string MODULENAME = null, string VISITNUM = null, string SUBJID = null,
            string MODULEID = null, string INDICATION = null, string INSERTQUERY = null, string UPDATEQUERY = null
            , string IMPORT_MODULEID = null, string EMAIL_IDS = null, string CCEMAIL_IDS = null, string BCCEMAIL_IDS = null, string INVID = null,
            string Email_Subject = null, string Email_body = null, bool IsMissing = false, string ID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();

            try
            {
                string MEDAUTH_FORM = null;
                if (HttpContext.Current.Session["MEDAUTH_FORM"] != null)
                {
                    MEDAUTH_FORM = HttpContext.Current.Session["MEDAUTH_FORM"].ToString();
                }

                string MEDAUTH_FIELD = null;
                if (HttpContext.Current.Session["MEDAUTH_FIELD"] != null)
                {
                    MEDAUTH_FIELD = HttpContext.Current.Session["MEDAUTH_FIELD"].ToString();
                }

                cmd = new SqlCommand("DM_PROJECT_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@ContID", ContID);
                cmd.Parameters.AddWithValue("@RecID", RECID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@CLASS", CLASS);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);

                if (CONTROLTYPE == "DROPDOWN")
                {
                    if (DATA != "0")
                    {
                        cmd.Parameters.AddWithValue("@DATA", DATA);
                    }
                }
                else
                {
                    if (DATA != "")
                    {
                        cmd.Parameters.AddWithValue("@DATA", DATA);
                    }
                }

                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@UPDATEDBY", UPDATEDBY);
                cmd.Parameters.AddWithValue("@CONTYN", CONTYN);
                cmd.Parameters.AddWithValue("@IMPORT_MODULEID", IMPORT_MODULEID);
                cmd.Parameters.AddWithValue("@EMAIL_IDS", EMAIL_IDS);
                cmd.Parameters.AddWithValue("@CCEMAIL_IDS", CCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@BCCEMAIL_IDS", BCCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@EMAIL_SUBJECT", Email_Subject);
                cmd.Parameters.AddWithValue("@EMAIL_BODY", Email_body);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", MEDAUTH_FORM);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
                cmd.Parameters.AddWithValue("@ID", ID);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }


        //  SAE_TYPE_SP
        public DataSet SAE_TYPE_SP(string ACTION = null, string SEQNO = null, string MODULEID = null, string @MODULENAME = null, string DM_MODULEID = null, string @ENTEREDBY = null, string ID = null, string SaeTYPE = null, string TABLENAME = null, string DOMAIN = null, string FIELDID = null, string FIELDNAME = null, string TYPEID = null, string VARIABLENAME = null, string SAE_MODULEID = null, string SAE_MODULENAME = null, string SAE_DOMAIN = null, string SAE_TABLENAME = null, string SAE_FIELDID = null, string SAE_FIELDNAME = null, string SAE_VARIABLENAME = null, string DTENTERED = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SAE_TYPE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SAE_TYPE", SaeTYPE);
                cmd.Parameters.AddWithValue("@TYPEID", TYPEID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@DOMAIN", DOMAIN);

                cmd.Parameters.AddWithValue("@SAE_MODULEID", SAE_MODULEID);
                cmd.Parameters.AddWithValue("@SAE_MODULENAME", SAE_MODULENAME);
                cmd.Parameters.AddWithValue("@SAE_DOMAIN", SAE_DOMAIN);
                cmd.Parameters.AddWithValue("@SAE_TABLENAME", SAE_TABLENAME);
                cmd.Parameters.AddWithValue("@SAE_FIELDID", SAE_FIELDID);
                cmd.Parameters.AddWithValue("@SAE_FIELDNAME", SAE_FIELDNAME);
                cmd.Parameters.AddWithValue("@SAE_VARIABLENAME", SAE_VARIABLENAME);

                cmd.Parameters.AddWithValue("@ENTEREDBY", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@DTENTERED", DTENTERED);
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


        public DataTable Get_WHODData(string pt_name = "", string Filter1 = "", string Filter2 = "", string Filter3 = "", string Filter4 = "", string Filter5 = "", string ProjectId = "")
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_WHODData", con);
                cmd.Parameters.AddWithValue("@pt_name", pt_name);
                cmd.Parameters.AddWithValue("@Filter1", Filter1);
                cmd.Parameters.AddWithValue("@Filter2", Filter2);
                cmd.Parameters.AddWithValue("@Filter3", Filter3);
                cmd.Parameters.AddWithValue("@Filter4", Filter4);
                cmd.Parameters.AddWithValue("@Filter5", Filter5);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(ds);
                dt = ds.Tables[0];
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return dt;
        }
        //===========================================UMT END=======================================================================

        public DataSet GetDropDownData(string Action = null, string VariableName = null, string VISITNUM = null, string ParentANS = null, string ParentVARIABLENAME = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("FillDropDown_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@VariableName", VariableName);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@ParentANS", ParentANS);
                cmd.Parameters.AddWithValue("@ParentVARIABLENAME", ParentVARIABLENAME);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd = null;
                adp = null;
            }
            return ds;
        }


        public DataSet COMM_SP(string ID = null, string SEQNO = null, string CritName = null, string Criteria = null, string CritCode = null, string Field1 = null, string CONDITION1 = null, string Value1 = null, string AndOr1 = null, string Field2 = null, string AndOr4 = null, string AndOr5 = null, string Field3 = null, string Field4 = null, string Field5 = null, string Field6 = null, string Condition3 = null, string Condition4 = null, string Condition5 = null, string Condition6 = null, string Condition2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string Value6 = null, string AndOr2 = null, string AndOr3 = null, string Value2 = null, string FIELDNAME = null, string FormCode = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("COMM_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@CritName", CritName);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@CONDITION1", CONDITION1);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);
                cmd.Parameters.AddWithValue("@AndOr5", AndOr5);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Field5", Field5);
                cmd.Parameters.AddWithValue("@Field6", Field6);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@Condition6", Condition6);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@Value6", Value6);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@FormCode", FormCode);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);


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

        public DataSet eTMF_SET_SP(string ACTION = null, string DocName = null, string RefNo = null, string USERID = null, string Project_ID = null, string DocType = null, string User = null, bool ETMF = false, bool CTMS = false, bool SITE = false, bool SPONSOR = false, bool QA = false, string Artifact_Name = null, string Folder_ID = null, string SEQNO = null, string DocID = null, string UnblindingType = null, bool E_Upload = false, string VerTYPE = null, string NoExpDocs = null, bool E_Download = false, string UniqueRefNo = null, string DocTypeId = null, string SPECtitle = null, string SubFolder_ID = null, string Comment = null, string Sub_Task_ID = null, string DateTitle = null, bool Internal = false, string Task_ID = null, string Folder = null, string FileName = null, string ContentType = null, string Data = null, string INVID = null, string ID = null, string Dept_Id = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("eTMF_SET_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@DocName", DocName);
                cmd.Parameters.AddWithValue("@RefNo", RefNo);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@DocType", DocType);
                cmd.Parameters.AddWithValue("@User", User);
                cmd.Parameters.AddWithValue("@ETMF", ETMF);
                cmd.Parameters.AddWithValue("@CTMS", CTMS);
                cmd.Parameters.AddWithValue("@SITE", SITE);
                cmd.Parameters.AddWithValue("@SPONSOR", SPONSOR);
                cmd.Parameters.AddWithValue("@QA", QA);
                cmd.Parameters.AddWithValue("@Artifact_Name", Artifact_Name);
                cmd.Parameters.AddWithValue("@Folder_ID", Folder_ID);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@DocID", DocID);
                cmd.Parameters.AddWithValue("@UnblindingType", UnblindingType);
                cmd.Parameters.AddWithValue("@E_Upload", E_Upload);
                cmd.Parameters.AddWithValue("@VerTYPE", VerTYPE);
                cmd.Parameters.AddWithValue("@NoExpDocs", NoExpDocs);
                cmd.Parameters.AddWithValue("@E_Download", E_Download);
                cmd.Parameters.AddWithValue("@UniqueRefNo", UniqueRefNo);
                cmd.Parameters.AddWithValue("@DocTypeId", DocTypeId);
                cmd.Parameters.AddWithValue("@SPECtitle", SPECtitle);
                cmd.Parameters.AddWithValue("@SubFolder_ID", SubFolder_ID);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@Sub_Task_ID", Sub_Task_ID);
                cmd.Parameters.AddWithValue("@DateTitle", DateTitle);
                cmd.Parameters.AddWithValue("@Internal", Internal);
                cmd.Parameters.AddWithValue("@Task_ID", Task_ID);
                cmd.Parameters.AddWithValue("@Folder", Folder);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Dept_Id", Dept_Id);


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

        public DataSet eTMF_DATA_SP(string SUBFOLDERID = null, string ID = null, string UploadZoneId = null, string ARTIFACTS = null, string UploadBy = null, string FOLDERID = null, string RefNo = null, string UploadTaskId = null, string USERID = null, string Status = null, string DocID = null, string UploadSectionId = null, string UploadArtifactId = null, string SubFolder_ID = null, string DocTypeId = null, string QCDocLocat = null, string QCDocLegible = null, string QCDocCurrent = null, string Folder_ID = null, string DocName = null, string DeadlineDate = null, string UploadSubTaskId = null, string UploadDocTypeid = null, string UploadDepartmentId = null, string QCDocGuide = null, string VersionID = null, string SEQNO = null, string INDIVIDUAL = null, string NOTE = null, string FileType = null, string Size = null, string Functions = null, string UploadFileName = null, string DOC_DATETIME = null, string ExpiryDate = null, string DOC_VERSIONNO = null, string SiteID = null, string CountryID = null, string AutoNomenclature = null, string UniqueRefNo = null, string Project_ID = null, string SnapId = null, string User = null, string SectionId = null, string ZoneID = null, string DOCTYPEID = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
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
                cmd.Parameters.AddWithValue("@USERID", USERID);
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


        public DataSet eTMF_UPLOAD_SP(string ACTION = null, string FOLDERID = null, string SUBFOLDERID = null, string DeadlineDate = null, string ARTIFACTS = null, string DOCTYPEID = null, string RefNo = null, string UploadFileName = null, string DocName = null, string CountryID = null, string SiteID = null, string SEQNO = null, string USERID = null, string VersionID = null, string PROJECTID = null, string DocID = null, string Status = null, string NOTE = null, string ExpiryDate = null, string UploadSubTaskId = null, string UploadDepartmentId = null, string UploadTaskId = null, string UploadSectionId = null, string UploadArtifactId = null, string AutoNomenclature = null, string UploadFileName_Editable = null, string Functions = null, string FileType_Editable = null, string SubStatus = null, string REFRENCE_DOC = null, string SUBJID = null, string SysFileName = null, bool SendEmail = false, string Size_Editable = null, string ToEmailIDs = null, string DOC_REMINDERDAT = null, string CCEmailIDs = null, string SPEC = null, string Size = null, string UploadDateTime = null, string ReplaceFile = null, string FileType = null, string DOC_VERSIONNO = null, string UniqueRefNo = null, string UploadDocTypeid = null, string INDIVIDUAL = null, string UploadZoneId = null, string DOC_DATETIME = null, string UploadBy = null, string ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
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
                cmd.Parameters.AddWithValue("@USERID", USERID);
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

        public DataSet CTMS_SET_SP(string Sub_Task_ID = null, string Task_ID = null, string INVID = null, string Project_ID = null, string SubFolder_ID = null, string Folder_ID = null, string Action = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("CTMS_SET_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Sub_Task_ID", Sub_Task_ID);
                cmd.Parameters.AddWithValue("@Task_ID", Task_ID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@SubFolder_ID", SubFolder_ID);
                cmd.Parameters.AddWithValue("@Folder_ID", Folder_ID);
                cmd.Parameters.AddWithValue("@Action", Action);


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


        public DataSet CODING_SP(string ACTION, string PVID = null, string RECID = null, string MODULEID = null,
            string DATA = null, string ID = null, string SUBJID = null, string ENTEREDBY = null, string FIELDNAME = null,
            string TABLENAME = null, string MODULENAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("CODING_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@DATA", DATA);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@ENTEREDBY", HttpContext.Current.Session["User_Id"].ToString());

                cmd.CommandTimeout = 0;

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_ANNOTATED_SP(string DefaultData = null, string ID = null, string MODULEID = null, string RECID = null, string PVID = null, string TABLENAME = null, string TIMEZONE = null, string SUBJID = null, string VISITID = null, string VARIABLENAME = null, string ACTION = null, string VISITNUM = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_ANNOTATED_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }


        public DataSet DM_STAT_AUDIT_SP(string SUBJID = null, string DATA = null, string MODULEID = null, string SITEID = null, string FIELDID = null, string SUBJECTID = null, string TIMEZONE = null, string TRANSACT = null, string TABLENAME = null, string VISITID = null, string VISITNUM = null, string PVID = null, string RECID = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_STAT_AUDIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@DATA", DATA);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);
                cmd.Parameters.AddWithValue("@TRANSACT", TRANSACT);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }
        public DataSet DM_ESOURCE_SP(string PVID = null, string TIMEZONE = null, string SUBJECTID = null, string SITEID = null, string TABLENAME = null, string MODULEID = null, string SUBJID = null, string RECID = null, string VISITNUM = null, string INVID = null, string ACTION = null, string USERID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_ESOURCE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@USERID", USERID);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_SDV_SP(string visit = null, string Unblind = null, string USERID = null, string ID = null, string AUTOID = null, string SDVBY = null, string VISITID = null, string SUBJID = null, string SITEID = null, string INVID = null, string VISITNUM = null, string PVID = null, string ENTEREDBY = null, bool SDVYN = false, string RECID = null, string TABLENAME = null, string VARIABLENAME = null, string CONTID = null, bool MULTIPLEYN = false, string MODULEID = null, string ACTION = null, string Project_ID = null, string PAGENUM = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_SDV_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@visit", visit);
                cmd.Parameters.AddWithValue("@Unblind", Unblind);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@AUTOID", AUTOID);
                cmd.Parameters.AddWithValue("@SDVBY", SDVBY);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SDVYN", SDVYN);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@MULTIPLEYN", MULTIPLEYN);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@PAGENUM", PAGENUM);


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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_STATUS_SP(string PVID = null, string RECID = null, string INDICATION = null, string VISIT = null, string INVID = null, string PROJECTID = null, string SUBJECTID = null, string DATATYPE = null, string DATA = null, string SUBJID = null, string USERID = null, string SITEID = null, string SDVStatus = null, string SignStatus = null, string VISITID = null, string EntryStatus = null, string ACTION = null, string VISITNUM = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_STATUS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                cmd.Parameters.AddWithValue("@DATA", DATA);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SDVStatus", SDVStatus);
                cmd.Parameters.AddWithValue("@SignStatus", SignStatus);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@EntryStatus", EntryStatus);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }


        public DataSet DM_ENTRY_SP(string RECID = null, string PVID = null, string MODULEID = null, string ACTION = null, string TABLENAME = null, string SUBJID = null, string VARIABLENAME = null, string VISITNUM = null, string INDICATION = null, string MODULENAME = null, string PROJECTID = null, bool MEDAUTH_FIELD = false)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_ENTRY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);



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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_FIELD_SP(string ID = null, string CTMS_TABLENAME = null, string VARIABLENAME = null, string ACTION = null, string VISITNUM = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_FIELD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@CTMS_TABLENAME", CTMS_TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);


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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet CTMS_MODULE_SP(string VISITNUM = null, string INDICATION = null, string PROJECTID = null, string MODULEID = null, string MODULENAME = null, string USERID = null, bool CTMS_TRACKER = false, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("CTMS_MODULE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@CTMS_TRACKER", CTMS_TRACKER);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);



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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_UNSCH_SP(string SubjID = null, string InvID = null, string INDICATION = null, string PROJECTID = null, string USERID = null, string SUBJECTID = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_UNSCH_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubjID", SubjID);
                cmd.Parameters.AddWithValue("@InvID", InvID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }





        public DataSet DM_REQUESTS_SP(string Action = null, string INVID = null, string SubjID = null, string VISITNUM = null, string PAGENUM = null, string EnteredBy = null, string Project_ID = null, string VISITCOUNT = null, string REQUEST = null, string REQ_ID = null, string Status = null, string Comments = null, string USERID = null, bool MULTIPLEYN = false, string RECID = null, string PVID = null, string TABLENAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_REQUESTS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SubjID", SubjID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@PAGENUM", PAGENUM);
                cmd.Parameters.AddWithValue("@EnteredBy", EnteredBy);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@VISITCOUNT", VISITCOUNT);
                cmd.Parameters.AddWithValue("@REQUEST", REQUEST);
                cmd.Parameters.AddWithValue("@REQ_ID", REQ_ID);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Comments", Comments);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@MULTIPLEYN", MULTIPLEYN);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);

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

        public DataSet DM_ManQuery_SP(string Action = null, string PVID = null, string CONTID = null, string RECID = null, string QID = null, string SUBJID = null, string QUERYTEXT = null, string MODULENAME = null, string FIELDNAME = null, string TABLENAME = null, string VARIABLENAME = null, string ENTEREDBY = null, string TIMEZONE = null, string SAEID = null, string MODULEID = null, string MASTERDB = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_ManQuery_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@QID", QID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@QUERYTEXT", QUERYTEXT);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MASTERDB", MASTERDB);

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

        public DataSet DM_PAGE_SP(string Action = null, string InvID = null, string SubjID = null, string visit = null, string INDICATION = null, string MEDAUTH_FORM = null, string MEDAUTH_FIELD = null, string Unblind = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_PAGE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@InvID", InvID);
                cmd.Parameters.AddWithValue("@SubjID", SubjID);
                cmd.Parameters.AddWithValue("@visit", visit);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", MEDAUTH_FORM);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
                cmd.Parameters.AddWithValue("@Unblind", Unblind);


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



        public DataSet DM_DATA_SP(string PVID = null, string MODULEID = null, string INDICATION = null, string RECID = null, string TABLENAME = null, string VARIABLENAME = null, string SUBJID = null, string VISITNUM = null, string IsMissing = null, string ENTEREDBY = null, string Action = null, string PROJECTID = null, string MODULENAME = null,
            string INVID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@INVID", INVID);



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

        public DataSet DM_EMAIL_SP(string Action = null, string INVID = null, string EMAIL_IDS = null, string CCEMAIL_IDS = null, string BCCEMAIL_IDS = null, string EMAIL_BODY = null, string EMAIL_SUBJECT = null, string CONTROLTYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_EMAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@EMAIL_IDS", EMAIL_IDS);
                cmd.Parameters.AddWithValue("@CCEMAIL_IDS", CCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@BCCEMAIL_IDS", BCCEMAIL_IDS);
                cmd.Parameters.AddWithValue("@EMAIL_BODY", EMAIL_BODY);
                cmd.Parameters.AddWithValue("@EMAIL_SUBJECT", EMAIL_SUBJECT);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);


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

        public DataSet DM_SYNC_SP(string Action = null, string FIELDNAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_SYNC_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);


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

        public DataSet DM_DRP_SP(string Action = null, string VISITNUM = null, string SUBJID = null, string VARIABLENAME = null, string TABLENAME = null, string PVID = null, string RECID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_DRP_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);


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



        public DataSet DM_INSERT_SP(string INSERTQUERY = null, string MODULEID = null, string TABLENAME = null, string PVID = null, string RECID = null, string SUBJID = null, bool IsMissing = false, string VISITNUM = null, string UPDATEQUERY = null, string INVID = null, string Action = null, string IMPORT_MODULEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_INSERT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@IMPORT_MODULEID", IMPORT_MODULEID);

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

        public DataSet DM_REVIEW_SP(string Action = null, string INDICATION = null, string SUBJID = null, string MODULENAME = null, string PVID = null, string VISITNUM = null, string ContID = null, string MODULEID = null, string FIELDNAME = null, string PROJECTID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_REVIEW_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@ContID", ContID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);

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


        public DataSet DM_LOCK_SP(string Action = null, string INVID = null, string VISITNUM = null, string PAGESTATUS = null, string PVID = null, string RECID = null, string EnteredBy = null, string SubjID = null, string TABLENAME = null, bool MULTIPLEYN = false, string Project_ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_LOCK_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@PAGESTATUS", PAGESTATUS);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@EnteredBy", EnteredBy);
                cmd.Parameters.AddWithValue("@SubjID", SubjID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@MULTIPLEYN", MULTIPLEYN);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);

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

        public DataSet DM_SIGN_SP(string Action = null, string INVID = null, string SubjID = null, string VISITNUM = null, string MODULEID = null, string PAGENUM = null, string EnteredBy = null, string Project_ID = null, string VISITCOUNT = null, string TIMEZONE = null, string SDVSTATUS = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_SIGN_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SubjID", SubjID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@PAGENUM", PAGENUM);
                cmd.Parameters.AddWithValue("@EnteredBy", EnteredBy);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@VISITCOUNT", VISITCOUNT);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);
                cmd.Parameters.AddWithValue("@SDVSTATUS", SDVSTATUS);

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





        public DataSet DB_RULE_SP(string ACTION = null, string ID = null, string Project_ID = null, string Indication_ID = null, string RULE_ID = null, string Actions = null, string Nature = null, string Informational = null, string All = null, string Visit_ID = null, string Module_ID = null, string Field_ID = null, string Description = null, string QueryText = null, string DATA = null, string ENTEREDBY = null, string SEQNO = null, string Value = null, string Condition = null, string Formula = null, string TESTED = null, string GEN_QUERY = null, string SET_VALUE = null, string DERIVED = null, string VARIABLENAMEDEC = null, string SUBJID = null, string VISITNO = null, string RECID = null, string PVID = null, string ColumnName = null, string TableName = null, string VISIT = null, string Criteria_ID = null, string FIELDID = null, string VISITNUM = null, string MODULEID = null, string ONESIDED = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_RULE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);
                cmd.Parameters.AddWithValue("@Indication_ID", Indication_ID);
                cmd.Parameters.AddWithValue("@RULE_ID", RULE_ID);
                cmd.Parameters.AddWithValue("@Actions", Actions);
                cmd.Parameters.AddWithValue("@Nature", Nature);
                cmd.Parameters.AddWithValue("@Informational", Informational);
                cmd.Parameters.AddWithValue("@All", All);
                cmd.Parameters.AddWithValue("@Visit_ID", Visit_ID);
                cmd.Parameters.AddWithValue("@Module_ID", Module_ID);
                cmd.Parameters.AddWithValue("@Field_ID", Field_ID);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@QueryText", QueryText);
                cmd.Parameters.AddWithValue("@DATA", DATA);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@Value", Value);
                cmd.Parameters.AddWithValue("@Condition", Condition);
                cmd.Parameters.AddWithValue("@Formula", Formula);
                cmd.Parameters.AddWithValue("@TESTED", TESTED);
                cmd.Parameters.AddWithValue("@GEN_QUERY", GEN_QUERY);
                cmd.Parameters.AddWithValue("@SET_VALUE", SET_VALUE);
                cmd.Parameters.AddWithValue("@DERIVED", DERIVED);
                cmd.Parameters.AddWithValue("@VARIABLENAMEDEC", VARIABLENAMEDEC);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNO", VISITNO);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@ColumnName", ColumnName);
                cmd.Parameters.AddWithValue("@TableName", TableName);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@Criteria_ID", Criteria_ID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ONESIDED", ONESIDED);

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

        public DataSet DB_UPLOAD_SP(string USERID = null, string PROJECTID = null, string Descrip = null, string MAXLEN = null, string TABLENAME = null, string SEQNO = null, string DATATYPE = null, string FIELDNAME = null, string CONTROLTYPE = null, string CLASS = null, string SDTM_VARIABLENAME = null, string MODULEID = null, string VARIABLENAME = null, string MODULENAME = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_UPLOAD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@CLASS", CLASS);
                cmd.Parameters.AddWithValue("@SDTM_VARIABLENAME", SDTM_VARIABLENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DB_LAB_SP(string ID = null, string SITEID = null, string SUBJID = null, string PVID = null, string RECID = null, string MODULEID = null, string EFFDATETO = null, string VISITID = null, string LBTEST = null, string SEX = null, string LABID = null, string LABNAME = null, string LBORNRHI = null, string LBORNRLO = null, string LBORRESU = null, string AGEHI = null, string EFFDATEFROM = null, string AGELO = null, string ACTION = null, string PRIMARY_MODULE_ID = null, string PRIMARY_FIELD_ID = null, string SECONDARY_MODULE_ID = null, string SECONDARY_FIELD_ID = null, string AGE_UNIT = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_LAB_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@EFFDATETO", EFFDATETO);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@LBTEST", LBTEST);
                cmd.Parameters.AddWithValue("@SEX", SEX);
                cmd.Parameters.AddWithValue("@LABID", LABID);
                cmd.Parameters.AddWithValue("@LABNAME", LABNAME);
                cmd.Parameters.AddWithValue("@LBORNRHI", LBORNRHI);
                cmd.Parameters.AddWithValue("@LBORNRLO", LBORNRLO);
                cmd.Parameters.AddWithValue("@LBORRESU", LBORRESU);
                cmd.Parameters.AddWithValue("@AGEHI", AGEHI);
                cmd.Parameters.AddWithValue("@EFFDATEFROM", EFFDATEFROM);
                cmd.Parameters.AddWithValue("@AGELO", AGELO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PRIMARY_MODULE_ID", PRIMARY_MODULE_ID);
                cmd.Parameters.AddWithValue("@PRIMARY_FIELD_ID", PRIMARY_FIELD_ID);
                cmd.Parameters.AddWithValue("@SECONDARY_MODULE_ID", SECONDARY_MODULE_ID);
                cmd.Parameters.AddWithValue("@SECONDARY_FIELD_ID", SECONDARY_FIELD_ID);
                cmd.Parameters.AddWithValue("@AGE_UNIT", AGE_UNIT);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DB_CTMS_SP(string VISITNUM = null, string INDICATION = null, string PROJECTID = null, string MODULEID = null, string MODULENAME = null, string USERID = null, bool CTMS_TRACKER = false, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_CTMS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@CTMS_TRACKER", CTMS_TRACKER);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }





        public DataSet DB_PV_SP(string PROJECTID = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_PV_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }



















        public DataSet DB_REPORT_SP(string PROJECTID = null, string MODULEID = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_REPORT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DB_IMPORT_SP(string ID = null, string IMPORT_TABLENAME = null, string MODULEID = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_IMPORT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@IMPORT_TABLENAME", IMPORT_TABLENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }



        public DataSet DB_DATA_SP(string INVID = null, string SUBJECTID = null, string LabID = null, string FIELDNAME = null, string DEFAULTVAL = null, string ID = null, string VISITNUM = null, string MODULEID = null, string PROJECTID = null, string VARIABLENAME = null, string TABLENAME = null, string PVID = null, string RECID = null, string DefaultData = null, string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJECTID", SUBJECTID);
                cmd.Parameters.AddWithValue("@LabID", LabID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@DEFAULTVAL", DEFAULTVAL);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet UMT_EMAIL_SP(string ID = null, string ACTIONS = null, string ACTION = null, string To = null, string CC = null, string BCC = null, string Email_Code = null, string SITEID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string UserID = null, TZ_VAL = null, User_Name = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    UserID = HttpContext.Current.Session["USER_ID"].ToString();
                }
                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd = new SqlCommand("UMT_EMAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ACTIONS", ACTIONS);
                cmd.Parameters.AddWithValue("@To", To);
                cmd.Parameters.AddWithValue("@CC", CC);
                cmd.Parameters.AddWithValue("@BCC", BCC);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@Email_Code", Email_Code);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);

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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet SYSTEM_SP(string ACTION = null, string ID = null, string PROJECTID = null, string USER_GROUP = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("SYSTEM_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@USER_GROUP", USER_GROUP);
                cmd.Parameters.AddWithValue("@ID", ID);

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

        public DataSet UMT_ETHICS_COMM_SP(string ID = null, string SiteID = null, string Name = null, string EmailID = null, string ContactNo = null, string Address = null,
            string ACTION = null, string ENTEREDBY = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("UMT_ETHICS_COMM_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SiteID", SiteID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@ContactNo", ContactNo);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
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


        //Lean DAL Methods DM NIKHIL---

        public void ESOURCE_GetSetPV(string Action = null, string PVID = null, string SUBJID = null, string VISITNUM = null, string MODULEID = null, string PAGESTATUS = null, bool HasMissing = false, string FREEZESTATUS = null, string NOTAPPLICABLE = null, string MODULENAME = null, string VISIT = null, string INVID = null)
        {
            SqlCommand cmd;
            try
            {
                string TZ_VAL = null, User_Name = null, USERID = "";

                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("ESOURCE_PV_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@PAGESTATUS", PAGESTATUS);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@HasMissing", HasMissing);
                cmd.Parameters.AddWithValue("@NOTAPPLICABLE", NOTAPPLICABLE);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
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
                cmd.Parameters.AddWithValue("@FREEZESTATUS", FREEZESTATUS);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            finally
            {
                cmd = null;
            }
        }


        public DataSet ESOURCE_INSERT_MODULE_DATA_SP(string ACTION = null, string TABLENAME = null, string PVID = null, string RECID = null, string INSERTQUERY = null, string UPDATEQUERY = null, string SUBJID = null, string VISITNUM = null, string INVID = null, string MODULEID = null, string INSERTQUERY_IWRS = null, bool IsComplete = false, bool IsMissing = false, string FREEZESTATUS = null, string VISIT = null, string MODULENAME = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
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

                cmd = new SqlCommand("ESOURCE_INSERT_MODULE_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@INSERTQUERY_IWRS", INSERTQUERY_IWRS);
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
                cmd.Parameters.AddWithValue("@IsComplete", IsComplete);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@FREEZESTATUS", FREEZESTATUS);

                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

































        public DataSet DM_LAB_DATA_SP(string ACTION = null, string MODULEID = null, string SUBJID = null, string LABID = null, string LBTEST = null, string SEX = null,
            string INVID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("DM_LAB_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@LABID", LABID);
                cmd.Parameters.AddWithValue("@LBTEST", LBTEST);
                cmd.Parameters.AddWithValue("@SEX", SEX);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_ONE_CLICK_ENTRY_SP(string ACTION = null, string MODULEID = null, string VISITNUM = null, string TABLENAME = null, string VARIABLENAME = null, string PVID = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("DM_ONE_CLICK_ENTRY_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DM_OnClick_CRIT_SP(string ACTION = null, string MODULEID = null, string Condition = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("DM_OnClick_CRIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@Condition", Condition);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }









        public DataSet DM_DATA_SP(string ACTION = null, string SUBJID = null, string MODULEID = null, string ID = null,
            string TABLENAME = null, string INSERTQUERY = null, string UPDATEQUERY = null, string MOBILENO = null, string FIELDNAME = null,
            string PVID = null, string RECID = null, string VISITNUM = null, string SITEID = null, string USERID = null, string INDICATION = null,
         string LabID = null, string VISIT = null, string NOTES = null, string TIMEZONE = null, bool IsMissing = false, string UploadFileName = null,
         string Size = null, string FileType = null, string SysFileName = null, string MacAddress = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
            try
            {
                TIMEZONE = null;
                string Unblind = "";
                string User_Name = "";

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TIMEZONE = "+05:30";
                }
                else
                {
                    TIMEZONE = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                string MEDAUTH_FORM = null;
                if (HttpContext.Current.Session["MEDAUTH_FORM"] != null)
                {
                    MEDAUTH_FORM = HttpContext.Current.Session["MEDAUTH_FORM"].ToString();
                }

                string MEDAUTH_FIELD = null;
                if (HttpContext.Current.Session["MEDAUTH_FIELD"] != null)
                {
                    MEDAUTH_FIELD = HttpContext.Current.Session["MEDAUTH_FIELD"].ToString();
                }

                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                if (HttpContext.Current.Session["Unblind"] != null)
                {
                    Unblind = HttpContext.Current.Session["Unblind"].ToString();
                }

                if (HttpContext.Current.Session["User_Name"] != null)
                {
                    User_Name = HttpContext.Current.Session["User_Name"].ToString();
                }

                cmd = new SqlCommand("DM_DATA_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@LabID", LabID);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@NOTES", NOTES);
                cmd.Parameters.AddWithValue("@TIMEZONE", TIMEZONE);
                cmd.Parameters.AddWithValue("@IsMissing", IsMissing);
                cmd.Parameters.AddWithValue("@UploadFileName", UploadFileName);
                cmd.Parameters.AddWithValue("@Size", Size);
                cmd.Parameters.AddWithValue("@FileType", FileType);
                cmd.Parameters.AddWithValue("@SysFileName", SysFileName);
                cmd.Parameters.AddWithValue("@MEDAUTH_FORM", MEDAUTH_FORM);
                cmd.Parameters.AddWithValue("@MEDAUTH_FIELD", MEDAUTH_FIELD);
                cmd.Parameters.AddWithValue("@Unblind", Unblind);
                cmd.Parameters.AddWithValue("@USERNAME", User_Name);
                cmd.Parameters.AddWithValue("@MacAddress", MacAddress);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }






        // SAE PORTAL

        public DataSet SAE_COMMENTS_SP(string ACTION = null, string SAEID = null, string CONTID = null, string RECID = null, string TABLENAME = null, string FIELDNAME = null, string VARIABLENAME = null, string COMMENTS = null, string ENTEREDBY = null, string SAE = null, string SUBJID = null, string INVID = null, string MODULENAME = null, string MODULEID = null, string STATUS = null)
        {
            SqlCommand cmd;
            SqlDataAdapter adp;
            DataSet ds = new DataSet();
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
                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }
                cmd = new SqlCommand("SAE_COMMENTS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SAEID", SAEID);
                cmd.Parameters.AddWithValue("@CONTID", CONTID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                cmd.Parameters.AddWithValue("@ENTEREDBY", ENTEREDBY);
                cmd.Parameters.AddWithValue("@SAE", SAE);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                con.Open();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw;
                con.Close();
            }
            finally
            {
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }


























        public DataSet DM_MODULE_DATA_VIEW_SP(string VISITNUM = null, string SUBJID = null, string MODULEID = null, string TABLENAME = null, string PVID = null, string RECID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DM_MODULE_DATA_VIEW_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
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
                ////ds.Dispose();
            }
            return ds;
        }






























        public DataSet GetQueryReports(string ACTION = null, string Comment = null, string InvId = "", string SubjId = "", string VisitNumber = "", string Page = "", string Module = "", string Field = "", string QueryStatus = "", string QueryType = "", string QRYRESBY = "", string User_ID = null, string QUERYID = null, string PVID = null, string RECID = null, string ID = null, string Project_ID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("Get_Query_Reports", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.Parameters.AddWithValue("@INVID", InvId);
                cmd.Parameters.AddWithValue("@SUBJID", SubjId);
                cmd.Parameters.AddWithValue("@VISITNUM", VisitNumber);
                cmd.Parameters.AddWithValue("@PAGENUM", Page);
                cmd.Parameters.AddWithValue("@MODULENAME", Module);
                cmd.Parameters.AddWithValue("@FIELDNAME", Field);
                cmd.Parameters.AddWithValue("@QUERYSTATUS", QueryStatus);
                cmd.Parameters.AddWithValue("@QUERYTYPE", QueryType);
                cmd.Parameters.AddWithValue("@QRYRESBY", QRYRESBY);
                cmd.Parameters.AddWithValue("@User_ID", HttpContext.Current.Session["User_Id"].ToString());
                cmd.Parameters.AddWithValue("@QUERYID", QUERYID);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Project_ID", Project_ID);

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
















        //Deepak Tank Used Methods
        public DataSet GET_SUBJECT_SP(string INVID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("GET_SUBJECT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INVID", INVID);
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
                ////ds.Dispose();
            }
            return ds;
        }




        public DataSet DB_CODE_SP(string ACTION = null, string ID = null, string MODULEID = null, string AUTO_ID = null,
            string LLTCD = null, string LLTNM = null, string PTCD = null, string PTNM = null, string HLTCD = null, string HLTNM = null, string HLGTCD = null, string HLGTNM = null, string SOCCD = null, string SOCNM = null,
          string CMATC1C = null, string CMATC1CD = null, string CMATC2C = null, string CMATC2CD = null, string CMATC3C = null, string CMATC3CD = null, string CMATC4C = null, string CMATC4CD = null, string CMATC5C = null, string CMATC5CD = null,
        string DICNM = null, string DICNO = null, string SITEID = null, string SUBJID = null, string CritCodeQUERY = null,
        string PVID = null, string RECID = null, string CODE_TYPE = null,
        string Approval = null, string AutoCodedTerm = null, string AUTOCODELIB = null, string ApproveComm = null)
        {
            DataSet dt = new DataSet();
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd = new SqlCommand("DB_CODE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@AUTO_ID", AUTO_ID);

                cmd.Parameters.AddWithValue("@LLTCD", LLTCD);
                cmd.Parameters.AddWithValue("@LLTNM", LLTNM);
                cmd.Parameters.AddWithValue("@PTCD", PTCD);
                cmd.Parameters.AddWithValue("@PTNM", PTNM);
                cmd.Parameters.AddWithValue("@HLTCD", HLTCD);
                cmd.Parameters.AddWithValue("@HLTNM", HLTNM);
                cmd.Parameters.AddWithValue("@HLGTCD", HLGTCD);
                cmd.Parameters.AddWithValue("@HLGTNM", HLGTNM);
                cmd.Parameters.AddWithValue("@SOCCD", SOCCD);
                cmd.Parameters.AddWithValue("@SOCNM", SOCNM);

                cmd.Parameters.AddWithValue("@CMATC1C", CMATC1C);
                cmd.Parameters.AddWithValue("@CMATC1CD", CMATC1CD);
                cmd.Parameters.AddWithValue("@CMATC2C", CMATC2C);
                cmd.Parameters.AddWithValue("@CMATC2CD", CMATC2CD);
                cmd.Parameters.AddWithValue("@CMATC3C", CMATC3C);
                cmd.Parameters.AddWithValue("@CMATC3CD", CMATC3CD);
                cmd.Parameters.AddWithValue("@CMATC4C", CMATC4C);
                cmd.Parameters.AddWithValue("@CMATC4CD", CMATC4CD);
                cmd.Parameters.AddWithValue("@CMATC5C", CMATC5C);
                cmd.Parameters.AddWithValue("@CMATC5CD", CMATC5CD);

                cmd.Parameters.AddWithValue("@DICNM", DICNM);
                cmd.Parameters.AddWithValue("@DICNO", DICNO);

                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);

                cmd.Parameters.AddWithValue("@CritCodeQUERY", CritCodeQUERY);

                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                //cmd.Parameters.AddWithValue("@CODE_TYPE", CODE_TYPE);

                cmd.Parameters.AddWithValue("@AutoCodedTerm", AutoCodedTerm);
                cmd.Parameters.AddWithValue("@Approval", Approval);
                cmd.Parameters.AddWithValue("@AUTOCODELIB", AUTOCODELIB);
                cmd.Parameters.AddWithValue("@ApproveComm", ApproveComm);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                cmd.CommandTimeout = 0;

                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return dt;
        }


        public DataSet CODE_DASH_SP(string ACTION = null)
        {
            DataSet dt = new DataSet();
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd = new SqlCommand("CODE_DASH_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);


                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return dt;
        }

        public DataSet CODE_REPORTS_SP(string ACTION = null)
        {
            DataSet dt = new DataSet();
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

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd = new SqlCommand("CODE_REPORTS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);

                cmd.CommandTimeout = 0;

                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return dt;
        }


    }
}
