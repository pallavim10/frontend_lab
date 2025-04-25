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
    public partial class CTMS_SDV_VISIT_MODULES : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetVisit();
                    GetModule();
                    GetIndication();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void GetVisit()
        {
            try
            {
                DataSet ds = dal.CTMS_SDV_DATA(ACTION: "GET_VISIT", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdVisit.DataSource = ds.Tables[0];
                    grdVisit.DataBind();

                    drpModuleVisit.DataSource = ds.Tables[0];
                    drpModuleVisit.DataTextField = "VISIT";
                    drpModuleVisit.DataValueField = "VISITNUM";
                    drpModuleVisit.DataBind();
                }
                else
                {
                    grdVisit.DataSource = null;
                    grdVisit.DataBind();

                    drpModuleVisit.DataSource = null;
                    drpModuleVisit.DataBind();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetIndication()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_INDICATION", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpIndication.DataSource = ds.Tables[0];
                    drpIndication.DataValueField = "ID";
                    drpIndication.DataTextField = "INDICATION";
                    drpIndication.DataBind();
                    drpIndication.Items.Insert(0, new ListItem("--Select--", "0"));

                    drpModuleIndication.DataSource = ds.Tables[0];
                    drpModuleIndication.DataValueField = "ID";
                    drpModuleIndication.DataTextField = "INDICATION";
                    drpModuleIndication.DataBind();
                    drpModuleIndication.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnsubmitSectionVisit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_SDV_DATA(ACTION: "INSERT_VISIT", PROJECTID: Session["PROJECTID"].ToString(), VISITNUM: txtVisitID.Text, VISIT: txtVisitName.Text, USERID: Session["User_ID"].ToString(), INDICATION: drpIndication.SelectedValue
                );
                txtVisitID.Text = "";
                txtVisitName.Text = "";
                drpIndication.SelectedValue = "0";
                GetVisit();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnupdateSectionVisit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_SDV_DATA(ACTION: "UPDATE_VISIT", PROJECTID: Session["PROJECTID"].ToString(), VISITNUM: txtVisitID.Text, VISIT: txtVisitName.Text, USERID: Session["User_ID"].ToString(), INDICATION: drpIndication.SelectedValue);
                txtVisitID.Text = "";
                txtVisitName.Text = "";
                drpIndication.SelectedValue = "0";
                btnupdateSectionVisit.Visible = false;
                btnsubmitSectionVisit.Visible = true;
                GetVisit();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btncancelSectionVisit_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdVisit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();

                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string INDICATION = (gvr.FindControl("lblINDICATIONID") as Label).Text;

                Session["VISITID"] = id;
                if (e.CommandName == "EditVisit")
                {
                    DataSet ds = dal.CTMS_SDV_DATA(ACTION: "GET_VISIT_BYID", PROJECTID: Session["PROJECTID"].ToString(), VISITNUM: Session["VISITID"].ToString(), INDICATION: INDICATION);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtVisitID.Text = ds.Tables[0].Rows[0]["VISITNUM"].ToString();
                        txtVisitName.Text = ds.Tables[0].Rows[0]["VISIT"].ToString();
                        drpIndication.SelectedValue = ds.Tables[0].Rows[0]["INDICATION"].ToString();
                        btnupdateSectionVisit.Visible = true;
                        btnsubmitSectionVisit.Visible = false;
                    }
                }
                else if (e.CommandName == "DeleteVisit")
                {
                    DataSet ds = dal.CTMS_SDV_DATA(ACTION: "DELETE_VISIT", PROJECTID: Session["PROJECTID"].ToString(), VISITNUM: Session["VISITID"].ToString(), INDICATION: INDICATION);
                    GetVisit();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetModule()
        {
            try
            {
                DataSet ds = dal.CTMS_SDV_DATA(ACTION: "GET_MODULENAME", PROJECTID: Session["PROJECTID"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdModule.DataSource = ds.Tables[0];
                    grdModule.DataBind();
                }
                else
                {
                    grdModule.DataSource = null;
                    grdModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSubmitModule_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_SDV_DATA
                (
                ACTION: "INSERT_MODULENAME",
                PROJECTID: Session["PROJECTID"].ToString(),
                MODULENAME: txtModuleName.Text,
                SEQNO: txtModuleSeqNo.Text,
                USERID: Session["User_ID"].ToString()
                );

                txtModuleName.Text = "";
                txtModuleSeqNo.Text = "";
                GetModule();
                GETMODULEBYINDICATIONCHANGE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnUpdateModule_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = dal.CTMS_SDV_DATA(ACTION: "UPDATE_MODULENAME", MODULENAME: txtModuleName.Text, SEQNO: txtModuleSeqNo.Text, ID: Session["ID"].ToString());

                txtModuleName.Text = "";
                txtModuleSeqNo.Text = "";
                btnSubmitModule.Visible = true;
                btnUpdateModule.Visible = false;

                GetModule();
                GETMODULEBYINDICATIONCHANGE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnCancelModule_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void grdModule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                string id = e.CommandArgument.ToString();
                Session["ID"] = id;
                if (e.CommandName == "EditModule")
                {
                    DataSet ds = dal.CTMS_SDV_DATA(ACTION: "GET_MODULENAME_BYID", ID: Session["ID"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtModuleName.Text = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                        txtModuleSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                        btnUpdateModule.Visible = true;
                        btnSubmitModule.Visible = false;


                    }
                }
                else if (e.CommandName == "DeleteModule")
                {
                    DataSet ds = dal.CTMS_SDV_DATA(ACTION: "DELETE_MODULENAME", ID: Session["ID"].ToString());
                    GetModule();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpModuleIndication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETMODULEBYINDICATIONCHANGE();
                GETMODULEBYVISITCHANGE();
                GetAddedModule();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        public void GETMODULEBYINDICATIONCHANGE()
        {
            try
            {
                DataSet ds = dal.CTMS_SDV_DATA(ACTION: "GET_MODULESBY_INDICATIONCHANGE", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: drpModuleIndication.SelectedValue, VISITNUM: drpModuleVisit.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdModuleVisit.DataSource = ds.Tables[1];
                    grdModuleVisit.DataBind();

                    drpModuleVisit.DataSource = ds.Tables[0];
                    drpModuleVisit.DataValueField = "VISITNUM";
                    drpModuleVisit.DataTextField = "VISIT";
                    drpModuleVisit.DataBind();
                    //  drpModuleVisit.Items.Insert(0, new ListItem("--Select--", "Select"));
                }
                else
                {
                    drpModuleVisit.Items.Clear();

                    grdModuleVisit.DataSource = null;
                    grdModuleVisit.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GETMODULEBYVISITCHANGE()
        {
            try
            {

                DataSet ds = dal.CTMS_SDV_DATA(ACTION: "GET_MODULEBY_INDICATION_VISIT", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: drpModuleIndication.SelectedValue, VISITNUM: drpModuleVisit.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdModuleVisit.DataSource = ds.Tables[0];
                    grdModuleVisit.DataBind();
                }
                else
                {
                    grdModuleVisit.DataSource = null;
                    grdModuleVisit.DataBind();

                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        public void GetAddedModule()
        {
            try
            {
                DataSet ds = dal.CTMS_SDV_DATA(ACTION: "GET_MODULEBY_INDICATION", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: drpModuleIndication.SelectedValue, VISITNUM: drpModuleVisit.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void drpModuleVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GETMODULEBYVISITCHANGE();
                GetAddedModule();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }



        protected void btnAddModule_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdModuleVisit.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)grdModuleVisit.Rows[i].FindControl("chkVisit");

                    if (ChAction.Checked)
                    {
                        Label MODULEID = (Label)grdModuleVisit.Rows[i].FindControl("MODULEID");
                        Label MODULENAME = (Label)grdModuleVisit.Rows[i].FindControl("MODULENAME");

                        DataSet ds = dal.CTMS_SDV_DATA
                        (
                        ACTION: "INSERT_CTMS_SDV_DETAILS",
                        PROJECTID: Session["PROJECTID"].ToString(),
                        VISITNUM: drpModuleVisit.SelectedValue,
                        INDICATION: drpModuleIndication.SelectedValue,
                        MODULEID: MODULEID.Text,
                        MODULENAME: MODULENAME.Text,
                        USERID: Session["User_ID"].ToString()
                        );
                    }
                }
                GETMODULEBYVISITCHANGE();
                GetAddedModule();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnClearModule_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DAL dal;
                dal = new DAL();
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string ID = (gvr.FindControl("lblID") as Label).Text;

                if (e.CommandName == "DeleteField")
                {
                    DataSet ds = dal.CTMS_SDV_DATA(ACTION: "DELETE_CTMS_SDV_DETAILS", ID: ID);
                    GetAddedModule();
                    GETMODULEBYVISITCHANGE();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}