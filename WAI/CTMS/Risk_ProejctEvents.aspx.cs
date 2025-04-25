using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace CTMS
{
    public partial class Risk_ProejctEvents : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }

                dal.ProtocolVoilation_SP(Action: "InsertRISKEVENT_log", ENTEREDBY: Session["User_ID"].ToString(), PROTVOIL_ID: Request.QueryString["RiskId"].ToString());

                Session["TYPE"] = Request.QueryString["TYPE"]; //HttpUtility.HtmlDecode(Request.QueryString["TYPE"]);
                Session["Risk_ProjEventID"] = Request.QueryString["RiskId"];// HttpUtility.HtmlDecode(Request.QueryString["RiskId"]);


                if (Session["TYPE"].ToString() == "NEW")
                {
                    DataSet ds = new DataSet();
                    ds = dal.getsetRisk_SP(Action: "MAX_RISK_ID");
                    Session["Risk_MitigationID"] = ds.Tables[0].Rows[0][0].ToString();
                    txtRiskID.Text = ds.Tables[0].Rows[0][0].ToString();
                    txtIndetifyBy.Text = Session["User_Name"].ToString();
                }
                else
                {
                    txtDateIdentify.Enabled = false;
                    txtIndetifyBy.Enabled = false;
                }

                if (!this.IsPostBack)
                {
                    DataSet ds = new DataSet();
                    ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "RISKTYPE");

                    lstRiskType.Items.Clear();
                    lstRiskType.DataSource = ds;
                    lstRiskType.DataTextField = "TEXT";
                    lstRiskType.DataValueField = "VALUE";
                    lstRiskType.DataBind();

                    ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Impact");

                    lstRiskImpact.Items.Clear();
                    lstRiskImpact.DataSource = ds;
                    lstRiskImpact.DataTextField = "TEXT";
                    lstRiskImpact.DataValueField = "VALUE";
                    lstRiskImpact.DataBind();


                    ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Status");
                    dal.BindDropDown(DrpRiskStatus, ds.Tables[0]);

                    //ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "RootCause");
                    //dal.BindDropDown(drpRootCause, ds.Tables[0]);


                    //ds = dal.getsetRisk_SP(Action: "GET_USER_LIST");
                    DataSet dsEmp = dal.EmpMaster_SP(Action: "VIEW");
                    drpRiskOwner.DataSource = dsEmp.Tables[0];
                    drpRiskOwner.DataValueField = "ID";
                    drpRiskOwner.DataTextField = "Name";
                    drpRiskOwner.DataBind();
                    drpRiskOwner.Items.Insert(0, new ListItem("--Select--", "0"));

                    getData();

                    bind_Cause();
                    bind_Mitigation();
                    CheckAnticipation();
                    BindRelatedGrid();
                    BINDUSERLOGS();
                    BINDComments();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BindRelatedGrid()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataTable dt = dal.Update_ProjEvents(
                Action: "GET_DATA_RELATED",
                RISK_ID: Request.QueryString["RiskId"].ToString(),
                Rcategory: ddlcategory.SelectedValue,
                RSubcategory: ddlsubcategory.SelectedValue,
                Rfactors: ddlfactor.SelectedValue
                );

                if (dt.Rows.Count > 0)
                {
                    grdRelatedRisk.DataSource = dt;
                    grdRelatedRisk.DataBind();
                }
                else
                {
                    grdRelatedRisk.DataSource = null;
                    grdRelatedRisk.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void getData()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = dal.getprojectevents(Action: "Update", Id: Session["Risk_ProjEventID"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtSiteID.Text = dt.Rows[0]["Site"].ToString();
                    txtRiskID.Text = dt.Rows[0]["id"].ToString();
                    txtDateIdentify.Text = dt.Rows[0]["RDateIdentified"].ToString();
                    txtIndetifyBy.Text = dt.Rows[0]["RIdentifiedBy"].ToString();
                    txtRiskDisc.Text = dt.Rows[0]["REvent_description"].ToString();
                    DrpRiskStatus.SelectedValue = dt.Rows[0]["Risk_Status"].ToString();
                    txtSource.Text = dt.Rows[0]["Source"].ToString();
                    lbtnRefrence.Text = dt.Rows[0]["Reference"].ToString();
                    //drpRootCause.SelectedValue = dt.Rows[0]["RootCause"].ToString();
                    drpRiskOwner.SelectedValue = dt.Rows[0]["Risk_Owner"].ToString();
                    BindCategory();
                    ddlcategory.SelectedValue = dt.Rows[0]["RCategory"].ToString();
                    BindSubcategory();
                    ddlsubcategory.SelectedValue = dt.Rows[0]["RSubCategory"].ToString();
                    BindFactors();
                    ddlfactor.SelectedValue = dt.Rows[0]["RFactor"].ToString();
                    lblCategory.Text = dt.Rows[0]["SCATEGORY"].ToString();
                    lblSubCategory.Text = dt.Rows[0]["SSUBFACTOR"].ToString();
                    lblFactor.Text = dt.Rows[0]["SFACTOR"].ToString();

                    ds = dal.getEventCount(Category: ddlcategory.SelectedValue,
              SubCategory: ddlsubcategory.SelectedValue,
              Factor: ddlfactor.SelectedValue,
              ProjectId: Session["PROJECTID"].ToString());
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        txtCount.Text = ds.Tables[0].Rows[0]["count"].ToString();
                    }

                    txtcomments.Text = dt.Rows[0]["RComments"].ToString();
                    ds = dal.getsetRisk_Type(Action: "GET_Risk_TYPE", RISK_ID: Session["Risk_ProjEventID"].ToString(), variablename: "Event Type", ProjectId: Session["PROJECTID"].ToString());
                    lstRiskType.ClearSelection();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string temp = ds.Tables[0].Rows[i]["RISK_TYPE"].ToString();
                            if (temp != "")
                            {
                                ListItem itm = lstRiskType.Items.FindByValue(temp);
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        lstRiskType.ClearSelection();
                    }

                    dt = dal.getRisk_Impact(Action: "GET_Risk_Impact", RISK_ID: Session["Risk_ProjEventID"].ToString(), VariableName: "Event Impact", ProjectId: Session["PROJECTID"].ToString());
                    lstRiskImpact.ClearSelection();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string temp = dt.Rows[i]["Risk_Impacts"].ToString();
                            if (temp != "")
                            {
                                ListItem itm = lstRiskImpact.Items.FindByValue(temp);
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        lstRiskImpact.ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void bntSave_Click(object sender, EventArgs e)
        {
            try
            {
                string rtype = "";
                foreach (ListItem item in lstRiskType.Items)
                {
                    if (item.Selected == true)
                    {
                        if (rtype.ToString() == "")
                        {
                            rtype = item.Value;
                        }
                        else
                        {
                            rtype += "," + item.Value;
                        }
                    }
                }


                string rimpacts = "";
                foreach (ListItem item in lstRiskImpact.Items)
                {
                    if (item.Selected == true)
                    {
                        if (rimpacts.ToString() == "")
                        {
                            rimpacts = item.Value;
                        }
                        else
                        {
                            rimpacts += "," + item.Value;
                        }
                    }
                }

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = dal.Update_ProjEvents(Action: "Insert",
                REvent_desc: txtRiskDisc.Text,
                RISK_ID: txtRiskID.Text,
                source: txtSource.Text,
                Reference: lbtnRefrence.Text,
                Risk_Owner: drpRiskOwner.SelectedValue,
                Comments: txtcomments.Text,
                Rcategory: ddlcategory.SelectedValue,
                RSubcategory: ddlsubcategory.SelectedValue,
                Rfactors: ddlfactor.SelectedValue,
                RImpacts: rimpacts,
                Risk_Type: rtype,
                status: DrpRiskStatus.SelectedValue);

                //DELETE RISK TYPE   
                ds = dal.getsetRisk_Type(Action: "DELETE_RISK_TYPE",
                     RISK_ID: Session["Risk_ProjEventID"].ToString(), variablename: "Event Type", ProjectId: Session["PROJECTID"].ToString());
                //INSERT RISK TYPE   
                foreach (ListItem item in lstRiskType.Items)
                {
                    if (item.Selected == true)
                    {
                        ds = dal.getsetRisk_Type(Action: "INSERT_RISK_TYPE",
                        RISK_ID: Session["Risk_ProjEventID"].ToString(),
                        RiskType: item.Value.Trim(),
                        ENTEREDBY: Session["User_ID"].ToString(),
                        variablename: "Event Type",
                        ProjectId: Session["PROJECTID"].ToString());
                    }
                }

                //DELETE RISK IMPACT   
                dt = dal.getRisk_Impact(Action: "DELETE_RISK_Impact",
                     RISK_ID: Session["Risk_ProjEventID"].ToString(), VariableName: "Event Impact", ProjectId: Session["PROJECTID"].ToString());
                //INSERT RISK TYPE   
                foreach (ListItem item in lstRiskImpact.Items)
                {
                    if (item.Selected == true)
                    {
                        dt = dal.getRisk_Impact(Action: "INSERT_RISK_Impact",
                        RISK_ID: Session["Risk_ProjEventID"].ToString(),
                        RiskImpact: item.Value.Trim(),
                        ENTEREDBY: Session["User_ID"].ToString(),
                        VariableName: "Event Impact",
                        ProjectId: Session["PROJECTID"].ToString());
                    }
                }

                //PD Comment save
                int rowindex, UPDATE_FLAG_cmt;
                for (rowindex = 0; rowindex < grdCmts.Rows.Count; rowindex++)
                {
                    UPDATE_FLAG_cmt = Convert.ToInt16(((TextBox)grdCmts.Rows[rowindex].FindControl("UPDATE_FLAG_cmt")).Text);
                    if (UPDATE_FLAG_cmt == 0)
                    {
                        if (((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text != "")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.ProtocolVoilation_SP(
                            Action: "INSERT_PRE_COMMENT",
                            PROTVOIL_ID: Request.QueryString["RiskId"].ToString(),
                            Source: ((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text,
                            ENTEREDBY: Session["User_ID"].ToString()
                            );
                        }
                    }
                    else
                    {
                        if (((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text != "")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.ProtocolVoilation_SP(
                            Action: "UPDATE",
                            PROTVOIL_ID: Request.QueryString["RiskId"].ToString(),
                            Source: ((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text,
                            UPDATEDBY: Session["User_ID"].ToString()
                            );
                        }
                    }
                }

                Response.Write("<script> alert('Record Updated successfully.')</script>");
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSubcategory();
                BindFactors();
                DataSet ds = dal.getEventCount(Category: ddlcategory.SelectedValue,
                SubCategory: ddlsubcategory.SelectedValue,
                Factor: ddlfactor.SelectedValue,
                ProjectId: Session["PROJECTID"].ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCount.Text = ds.Tables[0].Rows[0]["count"].ToString();
                }

                CheckAnticipation();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindFactors();
                DataSet ds = dal.getEventCount(Category: ddlcategory.SelectedValue,
               SubCategory: ddlsubcategory.SelectedValue,
               Factor: ddlfactor.SelectedValue,
               ProjectId: Session["PROJECTID"].ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCount.Text = ds.Tables[0].Rows[0]["count"].ToString();
                }

                CheckAnticipation();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BindCategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Category");
                if (dt.Rows.Count > 1)
                {
                    ddlcategory.DataSource = dt;
                    ddlcategory.DataTextField = "Description";
                    ddlcategory.DataValueField = "id";
                    ddlcategory.DataBind();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BindSubcategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue);
                if (dt.Rows.Count > 1)
                {
                    ddlsubcategory.DataSource = dt;
                    ddlsubcategory.DataTextField = "Description";
                    ddlsubcategory.DataValueField = "id";
                    ddlsubcategory.DataBind();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BindFactors()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Factor", SubCategoryvalue: ddlsubcategory.SelectedValue);
                if (dt.Rows.Count > 1)
                {
                    ddlfactor.DataSource = dt;
                    ddlfactor.DataTextField = "Description";
                    ddlfactor.DataValueField = "id";
                    ddlfactor.DataBind();
                }
                else
                {
                    DataTable dtf = new DataTable();
                    ddlfactor.DataSource = dtf;
                    ddlfactor.DataTextField = "Description";
                    ddlfactor.DataValueField = "id";
                    ddlfactor.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlfactor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.getEventCount(Category: ddlcategory.SelectedValue,
               SubCategory: ddlsubcategory.SelectedValue,
               Factor: ddlfactor.SelectedValue,
               ProjectId: Session["PROJECTID"].ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCount.Text = ds.Tables[0].Rows[0]["count"].ToString();
                }

                CheckAnticipation();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Risk Mitigation
        private void bind_Mitigation()
        {
            try
            {
                DataSet ds = dal.Risk_Mitigation_SP(Action: "GET", Event_ID: txtRiskID.Text);
                gvMitigation.DataSource = ds.Tables[0];
                gvMitigation.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BINDUSERLOGS()
        {
            try
            {
                DataSet ds = dal.ProtocolVoilation_SP(Action: "GETRISKEVENT_log", PROTVOIL_ID: Request.QueryString["RiskId"].ToString());
                if (ds != null)
                {
                    grdReviewHistory.DataSource = ds;
                    grdReviewHistory.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BINDComments()
        {
            try
            {
                //commments
                DataSet ds = dal.ProtocolVoilation_SP(Action: "GET_PRE_COMMENT", PROTVOIL_ID: Request.QueryString["RiskId"].ToString());                
                if (ds != null)
                {
                    grdCmts.DataSource = ds;
                    grdCmts.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvMitigation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["MITID"] = id;
                if (e.CommandName == "Complete")
                {
                    Complete(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    DELETE(id);
                }
                bind_Mitigation();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE(string id)
        {
            try
            {
                dal.Risk_Mitigation_SP(Action: "DELETE", ID: id);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Complete(string id)
        {
            try
            {
                dal.Risk_Mitigation_SP(Action: "Complete", ID: id);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
            bind_Mitigation();
        }

        protected void gvMitigation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string Date_Complete = drv["Date_Complete"].ToString();
                    LinkButton lbtncompleteMit = (e.Row.FindControl("lbtncompleteMit") as LinkButton);

                    if (Date_Complete == "")
                    {
                        lbtncompleteMit.Visible = true;
                    }
                    else
                    {
                        lbtncompleteMit.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //Risk Cause
        private void bind_Cause()
        {
            try
            {
                DataSet ds = dal.Risk_Cause_SP(Action: "GET", Event_ID: txtRiskID.Text);
                gvCause.DataSource = ds.Tables[0];
                gvCause.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void DELETE_Cause(string id)
        {
            try
            {
                dal.Risk_Cause_SP(Action: "DELETE", ID: id);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRefreshCause_Click(object sender, EventArgs e)
        {
            try
            {
                bind_Cause();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvCause_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["CAUSEID"] = id;
                if (e.CommandName == "Delete1")
                {
                    DELETE_Cause(id);
                }
                bind_Cause();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void AddAnticipated()
        {
            try
            {
                dal.insertRiskBucket(Action: "ADD", ProjectId: Session["PROJECTID"].ToString(), RCat: ddlcategory.SelectedValue, RSubCat: ddlsubcategory.SelectedValue, RFactor: ddlfactor.SelectedValue, RManager: Session["User_ID"].ToString());
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void CheckAnticipation()
        {
            try
            {
                string msg = dal.insertRiskBucket(Action: "CHECK", ProjectId: Session["PROJECTID"].ToString(), RCat: ddlcategory.SelectedValue, RSubCat: ddlsubcategory.SelectedValue, RFactor: ddlfactor.SelectedValue);
                if (msg == "")
                {
                    if (ddlcategory.SelectedIndex != 0 && ddlsubcategory.SelectedIndex != 0 && ddlfactor.SelectedIndex != 0)
                    {
                        divAddAnticip.Visible = true;
                    }
                    else
                    {
                        divAddAnticip.Visible = false;
                    }
                }
                else
                {
                    divAddAnticip.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddAnticip_Click(object sender, EventArgs e)
        {
            try
            {
                AddAnticipated();
                CheckAnticipation();
                if (divAddAnticip.Visible)
                {
                    Response.Write("<script> alert('This combination of Category, Sub-Category and Factor is not Available in Risk Bank. ')</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void GridView_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                    || (gv.ShowHeaderWhenEmpty == true))
                {
                    //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gv.ShowFooter == true && gv.Rows.Count > 0)
                {
                    //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                    gv.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void grdCmts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string Comment = dr["Comment"].ToString();
                    string flag = dr["UPDATE_FLAG_cmt"].ToString();
                    //string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    //string FIELDNAME = dr["FIELDNAME"].ToString();
                    //string CLASS = dr["CLASS"].ToString();

                    TextBox txtComment = (TextBox)e.Row.FindControl("Comment");
                    if (flag == "1")
                    {
                        txtComment.ReadOnly = true;
                    }




                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void bntCommentAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Table Structure
                DataRow drCurrentRow = null;
                DataTable dtCurrentTable;
                int rowIndex = 0;
                int i;
                dtCurrentTable = new DataTable();
                dtCurrentTable.Columns.Add(new DataColumn("Comment", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("UPDATE_FLAG_cmt", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("DTENTERED", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("ENTEREDBY", typeof(string)));



                if (grdCmts.Rows.Count > 0)
                {
                    for (i = 0; i < grdCmts.Rows.Count; i++)
                    {

                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["Comment"] = ((TextBox)grdCmts.Rows[rowIndex].FindControl("Comment")).Text;
                        drCurrentRow["UPDATE_FLAG_cmt"] = ((TextBox)grdCmts.Rows[rowIndex].FindControl("UPDATE_FLAG_cmt")).Text;
                        drCurrentRow["DTENTERED"] = ((Label)grdCmts.Rows[rowIndex].FindControl("DTENTERED")).Text;
                        drCurrentRow["ENTEREDBY"] = ((Label)grdCmts.Rows[rowIndex].FindControl("ENTEREDBY")).Text;

                        dtCurrentTable.Rows.Add(drCurrentRow);

                        rowIndex++;
                    }


                    if (((TextBox)grdCmts.Rows[rowIndex - 1].FindControl("Comment")).Text != "")
                    {
                        //Add Empty Row
                        drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["Comment"] = "";

                        drCurrentRow["UPDATE_FLAG_cmt"] = "0";
                        drCurrentRow["DTENTERED"] = "";
                        drCurrentRow["ENTEREDBY"] = "";
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }


                }
                grdCmts.DataSource = dtCurrentTable;
                grdCmts.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}