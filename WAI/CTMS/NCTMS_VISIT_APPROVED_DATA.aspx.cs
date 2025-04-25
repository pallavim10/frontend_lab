using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class NCTMS_VISIT_APPROVED_DATA : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    hdnVISITID.Value = Request.QueryString["VISITID"].ToString();
                    hdnVISIT.Value = Request.QueryString["VISIT"].ToString();
                    hdnSITEID.Value = Request.QueryString["INVID"].ToString();
                    hdnSVID.Value = Request.QueryString["SVID"].ToString();

                    lblSiteId.Text = Request.QueryString["INVID"].ToString();
                    lblVisit.Text = Request.QueryString["VISIT"].ToString();
                    lblVisitID.Text = Request.QueryString["SVID"].ToString();

                    FILL_MODULES();
                    GetStructure();
                    GetRecords();

                    GET_TRACKERS_DATA();

                    GetAuditDetails();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_TRACKERS_DATA()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_TRACKERS_MODULES",
                SVID: hdnSVID.Value,
                VISITID: hdnVISITID.Value
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeatData.DataSource = ds;
                    repeatData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;

                    GridView grd_Records = (GridView)e.Item.FindControl("grd_Records");

                    DataSet ds = dal.CTMS_DATA_SP(
                        ACTION: "GET_TRACKER_DATA",
                        SVID: hdnSVID.Value,
                        VISITID: hdnVISITID.Value,
                        MODULEID: row["MODULEID"].ToString(),
                        TABLENAME: row["TABLENAME"].ToString()
                        );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_Records.DataSource = ds;
                        grd_Records.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FILL_MODULES()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_MODULE_WITHOUT_TRACKER",
                SITEID: Request.QueryString["INVID"].ToString(),
                VISITID: Request.QueryString["VISITID"].ToString(),
                SVID: Request.QueryString["SVID"].ToString()
                );

                drpModule.DataSource = ds.Tables[0];
                drpModule.DataValueField = "MODULEID";
                drpModule.DataTextField = "MODULENAME";
                drpModule.DataBind();

                if (Request.QueryString["MODULEID"] != null)
                {
                    if (drpModule.Items.FindByValue(Request.QueryString["MODULEID"].ToString()) != null)
                    {
                        drpModule.Items.FindByValue(Request.QueryString["MODULEID"].ToString()).Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetStructure()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_STRUCTURE",
                VISITID: Request.QueryString["VISITID"].ToString(),
                MODULEID: drpModule.SelectedValue
                );

                lblModuleName.Text = drpModule.SelectedItem.Text;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hfTablename.Value = ds.Tables[1].Rows[0]["CTMS_TABLENAME"].ToString();

                    grdField.DataSource = ds.Tables[0];
                    grdField.DataBind();

                    grdField.Attributes.Add("ToolTip", drpModule.SelectedItem.Text);
                }
                else
                {
                    grdField.DataSource = null;
                    grdField.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private void GetRecords()
        {
            try
            {
                string COLNAME, COLVAL;
                int rownum = 0;
                string SVID = hdnSVID.Value;

                DAL dal;
                dal = new DAL();
                DataSet dsData;

                if (Request.QueryString["MODULEID"] != null)
                {
                    dsData = dal.CTMS_DATA_SP(
                      ACTION: "GET_DATA",
                      SVID: hdnSVID.Value,
                      TABLENAME: hfTablename.Value,
                      VISITID: Request.QueryString["VISITID"].ToString(),
                      MODULEID: drpModule.SelectedValue,
                      RECID: "0"
                      );
                }
                else
                {
                    dsData = dal.CTMS_DATA_SP(
                          ACTION: "GET_DATA",
                          SVID: hdnSVID.Value,
                          TABLENAME: hfTablename.Value,
                          VISITID: Request.QueryString["VISITID"].ToString(),
                          MODULEID: drpModule.SelectedValue,
                          RECID: "0"
                          );
                }

                DataSet ds = new DataSet();
                if (dsData.Tables.Count > 0)
                {
                    DataTable dt = GenerateTransposedTable(dsData.Tables[0]);
                    ds.Tables.Add(dt);
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        for (rownum = 0; rownum < grdField.Rows.Count; rownum++)
                        {
                            COLNAME = ((Label)grdField.Rows[rownum].FindControl("lblVARIABLENAME")).Text;

                            string DataVariableName = ds.Tables[0].Rows[i]["VARIABLENAME"].ToString();

                            COLVAL = ds.Tables[0].Rows[i]["DATA"].ToString();
                            if (DataVariableName == COLNAME)
                            {
                                ((Label)grdField.Rows[rownum].FindControl("lblANS")).Text = COLVAL.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            outputTable.Columns.Add("VARIABLENAME");
            outputTable.Columns.Add("DATA");

            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                foreach (DataColumn dc in inputTable.Columns)
                {
                    DataRow drNew = outputTable.NewRow();
                    drNew["VARIABLENAME"] = dc.ColumnName.ToString();
                    drNew["DATA"] = inputTable.Rows[i][dc.ColumnName];
                    outputTable.Rows.Add(drNew);
                }
            }

            return outputTable;
        }

        protected void grdField_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    GridView grdComments = (GridView)e.Row.FindControl("grdComments");

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    Label lblAllCommentCount = (Label)e.Row.FindControl("lblAllCommentCount");

                    DataSet ds = dal.GetSetChecklistComments(Action: "GET_CHECKLIST_COMMENTS_PM",
                    CheckListRow_ID: dr["FIELD_ID"].ToString(),
                    ChecklistID: hdnSVID.Value
                    );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdComments.DataSource = ds;
                        grdComments.DataBind();

                        anchor.Visible = true;

                        lblAllCommentCount.Text = ds.Tables[0].Rows.Count.ToString();
                    }
                    else
                    {
                        grdComments.DataSource = null;
                        grdComments.DataBind();

                        anchor.Visible = false;
                        lblAllCommentCount.Text = "0";
                    }

                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();

                    HtmlImage img = (HtmlImage)e.Row.FindControl("AD");
                    img = (HtmlImage)e.Row.FindControl("AD");
                    img.ID = "AD_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);

                    img = (HtmlImage)e.Row.FindControl("ADINITIAL");
                    img.ID = "ADINITIAL_" + VARIABLENAME;
                    img.Attributes.Add("fieldname", FIELDNAME);
                    img.Attributes.Add("variablename", VARIABLENAME);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnRECID.Value = null;

                if (drpModule.SelectedValue != "0")
                {
                    DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_MODULE_MULTIPLE", PROJECTID: Session["PROJECTID"].ToString(), VISITNUM: Request.QueryString["VISITID"].ToString(), MODULENAME: drpModule.SelectedItem.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                        {
                            Response.Redirect("NCTMS_VISIT_APPROVED_DATA.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + Request.QueryString["VISITID"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&VISIT_NOM=" + Request.QueryString["SVID"].ToString() + "&SVID=" + Request.QueryString["SVID"].ToString());
                        }
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                        {
                            Response.Redirect("NCTMS_VISIT_APPROVED_DATA.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + Request.QueryString["VISITID"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&VISIT_NOM=" + Request.QueryString["SVID"].ToString() + "&SVID=" + Request.QueryString["SVID"].ToString());
                        }
                    }
                    else
                    {
                        Response.Redirect("NCTMS_VISIT_APPROVED_DATA.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + Request.QueryString["VISITID"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&VISIT_NOM=" + Request.QueryString["SVID"].ToString() + "&SVID=" + Request.QueryString["SVID"].ToString());
                    }
                }
                else
                {
                    grdField.DataSource = null;
                    grdField.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdComments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    string Issue = dr["Issue"].ToString();
                    string Internal = dr["Internal"].ToString();
                    string Followup = dr["Followup"].ToString();
                    string Report = dr["Report"].ToString();
                    string Observation = dr["Observation"].ToString();
                    string PM_COMMENT = dr["PM_COMMENTS"].ToString();
                    string PD = dr["PD"].ToString();
                   // LinkButton lbtnAddComment = (LinkButton)e.Row.FindControl("lbtnAddComment");

                    if (Issue == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Row.FindControl("CHK_Issue");
                        CHK.Checked = true;
                    }
                    if (Internal == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Row.FindControl("CHK_Internal");
                        CHK.Checked = true;
                    }
                    if (Followup == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Row.FindControl("CHK_Followup");
                        CHK.Checked = true;
                    }
                    if (Observation == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Row.FindControl("CHK_Observation");
                        CHK.Checked = true;
                    }
                    if (Report == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Row.FindControl("CHK_REPORT");
                        CHK.Checked = true;
                    }
                    if (PD == "True")
                    {
                        CheckBox CHK_PD = (CheckBox)e.Row.FindControl("CHK_PD");
                        CHK_PD.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void gvEmp_PreRender(object sender, EventArgs e)
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

        public void GetAuditDetails()
        {
            try
            {
                DAL dal = new DAL();
                DataSet ds;
                ds = new DataSet();
                ds = dal.CTMS_GetAUDITTRAILDETAILS
                    (
                    Action: "GET_DATA_ALL",
                    SVID: hdnSVID.Value,
                    RECID: Convert.ToInt32(hdnRECID.Value)
                    );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdAUDITTRAILDETAILS.DataSource = ds;
                    grdAUDITTRAILDETAILS.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Records_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lnkPAGENUM = (LinkButton)e.Row.FindControl("lnkPAGENUM");

                GridView grd_Records = (GridView)sender;

                string DELETE = dr["DELETE"].ToString();

                if (DELETE == "True")
                {
                    e.Row.Attributes.Add("class", "strikeThrough");
                }

                grd_Records.HeaderRow.Cells[0].Visible = false;
                e.Row.Cells[0].Visible = false;
            }

        }
    }
}