using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_Freeze : System.Web.UI.Page
    {
        DAL_DM dal = new DAL_DM();

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
                    //FillINV();
                    //FillSubject();
                    //FillVisit();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpType.SelectedIndex != 0)
                {
                    if (drpType.SelectedValue == "1")
                    {
                        Btn_Get_Data.Visible = false;
                        btnBulkFreezing.Visible = true;
                        DivSubject.Visible = false;
                        DivVisits.Visible = false;
                    }
                    else if (drpType.SelectedValue == "2")
                    {
                        Btn_Get_Data.Visible = false;
                        btnBulkFreezing.Visible = true;
                        DivSubject.Visible = true;
                        DivVisits.Visible = false;
                    }
                    else if (drpType.SelectedValue == "3")
                    {
                        Btn_Get_Data.Visible = false;
                        btnBulkFreezing.Visible = true;
                        DivSubject.Visible = true;
                        DivVisits.Visible = true;
                    }
                    else if (drpType.SelectedValue == "4")
                    {
                        Btn_Get_Data.Visible = true;
                        btnBulkFreezing.Visible = false;
                        DivSubject.Visible = true;
                        DivVisits.Visible = true;
                    }

                    DivINV.Visible = true;
                    FillINV();
                }
                else
                {
                    DivINV.Visible = false;
                    DivSubject.Visible = false;
                    DivVisits.Visible = false;
                }

                Grd_Freeze.DataSource = null;
                Grd_Freeze.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
           

            DataSet ds = dal.DM_FREEZE_SP(
                    ACTION : "GET_INVID"
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
                string SUBJIDs = "";
                foreach (ListItem item in lstSubjects.Items)
                {
                    if (item.Selected == true)
                    {
                        if (SUBJIDs != "")
                        {
                            SUBJIDs += "," + item.Value.ToString();
                        }
                        else
                        {
                            SUBJIDs += item.Value.ToString();
                        }
                    }
                }

                DataSet ds = dal.DM_FREEZE_SP(ACTION: "GET_VISIT", SUBJID: SUBJIDs);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstVisits.DataSource = ds.Tables[0];
                    lstVisits.DataValueField = "VISITNUM";
                    lstVisits.DataTextField = "VISIT";
                    lstVisits.DataBind();
                    lstVisits.Items.Insert(0, new ListItem("--All Visit--", "0"));
                }
                else
                {
                    lstVisits.Items.Clear();
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
                
                DataSet ds = dal.DM_FREEZE_SP(ACTION: "GET_SUBJECT", INVID: drpInvID.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstSubjects.DataSource = ds.Tables[0];
                    lstSubjects.DataValueField = "SUBJID";
                    lstSubjects.DataTextField = "SUBJID";
                    lstSubjects.DataBind();
                    lstSubjects.Items.Insert(0, new ListItem("--All Subject--", "0"));
                }
                else
                {
                    lstSubjects.Items.Clear();
                    lstSubjects.Items.Clear();
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
                if (drpType.SelectedValue != "1")
                {
                    FillSubject();
                    FillVisit();

                    Grd_Freeze.DataSource = null;
                    Grd_Freeze.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void lstSubjects_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void lstVisits_SelectedIndexChanged(object sender, EventArgs e)
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
                DataSet ds = dal.DM_VISIT_CRITERIA_SP(
                ID: CritID,
                SUBJID: lstSubjects.SelectedValue,
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
                string SUBJIDs = "", VISITNUM = "";

                foreach (ListItem item in lstSubjects.Items)
                {
                    if (item.Selected == true)
                    {
                        if (SUBJIDs != "")
                        {
                            SUBJIDs += "," + item.Value.ToString();
                        }
                        else
                        {
                            SUBJIDs += item.Value.ToString();
                        }
                    }
                }

                foreach (ListItem item in lstVisits.Items)
                {
                    if (item.Selected == true)
                    {
                        if (VISITNUM != "")
                        {
                            VISITNUM += "," + item.Value.ToString();
                        }
                        else
                        {
                            VISITNUM += item.Value.ToString();
                        }
                    }
                }

                DataSet ds = dal.DM_FREEZE_SP(ACTION: "GET_FREEZE_DATA",
                    INVID: drpInvID.SelectedValue,
                    SUBJID: SUBJIDs,
                    VISITNUM: VISITNUM
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Grd_Freeze.DataSource = ds.Tables[0];
                    Grd_Freeze.DataBind();
                    selectchkall.Visible = true;
                }
                else
                {
                    Grd_Freeze.DataSource = null;
                    Grd_Freeze.DataBind();
                    selectchkall.Visible = false;
                }
            }
            catch (Exception ex)
            {
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
                    Label lblQuery = (Label)e.Row.FindControl("lblQuery");
                    LinkButton view = e.Row.FindControl("lbview") as LinkButton;
                    Label MULTIPLEYN = (Label)e.Row.FindControl("MULTIPLEYN");
                    CheckBox Chk_FreezeYN = (CheckBox)e.Row.FindControl("Chk_FreezeYN");
                    HtmlGenericControl div = (HtmlGenericControl)e.Row.FindControl("anchor");
                    LinkButton lnkQuery = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                    LinkButton lnkQUERYANS = (LinkButton)e.Row.FindControl("lnkQUERYANS");

                    GridView Gridchild = (GridView)e.Row.FindControl("Gridchild");

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

                    if (dr["QueryCount"].ToString() != "0")
                    {
                        lnkQuery.Visible = true;
                        Chk_FreezeYN.Visible = false;
                        lblQuery.Visible = true;
                    }
                    else
                    {
                        lnkQuery.Visible = false;
                        Chk_FreezeYN.Visible = true;
                        lblQuery.Visible = false;
                    }

                    if (dr["AnsQueryCount"].ToString() != "0")
                    {
                        lnkQUERYANS.Visible = true;
                        Chk_FreezeYN.Visible = false;
                        lblQuery.Visible = true;
                    }

                    if (MULTIPLEYN.Text == "True")
                    {
                        div.Visible = true;
                        view.Visible = false;

                        DataSet ds = dal.DM_FREEZE_SP(ACTION: "GET_MULTIPLE_RECORDS_FREEZ",
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

                    LinkButton lnkAUTOQUERY = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                    CheckBox Chk_FreezeYN = (CheckBox)e.Row.FindControl("Chkchild_FreezeYN");
                    Label lblQuery = (Label)e.Row.FindControl("lblQuery");
                    LinkButton lnkQUERYANS = (LinkButton)e.Row.FindControl("lnkQUERYANS");

                    Image lnkVISITPAGENO = e.Row.FindControl("lnkPAGENUM") as Image;

                    if (dr["IsMissing"].ToString() == "True")
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

                    if (dr["QueryCount"].ToString() != "0")
                    {
                        lnkAUTOQUERY.Visible = true;
                        Chk_FreezeYN.Visible = false;
                        lblQuery.Visible = true;
                    }
                    else
                    {
                        lnkAUTOQUERY.Visible = false;
                        Chk_FreezeYN.Visible = true;
                        lblQuery.Visible = false;
                    }

                    if (dr["AnsQueryCount"].ToString() != "0")
                    {
                        lnkQUERYANS.Visible = true;
                        Chk_FreezeYN.Visible = false;
                        lblQuery.Visible = true;
                    }

                    if (dr["DELETE"].ToString() == "True")
                    {
                        lnkQUERYANS.Visible = false;
                        Chk_FreezeYN.Visible = false;
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
                    CheckBox Chk_FreezeYN = (CheckBox)Grd_Freeze.Rows[i].FindControl("Chk_FreezeYN");

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
                        if (Chk_FreezeYN.Checked)
                        {
                            dal.DM_FREEZE_SP(ACTION: "UPDATE_FREEZE_STATUS",
                                PVID: lblPVID.Text,
                                RECID: lblRECID.Text,
                                INVID: drpInvID.SelectedValue,
                                SUBJID: lblSUBJID.Text,
                                VISITNUM: lblVISITNUM.Text,
                                MODULEID: lblMODULEID.Text,
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
                                CheckBox Chkchild_FreezeYN = (CheckBox)Gridchild.Rows[j].FindControl("Chkchild_FreezeYN");

                                if (Chkchild_FreezeYN.Checked)
                                {
                                    dal.DM_FREEZE_SP(ACTION: "UPDATE_FREEZE_STATUS",
                                    PVID: lblPVID.Text,
                                    RECID: ((Label)Gridchild.Rows[j].FindControl("lblRECID")).Text,
                                    INVID: drpInvID.SelectedValue,
                                    SUBJID: lblSUBJID.Text,
                                    VISITNUM: lblVISITNUM.Text,
                                    MODULEID: lblMODULEID.Text,
                                    VISIT: lblVISIT.Text,
                                    MODULENAME: lblMODULENAME.Text
                                    );
                                }
                            }
                        }
                        else
                        {
                            if (Chk_FreezeYN.Checked)
                            {
                                dal.DM_FREEZE_SP(ACTION: "UPDATE_FREEZE_STATUS",
                                    PVID: lblPVID.Text,
                                    RECID: lblRECID.Text,
                                    INVID: drpInvID.SelectedValue,
                                    SUBJID: lblSUBJID.Text,
                                    VISITNUM: lblVISITNUM.Text,
                                    MODULEID: lblMODULEID.Text,
                                    VISIT: lblVISIT.Text,
                                    MODULENAME: lblMODULENAME.Text
                                    );
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Frozen Successfully'); window.location.href='DM_Freeze.aspx'", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Chk_Select_All_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Chk_Select_All.Checked)
                {
                    for (int i = 0; i < Grd_Freeze.Rows.Count; i++)
                    {
                        CheckBox ChAction = (CheckBox)Grd_Freeze.Rows[i].FindControl("Chk_FreezeYN");

                        Label lblQuery = (Label)Grd_Freeze.Rows[i].FindControl("lblQuery");

                        if (lblQuery.Visible == false)
                        {
                            ChAction.Checked = true;
                        }
                        else
                        {
                            ChAction.Checked = false;
                        }

                        GridView Gridchild = (GridView)Grd_Freeze.Rows[i].FindControl("Gridchild");

                        for (int j = 0; j < Gridchild.Rows.Count; j++)
                        {
                            CheckBox Chkchild_FreezeYN = (CheckBox)Gridchild.Rows[j].FindControl("Chkchild_FreezeYN");

                            Label lblQuery1 = (Label)Gridchild.Rows[j].FindControl("lblQuery");

                            if (lblQuery1.Visible == false)
                            {
                                Chkchild_FreezeYN.Checked = true;
                            }
                            else
                            {
                                Chkchild_FreezeYN.Checked = false;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < Grd_Freeze.Rows.Count; i++)
                    {
                        CheckBox ChAction = (CheckBox)Grd_Freeze.Rows[i].FindControl("Chk_FreezeYN");

                        ChAction.Checked = false;

                        GridView Gridchild = (GridView)Grd_Freeze.Rows[i].FindControl("Gridchild");

                        for (int j = 0; j < Gridchild.Rows.Count; j++)
                        {
                            CheckBox Chkchild_FreezeYN = (CheckBox)Gridchild.Rows[j].FindControl("Chkchild_FreezeYN");

                            Chkchild_FreezeYN.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnBulkFreezing_Click(object sender, EventArgs e)
        {
            try
            {
                string INVID = "", SUBJIDs = "", VISITNUM = "";

                INVID = drpInvID.SelectedValue;

                if (drpType.SelectedValue == "2")
                {
                    foreach (ListItem item in lstSubjects.Items)
                    {
                        if (item.Selected == true)
                        {
                            if (SUBJIDs != "")
                            {
                                SUBJIDs += "," + item.Value.ToString();
                            }
                            else
                            {
                                SUBJIDs += item.Value.ToString();
                            }
                        }
                    }
                }
                else if (drpType.SelectedValue == "3")
                {
                    foreach (ListItem item in lstSubjects.Items)
                    {
                        if (item.Selected == true)
                        {
                            if (SUBJIDs != "")
                            {
                                SUBJIDs += "," + item.Value.ToString();
                            }
                            else
                            {
                                SUBJIDs += item.Value.ToString();
                            }
                        }
                    }

                    foreach (ListItem item in lstVisits.Items)
                    {
                        if (item.Selected == true)
                        {
                            if (VISITNUM != "")
                            {
                                VISITNUM += "," + item.Value.ToString();
                            }
                            else
                            {
                                VISITNUM += item.Value.ToString();
                            }
                        }
                    }
                }

                DataSet ds = dal.DM_FREEZE_SP(ACTION: "UPDATE_BULK_FREEZING",
                    INVID: INVID,
                    SUBJID: SUBJIDs,
                    VISITNUM: VISITNUM
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Multiple_Export_Excel.ToExcel(ds, drpType.SelectedItem.Text + ".xls", Page.Response);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data has already been frozen.'); window.location.href='DM_DataLock.aspx'", true);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}