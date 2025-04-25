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
    public partial class DM_DataEntry_Mutiple : System.Web.UI.Page
    {
        DAL_DM dal = new DAL_DM();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["INVID"] == null)
            {
                Response.Redirect("SessionExpired.aspx", true);
                return;
            }
            if (!this.IsPostBack)
            {
                GetVisit();
                GETVISITCOUNT();
                GetModule();

                if (drpModule.SelectedValue == "0")
                {
                    lblModuleName.Text = "";
                    btnAdddNew.Visible = false;
                }
                else
                {
                    lblModuleName.Text = drpModule.SelectedItem.Text;
                }

                Session["PVID"] = Session["PROJECTID"].ToString() + "-" + Session["INVID"].ToString() + "-" + Session["SUBJID"].ToString() + "-" + Session["VISITID"].ToString() + "-" + Session["MODULEID"].ToString() + "-" + Session["VISITCOUNT"].ToString();
                Session["VISITNUM"] = Session["VISITID"].ToString();
                Session["PAGENUM"] = Session["MODULEID"].ToString();
                Session["PAGESTATUS"] = "0";
                Session["DATAFREEZELOCKSTATUS"] = "0";

                lblSiteId.Text = "SITE ID : " + Session["INVID"].ToString();
                lblSubjectId.Text = "SUBJECT ID : " + Session["SUBJID"].ToString();
                lblVisit.Text = "VISIT : " + Session["VISIT"].ToString();
                lblIndication.Text = "INDICATION : " + Session["Indication"].ToString();
                lblPVID.Text = Session["PVID"].ToString();

                GetPageInfo();
                GetDataExists();

                if (Session["UserGroup_ID"].ToString() == "CRA" || Session["UserGroup_ID"].ToString() == "CDM")
                {
                    btnAdddNew.Visible = false;
                }
            }
        }

        protected void GetDataExists()
        {
            try
            {
                string PVID = Session["PVID"].ToString();

                DataSet ds = dal.GetSet_DM_ProjectData(
                      Action: "CHECK_DATA_EXISTS",
                      PVID: Session["PVID"].ToString(),
                      PROJECTID: Session["PROJECTID"].ToString(),
                      VISITNUM: Session["VISITID"].ToString(),
                      MODULEID: Session["MODULEID"].ToString(),
                      INDICATION: Session["Indication"].ToString(),
                      TABLENAME: "DSAE"
                      );

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_DATA.DataSource = ds;
                        grd_DATA.DataBind();
                    }
                    else
                    {
                        if (drpModule.SelectedValue == "Select")
                        {
                            lblErrorMsg.Text = "";
                        }
                        else
                        {
                            lblErrorMsg.Text = "No Record Found";
                        }
                    }
                }

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int remain = 0, Count_Data = 0, Count_DRP = 0;
                        string FIELDNAME = null;
                        Count_Data = Convert.ToInt32(ds.Tables[1].Rows[0]["Count_Data"]);
                        Count_DRP = Convert.ToInt32(ds.Tables[1].Rows[0]["Count_DRP"]);
                        FIELDNAME = ds.Tables[1].Rows[0]["FIELDNAME"].ToString();
                        remain = Convert.ToInt32(Count_DRP) - Convert.ToInt32(Count_Data);

                        if (remain != 0)
                        {
                            lblRemaining.Text = "Note : " + remain + " out of " + Count_DRP + " " + FIELDNAME + " not entered.";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void grd_DATA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                string AUTOQUERY = dr["AutoQueryCount"].ToString();
                string MANUALQUERY = dr["ManQueryCount"].ToString();
                string COMMENTS = dr["ComCount"].ToString();
                string AUDITTRAIL = dr["AuditCount"].ToString();

                if (AUTOQUERY == "")
                {
                    HtmlImage btnEdit = (HtmlImage)e.Row.FindControl("lnkAUTOQUERY");
                    btnEdit.Attributes.Add("class", "disp-none");
                }
                //if (MANUALQUERY == "")
                //{
                //    HtmlImage btnEdit = (HtmlImage)e.Row.FindControl("lnkMANUALQUERY");
                //    btnEdit.Attributes.Add("class", "disp-none");
                //}
                //if (COMMENTS == "")
                //{
                //    HtmlImage btnEdit = (HtmlImage)e.Row.FindControl("lnkCOMMENTS");
                //    btnEdit.Attributes.Add("class", "disp-none");
                //}
                //if (AUDITTRAIL == "")
                //{
                //    HtmlImage btnEdit = (HtmlImage)e.Row.FindControl("lnkAUDITTRAIL");
                //    btnEdit.Attributes.Add("class", "disp-none");
                //}

                grd_DATA.HeaderRow.Cells[6].Visible = false;
                grd_DATA.HeaderRow.Cells[7].Visible = false;
                grd_DATA.HeaderRow.Cells[8].Visible = false;
                grd_DATA.HeaderRow.Cells[9].Visible = false;
                grd_DATA.HeaderRow.Cells[10].Visible = false;

                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                //  grd_DATA.HeaderRow.Cells[0].Visible = false;

                // e.Columns("").Visible = False;

            }

        }
        protected void lnkPAGENUM_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label REC_ID = (Label)row.FindControl("lblRECID");

                Session["RECID"] = REC_ID.Text;
                string PVID = Session["PVID"].ToString();

                Response.Redirect("DM_DataEntry_MultipleData.aspx");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void btnAdddNew_Click(object sender, EventArgs e)
        {
            //Session["ViewStatus"] = "1";
            Session["RECID"] = "-1";
            Session["DATAFREEZELOCKSTATUS"] = "0";

            Response.Redirect("DM_DataEntry_MultipleData.aspx");
        }

        private void GetPageInfo()
        {
            try
            {
                //DataSet ds = dal.GetPAGEINFO(Session["PVID"].ToString());
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    if (ds.Tables[0].Rows[0]["PAGESTATUS"].ToString() == "1")
                //    {
                //        Session["DATAFREEZELOCKSTATUS"] = "1";
                //        Session["PAGESTATUS"] = ds.Tables[0].Rows[0]["PAGESTATUS"].ToString();
                //    }
                //    else
                //    {
                //        Session["PAGESTATUS"] = ds.Tables[0].Rows[0]["PAGESTATUS"].ToString();
                //        Session["DATAFREEZELOCKSTATUS"] = "0";
                //    }
                //    if (ds.Tables[0].Rows[0]["LOCKSTATUS"].ToString() == "True")
                //    {
                //        //  throw new Exception("THIS PAGE IS LOCKED (DATA LOCK)");
                //        Session["DATAFREEZELOCKSTATUS"] = "0";
                //        lblErrorMsg.Text = "THIS PAGE IS LOCKED (DATA LOCK)";
                //        grd_DATA.Enabled = false;
                //        btnAdddNew.Visible = false;
                //        return;
                //    }
                //    if (ds.Tables[0].Rows[0]["FREZSTATUS"].ToString() == "True")
                //    {
                //        // throw new Exception("THIS PAGE IS FREEZED");
                //        Session["DATAFREEZELOCKSTATUS"] = "0";
                //        lblErrorMsg.Text = "THIS PAGE IS FREEZED";
                //        grd_DATA.Enabled = false;
                //        btnAdddNew.Visible = false;
                //    }
                //}
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetVisit()
        {
            try
            {
                //DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_DM_PROJECTMASTER_VISIT", PROJECTID: Session["PROJECTID"].ToString(), INDICATION: Session["Indication"].ToString());
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    drpVisit.DataSource = ds.Tables[0];
                //    drpVisit.DataValueField = "VISITNUM";
                //    drpVisit.DataTextField = "VISIT";
                //    drpVisit.DataBind();
                //    // drpVisit.Items.Insert(0, new ListItem("--Select--", "Select"));

                //    if (Session["VISITID"] != null)
                //    {
                //        if (drpVisit.Items.FindByValue(Session["VISITID"].ToString()) != null)
                //        {
                //            drpVisit.Items.FindByValue(Session["VISITID"].ToString()).Selected = true;
                //        }
                //    }
                //}
                //else
                //{
                //    drpVisit.Items.Clear();
                //}
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
                //DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_MODULEMASTER_BYINDICATION", PROJECTID: Session["PROJECTID"].ToString(), VISITNUM: Session["VISITID"].ToString(), INDICATION: Session["Indication"].ToString(), SUBJECTID: Session["SUBJID"].ToString(), VISITCOUNT: Session["VISITCOUNT"].ToString());

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    drpModule.DataSource = ds.Tables[0];
                //    drpModule.DataValueField = "ID";
                //    drpModule.DataTextField = "MODULENAME";
                //    drpModule.DataBind();

                //    if (Session["MODULEID"] != null)
                //    {
                //        if (drpModule.Items.FindByValue(Session["MODULEID"].ToString()) != null)
                //        {
                //            drpModule.Items.FindByValue(Session["MODULEID"].ToString()).Selected = true;
                //        }
                //    }

                //    drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
                //}
                //else
                //{
                //    drpModule.Items.Clear();
                //    drpModule.Items.Insert(0, new ListItem("--Select--", "0"));
                //}
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GETVISITCOUNT()
        {
            try
            {
                drpVisitCount.Items.Insert(0, new ListItem("1", "1"));
                Session["VISITCOUNT"] = "1";

                if (Session["VISITCOUNT"] != null)
                {
                    if (drpVisitCount.Items.FindByValue(Session["VISITCOUNT"].ToString()) != null)
                    {
                        drpVisitCount.Items.FindByValue(Session["VISITCOUNT"].ToString()).Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["VISITID"] = drpVisit.SelectedValue;
                Session["VISIT"] = drpVisit.SelectedItem.Text;
                // Session["VISITCOUNT"] = drpVisitCount.SelectedValue;
                GetModule();
                //Session["MODULEID"] = drpModule.SelectedValue;
                //  Session["MODULENAME"] = drpModule.SelectedItem.Text;

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpModule_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                // Session["VISITID"] = drpVisit.SelectedValue;
                // Session["VISIT"] = drpVisit.SelectedItem.Text;
                Session["MODULEID"] = drpModule.SelectedValue;
                Session["MODULENAME"] = drpModule.SelectedItem.Text;
                // Session["VISITCOUNT"] = drpVisitCount.SelectedValue;
                //DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_MODULE_MULTIPLE", PROJECTID: Session["PROJECTID"].ToString(), VISITNUM: Session["VISITID"].ToString(), MODULENAME: Session["MODULENAME"].ToString());
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "True")
                //    {
                //        Response.Redirect(Request.RawUrl);
                //    }
                //    if (ds.Tables[0].Rows[0]["MULTIPLEYN"].ToString() == "False")
                //    {
                //        Response.Redirect("DM_DataEntry.aspx");
                //    }
                //}
                //else
                //{
                //    Response.Redirect(Request.RawUrl);
                //}
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }

        protected void drpVisitCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Session["VISITID"] = drpVisit.SelectedValue;
                // Session["VISIT"] = drpVisit.SelectedItem.Text;
                Session["VISITCOUNT"] = drpVisitCount.SelectedValue;
                GetModule();
                // Session["MODULEID"] = drpModule.SelectedValue;
                //Session["MODULENAME"] = drpModule.SelectedItem.Text;
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }
    }
}