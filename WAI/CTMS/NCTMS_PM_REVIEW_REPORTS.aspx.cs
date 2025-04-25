using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class NCTMS_PM_REVIEW_REPORTS : System.Web.UI.Page
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
                    hdnMODULEID.Value = Request.QueryString["MODULEID"].ToString();
                    hdnMODULENAME.Value = Request.QueryString["MODULENAME"].ToString();
                    hdnSITEID.Value = Request.QueryString["INVID"].ToString();

                    hdnSVID.Value = Request.QueryString["SVID"].ToString();

                    lblSiteId.Text = Request.QueryString["INVID"].ToString();
                    lblVisit.Text = Request.QueryString["VISIT"].ToString();
                    lblVisitID.Text = Request.QueryString["SVID"].ToString();

                    FILL_MODULES();
                    GetStructure();
                    GetRecords();

                    GetAuditDetails();
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
                drpModule.Items.Insert(0, new ListItem("--Select Section--", "0"));
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
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_STRUCTURE_LIST_VIEW",
                VISITID: Request.QueryString["VISITID"].ToString(),
                MODULEID: hdnMODULEID.Value
                );

                lblModuleName.Text = drpModule.SelectedItem.Text;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hfTablename.Value = ds.Tables[1].Rows[0]["CTMS_TABLENAME"].ToString();

                    grdField.DataSource = ds.Tables[0];
                    grdField.DataBind();

                    grdField.Attributes.Add("ToolTip", drpModule.SelectedItem.Text);

                    //bntSaveComplete.Visible = true;
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
                      MODULEID: Request.QueryString["MODULEID"].ToString(),
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
                          MODULEID: Request.QueryString["MODULEID"].ToString(),
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

                if (drpModule.SelectedIndex != 0)
                {
                    DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_MODULE_MULTIPLE", PROJECTID: Session["PROJECTID"].ToString(), VISITNUM: Request.QueryString["VISITID"].ToString(), MODULENAME: drpModule.SelectedItem.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                        {
                            Response.Redirect("NCTMS_PM_REVIEW_REPORTS.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + Request.QueryString["VISITID"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&VISIT_NOM=" + Request.QueryString["SVID"].ToString() + "&SVID=" + Request.QueryString["SVID"].ToString());
                        }
                        if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                        {
                            Response.Redirect("NCTMS_PM_REVIEW_REPORTS.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + Request.QueryString["VISITID"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&VISIT_NOM=" + Request.QueryString["SVID"].ToString() + "&SVID=" + Request.QueryString["SVID"].ToString());
                        }
                    }
                    else
                    {
                        Response.Redirect("NCTMS_PM_REVIEW_REPORTS.aspx?MODULEID=" + drpModule.SelectedValue + "&MODULENAME=" + drpModule.SelectedItem.Text + "&VISITID=" + Request.QueryString["VISITID"] + "&VISIT=" + Request.QueryString["VISIT"] + "&INVID=" + Request.QueryString["INVID"] + "&VISIT_NOM=" + Request.QueryString["SVID"].ToString());
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
                    LinkButton lbtnAddComment = (LinkButton)e.Row.FindControl("lbtnAddComment");

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

        protected void btnBacktoHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NCTMS_PM_REVIEW_OPEN_CRF.aspx?SVID=" + hdnSVID.Value + "&SITEID=" + hdnSITEID.Value + "&VISITID=" + hdnVISITID.Value + "&VISIT=" + hdnVISIT.Value, false);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}