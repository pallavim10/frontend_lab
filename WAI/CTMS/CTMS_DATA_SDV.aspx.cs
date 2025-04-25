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
    public partial class CTMS_DATA_SDV : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["UserGroup_ID"] == null)
                    {
                        Response.Redirect("SessionExpired.aspx", true);
                        return;
                    }
                    FillINV();
                    GetIndication();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void FillINV()
        {
            DAL dal;
            dal = new DAL();
            DataSet ds = dal.GetSiteID(
            Action: "INVID",
            Project_Name: Session["PROJECTIDTEXT"].ToString(),
            User_ID: Session["User_ID"].ToString()
            );
            drpInvID.DataSource = ds.Tables[0];
            drpInvID.DataValueField = "INVNAME";
            drpInvID.DataBind();

            if (Session["UserGroup_ID"].ToString() == "Investigator" || Session["UserGroup_ID"].ToString() == "Co_Investigator")
            {
                string Userid = Session["User_ID"].ToString();
                DivINV.Visible = false;
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
                    BINDVISITDATA();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void drpIndication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                //BINDSTATUSDATA();
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
                DataSet ds = dal.CTMS_SDV_DATA(ACTION: "GET_CTMS_SUBJECTS", INVID: drpInvID.SelectedValue, INDICATION: drpIndication.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        public void BINDVISITDATA()
        {
            try
            {
                DataSet ds = dal.CTMS_SDV_DATA(ACTION: "GET_VISITBY_INDICATION", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: drpIndication.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdData1.DataSource = ds.Tables[0];
                    grdData1.DataBind();
                }
                else
                {
                    grdData1.DataSource = ds.Tables[0];
                    grdData1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void GridView1_PreRender(object sender, EventArgs e)
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

        protected void gridData1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string VISITNUM = dr["VISITNUM"].ToString();
                    GridView gridData2 = e.Row.FindControl("grdData2") as GridView;

                    try
                    {
                        DataSet ds = dal.CTMS_SDV_DATA(ACTION: "GET_CTMS_VISIT_PAGE_DATA", PROJECTID: Session["PROJECTID"].ToString(), INVID: drpInvID.SelectedValue, SUBJECTID: drpSubID.SelectedValue, VISITNUM: VISITNUM, INDICATION: drpIndication.SelectedValue);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gridData2.DataSource = ds.Tables[0];
                            gridData2.DataBind();
                        }
                        else
                        {
                            gridData2.DataSource = ds.Tables[0];
                            gridData2.DataBind();
                        }

                        Control anchor = e.Row.FindControl("anchor") as Control;
                        if (gridData2.Rows.Count > 0)
                        {
                            anchor.Visible = true;
                        }
                        else
                        {
                            anchor.Visible = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        lblErrorMsg.Text = ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void gridData2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    CheckBox chkSDV = (CheckBox)e.Row.FindControl("chkSDV");
                    string SDV = dr["SDV"].ToString();

                    if (SDV == "True")
                    {
                        chkSDV.Checked = true;
                    }
                    else
                    {
                        chkSDV.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                btnSDV.Visible = true;
                BINDVISITDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void btnSDV_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdData1.Rows.Count; i++)
                {
                    string VISITNUM = ((Label)grdData1.Rows[i].FindControl("lblVISITNUM")).Text;
                    GridView grdData2 = (GridView)grdData1.Rows[i].FindControl("grdData2");
                    for (int j = 0; j < grdData2.Rows.Count; j++)
                    {
                        CheckBox chkSDV = (CheckBox)grdData2.Rows[j].FindControl("chkSDV");
                        string MODULEID = ((Label)grdData2.Rows[j].FindControl("lblMODULEID")).Text;
                        string MODULENAME = ((Label)grdData2.Rows[j].FindControl("lblMODULENAME")).Text;
                        if (chkSDV.Checked == true)
                        {
                            string ID = ((Label)grdData2.Rows[j].FindControl("lblID")).Text;
                            DataSet ds = dal.CTMS_SDV_DATA(ACTION: "Insert_SDV", PROJECTID: Session["PROJECTID"].ToString(),VISITNUM:VISITNUM,INDICATION:drpIndication.SelectedValue,INVID:drpInvID.SelectedValue,MODULEID:MODULEID,
                            MODULENAME:MODULENAME, USERID: Session["User_ID"].ToString(),SUBJECTID:drpSubID.SelectedValue);                         
                        }
                    }
                }
                BINDVISITDATA();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}