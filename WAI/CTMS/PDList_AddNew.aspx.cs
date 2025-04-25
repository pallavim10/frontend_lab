using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class PDList_AddNew : System.Web.UI.Page
    {
        DAL dal = new DAL();

        //string INVID = "";
        //string SubjectID = "";
        //string Source = "";
        //string Department = "";
        string ISSUEID;
        string PROTVOILID;

        protected void Page_Load(object sender, EventArgs e)
        {
            ISSUEID = Request.QueryString["ISSUEID"];
            PROTVOILID = Request.QueryString["PROTVOIL_ID"];
            try
            {
                if (!this.IsPostBack)
                {
                    fill_Project();
                    FillINV();
                    FillSubject();
                    getdrpdata();
                    GetData();
                    GET_PDMASTERID();

                    txtDateIdentified.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
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

                DAL dal;
                dal = new DAL();
                DataSet ds = dal.ProtocolVoilation_SP(Action: "INSERT_ProtocolViolation",
                 PROTVOIL_ID: PROTVOILID,
                 PDMaster_ID: txtPdmasterID.Text,
                 ISSUES_ID: ISSUEID,
                 Project_ID: Drp_Project.SelectedValue,
                 INVID: drp_InvID.SelectedValue,
                 SUBJID: drp_SUBJID.SelectedValue,
                 VISITNUM: txtVISITNUM.Text,
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
                 Priority_Med: drp_Priority_Final.SelectedValue,
                 Priority_Final: drp_Priority_Final.SelectedValue,
                 Rationalise: txtRationalise.Text,
                 Dt_Otcome: txtOCDate.Text,
                 Close_Date: txtCloseDate.Text,
                 UPDATEDBY: Session["User_ID"].ToString()
                 );


                PROTVOILID = ds.Tables[0].Rows[0]["PROTVOIL_ID"].ToString();
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
                            ds2 = dal.getsetPDComments(
                            Action: "INSERT",
                            PROTVOIL_ID: PROTVOILID,
                            Comment: ((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text
                            );
                        }
                    }
                    else
                    {
                        if (((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text != "")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetPDComments(
                            Action: "UPDATE",
                            PROTVOIL_ID: PROTVOILID,
                            Comment: ((TextBox)grdCmts.Rows[rowindex].FindControl("Comment")).Text
                            );
                        }
                    }
                }

                //PDImpact Save

                int rindex, UPDATE_FLAG_Impact;
                for (rindex = 0; rindex < grdImpact.Rows.Count; rindex++)
                {



                    UPDATE_FLAG_Impact = Convert.ToInt16(((TextBox)grdImpact.Rows[rindex].FindControl("UPDATE_FLAG_Impact")).Text);
                    if (UPDATE_FLAG_Impact == 0)
                    {
                        if (((DropDownList)grdImpact.Rows[rindex].FindControl("Impact")).SelectedValue != "0")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetPDImpact(
                            Action: "INSERT",
                            PROTVOIL_ID: PROTVOILID,
                            Impact: ((DropDownList)grdImpact.Rows[rindex].FindControl("Impact")).SelectedValue
                                 );
                        }
                    }
                    else
                    {
                        if (((DropDownList)grdImpact.Rows[rindex].FindControl("Impact")).SelectedValue != "0")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetPDImpact(
                            Action: "UPDATE",
                            PROTVOIL_ID: PROTVOILID,
                            Impact: ((DropDownList)grdImpact.Rows[rindex].FindControl("Impact")).SelectedValue
                                 );
                        }
                    }
                }

                //PD CAPA Save

                int rindex1, UPDATE_FLAG_CAPA;
                for (rindex1 = 0; rindex1 < grdCAPA.Rows.Count; rindex1++)
                {



                    UPDATE_FLAG_CAPA = Convert.ToInt16(((TextBox)grdCAPA.Rows[rindex1].FindControl("UPDATE_FLAG_CAPA")).Text);
                    if (UPDATE_FLAG_CAPA == 0)
                    {
                        if (((DropDownList)grdCAPA.Rows[rindex1].FindControl("Responsibility")).SelectedValue != "0")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetPDCAPA(
                            Action: "INSERT",
                            PROTVOIL_ID: PROTVOILID,
                            CAPA: ((TextBox)grdCAPA.Rows[rindex1].FindControl("CAPA")).Text,
                            Responsibility: ((DropDownList)grdCAPA.Rows[rindex1].FindControl("Responsibility")).SelectedValue,
                            Resolution_DT: ((TextBox)grdCAPA.Rows[rindex1].FindControl("Resolution_DT")).Text
                                 );
                        }
                    }
                    else
                    {
                        if (((DropDownList)grdCAPA.Rows[rindex1].FindControl("Responsibility")).SelectedValue != "0")
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = dal.getsetPDCAPA(
                            Action: "UPDATE",
                            PROTVOIL_ID: PROTVOILID,
                            CAPA: ((TextBox)grdCAPA.Rows[rindex1].FindControl("CAPA")).Text,
                            Responsibility: ((DropDownList)grdCAPA.Rows[rindex1].FindControl("Responsibility")).SelectedValue,
                           Resolution_DT: ((TextBox)grdCAPA.Rows[rindex1].FindControl("Resolution_DT")).Text
                                 );
                        }
                    }
                }

                Response.Write("<script> alert('Record Updated successfully.'); </script>");
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }


        private void fill_Project()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GetSetPROJECTDETAILS(
                Action: "Get_Specific_Project",
                Project_ID: Convert.ToInt32(Session["PROJECTID"]),
                ENTEREDBY: Session["User_ID"].ToString()
                );
                Drp_Project.DataSource = ds.Tables[0];
                Drp_Project.DataValueField = "Project_ID";
                Drp_Project.DataTextField = "PROJNAME";
                Drp_Project.DataBind();
                Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
                if (Session["PROJECTID"] != null)
                {
                    Drp_Project.Items.FindByValue(Session["PROJECTID"].ToString()).Selected = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        public void FillINV()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.ProtocolVoilation_SP(Action: "Get_Subject", Project_ID: Drp_Project.SelectedValue);
                if (ds != null)
                {
                    drp_InvID.Items.Clear();
                    drp_InvID.DataSource = ds.Tables[0];
                    drp_InvID.DataTextField = "INVID";
                    drp_InvID.DataValueField = "INVID";
                    drp_InvID.DataBind();
                }
                drp_InvID.Items.Insert(0, new ListItem("None", "0"));
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
                ds = dal.ProtocolVoilation_SP(Action: "Get_Subject", Project_ID: Drp_Project.SelectedValue, INVID: drp_InvID.SelectedItem.Value);
                if (ds != null)
                {
                    drp_SUBJID.Items.Clear();
                    drp_SUBJID.DataSource = ds.Tables[1];
                    drp_SUBJID.DataTextField = "SUBJID";
                    drp_SUBJID.DataValueField = "SUBJID";
                    drp_SUBJID.DataBind();
                }
                drp_SUBJID.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }


        }

        private void getdrpdata()
        {
            try
            {

                DAL dal;
                dal = new DAL();

                DataSet ds = new DataSet();


                ds = new DataSet();
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "DEPT");
                dal.BindDropDown(drp_DEPT, ds.Tables[0]);

                //ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Status");
                //dal.BindDropDown(drp_Status, ds.Tables[0]);

                ds = new DataSet();
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Status");
                dal.BindDropDown(drp_Status, ds.Tables[0]);

                ds = new DataSet();
                ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Priority");
                dal.BindDropDown(drp_Priority_Ops, ds.Tables[0]);
                dal.BindDropDown(drp_Priority_Med, ds.Tables[0]);
                dal.BindDropDown(drp_Priority_Final, ds.Tables[0]);

                dal = new DAL();
                ds = dal.getsetPDCode(
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

        public void GetData()
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
                        txtVISITNUM.Text = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                        txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                        txtSummary.Text = ds.Tables[0].Rows[0]["Summary"].ToString();
                        txtReference.Text = ds.Tables[0].Rows[0]["Refrence"].ToString();
                        txtSource.Text = ds.Tables[0].Rows[0]["Source"].ToString();

                        txtRationalise.Text = ds.Tables[0].Rows[0]["Rationalise"].ToString();
                        txtOCDate.Text = ds.Tables[0].Rows[0]["Dt_OTCome"].ToString();
                        txtCloseDate.Text = ds.Tables[0].Rows[0]["Close_Date"].ToString();

                        drp_Priority_Ops.SelectedValue = ds.Tables[0].Rows[0]["Priority_Ops"].ToString();
                        drp_Priority_Med.SelectedValue = ds.Tables[0].Rows[0]["Priority_Med"].ToString();
                        drp_Priority_Final.SelectedValue = ds.Tables[0].Rows[0]["Priority_Final"].ToString();


                        //if (ds.Tables[0].Rows[0]["Nature"].ToString() != "")
                        //{
                        //    drp_DEPT.Items.Add(new ListItem(ds.Tables[0].Rows[0]["Nature"].ToString(), ds.Tables[0].Rows[0]["Nature"].ToString()));
                        //}

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

                        fill_PDCode1();
                        if (ds.Tables[0].Rows[0]["PDCODE1"].ToString() != "")
                        {
                            drp_PDCode1.SelectedValue = ds.Tables[0].Rows[0]["PDCODE1"].ToString();
                        }
                        fill_PDCode2();
                        if (ds.Tables[0].Rows[0]["PDCODE2"].ToString() != "")
                        {
                            drp_PDCode2.SelectedValue = ds.Tables[0].Rows[0]["PDCODE2"].ToString();
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
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
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
                PDCODE2: drp_PDCode2.SelectedValue
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

        private void fill_PDCode1()
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

        private void fill_PDCode2()
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
                fill_PDCode1();
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
                fill_PDCode2();
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

        protected void bntImpactAdd_Click(object sender, EventArgs e)
        {
            try
            {

                //Table Structure
                DataRow drCurrentRow = null;
                DataTable dtCurrentTable;
                int rowIndex = 0;
                int i;
                dtCurrentTable = new DataTable();
                dtCurrentTable.Columns.Add(new DataColumn("PROTVOIL_ID", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("Impact", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("UPDATE_FLAG_Impact", typeof(string)));

                if (grdImpact.Rows.Count > 0)
                {
                    for (i = 0; i < grdImpact.Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["PROTVOIL_ID"] = ((TextBox)grdImpact.Rows[rowIndex].FindControl("PROTVOIL_ID")).Text;
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
                        drCurrentRow["PROTVOIL_ID"] = PROTVOILID;
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


                }


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void bntCAPAAdd_Click(object sender, EventArgs e)
        {
            try
            {

                //Table Structure
                DataRow drCurrentRow = null;
                DataTable dtCurrentTable;
                int rowIndex = 0;
                int i;
                dtCurrentTable = new DataTable();
                dtCurrentTable.Columns.Add(new DataColumn("PROTVOIL_ID", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("CAPA", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("Responsibility", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("Resolution_DT", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("UPDATE_FLAG_CAPA", typeof(string)));

                if (grdCAPA.Rows.Count > 0)
                {
                    for (i = 0; i < grdCAPA.Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["PROTVOIL_ID"] = ((TextBox)grdCAPA.Rows[rowIndex].FindControl("PROTVOIL_ID")).Text;
                        drCurrentRow["CAPA"] = ((TextBox)grdCAPA.Rows[rowIndex].FindControl("CAPA")).Text;

                        drCurrentRow["Responsibility"] = ((DropDownList)grdCAPA.Rows[rowIndex].FindControl("Responsibility")).SelectedValue;
                        drCurrentRow["Resolution_DT"] = ((TextBox)grdCAPA.Rows[rowIndex].FindControl("Resolution_DT")).Text;
                        drCurrentRow["UPDATE_FLAG_CAPA"] = ((TextBox)grdCAPA.Rows[rowIndex].FindControl("UPDATE_FLAG_CAPA")).Text;

                        dtCurrentTable.Rows.Add(drCurrentRow);
                        rowIndex++;
                    }

                    if (((DropDownList)grdCAPA.Rows[rowIndex - 1].FindControl("Responsibility")).SelectedValue != "0" && ((TextBox)grdCAPA.Rows[rowIndex - 1].FindControl("CAPA")).Text != "")
                    {
                        //Add Empty Row
                        drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["PROTVOIL_ID"] = PROTVOILID;
                        drCurrentRow["CAPA"] = "";
                        drCurrentRow["Responsibility"] = "";
                        drCurrentRow["Resolution_DT"] = "";
                        drCurrentRow["UPDATE_FLAG_CAPA"] = "0";
                        dtCurrentTable.Rows.Add(drCurrentRow);

                    }


                }
                grdCAPA.DataSource = dtCurrentTable;
                grdCAPA.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
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
                    //string CLASS = dr["CLASS"].ToString();

                    DropDownList btnEdit = (DropDownList)e.Row.FindControl("Responsibility");

                    DAL dal;
                    dal = new DAL();
                    DataSet ds;
                    ds = new DataSet();

                    ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "PD", VARIABLENAME: "Responsibility");

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

        protected void drp_InvID_SelectedIndexChanged1(object sender, EventArgs e)
        {
            FillSubject();
        }
    }
}