using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class IssueDetails : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ID = Request.QueryString["IssueID"];
                hdnISSUEID.Value = ID;
                if (!this.IsPostBack)
                {
                    if (Session["User_ID"] == null)
                    {
                        Response.Redirect("~/SessionExpired.aspx", false);
                        // Server.MapPath("~/SessionExpired.aspx");
                    }
                    fill_PDCode1();
                    getdrpdata();
                    getdata();
                    BindRelatedGrid();
                    UpdatePSD();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void getdrpdata()
        {
            try
            {

                DAL dal;
                dal = new DAL();

                DataSet ds = new DataSet();


                //ds = new DataSet();
                //ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Status");
                //dal.BindDropDown(drp_Status, ds.Tables[0]);

                ds = new DataSet();
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Status");
                dal.BindDropDown(drp_Status, ds.Tables[0]);

                ds = new DataSet();
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Priority");
                dal.BindDropDown(drp_Priority, ds.Tables[0]);

                dal = new DAL();
                ds = dal.getsetPDCode(
                Action: "Get_Nature",
                NATURE: drp_Nature.SelectedValue,
                PDCODE1: drp_PDCode1.SelectedValue
                );
                drp_Nature.DataSource = ds.Tables[0];
                drp_Nature.DataValueField = "NatureID";
                drp_Nature.DataTextField = "Nature";
                drp_Nature.DataBind();
                drp_Nature.Items.Insert(0, new ListItem("--Select--", "0"));



                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "RootCause");
                dal.BindDropDown(drpRootCause, ds.Tables[0]);



                DataSet dsEmp = dal.EmpMaster_SP(Action: "VIEW");
                Drp_Assginedto.DataSource = dsEmp.Tables[0];
                Drp_Assginedto.DataValueField = "ID";
                Drp_Assginedto.DataTextField = "Name";
                Drp_Assginedto.DataBind();
                Drp_Assginedto.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void getdata()
        {
            try
            {
                DataSet ds = new DataSet();
                DAL dal = new DAL();
                DataTable dt = new DataTable();
                ds = dal.getsetISSUES(Action: "GET_DATA", ISSUES_ID: ID);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        hdnID.Value = dt.Rows[0]["ISSUES_ID"].ToString();
                        txtSummary.Text = dt.Rows[0]["Summary"].ToString();
                        txtSiteId.Text = dt.Rows[0]["INVID"].ToString();
                        txtSubjId.Text = dt.Rows[0]["SUBJID"].ToString();
                        txtProj.Text = dt.Rows[0]["Project_ID"].ToString();
                        txtDept.Text = dt.Rows[0]["Department"].ToString();

                        if (dt.Rows[0]["Status"].ToString() == "New")
                        {
                            drp_Status.SelectedValue = "Active";
                        }
                        else
                        {
                            drp_Status.SelectedValue = dt.Rows[0]["Status"].ToString();
                        }
                        //drp_Nature.SelectedItem.Text = dt.Rows[0]["Nature"].ToString();

                        drp_Nature.SelectedValue = dt.Rows[0]["Nature"].ToString();

                        txtOpenDat.Text = dt.Rows[0]["ISSUEDate"].ToString();
                        txtOpenBy.Text = dt.Rows[0]["ISSUEBy"].ToString();

                        drp_Priority.SelectedValue = dt.Rows[0]["Priority"].ToString();
                        txtDueDate.Text = dt.Rows[0]["DueDate"].ToString();
                        Drp_Assginedto.SelectedItem.Text = dt.Rows[0]["AssignedTo"].ToString();
                        txtResolution.Text = dt.Rows[0]["ResolutionDate"].ToString();
                        txtDateClose.Text = dt.Rows[0]["DateClosed"].ToString();
                        txtDesc.Text = dt.Rows[0]["Description"].ToString();
                        drp_PDCode1.SelectedValue = dt.Rows[0]["PDCODE1"].ToString();

                        fill_PDCode2();
                        drp_PDCode2.SelectedValue = dt.Rows[0]["PDCODE2"].ToString();

                        BindFactor();
                        drp_Factor.SelectedValue = dt.Rows[0]["FactorID"].ToString();

                        txtSource.Text = dt.Rows[0]["Source"].ToString();
                        txtReference.Text = dt.Rows[0]["Refrence"].ToString();

                        if (dt.Rows[0]["Count"].ToString() != "0")
                        {
                            btnPost.Enabled = false;
                            btnPost.Text = "Already posted to Risk";
                        }

                    }
                }

                //ds = dal.getsetProtocolVoilation(Action: "GET_DATA", ISSUES_ID: ID);
                //if (ds != null)
                //{
                //    dt = ds.Tables[0];
                //    fill_PDCode1();

                //    drp_PDCode1.SelectedValue=dt.Rows[0]["PDCODE1"].ToString();
                //    fill_PDCode2();
                //    drp_PDCode2.SelectedValue=dt.Rows[0]["PDCODE2"].ToString();
                //}

                ds = dal.getsetIssueComments(Action: "GET_DATA", ISSUES_ID: ID);
                if (ds != null)
                {
                    grdCmts.DataSource = ds;
                    grdCmts.DataBind();
                }

                ds = dal.getsetIssueRootCause(Action: "GET_DATA", ISSUES_ID: ID);
                if (ds != null)
                {
                    grdRootCause.DataSource = ds;
                    grdRootCause.DataBind();
                }
                ds = dal.getsetIssueActionable(Action: "GET_DATA", ISSUES_ID: ID);
                if (ds != null)
                {
                    grdActionable.DataSource = ds;
                    grdActionable.DataBind();
                }

                //get Impact
                ds = dal.getsetIssueImpact(Action: "GET_DATA", ISSUES_ID: ID);
                if (ds != null)
                {
                    grdImpact.DataSource = ds;
                    grdImpact.DataBind();
                }

                ds = dal.getsetAttachments(Action: "GET_DATA", ISSUES_ID: ID);
                if (ds != null)
                {
                    grdAttachment.DataSource = ds;
                    grdAttachment.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void UpdatePSD()
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds = new DataSet();
                ds = dal.getEventCount(
                Category: drp_PDCode1.SelectedValue.ToString(),
                SubCategory: drp_PDCode2.SelectedValue.ToString(),
                Factor: drp_Factor.SelectedValue.ToString(),
                ProjectId: "1"
                );

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtRiskID.Text = ds.Tables[0].Rows[0]["RiskID"].ToString();
                        txtRiskCount.Text = ds.Tables[0].Rows[0]["count"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_Nature.SelectedValue == "Protocol Deviation" || drp_Nature.SelectedValue == "Process Deviation")
                {
                    if (drp_PDCode1.SelectedValue == "0")
                    {
                        throw new System.ArgumentException("PD Code is Mandatory");
                    }
                }


                if (drp_Nature.SelectedValue == "Protocol Deviation" || drp_Nature.SelectedValue == "Process Deviation")
                {
                    if (drp_PDCode2.SelectedValue == "0")
                    {
                        throw new System.ArgumentException("PD Code1 is Mandatory");
                    }
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "POST_TO_RISK();", true);

                //int P =  Convert.ToInt32(drpProbability.SelectedValue);
                //int S = Convert.ToInt32(drpSeverity.SelectedValue);
                //int D = Convert.ToInt32(drpDetectability.SelectedValue);
                //string RPT = Convert.ToString(P * S * D);

                //DAL dal = new DAL();
                //DataSet ds = new DataSet();
                //ds = dal.getsetRisk_SP(Action: "INSERT",
                //RISK_ID: txtRiskID.Text,
                //ISSUES_ID: hdnID.Value,
                //INVID: txtSiteId.Text,
                //Project_ID: txtProj.Text,
                //SUBJID: txtSubjId.Text,
                //Department: txtDept.Text,
                //DateIdentified: txtOpenDat.Text,
                //IdentifiedBy: Session["User_Name"].ToString(),
                //RiskDescription: txtDesc.Text,
                //RiskStatus: "Risk",
                //ENTEREDBY: Session["User_ID"].ToString(),
                //Source: txtSource.Text,
                //Refrence: txtReference.Text,
                //Nature: drp_Nature.SelectedItem.Text,
                //PDCODE1: drp_PDCode1.SelectedItem.Value,
                //PD1Catagory: drp_PDCode1.SelectedItem.Text,
                //PDCODE2: drp_PDCode2.SelectedItem.Value,
                //PD2Catagory: drp_PDCode2.SelectedItem.Text
                //);

                //dal.insertRiskEvent(CategoryId: drp_PDCode1.SelectedValue,
                //SubcategoryId: drp_PDCode2.SelectedValue,
                //TopicId: drp_Factor.SelectedValue,
                //Risk_Description: txtDesc.Text,
                //    //Impacts:txtDesc.Text,
                //Possible_Mitigations: "",
                //    //SugestedRiskCategory:txt
                //RiskNature: drp_Nature.SelectedValue,
                //    //TranscelerateCategory:
                //ProjectId: Session["PROJECTID"].ToString(),
                //    //Risk_Notes:
                //Risk_Count: txtRiskCount.Text,
                //Risk_Date: DateTime.Now.ToString(),
                //Risk_Manager: Session["User_ID"].ToString(),
                //Dept: txtDept.Text,
                //issueId: ID,
                //Source: txtSource.Text,
                //Reference: txtReference.Text,
                //RStatus: drp_Status.SelectedItem.Text,
                //ROwner: Drp_Assginedto.SelectedItem.Text
                //    //RType:
                //);



                //Response.Write("<script> alert('Record Inserted successfully.')</script>");
                //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_Nature.SelectedValue == "Protocol Deviation" || drp_Nature.SelectedValue == "Process Deviation")
                {
                    if (drp_PDCode1.SelectedValue == "0")
                    {
                        throw new System.ArgumentException("Category is Mandatory");
                    }
                }


                if (drp_Nature.SelectedValue == "Protocol Deviation" || drp_Nature.SelectedValue == "Process Deviation")
                {
                    if (drp_PDCode2.SelectedValue == "0")
                    {
                        throw new System.ArgumentException("Sub Category is Mandatory");
                    }
                }


                DAL dal;
                dal = new DAL();
                DataSet ds = new DataSet();
                ds = dal.getsetISSUES(Action: "UPDATE",
                ISSUES_ID: hdnID.Value,
                INVID: txtSiteId.Text,
                Project_ID: txtProj.Text,
                SUBJID: txtSubjId.Text,
                Department: txtDept.Text,
                Summary: txtSummary.Text,
                Status: drp_Status.SelectedValue,
                Nature: drp_Nature.SelectedItem.Text,
                ISSUEDate: txtOpenDat.Text,
                Priority: drp_Priority.SelectedValue,
                DueDate: txtDueDate.Text,
                AssignedTo: Drp_Assginedto.SelectedItem.Text,
                ResolutionDate: txtResolution.Text,
                DateClosed: txtDateClose.Text,
                Description: txtDesc.Text,
                PDCODE1: drp_PDCode1.SelectedItem.Value,
                PD1Catagory: drp_PDCode1.SelectedItem.Text,
                PDCODE2: drp_PDCode2.SelectedItem.Value,
                PD2Catagory: drp_PDCode2.SelectedItem.Text,
                Source: txtSource.Text,
                Refrence: txtReference.Text,
                FactorID: Convert.ToInt32(drp_Factor.SelectedValue),
                Factor: drp_Factor.SelectedItem.Text
                );

                int UPDATE_FLAG_Impact;
                //Save to Protocol Deviation
                //if (drp_Nature.SelectedItem.Text == "Protocol Deviation" || drp_Nature.SelectedItem.Text=="Process Deviation" )
                //{

                //    DataSet dspv = new DataSet();
                //    dspv = dal.getsetProtocolVoilation(
                //    Action: "INSERT",
                //    ISSUES_ID: ID,
                //    Project_ID: txtProj.Text,
                //    INVID: txtSiteId.Text,
                //    SUBJID: txtSubjId.Text,
                //    Department: txtDept.Text,
                //    Description: txtDesc.Text,
                //    PDCODE1: drp_PDCode1.SelectedValue,
                //    PDCODE2: drp_PDCode2.SelectedValue,
                //    Summary: txtSummary.Text,
                //    Nature:drp_Nature.SelectedValue,
                //    Status:drp_Status.SelectedValue,
                //    Source:txtSource.Text,
                //    Refrence:txtReference.Text,
                //    Priority_Ops:drp_Priority.SelectedValue

                //    );


                //    //PDImpact Save
                //    string PROTVOILID = dspv.Tables[0].Rows[0]["PROTVOIL_ID"].ToString();
                //    int rindex5;
                //    for (rindex5 = 0; rindex5 < grdImpact.Rows.Count; rindex5++)
                //    {



                //        UPDATE_FLAG_Impact = Convert.ToInt16(((TextBox)grdImpact.Rows[rindex5].FindControl("UPDATE_FLAG_Impact")).Text);
                //        if (UPDATE_FLAG_Impact == 0)
                //        {
                //            if (((DropDownList)grdImpact.Rows[rindex5].FindControl("Impact")).SelectedValue != "0")
                //            {
                //                DataSet ds2 = new DataSet();
                //                ds2 = dal.getsetPDImpact(
                //                Action: "INSERT",
                //                PROTVOIL_ID: PROTVOILID,
                //                Impact: ((DropDownList)grdImpact.Rows[rindex5].FindControl("Impact")).SelectedValue,
                //                Project_ID: Session["PROJECTID"].ToString(),
                //                ENTEREDBY: Session["User_ID"].ToString()
                //                     );
                //            }
                //        }
                //        else
                //        {
                //            if (((DropDownList)grdImpact.Rows[rindex5].FindControl("Impact")).SelectedValue != "0")
                //            {
                //                DataSet ds2 = new DataSet();
                //                ds2 = dal.getsetPDImpact(
                //                Action: "UPDATE",
                //                PROTVOIL_ID: PROTVOILID,
                //                Impact: ((DropDownList)grdImpact.Rows[rindex5].FindControl("Impact")).SelectedValue,
                //                Project_ID: Session["PROJECTID"].ToString(),
                //                UPDATEDBY: Session["User_ID"].ToString()
                //                     );
                //            }
                //        }
                //    }

                //}

                int rowindex, UPDATE_FLAG_cmt;
                for (rowindex = 0; rowindex < grdCmts.Rows.Count; rowindex++)
                {
                    UPDATE_FLAG_cmt = Convert.ToInt16(((TextBox)grdCmts.Rows[rowindex].FindControl("UPDATE_FLAG_cmt")).Text);
                    if (UPDATE_FLAG_cmt == 0)
                    {
                        if (((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text != "")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetIssueComments(
                            Action: "INSERT",
                            ISSUES_ID: hdnID.Value,
                            Comment: ((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text,
                            ENTEREDBY: Session["User_ID"].ToString()
                            );
                        }
                    }
                    else
                    {
                        if (((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text != "")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetIssueComments(
                            Action: "UPDATE",
                            ISSUES_ID: hdnID.Value,
                            Comment: ((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text,
                            UPDATEDBY: Session["User_ID"].ToString()
                            );
                        }
                    }

                }

                //int rindex, UPDATE_FLAG_Root;
                //for (rindex = 0; rindex < grdRootCause.Rows.Count; rindex++)
                //{



                //    UPDATE_FLAG_Root = Convert.ToInt16(((TextBox)grdRootCause.Rows[rindex].FindControl("UPDATE_FLAG_Root")).Text);
                //    if (UPDATE_FLAG_Root == 0)
                //    {
                //        if (((DropDownList)grdRootCause.Rows[rindex].FindControl("RootCause")).SelectedValue != "0")
                //        {
                //            DataSet ds2 = new DataSet();
                //            ds2 = dal.getsetIssueRootCause(
                //            Action: "INSERT",
                //            ISSUES_ID: hdnID.Value,
                //            RootCause: ((DropDownList)grdRootCause.Rows[rindex].FindControl("RootCause")).SelectedValue,
                //            ENTEREDBY: Session["User_ID"].ToString()
                //                 );
                //        }
                //    }
                //    else
                //    {
                //        if (((DropDownList)grdRootCause.Rows[rindex].FindControl("RootCause")).SelectedValue != "0")
                //        {
                //            DataSet ds2 = new DataSet();
                //            ds2 = dal.getsetIssueRootCause(
                //            Action: "UPDATE",
                //            ISSUES_ID: hdnID.Value,
                //            RootCause: ((DropDownList)grdRootCause.Rows[rindex].FindControl("RootCause")).SelectedValue,
                //            UPDATEDBY: Session["User_ID"].ToString()
                //                 );
                //        }
                //    }


                //}

                int rindex2, UPDATE_FLAG_Action;
                for (rindex2 = 0; rindex2 < grdActionable.Rows.Count; rindex2++)
                {
                    UPDATE_FLAG_Action = Convert.ToInt16(((TextBox)grdActionable.Rows[rindex2].FindControl("UPDATE_FLAG_Action")).Text);
                    if (UPDATE_FLAG_Action == 0)
                    {
                        if (((TextBox)grdActionable.Rows[rindex2].FindControl("Actionable")).Text != "")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetIssueActionable(
                            Action: "INSERT",
                            ISSUES_ID: hdnID.Value,
                            Actionable: ((TextBox)grdActionable.Rows[rindex2].FindControl("Actionable")).Text,
                            ActionBy: ((TextBox)grdActionable.Rows[rindex2].FindControl("ActionBy")).Text,
                              DueDate: ((TextBox)grdActionable.Rows[rindex2].FindControl("DueDate")).Text,
                              DateCompleted: ((TextBox)grdActionable.Rows[rindex2].FindControl("DateCompleted")).Text,
                            ENTEREDBY: Session["User_ID"].ToString()
                            );
                        }
                    }
                    else
                    {
                        if (((TextBox)grdActionable.Rows[rindex2].FindControl("Actionable")).Text != "")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetIssueActionable(
                            Action: "UPDATE",
                            ISSUES_ID: hdnID.Value,
                            Actionable: ((TextBox)grdActionable.Rows[rindex2].FindControl("Actionable")).Text,
                            ActionBy: ((TextBox)grdActionable.Rows[rindex2].FindControl("ActionBy")).Text,
                              DueDate: ((TextBox)grdActionable.Rows[rindex2].FindControl("DueDate")).Text,
                              DateCompleted: ((TextBox)grdActionable.Rows[rindex2].FindControl("DateCompleted")).Text,
                            UPDATEDBY: Session["User_ID"].ToString()
                            );
                        }
                    }

                }

                //IssueImpact Save

                int rindex3;
                for (rindex3 = 0; rindex3 < grdImpact.Rows.Count; rindex3++)
                {



                    UPDATE_FLAG_Impact = Convert.ToInt16(((TextBox)grdImpact.Rows[rindex3].FindControl("UPDATE_FLAG_Impact")).Text);
                    if (UPDATE_FLAG_Impact == 0)
                    {
                        if (((DropDownList)grdImpact.Rows[rindex3].FindControl("Impact")).SelectedValue != "0")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetIssueImpact(
                            Action: "INSERT",
                            ISSUES_ID: ID,
                            Impact: ((DropDownList)grdImpact.Rows[rindex3].FindControl("Impact")).SelectedValue,
                            Project_ID: Session["PROJECTID"].ToString(),
                            ENTEREDBY: Session["User_ID"].ToString()
                                 );
                        }
                    }
                    else
                    {
                        if (((DropDownList)grdImpact.Rows[rindex3].FindControl("Impact")).SelectedValue != "0")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetIssueImpact(
                            Action: "UPDATE",
                            ISSUES_ID: ID,
                            Impact: ((DropDownList)grdImpact.Rows[rindex3].FindControl("Impact")).SelectedValue,
                            Project_ID: Session["PROJECTID"].ToString(),
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
                lblErrorMsg.Text = ex.Message.ToString();
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

        protected void bntRootCauseAdd_Click(object sender, EventArgs e)
        {
            try
            {

                ////Table Structure
                //DataRow drCurrentRow = null;
                //DataTable dtCurrentTable;
                //int rowIndex = 0;
                //int i;
                //dtCurrentTable = new DataTable();
                //dtCurrentTable.Columns.Add(new DataColumn("ISSUES_ID", typeof(string)));
                //dtCurrentTable.Columns.Add(new DataColumn("RootCause", typeof(string)));
                //dtCurrentTable.Columns.Add(new DataColumn("UPDATE_FLAG_Root", typeof(string)));

                //if (grdRootCause.Rows.Count > 0)
                //{
                //    for (i = 0; i < grdRootCause.Rows.Count; i++)
                //    {
                //        drCurrentRow = dtCurrentTable.NewRow();

                //        drCurrentRow["ISSUES_ID"] = ((TextBox)grdRootCause.Rows[rowIndex].FindControl("ISSUES_ID")).Text;
                //        drCurrentRow["RootCause"] = ((DropDownList)grdRootCause.Rows[rowIndex].FindControl("RootCause")).SelectedValue;
                //        drCurrentRow["UPDATE_FLAG_Root"] = ((TextBox)grdRootCause.Rows[rowIndex].FindControl("UPDATE_FLAG_Root")).Text;

                //        dtCurrentTable.Rows.Add(drCurrentRow);
                //        rowIndex++;
                //    }

                //    if (((DropDownList)grdRootCause.Rows[rowIndex - 1].FindControl("RootCause")).SelectedValue != "0")
                //    {
                //        //Add Empty Row
                //        drCurrentRow = dtCurrentTable.NewRow();
                //        //drCurrentRow = dtCurrentTable.NewRow();
                //        drCurrentRow["ISSUES_ID"] = ID;
                //        drCurrentRow["RootCause"] = "";
                //        drCurrentRow["UPDATE_FLAG_Root"] = "0";
                //        dtCurrentTable.Rows.Add(drCurrentRow);     

                //    }


                //}

                DAL dal;
                dal = new DAL();
                DataSet ds2 = new DataSet();
                ds2 = dal.getsetIssueRootCause(
                Action: "INSERT",
                ISSUES_ID: hdnID.Value,
                RootCause: drpRootCause.SelectedValue,
                Sub_cause: drpSubRootCause.SelectedValue,
                ENTEREDBY: Session["User_ID"].ToString()
                );
                grdRootCause.DataSource = ds2;
                grdRootCause.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void bntActionAdd_Click(object sender, EventArgs e)
        {
            try
            {

                //Table Structure
                DataRow drCurrentRow = null;
                DataTable dtCurrentTable;
                int rowIndex = 0;
                int i;
                dtCurrentTable = new DataTable();
                dtCurrentTable.Columns.Add(new DataColumn("Actionable", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("ActionBy", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("DueDate", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("DateCompleted", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("UPDATE_FLAG_Action", typeof(string)));

                if (grdActionable.Rows.Count > 0)
                {
                    for (i = 0; i < grdActionable.Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["Actionable"] = ((TextBox)grdActionable.Rows[rowIndex].FindControl("Actionable")).Text;
                        drCurrentRow["ActionBy"] = ((TextBox)grdActionable.Rows[rowIndex].FindControl("ActionBy")).Text;
                        drCurrentRow["DueDate"] = ((TextBox)grdActionable.Rows[rowIndex].FindControl("DueDate")).Text;
                        drCurrentRow["DateCompleted"] = ((TextBox)grdActionable.Rows[rowIndex].FindControl("DateCompleted")).Text;
                        drCurrentRow["UPDATE_FLAG_Action"] = ((TextBox)grdActionable.Rows[rowIndex].FindControl("UPDATE_FLAG_Action")).Text;

                        dtCurrentTable.Rows.Add(drCurrentRow);
                        rowIndex++;
                    }



                    if (((TextBox)grdActionable.Rows[rowIndex - 1].FindControl("Actionable")).Text != "" &&
                        ((TextBox)grdActionable.Rows[rowIndex - 1].FindControl("ActionBy")).Text != "" &&
                        ((TextBox)grdActionable.Rows[rowIndex - 1].FindControl("DueDate")).Text != "" &&
                        ((TextBox)grdActionable.Rows[rowIndex - 1].FindControl("DateCompleted")).Text != "")
                    {
                        //Add Empty Row
                        drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Actionable"] = "";
                        drCurrentRow["ActionBy"] = "";
                        drCurrentRow["DueDate"] = "";
                        drCurrentRow["DateCompleted"] = "";
                        drCurrentRow["UPDATE_FLAG_Action"] = "0";
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                }
                grdActionable.DataSource = dtCurrentTable;
                grdActionable.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdRootCause_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string RootCause = dr["RootCause"].ToString();
                    string SubRootCause = dr["Sub_cause"].ToString();
                    //string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    //string FIELDNAME = dr["FIELDNAME"].ToString();
                    //string CLASS = dr["CLASS"].ToString();

                    DropDownList btnEdit = (DropDownList)e.Row.FindControl("RootCause");
                    DropDownList drpsubrootcause = (DropDownList)e.Row.FindControl("SubRootCause");

                    DAL dal;
                    dal = new DAL();
                    DataSet ds;
                    ds = new DataSet();

                    ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "RootCause");

                    btnEdit.DataSource = ds;
                    btnEdit.DataTextField = "TEXT";
                    btnEdit.DataValueField = "VALUE";
                    btnEdit.DataBind();

                    ListItem selectedListItem = btnEdit.Items.FindByValue(RootCause);
                    if (selectedListItem != null)
                    {
                        selectedListItem.Selected = true;
                    }

                    //drp subrootcause
                    ds = dal.getsetIssueRootCause(
                    Action: "SubCause",
                    RootCause: "Training"

                         );

                    drpsubrootcause.DataSource = ds;
                    drpsubrootcause.DataTextField = "SubCause";
                    drpsubrootcause.DataValueField = "SubCause";
                    drpsubrootcause.DataBind();

                    ListItem selectedListItemSR = drpsubrootcause.Items.FindByValue(SubRootCause);
                    if (selectedListItemSR != null)
                    {
                        selectedListItemSR.Selected = true;
                    }


                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }


        protected void DownloadFile(object sender, EventArgs e)
        {

            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", false);
                }
                else
                {

                    string id = (sender as LinkButton).CommandArgument;

                    byte[] bytes;
                    string fileName, contentType;
                    DAL dal = new DAL();
                    DataSet ds = new DataSet();
                    ds = dal.getsetAttachments(
                    Action: "GET_DATA",
                    ISSUES_ID: id);


                    bytes = (byte[])ds.Tables[0].Rows[0]["Attachments"];
                    contentType = ds.Tables[0].Rows[0]["ContentType"].ToString();
                    fileName = ds.Tables[0].Rows[0]["Name"].ToString();



                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "Inline; filename=" + fileName);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }

        }

        //protected void drp_Nature_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        fill_PDCode1();
        //        fill_PDCode2();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }

        //}

        protected void drp_PDCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_PDCode2();
                UpdatePSD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_PDCode1()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataTable ds = dal.getcategory(
                Action: "Category"
                );
                drp_PDCode1.DataSource = ds;
                drp_PDCode1.DataValueField = "Id";
                drp_PDCode1.DataTextField = "Description";
                drp_PDCode1.DataBind();
                //drp_PDCode1.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void fill_PDCode2()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataTable ds = dal.getcategory(
                Action: "SubCategory",
                Categoryvalue: drp_PDCode1.SelectedValue.ToString()
                );
                drp_PDCode2.DataSource = ds;
                drp_PDCode2.DataValueField = "Id";
                drp_PDCode2.DataTextField = "Description";
                drp_PDCode2.DataBind();
                //drp_PDCode2.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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

        private void BindRelatedGrid()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.getsetRelatedIssues(
                Action: "GET_DATA",
                ISSUES_ID: hdnID.Value
                );
                grdRelatedIssues.DataSource = ds;
                grdRelatedIssues.DataBind();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void bntImpactAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow drCurrentRow = null;
                DataTable dtCurrentTable;
                int rowIndex = 0;
                int i;
                dtCurrentTable = new DataTable();
                dtCurrentTable.Columns.Add(new DataColumn("ISSUES_ID", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("Impact", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("UPDATE_FLAG_Impact", typeof(string)));

                if (grdImpact.Rows.Count > 0)
                {
                    for (i = 0; i < grdImpact.Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["ISSUES_ID"] = ((TextBox)grdImpact.Rows[rowIndex].FindControl("ISSUES_ID")).Text;
                        drCurrentRow["Impact"] = ((DropDownList)grdImpact.Rows[rowIndex].FindControl("Impact")).SelectedValue;
                        drCurrentRow["UPDATE_FLAG_Impact"] = ((TextBox)grdImpact.Rows[rowIndex].FindControl("UPDATE_FLAG_Impact")).Text;

                        dtCurrentTable.Rows.Add(drCurrentRow);
                        rowIndex++;
                    }

                    if (((DropDownList)grdImpact.Rows[rowIndex - 1].FindControl("Impact")).SelectedValue != "0")
                    {
                        //Add Empty Row
                        drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["ISSUES_ID"] = ID;
                        drCurrentRow["Impact"] = "";
                        drCurrentRow["UPDATE_FLAG_Impact"] = "0";
                        dtCurrentTable.Rows.Add(drCurrentRow);

                    }


                }
                grdImpact.DataSource = dtCurrentTable;
                grdImpact.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdImpact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string Impact = dr["Impact"].ToString();
                    string flag = dr["UPDATE_FLAG_Impact"].ToString();
                    //string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    //string FIELDNAME = dr["FIELDNAME"].ToString();
                    //string CLASS = dr["CLASS"].ToString();

                    DropDownList btnEdit = (DropDownList)e.Row.FindControl("Impact");

                    DAL dal;
                    dal = new DAL();
                    DataSet ds;
                    ds = new DataSet();

                    ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "PD", VARIABLENAME: "Impact");

                    btnEdit.DataSource = ds;
                    btnEdit.DataTextField = "TEXT";
                    btnEdit.DataValueField = "VALUE";
                    btnEdit.DataBind();

                    ListItem selectedListItem = btnEdit.Items.FindByValue(Impact);
                    if (selectedListItem != null)
                    {
                        selectedListItem.Selected = true;
                    }
                    DropDownList drpImpact = (DropDownList)e.Row.FindControl("Impact");
                    if (flag == "1")
                    {
                        drpImpact.Enabled = false;
                    }


                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }


        private void SubCause()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.getsetIssueRootCause(
                Action: "SubCause",
                RootCause: "Training"

                );
                drp_PDCode2.DataSource = ds.Tables[0];
                drp_PDCode2.DataValueField = "PDCODE2";
                drp_PDCode2.DataTextField = "PDCatagory";
                drp_PDCode2.DataBind();
                drp_PDCode2.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpRootCause_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.getsetIssueRootCause(
                Action: "SubCause",
                RootCause: drpRootCause.SelectedValue
                );
                drpSubRootCause.DataSource = ds.Tables[0];
                drpSubRootCause.DataValueField = "SubCause";
                drpSubRootCause.DataTextField = "SubCause";
                drpSubRootCause.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void fillRootCause()
        {
            try
            {
                DataSet ds;
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "RootCause");

                drpRootCause.DataSource = ds;
                drpRootCause.DataTextField = "TEXT";
                drpRootCause.DataValueField = "VALUE";
                drpRootCause.DataBind();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void drp_PDCode2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindFactor();
                UpdatePSD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void BindFactor()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataTable ds = dal.getcategory(
                Action: "Factor",
                SubCategoryvalue: drp_PDCode2.SelectedValue
                );
                drp_Factor.DataSource = ds;
                drp_Factor.DataValueField = "Id";
                drp_Factor.DataTextField = "Description";
                drp_Factor.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drp_Factor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdatePSD();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}