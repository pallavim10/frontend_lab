using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CTMS.CommonFunction
{
    public class CommonFunction_SAE
    {
        DAL_SAE dal_SAE = new DAL_SAE();

        public DataSet SAE_GetAuditTrail(string VARIABLENAME, string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_AUDITTRAIL_SP(ACTION: "GET_AUDITTRAIL_SAEID_RECID_VARIABLE",
                    SAEID: SAEID,
                    RECID: RECID,
                    VARIABLENAME: VARIABLENAME,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_GetAuditTrail_MR(string VARIABLENAME, string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_AUDITTRAIL_SP(ACTION: "GET_AUDITTRAIL_SAEID_RECID_VARIABLE_MR",
                    SAEID: SAEID,
                    RECID: RECID,
                    VARIABLENAME: VARIABLENAME,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_GetFieldComments(string VARIABLENAME, string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_COMMENT_SP(ACTION: "GET_COMMENTS_SAEID_RECID_VARIABLENAME",
                    VARIABLENAME: VARIABLENAME,
                    SAEID: SAEID,
                    RECID: RECID,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_GetFieldQuery_OPEN(string VARIABLENAME, string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(ACTION: "SAE_GetFieldQuery_OPEN",
                    SAEID: SAEID,
                    RECID: RECID,
                    VARIABLENAME: VARIABLENAME,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_GetFieldQuery_ANS(string VARIABLENAME, string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(ACTION: "SAE_GetFieldQuery_ANS",
                    SAEID: SAEID,
                    RECID: RECID,
                    VARIABLENAME: VARIABLENAME,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_GetFieldQuery_CLOSED(string VARIABLENAME, string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(ACTION: "SAE_GetFieldQuery_CLOSED",
                    SAEID: SAEID,
                    RECID: RECID,
                    VARIABLENAME: VARIABLENAME,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_GET_QUERY_COMMENT(string ID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(ACTION: "SAE_GET_QUERY_COMMENT", ID: ID);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_Update_Comment_Status(string ID, string Comment)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(ACTION: "SAE_Update_Comment_Status",
                    ID: ID,
                    Comment: Comment
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_Update_Comment_Status_ReadOnly(string ID, string Comment, string Reason, string QUERYTEXT)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(ACTION: "SAE_Update_Comment_Status_ReadOnly",
                    ID: ID,
                    Comment: Comment,
                    REASON: Reason,
                    QUERYTEXT: QUERYTEXT
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_Show_Page_Comments(string SAEID, string MODULEID)
        {
            DataSet ds = new DataSet();

            try
            {
                ds = dal_SAE.SAE_COMMENT_SP(
                        ACTION: "GET_PAGE_COMMENT_SAEID",
                        SAEID: SAEID,
                        MODULEID: MODULEID
                        );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_Show_Internal_Comments(string SAEID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_COMMENT_SP(
                         ACTION: "GET_INTERNAL_COMMENT_SAEID",
                         SAEID: SAEID,
                         MODULEID: MODULEID
                         );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_ShowOpenQuery_SAEID(string SAEID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(
                    ACTION: "SAE_ShowOpenQuery_SAEID",
                    SAEID: SAEID,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_ShowOpenQuery_SAEID_RECID(string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(
                    ACTION: "SAE_ShowOpenQuery_SAEID_RECID",
                    SAEID: SAEID,
                    RECID: RECID,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_ShowAnsQuery_SAEID_RECID(string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(
                    ACTION: "SAE_ShowAnsQuery_SAEID_RECID",
                    SAEID: SAEID,
                    RECID: RECID,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_ShowAnsQuery_SAEID(string SAEID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(
                    ACTION: "SAE_ShowAnsQuery_SAEID",
                    SAEID: SAEID,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet SAE_ShowClosedQuery_SAEID_RECID(string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_QUERY_SP(
                    ACTION: "SAE_ShowClosedQuery_SAEID_RECID",
                    SAEID: SAEID,
                    RECID: RECID,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet Get_ALL_AUDIT_BY_SAEID_RECID(string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_AUDITTRAIL_SP(
                    ACTION: "Get_ALL_AUDIT_BY_SAEID_RECID",
                    SAEID: SAEID,
                    RECID: RECID,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet Get_ALL_AUDIT_BY_SAEID_RECID_MR(string SAEID, string RECID, string MODULEID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_AUDITTRAIL_SP(
                    ACTION: "Get_ALL_AUDIT_BY_SAEID_RECID_MR",
                    SAEID: SAEID,
                    RECID: RECID,
                    MODULEID: MODULEID
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet INSERT_MANUAL_QUERY(string SAEID, string RECID, string SUBJID, string QUERYTEXT, string MODULENAME, string MODULEID, string FIELDNAME, string TABLENAME, string VARIABLENAME)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = dal_SAE.SAE_Manual_Query_SP(
                    ACTION: "INSERT_MQ",
                    SAEID: SAEID,
                    RECID: RECID,
                    SUBJID: SUBJID,
                    QUERYTEXT: QUERYTEXT,
                    MODULENAME: MODULENAME,
                    MODULEID: MODULEID,
                    FIELDNAME: FIELDNAME,
                    TABLENAME: TABLENAME,
                    VARIABLENAME: VARIABLENAME
                    );
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
    }
}