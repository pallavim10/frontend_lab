using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class ProtDev : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        DAL dal = new DAL();

        string ISSUEID;
        string PROTVOILID;

        protected void Page_Load(object sender, EventArgs e)
        {
            ISSUEID = Request.QueryString["ISSUEID"];
            PROTVOILID = Request.QueryString["PROTVOIL_ID"];
            hdPROTVOILID.Value = Request.QueryString["PROTVOIL_ID"];
            lblPROTVOILID.Text = Request.QueryString["PROTVOIL_ID"];

            if (Request.QueryString["UserType"] != null)
            {
                hdnUserType.Value = Request.QueryString["UserType"].ToString();
            }
            else
            {
                hdnUserType.Value = "";
            }

            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["PROJECTID"] != null)
                    {
                        dal.ProtocolVoilation_SP(Action: "InsertProtocolViolation_log", PROTVOIL_ID: PROTVOILID);

                        FILL_CATEGORY();

                        GetData();

                        FILL_VISITS();

                        GET_PDMASTERID();

                        BindRelatedGrid();

                        //bind Impact
                        DataSet ds;
                        ds = new DataSet();

                        //CAPA Responsibility
                        ds = dal.ProtocolVoilation_SP(Action: "GETEMPDATA");

                        ddlResponsibility.DataSource = ds;
                        ddlResponsibility.DataTextField = "TEXT";
                        ddlResponsibility.DataValueField = "TEXT";
                        ddlResponsibility.DataBind();
                    }

                    CheckAccess();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void CheckAccess()
        {
            try
            {
                if (Request.QueryString["UserType"] != null)
                {
                    if (Request.QueryString["UserType"].ToString() == "Medical")
                    {
                        Drp_Project.Enabled = false;
                        Drp_Project.CssClass = "form-control drpControl";

                        drp_InvID.Enabled = false;
                        drp_InvID.CssClass = "form-control drpControl";

                        drp_SUBJID.Enabled = false;
                        drp_SUBJID.CssClass = "form-control drpControl";

                        drp_DEPT.Enabled = false;
                        drp_DEPT.CssClass = "form-control drpControl";

                        ddlVISITNUM.Enabled = false;
                        ddlVISITNUM.CssClass = "form-control drpControl";

                        drp_Status.Enabled = false;
                        drp_Status.CssClass = "form-control drpControl";

                        txtSource.Enabled = false;
                        txtSource.CssClass = "form-control drpControl";

                        txtReference.Enabled = false;
                        txtReference.CssClass = "form-control drpControl";

                        ddlDuplicacy.Enabled = false;
                        ddlDuplicacy.CssClass = "form-control drpControl";

                        //drp_Priority_Med.Enabled = false;
                        //drp_Priority_Med.CssClass = "form-control drpControl";

                        drp_Priority_Ops.Enabled = false;
                        drp_Priority_Ops.CssClass = "form-control drpControl";

                        drp_Priority_Final.Enabled = false;
                        drp_Priority_Final.CssClass = "form-control drpControl";

                        txtDescription.Enabled = false;
                        txtDescription.CssClass = "form-control drpControl";

                        txtOCDate.Enabled = false;
                        txtOCDate.CssClass = "form-control drpControl";

                        txtCloseDate.Enabled = false;
                        txtCloseDate.CssClass = "form-control drpControl";

                        txtPdmasterID.Enabled = false;
                        txtPdmasterID.CssClass = "form-control drpControl";

                        txtCount.Enabled = false;
                        txtCount.CssClass = "form-control drpControl";

                        txtSummary.Enabled = false;
                        txtSummary.CssClass = "form-control drpControl";

                        txtRationalise.Enabled = false;
                        txtRationalise.CssClass = "form-control drpControl";

                        divImpactAdd.Visible = false;
                        divCAPAAdd.Visible = false;
                    }
                    else if (Request.QueryString["UserType"].ToString() == "Stats")
                    {
                        Drp_Project.Enabled = false;
                        Drp_Project.CssClass = "form-control drpControl";

                        drp_InvID.Enabled = false;
                        drp_InvID.CssClass = "form-control drpControl";

                        drp_SUBJID.Enabled = false;
                        drp_SUBJID.CssClass = "form-control drpControl";

                        drp_DEPT.Enabled = false;
                        drp_DEPT.CssClass = "form-control drpControl";

                        ddlVISITNUM.Enabled = false;
                        ddlVISITNUM.CssClass = "form-control drpControl";

                        drp_Status.Enabled = false;
                        drp_Status.CssClass = "form-control drpControl";

                        txtSource.Enabled = false;
                        txtSource.CssClass = "form-control drpControl";

                        txtReference.Enabled = false;
                        txtReference.CssClass = "form-control drpControl";

                        drp_Nature.Enabled = false;
                        drp_Nature.CssClass = "form-control drpControl";

                        drp_PDCode1.Enabled = false;
                        drp_PDCode1.CssClass = "form-control drpControl";

                        drp_PDCode2.Enabled = false;
                        drp_PDCode2.CssClass = "form-control drpControl";

                        ddlDuplicacy.Enabled = false;
                        ddlDuplicacy.CssClass = "form-control drpControl";

                        drp_Priority_Med.Enabled = false;
                        drp_Priority_Med.CssClass = "form-control drpControl";

                        //drp_Priority_Ops.Enabled = false;
                        //drp_Priority_Ops.CssClass = "form-control drpControl";

                        drp_Priority_Final.Enabled = false;
                        drp_Priority_Final.CssClass = "form-control drpControl";

                        txtDescription.Enabled = false;
                        txtDescription.CssClass = "form-control drpControl";

                        txtOCDate.Enabled = false;
                        txtOCDate.CssClass = "form-control drpControl";

                        txtCloseDate.Enabled = false;
                        txtCloseDate.CssClass = "form-control drpControl";

                        txtPdmasterID.Enabled = false;
                        txtPdmasterID.CssClass = "form-control drpControl";

                        txtCount.Enabled = false;
                        txtCount.CssClass = "form-control drpControl";

                        txtSummary.Enabled = false;
                        txtSummary.CssClass = "form-control drpControl";

                        txtRationalise.Enabled = false;
                        txtRationalise.CssClass = "form-control drpControl";

                        divImpactAdd.Visible = false;
                        divCAPAAdd.Visible = false;
                    }
                    else if (Request.QueryString["UserType"].ToString() == "Sponsor")
                    {
                        Drp_Project.Enabled = false;
                        Drp_Project.CssClass = "form-control drpControl";

                        drp_InvID.Enabled = false;
                        drp_InvID.CssClass = "form-control drpControl";

                        drp_SUBJID.Enabled = false;
                        drp_SUBJID.CssClass = "form-control drpControl";

                        drp_DEPT.Enabled = false;
                        drp_DEPT.CssClass = "form-control drpControl";

                        ddlVISITNUM.Enabled = false;
                        ddlVISITNUM.CssClass = "form-control drpControl";

                        drp_Status.Enabled = false;
                        drp_Status.CssClass = "form-control drpControl";

                        txtSource.Enabled = false;
                        txtSource.CssClass = "form-control drpControl";

                        txtReference.Enabled = false;
                        txtReference.CssClass = "form-control drpControl";

                        drp_Nature.Enabled = false;
                        drp_Nature.CssClass = "form-control drpControl";

                        drp_PDCode1.Enabled = false;
                        drp_PDCode1.CssClass = "form-control drpControl";

                        drp_PDCode2.Enabled = false;
                        drp_PDCode2.CssClass = "form-control drpControl";

                        ddlDuplicacy.Enabled = false;
                        ddlDuplicacy.CssClass = "form-control drpControl";

                        drp_Priority_Med.Enabled = false;
                        drp_Priority_Med.CssClass = "form-control drpControl";

                        drp_Priority_Ops.Enabled = false;
                        drp_Priority_Ops.CssClass = "form-control drpControl";

                        drp_Priority_Final.Enabled = false;
                        drp_Priority_Final.CssClass = "form-control drpControl";

                        txtDescription.Enabled = false;
                        txtDescription.CssClass = "form-control drpControl";

                        txtOCDate.Enabled = false;
                        txtOCDate.CssClass = "form-control drpControl";

                        txtCloseDate.Enabled = false;
                        txtCloseDate.CssClass = "form-control drpControl";

                        txtPdmasterID.Enabled = false;
                        txtPdmasterID.CssClass = "form-control drpControl";

                        txtCount.Enabled = false;
                        txtCount.CssClass = "form-control drpControl";

                        txtSummary.Enabled = false;
                        txtSummary.CssClass = "form-control drpControl";

                        //txtRationalise.Enabled = false;
                        //txtRationalise.CssClass = "form-control drpControl";

                        divImpactAdd.Visible = false;
                        divCAPAAdd.Visible = false;
                    }
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
                DataSet ds = dal.ProtocolVoilation_SP(
                Action: "GET_DATA_RELATED",
                PROTVOIL_ID: hdPROTVOILID.Value,
                Source: hdnUserType.Value
                );
                grdRelatedPROTOCOL.DataSource = ds;
                grdRelatedPROTOCOL.DataBind();

                DataSet dsSubject = dal.ProtocolVoilation_SP(
                Action: "GET_DATA_RELATED_SUBJECT",
                PROTVOIL_ID: hdPROTVOILID.Value,
                SUBJID: lblSubjectID.Text,
                Source: hdnUserType.Value
                );
                grdSubjectRelatedPROTOCOL.DataSource = dsSubject;
                grdSubjectRelatedPROTOCOL.DataBind();

                lblSite.Text = drp_InvID.SelectedValue;
                DataSet dsSite = dal.ProtocolVoilation_SP(
                Action: "GET_DATA_RELATED_SITE",
                PROTVOIL_ID: hdPROTVOILID.Value,
                SUBJID: lblSubjectID.Text,
                INVID: drp_InvID.SelectedValue,
                Source: hdnUserType.Value
                   );

                grdSiteRelatedProtocol.DataSource = dsSite;
                grdSiteRelatedProtocol.DataBind();
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

                DAL dal;
                dal = new DAL();
                dal.ProtocolVoilation_SP(Action: "INSERT",
                PROTVOIL_ID: PROTVOILID,
                PDMaster_ID: txtPdmasterID.Text,
                Project_ID: Drp_Project.SelectedValue,
                INVID: drp_InvID.SelectedValue,
                SUBJID: drp_SUBJID.SelectedValue,
                VISITNUM: ddlVISITNUM.SelectedValue,
                Description: txtDescription.Text,
                Summary: txtSummary.Text,
                Nature: drp_Nature.SelectedValue,
                PDCODE1: drp_PDCode1.SelectedValue,
                PDCODE2: drp_PDCode2.SelectedValue,
                Department: drp_DEPT.SelectedValue,
                Status: drp_Status.SelectedValue,
                Source: txtSource.Text,
                Refrence: txtReference.Text,
                Priority_Default: lbl_Priority_Default.Text,
                Priority_Ops: drp_Priority_Ops.SelectedValue,
                Priority_Med: drp_Priority_Med.SelectedValue,
                Priority_Final: drp_Priority_Final.SelectedValue,
                Rationalise: txtRationalise.Text,
                Dt_Otcome: txtOCDate.Text,
                Close_Date: txtCloseDate.Text,
                UPDATEDBY: Session["User_ID"].ToString(),
                DUPLICATE: ddlDuplicacy.SelectedValue,
                Dt_IRB: txtIRBDate.Text
                );

                Response.Write("<script> alert('Protocol Deviation Updated Successfully.'); </script>");
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "POST_TO_RISK();", true);
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        private void GetData()
        {
            try
            {
                DataSet ds;
                ds = new DataSet();

                //Get PD Data
                ds = new DataSet();
                ds = dal.ProtocolVoilation_SP(Action: "GET_DATA", Project_ID: Drp_Project.SelectedValue, PROTVOIL_ID: PROTVOILID);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Drp_Project.Items.Add(new ListItem(Session["PROJECTIDTEXT"].ToString(), Session["PROJECTID"].ToString()));
                        drp_InvID.Items.Add(new ListItem(ds.Tables[0].Rows[0]["INVID"].ToString(), ds.Tables[0].Rows[0]["INVID"].ToString()));
                        drp_SUBJID.Items.Add(new ListItem(ds.Tables[0].Rows[0]["SUBJID"].ToString(), ds.Tables[0].Rows[0]["SUBJID"].ToString()));
                        lblSubjectID.Text = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                        ddlVISITNUM.SelectedValue = ds.Tables[0].Rows[0]["VISITNUM"].ToString();

                        txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                        txtSummary.Text = ds.Tables[0].Rows[0]["Summary"].ToString();
                        txtReference.Text = ds.Tables[0].Rows[0]["Refrence"].ToString();
                        txtSource.Text = ds.Tables[0].Rows[0]["Source"].ToString();

                        txtRationalise.Text = ds.Tables[0].Rows[0]["Rationalise"].ToString();
                        txtOCDate.Text = ds.Tables[0].Rows[0]["Dt_OTCome"].ToString();
                        txtCloseDate.Text = ds.Tables[0].Rows[0]["Close_Date"].ToString();

                        lbl_Priority_Default.Text = ds.Tables[0].Rows[0]["Priority_Default"].ToString();
                        drp_Priority_Ops.SelectedValue = ds.Tables[0].Rows[0]["Priority_Ops"].ToString();
                        drp_Priority_Med.SelectedValue = ds.Tables[0].Rows[0]["Priority_Med"].ToString();
                        drp_Priority_Final.SelectedValue = ds.Tables[0].Rows[0]["Priority_Final"].ToString();

                        ddlDuplicacy.SelectedValue = ds.Tables[0].Rows[0]["DUPLICATE"].ToString();
                        txtDateIdentified.Text = ds.Tables[0].Rows[0]["IdentifyDT"].ToString();

                        drp_DEPT.SelectedValue = ds.Tables[0].Rows[0]["Department"].ToString();
                        if (ds.Tables[0].Rows[0]["Status"].ToString() == "New")
                        {
                            drp_Status.SelectedValue = "Active";
                        }
                        else
                        {
                            drp_Status.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                        }



                        if (ds.Tables[0].Rows[0]["Nature"].ToString() != "")
                        {
                            drp_Nature.SelectedValue = ds.Tables[0].Rows[0]["Nature"].ToString();
                        }

                        FILL_SUB_CATEGORY();
                        if (ds.Tables[0].Rows[0]["PDCODE1"].ToString() != "")
                        {
                            drp_PDCode1.SelectedValue = ds.Tables[0].Rows[0]["PDCODE1"].ToString();
                        }
                        FILL_FACTOR();
                        if (ds.Tables[0].Rows[0]["PDCODE2"].ToString() != "")
                        {
                            drp_PDCode2.SelectedValue = ds.Tables[0].Rows[0]["PDCODE2"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Dt_IRB"].ToString() != "")
                        {
                            txtIRBDate.Text = ds.Tables[0].Rows[0]["Dt_IRB"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Count"].ToString() != "0")
                        {
                            btnPost.Enabled = false;
                            btnPost.Text = "Already posted to Issue";
                        }
                    }
                }

                //set Active date and Active by

                ds = dal.ProtocolVoilation_SP(Action: "Active", PROTVOIL_ID: PROTVOILID, ActiveBy: Session["User_ID"].ToString());

                //commments

                ds = dal.getsetPDComments(Action: "GET_DATA", PROTVOIL_ID: PROTVOILID);
                if (ds != null)
                {
                    grdCmts.DataSource = ds;
                    grdCmts.DataBind();
                }
                //Impact
                ds = dal.getsetPDImpact(Action: "GET_DATA", PROTVOIL_ID: PROTVOILID);
                if (ds != null)
                {
                    grdImpact.DataSource = ds;
                    grdImpact.DataBind();
                }

                //CAPA

                ds = dal.getsetPDCAPA(Action: "GET_DATA", PROTVOIL_ID: PROTVOILID);
                if (ds != null)
                {
                    grdCAPA.DataSource = ds;
                    grdCAPA.DataBind();
                    grdCAPA.Visible = true;
                }

                ds = dal.ProtocolVoilation_SP(Action: "GET_ProtocolViolation_AuditTrail", PROTVOIL_ID: PROTVOILID);
                if (ds != null)
                {
                    grdAuditTrail.DataSource = ds;
                    grdAuditTrail.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FILL_VISITS()
        {
            try
            {
                DataSet ds = dal_DM.DM_VISIT_DATA_SP(SUBJID: drp_SUBJID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVISITNUM.DataSource = ds.Tables[0];
                    ddlVISITNUM.DataValueField = "VISIT";
                    ddlVISITNUM.DataTextField = "VISIT";
                    ddlVISITNUM.DataBind();
                    ddlVISITNUM.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        private void GET_PDMASTERID()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.ProtocolVoilation_SP(
                Action: "Get_PDMASTERID",
                 Nature: drp_Nature.SelectedValue,
                PDCODE1: drp_PDCode1.SelectedValue,
                PDCODE2: drp_PDCode2.SelectedValue,
                Source: hdnUserType.Value
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtPdmasterID.Text = ds.Tables[0].Rows[0]["PDMaster_ID"].ToString();
                }
                txtCount.Text = ds.Tables[1].Rows[0]["Count"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //private void fill_PDCode1()
        //{
        //    try
        //    {
        //        DAL dal;
        //        dal = new DAL();
        //        DataSet ds = dal.getsetPDCode(
        //        Action: "Get_SUbCategoryPD",
        //        PDCODE1:  drp_Nature.SelectedValue
        //        );
        //        drp_PDCode1.DataSource = ds.Tables[0];
        //        drp_PDCode1.DataValueField = "SubCategoryID";
        //        drp_PDCode1.DataTextField = "SubCategory";
        //        drp_PDCode1.DataBind();
        //        drp_PDCode1.Items.Insert(0, new ListItem("--Select--", "0"));
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        //private void fill_PDCode2()
        //{
        //    try
        //    {
        //        DAL dal;
        //        dal = new DAL();
        //        DataSet ds = dal.getsetPDCode(
        //        Action: "Get_FactorPD",
        //        PDCODE1: drp_Nature.SelectedValue,
        //        PDCODE2: drp_PDCode1.SelectedValue
        //        );
        //        drp_PDCode2.DataSource = ds.Tables[0];
        //        drp_PDCode2.DataValueField = "FactorID";
        //        drp_PDCode2.DataTextField = "Factor";
        //        drp_PDCode2.DataBind();
        //        drp_PDCode2.Items.Insert(0, new ListItem("--Select--", "0"));
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

        private void FILL_CATEGORY()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.getsetPDCode(
                Action: "FILL_CATEGORY"
                );
                drp_Nature.DataSource = ds.Tables[0];
                drp_Nature.DataValueField = "Category";
                drp_Nature.DataTextField = "Category";
                drp_Nature.DataBind();
                drp_Nature.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void FILL_SUB_CATEGORY()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.getsetPDCode(
                Action: "FILL_SUB_CATEGORY",
                CATEGORY: drp_Nature.SelectedValue
                );
                drp_PDCode1.DataSource = ds.Tables[0];
                drp_PDCode1.DataValueField = "Subcategory";
                drp_PDCode1.DataTextField = "Subcategory";
                drp_PDCode1.DataBind();
                drp_PDCode1.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void FILL_FACTOR()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.getsetPDCode(
                Action: "FILL_FACTOR",
                CATEGORY: drp_Nature.SelectedValue,
                SUB_CATEGORY: drp_PDCode1.SelectedValue
                );
                drp_PDCode2.DataSource = ds.Tables[0];
                drp_PDCode2.DataValueField = "Factor";
                drp_PDCode2.DataTextField = "Factor";
                drp_PDCode2.DataBind();
                drp_PDCode2.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void drp_Nature_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FILL_SUB_CATEGORY();
                GET_PDMASTERID();

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drp_PDCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FILL_FACTOR();
                GET_PDMASTERID();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drp_PDCode2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = dal.getsetPDCode(Action: "GET_CLASSIFICATION",
            CATEGORY: drp_Nature.SelectedValue,
            SUB_CATEGORY: drp_PDCode1.SelectedValue,
            FACTOR: drp_PDCode2.SelectedValue
            );

            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_Priority_Default.Text = ds.Tables[0].Rows[0]["Classification"].ToString();
            }
            else
            {
                lbl_Priority_Default.Text = "";
            }

            GET_PDMASTERID();
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
                DataSet ds2 = new DataSet();

                ds2 = dal.getsetPDComments(
                Action: "INSERT",
                PROTVOIL_ID: PROTVOILID,
                Comment: txtComments.Text
                );

                txtComments.Text = "";
                GetData_Comment();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ClickNav('li4');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdatedComment_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds2 = new DataSet();

                ds2 = dal.getsetPDComments(
                Action: "UPDATE",
                PROTVOIL_ID: PROTVOILID,
                Comment: txtComments.Text,
                PDCOM_ID: ViewState["PDCOM_ID"].ToString()
                );

                txtComments.Text = "";
                btnUpdatedComment.Visible = false;
                bntCommentAdd.Visible = true;
                GetData_Comment();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ClickNav('li4');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GetData_Comment()
        {
            try
            {
                DataSet ds = dal.getsetPDComments(Action: "GET_DATA", PROTVOIL_ID: PROTVOILID);
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

        protected void bntImpactAdd_Click(object sender, EventArgs e)
        {
            try
            {

                dal.getsetPDImpact(
                   Action: "INSERT",
                   PROTVOIL_ID: PROTVOILID,
                   Impact: txtImpact.Text
                        );

                GetData_IMPACT();
                txtImpact.Text = "";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ClickNav('li5');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void bntImpactUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds2 = new DataSet();
                ds2 = dal.getsetPDImpact(
                Action: "UPDATE",
                PROTVOIL_ID: PROTVOILID,
                Impact: txtImpact.Text,
                PD_Imapct_ID: ViewState["PD_Imapct_ID"].ToString()
                     );

                GetData_IMPACT();
                txtImpact.Text = "";
                bntImpactUpdate.Visible = false;
                bntImpactAdd.Visible = true;

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ClickNav('li5');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GetData_IMPACT()
        {
            try
            {
                DataSet ds = dal.getsetPDImpact(Action: "GET_DATA", PROTVOIL_ID: PROTVOILID);
                if (ds != null)
                {
                    grdImpact.DataSource = ds;
                    grdImpact.DataBind();
                }
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
                    //string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    //string FIELDNAME = dr["FIELDNAME"].ToString();
                    //string CLASS = dr["CLASS"].ToString();

                    DropDownList btnEdit = (DropDownList)e.Row.FindControl("Impact");

                    DAL dal;
                    dal = new DAL();
                    DataSet ds;
                    ds = new DataSet();

                    ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "PDIMPACT", VARIABLENAME: "PDIMPACT");

                    btnEdit.DataSource = ds;
                    btnEdit.DataTextField = "TEXT";
                    btnEdit.DataValueField = "VALUE";
                    btnEdit.DataBind();

                    ListItem selectedListItem = btnEdit.Items.FindByValue(Impact);
                    if (selectedListItem != null)
                    {
                        selectedListItem.Selected = true;
                    }


                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdCAPA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string Responsibility = dr["Responsibility"].ToString();
                    //string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    //string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CAPA = dr["CAPA"].ToString();

                    DropDownList btnEdit = (DropDownList)e.Row.FindControl("Responsibility");
                    LinkButton lbtndelete = (LinkButton)e.Row.FindControl("lbtndelete");

                    if (CAPA != "")
                    {
                        lbtndelete.Visible = true;
                    }
                    else
                    {
                        lbtndelete.Visible = false;
                    }

                    DAL dal;
                    dal = new DAL();
                    DataSet ds;
                    ds = new DataSet();

                    //ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "PD", VARIABLENAME: "Responsibility");
                    ds = dal.ProtocolVoilation_SP(Action: "GETEMPDATA");

                    btnEdit.DataSource = ds;
                    btnEdit.DataTextField = "TEXT";
                    btnEdit.DataValueField = "VALUE";
                    btnEdit.DataBind();

                    ListItem selectedListItem = btnEdit.Items.FindByValue(Responsibility);
                    if (selectedListItem != null)
                    {
                        selectedListItem.Selected = true;
                    }


                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdRelatedPROTOCOL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string DUPLICATE = drv["DUPLICATE"].ToString();

                    Label lblDuplicateCheck = (e.Row.FindControl("lblDuplicateCheck") as Label);
                    Label lblDuplicateCheckPossible = (e.Row.FindControl("lblDuplicateCheckPossible") as Label);
                    Label lblDuplicateUnCheck = (e.Row.FindControl("lblDuplicateUnCheck") as Label);

                    if (DUPLICATE == "1")
                    {
                        lblDuplicateCheckPossible.Visible = true;
                    }
                    else if (DUPLICATE == "2")
                    {
                        lblDuplicateCheck.Visible = true;
                    }
                    else
                    {
                        lblDuplicateUnCheck.Visible = true;
                    }

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

        protected void grdCmts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                ViewState["PDCOM_ID"] = id;

                if (e.CommandName == "EditComment")
                {
                    DataSet ds = dal.getsetPDComments(Action: "EditComment", PDCOM_ID: id);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtComments.Text = ds.Tables[0].Rows[0]["Comment"].ToString();
                    }

                    btnUpdatedComment.Visible = true;
                    bntCommentAdd.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ClickNav('li4');", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdImpact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                ViewState["PD_Imapct_ID"] = id;

                if (e.CommandName == "EditImpact")
                {
                    DataSet ds = dal.getsetPDImpact(Action: "EditImpact", PD_Imapct_ID: id);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtImpact.Text = ds.Tables[0].Rows[0]["Impact"].ToString();
                    }

                    bntImpactUpdate.Visible = true;
                    bntImpactAdd.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ClickNav('li5');", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void bntCAPAAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds2 = new DataSet();

                ds2 = dal.getsetPDCAPA(
                Action: "INSERT",
                PROTVOIL_ID: PROTVOILID,
                CAPA: txtCAPA.Text,
                Responsibility: ddlResponsibility.SelectedValue,
                Resolution_DT: txtResDate.Text
                     );

                txtCAPA.Text = "";
                ddlResponsibility.SelectedIndex = 0;
                txtResDate.Text = "";

                GetData_CAPA();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ClickNav('li6');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void bntCAPAUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds2 = new DataSet();

                ds2 = dal.getsetPDCAPA(
                Action: "UPDATE",
                PROTVOIL_ID: PROTVOILID,
                CAPA: txtCAPA.Text,
                Responsibility: ddlResponsibility.SelectedValue,
                Resolution_DT: txtResDate.Text,
                PDCAPA_ID: ViewState["PDCAPA_ID"].ToString()
                     );

                txtCAPA.Text = "";
                ddlResponsibility.SelectedIndex = 0;
                txtResDate.Text = "";

                bntCAPAAdd.Visible = true;
                bntCAPAUpdate.Visible = false;

                GetData_CAPA();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ClickNav('li6');", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GetData_CAPA()
        {
            try
            {
                DataSet ds = dal.getsetPDCAPA(Action: "GET_DATA", PROTVOIL_ID: PROTVOILID);
                if (ds != null)
                {
                    grdCAPA.DataSource = ds;
                    grdCAPA.DataBind();
                    grdCAPA.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdCAPA_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                ViewState["PDCAPA_ID"] = id;
                //dal.getsetPDCAPA(Action: "DeleteRow", PDCAPA_ID: id);

                //DataSet ds = dal.getsetPDCAPA(Action: "GET_DATA", PROTVOIL_ID: PROTVOILID);
                //if (ds != null)
                //{
                //    grdCAPA.DataSource = ds;
                //    grdCAPA.DataBind();
                //    grdCAPA.Visible = true;
                //}

                if (e.CommandName == "EditCAPA")
                {
                    DataSet ds = dal.getsetPDCAPA(Action: "EditCAPA", PDCAPA_ID: id);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtCAPA.Text = ds.Tables[0].Rows[0]["CAPA"].ToString();
                        ddlResponsibility.SelectedValue = ds.Tables[0].Rows[0]["Responsibility"].ToString();
                        txtResDate.Text = ds.Tables[0].Rows[0]["Resolution_DT"].ToString();
                    }

                    bntCAPAUpdate.Visible = true;
                    bntCAPAAdd.Visible = false;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ClickNav('li6');", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}