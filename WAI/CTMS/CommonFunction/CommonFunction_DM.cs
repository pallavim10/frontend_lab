using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Text;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using PPT;

namespace CTMS.CommonFunction
{
    public class CommonFunction_DM
    {
        DAL_DM dal_DM = new DAL_DM();
        public DataSet GetAuditTrail(string VariableName, string DM_PVID, string DM_RECID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_AUDITTRAIL_SP(ACTION: "GET_AUDITTRAIL_PVID_RECID_VARIABLE", PVID: DM_PVID, RECID: DM_RECID, VARIABLENAME: VariableName);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet Get_ALL_AUDIT_BY_PVID_RECID(string PVID, string RECID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_AUDITTRAIL_SP(ACTION: "Get_ALL_AUDIT_BY_PVID_RECID", PVID: PVID, RECID: RECID);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet GetFieldComments(string VariableName, string DM_PVID, string DM_RECID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_COMMENT_SP(ACTION: "GET_COMMENTS_PVID_RECID_VARIABLENAME", PVID: DM_PVID, VARIABLENAME: VariableName, RECID: DM_RECID);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet GetFieldQuery_OPEN(string VariableName, string DM_PVID, string DM_RECID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_QUERY_SP(ACTION: "GetFieldQuery_OPEN", PVID: DM_PVID, VARIABLENAME: VariableName, RECID: DM_RECID);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet ShowOpenQuery_PVID(string PVID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_QUERY_SP(ACTION: "ShowOpenQuery_PVID",
                    PVID: PVID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet ShowOpenQuery_PVID_RECID(string PVID, string RECID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_QUERY_SP(ACTION: "ShowOpenQuery_PVID_RECID",
                    PVID: PVID,
                    RECID: RECID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet GetFieldQuery_ANS(string VariableName, string DM_PVID, string DM_RECID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_QUERY_SP(ACTION: "GetFieldQuery_ANS", PVID: DM_PVID, VARIABLENAME: VariableName, RECID: DM_RECID);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet ShowAnsQuery_PVID(string PVID)
        {
            DAL_DM dal_DM = new DAL_DM();
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_QUERY_SP(ACTION: "ShowAnsQuery_PVID",
                    PVID: PVID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet ShowAnsQuery_PVID_RECID(string PVID, string RECID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_QUERY_SP(ACTION: "ShowAnsQuery_PVID_RECID",
                    PVID: PVID,
                    RECID: RECID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet GetFieldQuery_CLOSED(string VariableName, string DM_PVID, string DM_RECID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_QUERY_SP(ACTION: "GetFieldQuery_CLOSED", PVID: DM_PVID, VARIABLENAME: VariableName, RECID: DM_RECID);

            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet ShowClosedQuery_PVID_RECID(string PVID, string RECID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_DM.DM_QUERY_SP(ACTION: "ShowClosedQuery_PVID_RECID",
                    PVID: PVID,
                    RECID: RECID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public string DM_Manual_Query_SP(string PVID, string RECID, string SUBJID, string QUERYTEXT, string MODULENAME, string FIELDNAME, string TABLENAME, string VARIABLENAME, string VISITNUM, string VISIT, string MODULEID, string INVID)
        {
            DataSet ds = new DataSet();
            string msg = "";
            try
            {
                dal_DM.DM_Manual_Query_SP(ACTION: "GENERATE_MQ",
                    PVID: PVID,
                    RECID: RECID,
                    SUBJID: SUBJID,
                    INVID: INVID,
                    QUERYTEXT: QUERYTEXT,
                    MODULENAME: MODULENAME,
                    FIELDNAME: FIELDNAME,
                    TABLENAME: TABLENAME,
                    VARIABLENAME: VARIABLENAME,
                    VISITNUM: VISITNUM,
                    VISIT: VISIT,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return msg;
        }

        public DataTable CHECK_MODULE_CRITERIA(string SUBJID, string PVID, string RECID, string VISITID, string MODULEID, string VARIABLENAME, string VALUE)
        {
            DataTable dsRes = new DataTable();
            dsRes.Columns.Add("VISITNAME");
            dsRes.Columns.Add("MODULENAME");

            DAL dal = new DAL();

            try
            {
                DataSet dsCRITs = dal_DM.DM_DATA_IMPACT_WARNING_SP(ACTION: "GET_PAGE_MODULEID_CRITs", MODULEID: MODULEID, VISITID: VISITID);

                if (VARIABLENAME != "" && VALUE != "")
                {
                    for (int i = 0; i < dsCRITs.Tables[0].Rows.Count; i++)
                    {
                        DataSet ds = dal_DM.DM_OnChange_MODULE_CRITERIA_SP(
                            ID: dsCRITs.Tables[0].Rows[i]["ID"].ToString(),
                            SUBJID: SUBJID,
                            VISITNUM: VISITID,
                            VARIABLENAME: VARIABLENAME,
                            VALUE: VALUE
                            );

                        if (!ds.Tables[0].Columns.Contains("RESULT"))
                        {
                            DataRow drNew = dsRes.NewRow();
                            drNew["VISITNAME"] = ds.Tables[0].Rows[0]["VISITNAME"].ToString();
                            drNew["MODULENAME"] = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                            dsRes.Rows.Add(drNew);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dsCRITs.Tables[0].Rows.Count; i++)
                    {
                        DataSet ds = dal_DM.DM_OnChange_MODULE_CRITERIA_SP(
                        ACTION: "Delete",
                        ID: dsCRITs.Tables[0].Rows[i]["ID"].ToString(),
                        SUBJID: SUBJID,
                        VISITNUM: VISITID
                        );

                        if (!ds.Tables[0].Columns.Contains("RESULT"))
                        {
                            DataRow drNew = dsRes.NewRow();
                            drNew["VISITNAME"] = ds.Tables[0].Rows[0]["VISITNAME"].ToString();
                            drNew["MODULENAME"] = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                            dsRes.Rows.Add(drNew);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return dsRes;
        }

        public DataTable CHECK_VISIT_CRITERIA(string SUBJID, string PVID, string RECID, string VISITID, string MODULEID, string VARIABLENAME, string VALUE)
        {
            DataTable dsRes = new DataTable();
            dsRes.Columns.Add("VISITNAME");
            dsRes.Columns.Add("MODULENAME");

            DAL dal = new DAL();

            try
            {
                DataSet dsCRITs = dal_DM.DM_DATA_IMPACT_WARNING_SP(ACTION: "GET_PAGE_VISIT_CRITs", MODULEID: MODULEID, VISITID: VISITID);

                if (VARIABLENAME != "" && VALUE != "")
                {
                    for (int i = 0; i < dsCRITs.Tables[0].Rows.Count; i++)
                    {
                        DataSet ds = dal_DM.DM_OnChange_VISIT_CRITERIA_SP(
                        ID: dsCRITs.Tables[0].Rows[i]["ID"].ToString(),
                        SUBJID: SUBJID,
                        VISITNUM: VISITID,
                        VARIABLENAME: VARIABLENAME,
                        VALUE: VALUE
                        );

                        if (!ds.Tables[0].Columns.Contains("RESULT"))
                        {
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                DataRow drNew = dsRes.NewRow();
                                drNew["VISITNAME"] = ds.Tables[0].Rows[j]["VISITNAME"].ToString();
                                drNew["MODULENAME"] = ds.Tables[0].Rows[j]["MODULENAME"].ToString();
                                dsRes.Rows.Add(drNew);
                            }                            
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dsCRITs.Tables[0].Rows.Count; i++)
                    {
                        DataSet ds = dal_DM.DM_OnChange_VISIT_CRITERIA_SP(
                        ACTION: "Delete",
                        ID: dsCRITs.Tables[0].Rows[i]["ID"].ToString(),
                        SUBJID: SUBJID,
                        VISITNUM: VISITID
                        );


                        if (!ds.Tables[0].Columns.Contains("RESULT"))
                        {
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                DataRow drNew = dsRes.NewRow();
                                drNew["VISITNAME"] = ds.Tables[0].Rows[j]["VISITNAME"].ToString();
                                drNew["MODULENAME"] = ds.Tables[0].Rows[j]["MODULENAME"].ToString();
                                dsRes.Rows.Add(drNew);
                            }                            
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return dsRes;
        }

        public void AutoSet_ESOURCE_PVs(string PVID, string RECID)
        {
            try
            {
                DataSet dsDATA = dal_DM.DM_INSERT_MODULE_DATA_SP(
                    ACTION: "GET_PVDETAILS",
                    PVID: PVID,
                    RECID: RECID
                    );

                bool IsMissing = false;

                DataTable dtVARIABLES = dsDATA.Tables[0];
                DataTable dtDATA = dsDATA.Tables[1];

                DataRow drDATA = dtDATA.Rows[0];

                foreach (DataRow drVARIABLES in dtVARIABLES.Rows)
                {
                    if (!IsMissing)
                    {
                        if (drVARIABLES["REQUIREDYN"].ToString() == "True" && drVARIABLES["ParentVariable"].ToString() == "")
                        {
                            string DATA = drDATA[drVARIABLES["VARIABLENAME"].ToString()].ToString();

                            if (DATA == "")
                            {
                                IsMissing = true;
                            }
                        }
                        else if (drVARIABLES["REQUIREDYN"].ToString() == "True" && drVARIABLES["ParentVariable"].ToString() != "")
                        {
                            string VAL_Child = drVARIABLES["VAL_Child"].ToString();
                            string ANS_Child = drDATA[drVARIABLES["ParentVariable"].ToString()].ToString();

                            if (VAL_Child == ANS_Child)
                            {
                                string DATA = drDATA[drVARIABLES["VARIABLENAME"].ToString()].ToString();

                                if (DATA == "")
                                {
                                    IsMissing = true;
                                }
                            }
                        }
                    }
                }

                dal_DM.DM_INSERT_MODULE_DATA_SP(
                    ACTION: "UPDATE_PVDETAILS",
                    PVID: PVID,
                    RECID: RECID,
                    IsMissing: IsMissing
                    );
            }
            catch (Exception ex)
            {

            }
        }
    }
}