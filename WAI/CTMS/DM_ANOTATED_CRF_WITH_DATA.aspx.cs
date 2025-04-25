using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class DM_ANOTATED_CRF_WITH_DATA : System.Web.UI.Page
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
                }
            }
            catch (Exception ex)
            {

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
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                FillVisit();

                repeat_AllModule.DataSource = null;
                repeat_AllModule.DataBind();
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
                DataSet ds = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_SUBJID",
                    INVID: drpInvID.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--Select Subject--", "0"));
                }
                else
                {
                    drpSubID.Items.Clear();
                }
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
                FillVisit();

                repeat_AllModule.DataSource = null;
                repeat_AllModule.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillVisit()
        {
            try
            {
                DataSet ds = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_VISITNUM",
                    SUBJID: drpSubID.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpVisit.DataSource = ds.Tables[0];
                    drpVisit.DataValueField = "VISITNUM";
                    drpVisit.DataTextField = "VISIT";
                    drpVisit.DataBind();
                    drpVisit.Items.Insert(0, new ListItem("All", "All"));
                }
                else
                {
                    drpVisit.Items.Clear();
                }
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
                repeat_AllModule.DataSource = null;
                repeat_AllModule.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void Btn_Get_Data_Click(object sender, EventArgs e)
        {
            try
            {
                GET_ANOTATED_VISITS_PV();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_ANOTATED_VISITS_PV()
        {
            try
            {
                DataSet ds = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_ANOTATED_VISITS_PV",
                SUBJID: drpSubID.SelectedValue,
                VISITNUM: drpVisit.SelectedValue
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeat_AllModule.DataSource = ds.Tables[0];
                    repeat_AllModule.DataBind();

                }
                else
                {
                    repeat_AllModule.DataSource = null;
                    repeat_AllModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeat_AllModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    DataSet ds;
                    ds = new DataSet();

                    GridView grd_Data = (GridView)e.Item.FindControl("grd_Data");

                    ds = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE",
                    MODULEID: row["MODULEID"].ToString(),
                    VISITNUM: row["VISITNUM"].ToString(),
                    PVID: row["PVID"].ToString(),
                    RECID: row["RECID"].ToString(),
                    SUBJID: row["SUBJID"].ToString()
                    );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_Data.DataSource = ds.Tables[0];
                        grd_Data.DataBind();

                    }
                    else
                    {
                        grd_Data.DataSource = null;
                        grd_Data.DataBind();
                    }

                    GridView grdAudit = (GridView)e.Item.FindControl("grdAudit");
                    HtmlTableRow TRAUDIT = (HtmlTableRow)e.Item.FindControl("TRAUDIT");

                    GridView grdQuery = (GridView)e.Item.FindControl("grdQuery");
                    HtmlTableRow TRQUERY = (HtmlTableRow)e.Item.FindControl("TRQUERY");

                    GridView grdQryComment = (GridView)e.Item.FindControl("grdQryComment");
                    HtmlTableRow TRQUERY_COMMENT = (HtmlTableRow)e.Item.FindControl("TRQUERY_COMMENT");

                    GridView grdEventLogs = (GridView)e.Item.FindControl("grdEventLogs");
                    HtmlTableRow TREvent_Logs = (HtmlTableRow)e.Item.FindControl("TREvent_Logs");

                    GridView grdFieldComment = (GridView)e.Item.FindControl("grdFieldComment");
                    HtmlTableRow TRFIELDCOMMENT = (HtmlTableRow)e.Item.FindControl("TRFIELDCOMMENT");

                    DataSet ds1 = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_CONT_DOC_QUR_AUDIT_SIGNOFF",
                    PVID: row["PVID"].ToString(),
                    RECID: row["RECID"].ToString(),
                    MODULEID: row["MODULEID"].ToString()
                    );

                    //Audit Data
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        grdAudit.DataSource = ds1.Tables[0];
                        grdAudit.DataBind();
                        TRAUDIT.Visible = true;
                    }
                    else
                    {
                        grdAudit.DataSource = null;
                        grdAudit.DataBind();
                        TRAUDIT.Visible = false;
                    }

                    //Query Data
                    if (ds1.Tables[1].Rows.Count > 0)
                    {
                        grdQuery.DataSource = ds1.Tables[1];
                        grdQuery.DataBind();
                        TRQUERY.Visible = true;
                    }
                    else
                    {
                        grdQuery.DataSource = null;
                        grdQuery.DataBind();
                        TRQUERY.Visible = false;
                    }

                    //Query Comments
                    if (ds1.Tables[2].Rows.Count > 0)
                    {
                        grdQryComment.DataSource = ds1.Tables[2];
                        grdQryComment.DataBind();
                        TRQUERY_COMMENT.Visible = true;
                    }
                    else
                    {
                        grdQryComment.DataSource = null;
                        grdQryComment.DataBind();
                        TRQUERY_COMMENT.Visible = false;
                    }

                    //Field Comment
                    if (ds1.Tables[3].Rows.Count > 0)
                    {
                        grdFieldComment.DataSource = ds1.Tables[3];
                        grdFieldComment.DataBind();
                        TRFIELDCOMMENT.Visible = true;
                    }
                    else
                    {
                        grdFieldComment.DataSource = null;
                        grdFieldComment.DataBind();
                        TRFIELDCOMMENT.Visible = false;
                    }

                    //Event Logs
                    if (ds1.Tables[4].Rows.Count > 0)
                    {
                        grdEventLogs.DataSource = ds1.Tables[4];
                        grdEventLogs.DataBind();
                        TREvent_Logs.Visible = true;
                    }
                    else
                    {
                        grdEventLogs.DataSource = null;
                        grdEventLogs.DataBind();
                        TREvent_Logs.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void grd_Data_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataSet ds = new DataSet();
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    hdnid.Value = ID;
                    string btnEdit = ((Label)e.Row.FindControl("TXT_FIELD")).Text;

                    GridView grd_Data1 = e.Row.FindControl("grd_Data1") as GridView;

                    DataSet ds1 = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE_CHILD",
                    VISITNUM: dr["VISITNUM"].ToString(),
                    MODULEID: dr["MODULEID"].ToString(),
                    FIELDID: ID,
                    PVID: dr["PVID"].ToString(),
                    RECID: dr["RECID"].ToString(),
                    DefaultData: btnEdit,
                    SUBJID: dr["SUBJID_DATA"].ToString()
                    );

                    if (ds1.Tables.Count > 0)
                    {
                        grd_Data1.DataSource = ds1.Tables[0];
                        grd_Data1.DataBind();
                    }
                    else
                    {
                        grd_Data1.DataSource = null;
                        grd_Data1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Data1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataSet ds = new DataSet();
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    hdnid.Value = ID;
                    string VAL_Child = dr["VAL_Child"].ToString();
                    string btnEdit = ((Label)e.Row.FindControl("TXT_FIELD1")).Text;

                    if (VAL_Child != null && VAL_Child != "")
                    {
                        GridView grd_Data2 = e.Row.FindControl("grd_Data2") as GridView;

                        DataSet ds1 = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE_CHILD",
                        VISITNUM: dr["VISITNUM"].ToString(),
                        MODULEID: dr["MODULEID"].ToString(),
                        FIELDID: ID,
                        PVID: dr["PVID"].ToString(),
                        RECID: dr["RECID"].ToString(),
                        DefaultData: btnEdit,
                        SUBJID: dr["SUBJID_DATA"].ToString()
                        );

                        if (ds1.Tables.Count > 0)
                        {
                            grd_Data2.DataSource = ds1.Tables[0];
                            grd_Data2.DataBind();
                        }
                        else
                        {
                            grd_Data2.DataSource = null;
                            grd_Data2.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Data2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataSet ds = new DataSet();
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    hdnid.Value = ID;
                    string VAL_Child = dr["VAL_Child"].ToString();
                    string btnEdit = ((Label)e.Row.FindControl("TXT_FIELD2")).Text;

                    if (VAL_Child != null && VAL_Child != "")
                    {
                        GridView grd_Data3 = e.Row.FindControl("grd_Data3") as GridView;

                        DataSet ds1 = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_STRUCTURE_CHILD",
                        VISITNUM: dr["VISITNUM"].ToString(),
                        MODULEID: dr["MODULEID"].ToString(),
                        FIELDID: ID,
                        PVID: dr["PVID"].ToString(),
                        RECID: dr["RECID"].ToString(),
                        DefaultData: btnEdit,
                        SUBJID: dr["SUBJID_DATA"].ToString()
                        );

                        if (ds1.Tables.Count > 0)
                        {
                            grd_Data3.DataSource = ds1.Tables[0];
                            grd_Data3.DataBind();
                        }
                        else
                        {
                            grd_Data3.DataSource = null;
                            grd_Data3.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Data3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataSet ds = new DataSet();
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string ID = dr["FIELD_ID"].ToString();
                    hdnid.Value = ID;
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    Label btnEdit = (Label)e.Row.FindControl("TXT_FIELD3");

                    if (CONTROLTYPE != "HEADER")
                    {
                        ds = dal_DM.DM_PRINT_REPORT_SP(ACTION: "GET_ANNOTATED_MODULE_DATA",
                            PVID: dr["PVID"].ToString(),
                            RECID: dr["RECID"].ToString(),
                            TABLENAME: dr["TABLENAME"].ToString(),
                            VARIABLENAME: dr["VARIABLENAME"].ToString()
                            );

                        btnEdit.Visible = true;

                        btnEdit.Text = ds.Tables[0].Rows[0][VARIABLENAME].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('DM_CRF_WITH_DATA_PRINT.aspx?INVID=" + drpInvID.SelectedValue + "&SUBJID=" + drpSubID.SelectedValue + "&VISITNUM=" + drpVisit.SelectedValue + "&VISITNAME=" + drpVisit.SelectedItem.Text + "','_newtab');", true);
        }
    }
}