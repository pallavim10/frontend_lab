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
    public partial class NCTMS_AddChecklistComments_SPONSOR : System.Web.UI.Page
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
                    hdnVISITID.Value = Request.QueryString["VISITID"];
                    hdnMODULEID.Value = Request.QueryString["MODULEID"];
                    hdnAction.Value = Request.QueryString["ACTION"];
                    hdnRECID.Value = Request.QueryString["RECID"];

                    lblSiteId.Text = Request.QueryString["INVID"].ToString();
                    lblVisit.Text = Request.QueryString["VISIT"].ToString();
                    lblSVID.Text = Request.QueryString["SVID"].ToString();
                    lblModuleName.Text = Request.QueryString["MODULENAME"].ToString();
                    lblFieldName.Text = Request.QueryString["FIELDNAME"].ToString();

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
                    Action: "GET_CHECKLIST_COMMENTS_SPONSOR",
                    PROJECTID: Session["ProjectId"].ToString(),
                    INVID: lblSiteId.Text,
                    ChecklistID: lblSVID.Text,
                    SECTIONID: hdnVISITID.Value,
                    SUBSECTIONID: hdnMODULEID.Value,
                    CheckListRow_ID: Request.QueryString["FIELDID"].ToString(),
                    RECID: hdnRECID.Value
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
                    HtmlGenericControl divCRACOMMENT = e.Item.FindControl("divCRACOMMENT") as HtmlGenericControl;
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

                    if (row["Comments"].ToString() == "")
                    {
                        divCRACOMMENT.Visible = false;
                    }
                    else
                    {
                        divCRACOMMENT.Visible = true;
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

                    if (row["Comments"].ToString() == "" && row["PM_COMMENTS"].ToString() == "")
                    {
                        if (row["SP_COM_USER_ID"].ToString() == Session["User_ID"].ToString())
                        {
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            btnDelete.Visible = false;
                        }
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

            ViewState["SPONSRCOMMID"] = ID;

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

                strdata = txtSponsorComment.Text;

                if (strdata == "")
                {
                    throw new Exception("Please insert Comment");
                }

                if (ViewState["SPONSRCOMMID"] == null)
                {
                    dal.GetSetChecklistComments(
                           Action: "INSERT_Comments_SPONSOR",
                           INVID: lblSiteId.Text,
                           SUBJID: drp_SUBJID.SelectedValue,
                           ChecklistID: lblSVID.Text,
                           SECTIONID: hdnVISITID.Value,
                           SUBSECTIONID: hdnMODULEID.Value,
                           Comments: txtSponsorComment.Text,
                           ENTEREDBY: Session["USER_ID"].ToString(),
                           CheckListRow_ID: Request.QueryString["FIELDID"].ToString(),
                           RECID: hdnRECID.Value
                           );
                }
                else
                {
                    dal.GetSetChecklistComments(
                          Action: "UPDATE_SPONSOR_COMMENT",
                          INVID: lblSiteId.Text,
                          SUBJID: drp_SUBJID.SelectedValue,
                          ChecklistID: lblSVID.Text,
                          SECTIONID: hdnVISITID.Value,
                          SUBSECTIONID: hdnMODULEID.Value,
                          Comments: txtSponsorComment.Text,
                          ENTEREDBY: Session["USER_ID"].ToString(),
                          CheckListRow_ID: Request.QueryString["FIELDID"].ToString(),
                          ID: ViewState["SPONSRCOMMID"].ToString(),
                          RECID: hdnRECID.Value
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
                    divTypeOfComment.Visible = true;
                    fill_SUBJID();

                    if (ds.Tables[0].Rows[0]["SUBJID"].ToString() != "")
                    {
                        drp_SUBJID.SelectedValue = ds.Tables[0].Rows[0]["SUBJID"].ToString();
                    }

                    txtCRAComments.Enabled = false;
                    txtPMComment.Enabled = false;
                    txtSponsorComment.Enabled = true;

                    chk_FollowUp.Enabled = false;
                    chk_Internal.Enabled = false;
                    chkPD.Enabled = false;
                    chkReport.Enabled = false;

                    if (ds.Tables[0].Rows[0]["Comments"].ToString() == "")
                    {
                        divCRACOMMENT.Visible = false;
                    }
                    else
                    {
                        divCRACOMMENT.Visible = true;
                    }

                    if (ds.Tables[0].Rows[0]["PM_COMMENTS"].ToString() == "")
                    {
                        divPMCOMMENT.Visible = false;
                    }
                    else
                    {
                        divPMCOMMENT.Visible = true;
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
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}