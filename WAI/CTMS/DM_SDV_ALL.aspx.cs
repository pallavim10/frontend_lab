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
    public partial class DM_SDV_ALL : System.Web.UI.Page
    {
        DAL_DM dal = new DAL_DM();
        CommonFunction.CommonFunction comm = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] == null)
                {
                    Response.Redirect("SessionExpired.aspx", false);
                }
                else if (!IsPostBack)
                {
                    FillINV();
                    FillSubject();
                    FillVisit();
                    GET_MODULE();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
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

            if (Session["DM_CRF_INVID"] != null)
            {
                if (drpInvID.Items.Contains(new ListItem(Session["DM_CRF_INVID"].ToString())))
                {
                    drpInvID.SelectedValue = Session["DM_CRF_INVID"].ToString();
                }
            }

            Session["DM_CRF_INVID"] = drpInvID.SelectedValue;
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                FillVisit();
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
                    //drpSubID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSubID.Items.Clear();
                }

                if (Session["DM_CRF_SUBJID"] != null)
                {
                    if (drpSubID.Items.Contains(new ListItem(Session["DM_CRF_SUBJID"].ToString())))
                    {
                        drpSubID.SelectedValue = Session["DM_CRF_SUBJID"].ToString();
                    }
                }
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
                GET_MODULE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillVisit()
        {
            try
            {
                DataSet ds = dal.DM_VISIT_SP(SUBJID: drpSubID.SelectedValue);

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
                    drpVisit.Items.Insert(0, new ListItem("--Select Visit--", "Select"));
                }
                else
                {
                    drpVisit.Items.Clear();
                }

                if (Session["DM_CRF_VISIT"] != null)
                {
                    if (drpVisit.Items.FindByValue(Session["DM_CRF_VISIT"].ToString()) != null)
                    {
                        drpVisit.SelectedValue = Session["DM_CRF_VISIT"].ToString();
                    }
                }
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

        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_MODULE();

                Session["DM_CRF_VISIT"] = drpVisit.SelectedValue;
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
                GET_MODULE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void GET_MODULE()
        {
            try
            {
                if (drpVisit.SelectedValue != "0")
                {
                    DataSet ds = dal.DM_OPEN_CRF_SP(
                    SUBJID: drpSubID.SelectedValue,
                    VISITNUM: drpVisit.SelectedValue
                    );

                    for (int i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];

                        if (dr["CritID"].ToString() != "" && dr["CritID"].ToString() != "0" && !toBeDeleted(dr["CritID"].ToString()))
                        {
                            dr.Delete();
                        }
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        repeatData.DataSource = ds;
                        repeatData.DataBind();
                    }
                    else
                    {
                        repeatData.DataSource = null;
                        repeatData.DataBind();
                    }
                }
                else
                {
                    repeatData.DataSource = null;
                    repeatData.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private bool toBeDeleted(string CritID)
        {
            bool res = false;
            try
            {
                DataSet ds = dal.DM_MODULE_CRITERIA_SP(
                ID: CritID,
                SUBJID: drpSubID.SelectedValue,
                VISITNUM: drpVisit.SelectedValue,
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

        protected void repeatData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    GridView grd_Records = (GridView)e.Item.FindControl("grd_Records");

                    DataSet ds;

                    string PVID = Session["PROJECTID"].ToString() + "-" + drpInvID.SelectedValue + "-" + drpSubID.SelectedValue + "-" + drpVisit.SelectedValue + "-" + row["MODULEID"].ToString() + "-" + 1;

                    ds = dal.DM_VISIT_OPEN_CRF_SDV_SP(
                        PVID: PVID,
                        SUBJID: drpSubID.SelectedValue,
                        VISITNUM: drpVisit.SelectedValue,
                        MODULEID: row["MODULEID"].ToString(),
                        TABLENAME: row["TABLENAME"].ToString()
                        );

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        grd_Records.DataSource = ds;
                        grd_Records.DataBind();
                    }
                    else
                    {
                        grd_Records.DataSource = null;
                        grd_Records.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void grd_Records_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                LinkButton lnkPAGENUM = (LinkButton)e.Row.FindControl("lnkPAGENUM");
                LinkButton lnkQuery = (LinkButton)e.Row.FindControl("lnkQUERYSTATUS");
                LinkButton lnkQueryAns = (LinkButton)e.Row.FindControl("lnkQUERYANS");
                LinkButton lnkQueryClose = (LinkButton)e.Row.FindControl("lnkQUERYCLOSE");
                LinkButton AD = (LinkButton)e.Row.FindControl("AD");
                LinkButton lbtnSDV = (LinkButton)e.Row.FindControl("lbtnSDV");
                LinkButton lbtnSDVDONE = (LinkButton)e.Row.FindControl("lbtnSDVDONE");
                HtmlControl ADICON = (HtmlControl)e.Row.FindControl("ADICON");

                DataSet dsMOD = dal.DM_MULTIPLE_RECORS_SP(ACTION: "GET_MODULE_FOR_SDV",
                    MODULEID: Request.QueryString["MODULEID"].ToString());

                if (dsMOD.Tables.Count > 0 && dsMOD.Tables[0].Rows.Count > 0 && dsMOD.Tables[0].Rows[0]["SDV"].ToString() == "0")
                {
                    lbtnSDVDONE.CssClass = "disp-none";
                    lbtnSDV.CssClass = "disp-none";
                }
                else
                {
                    if (dr["SDVSTATUS"].ToString() == "1")
                    {
                        lbtnSDVDONE.CssClass = lbtnSDVDONE.CssClass.Replace("disp-none", "");
                        lbtnSDV.CssClass = "disp-none";
                    }
                    else
                    {
                        lbtnSDVDONE.CssClass = "disp-none";
                        lbtnSDV.CssClass = lbtnSDV.CssClass.Replace("disp-none", "");
                    }
                }

                string DELETE = dr["DELETE"].ToString();

                Label lblDeletedStatus = (Label)e.Row.FindControl("lblDeletedStatus");
                if (DELETE == "True")
                {
                    lblDeletedStatus.Visible = true;
                    e.Row.CssClass = "brd-1px-maroonimp";
                    lbtnSDV.Visible = false;
                    lbtnSDVDONE.Visible = false;
                }
                else
                {
                    lnkPAGENUM.Visible = true;
                    lblDeletedStatus.Visible = false;
                }

                if (dr["QryCount"].ToString() != "0")
                {
                    lnkQuery.Attributes.Add("class", "");
                    lnkQuery.Visible = true;
                }
                else
                {
                    lnkQuery.Visible = false;
                }

                if (dr["QryAnsCount"].ToString() != "0")
                {
                    lnkQueryAns.Attributes.Add("class", "");
                    lnkQueryAns.Visible = true;
                }
                else
                {
                    lnkQueryAns.Visible = false;
                }

                if (dr["QryClosedCount"].ToString() != "0")
                {
                    lnkQueryClose.Attributes.Add("class", "");
                    lnkQueryClose.Visible = true;
                }
                else
                {
                    lnkQueryClose.Visible = false;
                }

                if (dr["AUDITSTATUS"].ToString() == "")
                {
                    AD.Attributes.Add("class", "disp-none");
                }
                else if (dr["AUDITSTATUS"].ToString() != "Initial Entry")
                {
                    AD.Attributes.Add("class", "");
                    ADICON.Attributes.Add("style", "color: red;font-size: 17px;");
                }
                else
                {
                    AD.Attributes.Add("class", "");
                    ADICON.Attributes.Add("style", "color: green;font-size: 17px;");
                }

                //if (Convert.ToString(Session["MEDAUTH_FORM"]) != "True" || Convert.ToString(Session["MEDAUTH_FIELD"]) != "True")
                //{
                //    lbtnSDV.CssClass = "disp-none";
                //    lbtnSDVDONE.CssClass = "disp-none";
                //}

                if (dr["FREEZESTATUS"].ToString() == "True" || dr["LOCKSTATUS"].ToString() == "True")
                {
                    lbtnSDV.CssClass = "disp-none";
                    lbtnSDVDONE.CssClass = "disp-none";
                }

                GridView grd_Records = (GridView)sender;
                grd_Records.HeaderRow.Cells[13].Visible = false;
                grd_Records.HeaderRow.Cells[14].Visible = false;
                grd_Records.HeaderRow.Cells[15].Visible = false;
                grd_Records.HeaderRow.Cells[16].Visible = false;
                grd_Records.HeaderRow.Cells[17].Visible = false;
                grd_Records.HeaderRow.Cells[18].Visible = false;
                grd_Records.HeaderRow.Cells[19].Visible = false;
                grd_Records.HeaderRow.Cells[20].Visible = false;
                grd_Records.HeaderRow.Cells[21].Visible = false;
                grd_Records.HeaderRow.Cells[22].Visible = false;
                grd_Records.HeaderRow.Cells[23].Visible = false;
                grd_Records.HeaderRow.Cells[24].Visible = false;
                grd_Records.HeaderRow.Cells[25].Visible = false;
                grd_Records.HeaderRow.Cells[26].Visible = false;
                grd_Records.HeaderRow.Cells[27].Visible = false;
                grd_Records.HeaderRow.Cells[28].Visible = false;
                grd_Records.HeaderRow.Cells[29].Visible = false;
                grd_Records.HeaderRow.Cells[30].Visible = false;
                grd_Records.HeaderRow.Cells[31].Visible = false;
                grd_Records.HeaderRow.Cells[32].Visible = false;
                grd_Records.HeaderRow.Cells[33].Visible = false;
                grd_Records.HeaderRow.Cells[34].Visible = false;
                grd_Records.HeaderRow.Cells[35].Visible = false;
                grd_Records.HeaderRow.Cells[36].Visible = false;
                grd_Records.HeaderRow.Cells[37].Visible = false;

                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;
                e.Row.Cells[21].Visible = false;
                e.Row.Cells[22].Visible = false;
                e.Row.Cells[23].Visible = false;
                e.Row.Cells[24].Visible = false;
                e.Row.Cells[25].Visible = false;
                e.Row.Cells[26].Visible = false;
                e.Row.Cells[27].Visible = false;
                e.Row.Cells[28].Visible = false;
                e.Row.Cells[29].Visible = false;
                e.Row.Cells[30].Visible = false;
                e.Row.Cells[31].Visible = false;
                e.Row.Cells[32].Visible = false;
                e.Row.Cells[33].Visible = false;
                e.Row.Cells[34].Visible = false;
                e.Row.Cells[35].Visible = false;
                e.Row.Cells[36].Visible = false;
                e.Row.Cells[37].Visible = false;
            }
        }

        protected void grd_data_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv.ShowHeader == true && gv.Rows.Count > 0)
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
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.ToString();

            }
        }
    }
}