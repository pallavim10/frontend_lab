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
    public partial class NCTMS_AddComments : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_ID"] == null)
            {
                Response.Redirect("SessionExpired.aspx");
            }
            try
            {
                if (!IsPostBack)
                {
                    hdnSectionId.Value = Request.QueryString["SectionID"];
                    hdnSubSectionId.Value = Request.QueryString["SubSectionID"];
                    hdnAction.Value = Request.QueryString["ACTION"];
                    hdnRECID.Value = Request.QueryString["RECID"];

                    lblSiteId.Text = Request.QueryString["INVID"].ToString();
                    lblVisit.Text = Request.QueryString["Section"].ToString();
                    lblSVID.Text = Request.QueryString["SVID"].ToString();
                    lblModuleName.Text = Request.QueryString["MODULENAME"].ToString();

                    GetData();
                    fill_SUBJID();

                    if (hdnAction.Value.Contains("APPROVED"))
                    {
                        divAddUpdateComment.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        public void GetData()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.GetSetChecklistComments(
                    Action: "GET_PAGE_COMMENT",
                    PROJECTID: Session["ProjectId"].ToString(),
                    INVID: Request.QueryString["INVID"].ToString(),
                    ChecklistID: Request.QueryString["SVID"].ToString(),
                    SECTIONID: hdnSectionId.Value,
                    SUBSECTIONID: hdnSubSectionId.Value
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    repeatData.DataSource = ds.Tables[0];
                    repeatData.DataBind();
                }
                else
                {
                    repeatData.DataSource = null;
                    repeatData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }

        private void fill_SUBJID()
        {
            try
            {
                DAL dal;
                dal = new DAL();

                DataSet ds = dal.GetSiteID(
                Action: "SUBJECT",
                Project_Name: Session["PROJECTIDTEXT"].ToString(),
                INVID: Request.QueryString["INVID"].ToString()
                );

                drp_SUBJID.DataSource = ds.Tables[0];
                drp_SUBJID.DataValueField = "SUBJID";
                drp_SUBJID.DataBind();
                drp_SUBJID.Items.Insert(0, new ListItem("--None--", "0"));
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void repeatData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;

                    string Issue = row["Issue"].ToString();
                    string Internal = row["Internal"].ToString();
                    string Followup = row["Followup"].ToString();
                    string Observation = row["Observation"].ToString();
                    string Report = row["Report"].ToString();
                    string PD = row["PD"].ToString();

                    Label lblHeader = (Label)e.Item.FindControl("lblHeader");
                    HtmlGenericControl divInternal = e.Item.FindControl("divInternal") as HtmlGenericControl;
                    HtmlGenericControl divSponsorComment = e.Item.FindControl("divSponsorComment") as HtmlGenericControl;
                    HtmlGenericControl divPMComment = e.Item.FindControl("divPMComment") as HtmlGenericControl;
                    Button btnDelete = (Button)e.Item.FindControl("lbtnDelete");
                    Button lbtnEdit = (Button)e.Item.FindControl("lbtnEdit");

                    if (row["SPONSOR_COMMENTS"].ToString() == "")
                    {
                        divSponsorComment.Visible = false;
                    }
                    else
                    {
                        divSponsorComment.Visible = true;
                    }

                    if (row["PM_COMMENTS"].ToString() == "")
                    {
                        divPMComment.Visible = false;
                    }
                    else
                    {
                        divPMComment.Visible = true;
                    }

                    if (Report == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Item.FindControl("chkReport");
                        CHK.Checked = true;
                    }

                    if (Internal == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Item.FindControl("chk_Internal");
                        CHK.Checked = true;
                    }
                    if (Followup == "True")
                    {
                        CheckBox CHK = (CheckBox)e.Item.FindControl("chk_FollowUp");
                        CHK.Checked = true;
                    }
                    if (PD == "True")
                    {
                        CheckBox CHK_PD = (CheckBox)e.Item.FindControl("chkPD");
                        CHK_PD.Checked = true;
                    }

                    if (hdnAction.Value.Contains("SPONSOR"))
                    {
                        divInternal.Visible = false;
                    }
                    else
                    {
                        divInternal.Visible = true;
                    }

                    if (hdnAction.Value.Contains("SPONSOR"))
                    {
                        divInternal.Visible = false;
                    }
                    else
                    {
                        divInternal.Visible = true;
                    }

                    if (row["CRA_COM_USER_ID"].ToString() == Session["User_ID"].ToString())
                    {
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnDelete.Visible = false;
                    }

                    if (hdnAction.Value.Contains("APPROVED"))
                    {
                        lbtnEdit.Visible = false;
                        btnDelete.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatData_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();

            ViewState["PAGECOMMENTID"] = ID;

            if (e.CommandName == "EDITCOMMENT")
            {
                GET_CHECKLISTDATA_BYID(ID);
            }
            else if (e.CommandName == "DELETECOMMENT")
            {
                dal.GetSetChecklistComments(
                       Action: "DELETE_Comments",
                       PROJECTID: ID
                   );

                GetData();
            }
        }

        protected void bntSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strdata = "";

                strdata = txtCRAComments.Text;

                if (strdata == "")
                {
                    throw new Exception("Please insert Comment");
                }
                string Issue = "False";
                string Observation = "False";
                string Internal = "False";
                string Followup = "False";
                string Report = "False";
                string PD = "False";

                if (chk_Internal.Checked)
                {
                    Internal = "True";
                }
                if (chk_FollowUp.Checked)
                {
                    Followup = "True";
                }
                if (chkReport.Checked)
                {
                    Report = "True";
                }
                if (chkPD.Checked)
                {
                    PD = "True";
                }

                if (ViewState["PAGECOMMENTID"] == null)
                {
                    dal.GetSetChecklistComments(
                      Action: "INSERT_Comments",
                      PROJECTID: Session["PROJECTID"].ToString(),
                      INVID: Request.QueryString["INVID"].ToString(),
                      SUBJID: drp_SUBJID.SelectedValue,
                      ChecklistID: Request.QueryString["SVID"].ToString(),
                      SECTIONID: hdnSectionId.Value,
                      SUBSECTIONID: hdnSubSectionId.Value,
                      Comments: txtCRAComments.Text,
                      Issue: Issue,
                      Observation: Observation,
                      Internal: Internal,
                      Followup: Followup,
                      Report: Report,
                      ENTEREDBY: Session["USER_ID"].ToString(),
                      PD: PD,
                      RECID: hdnRECID.Value
                      );
                }
                else
                {
                    dal.GetSetChecklistComments(
                        Action: "UPDATE_Comments",
                        SUBJID: drp_SUBJID.SelectedValue,
                        Comments: txtCRAComments.Text,
                        Issue: Issue,
                        Observation: Observation,
                        Internal: Internal,
                        Followup: Followup,
                        Report: Report,
                        PD: PD,
                        ENTEREDBY: Session["USER_ID"].ToString(),
                        ID: ViewState["PAGECOMMENTID"].ToString()
                        );
                }

                string URL = Request.RawUrl;
                Response.Write("<script> alert('Record Updated successfully.');window.location='" + URL + "'; </script>");
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
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        protected void GET_CHECKLISTDATA_BYID(string ID)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dal.GetSetChecklistComments(
                    Action: "GET_Comments_BYID",
                    ChecklistID: ID
                );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblNewComment.Text = "Update Section Comment";

                    txtCRAComments.Text = ds.Tables[0].Rows[0]["Comments"].ToString();
                    txtPMComment.Text = ds.Tables[0].Rows[0]["PM_COMMENTS"].ToString();
                    txtSponsorComment.Text = ds.Tables[0].Rows[0]["SPONSOR_COMMENTS"].ToString();

                    fill_SUBJID();

                    if (ds.Tables[0].Rows[0]["SUBJID"].ToString() != "")
                    {
                        drp_SUBJID.SelectedValue = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["Internal"].ToString() == "True")
                    {
                        chk_Internal.Checked = true;
                    }
                    else
                    {
                        chk_Internal.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["Followup"].ToString() == "True")
                    {
                        chk_FollowUp.Checked = true;
                    }
                    else
                    {
                        chk_FollowUp.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["Report"].ToString() == "True")
                    {
                        chkReport.Checked = true;
                    }
                    else
                    {
                        chkReport.Checked = false;
                    }

                    if (ds.Tables[0].Rows[0]["PD"].ToString() == "True")
                    {
                        chkPD.Checked = true;
                        chkPD.Enabled = false;
                    }
                    else
                    {
                        chkPD.Checked = false;
                    }

                    txtCRAComments.Enabled = true;
                    txtPMComment.Enabled = false;
                    txtSponsorComment.Enabled = false;

                    if (ds.Tables[0].Rows[0]["PM_COMMENTS"].ToString() == "")
                    {
                        divPMCOMMENT.Visible = false;
                    }
                    else
                    {
                        divPMCOMMENT.Visible = true;
                    }

                    if (ds.Tables[0].Rows[0]["SPONSOR_COMMENTS"].ToString() == "")
                    {
                        divSPONSORCOMMENT.Visible = false;
                    }
                    else
                    {
                        divSPONSORCOMMENT.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}