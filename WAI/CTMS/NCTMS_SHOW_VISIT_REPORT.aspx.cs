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
    public partial class NCTMS_SHOW_VISIT_REPORT : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    FillINV();
                    FILL_MODULES();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillINV()
        {
            try
            {
                DataSet ds = dal.GetSiteID(
                Action: "INVID",
                PROJECTID: Session["PROJECTID"].ToString(),
                User_ID: Session["User_ID"].ToString()
                );

                ddlSiteId.DataSource = ds.Tables[0];
                ddlSiteId.DataValueField = "INVNAME";
                ddlSiteId.DataBind();
                ddlSiteId.Items.Insert(0, new ListItem("--Select--", "0"));

                FillVISITS_TYPE();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillVISITS_TYPE()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_VISITTYPE_MASTER"
                );

                drpVisitType.DataSource = ds.Tables[0];
                drpVisitType.DataValueField = "ID";
                drpVisitType.DataTextField = "VISIT_NAME";
                drpVisitType.DataBind();
                drpVisitType.Items.Insert(0, new ListItem("--Select Visit Type--", "0"));

                FillVISITS_NOM();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FillVISITS_NOM()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_VISIT_NOM_AGAINST_USER",
                SITEID: ddlSiteId.SelectedValue,
                VISITID: drpVisitType.SelectedValue
                );

                drpVisitID.DataSource = ds.Tables[0];
                drpVisitID.DataValueField = "VISIT_NOM";
                drpVisitID.DataTextField = "VISIT_NOM";
                drpVisitID.DataBind();
                drpVisitID.Items.Insert(0, new ListItem("--Select Visit Id--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpVisitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillVISITS_NOM();
                FILL_MODULES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlSiteId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillVISITS_TYPE();

                FillVISITS_NOM();

                FILL_MODULES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpVisitID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FILL_MODULES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            try
            {
                FILL_MODULES();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void FILL_MODULES()
        {
            try
            {
                DataSet ds = dal.CTMS_DATA_SP(
                ACTION: "GET_MODULES_AGAINST_VISIT_NOM_REPORT",
                SITEID: ddlSiteId.SelectedValue,
                VISITID: drpVisitID.SelectedValue,
                SVID: drpVisitID.SelectedItem.Text
                );

                if (ds.Tables[0].Rows.Count > 0)
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
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string MODULEID = ((Label)e.Item.FindControl("lblMODULEID")).Text;
                    LinkButton lblViewAllComment = (LinkButton)e.Item.FindControl("lblViewAllComment");
                    Label lblAllCommentCount = (Label)e.Item.FindControl("lblAllCommentCount");
                    Control anchor = e.Item.FindControl("anchor") as Control;

                    if (row["COMMENTCOUNT"].ToString() == "0")
                    {
                        lblViewAllComment.Visible = false;
                    }
                    else
                    {
                        lblViewAllComment.Visible = true;
                        lblAllCommentCount.Text = row["COMMENTCOUNT"].ToString();
                    }

                    GridView grd_Records = (GridView)e.Item.FindControl("grd_Records");

                    DataSet ds;

                    ds = dal.CTMS_DATA_SP(
                                  ACTION: "GET_DATA_REPORT",
                                  SVID: drpVisitID.SelectedValue,
                                  INVID: ddlSiteId.SelectedValue,
                                  VISITID: drpVisitType.SelectedValue,
                                  MODULEID: MODULEID
                                  );

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grd_Records.DataSource = ds;
                        grd_Records.DataBind();
                        anchor.Visible = true;
                    }
                    else
                    {
                        grd_Records.DataSource = null;
                        grd_Records.DataBind();
                        anchor.Visible = false;
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

                HtmlImage lnkAUDITTRAIL = (HtmlImage)e.Row.FindControl("lnkAUDITTRAIL");
                HtmlImage ADINITIAL = (HtmlImage)e.Row.FindControl("ADINITIAL");

                GridView grd_Records = (GridView)sender;

                string DELETE = dr["DELETE"].ToString();

                if (DELETE == "True")
                {
                    e.Row.Attributes.Add("class", "strikeThrough");
                }

                if (dr["AUDITSTATUS"].ToString() != "Initial Entry")
                {
                    ADINITIAL.Attributes.Add("class", "disp-none");
                    lnkAUDITTRAIL.Attributes.Add("class", "");
                }
                else
                {
                    lnkAUDITTRAIL.Attributes.Add("class", "disp-none");
                    ADINITIAL.Attributes.Add("class", "");
                }

                grd_Records.HeaderRow.Cells[4].Visible = false;
                grd_Records.HeaderRow.Cells[5].Visible = false;
                grd_Records.HeaderRow.Cells[6].Visible = false;
                grd_Records.HeaderRow.Cells[7].Visible = false;
                grd_Records.HeaderRow.Cells[8].Visible = false;

                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
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