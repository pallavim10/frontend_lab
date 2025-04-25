using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CTMS.CommonFunction
{
    public class DAL_DB
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString);
        SqlDataAdapter adp;

        public string getconstr()
        {
            return ConfigurationManager.ConnectionStrings["PPTConnection"].ConnectionString;
        }

        public DataSet DB_MODULE_SP(string ACTION = null, string MODULEID = null, string MODULENAME = null, bool DM = false, string TABLENAME = null, bool CTMS = false, string CTMS_TABLENAME = null, bool EXT = false, string EXT_TABLENAME = null, bool eSource = false, string eSource_TABLENAME = null, bool PD = false, string PD_TABLENAME = null, bool ePRO = false, string ePRO_TABLENAME = null, bool Safety = false, string Safety_TABLENAME = null, bool IWRS = false, string IWRS_TABLENAME = null, bool eCRF_SignOff = false, bool MultipleYN = false, bool MultipleYN_SAE = false, bool BLINDED = false, bool UNBLINDED = false, bool HelpData = false, string HelpDesc = null, bool SAE_HELPDATA = false, string SAE_HelpDesc = null, bool MEDOP = false, bool DUPLICATE = false, string SEQNO = null, string ID = null, string Descrip = null, string DEFAULTVAL = null, string SUBJID = null, string VISITNUM = null, string DOMAIN = null, string PROJECTID = null, string INDICATION = null, string DEFULTVAL = null, string LIMIT = null, string SYSTEM = null)
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

                cmd = new SqlCommand("DB_MODULE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@DM", DM);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@CTMS", CTMS);
                cmd.Parameters.AddWithValue("@CTMS_TABLENAME", CTMS_TABLENAME);
                cmd.Parameters.AddWithValue("@EXT", EXT);
                cmd.Parameters.AddWithValue("@EXT_TABLENAME", EXT_TABLENAME);
                cmd.Parameters.AddWithValue("@eSource", eSource);
                cmd.Parameters.AddWithValue("@eSource_TABLENAME", eSource_TABLENAME);
                cmd.Parameters.AddWithValue("@PD", PD);
                cmd.Parameters.AddWithValue("@PD_TABLENAME", PD_TABLENAME);
                cmd.Parameters.AddWithValue("@ePRO", ePRO);
                cmd.Parameters.AddWithValue("@ePRO_TABLENAME", ePRO_TABLENAME);
                cmd.Parameters.AddWithValue("@Safety", Safety);
                cmd.Parameters.AddWithValue("@Safety_TABLENAME", Safety_TABLENAME);
                cmd.Parameters.AddWithValue("@IWRS", IWRS);
                cmd.Parameters.AddWithValue("@IWRS_TABLENAME", IWRS_TABLENAME);
                cmd.Parameters.AddWithValue("@eCRF_SignOff", eCRF_SignOff);
                cmd.Parameters.AddWithValue("@MultipleYN", MultipleYN);
                cmd.Parameters.AddWithValue("@MultipleYN_SAE", MultipleYN_SAE);
                cmd.Parameters.AddWithValue("@BLINDED", BLINDED);
                cmd.Parameters.AddWithValue("@UNBLINDED", UNBLINDED);
                cmd.Parameters.AddWithValue("@HelpData", HelpData);
                cmd.Parameters.AddWithValue("@HelpDesc", HelpDesc);
                cmd.Parameters.AddWithValue("@SAE_HELPDATA", SAE_HELPDATA);
                cmd.Parameters.AddWithValue("@SAE_HelpDesc", SAE_HelpDesc);
                cmd.Parameters.AddWithValue("@MEDOP", MEDOP);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                cmd.Parameters.AddWithValue("@DEFAULTVAL", DEFAULTVAL);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@DOMAIN", DOMAIN);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@INDICATION", INDICATION);
                cmd.Parameters.AddWithValue("@DEFULTVAL", DEFULTVAL);
                cmd.Parameters.AddWithValue("@LIMIT", LIMIT);
                cmd.Parameters.AddWithValue("@SYSTEM", SYSTEM);
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

        public DataSet DB_TABLES_SP(string Action = null, string TABLE = null, string OLDTABLE = null, string COLUMN = null, string OLDCOLUMN = null, string DATATYPE = null, string OLDDATATYPE = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_TABLES_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", Action);
                cmd.Parameters.AddWithValue("@TABLE", TABLE);
                cmd.Parameters.AddWithValue("@OLDTABLE", OLDTABLE);
                cmd.Parameters.AddWithValue("@COLUMN", COLUMN);
                cmd.Parameters.AddWithValue("@OLDCOLUMN", OLDCOLUMN);
                cmd.Parameters.AddWithValue("@DATATYPE", DATATYPE);
                cmd.Parameters.AddWithValue("@OLDDATATYPE", OLDDATATYPE);
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

        public DataSet DB_FIELD_SP(string ACTION = null, string MODULEID = null, string MODULENAME = null, string FIELDNAME = null, string VARIABLENAME = null, string Descrip = null, string CONTROLTYPE = null, string FORMAT = null, string SEQNO = null, string MAXLEN = null, string AnsColor = null, string FieldColor = null, bool UPPERCASE = false, bool BOLDYN = false, bool UNLNYN = false, bool READYN = false, bool MULTILINEYN = false, bool REQUIREDYN = false, bool INVISIBLE = false, bool AUTOCODE = false, string AutoCodeLIB = null, bool InList = false, bool InListEditable = false, bool LabData = false, bool AutoNum = false, bool Refer = false, bool Critic_DP = false, bool Prefix = false,
          string PrefixText = null, bool DUPLICATE = false, bool NONREPETATIVE = false, bool MANDATORY = false, string DefaultData = null, bool ParentLinked = false, bool ChildLinked = false, bool MEDOP = false, string ID = null, string VAL_Child = null, string CHILD_ID = null, string SYSTEM = null,string LINKEDMODULEID = null,  string PGL_TYPE = null, bool SDV = false)
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

                cmd = new SqlCommand("DB_FIELD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@FORMAT", FORMAT);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@AnsColor", AnsColor);
                cmd.Parameters.AddWithValue("@FieldColor", FieldColor);
                cmd.Parameters.AddWithValue("@UPPERCASE", UPPERCASE);
                cmd.Parameters.AddWithValue("@BOLDYN", BOLDYN);
                cmd.Parameters.AddWithValue("@UNLNYN", UNLNYN);
                cmd.Parameters.AddWithValue("@READYN", READYN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@REQUIREDYN", REQUIREDYN);
                cmd.Parameters.AddWithValue("@INVISIBLE", INVISIBLE);
                cmd.Parameters.AddWithValue("@AUTOCODE", AUTOCODE);
                cmd.Parameters.AddWithValue("@AutoCodeLIB", AutoCodeLIB);
                cmd.Parameters.AddWithValue("@InList", InList);
                cmd.Parameters.AddWithValue("@InListEditable", InListEditable);
                cmd.Parameters.AddWithValue("@LabData", LabData);
                cmd.Parameters.AddWithValue("@AutoNum", AutoNum);
                cmd.Parameters.AddWithValue("@Refer", Refer);
                cmd.Parameters.AddWithValue("@Critic_DP", Critic_DP);
                cmd.Parameters.AddWithValue("@Prefix", Prefix);
                cmd.Parameters.AddWithValue("@PrefixText", PrefixText);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);
                cmd.Parameters.AddWithValue("@NONREPETATIVE", NONREPETATIVE);
                cmd.Parameters.AddWithValue("@MANDATORY", MANDATORY);
                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);
                cmd.Parameters.AddWithValue("@ParentLinked", ParentLinked);
                cmd.Parameters.AddWithValue("@ChildLinked", ChildLinked);
                cmd.Parameters.AddWithValue("@MEDOP", MEDOP);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VAL_Child", VAL_Child);
                cmd.Parameters.AddWithValue("@CHILD_ID", CHILD_ID);
                cmd.Parameters.AddWithValue("@LINKEDMODULEID", LINKEDMODULEID);
                cmd.Parameters.AddWithValue("@SYSTEM", SYSTEM);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@PGL_TYPE", PGL_TYPE);
                cmd.Parameters.AddWithValue("@SDV", SDV);

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

        public DataSet DB_MASTER_SP(string ACTION = null, string MODULENAME = null, string DOMAIN = null, string FIELDNAME = null, string VARIABLENAME = null, string Descrip = null, string CONTROLTYPE = null, string FORMAT = null, string MODULE_SEQNO = null, string SEQNO = null, string MAXLEN = null, string AnsColor = null, string FieldColor = null, bool UPPERCASE = false, bool BOLDYN = false, bool UNLNYN = false, bool READYN = false, bool MULTILINEYN = false, bool REQUIREDYN = false, bool INVISIBLE = false, bool AUTOCODE = false, string AutoCodeLIB = null, bool InList = false, bool InListEditable = false, bool LabData = false, bool AutoNum = false, bool Refer = false, bool Critic_DP = false, bool Prefix = false, string PrefixText = null, bool DUPLICATE = false, bool NONREPETATIVE = false, bool MANDATORY = false, string DefaultData = null, bool ParentLinked = false, bool ChildLinked = false, bool MEDOP = false, string LINKEDMODULEID = null, string ID = null, string VAL_Child = null, string CHILD_ID = null, string ParentANS = null, string ParentField = null, string ParentVARIABLENAME = null, string OPTIONVALUE = null, string OLDMODULENAME = null, string OLDMODULESEQ = null, string OLDDOMAIN = null)
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

                cmd = new SqlCommand("DB_MASTER_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@DOMAIN", DOMAIN);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@FORMAT", FORMAT);
                cmd.Parameters.AddWithValue("@MODULE_SEQNO", MODULE_SEQNO);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@AnsColor", AnsColor);
                cmd.Parameters.AddWithValue("@FieldColor", FieldColor);
                cmd.Parameters.AddWithValue("@UPPERCASE", UPPERCASE);
                cmd.Parameters.AddWithValue("@BOLDYN", BOLDYN);
                cmd.Parameters.AddWithValue("@UNLNYN", UNLNYN);
                cmd.Parameters.AddWithValue("@READYN", READYN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@REQUIREDYN", REQUIREDYN);
                cmd.Parameters.AddWithValue("@INVISIBLE", INVISIBLE);
                cmd.Parameters.AddWithValue("@AUTOCODE", AUTOCODE);
                cmd.Parameters.AddWithValue("@AutoCodeLIB", AutoCodeLIB);
                cmd.Parameters.AddWithValue("@InList", InList);
                cmd.Parameters.AddWithValue("@InListEditable", InListEditable);
                cmd.Parameters.AddWithValue("@LabData", LabData);
                cmd.Parameters.AddWithValue("@AutoNum", AutoNum);
                cmd.Parameters.AddWithValue("@Refer", Refer);
                cmd.Parameters.AddWithValue("@Critic_DP", Critic_DP);
                cmd.Parameters.AddWithValue("@Prefix", Prefix);
                cmd.Parameters.AddWithValue("@PrefixText", PrefixText);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);
                cmd.Parameters.AddWithValue("@NONREPETATIVE", NONREPETATIVE);
                cmd.Parameters.AddWithValue("@MANDATORY", MANDATORY);
                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);
                cmd.Parameters.AddWithValue("@ParentLinked", ParentLinked);
                cmd.Parameters.AddWithValue("@ChildLinked", ChildLinked);
                cmd.Parameters.AddWithValue("@MEDOP", MEDOP);
                cmd.Parameters.AddWithValue("@LINKED_MODULEID", LINKEDMODULEID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VAL_Child", VAL_Child);
                cmd.Parameters.AddWithValue("@CHILD_ID", CHILD_ID);
                cmd.Parameters.AddWithValue("@ParentANS", ParentANS);
                cmd.Parameters.AddWithValue("@ParentField", ParentField);
                cmd.Parameters.AddWithValue("@ParentVARIABLENAME", ParentVARIABLENAME);
                cmd.Parameters.AddWithValue("@OPTIONVALUE", OPTIONVALUE);
                cmd.Parameters.AddWithValue("@OLDMODULENAME", OLDMODULENAME);
                cmd.Parameters.AddWithValue("@OLDMODULESEQ", OLDMODULESEQ);
                cmd.Parameters.AddWithValue("@OLDDOMAIN", OLDDOMAIN);
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

        public DataSet DB_REVIEW_SP(string ACTION = null, string SYSTEM = null, string MODULEID = null, string FIELDID = null, string VARIABLENAME = null, bool REVIEW = false, string ID = null, string FIELDNAME = null, string REASON = null, bool SEND_TO_REVIEW = false, string LastChangeDate = null, string PreviousChangeDate = null)
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

                cmd = new SqlCommand("DB_REVIEW_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SYSTEM", SYSTEM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@REVIEW", REVIEW);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@REASON", REASON);
                cmd.Parameters.AddWithValue("@SEND_TO_REVIEW", SEND_TO_REVIEW);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@LastChangeDate", LastChangeDate);
                cmd.Parameters.AddWithValue("@PreviousChangeDate", PreviousChangeDate);

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

        public DataSet DB_VISIT_SP(string LATE = null, string WINDOW = null, string EARLY = null, bool PUBLISH_eSOURCE = false, bool PUBLISH_DM = false, bool Repeat = false, string VISITNUM = null, string ID = null, string PROJECTID = null,
            string VISIT = null, bool Unscheduled = false, string ACTION = null, string VISITNUM_DEP = null, string MODULEID_DEP = null, string FIELDID_DEP = null, bool SUBJID_PROG = false, string VISITNUM_PROG = null, string MODULEID_PROG = null, string FIELDID_PROG = null)
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

                cmd = new SqlCommand("DB_VISIT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@PROJECTID", PROJECTID);
                cmd.Parameters.AddWithValue("@LATE", LATE);
                cmd.Parameters.AddWithValue("@WINDOW", WINDOW);
                cmd.Parameters.AddWithValue("@EARLY", EARLY);
                cmd.Parameters.AddWithValue("@PUBLISH_eSOURCE", PUBLISH_eSOURCE);
                cmd.Parameters.AddWithValue("@PUBLISH_DM", PUBLISH_DM);
                cmd.Parameters.AddWithValue("@Repeat", Repeat);
                cmd.Parameters.AddWithValue("@SUBJID_PROG", SUBJID_PROG);
                cmd.Parameters.AddWithValue("@Unscheduled", Unscheduled);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@VISITNUM_DEP", VISITNUM_DEP);
                cmd.Parameters.AddWithValue("@MODULEID_DEP", MODULEID_DEP);
                cmd.Parameters.AddWithValue("@FIELDID_DEP", FIELDID_DEP);
                cmd.Parameters.AddWithValue("@VISITNUM_PROG", VISITNUM_PROG);
                cmd.Parameters.AddWithValue("@MODULEID_PROG", MODULEID_PROG);
                cmd.Parameters.AddWithValue("@FIELDID_PROG", FIELDID_PROG);
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

        public DataSet DB_MAP_SP(string ACTION = null, string MODULEID = null, string ID = null, string SHEET_NAME = null, string SHEET_COLUMN = null, string FIELDNAME = null, string Cur_ANSWERS = null, string VISIT = null, string VISITNUM = null, string INVID = null, string SUBJID = null, string MODULENAME = null, string TABLENAME = null, string PVID = null, string RECID = null, string INSERTQUERY = null, string UPDATEQUERY = null, string PRIMQUERY = null)
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

                cmd = new SqlCommand("DB_MAP_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SHEET_NAME", SHEET_NAME);
                cmd.Parameters.AddWithValue("@SHEET_COLUMN", SHEET_COLUMN);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@Cur_ANSWERS", Cur_ANSWERS);
                cmd.Parameters.AddWithValue("@VISIT", VISIT);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@INVID", INVID);
                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@PVID", PVID);
                cmd.Parameters.AddWithValue("@RECID", RECID);
                cmd.Parameters.AddWithValue("@INSERTQUERY", INSERTQUERY);
                cmd.Parameters.AddWithValue("@UPDATEQUERY", UPDATEQUERY);
                cmd.Parameters.AddWithValue("@PRIMQUERY", PRIMQUERY);

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

        public DataSet DB_SETUP_CRITs_SP(
            string ACTION = null, string AndOr1 = null, string AndOr2 = null, string AndOr3 = null, string AndOr4 = null,
            string Field1 = null, string Field2 = null, string Field3 = null, string Field4 = null, string Field5 = null,
            string MODULEID1 = null, string MODULEID2 = null, string MODULEID3 = null, string MODULEID4 = null, string MODULEID5 = null,
            string VISIT1 = null, string VISIT2 = null, string VISIT3 = null, string VISIT4 = null, string VISIT5 = null,
            string CONDITION1 = null, string Condition2 = null, string Condition3 = null, string Condition4 = null, string Condition5 = null,
            string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null,
            string CritName = null, string Criteria = null, string CritCode = null,
            string ERR_MSG = null, string FIELDID = null, string FIELDNAME = null,
            string ID = null, string MODULEID = null, string MODULENAME = null, string TABLENAME = null, string VARIABLENAME = null,
            string VISITNUMVAL = null,string SEQNO = null, string VISITNUM = null, string DERIVED = null, string Formula = null, string Condition = null,
            string Criteria_ID = null, string SUBJID = null, bool ALLOWABLE = false, bool Restricted = false, string Name = null,
            bool SETVALUE = false, string SETFIELDID = null, string SETVALUEDATA = null, bool ISDERIVED = false, string ISDERIVED_VALUE = null, string EMAIL_SUBJECT = null, string EMAIL_BODY = null, string ACTIONS = null, string SITEID = null, string EMAILIDS = null, string CCEMAILIDS = null, string BCCEMAILIDS = null)
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

                cmd = new SqlCommand("DB_SETUP_CRITs_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@AndOr1", AndOr1);
                cmd.Parameters.AddWithValue("@AndOr2", AndOr2);
                cmd.Parameters.AddWithValue("@AndOr3", AndOr3);
                cmd.Parameters.AddWithValue("@AndOr4", AndOr4);

                cmd.Parameters.AddWithValue("@Field1", Field1);
                cmd.Parameters.AddWithValue("@Field2", Field2);
                cmd.Parameters.AddWithValue("@Field3", Field3);
                cmd.Parameters.AddWithValue("@Field4", Field4);
                cmd.Parameters.AddWithValue("@Field5", Field5);

                cmd.Parameters.AddWithValue("@CONDITION1", CONDITION1);
                cmd.Parameters.AddWithValue("@Condition2", Condition2);
                cmd.Parameters.AddWithValue("@Condition3", Condition3);
                cmd.Parameters.AddWithValue("@Condition4", Condition4);
                cmd.Parameters.AddWithValue("@Condition5", Condition5);
                cmd.Parameters.AddWithValue("@Value1", Value1);
                cmd.Parameters.AddWithValue("@Value2", Value2);
                cmd.Parameters.AddWithValue("@Value3", Value3);
                cmd.Parameters.AddWithValue("@Value4", Value4);
                cmd.Parameters.AddWithValue("@Value5", Value5);
                cmd.Parameters.AddWithValue("@CritCode", CritCode);
                cmd.Parameters.AddWithValue("@Criteria", Criteria);
                cmd.Parameters.AddWithValue("@CritName", CritName);
                cmd.Parameters.AddWithValue("@ERR_MSG", ERR_MSG);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@DERIVED", DERIVED);
                cmd.Parameters.AddWithValue("@Formula", Formula);
                cmd.Parameters.AddWithValue("@Condition", Condition);
                cmd.Parameters.AddWithValue("@Criteria_ID", Criteria_ID);

                cmd.Parameters.AddWithValue("@SUBJID", SUBJID);
                cmd.Parameters.AddWithValue("@ALLOWABLE", ALLOWABLE);
                cmd.Parameters.AddWithValue("@Restricted", Restricted);

                cmd.Parameters.AddWithValue("@VISIT1", VISIT1);
                cmd.Parameters.AddWithValue("@VISIT2", VISIT2);
                cmd.Parameters.AddWithValue("@VISIT3", VISIT3);
                cmd.Parameters.AddWithValue("@VISIT4", VISIT4);
                cmd.Parameters.AddWithValue("@VISIT5", VISIT5);

                cmd.Parameters.AddWithValue("@MODULEID1", MODULEID1);
                cmd.Parameters.AddWithValue("@MODULEID2", MODULEID2);
                cmd.Parameters.AddWithValue("@MODULEID3", MODULEID3);
                cmd.Parameters.AddWithValue("@MODULEID4", MODULEID4);
                cmd.Parameters.AddWithValue("@MODULEID5", MODULEID5);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@SETVALUE", SETVALUE);
                cmd.Parameters.AddWithValue("@SETFIELDID", SETFIELDID);
                cmd.Parameters.AddWithValue("@SETVALUEDATA", SETVALUEDATA);
                cmd.Parameters.AddWithValue("@ISDERIVED", ISDERIVED);
                cmd.Parameters.AddWithValue("@ISDERIVED_VALUE", ISDERIVED_VALUE);

                cmd.Parameters.AddWithValue("@EMAIL_SUBJECT", EMAIL_SUBJECT);
                cmd.Parameters.AddWithValue("@EMAIL_BODY", EMAIL_BODY);
                cmd.Parameters.AddWithValue("@ACTIONS", ACTIONS);

                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@EMAILIDS", EMAILIDS);
                cmd.Parameters.AddWithValue("@CCEMAILIDS", CCEMAILIDS);
                cmd.Parameters.AddWithValue("@BCCEMAILIDS", BCCEMAILIDS);
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

            }
            return ds;
        }

        public DataSet DB_DM_SP(string ACTION = null, string MODULEID = null, string MODULENAME = null, string FIELDNAME = null, string VARIABLENAME = null, string Descrip = null, string CONTROLTYPE = null, string FORMAT = null, string SEQNO = null, string MAXLEN = null, string AnsColor = null, string FieldColor = null, bool UPPERCASE = false, bool BOLDYN = false, bool UNLNYN = false, bool READYN = false, bool MULTILINEYN = false, bool REQUIREDYN = false, bool INVISIBLE = false, bool AUTOCODE = false, string AutoCodeLIB = null, bool InList = false, bool InListEditable = false, bool LabData = false, bool AutoNum = false, bool Refer = false, bool Critic_DP = false, bool Prefix = false,
          string PrefixText = null, bool DUPLICATE = false, bool NONREPETATIVE = false, bool MANDATORY = false, string DefaultData = null, bool ParentLinked = false, bool ChildLinked = false, string LINKEDMODULEID = null, bool MEDOP = false, string ID = null, string DEFAULTVAL = null, string VISITNUM = null, string ParentField = null, string ParentANS = null, string ParentVARIABLENAME = null, string OLD_VARIABLENAME = null, string OLD_OPTIONTEXT = null, bool chk_eSourceModule = false, bool chk_eSOURCE_FIELD = false, string PGL_TYPE = null, bool SDV=false)
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

                cmd = new SqlCommand("DB_DM_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@FORMAT", FORMAT);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@AnsColor", AnsColor);
                cmd.Parameters.AddWithValue("@FieldColor", FieldColor);
                cmd.Parameters.AddWithValue("@UPPERCASE", UPPERCASE);
                cmd.Parameters.AddWithValue("@BOLDYN", BOLDYN);
                cmd.Parameters.AddWithValue("@UNLNYN", UNLNYN);
                cmd.Parameters.AddWithValue("@READYN", READYN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@REQUIREDYN", REQUIREDYN);
                cmd.Parameters.AddWithValue("@INVISIBLE", INVISIBLE);
                cmd.Parameters.AddWithValue("@AUTOCODE", AUTOCODE);
                cmd.Parameters.AddWithValue("@AutoCodeLIB", AutoCodeLIB);
                cmd.Parameters.AddWithValue("@InList", InList);
                cmd.Parameters.AddWithValue("@InListEditable", InListEditable);
                cmd.Parameters.AddWithValue("@LabData", LabData);
                cmd.Parameters.AddWithValue("@AutoNum", AutoNum);
                cmd.Parameters.AddWithValue("@Refer", Refer);
                cmd.Parameters.AddWithValue("@Critic_DP", Critic_DP);
                cmd.Parameters.AddWithValue("@Prefix", Prefix);
                cmd.Parameters.AddWithValue("@PrefixText", PrefixText);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);
                cmd.Parameters.AddWithValue("@NONREPETATIVE", NONREPETATIVE);
                cmd.Parameters.AddWithValue("@MANDATORY", MANDATORY);
                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);
                cmd.Parameters.AddWithValue("@ParentLinked", ParentLinked);
                cmd.Parameters.AddWithValue("@ChildLinked", ChildLinked);
                cmd.Parameters.AddWithValue("@LINKEDMODULE", LINKEDMODULEID);
                cmd.Parameters.AddWithValue("@MEDOP", MEDOP);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@DEFAULTVAL", DEFAULTVAL);
                cmd.Parameters.AddWithValue("@ParentANS", ParentANS);
                cmd.Parameters.AddWithValue("@ParentField", ParentField);
                cmd.Parameters.AddWithValue("@ParentVARIABLENAME", ParentVARIABLENAME);
                cmd.Parameters.AddWithValue("@OLD_VARIABLENAME", OLD_VARIABLENAME);
                cmd.Parameters.AddWithValue("@OLD_OPTIONTEXT", OLD_OPTIONTEXT);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@PGL_TYPE", PGL_TYPE);
                cmd.Parameters.AddWithValue("@SDV", SDV);

                if (HttpContext.Current.Session["TimeZone_Value"] == null)
                {
                    TZ_VAL = "+05:30";
                }
                else
                {
                    TZ_VAL = HttpContext.Current.Session["TimeZone_Value"].ToString();
                }

                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@eSOURCE_MODULE", chk_eSourceModule);
                cmd.Parameters.AddWithValue("@eSOURCE_FIELD", chk_eSOURCE_FIELD); 
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

        public DataSet DB_DRP_SP(string ACTION = null, string MODULEID = null, string VARIABLENAME = null, string ParentANS = null, string ParentField = null, string ParentVARIABLENAME = null, string SEQNO = null, string DEFAULTVAL = null, string CONTROLTYPE = null, string ID = null, string OLD_VARIABLENAME = null, string PGL_TYPE = null,string FIELDID = null)
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

                cmd = new SqlCommand("DB_DRP_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@ParentANS", ParentANS);
                cmd.Parameters.AddWithValue("@ParentField", ParentField);
                cmd.Parameters.AddWithValue("@ParentVARIABLENAME", ParentVARIABLENAME);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@DEFAULTVAL", DEFAULTVAL);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@OLD_VARIABLENAME", OLD_VARIABLENAME);
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
                cmd.Parameters.AddWithValue("@PGL_TYPE", PGL_TYPE);

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

        public DataSet DB_UPLOAD_SP(string ACTION = null, string MODULEID = null, string MODULENAME = null, string DOMAIN = null, string MODULE_SEQNO = null, string FIELDNAME = null, string VARIABLENAME = null, string Descrip = null, string CONTROLTYPE = null, string FORMAT = null, string SEQNO = null, string MAXLEN = null, string AnsColor = null, string FieldColor = null, bool UPPERCASE = false, bool BOLDYN = false, bool UNLNYN = false, bool READYN = false, bool MULTILINEYN = false, bool REQUIREDYN = false, bool INVISIBLE = false, bool AUTOCODE = false, string AutoCodeLIB = null, bool InList = false, bool InListEditable = false, bool LabData = false, bool AutoNum = false, bool Refer = false, bool Critic_DP = false, bool Prefix = false, string PrefixText = null, bool DUPLICATE = false, bool NONREPETATIVE = false, bool MANDATORY = false, string DefaultData = null, bool ParentLinked = false, bool ChildLinked = false, bool MEDOP = false, string ID = null, string VAL_Child = null, string CHILD_ID = null, string SITEID = null, string LABID = null, string LABNAME = null, string PGL_TYPE = null, bool SDV = false, string FileName = null, string ContentType = null, byte[] fileData = null, string NEV_MENU_NAME = null)
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

                cmd = new SqlCommand("DB_UPLOAD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@MODULENAME", MODULENAME);
                cmd.Parameters.AddWithValue("@DOMAIN", DOMAIN);
                cmd.Parameters.AddWithValue("@MODULE_SEQNO", MODULE_SEQNO);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                cmd.Parameters.AddWithValue("@CONTROLTYPE", CONTROLTYPE);
                cmd.Parameters.AddWithValue("@FORMAT", FORMAT);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@MAXLEN", MAXLEN);
                cmd.Parameters.AddWithValue("@AnsColor", AnsColor);
                cmd.Parameters.AddWithValue("@FieldColor", FieldColor);
                cmd.Parameters.AddWithValue("@UPPERCASE", UPPERCASE);
                cmd.Parameters.AddWithValue("@BOLDYN", BOLDYN);
                cmd.Parameters.AddWithValue("@UNLNYN", UNLNYN);
                cmd.Parameters.AddWithValue("@READYN", READYN);
                cmd.Parameters.AddWithValue("@MULTILINEYN", MULTILINEYN);
                cmd.Parameters.AddWithValue("@REQUIREDYN", REQUIREDYN);
                cmd.Parameters.AddWithValue("@INVISIBLE", INVISIBLE);
                cmd.Parameters.AddWithValue("@AUTOCODE", AUTOCODE);
                cmd.Parameters.AddWithValue("@AutoCodeLIB", AutoCodeLIB);
                cmd.Parameters.AddWithValue("@InList", InList);
                cmd.Parameters.AddWithValue("@InListEditable", InListEditable);
                cmd.Parameters.AddWithValue("@LabData", LabData);
                cmd.Parameters.AddWithValue("@AutoNum", AutoNum);
                cmd.Parameters.AddWithValue("@Refer", Refer);
                cmd.Parameters.AddWithValue("@Critic_DP", Critic_DP);
                cmd.Parameters.AddWithValue("@Prefix", Prefix);
                cmd.Parameters.AddWithValue("@PrefixText", PrefixText);
                cmd.Parameters.AddWithValue("@DUPLICATE", DUPLICATE);
                cmd.Parameters.AddWithValue("@NONREPETATIVE", NONREPETATIVE);
                cmd.Parameters.AddWithValue("@MANDATORY", MANDATORY);
                cmd.Parameters.AddWithValue("@DefaultData", DefaultData);
                cmd.Parameters.AddWithValue("@ParentLinked", ParentLinked);
                cmd.Parameters.AddWithValue("@ChildLinked", ChildLinked);
                cmd.Parameters.AddWithValue("@MEDOP", MEDOP);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VAL_Child", VAL_Child);
                cmd.Parameters.AddWithValue("@CHILD_ID", CHILD_ID);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@LABID", LABID);
                cmd.Parameters.AddWithValue("@LABNAME", LABNAME);
                cmd.Parameters.AddWithValue("@PGL_TYPE", PGL_TYPE);
                cmd.Parameters.AddWithValue("@SDV", SDV);
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@ContentType", ContentType);
                cmd.Parameters.AddWithValue("@fileData", fileData);
                cmd.Parameters.AddWithValue("@NEV_MENU_NAME", NEV_MENU_NAME);
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
                //ds.Dispose();
            }
            return ds;
        }

        public DataSet DB_DOWNLOAD_LOGS_SP(string ACTION = null, string FIELNAME = null, string PAGENAME = null, string FUNCTIONNAME = null)
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

                cmd = new SqlCommand("DB_DOWNLOAD_LOGS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@FIELNAME", FIELNAME);
                cmd.Parameters.AddWithValue("@PAGENAME", PAGENAME);
                cmd.Parameters.AddWithValue("@FUNCTIONNAME", FUNCTIONNAME);
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

        public DataSet DB_RULE_SP(string ACTION = null, string VISITNUM = null, string MODULEID = null, string FIELDID = null, string RULEID = null, string NATURE = null, bool GEN_QUERY = false, bool SET_VALUE = false, string SET_VALUE_FORMULA = null, string Description = null, string QueryText = null, string SEQNO = null, string ID = null, string VARIABLENAME_DEF = null, string DERIVED = null, string FORMULA = null, string CONDITION = null, string TESTED = null)
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

                cmd = new SqlCommand("DB_RULE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@RULEID", RULEID);
                cmd.Parameters.AddWithValue("@NATURE", NATURE);
                cmd.Parameters.AddWithValue("@GEN_QUERY", GEN_QUERY);
                cmd.Parameters.AddWithValue("@SET_VALUE", SET_VALUE);
                cmd.Parameters.AddWithValue("@SET_VALUE_FORMULA", SET_VALUE_FORMULA);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@QueryText", QueryText);
                cmd.Parameters.AddWithValue("@SEQNO", SEQNO);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VARIABLENAME_DEF", VARIABLENAME_DEF);
                cmd.Parameters.AddWithValue("@DERIVED", DERIVED);
                cmd.Parameters.AddWithValue("@FORMULA", FORMULA);
                cmd.Parameters.AddWithValue("@CONDITION", CONDITION);
                cmd.Parameters.AddWithValue("@TESTED", TESTED);
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

        public DataTable MEDDDRA_UPLOAD_SP(string ACTION = null, string llt_code = null, string llt_name = null, string pt_code = null, string pt_name = null, string hlt_code = null, string hlt_name = null, string hlgt_code = null, string hlgt_name = null, string soc_code = null, string soc_name = null, string primary_soc_fg = null, string CMATC1C = null, string CMATC1CD = null, string CMATC2C = null, string CMATC2CD = null, string CMATC3C = null, string CMATC3CD = null, string CMATC4C = null, string CMATC4CD = null, string MEDRAVersionNo = null, string WHODVersionNo = null, string CMATC5C = null, string CMATC5CD = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd;
            SqlDataAdapter adp;
            string query = "";
            try
            {
                cmd = new SqlCommand("MEDDDRADATA_Libraries_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@llt_code", llt_code);
                cmd.Parameters.AddWithValue("@llt_name", llt_name);
                cmd.Parameters.AddWithValue("@pt_code", pt_code);
                cmd.Parameters.AddWithValue("@pt_name", pt_name);
                cmd.Parameters.AddWithValue("@hlt_code", hlt_code);
                cmd.Parameters.AddWithValue("@hlt_name", hlt_name);
                cmd.Parameters.AddWithValue("@hlgt_code", hlgt_code);
                cmd.Parameters.AddWithValue("@hlgt_name", hlgt_name);
                cmd.Parameters.AddWithValue("@soc_code", soc_code);
                cmd.Parameters.AddWithValue("@soc_name", soc_name);
                cmd.Parameters.AddWithValue("@primary_soc_fg", primary_soc_fg);
                cmd.Parameters.AddWithValue("@MEDRAVersionNo", MEDRAVersionNo);

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
                cmd.Parameters.AddWithValue("@WHODVersionNo", WHODVersionNo);

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

        public DataSet DB_LAB_REFERENCE_SP(string ACTION = null, string SITEID = null, string LABID = null, string LABNAME = null, string TESTNAME = null, string SEX = null, string AGELOW = null, string AGEHI = null, string REFRANGELO = null, string REFRANGEHI = null, string UNIT = null, string FROMDATE = null, string ENDDATE = null, string ID = null, string VISITNUM = null, string MODULEID = null, string FIELDID = null, string PRIMARY_VISITNUM = null, string PRIMARY_MODULEID = null, string PRIMARY_FIELDID = null, string SECONDARY_MODULEID = null, string SECONDARY_FIELDID = null, string AGE_UNIT = null, string LABID_FIELDID = null, string LL_FIELDID = null, string UL_FIELDID = null, string UNIT_FIELDID = null, string TESTNAME_FIELDID = null, string SEX_MODULEID = null, string SEX_FIELDID = null, string TABLENAME = null)
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

                cmd = new SqlCommand("DB_LAB_REFERENCE_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SITEID", SITEID);
                cmd.Parameters.AddWithValue("@LABID", LABID);
                cmd.Parameters.AddWithValue("@LABNAME", LABNAME);
                cmd.Parameters.AddWithValue("@TESTNAME", TESTNAME);
                cmd.Parameters.AddWithValue("@SEX", SEX);
                cmd.Parameters.AddWithValue("@AGELOW", AGELOW);
                cmd.Parameters.AddWithValue("@AGEHI", AGEHI);
                cmd.Parameters.AddWithValue("@REFRANGELO", REFRANGELO);
                cmd.Parameters.AddWithValue("@REFRANGEHI", REFRANGEHI);
                cmd.Parameters.AddWithValue("@UNIT", UNIT);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE);
                cmd.Parameters.AddWithValue("@ENDDATE", ENDDATE);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@PRIMARY_VISITNUM", PRIMARY_VISITNUM);
                cmd.Parameters.AddWithValue("@PRIMARY_MODULEID", PRIMARY_MODULEID);
                cmd.Parameters.AddWithValue("@PRIMARY_FIELDID", PRIMARY_FIELDID);
                cmd.Parameters.AddWithValue("@SECONDARY_MODULEID", SECONDARY_MODULEID);
                cmd.Parameters.AddWithValue("@SECONDARY_FIELDID", SECONDARY_FIELDID);
                cmd.Parameters.AddWithValue("@AGE_UNIT", AGE_UNIT);
                cmd.Parameters.AddWithValue("@LABID_FIELDID", LABID_FIELDID);
                cmd.Parameters.AddWithValue("@LL_FIELDID", LL_FIELDID);
                cmd.Parameters.AddWithValue("@UL_FIELDID", UL_FIELDID);
                cmd.Parameters.AddWithValue("@UNIT_FIELDID", UNIT_FIELDID);
                cmd.Parameters.AddWithValue("@TESTNAME_FIELDID", TESTNAME_FIELDID);
                cmd.Parameters.AddWithValue("@SEX_MODULEID", SEX_MODULEID);
                cmd.Parameters.AddWithValue("@SEX_FIELDID", SEX_FIELDID);
                cmd.Parameters.AddWithValue("@TABLENAME", TABLENAME);
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
            return ds;
        }

        public DataSet DB_EMAIL_REVIEW_SP(string ACTION = null, string ID = null, string EMAILID = null, string CC_EMAILID = null, string BCC_EMAILID = null, string EMAIL_BODY = null, string EMAIL_SUBJECT = null, string ACTIVITY = null, string SYSTEMS = null)
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

                cmd = new SqlCommand("DB_EMAIL_REVIEW_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
                cmd.Parameters.AddWithValue("@CC_EMAILID", CC_EMAILID);
                cmd.Parameters.AddWithValue("@BCC_EMAILID", BCC_EMAILID);
                cmd.Parameters.AddWithValue("@EMAIL_BODY", EMAIL_BODY);
                cmd.Parameters.AddWithValue("@EMAIL_SUBJECT", EMAIL_SUBJECT);
                cmd.Parameters.AddWithValue("@SYSTEMS", SYSTEMS);
                cmd.Parameters.AddWithValue("@ACTIVITY", ACTIVITY);
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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DB_ANNOTATED_PRINT_SP(string ACTION = null, string SYSTEM = null, string MODULEID = null, string FIELDID = null, string VARIABLENAME = null, string ID = null, string FIELDNAME = null, string OPTIONS = null)
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

                cmd = new SqlCommand("DB_ANNOTATED_PRINT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SYSTEM", SYSTEM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@OPTIONS", OPTIONS);
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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DB_LIST_SP(string Action = null, string ID = null, string LISTING_ID = null, string FIELDNAME = null,
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

                cmd = new SqlCommand("DB_LIST_SP", con);
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

        public DataSet DB_VISIT_ANNOTATED_PRINT_SP(string ACTION = null, string VISITNUM = null, string MODULEID = null, string FIELDID = null, string VARIABLENAME = null, string ID = null, string FIELDNAME = null, string OPTIONS = null,string SYSTEM=null)
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

                cmd = new SqlCommand("DB_VISIT_ANNOTATED_PRINT_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@VISITNUM", VISITNUM);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@FIELDNAME", FIELDNAME);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@OPTIONS", OPTIONS);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
                cmd.Parameters.AddWithValue("@SYSTEM", SYSTEM);

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

        public DataSet DB_DASHBORAD_SP(string ACTION = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                string USERID = null;
                if (HttpContext.Current.Session["USER_ID"] != null)
                {
                    USERID = HttpContext.Current.Session["USER_ID"].ToString();
                }

                cmd = new SqlCommand("DB_DASHBORAD_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
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
                cmd = null;
                adp = null;
                con.Close();
            }
            return ds;
        }

        public DataSet DB_DM_AUDIT_TRAIL_SP(string ID = null, string ACTION = null, string TABLENAME = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_AUDIT_TRAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
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

        public DataSet DB_ACTIVITY_AUDIT_TRAIL_SP(string ACTION = null, string MODULEID = null, string DOMAIN = null, string FIELDID = null, string VARIABLENAME = null, string LABID = null, string RULEID = null, string LISTINGID = null, string Listing_Crit = null, string VISITID = null)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_ACTIVITY_AUDIT_TRAIL_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MODULEID", MODULEID);
                cmd.Parameters.AddWithValue("@DOMAIN", DOMAIN);
                cmd.Parameters.AddWithValue("@FIELDID", FIELDID);
                cmd.Parameters.AddWithValue("@VARIABLENAME", VARIABLENAME);
                cmd.Parameters.AddWithValue("@LABID", LABID);
                cmd.Parameters.AddWithValue("@RULEID", RULEID);
                cmd.Parameters.AddWithValue("@LISTINGID", LISTINGID);
                cmd.Parameters.AddWithValue("@VISITID", VISITID);
                cmd.Parameters.AddWithValue("@Listing_Crit", Listing_Crit);

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


        public DataTable DB_Formula(string ACTION = null, string ID= null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd;
            SqlDataAdapter adp;
            try
            {
                cmd = new SqlCommand("DB_SETUP_CRITs_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
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
                cmd = null;
                adp = null;
                con.Close();
            }
            return dt;            
        }


        public DataSet DB_STATUS_SP(string ACTION = null, string ID = null, string DB_VERSION = null, string LAST_DB_VERSION = null, string MIGRATION = null)
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

                cmd = new SqlCommand("DB_STATUS_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@DB_VERSION", DB_VERSION);
                cmd.Parameters.AddWithValue("@LAST_DB_VERSION", LAST_DB_VERSION);
                cmd.Parameters.AddWithValue("@MIGRATION", MIGRATION);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@TZ_VAL", TZ_VAL);
                cmd.Parameters.AddWithValue("@User_Name", User_Name);

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

    }
}