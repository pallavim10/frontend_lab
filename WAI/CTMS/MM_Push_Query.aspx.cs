using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class MM_Push_Query : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    GET_QUERY_DETAILS();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GET_QUERY_DETAILS()
        {
            try
            {
                DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "GET_MM_QUERY_DETAILS_BY_ID", QUERYID: Request.QueryString["QUERYID"].ToString());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblvisitname.Text = ds.Tables[0].Rows[0]["VISIT"].ToString();
                    lblINVID.Text = ds.Tables[0].Rows[0]["INVID"].ToString();
                    lblSubjectid.Text = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                    hdnModuleid.Value = ds.Tables[0].Rows[0]["MODULEID"].ToString();
                    lblmodulename.Text = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                    txtQueryText.Text = ds.Tables[0].Rows[0]["QUERYTEXT"].ToString();
                    hdnPVID.Value = ds.Tables[0].Rows[0]["PVID"].ToString();
                    hdnRECID.Value = ds.Tables[0].Rows[0]["RECID"].ToString();

                    BIND_FIELDS();
                    
                    ddlFieldName.SelectedValue = ds.Tables[0].Rows[0]["FIELDID"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void BIND_FIELDS()
        {
            try
            {
                DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "GET_FIELDS",
                    MODULEID: hdnModuleid.Value,
                    QUERYID: Request.QueryString["QUERYID"].ToString()
                    );

                ddlFieldName.DataSource = ds;
                ddlFieldName.DataValueField = "FIELD_ID";
                ddlFieldName.DataTextField = "FIELDNAME";
                ddlFieldName.DataBind();
                ddlFieldName.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "PUSH_MM_QUERY_TO_DM_QUERY",
                MODULEID: hdnModuleid.Value,
                FIELDID: ddlFieldName.SelectedValue,
                QUERYTEXT: txtQueryText.Text,
                QUERYID: Request.QueryString["QUERYID"].ToString()
                );

                Session["BACKTO_MM_QUERY_REPORT"] = "1";

                Response.Write("<script> alert('MM query pushed to DM successfully.'); window.location.href='MM_QUERY_REPORTS.aspx' </script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Session["BACKTO_MM_QUERY_REPORT"] = "1";
                Response.Redirect("MM_QUERY_REPORTS.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnShowQuery_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DM.DM_MM_QUERY_SP(ACTION: "GET_MM_QUERY_DETAILS_BY_ID", QUERYID: Request.QueryString["QUERYID"].ToString());

                string MODULEID = ds.Tables[0].Rows[0]["MODULEID"].ToString();
                string MODULENAME = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                string VISITID = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                string VISIT = ds.Tables[0].Rows[0]["VISIT"].ToString();
                string INVID = ds.Tables[0].Rows[0]["INVID"].ToString();
                string SUBJID = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                string RECID = ds.Tables[0].Rows[0]["RECID"].ToString();

                var URL = "";

                if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "1")
                {
                    URL = "DM_DataEntry_MultipleData_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                }
                else
                {
                    URL = "DM_DataEntry_ReadOnly.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID;
                }

                Session["MM_QUERY_URL"] = "MM_Push_Query.aspx?QUERYID=" + Request.QueryString["QUERYID"].ToString();
                Response.Redirect(URL);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}