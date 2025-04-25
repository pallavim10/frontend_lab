using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CTMS
{
    public partial class DM_RunRules : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillINV();
                    FillSubject();
                    FillVisit();
                    FillModules();
                    FillFields();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.GET_INVID_SP();
            ddlInvid.DataSource = ds.Tables[0];
            ddlInvid.DataValueField = "INVNAME";
            ddlInvid.DataBind();
            ddlInvid.Items.Insert(0, new ListItem("--All--", "0"));

            FillSubject();
        }

        private void FillVisit()
        {
            try
            {
                DataSet ds = dal.DM_RUN_RULE(Action: "GET_VISIT_RUN", SUBJID: ddlSUBJID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVisit.DataSource = ds.Tables[0];
                    ddlVisit.DataValueField = "VISITNUM";
                    ddlVisit.DataTextField = "VISIT";
                    ddlVisit.DataBind();
                }
                else
                {
                    ddlVisit.Items.Clear();
                }
                ddlVisit.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal.getSetDM(Action: "GET_PATIENT_REG", INVID: ddlInvid.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSUBJID.DataSource = ds.Tables[0];
                    ddlSUBJID.DataValueField = "SUBJID";
                    ddlSUBJID.DataTextField = "SUBJID";
                    ddlSUBJID.DataBind();
                }
                else
                {
                    ddlSUBJID.Items.Clear();
                }
                ddlSUBJID.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillModules()
        {
            try
            {
                DataSet ds = dal.DM_RUN_RULE(Action: "GET_MODULES_RUN", SUBJID: ddlSUBJID.SelectedValue, VISIT: ddlVisit.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.DataSource = ds.Tables[0];
                    ddlModule.DataValueField = "MODULEID";
                    ddlModule.DataTextField = "MODULENAME";
                    ddlModule.DataBind();
                }
                else
                {
                    ddlModule.Items.Clear();
                }
                ddlModule.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillFields()
        {
            try
            {
                DataSet ds = dal.DM_RUN_RULE(Action: "GET_FIELD_RUN", Module_ID: ddlModule.SelectedValue, VISIT: ddlVisit.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlField.DataSource = ds.Tables[0];
                    ddlField.DataTextField = "FIELDNAME";
                    ddlField.DataValueField = "ID";
                    ddlField.DataBind();
                }
                else
                {
                    ddlField.Items.Clear();
                }
                ddlField.Items.Insert(0, new ListItem("--All--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlInvid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                FillVisit();
                FillModules();
                FillFields();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSUBJID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillVisit();
                FillModules();
                FillFields();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillModules();
                FillFields();
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
                FillFields();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnRunRules_Click(object sender, EventArgs e)
        {
            try
            {
                RUN_RULES_SITEID();

                Response.Write("<script> alert('Rules ran successfully.');window.location='" + Request.RawUrl.ToString() + "'; </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RUN_RULES_SITEID()
        {
            try
            {
                if (ddlInvid.SelectedValue != "0")
                {
                    RUN_RULES_SUBJID(ddlInvid.SelectedValue);
                }
                else
                {
                    DataSet dsInv = dal.GET_INVID_SP();
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drInv in dsInv.Tables[0].Rows)
                        {
                            RUN_RULES_SUBJID(drInv["INVNAME"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RUN_RULES_SUBJID(string SITEID)
        {
            try
            {
                if (ddlSUBJID.SelectedValue != "0")
                {
                    RUN_RULES_VISIT(ddlSUBJID.SelectedValue);
                }
                else
                {
                    DataSet dsSUBJID = dal.getSetDM(Action: "GET_PATIENT_REG_RULES", INVID: SITEID);
                    if (dsSUBJID.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drSUBJID in dsSUBJID.Tables[0].Rows)
                        {
                            RUN_RULES_VISIT(drSUBJID["SUBJID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RUN_RULES_VISIT(string SUBJID)
        {
            try
            {
                if (ddlVisit.SelectedValue != "0")
                {
                    RUN_RULES_MODULE(SUBJID, ddlVisit.SelectedValue);
                }
                else
                {
                    DataSet dsVisit = dal.DM_RUN_RULE(Action: "GET_VISIT_RUN_SEP", SUBJID: SUBJID);
                    if (dsVisit.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drVisit in dsVisit.Tables[0].Rows)
                        {
                            RUN_RULES_MODULE(SUBJID, drVisit["VISITNUM"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RUN_RULES_MODULE(string SUBJID, string VISIT)
        {
            try
            {
                if (ddlModule.SelectedValue != "0")
                {
                    RUN_RULES_FIELDS(SUBJID, VISIT, ddlModule.SelectedValue);
                }
                else
                {
                    DataSet dsModule = dal.DM_RUN_RULE(Action: "GET_MODULES_RUN_SEP", SUBJID: SUBJID, VISIT: VISIT);
                    if (dsModule.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drModule in dsModule.Tables[0].Rows)
                        {
                            RUN_RULES_FIELDS(SUBJID, VISIT, drModule["MODULEID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RUN_RULES_FIELDS(string SUBJID, string VISITID, string MODULEID)
        {
            try
            {
                if (ddlField.SelectedValue != "0")
                {
                    Run_Rules(SUBJID, VISITID, MODULEID, ddlField.SelectedValue);
                }
                else
                {
                    DataSet dsField = dal.DM_RUN_RULE(Action: "GET_FIELD_RUN", Module_ID: MODULEID, VISIT: VISITID);

                    if (dsField.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drField in dsField.Tables[0].Rows)
                        {
                            Run_Rules(SUBJID, VISITID, MODULEID, drField["ID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Run_Rules(string SUBJID, string VISITID, string MODULEID, string FIELDID)
        {
            try
            {
                DataSet dsDATA = dal.DM_RUN_RULE(Action: "GET_DATA_RUN", SUBJID: SUBJID, VISIT: VISITID, Module_ID: MODULEID, Field_ID: FIELDID);

                if (dsDATA.Tables.Count > 0 && dsDATA.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drDATA in dsDATA.Tables[0].Rows)
                    {
                        string Para_SUBJID = SUBJID;
                        string Para_Visit_ID = VISITID;
                        string Para_ModuleId = MODULEID;

                        string Para_VariableName = drDATA["VARIABLENAME"].ToString();
                        string Para_PVID = drDATA["PVID"].ToString();
                        string Para_RECID = drDATA["RECID"].ToString();
                        string Para_UserID = drDATA["User"].ToString();

                        Check_Rules(Para_SUBJID, Para_Visit_ID, Para_VariableName, Para_ModuleId, Para_PVID, Para_RECID, Para_UserID);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Check_Rules(string Para_SUBJID, string Para_Visit_ID, string Para_VariableName, string Para_ModuleId, string Para_PVID, string Para_RECID, string Para_UserID)
        {
            try
            {
                string UNVISITNUM = ConfigurationManager.AppSettings["UNVISITNUM"];

                DataSet dsVarData = new DataSet();

                string strdata = "", CONDITION = "";

                DataSet dsRule = dal.DM_RULE_SP(Action: "CHECK_RULE_AGAINST_VARIABLE_DM_SEP", Visit_ID: Para_Visit_ID, Module_ID: Para_ModuleId, VARIABLENAMEDEC: Para_VariableName);

                foreach (DataRow drRule in dsRule.Tables[0].Rows)
                {
                    string RULE_ID = drRule["RULE_ID"].ToString();
                    string OtherPVIDS = "";
                    string MainPVID = "", MainRECID = "0";
                    string DATA = "";

                    if (drRule["GEN_QUERY"].ToString() == "True" || drRule["SET_VALUE"].ToString() == "True")
                    {
                        try
                        {
                            DataTable table = new DataTable();
                            table.Columns.Add("VARIABLENAME", typeof(string));
                            table.Columns.Add("DATA", typeof(string));

                            bool RESULTS = false, isFAIL = false;

                            CONDITION = drRule["Condition"].ToString();

                            DataSet dsVariables = dal.DM_RULE_SP(Action: "GET_Rule_Variables_FOR_DM", RULE_ID: drRule["ID"].ToString());

                            string MainColumnName = drRule["VariableName"].ToString();
                            string MainVisit = drRule["Visit_ID"].ToString();

                            foreach (DataRow drVariable in dsVariables.Tables[1].Rows)
                            {
                                string VariableName = drVariable["VariableName"].ToString();
                                string CONTROLTYPE = drVariable["CONTROLTYPE"].ToString();
                                string Derived = drVariable["Derived"].ToString();
                                string VariableCONDITION = drVariable["Condition"].ToString();

                                if (Derived != "True")
                                {
                                    if (CONTROLTYPE != "HEADER" && CONTROLTYPE != "ChildModule")
                                    {
                                        if (VariableCONDITION != "")
                                        {
                                            foreach (DataRow drData in table.Rows)
                                            {
                                                if (VariableCONDITION.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                                {
                                                    VariableCONDITION = VariableCONDITION.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                                }
                                            }
                                        }

                                        if (drVariable["Visit_ID"].ToString() == UNVISITNUM && Para_Visit_ID.StartsWith(UNVISITNUM))
                                        {
                                            dsVarData = dal.DM_RULE_SP(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                ColumnName: drVariable["ColumnName"].ToString(),
                                                TableName: drVariable["TableName"].ToString(),
                                                SUBJID: Para_SUBJID.ToString(),
                                                Visit_ID: Para_Visit_ID,
                                                Module_ID: Para_ModuleId,
                                                RECID: Para_RECID,
                                                PVID: Para_PVID,
                                                Condition: VariableCONDITION
                                                );
                                        }
                                        else
                                        {
                                            if (drVariable["Visit_ID"].ToString() == "000" && (Para_Visit_ID == drRule["Visit_ID"].ToString()) && (drVariable["Module_ID"].ToString() == drRule["Module_ID"].ToString()))
                                            {
                                                dsVarData = dal.DM_RULE_SP(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                ColumnName: drVariable["ColumnName"].ToString(),
                                                TableName: drVariable["TableName"].ToString(),
                                                SUBJID: Para_SUBJID.ToString(),
                                                Visit_ID: "000",
                                                Module_ID: Para_ModuleId,
                                                RECID: Para_RECID,
                                                PVID: Para_PVID,
                                                Condition: VariableCONDITION
                                                );
                                            }
                                            else if (drVariable["Visit_ID"].ToString() == "000" && (drVariable["Module_ID"].ToString() == drRule["Module_ID"].ToString()))
                                            {
                                                dsVarData = dal.DM_RULE_SP(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                ColumnName: drVariable["ColumnName"].ToString(),
                                                TableName: drVariable["TableName"].ToString(),
                                                SUBJID: Para_SUBJID.ToString(),
                                                Visit_ID: Para_Visit_ID,
                                                Module_ID: Para_ModuleId,
                                                RECID: Para_RECID,
                                                PVID: Para_PVID,
                                                Condition: VariableCONDITION
                                                );
                                            }
                                            else
                                            {
                                                dsVarData = dal.DM_RULE_SP(
                                                    Action: "GET_DATA_AGAINST_VARIABLE",
                                                    ColumnName: drVariable["ColumnName"].ToString(),
                                                    TableName: drVariable["TableName"].ToString(),
                                                    SUBJID: Para_SUBJID.ToString(),
                                                    Visit_ID: drVariable["Visit_ID"].ToString(),
                                                    Module_ID: Para_ModuleId,
                                                    RECID: Para_RECID,
                                                    PVID: Para_PVID,
                                                    Condition: VariableCONDITION
                                                    );
                                            }
                                        }

                                        if (dsVarData.Tables[0].Rows.Count > 0)
                                        {
                                            strdata = dsVarData.Tables[0].Rows[0][0].ToString();
                                            if (dsVarData.Tables[1].Rows.Count > 0)
                                            {
                                                OtherPVIDS += "," + dsVarData.Tables[1].Rows[0][0].ToString() + "(" + dsVarData.Tables[1].Rows[0][1].ToString() + ")";
                                            }
                                            else
                                            {
                                                OtherPVIDS += "," + Para_PVID + "(" + Para_RECID + ")";
                                            }
                                        }
                                        else
                                        {
                                            strdata = "";
                                            OtherPVIDS += "," + Para_PVID + "(" + Para_RECID + ")";
                                        }

                                        if ((MainColumnName == drVariable["ColumnName"].ToString()) && ((MainVisit == drVariable["Visit_ID"].ToString() || (MainVisit.StartsWith(UNVISITNUM) && (drVariable["Visit_ID"].ToString() == UNVISITNUM)))))
                                        {
                                            DATA = strdata;

                                            if (dsVarData.Tables[1].Rows.Count > 0)
                                            {
                                                MainPVID = dsVarData.Tables[1].Rows[0][0].ToString();
                                                MainRECID = dsVarData.Tables[1].Rows[0][1].ToString();
                                            }
                                            else
                                            {
                                                MainPVID = Para_PVID;
                                                MainRECID = Para_RECID;
                                            }
                                        }

                                        table.Rows.Add(VariableName, strdata);

                                        if (CONDITION.Contains("[" + VariableName + "]"))
                                        {
                                            CONDITION = CONDITION.Replace("[" + VariableName + "]", CheckDatatype(strdata));
                                        }
                                    }
                                }
                                else
                                {
                                    string FORMULA = drVariable["Formula"].ToString();

                                    foreach (DataRow drData in table.Rows)
                                    {
                                        if (FORMULA.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                        {
                                            FORMULA = FORMULA.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                        }
                                    }

                                    dsVarData = dal.DM_RULE_SP(
                                        Action: "GET_DATA_DERIVED",
                                        Formula: FORMULA
                                        );

                                    if (dsVarData.Tables[0].Rows.Count > 0)
                                    {
                                        strdata = dsVarData.Tables[0].Rows[0][0].ToString();
                                    }
                                    else
                                    {
                                        strdata = "";
                                    }

                                    foreach (DataRow dr in table.Rows)
                                    {
                                        if (strdata.Contains("[" + dr["VARIABLENAME"] + "]"))
                                        {
                                            string CHKDATA = CheckDatatype(dr["DATA"].ToString());
                                            if (CHKDATA != "")
                                            {
                                                strdata = strdata.Replace("[" + dr["VARIABLENAME"] + "]", CHKDATA);
                                            }
                                        }
                                    }

                                    table.Rows.Add(VariableName, strdata);

                                    if (CONDITION.Contains("[" + VariableName + "]"))
                                    {
                                        CONDITION = CONDITION.Replace("[" + VariableName + "]", CheckDatatype(strdata));
                                    }
                                }

                            }

                            if (MainPVID == "")
                            {
                                MainPVID = Para_PVID;
                                MainRECID = Para_RECID;
                            }

                            DataSet dsResults = new DataSet();

                            if (CONDITION != "")
                            {
                                try
                                {
                                    //GET CONDITION TRUE OR FALSE
                                    SqlCommand cmd;
                                    SqlDataAdapter adp;

                                    SqlConnection con = new SqlConnection(dal.getconstr());
                                    cmd = new SqlCommand("DM_RULE_SP", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Action", "CHECKRULE_CONDITION");
                                    cmd.Parameters.AddWithValue("@Condition", CONDITION);

                                    con.Open();
                                    adp = new SqlDataAdapter(cmd);
                                    adp.Fill(dsResults);
                                    cmd.Dispose();
                                    con.Close();

                                    if (dsResults.Tables[0].Rows[0]["TESTED"].ToString() == "1")
                                    {
                                        RESULTS = true;
                                    }
                                    else
                                    {
                                        RESULTS = false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    isFAIL = true;
                                }
                            }
                            else
                            {
                                RESULTS = false;
                            }

                            if (!isFAIL)
                            {
                                if (RESULTS)
                                {
                                    if (drRule["GEN_QUERY"].ToString() == "True")
                                    {
                                        string QUERYTEXT = drRule["QueryText"].ToString();

                                        foreach (DataRow drData in table.Rows)
                                        {
                                            if (QUERYTEXT.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                            {
                                                QUERYTEXT = QUERYTEXT.Replace("[" + drData["VARIABLENAME"].ToString() + "]", drData["DATA"].ToString());
                                            }
                                        }

                                        Generate_Query
                                        (
                                        RULE_ID: drRule["RULE_ID"].ToString(),
                                        Nature: drRule["Nature"].ToString(),
                                        PVID: MainPVID,
                                        RECID: MainRECID,
                                        SUBJID: Para_SUBJID,
                                        Data: DATA,
                                        QUERYTEXT: QUERYTEXT,
                                        Module_ID: drRule["Module_ID"].ToString(),
                                        Field_ID: drRule["Field_ID"].ToString(),
                                        VARIABLENAME: drRule["VariableName"].ToString(),
                                        Informational: drRule["Informational"].ToString(),
                                        OtherPVIDS: OtherPVIDS,
                                        UserID: Para_UserID
                                        );
                                    }

                                    if (drRule["SET_VALUE"].ToString() == "True")
                                    {
                                        CONDITION = "";

                                        foreach (DataRow drData in table.Rows)
                                        {
                                            if (CONDITION == "")
                                            {
                                                if (drRule["FORMULA_VALUE"].ToString().Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                                {
                                                    CONDITION = drRule["FORMULA_VALUE"].ToString().Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                                }
                                            }
                                            else
                                            {
                                                if (CONDITION.Contains("[" + drData["VARIABLENAME"].ToString() + "]"))
                                                {
                                                    CONDITION = CONDITION.Replace("[" + drData["VARIABLENAME"].ToString() + "]", CheckDatatype(drData["DATA"].ToString()));
                                                }
                                            }
                                        }

                                        DataSet DS_SETVALUE = dal.DM_RULE_SP(Action: "CHECKRULE_FORMULA_VALUE", Formula: CONDITION);

                                        DataSet dsSET_Value = dal.DM_RULE_SP(Action: "UPDATE_SET_VALUE",
                                        RULE_ID: drRule["RULE_ID"].ToString(),
                                        Value: DS_SETVALUE.Tables[0].Rows[0]["Data"].ToString(),
                                        SUBJID: Para_SUBJID,
                                        VISITNO: Para_Visit_ID,
                                        RECID: MainRECID,
                                        PVID: MainPVID
                                        );
                                    }
                                }
                                else
                                {
                                    Resolve_Query
                                        (
                                        RULE_ID: drRule["RULE_ID"].ToString(),
                                        SUBJID: Para_SUBJID,
                                        VARIABLENAME: drRule["VariableName"].ToString(),
                                        MainPVID: MainPVID,
                                        MainRECID: MainRECID,
                                        UserID: Para_UserID
                                        );
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Generate_Query(string RULE_ID, string Nature, string PVID, string RECID, string SUBJID, string Data, string QUERYTEXT, string Module_ID,
            string Field_ID, string VARIABLENAME, string Informational, string OtherPVIDS, string UserID)
        {
            try
            {
                dal.DM_RUN_RULE(Action: "Generate_Query",
                RULE_ID: RULE_ID,
                Nature: Nature,
                PVID: PVID,
                RECID: RECID,
                ContID: "1",
                SUBJID: SUBJID,
                Data: Data,
                QUERYTEXT: QUERYTEXT,
                Module_ID: Module_ID,
                Field_ID: Field_ID,
                VARIABLENAME: VARIABLENAME,
                Informational: Informational,
                OtherPVIDS: OtherPVIDS,
                ENTEREDBY: UserID
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Resolve_Query(string RULE_ID, string SUBJID, string VARIABLENAME, string MainPVID, string MainRECID, string UserID)
        {
            try
            {
                dal.DM_RUN_RULE(Action: "Resolve_Query",
                RULE_ID: RULE_ID,
                SUBJID: SUBJID,
                VARIABLENAME: VARIABLENAME,
                PVID: MainPVID,
                RECID: MainRECID,
                ENTEREDBY: UserID
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private string CheckDatatype(string Val)
        {
            string RESULT = "";
            try
            {
                CommonFunction.CommonFunction cf = new CommonFunction.CommonFunction();

                int i = 0;
                float j = 0;
                double k = 0;
                DateTime l;

                if (Val.Contains("dd/"))
                {
                    Val = Val.Replace("dd/", "01/");
                }

                if (Val.Contains("mm/"))
                {
                    Val = Val.Replace("mm/", "01/");
                }

                if (int.TryParse(Val, out i))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (float.TryParse(Val, out j))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (double.TryParse(Val, out k))
                {
                    RESULT = "dbo.CastNumber(" + Val + ")";
                }
                else if (DateTime.TryParse(Val, out l) || cf.isDate(Val))
                {
                    RESULT = "dbo.CastDate('" + Val + "')";
                }
                else
                {
                    RESULT = "'" + Val + "'";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

            return RESULT;
        }

    }
}