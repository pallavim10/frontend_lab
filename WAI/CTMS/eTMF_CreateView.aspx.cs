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
    public partial class eTMF_CreateView : System.Web.UI.Page
    {
        DAL_eTMF dal_eTMF = new DAL_eTMF();
        DAL dal = new DAL();

        CommonFunction.CommonFunction ComFun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("~/SessionExpired.aspx", false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Get_Views();
                        COUNTRY();
                        SITE_AGAINST_COUNTRY();
                        GetUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_COUNTRY_SP(USERID: Session["User_ID"].ToString());
                drpCountry.DataSource = ds.Tables[0];
                drpCountry.DataTextField = "COUNTRYNAME";
                drpCountry.DataValueField = "COUNTRYID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void SITE_AGAINST_COUNTRY()
        {
            try
            {
                DataSet ds = dal.GET_INVID_SP(COUNTRYID: drpCountry.SelectedValue, USERID: Session["User_ID"].ToString());
                drpSites.DataSource = ds.Tables[0];
                drpSites.DataTextField = "INVID";
                drpSites.DataValueField = "INVID";
                drpSites.DataBind();
                drpSites.Items.Insert(0, new ListItem("None", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SITE_AGAINST_COUNTRY();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Views()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "Get_Views");
                grdViews.DataSource = ds;
                grdViews.DataBind();

                drpView.DataSource = ds;
                drpView.DataValueField = "ID";
                drpView.DataTextField = "ViewName";
                drpView.DataBind();
                drpView.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Insert_View()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_View_SP(
                ACTION: "Insert_View",
                SEQNO: txtSEQNO.Text,
                DocName: txtViewName.Text,
                UploadBy: Session["User_ID"].ToString(),
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue
                );



                dal_eTMF.eTMF_View_SP(ACTION: "Remove_ViewUsers", ID: ds.Tables[0].Rows[0]["ID"].ToString());
                foreach (ListItem item in lstUsers.Items)
                {
                    if (item.Selected == true)
                    {
                        dal_eTMF.eTMF_View_SP(ACTION: "Insert_ViewUsers", ID: ds.Tables[0].Rows[0]["ID"].ToString(), UploadBy: item.Value);
                    }
                }

                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("eTMF");

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Update_View()
        {
            try
            {

                if (txtSEQNO.Text.Trim() == "")
                {
                    Response.Write("<script language=javascript>alert('Please Enter Sequence No');</script>");
                    return;
                }

                dal_eTMF.eTMF_View_SP(
                ACTION: "Update_View",
                SEQNO: txtSEQNO.Text,
                DocName: txtViewName.Text,
                ID: ViewState["ViewID"].ToString(),
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue
                );


                dal_eTMF.eTMF_View_SP(ACTION: "Remove_ViewUsers", ID: ViewState["ViewID"].ToString());
                foreach (ListItem item in lstUsers.Items)
                {
                    if (item.Selected == true)
                    {
                        dal_eTMF.eTMF_View_SP(ACTION: "Insert_ViewUsers", ID: ViewState["ViewID"].ToString(), UploadBy: item.Value);
                    }
                }

                SiteMaster master = Master as SiteMaster;
                master.PopulateMenuControlChildItem("eTMF");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Select_View(string ID)
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "Select_View", ID: ID);

                txtSEQNO.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                txtViewName.Text = ds.Tables[0].Rows[0]["ViewName"].ToString();
                drpCountry.SelectedValue = ds.Tables[0].Rows[0]["CountryId"].ToString();
                SITE_AGAINST_COUNTRY();
                drpSites.SelectedValue = ds.Tables[0].Rows[0]["SiteId"].ToString();

                GetUsers();
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    if (dr["USERID"] != "")
                    {
                        ListItem itm = lstUsers.Items.FindByValue(dr["USERID"].ToString());
                        if (itm != null)
                            itm.Selected = true;
                        else
                            itm.Selected = false;
                    }
                }

                btnSubmitView.Visible = false;
                btnUpdateView.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Delete_View(string ID)
        {
            try
            {

                dal_eTMF.eTMF_View_SP(ACTION: "Delete_View", ID: ID);
                Cancel_view();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Cancel_view()
        {
            try
            {
                btnSubmitView.Visible = true;
                btnUpdateView.Visible = false;
                txtSEQNO.Text = "";
                txtViewName.Text = "";
                COUNTRY();
                SITE_AGAINST_COUNTRY();
                Get_Views();
                GetUsers();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetUsers()
        {
            try
            {
                DataSet ds = dal_eTMF.eTMF_View_SP(
                ACTION: "GetUsers",
                CountryID: drpCountry.SelectedValue,
                SiteID: drpSites.SelectedValue
                );

                lstUsers.Items.Clear();
                lstUsers.DataSource = ds;
                lstUsers.DataValueField = "User_ID";
                lstUsers.DataTextField = "User_Name";
                lstUsers.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grdViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditView")
                {
                    ViewState["ViewID"] = e.CommandArgument.ToString();

                    Select_View(e.CommandArgument.ToString());
                }
                else if (e.CommandName == "DeleteView")
                {
                    Delete_View(e.CommandArgument.ToString());

                    SiteMaster master = Master as SiteMaster;
                    master.PopulateMenuControlChildItem("eTMF");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitView_Click(object sender, EventArgs e)
        {
            try
            {
                if (ifExists())
                {
                    Insert_View();
                    Cancel_view();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ALERT", "alert('View name already exists. Please use different View name'); ", true);
                    txtViewName.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool ifExists()
        {
            bool res = true;
            try
            {
                DataSet ds = dal.eTMF_View_SP(ACTION: "CHECK_VIEWNAME", DocName: txtViewName.Text);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
            return res;
        }

        protected void btnUpdateView_Click(object sender, EventArgs e)
        {
            try
            {
                Update_View();
                Cancel_view();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancelView_Click(object sender, EventArgs e)
        {
            try
            {
                Cancel_view();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpView.SelectedIndex != 0)
                {
                    DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "GET_DOC_ARTIFACT");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvArtifact.DataSource = ds.Tables[0];
                        gvArtifact.DataBind();
                        btnAddFiles.Visible = true;
                    }
                    else
                    {
                        gvArtifact.DataSource = null;
                        gvArtifact.DataBind();
                        btnAddFiles.Visible = false;
                    }
                }
                else
                {
                    gvArtifact.DataSource = null;
                    gvArtifact.DataBind();

                    btnAddFiles.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvArtifact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string MainRefNo = drv["MainRefNo"].ToString();
                    string DocTypeId = drv["DocTypeId"].ToString();

                    GridView gvDocs = e.Row.FindControl("gvDocs") as GridView;

                    DataSet ds = dal_eTMF.eTMF_View_SP(ACTION: "GET_BD_DOCS_AGAINST_ARTIFACT", RefNo: MainRefNo, DOCTYPEID: DocTypeId, ID: drpView.SelectedValue);
                    gvDocs.DataSource = ds.Tables[0];
                    gvDocs.DataBind();

                    Control anchor = e.Row.FindControl("anchor") as Control;
                    if (gvDocs.Rows.Count > 0)
                    {
                        anchor.Visible = true;
                    }
                    else
                    {
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gvDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string COUNTS = drv["COUNTS"].ToString();

                    CheckBox chkDoc = e.Row.FindControl("chkDoc") as CheckBox;

                    if (COUNTS == "0")
                    {
                        chkDoc.Checked = false;
                    }
                    else
                    {
                        chkDoc.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnAddFiles_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvArtifact.Rows.Count; i++)
                {
                    GridView gvDocs = (GridView)gvArtifact.Rows[i].FindControl("gvDocs");

                    for (int j = 0; j < gvDocs.Rows.Count; j++)
                    {
                        CheckBox chkDoc = (CheckBox)gvDocs.Rows[j].FindControl("chkDoc");

                        string ID = ((Label)gvDocs.Rows[j].FindControl("ID")).Text;

                        if (chkDoc.Checked)
                        {
                            DataSet ds = dal_eTMF.eTMF_View_SP
                                (ACTION: "Insert_View_Docs",
                                ID: drpView.SelectedValue,
                                DocID: ID);


                        }
                        else
                        {
                            DataSet ds = dal_eTMF.eTMF_View_SP
                                (ACTION: "Delete_View_Docs",
                                ID: drpView.SelectedValue,
                                DocID: ID);

                        }
                    }
                }

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}