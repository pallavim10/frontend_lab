using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using CTMS.CommonFunction;

namespace Risk_Management
{
    public partial class UncodedEvents : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_ID"] == null)
            {
                Response.Redirect("SessionExpired.aspx");
            }
            try
            {
                if (!IsPostBack)
                {
                    FillAETYPE();
                    FillINV();
                    GetData();
                    GetRefrence();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        public void GetData()
        {
            try
            {
                Session["INVID"] = drpSite.SelectedItem.Value;
                DataSet ds = new DataSet();
                ds = dal.GetUncodedEvents(Action: "ALL_DATA", ProjectID: Session["ProjectId"].ToString(), INVID: drpSite.SelectedItem.Value, SUBJID: drpSubject.SelectedItem.Value,AETYPE:drpAEType.SelectedValue);
                if (ds != null)
                {
                    grdUncodedAE.DataSource = ds.Tables[0];
                    grdUncodedAE.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillAETYPE();
                FillSubject();
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }
        public void FillINV()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.GetUncodedEvents(Action: "Get_Subject", ProjectID: Session["ProjectId"].ToString(), UserID: Session["User_ID"].ToString());
                if (ds != null)
                {
                    drpSite.Items.Clear();
                    drpSite.DataSource = ds.Tables[0];
                    drpSite.DataTextField = "INVID";
                    drpSite.DataValueField = "INVID";
                    drpSite.DataBind();
                    drpSite.Items.Insert(0, new ListItem("--All--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }

           
        }
        public void FillSubject()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.GetUncodedEvents(Action: "Get_Subject", ProjectID: Session["PROJECTID"].ToString(), INVID: drpSite.SelectedItem.Value, UserID: Session["User_ID"].ToString());
                if (ds != null)
                {
                    drpSubject.Items.Clear();
                    drpSubject.DataSource = ds.Tables[1];
                    drpSubject.DataTextField = "SUBJID";
                    drpSubject.DataValueField = "SUBJID";
                    drpSubject.DataBind();
                    drpSubject.Items.Insert(0, new ListItem("--All--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }

         
        }
        public void GetRefrence()
        {
            try
            {
                //Set Refrence//
                DataSet ds = new DataSet();
                ds = dal.GetSeriousEvents(Action: "GET_REFRENCE", ProjectID: Session["ProjectId"].ToString(), Filter_Type: "Uncoded Event");
                if (ds != null)
                {
                    lblRefrence.Text = ds.Tables[0].Rows[0]["Refrence"].ToString();
                }
                ds = dal.getDDLValue(Action: "GET_REFRENCE", Project_ID: Session["ProjectId"].ToString(), SERVICE: "AE", VARIABLENAME: "FILTERS");
                if (ds != null)
                {
                    drpFilters.Items.Clear();
                    drpFilters.DataSource = ds.Tables[0];
                    drpFilters.DataTextField = "TEXT";
                    drpFilters.DataValueField = "VALUE";
                    drpFilters.DataBind();
                    drpFilters.Items.Insert(0, new ListItem("--All--", "99"));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void grdEventList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Session["Page"] = "MM_UncodedEvents.aspx";
                if (e.CommandName == "Subject")
                {
                    int SubjectID = Convert.ToInt32(e.CommandArgument);
                    Session["Subject"] = SubjectID;

                    Response.Redirect("EventsBySubject.aspx");
                }
                if (e.CommandName == "Code")
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    int SubjectID = Convert.ToInt32((gvr.FindControl("lnk_SUBJID") as LinkButton).Text);

                    Session["Subject"] = SubjectID;
                    Session["EventCode"] = e.CommandArgument.ToString();
                    Response.Redirect("MM_AdverseEventsDetails.aspx");
                }  
                if (e.CommandName == "Query")
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    string InvID = (gvr.FindControl("lblInvID") as Label).Text;
                    string SubjectID = (gvr.FindControl("lnk_SUBJID") as LinkButton).Text;
                    string EventCode = (gvr.FindControl("lblCode") as LinkButton).Text;
                    string EventTerm = (gvr.FindControl("lblETerm") as Label).Text;
                    string PCode = (gvr.FindControl("lblAEPTC") as Label).Text;
                    string QueryDetail = "This event is uncoded.";
                    string Source = "Adverse Events";
                    string Rule = "Uncoded Event";
                    string Status = "1";
                    string RuleType = "Rule3";

                    DataSet ds = new DataSet();
                    ds = dal.SetQueryAE(Action: "Raise_Query_AE", ProjectID: Session["ProjectId"].ToString(),
                    INVID: InvID, SUBJID: SubjectID, EventCode: EventCode, EventTerm: EventTerm, PCode: PCode, QueryDetail: QueryDetail
                    , Rule: Rule, Status: Status, RuleType: RuleType, UserID: Session["User_ID"].ToString(),Source:Source);

                  //----------------------------------------Email Section---------------------------------------//
                    CommonFunction CF = new CommonFunction();
                    DataSet dsEmailDetails;
                    dsEmailDetails = dal.Get_Email_Details
                    (
                    ProjectID: Session["ProjectId"].ToString(),
                    INVID: InvID,
                    Event_ID: "50"
                    );
                    if (dsEmailDetails.Tables[0].Rows.Count > 0)
                    {
                        DataRow drEmailDetails = dsEmailDetails.Tables[0].Rows[0];
                        string E_Address, E_CcAdd, E_Subject, E_Body;
                        E_Body = "Query is Raised For Site Id: " + InvID + ", Subject Id: " + SubjectID + ", Event Code: " + EventCode + ", Rule: " + Rule + ", Query Id:" + ds.Tables[0].Rows[0]["QueryID"].ToString() + " by User: " + Session["User_Name"].ToString() + ".";
                        E_Address = drEmailDetails["E_TO"].ToString();
                        E_CcAdd = drEmailDetails["E_CC"].ToString();
                        E_Subject = "Protocol ID-" + Session["PROJECTIDTEXT"].ToString() + ":Query Alert. Site Id: " + InvID + ", Subject Id: " + SubjectID + ", Rule: " + Rule + ".";
                        CF.Email_Users(E_Address, E_CcAdd, E_Subject, E_Body);
                    }
                    //----------------------------------------Email Section---------------------------------------//
                    GetData();
                }
                if (e.CommandName == "Reviewed")
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    string InvID = (gvr.FindControl("lblInvID") as Label).Text;
                    string SubjectID = (gvr.FindControl("lnk_SUBJID") as LinkButton).Text;
                    string EventCode = (gvr.FindControl("lblCode") as LinkButton).Text;
                    string EventTerm = (gvr.FindControl("lblETerm") as Label).Text;
                    string PCode = (gvr.FindControl("lblAEPTC") as Label).Text;
                    string QueryDetail = "This event is uncoded.";
                    string Rule = "Uncoded Event";
                    string Status = "3";
                    string RuleType = "Rule3";

                    DataSet ds = new DataSet();
                    ds = dal.SetQueryAE(Action: "Reviewed_AE", ProjectID: Session["ProjectId"].ToString(),
                    INVID: InvID, SUBJID: SubjectID, EventCode: EventCode, EventTerm: EventTerm, PCode: PCode, QueryDetail: QueryDetail
                    , Rule: Rule, Status: Status, RuleType: RuleType, UserID: Session["User_ID"].ToString());
                     if (ds.Tables[0].Rows[0]["ReturnVal"].ToString() == "Query Reviewed")
                    {
                        //----------------------------------------Email Section---------------------------------------//
                        CommonFunction CF = new CommonFunction();
                        DataSet dsEmailDetails;
                        dsEmailDetails = dal.Get_Email_Details
                        (
                        ProjectID: Session["ProjectId"].ToString(),
                        INVID: InvID,
                        Event_ID: "53"
                        );
                        if (dsEmailDetails.Tables[0].Rows.Count > 0)
                        {
                            DataRow drEmailDetails = dsEmailDetails.Tables[0].Rows[0];
                            string E_Address, E_CcAdd, E_Subject, E_Body;
                            E_Body = "Query Id: " + ds.Tables[0].Rows[0]["QueryID"].ToString() + " is Reviewed By User: " + Session["User_Name"].ToString() + ".";
                            E_Address = drEmailDetails["E_TO"].ToString();
                            E_CcAdd = drEmailDetails["E_CC"].ToString();
                            E_Subject = "Protocol ID-" + Session["PROJECTIDTEXT"].ToString() + ":Query Reviewed. Site Id: " + InvID + ", Subject Id: " + SubjectID + ", Rule: " + Rule + ", Event Code: " + EventCode + ".";
                            CF.Email_Users(E_Address, E_CcAdd, E_Subject, E_Body);
                        }
                        //----------------------------------------Email Section---------------------------------------//
                    }
                     if (ds.Tables[0].Rows[0]["ReturnVal"].ToString() == "Data Reviewed")
                     {
                         //----------------------------------------Email Section---------------------------------------//
                         CommonFunction CF = new CommonFunction();
                         DataSet dsEmailDetails;
                         dsEmailDetails = dal.Get_Email_Details
                         (
                         ProjectID: Session["ProjectId"].ToString(),
                         INVID: InvID,
                         Event_ID: "54"
                         );
                         if (dsEmailDetails.Tables[0].Rows.Count > 0)
                         {
                             DataRow drEmailDetails = dsEmailDetails.Tables[0].Rows[0];
                             string E_Address, E_CcAdd, E_Subject, E_Body;
                             E_Body = "Site Id: " + InvID + ", Subject Id: " + SubjectID + ", Rule: " + Rule + ", Event Code: " + EventCode + " is Reviewed by User: " + Session["User_Name"].ToString() + ".";
                             E_Address = drEmailDetails["E_TO"].ToString();
                             E_CcAdd = drEmailDetails["E_CC"].ToString();
                             E_Subject = "Protocol ID-" + Session["PROJECTIDTEXT"].ToString() + ":Data Reviewed. Site Id: " + InvID + ", Subject Id: " + SubjectID + ", Rule: " + Rule + ".";
                             CF.Email_Users(E_Address, E_CcAdd, E_Subject, E_Body);
                         }
                         //----------------------------------------Email Section---------------------------------------//
                     }
                    GetData();
                }  
            
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
        protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillAETYPE();
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
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

        protected void drpFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpFilters.SelectedValue == "99")
                {
                    GetData();
                }
                else
                {
                    Session["INVID"] = drpSite.SelectedItem.Value;
                    DataSet ds = new DataSet();
                    ds = dal.GetMM_SP_AE_FILTER(Action: drpFilters.SelectedValue, ProjectID: Session["ProjectId"].ToString(), INVID: drpSite.SelectedItem.Value, SUBJID: drpSubject.SelectedItem.Value, RULE: "Uncoded Event", RULE1: "Rule3",AETYPE:drpAEType.SelectedValue);
                    if (ds != null)
                    {
                        grdUncodedAE.DataSource = ds.Tables[0];
                        grdUncodedAE.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        protected void grdUncodedAE_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    int QueryCount = Convert.ToInt32(dr["QueryCount"].ToString());
                    string ReviewedStatus = dr["ReviewedStatus"].ToString();

                    if (QueryCount > 0)
                    {
                        e.Row.Cells[12].Attributes.Add("Style", "background-color: red");
                    }
                    if (ReviewedStatus == "Reviewed")
                    {
                        e.Row.Cells[12].Attributes.Add("Style", "background-color: Green");
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void drpAEType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }
        public void FillAETYPE()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = new DataSet();
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "AE", VARIABLENAME: "AETYPE");
                dal.BindDropDown(drpAEType, ds.Tables[0]);
                drpAEType.Items.RemoveAt(0);
                drpAEType.Items.Insert(0, new ListItem("--ALL--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }


        }
       

        //public void FillSubject()
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds = dal.GetUncodedEvents(Action: "Get_Subject");
        //        if (ds != null)
        //        {
        //            drpSubject.Items.Clear();
        //            drpSubject.DataSource = ds.Tables[0];
        //            drpSubject.DataTextField = "SUBJID";
        //            drpSubject.DataValueField = "SUBJID";
        //            drpSubject.DataBind();
        //            drpSubject.Items.Insert(0, new ListItem("All", "0"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.ToString();
        //        throw;
        //    }
        //}

    }
}