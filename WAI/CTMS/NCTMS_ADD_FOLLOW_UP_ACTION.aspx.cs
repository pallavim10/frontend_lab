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
    public partial class NCTMS_ADD_FOLLOW_UP_ACTION : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    lblSITEID.Text = Request.QueryString["SITEID"].ToString();
                    lblVisitType.Text = Request.QueryString["VISIT"].ToString();
                    lblVisitId.Text = Request.QueryString["SVID"].ToString();

                    GETDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GETDATA()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_FOLLOW_UP_ACTIONS_DATA",
                SVID: Request.QueryString["SVID"].ToString()
                );

                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdFollowupActions.DataSource = ds;
                        grdFollowupActions.DataBind();
                    }
                }
                else
                {
                    grdFollowupActions.DataSource = null;
                    grdFollowupActions.DataBind();
                }
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
                dal.CTMS_DATA_SP(ACTION: "INSERT_FOLLOW_UP_ACTION_DATA",
                SVID: lblVisitId.Text,
                VISITID: Request.QueryString["VISITID"].ToString(),
                VISITNAME: Request.QueryString["VISIT"].ToString(),
                SITEID: lblSITEID.Text,
                Comment: txtComment.Text,
                ID: Convert.ToString(ViewState["FOLLOW_UP_ID"])
                );

                ViewState.Remove("FOLLOW_UP_ID");

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dal.CTMS_DATA_SP(ACTION: "UPDATE_FOLLOW_UP_ACTION_DATA",
                SVID: lblVisitId.Text,
                VISITID: Request.QueryString["VISITID"].ToString(),
                VISITNAME: Request.QueryString["VISIT"].ToString(),
                SITEID: lblSITEID.Text,
                Comment: txtComment.Text,
                ID: Convert.ToString(ViewState["FOLLOW_UP_ID"])
                );

                ViewState.Remove("FOLLOW_UP_ID");

                btnUpdate.Visible = false;
                btnSubmit.Visible = true;

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState.Remove("FOLLOW_UP_ID");
                txtComment.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdFollowupActions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                ViewState["FOLLOW_UP_ID"] = ID;
                if (e.CommandName == "EditFollowUpAction")
                {
                    btnUpdate.Visible = true;
                    btnSubmit.Visible = false;
                    EDIT_FOLLOW_UP_ACTION(ID);
                }
                else if (e.CommandName == "DeleteFollowUpAction")
                {
                    DELETE_FOLLOW_UP_ACTION(ID);
                    GETDATA();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void EDIT_FOLLOW_UP_ACTION(string ID)
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "GET_FOLLOW_UP_ACTION_BY_ID",
                ID: ID
                );

                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblSITEID.Text = Request.QueryString["SITEID"].ToString();
                        lblVisitType.Text = Request.QueryString["VISIT"].ToString();
                        lblVisitId.Text = Request.QueryString["SVID"].ToString();
                        txtComment.Text = ds.Tables[0].Rows[0]["COMMENT"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void DELETE_FOLLOW_UP_ACTION(string ID)
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(ACTION: "DELETE_FOLLOW_UP_ACTION",
                ID: ID
                );
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NCTMS_FOLLOW_UP_ACTION.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}