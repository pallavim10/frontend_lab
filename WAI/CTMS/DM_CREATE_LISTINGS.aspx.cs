using CTMS.CommonFunction;
using PPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CTMS
{
    public partial class DM_CREATE_LISTINGS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_DB dal_DB = new DAL_DB();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtQueryText.Attributes.Add("MaxLength", "500");
            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                {
                    GetListing();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void btnAddListing_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtListing.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Listing Name');</script>");
                    return;
                }
                else if (txtSeqNo.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Sequence Number');</script>");
                    return;
                }

                DataSet ds = dal_DB.DB_LIST_SP(
                Action: "InsertListing",
                USERID: Session["User_ID"].ToString(),
                LISTING_NAME: txtListing.Text,
                LISTING_ID: drpSameas.SelectedValue,
                SEQNO: txtSeqNo.Text,
                DM: chkDM.Checked,

                MEDICAL: chkMM.Checked,

                QUERY: txtQueryText.Text,
                TRANSPOSE: chkTranspose.Checked,
                Listing_DashBoard: chkDashboard.Checked,
                PARENT: ddlParent.SelectedValue,
                TILES: chkTiles.Checked,
                Graphs: chkGraphs.Checked,


                IWRS: chkIWRS.Checked,
                Saftey: chkSaftey.Checked,
                MANUAL_CODE: chkManCode.Checked,
                PAT_REV: chkPatientReview.Checked,
                STUDY_REV: chkStudyReview.Checked,


                QueryReport: chkQueryReport.Checked,
                CommentReport: chkCommentReport.Checked,
                AutocodeLIB: drpAutoCode.SelectedValue
                );

                string dsPID = ds.Tables[0].Rows[0]["ID"].ToString();

                txtListing.Text = "";
                txtQueryText.Text = "";
                txtSeqNo.Text = "";
                chkDM.Checked = false;

                chkMM.Checked = false;

                chkTranspose.Checked = false;
                chkDashboard.Checked = false;
                chkTiles.Checked = false;


                chkIWRS.Checked = false;
                chkSaftey.Checked = false;
                chkManCode.Checked = false;
                chkPatientReview.Checked = false;
                chkStudyReview.Checked = false;


                ddlParent.SelectedValue = "None";
                chkQueryReport.Checked = false;
                chkCommentReport.Checked = false;
                drpAutoCode.SelectedValue = "0";
                GetListing();

                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "ChangeDivQuery();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnUpdateListing_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds1 = dal_DB.DB_LIST_SP(
                Action: "Delete_Parent_Listing",
                USERID: Session["User_ID"].ToString(),
                LISTING_NAME: txtListing.Text,
                SEQNO:txtSeqNo.Text,
                ID: Session["LISTID"].ToString(),
                DM: chkDM.Checked,
                MEDICAL: chkMM.Checked,
                QUERY: txtQueryText.Text,
                TRANSPOSE: chkTranspose.Checked,
                Listing_DashBoard: chkDashboard.Checked,
                PARENT: ddlParent.SelectedValue,
                IWRS: chkIWRS.Checked,
                Saftey: chkSaftey.Checked,
                MANUAL_CODE: chkManCode.Checked,
                PAT_REV: chkPatientReview.Checked,
                STUDY_REV: chkStudyReview.Checked,
                QueryReport: chkQueryReport.Checked,
                CommentReport: chkCommentReport.Checked
                );

                DataSet ds = dal_DB.DB_LIST_SP(
                Action: "UpdateListing",
                USERID: Session["User_ID"].ToString(),
                LISTING_NAME: txtListing.Text,
                SEQNO: txtSeqNo.Text,
                LISTING_ID: drpSameas.SelectedValue,
                ID: Session["LISTID"].ToString(),
                DM: chkDM.Checked,
                MEDICAL: chkMM.Checked,
                QUERY: txtQueryText.Text,
                TRANSPOSE: chkTranspose.Checked,
                Listing_DashBoard: chkDashboard.Checked,
                PARENT: ddlParent.SelectedValue,
                TILES: chkTiles.Checked,
                Graphs: chkGraphs.Checked,

                IWRS: chkIWRS.Checked,
                Saftey: chkSaftey.Checked,
                MANUAL_CODE: chkManCode.Checked,
                PAT_REV: chkPatientReview.Checked,
                STUDY_REV: chkStudyReview.Checked,
                QueryReport: chkQueryReport.Checked,
                CommentReport: chkCommentReport.Checked,
                AutocodeLIB: drpAutoCode.SelectedValue
                );

                txtListing.Text = "";
                txtQueryText.Text = "";
                txtSeqNo.Text = "";
                btnAddListing.Visible = true;
                btnUpdateListing.Visible = false;
                chkDM.Checked = false;
                chkMM.Checked = false;
                chkTranspose.Checked = false;
                chkDashboard.Checked = false;
                chkTiles.Checked = false;

                chkIWRS.Checked = false;
                chkSaftey.Checked = false;
                chkManCode.Checked = false;
                chkPatientReview.Checked = false;
                chkStudyReview.Checked = false;
                ddlParent.SelectedValue = "None";
                chkQueryReport.Checked = false;
                chkCommentReport.Checked = false;
                drpAutoCode.SelectedValue = "0";
                GetListing();

                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "ChangeDivQuery();", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        public void GetListing()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GETLISTING", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["LIST"].ToString() != "THIS LIST IS ALREADY EXISTS")
                    {
                        grdListing.DataSource = ds;
                        grdListing.DataBind();
                        btnExport.Visible = true;
                        drpSameas.DataSource = ds;
                        drpSameas.DataValueField = "ID";
                        drpSameas.DataTextField = "NAME";
                        drpSameas.DataBind();
                        drpSameas.Items.Insert(0, new ListItem("-Select-", "0"));

                    }
                    else
                    {
                        lblErrorMsg.Text = ds.Tables[0].Rows[0]["LIST"].ToString();
                    }
                }
                else
                {
                    grdListing.DataSource = null;
                    grdListing.DataBind();
                    btnExport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdListing_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = Convert.ToString(e.CommandArgument);
                Session["LISTID"] = ID;
                if (e.CommandName == "EditList")
                {
                    EditList();
                }
                else if (e.CommandName == "DeleteList")
                {
                    DeleteList();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void EditList()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(Action: "GETLISTINGBY_ID", PROJECTID: Session["PROJECTID"].ToString(), USERID: Session["User_ID"].ToString(), ID: Session["LISTID"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtListing.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                    txtSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                    txtQueryText.Text = ds.Tables[0].Rows[0]["QUERYTEXT"].ToString();
                    ddlParent.SelectedValue = ds.Tables[0].Rows[0]["PARENT"].ToString();

                    if (ds.Tables[0].Rows[0]["TRANSPOSE"].ToString() == "True")
                    {
                        chkTranspose.Checked = true;
                    }
                    else
                    {
                        chkTranspose.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["DM"].ToString() == "True")
                    {
                        chkDM.Checked = true;
                    }
                    else
                    {
                        chkDM.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["MEDICAL"].ToString() == "True")
                    {
                        chkMM.Checked = true;
                    }
                    else
                    {
                        chkMM.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["Listing_DashBoard"].ToString() == "True")
                    {
                        chkDashboard.Checked = true;
                    }
                    else
                    {
                        chkDashboard.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["TILES"].ToString() == "True")
                    {
                        chkTiles.Checked = true;
                    }
                    else
                    {
                        chkTiles.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["GRAPHS"].ToString() == "True")
                    {
                        chkGraphs.Checked = true;
                    }
                    else
                    {
                        chkGraphs.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["IWRS"].ToString() == "True")
                    {
                        chkIWRS.Checked = true;
                    }
                    else
                    {
                        chkIWRS.Checked = false;
                    }
                    if (ds.Tables[0].Rows[0]["Saftey"].ToString() == "True")
                    {
                        chkSaftey.Checked = true;
                    }
                    else
                    {
                        chkSaftey.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["MANUAL_CODE"].ToString() == "True")
                    {
                        drpAutoCode.SelectedValue = ds.Tables[0].Rows[0]["AutocodeLIB"].ToString();
                        chkManCode.Checked = true;
                    }
                    else
                    {
                        chkManCode.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["PAT_REV"].ToString() == "True")
                    {
                        chkPatientReview.Checked = true;
                    }
                    else
                    {
                        chkPatientReview.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["STUDY_REV"].ToString() == "True")
                    {
                        chkStudyReview.Checked = true;
                    }
                    else
                    {
                        chkStudyReview.Checked = false;
                    }
                    if (ds.Tables[0].Rows[0]["Query_Report"].ToString() == "True")
                    {
                        chkQueryReport.Checked = true;
                    }
                    else
                    {
                        chkQueryReport.Checked = false;
                    }
                    if (ds.Tables[0].Rows[0]["COMMENT_REPORT"].ToString() == "True")
                    {
                        chkCommentReport.Checked = true;
                    }
                    else
                    {
                        chkCommentReport.Checked = false;
                    }

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "ChangeDivQuery(); showDropdownList();", true);
                }

                btnAddListing.Visible = false;
                btnUpdateListing.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DeleteList()
        {
            try
            {
                DataSet ds = dal_DB.DB_LIST_SP(
                Action: "DeleteList",
                ID: Session["LISTID"].ToString(),
                USERID: Session["USER_ID"].ToString()
                );

                GetListing();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void grdListing_PreRender(object sender, EventArgs e)
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
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void grdListing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    HtmlControl iconDM = (HtmlControl)e.Row.FindControl("iconDM");

                    if (drv["DM"].ToString() == "True")
                    {
                        iconDM.Attributes.Add("class", "fa fa-check");
                        iconDM.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconDM.Attributes.Add("class", "fa fa-times");
                        iconDM.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconIWRS = (HtmlControl)e.Row.FindControl("iconIWRS");
                    if (drv["IWRS"].ToString() == "True")
                    {
                        iconIWRS.Attributes.Add("class", "fa fa-check");
                        iconIWRS.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconIWRS.Attributes.Add("class", "fa fa-times");
                        iconIWRS.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconSaftey = (HtmlControl)e.Row.FindControl("iconSaftey");
                    if (drv["Saftey"].ToString() == "True")
                    {
                        iconSaftey.Attributes.Add("class", "fa fa-check");
                        iconSaftey.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconSaftey.Attributes.Add("class", "fa fa-times");
                        iconSaftey.Attributes.Add("style", "color: red;");
                    }
                    HtmlControl iconMEDICAL = (HtmlControl)e.Row.FindControl("iconMEDICAL");
                    if (drv["MEDICAL"].ToString() == "True")
                    {
                        iconMEDICAL.Attributes.Add("class", "fa fa-check");
                        iconMEDICAL.Attributes.Add("style", "color: darkgreen;");
                    }
                    else
                    {
                        iconMEDICAL.Attributes.Add("class", "fa fa-times");
                        iconMEDICAL.Attributes.Add("style", "color: red;");
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds2 = dal_DB.DB_LIST_SP(Action: "EXPORT_LISTING_DETAILS");

                string xlname = Session["PROJECTIDTEXT"].ToString() + "_Listings_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

                DataSet dsExport = new DataSet();

                for (int i = 1; i < ds2.Tables.Count; i++)
                {
                    ds2.Tables[i].TableName = ds2.Tables[i - 1].Rows[0][0].ToString();
                    dsExport.Tables.Add(ds2.Tables[i].Copy());
                    i++;
                }

                Multiple_Export_Excel.ToExcel(dsExport, xlname, Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}