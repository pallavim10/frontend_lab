using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_Approve_UnFreeze : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", false);
                }

                if (!IsPostBack)
                {
                    FillINV();
                    FillSubject();
                    FillVisit();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        public void FillINV()
        {
            DAL dal = new DAL();

            DataSet ds = dal.GET_INVID_SP(
                    USERID: Session["User_ID"].ToString()
                    );

            drpInvID.DataSource = ds.Tables[0];
            drpInvID.DataValueField = "INVID";
            drpInvID.DataBind();

            FillSubject();
        }

        private void FillVisit()
        {
            try
            {
                DataSet ds = dal_DM.DM_VISIT_SP(SUBJID: drpSubID.SelectedValue);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    if (dr["CritID"].ToString() != "" && dr["CritID"].ToString() != "0" && !toBeDeleted_Visit(dr["CritID"].ToString()) && drpSubID.SelectedValue != "Select")
                    {
                        dr.Delete();
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpVisit.DataSource = ds.Tables[0];
                    drpVisit.DataValueField = "VISITNUM";
                    drpVisit.DataTextField = "VISIT";
                    drpVisit.DataBind();
                    drpVisit.Items.Insert(0, new ListItem("--All Visit--", "0"));
                }
                else
                {
                    drpVisit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DAL dal = new DAL();

                DataSet ds = dal.GET_SUBJECT_SP(INVID: drpInvID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                }
                else
                {
                    drpSubID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                FillVisit();

                Grd_Freeze.DataSource = null;
                Grd_Freeze.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSubID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillVisit();

                Grd_Freeze.DataSource = null;
                Grd_Freeze.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Grd_Freeze.DataSource = null;
                Grd_Freeze.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private bool toBeDeleted_Visit(string CritID)
        {
            bool res = false;
            try
            {
                DataSet ds = dal_DM.DM_VISIT_CRITERIA_SP(
                ID: CritID,
                SUBJID: drpSubID.SelectedValue,
                SITEID: drpInvID.SelectedValue
                );

                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "1")
                {
                    res = true;
                }
                else
                {
                    res = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                res = false;
            }
            return res;
        }

        protected void Btn_Get_Data_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal_DM.DM_UNFREEZE_REQUEST_SP(ACTION: "GET_UNFREEZE_REQUEST_DATA",
                    INVID: drpInvID.SelectedValue,
                    SUBJID: drpSubID.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue
                    );

                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_Freeze.DataSource = ds.Tables[0];
                        Grd_Freeze.DataBind();
                    }
                    else
                    {
                        Grd_Freeze.DataSource = null;
                        Grd_Freeze.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }

        }

        protected void Grd_Freeze_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    Label lblPVID = e.Row.FindControl("lblPVID") as Label;
                    Label lblVISITNUM = e.Row.FindControl("lblVISITNUM") as Label;
                    Label lblMODULEID = e.Row.FindControl("lblMODULEID") as Label;
                    Label lblTableName = (Label)e.Row.FindControl("lblTableName");
                    Label lblSUBJID = e.Row.FindControl("lblSUBJID") as Label;
                    LinkButton view = e.Row.FindControl("lbview") as LinkButton;
                    Label MULTIPLEYN = (Label)e.Row.FindControl("MULTIPLEYN");
                    HtmlGenericControl div = (HtmlGenericControl)e.Row.FindControl("anchor");

                    Image lnkVISITPAGENO = e.Row.FindControl("lnkPAGENUM") as Image;

                    GridView Gridchild = (GridView)e.Row.FindControl("Gridchild");
                    if (dr["PAGESTATUS"].ToString() == "1")
                    {
                        if (dr["HasMissing"].ToString() == "True")
                        {
                            lnkVISITPAGENO.ImageUrl = "img/REDFILE.png";
                            lnkVISITPAGENO.ToolTip = "Missing Fields";
                        }
                        else if (dr["IsComplete"].ToString() == "0")
                        {
                            lnkVISITPAGENO.ImageUrl = "img/orange file.png";
                            lnkVISITPAGENO.ToolTip = "Incomplete";
                        }
                        else if (dr["IsComplete"].ToString() == "2")
                        {
                            lnkVISITPAGENO.ImageUrl = "img/NotApplicableFile.png";
                            lnkVISITPAGENO.ToolTip = "Not Applicable";
                        }
                        else
                        {
                            lnkVISITPAGENO.ImageUrl = "img/green file.png";
                            lnkVISITPAGENO.ToolTip = "Completed";
                        }
                    }
                    else
                    {
                        lnkVISITPAGENO.ToolTip = "Not Entered";
                    }

                    if (MULTIPLEYN.Text == "True")
                    {
                        div.Visible = true;
                        view.Visible = false;

                        DataSet ds = dal_DM.DM_UNFREEZE_REQUEST_SP(ACTION: "GET_MULTIPLE_RECORDS_UNFREEZ_REQUEST",
                            PVID: lblPVID.Text,
                            VISITNUM: lblVISITNUM.Text,
                            MODULEID: lblMODULEID.Text,
                            TABLENAME: lblTableName.Text,
                            SUBJID: lblSUBJID.Text
                            );

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Gridchild.DataSource = ds.Tables[0];
                            Gridchild.DataBind();
                        }
                        else
                        {
                            Gridchild.DataSource = null;
                            Gridchild.DataBind();
                        }
                    }
                    else
                    {
                        div.Visible = false;
                        view.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void Gridchild_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    CheckBox Chk_ReqApp_Child = (CheckBox)e.Row.FindControl("Chk_ReqApp_Child");
                    CheckBox Chk_ReqDisApp_Child = (CheckBox)e.Row.FindControl("Chk_ReqDisApp_Child");
                    Label lblQuery = (Label)e.Row.FindControl("lblQuery");

                    Image lnkVISITPAGENO = e.Row.FindControl("lnkPAGENUM") as Image;

                    if (dr["PAGESTATUS"].ToString() == "1")
                    {
                        if (dr["HasMissing"].ToString() == "True")
                        {
                            lnkVISITPAGENO.ImageUrl = "img/REDFILE.png";
                            lnkVISITPAGENO.ToolTip = "Missing Fields";
                        }
                        else if (dr["IsComplete"].ToString() == "0")
                        {
                            lnkVISITPAGENO.ImageUrl = "img/orange file.png";
                            lnkVISITPAGENO.ToolTip = "Incomplete";
                        }
                        else if (dr["IsComplete"].ToString() == "2")
                        {
                            lnkVISITPAGENO.ImageUrl = "img/NotApplicableFile.png";
                            lnkVISITPAGENO.ToolTip = "Not Applicable";
                        }
                        else
                        {
                            lnkVISITPAGENO.ImageUrl = "img/green file.png";
                            lnkVISITPAGENO.ToolTip = "Completed";
                        }
                    }
                    else
                    {
                        lnkVISITPAGENO.ToolTip = "Not Entered";
                    }


                    if (dr["DELETE"].ToString() == "True")
                    {
                        Chk_ReqApp_Child.Visible = false;
                        Chk_ReqDisApp_Child.Visible = false;
                        lblQuery.Visible = true;
                        lblQuery.Text = "Data Deleted";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Grd_Freeze.Rows.Count; i++)
                {
                    CheckBox Chk_ReqApp = (CheckBox)Grd_Freeze.Rows[i].FindControl("Chk_ReqApp");

                    Label lblSUBJID = (Label)Grd_Freeze.Rows[i].FindControl("lblSUBJID");

                    Label lblVISITNUM = (Label)Grd_Freeze.Rows[i].FindControl("lblVISITNUM");

                    Label lblVISIT = (Label)Grd_Freeze.Rows[i].FindControl("lblVISIT");

                    Label lblMODULEID = (Label)Grd_Freeze.Rows[i].FindControl("lblMODULEID");

                    Label lblMODULENAME = (Label)Grd_Freeze.Rows[i].FindControl("lblMODULENAME");

                    Label lblPVID = (Label)Grd_Freeze.Rows[i].FindControl("lblPVID");

                    Label MULTIPLEYN = (Label)Grd_Freeze.Rows[i].FindControl("MULTIPLEYN");

                    Label lblTableName = (Label)Grd_Freeze.Rows[i].FindControl("lblTableName");

                    Label lblRECID = (Label)Grd_Freeze.Rows[i].FindControl("lblRECID");

                    Label PAGESTATUS = (Label)Grd_Freeze.Rows[i].FindControl("PAGESTATUS");

                    if (PAGESTATUS.Text == "2")
                    {
                        if (Chk_ReqApp.Checked)
                        {
                            dal_DM.DM_UNFREEZE_REQUEST_SP(ACTION: "UPDATE_UNFREEZE_REQUEST_APPROVED",
                                PVID: lblPVID.Text,
                                RECID: lblRECID.Text,
                                INVID: drpInvID.SelectedValue,
                                SUBJID: lblSUBJID.Text,
                                VISITNUM: lblVISITNUM.Text,
                                MODULEID: lblMODULEID.Text,
                                COMMENT: txtComment.Text,
                                VISIT: lblVISIT.Text,
                                MODULENAME: lblMODULENAME.Text
                                );

                            dal_DM.DM_SYNC_EDC_DATA(PVID: lblPVID.Text,
                                    RECID: lblRECID.Text,
                                    INVID: drpInvID.SelectedValue,
                                    VISITNUM: lblVISITNUM.Text,
                                    MODULEID: lblMODULEID.Text
                                    );
                        }
                    }
                    else
                    {
                        if (MULTIPLEYN.Text == "True")
                        {
                            GridView Gridchild = (GridView)Grd_Freeze.Rows[i].FindControl("Gridchild");

                            for (int j = 0; j < Gridchild.Rows.Count; j++)
                            {
                                CheckBox Chk_ReqApp_Child = (CheckBox)Gridchild.Rows[j].FindControl("Chk_ReqApp_Child");

                                if (Chk_ReqApp_Child.Checked)
                                {
                                    dal_DM.DM_UNFREEZE_REQUEST_SP(ACTION: "UPDATE_UNFREEZE_REQUEST_APPROVED",
                                    PVID: lblPVID.Text,
                                    RECID: ((Label)Gridchild.Rows[j].FindControl("lblRECID")).Text,
                                    INVID: drpInvID.SelectedValue,
                                    SUBJID: lblSUBJID.Text,
                                    VISITNUM: lblVISITNUM.Text,
                                    MODULEID: lblMODULEID.Text,
                                    COMMENT: txtComment.Text,
                                    VISIT: lblVISIT.Text,
                                    MODULENAME: lblMODULENAME.Text
                                    );

                                    dal_DM.DM_SYNC_EDC_DATA(PVID: lblPVID.Text,
                                        RECID: ((Label)Gridchild.Rows[j].FindControl("lblRECID")).Text,
                                        INVID: drpInvID.SelectedValue,
                                        VISITNUM: lblVISITNUM.Text,
                                        MODULEID: lblMODULEID.Text
                                        );
                                }
                            }
                        }
                        else
                        {
                            if (Chk_ReqApp.Checked)
                            {
                                dal_DM.DM_UNFREEZE_REQUEST_SP(ACTION: "UPDATE_UNFREEZE_REQUEST_APPROVED",
                                    PVID: lblPVID.Text,
                                    RECID: lblRECID.Text,
                                    INVID: drpInvID.SelectedValue,
                                    SUBJID: lblSUBJID.Text,
                                    VISITNUM: lblVISITNUM.Text,
                                    MODULEID: lblMODULEID.Text,
                                    COMMENT: txtComment.Text,
                                    VISIT: lblVISIT.Text,
                                    MODULENAME: lblMODULENAME.Text
                                    );

                                dal_DM.DM_SYNC_EDC_DATA(PVID: lblPVID.Text,
                                        RECID: lblRECID.Text,
                                        INVID: drpInvID.SelectedValue,
                                        VISITNUM: lblVISITNUM.Text,
                                        MODULEID: lblMODULEID.Text
                                        );
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('UnFreeze Request Approved Successfully'); window.location.href='DM_Approve_UnFreeze.aspx'", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Btn_DisApprove_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Grd_Freeze.Rows.Count; i++)
                {
                    CheckBox Chk_ReqDisApp = (CheckBox)Grd_Freeze.Rows[i].FindControl("Chk_ReqDisApp");

                    Label lblSUBJID = (Label)Grd_Freeze.Rows[i].FindControl("lblSUBJID");

                    Label lblVISITNUM = (Label)Grd_Freeze.Rows[i].FindControl("lblVISITNUM");

                    Label lblVISIT = (Label)Grd_Freeze.Rows[i].FindControl("lblVISIT");

                    Label lblMODULEID = (Label)Grd_Freeze.Rows[i].FindControl("lblMODULEID");

                    Label lblMODULENAME = (Label)Grd_Freeze.Rows[i].FindControl("lblMODULENAME");

                    Label lblPVID = (Label)Grd_Freeze.Rows[i].FindControl("lblPVID");

                    Label MULTIPLEYN = (Label)Grd_Freeze.Rows[i].FindControl("MULTIPLEYN");

                    Label lblTableName = (Label)Grd_Freeze.Rows[i].FindControl("lblTableName");

                    Label lblRECID = (Label)Grd_Freeze.Rows[i].FindControl("lblRECID");

                    Label PAGESTATUS = (Label)Grd_Freeze.Rows[i].FindControl("PAGESTATUS");

                    if (PAGESTATUS.Text == "2")
                    {
                        if (Chk_ReqDisApp.Checked)
                        {
                            dal_DM.DM_UNFREEZE_REQUEST_SP(ACTION: "UPDATE_UNFREEZE_REQUEST_DISAPPROVED",
                                PVID: lblPVID.Text,
                                RECID: lblRECID.Text,
                                INVID: drpInvID.SelectedValue,
                                SUBJID: lblSUBJID.Text,
                                VISITNUM: lblVISITNUM.Text,
                                MODULEID: lblMODULEID.Text,
                                COMMENT: txtComment.Text,
                                VISIT: lblVISIT.Text,
                                MODULENAME: lblMODULENAME.Text
                                );
                        }
                    }
                    else
                    {
                        if (MULTIPLEYN.Text == "True")
                        {
                            GridView Gridchild = (GridView)Grd_Freeze.Rows[i].FindControl("Gridchild");
                            for (int j = 0; j < Gridchild.Rows.Count; j++)
                            {
                                CheckBox Chk_ReqDisApp_Child = (CheckBox)Gridchild.Rows[j].FindControl("Chk_ReqDisApp_Child");

                                if (Chk_ReqDisApp_Child.Checked)
                                {
                                    dal_DM.DM_UNFREEZE_REQUEST_SP(ACTION: "UPDATE_UNFREEZE_REQUEST_DISAPPROVED",
                                    PVID: lblPVID.Text,
                                    RECID: ((Label)Gridchild.Rows[j].FindControl("lblRECID")).Text,
                                    INVID: drpInvID.SelectedValue,
                                    SUBJID: lblSUBJID.Text,
                                    VISITNUM: lblVISITNUM.Text,
                                    MODULEID: lblMODULEID.Text,
                                    COMMENT: txtComment.Text,
                                    VISIT: lblVISIT.Text,
                                    MODULENAME: lblMODULENAME.Text
                                    );
                                }
                            }
                        }
                        else
                        {
                            if (Chk_ReqDisApp.Checked)
                            {
                                dal_DM.DM_UNFREEZE_REQUEST_SP(ACTION: "UPDATE_UNFREEZE_REQUEST_DISAPPROVED",
                                    PVID: lblPVID.Text,
                                    RECID: lblRECID.Text,
                                    INVID: drpInvID.SelectedValue,
                                    SUBJID: lblSUBJID.Text,
                                    VISITNUM: lblVISITNUM.Text,
                                    MODULEID: lblMODULEID.Text,
                                    COMMENT: txtComment.Text,
                                    VISIT: lblVISIT.Text,
                                    MODULENAME: lblMODULENAME.Text
                                    );
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('UnFreeze Request Disapproved Successfully'); window.location.href='DM_Approve_UnFreeze.aspx'", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }
    }
}