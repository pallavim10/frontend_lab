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
    public partial class NCTMS_View_ChecklistComments_SPONSOR : System.Web.UI.Page
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

                    GetData();
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
                    HtmlGenericControl divFieldName = e.Item.FindControl("divFieldName") as HtmlGenericControl;

                    if (row["FIELDNAME"].ToString() == "")
                    {
                        lblHeader.Text = "Section Comment";
                        divFieldName.Visible = false;
                    }
                    else
                    {
                        lblHeader.Text = "Field Comment";
                        divFieldName.Visible = true;
                    }

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
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}