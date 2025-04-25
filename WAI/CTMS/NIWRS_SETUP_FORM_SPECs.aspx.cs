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
    public partial class NIWRS_SETUP_FORM_SPECs : System.Web.UI.Page
    {

        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hdnFormID.Value = Request.QueryString["ID"].ToString();
                    GET_REVIEW_STATUS();
                    GET_FORM_SPEC();
                    
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_REVIEW_STATUS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_CONFIGURATION_REVIEW");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //Review
                    hdnREVIEWSTATUS.Value = ds.Tables[0].Rows[0]["ANS"].ToString();
                }
                else
                {
                    //Unreview
                    hdnREVIEWSTATUS.Value = "Unreview";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void GET_FORM_SPEC()
        {
            try
            {
                DataSet ds = dal_IWRS.NIWRS_SETUP_SP(ACTION: "GET_FORM_SPEC", ID: Request.QueryString["ID"].ToString());

                lblForm.Text = ds.Tables[0].Rows[0]["FORMNAME"].ToString();

                grdFormSpecs.DataSource = ds;
                grdFormSpecs.DataBind();


            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdFormSpecs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    string VARIABLENAME = drv["VARIABLENAME"].ToString();

                    string IWRS_DM_SYNC = drv["IWRS_DM_SYNC"].ToString();

                    string IWRS = drv["IWRS"].ToString();

                    Label lblOptions = (Label)e.Row.FindControl("lblOptions");

                    LinkButton lbtnSync = (LinkButton)e.Row.FindControl("lbtnSync");
                    LinkButton lbtnUNSYNC = (LinkButton)e.Row.FindControl("lbtnUNSYNC");
                    LinkButton lbtnSetFieldValues = (LinkButton)e.Row.FindControl("lbtnSetFieldValues");
                    LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                    LinkButton LbtnUndo = (LinkButton)e.Row.FindControl("LbtnUndo");
                    Label lblFIELDNAME = (Label)e.Row.FindControl("lblFIELDNAME");

                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "GET_FORM_SPEC_FIELD_ANS", VARIABLENAME: VARIABLENAME);

                    DataTable dt = ds.Tables[0];
                    string Values = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Values += "" + dt.Rows[i]["Value"].ToString() + ",";
                    }

                    lblOptions.Text = Values.TrimEnd(',');

                    if (IWRS_DM_SYNC == "1")
                    {
                        lbtnUNSYNC.Visible = true;
                    }
                    else
                    {
                        lbtnSync.Visible = true;
                    }

                    if (IWRS == "1")
                    {
                        LbtnUndo.Visible = false;
                    }
                    else if (IWRS == "0")
                    {
                        lbtnSetFieldValues.Visible = false;
                        lbtnSync.Visible = false;
                        lbtnDelete.Visible = false;
                        LbtnUndo.Visible = true;
                    }

                    if (hdnREVIEWSTATUS.Value == "Review")
                    {
                        lbtnSync.Enabled = false;
                        lbtnSync.ToolTip = "Configuration has been Frozen";
                        lbtnSync.OnClientClick = "return ConfigFrozen_MSG()";

                        lbtnUNSYNC.Enabled = false;
                        lbtnUNSYNC.ToolTip = "Configuration has been Frozen";
                        lbtnUNSYNC.OnClientClick = "return ConfigFrozen_MSG()";

                        lbtnSetFieldValues.Enabled = false;
                        lbtnSetFieldValues.ToolTip = "Configuration has been Frozen";
                        lbtnSetFieldValues.OnClientClick = "return ConfigFrozen_MSG()";

                        LbtnUndo.Enabled = false;
                        LbtnUndo.ToolTip = "Configuration has been Frozen";
                        LbtnUndo.OnClientClick = "return ConfigFrozen_MSG()";

                        lbtnDelete.Enabled = false;
                        lbtnDelete.ToolTip = "Configuration has been Frozen";
                        lbtnDelete.OnClientClick = "return ConfigFrozen_MSG()";

                        LbtnUndo.Enabled = false;
                        LbtnUndo.ToolTip = "Configuration has been Frozen";
                        LbtnUndo.OnClientClick = "return ConfigFrozen_MSG()";

                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdFormSpecs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();

                if (e.CommandName == "DELETEFIELD")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "DELETE_FEILDS", FIELDID: ID);
                    GET_FORM_SPEC();
                }
                else if (e.CommandName == "Sync")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "SYNC_FEILDS", FIELDID: ID);
                    GET_FORM_SPEC();
                }
                else if (e.CommandName == "UNSYNC")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "UNSYNC_FEILDS", FIELDID: ID);
                    GET_FORM_SPEC();
                }
                else if (e.CommandName == "UNDOFIELD")
                {
                    DataSet ds = dal_IWRS.IWRS_SET_FORM_SP(ACTION: "UNDO_FIELDS", FIELDID: ID);
                    GET_FORM_SPEC();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}