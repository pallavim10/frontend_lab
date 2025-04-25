using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;
using System.Data.SqlClient;

namespace CTMS
{
    public partial class DM_OpenCRF_INV_ReadOnly : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!this.IsPostBack)
                {
                    FillINV();
                    FillSubject();
                    if (!string.IsNullOrEmpty(drpSubID.SelectedValue) && drpSubID.SelectedValue != "0")
                    {
                        FillVisit();
                        OpenCRF();
                        lbtnRefresh.Visible = true;
                    }
                    else
                    {
                        lbtnRefresh.Visible = false;
                    }

                    GET_LOCK_STATUS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();

            }

        }

        protected void GET_LOCK_STATUS()
        {
            try
            {
                DataSet ds = dal_DM.DM_LOCK_SP(ACTION: "GET_BULK_LOCK_STATUS",
                    INVID: drpInvID.SelectedValue,
                    SUBJID: drpSubID.SelectedValue,
                    VISITNUM: drpSubID.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Session["LOCK_STATUS"] = ds.Tables[0].Rows[0]["LOCKSTATUS"].ToString();
                }
                else
                {
                    Session.Remove("LOCK_STATUS");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            DAL dal = new DAL();

            DataSet ds = dal.GET_INVID_SP(
                    USERID: Session["User_ID"].ToString()
                    );
            drpInvID.DataSource = ds.Tables[0];
            drpInvID.DataValueField = "INVID";
            drpInvID.DataBind();

            FillSubject();

            if (Session["DM_CRF_INVID"] != null)
            {
                if (drpInvID.Items.Contains(new ListItem(Session["DM_CRF_INVID"].ToString())))
                {
                    drpInvID.SelectedValue = Session["DM_CRF_INVID"].ToString();
                }
            }

            Session["DM_CRF_INVID"] = drpInvID.SelectedValue;
        }

        private void FillVisit()
        {
            try
            {
                DataSet ds = dal_DM.DM_VISIT_SP(SUBJID: drpSubID.SelectedValue);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    if (dr["CritID"].ToString() != "" && dr["CritID"].ToString() != "0" && !toBeDeleted_Visit(dr["CritID"].ToString()) && drpSubID.SelectedValue != "Select")
                    {
                        dr.Delete();
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpVisit.DataSource = ds.Tables[0];
                    drpVisit.DataValueField = "VISITNUM";
                    drpVisit.DataTextField = "VISIT";
                    drpVisit.DataBind();
                    drpVisit.Items.Insert(0, new ListItem("--Select Visit--", "Select"));
                }
                else
                {
                    drpVisit.Items.Clear();
                }

                if (Session["DM_CRF_VISIT"] != null)
                {
                    if (drpVisit.Items.FindByValue(Session["DM_CRF_VISIT"].ToString()) != null)
                    {
                        drpVisit.SelectedValue = Session["DM_CRF_VISIT"].ToString();
                    }
                }
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
                DataSet ds = dal_DM.DM_SUBJECT_LIST_SP(INVID: drpInvID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    //drpSubID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSubID.Items.Clear();
                }

                if (Session["DM_CRF_SUBJID"] != null)
                {
                    if (drpSubID.Items.Contains(new ListItem(Session["DM_CRF_SUBJID"].ToString())))
                    {
                        drpSubID.SelectedValue = Session["DM_CRF_SUBJID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                if (!string.IsNullOrEmpty(drpSubID.SelectedValue) && drpSubID.SelectedValue != "0")
                {
                    FillVisit();
                    OpenCRF();
                    lbtnRefresh.Visible = true;
                }
                else
                {
                    drpSubID.Items.Clear();
                    drpVisit.Items.Clear();
                    Grd_OpenCRF.DataSource = null;
                    Grd_OpenCRF.DataBind();
                    lbtnRefresh.Visible = false;
                }
                Session["DM_CRF_INVID"] = drpInvID.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OpenCRF();

                Session["DM_CRF_VISIT"] = drpVisit.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSubID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(drpSubID.SelectedValue) && drpSubID.SelectedValue != "0")
                {
                    FillVisit();
                    OpenCRF();
                    lbtnRefresh.Visible = true;
                }
                else
                {
                    lbtnRefresh.Visible = false;
                }

                Session["DM_CRF_SUBJID"] = drpSubID.SelectedValue;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private bool toBeDeleted_Visit(string CritID)
        {
            bool res = false;
            try
            {
                DataSet ds = dal_DM.DM_VISIT_CRITERIA_SP(
                ID: CritID,
                SUBJID: drpSubID.SelectedValue,
                SITEID: drpInvID.SelectedValue
                );

                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "1")
                {
                    res = true;
                }
                else
                {
                    res = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                res = false;
            }
            return res;
        }

        private void OpenCRF()
        {
            try
            {
                DataSet ds = dal_DM.DM_OPEN_CRF_SP(
                    SUBJID: drpSubID.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];

                        if (dr["CritID"].ToString() != "" && dr["CritID"].ToString() != "0" && !toBeDeleted(dr["CritID"].ToString()))
                        {
                            dr.Delete();
                        }
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_OpenCRF.DataSource = ds.Tables[0];
                        Grd_OpenCRF.DataBind();

                        lbtnRefresh.Visible = true;
                    }
                    else
                    {
                        Grd_OpenCRF.DataSource = null;
                        Grd_OpenCRF.DataBind();

                        lbtnRefresh.Visible = false;
                    }
                }
                else
                {
                    Grd_OpenCRF.DataSource = null;
                    Grd_OpenCRF.DataBind();

                    lbtnRefresh.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private bool toBeDeleted(string CritID)
        {
            bool res = false;
            try
            {
                DataSet ds = dal_DM.DM_MODULE_CRITERIA_SP(
                ID: CritID,
                SUBJID: drpSubID.SelectedValue,
                VISITNUM: drpVisit.SelectedValue,
                SITEID: drpInvID.SelectedValue
                );

                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "1")
                {
                    res = true;
                }
                else
                {
                    res = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                res = false;
            }
            return res;
        }

        protected void Grd_OpenCRF_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    Label MODULEID = e.Row.FindControl("MODULEID") as Label;

                    ImageButton lnkVISITPAGENO = e.Row.FindControl("lnkPAGENUM") as ImageButton;

                    if (dr["PAGESTATUS"].ToString() == "1")
                    {
                        if (dr["HasMissing"].ToString() == "True")
                        {
                            lnkVISITPAGENO.ImageUrl = "img/REDFILE.png";
                            lnkVISITPAGENO.ToolTip = "Missing Fields";
                        }
                        else if (dr["IsComplete"].ToString() == "0")
                        {
                            lnkVISITPAGENO.ImageUrl = "img/orange file.png";
                            lnkVISITPAGENO.ToolTip = "Incomplete";
                        }
                        else if (dr["IsComplete"].ToString() == "2")
                        {
                            lnkVISITPAGENO.ImageUrl = "img/NotApplicableFile.png";
                            lnkVISITPAGENO.ToolTip = "Not Applicable";
                        }
                        else
                        {
                            lnkVISITPAGENO.ImageUrl = "img/green file.png";
                            lnkVISITPAGENO.ToolTip = "Completed";
                        }
                    }
                    else
                    {
                        lnkVISITPAGENO.ToolTip = "Not Entered";
                    }

                    if (dr["QueryCount"].ToString() == "0")
                    {
                        LinkButton lnkQuery = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                        lnkQuery.Visible = false;
                    }

                    if (dr["AnsQueryCount"].ToString() == "0")
                    {
                        LinkButton lnkQUERYANS = (LinkButton)e.Row.FindControl("lnkQUERYANS");
                        lnkQUERYANS.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Grd_OpenCRF_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "GOTOPAGE")
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                    string MODULEID = (gvr.FindControl("MODULEID") as Label).Text;
                    string MODULENAME = (gvr.FindControl("MODULENAME") as LinkButton).Text;
                    string VISITID = drpVisit.SelectedValue;
                    string VISIT = (gvr.FindControl("VISIT") as Label).Text;
                    string INVID = drpInvID.SelectedValue;
                    string SUBJID = drpSubID.SelectedValue;

                    if ((gvr.FindControl("MULTIPLEYN") as Label).Text == "True")
                    {
                        Response.Redirect("DM_DataEntry_MultipleData_INV_Read_Only.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID);
                    }
                    else
                    {
                        Response.Redirect("DM_DataEntry_INV_Read_Only.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID);
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void lbtnAddUnscheduled_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DM.DM_UNSC_VISIT_SP(
                ACTION: "INSERT_UNSCHEDULED_VISIT",
                SUBJID: Request.QueryString["SUBJID"].ToString()
                );

                Response.Redirect("List_of_Child_visits.aspx?SUBJID=" + Request.QueryString["SUBJID"].ToString() + "");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RUN_RULES_MODULE(drpSubID.SelectedValue, drpVisit.SelectedValue);

                Response.Write("<script> alert('Rules successfully refreshed.');window.location='DM_OpenCRF_INV_ReadOnly.aspx?VISITNUM=" + drpVisit.SelectedValue + "&SUBJID=" + drpSubID.SelectedValue + "'; </script>");
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
                DataSet dsModule = dal_DM.DM_RUN_RULE(Action: "GET_MODULES_RUN", SUBJID: SUBJID, VISIT: VISIT);
                if (dsModule.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drModule in dsModule.Tables[0].Rows)
                    {
                        RUN_RULES_FIELDS(SUBJID, drModule["MODULEID"].ToString(), VISIT);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void RUN_RULES_FIELDS(string SUBJID, string MODULEID, string VISITID)
        {
            try
            {
                DataSet dsField = dal_DM.DM_RUN_RULE(Action: "GET_FIELD_RUN", Module_ID: MODULEID, VISIT: VISITID);

                if (dsField.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drField in dsField.Tables[0].Rows)
                    {
                        Run_Rules(SUBJID, VISITID, MODULEID, drField["ID"].ToString());
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
                DataSet dsDATA = dal_DM.DM_RUN_RULE(Action: "GET_DATA_RUN", SUBJID: SUBJID, VISIT: VISITID, Module_ID: MODULEID, Field_ID: FIELDID);

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
                string UNVISITNUM = "";
                if (drpVisit.SelectedValue.Contains("."))
                {
                    UNVISITNUM = drpVisit.SelectedValue.ToString().Substring(0, drpVisit.SelectedValue.ToString().IndexOf('.'));
                }

                DataSet dsVarData = new DataSet();

                string strdata = "", CONDITION = "";

                DataSet dsRule = dal_DM.DM_RUN_RULE(Action: "CHECK_RULE_AGAINST_VARIABLE_DM",
                    VISITNUM: Para_Visit_ID,
                    Module_ID: Para_ModuleId,
                    Para_VariableName: Para_VariableName
                    );

                foreach (DataRow drRule in dsRule.Tables[0].Rows)
                {
                    string OtherPVIDS = "";
                    string MainPVID = "", MainRECID = "0", MainVISITNUM = "", MainVISIT = "";
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

                            DataSet dsVariables = dal_DM.DM_RUN_RULE(Action: "GET_Rule_Variables_FOR_DM", RULE_ID: drRule["ID"].ToString());

                            string MainColumnName = drRule["VARIABLENAME"].ToString();
                            string MainVisit = drRule["VISITNUM"].ToString();

                            foreach (DataRow drVariable in dsVariables.Tables[1].Rows)
                            {
                                string VariableName = drVariable["VARIABLENAME_DEF"].ToString();
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

                                        if (drVariable["VISITNUM"].ToString() == UNVISITNUM && Para_Visit_ID.Contains("."))
                                        {
                                            dsVarData = dal_DM.DM_RUN_RULE(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                TABLENAME: drVariable["TableName"].ToString(),
                                                SUBJID: Para_SUBJID,
                                                VISITNUM: Para_Visit_ID,
                                                Module_ID: Para_ModuleId,
                                                RECID: Para_RECID,
                                                PVID: Para_PVID,
                                                Condition: VariableCONDITION
                                                );
                                        }
                                        else
                                        {
                                            if (drVariable["VISITNUM"].ToString() == "000" && (Para_Visit_ID == drRule["VISITNUM"].ToString()) && (drVariable["MODULEID"].ToString() == drRule["MODULEID"].ToString()))
                                            {
                                                dsVarData = dal_DM.DM_RUN_RULE(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                TABLENAME: drVariable["TableName"].ToString(),
                                                SUBJID: Para_SUBJID,
                                                VISITNUM: "000",
                                                Module_ID: Para_ModuleId,
                                                RECID: Para_RECID,
                                                PVID: Para_PVID,
                                                Condition: VariableCONDITION
                                                );
                                            }
                                            else if (drVariable["VISITNUM"].ToString() == "000" && (drVariable["MODULEID"].ToString() == drRule["MODULEID"].ToString()))
                                            {
                                                dsVarData = dal_DM.DM_RUN_RULE(
                                                Action: "GET_DATA_AGAINST_VARIABLE",
                                                VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                TABLENAME: drVariable["TableName"].ToString(),
                                                SUBJID: Para_SUBJID,
                                                VISITNUM: Para_Visit_ID,
                                                Module_ID: Para_ModuleId,
                                                RECID: Para_RECID,
                                                PVID: Para_PVID,
                                                Condition: VariableCONDITION
                                                );
                                            }
                                            else
                                            {
                                                dsVarData = dal_DM.DM_RUN_RULE(
                                                    Action: "GET_DATA_AGAINST_VARIABLE",
                                                    VARIABLENAME: drVariable["VARIABLENAME"].ToString(),
                                                    TABLENAME: drVariable["TableName"].ToString(),
                                                    SUBJID: Para_SUBJID,
                                                    VISITNUM: drVariable["VISITNUM"].ToString(),
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

                                        if ((MainColumnName == drVariable["VARIABLENAME"].ToString()) && ((MainVisit == drVariable["VISITNUM"].ToString() || (MainVisit.Contains(".") && (drVariable["VISITNUM"].ToString().Contains("."))))))
                                        {
                                            DATA = strdata;

                                            if (dsVarData.Tables[1].Rows.Count > 0)
                                            {
                                                MainPVID = dsVarData.Tables[1].Rows[0][0].ToString();
                                                MainRECID = dsVarData.Tables[1].Rows[0][1].ToString();
                                                MainVISITNUM = dsVarData.Tables[1].Rows[0][2].ToString();
                                                MainVISIT = DBNull.Value.ToString();
                                            }
                                            else
                                            {
                                                MainPVID = Para_PVID;
                                                MainRECID = Para_RECID;
                                                MainVISITNUM = Para_Visit_ID;
                                                MainVISIT = drpVisit.SelectedValue;
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

                                    dsVarData = dal_DM.DM_RUN_RULE(
                                        Action: "GET_DATA_DERIVED",
                                        FORMULA: FORMULA
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

                                    SqlConnection con = new SqlConnection(dal_DM.getconstr());
                                    cmd = new SqlCommand("DM_RUN_RULE", con);
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
                                        RULE_ID: drRule["RULEID"].ToString(),
                                        Nature: drRule["Nature"].ToString(),
                                        PVID: MainPVID,
                                        RECID: MainRECID,
                                        SUBJID: Para_SUBJID,
                                        Data: DATA,
                                        QUERYTEXT: QUERYTEXT,
                                        Module_ID: drRule["MODULEID"].ToString(),
                                        Field_ID: drRule["FIELDID"].ToString(),
                                        VARIABLENAME: drRule["VARIABLENAME"].ToString(),
                                        OtherPVIDS: OtherPVIDS,
                                        VISITNUM: MainVISITNUM,
                                        VISIT: MainVISIT
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

                                        DataSet DS_SETVALUE = dal_DM.DM_RUN_RULE(Action: "CHECKRULE_FORMULA_VALUE", FORMULA: CONDITION);

                                        DataSet dsSET_Value = dal_DM.DM_RUN_RULE(Action: "UPDATE_SET_VALUE",
                                            RULE_ID: drRule["RULEID"].ToString(),
                                            Value: DS_SETVALUE.Tables[0].Rows[0]["Data"].ToString(),
                                            SUBJID: drpSubID.SelectedValue,
                                            VISITNUM: Para_Visit_ID.ToString(),
                                            RECID: MainRECID,
                                            PVID: MainPVID
                                            );
                                    }
                                }
                                else
                                {
                                    Resolve_Query
                                        (
                                        RULE_ID: drRule["RULEID"].ToString(),
                                        SUBJID: Para_SUBJID,
                                        VARIABLENAME: drRule["VARIABLENAME"].ToString(),
                                        MainPVID: MainPVID,
                                        MainRECID: MainRECID,
                                        OtherPVIDS: OtherPVIDS
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
        string Field_ID, string VARIABLENAME, string OtherPVIDS, string VISITNUM, string VISIT)
        {
            try
            {
                dal_DM.DM_RUN_RULE(Action: "Generate_Query",
                    RULE_ID: RULE_ID,
                    Nature: Nature,
                    PVID: PVID,
                    RECID: RECID,
                    SUBJID: SUBJID,
                    Data: Data,
                    QUERYTEXT: QUERYTEXT,
                    Module_ID: Module_ID,
                    Field_ID: Field_ID,
                    VARIABLENAME: VARIABLENAME,
                    OtherPVIDS: OtherPVIDS,
                    VISIT: VISIT,
                    VISITNUM: VISITNUM,
                    INVID: Request.QueryString["INVID"].ToString()
                    );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Resolve_Query(string RULE_ID, string SUBJID, string VARIABLENAME, string MainPVID, string MainRECID, string OtherPVIDS)
        {
            try
            {
                dal_DM.DM_RUN_RULE(Action: "Resolve_Query",
                RULE_ID: RULE_ID,
                SUBJID: SUBJID,
                VARIABLENAME: VARIABLENAME,
                PVID: MainPVID,
                RECID: MainRECID,
                OtherPVIDS: OtherPVIDS
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