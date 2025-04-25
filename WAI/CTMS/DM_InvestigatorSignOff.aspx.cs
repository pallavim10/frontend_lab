using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Web.UI.HtmlControls;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class DM_InvestigatorSignOff : System.Web.UI.Page
    {
        DAL_DM dal_DM = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
                DataSet ds = dal_DM.DM_SUBJECT_LIST_SP(INVID: drpInvID.SelectedValue);
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

                Grd_InvestigatorSignOff.DataSource = null;
                Grd_InvestigatorSignOff.DataBind();
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
                Grd_InvestigatorSignOff.DataSource = null;
                Grd_InvestigatorSignOff.DataBind();
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

                Grd_InvestigatorSignOff.DataSource = null;
                Grd_InvestigatorSignOff.DataBind();
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
                DataSet ds = dal_DM.DM_SIGNOFF_SP(ACTION: "GET_SIGNOFF_DATA",
                    INVID: drpInvID.SelectedValue,
                    SUBJID: drpSubID.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Grd_InvestigatorSignOff.DataSource = ds.Tables[0];
                    Grd_InvestigatorSignOff.DataBind();
                }
                else
                {
                    Grd_InvestigatorSignOff.DataSource = null;
                    Grd_InvestigatorSignOff.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Grd_InvestigatorSignOff_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Button Btn_Update = (Button)e.Row.FindControl("Btn_Update");
                    if (Session["SignOff_eCRF"].ToString() == "True")
                    {
                        Btn_Update.Visible = true;
                    }
                    else
                    {
                        Btn_Update.Visible = false;
                    }
                }

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
                    CheckBox Chk_InvSignOff = (CheckBox)e.Row.FindControl("Chk_InvSignOff");
                    LinkButton lnkQuery = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                    Label lblQuery = (Label)e.Row.FindControl("lblQuery");
                    HtmlGenericControl div = (HtmlGenericControl)e.Row.FindControl("anchor");

                    GridView Gridchild = (GridView)e.Row.FindControl("Gridchild");

                    if (dr["QueryCount"].ToString() != "0")
                    {
                        lnkQuery.Visible = true;
                        Chk_InvSignOff.Visible = false;
                        lblQuery.Visible = true;
                    }
                    else
                    {
                        lnkQuery.Visible = false;
                        Chk_InvSignOff.Visible = true;
                        lblQuery.Visible = false;
                    }

                    if (MULTIPLEYN.Text == "True")
                    {
                        div.Visible = true;
                        view.Visible = false;

                        DataSet ds = dal_DM.DM_SIGNOFF_SP(ACTION: "GET_MULTIPLE_RECORDS_SIGNOFF",
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

                    if (dr["QueryCount"].ToString() != "0")
                    {
                        lnkAUTOQUERY.Visible = true;
                    }
                    else
                    {
                        lnkAUTOQUERY.Visible = false;
                    }

                    Label lblQuery = (Label)e.Row.FindControl("lblQuery");

                    if (dr["DELETE"].ToString() == "True")
                    {
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
                ModalPopupExtender2.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            string str = Convert.ToString(txt_UserName.Text);
            string pass = Convert.ToString(txt_Pwd.Text);
            string compid = Session["User_ID"].ToString();
            ViewState["pass"] = pass;

            if (chkINVSIGN.Checked == true)
            {
                if (str == compid)
                {
                    ViewState["User_IDP"] = str;
                    Check_AUTH();
                    txt_UserName.Text = "";
                }
                else
                {
                    ModalPopupExtender2.Show();
                    Response.Write("<script>alert('Please enter valid User Id')</script>");
                }
            }
            else
            {
                txt_UserName.Text = "";
                txt_Pwd.Text = "";
                chkINVSIGN.Checked = false;
                ModalPopupExtender2.Show();
                Response.Write("<script>alert('Please Sign on Checkbox')</script>");
                txt_Pwd.Text = ViewState["pass"].ToString();
            }

        }

        private void Check_AUTH()
        {
            try
            {
                DAL_UMT dal_UMT = new DAL_UMT();

                DataSet dsAuth = dal_UMT.UMT_Auth(UserID: txt_UserName.Text, Pwd: txt_Pwd.Text);

                if (dsAuth.Tables.Count > 0 && dsAuth.Tables[0].Rows.Count > 0)
                {
                    string RESULT = dsAuth.Tables[0].Rows[0][0].ToString();

                    switch (RESULT)
                    {
                        case "Account Locked":
                            Response.Write("<script> alert('Your account has been locked'); window.location='Login.aspx';  </script>");

                            break;

                        case "Invalid Credentials, Account Locked":
                            Response.Write("<script> alert('Invalid credentials, Your account has been locked'); window.location='Login.aspx';  </script>");

                            break;

                        case "Invalid Credentials":
                            Response.Write("<script> alert('Invalid credentials');</script>");
                            ModalPopupExtender2.Show();

                            break;

                        case "Invalid User ID":
                            Response.Write("<script> alert('Invalid User ID');</script>");
                            ModalPopupExtender2.Show();

                            break;

                        default:
                            Investigator_SignOff();

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void Investigator_SignOff()
        {
            try
            {
                for (int i = 0; i < Grd_InvestigatorSignOff.Rows.Count; i++)
                {
                    CheckBox Chk_InvSignOff = (CheckBox)Grd_InvestigatorSignOff.Rows[i].FindControl("Chk_InvSignOff");

                    Label lblSUBJID = (Label)Grd_InvestigatorSignOff.Rows[i].FindControl("lblSUBJID");

                    Label lblVISITNUM = (Label)Grd_InvestigatorSignOff.Rows[i].FindControl("lblVISITNUM");

                    Label lblVISIT = (Label)Grd_InvestigatorSignOff.Rows[i].FindControl("lblVISIT");

                    Label lblMODULEID = (Label)Grd_InvestigatorSignOff.Rows[i].FindControl("lblMODULEID");

                    Label lblMODULENAME = (Label)Grd_InvestigatorSignOff.Rows[i].FindControl("lblMODULENAME");

                    Label lblPVID = (Label)Grd_InvestigatorSignOff.Rows[i].FindControl("lblPVID");

                    Label MULTIPLEYN = (Label)Grd_InvestigatorSignOff.Rows[i].FindControl("MULTIPLEYN");

                    Label lblTableName = (Label)Grd_InvestigatorSignOff.Rows[i].FindControl("lblTableName");

                    Label lblRECID = (Label)Grd_InvestigatorSignOff.Rows[i].FindControl("lblRECID");

                    Label PAGESTATUS = (Label)Grd_InvestigatorSignOff.Rows[i].FindControl("PAGESTATUS");

                    if (PAGESTATUS.Text == "2")
                    {
                        if (Chk_InvSignOff.Checked)
                        {
                            dal_DM.DM_SIGNOFF_SP(ACTION: "UPDATE_SIGNOFF_STATUS",
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
                        if (Chk_InvSignOff.Checked)
                        {
                            if (MULTIPLEYN.Text == "True")
                            {
                                GridView Gridchild = (GridView)Grd_InvestigatorSignOff.Rows[i].FindControl("Gridchild");

                                for (int j = 0; j < Gridchild.Rows.Count; j++)
                                {
                                    dal_DM.DM_SIGNOFF_SP(ACTION: "UPDATE_SIGNOFF_STATUS",
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
                            else
                            {
                                dal_DM.DM_SIGNOFF_SP(ACTION: "UPDATE_SIGNOFF_STATUS",
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

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Signed off Successfully'); window.location.href='DM_InvestigatorSignOff.aspx'", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }
    }
}